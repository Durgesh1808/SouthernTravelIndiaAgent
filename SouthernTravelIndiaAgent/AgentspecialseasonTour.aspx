<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgentspecialseasonTour.aspx.cs" Inherits="SouthernTravelIndiaAgent.AgentspecialseasonTour" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="uc1" TagName="AgentFooter" Src="UserControls/UcAgentFooter.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AgentHeader" Src="UserControls/UcAgentHeader.ascx" %>
<%@ Register Src="UserControls/UcSpecialTourFarePanel.ascx" TagName="SpecialTourFarePanel"
    TagPrefix="uc1" %>
<%@ Register Src="UserControls/UcSpecialFarePaneltable.ascx" TagName="SpecialFarePaneltable"
    TagPrefix="uc5" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Southern Travels :-: Agent Special Tour Booking</title>
    <link href="../Assets/css/style.css" rel="stylesheet" type="text/css" />
    <link href="../Assets/css/stylesheet.css" type="text/css" rel="stylesheet" />
    <link href="../Assets/css/calendar.css" rel="stylesheet" type="text/css" />
    <link href="../images/style.css" rel="stylesheet" type="text/css" />
    <link href="../Images/stylesheet.css" type="text/css" rel="stylesheet" />
    <link href="../indexResources/font-style.css" type="text/css" rel="stylesheet" />
    <link href="../Assets/css/calendar.css" rel="stylesheet" type="text/css" />
    <style>
        .Throug
        {
            text-decoration: line-through;
        }
        .DatePickerImage
        {
            position: relative;
            padding-left: 5px;
        }
        .modalBackground
        {
            background-color: Gray;
            filter: alpha(opacity=60);
            opacity: 0.60;
        }
        .updateProgress
        {
            border-width: 1px;
            border-style: solid;
            background-color: #FFFFFF;
            position: absolute;
            width: 150px;
            height: 50px;
        }
        .updateProgressMessage
        {
            margin: 3px;
            font-family: Trebuchet MS;
            font-size: small;
            vertical-align: middle;
        }
        .loading
        {
            background-image: url(https://www.southerntravelsindia.com/images/loading1.gif);
            background-position: right;
            background-repeat: no-repeat;
        }
        .AutoExtender
        {
            font-family: Verdana, Helvetica, sans-serif;
            font-size: .8em;
            font-weight: normal;
            border: solid 1px #006699;
            line-height: 20px;
            background-color: White;
        }
        .AutoExtenderList
        {
            border-bottom: dotted 1px #006699;
            cursor: pointer;
            color: Maroon;
        }
        .AutoExtenderHighlight
        {
            color: White;
            background-color: #006699;
            cursor: pointer;
        }
        .completionList
        {
            border: solid 1px #444444;
            margin: 0px;
            padding: 2px;
            overflow: auto;
            background-color: #FFFFFF;
            z-index: 6 !important;
        }
    </style>

    <script language="javascript" src="../Assets/js/spl_ST_calendar.js" type="text/javascript"></script>

    <script language="javascript" src="../Images/query-script.js" type="text/javascript"></script>

    <script language="javascript" src="../Assets/js/MyScript.js" type="text/javascript"></script>

    <%--<script language="javascript" src="JavaScript/GST_OnlineSplSeason.js" type="text/javascript"></script>--%>

    <script language="javascript" src="../Assets/js/GST_OnlineSplSeason.js" type="text/javascript"></script>

    <link href="../css/demos.css" rel="stylesheet" type="text/css" />
    <link type="text/css" href="../css/smoothness/jquery-ui-1.7.1.custom_blue.css" rel="stylesheet" />

    <script language="javascript" src="../Assets/js/MyScript.js" type="text/javascript"></script>

    <script src="../css/Js/jquery-1.3.2.min.js" type="text/javascript"></script>

    <script src="../css/Js/jquery-ui-1.7.1.custom.min.js" type="text/javascript"></script>

    <link href="../Assets/css/rupee.css" rel="stylesheet" type="text/css" />
    <style>
        .DatePickerImage
        {
            position: relative;
            padding-left: 5px;
        }
    </style>





    <style>
        .rupee
        {
            font-family: 'RupeeForadian';
        }
    </style>
    <style type="text/css">
        .black_overlay
        {
            display: none;
            position: absolute;
            top: 0%;
            left: 0%;
            width: 100%;
            height: 320%;
            background-color: Gray;
            z-index: 1001;
            -moz-opacity: 0.8;
        }
        .white_content
        {
            display: none;
            position: absolute;
            top: 180%;
            left: 10%;
            width: 80%;
            height: 40%;
            padding: 16px;
            border: 1px solid orange;
            background-color: white;
            z-index: 1002;
            overflow: auto;
        }
    </style>

   
        <link href="Assets/css/rupee.css" rel="stylesheet" type="text/css" />

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


    <script>
        function clearCitylist() {
            document.getElementById('txtCity').value = "";
            document.getElementById('txtZipCode').value = "";
            document.getElementById('hdnCity').value = "";
            document.getElementById('hdnCityId').value = "";
            document.getElementById('hdnStateIdBasedOnCity').value = "";
            return false;
        }
        function SetContextKey() {
            $find('<%=AutoCompleteExtender2.ClientID%>').set_contextKey($get("<%=ddlState.ClientID %>").value);
        }
        function ShowProcessImage(sender, e) {
            sender._element.className = "loading";
        }
        function HideProcessImage(sender, e) {
            sender._element.className = "";
        }
        function OnClientSelectedCity(source, eventArgs) {
            if (source) {
                // Get the HiddenField ID.
                var hdHACCity = source.get_id().replace("AutoCompleteExtender2", "hdnCity");
                $get(hdHACCity).value = eventArgs.get_text(); //.get_value();
                //alert(document.getElementById(hdHACCity).value);  //
                debugger;
                var hdnCityId = source.get_id().replace("AutoCompleteExtender2", "hdnCityId");
                var hdnStateIdBasedOnCity = source.get_id().replace("AutoCompleteExtender2", "hdnStateIdBasedOnCity");
                $get(hdnCityId).value = eventArgs.get_value().split("##")[0];
                $get(hdnStateIdBasedOnCity).value = eventArgs.get_value().split("##")[1];

                var stateid = document.getElementById("ddlState");
                var state = stateid.options[stateid.selectedIndex].value;
                if (state == "" || state == "0" || state == "--Select--" || state == "Select") {
                    //alert(document.getElementById(hdnStateIdBasedOnCity).value);
                    setSelectedValue(stateid, document.getElementById(hdnStateIdBasedOnCity).value);
                }
            }
        }

        function doValidate2() {
            debugger;
            if (Trim(document.Form1.txtName.value) == "") {
                alert("Please fill the Firstname .It is mandatory.");
                document.Form1.txtName.focus();
                return false;
            }
            var nam = document.Form1.txtName.value;
            if ((nam.length) < 3) {
                alert("Please Enter Minimum Three Characters in the name field");
                document.Form1.txtName.focus();
                return false;
            }
            //		    if (Trim(document.Form1.txtMail.value)== "" )	
            //		    {
            //			    alert("Please fill the e-mail field.It is mandatory.");
            //			    document.Form1.txtMail.focus();
            //			    return false;
            //		    }		    
            //		    else
            //		    {
            if (Trim(document.Form1.txtMail.value) != "") {
                if (CheckMail(document.Form1.txtMail.value) == false) {
                    //alert("Please enter your valid email Id.");
                    document.Form1.txtMail.value = "";
                    document.Form1.txtMail.focus();
                    return false;
                }
            }
            //		    }						
            if (Trim(document.Form1.txtAddress.value) == "") {
                alert("Please fill the address in address field.It is mandatory.");
                document.Form1.txtAddress.focus();
                return false;
            }
            if (Trim(document.Form1.ddlNationality.value) == "" || document.Form1.ddlNationality.value == "0") {
                alert("Please select Nationality.");
                document.Form1.ddlNationality.focus();
                return false;
            }
            if (Trim(document.Form1.ddlCountry.value) == "" || document.Form1.ddlCountry.value == "0") {
                alert("Please select Country.");
                document.Form1.ddlCountry.focus();
                return false;
            }
            if (document.Form1.ddlState.value == "0" && document.Form1.ddlCountry.value == "59") {
                alert('Please Select the State');
                document.Form1.ddlState.focus();
                chek = false;
                return false;
            }

            if (document.Form1.TxtForeignState.value == "" && document.Form1.ddlCountry.value != "59") {
                alert('Please Enter the State');
                document.Form1.TxtForeignState.focus();
                //                document.getElementById('<%= TxtForeignState.ClientID%>').focus();
                chek = false;
                return false;
            }

            //if (Trim(document.Form1.ddlCity.value) == "" || document.Form1.ddlCity.value == "0") {
            if (Trim(document.Form1.txtCity.value) == "" && document.Form1.ddlCountry.value == "59") {
                alert("Please select City.");
                document.Form1.txtCity.focus();
                chek = false;
                return false;
            }

            if (Trim(document.Form1.txtForeignCity.value) == "" && document.Form1.ddlCountry.value != "59") {
                alert("Please Enter City.");
                document.Form1.txtForeignCity.focus();
                chek = false;
                return false;
            }

            if (Trim(document.Form1.txtPhoneCountryCode.value) != "") {
                //                alert("Please fill the country code field.It is mandatory.");
                //                document.Form1.txtPhoneCountryCode.focus();
                //                return false;
                if (validateOnlyNumber1(Trim(document.Form1.txtPhoneCountryCode.value)) == false) {
                    alert("country code field should have numeric value only.");
                    document.Form1.txtPhoneCountryCode.value = "";
                    document.Form1.txtPhoneCountryCode.focus();
                    return false;
                }
            }

            if (Trim(document.Form1.txtPhone.value) != "") {
                var pho = document.Form1.txtPhone.value;
                if (validateOnlyNumber1(Trim(document.Form1.txtPhone.value)) == false) {
                    alert("Phone no. should have numeric value only.");
                    document.Form1.txtPhone.value = "";
                    document.Form1.txtPhone.focus();
                    return false;
                }
                else if ((pho.length) < 6) {
                    alert("Phone no. should have minimum 6 numbers");
                    document.Form1.txtPhone.focus();
                    return false;
                }
            }

            var txtmobile = document.getElementById('txtMobile');
            if (Trim(txtmobile.value) == "") {
                alert("Please enter Mobile No..");
                txtmobile.value = "";
                txtmobile.focus();
                return false;
            }
            if (Trim(document.Form1.txtMobile.value) != "") {
                if (validateOnlyNumber1(Trim(document.Form1.txtMobile.value)) == false) {
                    alert("Mobile no. should have numeric value only.");
                    document.Form1.txtMobile.value = "";
                    document.Form1.txtMobile.focus();
                    return false;
                }
                else {
                    var a = document.Form1.txtMobile.value;
                    if ((a.length < 10) | (a.length > 11)) {
                        alert("Invalid Mobile No")
                        document.Form1.txtMobile.value = "";
                        document.Form1.txtMobile.focus();
                        return false;
                    }
                }
            }
            var txtmobile = document.getElementById('txtAlternateMobileno');
            if (Trim(txtmobile.value) == "") {
                alert("Please enter Emergency Contact No..");
                txtmobile.value = "";
                txtmobile.focus();
                return false;
            }
            if (Trim(document.Form1.txtAlternateMobileno.value) != "") {
                if (validateOnlyNumber1(Trim(document.Form1.txtAlternateMobileno.value)) == false) {
                    alert("Emergency Contact No should have numeric value only.");
                    document.Form1.txtAlternateMobileno.value = "";
                    document.Form1.txtAlternateMobileno.focus();
                    return false;
                }

            }

            if (document.getElementById('rdbIsGSTApplicableYes').checked == true) {
                if (document.getElementById('txtCustomerGSTIN').value == "") {
                    alert('Please Enter Customer GSTIN.');
                    document.getElementById('txtCustomerGSTIN').focus();
                    return false;
                }
                else {
                    var GSTINNO = document.getElementById("txtCustomerGSTIN").value;
                    //if (!/([A-Z,a-z]){3}([A,B,C,F,G,H,J,L,P,T,a,b,c,f,g,h,j,l,p,t]){1}([A-Z,a-z]){1}([0-9]){4}([A-Z,a-z]){1}/.test(GSTINNO)) {
                    if (!/^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9]{1}Z[0-9A-Z]{1}$/.test(GSTINNO)) {
                        alert('Please enter Valid Customer GSTIN');
                        document.getElementById("txtCustomerGSTIN").focus();
                        return false;
                    }
                }
                if (document.getElementById('txtGstHolderName').value == "") {
                    alert('Please Enter GST Holder Name.');
                    document.getElementById('txtGstHolderName').focus();
                    return false;
                }
            }
            return validateruntime();
        }

        function validateruntime() {
            var chekSex = false;
            var chek = true;
            var i, len;
            len = document.Form1.length;
            var nchilds = 0;

            if (chek) {
                document.getElementById('Submit1').style.display = 'none';
            }
            return chek;

        }

        function setSelectedValue(selectObj, valueToSet) {
            //alert('');
            for (var i = 0; i < selectObj.options.length; i++) {
                if (selectObj.options[i].value == valueToSet) {
                    selectObj.options[i].selected = true;
                    return;
                }
            }
        }

        function OnChangeCountry(value) {
            if (value != 59) {
                document.getElementById('<%=TxtForeignState.ClientID %>').style.display = 'block';
                document.getElementById('<%=txtForeignCity.ClientID %>').style.display = 'block';
                document.getElementById('<%=ddlState.ClientID %>').style.display = 'none';
                document.getElementById('<%=txtCity.ClientID %>').style.display = 'none';

                document.getElementById('<%=TxtForeignState.ClientID %>').value = "";
                document.getElementById('<%=txtForeignCity.ClientID %>').value = "";
            }
            else {
                document.getElementById('<%=TxtForeignState.ClientID %>').style.display = 'none';
                document.getElementById('<%=txtForeignCity.ClientID %>').style.display = 'none';
                document.getElementById('<%=ddlState.ClientID %>').style.display = 'block';
                document.getElementById('<%=txtCity.ClientID %>').style.display = 'block';

                document.getElementById('<%=TxtForeignState.ClientID %>').value = "";
                document.getElementById('<%=txtForeignCity.ClientID %>').value = "";
            }
        }
        
            </script>
     <script type="text/javascript">

        $(function() {
            var dt = '';
            $("#txtDate").datepicker({

                numberOfMonths: 2,
                showOn: "button", autoSize: true,
                buttonImage: "../images/cal.gif",
                buttonImageOnly: true,
                minDate: new Date(),
                minDate: "+3D",
                closeText: 'Close',
                dateFormat: 'dd/mm/yy', buttonText: 'Choose a Date',
                onSelect: function(date) {
                    checkDate(date);
                    dt = date;
                    $('img.ui-datepicker-trigger').css({ 'cursor': 'pointer', "vertical-align": 'top' });
                    $('img.ui-datepicker-trigger').addClass('DatePickerImage');
                },
                onClose: function clearEndDate(date, inst) {
                    if (dt != '') {
                        checkDate(date);
                    }
                }
            });
            $('img.ui-datepicker-trigger').css({ 'cursor': 'pointer', "vertical-align": 'top' });
            $('img.ui-datepicker-trigger').addClass('DatePickerImage');
        });


        function GSTHideUnhide(val) {
            if (val == "yes") {
                document.getElementById('divGSTDetails').style.display = "";
                document.getElementById('divGSTDetails1').style.display = "";
            }
            else {
                document.getElementById('divGSTDetails').style.display = "none";
                document.getElementById('divGSTDetails1').style.display = "none";
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
     function approve() {
         window.open('../frmterms.aspx?', 'pops', 'width=418,height=249,scrollbars=yes');
     }
     function fnhideQualis() {
         /*if(document.getElementById('txtCar12pxHiddenSt').value==0 && document.getElementById('txtCar12pxHiddenDl').value==0)
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
         }*/
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
     function fnchkSingle() {
         if (ValidateNoOfPax()) {
             var ddlPax = document.getElementById('<%=ddlNoOfPax.ClientID %>');
             var ddlSelectedPax = ddlPax.options[ddlPax.selectedIndex].value;
             if (ddlSelectedPax == "0") {
                 alert("Please select No of Pax.");
                 document.getElementById('<%=ddlNoOfPax.ClientID %>').focus();
                 document.getElementById('<%=chkSingle.ClientID %>').checked = false;
                 //ExtrafareCal();
                 return false;
             }
             if ((document.getElementById('<%=chkSingle.ClientID %>').checked)) {
                 document.getElementById('<%=Singlepax.ClientID %>').style.display = 'block';
             }
             else {
                 document.getElementById('<%=Singlepax.ClientID %>').style.display = 'none';
                 finalFare();
             }
             if ((!document.getElementById('<%=chkSingle.ClientID %>').checked) && (document.getElementById('txtTotalExtrafare').value != "")) {
                 Getsuggession();
             }
         }
         else {
             return false;
         }
     }
     function fnchkSingle2() {
         if ((!document.getElementById('chkSingle').checked) && (document.getElementById('txtSinglePax').value > 0)) {
             alert("Please Check the Check box also");
             return false;
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
     function GetTourFare() {
         var Category = '';
         if (document.getElementById('<%=rdoStandard.ClientID %>').checked) {
             Category = 'St';
         }
         if (document.getElementById('<%=rdoDeluxe.ClientID %>').checked) {
             Category = 'Dl';
         }
         if (document.getElementById('<%=rdoLuxury.ClientID %>').checked) {
             Category = 'Lx';
         }
         if ((document.getElementById('<%=chkSingle.ClientID %>').checked)) {
             document.getElementById('<%=Singlepax.ClientID %>').style.display = 'block';
         }
         else {
             document.getElementById('<%=Singlepax.ClientID %>').style.display = 'none';
         }
         FareTypeCheck(Category);
         ExtrafareCal();
     }
     function ValidateJourneyDate() {

         if (document.getElementById('txtDate').value == "") {
             alert('Please first select a journey date.');
             return false;
         }
         if ((!document.getElementById('<%=rdoStandard.ClientID %>').checked) &&
         (!document.getElementById('<%=rdoDeluxe.ClientID %>').checked) &&
         (!document.getElementById('<%=rdoLuxury.ClientID %>').checked)) {
             alert('Please first select Category Type.');
             return false;
         }
         return true;
     }
     function ValidateNoOfPax() {
         if (ValidateJourneyDate()) {
             if ((!document.getElementById('<%=rdoStandard.ClientID %>').checked) &&
             (!document.getElementById('<%=rdoDeluxe.ClientID %>').checked) &&
             (!document.getElementById('<%=rdoLuxury.ClientID %>').checked)) {
                 alert('Please first select Category Type.');
                 return false;
             }
             if (document.getElementById('<%= ddlCarType.ClientID %>').selectedIndex != "0") {
                 return true;
             }
             else {
                 alert('Please select car type.');
                 document.getElementById('<%= ddlCarType.ClientID %>').focus();
                 return false;
             }
         }
         else {
             return false;
         }
     }
     //will be used for tours which has no of child with matress or without Matress

     function ValidatePaxWithChild(obj) {
         debugger;
         if (document.getElementById(obj).value == '')
             document.getElementById(obj).value = '0';

         var MaxPax = parseInt(document.getElementById('<%=hdfMaxVehiclePax.ClientID %>').value);
         var TotalPaxDrp = document.getElementById('<%= ddlNoOfPax.ClientID %>');
         var VehiclePaxSelected = parseInt(TotalPaxDrp.options[TotalPaxDrp.selectedIndex].text);

         var ChildWithoutMatress = parseInt(document.getElementById('<%=txtChildWhoutMatress.ClientID %>').value);
         var ChildWithMatress = parseInt(document.getElementById('<%=txtChildWithMatress.ClientID %>').value);

         var TotalPaxSelected = VehiclePaxSelected + ChildWithoutMatress + ChildWithMatress
         if (TotalPaxSelected > MaxPax) {
             document.getElementById(obj).value = '0';
             alert('Total pax selected should be less then the vehicle max pax');
             return false;
         }
         else if (TotalPaxSelected <= MaxPax) {
             //Methods to calucate the Fare
             ManageChildWithMatAndWitoutMatFare();
         }
     }

         </script>
        
    <script language="javascript" src="../includes/md5.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        function fnMd5(saltval) {

            encpass = hex_md5(saltval);
            document.getElementById('tmpEnValue').value = encpass;
            return true;
        }
    </script>


    <script type="text/javascript">
        // this function will be call on blur of txtchildwithmatress and txtchildWithout Matress
        function ManageChildWithMatAndWitoutMatFare() {
            if (document.getElementById('<%=txtChildWithMatress.ClientID %>').value == '')
                document.getElementById('<%=txtChildWithMatress.ClientID %>').value = '0';

            if (document.getElementById('<%=txtChildWhoutMatress.ClientID %>').value == '')
                document.getElementById('<%=txtChildWhoutMatress.ClientID %>').value = '0';

            document.getElementById('<%=hdfTotalChildWithMatress.ClientID %>').value = document.getElementById('<%=txtChildWithMatress.ClientID %>').value
            document.getElementById('<%=hdfTotalChildWithoutMatress.ClientID %>').value = document.getElementById('<%=txtChildWhoutMatress.ClientID %>').value

            var toalChildWithMatress = parseInt(document.getElementById('<%=hdfTotalChildWithMatress.ClientID %>').value);
            var toalChildWithoutMatress = parseInt(document.getElementById('<%=hdfTotalChildWithoutMatress.ClientID %>').value);

            var minAllowd = parseInt(document.getElementById('<%=hdfMinAllowedInVehicle.ClientID %>').value);
            var MaxAllowed = parseInt(document.getElementById('<%=hdfMaxAllowedInVehilce.ClientID %>').value); // for example max is 4 for car
            var totalPaxSelected = parseInt(document.getElementById('<%=hdfNoOfPaxSelected.ClientID %>').value);

            var totalChildToAdjusted = 0;
            var LeftChild = 0;

            if (MaxAllowed > totalPaxSelected) {
                totalChildToAdjusted = MaxAllowed - totalPaxSelected;
            }

            if (toalChildWithMatress > 0) {
                if (toalChildWithMatress >= totalChildToAdjusted) {
                    //- print 'under child with matress'
                    toalChildWithMatress = toalChildWithMatress - totalChildToAdjusted;
                    totalChildToAdjusted = 0;
                    // --print @childwihtmatrss
                }
                else if (toalChildWithMatress < totalChildToAdjusted) {
                    LeftChild = totalChildToAdjusted - toalChildWithMatress;
                    toalChildWithMatress = 0;
                    totalChildToAdjusted = LeftChild;
                }
            }

            if (LeftChild > 0)
                totalChildToAdjusted = LeftChild;

            if (totalChildToAdjusted > 0) {
                if (toalChildWithoutMatress > 0) {
                    if (toalChildWithoutMatress >= totalChildToAdjusted) {
                        toalChildWithoutMatress = toalChildWithoutMatress - totalChildToAdjusted;
                    }
                    else if (toalChildWithoutMatress < totalChildToAdjusted) {
                        LeftChild = totalChildToAdjusted - toalChildWithoutMatress;
                        toalChildWithoutMatress = 0;
                    }
                }
            }
            document.getElementById('<%=hdfTotalChildWithMatress.ClientID %>').value = toalChildWithMatress;
            document.getElementById('<%=hdfTotalChildWithoutMatress.ClientID %>').value = toalChildWithoutMatress;

            // Now calculate the finale fare.
            finalFare();
        }
    </script>
    <asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>
    <input type="hidden" id="tmpEnValue" runat="server" />
    <input type="hidden" id="txtServiceTax" runat="server" />
    <input type="hidden" id="txtCCVal" runat="server" />
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
    <input type="hidden" runat="server" id="etourname" />
    <input type="hidden" runat="server" id="txtHiddenFare" />
    <input type="hidden" runat="server" id="txtHiddenValue" />
    <input type="hidden" runat="server" id="txtMinPax" />
    <input type="hidden" runat="server" id="txtMaxPax" />
    <input type="hidden" runat="server" id="SARFare" />
    <input type="hidden" id="hdNoofDays" name="hdNoofDays" value="0" runat="server" />
    <asp:HiddenField ID="hdSPLDiscount" runat="server" Value="0" />
    <asp:HiddenField ID="hdfMaxVehiclePax" runat="server" Value="0" />
    <asp:HiddenField ID="hdfChildWithoutMatressFare" runat="server" Value="0" />
    <asp:HiddenField ID="hdfChildWithMatressFare" runat="server" Value="0" />
    <asp:HiddenField ID="hdfTotalChildWithMatress" runat="server" Value="0" />
    <asp:HiddenField ID="hdfTotalChildWithoutMatress" runat="server" Value="0" />
    <asp:HiddenField ID="hdfIsChildPaxAvailable" runat="server" Value="0" />
    <!-- When pax selected we will set the value in this and used to caluculate the child with matress fare -->
    <asp:HiddenField ID="hdfMinAllowedInVehicle" runat="server" Value="0" />
    <asp:HiddenField ID="hdfMaxAllowedInVehilce" runat="server" Value="0" />
    <asp:HiddenField ID="hdfNoOfPaxSelected" runat="server" Value="0" />
    <asp:HiddenField ID="hdfCaltotalFare" runat="server" Value="0" />
    <asp:HiddenField ID="hdfCalServicetax" runat="server" Value="0" />
    <asp:HiddenField ID="hdfCalDiscount" runat="server" Value="0" />
    <asp:HiddenField ID="hdfcalAdvance" runat="server" Value="0" />
            <asp:HiddenField ID="hdnTourFare" runat="server" Value="" />

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
                            <img src="Assets/images/left_corner.gif" alt="image" />
                        </td>
                        <td bgcolor="#e7e7e7">
                            <img src="Assets/images/trans.gif" alt="image" />
                        </td>
                        <td align="right">
                            <img src="Assets/images/rgt_corner.gif" alt="image" />
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#E7E7E7" style="width: 9px">
                            &nbsp;
                        </td>
                        <td align="center" width="100%">
                            <table cellpadding="1" cellspacing="1" border="0" width="100%" class="footer" style="text-align: left"
                                bgcolor="#ffffff">
                                <tr class="hlinks">
                                    <td align="center" colspan="2" bgcolor="#5aa1ea" class="verdana14w" height="25Px">
                                        <b>On Line Special Tour Details</b>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2">
                                        <table width="100%" border="0" cellspacing="2" cellpadding="0" bgcolor="#FFFFFF"
                                            align="center">
                                            <tr>
                                                <td class="hlinks" bgcolor="#ffffff" align="center" height="20" valign="bottom">
                                                    <b>Package cost per person in INR (Rs)</b>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100%;" bgcolor="#FFFFFF" height="180" align="left">
                                                    <%--<script language="javascript" src="../JavaScript/Spltours/tour<%=strTourId%>.js"
                                                        type="text/javascript"></script>--%>
                                                    <%--    <uc1:SpecialTourFarePanel ID="SpecialTourFarePanelUC" runat="server" CanBook="false"
                                                        fldPanelSection="SEC_Agent" />--%>
                                                    <uc5:SpecialFarePaneltable ID="SpecialTourFarePanelUC" runat="server" CanBook="false"
                                                        fldPanelSection="SEC_Agent" fldWidth="80%" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr class="hlinks">
                                    <td align="center" bgcolor="#5aa1ea" class="verdana14w" height="10Px">
                                        Tour Details
                                    </td>
                                    <td align="center" bgcolor="#5aa1ea" class="verdana14w" height="10Px">
                                        Personal Details
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2">
                                        <table align="center">
                                            <tr>
                                                <td>
                                                    <div id="imgWait" style="display: none;">
                                                        <img width="33px" height="33px" src="Assets/images/wait_spinner.gif" />
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
                                    <td valign="top">
                                        <table cellpadding="1" cellspacing="1" border="0" width="100%" class="footer" bgcolor="#ffffff">
                                            <tr id="trHide1" class="hlinks">
                                                <td style="width: 223px">
                                                    Tour Name :
                                                </td>
                                                <td>
                                                    <asp:Label ID="txttourName" runat="server" Font-Bold="true" BorderStyle="None" BorderWidth="0"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 223px">
                                                    Journey&nbsp; Date :*
                                                </td>
                                                <td>
                                                    <input id="txtDate" name="txtDate" type="text" runat="server" onblur="javascript:return checkDate(this.value);"
                                                        size="9" />
                                                    <%--<asp:TextBox ID="txtDate" runat="server"  onblur="javascript:return checkDate(this.value);"></asp:TextBox>
                                                    <input id="txtDate" name="txtDate" onchange="alert(this.value);" type="text" runat="server"
                                                        onblur="javascript:return checkDate(this.value);" size="9" onfocus="objCal('DepartIcon','txtDate','360','160'); " />
                                                    <a title="journey date" href="javascript:void(null)" onclick="objCal('DepartIcon','txtDate','360','160');"
                                                        hidefocus="">
                                                        <img id="DepartIcon" tabindex="-1" src="Assets/images/calendar.gif" border="0" style="visibility: visible;
                                                            padding-bottom: 0px;" /></a>--%>
                                                    <asp:Label ID="dept" CssClass="hlinks" runat="server">&nbsp;@&nbsp;<%=depttime.Value %></asp:Label>
                                                </td>
                                            </tr>

                                            <script language="javascript" type="text/javascript">
                                                stObj = new SOUTHERN.caldoy.Calendar2up("stObj", "STContainer", (thisMonth + 1) + "/" + thisYear, (thisMonth + 1) + "/" + (thisDay + 1) + "/" + thisYear);
                                                // stObj = new SOUTHERN.caldoy.Calendar2up("stObj","STContainer","05/2008","05/03/2008");
                                                stObj.title = " &nbsp;&nbsp;&nbsp;&nbsp;Select Date &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                                stObj.setChildFunction("onSelect", setDate);
                                                stObj.render();	
                                            </script>

                                            <tr>
                                                <td style="width: 223px; height: 28px;">
                                                    Category Type :
                                                </td>
                                                <td style="height: 28px">
                                                    Standard<input type="radio" runat="server" id="rdoStandard" name="rdocategory" onclick="javascript:FareTypeCheck(this.value);ExtrafareCal();"
                                                        value="St" />&nbsp; Deluxe<input type="radio" runat="server" id="rdoDeluxe" name="rdocategory"
                                                            onclick="FareTypeCheck(this.value),ExtrafareCal();" value="Dl" />
                                                    &nbsp;Premium<input type="radio" runat="server" id="rdoLuxury" name="rdocategory"
                                                        onclick="FareTypeCheck(this.value),ExtrafareCal();" value="Lx" />
                                                    &nbsp;&nbsp;
                                                    <asp:DropDownList ID="ddlSubCategory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSubCategory_SelectedIndexChanged">
                                                        <asp:ListItem Selected="True" Text="Select" Value="-1"></asp:ListItem>
                                                        <asp:ListItem Text="CP" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="MAP" Value="1"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr id="trHide3" class="hlinks">
                                                <td style="width: 223px">
                                                    Car &nbsp;Type :*
                                                </td>
                                                <td>
                                                    <%--<asp:TextBox ID="txtPax" MaxLength="2" size="5" onblur="Getsuggession();" Style="text-align: right"
                                                            runat="server"></asp:TextBox>--%>
                                                    <asp:DropDownList ID="ddlCarType" runat="server" onclick="return ValidateJourneyDate();"
                                                        OnSelectedIndexChanged="ddlCarType_SelectedIndexChanged" AutoPostBack="True">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr id="tr5" class="hlinks">
                                                <td style="width: 223px">
                                                    <asp:Label ID="lblNoOfpax" runat="server"> No Of Pax :*</asp:Label>
                                                </td>
                                                <td>
                                                    <%--<asp:TextBox ID="txtPax" MaxLength="2" size="5" onblur="Getsuggession();" Style="text-align: right"
                                                            runat="server"></asp:TextBox>--%>
                                                    <asp:DropDownList ID="ddlNoOfPax" runat="server" onclick="return ValidateNoOfPax();"
                                                        OnSelectedIndexChanged="ddlNoOfPax_SelectedIndexChanged" DataTextField="fldDisplayString"
                                                        DataValueField="fldPaxID" AutoPostBack="True">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr id="tr6" class="hlinks">
                                                <td colspan="4">
                                                    <table style="width: 100%;" id="tblChildPax" runat="server">
                                                        <tr>
                                                            <td style="width: 20%;">
                                                                Child without matress(6-11 Yrs)
                                                            </td>
                                                            <td style="width: 25%;">
                                                                <asp:TextBox ID="txtChildWhoutMatress" size="5" Style="text-align: right" runat="server"
                                                                    onblur="return ValidatePaxWithChild('txtChildWhoutMatress')" Text="0" onfocus="javascript: if(this.value == '0'){ this.value = ''; }"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 24%;">
                                                                Child with matress(6-11 Yrs)
                                                            </td>
                                                            <td style="width: 25%;">
                                                                <asp:TextBox ID="txtChildWithMatress" size="5" Style="text-align: right" runat="server"
                                                                    onblur="return ValidatePaxWithChild('txtChildWithMatress')" Text="0" onfocus="javascript: if(this.value == '0'){ this.value = ''; }"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr id="Tr1" class="hlinks">
                                                <td colspan="2">
                                                    <asp:CheckBox ID="chkSingle" runat="server" onclick="javascript:fnchkSingle();" />
                                                    <b><span><font color="red" size="3">*</font></span>Single Adult In a Room</b>&nbsp;
                                                </td>
                                            </tr>
                                            <tr id="Singlepax" class="hlinks" runat="server" align="left" style="display: none">
                                                <td style="width: 223px">
                                                    Single Pax
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtSinglePax" MaxLength="2" size="5" Style="text-align: right" Width="15"
                                                        runat="server" onblur="ExtrafareCal()" onclick="javascript:fnchkSingle2();"></asp:TextBox>@<asp:TextBox
                                                            ID="txtExtraFareS" Style="text-align: right" MaxLength="10" size="4" runat="server"
                                                            ReadOnly="true"></asp:TextBox>&nbsp;Fare<asp:TextBox ID="txtTotalExtrafare" MaxLength="10"
                                                                size="4" runat="server" Style="text-align: right" ReadOnly="true"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr id="Tr4" class="hlinks" align="left">
                                                <td colspan="5">
                                                    <b>#For single occupancy</b>
                                                </td>
                                            </tr>
                                            <tr id="Tr2" class="hlinks">
                                                <td style="width: 223px">
                                                    GST
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtTotalServiceTax" size="5" Style="text-align: right" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr class="hlinks" style="display: none">
                                                <td>
                                                    Convenience Charges:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCC" size="5" Style="text-align: right" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr id="Tr3" class="hlinks">
                                                <td style="width: 223px">
                                                    Total Fare
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtFareTotal" size="5" runat="server" Style="text-align: right"></asp:TextBox>
                                                    <asp:Label ID="lblDFare" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td align="left" style="width: 315px;" valign="top">
                                        <table width="100%" border="0" cellspacing="2" cellpadding="2" bgcolor="#FFFFFF"
                                            align="center">
                                            <tr>
                                                <td colspan="2" style="background-color: Yellow;" valign="middle">
                                                    <div style="width: 100%; text-align: center; color: Red;">
                                                        <strong>** Please select "State" carefully. State will not change after booking of ticket.</strong>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr class="hlinks" align="left" valign="top">
                                                <td align="left">
                                                    Name:*
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlTitle" runat="server" Width="30%">
                                                        <asp:ListItem Text="Title" Value="Title"></asp:ListItem>
                                                        <asp:ListItem Text="Mr." Value="Mr"></asp:ListItem>
                                                        <asp:ListItem Text="Mrs." Value="Mrs"></asp:ListItem>
                                                        <asp:ListItem Text="Miss." Value="Miss"></asp:ListItem>
                                                        <asp:ListItem Text="Dr." Value="Dr"></asp:ListItem>
                                                        <asp:ListItem Text="Prof." Value="Prof"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    &nbsp;
                                                    <asp:TextBox ID="txtFName" MaxLength="30" size="15" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr class="hlinks" align="left">
                                                <td align="left" style="height: 28px">
                                                    Nationality *
                                                </td>
                                                <td style="height: 28px">
                                                    <asp:DropDownList runat="server" ID="ddlNationality" class="sel" Width="150px">
                                                    </asp:DropDownList>
                                                    <font color="red"><strong>**</strong></font>
                                                    <%--AutoPostBack="True"
                                                        OnSelectedIndexChanged="ddlState_SelectedIndexChanged"--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" class="style3">
                                                    Country*
                                                </td>
                                                <td align="left" class="hlinks">
                                                    <asp:DropDownList ID="ddlCountry" runat="server" Width="150px" class="sel" onchange="OnChangeCountry(this.value);">
                                                    </asp:DropDownList>
                                                    <%--OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged"    AutoPostBack="True" --%>
                                                </td>
                                            </tr>
                                            <tr class="hlinks" align="left">
                                                <td align="left" style="height: 28px">
                                                    State *
                                                </td>
                                                <td class="hlinks" style="height: 28px">
                                                    <asp:DropDownList ID="ddlState" runat="server" Width="150px" onchange="return clearCitylist();">
                                                    </asp:DropDownList>
                                                    <asp:TextBox ID="TxtForeignState" runat="server" Style="display: none"></asp:TextBox>
                                                    <asp:HiddenField ID="hdfForeignState" runat="server" Value="48" />
                                                    <font color="red"><strong>**</strong></font>
                                                    <%--AutoPostBack="True"
                                                        OnSelectedIndexChanged="ddlState_SelectedIndexChanged"--%>
                                                </td>
                                            </tr>
                                            <tr class="hlinks" align="left">
                                                <td align="left" style="height: 28px">
                                                    Address:*
                                                </td>
                                                <td style="height: 28px">
                                                    <asp:TextBox ID="txtAddress" MaxLength="250" size="15" runat="server" TextMode="MultiLine" Width="291px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr class="hlinks" align="left">
                                                <td align="left" style="height: 28px">
                                                    City/District *
                                                </td>
                                                <td style="height: 28px">
                                                    <%--<asp:DropDownList ID="ddlCity" runat="server" Width="150px">
                                                    </asp:DropDownList>--%>
                                                    <asp:TextBox ID="txtCity" runat="server" autocomplete="off" onkeyup="SetContextKey()" ></asp:TextBox>
                                                    <asp:TextBox ID="txtForeignCity" runat="server" Style="display: none"></asp:TextBox>
                                                    <asp:HiddenField ID="hdfForeignCity" runat="server" Value="1630" />
                                                    <asp:HiddenField ID="hdnCity" runat="server" Value='' />
                                                    <asp:HiddenField ID="hdnCityId" runat="server" Value='' />
                                                    <asp:HiddenField ID="hdnStateIdBasedOnCity" runat="server" Value='' />
                                                    <cc1:AutoCompleteExtender runat="server" ID="AutoCompleteExtender2" TargetControlID="txtCity"
                                                        ServicePath="" ServiceMethod="GetCity" MinimumPrefixLength="1" EnableCaching="false"
                                                        CompletionListCssClass="completionList" CompletionListItemCssClass="AutoExtenderList"
                                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListElementID="divwidth"
                                                        OnClientPopulating="ShowProcessImage" OnClientPopulated="HideProcessImage" FirstRowSelected="false"
                                                        OnClientItemSelected="OnClientSelectedCity" UseContextKey="True" />
                                                </td>
                                            </tr>
                                            <tr class="hlinks" align="left">
                                                <td align="left" style="height: 28px">
                                                    Zip Code
                                                </td>
                                                <td style="height: 28px">
                                                    <asp:TextBox ID="txtZipCode" MaxLength="6" size="6" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr class="hlinks" align="left">
                                                <td align="left">
                                                    Mobile:*
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtMobile" MaxLength="11" size="15" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr class="hlinks" align="left">
                                                <td align="left">
                                                    Emergency Contact No:*
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtAlternateMobile" MaxLength="15" size="15" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr class="hlinks" align="left">
                                                <td align="left">
                                                    Phone No:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtphone" MaxLength="11" size="15" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr class="hlinks" align="left">
                                                <td align="left">
                                                    EmailId:*
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtMail" MaxLength="50" size="15" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            
                                             <tr class="hlinks" align="left">
                                                <td align="left">
                                                    Aadhar No:
                                                </td>
                                                <td>
                                                    <input id="txtAadharNo" runat="server" maxlength="20" type="text" />
                                                </td>
                                            </tr>
                                            
                                             <tr class="hlinks" align="left">
                                                <td align="left">
                                                    Image Aadhar Card:
                                                </td>
                                                <td>
                                                    <asp:FileUpload ID="fupAadhar" runat="server" />
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td align="left" colspan="2">
                                                    <b>
                                                        <asp:CheckBox runat="server" ID="chkPromotions" Text="Can Send Promotions ?" /></b>
                                                </td>
                                            </tr>
                                            <tr class="hlinks" align="left">
                                                <td align="left">
                                                    Comments:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtComment" MaxLength="150" size="15" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr class="hlinks" align="left">
                                                <td align="left">
                                                    Whether registered under GST :
                                                </td>
                                                <td class="hlinks" align="left">
                                                    <asp:RadioButton ID="rdbIsGSTApplicableYes" GroupName="GST" OnCheckedChanged="OnCheckChanged_rdbIsGSTApplicableYes"
                                                        OnClick="return GSTHideUnhide('yes');" runat="server" Text="Yes" />
                                                    <asp:RadioButton ID="rdbIsGSTApplicableNo" GroupName="GST" OnCheckedChanged="OnCheckChanged_rdbIsGSTApplicableNo"
                                                        OnClick="return GSTHideUnhide('no');" Checked="true" runat="server" Text="No" />
                                                </td>
                                            </tr>
                                            <tr class="hlinks" align="left" style="display: none;" runat="server" id="divGSTDetails">
                                                <td align="left" class="style3">
                                                    <span class="style3">GSTIN Of Customer :*</span>
                                                </td>
                                                <td class="hlinks" align="left">
                                                    <asp:TextBox title="Customer GSTIN" ID="txtCustomerGSTIN" placeholder="GSTIN Of Customer*"
                                                        type="text" name="txtCustomerGSTIN" runat="server" CssClass="form-control" />
                                                </td>
                                            </tr>
                                            <tr class="hlinks" align="left" style="display: none;" runat="server" id="divGSTDetails1">
                                                <td align="left">
                                                    Name of GST Holder :*
                                                </td>
                                                <td align="left" class="hlinks">
                                                    <asp:TextBox ID="txtGstHolderName" placeholder="Name of GST Holder*" type="text"
                                                        name="txtGstHolderName" runat="server" CssClass="form-control" />
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
                                        According to the Category.
                                    </td>
                                </tr>
                                <tr class="hlinks">
                                    <td colspan="2">
                                        <table border="0" cellpadding="0" width="100%" cellspacing="0">
                                            <tr>
                                                <td align="left" bgcolor="#5aa1ea" class="cgi1" style="height: 30px" width="50%">
                                                    <asp:CheckBox ID="chkPickUp" runat="server" onclick="showHidePickupInfo()" />PickUp
                                                    Information
                                                </td>
                                                <td align="center" bgcolor="#5aa1ea" class="cgi1" height="20px" width="50%">
                                                    <div id="divDropInfo" style="display: none;">
                                                        <table width="100%" cellpadding="0" cellspacing="0" border="0" bgcolor="#ffffff">
                                                            <tr class="hlinks" align="Center">
                                                                <td align="left" colspan="4" class="verdana11w" bgcolor="#5aa1ea">
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
                                                    <td align="left" style="height: 22px; width: 195px;">
                                                        PickUp From:
                                                    </td>
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
                                                        City:
                                                    </td>
                                                    <td>
                                                        <b>&nbsp;&nbsp;&nbsp;
                                                            <asp:Label ID="lblPkPoint" MaxLength="50" size="15" runat="server"></asp:Label></b>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table width="100%" border="0" id="hideFlight" cellspacing="2" cellpadding="1" bgcolor="#FFFFFF">
                                                <tr class="hlinks" align="left">
                                                    <td align="left" style="height: 14px; width: 212px;">
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
                                                            <option value="">min</option>
                                                            <option value="00">00</option>
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
                                                            <option value="">min</option>
                                                            <option value="00">00</option>
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
                                    <td colspan="2" width="50%">

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
                                                <tr>
                                                    <td align="Center" colspan="4" height="1" bgcolor="#cccccc">
                                                    </td>
                                                </tr>
                                                <tr class="hlinks" align="left">
                                                    <td align="left" style="height: 20px; width: 154px;">
                                                        Drop at:
                                                    </td>
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
                                                            <option value="">min</option>
                                                            <option value="00">00</option>
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
                                                            <option value="">min</option>
                                                            <option value="00">00</option>
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
                            </table>
                            <table cellpadding="1" cellspacing="1" border="0" width="100%" bgcolor="#ffffff">
                                <tr bgcolor="#FFFFFF">
                                    <td class="verdana11bk" style="height: 0px" bgcolor="#ffffff" align="center">

                                        <script language="javascript" type="text/javascript">
                                <!--

                                            function placeIt(obj) {
                                                obj = document.getElementById(obj);
                                                if (document.documentElement) {
                                                    theLeft = document.documentElement.scrollLeft;
                                                    theTop = document.documentElement.scrollTop;
                                                }
                                                else if (document.body) {
                                                    theLeft = document.body.scrollLeft;
                                                    theTop = document.body.scrollTop;
                                                }
                                                theLeft += x;
                                                theTop += y;
                                                obj.style.left = theLeft + 'px';
                                                obj.style.top = theTop + 'px';
                                                setTimeout("placeIt('layer1')", 500);

                                            }
                                 -->
                                        </script>

                                    </td>
                                </tr>
                                <tr bgcolor="#FFFFFF">
                                    <td align="center" colspan="2" class="verdana11bk">
                                        <asp:RadioButtonList ID="rbtnPaymentOption" runat="server" RepeatColumns="2" Style="display: none;">
                                            <asp:ListItem Text="50 % Payment" Value="HALF"></asp:ListItem>
                                            <asp:ListItem Text="Full Payment" Value="FullPayment"></asp:ListItem>
                                        </asp:RadioButtonList>
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
                                            CssClass="cgi1" OnClick="Submit_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="8" bgcolor="#E7E7E7">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 9px; height: 17px;">
                            <img src="Assets/images/left_d_corner.gif" />
                        </td>
                        <td bgcolor="#e7e7e7" style="height: 17px">
                            <img src="Assets/images/trans.gif" />
                        </td>
                        <td align="right" style="height: 17px">
                            <img src="Assets/images/rgt_d_corner.gif" />
                        </td>
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

</body>
</html>
