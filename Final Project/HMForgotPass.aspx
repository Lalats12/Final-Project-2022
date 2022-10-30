<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="HMForgotPass.aspx.vb" Inherits="Final_Project.HMForgotPass" %>

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
            <h1 >badminton Booking | Forgot Password</h1>
            <br />Username:&nbsp;&nbsp;
            <asp:TextBox ID="txt_username" runat="server"></asp:TextBox>
            <br />
            Email:&nbsp;
            <asp:TextBox ID="txt_email" runat="server"></asp:TextBox>
            <br />
&nbsp;<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/HMForgotName.aspx">Forgot Username</asp:HyperLink>
            <br />
            <asp:Button ID="btn_send" runat="server" Text="Send to email" />
&nbsp;<asp:Button ID="btn_menu" runat="server" Text="Return to menu" />
        </div>
    </form>
</body>
</html>
