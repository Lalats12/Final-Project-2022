<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="HMForgotName.aspx.vb" Inherits="Final_Project.HMForgotName" %>

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
            <h1>Batminton Booking | Forgot username</h1>
            <br />
            E-mail:&nbsp;&nbsp; <asp:TextBox ID="txt_email" runat="server"></asp:TextBox>
            <br />
            Phone:&nbsp;&nbsp; <asp:TextBox ID="txt_phone" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="btn_send" runat="server" Text="Send to Email" />
&nbsp;<asp:Button ID="btn_menu" runat="server" style="height: 26px" Text="Return to menu" />
        </div>
    </form>
</body>
</html>
