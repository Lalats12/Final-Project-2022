Imports System.Data.SqlClient
Public Class AdminUser
    Inherits System.Web.UI.Page
    Dim conn As SqlConnection
    Dim getUserCmd As SqlCommand
    Dim getHMCmd As SqlCommand
    Dim updateUserCmd As SqlCommand
    Dim checkUserCmd As SqlCommand
    Dim updateHMCmd As SqlCommand
    Dim checkHMCmd As SqlCommand
    Dim deleteUserCmd As SqlCommand
    Dim checkBookingCmd As SqlCommand
    Dim deleteHMCmd As SqlCommand
    Dim checkSchoolCmd As SqlCommand

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim connStr As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\jpyea\source\repos\Final Project\Final Project\App_Data\badminton_database.mdf"";Integrated Security=True"
        conn = New SqlConnection(connStr)
        conn.Open()

        Dim getUserSql As String = "SELECT * FROM user_data WHERE user_id = @uid"
        getUserCmd = New SqlCommand(getUserSql, conn)

        Dim getHMSql As String = "SELECT * FROM AdminHead WHERE role = 'counciler' AND hm_id = @hid"
        getHMCmd = New SqlCommand(getHMSql, conn)

        Dim updateUserSql As String = "UPDATE user_data SET username = @unam, user_email = @uem, user_numbers = @unum WHERE user_id = @uid"
        updateUserCmd = New SqlCommand(updateUserSql, conn)

        Dim checkUserSql As String = "SELECT * FROM user_data WHERE username = @unam OR user_email = @uem OR user_numbers = @unum"
        checkUserCmd = New SqlCommand(checkUserSql, conn)

        Dim updateHMSql As String = "UPDATE AdminHead SET hm_username = @hun, hm_email = @hem, hm_phone = @hph WHERE hm_id = @hid"
        updateHMCmd = New SqlCommand(updateHMSql, conn)

        Dim checkHMSql As String = "SELECT * FROM AdminHead WHERE hm_username = @us OR hm_email = @hme OR hm_phone = @hmp"
        checkHMCmd = New SqlCommand(checkHMSql, conn)

        Dim deleteUserSql As String = "DELETE user_data WHERE user_id = @uid"
        deleteUserCmd = New SqlCommand(deleteUserSql, conn)

        Dim checkBookingSql As String = "SELECT * FROM booking where user_id = @uid"
        checkBookingCmd = New SqlCommand(checkBookingSql, conn)

        Dim deleteHMSql As String = "DELETE AdminHead WHERE hm_id = @hid"
        deleteHMCmd = New SqlCommand(deleteHMSql, conn)

        Dim checkSchoolSql As String = "SELECT * FROM AdminHead WHERE hm_id = @hid AND school_num IS NULL"
        checkSchoolCmd = New SqlCommand(checkSchoolSql, conn)

    End Sub

    Protected Sub btn_searchUsers_Click(sender As Object, e As EventArgs) Handles btn_searchUsers.Click
        pnl_searchUser.Visible = True
    End Sub

    Protected Sub btn_searchHM_Click(sender As Object, e As EventArgs) Handles btn_searchHM.Click
        pnl_HMsearch.Visible = True
    End Sub

    Protected Sub btn_Userid_Click(sender As Object, e As EventArgs) Handles btn_Userid.Click
        Dim id As Integer = Integer.Parse(txt_userID.Text)

        getUserCmd.Parameters.Clear()
        getUserCmd.Parameters.AddWithValue("uid", id)

        Dim adap As SqlDataAdapter = New SqlDataAdapter(getUserCmd)
        Dim ds As DataSet = New DataSet
        adap.Fill(ds, "user")
        Dim dt As DataTable = ds.Tables("user")

        If dt.Rows.Count < 1 Then
            MsgBox("Error, either that id is non-existent or an error in dt")
        Else
            Dim dr As DataRow = dt.Rows(0)
            Dim name As String = dr("username")
            Dim email As String = dr("user_email")
            Dim num As String = dr("user_numbers")

            txt_UserName.Text = name
            txt_email.Text = email
            txt_number.Text = num
            pnl_users.Visible = True
            txt_userID.Enabled = False
        End If

    End Sub

    Protected Sub btn_HMSearch_Click(sender As Object, e As EventArgs) Handles btn_HMSearch.Click
        Dim id As Integer = Integer.Parse(txt_HMID.Text)

        If id = 1 Then
            MsgBox("Nothing here")
            Exit Sub
        End If

        getHMCmd.Parameters.Clear()
        getHMCmd.Parameters.AddWithValue("hid", id)

        Dim adap As SqlDataAdapter = New SqlDataAdapter(getHMCmd)
        Dim ds As DataSet = New DataSet()
        adap.Fill(ds, "HM")
        Dim dt As DataTable = ds.Tables("HM")

        If dt.Rows.Count < 1 Then
            MsgBox("Error when getting the headmasters data")
        Else
            Dim dr As DataRow = dt.Rows(0)
            Dim user As String = dr("hm_username")
            Dim email As String = dr("hm_email")
            Dim phone As String = dr("hm_phone")

            txt_HMName.Text = user
            txt_HMEmail.Text = email
            txt_HMnum.Text = phone
            pnl_HM.Visible = True
            txt_HMID.Enabled = False
        End If

    End Sub

    Protected Sub btn_editUser_Click(sender As Object, e As EventArgs) Handles btn_editUser.Click
        Dim id As Integer = Integer.Parse(txt_userID.Text)
        Dim user As String = txt_UserName.Text
        Dim email As String = txt_email.Text
        Dim phone As String = txt_number.Text

        checkUserCmd.Parameters.Clear()
        checkUserCmd.Parameters.AddWithValue("unam", user)
        checkUserCmd.Parameters.AddWithValue("uem", email)
        checkUserCmd.Parameters.AddWithValue("unum", phone)

        Dim adap As SqlDataAdapter = New SqlDataAdapter(checkUserCmd)
        Dim ds As DataSet = New DataSet
        adap.Fill(ds, "check")
        Dim dt As DataTable = ds.Tables("check")

        If dt.Rows.Count < 1 Then
            MsgBox("Error, there are users with the smae name/email/numbers")
            Exit Sub
        End If

        updateUserCmd.Parameters.Clear()
        updateUserCmd.Parameters.AddWithValue("unam", user)
        updateUserCmd.Parameters.AddWithValue("uem", email)
        updateUserCmd.Parameters.AddWithValue("unum", phone)
        updateUserCmd.Parameters.AddWithValue("uid", id)

        Dim rowsAff As Integer = updateUserCmd.ExecuteNonQuery
        If rowsAff < 1 Then
            MsgBox("Error, Something's wrong while updating data")
        Else
            MsgBox("Success, refreshing")
            Response.Redirect("AdminUser.aspx")
        End If
    End Sub

    Protected Sub btn_cancel_Click(sender As Object, e As EventArgs) Handles btn_cancel.Click
        pnl_HM.Visible = False
        pnl_users.Visible = False
        pnl_HMsearch.Visible = False
        pnl_searchUser.Visible = False
        wipeData()
    End Sub

    Private Sub wipeData()
        txt_email.Text = ""
        txt_HMEmail.Text = ""
        txt_HMID.Text = ""
        txt_HMName.Text = ""
        txt_HMnum.Text = ""
        txt_number.Text = ""
        txt_userID.Text = ""
        txt_UserName.Text = ""
    End Sub

    Protected Sub btn_HMedit_Click(sender As Object, e As EventArgs) Handles btn_HMedit.Click
        Dim id As Integer = Integer.Parse(txt_HMID.Text)
        Dim name As String = txt_HMName.Text
        Dim email As String = txt_HMEmail.Text
        Dim num As String = txt_HMnum.Text

        updateHMCmd.Parameters.Clear()
        updateHMCmd.Parameters.AddWithValue("hid", id)
        updateHMCmd.Parameters.AddWithValue("hun", name)
        updateHMCmd.Parameters.AddWithValue("hem", email)
        updateHMCmd.Parameters.AddWithValue("hph", num)

        Dim rowsAff As Integer = updateHMCmd.ExecuteNonQuery
        If rowsAff < 1 Then
            MsgBox("Error, something's wrong while updating the user")
        Else
            MsgBox("Success, refreshing")
            Response.Redirect("AdminUser.aspx")
        End If
    End Sub

    Protected Sub btn_Userdelete_Click(sender As Object, e As EventArgs) Handles btn_Userdelete.Click
        Dim id As Integer = Integer.Parse(txt_userID.Text)

        checkBookingCmd.Parameters.Clear()
        checkBookingCmd.Parameters.AddWithValue("uid", id)
        Dim adap As SqlDataAdapter = New SqlDataAdapter(checkBookingCmd)
        Dim ds As DataSet = New DataSet
        adap.Fill(ds, "check")
        Dim dt As DataTable = ds.Tables("check")

        If dt.Rows.Count > 0 Then
            MsgBox("There are bookings made with this accounts")
            Exit Sub
        End If

        deleteUserCmd.Parameters.Clear()
        deleteUserCmd.Parameters.AddWithValue("uid", id)

        Dim rowsAff As Integer = deleteUserCmd.ExecuteNonQuery

        If rowsAff < 1 Then
            MsgBox("Error, something's wrong with the system")
        Else
            MsgBox("Success, the user is deleted")
            Response.Redirect("AdminUser.aspx")
        End If
    End Sub

    Protected Sub btn_HMdelete_Click(sender As Object, e As EventArgs) Handles btn_HMdelete.Click
        Dim id As Integer = Integer.Parse(txt_HMID.Text)

        checkSchoolCmd.Parameters.Clear()
        checkSchoolCmd.Parameters.AddWithValue("hid", id)
        Dim adap As SqlDataAdapter = New SqlDataAdapter(checkSchoolCmd)
        Dim ds As DataSet = New DataSet
        adap.Fill(ds, "check")
        Dim dt As DataTable = ds.Tables("check")

        If dt.Rows.Count > 0 Then
            MsgBox("Error, there is a school assigned to this teacher")
            Exit Sub
        End If

        deleteHMCmd.Parameters.Clear()
        deleteHMCmd.Parameters.AddWithValue("hid", id)

        Dim rowsAff As Integer = deleteHMCmd.ExecuteNonQuery()
        If rowsAff < 1 Then
            MsgBox("Error, there is an error when attempting to delete it")
        Else
            MsgBox("Success, HM deleted, refreshing")
            Response.Redirect("AdminUser.aspx")
        End If
    End Sub
End Class