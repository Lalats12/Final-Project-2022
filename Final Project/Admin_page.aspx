<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Admin_page.aspx.vb" Inherits="Final_Project.Admin_page" %>

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
            <h2>Badminton Booking | Admin Sector</h2>
            <asp:Button ID="btn_checkUsers" runat="server" Text="Check the user list" />
&nbsp;
            <asp:Button ID="btn_booking" runat="server" Text="Check the boooking list" />
            &nbsp;
            <asp:Button ID="btn_schools" runat="server" Text="Check the school list"/>
        </div>
    </form>
</body>
</html>
