<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RINotificationMaintenance.aspx.cs" Inherits="GPI.RI.Admin.MOC.RINotificationMaintenance" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>





<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
   
        <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
        <script type="text/javascript">
            /*<![CDATA[*/


            function OnClientItemsRequesting(sender, args) { debugger



            var valueFind = $find('<%=DropDownBusinessUnit.ClientID%>').get_selectedItem().get_value();
            alert(valueFind);
            var valueFindSites = $find('<%=DropDownSites.ClientID%>').get_selectedItem().get_value();

            alert(valueFindSites);


            var context = args.get_context();
                context["sites"] = valueFind;
                context["siteid"] = "AU";
                


      <%--      var valueFind = $find("<%=DropDownBusinessUnit.ClientID%>").get_selectedItem().get_value();
            alert(valueFind);--%>

    }





            /*]]>*/
        </script>
    </telerik:RadScriptBlock>
   
        <h3>Reliability Incident Notification Maintenance</h3>
    <h4>Assign Notification by Business/Area/Line System</h4>
    <h4>Facility:  <telerik:RadLabel RenderMode="Lightweight" runat="server" 
        ID="RadLabelSite" Text="AU"></telerik:RadLabel></h4>
    <telerik:RadComboBox  RenderMode="Lightweight" ID="mysites" Skin="Default" 
        runat="server" TabIndex="1" Width="200" DropDownWidth="200" ></telerik:RadComboBox>
       
    <telerik:RadComboBox RenderMode="Lightweight" 
                    runat="server" ID="DropDownSites"
                    Skin="Default"
                    TabIndex="1" Width="200" 
                    DropDownWidth="200"> 
         </telerik:RadComboBox>


<telerik:RadAjaxPanel ID="RadAjaxPanel" runat="server">


     <div class="row">

        <div class="col-md-4">
            <h4>Business Unit</h4>
                <telerik:RadDropDownList RenderMode="Lightweight" 
                    runat="server" ID="DropDownBusinessUnit"
                    OnSelectedIndexChanged="DropDownBusinessUnit_SelectedIndexChanged"
                    Skin="Default"
                    TabIndex="1" Width="200" 
                    DropDownWidth="200" 
                    DefaultMessage="Select Business Unit...">
                    <WebServiceSettings Method="LoadBusinessUnit" Path="../ASMX/MaintNotifications.asmx" />
                </telerik:RadDropDownList>
            </div>

        <div class="col-md-4">
            <h4>Area</h4>
                <telerik:RadDropDownList RenderMode="Lightweight" 
                 runat="server" ID="DropDownArea"
                 onclientitemsrequesting="OnClientItemsRequesting"
                OnSelectedIndexChanged="DropDownArea_SelectedIndexChanged"
                Skin="Default" TabIndex="1" Width="200" DropDownWidth="200" 
                DefaultMessage="Select Area..."
                AutoPostBack="true"> 
                <WebServiceSettings Method="LoadArea" Path="../ASMX/MaintNotifications.asmx" />
                </telerik:RadDropDownList>
            </div>

        <div class="col-md-4">
            <h4>Line/System</h4>
                <telerik:RadDropDownList RenderMode="Lightweight" runat="server" 
                    ID="DropDownLineSystemType" 
                Skin="Default" TabIndex="1" Width="200" DropDownWidth="200" 
                    DefaultMessage="Select Line/System..."
                AutoPostBack="true" 
                DataTextField="TypeName" 
                DataValueField="ID">
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


</asp:Content>



