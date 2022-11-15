Imports System.Data.SqlClient
Public Class CourtsImages
    Inherits System.Web.UI.Page
    Dim conn As SqlConnection
    Dim loadSchoolsCmd As SqlCommand
    Dim loadImagesCmd As SqlCommand

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strConn As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\jpyea\source\repos\Final Project\Final Project\App_Data\badminton_database.mdf"";Integrated Security=True"
        conn = New SqlConnection(strConn)
        conn.Open()

        Dim loadSchoolsSql As String = "SELECT * FROM Venues"
        loadSchoolsCmd = New SqlCommand(loadSchoolsSql, conn)

        Dim loadImagesSql As String = "SELECT school_image FROM Venues WHERE school_tag = @sch"
        loadImagesCmd = New SqlCommand(loadImagesSql, conn)

        Dim adap As SqlDataAdapter = New SqlDataAdapter(loadSchoolsCmd)
        Dim ds As DataSet = New DataSet
        adap.Fill(ds, "schools")
        Dim dt As DataTable = ds.Tables("schools")

        If dt.Rows.Count < 1 Then
            MsgBox("Unknown Error has occured. Please try again")
        Else
            If Not IsPostBack Then
                lst_schools.Items.Clear()
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim dr As DataRow = dt.Rows(i)
                    lst_schools.Items.Add(dr("school_name"))
                    lst_schools.Items.Item(i).Value = dr("school_tag")
                Next
            End If
        End If
    End Sub

    Protected Sub lst_schools_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lst_schools.SelectedIndexChanged
        If lst_schools.SelectedIndex = -1 Then
            img_schoolPreview.ImageUrl = Nothing
        End If

        loadImagesCmd.Parameters.Clear()
        loadImagesCmd.Parameters.AddWithValue("sch", lst_schools.SelectedValue)
        Dim adap As SqlDataAdapter = New SqlDataAdapter(loadImagesCmd)
        Dim ds As DataSet = New DataSet
        adap.Fill(ds, "img")
        Dim dt As DataTable = ds.Tables("img")

        If dt.Rows.Count < 1 Then
            MsgBox("Unknown error")
        Else
            Dim dr As DataRow = dt.Rows(0)
            If IsDBNull(dr("school_image")) Then
                img_schoolPreview.ImageUrl = Nothing
                MsgBox("Sorry for the inconvience, there is no image for the school yet")
            Else
                img_schoolPreview.ImageUrl = dr("school_image")
            End If
        End If
    End Sub
End Class