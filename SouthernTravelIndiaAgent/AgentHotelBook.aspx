<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgentHotelBook.aspx.cs" Inherits="SouthernTravelIndiaAgent.AgentHotelBook" %>


<%@ Register TagPrefix="uc1" TagName="AgentHeader" Src="UserControls/UcAgentHeader.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AgentFooter" Src="UserControls/UcAgentfooter.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Hotel booking review</title>
    <link href="../Assets/css/main.css" rel="stylesheet" />
     <link href="../Assets/css/style.css" rel="stylesheet" />
     <link href="../Assets/css/stylesheet.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="chrome://firebug/content/highlighter.css" />

    <script language="javascript" type="text/javascript">
    <!--                        
       function valid(){
           var y = document.getElementById('rdyes');
           var n = document.getElementById('rdno');
           if(n.checked){
                var opt  = confirm('Are you sure you want to cancel?');
                if(opt)
                {
                    window.location.href = 'hotels.aspx';
                }
           }           
           return y.checked;
       }
       // valid
    -->    
    </script>

</head>
<body topmargin="0px">
    <form id="form1" method="post" runat="server">
        <div id="mainDiv" runat="server">
            <table cellspacing="0" cellpadding="0" border="0" align="center" id="desiya">
                <tbody>
                    <tr>
                        <td>
                            <table cellspacing="0" width="100%" cellpadding="0" border="0" id="HeaderTable">
                                <tbody>
                                    <tr>
                                        <td>
                                            <table id="Table1" cellspacing="0" cellpadding="0" style="width: 100%" border="0">
                                                <tr>
                                                    <td colspan="2">
                                                        <uc1:AgentHeader ID="AgentHeader1" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="tab-content">
                                <table cellspacing="1" cellpadding="1" border="0" class="bg2-left" id="CenterTable">
                                    <tbody>
                                        <tr>
                                            <td valign="top" bgcolor="#ffffff" align="center" id="RightCell" style="height: 16px">
                                                <table cellspacing="0" width="90%" cellpadding="0" border="0" id="Table2">
                                                    <tbody>
                                                        <tr>
                                                            <td valign="top" align="left" id="Td1">
                                                            </td>
                                                            <td valign="top" align="center" id="Td2">

                                                                <script language="javascript">
var valid_char ="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
var req;
var coupon_discount;
var checkcoupon;
var coupon_id;
var dueamount_discount;
var check_coupon;

function CheckTest()
{

    var test1=document.getElementById("check_discount").checked;

    if(test1==true)
    {
     check_coupon="true";
     document.getElementById("check_discount1").style.display='block';
    }
    else
    {
     check_coupon="false";
     document.getElementById("check_discount1").style.display='none';
     document.getElementById("couponNewDetail1").style.display='none';
     document.getElementById("couponNewDetail2").style.display='none';
     document.getElementById("coupon").value = '';
      coupon_id='';
     
    }

}
 


function validateCoupon()
{
	var couponTextField = document.getElementById("coupon");
	var couponCode = couponTextField.value;
        
	coupon_id=couponCode;
	if(couponCode!="") 
	{ 
		for(var i=0;i<couponCode.length;i++)
		{ 
			var str = couponCode.charAt(i);
			if(valid_char.indexOf(str)==-1)
			{      
				alert("Please enter correct coupon code");
				couponTextField.focus();
				couponTextField.select();
			        return false;
			}
		}
	}
	else
	{       
		alert("Please enter a coupon code");
		document.getElementById("coupon").value = '';
		document.getElementById("couponDetails").style.display = 'none';
		document.getElementById("couponNewDetail1").style.display='none';
		document.getElementById("couponNewDetail2").style.display='none';
		
		return false;
	}
	req = false;

	var now = new Date();
	var month = Number(now.getMonth()) + 1 ;
	var date = month+"/"+now.getDate()+"/"+now.getFullYear();
	

	var currency = 'INR';
	var totalrate = '2577.87';
	var httpMethod = "GET";
	var httpUrl = "/partners/servlet/validateCoupon?couponid="+couponCode+"&date="+date+"&totalrate="+totalrate+"&currency="+currency;
	
	if(window.XMLHttpRequest)
	{
		try{
			req = new XMLHttpRequest();
	        }catch(e){
			req = false;
	        }
	} 	
	else if(window.ActiveXObject)
	{
	       	try{
	        	req = new ActiveXObject("Msxml2.XMLHTTP");
	      	}catch(e){
			try{
				req = new ActiveXObject("Microsoft.XMLHTTP");
			}catch(e) {
				req = false;
			}
		}
	}
	try
	{
		if(req)
		{
			req.onreadystatechange = processReqChange;
			req.open(httpMethod, httpUrl, true);
			req.send("");
		}
	}
	catch(e)
	{
		req = false;
	}
}

function processReqChange() 
{
    if (req.readyState == 4) 
    {
        if(req.status == 200) 
        {
        	showCouponDetails();	
        } else {
            	alert("There was a problem retrieving the XML data:\n" +
                req.statusText);
        }
    }
}

function showCouponDetails()
{
	var resText = req.responseText;
	var resXML = req.responseXML;

	var strInputDataValidity = '';
	var strQueryValidity = '';
	var strDiscountType = '';
	var strDiscountAmountFixed = '';
	var strDiscountPercent = '';
	var strDiscountAmount = '';
	var strMaxAllowedDiscountAmount = '';
	var itemList = resXML.getElementsByTagName("item");
	for(var f=0;f<itemList.length;f++)
	{
		var nameNode = itemList[f].childNodes[0];
		var valueNode = itemList[f].childNodes[1];
		var name = nameNode.childNodes[0].nodeValue;
		var value = valueNode.childNodes[0].nodeValue;
		if(name == "InputDataValidity")
		{
			strInputDataValidity = value;		
		}
		if(name == "QueryValidity")
		{
			strQueryValidity = value;
		}
		if(name == "MaxAllowedDiscountAmount")
		{
			strMaxAllowedDiscountAmount = value;		
		}
		if(name == "DiscountType")
		{
			strDiscountType = value;		
		}
		if(name == "DiscountAmountFixed")
		{
			strDiscountAmountFixed = value;		
		}
		if(name == "DiscountPercent")
		{
			strDiscountPercent = value;		
		}
		if(name == "DiscountAmount")
		{
			strDiscountAmount = value;		
		}
	}
	if(strQueryValidity == "Valid")
	{
		document.getElementById("couponDetails").style.display= 'block';
		document.getElementById("couponNewDetail1").style.display='block'
		document.getElementById("couponNewDetail2").style.display='block'
		/*
		document.getElementById("maxallowedcouponAmount").innerHTML='INR'+' '+strMaxAllowedDiscountAmount;
		document.getElementById("couponType").innerHTML=strDiscountType;
		if(strDiscountType == "Percent")
		{
			document.getElementById("couponAmount").innerHTML='Lesser of '+strDiscountPercent+'%'+' of '+'INR'+' '+'2577.87'+' or Max allowed discount'+' = '+'INR'+' '+strDiscountAmount;
		}
		if(strDiscountType == "Fixed")
		{
			document.getElementById("couponAmount").innerHTML='Lesser of '+'INR'+' '+strDiscountAmountFixed+' or Max allowed discount'+' = '+'INR'+' '+strDiscountAmount;
		}
		*/
		document.getElementById("couponAmount").innerHTML='INR'+' '+strDiscountAmount;
		var totalRate = Number(2577.87);
		var dueAmount=Number(2199.96);
		var discount = Number(strDiscountAmount);
		document.getElementById("couponAmount_check").value=discount;
		coupon_discount= document.getElementById("couponAmount_check").value;
		var discountedRate = (Number(totalRate-discount)>0) ? Number(totalRate-discount) : 0;
		discount = (Number(totalRate-discount)>0) ? discount : 0;
		var dueamount_discount_check=Number( dueAmount-discount);
		document.getElementById("strDueAmount_check").value=dueamount_discount_check;
		dueamount_discount=document.getElementById("strDueAmount_check").value;
		document.getElementById("DiscountedRate").innerHTML='INR'+' '+discountedRate;
		document.getElementById("UseCouponNow").value='true';
		check_coupon= document.getElementById("UseCouponNow").value;
		
		document.test.strTest_check.value= check_coupon;
		document.getElementById("coupon").blur();
	}
	else
	{       coupon_id='';
	        
		alert("Please enter a valid coupon code");
		document.getElementById("coupon").value = '';
		document.getElementById("couponDetails").style.display = 'none';
		document.getElementById("couponNewDetail1").style.display='none';
		document.getElementById("couponNewDetail2").style.display='none';
		return false;	
	}
	
  }
function chk()
{
        var paget='true'; 
	var m=document.getElementsByName("choice"); 
        checkcoupon=check_coupon;
        
        if((checkcoupon=='true')&&(coupon_id=='' || coupon_id==null))
	{
	 alert("Please enter a coupon code");
	  paget='false';
	}
      
	if(coupon_id=='' || coupon_id==null)
	{
	 checkcoupon='false';
	}

	if(checkcoupon==false)
	{
	  coupon_discount=0; 
	}
       
	for (var i=0; i<m.length; i++)
	{
   		if (m[i].checked)
   		{
      		var rad_val = m[i].value;
      	}
    }
   
   if(rad_val=='yes')
   {   
   		if((paget=='true'))
   		{
		
		
		
   		 window.location.href='flowController.do?strMessage=AgreeBooking' +'&strCheckCoupon='+checkcoupon +'&strCouponId='+coupon_id;
		
		
   		}					
   }
   
   else
   {
   		var onDisagree= confirm("You have clicked I disagree");
   		
   		if(onDisagree)
   		{
  			window.location.href='flowController.do?strMessage=DisAgreeBooking';
   		}
   }
   
}
                                                                </script>

                                                                <table cellspacing="0" cellpadding="0" border="0">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td height="5" style="width: 6px">
                                                                                <img width="6" height="5" border="0" src="Assets/images/board1.gif" /></td>
                                                                            <td height="5" background="Assets/images/board_top.gif">
                                                                                <img width="1" height="5" border="0" src="Assets/images/board_top.gif" /></td>
                                                                            <td width="5" height="5">
                                                                                <img width="5" height="5" border="0" src="Assets/images/board2.gif" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td background="Assets/images/board_lft.gif" style="width: 6px">
                                                                                <img width="6" height="1" border="0" src="Assets/images/board_lft.gif" /></td>
                                                                            <td align="left">
                                                                                <table width="100%" cellspacing="5" cellpadding="10" border="0">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <font class="h1">Review Booking Details</font>
                                                                                                <hr size="1"  color="#ffcb97" />
                                                                                                <table width="100%" cellspacing="1" cellpadding="3" bordercolor="#111111" border="0"
                                                                                                    id="AutoNumber1" style="border-collapse: collapse;">
                                                                                                    <tbody>
                                                                                                        <tr>
                                                                                                            <td colspan="4">
                                                                                                                <img width="15" height="16" border="0" src="Assets/images/1.gif" />
                                                                                                                <font class="h2">Review Hotel Details</font><hr size="1" noshade="noshade" color="#e1e1e1" />
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td colspan="3">
                                                                                                                <font class="affiliate-name"><span id="imgRating" runat="server"></span><b>
                                                                                                                    <asp:Label ID="lblHotelName" runat="server" /></b> </font>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td colspan="3">
                                                                                                                <asp:Label ID="lblHotelAddress" runat="server" />
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td class="resultsPane2">
                                                                                                                <b>Check in date : </b>
                                                                                                                <asp:Label ID="lblCheckInDate" runat="server" /></td>
                                                                                                            <td class="resultsPane2">
                                                                                                                <b>Check out date : </b>
                                                                                                                <asp:Label ID="lblCheckOutDate" runat="server" /></td>
                                                                                                            <td class="resultsPane2">
                                                                                                                <b>Nights : </b>
                                                                                                                <asp:Label ID="lblDateDiff" runat="server" /></td>
                                                                                                            <td class="resultsPane2">
                                                                                                                <b>Adults : </b>
                                                                                                                <asp:Label ID="lblAdultCount" runat="server" /></td>
                                                                                                        </tr>
                                                                                                    </tbody>
                                                                                                </table>
                                                                                                <br />
                                                                                                <br />
                                                                                                <%=priceTable %>
                                                                                                <table width="100%" cellspacing="1" cellpadding="3" border="0">
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <span id="spMsg" runat="server"></span>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                                <table width="100%" cellspacing="1" style="display: none;" cellpadding="3" border="0">
                                                                                                    <tbody>
                                                                                                        <tr>
                                                                                                            <td nowrap="nowrap">
                                                                                                                <img width="15" height="16" border="0" src="Assets/images/3.gif" />
                                                                                                                <font class="h2">Do you have a discount coupon:
                                                                                                                    <input type="checkbox" onclick="CheckTest();" id="check_discount" name="discount_cond" /></font><hr
                                                                                                                        size="1" noshade="noshade" color="#e1e1e1" />
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </tbody>
                                                                                                </table>
                                                                                                <table width="100%" cellspacing="1" cellpadding="3" border="0" id="check_discount1"
                                                                                                    style="display: none;">
                                                                                                    <tbody>
                                                                                                        <tr>
                                                                                                            <td bgcolor="#ffffff">
                                                                                                                Enter your Discount Coupon code
                                                                                                                <input type="text" onblur="validateCoupon();" size="10" maxlength="10" name="coupon"
                                                                                                                    id="coupon" /></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td>
                                                                                                                <table style="display: none;" id="couponDetails">
                                                                                                                    <tbody>
                                                                                                                        <tr>
                                                                                                                            <td style="height: 26px">
                                                                                                                                <input type="hidden" id="couponAmount_check" name="strCouponAmount_check" /></td>
                                                                                                                        </tr>
                                                                                                                        <tr>
                                                                                                                            <td>
                                                                                                                                <input type="hidden" id="strDueAmount_check" name="strDueAmount_check" /></td>
                                                                                                                        </tr>
                                                                                                                        <tr>
                                                                                                                            <td>
                                                                                                                                <input type="hidden" value="" id="test_check" name="strTest_check" /></td>
                                                                                                                        </tr>
                                                                                                                        <tr>
                                                                                                                            <td>
                                                                                                                                <input type="hidden" value="false" id="UseCouponNow" name="strCouponUsed" /></td>
                                                                                                                        </tr>
                                                                                                                    </tbody>
                                                                                                                </table>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </tbody>
                                                                                                </table>
                                                                                                <!-- end of changes for coupon discount -->
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                                <br />
                                                                                <table width="100%" cellspacing="1" cellpadding="3" border="0">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <img width="15" height="16" border="0" src="Assets/images/4.gif" />
                                                                                                <font class="h2">Review Terms & Conditions</font><hr size="1" noshade="noshade" color="#e1e1e1" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td bgcolor="#ffffff">
                                                                                                <ol>
                                                                                                    <%=strTerms%>
                                                                                                   <%-- <font color="red"><b></b></font>--%>
                                                                                                </ol>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                                <hr size="1" noshade="noshade" color="#ffcb97" />
                                                                                <div align="center">
                                                                                    <center>
                                                                                        <table cellspacing="0" cellpadding="4" bordercolor="#111111" border="0" style="border-collapse: collapse;">
                                                                                            <tbody>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        I Agree</td>
                                                                                                    <td>
                                                                                                        <input type="radio" runat="server" id="rdyes" class="noborder" checked name="choice"
                                                                                                            value="yes" /></td>
                                                                                                    <td>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        I Disagree</td>
                                                                                                    <td>
                                                                                                        <input type="radio" runat="server" id="rdno" class="noborder" name="choice" value="no" /></td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td align="center" colspan="4">
                                                                                                        <asp:ImageButton ID="btnSubmit" runat="server" BorderWidth="0px" OnClientClick="return valid();"
                                                                                                            Style="" ImageUrl="Assets/images/submit_blue1.jpg" OnClick="btnSubmit_Click1" /></td>
                                                                                                </tr>
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </center>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                            <td width="5" height="113" background="Assets/images/board_rt.gif">
                                                                <img width="5" height="1" border="0" src="Assets/images/board_rt.gif" /></td>
                                                        </tr>
                                                        <tr>
                                                            <td width="6" height="5">
                                                                <img width="6" height="5" border="0" src="Assets/images/board3.gif" /></td>
                                                            <td height="5" background="Assets/images/board_bot.gif">
                                                                <img width="1" height="5" border="0" src="Assets/images/board_bot.gif" /></td>
                                                            <td width="5" height="5">
                                                                <img width="5" height="5" border="0" src="Assets/images/board4.gif" /></td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
            <table align="center" width="100%" style="vertical-align: bottom; margin-top: 200px">
                <tr>
                    <td>
                        <uc1:AgentFooter ID="BranchFooter1" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
