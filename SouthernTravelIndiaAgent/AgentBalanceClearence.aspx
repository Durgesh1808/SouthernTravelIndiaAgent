<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgentBalanceClearence.aspx.cs" Inherits="SouthernTravelIndiaAgent.AgentBalanceClearence" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="uc1" TagName="AgentHeader" Src="UserControls/UcAgentHeader.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AgentFooter" Src="UserControls/UcAgentFooter.ascx" %>
<%@ Register src="/UserControls/UCAgentBalanceClearence.ascx" tagname="UCAgentBalanceClearence" tagprefix="uc2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Southern India Travel,South India Travel Packages,Travel Packages to South India</title>
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <meta content="Southern India Travel - South India Travel guides offering southern india travel, south india travel packages, travel packages to south india, travel holidays package to south india, south india travel, southern india travel packages for south india, southern india travel packages, travel package for south india, south india pilgrimage travel package."
        name="Description" />
    <meta content="southern india travel, south india travel packages, travel packages to south india, travel holidays package to south india, south india travel, southern india travel packages for south india, southern india travel packages, travel package for south india, south india pilgrimage travel package, south india beaches travel packages, south india holiday travel packages, holiday travel package to south india, southern india package travel, south india tourism, tourism in south india, holidays travel in southern india, kerala backwater travel packages in india, north india tour packages, north india tours, tours to north india, tourism in north india, golden triangle tours, kathamandu tours, kashmir tour package, chennai tours, delhi tours, hyderabad tours, pilgrimage tours in india, kerala backwater tours, southern travels india, southerntravelsindia, Sirez"
        name="Keywords" />
    <meta content="index,follow" name="robots" />
    <meta content="Designed  www.Sirez.com" name="Author" />
    <link href="../Assets/css/stylesheet.css" type="text/css" rel="stylesheet" />
    <link href="../Assets/css/style.css" rel="stylesheet" type="text/css" />

    
    <script language="javascript" src="../Assets/js/calendar.js" type="text/javascript"></script>

    <script language="javascript" src="../Assets/js/MyScript.js" type="text/javascript"></script>

    <link href="../Assets/css/calendar.css" rel="stylesheet" type="text/css" />

    <script language="javascript" src="../Assets/js/ST_calendar.js" type="text/javascript"></script>

    <meta http-equiv="Pragma" content="no-cache">
    <meta content="MSHTML 6.00.2900.2180" name="GENERATOR" />
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1" />
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1" />
    <meta name="vs_defaultClientscript" content="Javascript" />
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />

    <script language="javascript" type="text/javascript">
   
    
    
    function search()
    {
        if(document.getElementById('txtticketno').value=="")
        {
            alert('Please Enter Ticket Number');
            document.getElementById('txtticketno').focus();
            return false;
        }
    }
    
    function validation()
    { 
    var che=true;
    var exceedingAmt
        if(document.getElementById('txtExceedingAmt'))
        {      
         exceedingAmt = parseFloat(document.getElementById('txtExceedingAmt').value);        
        }
     // var exceedingAmt = parseFloat(document.getElementById('txtExceedingAmt').value);
      if(document.getElementById('txtticketno').value=="")
        {
            alert('Please Enter Ticket Number');
            document.getElementById('txtticketno').focus();
            che=false;
            return false;
        } 
        if(document.getElementById('txtbalancepaidnow').value=="")
         {
            alert('Please Enter Balance Paid Amount');
            document.getElementById('txtbalancepaidnow').focus();
            che=false;
            return false;
        } 
        else
        {
            if(parseInt(document.getElementById('txtbalancepaidnow').value)==0)
             {
                alert('Please Enter Balance Paid Amount');
                document.getElementById('txtbalancepaidnow').focus();
                che=false;
                return false;
             } 
        }
        var total=parseFloat(document.getElementById('txttotalwithtax').value);
        var advance=parseFloat(document.getElementById('txtamountpaidtill').value);
        var presentamt = parseFloat(document.getElementById('txtbalancepaidnow').value);
        var PrevCan = parseFloat(document.getElementById('txtPrev').value);
       
        if (parseFloat(total) < (parseFloat(advance) + parseFloat(presentamt) - parseFloat(PrevCan)))
        {
            alert('Amount Paid is greater Than Balance Pending Amount');
            document.getElementById('txtbalancepaidnow').value="";
            document.getElementById('txtbalancepaidnow').value="0";
            document.getElementById('txtbalancepending').value="0";
            document.getElementById('txtbalancepaidnow').focus();
            che=false;
            return false;
        }
        return advancevalid();
        if(che)
        {
            document.getElementById('btnpay').style.display='none';	  
        }
        return che;
    }
    
    function advancevalid()
	{	
	    if (document.getElementById('DDLPaymode').value=="2")
        { 
            if(document.getElementById('txtBankname').value=="")
            {
                alert('Please Enter the Bank Name');
                document.getElementById('txtBankname').focus();
                return false;
            }
            if(document.getElementById('txtChequeNo').value=="")
            {
                alert('Please Enter the Transaction No');
                document.getElementById('txtChequeNo').focus();
                return false;
            }
            if(document.getElementById('txtpaydate').value=="")
            {
                alert('Please Select The PaymentDate');
                document.getElementById('txtpaydate').focus();
                return false;
            }
        }
        else if (document.getElementById('DDLPaymode').value=="3")
        { 
            if(document.getElementById('txtBankname').value=="")
            {
                alert('Please Enter the Bank Name');
                document.getElementById('txtBankname').focus();
                return false;
            }
            if(document.getElementById('txtChequeNo').value=="")
            {
                alert('Please Enter the Cheque/DD No');
                document.getElementById('txtChequeNo').focus();
                return false;
            }
            if(document.getElementById('txtpaydate').value=="")
            {
                alert('Please Select The Cheque/DD Date');
                document.getElementById('txtpaydate').focus();
                return false;
            }
        }
        else if (document.getElementById('DDLPaymode').value=="4")
        { 
            if(document.getElementById('txtBankname').value=="")
            {
                alert('Please Enter the Bank Name');
                document.getElementById('txtBankname').focus();
                return false;
            }
            if(document.getElementById('txtChequeNo').value=="")
            {
                alert('Please Enter the Cheque/DD/Trans No');
                document.getElementById('txtChequeNo').focus();
                return false;
            }
            if(document.getElementById('txtpaydate').value=="")
            {
                alert('Please Select The Transaction Date');
                document.getElementById('txtpaydate').focus();
                return false;
            }            
        }
        else
        {
           // document.getElementById('Submit').style.display='none';	     		                 
            return true;
        }
         
	}
    
    function balance()
    {    
        if(isNaN(document.getElementById('txtbalancepaidnow').value)==true)
        {
            alert('Please Enter Numeric value');
            document.getElementById('txtbalancepaidnow').value="0";
            document.getElementById('txtbalancepending').value="0";
            document.getElementById('txtbalancepaidnow').focus();
            return false;
        }
    
        var balance = parseFloat(document.getElementById('txtbalancetill').value);
        var paid = parseFloat(document.getElementById('txtbalancepaidnow').value);  
        var exceedingAmt
        if(document.getElementById('txtExceedingAmt'))
        {      
         exceedingAmt = parseFloat(document.getElementById('txtExceedingAmt').value);        
        }
        else
        {
        exceedingAmt=0;
        }
        if(parseFloat(balance+ exceedingAmt)<parseFloat(paid))
        {
            alert('Amount Paid is greater Than Balance Pending Amount');
            document.getElementById('txtbalancepaidnow').value="0";
            document.getElementById('txtbalancepending').value="0";
            document.getElementById('txtbalancepaidnow').focus();
            return false;
        }
        else
        {
            document.getElementById('txtbalancepending').value =roundNumber((parseFloat(balance + exceedingAmt)-parseFloat(paid)));
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
//	    function trackenter(evt)
//	    { 
//	        var charCode = (evt.which) ? evt.which : event.keyCode
//	        if(charCode==13)
//                validation();
//                document.getElementById('btnpay').click();
//	    }
     function roundNumber(num)
     {	 
        var result = Math.round(num*Math.pow(10,2))/Math.pow(10,2);
        return result;
     }
     function approve()
    {
          var Cid=document.getElementById('txtticketno').value;
         
          if(Cid=="")
          {
               alert("please enter ticket no");
               return false;
          }
          
          window.open('balancereceipt.aspx?CID='+ Cid +'','pops','width=600,height=500');
    }
  
//    function document.onkeydown()
//    {
//        if ( event.keyCode==116)
//        {
//            event.keyCode = 0;
//            event.cancelBubble = true;
//            return false;
//        }
//    }
    
//    function setClipBoardData()
//    {
//            setInterval("window.clipboardData.setData('text','')",20);
//    }onload="setClipBoardData();"
   

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

</head>
<body  oncontextmenu="return false;">
    <div id="STContainer" style="display: none; position: absolute">
    </div>
    <div id="framediv" style="z-index: 1; left: -999px; width: 148px; position: absolute;
        height: 194px;"  scrolling="no">
    </div>
    <form id="form1" runat="server">
        <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td colspan="2">
                    <uc1:AgentHeader ID="AgentHeader" runat="server" />
                </td>
            </tr>
            <tr>
                <td width="80%" valign="top" style="height: 400px">
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                        <tr>
                            <td>
                                <table width="296" border="0" align="center" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left" style="width: 9px">
                                            <img src="Assets/images/left_corner.gif" width="8" height="8" alt="" /></td>
                                        <td bgcolor="#E7E7E7">
                                            <img src="Assets/images/trans.gif" width="280" height="1" alt="" /></td>
                                        <td align="right">
                                            <img src="Assets/images/rgt_corner.gif" width="8" height="8" alt="" /></td>
                                    </tr>
                                    <tr>
                                        <td align="left" bgcolor="#E7E7E7" style="width: 9px">
                                            &nbsp;</td>
                                        <td bgcolor="#E7E7E7">
                                            
                                            <uc2:UCAgentBalanceClearence ID="UCAgentBalanceClearence1" runat="server" />
                                            
                                        </td>
                                        <td align="right" bgcolor="#E7E7E7">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <img src="Assets/images/left_d_corner.gif" width="8" height="8" alt="" /></td>
                                        <td bgcolor="#E7E7E7">
                                            <img src="Assets/images/trans.gif" width="1" height="1" alt="" /></td>
                                        <td align="right">
                                            <img src="Assets/images/rgt_d_corner.gif" width="8" height="8" alt="" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                   <uc1:AgentFooter ID="AgentFooter" runat="server" />
                </td>
            </tr>
        </table>

      
    </form>
</body>
</html>
