<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ForgotUser.aspx.vb" Inherits="Final_Project.ForgotUser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Batminton Booking | Forgot username</title>
    <style type="text/css">
        .auto-style1 {
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="auto-style1">
            <h2>Batminton Booking | Forgot username</h2>
            Email: <asp:TextBox ID="txt_email" runat="server"></asp:TextBox><br />
            Numbers:<asp:TextBox ID="txt_numbers" runat="server"></asp:TextBox><br />
            <asp:Button ID="btn_find" runat="server" Text="Find username" />
        &nbsp;<asp:Button ID="btn_return" runat="server" Text="Return to main menu" />
        </div>
    </form>
</body>
</html>
