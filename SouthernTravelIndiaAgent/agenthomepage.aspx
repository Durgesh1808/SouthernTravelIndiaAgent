<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="agenthomepage.aspx.cs" Inherits="SouthernTravelIndiaAgent.agenthomepage" %>

<%@ Register TagPrefix="uc1" TagName="agentheader" Src="UserControls/UcAgentHeader.ascx" %>
<%@ Register TagPrefix="uc2" TagName="agentFooter" Src="UserControls/UcAgentFooter.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>Southern Travels :-: Agent HomePage</title>
     <link href="../Assets/css/style.css" rel="stylesheet" type="text/css" />
      <link href="../Assets/css/stylesheet.css" rel="stylesheet" type="text/css" />
 <script language="javascript" type="text/javascript">
    
    function validation()
    {
    	if (document.form1.txtAmount.value=="")
 		{
		    alert("Please enter the Amount");
		    document.form1.txtAmount.focus();
		    return false;
 		}
 		
	     return true;		
	}
    
    function chkNumeric()
	{
				if(event.shiftKey) return false;
		 		if((event.keyCode<48 || event.keyCode>57) && event.keyCode != 190 && event.keyCode != 8 && event.keyCode != 37 && event.keyCode != 38 && event.keyCode != 39 && event.keyCode != 40 && event.keyCode != 46 && event.keyCode != 13 && event.keyCode != 116 && event.keyCode != 16 && (event.keyCode <96 || event.keyCode > 105) && event.keyCode != 9 && event.keyCode != 110) event.returnValue = false
	}  
    
 </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table width="1001" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <%
                            string strPMode = Convert.ToString(Request.QueryString["pmode"]);
                            if (strPMode == "" || strPMode == null)
                            {
                                strPMode = "agenttour.aspx";
                                Session["Pmode"] = "home";
                            }
                            else if (strPMode == "tour")
                            {
                                strPMode = "agenttour.aspx";
                                Session["Pmode"] = "tour";
                            }
                            else if (strPMode == "car")
                            {
                                strPMode = "Agentcarbooking.aspx";
                                Session["Pmode"] = "car";
                            }
                            else if (strPMode == "hotel")
                            {
                                strPMode = "Agenthotels_iframe.aspx";
                                //strPMode = "hotel.html";
                                Session["Pmode"] = "hotel";
                            }
                            else if (strPMode == "air")
                            {
                                strPMode = "Agentspecial.aspx";
                                Session["Pmode"] = "air";
                            }
                        %>
                    <uc1:agentheader ID="agHeader" runat="server" />
                
                    </td>
                </tr>
            </table>
            <!--MIDDLE -->
            
            <table width="982" border="0" height="100%"  align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="5"></td>
                    <td width="296" align="left" valign="top">
                        <iframe id="1" src="<%=strPMode%>" width="296" height="450" scrolling="yes" align="top" frameborder="0">
                        </iframe>
                    </td>
                    <td width="10"></td>
                    <td width="296" align="right" valign="top">
                        <iframe id="2" src="AgentTouratGlance.aspx" width="296" height="449" scrolling="no" align="top"
                            frameborder="0" ></iframe>
                    </td>
                    <td width="390" align="center" valign="top">
                        <table width="372" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td align="left">
                                    <img src="Assets/images/left_corner.gif" width="8" height="8" alt="image" /></td>
                                <td bgcolor="#E7E7E7">
                                    <img src="Assets/images/trans.gif" width="280" height="1" alt="image" /></td>
                                <td align="right">
                                    <img src="Assets/images/rgt_corner.gif" width="8" height="8" alt="image" /></td>
                            </tr>
                            <tr>
                                <td align="left" bgcolor="#E7E7E7">
                                    &nbsp;</td>
                                <td>
                                    <table width="355" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td width="100%" colspan="2">
                                                <img src="Assets/images/agent_summry.gif" width="355" height="41" alt="image"/></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="right">
                                                <img src="Assets/images/trans.gif" width="1" height="1" alt="image" /></td>
                                        </tr>
                                        <tr>
                                            <td width="227" align="right" valign="top">
                                                <img src="Assets/images/kerala_tour.jpg" width="226" height="165" alt="image" /></td>
                                            <td width="127" valign="top">
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td>
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td height="50" align="left" class="verdana12p">
                                                            &nbsp; <span class="verdana12pb">Balance:</span><br />&nbsp;&nbsp;<asp:Label ID="lblBalance" runat="server" Text="Balance" ></asp:Label><br />
                                                          </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="24" align="center" class="verdana12pb">
                                                            <!--Add Fund--></td>
                                                    </tr>
                                                    <tr>
                                                        <td height="24" align="center" class="verdana11">
                                                            <!--<asp:TextBox ID="txtAmount" runat="server" size="13" MaxLength="6" ></asp:TextBox>--></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            
                                                           <!-- <asp:ImageButton ID="btnSubmit" runat="server" ImageUrl="Assets/images/submit_pink.gif" width="67" height="20" OnClick="btnSubmit_Click"  />-->
                                                            </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="left">
                                                <img src="Assets/images/trans.gif" width="1" height="1" alt="image" /></td>
                                        </tr>
                                    </table>
                                </td>
                                <td align="right" bgcolor="#E7E7E7">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <img src="Assets/images/left_d_corner.gif" width="8" height="8" alt="image" /></td>
                                <td bgcolor="#E7E7E7">
                                    <img src="Assets/images/trans.gif" width="1" height="1" alt="image" /></td>
                                <td align="right">
                                    <img src="Assets/images/rgt_d_corner.gif" width="8" height="8" alt="image"/></td>
                            </tr>
                        </table>
                        <table width="372" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <img src="Assets/images/trans.gif" width="1" height="11"alt="image" /></td>
                            </tr>
                            <tr>
                                <td>
                                    <a href="#">
                                        <img src="Assets/images/kashmir.jpg" width="372" height="51" border="0" alt="image"/></a></td>
                            </tr>
                            <tr>
                                <td>
                                    <img src="Assets/images/trans.gif" width="1" height="14" alt="image" /></td>
                            </tr>
                        </table>
                        <table width="372" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td align="left">
                                    <a href="#">
                                        <img src="Assets/images/wild.jpg" width="180" height="147" border="0" alt="image"/></a></td>
                                <td align="right">
                                    <a href="#">
                                        <img src="Assets/images/goa.jpg" width="180" height="147" border="0" alt="image"/></a></td>
                            </tr>
                        </table>
                    </td>
                    
                </tr>
            </table>
            <table width="1001" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr>
                      <td height="22" colspan="3" align="center" class="verdana10">
                      <uc2:agentFooter ID="AgentFooter1" runat="server" />                      
                      </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
