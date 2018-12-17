﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="adminpage.aspx.cs" Inherits="test.adminpage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Page</title>
</head>
<body>
    <style>
            body {background:  url(images/bb.jpg) repeat fixed;}
    </style>
    <form id="form1" runat="server">
    <div style="width: 564px; align-content:stretch; align-self:stretch; align-items:stretch; opacity:inherit " >
    <h1 style="color: #03062e; font-size: 50px; font-family: broadway; background-color: rgba(128, 128, 128,0.7); width: 564px; text-align:center" >ADMIN PAGE</h1>
    <asp:Label ID="Message" runat="server" Text=" "></asp:Label>
    <asp:GridView ID="modifyUser" runat="server" AutoGenerateEditButton="True" AutoGenerateDeleteButton="True" EnableViewState="False" OnRowDeleting="User_RowDelete_Click" OnRowEditing="User_RowEdit_Click" OnRowCancelingEdit="User_RowEditCancel_Click" OnRowUpdating="User_RowUpdate_Click" DataKeyNames="userid" HorizontalAlign="Center" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" >
        <FooterStyle BackColor="White" ForeColor="#000066" />
        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
        <RowStyle ForeColor="#000066" />
        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#007DBB" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#00547E" />
    </asp:GridView>
        <br/>
        <br/>
    <asp:GridView ID="modifyTweet" runat="server" AutoGenerateEditButton="True" AutoGenerateDeleteButton="True" EnableViewState="False"  OnRowDeleting="Tweet_RowDelete_Click" OnRowEditing="Tweet_RowEdit_Click" OnRowCancelingEdit="Tweet_RowEditCancel_Click" OnRowUpdating="Tweet_RowUpdate_Click" DataKeyNames="tweetid,userid" HorizontalAlign="Center" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" >
        <FooterStyle BackColor="White" ForeColor="#000066" />
        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
        <RowStyle ForeColor="#000066" />
        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#007DBB" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#00547E" />
    </asp:GridView>
    </div>
    </form>
</body>
</html>