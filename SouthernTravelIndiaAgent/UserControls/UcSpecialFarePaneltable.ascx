<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcSpecialFarePaneltable.ascx.cs" Inherits="SouthernTravelIndiaAgent.UserControls.UcSpecialFarePaneltable" %>

    
    <link href="Assets/css/rupee.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
     .tablewrap td{border-bottom-color: #e9e9e9!important;}
    </style>

<div id ="tdPanel" runat="server"  >
    <asp:Literal runat="server" ID="litSpecialTourFarePanel" >
    </asp:Literal>
    
    <asp:Literal runat="server" ID="litSplTourFarePanelBook">
    </asp:Literal> 
    <p class="notepara">
    <div id="divnote1" runat="server">
    <strong>Note:</strong> GST @5.00% applicable.
    </div>
    <div id="divnote2" runat="server">
    <strong>Note:</strong>
    <ul style="margin-left:20px;"><li>GST @5.00% applicable.</li>
    <li>SDF (Sustainable Development Fee) is also included.</li>
    <li>Please Note that Non-AC Vehicles are provided in Bhutan Sector & Vehicle structure will be as follows
    <ul style="margin-left:20px;"><li><strong>Non AC Sedan (For 2-3 Pax )</strong> - Tuscan, Santafe /Hyundai H1 / Similar</li>
    <li><strong>Non AC SUV (For 4-6 Pax )</strong> - Non AC Jumbo Hiace Van / Similar</li>
    <li><strong>Non AC Tempo (7-9 Pax )</strong> – Non AC Coaster / Similar</li></ul></li></ul>
    </div>
</p>
</div>

<div runat="server" id="tdNotePanel">
    <asp:Label ID="lblNote" runat="server" Text=""></asp:Label>
</div>
