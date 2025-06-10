<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Agentrecentbookings.aspx.cs" Inherits="SouthernTravelIndiaAgent.Agentrecentbookings" %>


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
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 8.0" />
    <meta name="CODE_LANGUAGE" content="C#" />
    <meta name="vs_defaultClientscript" content="Javascript" />
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
    <link href="../Assets/css/stylesheet.css" type="text/css" rel="stylesheet" />
    <link href="../Assets/css/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <table width="296" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    <table width="296" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td align="left">
                                <img src="Assets/images/left_corner.gif" width="8" height="8" alt="image" /></td>
                            <td bgcolor="#E7E7E7">
                                <img src="Assets/images/trans.gif" width="280" height="1" alt="image" /></td>
                            <td align="right">
                                <img src="Assets/images/rgt_corner.gif" width="8" height="8" alt="image" /></td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="#E7E7E7">
                                &nbsp;</td>
                            <td>
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td height="43" align="center">
                                            <a href="AgentTouratGlance.aspx">
                                                <img src="Assets/images/tour_glance.gif" width="280" height="43" border="0" alt="image" /></a></td>
                                    </tr>
                                </table>
                            </td>
                            <td align="right" bgcolor="#E7E7E7">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="left">
                                <img src="Assets/images/left_d_corner.gif" width="8" height="8" alt="image" /></td>
                            <td bgcolor="#E7E7E7">
                                <img src="Assets/images/trans.gif" width="1" height="1" alt="image" /></td>
                            <td align="right">
                                <img src="Assets/images/rgt_d_corner.gif" width="8" height="8" alt="image" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <img src="Assets/images/trans.gif" width="296" height="6" alt="image" /></td>
            </tr>
        </table>
        <table width="296" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td align="left">
                    <img src="Assets/images/left_corner.gif" width="8" height="8" alt="image" /></td>
                <td bgcolor="#E7E7E7">
                    <img src="Assets/images/trans.gif" width="280" height="1" alt="image" /></td>
                <td align="right">
                    <img src="Assets/images/rgt_corner.gif" width="8" height="8" alt="image" /></td>
            </tr>
            <tr>
                <td align="left" bgcolor="#E7E7E7">
                    &nbsp;</td>
                <td>
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <img src="Assets/images/recent_booking1.gif" width="280" height="43" border="0" alt="image" /></td>
                        </tr>
                        <tr>
                            <td>
                                <table width="100%" border="0" cellspacing="1" cellpadding="0">
                                    <tr>
                                        <td bgcolor="#E7E7E7" class="verdana11bk" valign="top">
                                            <asp:DataGrid ID="dgrecentbookings" runat="server" Width="100%" CssClass="verdana9bk"
                                                AllowPaging="False" PageSize="20" AutoGenerateColumns="False" Font-Bold="False"
                                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False">
                                                <ItemStyle CssClass="verdana9bk" BackColor="#FFFF9C"></ItemStyle>
                                                <HeaderStyle Font-Bold="False" ForeColor="Maroon" CssClass="heads"></HeaderStyle>
                                                <FooterStyle ForeColor="Maroon" CssClass="heads" Font-Bold="False"></FooterStyle>
                                                <Columns>
                                                    <asp:TemplateColumn HeaderText="Ticket No">
                                                        <HeaderStyle HorizontalAlign="Center" Width="15%" Font-Bold="False" Font-Italic="False"
                                                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" BackColor="#FFBF00"
                                                            Wrap="False" CssClass="verdana11bk"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                            Font-Strikeout="False" Font-Underline="False" BackColor="#E7E7E7"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblticketno" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TicketNo") %>'>
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Tour Code">
                                                        <HeaderStyle HorizontalAlign="Center" Width="70%" Font-Bold="False" Font-Italic="False"
                                                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" BackColor="#FFBF00"
                                                            Wrap="False" CssClass="verdana11bk"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                            Font-Strikeout="False" Font-Underline="False" BackColor="#E7E7E7"></ItemStyle>
                                                        <ItemTemplate>
                                                            <a href="#" class="verdana11bk" title="<%#DataBinder.Eval(Container.DataItem,"TourName") %>">
                                                                <%#DataBinder.Eval(Container.DataItem,"Tour_Short_Key") %>
                                                            </a>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Date">
                                                        <HeaderStyle HorizontalAlign="Center" Width="10%" Font-Bold="False" Font-Italic="False"
                                                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" BackColor="#FFBF00"
                                                            CssClass="verdana11bk"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Right" BackColor="#C1C1C1" Font-Bold="False" Font-Italic="False"
                                                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblJourneyDate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"JourneyDate") %>'>
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Pax">
                                                        <HeaderStyle HorizontalAlign="Center" Width="5%" BackColor="#FFBF00" Font-Bold="False"
                                                            Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                                                            CssClass="verdana11bk"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Right" BackColor="#AFAFAF" Font-Bold="False" Font-Italic="False"
                                                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblNoOfSeats" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"NoOfSeats") %>'>
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                                <PagerStyle NextPageText="next" PrevPageText="Prev" CssClass="GridPager" Mode="NumericPages">
                                                </PagerStyle>
                                            </asp:DataGrid>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <img src="Assets/images/trans.gif" width="1" height="2" alt="image" /></td>
                        </tr>
                    </table>
                </td>
                <td align="right" bgcolor="#E7E7E7">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="left">
                    <img src="Assets/images/left_d_corner.gif" width="8" height="8" alt="image" /></td>
                <td bgcolor="#E7E7E7">
                    <img src="Assets/images/trans.gif" width="1" height="1" alt="image" /></td>
                <td align="right">
                    <img src="Assets/images/rgt_d_corner.gif" width="8" height="8" alt="image" /></td>
            </tr>
        </table>
    </form>
</body>
</html>
