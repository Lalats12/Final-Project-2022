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
        <div><asp:Label ID="lbl_Welcome" runat="server" Text="Welcome" CssClass="absolute"></asp:Label></div>
        <div class="auto-style1"><h2>Batminton Booking | Headmaster Sector</h2>
        <asp:Label ID="lbl_signSchool" runat="server" Text="Your school is not signed up,"></asp:Label>
            <br />
            <asp:Button ID="btn_signSchool" runat="server" Text="Sign up now" />
            <br />
            <br />
            <asp:Panel ID="pnl_school" runat="server" Height="280px" Width="100%">
                <br />
                Your school name:<br />
                <asp:TextBox ID="txt_school" runat="server" Enabled="False" AutoPostBack="True"></asp:TextBox>
                <br />
                Your school location:<br />
                <asp:TextBox ID="txt_school_loc" runat="server" Enabled="False" Height="94px" Width="360px" TextMode="MultiLine"></asp:TextBox>
                <asp:Label ID="txt_invisible_loctag" runat="server" Visible="False"></asp:Label>
                <asp:CheckBox ID="chk_sameLoc" runat="server" Text="Same location?" />
                <br />
                Your school tags:<br />
                <asp:TextBox ID="txt_tag" runat="server" Enabled="False"></asp:TextBox>
                <br />
                No. of courts:<br />
                <asp:TextBox ID="txt_schoolCourts" runat="server" Enabled="False"></asp:TextBox>
                <br />
                <asp:Button ID="btn_courts" runat="server" Text="Manage your courts" />
                <br />
                <asp:Button ID="btn_update" runat="server" Text="Update School" />
                &nbsp;
                <asp:Button ID="btn_confirm" runat="server" Text="Confirm" Visible="False" />
                &nbsp;
                <asp:Button ID="btn_cancel" runat="server" Text="Cancel" Visible="False" />
                <br />
            </asp:Panel>
            <br />
            <br />
        </div>
    </form>
</body>
</html>
