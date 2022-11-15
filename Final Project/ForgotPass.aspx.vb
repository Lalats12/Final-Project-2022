Imports System.Data.SqlClient
Public Class ForgotPass
    Inherits System.Web.UI.Page
    Dim conn As SqlConnection
    Dim findUserCmd As SqlCommand

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strConn As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\jpyea\source\repos\Final Project\Final Project\App_Data\badminton_database.mdf"";Integrated Security=True"
        conn = New SqlConnection(strConn)
        conn.Open()

        Dim findUserSql As String = "SELECT user_password
                                     FROM user_data
                                     WHERE username = @un AND user_email = @ue"
        findUserCmd = New SqlCommand(findUserSql, conn)

    End Sub

    Protected Sub btn_reset_Click(sender As Object, e As EventArgs) Handles btn_reset.Click
        findUserCmd.Parameters.Clear()
        findUserCmd.Parameters.AddWithValue("un", txt_user.Text)
        findUserCmd.Parameters.AddWithValue("ue", txt_email.Text)

        Dim adaptor As SqlDataAdapter = New SqlDataAdapter(findUserCmd)
        Dim ds As DataSet = New DataSet()
        adaptor.Fill(ds, "userNameFind")

        Dim dt As DataTable = ds.Tables("userNameFind")
        If dt.Rows.Count < 1 Then
            MsgBox("Username/Email not found, please try again")
        Else
            Dim dr As DataRow = dt.Rows(0)
            MsgBox("Username Found! Password: " + dr("user_password"))
        End If

    End Sub

    Protected Sub btn_main_menu_Click(sender As Object, e As EventArgs) Handles btn_main_menu.Click
        Server.Transfer("Log_In_Page.aspx")
    End Sub
End Class