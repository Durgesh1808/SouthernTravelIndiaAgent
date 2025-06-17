<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCAgentBalanceClearence.ascx.cs" Inherits="SouthernTravelIndiaAgent.UserControls.UCAgentBalanceClearence" %>


<script language="javascript" type="text/javascript">
   
    
    
    function search()
    {
        if(document.getElementById('<%=txtticketno.ClientID  %>').value=="")
        {
            Swal.fire({
                icon: 'warning',
                title: 'Oops...',
                text: 'Please Enter Ticket Number',
                confirmButtonColor: '#f2572b'
            });
            document.getElementById('<%=txtticketno.ClientID  %>').focus();
            return false;
        }
    }
    
    function validation()
    { 
    var che=true;
    var exceedingAmt
        if(document.getElementById('<%=txtExceedingAmt.ClientID  %>'))
        {      
         exceedingAmt = parseFloat(document.getElementById('<%=txtExceedingAmt.ClientID  %>').value);        
        }
     // var exceedingAmt = parseFloat(document.getElementById('txtExceedingAmt.ClientID  %>').value);
      if(document.getElementById('<%=txtticketno.ClientID  %>').value=="")
      {
          Swal.fire({
              icon: 'warning',
              title: 'Oops...',
              text: 'Please Enter Ticket Number',
              confirmButtonColor: '#f2572b'
          });
            document.getElementById('<%=txtticketno.ClientID  %>').focus();
            che=false;
            return false;
        } 
        if(document.getElementById('<%=txtbalancepaidnow.ClientID  %>').value=="")
        {
            Swal.fire({
                icon: 'warning',
                title: 'Oops...',
                text: 'Please Enter Balance Paid Amount',
                confirmButtonColor: '#f2572b'
            });
            document.getElementById('<%=txtbalancepaidnow.ClientID  %>').focus();
            che=false;
            return false;
        } 
        else
        {
            if(parseInt(document.getElementById('<%=txtbalancepaidnow.ClientID  %>').value)==0)
            {
                Swal.fire({
                    icon: 'warning',
                    title: 'Oops...',
                    text: 'Please Enter Balance Paid Amount',
                    confirmButtonColor: '#f2572b'
                });
                document.getElementById('<%=txtbalancepaidnow.ClientID  %>').focus();
                che=false;
                return false;
             } 
        }
        var total=parseFloat(document.getElementById('<%=txttotalwithtax.ClientID  %>').value);
        var advance=parseFloat(document.getElementById('<%=txtamountpaidtill.ClientID  %>').value);
        var presentamt = parseFloat(document.getElementById('<%=txtbalancepaidnow.ClientID  %>').value);
        var PrevCan = parseFloat(document.getElementById('<%=txtPrev.ClientID  %>').value);
       
        if (parseFloat(total) < (parseFloat(advance) + parseFloat(presentamt) - parseFloat(PrevCan)))
        {
            Swal.fire({
                icon: 'warning',
                title: 'Oops...',
                text: 'Amount Paid is greater Than Balance Pending Amount',
                confirmButtonColor: '#f2572b'
            });
            document.getElementById('<%=txtbalancepaidnow.ClientID  %>').value="";
            document.getElementById('<%=txtbalancepaidnow.ClientID  %>').value="0";
            document.getElementById('<%=txtbalancepending.ClientID  %>').value="0";
            document.getElementById('<%=txtbalancepaidnow.ClientID  %>').focus();
            che=false;
            return false;
        }
       
        if(che)
        {
            document.getElementById('<%=btnpay.ClientID  %>').style.display='none';	  
        }
        return che;
    }
    
   
    
    function balance()
    {    
        if(isNaN(document.getElementById('<%=txtbalancepaidnow.ClientID  %>').value)==true)
        {

            Swal.fire({
                icon: 'warning',
                title: 'Oops...',
                text: 'Please Enter Numeric value',
                confirmButtonColor: '#f2572b'
            });
            document.getElementById('<%=txtbalancepaidnow.ClientID  %>').value="0";
            document.getElementById('<%=txtbalancepending.ClientID  %>').value="0";
            document.getElementById('<%=txtbalancepaidnow.ClientID  %>').focus();
            return false;
        }
    
        var balance = parseFloat(document.getElementById('<%=txtbalancetill.ClientID  %>').value);
        var paid = parseFloat(document.getElementById('<%=txtbalancepaidnow.ClientID  %>').value);  
        var exceedingAmt
        if(document.getElementById('<%=txtExceedingAmt.ClientID  %>'))
        {      
         exceedingAmt = parseFloat(document.getElementById('<%=txtExceedingAmt.ClientID  %>').value);        
        }
        else
        {
        exceedingAmt=0;
        }
        if(parseFloat(balance+ exceedingAmt)<parseFloat(paid))
        {
            Swal.fire({
                icon: 'warning',
                title: 'Oops...',
                text: 'Amount Paid is greater Than Balance Pending Amount',
                confirmButtonColor: '#f2572b'
            });
            document.getElementById('<%=txtbalancepaidnow.ClientID  %>').value="0";
            document.getElementById('<%=txtbalancepending.ClientID  %>').value="0";
            document.getElementById('<%=txtbalancepaidnow.ClientID  %>').focus();
            return false;
        }
        else
        {
            document.getElementById('<%=txtbalancepending.ClientID  %>').value =roundNumber((parseFloat(balance + exceedingAmt)-parseFloat(paid)));
        }
    }
     function chkNumeric(evt)
	    {	   
            var charCode = (evt.which) ? evt.which : event.keyCode
            if(charCode==13)
                document.getElementById('btnpay').click();
            if (charCode > 31 && (charCode < 45 || charCode > 57) || (charCode == 47 || charCode==45))
                return false;
            
           return true;
	    } 

     function roundNumber(num)
     {	 
        var result = Math.round(num*Math.pow(10,2))/Math.pow(10,2);
        return result;
     }
     function approve()
    {
          var Cid=document.getElementById('<%=txtticketno.ClientID  %>').value;
         
          if(Cid=="")
          {
              Swal.fire({
                  icon: 'warning',
                  title: 'Oops...',
                  text: '"please enter ticket no',
                  confirmButtonColor: '#f2572b'
              });
               return false;
          }
          
          window.open('AgentBalanceReceipt.aspx?CID='+ Cid +'','pops','width=600,height=500');
    }
  

     var oLastBtn=0; 
      bIsMenu = false;

            if (window.Event)
            document.captureEvents(Event.MOUSEUP);

            function nocontextmenu()
            {
                event.cancelBubble = true
                event.returnValue = false;
                return false;
            }
            function norightclick(e)
            {
                if (window.Event)
                {
                    if (e.which !=1)
                    return false;
                }
                else
                    if (event.button !=1)
                    {
                        event.cancelBubble = true
                        event.returnValue = false;
                        return false;
                    }
            }
            document.oncontextmenu = nocontextmenu;
            document.onmousedown = norightclick; 

             function onKeyDown() 
             {

                        if ((event.altKey) || ((event.keyCode == 8) && (event.srcElement.type != "text" && event.srcElement.type != "textarea" && event.srcElement.type != "password")) ||((event.ctrlKey) && ((event.keyCode == 78) || (event.keyCode == 82)) ) ||(event.keyCode == 116) ) 
                        {
                               event.keyCode = 0;
                               event.returnValue = false;
                        }
             }

</script>

<table cellpadding="0" cellspacing="0" border="0" width="800" bgcolor="#cccccc">
    <tr>
        <td height="35" colspan="4" align="center" valign="middle" bgcolor="#348DE7" class="verdana14w">
            <table width="800" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td width="13" background="Assets/images/left_.gif">
                        &nbsp;
                    </td>
                    <td align="center" valign="middle" background="Assets/images/bg1.gif">
                        Agent Balance Clearance
                    </td>
                    <td width="13" height="42" background="Assets/images/right_.gif">
                        &nbsp;
                    </td>
                </tr>
            </table>
            <table cellpadding="2" cellspacing="1" border="0" width="800" class="verdana11bk"
                bgcolor="#cccccc">
                <tr>
                    <td bgcolor="#ffffff" height="10px" colspan="4">
                    </td>
                </tr>
                <tr id="trHide1">
                    <td bgcolor="#ffffff" align="left" class="style3" colspan="4">
                        &nbsp;Ticket No:*<input id="txtticketno" type="text" maxlength="25" name="txtticketno"
                            runat="server" />
                        <asp:Button ID="Submit" Style="background-color: #5aa1ea" class="cgi1" Text="Show Details"
                            runat="Server" BorderStyle="None" OnClick="Submit_Click" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#ffffff" height="10px" colspan="4">
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#ffffff" height="24px" colspan="4">
                        <asp:DataGrid ID="dgpaymentdetails" runat="server" CellSpacing="0" AutoGenerateColumns="False"
                            CssClass="hlinks " CellPadding="0" BorderWidth="0" ShowFooter="false" Width="100%"
                            BackColor="Silver" GridLines="Horizontal">
                            <SelectedItemStyle Font-Bold="False" ForeColor="#F5C65B" BackColor="White"></SelectedItemStyle>
                            <ItemStyle CssClass="hlinks" BackColor="White"></ItemStyle>
                            <HeaderStyle Font-Bold="True" CssClass="verdana11bk"></HeaderStyle>
                            <FooterStyle ForeColor="Maroon" CssClass="heads" Font-Bold="True"></FooterStyle>
                            <Columns>
                                <asp:BoundColumn DataField="Bookingdate" HeaderText="Booking Date" DataFormatString="{0:d}"
                                    ReadOnly="True">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="startdate" ReadOnly="True" HeaderText="Journeydate" DataFormatString="{0:d}">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="totalamount" HeaderText="Total Amount" ReadOnly="True">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="advancepaid" HeaderText="Amount Paid" ReadOnly="True">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="paymentdate" HeaderText="PaymentDate" ReadOnly="True"
                                    DataFormatString="{0:d}">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Center" ForeColor="#330099" BackColor="#FFFFCC"></PagerStyle>
                        </asp:DataGrid>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#ffffff" align="left">
                        &nbsp;Tour Name:
                    </td>
                    <td bgcolor="#ffffff">
                        <asp:Label ID="txttourname" Style="border-width: 0px;" runat="server" />
                    </td>
                    <td bgcolor="#ffffff" align="left">
                        &nbsp;Group Leader Name:
                    </td>
                    <td bgcolor="#ffffff">
                        <input id="txtgroupleadername" readonly="readonly" style="border-width: 0px;" maxlength="150"
                            type="text" name="txtGroupleadername" runat="server" />
                    </td>
                </tr>
                <%-- <tr>
                                                                <td bgcolor="#ffffff" align="left">
                                                                    &nbsp;No of Adults:</td>
                                                                <td bgcolor="#ffffff">
                                                                    <input id="txtAdults" maxlength="3" readonly="readonly" type="text" name="txtAdults" runat="server" /></td>
                                                                <td bgcolor="#ffffff" align="left">
                                                                    &nbsp;No of Childs:</td>
                                                                <td bgcolor="#ffffff">
                                                                    <input id="txtchilds" maxlength="3" readonly="readonly" type="text" name="txtchilds" runat="server" /></td>
                                                            </tr>--%>
                <tr>
                    <td bgcolor="#ffffff" align="left">
                        &nbsp;Amount:
                        <asp:Label ID="lblAmtGross" runat="server" Text=" (After Discount)"> </asp:Label>
                    </td>
                    <td bgcolor="#ffffff">
                        <input id="txtamount" maxlength="10" readonly="readonly" style="border-width: 0px;"
                            type="text" name="txtamount" runat="server" />
                    </td>
                    <td bgcolor="#ffffff" align="left">
                        &nbsp;GST :
                    </td>
                    <td bgcolor="#ffffff">
                        <input id="txtTax" maxlength="10" readonly="readonly" style="border-width: 0px;"
                            type="text" name="txtTax" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#ffffff" align="left">
                        &nbsp;Discount:
                    </td>
                    <td bgcolor="#ffffff">
                        <input id="txtdiscount" maxlength="10" readonly="readonly" style="border-width: 0px;"
                            type="text" name="txtdiscount" runat="server" />
                    </td>
                    <td bgcolor="#ffffff" align="left">
                        &nbsp;Total Amount With GST:
                    </td>
                    <td bgcolor="#ffffff">
                        <input id="txttotalwithtax" maxlength="10" readonly="readonly" style="border-width: 0px;"
                            type="text" name="txttotalwithtax" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#ffffff" align="left">
                        &nbsp;Previous Cancellation Charges :
                    </td>
                    <td bgcolor="#ffffff">
                        <input id="txtPrev" maxlength="10" readonly="readonly" style="border-width: 0px;"
                            type="text" name="txttotalwithtax" runat="server" />
                    </td>
                    <td bgcolor="#ffffff" align="left">
                        &nbsp;
                    </td>
                    <td bgcolor="#ffffff">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#ffffff" align="left">
                        &nbsp;Amount paid Till Now:
                    </td>
                    <td bgcolor="#ffffff">
                        <input id="txtamountpaidtill" maxlength="10" readonly="readonly" style="border-width: 0px;"
                            type="text" name="txtamountpaidtill" runat="server" />
                    </td>
                    <td bgcolor="#ffffff" align="left">
                        &nbsp;Balance Till Now:
                    </td>
                    <td bgcolor="#ffffff">
                        <input id="txtbalancetill" maxlength="10" readonly="readonly" style="border-width: 0px;"
                            type="text" name="txtbalancetill" runat="server" />
                    </td>
                </tr>
                <tr id="trExceeding" runat="server">
                    <td bgcolor="#ffffff" align="left">
                        &nbsp;Exceeding Amount
                    </td>
                    <td bgcolor="#ffffff">
                        <input id="txtExceedingAmt" maxlength="6" type="text" name="txtExceedingAmt" runat="server"
                            value="0" onkeypress="return chkNumeric(event);" onblur="balance();" />
                    </td>
                    <td bgcolor="#ffffff" align="left">
                    </td>
                    <td bgcolor="#ffffff">
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#ffffff" align="left">
                        &nbsp;Balance Pay Now:*
                    </td>
                    <td bgcolor="#ffffff">
                        <input id="txtbalancepaidnow" maxlength="6" type="text" name="txtbalancepaidnow"
                            runat="server" value="0" onkeypress="return chkNumeric(event);" onblur="balance()" />
                    </td>
                    <!--onkeydown="balance()" onfocus="balance()" -->
                    <td bgcolor="#ffffff" align="left">
                        &nbsp;Balance Pending:
                    </td>
                    <td bgcolor="#ffffff">
                        <input id="txtbalancepending" readonly="readonly" style="border-width: 0px;" maxlength="6"
                            type="text" name="txtbalancepending" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#ffffff" colspan="4" align="center" style="height: 22px">
                        <asp:Button ID="btnpay" Style="background-color: #5aa1ea; height: 21px;" class="cgi1"
                            Text="Pay Now" runat="Server" BorderStyle="None" OnClick="btnpay_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnreset" Style="background-color: #5aa1ea; height: 21px;" class="cgi1"
                            Text="Reset" runat="Server" BorderStyle="None" OnClick="btnreset_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnPrint" Style="background-color: #5aa1ea" class="cgi1" Text="Print Receipt"
                            runat="Server" BorderStyle="None" OnClientClick="javascript:return approve();"
                            OnClick="btnPrint_Click" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
