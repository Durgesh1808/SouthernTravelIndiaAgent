<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgentCarbooking.aspx.cs" Inherits="SouthernTravelIndiaAgent.AgentCarbooking" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Agent Car booking</title>

    <script type="text/javascript" language="JavaScript" src="../Assets/js/calendar.js"></script>

    <link href="../Assets/css/style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
    
   
     function Local()   
    {      
         debugger;
        if(document.getElementById("rdolocal").checked==true)
        {
            document.getElementById("tr1").style.display = "block";
              document.getElementById("tr3").style.display = "none";
               document.getElementById("tr5").style.display = "none";
               document.getElementById("ddlTransfer").value="0"
           
        }
      
      
    }
       function subtranshide()   
    {      
   
        if(document.getElementById("ddlTransfer").value=="0")
        {
            document.getElementById("tr6").style.display = "none";
            //alert('no');
             
           
        }
         if(document.getElementById("ddlTransfer").value!="0")
        {
            document.getElementById("tr6").style.display = "block";
           //alert('yes');
             
           
        }
      
      
    }
    
       
    
    function outstation()   
    {      
   
        if(document.getElementById("rdoout").checked==true)
        {
            document.getElementById("tr1").style.display = "none";
             document.getElementById("tr3").style.display = "block";
             document.getElementById("rdofixed").checked=false;
              //document.getElementById("rdotailer").checked=false;
               document.getElementById("tr6").style.display = "none";
           
        }
      
      
    }
    
    
    // function tailer()   
    //{      
   
       // if(document.getElementById("rdotailer").checked==true)
      //  {
           // document.getElementById("tr5").style.display = "block";
            // document.getElementById("rdokm").checked=false;
             // document.getElementById("rdolumpsum").checked=false;
            
           
       // }
      
      
   // }
    
     function fixed()   
    {      
   
        if(document.getElementById("rdofixed").checked==true)
        {
            document.getElementById("tr5").style.display = "none";
            
           
        }
      
      
    }
    
    
     <!--
        function showHide(tripType)
        {
            //alert(tripType);        
            if(tripType=='multicity'){                   
                //document.getElementById('trWithinCity').style.display = "none";
               // document.getElementById('trWithinCity1').style.display = "none";
                document.getElementById('trMultiCityDays').style.display = "block";
                document.getElementById('spCity').innerText = 'Originating City';
            }
            else if(tripType=='cityuse'){
                //document.getElementById('trWithinCity').style.display = "block";
                document.getElementById('trMultiCityDays').style.display = "none";                
                document.getElementById('trMultiCityDays1').style.display = "none";
                document.getElementById('spCity').innerText = 'Select City';
            }
            else if(tripType=='roundtrip'){
                //document.getElementById('trWithinCity').style.display = "none";
                document.getElementById('trMultiCityDays').style.display = "none";                
                document.getElementById('trMultiCityDays1').style.display = "none";
                document.getElementById('spCity').innerText = 'Select City';
            }
            document.getElementById('txtCartTripType').value = tripType;
            ///alert(document.getElementById('txtCartTripType').value);
       }
       
       function validateSubmit()
       {
          //  alert('hi');    
           var tempDropDown = document.getElementById('ddlcity');
           if(tempDropDown.selectedIndex==0)
           {           
                alert('select city');
                tempDropDown.focus();
                return false;
           }
           
			var radtour = document.getElementsByName('rdotype');
			if( (radtour[0].checked==false ) && (radtour[1].checked==false ))
			{
			    alert('Select Tour Local/Out Station.');
			    return false;   
			}
			
			
           tempDropDown = document.getElementById('ddlLocalCityUse');           
           if(tempDropDown.selectedIndex==0)
           {           
                alert('select transfer type');
                tempDropDown.focus();
                return false;
           }
           
           
           return true;   
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
       function fillTransfer(cityId){
            xmlHttp=GetXmlHttpObject()
            var url='../getTransferTypes.aspx?city=';            
            url = url + cityId;
            url = url + "&sid="+Math.random();	
            url = url + "&usertype=Agent";
           //alert(url);
            xmlHttp.onreadystatechange=stateChanged1;
            xmlHttp.open("GET",url,true);
	        xmlHttp.send(null);
	        
	        
	         if (document.getElementById("ddlcity").value=="0")
			        {
			          document.getElementById("rdolocal").checked=false;
			           document.getElementById("rdoout").checked=false;
			            document.getElementById("tr1").style.display = "none";
			            document.getElementById("tr3").style.display = "none";
			            document.getElementById("tr5").style.display = "none";
			             
			        }
			        
       }
       function stateChanged1()  
			{ 
				if( (xmlHttp.readyState == 4 || xmlHttp.readyState=="complete"))
				{                     
                   buildTransfer(xmlHttp.responseText);                   
				}
		}
		function buildTransfer(strValues)
		{
		    //alert(strValues);
		    var transferArr = strValues.split('<br>');
		    var eachTransfer,transferName,transferid;
		    document.getElementById('ddlTransfer').options.length = 0;
		    		    
		    addOption(document.getElementById('ddlTransfer'),'Select','0');
		    for(var i =0 ;i< transferArr.length; i++ )
		    {
		        if(transferArr[i]!=''){
		            eachTransfer  =   transferArr[i].split('#');
		            transferName = eachTransfer[0] ;
		            transferid = eachTransfer[1] ; 
		            addOption(document.getElementById('ddlTransfer'),transferName,transferid);
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
        function IsValid()
        {
            debugger;
	       
	               if (document.getElementById("ddlcity").value=="0")
			        {
				        alert("Please Select a City.");
				        document.getElementById("ddlcity").focus();
				        return false;
			        }
			        var radtour = document.getElementsByName('rdotype');
			        if( (radtour[0].checked==false ) && (radtour[1].checked==false ))
			        {
			            alert('Please select Local / Out Station.');
			            return false;   
			        }
			
			   if( (radtour[0].checked==true ) && (radtour[1].checked==false ))
			        {
			            if (document.getElementById("ddlTransfer").value=="0")
			            {
				            alert("Please select a transfer type.");
				            document.getElementById("ddlTransfer").focus();
				            return false;
			            }
			        }
    			  if( (radtour[0].checked==false ) && (radtour[1].checked==true ))
			        {
			            var radtour1 = document.getElementsByName('rdotour');
			        if( (radtour1[0].checked==false ) && (radtour1[1].checked==false ))
			        {
			            alert('Please select Fixed / Tailer Made Tour.');
			            return false;   
			        }
			        }
    			var radtour1 = document.getElementsByName('rdotype');
    			 if( (radtour1[0].checked==false ) && (radtour1[1].checked==true ))
			        {
			        //alert('hi');
			        var radtour23 = document.getElementsByName('rdotour');
			         if( (radtour23[0].checked==false ) && (radtour23[1].checked==true ))
			        {
			        
			        var radtour2 = document.getElementsByName('rdobooking');
			        if( (radtour2[0].checked==false ) && (radtour2[1].checked==false ))
			        {
			            alert('Please select Per KM / Lumpsum.');
			            return false;   
			       }
			      }
			     }
			    
			    return true;
    	}
	    function setTransfer(l) {
            document.getElementById('hidT').value = l;
        }
         function setTransfersub1(l) {
            document.getElementById('hidTsub1').value = l;
        }
         function setTransfercity2(l) {
            document.getElementById('hidTcity2').value = l;
            
        }
        
        //*********************
         function fillTransfer1(cityId,subtrans){
         //alert(ss);
            //alert(cityId);
            xmlHttp=GetXmlHttpObject()
            var url='../SubgetTransferTypes.aspx?city=';            
            url = url + cityId;
            url=url+ "&subtrans="+subtrans;
            //url
            url = url + "&sid="+Math.random();							
          //alert(url);
            xmlHttp.onreadystatechange=stateChanged2;
            xmlHttp.open("GET",url,true);
	        xmlHttp.send(null);
	       
       }
       function stateChanged2()  
			{ 
				if( (xmlHttp.readyState == 4 || xmlHttp.readyState=="complete"))
				{                     
                   buildTransfer1(xmlHttp.responseText);                   
				}
		}
		function buildTransfer1(strValues1)
        {
            debugger;
		    //alert(strValues);
		    var transferArr1 = strValues1.split('<br>');
		    
		    var eachTransfer1,Subtransfername,RowId;
		    document.getElementById('subddlTransfer').options.length = 0;
		    	    
		    addOption(document.getElementById('subddlTransfer'),'Select','0');
		    //alert(Subtransfername);
		        //alert(transferArr1.length);
		        if(transferArr1.length==1)
		        {
		         document.getElementById("tr6").style.display = "none";
		        }
		         if(transferArr1.length > 1)
		        {
		         document.getElementById("tr6").style.display = "block";
		        }
		        
		    for(var i =0 ;i< transferArr1.length; i++ )
		    {
		        if(transferArr1[i]!=''){
		            eachTransfer1  =   transferArr1[i].split('#');
		            Subtransfername = eachTransfer1[0] ;
		            RowId = eachTransfer1[1] ; 
		            addOption(document.getElementById('subddlTransfer'),Subtransfername,RowId);
		          
                }
                  //document.getElementById("tr6").style.display = "block";
              
		    } 
		   
		    
		}	
		
       
	
     -->
    </script>

    <style type="text/css">
        .style1
        {
            height: 19px;
            width: 82px;
        }
        .style2
        {
            width: 82px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" target="_parent" onsubmit="return IsValid();">
    <div>
        <table width="276" height="429" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td align="left">
                    <img  loading="lazy" alt="left_corner" src="Assets/images/left_corner.gif" width="8" height="8" />
                </td>
                <td bgcolor="#E7E7E7">
                    <img  loading="lazy" alt="trans" src="Assets/images/trans.gif" width="260" height="1" />
                </td>
                <td align="right">
                    <img  loading="lazy" alt="rgt_corner" src="Assets/images/rgt_corner.gif" width="8" height="8" />
                </td>
            </tr>
            <tr>
                <td align="left" bgcolor="#E7E7E7">
                    &nbsp;
                </td>
                <td valign="top">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td width="100%">
                                <img  loading="lazy" alt="car_img" src="Assets/images/car_img.gif" width="260" height="81" border="0" usemap="#MapMap" />
                            </td>
                        </tr>
                        <tr style="visibility: hidden">
                            <td height="50" align="center" class="car">
                                <input name="rbtnCarTripType" type="radio" onclick="showHide(this.value);" value="cityuse"
                                    checked="true" />City Use
                                <input name="rbtnCarTripType" type="radio" onclick="showHide(this.value);" value="multicity" />Multi
                                City
                                <!--<input name="rbtnCarTripType" type="radio" onclick="showHide(this.value);" value="roundtrip" />Roundtrip-->
                            </td>
                        </tr>
                        <tr>
                            <td align="center" valign="top" style="height: 200px">
                                <table width="96%" border="0" cellspacing="0" cellpadding="0">
                                    <tr class="verdana11bk" id="trSelectCity">
                                        <td width="125" align="left" style="height: 19px">
                                            <span id="spCity">Select City</span>
                                        </td>
                                        <td width="17" align="center" style="height: 19px">
                                            :
                                        </td>
                                        <td width="127" align="left" style="height: 19px">
                                            <label>
                                                <asp:DropDownList CssClass="hlinks" ID="ddlcity" runat="server" onchange="javascript:fillTransfer(this.value);setTransfercity2(this.value);"
                                                    OnSelectedIndexChanged="ddlcity_SelectedIndexChanged1">
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr class="verdana11bk" id="trSelectCity1">
                                        <td colspan="3" style="height: 8px">
                                            <img  loading="lazy" alt="trans" src="Assets/images/trans.gif" width="1" height="8" />
                                        </td>
                                    </tr>
                                    <tr class="verdana11bk" id="tr4">
                                        <td align="left" class="style1">
                                            <span id="Span3">Booking Type</span>
                                        </td>
                                        <td width="17" align="center" style="height: 19px">
                                            :
                                        </td>
                                        <td width="127" align="left" style="height: 19px">
                                            <asp:RadioButton ID="rdolocal" runat="server" CssClass="verdana11bk" GroupName="rdotype"
                                                Text="Local" onclick="javascript:return Local();" />
                                            </br>
                                            <asp:RadioButton ID="rdoout" runat="server" CssClass="verdana11bk" GroupName="rdotype"
                                                Text="Out Station" onclick="javascript:return outstation();" />
                                        </td>
                                    </tr>
                                    <tr class="verdana11bk">
                                        <td colspan="3" align="left">
                                            <img  loading="lazy" alt="trans" src="Assets/images/trans.gif" width="1" height="8" />
                                        </td>
                                    </tr>
                                    <tr class="verdana11bk" id="tr1" style="display: none">
                                        <td align="left" class="style1">
                                            <span id="Span1">Transfer Type</span>
                                        </td>
                                        <td width="17" align="center" style="height: 19px">
                                            :
                                        </td>
                                        <td width="127" align="left" style="height: 19px">
                                            <label>
                                                <select id="ddlTransfer" class="hlinks" onchange="javascript:fillTransfer1(document.getElementById('hidTcity2').value,this.value);setTransfer(this.value);subtranshide(this.value)"
                                                    runat="server">
                                                    <option value="0">Select </option>
                                                </select>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr class="verdana11bk" id="tr2">
                                        <td colspan="3" style="height: 8px">
                                            <img  loading="lazy" alt="trans" src="Assets/images/trans.gif" width="1" height="8" />
                                        </td>
                                    </tr>
                                    <tr class="verdana11bk" id="tr6" style="display: none">
                                        <td align="left" class="style1">
                                            <span id="Span5">Sub Transfer Type</span>
                                        </td>
                                        <td width="17" align="center" style="height: 19px">
                                            :
                                        </td>
                                        <td width="127" align="left" style="height: 19px">
                                            <label>
                                                <select id="subddlTransfer" class="hlinks" onchange="setTransfersub1(this.value);"
                                                    runat="server">
                                                    <option value="0">Select </option>
                                                </select>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr class="verdana11bk" id="tr7">
                                        <td colspan="3" style="height: 8px">
                                            <img  loading="lazy" alt="trans" src="Assets/images/trans.gif" width="1" height="8" />
                                        </td>
                                    </tr>
                                    <tr class="verdana11bk" id="tr3" style="display: none">
                                        <td align="left" class="style1">
                                            <span id="Span2">Tour Type</span>
                                        </td>
                                        <td width="17" align="center" style="height: 19px">
                                            :
                                        </td>
                                        <td width="127" align="left" style="height: 19px">
                                            <asp:RadioButton ID="rdofixed" runat="server" CssClass="verdana11bk" GroupName="rdotour"
                                                Text="Fixed Tour" onclick="javascript:return fixed();" />
                                            </br>
                                            <!--  <asp:radiobutton id="rdotailer" runat="server" CssClass="verdana11bk" GroupName="rdotour" Text="Tailer Made" onclick="javascript:return tailer();"/>-->
                                        </td>
                                    </tr>
                                    <tr class="verdana11bk">
                                        <td colspan="3" align="left">
                                            <img  loading="lazy" alt="trans" src="Assets/images/trans.gif" width="1" height="8" />
                                        </td>
                                    </tr>
                                    <tr class="verdana11bk" id="tr5" style="display: none">
                                        <td align="left" class="style1">
                                            <span id="Span4">Booking Type</span>
                                        </td>
                                        <td width="17" align="center" style="height: 19px">
                                            :
                                        </td>
                                        <td width="127" align="left" style="height: 19px">
                                            <asp:RadioButton ID="rdokm" runat="server" CssClass="verdana11bk" GroupName="rdobooking"
                                                Text="Per KM" />
                                            </br>
                                            <asp:RadioButton ID="rdolumpsum" runat="server" CssClass="verdana11bk" GroupName="rdobooking"
                                                Text="Lumpsum" />
                                        </td>
                                    </tr>
                                    <tr class="verdana11bk">
                                        <td colspan="3" align="left">
                                            <img  loading="lazy" alt="trans" src="Assets/images/trans.gif" width="1" height="8" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" align="center">
                                            <asp:ImageButton ID="btnSubmit" runat="server" ImageUrl="Assets/images/car_submit.gif" Width="67"
                                                Height="20" OnClick="btnSubmit_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td height="99" valign="top" background="Assets/images/fadecar.gif">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td height="40" align="center" valign="bottom">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
                <td align="right" bgcolor="#E7E7E7">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="left">
                    <img  loading="lazy" alt="left_d_corner" src="Assets/images/left_d_corner.gif" width="8" height="8" />
                </td>
                <td bgcolor="#E7E7E7">
                    <img  loading="lazy" alt="trans" src="Assets/images/trans.gif" width="1" height="1" />
                </td>
                <td align="right">
                    <img  loading="lazy" alt="rgt_d_corner" src="Assets/images/rgt_d_corner.gif" width="8" height="8" />
                </td>
            </tr>
        </table>
        <map name="MapMap" id="MapMap">
            <area shape="circle" coords="236,53,27" href="Agentspecial.aspx" title="Book Your Special Package" />
            <area shape="circle" coords="108,53,27" href="Agenthotels_iframe.aspx" title="Book Your Hotel" />
            <area shape="circle" coords="45,52,27" href="Agenttour.aspx" title="Build your Package" />
        </map>
    </div>
    <input type="hidden" id="isAgent" value="true" />
    <input type="hidden" id="txtCartTripType" value="" runat="server" />
    <input type="hidden" id="hidT" runat="server" />
    <input type="hidden" id="hidTsub1" runat="server" />
    <input type="hidden" id="hidTcity2" runat="server" />
    </form>

    <script type="text/javascript">
    <!--
        //showHide('cityuse');   
    -->
    </script>

</body>
</html>
