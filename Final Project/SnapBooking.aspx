<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SnapBooking.aspx.vb" Inherits="Final_Project.SnapBooking" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Batminton booking | Obtain booking certificate</title>
    <style type="text/css">
        #form1 {
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Batminton booking | Obtain booking proof</h2>
            <p>Use this to snap and show it to people in charge</p>
            <asp:DropDownList ID="drp_booking" runat="server" AutoPostBack="True">
            </asp:DropDownList>
            <br />
            <br />
            Your ID:<asp:Label ID="lbl_ID" runat="server" Text=""></asp:Label>

            <br />
            <br />
            Your name:<asp:Label ID="lbl_name" runat="server" Text=""></asp:Label>

            <br />

            <br />
            Your email: <asp:Label ID="lbl_email" runat="server"></asp:Label>

            <br />
            <br />
            Your phone: <asp:Label ID="lbl_phone" runat="server"></asp:Label>

            <br />
            <br />
            Court ID: <asp:Label ID="lbl_court" runat="server"></asp:Label>

        &nbsp;<br />
            <br />
            School name: <asp:Label ID="lbl_school" runat="server"></asp:Label>

        &nbsp;<br />
            <br />
            Start date: <asp:Label ID="lbl_start_date" runat="server"></asp:Label>

        &nbsp;<br />
            <br />
            End date: <asp:Label ID="lbl_end_date" runat="server"></asp:Label>

        &nbsp;<br />

        </div>
    </form>
</body>
</html>
