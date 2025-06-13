<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="agenttransactiondetails.aspx.cs" Inherits="SouthernTravelIndiaAgent.agenttransactiondetails" %>


<%@ Register TagPrefix="uc1" TagName="agentheader" Src="UserControls/UcAgentHeader.ascx" %>
<%@ Register TagPrefix="uc2" TagName="agentFooter" Src="UserControls/UcAgentFooter.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Southern Travels :-: Agent Transaction</title>
    <link href="../Assets/css/style.css" rel="stylesheet" type="text/css" />
    <link href="../Assets/css/stylesheet.css" rel="stylesheet" type="text/css" />
    <link href="../Assets/css/Calenderall.css" rel="Stylesheet" type="text/css" />
    <script language="javascript" src="../Assets/js/calenderall.js" type="text/javascript"></script>  
    <script language="javascript" type="text/javascript">
    
    function validation()
    {
    
        var st=document.getElementById("txtFromDate");	   
	    var et=document.getElementById("txtToDate");	    
	    if((st.value=="")&&(et.value!="") )
         {
              alert("Please enter the Date Range");
              return false;
         }
	    
        if((st.value!="")&&(et.value==""))
         {
              alert("Please enter the Date Range");
              return false;
         }
         
         if((st.value!="")&&(et.value!="")&&(st.value.length < 8))
         {
              alert("Please enter in dd/mm/yyyy format");
              return false;
         }
         if((st.value!="")&&(et.value!="")&&(et.value.length < 8))
         {
              alert("Please enter in dd/mm/yyyy format");
              return false;
         }
       
         
	         else
	         {
	                  var dt=new Date();
	                  var d=dt.getDate();
	                  var m=dt.getMonth()+1;
	                  var y=dt.getFullYear();
	                  dt=m+"/"+d+"/"+y;
    	          
	                var s=new String();
                    s=st.value;
                    var first=s.indexOf("/",1);
                    var second=s.indexOf("/",first+1);
                    var d1=new String();
                    d1=s.substr(first+1,second-first-1)+"/"+s.substr(0,first)+"/"+s.substr(second+1,4);
                    
                    s=et.value;
                    var first=s.indexOf("/",1);
                    var second=s.indexOf("/",first+1);
                    var d2=new String();
                    d2=s.substr(first+1,second-first-1)+"/"+s.substr(0,first)+"/"+s.substr(second+1,4);
                
                    if( Date.parse(d1) > Date.parse(d2) )
                    {
	                     alert("Invalid Date Range. Reason: From Date is greater than To Date.");
         	             return false;
	                }
	                else if( Date.parse(dt) < Date.parse(d1) )
	                {
	                     alert("Invalid Date Range. Reason: Current Date is less than From Date.");
         	             return false;
    	            
	                }
	                else if( Date.parse(dt) < Date.parse(d2) ) 
	                {
	                     alert("Invalid Date Range. Reason: To Date is greater than Current Date.");
         	             return false;
	                }  
	       
	         }
            
        return true;
   }
   
 function keyboardlock()
   {
       return false;
   }
   
    function MakeReadOnly()
    {
        document.getElementById('txtFromDate').readOnly = true;
        document.getElementById('txtToDate').readOnly = true;
    }
    </script>

</head>
<body onload="MakeReadOnly();">
   
    <form id="form1" runat="server">
        <div>
            <table width="1001" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <uc1:agentheader ID="agHeader" runat="server" />
                    </td>
                </tr>
            </table>
            <table width="982" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td align="left" valign="top">
                        <table width="680" height="449" border="0" cellpadding="0" cellspacing="0">
                            <tr height="8">
                                <td align="left">
                                    <img src="Assets/images/left_corner.gif" width="8" height="8" /></td>
                                <td bgcolor="#E7E7E7">
                                    <img src="Assets/images/trans.gif" width="1" height="1" /></td>
                                <td align="right">
                                    <img src="Assets/images/rgt_corner.gif" width="8" height="8" /></td>
                            </tr>
                            <tr height="433">
                                <td align="left" bgcolor="#E7E7E7">
                                    &nbsp;</td>
                                <td valign="top">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td width="283">
                                                <table width="200" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td width="13">
                                                            <img src="Assets/images/left_.gif" width="13" height="42" /></td>
                                                        <td align="center" background="Assets/images/bg1.gif" class="verdana14w">
                                                            Agent Transactions
                                                        </td>
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
                                            <td align="left">
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td height="38" align="left" background="Assets/images/bg1.gif" class="verdana11bk">
                                                            <img src="Assets/images/trans.gif" width="5" height="1" /></td>
                                                        <td align="left" background="Assets/images/bg1.gif" class="cgi1">
                                                            From Date &nbsp;&nbsp;
                                                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="verdana11bk" size="15"  onclick="showCalendarControl(this);"></asp:TextBox>
                                                            <img id="btn1" style="cursor: hand" alt="View calendar" src="Assets/images/calendar.gif"
                                                               onclick="showCalendarControl(document.getElementById('txtFromDate'));">
                                                        </td>
                                                        <td align="left" background="Assets/images/bg1.gif" class="cgi1">
                                                            To Date&nbsp;&nbsp;
                                                            <asp:TextBox ID="txtToDate" runat="server" CssClass="verdana11bk" size="15"  onclick="showCalendarControl(this);"></asp:TextBox>
                                                            <img id="btn2" style="cursor: hand" alt="View calendar" src="Assets/images/calendar.gif"
                                                               onclick="showCalendarControl(document.getElementById('txtToDate'));">
                                                        </td>
                                                        <td align="left" background="Assets/images/bg1.gif" class="cgi1">
                                                            Trans Type&nbsp;
                                                            <asp:DropDownList ID="ddlTrans" runat="server" Width="200" DataTextField="details"
                                                                DataValueField="TransactionTypeId">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="left" background="Assets/images/bg1.gif" class="verdana11bk">
                                                            <asp:ImageButton ID="btnSubmit" runat="server" ImageUrl="Assets/images/submit_blue1.jpg"
                                                                Width="67" Height="20" border="0" OnClick="btnSubmit_Click" /></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <table width="660" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td height="5">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <table width="970" border="0" cellspacing="1" cellpadding="0">
                                                    <tr class="bg">
                                                        <td class="verdana11bk">
                                                            <asp:Label ID="lblMsg" runat="server"></asp:Label></td>
                                                    </tr>
                                                    <tr class="bg" runat="server" id="trGrossTot">
                                                        <td>
                                                            <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                                                <tr>
                                                                    <td width="50%" align="right">
                                                                        <asp:Label ID="lblCary" Text="" runat="server" CssClass="verdana11bk" Font-Bold="true"></asp:Label></td>
                                                                    <td width="50%" align="left">
                                                                        <asp:Label ID="lblCaryFwd" Text="" runat="server" CssClass="verdana11bk" Font-Bold="true"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr class="bg">
                                                        <td align="center">
                                                            <asp:DataGrid ID="dgrReports" runat="server" AutoGenerateColumns="False" DataKeyField="RowId"
                                                                CssClass="verdana11bk" Width="100%" AllowPaging="True" OnItemDataBound="dgrReports_ItemDataBound"
                                                                OnPageIndexChanged="dgrReports_PageIndexChanged" PageSize="55">
                                                                <HeaderStyle CssClass="verdana11bkb bgheader" Height="24px" />
                                                                <AlternatingItemStyle CssClass="bg1 verdana11bk" />
                                                                <ItemStyle CssClass="verdana11bk" />
                                                                <Columns>
                                                                    <asp:BoundColumn DataField="RowId" Visible="False" ReadOnly="True"></asp:BoundColumn>
                                                                    <asp:BoundColumn DataField="AgentId" Visible="False" ReadOnly="True"></asp:BoundColumn>
                                                                    <asp:TemplateColumn HeaderText="Sno.">
                                                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateColumn>
                                                                    <asp:BoundColumn DataField="TransactionDate" HeaderText="Booking Date" DataFormatString="{0:dd/MM/yyyy}">
                                                                        <ItemStyle Width="10%" />
                                                                    </asp:BoundColumn>
                                                                    <asp:BoundColumn DataField="RefNo" HeaderText="Transaction No">
                                                                        <ItemStyle Width="15%" HorizontalAlign="Left" />
                                                                    </asp:BoundColumn>
                                                                    <asp:BoundColumn DataField="ChqNo" HeaderText="Details">
                                                                        <ItemStyle HorizontalAlign="Left" Width="12%" />
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                    </asp:BoundColumn>
                                                                    <asp:BoundColumn DataField="TourName" HeaderText="Tour Name">
                                                                        <ItemStyle Width="10%" />
                                                                    </asp:BoundColumn>
                                                                    <asp:BoundColumn DataField="TicketAmount" HeaderText="Ticket Amount">
                                                                        <ItemStyle HorizontalAlign="Right" Width="7%" />
                                                                    </asp:BoundColumn>
                                                                    <asp:BoundColumn DataField="ServiceTax" HeaderText="GST">
                                                                        <ItemStyle HorizontalAlign="Right" Width="5%" />
                                                                    </asp:BoundColumn>
                                                                    <asp:BoundColumn DataField="GCOMM" HeaderText=" Gross Commission">
                                                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                                    </asp:BoundColumn>
                                                                    <asp:BoundColumn DataField="TDS" HeaderText="TDS">
                                                                        <ItemStyle HorizontalAlign="Right" Width="5%" />
                                                                    </asp:BoundColumn>
                                                                    <asp:BoundColumn DataField="AgentCredit" HeaderText="Amount Deposited">
                                                                        <ItemStyle HorizontalAlign="Right" Width="9%" />
                                                                    </asp:BoundColumn>
                                                                    <asp:BoundColumn DataField="AgentDebit" HeaderText="Amount Deducted">
                                                                        <ItemStyle HorizontalAlign="Right" Width="9%" />
                                                                    </asp:BoundColumn>
                                                                    <asp:BoundColumn DataField="AvailableBalance" HeaderText="Balance">
                                                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                                    </asp:BoundColumn>
                                                                    <asp:BoundColumn DataField="AdvancePaid" HeaderText="Advance">
                                                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                                    </asp:BoundColumn>
                                                                    <asp:BoundColumn DataField="TicketDetails" HeaderText="TicketDetails" Visible="false">
                                                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                                    </asp:BoundColumn>
                                                                </Columns>
                                                                <PagerStyle PrevPageText="Previous |" NextPageText="Next" 
                                                                    Position="TopAndBottom" CssClass="verdana11bk"
                                                                    Height="8px" Mode="NumericPages" />
                                                            </asp:DataGrid>
                                                        </td>
                                                    </tr>
                                                    <tr class="bg" runat="server" id="trPageTot">
                                                        <td>
                                                            <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                                                <tr>
                                                                    <td width="50%" align="right">
                                                                        <asp:Label ID="lblTot" Text="" runat="server" CssClass="verdana11bk" Font-Bold="true"></asp:Label></td>
                                                                    <td width="50%" align="left">
                                                                        <asp:Label ID="lbltotal" Text="" runat="server" CssClass="verdana11bk" Font-Bold="true"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Button ID="btnExport" runat="server" Text="Export To Excel" BackColor="#5aa1ea"
                                                                CssClass="cgi1" ForeColor="white" OnClick="btnExport_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td align="right" bgcolor="#E7E7E7">
                                    &nbsp;</td>
                            </tr>
                            <tr height="8">
                                <td align="left">
                                    <img src="Assets/images/left_d_corner.gif" width="8" height="8" /></td>
                                <td bgcolor="#E7E7E7">
                                    <img src="Assets/images/trans.gif" width="1" height="1" /></td>
                                <td align="right">
                                    <img src="Assets/images/rgt_d_corner.gif" width="8" height="8" /></td>
                            </tr>
                        </table>
                    </td>
                    <td width="296" align="right" valign="top">
                    </td>
                </tr>
            </table>
            <table width="1001" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td height="22" colspan="3" align="center" class="verdana10">
                        <uc2:agentFooter ID="AgentFooter1" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
