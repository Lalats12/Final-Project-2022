<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Starting_Page.aspx.vb" Inherits="Final_Project.Starting_Page" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="text-align: center">
    <form id="form1" runat="server">
        <div>
            <h2>Badminton Booking Page | Welcome Screen</h2>
            <h4>The mission</h4>
            <h5>To help the chinese schools</h5>
            <asp:Button ID="Button1" runat="server" Text="Login as user" Height="48px" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button2" runat="server" Text="Login" Height="48px" />
            <br />
            <br />
            Are you interested for your school to join us? <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/ForUsers.aspx">Then Contact us</asp:HyperLink></div>
        <asp:HyperLink NavigateUrl="~/About_Us.aspx" runat="server" >About us</asp:HyperLink>
        <br />
        New Here? <asp:HyperLink NavigateUrl="~/NewUsers.aspx" runat="server" >Click Here!</asp:HyperLink>
    </form>
</body>
</html>
