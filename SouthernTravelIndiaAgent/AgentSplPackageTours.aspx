<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgentSplPackageTours.aspx.cs" Inherits="SouthernTravelIndiaAgent.AgentSplPackageTours" %>

<%@ Register TagPrefix="uc1" TagName="agentheader" Src="UserControls/UcAgentHeader.ascx" %>
<%@ Register TagPrefix="uc1" TagName="agentFooter" Src="UserControls/UcAgentFooter.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Southern India Travel,South India Travel Packages,Travel Packages to South India</title>
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

    <script language="javascript" src="../Assets/js/MyScript.js" type="text/javascript"></script>

    <script language="javascript" src="../Assets/js/md5.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <table width="1001" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <uc1:agentheader ID="agHeader" runat="server" />
                </td>
            </tr>
        </table>
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td style="width: 100%">
                    <table width="80%" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="left">
                                <img src="Assets/images/left_corner.gif" width="8" height="8" alt="left_corner"  loading="lazy"/></td>
                            <td bgcolor="#E7E7E7">
                                <img src="Assets/images/trans.gif" height="1" alt="trans" loading="lazy" /></td>
                            <td align="right">
                                <img src="Assets/images/rgt_corner.gif" width="8" height="8" alt="rgt_corner"  loading="lazy"/></td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="#E7E7E7" width="8">
                            </td>
                            <td bgcolor="#E7E7E7">
                                <div style="padding-left: 15px; padding-right: 15px; background-color: #ffffff">
                                    <table cellspacing="0" cellpadding="0" style="width: 100%" border="0">
                                        <tbody>
                                            <tr>
                                                <td style="background-color: #5aa1ea; height: 25px;">
                                                    <div class="heading" style="color: #ffffff; text-align: center">
                                                        <a id="Special Package Tours"></a><b class="cgi1">Special Tour Packages (By Car,Qualis,Innova,Tempo)</b></div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="30">
                                                </td>
                                            </tr>
                                            <tr id="trNtour" runat="server" visible="false">
                                                <td style="background-color: #5aa1ea">
                                                    <div class="heading" style="color: #ffffff; text-align: center">
                                                        <b class="cgi1">NORTH INDIA</b></div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100%">
                                                    <div class="hlinks">
                                                        <p>
                                                            <asp:DataList ID="dlToursN" runat="server" DataMember="" Width="100%">
                                                                <ItemTemplate>
                                                                    <img height="18" src="Assets/images/bullet.gif" style="width: 11" alt="bullet"  loading="lazy"/>
                                                                    <a href="agentspecialtour.aspx?tourid=<%#DataBinder.Eval(Container.DataItem, "Tourid")%>">
                                                                        <%#DataBinder.Eval(Container.DataItem, "TourName")%>
                                                                    </a>(<%#DataBinder.Eval(Container.DataItem, "Duration")%>)<br />
                                                                </ItemTemplate>
                                                            </asp:DataList>
                                                        </p>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr id="trStour" runat="server" visible="false">
                                                <td style="background-color: #5aa1ea">
                                                    <div class="heading" style="color: #ffffff; text-align: center">
                                                        <b class="cgi1">SOUTH INDIA</b></div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="hlinks">
                                                        <p>
                                                            <asp:DataList ID="dlToursS" runat="server" DataMember="" Width="100%">
                                                                <ItemTemplate>
                                                                    <img height="18" src="Assets/images/bullet.gif" style="width: 11" alt="bullet" loading="lazy"/>
                                                                    <a href="agentspecialtour.aspx?tourid=<%#DataBinder.Eval(Container.DataItem, "Tourid")%>">
                                                                        <%#DataBinder.Eval(Container.DataItem, "TourName")%>
                                                                    </a>(<%#DataBinder.Eval(Container.DataItem, "Duration")%>)<br />
                                                                </ItemTemplate>
                                                            </asp:DataList>
                                                        </p>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr id="trWtour" runat="server" visible="false">
                                                <td style="background-color: #5aa1ea">
                                                    <div class="heading" style="color: #ffffff; text-align: center">
                                                        <b class="cgi1">WESTERN INDIA TOURS</b></div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="hlinks">
                                                        <p>
                                                            <asp:DataList ID="dlToursW" runat="server" DataMember="" Width="100%">
                                                                <ItemTemplate>
                                                                    <img height="18" src="Assets/images/bullet.gif" style="width: 11" alt="bullet" loading="lazy"/>
                                                                    <a href="agentspecialtour.aspx?tourid=<%#DataBinder.Eval(Container.DataItem, "Tourid")%>">
                                                                        <%#DataBinder.Eval(Container.DataItem, "TourName")%>
                                                                    </a>(<%#DataBinder.Eval(Container.DataItem, "Duration")%>)<br />
                                                                </ItemTemplate>
                                                            </asp:DataList>
                                                        </p>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr id="trEtour" runat="server" visible="false">
                                                <td style="background-color: #5aa1ea">
                                                    <div class="heading" style="color: #ffffff; text-align: center">
                                                        <b class="cgi1">EASTERN INDIA TOURS</b></div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="hlinks">
                                                        <p>
                                                            <asp:DataList ID="dlToursE" runat="server" DataMember="" Width="100%">
                                                                <ItemTemplate>
                                                                    <img height="18" src="Assets/images/bullet.gif" style="width: 11" alt="bullet" loading="lazy" />
                                                                    <a href="agentspecialtour.aspx?tourid=<%#DataBinder.Eval(Container.DataItem, "Tourid")%>">
                                                                        <%#DataBinder.Eval(Container.DataItem, "TourName")%>
                                                                    </a>(<%#DataBinder.Eval(Container.DataItem, "Duration")%>)<br />
                                                                </ItemTemplate>
                                                            </asp:DataList>
                                                        </p>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: top; width: 158">
                                                </td>
                                                <td style="width: 100%">
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </td>
                            <td align="right" bgcolor="#E7E7E7" width="8">
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <img src="Assets/images/left_d_corner.gif" width="8" height="8" loading="lazy" alt="left_d_corner" /></td>
                            <td bgcolor="#E7E7E7">
                                <img src="Assets/images/trans.gif" width="1" height="1" loading="lazy" alt="trans" /></td>
                            <td align="right">
                                <img src="Assets/images/rgt_d_corner.gif" width="8" height="8" loading="lazy" alt="rgt_d_corner" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <uc1:agentFooter ID="footer1" runat="server" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
