<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CommentControl.ascx.cs" Inherits="Portal_aukcyjny.UserControls.CommentControl" %>
<style type="text/css">

    .auto-style4 {
        width: 100%;
        height: 34px;
    }
    .auto-style3 {
        width: 176px;
        height: 32px;
    }
    .auto-style2 {
        color: #336699;
    }
    .auto-style6 {
        height: 32px;
    }
    
    .comment {
        border: 1px solid black;
    }
    .auto-style9 {
        width: 24px;
    }
    .auto-style11 {
        width: 127px;
        height: 32px;
    }
    .auto-style12 {
        width: 159px;
        height: 32px;
    }
</style>

<table class="auto-style4 comment">
    <tr>
        <td class="auto-style9"></td>
        <td class="auto-style3">
            <asp:HyperLink ID="AuthorName" runat="server" CssClass="auto-style2">HyperLink </asp:HyperLink>
            <asp:Label ID="IsSeller" runat="server" Text="[Kupujący]"></asp:Label>
        </td>
        <td class="auto-style12">
            <asp:Label ID="Comment" runat="server" Text="Label"></asp:Label>
        </td>
        <td class="auto-style11">
            <asp:Label ID="Date" runat="server" Text="Label"></asp:Label>
        </td>
        <td class="auto-style6">
            <asp:HyperLink ID="Auction" runat="server" CssClass="auto-style2">HyperLink </asp:HyperLink>
        </td>
    </tr>
</table>


