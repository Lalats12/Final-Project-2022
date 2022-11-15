<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AdminResponse.aspx.vb" Inherits="Final_Project.AdminResponse" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="text-align: center">
    <form id="form1" runat="server">
        <div>
            Response Id:<br />
            <asp:DropDownList ID="drp_idlst" runat="server" AutoPostBack="True">
            </asp:DropDownList>
            <br />
            Name:
            <asp:TextBox ID="txt_name" runat="server"></asp:TextBox>
            <br />
            Response:<br />
            <asp:TextBox ID="txt_response" runat="server" Height="200px" TextMode="MultiLine" Width="400px"></asp:TextBox>
            <br />
            <asp:Button ID="btn_delete" runat="server" Text="Delete" />
            &nbsp;
            <asp:Button ID="btn_main_menu" runat="server" Text="Return to Main menu" />
        </div>
    </form>
</body>
</html>
