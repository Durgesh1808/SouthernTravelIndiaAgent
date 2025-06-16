<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Fixedenquiryform.aspx.cs" Inherits="SouthernTravelIndiaAgent.Fixedenquiryform" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="UserControls/UcHeaderEndUser.ascx" TagName="UCHeaderEndUser" TagPrefix="UCHeader" %>
<%@ Register Src="UserControls/UcFooterEndUser.ascx" TagName="UCFooterEndUser" TagPrefix="UCFooter" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Southern India Travel,South India Travel Packages,Travel Packages to South India
    </title>
    <link rel="shortcut icon" href="Assets/images/favicon.ico" />
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1" />
    <meta name="CODE_LANGUAGE" content="C#" />
    <meta name="vs_defaultClientScript" content="JavaScript" />
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <meta content="Southern India Travel - South India Travel guides offering southern india travel, south india travel packages, travel packages to south india, travel holidays package to south india, south india travel, southern india travel packages for south india, southern india travel packages, travel package for south india, south india pilgrimage travel package."
        name="Description" />
    <meta content="southern india travel, south india travel packages, travel packages to south india, travel holidays package to south india, south india travel, southern india travel packages for south india, southern india travel packages, travel package for south india, south india pilgrimage travel package, south india beaches travel packages, south india holiday travel packages, holiday travel package to south india, southern india package travel, south india tourism, tourism in south india, holidays travel in southern india, kerala backwater travel packages in india, north india tour packages, north india tours, tours to north india, tourism in north india, golden triangle tours, kathamandu tours, kashmir tour package, chennai tours, delhi tours, hyderabad tours, pilgrimage tours in india, kerala backwater tours, southern travels india, southerntravelsindia, Sirez"
        name="Keywords" />
    <meta content="index,follow" name="robots" />
    <meta content="Designed www.Sirez.com" name="Author" />
    <script async src="https://www.googletagmanager.com/gtag/js?id=AW-10777805346"></script>
<script>
    window.dataLayer = window.dataLayer || [];
    function gtag() { dataLayer.push(arguments); }
    gtag('js', new Date());

    gtag('config', 'AW-10777805346');
</script>
<!-- Google tag (gtag.js) -->
<!-- Google tag (gtag.js) -->
<!-- Event snippet for Submit lead form conversion page
In your html page, add the snippet and call gtag_report_conversion when someone clicks on the chosen link or button. -->
<script>
    function gtag_report_conversion(url) {
        var callback = function () {
            if (typeof (url) != 'undefined') {
                window.location = url;
            }
        };
        gtag('event', 'conversion', {
            'send_to': 'AW-10777805346/oBUICP7pjIUYEKKEoZMo',
            'event_callback': callback
        });
        return false;
    }
</script> 
    <script type="text/javascript">

        $(function() {


            $("#txtarrival").datepicker({

                numberOfMonths: 2,
                showOn: "button", autoSize: true,
                buttonImage: "images/date.gif",
                buttonImageOnly: true,
                minDate: new Date(),
                closeText: 'Close',
                dateFormat: 'dd/mm/yy', buttonText: 'Choose a Date'



            });
            $('img.ui-datepicker-trigger').css({ 'cursor': 'pointer', "vertical-align": 'top' });
            $('img.ui-datepicker-trigger').addClass('DatePickerImage');
        });
	
	
    </script>

    <script language="javascript">
        function fnValidation() {

            if (document.getElementById("S_name").value == "") {
                alert("Please enter your name");
                document.getElementById("S_name").value = "";
                document.getElementById("S_name").focus();
                return false;
            }

            if (document.getElementById("ddlAdults").value == "0") {
                alert("Please select the no of pax");
                document.getElementById("ddlAdults").focus();
                return false;
            }
            if (document.getElementById("S_email").value == "") {
                alert("Please enter your email");
                document.getElementById("S_email").value = "";
                document.getElementById("S_email").focus();
                return false;
            }
            if (!echeck(document.getElementById("S_email").value)) {
                document.getElementById("S_email").value = "";
                document.getElementById("S_email").focus();
                return false;
            }
            if (document.getElementById("S_phone").value == "") {
                alert("Please enter your phone number");
                document.getElementById("S_phone").value = "";
                document.getElementById("S_phone").focus();
                return false; chkMail
            }

            if (document.getElementById("S_city").value == "") {
                alert("Please enter your City name");
                document.getElementById("S_city").value = "";
                document.getElementById("S_city").focus();
                return false;
            }
            if (document.getElementById("S_country").value == "") {
                alert("Please enter your country name");
                document.getElementById("S_country").value = "";
                document.getElementById("S_country").focus();
                return false;
            }
            if (Trim(document.getElementById('txtCaptchImage').value) == "") {
                alert("Please enter verification code.");
                document.getElementById('txtCaptchImage').focus();
                return false;
            }
            // ******   Java Script Start here for Email Checking ***********
            function echeck(str) {
                if (str.search(/^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/) != -1) {
                    return true;
                }
                else {
                    alert("Invalid E-mail ID");
                    return false;
                }
            }
            // ******   Java Script end here for Email Checking ***********
        }
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
    </script>

    <script type="text/javascript">

        var _gaq = _gaq || [];
        _gaq.push(['_setAccount', 'UA-4994177-1']);
        _gaq.push(['_trackPageview']);

        (function() {
            var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
            ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
            var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
        })();
 
    </script>
    <!-- End Meta Pixel Code -->
    <script type="text/javascript">
        var $zoho = $zoho || {}; $zoho.salesiq = $zoho.salesiq || { widgetcode: "f9285012553db7ccdec3cf907b30482c1f0e0a2bd9e18f0f2b52a1810adb9374cd30ce7a28be5ad051877c21011ab9a5", values: {}, ready: function () { } }; var d = document; s = d.createElement("script"); s.type = "text/javascript"; s.id = "zsiqscript"; s.defer = true; s.src = "https://salesiq.zoho.com/widget"; t = d.getElementsByTagName("script")[0]; t.parentNode.insertBefore(s, t); d.write("<div id='zsiqwidget'></div>");
    </script>
    <script src="https://cdn.pagesense.io/js/southerntravels/95bf3c0ba74f44f9baed4ddf90896ba3.js"></script>
</head>
<body>
    <form id="Form1" runat="server">
    <header class="posrel innerheader" style="background-image: url(images/banner-enquiry.jpg)">
  
    <UCHeader:UCHeaderEndUser ID="UCHeaderEndUser1" runat="server" />
    
  </header>
    <section class="innersection2">
  <div class="container">
    <div class="row subheadrow">
      <div class="col-md-12 text-center" id="tblEnquiry" runat="server">
        <h1 class="title mrgnbtmh1"><span>Enquiry</span> Form</h1>
        <br />
        <asp:Label ID="lblTour" runat="server"></asp:Label>
      </div>
       <div class="formwrap innerforms" id="Table1" runat="server">
      <div class="row mrgnbtminput">
        <div class="col-md-6">
         	<div class="input-group width100" >
                
               <input id="txtarrival" name="txtarrival" readonly="readonly" type="text" runat="server"
                                        size="9"  placeholder="Journey Date" class="form-control"/>
              </div>
              </div>
       
    	<div class="col-md-6">
        	<div class="input-group width100" >
               
                <input maxlength="48" size="35" name="S_name" runat="server" id="S_name" class="form-control " placeholder="Name*"  />
              </div>
        </div>
        </div>
        <div class="row mrgnbtminput">
        <div class="col-md-6">
         	<div class="input-group width100" >
                
               <asp:DropDownList ID="ddlAdults" runat="server" Width="50%" class="form-control">
                                        <asp:ListItem Text="No of Adults" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                        <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                        <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                        <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                    </asp:DropDownList>
              </div>
              </div>
       
    	<div class="col-md-6">
        	<div class="input-group width100" >
               <asp:DropDownList ID="ddlChilds" runat="server" Width="50%" CssClass="form-control">
                                        <asp:ListItem Text="No of Childs" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                        <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                        <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                        <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                    </asp:DropDownList>
              </div>
        </div>
        </div>
         <div class="row mrgnbtminput">
        <div class="col-md-6">
         	<div class="input-group width100" >
                <input maxlength="48" size="35" name="S_email" runat="server" id="S_email" placeholder="EMail*" class="form-control" />
               
              </div>
              </div>
       
    	<div class="col-md-6">
        	<div class="input-group width100" >
               <input size="35" name="S_phone" runat="server" id="S_phone" onkeypress="javascript:return isNumberKey(event);"
                                        maxlength="11" class="form-control " placeholder="Phone / Mobile no*"/>
               
              </div>
        </div>
        </div>
         <div class="row mrgnbtminput">
        <div class="col-md-6">
         	<div class="input-group width100" >
                
               <input size="35" id="S_fax" runat="server" onkeypress="javascript:return isNumberKey(event);"
                                        maxlength="12" class="form-control " placeholder="Fax no" />
              </div>
              </div>
       
    	<div class="col-md-6">
        	<div class="input-group width100" >
               
                <input size="35" id="S_streetaddress" maxlength="200" runat="server" class="form-control " placeholder="Address" 
                                        style="width: 200px; height: 50px" />
              </div>
        </div>
        </div>
         <div class="row mrgnbtminput">
        <div class="col-md-6">
         	<div class="input-group width100" >
                
               <input maxlength="48" size="35" id="S_city" runat="server" class="form-control " placeholder="City / State*"  />
              </div>
              </div>
       
    	<div class="col-md-6">
        	<div class="input-group width100" >
               
                 <input maxlength="20" size="15" id="S_pin" runat="server" class="form-control " placeholder="Zip / Postal Code" />
              </div>
        </div>
        </div>
         <div class="row mrgnbtminput">
        <div class="col-md-6">
         	<div class="input-group width100" >
                
               <input maxlength="48" size="35" name="S_country" runat="server" id="S_country" class="form-control " placeholder="Country*" />
              </div>
              </div>
       
    	<div class="col-md-6">
        	<div class="input-group width100" >
               
                  <div class="g-recaptcha" runat="server" id="divrecaptcha" ></div>
                  <br />
                                    <asp:Label ID="MessageLabel" runat="server" CssClass="txt" ForeColor="red"></asp:Label>
                                
               
              </div>
        </div>
        </div>
      
   <div class="row mrgnbtminput"><div class="col-md-12">
    <p class="mrgnbtmno">**This helps Southern Travels prevent automated Enquiries. </p>
	<p class="mrgnbtmno">***Please give your Phone no. or Mobile no. or both </p>
    </div></div>
        <div class="row mrgnbtminput">
    
        <div class="col-md-12">
        <div class="btnwrap" align="center">
        <asp:Label ID="lblMsg"  runat="server" Text="" ></asp:Label>
                                    <asp:Button ID="Submit1" class="btn" Text="Submit" runat="Server" OnClick="Submit1_Click" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="ImageButton1" Text="Reset" class="btn" runat="Server" OnClick="ImageButton1_Click" />
                                   
                                     <%--<input type="submit" runat="server" value="Submit Query" name="" id="Submit1" language="javascript"
                                                        onclick="return fnValidation()" onserverclick="Submit1_ServerClick" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;<input type="reset" value="Reset" name="" />--%>
        </div>
        
            	
    </div>
    
  </div>
       
    </div>
    </div>
     </div>
</section>
    <UCFooter:UCFooterEndUser ID="UCFooterEndUser2" runat="server" />
    
    </form>

    <script>
        $('#refresh_captcha').click(function(e) {

            $('#imgCaptcha').attr('src', 'JpegImage.aspx?cache=' + new Date().getTime());
            e.preventDefault();
        });
        (function(i, s, o, g, r, a, m) {
            i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function() {
                (i[r].q = i[r].q || []).push(arguments)
            }, i[r].l = 1 * new Date(); a = s.createElement(o),
  m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
        })(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');

        ga('create', 'UA-4994177-1', 'auto');
        ga('require', 'displayfeatures');
        ga('send', 'pageview');
 
    </script>

</body>
</html>
