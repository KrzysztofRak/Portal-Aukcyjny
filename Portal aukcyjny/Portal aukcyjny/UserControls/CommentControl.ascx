<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CommentControl.ascx.cs" Inherits="Portal_aukcyjny.UserControls.CommentControl" %>
<style type="text/css">

    .auto-style4 {
        width: 100%;
        height: 34px;
    }
    .auto-style3 {
        width: 156px;
        height: 32px;
    }
    .auto-style2 {
        color: #336699;
    }
    .auto-style6 {
        height: 32px;
    }
    .auto-style7 {
        width: 429px;
        height: 32px;
    }

    .comment {
        border: 1px solid black;
    }
</style>

<table class="auto-style4 comment">
    <tr>
        <td class="auto-style3">
            <asp:HyperLink ID="AuthorName" runat="server" CssClass="auto-style2">HyperLink </asp:HyperLink>
            <asp:Label ID="IsSeller" runat="server" Text="[Kupujący]"></asp:Label>
        </td>
        <td class="auto-style7">
            <asp:Label ID="Comment" runat="server" Text="Label"></asp:Label>
        </td>
        <td class="auto-style6">
            <asp:Label ID="Date" runat="server" Text="Label"></asp:Label>
        </td>
    </tr>
</table>


