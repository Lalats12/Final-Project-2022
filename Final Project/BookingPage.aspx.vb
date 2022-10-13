Imports System.Data.SqlClient

Public Class BookingPage
    Inherits System.Web.UI.Page
    Dim conn As SqlConnection
    Dim loadCourtsCmd As SqlCommand
    Dim loadNameCmd As SqlCommand
    Dim getCourtsCmd As SqlCommand
    Dim checkUserCmd As SqlCommand
    Dim checkBooksCmd As SqlCommand
    Dim insertPaymentCmd As SqlCommand
    Dim takePayIdCmd As SqlCommand
    Dim insertBooksCmd As SqlCommand

    Dim cardNumCheck As Regex = New Regex("\d{4}-\d{4}-\d{4}-\d{4}")
    Dim secNumCheck As Regex = New Regex("\d{4}")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim connStr As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\jpyea\source\repos\Final Project\Final Project\App_Data\badminton_database.mdf"";Integrated Security=True"
        conn = New SqlConnection(connStr)
        conn.Open()

        Dim loadCourtsSql As String = "SELECT DISTINCT school_id, school_name, school_tag
                                       FROM Court INNER JOIN Venues ON Venues.venue_id = court.school_id"
        loadCourtsCmd = New SqlCommand(loadCourtsSql, conn)

        Dim loadNameSql As String = "SELECT school_name
                                      FROM Venues
                                      WHERE school_tag = @stag"
        loadNameCmd = New SqlCommand(loadNameSql, conn)

        Dim getCourtsSql As String = "SELECT venue_id, school_name,school_tag, court_id
                                      FROM Venues INNER JOIN Court ON Venues.venue_id = court.school_id
                                      WHERE school_tag = @schtag"
        getCourtsCmd = New SqlCommand(getCourtsSql, conn)

        Dim checkUserSql As String = "SELECT booking_id, booking.user_id, school_name, booking.court_id, booking_date_start, booking_date_end, school_address
                                      FROM booking INNER JOIN Court ON Court.court_id = booking.court_id INNER JOIN user_data ON booking.user_id = user_data.user_id 
                                      INNER JOIN Venues ON Venues.venue_id = Court.school_id INNER JOIN Locations ON Locations.location_id = Venues.school_location"

        Dim checkBooksSql As String = "SELECT booking_id,booking_date_start, booking_date_end 
                                       FROM booking
                                       WHERE court_id = @coid"

        checkBooksCmd = New SqlCommand(checkBooksSql, conn)

        Dim insertPaymentSql As String = "INSERT INTO Payment(card_bank,card_num, expire_date,payment_date,security_num, price_amount)
                                          VALUES(@cb,@cn,@expda,@paydate,@secnum,@price)"
        insertPaymentCmd = New SqlCommand(insertPaymentSql, conn)

        Dim takePayIdSql As String = "SELECT payment_id
                                      FROM Payment
                                      WHERE payment_date = @pid"
        takePayIdCmd = New SqlCommand(takePayIdSql, conn)

        Dim insertBooksSql As String = "INSERT INTO booking(user_id,court_id,booking_date_start,booking_date_end,booking_next_day,payment_id)
                                        VALUES(@uid, @cid, @start,@end,@next,@payId)"

        insertBooksCmd = New SqlCommand(insertBooksSql, conn)


        If Not IsPostBack Then
            Dim adapter As SqlDataAdapter = New SqlDataAdapter(loadCourtsCmd)
            Dim ds As DataSet = New DataSet()
            adapter.Fill(ds, "LoadSchools")

            Dim dt As DataTable = ds.Tables("LoadSchools")

            If dt.Rows.Count < 1 Then
                MsgBox("Error, data failed to retrive")
            Else
                drp_school.Items.Clear()
                drp_school.Items.Add("(Select)")
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim dr As DataRow = dt.Rows(i)
                    drp_school.Items.Add(dr("school_name"))
                    drp_school.Items.Item(i + 1).Value = dr("school_tag")
                Next
            End If
            If Not tags.Equals("None") Then
                loadNameCmd.Parameters.Clear()
                loadNameCmd.Parameters.AddWithValue("stag", tags)

                Dim adapter2 As SqlDataAdapter = New SqlDataAdapter(loadNameCmd)
                Dim ds2 As DataSet = New DataSet()
                adapter2.Fill(ds2, "LoadName")

                Dim dt2 As DataTable = ds2.Tables("LoadName")

                If dt2.Rows.Count < 1 Then
                    MsgBox("Error, something went wrong")
                Else
                    Dim dr As DataRow = dt2.Rows(0)
                    lbl_choose.Text = "You choose: " + dr("school_name")
                    lbl_choose.Visible = True
                End If
                drp_school.Text = drp_school.Items.FindByValue(tags).Text
                drp_school.Visible = False
                OnLoad_mc()
            End If
            cal_booking_date.SelectedDate = Date.Now()
            cal_expire_date.SelectedDate = Date.Now()
        End If
    End Sub

    Protected Sub OnLoad_mc()
        Dim value As String = drp_school.SelectedValue
        getCourtsCmd.Parameters.Clear()
        getCourtsCmd.Parameters.AddWithValue("schtag", value)

        Dim adapter As SqlDataAdapter = New SqlDataAdapter(getCourtsCmd)
        Dim ds As DataSet = New DataSet()
        adapter.Fill(ds, "courtsTable")

        Dim dt As DataTable = ds.Tables("courtsTable")

        If dt.Rows.Count < 1 Then
            MsgBox("Error occured, please try again")
        Else
            drp_court.Items.Clear()
            drp_court.Items.Add("(Select)")
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dr As DataRow = dt.Rows(i)
                Dim courtId As Integer = dr("court_id")
                drp_court.Items.Add(i + 1)
                drp_court.Items.Item(i + 1).Value = courtId
            Next
        End If

    End Sub

    Protected Sub btn_book_Click(sender As Object, e As EventArgs) Handles btn_book.Click
        Try
            Dim nextDay As Integer = 0
            Dim nowDate As DateTime = Date.Now()
            Dim booking_date As String = cal_booking_date.SelectedDate.Date.ToString("dd/MM/yyyy")
            Dim expire_date As String = cal_expire_date.SelectedDate.Date.ToString("dd/MM/yyyy")
            If booking_date < Date.Now.ToString("dd/MM/yyyy") Then
                MsgBox("The date you inputted is behind the current date.")
            End If
            Dim expireDate As DateTime = DateTime.Parse(expire_date)
            Dim startDate As DateTime = DateTime.Parse(booking_date + " " + start_time_hr.SelectedValue + ":" +
            start_time_min.SelectedValue + " " + start_time_ampm.SelectedValue)
            Dim endDate As DateTime = DateTime.Parse(cal_booking_date.SelectedDate.Date.ToString("dd/MM/yyyy") + " " + end_time_hr.SelectedValue + ":" +
            end_time_min.SelectedValue + " " + end_time_ampm.SelectedValue)

            If chk_nextDay.Checked Then
                endDate.AddDays(1)
                nextDay = 1
            End If

            If (DateDiff("n", startDate, endDate) > 180) Then
                MsgBox("The maximum allocated time is 3 hours")
                Exit Sub
            End If

            If (DateDiff("n", startDate, endDate) < 0) Then
                MsgBox("The end date must be later than the start date")
                Exit Sub
            End If

            If Not (cardNumCheck.IsMatch(txt_cardNum.Text)) Then
                MsgBox("Please follow the instructions specified")
                Exit Sub
            End If

            If Not (secNumCheck.IsMatch(txt_security.Text)) Then
                MsgBox("Please follow the instructions specified")
                Exit Sub
            End If

            If txt_donate.Text < 5 Then
                MsgBox("Please follow the instructions specified")
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
                    If (startDate >= dr("booking_date_start") And endDate <= dr("booking_date_end")) OrElse
                       (startDate >= dr("booking_date_start") And DateDiff("n", dr("booking_date_end"), endDate) >= 15) OrElse
                       (endDate <= dr("booking_date_end") And DateDiff("n", startDate, dr("booking_date_start")) <= -15) OrElse
                       ((DateDiff("n", dr("booking_date_end"), endDate) >= 15) And DateDiff("n", dr("booking_date_start"), startDate) <= -15) Then
                        MsgBox("Booking collision detected. Please try again")
                        Exit Sub
                    End If
                Next
            End If
            insertPaymentCmd.Parameters.Clear()
            insertPaymentCmd.Parameters.AddWithValue("cb", drp_card_type.SelectedValue)
            insertPaymentCmd.Parameters.AddWithValue("cn", txt_cardNum.Text)
            insertPaymentCmd.Parameters.AddWithValue("expda", expireDate)
            insertPaymentCmd.Parameters.AddWithValue("secnum", txt_security.Text)
            insertPaymentCmd.Parameters.AddWithValue("paydate", nowDate)
            insertPaymentCmd.Parameters.AddWithValue("price", txt_donate.Text)

            Dim rowsAffected As Integer = insertPaymentCmd.ExecuteNonQuery()

            If rowsAffected < 1 Then
                MsgBox("Error occured, something's wrong")
                Exit Sub
            End If

            takePayIdCmd.Parameters.Clear()
            takePayIdCmd.Parameters.AddWithValue("pid", nowDate)

            Dim adapter3 As SqlDataAdapter = New SqlDataAdapter(takePayIdCmd)
            Dim ds3 As DataSet = New DataSet()
            adapter3.Fill(ds3, "tableCheck")

            Dim dt3 As DataTable = ds3.Tables("tableCheck")

            If dt3.Rows.Count < 1 Then
                MsgBox("Error, the dt3 not working")
                Exit Sub
            Else
                Dim dr As DataRow = dt3.Rows(0)
                insertBooksCmd.Parameters.Clear()
                insertBooksCmd.Parameters.AddWithValue("uid", userId)
                insertBooksCmd.Parameters.AddWithValue("cid", drp_court.SelectedValue)
                insertBooksCmd.Parameters.AddWithValue("start", startDate)
                insertBooksCmd.Parameters.AddWithValue("end", endDate)
                insertBooksCmd.Parameters.AddWithValue("next", nextDay)
                insertBooksCmd.Parameters.AddWithValue("payId", dr("payment_id"))

                Dim rowsAffected2 As Integer = insertBooksCmd.ExecuteNonQuery()
                If rowsAffected2 < 1 Then
                    MsgBox("RowsAffected2 not working")
                    Exit Sub
                Else
                    MsgBox("Booking successful, returning to main page")
                    Response.Redirect("main_page.aspx")
                End If
            End If


        Catch ex As InvalidCastException
            MsgBox("Error detected, error: " + ex.ToString)
        End Try

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
            MsgBox("Error occured, please try again")
        Else
            drp_court.Items.Clear()
            drp_court.Items.Add("(Select)")
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dr As DataRow = dt.Rows(i)
                Dim courtId As Integer = dr("court_id")
                drp_court.Items.Add(i + 1)
                drp_court.Items.Item(i + 1).Value = courtId
            Next
        End If
    End Sub

    Protected Sub btn_return_Click(sender As Object, e As EventArgs) Handles btn_return.Click
        tags = "None"
        Response.Redirect("main_page.aspx")
    End Sub
End Class