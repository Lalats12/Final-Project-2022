<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Log_In_Page.aspx.vb" Inherits="Final_Project.Log_In_Page" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Batminton Booking Page | Login</title>
    <style type="text/css">
        .auto-style1 {
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h1 class="auto-style1">Batminton Booking Page Area</h1>
        <div style="display:grid; place-items:center">
            Username:<asp:TextBox ID="txt_username" runat="server" MaxLength="48"></asp:TextBox>
            <br />
            Password:<asp:TextBox ID="txt_password" runat="server"></asp:TextBox>
            <br />
            <asp:HyperLink ID="hyp_forgot" runat="server" NavigateUrl="~/ForgotPass.aspx">Forgotten your password?</asp:HyperLink>
            <br />
            <asp:Button ID="btn_login" runat="server" Text="Login" />
            <asp:Button ID="btn_register" runat="server" Text="Register" />
        &nbsp;<br />
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/About_Us.html">About Us</asp:HyperLink>
        </div>
    </form>
</body>
</html>
