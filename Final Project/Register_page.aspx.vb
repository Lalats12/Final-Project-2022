Public Class Register_page
    Inherits System.Web.UI.Page
    Dim regex As Regex = New Regex("^(([^<>()[\]\\.,;:\s@]+(\.[^<>()[\]\\.,;:\s@]+)*)|(.+))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$")
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btn_register_Click(sender As Object, e As EventArgs) Handles btn_register.Click
        Dim email As String = txt_email.Text.ToString
        If Not regex.IsMatch(email) Then
            MsgBox("Error, Please input email again")
            Exit Sub
        End If
    End Sub
End Class