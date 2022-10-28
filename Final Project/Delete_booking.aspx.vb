Imports System.Data.SqlClient
Public Class Delete_booking
    Inherits System.Web.UI.Page
    Dim conn As SqlConnection
    Dim loadUserBookCmd As SqlCommand
    Dim getPaymentCmd As SqlCommand
    Dim delPaymentCmd As SqlCommand
    Dim delBookingCmd As SqlCommand


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim connStr As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\jpyea\source\repos\Final Project\Final Project\App_Data\badminton_database.mdf"";Integrated Security=True"
        conn = New SqlConnection(connStr)
        conn.Open()

        Dim loadUserBookSql As String = "SELECT Court.court_id,booking_id, Payment.payment_id, booking_date_start,booking_date_end
                                         FROM booking INNER JOIN user_data ON user_data.user_id = booking.user_id 
                                         INNER JOIN Payment ON (payment.payment_id = booking.payment_id)
                                         INNER JOIN Court ON (Court.court_id = booking.court_id) INNER JOIN Venues ON (Venues.venue_id = Court.school_id)
                                         INNER JOIN Locations ON (Locations.location_id = Venues.school_location)
                                         WHERE user_data.user_id = @uid"
        loadUserBookCmd = New SqlCommand(loadUserBookSql, conn)

        Dim getPaymentSql As String = "SELECT Payment.payment_id
                                       FROM Payment INNER JOIN booking ON (Payment.payment_id = booking.payment_id)
                                       WHERE booking_id = @bid"
        getPaymentCmd = New SqlCommand(getPaymentSql, conn)

        Dim delPaymentSql As String = "DELETE FROM payment WHERE payment_id = @pid"
        delPaymentCmd = New SqlCommand(delPaymentSql, conn)

        Dim delBookingsql As String = "DELETE FROM booking WHERE booking_id = @bid"
        delBookingCmd = New SqlCommand(delBookingsql, conn)

        loadUserBookCmd.Parameters.Clear()
        loadUserBookCmd.Parameters.AddWithValue("uid", PubVar.userId)

        Dim adapter As SqlDataAdapter = New SqlDataAdapter(loadUserBookCmd)
        Dim ds As DataSet = New DataSet
        adapter.Fill(ds, "getUserData")

        Dim dt As DataTable = ds.Tables("getUserData")

        If dt.Rows.Count < 1 Then
            MsgBox("There is no booking made. Get started by entering the booking now")
            Response.Redirect("main_page.aspx")
        Else
            If Not IsPostBack Then
                lst_booking.Items.Clear()
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim dr As DataRow = dt.Rows(i)
                    Dim book As Integer = dr("booking_id")
                    Dim bookStart As DateTime = dr("booking_date_start")
                    Dim bookEnd As DateTime = dr("booking_date_end")
                    Dim str As String = "ID: " + book.ToString + " Start: " + bookStart.ToString + " End:" + bookEnd.ToString
                    lst_booking.Items.Add(str)
                    lst_booking.Items.Item(i).Value = book
                Next
            End If
        End If
    End Sub

    Protected Sub lst_booking_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lst_booking.SelectedIndexChanged
        bookId = Integer.Parse(lst_booking.SelectedValue)
        getPaymentCmd.Parameters.Clear()
        getPaymentCmd.Parameters.AddWithValue("bid", bookId)

        Dim adapter As SqlDataAdapter = New SqlDataAdapter(getPaymentCmd)
        Dim ds As DataSet = New DataSet
        adapter.Fill(ds, "getPaymentId")

        Dim dt As DataTable = ds.Tables("getPaymentId")

        If dt.Rows.Count < 1 Then
            MsgBox("Error, no data found")
        Else
            Dim dr As DataRow = dt.Rows(0)
            payId = dr("payment_id")
        End If

        lbl_Test.Text = bookId.ToString + " Payid" + payId.ToString
    End Sub

    Protected Sub btn_mainMenu_Click(sender As Object, e As EventArgs) Handles btn_mainMenu.Click
        Response.Redirect("main_page.aspx")
    End Sub

    Protected Sub btn_del_Click(sender As Object, e As EventArgs) Handles btn_del.Click
        If lst_booking.SelectedIndex < 0 Then
            MsgBox("No booking found, please try again")
            Exit Sub
        End If
        Dim userRe As Integer = MsgBox("Are you sure you want to delete?", vbQuestion + vbYesNo + vbDefaultButton2, "Confirmation")

        If userRe = vbYes Then
            delPaymentCmd.Parameters.Clear()
            delPaymentCmd.Parameters.AddWithValue("pid", payId)

            delBookingCmd.Parameters.Clear()
            delBookingCmd.Parameters.AddWithValue("bid", bookId)

            Dim rowsAffected As Integer = delPaymentCmd.ExecuteNonQuery()
            If rowsAffected < 1 Then
                MsgBox("Error, unclear error")
            Else
                Dim rowsAffected2 As Integer = delBookingCmd.ExecuteNonQuery()
                If rowsAffected2 < 1 Then
                    MsgBox("Error, rowsAffected2 Problem")
                Else
                    MsgBox("Booking Deleted")
                End If
            End If

        End If
    End Sub
End Class