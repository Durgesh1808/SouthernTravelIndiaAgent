var currentOptedSeats = 0;

function checkUnheck(ctrlId, divSelected) {
    var chkId = document.getElementById(ctrlId);
    if (chkId.checked) {
        chkId.checked = false;
    }
    else {
        chkId.checked = true;
    }

    alterValue(chkId, divSelected);
}

function alterValue(chkBoxObject, divSelected) {
    if (chkBoxObject.checked) {
        if (currentOptedSeats >= parseInt(document.getElementById('maxSeatAllowed').value)) {
            alert('You have selected more than ' + parseInt(document.getElementById('maxSeatAllowed').value) + ' seat(s)');
            chkBoxObject.checked = false;
            return;
        }

        addValues(chkBoxObject.value);
        divSelected.className = "TB_selctd";
    }
    else {
        removeValues(chkBoxObject.value);
        divSelected.className = "TB_avbl";
    }
}
       function addValues(chkValue)
       {
           currentOptedSeats = currentOptedSeats  + 1;
           var optedStr = document.getElementById('optedSeatNos').value;
           optedStr = optedStr.replace(",,",",");
           document.getElementById('optedSeatNos').value = optedStr + ',' + chkValue;           
           
       }
       function checkseats()
       {
                var chek = true;	
                if((currentOptedSeats)!= parseInt(document.getElementById('maxSeatAllowed').value))
                {                
                    Swal.fire({
                        icon: 'warning',
                        title: 'Oops...',
                        text: 'Please Select The Correct Number Of seats.',
                        confirmButtonColor: '#f2572b'
                    });
                    chek = false;
                }
                 if(chek)
                  {
		            document.getElementById('btnContinuee').style.display='none';	     		          
		          }
		    return chek;
       }
       function removeValues(chkValue)
       {
            currentOptedSeats = currentOptedSeats  - 1;
            var optedStr = document.getElementById('optedSeatNos').value;
            optedStr = optedStr.replace(chkValue,"");
            optedStr = optedStr.replace(",,",",");                                
            document.getElementById('optedSeatNos').value = optedStr;
       }
       function resetColor(tdObj)
       {      
         var idNo = tdObj.id.substring(3,5);
         var cc=tdObj.id.substring(2,3);         
         if(document.getElementById(cc+'chk'+idNo).checked==true)
         {         
            tdObj.bgColor = '#5aa1ea';
         }
         else
         {
            tdObj.bgColor = '#9bc7f4';         
         }
       }  
       	function checkOnsubmit()
		{
		    debugger;
			if (document.getElementById("ddlJdate").selectedIndex==0)
			{
				alert("Please select a date");
				document.getElementById("ddlJdate").focus();
				return false
			}
			if(document.getElementById("tra2f"))
		    {
			    if(document.getElementById("txtNoOfAdultsTwin").value!=0)
			    {
			       var lCurrPax = document.getElementById("txtNoOfAdultsTwin").value;
                    if (lCurrPax % 2 != 0)
                    {
                        alert('Enter Multiples of 2 Only');
                        return false;
                    }			
			    }			   
			}
			if(document.getElementById("tra3f"))
			{
			    if(document.getElementById("txtNoOfAdultsTriple").value!=0)
			    {
			       var lCurrPax = document.getElementById("txtNoOfAdultsTriple").value;
                    if (lCurrPax % 3 != 0)
                    {
                        alert('Enter Multiples of 3 Only');
                        return false;
                    }		
			    }			   
			}				
			if (chkavaild()==false)
			{
				return false;
			}
			
			if (ChekNumOnly()== false)
			{
				return false;
			}	
			if(checkradio()==false)
			{
			    return false;
			}	
		}
		
		function checkradio()
		{
		var tot=0;
		var adult=0;
		var child=0;
		    if (document.getElementById('RadAC') || document.getElementById('RadNAC'))
			{
			    if (!(document.getElementById('RadAC').checked) && !(document.getElementById('RadNAC').checked))
				{
					alert("Please choose any AC/Non-AC first");
					return false
				}
								
			}
			if(document.getElementById("traf"))
			{		
		        if (document.getElementById("txtNoOfAdults").value!=0)
		        {
		            tot=tot+eval(document.getElementById("txtNoOfAdults").value);
		            adult=adult+eval(document.getElementById("txtNoOfAdults").value);			    
		        }		        
		    }
		    if(document.getElementById("trcf"))
		    {
			    if(document.getElementById("txtNoOfChilds").value!=0)
			    {
			        tot=tot+eval(document.getElementById("txtNoOfChilds").value);
			        child=child+eval(document.getElementById("txtNoOfChilds").value);
			    }
			}
			if (document.getElementById("trAWF")) {
			    if (document.getElementById("txtNoAWFNoOfAdults").value != 0) {
			        tot = tot + eval(document.getElementById("txtNoAWFNoOfAdults").value);
			        adult = adult + eval(document.getElementById("txtNoAWFNoOfAdults").value);
			    }
			}
			if (document.getElementById("trCWF")) {
			    if (document.getElementById("txtNoCWFNoOfChilds").value != 0) {
			        tot = tot + eval(document.getElementById("txtNoCWFNoOfChilds").value);
			        child = child + eval(document.getElementById("txtNoCWFNoOfChilds").value);
			    }
			}
		    if(document.getElementById("tra2f"))
		    {
			    if(document.getElementById("txtNoOfAdultsTwin").value!=0)
			    {
			        tot=tot+eval(document.getElementById("txtNoOfAdultsTwin").value);
			        adult=adult+eval(document.getElementById("txtNoOfAdultsTwin").value);					
			    }			   
			}
			if(document.getElementById("tra3f"))
			{
			    if(document.getElementById("txtNoOfAdultsTriple").value!=0)
			    {
			        tot=tot+eval(document.getElementById("txtNoOfAdultsTriple").value);	
			         adult=adult+eval(document.getElementById("txtNoOfAdultsTriple").value);		
			    }			   
			}
			if(document.getElementById("trcbf"))
			{
		        if(document.getElementById("txtNoOfChildBed").value!=0)
		        {
		            tot=tot+eval(document.getElementById("txtNoOfChildBed").value);
		            child=child+eval(document.getElementById("txtNoOfChildBed").value);			
		        }		        
		    }
		    if(document.getElementById("trsf"))
		    {
			    if(document.getElementById("txtNoOfSingles").value!=0)
			    {
			        tot=tot+eval(document.getElementById("txtNoOfSingles").value);
			        adult=adult+eval(document.getElementById("txtNoOfSingles").value);				
			    }			    
			}	
			if(document.getElementById("tradf"))
		    {
			    if(document.getElementById("txtNoofdormitory").value!=0)
			    {
			        tot=tot+eval(document.getElementById("txtNoofdormitory").value);
			        adult=adult+eval(document.getElementById("txtNoofdormitory").value);				
			    }			    
			}	
			if(tot==0)
			{	
                Swal.fire({
                    icon: 'warning',
                    title: 'Oops...',
                    text: 'Please Enter Number of Passengers.',
                    confirmButtonColor: '#f2572b'
                });	
			return false;
			}
			if(adult==0)
			{
			alert("Please Enter At Least 1 Adult")
			return false;
			}	
			
			if(parseInt(document.getElementById('hdNoofDays').value)>14)
			{
		       var value4 = CheckPayment(document.getElementById('rbtnPaymentOption'), 'HALF');
		       if (value4 == false) {
                   Swal.fire({
                       icon: 'warning',
                       title: 'Oops...',
                       text: 'Please select Payment option.',
                       confirmButtonColor: '#f2572b'
                   });
                return false;
               }
			}
			var prevChild = 0;
			if(document.getElementById('prevchi').value.length>0)
			    prevChild = parseInt(document.getElementById('prevchi').value);			 
			var prevAdult = 0;
			if(document.getElementById('prevadu').value.length>0)
			    prevAdult = parseInt(document.getElementById('prevadu').value);			
			if((prevAdult>0 ) || (prevChild>0))
			{			   
			    if((adult+child)!=(prevAdult+prevChild))	
			    {			     
			        alert("Please Enter the Correct No of Passengers")
			        return false;
			    }
			     if((adult)!=(prevAdult))
			    {
                    alert("Please Enter the Correct No of Adults")
			        return false;
			    }
			    if((child)!=(prevChild))
			    {
                    alert("Please Enter the Correct No of Childs")
			        return false;
			    }
			}
			
		}
		function CheckPayment(objField) {

        var radioBoxList1 = objField;
        var radioBoxes = radioBoxList1.getElementsByTagName("input");
        var isvalid = false;
        for (var i = 0; i < radioBoxes.length; i++) {

            if (radioBoxes[i].checked) {
                    isvalid=true;
                    return true;
                    break;
                }
            

        }
        if (isvalid == false) {
            return false;
        }

    }
		function ChekNumOnly()
		{
		   	if(document.getElementById("traf"))
			  {	
                if (Trim(document.getElementById('txtNoOfAdults').value) != null)
                    {
	                    if (validateOnlyNumber1(document.getElementById('txtNoOfAdults').value) == false)
	                    {	
		                    alert("No. of persons must be a numeric value")
		                    document.getElementById('txtNoOfAdults').value=0;
		                    document.getElementById('txtNoOfAdults').focus();
		                    return false;
	                    }
                    }
		   }
		   if(document.getElementById("trcf"))
		   {
			    if (Trim(document.getElementById('txtNoOfChilds').value) != null)
			    {
				    if (validateOnlyNumber1(document.getElementById('txtNoOfChilds').value) == false)
				    {	
					    alert("No. of persons must be a numeric value")
					    document.getElementById('txtNoOfChilds').value=0;
					    document.getElementById('txtNoOfChilds').focus();
					    return false;
				    }
			    }
			}
			if (document.getElementById("trAWF")) {
			    if (Trim(document.getElementById('txtNoAWFNoOfAdults').value) != null) {
			        if (validateOnlyNumber1(document.getElementById('txtNoAWFNoOfAdults').value) == false) {
			            alert("No. of persons must be a numeric value")
			            document.getElementById('txtNoAWFNoOfAdults').value = 0;
			            document.getElementById('txtNoAWFNoOfAdults').focus();
			            return false;
			        }
			    }
			}
			if (document.getElementById("trCWF")) {
			    if (Trim(document.getElementById('txtNoCWFNoOfChilds').value) != null) {
			        if (validateOnlyNumber1(document.getElementById('txtNoCWFNoOfChilds').value) == false) {
			            alert("No. of persons must be a numeric value")
			            document.getElementById('txtNoCWFNoOfChilds').value = 0;
			            document.getElementById('txtNoCWFNoOfChilds').focus();
			            return false;
			        }
			    }
			}
		    if(document.getElementById("tra2f"))
             {   
			    if (Trim(document.getElementById('txtNoOfAdultsTwin').value) != null)
			    {
				    if (validateOnlyNumber1(document.getElementById('txtNoOfAdultsTwin').value) == false)
				    {	
					    alert("No. of persons must be a numeric value")
					    document.getElementById('txtNoOfAdultsTwin').value=0;
					    document.getElementById('txtNoOfAdultsTwin').focus();
					    return false;
				    }
			    }
			 }
		    if(document.getElementById("tra3f"))
             {
			    if (Trim(document.getElementById('txtNoOfAdultsTriple').value) != null)
			    {
				    if (validateOnlyNumber1(document.getElementById('txtNoOfAdultsTriple').value) == false)
				    {	
					    alert("No. of persons must be a numeric value")
					    document.getElementById('txtNoOfAdultsTriple').value=0;
					    document.getElementById('txtNoOfAdultsTriple').focus();
					    return false;
				    }
			    }
			  }
	     if(document.getElementById("trcbf"))
		    {
			    if (Trim(document.getElementById('txtNoOfChildBed').value) != null)
			    {
				    if (validateOnlyNumber1(document.getElementById('txtNoOfChildBed').value) == false)
				    {	
					    alert("No. of persons must be a numeric value")
					    document.getElementById('txtNoOfChildBed').value=0;
					    document.getElementById('txtNoOfChildBed').focus();
					    return false;
				    }
			    }
			 }
	    if(document.getElementById("trsf"))
		    {
		        if (Trim(document.getElementById('txtNoOfSingles').value) != null)
		        {
			        if (validateOnlyNumber1(document.getElementById('txtNoOfSingles').value) == false)
			        {	
				        alert("No. of persons must be a numeric value")
				        document.getElementById('txtNoOfSingles').value=0;
				        document.getElementById('txtNoOfSingles').focus();
				        return false;
			        }
		        }
			}
			
			if(document.getElementById("tradf"))
		    {
		        if (Trim(document.getElementById('txtNoofdormitory').value) != null)
		        {
			        if (validateOnlyNumber1(document.getElementById('txtNoofdormitory').value) == false)
			        {	
				        alert("No. of persons must be a numeric value")
				        document.getElementById('txtNoofdormitory').value=0;
				        document.getElementById('txtNoofdormitory').focus();
				        return false;
			        }
		        }
			}
			
			
		}
		
		function chkavaild()
		{
	
//			var dat;
//			dat=document.getElementById("ddlJdate").value;
//			if (document.getElementById(dat))
//			{
//				alert("You have allready booked tour on this date so please choose another date.");		        
//				return false;
//			}
		}
		function validtwin()
		{
		    if(document.getElementById("txtNoOfAdultsTwin").value>=0)
		    {
		     if ((document.getElementById("txtNoOfAdultsTwin").value % 2)!=0)
		     {
		     alert("Enter Multiples of 2 Only");
		     document.getElementById('txtNoOfAdultsTwin').value=0;
		     document.getElementById("txtNoOfAdultsTwin").focus();
		     }
		     else
		     {
		      document.getElementById("lblTotal").firstChild.data='0';
		        Displayfare();
		     }
		     
		    }
		}
		
		function validtriple()
		{
		    if(document.getElementById("txtNoOfAdultsTriple").value>=0)
		    {
		     if ((document.getElementById("txtNoOfAdultsTriple").value % 3)!=0)
		     {
		     alert("Enter Multiples of 3 Only");
		     document.getElementById('txtNoOfAdultsTriple').value=0;
		     document.getElementById("txtNoOfAdultsTriple").focus();
		     }
		     else
		     {
		        document.getElementById("lblTotal").firstChild.data='0';
		        Displayfare();		       
		     }
		     
		    }
		}
		function chkfield()
		{		
	        var tot=document.getElementById('lblTotal').firstChild.data ;
            if(isNaN(tot))
            {
                var allTxt = document.getElementsByTagName('input');
                for(var z=0;z<allTxt.length;z++)
                {
                    if(allTxt[z].name.substring(0,5)=="txtNo")
                    {
                       if( Trim(allTxt[z].value) =="")
                       {
                            allTxt[z].value = "0";      
                       }   
                    }   
                }
                document.getElementById('lblTotal').firstChild.data = "0";
                Displayfare();
                return;                
            } 
		}
		function Displayfare()
		{				
		var tot=0;	
		var pax=0;
		var Adultpax=0;
		var Childpax=0;
		//document.getElementById('maxSeatAllowed').value=+parseInt(document.getElementById("txtNoOfChilds").value)+parseInt(document.getElementById("txtNoOfAdultsTwin").value)+parseInt(document.getElementById("txtNoOfAdultsTriple").value)+parseInt(document.getElementById("txtNoOfChildBed").value)+parseInt(document.getElementById("txtNoOfSingles").value)	
			if (ChekNumOnly()== false)
			{
		    	return false;
			}
			if(isNaN(document.getElementById("stax").value))
			{			
                document.getElementById("stax").value="0";
			}
			document.getElementById("lblTotal").firstChild.data='0';			
			if(document.getElementById('RadAC').checked)
			{
			    document.getElementById("lblTotal").firstChild.data='0';			    	
			            if(document.getElementById("traf"))
			            {
			                var a = document.getElementById("lblAACfare").firstChild.data;    		
			                if((!isNaN(parseInt(a))) &&(parseInt(a)>0))
			                {
			                    document.getElementById("traf").style.display.block;
    				            document.getElementById("lblFareAdults").firstChild.data=parseInt(document.getElementById("lblAACfare").firstChild.data); 
    				            document.getElementById("lblCalcAdults").firstChild.data=(parseInt(document.getElementById("lblAACfare").firstChild.data) * eval(document.getElementById("txtNoOfAdults").value));
    				            document.getElementById("lblTotal").firstChild.data=parseInt(document.getElementById("lblCalcAdults").firstChild.data);    				       
    				            var prevSt =  document.getElementById("stax").value;    				       
    				            if(!isNaN(prevSt)) 
    				            {
    				                prevSt = 0;
    				            }
    				            document.getElementById("stax").value = parseFloat(prevSt) + parseFloat((eval(document.getElementById("lblTotal").firstChild.data)*eval(document.getElementById("service").value))/100);
    				            if(parseFloat(document.getElementById("stax").value)>0)
    				            {   				       
    				            tot=parseFloat(document.getElementById("lblTotal").firstChild.data) + parseFloat(document.getElementById("stax").value);
    				            }
    				                var se = document.getElementById("sess").value;    				         				        
    				                if(parseInt(se)>0)
    				                {
    				                    if(!isNaN(document.getElementById("cc").value))
    				                    {
    				                    document.getElementById("cc").value=0;
    				                    }    				       		        
    				                    document.getElementById("cc").value= parseFloat(document.getElementById("cc").value)+ parseFloat((eval(tot)*eval(document.getElementById("credit").value))/100);
    				                }    				       
    				                document.getElementById('Labeltax').innerHTML = 'GST of Rs:'+ Math.round(document.getElementById("stax").value) +' Extra'     
            				  pax=pax+parseInt(document.getElementById("txtNoOfAdults").value); 
            				  Adultpax=Adultpax+parseInt(document.getElementById("txtNoOfAdults").value);   
			                }
			                 else
	                        {
	                            document.getElementById("traf").style.display.none;
	                            document.getElementById("txtNoOfAdults").value=0;
	                        }
    		            }   
    		   
    				    if(document.getElementById("trcf"))
    				    {
    				         var b = document.getElementById("lblCACfare").firstChild.data;    		
			                if((!isNaN(parseInt(b))) &&(parseInt(b)>0))
			                {
			                document.getElementById("trcf").style.display.block;
    				        document.getElementById("lblfareChild").firstChild.data=parseInt(document.getElementById("lblCACfare").firstChild.data) ;
    				        document.getElementById("lblCalcChild").firstChild.data=(parseInt(document.getElementById("lblCACfare").firstChild.data) * eval(document.getElementById("txtNoOfChilds").value));
    				        document.getElementById("lblTotal").firstChild.data= parseInt(document.getElementById("lblTotal").firstChild.data) +  parseInt(document.getElementById("lblCalcChild").firstChild.data);
    				        var prevSt =  document.getElementById("stax").value;
    				        if(!isNaN(prevSt)) 
    				        {
    				            prevSt = 0;
    				        }
    				        document.getElementById("stax").value = parseFloat(prevSt) + parseFloat((eval(document.getElementById("lblTotal").firstChild.data)*eval(document.getElementById("service").value))/100);
    				           				       
    				        if(parseFloat(document.getElementById("stax").value)>0)
    				        {   				       
    				        tot=parseFloat(document.getElementById("lblTotal").firstChild.data) + parseFloat(document.getElementById("stax").value);
    				        }
    				        var se=document.getElementById("sess").value;    				        
    				        if(parseInt(se)>0)
    				        {   
    				         if(!isNaN(document.getElementById("cc").value))
    				        {
    				        document.getElementById("cc").value=0;
    				        } 				        
    				        document.getElementById("cc").value= parseFloat(document.getElementById("cc").value)+ parseFloat((eval(tot)*eval(document.getElementById("credit").value))/100);
    				        }
    				        document.getElementById('Labeltax').innerHTML = 'GST of Rs:'+ Math.round(document.getElementById("stax").value) +' Extra'   
                            pax=pax+parseInt(document.getElementById("txtNoOfChilds").value);
                            Childpax=Childpax+parseInt(document.getElementById("txtNoOfChilds").value);   
    				        }
    				        else
    		                {
    		                    document.getElementById("trcf").style.display.none;
    		                    document.getElementById("txtNoOfChilds").value=0;
    		                }
    		            }
    		            if (document.getElementById("trAWF")) {
    		                debugger;
    		                var a = document.getElementById("lblAWFfare").firstChild.data;

    		                if ((!isNaN(parseInt(a))) && (parseInt(a) > 0)) {
    		                    var prevSt = document.getElementById("stax").value;
    		                    document.getElementById("trAWF").style.display.block;
    		                    document.getElementById("lblAWFFareAdults").firstChild.data = parseInt(document.getElementById("lblAWFfare").firstChild.data);
    		                    document.getElementById("lblCalcAWF").firstChild.data = (parseInt(document.getElementById("lblAWFfare").firstChild.data) * eval(document.getElementById("txtNoAWFNoOfAdults").value));
    		                    document.getElementById("lblTotal").firstChild.data = parseInt(document.getElementById("lblTotal").firstChild.data) + parseInt(document.getElementById("lblCalcAWF").firstChild.data);

    		                    if (!isNaN(prevSt)) {
    		                        prevSt = 0;
    		                    }
    		                    document.getElementById("stax").value = parseFloat(prevSt) + parseFloat((eval(document.getElementById("lblTotal").firstChild.data) * eval(document.getElementById("service").value)) / 100);
    		                    if (parseFloat(document.getElementById("stax").value) > 0) {
    		                        tot = parseFloat(document.getElementById("lblTotal").firstChild.data) + parseFloat(document.getElementById("stax").value);
    		                    }
    		                    var se = document.getElementById("sess").value;
    		                    if (parseInt(se) > 0) {
    		                        if (!isNaN(document.getElementById("cc").value)) {
    		                            document.getElementById("cc").value = 0;
    		                        }
    		                        document.getElementById("cc").value = parseFloat(document.getElementById("cc").value) + parseFloat((eval(tot) * eval(document.getElementById("credit").value)) / 100);
    		                    }
    		                    // and Convenience Charges of Rs:'+ roundNumber(document.getElementById("cc").value) +' are     				       


    		                    document.getElementById('Labeltax').innerHTML = 'GST of Rs:' + roundNumber(document.getElementById("stax").value);
    		                    pax = pax + parseInt(document.getElementById("txtNoAWFNoOfAdults").value);
    		                    Adultpax=Adultpax+parseInt(document.getElementById("txtNoAWFNoOfAdults").value);
    		                }
    		                else {
    		                    document.getElementById("trAWF").style.display.none;
    		                    document.getElementById("txtNoAWFNoOfAdults").value = 0;
    		                }
    		            }

    		            if (document.getElementById("trCWF")) {
    		                var b = document.getElementById("lblCWFfare").firstChild.data;
    		                if ((!isNaN(parseInt(b))) && (parseInt(b) > 0)) {
    		                    document.getElementById("trCWF").style.display.block;
    		                    document.getElementById("lblCWFfareChild").firstChild.data = parseInt(document.getElementById("lblCWFfare").firstChild.data);
    		                    document.getElementById("lblCWFCalcChild").firstChild.data = (parseInt(document.getElementById("lblCWFfare").firstChild.data) * eval(document.getElementById("txtNoCWFNoOfChilds").value));
    		                    document.getElementById("lblTotal").firstChild.data = parseInt(document.getElementById("lblTotal").firstChild.data) + parseInt(document.getElementById("lblCWFCalcChild").firstChild.data);
    		                    var prevSt = document.getElementById("stax").value;
    		                    if (!isNaN(prevSt)) {
    		                        prevSt = 0;
    		                    }
    		                    document.getElementById("stax").value = parseFloat(prevSt) + parseFloat((eval(document.getElementById("lblTotal").firstChild.data) * eval(document.getElementById("service").value)) / 100);

    		                    if (parseFloat(document.getElementById("stax").value) > 0) {
    		                        tot = parseFloat(document.getElementById("lblTotal").firstChild.data) + parseFloat(document.getElementById("stax").value);
    		                    }
    		                    var se = document.getElementById("sess").value;
    		                    if (parseInt(se) > 0) {
    		                        if (!isNaN(document.getElementById("cc").value)) {
    		                            document.getElementById("cc").value = 0;
    		                        }
    		                        document.getElementById("cc").value = parseFloat(document.getElementById("cc").value) + parseFloat((eval(tot) * eval(document.getElementById("credit").value)) / 100);
    		                    }
    		                    document.getElementById('Labeltax').innerHTML = 'GST of Rs:' + roundNumber(document.getElementById("stax").value);
    		                    pax = pax + parseInt(document.getElementById("txtNoCWFNoOfChilds").value);
    		                    Adultpax=Adultpax+parseInt(document.getElementById("txtNoCWFNoOfChilds").value);
    		                }
    		                else {
    		                    document.getElementById("trCWF").style.display.none;
    		                    document.getElementById("txtNoCWFNoOfChilds").value = 0;
    		                }
    		            } 
                        if(document.getElementById("tra2f"))
                        { 
                            var c = document.getElementById("lblA2ACfare").firstChild.data;    		
			                if((!isNaN(parseInt(c))) &&(parseInt(c)>0))
			                { 
			                document.getElementById("tra2f").style.display.block;      
				            document.getElementById("lblFareAdultsTwin").firstChild.data = parseInt(document.getElementById("lblA2ACfare").firstChild.data);
				            document.getElementById("lblCalcAdultsTwin").firstChild.data=(parseInt(document.getElementById("lblA2ACfare").firstChild.data) * eval(document.getElementById("txtNoOfAdultsTwin").value));
				            document.getElementById("lblTotal").firstChild.data= parseInt(document.getElementById("lblTotal").firstChild.data) +  parseInt(document.getElementById("lblCalcAdultsTwin").firstChild.data);
				           var prevSt =  document.getElementById("stax").value;
    				        if(!isNaN(prevSt)) 
    				        {
    				            prevSt = 0;
    				        }
    				        document.getElementById("stax").value = parseFloat(prevSt) + parseFloat((eval(document.getElementById("lblTotal").firstChild.data)*eval(document.getElementById("service").value))/100);
    				           				       
    				        if(parseFloat(document.getElementById("stax").value)>0)
    				        {   				       
    				        tot=parseFloat(document.getElementById("lblTotal").firstChild.data) + parseFloat(document.getElementById("stax").value);
    				        }
    				        var se=document.getElementById("sess").value;    				        
    				        if(parseInt(se)>0)
    				        {
    				         if(!isNaN(document.getElementById("cc").value))
    				        {
    				        document.getElementById("cc").value=0;
    				        }    				        
    				        document.getElementById("cc").value= parseFloat(document.getElementById("cc").value)+ parseFloat((eval(tot)*eval(document.getElementById("credit").value))/100);
    				        }
    				        document.getElementById('Labeltax').innerHTML = 'GST of Rs:'+ Math.round(document.getElementById("stax").value) +' Extra'   
                            pax=pax+parseInt(document.getElementById("txtNoOfAdultsTwin").value);
                            Adultpax=Adultpax+parseInt(document.getElementById("txtNoOfAdultsTwin").value);
				            }
				            else
    		                {
    		                    document.getElementById("tra2f").style.display.none;
    		                    document.getElementById("txtNoOfAdultsTwin").value=0;
    		                }
			             }
			             
				        if(document.getElementById("tra3f"))
				        {
				            var d = document.getElementById("lblA3ACfare").firstChild.data;    		
			                if((!isNaN(parseInt(d))) &&(parseInt(d)>0))
			                {
			                document.getElementById("tra3f").style.display.block;
				            document.getElementById("lblFareAdultsTriple").firstChild.data = parseInt(document.getElementById("lblA3ACfare").firstChild.data);
				            document.getElementById("lblCalcAdultsTriple").firstChild.data=(parseInt(document.getElementById("lblA3ACfare").firstChild.data) * eval(document.getElementById("txtNoOfAdultsTriple").value));
				            document.getElementById("lblTotal").firstChild.data= parseInt(document.getElementById("lblTotal").firstChild.data) +  parseInt(document.getElementById("lblCalcAdultsTriple").firstChild.data);
				            var prevSt =  document.getElementById("stax").value;
    				        if(!isNaN(prevSt)) 
    				        {
    				            prevSt = 0;
    				        }
    				        document.getElementById("stax").value = parseFloat(prevSt) + parseFloat((eval(document.getElementById("lblTotal").firstChild.data)*eval(document.getElementById("service").value))/100);
    				           				       
    				        if(parseFloat(document.getElementById("stax").value)>0)
    				        {   				       
    				        tot=parseFloat(document.getElementById("lblTotal").firstChild.data) + parseFloat(document.getElementById("stax").value);
    				        }
    				        var se=document.getElementById("sess").value;    				        
    				        if(parseInt(se)>0)
    				        { 
    				         if(!isNaN(document.getElementById("cc").value))
    				        {
    				        document.getElementById("cc").value=0;
    				        }   				        
    				        document.getElementById("cc").value= parseFloat(document.getElementById("cc").value)+ parseFloat((eval(tot)*eval(document.getElementById("credit").value))/100);
    				        }
    				        document.getElementById('Labeltax').innerHTML = 'GST of Rs:'+ Math.round(document.getElementById("stax").value) +' Extra'   
                            pax=pax+parseInt(document.getElementById("txtNoOfAdultsTriple").value);
                            Adultpax=Adultpax+parseInt(document.getElementById("txtNoOfAdultsTriple").value);
				            }
				            else
    		                {
    		                    document.getElementById("tra3f").style.display.none;
    		                    document.getElementById("txtNoOfAdultsTriple").value=0;
    		                }
		                }
    		    
    		    
				        if(document.getElementById("trcbf"))
				        {
				            var e = document.getElementById("lblCBACfare").firstChild.data;    		
			                if((!isNaN(parseInt(e))) &&(parseInt(e)>0))
			                {
			                document.getElementById("trcbf").style.display.block;
                            document.getElementById("lblFareChildBed").firstChild.data = parseInt(document.getElementById("lblCBACfare").firstChild.data);
				            document.getElementById("lblCalcChildBed").firstChild.data=(parseInt(document.getElementById("lblCBACfare").firstChild.data) * eval(document.getElementById("txtNoOfChildBed").value));
				            document.getElementById("lblTotal").firstChild.data= parseInt(document.getElementById("lblTotal").firstChild.data) +  parseInt(document.getElementById("lblCalcChildBed").firstChild.data);
				            var prevSt =  document.getElementById("stax").value;
    				        if(!isNaN(prevSt)) 
    				        {
    				            prevSt = 0;
    				        }
    				        document.getElementById("stax").value = parseFloat(prevSt) + parseFloat((eval(document.getElementById("lblTotal").firstChild.data)*eval(document.getElementById("service").value))/100);
    				           				       
    				        if(parseFloat(document.getElementById("stax").value)>0)
    				        {   				       
    				        tot=parseFloat(document.getElementById("lblTotal").firstChild.data) + parseFloat(document.getElementById("stax").value);
    				        }
    				        var se=document.getElementById("sess").value;    				        
    				        if(parseInt(se)>0)
    				        {
    				         if(!isNaN(document.getElementById("cc").value))
    				        {
    				        document.getElementById("cc").value=0;
    				        }    				        
    				        document.getElementById("cc").value= parseFloat(document.getElementById("cc").value)+ parseFloat((eval(tot)*eval(document.getElementById("credit").value))/100);
    				        }
    				        document.getElementById('Labeltax').innerHTML = 'GST of Rs:'+ Math.round(document.getElementById("stax").value) +' Extra'   
                            pax=pax+parseInt(document.getElementById("txtNoOfChildBed").value);
                            Childpax=Childpax+parseInt(document.getElementById("txtNoOfChildBed").value);
				            }
				            else
    		                {
    		                    document.getElementById("trcbf").style.display.none;
    		                    document.getElementById("txtNoOfChildBed").value=0;
    		                }
		                }    		    
    		    
				        if(document.getElementById("trsf"))
				        {
				            var f = document.getElementById("lblSACfare").firstChild.data;    		
			                if((!isNaN(parseInt(f))) &&(parseInt(f)>0))
			                {
			                 document.getElementById("trsf").style.display.block;
                            document.getElementById("lblFareSingles").firstChild.data = parseInt(document.getElementById("lblSACfare").firstChild.data);
				            document.getElementById("lblCalcSingles").firstChild.data=(parseInt(document.getElementById("lblSACfare").firstChild.data) * eval(document.getElementById("txtNoOfSingles").value));
				            document.getElementById("lblTotal").firstChild.data= parseInt(document.getElementById("lblTotal").firstChild.data) +  parseInt(document.getElementById("lblCalcSingles").firstChild.data);
                            var prevSt =  document.getElementById("stax").value;
    				        if(!isNaN(prevSt)) 
    				        {
    				            prevSt = 0;
    				        }
    				        document.getElementById("stax").value = parseFloat(prevSt) + parseFloat((eval(document.getElementById("lblTotal").firstChild.data)*eval(document.getElementById("service").value))/100);
    				           				       
    				        if(parseFloat(document.getElementById("stax").value)>0)
    				        {   				       
    				        tot=parseFloat(document.getElementById("lblTotal").firstChild.data) + parseFloat(document.getElementById("stax").value);
    				        }
    				        var se=document.getElementById("sess").value;    				        
    				        if(parseInt(se)>0)
    				        { 
    				         if(!isNaN(document.getElementById("cc").value))
    				        {
    				        document.getElementById("cc").value=0;
    				        }   				        
    				        document.getElementById("cc").value= parseFloat(document.getElementById("cc").value)+ parseFloat((eval(tot)*eval(document.getElementById("credit").value))/100);
    				        }
    				        document.getElementById('Labeltax').innerHTML = 'GST of Rs:'+ Math.round(document.getElementById("stax").value) +' Extra'   
                            pax=pax+parseInt(document.getElementById("txtNoOfSingles").value);
                            Adultpax=Adultpax+parseInt(document.getElementById("txtNoOfSingles").value);
                            }
                            else
    		                {
    		                    document.getElementById("trsf").style.display.none;
    		                    document.getElementById("txtNoOfSingles").value=0;
    		                }
                        } 
                        
                        if(document.getElementById("tradf"))
				        {
				            var f1 = document.getElementById("lbldACfare").firstChild.data;    		
			                if((!isNaN(parseInt(f1))) &&(parseInt(f1)>0))
			                {
			                 document.getElementById("tradf").style.display.block;
                            document.getElementById("lblFaredormitory").firstChild.data = parseInt(document.getElementById("lbldACfare").firstChild.data);
				            document.getElementById("lblCalcdormitory").firstChild.data=(parseInt(document.getElementById("lbldACfare").firstChild.data) * eval(document.getElementById("txtNoofdormitory").value));
				            document.getElementById("lblTotal").firstChild.data= parseInt(document.getElementById("lblTotal").firstChild.data) +  parseInt(document.getElementById("lblCalcdormitory").firstChild.data);
                            var prevSt =  document.getElementById("stax").value;
    				        if(!isNaN(prevSt)) 
    				        {
    				            prevSt = 0;
    				        }
    				        document.getElementById("stax").value = parseFloat(prevSt) + parseFloat((eval(document.getElementById("lblTotal").firstChild.data)*eval(document.getElementById("service").value))/100);
    				           				       
    				        if(parseFloat(document.getElementById("stax").value)>0)
    				        {   				       
    				        tot=parseFloat(document.getElementById("lblTotal").firstChild.data) + parseFloat(document.getElementById("stax").value);
    				        }
    				        var se=document.getElementById("sess").value;    				        
    				        if(parseInt(se)>0)
    				        { 
    				         if(!isNaN(document.getElementById("cc").value))
    				        {
    				        document.getElementById("cc").value=0;
    				        }   				        
    				        document.getElementById("cc").value= parseFloat(document.getElementById("cc").value)+ parseFloat((eval(tot)*eval(document.getElementById("credit").value))/100);
    				        }
    				        document.getElementById('Labeltax').innerHTML = 'GST of Rs:'+ Math.round(document.getElementById("stax").value) +' Extra'   
                            pax=pax+parseInt(document.getElementById("txtNoofdormitory").value);
                            Adultpax=Adultpax+parseInt(document.getElementById("txtNoofdormitory").value);
                            }
                            else
    		                {
    		                    document.getElementById("tradf").style.display.none;
    		                    document.getElementById("txtNoofdormitory").value=0;
    		                }
                        } 
                          		    
				       
		        }
		
			if(document.getElementById('RadNAC').checked)
			{
			document.getElementById("lblTotal").firstChild.data='0';
			    			
		            if(document.getElementById("traf"))
		            {
		                var g = document.getElementById("lblANACfare").firstChild.data;    		
			            if((!isNaN(parseInt(g))) &&(parseInt(g)>0))
			            {
			            document.getElementById("traf").style.display.block;
				        document.getElementById("lblFareAdults").firstChild.data=parseInt(document.getElementById("lblANACfare").firstChild.data); 
				        document.getElementById("lblCalcAdults").firstChild.data=(parseInt(document.getElementById("lblANACfare").firstChild.data) * eval(document.getElementById("txtNoOfAdults").value));
				        document.getElementById("lblTotal").firstChild.data=parseInt(document.getElementById("lblCalcAdults").firstChild.data);
				           var prevSt =  document.getElementById("stax").value;
    				        if(!isNaN(prevSt)) 
    				        {
    				            prevSt = 0;
    				        }
    				        document.getElementById("stax").value = parseFloat(prevSt) + parseFloat((eval(document.getElementById("lblTotal").firstChild.data)*eval(document.getElementById("service").value))/100);
    				           				       
    				        if(parseFloat(document.getElementById("stax").value)>0)
    				        {   				       
    				        tot=parseFloat(document.getElementById("lblTotal").firstChild.data) + parseFloat(document.getElementById("stax").value);
    				        }
    				        var se=document.getElementById("sess").value;    				        
    				        if(parseInt(se)>0)
    				        { 
    				         if(!isNaN(document.getElementById("cc").value))
    				        {
    				        document.getElementById("cc").value=0;
    				        }   				        
    				        document.getElementById("cc").value= parseFloat(document.getElementById("cc").value)+ parseFloat((eval(tot)*eval(document.getElementById("credit").value))/100);
    				        }
    				        document.getElementById('Labeltax').innerHTML = 'GST of Rs:'+ Math.round(document.getElementById("stax").value) +' Extra'   
                            pax=pax+parseInt(document.getElementById("txtNoOfAdults").value);
                            Adultpax=Adultpax+parseInt(document.getElementById("txtNoOfAdults").value);
				        }
				        else
    		            {
    		                document.getElementById("traf").style.display.none;
    		                document.getElementById("txtNoOfAdults").value=0;
    		            }
				    }
    		    
    		   
				    if(document.getElementById("trcf"))
				    {
				        var h = document.getElementById("lblCNACfare").firstChild.data;    		
			            if((!isNaN(parseInt(h))) &&(parseInt(h)>0))
			            {
			            document.getElementById("trcf").style.display.block;
				        document.getElementById("lblfareChild").firstChild.data=parseInt(document.getElementById("lblCNACfare").firstChild.data) ;
				        document.getElementById("lblCalcChild").firstChild.data=(parseInt(document.getElementById("lblCNACfare").firstChild.data) * eval(document.getElementById("txtNoOfChilds").value));
				        document.getElementById("lblTotal").firstChild.data= parseInt(document.getElementById("lblTotal").firstChild.data) +  parseInt(document.getElementById("lblCalcChild").firstChild.data);
				        var prevSt =  document.getElementById("stax").value;
    				        if(!isNaN(prevSt)) 
    				        {
    				            prevSt = 0;
    				        }
    				        document.getElementById("stax").value = parseFloat(prevSt) + parseFloat((eval(document.getElementById("lblTotal").firstChild.data)*eval(document.getElementById("service").value))/100);
    				           				       
    				        if(parseFloat(document.getElementById("stax").value)>0)
    				        {   				       
    				        tot=parseFloat(document.getElementById("lblTotal").firstChild.data) + parseFloat(document.getElementById("stax").value);
    				        }
    				        var se=document.getElementById("sess").value;    				        
    				        if(parseInt(se)>0)
    				        { 
    				         if(!isNaN(document.getElementById("cc").value))
    				        {
    				        document.getElementById("cc").value=0;
    				        }   				        
    				        document.getElementById("cc").value= parseFloat(document.getElementById("cc").value)+ parseFloat((eval(tot)*eval(document.getElementById("credit").value))/100);
    				        }
    				        document.getElementById('Labeltax').innerHTML = 'GST of Rs:'+ Math.round(document.getElementById("stax").value) +' Extra'   
                            pax=pax+parseInt(document.getElementById("txtNoOfChilds").value);
                            Childpax=Childpax+parseInt(document.getElementById("txtNoOfChilds").value);
				        }
				         else
    		            {
    		                document.getElementById("trcf").style.display.none;
    		                document.getElementById("txtNoOfChilds").value=0;
    		            }				
                    }

                    if (document.getElementById("trAWF")) {
                        var g = document.getElementById("lblAWFNACfare").firstChild.data;
                        if ((!isNaN(parseInt(g))) && (parseInt(g) > 0)) {
                            document.getElementById("trAWF").style.display.block;
                            document.getElementById("lblAWFFareAdults").firstChild.data = parseInt(document.getElementById("lblAWFNACfare").firstChild.data);
                            document.getElementById("lblCalcAWF").firstChild.data = (parseInt(document.getElementById("lblAWFNACfare").firstChild.data) * eval(document.getElementById("txtNoAWFNoOfAdults").value));
                            document.getElementById("lblTotal").firstChild.data = parseInt(document.getElementById("lblTotal").firstChild.data) + parseInt(document.getElementById("lblCalcAWF").firstChild.data);
                            var prevSt = document.getElementById("stax").value;
                            if (!isNaN(prevSt)) {
                                prevSt = 0;
                            }
                            document.getElementById("stax").value = parseFloat(prevSt) + parseFloat((eval(document.getElementById("lblTotal").firstChild.data) * eval(document.getElementById("service").value)) / 100);

                            if (parseFloat(document.getElementById("stax").value) > 0) {
                                tot = parseFloat(document.getElementById("lblTotal").firstChild.data) + parseFloat(document.getElementById("stax").value);
                            }
                            var se = document.getElementById("sess").value;
                            if (parseInt(se) > 0) {
                                if (!isNaN(document.getElementById("cc").value)) {
                                    document.getElementById("cc").value = 0;
                                }
                                document.getElementById("cc").value = parseFloat(document.getElementById("cc").value) + parseFloat((eval(tot) * eval(document.getElementById("credit").value)) / 100);
                            }
                            document.getElementById('Labeltax').innerHTML = 'GST of Rs:' + roundNumber(document.getElementById("stax").value);
                            pax = pax + parseInt(document.getElementById("txtNoAWFNoOfAdults").value);
                            Adultpax=Adultpax+parseInt(document.getElementById("txtNoAWFNoOfAdults").value);
                        }
                        else {
                            document.getElementById("trAWF").style.display.none;
                            document.getElementById("txtNoAWFNoOfAdults").value = 0;
                        }
                    }
                    if (document.getElementById("trCWF")) {
                        var h = document.getElementById("lblCWFNACfare").firstChild.data;
                        if ((!isNaN(parseInt(h))) && (parseInt(h) > 0)) {
                            document.getElementById("trCWF").style.display.block;
                            document.getElementById("lblCWFfareChild").firstChild.data = parseInt(document.getElementById("lblCWFNACfare").firstChild.data);
                            document.getElementById("lblCWFCalcChild").firstChild.data = (parseInt(document.getElementById("lblCWFNACfare").firstChild.data) * eval(document.getElementById("txtNoCWFNoOfChilds").value));
                            document.getElementById("lblTotal").firstChild.data = parseInt(document.getElementById("lblTotal").firstChild.data) + parseInt(document.getElementById("lblCWFfareChild").firstChild.data);
                            var prevSt = document.getElementById("stax").value;
                            if (!isNaN(prevSt)) {
                                prevSt = 0;
                            }
                            document.getElementById("stax").value = parseFloat(prevSt) + parseFloat((eval(document.getElementById("lblTotal").firstChild.data) * eval(document.getElementById("service").value)) / 100);

                            if (parseFloat(document.getElementById("stax").value) > 0) {
                                tot = parseFloat(document.getElementById("lblTotal").firstChild.data) + parseFloat(document.getElementById("stax").value);
                            }
                            var se = document.getElementById("sess").value;
                            if (parseInt(se) > 0) {
                                if (!isNaN(document.getElementById("cc").value)) {
                                    document.getElementById("cc").value = 0;
                                }
                                document.getElementById("cc").value = parseFloat(document.getElementById("cc").value) + parseFloat((eval(tot) * eval(document.getElementById("credit").value)) / 100);
                            }
                            document.getElementById('Labeltax').innerHTML = 'GST of Rs:' + roundNumber(document.getElementById("stax").value);
                            pax = pax + parseInt(document.getElementById("txtNoCWFNoOfChilds").value);
                            Childpax=Childpax+parseInt(document.getElementById("txtNoCWFNoOfChilds").value);
                        }
                        else {
                            document.getElementById("trCWF").style.display.none;
                            document.getElementById("txtNoCWFNoOfChilds").value = 0;
                        }
                    }             
                    if(document.getElementById("tra2f"))
                    {
                        var i = document.getElementById("lblA2NACfare").firstChild.data;    		
			            if((!isNaN(parseInt(i))) &&(parseInt(i)>0))
			            {
			            document.getElementById("tra2f").style.display.block;
				        document.getElementById("lblFareAdultsTwin").firstChild.data = parseInt(document.getElementById("lblA2NACfare").firstChild.data);
				        document.getElementById("lblCalcAdultsTwin").firstChild.data=(parseInt(document.getElementById("lblA2NACfare").firstChild.data) * eval(document.getElementById("txtNoOfAdultsTwin").value));
				        document.getElementById("lblTotal").firstChild.data= parseInt(document.getElementById("lblTotal").firstChild.data) +  parseInt(document.getElementById("lblCalcAdultsTwin").firstChild.data);
				        var prevSt =  document.getElementById("stax").value;
    				        if(!isNaN(prevSt)) 
    				        {
    				            prevSt = 0;
    				        }
    				        document.getElementById("stax").value = parseFloat(prevSt) + parseFloat((eval(document.getElementById("lblTotal").firstChild.data)*eval(document.getElementById("service").value))/100);
    				           				       
    				        if(parseFloat(document.getElementById("stax").value)>0)
    				        {   				       
    				        tot=parseFloat(document.getElementById("lblTotal").firstChild.data) + parseFloat(document.getElementById("stax").value);
    				        }
    				        var se=document.getElementById("sess").value;    				        
    				        if(parseInt(se)>0)
    				        { 
    				         if(!isNaN(document.getElementById("cc").value))
    				        {
    				        document.getElementById("cc").value=0;
    				        }   				        
    				        document.getElementById("cc").value= parseFloat(document.getElementById("cc").value)+ parseFloat((eval(tot)*eval(document.getElementById("credit").value))/100);
    				        }
    				        document.getElementById('Labeltax').innerHTML = 'GST of Rs:'+ Math.round(document.getElementById("stax").value) +' Extra'   
                            pax=pax+parseInt(document.getElementById("txtNoOfAdultsTwin").value);
                            Adultpax=Adultpax+parseInt(document.getElementById("txtNoOfAdultsTwin").value);
				        }
				        else
    		            {
    		                document.getElementById("tra2f").style.display.none;
    		                document.getElementById("txtNoOfAdultsTwin").value=0;
    		            }
				    }
    		    
    		    
				     if(document.getElementById("tra3f"))
				    {
				        var j = document.getElementById("lblA3NACfare").firstChild.data;    		
			            if((!isNaN(parseInt(j))) &&(parseInt(j)>0))
			            {
			            document.getElementById("tra3f").style.display.block; 
				        document.getElementById("lblFareAdultsTriple").firstChild.data = parseInt(document.getElementById("lblA3NACfare").firstChild.data);
				        document.getElementById("lblCalcAdultsTriple").firstChild.data=(parseInt(document.getElementById("lblA3NACfare").firstChild.data) * eval(document.getElementById("txtNoOfAdultsTriple").value));
				        document.getElementById("lblTotal").firstChild.data= parseInt(document.getElementById("lblTotal").firstChild.data) +  parseInt(document.getElementById("lblCalcAdultsTriple").firstChild.data);
				        var prevSt =  document.getElementById("stax").value;
    				        if(!isNaN(prevSt)) 
    				        {
    				            prevSt = 0;
    				        }
    				        document.getElementById("stax").value = parseFloat(prevSt) + parseFloat((eval(document.getElementById("lblTotal").firstChild.data)*eval(document.getElementById("service").value))/100);
    				           				       
    				        if(parseFloat(document.getElementById("stax").value)>0)
    				        {   				       
    				        tot=parseFloat(document.getElementById("lblTotal").firstChild.data) + parseFloat(document.getElementById("stax").value);
    				        }
    				        var se=document.getElementById("sess").value;    				        
    				        if(parseInt(se)>0)
    				        { 
    				         if(!isNaN(document.getElementById("cc").value))
    				        {
    				        document.getElementById("cc").value=0;
    				        }   				        
    				        document.getElementById("cc").value= parseFloat(document.getElementById("cc").value)+ parseFloat((eval(tot)*eval(document.getElementById("credit").value))/100);
    				        }
    				        document.getElementById('Labeltax').innerHTML = 'GST of Rs:'+ Math.round(document.getElementById("stax").value) +' Extra'   
                            pax=pax+parseInt(document.getElementById("txtNoOfAdultsTriple").value);
                            Adultpax=Adultpax+parseInt(document.getElementById("txtNoOfAdultsTriple").value);
				        }
				         else
    		            {
    		                document.getElementById("tra3f").style.display.none;
    		                document.getElementById("txtNoOfAdultsTriple").value=0;
    		            }
				    }
    		   
    		    
				     if(document.getElementById("trcbf"))
				    {
				        var k = document.getElementById("lblCBNACfare").firstChild.data;    		
			            if((!isNaN(parseInt(k))) &&(parseInt(k)>0))
			            { 
			            document.getElementById("trcbf").style.display.block;
                        document.getElementById("lblFareChildBed").firstChild.data = parseInt(document.getElementById("lblCBNACfare").firstChild.data);
				        document.getElementById("lblCalcChildBed").firstChild.data=(parseInt(document.getElementById("lblCBNACfare").firstChild.data) * eval(document.getElementById("txtNoOfChildBed").value));
				        document.getElementById("lblTotal").firstChild.data= parseInt(document.getElementById("lblTotal").firstChild.data) +  parseInt(document.getElementById("lblCalcChildBed").firstChild.data);
				        var prevSt =  document.getElementById("stax").value;
    				        if(!isNaN(prevSt)) 
    				        {
    				            prevSt = 0;
    				        }
    				        document.getElementById("stax").value = parseFloat(prevSt) + parseFloat((eval(document.getElementById("lblTotal").firstChild.data)*eval(document.getElementById("service").value))/100);
    				           				       
    				        if(parseFloat(document.getElementById("stax").value)>0)
    				        {   				       
    				        tot=parseFloat(document.getElementById("lblTotal").firstChild.data) + parseFloat(document.getElementById("stax").value);
    				        }
    				        var se=document.getElementById("sess").value;    				        
    				        if(parseInt(se)>0)
    				        {
    				         if(!isNaN(document.getElementById("cc").value))
    				        {
    				        document.getElementById("cc").value=0;
    				        }    				        
    				        document.getElementById("cc").value= parseFloat(document.getElementById("cc").value)+ parseFloat((eval(tot)*eval(document.getElementById("credit").value))/100);
    				        }
    				        document.getElementById('Labeltax').innerHTML = 'GST of Rs:'+ Math.round(document.getElementById("stax").value) +' Extra'   
                            pax=pax+parseInt(document.getElementById("txtNoOfChildBed").value);
                            Childpax=Childpax+parseInt(document.getElementById("txtNoOfChildBed").value);
				        }
				        else
    		            {
    		                document.getElementById("trcbf").style.display.none;
    		                document.getElementById("txtNoOfChildBed").value=0;
    		            }
				    }
    		    
    		     
				     if(document.getElementById("trsf"))
				    {
				        var l = document.getElementById("lblSNACfare").firstChild.data;    		
			            if((!isNaN(parseInt(l))) &&(parseInt(l)>0))
			            {
			             document.getElementById("trsf").style.display.block;
                        document.getElementById("lblFareSingles").firstChild.data = parseInt(document.getElementById("lblSNACfare").firstChild.data);
				        document.getElementById("lblCalcSingles").firstChild.data=(parseInt(document.getElementById("lblSNACfare").firstChild.data) * eval(document.getElementById("txtNoOfSingles").value));				    
				        document.getElementById("lblTotal").firstChild.data= parseInt(document.getElementById("lblTotal").firstChild.data) +  parseInt(document.getElementById("lblCalcSingles").firstChild.data);
				       var prevSt =  document.getElementById("stax").value;
    				        if(!isNaN(prevSt)) 
    				        {
    				            prevSt = 0;
    				        }
    				        document.getElementById("stax").value = parseFloat(prevSt) + parseFloat((eval(document.getElementById("lblTotal").firstChild.data)*eval(document.getElementById("service").value))/100);
    				           				       
    				        if(parseFloat(document.getElementById("stax").value)>0)
    				        {   				       
    				        tot=parseFloat(document.getElementById("lblTotal").firstChild.data) + parseFloat(document.getElementById("stax").value);
    				        }
    				        var se=document.getElementById("sess").value;    				        
    				        if(parseInt(se)>0)
    				        {
    				         if(!isNaN(document.getElementById("cc").value))
    				        {
    				        document.getElementById("cc").value=0;
    				        }    				        
    				        document.getElementById("cc").value= parseFloat(document.getElementById("cc").value)+ parseFloat((eval(tot)*eval(document.getElementById("credit").value))/100);
    				        }
    				        document.getElementById('Labeltax').innerHTML = 'GST of Rs:'+ Math.round(document.getElementById("stax").value) +' Extra'   
                            pax=pax+parseInt(document.getElementById("txtNoOfSingles").value);
                            Adultpax=Adultpax+parseInt(document.getElementById("txtNoOfSingles").value);
				        }
				         else
    		            {
    		                document.getElementById("trsf").style.display.none;
    		                document.getElementById("txtNoOfSingles").value=0;
    		            }
				    }
				    
				     if(document.getElementById("tradf"))
				    {
				        var l1 = document.getElementById("lbldNACfare").firstChild.data;    		
			            if((!isNaN(parseInt(l1))) &&(parseInt(l1)>0))
			            {
			             document.getElementById("tradf").style.display.block;
                         document.getElementById("lblFaredormitory").firstChild.data = parseInt(document.getElementById("lbldNACfare").firstChild.data);
				         document.getElementById("lblCalcdormitory").firstChild.data=(parseInt(document.getElementById("lbldNACfare").firstChild.data) * eval(document.getElementById("txtNoofdormitory").value));				    
				         document.getElementById("lblTotal").firstChild.data= parseInt(document.getElementById("lblTotal").firstChild.data) +  parseInt(document.getElementById("lblCalcdormitory").firstChild.data);
				         var prevSt =  document.getElementById("stax").value;
    				        if(!isNaN(prevSt)) 
    				        {
    				            prevSt = 0;
    				        }
    				        document.getElementById("stax").value = parseFloat(prevSt) + parseFloat((eval(document.getElementById("lblTotal").firstChild.data)*eval(document.getElementById("service").value))/100);
    				           				       
    				        if(parseFloat(document.getElementById("stax").value)>0)
    				        {   				       
    				        tot=parseFloat(document.getElementById("lblTotal").firstChild.data) + parseFloat(document.getElementById("stax").value);
    				        }
    				        var se=document.getElementById("sess").value;    				        
    				        if(parseInt(se)>0)
    				        {
    				         if(!isNaN(document.getElementById("cc").value))
    				        {
    				        document.getElementById("cc").value=0;
    				        }    				        
    				        document.getElementById("cc").value= parseFloat(document.getElementById("cc").value)+ parseFloat((eval(tot)*eval(document.getElementById("credit").value))/100);
    				        }
    				        document.getElementById('Labeltax').innerHTML = 'GST of Rs:'+ Math.round(document.getElementById("stax").value) +' Extra'   
                            pax=pax+parseInt(document.getElementById("txtNoofdormitory").value);
                            Adultpax=Adultpax+parseInt(document.getElementById("txtNoofdormitory").value);
				        }
				         else
    		            {
    		                document.getElementById("tradf").style.display.none;
    		                document.getElementById("txtNoofdormitory").value=0;
    		            }
				    }
				    
    		   
			}
		
            if(document.getElementById("RadAC").checked)
	            {
	            document.getElementById("lblTotal").firstChild.data='0';
		        
	                if(document.getElementById("traf"))
	                {
	                     var m = document.getElementById("lblAACfare").firstChild.data;    		
		                if((!isNaN(parseInt(m))) &&(parseInt(m)>0))
		                {
		                document.getElementById("traf").style.display.block;
	                    document.getElementById("lblFareAdults").firstChild.data=parseInt(document.getElementById("lblAACfare").firstChild.data); 
                        document.getElementById("lblCalcAdults").firstChild.data=(parseInt(document.getElementById("lblAACfare").firstChild.data) * eval(document.getElementById("txtNoOfAdults").value));
                        document.getElementById("lblTotal").firstChild.data=parseInt(document.getElementById("lblCalcAdults").firstChild.data);
                        var prevSt =  document.getElementById("stax").value;
    				        if(!isNaN(prevSt)) 
    				        {
    				            prevSt = 0;
    				        }
    				        document.getElementById("stax").value = parseFloat(prevSt) + parseFloat((eval(document.getElementById("lblTotal").firstChild.data)*eval(document.getElementById("service").value))/100);
    				           				       
    				        if(parseFloat(document.getElementById("stax").value)>0)
    				        {   				       
    				        tot=parseFloat(document.getElementById("lblTotal").firstChild.data) + parseFloat(document.getElementById("stax").value);
    				        }
    				        var se=document.getElementById("sess").value;    				        
    				        if(parseInt(se)>0)
    				        {
    				         if(!isNaN(document.getElementById("cc").value))
    				        {
    				        document.getElementById("cc").value=0;
    				        }    				        
    				        document.getElementById("cc").value= parseFloat(document.getElementById("cc").value)+ parseFloat((eval(tot)*eval(document.getElementById("credit").value))/100);
    				        }
    				        document.getElementById('Labeltax').innerHTML = 'GST of Rs:'+ Math.round(document.getElementById("stax").value) +' Extra'   
                            // pax=pax+parseInt(document.getElementById("txtNoOfAdults").value);
                        }
                         else
    		            {
    		                document.getElementById("traf").style.display.none;
    		                document.getElementById("txtNoOfAdults").value=0;
    		            }
                    }
    		   
    		    
                    if(document.getElementById("trcf"))
                    {
                        var n = document.getElementById("lblCACfare").firstChild.data;    		
		                if((!isNaN(parseInt(n))) &&(parseInt(n)>0))
		                { 
		                 document.getElementById("trcf").style.display.block;                    
		                document.getElementById("lblfareChild").firstChild.data=parseInt(document.getElementById("lblCACfare").firstChild.data) ;
		                document.getElementById("lblCalcChild").firstChild.data=(parseInt(document.getElementById("lblCACfare").firstChild.data) * eval(document.getElementById("txtNoOfChilds").value));
		                document.getElementById("lblTotal").firstChild.data= parseInt(document.getElementById("lblTotal").firstChild.data) +  parseInt(document.getElementById("lblCalcChild").firstChild.data);	            
		                var prevSt =  document.getElementById("stax").value;
    				        if(!isNaN(prevSt)) 
    				        {
    				            prevSt = 0;
    				        }
    				        document.getElementById("stax").value = parseFloat(prevSt) + parseFloat((eval(document.getElementById("lblTotal").firstChild.data)*eval(document.getElementById("service").value))/100);
    				           				       
    				        if(parseFloat(document.getElementById("stax").value)>0)
    				        {   				       
    				        tot=parseFloat(document.getElementById("lblTotal").firstChild.data) + parseFloat(document.getElementById("stax").value);
    				        }
    				        var se=document.getElementById("sess").value;    				        
    				        if(parseInt(se)>0)
    				        {
    				         if(!isNaN(document.getElementById("cc").value))
    				        {
    				        document.getElementById("cc").value=0;
    				        }    				        
    				        document.getElementById("cc").value= parseFloat(document.getElementById("cc").value)+ parseFloat((eval(tot)*eval(document.getElementById("credit").value))/100);
    				        }
    				        document.getElementById('Labeltax').innerHTML = 'GST of Rs:'+ Math.round(document.getElementById("stax").value) +' Extra'   
                            //pax=pax+parseInt(document.getElementById("txtNoOfChilds").value);
		                }
		                else
    		            {
    		                document.getElementById("trcf").style.display.none;
    		                document.getElementById("txtNoOfChilds").value=0;
    		            }
		            }

		            if (document.getElementById("trAWF")) {
		                debugger;
		                var a = document.getElementById("lblAWFfare").firstChild.data;

		                if ((!isNaN(parseInt(a))) && (parseInt(a) > 0)) {
		                    var prevSt = document.getElementById("stax").value;
		                    document.getElementById("trAWF").style.display.block;
		                    document.getElementById("lblAWFFareAdults").firstChild.data = parseInt(document.getElementById("lblAWFfare").firstChild.data);
		                    document.getElementById("lblCalcAWF").firstChild.data = (parseInt(document.getElementById("lblAWFfare").firstChild.data) * eval(document.getElementById("txtNoAWFNoOfAdults").value));
		                    document.getElementById("lblTotal").firstChild.data = parseInt(document.getElementById("lblTotal").firstChild.data) + parseInt(document.getElementById("lblCalcAWF").firstChild.data);

		                    if (!isNaN(prevSt)) {
		                        prevSt = 0;
		                    }
		                    document.getElementById("stax").value = parseFloat(prevSt) + parseFloat((eval(document.getElementById("lblTotal").firstChild.data) * eval(document.getElementById("service").value)) / 100);
		                    if (parseFloat(document.getElementById("stax").value) > 0) {
		                        tot = parseFloat(document.getElementById("lblTotal").firstChild.data) + parseFloat(document.getElementById("stax").value);
		                    }
		                    var se = document.getElementById("sess").value;
		                    if (parseInt(se) > 0) {
		                        if (!isNaN(document.getElementById("cc").value)) {
		                            document.getElementById("cc").value = 0;
		                        }
		                        document.getElementById("cc").value = parseFloat(document.getElementById("cc").value) + parseFloat((eval(tot) * eval(document.getElementById("credit").value)) / 100);
		                    }
		                    // and Convenience Charges of Rs:'+ roundNumber(document.getElementById("cc").value) +' are     				       


		                    document.getElementById('Labeltax').innerHTML = 'GST of Rs:' + roundNumber(document.getElementById("stax").value);
		                    //pax = pax + parseInt(document.getElementById("txtNoAWFNoOfAdults").value);
		                }
		                else {
		                    document.getElementById("trAWF").style.display.none;
		                    document.getElementById("txtNoAWFNoOfAdults").value = 0;
		                }
		            }

		            if (document.getElementById("trCWF")) {
		                var b = document.getElementById("lblCWFfare").firstChild.data;
		                if ((!isNaN(parseInt(b))) && (parseInt(b) > 0)) {
		                    document.getElementById("trCWF").style.display.block;
		                    document.getElementById("lblCWFfareChild").firstChild.data = parseInt(document.getElementById("lblCWFfare").firstChild.data);
		                    document.getElementById("lblCWFCalcChild").firstChild.data = (parseInt(document.getElementById("lblCWFfare").firstChild.data) * eval(document.getElementById("txtNoCWFNoOfChilds").value));
		                    document.getElementById("lblTotal").firstChild.data = parseInt(document.getElementById("lblTotal").firstChild.data) + parseInt(document.getElementById("lblCWFCalcChild").firstChild.data);
		                    var prevSt = document.getElementById("stax").value;
		                    if (!isNaN(prevSt)) {
		                        prevSt = 0;
		                    }
		                    document.getElementById("stax").value = parseFloat(prevSt) + parseFloat((eval(document.getElementById("lblTotal").firstChild.data) * eval(document.getElementById("service").value)) / 100);

		                    if (parseFloat(document.getElementById("stax").value) > 0) {
		                        tot = parseFloat(document.getElementById("lblTotal").firstChild.data) + parseFloat(document.getElementById("stax").value);
		                    }
		                    var se = document.getElementById("sess").value;
		                    if (parseInt(se) > 0) {
		                        if (!isNaN(document.getElementById("cc").value)) {
		                            document.getElementById("cc").value = 0;
		                        }
		                        document.getElementById("cc").value = parseFloat(document.getElementById("cc").value) + parseFloat((eval(tot) * eval(document.getElementById("credit").value)) / 100);
		                    }
		                    document.getElementById('Labeltax').innerHTML = 'GST of Rs:' + roundNumber(document.getElementById("stax").value);
		                    //pax = pax + parseInt(document.getElementById("txtNoCWFNoOfChilds").value);
		                }
		                else {
		                    document.getElementById("trCWF").style.display.none;
		                    document.getElementById("txtNoCWFNoOfChilds").value = 0;
		                }
		            } 
		            if(document.getElementById("tra2f"))
		            {
		                var o = document.getElementById("lblA2ACfare").firstChild.data;    		
		                if((!isNaN(parseInt(o))) &&(parseInt(o)>0))
		                { 
		                document.getElementById("tra2f").style.display.block;
		                document.getElementById("lblFareAdultsTwin").firstChild.data = parseInt(document.getElementById("lblA2ACfare").firstChild.data);
		                document.getElementById("lblCalcAdultsTwin").firstChild.data=(parseInt(document.getElementById("lblA2ACfare").firstChild.data) * eval(document.getElementById("txtNoOfAdultsTwin").value));
		                document.getElementById("lblTotal").firstChild.data= parseInt(document.getElementById("lblTotal").firstChild.data) +  parseInt(document.getElementById("lblCalcAdultsTwin").firstChild.data);
		                var prevSt =  document.getElementById("stax").value;
    				        if(!isNaN(prevSt)) 
    				        {
    				            prevSt = 0;
    				        }
    				        document.getElementById("stax").value = parseFloat(prevSt) + parseFloat((eval(document.getElementById("lblTotal").firstChild.data)*eval(document.getElementById("service").value))/100);
    				           				       
    				        if(parseFloat(document.getElementById("stax").value)>0)
    				        {   				       
    				        tot=parseFloat(document.getElementById("lblTotal").firstChild.data) + parseFloat(document.getElementById("stax").value);
    				        }
    				        var se=document.getElementById("sess").value;    				        
    				        if(parseInt(se)>0)
    				        {
    				         if(!isNaN(document.getElementById("cc").value))
    				        {
    				        document.getElementById("cc").value=0;
    				        }    				        
    				        document.getElementById("cc").value= parseFloat(document.getElementById("cc").value)+ parseFloat((eval(tot)*eval(document.getElementById("credit").value))/100);
    				        }
    				        document.getElementById('Labeltax').innerHTML = 'GST of Rs:'+ Math.round(document.getElementById("stax").value) +' Extra'   
                            //pax=pax+parseInt(document.getElementById("txtNoOfAdultsTwin").value);
		                }
		                else
    		            {
    		                document.getElementById("tra2f").style.display.none;
    		                document.getElementById("txtNoOfAdultsTwin").value=0;
    		            }
		            }
    		    
    		     
		            if(document.getElementById("tra3f"))
		            {
		                var p = document.getElementById("lblA3ACfare").firstChild.data;    		
		                if((!isNaN(parseInt(p))) &&(parseInt(p)>0))
		                {
		                document.getElementById("tra3f").style.display.block; 
		                document.getElementById("lblFareAdultsTriple").firstChild.data = parseInt(document.getElementById("lblA3ACfare").firstChild.data);
		                document.getElementById("lblCalcAdultsTriple").firstChild.data=(parseInt(document.getElementById("lblA3ACfare").firstChild.data) * eval(document.getElementById("txtNoOfAdultsTriple").value));
		                document.getElementById("lblTotal").firstChild.data= parseInt(document.getElementById("lblTotal").firstChild.data) +  parseInt(document.getElementById("lblCalcAdultsTriple").firstChild.data);
		                var prevSt =  document.getElementById("stax").value;
    				        if(!isNaN(prevSt)) 
    				        {
    				            prevSt = 0;
    				        }
    				        document.getElementById("stax").value = parseFloat(prevSt) + parseFloat((eval(document.getElementById("lblTotal").firstChild.data)*eval(document.getElementById("service").value))/100);
    				           				       
    				        if(parseFloat(document.getElementById("stax").value)>0)
    				        {   				       
    				        tot=parseFloat(document.getElementById("lblTotal").firstChild.data) + parseFloat(document.getElementById("stax").value);
    				        }
    				        var se=document.getElementById("sess").value;    				        
    				        if(parseInt(se)>0)
    				        {
    				         if(!isNaN(document.getElementById("cc").value))
    				        {
    				        document.getElementById("cc").value=0;
    				        }    				        
    				        document.getElementById("cc").value= parseFloat(document.getElementById("cc").value)+ parseFloat((eval(tot)*eval(document.getElementById("credit").value))/100);
    				        }
    				        document.getElementById('Labeltax').innerHTML = 'GST of Rs:'+ Math.round(document.getElementById("stax").value) +' Extra'   
                            //pax=pax+parseInt(document.getElementById("txtNoOfAdultsTriple").value);
		                }
		                else
    		            {
    		                document.getElementById("tra3f").style.display.none;
    		                document.getElementById("txtNoOfAdultsTriple").value=0;
    		            }
		            }
    		    
    		    
		            if(document.getElementById("trcbf"))
		            {
		                var q = document.getElementById("lblCBACfare").firstChild.data;    		
		                if((!isNaN(parseInt(q))) &&(parseInt(q)>0))
		                { 
		                document.getElementById("trcbf").style.display.block;
                        document.getElementById("lblFareChildBed").firstChild.data = parseInt(document.getElementById("lblCBACfare").firstChild.data);
		                document.getElementById("lblCalcChildBed").firstChild.data=(parseInt(document.getElementById("lblCBACfare").firstChild.data) * eval(document.getElementById("txtNoOfChildBed").value));
		                document.getElementById("lblTotal").firstChild.data= parseInt(document.getElementById("lblTotal").firstChild.data) +  parseInt(document.getElementById("lblCalcChildBed").firstChild.data);
		                var prevSt =  document.getElementById("stax").value;
    				        if(!isNaN(prevSt)) 
    				        {
    				            prevSt = 0;
    				        }
    				        document.getElementById("stax").value = parseFloat(prevSt) + parseFloat((eval(document.getElementById("lblTotal").firstChild.data)*eval(document.getElementById("service").value))/100);
    				           				       
    				        if(parseFloat(document.getElementById("stax").value)>0)
    				        {   				       
    				        tot=parseFloat(document.getElementById("lblTotal").firstChild.data) + parseFloat(document.getElementById("stax").value);
    				        }
    				        var se=document.getElementById("sess").value;    				        
    				        if(parseInt(se)>0)
    				        {
    				         if(!isNaN(document.getElementById("cc").value))
    				        {
    				        document.getElementById("cc").value=0;
    				        }    				        
    				        document.getElementById("cc").value= parseFloat(document.getElementById("cc").value)+ parseFloat((eval(tot)*eval(document.getElementById("credit").value))/100);
    				        }
    				        document.getElementById('Labeltax').innerHTML = 'GST of Rs:'+ Math.round(document.getElementById("stax").value) +' Extra'   
                            //pax=pax+parseInt(document.getElementById("txtNoOfChildBed").value);
		                }
		                else
    		            {
    		                document.getElementById("trcbf").style.display.none;
    		                document.getElementById("txtNoOfChildBed").value=0;
    		            }
		            }
    		    
    		    
		            if(document.getElementById("trsf"))
		            {
		                var r = document.getElementById("lblSACfare").firstChild.data;    		
		                if((!isNaN(parseInt(r))) &&(parseInt(r)>0))
		                { 
		                document.getElementById("trsf").style.display.block;
                        document.getElementById("lblFareSingles").firstChild.data = parseInt(document.getElementById("lblSACfare").firstChild.data);
		                document.getElementById("lblCalcSingles").firstChild.data=(parseInt(document.getElementById("lblSACfare").firstChild.data) * eval(document.getElementById("txtNoOfSingles").value));
                        document.getElementById("lblTotal").firstChild.data= parseInt(document.getElementById("lblTotal").firstChild.data) +  parseInt(document.getElementById("lblCalcSingles").firstChild.data);
                        var prevSt =  document.getElementById("stax").value;
    				        if(!isNaN(prevSt)) 
    				        {
    				            prevSt = 0;
    				        }
    				        document.getElementById("stax").value = parseFloat(prevSt) + parseFloat((eval(document.getElementById("lblTotal").firstChild.data)*eval(document.getElementById("service").value))/100);
    				           				       
    				        if(parseFloat(document.getElementById("stax").value)>0)
    				        {   				       
    				        tot=parseFloat(document.getElementById("lblTotal").firstChild.data) + parseFloat(document.getElementById("stax").value);
    				        }
    				        var se=document.getElementById("sess").value;    				        
    				        if(parseInt(se)>0)
    				        {
    				         if(!isNaN(document.getElementById("cc").value))
    				        {
    				        document.getElementById("cc").value=0;
    				        }    				        
    				        document.getElementById("cc").value= parseFloat(document.getElementById("cc").value)+ parseFloat((eval(tot)*eval(document.getElementById("credit").value))/100);
    				        }
    				        document.getElementById('Labeltax').innerHTML = 'GST of Rs:'+ Math.round(document.getElementById("stax").value) +' Extra'   
                           // pax=pax+parseInt(document.getElementById("txtNoOfSingles").value);
                        }
                        else
    		            {
    		                document.getElementById("trsf").style.display.none;
    		                document.getElementById("txtNoOfSingles").value=0;
    		            } 
                    }
                    
                    if(document.getElementById("tradf"))
		            {
		                var r1 = document.getElementById("lbldACfare").firstChild.data;    		
		                if((!isNaN(parseInt(r1))) &&(parseInt(r1)>0))
		                { 
		                document.getElementById("tradf").style.display.block;
                        document.getElementById("lblFaredormitory").firstChild.data = parseInt(document.getElementById("lbldACfare").firstChild.data);
		                document.getElementById("lblCalcdormitory").firstChild.data=(parseInt(document.getElementById("lbldACfare").firstChild.data) * eval(document.getElementById("txtNoofdormitory").value));
                        document.getElementById("lblTotal").firstChild.data= parseInt(document.getElementById("lblTotal").firstChild.data) +  parseInt(document.getElementById("lblCalcdormitory").firstChild.data);
                        var prevSt =  document.getElementById("stax").value;
    				        if(!isNaN(prevSt)) 
    				        {
    				            prevSt = 0;
    				        }
    				        document.getElementById("stax").value = parseFloat(prevSt) + parseFloat((eval(document.getElementById("lblTotal").firstChild.data)*eval(document.getElementById("service").value))/100);
    				           				       
    				        if(parseFloat(document.getElementById("stax").value)>0)
    				        {   				       
    				        tot=parseFloat(document.getElementById("lblTotal").firstChild.data) + parseFloat(document.getElementById("stax").value);
    				        }
    				        var se=document.getElementById("sess").value;    				        
    				        if(parseInt(se)>0)
    				        {
    				         if(!isNaN(document.getElementById("cc").value))
    				        {
    				        document.getElementById("cc").value=0;
    				        }    				        
    				        document.getElementById("cc").value= parseFloat(document.getElementById("cc").value)+ parseFloat((eval(tot)*eval(document.getElementById("credit").value))/100);
    				        }
    				        document.getElementById('Labeltax').innerHTML = 'GST of Rs:'+ Math.round(document.getElementById("stax").value) +' Extra'   
                           // pax=pax+parseInt(document.getElementById("txtNoOfSingles").value);
                        }
                        else
    		            {
    		                document.getElementById("tradf").style.display.none;
    		                document.getElementById("txtNoofdormitory").value=0;
    		            } 
                    }
    		    
	            }
            if(document.getElementById("RadNAC").checked)
                {
                document.getElementById("lblTotal").firstChild.data='0';
                		
                    if(document.getElementById("traf"))
                    {
                        var s = document.getElementById("lblANACfare").firstChild.data;    		
		                if((!isNaN(parseInt(s))) &&(parseInt(s)>0))
		                {  
		                document.getElementById("traf").style.display.block;               
                        document.getElementById("lblFareAdults").firstChild.data=parseInt(document.getElementById("lblANACfare").firstChild.data); 
	                    document.getElementById("lblCalcAdults").firstChild.data=(parseInt(document.getElementById("lblANACfare").firstChild.data) * eval(document.getElementById("txtNoOfAdults").value));
	                    document.getElementById("lblTotal").firstChild.data=parseInt(document.getElementById("lblCalcAdults").firstChild.data);
	                    var prevSt =  document.getElementById("stax").value;
    				        if(!isNaN(prevSt)) 
    				        {
    				            prevSt = 0;
    				        }
    				        document.getElementById("stax").value = parseFloat(prevSt) + parseFloat((eval(document.getElementById("lblTotal").firstChild.data)*eval(document.getElementById("service").value))/100);
    				           				       
    				        if(parseFloat(document.getElementById("stax").value)>0)
    				        {   				       
    				        tot=parseFloat(document.getElementById("lblTotal").firstChild.data) + parseFloat(document.getElementById("stax").value);
    				        }
    				        var se=document.getElementById("sess").value;    				        
    				        if(parseInt(se)>0)
    				        {
    				         if(!isNaN(document.getElementById("cc").value))
    				        {
    				        document.getElementById("cc").value=0;
    				        }    				        
    				        document.getElementById("cc").value= parseFloat(document.getElementById("cc").value)+ parseFloat((eval(tot)*eval(document.getElementById("credit").value))/100);
    				        }
    				        document.getElementById('Labeltax').innerHTML = 'GST of Rs:'+ Math.round(document.getElementById("stax").value) +' Extra'   
                           // pax=pax+parseInt(document.getElementById("txtNoOfAdults").value);
	                    }
	                    else
    		            {
    		                document.getElementById("traf").style.display.none;
    		                document.getElementById("txtNoOfAdults").value=0;
    		            }
	                }
	            
    		     
	                 if(document.getElementById("trcf"))
	                {
	                    var t = document.getElementById("lblCNACfare").firstChild.data;    		
		                if((!isNaN(parseInt(t))) &&(parseInt(t)>0))
		                {
		                 document.getElementById("trcf").style.display.block;
		                document.getElementById("lblfareChild").firstChild.data=parseInt(document.getElementById("lblCNACfare").firstChild.data) ;
		                document.getElementById("lblCalcChild").firstChild.data=(parseInt(document.getElementById("lblCNACfare").firstChild.data) * eval(document.getElementById("txtNoOfChilds").value));
		                document.getElementById("lblTotal").firstChild.data= parseInt(document.getElementById("lblTotal").firstChild.data) +  parseInt(document.getElementById("lblCalcChild").firstChild.data);		            
		                var prevSt =  document.getElementById("stax").value;
    				        if(!isNaN(prevSt)) 
    				        {
    				            prevSt = 0;
    				        }
    				        document.getElementById("stax").value = parseFloat(prevSt) + parseFloat((eval(document.getElementById("lblTotal").firstChild.data)*eval(document.getElementById("service").value))/100);
    				           				       
    				        if(parseFloat(document.getElementById("stax").value)>0)
    				        {   				       
    				        tot=parseFloat(document.getElementById("lblTotal").firstChild.data) + parseFloat(document.getElementById("stax").value);
    				        }
    				        var se=document.getElementById("sess").value;    				        
    				        if(parseInt(se)>0)
    				        {
    				         if(!isNaN(document.getElementById("cc").value))
    				        {
    				        document.getElementById("cc").value=0;
    				        }    				        
    				        document.getElementById("cc").value= parseFloat(document.getElementById("cc").value)+ parseFloat((eval(tot)*eval(document.getElementById("credit").value))/100);
    				        }
    				        document.getElementById('Labeltax').innerHTML = 'GST of Rs:'+ Math.round(document.getElementById("stax").value) +' Extra'   
                            //pax=pax+parseInt(document.getElementById("txtNoOfChilds").value);
		                }
		                else
    		            {
    		                document.getElementById("trcf").style.display.none;
    		                document.getElementById("txtNoOfChilds").value=0;
    		            }
		            }
		            if (document.getElementById("trAWF")) {
		                var g = document.getElementById("lblAWFNACfare").firstChild.data;
		                if ((!isNaN(parseInt(g))) && (parseInt(g) > 0)) {
		                    document.getElementById("trAWF").style.display.block;
		                    document.getElementById("lblAWFFareAdults").firstChild.data = parseInt(document.getElementById("lblAWFNACfare").firstChild.data);
		                    document.getElementById("lblCalcAWF").firstChild.data = (parseInt(document.getElementById("lblAWFNACfare").firstChild.data) * eval(document.getElementById("txtNoAWFNoOfAdults").value));
		                    document.getElementById("lblTotal").firstChild.data = parseInt(document.getElementById("lblTotal").firstChild.data) + parseInt(document.getElementById("lblCalcAWF").firstChild.data);
		                    var prevSt = document.getElementById("stax").value;
		                    if (!isNaN(prevSt)) {
		                        prevSt = 0;
		                    }
		                    document.getElementById("stax").value = parseFloat(prevSt) + parseFloat((eval(document.getElementById("lblTotal").firstChild.data) * eval(document.getElementById("service").value)) / 100);

		                    if (parseFloat(document.getElementById("stax").value) > 0) {
		                        tot = parseFloat(document.getElementById("lblTotal").firstChild.data) + parseFloat(document.getElementById("stax").value);
		                    }
		                    var se = document.getElementById("sess").value;
		                    if (parseInt(se) > 0) {
		                        if (!isNaN(document.getElementById("cc").value)) {
		                            document.getElementById("cc").value = 0;
		                        }
		                        document.getElementById("cc").value = parseFloat(document.getElementById("cc").value) + parseFloat((eval(tot) * eval(document.getElementById("credit").value)) / 100);
		                    }
		                    document.getElementById('Labeltax').innerHTML = 'GST of Rs:' + roundNumber(document.getElementById("stax").value);
		                    //pax = pax + parseInt(document.getElementById("txtNoAWFNoOfAdults").value);
		                }
		                else {
		                    document.getElementById("trAWF").style.display.none;
		                    document.getElementById("txtNoAWFNoOfAdults").value = 0;
		                }
		            }
		            if (document.getElementById("trCWF")) {
		                var h = document.getElementById("lblCWFNACfare").firstChild.data;

		                if ((!isNaN(parseInt(h))) && (parseInt(h) > 0)) {
		                    document.getElementById("trCWF").style.display.block;
		                    document.getElementById("lblCWFfareChild").firstChild.data = parseInt(document.getElementById("lblCWFNACfare").firstChild.data);
		                    document.getElementById("lblCWFCalcChild").firstChild.data = (parseInt(document.getElementById("lblCWFNACfare").firstChild.data) * eval(document.getElementById("txtNoCWFNoOfChilds").value));
		                    document.getElementById("lblTotal").firstChild.data = parseInt(document.getElementById("lblTotal").firstChild.data) + parseInt(document.getElementById("lblCWFCalcChild").firstChild.data);
		                    var prevSt = document.getElementById("stax").value;
		                    if (!isNaN(prevSt)) {
		                        prevSt = 0;
		                    }
		                    document.getElementById("stax").value = parseFloat(prevSt) + parseFloat((eval(document.getElementById("lblTotal").firstChild.data) * eval(document.getElementById("service").value)) / 100);

		                    if (parseFloat(document.getElementById("stax").value) > 0) {
		                        tot = parseFloat(document.getElementById("lblTotal").firstChild.data) + parseFloat(document.getElementById("stax").value);
		                    }
		                    var se = document.getElementById("sess").value;
		                    if (parseInt(se) > 0) {
		                        if (!isNaN(document.getElementById("cc").value)) {
		                            document.getElementById("cc").value = 0;
		                        }
		                        document.getElementById("cc").value = parseFloat(document.getElementById("cc").value) + parseFloat((eval(tot) * eval(document.getElementById("credit").value)) / 100);
		                    }
		                    document.getElementById('Labeltax').innerHTML = 'GST of Rs:' + roundNumber(document.getElementById("stax").value);
		                    //pax = pax + parseInt(document.getElementById("txtNoCWFNoOfChilds").value);
		                }
		                else {
		                    document.getElementById("trCWF").style.display.none;
		                    document.getElementById("txtNoCWFNoOfChilds").value = 0;
		                }
		            }
		            
    		     
                    if(document.getElementById("tra2f"))
                    {
                        var u = document.getElementById("lblA2NACfare").firstChild.data;    		
		                if((!isNaN(parseInt(u))) &&(parseInt(u)>0))
		                {
		                 document.getElementById("tra2f").style.display.block;
	                    document.getElementById("lblFareAdultsTwin").firstChild.data = parseInt(document.getElementById("lblA2NACfare").firstChild.data);
	                    document.getElementById("lblCalcAdultsTwin").firstChild.data=(parseInt(document.getElementById("lblA2NACfare").firstChild.data) * eval(document.getElementById("txtNoOfAdultsTwin").value));
	                    document.getElementById("lblTotal").firstChild.data= parseInt(document.getElementById("lblTotal").firstChild.data) +  parseInt(document.getElementById("lblCalcAdultsTwin").firstChild.data);
	                    var prevSt =  document.getElementById("stax").value;
    				        if(!isNaN(prevSt)) 
    				        {
    				            prevSt = 0;
    				        }
    				        document.getElementById("stax").value = parseFloat(prevSt) + parseFloat((eval(document.getElementById("lblTotal").firstChild.data)*eval(document.getElementById("service").value))/100);
    				           				       
    				        if(parseFloat(document.getElementById("stax").value)>0)
    				        {   				       
    				        tot=parseFloat(document.getElementById("lblTotal").firstChild.data) + parseFloat(document.getElementById("stax").value);
    				        }
    				        var se=document.getElementById("sess").value;    				        
    				        if(parseInt(se)>0)
    				        {
    				         if(!isNaN(document.getElementById("cc").value))
    				        {
    				        document.getElementById("cc").value=0;
    				        }    				        
    				        document.getElementById("cc").value= parseFloat(document.getElementById("cc").value)+ parseFloat((eval(tot)*eval(document.getElementById("credit").value))/100);
    				        }
    				        document.getElementById('Labeltax').innerHTML = 'GST of Rs:'+ Math.round(document.getElementById("stax").value) +' Extra'   
                            //pax=pax+parseInt(document.getElementById("txtNoOfAdultsTwin").value);
	                    }
	                     else
    		            {
    		                document.getElementById("tra2f").style.display.none;
    		                document.getElementById("txtNoOfAdultsTwin").value=0;
    		            }
	                }
	           
    		    
	                if(document.getElementById("tra3f"))
	                {
	                    var v = document.getElementById("lblA3NACfare").firstChild.data;    		
		                if((!isNaN(parseInt(v))) &&(parseInt(v)>0))
		                { 
		                document.getElementById("tra3f").style.display.block;
	                    document.getElementById("lblFareAdultsTriple").firstChild.data = parseInt(document.getElementById("lblA3NACfare").firstChild.data);
	                    document.getElementById("lblCalcAdultsTriple").firstChild.data=(parseInt(document.getElementById("lblA3NACfare").firstChild.data) * eval(document.getElementById("txtNoOfAdultsTriple").value));
	                    document.getElementById("lblTotal").firstChild.data= parseInt(document.getElementById("lblTotal").firstChild.data) +  parseInt(document.getElementById("lblCalcAdultsTriple").firstChild.data);
	                    var prevSt =  document.getElementById("stax").value;
    				        if(!isNaN(prevSt)) 
    				        {
    				            prevSt = 0;
    				        }
    				        document.getElementById("stax").value = parseFloat(prevSt) + parseFloat((eval(document.getElementById("lblTotal").firstChild.data)*eval(document.getElementById("service").value))/100);
    				           				       
    				        if(parseFloat(document.getElementById("stax").value)>0)
    				        {   				       
    				        tot=parseFloat(document.getElementById("lblTotal").firstChild.data) + parseFloat(document.getElementById("stax").value);
    				        }
    				        var se=document.getElementById("sess").value;    				        
    				        if(parseInt(se)>0)
    				        { 
    				         if(!isNaN(document.getElementById("cc").value))
    				        {
    				        document.getElementById("cc").value=0;
    				        }   				        
    				        document.getElementById("cc").value= parseFloat(document.getElementById("cc").value)+ parseFloat((eval(tot)*eval(document.getElementById("credit").value))/100);
    				        }
    				        document.getElementById('Labeltax').innerHTML = 'GST of Rs:'+ Math.round(document.getElementById("stax").value) +' Extra'     
                            //pax=pax+parseInt(document.getElementById("txtNoOfAdultsTriple").value);
	                    }
	                    else
    		            {
    		                document.getElementById("tra3f").style.display.none;
    		                document.getElementById("txtNoOfAdultsTriple").value=0;
    		            }
	                }
	            
    		    
	                if(document.getElementById("trcbf"))
	                {
	                    var w = document.getElementById("lblCBNACfare").firstChild.data;    		
		                if((!isNaN(parseInt(w))) &&(parseInt(w)>0))
		                { 
		                 document.getElementById("trcbf").style.display.block;
                        document.getElementById("lblFareChildBed").firstChild.data = parseInt(document.getElementById("lblCBNACfare").firstChild.data);
	                    document.getElementById("lblCalcChildBed").firstChild.data=(parseInt(document.getElementById("lblCBNACfare").firstChild.data) * eval(document.getElementById("txtNoOfChildBed").value));
	                    document.getElementById("lblTotal").firstChild.data= parseInt(document.getElementById("lblTotal").firstChild.data) +  parseInt(document.getElementById("lblCalcChildBed").firstChild.data);
	                    var prevSt =  document.getElementById("stax").value;
    				        if(!isNaN(prevSt)) 
    				        {
    				            prevSt = 0;
    				        }
    				        document.getElementById("stax").value = parseFloat(prevSt) + parseFloat((eval(document.getElementById("lblTotal").firstChild.data)*eval(document.getElementById("service").value))/100);
    				           				       
    				        if(parseFloat(document.getElementById("stax").value)>0)
    				        {   				       
    				        tot=parseFloat(document.getElementById("lblTotal").firstChild.data) + parseFloat(document.getElementById("stax").value);
    				        }
    				        var se=document.getElementById("sess").value;    				        
    				        if(parseInt(se)>0)
    				        {
    				         if(!isNaN(document.getElementById("cc").value))
    				        {
    				        document.getElementById("cc").value=0;
    				        }    				        
    				        document.getElementById("cc").value= parseFloat(document.getElementById("cc").value)+ parseFloat((eval(tot)*eval(document.getElementById("credit").value))/100);
    				        }
    				        document.getElementById('Labeltax').innerHTML = 'GST of Rs:'+ Math.round(document.getElementById("stax").value) +' Extra'     
                           // pax=pax+parseInt(document.getElementById("txtNoOfChildBed").value);
	                    }
	                    else
    		            {
    		                document.getElementById("trcbf").style.display.none;
    		                document.getElementById("txtNoOfChildBed").value=0;
    		            }
	                }
	            
    		    
	                if(document.getElementById("trsf"))
	                {
	                    var x = document.getElementById("lblSNACfare").firstChild.data;    		
		                if((!isNaN(parseInt(x))) &&(parseInt(x)>0))
		                {
		                 document.getElementById("trsf").style.display.block; 
                        document.getElementById("lblFareSingles").firstChild.data = parseInt(document.getElementById("lblSNACfare").firstChild.data);
	                    document.getElementById("lblCalcSingles").firstChild.data=(parseInt(document.getElementById("lblSNACfare").firstChild.data) * eval(document.getElementById("txtNoOfSingles").value));
	                    document.getElementById("lblTotal").firstChild.data= parseInt(document.getElementById("lblTotal").firstChild.data) +  parseInt(document.getElementById("lblCalcSingles").firstChild.data);
	                    var prevSt =  document.getElementById("stax").value;
    				        if(!isNaN(prevSt)) 
    				        {
    				            prevSt = 0;
    				        }
    				        document.getElementById("stax").value = parseFloat(prevSt) + parseFloat((eval(document.getElementById("lblTotal").firstChild.data)*eval(document.getElementById("service").value))/100);
    				           				       
    				        if(parseFloat(document.getElementById("stax").value)>0)
    				        {   				       
    				        tot=parseFloat(document.getElementById("lblTotal").firstChild.data) + parseFloat(document.getElementById("stax").value);
    				        }
    				        var se=document.getElementById("sess").value;    				        
    				        if(parseInt(se)>0)
    				        {
    				            if(!isNaN(document.getElementById("cc").value))
    				            {
    				            document.getElementById("cc").value=0;
    				            }    				        
    				        document.getElementById("cc").value= parseFloat(document.getElementById("cc").value)+ parseFloat((eval(tot)*eval(document.getElementById("credit").value))/100);
    				        }
    				        document.getElementById('Labeltax').innerHTML = 'GST of Rs:'+ Math.round(document.getElementById("stax").value) +' Extra'     
                           // pax=pax+parseInt(document.getElementById("txtNoOfSingles").value);
	                    }
	                    else
    		            {
    		                document.getElementById("trsf").style.display.none;
    		                document.getElementById("txtNoOfSingles").value=0;
    		            } 
	                }
	                
	                if(document.getElementById("tradf"))
	                {
	                    var x1 = document.getElementById("lbldNACfare").firstChild.data;    		
		                if((!isNaN(parseInt(x1))) &&(parseInt(x1)>0))
		                {
		                 document.getElementById("tradf").style.display.block; 
		                 
                        document.getElementById("lblFaredormitory").firstChild.data = parseInt(document.getElementById("lbldNACfare").firstChild.data);
	                    document.getElementById("lblCalcdormitory").firstChild.data=(parseInt(document.getElementById("lbldNACfare").firstChild.data) * eval(document.getElementById("txtNoofdormitory").value));
	                    document.getElementById("lblTotal").firstChild.data= parseInt(document.getElementById("lblTotal").firstChild.data) +  parseInt(document.getElementById("lblCalcdormitory").firstChild.data);
	                    var prevSt =  document.getElementById("stax").value;
    				        if(!isNaN(prevSt)) 
    				        {
    				            prevSt = 0;
    				        }
    				        document.getElementById("stax").value = parseFloat(prevSt) + parseFloat((eval(document.getElementById("lblTotal").firstChild.data)*eval(document.getElementById("service").value))/100);
    				           				       
    				        if(parseFloat(document.getElementById("stax").value)>0)
    				        {   				       
    				        tot=parseFloat(document.getElementById("lblTotal").firstChild.data) + parseFloat(document.getElementById("stax").value);
    				        }
    				        var se=document.getElementById("sess").value;    				        
    				        if(parseInt(se)>0)
    				        {
    				            if(!isNaN(document.getElementById("cc").value))
    				            {
    				            document.getElementById("cc").value=0;
    				            }    				        
    				        document.getElementById("cc").value= parseFloat(document.getElementById("cc").value)+ parseFloat((eval(tot)*eval(document.getElementById("credit").value))/100);
    				        }
    				        document.getElementById('Labeltax').innerHTML = 'GST of Rs:'+ Math.round(document.getElementById("stax").value) +' Extra'     
                           // pax=pax+parseInt(document.getElementById("txtNoOfSingles").value);
	                    }
	                    else
    		            {
    		                document.getElementById("tradf").style.display.none;
    		                document.getElementById("txtNoofdormitory").value=0;
    		            } 
	                }
	                
	                	            
           }
            document.getElementById('hdAdults').value = Adultpax;
           document.getElementById('hdChild').value = Childpax;
           
           var lAPaxNo=0;var lCPaxNo=0;var lServiceChargeTotal=0;
           lAPaxNo = document.getElementById('hdAdults').value ;
           lCPaxNo= document.getElementById('hdChild').value ;
          
           if(document.getElementById("hdAServiceChargeFare").value!="" && parseFloat(document.getElementById("hdAServiceChargeFare").value)>0)
           {
                var lAAmt=parseFloat(document.getElementById("hdAServiceChargeFare").value) * parseFloat(lAPaxNo) ;    
                var lCAmt=parseFloat(document.getElementById("hdCServiceChargeFare").value) * parseFloat(lCPaxNo) ;    
               
                var lServiceChargeTax= ((parseFloat(lAAmt) + parseFloat(lCAmt))* parseFloat(document.getElementById('hdServiceChargeTax').value))/100;
               
                lServiceChargeTotal=parseFloat(lAAmt) + parseFloat(lCAmt)   + parseFloat(lServiceChargeTax);
                //alert(lServiceChargeTotal);
                document.getElementById('lblServiceCharge').innerHTML = 'Service Charge of Rs:'+ Math.round(lServiceChargeTotal);
           }  
            
           document.getElementById("stax").value =Math.round(document.getElementById("stax").value);
           document.getElementById("cc").value=Math.round(document.getElementById("cc").value);
           
           document.getElementById('Labeltax').innerHTML = 'GST of Rs:'+ Math.round(document.getElementById("stax").value);
           
         

           document.getElementById('maxSeatAllowed').value = pax;
           return chkfield();
		}
		
		function checkAvaildateAC()
		{
			if (document.getElementById('RadAC').checked) 
			{
			var seatAC;
			seatAC= "sajAC" + parseInt(document.getElementById('ddlJdate').selectedIndex); 
			var seatavailAC;
			seatavailAC=eval("document.Form1." + seatAC + ".value");
			var pax;
			pax=parseInt(document.getElementById('txt2').value) + parseInt(document.getElementById('txt3').value); 
				if (parseInt(pax)>parseInt(seatavailAC))
				{
				alert("There are only "+ seatavailAC +" seats available on the date " + document.getElementById('Deptdate').value +".")
				return false;
				}
			}
		}

		function checkAvaildateNAC()
		{
			if (document.getElementById('RadNAC').checked) 
			{
				var seatNAC;
				seatNAC= "sajNAC" + parseInt(document.getElementById('ddlJdate').selectedIndex)
				var seatavailNAC;
				seatavailNAC=eval("document.Form1." + seatNAC + ".value");
				var pax;
				pax=parseInt(document.getElementById('txt2').value) + parseInt(document.getElementById('txt3').value); 
				if (parseInt(pax)>parseInt(seatavailAC))
				{
					alert("There are only "+ seatavailAC +" seats available on the date " + document.getElementById('Deptdate').value +".")
					return false;
				}
			}
		}		

function pausecomp(millis) 
{
    var date = new Date();
    var curDate = null;

    do { curDate = new Date(); } 
    while(curDate-date < millis);
} 

function daydiff(first, second) {
    return (second-first)/(1000*60*60*24)
}
  var jdate ='';
	function Getfare()
    {		
                
			 	var  CtlTable8 = document.getElementById('table8');                    
			    CtlTable8.style.display.none;     
			    
                if(document.getElementById('ddlJdate').value!=null)
                {   
              
                   if( document.getElementById('ddlJdate').selectedIndex==0)
                    {
                        return ;
                    }
				    jdate  = document.getElementById("ddlJdate").value
				 
				   
				}
				
				xmlHttp=GetXmlHttpObject()
				
				if (xmlHttp==null)
				{
					alert ("Browser does not support HTTP Request")
					return
				}							
				//var url="AgentTourBooking.aspx";				
				var url="AgentTourBooking.aspx";
				url=url+"?jdate="+ escape(jdate) ;				
				url=url+"&tourid="+ escape(document.getElementById('hidTourId').value) ;				
				var prevChild = 0;
			    if(document.getElementById('prevchi').value.length>0)
			    {
			        prevChild = parseInt(document.getElementById('prevchi').value);
			    }			 
			    var prevAdult = 0;
			    if(document.getElementById('prevadu').value.length>0)
			    {
			        prevAdult = parseInt(document.getElementById('prevadu').value);
			    }	
				if((prevAdult>0)||(prevChild>0))
				{				
				    url=url+"&A="+ escape(document.getElementById('prevadu').value) ;
				    url=url+"&C="+ escape(document.getElementById('prevchi').value) ;
				}
				if((document.getElementById('order').value!="0")||(document.getElementById('order').value!='')||(document.getElementById('order').value!=null))
				{				
				    url=url+"&orderid="+ escape(document.getElementById('order').value) ;
				}
				var img = document.getElementById('imgWait');
				img.style.display="block";
				var pid = document.getElementById('ddlPickUp').selectedIndex;
				url=url+"&pickUpIndex="+pid;
				if(ltC_Check()=='true')
				{
				    url=url+"&ltc=true";
				}
				url=url+"&sid="+Math.random();									
				xmlHttp.onreadystatechange=stateChanged1
				xmlHttp.open("GET",url,true)
				xmlHttp.send(null)				
			}
			function stateChanged1() 
			{ 
				if( (xmlHttp.readyState == 4 || xmlHttp.readyState=="complete"))
                {
                    debugger;
                   document.body.innerHTML = xmlHttp.responseText;                                                  
                   document.getElementById("ddlJdate").value=jdate;
                    var  CtlTable8 = document.getElementById('table8');                    
                   CtlTable8.style.display.block;  
                   
                    var today = new Date();
				    var dd = today.getDate();
                    var mm = today.getMonth()+1; 
                    var yyyy = today.getFullYear();
                    var Tday = mm+'/'+dd+'/'+yyyy;
                    var DaysDiff = daydiff(new Date(Tday) ,new Date(jdate));
				   document.getElementById('hdNoofDays').value =DaysDiff;
				   document.getElementById('rbtnPaymentOption').style.display="none"; 
				   if( parseInt(DaysDiff)>14)
				   {     
				        document.getElementById('rbtnPaymentOption').style.display="block";  
				   }
				
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
			
	     function roundNumber(num)
	     {	 
	        var result = Math.round(num*Math.pow(10,2))/Math.pow(10,2);
	        return result;
         }
         
        function chkNumeric(evt)
	    {
//		    if(event.shiftKey) return false;
// 		    if((event.keyCode<48 || event.keyCode>57) && event.keyCode != 190 && event.keyCode != 8 && event.keyCode != 37 && event.keyCode != 38 && event.keyCode != 39 && event.keyCode != 40 && event.keyCode != 46 && event.keyCode != 13 && event.keyCode != 116 && event.keyCode != 16 && (event.keyCode <96 || event.keyCode > 105) && event.keyCode != 9 && event.keyCode != 110) event.returnValue = false
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
           return true;
	    } 
//      function isNumberKey(evt)
//         {
//            var charCode = (evt.which) ? evt.which : event.keyCode
//            if (charCode > 31 && (charCode < 48 || charCode > 57))
//                return false;
//           return true;
//        }
        function ltC_Check()
        {
             var myQuery = new QueryString();
            // Read query string from browser into the new QueryString object, name myQuery
            myQuery.read();
            // Check the status, to make sure it read the query string. Then write out the query string arguments		
            if(myQuery.getStatus())
            {
	            var aQueryData = myQuery.getAll();	      
	            for(var n in aQueryData)
	            {   
		            if(aQueryData[n]=='true')
		            {
		                return 'true';
		            }
	            }
            }
            return 'false';
        }
        