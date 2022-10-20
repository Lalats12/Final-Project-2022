Imports System.Data.SqlClient
Public Class HMRegister
    Inherits System.Web.UI.Page
    Dim conn As SqlConnection
    Dim checkHeadCmd As SqlCommand
    Dim getHeadCmd As SqlCommand
    Dim insertHeadCmd As SqlCommand

    Dim regexEmail As Regex = New Regex("^((([^<>()[\]\\.,;:\s@])+\.?([^!@#$%^&*()_+{}:<>?])+)|.*)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z]+\.))+[a-zA-Z]{2,})")
    Dim regexPass As Regex = New Regex("^(?=.*[0-9])(?=.*[A-Z])(?=.*[a-z])(?=.*[^\w\d\s:]).{8,64}")
    Dim regexVeri As Regex = New Regex("\d{6}")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strConn As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\jpyea\source\repos\Final Project\Final Project\App_Data\badminton_database.mdf"";Integrated Security=True"
        conn = New SqlConnection(strConn)
        conn.Open()

        Dim checkHeadSql As String = "SELECT hm_username, hm_email
                                      FROM AdminHead
                                      WHERE hm_username = @hun OR hm_email = @hem"
        checkHeadCmd = New SqlCommand(checkHeadSql, conn)


        Dim getHeadSql As String = "SELECT hm_id,hm_username, hm_email
                                      FROM AdminHead
                                      WHERE hm_username = @hun"
        getHeadCmd = New SqlCommand(getHeadSql, conn)

        Dim insertHeadSql As String = "INSERT INTO AdminHead(hm_username,hm_email,hm_phone,user_password,role,verify)
                                       VALUES(@hun,@hem,@hph,@hup, 'counciler', @veri)"
        insertHeadCmd = New SqlCommand(insertHeadSql, conn)


    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles btn_register.Click
        Dim uname As String = txt_username.Text
        Dim upass As String = txt_pass.Text
        Dim unum As String = txt_phoneNum.Text
        Dim uemail As String = txt_email.Text
        Dim uveri As String = txt_pin.Text

        If Not regexEmail.IsMatch(uemail) Then
            MsgBox("Invalid Email address, please try again")
            Exit Sub
        End If

        If Not regexPass.IsMatch(upass) Then
            MsgBox("Invalid Password, The password should contain" + vbCrLf + "One lowercase and uppercase letter" + vbCrLf + "One number" + vbCrLf + "One special character")
            Exit Sub
        End If

        If Not regexVeri.IsMatch(uveri) Then
            MsgBox("Invalid Pin numbers, enter 6 digits")
            Exit Sub
        End If

        checkHeadCmd.Parameters.Clear()
        checkHeadCmd.Parameters.AddWithValue("hun", uname)
        checkHeadCmd.Parameters.AddWithValue("hem", uemail)

        Dim adapter As SqlDataAdapter = New SqlDataAdapter(checkHeadCmd)
        Dim ds As DataSet = New DataSet()
        adapter.Fill(ds, "CheckUser")

        Dim dt As DataTable = ds.Tables("checkUser")
        If dt.Rows.Count > 1 Then
            MsgBox("Username/email found in use, please try a different username first")
            Exit Sub
        End If

        insertHeadCmd.Parameters.Clear()
        insertHeadCmd.Parameters.AddWithValue("hun", uname)
        insertHeadCmd.Parameters.AddWithValue("hem", uemail)
        insertHeadCmd.Parameters.AddWithValue("hph", unum)
        insertHeadCmd.Parameters.AddWithValue("hup", upass)
        insertHeadCmd.Parameters.AddWithValue("veri", uveri)

        Dim rowsAffected As Integer = insertHeadCmd.ExecuteNonQuery()

        If rowsAffected < 1 Then
            MsgBox("Sorry, an error occured")
        Else
            getHeadCmd.Parameters.Clear()
            getHeadCmd.Parameters.AddWithValue("hun", uname)

            Dim adapter2 As SqlDataAdapter = New SqlDataAdapter(getHeadCmd)
            Dim ds2 As DataSet = New DataSet()
            adapter2.Fill(ds2, "User")

            Dim dt2 As DataTable = ds2.Tables("User")
            Dim dr As DataRow = dt2.Rows(0)

            MsgBox("Success, welcome " + uname)
            HM_id = dr("hm_id")
            userName = uname
            Response.Redirect("ContratorPage.aspx")
        End If






    End Sub
End Class