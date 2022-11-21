<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmployeeNotifications.aspx.cs" Inherits="GPI.RI.Admin.Employee.EmployeeNotifications" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
















<div class="row">
                <div class="panel-footer">
                    <div class="col-md-3 col-md-offset-2">
                         <asp:LinkButton ID="_btnCancel" runat="server" SkinID="Button">
                                <span class="glyphicon glyphicon-remove"></span>&nbsp;<asp:Label ID="Label6" runat="server" Text="<%$IPResources:Global,Cancel%>"></asp:Label>
                            </asp:LinkButton>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <asp:LinkButton ID="_btnSave" runat="server" SkinID="ButtonPrimary">
                                <span class="glyphicon  glyphicon-floppy-save"></span>&nbsp;<asp:Label ID="_lblSaveButton" runat="server" Text="<%$IPResources:Global,Save Changes%>"></asp:Label>
                            </asp:LinkButton>
                        </div>

                        
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <asp:LinkButton ID="_btnDefaultSettings" runat="server" SkinID="Button">
                                <span class="glyphicon glyphicon-refresh"></span>&nbsp;<asp:Label ID="Label5" runat="server" Text="<%$IPResources:Global,Use Default Settings%>"></asp:Label>
                            </asp:LinkButton>
                        </div>
                        
                    </div>
                </div>
            </div>


</asp:Content>
