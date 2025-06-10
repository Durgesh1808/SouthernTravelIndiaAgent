<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgentTouratGlance.aspx.cs" Inherits="SouthernTravelIndiaAgent.AgentTouratGlance" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Southern Travels:-: Agent Tour  at a Glance</title>    
    <link href="../Assets/css/stylesheet.css" type="text/css" rel="stylesheet" />
    <link href="../Assets/css/style.css" rel="stylesheet" type="text/css" />
    <link href="../Assets/css/Calenderall.css" rel="Stylesheet" type="text/css" />
    <script language="javascript" src="../Assets/js/calenderall.js" type="text/javascript"></script>  

</head>
<body>
  
    <form id="agenttourglance" runat="server">
        <table width="296" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td colspan="3">
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
                                            <a href="Agentrecentbookings.aspx">
                                                <img src="Assets/images/recent_booking.gif" width="280" height="43" border="0" alt="image" /></a></td>
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
                <td colspan="3">
                    <img src="Assets/images/trans.gif" width="296" height="6" alt="image" /></td>
            </tr>
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
                                <img src="Assets/images/tour_glance1.gif" width="280" height="43" border="0" alt="image" /></td>
                        </tr>
                        <tr>
                            <td height="24" align="left" class="verdana11bk">
                                &nbsp;<input id="date" type="text" size="8" name="txtdate" runat="server" maxlength="10"
                                    readonly onclick="showCalendarControl(this);" />
                                <img id="Img1" style="cursor: hand" onclick="showCalendarControl(document.getElementById('date'));"
                                    alt="View calendar" src="Assets/images/calendar.gif" value="Calendar" />
                                <asp:DropDownList ID="ddlBranchName" runat="server" DataValueField="Branchcode" Width="120"
                                    DataTextField="Branchname">
                                    <asp:ListItem Text="-----All-----" Selected="True">
                                                
                                    </asp:ListItem>
                                </asp:DropDownList>
                                <asp:Button CssClass="btn" ID="btngo" runat="server" Text="Go" OnClick="btngo_Click">
                                </asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table width="100%" height="293" border="0" cellspacing="1" cellpadding="0" bgcolor="#ffffff">
                                    <tr>
                                        <td class="verdana11bk" valign="top">
                                            <asp:Label ID="lblMsg" runat="server" CssClass="spl"></asp:Label>
                                            <asp:DataGrid ID="dgglance" runat="server" Width="100%" CssClass="verdana9bk" AllowPaging="True"
                                                PageSize="15" AutoGenerateColumns="False" OnPageIndexChanged="dgglance_PageIndexChanged"
                                                Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False">
                                                <ItemStyle CssClass="verdana9bk" BackColor="#FFFF9C"></ItemStyle>
                                                <HeaderStyle Font-Bold="False" ForeColor="Maroon" CssClass="heads"></HeaderStyle>
                                                <FooterStyle ForeColor="Maroon" CssClass="heads" Font-Bold="False"></FooterStyle>
                                                <Columns>
                                                    <asp:TemplateColumn HeaderText="TourName">
                                                        <HeaderStyle HorizontalAlign="Center" Width="75%" Font-Bold="False" Font-Italic="False"
                                                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" BackColor="#FD6500"
                                                            Wrap="False" CssClass="verdana11w"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                            Font-Strikeout="False" Font-Underline="False" BackColor="#E7E7E7"></ItemStyle>
                                                        <ItemTemplate>
                                                            <a target="_top" href='agenttourbooking.aspx?tourid=<%#DataBinder.Eval(Container.DataItem,"TourId") %>&date1=<%=strDate%>'>
                                                                <%#DataBinder.Eval(Container.DataItem, "TourName")%>
                                                            </a>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Buses">
                                                        <HeaderStyle HorizontalAlign="Center" Width="13%" Font-Bold="False" Font-Italic="False"
                                                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" BackColor="#FD6500"
                                                            CssClass="verdana11w"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Right" BackColor="#C1C1C1" Font-Bold="False" Font-Italic="False"
                                                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnoofbuses" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Buses") %>'>
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Vacancy">
                                                        <HeaderStyle HorizontalAlign="Center" Width="12%" BackColor="#FD6500" Font-Bold="False"
                                                            Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                                                            CssClass="verdana11w"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Right" BackColor="#AFAFAF" Font-Bold="False" Font-Italic="False"
                                                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="vacent" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Vacancy") %>'>
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
                                <img src="Assets/images/trans.gif" width="1" height="7" alt="image" /></td>
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
