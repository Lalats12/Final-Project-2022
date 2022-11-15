Imports System.Data.SqlClient
Public Class HMAdminLogin
    Inherits System.Web.UI.Page
    Dim conn As SqlConnection
    Dim checkUserCmd As SqlCommand

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strConn As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\jpyea\source\repos\Final Project\Final Project\App_Data\badminton_database.mdf"";Integrated Security=True"
        conn = New SqlConnection(strConn)

        Dim checkUserSql As String = "SELECT hm_username,hm_id,role,school_num
                                      FROM AdminHead
                                      WHERE hm_username = @hun AND user_password = @hup"
        checkUserCmd = New SqlCommand(checkUserSql, conn)

    End Sub


    Protected Sub btn_login_Click(sender As Object, e As EventArgs) Handles btn_login.Click
        Dim userName As String = txt_username.Text
        Dim userPass As String = txt_pass.Text
        checkUserCmd.Parameters.Clear()
        checkUserCmd.Parameters.AddWithValue("hun", userName)
        checkUserCmd.Parameters.AddWithValue("hup", userPass)

        Dim adapter As SqlDataAdapter = New SqlDataAdapter(checkUserCmd)
        Dim ds As DataSet = New DataSet()
        adapter.Fill(ds, "loginCheck")

        Dim dt As DataTable = ds.Tables("loginCheck")

        If (dt.Rows.Count = 0) Then
            MsgBox("Username/password is incorrect.")
        Else
            Dim dr As DataRow = dt.Rows(0)
            MsgBox("Welcome, " + userName)
            Session("IsAdmin") = True
            Server.Transfer("Admin_page.aspx")
        End If
    End Sub
End Class