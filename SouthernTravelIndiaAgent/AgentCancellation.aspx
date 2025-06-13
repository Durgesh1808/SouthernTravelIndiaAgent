<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgentCancellation.aspx.cs" Inherits="SouthernTravelIndiaAgent.AgentCancellation" %>

<%@ Register TagPrefix="uc1" TagName="AgentHeader" Src="UserControls/UcAgentHeader.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AgentFooter" Src="UserControls/UcAgentFooter.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Southern India Travel,South India Travel Packages,Travel Packages to South India
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

    <script language="javascript" src="Assets/js/agentcancellation.js" type="text/javascript"></script>
     <link href="Assets/css/demos.css" rel="stylesheet
    " type="text/css" />
    <link type="text/css" href="../Assets/css/jquery-ui-1.7.1.custom_blue.css" rel="stylesheet" />

    <script src="../Assets/js/jquery-1.3.2.min.js" type="text/javascript"></script>

    <script src="../Assets/js/jquery-ui-1.7.1.custom.min.js" type="text/javascript"></script>
<link href="../Assets/css/style.css" rel="stylesheet">
    <style>
        .DatePickerImage
        {
            position: relative;
            padding-left: 5px;
        }
     </style>
          
<script>
    $(function() {
        $("#txtjdate").datepicker({

            numberOfMonths: 1,
            showOn: "button", autoSize: true,
            buttonImage: "../Assets/images/cal.gif",
            buttonImageOnly: true,
            minDate: new Date(),
            closeText: 'Close',
            dateFormat: 'dd/mm/yy', buttonText: 'Choose a Date'



        });
        $('img.ui-datepicker-trigger').css({ 'cursor': 'pointer', "vertical-align": 'top' });
        $('img.ui-datepicker-trigger').addClass('DatePickerImage');
    });
        var currentOptedSeats = 0;
        function checkUnheckBook(ctrlId, divSelected) {
            var chkId = document.getElementById(ctrlId);
            if (chkId.checked) {
                chkId.checked = false;
            }
            else {
                chkId.checked = true;
            }

            alterValueBook(chkId, divSelected);
        }

        function alterValueBook(chkBoxObject, divSelected) {
            
            if (chkBoxObject.checked) {

                if (document.getElementById('canc').value == "0"
                || document.getElementById('canc').value == "") {
                    alert('Please Select the Person to whom you want to Cancel.');
                    chkBoxObject.checked = false;
                    return;
                }
                if (currentOptedSeats >= parseInt(document.getElementById('canc').value)) {
                    alert('You have selected more than ' + parseInt(document.getElementById('canc').value) + ' seat(s)');
                    chkBoxObject.checked = false;
                    return;
                }

                addValuesBook(chkBoxObject.value);
                divSelected.className = "TB_avbl";
            }
            else {
                removeValuesBook(chkBoxObject.value);
                divSelected.className = "TB_selctd";
            }
        }
        function addValuesBook(chkValue) {
            currentOptedSeats = currentOptedSeats + 1;
            document.getElementById('maxSeatCancel').value = currentOptedSeats;
            var optedStr = document.getElementById('optedSeatNosBook').value;
            optedStr = optedStr.replace(",,", ",");
            document.getElementById('optedSeatNosBook').value = optedStr + ',' + chkValue;

        }

        function removeValuesBook(chkValue) {
            currentOptedSeats = currentOptedSeats - 1;
            document.getElementById('maxSeatCancel').value = currentOptedSeats;
            var optedStr = document.getElementById('optedSeatNosBook').value;
            optedStr = optedStr.replace(chkValue, "");
            optedStr = optedStr.replace(",,", ",");
            document.getElementById('optedSeatNosBook').value = optedStr;
        }

        function checkseats() {
            var chek = true;
            if (currentOptedSeats == 0) {
                alert('Please Select The Correct Number Of seats for cancel');
                return false;
                chek = false;
            }
            if (document.getElementById('maxSeatCancel').value == "" || document.getElementById('maxSeatCancel').value == "0") {
                alert('Please Select The Correct Number Of seats for cancel');
                return false;
                chek = false;
            }
            if ((currentOptedSeats) != parseInt(document.getElementById('maxSeatCancel').value)) {
                alert('Please Select The Correct Number Of seats for cancel');
                return false;
                chek = false;
            }
            if (parseInt(document.getElementById('canc').value) != parseInt(document.getElementById('maxSeatCancel').value)) {
                alert('Please Select The Correct Number Of seats for cancel');
                return false;
                chek = false;
            }
        }
        
</script>
</head>
<body>
    <div id="STContainer" style="display: none; position: absolute">
    </div>
    <div id="framediv" style="z-index: 1; left: -999px; width: 148px; position: absolute;
        height: 194px;" frameborder="0" scrolling="no">
    </div>
    <form id="form1" runat="server">
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
    <input type="hidden" id="lblAWFACfare" runat="server" />
    <input type="hidden" id="lblAWFNACfare" runat="server" />
    <input type="hidden" id="lblCWFACfare" runat="server" />
    <input type="hidden" id="lblCWFNACfare" runat="server" />
    <input type="hidden" id="canaduF" runat="server" />
    <input type="hidden" id="canchiF" runat="server" />
    
    <input type="hidden" id="hdDiscount" runat="server" value="0" />
    <input type="hidden" id="optedSeatNosBook" value="" runat="server" />
    <input type="hidden" id="maxSeatCancel" runat="server" />
    
    <table style="width: 100%" border="0" cellspacing="0" cellpadding="0">
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
                                        <img src="Assets/images/left_corner.gif" width="8" height="8" alt="" />
                                    </td>
                                    <td bgcolor="#E7E7E7">
                                        <img src="Assets/images/trans.gif" height="1" alt="" />
                                    </td>
                                    <td align="right">
                                        <img src="Assets/images/rgt_corner.gif" width="8" height="8" alt="" />
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#E7E7E7">
                                        <table id="Table2" style="border-color: #fff0ca; background-color: #ffffff" cellspacing="0"
                                            cellpadding="0" width="100%" border="0" align="center">
                                            <tr>
                                                <td width="283" align="left">
                                                    <table width="220" border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td width="13">
                                                                <img src="Assets/images/left_.gif" width="13" height="42" />
                                                            </td>
                                                            <td align="center" background="Assets/images/bg1.gif" class="verdana14w">
                                                                Fixed Tour Cancellation
                                                            </td>
                                                            <td width="13">
                                                                <img src="Assets/images/right_.gif" width="13" height="42" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <img src="Assets/images/trans.gif" width="1" height="1" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 25px; background-color: #5aa1ea" align="center">
                                                    <b class="cgi1">You can&nbsp;&nbsp;cancel &nbsp;your tours&nbsp;&nbsp;by PNR No and
                                                        entering all the details below &amp; clicking on&nbsp;submit button.</b>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center;">
                                                    <strong>
                                                        <asp:Label CssClass="cgi" ID="lblerrmsg" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>
                                                    </strong>
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
                                                                &nbsp;&nbsp;PNR No:&nbsp;&nbsp; &nbsp; &nbsp;<asp:TextBox CssClass="hlinks" ID="txtpNRo"
                                                                    runat="server" Width="130px"></asp:TextBox>
                                                            </td>
                                                            <td class="cgi1" align="right">
                                                                Email-ID / Mobile No:&nbsp;&nbsp;
                                                            </td>
                                                            <td style="width: 262px">
                                                                <asp:TextBox CssClass="hlinks" ID="txtemail" runat="server" Width="160px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="cgi1" style="height: 23px" align="left">
                                                                &nbsp;&nbsp;Ticket No:&nbsp;&nbsp;
                                                                <asp:TextBox CssClass="hlinks" ID="txtticketno" runat="server" Width="130px"></asp:TextBox>
                                                            </td>
                                                            <td class="cgi1" style="height: 23px">
                                                                Journey date:&nbsp;&nbsp;<input id="txtjdate" name="txtjdate" type="text" runat="server"
                                                                    size="9" /><%--<a title="departure date"
                                                                        href="javascript:void(null)" onclick="objCal('DepartIcon','txtjdate','360','160');"
                                                                        hidefocus=""><img id="DepartIcon" tabindex="-1" src="Assets/images/calendar.gif" border="0"
                                                                            style="visibility: visible; padding-bottom: 0px;" /></a>--%>
                                                            </td>
                                                            <td style="height: 23px;" colspan="2">
                                                                <asp:RadioButton ID="fullcancellation" class="cgi1" Text="Full Ticket Cancellation"
                                                                    TextAlign="right" onclick="radio();" runat="server" GroupName="can" />
                                                                &nbsp; &nbsp;
                                                                <asp:RadioButton ID="PartialCancellation" class="cgi1" Text="Partial Cancellation"
                                                                    TextAlign="right" onclick="radio();" runat="server" GroupName="can" />
                                                            </td>
                                                        </tr>

                                                       <%-- <script language="javascript" type="text/javascript">
                                                            stObj = new SOUTHERN.caldoy.Calendar2up("stObj", "STContainer", (thisMonth + 1) + "/" + thisYear, (thisMonth + 1) + "/" + thisDay + "/" + thisYear);
                                                            stObj.title = " &nbsp;&nbsp;&nbsp;&nbsp;Select Date &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                                            stObj.setChildFunction("onSelect", setDate);
                                                            stObj.render();	
                                                        </script>--%>

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
                                                        <tr>
                                                            <td style="background-color: #9bc7f4">
                                                                <asp:Label ID="lblticket" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr id="listpax" runat="server">
                                                            <td style="background-color: #ffffff" align="center">
                                                                <table style="border-color: #000000; background-color: #ffffff" cellspacing="0" cellpadding="1"
                                                                    width="550" border="1">
                                                                    <tr>
                                                                        <td>
                                                                            <%=stbuild.ToString()%>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr id="Remainpax" runat="server">
                                                            <td style="background-color: #ffffff" align="center">
                                                                <table style="border-color: #000000; background-color: #ffffff" cellspacing="0" cellpadding="1"
                                                                    width="550" border="1">
                                                                    <tr>
                                                                        <td>
                                                                            <table style="border-color: #000000; background-color: #ffffff" cellspacing="0" cellpadding="1"
                                                                                width="100%" border="0">
                                                                                <tr>
                                                                                    <td width="134" align="center" class="cgi1" style="background-color: #5aa1ea">
                                                                                    </td>
                                                                                    <td width="161" align="center" class="cgi1" style="background-color: #5aa1ea">
                                                                                        No. of Persons
                                                                                    </td>
                                                                                    <td width="69" align="center" class="cgi1" style="background-color: #5aa1ea">
                                                                                        Fare/Person
                                                                                    </td>
                                                                                    <td width="7" style="background-color: #5aa1ea">
                                                                                    </td>
                                                                                    <td width="97" align="center" class="cgi1" style="background-color: #5aa1ea">
                                                                                        Calculated Amount
                                                                                    </td>
                                                                                </tr>
                                                                                <tr id="traf" runat="server" style="border-color: #cccccc">
                                                                                    <td class="hlinks" align="left" style="border-color: #ffffff; height: 25px;">
                                                                                        Adults
                                                                                    </td>
                                                                                    <td class="hlinks" id="tx" style="border-color: #ffffff; height: 25px;">
                                                                                        &nbsp;
                                                                                        <asp:TextBox ID="txtNoOfAdults" runat="server" CssClass="hlinks" onblur="Displayfare();"
                                                                                            MaxLength="2" Columns="2" EnableViewState="False" onfocus="javascript: if(this.value == '0'){ this.value = ''; }">0</asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;X
                                                                                    </td>
                                                                                    <td class="hlinks" id="td8" align="center" style="border-color: #ffffff; height: 25px;">
                                                                                        <div class="hlinks" id="divAdultAmt" runat="server" ms_positioning="FlowLayout">
                                                                                            <asp:Label ID="lblFareAdults" runat="server" CssClass="hlinks">0</asp:Label>.00</div>
                                                                                    </td>
                                                                                    <td class="hlinks" align="right" style="border-color: #ffffff; height: 25px;">
                                                                                        =
                                                                                    </td>
                                                                                    <td class="hlinks" id="td10" align="right" style="border-color: #ffffff; height: 25px;">
                                                                                        <div class="hlinks" id="divCalcAdult" runat="server" ms_positioning="FlowLayout">
                                                                                            <asp:Label ID="lblCalcAdults" runat="server" CssClass="hlinks">0</asp:Label>.00</div>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr id="tr7" runat="server">
                                                                                    <td colspan="5" bgcolor="#DCDCDC" height="1">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr id="trcf" runat="server" style="border-color: #cccccc">
                                                                                    <td class="hlinks" style="height: 25px; border-color: #ffffff" align="left">
                                                                                        Children
                                                                                    </td>
                                                                                    <td class="hlinks" style="height: 25px; border-color: #ffffff">
                                                                                        &nbsp;
                                                                                        <asp:TextBox ID="txtNoOfChilds" runat="server" CssClass="hlinks" onblur="Displayfare();"
                                                                                            MaxLength="2" Columns="2" EnableViewState="False" onfocus="javascript: if(this.value == '0'){ this.value = ''; }">0</asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;X
                                                                                    </td>
                                                                                    <td class="hlinks" id="td9" style="height: 25px; border-color: #ffffff" align="center">
                                                                                        <div class="hlinks" id="divChildAmt" runat="server" ms_positioning="FlowLayout">
                                                                                            <asp:Label ID="lblfareChild" runat="server" CssClass="hlinks">0</asp:Label>.00</div>
                                                                                    </td>
                                                                                    <td class="hlinks" style="height: 25px; border-color: #ffffff" align="right">
                                                                                        =
                                                                                    </td>
                                                                                    <td class="hlinks" id="td11" style="height: 25px; border-color: #ffffff" align="right">
                                                                                        <div class="hlinks" id="divCalcChild" runat="server" ms_positioning="FlowLayout">
                                                                                            <asp:Label ID="lblCalcChild" runat="server" CssClass="hlinks">0</asp:Label>.00</div>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr id="trAWFColor" runat="server">
                                                                                    <td colspan="7" bgcolor="#DCDCDC" height="1">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr id="trAWF" runat="server" style="border-color: #cccccc">
                                                                                    <td class="hlinks" style="height: 25px; border-color: #ffffff" align="left">
                                                                                        Adult With South Veg Food
                                                                                    </td>
                                                                                    <td class="hlinks" style="height: 25px; border-color: #ffffff">
                                                                                        &nbsp;
                                                                                        <asp:TextBox ID="txtNoOfAdultWF" runat="server" CssClass="hlinks" onblur="Displayfare();"
                                                                                            MaxLength="2" Columns="2" EnableViewState="False" onfocus="javascript: if(this.value == '0'){ this.value = ''; }">0</asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;X
                                                                                    </td>
                                                                                    <td class="hlinks" id="tdAWF4" style="height: 25px; border-color: #ffffff" align="center">
                                                                                        <div class="hlinks" id="divAdultWFAmt" runat="server" ms_positioning="FlowLayout">
                                                                                            <asp:Label ID="lblAdulWFoodfare" runat="server" CssClass="hlinks">0</asp:Label>.00</div>
                                                                                    </td>
                                                                                    <td class="hlinks" style="height: 25px; border-color: #ffffff" align="right">
                                                                                        =
                                                                                    </td>
                                                                                    <td class="hlinks" id="tdAWF11" style="height: 25px; border-color: #ffffff" align="right">
                                                                                        <div class="hlinks" id="divCalcAdWfood" runat="server" ms_positioning="FlowLayout">
                                                                                            <asp:Label ID="lblCalcAWfood" runat="server" CssClass="hlinks">0</asp:Label>.00</div>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr id="trCWFColor" runat="server">
                                                                                    <td colspan="7" bgcolor="#DCDCDC" height="1">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr id="trCWF" runat="server" style="border-color: #cccccc">
                                                                                    <td class="hlinks" style="height: 25px; border-color: #ffffff" align="left">
                                                                                        Child With South Veg Food
                                                                                    </td>
                                                                                    <td class="hlinks" style="height: 25px; border-color: #ffffff">
                                                                                        &nbsp;
                                                                                        <asp:TextBox ID="txtNoOfChildWF" runat="server" CssClass="hlinks" onblur="Displayfare();"
                                                                                            MaxLength="2" Columns="2" EnableViewState="False" onfocus="javascript: if(this.value == '0'){ this.value = ''; }">0</asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;X
                                                                                    </td>
                                                                                    <td class="hlinks" id="tdCWF9" style="height: 25px; border-color: #ffffff" align="center">
                                                                                        <div class="hlinks" id="divChildWFAmt" runat="server" ms_positioning="FlowLayout">
                                                                                            <asp:Label ID="lblChildWFoodfare" runat="server" CssClass="hlinks">0</asp:Label>.00</div>
                                                                                    </td>
                                                                                    <td class="hlinks" style="height: 25px; border-color: #ffffff" align="right">
                                                                                        =
                                                                                    </td>
                                                                                    <td class="hlinks" id="tdCWF11" style="height: 25px; border-color: #ffffff" align="right">
                                                                                        <div class="hlinks" id="divCalcCdWfood" runat="server" ms_positioning="FlowLayout">
                                                                                            <asp:Label ID="lblCalcCWfood" runat="server" CssClass="hlinks">0</asp:Label>.00</div>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr id="tr8" runat="server">
                                                                                    <td colspan="5" bgcolor="#DCDCDC" height="1">
                                                                                    </td>
                                                                                </tr>
                                                                                <!-- fro Dormitory Accommodation Start-->
                                                                                <tr id="tradf" runat="server" style="border-color: #cccccc">
                                                                                    <td align="left" valign="top" class="hlinks">
                                                                                        Dormitory Accommodation:
                                                                                    </td>
                                                                                    <td class="hlinks" id="txd" style="border-color: #ffffff">
                                                                                        &nbsp;
                                                                                        <asp:TextBox ID="txtNoOfDormitory" runat="server" CssClass="hlinks" onblur="Displayfare();"
                                                                                            MaxLength="2" Columns="2" EnableViewState="False" onfocus="javascript: if(this.value == '0'){ this.value = ''; }">0</asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;X
                                                                                    </td>
                                                                                    <td class="hlinks" id="td8dorm" align="center" style="border-color: #ffffff">
                                                                                        <div class="hlinks" id="divAdultAmtdorm" runat="server" ms_positioning="FlowLayout">
                                                                                            <asp:Label ID="lblFareAdultsDorm" runat="server" CssClass="hlinks">0</asp:Label>.00</div>
                                                                                    </td>
                                                                                    <td class="hlinks" align="right" style="border-color: #ffffff">
                                                                                        =
                                                                                    </td>
                                                                                    <td class="hlinks" id="td10dorm" align="right" style="border-color: #ffffff">
                                                                                        <div class="hlinks" id="divCalcAdultDorm" runat="server" ms_positioning="FlowLayout">
                                                                                            <asp:Label ID="lblCalcAdultsDorm" runat="server" CssClass="hlinks">0</asp:Label>.00</div>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr id="tr2" runat="server">
                                                                                    <td colspan="5" bgcolor="#DCDCDC" height="1">
                                                                                    </td>
                                                                                </tr>
                                                                                <!-- fro Dormitory Accommodation End-->
                                                                                <tr id="tra2f" runat="server" style="border-color: #cccccc">
                                                                                    <td align="left" valign="top" class="hlinks">
                                                                                        Adult on Twin Sharing:
                                                                                    </td>
                                                                                    <td class="hlinks" id="tx2" style="border-color: #ffffff">
                                                                                        &nbsp;
                                                                                        <asp:TextBox ID="txtNoOfAdultsTwin" runat="server" CssClass="hlinks" onblur="validtwin()"
                                                                                            MaxLength="2" Columns="2" EnableViewState="False" onfocus="javascript: if(this.value == '0'){ this.value = ''; }">0</asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;X
                                                                                    </td>
                                                                                    <td class="hlinks" id="td8twin" align="center" style="border-color: #ffffff">
                                                                                        <div class="hlinks" id="divAdultAmttwin" runat="server" ms_positioning="FlowLayout">
                                                                                            <asp:Label ID="lblFareAdultsTwin" runat="server" CssClass="hlinks">0</asp:Label>.00</div>
                                                                                    </td>
                                                                                    <td class="hlinks" align="right" style="border-color: #ffffff">
                                                                                        =
                                                                                    </td>
                                                                                    <td class="hlinks" id="td10twin" align="right" style="border-color: #ffffff">
                                                                                        <div class="hlinks" id="divCalcAdultTwin" runat="server" ms_positioning="FlowLayout">
                                                                                            <asp:Label ID="lblCalcAdultsTwin" runat="server" CssClass="hlinks">0</asp:Label>.00</div>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr id="tr9" runat="server">
                                                                                    <td colspan="5" bgcolor="#DCDCDC" height="1">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr id="tra3f" runat="server" style="border-color: #cccccc">
                                                                                    <td align="left" valign="top" style="border-color: #ffffff" class="hlinks">
                                                                                        Adult on Triple Sharing:
                                                                                    </td>
                                                                                    <td class="hlinks" id="tx3" style="border-color: #ffffff">
                                                                                        &nbsp;
                                                                                        <asp:TextBox ID="txtNoOfAdultsTriple" runat="server" CssClass="hlinks" onblur="validtriple()"
                                                                                            MaxLength="2" Columns="2" EnableViewState="False" onfocus="javascript: if(this.value == '0'){ this.value = ''; }">0</asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;X
                                                                                    </td>
                                                                                    <td class="hlinks" id="td8triple" align="center" style="border-color: #ffffff">
                                                                                        <div class="hlinks" id="divAdultAmttriple" runat="server" ms_positioning="FlowLayout">
                                                                                            <asp:Label ID="lblFareAdultsTriple" runat="server" CssClass="hlinks">0</asp:Label>.00</div>
                                                                                    </td>
                                                                                    <td class="hlinks" align="right" style="border-color: #ffffff">
                                                                                        =
                                                                                    </td>
                                                                                    <td class="hlinks" id="td10triple" align="right" style="border-color: #ffffff">
                                                                                        <div class="hlinks" id="divCalcAdultTriple" runat="server" ms_positioning="FlowLayout">
                                                                                            <asp:Label ID="lblCalcAdultsTriple" runat="server" CssClass="hlinks">0</asp:Label>.00</div>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr id="tr10" runat="server">
                                                                                    <td colspan="5" bgcolor="#DCDCDC" height="1">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr id="trcbf" runat="server" style="border-color: #cccccc">
                                                                                    <td align="left" valign="top" class="hlinks">
                                                                                        Child(5-11)Without Bed:
                                                                                    </td>
                                                                                    <td class="hlinks" id="txcb">
                                                                                        &nbsp;
                                                                                        <asp:TextBox ID="txtNoOfChildBed" runat="server" CssClass="hlinks" onblur="Displayfare();"
                                                                                            MaxLength="2" Columns="2" EnableViewState="False" onfocus="javascript: if(this.value == '0'){ this.value = ''; }">0</asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;X
                                                                                    </td>
                                                                                    <td class="hlinks" id="td8childbed" align="center">
                                                                                        <div class="hlinks" id="divChildBedAmt" runat="server" ms_positioning="FlowLayout">
                                                                                            <asp:Label ID="lblFareChildBed" runat="server" CssClass="hlinks">0</asp:Label>.00</div>
                                                                                    </td>
                                                                                    <td class="hlinks" align="right">
                                                                                        =
                                                                                    </td>
                                                                                    <td class="hlinks" id="td10childbed" align="right">
                                                                                        <div class="hlinks" id="divCalcChildBed" runat="server" ms_positioning="FlowLayout">
                                                                                            <asp:Label ID="lblCalcChildBed" runat="server" CssClass="hlinks">0</asp:Label>.00</div>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr id="tr11" runat="server">
                                                                                    <td colspan="5" bgcolor="#DCDCDC" height="1">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr id="trsf" runat="server" style="border-color: #cccccc">
                                                                                    <td align="left" valign="top" class="hlinks">
                                                                                        Single Adult In a Room:
                                                                                    </td>
                                                                                    <td class="hlinks" id="txs">
                                                                                        &nbsp;
                                                                                        <asp:TextBox ID="txtNoOfSingles" runat="server" CssClass="hlinks" onblur="Displayfare();"
                                                                                            MaxLength="2" Columns="2" EnableViewState="False" onfocus="javascript: if(this.value == '0'){ this.value = ''; }">0</asp:TextBox>
                                                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;X
                                                                                    </td>
                                                                                    <td class="hlinks" id="td8single" align="center">
                                                                                        <div class="hlinks" id="divSingleAmt" runat="server" ms_positioning="FlowLayout">
                                                                                            <asp:Label ID="lblFareSingles" runat="server" CssClass="hlinks">0</asp:Label>.00</div>
                                                                                    </td>
                                                                                    <td class="hlinks" align="right">
                                                                                        =
                                                                                    </td>
                                                                                    <td class="hlinks" id="td10single" align="right">
                                                                                        <div class="hlinks" id="divCalcSingle" runat="server" ms_positioning="FlowLayout">
                                                                                            <asp:Label ID="lblCalcSingles" runat="server" CssClass="hlinks">0</asp:Label>.00</div>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr style="border-color: #cccccc">
                                                                                    <td class="hlinks" align="right" style="background-color: #9bc7f4" colspan="4">
                                                                                        Total:
                                                                                    </td>
                                                                                    <td class="hlinks" align="right" style="background-color: #9bc7f4" height="5">
                                                                                        <div id="divTotal" runat="server" ms_positioning="FlowLayout">
                                                                                            <asp:Label ID="lblTotal" runat="server" CssClass="hlinks">0.00</asp:Label>.00</div>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td height="10">
                                                                                    </td>
                                                                                    <td height="10">
                                                                                    </td>
                                                                                    <td height="10">
                                                                                    </td>
                                                                                    <td height="10">
                                                                                    </td>
                                                                                    <td height="10">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="left" style="background-color: #9bc7f4" colspan="5" height="5">
                                                                                        <asp:Label ID="Lblchild" runat="server" CssClass="heads" Font-Bold="true" Text="Child: Upto 11 years of age can be treated as child."></asp:Label>&nbsp;
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr id="trSeat" runat="server">
                                                                <td>
                                                                    <asp:Panel ID="Panel3" runat="server" Font-Size="20px" Width="98%">
                                                                        <div class="seatselectwrap" id="Div1" runat="server">
                                                                            <h3 class="title">
                                                                                Seat <span>Selection For Cancel</span>
                                                                                <ul class="seatlist">
                                                                                    <li class="booked">Booked</li>
                                                                                    <li class="selected">Selected</li>
                                                                                    <li class="avail">Available</li>
                                                                                </ul>
                                                                                <%if (BoookChart != null)
                                                                                  {%>
                                                                                <%=BoookChart.ToString()%>
                                                                                <%} %>
                                                                            </h3>
                                                                        </div>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                        <tr>
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
                                                                                <b>1. PHOTO ID</b>
                                                                            </p>
                                                                            <p class="hlinks">
                                                                                <font size="5"><b>·</b></font>&nbsp; All passengers must carry valid photo ID proof
                                                                                without which Refund will be denied.
                                                                                <br />
                                                                                <br />
                                                                                <span class="heads" style="font-size: 12pt; font-family: Times New Roman" />* please
                                                                                check the figures / timings as they may change time to time without any notice to
                                                                                the passenger.<span />
                                                                            </p>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="background-color: #5aa1ea">
                                                                <asp:CheckBox ID="chkAccept" runat="server" class="cgi1" Text="I confirm that i have read &amp; accept the terms and conditions.">
                                                                </asp:CheckBox>
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
                                                    <asp:Button ID="btncantic" runat="server" Text="Cancel My Ticket" OnClick="btncantic_Click"
                                                        BackColor="#5AA1EA" BorderStyle="None" CssClass="cgi1"></asp:Button>
                                                    <asp:Button ID="btnPartialTicket" runat="server" Text="Partial Cancellation" BackColor="#5AA1EA"
                                                        BorderStyle="None" CssClass="cgi1" OnClientClick="Displayfare();" OnClick="btnPartialTicket_Click">
                                                    </asp:Button>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <img src="Assets/images/left_d_corner.gif" width="8" height="8" alt="" />
                                    </td>
                                    <td bgcolor="#E7E7E7">
                                        <img src="Assets/images/trans.gif" width="1" height="1" alt="" />
                                    </td>
                                    <td align="right">
                                        <img src="Assets/images/rgt_d_corner.gif" width="8" height="8" alt="" />
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
    </form>
</body>
</html>
