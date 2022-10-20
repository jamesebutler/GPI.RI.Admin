<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RINotificationMaintenance.aspx.cs" Inherits="GPI.RI.Admin.MOC.RINotificationMaintenance" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>



<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        
   
        <h3>Reliability Incident Notification Maintenance</h3>
    <h4>Assign Notification by Business/Area/Line System</h4>
        
<telerik:RadAjaxPanel ID="RadAjaxPanel" runat="server">

     <div class="row">

        <div class="col-md-4">
            <h4>Business Unit</h4>
                <telerik:RadDropDownList RenderMode="Lightweight" runat="server" ID="DropDownVehicleType" 
                Skin="Silk" TabIndex="1" Width="200" 
                    DropDownWidth="200" 
                    DefaultMessage="Select Business Unit...">
                    <WebServiceSettings Method="GetBusinessUnit" Path="../ASMX/MaintNotifications.asmx" />
                </telerik:RadDropDownList>
            </div>

        <div class="col-md-4">
            <h4>Area</h4>
                <telerik:RadDropDownList RenderMode="Lightweight" runat="server" ID="DropDownAreaType" 
                Skin="Silk" TabIndex="1" Width="200" DropDownWidth="200" DefaultMessage="Select Area..."
                AutoPostBack="true" 
                DataTextField="TypeName" 
                DataValueField="ID">
                </telerik:RadDropDownList>
            </div>

        <div class="col-md-4">
            <h4>Line/System</h4>
                <telerik:RadDropDownList RenderMode="Lightweight" runat="server" ID="DropDownLineSystemType" 
                Skin="Silk" TabIndex="1" Width="200" DropDownWidth="200" DefaultMessage="Select Line/System..."
                AutoPostBack="true" 
                DataTextField="TypeName" 
                DataValueField="ID">
                </telerik:RadDropDownList>
            </div>


</div>

 </telerik:RadAjaxPanel>
</asp:Content>



