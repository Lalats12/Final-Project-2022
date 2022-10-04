<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="main_page.aspx.vb" Inherits="Final_Project.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Badminton Booking Centre | Main Page</title>
    <style type="text/css">
        .auto-style1 {
            text-align: center;
        }
        #table_venue{
            display:grid;
            place-items:center;
            width:100vw;
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
            <div class="header">
            <h6 class="auto-style2"><asp:Label ID="lbl_userId" runat="server" Text="Welcome, "></asp:Label></h6><asp:Button ID="btn_logout" runat="server" Text="Log out" CssClass="auto-style3"/>
            </div>
            <h1>Venue</h1>
            <p>Current status:<asp:Label ID="lblVenue" runat="server" Text="Label"></asp:Label> </p>
        </div>
        <div id="book_venue">
            <asp:Table ID="user_booked_tables" runat="server"></asp:Table>
        </div>
        <div id="table_venue">
            <asp:Table ID="available_venue" runat="server" CssClass="tables">
                <asp:TableRow runat="server">
                    <asp:TableCell ID="th_name" runat="server">School Name</asp:TableCell>
                    <asp:TableCell ID="th_school" runat="server">School Location</asp:TableCell>
                    <asp:TableCell ID="th_curr" runat="server">Courts</asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <asp:Button ID="btn_booking" runat="server" Text="Button" />
        </div>
    </form>
</body>
</html>
