<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MyAuctions.aspx.cs" Inherits="Portal_aukcyjny.MembersPages.Auction.MyAuctions" %>

<%@ Register Src="~/UserControls/AuctionControl.ascx" TagPrefix="uc1" TagName="AuctionControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width: 100%;">
        <tr>
            <td><strong>
                <asp:Label ID="Label1" runat="server" Style="font-size: x-large; color: #CC9900" Text="Wystawione"></asp:Label>
            </strong></td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="height: 22px">
                <asp:ListView ID="ListView_MyAuctions" runat="server">
                    <ItemTemplate>
                        <uc1:AuctionControl runat="server" ID="AuctionControl" />
                    </ItemTemplate>
                </asp:ListView>
            </td>
            <td style="height: 22px"></td>
            <td style="height: 22px"></td>
        </tr>
        <tr>
            <td style="height: 22px">&nbsp;</td>
            <td style="height: 22px">&nbsp;</td>
            <td style="height: 22px">&nbsp;</td>
        </tr>
        <tr>
            <td><strong>
                <asp:Label ID="Label3" runat="server" Style="font-size: x-large; color: #CC9900" Text="Zakończone"></asp:Label>
            </strong></td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:ListView ID="ListView_Finished" runat="server">
                    <ItemTemplate>
                        <uc1:AuctionControl runat="server" ID="AuctionControl" />
                    </ItemTemplate>
                </asp:ListView>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="height: 36px"><strong>
                <asp:Label ID="Label2" runat="server" Style="font-size: x-large; color: #CC9900" Text="Obserwowane"></asp:Label>
            </strong></td>
            <td style="height: 36px"></td>
            <td style="height: 36px"></td>
        </tr>
        <tr>
            <td style="height: 22px">
                <asp:ListView ID="ListView_Watched" runat="server">
                    <ItemTemplate>
                        <uc1:AuctionControl runat="server" ID="AuctionControl" />
                    </ItemTemplate>
                </asp:ListView>
            </td>
            <td style="height: 22px"></td>
            <td style="height: 22px"></td>
        </tr>
        <tr>
            <td style="height: 22px">&nbsp;</td>
            <td style="height: 22px">&nbsp;</td>
            <td style="height: 22px">&nbsp;</td>
        </tr>
        <tr>
            <td><strong>
                <asp:Label ID="Label4" runat="server" Style="font-size: x-large; color: #CC9900" Text="Licytuję"></asp:Label>
            </strong></td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:ListView ID="ListView_Bid" runat="server">
                    <ItemTemplate>
                        <uc1:AuctionControl runat="server" ID="AuctionControl" />
                    </ItemTemplate>
                </asp:ListView>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>
