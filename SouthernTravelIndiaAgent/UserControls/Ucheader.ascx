<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Ucheader.ascx.cs" Inherits="SouthernTravelIndiaAgent.UserControls.Ucheader" %>



<script src="Assets/js/language.js" type="text/javascript"></script>

<script type="text/javascript">

function f1(lang)
{
location.href="changeLang.aspx?lang="+lang;

}

</script>

<script language="javascript">
     function fnFb() {
          window.open('fb-login.aspx?tofb=y', 'login', 'height=600px, width=900px');
        // window.open('SeasonPage.aspx', 'login', 'height=600px, width=900px');
         return false;
     }
</script>

<script type="text/javascript">
	  	jQuery(window).load(function(){
    jQuery("#slider").nivoSlider({
        effect:"random",
        slices:15,
        boxCols:8,
        boxRows:4,
        animSpeed:500,
        pauseTime:5000,
        startSlide:0,
        directionNav:true,
        controlNav:true,
        controlNavThumbs:false,
        pauseOnHover:true,
        manualAdvance:false
    });
});

		$(window).load(function() {
		
		
		$("#mainSlider").hover(
			function () {
					$(".nivo-prevNav").stop(true, false).animate({left:"0px", opacity:1})
					$(".nivo-nextNav").stop(true, false).animate({left:"569px", opacity:1})
					
			  },
			function () {
					$(".nivo-prevNav").stop(true, false).animate({left:"-10px", opacity:0})
					$(".nivo-nextNav").stop(true, false).animate({left:"579px", opacity:0})
					
			  }
	)
	});
	$(window).load(function() {
	    $("#mainSlider2").hover(
			function() {
			    $(".nivo-prevNav").stop(true, false).animate({ left: "0px", opacity: 1 })
			    $(".nivo-nextNav").stop(true, false).animate({ left: "436px", opacity: 1 })

			},
			function() {
			    $(".nivo-prevNav").stop(true, false).animate({ left: "-10px", opacity: 0 })
			    $(".nivo-nextNav").stop(true, false).animate({ left: "442px", opacity: 0 })

			}
	)



	});
</script>

<script>
$(document).ready(function(){

	// hide #back-top first
	$("#back-top").hide();
	
	// fade in #back-top
	$(function () {
		$(window).scroll(function () {
			if ($(this).scrollTop() > 100) {
				$('#back-top').fadeIn();
			} else {
				$('#back-top').fadeOut();
			}
		});

		// scroll body to 0px on click
		$('#back-top a').click(function () {
			$('body,html').animate({
				scrollTop: 0
			}, 800);
			return false;
		});
	});

});

$(function(){
//alert('Hello');
		$('.ebrochurelink').mouseenter(function(){
			$('.ddmenu').show();
			
		});
		
		$('.ddmenu').mouseleave(function(){
			$('.ddmenu').hide();
			});


});

$(function() {
    //alert('Hello');

    $('.elink').mouseenter(function() {

        $('.ddmenu').show();

    });

    $('.elink').mouseleave(function() {
        $('.ddmenu').hide();
    });


});	
</script>

<link rel="shortcut icon" href="Assets/images/favicon.ico" />
<div id="header"><%--<div id="sidebar">
<a href="http://www.hostedivr.in/cc2/clickcall.php?uid=3378&pid=4823" target="popupwindow"  onclick="window.open('', 'popupwindow', 'scrollbars=no,width=580,height=650');return true"><img src="http://www.hostedivr.in/cc2/Assets/images/buttons/button3.png"  /> </a>
</div>--%>
    <div class="pageWidth" style="position: relative;">
        <div id="logo" itemscope="true" itemtype="http://schema.org/Organization" >
            <a itemprop="url" href="index.aspx">
                <img itemprop="logo" src="Assets/images/logo.webp" width="175" height="90" alt="Southern Travel" /></a></div>
        <div id="headRight">
            <!-- Language Bar start -->
            <div id="langRow">
                <div id="lang">
                    <div id="langL">
                        <img src="Assets/images/ChangeLanguage.gif" alt="Change Language" style="padding-top: 4px;" />
                        |&nbsp;<span class="mR5"><a href='javascript:convertnew("en|en");'><img src="Assets/images/Eng.gif"
                            style="padding-top: 4px;" /></a></span></div>
                    <div id="langR">
                        <langbar class="niceform">
              <div class="floatleft mR5" style="vertical-align:middle;">
                <select size="1" name="international" id="international" onchange="javascript:convertnew(this.value);" style="width:105px;">
                  <option value="-2">International</option>
                  <option value="en|en">English</option>
                  <option value="en|de">German</option>
                  <option value="en|fr">French</option>
                  <option value="en|es">Spanish</option>
                  <option value="en|zh">Chinese</option>
                  <option value="en|nl">Dutch</option>
                  <option value="en|ar">Arabic</option>
                  <option value="en|ja">Japanese</option>
                  <option value="en|ko">Korean</option>
                  <option value="en|ms">Malay</option>
                  <option value="en|ru">Russian</option>
                  <option value="en|tr">Turkish</option>
                  <option value="en|id">Indonesian</option>
                  <option value="en|th">Thai</option>
                  <option value="en|et">Estonian</option>
                </select>
              </div>
              <div class="floatleft mR5">
                <select  name="domestic" runat="server"  id="domestic" size="1"  onchange="javascript:convertnew(this.value);" style="width: 95px;" >
                  <option value="-1">Indian</option>
                  <option value="en|en">English</option>
                  <option value="en|hi">Hindi</option>
                  <option value="en|ta">Tamil</option>
                  <option value="en|te">Telugu</option>
                  <option value="en|kn">Kannad</option>
                  <option value="en|bn">Bangla</option>
                </select>
              </div>
            </langbar>
                    </div>
                </div>
                <div id="phone">
                    <img src="Assets/images/ico_phone.gif" width="16" height="16" alt="phone:" class="vImg" />
                    1800 11 0606 | 011-43532800/60
                </div>
            </div>
            <!-- Language Bar end -->
            <!-- Login link start -->
            <div id="loginRow">
                <div id="trophy">
                    <img src="Assets/images/Award_Trophy.gif" width="14" height="54" alt="Trophy" />
                    <span>Ranked No.1 Domestic Tour Operator </span>
                    <br />
                    by Ministry of Tourism
                </div>
                <%--<div id="hotDeals">
                    <a href="JavaScript:void(0);">
                        <img src="Assets/images/Southern_HotDeal.gif" width="55" height="24" alt="Hot Deals" class="vImg" /></a></div>--%>
                <div id="loginLinks" style="position: relative">
                    <%--<a href="flipbook/BrouchreFlipBook.html"><img src="Assets/images/e-brochure.png"  alt="e brochure"  class="floatright ebrochure"/></a>--%>
                    <div class="elink">
                    <a class="ebrochurelink">
                    <img src="Assets/images/e-brochure.png"  alt="e brochure"  class="floatright ebrochure">
                    <ul class="ddmenu">
                    <li><a href="flipbook/Domestic/index.html" target="_blank">Domestic e-Brochure</a></li>
                    <li><a href="flipbook/International/Index.html" target="_blank">International e-Brochure</a></li>
                    </ul>
                   </a>
                   </div>
                    <span runat="server" id="spnHome" visible="false">Home</span><a href="../index.aspx"
                        runat="server" id="aHome" visible="true">Home</a> |
                    <%if (Session["custrowid"] == null)
                      { %>
                    <a href="../CustomerLogin.aspx?LIN=2" runat="server" id="aAGLogin" visible="true">Agent Login</a><span
                        runat="server" id="spnAGLogin" visible="false">Agent Login</span> | 
                    <a href="../CustomerLogin.aspx?LIN=1" runat="server" id="aEULogin" visible="true">Customer Login</a><span
                        runat="server" id="spnEULogin" visible="false">Customer Login</span> | 
                    <%}%>
                    <%else
                        {%>
                    <a href="../CustomerProfile.aspx?MM=1" runat="server" id="aMyAccount" visible="true">
                        My Account</a><span runat="server" id="spnMyAccount" visible="false">My Account</span>
                    | <a href="Customerlogout.aspx">Logout</a> | 
                    <%}%>
                    <a href="../Contact-us.aspx">Contact Us</a> | 
                    <a href="../Enquiryform.aspx">Enquiry</a> |
                    <%--| <a href="../contact-us.aspx#Customer" runat="server" id="aCustSupport" visible="true">Customer Support</a><span runat="server" id="spnCustSupport" visible="false">Customer Support</span>--%>
                    <%--<%if (Session["custrowid"] == null)
                      { %>
                    <a href="#" onclick="javascript:fnFb();">
                        <img alt="" border="0" src="Assets/images/fblogin.jpg" align="absmiddle" style="display: none;" /></a>
                    <%}%>
                    <%else
                        {%>
                    <a href="#" onclick="javascript:fnFb();">
                        <img alt="" border="0" src="Assets/images/fblogin.jpg" align="absmiddle" style="display: none;" /></a>
                    <%}%>--%>
                </div>
            </div>
            <!-- Login link end -->
            <!-- Navigation start -->
            <div id="nav">
                <ul itemscope="itemscope" itemtype="http://www.schema.org/SiteNavigationElement">
                    <li class="first" itemprop="name"><a itemprop="url" href="../India-Tour-Packages.aspx" runat="server" id="MainMFD">
                        Fixed Departure Tours
                        <div class="coach">
                            (Domestic / seat in coach basis)</div>
                    </a></li>
                    <li class="secondon" itemprop="name"><a itemprop="url" href="" runat="server" id="MainMHP">Holiday Packages
                        <div class="coach">
                            (Indian / Nepal / Bhutan)</div></a>
                        <div class="subNav">
                            <ul>
                                <li><a href="holiday-packages.aspx">India, Nepal &amp; Bhutan</a></li>
                                <li><a href="holiday-packages.aspx">Luxury Train Offers</a></li>
                                <%--<li><a href="International-Packages.aspx">International Holidays</a></li>
                                <li><a href="International-Packages.aspx?Ofr=2">Offer - International Holidays</a></li>
                                <li><a href="JavaScript:void(0);">Cruise Packages</a></li>--%>
                            </ul>
                        </div>
                    </li>
                    <li class="secondon" >
                        <a href="../International-GroupDeparture.aspx" runat="server" id="MainMFLIGHT" style="margin-left:0px;width:110px!important;">International
                            <div style="font-size:9px; position:absolute; top:27px; left:12px;">
                                (Fixed / Group Departures)</div>
                        </a>
                    </li>
                    <li class="secondon" style="">
                        <a href="../International-Packages.aspx" runat="server" id="MainMINT" style="margin-left:0px; width:95px!important;">International<div style="font-size:9px; position:absolute; top:27px; left:12px;">
                                (Customized Holidays)</div></a>
                        <%--<div class="subNav">
                            <ul>
                                <li><a href="../International-Packages.aspx#CustomizeHoliday">Customize Holiday</a></li>
                                <li><a href="../International-Packages.aspx#GroupPackage">Group Holiday</a></li>
                            </ul>
                        </div>--%>
                    </li>
                    <li class="secondon"><a href="" runat="server" id="MainMHTL" style="line-height: 20px">Hotels</a>
                        <div class="subNav">
                            <ul>
                                <li><a href="hotel-southern.aspx?HTLREG=DEL">Hotel Southern, Delhi</a></li>
                                <li><a href="HotelSouthernGrandVijayawada.aspx" >Southern Grand, Vijayawada</a></li>
                                <%--<li><a href="http://hotelsoutherngrand.com/" target="_blank">Southern Grand, Vijayawada</a></li>--%>
                                <li><a href="hotel-southern-jaipur.aspx">Southern Grand, Jaipur</a></li>
                                <li><a href="hotel-southern.aspx?HTLREG=IND">Hotels In India</a></li>
                            </ul>
                        </div>
                    </li>
                    <li class="secondon" itemprop="name"><a itemprop="url" href="../Car-Coach-Rental.aspx?ORG=All" runat="server" id="MainMCR" style="line-height: 13px;">
                        Car/Coach <br />Rental</a> </li>
                    <li itemprop="name" style="display:none;"><%--<a itemprop="url" href="../Domestic-Flights.aspx" runat="server" id="MainMFLIGHT">Flights</a>--%>
                    </li>
                    <li class="secondon" itemprop="name"><a itemprop="url" href="../LFC-Home.aspx" runat="server" id="MainMLLTC" style="line-height: 13px;">LFC/LTC <br />Tour</a> </li>
                    <%--<li class="secondon" class="secondon"><a href="../Contact-us.aspx" runat="server"
                        id="MainMCONTACTUS">Contact Us</a> </li>--%>
                    <li class="secondon" itemprop="name" style="display:none;"><a itemprop="url" href="../Enquiryform.aspx" runat="server" id="MainENQRY">Enquiry</a> </li>
                </ul>
            </div>
            <!-- Navigation end -->
            <!-- Ticker start -->
            <div id="ticker" style=" position:relative;">
                <%--<a href="holiday-packages.aspx"><img src="Assets/images/HeavyDiscount.gif" style="position:absolute; left:0; top:8px;" /></a>--%> <span class="orange">IVR PAYMENT:</span> CALL US ON 011-2875-0606 (Booking over
                Phone, Payment by Credit card)
            </div>
            <%--<div style="width:593px; float:right;" >--%>
            <div style="width:725px; float:right;" >
                <a href="Fixed-Departure-Itinerary-4-Dham_22" style="color: Blue; font-size:14px">
                    <%--<marquee id='scroller' scrollamount='4' direction="left">
                        The temple opening dates of Badrinath , Kedarnath , Yamunotri, Gangotri for the year 2015 are announced. Temples will be opening by the end of April` 2015. Click here to Book Now </marquee>--%>
                </a><%--<br />
                <a href="Holiday-Packages-Itinerary-Rann-Utsav-20142015--Kutch-Darshan_149" style="color: Blue; font-size:14px">
                    <marquee id='scroller' scrollamount='3' direction="left" style="color:Red;">
                        Visit the famous RANN UTSAV 2014 / 2015 between 1st Dec 2014 - 7th March 2015. Click here to Book Now </marquee>
                </a>--%>
            </div>
            <%--Celebrating our 7th National Tourism Award with 10% Discount* on all Domestic Holiday Packages – Online and Offline**--%>
            <!-- Ticker end -->
        </div>
    </div>
    <!--  FAceBook Ret Button -->
    <asp:Button ID="btnFaceBookRet" runat="server" Text="Button" OnClick="btnFaceBookRet_Click" />
</div>
<script>
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

