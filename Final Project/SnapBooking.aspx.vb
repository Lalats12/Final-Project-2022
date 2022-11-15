Imports System.Data.SqlClient
Public Class SnapBooking
    Inherits System.Web.UI.Page
    Dim conn As SqlConnection
    Dim getBookingCmd As SqlCommand
    Dim getOneCmd As SqlCommand
    Dim getUserCmd As SqlCommand

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("UserID") Is Nothing Then
            Server.Transfer("Starting_page.aspx")
        End If
        Dim strConn As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\jpyea\source\repos\Final Project\Final Project\App_Data\badminton_database.mdf"";Integrated Security=True"
        conn = New SqlConnection(strConn)
        conn.Open()

        Dim getBookingSql As String = "SELECT booking_id,court_id,booking_date_start
                                       FROM booking
                                       WHERE user_id = @uid"
        getBookingCmd = New SqlCommand(getBookingSql, conn)

        Dim getOneSql As String = "SELECT *
                                   FROM booking INNER JOIN Court ON (booking.court_id = Court.court_id)
                                   INNER JOIN Venues ON (Court.school_id = Venues.venue_id)
                                   WHERE booking_id = @id"
        getOneCmd = New SqlCommand(getOneSql, conn)

        Dim getUserSql As String = "SELECT user_id,username, user_email, user_numbers
                                    FROM user_data
                                    WHERE user_id = @id"
        getUserCmd = New SqlCommand(getUserSql, conn)

        getBookingCmd.Parameters.Clear()
        getBookingCmd.Parameters.AddWithValue("uid", Session("UserID"))

        Dim adap As SqlDataAdapter = New SqlDataAdapter(getBookingCmd)
        Dim ds As DataSet = New DataSet
        adap.Fill(ds, "list")

        Dim dt As DataTable = ds.Tables("list")
        If dt.Rows.Count < 1 Then
            MsgBox("Error when fetching your data. Either you did not book any or server is down")
            Server.Transfer("main_page.aspx")
        Else
            If Not IsPostBack Then
                drp_booking.Items.Clear()
                drp_booking.Items.Add("(Select)")
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim dr As DataRow = dt.Rows(i)
                    Dim court As Integer = dr("court_id")
                    Dim bookingStart As String = dr("booking_date_start").ToString
                    drp_booking.Items.Add("Court ID:" + court.ToString + " Starting date:" + bookingStart)
                    drp_booking.Items.Item(i + 1).Value = dr("booking_id")
                Next
            End If
        End If

        getUserCmd.Parameters.Clear()
        getUserCmd.Parameters.AddWithValue("id", Session("UserID"))

        adap = New SqlDataAdapter(getUserCmd)
        ds = New DataSet
        adap.Fill(ds, "user")
        Dim dt2 As DataTable = ds.Tables("user")
        If dt2.Rows.Count <> 1 Then
            MsgBox("Error related while fetching your data")
            Server.Transfer("main_page.aspx")
        Else
            Dim dr As DataRow = dt2.Rows(0)
            lbl_ID.Text = dr("user_id")
            lbl_name.Text = dr("username")
            lbl_email.Text = dr("user_email")
            lbl_phone.Text = dr("user_numbers")
        End If

    End Sub

    Protected Sub drp_booking_SelectedIndexChanged(sender As Object, e As EventArgs) Handles drp_booking.SelectedIndexChanged
        getOneCmd.Parameters.Clear()
        getOneCmd.Parameters.AddWithValue("id", drp_booking.SelectedValue)

        Dim adap As SqlDataAdapter = New SqlDataAdapter(getOneCmd)
        Dim ds As DataSet = New DataSet
        adap.Fill(ds, "one")

        Dim dt As DataTable = ds.Tables("one")
        If dt.Rows.Count <> 1 Then
            MsgBox("Error while fetching your data")
            Server.Transfer("main_page.aspx")
        Else
            Dim dr As DataRow = dt.Rows(0)
            lbl_court.Text = dr("court_id")
            lbl_school.Text = dr("school_name")
            lbl_start_date.Text = dr("booking_date_start")
            lbl_end_date.Text = dr("booking_date_end")
        End If

    End Sub
End Class