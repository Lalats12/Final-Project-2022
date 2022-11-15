Imports System.Data.SqlClient
Public Class About_Us
    Inherits System.Web.UI.Page
    Dim conn As SqlConnection
    Dim submitFormCmd As SqlCommand

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim connStr As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\jpyea\source\repos\Final Project\Final Project\App_Data\badminton_database.mdf"";Integrated Security=True"
        conn = New SqlConnection(connStr)
        conn.Open()

        Dim submitFormSql As String = "INSERT INTO Responses(first_name, last_name,response_list) VALUES(@fn, @ln, @res)"
        submitFormCmd = New SqlCommand(submitFormSql, conn)

    End Sub

    Protected Sub btn_submit_Click(sender As Object, e As EventArgs) Handles btn_submit.Click
        Dim first As String = fname.Text
        Dim last As String = lname.Text
        Dim response As String = subject.Text

        If first.Count = 0 Or last.Count = 0 Or response.Count = 0 Then
            MsgBox("Your name/response must not be empty")
            Exit Sub
        End If

        If response.Count > 255 Then
            MsgBox("The max count is more than 255 characters. Please try again")
            Exit Sub
        End If

        submitFormCmd.Parameters.Clear()
        submitFormCmd.Parameters.AddWithValue("fn", first)
        submitFormCmd.Parameters.AddWithValue("ln", last)
        submitFormCmd.Parameters.AddWithValue("res", response)

        Dim rowsAff As Integer = submitFormCmd.ExecuteNonQuery
        If rowsAff < 1 Then
            MsgBox("Your message has not been reached, please try again")
        Else
            MsgBox("Your response is accepted")
        End If

    End Sub
End Class