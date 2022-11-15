Imports System.Data.SqlClient
Imports System.IO
Imports System.IO.StreamReader
Public Class CreateSchool
    Inherits System.Web.UI.Page
    Dim conn As SqlConnection
    Dim checkSchoolCmd As SqlCommand
    Dim getLocationCmd As SqlCommand
    Dim getSchoolIdCmd As SqlCommand
    Dim addLocationCmd As SqlCommand
    Dim addSchoolCmd As SqlCommand
    Dim addHMCmd As SqlCommand
    Dim getHMIdCmd As SqlCommand
    Dim updateHMCmd As SqlCommand
    Dim regexEmail As Regex = New Regex("^((([^<>()[\]\\.,;:\s@])+\.?([^!@#$%^&*()_+{}:<>?])+)|.*)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z]+\.))+[a-zA-Z]{2,})")


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim connStr As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\jpyea\source\repos\Final Project\Final Project\App_Data\badminton_database.mdf"";Integrated Security=True"
        conn = New SqlConnection(connStr)
        conn.Open()

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

        Dim getSchoolIdSql As String = "SELECT venue_id,school_tag
                                        FROM Venues
                                        WHERE school_name = @sch"
        getSchoolIdCmd = New SqlCommand(getSchoolIdSql, conn)

        Dim addSchoolSql As String = "INSERT INTO Venues(school_name,school_tag,school_available_courts,school_location,owner_id) VALUES(@nam, @tag, @ava,@loc,@own)"
        addSchoolCmd = New SqlCommand(addSchoolSql, conn)

        Dim addHMSql As String = "INSERT INTO Owner(owner_name, owner_email, owner_phone) VALUES(@nam, @em, @ph)"
        addHMCmd = New SqlCommand(addHMSql, conn)

        Dim getHMIdsql As String = "SELECT owner_id FROM Owner WHERE owner_name = @on AND owner_email = @oe"
        getHMIdCmd = New SqlCommand(getHMIdsql, conn)


    End Sub

    Protected Sub btn_courts_Click(sender As Object, e As EventArgs) Handles btn_courts.Click
        Dim sch As String = txt_school.Text.ToString()
        Dim loc As String = txt_location.Text.ToString()
        Dim tag As String = txt_tag.Text.ToString()
        Dim email As String = txt_hmEmail.Text.ToString

        If loc.Length <= 8 Then
            MsgBox("Location's word count should be more than 8")
            Exit Sub
        End If

        If sch.Length <= 5 Then
            MsgBox("School's word count should be more than 5")
            Exit Sub
        End If

        If Not regexEmail.IsMatch(email) Then
            MsgBox("The email is invalid, please try again")
        End If

        checkSchoolCmd.Parameters.Clear()
        checkSchoolCmd.Parameters.AddWithValue("sch", sch)
        checkSchoolCmd.Parameters.AddWithValue("tag", tag)
        checkSchoolCmd.Parameters.AddWithValue("loc", loc)

        Dim adapter As SqlDataAdapter = New SqlDataAdapter(checkSchoolCmd)
        Dim ds As DataSet = New DataSet()
        adapter.Fill(ds, "checkSchool")

        Dim dt As DataTable = ds.Tables("checkSchool")

        If dt.Rows.Count > 0 Then
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

                    addHMCmd.Parameters.Clear()
                    addHMCmd.Parameters.AddWithValue("nam", txt_hmname.Text)
                    addHMCmd.Parameters.AddWithValue("em", txt_hmEmail.Text)
                    addHMCmd.Parameters.AddWithValue("ph", txt_HMphone.Text)

                    Dim rowsAff3 As Integer = addHMCmd.ExecuteNonQuery
                    If rowsAff3 < 1 Then
                        MsgBox("Error detected, please try again")
                    Else
                        getHMIdCmd.Parameters.Clear()
                        getHMIdCmd.Parameters.AddWithValue("on", txt_hmname.Text)
                        getHMIdCmd.Parameters.AddWithValue("oe", txt_hmEmail.Text)

                        Dim adap4 As SqlDataAdapter = New SqlDataAdapter(getHMIdCmd)
                        Dim ds4 As DataSet = New DataSet
                        adap4.Fill(ds4, "num")
                        Dim dt4 As DataTable = ds4.Tables("num")
                        Dim dr3 As DataRow = dt4.Rows(0)
                        Dim own As Integer = dr3("owner_id")

                        addSchoolCmd.Parameters.Clear()
                        addSchoolCmd.Parameters.AddWithValue("nam", sch)
                        addSchoolCmd.Parameters.AddWithValue("tag", tag)
                        addSchoolCmd.Parameters.AddWithValue("loc", loca)
                        addSchoolCmd.Parameters.AddWithValue("ava", 0)
                        addSchoolCmd.Parameters.AddWithValue("own", own)

                        Dim rowsAff2 As Integer = addSchoolCmd.ExecuteNonQuery

                        getSchoolIdCmd.Parameters.Clear()
                        getSchoolIdCmd.Parameters.AddWithValue("sch", sch)

                        Dim adapter3 As SqlDataAdapter = New SqlDataAdapter(getSchoolIdCmd)
                        Dim ds3 As DataSet = New DataSet
                        adapter3.Fill(ds3, "getSch")

                        Dim dt3 As DataTable = ds3.Tables("getsch")
                        Dim dr2 As DataRow = dt3.Rows(0)

                        AdminVars.schoolTag = dr2("school_tag")

                        MsgBox(sch + " added, now to the courts")
                        Server.Transfer("AdminPage/AdminCourts.aspx")
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

    Private Function CheckBack(toString As String) As Boolean
        If toString.IndexOf(".") = -1 Then
            Return False
        End If

        Dim validExtend(5) As String

        validExtend(0) = "png"
        validExtend(1) = "jpg"
        validExtend(2) = "jpeg"
        validExtend(3) = "bmp"
        validExtend(4) = "tif"
        validExtend(5) = "tiff"

        Dim ext = toString.Substring(toString.LastIndexOf(".") + 1).ToLower()

        For Each extend As String In validExtend
            If extend.CompareTo(ext) = 0 Then
                Return True
            End If
        Next

        Return False
    End Function
End Class