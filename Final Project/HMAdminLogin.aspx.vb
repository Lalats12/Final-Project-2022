Public Class HMAdminLogin
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btn_resigter_Click(sender As Object, e As EventArgs) Handles btn_resigter.Click
        Response.Redirect("HMRegister.aspx")
    End Sub
End Class