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
                                    <td class="text-left" style="width: 146px">&nbsp;</td>
                                    <td class="text-left">&nbsp;</td>
                                    <td class="text-left">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="height: 43px; width: 146px; font-size: small; color: #336699;">
                                        <strong>
                                        <asp:Label ID="Label3" runat="server" Text="Tytuł: "></asp:Label>
                                        </strong>
                                    </td>
                                    <td class="text-left" style="height: 43px">
                                                    <asp:TextBox ID="ItemTitle" runat="server" Width="398px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ItemTitle" ErrorMessage="Tutuł jest wymagany"></asp:RequiredFieldValidator>
                                    </td>
                                    <td class="text-left" style="height: 43px">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="height: 24px; width: 146px; font-size: small; color: #336699;"><strong>Zdjęcie:</strong></td>
                                    <td class="text-left" style="height: 24px">
                                        <asp:FileUpload ID="ImageFile" runat="server" Width="406px" />
                                    </td>
                                    <td class="text-left" style="height: 24px">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="height: 24px; width: 146px">&nbsp;</td>
                                    <td class="text-left" style="height: 24px">
                                        &nbsp;</td>
                                    <td class="text-left" style="height: 24px">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="height: 24px; width: 146px; color: #336699; font-size: small;"><strong>Ilość przedmtów:</strong></td>
                                    <td class="text-left" style="height: 24px">
                                        <asp:TextBox ID="ItemsNumber" runat="server" Width="123px" TextMode="Number">1</asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ItemsNumber" ErrorMessage="Podanie ilości przedmiotów jest wymagane"></asp:RequiredFieldValidator>
                                    </td>
                                    <td class="text-left" style="height: 24px">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="height: 24px; width: 146px">&nbsp;</td>
                                    <td class="text-left" style="height: 24px">
                                        &nbsp;</td>
                                    <td class="text-left" style="height: 24px">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="width: 146px; font-size: small; color: #336699;"><strong>Cena i rodzaj aukcji:</strong></td>
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
                                                    <asp:TextBox ID="BuyItNowPrice" runat="server" TextMode="Number"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="StartPrice" runat="server" TextMode="Number"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="text-left">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="width: 146px">&nbsp;</td>
                                    <td class="text-left">
                                        &nbsp;</td>
                                    <td class="text-left">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="width: 146px; height: 24px; font-size: small; color: #336699;"><strong>Lokalizacja:</strong></td>
                                    <td class="text-left" style="height: 24px">
                                        <asp:TextBox ID="Location" runat="server" Width="175px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="Location" ErrorMessage="Podanie lokalizacji jest wymagane"></asp:RequiredFieldValidator>
                                    </td>
                                    <td class="text-left" style="height: 24px"></td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="width: 146px">&nbsp;</td>
                                    <td class="text-left">
                                        &nbsp;</td>
                                    <td class="text-left">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="width: 146px; font-size: small; color: #336699; height: 24px;"><strong>Rodzaj przesyłki:</strong></td>
                                    <td class="text-left" style="height: 24px">
                                        <asp:DropDownList ID="ShipmentType" runat="server" Height="25px" Width="298px">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ShipmentType" ErrorMessage="Wybranie rodzaju przesyłki jest wymagane"></asp:RequiredFieldValidator>
                                    </td>
                                    <td class="text-left" style="height: 24px"></td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="width: 146px">&nbsp;</td>
                                    <td class="text-left">
                                        &nbsp;</td>
                                    <td class="text-left">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="width: 146px; font-size: small; color: #336699; height: 24px;"><strong>Kategoria:</strong></td>
                                    <td class="text-left" style="height: 24px">
                                        <asp:DropDownList ID="ItemCategory" runat="server" Height="25px" Width="298px">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ItemCategory" ErrorMessage="Wybranie kategorii jest wymagane"></asp:RequiredFieldValidator>
                                    </td>
                                    <td class="text-left" style="height: 24px"></td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="width: 146px; height: 22px;"></td>
                                    <td class="text-left" style="height: 22px">
                                    </td>
                                    <td class="text-left" style="height: 22px"></td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="width: 146px">&nbsp;</td>
                                    <td class="text-left">
                                        &nbsp;</td>
                                    <td class="text-left">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="width: 146px"><span style="color: #336699; font-size: small;"><strong>Czas trwania:</strong></span></td>
                                    <td class="text-left">
                                        <asp:DropDownList ID="EndDate" runat="server" Height="25px" Width="200px">
                                            <asp:ListItem Value="1">1 dzień</asp:ListItem>
                                            <asp:ListItem Value="3">3 dni</asp:ListItem>
                                            <asp:ListItem Value="7">1 tydzień</asp:ListItem>
                                            <asp:ListItem Value="14">2 tygodnie</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="EndDate" ErrorMessage="Wybranie czasu trwania aukcji jest wymagane"></asp:RequiredFieldValidator>
                                    </td>
                                    <td class="text-left">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="width: 146px">&nbsp;</td>
                                    <td class="text-left">&nbsp;</td>
                                    <td class="text-left">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="width: 146px; height: 22px;"></td>
                                    <td class="text-left" style="color: #336699; height: 22px;"></td>
                                    <td class="text-left" style="height: 22px"></td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="width: 146px; font-size: small; color: #336699;"><strong>Opis przedmiotu:</strong></td>
                                    <td class="text-left">
                                        <asp:TextBox ID="ItemDescription" runat="server" Height="300px" TextMode="MultiLine" Wrap="True" style="min-width: 600px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ItemDescription" ErrorMessage="Podanie opisu przedmiotu jest wymagane"></asp:RequiredFieldValidator>
                                    </td>
                                    <td class="text-left">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="width: 146px; font-size: small; color: #336699;">&nbsp;</td>
                                    <td class="text-left">
                                        &nbsp;</td>
                                    <td class="text-left">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="text-left" style="width: 146px; font-size: small; color: #336699;">&nbsp;</td>
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
