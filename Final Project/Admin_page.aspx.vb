Imports System.Data.SqlClient
Public Class Admin_page
    Inherits System.Web.UI.Page
    Dim conn As SqlConnection
    Dim getResponseCmd As SqlCommand
    Dim getNewSchoolCmd As SqlCommand

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("IsAdmin") Is Nothing Then
            Server.Transfer("../Starting_Page.aspx")
        End If

        Dim strConn As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\jpyea\source\repos\Final Project\Final Project\App_Data\badminton_database.mdf"";Integrated Security=True"
        conn = New SqlConnection(strConn)
        conn.Open()

        Dim getResponseSql As String = "SELECT COUNT(*) as ""count"" FROM Responses"
        getResponseCmd = New SqlCommand(getResponseSql, conn)

        Dim getNewSchoolSql As String = "SELECT COUNT(*) as ""count"" FROM NewSchoolResponse"
        getNewSchoolCmd = New SqlCommand(getNewSchoolSql, conn)

        Dim adap As SqlDataAdapter = New SqlDataAdapter(getResponseCmd)
        Dim ds As DataSet = New DataSet
        adap.Fill(ds, "response")

        Dim dt As DataTable = ds.Tables("response")
        Dim dr As DataRow = dt.Rows(0)
        If Integer.Parse(dr("count").ToString) > 0 Then
            MsgBox("You have " + dr("count").ToString + " response(s) from users")
        End If

        adap = New SqlDataAdapter(getNewSchoolCmd)
        ds = New DataSet
        adap.Fill(ds, "responses")
        Dim dt2 As DataTable = ds.Tables("responses")
        Dim dr2 As DataRow = dt2.Rows(0)
        If Integer.Parse(dr2("count").ToString) > 0 Then
            MsgBox("You have " + dr2("count").ToString + " proporsal(s) from Headmasters")
        End If

    End Sub

    Protected Sub btn_schools_Click(sender As Object, e As EventArgs) Handles btn_schools.Click
        Server.Transfer("AdminPage/AdminSchool.aspx")
    End Sub

    Protected Sub btn_booking_Click(sender As Object, e As EventArgs) Handles btn_booking.Click
        Server.Transfer("AdminPage/AdminBooking.aspx")
    End Sub

    Protected Sub btn_checkUsers_Click(sender As Object, e As EventArgs) Handles btn_checkUsers.Click
        Server.Transfer("AdminPage/AdminSchool.aspx")
    End Sub

    Protected Sub btn_courts_Click(sender As Object, e As EventArgs) Handles btn_courts.Click
        Server.Transfer("AdminPage/AdminCourts.aspx")
    End Sub

    Protected Sub btn_feedback_Click(sender As Object, e As EventArgs) Handles btn_feedback.Click
        Server.Transfer("AdminPage/AdminResponse.aspx")
    End Sub

    Protected Sub btn_newSchool_Click(sender As Object, e As EventArgs) Handles btn_newSchool.Click
        Server.Transfer("AdminPage/AdminNewSchool.aspx")
    End Sub
End Class