Imports System.Data.SqlClient
Public Class ForUsers
    Inherits System.Web.UI.Page
    Dim conn As SqlConnection
    Dim uploadSchoolCmd As SqlCommand
    Dim regexNums As Regex = New Regex("^((?<malaysiacode>\(?\+?\d{2}\)?)? ?)(\d{2,3})[ -]?(\d{3})[ -]?(\d{4})$")
    Dim regexEmail As Regex = New Regex("^((([^<>()[\]\\.,;:\s@])+\.?([^!@#$%^&*()_+{}:<>?])+)|.*)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z]+\.))+[a-zA-Z]{2,})")


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strConn As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\jpyea\source\repos\Final Project\Final Project\App_Data\badminton_database.mdf"";Integrated Security=True"
        conn = New SqlConnection(strConn)
        conn.Open()

        Dim uploadSchoolSql As String = "INSERT INTO NewSchoolResponse(fullName, HMNumber, HMEmail,schoolName, schoolAddress, schoolCourts,time_open, time_close)
                                         VALUES(@ful, @num, @ema, @sch, @add, @cor,@open, @clo)"
        uploadSchoolCmd = New SqlCommand(uploadSchoolSql, conn)
    End Sub

    Protected Sub btn_enter_Click(sender As Object, e As EventArgs) Handles btn_enter.Click
        If Not checkEmpty() Then
            MsgBox("Error, there are empty spaces, please fill them")
            Exit Sub
        End If

        If txt_code.Text <> "Rt44#4$" Then
            MsgBox("Incorrect codes. You may not have received the codes")
            Exit Sub
        End If
        If txt_schoolAdd.Text.Count < 8 Or txt_schoolAdd.Text.Count > 255 Then
            MsgBox("The address is either too short or too long")
            Exit Sub
        End If

        If txt_name.Text.Count < 5 Or txt_name.Text.Count > 96 Then
            MsgBox("The name is either too short or too long")
            Exit Sub
        End If

        If Integer.Parse(drp_start_time.Text) >= Integer.Parse(drp_close_time.Text) Then
            MsgBox("The end time is earlier than the start time")
            Exit Sub
        End If

        If Not regexEmail.IsMatch(txt_email.Text) Then
            MsgBox("The email is not suitable")
            Exit Sub
        End If

        If Not regexNums.IsMatch(txt_nums.Text) Then
            MsgBox("The numbers must be malaysian numbers")
            Exit Sub
        End If

        uploadSchoolCmd.Parameters.Clear()
        uploadSchoolCmd.Parameters.AddWithValue("ful", txt_name.Text)
        uploadSchoolCmd.Parameters.AddWithValue("num", txt_nums.Text)
        uploadSchoolCmd.Parameters.AddWithValue("ema", txt_email.Text)
        uploadSchoolCmd.Parameters.AddWithValue("sch", txt_schoolName.Text)
        uploadSchoolCmd.Parameters.AddWithValue("add", txt_schoolAdd.Text)
        uploadSchoolCmd.Parameters.AddWithValue("open", drp_start_time.Text)
        uploadSchoolCmd.Parameters.AddWithValue("cor", txt_courtsNum.Text)
        uploadSchoolCmd.Parameters.AddWithValue("clo", drp_close_time.Text)

        Dim rowsAff As Integer = uploadSchoolCmd.ExecuteNonQuery

        If rowsAff < 1 Then
            MsgBox("Error, the message is not recieved from the user")
            Exit Sub
        Else
            MsgBox("The message is received. We'll contact you later")
            ClearAll()
        End If
    End Sub

    Private Sub ClearAll()
        txt_name.Text = ""
        txt_schoolAdd.Text = ""
        txt_schoolName.Text = ""
        txt_courtsNum.Text = ""
        txt_code.Text = ""
        txt_email.Text = ""
        txt_nums.Text = ""
        drp_close_time.Text = "00"
        drp_start_time.Text = "00"
    End Sub

    Private Function checkEmpty() As Boolean
        If txt_code.Text = "" Or txt_courtsNum.Text = "" Or
           txt_name.Text = "" Or txt_schoolAdd.Text = "" Or
           txt_schoolName.Text = "" Then
            Return False
        End If
        Return True
    End Function
End Class