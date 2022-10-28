<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Admin_page.aspx.vb" Inherits="Final_Project.Admin_page" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="shortcut icon" href="images/favicon.png" type="image/jpg" />
    <title>Admin Page</title>
    <style type="text/css">
        @import url('https://fonts.googleapis.com/css2?family=Bebas+Neue&family=Koulen&family=Lato&family=Nunito&family=Playfair+Display:ital@1&family=Prata&family=Raleway:ital,wght@1,100&family=Roboto&family=Roboto+Condensed&family=Teko&display=swap');

        .auto-style1 {
            text-align: center;
        }

        body  {
             background-image: url('../images/bg1.jpg');
             background-position: top;
             background-repeat: no-repeat;
             background-size: cover;
        }

        .button{
            font-family: Roboto, sans-serif;
            font-weight:normal;
            font-size: 14px;
            color: #fff;
            background-color: #303030;
            padding: 10px 30px;
            border: solid #ffffff 2px;
            box-shadow: rgb(0, 0, 0) 0px 0px 0px 0px;
            border-radius: 50px;
            transition : 1000ms;
            transform: translateY(0);
            display: flex;
            flex-direction: row;
            align-items: center;
            cursor: pointer;
            margin-top: 30px;
            margin-left: 140px;
        }

        .button:hover{
            transition : 1000ms;
            padding: 10px 48px;
            transform : translateY(-0px);
            background-color: #fff;
            color: #000000;
            border: solid 2px #383838;
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
            <h2>Admin Sector</h2>
        </div>

        <div class="auto-style1">
            
            <asp:Button ID="btn_checkUsers" runat="server" Text="Check the user list  " class="button"/>
&nbsp;
            <asp:Button ID="btn_booking" runat="server" Text="Check the booking list" class="button" />
&nbsp;         
            <asp:Button ID="btn_schools" runat="server" Text="Check the school list " class="button"/>
&nbsp;
            <asp:Button ID="btn_courts" runat="server" Text="Check the court list " class="button"/>
        </div>

    </form>
</body>
</html>