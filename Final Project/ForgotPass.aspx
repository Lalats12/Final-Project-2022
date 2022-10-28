<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ForgotPass.aspx.vb" Inherits="Final_Project.ForgotPass" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Batminton Booking | Password Reset</title>
    <style type="text/css">
        .auto-style1 {
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="auto-style1">
            <h1>Batminton Booking | Reset your password</h1>
        </div>
        <div class="auto-style1">
        Username:<asp:TextBox ID="txt_user" runat="server"></asp:TextBox>
        <br />
        Email:<asp:TextBox ID="txt_email" runat="server"></asp:TextBox>
        <br />
        <asp:HyperLink ID="hyp_user" runat="server" NavigateUrl="~/ForgotUser.aspx">Forgot your username?</asp:HyperLink>
        <br />
        <asp:Button ID="btn_reset" runat="server" Text="Check" />
    &nbsp;
        <asp:Button ID="btn_main_menu" runat="server" Text="Return to Log in screen" />
        </div>
    </form>
</body>
</html>
