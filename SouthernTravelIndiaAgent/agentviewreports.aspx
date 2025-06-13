<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="agentviewreports.aspx.cs" Inherits="SouthernTravelIndiaAgent.agentviewreports" %>


<%@ Register TagPrefix="uc1" TagName="agentheader" Src="UserControls/UcAgentHeader.ascx" %>
<%@ Register TagPrefix="uc2" TagName="agentFooter" Src="UserControls/UcAgentFooter.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Southern Travels:-:Agent Commission Report</title>    
    <link href="../Assets/css/style.css" rel="stylesheet" type="text/css" />
    <link href="../Assets/css/stylesheet.css" rel="stylesheet" type="text/css" />
    <link href="../Assets/css/Calenderall.css" rel="Stylesheet" type="text/css" />
    <script language="javascript" src="../Assets/js/calenderall.js" type="text/javascript"></script>  

    <script language="javascript" type="text/javascript">
    function validation()
    {       
        var st=document.getElementById("txtFromDate");	   
	    var et=document.getElementById("txtToDate");	    
	    if((st.value=="")&&(et.value==""))
         {
              alert("Please enter the Date Range");
              return false;
         }	    
         if((st.value=="")&&(et.value!=""))
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
	                    ///alert(Date.parse(dt));
	                    //alert(Date.parse(d2));
	                     alert("Invalid Date Range. Reason: To Date is greater than Current Date.");
         	             return false;
	                }
        	                    
                    else if(document.form1.ddlType.value == "Select")
                    {
                        alert('Please choose the type');
                        document.form1.ddlType.focus();
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
                    <td align="center" valign="top">
                        <table width="780" height="449" border="0" cellpadding="0" cellspacing="0">
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
                                        <tr >
                                            <td width="283" align="left">
                                                <table width="220" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                      <td width="13"><img src="Assets/images/left_.gif" width="13" height="42" /></td>
                                                      <td align="center" background="Assets/images/bg1.gif" class="verdana14w"> Agent Commission Report </td>
                                                      <td width="13"><img src="Assets/images/right_.gif" width="13" height="42" /></td>
                                                    </tr>
                                                  </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <img src="Assets/images/trans.gif" width="1" height="1" /></td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <table width="760" border="0" cellspacing="0" cellpadding="0" >
                                                    <tr height="40">
                                                        <td align="left" class="cgi1" background="Assets/images/bg1.gif" width="3%"></td>
                                                        <td align="left" class="cgi1" background="Assets/images/bg1.gif" width="10%" valign="middle">From Date </td>
                                                           <td align="left" class="cgi1" background="Assets/images/bg1.gif" width="15%" valign="middle">
                                                                <asp:TextBox ID="txtFromDate" width="70%" runat="server" onclick="showCalendarControl(this);"></asp:TextBox>
                                                                <img id="btn1" style="cursor: hand" alt="View calendar" src="Assets/images/calendar.gif"
                                                                onclick="showCalendarControl(document.getElementById('txtFromDate'));">
                                                         </td>
                                                         <td align="right" class="cgi1" background="Assets/images/bg1.gif" width="10%" valign="middle">
                                                            To Date &nbsp; 
                                                           </td>
                                                          <td align="left" class="cgi1" background="Assets/images/bg1.gif" width="15%" valign="middle">  
                                                            <asp:TextBox ID="txtToDate" runat="server" Width="70%"  onclick="showCalendarControl(this);"></asp:TextBox>
                                                            
                                                            <img id="btn2" style="cursor: hand" alt="View calendar" src="Assets/images/calendar.gif"
                                                               onclick="showCalendarControl(document.getElementById('txtToDate'));">
                                                            </td>
                                                            <td align="right" class="cgi1" background="Assets/images/bg1.gif" width="6%" valign="middle">
                                                            Type &nbsp;</td>
                                                            <td>
                                                            <td align="left" class="verdana11bk" background="Assets/images/bg1.gif" width="29%" valign="middle">  
                                                             <asp:DropDownList ID="ddlType" runat="server" Width="90%" DataValueField="TransactionTypeId"
                                                                DataTextField="Details">
                                                            </asp:DropDownList>
                                                            </td>
                                                            <td align="left" class="verdana11bk" background="Assets/images/bg1.gif" width="12%" valign="middle">
                                                            <asp:ImageButton ID="btnSubmit" runat="server" ImageUrl="~/Assets/images/submit_blue1.jpg"
                                                                OnClick="btnSubmit_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table width="660" border="0" cellspacing="1" cellpadding="0">
                                                    <tr class="bg">
                                                        <td>
                                                            <asp:Label ID="lblMsg" runat="server"></asp:Label></td>
                                                    </tr>
                                                    <tr class="bg" runat="server" id="trGrossTot">
                                                        <td>
                                                            <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                                                <tr>
                                                                    <td width="50%" align="right"><asp:Label ID="lblCary" Text="" runat="server" CssClass="verdana11bk" Font-Bold="true" ></asp:Label></td>
                                                                    <td width="50%" align="left">
                                                                        <asp:Label ID="lblCaryFwd" Text="" runat="server" CssClass="verdana11bk" Font-Bold="true"></asp:Label>
                                                                    </td>    
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr class="bg" >
                                                        <td align="center">
                                                            <asp:DataGrid ID="dgrReports" runat="server" AutoGenerateColumns="False" DataKeyField="RowId" CssClass="verdana11bk"
                                                                Width="100%" AllowPaging="True" OnPageIndexChanged="dgrReports_PageIndexChanged"
                                                                OnItemDataBound="dgrReports_ItemDataBound" ShowFooter="true" PageSize="55">
                                                                <HeaderStyle CssClass="verdana11bkb bgheader" Height="24px" />
                                                                <AlternatingItemStyle CssClass="bg1 verdana11bk" />
                                                                <ItemStyle CssClass="verdana11bk" />
                                                                
                                                                <Columns>
                                                                    <asp:BoundColumn DataField="RowId" Visible="False" ReadOnly="True">
                                                                        <ItemStyle Width="30%" />
                                                                    </asp:BoundColumn>
                                                                    <asp:BoundColumn DataField="AgentId" HeaderText="Agent Id" Visible="False" ReadOnly="True">
                                                                        <ItemStyle Width="30%" />
                                                                    </asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="RefNo" HeaderText="Transaction No">
                                                                            <ItemStyle Width="15%" HorizontalAlign="Left" />
                                                                        </asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="TransactionDate" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}">
                                                                            <ItemStyle Width="20%" HorizontalAlign="Left" />
                                                                        </asp:BoundColumn>
                                                                 
                                                                    <asp:BoundColumn DataField="TicketAmount" HeaderText="Ticket Amount">
                                                                        <ItemStyle HorizontalAlign="Right" Width="20%" />
                                                                    </asp:BoundColumn>
                                                                    <asp:BoundColumn DataField="Commission" HeaderText="Commission">
                                                                        <ItemStyle HorizontalAlign="Right" Width="15%" />
                                                                    </asp:BoundColumn>
                                                                </Columns>
                                                                <PagerStyle PrevPageText="Previous |" NextPageText="Next" PageButtonCount="50" 
                                                                    Position="TopAndBottom" CssClass="verdana11bk" Height="8px" 
                                                                    Mode="NumericPages" />
                                                            </asp:DataGrid>
                                                           
                                                        </td>
                                                    </tr>
                                                    <tr class="bg" runat="server" id="trPageTot">
                                                        <td>
                                                            <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                                                <tr>
                                                                    <td align="right" style="width: 50%"><asp:Label ID="lblTot" Text="" runat="server" CssClass="verdana11bk" Font-Bold="true" ></asp:Label></td>
                                                                    <td width="50%" align="left">
                                                                        <asp:Label ID="lbltotal" Text="" runat="server" CssClass="verdana11bk" Font-Bold="true"></asp:Label>
                                                                    </td>    
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    
                                                    <tr><td>
                                                                <asp:Button ID="btnExport" BackColor="#5aa1ea" CssClass="cgi1" ForeColor="white" runat="server" Text="Export To Excel" OnClick="btnExport_Click" />
                                                                
                                                                </td></tr>
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
