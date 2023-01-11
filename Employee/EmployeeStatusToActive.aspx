<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmployeeStatusToActive.aspx.cs" Inherits="GPI.RI.Admin.Employee.EmployeeStatusToActive" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
 
    <%@ MasterType VirtualPath="~/Site.Master" %>


    <style type="text/css">
        </style>


        <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
        
            <script type="text/javascript">
            /*<![CDATA[*/
            
                /*]]>*/
            </script>
        </telerik:RadScriptBlock>



<telerik:RadAjaxPanel ID="RadAjaxPanel" runat="server" >

        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>


            <telerik:AjaxSetting AjaxControlID="panelEmployeeHasTask">
                <UpdatedControls>
                   <%-- <telerik:AjaxUpdatedControl ControlID="DropDownTaskToEmployee" ></telerik:AjaxUpdatedControl>--%>
                </UpdatedControls>
            </telerik:AjaxSetting>

            

            <telerik:AjaxSetting AjaxControlID="DropDownTaskToEmployee">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ButtonUpdate" ></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="DropDownEmployees">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="panelEmployeeHasTask" ></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="ButtonUpdate" LoadingPanelID="LoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="DropDownTaskToEmployee" LoadingPanelID="LoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="SuccessStatus" ></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>


                        <telerik:AjaxSetting AjaxControlID="ButtonUpdate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="SuccessStatus" ></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="FailureAdded" ></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="ButtonUpdate" LoadingPanelID="LoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="DropDownTaskToEmployee" LoadingPanelID="LoadingPanel1" ></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="DropDownEmployees" LoadingPanelID="LoadingPanel1" ></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="panelEmployeeHasTask" ></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="RadGridEmployees" LoadingPanelID="LoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>



        </AjaxSettings>
        </telerik:RadAjaxManager>


   
    
<%--        <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server" Visible="false">
        <AjaxSettings>

            <telerik:AjaxSetting AjaxControlID="ButtonUpdate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="SuccessStatus" ></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="FailureAdded" ></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="ButtonUpdate" LoadingPanelID="LoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="DropDownTaskToEmployee" LoadingPanelID="LoadingPanel1" ></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="DropDownEmployees" LoadingPanelID="LoadingPanel1" ></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="panelEmployeeHasTask" ></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="RadGridEmployees" LoadingPanelID="LoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>

        </AjaxSettings>
        </telerik:RadAjaxManagerProxy>--%>



  

    <div class="container">   <%--this will make the form narrow--%>
    <%-- <div class="container-fluid">--%>


                <div class="row"> 
                <div class="col-lg-12">
                    <h3><asp:Label ID="LabelSetStatus" runat="server" Text="Set Employee to Active" ></asp:Label></h3>
                </div>
        </div>

            <div class="row row-no-gutters">
        <div class="col-lg-12" >
                <p class="noteAltNoImage">
                <strong>
                Select the Employee to update the status to Active.<br />
                Click "Update to Active" <br />
                Task Tracker Notifications (default) records will be added automatically.
                </strong>
                </p>
        </div>
        </div>



 


        <%--    <p class="noteAltNoImage">
            <strong>Note:</strong> Be sure to
            <a href="https://mdbootstrap.com/docs/standard/layout/grid/">read the Grid page</a>
            first before diving into how to modify and customize your grid columns.
          </p>--%>


<%--                <div class="row row-no-gutters" style="background-color:#367CCF;color:white" >
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

        <div class="row row-no-gutters"> 

                        <div class="col-lg-3">
                            <asp:Label ID="LabelEmployee" runat="server" Font-Bold="true" >InActive Employee:</asp:Label>
                        </div>
                         <div class="col-lg-9"></div>

        </div>


        <div class="row row-no-gutters"> 


                        <div class="col-lg-3">
                            <telerik:RadComboBox RenderMode="Lightweight" 
                            runat="server"
                            IsTextSearchEnabled="True"
                            IsCaseSensitive="false"
                            Filter="Contains" 
                            Font-Size="14px" 
                            MarkFirstMatch="true"
                            ID="DropDownEmployees"
                            Skin="Silk"
                            TabIndex="1" Width="250" 
                            OnSelectedIndexChanged="DropDownEmployees_SelectedIndexChanged"
                            DropDownWidth="170"
                            EnableLoadOnDemand="true"
                            AllowCustomText="false" 
                            AutoPostBack="true">
                            </telerik:RadComboBox>
                    
                        </div>





                         <div class="col-lg-2" >
                             <telerik:RadButton ID="ButtonUpdate" Enabled="false" OnClick="ButtonUpdate_Click" Skin="Black" runat="server"  Text="Update to Active">
                            <Icon PrimaryIconCssClass="rbSave" />
                            </telerik:RadButton> 
                        </div>
                        <div class="col-lg-7"></div>


        </div>

        <p></p>
    


        <div class="row">
                        <div class="col-lg-12" > 
                            <asp:Label ID="SuccessStatus" Width="100%" runat="server" Text="Employee Status Updated" Visible="false" style="background-color:#0F8F14;color:white;text-align:center;font-size: 21px;"></asp:Label>
                            <asp:Label ID="FailureAdded" Width="100%" runat="server" Text="Failed To Update Status" Visible="false" style="background-color:#FF0000;color:white;text-align:center;font-size: 21px;"></asp:Label>
                             </div>
        </div>


        <telerik:RadAjaxPanel ID="panelEmployeeHasTask" runat="server" Visible="false" >
             <br />



                <div class="row">

                            <div class="col-lg-2" >
                            <asp:Label ID="LabelEmployeeTask" runat="server" AssociatedControlID="DropDownTaskToEmployee">Assign Open Task To:</asp:Label>
                            </div>

                            <div class="col-lg-10"></div>
                </div>
                <div class="row">
                            <div class="col-lg-3" >
                            <telerik:RadComboBox RenderMode="Lightweight" 
                            runat="server" 
                            ID="DropDownTaskToEmployee"
                            IsTextSearchEnabled="True"
                            IsCaseSensitive="false"
                            Filter="Contains" 
                            Font-Size="14px" 
                            MarkFirstMatch="true"
                            Skin="Silk"
                            TabIndex="1" Width="250" 
                            OnSelectedIndexChanged="DropDownTaskToEmployee_SelectedIndexChanged"
                            DropDownWidth="170"
                            EnableLoadOnDemand="true"
                            AllowCustomText="false" 
                            AutoPostBack="true">
                            </telerik:RadComboBox>
                            </div>
        
<%--                            <div class="col-lg-10" >
                            </div>--%>

                            <div class="col-lg-2" >
                                <asp:Label ID="LabelTasks" CssClass="label label-danger"  Font-Size="15px" Visible="false" Enabled="false" runat="server" Text="Tasks" ></asp:Label>
                            </div>

                            <div class="col-lg-7"></div>



                </div>

                <p></p> 

<%--                <div class="row">
                            <div class="col-lg-1" >
                                <asp:Label ID="LabelTasks" CssClass="label label-danger"  Font-Size="15px" Visible="false" Enabled="false" runat="server" Text="Tasks" ></asp:Label>
                            </div>

                            <div class="col-lg-11"></div>
                </div>--%>


        </telerik:RadAjaxPanel>


    <br />


                <div class="row">
         <div class="pull-right">
            <div class="col-lg-12 pull-right">
           <telerik:RadRadioButtonList runat="server" ID="RadioButtonShowEmployees" Direction="Horizontal" OnSelectedIndexChanged="RadioButtonShowEmployees_SelectedIndexChanged" >
            <Items>
                <telerik:ButtonListItem Text="Show Active Users"  Value="N" />
                <telerik:ButtonListItem Text="Show InActive Users" Selected="true" Value="Y" />
                <telerik:ButtonListItem Text="Show Both Acitve and InActive" Value="B" />
            </Items>
        </telerik:RadRadioButtonList>
                </div>
             </div>
             </div>   
        

 <div class="row">
        <div class="col-lg-12">
            

            <telerik:RadGrid ID="RadGridEmployees" RenderMode="Lightweight" runat="server"
			OnItemDataBound="RadGridEmployees_ItemDataBound"
            OnNeedDataSource="RadGridEmployees_NeedDataSource"
                Width="99%"
			Visible="true"
			AllowPaging="true"
			ShowGroupPanel="true"
			
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
			
			ItemStyle-Font-Names="Arial"
			ItemStyle-Font-Size="Larger"
			ItemStyle-Font-Bold="true"
			ItemStyle-BackColor="White"
			
			AlternatingItemStyle-Font-Names="Arial"
			AlternatingItemStyle-Font-Size="Larger"
			AlternatingItemStyle-Font-Bold="true"
			AlternatingItemStyle-BackColor="WhiteSmoke"

			EnableLinqExpressions="false"

			Skin="Office2010Silver">

<ExportSettings>
     <Excel DefaultCellAlignment="Left" Format="Xlsx" WorksheetName="MOCListing" />
<Pdf PageWidth="297mm" PageHeight="210mm" />
</ExportSettings>



<MasterTableView CommandItemDisplay="Top" Width="100%" >
		<%--TableLayout="Fixed"--%>
		<GroupHeaderItemStyle Height="10px" /> 
		<PagerStyle AlwaysVisible="true" />

<CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="true" />
<Columns>
<%--setting Display=false will still let you have access to the data but will not display or export--%>

<telerik:GridBoundColumn HeaderStyle-Width="13%" AllowSorting="true" AllowFiltering="false"   DataField="username" HeaderText="NetworkID" ></telerik:GridBoundColumn>
<telerik:GridBoundColumn HeaderStyle-Width="11%" AllowSorting="true" AllowFiltering="true" DataField="lastname" HeaderText="Last Name" ></telerik:GridBoundColumn>
<telerik:GridBoundColumn HeaderStyle-Width="11%" AllowSorting="true" AllowFiltering="true" DataField="firstname" HeaderText="First Name" ></telerik:GridBoundColumn>
<telerik:GridBoundColumn HeaderStyle-Width="5%" AllowSorting="true" AllowFiltering="false" DataField="middleinit" HeaderText="MidInit" ></telerik:GridBoundColumn>
<telerik:GridBoundColumn HeaderStyle-Width="20%" AllowSorting="true" AllowFiltering="true" DataField="email" HeaderText="Email"  DataType="System.String"></telerik:GridBoundColumn>
<telerik:GridBoundColumn HeaderStyle-Width="5%" AllowSorting="true" AllowFiltering="false" DataField="domain" HeaderText="Domain" HeaderStyle-Wrap="true"  DataType="System.String"></telerik:GridBoundColumn>
<telerik:GridBoundColumn HeaderStyle-Width="9%" AllowSorting="true" AllowFiltering="false" DataField="default_language" HeaderText="Language" HeaderStyle-Wrap="true"  DataType="System.String"></telerik:GridBoundColumn>
<telerik:GridBoundColumn HeaderStyle-Width="5%" AllowSorting="true" AllowFiltering="false" DataField="newinactive_flag" HeaderText="Active" HeaderStyle-Wrap="true"  DataType="System.String"></telerik:GridBoundColumn>

</Columns>

</MasterTableView>

<ClientSettings ReorderColumnsOnClient="True" AllowDragToGroup="false" AllowColumnsReorder="True">
<Scrolling AllowScroll="true" UseStaticHeaders="true" ScrollHeight="" />
<Selecting AllowRowSelect="false"></Selecting>
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


        
        <div class="panelBottom">
                <telerik:RadAjaxLoadingPanel 
                ID="LoadingPanel1" 
                runat="server" 
                BackgroundPosition="Center" Skin="Telerik">
                </telerik:RadAjaxLoadingPanel>
            </div>

 

</div>

  </telerik:RadAjaxPanel> 









</asp:Content>

