<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgentSeatRequestForm.aspx.cs" Inherits="SouthernTravelIndiaAgent.AgentSeatRequestForm" %>

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
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1" />
    <meta name="vs_defaultClientscript" content="Javascript" />
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
    <link href="../Assets/css/stylesheet.css" type="text/css" rel="stylesheet" />
    <link href="../Assets/css/style.css" rel="stylesheet" type="text/css" />

    <script language="javascript" src="../Assets/js/calendar.js" type="text/javascript"></script>

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
    function validate()
		{ 	
		    if(Trim(document.getElementById("txtgroupleadername").value)=="")
			{
			    alert("Please Enter the Group Leader Name.");
				document.getElementById("txtgroupleadername").focus();
				return false; 
			}
		
				
			if((document.getElementById("txtadults").value)=="")
			{
			    alert("Please Enter No of Passengers");				
				document.getElementById("txtadults").focus();
				return false;			
			}
			
//			if (validateOnlyNumber1(parseInt(document.getElementById("txtadults").value))==false)
//			{
//				alert("No Of Adults should be numeric.");
//				document.getElementById("txtadults").value="";
//				document.getElementById("txtadults").focus();
//				return false;
//			}
//			if((document.getElementById("txtchild").value)!="")
//			{
//			    if (validateOnlyNumber1(parseInt(document.getElementById("txtchild").value))==false)
//			    {
//				    alert("No Of Childs should be numeric.");
//				    document.getElementById("txtchild").value="";
//				    document.getElementById("txtchild").focus();
//				    return false;
//			    }
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
			document.getElementById("txtadults").focus();		
			return false;
			}
			
			var  rbseater = document.getElementsByName("Rdbbustype");		        
	        var varccorcashChecked=false;
	        for(var i=0; i< rbseater.length; i++)
	        {			   			   
	            if(rbseater[i].type=='radio' && rbseater[i].checked==true)		    
	            {		       	    
	                varccorcashChecked = true;		                
	                break;    
	            }			    
	        }			
	        if(!varccorcashChecked)	
	        {	    
	            alert('Please Select Bus Type AC/NonAC');
	            return false;
		    } 
					 
		    if(document.getElementById("departure").value=="")
		    {
		        alert("Please Select the Departure Date.");
				document.getElementById("departure").focus();
				return false; 
		    } 
		    else
		    {
		          var st=document.getElementById("departure");
		         
	              var dt=new Date();
                  var d=dt.getDate();
                  var m=dt.getMonth()+1;
                  var y=dt.getFullYear();
                  dt=m+"/"+d+"/"+y;
                
                  var s=new String();
                  s  =  st.value;
                 
                  var first=s.indexOf("/",1);
                  var second=s.indexOf("/",first+1);
                  var d1=new String();
                
                  d1=s.substr(first+1,second-first-1)+"/"+s.substr(0,first)+"/"+s.substr(second+1,4);
                 
                
                    
	                if( Date.parse(d1) < Date.parse(dt) )
	                {
	                     alert("Invalid Date");
         	             return false;
    	            
	                }
	              
        	            
        	           
        	}       
		    				
			
		  if(Trim(document.getElementById("txtaddress").value)=="")    
			{				
				alert("Address should not be Empty.");
				document.getElementById("txtaddress").focus();
				return false;
			}
			if(Trim(document.getElementById("txtcity").value)=="")    
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
				    alert("Zip Code should be 6 digits.");
				    document.getElementById("txtZip").focus();
				    return false;
			    }
		   }
			if ((document.getElementById("txtPhone").value=="") && (document.getElementById("txtMobile").value=="" ))   
			{				
				alert("Mobile /Phone No should not be Empty.");
				document.getElementById("txtMobile").focus();
				return false;
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

			if (Trim(document.getElementById("txtMail").value)!= "" )
			{
				if (CheckMail(document.getElementById("txtMail").value)== false)
				{					
					document.getElementById("txtMail").value="";
					document.getElementById("txtMail").focus();
					return false;
				}
			}			
			
			if(Trim(document.getElementById('txtCaptchImage').value)=="")
		    {
			    alert("Please enter verification code.");
			    document.getElementById('txtCaptchImage').focus();
			    return false; 
		    }					
		}
		
   function CheckOnlyCharacter()
  {
			var kk
			kk=event.keyCode
			//alert(kk);
			
			if((kk>=65 && kk<=90)|| kk==32  || kk==8 || kk==9 || kk==127 || kk==16 || kk==20|| kk==46 )
			 {
				return true;
			 }
				alert("Please enter characters only");
				return false;
	}
	
	function chkNumeric()
	{
		if(event.shiftKey) return false;
 		if((event.keyCode<48 || event.keyCode>57)  && event.keyCode != 8 && event.keyCode != 37 && event.keyCode != 38 && event.keyCode != 39 && event.keyCode != 40 && event.keyCode != 46 && event.keyCode != 13 && event.keyCode != 116 && event.keyCode != 16 && (event.keyCode <96 || event.keyCode > 105) && event.keyCode != 9 && event.keyCode != 110) event.returnValue = false
	} 
	
	function chk()
	{
		
 		if(event.keyCode== 16 ||event.keyCode == 191|| event.keyCode ==37 ||event.keyCode == 38 ||event.keyCode == 39 || event.keyCode == 40 || event.keyCode ==13)
 		
 		{
 		    alert("No Special character");
 		    return false;
 		
 		    
 		}
 		
	
	}
		
		
    </script>

</head>
<body>
    <iframe id="CalFrame" style="border-right: 1px solid; border-top: 1px solid; display: none;
        z-index: 100; border-left: 1px solid; width: 182px; border-bottom: 1px solid;
        position: absolute; height: 193px" marginwidth="0" marginheight="0" src="JavaScript/Calender.htm"
        frameborder="0" noresize="yes" scrolling="no"></iframe>
    <form id="groupbooking" runat="server" target="_parent">
        <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td>
                    <uc1:AgentHeader ID="agentHeader" runat="server" />
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <table cellpadding="0" cellspacing="0" border="0" width="100%" class="footer">
                        <tr>
                            <td>
                                <table width="296" border="0" align="center" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left" style="width: 9px">
                                            <img src="Assets/images/left_corner.gif" width="8" height="8" loading="lazy" alt="left_corner"/></td>
                                        <td bgcolor="#E7E7E7">
                                            <img src="Assets/images/trans.gif" width="280" height="1" loading="lazy" alt="trans"/></td>
                                        <td align="right">
                                            <img src="Assets/images/rgt_corner.gif" width="8" height="8" loading="lazy" alt="rgt_d_corner"/></td>
                                    </tr>
                                    <tr>
                                        <td align="left" bgcolor="#E7E7E7" style="width: 9px">
                                            &nbsp;</td>
                                        <td bgcolor="#E7E7E7">
                                            <table cellpadding="0" cellspacing="0" border="0" width="500" class="footer" bgcolor="#cccccc">
                                                <tr>
                                                    <td height="35" colspan="2" align="center" valign="middle" bgcolor="#348DE7" class="verdana14w">
                                                        <table width="500" border="0" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td width="13" background="Assets/images/left_.gif">
                                                                    &nbsp;</td>
                                                                <td align="center" valign="middle" background="Assets/images/bg1.gif">
                                                                    Request For Tour
                                                                </td>
                                                                <td width="13" height="42" background="Assets/images/right_.gif">
                                                                    &nbsp;</td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table cellpadding="0" cellspacing="1" border="0" width="500" class="footer" bgcolor="#cccccc">
                                                <tr>
                                                    <td bgcolor="#ffffff" height="24px" colspan="2">
                                                    </td>
                                                </tr>
                                                <tr id="trHide1">
                                                    <td bgcolor="#ffffff" style="height: 24px">
                                                        <div align="left">
                                                            <span class="style3">&nbsp;Tour Name:</span></div>
                                                    </td>
                                                    <td bgcolor="#ffffff" style="height: 24px">
                                                        <input style="width: 200px" id="txttourname" readonly="readonly" size="150" type="text"
                                                            name="txttourname" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr id="trHide2">
                                                    <td bgcolor="#ffffff" align="left">
                                                        &nbsp;Group Leader Name*:</td>
                                                    <td bgcolor="#ffffff">
                                                        <input style="width: 200px" id="txtgroupleadername" maxlength="150" size="150" type="text"
                                                            name="txtGroupleadername" runat="server" /></td>
                                                </tr>
                                                <tr>
                                                    <td align="left" bgcolor="#ffffff" style="height: 24px">
                                                        &nbsp;No of Adults*:</td>
                                                    <td bgcolor="#ffffff" style="height: 24px">
                                                        <input style="width: 200px" id="txtadults" maxlength="3" size="150" type="text" name="txtadults"
                                                            runat="server" /></td>
                                                </tr>
                                                <tr>
                                                    <td bgcolor="#ffffff" align="left">
                                                        &nbsp;No of Childs:</td>
                                                    <td bgcolor="#ffffff">
                                                        <input style="width: 200px" id="txtchild" maxlength="3" size="150" type="text" name="txtchild"
                                                            runat="server" /></td>
                                                </tr>
                                                <tr>
                                                    <td bgcolor="#ffffff" align="left">
                                                        &nbsp;Type Of Bus*:</td>
                                                    <td bgcolor="#ffffff">
                                                        <asp:RadioButtonList ID="Rdbbustype" runat="server" RepeatDirection="Horizontal"
                                                            RepeatLayout="Flow" Width="145px">
                                                            <asp:ListItem Value="AC">AC&nbsp;</asp:ListItem>
                                                            <asp:ListItem Value="NAC">Non AC</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td bgcolor="#ffffff" align="left">
                                                        &nbsp;Journey Date*:</td>
                                                    <td bgcolor="#ffffff" id="td1" class="hlinks">
                                                        <input id="departure" type="text" size="8" name="txtDeparture" runat="server" maxlength="10"
                                                            readonly onclick="javascript:cbfshowcalendar('groupbooking', 'departure', 'Img1');event.cancelBubble=true;" />
                                                        <img id="Img1" style="cursor: hand" onclick="javascript:cbfshowcalendar('groupbooking', 'departure', 'Img1');event.cancelBubble=true;"
                                                            alt="View calendar" src="Assets/images/calendar.gif" value="Calendar"  loading="lazy"/>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td bgcolor="#ffffff" align="left">
                                                        &nbsp;Address*:</td>
                                                    <td bgcolor="#ffffff">
                                                        <input style="width: 200px" id="txtaddress" maxlength="150" size="150" type="text"
                                                            name="txtaddress" runat="server" /></td>
                                                </tr>
                                                <tr>
                                                    <td bgcolor="#ffffff" align="left">
                                                        &nbsp;City*:</td>
                                                    <td bgcolor="#ffffff">
                                                        <input style="width: 200px" id="txtcity" maxlength="150" size="150" type="text" name="txtcity"
                                                            runat="server" /></td>
                                                </tr>
                                                <tr id="trHide3">
                                                    <td bgcolor="#ffffff">
                                                        <span class="style3">&nbsp;State*:</span></td>
                                                    <td bgcolor="#ffffff" class="hlinks">
                                                        <asp:DropDownList ID="ddlState" runat="server" Width="206px">
                                                            <asp:ListItem Value="" Selected="True">--------Select--------</asp:ListItem>
                                                            <asp:ListItem Value="Andaman Nicobar">Andaman &amp; Nicobar</asp:ListItem>
                                                            <asp:ListItem Value="Andhra Pradesh">Andhra Pradesh</asp:ListItem>
                                                            <asp:ListItem Value="Arunachal Pradesh">Arunachal Pradesh</asp:ListItem>
                                                            <asp:ListItem Value="Assam">Assam</asp:ListItem>
                                                            <asp:ListItem Value="Bihar">Bihar</asp:ListItem>
                                                            <asp:ListItem Value="Chandigarh">Chandigarh</asp:ListItem>
                                                            <asp:ListItem Value="Chhattisgarh">Chhattisgarh</asp:ListItem>
                                                            <asp:ListItem Value="Dadar Nagar Haveli">Dadar &amp; Nagar Haveli </asp:ListItem>
                                                            <asp:ListItem Value="Daman and Diu">Daman and Diu</asp:ListItem>
                                                            <asp:ListItem Value="Delhi">Delhi </asp:ListItem>
                                                            <asp:ListItem Value="Goa">Goa </asp:ListItem>
                                                            <asp:ListItem Value="Gujrat">Gujrat  </asp:ListItem>
                                                            <asp:ListItem Value="Haryana">Haryana</asp:ListItem>
                                                            <asp:ListItem Value="Himachal Pradesh">Himachal Pradesh</asp:ListItem>
                                                            <asp:ListItem Value="Jammu Kashmir">Jammu &amp; Kashmir </asp:ListItem>
                                                            <asp:ListItem Value="Jharkhand">Jharkhand </asp:ListItem>
                                                            <asp:ListItem Value="Karnataka">Karnataka </asp:ListItem>
                                                            <asp:ListItem Value="Kerala">Kerala </asp:ListItem>
                                                            <asp:ListItem Value="Lakshadweep">Lakshadweep</asp:ListItem>
                                                            <asp:ListItem Value="Madhya Pradesh">Madhya Pradesh</asp:ListItem>
                                                            <asp:ListItem Value="Maharashtra">Maharashtra</asp:ListItem>
                                                            <asp:ListItem Value="Manipur">Manipur </asp:ListItem>
                                                            <asp:ListItem Value="Meghalaya">Meghalaya </asp:ListItem>
                                                            <asp:ListItem Value="Mizoram">Mizoram </asp:ListItem>
                                                            <asp:ListItem Value="Nagaland">Nagaland </asp:ListItem>
                                                            <asp:ListItem Value="Orissa">Orissa </asp:ListItem>
                                                            <asp:ListItem Value="Pondicherry">Pondicherry</asp:ListItem>
                                                            <asp:ListItem Value="Punjab">Punjab</asp:ListItem>
                                                            <asp:ListItem Value="Rajasthan">Rajasthan </asp:ListItem>
                                                            <asp:ListItem Value="Sikkim">Sikkim </asp:ListItem>
                                                            <asp:ListItem Value="Tamil Nadu">Tamil Nadu</asp:ListItem>
                                                            <asp:ListItem Value="Tripura">Tripura</asp:ListItem>
                                                            <asp:ListItem Value="Uttar Pradesh">Uttar Pradesh</asp:ListItem>
                                                            <asp:ListItem Value="Uttaranchal">Uttaranchal </asp:ListItem>
                                                            <asp:ListItem Value="West Bengal">West Bengal</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr id="trHide4">
                                                    <td bgcolor="#ffffff">
                                                        <span class="style3"><span class="style3">&nbsp;Zip Code:</span></span></td>
                                                    <td bgcolor="#ffffff">
                                                        <input name="textfield" type="text" class="hlinks" style="width: 200px" id="txtZip"
                                                            maxlength="8" size="10" runat="server" /></td>
                                                </tr>
                                                <tr id="trHide5">
                                                    <td bgcolor="#ffffff">
                                                        <div align="left">
                                                            <span class="style3">&nbsp;Phone no**:</span></div>
                                                    </td>
                                                    <td bgcolor="#ffffff" class="hlinks">
                                                        <input style="width: 200px" id="txtPhone" maxlength="11" size="15" type="text" name="txtPhone"
                                                            runat="server" /></td>
                                                </tr>
                                                <tr>
                                                    <td bgcolor="#ffffff">
                                                        <div align="left">
                                                            <span class="style3">&nbsp;Mobile no:</span></div>
                                                    </td>
                                                    <td bgcolor="#ffffff" class="hlinks" style="width: 200px; height: 27px" align="left">
                                                        <input id="txtMobile" maxlength="11" size="15" style="width: 200px" type="text" name="txtMobile"
                                                            runat="server"></td>
                                                </tr>
                                                <tr id="trHide6">
                                                    <td bgcolor="#ffffff">
                                                        <div align="left">
                                                            <span class="style3">&nbsp;EMail :</span></div>
                                                    </td>
                                                    <td bgcolor="#ffffff" class="hlinks" style="width: 200px; height: 24px;">
                                                        <input id="txtMail" style="width: 200px" type="text" maxlength="250" name="txtMail"
                                                            runat="server" />
                                                    </td>
                                                </tr>
                                                <tr id="trVerify" runat="server">
                                                    <td colspan="2">
                                                        <table cellspacing="0" cellpadding="4" width="100%" align="center" bgcolor="#ffffff"
                                                            border="0">
                                                            <tr>
                                                                <td class="txt" align="left" width="35%" colspan="2">
                                                                    <label>
                                                                        Enter the Code shown below *:</label></td>
                                                                <td width="64%" colspan="2">
                                                                    <asp:TextBox ID="txtCaptchImage" Width="100px" MaxLength="50" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;<asp:Label
                                                                        ID="MessageLabel" runat="server" CssClass="txt" ForeColor="red"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                </td>
                                                                <td>
                                                                    <asp:Image ID="imgCaptcha" runat="server" ImageUrl="../JpegImage.aspx" Height="40" AlternateText="This is a test to determine whether the user visiting the site is human or an automated program. Automated programs cannot read the distorted letters in the box - only humans can. When you type in the word, it ensures that the page is being accessed by a real person. If you don’t see an image, make sure your browser is set to display images and try again. And if you’re not sure about exactly what the letters are, make your best guess - if you’re wrong you’ll get another chance to enter a different word.">
                                                                    </asp:Image></td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4" align="center" style="height: 27px">
                                                                    <span class="txt">This helps Southern Travels prevent automated Enquiries.</span></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td bgcolor="#ffffff" height="24px" colspan="2">
                                                        ** Either &nbsp;give Phone no/ Mobile or both</td>
                                                </tr>
                                                <tr>
                                                    <td bgcolor="#ffffff" colspan="2" align="center">
                                                        <asp:Button ID="Submit" Text="Send Query" Style="background-color: #5aa1ea" class="cgi1"
                                                            runat="Server" OnClick="Submit_Click" BorderStyle="None" /></td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="right" bgcolor="#E7E7E7">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <img src="Assets/images/left_d_corner.gif" width="8" height="8" loading="lazy" alt="left_d_corner"/></td>
                                        <td bgcolor="#E7E7E7">
                                            <img src="Assets/images/trans.gif" width="1" height="1" loading="lazy" alt="trans"/></td>
                                        <td align="right">
                                            <img src="Assets/images/rgt_d_corner.gif" width="8" height="8" loading="lazy" alt="rgt_d_corner"/></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <uc1:AgentFooter ID="footer1" runat="server" />
                </td>
            </tr>
        </table>
        <input type="hidden" name="stax" id="stax" runat="server" />
    </form>

    <script type="text/javascript">
var gaJsHost = (("https:" == document.location.protocol) ? "https://ssl." : "http://www.");
document.write(unescape("%3Cscript src='" + gaJsHost + "google-analytics.com/ga.js' type='text/javascript'%3E%3C/script%3E"));
    </script>

    <script type="text/javascript">
var pageTracker = _gat._getTracker("UA-4994177-1");
pageTracker._initData();
pageTracker._trackPageview();
    </script>

</body>
</html>

