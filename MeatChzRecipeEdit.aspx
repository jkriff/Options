<%@ Page Language="C#" MasterPageFile="~/MasterPageAjax.master" AutoEventWireup="true"
    CodeFile="MeatChzRecipeEdit.aspx.cs" Inherits="MeatChzRecipeEdit" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Meat & Cheese Recipe Editor</title>
    <link rel="Stylesheet" type="text/css" href="meatChz.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table class="recipeEdit">
        <tr>
            <td>
                <asp:SqlDataSource ID="dsPLU" runat="server" />
                <asp:SqlDataSource ID="dsRecipe" runat="server">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="cbPLU" DefaultValue="0" Name="PLU" PropertyName="SelectedValue"
                            Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="dsMeatChz" runat="server" />
                <asp:ComboBox ID="cbPLU" runat="server" AutoPostBack="true" DropDownStyle="DropDown"
                    AutoCompleteMode="SuggestAppend" CaseSensitive="false" ItemInsertLocation="Append"
                    DataSourceID="dsPLU" DataTextField="plu" OnDataBound="cbPLU_DataBound" 
                    oniteminserting="cbPLU_ItemInserting" />
                <asp:DropDownList ID="ddltest" runat="server" DataSourceID="dsMeatChz" DataTextField="MeatChzDescrip"
                    DataValueField="MeatChzId" AppendDataBoundItems="true" />
                <asp:Button ID="clickMe" runat="server" Text="Button" />
                <asp:ModalPopupExtender ID="mdlConfirm" runat="server" TargetControlID="clickMe" PopupControlID="popPanelPLU" DropShadow="true" OkControlID="btnOk" CancelControlID="btnCancel">
                </asp:ModalPopupExtender>
                <asp:Panel ID="popPanelPLU" runat="server" style="display:none; background-color:white; border:solid 2px black; width:200px;height:100px;" SkinID="PopUpPanel">
                    <asp:Label ID="lblPopupMessage" runat="server" Text=""></asp:Label><br />
                    <asp:Button ID="btnOk" runat="server" Text="Ok" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
                </asp:Panel>
                <asp:ListView ID="lvRecipe" runat="server" DataSourceID="dsRecipe" DataKeyNames="MeatChzID"
                    InsertItemPosition="LastItem"
                    OnItemInserting="lvRecipe_ItemInserting">
                    <LayoutTemplate>
                        <table class="ingredients">
                            <tr>
                                <thead>
                                    <th>
                                        Description
                                    </th>
                                    <th>
                                        Amount
                                    </th>
                                </thead>
                            </tr>
                            <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:TextBox ID="tbDescriptionId" runat="server" Visible="false" Text='<%#Bind("MeatChzID") %>'
                                    Columns="5"></asp:TextBox>
                                <asp:DropDownList ID="ddlDescription" runat="server" DataSourceID="dsMeatChz" DataTextField="MeatChzDescrip"
                                    DataValueField="MeatChzId" AppendDataBoundItems="true" />
                            </td>
                            <td>
                                <asp:TextBox ID="tbAmount" runat="server" Text='<%#Bind("Amount") %>' Columns="5"></asp:TextBox>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <InsertItemTemplate>
                        <tr>
                            <td>
                                <asp:ComboBox ID="cbDescription" runat="server" AutoPostBack="true" DropDownStyle="DropDown"
                                    AutoCompleteMode="SuggestAppend" CaseSensitive="false" ItemInsertLocation="Append"
                                    DataSourceID="dsMeatChz" DataTextField="MeatChzDescrip" DataValueField="MeatChzID" />
                            </td>
                            <td>
                                <asp:TextBox ID="tbAmount" runat="server" Text='<%#Bind("Amount") %>' Columns="5"></asp:TextBox>
                            </td>
                        </tr>
                    </InsertItemTemplate>
                    <EmptyDataTemplate>
                        <div>
                            Add your recipe</div>
                    </EmptyDataTemplate>
                </asp:ListView>
            </td>
        </tr>
    </table>
</asp:Content>
