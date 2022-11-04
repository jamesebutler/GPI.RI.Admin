<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmployeeTransfer.aspx.cs" Inherits="GPI.RI.Admin.Employee.EmployeeTransfer" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
 




        <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
        
            <script type="text/javascript">
            /*<![CDATA[*/
            
                /*]]>*/
            </script>
        </telerik:RadScriptBlock>


<telerik:RadAjaxPanel ID="RadAjaxPanel" runat="server">


    <div class="container">

                    <div class="row" style="background-color:#367CCF;color:white" >
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
            </div>

    <div class="row"> 
    <div class="col-lg-12">
    <h3><asp:Label ID="LabelFromMill" runat="server" Text="dd" ></asp:Label></h3>
     
    </div>
    </div>
 


<div class="row">
<div class="col-lg-12" style="background-color:#E0E1E2;">
<h4>Select the Employee to transfer.  Select the Mill to transfer to and Click "Transfer."  If there are any open Tasks, select an employee to transfer their open Task.</h4>
           
</div>
</div>

    <p></p>


<div class="row"> 
    <div class="col-lg-7">
    
            <asp:Label ID="LabelEmployee" runat="server" AssociatedControlID="DropDownEmployees">Employee:</asp:Label>
            <telerik:RadComboBox RenderMode="Lightweight" 
                            runat="server" 
                            ID="DropDownEmployees"
                            Skin="Silk"
                            TabIndex="1" Width="200" 
  
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

                    

         <div class="col-lg-2" style="background-color:#367CCF;">
                                 <telerik:RadButton ID="ButtonTransfer" OnClick="ButtonTransfer_Click" Skin="Black" runat="server"  Text="Transfer">
                    <Icon PrimaryIconCssClass="rbNext" />
                    </telerik:RadButton>
             </div>

             <div class="col-lg-3" style="background-color:#367CCF;color:white">
                  <asp:Label ID="SuccessAdded" Width="100%" runat="server" Text="Employee Transfered" Visible="true" style="background-color:#0F8F14;color:white;text-align:center;font-size: 21px;"></asp:Label>
                     
             </div>
</div>






















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
