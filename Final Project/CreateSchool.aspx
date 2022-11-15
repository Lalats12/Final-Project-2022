<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CreateSchool.aspx.vb" Inherits="Final_Project.CreateSchool" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Batminton booking | Create school</title>
    <style type="text/css">
        .auto-style1 {
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="auto-style1">
            <h2 class="auto-style1">badminton Booking | Add your school</h2>
            <div class="auto-style1">
            <br />
            </div>
            <div class="auto-style1">
            School Name:<br /><asp:TextBox ID="txt_school" runat="server" AutoPostBack="True"></asp:TextBox><br />
            School Location:<br /><asp:TextBox ID="txt_location" runat="server" Height="90px" Width="401px" TextMode="MultiLine"></asp:TextBox><br />
            School Tag(Change only if error):<br />
            <asp:TextBox ID="txt_tag" runat="server"></asp:TextBox>
                <br />
                Image upload:<br />
            <asp:FileUpload ID="file_up" runat="server" />
                <br />
                Headmaster&#39;s name:<br />
            <asp:TextBox ID="txt_hmname" runat="server"></asp:TextBox>
                <br />
                Headmaster&#39;s email:<br />
            <asp:TextBox ID="txt_hmEmail" runat="server"></asp:TextBox>
                <br />
                Headmaster&#39;s phone:<br />
            <asp:TextBox ID="txt_HMphone" runat="server"></asp:TextBox><br />
            <asp:Button ID="btn_courts" runat="server" Text="To your courts" Enabled="False" />
            </div>
          </div>
    </form>
    <script src="JavaScript.js"></script>
</body>
</html>
