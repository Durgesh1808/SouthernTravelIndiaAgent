<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgentEnquiry.aspx.cs" Inherits="SouthernTravelIndiaAgent.AgentEnquiry" %>

<%@ Register TagPrefix="uc1" TagName="agentheader" Src="UserControls/UcAgentHeader.ascx" %>
<%@ Register TagPrefix="uc1" TagName="agentRightControl" Src="UserControls/UcAgentRightUsc.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Southern Travels :-: Agent Enquiry</title>
    <link href="../Assets/css/style.css" rel="stylesheet" type="text/css" />
    <link href="../Assets/css/Calenderall.css" rel="Stylesheet" type="text/css" />
    <script language="javascript" src="../Assets/js/calenderall.js" type="text/javascript"></script> 
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
        }  function isNumberKey(evt)
        {
             var charCode = (evt.which) ? evt.which : event.keyCode
             if (charCode > 46 && charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
             return true;
        }   
        	function CheckOnlyCharacter(evt)
	{
			var kk
			kk= (evt.which) ? evt.which : event.keyCode
			
			if((kk>=65 && kk<=90)|| kk==8 ||kk==32 || kk==9 || kk==127 || kk==16 || kk==20|| kk==46)
			 {
				return true;
			 }
				alert("Please enter characters only.");
				return false;
    }		
        function validate()
		{	   
            if(Trim(document.getElementById("txtfirstname").value)=="")    
			{			
				alert("First name should not be Empty.");
				document.getElementById("txtfirstname").focus();				
				return false;
			}
			if (validateOnlyNumber1(parseInt(document.getElementById("txtfirstname").value))==true)
			{
				alert("First name should not be numeric.");
				document.getElementById("txtfirstname").value="";
				document.getElementById("txtfirstname").focus();
				return false;
			}
			
			if (Trim(document.getElementById("txtlastname").value)=="")    
			{			
				alert("Last name should not be Empty.");
				document.getElementById("txtlastname").focus();				
				return false;
			}
			var  rbgender = document.getElementsByName("RadioButtonList2");
			var vargenderChecked=false;
			for(var i=0; i< rbgender.length; i++)
			{
			    if(rbgender[i].type=='radio' && rbgender[i].checked==true)		    
			    {			    
			        vargenderChecked = true;
			        break;    
			    }			    
			}			
			if(!vargenderChecked)
			{
			    alert('Select Gender');			    
			    return false;
			}
		  if(Trim(document.getElementById("txtaddress").value)=="")    
			{				
				alert("Address should not be Empty.");
				document.getElementById("txtaddress").focus();
				return false;
			}
			
			if (document.getElementById("txtphone").value=="")   
			{				
				alert("Phone No should not be Empty.");
				document.getElementById("txtphone").focus();
				return false;
			}
		
			if(document.getElementById("txtphone").value!="")
			{
			    var k=document.getElementById("txtphone").value;
			   if(k.length<6)   
			    {
				    alert("Phone No should be 6 digits.");
				    document.getElementById("txtphone").focus();
				    return false;
			    }
			}
			
		    if (document.getElementById("txtmobile").value=="")   
			{				
				alert("Mobile No should not be Empty.");
				document.getElementById("txtmobile").focus();
				return false;
			}
			if(document.getElementById("txtmobile").value!="")
			{
			        var k=document.getElementById("txtmobile").value;
			   if(k.length<10)   
			    {
				    alert("Mobile No should be 10 digits.");
				    document.getElementById("txtmobile").focus();
				    return false;
			    }
			}
			 if (document.getElementById('txtemail').value== "" )	
             {
                alert("Please fill the e-mail field.It is Mandatory.");
                document.getElementById('txtemail').focus();
                return false;
             }
	        else
	        {
		        if (document.getElementById('txtemail').value!= "" )
		        {
			        if (CheckMail(document.getElementById('txtemail').value)== false )
			        {
			            //alert("Plese enter your valid email Id.");
				        document.getElementById('txtemail').value="";
				        document.getElementById('txtemail').focus();
				        return false;
			         } 
			    }	    
		    }	
		    
//		  if(Trim(document.form1.txtfax.value)=="")
//		  {
//				alert("Please enter fax Number");
//				document.form1.txtfax.focus();
//				return false; 
//		  }	
		  if(document.form1.txtCaptchImage.value=="")
			{
				alert("Please enter verification code.");
				document.form1.txtCaptchImage.focus();
				return false; 
			}				
	}
		
    function chkNumeric()
	{
	    
		if(event.shiftKey) return false;
 		if((event.keyCode<48 || event.keyCode>57) && event.keyCode != 8 && event.keyCode != 37 && event.keyCode != 38 && event.keyCode != 39 && event.keyCode != 40 && event.keyCode != 46 && event.keyCode != 13 && event.keyCode != 116 && event.keyCode != 16 && (event.keyCode <96 || event.keyCode > 105) && event.keyCode != 9 && event.keyCode != 110) event.returnValue = false
	} 
	
	function chkCharacter()
	{
		
 		if((event.keyCode > 90) || (event.keyCode < 65) && event.keyCode != 8 && event.keyCode != 37 && event.keyCode != 38 && event.keyCode != 39 && event.keyCode != 40 && event.keyCode != 46 && event.keyCode != 13 && event.keyCode != 116 && event.keyCode != 16 && (event.keyCode <96 || event.keyCode > 105) && event.keyCode != 9 && event.keyCode != 110) event.returnValue = false
 		
	}
	

    
    </script>

</head>
<body>   
    <form id="form1" runat="server">
        <div>
            <table width="1001" border="0" align="right" cellpadding="0" cellspacing="0">
                <tr>
                    <td colspan="2" valign="top">
                        <uc1:agentheader ID="agHeader" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td valign="top" width="705">
                        <table cellpadding="1" cellspacing="0" border="0" width="700" align="right" bgcolor="#348DF7">
                            <tr>
                                <td>
                                    <table class="verdana11bk" cellpadding="1" cellspacing="0" border="0" width="100%"
                                        bgcolor="#ffffff">
                                        <tr>
                                            <td bgcolor="#348DF7" class="verdana14w" align="center" colspan="4" style="height: 30px">
                                                <b><span align="center">&nbsp;Agent Registration Details<br />
                                                </span></b>
                                            </td>
                                        </tr>
                                        <tr height="21px">
                                            <td colspan="4">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="height: 26px; width: 142px;">
                                                &nbsp;FirstName*
                                            </td>
                                            <td style="height: 26px; width: 181px;">
                                                <asp:TextBox ID="txtfirstname" runat="server" Width="151px" /><br />
                                            </td>
                                            <td align="right" style="height: 26px; width: 107px;">
                                                &nbsp;LastName*</td>
                                            <td style="height: 26px">
                                                <asp:TextBox ID="txtlastname" runat="server" Width="152px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 142px; height: 10px">
                                            </td>
                                            <td style="height: 10px; width: 181px;">
                                            </td>
                                            <td align="right" style="height: 10px; width: 107px;">
                                            </td>
                                            <td style="height: 10px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 142px">
                                                &nbsp;Date of Birth</td>
                                            <td style="width: 181px">
                                                <%--<asp:TextBox ID="txtdob" runat="server" MaxLength="0" ReadOnly="True" Width="152px"></asp:TextBox>
                                                <img  loading="lazy" alt="" id="btn1" style="cursor: hand" onclick="javascript:cbfshowcalendar('form1', 'txtdob', 'btn1');event.cancelBubble=true;"
                                                    alt="View calendar" src="images/calendar.gif" value="Calendar" />--%>
                                                
                                                <input id="txtdob" type="text" size="8" name="txtdob" runat="server" maxlength="10" readonly="readonly" onclick="showCalendarControl(this);" />                                  
                                                <img  loading="lazy" id="Img1" style="cursor: hand" onclick="showCalendarControl(document.getElementById('txtdob'));" alt="View calendar" src="Assets/images/calendar.gif" value="Calendar" />
                                                    
                                            </td>
                                            <td align="right" style="width: 107px">
                                                &nbsp;Gender*</td>
                                            <td>
                                                <asp:RadioButtonList ID="RadioButtonList2" runat="server" RepeatDirection="Horizontal"
                                                    RepeatLayout="Flow" Width="152px">
                                                    <asp:ListItem Value="M">Male</asp:ListItem>
                                                    <asp:ListItem Value="F">Female</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 142px; height: 10px">
                                            </td>
                                            <td align="justify" colspan="3" style="height: 10px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="height: 39px; width: 142px;">
                                                &nbsp;Address*</td>
                                            <td align="justify" style="height: 39px; width: 181px;">
                                                <asp:TextBox ID="txtaddress" runat="server" MaxLength="200" Width="152px" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                            <td align="right" style="width: 107px">
                                                City</td>
                                            <td>
                                                <asp:TextBox ID="txtcity" runat="server" Width="152px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 142px;">
                                                &nbsp;Country</td>
                                            <td style="width: 181px;">
                                                <br />
                                                <select id="ddlcountry" runat="server" class="intdtext" name="ddlcountry" style="width: 152px;">
                                                    <option selected="selected" value="0">-------Select-------</option>
                                                    <option value="India">India</option>
                                                    <option value="Nepal">Nepal</option>
                                                </select>
                                            </td>
                                            <td align="right" style="width: 107px">
                                                State</td>
                                            <td>
                                                <select id="ddlstate" runat="server" class="intdtext" style="width: 152px;">
                                                    <option selected="selected" value="0">-------Select-------</option>
                                                    <option value="Andhra Pradesh">Andhra Pradesh</option>
                                                    <option value="Andaman Nicobar">Andaman &amp; Nicobar</option>
                                                    <option value="Arunachal Pradesh">Arunachal Pradesh</option>
                                                    <option value="Assam">Assam</option>
                                                    <option value="Bihar">Bihar</option>
                                                    <option value="Chhattisgarh">Chhattisgarh</option>
                                                    <option value="Dadar Nagar Haveli">Dadar &amp; Nagar Haveli</option>
                                                    <option value="Delhi">Delhi</option>
                                                    <option value="Goa">Goa</option>  
                                                    <option value="Gujrat">Gujrat</option>
                                                    <option value="Haryana">Haryana</option>  
                                                    <option value="Himachal Pradesh">Himachal Pradesh</option>
                                                    <option value="Jammu and Kashmir">Jammu &amp; Kashmir </option>
                                                    <option value="Jharkhand">Jharkhand</option>  
                                                    <option value="Karnataka">Karnataka</option> 
                                                    <option value="Kerala">Kerala</option>  
                                                    <option value="Lakshadweep">Lakshadweep</option>
                                                    <option value="Madhya Pradesh">Madhya Pradesh</option>
                                                    <option value="Maharashtra">Maharashtra</option> 
                                                    <option value="Manipur">Manipur</option> 
                                                    <option value="Meghalaya">Meghalaya</option>
                                                    <option value="Mizoram">Mizoram</option>    
                                                    <option value="Nagaland">Nagaland</option>
                                                    <option value="Orissa">Orissa</option>
                                                    <option value="Pondicherry">Pondicherry</option>          
                                                    <option value="Punjab">Punjab</option>
                                                    <option value="Rajasthan">Rajasthan</option>
                                                    <option value="Sikkim">Sikkim</option>        
                                                    <option value="Tamil Nadu">Tamil Nadu</option>         
                                                    <option value="Tripura">Tripura</option>
                                                    <option value="Uttaranchal">Uttaranchal</option>                                                               
                                                    <option value="Uttar Pradesh">Uttar Pradesh</option>                                                    
                                                    <option value="West Bengal">West Bengal</option>                                                     
                                                </select>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 142px; height: 10px">
                                            </td>
                                            <td style="height: 10px; width: 181px;">
                                            </td>
                                            <td align="right" style="height: 10px; width: 107px;">
                                            </td>
                                            <td style="height: 10px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 142px">
                                                PhoneNo*</td>
                                            <td style="width: 181px">
                                                <asp:TextBox ID="txtphone" runat="server" Width="152px" MaxLength="8"></asp:TextBox></td>
                                            <td align="right" style="width: 107px">
                                                &nbsp;Pin</td>
                                            <td>
                                                <asp:TextBox ID="txtpin" runat="server" Width="152px" MaxLength="6"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 142px; height: 10px">
                                            </td>
                                            <td style="height: 10px; width: 181px;">
                                            </td>
                                            <td align="right" style="height: 10px; width: 107px;">
                                            </td>
                                            <td style="height: 10px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="height: 26px; width: 142px;">
                                                &nbsp;FaxNo&nbsp;</td>
                                            <td style="height: 26px; width: 181px;">
                                                <asp:TextBox ID="txtfax" runat="server" Width="152px" MaxLength="11"></asp:TextBox></td>
                                            <td align="right" style="height: 26px; width: 107px;">
                                                &nbsp;MobileNo*</td>
                                            <td style="height: 26px">
                                                <asp:TextBox ID="txtmobile" runat="server" MaxLength="11" Width="152px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 142px; height: 10px">
                                            </td>
                                            <td style="height: 10px; width: 181px;">
                                            </td>
                                            <td align="right" style="height: 10px; width: 107px;">
                                            </td>
                                            <td style="height: 10px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="height: 26px; width: 142px;">
                                                AuthorityMember</td>
                                            <td style="height: 26px; width: 181px;">
                                                <select id="ddlauthority" runat="server" class="intdtext" name="ddlauthority" style="width: 158px;">
                                                    <option selected="selected" value="0">-------Select-------</option>
                                                    <option value="IATO">IATO</option>
                                                    <option value="AIMTC">AIMTC</option>
                                                    <option value="AIMA">AIMA</option>
                                                    <option value="IITA">IITA</option>
                                                    <option value="FHRAI">FHRAI</option>
                                                    <option value="HRAI">HRAI</option>
                                                </select>
                                            </td>
                                            <td align="right" style="height: 26px; width: 107px;">
                                                &nbsp;EmailId*</td>
                                            <td style="height: 26px">
                                                <asp:TextBox ID="txtemail" runat="server" Width="152px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 142px; height: 10px">
                                            </td>
                                            <td style="height: 10px; width: 181px;">
                                            </td>
                                            <td align="right" style="height: 10px; width: 107px;">
                                            </td>
                                            <td style="height: 10px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 142px">
                                            </td>
                                            <td style="width: 181px">
                                                &nbsp;<br />
                                            </td>
                                            <td align="right" style="width: 107px">
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="height: 26px; width: 142px;">
                                                Pan No.</td>
                                            <td style="height: 26px; width: 181px;">
                                               
                                               <asp:TextBox ID="txtPanNo" runat="server" Width="152px" MaxLength="15"></asp:TextBox>
                                            </td>
                                            <td align="right" style="height: 26px; width: 107px;">
                                                &nbsp;Description</td>
                                            <td style="height: 26px">
                                                 <asp:TextBox ID="TextBox1" runat="server" Height="90px" TextMode="MultiLine" Width="268px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        
                                        <tr id="trVerify" runat="server">
                                            <td colspan="4">
                                                <table cellspacing="0" cellpadding="4" width="100%" align="center" bgcolor="#ffffff"
                                                    border="0">
                                                    <tr>
                                                        <td class="verdana11bk" align="right" width="30%" colspan="2">
                                                            <label>
                                                                Enter the Code shown below*</label></td>
                                                        <td width="69%" colspan="2">
                                                            <asp:TextBox ID="txtCaptchImage" Width="100px" MaxLength="50" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;<asp:Label
                                                                ID="MessageLabel" runat="server" CssClass="verdana11bk" ForeColor="red"></asp:Label>
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
                                                        <td colspan="4" align="center">
                                                            <span class="verdana11bk">This helps Southern Travels prevent automated Enquiries.</span></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" align="center" style="height: 26px">
                                                <asp:Button ID="SendRequest" Text="Send Request" runat="server" OnClick="SendRequest_Click1" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="296" align="right" valign="top">
                        <table width="296" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <uc1:agentRightControl ID="agentRight" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>

