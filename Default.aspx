<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="GPI.RI.Admin._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h2>Administrator System for Employee, RI, MOC, and  Task Tracker</h2>
        <p class="lead">As an administrator you can do every thing from this site.</p>
<%--        <p><a href="http://www.asp.net" class="btn btn-primary btn-lg">Learn more &raquo;</a></p>
   --%> </div>

    <div class="row">
        <div class="col-md-3">
            <h2>Employee</h2>
            <%--<p>You can Add, Update, Delete Employess. </p>--%>
            <p>
                 <a class="btn btn-lg btn-primary" style="width: 220px;" href="#">Add &raquo;</a>
               <br /><br />
                <a class="btn btn-lg btn-primary" style="width: 220px;" href="#">Delete &raquo;</a>
               <br /> <br />
                <a class="btn btn-lg btn-primary" style="width: 220px;" href="#">Transfer &raquo;</a>
                           <br /> <br />
                <a class="btn btn-lg btn-primary" style="width: 220px;" href="#">Assign Roles &raquo;</a>
            </p>
        </div>
        <div class="col-md-3">
            <h2>RI</h2>

            <p>
                 <a class="btn btn-lg btn-primary" style="width: 220px;" href="#">RI Notification &raquo;</a>
               <br /><br />
                <a class="btn btn-lg btn-primary" style="width: 220px;" href="../RI/RINotificationMaintenance.aspx">RI Analysis Leaders &raquo;</a>
               <br /> <br />
                <a class="btn btn-lg btn-primary" style="width: 220px;" href="#">Location Maintenance &raquo;</a>

            
            </p>
        </div>
        <div class="col-md-3">
            <h2>MOC</h2>

           <p>
                 <a class="btn btn-lg btn-primary" style="width: 220px;" href="#">MOC Notification Email &raquo;</a>
               <br /><br />
                <a class="btn btn-lg btn-primary" style="width: 220px;" href="#">MOC Reviewers &raquo;</a>
               <br /> <br />
                <a class="btn btn-lg btn-primary" style="width: 220px;" href="#">Location Maintenance &raquo;</a>
            
            </p>
        </div>
                <div class="col-md-3">
            <h2>Task Tracker</h2>

           <p>
                 <a class="btn btn-lg btn-primary" style="width: 220px;" href="#">Task Notification Email &raquo;</a>
<%--               <br /><br />
                <a class="btn btn-lg btn-primary" style="width: 220px;" href="#">MOC Reviewers &raquo;</a>
               <br /> <br />
                <a class="btn btn-lg btn-primary" style="width: 220px;" href="#">Location Maintenance &raquo;</a>
            --%>
            </p>
        </div>
    </div>

</asp:Content>
