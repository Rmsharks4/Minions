<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="loginpage.aspx.cs" Inherits="test.loginpage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
 
<body>
<style type="text/css">
     
    body {
        background: url(images/GmDqj.jpg) no-repeat top;
        background-size: contain;
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
      
    <div id="lg" >
    <br />
    <br />
 <br />   
    </div>
    <div>
    <br />
        <br />
    <br />
    <br />
    <br /><br /><br /><br />
    <br />
    <br />
    <br />
    <br /><br /><br /><br />
    <br />
    <br />
    <br />
    <br /><br /><br /><br />
    <br />
        <span style="color: #03062e; font-size: 25px; ">Welcome back Minion!</span> 
    <br />
        <br />
    <span style="color: #03062e; font-family: BROADway; font-size: 60px; ">Username:</span> 
    <asp:textbox ID="Textbox1" runat="server" style="font-family: 'Times New Roman'; font-size: 25px;" Height="41px" Width="202px"></asp:textbox> 
    <br />
    <br />
        <br />
    <span style="color: #03062e; font-family: BROADway;font-size: 60px; ">Password:</span> 
    <asp:textbox ID="Textbox2" TextMode="Password" style="font-size: 25px;" runat="server" Height="41px" Width="202px"></asp:textbox>
 
    <br />
    <br />
        <br />
    <asp:button ID="Button1" style=" font-family: Broadway;  font-size: 70px; color: #03062e;" Text="LOGIN" onclick="SearchUserButton" runat="server" ></asp:button>
        <br />
        <br />
        <br />
        <asp:Label ID="Error" style=" font-size: 30px;" runat="server"></asp:Label>

    </div>
    </form>
</body>
</html>

