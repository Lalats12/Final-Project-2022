<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Register_page.aspx.vb" Inherits="Final_Project.Register_page" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            text-align: center;
        }
        .register_box{
            display:grid;
            place-items:center;
            border:3px dashed black;
            border-radius: 250px;
        }
    </style>
</head>
<body>
    <h1 class="auto-style1">Register for the account</h1>
    <form id="form1" runat="server">
        <div class="auto-style1">
        <div class="register_box">
             <h3>E-Mail:   <asp:TextBox ID="txt_email" runat="server"></asp:TextBox></h3>
             <h3>UserName: <asp:TextBox ID="txt_username" runat="server"></asp:TextBox></h3>
             <h3>Password: <asp:TextBox ID="txt_pass" runat="server"></asp:TextBox></h3>
        </div>
        <asp:Button ID="btn_register" runat="server" Text="Register" />
        </div>
    </form>
</body>
</html>
