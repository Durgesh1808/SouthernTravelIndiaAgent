<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccommodationCancellation.aspx.cs" Inherits="SouthernTravelIndiaAgent.AccommodationCancellation" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="uc1" TagName="AgentHeader" Src="UserControls/UcAgentHeader.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AgentFooter" Src="UserControls/UcAgentFooter.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Southern India Travel,South India Travel Packages,Travel Packages to South 
        India
    </title>
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <meta content="Southern India Travel - South India Travel guides offering southern india travel, south india travel packages, travel packages to south india, travel holidays package to south india, south india travel, southern india travel packages for south india, southern india travel packages, travel package for south india, south india pilgrimage travel package."
        name="Description" />
    <meta content="southern india travel, south india travel packages, travel packages to south india, travel holidays package to south india, south india travel, southern india travel packages for south india, southern india travel packages, travel package for south india, south india pilgrimage travel package, south india beaches travel packages, south india holiday travel packages, holiday travel package to south india, southern india package travel, south india tourism, tourism in south india, holidays travel in southern india, kerala backwater travel packages in india, north india tour packages, north india tours, tours to north india, tourism in north india, golden triangle tours, kathamandu tours, kashmir tour package, chennai tours, delhi tours, hyderabad tours, pilgrimage tours in india, kerala backwater tours, southern travels india, southerntravelsindia, Sirez"
        name="Keywords" />
    <meta content="index,follow" name="robots" />
    <meta content="Designed  www.Sirez.com" name="Author" />
    <meta content="MSHTML 6.00.2900.2180" name="GENERATOR" />
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1" />
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1" />
    <meta name="vs_defaultClientscript" content="Javascript" />
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
    <link href="../Assets/css/stylesheet.css" type="text/css" rel="stylesheet" />
    <link href="../Assets/css/style.css" rel="stylesheet" type="text/css" />
    <link href="../Assets/css/calendar.css" rel="stylesheet" type="text/css" />

    <script language="javascript" src="../Assets/js/ST_calendar.js" type="text/javascript"></script>

    <script language="javascript" src="../Assets/js/MyScript.js" type="text/javascript"></script>

    <script language="javascript" src="../Assets/js/Script.js" type="text/javascript"></script>

   

    <script type="text/javascript" language="javascript">
     function framePrint(whichFrame)
{
    //parent[whichFrame].focus();
    go(whichFrame);
}
function go(whichFrame)
{
    var a = window.open('','','scrollbars=yes');
    a.document.open("text/html");
    var strDoc = document.getElementById(whichFrame).innerHTML;
    a.document.write(strDoc);
    a.document.close();
    a.print();
}
    function CheckMail(str) 
{
	if (str.search(/^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/) != -1)
    {
        return true;
    }
    else
    {
        alert("Invalid E-mail ID");
        return false;
    }
}
    function validation()
{
    if(document.form1.txtpNRo.value == "")
    {
        alert("Enter your PNR No:");
        document.form1.txtpNRo.focus();
        return false;
	}
	if (document.form1.txtemail.value=="")
    {
        alert("Enter your Email-ID or Mobile No");
        document.form1.txtemail.focus();
        return false;
    }
    else
	{
		if (isNaN(document.form1.txtemail.value)==true)
		{
	        if ((CheckMail(document.form1.txtemail.value) )== false)
	        {
		        document.form1.txtemail.value="";
		        document.form1.txtemail.focus();
		        return false;
	        }
		}
		else
		{
		    var a=document.form1.txtemail.value;
		    if((a.length < 10)|(a.length > 11))
		    {
		        alert("Invalid Mobile No")
		        document.form1.txtemail.value="";
                document.form1.txtemail.focus();
                return false;
		    }
		}
	}
	
	if(document.getElementById('txtticketno').value == "")
	{
        alert("Enter your Ticket No");
        document.getElementById('txtticketno').focus();
        return false;
	}
	if(document.getElementById('txtCheckindate').value == "")
	{
        alert("Enter CheckIn Date.");
        document.getElementById('txtCheckindate').focus();
        return false;
	}
//	if(!(document.getElementById("fullcancellation").checked) && 
//	    (!(document.getElementById("PartialCancellation").checked)))
//	{
//		alert('Please select Either Full / Partial Cancellation');
//		return false;
//	}
//	else
//	{
//	}

	
}
    
       

   
    </script>

</head>
<body>
    <div id="STContainer" style="display: none; position: absolute">
    </div>
    <div id="framediv" style="z-index: 1; left: -999px; width: 148px; position: absolute;
        height: 194px;" frameborder="0" scrolling="no">
    </div>
    <form id="form1" name="form1" runat="server">
    <input type="hidden" id="tpx" runat="server" />
    <input type="hidden" id="canrowid" value="" runat="server" />
    <input type="hidden" id="canc" runat="server" />
    <input type="hidden" id="optedSeatNos" value="" runat="server" />
    <input type="hidden" id="maxSeatAllowed" runat="server" />
    <input type="hidden" id="Cancelledseats" runat="server" />
    <input type="hidden" id="lblAACfare" runat="server" />
    <input type="hidden" id="lblANACfare" runat="server" />
    <input type="hidden" id="lblCACfare" runat="server" />
    <input type="hidden" id="lblCNACfare" runat="server" />
    <input type="hidden" id="lblDACfare" runat="server" />
    <input type="hidden" id="lblDNACfare" runat="server" />
    <input type="hidden" id="lblA2ACfare" runat="server" />
    <input type="hidden" id="lblA2NACfare" runat="server" />
    <input type="hidden" id="lblA3ACfare" runat="server" />
    <input type="hidden" id="lblA3NACfare" runat="server" />
    <input type="hidden" id="lblCBACfare" runat="server" />
    <input type="hidden" id="lblCBNACfare" runat="server" />
    <input type="hidden" id="lblSACfare" runat="server" />
    <input type="hidden" id="lblSNACfare" runat="server" />
    <input type="hidden" id="trAC" value="1" runat="server" />
    <input type="hidden" id="trNONAC" value="1" runat="server" />
    <input type="hidden" id="trDAC" value="1" runat="server" />
    <input type="hidden" id="tr2AC" value="1" runat="server" />
    <input type="hidden" id="tr3AC" value="1" runat="server" />
    <input type="hidden" id="trcbAC" value="1" runat="server" />
    <input type="hidden" id="trsAC" value="1" runat="server" />
    <input type="hidden" id="RadAC" runat="server" />
    <input type="hidden" id="RadNAC" runat="server" />
    <input type="hidden" id="rpax" runat="server" />
    <input type="hidden" id="canadul" runat="server" />
    <input type="hidden" id="canchil" runat="server" />
    <input type="hidden" id="candorm" runat="server" />
    <input type="hidden" id="cantwin" runat="server" />
    <input type="hidden" id="cantrip" runat="server" />
    <input type="hidden" id="canwith" runat="server" />
    <input type="hidden" id="cansing" runat="server" />
    <input type="hidden" id="totchi" runat="server" />
    <input type="hidden" id="totadu" runat="server" />
    <div>
        <table style="width: 100%;" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    <uc1:AgentHeader ID="AgentHeader" runat="server" />
                </td>
            </tr>
        </table>
        <table style="width: 1001px;" border="0" align="center" cellspacing="0" cellpadding="0">
            <tr>
                <td width="100%" align="left">
                    <table style="width: 100%;" align="left" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="vertical-align: top; width: 1001px;">
                                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left">
                                            <img loading="lazy" src="Assets/images/left_corner.gif" width="8" height="8" alt="" />
                                        </td>
                                        <td bgcolor="#E7E7E7">
                                            <img loading="lazy" src="Assets/images/trans.gif" height="1" alt="" />
                                        </td>
                                        <td align="right">
                                            <img loading="lazy" src="Assets/images/rgt_corner.gif" width="8" height="8" alt="" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" width="8" bgcolor="#E7E7E7">
                                            &nbsp;
                                        </td>
                                        <td bgcolor="#E7E7E7">
                                            <table id="Table2" style="border-color: #fff0ca; background-color: #ffffff" cellspacing="0"
                                                cellpadding="0" width="100%" border="0" align="center">
                                                <tr>
                                                    <td width="283" align="left">
                                                        <table width="220" border="0" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td width="13">
                                                                    <img loading="lazy" src="Assets/images/left_.gif" width="13" height="42" />
                                                                </td>
                                                                <td align="center" background="Assets/images/bg1.gif" class="verdana14w">
                                                                    Accommodation Cancellation
                                                                </td>
                                                                <td width="13">
                                                                    <img loading="lazy" src="Assets/images/right_.gif" width="13" height="42" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <img loading="lazy" src="Assets/images/trans.gif" width="1" height="1" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <%--<td style="height: 25px; background-color: #5aa1ea" align="center">
                                            <b class="cgi1">You can&nbsp;&nbsp;cancel &nbsp;your tours&nbsp;&nbsp;by PNR No and
                                                entering all the details below &amp; clicking on&nbsp;submit button.</b>
                                        </td>--%>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: center;">
                                                        <asp:Label CssClass="cgi" ID="lblerrmsg" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr class="heads">
                                                    <td style="width: 100%;">
                                                        <table id="Table3" style="background-color: #9bc7f4" cellspacing="0" cellpadding="0"
                                                            width="100%" border="0">
                                                            <tr>
                                                                <td style="height: 5px" colspan="4">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="cgi1" align="left" colspan="2">
                                                                    &nbsp;&nbsp;PNR No:&nbsp;&nbsp; &nbsp;&nbsp;
                                                                    <asp:TextBox CssClass="hlinks" ID="txtpNRo" runat="server" Width="130px"></asp:TextBox>
                                                                </td>
                                                                <td class="cgi1" align="right">
                                                                    Email-ID / Mobile No:&nbsp;&nbsp;
                                                                </td>
                                                                <td style="width: 262px">
                                                                    <asp:TextBox CssClass="hlinks" ID="txtemail" runat="server" Width="160px" alt="Kindly enter the Email-ID/Mobile No given while booking."></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="cgi1" style="height: 23px" align="left">
                                                                    &nbsp;&nbsp;Ticket No:&nbsp;&nbsp;
                                                                    <asp:TextBox CssClass="hlinks" ID="txtticketno" runat="server" Width="130px"></asp:TextBox>
                                                                </td>
                                                                <td style="height: 23px" class="cgi1">
                                                                    Checkin date:&nbsp;<input id="txtCheckindate" name="txtCheckindate" type="text" runat="server"
                                                                        size="9" onfocus="objCal('DepartIcon','txtCheckindate','360','160'); " /><a title="Checkin date"
                                                                            href="javascript:void(null)" onclick="objCal('DepartIcon','txtCheckindate','360','160');"
                                                                            hidefocus=""><img loading="lazy" id="DepartIcon" tabindex="-1" src="Assets/images/calendar.gif" border="0"
                                                                                style="visibility: visible; padding-bottom: 0px;" /></a>
                                                                </td>
                                                                <td style="height: 15px" colspan="2" align="left" sty>
                                                                    <%--<asp:RadioButton ID="fullcancellation" class="cgi1" Text="Full Ticket Cancellation"
                                                                        TextAlign="right" onclick="radio();" runat="server" GroupName="can" />
                                                                    &nbsp;&nbsp;
                                                                    <asp:RadioButton ID="PartialCancellation" class="cgi1" Text="Partial Cancellation"
                                                                        TextAlign="right" onclick="radio();" runat="server" GroupName="can" />--%>
                                                                </td>
                                                            </tr>
                                                           

                                                            <script language="javascript" type="text/javascript">
                                                    stObj = new SOUTHERN.caldoy.Calendar2up("stObj","STContainer",(thisMonth+1)+"/"+thisYear,(thisMonth+1)+"/"+thisDay+"/"+thisYear);
                                                    stObj.title = " &nbsp;&nbsp;&nbsp;&nbsp;Select Date &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                                    stObj.setChildFunction("onSelect",setDate);
                                                    stObj.render();	
                                                            </script>

                                                            <tr>
                                                                <td style="height: 5px" colspan="4">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4">
                                                                    Note: Kindly enter the Email-ID/Mobile No given while booking.
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 5px; background-color: #ffffff" colspan="4">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 10px; background-color: #ffffff" colspan="4" align="center" valign="bottom">
                                                                    <asp:Button ID="btnsubmit" runat="server" CssClass="cgi1" Text="Submit" BackColor="#5AA1EA"
                                                                        BorderStyle="None" OnClick="btnsubmit_Click"></asp:Button>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table id="tblRuleRegulation" runat="server" style="background-color: #ffffff" cellspacing="0"
                                                            width="100%" cellpadding="0" border="0">
                                                            <tr id="trTicketDetails" runat="server">
                                                                <td align="center">
                                                                    <table style="border-color: #000000;" cellspacing="0" cellpadding="1" width="100%"
                                                                        border="1">
                                                                        <tr>
                                                                            <td align="center">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center" class="cgi1" style="background-color: #5aa1ea; height: 25">
                                                                                CANCELLED TICKET
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <%
                                                                                    if (tblPrintBG == "Y")
                                                                                    {
                                                                                %>
                                                                                <table cellspacing="0" cellpadding="3" width="100%" border="1" background="Images/tcancel.gif">
                                                                                    <%}
                                                                                    else
                                                                                    {%>
                                                                                    <table cellspacing="0" cellpadding="0" width="100%" border="0" style="background-color: #DDEEFF;">
                                                                                        <%} %>
                                                                                        
                                                                                        <tr>
                                                                                            <td >
                                                                                                
                                                                                                        <table id="tblChk" runat="server" cellspacing="0" cellpadding="0" width="100%" border="1" style="background-color: #DDEEFF;" >
                                                                                                            <tr>
                                                                                                                <td align="left">
                                                                                                                    <span class="cgi">PNR No:</span>&nbsp;<span class="hlinks"><asp:Label ID="lblPnr"
                                                                                                                        runat="server"></asp:Label></span>
                                                                                                                </td>
                                                                                                                <td align="left">
                                                                                                                    <span class="cgi">Ticket No:</span>&nbsp;<span class="hlinks"><asp:Label ID="lblTicketNo"
                                                                                                                        runat="server"></asp:Label></span>
                                                                                                                </td>
                                                                                                                <td align="left" colspan="2">
                                                                                                                    <span class="cgi">Checkin Date:</span>&nbsp;<span class="hlinks"><asp:Label ID="lblCheckinDate"
                                                                                                                        runat="server"></asp:Label></span><br />
                                                                                                                    <span class="cgi">Checkout Date:</span>&nbsp;<span class="hlinks"><asp:Label ID="lblCheckoutDate"
                                                                                                                        runat="server"></asp:Label></span>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td align="left">
                                                                                                                    <span class="cgi">Name:</span>&nbsp; <span class="hlinks"><span class="hlinks">
                                                                                                                        <asp:Label ID="lblName" runat="server"></asp:Label></span>
                                                                                                                </td>
                                                                                                                <td align="left" colspan="3">
                                                                                                                    <span class="cgi">Mobile No:</span>&nbsp; <span class="hlinks">
                                                                                                                        <asp:Label ID="lblMbl" runat="server"></asp:Label></span>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td align="left" colspan="4">
                                                                                                                    <span class="cgi">Address:</span>&nbsp; <span class="hlinks">
                                                                                                                        <asp:Label ID="lblAddress" runat="server"></asp:Label></span>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td align="left">
                                                                                                                    <span class="cgi">No.Of Persons:</span>&nbsp;<span class="hlinks"><asp:Label ID="lblNoPax"
                                                                                                                        runat="server"></asp:Label></span>
                                                                                                                </td>
                                                                                                                <td align="left">
                                                                                                                    <span class="cgi">No.Of Rooms:</span>&nbsp;<span class="hlinks"><asp:Label ID="lblNoRoom"
                                                                                                                        runat="server"></asp:Label></span>
                                                                                                                </td>
                                                                                                                <td align="left" colspan="2">
                                                                                                                    <span class="cgi">No.Of Days:</span>&nbsp;<span class="hlinks"><asp:Label ID="lblNoDays"
                                                                                                                        runat="server"></asp:Label></span>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td align="left">
                                                                                                                    <span class="cgi">Amount Rs.:</span>&nbsp;<span class="hlinks"><asp:Label ID="lblAmount"
                                                                                                                        runat="server"></asp:Label></span>
                                                                                                                </td>
                                                                                                                <td align="left">
                                                                                                                    <span class="cgi">GST (With LT
                                                                                                                        <asp:Label ID="lblLT" runat="server"></asp:Label>
                                                                                                                        and ST
                                                                                                                        <asp:Label ID="lblST" runat="server"></asp:Label>) :</span>&nbsp;<span class="hlinks"><asp:Label
                                                                                                                            ID="lblServiceTax" runat="server"></asp:Label></span>
                                                                                                                </td>
                                                                                                                <td align="left" colspan="2">
                                                                                                                    <span class="cgi">Total Amount:</span>&nbsp;<span class="hlinks"><asp:Label ID="lblTotalAmount"
                                                                                                                        runat="server"></asp:Label></span>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr id="trCancelView" runat="server">
                                                                                                                <td align="left">
                                                                                                                    <span class="cgi">Cancellation Charge:</span>&nbsp;<span class="hlinks"><asp:Label
                                                                                                                        ID="lblCancellationCharge" runat="server"></asp:Label></span>
                                                                                                                </td>
                                                                                                                <td align="left">
                                                                                                                    <span class="cgi">Refund Amount:</span>&nbsp;<span class="hlinks"><asp:Label ID="lblRefundAmount"
                                                                                                                        runat="server"></asp:Label></span>
                                                                                                                </td>
                                                                                                                <td align="left" id="AdvanceHide" runat="server">
                                                                                                                    <span class="cgi">Advance Amount:</span>&nbsp;<span class="hlinks"><asp:Label ID="lblAdvance"
                                                                                                                        runat="server"></asp:Label></span>
                                                                                                                </td>
                                                                                                                <td align="left" runat="server" id="BalanceHide">
                                                                                                                    <span class="cgi">Balance Due:</span>&nbsp;<span class="hlinks"><asp:Label ID="lblBalance"
                                                                                                                        runat="server"></asp:Label></span>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr id="trTicketView" runat="server" colspan="2">
                                                                                                                <td align="left">
                                                                                                                    <span class="cgi">Payment Mode:</span>&nbsp;<span class="hlinks"><asp:Label ID="lblPaymentMode"
                                                                                                                        runat="server"></asp:Label></span>
                                                                                                                </td>
                                                                                                                <td align="left">
                                                                                                                    <span class="cgi">Branch Code:</span>&nbsp;<span class="hlinks"><asp:Label ID="lblBranchCode"
                                                                                                                        runat="server"></asp:Label></span>
                                                                                                                </td>
                                                                                                                <td align="left" colspan="2">
                                                                                                                    &nbsp;
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            </table>
                                                                                                       
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td >
                                                                                                <table id="tblCan" runat="server" width="100%" cellspacing="0" cellpadding="0">
                                                                                                    <tr>
                                                                                                        <td align="center">
                                                                                                            <asp:Label ID="lblticket" runat="server"></asp:Label>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr id="trTicketTerms" runat="server">
                                                                <td>
                                                                    <table class="MsoNormalTable" style="background-color: #ffffff" cellpadding="0" width="100%"
                                                                        border="0">
                                                                        <tr>
                                                                            <td>
                                                                                <p class="cgi">
                                                                                    <strong><span style="font-size: 14pt; color: #ff3300">Important Terms and 
                                                                                    Conditions:
                                                                                    </span></strong>
                                                                                </p>
                                                                                <p class="cgi">
                                                                                    <b>1. CANCELLATION CHARGES</b>
                                                                                </p>
                                                                                <p class="hlinks" style="margin-left: 0.3in">
                                                                                    <font size="5"><b>·</b></font>&nbsp; Before 48 Hrs of the Check-in date – 10%
                                                                                    <br />
                                                                                    <font size="5"><b>·</b></font>&nbsp; Before 24 Hrs of the Check-in date – One 
                                                                                    night charges will be deducted.
                                                                                    <br />
                                                                                    <font size="5"><b>·</b></font>&nbsp; Within 24 Hrs of the Check-in date – NO 
                                                                                    REFUND.
                                                                                </p>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="background-color: #5aa1ea">
                                                                                <asp:CheckBox ID="chkAccept" runat="server" class="cgi1" Text="I confirm that i have read &amp; accept the terms and conditions.">
                                                                                </asp:CheckBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="height: 5">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 819px" align="center">
                                                                                <asp:Button ID="btncantic" runat="server" Text="Cancel Ticket" BackColor="#5AA1EA"
                                                                                    BorderStyle="None" CssClass="cgi1" OnClick="btncantic_Click"></asp:Button>
                                                                                <asp:Button ID="btnPartialTicket" runat="server" Text="Partial Cancellation" BackColor="#5AA1EA"
                                                                                    BorderStyle="None" CssClass="cgi1"></asp:Button>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 819px" height="5">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 819px" align="center">
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="right" width="8" bgcolor="#E7E7E7">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <img loading="lazy" src="Assets/images/left_d_corner.gif" width="8" height="8" alt="" />
                                        </td>
                                        <td bgcolor="#E7E7E7">
                                            <img loading="lazy" src="Assets/images/trans.gif" width="1" height="1" alt="" />
                                        </td>
                                        <td align="right">
                                            <img loading="lazy" src="Assets/images/rgt_d_corner.gif" width="8" height="8" alt="" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td valign="bottom">
                                <uc1:AgentFooter ID="AgentFooter" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>

