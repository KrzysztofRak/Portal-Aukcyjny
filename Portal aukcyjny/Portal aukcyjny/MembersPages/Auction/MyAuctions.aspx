<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MyAuctions.aspx.cs" Inherits="Portal_aukcyjny.MembersPages.Auction.MyAuctions" %>

<%@ Register Src="~/UserControls/AuctionControl.ascx" TagPrefix="uc1" TagName="AuctionControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width: 100%;">
        <tr>
            <td><strong>
                <asp:Label ID="Label1" runat="server" Style="font-size: x-large; color: #CC9900" Text="<%$ Resources:Wystawione%>"></asp:Label>
            </strong></td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="height: 22px">
                <div style="height:200px; overflow:scroll">
                <asp:ListView ID="ListView_MyAuctions" runat="server">
                    <ItemTemplate>
                        <uc1:AuctionControl runat="server" ID="AuctionControl" />
                    </ItemTemplate>
                </asp:ListView>
                </div>
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
                <asp:Label ID="Label3" runat="server" Style="font-size: x-large; color: #CC9900" Text="<%$ Resources:Sprzedane%>"></asp:Label>
            </strong></td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <div style="height:200px; overflow:scroll">
                <asp:ListView ID="ListView_Sold" runat="server">
                    <ItemTemplate>
                        <uc1:AuctionControl runat="server" ID="AuctionControl" />
                    </ItemTemplate>
                </asp:ListView>
                </div>
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
            <td><strong>
                <asp:Label ID="Label5" runat="server" Style="font-size: x-large; color: #CC9900" Text="<%$ Resources:Kupione%>"></asp:Label>
            </strong></td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <div style="height:200px; overflow:scroll">
                <asp:ListView ID="ListView_Buyed" runat="server">
                    <ItemTemplate>
                        <uc1:AuctionControl runat="server" ID="AuctionControl" />
                    </ItemTemplate>
                </asp:ListView>
                </div>
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
                <asp:Label ID="Label4" runat="server" Style="font-size: x-large; color: #CC9900" Text="<%$ Resources:Licytuję%>"></asp:Label>
            </strong></td>
            <td style="height: 36px"></td>
            <td style="height: 36px"></td>
        </tr>
        <tr>
            <td style="height: 22px">
                <div style="height:200px; overflow:scroll">
                <asp:ListView ID="ListView_Bid" runat="server">
                    <ItemTemplate>
                        <uc1:AuctionControl runat="server" ID="AuctionControl" />
                    </ItemTemplate>
                </asp:ListView>
                </div>
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
                <asp:Label ID="Label2" runat="server" Style="font-size: x-large; color: #CC9900" Text="<%$ Resources:Obserwowane%>"></asp:Label>
            </strong></td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <div style="height:200px; overflow:scroll">
                                <asp:ListView ID="ListView_Watched" runat="server">
                    <ItemTemplate>
                        <uc1:AuctionControl runat="server" ID="AuctionControl" />
                    </ItemTemplate>
                </asp:ListView>
                    </div>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>
