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
            <asp:Table ID="user_booked_tables" runat="server" CssClass="tables">
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server">Venue Id</asp:TableCell>
                    <asp:TableCell runat="server">School Location</asp:TableCell>
                    <asp:TableCell runat="server">Start Date</asp:TableCell>
                    <asp:TableCell runat="server">End date</asp:TableCell>
                    <asp:TableCell runat="server">Payment Date</asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <asp:Button ID="btn_edit" runat="server" Text="Edit your books" />
            <asp:Button ID="btn_delete" runat="server" Text="Delete" />
        </div>
        <h2 class="auto-style1">Available list</h2>
            <div class="center">
            <asp:Panel ID="Panel1" runat="server">
                <asp:Calendar ID="cal_venue" runat="server" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" Width="200px">
                    <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                    <NextPrevStyle VerticalAlign="Bottom" />
                    <OtherMonthDayStyle ForeColor="#808080" />
                    <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                    <SelectorStyle BackColor="#CCCCCC" />
                    <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                    <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                    <WeekendDayStyle BackColor="#FFFFCC" />
                </asp:Calendar>
                Start Time<br />
                <asp:DropDownList ID="drp_start_hr" runat="server">
                    <asp:ListItem>01</asp:ListItem>
                    <asp:ListItem>02</asp:ListItem>
                    <asp:ListItem>03</asp:ListItem>
                    <asp:ListItem>04</asp:ListItem>
                    <asp:ListItem>05</asp:ListItem>
                    <asp:ListItem>06</asp:ListItem>
                    <asp:ListItem>07</asp:ListItem>
                    <asp:ListItem>08</asp:ListItem>
                    <asp:ListItem>09</asp:ListItem>
                    <asp:ListItem>10</asp:ListItem>
                    <asp:ListItem>11</asp:ListItem>
                    <asp:ListItem>12</asp:ListItem>
                </asp:DropDownList>
                &nbsp; :&nbsp; 00&nbsp;
                <asp:DropDownList ID="drp_start_ampm" runat="server">
                    <asp:ListItem>AM</asp:ListItem>
                    <asp:ListItem>PM</asp:ListItem>
                </asp:DropDownList>
                <br />
                End time<br />
                <asp:DropDownList ID="drp_end_hr" runat="server">
                    <asp:ListItem>01</asp:ListItem>
                    <asp:ListItem>02</asp:ListItem>
                    <asp:ListItem>03</asp:ListItem>
                    <asp:ListItem>04</asp:ListItem>
                    <asp:ListItem>05</asp:ListItem>
                    <asp:ListItem>06</asp:ListItem>
                    <asp:ListItem>07</asp:ListItem>
                    <asp:ListItem>08</asp:ListItem>
                    <asp:ListItem>09</asp:ListItem>
                    <asp:ListItem>10</asp:ListItem>
                    <asp:ListItem>11</asp:ListItem>
                    <asp:ListItem>12</asp:ListItem>
                </asp:DropDownList>
                &nbsp; :&nbsp; 00
                <asp:DropDownList ID="drp_end_ampm" runat="server">
                    <asp:ListItem>AM</asp:ListItem>
                    <asp:ListItem>PM</asp:ListItem>
                </asp:DropDownList>
                &nbsp;<asp:CheckBox ID="chk_next_day" runat="server" Text="Next Day" />
                <br />
                <asp:Button ID="btn_check" runat="server" Text="Check Booking" />
                &nbsp;
                <asp:Button ID="btn_cancel" runat="server" Text="Cancel" Visible="False" />
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
            <asp:Button ID="btn_booking" runat="server" Text="Book now" />
            <asp:Label ID="lbl_test" runat="server"></asp:Label>
        </div>
        </div>
    </form>
</body>
</html>
