<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgentLogin.aspx.cs" Inherits="SouthernTravelIndiaAgent.AgentLogin" %>

<%@ Register TagPrefix="uc1" TagName="AgentHeader" Src="UserControls/UcAgentHeader.ascx" %>
<%@ Register TagPrefix="uc1" TagName="agentFooter" Src="UserControls/UcAgentFooter.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="h1" runat="server"  >
    <title>Southern Travels :-: Agent Login</title>    
    <link href="../Assets/css/stylesheet.css" type="text/css" rel="stylesheet" />
    <link href="../Assets/css/style.css" rel="stylesheet" type="text/css" />

    <script language="javascript" src="../Assets/js/MyScript.js" type="text/Assets/js"></script>
    <script language="javascript" src="../Assets/js/md5.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
		function validate()
        {
            debugger;
            if (document.getElementById("txtagentid").value.trim() === "") {
                alert("Please enter your User Name");
                document.form1.txtagentid.focus();
                return false;
            }
 
            if (document.getElementById("txtPassword").value.trim() === "") {
                alert("Please enter your Password");
                document.form1.txtPassword.focus();
                return false;
            }
 	
			 
	            var pass;
		        pass=document.getElementById("txtPassword").value;
		        encpass=hex_md5(pass);
		      var k= encpass+(document.getElementById('txtsalt').value);
            document.getElementById("txtPassword").value = k;	 			 					
		}
    </script>

</head>
<body topmargin="0" leftmargin="0" onload="document.getElementById('txtagentid').focus();">
    <form id="form1" runat="server" method="post">
        &nbsp;<table cellpadding="0" cellspacing="0" align="center" border="0">
            <tr>
                <td>
                    <uc1:AgentHeader ID="AgentHeader" runat="server" />
                </td>
            </tr>
            <tr>
                <td height="300" valign="middle" align="center">
                    <table width="500" cellspacing="0" cellpadding="0" border="0">
                        <tbody>
                            <tr>
                                <td align="left">
                                    <img width="8" height="8" src="Assets/images/left_corner.gif" /></td>
                                <td bgcolor="#e7e7e7">
                                    <img width="280" height="1" src="Assets/images/trans.gif" /></td>
                                <td align="right">
                                    <img width="8" height="8" src="Assets/images/rgt_corner.gif" /></td>
                            </tr>
                            <tr>
                                <td bgcolor="#e7e7e7" align="left">
                                </td>
                                <td align="center">
                                    <table width="500" cellspacing="0" cellpadding="0" border="0">
                                        <tbody>
                                            <tr>
                                                <td height="39" background="Assets/images/login_blue.jpg" align="center" class="verdana14w">
                                                    Agent Login</td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <img width="1" height="1" src="Assets/images/trans.gif" /></td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="center">
                                                    <table width="500" align="center" cellspacing="0" cellpadding="0" border="0">
                                                        <tbody>
                                                            <tr>
                                                                <td valign="middle" height="42" bgcolor="#d6f0fe" align="center" class="verdana12pb"
                                                                    colspan="3">
                                                                    <span style="visibility: hidden">Enter a Valid UserID and Password. Than Click the “login”
                                                                        button</span>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="38" bgcolor="#d6f0fe" align="right">
                                                                </td>
                                                                <td bgcolor="#d6f0fe" align="left">
                                                                    <span class="verdana11bk">Agent ID</span></td>
                                                                <td bgcolor="#d6f0fe" align="left">
                                                                    <span class="verdana11bk"></span>
                                                                    <asp:TextBox ID="txtagentid" runat="server" Width="150px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 3px;">
                                                                <td bgcolor="#d6f0fe" align="right" colspan="2">
                                                                </td>
                                                                <td bgcolor="#d6f0fe">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td bgcolor="#d6f0fe" align="right">
                                                                </td>
                                                                <td bgcolor="#d6f0fe" align="left">
                                                                    <span class="verdana11bk">Password</span></td>
                                                                <td valign="middle" bgcolor="#d6f0fe" align="left">
                                                                    <span class="verdana11bk"></span>
                                                                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="150px"></asp:TextBox></td>
                                                            </tr>
                                                            <tr style="height: 5px">
                                                                <td bgcolor="#d6f0fe" colspan="2" style="height: 2px">
                                                                </td>
                                                                <td bgcolor="#d6f0fe" style="height: 2px">
                                                                    <a class="verdana11bk" href="#" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td bgcolor="#d6f0fe" colspan="3" align="center">
                                                                    <asp:ImageButton ID="Button1" BorderWidth="0" ImageUrl="Assets/images/login_bluee.gif"
                                                                        runat="server" OnClientClick="javascript:return validate()" />
                                                                    <br />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td bgcolor="#d6f0fe" colspan="2" align="center" style="height: 15px">
                                                                    <a class="verdana11bk" href="AgentForgotPassword.aspx">Forgot Password ?</a>
                                                                </td>
                                                                <td bgcolor="#d6f0fe" colspan="2" align="center" style="height: 15px">
                                                                    <a class="verdana11bk" href="AgentEnquiry.aspx">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                        New Agent Registration</a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td bgcolor="#d6f0fe" colspan="3" height="20">
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <img width="1" height="1" src="Assets/images/trans.gif" /></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                                <td bgcolor="#e7e7e7" align="right">
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="height: 8px">
                                    <img width="8" height="8" src="Assets/images/left_d_corner.gif" /></td>
                                <td bgcolor="#e7e7e7" style="height: 8px">
                                    <img width="1" height="1" src="Assets/images/trans.gif" /></td>
                                <td align="right" style="height: 8px">
                                    <img width="8" height="8" src="Assets/images/rgt_d_corner.gif" /></td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
            <tr>
                <td height="50px" valign="bottom">
                    <uc1:agentFooter ID="AgentFooter1" runat="server" />
                </td>
            </tr>
        </table>
    </form>
    <input type="hidden" value='<%=sSalt%>' id="txtsalt" />
</body>
</html>

