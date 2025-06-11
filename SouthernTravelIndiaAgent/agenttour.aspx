<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="agenttour.aspx.cs" Inherits="SouthernTravelIndiaAgent.agenttour" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <link href="../Assets/css/style.css" rel="stylesheet" type="text/css" />
    <title>Southern India Travel,South India Travel Packages,Travel Packages to South India</title>
    <script language="javascript" type="text/javascript">
    function validation()
	    {
	            if ((document.form1.ddlTourName.value=="Select")||(document.form1.ddlTourName.value=="0"))
		        {
			        alert("Please choose a tour.");
			        document.getElementById("ddlTourName").focus();
			        return false;
		        }
	    }
	    function Search()
	    {
	           var img = document.getElementById('imgWait');
               img.style.display = 'block';
               document.getElementById('btnBooking').style.display = 'none';
	           var CurrentCity = document.getElementsByName('grpBranchCity');	       
	           var CurrentCityValue="";	       
	           //alert(CurrentCity.length);
	           for(var i=0; i< CurrentCity.length;i++){
	                if(CurrentCity[i].checked){
	                    CurrentCityValue = CurrentCity[i].value;
	                    break;
	                }
	           }
	        if(CurrentCityValue=='')
	            alert('Select one branch');   
	       //alert(CurrentCityValue);
	        xmlHttp=GetXmlHttpObject()
            document.getElementById('imgWait').style.display='block';
             document.getElementById('btnBooking').style.display = 'none';
  	        var img = document.getElementById('imgWait');
            img.style.display = 'block';
            var url='../CityWisetours.aspx?branchcode=';            
            url = url + CurrentCityValue;
            url = url + "&sid="+Math.random();							
            xmlHttp.onreadystatechange=stateChanged1;
            xmlHttp.open("GET",url,true);
	        xmlHttp.send(null);
	    }
	    function stateChanged1()  
			{ 
				if( (xmlHttp.readyState == 4 || xmlHttp.readyState=="complete"))
				{ 
                   //document.body.innerHTML = xmlHttp.responseText;                    
                   //alert(xmlHttp.responseText);
                   //addOption(document.getElementById('ddlTourName'),'hi',6);
                   //buildDDlTour('branch1#b1\nbranch2#b2');
                   buildDDlTour(xmlHttp.responseText);
                   var img = document.getElementById('imgWait');
                   img.style.display = 'none';
                   document.getElementById('btnBooking').style.display = 'block';
				}
			}
		function buildDDlTour(strValues)
		{
		    var BranchArr = strValues.split('<br>');
		    var eachBranch,branchName,branchId;
		    document.getElementById('ddlTourName').options.length = 0;
		    		    
		    addOption(document.getElementById('ddlTourName'),'Select','0');
		    for(var brnch =0 ;brnch< BranchArr.length; brnch++ )
		    {
		        if(BranchArr[brnch]!=''){
		            eachBranch  =   BranchArr[brnch].split('#');		            
		            branchName = eachBranch[0] ;
		            branchId = eachBranch[1] ; 
		            addOption(document.getElementById('ddlTourName'),branchName,branchId);
                }
		    }   
		    
		}	
		function addOption(selectbox,text,value )
        {
            var optn = document.createElement("OPTION");
            optn.text = text;
            optn.value = value;
            selectbox.options.add(optn);
        }
	
	    function GetXmlHttpObject(){ 
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
        <div>
            <table width="276" height="429" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td align="left">
                        <img  loading="lazy" src="Assets/images/left_corner.gif" width="8" height="8" alt="left_corner" /></td>
                    <td bgcolor="#E7E7E7">
                        <img  loading="lazy"  src="Assets/images/trans.gif" width="260" height="1" alt="trans" /></td>
                    <td align="right" style="width: 9px">
                        <img  loading="lazy"  src="Assets/images/rgt_corner.gif" width="8" height="8" alt="rgt_corner" /></td>
                </tr>
                <tr>
                    <td align="left" bgcolor="#E7E7E7">
                        &nbsp;</td>
                    <td valign="top">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td width="100%" style="height: 81px">
                                    <img  loading="lazy" alt="build" src="Assets/images/build.gif" width="260" height="81" border="0" usemap="#Map"  alt="Tour Booking" id="IMG1" language="javascript" onclick="return IMG1_onclick()"/></td>
                            </tr>
                            <tr><td style="height:5px"></td></tr>
                            <tr>
                                <td align="Left" class="tour" style="height: 50px">
                                    &nbsp;<img  loading="lazy" alt="bullet" src="Assets/images/bullet.gif" alt="-" border="0" /><a href="Agentspecial.aspx" class="tour">Special Packages</a>&nbsp;&nbsp;
                                    <img  loading="lazy" alt="bullet" src="Assets/images/bullet.gif" border="0" /><a href="GroupBookingRequestform.aspx" target ="_parent" class="tour">Group Booking</a>
                                    <br /><br />
                                    <b>&nbsp;Fixed Tours Booking</b>
                                    </td>
                            </tr>
                            <tr>
                                <td align="center" background="Assets/images/green_img.jpg" style="background-position:bottom; background-repeat:no-repeat; height: 277px;" valign="top">
                                    <table width="96%" border="0" cellspacing="0" cellpadding="0">
                                        <tr class="verdana11bk">
                                            <td colspan="2" valign="top">
                                                <table border="0" style="height:100px;" cellpadding="0" cellspacing="0">
                                                    <tr >
                                                        <td align="left"><input id="Radio1" checked="true" onclick="javascript:Search();" type="radio" value="DEL0001" name="grpBranchCity" runat="server" />Delhi</td>
                                                        <td align="left"><input id="Radio2" type="radio" onclick="javascript:Search();" value="HYD001" name="grpBranchCity" runat="server" />Hyderabad</td>
                                                        <td rowspan="2" valign="bottom">
                                                            <!--<a href="javascript:Search();" id="btnChkCity" >Search</a>-->
                                                            </td>
                                                    </tr>     
                                                    <tr >
                                                       <td align="left"><input id="Radio3" type="radio" onclick="javascript:Search();" value="BAN0001" name="grpBranchCity" runat="server" />Bengaluru</td>                                                        
                                                       <td align="left"><input id="Radio4" type="radio" onclick="javascript:Search();" value="CHE0001" name="grpBranchCity" runat="server" />Chennai</td>
                                                       <td></td>
                                                    </tr>
                                                    <tr >
                                                      <td align="left"><input id="Radio5" type="radio" onclick="javascript:Search();" value="BBS001" name="grpBranchCity" runat="server" />Bhubaneswar</td>                                                       
                                                       <td align="left"><input id="Radio6" type="radio" onclick="javascript:Search();" value="AMD001" name="grpBranchCity" runat="server" />Ahmedabad</td>
                                                       <td>&nbsp;</td>
                                                    </tr>
                                                    <tr >
                                                      <td align="left"><input id="Radio7" type="radio" onclick="javascript:Search();" value="Durga Puja" name="grpBranchCity" runat="server" />Durga Puja Special</td>                                                       
                                                       <td align="left">&nbsp;</td>
                                                       <td>&nbsp;</td>
                                                    </tr>
                                                    <tr style="height:40px;">
                                                        <td colspan="3" align="center" valign="middle"> 
                                                            <div id="imgWait" style="display:none;">
                                                                <img  loading="lazy" width="33px" height="33px"  src="Assets/images/wait_spinner.gif" alt="wait_spinner" /><br />
                                                                Please wait...
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr class="verdana11bk">
                                            <td width="30%" height="30" align="left">
                                                Tour Name
                                            </td>
                                            <td width="70%" align="left">
                                                <%--  <label>
                                                    <select name="select3" size="1">
                                                        <option selected="selected">North India</option>
                                                        <option>South India</option>
                                                        <option>East India</option>
                                                        <option>West India</option>
                                                    </select>
                                                </label>--%>
                                                <asp:DropDownList ID="ddlTourName" runat="server" DataValueField="tourno" Width="180" DataTextField="tourname">
                                                    <asp:ListItem Text="Select" Selected="True">
                                                
                                                    </asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr class="verdana11bk">
                                            <td colspan="2" align="right">
                                                <asp:ImageButton ID="btnBooking" runat="server" ImageUrl="Assets/images/book_now.gif" Width="67"
                                                    Height="20" OnClick="btnBooking_Click" />&nbsp;&nbsp;
                                            </td>
                                        </tr>
                                        
                                    </table>
                                </td>
                            </tr>
                            
                        </table>
                    </td>
                    <td align="right" bgcolor="#E7E7E7" style="width: 9px">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="left" style="height: 8px">
                        <img  loading="lazy" src="Assets/images/left_d_corner.gif" width="8" height="8" alt="left_d_corner" /></td>
                    <td bgcolor="#E7E7E7" style="height: 8px">
                        <img  loading="lazy" src="Assets/images/trans.gif" width="1" height="1" alt="trans" /></td>
                    <td align="right" style="width: 9px; height: 8px">
                        <img  loading="lazy" src="Assets/images/rgt_d_corner.gif" width="8" height="8" alt="rgt_d_corner" /></td>
                </tr>
            </table>
            <map name="Map" id="Map">
                <area shape="circle" coords="236,53,27" href="Agentspecial.aspx" title="Special Tour" alt="Special Packages"/>
                <area shape="circle" coords="171,53,27" href="Agentcarbooking.aspx" title="Book Your Car" alt="Car Booking" />
                <area shape="circle" coords="107,52,27" href="Agenthotels_iframe.aspx" title="Book Your Hotel"  alt="Hotel Booking"/>
            </map>
        </div>
        <script>
            <!--
                Search();
function IMG1_onclick() {

}

            -->
        </script>
    </form>
</body>
</html>

