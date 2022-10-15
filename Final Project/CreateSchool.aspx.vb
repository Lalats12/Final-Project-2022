Imports System.Data.SqlClient
Public Class CreateSchool
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btn_courts_Click(sender As Object, e As EventArgs) Handles btn_courts.Click
        Response.Redirect("ManageCourts.aspx")
    End Sub
End Class