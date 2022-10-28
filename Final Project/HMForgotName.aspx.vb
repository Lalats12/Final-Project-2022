Imports System.Data.SqlClient
Public Class HMForgotName
    Inherits System.Web.UI.Page
    Dim conn As SqlConnection
    Dim retriveUserCmd As SqlCommand
    Dim verifyUserCmd As SqlCommand

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim connStr As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\jpyea\source\repos\Final Project\Final Project\App_Data\badminton_database.mdf"";Integrated Security=True;Connect Timeout=30"
        conn = New SqlConnection(connStr)
        conn.Open()

        Dim retriveUserSql As String = "SELECT hm_username
                                        FROM AdminHead
                                        WHERE hm_phone = @hph AND hm_email = @em"
        retriveUserCmd.Parameters.AddWithValue(retriveUserSql, conn)

        Dim verifyUserSql As String = "SELECT verify
                                       FROM AdminHead
                                       WHERE hm_email = @hem AND verify = @veri"
        verifyUserCmd.Parameters.AddWithValue(verifyUserSql, conn)

    End Sub

    Protected Sub btn_menu_Click(sender As Object, e As EventArgs) Handles btn_menu.Click
        Response.Redirect("HMAdminLogin.aspx")
    End Sub

    Protected Sub btn_send_Click(sender As Object, e As EventArgs) Handles btn_send.Click
        If Not VerifyUser() Then
            MsgBox("Your verification code does not match")
            Exit Sub
        End If

        retriveUserCmd.Parameters.Clear()
        retriveUserCmd.Parameters.AddWithValue("hph", txt_phone.Text)
        retriveUserCmd.Parameters.AddWithValue("em", txt_email.Text)

        Dim adap As SqlDataAdapter = New SqlDataAdapter(retriveUserCmd)
        Dim ds As DataSet = New DataSet
        adap.Fill(ds, "user")
        Dim dt As DataTable = ds.Tables("user")

        If dt.Rows.Count < 1 Then
            MsgBox("Error, There is no username with the entered data")
            Exit Sub
        Else
            MsgBox("Sending the username to your email")
        End If

    End Sub

    Protected Function VerifyUser()
        Dim veri As String = InputBox("Enter your numbers", "Pin verification")

        verifyUserCmd.Parameters.Clear()
        verifyUserCmd.Parameters.AddWithValue("hem", txt_email.Text)

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