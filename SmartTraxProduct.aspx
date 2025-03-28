<%@ Page Language="VB" EnableEventValidation="false" AutoEventWireup="false" CodeFile="SmartTraxProduct.aspx.vb" Inherits="SmartTraxProduct" %>
<%@ Register Assembly="AjaxControlToolkit" TagPrefix="act" Namespace="AjaxControlToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>SmartTrax Products</title>
        <style type="text/css">
            .mouseover { CURSOR: pointer; BACKGROUND-COLOR: #ebf3f6 }
	        .mouseout { CURSOR: pointer; BACKGROUND-COLOR: #ffffff }
	        H1 { MARGIN-TOP: 35px }
	        .gridHeader { FONT-WEIGHT: bolder; COLOR: white; BACKGROUND-COLOR: #86adce; text-align: left }
	        .gridHeader A { COLOR: white }
	        .gridHeader2 { FONT-WEIGHT: bolder; COLOR: white; BACKGROUND-COLOR: #86adce; text-align: center }
	        .gridHeader2 A { COLOR: white }
            .prodID, .prodPT, .prodDT { text-align: center; }
            .prodPLU, .prodUP, .prodType { text-align: left; }
            .bottomNav { background-color: #92ADD8; font-weight: bold; text-align: center; }
	        .box H4 { PADDING-RIGHT: 5px; PADDING-LEFT: 5px; MARGIN-BOTTOM: 10px; PADDING-BOTTOM: 5px; PADDING-TOP: 5px; BACKGROUND-COLOR: #ebf3f6 }
	        #labelDesc { BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid; WIDTH: 153px; BORDER-BOTTOM: black 1px solid }
	        .clickedRow { BACKGROUND-COLOR: #3fa84b }
            .odd { background: #f0f2f4; }
            h2 { font-family: Verdana, Geneva, sans-serif; }
            a { font-family: Verdana, Geneva, sans-serif; }
            body { font-family: Verdana, Geneva, sans-serif; color: #000; }
            #dgProducts tr .gridheader {background: #3A4856; padding: 15px 10px; color: #fff; text-align: left; font-weight: normal; text-transform: uppercase;}
            #dgProducts tr .gridheader a { color: #FFF; }
            #dgProducts { font-size: 12px; border-top-style: none; border-right-style: none; border-bottom-style: none; border-left-style: none; border-top-width: 0px; border-right-width: 0px; border-bottom-width: 0px; border-left-width: 0px; }
            #dgProducts .odd td, #dgProducts td { padding: 5px 2px; border:none; }
            .topEntry tr td .topEntry { font-size: 14px; margin: 0px; padding: 0px; border: 1px double #3A4856; }
            .editProduct { border: 1px double 92ADD8; }
            .topEntry { border: 1px solid #3A4856; margin-top: 10px; margin-right: 20px; margin-bottom: 20px; margin-left: 20px; padding-top: 10px; padding-right: 20px; padding-bottom: 20px; padding-left: 20px; text-align: center; }
            .topEntry tr td .editProduct .editEntry { font-size: 14px; text-align: right; padding: 0px; margin: 5px; }
            .topEntry tr td .editProduct .editEntry tr .col2 { text-align: left; }
            .topEntry tr td .editProduct .editEntry tr .col1 { font-weight: normal; color: #3A4856; }
            select { background-color: #92ADD8; }
            .modalPopup { background-color:#86adce; border-width:3px; border-style:solid; border-color:Gray; padding:3px; width:500px; }
            .modalPopupTitleBar { background-color:#3A4856; border-width:3px; border-style:solid; border-color:Gray; padding:3px; width:488px; color:#FFFFFF; }
            .modalBackground { background-color:Gray; filter:alpha(opacity=70); opacity:0.7; }    
	    </style>
        <script language="javascript" type="text/javascript">
            function validateData() {
                if (document.getElementById("ddlProduct").selectedIndex < 1) {
                    alert("Missing Required Information: Product");
                    return false;
                }

                if (document.getElementById("ddlProductType").selectedIndex < 1) {
                    alert("Missing Required Information: Product Type");
                    return false;
                }

                if (document.getElementById("ddlUpgradeProduct") != null) {
                    if (document.getElementById("ddlUpgradeProduct").selectedIndex < 1) {
                        alert("Missing Required Information: Upgrade Product");
                        return false;
                    }
                }

                if (document.getElementById("ddlPrepTime").selectedIndex < 1) {
                    alert("Missing Required Information: Preparation Time");
                    return false;
                }

                if (document.getElementById("ddlCookTime").selectedIndex < 1) {
                    alert("Missing Required Information: Cooking Time");
                    return false;
                }

                if (document.getElementById("ddlHoldTime").selectedIndex < 1) {
                    alert("Missing Required Information: Hold Time");
                    return false;
                }

                return true;
            }

            function ok(sender, e) {
                if (validateData()) {
                    $find('mpeEditProduct').hide();
                    __doPostBack('btnEditProductOK', e);
                }
                else {
                    $find('mpeEditProduct').show();
                }
            }

            function cancel(sender, e) {
                $find('mpeEditProduct').hide();
            }
        </script>
    </head>

    <body>
        <form id="form1" runat="server">
            <asp:ScriptManager ID="tsmMain" runat="server" />
            <input id="hideStore" type="hidden" name="hideStore" runat="server" /> 
            <div>
                <h2>SmartTrax Products</h2>
                <h4><asp:Label ID="lblStoreDisplay" runat="server">Store 8888</asp:Label></h4>
                <asp:Label ID="lblErrors" runat="server" ForeColor="Red"></asp:Label>
                <div id="divProducts" runat="server" style="width:100%;height:400px;overflow:auto;border:black 1px solid;">
                    <asp:datagrid id="dgProducts" runat="server" AutoGenerateColumns="False" CellPadding="3" Width="100%">
                        <Columns>
                            <asp:BoundColumn DataField="ProductID" SortExpression="ProductID" HeaderText="Product ID">
                                <HeaderStyle CssClass="gridHeader2" />
                                <ItemStyle CssClass="prodID" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="PLU" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PLUDesc" SortExpression="PLU" HeaderText="Product">
                                <HeaderStyle CssClass="gridHeader" />
                                <ItemStyle CssClass="prodPLU" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TypeID" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Type" HeaderText="Type">
                                <HeaderStyle CssClass="gridHeader" />
                                <ItemStyle CssClass="prodType" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="UpgradePLU" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="UpgradePLUDesc" HeaderText="Upgrade Product">
                                <HeaderStyle CssClass="gridHeader" />
                                <ItemStyle CssClass="prodUP" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="PrepTime" HeaderText="Prep<br />Time">
                                <HeaderStyle CssClass="gridHeader2" />
                                <ItemStyle CssClass="prodPT" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="CookTime" HeaderText="Cook<br />Time">
                                <HeaderStyle CssClass="gridHeader2" />
                                <ItemStyle CssClass="prodPT" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DecayTime" HeaderText="Hold<br />Time">
                                <HeaderStyle CssClass="gridHeader2" />
                                <ItemStyle CssClass="prodDT" />
                            </asp:BoundColumn>
                            <asp:ButtonColumn ButtonType="PushButton" CommandName="Edit" Text="Edit" ItemStyle-Width="5%">
                                <HeaderStyle CssClass="gridHeader" />
                                <ItemStyle CssClass="prodDT" />
                            </asp:ButtonColumn>
                            <asp:ButtonColumn ButtonType="PushButton" CommandName="Delete" Text="Delete" ItemStyle-Width="5%">
                                <HeaderStyle CssClass="gridHeader" />
                                <ItemStyle CssClass="prodDT" />
                            </asp:ButtonColumn>
                        </Columns>
                        <AlternatingItemStyle CssClass="odd" />
                        <PagerStyle CssClass="bottomNav" />
                    </asp:datagrid>
                </div>
                <br /><center><asp:Button ID="btnAddNew" runat="server" Text="Add New" /></center>
                <asp:Button runat="server" ID="hiddenTargetControlForModalPopup" style="display:none"/>
            </div>

            <asp:Panel ID="pnlEditProduct" runat="server" CssClass="modalPopup" style="display:none">
                <asp:Panel ID="pnlTitleBar" runat="server" CssClass="modalPopupTitleBar">
                    <asp:Literal ID="litModalTitle" runat="server" Text="Edit Product"></asp:Literal>
                </asp:Panel>
                <div style="margin:10px">
                    <asp:UpdatePanel runat="server" ID="upnEditProduct" RenderMode="Inline" UpdateMode="Conditional"> 
                        <ContentTemplate>
                            <input type="hidden" id="hideEditProduct" runat="server" />
                            <table runat="server">
                                <tr>
                                    <td><b>Product:</b></td>
                                    <td>
                                        <asp:DropDownList runat="server" id="ddlProduct" Width="300px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td><b>Product Type:</b></td>
                                    <td>
                                        <asp:DropDownList runat="server" id="ddlProductType" Width="300px" AutoPostBack="true" />
                                    </td>
                                </tr>
                                <tr id="trUpgradeProduct">
                                    <td><b>Upgrade Product:</b></td>
                                    <td>
                                        <asp:DropDownList runat="server" id="ddlUpgradeProduct" Width="300px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td><b>Prep Time:</b></td>
                                    <td>
                                        <asp:DropDownList runat="server" id="ddlPrepTime" Width="300px">
                                            <asp:ListItem Value="-1">[select]</asp:ListItem>
                                            <asp:ListItem Value="0">None</asp:ListItem>
                                            <asp:ListItem Value="5">5 minutes</asp:ListItem>
                                            <asp:ListItem Value="10">10 minutes</asp:ListItem>
                                            <asp:ListItem Value="15">15 minutes</asp:ListItem>
                                            <asp:ListItem Value="20">20 minutes</asp:ListItem>
                                            <asp:ListItem Value="25">25 minutes</asp:ListItem>
                                            <asp:ListItem Value="30">30 minutes</asp:ListItem>
                                            <asp:ListItem Value="35">35 minutes</asp:ListItem>
                                            <asp:ListItem Value="40">40 minutes</asp:ListItem>
                                            <asp:ListItem Value="45">45 minutes</asp:ListItem>
                                            <asp:ListItem Value="50">50 minutes</asp:ListItem>
                                            <asp:ListItem Value="55">55 minutes</asp:ListItem>
                                            <asp:ListItem Value="60">60 minutes</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td><b>Cook Time:</b></td>
                                    <td>
                                        <asp:DropDownList runat="server" id="ddlCookTime" Width="300px">
                                            <asp:ListItem Value="-1">[select]</asp:ListItem>
                                            <asp:ListItem Value="0">None</asp:ListItem>
                                            <asp:ListItem Value="5">5 minutes</asp:ListItem>
                                            <asp:ListItem Value="10">10 minutes</asp:ListItem>
                                            <asp:ListItem Value="15">15 minutes</asp:ListItem>
                                            <asp:ListItem Value="20">20 minutes</asp:ListItem>
                                            <asp:ListItem Value="25">25 minutes</asp:ListItem>
                                            <asp:ListItem Value="30">30 minutes</asp:ListItem>
                                            <asp:ListItem Value="35">35 minutes</asp:ListItem>
                                            <asp:ListItem Value="40">40 minutes</asp:ListItem>
                                            <asp:ListItem Value="45">45 minutes</asp:ListItem>
                                            <asp:ListItem Value="50">50 minutes</asp:ListItem>
                                            <asp:ListItem Value="55">55 minutes</asp:ListItem>
                                            <asp:ListItem Value="60">60 minutes</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td><b>Hold Time:</b></td>
                                    <td>
                                        <asp:DropDownList runat="server" id="ddlHoldTime" Width="300px">
                                            <asp:ListItem Value="-1">[select]</asp:ListItem>
                                            <asp:ListItem Value="5">5 minutes</asp:ListItem>
                                            <asp:ListItem Value="10">10 minutes</asp:ListItem>
                                            <asp:ListItem Value="15">15 minutes</asp:ListItem>
                                            <asp:ListItem Value="20">20 minutes</asp:ListItem>
                                            <asp:ListItem Value="25">25 minutes</asp:ListItem>
                                            <asp:ListItem Value="30">30 minutes</asp:ListItem>
                                            <asp:ListItem Value="35">35 minutes</asp:ListItem>
                                            <asp:ListItem Value="40">40 minutes</asp:ListItem>
                                            <asp:ListItem Value="45">45 minutes</asp:ListItem>
                                            <asp:ListItem Value="50">50 minutes</asp:ListItem>
                                            <asp:ListItem Value="55">55 minutes</asp:ListItem>
                                            <asp:ListItem Value="60">60 minutes</asp:ListItem>
                                            <asp:ListItem Value="65">65 minutes</asp:ListItem>
                                            <asp:ListItem Value="70">70 minutes</asp:ListItem>
                                            <asp:ListItem Value="75">75 minutes</asp:ListItem>
                                            <asp:ListItem Value="80">80 minutes</asp:ListItem>
                                            <asp:ListItem Value="85">85 minutes</asp:ListItem>
                                            <asp:ListItem Value="90">90 minutes</asp:ListItem>
                                            <asp:ListItem Value="95">95 minutes</asp:ListItem>
                                            <asp:ListItem Value="100">100 minutes</asp:ListItem>
                                            <asp:ListItem Value="105">105 minutes</asp:ListItem>
                                            <asp:ListItem Value="110">110 minutes</asp:ListItem>
                                            <asp:ListItem Value="115">115 minutes</asp:ListItem>
                                            <asp:ListItem Value="120">120 minutes</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>   
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <br />
                    <center>
                        <asp:Button ID="btnEditProductOK" runat="server" Text="OK" width="100px" />
                        <asp:Button ID="btnEditProductCancel" runat="server" Text="Cancel" width="100px"  />
                    </center>
                </div>
            </asp:Panel>
        
            <act:ModalPopupExtender ID="mpeEditProduct" runat="server" TargetControlID="hiddenTargetControlForModalPopup" PopupControlID="pnlEditProduct" BackgroundCssClass="modalBackground" OkControlID="btnEditProductOK" OnOkScript="ok()" CancelControlID="btnEditProductCancel" OnCancelScript="cancel()" DropShadow="true" PopupDragHandleControlID="pnlTitleBar" /> 
        </form>
    </body>
</html>
