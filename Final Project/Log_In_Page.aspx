<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Log_In_Page.aspx.vb" Inherits="Final_Project.Log_In_Page" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>badminton Booking Page | Login</title>
    <style type="text/css">        
        body  {
             background-image: url('../images/bg1.jpg');
             background-position: top;
             background-repeat: no-repeat;
             background-size: cover;
        }

        .auto-style1 {
            align-content: center;
        }

        .auto-style2 {
            text-align: center;
        }

        .ogtitle {
              font-family: Roboto, sans-serif;
              color: white;
              font-size: 30px;
              margin-top:  100px;
              margin-left: 150px;
              margin-bottom: 100px;
        }

        .contentBox {
            display: block;
            margin-left: auto;
            margin-right: auto;
            margin-bottom: 300px;
            background-color: white;
            width: 350px;
            height: 450px;
            padding: 50px;
            border-radius: 5px;

        }

        h1 {
            font-family: futura;
            font-weight: ;
        }

        .textfield {
            border-radius: 2px;
            border: 2px solid #777;
            box-sizing: border-box;
            font-size: 1.25em;
            width: 100%;
            padding: 10px;
            margin-bottom: 1px;
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

@media (min-width: 768px) {
        .button {
              padding: 0.25em 0.75em;
         }
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
                    <h2>User Login</h2>
                </div>
        </div>

        <div class="auto-style1" >          
            <div class="contentBox" style="display:grid; place-items:center">
                <h1> LOGIN</h1>
                <asp:TextBox ID="txt_username" runat="server" MaxLength="48" placeholder=" Username" CssClass=" textfield"></asp:TextBox>
                <br />
                <asp:TextBox ID="txt_password" runat="server" TextMode="Password" placeholder=" Password" CssClass=" textfield"></asp:TextBox>
                <br />
                <asp:HyperLink ID="hyp_forgot" runat="server" NavigateUrl="~/ForgotPass.aspx">Forgot your password?</asp:HyperLink>
                <br />
                <asp:Button ID="btn_login" runat="server" Text="Login" class="button"/>
                <asp:Button ID="btn_register" runat="server" Text="Register" class="button"/>
            &nbsp;<br />
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/About_Us.aspx">About Us</asp:HyperLink>
            </div>
        </div>
    </form>
</body>
</html>
