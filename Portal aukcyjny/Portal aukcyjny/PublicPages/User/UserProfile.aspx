<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" Inherits="Portal_aukcyjny.PublicPages.User.UserProfile" %>

<%@ Register Src="~/UserControls/AuctionControl.ascx" TagPrefix="uc1" TagName="AuctionControl" %>
<%@ Register Src="~/UserControls/CommentControl.ascx" TagPrefix="uc1" TagName="CommentControl" %>



<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width: 100%;">
        <tr>
            <td>
                    <asp:Label ID="Username" runat="server" Text="Username" Style="font-size: xx-large; color: #336699"></asp:Label>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="height: 22px"></td>
            <td style="height: 22px"></td>
            <td style="height: 22px"></td>
        </tr>
        <tr>
            <td style="height: 22px"><strong>
                <asp:Label ID="EmailLabel" runat="server" Text="<%$ Resources:Email%>"></asp:Label>
            </strong>
                <asp:Label ID="Email" runat="server" Text="Label"></asp:Label>
            </td>
            <td style="height: 22px"></td>
            <td style="height: 22px"></td>
        </tr>
        <tr>
            <td style="height: 22px"><strong>
                <asp:Label ID="RegistrationDateLabel" runat="server" Text="<%$ Resources:Członek od%>"></asp:Label>
            </strong>
                <asp:Label ID="RegistrationDate" runat="server" Text="Label"></asp:Label>
            </td>
            <td style="height: 22px">&nbsp;</td>
            <td style="height: 22px">&nbsp;</td>
        </tr>
        <tr>
            <td style="height: 23px"><strong>
                <asp:Label ID="SoldItemsNumLabel" runat="server" Text="<%$ Resources:Sprzedanych przedmiotów%>"></asp:Label>
            </strong>&nbsp;<asp:Label ID="SoldItemsNum" runat="server" Text="Label"></asp:Label>
            </td>
            <td style="height: 23px"></td>
            <td style="height: 23px"></td>
        </tr>
        <tr>
            <td style="height: 22px"></td>
            <td style="height: 22px"></td>
            <td style="height: 22px"></td>
        </tr>
        <tr>
            <td><strong>
                <asp:Label ID="Label9" runat="server" Style="color: #CC9900; font-size: x-large" Text="<%$ Resources:Lista aukcji użytkownika%>"></asp:Label>
            </strong></td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:ListView ID="ListView_UserAuctions" runat="server">
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
            <td style="height: 22px"><strong>
                <asp:Label ID="Label8" runat="server" Style="color: #CC9900; font-size: x-large" Text="<%$ Resources:Otrzymane komentarze%>"></asp:Label>
            </strong></td>
            <td style="height: 22px"></td>
            <td style="height: 22px"></td>
        </tr>
        <tr>
            <td style="height: 22px">&nbsp;</td>
            <td style="height: 22px">&nbsp;</td>
            <td style="height: 22px">&nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:ListView ID="ListView_Comments" runat="server">
                    <ItemTemplate>
                        <uc1:CommentControl runat="server" ID="CommentControl" />
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
    </table>
</asp:Content>
