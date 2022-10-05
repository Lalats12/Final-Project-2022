<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="main_page.aspx.vb" Inherits="Final_Project.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Badminton Booking Centre | Main Page</title>
    <style type="text/css">
        .auto-style1 {
            text-align: center;
        }
        .venue{
            display:grid;
            place-items:center;
            width:100vw;
        }
        .center{
            display:grid;
            place-items:center;
        }
        .tables{
            border:1px solid black;
            border-collapse:collapse;
        }
        tr:not(:last-child){
            border-bottom: 1px solid black;
        }
        td{
            border-right: 1px solid black;
        }
        .auto-style2 {
            text-align: left;
        }
        .auto-style3 {
            position: absolute;
            left: 973px;
            top: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="auto-style1">
        <div class="auto-style1">
            <div class="header">
            <h6 class="auto-style2"><asp:Label ID="lbl_userId" runat="server" Text="Welcome, "></asp:Label></h6><asp:Button ID="btn_logout" runat="server" Text="Log out" CssClass="auto-style3"/>
            </div>
            <h1>Venue</h1>
            <p>Current status:<asp:Label ID="lblVenue" runat="server" Text="Label"></asp:Label> </p>
        </div>
        <h2 class="auto-style1">Your booked list</h2>
        <asp:Label ID="lblNoBooks" runat="server" Text="There's no bookings made" Visible="False" Enabled="False"></asp:Label>
        <div class="venue">
            <asp:Table ID="user_booked_tables" runat="server" CssClass="tables"></asp:Table>
        </div>
        <h2 class="auto-style1">Available list</h2>
            <div class="center">
            <asp:Panel ID="Panel1" runat="server">
                <asp:Calendar ID="cal_venue" runat="server"></asp:Calendar>
                <br />
                <asp:DropDownList ID="DropDownList1" runat="server">
                    <asp:ListItem>1</asp:ListItem>
                    <asp:ListItem>2</asp:ListItem>
                    <asp:ListItem>3</asp:ListItem>
                    <asp:ListItem>4</asp:ListItem>
                    <asp:ListItem>5</asp:ListItem>
                    <asp:ListItem>6</asp:ListItem>
                    <asp:ListItem>7</asp:ListItem>
                    <asp:ListItem>8</asp:ListItem>
                    <asp:ListItem>9</asp:ListItem>
                    <asp:ListItem>10</asp:ListItem>
                    <asp:ListItem>11</asp:ListItem>
                    <asp:ListItem>12</asp:ListItem>
                </asp:DropDownList>
                &nbsp; :&nbsp;
                <asp:DropDownList ID="DropDownList2" runat="server">
                    <asp:ListItem>00</asp:ListItem>
                    <asp:ListItem>15</asp:ListItem>
                    <asp:ListItem>30</asp:ListItem>
                    <asp:ListItem>45</asp:ListItem>
                </asp:DropDownList>
                &nbsp;
                <asp:DropDownList ID="DropDownList3" runat="server">
                    <asp:ListItem>AM</asp:ListItem>
                    <asp:ListItem>PM</asp:ListItem>
                </asp:DropDownList>
            </asp:Panel>
                </div>
        <div class="venue">
            <asp:Table ID="available_venue" runat="server" CssClass="tables">
                <asp:TableRow runat="server">
                    <asp:TableCell ID="th_name" runat="server">School Name</asp:TableCell>
                    <asp:TableCell ID="th_school" runat="server">School Location</asp:TableCell>
                    <asp:TableCell ID="th_curr" runat="server">Courts</asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <asp:Button ID="btn_booking" runat="server" Text="Button" />
        </div>
        </div>
    </form>
</body>
</html>
