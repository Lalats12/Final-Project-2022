Imports System.Data.SqlClient

Public Module adminBook
    Public timeStart As String = ""
    Public timeEnd As String = ""
End Module

Public Class AdminBooking
    Inherits System.Web.UI.Page
    Dim conn As SqlConnection
    Dim loadBookingsCmd As SqlCommand
    Dim loadOneBookCmd As SqlCommand
    Dim loadSchoolsCmd As SqlCommand
    Dim loadCourtsCmd As SqlCommand
    Dim courtSearchCmd As SqlCommand
    Dim checkBooksCmd As SqlCommand
    Dim getTimeCmd As SqlCommand
    Dim updateBookingCmd As SqlCommand
    Dim updatePaymentCmd As SqlCommand
    Dim deleteBookingCmd As SqlCommand
    Dim deletePaymentCmd As SqlCommand
    Dim getBooksCmd As SqlCommand

    Dim cardNumCheck As Regex = New Regex("\d{4}-\d{4}-\d{4}-\d{4}")
    Dim secNumCheck As Regex = New Regex("\d{4}")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("IsAdmin") Is Nothing Then
            Server.Transfer("../Starting_Page.aspx")
        End If
        Dim connStr As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\jpyea\source\repos\Final Project\Final Project\App_Data\badminton_database.mdf"";Integrated Security=True"
        conn = New SqlConnection(connStr)
        conn.Open()

        Dim loadBookingsSql As String = "SELECT *
                                         FROM booking"
        loadBookingsCmd = New SqlCommand(loadBookingsSql, conn)

        Dim loadOneBookSql As String = "SELECT * 
                                        FROM booking INNER JOIN Payment ON Payment.payment_id = booking.payment_id 
                                        INNER JOIN Court ON Court.court_Id = booking.court_id INNER JOIN Venues ON Court.school_id = Venues.venue_id
                                        INNER JOIN Locations ON Locations.location_id = Venues.school_location 
                                        INNER JOIN user_data ON user_data.user_id = booking.user_id
                                        WHERE booking_id = @bid"
        loadOneBookCmd = New SqlCommand(loadOneBookSql, conn)

        Dim loadSchoolsSql As String = "SELECT school_name, venue_id, school_tag
                                        FROM Venues"
        loadSchoolsCmd = New SqlCommand(loadSchoolsSql, conn)

        Dim loadCourtsSql As String = "SELECT court_id, status
                                       FROM Court INNER JOIN Venues ON Court.school_id = Venues.venue_id
                                       WHERE school_tag = @tag AND Court.status = 1"
        loadCourtsCmd = New SqlCommand(loadCourtsSql, conn)

        Dim courtSearchSql As String = "SELECT *
                                        FROM booking
                                        WHERE court_id = @id"
        courtSearchCmd = New SqlCommand(courtSearchSql, conn)

        Dim checkBooksSql As String = "SELECT booking_id,booking_date_start, booking_date_end 
                                       FROM booking
                                       WHERE court_id = @coid"
        checkBooksCmd = New SqlCommand(checkBooksSql, conn)

        Dim updateBookingSql As String = "UPDATE booking SET court_id = @cid, booking_date_start = @bds, 
                                                             booking_date_end = @bde, booking_next_day = @bnd
                                          WHERE booking_id = @bid"
        updateBookingCmd = New SqlCommand(updateBookingSql, conn)

        Dim getTimeSql As String = "SELECT time_open, time_close FROM Court WHERE court_id = @cid"
        getTimeCmd = New SqlCommand(getTimeSql, conn)

        Dim updatePaymentSql As String = "UPDATE Payment SET card_bank = @cb, card_num = @cn, expire_date = @ed, security_num = @sec, price_amount = @pd
                                          WHERE payment_id = @pid"
        updatePaymentCmd = New SqlCommand(updatePaymentSql, conn)

        Dim deleteBookingSql As String = "DELETE booking WHERE booking_id = @bid"
        deleteBookingCmd = New SqlCommand(deleteBookingSql, conn)

        Dim deletePaymentSql As String = "DELETE Payment WHERE payment_id = @pid"
        deletePaymentCmd = New SqlCommand(deletePaymentSql, conn)

        Dim getBooksSql As String = "SELECT booking_id, booking_date_start,booking_date_end, username
                                     FROM booking INNER JOIN user_data ON (user_data.user_id = booking.user_id)
                                     WHERE court_id = @coid"
        getBooksCmd = New SqlCommand(getBooksSql, conn)

        Dim schoolAdap As SqlDataAdapter = New SqlDataAdapter(loadSchoolsCmd)
        Dim schDS As DataSet = New DataSet
        schoolAdap.Fill(schDS, "schools")
        Dim schDT As DataTable = schDS.Tables("schools")

        If Not IsPostBack Then
            If schDT.Rows.Count < 1 Then
                MsgBox("Error, theres something's wrong on schDT")
            Else
                drp_schools.Items.Clear()
                drp_schools.Items.Add("(select)")
                For i As Integer = 0 To schDT.Rows.Count - 1
                    Dim dr As DataRow = schDT.Rows(i)
                    Dim schName As String = dr("school_name")
                    Dim schTag As String = dr("school_tag")
                    Dim schId As Integer = dr("venue_id")
                    Dim strJoin As String = schName + " (" + schId.ToString + ")"
                    drp_schools.Items.Add(strJoin)
                    drp_schools.Items.Item(i + 1).Value() = schTag
                Next
            End If
        End If
    End Sub

    Protected Sub btn_search_Click(sender As Object, e As EventArgs) Handles btn_search.Click
        loadOneBookCmd.Parameters.Clear()
        loadOneBookCmd.Parameters.AddWithValue("bid", txt_searchID.Text)

        Dim oneAdap As SqlDataAdapter = New SqlDataAdapter(loadOneBookCmd)
        Dim oneDS As DataSet = New DataSet()
        oneAdap.Fill(oneDS, "booking")
        Dim oneDT As DataTable = oneDS.Tables("booking")

        If oneDT.Rows.Count < 1 Then
            MsgBox("Error detected at oneDT. Please try again")
        Else
            Dim dr As DataRow = oneDT.Rows(0)
            Dim bookingId As Integer = dr("booking_id")
            Dim schTag As String = dr("school_tag")
            Dim userId As Integer = dr("user_id")
            Dim courtId As Integer = dr("court_id")
            Dim startDate As DateTime = dr("booking_date_start")
            Dim endDate As DateTime = dr("booking_date_end")
            Dim nextDay As Boolean = dr("booking_next_day")
            Dim payDay As DateTime = dr("payment_date")
            Dim payId As Integer = dr("payment_id")
            Dim bankCard As String = dr("card_bank")
            Dim numCard As String = dr("card_num")
            Dim expireDate As DateTime = dr("expire_date")
            Dim secNum As String = dr("security_num")
            Dim priceAmt As String = dr("price_amount")

            AdminVars.bookingId = bookingId
            txt_userID.Text = userId
            drp_schools.Text = schTag
            selectCourt()
            drp_courts.Text = courtId
            cal_Booking_date.SelectedDate = DateTime.Parse(startDate.ToString("dd/MM/yyyy"))
            start_date_hr.Text = startDate.ToString("hh")
            start_date_ampm.Text = startDate.ToString("tt")

            end_date_hr.Text = endDate.ToString("hh")
            end_date_ampm.Text = endDate.ToString("tt")

            If nextDay Then
                chk_nextDay.Checked = True
            End If

            paymentId = payId
            txt_paymentId.Text = payId
            drp_card_type.Text = bankCard
            txt_cardNum.Text = numCard
            cal_expire_date.SelectedDate = expireDate

            txt_security.Text = secNum
            txt_donate.Text = priceAmt

            Panel1.Visible = True
        End If
    End Sub

    Protected Sub btn_all_Click(sender As Object, e As EventArgs) Handles btn_all.Click
        Dim adapter As SqlDataAdapter = New SqlDataAdapter(loadBookingsCmd)
        Dim ds As DataSet = New DataSet
        adapter.Fill(ds, "bookings")
        Dim dt As DataTable = ds.Tables("bookings")

        If dt.Rows.Count < 1 Then
            MsgBox("Error while loading the bookings")
        Else
            lst_bookingIDs.Items.Clear()
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dr As DataRow = dt.Rows(i)
                Dim book As Integer = dr("booking_id")
                Dim user As Integer = dr("user_id")
                Dim booking_date_start As DateTime = dr("booking_date_start")

                Dim strJoin As String = "Booking id: " + book.ToString + " User ID: " + user.ToString + " Start date:" + booking_date_start.ToString()
                lst_bookingIDs.Items.Add(strJoin)
                lst_bookingIDs.Items.Item(i).Value = book
            Next
        End If


        pan_displayAll.Visible = True
    End Sub

    Protected Sub btn_selectSearch_Click(sender As Object, e As EventArgs) Handles btn_selectSearch.Click
        pan_search.Visible = True
    End Sub

    Protected Sub drp_schools_SelectedIndexChanged(sender As Object, e As EventArgs) Handles drp_schools.SelectedIndexChanged
        loadCourtsCmd.Parameters.Clear()
        loadCourtsCmd.Parameters.AddWithValue("tag", drp_schools.SelectedValue)

        Dim courtAdap As SqlDataAdapter = New SqlDataAdapter(loadCourtsCmd)
        Dim courtDS As DataSet = New DataSet
        courtAdap.Fill(courtDS, "courts")
        Dim courtDT As DataTable = courtDS.Tables("courts")

        If courtDT.Rows.Count < 1 Then
            MsgBox("Court may not exist")
            Exit Sub
        Else
            drp_courts.Items.Clear()
            drp_courts.Items.Add("(select)")
            For i As Integer = 0 To courtDT.Rows.Count - 1
                Dim dr As DataRow = courtDT.Rows(i)
                Dim courtId As Integer = dr("court_id")
                Dim strInt As String = i + 1
                drp_courts.Items.Add(strInt + " (" + courtId.ToString + ")")
                drp_courts.Items.Item(i + 1).Value = courtId
            Next
        End If
    End Sub

    Protected Sub selectCourt()
        loadCourtsCmd.Parameters.Clear()
        loadCourtsCmd.Parameters.AddWithValue("tag", drp_schools.SelectedValue)

        Dim courtAdap As SqlDataAdapter = New SqlDataAdapter(loadCourtsCmd)
        Dim courtDS As DataSet = New DataSet
        courtAdap.Fill(courtDS, "courts")
        Dim courtDT As DataTable = courtDS.Tables("courts")

        If courtDT.Rows.Count < 1 Then
            MsgBox("Error while loading CourtDT")
        Else
            drp_courts.Items.Clear()
            drp_courts.Items.Add("(select)")
            For i As Integer = 0 To courtDT.Rows.Count - 1
                Dim dr As DataRow = courtDT.Rows(i)
                Dim courtId As Integer = dr("court_id")
                Dim strInt As String = i + 1
                drp_courts.Items.Add(strInt + " (" + courtId.ToString + ")")
                drp_courts.Items.Item(i + 1).Value = courtId
            Next
        End If
    End Sub

    Protected Sub btn_searchCourts_Click(sender As Object, e As EventArgs) Handles btn_searchCourts.Click
        pnl_courts.Visible = True
    End Sub

    Protected Sub btn_delete_Click(sender As Object, e As EventArgs) Handles btn_delete.Click
        Dim userRe As Integer = MsgBox("Are you sure you want to delete?", vbQuestion + vbYesNo + vbDefaultButton2, "Confirmation")

        If userRe = vbNo Then
            Exit Sub
        End If

        deletePaymentCmd.Parameters.Clear()
        deletePaymentCmd.Parameters.AddWithValue("pid", paymentId)

        deleteBookingCmd.Parameters.Clear()
        deleteBookingCmd.Parameters.AddWithValue("bid", AdminVars.bookingId)

        Dim rowsAff As Integer = deleteBookingCmd.ExecuteNonQuery()

        If rowsAff < 1 Then
            MsgBox("error detected when deleting it")
        Else
            Dim rowsAff2 As Integer = deletePaymentCmd.ExecuteNonQuery()
            If rowsAff2 < 1 Then
                MsgBox("error detected when deleting it")
            Else
                MsgBox("Sucessful, closing")
                Server.Transfer("AdminBooking.aspx")
                Panel1.Visible = False
                pnl_courts.Visible = False
                pan_search.Visible = False
                pan_displayAll.Visible = False
            End If
        End If
    End Sub

    Protected Sub btn_research_Click(sender As Object, e As EventArgs) Handles btn_research.Click
        Panel1.Visible = False
    End Sub

    Protected Sub btn_update_Click(sender As Object, e As EventArgs) Handles btn_update.Click
        Dim nextDay As Integer = 0
        paymentId = Integer.Parse(txt_paymentId.Text)
        Dim nowDate As DateTime = Date.Now()
        Dim booking_date As String = cal_Booking_date.SelectedDate.Date.ToString("dd/MM/yyyy")
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
        Dim startDate As DateTime = DateTime.Parse(booking_date + " " + start_date_hr.SelectedValue + ":00 " + start_date_ampm.SelectedValue)
        Dim endDate As DateTime = DateTime.Parse(booking_date + " " + end_date_hr.SelectedValue + ":00 " + end_date_ampm.SelectedValue)

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

        Dim timeStart As DateTime = DateTime.Parse(booking_date + " " + editVar.timeStart)
        Dim timeEnd As DateTime = DateTime.Parse(booking_date + " " + editVar.timeEnd)

        If Not (DateDiff("h", timeStart, startDate) >= 0 And DateDiff("h", timeEnd, endDate) <= 0) Then
            MsgBox("Error, the time you submitted has exceeded the time the court is available")
            Exit Sub
        End If

        checkBooksCmd.Parameters.Clear()
        checkBooksCmd.Parameters.AddWithValue("coid", drp_courts.SelectedValue)

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
                   ((DateDiff("h", dr("booking_date_end"), endDate) >= 0) And DateDiff("h", dr("booking_date_start"), startDate) <= 0) Then
                    MsgBox("Booking collision detected. Please try again")
                    Exit Sub
                End If
            Next
        End If

        updateBookingCmd.Parameters.Clear()
        updateBookingCmd.Parameters.AddWithValue("bid", bookingId)
        updateBookingCmd.Parameters.AddWithValue("cid", drp_courts.SelectedValue)
        updateBookingCmd.Parameters.AddWithValue("bds", startDate)
        updateBookingCmd.Parameters.AddWithValue("bde", endDate)
        updateBookingCmd.Parameters.AddWithValue("bnd", nextDay)

        Dim rowsAff As Integer = updateBookingCmd.ExecuteNonQuery
        If rowsAff < 1 Then
            MsgBox("Error while updating the data")
        Else
            MsgBox("Success updating, Refreshing")
            pnl_courts.Visible = False
            pan_search.Visible = False
            pan_displayAll.Visible = False
            Server.Transfer("AdminBooking.aspx")
        End If
    End Sub

    Protected Sub btn_showContent_Click(sender As Object, e As EventArgs) Handles btn_showContent.Click
        Dim selectedThing As Integer = Integer.Parse(lst_bookingIDs.SelectedValue)
        loadOneBookCmd.Parameters.Clear()
        loadOneBookCmd.Parameters.AddWithValue("bid", selectedThing)

        Dim oneAdap As SqlDataAdapter = New SqlDataAdapter(loadOneBookCmd)
        Dim oneDS As DataSet = New DataSet()
        oneAdap.Fill(oneDS, "booking")
        Dim oneDT As DataTable = oneDS.Tables("booking")

        If oneDT.Rows.Count < 1 Then
            MsgBox("Error detected at oneDT. Please try again")
        Else
            Dim dr As DataRow = oneDT.Rows(0)
            Dim bookingId As Integer = dr("booking_id")
            Dim schTag As String = dr("school_tag")
            Dim userId As Integer = dr("user_id")
            Dim courtId As Integer = dr("court_id")
            Dim startDate As DateTime = dr("booking_date_start")
            Dim endDate As DateTime = dr("booking_date_end")
            Dim nextDay As Boolean = dr("booking_next_day")
            Dim payDay As DateTime = dr("payment_date")
            Dim payId As Integer = dr("payment_id")
            Dim bankCard As String = dr("card_bank")
            Dim numCard As String = dr("card_num")
            Dim expireDate As DateTime = dr("expire_date")
            Dim secNum As String = dr("security_num")
            Dim priceAmt As String = dr("price_amount")

            AdminVars.bookingId = bookingId
            txt_userID.Text = userId
            drp_schools.Text = schTag
            selectCourt()
            drp_courts.Text = courtId
            cal_Booking_date.SelectedDate = DateTime.Parse(startDate.ToString("dd/MM/yyyy"))
            start_date_hr.Text = startDate.ToString("hh")
            start_date_ampm.Text = startDate.ToString("tt")

            end_date_hr.Text = endDate.ToString("hh")
            end_date_ampm.Text = endDate.ToString("tt")

            If nextDay Then
                chk_nextDay.Checked = True
            End If

            paymentId = payId
            txt_paymentId.Text = payId.ToString
            drp_card_type.Text = bankCard
            txt_cardNum.Text = numCard
            cal_expire_date.SelectedDate = expireDate

            txt_security.Text = secNum
            txt_donate.Text = priceAmt

        End If
        Panel1.Visible = True
        pnl_courts.Visible = False
        pan_search.Visible = False
        pan_displayAll.Visible = False
    End Sub

    Protected Sub btn_searchCourt_Click(sender As Object, e As EventArgs) Handles btn_searchCourt.Click
        Dim court As String = txt_searchCourt.Text
        courtSearchCmd.Parameters.Clear()
        courtSearchCmd.Parameters.AddWithValue("id", Integer.Parse(court))

        Dim adap As SqlDataAdapter = New SqlDataAdapter(courtSearchCmd)
        Dim ds As DataSet = New DataSet
        adap.Fill(ds, "list")
        Dim dt As DataTable = ds.Tables("list")

        If dt.Rows.Count < 1 Then
            MsgBox("Error, either empty or Not working")
        Else
            lst_bookingCourts.Items.Clear()
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dr As DataRow = dt.Rows(i)
                Dim bookId As Integer = dr("booking_id")
                Dim userId As Integer = dr("user_id")
                Dim courtId As Integer = dr("court_id")
                Dim strJoin As String = "Booking ID: " + bookId.ToString + " User ID: " + userId.ToString + " Court ID: " + courtId.ToString
                lst_bookingCourts.Items.Add(strJoin)
                lst_bookingCourts.Items.Item(i).Value = bookId
            Next
        End If

    End Sub

    Protected Sub btn_courtGetBook_Click(sender As Object, e As EventArgs) Handles btn_courtGetBook.Click
        Try
            Dim booking As Integer = Integer.Parse(lst_bookingCourts.SelectedValue)
            loadOneBookCmd.Parameters.Clear()
            loadOneBookCmd.Parameters.AddWithValue("bid", booking)

            Dim oneAdap As SqlDataAdapter = New SqlDataAdapter(loadOneBookCmd)
            Dim oneDS As DataSet = New DataSet()
            oneAdap.Fill(oneDS, "booking")
            Dim oneDT As DataTable = oneDS.Tables("booking")

            If oneDT.Rows.Count < 1 Then
                MsgBox("Error detected at oneDT. Please try again")
            Else
                Dim dr As DataRow = oneDT.Rows(0)
                Dim bookingId As Integer = dr("booking_id")
                Dim schTag As String = dr("school_tag")
                Dim userId As Integer = dr("user_id")
                Dim courtId As Integer = dr("court_id")
                Dim startDate As DateTime = dr("booking_date_start")
                Dim endDate As DateTime = dr("booking_date_end")
                Dim nextDay As Boolean = dr("booking_next_day")
                Dim payDay As DateTime = dr("payment_date")
                Dim payId As Integer = dr("payment_id")
                Dim bankCard As String = dr("card_bank")
                Dim numCard As String = dr("card_num")
                Dim expireDate As DateTime = dr("expire_date")
                Dim secNum As String = dr("security_num")
                Dim priceAmt As String = dr("price_amount")

                AdminVars.bookingId = bookingId
                txt_userID.Text = userId
                drp_schools.Text = schTag
                selectCourt()
                drp_courts.Text = courtId
                cal_Booking_date.SelectedDate = DateTime.Parse(startDate.ToString("dd/MM/yyyy"))
                start_date_hr.Text = startDate.ToString("hh")
                start_date_ampm.Text = startDate.ToString("tt")

                end_date_hr.Text = endDate.ToString("hh")
                end_date_ampm.Text = endDate.ToString("tt")

                If nextDay Then
                    chk_nextDay.Checked = True
                End If

                paymentId = payId
                txt_paymentId.Text = payId.ToString
                drp_card_type.Text = bankCard
                txt_cardNum.Text = numCard
                cal_expire_date.SelectedDate = expireDate

                txt_security.Text = secNum
                txt_donate.Text = priceAmt

            End If
            Panel1.Visible = True
            pnl_courts.Visible = False
            pan_search.Visible = False
            pan_displayAll.Visible = False
        Catch ex As Exception
            MsgBox("Error, unknown error")
        End Try
    End Sub

    Protected Sub drp_courts_SelectedIndexChanged(sender As Object, e As EventArgs) Handles drp_courts.SelectedIndexChanged
        Dim value As Integer = drp_courts.SelectedValue
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
            adminBook.timeStart = timeStart
            adminBook.timeEnd = timeEnd
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

    Protected Sub btn_main_menu_Click(sender As Object, e As EventArgs) Handles btn_main_menu.Click
        Server.Transfer("../Admin_page.aspx")
    End Sub
End Class