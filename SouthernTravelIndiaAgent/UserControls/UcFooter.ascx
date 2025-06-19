<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcFooter.ascx.cs" Inherits="SouthernTravelIndiaAgent.UserControls.UcFooter" %>

<%@ Register Src="UCPaymentOptionNetBanking.ascx" TagName="UCPaymentOptionNetBanking"
    TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<div id="footer">
    <div class="pageWidth">
        <div id="footLink">
            <div class="row">
                <ul class="first">
                    <li class="head">Corporate</li>
                    <li><a href="company-profile.aspx">About us</a></li>
                    <li><a href="News-Events.aspx">News &amp; Media</a></li>
                    <%--<li><a href="Testimonials.aspx">Testimonials</a></li>--%>
                    <li><a href="Career.aspx">Careers</a></li>
                    <li><a href="Social-Responsibility.aspx">Plant a Tree Program</a></li>
                    <li><a href="flipbook/BrouchreFlipBook.html" target="_blank">eBrochure</a></li>
                </ul>
                <ul>
                    <li class="head">Product Offerings</li>
                    <li><a href="India-Tour-Packages.aspx">Fixed Departure</a></li>
                    <li><a href="holiday-packages.aspx">Domestic Holidays</a></li>
                    <li><a href="International-Packages.aspx">International Holidays</a></li>
                    <li><a href="hotel-southern.aspx?HTLREG=DEL">Hotel Southern, Delhi</a></li>
                    <li><a href="hotel-southern.aspx?HTLREG=IND">Hotels in India</a></li>
                    <li><a href="Car-Coach-Rental.aspx?ORG=All">Car / Coach Rental</a></li>
                    <%--<li><a href="Domestic-Flights.aspx">Domestic Flights</a></li>
                     <li><a href="JavaScript:void(0);">MICE</a></li>
                    <li><a href="JavaScript:void(0);">B2B</a></li>--%>
                    <li><a href="LFC-Home.aspx">LFC / LTC Tours</a></li>
                    <li><a href="OnlineBalanceClear.aspx">Clear Balance</a></li>
                </ul>
                <ul>
                    <li class="head">About the Site</li>
                    <li><a href="FAQs.aspx">FAQs</a></li>
                    <li><a href="Terms-Conditions.aspx">Terms &amp; Conditions</a></li>
                    <li><a href="PrivacyPolicy.aspx">Privacy Policy</a></li>
                    <li><a href="Terms-Conditions.aspx#CancellationPolicy">Cancellation Policy</a></li>
                    <li><a href="site-map.aspx">Site Map</a></li>
                    <%--<li><a href="JavaScript:void(0);">Payment Security</a></li>
                    <li><a href="http://www.alexa.com/siteinfo/southerntravelsindia.com" target="_blank">Rank</a></li>--%>
                    <br />
                    <li class="head">Payment Modes</li>
                    <li><a href="PaymentModes.aspx#BankTransfer">Bank Transfer</a></li>
                    <li><a href="PaymentModes.aspx#NetBanking">Net Banking</a></li>
                    <li>VISA / MASTER / AMEX Card</li>
                </ul>
                <ul>
                    <li class="head">Contact us</li>
                    <li><a href="contact-us.aspx?CONREG=Corporate">Corporate Office</a></li>
                    <li><a href="contact-us.aspx?CONREG=Branches">Branch Office</a></li>
                    <%--<li><a href="Franchisee.aspx">Franchisee</a></li>--%>
                    <li><a href="contact-us.aspx#Customer">Customer Care</a></li>
                    <li><a href="Feedback.aspx">Feedback Form</a></li>
                    <li><a href="Enquiryform.aspx">Enquiry Form</a></li>
                </ul>
                <ul class="first">
                    <li>
                        <!-- AddThis Button BEGIN -->
                        <div class="row1 btm10">
                            <a href="https://twitter.com/happyholidaying">
                                    <img src="Assets/images/twitter.png" width="16" height="16" align="left" alt="Twitter"
                                        style="margin-right: 2px;" /></a>
                            <a href="http://www.youtube.com/southerntravels">
                                    <img src="Assets/images/youtube.png" width="16" height="16" align="left" alt="Twitter"
                                        style="margin-right: 2px;" /></a>
                        </div>
                        <!-- AddThis Button END -->
                        <div class="row1 btm10">
                            <iframe src="//www.facebook.com/plugins/likebox.php?href=https%3A%2F%2Fwww.facebook.com%2FSouthernTravels&amp;width=250&amp;height=181&amp;colorscheme=light&amp;show_faces=true&amp;border_color&amp;stream=false&amp;header=false"
                                scrolling="no" frameborder="0" style="border: none; overflow: hidden; width: 250px;
                                height: 181px;" allowtransparency="true"></iframe>
                        </div>
                        <div class="rss_fdimg">
                            <div style="text-align: left;" onmouseover="this.className='show';" onmouseout="this.className='hide';"
                                class="hide">
                                <a href="#">
                                    <img src="Assets/images/rss_feeds.gif" width="16" height="16" align="left" alt="RSS Feeds"
                                        style="margin-right: 2px;" /></a>
                                <ul>
                                    <li><a href="rss-international-tours.xml">International Packages </a></li>
                                    <li><a href="rss-fixed-departure-tours.xml">Fixed Departure Tours</a></li>
                                    <li><a href="rss-holiday-package-tours.xml">Holiday Package Tours</a></li>
                                </ul>
                            </div>
                        </div>
                        <div class="addthis_toolbox addthis_default_style " style="padding-bottom: 10px;">
                            <a class="addthis_button_preferred_1"></a><a class="addthis_button_preferred_2">
                            </a><a class="addthis_button_preferred_3"></a><a class="addthis_button_preferred_4">
                            </a><a class="addthis_button_compact"></a><a class="addthis_counter addthis_bubble_style">
                            </a>
                        </div>

                        <script type="text/javascript" src="http://s7.addthis.com/js/250/addthis_widget.js#pubid=ra-5073e7951ddeab53"></script>
                    </li>
                </ul>
            </div>
            
            <div class="row">
                <span class="orange">Members of</span><br />
                <img src="Assets/images/logos.gif" width="791" height="37" alt="logo" align="left" />
                <div style="float: left; margin-left: 20px; margin-top: -42px;">
                    <table width="135" border="0" cellpadding="2" cellspacing="0" title="Click to Verify - This site chose VeriSign Trust Seal to promote trust online with consumers.">
                        <tr>
                            <td width="135" align="center" valign="top">

                                <script type="text/javascript" src="https://trustseal.verisign.com/getseal?host_name=www.southerntravelsindia.com&size=L&use_flash=YES&use_transparent=YES&lang=en"></script>

                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="row">
                <span class="orange floatright">This site is compatible with IE8 and above.</span>
            </div>
        </div>
        <div id="footBtm">
            <div id="cards">
                We Accept<br />
                <img src="Assets/images/cards.gif" width="200" height="27" alt="Visa | Master | Maestro | AMEX" />&nbsp;&nbsp;<%--<img
                    src="Assets/images/net_banking.gif" width="60" height="27" />--%>
                <img src="../Assets/images/net_banking.gif" id="btnDate" runat="server" width="60" height="27"
                    alt="Bank" style="cursor: pointer" onmouseover="$find('PopExNet').showPopup();"
                    onmouseout="$find('PopExNet').hidePopup();" />
                <cc1:PopupControlExtender ID="PopExNet" runat="server" BehaviorID="PopExNet" TargetControlID="btnDate"
                    PopupControlID="Panel123" Position="Top" OffsetX="-230" OffsetY="-515">
                    
                </cc1:PopupControlExtender>
                <asp:Panel ID="Panel123" runat="server" class="PopUp" BackColor="White" BorderWidth="1"
                    BorderColor="Red" Style="display: none; padding: 20px;" Width="951px">
                    <uc1:UCPaymentOptionNetBanking ID="UCPaymentOptionNetBanking1" runat="server" />
                </asp:Panel>
                
                <a href="../PaymentModes.aspx#BankTransfer"><img src="../Assets/images/bank_transfer.jpg" id="imgBankTransfer" runat="server" width="60" height="27"
                    alt="Bank" style="cursor: pointer" onmouseover="$find('PopBankTransfer').showPopup();"
                    onmouseout="$find('PopBankTransfer').hidePopup();" /></a>
                <cc1:PopupControlExtender ID="PopBankTransfer" runat="server" BehaviorID="PopBankTransfer" TargetControlID="imgBankTransfer"
                    PopupControlID="pnlBankTransfer" Position="Top" OffsetX="-230" OffsetY="-385">
                    
                </cc1:PopupControlExtender>
                <asp:Panel ID="pnlBankTransfer" runat="server" class="PopUp" BackColor="White" BorderWidth="1"
                    BorderColor="Red" Style="display: none; padding: 20px;" Width="800px">
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td>
                                <div class="spn">
                                    <span class="orange">Name of the account :</span> Southern Travels Pvt. Ltd.<br />
                                    <table cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td width="5px" align="center" valign="top">1.</td>
                                            <td width="48%" align="left" valign="top">
                                        <span class="orange">ICICI Bank</span><br />
                                        Address: 2692, Deshbandu Gupta Road, Karol Bagh, New Delhi 110005.<br />
                                        Account number: 6291 0503 6409<br />
                                        Swift Code: ICICINBBXXX<br />
                                        RTGS / NEFT/IFSC CODE: ICIC0006291
                                            </td>
                                            <td width="5px" align="center" valign="top">4.</td>
                                            <td width="48%" align="left" valign="top">
                                            <span class="orange">Central Bank of India</span><br />                                    
                                        Account number: 3054506320<br />
                                        Swift Code: CBININBBPAR<br />
                                        RTGS / NEFT/IFSC CODE: CBIN0280301
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="5px" align="center" valign="top">2.</td>
                                            <td width="48%" align="left" valign="top">
                                        <span class="orange">ING Vysya Bank Ltd.</span><br />
                                        Address: 16/11,R.D.Chambers,1 Floor, Arya Samaj Road, Karol Bagh, New Delhi 110005.<br />
                                        Account number: 5300 1100 9431<br />
                                        Swift Code: VY SAIN BB NDL<br />
                                        RTGS / NEFT/IFSC CODE: VYSA0005300
                                            </td>
                                            <td width="5px" align="center" valign="top">5.</td>
                                            <td width="48%" align="left" valign="top">
                                            <span class="orange">INDIAN OVERSEAS BANK</span><br />                                    
                                        Account number: 044202000005218<br />
                                        Swift Code: IOBAINBB442<br />
                                        RTGS / NEFT/IFSC CODE: IOBA0000442
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="5px" align="center" valign="top">3.</td>
                                            <td width="48%" align="left" valign="top">
                                            <span class="orange">HDFC Bank</span><br />
                                        Address: No. 2212, Gali no. 64-65, J-Block Naiwala, Gurudwara Road, Karol Bagh, New Delhi – 110005<br />
                                        Account number: 04398520000023<br />
                                        Swift Code: BOFAUS3N<br />
                                        RTGS / NEFT/IFSC CODE: HDFC0000439
                                            </td>
                                            <td width="5px" align="center" valign="top">6.</td>
                                            <td width="48%" align="left" valign="top">
                                            <span class="orange">AXIS BANK</span><br />
                                        Address: JHANDEWALAN EXTENSION NEW DELHI<br />
                                        Account number: 911020049660426<br />
                                        Swift Code: AXISINBB738<br />
                                        RTGS / NEFT/IFSC CODE: UTIB0000738
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                
                
            </div>
            <div id="airlines" style="padding-top: 5px;">
                Airlines<br />
                <img src="Assets/images/airlines.gif" width="579" height="42" alt="Jet Airways | Kingfisher | spicejet | Indian | JetLite | IndiGo | Paramount | Go" /></div>
        </div>
        <div id="copyright">
            <span class="floatleft">Copyright 2013 &copy; Southern Travels Pvt. Ltd., All Rights
                Reserved </span><span class="floatright">Designed &amp; Developed by <a href="http://sirez.com/"
                    target="_blank">Sirez Limited</a></span>
        </div>
    </div>
	<div class="row"><div id="back-top">
		<a href="#top"><img src="Assets/images/back-top.png" /></a>
	</div></div>
    <div style="position:absolute; right:0; bottom:120px;" runat="server" id="divPChat">
        <script type="text/javascript">(function () { var done = false; var script = document.createElement("script"); script.async = true; script.type = "text/javascript"; script.src = "https://www.purechat.com/VisitorWidget/WidgetScript"; document.getElementsByTagName('HEAD').item(0).appendChild(script); script.onreadystatechange = script.onload = function (e) { if (!done && (!this.readyState || this.readyState == "loaded" || this.readyState == "complete")) { var w = new PCWidget({ c: '0567b24d-9cee-44a2-9938-6021b0e0b712', f: true }); done = true; } }; })();</script>
    </div>
</div>

<script type="text/javascript">
var gaJsHost = (("https:" == document.location.protocol) ? "https://ssl." : "http://www.");
document.write(unescape("%3Cscript src='" + gaJsHost + "google-analytics.com/ga.js' type='text/javascript'%3E%3C/script%3E"));
</script>

<script type="text/javascript">
var pageTracker = _gat._getTracker("UA-4994177-1");
pageTracker._trackPageview();
</script>

