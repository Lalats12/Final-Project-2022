<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AdminNewSchool.aspx.vb" Inherits="Final_Project.AdminNewSchool" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: center">
            School Proporsal ID:<br />
            <asp:DropDownList ID="drp_proporsalID" runat="server" AutoPostBack="True">
            </asp:DropDownList>
            <br />
            User&#39;s full name:<br />
            <asp:TextBox ID="txt_name" runat="server"></asp:TextBox>
            <br />
            User&#39;s email:<br />
            <asp:TextBox ID="txt_email" runat="server"></asp:TextBox>
            <br />
            User&#39;s numbers:<br />
            <asp:TextBox ID="txt_num" runat="server"></asp:TextBox><br />
            User&#39;s school name:<br />
            <asp:TextBox ID="txt_schoolName" runat="server"></asp:TextBox><br />
            User&#39;s school address:<br />
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
            <br />
            <asp:Button ID="btn_delete" runat="server" Text="Delete" />
&nbsp;
            <asp:Button ID="btn_menu" runat="server" Text="Return to Main menu" />
        </div>
    </form>
</body>
</html>
