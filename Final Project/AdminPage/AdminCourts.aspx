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
             width: 100px;
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
            <br />
            <asp:Button ID="btn_add" CssClass="button" runat="server" Text="Add Court" />
&nbsp;
            <asp:Button ID="btn_change" CssClass="button" runat="server" Text="Update" />
&nbsp;
            <asp:Button ID="btn_delete" CssClass="button" runat="server" Text="Delete court" />

        </div>
    </form>
</body>
</html>
