<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ForgotPass.aspx.vb" Inherits="Final_Project.ForgotPass" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>badminton Booking | Password Reset</title>
    <style type="text/css">    <style type="text/css">
        .auto-style1 {
            text-align: center;
        }

        .auto-style2 {
            text-align: center;
            margin-top: 50px;
        }

        .ogtitle {
              font-family: Roboto, sans-serif;
              color: white;
              font-size: 30px;
              margin-top:  100px;
              margin-left: 150px;
              margin-bottom: 100px;
        }

        h1 {
            font-family: futura;
            font-weight: normal;
        }

        .contentBox {
            display: block;
            margin-left: auto;
            margin-right: auto;
            margin-bottom: 300px;
            background-color: white;
            width: 350px;
            height: 300px;
            padding: 50px;
            border-radius: 5px;

        }

        .textfield {
            border-radius: 2px;
            border: 2px solid #777;
            box-sizing: border-box;
            font-size: 1.25em;
            width: 100%;
            padding: 10px;
            margin-bottom: 10px;
        }

        body  {
             background-image: url('../images/bg1.jpg');
             background-position: top;
             background-repeat: no-repeat;
             background-size: cover;
        }

        .button {
              font-family: "Open Sans", sans-serif;
              font-size: 16px;
              letter-spacing: 2px;
              text-decoration: none;
              text-transform: uppercase;
              color: #000;
              cursor: pointer;
              border: 3px solid;
              padding: 0.25em 0.5em;
              box-shadow: 1px 1px 0px 0px, 2px 2px 0px 0px, 3px 3px 0px 0px, 4px 4px 0px 0px, 5px 5px 0px 0px;
              position: relative;
              user-select: none;
              -webkit-user-select: none;
              touch-action: manipulation;
        }

        .button:active {
              box-shadow: 0px 0px 0px 0px;
              top: 5px;
              left: 5px;
        }

        .lineUp {
            animation: 4s anim-lineUp ease-out;
        }
@keyframes anim-lineUp {
        0% {
            opacity: 0;
            transform: translateY(20%);
        }
        20% {
          opacity: 0;
        }
        50% {
            opacity: 1;
            transform: translateY(0%);
        }
        100% {
            opacity: 1;
            transform: translateY(0%);
        }
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="ogtitle">
            <h1>Badminton Booking |</h1>
                <div class="lineUp">
                    <h2>Password Reset</h2>
                </div>
        </div>
        <div class="contentBox">
            <div class="auto-style1">
                <h1>Reset</h1>
                <p>Make sure no-one sees it</p>
            </div>

        <asp:TextBox ID="txt_user" runat="server" placeholder="Username" CssClass=" textfield"></asp:TextBox>
        <asp:TextBox ID="txt_email" runat="server" placeholder=" E-mail" CssClass=" textfield"></asp:TextBox>
        
            <div class="auto-style1">
                <asp:HyperLink ID="hyp_user" runat="server" NavigateUrl="~/ForgotUser.aspx">Forgot your username?</asp:HyperLink>  
            </div>
        
            <div class ="auto-style2">
                <asp:Button ID="btn_reset" runat="server" Text="Check" class ="button"/>
&nbsp;
                <asp:Button ID="btn_main_menu" runat="server" Text="Back" class ="button"/>
            </div>
        </div>
    </form>
</body>
</html>
