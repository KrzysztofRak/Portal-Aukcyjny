<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" Inherits="Portal_aukcyjny.PublicPages.User.UserProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width:100%;">
        <tr>
            <td>
                <strong>
                <asp:Label ID="Username" runat="server" Text="Username" style="font-size: x-large; color: #336699"></asp:Label>
                </strong>
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
                <asp:Label ID="EmailLabel" runat="server" Text="Email: "></asp:Label>
                </strong>
                <asp:Label ID="Email" runat="server" Text="Label"></asp:Label>
            </td>
            <td style="height: 22px"></td>
            <td style="height: 22px"></td>
        </tr>
        <tr>
            <td style="height: 23px"><strong>
                <asp:Label ID="SoldItemsNumLabel" runat="server" Text="Sprzedanych przedmiotów:"></asp:Label>
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
                <asp:Label ID="Label9" runat="server" style="color: #336699; font-size: large" Text="Lista aukcji użytkownika"></asp:Label>
                </strong></td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:ListView ID="ListView_UserAuctions" runat="server">
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
                <asp:Label ID="Label8" runat="server" style="color: #336699; font-size: large" Text="Komentarze"></asp:Label>
                </strong></td>
            <td style="height: 22px"></td>
            <td style="height: 22px"></td>
        </tr>
        <tr>
            <td>
                <asp:ListView ID="ListView_Comments" runat="server">
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
