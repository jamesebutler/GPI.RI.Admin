<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" 
    AutoEventWireup="true" CodeBehind="TaskTrackerNotifications.aspx.cs" 
    Inherits="GPI.RI.Admin.TaskTracker.TaskTrackerNotifications" %>

<%@ MasterType VirtualPath="~/Site.Master" %>

<%@ Register Src="~/UserControls/NotificationFrequency.ascx" TagName="NotificationFrequency" TagPrefix="IP" %>



<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

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


   </AjaxSettings>

    </telerik:RadAjaxManager>



<div class="container">

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

        <div class="row"> 
    <div class="col-lg-12">
            <h3><asp:Label ID="LabelAddMill" runat="server" Text="Notifications for [mill]" ></asp:Label></h3>
     
    </div>
    </div>

        <div class="row row-no-gutters"> 

                    <div class="col-lg-2">
                        <asp:Label ID="LabelEmployee" runat="server" AssociatedControlID="DropDownEmployees">Employee:</asp:Label>
                    </div>
                    <div class="col-lg-10"></div>

    </div>

    <div class="row">
    <div class="col-lg-12">
                        <telerik:RadComboBox RenderMode="Lightweight" 
                        runat="server"
                        IsTextSearchEnabled="True"
                        IsCaseSensitive="false"
                        Filter="Contains" 
                        Font-Size="14px" 
                        MarkFirstMatch="true"
                        ID="DropDownEmployees"
                        Skin="Silk"
                        TabIndex="1" Width="170" 
                        OnSelectedIndexChanged="DropDownEmployees_SelectedIndexChanged"
                        DropDownWidth="170"
                        EnableLoadOnDemand="true"
                        AllowCustomText="false" 
                        AutoPostBack="true">
                        </telerik:RadComboBox>
                    
        </div>
        </div>
    <br />

    <%-- ====================== Task Creator ======================--%>
                <div class="row" >
                <div class="col-md-8">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <div class="panel-title">
                            <asp:Label ID="LabelTaskCreator" runat="server" Text="When I'm The Task Creator Notify Me"></asp:Label>
                            </div>
                       </div>

                         <div class="row">
                        <div class="col-md-12">
                             <div class="form-group">
                                  <div class="form-group">
                           </div>
                            </div>
                        </div>
                        </div>



                        <div class="row">
                        <div class="col-md-4">
                             <div class="form-group">
                                  <div class="form-group">
                                       &nbsp; &nbsp;&nbsp;&nbsp;<asp:Label ID="_lblOptOut" Font-Bold="true" runat="server" Text="Opt Out of Email Notification"></asp:Label>
                                        <asp:Literal ID="_litOptOut" runat="server" Text="<br />"></asp:Literal>
                                       &nbsp; &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="_cbOptOut"  Enabled="true" runat="server"  Font-Bold="true" Text="Do not send Emails" ForeColor="red" />
                                </div>
                            </div>
                        </div>
                        </div>



                        <div class="row">
                             
                            <div class="col-md-1"></div>
                        <div class="col-md-8">
                        <div class="form-group">
                            <asp:Label ID="_lblNotificationFrequencyCreator" Font-Bold="true" Text="Task Notification Frequency" runat="server"></asp:Label><br />
                            <asp:RadioButton ID="_rbEveryDay" runat="server" Text="Daily" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:RadioButton ID="_rbEveryWeek" Text="Weekly on"
                            runat="server" />
                            &nbsp;&nbsp;&nbsp;&nbsp; <asp:DropDownList ID="_ddlDayOfWeek" runat="server" Style="min-width: 50px">
                            </asp:DropDownList>
                 
                            &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                            <asp:RadioButton ID="_rblEveryMonth" Text="Monthly on day" runat="server" />
                            &nbsp; <asp:DropDownList
                            ID="_ddlOrdinalMonth" runat="server" Style="min-width: 50px">
                            </asp:DropDownList>
                            </div>
         

                            <div class="col-md-3"><div class="form-group"></div>

                        </div>
                        </div>
                                 
                        </div>



                        <div class="row">
                            <div class="col-md-1"></div>
                        <div class="col-md-10">
                        <div class="form-group">
                        <asp:Label ID="_lblFutureTimePeriodHeader" Font-Bold="true" Text="Notification Time Period for Future Tasks" runat="server"></asp:Label><br />
                         
                        <asp:RadioButtonList ID="_rblFutureNotificationPeriod" runat="server" RepeatDirection="Horizontal" Width="100%">
                        </asp:RadioButtonList>
                        </div>
                        </div>


                        <div class="col-md-1"><div class="form-group"></div></div> 

                     </div>

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

        </div>
    </div> 
</div>
   
<%-- ======================================================--%>               
                
         
    
    
 <%-- ====================== Responsible Person ======================--%>    
                 <div class="row" >
                <div class="col-md-8">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <div class="panel-title">
                            <asp:Label ID="LabelResponsiblePerson" runat="server" Text="When I'm The Responsible Person Notify Me"></asp:Label>
                            </div>
                       </div>


                        <div class="row">
                        <div class="col-md-12">
                             <div class="form-group">
                                  <div class="form-group">
                           </div>
                            </div>
                        </div>
                        </div>

                    <div class="row">
                             
                            <div class="col-md-1"></div>
                        <div class="col-md-8">
                        <div class="form-group">
                            <asp:Label ID="_lblEnteredNotify" Font-Bold="true" Text="When tasks are Entered Notify Me" runat="server"></asp:Label><br />
                            <asp:RadioButton ID="_rbEnteredNotifEveryDay" runat="server" Text="Immediately" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:RadioButton ID="_rbEnteredNotifEveryWeek" Text="Daily"
                            runat="server" />


                            </div>
         

                            <div class="col-md-3"><div class="form-group"></div>

                        </div>
                        </div>
                                 
                        </div>



                        <div class="row">
                             
                            <div class="col-md-1"></div>
                        <div class="col-md-8">
                        <div class="form-group">
                            <asp:Label ID="_lblResponsiblePersonFrequencyCreator" Font-Bold="true" Text="Task Notification Frequency" runat="server"></asp:Label><br />
                            <asp:RadioButton ID="_rbResponsiblePersonEveryDay" runat="server" Text="Daily" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:RadioButton ID="_rbResponsiblePersonEveryWeek" Text="Weekly on"
                            runat="server" />
                            &nbsp;&nbsp;&nbsp;&nbsp; <asp:DropDownList ID="_ddlResponsiblePersonDayOfWeek" runat="server" Style="min-width: 50px">
                            </asp:DropDownList>

                            </div>
         

                            <div class="col-md-3"><div class="form-group"></div>

                        </div>
                        </div>
                                 
                        </div>



                        <div class="row">
                            <div class="col-md-1"></div>
                        <div class="col-md-10">
                        <div class="form-group">
                        <asp:Label ID="_lblResponsiblePersonFutureTimePeriodHeader" Font-Bold="true" Text="Notification Time Period for Future Tasks" runat="server"></asp:Label><br />
                         
                        <asp:RadioButtonList ID="_rblResponsiblePersonFutureNotificationPeriod" runat="server" RepeatDirection="Horizontal" Width="100%">
                        </asp:RadioButtonList>
                        </div>
                        </div>


                        <div class="col-md-1"><div class="form-group"></div></div> 

                     </div>

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

        </div>
    </div> 
</div>
 <%-- ======================================================--%>    
    
    
 <%-- ====================== Business Unit Manager or Type Manager ======================--%> 

           <div class="row" >
                <div class="col-md-8">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <div class="panel-title">
                            <asp:Label ID="LabelManager" runat="server" Text="When I'm The Business Unit Manager or Type Manager Notify Me"></asp:Label>
                            </div>
                       </div>

                       <div class="row">
                        <div class="col-md-12">
                             <div class="form-group">
                                  <div class="form-group">
                           </div>
                            </div>
                        </div>
                        </div>


                        <div class="row">
                             
                            <div class="col-md-1"></div>
                        <div class="col-md-8">
                        <div class="form-group">
                            <asp:Label ID="_lblNotificationManagerFrequencyCreator" Font-Bold="true" Text="Task Notification Frequency" runat="server"></asp:Label><br />
                            <asp:RadioButton ID="_rbManagerEveryDay" runat="server" Text="Daily" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:RadioButton ID="_rbManagerEveryWeek" Text="Weekly on"
                            runat="server" />
                            &nbsp;&nbsp;&nbsp;&nbsp; <asp:DropDownList ID="_ddlManagerDayOfWeek" runat="server" Style="min-width: 50px">
                            </asp:DropDownList>
                 
                            &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                            <asp:RadioButton ID="_rblManagerEveryMonth" Text="Monthly on day" runat="server" />
                            &nbsp; <asp:DropDownList
                            ID="_ddlManagerOrdinalMonth" runat="server" Style="min-width: 50px">
                            </asp:DropDownList>
                            </div>
         

                            <div class="col-md-3"><div class="form-group"></div>

                        </div>
                        </div>
                                 
                        </div>



                        <div class="row">
                            <div class="col-md-1"></div>
                        <div class="col-md-10">
                        <div class="form-group">
                        <asp:Label ID="_lblManagerFutureTimePeriodHeader" Font-Bold="true" Text="Notification Time Period for Future Tasks" runat="server"></asp:Label><br />
                         
                        <asp:RadioButtonList ID="_rblManagerFutureNotificationPeriod" runat="server" RepeatDirection="Horizontal" Width="100%">
                        </asp:RadioButtonList>
                        </div>
                        </div>


                        <div class="col-md-1"><div class="form-group"></div></div> 

                     </div>



        </div>
    </div> 
</div>        
    
  <%-- ======================================================--%>    
       


            <div class="row">
               <%-- <div class="panel-footer">--%>

                    
                    <div class="col-md-2">
                    <div class="form-group">
                     <telerik:RadButton ID="ButtonSave"  Skin="Black" runat="server"  Text="Save">
                    <Icon PrimaryIconCssClass="rbSave" />
                    </telerik:RadButton>
                     </div>   
                    </div>
                    
                    
                    <div class="col-md-2">
                        <div class="form-group">
                        <telerik:RadButton ID="ButtonDefault"  Skin="Black" runat="server"  Text="Use Mill Defaults">
                         <Icon PrimaryIconCssClass="rbSave" />
                         </telerik:RadButton>
                        </div>
                    </div>

                <%--</div>--%>
            </div>


    </div>

    </telerik:RadAjaxPanel>
</asp:Content>
