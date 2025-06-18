<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgentHotelBooking.aspx.cs" Inherits="SouthernTravelIndiaAgent.AgentHotelBooking" %>


<%@ Register TagPrefix="uc1" TagName="AgentHeader" Src="UserControls/UcAgentHeader.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AgentFooter" Src="UserControls/UcAgentFooter.ascx" %>
<%@ Register Src="UserControls/GST_ucManageCustomer.ascx" TagName="ucManageCustomer"
    TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Southern India Travel,South India Travel Packages,Travel Packages to South India
    </title>
    <link href="../Assets/css/stylesheet.css" type="text/css" rel="stylesheet" />
    <link href="../Assets/css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/demos.css" rel="stylesheet" type="text/css" />
    <link type="text/css" href="/css/smoothness/jquery-ui-1.7.1.custom_blue.css" rel="stylesheet" />

    <script language="javascript" src="../Assets/js/MyScript.js" type="text/javascript"></script>
            <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

    <script src="../css/Js/jquery-1.3.2.min.js" type="text/javascript"></script>

    <script src="../css/Js/jquery-ui-1.7.1.custom.min.js" type="text/javascript"></script>

    <script language="javascript" src="../Assets/js/md5.js" type="text/javascript"></script>

    <style>
        .DatePickerImage
        {
            position: relative;
            padding-left: 5px;
        }
        .mGrid
        {
            width: 100%;
            background-color: #fff;
            margin: 5px 0 10px 0;
            border: solid 1px #f0f0f0;
            border-collapse: collapse;
        }
        .mGrid td
        {
            padding: 2px;
            border: solid 1px #f0f0f0;
            color: #717171;
            text-align: left;
        }
        .mGrid th
        {
            padding: 4px 2px;
            color: #fff;
            text-align: left;
            background: #5aa1ea;
            border-left: solid 1px #f0f0f0;
            font-size: 0.9em;
        }
        .mGrid .alt
        {
            background: #f0f0f0;
        }
        .mGrid .pgr
        {
            background: #424242;
        }
        .mGrid .pgr table
        {
            margin: 5px 0;
        }
        .mGrid .pgr td
        {
            border-width: 0;
            padding: 0 6px;
            border-left: solid 1px #666;
            text-align: left;
            font-weight: bold;
            color: #fff;
            line-height: 12px;
        }
        .mGrid .pgr a
        {
            color: #666;
            text-decoration: none;
        }
        .mGrid .pgr a:hover
        {
            color: #000;
            text-decoration: none;
        }
        .mGrid .ftr
        {
            background: #424242;
        }
        .mGrid .ftr table
        {
            margin: 0px 0;
        }
        .mGrid .ftr td
        {
            border-width: 0;
            padding: 0 6px;
            border-left: solid 1px #666;
            text-align: left;
            font-weight: bold;
            color: #fff;
            line-height: 12px;
        }
        .mGrid .ftr a
        {
            color: #666;
            text-decoration: none;
        }
        .mGrid .ftr a:hover
        {
            color: #000;
            text-decoration: none;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server" defaultbutton="Submit">
         <script type="text/javascript">

             $(function () {
                 var checkInDate = $('#txtarr');
                 var checkOutDate = $('#txtdep');
                 var dt = '';
                 $("#txtarr").datepicker({

                     numberOfMonths: 2,
                     showOn: "button", autoSize: true,
                     buttonImage: "../Assets/images/cal.gif",
                     buttonImageOnly: true,
                     minDate: new Date(),
                     closeText: 'Close',
                     dateFormat: 'dd/mm/yy', buttonText: 'Choose a Date',
                     onSelect: function (date) {

                         dt = checkInDate.datepicker('getDate');
                         var Tow = dt;
                         Tow.setDate(dt.getDate() + 1); //get yesterday's date
                         //alert(Tow);
                         $('#txtdep').datepicker('option', 'defaultDate', new Date(dt));
                         $("#txtdep").datepicker("option", "minDate", new Date(dt));
                         $('img.ui-datepicker-trigger').css({ 'cursor': 'pointer', "vertical-align": 'top' });
                         $('img.ui-datepicker-trigger').addClass('DatePickerImage');
                     },
                     onClose: function clearEndDate(date, inst) {

                         if (dt == '') {
                             checkOutDate.val('');
                         }
                         else {
                             var today = new Date(dt);
                             var dd = today.getDate();
                             var mm = today.getMonth() + 1; //January is 0!
                             var yyyy = today.getFullYear();
                             if (dd < 10) { dd = '0' + dd }
                             if (mm < 10) { mm = '0' + mm }

                             checkOutDate.val(dd + '/' + mm + '/' + yyyy);
                         }
                     }
                 });


                 $("#txtdep").datepicker({
                     numberOfMonths: 2,
                     //  maxDate:"+4Y",
                     buttonImage: "../Assets/images/cal.gif",
                     buttonImageOnly: true,
                     minDate: new Date(), dateFormat: 'dd/mm/yy', buttonText: 'Choose a Date',
                     showOn: 'button', autoSize: true,
                     closeText: 'Close', onSelect: function () { }

                 });
                 $('img.ui-datepicker-trigger').css({ 'cursor': 'pointer', "vertical-align": 'top' });
                 $('img.ui-datepicker-trigger').addClass('DatePickerImage');
             });

             $(function () {


                 $("#txtpaydate").datepicker({

                     numberOfMonths: 1,
                     showOn: "button", autoSize: true,
                     buttonImage: "../Assets/images/cal.gif",
                     buttonImageOnly: true,
                     minDate: new Date(),
                     closeText: 'Close',
                     dateFormat: 'dd/mm/yy', buttonText: 'Choose a Date'
                 });
                 $('img.ui-datepicker-trigger').css({ 'cursor': 'pointer', "vertical-align": 'top' });
                 $('img.ui-datepicker-trigger').addClass('DatePickerImage');
             });


         </script>

 <script language="javascript" type="text/javascript">
     function isNumberKey(evt) {
         var charCode = (evt.which) ? evt.which : event.keyCode
         if (charCode > 31 && (charCode < 48 || charCode > 57))
             return false;
         return true;
     }
     function datecheck() {

         document.getElementById('<%=txtdep.ClientID %>').value = "";
         document.getElementById('<%=ddlarrivalhr.ClientID %>').value = '0';
         document.getElementById('<%=ddlarrivalmin.ClientID %>').value = '0';
         document.getElementById('<%=ddlDeparture.ClientID %>').value = '0';
         document.getElementById('<%=ddlDeparturemin.ClientID %>').value = '0';

     }



     function calcamount(indexID) {
         validate1();
         //***********************************************************************
         calcdays();
         //******************************************************************************************* 

         var sfareamount = 0;
         var sTaxfareamount = 0;
         var snooftotaldays = 1;
         if (Trim(document.getElementById("txttotaldays").value) != "") {
             snooftotaldays = parseInt(document.getElementById('txttotaldays').value);
         }
         //alert(document.getElementById("txttotaldays").value);
         //alert(document.getElementById("txtnodays1").value);

         var noofdays = document.getElementById("txtnodays1").value;
         var TotalDays = document.getElementById("txtnodays1").value;

         var grid = document.getElementById('<%= this.gvRoomType.ClientID %>');

         var ddlRoom = document.getElementById("ddlNoRoom").value;
         var ddlAdults = document.getElementById("ddlAdults").value;
         var ddlChilds = document.getElementById("ddlChilds").value;

         var hdFare = grid.rows[indexID + 1].cells[1].getElementsByTagName("*")[1];
         var hdDisFare = grid.rows[indexID + 1].cells[1].getElementsByTagName("*")[3];
         var Disnoofdays = grid.rows[indexID + 1].cells[1].getElementsByTagName("*")[4];
         var hdOrgFare = grid.rows[indexID + 1].cells[1].getElementsByTagName("*")[5];

         //alert(Disnoofdays.value);
         var room = ddlRoom;

         var val = hdFare.value;
         var DisCountFare = 0;


         // alert(DisCountFare);
         var val = hdFare.value;

         if (room != "0") {

             var Extval = grid.rows[indexID + 1].cells[1].getElementsByTagName("*")[2].value;
             debugger;
             var lCWOBed = grid.rows[indexID + 1].cells[1].getElementsByTagName("*")[9].value;
             debugger;
             var chkval = 0;
             if (ddlAdults != 0) {
                 var Adults = ddlAdults;

                 var SelValue = 0;
                 var myindex = ddlAdults.length;
                 for (var k = 0; k < ddlAdults.length; k++) {
                     SelValue = ddlAdults;
                     var SelText = ddlAdults;
                 }
                 if (parseInt(Adults) > (2 * parseInt(room))) {
                     chkval = parseInt(Adults) % (2 * parseInt(room));
                 }
             }
             if (parseInt(Disnoofdays.value) > 0) {
                 noofdays = parseInt(noofdays) - parseInt(Disnoofdays.value);
                 DisCountFare = parseInt(DisCountFare) + (parseInt(Disnoofdays.value) * parseInt(hdDisFare.value) * parseInt(room));
             }
             if (noofdays == 0) {
                 sfareamount = DisCountFare + sfareamount;
                 sTaxfareamount = sTaxfareamount + (parseInt(hdOrgFare.value) * parseInt(room) * parseInt(TotalDays));
             }
             else {
                 sfareamount = DisCountFare + sfareamount + (parseInt(val) * parseInt(room)) * parseInt(noofdays);
                 sTaxfareamount = sTaxfareamount + (parseInt(hdOrgFare.value) * parseInt(room)) * parseInt(TotalDays);
             }
             // alert(sTaxfareamount);
             if (chkval > 0) {
                 if (parseInt(Disnoofdays.value) > 0) {
                     noofdays = parseInt(noofdays) + parseInt(Disnoofdays.value);
                 }



                 sfareamount = sfareamount + (parseInt(Extval) * parseInt(chkval) * parseInt(noofdays));
                 sTaxfareamount = sTaxfareamount + (parseInt(Extval) * parseInt(chkval)) * parseInt(TotalDays);

             }
             // Add Child without bed calculation here in sfareamount and sTaxfareamount
             if (document.getElementById('hdnIsCalCWBFare').value == "1") {
                 sfareamount = sfareamount + (parseInt(lCWOBed) * parseInt(noofdays) * parseInt(document.getElementById('hdnTotalCWB').value));
                 sTaxfareamount = sTaxfareamount;  //+ (parseInt(lCWOBed) * parseInt(TotalDays) * parseInt(document.getElementById('hdnTotalCWB').value));
             }
             // End of  Add Child without bed calculation here in sfareamount and sTaxfareamount
         }
         document.getElementById('txtamt').value = sfareamount;
         var stPerCent = document.getElementById("stax").value;
         var stLTPerCent = document.getElementById("LTstax").value;
         var staxVal = Math.round(parseFloat((eval(sTaxfareamount) * eval(stPerCent)) / 100));
         var LTstaxVal = Math.round(parseFloat((eval(sTaxfareamount) * eval(stLTPerCent)) / 100));

         document.getElementById("txttax").value = parseFloat(LTstaxVal) + parseFloat(staxVal);
         var staxvalue = document.getElementById("txttax").value;
         var jtotalwithtax = parseFloat(sfareamount) + parseFloat(staxvalue);
         document.getElementById("txttotalamtwithtax").value = Math.round(jtotalwithtax);


     }

     function paymenttype() {
         if (document.getElementById('DDLPaymode')) {
             if (document.getElementById('DDLPaymode').value == "1") {
                 document.getElementById("txtBankname").value = "";
                 document.getElementById("txtChequeNo").value = "";
                 document.getElementById("txtpaydate").value = "";
                 document.getElementById("txtBankname").readOnly = true;
                 document.getElementById("txtChequeNo").readOnly = true;
                 //document.getElementById("txtpaydate").readOnly = true;

             }
             else {
                 if (document.getElementById('DDLPaymode').value == "2") {
                     document.getElementById("paymnetNo").innerHTML = "Trans No :";
                 }
                 else {
                     document.getElementById("paymnetNo").innerHTML = "Cheque/DD No:";
                 }

                 document.getElementById("txtBankname").readOnly = false;
                 document.getElementById("txtChequeNo").readOnly = false;
                 //document.getElementById("txtpaydate").readOnly = false;

             }
         }
     }
     function Emailvalid() {

         if (document.getElementById("txtSearch").value == "") {
             alert("Please Enter Email Id or Mobile No.");
             document.getElementById("txtSearch").focus();
             return false;
         }
         else {
             if (isNaN(document.getElementById("txtSearch").value) == true) {
                 if ((echeck(document.getElementById("txtSearch").value)) == false) {
                     document.getElementById("txtSearch").value = "";
                     document.getElementById("txtSearch").focus();
                     return false;
                 }
                 else {
                     document.frmbooking.type.value = "email";
                 }

             }
             else {
                 var a = document.getElementById("txtSearch").value;
                 if ((a.length < 10) | (a.length > 11)) {
                     alert("Invalid Mobile No")
                     document.getElementById("txtSearch").value = "";
                     document.getElementById("txtSearch").focus();
                     return false;
                 }
                 else {
                     document.frmbooking.type.value = "Mobile";
                 }

             }
         }

     }
     function valid() {
         var chek = true;
         if (Trim(document.getElementById("S_name").value) == "") {
             alert("Please Enter Name.");
             document.getElementById("S_name").focus();
             chek = false;
             return false;
         }


         if (Trim(document.getElementById("S_streetaddress").value) == "") {
             alert("Please Enter Address.");
             document.getElementById("S_streetaddress").focus();
             chek = false;
             return false;
         }
         if (Trim(document.getElementById("txtcity").value) == "" && document.frmbooking.ddlCountry.value == "59") {
             alert("Please Enter City Name.");
             document.getElementById("txtcity").focus();
             chek = false;
             return false;
         }
         if (Trim(document.getElementById("txtcity").value) == "" && document.frmbooking.ddlCountry.value == "59") {
             alert("Please Enter City Name.");
             document.getElementById("txtcity").focus();
             chek = false;
             return false;
         }
         if (Trim(document.getElementById("ddlState").value) == "") {
             alert("Please Select State Name.");
             document.getElementById("ddlState").focus();
             chek = false;
             return false;
         }
         if (Trim(document.getElementById("txtmobile").value) == "") {
             alert("Please Enter Mobile No.");
             document.frmbooking.txtmobile.focus();
             chek = false;
             return false;
         }
         if (Trim(document.getElementById("txtAlternateMobileNo").value) == "") {
             alert("Please Enter Emergency Contact No.");
             document.frmbooking.txtAlternateMobileNo.focus();
             chek = false;
             return false;
         }

         /*if (Trim(document.getElementById("S_email").value) == "")
         {
         alert("Please Enter Email Id.");
         document.getElementById("S_email").focus();		
         chek = false;
         return false;
         }
         if(echeck(document.getElementById("S_email").value)==false)
         {
         document.getElementById("S_email").value = "";
         document.getElementById("S_email").focus();
         chek = false;
         return false;
         }*/
         if (Trim(document.getElementById("S_email").value) != "") {
             if (echeck(document.getElementById("S_email").value) == false) {
                 document.getElementById("S_email").value = "";
                 document.getElementById("S_email").focus();
                 chek = false;
                 return false;
             }
         }
         if (document.getElementById('chkPickUp').checked) {
             //	

             if ((!document.getElementById('RadFlight').checked) && (!document.getElementById('RadTrain').checked) && (!document.getElementById('RadBus').checked)) {
                 alert("Please choose the pickUp option");
                 chek = false;
                 return false;
             }
             if ((document.getElementById('RadFlight').checked)) {

                 if (document.getElementById("txtpickVehicleNo").value == "Select") {
                     alert("Please choose flight type");
                     document.getElementById("txtpickVehicleNo").focus();
                     chek = false;
                     return false;
                 }
                 if ((document.getElementById("ddlPkHrs").value == "") || (document.getElementById("ddlPkMints").value == "")) {
                     alert("Please Select the  time.");
                     if (document.getElementById("ddlPkMints").value == "")
                         document.getElementById("ddlPkMints").focus();
                     if (document.getElementById("ddlPkHrs").value == "")
                         document.getElementById("ddlPkHrs").focus();

                     chek = false;
                     return false;
                 }
                 if (Trim(document.getElementById("txtFlightNo").value) == "") {
                     alert("Please enter Flight No ");
                     document.getElementById("txtFlightNo").focus();

                     chek = false;
                     return false;
                 }
             }
             else if ((document.getElementById('RadTrain').checked)) {
                 if (Trim(document.getElementById("txtRlyStationName").value) == "") {
                     alert("Please enter station name");
                     document.getElementById("txtRlyStationName").focus();

                     chek = false;
                     return false;
                 }
                 if ((document.getElementById("ddlTrainPkHr").value == "") || (document.getElementById("ddlTrainPkMin").value == "")) {
                     alert("Please Select the  time.");
                     if (document.getElementById("ddlTrainPkMin").value == "")
                         document.getElementById("ddlTrainPkMin").focus();
                     if (document.getElementById("ddlTrainPkHr").value == "")
                         document.getElementById("ddlTrainPkHr").focus();

                     chek = false;
                     return false;
                 }
                 if (Trim(document.getElementById("txtTrainNo").value) == "") {
                     alert("Please enter Train No ");
                     document.getElementById("txtTrainNo").focus();

                     chek = false;
                     return false;
                 }
             }
             else if ((document.getElementById('RadBus').checked)) {
                 if (Trim(document.getElementById("txtAddr").value) == "") {
                     alert("Please enter Pickup address");
                     document.getElementById("txtAddr").focus();

                     chek = false;
                     return false;
                 }
                 if (Trim(document.getElementById("txtStreet").value) == "") {
                     alert("Please enter Street");
                     document.getElementById("txtStreet").focus();
                     chek = false;

                     return false;
                 }
             }
         }

         if (chek) {
             document.getElementById('Submit').style.display = 'none';
         }
         return chek;
     }


     function validateRadioButtonList(radioButtonListId) {
         var listItemArray = document.getElementsByName(radioButtonListId);
         var isItemChecked = false;
         for (var i = 0; i < listItemArray.length; i++) {
             var listItem = listItemArray[i];

             if (listItem.checked) {
                 isItemChecked = true;
             }
         }
         if (isItemChecked == false) {
             Swal.fire({
                 icon: 'warning',
                 title: 'Oops...',
                 text: 'Please select Occupants.',
                 confirmButtonColor: '#f2572b'
             });
             return false;
         }
         return true;
     }
     function validate() {

         if (document.getElementById("ddlCity").value == "0") {
             Swal.fire({
                 icon: 'warning',
                 title: 'Oops...',
                 text: 'Please select City.',
                 confirmButtonColor: '#f2572b'
             });
             document.getElementById("ddlCity").focus();
             return false;
         }
         if (document.getElementById("ddlHotel").value == "" || document.getElementById("ddlHotel").value == "0") {
             Swal.fire({
                 icon: 'warning',
                 title: 'Oops...',
                 text: 'Please select Hotel.',
                 confirmButtonColor: '#f2572b'
             });
             document.getElementById("ddlHotel").focus();
             return false;
         }

         if (Trim(document.getElementById("txtarr").value) == "" || document.getElementById("txtarr").value == "dd/mm/yyyy") {
             Swal.fire({
                 icon: 'warning',
                 title: 'Oops...',
                 text: 'Please Select CheckIn Date.',
                 confirmButtonColor: '#f2572b'
             });
             document.getElementById("txtarr").focus();
             return false;
         }

         if (Trim(document.getElementById("txtdep").value) == "" || document.getElementById("txtdep").value == "dd/mm/yyyy") {
             alert('Please Select CheckOut Date.');
             document.getElementById("txtdep").focus();
             return false;
         }
         if (validateRadioButtonList('<%= rbtOccupants.ClientID %>') == false) {
             return false;
         }
         if (Trim(document.getElementById("ddlNoRoom").value) == "0" || document.getElementById("ddlNoRoom").value == "") {
             Swal.fire({
                 icon: 'warning',
                 title: 'Oops...',
                 text: 'Please Select No of Rooms.',
                 confirmButtonColor: '#f2572b'
             });
             document.getElementById("ddlNoRoom").focus();
             return false;
         }
         if (Trim(document.getElementById("ddlAdults").value) == "0" || document.getElementById("ddlAdults").value == "") {
             Swal.fire({
                 icon: 'warning',
                 title: 'Oops...',
                 text: 'Please Select No of Adults.',
                 confirmButtonColor: '#f2572b'
             });
             document.getElementById("ddlAdults").focus();
             return false;
         }
     }
     function validate1() {

         if (document.getElementById("ddlCity").value == "0") {
             Swal.fire({
                 icon: 'warning',
                 title: 'Oops...',
                 text: 'Please select City.',
                 confirmButtonColor: '#f2572b'
             });
             document.getElementById("ddlCity").focus();
             return false;
         }
         if (document.getElementById("ddlHotel").value == "" || document.getElementById("ddlHotel").value == "0") {
             Swal.fire({
                 icon: 'warning',
                 title: 'Oops...',
                 text: 'Please select Hotel.',
                 confirmButtonColor: '#f2572b'
             });
             document.getElementById("ddlHotel").focus();
             return false;
         }
         if (Trim(document.getElementById("txtarr").value) == "" || document.getElementById("txtarr").value == "dd/mm/yyyy") {
             Swal.fire({
                 icon: 'warning',
                 title: 'Oops...',
                 text: 'Please Select CheckIn Date.',
                 confirmButtonColor: '#f2572b'
             });
             document.getElementById("txtarr").focus();
             return false;
         }

         if (Trim(document.getElementById("txtdep").value) == "" || document.getElementById("txtdep").value == "dd/mm/yyyy") {
             alert('Please Select CheckOut Date.');
             document.getElementById("txtdep").focus();
             return false;
         }
         if (validateRadioButtonList('<%= rbtOccupants.ClientID %>') == false) {
             return false;
         }
         if (Trim(document.getElementById("ddlNoRoom").value) == "0" || document.getElementById("ddlNoRoom").value == "") {
             Swal.fire({
                 icon: 'warning',
                 title: 'Oops...',
                 text: 'Please Select No of Rooms.',
                 confirmButtonColor: '#f2572b'
             });
             document.getElementById("ddlNoRoom").focus();
             return false;
         }
         if (Trim(document.getElementById("ddlAdults").value) == "0" || document.getElementById("ddlAdults").value == "") {
             Swal.fire({
                 icon: 'warning',
                 title: 'Oops...',
                 text: 'Please Select No of Adults.',
                 confirmButtonColor: '#f2572b'
             });
             document.getElementById("ddlAdults").focus();
             return false;
         }
     }
     function checknumber() {

         var kk
         kk = event.keyCode
         if (event.shiftKey) return false;
         //alert(kk);
         if ((kk >= 96 && kk <= 105) || (kk >= 48 && kk <= 57) || kk == 8 || kk == 190 || kk == 110 || kk == 9 || kk == 35 || kk == 36 || kk == 37 || kk == 38 || kk == 39 || kk == 40 || kk == 46) {
             return true;
         }
         return false;
     }

     function chkNumeric() {
         if (event.shiftKey) return false;
         if ((event.keyCode < 48 || event.keyCode > 57) && event.keyCode != 8 && event.keyCode != 37 && event.keyCode != 38 && event.keyCode != 39 && event.keyCode != 40 && event.keyCode != 46 && event.keyCode != 13 && event.keyCode != 116 && event.keyCode != 16 && (event.keyCode < 96 || event.keyCode > 105) && event.keyCode != 9 && event.keyCode != 110) event.returnValue = false;
     }

     function CheckOnlyCharacter() {
         var kk
         kk = event.keyCode
         if ((kk >= 65 && kk <= 90) || kk == 32 || kk == 8 || kk == 9 || kk == 127 || kk == 16 || kk == 20 || kk == 46) {
             return true;
         }
         alert("Please enter characters only.");
         return false;
     }
     function echeck(str) {
         var at = "@"
         var dot = "."
         var und = "_"
         var lat = str.indexOf(at)
         var lstr = str.length
         var ldot = str.indexOf(dot)
         if (str.indexOf(at) == -1) {
             alert("Invalid E-mail ID.")
             return false
         }
         if (str.indexOf(at) == -1 || str.indexOf(at) == 0 || str.indexOf(at) == lstr) {
             alert("Invalid E-mail ID.")
             return false
         }
         if (str.indexOf(dot) == -1 || str.indexOf(dot) == 0 || str.indexOf(dot) == lstr) {
             alert("Invalid E-mail ID.")
             return false
         }
         if (str.indexOf(at, (lat + 1)) != -1) {
             alert("Invalid E-mail ID.")
             return false
         }
         if (str.substring(lat - 1, lat) == dot || str.substring(lat + 1, lat + 2) == dot) {
             alert("Invalid E-mail ID.")
             return false
         }
         if (str.indexOf(dot, (lat + 2)) == -1) {
             alert("Invalid E-mail ID.")
             return false
         }
         if (str.indexOf(" ") != -1) {
             alert("Invalid E-mail ID.")
             return false
         }
         if ((str.indexOf("..") > -1) || (str.substring(str.length - 1, str.length) == dot)) {
             alert("Invalid E-mail ID.")
             return false
         }
         if ((str.substring(0, 1) == und)) {
             alert("Invalid E-mail ID.")
             return false
         }
         return true
     }

     function calcdaysold() {
         var dbd;
         var chrs;

         if (Trim(document.getElementById("txtdep").value) != "") {
             if ((document.getElementById("txtarr").value != "") && (document.getElementById("txtdep").value != "")) {
                 var startDate = document.getElementById('txtarr').value;
                 var startArr = new Array(3)
                 startArr = startDate.split("/")
                 var vdd = startArr[0];
                 if (vdd.length == 1)
                     vdd = "0" + vdd
                 var vmm = startArr[1];
                 if (vmm.length == 1)
                     vmm = "0" + vmm
                 var vyy = startArr[2];
                 var yyyymmdd = vyy + '' + vmm + '' + vdd;

                 var endDate = document.getElementById('txtdep').value;
                 var endArr = new Array(3)
                 endArr = endDate.split("/")
                 var vdd1 = endArr[0];
                 if (vdd1.length == 1)
                     vdd1 = "0" + vdd1
                 var vmm1 = endArr[1];
                 if (vmm1.length == 1)
                     vmm1 = "0" + vmm1
                 var vyy1 = endArr[2];
                 var yyyymmdd1 = vyy1 + '' + vmm1 + '' + vdd1;

                 var depttime = document.getElementById('ddlDeparture').value;
                 var deptampm = document.getElementById('ddlDepartuream').value;
                 if (deptampm == "PM") {
                     depttime = parseInt(depttime) + 12;
                 }
                 var arrtime = document.getElementById('ddlarrivalhr').value;
                 var arrampm = document.getElementById('ddlarrivalam').value;
                 if (arrampm == "PM") {
                     arrtime = parseInt(arrtime) + 12;
                 }
                 if (yyyymmdd > yyyymmdd1) {
                     alert('Sorry! CheckOut date is less than CheckIn Date. Please Select CheckOut Date again');
                     document.getElementById('txtarr').focus();
                     document.getElementById('txtdep').focus();

                     return false;
                 }
                 else if (yyyymmdd == yyyymmdd1) {

                     if (parseInt(depttime) == parseInt(arrtime)) {
                         alert('Sorry! Both CheckIn & CheckOut Date Times Are Equal. Please Select CheckOut Date  Time again');
                         document.getElementById('ddlarrivalhr').focus();
                         document.getElementById('ddlDeparture').focus();
                         return false;
                     }
                     if (parseInt(depttime) < parseInt(arrtime)) {
                         alert('Sorry! CheckOut Time is less than CheckIn Time. Please Select CheckOut Time again');
                         document.getElementById('ddlarrivalhr').focus();
                         document.getElementById('ddlDeparture').focus();
                         return false;
                     }
                 }

                 var firstDate = Date.parse(vyy + '/' + vmm + '/' + vdd)
                 var secondDate = Date.parse(vyy1 + '/' + vmm1 + '/' + vdd1)

                 msPerDay = 24 * 60 * 60 * 1000
                 dbd = Math.round((secondDate.valueOf() - firstDate.valueOf()) / msPerDay);

                 //chrs=parseInt(depttime)-parseInt(arrtime);

                 //alert('Days:'+dbd);                
                 //alert('Arr'+arrtime);
                 //alert('Dep'+depttime);
                 var ndys = 0;
                 if (parseInt(dbd) > 0)
                     ndys = ndys + dbd;
                 if ((parseInt(arrtime) >= 6) && (parseInt(arrtime) <= 9)) {
                     //                 if((parseInt(dbd)==0)&&(parseInt(depttime)>8)&&(parseInt(depttime)<10))
                     //                    ndys=ndys+1;
                     //                if((parseInt(dbd)==0)&&(parseInt(depttime)>9))
                     //                    ndys=ndys+1;              
                     //                if((parseInt(dbd)==0)&&(parseInt(depttime)>13))
                     //                    ndys=ndys+1;                
                     if ((parseInt(dbd) == 0))
                         ndys = ndys + 1;
                     if ((parseInt(dbd) > 0) && (parseInt(depttime) < 8))
                         ndys = ndys;
                     if ((parseInt(dbd) > 0) && (parseInt(depttime) > 8) && (parseInt(depttime) < 10))
                         ndys = ndys;
                     if ((parseInt(dbd) > 0) && (parseInt(depttime) > 9))
                         ndys = ndys + 1;
                 }
                 else if ((parseInt(arrtime) >= 10) && (parseInt(arrtime) <= 13)) {
                     //ndys=ndys+1;
                     if ((parseInt(dbd) > 0) && (parseInt(depttime) > 13))
                         ndys = ndys;
                     if ((parseInt(dbd) > 0) && (parseInt(depttime) > 10) && (parseInt(depttime) < 13))
                         ndys = ndys;
                     //                if((parseInt(dbd)>0)&&(parseInt(depttime)>10))     
                     //                    ndys=ndys+1;  


                 }
                 else if (parseInt(arrtime) < 6) {
                     if ((parseInt(dbd) == 0) && (parseInt(depttime) > 8) && (parseInt(depttime) < 10))
                         ndys = ndys + 1;
                     if ((parseInt(dbd) == 0) && (parseInt(depttime) > 9))
                         ndys = ndys + 2;
                     //                if((parseInt(dbd)==0)&&(parseInt(depttime)>13))
                     //                    ndys=ndys+1;
                     if ((parseInt(dbd) > 0) && (parseInt(depttime) <= 8))
                         ndys = ndys + 1;
                     if ((parseInt(dbd) > 0) && (parseInt(depttime) > 8) && (parseInt(depttime) < 10))
                         ndys = ndys + 1;
                     if ((parseInt(dbd) > 0) && (parseInt(depttime) > 9))
                         ndys = ndys + 2;
                 }
                 else if (parseInt(arrtime) > 13) {
                     //                 if((parseInt(dbd)==0)&&(parseInt(depttime)>8)&&(parseInt(depttime)<10))
                     //                    ndys=ndys+1;
                     if ((parseInt(dbd) == 0) && (parseInt(depttime) > 13))
                         ndys = ndys + 1;
                     //                if((parseInt(dbd)==0)&&(parseInt(depttime)>13))
                     //                    ndys=ndys+1;
                     if ((parseInt(dbd) > 0) && (parseInt(depttime) < 8))
                         ndys = ndys + 1;
                     if ((parseInt(dbd) > 0) && (parseInt(depttime) > 8) && (parseInt(depttime) < 10))
                         ndys = ndys + 1;
                 }
                 alert(ndys);
             }
         }
     }

     function calcdays()//calcdays24HrsSlot()
     {
         var dbd;
         var chrs;
         if (document.getElementById("txtdep").value != "") {
             if ((document.getElementById("txtarr").value != "") && (document.getElementById("txtdep").value != "")) {
                 var startDate = document.getElementById('txtarr').value;
                 var startArr = new Array(3)
                 startArr = startDate.split("/")
                 var vdd = startArr[0];
                 if (vdd.length == 1)
                     vdd = "0" + vdd
                 var vmm = startArr[1];
                 if (vmm.length == 1)
                     vmm = "0" + vmm
                 var vyy = startArr[2];
                 var yyyymmdd = vyy + '' + vmm + '' + vdd;

                 var endDate = document.getElementById('txtdep').value;
                 var endArr = new Array(3)
                 endArr = endDate.split("/")
                 var vdd1 = endArr[0];
                 if (vdd1.length == 1)
                     vdd1 = "0" + vdd1
                 var vmm1 = endArr[1];
                 if (vmm1.length == 1)
                     vmm1 = "0" + vmm1
                 var vyy1 = endArr[2];
                 var yyyymmdd1 = vyy1 + '' + vmm1 + '' + vdd1;

                 var depttime = document.getElementById('ddlDeparture').value;
                 depttime = parseInt(depttime)
                 var arrtime = document.getElementById('ddlarrivalhr').value;
                 arrtime = parseInt(arrtime)
                 if (yyyymmdd > yyyymmdd1) {
                     alert('Sorry! CheckOut date is less than Checkin Date. Please Select CheckOut Date again');
                     document.getElementById('txtarr').focus();
                     document.getElementById('txtdep').focus();
                     return false;
                 }
                 else if (yyyymmdd == yyyymmdd1) {
                     if (parseInt(depttime) == parseInt(arrtime)) {
                         alert('Sorry! Both Checkin & CheckOut Date Times Are Equal. Please Select Checkout Date  Time again');
                         document.getElementById('ddlarrivalhr').focus();
                         document.getElementById('ddlDeparture').focus();
                         return false;
                     }
                     if (parseInt(depttime) < parseInt(arrtime)) {
                         alert('Sorry! CheckOut Time is less than Checkin Time. Please Select Checkout Time again');
                         document.getElementById('ddlarrivalhr').focus();
                         document.getElementById('ddlDeparture').focus();
                         return false;
                     }
                 }
                 // *************************
                 // START --Determine Check-In Check-Out Time Slot
                 // *************************
                 var HrsAlwdB4Checkin = 0;
                 var ActualStartTime = 0;
                 var lCheckOutBuffer = 0;

                 if (document.getElementById('<%=hdnTimeSlot.ClientID%>').value == "" || document.getElementById('<%= hdnTimeSlot.ClientID%>').value == "812hrs") {
                     if (arrtime >= 11 || arrtime <= 3) {
                         ActualStartTime = 12;
                         HrsAlwdB4Checkin = 1;
                     }
                     else {
                         ActualStartTime = 8;
                         HrsAlwdB4Checkin = 4;
                     }
                     lCheckOutBuffer = 2;
                 }
                 else {
                     ActualStartTime = arrtime;
                     HrsAlwdB4Checkin = 1;
                     lCheckOutBuffer = 1;
                 }
                 // *************************
                 // END --Determine Check-In Check-Out Time Slot
                 // *************************                
                 date1 = new Date();
                 date2 = new Date();
                 diff = new Date();

                 date1temp = new Date(vmm + '/' + vdd + '/' + vyy + " " + ActualStartTime + ":00:00");
                 date1.setTime(date1temp.getTime());

                 date2temp = new Date(vmm1 + '/' + vdd1 + '/' + vyy1 + " " + ActualStartTime + ":00:00");
                 date2.setTime(date2temp.getTime());

                 diff.setTime(Math.abs(date1.getTime() - date2.getTime()));

                 timediff = diff.getTime();

                 days = Math.floor(timediff / (1000 * 60 * 60 * 24));
                 timediff -= days * (1000 * 60 * 60 * 24);

                 hours = Math.floor(timediff / (1000 * 60 * 60));
                 timediff -= hours * (1000 * 60 * 60);
                 //alert(days);
                 //alert(hours);				
                 //*******************
                 var firstDate = Date.parse(vyy + '/' + vmm + '/' + vdd)
                 var secondDate = Date.parse(vyy1 + '/' + vmm1 + '/' + vdd1)
                 var TotalMS = (((days * 24) + hours) * 60 * 60 * 1000);
                 msPerDay = 24 * 60 * 60 * 1000
                 dbd = parseInt((TotalMS / msPerDay));

                 if ((arrtime <= (ActualStartTime + lCheckOutBuffer)) && (arrtime >= (ActualStartTime - HrsAlwdB4Checkin))) {
                     if (days > 0) {
                         // On Time Arrival
                         if (depttime > (ActualStartTime + lCheckOutBuffer)) {
                             // Late Departure
                             dbd = parseInt(dbd + 1);
                         }
                     }
                     else {
                         dbd = 1;
                     }
                 }
                 else {
                     // Late Arrival
                     var TotalHrs = 0;
                     var TotaDays = 0;
                     if (arrtime < (ActualStartTime - HrsAlwdB4Checkin)) {
                         TotaDays += 1;
                     }
                     if (depttime > (ActualStartTime + lCheckOutBuffer)) {
                         // Late Departure
                         TotalHrs += depttime - ActualStartTime;
                         TotaDays += 1;
                     }
                     TotaDays += days;
                     dbd = TotaDays;
                 }
                 document.getElementById("txtnodays1").value = dbd;
             }
         }
     }
 </script>

 <script language="javascript" type="text/javascript">
     function fnMd5(saltval) {
         encpass = hex_md5(saltval);
         document.getElementById('tmpEnValue').value = encpass;
         return true;
     }
 </script>

 <script type="text/javascript">
     function VerifyGuestDeatils() {

         var guestgrid = document.getElementById('<%= grdGuestDetails.ClientID %>');
         var i = 1;
         if (guestgrid.rows.length > 0) {
             var controls = guestgrid.getElementsByTagName("*");
             //Loop through the fetched controls.
             for (var i = 0; i < controls.length; i++) {

                 if (controls[i].id.indexOf("txtGuestType") != -1) {
                     txtGuestType = controls[i];
                     //alert(txtGuestType.value);
                 }

                 if (controls[i].id.indexOf("txtName") != -1) {
                     txtName = controls[i];

                     if (txtName.value == '') {
                         Swal.fire({
                             icon: 'warning',
                             title: 'Oops...',
                             text: 'Please enter guest name.',
                             confirmButtonColor: '#f2572b'
                         });
                         txtName.focus();
                         return false;
                         break;
                     }

                 }

                 if (controls[i].id.indexOf("txtAge") != -1) {
                     txtage = controls[i];
                     if (txtage.value == '') {
                         Swal.fire({
                             icon: 'warning',
                             title: 'Oops...',
                             text: 'Please enter guest age.',
                             confirmButtonColor: '#f2572b'
                         });
                         txtage.focus();
                         return false;
                         break;
                     }
                 }

                 if (controls[i].id.indexOf("ddlGender") != -1) {
                     ddlGender = controls[i];
                     if (ddlGender.value == '0') {
                         Swal.fire({
                             icon: 'warning',
                             title: 'Oops...',
                             text: 'Please select  guest gender.',
                             confirmButtonColor: '#f2572b'
                         });
                         ddlGender.focus();
                         return false;
                         break;
                     }

                 }

             }
         }
     }
 
 </script>

 <script type="text/javascript">
     function chkNumericForage(evt) {
         var charCode = (evt.which) ? evt.which : event.keyCode
         if (charCode > 31 && (charCode < 45 || charCode > 57) || (charCode == 47 || charCode == 45 || charCode == 46))
             return false;
         return true;
     }
 </script>

    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <input type="hidden" id="tmpEnValue" runat="server" />
        <input type="hidden" id="type" value="" runat="server" />
        <input type="hidden" name="stax" id="stax" runat="server" />
        <input type="hidden" name="stax" id="hddeluxe" runat="server" />
        <input type="hidden" name="stax" id="hdsuperdeluxe" runat="server" />
        <input type="hidden" name="stax" id="hdexecutive" runat="server" />
        <input type="hidden" name="stax" id="hdroyal" runat="server" />
        <input type="hidden" name="txttotaldays" id="txttotaldays" runat="server" />
        <input type="hidden" name="txtnodays1" id="txtnodays1" runat="server" />
        <input type="hidden" name="LTstax" id="LTstax" runat="server" />
        <input type="hidden" name="hdRowIndex" id="hdRowIndex" runat="server" />
        <input type="hidden" name="hdnTimeSlot" id="hdnTimeSlot" runat="server" />
        <input type="hidden" name="hdnIsCalCWBFare" id="hdnIsCalCWBFare" runat="server" value="0" />
        <input type="hidden" name="hdnTotalCWB" id="hdnTotalCWB" runat="server" value="0" />
        <table id="table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td colspan="1">
                    <uc1:AgentHeader ID="agentHeader" runat="server" />
                </td>
            </tr>
            <tr>
                <td width="80%" style="height: 226px" valign="top">
                    <table cellpadding="0" cellspacing="0" border="0" width="100%" class="footer">
                        <tr>
                            <td>
                                <table width="296" border="0" align="center" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left" style="width: 9px">
                                            <img src="Assets/images/left_corner.gif" width="8" height="8"  loading="lazy" alt="left_corner"/>
                                        </td>
                                        <td bgcolor="#E7E7E7">
                                            <img src="Assets/images/trans.gif" width="280" height="1" loading="lazy" alt="trans"/>
                                        </td>
                                        <td align="right">
                                            <img src="Assets/images/rgt_corner.gif" width="8" height="8" loading="lazy" alt="rgt_corner"
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" bgcolor="#E7E7E7" style="width: 9px">
                                            &nbsp;
                                        </td>
                                        <td bgcolor="#E7E7E7">
                                            <table cellpadding="0" cellspacing="0" border="0" width="800" class="footer" bgcolor="#cccccc">
                                                <tr>
                                                    <td height="35" colspan="4" align="center" valign="middle" bgcolor="#348DE7" class="verdana14w">
                                                        <table width="800" border="0" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td width="13" background="Assets/images/left_.gif">
                                                                    &nbsp;
                                                                </td>
                                                                <td align="center" valign="middle" background="Assets/images/bg1.gif">
                                                                    Accommodation Booking
                                                                </td>
                                                                <td width="13" height="42" background="Assets/images/right_.gif">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:Panel ID="Panel2" runat="server" Style="display: none;">
                                                <table cellpadding="2" cellspacing="1" border="0" width="100%" class="footer" bgcolor="#cccccc">
                                                    <tr>
                                                        <td align="left" colspan="4" height="10" bgcolor="#5aa1ea" class="cgi1">
                                                            Fare Details
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td bgcolor="#ffffff" style="height: 28px; width: 20%;">
                                                            &nbsp;CheckIn Date: &nbsp;<asp:Label ID="lvlChkI" runat="server" Text=""></asp:Label>
                                                        </td>
                                                        <td bgcolor="#ffffff" style="width: 35%; height: 14px">
                                                            &nbsp;CheckOut Date: &nbsp;
                                                            <asp:Label ID="lvlChkO" runat="server" Text=""></asp:Label>
                                                        </td>
                                                        <td bgcolor="#ffffff" style="height: 28px;">
                                                        </td>
                                                        <td bgcolor="#ffffff" style="height: 28px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td bgcolor="#ffffff" style="height: 28px; width: 20%;">
                                                            &nbsp;No of Room: &nbsp;
                                                            <asp:Label ID="lblRom" runat="server" Text=""></asp:Label>
                                                        </td>
                                                        <td bgcolor="#ffffff" style="width: 35%; height: 14px">
                                                            &nbsp;No of Adults: &nbsp;
                                                            <asp:Label ID="lblAdu" runat="server" Text=""></asp:Label>
                                                        </td>
                                                        <td bgcolor="#ffffff" style="height: 28px;">
                                                            &nbsp;No of Childs: &nbsp;<asp:Label ID="lblChld" runat="server" Text=""></asp:Label>
                                                        </td>
                                                        <td bgcolor="#ffffff" style="height: 28px">
                                                            &nbsp;No of Days: &nbsp;
                                                            <asp:Label ID="lblDay" runat="server" Text=""></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td bgcolor="#ffffff">
                                                            &nbsp;Room Type: &nbsp;
                                                            <asp:Label ID="lblRomType" runat="server" Text=""></asp:Label>
                                                        </td>
                                                        <td bgcolor="#ffffff">
                                                            &nbsp;Fare: &nbsp;
                                                            <asp:Label ID="lblFare" runat="server" Text=""></asp:Label>
                                                            &nbsp; &nbsp;
                                                            <asp:Label ID="lblDiscountFare" runat="server" Text=""></asp:Label>
                                                        </td>
                                                        <td bgcolor="#ffffff">
                                                            &nbsp;Extra Bed Fare: &nbsp;
                                                            <asp:Label ID="lblExtra" runat="server" Text=""></asp:Label>
                                                        </td>
                                                        <td bgcolor="#ffffff">
                                                            &nbsp;Child Without Bed Fare: &nbsp;
                                                            <asp:Label ID="lblCWBedFare" runat="server" Text=""></asp:Label>
                                                        </td>
                                                        <td bgcolor="#ffffff">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td bgcolor="#ffffff">
                                                            &nbsp;Total Amount:
                                                        </td>
                                                        <td bgcolor="#ffffff" colspan="3">
                                                            <asp:TextBox ID="txtamt" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td bgcolor="#ffffff" style="height: 28px; width: 20%;">
                                                            &nbsp;GST :
                                                        </td>
                                                        <td bgcolor="#ffffff" style="width: 35%; height: 14px">
                                                            <asp:TextBox ID="txttax" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td bgcolor="#ffffff" style="height: 28px;">
                                                            &nbsp;Total with tax:
                                                        </td>
                                                        <td bgcolor="#ffffff" style="height: 28px">
                                                            <asp:TextBox ID="txttotalamtwithtax" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td bgcolor="#ffffff" style="height: 28px">
                                                            &nbsp;Remarks:
                                                        </td>
                                                        <td bgcolor="#ffffff" style="height: 28px" colspan="3">
                                                            <asp:TextBox ID="txtremarks" runat="server" MaxLength="150" TextMode="MultiLine"
                                                                ValidationGroup="Validation"></asp:TextBox>
                                                            <asp:RegularExpressionValidator runat="server" ValidationGroup="Validation" SetFocusOnError="True"
                                                                ID="revTBIssueDescription" Display="Dynamic" ControlToValidate="txtremarks" ValidationExpression="^[\s\S]{0,150}$"
                                                                ErrorMessage="Please enter remarks in 150 characters." />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td bgcolor="#ffffff" height="15px" colspan="4">
                                                        </td>
                                                    </tr>
                                                    <tr style="display: none;">
                                                        <td align="right" height="10" colspan="3" bgcolor="#ffffff">
                                                            Email/Mobile:&nbsp;
                                                        </td>
                                                        <td align="left" colspan="1" height="10" bgcolor="#ffffff">
                                                            <asp:TextBox ID="txtSearch" runat="server" MaxLength="50"></asp:TextBox>&nbsp;&nbsp;
                                                            <asp:Button ID="btnSearch" Style="background-color: #5aa1ea" class="cgi1" Text=" Search "
                                                                runat="Server" BorderStyle="None" ValidationGroup="ValidSearch" OnClick="btnSearch_Click" />
                                                        </td>
                                                    </tr>
                                                    <tr style="display: none;">
                                                        <td bgcolor="#ffffff" height="15px" colspan="4" align="right">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtSearch"
                                                                ErrorMessage="Required Field." SetFocusOnError="True" ValidationGroup="ValidSearch"
                                                                Display="Dynamic"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlCustomer" runat="server" Visible="false">
                                                <table cellpadding="2" cellspacing="1" border="0" width="800" class="footer">
                                                    <tr>
                                                        <td>
                                                            <uc3:ucManageCustomer ID="ucManageCustomer1" runat="server" fldIsEndUser="false"
                                                                fldIsPassportRequired="false" fldIsDelegateRequired="true" fldIsPanRequired="false"
                                                                fldIsRequierdOccupation="true" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                            <asp:Panel ID="pnl2" runat="server" Visible="false">
                                                <table cellpadding="2" cellspacing="1" border="0" width="800" class="footer">
                                                    <tr class="hlinks">
                                                        <td colspan="2">
                                                            <table border="0" cellpadding="0" width="100%" cellspacing="0">
                                                                <tr>
                                                                    <td align="left" bgcolor="#5aa1ea" class="cgi1" style="height: 30px" width="50%">
                                                                        <asp:CheckBox ID="chkPickUp" runat="server" onclick="showHidePickupInfo()" />PickUp
                                                                        Information
                                                                    </td>
                                                                    <td align="center" bgcolor="#5aa1ea" class="cgi1" height="20px" width="100%">
                                                                        <div id="divDropInfo" style="display: none;">
                                                                            <table width="100%" cellpadding="0" cellspacing="0" border="0" bgcolor="#ffffff"
                                                                                style="display: none;">
                                                                                <tr class="hlinks" align="Center">
                                                                                    <td align="left" class="verdana11w" bgcolor="#5aa1ea">
                                                                                        <b>Drop Information</b>
                                                                                        <asp:CheckBox ID="chkDrop" runat="server" onclick="javascript:fnDrophide(this.id);clear5();" />&nbsp;Same
                                                                                        As PickUp
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr class="hlinks" valign="top">
                                                        <td width="50%">
                                                            <div id="divPickup" style="display: none;">
                                                                <table width="100%" border="0" cellspacing="2" cellpadding="1" bgcolor="#FFFFFF">
                                                                    <tr>
                                                                        <td align="left" style="height: 22px; width: 30%;">
                                                                            PickUp From:
                                                                        </td>
                                                                        <td style="height: 22px">
                                                                            <asp:RadioButton ID="RadFlight" runat="server" Checked="true" GroupName="pick" Text="Flight"
                                                                                onclick="javascript:fnRDOSelection('',this.id);"></asp:RadioButton>
                                                                            <asp:RadioButton ID="RadTrain" runat="server" GroupName="pick" Text="Train" onclick="javascript:fnRDOSelection('',this.id);">
                                                                            </asp:RadioButton><asp:RadioButton ID="RadBus" runat="server" GroupName="pick" Text="Address/Location"
                                                                                onclick="javascript:fnRDOSelection('',this.id);" Style="display: none;"></asp:RadioButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <table width="100%" border="0" id="hideFlight" cellspacing="2" cellpadding="1" bgcolor="#FFFFFF">
                                                                    <tr class="hlinks" align="left">
                                                                        <td align="left" style="height: 22px; width: 212px;">
                                                                            AirPort:*
                                                                        </td>
                                                                        <td style="height: 14px">
                                                                            <asp:DropDownList ID="txtpickVehicleNo" runat="server" onchange="copyToDepart(this);">
                                                                                <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                                                                                <asp:ListItem Text="Domestic Airport" Value="1"></asp:ListItem>
                                                                                <asp:ListItem Text="International" Value="2"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="hlinks" align="left">
                                                                        <td align="left" style="height: 24px; width: 212px;">
                                                                            Expected Arrival Time: *
                                                                        </td>
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
                                                                                <%--<option value="">min</option>--%>
                                                                                <option value="00" selected="selected">00</option>
                                                                                <option value="15">15</option>
                                                                                <option value="30">30</option>
                                                                                <option value="45">45</option>
                                                                            </select>
                                                                            &nbsp;Mints
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="hlinks" align="left">
                                                                        <td style="width: 212px">
                                                                            Flight No:*
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtFlightNo" onkeyup="copyToDepart(this);" MaxLength="25" size="15"
                                                                                runat="server"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <!--Code by Jaggu for Toggle---------->
                                                                <table width="100%" border="0" id="hideTrain" style="display: none" cellspacing="2"
                                                                    cellpadding="1" bgcolor="#FFFFFF">
                                                                    <tr class="hlinks" align="left">
                                                                        <td align="left" style="height: 24px; width: 212px;">
                                                                            Railway Station: *
                                                                        </td>
                                                                        <td style="height: 24px">
                                                                            <asp:TextBox ID="txtRlyStationName" onkeyup="copyToDepart(this);" MaxLength="50"
                                                                                size="15" runat="server"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="hlinks" align="left">
                                                                        <td align="left" style="height: 24px; width: 212px;">
                                                                            Expected Arrival Time: *
                                                                        </td>
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
                                                                                <%--<option value="">min</option>--%>
                                                                                <option value="00" selected="selected">00</option>
                                                                                <option value="15">15</option>
                                                                                <option value="30">30</option>
                                                                                <option value="45">45</option>
                                                                            </select>
                                                                            &nbsp;Mints.
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="hlinks" align="left">
                                                                        <td style="width: 212px">
                                                                            Train No:*
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtTrainNo" onkeyup="copyToDepart(this);" MaxLength="25" size="15"
                                                                                runat="server"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <!--Code For Location/Address------>
                                                                <table width="100%" border="0" id="hideLocation" style="display: none" cellspacing="2"
                                                                    cellpadding="1" bgcolor="#FFFFFF">
                                                                    <tr class="hlinks" align="left">
                                                                        <td align="left" style="height: 24px; width: 212px;">
                                                                            Address: *
                                                                        </td>
                                                                        <td style="height: 24px">
                                                                            <asp:TextBox ID="txtAddr" onkeyup="copyToDepart(this);" MaxLength="50" size="15"
                                                                                runat="server"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="hlinks" align="left">
                                                                        <td align="left" style="height: 24px; width: 212px;">
                                                                            Street:*
                                                                        </td>
                                                                        <td style="height: 24px">
                                                                            <asp:TextBox ID="txtStreet" onkeyup="copyToDepart(this);" MaxLength="50" size="15"
                                                                                runat="server"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </td>
                                                        <td width="50%">

                                                            <script>
                                            <!--

                                                                function copyToDepart(v) {

                                                                    var chkDropObj = document.getElementById('chkDrop');
                                                                    if (chkDropObj != null) {
                                                                        if (chkDropObj.checked) {
                                                                            var vArrObj = v;
                                                                            var vDepartObj = document.getElementById(v.id + '_d');
                                                                            if ((vDepartObj != null) && (vArrObj != null)) {
                                                                                vDepartObj.value = vArrObj.value;
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                            -->
                                                            </script>

                                                            <div id="divDrop" style="display: none;">
                                                                <table width="100%" cellpadding="2" cellspacing="2" border="0" bgcolor="#ffffff">
                                                                    <tr class="hlinks" align="left">
                                                                        <td align="left" style="height: 22px; width: 154px;">
                                                                            Drop at:
                                                                        </td>
                                                                        <td style="height: 22px">
                                                                            <asp:RadioButton ID="RadFlight_d" onclick="javascript:showHide('_d');" runat="server"
                                                                                GroupName="Drop" Text="Flight"></asp:RadioButton>
                                                                            <asp:RadioButton ID="RadTrain_d" onclick="javascript:showHide('_d');" runat="server"
                                                                                GroupName="Drop" Text="Train"></asp:RadioButton><asp:RadioButton ID="RadBus_d" onclick="javascript:showHide('_d');"
                                                                                    runat="server" GroupName="Drop" Text="Location/Address" Style="display: none;">
                                                                            </asp:RadioButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <table width="100%" border="0" id="hideFlight_d" cellspacing="2" cellpadding="1"
                                                                    bgcolor="#FFFFFF">
                                                                    <tr class="hlinks" align="left">
                                                                        <td align="left" style="height: 14px; width: 212px;">
                                                                            Flight Type:
                                                                        </td>
                                                                        <td style="height: 14px">
                                                                            <asp:DropDownList ID="txtpickVehicleNo_d" runat="server">
                                                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                                                <asp:ListItem Text="Domestic Airport" Value="1"></asp:ListItem>
                                                                                <asp:ListItem Text="International" Value="2"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
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
                                                                                <%--<option value="">min</option>--%>
                                                                                <option value="00" selected="selected">00</option>
                                                                                <option value="15">15</option>
                                                                                <option value="30">30</option>
                                                                                <option value="45">45</option>
                                                                            </select>
                                                                            &nbsp;Mints.
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="hlinks" align="left">
                                                                        <td style="width: 212px">
                                                                            Flight No:
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtFlightNo_d" MaxLength="25" size="15" runat="server"></asp:TextBox>
                                                                        </td>
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
                                                                            <asp:TextBox ID="txtRlyStationName_d" MaxLength="50" size="15" runat="server"></asp:TextBox>
                                                                        </td>
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
                                                                                <%--<option value="">min</option>--%>
                                                                                <option value="00" selected="selected">00</option>
                                                                                <option value="15">15</option>
                                                                                <option value="30">30</option>
                                                                                <option value="45">45</option>
                                                                            </select>
                                                                            &nbsp;Mints.
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="hlinks" align="left">
                                                                        <td style="width: 212px">
                                                                            Train No:
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtTrainNo_d" MaxLength="25" size="15" runat="server"></asp:TextBox>
                                                                        </td>
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
                                                                            <asp:TextBox ID="txtAddr_d" MaxLength="50" size="15" runat="server"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="hlinks" align="left">
                                                                        <td align="left" style="height: 24px; width: 212px;">
                                                                            Street:
                                                                        </td>
                                                                        <td style="height: 24px">
                                                                            <asp:TextBox ID="txtStreet_d" MaxLength="50" size="15" runat="server"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" colspan="2" height="10" bgcolor="#5aa1ea" class="cgi1">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td bgcolor="#ffffff" height="15px" colspan="2">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td bgcolor="#ffffff" colspan="2" align="center">
                                                            <asp:Button ID="Btnreset" Style="background-color: #5aa1ea" class="cgi1" Text=" Reset "
                                                                runat="Server" BorderStyle="None" OnClick="Btnreset_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                                            <asp:Button ID="Submit" Style="background-color: #5aa1ea" class="cgi1" Text=" Submit & Book Now "
                                                                runat="Server" BorderStyle="None" OnClick="Submit_Click" ValidationGroup="Validation1" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td bgcolor="#ffffff" height="15px" colspan="2">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                            <asp:Panel ID="Panel1" runat="server">
                                                <table cellpadding="2" cellspacing="1" border="0" width="800" class="footer">
                                                    <tr>
                                                        <td bgcolor="#ffffff" height="10">
                                                            <asp:Panel ID="PnlCh" runat="server">
                                                                <table style="border-collapse: collapse" bordercolor="#f0f0f0" cellspacing="3" cellpadding="3"
                                                                    width="100%" border="1" align="center">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td colspan="4">
                                                                                <table align="center" border="1" bordercolor="#f0f0f0" cellpadding="3" cellspacing="3"
                                                                                    style="border-collapse: collapse;" width="100%">
                                                                                    <tr>
                                                                                        <td width="20%">
                                                                                            <font color="red">*</font> City:
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:DropDownList ID="ddlCity" runat="server" AutoPostBack="True" ValidationGroup="Validation"
                                                                                                OnSelectedIndexChanged="ddlCity_SelectedIndexChanged">
                                                                                            </asp:DropDownList>
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlCity"
                                                                                                Display="Dynamic" ErrorMessage="Required Field." InitialValue="0" SetFocusOnError="True"
                                                                                                ValidationGroup="Validation"></asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                        <td width="20%">
                                                                                            <font color="red">*</font> Hotels
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:DropDownList ID="ddlHotel" runat="server" ValidationGroup="Validation" AutoPostBack="True"
                                                                                                OnSelectedIndexChanged="ddlHotel_SelectedIndexChanged">
                                                                                            </asp:DropDownList>
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlHotel"
                                                                                                Display="Dynamic" ErrorMessage="Required Field." InitialValue="" SetFocusOnError="True"
                                                                                                ValidationGroup="Validation"></asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" colspan="4" height="10" bgcolor="#5aa1ea" class="cgi1">
                                                                                CheckIn & CheckOut Times
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" style="height: 14px; width: 20%">
                                                                                <font color="red">*</font> CheckIn Date:
                                                                            </td>
                                                                            <td align="left" style="height: 14px;">
                                                                                <asp:TextBox ID="txtarr" runat="server" ValidationGroup="Validation"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="Req1" runat="server" ControlToValidate="txtarr" ErrorMessage="Required Field."
                                                                                    SetFocusOnError="True" ValidationGroup="Validation" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                            </td>
                                                                            <td align="left" style="height: 14px; width: 20%;">
                                                                                CheckIn Time:
                                                                            </td>
                                                                            <td align="left" style="height: 14px;">
                                                                                <table width="100%" border="0">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:DropDownList ID="ddlarrivalhr" runat="server" OnChange="calcdays();">
                                                                                                <asp:ListItem Value="0">0</asp:ListItem>
                                                                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                                                                <asp:ListItem Value="3">3</asp:ListItem>
                                                                                                <asp:ListItem Value="4">4</asp:ListItem>
                                                                                                <asp:ListItem Value="5">5</asp:ListItem>
                                                                                                <asp:ListItem Value="6">6</asp:ListItem>
                                                                                                <asp:ListItem Value="7">7</asp:ListItem>
                                                                                                <asp:ListItem Value="8" Selected="true">8</asp:ListItem>
                                                                                                <asp:ListItem Value="9">9</asp:ListItem>
                                                                                                <asp:ListItem Value="10">10</asp:ListItem>
                                                                                                <asp:ListItem Value="11">11</asp:ListItem>
                                                                                                <asp:ListItem Value="12">12</asp:ListItem>
                                                                                                <asp:ListItem Value="13">13</asp:ListItem>
                                                                                                <asp:ListItem Value="14">14</asp:ListItem>
                                                                                                <asp:ListItem Value="15">15</asp:ListItem>
                                                                                                <asp:ListItem Value="16">16</asp:ListItem>
                                                                                                <asp:ListItem Value="17">17</asp:ListItem>
                                                                                                <asp:ListItem Value="18">18</asp:ListItem>
                                                                                                <asp:ListItem Value="19">19</asp:ListItem>
                                                                                                <asp:ListItem Value="20">20</asp:ListItem>
                                                                                                <asp:ListItem Value="21">21</asp:ListItem>
                                                                                                <asp:ListItem Value="22">22</asp:ListItem>
                                                                                                <asp:ListItem Value="23">23</asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:DropDownList ID="ddlarrivalmin" runat="server" OnChange="calcdays();">
                                                                                                <asp:ListItem Value="0" Selected="true">00</asp:ListItem>
                                                                                                <asp:ListItem Value="15">15</asp:ListItem>
                                                                                                <asp:ListItem Value="30">30</asp:ListItem>
                                                                                                <asp:ListItem Value="45">45</asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:DropDownList ID="ddlarrivalam" runat="server" Style="display: none;">
                                                                                                <asp:ListItem Value="AM" Selected="true">AM</asp:ListItem>
                                                                                                <asp:ListItem Value="PM">PM</asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" style="height: 14px; width: 20%">
                                                                                <font color="red">*</font> CheckOut Date:
                                                                            </td>
                                                                            <td align="left" style="height: 14px">
                                                                                <asp:TextBox ID="txtdep" runat="server" ValidationGroup="Validation"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtdep"
                                                                                    ErrorMessage="Required Field." SetFocusOnError="True" ValidationGroup="Validation"
                                                                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                                                            </td>
                                                                            <td align="left" style="height: 14px; width: 20%;">
                                                                                CheckOut Time:
                                                                            </td>
                                                                            <td align="left" style="height: 14px;">
                                                                                <table width="100%" border="0">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:DropDownList ID="ddlDeparture" runat="server" OnChange="calcdays();">
                                                                                                <asp:ListItem Value="0">0</asp:ListItem>
                                                                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                                                                <asp:ListItem Value="3">3</asp:ListItem>
                                                                                                <asp:ListItem Value="4">4</asp:ListItem>
                                                                                                <asp:ListItem Value="5">5</asp:ListItem>
                                                                                                <asp:ListItem Value="6">6</asp:ListItem>
                                                                                                <asp:ListItem Value="7">7</asp:ListItem>
                                                                                                <asp:ListItem Value="8" Selected="true">8</asp:ListItem>
                                                                                                <asp:ListItem Value="9">9</asp:ListItem>
                                                                                                <asp:ListItem Value="10">10</asp:ListItem>
                                                                                                <asp:ListItem Value="11">11</asp:ListItem>
                                                                                                <asp:ListItem Value="12">12</asp:ListItem>
                                                                                                <asp:ListItem Value="13">13</asp:ListItem>
                                                                                                <asp:ListItem Value="14">14</asp:ListItem>
                                                                                                <asp:ListItem Value="15">15</asp:ListItem>
                                                                                                <asp:ListItem Value="16">16</asp:ListItem>
                                                                                                <asp:ListItem Value="17">17</asp:ListItem>
                                                                                                <asp:ListItem Value="18">18</asp:ListItem>
                                                                                                <asp:ListItem Value="19">19</asp:ListItem>
                                                                                                <asp:ListItem Value="20">20</asp:ListItem>
                                                                                                <asp:ListItem Value="21">21</asp:ListItem>
                                                                                                <asp:ListItem Value="22">22</asp:ListItem>
                                                                                                <asp:ListItem Value="23">23</asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:DropDownList ID="ddlDeparturemin" runat="server" OnChange="calcdays();">
                                                                                                <asp:ListItem Value="0" Selected="true">00</asp:ListItem>
                                                                                                <asp:ListItem Value="15">15</asp:ListItem>
                                                                                                <asp:ListItem Value="30">30</asp:ListItem>
                                                                                                <asp:ListItem Value="45">45</asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:DropDownList ID="ddlDepartuream" runat="server" Style="display: none;">
                                                                                                <asp:ListItem Value="AM" Selected="true">AM</asp:ListItem>
                                                                                                <asp:ListItem Value="PM">PM</asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td height="10" colspan="4">
                                                                                <b>Note:</b> &nbsp;
                                                                                <asp:Label ID="lblNote" runat="server" Text="CheckIn/CheckOut Time : 8 AM/12 Noon or 24 Hr."></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td bgcolor="#ffffff" height="10" colspan="4">
                                                                                <table style="border-collapse: collapse" bordercolor="#f0f0f0" cellspacing="3" cellpadding="3"
                                                                                    width="100%" border="1" align="center">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td align="left" height="10" bgcolor="#5aa1ea" class="cgi1">
                                                                                                No.Of Occupants
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="4">
                                                                                <table style="border-collapse: collapse;" bordercolor="#f0f0f0" cellspacing="3" cellpadding="3"
                                                                                    width="100%" border="1" align="center">
                                                                                    <tr>
                                                                                        <td width="20%">
                                                                                            <font color="red">*</font> Occupancy:
                                                                                        </td>
                                                                                        <td colspan="4">
                                                                                            <asp:RadioButtonList ID="rbtOccupants" runat="server" RepeatDirection="Vertical"
                                                                                                RepeatColumns="3" AutoPostBack="True" OnSelectedIndexChanged="rbtOccupants_SelectedIndexChanged">
                                                                                            </asp:RadioButtonList>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td width="20%">
                                                                                            <font color="red">*</font> No.of Rooms:
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:DropDownList ID="ddlNoRoom" runat="server" ValidationGroup="Validation" AutoPostBack="True"
                                                                                                OnSelectedIndexChanged="ddlRooms_SelectedIndexChanged">
                                                                                                <asp:ListItem Value="0" Selected="True">0</asp:ListItem>
                                                                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                                                                <asp:ListItem Value="3">3</asp:ListItem>
                                                                                                <asp:ListItem Value="4">4</asp:ListItem>
                                                                                                <asp:ListItem Value="5">5</asp:ListItem>
                                                                                                <asp:ListItem Value="6">6</asp:ListItem>
                                                                                                <asp:ListItem Value="7">7</asp:ListItem>
                                                                                                <asp:ListItem Value="8">8</asp:ListItem>
                                                                                                <asp:ListItem Value="9">9</asp:ListItem>
                                                                                                <asp:ListItem Value="10">10</asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlNoRoom"
                                                                                                ErrorMessage="Required Field." InitialValue="0" ValidationGroup="Validation"
                                                                                                SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                        <td width="20%">
                                                                                            No.of Adults:
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:DropDownList ID="ddlAdults" runat="server">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td width="20%">
                                                                                            No.of Childs(Age 6-10):
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:DropDownList ID="ddlChilds" runat="server">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td bgcolor="#ffffff" align="center">
                                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                                            <asp:Button ID="btnContinue" Style="background-color: #5aa1ea" class="cgi1" Text=" Continue "
                                                                runat="Server" BorderStyle="None" ValidationGroup="Validation1" OnClick="btnContinue_Click" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td bgcolor="#ffffff" height="10">
                                                            <asp:Panel runat="server" ID="pnlGuestDetails" Visible="false">
                                                                <table style="border-collapse: collapse" bordercolor="#f0f0f0" cellspacing="3" cellpadding="3"
                                                                    width="100%" border="1" align="center">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td align="left" height="10" bgcolor="#5aa1ea" class="cgi1">
                                                                                Guest Details
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="txt">
                                                                                <asp:GridView ID="grdGuestDetails" runat="server" AutoGenerateColumns="False" GridLines="None"
                                                                                    CssClass="mGrid" Width="100%" PageSize="20" CellPadding="4" OnRowDataBound="grdGuestDetails_RowDataBound">
                                                                                    <AlternatingRowStyle CssClass="alt" />
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                    <Columns>
                                                                                        <asp:TemplateField>
                                                                                            <HeaderTemplate>
                                                                                                <asp:Label ID="Label1" runat="server" Text="SrNo."></asp:Label>
                                                                                            </HeaderTemplate>
                                                                                            <ItemTemplate>
                                                                                                <span>
                                                                                                    <%# Container.DataItemIndex + 1 %>
                                                                                                </span>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <HeaderTemplate>
                                                                                                <asp:Label ID="lblGuetType" runat="server" Text=""></asp:Label>
                                                                                            </HeaderTemplate>
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtGuestType" ReadOnly runat="server" Text='<%#Bind("GuestType") %>'
                                                                                                    BorderWidth="0" BackColor="Transparent"></asp:TextBox>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <HeaderTemplate>
                                                                                                <asp:Label ID="l1" runat="server" Text="Name"></asp:Label>
                                                                                            </HeaderTemplate>
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtName" runat="server" MaxLength="200"></asp:TextBox>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <HeaderTemplate>
                                                                                                <asp:Label ID="l1" runat="server" Text="Age"></asp:Label>
                                                                                            </HeaderTemplate>
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtAge" runat="server" onkeypress="return chkNumericForage(event);"
                                                                                                    MaxLength="2" Columns="5"></asp:TextBox>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <HeaderTemplate>
                                                                                                <asp:Label ID="l1" runat="server" Text="Gender"></asp:Label>
                                                                                            </HeaderTemplate>
                                                                                            <ItemTemplate>
                                                                                                <asp:DropDownList ID="ddlGender" runat="server">
                                                                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                                                                    <asp:ListItem Value="M">Male</asp:ListItem>
                                                                                                    <asp:ListItem Value="F">Female</asp:ListItem>
                                                                                                </asp:DropDownList>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center">
                                                                                <asp:Button ID="btnGuestDetails" Style="background-color: #5aa1ea" class="cgi1" Text="Continue "
                                                                                    runat="Server" BorderStyle="None" OnClick="btnGuestDetails_Click" OnClientClick="return VerifyGuestDeatils();" />
                                                                                &nbsp;
                                                                                <asp:Button ID="btnRe" runat="Server" BorderStyle="None" class="cgi1" OnClick="btnRe_Click"
                                                                                    Style="background-color: #5aa1ea" Text=" Reset " />
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td bgcolor="#ffffff" height="10">
                                                            <asp:Panel runat="server" ID="PnlGV" Visible="false">
                                                                <table style="border-collapse: collapse" bordercolor="#f0f0f0" cellspacing="3" cellpadding="3"
                                                                    width="100%" border="1" align="center">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td align="left" height="10" bgcolor="#5aa1ea" class="cgi1">
                                                                                Type Of Rooms
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="txt">
                                                                                <asp:Label ID="lblGV" runat="server"></asp:Label>
                                                                                <asp:GridView ID="gvRoomType" runat="server" AutoGenerateColumns="False" GridLines="None"
                                                                                    CssClass="mGrid" Width="100%" PageSize="20" CellPadding="4" OnRowDataBound="gvRoomType_RowDataBound"
                                                                                    DataKeyNames="roomtypeId">
                                                                                    <AlternatingRowStyle CssClass="alt" />
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                    <Columns>
                                                                                        <asp:BoundField DataField="roomtypeId" Visible="false" />
                                                                                        <asp:TemplateField>
                                                                                            <HeaderTemplate>
                                                                                                <asp:Label ID="Label1" runat="server" Text="SrNo."></asp:Label>
                                                                                            </HeaderTemplate>
                                                                                            <ItemTemplate>
                                                                                                <span>
                                                                                                    <%# Container.DataItemIndex + 1 %>
                                                                                                </span>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <HeaderTemplate>
                                                                                                <asp:Label ID="lbl1" runat="server" Text="Room Type"></asp:Label>
                                                                                            </HeaderTemplate>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblrtype" runat="server" Text='<%#Bind("roomtype") %>'></asp:Label>
                                                                                                <asp:HiddenField ID="hdFare" runat="server" Value='<%#Bind("fare") %>' />
                                                                                                <asp:HiddenField ID="hdExFare" runat="server" Value='<%#Bind("ExtraBedFare") %>' />
                                                                                                <asp:HiddenField ID="hdDisFare" runat="server" Value='<%#Bind("fare") %>' />
                                                                                                <asp:HiddenField ID="hdDisDays" runat="server" Value="0" />
                                                                                                <asp:HiddenField ID="hdOrgFare" runat="server" Value='<%#Bind("fare") %>' />
                                                                                                <asp:HiddenField ID="hdRoom" runat="server" />
                                                                                                <asp:HiddenField ID="hdAdults" runat="server" />
                                                                                                <asp:Label ID="lblroomtypeId" runat="server" Text='<%#Bind("roomtypeId") %>' Visible="false"></asp:Label>
                                                                                                <asp:HiddenField ID="hdDisType" Value="None" runat="server" />
                                                                                                <asp:HiddenField ID="hdnCWBFare" runat="server" Value='<%#Bind("ChildWithoutBedFare") %>' />
                                                                                                <asp:HiddenField ID="hdISAMT" runat="server" />
                                                                                                <asp:HiddenField ID="hdAmountDis" runat="server" />
                                                                                                <asp:HiddenField ID="hdfareid" runat="server" Value='<%#Bind("fareid") %>' />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <HeaderTemplate>
                                                                                                <asp:Label ID="l1" runat="server" Text="Adult"></asp:Label>
                                                                                            </HeaderTemplate>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblFare" runat="server" Text='<%#Bind("fare") %>'></asp:Label>
                                                                                                <asp:Label ID="lblDFare" runat="server" Text=""></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <HeaderTemplate>
                                                                                                <asp:Label ID="l2" runat="server" Text="Extra Bed"></asp:Label>
                                                                                            </HeaderTemplate>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblFareextraBed" runat="server" Text='<%#Bind("ExtraBedFare") %>'></asp:Label>
                                                                                                <asp:Label ID="lblDFareextraBed" runat="server" Text=""></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <HeaderTemplate>
                                                                                                <asp:Label ID="l3" runat="server" Text="ChildWithout Bed"></asp:Label>
                                                                                            </HeaderTemplate>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblFareChildWBed" runat="server" Text='<%#Bind("ChildWithoutBedFare") %>'></asp:Label>
                                                                                                <asp:Label ID="lblDFareChildWBed" runat="server" Text=""></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <%-- <asp:BoundField DataField="fare" HeaderText="Fare">
                                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                                    </asp:BoundField>--%>
                                                                                        <asp:TemplateField>
                                                                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                                            <ItemTemplate>
                                                                                                <asp:Button ID="btnbook" Style="background-color: #5aa1ea" class="cgi1" Text=" Book Now "
                                                                                                    OnClientClick='<%# "javascript:return calcamount("+ Container.DataItemIndex+");validate();" %>'
                                                                                                    runat="Server" BorderStyle="None" CommandName="<%# Container.DataItemIndex %>"
                                                                                                    OnClick="btnbook_Click" ValidationGroup="Validation" />
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                        <td align="right" bgcolor="#E7E7E7">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <img src="Assets/images/left_d_corner.gif" width="8" height="8"  loading="lazy" alt="left_corner"/>
                                        </td>
                                        <td bgcolor="#E7E7E7">
                                            <img src="Assets/images/trans.gif" width="1" height="1"  loading="lazy" alt="trans"/>
                                        </td>
                                        <td align="right">
                                            <img src="Assets/images/rgt_d_corner.gif" width="8" height="8" loading="lazy" alt="rgt_d_corner"/>
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
                </td>
            </tr>
        </table>
        <uc1:AgentFooter ID="AFooter" runat="server"></uc1:AgentFooter>
    </div>



    </form>

    <script language="javascript" type="text/javascript">
        paymenttype();
    </script>

    <script language="javascript" type="text/javascript">
        function showHidePickupInfo() {
            if (document.getElementById("<%=chkPickUp.ClientID%>").checked) {
                document.getElementById('divDropInfo').style.display = 'block'
                document.getElementById('divDrop').style.display = 'block'
                document.getElementById('divPickup').style.display = 'block'
            }
            else {
                document.getElementById('divDropInfo').style.display = 'none'
                document.getElementById('divDrop').style.display = 'none'
                document.getElementById('divPickup').style.display = 'none'

                //Pickup Reset
                document.getElementById("<%=RadFlight.ClientID%>").checked = true;
                document.getElementById("<%=RadTrain.ClientID%>").checked = false;
                document.getElementById("<%=RadBus.ClientID%>").checked = false;

                document.getElementById("<%=txtAddr.ClientID%>").value = "";
                document.getElementById("<%=txtStreet.ClientID%>").value = "";
                document.getElementById("<%=txtRlyStationName.ClientID%>").value = "";
                document.getElementById("<%=ddlTrainPkHr.ClientID%>").selectedIndex = 0;
                document.getElementById("<%=ddlTrainPkMin.ClientID%>").selectedIndex = 0;
                document.getElementById("<%=txtTrainNo.ClientID%>").value = "";
                document.getElementById("<%=txtpickVehicleNo.ClientID%>").selectedIndex = 0;
                document.getElementById("<%=txtFlightNo.ClientID%>").value = "";

                document.getElementById("<%=ddlPkHrs.ClientID%>").selectedIndex = 0;
                document.getElementById("<%=ddlPkMints.ClientID%>").selectedIndex = 0;

                //Drop Reset

                document.getElementById("<%=chkDrop.ClientID%>").checked = false;
                document.getElementById("<%=RadFlight_d.ClientID%>").checked = false;
                document.getElementById("<%=RadTrain_d.ClientID%>").checked = false;
                document.getElementById("<%=RadBus_d.ClientID%>").checked = false;

                document.getElementById("<%=txtpickVehicleNo_d.ClientID%>").selectedIndex = 0;
                document.getElementById("<%=ddlPkHrs_d.ClientID%>").value = "";
                document.getElementById("<%=ddlPkMints_d.ClientID%>").value = "";
                document.getElementById("<%=txtFlightNo_d.ClientID%>").value = "";
                document.getElementById("<%=txtRlyStationName_d.ClientID%>").value = "";
                document.getElementById("<%=ddlTrainPkHr_d.ClientID%>").value = "";
                document.getElementById("<%=ddlTrainPkMin_d.ClientID%>").value = "";
                document.getElementById("<%=txtTrainNo_d.ClientID%>").value = "";
                document.getElementById("<%=txtAddr_d.ClientID%>").value = "";
                document.getElementById("<%=txtStreet_d.ClientID%>").value = "";

                document.getElementById("hideTrain").style.display = "none";
                document.getElementById("hideLocation").style.display = "none";
                document.getElementById("hideFlight").style.display = "block";


                document.getElementById("hideTrain_d").style.display = "none";
                document.getElementById("hideLocation_d").style.display = "none";
                document.getElementById("hideFlight_d").style.display = "block";
            }
        }
    </script>

    <script language="javascript" type="text/javascript">
        function fnDrophide() {
            if (document.getElementById('chkDrop').checked == true) {
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
                if (document.getElementById('RadFlight').checked)
                    fnRDOSelection('', 'RadFlight');
                if (document.getElementById('RadTrain').checked)
                    fnRDOSelection('', 'RadTrain');
                if (document.getElementById('RadBus').checked)
                    fnRDOSelection('', 'RadBus');
            }
            else {
                //document.getElementById('divHide').style.display='block';
            }
        }


        function showHide(k) {
            if (document.getElementById('RadTrain' + k).checked) {
                document.getElementById('hideTrain' + k).style.display = 'block';
                document.getElementById('hideFlight' + k).style.display = 'none';
                document.getElementById('hideLocation' + k).style.display = 'none';
                if (k == "") {
                    document.getElementById('txtpickVehicleNo').selectedIndex = 0;
                    // document.getElementById('txtpicktime').value ='';
                    document.getElementById('txtFlightNo').value = '';

                    document.getElementById('txtAddr').value = '';
                    document.getElementById('txtStreet').value = '';
                    // To clear hte drop text boxes

                    document.getElementById('txtpickVehicleNo_d').selectedIndex = 0;
                    //  document.getElementById('txtpicktime_d').value ='';
                    document.getElementById('txtFlightNo_d').value = '';

                    document.getElementById('txtAddr_d').value = '';
                    document.getElementById('txtStreet_d').value = '';
                }
            }
            if (document.getElementById('RadFlight' + k).checked) {
                document.getElementById('hideTrain' + k).style.display = 'none';
                document.getElementById('hideFlight' + k).style.display = 'block';
                document.getElementById('hideLocation' + k).style.display = 'none';

                if (k == "") {
                    document.getElementById('txtAddr').value = '';
                    document.getElementById('txtStreet').value = '';

                    document.getElementById('txtRlyStationName').value = '';
                    //document.getElementById('txtRlyArrivalTime').value ='';
                    document.getElementById('txtTrainNo').value = '';
                    // To clear hte drop text boxes

                    document.getElementById('txtAddr_d').value = '';
                    document.getElementById('txtStreet_d').value = '';

                    document.getElementById('txtRlyStationName_d').value = '';
                    //document.getElementById('txtRlyArrivalTime_d').value ='';
                    document.getElementById('txtTrainNo_d').value = '';
                }
            }
            if (document.getElementById('RadBus' + k).checked) {
                document.getElementById('hideTrain' + k).style.display = 'none';
                document.getElementById('hideFlight' + k).style.display = 'none';
                document.getElementById('hideLocation' + k).style.display = 'block';
                if (k == "") {
                    document.getElementById('txtpickVehicleNo').selectedIndex = 0;
                    // document.getElementById('txtpicktime').value ='';
                    document.getElementById('txtFlightNo').value = '';

                    document.getElementById('txtRlyStationName').value = '';
                    // document.getElementById('txtRlyArrivalTime').value ='';
                    document.getElementById('txtTrainNo').value = '';
                    // To clear hte drop text boxes
                    document.getElementById('txtpickVehicleNo_d').selectedIndex = 0;
                    //  document.getElementById('txtpicktime_d').value ='';
                    document.getElementById('txtFlightNo_d').value = '';

                    document.getElementById('txtRlyStationName_d').value = '';
                    // document.getElementById('txtRlyArrivalTime_d').value ='';
                    document.getElementById('txtTrainNo_d').value = '';
                }
            }
        }
        function fnRDOSelection(k, idRad) {
            showHide(k);
            var chkDropObj = document.getElementById('chkDrop');
            if ((chkDropObj.checked) && (k == '')) {
                document.getElementById(idRad + '_d').checked = true;
                showHide('_d');
            }
        }
        function clear5() {
            if (!document.getElementById('chkDrop').checked) {
                var allTxt = document.getElementsByTagName('input');
                for (var i = 0; i < allTxt.length; i++) {
                    if (allTxt[i].id.indexOf('_d') != -1)
                        document.getElementById(allTxt[i].id).value = "";
                }
            }
        }


        function chkNoPick1() {
            if ((document.getElementById('chkNoPick').checked)) {
                document.getElementById('HideDetails').style.display = 'none';
                document.getElementById('RadBus').checked = true;
                document.getElementById('txtAddr').value = "Not decided";
                document.getElementById('txtStreet').value = "Not decided";
                document.getElementById('txtAddr_d').value = "Not decided";
                document.getElementById('txtStreet_d').value = "Not decided";
            }
            else {
                document.getElementById('HideDetails').style.display = 'block';
            }
        }
        
       
   
    </script>

</body>
</html>
