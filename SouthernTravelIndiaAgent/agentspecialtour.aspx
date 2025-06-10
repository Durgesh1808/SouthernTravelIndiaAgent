<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="agentspecialtour.aspx.cs" Inherits="SouthernTravelIndiaAgent.agentspecialtour" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="uc1" TagName="AgentFooter" Src="UserControlS/UcAgentFooter.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AgentHeader" Src="UserControlS/UcAgentHeader.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Southern Travels :-: Agent Special Tour Booking</title>   
    <link href="../StyleSheets/style.css" rel="stylesheet" type="text/css" />
    <link href="../Assets/css/stylesheet.css" type="text/css" rel="stylesheet" />
    <link href="../Assets/css/calendar.css" rel="stylesheet" type="text/css" />
    <script language="javascript" src="Assets/js/AgentsplCalender.js" type="text/javascript"></script>
    <script language="javascript" src="../Assets/js/MyScript.js" type="text/javascript"></script>
    <script language="javascript" src="Assets/js/AgntSplTour.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
    function approve()
     {       
          window.open('../frmterms.aspx?','pops','width=418,height=249,scrollbars=yes');
     }    
    function fnDrophide()
    {
        if(document.getElementById('chkDrop').checked==true)
        {  
           document.getElementById('RadFlight_d').checked = document.getElementById('RadFlight').checked;
           document.getElementById('RadTrain_d').checked = document.getElementById('RadTrain').checked;
           document.getElementById('RadBus_d').checked = document.getElementById('RadBus').checked;
           copyToDepart(document.getElementById('txtpickVehicleNo')); 
           copyToDepart(document.getElementById('txtFlightNo'));
           copyToDepart(document.getElementById('ddlPkHrs'));
           copyToDepart(document.getElementById('ddlPkMints'));
           copyToDepart(document.getElementById('txtRlyStationName'));
           copyToDepart(document.getElementById('ddlTrainPkHr'));
           copyToDepart(document.getElementById('ddlTrainPkMin'));
           copyToDepart(document.getElementById('txtTrainNo'));
           copyToDepart(document.getElementById('txtAddr'));
           copyToDepart(document.getElementById('txtStreet'));
           
           
          if(document.getElementById('RadFlight').checked)          
                fnRDOSelection('','RadFlight');           
          if(document.getElementById('RadTrain').checked)
                fnRDOSelection('','RadTrain');
          if(document.getElementById('RadBus').checked)
                fnRDOSelection('','RadBus');
            
        }
        else
        {
             //document.getElementById('divHide').style.display='block';
        }
    }
    function fnhideQualis()
    {       
        if(document.getElementById('txtCar12pxHiddenSt').value==0 && document.getElementById('txtCar12pxHiddenDl').value==0)
        {
           document.getElementById('hideCar').style.display='none';
        }        
        if(document.getElementById('txtQua46PxHiddenSt').value==0 && document.getElementById('txtQua46PxHiddenDl').value==0)
        {
           document.getElementById('hideQuils').style.display='none';
        }        
        if(document.getElementById('txtInn45PxHiddenSt').value==0 && document.getElementById('txtInn45PxHiddenDl').value==0)
        {
           document.getElementById('hideInn').style.display='none';
        }        
        if(document.getElementById('txtTem89PxHiddenSt').value==0 && document.getElementById('txtTem89PxHiddenDl').value==0)
        {
           document.getElementById('hideTempo').style.display='none';
        }       
    }
    function showHide(k)
   {             
        if(document.getElementById('RadTrain'+k).checked)
        {  
           document.getElementById('hideTrain'+k).style.display='block';
           document.getElementById('hideFlight'+k).style.display='none';
           document.getElementById('hideLocation'+k).style.display='none';
          
            if(k=="")
            {
              document.getElementById('txtpickVehicleNo').selectedIndex = 0;
              document.getElementById('txtFlightNo').value =''; 
              document.getElementById('txtAddr').value ='';
              document.getElementById('txtStreet').value ='';
              // To clear hte drop text boxes                       
              document.getElementById('txtpickVehicleNo_d').selectedIndex = 0;
              document.getElementById('txtFlightNo_d').value ='';
              document.getElementById('txtAddr_d').value ='';
              document.getElementById('txtStreet_d').value ='';
            }
         }            
        if(document.getElementById('RadFlight'+k).checked)
        {
           document.getElementById('hideTrain'+k).style.display='none';
           document.getElementById('hideFlight'+k).style.display='block';
           document.getElementById('hideLocation'+k).style.display='none';

              if(k=="")
              {                           
                   document.getElementById('txtAddr').value ='';
                   document.getElementById('txtStreet').value ='';                       
                   document.getElementById('txtRlyStationName').value ='';                       
                   document.getElementById('txtTrainNo').value ='';
                     // To clear hte drop text boxes                       
                   document.getElementById('txtAddr_d').value ='';
                   document.getElementById('txtStreet_d').value ='';                       
                   document.getElementById('txtRlyStationName_d').value ='';                      
                   document.getElementById('txtTrainNo_d').value ='';
              }
        }            
        if(document.getElementById('RadBus'+k).checked)
        {
           document.getElementById('hideTrain'+k).style.display='none';
           document.getElementById('hideFlight'+k).style.display='none';
           document.getElementById('hideLocation'+k).style.display='block';
           if(k=="")
           {
               document.getElementById('txtpickVehicleNo').selectedIndex = 0;
               document.getElementById('txtFlightNo').value ='';                    
               document.getElementById('txtRlyStationName').value ='';
               document.getElementById('txtTrainNo').value ='';
                 // To clear hte drop text boxes
               document.getElementById('txtpickVehicleNo_d').selectedIndex = 0;
               document.getElementById('txtFlightNo_d').value ='';                    
               document.getElementById('txtRlyStationName_d').value ='';                 
               document.getElementById('txtTrainNo_d').value ='';
           }
        }
   }  
    
   
   function fnRDOSelection(k,idRad)
   {        
        showHide(k); 
        var chkDropObj =  document.getElementById('chkDrop');
        if((chkDropObj.checked) && (k==''))
        {            
            document.getElementById(idRad+'_d').checked = true;
            showHide('_d');
        }  
   }   
   	 function clear5()
   	 {
   	
   	        if(!document.getElementById('chkDrop').checked)
   	        {
   	            var allTxt  = document.getElementsByTagName('input');
   	            
	           for(var i=0; i <allTxt.length; i++)
	           {
	              if( allTxt[i].id.indexOf('_d')!=-1)
	              	                    
	                        document.getElementById(allTxt[i].id).value="";
	         
	           }
	       }
	}

function fnchkSingle()
	{
	     if(document.getElementById('txtPax').value=="")
	     {
	        alert("Please enter the Pax no");
	        document.getElementById('txtPax').focus();
	        document.getElementById('chkSingle').checked=false;
	        //ExtrafareCal();
	        return false;
	     }
	     
	     if((document.getElementById('chkSingle').checked))
	     {
	       document.getElementById('SinglePax').style.display='block';
	       
	     }
	     else
	     {
	        document.getElementById('SinglePax').style.display='none';
	     }
	      
	      
	     if((!document.getElementById('chkSingle').checked)&&(document.getElementById('txtTotalExtrafare').value!=""))
	     {
	       
	      
	        Getsuggession();
	       
	     }
   }
   
   function fnchkSingle2()
	{
	     if((!document.getElementById('chkSingle').checked) &&(document.getElementById('txtSinglePax').value>0)) 
	     {
	        alert("Please Check the single adult in a roon check box");
	        return false;
	       
	      }
	     
   }
   
function postlimit()
{
	var maxlength = 150;

	if (document.form1.txtComment.value.length > maxlength)
    {
		alert("You have typed the maximun amount of characters for your Address.\n\n 150 Characters is the maximum.");
		document.form1.txtComment.value = document.form1.txtComment.value.substring(0, maxlength);
	}
   
}
    
   
    </script>

</head>
<body vlink="#000000" alink="#000000" link="#000000" bgcolor="#ffffff" leftmargin="0"
    topmargin="0" onload="get(); fnhideQualis();" marginwidth="0" marginheight="0"
    bottommargin="0" rightmargin="0" style="background-repeat: repeat-x">
    <div id="STContainer" style="display: none; position: absolute">
    </div>
    <div id="framediv" style="z-index: 1; left: -999px; width: 148px; position: absolute;
        height: 194px;" frameborder="0" scrolling="no">
    </div>
    <form id="form1" runat="server">
        <input type="hidden" id="txtServiceTax" runat="server" />
        <input type="hidden" runat="server" id="car2pax" />
        <input type="hidden" runat="server" id="car34pax" />
        <input type="hidden" runat="server" id="innova45pax" />
        <input type="hidden" runat="server" id="innova67pax" />
        <input type="hidden" runat="server" id="qualis46pax" />
        <input type="hidden" runat="server" id="qualis78pax" />
        <input type="hidden" runat="server" id="tempo89pax" />
        <input type="hidden" runat="server" id="car2min" />
        <input type="hidden" runat="server" id="car2max" />
        <input type="hidden" runat="server" id="car34min" />
        <input type="hidden" runat="server" id="car34max" />
        <input type="hidden" runat="server" id="innova45min" />
        <input type="hidden" runat="server" id="innova45max" />
        <input type="hidden" runat="server" id="innova67min" />
        <input type="hidden" runat="server" id="innova67max" />
        <input type="hidden" runat="server" id="qualis46min" />
        <input type="hidden" runat="server" id="qualis46max" />
        <input type="hidden" runat="server" id="qualis78min" />
        <input type="hidden" runat="server" id="qualis78max" />
        <input type="hidden" runat="server" id="tempo89min" />
        <input type="hidden" runat="server" id="tempo89max" />
        <input type="hidden" runat="server" id="txtCar12pxHiddenSt" />
        <input type="hidden" runat="server" id="txtCar12pxHiddenDl" />
        <input type="hidden" runat="server" id="txtCar34pxHiddenSt" />
        <input type="hidden" runat="server" id="txtCar34pxHiddenDl" />
        <input type="hidden" runat="server" id="txtInn45PxHiddenSt" />
        <input type="hidden" runat="server" id="txtInn45PxHiddenDl" />
        <input type="hidden" runat="server" id="txtInn67PxHiddenSt" />
        <input type="hidden" runat="server" id="txtInn67PxHiddenDl" />
        <input type="hidden" runat="server" id="txtQua46PxHiddenSt" />
        <input type="hidden" runat="server" id="txtQua46PxHiddenDl" />
        <input type="hidden" runat="server" id="txtQua78PxHiddenSt" />
        <input type="hidden" runat="server" id="txtQua78PxHiddenDl" />
        <input type="hidden" runat="server" id="txtTem89PxHiddenSt" />
        <input type="hidden" runat="server" id="txtTem89PxHiddenDl" />
        <input type="hidden" runat="server" id="depttime" />
        <input type="hidden" runat="server" id="SARstandard" />
        <input type="hidden" runat="server" id="SARdeluxe" />
        <input type="hidden" runat="server" id="tid" />
        <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td colspan="2">
                    <uc1:AgentHeader ID="agentHeader" runat="server" />
                </td>
            </tr>
            <tr>
                <td width="80%" style="height: 226px" valign="top" align="center" colspan="2">
                    <table width="80%" cellspacing="0" cellpadding="0" border="0" style="background-color: #cccccc">
                        <tr>
                            <td align="left" style="width: 9px">
                                <img  loading="lazy" src="Assets/images/left_corner.gif" alt="left_corner" /></td>
                            <td bgcolor="#e7e7e7">
                                <img  loading="lazy" src="Assets/images/trans.gif" alt="trans" /></td>
                            <td align="right">
                                <img  loading="lazy" src="Assets/images/rgt_corner.gif" alt="rgt_corner" /></td>
                        </tr>
                        <tr>
                            <td bgcolor="#E7E7E7" style="width: 9px">
                                &nbsp;</td>
                            <td align="center" width="100%">
                                <table cellpadding="1" cellspacing="1" border="0" width="100%" class="footer" style="text-align: left"
                                    bgcolor="#ffffff">
                                    <tr class="hlinks">
                                        <td align="center" colspan="2" bgcolor="#5aa1ea" class="verdana14w" height="25Px">
                                            <b>On Line Special Tour Details</b></td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2">
                                            <table width="100%" border="0" cellspacing="2" cellpadding="0" bgcolor="#FFFFFF"
                                                align="center">
                                                <tr>
                                                    <td class="hlinks" bgcolor="#ffffff" align="center" height="20" valign="bottom">
                                                        <b>Package cost per person in INR (Rs)</b></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 100%;" bgcolor="#FFFFFF" height="180" align="left">

                                                        <script language="javascript" src="../JavaScript/Spltours/tour<%=strTourId%>.js" type="text/javascript"></script>

                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr class="hlinks">
                                        <td align="center" colspan="2" bgcolor="#5aa1ea" class="verdana14w" height="10Px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2">
                                            <table align="center">
                                                <tr>
                                                    <td>
                                                        <div id="imgWait" style="display: none;">
                                                            <img  loading="lazy" alt="wait_spinner" width="33px" height="33px" src="Assets/images/wait_spinner.gif" />
                                                            Please wait...
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                            <%-- <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td align="center" colspan="2">
                                                        <asp:Label Visible="false" ID="lblMsg" runat="server">  </asp:Label></td>
                                                </tr>
                                            </table>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" style="width: 315px">
                                            <table cellpadding="1" cellspacing="1" border="0" width="100%" class="footer" bgcolor="#ffffff">
                                                <tr id="trHide1" class="hlinks">
                                                    <td style="width: 423px">
                                                        Tour Name :</td>
                                                    <td>
                                                        <asp:Label ID="txttourName" runat="server" Font-Bold="true" BorderStyle="None" BorderWidth="0"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 423px; height: 28px;">
                                                        Category Type :</td>
                                                    <td style="height: 28px">
                                                        Standard<input type="radio" runat="server" id="rdoStandard" name="rdocategory" onclick="javascript:FareTypeCheck(this.value);ExtrafareCal();"
                                                            value="St" />&nbsp; Deluxe<input type="radio" checked="true" runat="server" id="rdoDeluxe"
                                                                name="rdocategory" onclick="FareTypeCheck(this.value),ExtrafareCal();" value="Dl" />
                                                    </td>
                                                </tr>
                                                <tr id="trHide3" class="hlinks">
                                                    <td style="width: 423px">
                                                        No Of &nbsp; Pax :*</td>
                                                    <td>
                                                        <asp:TextBox ID="txtPax" MaxLength="2" size="5" onblur="Getsuggession();" Style="text-align: right"
                                                            runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 423px">
                                                        Journey&nbsp; Date :*</td>
                                                    <td>
                                                        <input id="txtDate" name="txtDate" onchange="alert(this.value);" type="text" runat="server"
                                                            size="9" onfocus="objCal('DepartIcon','txtDate','360','160'); " /><a title="journey date"
                                                                href="javascript:void(null)" onclick="objCal('DepartIcon','txtDate','360','160');"
                                                                hidefocus=""><img  loading="lazy" alt="calendar" id="DepartIcon" tabindex="-1" src="Assets/images/calendar.gif" border="0"
                                                                    style="visibility: visible; padding-bottom: 0px;" /></a><asp:Label ID="dept" CssClass="hlinks"
                                                                        runat="server">&nbsp;@&nbsp;<%=depttime.Value %></asp:Label>
                                                    </td>
                                                </tr>

                                                <script language="javascript" type="text/javascript">
                                                        stObj = new SOUTHERN.caldoy.Calendar2up("stObj","STContainer",(thisMonth+1)+"/"+thisYear,(thisMonth+1)+"/"+(thisDay+1)+"/"+thisYear);
                                                       // stObj = new SOUTHERN.caldoy.Calendar2up("stObj","STContainer","05/2008","05/03/2008");
                                                        stObj.title = " &nbsp;&nbsp;&nbsp;&nbsp;Select Date &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                                        stObj.setChildFunction("onSelect",setDate);
                                                        stObj.render();	
                                                </script>

                                                <tr id="Tr1" class="hlinks">
                                                    <td colspan="2">
                                                        <asp:CheckBox ID="chkSingle" runat="server" onclick="javascript:fnchkSingle();" />
                                                        <b><span><font color="red" size="3">*</font></span>Single Adult In a Room</b>&nbsp;</td>
                                                    <td>
                                                </tr>
                                                <tr id="Singlepax" class="hlinks" align="left" style="display: none">
                                                    <td style="width: 423px">
                                                        Single Pax</td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtSinglePax" MaxLength="2" size="5" Style="text-align: right" Width="15"
                                                            runat="server" onblur="javascript:return ExtrafareCal();" onclick="javascript:fnchkSingle2();"></asp:TextBox>@<asp:TextBox
                                                                ID="txtExtraFareS" Style="text-align: right" MaxLength="10" size="4" runat="server"
                                                                ReadOnly="true"></asp:TextBox>&nbsp;Fare<asp:TextBox ID="txtTotalExtrafare" MaxLength="10"
                                                                    size="4" runat="server" Style="text-align: right" ReadOnly="true"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr id="Tr4" class="hlinks" align="left">
                                                    <td colspan="5">
                                                        <b>#For single occupancy</b></td>
                                                </tr>
                                                <tr id="Tr2" class="hlinks">
                                                    <td style="width: 423px">
                                                        GST</td>
                                                    <td>
                                                        <asp:TextBox ID="txtTotalServiceTax" size="5" Style="text-align: right" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr id="Tr3" class="hlinks">
                                                    <td style="width: 423px">
                                                        Total Fare</td>
                                                    <td>
                                                        <asp:TextBox ID="txtFareTotal" size="5" runat="server" Style="text-align: right"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td valign="top">
                                            <table cellpadding="1" cellspacing="1" border="0" width="100%" class="footer" style="text-align: left"
                                                bgcolor="#ffffff">
                                                <tr>
                                                    <td align="center">
                                                        <table border="0" align="center" cellpadding="3" cellspacing="1">
                                                            <tr>
                                                                <td height="30" bgcolor="#5aa1ea">
                                                                    <div align="center" class="verdana11w">
                                                                        <b>Vehicle</b></div>
                                                                </td>
                                                                <td height="30" bgcolor="#5aa1ea">
                                                                    <div align="center" class="verdana11w">
                                                                        <b>Capacity</b></div>
                                                                </td>
                                                                <td height="30" bgcolor="#5aa1ea" style="width: 73px">
                                                                    <div align="center" class="verdana11w">
                                                                        <b>No. of Pax </b>
                                                                    </div>
                                                                </td>
                                                                <td height="30" bgcolor="#5aa1ea">
                                                                    <div align="center" class="verdana11w">
                                                                        <b>Fare Per Pax</b></div>
                                                                </td>
                                                                <td bgcolor="#5aa1ea">
                                                                    <div align="center" class="verdana11w">
                                                                        <b>Total Fare</b></div>
                                                                </td>
                                                            </tr>
                                                            <tr id="hideCar">
                                                                <td align="center" bgcolor="#9bc7f4" style="height: 42px">
                                                                    <span class="verdana11bk">Car</span></td>
                                                                <td valign="middle" bgcolor="#9bc7f4" align="left" style="height: 42px; width: 69px;">
                                                                    <span class="verdana11bk">
                                                                        <input type="checkbox" name="checkbox222" runat="server" id="chkCar2" onclick="chkCarvisible();" />
                                                                        2pax<br />
                                                                        <input type="checkbox" name="checkbox322" value="checkbox" id="chkCar3-4" onclick="chkCarvisible();" />
                                                                        3-4pax</span></td>
                                                                <td align="center" valign="middle" bgcolor="#9bc7f4" style="height: 42px; width: 73px;">
                                                                    <input name="txtCar12px" type="text" class="verdana11bk" onblur="car2(this.id);"
                                                                        readonly="readonly" runat="server" size="5" id="txtCar12px" style="text-align: right"
                                                                        maxlength="2" />
                                                                    <br />
                                                                    <span class="verdana11bk">
                                                                        <input name="txtCar34px" type="text" class="verdana11bk" size="5" onblur="car34(this.id);"
                                                                            readonly="readonly" runat="server" id="txtCar34px" style="text-align: right"
                                                                            maxlength="2" />
                                                                    </span>
                                                                </td>
                                                                <td align="center" valign="middle" bgcolor="#9bc7f4" style="height: 42px">
                                                                    <input type="text" id="txtCar12pxfare" class="verdana11bk" style="text-align: right"
                                                                        size="10" readonly="readOnly" /><br />
                                                                    <span class="verdana11bk">
                                                                        <input type="text" id="txtCar34pxfare" style="text-align: right" class="verdana11bk"
                                                                            size="10" readonly="readOnly" /></span>
                                                                </td>
                                                                <td align="center" valign="middle" bgcolor="#9bc7f4" style="height: 42px">
                                                                    <input type="text" class="verdana11bk" size="10" id="txtCar12pxValue" style="text-align: right" /><br />
                                                                    <span class="verdana11bk">
                                                                        <input type="text" id="txtCar34pxValue" class="verdana11bk" style="text-align: right"
                                                                            size="10" /></span>
                                                                </td>
                                                            </tr>
                                                            <tr id="hideInn">
                                                                <td height="41" align="center" bgcolor="#9bc7f4">
                                                                    <span class="verdana11bk">Innova</span></td>
                                                                <td valign="middle" bgcolor="#9bc7f4" align="left" style="width: 69px">
                                                                    <span class="verdana11bk">
                                                                        <input type="checkbox" name="checkbox22" value="checkbox" id="chkinnova4-5" onclick="chkCarvisible();" />
                                                                        4-5pax<br />
                                                                        <input type="checkbox" name="checkbox32" value="checkbox" id="chkinnova6-7" onclick="chkCarvisible();" />
                                                                        6-7pax</span></td>
                                                                <td align="center" valign="middle" bgcolor="#9bc7f4" style="width: 73px">
                                                                    <input name="txtInn45Px" type="text" class="verdana11bk" readonly="readonly" runat="server"
                                                                        onblur="innova45(this.id);" size="5" id="txtInn45Px" style="text-align: right"
                                                                        maxlength="2" />
                                                                    <br />
                                                                    <span class="verdana11bk">
                                                                        <input name="txtInn67Px" type="text" class="verdana11bk" style="text-align: right"
                                                                            readonly="readonly" runat="server" onblur="innova67(this.id);" size="5" id="txtInn67Px"
                                                                            maxlength="2" />
                                                                    </span>
                                                                </td>
                                                                <td align="center" valign="middle" bgcolor="#9bc7f4">
                                                                    <input type="text" id="txtInn45Pxfare" name="txti45" class="verdana11bk" style="text-align: right"
                                                                        size="10" readonly="readOnly" /><br />
                                                                    <span class="verdana11bk">
                                                                        <input type="text" id="txtInn67Pxfare" name="txti67" style="text-align: right" class="verdana11bk"
                                                                            size="10" readonly="readOnly" /></span>
                                                                </td>
                                                                <td align="center" valign="middle" bgcolor="#9bc7f4">
                                                                    <input name="textfield2322253" type="text" class="verdana11bk" size="10" style="text-align: right"
                                                                        id="txtInn45PxValue" /><br />
                                                                    <span class="verdana11bk">
                                                                        <input name="textfield2332253" type="text" class="verdana11bk" size="10" style="text-align: right"
                                                                            id="txtInn67PxValue" /></span>
                                                                </td>
                                                            </tr>
                                                            <tr id="hideQuils">
                                                                <td align="center" bgcolor="#9bc7f4">
                                                                    <span class="verdana11bk">Qualis</span></td>
                                                                <td valign="middle" bgcolor="#9bc7f4" align="left" style="width: 69px">
                                                                    <span class="verdana11bk">
                                                                        <input type="checkbox" name="checkbox23" value="checkbox" id="chkqualis4-6" onclick="chkCarvisible();" />
                                                                        4-6pax<br />
                                                                        <input type="checkbox" name="checkbox33" value="checkbox" id="chkqualis7-8" onclick="chkCarvisible();" />
                                                                        7-8pax</span></td>
                                                                <td align="center" valign="middle" bgcolor="#9bc7f4" style="width: 73px">
                                                                    <input name="txtQua46Px" style="text-align: right" type="text" class="verdana11bk"
                                                                        readonly="readonly" onblur="qualis46(this.id);" runat="server" size="5" id="txtQua46Px"
                                                                        maxlength="2" />
                                                                    <br />
                                                                    <span class="verdana11bk">
                                                                        <input name="txtQua78Px" runat="server" style="text-align: right" type="text" class="verdana11bk"
                                                                            readonly="readonly" size="5" onblur="qualis78(this.id);" id="txtQua78Px" maxlength="2" />
                                                                    </span>
                                                                </td>
                                                                <td align="center" valign="middle" bgcolor="#9bc7f4">
                                                                    <input type="text" id="txtQua46Pxfare" style="text-align: right" name="txtqfare46"
                                                                        class="verdana11bk" size="10" readonly="readOnly" /><br />
                                                                    <span class="verdana11bk">
                                                                        <input type="text" id="txtQua78Pxfare" style="text-align: right" name="txtqfare78"
                                                                            class="verdana11bk" size="10" readonly="readOnly" /></span>
                                                                </td>
                                                                <td align="center" valign="middle" bgcolor="#9bc7f4">
                                                                    <input name="textq46" type="text" style="text-align: right" class="verdana11bk" size="10"
                                                                        id="txtQua46PxValue" /><br />
                                                                    <span class="verdana11bk">
                                                                        <input name="textq78" type="text" style="text-align: right" class="verdana11bk" size="10"
                                                                            id="txtQua78PxValue" /></span>
                                                                </td>
                                                            </tr>
                                                            <tr id="hideTempo">
                                                                <td align="center" bgcolor="#9bc7f4">
                                                                    <span class="verdana11bk">Tempo</span></td>
                                                                <td valign="middle" bgcolor="#9bc7f4" align="left" style="width: 69px">
                                                                    <span class="verdana11bk">
                                                                        <input type="checkbox" name="checkbox34" value="checkbox" id="chktempo8-9" onclick="chkCarvisible();" />
                                                                        8-9pax</span></td>
                                                                <td align="center" valign="middle" bgcolor="#9bc7f4" style="width: 73px">
                                                                    <span class="verdana11bk">
                                                                        <input name="txtTem89Px" style="text-align: right" runat="server" type="text" class="verdana11bk"
                                                                            readonly="readonly" size="5" onblur="tempo89(this.id);" id="txtTem89Px" maxlength="2" />
                                                                    </span>
                                                                </td>
                                                                <td align="center" valign="middle" bgcolor="#9bc7f4">
                                                                    <input type="text" id="txtTem89Pxfare" style="text-align: right" class="verdana11bk"
                                                                        size="10" readonly="readOnly" />
                                                                </td>
                                                                <td align="center" valign="middle" bgcolor="#9bc7f4">
                                                                    <span class="verdana11bk">
                                                                        <input name="textfield233225" type="text" style="text-align: right" class="verdana11bk"
                                                                            size="10" id="txtTem89PxValue" />
                                                                    </span>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td height="30" colspan="2" align="right" bgcolor="#9bc7f4">
                                                                    <span class="verdana11bk">Total Pax: </span>
                                                                </td>
                                                                <td colspan="1" bgcolor="#9bc7f4" style="width: 73px">
                                                                    <input name="textfield2332242" type="text" style="text-align: right" class="verdana11bk"
                                                                        size="5" id="txtTotalPax" runat="server" readonly="readOnly" />
                                                                </td>
                                                                <td align="right" valign="middle" bgcolor="#9bc7f4">
                                                                    <span class="verdana11bk">&nbsp;Fare: </span>
                                                                </td>
                                                                <td bgcolor="#9bc7f4">
                                                                    <span class="verdana11bk">
                                                                        <input name="textfield2332242" style="text-align: right" type="text" class="verdana11bk"
                                                                            size="10" id="txtTotalFare" runat="server" readonly="readOnly" /></span>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="Center" colspan="2" height="1" bgcolor="#cccccc">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" class="hlinks" style="height: 36px">
                                            <span><font color="red" size="3">*</font></span> For Special Tours we Provide Accommodation
                                            On Twin Sharing Basis, If Passengers are in odd numbers like 3,5,7.. we Provide
                                            one Extra bed, If Passengers Need Single Adult In a Room has to Pay Extra Amount
                                            According to the Category.</td>
                                    </tr>
                                    <tr class="hlinks">
                                        <td colspan="2" style="height: 20px">
                                            <table style="height: 20px" border="0" cellpadding="0" width="100%" cellspacing="0">
                                                <tr>
                                                    <td align="center" bgcolor="#5aa1ea" class="cgi1" style="width: 315px; height: 20px;">
                                                        <b>Personal Information</b></td>
                                                    <td align="center" bgcolor="#5aa1ea" class="cgi1" style="height: 20px">
                                                        <b>PickUp Information</b></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr class="hlinks" valign="top">
                                        <td align="left" style="width: 315px;">
                                            <table width="100%" border="0" cellspacing="2" cellpadding="2" bgcolor="#FFFFFF"
                                                align="center">
                                                <tr class="hlinks" align="left" valign="top">
                                                    <td align="left">
                                                        Name:*</td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlTitle" runat="server" Width="30%">
                                                            <asp:ListItem Text="Title" Value="Title"></asp:ListItem>
                                                            <asp:ListItem Text="Mr." Value="Mr"></asp:ListItem>
                                                            <asp:ListItem Text="Mrs." Value="Mrs"></asp:ListItem>
                                                            <asp:ListItem Text="Miss." Value="Miss"></asp:ListItem>
                                                            <asp:ListItem Text="Dr." Value="Dr"></asp:ListItem>
                                                            <asp:ListItem Text="Prof." Value="Prof"></asp:ListItem>
                                                        </asp:DropDownList>&nbsp;
                                                        <asp:TextBox ID="txtFName" MaxLength="30" size="15" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr class="hlinks" align="left">
                                                    <td align="left" style="height: 28px">
                                                        Address:*</td>
                                                    <td style="height: 28px">
                                                        <asp:TextBox ID="txtAddress" MaxLength="250" size="15" runat="server"></asp:TextBox></td>
                                                </tr>
                                                <tr class="hlinks" align="left">
                                                    <td align="left">
                                                        Mobile:*</td>
                                                    <td>
                                                        <asp:TextBox ID="txtMobile" MaxLength="11" size="15" runat="server"></asp:TextBox></td>
                                                </tr>
                                                <tr class="hlinks" align="left">
                                                    <td align="left">
                                                        Phone No:</td>
                                                    <td>
                                                        <asp:TextBox ID="txtphone" MaxLength="11" size="15" runat="server"></asp:TextBox></td>
                                                </tr>
                                                <tr class="hlinks" align="left">
                                                    <td align="left">
                                                        EmailId:*</td>
                                                    <td>
                                                        <asp:TextBox ID="txtMail" MaxLength="50" size="15" runat="server"></asp:TextBox></td>
                                                </tr>
                                                <tr class="hlinks" align="left">
                                                    <td align="left">
                                                        Comments:</td>
                                                    <td>
                                                        <asp:TextBox ID="txtComment" MaxLength="150" size="15" runat="server" TextMode="MultiLine"></asp:TextBox></td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td colspan="2" style="height: 19px">
                                            <table width="100%" border="0" cellspacing="2" cellpadding="1" bgcolor="#FFFFFF">
                                                <tr>
                                                    <td align="left" style="height: 22px; width: 195px;">
                                                        PickUp From:</td>
                                                    <td style="height: 22px">
                                                        <asp:RadioButton ID="RadFlight" runat="server" Checked="true" GroupName="pick" Text="Flight"
                                                            onclick="javascript:fnRDOSelection('',this.id);"></asp:RadioButton>
                                                        <asp:RadioButton ID="RadTrain" runat="server" GroupName="pick" Text="Train" onclick="javascript:fnRDOSelection('',this.id);">
                                                        </asp:RadioButton><asp:RadioButton ID="RadBus" runat="server" GroupName="pick" Text="Address/Location"
                                                            onclick="javascript:fnRDOSelection('',this.id);"></asp:RadioButton>
                                                    </td>
                                                </tr>
                                                <tr class="hlinks" align="left">
                                                    <td align="left" style="width: 195px">
                                                        City:</td>
                                                    <td>
                                                        <b>&nbsp;&nbsp;&nbsp;
                                                            <asp:Label ID="lblPkPoint" MaxLength="50" size="15" runat="server"></asp:Label></b></td>
                                                </tr>
                                            </table>

                                            <script>
                                            <!-- 
                                                
                                                function copyToDepart(v){
                                               
                                                    var chkDropObj =  document.getElementById('chkDrop');
                                                    if(chkDropObj!=null){
                                                        if(chkDropObj.checked){  
                                                            var vArrObj = v;
                                                            var vDepartObj = document.getElementById(v.id+'_d');                                                                            
                                                            if((vDepartObj!=null) && (vArrObj!=null)){                                                      
                                                                vDepartObj.value =  vArrObj.value;    
                                                            }
                                                        }
                                                    }
                                                }
                                            -->
                                            </script>

                                            <table width="100%" border="0" id="hideFlight" cellspacing="2" cellpadding="1" bgcolor="#FFFFFF">
                                                <tr class="hlinks" align="left">
                                                    <td align="left" style="height: 14px; width: 212px;">
                                                        AirPort:*</td>
                                                    <td style="height: 14px">
                                                        <asp:DropDownList ID="txtpickVehicleNo" runat="server" onchange="copyToDepart(this);">
                                                            <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                                                            <asp:ListItem Text="Domestic Airport" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="International" Value="2"></asp:ListItem>
                                                        </asp:DropDownList></td>
                                                </tr>
                                                <tr class="hlinks" align="left">
                                                    <td align="left" style="height: 24px; width: 212px;">
                                                        Expected Arrival Time: *</td>
                                                    <td style="height: 24px">
                                                        <select runat="server" id="ddlPkHrs" name="ddlPkHrs" class="inputControl" onchange="copyToDepart(this);">
                                                            <option value="">hrs</option>
                                                            <option value="00">00</option>
                                                            <option value="01">01</option>
                                                            <option value="02">02</option>
                                                            <option value="03">03</option>
                                                            <option value="04">04</option>
                                                            <option value="05">05</option>
                                                            <option value="06">06</option>
                                                            <option value="07">07</option>
                                                            <option value="08">08</option>
                                                            <option value="09">09</option>
                                                            <option value="10">10</option>
                                                            <option value="11">11</option>
                                                            <option value="12">12</option>
                                                            <option value="13">13</option>
                                                            <option value="14">14</option>
                                                            <option value="15">15</option>
                                                            <option value="16">16</option>
                                                            <option value="17">17</option>
                                                            <option value="18">18</option>
                                                            <option value="19">19</option>
                                                            <option value="20">20</option>
                                                            <option value="21">21</option>
                                                            <option value="22">22</option>
                                                            <option value="23">23</option>
                                                        </select>
                                                        &nbsp;Hrs.
                                                        <select runat="server" id="ddlPkMints" name="ddlPkMints" class="inputControl" onchange="copyToDepart(this);">
                                                            <option value="">min</option>
                                                            <option value="00">00</option>
                                                            <option value="15">15</option>
                                                            <option value="30">30</option>
                                                            <option value="45">45</option>
                                                        </select>
                                                        &nbsp;Mints</td>
                                                </tr>
                                                <tr class="hlinks" align="left">
                                                    <td style="width: 212px">
                                                        Flight No:*
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtFlightNo" onkeyup="copyToDepart(this);" MaxLength="25" size="15"
                                                            runat="server"></asp:TextBox></td>
                                                </tr>
                                            </table>
                                            <!--Code by Jaggu for Toggle---------->
                                            <table width="100%" border="0" id="hideTrain" style="display: none" cellspacing="2"
                                                cellpadding="1" bgcolor="#FFFFFF">
                                                <tr class="hlinks" align="left">
                                                    <td align="left" style="height: 24px; width: 212px;">
                                                        Railway Station: *</td>
                                                    <td style="height: 24px">
                                                        <asp:TextBox ID="txtRlyStationName" onkeyup="copyToDepart(this);" MaxLength="50"
                                                            size="15" runat="server"></asp:TextBox></td>
                                                </tr>
                                                <tr class="hlinks" align="left">
                                                    <td align="left" style="height: 24px; width: 212px;">
                                                        Expected Arrival Time: *</td>
                                                    <td style="height: 24px">
                                                        <select runat="server" id="ddlTrainPkHr" name="ddlTrainPkHr" class="inputControl"
                                                            onchange="copyToDepart(this);">
                                                            <option value="">hrs</option>
                                                            <option value="00">00</option>
                                                            <option value="01">01</option>
                                                            <option value="02">02</option>
                                                            <option value="03">03</option>
                                                            <option value="04">04</option>
                                                            <option value="05">05</option>
                                                            <option value="06">06</option>
                                                            <option value="07">07</option>
                                                            <option value="08">08</option>
                                                            <option value="09">09</option>
                                                            <option value="10">10</option>
                                                            <option value="11">11</option>
                                                            <option value="12">12</option>
                                                            <option value="13">13</option>
                                                            <option value="14">14</option>
                                                            <option value="15">15</option>
                                                            <option value="16">16</option>
                                                            <option value="17">17</option>
                                                            <option value="18">18</option>
                                                            <option value="19">19</option>
                                                            <option value="20">20</option>
                                                            <option value="21">21</option>
                                                            <option value="22">22</option>
                                                            <option value="23">23</option>
                                                        </select>
                                                        &nbsp;Hrs.
                                                        <select runat="server" id="ddlTrainPkMin" name="ddlTrainPkMin" class="inputControl"
                                                            onchange="copyToDepart(this);">
                                                            <option value="">min</option>
                                                            <option value="00">00</option>
                                                            <option value="15">15</option>
                                                            <option value="30">30</option>
                                                            <option value="45">45</option>
                                                        </select>
                                                        &nbsp;Mints.</td>
                                                </tr>
                                                <tr class="hlinks" align="left">
                                                    <td style="width: 212px">
                                                        Train No:*
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtTrainNo" onkeyup="copyToDepart(this);" MaxLength="25" size="15"
                                                            runat="server"></asp:TextBox></td>
                                                </tr>
                                            </table>
                                            <!--Code For Location/Address------>
                                            <table width="100%" border="0" id="hideLocation" style="display: none" cellspacing="2"
                                                cellpadding="1" bgcolor="#FFFFFF">
                                                <tr class="hlinks" align="left">
                                                    <td align="left" style="height: 24px; width: 212px;">
                                                        Address: *</td>
                                                    <td style="height: 24px">
                                                        <asp:TextBox ID="txtAddr" onkeyup="copyToDepart(this);" MaxLength="50" size="15"
                                                            runat="server"></asp:TextBox></td>
                                                </tr>
                                                <tr class="hlinks" align="left">
                                                    <td align="left" style="height: 24px; width: 212px;">
                                                        Street:*
                                                    </td>
                                                    <td style="height: 24px">
                                                        <asp:TextBox ID="txtStreet" onkeyup="copyToDepart(this);" MaxLength="50" size="15"
                                                            runat="server"></asp:TextBox></td>
                                                </tr>
                                            </table>
                                            <table width="100%" cellpadding="2" cellspacing="2" border="0" bgcolor="#ffffff">
                                                <tr class="hlinks" align="Center">
                                                    <td align="Center" colspan="4" class="verdana11w" bgcolor="#5aa1ea">
                                                        <b>Drop Information</b>
                                                        <asp:CheckBox ID="chkDrop" runat="server" onclick="javascript:fnDrophide(this.id);clear5();" />&nbsp;Same
                                                        As Above
                                                    </td>
                                                </tr>
                                            </table>
                                            <table width="100%" cellpadding="2" cellspacing="2" border="0" bgcolor="#ffffff">
                                                <tr>
                                                    <td align="Center" colspan="4" height="1" bgcolor="#cccccc">
                                                    </td>
                                                </tr>
                                                <tr class="hlinks" align="left">
                                                    <td align="left" style="height: 20px; width: 154px;">
                                                        Drop at:</td>
                                                    <td style="height: 20px">
                                                        <asp:RadioButton ID="RadFlight_d" onclick="javascript:showHide('_d');" runat="server"
                                                            GroupName="Drop" Text="Flight"></asp:RadioButton>
                                                        <asp:RadioButton ID="RadTrain_d" onclick="javascript:showHide('_d');" runat="server"
                                                            GroupName="Drop" Text="Train"></asp:RadioButton><asp:RadioButton ID="RadBus_d" onclick="javascript:showHide('_d');"
                                                                runat="server" GroupName="Drop" Text="Location/Address"></asp:RadioButton>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table width="100%" border="0" id="hideFlight_d" cellspacing="2" cellpadding="1"
                                                bgcolor="#FFFFFF">
                                                <tr class="hlinks" align="left">
                                                    <td align="left" style="height: 14px; width: 212px;">
                                                        Flight Type:</td>
                                                    <td style="height: 14px">
                                                        <asp:DropDownList ID="txtpickVehicleNo_d" runat="server">
                                                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="Domestic Airport" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="International" Value="2"></asp:ListItem>
                                                        </asp:DropDownList></td>
                                                </tr>
                                                <tr class="hlinks" align="left">
                                                    <td align="left" style="height: 24px; width: 212px;">
                                                        Departure Time:
                                                    </td>
                                                    <td style="height: 24px">
                                                        <select runat="server" id="ddlPkHrs_d" name="ddlPkHrs_d" class="inputControl">
                                                            <option value="">hrs</option>
                                                            <option value="00">00</option>
                                                            <option value="01">01</option>
                                                            <option value="02">02</option>
                                                            <option value="03">03</option>
                                                            <option value="04">04</option>
                                                            <option value="05">05</option>
                                                            <option value="06">06</option>
                                                            <option value="07">07</option>
                                                            <option value="08">08</option>
                                                            <option value="09">09</option>
                                                            <option value="10">10</option>
                                                            <option value="11">11</option>
                                                            <option value="12">12</option>
                                                            <option value="13">13</option>
                                                            <option value="14">14</option>
                                                            <option value="15">15</option>
                                                            <option value="16">16</option>
                                                            <option value="17">17</option>
                                                            <option value="18">18</option>
                                                            <option value="19">19</option>
                                                            <option value="20">20</option>
                                                            <option value="21">21</option>
                                                            <option value="22">22</option>
                                                            <option value="23">23</option>
                                                        </select>
                                                        &nbsp;Hrs.
                                                        <select runat="server" id="ddlPkMints_d" name="ddlPkMints_d" class="inputControl">
                                                            <option value="">min</option>
                                                            <option value="00">00</option>
                                                            <option value="15">15</option>
                                                            <option value="30">30</option>
                                                            <option value="45">45</option>
                                                        </select>
                                                        &nbsp;Mints.</td>
                                                </tr>
                                                <tr class="hlinks" align="left">
                                                    <td style="width: 212px">
                                                        Flight No:
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtFlightNo_d" MaxLength="25" size="15" runat="server"></asp:TextBox></td>
                                                </tr>
                                                <%--  <tr class="hlinks" align="left">
                                                    <td align="left" style="width: 209px">
                                                        Address:</td>
                                                    <td>
                                                        <asp:TextBox ID="txtPkPoint_d" MaxLength="50" size="15" runat="server"></asp:TextBox></td>
                                                </tr>--%>
                                            </table>
                                            <!--Code by Jaggu for Toggle---------->
                                            <table width="100%" border="0" id="hideTrain_d" style="display: none" cellspacing="2"
                                                cellpadding="1" bgcolor="#FFFFFF">
                                                <tr class="hlinks" align="left">
                                                    <td align="left" style="height: 24px; width: 212px;">
                                                        Railway Station:
                                                    </td>
                                                    <td style="height: 24px">
                                                        <asp:TextBox ID="txtRlyStationName_d" MaxLength="50" size="15" runat="server"></asp:TextBox></td>
                                                </tr>
                                                <tr class="hlinks" align="left">
                                                    <td align="left" style="height: 24px; width: 212px;">
                                                        Departure Time:
                                                    </td>
                                                    <td style="height: 24px">
                                                        <select runat="server" id="ddlTrainPkHr_d" name="ddlTrainPkHr_d" class="inputControl">
                                                            <option value="">hrs</option>
                                                            <option value="00">00</option>
                                                            <option value="01">01</option>
                                                            <option value="02">02</option>
                                                            <option value="03">03</option>
                                                            <option value="04">04</option>
                                                            <option value="05">05</option>
                                                            <option value="06">06</option>
                                                            <option value="07">07</option>
                                                            <option value="08">08</option>
                                                            <option value="09">09</option>
                                                            <option value="10">10</option>
                                                            <option value="11">11</option>
                                                            <option value="12">12</option>
                                                            <option value="13">13</option>
                                                            <option value="14">14</option>
                                                            <option value="15">15</option>
                                                            <option value="16">16</option>
                                                            <option value="17">17</option>
                                                            <option value="18">18</option>
                                                            <option value="19">19</option>
                                                            <option value="20">20</option>
                                                            <option value="21">21</option>
                                                            <option value="22">22</option>
                                                            <option value="23">23</option>
                                                        </select>
                                                        &nbsp;Hrs.
                                                        <select runat="server" id="ddlTrainPkMin_d" name="ddlTrainPkMin_d" class="inputControl">
                                                            <option value="">min</option>
                                                            <option value="00">00</option>
                                                            <option value="15">15</option>
                                                            <option value="30">30</option>
                                                            <option value="45">45</option>
                                                        </select>
                                                        &nbsp;Mints.</td>
                                                </tr>
                                                <tr class="hlinks" align="left">
                                                    <td style="width: 212px">
                                                        Train No:
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtTrainNo_d" MaxLength="25" size="15" runat="server"></asp:TextBox></td>
                                                </tr>
                                            </table>
                                            <!--Code For Location/Address------>
                                            <table width="100%" border="0" id="hideLocation_d" style="display: none;" cellspacing="2"
                                                cellpadding="1" bgcolor="#FFFFFF">
                                                <tr class="hlinks" align="left">
                                                    <td align="left" style="height: 24px; width: 212px;">
                                                        Address:
                                                    </td>
                                                    <td style="height: 24px">
                                                        <asp:TextBox ID="txtAddr_d" MaxLength="50" size="15" runat="server"></asp:TextBox></td>
                                                </tr>
                                                <tr class="hlinks" align="left">
                                                    <td align="left" style="height: 24px; width: 212px;">
                                                        Street:
                                                    </td>
                                                    <td style="height: 24px">
                                                        <asp:TextBox ID="txtStreet_d" MaxLength="50" size="15" runat="server"></asp:TextBox></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <table cellpadding="1" cellspacing="1" border="0" width="100%" bgcolor="#ffffff">
                                    <tr bgcolor="#FFFFFF">
                                        <td class="verdana11bk" style="height: 0px" bgcolor="#ffffff" align="center">

                                            <script language="javascript" type="text/javascript">
                                <!--
                                  
                                    function placeIt(obj)
                                    {
	                                    obj = document.getElementById(obj);
	                                    if (document.documentElement)
	                                    {
		                                    theLeft = document.documentElement.scrollLeft;
		                                    theTop = document.documentElement.scrollTop;
	                                    }
	                                    else if (document.body)
	                                    {
		                                    theLeft = document.body.scrollLeft;
		                                    theTop = document.body.scrollTop;
	                                    }
	                                    theLeft += x;
	                                    theTop += y;
	                                    obj.style.left = theLeft + 'px' ;
	                                    obj.style.top = theTop + 'px' ;
	                                    setTimeout("placeIt('layer1')",500);
	                                    
                                    }
                                 -->
                                            </script>

                                        </td>
                                    </tr>
                                    <tr bgcolor="#FFFFFF">
                                        <td align="center" colspan="2" class="verdana11bk">
                                            <br />
                                            <asp:CheckBox ID="chkTrue" runat="server" />
                                            I agree to the <a href="#" onclick="javascript:approve();"><b>terms & conditions.</b></a>
                                            <br />
                                            <br />
                                        </td>
                                    </tr>
                                    <tr class="hlinks">
                                        <td align="center" width="100%" style="height: 20px" colspan="2">
                                            <asp:Button ID="Submit" Text="Make Payment" runat="Server" BackColor="#5aa1ea" BorderStyle="None"
                                                CssClass="cgi1" OnClick="Submit_Click" /></td>
                                    </tr>
                                </table>
                            </td>
                            <td width="8" bgcolor="#E7E7E7">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 9px; height: 17px;">
                                <img  loading="lazy" alt="left_d_corner" src="Assets/images/left_d_corner.gif" /></td>
                            <td bgcolor="#e7e7e7" style="height: 17px">
                                <img  loading="lazy" alt="trans" src="Assets/images/trans.gif" /></td>
                            <td align="right" style="height: 17px">
                                <img  loading="lazy" alt="rgt_d_corner" src="Assets/images/rgt_d_corner.gif" /></td>
                        </tr>
                    </table>
                    <tr height="10px" bgcolor="#ffffff">
                        <td colspan="3">
                        </td>
                    </tr>
            <tr style="background-color: #ffffff">
                <td style="text-align: justify" class="intdtext" colspan="3">
                    <uc1:AgentFooter ID="AgentFooter" runat="server" />
                </td>
            </tr>
        </table>
        <input type="Hidden" id="hidPundit" runat="server" value="" />
    </form>
</body>
</html>
