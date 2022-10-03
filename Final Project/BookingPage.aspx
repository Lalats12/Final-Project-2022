<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="BookingPage.aspx.vb" Inherits="Final_Project.BookingPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        form{
            display:grid;
            place-items:center;
        }
        .auto-style1 {
            height: 74px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="auto-style1">
            <asp:DropDownList ID="DropDownList1" runat="server">
            </asp:DropDownList>
            <br />
            <br />
            &nbsp;Day&nbsp;&nbsp;&nbsp; Month&nbsp; Year&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Hour Minute<br />
            <asp:TextBox ID="TextBox1" runat="server" Width="22px"></asp:TextBox> &nbsp; - <asp:TextBox ID="TextBox2" runat="server" Width="22px"></asp:TextBox>
        &nbsp;- <asp:TextBox ID="TextBox3" runat="server" Width="22px"></asp:TextBox>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:TextBox ID="TextBox4" runat="server" Width="22px"></asp:TextBox>
        &nbsp;: <asp:TextBox ID="TextBox5" runat="server" Width="22px"></asp:TextBox>
        </div>
    </form>
</body>
</html>
