<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmployeeAdd.aspx.cs" Inherits="GPI.RI.Admin.Employee.EmployeeAdd" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
 

        <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
        
            <script type="text/javascript">
            /*<![CDATA[*/
            
                /*]]>*/
            </script>
        </telerik:RadScriptBlock>


<telerik:RadAjaxPanel ID="RadAjaxPanel" runat="server">

         <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
  
         <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="DropDownSites">
                    <UpdatedControls>
                     <telerik:AjaxUpdatedControl ControlID="DropDownSites" ></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="RadGridEmployees" LoadingPanelID="LoadingPanel1"></telerik:AjaxUpdatedControl>
                    </UpdatedControls>
                </telerik:AjaxSetting>

                 <telerik:AjaxSetting AjaxControlID="RadioButtonShowEmployees">
                    <UpdatedControls>
                     <telerik:AjaxUpdatedControl ControlID="RadioButtonShowEmployees" ></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="RadGridEmployees" LoadingPanelID="LoadingPanel1"></telerik:AjaxUpdatedControl>
   
                    </UpdatedControls>
                </telerik:AjaxSetting>


                <telerik:AjaxSetting AjaxControlID="ButtonSearchForByEmail">
                    <UpdatedControls>
                     <telerik:AjaxUpdatedControl ControlID="ButtonSearchForByEmail" ></telerik:AjaxUpdatedControl>
                         <telerik:AjaxUpdatedControl ControlID="ButtonAddEmployee" ></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="EmailFound" ></telerik:AjaxUpdatedControl>
                      <telerik:AjaxUpdatedControl ControlID="EmailNotFound" ></telerik:AjaxUpdatedControl>
                     
                        <telerik:AjaxUpdatedControl ControlID="panelEmployeeInfo" LoadingPanelID="LoadingPanel1"></telerik:AjaxUpdatedControl>
                    </UpdatedControls>
                </telerik:AjaxSetting>



        </AjaxSettings>

        </telerik:RadAjaxManager>



    <h3>Reliability Employee Maintenance</h3>
    <h4>Add Employee</h4>
 
<div class="container">
   <div class="row"> 
    <div class="col-lg-12">
    <asp:Label ID="LabelFacility" runat="server" AssociatedControlID="DropDownSites">Facility:</asp:Label>
    <telerik:RadComboBox RenderMode="Lightweight" 
                    runat="server" 
                    OnSelectedIndexChanged="DropDownSites_SelectedIndexChanged"
                    ID="DropDownSites"
                    Skin="Silk"
                    TabIndex="1" Width="200" 
                    DropDownWidth="200"
                    EnableLoadOnDemand="true"
                    AllowCustomText="false" 
                    AutoPostBack="true">
         </telerik:RadComboBox>
</div>
       </div>
<br />

<div class="row">

<div class="col-lg-12" style="background-color:#E0E1E2;">
<h4>Enter the email then Click "Search".  If the email is found the nessary fields will populate.  Click on the "Add Employee" button to add employee.</h4>
</div>
</div>
    <br />
        <div class="row" >


            <div class="col-lg-4" >
                            <asp:Label ID="LabelLookUpByEmail" runat="server" AssociatedControlID="EmailTextBox">Email:</asp:Label>
                            <telerik:RadTextBox RenderMode="Lightweight" EmptyMessage="enter email" ID="EmailTextBox" AssociatedControlID="ButtonSearchForByEmail" Width="310" runat="server" />
               

            </div>
            
            <div class="col-lg-2" >
                    <telerik:RadButton ID="ButtonSearchForByEmail" OnClick="ButtonSearchForByEmail_Click" Skin="Black" runat="server"  Text="Search">
                    <Icon PrimaryIconCssClass="rbSearch" />
                    </telerik:RadButton>
                            <h4>  <p class="text-success" id="EmailFound" runat="server" visible="false">Success</p></h4>
             <p class="text-danger"  id="EmailNotFound" runat="server" visible="false">Not Found</p>
 
       
            </div>

            <div class="col-lg-6  pull-left" >
     
                </div>



        </div>

    <br />
<telerik:RadAjaxPanel ID="panelEmployeeInfo" runat="server" >

<%--     <div class="row">
      
         <div class="col-lg-12">
                     <img src="../images/found.png" width="20" height="20" id="ImageFound" runat="server" visible="true" />
                      <img src="../images/notfound.png"  width="30" height="30"  id="ImageNotFound" runat="server" visible="false" />
           
             </div>
         </div>
      --%>
 <div class="row">
      
         <div class="col-lg-3">
             <asp:Label ID="LabelNetWorkID" runat="server" >NetWorkID:</asp:Label><br />
              <telerik:RadTextBox RenderMode="Lightweight" ReadOnly="true" ID="TextBoxNetWorkID" Width="200px" runat="server" />
        </div>

            <div class="col-lg-3">
             <asp:Label ID="LabelLastName" runat="server" >Last Name:</asp:Label><br />
              <telerik:RadTextBox RenderMode="Lightweight" ReadOnly="true" ID="TextBoxLastName" Width="200px" runat="server" />
        </div>

        <div class="col-lg-3">
             <asp:Label ID="LabelFirstName" runat="server" >First Name:</asp:Label><br />
              <telerik:RadTextBox RenderMode="Lightweight" ReadOnly="true" ID="TextBoxFirstName" Width="200px" runat="server" />
        </div>

            <div class="col-lg-3">
             <asp:Label ID="LabelMidInit" runat="server" >MidInit:</asp:Label><br />
              <telerik:RadTextBox RenderMode="Lightweight" ReadOnly="true" ID="TextBoxMidInit" Width="200px" runat="server" />
        </div>

    </div>

        <div class="row">
        <div class="col-lg-3">
             <asp:Label ID="LabelEmailAddress" runat="server" >Email Address:</asp:Label><br />
              <telerik:RadTextBox RenderMode="Lightweight" ReadOnly="true" ID="TextBoxEmailAddress" Width="250px" runat="server" />
        </div>

            <div class="col-lg-3">
             <asp:Label ID="LabelPhoneNumber" runat="server" >Phone Number:</asp:Label><br />
              <telerik:RadTextBox RenderMode="Lightweight" ReadOnly="true" ID="TextBoxPhoneNumber" Width="200px" runat="server" />
        </div>

        <div class="col-lg-3">
             <asp:Label ID="LabelDomain" runat="server" >Domain:</asp:Label><br />
              <telerik:RadTextBox RenderMode="Lightweight" ReadOnly="true" ID="TextBoxDomain" Width="200px" runat="server" />
        </div>

            <div class="col-lg-3">
             <asp:Label ID="LabelDefaultLang" runat="server" >Default Language:</asp:Label><br />
              <telerik:RadTextBox RenderMode="Lightweight" ReadOnly="true" ID="TextBoxDefaultLang" Width="200px" runat="server" />
        </div>

    </div>

</telerik:RadAjaxPanel>

    <p></p>

      <div class="row">
        <div class="col-lg-5">
         </div>
            


        <div class="col-lg-7">
                 <telerik:RadButton ID="ButtonAddEmployee" Enabled="false" OnClick="ButtonSearchForByEmail_Click" Skin="Black" runat="server"   Text="Add Employee">
                <Icon PrimaryIconCssClass="rbAdd" />
                    </telerik:RadButton>
            </div>
                </div>

<br />

         <div class="row">
         <div class="pull-right">
            <div class="col-lg-12 pull-right">
           <telerik:RadRadioButtonList runat="server" ID="RadioButtonShowEmployees" Direction="Horizontal" OnSelectedIndexChanged="RadioButtonShowEmployees_SelectedIndexChanged" >
            <Items>
                <telerik:ButtonListItem Text="Show Active Users" Selected="true" Value="N" />
                <telerik:ButtonListItem Text="Show InActive Users" Value="Y" />
                <telerik:ButtonListItem Text="Show Both Acitve and InActive" Value="B" />
            </Items>
        </telerik:RadRadioButtonList>
                </div>
             </div>
             </div>

<%--     <div class="row">
         <div class="pull-right">
            <div class="col-lg-11 pull-right">
                <telerik:RadCheckBox ID="CheckBoxInAcitveOnly" OnCheckedChanged="CheckBoxAcitveOnly_CheckedChanged"  Checked="false" runat="server" Text="Show Only Inactive Users"  >
                </telerik:RadCheckBox>
                <telerik:RadCheckBox ID="CheckBoxAcitveOnly" OnCheckedChanged="CheckBoxAcitveOnly_CheckedChanged"  Checked="false" runat="server" Text="Show Inactive Users"  >
                </telerik:RadCheckBox>
                </div>
         
                 <div class="col-lg-1">
                </div>
        </div>
     </div>--%>


      <div class="row">
        <div class="col-lg-12">
            

            <telerik:RadGrid ID="RadGridEmployees" RenderMode="Lightweight" runat="server"
			OnItemDataBound="RadGridEmployees_ItemDataBound"
                OnNeedDataSource="RadGridEmployees_NeedDataSource"
                idth="99%"
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

<CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="false" />
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

<PagerStyle Mode="NextPrevAndNumeric" PageSizeControlType="None"></PagerStyle>




</telerik:RadGrid>  
        
        </div>
        </div>


</div>

            <div class="panelBottom">
            <telerik:RadAjaxLoadingPanel 
                ID="LoadingPanel1" 
                runat="server" 
                BackgroundPosition="Center">
            </telerik:RadAjaxLoadingPanel>
        </div>

</telerik:RadAjaxPanel>

</asp:Content>
