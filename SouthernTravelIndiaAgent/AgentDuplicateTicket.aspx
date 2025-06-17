<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgentDuplicateTicket.aspx.cs" Inherits="SouthernTravelIndiaAgent.AgentDuplicateTicket" %>

<%@ Register TagPrefix="uc1" TagName="AgentHeader" Src="UserControls/UcAgentHeader.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AgentFooter" Src="UserControls/UcAgentFooter.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 Transitional//EN" "http://www.w3.org/tr/xhtml1/Dtd/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Southern India Travel,South India Travel Packages,Travel Packages to South India</title>    
    <link href="../Assets/css/style.css" rel="stylesheet" type="text/css" />
    <link href="../Assets/css/stylesheet.css" type="text/css" rel="stylesheet" />
    <link href="../Assets/css/calendar.css" rel="stylesheet" type="text/css" />
    <script language="javascript" src="../Assets/js/ST_calendar.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
      function isConfirm()
	  {			    
		var counter=0;
		for(i=0;i<=document.Form1.elements.length-1;i++)
		{
			if(document.Form1.elements[i].type=="checkbox" && document.Form1.elements[i].checked==true)
			{
			  counter++;
			}
		}

		if (counter > 0)
		{
			var confirm1=confirm("Are you sure you want to send the mail to selected PNR(s)?");
			if(confirm1)
				return true;
			else
				return false;
		}
		else
		{
			alert("No record has been selected!");
			return false;
		}
	}
	function fnDelete()
	 {
	 		   
			if(isConfirm()==true)
			{
			    document.getElementById("tmpDelete").value=1;
			 
			    document.Form1.submit();
			    return true;
			}
		   return false;				
	 }
    
    
    </script>

</head>
<body>
    <div id="STContainer" style="display: none; position: absolute">
    </div>
    <div id="framediv" style="z-index: 1; left: -999px; width: 148px; position: absolute;
        height: 194px;" frameborder="0" scrolling="no">
    </div>
    <form id="Form1" method="post" runat="server">
        <table border="0" cellspacing="0" cellpadding="0" width="100%">
            <tr>
                <td colspan="3">
                    <uc1:AgentHeader ID="agentHeader" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="width: 2%">
                </td>
                <td bgcolor="#E7E7E7" class="verdana11bk" style="width: 96%">
                    <table width="100%" cellspacing="0" cellpadding="0" border="0">
                        <tr>
                            <td align="left">
                                <img width="8" height="8" src="Assets/images/left_corner.gif" loading="lazy" alt="left_corner" /></td>
                            <td bgcolor="#e7e7e7">
                                <img width="380" height="1" src="Assets/images/trans.gif" loading="lazy" alt="trans" /></td>
                            <td align="right">
                                <img width="8" height="8" src="Assets/images/rgt_corner.gif" loading="lazy" alt="rgt_corner" /></td>
                        </tr>
                        <tr>
                            <td width="8" bgcolor="#E7E7E7">
                                &nbsp;</td>
                            <td valign="top" bgcolor="#ffffff">
                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td>
                                            <table width="200" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td>
                                                        <table width="200" border="0" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td width="13" height="42" background="Assets/images/left_.gif">
                                                                    &nbsp;
                                                                </td>
                                                                <td background="Assets/images/bg1.gif" class="verdana14w">
                                                                    Agent Duplicate Ticket
                                                                </td>
                                                                <td width="13" height="42" background="Assets/images/right_.gif">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <img src="/Assets/images/bottom.gif" width="138" height="1" loading="lazy" alt="bottom" /></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr style="background-color: #348DE7">
                                        <td class="cgi1">
                                            &nbsp;PNR No:
                                        </td>
                                        <td class="cgi1">
                                            Ticket No:
                                        </td>
                                        <td class="cgi1">
                                            Contact No:
                                        </td>
                                        <td class="cgi1">
                                            Journey Date
                                        </td>
                                        <td class="cgi1">
                                            Email:
                                        </td>
                                        <td class="cgi1">
                                        </td>
                                    </tr>
                                    <tr style="background-color: #348DE7; height: 30px;">
                                        <td>
                                            <input name="txtPnrNo" type="text" class="verdana11bk" runat="server" id="txtPnrNo" />
                                        </td>
                                        <td>
                                            <input name="txtticketno" type="text" class="verdana11bk" runat="server" id="txtticketno" />
                                        </td>
                                        <td>
                                            <input name="txtContact" type="text" class="verdana11bk" runat="server" id="txtContact" />
                                        </td>
                                        <td>
                                            <input name="txtjdate" type="text" class="verdana11bk" readonly="readonly" runat="server"
                                                id="txtjdate" onfocus="objCal('JICON','txtjdate','390','160');" /><a href="javascript:void(null)"
                                                    onclick="objCal('JICON','txtjdate','390','160');" hidefocus=""><img src="Assets/images/calendar.gif"
                                                        border="0" id="JICON" tabindex="-1" style="padding-bottom: 0px; visibility: visible;"  loading="lazy" alt="calendar"/></a>
                                        </td>
                                        <td>
                                            <input name="txtEmail" type="text" class="verdana11bk" runat="server" id="txtEmail" />
                                        </td>
                                        <td>
                                            <asp:Button ID="Submit" Text="Search" runat="server" BackColor="#5aa1ea" CssClass="cgi1"
                                                OnClick="Submit_Click" />
                                        </td>
                                    </tr>

                                    <script language="javascript" type="text/javascript">
                                        stObj = new SOUTHERN.caldoy.Calendar2up("stObj","STContainer",(thisMonth+1)+"/"+thisYear,(thisMonth+1)+"/"+thisDay+"/"+thisYear);
                                        stObj.title = " &nbsp;&nbsp;&nbsp;&nbsp;Select Date &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                        stObj.setChildFunction("onSelect",setDate);
                                        stObj.render();	
                                    </script>

                                    <tr>
                                        <td colspan="6" valign="top">
                                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                            <asp:DataGrid ID="dgDuplicateTickets" runat="server" Width="100%" CssClass="verdana11bk"
                                                DataKeyField="Ticketcode" AutoGenerateColumns="False" OnPageIndexChanged="dgDuplicateTickets_PageIndexChanged"
                                                PageSize="20" AllowPaging="true" OnItemDataBound="dgDuplicateTickets_ItemDataBound">
                                                <HeaderStyle CssClass="verdana11bkb bgheader" Height="24px" HorizontalAlign="Center" />
                                                <AlternatingItemStyle CssClass="bg1 verdana11bk" />
                                                <ItemStyle CssClass="verdana11bk" HorizontalAlign="left" />
                                                <Columns>
                                                    <asp:TemplateColumn>
                                                        <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                                        <HeaderTemplate>
                                                            <asp:Button ID="btnAccept" runat="server" Text="Send Mail" OnClientClick="javascript:return fnDelete();" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkAccept" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="PNR Number">
                                                        <HeaderStyle HorizontalAlign="Center" Width="15%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        <ItemTemplate>
                                                            <a href="AgentTicket.aspx?orderid=<%#DataBinder.Eval(Container.DataItem,"orderid") %>&Ticket=kps">
                                                                <asp:Label ID="lblticketno" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"orderid") %>'></asp:Label></a>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:BoundColumn DataField="Ticketcode" HeaderText="TicketCode">
                                                        <ItemStyle Width="15%" HorizontalAlign="left" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="doj" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Journey Date">
                                                        <ItemStyle Width="15%" HorizontalAlign="left" />
                                                    </asp:BoundColumn>
                                                    <asp:TemplateColumn HeaderText="Group Leader">
                                                        <HeaderStyle HorizontalAlign="Center" Width="15%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblGroupLeader" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"groupleader") %>'>
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:BoundColumn DataField="Dob" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Booking Date">
                                                        <ItemStyle Width="15%" HorizontalAlign="Center" />
                                                    </asp:BoundColumn>
                                                    <asp:TemplateColumn HeaderText="Transaction Type">
                                                        <HeaderStyle HorizontalAlign="Center" Width="15%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="No of Seats">
                                                        <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblNoOfSeats" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"noofseats") %>'>
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
                            <td width="8" bgcolor="#E7E7E7">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="left">
                                <img width="8" height="8" src="Assets/images/left_d_corner.gif" alt="image"   loading="lazy" /></td>
                            <td bgcolor="#e7e7e7">
                                <img width="1" height="1" src="Assets/images/trans.gif" alt="image" loading="lazy"/></td>
                            <td align="right">
                                <img width="8" height="8" src="Assets/images/rgt_d_corner.gif" alt="image" loading="lazy"/></td>
                        </tr>
                    </table>
                </td>
                <td style="width: 2%">
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <uc1:AgentFooter ID="agentfooter" runat="server" />
                </td>
            </tr>
        </table>
        <input id="tmpDelete" type="hidden" name="tmpDelete" runat="server" style="width: 77px" />
    </form>
</body>
</html>
