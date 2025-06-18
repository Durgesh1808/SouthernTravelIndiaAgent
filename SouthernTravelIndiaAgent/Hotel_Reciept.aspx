<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Hotel_Reciept.aspx.cs" Inherits="SouthernTravelIndiaAgent.Hotel_Reciept" %>

<%@ Register TagPrefix="uc1" TagName="AgentHeader" Src="UserControls/UcAgentHeader.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AgentFooter" Src="UserControls/UcAgentFooter.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Southern India Travel,South India Travel Packages,Travel Packages to South India
    </title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE" />
    <meta content="Javascript" name="vs_defaultClientscript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
    <link href="../Assets/css/style.css" rel="stylesheet" type="text/css" />
    <link href="../Assets/css/stylesheet.css" type="text/css" rel="stylesheet" />

    <script language="javascript" src="../Assets/js/Script.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">	
        function framePrint(whichFrame)
        {   
            go(whichFrame);
        }
        function go(whichFrame)
        {
            var a = window. open('','','scrollbars=no');
            a.document.open("text/html");       
            var strDoc = document.getElementById(whichFrame).innerHTML;                
            a.document.write(strDoc); 
            a.document.close();
            a.print();
        }
    </script>

</head>
<body>
    <form id="Form1" method="post" runat="server">
    <table id="table1" cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table id="table2" cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td width="100%" align="center">
                            <uc1:AgentHeader ID="agentHeader" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td valign="top">
            </td>
        </tr>
    </table>
    <table id="table4" cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td valign="top" align="center">
                <table id="table3" cellspacing="0" cellpadding="6" width="800" bgcolor="#ffffff"
                    border="0">
                    <tbody class="hlinks">
                        <tr>
                            <td class="hlinks" align="center">
                                <font style="font-family: Arial; font-size: large;"><u>Thank You For Choosing Southern
                                    Hotels</u></font><br />
                                &nbsp;&nbsp;&nbsp;&nbsp;Details about your booking are mentioned below.
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <%=strticket%>
                            </td>
                        </tr>
                        <tr>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
        <tr>
            <td valign="top" align="center">
                <table id="table5" cellspacing="0" cellpadding="5" width="800" bgcolor="#ffffff"
                    border="0">
                    <tbody>
                        <tr>
                            <td align="left">
                                <div id="terms" runat="server">
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                 <uc1:AgentFooter ID="AFooter" runat="server"></uc1:AgentFooter>
            </td>
        </tr>
    </table>
    </form>

    <script type="text/javascript">
var gaJsHost = (("https:" == document.location.protocol) ? "https://ssl." : "http://www.");
document.write(unescape("%3Cscript src='" + gaJsHost + "google-analytics.com/ga.js' type='text/javascript'%3E%3C/script%3E"));
    </script>


  <script type="text/javascript">
var pageTracker = _gat._getTracker("UA-4994177-1");
pageTracker._initData();
pageTracker._trackPageview();
  </script>

</body>
</html>
