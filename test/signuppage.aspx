<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="signuppage.aspx.cs" Inherits="test.signuppage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>

<body>
<style type="text/css">

     body {
        background: url(images/c.jpg) no-repeat fixed;
        background-size: 100% 100%;
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
    </script>
    
    <form id="form1" runat="server">
     
    <div>

                <span style="color: #03062e; font-size: 50px; ">Welcome New Minion!</span> 
        <br />
        <br />
        <br />
        <br />
        <span style="color: #03062e; font-family: BROADway; font-size: 30px; ">USERNAME:</span> 
    <asp:textbox ID="Textbox1" style="font-size: 30px;" runat="server" ToolTip="Username must be unique" Height="26px" Width="155px"></asp:textbox> 
     <br />
        
        <asp:Label ID="Error1" style=" font-family: Broadway;  font-size: 30px;" runat="server"></asp:Label>
        <br />

   
    <span style="color: #03062e; font-family: BROADway;font-size: 30px;">PASSWORD:</span>
   <asp:TextBox ID="Textbox7" style="font-size: 30px;" TextMode="password" runat="server" ToolTip="Password must include UpperCase characters and numerical digits." Height="23px" Width="207px"></asp:TextBox>
    <br />
    
        <asp:Label ID="Error2" style=" font-family: Broadway;  font-size: 30px;" runat="server"></asp:Label>
   <br />

             <span style="color: #03062e; font-family: BROADway; font-size: 30px; ">NAME:</span> 
    <asp:textbox ID="Textbox2" style="font-size: 30px;" runat="server" ToolTip="FirstName LastName" Height="30px" Width="305px"></asp:textbox>
     <br />
   
        <asp:Label ID="Error3" style=" font-family: Broadway;  font-size: 30px;" runat="server"></asp:Label>
   <br />

         <span style="color: #03062e; font-family: BROADway; font-size: 30px;">BIRTHDATE:</span> 
    <asp:textbox ID="Textbox6" style="font-size: 30px;" TextMode="Date" runat="server" ToolTip="Month/Day/Year" Height="30px" Width="175px"></asp:textbox>
     <br />

        <asp:Label ID="Error4" style=" font-family: Broadway;  font-size: 30px;" runat="server"></asp:Label>
   <br />

      <span style="color: #03062e; font-family: BROADway; font-size: 30px; ">EMAIL:</span> 
    <asp:textbox ID="Textbox3" style="font-size: 30px;" runat="server" ToolTip="example: abc@email.com" Height="29px" Width="300px"></asp:textbox>
     <br />
    
        <asp:Label ID="Error5" style=" font-family: Broadway;  font-size: 30px;" runat="server"></asp:Label>
   <br />

     <span style="color: #03062e; font-family: BROADway; font-size: 30px;">COUNTRY:</span> 
    <asp:textbox ID="Textbox4" style="font-size: 30px;" runat="server" Height="30px" Width="204px"></asp:textbox>
     <br />

        <asp:Label ID="Error6" style=" font-family: Broadway;  font-size: 30px;" runat="server"></asp:Label>
   <br />
    
     <span style="color: #03062e; font-family: BROADway; font-size: 30px;">CELL NUMBER:</span> 
    <asp:textbox ID="Textbox5" style="font-size: 30px;" runat="server" ToolTip="XXX-XXX-XXX" Height="27px" Width="201px"></asp:textbox>
    <br /><br />
    
   <asp:button ID="LinkButton1" style=" font-family: Broadway;  font-size: 70px; color: #03062e;" Text="SIGNUP" onclick="EnterUserButton" runat="server" ></asp:button>
     <br /><br />

       <div id="lg"></div>
    </div>
    </form>
</body>
</html>