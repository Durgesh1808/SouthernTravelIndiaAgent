<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Agent_CarPerKMBooking.aspx.cs" Inherits="SouthernTravelIndiaAgent.Agent_CarPerKMBooking" %>

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
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 8" />
    <meta name="CODE_LANGUAGE" content="c#" />
    <meta name="vs_defaultClientscript" content="Javascript" />
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
    <link href="../Assets/css/stylesheet.css" rel="stylesheet" type="text/css" />
    <link href="../Assets/css/style.css" type="text/css" rel="stylesheet" />
    <link href="../Assets/css/carstyle.css" type="text/css" rel="stylesheet" />
    <link href="../Assets/css/calendar.css" rel="stylesheet" type="text/css" />

    <script language="javascript" src="../Assets/js/ST_calendar.js" type="text/javascript"></script>

    <script language="javascript" src="../Assets/jsMyScript.js" type="text/javascript"></script>

    <script language="javascript" src="../Assets/js/query_string.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
   	
	
	
	function chkNumeric(evt)
    {
        var charCode = (evt.which) ? evt.which : event.keyCode
        if (charCode > 31 && (charCode < 48 || charCode > 57))
            return false;
       return true;
    }	    
    function validatesubmit()
    {   
        if(Trim(document.getElementById("txtTourName").value)=="")
        {
             alert('Please Enter the Tailer Made Tour');
             document.getElementById("txtTourName").focus();
             return false;   
        }       
        if(document.getElementById("ddlvehicle").value=="0")
        {
            alert('Please Select The vehicle');
            document.getElementById("ddlvehicle").focus();
            return false;        
        }
        if(document.getElementById("ddlnoofpax").value=="0")
        {
            alert('Please Select the No of Passengers');
            document.getElementById("ddlnoofpax").focus();
            return false;
        }
        if((document.getElementById("rdoac").checked==false)&&(document.getElementById("rdononac").checked==false))
        {
            alert('Please Select Eaither AC or Non Ac');
            return false;
        }
        if(Trim(document.getElementById("txtstartdate").value)=="")
        {
            alert('Please Select The Start Date & Time');
            document.getElementById("txtstartdate").focus();
            return false;
        }
        
        if(Trim(document.getElementById("txtenddate").value)!="")
        {   
		    if((document.getElementById("txtstartdate").value!="")&&(document.getElementById("txtenddate").value!=""))
		    {		        
		        var startDate	=		document.getElementById('txtstartdate').value;
				var startArr	=		new Array(3)
				startArr		=		startDate.split("/")
				var vdd			=		startArr[0];
				if(vdd.length == 1)
					vdd = "0"+vdd
				var vmm			=		startArr[1];
				if(vmm.length == 1)
					vmm = "0"+vmm
				var vyy			=		startArr[2];
				var yyyymmdd	=		vyy + '' + vmm + '' + vdd;
				
				var endDate		=		document.getElementById('txtenddate').value;
				var endArr		=		new Array(3)
				endArr			=		endDate.split("/")
				var vdd1		=		endArr[0];
				if(vdd1.length == 1)
					vdd1 = "0"+vdd1
				var vmm1		=		endArr[1];
				if(vmm1.length == 1)
					vmm1 = "0"+vmm1
				var vyy1		=		endArr[2];
				var yyyymmdd1	=		vyy1 + '' + vmm1 + '' + vdd1;
				
				if(yyyymmdd > yyyymmdd1)
				{
					alert('Sorry! End Date is less than Start Date. Please Select End Date again');
					document.getElementById('txtstartdate').focus();
					document.getElementById('txtenddate').focus();
					
					return false;
				}
				else if(yyyymmdd==yyyymmdd1)
				{
				    var depttime=document.getElementById('ddlstarthr').value;
				    var deptampm=document.getElementById('ddlstartPM').value;
				    if(deptampm="PM")
				    {
				        depttime=depttime+12;
				    }
				    var arrtime=document.getElementById('ddlendhr').value;
				    var arrampm=document.getElementById('ddlendPM').value;
				    if(arrampm="PM")
				    {
				        arrtime=arrtime+12;
				    }
				    if(depttime>arrtime)
				    {
				        alert('Sorry! End Time is less than Start Time. Please Select End Time again');
					    document.getElementById('ddlstarthr').focus();
					    document.getElementById('ddlendhr').focus();					    
					    return false; 
				    }
				}		        
		    } 
        
        }       
       
		if(Trim(document.getElementById("txtreportingadd").value)=="")    
		{				
			alert("Pickup address Should not be empty.");
			document.getElementById("txtreportingadd").focus();			
			return false;
		}       
		if(Trim(document.getElementById("txtdropadd").value)=="")    
		{				
			alert("Drop address Should not be empty.");
			document.getElementById("txtdropadd").focus();			
			return false;
		}  
      
        if(Trim(document.getElementById('txtapproxkm').value)=="")
        {
            alert('Please Enter Approximate Kms');                 
            document.getElementById('txtapproxkm').focus();
            return false;
        }
        if(Trim(document.getElementById('txtapproxnoofdays').value)=="")
        {
            alert('Please Enter Approximate Noof Days');                    
            document.getElementById('txtapproxnoofdays').focus();
            return false;
        }  
       
		if(Trim(document.getElementById("txtadvance").value)=="")    
		{				
			alert("Please Enter the advance.");
			document.getElementById("txtadvance").focus();			
			return false;
		}  
        if(Trim(document.getElementById("txtEmail").value)== "")
		{
		        alert("Enter your Email-ID or Mobile No");
		        document.getElementById("txtEmail").focus();
		        return false;
		}
		else
		{
		    if (isNaN(document.getElementById("txtEmail").value)==true)
			{		
	            if ((CheckMail(document.getElementById("txtEmail").value))== false)
		        {  
			        document.getElementById("txtEmail").value="";
			        document.getElementById("txtEmail").focus();
			        return false;
		        }		   
			 }
		    else
			{
			    var a=document.getElementById("txtEmail").value;
			    if((a.length<10)|(a.length>11))
			    {
			        alert("Invalid Mobile No")
			        document.getElementById("txtEmail").value="";
	                document.getElementById("txtEmail").focus();
	                return false;
			    }			    
			}
		}
		return checkbalance();
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
	
    function CheckOnlyCharacter()
	{
		var kk
		kk=event.keyCode
		
		if((kk>=65 && kk<=90)|| kk==32 || kk==8 || kk==9 || kk==127 || kk==16 || kk==20|| kk==46)
		 {
			return true;
		 }
			alert("Please enter characters only.");
			return false;
    }
    function postlimit()
    {
	    var maxlength = 450;
	    if (document.getElementById('txtplacescovered').value.length > maxlength)
        {
		    alert("You have entered the maximum no. of characters for your Places Covered.\n\n 450 Characters is the maximum.");
		    document.getElementById('txtplacescovered').value = document.getElementById('txtplacescovered').value.substring(0, maxlength);
	    }       
    }    
    
    function calctax()
    {    

    }
    function farecalc()
    {  								
		if(document.getElementById('rdoac').checked)
		{
			document.getElementById('txtperkm').value=document.getElementById('ackmfare').value;
							   		    	
	    }  
	    else
	    {
	        document.getElementById('txtperkm').value=document.getElementById('nonackmfare').value;
	    }
    }
    
    function checkbalance()
    {
        if(isNaN(document.getElementById('txtadvance').value)==true)
        {
            alert('Please Enter Numeric value of Advance');                     
            document.getElementById('txtadvance').focus();
            return false;
        }        
        if(isNaN(document.getElementById('txtapproxkm').value)==true)
        {
            alert('Please Enter Numeric value Of Approximate Km');                     
            document.getElementById('txtapproxkm').focus();
            return false;
        }
        if(isNaN(document.getElementById('txtapproxnoofdays').value)==true)
        {
            alert('Please Enter Numeric value of Noof Days');                    
            document.getElementById('txtapproxnoofdays').focus();
            return false;
        }    
        var minkms;
        var night;
        var driver;
        var noofdays;        
        if(parseInt(document.getElementById('txtapproxkm').value)<parseInt(document.getElementById('minkmallowed').value))
        {
            minkms=parseInt(document.getElementById('minkmallowed').value);
            night=parseInt(document.getElementById('txtnight').value);
            driver=parseInt(document.getElementById('txtdriver').value);            
            noofdays=1;
            if(parseInt(document.getElementById('txtapproxnoofdays').value)>1)
            {
                noofdays=parseInt(document.getElementById('txtapproxnoofdays').value);
                minkms=minkms*noofdays;
                night=night*noofdays;
                driver=driver*noofdays;
            }           
        }
        else
        {
            minkms=parseInt(document.getElementById('txtapproxkm').value);
            night=parseInt(document.getElementById('txtnight').value);
            driver=parseInt(document.getElementById('txtdriver').value);   
            var kmfromdb=parseInt(document.getElementById('minkmallowed').value);         
            noofdays=1;
            if(parseInt(document.getElementById('txtapproxnoofdays').value)>1)
            {
                noofdays=parseInt(document.getElementById('txtapproxnoofdays').value);
                if(parseInt(noofdays)*parseInt(document.getElementById('minkmallowed').value)>=parseInt(minkms))
                {
                    minkms=parseInt(document.getElementById('minkmallowed').value);
                    minkms=minkms*noofdays;
                    night=night*noofdays;
                    driver=driver*noofdays; 
                }
                else
                {
                    minkms=minkms;
                    night=night*noofdays;
                    driver=driver*noofdays;                
                }
            }
        }
        var total=parseFloat(minkms)*parseFloat(document.getElementById('txtperkm').value);
        total=total+parseFloat(night)+parseFloat(driver);
        var st=parseFloat(document.getElementById('staxvalue').value)
        total=total+((total*st)/100);
        total=roundNumber(total);
        var adv=parseFloat(document.getElementById('txtadvance').value);
        if(parseFloat(total/4)>parseFloat(adv))
        {
            var minadv=roundNumber(parseFloat(total/4))
            alert('Minimum Amount i.e. '+minadv+' needs to pay as advance');
            document.getElementById('txtadvance').value="";           
            document.getElementById('txtadvance').focus();
            return false;
        } 
    }
       
   function roundNumber(num)
     {	 
        var result = Math.round(num*Math.pow(10,0))/Math.pow(10,0);
        return result;
     }
 function document.onkeydown()
    {
        if ( event.keyCode==116)
        {
            event.keyCode = 0;
            event.cancelBubble = true;
            return false;
        }
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
        else if (event.button !=1)
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

    <style type="text/css">
        #txtTourName
        {
            width: 415px;
        }
    </style>

</head>
<body oncontextmenu="return false;">
    <div id="STContainer" style="display: none; position: absolute">
    </div>
    <div id="framediv" style="z-index: 1; left: -999px; width: 148px; position: absolute;
        height: 194px;">
    </div>     
    <form id="form1" runat="server" target="_parent">
    <input type="hidden" runat="server" id="staxvalue" />
    <input type="hidden" runat="server" id="ackmfare" />
    <input type="hidden" runat="server" id="nonackmfare" />
    <input type="hidden" runat="server" id="minkmallowed" />
    
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                      <uc1:AgentHeader ID="agentHeader" runat="server" />
                </td>
            </tr>
        </table>
        <table width="816" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td width="8" align="left">
                    <img  loading="lazy" alt="left_corner" src="Assets/images/left_corner.gif" width="8" height="8" /></td>
                <td width="799" bgcolor="#E7E7E7">
                    <img  loading="lazy" alt="trans" src="Assets/images/trans.gif" width="1" height="1" /></td>
                <td width="8" align="right">
                    <img  loading="lazy" alt="rgt_corner" src="Assets/images/rgt_corner.gif" width="8" height="8" /></td>
            </tr>
            <tr>
                <td bgcolor="#E7E7E7">
                    &nbsp;
                </td>
                <td>
                    <table width="860" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                            <td width="8" align="left" style="height: 42px">
                                <img  loading="lazy" alt="trans" src="Assets/images/left_.gif" width="13" height="42" /></td>
                            <td width="799" align="center" background="Assets/images/bg1.gif" class="verdana14w" style="height: 42px">
                                Car Outstation TailerMade Tour - Per KM Booking</td>
                            <td width="8" align="right" style="height: 42px">
                                <img  loading="lazy" alt="trans" src="Assets/images/right_.gif" width="13" height="42" /></td>
                        </tr>
                    </table>
                </td>
                <td bgcolor="#E7E7E7">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td bgcolor="#E7E7E7">
                    &nbsp;
                </td>
                <td align="center" bgcolor="#FFFFFF">                   
                    <table width="850" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td colspan="4" align="left" class="px_12">
                                <img  loading="lazy" alt="trans" src="Assets/images/trans.gif" width="1" height="8" /></td>
                        </tr>                                         
                        <tr id="trOther">
                            <td align="left" class="px_11b" style="width: 140px">
                                &nbsp;
                                Tour Name* :</td>
                            <td colspan="3" align="left">
                                <input type="text" id="txtTourName" runat="server" maxlength="190" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" align="left" class="px_12">
                                <img  loading="lazy" alt="trans" src="Assets/images/trans.gif" width="1" height="8" /></td>
                        </tr>
                        <tr>
                            <td align="left" class="px_11b" style="width: 140px">
                                &nbsp;
                                Vechicle* :</td>
                            <td align="left" class="px_11b" style="width: 275px">
                                <asp:DropDownList ID="ddlvehicle" runat="server" AutoPostBack="true"                                    
                                    onselectedindexchanged="ddlvehicle_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td align="left" class="px_11b">&nbsp; No of Persons* :</td>
                            <td align="left" class="px_11b" style="width: 275px">
                                <asp:DropDownList ID="ddlnoofpax" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>                        
                        <tr>
                            <td colspan="4" align="left" class="px_11b">
                                <img  loading="lazy" alt="trans" src="Assets/images/trans.gif" width="1" height="8" /></td>
                        </tr>
                        <tr>
                         <td align="left" class="px_11b">
                                &nbsp;
                                Car Type* :</td>
                            <td align="left">
                                <table width="120" border="0" cellspacing="0" cellpadding="0">
                                    
                                    <tr>
                                        <td class="px_11b">
                                            <asp:RadioButton ID="rdoac" runat="server" Checked="true" GroupName="radio3" Text="AC" onclick="farecalc();" />
                                            <asp:RadioButton ID="rdononac" runat="server" GroupName="radio3" Text="Non-AC" onclick="farecalc();" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td colspan="2"></td>
                        </tr>
                         <tr>
                            <td colspan="4" align="left" class="px_11b">
                                <img  loading="lazy" alt="trans" src="Assets/images/trans.gif" width="1" height="8" /></td>
                        </tr>
                        <tr>
                            <td align="left" class="px_11b" style="width: 140px">
                                &nbsp;
                                Start Date &amp; Time* :</td>
                            <td align="left" style="width: 275px">
                                <input name="txtstartdate" type="text" class="px_11" id="txtstartdate" style="width: 83px"
                                    runat="server" readonly="readonly" />
                                <a title="startdate" href="javascript:void(null)" onclick="objCal('startIcon','txtstartdate','360','160');">
                                    <img  loading="lazy" alt="calendar" id="startIcon" tabindex="-1" src="Assets/images/calendar.gif" style="visibility: visible;
                                        padding-bottom: 0px;" border="0" alt="image" /></a>&nbsp;
                                <asp:DropDownList ID="ddlstarthr" runat="server">
                                    <asp:ListItem Value="1">1</asp:ListItem>
                                    <asp:ListItem Value="2">2</asp:ListItem>
                                    <asp:ListItem Value="3">3</asp:ListItem>
                                    <asp:ListItem Value="4">4</asp:ListItem>
                                    <asp:ListItem Value="5">5</asp:ListItem>
                                    <asp:ListItem Value="6">6</asp:ListItem>
                                    <asp:ListItem Value="7">7</asp:ListItem>
                                    <asp:ListItem Value="8" Selected="true">8</asp:ListItem>
                                    <asp:ListItem Value="9">9</asp:ListItem>
                                    <asp:ListItem Value="10">10</asp:ListItem>
                                    <asp:ListItem Value="11">11</asp:ListItem>
                                    <asp:ListItem Value="12">12</asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlstartmin" runat="server">
                                    <asp:ListItem Value="00" Selected="true">00</asp:ListItem>
                                    <asp:ListItem Value="15">15</asp:ListItem>
                                    <asp:ListItem Value="30">30</asp:ListItem>
                                    <asp:ListItem Value="45">45</asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlstartPM" runat="server">
                                    <asp:ListItem Value="AM" Selected="true">AM</asp:ListItem>
                                    <asp:ListItem Value="PM">PM</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td align="left" class="px_11b">
                                &nbsp;Approx.
                                End Date &amp; Time :</td>
                            <td align="left">
                                <input name="txtenddate" type="text" class="px_11" id="txtenddate" style="width: 83px"
                                    runat="server"  readonly="readonly"/>
                                <a title="enddate" href="javascript:void(null)" onclick="objCal('endIcon','txtenddate','360','160');">
                                    <img  loading="lazy" alt="calendar" id="endIcon" tabindex="-1" src="Assets/images/calendar.gif" style="visibility: visible;
                                        padding-bottom: 0px;" border="0" /></a>&nbsp;
                                <asp:DropDownList ID="ddlendhr" runat="server">
                                    <asp:ListItem Value="1">1</asp:ListItem>
                                    <asp:ListItem Value="2">2</asp:ListItem>
                                    <asp:ListItem Value="3">3</asp:ListItem>
                                    <asp:ListItem Value="4">4</asp:ListItem>
                                    <asp:ListItem Value="5">5</asp:ListItem>
                                    <asp:ListItem Value="6">6</asp:ListItem>
                                    <asp:ListItem Value="7">7</asp:ListItem>
                                    <asp:ListItem Value="8" Selected="true">8</asp:ListItem>
                                    <asp:ListItem Value="9">9</asp:ListItem>
                                    <asp:ListItem Value="10">10</asp:ListItem>
                                    <asp:ListItem Value="11">11</asp:ListItem>
                                    <asp:ListItem Value="12">12</asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlendmin" runat="server">
                                    <asp:ListItem Value="00" Selected="true">00</asp:ListItem>
                                    <asp:ListItem Value="15">15</asp:ListItem>
                                    <asp:ListItem Value="30">30</asp:ListItem>
                                    <asp:ListItem Value="45">45</asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlendPM" runat="server">
                                    <asp:ListItem Value="AM" Selected="true">AM</asp:ListItem>
                                    <asp:ListItem Value="PM">PM</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                          <script language="javascript" type="text/javascript">
                                                                stObj = new SOUTHERN.caldoy.Calendar2up("stObj","STContainer",(thisMonth+1)+"/"+thisYear,(thisMonth+1)+"/"+thisDay+"/"+thisYear);
                                                                stObj.title = " &nbsp;&nbsp;&nbsp;&nbsp;Select Date &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                                                stObj.setChildFunction("onSelect",setDate);
                                                                stObj.render();	
                          </script>
                        <tr>
                            <td colspan="4" align="left" class="px_11b">
                                <img  loading="lazy" alt="trans" src="Assets/images/trans.gif" width="1" height="8" /></td>
                        </tr>
                        <tr>
                            <td align="left" class="px_11b" style="width: 140px">
                                &nbsp;
                                Pick Up Address* :</td>
                            <td align="left" style="width: 275px">
                                <input name="txtreportingadd" type="text" class="px_11" id="txtreportingadd" runat="server"
                                    maxlength="150" />
                            </td>
                            <td align="left" class="px_11b">
                                &nbsp;
                                Drop Address* :</td>
                            <td colspan="3" align="left">
                                <input name="txtdropadd" type="text" class="px_11" id="txtdropadd" runat="server"
                                    maxlength="150" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" align="left" class="px_11b">
                                <img  loading="lazy" alt="trans" src="Assets/images/trans.gif" width="1" height="8" /></td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" class="px_11b" style="width: 140px">
                                &nbsp;
                                Places Covered :</td>
                            <td colspan="3" align="left" valign="top" class="px_11b">
                                <span style="height: 28px;">
                                    <textarea name="txtplacescovered" id="txtplacescovered" cols="50" rows="2" runat="server" ></textarea>
                                </span>
                            </td>
                        </tr>
                         <tr>
                            <td colspan="4" align="left" class="px_11b">
                                <span class="px_12">
                                    <img  loading="lazy" alt="trans" src="Assets/images/trans.gif" width="1" height="8" /></span></td>
                        </tr>
                        <tr>
                            <td colspan="4" align="left" bgcolor="#348DE7" class="px_11b">
                                <span class="px_12">
                                    <img  loading="lazy" alt="trans" src="Assets/images/trans.gif" width="1" height="1" /></span></td>
                        </tr>
                         <tr>
                            <td colspan="4" align="left" class="px_11b">
                                <img  loading="lazy" alt="trans" src="Assets/images/trans.gif" width="1" height="8" /></td>
                        </tr>
                        <tr>
                            <td align="left" class="px_11b" style="width: 140px">
                                &nbsp;&nbsp;Approx No of KM's* :</td>
                            <td align="left"><asp:TextBox ID="txtapproxkm" runat="server" MaxLength="6" onkeypress="return chkNumeric(event);"></asp:TextBox> </td>
                            <td align="left" class="px_11b" style="width: 140px">
                                 Approx No of Days :</td>
                            <td align="left"><asp:TextBox ID="txtapproxnoofdays" runat="server" MaxLength="2" onkeypress="return chkNumeric(event);"></asp:TextBox>  </td>
                        </tr>
                        <tr>
                            <td colspan="4" align="left" class="px_11b">
                                <img  loading="lazy" alt="trans" src="Assets/images/trans.gif" width="1" height="8" /></td>
                        </tr>
                         <tr id="tr1">
                            <td align="left" class="px_11b" style="width: 140px">
                                &nbsp;&nbsp; Per KM Price* :</td>
                            <td align="left">
                                <asp:TextBox ID="txtperkm" runat="server" ReadOnly="true" MaxLength="6" onkeypress="return chkNumeric(event);" onblur="calctax();"></asp:TextBox>                                
                            </td>
                             <td align="left" class="px_11b" style="width: 140px">
                                 Night Detention :</td>
                            <td align="left">
                                <asp:TextBox ID="txtnight" runat="server" ReadOnly="true" ></asp:TextBox>                                
                            </td>
                        </tr>
                         <tr>
                            <td colspan="4" align="left" class="px_11b">
                                <img  loading="lazy" alt="trans" src="Assets/images/trans.gif" width="1" height="8" /></td>
                        </tr>
                         <tr id="tr2">
                            <td align="left" class="px_11b" style="width: 140px">
                                &nbsp;
                                Driver Reward :</td>
                            <td align="left">
                                <asp:TextBox ID="txtdriver"  runat="server" ReadOnly="true"></asp:TextBox>                                
                            </td>
                             <td align="left" class="px_11b" style="width: 140px">
                                Advance* :</td>
                            <td align="left">
                                <asp:TextBox ID="txtadvance" runat="server" MaxLength="6" onkeypress="return chkNumeric(event);" onblur="checkbalance();"></asp:TextBox>                                
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" align="left" class="px_11b">
                                <span class="px_12">
                                    <img  loading="lazy" alt="trans" src="Assets/images/trans.gif" width="1" height="8" /></span></td>
                        </tr>                        
                        <tr>
                            <td colspan="4" align="left" bgcolor="#348DE7" class="px_11b">
                                <img  loading="lazy" alt="trans" src="Assets/images/trans.gif" width="1" height="1" /></td>
                        </tr>
                        <tr>
                            <td colspan="4" align="left" class="px_11b" style="height: 19px">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>                                                                    
                            <td align="center" class="px_11b" colspan="4">
                                Email / Mobile No:&nbsp;&nbsp;
                                <input id="txtEmail" runat="server" value="" size="25" maxlength="88"
                                    type="text" /></td>
                        </tr>
                        <tr>
                            <td colspan="4" align="left" bgcolor="#348DE7" class="px_11b">
                                <img  loading="lazy" alt="trans" src="Assets/images/trans.gif" width="1" height="1" /></td>
                        </tr>
                        <tr>
                            <td colspan="4" align="left" class="px_11b" style="height: 19px">
                                &nbsp;
                            </td>
                        </tr>
                       <tr>
                            <td colspan="4" align="center" class="px_11b">
                            <asp:Button ID="Submit" Text=" Continue Booking " class="cgi1" runat="server" 
                                    style="border-style: none; background-color: rgb(90, 161, 234);" 
                                    onclick="Submit_Click" />
                               
                                &nbsp;&nbsp;
                                <asp:Button ID="reset" Text="Reset" CssClass="cgi1" runat="server" 
                                    style="border-style: none; background-color: rgb(90, 161, 234);" 
                                    onclick="reset_Click" />                               </td>
                        </tr>
                        <tr>
                            <td colspan="4" align="center" class="px_11b">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
                <td bgcolor="#E7E7E7">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <img  loading="lazy" alt="left_d_corner" src="Assets/images/left_d_corner.gif" width="8" height="8" /></td>
                <td bgcolor="#E7E7E7">
                    <img  loading="lazy" alt="trans" src="Assets/images/trans.gif" width="1" height="1" /></td>
                <td>
                    <img  loading="lazy" alt="rgt_d_corner" src="Assets/images/rgt_d_corner.gif" width="8" height="8" /></td>
            </tr>
        </table>
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                   <uc1:AgentFooter ID="AgentFooter1" runat="server" />
                </td>
            </tr>
        </table>       
    </form>
</body>
</html>
