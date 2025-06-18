<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgentChangePassWord.aspx.cs" Inherits="SouthernTravelIndiaAgent.AgentChangePassWord" %>

<%@ Register TagPrefix="uc1" TagName="AgentFooter" Src="UserControls/UcAgentFooter.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AgentHeader1" Src="UserControls/UcAgentHeader1.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AgentHeader" Src="UserControls/UcAgentHeader.ascx" %>
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
    <link href="../Assets/css/style.css" rel="stylesheet" type="text/css" />
    <link href="../Assets/css/stylesheet.css" type="text/css" rel="stylesheet" />

    <script language="javascript" src="../Assets/js/MyScript.js" type="text/javascript"></script>

    <!--<script language="javascript" src="../query-script.js" type="text/javascript"></script>-->
    <meta content="MSHTML 6.00.2900.2180" name="GENERATOR" />
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1" />
    <meta name="CODE_LANGUAGE" content="C#" />
    <meta name="vs_defaultClientscript" content="Javascript" />
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
            <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

    <script language="javascript" src="../Assets/js/md5.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        function validatePwd() {
            var invalid = " "; // Invalid character is a space
            var minLength = 6; // Minimum length

            // check for minimum length
            if (document.form1.txtNewpwd.value.length < minLength) {
                //alert('Your password must be at least ' + minLength + ' characters long. Try again.');
                Swal.fire({
                    icon: 'warning',
                    title: 'Oops...',
                    text: ' Your password must be at least ' + minLength + ' characters long. Try again.',
                    confirmButtonColor: '#f2572b'
                });
                return false;
            }
            // check for spaces
            if (document.form1.txtNewpwd.value.indexOf(invalid) > -1) {
                //alert("Sorry, spaces are not allowed.");
                Swal.fire({
                    icon: 'warning',
                    title: 'Oops...',
                    text: ' Sorry, spaces are not allowed.',
                    confirmButtonColor: '#f2572b'
                });
                return false;
            }
        }

        function validate() {
            if (Trim(document.form1.txtoldpwd.value) == "") {
                //alert("Please Enter the old password");
                Swal.fire({
                    icon: 'warning',
                    title: 'Oops...',
                    text: 'Please Enter the old password.',
                    confirmButtonColor: '#f2572b'
                });
                document.form1.txtoldpwd.focus();
                return false;
            }
            //				if(Trim(document.form1.txtoldpwd.value)!= Trim(document.form1.txtOPwd.value))
            //				{
            //					alert("Your old password is wrong ! Please enter valid old password.");
            //					document.form1.txtoldpwd.value="";
            //					document.form1.txtoldpwd.focus();
            //					return false;
            //				}
            if (Trim(document.form1.txtNewpwd.value) == "") {
                //alert("Please enter the new password");
                Swal.fire({
                    icon: 'warning',
                    title: 'Oops...',
                    text: 'Please enter the new password.',
                    confirmButtonColor: '#f2572b'
                });
                document.form1.txtNewpwd.value = "";
                document.form1.txtNewpwd.focus();
                return false;
            }
            if (validatePwd() == false) {
                document.getElementById("txtNewpwd").value = "";
                document.getElementById("txtNewpwd").focus();
                return false;
            }
            if (Trim(document.form1.txtrepwd.value) == "") {
                Swal.fire({
                    icon: 'warning',
                    title: 'Oops...',
                    text: 'Please Re-enter Password.',
                    confirmButtonColor: '#f2572b'
                });
                //alert("Please Re-enter Password.");
                document.form1.txtrepwd.value = "";
                document.form1.txtrepwd.focus();
                return false;
            }
            if (Trim(document.form1.txtNewpwd.value) != Trim(document.form1.txtrepwd.value)) {
                Swal.fire({
                    icon: 'warning',
                    title: 'Oops...',
                    text: 'New Password and Re-enter Password mismatch..',
                    confirmButtonColor: '#f2572b'
                });
                //alert("New Password and Re-enter Password mismatch.");
                document.form1.txtNewpwd.value = "";
                document.form1.txtrepwd.value = "";
                document.form1.txtNewpwd.focus();
                return false;
            }

            var pass;
            pass = document.getElementById("txtoldpwd").value;
            var encpass = hex_md5(pass);
            //alert(encpass);
            document.form1.txtoldpwd.value = encpass;

            pass = "";
            pass = document.getElementById("txtNewpwd").value;
            encpass = hex_md5(pass);
            //alert(encpass);
            document.form1.txtNewpwd.value = encpass;

            pass = "";
            pass = document.getElementById("txtrepwd").value;
            encpass = hex_md5(pass);
            //alert(encpass);
            document.form1.txtrepwd.value = encpass;
            return true;

            //return true;
        }

        function GotoPrev() {
            history.back();
        }
    </script>

</head>
<body class="Body">
    <form id="form1" runat="server">
        <%if (Session["IsForgot"] != null && Session["IsForgot"].ToString() == "Y")
          {%>
        <div style="width: 100%;">
            <uc1:AgentHeader1 ID="agentHeader1" runat="server" />
        </div>
        <%} %>
        <%
            else
            {%>
        <div style="width: 100%;">
            <uc1:AgentHeader ID="agentHeader" runat="server" />
        </div>
        <%} %>
        <div style="width: 98%; margin-left: 10px; margin-right: 10px;">
            <table width="600" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td align="left" style="width: 9px">
                        <img src="Assets/images/left_corner.gif" width="8" height="8"  loading="lazy" alt="left_corner" /></td>
                    <td bgcolor="#E7E7E7">
                        <img src="Assets/images/trans.gif" width="280" height="1"  loading="lazy" alt="trans" /></td>
                    <td align="right">
                        <img src="Assets/images/rgt_corner.gif" width="8" height="8"  loading="lazy" alt="rgt_corner" /></td>
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
                                            <td colspan="3" style="height: 40px" align="center" valign="middle" background="Assets/images/bg1.gif"
                                                class="verdana14w">
                                                Change Agent Password
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <img src="Assets/images/trans.gif" width="1" height="1" loading="lazy" alt="trans"/></td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" bgcolor="#F2F9FF">
                                                <img src="Assets/images/trans.gif" width="1" height="5" loading="lazy" alt="trans"/></td>
                                        </tr>
                                        <tr>
                                            <td height="30" bgcolor="#F2F9FF">
                                                &nbsp;</td>
                                            <td width="169" height="30" align="left" bgcolor="#F2F9FF" class="verdana11bk">
                                                &nbsp;&nbsp;Old Password</td>
                                            <td align="left" bgcolor="#F2F9FF" class="verdana11bk">
                                                :
                                                <asp:TextBox ID="txtoldpwd" runat="server" TextMode="Password"></asp:TextBox>
                                                <input id="txtOPwd" type="hidden" runat="server" /></td>
                                        </tr>
                                        <tr>
                                            <td height="30" bgcolor="#F2F9FF">
                                                &nbsp;</td>
                                            <td height="30" align="left" bgcolor="#F2F9FF" class="verdana11bk">
                                                &nbsp;&nbsp;New Password</td>
                                            <td align="left" bgcolor="#F2F9FF" class="verdana11bk">
                                                :
                                                <asp:TextBox ID="txtNewpwd" runat="server" TextMode="Password"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td height="30" bgcolor="#F2F9FF">
                                                &nbsp;</td>
                                            <td height="30" align="left" bgcolor="#F2F9FF" class="verdana11bk">
                                                &nbsp;&nbsp;Confirm Password</td>
                                            <td align="left" bgcolor="#F2F9FF" class="verdana11bk">
                                                :
                                                <asp:TextBox ID="txtrepwd" runat="server" TextMode="Password"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td height="30" align="right" bgcolor="#F2F9FF" class="verdana11bk">
                                                &nbsp;</td>
                                            <td height="22" align="center" bgcolor="#F2F9FF" class="verdana11bk">
                                                &nbsp;</td>
                                            <td height="22" align="left" bgcolor="#F2F9FF" class="verdana11bk">
                                                <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="Assets/images/update.gif" OnClick="btnAdd_Click" />&nbsp;&nbsp;
                                                <asp:ImageButton ID="btnCancel" runat="server" ImageUrl="Assets/images/cancel1.gif" OnClick="btnCancel_Click" />
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
                        <img src="Assets/images/left_d_corner.gif" width="8" height="8" loading="lazy" alt="left_d_corner"/></td>
                    <td bgcolor="#E7E7E7">
                        <img src="Assets/images/trans.gif" width="1" height="1" loading="lazy" alt="trans"/></td>
                    <td align="right">
                        <img src="Assets/images/rgt_d_corner.gif" width="8" height="8" loading="lazy" alt="rgt_d_corner"/></td>
                </tr>
            </table>
        </div>
        <div>
            <uc1:AgentFooter ID="Footer1" runat="server"></uc1:AgentFooter>
        </div>
    </form>
    <input type="hidden" value="<%=ViewState["salt"]%>" id="txtsalt" />
</body>
</html>
