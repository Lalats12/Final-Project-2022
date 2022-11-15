<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AdminCourts.aspx.vb" Inherits="Final_Project.SchoolCourts" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            text-align: center;
        }

        body  {
             background-image: url('../images/bg1.jpg');
             background-position: top;
             background-repeat: no-repeat;
             background-size: cover;
             color: white;
        }

        .button {
             height: 50px;
             width: 180px;
             background: #000;
             border: none;
             color: white;
             font-size: 1.25em;
             font-family: 'Nanum Gothic';
             border-radius: 25px;
             cursor: pointer;
        }

        .button:hover {
            background-color: #555;
        }

        .ogtitle {
              font-family: Roboto, sans-serif;
              color: white;
              font-size: 30px;
              margin-top:  100px;
              margin-left: 150px;
              margin-bottom: 100px;
        }

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="ogtitle">
            <h1>Badminton Booking |</h1>
            <h2>Court List</h2>
              </div> 
        <div class="auto-style1">
                        <br />

            School Name:<br />
            <asp:DropDownList ID="drp_schools" runat="server" AutoPostBack="True">
            </asp:DropDownList>
            <br />
            <br />
            Court number:<br />
            <asp:DropDownList ID="drp_courts" runat="server" AutoPostBack="True">
            </asp:DropDownList>
            <br />
            <br />
            Available:<br />
            <asp:DropDownList ID="drp_availa" runat="server">
                <asp:ListItem Value="1">Ready</asp:ListItem>
                <asp:ListItem Value="0">Not Ready</asp:ListItem>
            </asp:DropDownList>
                        <br />
            <br />            Time start:<br />
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
            <br />
            <asp:Button ID="btn_add" CssClass="button" runat="server" Text="Add Court" />
&nbsp;
            <asp:Button ID="btn_change" CssClass="button" runat="server" Text="Update" />
&nbsp;
            <asp:Button ID="btn_delete" CssClass="button" runat="server" Text="Delete court" />

        &nbsp;<asp:Button ID="btn_main_menu" CssClass="button" runat="server" Text="Return to Main menu" />

        </div>
    </form>
</body>
</html>
