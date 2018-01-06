<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Portal_aukcyjny._Default" %>

<%@ Register Src="~/UserControls/AuctionControl.ascx" TagPrefix="uc1" TagName="AuctionControl" %>


<%@ Register src="UserControls/FilterControl.ascx" tagname="FilterControl" tagprefix="uc2" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        .categories-cell {
           vertical-align: top;
        }
    </style>
    <table style="width: 100%;">
        <tr>
            <td>
                &nbsp;</td>
            <td class="text-left" colspan="3">
                <uc2:FilterControl ID="FilterControl1" runat="server" />
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td class="text-center" colspan="2">
                &nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style1"></td>
            <td colspan="2" class="auto-style1"></td>
            <td class="auto-style1"></td>
        </tr>
        <tr>
            <td colspan="2" class="categories-cell">
                <asp:TreeView ID="CategoriesTree" runat="server" ImageSet="Simple" style="color: #FFFFFF; background-color: #CCCCCC" Width="220px" ForeColor="White" CssClass="categories-tree" OnSelectedNodeChanged="CategoriesTree_SelectedNodeChanged">
                    <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                    <NodeStyle Font-Names="Tahoma" Font-Size="10pt" ForeColor="Black" HorizontalPadding="0px" NodeSpacing="0px" VerticalPadding="0px" />
                    <ParentNodeStyle Font-Bold="False" />
                    <SelectedNodeStyle Font-Underline="True" Font-Bold="true" HorizontalPadding="0px" VerticalPadding="0px" />
                </asp:TreeView>
            </td>
            <td>
                <div style="height:500px; overflow:scroll">
                <asp:ListView ID="ListView_Auctions" runat="server">
                    <ItemTemplate>
                        <uc1:AuctionControl runat="server" ID="AuctionControl" />
                    </ItemTemplate>
                </asp:ListView>
                </div>
            </td>
            <td>&nbsp;</td>
        </tr>
    </table>

</asp:Content>
