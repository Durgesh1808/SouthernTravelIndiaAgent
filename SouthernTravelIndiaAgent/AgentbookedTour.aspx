<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgentbookedTour.aspx.cs" Inherits="SouthernTravelIndiaAgent.AgentbookedTour" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="uc1" TagName="AgentFooter" Src="UserControls/UcAgentFooter.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AgentHeader" Src="UserControls/UcAgentHeader.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 Transitional//EN" "http://www.w3.org/tr/xhtml1/Dtd/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Southern India Travel,South India Travel Packages,Travel Packages to South India
    </title>
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <meta content="Southern India Travel - South India Travel guides offering southern india travel, south india travel packages, travel packages to south india, travel holidays package to south india, south india travel, southern india travel packages for south india, southern india travel packages, travel package for south india, south india pilgrimage travel package."
        name="Description" />
    <meta content="southern india travel, south india travel packages, travel packages to south india, travel holidays package to south india, south india travel, southern india travel packages for south india, southern india travel packages, travel package for south india, south india pilgrimage travel package, south india beaches travel packages, south india holiday travel packages, holiday travel package to south india, southern india package travel, south india tourism, tourism in south india, holidays travel in southern india, kerala backwater travel packages in india, north india tour packages, north india tours, tours to north india, tourism in north india, golden triangle tours, kathamandu tours, kashmir tour package, chennai tours, delhi tours, hyderabad tours, pilgrimage tours in india, kerala backwater tours, southern travels india, southerntravelsindia, Sirez"
        name="Keywords" />
    <meta content="index,follow" name="robots" />
    <meta content="Designed  www.Sirez.com" name="Author" />
    <meta content="MSHTML 6.00.2900.2180" name="GENERATOR" />
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1" />
    <meta name="CODE_LANGUAGE" content="C#" />
    <meta name="vs_defaultClientscript" content="Javascript" />
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
    <link href="../Assets/css/style.css" rel="stylesheet" type="text/css" />
    <link href="../Assets/css/stylesheet.css" type="text/css" rel="stylesheet" />
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

    <script language="javascript" src="../Assets/js/MyScript.js" type="text/javascript"></script>

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
            if (document.Form1.emailid.value == "") {
                alert("Enter your Email-ID or Mobile No");
                document.Form1.emailid.focus();
                return false;
            }
            else {
                if (isNaN(document.Form1.emailid.value) == true) {
                    if ((CheckMail(document.Form1.emailid.value)) == false) {
                        document.Form1.emailid.value = "";
                        document.Form1.emailid.focus();
                        return false;
                    }
                    else {
                        document.Form1.type.value = "email";
                    }
                }
                else {
                    var a = document.Form1.emailid.value;
                    if ((a.length < 10) | (a.length > 11)) {
                        alert("Invalid Mobile No")
                        document.Form1.emailid.value = "";
                        document.Form1.emailid.focus();
                        return false;
                    }
                    else {
                        document.Form1.type.value = "Mobile";
                    }
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
            debugger

            // if (Trim(document.Form1.ddlState.value) == "" || document.Form1.ddlState.value == "0") {
            // alert("Please select State.");
            // document.Form1.ddlState.focus();
            // return false;
            // }

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

        function checkout() {
            location.href = "logIn.aspx?orderid=" + document.getElementById("orderid").value
        }
        function checkonsubmit() {
            if (document.getElementById("ddlTour").value == "0") {
                alert("Please choose a tour.");
                document.getElementById("ddlTour").focus();
                return false
            }
        }
        function chkNumeric() {
            if (event.shiftKey) return false;
            if ((event.keyCode < 48 || event.keyCode > 57) && event.keyCode != 8 && event.keyCode != 37 && event.keyCode != 38 && event.keyCode != 39 && event.keyCode != 40 && event.keyCode != 46 && event.keyCode != 13 && event.keyCode != 116 && event.keyCode != 16 && (event.keyCode < 96 || event.keyCode > 105) && event.keyCode != 9 && event.keyCode != 110) event.returnValue = false
        }
        function changeSex(num) {
            var SexCtl = document.getElementsByName('Radio' + num);
            var TitleCtl = document.getElementById('Title' + num);
            if (TitleCtl.selectedIndex == 2 || TitleCtl.selectedIndex == 1 || TitleCtl.selectedIndex == 5) {
                SexCtl[0].checked = true;
            }
            else if ((TitleCtl.selectedIndex == 3) || (TitleCtl.selectedIndex == 4)) {
                SexCtl[1].checked = true;  //check female
            }
            else {
                SexCtl[1].checked = false;
                SexCtl[0].checked = false;
            }
        }
        function Left(str, n) {
            if (n <= 0)
                return "";
            else if (n >= String(str).length)
                return str;
            else
                return String(str).substring(0, n);
        }

        function validateruntime() {
            var chekSex = false;
            var chek = true;
            var i, len;
            len = document.Form1.length;
            var nchilds = 0;
            for (i = 1; i < len; i++) {
                var chk = "";
                if (document.Form1.elements[i].type == "text") {
                    var Cname = document.Form1.elements[i].name;
                    if ((Cname != "txtAddress") && (Cname != "txtName") && (Cname != "txtCity") && (Cname != "TxtLName") && (Cname != "txtPhone") && (Cname != "txtMobile") && (Cname != "txtMail") && (Cname != "txtDOB")) {
                        if (Left(Cname, 6) == "txtONa") {
                            if (document.Form1.elements[i].value == "") {

                                alert("Passenger's name should not be null.");
                                document.Form1.elements[i].focus();
                                chek = false;
                                return false;
                            }

                            if (validateOnlyNumber1(parseInt(document.Form1.elements[i].value)) == true) {
                                alert("Passenger's name should not be numeric.");
                                document.Form1.elements[i].value = "";
                                document.Form1.elements[i].focus();
                                chek = false;
                                return false;
                            }
                            if (Trim(document.Form1.elements[i].value) == "") {
                                alert("Please enter Proper Passenger Name");
                                document.Form1.elements[i].value = "";
                                document.Form1.elements[i].focus();
                                chek = false;
                                return false;
                            }
                        }

                        if (Left(Cname, 6) == "txtAge") {
                            if (document.Form1.elements[i].value == "") {
                                alert('Age field must have any value. It is mandatory.');
                                document.Form1.elements[i].focus();
                                chek = false;
                                return false;
                            }

                            if (document.Form1.elements[i].value == 0) {
                                alert('Age should not be zero.');
                                document.Form1.elements[i].value = ""
                                document.Form1.elements[i].focus();
                                chek = false;
                                return false;
                            }
                            if (validateOnlyNumber1(parseInt(document.Form1.elements[i].value)) == false) {
                                alert("Age value should be numeric.");
                                document.Form1.elements[i].value = "";
                                document.Form1.elements[i].focus();
                                chek = false;
                                return false;
                            }
                            if ((parseInt(document.Form1.elements[i].value) > 0) && (parseInt(document.Form1.elements[i].value) < 12)) {
                                nchilds = nchilds + 1;
                            }
                        }
                    }
                }

                if (document.Form1.elements[i].type == "select-one") {


                    var Cname2 = document.Form1.elements[i].name;
                    if (Left(Cname2, 13) == "contact_title") {

                        if (document.Form1.elements[i].value == "") {
                            alert("Select any title from focused field.");
                            document.Form1.elements[i].focus();
                            chek = false;
                            return false;
                        }
                    }
                }
                /*if(document.Form1.elements[i].type=="radio")
                {
                alert(document.Form1.elements[i].checked);
				  
					var Cname2=document.Form1.elements[i].name;	
                if (Left(Cname2,5)=="Radio")
                {
                if(document.Form1.elements[i].checked==false)
                {
                var Cname3=document.Form1.elements[i].id;
    					      
                if( chekSex==false && (Left(Cname3,4)=='RadM' || Left(Cname3,4)=='RadF'))
                {
                //alert('+++');
                if(document.Form1.elements[i].checked==false)
                {
                alert("Please select gender.");
                chek = false;
                return false;
                }
                else
                {
                chekSex=true;
                }
                }
                if( Left(Cname3,4)=='RadF')
                {
                //alert('---');
                if(document.Form1.elements[i].checked==false)
                {
                alert("Please select gender.");
                chek = false;
                return false;
                }
                }
                }   
                }
                }	*/
            }

            if (parseInt(document.getElementById("Child1").value) > nchilds) {
                alert("Please Enter the Child's Age less than or equal to 11 years");
                chek = false;
                return false;
            }
            else if (parseInt(document.getElementById("Child1").value) < nchilds) {
                alert("Please Enter Adults Age Greater than 11 years");
                chek = false;
                return false;
            }
            if (chek) {
                document.getElementById('Submit1').style.display = 'none';
            }
            return chek;

        }
        function CheckOnlyCharacter() {
            var kk
            kk = event.keyCode
            //alert(kk);
            if ((kk >= 65 && kk <= 90) || kk == 32 || kk == 8 || kk == 9 || kk == 127 || kk == 16 || kk == 20 || kk == 46) {
                return true;
            }
            alert("Please enter characters only.");
            return false;
        }
        function GSTHideUnhide(val) {
            if (val == "yes") {
                document.getElementById('divGSTDetails').style.display = "";
            }
            else {
                document.getElementById('divGSTDetails').style.display = "none";
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

    <script>
        function clearCitylist() {
            document.getElementById('txtCity').value = "";
            document.getElementById('txtPincode').value = "";
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

</head>
<body>
    <form id="Form1" method="post" runat="server">
    <asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>
    <input type="hidden" id="type" value="" runat="server" />
    <input type="hidden" id="Child1" runat="server" />
    <input type="hidden" id="tmpEnValue" runat="server" />
    <table border="0" cellspacing="0" cellpadding="0" width="100%">
        <tr>
            <td>
                <uc1:AgentHeader ID="agentHeader" runat="server" />
            </td>
        </tr>
    </table>
    <br />
    <table width="96%" align="center" cellspacing="0" cellpadding="0" border="0" style="background-color: #dcdcdc">
        <tr>
            <td align="left">
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
            <td width="8" bgcolor="#E7E7E7">
                &nbsp;
            </td>
            <td>
                <table border="0" cellspacing="1" cellpadding="0" width="100%">
                    <tr>
                        <td align="center" valign="top">
                            <table id="Table2" cellspacing="0" bgcolor="#5aa1ea" cellpadding="0" border="0" width="100%">
                                <tr>
                                    <td class="cgi1" align="left">
                                        Booking Date:&nbsp;&nbsp;
                                        <asp:Label ID="lblbookingdate" runat="server" CssClass="heads cgi1"></asp:Label>
                                    </td>
                                    <td class="cgi1" id="adult">
                                        Adults:&nbsp;&nbsp;
                                        <asp:Label ID="lblNoofAdults" runat="server" CssClass="heads cgi1"></asp:Label>
                                    </td>
                                    <td class="cgi1" id="child">
                                        Children:&nbsp;&nbsp;
                                        <asp:Label ID="lblNoofchild" runat="server" CssClass="heads cgi1"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <table id="table4" align="center" width="100%" border="0">
                                <tr>
                                    <td colspan="2" style="background-color: #ffffff">
                                        <asp:DataGrid ID="dgtourdt" runat="server" CellSpacing="0" AutoGenerateColumns="False"
                                            CssClass="hlinks " CellPadding="0" BorderWidth="0" DataKeyField="Rowid" ShowFooter="false"
                                            OnDeleteCommand="dgtourdt_DeleteCommand" OnItemDataBound="dgtourdt_ItemDataBound"
                                            OnSelectedIndexChanged="dgtourdt_SelectedIndexChanged" Width="100%" OnUpdateCommand="DoUpdate"
                                            OnCancelCommand="DoCancel" OnEditCommand="DoEdit" BackColor="Silver" GridLines="Horizontal">
                                            <SelectedItemStyle Font-Bold="False" ForeColor="#F5C65B" BackColor="White"></SelectedItemStyle>
                                            <ItemStyle CssClass="hlinks" BackColor="White"></ItemStyle>
                                            <HeaderStyle Font-Bold="True" CssClass="verdana11bk"></HeaderStyle>
                                            <FooterStyle ForeColor="Maroon" CssClass="heads" Font-Bold="True"></FooterStyle>
                                            <Columns>
                                                <asp:TemplateColumn Visible="False">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkdelete" runat="server"></asp:CheckBox>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="TourName" ReadOnly="True" HeaderText="Tour Name">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>
                                                <asp:TemplateColumn HeaderText="Journey Date">
                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldoj" runat="server" DataTextFormatString="{0:d}" Text='<%#DataBinder.Eval(Container.DataItem,"doj") %>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="cmbdoj" runat="server" DataTextField="doj" DataSource='<%#TempDataView%>'
                                                            DataTextFormatString="{0:d}">
                                                        </asp:DropDownList>
                                                    </EditItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="ReturnDate" ReadOnly="True" HeaderText="Return Date"
                                                    DataFormatString="{0:d}">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn Visible="False" DataField="dob" HeaderText="Booking Date" DataFormatString="{0:d}">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="departtime" ReadOnly="True" HeaderText="Departure Time">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="BusEnvType" ReadOnly="True" HeaderText="Bus Type">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="noofadults" HeaderText="No. of Adults" Visible="False">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="noofchild" HeaderText="No. of Children" Visible="False">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="totalamount" ReadOnly="True" HeaderText="Total Amount"
                                                    DataFormatString="{0:n}">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>
                                                <asp:EditCommandColumn UpdateText="Update" CancelText="Cancel" EditText="&lt;img src=Assets/images/edit.gif border=0 alt=&quot;Changing date here will get you seats on a Random basis. To check the chart for another date, Please go to the Home page&quot;&gt;">
                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:EditCommandColumn>
                                                <asp:ButtonColumn Text="&lt;img src=Assets/images/delete1.gif border=0&gt;" CommandName="Delete">
                                                    <ItemStyle Font-Bold="True" HorizontalAlign="Center" />
                                                </asp:ButtonColumn>
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Center" ForeColor="#330099" BackColor="#FFFFCC"></PagerStyle>
                                        </asp:DataGrid>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="cgi2" style="background-color: #9bc7f4; height: 23px;" align="right" colspan="2">
                                        <b>Grand Total:</b> <b>
                                            <%=GrandTotal%>
                                        </b>
                                    </td>
                                </tr>
                                <tr style="text-align: right">
                                    <td style="background-color: #ffffff; height: 23px;" colspan="2" align="right">
                                        <strong class="cgi2">*Customer Email Id / Mobile No:</strong>&nbsp;&nbsp;<input class="hlinks"
                                            id="emailid" type="text" size="25" name="emailid" maxlength="145" runat="server" />
                                        <asp:Button CssClass="btn1" ID="CheckSubmit" runat="server" Text="Continue Booking"
                                            OnClick="CheckSubmit_Click"></asp:Button>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="left" class="hlinks" style="background-color: #9bc7f4">
                                        * Ticket booking should be confirmed within 5 minutes, otherwise session will expire
                                        and the booking process will be reset.
                                    </td>
                                </tr>
                            </table>
                            <table id="Table1" cellspacing="0" cellpadding="0" width="100%" style="background-color: #9bc7f4"
                                border="0">
                                <tr>
                                    <td align="right" class="cgi2" style="background-color: #ffffff; height: 22px;">
                                        <b>Book Another Tour Package:</b>
                                        <asp:DropDownList CssClass="hlinks" ID="ddlTour" runat="server" Font-Size="8pt" OnSelectedIndexChanged="ddlTour_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:Button CssClass="btn1" ID="btngo" runat="server" Text="Book Another Tour" OnClick="btngo_Click1">
                                        </asp:Button>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" valign="top" bgcolor="#ffffff">
                            <%if ((IsPostBack) && (emailid.Value != ""))
                              {%>
                            <table cellpadding="1" cellspacing="1" border="1" bgcolor="#ffffff" width="70%">
                                <tr>
                                    <td>
                                        <table id="Table7" cellspacing="1" cellpadding="1" border="0" bgcolor="#5aa1ea" width="100%">
                                            <tr>
                                                <td align="left" colspan="2" class="cgi1">
                                                    Note * (Indicate Required Field)
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" colspan="2">
                                                    <p class="cgi1">
                                                        Person filling up this form must be of above 18 years of age and &nbsp;should have
                                                        any identification proof.</p>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table id="Table3" style="border-color: #ffffff; height: 25px" cellspacing="1" cellpadding="1"
                                                        width="100%" border="0">
                                                        <tr>
                                                            <td valign="middle" align="left" colspan="2">
                                                                <div align="center" class="cgi1">
                                                                    <b>CUSTOMER'S DETAILS</b>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <div style="background-color: #fff; width: 100%;">
                                            <div style="background-color: Yellow; width: 70%; margin: auto;">
                                                <div style="width: 100%; text-align: center; color: red; padding: 5px 5px 5px 5px;">
                                                    <strong>** Please select "State" carefully. State will not change after booking of ticket.</strong>
                                                </div>
                                            </div>
                                        </div>
                                        <table id="Table41" bgcolor="#ffffff" border="0" cellpadding="5" cellspacing="0"
                                            class="hlinks" width="100%">
                                            <tr>
                                                <td align="left" class="style3">
                                                    &nbsp;First Name*
                                                </td>
                                                <td align="left">
                                                    <input id="txtName" runat="server" maxlength="35" name="txtName" type="text" />
                                                </td>
                                                <td align="left" class="style3">
                                                    &nbsp;Nationality*
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList runat="server" ID="ddlNationality" class="sel" Width="150px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td bgcolor="#cccccc" colspan="4">
                                                </td>
                                            </tr>
                                            <%--<tr>
                                                <td align="left" class="style3">
                                                    &nbsp;Phone no.*
                                                </td>
                                                <td align="left" class="hlinks">
                                                    <input id="txtPhoneCountryCode" runat="server" maxlength="3" name="txtPhoneCountryCode"
                                                        onkeydown="return chkNumeric();" size="2" style="width: 21px" title="Put Country code"
                                                        type="text" />
                                                    <input id="txtPhone" runat="server" maxlength="10" name="txtPhone" onkeydown="return chkNumeric();"
                                                        size="15" type="text" />
                                                </td>
                                                <td>
                                                    <div align="left">
                                                        <span class="style3">&nbsp;State </span>
                                                    </div>
                                                </td>
                                                <td align="left" class="hlinks" style="width: 100px; height: 26px">
                                                    <asp:DropDownList ID="ddlState" runat="server" Width="150px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>--%>
                                            <tr>
                                                <td align="left" class="style3">
                                                    &nbsp;Country*
                                                </td>
                                                <td align="left" class="hlinks">
                                                    <asp:DropDownList ID="ddlCountry" runat="server" Width="150px" class="sel" onchange="OnChangeCountry(this.value);">
                                                    </asp:DropDownList>
                                                    <%--OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged"    AutoPostBack="True" --%>
                                                </td>
                                                <td>
                                                    <div align="left">
                                                        &nbsp;State : <span class="orange">*</span>
                                                    </div>
                                                </td>
                                                <td align="left" class="hlinks" style="width: 100px; height: 26px">
                                                    <%--<asp:DropDownList ID="ddlState" runat="server" Width="150px">
                                                    </asp:DropDownList>--%>
                                                    <asp:DropDownList ID="ddlState" runat="server" Width="150px" onchange="return clearCitylist();">
                                                    </asp:DropDownList>
                                                    <asp:TextBox ID="TxtForeignState" runat="server" Style="display: none"></asp:TextBox>
                                                    <asp:HiddenField ID="hdfForeignState" runat="server" Value="48" />
                                                    <%--AutoPostBack="True"
                                                        OnSelectedIndexChanged="ddlState_SelectedIndexChanged"--%>
                                                    <font color="red"><strong>**</strong></font>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td bgcolor="#cccccc" colspan="4">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    &nbsp;Address : <span class="orange">*</span>
                                                </td>
                                                <td align="left">
                                                    <%--  <asp:TextBox runat="server" ID="TextBox1" MaxLength="150" class="inp" Width="89%" TextMode="MultiLine"></asp:TextBox>--%>
                                                    <textarea id="txtAddress" runat="server" maxlength="150" rows="2" width="89%" name="txtAddress"
                                                        type="text"></textarea>
                                                    <%--<input id="txtAddress" runat="server" maxlength="150" width="89%"  name="txtAddress" type="text" />--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td bgcolor="#cccccc" colspan="4">
                                                </td>
                                            </tr>
                                            <%--<tr>
                                                <td align="left" class="style3">
                                                    &nbsp;EMail
                                                </td>
                                                <td class="hlinks" align="left">
                                                    <input id="txtMail" runat="server" maxlength="145" name="txtMail" type="text" />
                                                </td>
                                                <td align="left">
                                                    <span class="style3">&nbsp;Mobile no.*</span>
                                                </td>
                                                <td align="left" class="hlinks">
                                                    <input id="txtMobile" runat="server" maxlength="10" name="txtMobile" onkeydown="return chkNumeric();"
                                                        size="10" type="text" />
                                                </td>
                                            </tr>--%>
                                            <tr>
                                                <td align="left" class="style3">
                                                    &nbsp;City/District
                                                </td>
                                                <td class="hlinks" align="left">
                                                    <%-- <asp:DropDownList ID="ddlCity" runat="server" Width="150px">
                                                    </asp:DropDownList>--%>
                                                    <asp:TextBox ID="txtCity" runat="server" autocomplete="off" onkeyup="SetContextKey()"></asp:TextBox>
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
                                                <td align="left">
                                                    &nbsp;Pin code.
                                                </td>
                                                <td align="left" class="hlinks">
                                                    <input id="txtPincode" runat="server" maxlength="6" name="txtPincode" onkeydown="return chkNumeric();"
                                                        type="text" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" class="style3">
                                                    <span>&nbsp;Email*</span>
                                                </td>
                                                <td align="left" class="hlinks">
                                                    <input id="txtMail" runat="server" maxlength="145" name="txtMail" type="text" />
                                                </td>
                                                <td align="left">
                                                    <span class="style3">&nbsp;Mobile no.*</span>
                                                </td>
                                                <td align="left" class="hlinks">
                                                    <input id="txtMobile" runat="server" maxlength="10" name="txtMobile" onkeydown="return chkNumeric();"
                                                        size="10" type="text" />
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td align="left" class="style3">
                                                    <span>&nbsp;Aadhar No.</span>
                                                </td>
                                                <td align="left" class="hlinks">
                                                    <input id="txtAadharNo" runat="server" maxlength="20" type="text" onkeydown="return chkNumeric();"/>
                                                </td>
                                                <td align="left">
                                                    <span class="style3">&nbsp;Image Aadhar Card</span>
                                                </td>
                                                <td align="left" class="hlinks">
                                                    <asp:FileUpload ID="fupAadhar" runat="server" />
                                                </td>
                                            </tr>
                                            
                                            
                                            <tr>
                                                <td bgcolor="#cccccc" colspan="4">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" class="style3">
                                                    &nbsp;Phone no.
                                                </td>
                                                <td align="left" class="hlinks">
                                                    <input id="txtPhoneCountryCode" runat="server" maxlength="3" name="txtPhoneCountryCode"
                                                        onkeydown="return chkNumeric();" size="2" style="width: 21px" title="Put Country code"
                                                        type="text" />
                                                    <input id="txtPhone" runat="server" maxlength="10" name="txtPhone" onkeydown="return chkNumeric();"
                                                        size="15" type="text" />
                                                </td>
                                                <td align="left" class="style3">
                                                    <span class="style3">&nbsp;Emergency Contact No.*</span>
                                                </td>
                                                <td class="hlinks" align="left">
                                                    <input id="txtAlternateMobileno" runat="server" maxlength="15" name="txtAlternateMobileno"
                                                        onkeydown="return chkNumeric();" size="10" type="text" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" class="style3">
                                                    <span class="style3">&nbsp;Whether registered under GST :</span>
                                                </td>
                                                <td class="hlinks" align="left">
                                                    <asp:RadioButton ID="rdbIsGSTApplicableYes" GroupName="GST" OnCheckedChanged="OnCheckChanged_rdbIsGSTApplicableYes"
                                                        OnClick="return GSTHideUnhide('yes');" runat="server" Text="Yes" />
                                                    <asp:RadioButton ID="rdbIsGSTApplicableNo" GroupName="GST" OnCheckedChanged="OnCheckChanged_rdbIsGSTApplicableNo"
                                                        OnClick="return GSTHideUnhide('no');" Checked="true" runat="server" Text="No" />
                                                </td>
                                                <td align="left">
                                                    Send Promotions ?
                                                </td>
                                                <td align="left" class="hlinks">
                                                    <asp:CheckBox runat="server" ID="chkPromotions" />
                                                </td>
                                            </tr>
                                            <tr style="display: none;" runat="server" id="divGSTDetails">
                                                <td align="left" class="style3">
                                                    <span class="style3">&nbsp;GSTIN Of Customer :*</span>
                                                </td>
                                                <td class="hlinks" align="left">
                                                    <asp:TextBox title="Customer GSTIN" ID="txtCustomerGSTIN" placeholder="GSTIN Of Customer*"
                                                        type="text" name="txtCustomerGSTIN" runat="server" CssClass="form-control" />
                                                </td>
                                                <td align="left">
                                                    Name of GST Holder :*
                                                </td>
                                                <td align="left" class="hlinks">
                                                    <asp:TextBox ID="txtGstHolderName" placeholder="Name of GST Holder*" type="text"
                                                        name="txtGstHolderName" runat="server" CssClass="form-control" />
                                                </td>
                                            </tr>
                                        </table>
                                        <table id="Table5" cellspacing="0" cellpadding="0" width="100%" bgcolor="#ffffff"
                                            border="0">
                                            <tr bgcolor="#5aa1ea" height="20">
                                                <td colspan="4" class="cgi1" align="center" style="height: 20px">
                                                    <b>PASSENGER'S DETAILS</b>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td bgcolor="#ffffff" colspan="4">
                                                    <table id="Table6" bgcolor="#ffffff" cellspacing="1" cellpadding="0" align="center"
                                                        width="100%" border="0" class="hlinks">
                                                        <%=stbuild.ToString()%>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td bgcolor="#cccccc" colspan="4">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" bgcolor="#ffffff" colspan="4" style="height: 22px">
                                                    <input class="cgi1" style="background-color: #5aa1ea" type="submit" runat="server"
                                                        value="Submit &amp; Pay Now" name="btnOK" onserverclick="btnOK_ServerClick" id="Submit1"
                                                        onclick="return Submit1_onclick()" />
                                                    &nbsp;
                                                    <input class="cgi1" type="reset" style="background-color: #5aa1ea" value="Reset" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <%} %>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="8" bgcolor="#E7E7E7">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="left">
                <img width="8" height="8" src="Assets/images/left_d_corner.gif" alt="" />
            </td>
            <td bgcolor="#e7e7e7">
                <img width="1" height="1" src="Assets/images/trans.gif" alt="" />
            </td>
            <td align="right">
                <img width="8" height="8" src="Assets/images/rgt_d_corner.gif" alt="" />
            </td>
        </tr>
    </table>
    <br />
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td>
                <uc1:AgentFooter ID="Footer1" runat="server" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
