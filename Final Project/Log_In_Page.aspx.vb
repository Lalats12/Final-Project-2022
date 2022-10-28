Imports System.Data.SqlClient

Public Class Log_In_Page
    Inherits System.Web.UI.Page
    Dim conn As SqlConnection
    Dim loginCmd As SqlCommand
    Dim pass As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim connStr As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\jpyea\source\repos\Final Project\Final Project\App_Data\badminton_database.mdf"";Integrated Security=True"
        conn = New SqlConnection(connStr)
        conn.Open()

        Dim loginSql As String = "SELECT username, user_password,user_id FROM user_data
                                  WHERE username = @uname AND user_password = @upass"
        loginCmd = New SqlCommand(loginSql, conn)


    End Sub

    Protected Sub btn_login_Click(sender As Object, e As EventArgs) Handles btn_login.Click
        Dim userName As String = txt_username.Text
        Dim userPass As String = txt_password.Text
        loginCmd.Parameters.Clear()
        loginCmd.Parameters.AddWithValue("uname", userName)
        loginCmd.Parameters.AddWithValue("upass", userPass)

        Dim adapter As SqlDataAdapter = New SqlDataAdapter(loginCmd)
        Dim ds As DataSet = New DataSet()
        adapter.Fill(ds, "loginCheck")

        Dim dt As DataTable = ds.Tables("loginCheck")

        If (dt.Rows.Count = 0) Then
            MsgBox("Username/password is incorrect.")
        Else
            Dim dr As DataRow = dt.Rows(0)
            MsgBox("Welcome, " + userName)
            PubVar.userId = dr("user_id")
            PubVar.userName = dr("userName")
            Response.Redirect("main_page.aspx")
        End If
    End Sub

    Protected Sub btn_register_Click(sender As Object, e As EventArgs) Handles btn_register.Click
        Response.Redirect("Register_page.aspx")
    End Sub


End Class