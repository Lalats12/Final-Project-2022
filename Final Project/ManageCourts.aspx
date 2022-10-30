<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ManageCourts.aspx.vb" Inherits="Final_Project.CreateCourts" %>

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
        <div class="auto-style1">
            <h2>badminton Booking | Manage Courts </h2>
            <asp:Label ID="lbl_invisiSchool" runat="server" Visible="False"></asp:Label>
            <br />
            Court:<br />
            <asp:DropDownList ID="drp_courts" runat="server" AutoPostBack="True"></asp:DropDownList>
            <br />
            Status:<br />
            <asp:DropDownList ID="drp_availa" runat="server" Enabled="False">
                <asp:ListItem Value="1">Ready</asp:ListItem>
                <asp:ListItem Value="0">Not Ready</asp:ListItem>
            </asp:DropDownList>
            <br />
            Time start:<br />
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
            Time end:<br />
            <asp:DropDownList ID="drp_end_time" runat="server">
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
            <asp:Button ID="btn_add" runat="server" Text="Add " />
&nbsp;
            <asp:Button ID="btn_edit" runat="server" Text="Edit" />
&nbsp;
            <asp:Button ID="btn_editConfirm" runat="server" Text="Confirm" Visible="False" />
&nbsp;&nbsp;<asp:Button ID="btn_editCancel" runat="server" Text="Cancel" Visible="False" />
&nbsp;
            <asp:Button ID="btn_delete" runat="server" Text="Delete" />
            <br />
        </div>
        </div>
    </form>
</body>
</html>
