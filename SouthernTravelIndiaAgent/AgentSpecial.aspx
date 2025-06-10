<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgentSpecial.aspx.cs" Inherits="SouthernTravelIndiaAgent.AgentSpecial" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link href="../Assets/css/style.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">    
        function GetTours(v)
        {
            fillTours(v,'<%=mode%>');
        }
        function fillTours(zone,modeLogin)
        {            
            var img = document.getElementById('imgWait');
            img.style.display = 'block';
	        xmlHttp=GetXmlHttpObject();
            var url = '../getAllSplTours.aspx?zone='+ zone+ '&mode=' + modeLogin ;  
            url = url + "&sid="+Math.random();							
            xmlHttp.onreadystatechange=stateChanged1;
           
            xmlHttp.open("GET",url,true);
	        xmlHttp.send(null);
	    }
	    function stateChanged1()  
		{ 
			if( (xmlHttp.readyState == 4 || xmlHttp.readyState=="complete"))
			{ 
               document.getElementById('divShowTours').innerHTML = xmlHttp.responseText; 
               var img = document.getElementById('imgWait');
               img.style.display = 'none';
			}
		}
		function GetXmlHttpObject()
		{ 
		    var objXMLHttp=null
		    if (window.XMLHttpRequest)
		    {
		        objXMLHttp=new XMLHttpRequest()
		    }
		    else if (window.ActiveXObject)
		    {
		        objXMLHttp=new ActiveXObject("Microsoft.XMLHTTP")
		    }
		    else if (!xmlhttp && typeof XMLHttpRequest != "undefined") {
			    try {
					    xmlhttp = new XMLHttpRequest();
			    } catch (e) {
					    xmlhttp = false;
			    }
		    }
		    return objXMLHttp
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" target="_parent">
        <table width="276" height="429" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td align="left">
                    <img  loading="lazy" alt="left_corner" src="Assets/images/left_corner.gif" width="8" height="8" /></td>
                <td bgcolor="#E7E7E7">
                    <img  loading="lazy" alt="trans" src="Assets/images/trans.gif" width="260" height="1" /></td>
                <td align="right">
                    <img  loading="lazy" alt="rgt_corner" src="Assets/images/rgt_corner.gif" width="8" height="8" /></td>
            </tr>
            <tr>
                <td align="left" bgcolor="#E7E7E7">
                    &nbsp;</td>
                <td valign="top">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" height="415">
                        <tr>
                            <td width="100%" valign="top">
                                <img  loading="lazy" alt="flight_img" src="Assets/images/flight_img.gif" alt="Build Your Package" width="260" height="81"
                                    border="0" usemap="#Map1" /></td>
                        </tr>
                        <tr>
                            <td height="5"></td>
                        </tr>
                        <tr>
                            <td align="center" background="Assets/images/green_img.jpg" style="background-position:bottom; background-repeat:no-repeat; height: 277px;" valign="top">
                                <table width="90%" border="0" cellspacing="0" cellpadding="0">
                                 <tr class="verdana11bk">
                                        <td colspan="3" align="left" class="hotel">
                                         &nbsp;<b>Select a zone </b>  
                                         </td>   
                                    </tr>
                                    <tr class="verdana11bk">
                                        <td width="38%" height="30" align="left">
                                                <input type="radio" onclick="GetTours(this.value);"  name="radZone" value="4" />East
                                            </td>
                                        <td width="6%" align="center">
                                            </td>
                                        <td width="56%" align="left">
                                          <input type="radio" onclick="GetTours(this.value);" name="radZone" value="3" />West
                                        </td>
                                    </tr>
                                    <tr class="verdana11bk">
                                        <td width="38%" height="30" align="left">
                                          <input type="radio" onclick="GetTours(this.value);" name="radZone" value="1" />North
                                           </td>
                                        <td width="6%" align="center">
                                            </td>
                                        <td width="56%" align="left">
                                           <input type="radio" onclick="GetTours(this.value);" name="radZone" value="2" />South 
                                        </td>
                                    </tr>
                                    <tr class="verdana11bk">
                                        <td colspan="3" align="center">
                                            <div id="imgWait" style="display:none;">
                                                                <img  loading="lazy" alt="wait_spinner" width="33px" height="33px"  src="Assets/images/wait_spinner.gif" alt="" /><br />
                                                                Please wait...
                                             </div>
                                         </td>   
                                    </tr>
                                    <tr class="verdana11bk">
                                        <td height="30" colspan="3" align="left">                                         
                                            <div id="divShowTours" >
                                            </div>
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
                <td align="left">
                    <img  loading="lazy" alt="left_d_corner" src="Assets/images/left_d_corner.gif" width="8" height="8" /></td>
                <td bgcolor="#E7E7E7">
                    <img  loading="lazy" alt="trans" src="Assets/images/trans.gif" width="1" height="1" /></td>
                <td align="right">
                    <img  loading="lazy" alt="rgt_d_corner" src="Assets/images/rgt_d_corner.gif" width="8" height="8" /></td>
            </tr>
        </table>
         <map name="Map1" id="Map1">
            <area shape="circle" coords="100,53,27" href="Agenthotels_iframe.aspx" title="Book Your Hotel" alt="Hotel Booking"/>
            <area shape="circle" coords="171,53,27" href="Agentcarbooking.aspx" title="Book Your Car" alt="Car Booking"/>
           
            <area shape="circle" coords="45,52,27" href="Agenttour.aspx" title="Build your Package" alt="Book Your Package"/>         
        </map>
    </form>
  
</body>
</html>
