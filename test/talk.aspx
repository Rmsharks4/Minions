<%@ Page Title="" Language="C#" MasterPageFile="~/master.Master" AutoEventWireup="true" CodeBehind="talk.aspx.cs" Inherits="test.talk" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:ScriptManager ID="S1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
      <script src="Scripts/jquery-1.6.4.min.js"></script>
    <script src="Scripts/jquery.signalR-2.2.1.min.js"></script>
    <script src="/signalR/hubs"></script>
      <script type="text/javascript">

          $(function () {

              // Proxy created on the fly
              var job = $.connection.myHub;

              // Declare a function on the job hub so the server can invoke it
              job.client.displayStatus = function () {
                  getData();
              };

              // Start the connection
              $.connection.hub.start();
              getData();
          });

          function getData() {
              $.ajax({
                  url: 'talk.aspx/GetData',
                  contentType: "application/json; charset=utf-8",
                  dataType: "json",
                  type: "POST",
                  success: function (data) {
                      debugger;
                  }
              });
          }

    </script>
    <asp:LinkButton id="heading" style="color: #03062e; font-size: 50px; font-family: broadway; background-color: rgba(128, 128, 128,0.7); width: 564px; text-align:center" runat="server"></asp:LinkButton>
  
      <asp:TextBox ID="t" style="font-size: 20px; border: solid #03062e;" runat="server"></asp:TextBox>
    <br />
    <div style="text-align: right">
    <asp:Button ID="Post" runat="server" style="font-size: 23px;" Text="Post" OnClick="CreateMsgs" />
        </div>
      <asp:Label ID="msgtxt" style="color: red;" runat="server"></asp:Label>
<asp:UpdatePanel id="keep" runat="server">
           <ContentTemplate>
                    <asp:PlaceHolder runat="server" ID="news">
                    </asp:PlaceHolder>
                    <asp:PlaceHolder runat="server" ID="place">
                    </asp:PlaceHolder>
           </ContentTemplate>
    <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Post" EventName="Click" runat="server"/>
        </Triggers>
</asp:UpdatePanel>

      <asp:PlaceHolder ID="p" runat="server" ></asp:PlaceHolder>
</asp:Content>
