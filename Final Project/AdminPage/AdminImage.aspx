<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AdminImage.aspx.vb" Inherits="Final_Project.AdminImageaspx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: center">
            <h2>Batminton Booking | Change Image</h2>
            School:
            <asp:DropDownList ID="drp_schools" runat="server" AutoPostBack="True"></asp:DropDownList>
            <br />
            <asp:Image ID="img_school" runat="server" Height="211px" Width="225px" />
            <br />
            New Image:<br />
            <asp:FileUpload ID="file_up" runat="server" />
            <br />
            <br />
            <asp:Button ID="btn_imgChange" runat="server" Text="Change Image" Enabled="False" />
            &nbsp;
            <asp:Button ID="btn_return" runat="server" Text="Return to school" />
        </div>
    </form>
</body>
</html>
