Imports System.Data.SqlClient

Public Class BookingPage
    Inherits System.Web.UI.Page
    Dim conn As SqlConnection
    Dim loadCourtsCmd As SqlCommand
    Dim getCourtsCmd As SqlCommand
    Dim checkUserCmd As SqlCommand
    Dim checkBooksCmd As SqlCommand

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim connStr As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\jpyea\source\repos\Final Project\Final Project\App_Data\badminton_database.mdf"";Integrated Security=True"
        conn = New SqlConnection(connStr)
        conn.Open()

        Dim loadCourtsSql As String = "SELECT DISTINCT school_id, school_name
                                       FROM Court INNER JOIN Venues ON Venues.venue_id = court.school_id"
        loadCourtsCmd = New SqlCommand(loadCourtsSql, conn)

        Dim getCourtsSql As String = "SELECT venue_id, school_name, court_id
                                      FROM Venues INNER JOIN Court ON Venues.venue_id = court.school_id
                                      WHERE school_id = @schid"
        getCourtsCmd = New SqlCommand(getCourtsSql, conn)

        Dim checkUserSql As String = "SELECT booking_id, booking.user_id, school_name, booking.court_id, booking_date_start, booking_date_end, school_address
                                      FROM booking INNER JOIN Court ON Court.court_id = booking.court_id INNER JOIN user_data ON booking.user_id = user_data.user_id 
                                      INNER JOIN Venues ON Venues.venue_id = Court.school_id INNER JOIN Locations ON Locations.location_id = Venues.school_location"

        Dim checkBooksSql As String = "SELECT booking_id,booking_date_start, booking_date_end 
                                       FROM booking
                                       WHERE court_id = @coid"

        checkBooksCmd = New SqlCommand(checkBooksSql, conn)

        Dim adapter As SqlDataAdapter = New SqlDataAdapter(loadCourtsCmd)
        Dim ds As DataSet = New DataSet()
        adapter.Fill(ds, "LoadSchools")

        Dim dt As DataTable = ds.Tables("LoadSchools")

        If dt.Rows.Count < 1 Then
            MsgBox("Error, data failed to retrive")
        Else
            drp_school.Items.Clear()
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dr As DataRow = dt.Rows(i)
                drp_school.Items.Add(dr("school_name"))
                drp_school.Items.Item(i).Value = dr("school_id")
            Next
        End If

        If Not IsPostBack Then
            cal_booking_date.SelectedDate = Date.Now()
        End If
    End Sub

    Protected Sub btn_book_Click(sender As Object, e As EventArgs) Handles btn_book.Click
        Dim booking_date As String = cal_booking_date.SelectedDate.Date.ToString("dd/MM/yyyy")
        If booking_date < Date.Now.ToString("dd/MM/yyyy") Then
            MsgBox("The date you inputted is behind the current date.")
        End If
        Dim startDate As DateTime = DateTime.Parse(booking_date + " " + start_time_hr.SelectedValue + ":" +
        start_time_min.SelectedValue + " " + start_time_ampm.SelectedValue)
        Dim endDate As DateTime = DateTime.Parse(cal_booking_date.SelectedDate.Date.ToString("dd/MM/yyyy") + " " + end_time_hr.SelectedValue + ":" +
        end_time_min.SelectedValue + " " + end_time_ampm.SelectedValue)

        If (DateDiff("n", startDate, endDate) < 0) Then
            MsgBox("The end date must be later than the start date")
            Exit Sub
        End If

        checkBooksCmd.Parameters.Clear()
        checkBooksCmd.Parameters.AddWithValue("coid", drp_court.SelectedValue)

        Dim adapter As SqlDataAdapter = New SqlDataAdapter(checkBooksCmd)
        Dim ds As DataSet = New DataSet()
        adapter.Fill(ds, "tableCheck")

        Dim dt As DataTable = ds.Tables("tableCheck")

        If dt.Rows.Count < 1 Then
            MsgBox("Error occured, please try again")
        Else
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dr As DataRow = dt.Rows(i)
                MsgBox(DateDiff("n", dr("booking_date_end"), endDate))
                MsgBox(DateDiff("n", dr("booking_date_start"), startDate))
                If (startDate >= dr("booking_date_start") And endDate <= dr("booking_date_end")) OrElse
                   (startDate >= dr("booking_date_start") And DateDiff("n", dr("booking_date_end"), endDate) >= 15) OrElse
                   (endDate <= dr("booking_date_end") And DateDiff("n", startDate, dr("booking_date_start")) <= -15) OrElse
                   ((DateDiff("n", dr("booking_date_end"), endDate) >= 15) And DateDiff("n", dr("booking_date_start"), startDate) <= -15) Then
                    MsgBox("Booking collision detected. Please try again")
                End If
            Next
        End If

    End Sub

    Protected Sub drp_school_SelectedIndexChanged(sender As Object, e As EventArgs) Handles drp_school.SelectedIndexChanged
        drp_court.Items.Clear()
        getCourtsCmd.Parameters.Clear()
        getCourtsCmd.Parameters.AddWithValue("schid", drp_school.SelectedValue)
    End Sub
End Class