Imports System.Data.SqlClient

Public Class BookingPage
    Inherits System.Web.UI.Page
    Dim conn As SqlConnection
    Dim getCourtsCmd As SqlCommand
    Dim checkUserCmd As SqlCommand

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim connStr As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\jpyea\source\repos\Final Project\Final Project\App_Data\badminton_database.mdf"";Integrated Security=True"
            conn = New SqlConnection(connStr)
            conn.Open()

            Dim getCourtsSql As String = "SELECT venue_id, school_name, court_id
                                      FROM Venues INNER JOIN Court ON Venues.venue_id = court.school_id"
            getCourtsCmd = New SqlCommand(getCourtsSql, conn)

            Dim checkUserSql As String = "SELECT booking_id, booking.user_id, school_name, booking.court_id, booking_date_start, booking_date_end, school_address
                                      FROM booking INNER JOIN Court ON Court.court_id = booking.court_id INNER JOIN user_data ON booking.user_id = user_data.user_id 
                                      INNER JOIN Venues ON Venues.venue_id = Court.school_id INNER JOIN Locations ON Locations.location_id = Venues.school_location"
            checkUserCmd = New SqlCommand(checkUserSql, conn)
            cal_booking_date.SelectedDate = Date.Now()
        End If
    End Sub

    Protected Sub btn_book_Click(sender As Object, e As EventArgs) Handles btn_book.Click
        lbl_results.Text = cal_booking_date.SelectedDate.Date.ToString
    End Sub
End Class