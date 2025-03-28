<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageAjax.master" AutoEventWireup="true"
    CodeFile="ListingCreditCard.aspx.cs" Inherits="ListingCreditCard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="Stylesheet" type="text/css" href="/default/current/Style/MasterData.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:TextBox runat="server" ID="tbStoreId" Visible="false" />
    <asp:TextBox runat="server" ID="tbStartDate" Visible="false" />
    <asp:TextBox runat="server" ID="tbEndDate" Visible="false" />
    
    <table width="100%" style="text-align: center;">
        <tr>
            <td>
                <table style="width: 650px;text-align:left;">
                    <tr>
                        <td>
                            <asp:Image runat="server" ImageUrl="/images/Logo.jpg" />
                        </td>
                        <td>
                            <h4>
                                Credit Card Listing</h4>
                        </td>
                        <td style="font-size: small;">
                            <b>Store #<asp:Label ID="lblStore" runat="server" /></b> -
                            <asp:Label ID="lblStoreName" runat="server"></asp:Label><br />
                            <b>Dates:</b>
                            <asp:Label ID="lblDates" runat="server" /><br />
                            <br />
                            <b>As of:</b>
                            <asp:Label ID="lblAsOf" runat="server" />
                        </td>
                    </tr>
                </table>
                <asp:GridView ID="gvListing" runat="server" AutoGenerateColumns="False"
                    BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                    CellPadding="3" GridLines="None" Width="600px" ShowFooter="true">
                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" Font-Size="Small" />
                    <Columns>
                        <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" ItemStyle-Width="150px"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemStyle HorizontalAlign="Center" Width="200px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Visa" HeaderText="Visa" SortExpression="Visa" ReadOnly="True"
                            DataFormatString="{0:c}" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Right">
                            <ItemStyle HorizontalAlign="Right" Width="100px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Mastercard" HeaderText="Mastercard" SortExpression="Mastercard"
                            ReadOnly="True" DataFormatString="{0:c}" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Right">
                            <ItemStyle HorizontalAlign="Right" Width="100px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Discover" HeaderText="Discover" SortExpression="Discover"
                            ReadOnly="True" DataFormatString="{0:c}" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Right">
                            <ItemStyle HorizontalAlign="Right" Width="100px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="AMEX" HeaderText="American Express" SortExpression="AMEX"
                            ReadOnly="True" DataFormatString="{0:c}" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Right">
                            <ItemStyle HorizontalAlign="Right" Width="100px"></ItemStyle>
                        </asp:BoundField>
                    </Columns>
                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" Font-Size="Small" />
                    <EditRowStyle Font-Size="Small" />
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
