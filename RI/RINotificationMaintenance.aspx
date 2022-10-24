<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RINotificationMaintenance.aspx.cs" Inherits="GPI.RI.Admin.MOC.RINotificationMaintenance" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>





<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
   
        <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
        <script type="text/javascript">
            /*<![CDATA[*/


            function ClearItems() {
<%--                  debugger
                var dropdownlist = $find("<%= DropDownLineSystemType.ClientID %>");
                var selectedItem = dropdownlist.get_selectedItem();
                if (selectedItem) {
                            selectedItem.unselect();
                        dropdownlist.ClearItems();--%>
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

        <div class="col-md-4">
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

        <div class="col-md-4">
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

        <div class="col-md-4">
            <h4>Line/System</h4>
                <telerik:RadDropDownList RenderMode="Lightweight" runat="server" 
                    ID="DropDownLineSystemType" 
                    Skin="Default" TabIndex="1" Width="200" DropDownWidth="200"
                    OnClientItemsRequesting="OnClientItemsDropDownLineRequesting"
                DefaultMessage="Select Line/System...">
                     <WebServiceSettings Method="LoadLine" Path="../ASMX/MaintNotifications.asmx" />
                </telerik:RadDropDownList>
            </div>

</div>
    <br />
     <div class="row">

         <div class="col-md-2">
            <telerik:RadButton RenderMode="Lightweight" ID="btnGetData" Skin="Silk" 
            Text="Select Notification" OnClick="btnGetData_Click" runat="server" />
        </div>            
    </div>
    <br />
        <div class="row">
        <div class="col-md-4">

         <asp:PlaceHolder runat="server" ID="OrderTable" Visible="false">
                <table class="order-summary">
                    <tbody>
                         <tr>
                            <th>Mill:
                            </th>
                            <td>
                                <asp:Label ID="labelMill" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <th>Business Unit:
                            </th>
                            <td>
                                <asp:Label ID="labelBusinessType" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <th>Area:
                            </th>
                            <td>
                                <asp:Label ID="labelBusinessArea" runat="server" />
                            </td>
                        </tr>
                        <tr class="price-row">
                            <th>Line/System:
                            </th>
                            <td>
                                <asp:Label ID="labelBusinessLine" runat="server" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </asp:PlaceHolder>

            </div>
         </div>



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



