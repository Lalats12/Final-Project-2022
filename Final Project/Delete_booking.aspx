<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Delete_booking.aspx.vb" Inherits="Final_Project.Delete_booking" %>

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
        <h1 class="auto-style1">Batminton Booking | Delete</h1>
        <div class="auto-style1">
        <br />
        <asp:ListBox ID="lst_booking" runat="server" Height="159px" Width="222px" AutoPostBack="True"></asp:ListBox>
            <br />
            <br />
            <asp:Button ID="btn_del" runat="server" Text="Delete" />
            <asp:Button ID="btn_mainMenu" runat="server" Text="Return to main " />
        &nbsp;
            <asp:Label ID="lbl_Test" runat="server" Text="Label"></asp:Label>
        </div>
        <div>
        </div>
    </form>
</body>
</html>
