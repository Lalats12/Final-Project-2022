Public Class Starting_Page
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Server.Transfer("Log_in_Page.aspx")
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim num As String = InputBox("The codes for Logging in the page")

        If num.CompareTo("A@is3mf") <> 0 Then
            MsgBox("Error, input denied")
        End If

        Server.Transfer("HMAdminLogin.aspx")
    End Sub
End Class