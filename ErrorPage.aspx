<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="GPI.RI.Admin.ErrorPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>RI Admin Error</title>

            <link href="styles/Default.css" rel="stylesheet" />
    <link href="styles/Notes.css" rel="stylesheet" />
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
        <%--<link href="Menu/Data/styles.css" rel="stylesheet" />--%>



    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
    </asp:PlaceHolder>
    <webopt:bundlereference runat="server" path="~/Content/css" />

</head>
<body>
    <form id="form1" runat="server">
  <div class="container-fluid"> 
                 <div class="row" style="background-color:white;color:red" >
                <div class="col-lg-1 .align-middle" ><img src="../Images/LogoOnly.png" alt="GPI Logo" width="55" height="55"    /></div>
                <div class="col-lg-11 .align-middle" style="text-align:left;" ><h2>Error has occured.</h2></div>
 
      </div>
   

                 <div class="row" style="background-color:#367CCF;color:white" >
                <div class="col-lg-12" ></div>

            </div>
          


<%-- <div class="col-lg-12" >
<p class="noteAltNoImage">
<strong>
An Error has been logged.  <br />something......  <br />and more....
</strong>
</p>
</div>--%>

          <div class="jumbotron" style="background-color:cornsilk">
        <h2>An Error has been logged.</h2>
        <p class="lead">Email: james.butler@graphicpkg.com with the following error:</p>
<asp:Label ID="errormessage" runat="server" Text=""></asp:Label>
<%--        <p><a href="http://www.asp.net" class="btn btn-primary btn-lg">Learn more &raquo;</a></p>
   --%> </div>


</div>
    </form>
</body>
</html>
