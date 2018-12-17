<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="test.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>login page</title>
</head>
 
<body>

<style type="text/css">

    body {
        background: url(images/check.png) no-repeat center fixed;
        background-size: cover;
    }
    
   form {
  text-align: center;
}

    a.button {
    -webkit-appearance: button;
    -moz-appearance: button;
    appearance: button;

    }
            </style>
    
    <form id="form1" runat="server">  
    <div>  
        <div id="welcome">        
            <p style="border: thick solid #0026ff; color:#FFFFFF; font-size: 150px; font-family: Broadway; background-color: rgba(128, 128, 128,0.7);"> MINIONS</p>
    <br />
    <br />
        <asp:linkbutton ID="LinkButton1" style="border: thick solid #0026ff; color: #ffffff; font-family: Broadway;  font-size: 50px; background-color: rgba(128, 128, 128,0.7);" href="loginpage.aspx" Text="LOGIN" runat="server"></asp:linkbutton>
    <br />
    <br />
    <br />
    <asp:linkbutton ID="LinkButton2" style="border: thick solid #0026ff; font-family: Broadway;  font-size: 50px; color: #ffffff; background-color: rgba(128, 128, 128,0.7);" href="signuppage.aspx" Text="SIGNUP" runat="server"></asp:linkbutton>
    </div>
    
    </div>
    </form>
</body>
</html>

