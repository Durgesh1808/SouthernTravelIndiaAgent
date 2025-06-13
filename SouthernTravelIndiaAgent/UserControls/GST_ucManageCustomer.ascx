<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GST_ucManageCustomer.ascx.cs" Inherits="SouthernTravelIndiaAgent.UserControls.GST_ucManageCustomer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script language="javascript" type="text/javascript">



    function CheckMail(str) {
        if (str.search(/^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/) != -1) {
            return true;
        }
        else {
            alert("Invalid E-mail ID");
            return false;
        }
    }
    function trackEnter() {
        if (event.keyCode == 13) {
            chk1();
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

    function chk1() {
        var chek = true;
        if (document.getElementById('<%=emailid.ClientID %>').value == "") {
            alert("Enter your Email-ID or Mobile No");
            document.getElementById('<%=emailid.ClientID %>').focus();
            chek = false;
            return false;
        }
        else {

            if (isNaN(document.getElementById('<%=emailid.ClientID %>').value) == true) {
                if ((CheckMail(document.getElementById('<%=emailid.ClientID %>').value)) == false) {
                    document.getElementById('<%=emailid.ClientID %>').value = "";
                    document.getElementById('<%=emailid.ClientID %>').focus();
                    chek = false;
                    return false;
                }
                else {
                    document.getElementById('<%=type.ClientID %>').value = "email";
                }
            }
            else {
                var a = document.getElementById('<%=emailid.ClientID %>').value;
                if ((a.length < 10) | (a.length > 11)) {
                    alert("Invalid Mobile No")
                    document.getElementById('<%=emailid.ClientID %>').value = "";
                    document.getElementById('<%=emailid.ClientID %>').focus();
                    chek = false;
                    return false;
                }
                else {
                    document.getElementById('<%=type.ClientID %>').value = "Mobile";
                }
            }

        }
        chek = true;
        return chek;
    }
    function validCheck() {
        var chek = true;
        chek = chk1();
        if (chek) {
            if (!document.getElementById('<%= ddlTitle.ClientID%>')) {
                if (document.getElementById('<%= hdValues.ClientID%>').value == "0") {
                    alert('Please click go button.');
                    document.getElementById('<%= hdValues.ClientID%>').focus();
                    chek = false;
                    return false;
                }
            }

            if (document.getElementById('<%= ddlTitle.ClientID%>').value == "Title") {
                alert('Please Select the Title');
                document.getElementById('<%= ddlTitle.ClientID%>').focus();
                chek = false;
                return false;
            }
            if (document.getElementById('<%= txtName.ClientID%>').value == "") {
                alert('Please Enter the Name');
                document.getElementById('<%= txtName.ClientID%>').focus();
                chek = false;
                return false;
            }
            if (document.getElementById('<%= ddlNationality.ClientID%>').value == "0") {
                alert('Please Select the Nationality');
                document.getElementById('<%= ddlNationality.ClientID%>').focus();
                chek = false;
                return false;
            }


            if (document.getElementById('<%= ddlCountry.ClientID%>').value == "0") {
                alert('Please Select the Country');
                document.getElementById('<%= ddlCountry.ClientID%>').focus();
                chek = false;
                return false;
            }
            if (document.getElementById('<%= ddlState.ClientID%>').value == "0" && document.getElementById('<%=ddlCountry.ClientID %>').value == "59") {
                alert('Please Select the State');
                document.getElementById('<%= ddlState.ClientID%>').focus();
                chek = false;
                return false;
            }

            if (document.getElementById('<%= TxtForeignState.ClientID%>').value == "" && document.getElementById('<%=ddlCountry.ClientID %>').value != "59") {
                alert('Please Enter the State');
                document.getElementById('<%= TxtForeignState.ClientID%>').focus();
                chek = false;
                return false;
            }

            if (document.getElementById('<%= txtAddress.ClientID%>').value == "") {
                alert('Please Enter the Address');
                document.getElementById('<%= txtAddress.ClientID%>').focus();
                chek = false;
                return false;
            }
            if (document.getElementById('<%= txtCity.ClientID%>').value == "" && document.getElementById('<%=ddlCountry.ClientID %>').value == "59") {
                alert('Please Enter The City');
                document.getElementById('<%= txtCity.ClientID%>').focus();
                chek = false;
                return false;
            }

            if (document.getElementById('<%= txtForeignCity.ClientID%>').value == "" && document.getElementById('<%=ddlCountry.ClientID %>').value != "59") {
                alert('Please Enter The City');
                document.getElementById('<%= txtForeignCity.ClientID%>').focus();
                chek = false;
                return false;
            }

            if (document.getElementById('<%= txtPinCode.ClientID%>').value == "") {
                alert('Please Enter The PinCode');
                document.getElementById('<%= txtPinCode.ClientID%>').focus();
                chek = false;
                return false;
            }
            if (isNaN(document.getElementById('<%=txtMail.ClientID %>').value) == true) {
                if ((CheckMail(document.getElementById('<%=txtMail.ClientID %>').value)) == false) {
                    document.getElementById('<%=txtMail.ClientID %>').focus();
                    chek = false;
                    return false;
                }
            }
            if (document.getElementById('<%= txtMobile.ClientID%>').value == "") {
                alert('Please Enter The Mobile');
                document.getElementById('<%= txtMobile.ClientID%>').focus();
                chek = false;
                return false;
            }

            if (document.getElementById('<%= txtMobile.ClientID%>').value != "") {
                if (document.getElementById('<%= txtMobile.ClientID%>').value.length < 10) {
                    alert(" Your Mobile Number must be 10 digits");
                    document.getElementById('<%= txtMobile.ClientID%>').focus();
                    return false;
                }
            }
            if (document.getElementById('<%= txtAlternateMobileNo.ClientID%>').value == "") {
                alert('Please Enter The Emergency Contact No');
                document.getElementById('<%= txtAlternateMobileNo.ClientID%>').focus();
                chek = false; 
                return false;
            }

            var ReqOccupation = '<%= fldIsRequierdOccupation %>';
            if (ReqOccupation == 'True') {
                if (document.getElementById('<%= ddlOccupation.ClientID%>').value == "0") {
                    alert('Please select your occupation');
                    document.getElementById('<%= ddlOccupation.ClientID%>').focus();
                    chek = false;
                    return false;
                }
                if (document.getElementById('<%= ddlOccupation.ClientID%>').value == "-1") { // other has been selected
                    if (document.getElementById('<%= txtOccupation.ClientID%>').value == "") {
                        alert('Please enter your occupation deatails');
                        document.getElementById('<%= txtOccupation.ClientID%>').focus();
                        chek = false;
                        return false;
                    }
                }
            }

            var checkPan = '<%= fldIsPanRequired %>';
            if ((checkPan) && checkPan == 'True') {
                if (document.getElementById('<%= txtPanNo.ClientID%>').value != "") {
                    HotelPANNo = document.getElementById("<%= txtPanNo.ClientID %>").value;
                    if (!/([A-Z,a-z]){3}([A,B,C,F,G,H,J,L,P,T,a,b,c,f,g,h,j,l,p,t]){1}([A-Z,a-z]){1}([0-9]){4}([A-Z,a-z]){1}/.test(HotelPANNo)) {
                        alert('Please enter Valid PAN No..');
                        document.getElementById("<%=txtPanNo.ClientID  %>").focus();
                        chek = false;
                        return false;
                    }
                }
                if (document.getElementById('<%= flImage.ClientID%>').value != "") {
                    var array = ['jpg', 'jpeg', 'png'];
                    var xyz = document.getElementById("<%= flImage.ClientID%>");
                    var Extension = xyz.value.substring(xyz.value.lastIndexOf('.') + 1).toLowerCase();
                    if (array.indexOf(Extension) <= -1) {
                        alert("Please Upload Pan Image only png, jpg and jpeg extension flle.");
                        return false;
                    }
                }

                if (document.getElementById('<%= flForm60.ClientID%>')) {
                    if (document.getElementById('<%= flForm60.ClientID%>').value != "") {
                        var array = ['jpg', 'jpeg', 'png'];
                        var xyz = document.getElementById("<%= flForm60.ClientID%>");
                        var Extension = xyz.value.substring(xyz.value.lastIndexOf('.') + 1).toLowerCase();
                        if (array.indexOf(Extension) <= -1) {
                            alert("Please Upload Form60 Page - 1 only  png, jpg and jpeg extension flle.");
                            return false;
                        }
                    }
                }
                if (document.getElementById('<%= flForm60Page2.ClientID%>').value != "") {
                    var array = ['jpg', 'jpeg', 'png'];
                    var xyz = document.getElementById("<%= flForm60Page2.ClientID%>");
                    var Extension = xyz.value.substring(xyz.value.lastIndexOf('.') + 1).toLowerCase();
                    if (array.indexOf(Extension) <= -1) {
                        alert("Please Upload Form60 Page - 2 only  png, jpg and jpeg extension flle.");
                        return false;
                    }
                }
                if (document.getElementById('<%= flForm60Page3.ClientID%>').value != "") {
                    var array = ['jpg', 'jpeg', 'png'];
                    var xyz = document.getElementById("<%= flForm60Page3.ClientID%>");
                    var Extension = xyz.value.substring(xyz.value.lastIndexOf('.') + 1).toLowerCase();
                    if (array.indexOf(Extension) <= -1) {
                        alert("Please Upload Form60 Page - 3 only  png, jpg and jpeg extension flle.");
                        return false;
                    }
                }
            }

            if (document.getElementById('<%= rdbIsGSTApplicableYes.ClientID%>').checked == true) {
                if (document.getElementById('<%= txtCustomerGSTIN.ClientID%>').value == "") {
                    alert('Please Enter GSTIN.');
                    document.getElementById('<%= txtCustomerGSTIN.ClientID%>').focus();
                    chek = false;
                    return false;
                }
                else {
                    var GSTINNO = document.getElementById("<%= txtCustomerGSTIN.ClientID %>").value;
                    //if (!/([A-Z,a-z]){3}([A,B,C,F,G,H,J,L,P,T,a,b,c,f,g,h,j,l,p,t]){1}([A-Z,a-z]){1}([0-9]){4}([A-Z,a-z]){1}/.test(GSTINNO)) {
                    if (!/^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9]{1}Z[0-9A-Z]{1}$/.test(GSTINNO)) {
                        alert('Please enter Valid GSTIN');
                        document.getElementById("<%=txtCustomerGSTIN.ClientID  %>").focus();
                        chek = false;
                        return false;
                    }
                }
                if (document.getElementById('<%= txtGstHolderName.ClientID%>').value == "") {
                    alert('Please Enter GST Holder Name.');
                    document.getElementById('<%= txtGstHolderName.ClientID%>').focus();
                    chek = false;
                    return false;
                }
            }

            var check = '<%= fldIsPassportRequired %>';
            if ((check) && check == 'True') {
                if (document.getElementById('<%= txtMail.ClientID%>').value == "") {
                    alert('Please Enter The Email ID.');
                    document.getElementById('<%= txtMail.ClientID%>').focus();
                    chek = false;
                    return false;
                }

                if (document.getElementById('<%= txtPassportNo.ClientID%>').value == "") {
                    alert('Please Enter The Passport No');
                    document.getElementById('<%= txtPassportNo.ClientID%>').focus();
                    chek = false;
                    return false;
                }
                if (document.getElementById('<%= txtPassportValidity.ClientID%>').value == "") {
                    alert('Please Enter The Passport Validity');
                    document.getElementById('<%= txtPassportValidity.ClientID%>').focus();
                    chek = false;
                    return false;
                }

            }
            check = '<%= fldIsZeroAdvance %>';
            if ((check) && check == 'True') {
                var radiolist = document.getElementById('<%= rbtnCreditBooking.ClientID %>');
                var radio = radiolist.getElementsByTagName("input");
                for (var x = 0; x < radio.length; x++) {
                    if (radio[x].type === "radio" && radio[x].checked) {
                        if (radio[x].value == "1") {
                            if (document.getElementById('<%= txtCreditPeriod.ClientID%>').value == "") {
                                alert('Please enter credit perdiod');
                                document.getElementById('<%= txtCreditPeriod.ClientID%>').focus();
                                chek = false;
                                return false;
                            }
                            if (document.getElementById('<%= txtAdvanceAmount.ClientID%>').value == "") {
                                alert('Please enter Advance Amount (In %).');
                                document.getElementById('<%= txtAdvanceAmount.ClientID%>').focus();
                                chek = false;
                                return false;
                            }
                        }
                    }
                }
                check = '<%= fldIsApproval %>';
                if ((check) && check == 'False') {
                    if (document.getElementById('<%= txtRemarks.ClientID%>').value == "") {
                        alert('Please Enter The Remarks');
                        document.getElementById('<%= txtRemarks.ClientID%>').focus();
                        chek = false;
                        return false;
                    }
                    //                     if (document.getElementById('<%= ddlReferred.ClientID%>').value == "0") {
                    //                         alert('Please Select Referred By.');
                    //                         document.getElementById('<%= ddlReferred.ClientID%>').focus();
                    //                         chek = false;
                    //                         return false;
                    //                     }

                }
            }
        }
        return chek;
    }
    function Check_Length(my_Object) {
        var maxLen = 250;
        if (document.getElementById("<%=txtRemarks.ClientID  %>").value.length >= maxLen) {

            var msg = "Please enter maximum 250 characters allowed.";
            alert(msg);
            document.getElementById('<%=txtRemarks.ClientID  %>').focus();
        }
    }
    function ManageOccupation() {

        var e = document.getElementById("<%= ddlOccupation.ClientID%>");
        var occvalue = e.options[e.selectedIndex].value;

        if (occvalue == '-1') {
            document.getElementById('<%= txtOccupation.ClientID%>').style.display = 'block';
        }
        else {

            document.getElementById("<%= txtOccupation.ClientID%>").style.display = 'none';
        }

    }
    //
    function GSTHideUnhide(val) {
        if (val == "yes") {
            document.getElementById('<%= trGSTDetails.ClientID%>').style.display = "";
            document.getElementById('<%= txtGstHolderName.ClientID%>').value = "";
            document.getElementById('<%= txtCustomerGSTIN.ClientID%>').value = "";
        }
        else {
            document.getElementById('<%= trGSTDetails.ClientID%>').style.display = "none";
            document.getElementById('<%= txtGstHolderName.ClientID%>').value = "";
            document.getElementById('<%= txtCustomerGSTIN.ClientID%>').value = "";
        }
    }    
</script>

<script type="text/javascript">
    window.onload = function () {
        ManageOccupation()
    }

</script>

<link href="Assets/css/style.css" rel="stylesheet" type="text/css" />
<link href="Assets/css/stylesheet.css" type="text/css" rel="stylesheet" />
<link href="css/demos.css" rel="stylesheet
    " type="text/css" />
<%--<link type="text/css" href="css/smoothness/jquery-ui-1.7.1.custom_blue.css" rel="stylesheet" />--%>

<script src="css/Js/jquery-1.3.2.min.js" type="text/javascript"></script>

<script src="css/Js/jquery-ui-1.7.1.custom.min.js" type="text/javascript"></script>

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
        background-image: url(https://www.southerntravelsindia.com/Assets/images/loading1.gif);
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

<script>
    function clearCitylist() {
        document.getElementById('ucManageCustomer1_txtCity').value = "";
        document.getElementById('ucManageCustomer1_txtPinCode').value = "";
        document.getElementById('ucManageCustomer1_hdnCity').value = "";
        document.getElementById('ucManageCustomer1_hdnCityId').value = "";
        document.getElementById('ucManageCustomer1_hdnStateIdBasedOnCity').value = "";
        document.getElementById('ucManageCustomer1_txtCity').style.display = "block";
        document.getElementById('ucManageCustomer1_txtForeignCity').style.display = "none";
        return false;
    }
    function SetContextKey() {
        debugger;
        //alert($get("<%=ddlState.ClientID %>").value);
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

            var stateid = document.getElementById("ucManageCustomer1_ddlState");
            var state = stateid.options[stateid.selectedIndex].value;
            if (state == "" || state == "0" || state == "--Select--" || state == "Select") {
                //alert(document.getElementById(hdnStateIdBasedOnCity).value);
                setSelectedValue(stateid, document.getElementById(hdnStateIdBasedOnCity).value);
            }
        }
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
</script>

<style>
    .Throug
    {
        text-decoration: line-through;
    }
    .DatePickerImage
    {
        position: relative;
        padding-left: 5px;
        padding-top: 7px;
    }
    .fltright
    {
        float: right;
    }
    .inputlabel
    {
        float: left;
        margin-right: 5px;
        font-size: 12px;
    }
    .inputtxt
    {
        float: left;
        margin-right: 5px;
    }
    .inputbtn
    {
        float: left;
        margin-right: 5px;
    }
    #txtMobile0
    {
        width: 301px;
    }
    #txtOccupation
    {
        width: 200px;
    }
</style>

<script type="text/javascript">

    $(function () {
        //debugger;
        var check = "http://southerntravelsindia.com/Assets/images/cal.gif";
        $("#<%=txtPassportValidity.ClientID %>").datepicker({
            numberOfMonths: 2,
            showOn: "button", autoSize: true,
            buttonImage: check,
            buttonImageOnly: true,
            minDate: new Date(),
            closeText: 'Close',
            dateFormat: 'dd/mm/yy', buttonText: 'Choose a Date'
        });
        $('img.ui-datepicker-trigger').css({ 'cursor': 'pointer', "vertical-align": 'top' });
        $('img.ui-datepicker-trigger').addClass('DatePickerImage');
    });

</script>

<style>
    .btnbooknow a
    {
        background-color: #f1572b;
        color: #fff !important;
        padding: 7px 15px;
        display: inline-block;
        border-radius: 4px;
        text-decoration: none !important;
    }
</style>
<table cellpadding="0" cellspacing="0" width="100%">
    <tr style="text-align: right; background-color: <%if (fldIsEndUser) { %> #C7EDFF <%} else { %> White <%} %>;">
        <td valign="middle">
            <div class="fltright">
                <div class="inputlabel">
                    <%--<span class="cgi1" style="text-align: left; float: left; color: Red;"><b>Note</b> *
                (Indicate Required Field)</span><strong class="cgi2">*--%><span style="float: left;
                    vertical-align: middle; text-align: center; padding-top: 9px!important;"> Email
                    Id / Mobile No :</span><%--</strong>--%>&nbsp;&nbsp;</div>
                <div class="inputtxt">
                    <asp:TextBox class="inp" ID="emailid" type="text" size="25" onkeydown="trackEnter();"
                        name="emailid" runat="server" MaxLength="145" Style="margin: 4px 0 0 4px;" />
                    <input type="hidden" id="type" value="" runat="server" class="inp" /></div>
                <div class="inputbtn">
                    <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="../Assets/images/go.png" Style="margin: 4px 0 0 4px;"
                        OnClick="btnSearch_Click" />
                    <asp:HiddenField ID="hdValues" runat="server" Value="0" />
                </div>
            </div>
        </td>
        <%-- <td style="float: left;">
            
             <asp:Button CssClass="btn1" ID="CheckSubmit" runat="server" Text="Continue Booking" 
                OnClick="CheckSubmit_Click"></asp:Button>
        </td>
        <td style="float: left;">
            
        </td>--%>
    </tr>
    <%-- <tr>
        <td align="center">
            <p class="cgi1" style="color: Red;">
                Person filling up this form must be of above 18 years of age and &nbsp;should have
                any identification proof.</p>
        </td>
    </tr>--%>
</table>
<asp:Panel ID="pnlPerDetail" runat="server" Visible="false">
    <table cellpadding="1" cellspacing="1" border="0" bgcolor="#ffffff" width="100%">
        <tr>
            <td>
                <table style="background-color: <%if (fldIsEndUser) { %> #DEF5FF <%} else { %> #5aa1ea <%} %>;"
                    width="100%">
                    <tr>
                        <td>
                            <table id="Table3" style="border-color: #ffffff; height: 25px" cellspacing="1" cellpadding="1"
                                width="100%" border="0">
                                <tr>
                                    <td valign="middle" align="left">
                                        <div align="center" style="color: <%if (fldIsEndUser) { %> #df411a <%} else { %> White <%} %>;">
                                            <b>CUSTOMER'S DETAILS</b>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table id="Table41" bgcolor="#ffffff" border="0" cellpadding="5" cellspacing="1"
                    width="100%">
                    <tr>
                        <td colspan="4" style="background-color: Yellow;" valign="middle">
                            <div style="width: 100%; text-align: center; color: Red;">
                                <strong>** Please select "State" carefully. State will not change after booking of ticket.</strong>
                            </div>
                        </td>
                    </tr>
                    <tr style="background-color: <%if (fldIsEndUser) { %> #C7EDFF <%} else { %> White <%} %>;">
                        <td align="right">
                            &nbsp;Full Name : <span class="orange">*</span>
                        </td>
                        <td align="left" valign="middle">
                            <asp:DropDownList runat="server" ID="ddlTitle" class="sel" Style="width: auto; float: left">
                                <asp:ListItem Text="Title" Value="Title"></asp:ListItem>
                                <asp:ListItem Text="Mr." Value="Mr."></asp:ListItem>
                                <asp:ListItem Text="Mrs." Value="Mrs."></asp:ListItem>
                                <asp:ListItem Text="Miss." Value="Miss."></asp:ListItem>
                                <asp:ListItem Text="Dr." Value="Dr."></asp:ListItem>
                                <asp:ListItem Text="Prof." Value="Prof."></asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;&nbsp;<input id="txtName" runat="server" maxlength="35" name="txtName"
                                type="text" class="inp" style="margin-left: 5px!important; margin-top: 0px!important;" />
                        </td>
                        <td align="right">
                            Nationality : <span class="orange">*</span>
                        </td>
                        <td align="left">
                            <%--<input id="txtAddress" runat="server" maxlength="150" oncopy="return false" ondrag="return false"
                            ondrop="return false" onpaste="return false" name="txtAddress" type="text"   class="inp"/>--%>
                            <asp:DropDownList runat="server" ID="ddlNationality" class="sel">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr style="background-color: <%if (fldIsEndUser) { %> #DEF5FF <%} else { %> White <%} %>;">
                        <td align="right">
                            &nbsp;Country : <span class="orange">*</span>
                        </td>
                        <td align="left">
                            <asp:DropDownList runat="server" ID="ddlCountry" class="sel" onchange="OnChangeCountry(this.value);">
                            </asp:DropDownList>
                        </td>
                        <td align="right">
                            &nbsp;State : <span class="orange">*</span>
                        </td>
                        <td align="left" class="hlinks" style="width: 100px; height: 26px">
                            <asp:DropDownList ID="ddlState" runat="server" Width="150px" class="sel" onchange="return clearCitylist();">
                            </asp:DropDownList>
                            <asp:TextBox ID="TxtForeignState" runat="server" Style="display: none"></asp:TextBox>
                            <asp:HiddenField ID="hdfForeignState" runat="server" Value="48" />
                            &nbsp;&nbsp;<font color="red"><strong>**</strong></font>
                        </td>
                    </tr>
                    <tr style="background-color: <%if (fldIsEndUser) { %> #DEF5FF <%} else { %> White <%} %>;">
                        <td align="right">
                            &nbsp;Address : <span class="orange">*</span>
                        </td>
                        <td align="left" colspan="3">
                            <asp:TextBox runat="server" ID="txtAddress" MaxLength="150" class="inp" Width="92%"
                                TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="background-color: <%if (fldIsEndUser) { %> #C7EDFF <%} else { %> White <%} %>;">
                        <td align="right">
                            City/District : <span class="orange">*</span>
                        </td>
                        <td align="left">
                            <asp:TextBox runat="server" ID="txtCity" MaxLength="50" class="inp" autocomplete="off"
                                onkeyup="SetContextKey()"></asp:TextBox>
                            <asp:TextBox ID="txtForeignCity" runat="server" Style="display: none"></asp:TextBox>
                            <asp:HiddenField ID="hdfForeignCity" runat="server" Value="1630" />
                            <asp:HiddenField ID="hdnCity" runat="server" Value='' />
                            <asp:HiddenField ID="hdnCityId" runat="server" Value='' />
                            <asp:HiddenField ID="hdnStateIdBasedOnCity" runat="server" Value='' />
                            <%--<cc1:AutoCompleteExtender runat="server" ID="AutoCompleteExtender2" TargetControlID="txtCity"
                                ServicePath="~/Branch/GST_BranchBookedtour.aspx" ServiceMethod="GetCity" MinimumPrefixLength="1"
                                EnableCaching="false" CompletionListCssClass="completionList" CompletionListItemCssClass="AutoExtenderList"
                                CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListElementID="divwidth"
                                OnClientPopulating="ShowProcessImage" OnClientPopulated="HideProcessImage" FirstRowSelected="false"
                                OnClientItemSelected="OnClientSelectedCity" UseContextKey="True" />--%>
                            <%--    ServicePath='<% ConfigurationManager.AppSettings["SouthernBasePath"] +"CallCityFromBranch.aspx" %>'--%>
                            <%--ServicePath="http://localhost:54618/Southern_Travels2017/CallCityFromBranch.aspx"--%>
                            <cc1:AutoCompleteExtender runat="server" ID="AutoCompleteExtender2" TargetControlID="txtCity"
                                ServicePath="http://www.southerntravelsindia.com/CallCityFromBranch.aspx"
                                ServiceMethod="GetCity" MinimumPrefixLength="1" EnableCaching="false" CompletionListCssClass="completionList"
                                CompletionListItemCssClass="AutoExtenderList" CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                                CompletionListElementID="divwidth" OnClientPopulating="ShowProcessImage" OnClientPopulated="HideProcessImage"
                                FirstRowSelected="false" OnClientItemSelected="OnClientSelectedCity" UseContextKey="True" />
                        </td>
                        <td align="right">
                            Pin Code : <span class="orange">*</span>
                        </td>
                        <td align="left" class="hlinks" style="width: 100px; height: 26px">
                            <asp:TextBox runat="server" ID="txtPinCode" MaxLength="6" class="inp"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="background-color: <%if (fldIsEndUser) { %> #DEF5FF <%} else { %> White <%} %>;">
                        <td align="right">
                            &nbsp;Phone no. :
                            <%--<span class="orange">*</span>--%>
                        </td>
                        <td align="left">
                            <input id="txtPhoneCountryCode" runat="server" maxlength="3" name="txtPhoneCountryCode"
                                onkeypress="return chkNumeric(event);" size="2" style="width: 21px" title="Put Country code"
                                type="text" class="inp" />
                            <input id="txtPhone" runat="server" maxlength="15" name="txtPhone" onkeypress="return chkNumeric(event);"
                                size="15" type="text" class="inp" />
                        </td>
                        <td align="right">
                            &nbsp;EMail :
                        </td>
                        <td align="left">
                            <input id="txtMail" runat="server" maxlength="145" name="txtMail" type="text" class="inp" />
                        </td>
                    </tr>
                    <tr style="background-color: <%if (fldIsEndUser) { %> #C7EDFF <%} else { %> White <%} %>;">
                        <td align="right">
                            &nbsp;Mobile no. :<span class="orange">*</span>
                        </td>
                        <td align="left">
                            <input id="txtMobile" runat="server" maxlength="10" name="txtMobile" onkeypress="return chkNumeric(event);"
                                size="10" type="text" class="inp" />
                        </td>
                        <td align="right">
                            &nbsp;Emergency Contact No. : <span class="orange">*</span>
                        </td>
                        <td align="left">
                            <input id="txtAlternateMobileNo" runat="server" maxlength="15" name="txtAlternateMobileNo"
                                onkeypress="return chkNumeric(event);" size="10" type="text" class="inp" />
                        </td>
                    </tr>
                    <tr style="background-color: <%if (fldIsEndUser) { %> #C7EDFF <%} else { %> White <%} %>;"
                        id="trOccupation" runat="server" visible="false">
                        <td align="right">
                            Occupation :
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlOccupation" runat="server" class="sel" onchange="javascript:ManageOccupation();">
                            </asp:DropDownList>
                        </td>
                        <td align="right">
                        </td>
                        <td align="left">
                            <input id="txtOccupation" runat="server" class="inp" maxlength="200" name="txtOccupation"
                                oncopy="return false" ondrag="return false" type="text" style="display: none;" />
                        </td>
                    </tr>
                    <tr id="divform60" runat="server" style="display: none;">
                        <td align="right">
                            &nbsp;Upload Form60 : &nbsp;&nbsp;
                        </td>
                        <td align="left">
                            Page - 1 : &nbsp;
                            <asp:FileUpload ID="flForm60" runat="server" /><br />
                            Page - 2 : &nbsp;
                            <asp:FileUpload ID="flForm60Page2" runat="server" /><br />
                            Page - 3 : &nbsp;
                            <asp:FileUpload ID="flForm60Page3" runat="server" />
                        </td>
                    </tr>
                    <tr runat="server" id="trPanDetails" style="background-color: <%if (fldIsEndUser) { %> #C7EDFF <%} else { %> White <%} %>;">
                        <td align="right">
                            &nbsp;PAN No. :
                        </td>
                        <td align="left">
                            <div class="btnbooknow">
                                <asp:TextBox runat="server" ID="txtPanNo" class="inp" MaxLength="15" Width="100px"></asp:TextBox>
                                <%--  <asp:TextBox runat="server" ID="txtPanNo" class="inp" MaxLength="15" onblur="CheckPanNO(this);"
                                    Width="100px"></asp:TextBox>--%>
                                <br />
                                <asp:RegularExpressionValidator runat="server" ValidationGroup="Validation" ID="RegularExpressionValidator2"
                                    Display="Dynamic" ControlToValidate="txtPanNo" ValidationExpression="([A-Z,a-z]){3}([A,B,C,F,G,H,J,L,P,T,a,b,c,f,g,h,j,l,p,t]){1}([A-Z,a-z]){1}([0-9]){4}([A-Z,a-z]){1}"
                                    ErrorMessage="Please enter Valid and 10 digits PAN No." />
                                <asp:Label ID="lblTCS" runat="server" Text="" ForeColor="Red" Style="font-size: 12px;
                                    font-weight: bold;"></asp:Label>
                            </div>
                        </td>
                        <td align="right">
                            &nbsp;Image Pan Card :
                        </td>
                        <td align="left">
                            <asp:FileUpload ID="flImage" runat="server" />
                        </td>
                    </tr>
                     <tr runat="server" id="TrAadharDetails" style="background-color: <%if (fldIsEndUser) { %> #C7EDFF <%} else { %> White <%} %>;">
                        <td align="right">
                            &nbsp;Aadhar No. :
                        </td>
                        <td align="left">
                            <div class="btnbooknow">
                                <asp:TextBox runat="server" ID="txtAadharNo" class="inp" MaxLength="20" Width="100px" onkeypress="return chkNumeric(event);" ></asp:TextBox>
                                <br />
                            </div>
                        </td>
                        <td align="right">
                            &nbsp;Image Aadhar Card :
                        </td>
                        <td align="left">
                            <asp:FileUpload ID="fupAadhar" runat="server" />
                        </td>
                    </tr>
                    <tr runat="server" id="trISGST" style="background-color: <%if (fldIsEndUser) { %> #C7EDFF <%} else { %> White <%} %>;">
                        <td align="right">
                            &nbsp;Whether registered under GST :
                        </td>
                        <td align="left">
                            <asp:RadioButton ID="rdbIsGSTApplicableYes" GroupName="GST" OnClick="return GSTHideUnhide('yes');"
                                runat="server" Text="Yes" />
                            <asp:RadioButton ID="rdbIsGSTApplicableNo" GroupName="GST" OnClick="return GSTHideUnhide('no');"
                                Checked="true" runat="server" Text="No" />
                            <%--<div class="btnbooknow">
                            <asp:TextBox runat="server" ID="TextBox1" class="inp" MaxLength="15"
                                    Width="100px"></asp:TextBox>                              
                                    <br />
                                <asp:RegularExpressionValidator runat="server" ValidationGroup="Validation" ID="RegularExpressionValidator1"
                                    Display="Dynamic" ControlToValidate="txtPanNo" ValidationExpression="([A-Z,a-z]){3}([A,B,C,F,G,H,J,L,P,T,a,b,c,f,g,h,j,l,p,t]){1}([A-Z,a-z]){1}([0-9]){4}([A-Z,a-z]){1}"
                                    ErrorMessage="Please enter Valid and 10 digits PAN No." />
                                <asp:Label ID="Label1" runat="server" Text="" ForeColor="Red" Style="font-size: 12px;
                                    font-weight: bold;"></asp:Label>
                            </div>--%>
                        </td>
                        <td align="right">
                        </td>
                        <td align="left">
                        </td>
                    </tr>
                    <tr runat="server" id="trGSTDetails" style="display: none; background-color: <%if (fldIsEndUser) { %> #C7EDFF <%} else { %> White <%} %>;">
                        <td align="right">
                            &nbsp;GSTIN Of Customer :
                        </td>
                        <td align="left">
                            <%--<div class="btnbooknow">--%>
                            <asp:TextBox runat="server" ID="txtCustomerGSTIN" class="inp" MaxLength="20" Width="169px"></asp:TextBox>
                            <%--<br />
                            <asp:RegularExpressionValidator runat="server" ValidationGroup="Validation" ID="RegularExpressionValidatorGSTIN"
                                Display="Dynamic" ControlToValidate="txtCustomerGSTIN" ValidationExpression="[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9A-Z]{1}Z[0-9A-Z]{1}"
                                ErrorMessage="Please enter Valid and 15 digits GSTIN." />
                            <asp:Label ID="lblGSTIN" runat="server" Text="" ForeColor="Red" Style="font-size: 12px;
                                font-weight: bold;"></asp:Label>--%>
                            <%--</div>--%>
                        </td>
                        <td align="right">
                            &nbsp;Name of GST holder
                        </td>
                        <td align="left">
                            <asp:TextBox runat="server" ID="txtGstHolderName" class="inp" MaxLength="256" Width="169px"></asp:TextBox>
                            <br />
                            <%--<asp:RequiredFieldValidator runat="server" ValidationGroup="Validation" ID="RegularExpressionValidatorGStHolderName"
                                Display="Dynamic" ControlToValidate="txtGstHolderName" ErrorMessage="Please enter GST holder name" />
                            <asp:Label ID="lblGSTHolderName" runat="server" Text="" ForeColor="Red" Style="font-size: 12px;
                                font-weight: bold;"></asp:Label>--%>
                        </td>
                    </tr>
                    <tr runat="server" id="trPassport">
                        <td align="right">
                            &nbsp;Passport No.:
                        </td>
                        <td align="left">
                            <input id="txtPassportNo" runat="server" maxlength="15" name="txtPassportNo" oncopy="return false"
                                size="20"
                                class="inp" type="text" />
                        </td>
                        <td align="right">
                            Passport Valid Upto :
                        </td>
                        <td align="left">
                            <asp:TextBox runat="server" ID="txtPassportValidity" class="inp" Width="125px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="background-color: <%if (fldIsEndUser) { %> #DEF5FF <%} else { %> White <%} %>;">
                        <td align="right">
                            Send Promotions ?
                        </td>
                        <td align="left" colspan="3">
                            <asp:CheckBox runat="server" ID="chkPromotions" />
                        </td>
                    </tr>
                    <tr id="trZeroAdvance" runat="server" style="background-color: <%if (fldIsEndUser) { %> #C7EDFF <%} else { %> White <%} %>;"
                        visible="false">
                        <td align="right">
                            &nbsp;
                        </td>
                        <td align="left">
                            <asp:RadioButtonList ID="rbtnType" runat="server" RepeatColumns="3">
                                <asp:ListItem Text="Personal" Value="P"></asp:ListItem>
                                <asp:ListItem Text="Corporate" Value="C"></asp:ListItem>
                                <asp:ListItem Text="Operator" Value="O"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td align="right">
                            &nbsp;Company Name:
                        </td>
                        <td align="left">
                            <asp:TextBox runat="server" ID="txtCompanyName" class="inp" MaxLength="250" Width="125px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr runat="server" id="trZeroAdvance1" style="background-color: <%if (fldIsEndUser) { %> #C7EDFF <%} else { %> White <%} %>;"
                        visible="false">
                        <td align="right">
                            Allow Credit Booking
                        </td>
                        <td align="left">
                            <asp:RadioButtonList ID="rbtnCreditBooking" runat="server" RepeatColumns="2">
                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td align="right">
                            &nbsp;Credit Period:
                        </td>
                        <td align="left">
                            <asp:TextBox runat="server" ID="txtCreditPeriod" class="inp" Columns="5" MaxLength="3"
                                onkeypress="return chkNumeric(event);"></asp:TextBox>
                        </td>
                    </tr>
                    <tr runat="server" id="trZeroAdvance2" style="background-color: <%if (fldIsEndUser) { %> #C7EDFF <%} else { %> White <%} %>;"
                        visible="false">
                        <td align="right">
                            &nbsp;Advance Amount (In %):
                        </td>
                        <td align="left">
                            <asp:TextBox runat="server" ID="txtAdvanceAmount" class="inp" Columns="5" MaxLength="99"
                                onkeypress="return chkNumeric(event);"></asp:TextBox>
                        </td>
                        <td align="right">
                            Active ?
                        </td>
                        <td align="left">
                            &nbsp;&nbsp;<asp:CheckBox runat="server" ID="chkIsActive" />
                        </td>
                    </tr>
                    <tr runat="server" id="trZeroAdvance3" style="background-color: <%if (fldIsEndUser) { %> #C7EDFF <%} else { %> White <%} %>;"
                        visible="false">
                        <td align="right">
                            Remarks
                        </td>
                        <td align="left">
                            &nbsp;&nbsp;<asp:TextBox ID="txtRemarks" onKeyPress="Check_Length(this);" runat="server"
                                TextMode="MultiLine"></asp:TextBox></td>
                        <td align="right" style="display: none">
                            &nbsp;Referred By:
                        </td>
                        <td align="left" style="display: none">
                            <asp:DropDownList ID="ddlReferred" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hdnRowId" runat="server" />
    <asp:Button ID="Button1" runat="server" Text="Button" Visible="false" OnClick="Button1_Click" />
</asp:Panel>
