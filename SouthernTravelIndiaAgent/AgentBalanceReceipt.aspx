<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgentBalanceReceipt.aspx.cs" Inherits="SouthernTravelIndiaAgent.AgentBalanceReceipt" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
      <title>Balance Clear Receipt</title>
    <link href="../Assets/css/stylesheet.css" type="text/css" rel="stylesheet" />
    <link href="../Assets/css/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <table id="table3" cellspacing="0" cellpadding="0" bgcolor="#ffffff" border="0">
        <tr valign="top">
            <td class="hlinks" align="center">
                <font style="font-family: Arial; font-size: large;"><u>Thank You For Choosing Southern
                    Travels</u></font><br />
                &nbsp;&nbsp;&nbsp;&nbsp;Balance Clearance Receipt
            </td>
        </tr>
        <tr>
            <td>
                <%=s.ToString()%>
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
<script language="javascript" type="text/javascript">	
    <!--  
     window.print();
    -->
</script>
</html>
