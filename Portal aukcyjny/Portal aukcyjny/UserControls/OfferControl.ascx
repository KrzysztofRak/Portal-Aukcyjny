<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OfferControl.ascx.cs" Inherits="Portal_aukcyjny.UserControls.OfferControl" %>
<style type="text/css">
    .auto-style2 {
        color: #336699;
    }
    .auto-style3 {
        width: 130px;
    }
    .auto-style4 {
        width: 100%;
        height: 34px;
    }
    .auto-style5 {
        width: 107px;
    }
</style>

<table class="auto-style4">
    <tr>
        <td class="auto-style3">
            <asp:HyperLink ID="BidderName" runat="server" CssClass="auto-style2">HyperLink</asp:HyperLink>
        </td>
        <td class="auto-style5">
            <asp:Label ID="BidPrice" runat="server" Text="Label"></asp:Label>
        </td>
        <td>
            <asp:Label ID="BidDate" runat="server" Text="Label"></asp:Label>
        </td>
    </tr>
</table>

