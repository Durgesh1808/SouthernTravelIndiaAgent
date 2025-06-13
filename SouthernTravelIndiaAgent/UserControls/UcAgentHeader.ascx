<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcAgentHeader.ascx.cs" Inherits="SouthernTravelIndiaAgent.UserControls.UcAgentHeader" %>
<%@ Register Src="UcAjentmenu.ascx" TagName="Ajentmenu" TagPrefix="uc1" %>

<script language="javascript" type="text/javascript">
window.history.forward(1);
</script>



<table width="1002" border="0" align="center" cellpadding="0" cellspacing="0">
    <tr>
        <td width="245" align="right" valign="bottom" background="Assets/images/logo.jpg">
            <table width="220" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td width="70">
                    </td>
                    <td height="30" align="left" valign="top" class="text1">
                        <h3>
                            <a href="agenthomepage.aspx" class="text1">AGENT</a></h3>
                    </td>
                </tr>
            </table>
        </td>
        <td width="426">
            <img  loading="lazy" alt="header1" src="Assets/images/header1.jpg" width="426" height="155" />
        </td>
        <td width="331" align="right" valign="top" background="Assets/images/header_03.jpg">
            <table width="150" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td width="131">
                        &nbsp;
                    </td>
                    <td width="14">
                        &nbsp;
                    </td>
                </tr>
                <%if (Session["AgentFname"] != null)
                  {
                      String sName = Session["AgentFname"].ToString().Trim() + " " + Session["AgentLname"].ToString().Trim();
                %>
                <tr>
                    <td class="text1" align="center">
                        Welcome,<br />
                        <strong>
                            <%=sName%></strong>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2" height="10">
                    </td>
                </tr>
                <tr>
                    <td>
                        <a href="AgentProfilePage.aspx">
                            <img  loading="lazy" alt="edit_profile" src="Assets/images/edit_profile.jpg" width="75" height="18" border="0" /></a>&nbsp;<a
                                href="AgentLogout.aspx"><img  loading="lazy" alt="signout" src="Assets/images/signout.jpg" width="53" height="18" border="0" /></a>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <% }%>
                <%if (Session["BranchId"] != null)
                  {  %>
                <tr>
                    <td colspan="2" align="center" >
                        <asp:LinkButton ID="lbtn" runat="server" OnClick="lbtn_Click" CausesValidation="false" CssClass="tour">Back to My<br /> Branch Login</asp:LinkButton>
                    </td>
                    
                </tr>
                <% }%>
            </table>
        </td>
    </tr>
    <%if (Session["AgentFname"] != null)
      {
    %>
    <tr>
        <td colspan="3">
            <table width="1002" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td width="14">
                        <img  loading="lazy" alt="left_curve" src="Assets/images/left_curve.jpg" width="14" height="27" />
                    </td>
                    <td width="691" bgcolor="#348de7">
                        <%--<script language="javascript" src="JavaScript/agent_headerlinks.js" type="text/javascript">
          </script> --%>
                        <uc1:Ajentmenu ID="Ajentmenu1" runat="server" />
                    </td>
                    <td width="282" bgcolor="#348de7" class="yellow_text">
                        Available Funds :
                        <%=sBalance%>
                        &nbsp;&nbsp;| &nbsp;&nbsp;<a href="agentaddfundsbycc.aspx">Add Funds</a>
                    </td>
                    <td width="15">
                        <img  loading="lazy" alt="right_curve" src="Assets/images/right_curve.jpg" width="15" height="27" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <% }
      else
      {
    %>
    <tr>
        <td colspan="3">
            <table width="1002" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td width="14">
                        <img  loading="lazy" alt="left_curve" src="Assets/images/left_curve.jpg" width="14" height="27" />
                    </td>
                    <td width="973" bgcolor="#348de7" class="white_text" colspan="2">
                        &nbsp;
                    </td>
                    <td width="15">
                        <img  loading="lazy" alt="right_curve" src="Assets/images/right_curve.jpg" width="15" height="27" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <% }%>
    <tr>
        <td colspan="3" height="5">
        </td>
    </tr>
</table>
