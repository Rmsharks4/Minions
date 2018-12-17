<%@ Page Title="" Language="C#" MasterPageFile="~/master.Master" AutoEventWireup="true" CodeBehind="retweet.aspx.cs" Inherits="test.retweet" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <span: style="font-family: broadway; font-size: 20px;" Text="Your Tweet" />
            <asp:textbox ID="ret"  style=" font-family: 'Times New Roman';font-size: 25px;Height:69px; Width:900px; background-color:rgba(255, 216, 0,0.6); border:dotted; color:black;" runat="server" BorderStyle="Solid"></asp:textbox>

    <br />
        <asp:Button ID="btnSave"  style="background-color:#FFFFCC; Height:33px; Width:130px;margin-left:772px" runat="server" />

    <br /> <br /> <br />
     <asp:PlaceHolder runat="server" ID="ph" />
        <asp:Label ID="tweets" style="width: 500px; height: 200px; padding: 10px; border: 2px solid #000000; font-family: Times New Roman; font-size: 25px; background-color: #ffffff" runat="server"></asp:Label>

</asp:Content>
