<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="agentCarSelect.aspx.cs" Inherits="SouthernTravelIndiaAgent.agentCarSelect" %>


<%@ Register TagPrefix="uc1" TagName="AgentHeader" Src="UserControls/UcAgentHeader.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AgentFooter" Src="UserControls/UcAgentFooter.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Southern India Travel,South India Travel Packages,Travel Packages to South India
    </title>
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <meta content="Southern India Travel - South India Travel guides offering southern india travel, south india travel packages, travel packages to south india, travel holidays package to south india, south india travel, southern india travel packages for south india, southern india travel packages, travel package for south india, south india pilgrimage travel package."
        name="Description" />
    <meta content="southern india travel, south india travel packages, travel packages to south india, travel holidays package to south india, south india travel, southern india travel packages for south india, southern india travel packages, travel package for south india, south india pilgrimage travel package, south india beaches travel packages, south india holiday travel packages, holiday travel package to south india, southern india package travel, south india tourism, tourism in south india, holidays travel in southern india, kerala backwater travel packages in india, north india tour packages, north india tours, tours to north india, tourism in north india, golden triangle tours, kathamandu tours, kashmir tour package, chennai tours, delhi tours, hyderabad tours, pilgrimage tours in india, kerala backwater tours, southern travels india, southerntravelsindia, Sirez"
        name="Keywords" />
    <meta content="index,follow" name="robots" />
    <meta content="Designed  www.Sirez.com" name="Author" />
    <meta content="MSHTML 6.00.2900.2180" name="GENERATOR" />
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1" />
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1" />
    <meta name="vs_defaultClientscript" content="Javascript" />
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
    <link href="../Assets/css/style.css" rel="stylesheet" type="text/css" />
    <link href="../Assets/css/stylesheet.css" type="text/css" rel="stylesheet" />
    <link href="../Assets/css/calendar.css" rel="stylesheet" type="text/css" />

    <script language="javascript" src="../Assets/js/ST_calendar.js" type="text/javascript"></script>

    <script language="javascript" src="../Assets/js/MyScript.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">	
    
    function sadvace()
    {
    var tot1 = document.getElementById('lblTotalAmount').innerHTML.replace("INR ","");
            var dis1=document.getElementById('txtDiscount').value;
            var advance1 = document.getElementById('txtAdvance').value;
            var safterfare1=parseFloat(tot1) - parseFloat(dis1);
            var dd=safterfare1+Math.round(document.getElementById("txtStaxAmount").value)
           var de=parseFloat(eval(parseFloat(dd)*eval(25))/100);
          if(advance1<de)
           {          
          
                   //alert(de);
                alert("Advance amount should be atleast:Rs:"+de+"");
                 document.getElementById("txtAdvance").focus();
                return false;
           }
    }
   
    function CheckMail(str) 
		{
			if (str.search(/^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/) != -1)
            {
                return true;
            }
            else
            {
                alert("Invalid E-mail ID");
                return false;
            }					
		}
		
	    function chkNumeric()
        {
                 var advance=document.getElementById('txtAdvance').value; 
                 if(!isNaN(advance))
                 {
                        return true;
                 }
                 else
                 {
                     alert("Only Numeric entry in advance");
                    document.getElementById('txtAdvance').value=0;
                     return false;
                 }
        }   
        function Discountfare()
        { 
            var tot = document.getElementById('lblTotalAmount').innerHTML.replace("INR ","");
            var dis=document.getElementById('txtDiscount').value;
            var advance = document.getElementById('txtAdvance').value;
            var safterfare=parseFloat(tot) - parseFloat(dis);
//          if(advance<parseFloat(eval(parseFloat(safterfare)*eval(25))/100))
//           {
//                alert("Please Enter Min 25% of Net Amount");
//                return false;
//           }
           
            var stPerCent = document.getElementById("txtStax").value;
            document.getElementById("txtStaxAmount").value =  parseFloat((eval(tot-parseFloat(dis))*eval(stPerCent))/100); 				       
            var Stbefore = document.getElementById("txtStaxAmount").value;
            var k = parseInt(dis) + parseInt(advance);
            // var p=parseInt(advance);
             
            // calculate 50% of the  total Amount
             
            var calHalf = parseFloat(parseFloat(tot)+parseFloat(Stbefore)-parseFloat(dis));
          
          
           if(dis=="" || advance=="")
           {
                alert("enter zero or any numeric,blank/characters not allowded");
                return false;
           }
            else if((!isNaN(tot))&&(!isNaN(dis)) &&(!isNaN(advance)))
             {
                //alert('hie');
                 if(Math.round(parseFloat(parseFloat(tot)+parseFloat(Stbefore)))< parseFloat(k))// check advance+dicount can not be greater than total amount including GST
                 {
                     //alert('Advance Can\'t Greater than Total Amount');
                     document.getElementById('txtdiscount').value="0";
                     document.getElementById('txtAdvance').value="0";
                     document.getElementById('lblBalanceTotal').innerHTML = tot;                  
                     document.getElementById('txtdiscount').focus();
                     return false;
                 }
                 else
                 {
                    var afterdis = parseFloat(tot) - parseFloat(k);
                    //alert('ddd');
                                    
		            document.getElementById("txtStaxAmount").value =  parseFloat((eval(afterdis+parseFloat(advance))*eval(stPerCent))/100); 				       		        		        
                    document.getElementById('txtAfterDiscount').value = afterdis;
                     document.getElementById('Label1').innerHTML =safterfare;
                      // alert(safterfare);
                     document.getElementById('txtafterfare').value=safterfare;
                    //alert(document.getElementById("txtStaxAmount").value);
                  //alert(document.getElementById("txtAfterDiscount").value);
                    document.getElementById('lblBalanceTotal').innerHTML =  afterdis;              
		            //document.getElementById('Labeltax').innerHTML = 'GST of Rs: '+ Math.round(document.getElementById("txtStaxAmount").value) + ' Extra'   
		            document.getElementById('Labeltax').innerHTML = Math.round(document.getElementById("txtStaxAmount").value); 
		            document.getElementById('lblNetBal').innerHTML = Math.round(document.getElementById("txtStaxAmount").value) +  afterdis;
                 }
             }
             
             else
             {
                    alert("Only numeric");
                    return false;
             }
             
        }        
		
		function checkonsubmit()
	    { 
	        var tot1 = document.getElementById('lblTotalAmount').innerHTML.replace("INR ","");
            var dis1=document.getElementById('txtDiscount').value;
            var advance1 = document.getElementById('txtAdvance').value;
            var safterfare1=parseFloat(tot1) - parseFloat(dis1);
            var dd=safterfare1+Math.round(document.getElementById("txtStaxAmount").value)
            var de=parseFloat(eval(parseFloat(dd)*eval(25))/100);
          //if(advance1<de)
          // {          
          
                   //alert(de);
               // alert("Advance amount should be atleast:Rs:"+de+"");
                // document.getElementById('txtAdvance').value="0";
                // document.getElementById("txtAdvance").focus();
               // return false;
          // }

	    
	     var st=document.getElementById("ddate");
        if(st.value=="")
         {
              alert("Please enter the Date ");
              return false;
         }
         else
         {
         
                  var dt=new Date();
                  var d=dt.getDate();
                  d=d+1;
                  var m=dt.getMonth()+1;
                  var y=dt.getFullYear();
                  dt=m+"/"+d+"/"+y;
    	          
	                var s=new String();
                    s=st.value;
                    var first=s.indexOf("/",1);
                    var second=s.indexOf("/",first+1);
                    var d1=new String();
                    d1=s.substr(first+1,second-first-1)+"/"+s.substr(0,first)+"/"+s.substr(second+1,4);
//                    if( Date.parse(d1) < Date.parse(dt))
//	                {	                   
//	                     alert("Date should be greater than present date");
//         	             return false;    	            
//	                }
	            }
	            
	                
	            if ((document.getElementById("pick_hrs").value=="")||(document.getElementById("pick_min").value==""))
		        {
		        
			        alert("Please Select the  time.");
			        document.getElementById("pick_hrs").focus();
				   
			        return false;
			    }
			    if (document.getElementById("numPax").value=="0")
		        {
			    alert("Please Select the No of passenger.");
			    document.getElementById("numPax").focus();
			    return false;
		        }
			    if (document.getElementById("txtPickAddress").value=="")
		        {
		          alert("Please enter the pickup address");
		          document.getElementById("txtPickAddress").focus();
		          return false;
		         }    				     
	            if (document.getElementById("txtDropAddress1").value=="")
	            {
	              alert("Please Enter the Drop address");
	              document.getElementById("txtDropAddress1").focus();
	              return false;
	             } 
	             
	              if (Trim(document.getElementById('txtEmail').value)== "" )	
		            {
			            alert("Plese fill the e-mail/Mobile no.");
			            document.getElementById('txtEmail').focus();
			            return false;
		            }
 
    				                                
//				    if (Trim(document.getElementById('txtEmail').value)== "" )	
//		            {
//			            alert("Plese fill the e-mail field.It is mandatory.");
//			            document.getElementById('txtEmail').focus();
//			            return false;
//		            }
//			        else
//			        {
//				        if (Trim(document.getElementById('txtEmail').value)!= "" )
//				        {
//					        if (CheckMail(document.getElementById('txtEmail').value)== false )
//					        {
//					            
//						        document.getElementById('txtEmail').value="";
//						        document.getElementById('txtEmail').focus();
//						        return false;
//					         }
//				        }
//			        }
			}
            function changeNoOfCars(){
            
                    if(document.getElementById('numPax').selectedIndex==0)
                    {
                        document.getElementById('numPax').selectedIndex=1;
                    } 
                    var noOfPax = document.getElementById('numPax').selectedIndex;
                    var maxSeat = document.getElementById('maxSeat').value;
                    var noOfCar ='';
                    var carFare = parseFloat(document.getElementById('fare').value);                
                    noOfCar = Math.ceil( parseInt(noOfPax) / parseInt( maxSeat) );   
                    document.getElementById('noOfCarsHidden').value = noOfCar ;
                    document.getElementById('lblNoOfCars').innerHTML =  noOfCar;
                    lblNoOfCars
                    carFare =  noOfCar  * carFare;                
                    document.getElementById('lblTotalAmount').innerHTML =carFare.toFixed(2);
                    Discountfare();
            }
            
    </script>

</head>
<body topmargin="0" leftmargin="0px">
    <div id="STContainer" style="display: none; position: absolute">
    </div>
    <div id="framediv" style="z-index: 1; left: -999px; width: 148px; position: absolute;
        height: 194px;" frameborder="0" scrolling="no">
    </div>
    <form id="form1" method="post" runat="server">
    <input type="hidden" id="txtafterfare" runat="server" />
    <table id="Table1" cellspacing="4" cellpadding="0" style="width: 100%" border="0">
        <tr>
            <td colspan="2">
                <uc1:AgentHeader ID="agentHeader" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" valign="top" align="center" style="width: 63%;">
                <!--<div style="PADDING-LEFT: 15px">-->
                <table border="0" cellpadding="0" cellspacing="0" bordercolor="#999999" align="CENTER">
                    <tr>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td align="left">
                                        <img src="Assets/images/left_corner.gif" width="8" height="8" alt="image" />
                                    </td>
                                    <td bgcolor="#E7E7E7">
                                        <img src="Assets/images/trans.gif" width="280" height="1" alt="image" />
                                    </td>
                                    <td align="right">
                                        <img src="Assets/images/rgt_corner.gif" width="8" height="8" alt="image" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" bgcolor="#E7E7E7">
                                        &nbsp;
                                    </td>
                                    <td valign="top">
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0" style="vertical-align: top">
                                            <!--<tbody>-->
                                            <tr>
                                                <td align="Left" width="100%" style="vertical-align: top">
                                                    <div align="Left" style="background-color: Transparent">
                                                        <img src="Assets/images/rentacar.jpg" />
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr style="border: 0; border-color: White;">
                                                <td align="left">
                                                    <img src="Assets/images/3.GIF" />
                                                </td>
                                            </tr>
                                        </table>
                                        <table border="0" cellpadding="0" cellspacing="0" width="553" id="TABLE2" style="height: 273px"
                                            align="center">
                                            <!-- main head start here -->
                                            <tbody>
                                                <tr>
                                                    <td colspan="3" class="newheaderbg">
                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" height="35">
                                                            <tbody>
                                                                <tr>
                                                                    <td align="center" bgcolor="#5aa1ea" class="cgi1" height="35">
                                                                        <%--   <img src="Assets/images/carrentaldetail.jpg" />--%>
                                                                        Car Rental Details<br />
                                                                    </td>
                                                                    <%-- <td class="mandatory" align="right" style="height: 25px">
                                                                </td>--%>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <!-- main head end here -->
                                                <tr>
                                                    <td style="font-family: Arial; font-size: 12px" align="center">
                                                        <table border="0" cellpadding="0" cellspacing="0" width="551" align="center">
                                                            <tbody>
                                                                <tr>
                                                                    <td style="margin: 0pt 10px; width: 150px; height: 57px;" align="center">
                                                                        <img src="Assets/images/car1.jpg" />
                                                                    </td>
                                                                    <td class="pad2" align="left" style="height: 57px">
                                                                        <p>
                                                                        </p>
                                                                        <p class="h1tdB">
                                                                        </p>
                                                                    </td>
                                                                    <td class="pad2" align="center" style="height: 57px">
                                                                        <table border="0" cellpadding="0" cellspacing="0">
                                                                            <tr>
                                                                                <td align="left" width="150px">
                                                                                    <span class="h1tdBS">Car Models </span>:
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:Label ID="lblCarModel" runat="server" CssClass="font-color:red;" Width="108px"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="left">
                                                                                    <span class="h1tdBS">Seat Capacity</span> :
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:Label ID="lblCarCapacity" runat="server" Text="" Width="98px"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="left">
                                                                                    <span class="h1tdBS">No of Cars</span> :
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:Label ID="lblNoOfCars" runat="server" Text="1" Width="98px"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="left">
                                                                                    <span class="h1tdBS">Fare</span>
                                                                                </td>
                                                                                <td align="left">
                                                                                    Rs.
                                                                                    <asp:Label ID="lblTotalAmount" runat="server" Text="" Width="66px"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="left">
                                                                                    <span class="h1tdBS">Purpose </span>:
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:Label ID="lblPurpose" runat="server" CssClass="color: blue;" Text=""></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="left">
                                                                                    Extra Kms :
                                                                                </td>
                                                                                <td align="left">
                                                                                    Rs.
                                                                                    <asp:Label ID="lblExtraKms" runat="server" Text=""></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="left">
                                                                                    Extra Hours :
                                                                                </td>
                                                                                <td align="left">
                                                                                    Rs.
                                                                                    <asp:Label ID="lblExtraHrs" runat="server" Text=""></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr id="trST" runat="server">
                                                                                <td align="left">
                                                                                    GST
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:Label ID="Labeltax" runat="server" Text=""></asp:Label>&nbsp;(<asp:Label ID="lblSTax"
                                                                                        runat="server" Text=""></asp:Label>)
                                                                                </td>
                                                                            </tr>
                                                                            <tr id="tr1" runat="server" style="font-family: Verdana, Arial, Helvetica; line-height: 1.4;
                                                                                color: #000000; font-size: 12px; display: none;">
                                                                                <td align="left">
                                                                                    Discount
                                                                                </td>
                                                                                <td align="left">
                                                                                    <input type="text" id="txtDiscount" style="text-align: right; width: 106px;" maxlength="10"
                                                                                        size="8" onblur="Discountfare();" runat="server" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr id="tr2" runat="server" style="font-family: Verdana, Arial, Helvetica; line-height: 1.4;
                                                                                color: #000000; font-size: 12px; display: none;">
                                                                                <td align="left">
                                                                                    Advance
                                                                                </td>
                                                                                <td align="left">
                                                                                    <input type="text" id="txtAdvance" style="text-align: right; width: 105px;" maxlength="10"
                                                                                        size="8" onblur="chkNumeric();Discountfare();" runat="server" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr id="tr3" runat="server" style="display: none;">
                                                                                <td align="left">
                                                                                    Balance Amount
                                                                                </td>
                                                                                <td align="left">
                                                                                    Rs.
                                                                                    <asp:Label ID="lblBalanceTotal" runat="server" Text=""></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr id="tr5" runat="server">
                                                                                <td align="left">
                                                                                    Fare
                                                                                </td>
                                                                                <td align="left" style="font-family: Verdana, Arial, Helvetica; line-height: 1.4;
                                                                                    color: #FF0000; font-size: 14px">
                                                                                    Rs.
                                                                                    <asp:Label ID="Label1" runat="server"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr id="tr4" runat="server">
                                                                                <td align="left">
                                                                                    Net
                                                                                    <asp:Label ID="netBalorTotal" runat="server" />
                                                                                </td>
                                                                                <td align="left" style="font-family: Verdana, Arial, Helvetica; line-height: 1.4;
                                                                                    color: #FF0000; font-size: 14px">
                                                                                    Rs.
                                                                                    <asp:Label ID="lblNetBal" runat="server" Text=""></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                        <table style="margin: 0pt 20px;" border="0" cellpadding="0" cellspacing="0" width="511">
                                                            <tbody>
                                                                <tr style="display: none">
                                                                    <td style="width: 4px">
                                                                        <img src="Assets/images/bullet.gif" alt="image" />
                                                                    </td>
                                                                    <td style="width: 258px" align="left">
                                                                    </td>
                                                                    <td style="width: 15px">
                                                                        <img src="Assets/images/bullet.gif" alt="image" />
                                                                    </td>
                                                                    <td align="left">
                                                                </tr>
                                                                <tr style="display: none">
                                                                    <td style="width: 4px; height: 19px;">
                                                                        <img src="Assets/images/bullet.gif" alt="image" />
                                                                    </td>
                                                                    <td style="width: 258px; height: 19px;" align="left">
                                                                    </td>
                                                                    <td style="width: 15px; height: 18px;">
                                                                        <img src="Assets/images/bullet.gif" alt="image" />
                                                                    </td>
                                                                    <td align="left">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="4" style="width: 5px" bgcolor="white">
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="4" align="left">
                                                                        <table cellpadding="0" cellspacing="0" bgcolor="white">
                                                                            <tr>
                                                                                <td style="width: 40%; vertical-align: top; height: 45px;" bgcolor="white">
                                                                                    <b>* Other extra charges :</b>
                                                                                </td>
                                                                                <td bgcolor="white" style="height: 45px">
                                                                                    <span style="font-family: Arial; font-size: 12px">Toll charges and Road taxes to be
                                                                                        paid locally. All extra charges will be payable directly to the driver in cash.<br>
                                                                                        <b>* </b>Image of the car shown is only for indicative purpose. </span>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="4" style="width: 5px" bgcolor="white">
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                        <table cellpadding="0" cellspacing="0" width="100%" border="0" height="35">
                                                            <tr>
                                                                <td align="center" style="width: 75%;" height="35" bgcolor="#5aa1ea" class="cgi1">
                                                                    <%--<font style="font-weight: bold; font-family: Arial; font-size: 14px">
                                                                    <div align="center">
                                                                        <strong>PICK UP DETAILS </strong>
                                                                    </div>
                                                                </font>--%>
                                                                    Pick Up Details<br />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" style="font-family: Arial; font-size: 12px">
                                                            <tr>
                                                                <td colspan="6" style="width: 5px">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="pad2" align="left">
                                                                    <span class="h1tdr" style="left: 5px; position: relative">Pick up Date</span>
                                                                </td>
                                                                <td style="width: 20px;">
                                                                    <span class="h1tdr">:</span>
                                                                </td>
                                                                <td>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <input id="ddate" name="ddate" type="text" runat="server" size="9" onfocus="objCal('DepartIcon','ddate','360','160'); " />
                                                                            </td>
                                                                            <td>
                                                                                <a title="Pick up date" href="javascript:void(null)" onclick="objCal('DepartIcon','ddate','360','160');"
                                                                                    hidefocus="">
                                                                                    <img alt="Calendar" id="DepartIcon" tabindex="-1" src="Assets/images/calendar.gif" border="0"
                                                                                        style="visibility: visible; padding-bottom: 0px;" /></a>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td align="right" class="pad2">
                                                                    <span class="h1tdr">Pickup Address</span>
                                                                </td>
                                                                <td width="20">
                                                                    <span class="h1tdr">:</span>
                                                                </td>
                                                                <td rowspan="2">
                                                                    <textarea name="txtPickAddress" runat="server" cols="20" id="txtPickAddress"></textarea>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="pad2" align="left" style="left: 5px; position: relative">
                                                                    <span class="h1tdr">Pick up Time</span>
                                                                </td>
                                                                <td style="width: 21px">
                                                                    <span class="h1tdr">:</span>
                                                                </td>
                                                                <td>
                                                                    <table width="100%" border="0">
                                                                        <tr>
                                                                            <td>
                                                                                <select runat="server" id="pick_hrs" name="pick_hrs" class="inputControl">
                                                                                    <option value="">hrs</option>
                                                                                    <option value="00">00</option>
                                                                                    <option value="01">01</option>
                                                                                    <option value="02">02</option>
                                                                                    <option value="03">03</option>
                                                                                    <option value="04">04</option>
                                                                                    <option value="05">05</option>
                                                                                    <option value="06">06</option>
                                                                                    <option value="07">07</option>
                                                                                    <option value="08">08</option>
                                                                                    <option value="09">09</option>
                                                                                    <option value="10">10</option>
                                                                                    <option value="11">11</option>
                                                                                    <option value="12">12</option>
                                                                                    <option value="13">13</option>
                                                                                    <option value="14">14</option>
                                                                                    <option value="15">15</option>
                                                                                    <option value="16">16</option>
                                                                                    <option value="17">17</option>
                                                                                    <option value="18">18</option>
                                                                                    <option value="19">19</option>
                                                                                    <option value="20">20</option>
                                                                                    <option value="21">21</option>
                                                                                    <option value="22">22</option>
                                                                                    <option value="23">23</option>
                                                                                </select>
                                                                            </td>
                                                                            <td>
                                                                                <select runat="server" id="pick_min" name="pick_min" class="inputControl">
                                                                                    <option value="">min</option>
                                                                                    <option value="00" selected>00</option>
                                                                                    <option value="15">15</option>
                                                                                    <option value="30">30</option>
                                                                                    <option value="45">45</option>
                                                                                </select>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr height="5">
                                                                <td colspan="6">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" class="pad2">
                                                                    <span class="h1tdr">No. of passengers</span>
                                                                </td>
                                                                <td style="width: 20px;">
                                                                    <span class="h1tdr">:</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:DropDownList ID="numPax" runat="server">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td class="pad2" align="right">
                                                                    <span class="h1tdr">DropOff Address</span>
                                                                </td>
                                                                <td>
                                                                    <span class="h1tdr">:</span>
                                                                </td>
                                                                <td rowspan="2">
                                                                    <textarea name="txtDropAddress1" runat="server" cols="20" id="txtDropAddress1"></textarea>
                                                                </td>
                                                            </tr>
                                                            <tr height="5">
                                                                <td colspan="6">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td style="width: 20px">
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr height="5">
                                                                <td colspan="6">
                                                                    <table cellpadding="0" cellspacing="0" width="100%" border="0" style="height: 4px">
                                                                        <tr>
                                                                            <td align="center" style="width: 75%;" height="15" bgcolor="#5aa1ea" class="cgi1">
                                                                                <%--<font style="font-weight: bold; font-family: Arial; font-size: 14px">
                                                                    <div align="center">
                                                                        <strong>PICK UP DETAILS </strong>
                                                                    </div>
                                                                </font>--%>
                                                                                &nbsp;<br />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr height="5">
                                                                <td colspan="6">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" colspan="6" valign="middle">
                                                                    Email / Mobile No:&nbsp;&nbsp;
                                                                    <input id="txtEmail" runat="server" value="" size="25" class="inputControl" maxlength="88"
                                                                        type="text" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="10">
                                                                </td>
                                                            </tr>
                                                            <tr height="5">
                                                                <td colspan="6">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" align="CENTER">
                                                                    <asp:Button ID="Button1" runat="server" CssClass="cgi1" Text="Continue Booking" OnClick="Button1_Click"
                                                                        BackColor="#5AA1EA" BorderStyle="None" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="10">
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <td align="right" bgcolor="#E7E7E7">
                                            &nbsp;
                                        </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <img src="Assets/images/left_d_corner.gif" width="8" height="8" alt="image" />
                                    </td>
                                    <td bgcolor="#E7E7E7">
                                        <img src="Assets/images/trans.gif" width="1" height="1" alt="image" />
                                    </td>
                                    <td align="right">
                                        <img src="Assets/images/rgt_d_corner.gif" width="8" height="8" alt="image" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" valign="bottom">
                <uc1:AgentFooter ID="AgentFooter1" runat="server" />
            </td>
        </tr>
    </table>
    <input type="hidden" id="noOfCarsHidden" value="1" runat="server" />
    <input type="hidden" id="maxSeat" value="" runat="server" />
    <input type="hidden" id="fare" value="" runat="server" />
    <input type="hidden" id="txtStax" runat="server" />
    <input type="hidden" id="txtStaxAmount" runat="server" value="0.00" />
    <input type="hidden" id="txtAfterDiscount" runat="server" />
    <input type="hidden" id="txtcityid" runat="server" />
    </form>

    <script language="javascript" type="text/javascript">
                    stObj = new SOUTHERN.caldoy.Calendar2up("stObj","STContainer",(thisMonth+1)+"/"+thisYear,(thisMonth+1)+"/"+thisDay+"/"+thisYear);
                    stObj.title = " &nbsp;&nbsp;&nbsp;&nbsp;Select Date &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                    stObj.setChildFunction("onSelect",setDate);
                    stObj.render();	
    </script>

    <script language="javascript" type="text/javascript">
           
                 try{     
                  
                       document.getElementById('lblBalanceTotal').innerHTML = document.getElementById('lblTotalAmount').innerHTML.replace("INR ","");
                       document.getElementById('txtAdvance').value = "0";
                       document.getElementById('txtDiscount').value = "0";   
                     }
                       catch(err)
                       {
                              txt="There was an error on this page.\n\n";
                              txt+="Error description: " + err.description + "\n\n";
                              txt+="Click OK to continue.\n\n";
                            
                       }
                   Discountfare(); 
                   
                 
		           
              
    </script>

    <%--<div id="framediv" style="z-index: 103; left: -999px; width: 148px; position: absolute;
            height: 194px;" frameborder="0" scrolling="no">
        </div>--%>
</body>
</html>
