Imports System.Data.SqlClient
Public Class ContratorPage
    Inherits System.Web.UI.Page
    Dim conn As SqlConnection
    Dim loadHMSchoolCmd As SqlCommand

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim connStr As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\jpyea\source\repos\Final Project\Final Project\App_Data\badminton_database.mdf"";Integrated Security=True"
        conn = New SqlConnection(connStr)
        conn.Open()

        Dim loadHMSchoolSql As String = ""

    End Sub

End Class