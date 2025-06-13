<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgentCarCancellation.aspx.cs" Inherits="SouthernTravelIndiaAgent.AgentCarCancellation" %>

<%@ Register TagPrefix="uc1" TagName="BranchHeader" Src="UserControls/UcAgentHeader.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AgentFooter" Src="UserControls/UcAgentFooter.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Southern India Travel,South India Travel Packages,Travel Packages to South India</title>
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <meta content="Southern India Travel - South India Travel guides offering southern india travel, south india travel packages, travel packages to south india, travel holidays package to south india, south india travel, southern india travel packages for south india, southern india travel packages, travel package for south india, south india pilgrimage travel package."
        name="Description" />
    <meta content="southern india travel, south india travel packages, travel packages to south india, travel holidays package to south india, south india travel, southern india travel packages for south india, southern india travel packages, travel package for south india, south india pilgrimage travel package, south india beaches travel packages, south india holiday travel packages, holiday travel package to south india, southern india package travel, south india tourism, tourism in south india, holidays travel in southern india, kerala backwater travel packages in india, north india tour packages, north india tours, tours to north india, tourism in north india, golden triangle tours, kathamandu tours, kashmir tour package, chennai tours, delhi tours, hyderabad tours, pilgrimage tours in india, kerala backwater tours, southern travels india, southerntravelsindia, Sirez"
        name="Keywords" />
    <meta content="index,follow" name="robots" />
    <meta content="Designed  www.Sirez.com" name="Author" />
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1" />
    <meta name="vs_defaultClientscript" content="Javascript" />
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
    <link href="../Assets/css/stylesheet.css" type="text/css" rel="stylesheet" />
    <link href="../Assets/css/style.css" rel="stylesheet" type="text/css" />
    <link href="../Assets/css/calendar.css" rel="stylesheet" type="text/css" />

    <script language="javascript" src="../Assets/js/MyScript.js" type="text/javascript"></script>

    <script language="javascript" src="../Assets/js/Script.js" type="text/javascript"></script>

    <script language="javascript" src="../Assets/js/ST_calendar.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        function framePrint(whichFrame) {
            //parent[whichFrame].focus();
            go(whichFrame);
        }
        function go(whichFrame) {
            var a = window.open('', '', 'scrollbars=yes', 'resizable=yes');
            a.document.open("text/html");
            var strDoc = document.getElementById(whichFrame).innerHTML;
            a.document.write(strDoc);
            a.document.close();
            a.print();
        }


        function validation() {
            if (document.form1.txtpNRo.value == "") {
                alert("Enter your Cab Ticket No:");
                document.form1.txtpNRo.focus();
                return false;
            }

            if (document.form1.txtemail.value == "") {
                alert("Enter your Email-ID or Mobile No");
                document.form1.txtemail.focus();
                return false;
            }
            else {
                if (isNaN(document.form1.txtemail.value) == true) {
                    if ((CheckMail(document.form1.txtemail.value)) == false) {
                        document.form1.txtemail.value = "";
                        document.form1.txtemail.focus();
                        return false;
                    }
                }
                else {
                    var a = document.form1.txtemail.value;
                    if ((a.length < 10) | (a.length > 11)) {
                        alert("Invalid Mobile No")
                        document.form1.txtemail.value = "";
                        document.form1.txtemail.focus();
                        return false;
                    }
                }
            }
            if (document.form1.txtjdate.value == "") {
                alert("Enter Journey Date:");
                document.form1.txtjdate.focus();
                return false;
            }

        }


        function CheckMail(str) {
            if (str.search(/^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/) != -1) {
                return true;
            }
            else {
                alert("Invalid E-mail ID");
                return false;
            }
        }

        function Left(str, n) {
            if (n <= 0)
                return "";
            else if (n >= String(str).length)
                return str;
            else
                return String(str).substring(0, n);
        }
        function chkterms() {
            if (document.getElementById('chkAccept').checked == false) {
                alert('Please Agree With Our Terms and Conditions Before Proceeding');
                return false;
            }
        }
    </script>

    <style type="text/css">
    #print_area (
            display: list-item; 
            list-style-image: url('Images/tcancel.gif'); 
            list-style-position: inside;
        }

    </style>
</head>
<body>
    <div id="STContainer" style="display: none; position: absolute">
    </div>
    <div id="framediv" style="z-index: 1; left: -999px; width: 148px; position: absolute;
        height: 194px;">
    </div>
    <form id="form1" runat="server">
        <table style="width: 100%" border="0" cellspacing="0" cellpadding="0">           
            <tr>
                <td>
                    <uc1:BranchHeader ID="BranchHeader" runat="server" />
                </td>
            </tr>           
            <tr>
                <td style="vertical-align: top;">
                    <table width="80%" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="left">
                                <img src="Assets/images/left_corner.gif" width="8" height="8" alt="" /></td>
                            <td style="background-color: #E7E7E7">
                                <img src="Assets/images/trans.gif" height="1" alt="" /></td>
                            <td align="right">
                                <img src="Assets/images/rgt_corner.gif" width="8" height="8" alt="" /></td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="#E7E7E7" width="8">
                            </td>
                            <td bgcolor="#E7E7E7">
                                <table id="Table2" style="border-color: #fff0ca; background-color: #ffffff" cellspacing="0"
                                    cellpadding="0" width="100%" border="0" align="center">
                                    <tr>
                                        <td width="283" align="left">
                                            <table width="250" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td width="13">
                                                        <img src="Assets/images/left_.gif" width="13" height="42" /></td>
                                                    <td align="center" background="Assets/images/bg1.gif" class="verdana14w">
                                                        Cab Cancellation</td>
                                                    <td width="13">
                                                        <img src="Assets/images/right_.gif" width="13" height="42" /></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <img src="Assets/images/trans.gif" width="1" height="1" /></td>
                                    </tr>
                                    <tr>
                                        <td style="height: 25px; background-color: #5aa1ea" align="center">
                                            <b class="cgi1">You can&nbsp;&nbsp;cancel &nbsp;your Cab&nbsp;&nbsp;by Cab Ticket no
                                                and entering all the details below &amp; clicking on&nbsp;submit button.</b>
                                        </td>
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
                                                    <td style="height: 10px" colspan="4">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="cgi1" align="right">
                                                        Cab Ticket No:&nbsp;&nbsp;</td>
                                                    <td>
                                                        <asp:TextBox CssClass="hlinks" ID="txtpNRo" MaxLength="50" runat="server" Width="168px"></asp:TextBox>
                                                    </td>
                                                    <td class="cgi1" align="right">
                                                        Email-ID / Mobile No:&nbsp;&nbsp;</td>
                                                    <td style="width: 262px">
                                                        <asp:TextBox CssClass="hlinks" ID="txtemail" runat="server" MaxLength="75" Width="160px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="cgi1" style="height: 23px" align="right">
                                                        Journey date:&nbsp;&nbsp;</td>
                                                    <td class="hlinks" style="height: 23px">
                                                        <input id="txtjdate" name="txtjdate" readonly="readonly" type="text" runat="server"
                                                            size="9" onfocus="objCal('DepartIcon','txtjdate','360','160'); " /><a title="departure date"
                                                                href="javascript:void(null)" onclick="objCal('DepartIcon','txtjdate','360','160');">
                                                                <img id="DepartIcon" tabindex="-1" src="Assets/images/calendar.gif" border="0"
                                                                    style="visibility: visible; padding-bottom: 0px;" /></a>
                                                    </td>
                                                    <td style="width: 97px; height: 23px;" colspan="2">
                                                    </td>
                                                </tr>

                                                <script language="javascript" type="text/javascript">
                                                    stObj = new SOUTHERN.caldoy.Calendar2up("stObj", "STContainer", (thisMonth + 1) + "/" + thisYear, (thisMonth + 1) + "/" + thisDay + "/" + thisYear);
                                                    stObj.title = " &nbsp;&nbsp;&nbsp;&nbsp;Select Date &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                                    stObj.setChildFunction("onSelect", setDate);
                                                    stObj.render();
                                                </script>

                                                <tr>
                                                    <td style="height: 10px" colspan="4">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 23px; background-color: #ffffff" colspan="4" align="center">
                                                        <asp:Button ID="btnsubmit" runat="server" CssClass="cgi1" Text="Submit" OnClick="btnsubmit_Click"
                                                            BackColor="#5AA1EA" BorderStyle="None"></asp:Button>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table id="tblRuleRegulation" runat="server" style="background-color: #ffffff" cellspacing="0"
                                                width="100%" cellpadding="0" border="0">
                                                <tr id="trPrint" runat="server">
                                                    <td align="right">
                                                        <input type="button" class="intdtxth" id="Button1" onclick="framePrint('print_area');"
                                                            value="Print" name="Button1" />
                                                    </td>
                                                </tr>
                                                <tr id="trTicketDetails" runat="server">
                                                    <td align="center">
                                                        <div id="print_area">
                                                            <table style="border-color: #000000;" cellspacing="0" cellpadding="1" width="100%"
                                                                border="1">
                                                                <tr>
                                                                    <td align="center">
                                                                        <img id="imglogo" runat="server" border="0" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="cgi1" style="background-color: #5aa1ea; height: 25">
                                                                        CANCELLED TOUR TICKET</td>
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
                                                                            <table cellspacing="0" cellpadding="3" width="100%" border="1" style="background-color: #DDEEFF;">
                                                                                <%} %>
                                                                                <tr>
                                                                                    <%--<td align="left" style="width: 33%;" colspan="2">
                                                                                        <span class="cgi">SPL No:</span>&nbsp;<span class="hlinks"><asp:Label ID="lblSplNo"
                                                                                            runat="server"></asp:Label></span></td>--%>
                                                                                    <td align="left" style="width: 66%;" colspan="3">
                                                                                        <span class="cgi">Ticket No:</span>&nbsp;<span class="hlinks"><asp:Label ID="lblTicketNo"
                                                                                            runat="server"></asp:Label></span></td>
                                                                                    <td align="left" style="width: 34%;" colspan="2">
                                                                                        <span class="cgi">Journey Date:</span>&nbsp;<span class="hlinks"><asp:Label ID="lblJourneyDate"
                                                                                            runat="server"></asp:Label></span></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="left" colspan="3">
                                                                                        <span class="cgi">Transfer Type:</span>&nbsp;<span class="hlinks"><asp:Label ID="lblTourName"
                                                                                            runat="server"></asp:Label></span></td>
                                                                                    <td align="left" colspan="2">
                                                                                        <span class="cgi">Vehicle:</span>&nbsp;<span class="hlinks"><asp:Label ID="lblVehicle"
                                                                                            runat="server"></asp:Label></span></td>
                                                                                    <%--<td align="left" colspan="2">
                                                                                        <span class="cgi">Category:</span>&nbsp;<span class="hlinks"><asp:Label ID="lblCategory"
                                                                                            runat="server"></asp:Label></span></td>--%>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="left" colspan="2">
                                                                                        <span class="cgi">Pickup From:</span>&nbsp;<span class="hlinks"><asp:Label ID="lblPickupFrom"
                                                                                            runat="server"></asp:Label></span></td>
                                                                                    <td align="left">
                                                                                        <%--<span class="cgi">
                                                                                           <asp:Label ID="lblPickupType" runat="server"></asp:Label>
                                                                                            No:</span>&nbsp;<span class="hlinks"><asp:Label ID="lblPickupNo" runat="server"></asp:Label></span>--%>
                                                                                        <span class="cgi">Pickup Time:</span>&nbsp;<span class="hlinks"><asp:Label ID="lblPickupTime"
                                                                                            runat="server"></asp:Label></span>
                                                                                    </td>
                                                                                    <td align="left" colspan="2">
                                                                                        <%--<span class="cgi">Pickup Time:</span>&nbsp;<span class="hlinks"><asp:Label ID="lblPickupTime"
                                                                                            runat="server"></asp:Label></span>--%>
                                                                                        <span class="cgi">Drop At:</span>&nbsp;<span class="hlinks"><asp:Label ID="lblDropAt"
                                                                                            runat="server"></asp:Label></span>
                                                                                    </td>
                                                                                </tr>
                                                                                <%-- <tr>
                                                                                    <td align="left" colspan="2">
                                                                                        <span class="cgi">Drop At:</span>&nbsp;<span class="hlinks"><asp:Label ID="lblDropAt"
                                                                                            runat="server"></asp:Label></span></td>
                                                                                    <td align="left">
                                                                                        <span class="cgi">
                                                                                            <asp:Label ID="lblDropType" runat="server"></asp:Label>
                                                                                            No:</span>&nbsp;<span class="hlinks"><asp:Label ID="lblDropNo" runat="server"></asp:Label></span></td>
                                                                                    <td align="left" colspan="2">
                                                                                        <span class="cgi">Drop Time:</span>&nbsp;<span class="hlinks"><asp:Label ID="lblDropTime"
                                                                                            runat="server"></asp:Label></span></td>
                                                                                </tr>--%>
                                                                                <tr>
                                                                                    <td align="left" style="width: 25%;">
                                                                                        <span class="cgi">Name:</span>&nbsp;</td>
                                                                                    <td align="left" style="width: 55%;" colspan="3">
                                                                                        <span class="cgi">Address:</span>&nbsp;</td>
                                                                                    <td align="left" style="width: 20%;">
                                                                                        <span class="cgi">No of Pax:</span>&nbsp;</td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="left">
                                                                                        <span class="hlinks">
                                                                                            <asp:Label ID="lblName" runat="server"></asp:Label></span></td>
                                                                                    <td align="left" colspan="3">
                                                                                        <span class="hlinks">
                                                                                            <asp:Label ID="lblAddress" runat="server"></asp:Label></span></td>
                                                                                    <td align="left">
                                                                                        <span class="hlinks">
                                                                                            <asp:Label ID="lblPax" runat="server"></asp:Label></span></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="left" colspan="2">
                                                                                        <span class="cgi">Amount Rs.:</span>&nbsp;<span class="hlinks"><asp:Label ID="lblAmount"
                                                                                            runat="server"></asp:Label></span></td>
                                                                                    <td align="left">
                                                                                        <span class="cgi">GST :</span>&nbsp;<span class="hlinks"><asp:Label ID="lblServiceTax"
                                                                                            runat="server"></asp:Label></span></td>
                                                                                    <td align="left" colspan="2">
                                                                                        <span class="cgi">Total Amount:</span>&nbsp;<span class="hlinks"><asp:Label ID="lblTotalAmount"
                                                                                            runat="server"></asp:Label></span></td>
                                                                                </tr>
                                                                                <tr id="trTicketView" runat="server" colspan="2">
                                                                                    <td align="left" colspan="2">
                                                                                        <span class="cgi">Payment Mode:</span>&nbsp;<span class="hlinks"><asp:Label ID="lblPaymentMode"
                                                                                            runat="server"></asp:Label></span></td>
                                                                                    <td align="left">
                                                                                        <span class="cgi">Branch Code:</span>&nbsp;<span class="hlinks"><asp:Label ID="lblBranchCode"
                                                                                            runat="server"></asp:Label></span></td>
                                                                                    <td align="left" colspan="2">
                                                                                        &nbsp;</td>
                                                                                </tr>
                                                                                <tr id="trCancelView" runat="server">
                                                                                    <td align="left" colspan="2">
                                                                                        <span class="cgi">Cancellation Charge:</span>&nbsp;<span class="hlinks"><asp:Label
                                                                                            ID="lblCancellationCharge" runat="server"></asp:Label></span></td>
                                                                                    <td align="left">
                                                                                        <span class="cgi">Refund Amount:</span>&nbsp;<span class="hlinks"><asp:Label ID="lblRefundAmount"
                                                                                            runat="server"></asp:Label></span></td>
                                                                                    <td align="left" id="AdvanceHide" runat="server">
                                                                                        <span class="cgi">Advance Amount:</span>&nbsp;<span class="hlinks"><asp:Label ID="lblAdvance"
                                                                                            runat="server"></asp:Label></span></td>
                                                                                    <td align="left" runat="server" id="BalanceHide">
                                                                                        <span class="cgi">Balance Amount:</span>&nbsp;<span class="hlinks"><asp:Label ID="lblBalance"
                                                                                            runat="server"></asp:Label></span></td>
                                                                                </tr>
                                                                            </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr id="trTicketTerms" runat="server">
                                                    <td>
                                                        <table class="MsoNormalTable" style="background-color: #ffffff" cellpadding="0" width="100%"
                                                            border="0">
                                                            <tr>
                                                                <td>
                                                                    <p class="cgi">
                                                                        <strong><span style="font-size: 14pt; color: #ff3300">Important Terms and Conditions:
                                                                        </span></strong>
                                                                    </p>
                                                                    <p class="cgi">
                                                                        <b>1. CANCELLATION CHARGES</b>
                                                                    </p>
                                                                    <p class="hlinks" style="margin-left: 0.3in">
                                                                        <font size="5"><b>·</b></font>&nbsp; Before 24 hours of the departure of the vehicles
                                                                        : 10%
                                                                        <br />
                                                                        <font size="5"><b>·</b></font>&nbsp; Within 24 hours of the departures of the vehicles:
                                                                        25%
                                                                    </p>
                                                                    <p class="cgi">
                                                                        <b>2. PHOTO ID</b>
                                                                    </p>
                                                                    <p class="hlinks" style="margin-left: 0.3in">
                                                                        <font size="5"><b>·</b></font>&nbsp; All passengers must carry valid photo ID proof
                                                                        without which Refund will be denied.
                                                                        <br />
                                                                        <br />
                                                                        <span class="heads" style="font-size: 12px;" />* Please check the figures / timings
                                                                        as they may change time to time without any notice to the passenger.<span />
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
                                                                    <asp:Button ID="btncantic" runat="server" Text="Cancel My Cab" OnClick="btncantic_Click"
                                                                        BackColor="#5AA1EA" BorderStyle="None" CssClass="cgi1"></asp:Button>
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
                            <td align="right" bgcolor="#E7E7E7" width="8">
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <img src="Assets/images/left_d_corner.gif" width="8" height="8" alt="" /></td>
                            <td bgcolor="#E7E7E7">
                                <img src="Assets/images/trans.gif" width="1" height="1" alt="" /></td>
                            <td align="right">
                                <img src="Assets/images/rgt_d_corner.gif" width="8" height="8" alt="" /></td>
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
    </form>
</body>
</html>

