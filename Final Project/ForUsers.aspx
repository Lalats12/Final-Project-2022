<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ForUsers.aspx.vb" Inherits="Final_Project.ForUsers" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #form1 {
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>HM school Registration</h2>
            Your full name:<br />
            <asp:TextBox ID="txt_name" runat="server"></asp:TextBox><br />
            Your email:<br />
            <asp:TextBox ID="txt_email" runat="server"></asp:TextBox><br />
            Your number:<br />
            <asp:TextBox ID="txt_nums" runat="server"></asp:TextBox><br />
            Your school name:<br />
            <asp:TextBox ID="txt_schoolName" runat="server"></asp:TextBox><br />
            Your school address:<br />
            <asp:TextBox ID="txt_schoolAdd" runat="server" Height="104px" TextMode="MultiLine" Width="292px"></asp:TextBox><br />
            Amount of courts:<br />
            <asp:TextBox ID="txt_courtsNum" runat="server" TextMode="Number"></asp:TextBox>
            <br />
            The Time to open:<br />
            <asp:DropDownList ID="drp_start_time" runat="server">
                <asp:ListItem>00</asp:ListItem>
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
                <asp:ListItem>13</asp:ListItem>
                <asp:ListItem>14</asp:ListItem>
                <asp:ListItem>15</asp:ListItem>
                <asp:ListItem>16</asp:ListItem>
                <asp:ListItem>17</asp:ListItem>
                <asp:ListItem>18</asp:ListItem>
                <asp:ListItem>19</asp:ListItem>
                <asp:ListItem>20</asp:ListItem>
                <asp:ListItem>21</asp:ListItem>
                <asp:ListItem>22</asp:ListItem>
                <asp:ListItem>23</asp:ListItem>
            </asp:DropDownList>
&nbsp;: 00<br />
            The time to close:<br />
            <asp:DropDownList ID="drp_close_time" runat="server">
                <asp:ListItem>00</asp:ListItem>
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
                <asp:ListItem>13</asp:ListItem>
                <asp:ListItem>14</asp:ListItem>
                <asp:ListItem>15</asp:ListItem>
                <asp:ListItem>16</asp:ListItem>
                <asp:ListItem>17</asp:ListItem>
                <asp:ListItem>18</asp:ListItem>
                <asp:ListItem>19</asp:ListItem>
                <asp:ListItem>20</asp:ListItem>
                <asp:ListItem>21</asp:ListItem>
                <asp:ListItem>22</asp:ListItem>
                <asp:ListItem>23</asp:ListItem>
            </asp:DropDownList>
&nbsp;: 00<br />
            The secret code:<br />
            <asp:TextBox ID="txt_code" runat="server"></asp:TextBox><br />
            <asp:Button Text="Enter" ID="btn_enter" runat="server" />
        </div>
    </form>
</body>
</html>
