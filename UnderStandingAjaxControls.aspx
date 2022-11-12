<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UnderStandingAjaxControls.aspx.cs" Inherits="GPI.RI.Admin.UnderStandingAjaxControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


        <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
        
            <script type="text/javascript">
            /*<![CDATA[*/
            
                /*]]>*/
            </script>
        </telerik:RadScriptBlock>
 <div class="container-fluid">
                <div class="row" style="background-color:#367CCF;color:white" >
                <div class="col-lg-1" >test1</div>
                <div class="col-lg-1" >test2</div>
                <div class="col-lg-1" >test3</div>
                <div class="col-lg-1" >test4</div>
                <div class="col-lg-1" >test5</div>
                <div class="col-lg-1" >test6</div>
                <div class="col-lg-1" >test7</div>
                <div class="col-lg-1" >test8</div>
                <div class="col-lg-1" >test9</div>
                <div class="col-lg-1" >test10</div>
                <div class="col-lg-1" >test11</div>
                <div class="col-lg-1" >test12</div>
            </div>
            <br />

 
  <div class="row justify-content-md-center" style="background-color:#FBC118;color:white">
    <div class="col col-lg-2">
      1 of 3
    </div>
    <div class="col-md-auto">
      EWS is a comprehensive service that your applications can use to access almost all the information stored in an Exchange
    </div>
    <div class="col col-lg-2">
      3 of 3
    </div>
  </div>
        <br />
  <div class="row" style="background-color:#7FBA00;color:white">
    <div class="col">
      1 of 3
    </div>
    <div class="col-md-auto">
      Variable width content
    </div>
    <div class="col col-lg-2">
      3 of 3
    </div>
  </div>
</div>

    <br />

    <asp:Button ID="Button1" runat="server" Text="Button1" OnClick="UpdateTime" />
    <asp:Button ID="Button2" runat="server" Text="Button2" OnClick="UpdateTime2" />
    <asp:Button ID="Button3" runat="server" Text="Button3" OnClick="UpdateTime" />

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        
        <asp:Label ID="Label1" runat="server"></asp:Label><br />
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        
  
         <asp:Label ID="Label2" runat="server"></asp:Label><br />
 
    </ContentTemplate>
<%--          <Triggers>
        <asp:AsyncPostBackTrigger ControlID="Button1" EventName="click" />
    </Triggers>--%>
</asp:UpdatePanel>
        <asp:Label ID="Label3" runat="server"></asp:Label>
    </ContentTemplate>
<%--          <Triggers>
        <asp:AsyncPostBackTrigger ControlID="Button1" EventName="click" />
    </Triggers>--%>
</asp:UpdatePanel>


 





























</asp:Content>
