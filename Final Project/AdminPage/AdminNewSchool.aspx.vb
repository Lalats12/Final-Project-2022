Imports System.Data.SqlClient
Public Class AdminNewSchool
    Inherits System.Web.UI.Page
    Dim conn As SqlConnection
    Dim loadListCmd As SqlCommand
    Dim loadOneCmd As SqlCommand
    Dim removeProporsalCmd As SqlCommand

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("IsAdmin") Is Nothing Then
            Server.Transfer("../Starting_Page.aspx")
        End If
        Dim connStr As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\jpyea\source\repos\Final Project\Final Project\App_Data\badminton_database.mdf"";Integrated Security=True"
        conn = New SqlConnection(connStr)
        conn.Open()

        Dim loadListSql As String = "SELECT responseSchool_id, fullName
                                     FROM NewSchoolResponse"
        loadListCmd = New SqlCommand(loadListSql, conn)

        Dim loadOneSql As String = "SELECT *
                                    FROM NewSchoolResponse
                                    WHERE responseSchool_id = @sci"
        loadOneCmd = New SqlCommand(loadOneSql, conn)

        Dim removeProporsalSql As String = "DELETE FROM NewSchoolResponse WHERE responseSchool_id = @id"
        removeProporsalCmd = New SqlCommand(removeProporsalSql, conn)

        Dim adap As SqlDataAdapter = New SqlDataAdapter(loadListCmd)
        Dim ds As DataSet = New DataSet
        adap.Fill(ds, "list")
        Dim dt As DataTable = ds.Tables("list")

        If dt.Rows.Count < 1 Then
            MsgBox("Error related to fetching data from dt or its empty")
            Exit Sub
        Else
            If Not IsPostBack Then
                drp_proporsalID.Items.Clear()
                drp_proporsalID.Items.Add("(Select)")
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim dr As DataRow = dt.Rows(i)
                    Dim strJoin As String = "ID: " + dr("responseSchool_id").ToString + " Name:" + dr("fullName")
                    drp_proporsalID.Items.Add(strJoin)
                    drp_proporsalID.Items.Item(i + 1).Value = dr("responseSchool_id").ToString
                Next
            End If
        End If

    End Sub

    Protected Sub btn_menu_Click(sender As Object, e As EventArgs) Handles btn_menu.Click
        Server.Transfer("../Admin_page.aspx")
    End Sub

    Protected Sub drp_proporsalID_SelectedIndexChanged(sender As Object, e As EventArgs) Handles drp_proporsalID.SelectedIndexChanged
        If drp_proporsalID.SelectedIndex = 0 Then
            Exit Sub
        End If
        Dim value As Integer = drp_proporsalID.SelectedValue

        loadOneCmd.Parameters.Clear()
        loadOneCmd.Parameters.AddWithValue("sci", value)

        Dim adap As SqlDataAdapter = New SqlDataAdapter(loadOneCmd)
        Dim ds As DataSet = New DataSet
        adap.Fill(ds, "one")
        Dim dt As DataTable = ds.Tables("one")

        If dt.Rows.Count < 1 Then
            MsgBox("Error related when fetching a single data")
            Exit Sub
        Else
            Dim dr As DataRow = dt.Rows(0)
            Dim name As String = dr("fullName")
            Dim num As String = dr("HMNumber")
            Dim email As String = dr("HMEmail")
            Dim sch As String = dr("schoolName")
            Dim add As String = dr("schoolAddress")
            Dim court As String = dr("schoolCourts")
            Dim open As String = dr("time_open")
            Dim close As String = dr("time_close")
            txt_name.Text = name
            txt_schoolName.Text = sch
            txt_num.Text = num
            txt_email.Text = email
            txt_schoolAdd.Text = add
            txt_courtsNum.Text = court
            drp_start_time.Text = open
            drp_close_time.Text = close
        End If

    End Sub

    Protected Sub btn_delete_Click(sender As Object, e As EventArgs) Handles btn_delete.Click
        If drp_proporsalID.SelectedIndex = 0 Then
            MsgBox("Error, you must select an item")
            Exit Sub
        End If

        removeProporsalCmd.Parameters.Clear()
        removeProporsalCmd.Parameters.AddWithValue("id", drp_proporsalID.SelectedValue)

        Dim rowsAff As Integer = removeProporsalCmd.ExecuteNonQuery
        If rowsAff < 1 Then
            MsgBox("Error related to removing data")
        Else
            MsgBox("Successfully deleted.")
            Server.Transfer("AdminNewSchool.aspx")
        End If
    End Sub
End Class