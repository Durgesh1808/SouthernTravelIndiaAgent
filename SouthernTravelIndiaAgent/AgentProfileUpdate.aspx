<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgentProfileUpdate.aspx.cs" Inherits="SouthernTravelIndiaAgent.AgentProfileUpdate" %>


<%@ Register TagPrefix="uc1" TagName="AgentFooter" Src="UserControls/UcAgentFooter.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AgentHeader" Src="UserControls/UcAgentHeader.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Southern Travels :-: Update Agent Profile</title>    
    <link href="../Assets/css/style.css" rel="stylesheet" type="text/css" />
    <link href="../Assets/css/stylesheet.css" type="text/css" rel="stylesheet" />
    <script language="javascript" src="../Assets/js/MyScript.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">

        function specialcharecter(lVal) {

            var iChars = "!`@#$%^&*()+=-[]\\\';,./{}|\":<>?~_";
            var data = lVal.value;
            var er = false;
            for (var i = 0; i < data.length; i++) {
                if (iChars.indexOf(data.charAt(i)) != -1) {
                    er = true;
                    break;
                }
            }
            if (er == true) {
                alert("Address has special characters. \nThese are not allowed.");
                lVal.focus();
                return false;
            }
        }
    
    function postlimit(control)
    {
	    var maxlength = 200;
	    if (control.value.length > maxlength)
        {
		    alert("you can only type 200 charecters");
		    control.value = control.value.substring(0, maxlength);
	    }
    }    
    function echeck(str) 
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
		
	function CheckValidation()
	{

	    if( (Trim(document.getElementById("txtFname").value)=="") || (Trim(document.getElementById("txtFname").value)=="First Name"))
	    {
		    alert("Please enter First Name");
		    document.form1.txtFname.focus();
		    return false; 
	    }
		if( (Trim(document.getElementById("txtLname").value)=="") || (Trim(document.getElementById("txtLname").value)=="Last Name"))
		{ 
			alert("Please enter last Name");
			document.form1.txtLname.focus();
			return false; 
		}	
		var radSex = document.getElementsByName('rdoGender');
		if( (radSex[0].checked==false ) && (radSex[1].checked==false ))
		{
		    alert('Select your gender');
		    return false;   
		}
		if (document.form1.ddlDay.selectedIndex==0 || document.form1.ddlMonth.selectedIndex==0 || document.form1.ddlYear.selectedIndex==0)
		{
			alert("Please select the correct date of birth");
			return false;
		}		
		if(Trim(document.form1.txtadd.value)=="")
		{
			alert("Please enter Office address");
			document.form1.txtadd.focus();
			return false; 
		}
	    if ((document.form1.txtEmail.value == "") || (document.form1.txtEmail.value == null))
		{ 
			alert("Please enter your Email");
			document.form1.txtEmail.focus();
			return false;
		}	
		if (echeck(Trim(document.form1.txtEmail.value))==false)
		{
			document.getElementById("txtEmail").value = "";
			document.getElementById("txtEmail").focus();
			return false;
		}
		if(Trim(document.form1.txtphone.value)=="")
		{
			alert("Please enter Phone Number");
			document.form1.txtphone.focus();
			return false; 
		}
	    if (document.form1.txtmob.value == "")
	    {
		    alert("Please enter your mobile number")
		    document.form1.txtmob.focus();
		    return false;
	    }
	    else
	    {			
		    var phone = document.form1.txtmob.value;		    
		    if (phone.length != 10)
		    {
			    alert('Please enter 10 Digit Mobile Number')
			    return false;
		    }
		    if (phone.substring(0,1)!='9')
		    {
			    alert('Mobile number should start with digit 9')
			    document.form1.txtmob.focus();
			    return false;
		    }
	    }	
		if(Trim(document.form1.txtFax.value)=="")
		{
			alert("Please enter fax Number");
			document.form1.txtFax.focus();
			return false; 
		}
	}
	 function FillTxt()
      {
	   var sf2;
	   if(document.getElementById("txtFname")!=null)
		 sf2 = document.getElementById("txtFname");
	  else
		 sf2 = document.getElementById("txtFname");
	
	 if(sf2.value=="")
	   {
		sf2.value="First Name";
		sf2.style.color="#cccccc";
		demoText=false;
	  }
	 else
	  {
		sf2.style.color="#000000";
		demoText=true;
	  }
	
	if(sf2.value=="First Name")
		sf2.style.color="#cccccc";
   }

function FillTxtF()
 {
	var si2;
	if(document.getElementById("txtLname")!=null)
		si2 = document.getElementById("txtLname");
	else
		si2 = document.getElementById("txtLname");
	
	if(si2.value=="")
	{
		si2.value="Last Name";
		si2.style.color="#cccccc";
	}
	else
	{
		si2.style.color="#000000";
	}
	
	if(si2.value=="Last Name")
		si2.style.color="#cccccc";
}
function ClearTxt()
{
	var sf;
	if(document.getElementById("txtFname")!=null)
		sf = document.getElementById("txtFname");
	else
		sf = document.getElementById("txtFname");
	
	if(sf.value=="First Name")
	{
		sf.value="";
		sf.style.color="#000000";
		demoText=false;
	}
	else
	{
		demoText=true;
	}
}

function ClearTxtF()
{
	var si;
	if(document.getElementById("txtLname")!=null)
		si = document.getElementById("txtLname");
	else
		si = document.getElementById("txtLname");
	
	if(si.value=="Last Name")
	{
		si.value="";
		si.style.color="#000000";
	}
}

    function checknumber()
	{
	    var kk
	    kk=event.keyCode
	    if (event.shiftKey) return false;	
	    if ((kk>=96 && kk<=105) || (kk>=48 && kk<=57) || kk==8 || kk==190 || kk==110 || kk==9 || kk==35 || kk==36 || kk==37 || kk==38 || kk==39 || kk==40 || kk==46)
	    {   
	       return true;	   
	    }
	   return false;
	}
    </script>

</head>
<body class="Body">
    <form id="form1" runat="server">
        <div style="width: 100%;">
            <uc1:AgentHeader ID="agentHeader" runat="server" />
        </div>
        <div style="width: 98%; margin-left: 10px; margin-right: 10px;">
            <table width="600" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td align="left" style="width: 9px">
                        <img src="Assets/images/left_corner.gif" width="8" height="8" /></td>
                    <td bgcolor="#E7E7E7">
                        <img src="Assets/images/trans.gif" width="280" height="1" /></td>
                    <td align="right">
                        <img src="Assets/images/rgt_corner.gif" width="8" height="8" /></td>
                </tr>
                <tr>
                    <td align="left" bgcolor="#E7E7E7" style="width: 9px">
                        &nbsp;</td>
                    <td bgcolor="#E7E7E7">
                        <table width="600" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td width="600" height="35" align="center" valign="middle">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td colspan="3" align="center" background="Assets/images/bg1.gif" class="verdana14w" style="height: 24px">
                                                &nbsp;&nbsp;&nbsp;&nbsp; Update Details
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" align="right" class="verdana11bk">
                                                <img src="Assets/images/trans.gif" width="1" height="1" /></td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" align="right" bgcolor="#F2F9FF" class="verdana11bk">
                                                <img src="Assets/images/trans.gif" width="1" height="10" /></td>
                                        </tr>
                                        <tr>
                                            <td width="40%" height="30" align="right" bgcolor="#F2F9FF" class="verdana11bk">
                                                My Name :
                                            </td>
                                            <td width="1%" align="center" bgcolor="#F2F9FF" class="verdana11">
                                                <img src="Assets/images/trans.gif" width="25" height="30" /></td>
                                            <td width="50%" align="left" bgcolor="#F2F9FF" class="verdana11">
                                                <input name="textfield7" type="text" class="verdana11bk" id="txtFname" runat="server"
                                                    maxlength="25" onfocus="JavaScript:ClearTxt();" onblur="JavaScript:FillTxt();" />
                                                &nbsp;
                                                <input name="textfield23" type="text" class="verdana11bk" id="txtLname" runat="server"
                                                    maxlength="25" onfocus="JavaScript:ClearTxtF();" onblur="JavaScript:FillTxtF();" /></td>
                                        </tr>
                                        <tr>
                                            <td height="30" align="right" bgcolor="#F2F9FF" class="verdana11bk">
                                                <p>
                                                    Gender :</p>
                                            </td>
                                            <td height="22" align="center" bgcolor="#F2F9FF" class="verdana11bk">
                                                &nbsp;</td>
                                            <td height="22" align="left" bgcolor="#F2F9FF" class="verdana11bk">
                                                <span class="verdana11">
                                                    <asp:RadioButton ID="rdoMale" runat="server" Text="Male" GroupName="rdoGender" CssClass="verdana11bk">
                                                    </asp:RadioButton>&nbsp;
                                                    <asp:RadioButton ID="rdoFemale" runat="server" Text="Female" GroupName="rdoGender"
                                                        CssClass="verdana11"></asp:RadioButton>&nbsp;</span></td>
                                        </tr>
                                        <tr>
                                            <td height="30" align="right" bgcolor="#F2F9FF" class="verdana11bk">
                                                <p>
                                                    Date of Birth :</p>
                                            </td>
                                            <td height="22" align="center" bgcolor="#F2F9FF" class="verdana11bk">
                                                &nbsp;</td>
                                            <td height="22" align="left" bgcolor="#F2F9FF" class="verdana11bk">
                                                <span class="verdana11">
                                                    <asp:DropDownList ID="ddlDay" runat="server" CssClass="verdana11">
                                                    </asp:DropDownList>&nbsp;<asp:DropDownList ID="ddlMonth" runat="server" CssClass="verdana11bk">
                                                    </asp:DropDownList>&nbsp;<asp:DropDownList ID="ddlYear" runat="server" CssClass="verdana11bk">
                                                    </asp:DropDownList>&nbsp;</span></td>
                                        </tr>
                                        <tr>
                                            <td height="30" align="right" bgcolor="#F2F9FF" class="verdana11bk">
                                                <p>
                                                    Address :</p>
                                            </td>
                                            <td height="22" align="center" bgcolor="#F2F9FF" class="verdana11bk">
                                                &nbsp;</td>
                                            <td height="22" align="left" bgcolor="#F2F9FF" class="verdana11bk">
                                                <span class="verdana11bk"><textarea id="txtadd" runat="server" cols="33" rows="4"
                                                    onkeydown="postlimit(this)" onkeyup="postlimit(this)" onchange="return specialcharecter(this)" ></textarea></span></td>
                                        </tr>
                                        <tr>
                                            <td height="30" align="right" bgcolor="#F2F9FF" class="verdana11bk">
                                                <p>
                                                    City :</p>
                                            </td>
                                            <td height="22" align="center" bgcolor="#F2F9FF" class="verdana11bk">
                                                &nbsp;</td>
                                            <td height="22" align="left" bgcolor="#F2F9FF" class="verdana11bk">
                                                <span class="verdana11"><input name="textfield2272" type="text" class="verdana11bk" runat="server" id="txtcity" /></span></td>
                                        </tr>
                                        <tr>
                                            <td align="right" bgcolor="#F2F9FF" class="verdana11bk" style="height: 30px">
                                                <p>
                                                    E-mail :</p>
                                            </td>
                                            <td align="center" bgcolor="#F2F9FF" class="verdana11bk" style="height: 30px">
                                                &nbsp;</td>
                                            <td align="left" bgcolor="#F2F9FF" class="verdana11bk" style="height: 30px">
                                                <span class="verdana11">
                                                    <input name="textfield2272" type="text" class="verdana11bk" size="45" runat="server"
                                                        id="txtEmail" maxlength="75" />
                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" bgcolor="#F2F9FF" class="verdana11bk" style="height: 30px">
                                                <p>
                                                    Tele Phone Number :</p>
                                            </td>
                                            <td align="center" bgcolor="#F2F9FF" class="verdana11bk" style="height: 30px">
                                                &nbsp;</td>
                                            <td align="left" bgcolor="#F2F9FF" class="verdana11bk" style="height: 30px">
                                                <span class="verdana11">
                                                    <input name="textfield22922" type="text" class="verdana11bk" id="txtphone" runat="server"
                                                        maxlength="14" />
                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" bgcolor="#F2F9FF" class="verdana11bk" style="height: 30px">
                                                <p>
                                                    Mobile :</p>
                                            </td>
                                            <td align="center" bgcolor="#F2F9FF" class="verdana11bk" style="height: 30px">
                                                &nbsp;</td>
                                            <td align="left" bgcolor="#F2F9FF" class="verdana11bk" style="height: 30px">
                                                <span class="verdana11">
                                                    <input name="textfield2283" type="text" class="verdana11bk" runat="server" id="txtmob"
                                                        maxlength="10" />
                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="30" align="right" bgcolor="#F2F9FF" class="verdana11bk">
                                                <p>
                                                    Fax Number :</p>
                                            </td>
                                            <td height="22" align="center" bgcolor="#F2F9FF" class="verdana11bk">
                                                &nbsp;</td>
                                            <td height="22" align="left" bgcolor="#F2F9FF" class="verdana11bk">
                                                <span class="verdana11">
                                                    <input name="textfield2262" type="text" class="verdana11bk" id="txtFax" runat="server"
                                                        maxlength="14" />
                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="30" align="right" bgcolor="#F2F9FF" class="verdana11bk">
                                                <p>
                                                    Pan Number :</p>
                                            </td>
                                            <td height="22" align="center" bgcolor="#F2F9FF" class="verdana11bk">
                                                &nbsp;</td>
                                            <td height="22" align="left" bgcolor="#F2F9FF" class="verdana11bk">
                                                <span class="verdana11">
                                                    <asp:TextBox ID="txtPanNo" runat="server" MaxLength="15" class="verdana11bk" > </asp:TextBox>
                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="30" align="right" bgcolor="#F2F9FF" class="verdana11bk">
                                                &nbsp;</td>
                                            <td height="22" align="center" bgcolor="#F2F9FF" class="verdana11bk">
                                                &nbsp;</td>
                                            <td height="22" align="left" bgcolor="#F2F9FF" class="verdana11bk">
                                                <a href="agenthomepage.aspx">
                                                    <asp:ImageButton ID="btnUpdate" runat="server" ImageUrl="Assets/images/update.gif" OnClick="btnUpdate_Click" /></a>&nbsp;&nbsp;&nbsp;&nbsp;<a
                                                        href="agenthomepage.aspx"><img id="ImageButton2" alt="" border="0" runat="server"
                                                            src="Assets/images/cancel1.gif" />
                                                    </a>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" bgcolor="#F2F9FF" class="verdana11bk" style="height: 30px">
                                                &nbsp;</td>
                                            <td align="center" bgcolor="#F2F9FF" class="verdana11bk" style="height: 30px">
                                                &nbsp;</td>
                                            <td align="left" bgcolor="#F2F9FF" class="verdana11bk" style="height: 30px">
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td align="right" bgcolor="#E7E7E7">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="left" style="width: 9px">
                        <img src="Assets/images/left_d_corner.gif" width="8" height="8" /></td>
                    <td bgcolor="#E7E7E7">
                        <img src="Assets/images/trans.gif" width="1" height="1" /></td>
                    <td align="right">
                        <img src="Assets/images/rgt_d_corner.gif" width="8" height="8" /></td>
                </tr>
            </table>
            <div>
                <uc1:AgentFooter ID="Footer1" runat="server"></uc1:AgentFooter>
            </div>
        </div>
    </form>

    <script language="javascript" type="text/javascript">
        <!-- 
            FillTxt();
            FillTxtF();
        -->
    </script>

</body>
</html>
