<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgentProfilePage.aspx.cs" Inherits="SouthernTravelIndiaAgent.AgentProfilePage" %>


<%@ Register TagPrefix="uc1" TagName="Footer" Src="UserControls/UcAgentFooter.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AgentHeader" Src="UserControls/UcAgentHeader.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Southern Travels :-: Agent Profile</title>    
    <link href="../Assets/css/style.css" rel="stylesheet" type="text/css" />
    <link href="../Assets/css/stylesheet.css" type="text/css" rel="stylesheet" />
    <script language="javascript" src="../Assets/js/MyScript.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
    </script>

</head>
<body class="Body" ">
    <form id="form1" runat="server">
        <div style="width: 100%;">
            <uc1:AgentHeader ID="agentHeader" runat="server" />
        </div>
        <div style="width: 98%; margin-left: 10px; margin-right: 10px;">
            <table width="600" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td align="left" style="width: 9px">
                        <img  loading="lazy" alt="left_corner" src="Assets/images/left_corner.gif" width="8" height="8" /></td>
                    <td bgcolor="#E7E7E7">
                        <img  loading="lazy" alt="trans" src="Assets/images/trans.gif" width="280" height="1" /></td>
                    <td align="right">
                        <img  loading="lazy" alt="rgt_corner" src="Assets/images/rgt_corner.gif" width="8" height="8" /></td>
                </tr>
                <tr>
                    <td align="left" bgcolor="#E7E7E7" style="width: 9px">
                        &nbsp;</td>
                    <td bgcolor="#E7E7E7">
                        <table width="600" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td width="600" height="35" align="center" valign="middle">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="1">
                                        <tr>
                                            <td bgcolor="ffffff">
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td align="left" valign="top" bgcolor="ffffff">
                                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                    <td width="13">
                                                                        <img  loading="lazy" alt="left_" src="Assets/images/left_.gif" width="13" height="42" /></td>
                                                                    <td align="left" background="Assets/images/bg1.gif" class="verdana14w">
                                                                        My Profile</td>
                                                                    <td width="13">
                                                                        <img  loading="lazy" alt="right_" src="Assets/images/right_.gif" width="13" height="42" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        &nbsp;</td>
                                                                    <td align="center">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        &nbsp;</td>
                                                                    <td align="center">
                                                                        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
                                                                            <tr>
                                                                                <td>
                                                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                        <tr>
                                                                                            <td width="26" height="30" align="center" background="Assets/images/bg1.gif">
                                                                                            </td>
                                                                                            <td height="30" colspan="3" align="left" background="Assets/images/bg1.gif" class="verdana14w">
                                                                                                Basic Profile
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td colspan="4">
                                                                                                <img  loading="lazy" alt="trans" src="Assets/images/trans.gif" width="1" height="1" /></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td colspan="4" bgcolor="#F2F9FF">
                                                                                                <img  loading="lazy" alt="trans" src="Assets/images/trans.gif" width="1" height="5" /></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td height="30" bgcolor="#F2F9FF">
                                                                                                &nbsp;</td>
                                                                                            <td width="169" height="30" align="left" bgcolor="#F2F9FF" class="verdana11bk">
                                                                                                &nbsp;&nbsp;Name</td>
                                                                                            <td bgcolor="#F2F9FF">
                                                                                                :</td>
                                                                                            <td align="left" bgcolor="#F2F9FF" class="verdana11">
                                                                                                &nbsp;<asp:Label ID="lblFName" runat="server" BorderColor="SteelBlue" class="verdana11bk"
                                                                                                    BorderWidth="1" Text="First Name" Width="135px"></asp:Label>
                                                                                                &nbsp;&nbsp;
                                                                                                <asp:Label ID="lblLName" runat="server" BorderColor="SteelBlue" class="verdana11bk"
                                                                                                    BorderWidth="1" Text="Last Name" Width="135px"></asp:Label></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td height="30" bgcolor="#F2F9FF">
                                                                                                &nbsp;</td>
                                                                                            <td height="30" width="80" align="left" bgcolor="#F2F9FF" class="verdana11bk">
                                                                                                &nbsp;&nbsp;Email</td>
                                                                                            <td bgcolor="#F2F9FF">
                                                                                                :</td>
                                                                                            <td align="left" bgcolor="#F2F9FF" class="verdana11">
                                                                                                &nbsp;<asp:Label ID="lblEmail" runat="server" BorderColor="SteelBlue" class="verdana11bk"
                                                                                                    BorderWidth="1" Width="227px"></asp:Label></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td height="30" bgcolor="#F2F9FF">
                                                                                                &nbsp;</td>
                                                                                            <td height="30" align="left" bgcolor="#F2F9FF" class="verdana11bk">
                                                                                                &nbsp;&nbsp;Gender</td>
                                                                                            <td bgcolor="#F2F9FF">
                                                                                                :</td>
                                                                                            <td align="left" bgcolor="#F2F9FF" class="verdana11">
                                                                                                &nbsp;<asp:Label ID="lblGender" runat="server" BorderColor="SteelBlue" class="verdana11bk"
                                                                                                    BorderWidth="1" Width="135px"></asp:Label></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td bgcolor="#F2F9FF" style="height: 30px">
                                                                                                &nbsp;</td>
                                                                                            <td align="left" bgcolor="#F2F9FF" class="verdana11bk" style="height: 30px">
                                                                                                &nbsp;&nbsp;Date of Birth
                                                                                            </td>
                                                                                            <td bgcolor="#F2F9FF">
                                                                                                :</td>
                                                                                            <td align="left" bgcolor="#F2F9FF" class="verdana11bk" style="height: 30px">
                                                                                                &nbsp;<asp:Label ID="lblDOB" runat="server" BorderColor="SteelBlue" class="verdana11bk"
                                                                                                    BorderWidth="1" Width="135px"></asp:Label>&nbsp;&nbsp;<span class="verdana11bk">MM/DD/YYYY</span></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td colspan="4" style="height: 5px">
                                                                                                <img  loading="lazy" alt="trans" src="Assets/images/trans.gif" width="1" height="5" /></td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        &nbsp;</td>
                                                                    <td align="center">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        &nbsp;</td>
                                                                    <td align="center">
                                                                        <table width="99%" border="0" align="center" cellpadding="1" cellspacing="1">
                                                                            <tr>
                                                                                <td>
                                                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                        <tr>
                                                                                            <td width="26" height="30" align="center" background="Assets/images/bg1.gif">
                                                                                            </td>
                                                                                            <td height="30" colspan="3" align="left" background="Assets/images/bg1.gif" class="verdana14w">
                                                                                                Additional profile
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td colspan="3" align="center">
                                                                                                <img  loading="lazy" alt="trans" src="Assets/images/trans.gif" width="1" height="1" /></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td colspan="3" align="center" bgcolor="#F2F9FF">
                                                                                                <img  loading="lazy" alt="trans" src="Assets/images/trans.gif" width="1" height="5" /></td>
                                                                                            <tr>
                                                                                                <td height="30" align="center" bgcolor="#F2F9FF">
                                                                                                    &nbsp;</td>
                                                                                                <td height="30" width="169" align="left" bgcolor="#F2F9FF" class="verdana11bk" valign="middle">
                                                                                                    Address
                                                                                                </td>
                                                                                                <td bgcolor="#F2F9FF">
                                                                                                    :</td>
                                                                                                <td height="30" align="left" valign="top" bgcolor="#F2F9FF" class="verdana11bk">
                                                                                                    &nbsp;<asp:Label ID="lbloffadd" runat="server" BorderColor="SteelBlue" BorderWidth="1"
                                                                                                        Height="60" Width="193px"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                        <tr>
                                                                                            <td align="center" bgcolor="#F2F9FF" style="height: 30px">
                                                                                                &nbsp;</td>
                                                                                            <td align="left" bgcolor="#F2F9FF" class="verdana11bk" style="height: 30px">
                                                                                                City
                                                                                            </td>
                                                                                            <td style="height: 30px" bgcolor="#F2F9FF">
                                                                                                :</td>
                                                                                            <td align="left" bgcolor="#F2F9FF" class="verdana11bk" style="height: 30px">
                                                                                                <span class="verdana11bk">&nbsp;<asp:Label ID="lblcity" runat="server" BorderColor="SteelBlue"
                                                                                                    BorderWidth="1" Width="150px"></asp:Label>
                                                                                                </span>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td height="30" align="center" bgcolor="#F2F9FF">
                                                                                                &nbsp;</td>
                                                                                            <td height="30" align="left" bgcolor="#F2F9FF" class="verdana11bk">
                                                                                                &nbsp;Mobile No.
                                                                                            </td>
                                                                                            <td bgcolor="#F2F9FF">
                                                                                                :</td>
                                                                                            <td height="30" align="left" bgcolor="#F2F9FF" class="verdana11bk">
                                                                                                &nbsp;<asp:Label ID="lblmob" runat="server" BorderColor="SteelBlue" BorderWidth="1"
                                                                                                    Width="150px"></asp:Label></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td height="30" align="center" bgcolor="#F2F9FF">
                                                                                                &nbsp;</td>
                                                                                            <td height="30" align="left" bgcolor="#F2F9FF" class="verdana11bk">
                                                                                                &nbsp;Phone No.
                                                                                            </td>
                                                                                            <td bgcolor="#F2F9FF">
                                                                                                :</td>
                                                                                            <td height="30" align="left" bgcolor="#F2F9FF" class="verdana11">
                                                                                                <span class="verdana11bk">&nbsp;<asp:Label ID="lblpho" runat="server" BorderColor="SteelBlue"
                                                                                                    BorderWidth="1" Width="150px"></asp:Label>
                                                                                                </span>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td height="30" align="center" bgcolor="#F2F9FF">
                                                                                                &nbsp;</td>
                                                                                            <td height="30" align="left" bgcolor="#F2F9FF" class="verdana11bk">
                                                                                                &nbsp;Fax No.
                                                                                            </td>
                                                                                            <td bgcolor="#F2F9FF">
                                                                                                :</td>
                                                                                            <td height="30" align="left" bgcolor="#F2F9FF" class="verdana11bk">
                                                                                                <span class="verdana11bk">&nbsp;<asp:Label ID="lblfax" runat="server" BorderColor="SteelBlue"
                                                                                                    BorderWidth="1" Width="150px"></asp:Label>
                                                                                                </span>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td height="30" align="center" bgcolor="#F2F9FF">
                                                                                                &nbsp;</td>
                                                                                            <td height="30" align="left" bgcolor="#F2F9FF" class="verdana11bk">
                                                                                                &nbsp;Pan No.
                                                                                            </td>
                                                                                            <td bgcolor="#F2F9FF">
                                                                                                :</td>
                                                                                            <td height="30" align="left" bgcolor="#F2F9FF" class="verdana11bk">
                                                                                                <span class="verdana11bk">&nbsp;
                                                                                                <asp:Label ID="lblPanNO" runat="server" BorderColor="SteelBlue"
                                                                                                    BorderWidth="1" Width="150px"></asp:Label>
                                                                                                </span>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td colspan="3" align="center" bgcolor="#F2F9FF">
                                                                                                <img  loading="lazy" alt="trans" src="Assets/images/trans.gif" width="1" height="5" /></td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        &nbsp;</td>
                                                                    <td align="center">
                                                                        <br />
                                                                        <a href="AgentProfileUpdate.aspx">
                                                                            <img  loading="lazy" alt="edit1" src="Assets/images/edit1.gif" width="64" height="25" border="0" /></a>&nbsp;&nbsp;&nbsp;&nbsp;<a
                                                                                href="agenthomepage.aspx"><img  loading="lazy" alt="cancel1" id="btnCancel" border="0" src="Assets/images/cancel1.gif"
                                                                                    alt="Cancel" /></a><br />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        &nbsp;</td>
                                                                    <td align="center">
                                                                        <table width="99%" border="0" align="center" cellpadding="1" cellspacing="1">
                                                                            <tr>
                                                                                <td bgcolor="#75B2DD">
                                                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                        <tr>
                                                                                            <td width="23" height="30" align="center" background="Assets/images/bg1.gif">
                                                                                            </td>
                                                                                            <td height="30" colspan="6" align="left" background="Assets/images/bg1.gif" class="verdana14w">
                                                                                                Change Password
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td colspan="7" align="center">
                                                                                                <img  loading="lazy" alt="trans" src="Assets/images/trans.gif" width="1" height="1" /></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td colspan="7" align="center" bgcolor="#F2F9FF">
                                                                                                <img  loading="lazy" alt="trans" src="Assets/images/trans.gif" width="1" height="5" /></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td height="30" align="center" bgcolor="#F2F9FF">
                                                                                                &nbsp;</td>
                                                                                            <td height="30" colspan="6" align="left" bgcolor="#F2F9FF" class="verdana11bk">
                                                                                                <a href="AgentChangePassWord.aspx" class="left">Click here to change password</a>
                                                                                                <br />
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <br />
                                                        </td>
                                                    </tr>
                                                </table>
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
                        <img  loading="lazy" alt="left_d_corner" src="Assets/images/left_d_corner.gif" width="8" height="8" /></td>
                    <td bgcolor="#E7E7E7">
                        <img  loading="lazy" alt="trans" src="Assets/images/trans.gif" width="1" height="1" /></td>
                    <td align="right">
                        <img  loading="lazy" alt="rgt_d_corner" src="Assets/images/rgt_d_corner.gif" width="8" height="8" /></td>
                </tr>
            </table>
            <div>
                <uc1:Footer ID="Footer1" runat="server"></uc1:Footer>
            </div>
        </div>
    </form>
</body>
</html>
