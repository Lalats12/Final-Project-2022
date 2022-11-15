<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AdminUser.aspx.vb" Inherits="Final_Project.AdminUser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="auto-style1">
            <h1>badminton Booking | Users page</h1>
            <br />
            <asp:Button ID="btn_searchUsers" runat="server" Text="Search User" />
        &nbsp;
            <br />
            <br />
            <asp:Panel ID="pnl_searchUser" runat="server" Visible="False">
                User ID:<br />
                <asp:TextBox ID="txt_userID" runat="server" TextMode="Number"></asp:TextBox>
                <br />
                <asp:Button ID="btn_Userid" runat="server" Text="Search User" />
            </asp:Panel>
            <br />
            <asp:Panel ID="pnl_users" runat="server" Visible="False">
                Username:<br />
                <asp:TextBox ID="txt_UserName" runat="server"></asp:TextBox>
                <br />
                Email:<br />
                <asp:TextBox ID="txt_email" runat="server"></asp:TextBox>
                <br />
                Password:<br />
                <asp:TextBox ID="txt_pass" runat="server"></asp:TextBox>
                <br />
                Number:<br />
                <asp:TextBox ID="txt_number" runat="server"></asp:TextBox>
                <br />
                <br />
                <asp:Button ID="btn_editUser" runat="server" Text="Update" />
                &nbsp;
                <asp:Button ID="btn_Userdelete" runat="server" Text="Delete" />
            </asp:Panel>
            <br />
            <asp:Button ID="btn_cancel" runat="server" Text="Cancel" />
            &nbsp;
            <asp:Button ID="btn_main_menu" runat="server" Text="Return to Main menu" />
            <br />
        </div>
    </form>
</body>
</html>
