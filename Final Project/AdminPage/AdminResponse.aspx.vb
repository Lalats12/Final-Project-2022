Imports System.Data.SqlClient
Public Class AdminResponse
    Inherits System.Web.UI.Page
    Dim conn As SqlConnection
    Dim listResponseCmd As SqlCommand
    Dim loadResponseCmd As SqlCommand
    Dim deleteResponseCmd As SqlCommand

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("IsAdmin") Is Nothing Then
            Server.Transfer("../Starting_Page.aspx")
        End If
        Dim connStr As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\jpyea\source\repos\Final Project\Final Project\App_Data\badminton_database.mdf"";Integrated Security=True"
        conn = New SqlConnection(connStr)
        conn.Open()

        Dim listResponseSql As String = "SELECT * FROM Responses"
        listResponseCmd = New SqlCommand(listResponseSql, conn)

        Dim loadResponseSql As String = "SELECT * FROM Responses WHERE response_id = @rid"
        loadResponseCmd = New SqlCommand(loadResponseSql, conn)

        Dim deleteResponseSql As String = "DELETE FROM Responses WHERE response_id = @id"
        deleteResponseCmd = New SqlCommand(deleteResponseSql, conn)

        Dim adap As SqlDataAdapter = New SqlDataAdapter(listResponseCmd)
        Dim ds As DataSet = New DataSet
        adap.Fill(ds, "list")
        Dim dt As DataTable = ds.Tables("list")

        If dt.Rows.Count < 1 Then
            MsgBox("Either empty or an error at somewhere")
        Else
            If Not IsPostBack Then
                drp_idlst.Items.Clear()
                drp_idlst.Items.Add("(Select)")
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim dr As DataRow = dt.Rows(i)
                    Dim id As Integer = dr("response_id")
                    drp_idlst.Items.Add("Response id: " + id.ToString)
                    drp_idlst.Items.Item(i + 1).Value = id
                Next
            End If
        End If

    End Sub

    Protected Sub drp_idlst_SelectedIndexChanged(sender As Object, e As EventArgs) Handles drp_idlst.SelectedIndexChanged
        loadResponseCmd.Parameters.Clear()
        loadResponseCmd.Parameters.AddWithValue("rid", drp_idlst.SelectedValue)

        Dim adap As SqlDataAdapter = New SqlDataAdapter(loadResponseCmd)
        Dim ds As DataSet = New DataSet
        adap.Fill(ds, "load")
        Dim dt As DataTable = ds.Tables("load")

        If dt.Rows.Count < 1 Then
            MsgBox("Error when loading the data")
        Else
            Dim dr As DataRow = dt.Rows(0)
            Dim first As String = dr("first_name")
            Dim last As String = dr("last_name")
            Dim res As String = dr("response_list")

            txt_name.Text = first + " " + last
            txt_response.Text = res
        End If
    End Sub

    Protected Sub btn_delete_Click(sender As Object, e As EventArgs) Handles btn_delete.Click
        If drp_idlst.SelectedIndex = 0 Then
            MsgBox("Error, the selected index must be changed to something else")
        End If
        deleteResponseCmd.Parameters.Clear()
        deleteResponseCmd.Parameters.AddWithValue("id", drp_idlst.SelectedValue)

        Dim rowsAff As Integer = deleteResponseCmd.ExecuteNonQuery
        If rowsAff < 1 Then
            MsgBox("Unknown error related to rowsAff")
        Else
            MsgBox("Response successfully deleted.")
            Server.Transfer("AdminResponse.aspx")
        End If
    End Sub

    Protected Sub btn_main_menu_Click(sender As Object, e As EventArgs) Handles btn_main_menu.Click
        Server.Transfer("../Admin_page.aspx")
    End Sub
End Class