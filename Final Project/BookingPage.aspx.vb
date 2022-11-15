Imports System.Data.SqlClient

Public Module BookVar
    Public timeStart As String = ""
    Public timeEnd As String = ""
End Module


Public Class BookingPage
    Inherits System.Web.UI.Page
    Dim conn As SqlConnection
    Dim loadCourtsCmd As SqlCommand
    Dim loadNameCmd As SqlCommand
    Dim getCourtsCmd As SqlCommand
    Dim getTimeCmd As SqlCommand
    Dim checkUserCmd As SqlCommand
    Dim checkBooksCmd As SqlCommand
    Dim insertPaymentCmd As SqlCommand
    Dim takePayIdCmd As SqlCommand
    Dim insertBooksCmd As SqlCommand
    Dim getBooksCmd As SqlCommand

    Dim cardNumCheck As Regex = New Regex("\d{4}-\d{4}-\d{4}-\d{4}")
    Dim secNumCheck As Regex = New Regex("\d{4}")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("UserID") Is Nothing Then
            Server.Transfer("Starting_Page.aspx")
        End If
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

        Dim getCourtsSql As String = "SELECT venue_id, school_name,school_tag, court_id, status
                                      FROM Venues INNER JOIN Court ON Venues.venue_id = court.school_id
                                      WHERE school_tag = @schtag AND Court.status = 1"
        getCourtsCmd = New SqlCommand(getCourtsSql, conn)

        Dim checkUserSql As String = "SELECT booking_id, booking.user_id, school_name, booking.court_id, booking_date_start, booking_date_end, school_address
                                      FROM booking INNER JOIN Court ON Court.court_id = booking.court_id INNER JOIN user_data ON booking.user_id = user_data.user_id 
                                      INNER JOIN Venues ON Venues.venue_id = Court.school_id INNER JOIN Locations ON Locations.location_id = Venues.school_location"

        Dim getTimeSql As String = "SELECT time_open, time_close FROM Court WHERE court_id = @cid"
        getTimeCmd = New SqlCommand(getTimeSql, conn)

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

        Dim getBooksSql As String = "SELECT booking_id, booking_date_start,booking_date_end, username
                                     FROM booking INNER JOIN user_data ON (user_data.user_id = booking.user_id)
                                     WHERE court_id = @coid"
        getBooksCmd = New SqlCommand(getBooksSql, conn)


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
            MsgBox("All courts may be under maintance")
        Else
            drp_court.Items.Clear()
            drp_court.Items.Add("(Select)")
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dr As DataRow = dt.Rows(i)
                If dr("status") = True Then
                    Dim courtId As Integer = dr("court_id")
                    drp_court.Items.Add(i + 1)
                    drp_court.Items.Item(i + 1).Value = courtId
                End If
            Next
        End If

    End Sub

    Protected Sub btn_book_Click(sender As Object, e As EventArgs) Handles btn_book.Click
        Try
            Dim nextDay As Integer = 0
            Dim nowDate As DateTime = Date.Now()
            Dim booking_date As String = cal_booking_date.SelectedDate.Date.ToString("dd/MM/yyyy")
            Dim expire_date As String = cal_expire_date.SelectedDate.Date.ToString("dd/MM/yyyy")
            If DateTime.Parse(booking_date) < Date.Now Then
                MsgBox("The date you inputted is behind the current date.")
                Exit Sub
            End If
            If DateTime.Parse(booking_date) > Date.Now.AddMonths(3) Then
                MsgBox("The date you entered is exceeded the limits of booking(3 months)")
            End If
            Dim expireDate As DateTime = DateTime.Parse(expire_date)
            If expireDate < Date.Now() Then
                MsgBox("Your card has expired, please try again")
                Exit Sub
            End If
            Dim startDate As DateTime = DateTime.Parse(booking_date + " " + start_time_hr.SelectedValue + ":00 " + start_time_ampm.SelectedValue)
            Dim endDate As DateTime = DateTime.Parse(booking_date + " " + end_time_hr.SelectedValue + ":00" + end_time_ampm.SelectedValue)

            If Not blankCheck() Then
                MsgBox("The School/court is not selected")
                Exit Sub
            End If
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

            Dim timeStart As DateTime = DateTime.Parse(booking_date + " " + BookVar.timeStart)
            Dim timeEnd As DateTime = DateTime.Parse(booking_date + " " + BookVar.timeEnd)

            If Not (DateDiff("h", timeStart, startDate) >= 0 And DateDiff("h", timeEnd, endDate) <= 0) Then
                MsgBox("Error, the time you submitted has exceeded the time the court is available")
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
                       ((startDate >= dr("booking_date_start") And startDate <= dr("booking_date_end")) And DateDiff("h", dr("booking_date_end"), endDate) >= 0) OrElse
                       ((endDate <= dr("booking_date_end") And endDate >= dr("booking_date_start")) And DateDiff("h", startDate, dr("booking_date_start")) <= 0) OrElse
                       ((DateDiff("h", dr("booking_date_end"), endDate) >= 0) And DateDiff("h", dr("booking_date_start"), startDate) <= 0) Then
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
                insertBooksCmd.Parameters.AddWithValue("uid", Session("UserID"))
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
                    MsgBox("Booking successful, remember to obtain the copy")
                    Server.Transfer("main_page.aspx")
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
                Dim status As String = (i + 1).ToString
                drp_court.Items.Add(status + " (" + courtId.ToString + ")")
                drp_court.Items.Item(1 + i).Value = courtId
            Next
        End If
        Panel1.Visible = True
    End Sub

    Protected Sub btn_return_Click(sender As Object, e As EventArgs) Handles btn_return.Click
        tags = "None"
        Server.Transfer("main_page.aspx")
    End Sub

    Protected Sub drp_court_SelectedIndexChanged(sender As Object, e As EventArgs) Handles drp_court.SelectedIndexChanged
        Dim value As Integer = drp_court.SelectedValue
        getTimeCmd.Parameters.Clear()
        getTimeCmd.Parameters.AddWithValue("cid", value)

        Dim adap As SqlDataAdapter = New SqlDataAdapter(getTimeCmd)
        Dim ds As DataSet = New DataSet
        adap.Fill(ds, "time")
        Dim dt As DataTable = ds.Tables("time")

        If dt.Rows.Count < 1 Then
            MsgBox("Error when fetching data")
        Else
            Dim dr As DataRow = dt.Rows(0)
            Dim timeStart As String = dr("time_open").ToString
            Dim timeEnd As String = dr("time_close").ToString
            BookVar.timeStart = timeStart
            BookVar.timeEnd = timeEnd
            Dim strJoin = timeStart + " - " + timeEnd
            lbl_courtTime.Text = strJoin
        End If

        getBooksCmd.Parameters.Clear()
        getBooksCmd.Parameters.AddWithValue("coid", value)
        Dim adap2 As SqlDataAdapter = New SqlDataAdapter(getBooksCmd)
        Dim ds2 As DataSet = New DataSet
        adap2.Fill(ds2, "books")
        Dim dt2 As DataTable = ds2.Tables("books")

        If dt2.Rows.Count > 0 Then
            tbl_books.Visible = True
            For i As Integer = 0 To dt2.Rows.Count - 1
                Dim dr As DataRow = dt2.Rows(i)
                Dim row As TableRow = New TableRow
                Dim bookId As String = dr("booking_id").ToString
                Dim bookStart As String = dr("booking_date_start").ToString
                Dim bookEnd As String = dr("booking_date_end").ToString
                Dim cellId As TableCell = New TableCell()
                cellId.Text = bookId
                Dim cellStart As TableCell = New TableCell
                cellStart.Text = bookStart
                Dim cellEnd As TableCell = New TableCell
                cellEnd.Text = bookEnd
                row.Cells.Add(cellId)
                row.Cells.Add(cellStart)
                row.Cells.Add(cellEnd)
                tbl_books.Rows.Add(row)
            Next
        End If
    End Sub

    Protected Function blankCheck()
        If drp_court.SelectedIndex = 0 Or drp_school.SelectedIndex = 0 Then
            Return False
        End If
        Return True
    End Function

End Class