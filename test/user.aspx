<%@ Page Title="" Language="C#" MasterPageFile="~/master.Master" AutoEventWireup="true" CodeBehind="user.aspx.cs" Inherits="test.user" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    



              <asp:ScriptManager ID="S1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="u" runat="server">
        <ContentTemplate>

    <asp:PlaceHolder runat="server" ID="ph" />
            </ContentTemplate>
        </asp:UpdatePanel>
    <br />
    <br />
        <asp:Label ID="tweets" runat="server"></asp:Label>
</asp:Content>
