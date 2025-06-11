<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="agentCarMultiple.aspx.cs" Inherits="SouthernTravelIndiaAgent.agentCarMultiple" %>


<%@ Register TagPrefix="uc1" TagName="AgentHeader" Src="UserControls/UcAgentHeader.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AgentFooter" Src="UserControls/UcAgentFooter.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 Transitional//EN" "http://www.w3.org/tr/xhtml1/Dtd/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Southern Travels - Agent Car-booking results</title>
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
    <link href="../Assets/css/style.css" rel="stylesheet" type="text/css" />
    <link href="../Assets/css/stylesheet.css" type="text/css" rel="stylesheet" />

     <%--<script language="javascript" type="text/javascript">
		<!--
		function trapPost()
		{		  
		}
		function doPostBook(fareId)
		{		
           document.getElementById('hidFareId').value  = fareId	;	    
           document.getElementById('hidTransferId').value  = <%=strTransferId%>;
           document.getElementById('btnClick').value  = 'yes';               
           document.form1.submit(); 	
	    }
		-->
    </script>--%>

</head>
<body>
    <form id="form1" runat="server">
        <table id="Table1" cellspacing="0" cellpadding="0" style="width: 100%" border="0">
            <tr>
                <td colspan="2">
                    <uc1:AgentHeader ID="agentHeader" runat="server" />
                </td>
            </tr>
            <tr>
                <td width="75%" valign="top" align="center">
                    <table align="center" border="0" cellpadding="0" cellspacing="0" bordercolor="#666666"
                        style="height: 437px">
                        <tr>
                            <td style="width: 630px" align="center">
                                <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left">
                                            <img  loading="lazy" src="Assets/images/left_corner.gif" width="8" height="8" alt="left_corner" /></td>
                                        <td bgcolor="#E7E7E7">
                                            <img  loading="lazy" src="Assets/images/trans.gif" width="280" height="1" alt="trans" /></td>
                                        <td align="right">
                                            <img  loading="lazy" src="Assets/images/rgt_corner.gif" width="8" height="8" alt="rgt_corner" /></td>
                                    </tr>
                                    <tr>
                                        <td align="left" bgcolor="#E7E7E7">
                                            &nbsp;</td>
                                        <td valign="top">
                                            <table cellspacing="0" cellpadding="0" border="0" style="vertical-align: top; width: 100%;"
                                                align="center">
                                                <tr>
                                                    <td align="center" style="vertical-align: middle; width: 100%; height: 45px;">
                                                        <div align="Left" style="background-color: Transparent">
                                                            <img  loading="lazy" alt="rentacar" src="Assets/images/rentacar.jpg" />
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="Left">
                                                        <img  loading="lazy" alt="2" src="Assets/images/2.gif" /><br />
                                                    </td>
                                                </tr>
                                                <tr style="border: 0; border-color: White;">
                                                    <td align="right">
                                                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;&nbsp;<br />
                                                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; <strong><span
                                                            style="color: #ff0000; font-family: arial;" runat="server" id="lblMsg"></span></strong>
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:GridView ID="dgCarResults" CellPadding="3" OnPageIndexChanging="pageChange"
                                                runat="server" CssClass="footer" GridLines="None" BackColor="White" BorderColor="White"
                                                BorderWidth="0px" Width="100%" AutoGenerateColumns="False" BorderStyle="Ridge"
                                                CellSpacing="1" PageSize="15" AllowPaging="true" >
                                                <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                                                <AlternatingRowStyle CssClass="bg1 verdana11bk" />
                                                <RowStyle CssClass="verdana11bk" />
                                                <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                                <HeaderStyle BackColor="#5aa1ea" CssClass="verdana11bkb" Font-Bold="True" ForeColor="#ffffff" />
                                                <Columns>
                                                    <asp:BoundField HeaderText="Car Models/Max Capacity" DataField="Cartype" />
                                                    <asp:BoundField HeaderText="Fare" DataField="fare" />
                                                    <asp:BoundField HeaderText="Extra Charge per Hrs (Rs)" DataField="ExtraHrsAC" />
                                                    <asp:BoundField HeaderText="Extra Charge per KM (Rs)" DataField="PerKMAcFare" />
                                                    
                                                    <asp:TemplateField HeaderText="Book">
                                                        <ItemTemplate>
                                                            <%--<input type="button" value="Book" onclick="javascript:doPostBook('<%#DataBinder.Eval(Container.DataItem,"fareId")%>');" />--%>
                                                             <asp:Button runat="server" ID="btnBook"  Text="Book" 
                                                CommandArgument='<%#Eval("fareId")%>' onclick="btnBook_Click"  />
                                                            
                                                            
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            
                                           </td>
                                            <td align="right" bgcolor="#E7E7E7">
                                                &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <img  loading="lazy" src="Assets/images/left_d_corner.gif" width="8" height="8" alt="left_d_corner" /></td>
                                        <td bgcolor="#E7E7E7">   
                                            <img  loading="lazy" src="Assets/images/trans.gif" width="1" height="1" alt="trans" /></td>
                                        <td align="right">       
                                            <img  loading="lazy" src="Assets/images/rgt_d_corner.gif" width="8" height="8" alt="rgt_d_corner" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" height="250px" valign="bottom">
                    <uc1:AgentFooter ID="AgentFooter1" runat="server" />
                </td>
            </tr>
        </table>
       <input type="hidden" runat="server" id="btnClick" value="no" />
        <input type="hidden" runat="server" id="hidFareId" value="" />
        <input type="hidden" runat="server" id="hidTransferId" value="" />
    </form>
</body>
</html>
