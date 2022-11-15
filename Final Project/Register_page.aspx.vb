Imports System.Data.SqlClient
Public Class Register_page
    Inherits System.Web.UI.Page
    Dim regexEmail As Regex = New Regex("^((([^<>()[\]\\.,;:\s@])+\.?([^!@#$%^&*()_+{}:<>?])+)|.*)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z]+\.))+[a-zA-Z]{2,})")
    Dim regexPass As Regex = New Regex("^(?=.*[0-9])(?=.*[A-Z])(?=.*[a-z])(?=.*[^\w\d\s:]).{8,64}")
    Dim regexNums As Regex = New Regex("^((?<malaysiacode>\(?\+?\d{2}\)?)? ?)(\d{2,3})[ -]?(\d{3})[ -]?(\d{4})$")
    Dim conn As SqlConnection
    Dim checkCmd As SqlCommand
    Dim registerCmd As SqlCommand

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim connStr As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\jpyea\source\repos\Final Project\Final Project\App_Data\badminton_database.mdf"";Integrated Security=True;Connect Timeout=30"
        conn = New SqlConnection(connStr)
        conn.Open()

        Dim checkSql As String = "SELECT user_id ,username, user_email FROM user_data WHERE username = @uname OR user_email = @ema"
        checkCmd = New SqlCommand(checkSql, conn)

        Dim registerSql As String = "INSERT INTO user_data(username, user_password, user_email, user_numbers) VALUES(@uname,@upass,@uemail,@unum)"
        registerCmd = New SqlCommand(registerSql, conn)

    End Sub

    Protected Sub btn_register_Click(sender As Object, e As EventArgs) Handles btn_register.Click
        Dim email As String = txt_email.Text.ToString
        Dim user As String = txt_username.Text.ToString
        Dim pass As String = txt_pass.Text.ToString
        Dim repass As String = txt_repass.Text.ToString
        Dim num As String = txt_num.Text.ToString

        If Not regexEmail.IsMatch(email) Then
            MsgBox("Error, Please input email again")
            Exit Sub
        End If
        If pass.Length < 8 Then
            MsgBox("Error, the password length should be more than 8")
            Exit Sub
        End If

        If Not regexPass.IsMatch(pass) Then
            MsgBox("Error, the password needs to have at least: One special character, One lowercase and uppercase character, and one number")
            Exit Sub
        End If

        If Not regexNums.IsMatch(num) Then
            MsgBox("Error, the numbers need to be malaysian numbers")
            Exit Sub
        End If

        If Not repass.CompareTo(pass) = 0 Then
            MsgBox("Error, the passwords are not the same")
            Exit Sub
        End If

        checkCmd.Parameters.Clear()
        checkCmd.Parameters.AddWithValue("uname", user)
        checkCmd.Parameters.AddWithValue("ema", email)

        Dim adapter As SqlDataAdapter = New SqlDataAdapter(checkCmd)
        Dim ds As DataSet = New DataSet()
        adapter.Fill(ds, "checkHas")

        Dim dt As DataTable = ds.Tables("checkHas")

        If dt.Rows.Count >= 1 Then
            MsgBox("Username/Email exist. Please use a different account")
            Exit Sub
        End If

        registerCmd.Parameters.Clear()
        registerCmd.Parameters.AddWithValue("uname", user)
        registerCmd.Parameters.AddWithValue("upass", pass)
        registerCmd.Parameters.AddWithValue("uemail", email)
        registerCmd.Parameters.AddWithValue("unum", num)

        Dim rowsAffected As Integer = registerCmd.ExecuteNonQuery()

        If rowsAffected > 0 Then
            Dim adapter2 As SqlDataAdapter = New SqlDataAdapter(checkCmd)
            Dim ds2 As DataSet = New DataSet()
            adapter.Fill(ds, "loginU")

            Dim dt2 As DataTable = ds.Tables("loginU")
            Dim usid As DataRow = dt2.Rows(0)
            MsgBox("User registration successful, welcome," + user)
            Session("UserID") = usid("user_id")
            Session("UserName") = user
            Server.Transfer("main_page.aspx")
        Else
            MsgBox("Somethings wrong")
        End If
    End Sub

    Protected Function CheckEmpty()
        If txt_username.Text = "" Or txt_repass.Text = "" Or txt_pass.Text = "" Or txt_num.Text = "" Or txt_email.Text = "" Then
            Return False
        End If
        Return True
    End Function

End Class