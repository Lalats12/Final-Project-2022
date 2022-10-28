Public Class Admin_page
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btn_schools_Click(sender As Object, e As EventArgs) Handles btn_schools.Click
        Response.Redirect("AdminPage/AdminSchool.aspx")
    End Sub

    Protected Sub btn_booking_Click(sender As Object, e As EventArgs) Handles btn_booking.Click
        Response.Redirect("AdminPage/AdminBooking.aspx")
    End Sub

    Protected Sub btn_checkUsers_Click(sender As Object, e As EventArgs) Handles btn_checkUsers.Click
        Response.Redirect("AdminPage/AdminSchool.aspx")
    End Sub

    Protected Sub btn_courts_Click(sender As Object, e As EventArgs) Handles btn_courts.Click
        Response.Redirect("AdminPage/AdminCourts.aspx")
    End Sub
End Class