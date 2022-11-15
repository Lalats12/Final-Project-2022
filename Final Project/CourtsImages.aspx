<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CourtsImages.aspx.vb" Inherits="Final_Project.CourtsImages" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: center">
            <h2>School Courts Preview</h2>
            <asp:ListBox ID="lst_schools" runat="server" AutoPostBack="True" Height="157px" style="margin-right: 8px" Width="157px"></asp:ListBox>
        &nbsp;&nbsp;
            <asp:Image ID="img_schoolPreview" runat="server" Height="156px" Width="175px" />
            <br />
            <br />
            <asp:Button ID="btn_book" runat="server" Text="Book Now" />
        </div>
    </form>
</body>
</html>
