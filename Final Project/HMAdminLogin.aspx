<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="HMAdminLogin.aspx.vb" Inherits="Final_Project.HMAdminLogin" %>

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
            <h2 class="auto-style1">badminton Booking | Special user login</h2>
            <p class="auto-style1">UserName:</p>
            <asp:TextBox ID="txt_username" runat="server"></asp:TextBox>
            <p class="auto-style1">Password:</p>
            <asp:TextBox ID="txt_pass" runat="server" TextMode="Password"></asp:TextBox>
            <br />
            <asp:HyperLink ID="hyperlink1" runat="server" NavigateUrl="~/HMForgotPass.aspx">Forgotten Password?</asp:HyperLink>
        </div>
            <asp:Button ID="btn_login" runat="server" Text="Login" />
&nbsp;
            <asp:Button ID="btn_resigter" runat="server" Text="Register" />
        </div>
    </form>
</body>
</html>
