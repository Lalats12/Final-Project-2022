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
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="auto-style1">
            <h2>School Name</h2>
            <asp:DropDownList ID="drp_school" runat="server">
            </asp:DropDownList>
            <br />
            <br />
            <h2>Court Number</h2>
            <asp:DropDownList ID="drp_court" runat="server">
            </asp:DropDownList>
            <br />
            <br />
            <asp:Calendar ID="cal_booking_date" runat="server" BackColor="White" BorderColor="#999999" Caption="Choose the date" CaptionAlign="Top" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" SelectedDate="2022-10-05" Width="200px">
                <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                <NextPrevStyle VerticalAlign="Bottom" />
                <OtherMonthDayStyle ForeColor="#808080" />
                <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                <SelectorStyle BackColor="#CCCCCC" />
                <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                <WeekendDayStyle BackColor="#FFFFCC" />
            </asp:Calendar>
            <br />
            <br />
            <asp:Button ID="btn_book" runat="server" Text="Book" />
            <asp:Label ID="lbl_results" runat="server" Text="Label"></asp:Label>
        </div>
    </form>
</body>
</html>
