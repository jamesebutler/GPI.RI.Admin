<%@ Page Title="RI Analysis Leaders " Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RINotificationMaintenance.aspx.cs" Inherits="GPI.RI.Admin.MOC.RINotificationMaintenance" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>



<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<%-- <link href="styles.css" rel="stylesheet" type="text/css" />--%>
   
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


.rgCommandCell a img
{
    margin-right: 5px;
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
                       <telerik:AjaxUpdatedControl ControlID="RadGridEmployees" LoadingPanelID="LoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="LabelShowAssignments" ></telerik:AjaxUpdatedControl>
                   
                                     
                    

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



    <h3>Reliability Incident Assign Notification</h3>

 
    <%--<h4>Assign Notification by Business/Area/Line System</h4>--%>
    <br />

        <div class="row">
            <div class="col-md-12">
            <asp:Label ID="LabelFacility" runat="server" AssociatedControlID="DropDownSites">Facility:</asp:Label>
            

           
            <telerik:RadComboBox RenderMode="Lightweight" 
            runat="server" ID="DropDownSites"
            OnSelectedIndexChanged="DropDownSites_SelectedIndexChanged"
            AutoPostBack="true"
            Skin="Silk"
            TabIndex="1" Width="200" 
            DropDownWidth="200"> 
            </telerik:RadComboBox>
            </div>

        </div>
    



     <div class="row">

        <div class="col-md-2">
                <br />
                <asp:Label ID="LabelBusinessUnit" runat="server" AssociatedControlID="DropDownBusinessUnit">Business Unit:</asp:Label>
                <br />

                <telerik:RadComboBox RenderMode="Lightweight" 
                    runat="server" ID="DropDownBusinessUnit"
                    OnItemsRequested="DropDownBusinessUnit_ItemsRequested"
                    OnSelectedIndexChanged="DropDownBusinessUnit_SelectedIndexChanged"
                    Skin="Default"
                    TabIndex="1" Width="175" 
                    DropDownWidth="200" 
                    EnableLoadOnDemand="true"
                    AllowCustomText="false" 
                    AutoPostBack="true">
                </telerik:RadComboBox>
            </div>

        <div class="col-md-2">
                <br />
                <asp:Label ID="LabelArea" runat="server" AssociatedControlID="DropDownArea">Area:</asp:Label>
                <br />
                <telerik:RadComboBox RenderMode="Lightweight" 
                     runat="server" ID="DropDownArea"
                    OnItemsRequested="DropDownArea_ItemsRequested"
                    Skin="Default" TabIndex="1" Width="175"
                      AllowCustomText="false" 
                    EnableLoadOnDemand="true"
                    AutoPostBack="true">
                </telerik:RadComboBox>
            </div>

        <div class="col-md-2">
                <br />
                <asp:Label ID="LabelLineSystem" runat="server" AssociatedControlID="DropDownLineSystemType">Line/System:</asp:Label>
                <br />
                <telerik:RadComboBox RenderMode="Lightweight" runat="server" 
                    ID="DropDownLineSystemType" 
                    OnItemsRequested="DropDownLineSystemType_ItemsRequested"
                    Skin="Default" TabIndex="1" 
                    Width="175" DropDownWidth="200"
                       AllowCustomText="false" 
                    EnableLoadOnDemand="true"
                    AutoPostBack="true">
                    
                </telerik:RadComboBox>
            </div>

<%--         <div class="col-md-2">
                <br />
                <asp:Label ID="LabelToCopy" runat="server" AssociatedControlID="DropDownToCopy">To/Copy:</asp:Label>
                <br />
                 <telerik:RadDropDownList RenderMode="Lightweight" runat="server" 
                    ID="DropDownToCopy" 
                    Skin="Default" TabIndex="1" Width="175" DropDownWidth="200">
                     <Items>
                         <telerik:DropDownListItem Text="Copy" Value="C" Selected="true" />
                        <telerik:DropDownListItem Text="To" Value="T" />

                    </Items>
                </telerik:RadDropDownList>
        </div>--%>



                  <div class="col-md-2">
                <br />
                <asp:Label ID="LabelIncludeAll" runat="server" AssociatedControlID="RadCheckBoxIncludeAll">Show All Assignments</asp:Label>
                <br />
                      <telerik:RadCheckBox ID="RadCheckBoxIncludeAll" runat="server" Text="Yes" Checked="true"></telerik:RadCheckBox>
                </div>
               

         

               <div class="col-md-4" >
                          <br />
                <asp:Label ID="LabelblankforAlignmnet" runat="server" >&nbsp;</asp:Label>
                <br />
                <div class="NotificationMaintenance"  runat="server">
                <telerik:RadButton ID="ButtonShowAssignments" 
                    OnClick="btnGetData_Click" runat="server" Skin="Black"  
                    Text="Show Assignments">
                </telerik:RadButton>
                </div>
                   </div> 

        </div>


 

<br />

            <div class="row"  >
        <div class="col-lg-12" >
        <asp:Label ID="LabelShowAssignments" Text="" runat="server" EnableViewState="False" Font-Bold="True" ForeColor="#FF8080"></asp:Label>
        </div>
        </div>

        <%--EMPLOYEE GRID STARTS HERE--%>
                <div class="row"  >
                <div class="col-lg-12" >
                
                <telerik:RadGrid ID="RadGridEmployees" RenderMode="Lightweight" runat="server"
                OnItemDataBound="RadGridEmployees_ItemDataBound"
                OnNeedDataSource="RadGridEmployees_NeedDataSource"
                Width="99%"
                Visible="true"
                AllowPaging="true"
                ShowGroupPanel="true"

                    AllowMultiRowSelection="True"
                    AllowMultiRowEdit="True"
			
                AllowSorting="true"
                AllowFilteringByColumn="True"
                ExportSettings-FileName="Report"
                ExportSettings-ExportOnlyData="true"
                ExportSettings-IgnorePaging="true"
                ExportSettings-OpenInNewWindow="true"
                ExportSettings-UseItemStyles="true"
                PageSize="25"
                AutoGenerateColumns="false"
			
                HeaderStyle-HorizontalAlign="Left"
                HeaderStyle-Font-Underline="true"
                HeaderStyle-Wrap="false"
                HeaderStyle-Font-Bold="true"
                HeaderStyle-Font-Size="13px"
                HeaderStyle-Font-Names="Arial"
			
                ShowFooter="True"
                FooterStyle-HorizontalAlign="Right"
                FooterStyle-Font-Bold="true"
                FooterStyle-Font-Size="Medium"
			
              

                EnableLinqExpressions="false">

                <ExportSettings>
                <Excel DefaultCellAlignment="Left" Format="Xlsx" WorksheetName="AssignNotification" />
                <Pdf PageWidth="297mm" PageHeight="210mm" />
                </ExportSettings>



                <MasterTableView CommandItemDisplay="Top" Width="100%" >
                <%--TableLayout="Fixed"--%>
                <GroupHeaderItemStyle Height="10px" /> 
                <PagerStyle AlwaysVisible="true" />

                                
                                    <CommandItemTemplate>
                    <div style="padding: 5px 5px;">
                        Commands&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:LinkButton ID="btnEditSelected" runat="server" CommandName="EditSelected" Visible='<%# RadGridEmployees.EditIndexes.Count == 0 %>'><img style="border:0px;vertical-align:middle;" alt="" src="../images/Edit.png"/>Edit selected</asp:LinkButton>&nbsp;&nbsp;
                <asp:LinkButton ID="LinkButton2" runat="server" CommandName="InitInsert" Visible='<%# !RadGridEmployees.MasterTableView.IsItemInserted %>'><img style="border:0px;vertical-align:middle;" alt="" src="../images/AddRecord.png"/>Add new notification</asp:LinkButton>&nbsp;&nbsp;
                <asp:LinkButton ID="LinkButton3" runat="server" CommandName="PerformInsert" Visible='<%# RadGridEmployees.MasterTableView.IsItemInserted %>'><img style="border:0px;vertical-align:middle;" alt="" src="../images/Insert.gif"/> Add this Customer</asp:LinkButton>&nbsp;&nbsp;
                <asp:LinkButton ID="LinkButton1" OnClientClick="javascript:return confirm('Delete Assign Notification?')"
                    runat="server" CommandName="DeleteSelected"><img style="border:0px;vertical-align:middle;" alt="" src="../images/Delete.png"/>Delete assign notification</asp:LinkButton>&nbsp;&nbsp;
                <asp:LinkButton ID="LinkButton4" runat="server" CommandName="RebindGrid"><img style="border:0px;vertical-align:middle;" alt="" src="../images/Refresh.png"/>Refresh customer list</asp:LinkButton>
                <asp:LinkButton ID="btnUpdateEdited" runat="server" CommandName="UpdateEdited" Visible='<%# RadGridEmployees.EditIndexes.Count > 0 %>'><img style="border:0px;vertical-align:middle;" alt="" src="../images/Update.png"/>Update</asp:LinkButton>&nbsp;&nbsp;
                <asp:LinkButton ID="btnCancel" runat="server" CommandName="CancelAll" Visible='<%# RadGridEmployees.EditIndexes.Count > 0 || RadGridEmployees.MasterTableView.IsItemInserted %>'><img style="border:0px;vertical-align:middle;" alt="" src="../images/Cancel.png"/>Cancel editing</asp:LinkButton>&nbsp;&nbsp;
						
                    </div>
                </CommandItemTemplate>
                    
                    <Columns>
                <%--setting Display=false will still let you have access to the data but will not display or export--%>

                <telerik:GridBoundColumn HeaderStyle-Width="13%" AllowSorting="true" AllowFiltering="true"   DataField="username" HeaderText="UserName" ></telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="13%" AllowSorting="true" AllowFiltering="true" DataField="lastname" HeaderText="Last Name" ></telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="13%" AllowSorting="true" AllowFiltering="true" DataField="firstname" HeaderText="First Name" ></telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="13%" AllowSorting="true" AllowFiltering="true" DataField="risuperarea" HeaderText="Business Unit"  DataType="System.String"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="13%" AllowSorting="true" AllowFiltering="true" DataField="subarea" HeaderText="Area" HeaderStyle-Wrap="true"  DataType="System.String"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="13%" AllowSorting="true" AllowFiltering="true" DataField="area" HeaderText="Line/System" HeaderStyle-Wrap="true"  DataType="System.String"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="13%" AllowSorting="true" AllowFiltering="true" DataField="notifytype" HeaderText="To/Copy" HeaderStyle-Wrap="true"  DataType="System.String"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="4%" AllowSorting="false" AllowFiltering="false" DataField="siteid" HeaderText="Site" HeaderStyle-Wrap="true"  DataType="System.String"></telerik:GridBoundColumn>

                </Columns>

                </MasterTableView>


                <ClientSettings 
                    ReorderColumnsOnClient="True" 
                    AllowDragToGroup="false" 
                    AllowColumnsReorder="True">

                <Scrolling AllowScroll="true" UseStaticHeaders="true" ScrollHeight="" />
                <Selecting AllowRowSelect="true"></Selecting>
                <Resizing
                AllowRowResize="False"
                AllowColumnResize="False"
                EnableRealTimeResize="False"
                ResizeGridOnColumnResize="False"></Resizing>

 
                </ClientSettings>

                <GroupingSettings ShowUnGroupButton="true" ></GroupingSettings>

                <FilterMenu RenderMode="Lightweight"></FilterMenu>

                <HeaderContextMenu RenderMode="Lightweight"></HeaderContextMenu>

                <PagerStyle Mode="NextPrevAndNumeric" PageSizeControlType="RadDropDownList"></PagerStyle>




                </telerik:RadGrid> 


                </div>
                </div>

    <br /><br />

        <div class="row"  >
        <div class="col-lg-12" >
        <asp:Label ID="LabelError" runat="server" EnableViewState="False" Font-Bold="True" ForeColor="#FF8080"></asp:Label>
        <asp:Label ID="LabelSuccess" runat="server" EnableViewState="False" Font-Bold="True" ForeColor="#00C000"></asp:Label>
        </div>
        </div>




    <br /><br />

   
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




<%--            <div class="row" style="background-color:#367CCF;color:white" >
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
            </div>--%>



        <div class="panelBottom">
            <telerik:RadAjaxLoadingPanel 
                ID="LoadingPanel1" 
                 Skin="Telerik"
                runat="server" 
                BackgroundPosition="Center">
            </telerik:RadAjaxLoadingPanel>
        </div>


 
 </telerik:RadAjaxPanel>





</asp:Content>



