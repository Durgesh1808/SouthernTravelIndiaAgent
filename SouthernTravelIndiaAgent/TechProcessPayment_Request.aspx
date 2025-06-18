<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TechProcessPayment_Request.aspx.cs" Inherits="SouthernTravelIndiaAgent.TechProcessPayment_Request" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <meta http-equiv="refresh" content="javascript:J();"/>
    <script language="javascript" type="text/javascript">    
       
        window.history.forward(1);
    
        function doPost() {
             document.TechPro.submit();
        }                
       
    </script>
</head>
<body>
    <form id="TechPro" action="<%=rdurl%>" method="post"  name="TechPro" target="_parent">
    <div>
    
    </div>
    </form>
    <script language="javascript" type="text/javascript">
        doPost();
    </script>
</body>
</html>
