<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CreateSchool.aspx.vb" Inherits="Final_Project.CreateSchool" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2 class="auto-style1">Batminton Booking | Add your school</h2>
            <br />
            <div class="center">
            School Name:<br /><asp:TextBox ID="TextBox1" runat="server"></asp:TextBox><br />
            School Location:<br /><asp:TextBox ID="TextBox2" runat="server"></asp:TextBox><br />
            School Tag:<br />
            <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox><br />
            <asp:Button ID="btn_courts" runat="server" Text="To your courts" />
            </div>
          </div>
    </form>
</body>
</html>
