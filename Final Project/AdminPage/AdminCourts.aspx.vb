Imports System.Data.SqlClient
Public Class SchoolCourts
    Inherits System.Web.UI.Page
    Dim conn As SqlConnection
    Dim loadSchoolsCmd As SqlCommand
    Dim loadCourtsCmd As SqlCommand
    Dim loadAvaCmd As SqlCommand
    Dim addCourtCmd As SqlCommand
    Dim getSchoolIdCmd As SqlCommand
    Dim updateSchoolCmd As SqlCommand
    Dim updateCourtsCmd As SqlCommand
    Dim checkCourtsCmd As SqlCommand
    Dim deleteCourtsCmd As SqlCommand

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("IsAdmin") Is Nothing Then
            Server.Transfer("../Starting_Page.aspx")
        End If
        Dim connStr As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\jpyea\source\repos\Final Project\Final Project\App_Data\badminton_database.mdf"";Integrated Security=True"
        conn = New SqlConnection(connStr)
        conn.Open()

        Dim loadSchoolsSql As String = "SELECT * FROM Venues"
        loadSchoolsCmd = New SqlCommand(loadSchoolsSql, conn)

        Dim loadCourtsSql As String = "SELECT * 
                                       FROM Court INNER JOIN Venues ON Venues.venue_id = Court.school_id
                                       WHERE school_tag = @tag"
        loadCourtsCmd = New SqlCommand(loadCourtsSql, conn)

        Dim loadAvaSql As String = "SELECT status, time_open, time_close
                                    FROM Court 
                                    WHERE court_id = @id"
        loadAvaCmd = New SqlCommand(loadAvaSql, conn)

        Dim addCourtSql As String = "INSERT INTO Court(school_id, status, time_open, time_close) VALUES(@id,1, @to, @tc)"
        addCourtCmd = New SqlCommand(addCourtSql, conn)

        Dim getSchoolIdSql As String = "SELECT venue_id, school_available_courts
                                        FROM Venues
                                        WHERE school_tag = @tag"
        getSchoolIdCmd = New SqlCommand(getSchoolIdSql, conn)

        Dim updateSchoolSql As String = "UPDATE Venues SET school_available_courts = @sac WHERE venue_id = @ven"
        updateSchoolCmd = New SqlCommand(updateSchoolSql, conn)

        Dim updateCourtsSql As String = "UPDATE Court SET status = @s, time_open = @to, time_close = @tc WHERE court_id = @cid"
        updateCourtsCmd = New SqlCommand(updateCourtsSql, conn)

        Dim checkCourtsSql As String = "SELECT booking_id
                                        FROM booking
                                        WHERE court_id = @id"
        checkCourtsCmd = New SqlCommand(checkCourtsSql, conn)

        Dim deleteCourtsSql As String = "DELETE FROM Court WHERE court_id = @cid"
        deleteCourtsCmd = New SqlCommand(deleteCourtsSql, conn)

        Dim adap As SqlDataAdapter = New SqlDataAdapter(loadSchoolsCmd)
        Dim ds As DataSet = New DataSet
        adap.Fill(ds, "schools")
        Dim dt As DataTable = ds.Tables("schools")

        If Not IsPostBack Then
            If dt.Rows.Count < 1 Then
                MsgBox("Error on dt rows. Either empty or another error")
            Else
                drp_schools.Items.Clear()
                drp_schools.Items.Add("(select)")
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim dr As DataRow = dt.Rows(i)
                    Dim schId As Integer = dr("venue_id")
                    Dim schName As String = dr("school_name")
                    Dim schTag As String = dr("school_tag")
                    Dim strJoin As String = "School ID: " + schId.ToString + " Name: " + schName + " (" + schTag + ")"
                    drp_schools.Items.Add(strJoin)
                    drp_schools.Items.Item(i + 1).Value = schTag
                Next
                If Not AdminVars.schoolTag = "" Then
                    drp_schools.Text = AdminVars.schoolTag
                    loadCourt()
                End If
            End If
        End If
    End Sub

    Protected Sub loadCourt()
        Dim tag As String = drp_schools.SelectedValue

        loadCourtsCmd.Parameters.Clear()
        loadCourtsCmd.Parameters.AddWithValue("tag", tag)

        Dim adap As SqlDataAdapter = New SqlDataAdapter(loadCourtsCmd)
        Dim ds As DataSet = New DataSet
        adap.Fill(ds, "courts")
        Dim dt As DataTable = ds.Tables("courts")

        If dt.Rows.Count < 1 Then
            MsgBox("Error on dt rows. Either empty or another error")
        Else
            drp_courts.Items.Clear()
            drp_courts.Items.Add("(select)")
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dr As DataRow = dt.Rows(i)
                Dim courtId As Integer = dr("court_id")
                Dim status As String = dr("school_id")
                Dim strJoin As String = "Court ID: " + courtId.ToString
                drp_courts.Items.Add(strJoin)
                drp_courts.Items.Item(i + 1).Value = courtId
            Next
        End If
    End Sub

    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles drp_schools.SelectedIndexChanged
        Dim tag As String = drp_schools.SelectedValue

        loadCourtsCmd.Parameters.Clear()
        loadCourtsCmd.Parameters.AddWithValue("tag", tag)

        Dim adap As SqlDataAdapter = New SqlDataAdapter(loadCourtsCmd)
        Dim ds As DataSet = New DataSet
        adap.Fill(ds, "courts")
        Dim dt As DataTable = ds.Tables("courts")

        If dt.Rows.Count < 1 Then
            MsgBox("Error on dt rows. Either empty or another error")
        Else
            drp_courts.Items.Clear()
            drp_courts.Items.Add("(select)")
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dr As DataRow = dt.Rows(i)
                Dim courtId As Integer = dr("court_id")
                Dim status As String = dr("school_id")
                Dim strJoin As String = "Court ID: " + courtId.ToString
                drp_courts.Items.Add(strJoin)
                drp_courts.Items.Item(i + 1).Value = courtId
            Next
        End If
    End Sub

    Protected Sub btn_change_Click(sender As Object, e As EventArgs) Handles btn_change.Click
        Dim status As Integer = drp_availa.SelectedValue
        Dim court As Integer = drp_courts.SelectedValue

        checkCourtsCmd.Parameters.Clear()
        checkCourtsCmd.Parameters.AddWithValue("id", court)

        Dim timeStart As DateTime = DateTime.Parse(drp_start_time.SelectedValue + ": 00")
        Dim timeEnd As DateTime = DateTime.Parse(drp_end_time.SelectedValue + ": 00")

        Dim adap As SqlDataAdapter = New SqlDataAdapter(checkCourtsCmd)
        Dim ds As DataSet = New DataSet
        adap.Fill(ds, "courts")
        Dim dt As DataTable = ds.Tables("courts")

        If dt.Rows.Count > 0 And status = 0 Then
            Dim userRe As Integer = MsgBox("Have you warned the users before changing the status?", vbQuestion + vbYesNo + vbDefaultButton2, "Confirmation")
            If userRe = vbNo Then
                Exit Sub
            End If
        End If

        updateCourtsCmd.Parameters.Clear()
        updateCourtsCmd.Parameters.AddWithValue("s", status)
        updateCourtsCmd.Parameters.AddWithValue("cid", court)
        updateCourtsCmd.Parameters.AddWithValue("to", timeStart)
        updateCourtsCmd.Parameters.AddWithValue("tc", timeEnd)

        Dim rowsAff As Integer = updateCourtsCmd.ExecuteNonQuery
        If rowsAff < 1 Then
            MsgBox("Error related to updating the data")
        Else
            MsgBox("Success updating")
            Server.Transfer("AdminCourts.aspx")
        End If

    End Sub

    Protected Sub drp_courts_SelectedIndexChanged(sender As Object, e As EventArgs) Handles drp_courts.SelectedIndexChanged
        Dim court As Integer = drp_courts.SelectedValue

        loadAvaCmd.Parameters.Clear()
        loadAvaCmd.Parameters.AddWithValue("id", court)

        Dim adap As SqlDataAdapter = New SqlDataAdapter(loadAvaCmd)
        Dim ds As DataSet = New DataSet
        adap.Fill(ds, "ava")
        Dim dt As DataTable = ds.Tables("ava")

        If dt.Rows.Count < 1 Then
            MsgBox("Error, dt is either empty or damaged")
        Else
            Dim dr As DataRow = dt.Rows(0)
            Dim ava As Integer = dr("status")
            Dim timeOpen As String = dr("time_open").ToString.Substring(0, 2)
            Dim timeClose As String = dr("time_close").ToString.Substring(0, 2)

            drp_availa.Text = ava
            drp_start_time.Text = timeOpen
            drp_end_time.Text = timeClose
        End If
    End Sub

    Protected Sub btn_delete_Click(sender As Object, e As EventArgs) Handles btn_delete.Click
        If drp_courts.SelectedIndex = 0 Then
            MsgBox("You must select an court")
        End If

        Dim court As Integer = drp_courts.SelectedValue

        checkCourtsCmd.Parameters.Clear()
        checkCourtsCmd.Parameters.AddWithValue("id", court)

        Dim adap As SqlDataAdapter = New SqlDataAdapter(checkCourtsCmd)
        Dim ds As DataSet = New DataSet
        adap.Fill(ds, "courts")
        Dim dt As DataTable = ds.Tables("courts")

        If dt.Rows.Count > 0 Then
            MsgBox("There are bookings made with this court")
            Exit Sub
        End If

        Dim userRe As Integer = MsgBox("Are you sure?", vbQuestion + vbYesNo + vbDefaultButton2, "Confirmation")

        If userRe = vbNo Then
            Exit Sub
        End If

        getSchoolIdCmd.Parameters.Clear()
        getSchoolIdCmd.Parameters.AddWithValue("tag", drp_schools.SelectedValue)

        Dim adap2 As SqlDataAdapter = New SqlDataAdapter(getSchoolIdCmd)
        Dim ds2 As DataSet = New DataSet
        adap2.Fill(ds2, "id")
        Dim dt2 As DataTable = ds2.Tables("id")

        If dt2.Rows.Count < 1 Then
            MsgBox("Error related to fetching id")
            Exit Sub
        End If

        Dim dr2 As DataRow = dt2.Rows(0)
        Dim id As Integer = dr2("venue_id")
        Dim nums As Integer = dr2("school_available_courts")
        nums -= 1

        updateSchoolCmd.Parameters.Clear()
        updateSchoolCmd.Parameters.AddWithValue("ven", ID)
        updateSchoolCmd.Parameters.AddWithValue("sac", nums)

        deleteCourtsCmd.Parameters.Clear()
        deleteCourtsCmd.Parameters.AddWithValue("cid", court)

        Dim rowsAff As Integer = deleteCourtsCmd.ExecuteNonQuery
        Dim rowsAff2 As Integer = updateSchoolCmd.ExecuteNonQuery

        If rowsAff < 1 Then
            MsgBox("Error related to rowsAff")
        Else
            If rowsAff2 < 1 Then
                MsgBox("Error related to rowsAff2")
            Else
                MsgBox("Success, refreshing")
                Server.Transfer("AdminCourts.aspx")
            End If
        End If

    End Sub

    Protected Sub btn_add_Click(sender As Object, e As EventArgs) Handles btn_add.Click
        getSchoolIdCmd.Parameters.Clear()
        getSchoolIdCmd.Parameters.AddWithValue("tag", drp_schools.SelectedValue)

        Dim timeStart As DateTime = DateTime.Parse(drp_start_time.SelectedValue + ": 00")
        Dim timeEnd As DateTime = DateTime.Parse(drp_end_time.SelectedValue + ": 00")

        MsgBox(DateDiff("h", timeStart, timeEnd))

        If DateDiff("h", timeStart, timeEnd) < 1 Then
            MsgBox("the time to end is more than time to start")
        End If

        Dim adap As SqlDataAdapter = New SqlDataAdapter(getSchoolIdCmd)
        Dim ds As DataSet = New DataSet
        adap.Fill(ds, "id")
        Dim dt As DataTable = ds.Tables("id")

        If dt.Rows.Count < 1 Then
            MsgBox("Error related to fetching id")
            Exit Sub
        End If

        Dim dr As DataRow = dt.Rows(0)
        Dim id As Integer = dr("venue_id")
        Dim nums As Integer = 0
        If Not IsDBNull(dr("school_available_courts")) Then
            nums = dr("school_available_courts")
        End If
        nums += 1

        updateSchoolCmd.Parameters.Clear()
        updateSchoolCmd.Parameters.AddWithValue("ven", id)
        updateSchoolCmd.Parameters.AddWithValue("sac", nums)

        addCourtCmd.Parameters.Clear()
        addCourtCmd.Parameters.AddWithValue("id", id)
        addCourtCmd.Parameters.AddWithValue("to", timeStart)
        addCourtCmd.Parameters.AddWithValue("tc", timeEnd)

        Dim rowsAff As Integer = addCourtCmd.ExecuteNonQuery
        If rowsAff < 1 Then
            MsgBox("Error related when adding court")
        Else
            Dim rowsAff2 As Integer = updateSchoolCmd.ExecuteNonQuery
            If rowsAff2 < 1 Then
                MsgBox("Error related to updaing school nums")
            Else
                MsgBox("Added successfully")
                Server.Transfer("AdminCourts.aspx")
            End If
        End If
    End Sub

    Protected Sub btn_main_menu_Click(sender As Object, e As EventArgs) Handles btn_main_menu.Click
        Server.Transfer("../Admin_page.aspx")
    End Sub
End Class