<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgentCarDuplicateTicket.aspx.cs" Inherits="SouthernTravelIndiaAgent.AgentCarDuplicateTicket" %>

<%@ Register TagPrefix="uc1" TagName="AgentHeader" Src="UserControls/UcAgentHeader.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AgentFooter" Src="UserControls/UcAgentFooter.ascx" %>

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
    <meta name="CODE_LANGUAGE" content="C#" />
    <meta name="vs_defaultClientscript" content="Javascript" />
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
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
        height: 194px;">
    </div>
    <form id="Form1" method="post" runat="server">
        <table border="0" cellspacing="0" cellpadding="0" width="100%">
            <tr>
                <td colspan="2">
                <uc1:AgentHeader ID="AgentHeader" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <table width="95%" cellspacing="0" cellpadding="0" border="0" align="center">
                        <tr>
                            <td bgcolor="#E7E7E7" class="verdana11bk">
                                <table width="100%" cellspacing="0" cellpadding="0" border="0" align="center">
                                    <tr>
                                        <td align="left">
                                            <img width="8" height="8" src="Assets/images/left_corner.gif" alt="image" /></td>
                                        <td bgcolor="#e7e7e7">
                                            <img width="380" height="1" src="Assets/images/trans.gif" alt="image" /></td>
                                        <td align="right">
                                            <img width="8" height="8" src="Assets/images/rgt_corner.gif" alt="image" /></td>
                                    </tr>
                                    <tr>
                                        <td width="8" bgcolor="#E7E7E7">
                                            &nbsp;</td>
                                        <td valign="top" bgcolor="#ffffff">
                                            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td>
                                                        <table width="250" border="0" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td>
                                                                    <table width="250" border="0" cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <td width="13" height="42" background="Assets/images/left_.gif">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td background="Assets/images/bg1.gif" class="verdana14w">
                                                                                Agent Car Duplicate Ticket
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
                                                                    <img src="/Assets/images/bottom.gif" width="138" height="1" /></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <img src="Assets/images/trans.gif" width="1" height="1" /></td>
                                                </tr>
                                                <tr style="background-color: #348DE7">
                                                    <td class="cgi1">
                                                        &nbsp;PNR No:</td>
                                                    <td class="cgi1">
                                                        Ticket No:</td>
                                                    <td class="cgi1">
                                                        Contact No:</td>
                                                    <td class="cgi1">
                                                        Journey Date</td>
                                                    <td class="cgi1">
                                                        Email:</td>
                                                    <td class="cgi1">
                                                    </td>
                                                </tr>
                                                <tr style="background-color: #348DE7; height: 30px;">
                                                    <td>
                                                        &nbsp;<input name="txtPnrNo" type="text" class="verdana11bk" runat="server" id="txtPnrNo" />
                                                    </td>
                                                    <td>
                                                        <input name="txtticketno" type="text" class="verdana11bk" runat="server" id="txtticketno" />
                                                    </td>
                                                    <td>
                                                        <input name="txtcontact" type="text" class="verdana11bk" runat="server" id="txtContact" />
                                                    </td>
                                                    <td>
                                                        <input name="txtjdate" type="text" class="verdana11bk" runat="server" id="txtjdate"
                                                            onfocus="objCal('JICON','txtjdate','390','160');" readonly="readonly" /><a href="javascript:void(null)"
                                                                onclick="objCal('JICON','txtjdate','390','160');"><img src="Assets/images/calendar.gif"
                                                                    border="0" id="JICON" tabindex="-1" style="padding-bottom: 0px; visibility: visible;" /></a>
                                                    </td>
                                                    <td>
                                                        <input name="txtEmail" type="text" class="verdana11bk" runat="server" id="txtEmail" />
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="Submit" Text="Search" runat="server" BackColor="#5aa1ea" 
                                                            CssClass="cgi1" onclick="Submit_Click"
                                                             />
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
                                                            DataKeyField="TicketNo" AutoGenerateColumns="false" 
                                                           AllowPaging="true" PageSize="20" 
                                                            onitemdatabound="dgDuplicateTickets_ItemDataBound" 
                                                            onpageindexchanged="dgDuplicateTickets_PageIndexChanged">
                                                            <HeaderStyle CssClass="verdana11bkb bgheader" Height="24px" />
                                                            <AlternatingItemStyle CssClass="bg1 verdana11bk" />
                                                            <ItemStyle CssClass="verdana11bk" />
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
                                                                    <ItemStyle HorizontalAlign="Left" Width="16%"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <a href="AgentCarTicket.aspx?orderid=<%#DataBinder.Eval(Container.DataItem,"CabId") %>&duplicate=cabdup">
                                                                            <asp:Label ID="lblticketno" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CabId") %>'></asp:Label></a>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:BoundColumn DataField="TicketNo" HeaderText="TicketCode">
                                                                    <ItemStyle Width="12%" HorizontalAlign="Left" />
                                                                </asp:BoundColumn>
                                                                <asp:BoundColumn DataField="PickTupTime" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Journey Date">
                                                                    <ItemStyle Width="11%" HorizontalAlign="Left" />
                                                                </asp:BoundColumn>
                                                                <asp:BoundColumn DataField="BookedOn" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Booking Date">
                                                                    <ItemStyle Width="15%" HorizontalAlign="Left" />
                                                                </asp:BoundColumn>
                                                                <asp:BoundColumn DataField="TourName" HeaderText="Tour Name">
                                                                    <ItemStyle Width="15%" HorizontalAlign="Left" />
                                                                </asp:BoundColumn>
                                                                <asp:TemplateColumn HeaderText="Transaction Type">
                                                                    <HeaderStyle HorizontalAlign="Center" Width="15%"></HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                </asp:TemplateColumn>
                                                                <asp:BoundColumn DataField="NoOfPax" HeaderText="No of Pax">
                                                                    <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                </asp:BoundColumn>
                                                             
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
                                            <img width="8" height="8" src="Assets/images/left_d_corner.gif" alt="image" /></td>
                                        <td bgcolor="#e7e7e7">
                                            <img width="1" height="1" src="Assets/images/trans.gif" alt="image" /></td>
                                        <td align="right">
                                            <img width="8" height="8" src="Assets/images/rgt_d_corner.gif" alt="image" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <input id="tmpDelete" type="hidden" name="tmpDelete" runat="server" style="width: 77px">
        </table>
    </form>
</body>
</html>
