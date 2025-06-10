<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgentForgotPassword.aspx.cs" Inherits="SouthernTravelIndiaAgent.AgentForgotPassword" %>


<%@ Register TagPrefix="uc1" TagName="Footer" Src="UserControls/UcAgentFooter.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AgentHeader" Src="UserControls/UcAgentHeader1.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Southern Travels :-: Agent Forgot Password</title>   
    <link href="../Assets/css/style.css" rel="stylesheet" type="text/css" />
    <link href="../Assets/css/stylesheet.css" type="text/css" rel="stylesheet" />

    <script language="javascript" src="../Assets/js/MyScript.js" type="text/javascript"></script>

    <script language="javascript" src="../Assets/js/md5.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
    function isEmail (s)
			{  
				// there must be >= 1 character before @, so we
				// start looking at character position 1 
				// (i.e. second character)
				var i = 1;
				var sLength = s.length;
				// look for @
				while ((i < sLength) && (s.charAt(i) != "@"))
				{
					i++;
				}

				if ((i >= sLength) || (s.charAt(i) != "@"))
				 return false;
				else i += 2;

				// look for .
				while ((i < sLength) && (s.charAt(i) != "."))
				{ i++;
				}

				// there must be at least one character after the .
				if ((i >= sLength - 1) || (s.charAt(i) != ".")) 
				return false;
				else
				 return true;
			}
			function validation()
			{
				if(!isEmail(Trim(document.form1.txtmail.value)))
					{
					alert("Please enter valid Email Id");
					document.form1.txtmail.value="";
					document.form1.txtmail.focus();
					return false;
					}
					
					}
			function fnMd5(saltval)
			{
	            
		        var encpass=hex_md5(saltval);
		        //alert(encpass);
		        document.getElementById('tmpEnValue').value= encpass;
		        return true;
		       //alert(document.form1.tmpEnValue.value);		
					 
				//return false;	
				
			}
			
			 			 
			
    </script>

</head>
<body class="Body">
    <form id="form1" runat="server">
        <input type="hidden" id="tmpEnValue" runat="server" />
        <div style="width: 100%;">
            <uc1:AgentHeader ID="agentHeader" runat="server" />
        </div>
        <br />
        <br />
        <%--<div style="width: 98%; margin-left: 10px; margin-right: 10px;">--%>
        <table width="600" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td align="left" style="width: 9px">
                    <img src="Assets/images/left_corner.gif" width="8" height="8" /></td>
                <td bgcolor="#E7E7E7">
                    <img src="Assets/images/trans.gif" width="280" height="1" /></td>
                <td align="right">
                    <img src="Assets/images/rgt_corner.gif" width="8" height="8" /></td>
            </tr>
            <tr valign="middle">
                <td align="left" bgcolor="#E7E7E7" style="width: 9px">
                    &nbsp;</td>
                <td bgcolor="#E7E7E7">
                    <table width="600" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td width="600" height="35" align="center" valign="middle">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td colspan="3" style="height: 40px" align="center" valign="middle" background="Assets/images/bg1.gif"
                                            class="verdana14w">
                                            Forgot Password
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <img src="Assets/images/trans.gif" width="1" height="1" /></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" bgcolor="#F2F9FF">
                                            <img src="Assets/images/trans.gif" width="1" height="5" /></td>
                                    </tr>
                                    <tr>
                                        <td height="30" bgcolor="#F2F9FF">
                                            &nbsp;</td>
                                        <td width="169" height="30" align="right" bgcolor="#F2F9FF" class="verdana11bk">
                                            &nbsp;&nbsp;E-Mail</td>
                                        <td align="left" bgcolor="#F2F9FF" class="verdana11bk">
                                            :
                                            <asp:TextBox ID="txtmail" runat="server" Width="184px"></asp:TextBox>
                                            <input id="txtgenerate" type="hidden" runat="server" /></td>
                                    </tr>
                                    <tr>
                                        <td height="30" align="right" bgcolor="#F2F9FF" class="verdana11bk">
                                            &nbsp;</td>
                                        <td height="22" align="center" bgcolor="#F2F9FF" class="verdana11bk">
                                            &nbsp;</td>
                                        <td height="22" align="left" bgcolor="#F2F9FF" class="verdana11bk">
                                            <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="Assets/images/submit_blue.jpg" OnClick="btnAdd_Click" />
                                            &nbsp;&nbsp;<asp:ImageButton ID="btnCancel" runat="server" ImageUrl="~/Assets/images/cancel.gif"
                                                OnClick="btnCancel_Click" />
                                        </td>
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
            <uc1:Footer ID="Footer1" runat="server"></uc1:Footer>
        </div>
        <input type="hidden" value='<%=ViewState["txtpass"]%>' id="txtsalt" />
    </form>
</body>
</html>
