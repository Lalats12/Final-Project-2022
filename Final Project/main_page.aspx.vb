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



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim connStr As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\jpyea\source\repos\Final Project\Final Project\App_Data\badminton_database.mdf"";Integrated Security=True;Connect Timeout=30"
        conn = New SqlConnection(connStr)
        conn.Open()

        Dim loadVentureSql As String = "SELECT * FROM Venues"
        loadVentureCmd = New SqlCommand(loadVentureSql, conn)

        Dim loadUserBookingSql As String = "SELECT * FROM booking WHERE user_id = @uid"
        loadUserBookingCmd = New SqlCommand(loadUserBookingSql, conn)

        Dim checkBooksSql As String = "SELECT booking_id,FORMAT (booking_date_start, 'dd/MM/yyyy'), FORMAT (booking_date_start, 'hh:mm'), 
                                       FORMAT (booking_date_end, 'dd/MM/yyyy'), FORMAT (booking_date_end, 'hh:mm')
                                       FROM booking"
        checkBookCmd = New SqlCommand(checkBooksSql, conn)

        Dim adapter As SqlDataAdapter = New SqlDataAdapter(loadVentureCmd)
        Dim ds As DataSet = New DataSet()
        adapter.Fill(ds, "numVenues")

        Dim dt As DataTable = ds.Tables("numVenues")

        If dt.Rows.Count < 1 Then
            MsgBox("No Venues")
        Else
            Dim numCourtsAvailable As Integer = 0
            If Not IsPostBack Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim dr As DataRow = dt.Rows(i)
                    Dim newRow As TableRow = New TableRow
                    newRow.CssClass = "rows"
                    Dim VenueId As String = dr("venue_id")
                    Dim SchoolName As String = dr("school_name")
                    Dim SchoolTag As String = dr("school_tag")
                    Dim SchoolLocation As Integer = dr("school_location")
                    Dim SchoolAvailable As Integer = dr("school_available_courts")
                    numCourtsAvailable += SchoolAvailable
                    Dim ven As TableCell = New TableCell
                    ven.Text = VenueId
                    ven.Visible = False
                    Dim sch As TableCell = New TableCell
                    sch.Text = SchoolName
                    Dim loc As TableCell = New TableCell
                    loc.Text = SchoolLocation
                    Dim ava As TableCell = New TableCell
                    ava.Text = SchoolAvailable
                    Dim butt As HyperLink = New HyperLink
                    butt.Text = "Book"
                    butt.ID = SchoolTag
                    butt.NavigateUrl = "BookingPage.aspx"
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
        loadUserBookingCmd.Parameters.AddWithValue("uid", userId)

        Dim adapter2 As SqlDataAdapter = New SqlDataAdapter(loadUserBookingCmd)
        Dim ds2 As DataSet = New DataSet()
        adapter2.Fill(ds2, "userBooking")

        Dim dt2 As DataTable = ds2.Tables("userBooking")

        If dt2.Rows.Count < 1 Then
            lblNoBooks.Visible = True
            lblNoBooks.Enabled = True
        Else
            If Not IsPostBack Then
                For i As Integer = 0 To dt2.Rows.Count() - 1
                    Dim dr As DataRow = dt2.Rows(i)
                    Dim newRow As TableRow = New TableRow
                    newRow.CssClass = "rows"
                    Dim user_venue As Integer = dr("court_id")
                    Dim booking_start As String = dr("booking_date_start")
                    Dim booking_end As String = dr("booking_date_end")
                    Dim payment_date As String = dr("payment_date")
                    Dim ven As TableCell = New TableCell
                    ven.Text = user_venue
                    Dim bookStart As TableCell = New TableCell
                    bookStart.Text = booking_start
                    Dim bookEnd As TableCell = New TableCell
                    bookEnd.Text = booking_end
                    Dim payDate As TableCell = New TableCell
                    payDate.Text = payment_date
                    newRow.Cells.Add(ven)
                    newRow.Cells.Add(bookStart)
                    newRow.Cells.Add(bookEnd)
                    newRow.Cells.Add(payDate)
                    user_booked_tables.Rows.Add(newRow)
                Next
            End If
        End If
        If Not IsPostBack Then
            cal_venue.SelectedDate = Date.Now()
            lbl_userId.Text = "Welcome, " + Name
        End If
    End Sub

    Protected Sub btn_logout_Click(sender As Object, e As EventArgs) Handles btn_logout.Click
        Response.Redirect("Log_In_page.aspx")
    End Sub
End Class