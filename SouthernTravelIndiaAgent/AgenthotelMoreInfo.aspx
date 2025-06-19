<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgenthotelMoreInfo.aspx.cs" Inherits="SouthernTravelIndiaAgent.AgenthotelMoreInfo" %>

<%@ Register TagPrefix="uc1" TagName="BranchHeader" Src="UserControls/UcAgentHeader.ascx" %>
<%@ Register TagPrefix="uc1" TagName="BranchFooter" Src="UserControls/UcAgentfooter.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Hotel booking review</title>
    <link href="../Assets/css/main.css" rel="stylesheet" />
     <link href="../Assets/css/style.css" type="text/css" rel="stylesheet" />
    <link href="../Assets/css/stylesheet.css" rel="stylesheet" type="text/css" />

    <link rel="stylesheet" type="text/css" href="chrome://firebug/content/highlighter.css" />

    <script>
    <!-- 
           function showDivs(ddd)
           {
                hideAll();
                document.getElementById('div'+ddd).style.display='block';
                 
           }
           function hideAll(){
                        document.getElementById('divOverView').style.display='none';  
                        document.getElementById('divRooms').style.display='none';
                        document.getElementById('divAttr').style.display='none';     
                        document.getElementById('divFac').style.display='none';
                        document.getElementById('divIn').style.display='none';
                        document.getElementById('divPhoto').style.display='none';
            }
    -->
    </script>

</head>
<body topmargin="0">
    <form id="form1" method="post" runat="server">
        <div id="mainDiv" runat="server">
            <table cellspacing="0" cellpadding="0" width="100%" border="0" valign="top" align="center"
                id="desiya">
                <tbody>
                    <tr>
                        <td style="height: 351px">
                            <table cellspacing="0" cellpadding="0" border="0" width="100%" id="HeaderTable">
                                <tbody>
                                    <tr>
                                        <td>
                                            <!--Start Header -->
                                            <table id="Table1" cellspacing="0" cellpadding="0" style="width: 100%" border="0">
                                                <tr>
                                                    <td colspan="2">
                                                        <uc1:BranchHeader ID="BranchHeader1" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" valign="top">
                                                    </td>
                                                </tr>
                                            </table>
                                            <!--End Header -->
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="tab-content">
                                <table cellspacing="1" cellpadding="1" border="0" class="bg2-left" id="CenterTable">
                                    <tbody>
                                        <tr>
                                            <td valign="top" bgcolor="#ffffff" align="left" id="LeftCell" style="width: 96px;
                                                height: 16px;">

                                                <script language="javascript"></script>

                                            </td>
                                            <td valign="top" bgcolor="#ffffff" align="right" id="RightCell" style="height: 16px">
                                                <div class="tab-content">
                                                    <table cellspacing="0" cellpadding="0" border="0" id="Table2">
                                                        <tbody>
                                                            <tr>
                                                                <td valign="top" align="left" id="LeftCellMoreInfo">
                                                                    <table width="165" height="100%" cellspacing="0" cellpadding="0" border="0" align="left">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td valign="top" align="left">
                                                                                    <a href="moreinfo.do?link=RoomDescription" /><a href="moreinfo.do?link=ServiceAttribute" />
                                                                                    <a href="moreinfo.do?link=facilities" /><a href="moreinfo.do?link=attraction" />
                                                                                    <a href="moreinfo.do?link=photo" />
                                                                                    <table width="150" height="100%" cellspacing="0" cellpadding="0" border="0">
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <td height="75" class="tab-table">
                                                                                                    <table cellspacing="0" cellpadding="2" border="0">
                                                                                                        <tbody>
                                                                                                            <tr>
                                                                                                                <td style="width: 33px">
                                                                                                                    <img src="Assets/images/arrow.gif" /></td>
                                                                                                                <td>
                                                                                                                    <a class="a-red" href="Branchhotels.aspx">Start a New Search</a>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td style="width: 33px">
                                                                                                                    <img src="Assets/images/arrow.gif" /></td>
                                                                                                                <td>
                                                                                                                    <a class="a-red" href="#" onclick="javscript:window.history.go(-1);">Back To Search
                                                                                                                        Result</a>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </tbody>
                                                                                                    </table>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr id="OverView" onclick="showDivs(this.id);">
                                                                                                <td height="29" class="tab-actv" style="cursor: pointer;">
                                                                                                    &nbsp; Hotel Overview</td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td height="3" class="tab-table">
                                                                                                    <img width="1" height="1" src="moreinfoAssets/images/dot.gif" /></td>
                                                                                            </tr>
                                                                                            <tr id="Rooms" onclick="showDivs(this.id);">
                                                                                                <td height="29" class="tab-oth" style="cursor: pointer;">
                                                                                                    &nbsp; Room Description</td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="tab-table" style="height: 3px">
                                                                                                    <img width="1" height="1" src="moreinfoAssets/images/dot.gif" /></td>
                                                                                            </tr>
                                                                                            <tr id="Attr" onclick="showDivs(this.id);">
                                                                                                <td height="29" class="tab-oth" style="cursor: pointer;">
                                                                                                    &nbsp; Service Attributes</td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td height="3" class="tab-table">
                                                                                                    <img width="1" height="1" src="moreinfoAssets/images/dot.gif" /></td>
                                                                                            </tr>
                                                                                            <tr id="Fac" onclick="showDivs(this.id);">
                                                                                                <td height="29" class="tab-oth" style="cursor: pointer;">
                                                                                                    &nbsp; Facilities</td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td height="3" class="tab-table">
                                                                                                    <img width="1" height="1" src="moreinfoAssets/images/dot.gif" /></td>
                                                                                            </tr>
                                                                                            <tr id="In" onclick="showDivs(this.id);">
                                                                                                <td height="29" class="tab-oth" style="cursor: pointer;">
                                                                                                    &nbsp; In and Around</td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td height="3" class="tab-table">
                                                                                                    <img width="1" height="1" src="moreinfoAssets/images/dot.gif" /></td>
                                                                                            </tr>
                                                                                            <tr id="Photo" onclick="showDivs(this.id);">
                                                                                                <td height="29" class="tab-oth" style="cursor: pointer;">
                                                                                                    &nbsp; Photos</td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td valign="top" class="tab-table">
                                                                                                    <br />
                                                                                                    <table cellspacing="0" cellpadding="2" border="0">
                                                                                                        <tbody>
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    <img src="Assets/images/arrow.gif" /></td>
                                                                                                                <td>
                                                                                                                    <a class="a-red" href="moreinfo.do?link=emailfriend">Email this Hotel to a Friend</a></td>
                                                                                                            </tr>
                                                                                                        </tbody>
                                                                                                    </table>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </tbody>
                                                                                    </table>
                                                                                    <br />
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </td>
                                                                <td valign="top" id="RightCellMoreInfo">
                                                                    <table width="100%" cellspacing="0" cellpadding="0" border="0" id="MoreInfoWidth">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td>
                                                                                    <table width="100%" style="display: none;" cellspacing="0" cellpadding="2" border="0">
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <td class="moreinfo-h1">
                                                                                                    <asp:Label ID="lblHotel_Name" runat="server" />
                                                                                                </td>
                                                                                                <td align="right">
                                                                                                    <table cellspacing="0" cellpadding="2" border="0">
                                                                                                    </table>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td colspn="2">
                                                                                                    <asp:Label ID="lblHotelAddress" runat="server" />
                                                                                                </td>
                                                                                            </tr>
                                                                                        </tbody>
                                                                                    </table>
                                                                                    <table width="100%" style="display: none;" cellspacing="0" cellpadding="2" border="0">
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <td width="85" class="infotbl">
                                                                                                    <strong>Check-in Date</strong></td>
                                                                                                <td class="infotbl">
                                                                                                    Fri, 28 Dec 2007</td>
                                                                                                <td width="90" class="infotbl">
                                                                                                    <strong>Check-out Date</strong></td>
                                                                                                <td class="infotbl">
                                                                                                    Sat, 29 Dec 2007</td>
                                                                                                <td width="30" class="infotbl">
                                                                                                    <strong>Rooms</strong></td>
                                                                                                <td width="25" align="center" class="infotbl">
                                                                                                    1</td>
                                                                                                <td width="30" class="infotbl">
                                                                                                    <strong>Adults</strong></td>
                                                                                                <td width="25" align="center" class="infotbl">
                                                                                                    1</td>
                                                                                            </tr>
                                                                                        </tbody>
                                                                                    </table>
                                                                                    <table style="display: none;" cellspacing="2" cellpadding="2" border="0">
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <img src="Assets/images/bestprice.gif" /></td>
                                                                                                <td class="h4">
                                                                                                    <b>Lowest Average Rate Per Night:</b></td>
                                                                                                <td class="moreinfo-special-rate">
                                                                                                    INR 3783.62</td>
                                                                                            </tr>
                                                                                        </tbody>
                                                                                    </table>
                                                                                    <table style="display: none;" width="100%" cellspacing="0" cellpadding="2" border="0">
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <td align="right">
                                                                                                    <form action="/partners/moreinfo.do" method="post" name="MoreInfoForm">
                                                                                                        <input width="131" type="image" height="25" border="0" src="Assets/images/cntbooking.gif" />
                                                                                                        <input type="hidden" value="00002483" name="strHotelId" />
                                                                                                        <input type="hidden" value="continuebooking" name="link" />
                                                                                                        <input type="hidden" value="Hotel Park Residency" name="HotelName" />
                                                                                                        <input type="hidden" value="3" name="HotelClass" />
                                                                                                        <input type="hidden" value="D-1, Green Park, Ashirwad Building" name="Address1" />
                                                                                                        <input type="hidden" value="South Delhi" name="Address2" />
                                                                                                        <input type="hidden" value="New delhi" name="City" />
                                                                                                        <input type="hidden" value="Delhi" name="State" />
                                                                                                        <input type="hidden" value="110016" name="PinCode" />
                                                                                                        <input type="hidden" value="India" name="Country" />
                                                                                                        <input type="hidden" value="INR" name="Currency" />
                                                                                                        <input type="hidden" value="3783.62" name="LowestAverageRate" />
                                                                                                    </form>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </tbody>
                                                                                    </table>
                                                                                    <div id="divOverView">
                                                                                        <table width="100%" cellspacing="0" cellpadding="0" border="0">
                                                                                            <tbody>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <table width="100%" cellspacing="5" cellpadding="5">
                                                                                                            <tbody>
                                                                                                                <tr>
                                                                                                                    <td colspan="2" class="moreinfo-h2">
                                                                                                                        Hotel Overview
                                                                                                                        <hr size="1" noshade="" class="moreinfo-hr" />
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td valign="top">
                                                                                                                        <img width="111" vspace="5" hspace="10" height="110" border="1" align="left" src="http://209.85.13.194/DesiyaAssets/images/Image/nxd/maw/uyi/lbq/HO.jpg" />
                                                                                                                        <p align="justify" class="bigfont">
                                                                                                                            <%=strDesc%>
                                                                                                                        </p>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </tbody>
                                                                                                        </table>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </div>
                                                                                    <div id="divRooms">
                                                                                        <%=strRooms %>
                                                                                    </div>
                                                                                    <div id="divAttr">
                                                                                    </div>
                                                                                    <div id="divFac">
                                                                                        <%=strFac %>
                                                                                    </div>
                                                                                    <div id="divIn">
                                                                                    </div>
                                                                                    <div id="divPhoto">
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
            <!--Start Footer -->
            <table align="center" width="100%" style="vertical-align: bottom; margin-top: 200px">
                <tr>
                    <td>                        
                        <uc1:BranchFooter ID="BranchFooter1" runat="server" />                        
                    </td>
                </tr>
            </table>
            <!--End Footer -->
        </div>

        <script language="javascript" type="text/javascript">
        <!--        
         hideAll();
         document.getElementById('divOverView').style.display='block';  
        -->
        </script>

    </form>
</body>
</html>
