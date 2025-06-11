<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgentTourBooking.aspx.cs" Inherits="SouthernTravelIndiaAgent.AgentTourBooking" %>


<%@ Register TagPrefix="uc1" TagName="AgentHeader" Src="UserControls/UcAgentHeader.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AgentFooter" Src="UserControls/UcAgentFooter.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 transitional//EN" "http://www.w3.org/tr/xhtml1/Dtd/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" id="AgentID">
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
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 8" />
    <meta name="CODE_LANGUAGE" content="c#" />
    <meta name="vs_defaultClientscript" content="Javascript" />
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
    <link href="../Assets/css/style.css" rel="stylesheet" type="text/css" />
    <link href="../Assets/css/stylesheet.css" type="text/css" rel="stylesheet" />
    <link href="/Assets/css/rupee.css" rel="stylesheet" type="text/css" />
    <link href="../Assets/css/style.css" rel="stylesheet">
    <style>
        .rupee
        {
            font-family: 'RupeeForadian';
        }
    </style>

    <script language="javascript" src="../Assets/js/MyScript.js" type="text/javascript"></script>

    <script language="javascript" src="Assets/js/agenttourbooking.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../Assets/js/query_string.js"></script>

    <script language="javascript" type="text/javascript">
     function fillTransfer(MasterId) {
        xmlHttp = GetXmlHttpObject()
        var url = '../getaddress.aspx?RowId=';
        url = url + MasterId;
        url = url + "&sid=" + Math.random();
        // alert(url);
        xmlHttp.onreadystatechange = stateChanged5;
        xmlHttp.open("GET", url, true);
        xmlHttp.send(null);
    }
    function stateChanged5() {

        if ((xmlHttp.readyState == 4 || xmlHttp.readyState == "complete")) {
            var k = xmlHttp.responseText;

            var aa = k.split("^");
            var sd = aa[0];
            document.getElementById('lblPickupPlace').innerHTML = sd;
            var sd1 = aa[1];
            //alert(sd1);
            document.getElementById('lblDepTime').innerHTML = sd1;
            document.getElementById('hidPickupPlace').value = sd;
            document.getElementById('hidDepTime').value = sd1;
            var pAFare=aa[2];
            var pCFare=aa[3];
            document.getElementById('hdAServiceChargeFare').value=pAFare;
            document.getElementById('hdCServiceChargeFare').value=pCFare;
           if (pAFare > 0)
           {
              document.getElementById('lblFare').innerHTML='<b>Current selected Pickup Point`s Service charge is  (Adult / Child) : <span class="rupee">`</span> ' + pAFare +' /- <span class="rupee">`</span> '+ pCFare + '/-.</b>';
          }
          else
          {
            document.getElementById('lblFare').innerHTML='';
          }
        }
    } 
    <!--
        var backColorOver = "#9bc7f4";
        var backColorOut = "#ffffff";
        function SetColor(val) {
            var tdNo = 4;
            //alert(val);
            tdNo = tdNo + val
            chkValidObj(document.getElementById('td' + (val + 1)), backColorOver);
            chkValidObj(document.getElementById('td' + tdNo), backColorOver);
            chkValidObj(document.getElementById('td' + (tdNo + 2)), backColorOver);
            chkValidObj(document.getElementById('td' + tdNo + 'twin'), backColorOver);
            chkValidObj(document.getElementById('td' + tdNo + 'triple'), backColorOver);
            chkValidObj(document.getElementById('td' + tdNo + 'childbed'), backColorOver);
            chkValidObj(document.getElementById('td' + tdNo + 'single'), backColorOver);
            chkValidObj(document.getElementById('td' + tdNo + 'dormitory'), backColorOver);
            chkValidObj(document.getElementById('tdAWF' + tdNo), backColorOver);
            chkValidObj(document.getElementById('tdCWF' + (tdNo + 2)), backColorOver);
            val++;
            clearColor(val);

        }
        function chkValidObj(obj, color) {
            if (obj != null) {
                obj.style.backgroundColor = color;
            }
        }
        function clearColor(c) {
            var tdNo = (4 + (c % 2));
            chkValidObj(document.getElementById('td' + (3 - c)), backColorOut);
            chkValidObj(document.getElementById('td' + tdNo), backColorOut);
            chkValidObj(document.getElementById('td' + (tdNo + 2)), backColorOut);
            chkValidObj(document.getElementById('td' + tdNo + 'twin'), backColorOut);
            chkValidObj(document.getElementById('td' + tdNo + 'triple'), backColorOut);
            chkValidObj(document.getElementById('td' + tdNo + 'childbed'), backColorOut);
            chkValidObj(document.getElementById('td' + tdNo + 'single'), backColorOut);
            chkValidObj(document.getElementById('td' + tdNo + 'dormitory'), backColorOut);
            chkValidObj(document.getElementById('tdAWF' + tdNo), backColorOut);
            chkValidObj(document.getElementById('tdCWF' + (tdNo + 2)), backColorOut);
        }

        function chkTypeAc() {
            var k = document.getElementById('BuschkType').value;
            if (k == "N") {
                document.getElementById('RadAC').selected = false;
                alert("OOPs....!, Selected journey date is not available in AC,Try another date or choose NoN-AC");
                
                clearColor(0);
                //SetColor(1);
                return false;

            }
        }

        function chkTypeNAc() {
        //alert(document.getElementById('RadNAC').selected );
        //alert(document.getElementById('RadAC').selected );
            var k = document.getElementById('BuschkType').value;
            if (k == "Y") {
                document.getElementById('RadNAC').selected = false;
                alert("OOPs...! Selected journey date is not available in NON-AC,Try another date or choose AC");
                clearColor(1);
                //SetColor(0);
                return false;
            }
        }     
    -->
    </script>

</head>
<body>
    <form id="Form1" method="post" runat="server">
    <input id="BusserialH" type="hidden" name="BusserialH" runat="server" />
    <input id="OrderIDH" type="hidden" name="OrderIDH" runat="server" />
    <input id="TourNoH" type="hidden" name="TourNoH" runat="server" />
    <input id="totamt" type="hidden" value="1" name="totamt" runat="server" />
    <input id="nadults" type="hidden" value="1" name="nadults" />
    <input id="nchild" type="hidden" value="1" name="nchild" />
    <input type="hidden" name="hidTourId" id="hidTourId" runat="server" />
    <input type="hidden" id="hiddate" name="hiddate" runat="server" />
    <input type="hidden" id="raac" name="raac" runat="server" />
    <input type="hidden" id="ranac" name="ranac" runat="server" />
    <input type="hidden" id="rcac" name="rcac" runat="server" />
    <input type="hidden" id="rcnac" name="rcnac" runat="server" />
    <input type="hidden" id="rdac" name="rdac" runat="server" />
    <input type="hidden" id="rdnac" name="rdnac" runat="server" />
    <input type="hidden" id="ra2ac" name="ra2ac" runat="server" />
    <input type="hidden" id="ra2nac" name="ra2nac" runat="server" />
    <input type="hidden" id="ra3ac" name="ra3ac" runat="server" />
    <input type="hidden" id="ra3nac" name="ra3nac" runat="server" />
    <input type="hidden" id="rcbac" name="rcbac" runat="server" />
    <input type="hidden" id="rcbnac" name="rcbnac" runat="server" />
    <input type="hidden" id="rsac" name="rsac" runat="server" />
    <input type="hidden" id="rsnac" name="rsnac" runat="server" />
    <input type="hidden" id="aac" name="aac" runat="server" />
    <input type="hidden" id="anac" name="anac" runat="server" />
    <input type="hidden" id="cac" name="cac" runat="server" />
    <input type="hidden" id="cnac" name="cnac" runat="server" />
    <input type="hidden" id="dac" name="dac" runat="server" />
    <input type="hidden" id="dnac" name="dnac" runat="server" />
    <input type="hidden" id="a2ac" name="a2ac" runat="server" />
    <input type="hidden" id="a2nac" name="a2nac" runat="server" />
    <input type="hidden" id="a3ac" name="a3ac" runat="server" />
    <input type="hidden" id="a3nac" name="a3nac" runat="server" />
    <input type="hidden" id="cbac" name="cbac" runat="server" />
    <input type="hidden" id="cbnac" name="cbnac" runat="server" />
    <input type="hidden" id="sac" name="sac" runat="server" />
    <input type="hidden" id="snac" name="snac" runat="server" />
    <input type="hidden" id="prevadu" name="prevadu" runat="server" />
    <input type="hidden" id="prevchi" name="prevchi" runat="server" />
    <input type="hidden" id="order" name="order" runat="server" />
    <input type="hidden" id="optedSeatNos" value="" runat="server" />
    <input type="hidden" id="maxSeatAllowed" runat="server" />
    <input type="hidden" id="service" name="service" runat="server" />
    <input type="hidden" id="credit" name="credit" runat="server" />
    <input type="hidden" id="sess" name="sess" runat="server" />
    <input type="hidden" id="stax" name="stax" value="0" runat="server" />
    <input type="hidden" id="cc" name="cc" value="0" runat="server" />
    <input type="hidden" id="discount" name="discount" value="0" runat="server" />
    <input type="hidden" id="BuschkType" name="discount" value="0" runat="server" />
    <input type="hidden" id="hdNoofDays" name="hdNoofDays" value="0" runat="server" />
    <asp:HiddenField ID="hdAdults" runat="server" />
    <asp:HiddenField ID="hdChild" runat="server" />
    <table id="table1" cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td colspan="1">
                <uc1:AgentHeader ID="agentHeader" runat="server" />
            </td>
        </tr>
        <tr>
            <td valign="top" align="center" style="width: 100%">
                <table width="80%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td>
                            <table id="table2" cellspacing="0" cellpadding="0" width="100%" border="0" height="250">
                                <tr>
                                    <td valign="top" align="center">
                                        <asp:Label ID="lbMsgErr" runat="server" CssClass="heads" Font-Bold="true"></asp:Label>&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="heads" align="center">
                                        <asp:HyperLink ID="hlmsgerr" runat="server" CssClass="heads" Font-Bold="true"></asp:HyperLink>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="heads" align="right" width="760">
                                        <asp:Label ID="LblMsg" runat="server" CssClass="hlinks"></asp:Label>
                                        <asp:HyperLink ID="hlback" runat="server" CssClass="heads" Font-Bold="true" NavigateUrl="AgentHomepage.aspx">Back</asp:HyperLink>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <table width="96%" cellspacing="0" cellpadding="0" border="0" style="background-color: #cccccc">
                                            <tr>
                                                <td align="left">
                                                    <img  loading="lazy" src="Assets/images/left_corner.gif" alt="left_corner" />
                                                </td>
                                                <td bgcolor="#e7e7e7">
                                                    <img  loading="lazy" src="Assets/images/trans.gif" alt="trans" />
                                                </td>
                                                <td align="right">
                                                    <img  loading="lazy" src="Assets/images/rgt_corner.gif" alt="rgt_corner" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="8" bgcolor="#E7E7E7">
                                                    &nbsp;
                                                </td>
                                                <td align="center">
                                                    <table id="table3" style="background-color: #ffffff" cellspacing="1" cellpadding="0"
                                                        width="100%" border="0">
                                                        <tr>
                                                            <td class="cgi1" align="center" style="background-color: #5aa1ea; height: 39px; width: 115px;">
                                                                &nbsp;Tour Name
                                                            </td>
                                                            <td class="cgi1" align="center" style="background-color: #5aa1ea; width: 158px; height: 39px;">
                                                                &nbsp;Pickup Point
                                                            </td>
                                                            <td class="cgi1" align="center" style="background-color: #5aa1ea; width: 196px; height: 39px;">
                                                                &nbsp;Pickup Address
                                                            </td>
                                                            <td class="cgi1" align="center" style="background-color: #5aa1ea; height: 39px;">
                                                                &nbsp;Departure Time
                                                            </td>
                                                            <td class="cgi1" valign="top" align="center" width="80px" style="background-color: #5aa1ea;
                                                                height: 39px;">
                                                                Journey Date
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="hlinks" valign="top" align="center" style="height: 50px; width: 130px;">
                                                                <asp:Label ID="lblTourName" runat="server" CssClass="hlinks" Font-Size="9pt"></asp:Label>
                                                            </td>
                                                            <td class="hlinks" valign="top" align="center" style="height: 50px; width: 158px;">
                                                                <asp:DropDownList ID="ddlPickUp" runat="server" DataTextField="PickupPlace" DataValueField="Rowid"
                                                                    CssClass="hlinks" Font-Size="9pt" Width="90%" onchange="javascript:fillTransfer(this.value);">
                                                                </asp:DropDownList>
                                                                <asp:HiddenField runat="server" ID="hidPickupPlace" />
                                                                <asp:HiddenField runat="server" ID="hidDepTime" />
                                                            </td>
                                                            <td class="hlinks" valign="top" align="left" style="height: 50px; width: 258px;">
                                                                <asp:Label ID="lblPickupPlace" runat="server" CssClass="hlinks" Font-Size="9pt"></asp:Label>
                                                            </td>
                                                            <td class="hlinks" valign="top" align="center" width="150px" style="height: 50px">
                                                                <asp:Label ID="lblDepTime" runat="server" CssClass="hlinks"></asp:Label>
                                                            </td>
                                                            <td class="hlinks" valign="top" align="center" width="80px" style="height: 50px">
                                                                <input class="hlinks" id="Deptdate" type="hidden" value="01/01/1900" name="Deptdate"
                                                                    runat="server" />
                                                                <asp:DropDownList ID="ddlJdate" runat="server" Font-Size="10pt" onChange="Getfare()">
                                                                </asp:DropDownList>
                                                                <div class="cgi" id="divNAavail" style="display: none">
                                                                    &nbsp;</div>
                                                                <!--OnSelectedIndexChanged="ddlJdate_SelectedIndexChanged"-->
                                                                <div style="height: 50px;">
                                                                    <div id="imgWait" style="display: none;">
                                                                        <img  loading="lazy" alt="wait_spinner" width="33px" height="33px" src="Assets/images/wait_spinner.gif" /><br />
                                                                        Please wait...
                                                                    </div>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td class="hlinks" valign="top" align="left" colspan="3">
                                                                <strong style="color: Red">Note : </strong>* marked Pickup Point will comprise of
                                                                some service charge.<br />
                                                                <asp:Label ID="lblFare" runat="server" CssClass="hlinks"></asp:Label>
                                                                <asp:HiddenField ID="hdAServiceChargeFare" Value="0" runat="server" />
                                                                <asp:HiddenField ID="hdCServiceChargeFare" Value="0" runat="server" />
                                                                <asp:HiddenField ID="hdServiceChargeTax" Value="0" runat="server" />
                                                            </td>
                                                            <td class="hlinks">
                                                                <div>
                                                                    <strong style="color: Red">Note : </strong>Dates on which the tour is available</div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td width="8" bgcolor="#E7E7E7">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <img  loading="lazy" alt="left_d_corner" width="8" height="8" src="../Assets/images/left_d_corner.gif" />
                                                </td>
                                                <td bgcolor="#e7e7e7">
                                                    <img  loading="lazy" alt="trans" width="1" height="1" src="../Assets/images/trans.gif" />
                                                </td>
                                                <td align="right">
                                                    <img  loading="lazy" alt="rgt_d_corner" width="8" height="8" src="../Assets/images/rgt_d_corner.gif" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr height="5">
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <table id="table8" cellspacing="0" cellpadding="0" width="96%" border="0" align="center">
                                            <tr bgcolor="#FFFFFF">
                                                <td id="tdcurrent" valign="top" align="left">
                                                    <table id="table5" runat="server" cellspacing="0" cellpadding="0" border="0" style="background-color: #cccccc">
                                                        <tr>
                                                            <td align="left">
                                                                <img  loading="lazy"  src="Assets/images/left_corner.gif" alt="left_corner" />
                                                            </td>
                                                            <td bgcolor="#e7e7e7">
                                                                <img  loading="lazy"  src="Assets/images/trans.gif" alt="trans" />
                                                            </td>
                                                            <td align="right">
                                                                <img  loading="lazy"  src="Assets/images/rgt_corner.gif" alt="rgt_corner" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td bgcolor="#E7E7E7">
                                                                &nbsp;
                                                            </td>
                                                            <td style="background-color: #cccccc" valign="top" width="100%">
                                                                <table style="border-color: #000000; background-color: #ffffff" cellspacing="0" cellpadding="1"
                                                                    width="100%" border="0">
                                                                    <tr>
                                                                        <td align="center" class="cgi1" style="background-color: #5aa1ea; width: 134px">
                                                                        </td>
                                                                        <td align="center" class="cgi1" style="background-color: #5aa1ea">
                                                                            <asp:RadioButton ID="RadAC" onclick="Displayfare();SetColor(0);javascript:return chkTypeAc();"
                                                                                runat="server" GroupName="RadioType" Text="AC"></asp:RadioButton>
                                                                            <asp:Label ID="lblavailcheck" runat="server" ToolTip="Available Seats In AC" CssClass="heads"
                                                                                Font-Bold="true"></asp:Label>
                                                                        </td>
                                                                        <td align="center" class="cgi1" style="background-color: #5aa1ea">
                                                                            <asp:RadioButton ID="RadNAC" onclick="Displayfare();SetColor(1);javascript:return chkTypeNAc();"
                                                                                runat="server" GroupName="RadioType" Text="Non-AC"></asp:RadioButton>
                                                                            <asp:Label ID="lblNONACAVAIL" runat="server" ToolTip="Available Seats In Non-AC"
                                                                                CssClass="heads" Font-Bold="true"></asp:Label>
                                                                        </td>
                                                                        <td align="center" class="cgi1" style="background-color: #5aa1ea; width: 161px">
                                                                            No. of Persons
                                                                        </td>
                                                                        <td align="center" class="cgi1" style="background-color: #5aa1ea; width: 69px">
                                                                            Fare/Person
                                                                        </td>
                                                                        <td style="background-color: #5aa1ea; width: 7px">
                                                                        </td>
                                                                        <td align="center" class="cgi1" style="background-color: #5aa1ea; width: 97px">
                                                                            Calculated Amount
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="traf" runat="server">
                                                                        <td class="hlinks" align="left" style="border-color: #ffffff; height: 5px; width: 134px">
                                                                            Adults
                                                                        </td>
                                                                        <td class="hlinks" id="td4" valign="top" align="center" style="background-color: #ffffff">
                                                                            <div class="hlinks" id="divAACFAre" runat="server" ms_positioning="FlowLayout">
                                                                                <asp:Label ID="lblAACfare" runat="server" CssClass="hlinks"></asp:Label>
                                                                            </div>
                                                                        </td>
                                                                        <td class="hlinks" id="td5" valign="top" align="center" style="background-color: #ffffff">
                                                                            <div class="hlinks" id="divCACFAre" runat="server" ms_positioning="FlowLayout">
                                                                                <asp:Label ID="lblANACfare" runat="server" CssClass="hlinks"></asp:Label></div>
                                                                        </td>
                                                                        <td class="hlinks" id="tx" style="border-color: #ffffff; height: 5px; width: 161px">
                                                                            &nbsp;
                                                                            <asp:TextBox ID="txtNoOfAdults" runat="server" CssClass="hlinks" onkeypress="return chkNumeric(event);"
                                                                                onblur="Displayfare();" MaxLength="2" Columns="2" EnableViewState="False"  onfocus="javascript: if(this.value == '0'){ this.value = ''; }">0</asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;X
                                                                        </td>
                                                                        <td class="hlinks" id="td8" align="center" style="border-color: #ffffff; height: 5px;
                                                                            width: 69px">
                                                                            <div class="hlinks" id="div1" runat="server" ms_positioning="FlowLayout">
                                                                                <asp:Label ID="lblFareAdults" runat="server" CssClass="hlinks">0</asp:Label>.00&nbsp;</div>
                                                                        </td>
                                                                        <td class="hlinks" style="border-color: #ffffff; height: 5px; width: 7px" align="right">
                                                                            =
                                                                        </td>
                                                                        <td class="hlinks" id="td10" style="border-color: #ffffff; height: 5px; width: 97px"
                                                                            align="right">
                                                                            <div class="hlinks" id="div2" runat="server" ms_positioning="FlowLayout">
                                                                                <asp:Label ID="lblCalcAdults" runat="server" CssClass="hlinks">0</asp:Label>.00&nbsp;</div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="tr7" runat="server">
                                                                        <td colspan="7" bgcolor="#DCDCDC" height="1">
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="trcf" runat="server" style="border-color: #cccccc">
                                                                        <td class="hlinks" style="height: 25px; border-color: #ffffff; width: 134px" align="left">
                                                                            Children
                                                                        </td>
                                                                        <td id="td6" valign="top" align="center" style="background-color: #ffffff">
                                                                            <div class="hlinks" id="ANonACFAre" runat="server" ms_positioning="FlowLayout">
                                                                                <asp:Label ID="lblCACfare" runat="server" CssClass="hlinks"></asp:Label></div>
                                                                        </td>
                                                                        <td class="hlinks" id="td7" valign="top" align="center" style="background-color: #ffffff">
                                                                            <div class="hlinks" id="CNonACFAre" runat="server" ms_positioning="FlowLayout">
                                                                                <asp:Label ID="lblCNACfare" runat="server" CssClass="hlinks"></asp:Label></div>
                                                                        </td>
                                                                        <td class="hlinks" style="height: 25px; border-color: #ffffff; width: 161px">
                                                                            &nbsp;
                                                                            <asp:TextBox ID="txtNoOfChilds" runat="server" CssClass="hlinks" onkeypress="return chkNumeric(event);"
                                                                                onblur="Displayfare();" MaxLength="2" Columns="2" EnableViewState="False" onfocus="javascript: if(this.value == '0'){ this.value = ''; }">0</asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;X
                                                                        </td>
                                                                        <td class="hlinks" id="td9" style="height: 25px; border-color: #ffffff; width: 69px"
                                                                            align="center">
                                                                            <div class="hlinks" id="divChildAmt" runat="server" ms_positioning="FlowLayout">
                                                                                <asp:Label ID="lblfareChild" runat="server" CssClass="hlinks">0</asp:Label>.00&nbsp;</div>
                                                                        </td>
                                                                        <td class="hlinks" style="height: 25px; border-color: #ffffff; width: 7px" align="right">
                                                                            =
                                                                        </td>
                                                                        <td class="hlinks" id="td11" style="height: 25px; border-color: #ffffff; width: 97px"
                                                                            align="right">
                                                                            <div class="hlinks" id="divCalcChild" runat="server" ms_positioning="FlowLayout">
                                                                                <asp:Label ID="lblCalcChild" runat="server" CssClass="hlinks">0</asp:Label>.00&nbsp;</div>
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
                                                                        <td id="tdAWF4" valign="top" align="center" style="background-color: #ffffff">
                                                                            <div class="hlinks" id="AWFACFAre" runat="server" ms_positioning="FlowLayout">
                                                                                <asp:Label ID="lblAWFfare" runat="server" CssClass="hlinks"></asp:Label></div>
                                                                        </td>
                                                                        <td class="hlinks" id="tdAWF5" valign="top" align="center" style="background-color: #ffffff">
                                                                            <div class="hlinks" id="AWFNonACFAre" runat="server" ms_positioning="FlowLayout">
                                                                                <asp:Label ID="lblAWFNACfare" runat="server" CssClass="hlinks"></asp:Label></div>
                                                                        </td>
                                                                        <td class="hlinks" style="height: 25px; border-color: #ffffff">
                                                                            &nbsp;
                                                                            <asp:TextBox ID="txtNoAWFNoOfAdults" runat="server" CssClass="hlinks" onkeypress="return chkNumeric(event);"
                                                                                onblur="Displayfare();" MaxLength="2" Columns="2" EnableViewState="False" onfocus="javascript: if(this.value == '0'){ this.value = ''; }">0</asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;X
                                                                        </td>
                                                                        <td class="hlinks" id="tdAWF8" style="height: 25px; border-color: #ffffff" align="center">
                                                                            <div class="hlinks" id="divAWFAmt" runat="server" ms_positioning="FlowLayout">
                                                                                <asp:Label ID="lblAWFFareAdults" runat="server" CssClass="hlinks">0</asp:Label>.00</div>
                                                                        </td>
                                                                        <td class="hlinks" style="height: 25px; border-color: #ffffff" align="right">
                                                                            =
                                                                        </td>
                                                                        <td class="hlinks" id="tdAWF10" style="height: 25px; border-color: #ffffff" align="right">
                                                                            <div class="hlinks" id="divCalcAWF" runat="server" ms_positioning="FlowLayout">
                                                                                <asp:Label ID="lblCalcAWF" runat="server" CssClass="hlinks">0</asp:Label>.00</div>
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
                                                                        <td id="tdCWF6" valign="top" align="center" style="background-color: #ffffff">
                                                                            <div class="hlinks" id="CWFACFAre" runat="server" ms_positioning="FlowLayout">
                                                                                <asp:Label ID="lblCWFfare" runat="server" CssClass="hlinks"></asp:Label></div>
                                                                        </td>
                                                                        <td class="hlinks" id="tdCWF7" valign="top" align="center" style="background-color: #ffffff">
                                                                            <div class="hlinks" id="CWFNonACFAre" runat="server" ms_positioning="FlowLayout">
                                                                                <asp:Label ID="lblCWFNACfare" runat="server" CssClass="hlinks"></asp:Label></div>
                                                                        </td>
                                                                        <td class="hlinks" style="height: 25px; border-color: #ffffff">
                                                                            &nbsp;
                                                                            <asp:TextBox ID="txtNoCWFNoOfChilds" runat="server" CssClass="hlinks" onkeypress="return chkNumeric(event);"
                                                                                onblur="Displayfare();" MaxLength="2" Columns="2" EnableViewState="False" onfocus="javascript: if(this.value == '0'){ this.value = ''; }">0</asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;X
                                                                        </td>
                                                                        <td class="hlinks" id="tdCWF9" style="height: 25px; border-color: #ffffff" align="center">
                                                                            <div class="hlinks" id="divCWFAmt" runat="server" ms_positioning="FlowLayout">
                                                                                <asp:Label ID="lblCWFfareChild" runat="server" CssClass="hlinks">0</asp:Label>.00</div>
                                                                        </td>
                                                                        <td class="hlinks" style="height: 25px; border-color: #ffffff" align="right">
                                                                            =
                                                                        </td>
                                                                        <td class="hlinks" id="tdCWF11" style="height: 25px; border-color: #ffffff" align="right">
                                                                            <div class="hlinks" id="divCalcCWF" runat="server" ms_positioning="FlowLayout">
                                                                                <asp:Label ID="lblCWFCalcChild" runat="server" CssClass="hlinks">0</asp:Label>.00</div>
                                                                        </td>
                                                                    </tr>
                                                                    <!--for Dormetory Accommodation start-->
                                                                    <tr id="tra2f" runat="server" style="border-color: #cccccc">
                                                                        <td align="left" valign="top" class="hlinks" width="134px">
                                                                            Adult on Twin Sharing:
                                                                        </td>
                                                                        <td class="hlinks" id="td1" valign="top" align="center" style="background-color: #ffffff">
                                                                            <div class="hlinks" id="div3" runat="server" ms_positioning="FlowLayout">
                                                                                <asp:Label ID="lblA2ACfare" runat="server" CssClass="hlinks"></asp:Label></div>
                                                                        </td>
                                                                        <td class="hlinks" id="td2" valign="top" align="center" height="22" style="width: 157px;
                                                                            background-color: #ffffff">
                                                                            <div class="hlinks" id="div4" runat="server" ms_positioning="FlowLayout">
                                                                                <asp:Label ID="lblA2NACfare" runat="server" CssClass="hlinks"></asp:Label></div>
                                                                        </td>
                                                                        <td class="hlinks" id="tx2" style="border-color: #ffffff" width="161px">
                                                                            &nbsp;
                                                                            <asp:TextBox ID="txtNoOfAdultsTwin" runat="server" CssClass="hlinks" onkeypress="return chkNumeric(event);"
                                                                                onblur="validtwin()" MaxLength="2" Columns="2" EnableViewState="False" onfocus="javascript: if(this.value == '0'){ this.value = ''; }">0</asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;X
                                                                        </td>
                                                                        <td class="hlinks" id="td8twin" align="center" style="border-color: #ffffff" width="69px">
                                                                            <div class="hlinks" id="divAdultAmttwin" runat="server" ms_positioning="FlowLayout">
                                                                                <asp:Label ID="lblFareAdultsTwin" runat="server" CssClass="hlinks">0</asp:Label>.00&nbsp;</div>
                                                                        </td>
                                                                        <td class="hlinks" align="right" style="border-color: #ffffff" width="7px">
                                                                            =
                                                                        </td>
                                                                        <td class="hlinks" id="td10twin" align="right" style="border-color: #ffffff" width="97px">
                                                                            <div class="hlinks" id="divCalcAdultTwin" runat="server" ms_positioning="FlowLayout">
                                                                                <asp:Label ID="lblCalcAdultsTwin" runat="server" CssClass="hlinks">0</asp:Label>.00&nbsp;</div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="tr9" runat="server">
                                                                        <td colspan="7" bgcolor="#DCDCDC" height="1">
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="tra3f" runat="server" style="border-color: #cccccc">
                                                                        <td align="left" valign="top" style="border-color: #ffffff" class="hlinks" width="134px">
                                                                            Adult on Triple Sharing:
                                                                        </td>
                                                                        <td class="hlinks" id="td4triple" valign="top" align="center" style="background-color: #ffffff">
                                                                            <div class="hlinks" id="divA3ACFAre" runat="server" ms_positioning="FlowLayout">
                                                                                <asp:Label ID="lblA3ACfare" runat="server" CssClass="hlinks"></asp:Label>
                                                                            </div>
                                                                        </td>
                                                                        <td class="hlinks" id="td5triple" valign="top" align="center" height="22" style="width: 157px;
                                                                            background-color: #ffffff">
                                                                            <div class="hlinks" id="divA3NACFAre" runat="server" ms_positioning="FlowLayout">
                                                                                <asp:Label ID="lblA3NACfare" runat="server" CssClass="hlinks"></asp:Label></div>
                                                                        </td>
                                                                        <td class="hlinks" id="tx3" style="border-color: #ffffff" width="161px">
                                                                            &nbsp;
                                                                            <asp:TextBox ID="txtNoOfAdultsTriple" runat="server" CssClass="hlinks" onkeypress="return chkNumeric(event);"
                                                                                onblur="validtriple()" MaxLength="2" Columns="2" EnableViewState="False" onfocus="javascript: if(this.value == '0'){ this.value = ''; }">0</asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;X
                                                                        </td>
                                                                        <td class="hlinks" id="td8triple" align="center" style="border-color: #ffffff" width="69px">
                                                                            <div class="hlinks" id="divAdultAmttriple" runat="server" ms_positioning="FlowLayout">
                                                                                <asp:Label ID="lblFareAdultsTriple" runat="server" CssClass="hlinks">0</asp:Label>.00&nbsp;</div>
                                                                        </td>
                                                                        <td class="hlinks" align="right" style="border-color: #ffffff" width="7px">
                                                                            =
                                                                        </td>
                                                                        <td class="hlinks" id="td10triple" align="right" style="border-color: #ffffff" width="97px">
                                                                            <div class="hlinks" id="divCalcAdultTriple" runat="server" ms_positioning="FlowLayout">
                                                                                <asp:Label ID="lblCalcAdultsTriple" runat="server" CssClass="hlinks">0</asp:Label>.00&nbsp;</div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="tr10" runat="server">
                                                                        <td colspan="7" bgcolor="#DCDCDC" height="1">
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="trcbf" runat="server" style="border-color: #cccccc">
                                                                        <td align="left" valign="top" class="hlinks" width="134px">
                                                                            Child(5-11)Without Bed:
                                                                        </td>
                                                                        <td class="hlinks" id="td4childbed" valign="top" align="center" style="background-color: #ffffff">
                                                                            <div class="hlinks" id="divCBACFAre" runat="server" ms_positioning="FlowLayout">
                                                                                <asp:Label ID="lblCBACfare" runat="server" CssClass="hlinks"></asp:Label></div>
                                                                        </td>
                                                                        <td class="hlinks" id="td5childbed" valign="top" align="center" height="22" style="width: 157px;
                                                                            background-color: #ffffff">
                                                                            <div class="hlinks" id="divCBNACFAre" runat="server" ms_positioning="FlowLayout">
                                                                                <asp:Label ID="lblCBNACfare" runat="server" CssClass="hlinks"></asp:Label></div>
                                                                        </td>
                                                                        <td class="hlinks" id="txcb" width="161px">
                                                                            &nbsp;
                                                                            <asp:TextBox ID="txtNoOfChildBed" runat="server" CssClass="hlinks" onkeypress="return chkNumeric(event);"
                                                                                onblur="Displayfare();" MaxLength="2" Columns="2" EnableViewState="False" onfocus="javascript: if(this.value == '0'){ this.value = ''; }">0</asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;X
                                                                        </td>
                                                                        <td class="hlinks" id="td8childbed" align="center" width="69px">
                                                                            <div class="hlinks" id="divChildBedAmt" runat="server" ms_positioning="FlowLayout">
                                                                                <asp:Label ID="lblFareChildBed" runat="server" CssClass="hlinks">0</asp:Label>.00&nbsp;</div>
                                                                        </td>
                                                                        <td class="hlinks" align="right" width="7px">
                                                                            =
                                                                        </td>
                                                                        <td class="hlinks" id="td10childbed" align="right" width="97px">
                                                                            <div class="hlinks" id="divCalcChildBed" runat="server" ms_positioning="FlowLayout">
                                                                                <asp:Label ID="lblCalcChildBed" runat="server" CssClass="hlinks">0</asp:Label>.00&nbsp;</div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="tr11" runat="server">
                                                                        <td colspan="7" bgcolor="#DCDCDC" height="1">
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="trsf" runat="server" style="border-color: #cccccc">
                                                                        <td align="left" valign="top" class="hlinks" width="134px">
                                                                            Single Adult In a Room:
                                                                        </td>
                                                                        <td class="hlinks" id="td4single" valign="top" align="center" style="background-color: #ffffff">
                                                                            <div class="hlinks" id="divSACFAre" runat="server" ms_positioning="FlowLayout">
                                                                                <asp:Label ID="lblSACfare" runat="server" CssClass="hlinks"></asp:Label></div>
                                                                        </td>
                                                                        <td class="hlinks" id="td5single" valign="top" align="center" height="22" style="width: 157px;
                                                                            background-color: #ffffff">
                                                                            <div class="hlinks" id="divSNACFAre" runat="server" ms_positioning="FlowLayout">
                                                                                <asp:Label ID="lblSNACfare" runat="server" CssClass="hlinks"></asp:Label></div>
                                                                        </td>
                                                                        <td class="hlinks" id="txs" width="161px">
                                                                            &nbsp;
                                                                            <asp:TextBox ID="txtNoOfSingles" runat="server" CssClass="hlinks" onkeypress="return chkNumeric(event);"
                                                                                onblur="Displayfare();" MaxLength="2" Columns="2" EnableViewState="False" onfocus="javascript: if(this.value == '0'){ this.value = ''; }">0</asp:TextBox>
                                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;X
                                                                        </td>
                                                                        <td class="hlinks" id="td8single" align="center" width="69px">
                                                                            <div class="hlinks" id="divSingleAmt" runat="server" ms_positioning="FlowLayout">
                                                                                <asp:Label ID="lblFareSingles" runat="server" CssClass="hlinks">0</asp:Label>.00&nbsp;</div>
                                                                        </td>
                                                                        <td class="hlinks" align="right" width="7px">
                                                                            =
                                                                        </td>
                                                                        <td class="hlinks" id="td10single" align="right" width="97px">
                                                                            <div class="hlinks" id="divCalcSingle" runat="server" ms_positioning="FlowLayout">
                                                                                <asp:Label ID="lblCalcSingles" runat="server" CssClass="hlinks">0</asp:Label>.00&nbsp;</div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="tr8" runat="server">
                                                                        <td colspan="7" bgcolor="#DCDCDC" height="1">
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="tr12" runat="server">
                                                                        <td colspan="7" bgcolor="#DCDCDC" height="1">
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="tradf" runat="server" style="border-color: #cccccc">
                                                                        <td align="left" valign="top" class="hlinks">
                                                                            Dormitory accommodation:
                                                                        </td>
                                                                        <td class="hlinks" id="td4dormitory" valign="top" align="center" style="background-color: #ffffff">
                                                                            <div class="hlinks" id="div1dormitory" runat="server" ms_positioning="FlowLayout">
                                                                                <asp:Label ID="lbldACfare" runat="server" CssClass="hlinks"></asp:Label></div>
                                                                        </td>
                                                                        <td class="hlinks" id="td5dormitory" valign="top" align="center" height="22" style="width: 157px;
                                                                            background-color: #ffffff">
                                                                            <div class="hlinks" id="div2dormitory" runat="server" ms_positioning="FlowLayout">
                                                                                <asp:Label ID="lbldNACfare" runat="server" CssClass="hlinks"></asp:Label></div>
                                                                        </td>
                                                                        <td class="hlinks" id="txd" style="border-color: #ffffff">
                                                                            &nbsp;
                                                                            <asp:TextBox ID="txtNoofdormitory" runat="server" CssClass="hlinks" onkeypress="return chkNumeric(event);"
                                                                                onblur="Displayfare();" MaxLength="2" Columns="2" EnableViewState="False" onfocus="javascript: if(this.value == '0'){ this.value = ''; }">0</asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;X
                                                                        </td>
                                                                        <td class="hlinks" id="td8dormitory" align="center" style="border-color: #ffffff">
                                                                            <div class="hlinks" id="div3dormitory" runat="server" ms_positioning="FlowLayout">
                                                                                <asp:Label ID="lblFaredormitory" runat="server" CssClass="hlinks">0</asp:Label>.00</div>
                                                                        </td>
                                                                        <td class="hlinks" align="right" style="border-color: #ffffff">
                                                                            =
                                                                        </td>
                                                                        <td class="hlinks" id="td10dormitory" align="right" style="border-color: #ffffff">
                                                                            <div class="hlinks" id="div4dormitory" runat="server" ms_positioning="FlowLayout">
                                                                                <asp:Label ID="lblCalcdormitory" runat="server" CssClass="hlinks">0</asp:Label>.00</div>
                                                                        </td>
                                                                    </tr>
                                                                    <!--for Dormetory Accommodation End-->
                                                                    <tr style="border-color: #cccccc">
                                                                        <td class="hlinks" align="right" style="background-color: #9bc7f4" colspan="6">
                                                                            Total:
                                                                        </td>
                                                                        <td class="hlinks" align="right" style="background-color: #9bc7f4" height="5">
                                                                            <div id="divTotal" runat="server" ms_positioning="FlowLayout">
                                                                                <asp:Label ID="lblTotal" runat="server" CssClass="hlinks">0.00</asp:Label>.00&nbsp;</div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="7" bgcolor="#DCDCDC" height="1">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="left" style="background-color: #9bc7f4" colspan="4" height="5">
                                                                            <asp:Label ID="Lblchild" runat="server" CssClass="heads" Font-Bold="true" Text="Child: Upto 11 years of age can be treated as child."></asp:Label>&nbsp;
                                                                        </td>
                                                                        <td align="right" style="background-color: #9bc7f4" colspan="3" height="5">
                                                                            <asp:Label ID="Labeltax" runat="server" CssClass="heads" Font-Bold="true"></asp:Label>&nbsp;
                                                                            <br />
                                                                            <asp:Label ID="lblServiceCharge" runat="server" CssClass="heads" Font-Bold="true" />&nbsp;
                                                                            <asp:Label ID="lblLTC" runat="server" CssClass="heads" Font-Bold="true" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="left" height="5" class="hlinks">
                                                                            Remarks :
                                                                        </td>
                                                                        <td align="left" height="5" colspan="0">
                                                                            <asp:TextBox ID="txtremarks" runat="server" CssClass="hlinks" EnableViewState="True"
                                                                                MaxLength="150" Width="500"></asp:TextBox>
                                                                        </td>
                                                                        <td align="right" height="5" colspan="5">
                                                                            <asp:RadioButtonList ID="rbtnPaymentOption" runat="server" RepeatColumns="2">
                                                                                <asp:ListItem Text="50 % Payment" Value="HALF"></asp:ListItem>
                                                                                <asp:ListItem Text="Full Payment" Value="FullPayment"></asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center" style="background-color: #ffffff" colspan="7" height="5">
                                                                            &nbsp;&nbsp;&nbsp;
                                                                            <asp:Button ID="btncheckavail" runat="server" CssClass="btn" Font-Size="8pt" Width="155px"
                                                                                Text="CHECK AVAILABILITY" BorderStyle="Solid" Style="background-color: #5aa1ea"
                                                                                OnClick="btncheckavail_Click"></asp:Button>&nbsp;&nbsp;
                                                                            <asp:Button ID="btnReset" runat="server" Text="ReSet" OnClick="btnReset_Click" CssClass="btn"
                                                                                BorderStyle="Solid" Style="background-color: #5aa1ea" Font-Size="8pt" Width="70px" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td bgcolor="#E7E7E7">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <img  loading="lazy" src="Assets/images/left_d_corner.gif" alt="left_d_corner" />
                                                            </td>
                                                            <td bgcolor="#e7e7e7">
                                                                <img  loading="lazy" src="Assets/images/trans.gif" alt="trans" />
                                                            </td>
                                                            <td align="right">
                                                                <img  loading="lazy" src="Assets/images/rgt_d_corner.gif" alt="rgt_d_corner" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 3px">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <!--New Row for Seating Chart-->
                                            <tr>
                                                <td style="height: 10px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">
                                                    <table id="colorindication" runat="server" align="center" width="100%" border="0"
                                                        cellspacing="0" cellpadding="0">
                                                        <%--<tr>
                                                            <td align="left" valign="top">
                                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                    <tr>
                                                                        <td align="left" valign="top" class="TB_lt">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td align="left" valign="top" class="TB_Mrpt">
                                                                            <%if (Chart != null)
                                                                              {%>
                                                                            <%=Chart.ToString()%>
                                                                            <%} %>
                                                                            
                                                                        </td>
                                                                        <td align="left" valign="top" class="TB_Rt">
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" valign="top">
                                                                <img  loading="lazy" alt="" src="Assets/images/spacer.gif" width="1" height="30" />
                                                            </td>
                                                        </tr>
                                                        <tr >
                                                            <td align="center" valign="top">
                                                                <table width="45%" border="0" cellspacing="0" cellpadding="0" align="center">
                                                                    <tr>
                                                                        <td width="7%" align="left" valign="middle">
                                                                            <img  loading="lazy" alt="" src="Assets/images/booked_img.gif" width="22" height="20" />
                                                                        </td>
                                                                        <td width="26%" align="left" valign="middle" class="Rfnt">
                                                                            Booked
                                                                        </td>
                                                                        <td width="6%" align="left" valign="middle">
                                                                            <img  loading="lazy" alt="" src="Assets/images/selected_img.gif" width="23" height="20" />
                                                                        </td>
                                                                        <td width="27%" align="left" valign="middle" class="Rfnt">
                                                                            Selected
                                                                        </td>
                                                                        <td width="6%" align="left" valign="middle">
                                                                            <img  loading="lazy" alt="" src="Assets/images/available_img.gif" width="23" height="20" />
                                                                        </td>
                                                                        <td width="28%" align="left" valign="middle" class="Rfnt">
                                                                            Available
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" valign="top">
                                                            </td>
                                                        </tr>--%>
                                                        <tr>
                                                            <td align="left" valign="top">
                                                             <div class="seatselectwrap" id="Div5" runat="server">
                                                                        <h3 class="title">
                                                                            Seat <span>Selection</span>
                                                                            <ul class="seatlist">
                                                                                <li class="booked">Booked</li>
                                                                                <li class="selected">Selected</li>
                                                                                <li class="avail">Available</li>
                                                                            </ul>
                                                                            <%--<div class="selectiondiv">
                                                                                <div class="frontsection">
                                                                                    <div class="seat-d">
                                                                                        <img  loading="lazy" alt="" src="Assets/images/seat-d.png"></div>
                                                                                    <div class="seat-c">
                                                                                        <img  loading="lazy" alt="" src="Assets/images/seat-c.png"></div>
                                                                                    <div class="bus-entry">
                                                                                        <img  loading="lazy" alt="" src="Assets/images/bus-entry.jpg"></div>
                                                                                </div>
                                                                                <div class="backsection">
                                                                                    <div class="bus-midspace bus-midspace2">
                                                                                        &nbsp;</div>--%>
                                                                                    <%if (Chart != null)
                                                                                      {%>
                                                                                    <%=Chart.ToString()%>
                                                                                    <%} %>
                                                                                <%--</div>
                                                                            </div>--%>
                                                                        </h3>
                                                                    </div>
                                                                <%--<%if (Chart != null)
                                                                  {%>
                                                                <%=Chart.ToString()%>
                                                                <%} %>--%>
                                                            </td>
                                                        </tr>
                                                        <%--<tr>
                                                            <td align="center" valign="top">
                                                                <table width='35%' border='0' cellspacing='0' cellpadding='0' align='center'>
                                                                    <tr>
                                                                        <td width='7%' align='left' valign='middle'>
                                                                            <img  loading="lazy" alt="" src='Assets/images/booked1_img.gif' width='22' height='20' />
                                                                        </td>
                                                                        <td width='26%' align='left' valign='middle' class='Rfnt'>
                                                                            Booked
                                                                        </td>
                                                                        <td width='6%' align='left' valign='middle'>
                                                                            <img  loading="lazy" alt="" src='Assets/images/selected_img.gif' width='23' height='20' />
                                                                        </td>
                                                                        <td width='27%' align='left' valign='middle' class='Rfnt'>
                                                                            Selected
                                                                        </td>
                                                                        <td width='6%' align='left' valign='middle'>
                                                                            <img  loading="lazy" alt="" src='Assets/images/available_img.gif' width='23' height='20' />
                                                                        </td>
                                                                        <td width='28%' align='left' valign='middle' class='Rfnt'>
                                                                            Available
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>--%>
                                                        <tr>
                                                            <td align="left" valign="top">
                                                                <img  loading="lazy" alt="spacer1" src="Assets/images/spacer1.gif" width="1" height="30" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" valign="top">
                                                                <asp:Button ID="btnContinuee" runat="server" CssClass="btn" Style="background-color: #5aa1ea"
                                                                    Text="Continue" OnClientClick="return checkseats();" OnClick="btnContinuee_Click">
                                                                </asp:Button>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <%--<table id="colorindication" runat="server" align="center" cellspacing="0" cellpadding="0"
                                                            width="100%" border="0">
                                                            <tr>
                                                                <td>
                                                                    <table align="center" cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                        <%if (Chart != null)
                                                                      {%>
                                                                        <%=Chart.ToString()%>
                                                                        <%} %>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td height="10">
                                                                </td>
                                                            </tr>
                                                            <tr id="Tr1" runat="server">
                                                                <td align="center" colspan="3">
                                                                    <table width="100%" cellspacing="1" cellpadding="5" border="0" class="hlinks" style="background-color: #cccccc">
                                                                        <tr style="height: 20px" bgcolor="#ffffff">
                                                                            <td bgcolor="#FF7755" width="20" valign="middle">
                                                                            </td>
                                                                            <td align="left" valign="middle" class="heads" bgcolor="#ffffff">
                                                                                &nbsp;<img  loading="lazy" alt="" src="Assets/images/bullet.gif" alt="-" border="0" />&nbsp;Booked Seats
                                                                            </td>
                                                                            <td bgcolor="#9bc7f4" width="20" valign="middle">
                                                                            </td>
                                                                            <td align="left" valign="middle" class="heads" bgcolor="#ffffff">
                                                                                &nbsp;<img  loading="lazy" alt="" src="Assets/images/bullet.gif" alt="-" border="0" />&nbsp;Available Seats
                                                                            </td>
                                                                            <td bgcolor="#5aa1ea" width="20" valign="middle">
                                                                            </td>
                                                                            <td align="left" valign="middle" class="heads" bgcolor="#ffffff">
                                                                                &nbsp;<img  loading="lazy" alt="" src="Assets/images/bullet.gif" alt="-" border="0" />&nbsp;Selected Seats
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 24px" align="center" colspan="3">
                                                                    <asp:Button ID="btnContinuee" runat="server" CssClass="btn" Style="background-color: #5aa1ea"
                                                                        Text="Continue" OnClientClick="return checkseats();" OnClick="btnContinuee_Click">
                                                                    </asp:Button>
                                                                </td>
                                                            </tr>
                                                        </table>--%>
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
        <tr>
            <td height="10">
            </td>
        </tr>
    </table>
    <uc1:AgentFooter ID="AFooter" runat="server"></uc1:AgentFooter>

    <script language="javascript" type="text/javascript">
        <!--
        if ('<%=autoPost%>' != 'false') {
            //alert(document.getElementById('ddlJdate').options.length);
            for (var k = 0; k < document.getElementById('ddlJdate').options.length; k++) {
                if (document.getElementById('ddlJdate').options[k].value == '<%=autoPost%>') {

                    document.getElementById('ddlJdate').selectedIndex = k;
                    Getfare();
                }
            }
        }
        -->
    </script>

    </form>
</body>
</html>
