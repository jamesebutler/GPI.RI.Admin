<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" 
    AutoEventWireup="true" CodeBehind="EmployeeStatus.aspx.cs" 
    Inherits="GPI.RI.Admin.Employee.EmployeeStaus" %>
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





<telerik:RadAjaxPanel ID="RadAjaxPanel" runat="server">

   <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
   <AjaxSettings>
             

          

              <telerik:AjaxSetting AjaxControlID="RadioButtonShowEmployees">
                    <UpdatedControls>
                        
                          <telerik:AjaxUpdatedControl ControlID="panelEmployeeHasTask" ></telerik:AjaxUpdatedControl>

                  <%--   <telerik:AjaxUpdatedControl ControlID="ButtonUpdate"></telerik:AjaxUpdatedControl>
                   --%>
        

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
                   
                    </UpdatedControls>
             </telerik:AjaxSetting>
      

    </AjaxSettings>

    </telerik:RadAjaxManager>


           <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server" Visible="false">
        
                <AjaxSettings>

                    <telerik:AjaxSetting AjaxControlID="ButtonUpdate">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="SuccessStatus" ></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="FailureAdded" ></telerik:AjaxUpdatedControl>
     
                        <telerik:AjaxUpdatedControl ControlID="ButtonUpdate" LoadingPanelID="LoadingPanel1"></telerik:AjaxUpdatedControl>
                   <telerik:AjaxUpdatedControl ControlID="DropDownTaskToEmployee" ></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="DropDownEmployees" LoadingPanelID="LoadingPanel1" ></telerik:AjaxUpdatedControl>
                     <telerik:AjaxUpdatedControl ControlID="panelEmployeeHasTask" ></telerik:AjaxUpdatedControl>
                    
                        

                    </UpdatedControls>
             </telerik:AjaxSetting>

                </AjaxSettings>
           </telerik:RadAjaxManagerProxy>



  

<div class="container">   <%--this will make the form narrow--%>
<%-- <div class="container-fluid">--%>

<%--    <div class="row row-no-gutters" style="background-color:#367CCF;color:white" >
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
    <div class="col-lg-12" >
            <p class="noteAltNoImage">
            <strong>
            Select the Employee to update the status. The status of the employee is indicated.  <br />
            If the employee has any open tasks, assign tasks to another Mill employee.  <br />
            Click "Update" 
            </strong>
            </p>
    </div>
    </div>


    <div class="row"> 
            <div class="col-lg-12">
                <h3><asp:Label ID="LabelSetStatus" runat="server" Text="Set Status of Employee" ></asp:Label></h3>
            </div>
    </div>
 


    <%--    <p class="noteAltNoImage">
        <strong>Note:</strong> Be sure to
        <a href="https://mdbootstrap.com/docs/standard/layout/grid/">read the Grid page</a>
        first before diving into how to modify and customize your grid columns.
      </p>--%>


             <div class="row">
         <div class="pull-left">
            <div class="col-lg-12 pull-right">
                   <telerik:RadRadioButtonList runat="server" ID="RadioButtonShowEmployees" Direction="Horizontal" OnSelectedIndexChanged="RadioButtonShowEmployees_SelectedIndexChanged" >
                    <Items>
                        <telerik:ButtonListItem Text="Select Active Users" Selected="true" Value="N" />
                        <telerik:ButtonListItem Text="Select InActive Users" Value="Y" />
                
                    </Items>
                </telerik:RadRadioButtonList>
                </div>
             </div>
             </div>
    <br />


    <div class="row row-no-gutters"> 

                    <div class="col-lg-3">
                        <asp:Label ID="LabelEmployee" runat="server" >Employee:</asp:Label>
                    </div>
                    <div class="col-lg-2">
                        <asp:Label ID="LabelUpdateStatusTo" runat="server"  Text="Update Status To:" ></asp:Label>
                    </div>
                    <div class="col-lg-7"></div>

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




                    <div class="col-lg-2">
                        
                    <asp:Label ID="LabelSetStatusActive" runat="server" Visible="false" Font-Bold="true"  Text="Active"  ></asp:Label>
                    <asp:Label ID="LabelSetStatusInActive" runat="server"    Text="InActive" Font-Bold="true"  ForeColor="Red" ></asp:Label>
            
                    </div>

                     <div class="col-lg-2" >
                         <telerik:RadButton ID="ButtonUpdate" Enabled="false" OnClick="ButtonUpdate_Click" Skin="Black" runat="server"  Text="Update">
                        <Icon PrimaryIconCssClass="rbSave" />
                        </telerik:RadButton> 
                    </div>
                    <div class="col-lg-5"></div>


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
                <div class="col-lg-2" >
                <telerik:RadComboBox RenderMode="Lightweight" 
                runat="server" 
                ID="DropDownTaskToEmployee"
                IsTextSearchEnabled="True"
                IsCaseSensitive="false"
                Filter="Contains" 
                Font-Size="14px" 
                MarkFirstMatch="true"
                Skin="Silk"
                TabIndex="1" Width="170" 
                OnSelectedIndexChanged="DropDownTaskToEmployee_SelectedIndexChanged"
                DropDownWidth="170"
                EnableLoadOnDemand="true"
                AllowCustomText="false" 
                AutoPostBack="true">
                </telerik:RadComboBox>
                </div>
        
                <div class="col-lg-10" >
                </div>
    </div>

    <p></p> 

    <div class="row">
                <div class="col-lg-1" >
                    <asp:Label ID="LabelTasks" CssClass="label label-danger"  Font-Size="15px" Visible="false" Enabled="false" runat="server" Text="Tasks" ></asp:Label>
                </div>

                <div class="col-lg-11"></div>
    </div>

</div>

</telerik:RadAjaxPanel>

<%--<hr class="hr hr-blurry" />--%>


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
            BackgroundPosition="Center" Skin="Telerik">
            </telerik:RadAjaxLoadingPanel>
        </div>

</div>
</telerik:RadAjaxPanel>






</asp:Content>
