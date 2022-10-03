Imports System.Data.SqlClient

Public Module GlobalVariables
    Public schoolTag As String
End Module

Public Class WebForm1
    Inherits System.Web.UI.Page

    Dim conn As SqlConnection
    Dim loadVentureCmd As SqlCommand



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim connStr As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\jpyea\source\repos\Final Project\Final Project\App_Data\badminton_database.mdf"";Integrated Security=True;Connect Timeout=30"
        conn = New SqlConnection(connStr)
        conn.Open()

        Dim loadVentureSql As String = "SELECT * FROM Venues"
        loadVentureCmd = New SqlCommand(loadVentureSql, conn)

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
                    Dim SchoolSchedule As String = dr("school_schedule")
                    Dim ven As TableCell = New TableCell
                    ven.Text = VenueId
                    Dim sch As TableCell = New TableCell
                    sch.Text = SchoolName
                    Dim loc As TableCell = New TableCell
                    loc.Text = SchoolLocation
                    Dim ava As TableCell = New TableCell
                    ava.Text = SchoolAvailable
                    Dim sche As TableCell = New TableCell
                    sche.Text = SchoolSchedule
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
                    newRow.Cells.Add(sche)
                    newRow.Cells.Add(but)
                    Table1.Rows.Add(newRow)
                Next
                lblVenue.Text = numCourtsAvailable
            End If
        End If
        lbl_userId.Text = "Welcome, " + Name
    End Sub

    Protected Sub btn_logout_Click(sender As Object, e As EventArgs) Handles btn_logout.Click
        Response.Redirect("Log_In_page.aspx")
    End Sub
End Class