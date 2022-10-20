<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ManageCourts.aspx.vb" Inherits="Final_Project.CreateCourts" %>

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
        <div class="auto-style1">
            <h2>Batminton Booking | Manage Courts </h2>
            <asp:Label ID="lbl_invisiSchool" runat="server" Visible="False"></asp:Label>
            <br />
            Court:<br />
            <asp:DropDownList ID="drp_courts" runat="server" AutoPostBack="True"></asp:DropDownList>
            <br />
            Status:<br />
            <asp:DropDownList ID="drp_availa" runat="server" Enabled="False">
                <asp:ListItem Value="1">Ready</asp:ListItem>
                <asp:ListItem Value="0">Not Ready</asp:ListItem>
            </asp:DropDownList>
            <br />
            <asp:Button ID="btn_add" runat="server" Text="Add " />
&nbsp;
            <asp:Button ID="btn_edit" runat="server" Text="Edit" />
&nbsp;
            <asp:Button ID="btn_editConfirm" runat="server" Text="Confirm" Visible="False" />
&nbsp;&nbsp;<asp:Button ID="btn_editCancel" runat="server" Text="Cancel" Visible="False" />
&nbsp;
            <asp:Button ID="btn_delete" runat="server" Text="Delete" />
            <br />
        </div>
        </div>
    </form>
</body>
</html>
