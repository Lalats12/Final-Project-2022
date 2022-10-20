Imports System.Data.SqlClient
Public Class CreateSchool
    Inherits System.Web.UI.Page
    Dim conn As SqlConnection
    Dim checkHMCmd As SqlCommand
    Dim checkSchoolCmd As SqlCommand
    Dim getLocationCmd As SqlCommand
    Dim getSchoolIdCmd As SqlCommand
    Dim addLocationCmd As SqlCommand
    Dim addSchoolCmd As SqlCommand
    Dim updateHMCmd As SqlCommand

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim connStr As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\jpyea\source\repos\Final Project\Final Project\App_Data\badminton_database.mdf"";Integrated Security=True"
        conn = New SqlConnection(connStr)
        conn.Open()

        Dim checkHMSql As String = "SELECT hm_id, school_num
                                    FROM AdminHead
                                    WHERE hm_id = @hi"
        checkHMCmd = New SqlCommand(checkHMSql, conn)

        Dim checkSchoolSql As String = "SELECT school_name, school_tag, school_address
                                        FROM Venues INNER JOIN Locations ON Venues.school_location = Locations.location_id
                                        WHERE school_name = @sch OR school_tag = @tag OR school_address = @loc"
        checkSchoolCmd = New SqlCommand(checkSchoolSql, conn)

        Dim addLocationSql As String = "INSERT INTO Locations(school_address) VALUES (@add)"
        addLocationCmd = New SqlCommand(addLocationSql, conn)

        Dim getLocationSql As String = "SELECT location_id
                                        FROM Locations
                                        WHERE school_address = @add"
        getLocationCmd = New SqlCommand(getLocationSql, conn)

        Dim getSchoolIdSql As String = "SELECT venue_id
                                        FROM Venues
                                        WHERE school_name = @sch"
        getSchoolIdCmd = New SqlCommand(getSchoolIdSql, conn)

        Dim addSchoolSql As String = "INSERT INTO Venues(school_name,school_tag,school_location) VALUES(@nam, @tag, @loc)"
        addSchoolCmd = New SqlCommand(addSchoolSql, conn)

        Dim updateHM As String = "UPDATE AdminHead SET school_num = @sch WHERE hm_id = @hid"
        updateHMCmd = New SqlCommand(updateHM, conn)

        checkHMCmd.Parameters.Clear()
        checkHMCmd.Parameters.AddWithValue("hi", HM_id)

        Dim adapter As SqlDataAdapter = New SqlDataAdapter(checkHMCmd)
        Dim ds As DataSet = New DataSet
        adapter.Fill(ds, "check")

        Dim dt As DataTable = ds.Tables("check")

        Dim dr As DataRow = dt.Rows(0)

        If Not IsDBNull(dr("school_num")) Then
            MsgBox("Your school is registered, returing to main menu")
            Response.Redirect("ContratorPage.aspx")
        End If



    End Sub

    Protected Sub btn_courts_Click(sender As Object, e As EventArgs) Handles btn_courts.Click
        Dim sch As String = txt_school.Text.ToString()
        Dim loc As String = txt_location.Text.ToString()
        Dim tag As String = txt_tag.Text.ToString()

        If loc.Length <= 8 Then
            MsgBox("Location's word count should be more than 8")
            Exit Sub
        End If

        If sch.Length <= 5 Then
            MsgBox("School's word count should be more than 5")
            Exit Sub
        End If

        checkSchoolCmd.Parameters.Clear()
        checkSchoolCmd.Parameters.AddWithValue("sch", sch)
        checkSchoolCmd.Parameters.AddWithValue("tag", tag)
        checkSchoolCmd.Parameters.AddWithValue("loc", loc)

        Dim adapter As SqlDataAdapter = New SqlDataAdapter(checkSchoolCmd)
        Dim ds As DataSet = New DataSet()
        adapter.Fill(ds, "checkSchool")

        Dim dt As DataTable = ds.Tables("checkSchool")

        If dt.Rows.Count > 1 Then
            MsgBox("There is(are) school(s) with the same name of the school/location/tag")
            Exit Sub
        Else
            addLocationCmd.Parameters.Clear()
            addLocationCmd.Parameters.AddWithValue("add", loc)

            Dim rowsAff As Integer = addLocationCmd.ExecuteNonQuery()

            If rowsAff < 1 Then
                MsgBox("Unknown error please try again")
            Else
                getLocationCmd.Parameters.Clear()
                getLocationCmd.Parameters.AddWithValue("add", loc)

                Dim adapter2 As SqlDataAdapter = New SqlDataAdapter(getLocationCmd)
                Dim ds2 As DataSet = New DataSet
                adapter2.Fill(ds2, "getLoc")

                Dim dt2 As DataTable = ds2.Tables("getLoc")

                If dt2.Rows.Count < 1 Then
                    MsgBox("Error occured, please try again")
                    Exit Sub
                Else
                    Dim dr = dt2.Rows(0)
                    Dim loca As Integer = dr("location_id")
                    addSchoolCmd.Parameters.Clear()
                    addSchoolCmd.Parameters.AddWithValue("nam", sch)
                    addSchoolCmd.Parameters.AddWithValue("tag", tag)
                    addSchoolCmd.Parameters.AddWithValue("loc", loca)

                    Dim rowsAff2 As Integer = addSchoolCmd.ExecuteNonQuery
                    If rowsAff2 < 1 Then
                        MsgBox("Error detected, please try again")
                    Else
                        getSchoolIdCmd.Parameters.Clear()
                        getSchoolIdCmd.Parameters.AddWithValue("sch", sch)

                        Dim adapter3 As SqlDataAdapter = New SqlDataAdapter(getSchoolIdCmd)
                        Dim ds3 As DataSet = New DataSet
                        adapter3.Fill(ds3, "getSch")

                        Dim dt3 As DataTable = ds3.Tables("getsch")
                        Dim dr2 As DataRow = dt3.Rows(0)

                        updateHMCmd.Parameters.Clear()
                        updateHMCmd.Parameters.AddWithValue("hid", HM_id)
                        updateHMCmd.Parameters.AddWithValue("sch", dr2("venue_id"))
                        HM_school = dr2("venue_id")

                        Dim rowsAff3 As Integer = updateHMCmd.ExecuteNonQuery()

                        If rowsAff3 < 1 Then
                            MsgBox("Error at roAff3")
                        Else
                            MsgBox(sch + " added, now to the courts")
                            Response.Redirect("ManageCourts.aspx")
                        End If

                    End If
                End If

            End If
        End If
    End Sub

    Protected Sub txt_school_TextChanged(sender As Object, e As EventArgs) Handles txt_school.TextChanged
        If txt_school.Text.Length <= 5 Then
            MsgBox("School's word count should be more than 5")
            Exit Sub
        End If
        btn_courts.Enabled = True
        txt_tag.Text = txt_school.Text.Substring(0, 5) + "_pg"
    End Sub
End Class