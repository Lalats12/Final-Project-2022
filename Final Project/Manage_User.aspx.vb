Imports System.Data.SqlClient
Public Class Manage_User
    Inherits System.Web.UI.Page
    Dim conn As SqlConnection
    Dim loadUserCmd As SqlCommand
    Dim updateUserCmd As SqlCommand
    Dim matchPassCmd As SqlCommand
    Dim checkUserCmd As SqlCommand
    Dim deleteUserCmd As SqlCommand
    Dim regexEmail As Regex = New Regex("^((([^<>()[\]\\.,;:\s@])+\.?([^!@#$%^&*()_+{}:<>?])+)|.*)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z]+\.))+[a-zA-Z]{2,})")
    Dim regexPass As Regex = New Regex("^(?=.*[0-9])(?=.*[A-Z])(?=.*[a-z])(?=.*[^\w\d\s:]).{8,64}")
    Dim regexNums As Regex = New Regex("^((?<malaysiacode>\(?\+?\d{2}\)?)? ?)(\d{2,3})[ -]?(\d{3})[ -]?(\d{4})$")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("UserID") Is Nothing Then
            Server.Transfer("Starting_Page.aspx")
        End If
        Dim connStr = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\jpyea\source\repos\Final Project\Final Project\App_Data\badminton_database.mdf"";Integrated Security=True"
        conn = New SqlConnection
        conn.Open()

        Dim loadUserSql As String = "SELECT * FROM user_data WHERE user_id = @id"
        loadUserCmd = New SqlCommand(loadUserSql, conn)

        Dim updateUserSql As String = "UPDATE user_data SET username = @un, user_password = @up, user_email = @uem, user_numbers = @unum WHERE user_id = @uid"
        updateUserCmd = New SqlCommand(updateUserSql, conn)

        Dim matchPassSql As String = "SELECT user_id FROM user_data WHERE user_id = @uid AND user_password = @up"
        matchPassCmd = New SqlCommand(matchPassSql, conn)

        Dim checkUserSql As String = "SELECT user_id
                                      FROM user_data
                                      WHERE (username = @un OR user_email = @ue OR user_numbers = @unum) AND user_id != @uid"
        checkUserCmd = New SqlCommand(checkUserSql, conn)

        Dim deleteUserSql As String = "DELETE FROM user_data WHERE user_id = @uid"
        deleteUserCmd = New SqlCommand(deleteUserSql, conn)

        loadUserCmd.Parameters.Clear()
        loadUserCmd.Parameters.AddWithValue("id", Session("UserID"))

        Dim adap As SqlDataAdapter = New SqlDataAdapter(loadUserCmd)
        Dim ds As DataSet = New DataSet
        adap.Fill(ds, "user")
        Dim dt As DataTable = ds.Tables("user")

        If dt.Rows.Count < 1 Then
            MsgBox("Unknown error, please try again")
        Else
            Dim dr As DataRow = dt.Rows(0)
            Dim name As String = dr("username")
            Dim email As String = dr("user_email")
            Dim num As String = dr("user_numbers")
        End If
    End Sub

    Protected Sub btn_main_menu_Click(sender As Object, e As EventArgs) Handles btn_main_menu.Click
        Server.Transfer("main_page.aspx")
    End Sub

    Protected Sub btn_confirm_Click(sender As Object, e As EventArgs) Handles btn_confirm.Click
        Dim input As String = InputBox("What is your password?")

        matchPassCmd.Parameters.Clear()
        matchPassCmd.Parameters.AddWithValue("up", input)
        matchPassCmd.Parameters.AddWithValue("uid", Session("UserID"))

        Dim pass As String = txt_pass.Text
        Dim repass As String = txt_repass.Text

        If Not regexNums.IsMatch(pass) Then
            MsgBox("The password is not allowed, please try again")
            Exit Sub
        End If

        If Not regexEmail.IsMatch(txt_email.Text) Then
            MsgBox("The email format is not allowed, please try again")
            Exit Sub
        End If

        If Not regexNums.IsMatch(txt_number.Text) Then
            MsgBox("The numbers must be malaysian numbers")
            Exit Sub
        End If

        If Not pass.CompareTo(repass) = 0 Then
            MsgBox("Your passwords does not match")
            Exit Sub
        End If

        Dim adap As SqlDataAdapter = New SqlDataAdapter(matchPassCmd)
        Dim ds As DataSet = New DataSet
        adap.Fill(ds, "check")
        Dim dt As DataTable = ds.Tables("check")

        If dt.Rows.Count < 1 Then
            MsgBox("Error, either password does not match or a server is down")
            Exit Sub
        End If

        checkUserCmd.Parameters.Clear()
        checkUserCmd.Parameters.AddWithValue("uid", Session("UserID"))
        checkUserCmd.Parameters.AddWithValue("un", txt_name.Text)
        checkUserCmd.Parameters.AddWithValue("ue", txt_email.Text)
        checkUserCmd.Parameters.AddWithValue("unum", txt_number.Text)

        Dim chAdap As SqlDataAdapter = New SqlDataAdapter(checkUserCmd)
        Dim chds As DataSet = New DataSet
        chAdap.Fill(chds, "find")
        Dim chDt As DataTable = chds.Tables("find")

        If chDt.Rows.Count >= 1 Then
            MsgBox("Error, there are users with the same name/email/number.")
            Exit Sub
        End If

        updateUserCmd.Parameters.Clear()
        updateUserCmd.Parameters.AddWithValue("un", txt_name.Text)
        updateUserCmd.Parameters.AddWithValue("up", txt_pass.Text)
        updateUserCmd.Parameters.AddWithValue("uem", txt_email.Text)
        updateUserCmd.Parameters.AddWithValue("unum", txt_number.Text)
        updateUserCmd.Parameters.AddWithValue("uid", Session("UserID"))

        Dim rowsAff As Integer = updateUserCmd.ExecuteNonQuery

        If rowsAff < 1 Then
            MsgBox("Unknown error related to updating your profile")
            Exit Sub
        Else
            MsgBox("Update successful. Returning to main menu")
            Server.Transfer("main_page.aspx")
        End If

    End Sub
End Class