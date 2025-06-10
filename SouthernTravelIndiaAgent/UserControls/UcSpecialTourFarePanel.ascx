<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcSpecialTourFarePanel.ascx.cs" Inherits="SouthernTravelIndiaAgent.UserControls.UcSpecialTourFarePanel" %>


    <link href="Assets/css/rupee.css" rel="stylesheet" type="text/css" />
            
<div id ="tdPanel" runat="server"  >
    <asp:Literal runat="server" ID="litSpecialTourFarePanel">
    </asp:Literal>
    <p class="notepara">
    <span>Note:</span> GST @5.00% applicable.
</p>
    <asp:Literal runat="server" ID="litSplTourFarePanelBook">
    </asp:Literal> 
</div>
<div runat="server" id="tdNotePanel">
    <asp:Label ID="lblNote" runat="server" Text=""></asp:Label>
</div>
