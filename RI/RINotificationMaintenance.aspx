<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RINotificationMaintenance.aspx.cs" Inherits="GPI.RI.Admin.MOC.RINotificationMaintenance" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>



<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
 
   
<style type="text/css">
    .NotificationMaintenance {
    text-align: center;
}
 
.wrapper {
    display: inline;
    display: inline-block;
    zoom: 1;
}
 
.RadListBox {
    text-align: left;
}


    </style> 


        <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
        
            <script type="text/javascript">
            /*<![CDATA[*/
            
                /*]]>*/
            </script>
        </telerik:RadScriptBlock>


<telerik:RadAjaxPanel ID="RadAjaxPanel" runat="server">

     <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
  
         <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="ButtonShowAssignments">
                <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="ButtonShowAssignments" ></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="RadListBoxSource" LoadingPanelID="LoadingPanel1"></telerik:AjaxUpdatedControl>
                     <telerik:AjaxUpdatedControl ControlID="RadListBoxDestination" LoadingPanelID="LoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
                </telerik:AjaxSetting>


             <telerik:AjaxSetting AjaxControlID="ButtonSaveAssignments">
                <UpdatedControls>
                 <telerik:AjaxUpdatedControl ControlID="ButtonSaveAssignments" ></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="RadListBoxSource" LoadingPanelID="LoadingPanel1"></telerik:AjaxUpdatedControl>
                     <telerik:AjaxUpdatedControl ControlID="RadListBoxDestination" LoadingPanelID="LoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
                </telerik:AjaxSetting>
         </AjaxSettings>

        </telerik:RadAjaxManager>



    <h3>Reliability Incident Notification Maintenance</h3>

 
    <h4>Assign Notification by Business/Area/Line System</h4>
    <br />


    <asp:Label ID="LabelFacility" runat="server" AssociatedControlID="DropDownSites">Facility:</asp:Label>
    <br />
    <telerik:RadComboBox RenderMode="Lightweight" 
                    runat="server" ID="DropDownSites"
                    OnSelectedIndexChanged="DropDownSites_SelectedIndexChanged"
                    AutoPostBack="true"
                    Skin="Silk"
                    TabIndex="1" Width="200" 
                    DropDownWidth="200"> 
         </telerik:RadComboBox>
   


     <div class="row">

        <div class="col-md-3">
                <br />
                <asp:Label ID="LabelBusinessUnit" runat="server" AssociatedControlID="DropDownBusinessUnit">Business Unit:</asp:Label>
                <br />

                <telerik:RadComboBox RenderMode="Lightweight" 
                    runat="server" ID="DropDownBusinessUnit"
                    OnItemsRequested="DropDownBusinessUnit_ItemsRequested"
                    OnSelectedIndexChanged="DropDownBusinessUnit_SelectedIndexChanged"
                    Skin="Default"
                    TabIndex="1" Width="200" 
                    DropDownWidth="200" 
                    EnableLoadOnDemand="true"
                    AllowCustomText="false" 
                    AutoPostBack="true">
                </telerik:RadComboBox>
            </div>

        <div class="col-md-3">
                <br />
                <asp:Label ID="LabelArea" runat="server" AssociatedControlID="DropDownArea">Area:</asp:Label>
                <br />
                <telerik:RadComboBox RenderMode="Lightweight" 
                     runat="server" ID="DropDownArea"
                    OnItemsRequested="DropDownArea_ItemsRequested"
                    Skin="Default" TabIndex="1" Width="200"
                      AllowCustomText="false" 
                    EnableLoadOnDemand="true"
                    AutoPostBack="true">
                </telerik:RadComboBox>
            </div>

        <div class="col-md-3">
                <br />
                <asp:Label ID="LabelLineSystem" runat="server" AssociatedControlID="DropDownLineSystemType">Line/System:</asp:Label>
                <br />
                <telerik:RadComboBox RenderMode="Lightweight" runat="server" 
                    ID="DropDownLineSystemType" 
                    OnItemsRequested="DropDownLineSystemType_ItemsRequested"
                    Skin="Default" TabIndex="1" 
                    Width="200" DropDownWidth="200"
                       AllowCustomText="false" 
                    EnableLoadOnDemand="true"
                    AutoPostBack="true">
                    
                </telerik:RadComboBox>
            </div>

         <div class="col-md-3">
                <br />
                <asp:Label ID="LabelToCopy" runat="server" AssociatedControlID="DropDownToCopy">To/Copy:</asp:Label>
                <br />
                 <telerik:RadDropDownList RenderMode="Lightweight" runat="server" 
                    ID="DropDownToCopy" 
                    Skin="Default" TabIndex="1" Width="200" DropDownWidth="200">
                     <Items>
                         <telerik:DropDownListItem Text="Copy" Value="C" Selected="true" />
                        <telerik:DropDownListItem Text="To" Value="T" />

                    </Items>
                </telerik:RadDropDownList>
        </div>

</div>
    <br />
     
    <div class="row">

             <div class="col-md-12">
                <div class="NotificationMaintenance"  runat="server">
                <telerik:RadButton ID="ButtonShowAssignments" 
                    OnClick="btnGetData_Click" runat="server" Skin="Black"  
                    Text="Show Assignments">
                </telerik:RadButton>
                </div>
            </div>
      </div>
 

<br />
   
   <div class="row">

       <div class="col-md-2" >
           </div>
    <%--Avaiable Employees--%>
    <div class="NotificationMaintenance size-narrow" id="NotificationMaintenanceContainer" runat="server">
                                         
        <div class="col-md-4" >
                                <%--<div class="wrapper">
                                    <div class="NotificationMaintenance size-narrow" id="NotificationMaintenanceContainer" runat="server">
                                        <div class="wrapper">--%>
            <telerik:RadListBox RenderMode="Lightweight" runat="server" 
                ID="RadListBoxSource" Height="400px" Width="400px"
                  SelectionMode="Multiple" 
                AllowTransfer="true" 
                AllowTransferOnDoubleClick="true"
                TransferToID="RadListBoxDestination"
                  ButtonSettings-VerticalAlign="Middle"
                 ButtonSettings-RenderButtonText="true"
                  ButtonSettings-AreaWidth="125px" >
                 <HeaderTemplate>
                    <h4>Site Resources</h4>
                </HeaderTemplate>
                
            </telerik:RadListBox>
       </div>
        </div>

        <%--Assigned Employees--%>
       <div class="col-md-4" >
            <telerik:RadListBox RenderMode="Lightweight" runat="server" 
                ID="RadListBoxDestination" Height="400px" Width="300px"
                SelectionMode="Multiple"
                 >
                <HeaderTemplate>
                    <h4>Assigned Notification</h4>
                </HeaderTemplate>

                <FooterTemplate></FooterTemplate>
            </telerik:RadListBox>
        </div>

              <div class="col-md-2" >
           </div>
    
  </div>


         
    <br />



            <div class="NotificationMaintenance"  runat="server">
            <telerik:RadButton ID="ButtonSaveAssignments" OnClick="btnSaveNotification_Click" Skin="Black" runat="server"  Text="Save Assignments">
            </telerik:RadButton>
            </div>

    

    

            <p class="text-center">
                <br />
            <span id="alertmessage" runat="server" visible="false" style="background-color:red; display:block;"  >
                <asp:Label Font-Bold="true" ForeColor="white" ID="LabelMissingText" runat="server" Text=""></asp:Label>
            </span>
               <span id="LoadingRecords" runat="server" visible="false" style="background-color:white; display:block;"  >
                   <asp:Image ID="ImageLoading" runat="server" Width="35"  Height="35"  src="../images/loading4.gif"/>
                   </span>
            </p>



        <div class="panelBottom">
            <telerik:RadAjaxLoadingPanel 
                ID="LoadingPanel1" 
                runat="server" 
                BackgroundPosition="Center">
            </telerik:RadAjaxLoadingPanel>
        </div>




 </telerik:RadAjaxPanel>


</asp:Content>



