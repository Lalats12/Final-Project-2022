Imports System.Data.SqlClient
Public Class AdminSchool
    Inherits System.Web.UI.Page
    Dim conn As SqlConnection
    Dim loadSchoolsCmd As SqlCommand
    Dim loadOneSchoolCmd As SqlCommand
    Dim loadTagSchoolCmd As SqlCommand
    Dim updateSchoolCmd As SqlCommand
    Dim updateLocationCmd As SqlCommand
    Dim checkHMSchoolCmd As SqlCommand
    Dim deleteSchoolCmd As SqlCommand
    Dim deleteLocationCmd As SqlCommand
    Dim updateHMSchoolCmd As SqlCommand

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim connStr As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\jpyea\source\repos\Final Project\Final Project\App_Data\badminton_database.mdf"";Integrated Security=True"
        conn = New SqlConnection(connStr)
        conn.Open()

        Dim loadSchoolsSql As String = "SELECT * FROM Venues INNER JOIN Locations ON Locations.location_id = Venues.school_location"
        loadSchoolsCmd = New SqlCommand(loadSchoolsSql, conn)

        Dim loadOneSchoolSql As String = "SELECT * 
                                          FROM Venues INNER JOIN Locations ON Locations.location_id = Venues.school_location
                                          INNER JOIN AdminHead ON AdminHead.school_num = Venues.venue_id
                                          WHERE venue_id = @id"
        loadOneSchoolCmd = New SqlCommand(loadOneSchoolSql, conn)

        Dim loadTagSchoolSql As String = "SELECT * FROM Venues 
                                          INNER JOIN Locations ON Locations.location_id = Venues.school_location
                                          INNER JOIN AdminHead ON AdminHead.school_num = Venues.venue_id
                                          WHERE school_tag = @tag"
        loadTagSchoolCmd = New SqlCommand(loadTagSchoolSql, conn)

        '
        Dim updateSchoolSql As String = "UPDATE Venues SET school_name = @scnam, school_tag = @sctag WHERE venue_id = @id"
        updateSchoolCmd = New SqlCommand(updateSchoolSql, conn)

        Dim checkHMSchoolSql As String = "SELECT * FROM Venues 
                                          INNER JOIN Locations ON Locations.location_id = Venues.school_location
                                          INNER JOIN AdminHead ON AdminHead.school_num = Venues.venue_id
                                          WHERE school_name = @sch OR school_tag = @tag OR school_address = @add"
        checkHMSchoolCmd = New SqlCommand(checkHMSchoolSql, conn)

        Dim updateLocationSql As String = "UPDATE Locations SET school_address = @schadd WHERE location_id = @id"
        updateLocationCmd = New SqlCommand(updateLocationSql, conn)

        Dim deleteSchoolSql As String = "DELETE Venues WHERE venue_id = @id"
        deleteSchoolCmd = New SqlCommand(deleteSchoolSql, conn)

        Dim deleteLocationSql As String = "DELETE Locations WHERE location_id = @id"
        deleteLocationCmd = New SqlCommand(deleteLocationSql, conn)

        Dim updateHMSchoolSql As String = "UPDATE AdminHead SET school_num = NULL WHERE hm_id = @hid"
        updateHMSchoolCmd = New SqlCommand(updateHMSchoolSql, conn)

    End Sub

    Protected Sub btn_allSchools_Click(sender As Object, e As EventArgs) Handles btn_allSchools.Click
        Dim adap As SqlDataAdapter = New SqlDataAdapter(loadSchoolsCmd)
        Dim ds As DataSet = New DataSet
        adap.Fill(ds, "schools")
        Dim dt As DataTable = ds.Tables("schools")

        If dt.Rows.Count < 1 Then
            MsgBox("Error when trying to load schools")
            Exit Sub
        Else
            lst_Schools.Items.Clear()

            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dr As DataRow = dt.Rows(i)
                Dim id As Integer = dr("venue_id")
                Dim loc As String = dr("school_address")
                Dim tag As String = dr("school_tag")
                Dim name As String = dr("school_name")
                Dim strJoin As String = "ID: " + id.ToString + " (" + tag + ") Name:" + name
                lst_Schools.Items.Add(strJoin)
                lst_Schools.Items.Item(i).Value = id
            Next
        End If
        pnl_listSchool.Visible = True
    End Sub

    Protected Sub btn_byID_Click(sender As Object, e As EventArgs) Handles btn_byID.Click
        pnl_SearchOne.Visible = True
    End Sub

    Protected Sub btn_searchTag_Click(sender As Object, e As EventArgs) Handles btn_searchTag.Click
        pnl_tag.Visible = True
    End Sub

    Protected Sub btn_searchID_Click(sender As Object, e As EventArgs) Handles btn_searchID.Click
        loadOneSchoolCmd.Parameters.Clear()
        loadOneSchoolCmd.Parameters.AddWithValue("id", txt_searchID.Text)

        Dim adap As SqlDataAdapter = New SqlDataAdapter(loadOneSchoolCmd)
        Dim ds As DataSet = New DataSet
        adap.Fill(ds, "school")
        Dim dt As DataTable = ds.Tables("school")

        If dt.Rows.Count < 1 Then
            MsgBox("Error when getting data from database")
        Else
            Dim dr As DataRow = dt.Rows(0)
            Dim hm As String = dr("hm_id")
            Dim name As String = dr("school_name")
            Dim tag As String = dr("school_tag")
            Dim locID As Integer = dr("school_location")
            Dim loc As String = dr("school_address")

            txt_searchID.Enabled = False
            txt_ID.Text = txt_searchID.Text
            txt_IDHM.Text = hm
            txt_name.Text = name
            txt_loc.Text = loc
            lbl_locId.Text = locID
            txt_tag.Text = tag
            Panel1.Visible = True
        End If

    End Sub

    Protected Sub btn_displaySchool_Click(sender As Object, e As EventArgs) Handles btn_displaySchool.Click
        If lst_Schools.SelectedIndex < 0 Then
            MsgBox("Unable to find the school, please choose one")
            Exit Sub
        End If

        Dim id As Integer = Integer.Parse(lst_Schools.SelectedValue)

        loadOneSchoolCmd.Parameters.Clear()
        loadOneSchoolCmd.Parameters.AddWithValue("id", id)

        Dim adap As SqlDataAdapter = New SqlDataAdapter(loadOneSchoolCmd)
        Dim ds As DataSet = New DataSet
        adap.Fill(ds, "school")
        Dim dt As DataTable = ds.Tables("school")

        If dt.Rows.Count < 1 Then
            MsgBox("Error when getting data from database")
        Else
            Dim dr As DataRow = dt.Rows(0)
            Dim sid As Integer = dr("venue_id")
            Dim hm As String = dr("hm_id")
            Dim name As String = dr("school_name")
            Dim locID As Integer = dr("school_location")
            Dim tag As String = dr("school_tag")
            Dim loc As String = dr("school_address")

            txt_searchID.Enabled = False
            txt_ID.Text = sid
            txt_IDHM.Text = hm
            txt_name.Text = name
            txt_loc.Text = loc
            lbl_locId.Text = locID
            txt_tag.Text = tag
            Panel1.Visible = True
        End If

    End Sub

    Protected Sub btn_byTag_Click(sender As Object, e As EventArgs) Handles btn_byTag.Click
        Dim tag As String = txt_searchTag.Text

        loadTagSchoolCmd.Parameters.Clear()
        loadTagSchoolCmd.Parameters.AddWithValue("tag", tag)

        Dim adap As SqlDataAdapter = New SqlDataAdapter(loadTagSchoolCmd)
        Dim ds As DataSet = New DataSet
        adap.Fill(ds, "tag")
        Dim dt As DataTable = ds.Tables("tag")

        If dt.Rows.Count < 1 Then
            MsgBox("Error when getting data from database")
        Else
            Dim dr As DataRow = dt.Rows(0)
            Dim sid As Integer = dr("venue_id")
            Dim hm As String = dr("hm_id")
            Dim name As String = dr("school_name")
            Dim tag2 As String = dr("school_tag")
            Dim locID As Integer = dr("school_location")
            Dim loc As String = dr("school_address")

            txt_searchID.Enabled = False
            txt_ID.Text = sid
            txt_IDHM.Text = hm
            txt_name.Text = name
            txt_loc.Text = loc
            lbl_locId.Text = locID
            txt_tag.Text = tag2
            Panel1.Visible = True
        End If

    End Sub

    Protected Sub btn_update_Click(sender As Object, e As EventArgs) Handles btn_update.Click
        If txt_name.Text.Length <= 5 Then
            MsgBox("School's word count should be more than 5")
            Exit Sub
        End If

        If txt_loc.Text.Length <= 8 Then
            MsgBox("Location's word count should be more than 8")
            Exit Sub
        End If

        checkHMSchoolCmd.Parameters.Clear()
        checkHMSchoolCmd.Parameters.AddWithValue("sch", txt_name.Text)
        checkHMSchoolCmd.Parameters.AddWithValue("tag", txt_tag.Text)
        checkHMSchoolCmd.Parameters.AddWithValue("add", txt_loc.Text)

        Dim adap As SqlDataAdapter = New SqlDataAdapter(checkHMSchoolCmd)
        Dim ds As DataSet = New DataSet
        adap.Fill(ds, "check")
        Dim dt As DataTable = ds.Tables("check")

        If dt.Rows.Count > 0 Then
            MsgBox("Error, there is a same name/tag/address")
            Exit Sub
        End If

        updateLocationCmd.Parameters.Clear()
        updateLocationCmd.Parameters.AddWithValue("id", lbl_locId.Text)
        updateLocationCmd.Parameters.AddWithValue("schAdd", txt_loc.Text)

        updateSchoolCmd.Parameters.Clear()
        updateSchoolCmd.Parameters.AddWithValue("scnam", txt_name.Text)
        updateSchoolCmd.Parameters.AddWithValue("sctag", txt_tag.Text)
        updateSchoolCmd.Parameters.AddWithValue("id", txt_ID.Text)

        Dim rowsAff As Integer = updateLocationCmd.ExecuteNonQuery

        If rowsAff < 1 Then
            MsgBox("Error, Somethings wrong")
        Else
            Dim rowsAff2 As Integer = updateSchoolCmd.ExecuteNonQuery
            If rowsAff2 < 1 Then
                MsgBox("Error when uploading school data")
            Else
                MsgBox("Success updating, refreshing")
                Response.Redirect("AdminSchool.aspx")
            End If
        End If
    End Sub

    Protected Sub btn_delete_Click(sender As Object, e As EventArgs) Handles btn_delete.Click
        Dim choose As Integer = MsgBox("Are you sure to delete?", vbYesNo + vbQuestion, "Confirmation")

        If choose = vbNo Then
            Exit Sub
        End If

        deleteLocationCmd.Parameters.Clear()
        deleteLocationCmd.Parameters.AddWithValue("id", lbl_locId.Text)

        deleteSchoolCmd.Parameters.Clear()
        deleteSchoolCmd.Parameters.AddWithValue("id", txt_ID.Text)

        updateHMSchoolCmd.Parameters.Clear()
        updateHMSchoolCmd.Parameters.AddWithValue("hid", txt_IDHM.Text)

        Dim rowsAff As Integer = updateHMSchoolCmd.ExecuteNonQuery

        If rowsAff < 1 Then
            MsgBox("Error at rowsAff")
            Exit Sub
        End If

        Dim rowsAff2 As Integer = deleteLocationCmd.ExecuteNonQuery

        If rowsAff2 < 1 Then
            MsgBox("Error at rowsAff2")
            Exit Sub
        End If

        Dim rowsAff3 As Integer = deleteSchoolCmd.ExecuteNonQuery

        If rowsAff3 < 1 Then
            MsgBox("Error at rowsAff3")
        Else
            MsgBox("Success, school deleted")
            Response.Redirect("AdminSchool.aspx")
        End If
    End Sub

    Protected Sub txt_name_TextChanged(sender As Object, e As EventArgs) Handles txt_name.TextChanged
        If txt_name.Text.Length <= 5 Then
            MsgBox("School's word count should be more than 5")
            Exit Sub
        End If
        txt_tag.Text = txt_name.Text.Substring(0, 5) + "_pg"

    End Sub

    Protected Sub btn_refresh_Click(sender As Object, e As EventArgs) Handles btn_refresh.Click
        txt_searchID.Enabled = True
        txt_searchTag.Enabled = True
        Panel1.Visible = False
        pnl_listSchool.Visible = False
        pnl_SearchOne.Visible = False
        pnl_tag.Visible = False
    End Sub
End Class