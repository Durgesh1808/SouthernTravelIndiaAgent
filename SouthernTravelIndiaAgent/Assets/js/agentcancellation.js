function framePrint(whichFrame)
{
    //parent[whichFrame].focus();
    go(whichFrame);
}
function go(whichFrame)
{

var a = window.open('','','scrollbars=no');

        a.document.open("text/html");        
        var strDoc = document.getElementById(whichFrame).innerHTML;                
        a.document.write(strDoc); 
        a.document.close();
        a.print();
}
    function CheckMail(str) 
		{
			if (str.search(/^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/) != -1)
            {
                return true;
            }
            else
            {
                alert("Invalid E-mail ID");
                return false;
            }					
	    }
    function validation()
    {  
		if(document.form1.txtpNRo.value=="")
		{
		        alert("Enter your PNR No:");
		        document.form1.txtpNRo.focus();
		        return false;
		}
		
		if (document.form1.txtemail.value=="")
		    {
		        alert("Enter your Email-ID or Mobile No");
		        document.form1.txtemail.focus();
		        return false;
		    }
	    else
		{
		if (isNaN(document.form1.txtemail.value)==true)
			{			
		        if ((CheckMail(document.form1.txtemail.value) )== false)
			        {        				
				        document.form1.txtemail.value="";
				        document.form1.txtemail.focus();
				        return false;
			        }
			 }
		else
			{
			    var a=document.form1.txtemail.value;
			    if((a.length<10)|(a.length>11))
			    {
			        alert("Invalid Mobile No")
			        document.form1.txtemail.value="";
	                document.form1.txtemail.focus();
	                return false;
			    }
			}						
		}
		if(document.form1.txtticketno.value=="")
		{
	        alert("Enter your Ticket No");
	        document.form1.txtticketno.focus();
	        return false;
	    }
	    if (document.form1.txtjdate.value == "") {
	        alert("Select Journey date.");
	        document.form1.txtjdate.focus();
	        return false;
	    }
		if(!(document.getElementById("fullcancellation").checked)&&(!(document.getElementById("PartialCancellation").checked)))
		{
		alert('Please select Either Full / Partial Cancellation');
		return false;
		}
		
    }
//    function checkradio()
//		{
//		var chek1 = true;			
//		var tot=0;
//		var adult=0;
//		var child=0;		    
//			if(document.getElementById("trAC").value!='0')
//			{		
//		        if (document.getElementById("txtNoOfAdults").value!=0)
//		        {
//		            tot=tot+eval(document.getElementById("txtNoOfAdults").value);
//		            adult=adult+eval(document.getElementById("txtNoOfAdults").value);			    
//		        }		        
//		    }
////		    if(document.getElementById("trNONAC").visible==true)
////		    {
//		        if(document.getElementById("trNONAC").value!='0')
//		        {
//			        if(document.getElementById("txtNoOfChilds").value!=0)
//			        {
//			            tot=tot+eval(document.getElementById("txtNoOfChilds").value);
//			            child=child+eval(document.getElementById("txtNoOfChilds").value);
//			        }			   
//		        }
////		    }
//		    if(document.getElementById("tr2AC").value!='0')
//		    {
//			    if(document.getElementById("txtNoOfAdultsTwin").value!=0)
//			    {
//			        tot=tot+eval(document.getElementById("txtNoOfAdultsTwin").value);
//			        adult=adult+eval(document.getElementById("txtNoOfAdultsTwin").value);					
//			    }			   
//			}
//			if(document.getElementById("tr3AC").value!='0')
//			{
//			    if(document.getElementById("txtNoOfAdultsTriple").value!=0)
//			    {
//			        tot=tot+eval(document.getElementById("txtNoOfAdultsTriple").value);	
//			         adult=adult+eval(document.getElementById("txtNoOfAdultsTriple").value);		
//			    }			   
//			}
////			if(document.getElementById("trcbAC").visible==true)
////			{
//			    if(document.getElementById("trcbAC").value!='0')
//			    {
//		            if(document.getElementById("txtNoOfChildBed").value!=0)
//		            {
//		                tot=tot+eval(document.getElementById("txtNoOfChildBed").value);
//		                child=child+eval(document.getElementById("txtNoOfChildBed").value);			
//		            }		        
//		        }
////		    }
//		    if(document.getElementById("trsAC").value!='0')
//		    {
//			    if(document.getElementById("txtNoOfSingles").value!=0)
//			    {
//			        tot=tot+eval(document.getElementById("txtNoOfSingles").value);
//			        adult=adult+eval(document.getElementById("txtNoOfSingles").value);				
//			    }			    
//			}	
//			if(tot==0)
//			{	
//			alert("Please Enter No of Passengers");	
//			chek1=false;
//			return false;
//			}
//			if(adult==0)
//			{
//			alert("Please Enter At Least 1 Adult")
//			chek1=false;
//			return false;
//			}				
//			
//			if(document.getElementById('rpax').value.length>0)	
//			{		   
//			    if((adult+child)!=parseInt(document.getElementById('rpax').value))	
//			    {			     
//			        alert("Please Enter the Correct No of Passengers...")
//			        chek1=false;
//			        return false;
//			    }
//			}
//			else
//			{
//		        alert("Please Select the Cancelled Passengers")
//		        chek1=false;
//		        return false;
//			}
//			
//			if(validateruntime()==false)
//			{
//			   // alert("Please Enter the Passengers Details");
//			   chek1=false;
//			    return false;
//			}
//			if(chek1)
//              {
//	            document.getElementById('btnPartialTicket').style.display='none';
//	            		          
//	          }
//		    return chek1;
//			
//		}

    function checkradio() {
        	
		var tot=0;
		var adult=0;
		var child=0;		   
			if(document.getElementById("trAC").value!='0')
			{		
			    if(document.getElementById("txtNoOfAdults")!=null)
			    {
		            if (document.getElementById("txtNoOfAdults").value!=0)
		            {
		                tot=tot+parseInt(document.getElementById("txtNoOfAdults").value);
		                adult=adult+parseInt(document.getElementById("txtNoOfAdults").value);			    
		            }	
		        }	        
		    }
//		    if(document.getElementById("trNONAC").visible==true)
//		    {
		        if(document.getElementById("trNONAC").value!='0')
		        {
		            if(document.getElementById("txtNoOfChilds")!=null)
		            {
			            if(document.getElementById("txtNoOfChilds").value!=0)
			            {
			                tot=tot+parseInt(document.getElementById("txtNoOfChilds").value);
			                child=child+parseInt(document.getElementById("txtNoOfChilds").value);
			            }	
			        }		   
		        }
		        //		    }
		        if (document.getElementById("trAC").value != '0') {
		            if (document.getElementById("txtNoOfAdultWF") != null) {
		                if (document.getElementById("txtNoOfAdultWF").value != 0) {
		                    tot = tot + parseInt(document.getElementById("txtNoOfAdultWF").value);
		                    adult = adult + parseInt(document.getElementById("txtNoOfAdultWF").value);
		                }
		            }
		        }
		        if (document.getElementById("trNONAC").value != '0') {
		            if (document.getElementById("txtNoOfChildWF") != null) {
		                if (document.getElementById("txtNoOfChildWF").value != 0) {
		                    tot = tot + parseInt(document.getElementById("txtNoOfChildWF").value);
		                    child = child + parseInt(document.getElementById("txtNoOfChildWF").value);
		                }
		            }
		        }
		    if(document.getElementById("tr2AC").value!='0')
		    {
			    if(document.getElementById("txtNoOfAdultsTwin")!=null)
			    {
			        if(document.getElementById("txtNoOfAdultsTwin").value!=0)
			        {
			            tot=tot+parseInt(document.getElementById("txtNoOfAdultsTwin").value);
			            adult=adult+parseInt(document.getElementById("txtNoOfAdultsTwin").value);					
			        }
			    }			   
			}
			if(document.getElementById("tr3AC").value!='0')
			{
			    if(document.getElementById("txtNoOfAdultsTriple")!=null)
			    {
			        if(document.getElementById("txtNoOfAdultsTriple").value!=0)
			        {
			            tot=tot+parseInt(document.getElementById("txtNoOfAdultsTriple").value);	
			             adult=adult+parseInt(document.getElementById("txtNoOfAdultsTriple").value);		
			        }
			    }			   
			}
//			if(document.getElementById("trcbAC").visible==true)
//			{
			    if(document.getElementById("trcbAC").value!='0')
			    {
			        if(document.getElementById("txtNoOfChildBed")!=null)
			        {
		                if(document.getElementById("txtNoOfChildBed").value!=0)
		                {
		                    tot=tot+parseInt(document.getElementById("txtNoOfChildBed").value);
		                    child=child+parseInt(document.getElementById("txtNoOfChildBed").value);			
		                }
		            }		        
		        }
//		    }
		    if(document.getElementById("trsAC").value!='0')
		    {
		        if(document.getElementById("txtNoOfSingles")!=null)
		        {
			        if(document.getElementById("txtNoOfSingles").value!=0)
			        {
			            tot=tot+parseInt(document.getElementById("txtNoOfSingles").value);
			            adult=adult+parseInt(document.getElementById("txtNoOfSingles").value);				
			        }	
			    }		    
			}	
			
			if(document.getElementById("trDAC").value!='0')
		    {
		        if(document.getElementById("txtNoOfDormitory")!=null)
		        {
			        if(document.getElementById("txtNoOfDormitory").value!=0)
			        {
			            tot=tot+parseInt(document.getElementById("txtNoOfDormitory").value);
			            adult=adult+parseInt(document.getElementById("txtNoOfDormitory").value);				
			        }	
			    }		    
			}	
			if(document.getElementById('canc').value == 0)
	        {
	            alert("Please select The Cancelled Passenger.");
	            return false;
	        }
			if(tot==0)
			{	
			alert("Please Enter No of Passengers");		
			return false;
			}
			if(adult==0)
			{
			alert("Please Enter At Least 1 Adult")
			return false;
			}				
			
			if(document.getElementById('rpax').value.length>0)	
			{		   
			    if((adult+child)!=parseInt(document.getElementById('rpax').value))	
			    {			     
			        alert("Please Enter the Correct No of Passengers...")
			        return false;
			    }
			}
			else
			{
			 alert("Please select the passenger(s) whom you want to cancel")
			        return false;
			}
			
			//for correct no o fadults and childs
			var prevChild = 0;
			if(document.getElementById('totchi').value.length>0)
			    prevChild = parseInt(document.getElementById('totchi').value);			 
			var prevAdult = 0;
			if(document.getElementById('totadu').value.length>0)
			    prevAdult = parseInt(document.getElementById('totadu').value);

			var ca, catw, catr, cas, cc, ccwb, cad, cAdlF, cChlF;
			ca=parseInt(document.getElementById('canadul').value);
            cc=parseInt(document.getElementById('canchil').value);
            catw=parseInt(document.getElementById('cantwin').value);
            catr=parseInt(document.getElementById('cantrip').value);
            ccwb=parseInt(document.getElementById('canwith').value);
            cas=parseInt(document.getElementById('cansing').value); 
            cad=parseInt(document.getElementById('candorm').value);
            cAdlF = parseInt(document.getElementById('canaduF').value);
            cChlF = parseInt(document.getElementById('canchiF').value);
			if((prevAdult>0 ) || (prevChild>0))
			{			   
//			    if((child)>(cc+ccwb))	
//			    {			     
//			        alert("Please Enter the Correct No of Childs")
//			        return false;
//			    }
			    if (prevChild != (child + cc + ccwb + cChlF))
			    {
			        alert("Please Enter the Correct No of Childs")
			        return false;
			    }

			    if (prevAdult != (adult + ca + catw + catr + cas + cad + cAdlF))
			    {
			        alert("Please Enter the Correct No of Adults")
			        return false;
			    }			   
			}
			//--------end------------------------
			if (checkseats() == false) {
			    return false;
			}
			if(validateruntime()==false)
			{
			   // alert("Please Enter the Passengers Details");
			    return false;
			}
			document.getElementById('btnPartialTicket').style.display='none';
			return true;
			
		}
    function radio()
    {
         if(document.getElementById("fullcancellation").checked)
		    {
		     //document.getElementById("Cancelledpax").value="";
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
			    if (Trim(document.getElementById('txtNoOfAdultWF').value) != null) {
			        if (validateOnlyNumber1(document.getElementById('txtNoOfAdultWF').value) == false) {
			            alert("No. of persons must be a numeric value")
			            document.getElementById('txtNoOfAdultWF').value = 0;
			            document.getElementById('txtNoOfAdultWF').focus();
			            return false;
			        }
			    }
			}
			if (document.getElementById("trCWF")) {
			    if (Trim(document.getElementById('txtNoOfChildWF').value) != null) {
			        if (validateOnlyNumber1(document.getElementById('txtNoOfChildWF').value) == false) {
			            alert("No. of persons must be a numeric value")
			            document.getElementById('txtNoOfChildWF').value = 0;
			            document.getElementById('txtNoOfChildWF').focus();
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
		        if (Trim(document.getElementById('txtNoOfDormitory').value) != null)
		        {
			        if (validateOnlyNumber1(document.getElementById('txtNoOfDormitory').value) == false)
			        {	
				        alert("No. of persons must be a numeric value")
				        document.getElementById('txtNoOfDormitory').value=0;
				        document.getElementById('txtNoOfDormitory').focus();
				        return false;
			        }
		        }
			}
			
			
		}

		function ChhkZero() {

		    var tot = document.getElementById('lblTotal').firstChild.data;
		    if (isNaN(tot)) {
		        var allTxt = document.getElementsByTagName('input');
		        for (var z = 0; z < allTxt.length; z++) {
		            //alert(allTxt[z].name.substring(0, 5));
		            if (allTxt[z].name.substring(0, 5) == "txtNo") {
		                if (Trim(allTxt[z].value) == "") {
		                    allTxt[z].value = "0";
		                }
		            }
		        }
		        document.getElementById('lblTotal').firstChild.data = "0";
		        Displayfare();
		        return;
		        //alert(allTxt.length);
		    }
		}
    function Displayfare()
		{				
		var tot=0;	
		var pax=0;		
			if (ChekNumOnly()== false)
			{
		    	return false;
			}
			document.getElementById("lblTotal").innerHTML='0';			
			if((document.getElementById('RadAC').value=='1')&&(document.getElementById('RadNAC').value=='0'))
			{
			    document.getElementById("lblTotal").innerHTML='0';			    	
			            if(document.getElementById("trAC").value!='0')
			            {
			                var a = document.getElementById("lblAACfare").value;    		
			                if((!isNaN(parseInt(a))) &&(parseInt(a)>0))
			                {
			                    if(document.getElementById("traf")!=null)
			                    {
			                        document.getElementById("traf").style.display='table-row';;
    				                document.getElementById("lblFareAdults").innerHTML=parseInt(document.getElementById("lblAACfare").value); 
    				                document.getElementById("lblCalcAdults").innerHTML=(parseInt(document.getElementById("lblAACfare").value) * parseInt(document.getElementById("txtNoOfAdults").value));
            		                document.getElementById("lblTotal").innerHTML=parseInt(document.getElementById("lblCalcAdults").innerHTML);    				       
    				                pax=pax+parseInt(document.getElementById("txtNoOfAdults").value);    
    				            }
			                }
			                 else
	                        {
	                            document.getElementById("traf").style.display='none';
	                            document.getElementById("txtNoOfAdults").value=0;
	                        }
    		            }   
//    		            if(document.getElementById("trcf").visble==true)
//		                {
    				        if(document.getElementById("trNONAC").value!='0')
    				        {
    				             var b = document.getElementById("lblCACfare").value;    		
			                    if((!isNaN(parseInt(b))) &&(parseInt(b)>0))
			                    {
			                        if(document.getElementById("trcf")!=null)
			                        {
			                            document.getElementById("trcf").style.display='table-row';;
    				                    document.getElementById("lblfareChild").innerHTML=parseInt(document.getElementById("lblCACfare").value) ;
    				                    document.getElementById("lblCalcChild").innerHTML=(parseInt(document.getElementById("lblCACfare").value) * parseInt(document.getElementById("txtNoOfChilds").value));
    				                    document.getElementById("lblTotal").innerHTML= parseInt(document.getElementById("lblTotal").innerHTML) +  parseInt(document.getElementById("lblCalcChild").innerHTML);
                                        pax=pax+parseInt(document.getElementById("txtNoOfChilds").value);
                                    }
    				            }
    				            else
    		                    {
    		                        if(document.getElementById("trcf")!=null)
			                        {
    		                            document.getElementById("trcf").style.display='none';
    		                            document.getElementById("txtNoOfChilds").value=0;
    		                        }
    		                    }
    		                }
//    		            }
    		                if (document.getElementById("trAC").value != '0') {
    		                    var b = document.getElementById("lblAWFACfare").value;
    		                    if ((!isNaN(parseInt(b))) && (parseInt(b) > 0)) {
    		                        if (document.getElementById("trAWF") != null) {
    		                            document.getElementById("trAWF").style.display = 'block';

    		                            document.getElementById("lblAdulWFoodfare").innerHTML = parseInt(document.getElementById("lblAWFACfare").value);
    		                            document.getElementById("lblCalcAWfood").innerHTML = (parseInt(document.getElementById("lblAWFACfare").value) * parseInt(document.getElementById("txtNoOfAdultWF").value));
    		                            document.getElementById("lblTotal").innerHTML = parseInt(document.getElementById("lblTotal").innerHTML) + parseInt(document.getElementById("lblCalcAWfood").innerHTML);
    		                            pax = pax + parseInt(document.getElementById("txtNoOfAdultWF").value);
    		                        }
    		                    }
    		                    else {
    		                        if (document.getElementById("trAWF") != null) {
    		                            document.getElementById("trAWF").style.display = 'none';
    		                            document.getElementById("txtNoOfAdultWF").value = 0;
    		                        }
    		                    }
    		                }
    		                if (document.getElementById("trAC").value != '0') {
    		                    var b = document.getElementById("lblCWFACfare").value;
    		                    if ((!isNaN(parseInt(b))) && (parseInt(b) > 0)) {
    		                        if (document.getElementById("trCWF") != null) {
    		                            document.getElementById("trCWF").style.display = 'block';

    		                            document.getElementById("lblChildWFoodfare").innerHTML = parseInt(document.getElementById("lblCWFACfare").value);
    		                            document.getElementById("lblCalcCWfood").innerHTML = (parseInt(document.getElementById("lblCWFACfare").value) * parseInt(document.getElementById("txtNoOfChildWF").value));
    		                            document.getElementById("lblTotal").innerHTML = parseInt(document.getElementById("lblTotal").innerHTML) + parseInt(document.getElementById("lblCalcCWfood").innerHTML);
    		                            pax = pax + parseInt(document.getElementById("txtNoOfChildWF").value);
    		                        }
    		                    }
    		                    else {
    		                        if (document.getElementById("trCWF") != null) {
    		                            document.getElementById("trCWF").style.display = 'none';
    		                            document.getElementById("txtNoOfChildWF").value = 0;
    		                        }
    		                    }
    		                }
                        if(document.getElementById("tr2AC").value!='0')
                        { 
                            var c = document.getElementById("lblA2ACfare").value;    		
			                if((!isNaN(parseInt(c))) &&(parseInt(c)>0))
			                {
			                    if(document.getElementById("tra2f")!=null)
			                    { 
			                        document.getElementById("tra2f").style.display='table-row';;      
				                    document.getElementById("lblFareAdultsTwin").innerHTML = parseInt(document.getElementById("lblA2ACfare").value);
				                    document.getElementById("lblCalcAdultsTwin").innerHTML=(parseInt(document.getElementById("lblA2ACfare").value) * parseInt(document.getElementById("txtNoOfAdultsTwin").value));
				                    document.getElementById("lblTotal").innerHTML= parseInt(document.getElementById("lblTotal").innerHTML) +  parseInt(document.getElementById("lblCalcAdultsTwin").innerHTML);
                                    pax=pax+parseInt(document.getElementById("txtNoOfAdultsTwin").value);
                                }
				            }
				            else
    		                {
    		                    document.getElementById("tra2f").style.display='none';
    		                    document.getElementById("txtNoOfAdultsTwin").value=0;
    		                }
			             }
			             
				        if(document.getElementById("tr3AC").value!='0')
				        {
				            var d = document.getElementById("lblA3ACfare").value;    		
			                if((!isNaN(parseInt(d))) &&(parseInt(d)>0))
			                {
			                    if(document.getElementById("tra3f")!=null)
			                    {
			                        document.getElementById("tra3f").style.display='table-row';;
				                    document.getElementById("lblFareAdultsTriple").innerHTML = parseInt(document.getElementById("lblA3ACfare").value);
				                    document.getElementById("lblCalcAdultsTriple").innerHTML=(parseInt(document.getElementById("lblA3ACfare").value) * parseInt(document.getElementById("txtNoOfAdultsTriple").value));
				                    document.getElementById("lblTotal").innerHTML= parseInt(document.getElementById("lblTotal").innerHTML) +  parseInt(document.getElementById("lblCalcAdultsTriple").innerHTML);
                                    pax=pax+parseInt(document.getElementById("txtNoOfAdultsTriple").value);
                                }
				            }
				            else
    		                {
    		                    document.getElementById("tra3f").style.display='none';
    		                    document.getElementById("txtNoOfAdultsTriple").value=0;
    		                }
		                }
    		    
//    		         if(document.getElementById("trcbf").visble==true)
//		                {
				            if(document.getElementById("trcbAC").value!='0')
				            {
				                var e = document.getElementById("lblCBACfare").value;    		
			                    if((!isNaN(parseInt(e))) &&(parseInt(e)>0))
			                    {
			                        if(document.getElementById("trcbf")!=null)
			                        {
			                            document.getElementById("trcbf").style.display='table-row';;
                                        document.getElementById("lblFareChildBed").innerHTML = parseInt(document.getElementById("lblCBACfare").value);
				                        document.getElementById("lblCalcChildBed").innerHTML=(parseInt(document.getElementById("lblCBACfare").value) * parseInt(document.getElementById("txtNoOfChildBed").value));
				                        document.getElementById("lblTotal").innerHTML= parseInt(document.getElementById("lblTotal").innerHTML) +  parseInt(document.getElementById("lblCalcChildBed").innerHTML);
                                        pax=pax+parseInt(document.getElementById("txtNoOfChildBed").value);
                                    }
				                }
				                else
    		                    {
    		                        if(document.getElementById("trcbf")!=null)
			                        {
    		                            document.getElementById("trcbf").style.display='none';
    		                            document.getElementById("txtNoOfChildBed").value=0;
    		                        }
    		                    }
		                    }  
//		                }  		    
    		    
				        if(document.getElementById("trsAC").value!='0')
				        {
				            var f = document.getElementById("lblSACfare").value;    		
			                if((!isNaN(parseInt(f))) &&(parseInt(f)>0))
			                {
			                    if(document.getElementById("trsf")!=null)
			                    {
			                        document.getElementById("trsf").style.display='table-row';;
                                    document.getElementById("lblFareSingles").innerHTML = parseInt(document.getElementById("lblSACfare").value);
				                    document.getElementById("lblCalcSingles").innerHTML=(parseInt(document.getElementById("lblSACfare").value) * parseInt(document.getElementById("txtNoOfSingles").value));
				                    document.getElementById("lblTotal").innerHTML= parseInt(document.getElementById("lblTotal").innerHTML) +  parseInt(document.getElementById("lblCalcSingles").innerHTML);
                                    pax=pax+parseInt(document.getElementById("txtNoOfSingles").value);
                                }
                            }
                            else
    		                {
    		                    document.getElementById("trsf").style.display='none';
    		                    document.getElementById("txtNoOfSingles").value=0;
    		                }
                        } 
                        
                        if(document.getElementById("trDAC").value!='0')
				        {
				            var f1 = document.getElementById("lblDACfare").value;    		
			                if((!isNaN(parseInt(f1))) &&(parseInt(f1)>0))
			                {
			                    if(document.getElementById("tradf")!=null)
			                    {
			                        document.getElementById("tradf").style.display='table-row';;
                                    document.getElementById("lblFareAdultsDorm").innerHTML = parseInt(document.getElementById("lblDACfare").value);
				                    document.getElementById("lblCalcAdultsDorm").innerHTML=(parseInt(document.getElementById("lblDACfare").value) * parseInt(document.getElementById("txtNoOfDormitory").value));
				                    document.getElementById("lblTotal").innerHTML= parseInt(document.getElementById("lblTotal").innerHTML) +  parseInt(document.getElementById("lblCalcAdultsDorm").innerHTML);
                                    pax=pax+parseInt(document.getElementById("txtNoOfDormitory").value);
                                }
                            }
                            else
    		                {
    		                    document.getElementById("tradf").style.display='none';
    		                    document.getElementById("txtNoOfDormitory").value=0;
    		                }
                        } 
                        
                        
                             
		        }
		
			else if((document.getElementById('RadNAC').value=='1')&&(document.getElementById('RadAC').value=='0'))
			    {
			        document.getElementById("lblTotal").innerHTML='0';
    			    			
		                    if(document.getElementById("trAC").value!='0')
		                    {
		                        var g = document.getElementById("lblANACfare").value;    		
			                    if((!isNaN(parseInt(g))) &&(parseInt(g)>0))
			                    {
			                        if(document.getElementById("traf")!=null)
			                        {
			                            document.getElementById("traf").style.display='table-row';;
				                        document.getElementById("lblFareAdults").innerHTML=parseInt(document.getElementById("lblANACfare").value); 
				                        document.getElementById("lblCalcAdults").innerHTML=(parseInt(document.getElementById("lblANACfare").value) * parseInt(document.getElementById("txtNoOfAdults").value));
				                        document.getElementById("lblTotal").innerHTML=parseInt(document.getElementById("lblCalcAdults").innerHTML);
                                        pax=pax+parseInt(document.getElementById("txtNoOfAdults").value);
                                    }
				                }
				                else
    		                    {
    		                        document.getElementById("traf").style.display='none';
    		                        document.getElementById("txtNoOfAdults").value=0;
    		                    }
				            }
            		    
//        		            if(document.getElementById("trcf").visble==true)
//		                     {
				                if(document.getElementById("trNONAC").value!='0')
				                {
				                    var h = document.getElementById("lblCNACfare").value;    		
			                        if((!isNaN(parseInt(h))) &&(parseInt(h)>0))
			                        {
			                            if(document.getElementById("trcf")!=null)
			                            {
			                                document.getElementById("trcf").style.display='table-row';;
				                            document.getElementById("lblfareChild").innerHTML=parseInt(document.getElementById("lblCNACfare").value) ;
				                            document.getElementById("lblCalcChild").innerHTML=(parseInt(document.getElementById("lblCNACfare").value) * parseInt(document.getElementById("txtNoOfChilds").value));
				                            document.getElementById("lblTotal").innerHTML= parseInt(document.getElementById("lblTotal").innerHTML) +  parseInt(document.getElementById("lblCalcChild").innerHTML);
                                            pax=pax+parseInt(document.getElementById("txtNoOfChilds").value);
                                        }
				                    }
				                     else
    		                        {    		                            
    		                            if(document.getElementById("trcf")!=null)
			                            {   
    		                                document.getElementById("trcf").style.display='none';
    		                                document.getElementById("txtNoOfChilds").value=0;
    		                            }
    		                        }				
                                }
//                             }

                                if (document.getElementById("trNONAC").value != '0') {
                                    var y = document.getElementById("lblAWFNACfare").value;
                                    if ((!isNaN(parseInt(y))) && (parseInt(y) > 0)) {
                                        if (document.getElementById("trAWF") != null) {
                                            document.getElementById("trAWF").style.display = 'block';

                                            document.getElementById("lblAdulWFoodfare").innerHTML = parseInt(document.getElementById("lblAWFNACfare").value);
                                            document.getElementById("lblCalcAWfood").innerHTML = (parseInt(document.getElementById("lblAWFNACfare").value) * parseInt(document.getElementById("txtNoOfAdultWF").value));
                                            document.getElementById("lblTotal").innerHTML = parseInt(document.getElementById("lblTotal").innerHTML) + parseInt(document.getElementById("lblCalcAWfood").innerHTML);
                                            pax = pax + parseInt(document.getElementById("txtNoOfAdultWF").value);
                                        }
                                    }
                                    else {
                                        if (document.getElementById("trAWF") != null) {
                                            document.getElementById("trAWF").style.display = 'none';
                                            document.getElementById("txtNoOfAdultWF").value = 0;
                                        }
                                    }
                                }
                                if (document.getElementById("trNONAC").value != '0') {
                                    var z = document.getElementById("lblCWFNACfare").value;
                                    if ((!isNaN(parseInt(z))) && (parseInt(z) > 0)) {
                                        if (document.getElementById("trCWF") != null) {
                                            document.getElementById("trCWF").style.display = 'block';

                                            document.getElementById("lblChildWFoodfare").innerHTML = parseInt(document.getElementById("lblCWFNACfare").value);
                                            document.getElementById("lblCalcCWfood").innerHTML = (parseInt(document.getElementById("lblCWFNACfare").value) * parseInt(document.getElementById("txtNoOfChildWF").value));
                                            document.getElementById("lblTotal").innerHTML = parseInt(document.getElementById("lblTotal").innerHTML) + parseInt(document.getElementById("lblCalcCWfood").innerHTML);
                                            pax = pax + parseInt(document.getElementById("txtNoOfChildWF").value);
                                        }
                                    }
                                    else {
                                        if (document.getElementById("trCWF") != null) {
                                            document.getElementById("trCWF").style.display = 'none';
                                            document.getElementById("txtNoOfChildWF").value = 0;
                                        }
                                    }
                                }       
                            if(document.getElementById("tr2AC").value!='0')
                            {
                                var i = document.getElementById("lblA2NACfare").value;    		
			                    if((!isNaN(parseInt(i))) &&(parseInt(i)>0))
			                    {
			                        if(document.getElementById("tra2f")!=null)
			                        {
			                            document.getElementById("tra2f").style.display='table-row';;
				                        document.getElementById("lblFareAdultsTwin").innerHTML = parseInt(document.getElementById("lblA2NACfare").value);
				                        document.getElementById("lblCalcAdultsTwin").innerHTML=(parseInt(document.getElementById("lblA2NACfare").value) * parseInt(document.getElementById("txtNoOfAdultsTwin").value));
				                        document.getElementById("lblTotal").innerHTML= parseInt(document.getElementById("lblTotal").innerHTML) +  parseInt(document.getElementById("lblCalcAdultsTwin").innerHTML);
                                        pax=pax+parseInt(document.getElementById("txtNoOfAdultsTwin").value);
                                    }
				                }
				                else
    		                    {
    		                        document.getElementById("tra2f").style.display='none';
    		                        document.getElementById("txtNoOfAdultsTwin").value=0;
    		                    }
				            }    		    
        		    
				             if(document.getElementById("tr3AC").value!='0')
				            {
				                var j = document.getElementById("lblA3NACfare").value;    		
			                    if((!isNaN(parseInt(j))) &&(parseInt(j)>0))
			                    {
			                        if(document.getElementById("tra3f")!=null)
			                        {
			                            document.getElementById("tra3f").style.display='table-row';; 
				                        document.getElementById("lblFareAdultsTriple").innerHTML = parseInt(document.getElementById("lblA3NACfare").value);
				                        document.getElementById("lblCalcAdultsTriple").innerHTML=(parseInt(document.getElementById("lblA3NACfare").value) * parseInt(document.getElementById("txtNoOfAdultsTriple").value));
				                        document.getElementById("lblTotal").innerHTML= parseInt(document.getElementById("lblTotal").innerHTML) +  parseInt(document.getElementById("lblCalcAdultsTriple").innerHTML);
                                        pax=pax+parseInt(document.getElementById("txtNoOfAdultsTriple").value);
                                    }
				                }
				                 else
    		                    {
    		                        document.getElementById("tra3f").style.display='none';
    		                        document.getElementById("txtNoOfAdultsTriple").value=0;
    		                    }
				            }   		   
//        		             if(document.getElementById("trcbf").visble==true)
//		                     {
				                 if(document.getElementById("trcbAC").value!='0')
				                {
				                    var k = document.getElementById("lblCBNACfare").value;    		
			                        if((!isNaN(parseInt(k))) &&(parseInt(k)>0))
			                        { 
			                            if(document.getElementById("trcbf")!=null)
			                            {
			                                document.getElementById("trcbf").style.display='table-row';;
                                            document.getElementById("lblFareChildBed").innerHTML = parseInt(document.getElementById("lblCBNACfare").value);
				                            document.getElementById("lblCalcChildBed").innerHTML=(parseInt(document.getElementById("lblCBNACfare").value) * parseInt(document.getElementById("txtNoOfChildBed").value));
				                            document.getElementById("lblTotal").innerHTML= parseInt(document.getElementById("lblTotal").innerHTML) +  parseInt(document.getElementById("lblCalcChildBed").innerHTML);
                                            pax=pax+parseInt(document.getElementById("txtNoOfChildBed").value);
                                        }
				                    }
				                    else
    		                        {
    		                            if(document.getElementById("trcbf")!=null)
			                            {
    		                                document.getElementById("trcbf").style.display='none';
    		                                document.getElementById("txtNoOfChildBed").value=0;
    		                            }
    		                        }
				                }
//				            }
        		    
        		     
				            if(document.getElementById("trsAC").value!='0')
				            {
				                var l = document.getElementById("lblSNACfare").value;    		
			                    if((!isNaN(parseInt(l))) &&(parseInt(l)>0))
			                    {
			                        if(document.getElementById("trsf")!=null)
			                        {
			                            document.getElementById("trsf").style.display='table-row';;
                                        document.getElementById("lblFareSingles").innerHTML = parseInt(document.getElementById("lblSNACfare").value);
				                        document.getElementById("lblCalcSingles").innerHTML=(parseInt(document.getElementById("lblSNACfare").value) * parseInt(document.getElementById("txtNoOfSingles").value));				                				       
				                        document.getElementById("lblTotal").innerHTML= parseInt(document.getElementById("lblTotal").innerHTML) +  parseInt(document.getElementById("lblCalcSingles").innerHTML);
                                        pax=pax+parseInt(document.getElementById("txtNoOfSingles").value);
                                    }
				                }
				                 else
    		                    {
    		                        document.getElementById("trsf").style.display='none';
    		                        document.getElementById("txtNoOfSingles").value=0;
    		                    }
				            }
				            
				            if(document.getElementById("trDAC").value!='0')
				            {
				                var l1 = document.getElementById("lblDNACfare").value;    		
			                    if((!isNaN(parseInt(l1))) &&(parseInt(l1)>0))
			                    {
			                        if(document.getElementById("tradf")!=null)
			                        {
			                            document.getElementById("tradf").style.display='table-row';;
                                        document.getElementById("lblFareAdultsDorm").innerHTML = parseInt(document.getElementById("lblDNACfare").value);
				                        document.getElementById("lblCalcAdultsDorm").innerHTML=(parseInt(document.getElementById("lblDNACfare").value) * parseInt(document.getElementById("txtNoOfDormitory").value));				                				       
				                        document.getElementById("lblTotal").innerHTML= parseInt(document.getElementById("lblTotal").innerHTML) +  parseInt(document.getElementById("lblCalcAdultsDorm").innerHTML);
                                        pax=pax+parseInt(document.getElementById("txtNoOfSingles").value);
                                    }
				                }
				                 else
    		                    {
    		                        document.getElementById("tradf").style.display='none';
    		                        document.getElementById("txtNoOfDormitory").value=0;
    		                    }
				            }
				            
			         }

			         document.getElementById('maxSeatAllowed').value = pax;
			         return ChhkZero();	
			 }
			 
			 
	function changeSex(num)
	    {
	       var SexCtl = document.getElementsByName('Radio'+num);
	       var TitleCtl  = document.getElementById('Title'+num);
	       if(TitleCtl.selectedIndex==2)
	        {
	            SexCtl[0].checked = true; ////check male
	        }
	        else if((TitleCtl.selectedIndex==3 )|| (TitleCtl.selectedIndex==4))
	        {
	            SexCtl[1].checked = true;  //check female
	        }
	        else
	        {
	            SexCtl[1].checked = false;  
	            SexCtl[0].checked = false;  
	        } 
	    }
	    function Left(str, n)
		{
		if (n <= 0)
			return "";
		else if (n >= String(str).length)
			return str;
		else
			return String(str).substring(0,n);
		}

	function validateruntime()
		{		
		var i,len;
		len=document.form1.length;		
			for (i=1;i<len;i++)
			{
			if(document.form1.elements[i].type=="text")
				{
					var Cname=document.form1.elements[i].name;
					if((Cname != "txtAddress") && (Cname != "txtName") && (Cname != "txtCity") && (Cname != "TxtLName") &&  (Cname != "txtPhone") && (Cname != "txtMobile") && (Cname != "txtMail") && (Cname != "txtDOB"))
					{
//						    if (Left(Cname,6)=="txtONa")
//						    {						
//							    if (document.form1.elements[i].value == "")
//							    {								
//								    alert("Passenger's name should not be null.");
//								    document.form1.elements[i].focus();
//								    return false;
//							    }	
    							
//							    if (validateOnlyNumber1(parseInt(document.form1.elements[i].value))==true)
//							    {
//								    alert("Passenger's name should not be numeric.");
//								    document.form1.elements[i].value="";
//								    document.form1.elements[i].focus();
//								    return false;
//							    }
//						    }
						
//						    if (Left(Cname,6)=="txtAge")
//						    {						
//							    if (document.form1.elements[i].value == "")
//							    {
//								    alert('Age field must have any value. It is mandatory.');
//								    document.form1.elements[i].focus();
//								    return false;
//							    }							
//							    if(document.form1.elements[i].value == 0)
//							    {								
//								    alert('Age should not be zero.');
//								    document.form1.elements[i].value =""
//								    document.form1.elements[i].focus();
//								    return false;
//							    }
//							    if (validateOnlyNumber1(parseInt(document.form1.elements[i].value))==false)
//							    {
//								    alert("Age value should be numeric.");
//								    document.form1.elements[i].value="";
//								    document.form1.elements[i].focus();
//								    return false;
//							    }							
//						    }			
					}
				}
				
				    if(document.form1.elements[i].type=="select")
				    {				
					    var Cname2=document.form1.elements[i].name;	
					    if (Left(Cname2,13)=="contact_title")
					    {						
						    if (document.form1.elements[i].value=="")
						    {
							    alert("Select any title from focused field.");
							    document.form1.elements[i].focus();
							    return false;
						    }
					    }
				    }													
			}		
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
		          document.getElementById("lblTotal").innerHTML='0';
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
		            document.getElementById("lblTotal").innerHTML='0';
		            Displayfare();		       
		         }		     
		    }
		}
		
		 var pax=0;
		 var adul=0;
         var chil=0;
         var twin=0;
         var trip=0;
         var cwith=0;
         var sing=0;
         var dorm = 0;
         var aduF = 0;
         var chiF = 0;
		function cancelpax(chkBoxObject)
		{		  
		   if(chkBoxObject.checked)   
            { 
                var paxType = chkBoxObject.id;
		        paxType =  paxType.substring(3,7);   
		        if(paxType=="Twin")
		        {
		        twin=twin+1;		        
		        }
		        else if(paxType=="Trip")
		        {
		        trip=trip+1;
		        }
		        else if(paxType=="with")
		        {
		        cwith=cwith+1;
		        }
		        else if(paxType=="Sing")
		        {
		        sing=sing+1;
		        }
		        else if(paxType=="adul")
		        {
		        adul=adul+1;
		        }
		        else if(paxType=="chil")
		        {
		        chil=chil+1;
		        }
		        else if(paxType=="Dorm")
		        {
		        dorm=dorm+1;
		        }
		        else if (paxType == "aduF") {
		            aduF = aduF + 1;
		        }
		        else if (paxType == "chiF") {
		            chiF = chiF + 1;
		        }
               pax=pax + 1;	           
               var optedStr = document.getElementById('canrowid').value;
               optedStr = optedStr.replace(",,",",");  
               if(optedStr!="")
               {
               optedStr = optedStr.replace(",,",","); 
               document.getElementById('canrowid').value = optedStr + ',' + chkBoxObject.value;               
               }
               else
               {
               optedStr = optedStr.replace(",,",","); 
               document.getElementById('canrowid').value = chkBoxObject.value;  
               }
            }
           else
            {
                var paxType = chkBoxObject.id;
		        paxType =  paxType.substring(3,7);   
		        if(paxType=="Twin")
		        {
		        twin=twin-1;		        
		        }
		        else if(paxType=="Trip")
		        {
		        trip=trip-1;
		        }
		        else if(paxType=="with")
		        {
		        cwith=cwith-1;
		        }
		        else if(paxType=="Sing")
		        {
		        sing=sing-1;
		        }
		        else if(paxType=="adul")
		        {
		        adul=adul-1;
		        }
		        else if(paxType=="chil")
		        {
		        chil=chil-1;
		        }
		        else if(paxType=="Dorm")
		        {
		        dorm=dorm-1;
		        }
		        else if (paxType == "aduF") {
		            aduF = aduF - 1;
		        }
		        else if (paxType == "chiF") {
		            chiF = chiF - 1;
		        }
                pax = pax  - 1;
                var optedStr = document.getElementById('canrowid').value;
                optedStr = optedStr.replace(chkBoxObject.value,"");
                optedStr = optedStr.replace(",,",",");                
                document.getElementById('canrowid').value = optedStr;
                var i, rr;
                var jj="";
                rr=optedStr.split(",");
                for(i=0;i<rr.length;i++)
                {
                     if(rr[i]!="")
                     {
                           if(jj!="")
                           {
                           jj=jj+","+rr[i];
                           }
                           else
                           {
                           jj=rr[i];
                           } 
                     }
                }
                document.getElementById('canrowid').value=jj;
                
            }
            document.getElementById('canadul').value=adul;
            document.getElementById('canchil').value=chil;
            document.getElementById('cantwin').value=twin;
            document.getElementById('cantrip').value=trip;
            document.getElementById('canwith').value=cwith;
            document.getElementById('cansing').value=sing;
            document.getElementById('candorm').value = dorm;
            document.getElementById('canaduF').value = aduF;
            document.getElementById('canchiF').value = chiF;     
            document.getElementById('rpax').value=(parseInt(document.getElementById('tpx').value)- parseInt(pax));
            document.getElementById("canc").value = pax;
            
		}	
function chkterms()
    {
    var chek = true;
        if(document.getElementById('chkAccept').checked==false)
        {
            alert('Please Agree With Our Terms and Conditions Before Proceeding');
            chek = false;
            return false;
        }
        if(chek)
          {
            document.getElementById('btncantic').style.display='none';	     		          
          }
	    return chek;
    }