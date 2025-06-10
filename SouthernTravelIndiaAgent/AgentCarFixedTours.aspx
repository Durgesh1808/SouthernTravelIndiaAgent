<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgentCarFixedTours.aspx.cs" Inherits="SouthernTravelIndiaAgent.AgentCarFixedTours" %>

<%@ Register TagPrefix="uc1" TagName="AgentHeader" Src="UserControls/UcAgentHeader.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AgentFooter" Src="UserControls/UcAgentFooter.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Southern India Travel,South India Travel Packages,Travel Packages to South India
    </title>
    <link href="../Assets/css/style.css" rel="stylesheet" type="text/css" />
    <link href="../Assets/css/stylesheet.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
        .style2
        {
            height: 12px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="1001" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <uc1:AgentHeader ID="agentHeader" runat="server" />
                </td>
            </tr>
        </table>
        <table width="95%" border="0" align="center" cellpadding="0" cellspacing="0">
           <tr>
                <td width="8" align="left">
                    <img  loading="lazy" alt="left_corner" src="Assets/images/left_corner.gif" width="8" height="8" /></td>
                <td width="799" bgcolor="#E7E7E7">
                    <img  loading="lazy" alt="trans" src="Assets/images/trans.gif" width="1" height="1" /></td>
                <td width="8" align="right">
                    <img  loading="lazy" alt="rgt_corner" src="Assets/images/rgt_corner.gif" width="8" height="8" /></td>
            </tr>
            <tr>
                <td bgcolor="#E7E7E7">
                    &nbsp;
                </td>
                <td>       
                    <!--from here to-->
                    <%--<table width="650" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td width="650" height="35" align="center" valign="middle">--%>
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td width="8" align="left">
                                            <img  loading="lazy" alt="left_" src="Assets/images/left_.gif" width="13" height="42" /></td>
                                        <td width="98%" align="center" background="Assets/images/bg1.gif" class="verdana14w">
                                            Car Fixed Tours
                                        </td>
                                        <td width="8" align="right">
                                            <img  loading="lazy" alt="right_" src="Assets/images/right_.gif" width="13" height="42" /></td>
                                    </tr>
                                    <tr style="border: 0; border-color: White; background-color: White;">
                                        <td align="center" colspan="3">
                                            <strong><span style="color: #ff0000; font-family: arial;" runat="server" id="lblMsg">
                                            </span></strong>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="42" colspan="3" align="center" valign="middle" bgcolor="#FFFFFF">
                                            <div>
                                                <asp:DataList ID="dg_list" runat="server" RepeatColumns="1" Width="95%" RepeatDirection="Vertical">
                                                    <ItemTemplate>
                                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td style="font-family: Verdana, Arial, Helvetica; line-height: 1.4; color: #000000;
                                                                    font-size: 12px" align="left">
                                                                    <b>
                                                                        <%#DataBinder.Eval(Container.DataItem, "TourName")%></b>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" style="font-family: Verdana, Arial, Helvetica; line-height: 1.4;
                                                                    color: #000000; font-size: 11px">
                                                                    <%#DataBinder.Eval(Container.DataItem, "TourDescription")%>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="font-family: Verdana, Arial, Helvetica; line-height: 1.4; color: #000000;
                                                                    font-size: 12px" align="left">
                                                                    <b>Duration:</b>
                                                                    <%#DataBinder.Eval(Container.DataItem, "DurationDay")%>/<%#DataBinder.Eval(Container.DataItem, "DurationNight")%>)
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="font-family: Verdana, Arial, Helvetica; line-height: 1.4; color: #000000;
                                                                    font-size: 12px" align="left">
                                                                    <b>Price: </b>Rs.<%#DataBinder.Eval(Container.DataItem, "Amount")%>/- &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" class="style2">
                                                                    &nbsp;&nbsp;&nbsp;<a href="agentCarMultiple.aspx?tourno=<%#DataBinder.Eval(Container.DataItem,"TourNo")%>&strs=<%=sfixed %>"><img  loading="lazy" alt="book_now"
                                                                        src="Assets/images/book_now.gif" style="border: 0;" /></a>
                                                                </td>
                                                            </tr>
                                                            <tr style="height:1px"><td bgcolor="#cccccc"></td></tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                           <%-- </td>
                        </tr>
                    </table>--%>
                    <!-- upto here-->
               </td>
                <td bgcolor="#E7E7E7">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="left">
                    <img  loading="lazy" alt="left_d_corner" src="Assets/images/left_d_corner.gif" width="8" height="8" /></td>
                <td bgcolor="#E7E7E7">
                    <img  loading="lazy" alt="trans" src="Assets/images/trans.gif" width="1" height="1" /></td>
                <td align="right">
                    <img  loading="lazy" alt="rgt_d_corner" src="Assets/images/rgt_d_corner.gif" width="8" height="8" /></td>
            </tr>
            <tr>
                <td colspan="3" valign="bottom">
                <br />
                    <uc1:AgentFooter ID="AgentFooter1" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
