<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Portal_aukcyjny._Default" %>

<%@ Register src="UserControls/SearchControl.ascx" tagname="SearchControl" tagprefix="uc1" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <table style="width: 100%;">
        <tr>
            <td>&nbsp;</td>
            <td class="text-center">
                <uc1:SearchControl ID="SearchControl1" runat="server" />
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                <asp:TreeView ID="CategoriesTree" runat="server" ImageSet="Simple" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged" style="color: #FFFFFF; background-color: #CCCCCC" Width="232px" ForeColor="White">
                    <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                    <NodeStyle Font-Names="Tahoma" Font-Size="10pt" ForeColor="Black" HorizontalPadding="0px" NodeSpacing="0px" VerticalPadding="0px" />
                    <ParentNodeStyle Font-Bold="False" />
                    <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px" VerticalPadding="0px" />
                </asp:TreeView>
            </td>
            <td>&nbsp;</td>
        </tr>
    </table>

</asp:Content>
