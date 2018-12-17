<%@ Page Title="" Language="C#" MasterPageFile="~/master.Master" AutoEventWireup="true" MaintainScrollPositionOnPostBack="true" CodeBehind="home.aspx.cs" Inherits="test.home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <span style="font-family: broadway; font-size: 20px;">YOUR TWEET</span>
            <asp:label ID="Textbox1" text="WHATS ON YOUR MIND???" style=" Height:30px; Width:400px; background-color:rgba(255, 216, 0,0.6); border:Dotted; color:black;" runat="server"  ></asp:label>
    <br />
        <asp:textbox ID="Textbox5" Text="" style=" font-family: 'Times New Roman';font-size: 25px;Height:69px; Width:900px; background-color:rgba(255, 216, 0,0.6); border:dotted; color:black;" runat="server" BorderStyle="Solid"></asp:textbox>
    <br />
    <br />
   
    <asp:Button ID="btnSave"  style="background-color:#FFFFCC; Height:33px; Width:130px;margin-left:772px" runat="server" />
    <asp:FileUpload ID="FileUpload1" runat="server"/>
    <br /> <br /> <br />
    <asp:UpdatePanel ID="u" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
    <asp:PlaceHolder runat="server" ID="ph" />
        </ContentTemplate>
     </asp:UpdatePanel>
        <asp:Label ID="tweets" runat="server"></asp:Label>
    </asp:Content>
