Imports System.Data.SqlClient
Public Class AdminImageaspx
    Inherits System.Web.UI.Page
    Dim conn As SqlConnection
    Dim loadSchoolsCmd As SqlCommand
    Dim loadImgCmd As SqlCommand
    Dim updateImgCmd As SqlCommand

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("IsAdmin") Is Nothing Then
            Server.Transfer("../Starting_Page.aspx")
        End If
        Dim strConn As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\jpyea\source\repos\Final Project\Final Project\App_Data\badminton_database.mdf"";Integrated Security=True"
        conn = New SqlConnection(strConn)
        conn.Open()

        Dim loadSchoolsSql As String = "SELECT venue_id,school_name,school_tag
                                        FROM Venues"
        loadSchoolsCmd = New SqlCommand(loadSchoolsSql, conn)

        Dim loadImgSql As String = "SELECT school_image FROM Venues WHERE school_tag = @tag"
        loadImgCmd = New SqlCommand(loadImgSql, conn)

        Dim updateImgSql As String = "UPDATE Venues SET school_image = @img WHERE school_tag = @tag"
        updateImgCmd = New SqlCommand(updateImgSql, conn)

        Dim adap As SqlDataAdapter = New SqlDataAdapter(loadSchoolsCmd)
        Dim ds As DataSet = New DataSet
        adap.Fill(ds, "schools")
        Dim dt As DataTable = ds.Tables("schools")
        If dt.Rows.Count < 1 Then
            MsgBox("Error while fetching data")
        Else
            If Not IsPostBack Then
                drp_schools.Items.Clear()
                drp_schools.Items.Add("(Select)")
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim dr As DataRow = dt.Rows(i)
                    Dim id As Integer = dr("venue_id")
                    Dim name As String = dr("school_name")
                    Dim tag As String = dr("school_tag")
                    Dim strJoin As String = id.ToString + ": " + name
                    drp_schools.Items.Add(strJoin)
                    drp_schools.Items.Item(i + 1).Value = tag
                Next
            End If
        End If
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

    Protected Sub btn_return_Click(sender As Object, e As EventArgs) Handles btn_return.Click
        Server.Transfer("AdminSchool.aspx")
    End Sub

    Protected Sub file_up_DataBinding(sender As Object, e As EventArgs) Handles file_up.DataBinding

    End Sub

    Protected Sub btn_imgChange_Click(sender As Object, e As EventArgs) Handles btn_imgChange.Click
        If file_up.HasFile AndAlso CheckBack(file_up.FileName) Then
            Dim filename As String = file_up.FileName
            file_up.SaveAs(Server.MapPath("../images/" + filename))
            Dim imgLoc As String = "../images/" + filename
            updateImgCmd.Parameters.Clear()
            updateImgCmd.Parameters.AddWithValue("img", imgLoc)
            updateImgCmd.Parameters.AddWithValue("tag", drp_schools.SelectedValue)

            Dim rowsAff As Integer = updateImgCmd.ExecuteNonQuery
            If rowsAff < 1 Then
                MsgBox("Error related to updating image")
            Else
                MsgBox("Successfully updated")
                Server.Transfer("AdminImage.aspx")
            End If
        Else
            MsgBox("Wrong File or empty file")
        End If
    End Sub

    Protected Sub drp_schools_SelectedIndexChanged(sender As Object, e As EventArgs) Handles drp_schools.SelectedIndexChanged
        If drp_schools.SelectedIndex = 0 Then
            btn_imgChange.Enabled = False
            img_school.ImageUrl = Nothing
        End If
        btn_imgChange.Enabled = True

        loadImgCmd.Parameters.Clear()
        loadImgCmd.Parameters.AddWithValue("tag", drp_schools.SelectedValue)
        Dim adap As SqlDataAdapter = New SqlDataAdapter(loadImgCmd)
        Dim ds As DataSet = New DataSet
        adap.Fill(ds, "img")
        Dim dt As DataTable = ds.Tables("img")

        If dt.Rows.Count < 1 Then
            MsgBox("Unknown error")
        Else
            Dim dr As DataRow = dt.Rows(0)
            If IsDBNull(dr("school_image")) Then
                img_school.ImageUrl = Nothing
            Else
                img_school.ImageUrl = dr("school_image")
            End If
        End If

    End Sub
End Class