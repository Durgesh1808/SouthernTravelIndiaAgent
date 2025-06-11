<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GroupBookingRequestform.aspx.cs" Inherits="SouthernTravelIndiaAgent.GroupBookingRequestform" %>


<%@ Register TagPrefix="uc1" TagName="AgentFooter" Src="UserControls/UcAgentFooter.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AgentHeader" Src="UserControls/UcAgentHeader.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Southern Travels :-: Agent Group Request</title>
    <link href="../Assets/css/stylesheet.css" type="text/css" rel="stylesheet" />
    <link href="../Assets/css/style.css" rel="stylesheet" type="text/css" />
    <link href="../Assets/css/calendar.css" rel="stylesheet" type="text/css" />

    <script language="javascript" src="../Assets/js/ST_calendar.js" type="text/javascript"></script>

    <script language="javascript" src="../Assets/js/MyScript.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
    
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
    function others()
    {        
        if(document.getElementById("ddlTour").value=="00")
        { 
            document.getElementById("other").style.display = "block";
        }
        else
        {        
        document.getElementById("other").style.display = "none";
        }
    }
    function validate()
		{	
		    if((document.getElementById("ddlTour").value=="0")||(document.getElementById("ddlTour").value==""))
		    {
		        alert("Please select Tour name");
			    document.getElementById("ddlTour").focus();
			    return false;
		    }
		    if(document.getElementById("ddlTour").value==00)
		    {
		        if(document.getElementById("othertour").value=="")
		        {
		            alert("Please Enter the tour Name.");
				    document.getElementById("othertour").focus();
				    return false;  
		        }
		    }			    
			if(document.getElementById("txtgroupleadername").value=="")
			{
			    alert("Please Enter the Group Leader Name.");
				document.getElementById("txtgroupleadername").focus();
				return false; 
			}				   
           
			if (validateOnlyNumber1(parseInt(document.getElementById("txtgroupleadername").value))==true)
			{
				alert("Group Leader name should not be numeric.");
				document.getElementById("txtgroupleadername").value="";
				document.getElementById("txtgroupleadername").focus();
				return false;
			}
			if (validateOnlyNumber1(parseInt(document.getElementById("txtadults").value))==false)
			{
				alert("No Of Adults should be numeric.");
				document.getElementById("txtadults").value="";
				document.getElementById("txtadults").focus();
				return false;
			}
//			if (validateOnlyNumber1(parseInt(document.getElementById("txtchild").value))==false)
//			{
//				alert("No Of Childs should be numeric.");
//				document.getElementById("txtchild").value="";
//				document.getElementById("txtchild").focus();
//				return false;
//			}
						
			var tot=0;					
	        if (document.getElementById("txtadults").value!=0)
	        {
	            tot=tot+eval(document.getElementById("txtadults").value);	           		    
	        }	 		   
		   
		   if (document.getElementById("txtchild").value!=0)
	        {
	            tot=tot+eval(document.getElementById("txtchild").value);	           		    
	        }
			if(tot==0)
			{	
			alert("Please Enter No of Passengers");		
			return false;
			}
						
		 if((document.getElementById("ddlbustype").value==0)||(document.getElementById("ddlbustype").value==""))
		    {
		        alert("Please select Bus Type name");
			    document.getElementById("ddlbustype").focus();
			    return false;
		    }
		    if(document.getElementById("departure").value=="")
		    {
		        alert("Please Select the Departure Date.");
				document.getElementById("departure").focus();
				return false; 
		    }  
		    if(document.getElementById("arrival").value=="")
		    {
		        alert("Please Select the Arrival Date.");
				document.getElementById("arrival").focus();
				return false; 
		    } 
		     if((document.getElementById("departure").value!="")&&(document.getElementById("arrival").value!=""))
		    {		        
		        var startDate	=		document.getElementById('departure').value;
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
				
				var endDate		=		document.getElementById('arrival').value;
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
					alert('Sorry! Arrival Date is less than Departure Date. Please Select Arrival Date again');
					document.getElementById('departure').focus();
					document.getElementById('arrival').focus();
					return false;
				}  				
			}
		  if(document.getElementById("txtaddress").value=="")    
			{				
				alert("Address should not be Empty.");
				document.getElementById("txtaddress").focus();
				return false;
			}
			if(document.getElementById("txtcity").value=="")    
			{				
				alert("City should not be Empty.");
				document.getElementById("txtcity").focus();
				return false;
			}
			if((document.getElementById("ddlState").value==0)||(document.getElementById("ddlState").value==""))
		    {
		        alert("Please select State");
			    document.getElementById("ddlState").focus();
			    return false;
		    }
		    
		  
			
			if(document.getElementById("txtZip").value!="")
			{
			      var k=document.getElementById("txtZip").value;
			    if(k.length<6)   
			    {
				    alert("Pin Code should be 6 digits.");
				    document.getElementById("txtZip").focus();
				    return false;
			    }
			}
			
			if (document.getElementById("txtPhone").value=="" && document.getElementById("txtMobile").value=="" )   
			{				
				alert("Mobile /Phone No should not be Empty.");
				document.getElementById("txtMobile").focus();
				return false;
			}
			
			if(document.getElementById("txtPhone").value!="")
			{
			        var k=document.getElementById("txtPhone").value;
			    if(k.length<6)   
			    {
				    alert("Phone No should be 6 digits.");
				    document.getElementById("txtPhone").focus();
				    return false;
			    }
			}
			
			
			if(document.getElementById("txtMobile").value!="")
			{
			        var k=document.getElementById("txtMobile").value;
			   if(k.length<10)   
			    {
				    alert("Mobile No should be 10 digits.");
				    document.getElementById("txtMobile").focus();
				    return false;
			    }
			}
			
			
			if (Trim(document.getElementById("txtMail").value)== "" )	
			{
				alert("Plese fill the e-mail field.It is mandatory.");
				document.getElementById("txtMail").focus();
				return false;
			}
			else
			{
				if (Trim(document.getElementById("txtMail").value)!= "" )
				{
					if (CheckMail(document.getElementById("txtMail").value)== false)
					{
						
						document.getElementById("txtMail").value="";
						document.getElementById("txtMail").focus();
						return false;
					}
				}
			}
			
			document.getElementById('Submit').style.display='none';				
			return true;
		}
		
    function chkCharacter()
	{
		
 		if((event.keyCode > 90) || (event.keyCode < 65) && event.keyCode != 8 && event.keyCode != 37 && event.keyCode != 38 && event.keyCode != 39 && event.keyCode != 40 && event.keyCode != 46 && event.keyCode != 13 && event.keyCode != 116 && event.keyCode != 16 && (event.keyCode <96 || event.keyCode > 105) && event.keyCode != 9 && event.keyCode != 110) event.returnValue = false
 		
	}
	
	function chkNumeric()
	{
		if(event.shiftKey) return false;
 		if((event.keyCode<48 || event.keyCode>57) && event.keyCode != 8 && event.keyCode != 37 && event.keyCode != 38 && event.keyCode != 39 && event.keyCode != 40 && event.keyCode != 46 && event.keyCode != 13 && event.keyCode != 116 && event.keyCode != 16 && (event.keyCode <96 || event.keyCode > 105) && event.keyCode != 9 && event.keyCode != 110) event.returnValue = false
	} 	
    function isNumberKey(evt)
        {
             var charCode = (evt.which) ? evt.which : event.keyCode
             if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
             return true;
        }
    </script>

</head>
<body>
    <div id="STContainer" style="display: none; position: absolute">
    </div>
    <div id="framediv" style="z-index: 1; left: -999px; width: 148px; position: absolute;
        height: 194px;" frameborder="0" scrolling="no">
    </div>
    <form id="groupbooking" runat="server" target="_parent">
    <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <uc1:AgentHeader ID="agentHeader" runat="server" />
            </td>
        </tr>
        <tr>
            <td width="100%" style="height: 226px" valign="top">
                <table cellpadding="0" cellspacing="0" border="0" width="100%" class="footer">
                    <tr>
                        <td width="30%" style="height: 590px">
                        </td>
                        <td style="height: 590px">
                            <table width="100%" cellspacing="0" cellpadding="0" border="0" style="background-color: #cccccc">
                                <tr>
                                    <td align="left">
                                        <img  loading="lazy"  src="Assets/images/left_corner.gif" alt="left_corner" />
                                    </td>
                                    <td bgcolor="#e7e7e7">
                                        <img  loading="lazy"  src="Assets/images/trans.gif" alt="trans" />
                                    </td>
                                    <td align="right">
                                        <img  loading="lazy"  src="Assets/images/rgt_corner.gif" alt="rgt_corner" />
                                    </td>
                                </tr>
                                <tr>
                                    <td width="8" bgcolor="#E7E7E7">
                                        &nbsp;
                                    </td>
                                    <td align="center">
                                        <table cellpadding="1" cellspacing="1" border="0" width="100%" class="footer" bgcolor="#cccccc">
                                            <tr>
                                                <td colspan="2" height="29px" align="center" style="background-color: #5aa1ea" class="cgi1">
                                                    <b>Group Booking Request Form</b>
                                                </td>
                                            </tr>
                                            <tr id="trHide1">
                                                <td bgcolor="#ffffff">
                                                    <div align="left">
                                                        <span class="style3">&nbsp;Tour Name:*</span></div>
                                                </td>
                                                <td bgcolor="#ffffff">
                                                    <p class="hlinks" style="text-align: left">
                                                        <asp:DropDownList CssClass="hlinks" ID="ddlTour" runat="server" Font-Size="8pt" Width="206px"
                                                            onchange="others()">
                                                        </asp:DropDownList>
                                                    </p>
                                                </td>
                                            </tr>
                                            <tr id="other" style="display: none">
                                                <td align="left" bgcolor="#ffffff">
                                                    Other Tour Name:*
                                                </td>
                                                <td bgcolor="#ffffff">
                                                    <input id="othertour" maxlength="150" style="width: 200px" type="text" name="txtothertour"
                                                        runat="server" />
                                                </td>
                                            </tr>
                                            <tr id="trHide2">
                                                <td bgcolor="#ffffff" align="left">
                                                    Group Leader Name:*
                                                </td>
                                                <td bgcolor="#ffffff" align="left">
                                                    <input style="width: 200px" id="txtgroupleadername" maxlength="150" size="150" type="text"
                                                        name="txtGroupleadername" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" bgcolor="#ffffff">
                                                    No of Adults:*
                                                </td>
                                                <td bgcolor="#ffffff" align="left">
                                                    <input style="width: 200px" id="txtadults" maxlength="2" size="150" type="text" name="txtadults"
                                                        runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td bgcolor="#ffffff" align="left">
                                                    No of Children:
                                                </td>
                                                <td bgcolor="#ffffff" align="left">
                                                    <input style="width: 200px" id="txtchild" maxlength="2" size="150" type="text" name="txtchild"
                                                        runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td bgcolor="#ffffff" align="left">
                                                    Type Of Bus:*
                                                </td>
                                                <td bgcolor="#ffffff">
                                                    <p class="hlinks" style="text-align: left">
                                                        <asp:DropDownList CssClass="hlinks" ID="ddlbustype" runat="server" Font-Size="8pt"
                                                            Width="206px">
                                                            <asp:ListItem Value="" Selected="True">--------Select--------</asp:ListItem>
                                                            <asp:ListItem Value="AC">AC</asp:ListItem>
                                                            <asp:ListItem Value="NONAC">NON AC</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </p>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td bgcolor="#ffffff" align="left">
                                                    Departure Date:*
                                                </td>
                                                <td bgcolor="#ffffff" id="td1" class="hlinks" align="left">
                                                    <input id="departure" name="departure" type="text" runat="server" size="9" onfocus="objCal('DepartIcon','departure','360','160'); " /><a
                                                        title="departure date" href="javascript:void(null)" onclick="objCal('DepartIcon','departure','360','160');"
                                                        hidefocus=""><img  loading="lazy" alt="calendar" id="DepartIcon" tabindex="-1" src="Assets/images/calendar.gif" border="0"
                                                            style="visibility: visible; padding-bottom: 0px;" /></a>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td bgcolor="#ffffff" align="left">
                                                    Arrival Date:*
                                                </td>
                                                <td bgcolor="#ffffff" id="td2" align="left">
                                                    <input id="arrival" name="arrival" type="text" runat="server" size="9" onfocus="objCal('ReturnIcon','arrival','390','160');" /><a
                                                        title="arrival date" href="javascript:void(null)" onclick="objCal('ReturnIcon','arrival','390','160');"
                                                        hidefocus=""><img  loading="lazy" alt="calendar" src="Assets/images/calendar.gif" border="0" id="ReturnIcon" tabindex="-1"
                                                            style="padding-bottom: 0px; visibility: visible;" /></a>
                                                </td>
                                            </tr>

                                            <script language="javascript" type="text/javascript">
                                                    stObj = new SOUTHERN.caldoy.Calendar2up("stObj","STContainer",(thisMonth+1)+"/"+thisYear,(thisMonth+1)+"/"+thisDay+"/"+thisYear);
                                                    stObj.title = " &nbsp;&nbsp;&nbsp;&nbsp;Select Date &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                                    stObj.setChildFunction("onSelect",setDate);
                                                    stObj.render();	
                                            </script>

                                            <tr>
                                                <td bgcolor="#ffffff" align="left">
                                                    <b>
                                                        <asp:CheckBox runat="server" ID="chkPromotions" Text="Can Send Promotions ?" /></b>
                                                </td>
                                                <td bgcolor="#ffffff" align="left">
                                                    <asp:CheckBox ID="chkaccommodation" runat="server" Text="Accommodation" TextAlign="right" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td bgcolor="#ffffff">
                                                </td>
                                                <td bgcolor="#ffffff" align="left">
                                                    <asp:CheckBox ID="ChkFood" runat="server" Text="Food" TextAlign="right" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td bgcolor="#ffffff" align="left">
                                                    Address:*
                                                </td>
                                                <td bgcolor="#ffffff" align="left">
                                                    <input style="width: 200px" id="txtaddress" maxlength="150" size="150" type="text"
                                                        name="txtaddress" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td bgcolor="#ffffff" align="left">
                                                    City:*
                                                </td>
                                                <td bgcolor="#ffffff" align="left">
                                                    <input style="width: 200px" id="txtcity" maxlength="150" size="150" type="text" name="txtcity"
                                                        runat="server" />
                                                </td>
                                            </tr>
                                            <tr id="trHide3">
                                                <td bgcolor="#ffffff" align="left">
                                                    <span class="style3">State:*</span>
                                                </td>
                                                <td bgcolor="#ffffff" class="hlinks" align="left">
                                                    <div id="dState" style="z-index: 0;">
                                                        <asp:DropDownList ID="ddlState" runat="server" Width="206px">
                                                        </asp:DropDownList>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr id="trHide4">
                                                <td bgcolor="#ffffff" align="left">
                                                    <span class="style3"><span class="style3">&nbsp;Zip Code:</span></span>
                                                </td>
                                                <td bgcolor="#ffffff" align="left">
                                                    <input name="textfield" type="text" class="hlinks" style="width: 200px" id="txtZip"
                                                        maxlength="6" size="10" runat="server" />
                                                </td>
                                            </tr>
                                            <tr id="trHide5">
                                                <td bgcolor="#ffffff">
                                                    <div align="left">
                                                        <span class="style3">&nbsp;Phone no:**</span></div>
                                                </td>
                                                <td bgcolor="#ffffff" align="left" class="hlinks" style="width: 200px; height: 26px;">
                                                    <input style="width: 200px" id="txtPhone" maxlength="11" size="15" type="text" name="txtPhone"
                                                        runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td bgcolor="#ffffff">
                                                    <div align="left">
                                                        <span class="style3">&nbsp;Mobile no:**</span></div>
                                                </td>
                                                <td bgcolor="#ffffff" class="hlinks" style="width: 200px; height: 27px" align="left">
                                                    <input id="txtMobile" maxlength="11" size="15" style="width: 200px" type="text" name="txtMobile"
                                                        runat="server">
                                                </td>
                                            </tr>
                                            <tr id="trHide6">
                                                <td bgcolor="#ffffff">
                                                    <div align="left">
                                                        <span class="style3">&nbsp;EMail :*</span></div>
                                                </td>
                                                <td bgcolor="#ffffff" align="left" class="hlinks" style="width: 200px; height: 24px;">
                                                    <input id="txtMail" style="width: 200px" type="text" maxlength="250" name="txtMail"
                                                        runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td bgcolor="#ffffff" height="24px" colspan="2">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" class="style3" align="left" bgcolor="#ffffff">
                                                    <font color="red">Note:</font> '**' Indicates either Phone no or Mobile no is mandatory
                                                </td>
                                            </tr>
                                            <%--<tr>
                                        <td id="tdDob1" >
                                            <div align="left">
                                                <span class="style3">&nbsp;Date of Birth :</span></div>
                                        </td>
                                        <td id="tdDob2" class="hlinks" >
                                            <input id="txtDOB" type="text" size="8" name="txtDOB" runat="server" maxlength="0" />
                                            <img  loading="lazy" alt="" id="btn1" style="cursor: hand" onclick="javascript:cbfshowcalendar('form1', 'txtDOB', 'btn1');event.cancelBubble=true;"
                                                alt="View calendar" src="Assets/images/calendar.gif" value="Calendar" />
                                        </td>
                                    </tr>--%>
                                            <tr>
                                                <td bgcolor="#ffffff" colspan="2" align="center">
                                                    <asp:Button ID="Submit" Text="Send Query" runat="Server" OnClick="Submit_Click" BackColor="#5AA1EA"
                                                        BorderStyle="None" CssClass="cgi1" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="8" bgcolor="#E7E7E7">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <img  loading="lazy" alt="left_d_corner" width="8" height="8" src="Assets/images/left_d_corner.gif" />
                                    </td>
                                    <td bgcolor="#e7e7e7">
                                        <img  loading="lazy" alt="trans" width="1" height="1" src="Assets/images/trans.gif" />
                                    </td>
                                    <td align="right">
                                        <img  loading="lazy" alt="rgt_d_corner" width="8" height="8" src="Assets/images/rgt_d_corner.gif" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="30%" style="height: 590px">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <uc1:AgentFooter ID="Footer1" runat="server"></uc1:AgentFooter>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
