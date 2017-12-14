<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewAuction.aspx.cs" Inherits="Portal_aukcyjny.PublicPages.Auction.ViewAuction" %>

<%@ Register Src="../../UserControls/OfferControl.ascx" TagName="OfferControl" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/OfferControl.ascx" TagPrefix="uc2" TagName="OfferControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .bid-input {
            margin-bottom: 8px;
        }
        .auto-style1 {
            width: 30px;
            height: 29px;
        }
        .auto-style2 {
            width: 344px;
            height: 29px;
        }
        .auto-style3 {
            height: 29px;
        }
        .auto-style6 {
            width: 229px;
            height: 29px;
        }
        .auto-style7 {
            width: 304px;
            height: 29px;
        }
        .auto-style10 {
            text-align: left;
            width: 229px;
        }
        .auto-style11 {
            width: 30px;
        }
        .auto-style12 {
            width: 344px;
        }
        .auto-style13 {
            width: 304px;
        }
        .auto-style14 {
            width: 30px;
            height: 31px;
        }
        .auto-style15 {
            width: 344px;
            height: 31px;
        }
        .auto-style16 {
            height: 31px;
        }
    </style>
    <table style="width: 100%;">
        <tr>
            <td style="width: 30px">&nbsp;</td>
            <td>&nbsp;</td>
            <td colspan="4"><strong>
                <asp:Label ID="AuctionTitle" runat="server" Style="font-size: x-large" Text="Tytuł"></asp:Label>
            </strong></td>
            <td style="width: 304px">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 30px; height: 22px"></td>
            <td style="width: 30px; height: 22px">&nbsp;</td>
            <td style="width: 30px; height: 22px"></td>
            <td style="width: 29px; height: 22px"></td>
            <td style="height: 22px; width: 344px"></td>
            <td style="width: 229px; height: 22px"></td>
            <td style="width: 304px; height: 22px"></td>
            <td style="height: 22px"></td>
        </tr>
        <tr>
            <td class="auto-style1"></td>
            <td rowspan="6" style="width: 30px">&nbsp;</td>
            <td rowspan="6" style="width: 30px">
                <asp:Image ID="AuctionImg" runat="server" Width="400px" BorderStyle="Solid" BorderWidth="1px" />
            </td>
            <td rowspan="6" style="width: 29px">&nbsp;</td>
            <td class="auto-style2"><strong>
                <asp:Label ID="Label6" runat="server" Style="color: #336699" Text="Ilość przedmiotów: "></asp:Label>
            </strong>&nbsp;<asp:Label ID="ItemsNum" runat="server" Text="Label"></asp:Label>
            </td>
            <td colspan="2" class="auto-style3"><strong>
                <asp:Label ID="Label8" runat="server" Style="color: #336699" Text="Do końca pozostało: "></asp:Label>
            </strong>&nbsp;<asp:Label ID="EndTime" runat="server" Text="Label"></asp:Label>
                &nbsp;</td>
            <td class="auto-style3"></td>
        </tr>
        <tr>
            <td class="auto-style1">&nbsp;</td>
            <td class="auto-style2">&nbsp;</td>
            <td colspan="2" class="auto-style3">&nbsp;</td>
            <td class="auto-style3">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style14"></td>
            <td class="auto-style15"><strong>
                <asp:Label ID="BuyItNowLabel" runat="server" Style="color: #990000" Text="Cena kup teraz: "></asp:Label>
            </strong>&nbsp;<asp:Label ID="BuyItNowPrice" runat="server" Text="Label"></asp:Label>
            </td>
            <td colspan="2" class="auto-style16">
                <asp:Button ID="BuyItNowBtn" runat="server" Text="Kup teraz" Width="125px" Height="31px" />
            </td>
            <td class="auto-style16"></td>
        </tr>
        <tr>
            <td class="auto-style14">&nbsp;</td>
            <td class="auto-style15">&nbsp;</td>
            <td colspan="2" class="auto-style16">&nbsp;</td>
            <td class="auto-style16">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style11"></td>
            <td class="auto-style12">
                <strong>
                <asp:Label ID="BidLabel" runat="server" Style="color: #990000" Text="Najwyższa oferta: "></asp:Label>
            </strong><asp:Label ID="HighestBid" runat="server" Text="Label"></asp:Label>
            </td>
            <td class="auto-style10">
                <asp:TextBox ID="Bid" runat="server" CssClass="bid-input">0</asp:TextBox>
                <asp:Button ID="BidBtn" runat="server" Text="Licytuj" Width="125px" Height="31px" />
            </td>
            <td class="auto-style13"></td>
            <td></td>
        </tr>
        <tr>
            <td class="auto-style1"></td>
            <td class="auto-style2">
                <strong>
                <asp:Label ID="Label10" runat="server" Style="color: #006600" Text="Przesyłka: "></asp:Label>
            </strong><asp:Label ID="Shipment" runat="server" Text="Label"></asp:Label>
            </td>
            <td class="auto-style6">
                <strong>
                <asp:Label ID="Label12" runat="server" Style="color: #CC6600" Text="Sprzedający: "></asp:Label>
            </strong>
                <asp:HyperLink ID="SellerName" runat="server">HyperLink</asp:HyperLink>
            </td>
            <td class="auto-style7">
                </td>
            <td class="auto-style3"></td>
        </tr>
        <tr>
            <td style="height: 22px; width: 30px">&nbsp;</td>
            <td style="height: 22px; width: 30px">&nbsp;</td>
            <td style="height: 22px; width: 30px">&nbsp;</td>
            <td style="height: 22px; width: 29px">&nbsp;</td>
            <td style="height: 22px; width: 344px">&nbsp;</td>
            <td style="height: 22px; width: 229px">&nbsp;</td>
            <td style="height: 22px; width: 304px">&nbsp;</td>
            <td style="height: 22px">&nbsp;</td>
        </tr>
        <tr>
            <td style="height: 22px; width: 30px"></td>
            <td style="height: 22px; width: 30px">&nbsp;</td>
            <td style="height: 22px; width: 30px"></td>
            <td style="height: 22px; width: 29px"></td>
            <td style="height: 22px; width: 344px"></td>
            <td style="height: 22px; width: 229px"></td>
            <td style="height: 22px; width: 304px"></td>
            <td style="height: 22px"></td>
        </tr>
        <tr>
            <td style="width: 30px; height: 22px"></td>
            <td style="height: 22px">&nbsp;</td>
            <td colspan="5" style="height: 22px"><strong>
                <asp:Label ID="Label11" runat="server" Style="font-size: large; color: #336699; text-decoration: underline;" Text="Opis przedmiotu"></asp:Label>
            </strong></td>
            <td style="height: 22px"></td>
        </tr>
        <tr>
            <td style="width: 30px; height: 22px">&nbsp;</td>
            <td style="height: 22px">&nbsp;</td>
            <td colspan="5" style="height: 22px">&nbsp;</td>
            <td style="height: 22px">&nbsp;</td>
        </tr>
        <tr>
            <td style="height: 22px; width: 30px"></td>
            <td style="height: 22px">&nbsp;</td>
            <td colspan="5" style="height: 22px">
                <asp:Label ID="Description" runat="server" Text="Opis"></asp:Label>
            </td>
            <td style="height: 22px"></td>
        </tr>
        <tr>
            <td style="width: 30px; height: 22px"></td>
            <td style="width: 30px; height: 22px">&nbsp;</td>
            <td style="width: 30px; height: 22px"></td>
            <td style="width: 29px; height: 22px"></td>
            <td style="width: 344px; height: 22px"></td>
            <td style="width: 229px; height: 22px"></td>
            <td style="width: 304px; height: 22px"></td>
            <td style="height: 22px"></td>
        </tr>
        <tr>
            <td style="width: 30px; height: 22px">&nbsp;</td>
            <td style="width: 30px; height: 22px">&nbsp;</td>
            <td style="width: 30px; height: 22px">&nbsp;</td>
            <td style="width: 29px; height: 22px">&nbsp;</td>
            <td style="width: 344px; height: 22px">&nbsp;</td>
            <td style="width: 229px; height: 22px">&nbsp;</td>
            <td style="width: 304px; height: 22px">&nbsp;</td>
            <td style="height: 22px">&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 30px; height: 22px">&nbsp;</td>
            <td style="height: 22px">&nbsp;</td>
            <td style="height: 22px" colspan="5"><strong>
                <asp:Label ID="Label13" runat="server" Style="font-size: large; color: #336699; text-decoration: underline;" Text="Lista ofert"></asp:Label>
            </strong></td>
            <td style="height: 22px">&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 30px; height: 22px">&nbsp;</td>
            <td style="height: 22px">&nbsp;</td>
            <td style="height: 22px" colspan="5">
                <asp:ListView ID="ListView_Offers" runat="server">

                    <ItemTemplate>
                        <uc2:OfferControl runat="server" ID="OfferControl" />
                    </ItemTemplate>
                </asp:ListView>
            </td>
            <td style="height: 22px">&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 30px; height: 22px"></td>
            <td style="width: 30px; height: 22px"></td>
            <td style="width: 30px; height: 22px"></td>
            <td style="width: 29px; height: 22px"></td>
            <td style="width: 344px; height: 22px"></td>
            <td style="width: 229px; height: 22px"></td>
            <td style="width: 304px; height: 22px"></td>
            <td style="height: 22px"></td>
        </tr>
        <tr>
            <td style="width: 30px; height: 22px">&nbsp;</td>
            <td style="width: 30px; height: 22px">&nbsp;</td>
            <td style="width: 30px; height: 22px"><strong>
                <asp:Label ID="Label14" runat="server" Style="font-size: large; color: #336699; text-decoration: underline;" Text="Komentarze: "></asp:Label>
            </strong></td>
            <td style="width: 29px; height: 22px">&nbsp;</td>
            <td style="width: 344px; height: 22px">&nbsp;</td>
            <td style="width: 229px; height: 22px">&nbsp;</td>
            <td style="width: 304px; height: 22px">&nbsp;</td>
            <td style="height: 22px">&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 30px; height: 22px">&nbsp;</td>
            <td style="width: 30px; height: 22px">&nbsp;</td>
            <td style="width: 30px; height: 22px">&nbsp;</td>
            <td style="width: 29px; height: 22px">&nbsp;</td>
            <td style="width: 344px; height: 22px">&nbsp;</td>
            <td style="width: 229px; height: 22px">&nbsp;</td>
            <td style="width: 304px; height: 22px">&nbsp;</td>
            <td style="height: 22px">&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 30px; height: 22px"></td>
            <td style="width: 30px; height: 22px">&nbsp;</td>
            <td style="width: 30px; height: 22px"></td>
            <td style="width: 29px; height: 22px"></td>
            <td style="height: 22px; width: 344px"></td>
            <td style="width: 229px; height: 22px"></td>
            <td style="width: 304px; height: 22px"><strong>
                <asp:Label ID="LabelViews" runat="server" Text="Wyświetlenia: "></asp:Label>
            </strong>
                <asp:Label ID="ViewsNum" runat="server" Text="Label"></asp:Label>
            </td>
            <td style="height: 22px"></td>
        </tr>
    </table>
</asp:Content>
