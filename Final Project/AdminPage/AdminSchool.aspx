<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AdminSchool.aspx.vb" Inherits="Final_Project.AdminSchool" %>

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
            <h1>badminton Booking | Schools page</h1>
            <asp:Button ID="btn_allSchools" runat="server" Text="List all schools" />
&nbsp;
            <asp:Button ID="btn_byID" runat="server" Text="Search by ID" />
&nbsp;
            <asp:Button ID="btn_searchTag" runat="server" Text="Search By tag" />
            <br />
            <br />
            <asp:Panel ID="pnl_listSchool" runat="server" Visible="False">
                <asp:ListBox ID="lst_Schools" runat="server" Height="167px" Width="198px" AutoPostBack="True"></asp:ListBox>
                <br />
                <asp:Button ID="btn_displaySchool" runat="server" Text="Display school" />
            </asp:Panel>
            <br />
            <asp:Panel ID="pnl_SearchOne" runat="server" Visible="False">
                School ID:<br />
                <asp:TextBox ID="txt_searchID" runat="server"></asp:TextBox>
                <br />
                <asp:Button ID="btn_searchID" runat="server" Text="Search" />
            </asp:Panel>
            <br />
            <asp:Panel ID="pnl_tag" runat="server" Visible="False">
                School Tag:<br />
                <asp:TextBox ID="txt_searchTag" runat="server"></asp:TextBox>
                <br />
                <asp:Button ID="btn_byTag" runat="server" Text="cs" />
            </asp:Panel>
            <br />
            <asp:Panel ID="Panel1" runat="server" Visible="False">
                HM ID:<br />
                <asp:TextBox ID="txt_IDHM" runat="server"></asp:TextBox>
                <br />
                School ID:<br />
                <asp:TextBox ID="txt_ID" runat="server" Enabled="False"></asp:TextBox>
                <br />
                School Name:
                <br />
                <asp:TextBox ID="txt_name" runat="server" AutoPostBack="True"></asp:TextBox>
                <br />
                School Location:
                <br />
                <asp:TextBox ID="txt_loc" runat="server" Height="108px" Width="245px" TextMode="MultiLine"></asp:TextBox>
                <asp:Label ID="lbl_locId" runat="server"></asp:Label>
                <br />
                School Tag:
                <br />
                <asp:TextBox ID="txt_tag" runat="server"></asp:TextBox>
                <br />
                <asp:Button ID="btn_update" runat="server" Text="Update" />
                &nbsp;
                <asp:Button ID="btn_delete" runat="server" Text="Delete" />
                &nbsp;
                <asp:Button ID="btn_refresh" runat="server" Text="Reset search" />
            </asp:Panel>
            <br />
            <br />

        </div>
    </form>
</body>
</html>
