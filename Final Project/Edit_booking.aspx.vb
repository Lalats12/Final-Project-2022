Imports System.Data.SqlClient
Public Class Edit_bookingaspx
    Inherits System.Web.UI.Page
    Dim conn As SqlConnection
    Dim loadCourtsCmd As SqlCommand
    Dim getCourtsCmd As SqlCommand
    Dim getPaymentCmd As SqlCommand
    Dim editPaymentCmd As SqlCommand
    Dim checkBooksCmd As SqlCommand
    Dim editUserCourtCmd As SqlCommand
    Dim loadUserCourtCmd As SqlCommand
    Dim loadIndCourtCmd As SqlCommand

    Dim payId As Integer

    Dim cardNumCheck As Regex = New Regex("\d{4}-\d{4}-\d{4}-\d{4}")
    Dim secNumCheck As Regex = New Regex("\d{4}")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim connStr As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\jpyea\source\repos\Final Project\Final Project\App_Data\badminton_database.mdf"";Integrated Security=True"
        conn = New SqlConnection(connStr)
        conn.Open()

        Dim loadCourtsSql As String = "SELECT DISTINCT school_id, school_name, school_tag
                                       FROM Court INNER JOIN Venues ON Venues.venue_id = court.school_id"
        loadCourtsCmd = New SqlCommand(loadCourtsSql, conn)

        Dim editPaymentSql As String = "UPDATE Payment SET card_bank = @cb, card_num = @cn, expire_date = @expda, security_num = @secnum, price_amount = @priamt
                                        WHERE payment_id = @pid"
        editPaymentCmd = New SqlCommand(editPaymentSql, conn)

        Dim editUserCourtSql As String = "UPDATE booking SET court_id = @cid, booking_date_start = @bkst, booking_date_end = @bken, booking_next_day = @bnd
                                          WHERE booking_id = @bid"
        editUserCourtCmd = New SqlCommand(editUserCourtSql, conn)

        Dim loadUserCourtSql As String = "SELECT booking.booking_id,venue_id,Court.court_id,school_name,school_tag,booking_date_start,booking_date_end,payment_date,card_bank,card_num,expire_date,security_num,price_amount,booking_next_day
                                          FROM Venues INNER JOIN Court ON Venues.venue_id = Court.school_id INNER JOIN booking ON Court.court_id = booking.court_id 
                                          INNER JOIN Payment ON Payment.payment_id = booking.payment_id INNER JOIN user_data ON booking.user_id = user_data.user_id
                                          WHERE booking.user_id = @uid"
        loadUserCourtCmd = New SqlCommand(loadUserCourtSql, conn)

        Dim loadIndCourtSql As String = "SELECT booking.booking_id,venue_id,Payment.payment_id,Court.court_id,school_name,school_tag,booking_date_start,booking_date_end,payment_date,card_bank,card_num,expire_date,security_num,price_amount,booking_next_day
                                        FROM Venues INNER JOIN Court ON Venues.venue_id = Court.school_id INNER JOIN booking ON Court.court_id = booking.court_id 
                                        INNER JOIN Payment ON Payment.payment_id = booking.payment_id INNER JOIN user_data ON booking.user_id = user_data.user_id
                                        WHERE booking.booking_id = @bid"
        loadIndCourtCmd = New SqlCommand(loadIndCourtSql, conn)

        Dim getCourtsSql As String = "SELECT venue_id, school_name,school_tag, court_id
                                      FROM Venues INNER JOIN Court ON Venues.venue_id = court.school_id
                                      WHERE school_tag = @schtag AND Court.status = 1"
        getCourtsCmd = New SqlCommand(getCourtsSql, conn)

        Dim getPaymentSql As String = "SELECT payment_id
                                       FROM Payment
                                       WHERE payment_id = @payid"
        getPaymentCmd = New SqlCommand(getPaymentSql, conn)

        Dim checkBooksSql As String = "SELECT booking_id,booking_date_start, booking_date_end 
                                       FROM booking
                                       WHERE court_id = @coid"
        checkBooksCmd = New SqlCommand(checkBooksSql, conn)


        loadUserCourtCmd.Parameters.Clear()
        loadUserCourtCmd.Parameters.AddWithValue("uid", PubVar.userId)

        If Not IsPostBack Then
            Dim adapter As SqlDataAdapter = New SqlDataAdapter(loadUserCourtCmd)
            Dim ds As DataSet = New DataSet()
            adapter.Fill(ds, "userData")

            Dim dt As DataTable = ds.Tables("userData")

            If dt.Rows.Count < 1 Then
                MsgBox("You have no bookings made, returning to main menu")
                Response.Redirect("main_page.aspx")
            Else
                drp_user_booking.Items.Clear()
                drp_user_booking.Items.Add("(Select)")
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim dr As DataRow = dt.Rows(i)
                    Dim bookId As Integer = dr("booking_id")
                    Dim SchoolName As String = dr("school_name")
                    drp_user_booking.Items.Add("Booking Id: " + bookId.ToString + " Venue: " + SchoolName)
                    drp_user_booking.Items.Item(i + 1).Value = bookId
                Next
            End If

            Dim adapter2 As SqlDataAdapter = New SqlDataAdapter(loadCourtsCmd)
            Dim ds2 As DataSet = New DataSet()
            adapter2.Fill(ds2, "LoadSchools")

            Dim dt2 As DataTable = ds2.Tables("LoadSchools")

            If dt2.Rows.Count < 1 Then
                MsgBox("Error, data failed to retrive")
            Else
                drp_school.Items.Clear()
                drp_school.Items.Add("(Select)")
                For i As Integer = 0 To dt2.Rows.Count - 1
                    Dim dr As DataRow = dt2.Rows(i)
                    Dim sc As String = dr("school_name")
                    drp_school.Items.Add(sc)
                    drp_school.Items.Item(i + 1).Value = dr("school_tag")
                Next
            End If
        End If
    End Sub



    Protected Sub btn_edit_Click(sender As Object, e As EventArgs) Handles btn_edit.Click
        Try
            Dim nextDay As Integer = 0
            payId = lbl_results.Text
            Dim nowDate As DateTime = Date.Now()
            Dim booking_date As String = cal_booking_date.SelectedDate.Date.ToString("dd/MM/yyyy")
            Dim expire_date As String = cal_expire_date.SelectedDate.Date.ToString("dd/MM/yyyy")
            If booking_date < Date.Now.ToString("dd/MM/yyyy") Then
                MsgBox("The date you inputted is behind the current date.")
                Exit Sub
            End If
            If booking_date > DateTime.Parse(Date.Now.AddMonths(3).ToString("dd/MM/yyyy")) Then
                MsgBox("The date you entered is exceeded the limits of booking(3 months)")
                Exit Sub
            End If
            Dim expireDate As DateTime = DateTime.Parse(expire_date)
            If expireDate < Date.Now() Then
                MsgBox("Your card has expired, please try again")
                Exit Sub
            End If
            Dim startDate As DateTime = DateTime.Parse(booking_date + " " + start_time_hr.SelectedValue + ":00 " + start_time_ampm.SelectedValue)
            Dim endDate As DateTime = DateTime.Parse(booking_date + " " + end_time_hr.SelectedValue + ":00 " + end_time_ampm.SelectedValue)

            If chk_nextDay.Checked = True Then
                endDate = endDate.AddDays(1)
                nextDay = 1
            End If



            If (DateDiff("h", startDate, endDate) > 3) Then
                MsgBox("The maximum allocated time is 3 hours")
                Exit Sub
            End If

            If (DateDiff("h", startDate, endDate) < 0) Then
                MsgBox("The end date must be later than the start date")
                Exit Sub
            End If

            If Not (cardNumCheck.IsMatch(txt_cardNum.Text)) Then
                MsgBox("Please follow the instructions specified(Card Numbers)")
                Exit Sub
            End If

            If Not (secNumCheck.IsMatch(txt_security.Text)) Then
                MsgBox("Please follow the instructions specified(Security numbers)")
                Exit Sub
            End If

            If txt_donate.Text < 5 Then
                MsgBox("Please follow the instructions specified(Payment)")
                Exit Sub
            End If

            checkBooksCmd.Parameters.Clear()
            checkBooksCmd.Parameters.AddWithValue("coid", drp_court.SelectedValue)

            Dim adapter As SqlDataAdapter = New SqlDataAdapter(checkBooksCmd)
            Dim ds As DataSet = New DataSet()
            adapter.Fill(ds, "tableCheck")

            Dim dt As DataTable = ds.Tables("tableCheck")

            If dt.Rows.Count < 1 Then

            Else
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim dr As DataRow = dt.Rows(i)
                    MsgBox(DateDiff("h", dr("booking_date_start"), startDate))
                    MsgBox(DateDiff("h", dr("booking_date_end"), endDate))
                    If (startDate >= dr("booking_date_start") And endDate <= dr("booking_date_end")) OrElse
                       ((startDate >= dr("booking_date_start") And startDate <= dr("booking_date_end")) And DateDiff("h", dr("booking_date_end"), endDate) >= 0) OrElse
                       ((endDate <= dr("booking_date_end") And endDate >= dr("booking_date_start")) And DateDiff("h", startDate, dr("booking_date_start")) <= 0) OrElse
                       ((DateDiff("h", dr("booking_date_end"), endDate) >= 1) And DateDiff("h", dr("booking_date_start"), startDate) >= 1) Then
                        MsgBox("Booking collision detected. Please try again")
                        Exit Sub
                    End If
                Next
            End If

            editPaymentCmd.Parameters.Clear()
            editPaymentCmd.Parameters.AddWithValue("pid", payId)
            editPaymentCmd.Parameters.AddWithValue("cb", drp_card_type.SelectedValue)
            editPaymentCmd.Parameters.AddWithValue("cn", txt_cardNum.Text)
            editPaymentCmd.Parameters.AddWithValue("expda", cal_expire_date.SelectedDate.Date)
            editPaymentCmd.Parameters.AddWithValue("secnum", txt_security.Text)
            editPaymentCmd.Parameters.AddWithValue("priamt", txt_donate.Text)

            Dim rowsAffected As Integer = editPaymentCmd.ExecuteNonQuery()

            If rowsAffected < 1 Then
                MsgBox("Error, it failed to upload to the system")
            Else
                getPaymentCmd.Parameters.Clear()
                getPaymentCmd.Parameters.AddWithValue("payid", payId)
                Dim adapter2 As SqlDataAdapter = New SqlDataAdapter(getPaymentCmd)
                Dim ds2 As DataSet = New DataSet
                adapter2.Fill(ds2, "GetPayment")

                Dim dt2 As DataTable = ds2.Tables("GetPayment")

                If dt2.Rows.Count < 1 Then
                    MsgBox("Error, dt2 doesn't work")
                Else
                    Dim dr As DataRow = dt2.Rows(0)
                    editUserCourtCmd.Parameters.Clear()
                    editUserCourtCmd.Parameters.AddWithValue("bid", drp_user_booking.SelectedValue)
                    editUserCourtCmd.Parameters.AddWithValue("cid", drp_court.SelectedValue)
                    editUserCourtCmd.Parameters.AddWithValue("bkst", startDate)
                    editUserCourtCmd.Parameters.AddWithValue("bken", endDate)
                    editUserCourtCmd.Parameters.AddWithValue("bnd", nextDay)

                    Dim rowsAffected2 As Integer = editUserCourtCmd.ExecuteNonQuery()

                    If rowsAffected2 < 1 Then
                        MsgBox("Somethings wrond with rowsAffected2")
                    Else
                        MsgBox("Success, returing to main menu")
                        Response.Redirect("main_page.aspx")
                    End If
                End If

                editUserCourtCmd.Parameters.Clear()
            End If

        Catch ex As Exception
            MsgBox("Error, error message: " + ex.ToString)
        End Try
    End Sub

    Protected Sub btn_return_Click(sender As Object, e As EventArgs) Handles btn_return.Click
        Response.Redirect("main_page.aspx")
    End Sub

    Protected Sub drp_user_booking_SelectedIndexChanged(sender As Object, e As EventArgs) Handles drp_user_booking.SelectedIndexChanged
        Dim bookId As Integer = Integer.Parse(drp_user_booking.SelectedValue)
        loadIndCourtCmd.Parameters.Clear()
        loadIndCourtCmd.Parameters.AddWithValue("bid", bookId)

        Dim adapter As SqlDataAdapter = New SqlDataAdapter(loadIndCourtCmd)
        Dim ds As DataSet = New DataSet()
        adapter.Fill(ds, "userData")

        Dim dt As DataTable = ds.Tables("userData")

        If dt.Rows.Count < 1 Then
            MsgBox("Error occured, please try again")
        Else
            Dim dr As DataRow = dt.Rows(0)
            Dim dateStart As DateTime = dr("booking_date_start")
            Dim dateEnd As DateTime = dr("booking_date_end")
            Dim dateExpire As DateTime = dr("expire_date")

            cal_expire_date.SelectedDate = dateExpire

            Dim dpSch As String = dr("school_tag")
            payId = dr("payment_id")
            lbl_results.Text = payId

            drp_school.Text = dpSch
            drp_school_SelectedIndexChanged(drp_school, e)
            drp_court.Text = dr("court_id")

            cal_booking_date.SelectedDate = DateTime.Parse(dateStart.ToString("dd/MM/yyyy"))
            start_time_hr.Text = dateStart.ToString("hh")
            start_time_ampm.Text = dateStart.ToString("tt")

            end_time_ampm.Text = dateEnd.ToString("tt")
            end_time_hr.Text = dateEnd.ToString("hh")

            If dr("booking_next_day") = 1 Then
                chk_nextDay.Checked = True
            End If
            drp_card_type.Text = dr("card_bank")
            txt_cardNum.Text = dr("card_num")
            txt_security.Text = dr("security_num")
            txt_donate.Text = dr("price_amount")
        End If
    End Sub


    Protected Sub drp_school_SelectedIndexChanged(sender As Object, e As EventArgs) Handles drp_school.SelectedIndexChanged
        Dim value As String = drp_school.SelectedValue
        getCourtsCmd.Parameters.Clear()
        getCourtsCmd.Parameters.AddWithValue("schtag", value)

        Dim adapter As SqlDataAdapter = New SqlDataAdapter(getCourtsCmd)
        Dim ds As DataSet = New DataSet()
        adapter.Fill(ds, "courtsTable")

        Dim dt As DataTable = ds.Tables("courtsTable")

        If dt.Rows.Count < 1 Then
            MsgBox("All courts may be unavailable")
        Else
            drp_court.Items.Clear()
            drp_court.Items.Add("(Select)")
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dr As DataRow = dt.Rows(i)
                Dim courtId As Integer = dr("court_id")
                Dim status As String = (i + 1).ToString
                drp_court.Items.Add(status + " (" + courtId.ToString + ")")
                drp_court.Items.Item(1 + i).Value = courtId
            Next
        End If
    End Sub
End Class