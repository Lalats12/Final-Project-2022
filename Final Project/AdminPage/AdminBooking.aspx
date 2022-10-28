<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AdminBooking.aspx.vb" Inherits="Final_Project.AdminBooking" %>

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
            <h1>Batminton Booking | Booking management</h1>
            <asp:Button ID="btn_all" runat="server" Text="Select All" />
&nbsp;
            <asp:Button ID="btn_selectSearch" runat="server" Text="Search By ID" />
            &nbsp;<asp:Button ID="btn_searchCourts" runat="server" Text="Search By Courts" />
            <br />
            <asp:Panel ID="pan_search" runat="server" Visible="False">
                Booking ID:
                <br />
                <asp:TextBox ID="txt_searchID" runat="server"></asp:TextBox>
                <br />
                <asp:Button ID="btn_search" runat="server" Text="Search ID" />
            </asp:Panel>
            <br />
            <asp:Panel ID="pan_displayAll" runat="server" Visible="False">
                <asp:ListBox ID="lst_bookingIDs" runat="server" Height="175px" Width="245px" AutoPostBack="True"></asp:ListBox>
                <br />
                <asp:Button ID="btn_showContent" runat="server" Text="Select booking" />
            </asp:Panel><br />
            <asp:Panel ID="pnl_courts" runat="server" Visible="False">
                Court Search:<br />
                <asp:TextBox ID="txt_searchCourt" runat="server"></asp:TextBox>
                <br />
                <asp:Button ID="btn_searchCourt" runat="server" Text="Search" />
                <br />
                <asp:ListBox ID="lst_bookingCourts" runat="server" Height="195px" Width="239px" AutoPostBack="True"></asp:ListBox>
                <br />
                <asp:Button ID="btn_courtGetBook" runat="server" Text="Get Booking" />
            </asp:Panel>
            <br />
            <asp:Panel ID="Panel1" runat="server" Visible="False">
                User ID:<br />
                <asp:TextBox ID="txt_userID" runat="server" Enabled="False"></asp:TextBox>
                <br />
                <br />
                School Name:<br />
                <asp:DropDownList ID="drp_schools" runat="server" AutoPostBack="True">
                </asp:DropDownList>
                <br />
                <br />
                Court:<br />
                <asp:DropDownList ID="drp_courts" runat="server">
                </asp:DropDownList>
                <br />
                <br />
                Booking date:<br />
                <asp:Calendar ID="cal_Booking_date" runat="server" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" Width="200px">
                    <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                    <NextPrevStyle VerticalAlign="Bottom" />
                    <OtherMonthDayStyle ForeColor="#808080" />
                    <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                    <SelectorStyle BackColor="#CCCCCC" />
                    <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                    <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                    <WeekendDayStyle BackColor="#FFFFCC" />
                </asp:Calendar>
                Start Time:<br />
                <asp:DropDownList ID="start_date_hr" runat="server">
                    <asp:ListItem Value="01"></asp:ListItem>
                    <asp:ListItem Value="02"></asp:ListItem>
                    <asp:ListItem Value="03"></asp:ListItem>
                    <asp:ListItem Value="04"></asp:ListItem>
                    <asp:ListItem Value="05"></asp:ListItem>
                    <asp:ListItem Value="06"></asp:ListItem>
                    <asp:ListItem Value="07"></asp:ListItem>
                    <asp:ListItem Value="08"></asp:ListItem>
                    <asp:ListItem Value="09"></asp:ListItem>
                    <asp:ListItem Value="10"></asp:ListItem>
                    <asp:ListItem Value="11"></asp:ListItem>
                    <asp:ListItem Value="12"></asp:ListItem>
                </asp:DropDownList>
                &nbsp;: 00&nbsp;
                <asp:DropDownList ID="start_date_ampm" runat="server">
                    <asp:ListItem>AM</asp:ListItem>
                    <asp:ListItem>PM</asp:ListItem>
                </asp:DropDownList>
                <br />
                End Time:<br />
                <asp:DropDownList ID="end_date_hr" runat="server">
                    <asp:ListItem Value="01"></asp:ListItem>
                    <asp:ListItem Value="02"></asp:ListItem>
                    <asp:ListItem Value="03"></asp:ListItem>
                    <asp:ListItem Value="04"></asp:ListItem>
                    <asp:ListItem Value="05"></asp:ListItem>
                    <asp:ListItem Value="06"></asp:ListItem>
                    <asp:ListItem Value="07"></asp:ListItem>
                    <asp:ListItem Value="08"></asp:ListItem>
                    <asp:ListItem Value="09"></asp:ListItem>
                    <asp:ListItem Value="10"></asp:ListItem>
                    <asp:ListItem Value="11"></asp:ListItem>
                    <asp:ListItem Value="12"></asp:ListItem>
                </asp:DropDownList>
                &nbsp;: 00&nbsp;
                <asp:DropDownList ID="end_date_ampm" runat="server">
                    <asp:ListItem>AM</asp:ListItem>
                    <asp:ListItem>PM</asp:ListItem>
                </asp:DropDownList>
                &nbsp;<asp:CheckBox ID="chk_nextDay" runat="server" Text="Next Day" />
                <br />
                Payment ID:<br />
                <asp:TextBox ID="txt_paymentId" runat="server" ReadOnly="True"></asp:TextBox>
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
                <asp:Button ID="btn_update" runat="server" Text="Update" />
                &nbsp;<asp:Button ID="btn_delete" runat="server" Text="Delete" />
                &nbsp;<asp:Button ID="btn_research" runat="server" Text="Restart the searching" />
                <br />
                <br />
            </asp:Panel>

        </div>
    </form>
</body>
</html>
