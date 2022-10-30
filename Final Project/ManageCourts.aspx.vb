Imports System.Data.SqlClient
Public Class CreateCourts
    Inherits System.Web.UI.Page
    Dim conn As SqlConnection
    Dim loadCourtsCmd As SqlCommand
    Dim loadStatusCmd As SqlCommand
    Dim checkCourtsCmd As SqlCommand
    Dim checkUserCmd As SqlCommand
    Dim addCourtCmd As SqlCommand
    Dim updateCourtCmd As SqlCommand
    Dim getSchoolCmd As SqlCommand
    Dim updateSchoolCmd As SqlCommand
    Dim deleteCourtCmd As SqlCommand

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim connStr As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\jpyea\source\repos\Final Project\Final Project\App_Data\badminton_database.mdf"";Integrated Security=True"
        conn = New SqlConnection(connStr)
        conn.Open()

        lbl_invisiSchool.Text = HM_school

        Dim loadCourtsSql As String = "SELECT court_id, status, time_open, time_close 
                                       FROM Court INNER JOIN Venues ON Court.school_id = Venues.venue_id WHERE school_id = @sh"
        loadCourtsCmd = New SqlCommand(loadCourtsSql, conn)

        Dim loadStatusSql As String = "SELECT status,time_open, time_close FROM Court INNER JOIN Venues ON Court.school_id = Venues.venue_id WHERE court_id = @cid"
        loadStatusCmd = New SqlCommand(loadStatusSql, conn)

        Dim checkCourtsSql As String = "SELECT booking_id, user_id, booking_date_start,booking_date_end
                                        FROM booking INNER JOIN Court ON booking.court_id = Court.court_id
                                        WHERE Court.court_id = @cid"
        checkCourtsCmd = New SqlCommand(checkCourtsSql, conn)

        Dim checkUserSql As String = "SELECT verify
                                      FROM AdminHead
                                      WHERE hm_id = @hi"
        checkUserCmd = New SqlCommand(checkUserSql, conn)

        Dim addCourtSql As String = "INSERT INTO Court(school_id, status, time_open, time_close) VALUES(@sch,1,@to, @tc)"
        addCourtCmd = New SqlCommand(addCourtSql, conn)

        Dim updateCourtSql As String = "UPDATE Court SET status = @sta, time_open = @to, time_close = @tc WHERE court_id = @coid"
        updateCourtCmd = New SqlCommand(updateCourtSql, conn)

        Dim getSchoolSql As String = "SELECT school_available_courts
                                      FROM Venues
                                      WHERE venue_id = @ven"
        getSchoolCmd = New SqlCommand(getSchoolSql, conn)

        Dim updateSchoolSql As String = "UPDATE Venues SET school_available_courts = @sac WHERE venue_id = @venid"
        updateSchoolCmd = New SqlCommand(updateSchoolSql, conn)

        Dim deleteCourtSql = "DELETE FROM Court WHERE court_id = @cid"
        deleteCourtCmd = New SqlCommand(deleteCourtSql, conn)

        loadCourtsCmd.Parameters.Clear()
        loadCourtsCmd.Parameters.AddWithValue("sh", HM_school)

        If Not IsPostBack Then
            Dim adapter As SqlDataAdapter = New SqlDataAdapter(loadCourtsCmd)
            Dim ds As DataSet = New DataSet
            adapter.Fill(ds, "courts")

            Dim dt As DataTable = ds.Tables("courts")

            If dt.Rows.Count < 1 Then
                MsgBox("No courts present, but If you are new here, then ignore it")
            Else
                drp_courts.Items.Clear()
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim dr As DataRow = dt.Rows(i)
                    Dim num As String = (i + 1).ToString
                    drp_courts.Items.Add(num + " (" + dr("court_id").ToString + ")")
                    drp_courts.Items.Item(i).Value = dr("court_id")
                Next
                drp_courts.SelectedIndex = 0
                onReady()
            End If
        End If

    End Sub

    Protected Sub onReady()
        loadStatusCmd.Parameters.Clear()
        loadStatusCmd.Parameters.AddWithValue("cid", drp_courts.SelectedValue)

        Dim adapter As SqlDataAdapter = New SqlDataAdapter(loadStatusCmd)
        Dim ds As DataSet = New DataSet
        adapter.Fill(ds, "courts")

        Dim dt As DataTable = ds.Tables("courts")

        If dt.Rows.Count < 0 Then
            MsgBox("Unknown error, please contact the support team")
        Else
            Dim dr As DataRow = dt.Rows(0)
            If dr("status") = True Then
                drp_availa.Text = 1
            Else
                drp_availa.Text = 0
            End If
        End If
    End Sub


    Protected Sub drp_courts_SelectedIndexChanged(sender As Object, e As EventArgs) Handles drp_courts.SelectedIndexChanged
        loadStatusCmd.Parameters.Clear()
        loadStatusCmd.Parameters.AddWithValue("cid", drp_courts.SelectedValue)

        Dim adapter As SqlDataAdapter = New SqlDataAdapter(loadStatusCmd)
        Dim ds As DataSet = New DataSet
        adapter.Fill(ds, "courts")

        Dim dt As DataTable = ds.Tables("courts")

        If dt.Rows.Count < 0 Then
            MsgBox("Unknown error, please contact the support team")
        Else
            Dim dr As DataRow = dt.Rows(0)
            If dr("status") = True Then
                drp_availa.Text = 1
            Else
                drp_availa.Text = 0
            End If
            Dim timeStart As String = dr("time_open").ToString
            Dim timeEnd As String = dr("time_close").ToString

            drp_start_time.Text = timeStart
            drp_end_time.Text = timeEnd
        End If

    End Sub

    Protected Sub btn_delete_Click(sender As Object, e As EventArgs) Handles btn_delete.Click
        Dim ver As Boolean = VerifyUser()
        If Not ver Then
            MsgBox("Invalid code, please try again")
            Exit Sub
        End If

        Dim userRe As Integer = MsgBox("Are you sure you want to delete?", vbQuestion + vbYesNo + vbDefaultButton2, "Confirmation")

        If userRe = vbNo Then
            Exit Sub
        End If

        checkCourtsCmd.Parameters.Clear()
        checkCourtsCmd.Parameters.AddWithValue("cid", drp_courts.SelectedValue)

        Dim adapter As SqlDataAdapter = New SqlDataAdapter(checkCourtsCmd)
        Dim ds As DataSet = New DataSet
        adapter.Fill(ds, "checkCor")

        Dim dt As DataTable = ds.Tables("checkCor")

        getSchoolCmd.Parameters.Clear()
        getSchoolCmd.Parameters.AddWithValue("ven", HM_school)

        Dim adapter2 As SqlDataAdapter = New SqlDataAdapter(getSchoolCmd)
        Dim ds2 As DataSet = New DataSet
        adapter2.Fill(ds, "schoolNum")

        Dim dt2 As DataTable = ds.Tables("schoolNum")

        If dt2.Rows.Count < 1 Then
            MsgBox("Error while obtaining data for school")
            Exit Sub
        End If

        Dim dr2 As DataRow = dt2.Rows(0)
        Dim schNum As Integer = dr2("school_available_courts")

        If dt.Rows.Count > 0 Then
            MsgBox("Apologizes, there are bookings made with the current court. " + vbCrLf + "Contact the support team if you want to remove it")
        Else
            deleteCourtCmd.Parameters.Clear()
            deleteCourtCmd.Parameters.AddWithValue("cid", drp_courts.SelectedValue)

            schNum -= 1
            updateSchoolCmd.Parameters.Clear()
            updateSchoolCmd.Parameters.AddWithValue("sac", schNum)
            updateSchoolCmd.Parameters.AddWithValue("venid", HM_school)

            Dim rowsAff As Integer = deleteCourtCmd.ExecuteNonQuery()
            Dim rowsAff2 As Integer = updateSchoolCmd.ExecuteNonQuery

            If rowsAff < 1 And rowsAff2 < 1 Then
                MsgBox("Error detected when deleting, please try again")
                Exit Sub
            Else
                MsgBox("Delete successful")
                refreshRoom()
            End If
        End If


    End Sub

    Protected Sub refreshRoom()
        loadCourtsCmd.Parameters.Clear()
        loadCourtsCmd.Parameters.AddWithValue("sh", HM_school)

        Dim adapter As SqlDataAdapter = New SqlDataAdapter(loadCourtsCmd)
        Dim ds As DataSet = New DataSet
        adapter.Fill(ds, "courts")

        Dim dt As DataTable = ds.Tables("courts")

        If dt.Rows.Count < 1 Then
            MsgBox("No rooms present. If you are new here, then ignore it")
        Else
            drp_courts.Items.Clear()
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dr As DataRow = dt.Rows(i)
                Dim num As String = (i + 1).ToString
                drp_courts.Items.Add(num + " (" + dr("court_id").ToString + ")")
                drp_courts.Items.Item(i).Value = dr("court_id")
            Next
            drp_courts.SelectedIndex = 0
        End If

    End Sub

    Protected Sub btn_add_Click(sender As Object, e As EventArgs) Handles btn_add.Click
        Dim veri As String = InputBox("Enter your numbers", "Pin verification")

        checkUserCmd.Parameters.Clear()
        checkUserCmd.Parameters.AddWithValue("hi", HMVars.HM_id)

        Dim adapter As SqlDataAdapter = New SqlDataAdapter(checkUserCmd)
        Dim ds As DataSet = New DataSet
        adapter.Fill(ds, "user")

        Dim dt As DataTable = ds.Tables("user")

        getSchoolCmd.Parameters.Clear()
        getSchoolCmd.Parameters.AddWithValue("ven", HM_school)

        Dim adapter2 As SqlDataAdapter = New SqlDataAdapter(getSchoolCmd)
        Dim ds2 As DataSet = New DataSet
        adapter2.Fill(ds, "schoolNum")

        Dim dt2 As DataTable = ds.Tables("schoolNum")

        If dt2.Rows.Count < 1 Then
            MsgBox("Error while obtaining data for school")
            Exit Sub
        End If

        Dim schNum = 0
        Dim dr2 As DataRow = dt2.Rows(0)
        If Not IsDBNull(dr2("school_available_courts")) Then
            schNum = dr2("school_available_courts")
        End If

        Dim timeStart As DateTime = DateTime.Parse(drp_start_time.SelectedValue + ": 00")
        Dim timeEnd As DateTime = DateTime.Parse(drp_end_time.SelectedValue + ": 00")

        If dt.Rows.Count < 1 Then
            MsgBox("Error detected, please try again")
        Else
            Dim dr As DataRow = dt.Rows(0)
            If veri.CompareTo(dr("verify")) = 0 Then
                addCourtCmd.Parameters.Clear()
                addCourtCmd.Parameters.AddWithValue("sch", HM_school)
                addCourtCmd.Parameters.AddWithValue("to", timeStart)
                addCourtCmd.Parameters.AddWithValue("tc", timeEnd)

                schNum += 1
                updateSchoolCmd.Parameters.Clear()
                updateSchoolCmd.Parameters.AddWithValue("sac", schNum)
                updateSchoolCmd.Parameters.AddWithValue("venid", HM_school)

                Dim rowsAffected As Integer = addCourtCmd.ExecuteNonQuery
                Dim rowsAffected2 As Integer = updateSchoolCmd.ExecuteNonQuery

                If rowsAffected < 1 And rowsAffected2 < 1 Then
                    MsgBox("Error detected, please try again")
                Else
                    MsgBox("Successful. Refreshing")
                    Response.Redirect("ManageCourts.aspx")
                End If

            Else
                MsgBox("Match failed, please try again")
            End If
        End If
    End Sub

    Protected Function VerifyUser()
        Dim veri As String = InputBox("Enter your numbers", "Pin verification")

        checkUserCmd.Parameters.Clear()
        checkUserCmd.Parameters.AddWithValue("hi", HMVars.HM_id)

        Dim adapter As SqlDataAdapter = New SqlDataAdapter(checkUserCmd)
        Dim ds As DataSet = New DataSet
        adapter.Fill(ds, "user")

        Dim dt As DataTable = ds.Tables("user")

        If dt.Rows.Count < 1 Then
            MsgBox("Error detected, please try again")
        Else
            Dim dr As DataRow = dt.Rows(0)
            If veri.CompareTo(dr("verify")) = 0 Then
                Return True
            End If
        End If

        Return False
    End Function

    Protected Sub btn_edit_Click(sender As Object, e As EventArgs) Handles btn_edit.Click
        drp_courts.Enabled = False
        drp_availa.Enabled = True
        btn_add.Visible = False
        btn_delete.Visible = False
        btn_editConfirm.Visible = True
        btn_editCancel.Visible = True
    End Sub

    Protected Sub btn_editCancel_Click(sender As Object, e As EventArgs) Handles btn_editCancel.Click
        drp_courts.Enabled = True
        drp_availa.Enabled = False
        btn_add.Visible = True
        btn_delete.Visible = True
        btn_editConfirm.Visible = False
        btn_editCancel.Visible = False
    End Sub

    Protected Sub btn_editConfirm_Click(sender As Object, e As EventArgs) Handles btn_editConfirm.Click
        Dim veri As Boolean = VerifyUser()
        If Not veri Then
            MsgBox("Sorry, unable to verify your status")
        End If

        Dim sta As String = drp_availa.SelectedValue

        checkCourtsCmd.Parameters.Clear()
        checkCourtsCmd.Parameters.AddWithValue("cid", drp_courts.SelectedValue)

        Dim adapter As SqlDataAdapter = New SqlDataAdapter(checkCourtsCmd)
        Dim ds As DataSet = New DataSet
        adapter.Fill(ds, "checkCor")

        Dim dt As DataTable = ds.Tables("checkCor")

        If drp_availa.SelectedValue = 0 And dt.Rows.Count > 0 Then
            MsgBox("Error, there are bookings made with the system." + vbCrLf + "If you want to change it, notify the support team")
            Exit Sub
        End If

        Dim timeStart As DateTime = DateTime.Parse(drp_start_time.SelectedValue + ": 00")
        Dim timeEnd As DateTime = DateTime.Parse(drp_end_time.SelectedValue + ": 00")

        updateCourtCmd.Parameters.Clear()
        updateCourtCmd.Parameters.AddWithValue("sta", sta)
        updateCourtCmd.Parameters.AddWithValue("coid", drp_courts.SelectedValue)
        updateCourtCmd.Parameters.AddWithValue("to", timeStart)
        updateCourtCmd.Parameters.AddWithValue("tc", timeEnd)


        Dim rowsAff As Integer = updateCourtCmd.ExecuteNonQuery()

        If rowsAff < 1 Then
            MsgBox("Failed, unknown reasons")
        Else
            MsgBox("Success. Court status changed")
        End If

    End Sub
End Class