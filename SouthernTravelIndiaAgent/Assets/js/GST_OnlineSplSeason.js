var pvPath = window.location.pathname;
var pvPage = pvPath.substring(pvPath.lastIndexOf('/') + 1);
function CheckMail(str) {
    if (str.search(/^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/) != -1) {
        return true;
    }
    else {
        alert("Invalid E-mail ID");
        return false;
    }
}
function chkCarvisible() {
    if (document.getElementById('chkCar2').checked == true) {
        document.getElementById('txtCar12px').readOnly = false;
        document.getElementById('txtCar12pxValue').readOnly = true;
    }
    else {
        document.getElementById('txtCar12px').readOnly = true;
        document.getElementById('txtCar12px').value = "";
        document.getElementById('txtCar12pxValue').value = "";
        document.getElementById('txtCar12pxfare').value = "";
        document.getElementById('txtCar12pxValue').readOnly = true;
        finalFare();
    }
    if (document.getElementById('chkCar3-4').checked == true) {
        document.getElementById('txtCar34px').readOnly = false;
        document.getElementById('txtCar34pxValue').readOnly = true;
    }
    else {
        document.getElementById('txtCar34px').readOnly = true;
        document.getElementById('txtCar34px').value = "";
        document.getElementById('txtCar34pxValue').value = "";
        document.getElementById('txtCar34pxfare').value = "";
        document.getElementById('txtCar34pxValue').readOnly = true;
        finalFare();
    }
    if (document.getElementById('chkinnova4-5').checked == true) {
        document.getElementById('txtInn45Px').readOnly = false;
        document.getElementById('txtInn45PxValue').readOnly = true;
    }
    else {
        document.getElementById('txtInn45Px').readOnly = true;
        document.getElementById('txtInn45Px').value = "";
        document.getElementById('txtInn45PxValue').value = "";
        document.getElementById('txtInn45Pxfare').value = "";
        document.getElementById('txtInn45PxValue').readOnly = true;
        finalFare();
    }
    if (document.getElementById('chkinnova6-7').checked == true) {
        document.getElementById('txtInn67Px').readOnly = false;
        document.getElementById('txtInn67PxValue').readOnly = true;
    }
    else {
        document.getElementById('txtInn67Px').readOnly = true;
        document.getElementById('txtInn67Px').value = "";
        document.getElementById('txtInn67PxValue').value = "";
        document.getElementById('txtInn67Pxfare').value = "";
        document.getElementById('txtInn67PxValue').readOnly = true;
        finalFare();
    }
    if (document.getElementById('chkqualis4-6').checked == true) {
        document.getElementById('txtQua46Px').readOnly = false;
        document.getElementById('txtQua46PxValue').readOnly = true;
        var unitVal = parseFloat(document.getElementById('txtQua46PxHidden' + fareType).value);
        if (unitVal <= 0) {
            document.getElementById('chkqualis4-6').checked = false;
            alert('This vechile is not available');
        }
    }
    else {
        document.getElementById('txtQua46Px').readOnly = true;
        document.getElementById('txtQua46Px').value = "";
        document.getElementById('txtQua46PxValue').value = "";
        document.getElementById('txtQua46Pxfare').value = "";
        document.getElementById('txtQua46PxValue').readOnly = true;
        finalFare();
    }
    if (document.getElementById('chkqualis7-8').checked == true) {
        document.getElementById('txtQua78Px').readOnly = false;
        document.getElementById('txtQua78PxValue').readOnly = true;
        var unitVal = parseFloat(document.getElementById('txtQua78PxHidden' + fareType).value);
        if (unitVal <= 0) {
            document.getElementById('chkqualis7-8').checked = false;
            alert('This vechile is not available');
        }
    }
    else {
        document.getElementById('txtQua78Px').readOnly = true;
        document.getElementById('txtQua78Px').value = "";
        document.getElementById('txtQua78PxValue').value = "";
        document.getElementById('txtQua78Pxfare').value = "";
        document.getElementById('txtQua78PxValue').readOnly = true;
        finalFare();
    }
    if (document.getElementById('chktempo8-9').checked == true) {
        document.getElementById('txtTem89Px').readOnly = false;
        document.getElementById('txtTem89PxValue').readOnly = true;
    }
    else {
        document.getElementById('txtTem89Px').readOnly = true;
        document.getElementById('txtTem89Px').value = "";
        document.getElementById('txtTem89PxValue').value = "";
        document.getElementById('txtTem89Pxfare').value = "";
        document.getElementById('txtTem89PxValue').readOnly = true;
        finalFare();
    }
}
var fareType = 'Dl';
function FareTypeCheck(type) {

    if (document.getElementById('txtDate').value == "") {
        //alert('Please first select a journey date.');
        Swal.fire({
            icon: 'warning',
            title: 'Oops...',
            text: 'Please first select a journey date.',
            confirmButtonColor: '#f2572b'
        });
        return false;
    }
    else {
        fareType = type;
        var IndexValue = document.getElementById('ddlNoOfPax');
        if ((IndexValue.selectedIndex != "-1") && (IndexValue.selectedIndex != "0")) {
            var lCarType = 'txt';
            var paxN0 = IndexValue.options[IndexValue.selectedIndex].value;
            var SelectedPax = paxN0.split("-");
            if (SelectedPax[1] == "2") {
                fare('txtCar12px');
            }
            else {
                var CarTypeValue = document.getElementById('ddlCarType');
                var lSelectedCarType = CarTypeValue.options[CarTypeValue.selectedIndex].text;
                lCarType = lCarType + lSelectedCarType.substring(0, 3) + SelectedPax[1] + SelectedPax[2];
                if (SelectedPax[1] == "3") {
                    lCarType = lCarType + "px";
                }
                else {
                    lCarType = lCarType + "Px";
                }
                fare(lCarType);
            }
        }
    }
}
function ExtrafareCal() {
    debugger;
    var dbSt = (document.getElementById('txtServiceTax').value);
    var singlePax = parseInt(document.getElementById('txtSinglePax').value);
    var IndexValue = document.getElementById('ddlNoOfPax');
    var paxN0 = IndexValue.options[IndexValue.selectedIndex].value;
    var SelectedPax = paxN0.split("-");
    if (parseInt(IndexValue.options[IndexValue.selectedIndex].text) < parseInt(SelectedPax[1])) {
        paxN0 = parseInt(SelectedPax[1]);
    }
    else {
        paxN0 = parseInt(IndexValue.options[IndexValue.selectedIndex].text);
    }
    var tPax = parseInt(paxN0);
    if ((singlePax > 0) && (tPax > 0)) {
        if (singlePax > tPax) {
            alert("Single pax can not be greater than Total Pax");
            document.getElementById('txtTotalExtrafare').value = "";
            document.getElementById('txtExtraFareS').value = "";
            document.getElementById('txtSinglePax').value = "";
            //return false;
        }
    }
    if (document.getElementById('rdoStandard').checked == true) {
        var k = document.getElementById('txtSinglePax').value;
        if (k != 0) {
            document.getElementById('txtExtraFareS').value = document.getElementById('SARFare').value;
            var s = k * parseInt(document.getElementById('SARFare').value);
            document.getElementById('txtTotalExtrafare').value = Math.round(s);
            var fare = document.getElementById('txtHiddenValue').value;
            var allSt = (parseInt(s) + parseInt(fare));
            var st = Math.round(parseFloat((allSt) * (dbSt / 100)));
            var CC = Math.round(parseFloat(fare) + parseFloat(s) + parseFloat(st));  // Calculating CC charges
            var CCVal = document.getElementById('txtCCVal').value;
            var CC1 = 0;
            CC1 = Math.round((CC) * (CCVal / 100));
            document.getElementById('txtTotalServiceTax').value = st + CC1;
            document.getElementById('txtCC').value = CC1;
            document.getElementById('txtFareTotal').value = Math.round(parseFloat(st) + parseInt(fare) + parseInt(s) + (CC1));
            document.getElementById('txtFareTotal').readOnly = true;
            document.getElementById('txtCC').readOnly = true;
        }
        else {
            document.getElementById('txtExtraFareS').value = "";
            document.getElementById('txtSinglePax').value = "";
            document.getElementById('txtTotalExtrafare').value = "";
            var fare = document.getElementById('txtHiddenValue').value;
            var allSt = (parseInt(fare));
            var st = Math.round(parseFloat((allSt) * (dbSt / 100)));
            var CC = Math.round(parseFloat(fare) + parseFloat(st));  // Calculating CC charges
            var CCVal = document.getElementById('txtCCVal').value;
            var CC1 = 0;
            CC1 = Math.round((CC) * (CCVal / 100));
            document.getElementById('txtTotalServiceTax').value = st + CC1;
            document.getElementById('txtCC').value = CC1;
            document.getElementById('txtFareTotal').value = Math.round(parseFloat(st) + parseInt(fare) + (CC1));
            document.getElementById('txtFareTotal').readOnly = true;
            document.getElementById('txtCC').value = CC1;
            document.getElementById('txtCC').readOnly = true;
            return false;
        }
    }
    if (document.getElementById('rdoDeluxe').checked == true) {
        var k = document.getElementById('txtSinglePax').value;
        if (k != 0) {
            document.getElementById('txtExtraFareS').value = document.getElementById('SARFare').value;
            var s = k * parseInt(document.getElementById('SARFare').value);
            document.getElementById('txtTotalExtrafare').value = Math.round(s);
            var fare = document.getElementById('txtHiddenValue').value;
            var allSt = (parseInt(s) + parseInt(fare));
            var st = Math.round(parseFloat((allSt) * (dbSt / 100)));
            // var CC= Math.round(parseFloat(fare)+parseFloat(st));  // Calculating CC charges
            var CC = Math.round(parseFloat(fare) + parseFloat(s) + parseFloat(st));
            var CCVal = document.getElementById('txtCCVal').value;
            var CC1 = 0;
            CC1 = Math.round((CC) * (CCVal / 100));
            document.getElementById('txtTotalServiceTax').value = st + CC1;
            document.getElementById('txtCC').value = CC1;
            document.getElementById('txtFareTotal').value = (parseFloat(st) + parseFloat(fare) + parseFloat(s) + (CC1));
            document.getElementById('txtFareTotal').readOnly = true;
            document.getElementById('txtCC').value = CC1;
            document.getElementById('txtCC').readOnly = true;
        }
        else {
            document.getElementById('txtExtraFareS').value = "";
            document.getElementById('txtSinglePax').value = "";
            document.getElementById('txtTotalExtrafare').value = "";
            var fare = document.getElementById('txtHiddenValue').value;
            var allSt = (parseInt(fare));
            var st = Math.round(parseFloat((allSt) * (dbSt / 100)));
            var CC = Math.round(parseFloat(fare) + parseFloat(st));  // Calculating CC charges
            var CCVal = document.getElementById('txtCCVal').value;
            var CC1 = 0;
            CC1 = Math.round((CC) * (CCVal / 100));
            document.getElementById('txtTotalServiceTax').value = st + CC1;
            document.getElementById('txtCC').value = CC1;
            document.getElementById('txtFareTotal').value = Math.round(parseFloat(st) + parseInt(fare) + (CC1));
            document.getElementById('txtFareTotal').readOnly = true;
            document.getElementById('txtCC').value = CC1;
            document.getElementById('txtCC').readOnly = true;
            Getsuggession();
            return false;
        }
    }
    if (document.getElementById('rdoLuxury').checked == true) {
        var k = document.getElementById('txtSinglePax').value;
        if (k != 0) {
            document.getElementById('txtExtraFareS').value = document.getElementById('SARFare').value;
            var s = k * parseInt(document.getElementById('SARFare').value);
            document.getElementById('txtTotalExtrafare').value = Math.round(s);
            var fare = document.getElementById('txtHiddenValue').value;
            var allSt = (parseInt(s) + parseInt(fare));
            var st = Math.round(parseFloat((allSt) * (dbSt / 100)));
            // var CC= Math.round(parseFloat(fare)+parseFloat(st));  // Calculating CC charges
            var CC = Math.round(parseFloat(fare) + parseFloat(s) + parseFloat(st));
            var CCVal = document.getElementById('txtCCVal').value;
            var CC1 = 0;
            CC1 = Math.round((CC) * (CCVal / 100));
            document.getElementById('txtTotalServiceTax').value = st + CC1;
            document.getElementById('txtCC').value = CC1;
            document.getElementById('txtFareTotal').value = (parseFloat(st) + parseFloat(fare) + parseFloat(s) + (CC1));
            document.getElementById('txtFareTotal').readOnly = true;
            document.getElementById('txtCC').value = CC1;
            document.getElementById('txtCC').readOnly = true;
        }
        else {
            document.getElementById('txtExtraFareS').value = "";
            document.getElementById('txtSinglePax').value = "";
            document.getElementById('txtTotalExtrafare').value = "";
            var fare = document.getElementById('txtHiddenValue').value;
            var allSt = (parseInt(fare));
            var st = Math.round(parseFloat((allSt) * (dbSt / 100)));
            var CC = Math.round(parseFloat(fare) + parseFloat(st));  // Calculating CC charges
            var CCVal = document.getElementById('txtCCVal').value;
            var CC1 = 0;
            CC1 = Math.round((CC) * (CCVal / 100));
            document.getElementById('txtTotalServiceTax').value = st + CC1;
            document.getElementById('txtCC').value = CC1;
            document.getElementById('txtFareTotal').value = Math.round(parseFloat(st) + parseInt(fare) + (CC1));
            document.getElementById('txtFareTotal').readOnly = true;
            document.getElementById('txtCC').value = CC1;
            document.getElementById('txtCC').readOnly = true;
            Getsuggession();
            return false;
        }
    }
}
function fare(senderId) {
    var IndexValue = document.getElementById('ddlNoOfPax');
    if (IndexValue.selectedIndex != "-1" && IndexValue.selectedIndex != "0") {
        var paxN0 = IndexValue.options[IndexValue.selectedIndex].value;
        var SelectedPax = paxN0.split("-");
        if (parseInt(IndexValue.options[IndexValue.selectedIndex].text) < parseInt(SelectedPax[1])) {
            paxN0 = parseInt(SelectedPax[1]);
        }
        else {
            paxN0 = parseInt(IndexValue.options[IndexValue.selectedIndex].text);
        }
        if (paxN0 == "0") {
            return;
        }
        //var unitVal = parseFloat(document.getElementById(senderId + 'Hidden' + fareType).value); 
        var unitVal = parseFloat(document.getElementById('txtHiddenFare').value);
        if (unitVal > 0) {
            //document.getElementById('txtHiddenFare').value=unitVal;
            document.getElementById('txtHiddenValue').value = parseFloat(unitVal * paxN0);
        }
        else {
            alert('This vechile is not available . Please select manually');
            //document.getElementById('txtHiddenFare').value = unitVal;
            document.getElementById('txtHiddenValue').value = parseFloat(unitVal * paxN0);
            document.getElementById('txtHiddenValue').value = 0;
            //document.getElementById(senderId + 'Hidden' + fareType)
            //document.getElementById(senderId).value = 0;
        }
        document.getElementById('txtFareTotal').readOnly = true;
        document.getElementById('txtTotalServiceTax').readOnly = true;
    }
    else {
        return;
    }
}
function finalFare() {
    debugger;
    var allTxt = document.getElementsByTagName('input');
    var totalAmt = parseFloat(document.getElementById('txtHiddenValue').value);
    var IndexValue = document.getElementById('ddlNoOfPax');
    var totalpax = IndexValue.options[IndexValue.selectedIndex].value;
    var SelectedPax = totalpax.split("-");
    if (parseInt(IndexValue.options[IndexValue.selectedIndex].text) < parseInt(SelectedPax[1])) {
        totalpax = parseInt(SelectedPax[1]);
    }
    else {
        totalpax = parseInt(IndexValue.options[IndexValue.selectedIndex].text);
    }
    var dbSt = (document.getElementById('txtServiceTax').value);
    var st = Math.round(parseFloat((totalAmt) * (dbSt / 100)));
    var CC = Math.round(parseFloat(totalAmt) + parseFloat(st));  // Calculating CC charges
    var CCVal = parseFloat(document.getElementById('txtCCVal').value);

    var CC1 = 0;
    var CC1 = Math.round(parseFloat((CC) * (CCVal / 100)));
    //    alert(dbSt);
    document.getElementById('txtTotalServiceTax').value = (st + CC1);
    document.getElementById('txtCC').value = CC1;
    document.getElementById('txtFareTotal').value = Math.round(st + totalAmt + CC1);

    document.getElementById('txtCC').readOnly = true;
}
function car2(senderId) {
    if (document.getElementById('chkCar2').checked == true) {
        if ((parseInt(document.getElementById(senderId).value) % parseInt(document.getElementById('car2min').value) == 0) || (parseInt(document.getElementById(senderId).value) % parseInt(document.getElementById('car2max').value) == 0) || (parseInt(document.getElementById(senderId).value) % (parseInt(document.getElementById('car2max').value) + parseInt(document.getElementById('car2min').value)) == 0)) {
            fare(senderId);
        }
        else {
            alert('Please enter the Multiples of Two Passengers');
            document.getElementById(senderId).value = "";
            fare(senderId)
            document.getElementById(senderId + 'Value').value = "";
            document.getElementById(senderId + 'fare').value = "";
            finalFare();
            return;
        }
    }
}
function car34(senderId) {
    if (document.getElementById('chkCar3-4').checked == true) {
        if ((parseInt(document.getElementById(senderId).value) % parseInt(document.getElementById('car34min').value) == 0) || (parseInt(document.getElementById(senderId).value) % parseInt(document.getElementById('car34max').value) == 0) || (parseInt(document.getElementById(senderId).value) % (parseInt(document.getElementById('car34max').value) + parseInt(document.getElementById('car34min').value))) == 0) {
            fare(senderId)
        }
        else {
            alert('Please enter Multiples of 3 or 4 Passengers');
            document.getElementById(senderId).value = "";
            fare(senderId)
            document.getElementById(senderId + 'Value').value = "";
            document.getElementById(senderId + 'fare').value = "";
            finalFare();
            return;
        }
    }
}
function innova45(senderId) {
    if (document.getElementById('chkinnova4-5').checked == true) {
        if ((parseInt(document.getElementById(senderId).value) % parseInt(document.getElementById('innova45min').value) == 0) || (parseInt(document.getElementById(senderId).value) % parseInt(document.getElementById('innova45max').value) == 0) || (parseInt(document.getElementById(senderId).value) % (parseInt(document.getElementById('innova45min').value) + parseInt(document.getElementById('innova45max').value))) == 0) {
            fare(senderId)
        }
        else {
            alert('Please enter Multiples of 4 or 5 Passengers');
            document.getElementById(senderId).value = "";
            fare(senderId)
            document.getElementById(senderId + 'Value').value = "";
            document.getElementById(senderId + 'fare').value = "";
            finalFare();
            return;
        }
    }
}
function innova67(senderId) {
    if (document.getElementById('chkinnova6-7').checked == true) {
        if ((parseInt(document.getElementById(senderId).value) % parseInt(document.getElementById('innova67min').value) == 0) || (parseInt(document.getElementById(senderId).value) % parseInt(document.getElementById('innova67max').value) == 0) || (parseInt(document.getElementById(senderId).value) % (parseInt(document.getElementById('innova67min').value) + parseInt(document.getElementById('innova67max').value))) == 0) {
            fare(senderId)
        }
        else {
            alert('Please enter Multiples of 6 or 7 Passengers');
            document.getElementById(senderId).value = "";
            fare(senderId)
            document.getElementById(senderId + 'Value').value = "";
            document.getElementById(senderId + 'fare').value = "";
            finalFare();
            return;
        }
    }
}
function qualis46(senderId) {
    if (document.getElementById('chkqualis4-6').checked == true) {
        if ((parseInt(document.getElementById(senderId).value) % parseInt(document.getElementById('qualis46min').value) == 0) || (parseInt(document.getElementById(senderId).value) % parseInt(document.getElementById('qualis46max').value) == 0) || (parseInt(document.getElementById(senderId).value) % parseInt(5) == 0) || (parseInt(document.getElementById(senderId).value) % (parseInt(document.getElementById('qualis46max').value) + parseInt(document.getElementById('qualis46min').value))) == 0) {
            fare(senderId)
        }
        else {
            alert('Please enter Multiples of 4 or 5 or 6 Passengers');
            document.getElementById(senderId).value = "";
            fare(senderId)
            document.getElementById(senderId + 'Value').value = "";
            document.getElementById(senderId + 'fare').value = "";
            finalFare();
            return;
        }
    }
}
function qualis78(senderId) {
    if (document.getElementById('chkqualis7-8').checked == true) {
        if ((parseInt(document.getElementById(senderId).value) % parseInt(document.getElementById('qualis78min').value) == 0) || (parseInt(document.getElementById(senderId).value) % parseInt(document.getElementById('qualis78max').value) == 0) || (parseInt(document.getElementById(senderId).value) % (parseInt(document.getElementById('qualis78min').value) + parseInt(document.getElementById('qualis78max').value))) == 0) {
            fare(senderId)
        }
        else {
            alert('Please enter Multiples of 7 or 8 Passengers');
            document.getElementById(senderId).value = "";
            //document.getElementById(senderId).focus();
            fare(senderId)
            document.getElementById(senderId + 'Value').value = "";
            document.getElementById(senderId + 'fare').value = "";
            finalFare();
            return;
        }
    }
}
function tempo89(senderId) {
    if (document.getElementById('chktempo8-9').checked == true) {
        if ((parseInt(document.getElementById(senderId).value) % parseInt(document.getElementById('tempo89min').value) == 0) || (parseInt(document.getElementById(senderId).value) % parseInt(document.getElementById('tempo89max').value) == 0) || (parseInt(document.getElementById(senderId).value) % (parseInt(document.getElementById('tempo89max').value) + parseInt(document.getElementById('tempo89min').value))) == 0) {
            fare(senderId)
        }
        else {
            alert('Please enter Multiples of 8 or 9 Passengers');
            document.getElementById(senderId).value = "";
            //document.getElementById(senderId).focus();
            fare(senderId)
            document.getElementById(senderId + 'Value').value = "";
            document.getElementById(senderId + 'fare').value = "";
            finalFare();
            return;
        }
    }
}

function PostPage() {
    document.getElementById('hidPundit').value = 'calcPundit';
    document.form1.submit();
}
function clear() {
    var allTxt = document.getElementsByTagName('input');
    //var s= document.getElementsByTagName('chkDrop').value;    
    for (var i = 0; i < allTxt.length; i++) {
        if (allTxt[i].type == 'checkbox') {
            if (allTxt[i].id != 'chkDrop')
                document.getElementById(allTxt[i].id).checked = false;
            //chkCarvisible();
        }
    }
}
var pax = '';
function Getsuggession() {
    clear();
    var IndexValue = document.getElementById('ddlNoOfPax');
    var paxN0 = IndexValue.options[IndexValue.selectedIndex].value;
    var SelectedPax = paxN0.split("-");
    if (parseInt(IndexValue.options[IndexValue.selectedIndex].text) < parseInt(SelectedPax[1])) {
        paxN0 = parseInt(SelectedPax[1]);
    }
    else {
        paxN0 = parseInt(IndexValue.options[IndexValue.selectedIndex].text);
    }
    var tPax = parseInt(paxN0);
    if (parseInt(IndexValue.selectedIndex) > 0) {
        if (parseInt(IndexValue.selectedIndex) == 0) {
            return;
        }
        else if (parseInt(paxN0) > 18) {
            alert('Upto 18 Members will allow, Other wise Request Will send');
            return;
        }
        pax = parseInt(paxN0);
        document.getElementById("txtSinglePax").value = "";
        document.getElementById("txtExtraFareS").value = "";
        document.getElementById("txtTotalExtrafare").value = "";
    }
    /*xmlHttp=GetXmlHttpObject()
    if (xmlHttp==null)
    {
    alert ("Browser does not support HTTP Request")
    return
    }
    var img = document.getElementById('imgWait');
    img.style.display = 'block'; 			
    var url="test1.aspx?paxnum=" + escape(pax);	
    if (pvPage.toLowerCase() == "agentspecialseasontour.aspx")
    {
    url = "../" + url;
    }
    url=url+"&sid="+Math.random();
    url=url+"&tourid="+document.getElementById("tid").value;						
    xmlHttp.onreadystatechange=stateChanged1
    xmlHttp.open("GET",url,true)
    xmlHttp.send(null)*/
}
function stateChanged1() {
    if ((xmlHttp.readyState == 4 || xmlHttp.readyState == "complete")) {
        var img = document.getElementById('imgWait');
        img.style.display = 'none';

        var arr = xmlHttp.responseText;
        var aa = arr.split("^");
        //alert(aa[0]);
        var bb = aa[1];
        var cc = bb.split("#");
        /*for(var i=0;i<cc.length;i++)
        {
        var dd=cc[i].split("-");
        //alert(dd[0]);
        if(dd[0]==1)
        {
        //chkCarvisible()
        finalFare();
        //txtCar12pxfare
        fare('txtCar12px');
        var unitVal = parseFloat(document.getElementById('txtCar12pxHidden' + fareType).value);
        if(unitVal<=0)
        document.getElementById('chkCar2').checked=false;
        }
        else if(dd[0]==2)
        {
        //chkCarvisible()
        finalFare();
        fare('txtCar34px')
        var unitVal = parseFloat(document.getElementById('txtCar34pxHidden' + fareType).value);
        if(unitVal<=0)
        document.getElementById('chkCar3-4').checked=false;
        }
        else if(dd[0]==3)
        {
        //chkCarvisible()
        finalFare();
        fare('txtInn45Px')
        var unitVal = parseFloat(document.getElementById('txtInn45PxHidden' + fareType).value);
        if(unitVal<=0)
        document.getElementById('chkinnova4-5').checked=false;
        }
        else if(dd[0]==4)
        {
        //chkCarvisible()
        finalFare();
        fare('txtInn67Px')
        var unitVal = parseFloat(document.getElementById('txtInn67PxHidden' + fareType).value);
        if(unitVal<=0)
        document.getElementById('chkinnova6-7').checked=false;
        }
        else if(dd[0]==5)
        {
        //chkCarvisible()
        finalFare();
        fare('txtQua46Px')
        var unitVal = parseFloat(document.getElementById('txtQua46PxHidden' + fareType).value);
        if(unitVal<=0)
        {
        document.getElementById('chkqualis4-6').checked=false;
        }
        }
        else if(dd[0]==6)
        {
        //chkCarvisible()
        finalFare();
        fare('txtQua78Px')
        var unitVal = parseFloat(document.getElementById('txtQua78PxHidden' + fareType).value);
        if(unitVal<=0)
        {
        document.getElementById('chkqualis7-8').checked=false;                       
        }
        }
        else if(dd[0]==7)
        {
        //chkCarvisible()
        finalFare();
        fare('txtTem89Px')
        var unitVal = parseFloat(document.getElementById('txtTem89PxHidden' + fareType).value);
        if(unitVal<=0)
        document.getElementById('chktempo8-9').checked=false;
        }
        }*/
    }
}
function GetXmlHttpObject() {
    var objXMLHttp = null
    if (window.XMLHttpRequest) {
        objXMLHttp = new XMLHttpRequest()
    }
    else if (window.ActiveXObject) {
        objXMLHttp = new ActiveXObject("Microsoft.XMLHTTP")
    }
    else if (!xmlhttp && typeof XMLHttpRequest != "undefined") {
        try {
            xmlhttp = new XMLHttpRequest();
        }
        catch (e) {
            xmlhttp = false;
        }
    }
    return objXMLHttp
}
function Validationcheck() {
    var lCarType = document.getElementById('ddlCarType');
    var lSelectedCarType = lCarType.options[lCarType.selectedIndex].value;
    if (parseInt(lSelectedCarType) <= 0) {
        //alert("please select type of car");
        Swal.fire({
            icon: 'warning',
            title: 'Oops...',
            text: 'Please select type of car.',
            confirmButtonColor: '#f2572b'
        });
        document.getElementById('ddlCarType').focus();
        return false;
    }
    var IndexValue = document.getElementById('ddlNoOfPax');
    var paxN0 = IndexValue.options[IndexValue.selectedIndex].value;
    if ((paxN0 == "") || (paxN0 == "0")) {
        alert("please enter the pax no");
        document.getElementById('ddlNoOfPax').focus();
        return false;
    }
    if (document.form1.txtDate.value == "") {
        alert("please select the date");
        document.form1.txtDate.focus();
        return false;
    }
    else {
        if (!submitDate) {
            alert("This is closed date for this tour, choose another or send Request ");
            if (pvPage.toLowerCase() == "agentspecialseasontour.aspx") {
                window.location.href = "../SeatRequestForm.aspx?TourName=" + document.getElementById('etourname').value + "&spl=Y";
            }
            else {
                window.location.href = "SeatRequestForm.aspx?TourName=" + document.getElementById('etourname').value + "&spl=Y";
            }
            return false;
        }
    }
    if (document.form1.txtDate.value != "") {
        var st = document.getElementById("txtDate");
        var dt = new Date();
        var d = dt.getDate();
        var m = dt.getMonth() + 1;
        var y = dt.getFullYear();
        dt = m + "/" + d + "/" + y;
        var s = new String();
        s = st.value;
        var first = s.indexOf("/", 1);
        var second = s.indexOf("/", first + 1);
        var d1 = new String();
        d1 = s.substr(first + 1, second - first - 1) + "/" + s.substr(0, first) + "/" + s.substr(second + 1, 4);
        var v = Date.parse(d1) - Date.parse(dt);
        var diff = ((v / (24 * 3600 * 1000)));
        if (diff < 2) {
            alert('journey date should be greater by 2 days');
            document.form1.txtDate.focus();
            return false;
        }
    }
    if (document.form1.txtTotalExtrafare.value != "") {
        if (document.form1.chkSingle.checked == false) {
            alert("Please check the single adult in a room");
            return false;
        }
    }
    if (document.form1.ddlTitle.value == "Title") {
        alert("Please select the title");
        document.form1.ddlTitle.focus();
        return false;
    }
    if (Trim(document.form1.txtFName.value) == "") {
        alert("Please enter the name");
        document.form1.txtFName.focus();
        return false;
    }

    //New Addded for SPL_Agent
    debugger;
    if (Trim(document.form1.ddlNationality.value) == "" || document.form1.ddlNationality.value == "0") {
        alert("Please select Nationality.");
        document.form1.ddlNationality.focus();
        return false;
    }
    if (Trim(document.form1.ddlCountry.value) == "" || document.form1.ddlCountry.value == "0") {
        alert("Please select Country.");
        document.form1.ddlCountry.focus();
        return false;
    }
    if (document.form1.ddlState.value == "0" && document.form1.ddlCountry.value == "59") {
        alert('Please Select the State');
        document.form1.ddlState.focus();
        chek = false;
        return false;
    }

    if (document.form1.TxtForeignState.value == "" && document.form1.ddlCountry.value != "59") {
        alert('Please Enter the State');
        document.form1.TxtForeignState.focus();
        chek = false;
        return false;
    }

    if (Trim(document.form1.txtAddress.value) == "") {
        alert("Please fill the address in address field.It is mandatory.");
        document.form1.txtAddress.focus();
        return false;
    }


    if (Trim(document.form1.txtAddress.value) == "") {
        alert("Please enter the address");
        document.form1.txtAddress.focus();
        return false;
    }

    if (Trim(document.form1.txtCity.value) == "" && document.form1.ddlCountry.value == "59") {
        alert("Please select City.");
        document.form1.txtCity.focus();
        chek = false;
        return false;
    }

    if (Trim(document.form1.txtForeignCity.value) == "" && document.form1.ddlCountry.value != "59") {
        alert("Please Enter City.");
        document.form1.txtForeignCity.focus();
        chek = false;
        return false;
    }

    //**************************************************************************************************
    if ((document.form1.txtphone.value == "") && (document.form1.txtMobile.value == "")) {
        alert("Please enter the Mobile/Phone no");
        document.getElementById("txtMobile").focus();
        return false;
    }
    if (document.form1.txtphone.value != "") {
        var k = document.getElementById("txtphone").value;
        if (k.length < 6) {
            alert("Phone No should be 6 digits.");
            document.getElementById("txtphone").focus();
            return false;
        }
    }
    if (document.form1.txtMobile.value != "") {
        var k = document.getElementById("txtMobile").value;
        if (k.length < 10) {
            alert("Mobile No should be 10 digits.");
            document.getElementById("txtMobile").focus();
            return false;
        }
    }
    if (isNaN(document.form1.txtMobile.value) == true) {
        alert("Mobile No should be Numeric.");
        document.getElementById("txtMobile").value = "";
        document.getElementById("txtMobile").focus();
        return false;
    }
    if ((document.getElementById("txtMail").value) == "") {
        alert("Plese fill the e-mail field.It is mandatory.");
        document.getElementById("txtMail").focus();
        return false;
    }
    else {
        if ((document.getElementById("txtMail").value) != "") {
            if (CheckMail(document.getElementById("txtMail").value) == false) {
                //document.getElementById("txtMail").value="";
                document.getElementById("txtMail").focus();
                return false;
            }
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
    //    if ((!document.getElementById('chkNoPick').checked)) 
    //	{  
    //	
    if ((!document.getElementById('RadFlight').checked) && (!document.getElementById('RadTrain').checked) && (!document.getElementById('RadBus').checked)) {
        alert("Please choose the pickUp option");
        return false;
    }
    if ((document.getElementById('RadFlight').checked)) {

        if (document.getElementById("txtpickVehicleNo").value == "Select") {
            alert("Please choose flight type");
            document.getElementById("txtpickVehicleNo").focus();
            return false;
        }
        if ((document.getElementById("ddlPkHrs").value == "") || (document.getElementById("ddlPkMints").value == "")) {
            alert("Please Select the  time.");
            if (document.getElementById("ddlPkMints").value == "")
                document.getElementById("ddlPkMints").focus();
            if (document.getElementById("ddlPkHrs").value == "")
                document.getElementById("ddlPkHrs").focus();
            return false;
        }
        if (Trim(document.getElementById("txtFlightNo").value) == "") {
            alert("Please enter Flight No ");
            document.getElementById("txtFlightNo").focus();
            return false;
        }
    }
    else if ((document.getElementById('RadTrain').checked)) {
        if (Trim(document.getElementById("txtRlyStationName").value) == "") {
            alert("Please enter station name");
            document.getElementById("txtRlyStationName").focus();
            return false;
        }
        if ((document.getElementById("ddlTrainPkHr").value == "") || (document.getElementById("ddlTrainPkMin").value == "")) {
            alert("Please Select the  time.");
            if (document.getElementById("ddlTrainPkMin").value == "")
                document.getElementById("ddlTrainPkMin").focus();
            if (document.getElementById("ddlTrainPkHr").value == "")
                document.getElementById("ddlTrainPkHr").focus();
            return false;
        }
        if (Trim(document.getElementById("txtTrainNo").value) == "") {
            alert("Please enter Train No ");
            document.getElementById("txtTrainNo").focus();
            return false;
        }
    }
    else if ((document.getElementById('RadBus').checked)) {
        if (Trim(document.getElementById("txtAddr").value) == "") {
            alert("Please enter Pickup address");
            document.getElementById("txtAddr").focus();
            return false;
        }
        if (Trim(document.getElementById("txtStreet").value) == "") {
            alert("Please enter Street");
            document.getElementById("txtStreet").focus();
            return false;
        }
    }
    //}
    if (document.getElementById("rdoNetBanking").checked == false && document.getElementById("rdoCC").checked == false /*&& document.getElementById("rdoamex").checked == false*/) {

        alert("please choose the payment option");
        return false;

        /*var RadioButtonAmex = document.getElementById("rdoamex");
        if ((RadioButtonAmex != null) && (RadioButtonAmex.checked == false))
        {
        
        alert("please choose the payment option");
        return false;
        }
        else if (document.getElementById("rdoNetBanking").checked == false && document.getElementById("rdoCC").checked == false && document.getElementById("rdoamex").checked == false)
        {
        alert("please choose the payment option");
        return false;
        }
        if (document.getElementById("rdoNetBanking").checked == false && document.getElementById("rdoCC").checked == false && document.getElementById("rdoamex").checked == false) {
        alert("please choose the payment option");
        return false;
        }*/
    }
    if (document.form1.chkTrue.checked == false) {
        alert("before submit you should agree with terms and conditions");
        return false;
    }
    document.getElementById('Submit').style.display = 'none';
    return true;
}
function keyboardlock() {
    return false;
}
//function chkNumeric()
//    {		
//    
//	    if(event.shiftKey) return false;
//	    if((event.keyCode<48 || event.keyCode>57) && event.keyCode != 8 && event.keyCode != 37 && event.keyCode != 38 && event.keyCode != 39 && event.keyCode != 40 && event.keyCode != 46 && event.keyCode != 13 && event.keyCode != 116 && event.keyCode != 16 && (event.keyCode <96 || event.keyCode > 105) && event.keyCode != 9 && event.keyCode != 110) event.returnValue = false
//    }
function chkNumeric(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;
    return true;
}
function ISMeal() {
    if ((xmlHttp.readyState == 4 || xmlHttp.readyState == "complete")) {
        var valResponse = xmlHttp.responseText;
        //alert(valResponse);
        document.getElementById('ddlSubCategory').selectedIndex = 0;
        if (valResponse == "0") {
            document.getElementById("ddlSubCategory").disabled = true;
        }
        else if (valResponse == "1") {
            document.getElementById("ddlSubCategory").disabled = false;
        }
    }
}
function IScheckMeal(val) {
    xmlHttp = GetXmlHttpObject();
    if (xmlHttp == null) {
        alert("Browser does not support HTTP Request")
        return
    }
    var url = "../test1.aspx?spljdate=" + val;
    url = url + "&sid=" + Math.random();
    url = url + "&tourid=" + document.getElementById("tid").value;
    url = url + "&isMeal=1";
    xmlHttp.onreadystatechange = ISMeal;
    xmlHttp.open("GET", url, true)
    xmlHttp.send(null);
}
function checkDate(val) {

    xmlHttp = GetXmlHttpObject();
    if (xmlHttp == null) {
        alert("Browser does not support HTTP Request")
        return
    }
    var url = "test1.aspx?spljdate=" + val;
    if (pvPage.toLowerCase() == "agentspecialseasontour.aspx") {
        url = "../" + url;
    }
    url = url + "&sid=" + Math.random();
    url = url + "&tourid=" + document.getElementById("tid").value;
    xmlHttp.onreadystatechange = chekDD;
    xmlHttp.open("GET", url, true)
    xmlHttp.send(null);
}
var submitDate = true;
function chekDD() {
    if ((xmlHttp.readyState == 4 || xmlHttp.readyState == "complete")) {
        var valResponse = xmlHttp.responseText;
        if (valResponse == "0") {
            submitDate = false;
            alert('This is closed date for this tour, choose another or send Request');
            if (pvPage.toLowerCase() == "agentspecialseasontour.aspx") {
                window.location.href = "../SeatRequestForm.aspx?TourName=" + document.getElementById('etourname').value + "&spl=Y";
            }
            else {
                window.location.href = "SeatRequestForm.aspx?TourName=" + document.getElementById('etourname').value + "&spl=Y";
            }
        }
        else {
            IScheckMeal(document.getElementById("txtDate").value);
            submitDate = true;
        }
    }
}