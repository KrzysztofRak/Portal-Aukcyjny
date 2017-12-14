<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Portal_aukcyjny._Default" %>

<%@ Register src="UserControls/SearchControl.ascx" tagname="SearchControl" tagprefix="uc1" %>
<%@ Register Src="~/UserControls/AuctionControl.ascx" TagPrefix="uc1" TagName="AuctionControl" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        .categories-cell {
           vertical-align: top;
        }
    </style>
    <table style="width: 100%;">
        <tr>
            <td>&nbsp;</td>
            <td class="text-center" colspan="2">
                <uc1:SearchControl ID="SearchControl1" runat="server" />
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style1"></td>
            <td colspan="2" class="auto-style1"></td>
            <td class="auto-style1"></td>
        </tr>
        <tr>
            <td colspan="2" class="categories-cell">
                <asp:TreeView ID="CategoriesTree" runat="server" ImageSet="Simple" style="color: #FFFFFF; background-color: #CCCCCC" Width="220px" ForeColor="White" CssClass="categories-tree">
                    <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                    <NodeStyle Font-Names="Tahoma" Font-Size="10pt" ForeColor="Black" HorizontalPadding="0px" NodeSpacing="0px" VerticalPadding="0px" />
                    <ParentNodeStyle Font-Bold="False" />
                    <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px" VerticalPadding="0px" />
                </asp:TreeView>
            </td>
            <td>
                <asp:ListView ID="ListView_Auctions" runat="server">
                    <ItemTemplate>
                        <uc1:AuctionControl runat="server" ID="AuctionControl" />
                    </ItemTemplate>
                </asp:ListView>
            </td>
            <td>&nbsp;</td>
        </tr>
    </table>

</asp:Content>
