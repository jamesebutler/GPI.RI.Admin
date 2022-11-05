<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmployeeTransfer.aspx.cs" Inherits="GPI.RI.Admin.Employee.EmployeeTransfer" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
 

    <style type="text/css">
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
             
             <telerik:AjaxSetting AjaxControlID="DropDownEmployees">
                    <UpdatedControls>
                    </UpdatedControls>
             </telerik:AjaxSetting>

             <telerik:AjaxSetting AjaxControlID="DropDownTaskToEmployee">
                    <UpdatedControls>
                         <telerik:AjaxUpdatedControl ControlID="ButtonTransfer" ></telerik:AjaxUpdatedControl>
                    
                        
                    </UpdatedControls>
             </telerik:AjaxSetting>


        </AjaxSettings>

        </telerik:RadAjaxManager>



  

<div class="container">

<%--<div class="row" style="background-color:#367CCF;color:white" >
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

<div class="row"> 
    <div class="col-lg-12">
            <h3><asp:Label ID="LabelFromMill" runat="server" Text="dd" ></asp:Label></h3>
     
    </div>
    </div>
 


<%--    <p class="noteAltNoImage">
    <strong>Note:</strong> Be sure to
    <a href="https://mdbootstrap.com/docs/standard/layout/grid/">read the Grid page</a>
    first before diving into how to modify and customize your grid columns.
  </p>--%>


<div class="row">
<div class="col-lg-12" >
    <p class="noteAltNoImage">
<strong>
Select the Employee to transfer.  <br />Select the Mill to transfer to and Click "Transfer." <br /> If the employee has any open tasks, assign tasks to another Mill employee.
</strong>
</p>
        
</div>
</div>


<div class="row"> 
    <div class="col-lg-7">
            <asp:Label ID="LabelEmployee" runat="server" AssociatedControlID="DropDownEmployees">Employee:</asp:Label>
            <telerik:RadComboBox RenderMode="Lightweight" 
                            runat="server" 
                            ID="DropDownEmployees"
                            Skin="Silk"
                            TabIndex="1" Width="200" 
                            OnSelectedIndexChanged="DropDownEmployees_SelectedIndexChanged"
                            DropDownWidth="200"
                            EnableLoadOnDemand="true"
                            AllowCustomText="false" 
                            AutoPostBack="true">
                 </telerik:RadComboBox>
        &nbsp;&nbsp;

                <asp:Label ID="LabelFacility" runat="server" AssociatedControlID="DropDownSites">Transfer to:</asp:Label>
            &nbsp;&nbsp;<telerik:RadComboBox RenderMode="Lightweight" 
                            runat="server" 
                            ID="DropDownSites"
                            Skin="Silk"
                            TabIndex="1" Width="200" 
                            DropDownWidth="200"
                            EnableLoadOnDemand="true"
                            AllowCustomText="false" 
                            AutoPostBack="true">
                 </telerik:RadComboBox>
            
        </div>
             <div class="col-lg-2" >
                                 <telerik:RadButton ID="ButtonTransfer" Enabled="false" OnClick="ButtonTransfer_Click" Skin="Black" runat="server"  Text="Transfer">
                    <Icon PrimaryIconCssClass="rbNext" />
                    </telerik:RadButton>
             </div>

             <div class="col-lg-3" >
                  <asp:Label ID="SuccessAdded" Width="100%" runat="server" Text="Employee Transfered" Visible="false" style="background-color:#0F8F14;color:white;text-align:center;font-size: 21px;"></asp:Label>
                     
            </div>
</div>


       



<telerik:RadAjaxPanel ID="panelEmployeeHasTask" runat="server" Visible="false" >
     <br />
<div class="row">
<div class="col-lg-12" >



            <asp:Label ID="LabelEmployeeTask" runat="server" AssociatedControlID="DropDownTaskToEmployee">Assign open tasks to:</asp:Label>
           &nbsp;&nbsp; <telerik:RadComboBox RenderMode="Lightweight" 
                            runat="server" 
                            ID="DropDownTaskToEmployee"
                            Skin="Silk"
                            TabIndex="1" Width="200" 
                            OnSelectedIndexChanged="DropDownTaskToEmployee_SelectedIndexChanged"
                            DropDownWidth="200"
                            EnableLoadOnDemand="true"
                            AllowCustomText="false" 
                            AutoPostBack="true">
                 </telerik:RadComboBox>



</div>
</div>

</telerik:RadAjaxPanel>

<hr class="hr hr-blurry" />


   <div class="row"> 
    <div class="col-lg-12" style="background-color:#367CCF;color:white">
    <h4><asp:Label ID="LabelTasks" runat="server" Text="Tasks" ></asp:Label></h4>
    </div>
    </div>

<%--<br />
    <div class="row"> 
    <div class="col-lg-12" style="background-color:#367CCF;color:white">
    <h4><asp:Label ID="LabelReviewer" runat="server" Text="Reviewer" ></asp:Label></h4>
    </div>
    </div>
<br />
     <div class="row"> 
    <div class="col-lg-12" style="background-color:#367CCF;color:white">
    <h4><asp:Label ID="LabelRoles" runat="server" Text="Roles" ForeColor="white" ></asp:Label></h4>
    </div>
    </div>--%>

<br />
















            <div class="panelBottom">
            <telerik:RadAjaxLoadingPanel 
                ID="LoadingPanel1" 
                runat="server" 
                BackgroundPosition="Center">
            </telerik:RadAjaxLoadingPanel>

            </div>
</div>
</telerik:RadAjaxPanel>

</asp:Content>
