<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Agenthotels_iframe.aspx.cs" Inherits="SouthernTravelIndiaAgent.Agenthotels_iframe" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="../Assets/css/style.css" rel="stylesheet" type="text/css" />

   
    <script language="javascript" type="text/javascript">
    <!--
         function showRooms(val){           
            
            for(var i=1;i<=4;i++)
            {
               if(parseInt(i) <= parseInt(val)){
               
                    document.getElementById('room'+ i).style.display = 'block'; 
               }
               else
               {
                    document.getElementById('room'+ i).style.display = 'none'; 
               }
            }   
            document.getElementById('room1').style.display = 'block';  
            if(parseInt(val)==1)
            {
                resetAll();
            }        
         }
         function showRoomsChildren(rowId, val){
            var rowCount = parseInt(document.getElementById('strRooms').selectedIndex)+1;
            //rowId = rowCount;
            //childrenAge
            //roomChild1
            //r1c2
           // if( parseInt(val)>0) {
                document.getElementById('childrenAge').style.display = 'block'; 
            //}
           // else {
           //     document.getElementById('childrenAge').style.display = 'none';                 
           // }
            
            
           
            for(var i=1; i<=rowCount;i++)
            {
               if(parseInt(i) <= parseInt(rowCount)){
               
                  
                   document.getElementById('roomChild'+ i).style.display = 'block'; 
                   if((parseInt(i) == parseInt(rowId)) && ( parseInt(val)==0) ){
                        document.getElementById('roomChild'+ i).style.display = 'none'; 
                    }
                    
               }
               else
               {
                    document.getElementById('roomChild'+ i).style.display = 'none'; 
               }
            }
            
            //document.getElementById('roomChild'+ rowId).style.display = 'block';
             
            for(var i=1; i<=4;i++)
            {
               if(parseInt(i) <= parseInt(val)){
               
                    document.getElementById('r'+ rowId +'c'+ i).style.display = 'block'; 
               }
               else
               {
                    document.getElementById('r'+ rowId + 'c'+ i).style.display = 'none'; 
               }
            }
            
         }
         
         function resetAll(){
         
         }
         function validateInput(){
               var radCity = document.getElementsByName('strCity');                
               if(radCity[8].checked){
                    if(trim(document.getElementById('strOtherCity').value)=="")
                    {
                        alert('Enter city name');    
                        return false;
                    }
                    else
                    {
                        document.getElementById('hidCity').value =   trim(document.getElementById('strOtherCity').value); 
                    }  
               }
               else{
                   for(var i=0;i < radCity.length;i++ )
                   {
                        if(radCity[i].checked)
                        {
                            document.getElementById('hidCity').value = trim(radCity[i].value);
                            break;
                        }
                   }
               }
                           
                
               
               //alert(document.getElementById('hidCity').value);
               var tempDate = new Date();        
               var curDate = new Date(tempDate.getFullYear(),tempDate.getMonth(),tempDate.getDate());
               var tempSelectYear =  document.getElementById('strCheckinyear');
               var tempSelectMonth =  document.getElementById('strCheckinmonth');
               var tempSelectDate =  document.getElementById('strCheckindate');                           
               
               if(!isDate(isDate(tempSelectDate.options[tempSelectDate.selectedIndex].value,tempSelectMonth.options[tempSelectMonth.selectedIndex].value,tempSelectYear.options[tempSelectYear.selectedIndex].value)))
               {
                    alert('Select a valid check-in date');
                    return false;
               }
               
               
               var CheckInDate  = new Date(); 
               CheckInDate.setFullYear(parseInt(tempSelectYear.value));
               CheckInDate.setMonth(tempSelectMonth.selectedIndex);
               CheckInDate.setDate( parseInt(tempSelectDate.value));

              
               tempSelectYear =  document.getElementById('strCheckoutyear');
               tempSelectMonth =  document.getElementById('strCheckoutmonth');
               tempSelectDate =  document.getElementById('strCheckoutdate');             
               
               if(!isDate(isDate(tempSelectDate.options[tempSelectDate.selectedIndex].value,tempSelectMonth.options[tempSelectMonth.selectedIndex].value,tempSelectYear.options[tempSelectYear.selectedIndex].value)))
               {
                    alert('Select a valid check-out date');
                    return false;
               }  
               
               var CheckOutDate  = new Date();                
               CheckOutDate.setFullYear(parseInt(tempSelectYear1.value));
               CheckOutDate.setMonth(tempSelectMonth1.selectedIndex);
               CheckOutDate.setDate( parseInt(tempSelectDate1.value));  
               
               
               if( (CheckInDate - curDate)<0)
               {
                    alert('check-in date should be higher than today\'s date'); 
                    return false;
               }              
               else if( (CheckOutDate - CheckInDate)<0)
               {
                    alert('check-out date should be higher or equal to check-in date'); 
                    return false;
               }                                        
            return true;                  
         }
         
         function validateChilds(){
            
         
         
         
         }
         
         function LTrim( value ) {
        	
	        var re = /\s*((\S+\s*)*)/;
	        return value.replace(re, "$1");
        	
        }

        // Removes ending whitespaces
        function RTrim( value ) {
        	
	        var re = /((\s*\S+)*)\s*/;
	        return value.replace(re, "$1");
        	
        }

        // Removes leading and ending whitespaces
        function trim( value ) {
        	
	        return LTrim(RTrim(value));
        	
        }
        function y2k(number) { return (number < 1000) ? number + 1900 : number; }

        function isDate (day,month,year) {
            //alert('day '+ day);
            //alert('month '+ month);
            //alert('year '+ year);            
            // checks if date passed is valid
            // will accept dates in following format:
            // isDate(dd,mm,ccyy), or
            // isDate(dd,mm) - which defaults to the current year, or
            // isDate(dd) - which defaults to the current month and year.
            // Note, if passed the month must be between 1 and 12, and the
            // year in ccyy format.

            var today = new Date();
            year = ((!year) ? y2k(today.getYear()):year);
            month = ((!month) ? today.getMonth():month-1);
            if (!day) return false
            var test = new Date(year,month,day);
            if ( (y2k(test.getYear()) == year) &&
                 (month == test.getMonth()) &&
                 (day == test.getDate()) )
                return true;
            else
                return false
        }

        /*
        var dteDate;

        //set up a Date object based on the day, month and year arguments
        //javascript months start at 0 (0-11 instead of 1-12)
        
        
        
        dteDate = new Date(2007,0,21);    //Date(year,month,day);
        dDate = new Date(2007,0,21);
        var date1 = new Date(2007,11,12);
        
        var cURRdate = new Date();        
        var curDate = new Date(cURRdate.getFullYear(),cURRdate.getMonth(),cURRdate.getDate());
        
        alert( curDate );
        alert(date1);
        
        alert(  date1 - curDate);
         
        //alert(dteDate - dDate);
        */
        
     
         
function IMG1_onclick() {

}

     -->
    </script>

    <meta content="MSHTML 6.00.2900.2180" name="GENERATOR" />
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1" />
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1" />
    <meta name="vs_defaultClientscript" content="Javascript" />
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
    <style type="text/css">
    <!--      

        .tp-lk {
        color:#000000;
        font-family:arial;
        font-size:11px;
        font-weight:bold;
        padding -right:10px;
        }
        .tp-lk a {
        color:#000000;
        text-decoration:none;
        }
        .tp-lk a:hover {
        color:#000000;
        text-decoration:underline;
        }
        .mn-lk {
        color:#DD4503;
        font-family:arial;
        font-size:12px;
        font-weight:bold;
        line-height:19px;
        padding-right:10px;
        }
        .mn-lk a {
        color:#DD4503;
        text-decoration:none;
        }
        .mn-lk a:hover {
        color:#000000;
        text-decoration:underline;
        }
        .mn-lk1 {
        color:#DD4503;
        font-family:arial;
        font-size:12px;
        font-weight:bold;
        line-height:19px;
        padding-right:10px;
        }
        .mn-lk1 a {
        color:#DD4503;
        text-decoration:none;
        }
        .mn-lk1 a:hover {
        color:#000000;
        text-decoration:underline;
        }
        .lft-td {
        float:left;
        margin-left:7px;
        width:193px;
        }
        .mn-tr-hd {
        background:transparent url(../Assets/images/ip-hd-bg.gif) repeat scroll 0%;
        color:#E14000;
        font-family:arial;
        font-size:12px;
        padding-left:15px;
        }
        .mn-tr-hd a {
        color:#E14000;
        text-decoration:none;
        }
        .mn-tr-hd a:hover {
        color:#000000;
        text-decoration:underline;
        }
        .tour-bg {
        background:transparent url(../Assets/images/tour-bg.gif) repeat scroll 0%;
        color:#273600;
        font-family:arial;
        font-size:11px;
        }
        .tour-bg a {
        color:#273600;
        text-decoration:underline;
        }
        .tour-bg a:hover {
        color:#000000;
        text-decoration:none;
        }
        .tour-bg b {
        color:#730D00;
        }
        .tour-bg b a {
        color:#730D00;
        text-decoration:underline;
        }
        .tour-bg b a:hover {
        color:#000000;
        text-decoration:none;
        }
        .tour-lk {
        line-height:19px;
        padding-left:10px;
        }
        .royal-india {
        background:transparent url(../Assets/images/royal-india-bg.gif) no-repeat scroll left top;
        color:#273600;
        font-family:arial;
        font-size:11px;
        }
        .royal-india a {
        color:#273600;
        text-decoration:underline;
        }
        .royal-india a:hover {
        color:#000000;
        text-decoration:none;
        }
        .mn-ht-hd {
        background:transparent url(../Assets/images/hotel-hd-bg.jpg) repeat scroll 0%;
        color:#E14000;
        font-family:arial;
        font-size:12px;
        padding-left:15px;
        }
        .mn-ht-hd a {
        color:#E14000;
        text-decoration:none;
        }
        .mn-ht-hd a:hover {
        color:#000000;
        text-decoration:underline;
        }
        .hotel-lk {
        background:transparent url(../Assets/images/ht-bg.gif) repeat scroll 0%;
        color:#000000;
        font-family:arial;
        font-size:11px;
        line-height:19px;
        }
        .hotel-lk a {
        color:#E14000;
        text-decoration:none;
        }
        .hotel-lk a:hover {
        color:#000000;
        text-decoration:underline;
        }
        .ht-lk {
        padding-left:5px;
        padding-right:5px;
        }
        .ht-lk1 {
        padding-left:5px;
        }
        .ht-lk .ht-southern {
        color:#EF4900;
        }
        .ht-lk1 .ht-southern {
        color:#EF4900;
        }
        .travel-kit-hd {
        background:#FD8E00 url(../Assets/images/travel-kit-bg.gif) repeat scroll 0%;
        color:#FFFFFF;
        font-family:arial;
        font-size:13px;
        padding-left:20px;
        }
        .travel-kit-hd a {
        color:#FFFFFF;
        text-decoration:none;
        }
        .travel-kit-hd a:hover {
        color:#000000;
        text-decoration:underline;
        }
        .travel-kit-lk {
        background:#FFE763 none repeat scroll 0%;
        color:#000000;
        font-family:arial;
        font-size:12px;
        }
        .travel-kit-lk a {
        color:#000000;
        text-decoration:none;
        }
        .travel-kit-lk a:hover {
        color:#000000;
        text-decoration:underline;
        }
        .tp-nav {
        color:#000000;
        font-family:arial;
        font-size:11px;
        margin-top:5px;
        padding-right:10px;
        }
        .tp-nav a {
        color:#000000;
        text-decoration:none;
        }
        .tp-nav a:hover {
        color:#000000;
        text-decoration:underline;
        }
        h1 {
        color:#E14000;
        font-family:arial;
        font-size:17px;
        padding-left:218px;
        }
        .pg-hd {
        color:#E14000;
        float:left;
        font-family:arial;
        font-size:17px;
        }
        .page-hd {
        color:#143780;
        font-family:arial;
        font-size:15px;
        padding-bottom:10px;
        padding-left:218px;
        padding-top:10px;
        }
        .page-hd h1 {
        color:#143780;
        display:inline;
        font-family:arial;
        font-size:15px;
        padding-left:0px;
        }
        .text {
        color:#000000;
        font-family:arial;
        font-size:11px;
        line-height:19px;
        margin-left:214px;
        padding-right:10px;
        }
        .text .hd {
        color:#AF280C;
        font-family:arial;
        font-size:12px;
        line-height:19px;
        padding-right:10px;
        }
        .ftr {
        padding-left:10px;
        padding-right:10px;
        }
        .ftr-lk {
        color:#646464;
        font-family:arial;
        font-size:11px;
        line-height:19px;
        }
        .ftr-lk a {
        color:#646464;
        text-decoration:none;
        }
        .ftr-lk a:hover {
        color:#000000;
        text-decoration:underline;
        }
        .ftr-lk b {
        color:#FF6000;
        }
        .ftr-lk-mn {
        color:#000000;
        font-family:arial;
        font-size:11px;
        font-weight:bold;
        line-height:19px;
        }
        .ftr-lk-mn a {
        color:#000000;
        text-decoration:none;
        }
        .ftr-lk-mn a:hover {
        color:#000000;
        text-decoration:underline;
        }
        .fact-bg {
        background:#C26958 url(../Assets/images/travel-fact-rt-bg.gif) no-repeat scroll 0%;
        color:#FFFFFF;
        float:right;
        font-weight:bold;
        line-height:21px;
        width:155px;
        }
        .fact-txt .fact-bg b {
        color:#FFEC86;
        text-decoration:none;
        }
        .fact-hd {
        background:transparent url(../Assets/images/travel-fact-bg.gif) repeat-x scroll 0%;
        color:#E01000;
        font-family:Ms Sans Serif,Verdana,Arial;
        font-size:12px;
        font-weight:bold;
        line-height:22px;
        padding-bottom:6px;
        padding-left:6px;
        }
        .fact-txt {
        background:transparent url(../Assets/images/travel-fact-text-bg.gif) repeat-x scroll 0%;
        color:#3C3C3C;
        font-family:Ms Sans Serif,Verdana,Arial;
        font-size:11px;
        line-height:22px;
        padding-left:5px;
        }
        .fact-txt b {
        color:#98493D;
        }
        .fact-txt b.hd {
        color:#98493D;
        }
        .fact-txt a {
        color:#772A00;
        font-weight:bold;
        text-decoration:underline;
        }
        .fact-txt a:hover {
        color:#000000;
        text-decoration:none;
        }
        .holiday-txt {
        background:#FFFFF6 none repeat scroll 0%;
        color:#3C3C3C;
        font-family:Arial;
        font-size:12px;
        line-height:22px;
        padding-left:5px;
        }
        .holiday-txt b {
        color:#98493D;
        }
        .holiday-txt b.hd {
        color:#C34500;
        }
        .holiday-txt a {
        color:#C34500;
        font-weight:bold;
        text-decoration:underline;
        }
        .holiday-txt a:hover {
        color:#000000;
        text-decoration:none;
        }
        .detail-bg {
        background-image:url(../Assets/images/detail-bg.gif);
        color:#1F51C2;
        font-family:Ms Sans Serif,Verdana,Arial;
        font-size:11px;
        line-height:22px;
        }
        .fact-txt2 {
        color:#903A0B;
        font-family:Arial;
        font-size:12px;
        line-height:19px;
        }
        .cost-bg {
        background:#FFCB2B none repeat scroll 0%;
        border:1px solid #DC8574;
        color:#000000;
        float:right;
        font-family:Ms Sans Serif,Verdana,Arial;
        font-weight:bold;
        line-height:17px;
        width:155px;
        }
        .cost-ul {
        color:#97301A;
        list-style-image:none;
        list-style-position:outside;
        list-style-type:square;
        margin-left:10px;
        margin-top:5px;
        padding-left:10px;
        }
        .guided-lk {
        background:transparent url(gifs2/line-india-guide.gif) repeat scroll center bottom;
        color:#000000;
        font-family:Ms Sans Serif,Verdana,Arial;
        font-size:12px;
        font-weight:bold;
        }
        .guided-lk ul {
        line-height:30px;
        margin:0px 0px 0px 6px;
        padding-left:16px;
        }
        .guided-lk li {
        list-style-image:url(../Assets/images/ind-guide-bt1.gif);
        margin-top:0px;
        }
        .book-lk {
        border:1px solid #DC8574;
        color:#E80101;
        font-size:12px;
        margin-right:7px;
        width:178px;
        }
        .book-lk a {
        color:#E80101;
        text-decoration:underline;
        }
        .book-lk a:hover {
        color:#000000;
        text-decoration:none;
        }
        .footer {
        color:#000000;
        font-family:arial,verdana,ms sans serif;
        font-size:12px;
        }
        .footer a {
        color:#000000;
        text-decoration:none;
        }
        .footer a:hover {
        color:#666666;
        text-decoration:underline;
        }
        .td-txt {
        color:#000000;
        font-family:arial,verdana,ms sans serif;
        font-size:11px;
        padding-left:5px;
        text-align:left;
        }
        .td-txt b {
        color:#FFFFFF;
        }
        .td-txt .tdb {
        color:#B02B26;
        }
        .tariff-chart {
        color:#B00C01;
        font-family:arial,verdana,ms sans serif;
        font-size:11px;
        }
        .tariff-chart a {
        color:#B00C01;
        text-decoration:underline;
        }
        .tariff-chart a:hover {
        color:#000000;
        text-decoration:none;
        }
        .text1 {
        color:#000000;
        font-family:arial;
        font-size:11px;
        line-height:19px;
        margin-left:214px;
        padding-right:10px;
        }
        .mh {
        background:transparent url(../Assets/images/travel-fact-bg.gif) repeat-x scroll 0%;
        color:#E01000;
        font-family:Arial;
        font-size:13px;
        font-weight:bold;
        line-height:22px;
        padding-bottom:6px;
        padding-left:10px;
        }
        .mh a {
        color:#E01000;
        text-decoration:none;
        }
        .mh a:hover {
        color:#000000;
        text-decoration:underline;
        }
        .text1 .sh {
        background:#FFFDF5 none repeat scroll 0%;
        color:#B35800;
        font-family:Arial;
        font-size:12px;
        font-weight:bold;
        padding-left:25px;
        }
        .text1 .sh a {
        color:#B35800;
        text-decoration:none;
        }
        .text1 .sh a:hover {
        color:#000000;
        text-decoration:underline;
        }
        .text1 .mh2 {
        background-image:url(../Assets/images/detail-bg.gif);
        color:#B35800;
        font-family:Arial;
        font-size:12px;
        font-weight:bold;
        line-height:22px;
        padding-left:25px;
        }
        .text1 .mh2 a {
        color:#B35800;
        text-decoration:none;
        }
        .text1 .mh2 a:hover {
        color:#000000;
        text-decoration:underline;
        }
        .text1 .sh1 {
        background:#FFF6B0 none repeat scroll 0%;
        color:#323232;
        font-family:Arial;
        font-size:12px;
        font-weight:bold;
        padding-left:43px;
        }
        .text1 .sh1 a {
        color:#323232;
        text-decoration:none;
        }
        .text1 .sh1 a:hover {
        color:#000000;
        text-decoration:underline;
        }
        .text1 .sh2 {
        color:#323232;
        font-family:Arial;
        font-size:11px;
        padding-left:63px;
        }
        .text1 .sh2 a {
        color:#323232;
        text-decoration:none;
        }
        .text1 .sh2 a:hover {
        color:#000000;
        text-decoration:underline;
        }
        .tp-lk-hm {
        color:#000000;
        font-family:arial;
        font-size:11px;
        font-weight:bold;
        line-height:20px;
        padding-right:10px;
        }
        .tp-lk-hm a {
        color:#000000;
        text-decoration:none;
        }
        .tp-lk-hm a:hover {
        color:#000000;
        text-decoration:underline;
        }
        .tptxt {
        color:#000000;
        font-family:Arial;
        font-size:11px;
        line-height:18px;
        padding:10px;
        }
        .lfttd {
        float:left;
        width:221px;
        }
        .mn-hd {
        color:#E14000;
        font-family:Arial;
        font-size:16px;
        }
        .mn-hd a {
        color:#E14000;
        text-decoration:none;
        }
        .mn-hd a:hover {
        color:#E14000;
        text-decoration:none;
        }
        .lfthd {
        background:transparent url(../Assets/images/lft-hd-bg.gif) repeat scroll 0%;
        color:#A93300;
        font-family:Arial;
        font-size:13px;
        line-height:29px;
        padding-left:45px;
        }
        .lft-bg {
        background:transparent url(../Assets/images/lftbg.gif) repeat scroll 0%;
        color:#000000;
        font-family:Arial;
        font-size:11px;
        }
        .lft-bg ul {
        line-height:18px;
        list-style-image:none;
        list-style-position:outside;
        list-style-type:none;
        margin:0px;
        padding-bottom:8px;
        }
        .lft-bg ul li {
        background:transparent url(../Assets/images/lft-arrow.gif) no-repeat scroll left center;
        margin-left:20px;
        padding-left:10px;
        }
        .lft-bg a {
        color:#000000;
        text-decoration:none;
        }
        .lft-bg a:hover {
        color:#000000;
        text-decoration:underline;
        }
        .lftsh {
        color:#04369A;
        font-family:Arial;
        font-size:12px;
        padding-left:20px;
        }
        .righttd {
        float:right;
        padding-left:5px;
        padding-right:5px;
        text-align:center;
        width:195px;
        }
        .ht-st {
        background:transparent url(../Assets/images/ht-st-bg.jpg) no-repeat scroll 0%;
        color:#FFFFFF;
        font-family:Arial;
        font-size:12px;
        height:78px;
        line-height:19px;
        padding-left:10px;
        padding-right:30px;
        text-align:left;
        }
        .ht-st b {
        color:#F6FF00;
        }
        .ht-st b a {
        color:#F6FF00;
        text-decoration:underline;
        }
        .ht-st b a:hover {
        color:#F6FF00;
        text-decoration:none;
        }
        .form-lk {
        background:#EDEEFB none repeat scroll 0%;
        color:#000000;
        font-family:arial;
        font-size:11px;
        line-height:19px;
        text-align:left;
        width:184px;
        }
        .form-hd {
        color:#FFFFFF;
        font-family:arial;
        font-size:12px;
        }
        .mn-hd2 {
        color:#0B0E5A;
        font-family:Arial;
        font-size:16px;
        margin-top:10px;
        }
        .tool-kit {
        text-align:left;
        width:184px;
        }
        .news-md {
        background:transparent url(../Assets/images/news-md.gif) repeat scroll 0%;
        text-align:left;
        }
        .news-hd {
        background:transparent url(../Assets/images/news-hd.gif) no-repeat scroll 0%;
        color:#000000;
        font-family:verdana;
        font-size:12px;
        line-height:27px;
        }
        .news-md-txt {
        color:#000000;
        font-family:arial;
        font-size:11px;
        padding-left:10px;
        }
        .credit-card {
        background:transparent url(../Assets/images/credit-card.gif) no-repeat scroll 0%;
        color:red;
        font-family:arial;
        font-size:12px;
        line-height:37px;
        }
        .royal-india-hm {
        background:transparent url(../Assets/images/royal-india-hm-bg.gif) no-repeat scroll left top;
        color:#273600;
        font-family:arial;
        font-size:11px;
        padding-left:10px;
        text-align:left;
        }
        .royal-india-hm a {
        color:#273600;
        text-decoration:underline;
        }
        .royal-india-hm a:hover {
        color:#000000;
        text-decoration:none;
        }
        .cen {
        margin-left:235px;
        padding-right:10px;
        }
        .cen .tour-hd {
        color:#000000;
        font-family:verdana;
        font-size:13px;
        font-weight:bold;
        margin-top:10px;
        padding-bottom:2px;
        padding-left:10px;
        }
        .cen .tour-lnk {
        color:#730D00;
        font-family:arial,verdana;
        font-size:12px;
        line-height:18px;
        padding-left:10px;
        }
        .cen a {
        text-decoration:none;
        }
        .cen a:hover {
        text-decoration:underline;
        }
        .cen .tour-lnk a {
        color:#730D00;
        }
        .cen .ker-tour-lnk {
        color:#273600;
        font-family:arial,verdana;
        font-size:12px;
        line-height:18px;
        padding-left:10px;
        }
        .cen .ker-tour-lnk a {
        color:#273600;
        }
        .cen .goa-tour-lnk {
        color:#044297;
        font-family:arial,verdana;
        font-size:12px;
        line-height:18px;
        padding-left:10px;
        }
        .cen .goa-tour-lnk a {
        color:#044297;
        }
        .cen .nor-tour-lnk {
        color:#044297;
        font-family:arial,verdana;
        font-size:12px;
        line-height:18px;
        padding-left:10px;
        }
        .cen .nor-tour-lnk a {
        color:#044297;
        }
        .dotline {
        background:transparent url(../Assets/images/dot.gif) repeat-x scroll 0%;
        margin-right:201px;
        margin-top:5px;
        }
        td.left {
        color:#000000;
        }
        .bg1-left {
        background-color:#FFFFFF;
        }
        .bg2-left {
        background-color:#FFFFFF;
        }
        .h1-left {
        color:#DC590F;
        font-size:11px;
        font-weight:bold;
        }
        .h2-left {
        color:#DC590F;
        font-size:11px;
        font-weight:bold;
        }
        .h3-left {
        color:#A31708;
        font-size:11px;
        }
        .color1-left {
        color:#000000;
        font-size:11px;
        }
        hr.hr-left {
        color:#DC590F;
        }
        a.link-left {
        color:#DC590F;
        text-decoration:underline;
        }
        a.link-left:hover {
        color:#A31708;
        text-decoration:underline;
        }
        input.submit-left {
        background-color:#ECEBEB;
        border:1px solid #000000;
        color:#000000;
        font-family:Verdana,Arial,Helvetica;
        font-size:10px;
        }
        body {
        color:#000000;
        font-family:Arial,Helvetica,sans-serif;
        font-size:11px;
        }
        td {
        color:#000000;
        font-family:Arial,Helvetica,sans-serif;
        font-size:11px;
        }
        .hilighted-text {
        color:#DC590F;
        }
        .error {
        color:#164316;
        }
        .special-rate {
        color:#DC590F;
        font-size:12px;
        font-weight:bold;
        }
        p {
        border:0px none #FFFFFF;
        color:#000000;
        font-family:Arial;
        font-size:11px;
        }
        i {
        border:0px none #FFFFFF;
        color:#000000;
        font-family:Arial;
        font-size:11px;
        text-decoration:none;
        }
        .h1 {
        color:#DC590F;
        font-size:16px;
        font-weight:bold;
        }
        .h2 {
        color:#DC590F;
        font-family:Arial;
        font-size:14px;
        font-weight:bold;
        }
        .h3 {
        color:#DC590F;
        font-family:Arial;
        font-size:12px;
        font-weight:bold;
        }
        .affiliate-name {
        color:#000000;
        font-family:Arial;
        font-size:12px;
        font-weight:bold;
        }
        .star {
        color:#C60803;
        font-size:16px;
        }
        .text-hilighted {
        color:#000000;
        }
        .text-error {
        color:#000000;
        }
        hr {
        color:#FFD697;
        }
        a {
        color:#DC590F;
        font-size:11px;
        text-decoration:none;
        }
        a:hover {
        color:#E76C04;
        text-decoration:underline;
        }
        .input {
        border:1px solid #000000;
        font-family:Arial;
        font-size:11px;
        height:18px;
        }
        textarea {
        border-width:1px;
        color:#000000;
        font-family:Arial;
        font-size:11px;
        }
        .textarea {
        border-width:2px;
        color:#000000;
        font-family:Arial;
        font-size:11px;
        }
        select {
        background-color:#FFFFFF;
        border:1px solid #000000;
        font-family:Arial;
        font-size:10px;
        height:18px;
        margin:0px;
        }
        .select {
        background-color:#FFFFFF;
        border:1px solid #000000;
        font-family:Arial;
        font-size:10px;
        height:18px;
        margin:0px;
        }
        option {
        padding:1px;
        }
        .button {
        background-color:#D77601;
        border:1px solid #000000;
        color:#000000;
        font-family:Arial;
        font-size:11px;
        }
        .input1 {
        border:0px solid #FFFFFF;
        color:#000000;
        font-family:Arial;
        font-size:11px;
        }
        input.submit {
        background-color:#D77601;
        border:1px solid #000000;
        color:#000000;
        font-family:Verdana,Arial,Helvetica;
        font-size:10px;
        }
        input.radio {
        color:black;
        display:block;
        float:left;
        width:2em;
        }
        .table1-heading-bg {
        background-image:url(../Assets/images/yl-bg.gif);
        color:#000000;
        font-size:12px;
        font-weight:bold;
        height:23px;
        }
        .resultsPane_carbl {
        background-color:#FFFFFF;
        border:1px solid #FFC72A;
        border-collapse:collapse;
        color:#B95613;
        font-size:12px;
        font-weight:bold;
        }
        .resultsPane-carb-more {
        background-color:#FFFFFF;
        border:1px solid #FFC72A;
        border-collapse:collapse;
        color:#B95613;
        font-size:12px;
        font-weight:bold;
        }
        .resultsPane-carb-facility {
        background-color:#FDEED3;
        color:#1F6296;
        font-size:12px;
        font-weight:bold;
        }
        .resultsPane-photo-border {
        background-color:#FFF8EC;
        border:1px solid #FFC72A;
        color:#FDEED3;
        font-size:12px;
        font-weight:bold;
        }
        .resultsPane-photo-bg {
        background-color:#FDEED3;
        font-size:11px;
        font-weight:normal;
        }
        .resultsPane-facility-list {
        background-color:#FFF8EC;
        font-size:11px;
        font-weight:normal;
        }
        .table1-border {
        background-color:#D4E1BF;
        background-image:url(../Assets/images/yl-bg.gif);
        margin-bottom:4px;
        margin-top:4px;
        }
        .table1-border2 {
        background-color:#FDEED3;
        border:1px solid #FFC72A;
        border-collapse:collapse;
        }
        .table1-light-bg {
        background-color:#FFFEEC;
        }
        .table1-light-border {
        background-color:#F3FCF3;
        }
        .resultsPane {
        background-color:#FFFEEC;
        border:1px solid #FFC72A;
        border-collapse:collapse;
        }
        .resultsPane2 {
        background-color:#FFFEEC;
        border:1px solid #FFC72A;
        border-collapse:collapse;
        }
        .noborder {
        border:0px solid;
        }
        #LeftCellMoreInfo {
        background-image:url(/partners/pages/booking/PalaceOnWheel/Assets/images/left_border.gif);
        border-bottom:2px solid #FFFFFF;
        width:169px;
        }
        #RightCellMoreInfo {
        width:609px;
        }
        .moreinfo-table1-border {
        background-color:#FDEED3;
        }
        .moreinfo-table1-heading-bg {
        background-color:#FFF8EC;
        color:#A31708;
        font-size:12px;
        font-weight:bold;
        }
        .moreinfo-facility {
        background:#FFF8EC none repeat scroll 0%;
        border:1px solid #FDEED3;
        border-collapse:collapse;
        }
        .moreinfo-booking {
        background-color:#FFF8EC;
        border:1px solid #FDEED3;
        border-collapse:collapse;
        }
        .moreinfo-h1 {
        color:#A31708;
        font-size:16px;
        font-weight:bold;
        }
        .moreinfo-special-rate {
        color:#CB3301;
        font-family:Arial;
        font-size:16px;
        font-weight:bold;
        }
        .moreinfo-rate-small {
        color:#CA1B04;
        }
        .moreinfo-h2 {
        color:#A31708;
        font-family:Arial;
        font-size:18px;
        }
        .moreinfo-h3 {
        color:#A31708;
        font-family:Arial;
        font-size:12px;
        font-weight:bold;
        }
        .moreinfo-hr {
        color:#DC590F;
        }
        .a {
        color:#31349B;
        font-family:Arial;
        font-size:11px;
        text-decoration:underline;
        }
        .a:hover {
        color:#FF6600;
        text-decoration:underline;
        }
        .a-red {
        color:#000000;
        font-family:Arial;
        font-size:11px;
        text-decoration:none;
        }
        .a-red:hover {
        color:#DE3107;
        text-decoration:underline;
        }
        .bigfont {
        border:0px none #FFFFFF;
        color:#000000;
        font-family:Arial;
        font-size:12px;
        }
        .table {
        background-color:#FBB56E;
        border:1px solid #000000;
        }
        .td {
        border:0px none #FFFFFF;
        color:#000000;
        font-family:Arial;
        font-size:11px;
        }
        .resultsPane_blpt {
        background-color:#DAE6FF;
        border:1px solid #7BAEED;
        border-collapse:collapse;
        color:#010082;
        font-family:Arial,Helvetica,sans-serif;
        font-size:13px;
        font-weight:bold;
        text-decoration:none;
        }
        .resultsPane_bl {
        border:1px solid #7BAEED;
        border-collapse:collapse;
        }
        .tab-oth {
        background-image:url(/partners/pages/booking/PalaceOnWheel/Assets/images/tabbg.gif);
        border:1px solid #F2922C;
        color:#880609;
        font-family:Verdana,Arial,Helvetica,sans-serif;
        font-size:11px;
        font-style:normal;
        font-weight:bold;
        line-height:normal;
        text-decoration:none;
        }
        .tab-oth:hover {
        background-image:url(/partners/pages/booking/PalaceOnWheel/Assets/images/tabbg.gif);
        border:1px solid #F2922C;
        color:#880609;
        font-family:Verdana,Arial,Helvetica,sans-serif;
        font-size:11px;
        font-style:normal;
        font-weight:bold;
        line-height:normal;
        text-decoration:none;
        }
        .tab-actv {
        border-color:#F2922C rgb(255, 255, 255) rgb(242, 146, 44) rgb(242, 146, 44);
        border-style:solid;
        border-width:1px;
        color:#A31708;
        font-family:Verdana,Arial,Helvetica,sans-serif;
        font-size:11px;
        font-style:normal;
        font-weight:bold;
        line-height:normal;
        text-decoration:none;
        }
        .tab-table {
        border-color:#F2922C;
        border-style:none solid none none;
        border-width:1px;
        }
        .h4 {
        font-family:Arial,Helvetica,sans-serif;
        font-size:11px;
        font-weight:normal;
        }
        .h5 {
        color:#010082;
        font-family:Arial,Helvetica,sans-serif;
        font-size:13px;
        font-weight:bold;
        text-decoration:none;
        }
        .infotbl {
        background-color:#FFF8EC;
        border-color:#FFFFFF;
        border-style:none solid none none;
        border-width:1px;
        font-family:Arial,Helvetica,sans-serif;
        font-size:11px;
        font-weight:normal;
        }
        .moreinfo-email-link {
        color:#D34C02;
        text-decoration:none;
        }
        .moreinfo-email-link:hover {
        color:#D34C02;
        text-decoration:none;
        }
        .moreinfo-input {
        border:1px solid #C0C0C0;
        font-family:Arial;
        font-size:12px;
        height:18px;
        }
        .moreinfo-textarea {
        border-width:1px;
        font-family:Arial;
        font-size:12px;
        }
        -->
        </style>
</head>
<body>
    <form id="form1" runat="server" target="_parent">
        <table width="276" height="409" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td align="left">
                    <img src="Assets/images/left_corner.gif" width="8" height="8" /></td>
                <td bgcolor="#E7E7E7">
                    <img src="Assets/images/trans.gif" width="260" height="1" /></td>
                <td align="right">
                    <img src="Assets/images/rgt_corner.gif" width="8" height="8" /></td>
            </tr>
            <tr>
                <td align="left" bgcolor="#E7E7E7">
                    &nbsp;</td>
                <td valign="top">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td width="100%">
                                <img src="Assets/images/hotel_img.gif" width="260" height="81" border="0" usemap="#Map"
                                    id="IMG1" language="javascript" onclick="return IMG1_onclick()" /></td>
                        </tr>
                        <tr>
                            <td align="center">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td valign="top" align="center">
                                            <!-- Main Content Starts -->
                                            <table cellspacing="0" cellpadding="0" border="0" style="display: block;" id="table_des_hotel">
                                                <tr>
                                                    <td align="center" colspan="2">
                                                        <table width="100%" cellspacing="0" cellpadding="2" border="0">
                                                            <tr>
                                                                <td width="100%">
                                                                    <table width="100%" cellspacing="0" cellpadding="0" border="0">
                                                                        <tbody class="city">
                                                                            <tr>
                                                                                <td nowrap="" align="left" class="fntwht">
                                                                                    <input type="radio" checked="checked" name="strCity" value="New Delhi" />
                                                                                    New Delhi
                                                                                </td>
                                                                                <td nowrap="" align="left" class="fntwht">
                                                                                    <input type="radio" name="strCity" value="Mumbai " />
                                                                                    Mumbai</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td nowrap="" align="left" class="fntwht">
                                                                                    <input type="radio" name="strCity" value="Jaipur" />
                                                                                    Jaipur</td>
                                                                                <td align="left" class="fntwht">
                                                                                    <input type="radio" name="strCity" value="Agra" />
                                                                                    Agra</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td nowrap="" align="left" class="fntwht">
                                                                                    <input type="radio" name="strCity" value="Goa" />
                                                                                    Goa</td>
                                                                                <td nowrap="" align="left" class="fntwht">
                                                                                    <input type="radio" name="strCity" value="Bangalore" />
                                                                                    Bengaluru</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td nowrap="" align="left" class="fntwht">
                                                                                    <input type="radio" name="strCity" value="Chennai" />
                                                                                    Chennai</td>
                                                                                <td nowrap="" align="left" class="fntwht">
                                                                                    <input type="radio" name="strCity" value="Kolkata" />
                                                                                    Kolkata</td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                    <input id="hidCity" type="hidden" runat="server">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="100%" nowrap="" class="fntwht">
                                                                    <input type="radio" name="strCity" value="other" onclick="text()" />
                                                                    Other City
                                                                    <input style="height: 20px; background-color: rgb(255, 255, 160);" size="15" onfocus="othercity();"
                                                                        class="input" name="strOtherCity" id="strOtherCity" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td background="Assets/images/hotel_room.jpg" style="background-position: bottom; background-repeat: no-repeat;
                                                        height: 235px;" valign="top">
                                                        <table width="100%" cellspacing="0" cellpadding="0" border="0">
                                                            <tr>
                                                                <td width="100%" valign="top">
                                                                    <table cellspacing="0" cellpadding="2" bordercolor="#111111" border="0" style="border-collapse: collapse;">
                                                                        <tr>
                                                                            <td width="55" valign="bottom" height="25" colspan="2">
                                                                                <font class="tabwht">Check-in:</font></td>
                                                                            <td nowrap="">
                                                                                <select class="homeselect" name="strCheckindate" id="strCheckindate" runat="server"
                                                                                    size="1" onchange="setCheckOutDateNew();">
                                                                                    <option value="01">1</option>
                                                                                    <option value="02">2</option>
                                                                                    <option value="03">3</option>
                                                                                    <option value="04">4</option>
                                                                                    <option value="05">5</option>
                                                                                    <option value="06">6</option>
                                                                                    <option value="07">7</option>
                                                                                    <option value="08">8</option>
                                                                                    <option value="09">9</option>
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
                                                                                    <option value="24">24</option>
                                                                                    <option value="25">25</option>
                                                                                    <option value="26">26</option>
                                                                                    <option value="27">27</option>
                                                                                    <option value="28">28</option>
                                                                                    <option value="29">29</option>
                                                                                    <option value="30">30</option>
                                                                                    <option value="31">31</option>
                                                                                </select>
                                                                                <select class="homeselect" name="strCheckinmonth" id="strCheckinmonth" size="1" runat="server"
                                                                                    onchange="setCheckOutDateNew();">
                                                                                    <option value="01">Jan</option>
                                                                                    <option value="02">Feb</option>
                                                                                    <option value="03">Mar</option>
                                                                                    <option value="04">Apr</option>
                                                                                    <option value="05">May</option>
                                                                                    <option value="06">Jun</option>
                                                                                    <option value="07">Jul</option>
                                                                                    <option value="08">Aug</option>
                                                                                    <option value="09">Sep</option>
                                                                                    <option value="10">Oct</option>
                                                                                    <option value="11">Nov</option>
                                                                                    <option value="12">Dec</option>
                                                                                </select>
                                                                                <select class="homeselect" name="strCheckinyear" id="strCheckinyear" size="1" runat="server"
                                                                                    onchange="setCheckOutDateNew();">
                                                                                    <option value="2006">2006</option>
                                                                                    <option value="2007">2007</option>
                                                                                    <option value="2008">2008</option>
                                                                                    <option value="2009">2009</option>
                                                                                    <option value="2010">2010</option>
                                                                                    <option value="2011">2011</option>
                                                                                    <option value="2012">2012</option>
                                                                                    <option value="2013">2013</option>
                                                                                    <option value="2014">2014</option>
                                                                                    <option value="2015">2015</option>
                                                                                    <option value="2016">2016</option>
                                                                                    <option value="2017">2017</option>
                                                                                    <option value="2018">2018</option>
                                                                                    <option value="2019">2019</option>
                                                                                    <option value="2020">2020</option>
                                                                                    <option value="2021">2021</option>
                                                                                    <option value="2022">2022</option>
                                                                                    <option value="2023">2023</option>
                                                                                    <option value="2024">2024</option>
                                                                                    <option value="2025">2025</option>
                                                                                </select>
                                                                            </td>
                                                                            <td valign="center" align="left">
                                                                                <!-- 
                                                                               <a name="anchor51" href="#" onclick="getmonth51();cal51.showCalendar('anchor51',getDateString(document.searchform.strCheckinyear,document.searchform.strCheckinmonth,document.searchform.strCheckindate,'1')); return false;" id="anchor10">
                                                                                <img width="18" height="14" border="0" src="Assets/images/hotels_calendar.jpg" onmouseover="if(!document.all){style.cursor='pointer'};style.cursor='hand';"/> 
                                                                               </a>
                                                                            -->
                                                                                <div style="background: rgb(143, 184, 245) none repeat scroll 0%; font-size: 5pt;
                                                                                    -moz-background-clip: -moz-initial; -moz-background-origin: -moz-initial; -moz-background-inline-policy: -moz-initial;
                                                                                    visibility: visible; font-family: courier; position: absolute;" id="testdiv1" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <table cellspacing="0" cellpadding="2" bordercolor="#111111" border="0" style="border-collapse: collapse;">
                                                                        <tr>
                                                                            <td valign="bottom" height="25" colspan="2">
                                                                                <font class="tabwht">Check-out:</font></td>
                                                                            <td valign="center" nowrap="">
                                                                                <select onchange="setDates();" class="homeselect" name="strCheckoutdate" id="strCheckoutdate"
                                                                                    runat="server" size="1">
                                                                                    <option value="01">1</option>
                                                                                    <option value="02">2</option>
                                                                                    <option value="03">3</option>
                                                                                    <option value="04">4</option>
                                                                                    <option value="05">5</option>
                                                                                    <option value="06">6</option>
                                                                                    <option value="07">7</option>
                                                                                    <option value="08">8</option>
                                                                                    <option value="09">9</option>
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
                                                                                    <option value="24">24</option>
                                                                                    <option value="25">25</option>
                                                                                    <option value="26">26</option>
                                                                                    <option value="27">27</option>
                                                                                    <option value="28">28</option>
                                                                                    <option value="29">29</option>
                                                                                    <option value="30">30</option>
                                                                                    <option value="31">31</option>
                                                                                </select>
                                                                                <select onchange="setDates();" class="homeselect" id="strCheckoutmonth" runat="server"
                                                                                    name="strCheckoutmonth" size="1">
                                                                                    <option value="01">Jan</option>
                                                                                    <option value="02">Feb</option>
                                                                                    <option value="03">Mar</option>
                                                                                    <option value="04">Apr</option>
                                                                                    <option value="05">May</option>
                                                                                    <option value="06">Jun</option>
                                                                                    <option value="07">Jul</option>
                                                                                    <option value="08">Aug</option>
                                                                                    <option value="09">Sep</option>
                                                                                    <option value="10">Oct</option>
                                                                                    <option value="11">Nov</option>
                                                                                    <option value="12">Dec</option>
                                                                                </select>
                                                                                <select onchange="setDates();" class="homeselect" id="strCheckoutyear" runat="server"
                                                                                    name="strCheckoutyear" size="1">
                                                                                    <option value="2006">2006</option>
                                                                                    <option value="2007">2007</option>
                                                                                    <option  value="2008">2008</option>
                                                                                    <option value="2009">2009</option>
                                                                                    <option value="2010">2010</option>
                                                                                    <option value="2011">2011</option>
                                                                                    <option value="2012">2012</option>
                                                                                    <option value="2013">2013</option>
                                                                                    <option value="2014">2014</option>
                                                                                    <option value="2015">2015</option>
                                                                                    <option value="2016">2016</option>
                                                                                    <option value="2017">2017</option>
                                                                                    <option value="2018">2018</option>
                                                                                    <option value="2019">2019</option>
                                                                                    <option value="2020">2020</option>
                                                                                    <option value="2021">2021</option>
                                                                                    <option value="2022">2022</option>
                                                                                    <option value="2023">2023</option>
                                                                                    <option value="2024">2024</option>
                                                                                    <option value="2025">2025</option>
                                                                                </select>
                                                                            </td>
                                                                            <td valign="center" align="left">
                                                                                <!--<a name="anchor52" href="#" onclick="getmonthout52();cal52.showCalendar('anchor52',getDateString(document.searchform.strCheckoutyear,document.searchform.strCheckoutmonth,document.searchform.strCheckoutdate,'1')); return false;" id="anchor11"><img width="18" height="14" border="0" src="Assets/images/hotels_calendar.jpg" onmouseover="if(!document.all){style.cursor='pointer'};style.cursor='hand';"/> </a>-->
                                                                                <div style="background: rgb(143, 184, 245) none repeat scroll 0%; font-size: 5pt;
                                                                                    -moz-background-clip: -moz-initial; -moz-background-origin: -moz-initial; -moz-background-inline-policy: -moz-initial;
                                                                                    visibility: hidden; font-family: courier; position: absolute;" id="testdiv2" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table width="100%" cellspacing="0" cellpadding="1" border="0">
                                                            <tr>
                                                                <td>
                                                                    <table width="100%" cellspacing="0" cellpadding="0" border="0" style="border-collapse: collapse;">
                                                                        <tr>
                                                                            <td width="25%" class="tabwht">
                                                                                No. of Rooms</td>
                                                                            <td width="5%">
                                                                            </td>
                                                                            <td width="35%" class="tabwht">
                                                                                Adults: (age 18+)</td>
                                                                            <td width="35%" class="tabwht">
                                                                                Children: (0 - 18)</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="4">
                                                                                <table width="100%" cellspacing="0" border="0" style="display: block;" id="room1">
                                                                                    <tr>
                                                                                        <td width="85" style="height: 21px">
                                                                                            <select onchange="javascript:showRooms(this.value)" id="strRooms" runat="server"
                                                                                                name="strRooms" size="1">
                                                                                                <option checked="" value="1">1</option>
                                                                                                <option value="2">2</option>
                                                                                                <option value="3">3</option>
                                                                                                <option value="4">4</option>
                                                                                            </select>
                                                                                        </td>
                                                                                        <td width="60" class="tabwht" style="height: 21px">
                                                                                            <div style="display: block;" id="tdname_1">
                                                                                                <b>Room 1</b></div>
                                                                                        </td>
                                                                                        <td width="135" style="height: 21px">
                                                                                            <select name="strAdultsRoom1" id="strAdultsRoom1" runat="server" size="1">
                                                                                                <option value="1">1</option>
                                                                                                <option checked="" value="2">2</option>
                                                                                                <option value="3">3</option>
                                                                                                <option value="4">4</option>
                                                                                            </select>
                                                                                        </td>
                                                                                        <td style="height: 21px">
                                                                                            <select onchange="javascript:showRoomsChildren('1',this.value)" name="strChildrenRoom1"
                                                                                                id="strChildrenRoom1" runat="server" size="1">
                                                                                                <option checked="" value="0">0</option>
                                                                                                <option value="1">1</option>
                                                                                                <option value="2">2</option>
                                                                                                <option value="3">3</option>
                                                                                            </select>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                                <table width="100%" cellspacing="0" style="display: none;" id="room2">
                                                                                    <tr>
                                                                                        <td width="85">
                                                                                            &nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                                                        <td width="60" class="tabwht">
                                                                                            <b>Room 2</b></td>
                                                                                        <td width="135">
                                                                                            <select name="strAdultsRoom2" id="strAdultsRoom2" runat="server" size="1">
                                                                                                <option value="1">1</option>
                                                                                                <option checked="" value="2">2</option>
                                                                                                <option value="3">3</option>
                                                                                                <option value="4">4</option>
                                                                                            </select>
                                                                                        </td>
                                                                                        <td>
                                                                                            <select onchange="javascript:showRoomsChildren('2',this.value)" name="strChildrenRoom2"
                                                                                                id="strChildrenRoom2" runat="server" size="1">
                                                                                                <option checked="" value="0">0</option>
                                                                                                <option value="1">1</option>
                                                                                                <option value="2">2</option>
                                                                                                <option value="3">3</option>
                                                                                            </select>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                                <table width="100%" cellspacing="0" style="display: none;" id="room3">
                                                                                    <tr>
                                                                                        <td width="85" style="height: 21px">
                                                                                            &nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                                                        <td width="60" class="tabwht" style="height: 21px">
                                                                                            <b>Room 3</b></td>
                                                                                        <td width="135" style="height: 21px">
                                                                                            <select name="strAdultsRoom3" id="strAdultsRoom3" runat="server" size="1">
                                                                                                <option value="1">1</option>
                                                                                                <option checked="" value="2">2</option>
                                                                                                <option value="3">3</option>
                                                                                                <option value="4">4</option>
                                                                                            </select>
                                                                                        </td>
                                                                                        <td style="height: 21px">
                                                                                            <select onchange="javascript:showRoomsChildren('3',this.value)" id="strChildrenRoom3"
                                                                                                name="strChildrenRoom3" runat="server" size="1">
                                                                                                <option checked="" value="0">0</option>
                                                                                                <option value="1">1</option>
                                                                                                <option value="2">2</option>
                                                                                                <option value="3">3</option>
                                                                                            </select>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                                <table width="100%" cellspacing="0" style="display: none;" id="room4">
                                                                                    <tr>
                                                                                        <td width="85" style="height: 21px">
                                                                                            &nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                                                        <td width="60" class="tabwht" style="height: 21px">
                                                                                            <b>Room 4</b></td>
                                                                                        <td width="135" style="height: 21px">
                                                                                            <select name="strAdultsRoom4" id="strAdultsRoom4" runat="server" size="1">
                                                                                                <option value="1">1</option>
                                                                                                <option checked="" value="2">2</option>
                                                                                                <option value="3">3</option>
                                                                                                <option value="4">4</option>
                                                                                            </select>
                                                                                        </td>
                                                                                        <td style="height: 21px">
                                                                                            <select onchange="javascript:showRoomsChildren('4',this.value)" name="strChildrenRoom4"
                                                                                                id="strChildrenRoom4" runat="server" size="1">
                                                                                                <option checked="" value="0">0</option>
                                                                                                <option value="1">1</option>
                                                                                                <option value="2">2</option>
                                                                                                <option value="3">3</option>
                                                                                            </select>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                                <table width="100%" cellspacing="0" style="display: none;" id="room5">
                                                                                    <tr>
                                                                                        <td width="85" style="height: 21px">
                                                                                            &nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                                                        <td width="60" class="tabwht" style="height: 21px">
                                                                                            <b>Room 5</b></td>
                                                                                        <td width="135" style="height: 21px">
                                                                                            <select name="strAdultsRoom5" id="strAdultsRoom5" runat="server" size="1">
                                                                                                <option value="1">1</option>
                                                                                                <option checked="" value="2">2</option>
                                                                                                <option value="3">3</option>
                                                                                                <option value="4">4</option>
                                                                                            </select>
                                                                                        </td>
                                                                                        <td style="height: 21px">
                                                                                            <select onchange="javascript:showRoomsChildren('5',this.value)" name="strChildrenRoom5"
                                                                                                size="1">
                                                                                                <option checked="" value="0">0</option>
                                                                                                <option value="1">1</option>
                                                                                                <option value="2">2</option>
                                                                                                <option value="3">3</option>
                                                                                            </select>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                                <table width="100%" cellspacing="0" style="display: none;" id="room6">
                                                                                    <tr>
                                                                                        <td width="85">
                                                                                            &nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                                                        <td width="60" class="tabwht">
                                                                                            <b>Room 6</b></td>
                                                                                        <td width="135">
                                                                                            <select name="strAdultsRoom6" id="strAdultsRoom6" runat="server" size="1">
                                                                                                <option value="1">1</option>
                                                                                                <option checked="" value="2">2</option>
                                                                                                <option value="3">3</option>
                                                                                                <option value="4">4</option>
                                                                                            </select>
                                                                                        </td>
                                                                                        <td>
                                                                                            <select onchange="javascript:showRoomsChildren('6',this.value)" name="strChildrenRoom6"
                                                                                                size="1">
                                                                                                <option checked="" value="0">0</option>
                                                                                                <option value="1">1</option>
                                                                                                <option value="2">2</option>
                                                                                                <option value="3">3</option>
                                                                                            </select>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                                <table width="100%" cellspacing="0" style="display: none;" id="room7">
                                                                                    <tr>
                                                                                        <td width="85">
                                                                                            &nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                                                        <td width="60" class="tabwht">
                                                                                            <b>Room 7</b></td>
                                                                                        <td width="135">
                                                                                            <select name="strAdultsRoom7" id="strAdultsRoom7" runat="server" size="1">
                                                                                                <option value="1">1</option>
                                                                                                <option checked="" value="2">2</option>
                                                                                                <option value="3">3</option>
                                                                                                <option value="4">4</option>
                                                                                            </select>
                                                                                        </td>
                                                                                        <td>
                                                                                            <select onchange="javascript:showRoomsChildren('7',this.value)" name="strChildrenRoom7"
                                                                                                size="1">
                                                                                                <option checked="" value="0">0</option>
                                                                                                <option value="1">1</option>
                                                                                                <option value="2">2</option>
                                                                                                <option value="3">3</option>
                                                                                            </select>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                                <table width="100%" cellspacing="0" style="display: none;" id="room8">
                                                                                    <tr>
                                                                                        <td width="85">
                                                                                            &nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                                                        <td width="60" class="tabwht">
                                                                                            <b>Room 8</b></td>
                                                                                        <td width="135">
                                                                                            <select name="strAdultsRoom8" id="strAdultsRoom8" runat="server" size="1">
                                                                                                <option value="1">1</option>
                                                                                                <option checked="" value="2">2</option>
                                                                                                <option value="3">3</option>
                                                                                                <option value="4">4</option>
                                                                                            </select>
                                                                                        </td>
                                                                                        <td>
                                                                                            <select onchange="javascript:showRoomsChildren('8',this.value)" name="strChildrenRoom8"
                                                                                                size="1">
                                                                                                <option checked="" value="0">0</option>
                                                                                                <option value="1">1</option>
                                                                                                <option value="2">2</option>
                                                                                                <option value="3">3</option>
                                                                                            </select>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <br />
                                                                    <table width="100%" cellspacing="0" cellpadding="0" border="0" style="display: none;"
                                                                        id="childrenAge">
                                                                        <tr>
                                                                            <td valign="top" nowrap="nowrap" class="txtwht">
                                                                                <b>Specify ages of children at time of travel.</b><br />
                                                                                Discounts may be offered to children of certain ages.
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <table width="100%" style="display: none;" id="roomChild1">
                                                                                    <tr>
                                                                                        <td width="12%" nowrap="nowrap" class="tabwht">
                                                                                            <strong>
                                                                                                <img width="1" height="22" src="Assets/images/blank.gif" />Room 1</strong></td>
                                                                                        <td>
                                                                                            <table>
                                                                                                <tr>
                                                                                                    <td class="txtwht">
                                                                                                        <div style="display: none;" id="r1c1">
                                                                                                            Child 1
                                                                                                            <br />
                                                                                                            <select name="strAgeChild1Room1" id="strAgeChild1Room1" runat="server" size="1">
                                                                                                                <option value="-1">-?-</option>
                                                                                                                <option value="0">< 1</option>
                                                                                                                <option value="1">1</option>
                                                                                                                <option value="2">2</option>
                                                                                                                <option value="3">3</option>
                                                                                                                <option value="4">4</option>
                                                                                                                <option value="5">5</option>
                                                                                                                <option value="6">6</option>
                                                                                                                <option value="7">7</option>
                                                                                                                <option value="8">8</option>
                                                                                                                <option value="9">9</option>
                                                                                                                <option value="10">10</option>
                                                                                                                <option value="11">11</option>
                                                                                                                <option value="12">12</option>
                                                                                                                <option value="13">13</option>
                                                                                                                <option value="14">14</option>
                                                                                                                <option value="15">15</option>
                                                                                                                <option value="16">16</option>
                                                                                                                <option value="17">17</option>
                                                                                                                <option value="18">18</option>
                                                                                                            </select>
                                                                                                        </div>
                                                                                                    </td>
                                                                                                    <td class="txtwht">
                                                                                                        <div style="display: none;" id="r1c2">
                                                                                                            Child 2
                                                                                                            <br />
                                                                                                            <select name="strAgeChild2Room1" id="strAgeChild2Room1" runat="server" size="1">
                                                                                                                <option value="-1">-?-</option>
                                                                                                                <option value="0">< 1</option>
                                                                                                                <option value="1">1</option>
                                                                                                                <option value="2">2</option>
                                                                                                                <option value="3">3</option>
                                                                                                                <option value="4">4</option>
                                                                                                                <option value="5">5</option>
                                                                                                                <option value="6">6</option>
                                                                                                                <option value="7">7</option>
                                                                                                                <option value="8">8</option>
                                                                                                                <option value="9">9</option>
                                                                                                                <option value="10">10</option>
                                                                                                                <option value="11">11</option>
                                                                                                                <option value="12">12</option>
                                                                                                                <option value="13">13</option>
                                                                                                                <option value="14">14</option>
                                                                                                                <option value="15">15</option>
                                                                                                                <option value="16">16</option>
                                                                                                                <option value="17">17</option>
                                                                                                                <option value="18">18</option>
                                                                                                            </select>
                                                                                                        </div>
                                                                                                    </td>
                                                                                                    <td class="txtwht">
                                                                                                        <div style="display: none;" id="r1c3">
                                                                                                            Child 3
                                                                                                            <br />
                                                                                                            <select name="strAgeChild3Room1" id="strAgeChild3Room1" runat="server" size="1">
                                                                                                                <option value="-1">-?-</option>
                                                                                                                <option value="0">< 1</option>
                                                                                                                <option value="1">1</option>
                                                                                                                <option value="2">2</option>
                                                                                                                <option value="3">3</option>
                                                                                                                <option value="4">4</option>
                                                                                                                <option value="5">5</option>
                                                                                                                <option value="6">6</option>
                                                                                                                <option value="7">7</option>
                                                                                                                <option value="8">8</option>
                                                                                                                <option value="9">9</option>
                                                                                                                <option value="10">10</option>
                                                                                                                <option value="11">11</option>
                                                                                                                <option value="12">12</option>
                                                                                                                <option value="13">13</option>
                                                                                                                <option value="14">14</option>
                                                                                                                <option value="15">15</option>
                                                                                                                <option value="16">16</option>
                                                                                                                <option value="17">17</option>
                                                                                                                <option value="18">18</option>
                                                                                                            </select>
                                                                                                        </div>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                                <table width="100%" style="display: none;" id="roomChild2">
                                                                                    <tr>
                                                                                        <td width="12%" nowrap="nowrap" class="tabwht">
                                                                                            <strong>
                                                                                                <img width="1" height="22" src="Assets/images/blank.gif" />Room 2</strong></td>
                                                                                        <td>
                                                                                            <table>
                                                                                                <tr>
                                                                                                    <td class="txtwht">
                                                                                                        <div style="display: none;" id="r2c1">
                                                                                                            Child 1
                                                                                                            <br />
                                                                                                            <select name="strAgeChild1Room2" id="strAgeChild1Room2" runat="server" size="1">
                                                                                                                <option value="-1">-?-</option>
                                                                                                                <option value="0">< 1</option>
                                                                                                                <option value="1">1</option>
                                                                                                                <option value="2">2</option>
                                                                                                                <option value="3">3</option>
                                                                                                                <option value="4">4</option>
                                                                                                                <option value="5">5</option>
                                                                                                                <option value="6">6</option>
                                                                                                                <option value="7">7</option>
                                                                                                                <option value="8">8</option>
                                                                                                                <option value="9">9</option>
                                                                                                                <option value="10">10</option>
                                                                                                                <option value="11">11</option>
                                                                                                                <option value="12">12</option>
                                                                                                                <option value="13">13</option>
                                                                                                                <option value="14">14</option>
                                                                                                                <option value="15">15</option>
                                                                                                                <option value="16">16</option>
                                                                                                                <option value="17">17</option>
                                                                                                                <option value="18">18</option>
                                                                                                            </select>
                                                                                                        </div>
                                                                                                    </td>
                                                                                                    <td class="txtwht">
                                                                                                        <div style="display: none;" id="r2c2">
                                                                                                            Child 2
                                                                                                            <br />
                                                                                                            <select name="strAgeChild2Room2" id="strAgeChild2Room2" runat="server" size="1">
                                                                                                                <option value="-1">-?-</option>
                                                                                                                <option value="0">< 1</option>
                                                                                                                <option value="1">1</option>
                                                                                                                <option value="2">2</option>
                                                                                                                <option value="3">3</option>
                                                                                                                <option value="4">4</option>
                                                                                                                <option value="5">5</option>
                                                                                                                <option value="6">6</option>
                                                                                                                <option value="7">7</option>
                                                                                                                <option value="8">8</option>
                                                                                                                <option value="9">9</option>
                                                                                                                <option value="10">10</option>
                                                                                                                <option value="11">11</option>
                                                                                                                <option value="12">12</option>
                                                                                                                <option value="13">13</option>
                                                                                                                <option value="14">14</option>
                                                                                                                <option value="15">15</option>
                                                                                                                <option value="16">16</option>
                                                                                                                <option value="17">17</option>
                                                                                                                <option value="18">18</option>
                                                                                                            </select>
                                                                                                        </div>
                                                                                                    </td>
                                                                                                    <td class="txtwht">
                                                                                                        <div style="display: none;" id="r2c3">
                                                                                                            Child 3
                                                                                                            <br />
                                                                                                            <select name="strAgeChild3Room2" id="strAgeChild3Room2" runat="server" size="1">
                                                                                                                <option value="-1">-?-</option>
                                                                                                                <option value="0">< 1</option>
                                                                                                                <option value="1">1</option>
                                                                                                                <option value="2">2</option>
                                                                                                                <option value="3">3</option>
                                                                                                                <option value="4">4</option>
                                                                                                                <option value="5">5</option>
                                                                                                                <option value="6">6</option>
                                                                                                                <option value="7">7</option>
                                                                                                                <option value="8">8</option>
                                                                                                                <option value="9">9</option>
                                                                                                                <option value="10">10</option>
                                                                                                                <option value="11">11</option>
                                                                                                                <option value="12">12</option>
                                                                                                                <option value="13">13</option>
                                                                                                                <option value="14">14</option>
                                                                                                                <option value="15">15</option>
                                                                                                                <option value="16">16</option>
                                                                                                                <option value="17">17</option>
                                                                                                                <option value="18">18</option>
                                                                                                            </select>
                                                                                                        </div>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                                <table width="100%" style="display: none;" id="roomChild3">
                                                                                    <tr>
                                                                                        <td width="12%" nowrap="nowrap" class="tabwht">
                                                                                            <strong>
                                                                                                <img width="1" height="22" src="Assets/images/blank.gif" />Room 3</strong></td>
                                                                                        <td>
                                                                                            <table class="txtwht">
                                                                                                <tr>
                                                                                                    <td class="txtwht">
                                                                                                        <div style="display: none;" id="r3c1">
                                                                                                            Child 1
                                                                                                            <br />
                                                                                                            <select name="strAgeChild1Room3" id="strAgeChild1Room3" runat="server" size="1">
                                                                                                                <option value="-1">-?-</option>
                                                                                                                <option value="0">< 1</option>
                                                                                                                <option value="1">1</option>
                                                                                                                <option value="2">2</option>
                                                                                                                <option value="3">3</option>
                                                                                                                <option value="4">4</option>
                                                                                                                <option value="5">5</option>
                                                                                                                <option value="6">6</option>
                                                                                                                <option value="7">7</option>
                                                                                                                <option value="8">8</option>
                                                                                                                <option value="9">9</option>
                                                                                                                <option value="10">10</option>
                                                                                                                <option value="11">11</option>
                                                                                                                <option value="12">12</option>
                                                                                                                <option value="13">13</option>
                                                                                                                <option value="14">14</option>
                                                                                                                <option value="15">15</option>
                                                                                                                <option value="16">16</option>
                                                                                                                <option value="17">17</option>
                                                                                                                <option value="18">18</option>
                                                                                                            </select>
                                                                                                        </div>
                                                                                                    </td>
                                                                                                    <td class="txtwht">
                                                                                                        <div style="display: none;" id="r3c2">
                                                                                                            Child 2
                                                                                                            <br />
                                                                                                            <select name="strAgeChild2Room3" id="strAgeChild2Room3" runat="server" size="1">
                                                                                                                <option value="-1">-?-</option>
                                                                                                                <option value="0">< 1</option>
                                                                                                                <option value="1">1</option>
                                                                                                                <option value="2">2</option>
                                                                                                                <option value="3">3</option>
                                                                                                                <option value="4">4</option>
                                                                                                                <option value="5">5</option>
                                                                                                                <option value="6">6</option>
                                                                                                                <option value="7">7</option>
                                                                                                                <option value="8">8</option>
                                                                                                                <option value="9">9</option>
                                                                                                                <option value="10">10</option>
                                                                                                                <option value="11">11</option>
                                                                                                                <option value="12">12</option>
                                                                                                                <option value="13">13</option>
                                                                                                                <option value="14">14</option>
                                                                                                                <option value="15">15</option>
                                                                                                                <option value="16">16</option>
                                                                                                                <option value="17">17</option>
                                                                                                                <option value="18">18</option>
                                                                                                            </select>
                                                                                                        </div>
                                                                                                    </td>
                                                                                                    <td class="txtwht">
                                                                                                        <div style="display: none;" id="r3c3">
                                                                                                            Child 3
                                                                                                            <br />
                                                                                                            <select name="strAgeChild3Room3" id="strAgeChild3Room3" runat="server" size="1">
                                                                                                                <option value="-1">-?-</option>
                                                                                                                <option value="0">< 1</option>
                                                                                                                <option value="1">1</option>
                                                                                                                <option value="2">2</option>
                                                                                                                <option value="3">3</option>
                                                                                                                <option value="4">4</option>
                                                                                                                <option value="5">5</option>
                                                                                                                <option value="6">6</option>
                                                                                                                <option value="7">7</option>
                                                                                                                <option value="8">8</option>
                                                                                                                <option value="9">9</option>
                                                                                                                <option value="10">10</option>
                                                                                                                <option value="11">11</option>
                                                                                                                <option value="12">12</option>
                                                                                                                <option value="13">13</option>
                                                                                                                <option value="14">14</option>
                                                                                                                <option value="15">15</option>
                                                                                                                <option value="16">16</option>
                                                                                                                <option value="17">17</option>
                                                                                                                <option value="18">18</option>
                                                                                                            </select>
                                                                                                        </div>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                                <table width="100%" style="display: none;" id="roomChild4">
                                                                                    <tr>
                                                                                        <td width="12%" nowrap="nowrap" class="tabwht">
                                                                                            <strong>
                                                                                                <img width="1" height="22" src="Assets/images/blank.gif" />Room 4</strong></td>
                                                                                        <td>
                                                                                            <table class="txtwht">
                                                                                                <tr>
                                                                                                    <td class="txtwht">
                                                                                                        <div style="display: none;" id="r4c1">
                                                                                                            Child 1
                                                                                                            <br />
                                                                                                            <select name="strAgeChild1Room4" id="strAgeChild1Room4" runat="server" size="1">
                                                                                                                <option value="-1">-?-</option>
                                                                                                                <option value="0">< 1</option>
                                                                                                                <option value="1">1</option>
                                                                                                                <option value="2">2</option>
                                                                                                                <option value="3">3</option>
                                                                                                                <option value="4">4</option>
                                                                                                                <option value="5">5</option>
                                                                                                                <option value="6">6</option>
                                                                                                                <option value="7">7</option>
                                                                                                                <option value="8">8</option>
                                                                                                                <option value="9">9</option>
                                                                                                                <option value="10">10</option>
                                                                                                                <option value="11">11</option>
                                                                                                                <option value="12">12</option>
                                                                                                                <option value="13">13</option>
                                                                                                                <option value="14">14</option>
                                                                                                                <option value="15">15</option>
                                                                                                                <option value="16">16</option>
                                                                                                                <option value="17">17</option>
                                                                                                                <option value="18">18</option>
                                                                                                            </select>
                                                                                                        </div>
                                                                                                    </td>
                                                                                                    <td class="txtwht">
                                                                                                        <div style="display: none;" id="r4c2">
                                                                                                            Child 2
                                                                                                            <br />
                                                                                                            <select name="strAgeChild2Room4" id="strAgeChild2Room4" runat="server" size="1">
                                                                                                                <option value="-1">-?-</option>
                                                                                                                <option value="0">< 1</option>
                                                                                                                <option value="1">1</option>
                                                                                                                <option value="2">2</option>
                                                                                                                <option value="3">3</option>
                                                                                                                <option value="4">4</option>
                                                                                                                <option value="5">5</option>
                                                                                                                <option value="6">6</option>
                                                                                                                <option value="7">7</option>
                                                                                                                <option value="8">8</option>
                                                                                                                <option value="9">9</option>
                                                                                                                <option value="10">10</option>
                                                                                                                <option value="11">11</option>
                                                                                                                <option value="12">12</option>
                                                                                                                <option value="13">13</option>
                                                                                                                <option value="14">14</option>
                                                                                                                <option value="15">15</option>
                                                                                                                <option value="16">16</option>
                                                                                                                <option value="17">17</option>
                                                                                                                <option value="18">18</option>
                                                                                                            </select>
                                                                                                        </div>
                                                                                                    </td>
                                                                                                    <td class="txtwht">
                                                                                                        <div style="display: none;" id="r4c3">
                                                                                                            Child 3
                                                                                                            <br />
                                                                                                            <select name="strAgeChild3Room4" id="strAgeChild3Room4" runat="server" size="1">
                                                                                                                <option value="-1">-?-</option>
                                                                                                                <option value="0">< 1</option>
                                                                                                                <option value="1">1</option>
                                                                                                                <option value="2">2</option>
                                                                                                                <option value="3">3</option>
                                                                                                                <option value="4">4</option>
                                                                                                                <option value="5">5</option>
                                                                                                                <option value="6">6</option>
                                                                                                                <option value="7">7</option>
                                                                                                                <option value="8">8</option>
                                                                                                                <option value="9">9</option>
                                                                                                                <option value="10">10</option>
                                                                                                                <option value="11">11</option>
                                                                                                                <option value="12">12</option>
                                                                                                                <option value="13">13</option>
                                                                                                                <option value="14">14</option>
                                                                                                                <option value="15">15</option>
                                                                                                                <option value="16">16</option>
                                                                                                                <option value="17">17</option>
                                                                                                                <option value="18">18</option>
                                                                                                            </select>
                                                                                                        </div>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <div style="visibility: hidden;">
                                                                        <b>Currency</b>
                                                                        <!-------------------------------  CURRENCY-->
                                                                        <input type="radio" checked="" value="INR" name="currency" class="formField" />
                                                                        INR &nbsp;
                                                                        <input type="radio" value="USD" name="currency" class="formField" />
                                                                        USD
                                                                        <!-------------------------------  END CURRENCY-->
                                                                    </div>
                                                                    &nbsp;&nbsp;<input id="btnHhotelSearch" visible="true" style="background-color: #edeefb; display:none;" runat="server"
                                                                        type="submit" onclick="return validateInput();" value="Search" onserverclick="btnHhotelSearch_ServerClick"  />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                            <!-- Main Content End -->
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
                    <img src="Assets/images/left_d_corner.gif" width="8" height="8" /></td>
                <td bgcolor="#E7E7E7">
                    <img src="Assets/images/trans.gif" width="1" height="1" /></td>
                <td align="right">
                    <img src="Assets/images/rgt_d_corner.gif" width="8" height="8" /></td>
            </tr>
        </table>
    </form>
    <map name="Map" id="Map">
        <area shape="circle" coords="236,53,27" href="Agentspecial.aspx" title="Book Your Special Package" />
        <area shape="circle" coords="171,53,27" href="Agentcarbooking.aspx" title="Book Your Car" />
        <area shape="circle" coords="45,52,27" href="Agenttour.aspx" title="Build your Package" />
    </map>
</body>
</html>
