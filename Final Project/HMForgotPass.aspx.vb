Imports System.Data.SqlClient
Public Class HMForgotPass
    Inherits System.Web.UI.Page
    Dim conn As SqlConnection
    Dim verifyUserCmd As SqlCommand
    Dim findPasswordCmd As SqlCommand

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim connStr As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\jpyea\source\repos\Final Project\Final Project\App_Data\badminton_database.mdf"";Integrated Security=True;Connect Timeout=30"
        conn = New SqlConnection(connStr)
        conn.Open()

        Dim verifyUserSql As String = "SELECT hm_id, verify
                                       FROM AdminHead
                                       WHERE hm_username = @hun"
        verifyUserCmd = New SqlCommand(verifyUserSql, conn)

        Dim findPasswordSql As String = "SELECT user_password
                                         FROM AdminHead
                                         WHERE hm_username = @hun AND hm_email = @hem"
        findPasswordCmd = New SqlCommand(findPasswordSql, conn)

    End Sub

    Protected Sub btn_send_Click(sender As Object, e As EventArgs) Handles btn_send.Click
        If txt_username.Text.CompareTo("Admin") = 0 Then
            MsgBox("Nice try")
            Exit Sub
        End If
        If Not VerifyUser() Then
            MsgBox("Error, The verification failed")
            Exit Sub
        End If

        Dim name As String = txt_username.Text
        Dim email As String = txt_email.Text

        findPasswordCmd.Parameters.Clear()
        findPasswordCmd.Parameters.AddWithValue("hun", name)
        findPasswordCmd.Parameters.AddWithValue("hem", email)

        Dim adap As SqlDataAdapter = New SqlDataAdapter(findPasswordCmd)
        Dim ds As DataSet = New DataSet
        adap.Fill(ds, "user")
        Dim dt As DataTable = ds.Tables("user")

        If dt.Rows.Count < 1 Then
            MsgBox("Error, Username/email not found")
        Else
            MsgBox("An email has been sent to you")
        End If
    End Sub

    Protected Sub btn_menu_Click(sender As Object, e As EventArgs) Handles btn_menu.Click
        Response.Redirect("HMAdminLogin.aspx")
    End Sub

    Protected Function VerifyUser()
        Dim veri As String = InputBox("Enter your numbers", "Pin verification")

        verifyUserCmd.Parameters.Clear()
        verifyUserCmd.Parameters.AddWithValue("hun", txt_username.Text)

        Dim adapter As SqlDataAdapter = New SqlDataAdapter(verifyUserCmd)
        Dim ds As DataSet = New DataSet
        adapter.Fill(ds, "user")

        Dim dt As DataTable = ds.Tables("user")

        If dt.Rows.Count < 1 Then
            MsgBox("Error detected, please try again")
        Else
            Dim dr As DataRow = dt.Rows(0)
            If veri.CompareTo(dr("verify")) = 0 Then
                Return True
            End If
        End If

        Return False
    End Function
End Class