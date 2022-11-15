<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Manage_User.aspx.vb" Inherits="Final_Project.Manage_User" %>

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
<body style="margin-top: 15px">
    <form id="form1" runat="server">
        <div class="auto-style1">
            <h2 class="auto-style1">Booking Page | Manage your profile</h2>
            <div class="auto-style1">
            Your name:<br />
            <asp:TextBox ID="txt_name" runat="server"></asp:TextBox>
                <br />
                Your email:<br />
            <asp:TextBox ID="txt_email" runat="server"></asp:TextBox>
                <br />
                Your number:<br />
            <asp:TextBox ID="txt_number" runat="server"></asp:TextBox>
                <br />
                <br />
                New password:<br />
            <asp:TextBox ID="txt_pass" runat="server" TextMode="Password"></asp:TextBox>
                <br />
                Re-enter password:<br />
            <asp:TextBox ID="txt_repass" runat="server" TextMode="Password"></asp:TextBox>
                <br />
                <br />
                <asp:Button ID="btn_confirm" runat="server" Text="Confirm changes" />
&nbsp;
                <asp:Button ID="btn_main_menu" runat="server" Text="Return to main menu" />
            </div>
        </div>
    </form>
</body>
</html>
