<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmterms.aspx.cs" Inherits="SouthernTravelIndiaAgent.frmterms" %>

<%@ Register Src="UserControls/Ucheader.ascx" TagPrefix="uc" TagName="Header" %>
<%@ Register Src="UserControls/UcFooter.ascx" TagPrefix="uc" TagName="Footer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Southern Travels India :: Terms & Conditions</title>
   <link href="css/st.css" rel="stylesheet" type="text/css" />
    <link href="css/slider.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" media="all" href="css/niceforms-default.css" />

    <script type="text/javascript" src="Assets/js/jquery-1.6.1.min.js"></script>

    <script type="text/javascript" src="Assets/js/effects.js"></script>

    <script type="text/javascript" src="Assets/js/jquery.nivo.slider.js"></script>

    <script type="text/javascript">
	    $(window).load(function() {
		    $('#slider').nivoSlider();
        });
    </script>

    <script language="javascript" type="text/javascript" src="Assets/js/niceforms.js"></script>
<script language="javascript" src="Assets/js/Script.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
     function closewind()
    {
        window.self.close();
    }
    function framePrint(whichFrame)
    {
        //parent[whichFrame].focus();
        go(whichFrame);
    }
    function go(whichFrame)
    {
        var a = window. open('','','scrollbars=yes');
        a.document.open("text/html");       
        var strDoc = document.getElementById(whichFrame).innerHTML;                
        a.document.write(strDoc); 
        a.document.close();
        a.print();
    }
    </script>

</head>
<body>
    <form id="form1" runat="server">
     <div id="wrapper">
        <!-- header start --><asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <uc:Header runat="server" ID="ucHeader"  />
        <!-- header end -->
        <!-- body content start -->
        <div id="page-content">
            <div class="pageWidth">
                <div class="row">
                    <div class="pLeft">
                        
                    </div>
                    <div class="pRight">
                        <!--START -- Page Content -->
                        <div class="pRight" align="right">
                        <img  id=Button1 onclick=framePrint('terms');   alt=Print src=Assets/images/print.png name=Button1 style=cursor:pointer;/>
                        <%--<input class="intdtxth" id="Button1" onclick="framePrint('terms');" type="button"
                                value="Print" name="Button1" />--%></div>
                        <!--END -- Page Content -->
                    </div>
                </div>
                <div class="row">
                 <div id="terms" runat="server">
                </div>
                </div>
            </div>
        </div>
        <!-- body content end -->
        <!-- footer start -->
        <uc:Footer runat="server" ID="ucFooter" />
        <!-- footer end -->
    </div>
   
    </form>
</body>
</html>
