<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RINotificationMaintenanceBackup.aspx.cs" Inherits="GPI.RI.Admin.RI.RINotificationMaintenanceBackup" %>
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


            function ClearItems() {
                 debugger
                var dropdownlist = $find("<%= DropDownLineSystemType.ClientID %>");
                //alert(dropdownlist)
                var selectedItem = dropdownlist.get_selectedItem();
                //alert(selectedItem)
                //if (selectedItem) {
                //            selectedItem.unselect();
                //        dropdownlist.ClearItems();
            }

            //Load the Area 
            function OnClientItemsDropDownAreaRequesting(sender, args) {
                debugger
                //alert("i am here at area")
            var valueFind = $find('<%=DropDownBusinessUnit.ClientID%>').get_selectedItem().get_value();
            
            var valueFindSites = $find('<%=DropDownSites.ClientID%>').get_selectedItem().get_value();
            //alert(valueFind);
            //alert(valueFindSites);

            var context = args.get_context();
                context["businessunit"] = valueFind;
                context["siteid"] = valueFindSites;

            }


             //Load the Line 
            function OnClientItemsDropDownLineRequesting(sender, args) {
                debugger
                //alert("DropDownLineRequesting")
            var valueFindSites = $find('<%=DropDownSites.ClientID%>').get_selectedItem().get_value();
            var valueFindBusiness = $find('<%=DropDownBusinessUnit.ClientID%>').get_selectedItem().get_value();
            var valueFindArea = $find('<%=DropDownArea.ClientID%>').get_selectedItem().get_value();
            
                //alert(valueFindSites);
                //alert(valueFindBusiness);
                //alert(valueFindArea);

                var context = args.get_context();
                context["siteid"] = valueFindSites;
                context["businessunit"] = valueFindBusiness;
                context["area"] = valueFindArea;

            }


            function OnClientItemSelected(sender, eventArgs) {

                var item = eventArgs.get_item();
                //alert("You selected " + item.get_text() + " with value " + item.get_value());


                var dropdownlist = $find("<%= DropDownArea.ClientID %>");
                var selectedItem = dropdownlist.get_selectedItem();
                if (selectedItem) {
                     
                    // following two lines clear the entries
                    dropdownlist.get_textElement().innerHTML = "";
                    dropdownlist.get_items().clear();
                    selectedItem.unselect();
                }

                var dropdownlist = $find("<%= DropDownLineSystemType.ClientID %>");
                var selectedItem = dropdownlist.get_selectedItem();
                if (selectedItem) {
                    selectedItem.unselect();
                }

            }

            
                /*]]>*/
            </script>
        </telerik:RadScriptBlock>



    <h3>Reliability Incident Notification Maintenance</h3>

 
    <h4>Assign Notification by Business/Area/Line System</h4>

       <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
        </telerik:RadAjaxLoadingPanel>

<telerik:RadAjaxPanel ID="RadAjaxPanel" runat="server">

    <h5>Facility:
    <telerik:RadComboBox RenderMode="Lightweight" 
                    runat="server" ID="DropDownSites"
                    OnSelectedIndexChanged="DropDownSites_SelectedIndexChanged"
                    AutoPostBack="true"
                    Skin="Silk"
                    TabIndex="1" Width="200" 
                    DropDownWidth="200"> 
         </telerik:RadComboBox></h5>
   



     <div class="row">

        <div class="col-md-3">
            <h4>Business Unit</h4>
                <telerik:RadComboBox RenderMode="Lightweight" 
                    runat="server" ID="DropDownBusinessUnit"
                    OnSelectedIndexChanged="DropDownBusinessUnit_SelectedIndexChanged"
                    Skin="Default"
                    TabIndex="1" Width="200" 
                    DropDownWidth="200" 
                    AutoPostBack="true"
                    DefaultMessage="Select Business Unit...">
                </telerik:RadComboBox>
            </div>

        <div class="col-md-3">
            <h4>Area</h4>
                <telerik:RadDropDownList RenderMode="Lightweight" 
                     runat="server" ID="DropDownArea"
                 Skin="Default" TabIndex="1" Width="200" 
                    DropDownWidth="200" 
                    OnClientDropDownOpening="ClearItems"
                    OnClientItemsRequesting="OnClientItemsDropDownAreaRequesting"
                DefaultMessage="Select Area..."> 
                <WebServiceSettings Method="LoadArea" Path="../ASMX/MaintNotifications.asmx" />
            </telerik:RadDropDownList>
            </div>

        <div class="col-md-3">
            <h4>Line/System</h4>
                <telerik:RadDropDownList RenderMode="Lightweight" runat="server" 
                    ID="DropDownLineSystemType" 
                    Skin="Default" TabIndex="1" Width="200" DropDownWidth="200"
                    OnClientItemsRequesting="OnClientItemsDropDownLineRequesting"
                DefaultMessage="Select Line/System...">
                     <WebServiceSettings Method="LoadLine" Path="../ASMX/MaintNotifications.asmx" />
                </telerik:RadDropDownList>
            </div>

         <div class="col-md-3">
            <h4>To/Copy</h4>
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

       <div class="col-md-2">
           </div>
       
    <%--Employees--%>
     
        <div class="col-md-10" >
   <div class="wrapper">
    <div class="NotificationMaintenance size-narrow" id="NotificationMaintenanceContainer" runat="server">
        <div class="wrapper">
            <telerik:RadListBox RenderMode="Lightweight" runat="server" 
                ID="RadListBoxSource" Height="400px" Width="430px"
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
            <telerik:RadListBox RenderMode="Lightweight" runat="server" 
                ID="RadListBoxDestination" Height="400px" Width="330px"
                SelectionMode="Multiple"
                ButtonSettings-AreaWidth="105px" >
                <HeaderTemplate>
                    <h4>Assigned Notification</h4>
                </HeaderTemplate>

                <FooterTemplate></FooterTemplate>
            </telerik:RadListBox>
        </div>
    </div>

                </div>
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
                <asp:Label Font-Bold="true" ForeColor="white" ID="LabelMissingArea" runat="server" Text="No Area has been selected."></asp:Label>
            </span>
               <span id="LoadingRecords" runat="server" visible="false" style="background-color:white; display:block;"  >
                   <asp:Image ID="ImageLoading" runat="server"  src="../images/loading1.gif"/>
                   </span>
            </p>



<%--     <asp:UpdateProgress ID="updProgress"
        AssociatedUpdatePanelID="UpdatePanel1"
        runat="server">
            <ProgressTemplate>           
            <img alt="progress" src="../images/loading.gif" width="60" height="50"/>
               Processing...           
            </ProgressTemplate>
        </asp:UpdateProgress>
       
       <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Label ID="lblText" runat="server" Text=""></asp:Label>
                <br />
                <asp:Button ID="btnInvoke" runat="server" Text="Click"
                    onclick="btnInvoke_Click" />
                <br />

            </ContentTemplate>
    </asp:UpdatePanel> --%>


 </telerik:RadAjaxPanel>





<%--                    <telerik:RadDropDownList RenderMode="Lightweight" 
                    runat="server" ID="DropDownBusinessUnit"
                    OnClientItemSelected="OnClientItemSelected"
                    Skin="Default"
                    TabIndex="1" Width="200" 
                    DropDownWidth="200" 
                    AutoPostBack="false"
                    DefaultMessage="Select Business Unit...">
                    <WebServiceSettings Method="LoadBusinessUnit" Path="../ASMX/MaintNotifications.asmx" />
                </telerik:RadDropDownList>--%>


</asp:Content>