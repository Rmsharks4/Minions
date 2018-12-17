<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="welcome.aspx.cs" Inherits="test.welcome" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>login page</title>
</head>
 
<body>

<style type="text/css">

    body {
        background: url(images/hmmm2.jpg) no-repeat fixed;
        background-size: 100% 100%;
    }
                </style>
    
    <form id="form1" style="margin-top: 10px;  text-align: center;" runat="server">  
            <span style=" color:#FFFFFF; font-size: 100px; font-family: Broadway;"> MINIONS</span>
    <br />
        <div style="text-align: center;">
                <asp:linkbutton ID="LinkButton1" style=" color: #ffffff; font-family: Broadway;  font-size: 70px; " href="loginpage.aspx" Text=" LOGIN " runat="server"></asp:linkbutton>
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:linkbutton ID="LinkButton2" style=" font-family: Broadway;  font-size: 70px; color: #ffffff; " href="signuppage.aspx" Text=" SIGNUP " runat="server"></asp:linkbutton>
        </div>
            </form>

</body>
</html>

