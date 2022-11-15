Imports System.Data.SqlClient
Public Class AdminUser
    Inherits System.Web.UI.Page
    Dim conn As SqlConnection
    Dim getUserCmd As SqlCommand
    Dim updateUserCmd As SqlCommand
    Dim checkUserCmd As SqlCommand
    Dim deleteUserCmd As SqlCommand
    Dim checkBookingCmd As SqlCommand

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("IsAdmin") Is Nothing Then
            Server.Transfer("../Starting_Page.aspx")
        End If
        Dim connStr As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\jpyea\source\repos\Final Project\Final Project\App_Data\badminton_database.mdf"";Integrated Security=True"
        conn = New SqlConnection(connStr)
        conn.Open()

        Dim getUserSql As String = "SELECT * FROM user_data WHERE user_id = @uid"
        getUserCmd = New SqlCommand(getUserSql, conn)

        Dim updateUserSql As String = "UPDATE user_data SET username = @unam, user_password = @pas,user_email = @uem, user_numbers = @unum WHERE user_id = @uid"
        updateUserCmd = New SqlCommand(updateUserSql, conn)

        Dim checkUserSql As String = "SELECT * FROM user_data WHERE (username = @unam OR user_email = @uem OR user_numbers = @unum) AND user_id != @uid"
        checkUserCmd = New SqlCommand(checkUserSql, conn)

        Dim deleteUserSql As String = "DELETE user_data WHERE user_id = @uid"
        deleteUserCmd = New SqlCommand(deleteUserSql, conn)

        Dim checkBookingSql As String = "SELECT * FROM booking where user_id = @uid"
        checkBookingCmd = New SqlCommand(checkBookingSql, conn)
    End Sub

    Protected Sub btn_searchUsers_Click(sender As Object, e As EventArgs) Handles btn_searchUsers.Click
        pnl_searchUser.Visible = True
    End Sub

    Protected Sub btn_Userid_Click(sender As Object, e As EventArgs) Handles btn_Userid.Click
        Dim id As Integer = Integer.Parse(txt_userID.Text)

        getUserCmd.Parameters.Clear()
        getUserCmd.Parameters.AddWithValue("uid", id)

        Dim adap As SqlDataAdapter = New SqlDataAdapter(getUserCmd)
        Dim ds As DataSet = New DataSet
        adap.Fill(ds, "user")
        Dim dt As DataTable = ds.Tables("user")

        If dt.Rows.Count < 1 Then
            MsgBox("Error, either that id is non-existent or an error in dt")
        Else
            Dim dr As DataRow = dt.Rows(0)
            Dim name As String = dr("username")
            Dim email As String = dr("user_email")
            Dim pass As String = dr("user_password")
            Dim num As String = dr("user_numbers")

            txt_UserName.Text = name
            txt_email.Text = email
            txt_number.Text = num
            txt_pass.Text = pass
            pnl_users.Visible = True
            txt_userID.Enabled = False
        End If

    End Sub


    Protected Sub btn_editUser_Click(sender As Object, e As EventArgs) Handles btn_editUser.Click
        Dim id As Integer = Integer.Parse(txt_userID.Text)
        Dim user As String = txt_UserName.Text
        Dim email As String = txt_email.Text
        Dim pass As String = txt_pass.Text
        Dim phone As String = txt_number.Text

        checkUserCmd.Parameters.Clear()
        checkUserCmd.Parameters.AddWithValue("unam", user)
        checkUserCmd.Parameters.AddWithValue("uem", email)
        checkUserCmd.Parameters.AddWithValue("unum", phone)
        checkUserCmd.Parameters.AddWithValue("uid", id)

        Dim adap As SqlDataAdapter = New SqlDataAdapter(checkUserCmd)
        Dim ds As DataSet = New DataSet
        adap.Fill(ds, "check")
        Dim dt As DataTable = ds.Tables("check")

        If dt.Rows.Count < 1 Then
            MsgBox("Error, there are users with the smae name/email/numbers")
            Exit Sub
        End If

        updateUserCmd.Parameters.Clear()
        updateUserCmd.Parameters.AddWithValue("unam", user)
        updateUserCmd.Parameters.AddWithValue("uem", email)
        updateUserCmd.Parameters.AddWithValue("pas", pass)
        updateUserCmd.Parameters.AddWithValue("unum", phone)
        updateUserCmd.Parameters.AddWithValue("uid", id)

        Dim rowsAff As Integer = updateUserCmd.ExecuteNonQuery
        If rowsAff < 1 Then
            MsgBox("Error, Something's wrong while updating data")
        Else
            MsgBox("Success, refreshing")
            Server.Transfer("AdminUser.aspx")
        End If
    End Sub

    Protected Sub btn_cancel_Click(sender As Object, e As EventArgs) Handles btn_cancel.Click
        pnl_users.Visible = False
        pnl_searchUser.Visible = False
        txt_userID.Enabled = True
        wipeData()
    End Sub

    Private Sub wipeData()
        txt_email.Text = ""
        txt_number.Text = ""
        txt_userID.Text = ""
        txt_UserName.Text = ""
    End Sub


    Protected Sub btn_Userdelete_Click(sender As Object, e As EventArgs) Handles btn_Userdelete.Click
        Dim id As Integer = Integer.Parse(txt_userID.Text)

        checkBookingCmd.Parameters.Clear()
        checkBookingCmd.Parameters.AddWithValue("uid", id)
        Dim adap As SqlDataAdapter = New SqlDataAdapter(checkBookingCmd)
        Dim ds As DataSet = New DataSet
        adap.Fill(ds, "check")
        Dim dt As DataTable = ds.Tables("check")

        If dt.Rows.Count > 0 Then
            MsgBox("There are bookings made with this accounts")
            Exit Sub
        End If

        deleteUserCmd.Parameters.Clear()
        deleteUserCmd.Parameters.AddWithValue("uid", id)

        Dim rowsAff As Integer = deleteUserCmd.ExecuteNonQuery

        If rowsAff < 1 Then
            MsgBox("Error, something's wrong with the system")
        Else
            MsgBox("Success, the user is deleted")
            Server.Transfer("AdminUser.aspx")
        End If
    End Sub

End Class