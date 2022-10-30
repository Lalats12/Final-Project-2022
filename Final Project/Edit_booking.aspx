<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Edit_booking.aspx.vb" Inherits="Final_Project.Edit_bookingaspx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>badminton Booking | Edit your books</title>
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
            <h2>Edit your Bookings</h2>
            Your Books<br />
            <asp:DropDownList ID="drp_user_booking" runat="server" AutoPostBack="True">
                <asp:ListItem>(Select)</asp:ListItem>
            </asp:DropDownList>
            <h2>School Name</h2>
            <asp:Label ID="lbl_choose" runat="server" Text="You choose: " Visible="False"></asp:Label>
            <br />
            <asp:DropDownList ID="drp_school" runat="server" AutoPostBack="True">
                <asp:ListItem Value="(Select)"></asp:ListItem>
            </asp:DropDownList>
            <br />
            <br />
            <h2>Court Number</h2>
            <asp:DropDownList ID="drp_court" runat="server" AutoPostBack="True">
                <asp:ListItem>(select)</asp:ListItem>
            </asp:DropDownList>
            <br />
            Maximum Allocated time is 3 Hours<br />
            Maximum Forward booking is 3 months<br />
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
            Court Available At:
            <asp:Label ID="lbl_courtTime" runat="server"></asp:Label>
            <br />
            Starts at:<br />
            <asp:DropDownList ID="start_time_hr" runat="server">
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
            </asp:DropDownList>&nbsp;: 00&nbsp;<asp:DropDownList ID="start_time_ampm" runat="server">
                <asp:ListItem>AM</asp:ListItem>
                <asp:ListItem>PM</asp:ListItem>
            </asp:DropDownList>
            <br />
            Ends at<br />
            <asp:DropDownList ID="end_time_hr" runat="server">
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
            </asp:DropDownList>&nbsp;: 00&nbsp;<asp:DropDownList ID="end_time_ampm" runat="server">
                <asp:ListItem>AM</asp:ListItem>
                <asp:ListItem>PM</asp:ListItem>
            </asp:DropDownList>
&nbsp;<asp:CheckBox ID="chk_nextDay" runat="server" Text="Next Day" />
            <br />
            Bank Card:<br />
            <asp:DropDownList ID="drp_card_type" runat="server" AutoPostBack="True">
                <asp:ListItem>Maybank</asp:ListItem>
                <asp:ListItem>OCBC</asp:ListItem>
                <asp:ListItem>VISA</asp:ListItem>
            </asp:DropDownList>
            <br />
            Card number(Vaild number: XXXX-XXXX-XXXX-XXXX):<br />
            <asp:TextBox ID="txt_cardNum" runat="server"></asp:TextBox>
            <br />
            Expire date<br />
            <asp:Calendar ID="cal_expire_date" runat="server" BackColor="White" BorderColor="#999999" Caption="Choose the date" CaptionAlign="Top" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" SelectedDate="2022-10-05" Width="200px">
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
            Security number(Vaild numbers: XXXX):<br />
            <asp:TextBox ID="txt_security" runat="server"></asp:TextBox>
            <br />
            Your amount(Min: RM5.00):<br />
            <asp:TextBox ID="txt_donate" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="btn_edit" runat="server" Text="Edit" />
            &nbsp;&nbsp;
            <asp:Button ID="btn_return" runat="server" Text="Return to menu" />
            <asp:Label ID="lbl_results" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>
