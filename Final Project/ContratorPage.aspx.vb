Imports System.Data.SqlClient
Public Class ContratorPage
    Inherits System.Web.UI.Page
    Dim conn As SqlConnection
    Dim loadHMSchoolCmd As SqlCommand
    Dim loadSchoolCmd As SqlCommand
    Dim updateSchoolCmd As SqlConnection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim connStr As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\jpyea\source\repos\Final Project\Final Project\App_Data\badminton_database.mdf"";Integrated Security=True"
        conn = New SqlConnection(connStr)
        conn.Open()

        Dim loadHMSchoolSql As String = "SELECT hm_id, hm_username, role,school_num
                                         FROM AdminHead
                                         WHERE hm_id = @hid"
        loadHMSchoolCmd = New SqlCommand(loadHMSchoolSql, conn)

        Dim loadSchoolSql As String = "SELECT school_num, school_name,school_address,school_available_courts, school_tag
                                       FROM AdminHead INNER JOIN Venues ON AdminHead.school_num = Venues.venue_id
                                       INNER JOIN Locations ON Locations.location_id = Venues.school_location
                                       WHERE hm_id = @hid"
        loadSchoolCmd = New SqlCommand(loadSchoolSql, conn)

        loadHMSchoolCmd.Parameters.Clear()
        loadHMSchoolCmd.Parameters.AddWithValue("hid", HM_id)

        loadSchoolCmd.Parameters.Clear()
        loadSchoolCmd.Parameters.AddWithValue("hid", HM_id)
        If Not IsPostBack Then
            Dim adapter As SqlDataAdapter = New SqlDataAdapter(loadHMSchoolCmd)
            Dim ds As DataSet = New DataSet()
            adapter.Fill(ds, "LoadHM")

            Dim dt As DataTable = ds.Tables("LoadHM")

            If dt.Rows.Count < 1 Then
                MsgBox("Unknown Error, please try again")
                Response.Redirect("HMAdminLogin.aspx")
            Else
                Dim dr As DataRow = dt.Rows(0)
                If IsDBNull(dr("school_num")) Then
                    pnl_school.Visible = False
                Else
                    HM_school = dr("school_num")
                    Dim adapter2 As SqlDataAdapter = New SqlDataAdapter(loadSchoolCmd)
                    Dim ds2 As DataSet = New DataSet()
                    adapter2.Fill(ds2, "School")

                    Dim dt2 As DataTable = ds2.Tables("School")

                    If dt2.Rows.Count < 1 Then
                        MsgBox("Something's wrong, please try again")
                    Else
                        Dim dr2 As DataRow = dt2.Rows(0)
                        Dim schNam As String = dr2("school_name")
                        Dim schLoc As String = dr2("school_address")
                        Dim schCourts As String = dr2("school_available_courts")
                        Dim schTag As String = dr2("school_tag")

                        txt_school.Text = schNam
                        txt_school_loc.Text = schLoc
                        txt_schoolCourts.Text = schCourts
                        txt_tag.Text = schTag

                    End If
                    lbl_signSchool.Visible = False
                    btn_signSchool.Visible = False
                End If
            End If
        End If

    End Sub

    Protected Sub btn_signSchool_Click(sender As Object, e As EventArgs) Handles btn_signSchool.Click
        Response.Redirect("CreateSchool.aspx")
    End Sub

    Protected Sub btn_update_Click(sender As Object, e As EventArgs) Handles btn_update.Click
        enableDisable(True)
    End Sub

    Protected Sub btn_courts_Click(sender As Object, e As EventArgs) Handles btn_courts.Click
        Response.Redirect("ManageCourts.aspx")
    End Sub

    Protected Sub txt_schoolName_TextChanged(sender As Object, e As EventArgs) Handles txt_school.TextChanged
        If txt_school.Text.Length <= 5 Then
            MsgBox("School's word count should be more than 5")
            Exit Sub
        End If
        btn_courts.Enabled = True
        txt_tag.Text = txt_school.Text.Substring(0, 5) + "_pg"
    End Sub

    Protected Sub btn_cancel_Click(sender As Object, e As EventArgs) Handles btn_cancel.Click
        loadHMSchoolCmd.Parameters.Clear()
        loadHMSchoolCmd.Parameters.AddWithValue("hid", HM_id)

        Dim adapter As SqlDataAdapter = New SqlDataAdapter(loadHMSchoolCmd)
        Dim ds As DataSet = New DataSet()
        adapter.Fill(ds, "LoadHM")

        Dim dt As DataTable = ds.Tables("LoadHM")

        If dt.Rows.Count < 1 Then
            MsgBox("Unknown Error, please try again")
            Response.Redirect("HMAdminLogin.aspx")
        Else
            Dim dr As DataRow = dt.Rows(0)
            If IsDBNull(dr("school_num")) Then
                pnl_school.Visible = False
            Else
                Dim adapter2 As SqlDataAdapter = New SqlDataAdapter(loadSchoolCmd)
                Dim ds2 As DataSet = New DataSet()
                adapter2.Fill(ds2, "School")

                Dim dt2 As DataTable = ds2.Tables("School")

                If dt2.Rows.Count < 1 Then
                    MsgBox("Something's wrong, please try again")
                Else
                    Dim dr2 As DataRow = dt2.Rows(0)
                    Dim schNam As String = dr2("school_name")
                    Dim schLoc As String = dr2("school_address")
                    Dim schCourts As String = dr2("school_available_courts")
                    Dim schTag As String = dr2("school_tag")

                    txt_school.Text = schNam
                    txt_school_loc.Text = schLoc
                    txt_schoolCourts.Text = schCourts
                    txt_tag.Text = schTag

                End If
                lbl_signSchool.Visible = False
                btn_signSchool.Visible = False
            End If
        End If
        enableDisable(False)
    End Sub

    Protected Sub enableDisable(bool As Boolean)
        txt_school.Enabled = bool
        txt_school_loc.Enabled = bool
        txt_tag.Enabled = bool
        btn_cancel.Visible = bool
        btn_confirm.Visible = bool
        btn_update.Visible = Not bool
    End Sub

End Class