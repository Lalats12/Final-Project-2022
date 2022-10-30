<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="HMRegister.aspx.vb" Inherits="Final_Project.HMRegister" %>

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
            <h2 class="auto-style1">badminton Booking| Headmaster Register</h2>
            <div class="auto-style1">
            Username:
            <br />
            <asp:TextBox ID="txt_username" runat="server"></asp:TextBox>
            <br />
            Password:
            <br />
            <asp:TextBox ID="txt_pass" runat="server"></asp:TextBox>
            <br />
                Re-enter password:
            <br />
            <asp:TextBox ID="txt_repass" runat="server"></asp:TextBox>
            <br />
            Email:
            <br />
            <asp:TextBox ID="txt_email" runat="server"></asp:TextBox>
            <br />
            Phone Numbers: <br />
            <asp:TextBox ID="txt_phoneNum" runat="server"></asp:TextBox>
            <br />
                Verify Pin(max 6 digits)(Note it down, There is no way of recovery): <br />
            <asp:TextBox ID="txt_pin" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="btn_register" runat="server" Text="Register" />
            </div>
        </div>
    </form>
</body>
</html>
