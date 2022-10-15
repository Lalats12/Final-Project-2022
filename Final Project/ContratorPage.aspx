<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ContratorPage.aspx.vb" Inherits="Final_Project.ContratorPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            text-align: center;
        }
        .absolute{
            position:absolute;
            float:right;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div><asp:Label ID="Label1" runat="server" Text="Welcome" CssClass="absolute"></asp:Label></div>
        <div class="auto-style1"><h2>Batminton Booking | Headmaster Sector</h2>
        <asp:Label ID="Label2" runat="server" Text="Your school is not signed up,"></asp:Label>
            <br />
            <asp:Button ID="btn_signUp" runat="server" Text="Sign up now" />
            <br />
            <br />
            <asp:Panel ID="pnl_school" runat="server" Height="280px" Width="100%">
                <br />
                Your school name:<br />
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                <br />
                Your school location:<br />
                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                <br />
                No. of courts:<br />
                <asp:TextBox ID="TextBox3" runat="server" Enabled="False"></asp:TextBox>
                <br />
                <asp:Button ID="btn_courts" runat="server" Text="Manage your courts" />
                <br />
                <asp:Button ID="btn_update" runat="server" Text="Update School" />
                <br />
            </asp:Panel>
            <br />
            <br />
        </div>
    </form>
</body>
</html>
