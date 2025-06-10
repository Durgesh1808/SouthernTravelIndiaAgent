
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
   
    function chkCarvisible()
    {  
       
       if(document.getElementById('chkCar2').checked ==true)
       {
          document.getElementById('txtCar12px').readOnly=false;
          document.getElementById('txtCar12pxValue').readOnly=true;           
        }
        else
        {
            document.getElementById('txtCar12px').readOnly=true;
            document.getElementById('txtCar12px').value="";
            document.getElementById('txtCar12pxValue').value="";
            document.getElementById('txtCar12pxfare').value="";
            document.getElementById('txtCar12pxValue').readOnly=true;
            finalFare();
         }
         if(document.getElementById('chkCar3-4').checked ==true)
       {
          document.getElementById('txtCar34px').readOnly=false;
          document.getElementById('txtCar34pxValue').readOnly=true;           
        }
        else
        {
            document.getElementById('txtCar34px').readOnly=true;
            document.getElementById('txtCar34px').value="";
            document.getElementById('txtCar34pxValue').value="";
            document.getElementById('txtCar34pxfare').value="";
            document.getElementById('txtCar34pxValue').readOnly=true;
            finalFare();
         }
         
       if(document.getElementById('chkinnova4-5').checked ==true)
       {
          document.getElementById('txtInn45Px').readOnly=false;
          document.getElementById('txtInn45PxValue').readOnly=true;           
        }
        else
        {
            document.getElementById('txtInn45Px').readOnly=true;
            document.getElementById('txtInn45Px').value="";
            document.getElementById('txtInn45PxValue').value="";
            document.getElementById('txtInn45Pxfare').value="";
            document.getElementById('txtInn45PxValue').readOnly=true;
            finalFare();
         }
          if(document.getElementById('chkinnova6-7').checked ==true)
       {
          document.getElementById('txtInn67Px').readOnly=false;
          document.getElementById('txtInn67PxValue').readOnly=true;           
        }
        else
        {
            document.getElementById('txtInn67Px').readOnly=true;
            document.getElementById('txtInn67Px').value="";
            document.getElementById('txtInn67PxValue').value="";
            document.getElementById('txtInn67Pxfare').value="";
            document.getElementById('txtInn67PxValue').readOnly=true;
            finalFare();
         }
          if(document.getElementById('chkqualis4-6').checked ==true)
       {
          document.getElementById('txtQua46Px').readOnly=false;
          document.getElementById('txtQua46PxValue').readOnly=true;           
        }
        else
        {
            document.getElementById('txtQua46Px').readOnly=true;
            document.getElementById('txtQua46Px').value="";
            document.getElementById('txtQua46PxValue').value="";
            document.getElementById('txtQua46Pxfare').value="";
            document.getElementById('txtQua46PxValue').readOnly=true;
            finalFare();
         }
          if(document.getElementById('chkqualis7-8').checked ==true)
       {
          document.getElementById('txtQua78Px').readOnly=false;
          document.getElementById('txtQua78PxValue').readOnly=true;           
        }
        else
        {
            document.getElementById('txtQua78Px').readOnly=true;
            document.getElementById('txtQua78Px').value="";
            document.getElementById('txtQua78PxValue').value="";
            document.getElementById('txtQua78Pxfare').value="";
            document.getElementById('txtQua78PxValue').readOnly=true;
            finalFare();
         }
         if(document.getElementById('chktempo8-9').checked ==true)
       {
          document.getElementById('txtTem89Px').readOnly=false;
          document.getElementById('txtTem89PxValue').readOnly=true;           
        }
        else
        {
            document.getElementById('txtTem89Px').readOnly=true;
            document.getElementById('txtTem89Px').value="";
            document.getElementById('txtTem89PxValue').value="";
            document.getElementById('txtTem89Pxfare').value="";
            document.getElementById('txtTem89PxValue').readOnly=true;
            finalFare();
         }
    }
   
    var fareType='Dl';     
    function FareTypeCheck(type)
    {
    	
        fareType  = type;
        fare('txtCar12px');
        fare('txtCar34px');
        fare('txtInn45Px');
        fare('txtInn67Px');
        fare('txtQua46Px'); 
        fare('txtQua78Px');
        fare('txtTem89Px');
    }
    
    function ExtrafareCal()
    {    

        var dbSt=(document.getElementById('txtServiceTax').value);
  
         var singlePax = parseInt(document.getElementById('txtSinglePax').value);
         var tPax = parseInt(document.getElementById('txtPax').value);
         if((!isNaN(singlePax)) && (!isNaN(tPax)))
         {
         
            if(singlePax>tPax)
            {

                alert("Single pax can not be greater than Total Pax");
                document.getElementById('txtTotalExtrafare').value="";
                document.getElementById('txtExtraFareS').value="";
                document.getElementById('txtSinglePax').value="";
                return false;
            }
        }
        
       
        
         if(document.getElementById('rdoStandard').checked==true) 
         {
         
            var k=document.getElementById('txtSinglePax').value;
            if(k>0)
            {
         
              
                document.getElementById('txtExtraFareS').value=document.getElementById('SARstandard').value; 
                var s=k*parseInt(document.getElementById('SARstandard').value);
                document.getElementById('txtTotalExtrafare').value=s;
                
                 var fare= document.getElementById('txtTotalFare').value;
             
		         var allSt=(parseInt(s) + parseInt(fare));
		        
		          var st=parseFloat((allSt)*(dbSt/100));
		        
		           
		           document.getElementById('txtTotalServiceTax').value=Math.round(st*100)/100;
		        
		          
		           document.getElementById('txtFareTotal').value=(parseFloat(st) + parseInt(fare)+ parseInt(s));
		            document.getElementById('txtFareTotal').readOnly=true;
		       
                
            }
            else
            {
              
              
                document.getElementById('txtExtraFareS').value="";
                document.getElementById('txtSinglePax').value="";
                document.getElementById('txtTotalExtrafare').value="";
                var fare= document.getElementById('txtTotalFare').value;
                var allSt=(parseInt(fare));
		        var st=parseFloat((allSt)*(dbSt/100));
		        document.getElementById('txtTotalServiceTax').value=Math.round(st*100)/100;
		        document.getElementById('txtFareTotal').value=(parseFloat(st) + parseInt(fare));
		        document.getElementById('txtFareTotal').readOnly=true;
                return false;
            }
            
         }
         
         if(document.getElementById('rdoDeluxe').checked==true) 
         {
         
            var k=document.getElementById('txtSinglePax').value;
            if(k!=0)
            {
         
                document.getElementById('txtExtraFareS').value=document.getElementById('SARdeluxe').value;
                var s=k*parseInt(document.getElementById('SARdeluxe').value);
                document.getElementById('txtTotalExtrafare').value=s;
                
                
                 var fare= document.getElementById('txtTotalFare').value;
             
		         var allSt=(parseInt(s) + parseInt(fare));
		         
		        
		          var st=parseFloat((allSt)*(dbSt/100));
		           
		           document.getElementById('txtTotalServiceTax').value=Math.round(st*100)/100;
		         
		           
		           document.getElementById('txtFareTotal').value=(parseFloat(st) + parseFloat(fare)+ parseFloat(s));
		           
		           
            }
            else
            {
                
                document.getElementById('txtExtraFareS').value="";
                document.getElementById('txtSinglePax').value="";
                document.getElementById('txtTotalExtrafare').value="";
                var fare= document.getElementById('txtTotalFare').value;
                var allSt=(parseInt(fare));
		        var st=parseFloat((allSt)*(dbSt/100));
		        document.getElementById('txtTotalServiceTax').value=Math.round(st*100)/100;
		        document.getElementById('txtFareTotal').value=(parseFloat(st) + parseInt(fare));
		        document.getElementById('txtFareTotal').readOnly=true;
		        document.getElementById('SinglePax').style.display='none';
		        document.getElementById('chkSingle').checked=false;
                //Getsuggession();
                return false;
            }
            
         }
                
 }
 	    
		  
		function fare(senderId)
		       {   
		      
		       
		          var paxN0 = parseFloat(document.getElementById(senderId).value);
		          if( isNaN(paxN0))
		          {
		            return; 
		          }
		          try
		          {		             
		              var ctlName = senderId+'Hidden'+fareType;
		              var unitVal = parseFloat(document.getElementById(ctlName).value);  		              		              
		              if(parseFloat(unitVal)<=0)
		              {
		                 if(fareType=='Dl')
		                 {
                               fareType='St';
                               ctlName = senderId+'Hidden'+fareType; 
                               unitVal = parseFloat(document.getElementById(ctlName).value);
                               if(parseFloat(unitVal)>0)                               
                                    alert('Deluxe fare is not available for this vehicle.');
                               else
                               {
                                    alert('No vehicle is available for this criteria');
                                    return false;
                               }
                                    
                               document.getElementsByName('rdocategory')[1].checked =false;
                               document.getElementsByName('rdocategory')[0].checked =true;
                               return ;
                         }
                         else
                         {
                            
                                fareType='Dl';  
                                ctlName = senderId+'Hidden'+fareType; 
                                unitVal = parseFloat(document.getElementById(ctlName).value);
                                
                                if(parseFloat(unitVal)>0)
                                    alert('Standard fare is not available for this vehicle.');  		                         
                                else
                                {
                                    alert('No vehicle is available for this criteria');
                                    return false;
                                }                                    
                                
                                document.getElementsByName('rdocategory')[0].checked =false;
                                document.getElementsByName('rdocategory')[1].checked =true;
                                return ;
                         }
                         
                         
		                  var unitVal = parseFloat(document.getElementById(senderId+'Hidden' + fareType).value);  		         
		                  document.getElementById(senderId+'fare').value=unitVal;
		                  document.getElementById(senderId+'Value' ).value = parseFloat(unitVal * paxN0);
		                  document.getElementById('txtFareTotal').readOnly=true;
		                  document.getElementById('txtTotalServiceTax').readOnly=true;
		                  finalFare(); 
		                  FareTypeCheck(fareType),ExtrafareCal();                   
                         return;   
                      }  
                    
                   }   
                   catch(err)
		           {
		           
		           }		          
		              document.getElementById(senderId+'fare').value = unitVal;		         
		              document.getElementById(senderId+'Value' ).value = parseFloat(unitVal * paxN0);
		              document.getElementById('txtFareTotal').readOnly=true;
		              document.getElementById('txtTotalServiceTax').readOnly=true;
		              finalFare();        
		}
		     
		       function finalFare()
		       {		            	        
		            var allTxt  = document.getElementsByTagName('input');		           
		            var totalAmt=0;
		            var totalpax=0;
		            for(var i=0; i <allTxt.length; i++)
		            { 
		              		        
		                if( allTxt[i].type=='text')
		                {               		               
		                    if( allTxt[i].id.indexOf('Value')!=-1)
		                    {    		                
		                        var k = parseFloat( document.getElementById(allTxt[i].id).value) ;
		                        
		                        if(!isNaN(k))
		                        {
		                            totalAmt += k;
		                        }
		                       
		                    }
		                    var Cname=document.form1.elements[i].name;	
		                                       
					        if((Cname == "txtCar12px") || (Cname == "txtCar34px") || (Cname == "txtInn45Px") || (Cname == "txtInn67Px") ||  (Cname == "txtQua46Px") || (Cname == "txtQua78Px") || (Cname == "txtTem89Px"))
					        {
					        
					            var k = parseFloat(document.getElementById(allTxt[i].id).value) ;
		                        if(!isNaN(k))
		                        {
		                            totalpax += k;
		                        }
					        }
		                }
		            }
		           document.getElementById('txtTotalFare').value =  totalAmt;
		         
		           var dbSt=(document.getElementById('txtServiceTax').value);
		           var st=parseFloat((totalAmt)*(dbSt/100));
		         document.getElementById('txtTotalServiceTax').value=Math.round(st*100)/100;
		         document.getElementById('txtFareTotal').value=Math.round((st+totalAmt)*100)/100;
		        
		           document.getElementById('txtTotalPax').value =  totalpax;
		       }
		       
		      function car2(senderId)
		      {	   
		  
		        if(document.getElementById('chkCar2').checked ==true)
                {
		              if((parseInt(document.getElementById(senderId).value)%parseInt(document.getElementById('car2min').value)==0)||(parseInt(document.getElementById(senderId).value)%parseInt(document.getElementById('car2max').value)==0)||(parseInt(document.getElementById(senderId).value)%(parseInt(document.getElementById('car2max').value)+parseInt(document.getElementById('car2min').value))==0))
		              {
		                  fare(senderId)
		              }
		              else
		              {
		              alert('Please enter the Multiples of Two Passengers');
		              document.getElementById(senderId).value="";		             
		             
		              fare(senderId)
		              document.getElementById(senderId+'Value').value = "";
		              document.getElementById(senderId+'fare').value = "";
		              finalFare();
		              return;
		              }	
		         }		           	      
		      } 
		    function car34(senderId)
		      {	        
		        if(document.getElementById('chkCar3-4').checked ==true)
                 {
		              if((parseInt(document.getElementById(senderId).value)%parseInt(document.getElementById('car34min').value)==0)||(parseInt(document.getElementById(senderId).value)%parseInt(document.getElementById('car34max').value)==0)||(parseInt(document.getElementById(senderId).value)%(parseInt(document.getElementById('car34max').value)+parseInt(document.getElementById('car34min').value)))==0)		              
		              {
		              fare(senderId)
		              }
		              else
		              {
		              alert('Please enter Multiples of 3 or 4 Passengers');
		              document.getElementById(senderId).value="";		             
		            
		              fare(senderId)
		              document.getElementById(senderId+'Value').value = "";
		              document.getElementById(senderId+'fare').value = "";
		              finalFare();
		              return;
		              }
		          }			           	      
		      } 
		    function innova45(senderId)
		      {	   
		         if(document.getElementById('chkinnova4-5').checked ==true)
                 {     
		              if((parseInt(document.getElementById(senderId).value)%parseInt(document.getElementById('innova45min').value)==0)||(parseInt(document.getElementById(senderId).value)%parseInt(document.getElementById('innova45max').value)==0)||(parseInt(document.getElementById(senderId).value)%(parseInt(document.getElementById('innova45min').value)+parseInt(document.getElementById('innova45max').value)))==0)
		              {
		              fare(senderId)
		              }
		              else
		              {
		              alert('Please enter Multiples of 4 or 5 Passengers');
		              document.getElementById(senderId).value="";		             
		            
		              fare(senderId)
		              document.getElementById(senderId+'Value').value = "";
		              document.getElementById(senderId+'fare').value = "";
		              finalFare();
		              return;
		              }
		          }			           	      
		      } 
		    function innova67(senderId)
		      {	    
		        if(document.getElementById('chkinnova6-7').checked ==true)
                 {    
		              if((parseInt(document.getElementById(senderId).value)%parseInt(document.getElementById('innova67min').value)==0)||(parseInt(document.getElementById(senderId).value)%parseInt(document.getElementById('innova67max').value)==0)||(parseInt(document.getElementById(senderId).value)%(parseInt(document.getElementById('innova67min').value)+parseInt(document.getElementById('innova67max').value)))==0)
		              {
		              fare(senderId)
		              }
		              else
		              {
		                  alert('Please enter Multiples of 6 or 7 Passengers');
		                  document.getElementById(senderId).value="";		             
    		           
		                  fare(senderId)
		                  document.getElementById(senderId+'Value').value = "";
		                  document.getElementById(senderId+'fare').value = "";
		                  finalFare();
		              return;
		              }	
		          }		           	      
		      } 
		  function qualis46(senderId)
		   {	       
		        if(document.getElementById('chkqualis4-6').checked ==true)
                { 
		              if((parseInt(document.getElementById(senderId).value)%parseInt(document.getElementById('qualis46min').value)==0)||(parseInt(document.getElementById(senderId).value)%parseInt(document.getElementById('qualis46max').value)==0)||(parseInt(document.getElementById(senderId).value)%parseInt(5)==0)||(parseInt(document.getElementById(senderId).value)%(parseInt(document.getElementById('qualis46max').value)+parseInt(document.getElementById('qualis46min').value)))==0)
		              {
		                fare(senderId);
		              }
		              else
		              {
		                  alert('Please enter Multiples of 4 or 5 or 6 Passengers');
		                  document.getElementById(senderId).value="";		             
    		             
		                  fare(senderId)
		                  document.getElementById(senderId+'Value').value = "";
		                  document.getElementById(senderId+'fare').value = "";
		                  finalFare();
		                  return;
		              }	
		           }		           	      
		      } 
		    function qualis78(senderId)
		      {	     
		        if(document.getElementById('chkqualis7-8').checked ==true)
                {   
		              if((parseInt(document.getElementById(senderId).value)%parseInt(document.getElementById('qualis78min').value)==0)||(parseInt(document.getElementById(senderId).value)%parseInt(document.getElementById('qualis78max').value)==0)||(parseInt(document.getElementById(senderId).value)%(parseInt(document.getElementById('qualis78min').value)+parseInt(document.getElementById('qualis78max').value)))==0)
		              {
		              fare(senderId)
		              }
		              else
		              {
		                  alert('Please enter Multiples of 7 or 8 Passengers');
		                  document.getElementById(senderId).value="";		             
		                  //document.getElementById(senderId).focus();
		                  fare(senderId)
		                  document.getElementById(senderId+'Value').value = "";
		                  document.getElementById(senderId+'fare').value = "";
		                  finalFare();
		                  return;
		              }
		           }			           	      
		      } 
		      function tempo89(senderId)
		      {	        
		         if(document.getElementById('chktempo8-9').checked ==true)
                    {
		              if((parseInt(document.getElementById(senderId).value)%parseInt(document.getElementById('tempo89min').value)==0)||(parseInt(document.getElementById(senderId).value)%parseInt(document.getElementById('tempo89max').value)==0)||(parseInt(document.getElementById(senderId).value)%(parseInt(document.getElementById('tempo89max').value)+parseInt(document.getElementById('tempo89min').value)))==0)
		              {
		                 fare(senderId)
		              }
		              else
		              {
		                  alert('Please enter Multiples of 8 or 9 Passengers');
		                  document.getElementById(senderId).value="";		             
		                  //document.getElementById(senderId).focus();
		                  fare(senderId)
		                  document.getElementById(senderId+'Value').value = "";
		                  document.getElementById(senderId+'fare').value = "";
		                  finalFare();
		                  return;
		              }
		           }		           	      
		      } 
		      
		     function PostPage() 
		     {
		        document.getElementById('hidPundit').value = 'calcPundit';
		        document.form1.submit();
		     }
		     function clear()
		     {
	            var allTxt  = document.getElementsByTagName('input');       
	            for(var i=0; i <allTxt.length; i++)
	            {    		        
	                if( allTxt[i].type=='checkbox')
	                {               		               
	                    document.getElementById(allTxt[i].id).checked=false;
	                    chkCarvisible()
	                }
	            }
		     }
	var pax ='';
	function Getsuggession()
			{
			
			clear();
			
                if((document.getElementById('txtPax').value!=null)||(document.getElementById('txtPax').value!=""))
                {        
                   if(document.getElementById('txtPax').value==0)
                   { 
                        return ;
                   }
                   else if(parseInt(document.getElementById('txtPax').value)>18)
                   {
                      alert('Upto 18 Members will allow, Other wise Request Will send');
                         return;
                   }
                   
				    pax  = document.getElementById("txtPax").value
				   document.getElementById("txtSinglePax").value="";  
				   document.getElementById("txtExtraFareS").value="";
				   document.getElementById("txtTotalExtrafare").value="";
				}	
				xmlHttp=GetXmlHttpObject()				
				if (xmlHttp==null)
				{
					alert ("Browser does not support HTTP Request")
					return
				} 			
				var url="test1.aspx?paxnum=" + escape(pax);	
				var img = document.getElementById('imgWait');
				img.style.display = 'block';
				url=url+"&sid="+Math.random();	
				url=url+"&tourid="+document.getElementById("tid").value;
										
				xmlHttp.onreadystatechange=stateChanged1
				xmlHttp.open("GET",url,true)
				xmlHttp.send(null)				
			}
		     
	    function stateChanged1() 
		{ 
			if( (xmlHttp.readyState == 4 || xmlHttp.readyState=="complete"))
			{
			    var img = document.getElementById('imgWait');
				img.style.display = 'none';			
               var arr=xmlHttp.responseText;
               var aa=arr.split("^");
               //alert(aa[0]);
               var bb=aa[1];
               var cc=bb.split("#");
               
              for(var i=0;i<cc.length;i++)
              {               
                var dd=cc[i].split("-");
                //alert(dd[0]);
                 if(dd[0]==1)
                 {
                     document.getElementById('chkCar2').checked=true;
                     chkCarvisible();
                     document.getElementById('txtCar12px').value=dd[1];
                     fare('txtCar12px');
                 }
                 else if(dd[0]==2)
                 {
                     document.getElementById('chkCar3-4').checked=true;
                     chkCarvisible();
                     document.getElementById('txtCar34px').value=dd[1];
                     fare('txtCar34px');
                 }
                  else if(dd[0]==3)
                 {
                     document.getElementById('chkinnova4-5').checked=true;
                     chkCarvisible()
                     document.getElementById('txtInn45Px').value=dd[1];
                     fare('txtInn45Px');
                 }
                  else if(dd[0]==4)
                 {
                     document.getElementById('chkinnova6-7').checked=true;
                     chkCarvisible()
                     document.getElementById('txtInn67Px').value=dd[1];
                     fare('txtInn67Px')
                 }
                  else if(dd[0]==5)
                 {
                     document.getElementById('chkqualis4-6').checked=true;
                     chkCarvisible()
                     document.getElementById('txtQua46Px').value=dd[1];
                     fare('txtQua46Px')
                     }
                  else if(dd[0]==6)
                 {
                     document.getElementById('chkqualis7-8').checked=true;
                     chkCarvisible()
                      document.getElementById('txtQua78Px').value=dd[1];
                     fare('txtQua78Px')
                 }
                 else if(dd[0]==7)
                 {
                     document.getElementById('chktempo8-9').checked=true;
                     chkCarvisible()
                     document.getElementById('txtTem89Px').value=dd[1];
                     fare('txtTem89Px')
                 }              
              }            
               
       
               document.getElementById("txtPax").value=pax;      
                   
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
			else if (!xmlhttp && typeof XMLHttpRequest != "undefined") 
			{
				try 
				{
					xmlhttp = new XMLHttpRequest();
				} 
				catch (e) 
				{
					xmlhttp = false;
				}
			}
			return objXMLHttp
		}
		
		function Validationcheck()
		{
		   if((document.form1.txtTotalPax.value=="") ||(document.form1.txtTotalPax.value == 0))
		   { 
		        alert("please enter the pax no");
		        document.form1.txtTotalPax.focus();
		        return false;
		    }
		    if((document.form1.txtPax.value)!=(document.form1.txtTotalPax.value))
		   { 
		        alert("No of pax should be same");
		        document.form1.txtPax.focus();
		        return false;
		    }
		    
		   if(document.form1.txtDate.value=="") 
		   { 
		        alert("please select the date");
		        document.form1.txtDate.focus();
		        return false;
		    }
		    else
		    {
		      if(!submitDate)
		      {
		            alert("This is closed date for this tour, choose another ");
		            document.form1.txtDate.focus();
		            return false;
		      }   
		    }
		   if(document.form1.txtTotalExtrafare.value!="")
		   {
		       if(document.form1.chkSingle.checked==false)
		       {
		            alert("Please select single adult in a room.");
		            return false;
		            
		       }
		   }
		   
		  if(document.getElementById("txtTotalFare").value=="0")
		  {
			    alert("Fare can't be Zero,please choose another criteria");
			    return false;
		  }
		    
		   if(document.form1.ddlTitle.value=="Title")
		   { 
		        alert("Please select the title");
		        document.form1.ddlTitle.focus();
		        return false;
		   }
		   
		   if(Trim(document.form1.txtFName.value)=="")
		   { 
		        alert("Please enter the name");
		        document.form1.txtFName.focus();
		        return false;
		    }
		    
		 
		   if(Trim(document.form1.txtAddress.value)=="")
		   { 
		        alert("Please enter the address");
		        document.form1.txtAddress.focus();
		        return false;
		    }
		    
		   if(document.form1.txtMobile.value=="" && document.form1.txtphone.value=="")
		   { 
		        alert("Please enter the Mobile/ phone no");
		        document.form1.txtMobile.focus();
		        return false;
		   }
		   
		   if(Trim(document.form1.txtMobile.value)!="")
		   {
		   
		       var k=document.getElementById("txtMobile").value;
			   if(k.length<10)   
			    {
				    alert("Mobile No should be 10 digits.");
				    document.getElementById("txtMobile").focus();
				    return false;
			    }
		  }
		   if(Trim(document.form1.txtphone.value)!="")
		   {		   
		       var k=document.getElementById("txtphone").value;
			   if(k.length<6)   
			    {
				    alert("Phone No should be 6 digits.");
				    document.getElementById("txtphone").focus();
				    return false;
			    }
			}
		
		   
		 		    
		    if ((document.getElementById("txtMail").value)== "" )	
			{
				alert("Plese fill the e-mail field.It is mandatory.");
				document.getElementById("txtMail").focus();
				return false;
			}
			else
			{
				if ((document.getElementById("txtMail").value)!= "" )
				{
					if (CheckMail(document.getElementById("txtMail").value)== false)
					{
						
						
						document.getElementById("txtMail").focus();
						return false;
					}
				}
			}
			
			
			if ((document.getElementById('RadFlight').checked)) 
			{      
			    
				if(document.getElementById("txtpickVehicleNo").value=="Select")
				{
				    alert("Please choose flight type");
				    document.getElementById("txtpickVehicleNo").focus();
				    return false;
				}
				
			    if ((document.getElementById("ddlPkHrs").value=="")||(document.getElementById("ddlPkMints").value==""))
		        {
		            alert("Please Select the  time.");
		            
		            if(document.getElementById("ddlPkMints").value=="")
				        document.getElementById("ddlPkMints").focus();
		            if(document.getElementById("ddlPkHrs").value=="")
				        document.getElementById("ddlPkHrs").focus();
				        
			        return false;
			    }
				
				if(Trim(document.getElementById("txtFlightNo").value)=="")
				{
				    alert("Please enter Flight No ");
				    document.getElementById("txtFlightNo").focus();
				    return false;
				}
				
				
		    }
		    
		    
		    if ((document.getElementById('RadTrain').checked)) 
			{      
			    
				if(Trim(document.getElementById("txtRlyStationName").value)=="")
				{
				    alert("Please enter station name");
				    document.getElementById("txtRlyStationName").focus();
				    return false;
				}
				
			   if ((document.getElementById("ddlTrainPkHr").value=="")||(document.getElementById("ddlTrainPkMin").value==""))
		        {
		            alert("Please Select the  time.");
		            
		            if(document.getElementById("ddlTrainPkMin").value=="")
				        document.getElementById("ddlTrainPkMin").focus();
		            if(document.getElementById("ddlTrainPkHr").value=="")
				        document.getElementById("ddlTrainPkHr").focus();
				        
			        return false;
			    }
				
				if(Trim(document.getElementById("txtTrainNo").value)=="")
				{
				    alert("Please enter Train No ");
				    document.getElementById("txtTrainNo").focus();
				    return false;
				}
				
				
		    }
		    
		    
		    if ((document.getElementById('RadBus').checked)) 
			{      
			    
				if(Trim(document.getElementById("txtAddr").value)=="")
				{
				    alert("Please enter Pickup address");
				    document.getElementById("txtAddr").focus();
				    return false;
				}
				
				if(Trim(document.getElementById("txtStreet").value)=="")
				{
				    alert("Please enter Street");
				    document.getElementById("txtStreet").focus();
				    return false;
				}
				
		   }
			

		    
		    if(document.form1.chkTrue.checked==false)
            {
                alert("before submit you should agree with terms and conditions");
                return false;                
            }
            
               document.getElementById('Submit').style.display='none';        
	          return true;	
		}
		
function keyboardlock()
 {
       return false;
 }
 
  function chkNumeric()
    {		
    
	    if(event.shiftKey) return false;
	    if((event.keyCode<48 || event.keyCode>57) && event.keyCode != 8 && event.keyCode != 37 && event.keyCode != 38 && event.keyCode != 39 && event.keyCode != 40 && event.keyCode != 46 && event.keyCode != 13 && event.keyCode != 116 && event.keyCode != 16 && (event.keyCode <96 || event.keyCode > 105) && event.keyCode != 9 && event.keyCode != 110) event.returnValue = false
    } 

    function chkCharacter()
    {    	
	    if((event.keyCode > 90) || (event.keyCode < 65) && event.keyCode != 8 && event.keyCode != 37 && event.keyCode != 38 && event.keyCode != 39 && event.keyCode != 40 && event.keyCode != 46 && event.keyCode != 13 && event.keyCode != 116 && event.keyCode != 16 && (event.keyCode <96 || event.keyCode > 105) && event.keyCode != 9 && event.keyCode != 110) event.returnValue = false 		
    }
    
    
    function checkDate(val)
    {
                //alert(val);
                xmlHttp=GetXmlHttpObject();				
				if (xmlHttp==null)
				{
					alert ("Browser does not support HTTP Request")
					return
				} 			
				var url="test1.aspx?spljdate=" + val;
				url=url+"&sid="+Math.random();	
				url=url+"&tourid="+document.getElementById("tid").value;										
				xmlHttp.onreadystatechange=chekDD;
				xmlHttp.open("GET",url,true)
				xmlHttp.send(null);
    }
    var submitDate  = true;
     function chekDD() 
		{ 
		  
			if( (xmlHttp.readyState == 4 || xmlHttp.readyState=="complete"))
			{
			    
               var valResponse  = xmlHttp.responseText;
               if(valResponse=="0")
               {
                submitDate = false;
                alert('This is closed date for this tour, choose another.');
               }
               else
                   submitDate = true;   
              
               
           }
           
       }    
               
             
    