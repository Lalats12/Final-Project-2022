<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="About_Us.aspx.vb" Inherits="Final_Project.About_Us" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link rel="shortcut icon" href="images/favicon.png" type="image/jpg" />
<meta name="viewport" content="width=device-width, initial-scale=1" />
<title>About Us</title>
    <style>
        
        input[type=text], select, textarea {
        width: 100%;
        padding: 12px;
        border: 1px solid #ccc;
        border-radius: 4px;
        box-sizing: border-box;
        margin-top: 6px;
        margin-bottom: 16px;
        resize: vertical;
    }

    input[type=submit] {
        background-color: #000;
        color: white;
        padding: 12px 20px;
        border: none;
        border-radius: 4px;
        cursor: pointer;
    }

        input[type=submit]:hover {
            background-color: #555;
        }

    .form_container {
        border-radius: 5px;
        background-color: #f2f2f2;
        padding: 20px;
    }

    <!---

    .icon {
        display: block;
        margin-left: auto;
        margin-right: auto;
        height: 65px;
        width: 20%;
        border-radius: 100%;
    }

    .icon_column {
        float: left;
        width: 25%;
        margin-bottom: 16px;
        padding: 0 8px;
    }

    --->

    body {
        font-family: Arial, Helvetica, sans-serif;
        margin: 0;
    }

    html {
        box-sizing: border-box;
    }

    *, *:before, *:after {
        box-sizing: inherit;
    }

    .column {
        float: left;
        width: 25%;
        margin-bottom: 16px;
        padding: 0 8px;
    }

    .card {
        box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2);
        margin: 8px;
    }

    .about-section {
        padding: 50px;
        text-align: center;
        background-color: #5b5b5e;
        color: white;
    }

    .container {
        padding: 0 16px;
    }

        .container::after, .row::after {
            content: "";
            clear: both;
            display: table;
        }

    .title {
        color: grey;
    }

    .button {
        border: none;
        outline: 0;
        display: inline-block;
        padding: 8px;
        color: white;
        background-color: #000;
        text-align: center;
        cursor: pointer;
        width: 100%;
    }

        .button:hover {
            background-color: #555;
        }

    @media screen and (max-width: 650px) {
        .column {
            width: 100%;
            display: block;
        }
    }
    </style>   
</head>
<body>
    <div class="about-section">
  <h1>About Us Page</h1>
  <p>A simple introduction to our team.</p>
  <p>Subject Code: DPT3054N Project</p>
  <p>by Dr.Justtina</p>
</div>

<h2 style="text-align:center">Our Team</h2>

<div class="row">
  <div class="column">
    <div class="card">
      <img src="images/ms.jpg" alt="Neoh Kai Xuan" style="width:100%" />
      <div class="container">
        <h2>Neoh Kai Xuan</h2>
        <p class="title">Leader of the group, report compiler</p>
        <p>Get Lucky</p>
        <p>0204394@student.kdupg.edu.my</p>
        <p><button class="button">Contact</button></p>
      </div>
    </div>
  </div>

  <div class="column">
    <div class="card">
      <img src="images/ms.jpg" alt="Tan Wei Lun" style="width:100%" />
      <div class="container">
        <h2>Tan Wei Lun</h2>
        <p class="title">Documentation</p>
        <p>Be Happy</p>
        <p>0205601@student.kdupg.edu.my</p>
        <p><button class="button">Contact</button></p>
      </div>
    </div>
  </div>
  
  <div class="column">
    <div class="card">
      <img src="images/ms.jpg" alt="John Paul Yeap" style="width:100%">
      <div class="container">
        <h2>John Paul Yeap</h2>
        <p class="title">Coding Team</p>
        <p>Just Some Student</p>
        <p>0205573@student.kdupg.edu.my</p>
        <p><button class="button">Contact</button></p>
      </div>
    </div>
  </div>

  <div class="column">
   <div class="card">
    <img src="images/ms.jpg" alt="Bugk" style="width:100%">
    <div class="container">
      <h2>Bugk How Ning</h2>
      <p class="title">Coding Team</p>
      <p>Does not like coding</p>
      <p>0205415@student.kdupg.edu.my</p>
      <p><button class="button">Contact</button></p>
    </div>
   </div>
  </div>
</div>

<h3 style="text-align:center">Contact Us</h3>


 <!---
  <div class="icon_column">
    <img src="images/facebook.png" alt="fb" class="icon">
  </div>
  <div class="icon_column">
    <img src="images/instagram.png" alt="ig" class="icon">  
  </div>
  <div class="icon_column">
    <img src="images/twitter.png" alt="tt" class="icon"> 
  </div>
  <div class="icon_column">
    <img src="images/gmail.png" alt="gm" class="icon"> 
  </div>
-->
  <div class="form_container">
  <form  runat="server">
      <label for="fname">First Name</label>
      <asp:TextBox ID="fname" runat="server"></asp:TextBox>
      <label for="lname">Last Name</label>
      <asp:TextBox ID="lname" runat="server"></asp:TextBox>
      <label for="subject">Subject</label>
      <asp:TextBox ID="subject" runat="server" Height="200px" TextMode="MultiLine"></asp:TextBox>
      <asp:Button ID="btn_submit" runat="server" Text="Submit" />
  </form>
</div>
</body>
</html>
