Imports System.Data.SqlClient

Public Module GlobalVariables
    Public schoolTag As String
End Module

Public Class WebForm1
    Inherits System.Web.UI.Page

    Dim conn As SqlConnection
    Dim loadVentureCmd As SqlCommand
    Dim loadUserBookingCmd As SqlCommand
    Dim checkBookCmd As SqlCommand
    Dim checkCurrDayCmd As SqlCommand



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim connStr As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\jpyea\source\repos\Final Project\Final Project\App_Data\badminton_database.mdf"";Integrated Security=True;Connect Timeout=30"
        conn = New SqlConnection(connStr)
        conn.Open()

        Dim loadVentureSql As String = "SELECT * FROM Venues INNER JOIN Locations ON Locations.location_id = Venues.school_location"
        loadVentureCmd = New SqlCommand(loadVentureSql, conn)

        Dim loadUserBookingSql As String = "SELECT * 
                                            FROM booking INNER JOIN Court ON booking.court_id = Court.court_id INNER JOIN Venues ON
                                            Court.school_id = Venues.venue_id INNER JOIN Locations ON Locations.location_id = Venues.school_location
                                            INNER JOIN Payment ON payment.payment_id = booking.payment_id
                                            WHERE user_id = @uid"
        loadUserBookingCmd = New SqlCommand(loadUserBookingSql, conn)

        Dim checkBooksSql As String = "SELECT booking_id,FORMAT (booking_date_start, 'dd/MM/yyyy'), FORMAT (booking_date_start, 'hh:mm'), 
                                       FORMAT (booking_date_end, 'dd/MM/yyyy'), FORMAT (booking_date_end, 'hh:mm')
                                       FROM booking"
        checkBookCmd = New SqlCommand(checkBooksSql, conn)

        Dim checkCurrDaySql As String = "SELECT booking_id,school_name, booking.court_id, booking_date_start, booking_date_end
                                         FROM booking INNER JOIN Court ON (Court.court_id = booking.court_id) 
                                         INNER JOIN Venues ON Venues.venue_id = Court.school_id"
        checkCurrDayCmd = New SqlCommand(checkCurrDaySql, conn)

        If Not IsPostBack Then
            Dim adapter As SqlDataAdapter = New SqlDataAdapter(loadVentureCmd)
            Dim ds As DataSet = New DataSet()
            adapter.Fill(ds, "numVenues")

            Dim dt As DataTable = ds.Tables("numVenues")

            If dt.Rows.Count < 1 Then
                MsgBox("No Venues")
            Else
                Dim numCourtsAvailable As Integer = 0
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim dr As DataRow = dt.Rows(i)
                    Dim newRow As TableRow = New TableRow
                    newRow.CssClass = "rows"
                    Dim courtId As Integer = dr("venue_id")
                    Dim SchoolName As String = dr("school_name")
                    Dim SchoolTag As String = dr("school_tag")
                    Dim SchoolLocation As String = dr("school_address")
                    Dim SchoolAvailable As Integer = dr("school_available_courts")
                    numCourtsAvailable += SchoolAvailable
                    Dim ven As TableCell = New TableCell
                    ven.Text = courtId
                    Dim sch As TableCell = New TableCell
                    sch.Text = SchoolName
                    Dim loc As TableCell = New TableCell
                    loc.Text = SchoolLocation
                    Dim ava As TableCell = New TableCell
                    ava.Text = SchoolAvailable
                    Dim butt As LinkButton = New LinkButton
                    butt.Text = "Book"
                    butt.ID = SchoolTag
                    butt.PostBackUrl = "BookingPage.aspx"
                    AddHandler butt.Click, AddressOf btn_to_booking_page_Click
                    'Dim butt As HyperLink = New HyperLink
                    'butt.Text = "Book"
                    'butt.ID = SchoolTag
                    'butt.NavigateUrl = "BookingPage.aspx"
                    Dim but As TableCell = New TableCell
                    but.Controls.Add(butt)
                    newRow.Cells.Add(ven)
                    newRow.Cells.Add(sch)
                    newRow.Cells.Add(loc)
                    newRow.Cells.Add(ava)
                    newRow.Cells.Add(but)
                    available_venue.Rows.Add(newRow)
                Next
                lblVenue.Text = numCourtsAvailable
            End If
        End If
        loadUserBookingCmd.Parameters.Clear()
        loadUserBookingCmd.Parameters.AddWithValue("uid", PubVar.userId)

        Dim adapter2 As SqlDataAdapter = New SqlDataAdapter(loadUserBookingCmd)
            Dim ds2 As DataSet = New DataSet()
            adapter2.Fill(ds2, "userBooking")

            Dim dt2 As DataTable = ds2.Tables("userBooking")

        If dt2.Rows.Count < 1 Then
            lblNoBooks.Visible = True
            lblNoBooks.Enabled = True
            user_booked_tables.Visible = False
            btn_edit.Visible = False
            btn_delete.Visible = False
        Else
            For i As Integer = 0 To dt2.Rows.Count() - 1
                Dim dr As DataRow = dt2.Rows(i)
                Dim newRow As TableRow = New TableRow
                newRow.CssClass = "rows"
                Dim venueLoc As String = dr("school_address")
                Dim bookingId As Integer = dr("booking_id")
                Dim user_venue As Integer = dr("court_id")
                Dim schName As String = dr("school_name")
                Dim booking_start As String = dr("booking_date_start")
                Dim booking_end As String = dr("booking_date_end")
                Dim payment_date As String = dr("payment_date")
                Dim venloc As TableCell = New TableCell
                venloc.Text = venueLoc
                Dim inBook As TableCell = New TableCell
                inBook.Text = bookingId
                Dim book As TableCell = New TableCell
                book.Text = i + 1
                Dim ven As TableCell = New TableCell
                ven.Text = user_venue
                Dim sch As TableCell = New TableCell
                sch.Text = schName
                Dim bookStart As TableCell = New TableCell
                bookStart.Text = booking_start
                Dim bookEnd As TableCell = New TableCell
                bookEnd.Text = booking_end
                Dim payDate As TableCell = New TableCell
                payDate.Text = payment_date
                newRow.Cells.Add(ven)
                newRow.Cells.Add(sch)
                newRow.Cells.Add(venloc)
                newRow.Cells.Add(bookStart)
                newRow.Cells.Add(bookEnd)
                user_booked_tables.Rows.Add(newRow)
            Next
        End If
        If Not IsPostBack Then
            cal_venue.SelectedDate = DateTime.Parse(Date.Now.ToString("dd/MM/yyyy"))
            lbl_userId.Text = "Welcome, " + userName + ". Your id is: " + PubVar.userId.ToString
        End If
    End Sub

    Protected Sub btn_to_booking_page_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button
        btn = CType(sender, Button)

        tags = btn.ID
    End Sub

    Protected Sub btn_logout_Click(sender As Object, e As EventArgs) Handles btn_logout.Click
        Response.Redirect("Log_In_page.aspx")
    End Sub


    Protected Sub btn_booking_Click(sender As Object, e As EventArgs) Handles btn_booking.Click
        Response.Redirect("BookingPage.aspx")
    End Sub

    Protected Sub btn_edit_Click(sender As Object, e As EventArgs) Handles btn_edit.Click
        Response.Redirect("Edit_booking.aspx")
    End Sub

    Protected Sub btn_delete_Click(sender As Object, e As EventArgs) Handles btn_delete.Click
        Response.Redirect("Delete_booking.aspx")
    End Sub

    Protected Sub btn_check_Click(sender As Object, e As EventArgs) Handles btn_check.Click
        Dim startDate As DateTime = DateTime.Parse(cal_venue.SelectedDate.Date.ToString("dd/MM/yyyy") + " " + drp_start_hr.Text + ":00 " + drp_start_ampm.Text)
        Dim endDate As DateTime = DateTime.Parse(cal_venue.SelectedDate.Date.ToString("dd/MM/yyyy") + " " + drp_end_hr.Text + ":00 " + drp_end_ampm.Text)
        If chk_next_day.Checked Then
            endDate = endDate.AddDays(1)
        End If

        available_venue.Rows.Clear()

        Dim adapter As SqlDataAdapter = New SqlDataAdapter(checkCurrDayCmd)
        Dim ds As DataSet = New DataSet()
        adapter.Fill(ds, "TableCheck")

        Dim dt As DataTable = ds.Tables("TableCheck")

        If dt.Rows.Count < 1 Then
            MsgBox("Error, failed to retrive data")
        Else
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dr As DataRow = dt.Rows(i)
                If (startDate >= dr("booking_date_start") And endDate <= dr("booking_date_end")) OrElse
                    ((startDate >= dr("booking_date_start") And startDate <= dr("booking_date_end")) And DateDiff("h", dr("booking_date_end"), endDate) >= 0) OrElse
                    ((endDate <= dr("booking_date_end") And endDate >= dr("booking_date_start")) And DateDiff("h", startDate, dr("booking_date_start")) <= 0) OrElse
                    ((DateDiff("h", dr("booking_date_end"), endDate) >= 0) And DateDiff("h", dr("booking_date_start"), startDate) <= 0) Then
                    Dim newRow As TableRow = New TableRow
                    Dim id As Integer = dr("booking_id")
                    Dim schName As String = dr("school_name")
                    Dim courtId As Integer = dr("court_id")
                    Dim bookStart As DateTime = dr("booking_date_start")
                    Dim bookEnd As DateTime = dr("booking_date_end")

                    Dim cellId As TableCell = New TableCell
                    Dim cellSch As TableCell = New TableCell
                    Dim cellCourt As TableCell = New TableCell
                    Dim cellStart As TableCell = New TableCell
                    Dim cellEnd As TableCell = New TableCell

                    cellId.Text = id
                    cellSch.Text = schName
                    cellCourt.Text = courtId
                    cellStart.Text = bookStart.ToString
                    cellEnd.Text = bookEnd.ToString

                    newRow.Cells.Add(cellId)
                    newRow.Cells.Add(cellSch)
                    newRow.Cells.Add(cellCourt)
                    newRow.Cells.Add(cellStart)
                    newRow.Cells.Add(cellEnd)
                    available_venue.Rows.Add(newRow)
                End If
            Next

        End If


        btn_cancel.Visible = True
    End Sub

    Protected Sub btn_cancel_Click(sender As Object, e As EventArgs) Handles btn_cancel.Click
        Dim adapter As SqlDataAdapter = New SqlDataAdapter(loadVentureCmd)
        Dim ds As DataSet = New DataSet()
        adapter.Fill(ds, "numVenues")

        Dim dt As DataTable = ds.Tables("numVenues")

        If dt.Rows.Count < 1 Then
            MsgBox("No Venues")
        Else
            Dim numCourtsAvailable As Integer = 0
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dr As DataRow = dt.Rows(i)
                Dim newRow As TableRow = New TableRow
                newRow.CssClass = "rows"
                Dim courtId As Integer = dr("venue_id")
                Dim SchoolName As String = dr("school_name")
                Dim SchoolTag As String = dr("school_tag")
                Dim SchoolLocation As String = dr("school_address")
                Dim SchoolAvailable As Integer = dr("school_available_courts")
                numCourtsAvailable += SchoolAvailable
                Dim ven As TableCell = New TableCell
                ven.Text = courtId
                ven.Visible = False
                Dim sch As TableCell = New TableCell
                sch.Text = SchoolName
                Dim loc As TableCell = New TableCell
                loc.Text = SchoolLocation
                Dim ava As TableCell = New TableCell
                ava.Text = SchoolAvailable
                Dim butt As LinkButton = New LinkButton
                butt.Text = "Book"
                butt.ID = SchoolTag
                butt.PostBackUrl = "BookingPage.aspx"
                AddHandler butt.Click, AddressOf btn_to_booking_page_Click
                'Dim butt As HyperLink = New HyperLink
                'butt.Text = "Book"
                'butt.ID = SchoolTag
                'butt.NavigateUrl = "BookingPage.aspx"
                Dim but As TableCell = New TableCell
                but.Controls.Add(butt)
                newRow.Cells.Add(ven)
                newRow.Cells.Add(sch)
                newRow.Cells.Add(loc)
                newRow.Cells.Add(ava)
                newRow.Cells.Add(but)
                available_venue.Rows.Add(newRow)
            Next
            lblVenue.Text = numCourtsAvailable
        End If
    End Sub
End Class