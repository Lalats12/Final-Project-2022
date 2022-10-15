Imports System.Data.SqlClient
Public Class ForgotUser
    Inherits System.Web.UI.Page
    Dim conn As SqlConnection
    Dim findUserCmd As SqlCommand

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strConn As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\jpyea\source\repos\Final Project\Final Project\App_Data\badminton_database.mdf"";Integrated Security=True"
        conn = New SqlConnection(strConn)
        conn.Open()

        Dim findUserSql As String = "SELECT username
                                     FROM user_data
                                     WHERE user_email = @ue AND user_numbers = @up"
        findUserCmd = New SqlCommand(findUserSql, conn)

    End Sub

    Protected Sub btn_find_Click(sender As Object, e As EventArgs) Handles btn_find.Click
        findUserCmd.Parameters.Clear()
        findUserCmd.Parameters.AddWithValue("ue", txt_email.Text)
        findUserCmd.Parameters.AddWithValue("up", txt_numbers.Text)

        Dim adapter As SqlDataAdapter = New SqlDataAdapter(findUserCmd)
        Dim ds As DataSet = New DataSet
        adapter.Fill(ds, "FindUser")

        Dim dt As DataTable = ds.Tables("FindUser")

        If dt.Rows.Count < 1 Then
            MsgBox("Unable to find your username")
        Else
            Dim dr As DataRow = dt.Rows(0)
            MsgBox("Found your username! Username:" + dr("username"))
        End If

    End Sub

    Protected Sub btn_return_Click(sender As Object, e As EventArgs) Handles btn_return.Click
        Response.Redirect("Log_In_Page.aspx")
    End Sub
End Class