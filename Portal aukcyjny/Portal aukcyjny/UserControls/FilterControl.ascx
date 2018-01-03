<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FilterControl.ascx.cs" Inherits="Portal_aukcyjny.UserControls.FilterControl" %>
<style type="text/css">
    .auto-style4 {
        width: 100%;
    }
    .auto-style10 {
        font-size: medium;
    }
    .auto-style23 {
        width: 154px;
    }
    .auto-style29 {
        height: 29px;
    }
    .auto-style31 {
        width: 183px;
    }
    .auto-style33 {
        height: 29px;
        width: 158px;
    }
    .auto-style35 {
        width: 101px;
    }
    .auto-style36 {
        height: 29px;
        width: 101px;
    }
    .auto-style37 {
        width: 54px;
    }
    .auto-style38 {
        width: 158px;
    }
</style>

<table class="auto-style4">
    <tr>
        <td>&nbsp;</td>
        <td>
            <asp:CheckBox ID="CheckBox_OnlyBuyItNow" runat="server" Text="<%$ Resources:Tylko kup teraz%>" />
        </td>
        <td class="auto-style38">
            <asp:CheckBox ID="CheckBox_MinPrice" runat="server" Text="<%$ Resources:Cena minimalna%>" />
        </td>
        <td class="auto-style35">
            <asp:TextBox ID="TextBox_MinPrice" runat="server" TextMode="Number" Width="50px"></asp:TextBox>
        </td>
        <td class="auto-style31">
            <asp:CheckBox ID="CheckBox_MinViews" runat="server" Text="<%$ Resources:Min. ilość wyświetleń%>" />
        </td>
        <td class="auto-style37">
            <asp:TextBox ID="TextBox_MinViews" runat="server" TextMode="Number" Width="50px"></asp:TextBox>
        </td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td class="auto-style23">
            <asp:CheckBox ID="CheckBox_OnlyBidding" runat="server" Text="<%$ Resources:Tylko licytacje%>" />
        </td>
        <td class="auto-style38">
            <asp:CheckBox ID="CheckBox_MaxPrice" runat="server" Text="<%$ Resources:Cena maksymalna%>" />
        </td>
        <td class="auto-style35">
            <asp:TextBox ID="TextBox_MaxPrice" runat="server" TextMode="Number" Width="50px"></asp:TextBox>
        </td>
        <td class="auto-style31">
            <asp:CheckBox ID="CheckBox_MaxViews" runat="server" Text="<%$ Resources:Maks. ilość wyświetleń%>" />
        </td>
        <td class="auto-style37">
            <asp:TextBox ID="TextBox_MaxViews" runat="server" TextMode="Number" Width="50px"></asp:TextBox>
        </td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td class="auto-style29"></td>
        <td class="auto-style29"></td>
        <td class="auto-style33">
            <asp:CheckBox ID="CheckBox_MinOffersNum" runat="server" Text="<%$ Resources:Min. ilość ofert%>" />
        </td>
        <td class="auto-style36">
            <asp:TextBox ID="TextBox_MinOffersNum" runat="server" TextMode="Number" Width="50px"></asp:TextBox>
        </td>
        <td class="auto-style31">
            <asp:CheckBox ID="CheckBox_MaxTimeLeft" runat="server" Text="<%$ Resources:Maks. czas do końca%>" />
        </td>
        <td class="auto-style37">
            <asp:TextBox ID="TextBox_MaxTimeLeft" runat="server" TextMode="Number" Width="50px"></asp:TextBox>
        </td>
        <td>
            <asp:Label ID="Label1" runat="server" CssClass="auto-style10" Text="<%$ Resources:dni%>"></asp:Label>
        </td>
        <td></td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td class="auto-style38">
            <asp:CheckBox ID="CheckBox_MaxOffersNum" runat="server" Text="<%$ Resources:Maks. ilość ofert%>" />
        </td>
        <td class="auto-style35">
            <asp:TextBox ID="TextBox_MaxOffersNum" runat="server" TextMode="Number" Width="50px"></asp:TextBox>
        </td>
        <td class="auto-style31">
            <asp:CheckBox ID="CheckBox_ShipmentType" runat="server" Text="<%$ Resources:Rodzaj przesyłki%>" />
        </td>
        <td colspan="2">
            <asp:DropDownList ID="DropDownList_ShipmentType" runat="server" Height="22px" Width="300px">
            </asp:DropDownList>
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td class="auto-style38">
            &nbsp;</td>
        <td class="auto-style35">
            &nbsp;</td>
        <td class="auto-style31">
            &nbsp;</td>
        <td colspan="2">
            &nbsp;</td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td colspan="5">
<asp:TextBox ID="SearchBox" runat="server" Width="391px"></asp:TextBox>
<asp:Button ID="BtnSearch" runat="server" Text="<%$ Resources:Szukaj%>" OnClick="SearchBtn_Click" />

        </td>
        <td>&nbsp;</td>
    </tr>
</table>

