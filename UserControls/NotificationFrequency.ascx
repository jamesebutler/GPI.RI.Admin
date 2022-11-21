<%@ Control Language="C#" AutoEventWireup="true"
    CodeBehind="NotificationFrequency.ascx.cs" 
    Inherits="GPI.RI.Admin.UserControls.NotificationFrequency" %>


<asp:Panel ID="_pnlNotificationFrequency" runat="server" 
    Width="98%" HorizontalAlign="left" style="min-height:100px">


    <div class="panel-body">
        <%--opt out of emails--%>
        <div class="row">
            <div class="col-xs-12">
                <div class="form-group">
                    <asp:Label ID="_lblOptOut" runat="server" Text="Opt Out of Email Notification"></asp:Label>
                    <asp:Literal ID="_litOptOut" runat="server" Text="<br />"></asp:Literal>
                    <asp:CheckBox ID="_cbOptOut"  Enabled="false" runat="server" Font-Bold="true" ForeColor="red" />
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-xs-12">
                <asp:Label ID="_lblWhenEntered" runat="server"></asp:Label>
                <div class="form-group">
                    <asp:RadioButtonList ID="_rblImmediate" runat="server" RepeatDirection="horizontal" RepeatColumns="2" Width="250px">
                    </asp:RadioButtonList>
                </div>
            </div>
        </div>


        <%--//Task Notification Frequency--%>
        <div class="row">
            <div class="col-xs-12">
                <asp:Label ID="_lblNotificationHeading" runat="server"></asp:Label><br />

            </div>
        </div>

        <div class="row">
            <div class="col-md-2">
                <div class="form-group">
                    <asp:RadioButton ID="_rbEveryDay" runat="server" />
                </div>
            </div>
            <div class="col-md-3">
                <div class="form-group">
                    <asp:RadioButton ID="_rbEveryWeek"
                        runat="server" />
                </div>
            </div>
            <div class="col-md-7">
                <div class="form-group">
                   &nbsp;&nbsp; <asp:DropDownList ID="_ddlDayOfWeek" runat="server" Style="min-width: 50px">
                    </asp:DropDownList>
                </div>
            </div>


        </div>



        
        <div class="row">
            <div class="col-md-5">
                <div class="form-group">
                    <asp:RadioButton ID="_rblEveryMonth" runat="server" />
               <%-- </div>
            </div>
            <div class="col-md-2">
                <div class="form-group">--%>
                    &nbsp; <asp:DropDownList
                        ID="_ddlOrdinalMonth" runat="server" Style="min-width: 50px">
                    </asp:DropDownList>
                </div>
            </div>

            <div class="col-md-7">
                <div class="form-group">
                    </div>
                </div>

        </div>


       <%-- Time period--%>
        <div class="row">
            <div class="col-md-12">
                <div class="form-group">
                    <asp:Label ID="_lblFutureTimePeriodHeader" runat="server"></asp:Label><br />
                    <asp:RadioButtonList ID="_rblFutureNotificationPeriod" runat="server" RepeatDirection="Horizontal" Width="100%">
                </asp:RadioButtonList>
            </div>
           </div>
        </div>


 <%--               <div class="row row-no-gutters" style="background-color:#367CCF;color:white" >
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
    
</asp:Panel>
