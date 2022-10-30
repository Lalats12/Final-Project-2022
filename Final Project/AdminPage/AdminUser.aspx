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
            <asp:Button ID="btn_searchHM" runat="server" Text="Search Headmaster" />
            <br />
            <br />
            <asp:Panel ID="pnl_searchUser" runat="server" Visible="False">
                User ID:<br />
                <asp:TextBox ID="txt_userID" runat="server" TextMode="Number"></asp:TextBox>
                <br />
                <asp:Button ID="btn_Userid" runat="server" Text="Search User" />
            </asp:Panel>
            <br />
            <asp:Panel ID="pnl_HMsearch" runat="server" Visible="False">
                HM user ID:<br />
                <asp:TextBox ID="txt_HMID" runat="server" TextMode="Number"></asp:TextBox>
                <br />
                <asp:Button ID="btn_HMSearch" runat="server" Text="Search HM" style="height: 26px" />
            </asp:Panel>
            <br />
            <asp:Panel ID="pnl_users" runat="server" Visible="False">
                Username:<br />
                <asp:TextBox ID="txt_UserName" runat="server"></asp:TextBox>
                <br />
                Email:<br />
                <asp:TextBox ID="txt_email" runat="server"></asp:TextBox>
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
            <asp:Panel ID="pnl_HM" runat="server" Visible="False">
                HM Username:<br />
                <asp:TextBox ID="txt_HMName" runat="server"></asp:TextBox>
                <br />
                HM Email:<br />
                <asp:TextBox ID="txt_HMEmail" runat="server"></asp:TextBox>
                <br />
                HM Number:<br />
                <asp:TextBox ID="txt_HMnum" runat="server"></asp:TextBox>
                <br />
                <br />
                <asp:Button ID="btn_HMedit" runat="server" Text="Update" />
                &nbsp;
                <asp:Button ID="btn_HMdelete" runat="server" Text="Delete" />
                <br />
            </asp:Panel>
            <asp:Button ID="btn_cancel" runat="server" Text="Cancel" />
            <br />
        </div>
    </form>
</body>
</html>
