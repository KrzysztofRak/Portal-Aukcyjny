<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateAuction.aspx.cs" Inherits="Portal_aukcyjny.Auction.CreateAuction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width:100%;">
        <tr>
            <td style="height: 22px">
                <table style="width:100%;">
                    <tr>
                        <td>&nbsp;</td>
                        <td class="text-center">
                            <asp:Label ID="Label2" runat="server" Font-Size="X-Large" Text="Utwórz nową aukcję"></asp:Label>
                            <table class="nav-justified">
                                <tr>
                                    <td class="text-left" style="width: 137px">&nbsp;</td>
                                    <td class="text-left">&nbsp;</td>
                                    <td class="text-left">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="height: 43px; width: 137px; font-size: small; color: #336699;">
                                        <strong>
                                        <asp:Label ID="Label3" runat="server" Text="Tytuł: "></asp:Label>
                                        </strong>
                                    </td>
                                    <td class="text-left" style="height: 43px">
                                                    <asp:TextBox ID="ItemTitle" runat="server" Width="398px"></asp:TextBox>
                                    </td>
                                    <td class="text-left" style="height: 43px">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="height: 24px; width: 137px; font-size: small; color: #336699;"><strong>Zdjęcie:</strong></td>
                                    <td class="text-left" style="height: 24px">
                                        <asp:FileUpload ID="ImageFile" runat="server" Width="406px" />
                                    </td>
                                    <td class="text-left" style="height: 24px">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="height: 24px; width: 137px">&nbsp;</td>
                                    <td class="text-left" style="height: 24px">
                                        &nbsp;</td>
                                    <td class="text-left" style="height: 24px">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="height: 24px; width: 137px; color: #336699; font-size: small;"><strong>Ilość przedmtów:</strong></td>
                                    <td class="text-left" style="height: 24px">
                                        <asp:TextBox ID="ItemsNumber" runat="server" Width="123px"></asp:TextBox>
                                    </td>
                                    <td class="text-left" style="height: 24px">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="height: 24px; width: 137px">&nbsp;</td>
                                    <td class="text-left" style="height: 24px">
                                        &nbsp;</td>
                                    <td class="text-left" style="height: 24px">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="width: 137px; font-size: small; color: #336699;"><strong>Cena:</strong></td>
                                    <td class="text-left">
                                        <table style="width:100%;">
                                            <tr>
                                                <td style="width: 184px">
                                                    <asp:CheckBox ID="CheckBox_BuyItNow" runat="server" Text="Kup teraz" />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="CheckBox_Auction" runat="server" Text="Licytacja od" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 184px">
                                                    <asp:TextBox ID="BuyItNowPrice" runat="server"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="StartPrice" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="text-left">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="width: 137px">&nbsp;</td>
                                    <td class="text-left">
                                        &nbsp;</td>
                                    <td class="text-left">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="width: 137px; height: 24px; font-size: small; color: #336699;"><strong>Lokalizacja:</strong></td>
                                    <td class="text-left" style="height: 24px">
                                        <asp:TextBox ID="Location" runat="server" Width="175px"></asp:TextBox>
                                    </td>
                                    <td class="text-left" style="height: 24px"></td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="width: 137px">&nbsp;</td>
                                    <td class="text-left">
                                        &nbsp;</td>
                                    <td class="text-left">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="width: 137px; font-size: small; color: #336699; height: 24px;"><strong>Rodzaj przesyłki:</strong></td>
                                    <td class="text-left" style="height: 24px">
                                        <asp:DropDownList ID="ShipmentType" runat="server" Height="17px" Width="200px" DataSourceID="SqlDataSource1" DataTextField="Name" DataValueField="Id">
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PortalAukcyjnyConnectionString %>" SelectCommand="SELECT [Id], [Name], [Price] FROM [Shipments]"></asp:SqlDataSource>
                                    </td>
                                    <td class="text-left" style="height: 24px"></td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="width: 137px">&nbsp;</td>
                                    <td class="text-left">
                                        &nbsp;</td>
                                    <td class="text-left">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="width: 137px; font-size: small; color: #336699; height: 24px;"><strong>Kategoria:</strong></td>
                                    <td class="text-left" style="height: 24px">
                                        <asp:DropDownList ID="ItemCategory" runat="server" Height="17px" Width="200px" DataSourceID="SqlDataSource_Categories" DataTextField="Name" DataValueField="Id">
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlDataSource_Categories" runat="server" ConnectionString="<%$ ConnectionStrings:PortalAukcyjnyConnectionString %>" SelectCommand="SELECT [Name], [Id] FROM [Categories]"></asp:SqlDataSource>
                                    </td>
                                    <td class="text-left" style="height: 24px"></td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="width: 137px; height: 22px;"></td>
                                    <td class="text-left" style="height: 22px">
                                    </td>
                                    <td class="text-left" style="height: 22px"></td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="width: 137px">&nbsp;</td>
                                    <td class="text-left">
                                        &nbsp;</td>
                                    <td class="text-left">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="width: 137px"><span style="color: #336699; font-size: small;"><strong>Data zakończenia</strong></span><strong>:</strong></td>
                                    <td class="text-left">
                                        <asp:DropDownList ID="EndDate" runat="server" Height="17px" Width="200px">
                                            <asp:ListItem Value="1">1 dzień</asp:ListItem>
                                            <asp:ListItem Value="3">3 dni</asp:ListItem>
                                            <asp:ListItem Value="7">1 tydzień</asp:ListItem>
                                            <asp:ListItem Value="14">2 tygodnie</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="text-left">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="width: 137px">&nbsp;</td>
                                    <td class="text-left">&nbsp;</td>
                                    <td class="text-left">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="width: 137px; height: 22px;"></td>
                                    <td class="text-left" style="color: #336699; height: 22px;"></td>
                                    <td class="text-left" style="height: 22px"></td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="width: 137px; font-size: small; color: #336699;"><strong>Opis przedmiotu:</strong></td>
                                    <td class="text-left">
                                        <asp:TextBox ID="ItemDescription" runat="server" Height="300px" TextMode="MultiLine" Wrap="True" style="min-width: 600px"></asp:TextBox>
                                    </td>
                                    <td class="text-left">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="width: 137px; font-size: small; color: #336699;">&nbsp;</td>
                                    <td class="text-left">
                                        &nbsp;</td>
                                    <td class="text-left">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="width: 137px; font-size: small; color: #336699;">&nbsp;</td>
                                    <td class="text-left">
                                        <strong>
                                        <asp:Button ID="CreateAuctionBtn" runat="server" Height="60px" style="font-weight: bold" Text="Dodaj aukcje" Width="600px" OnClick="CreateAuctionBtn_Click" />
                                        </strong>
                                    </td>
                                    <td class="text-left">&nbsp;</td>
                                </tr>
                            </table>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <table style="width:100%;">
            <tr>
                <td style="height: 22px"></td>
            </tr>
        </table>
    </table>
</asp:Content>
