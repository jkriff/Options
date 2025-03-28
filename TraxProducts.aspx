<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TraxProducts.aspx.vb" Inherits="TraxProducts" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
    <head>
        <title>SmartTrax Product Definition</title>
        <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
        <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
        <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE" />
        <meta content="JavaScript" name="vs_defaultClientScript" />
        <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
        <style type="text/css">
            .mouseover { CURSOR: pointer; BACKGROUND-COLOR: #ebf3f6 }
	        .mouseout { CURSOR: pointer; BACKGROUND-COLOR: #ffffff }
	        H1 { MARGIN-TOP: 35px }
	        .gridHeader { FONT-WEIGHT: bolder; COLOR: white; BACKGROUND-COLOR: #86adce }
	        .gridHeader A { COLOR: white }
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
	    </style>

        <script type="text/javascript" language="javascript">
		    <!--
		    var clickedRow;
		    function loadItems(row, productID, plu, upgrade, prepTime, decayTime)
		        {
			    try
			        {
				    clickedRow.className = clickedRow.oldClass;
    			    }
			    catch(ex){}
			    
			    row.className = "clickedRow";
			    clickedRow = row;
			    
			    var hProductID = document.getElementById("hideProductID");
			    var oPlu = document.getElementById("ddlPLU");
			    var oUpgrade = document.getElementById("ddlUpgradePLU");
			    var oPrepTime = document.getElementById("ddlPrepTime");
			    var oDecayTime = document.getElementById("ddlDecayTime");
    			
			    hProductID.value = productID;
			    oPlu.value = plu;
			    oUpgrade.value = upgrade;
			    oPrepTime.value = prepTime;
			    oDecayTime.value = decayTime;
	    	    }
    	    	
		    function mouseover(row)
		        {
			    if (row.className != "clickedRow")
			        {
			        row.oldClass = row.className;
				    row.className = "mouseover";
				    }
		        }
		    function mouseout(row)
		        {
			    if (row.className != "clickedRow")
				    {
				    row.className = row.oldClass;
				    }
		        }
		    //-->
        </script>
    </head>
    <body>
        <form id="frmMain" method="post" runat="server">
            <asp:ScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ScriptManager>
            <input id="hideStore" type="hidden" name="hideStore" runat="server" /> 
            <input id="hideProductID" type="hidden" name="hideProductID" runat="server" /> 
            <div align="center">
                <table width="100%">
                    <tr>
                        <td><h2>SmartTrax Product Definition</h2></td>
                        <td><h4 style="text-align:right"><asp:label id="lblStoreDisplay" Runat="server" /></h4></td>
                    </tr>
                </table>
                <p>&nbsp;</p>
                <table class="topEntry">
                    <tr>
                        <td>
                            <h3>Edit Product:</h3>
                            <table class="editEntry" width="100%">
                                <tr>
                                    <td class="col1">Product&nbsp;&nbsp;</td>
                                    <td class="col2"><asp:DropDownList id="ddlPLU" runat="server"></asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td class="col1">Type&nbsp;&nbsp;</td>
                                    <td class="col2">
                                        <asp:DropDownList id="DropDownList1" runat="server">
                                            <asp:ListItem Selected="True">New Item</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="col1">Upgrade Product&nbsp;&nbsp;</td>
                                    <td class="col2"><asp:DropDownList id="ddlUpgradePLU" runat="server" Enabled="false"></asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td class="col1">Average Preparation Time&nbsp;&nbsp;</td>
                                    <td class="col2">
                                        <asp:DropDownList id="ddlPrepTime" runat="server">
                                            <asp:ListItem Value="0">0 minutes</asp:ListItem>
                                            <asp:ListItem Value="1">1 minute</asp:ListItem>
                                            <asp:ListItem Value="2">2 minutes</asp:ListItem>
                                            <asp:ListItem Value="3">3 minutes</asp:ListItem>
                                            <asp:ListItem Value="4">4 minutes</asp:ListItem>
                                            <asp:ListItem Value="5" Selected="True">5 minutes</asp:ListItem>
                                            <asp:ListItem Value="6">6 minutes</asp:ListItem>
                                            <asp:ListItem Value="7">7 minutes</asp:ListItem>
                                            <asp:ListItem Value="8">8 minutes</asp:ListItem>
                                            <asp:ListItem Value="9">9 minutes</asp:ListItem>
                                            <asp:ListItem Value="10">10 minutes</asp:ListItem>
                                            <asp:ListItem Value="11">11 minutes</asp:ListItem>
                                            <asp:ListItem Value="12">12 minutes</asp:ListItem>
                                            <asp:ListItem Value="13">13 minutes</asp:ListItem>
                                            <asp:ListItem Value="14">14 minutes</asp:ListItem>
                                            <asp:ListItem Value="15">15 minutes</asp:ListItem>
                                            <asp:ListItem Value="16">16 minutes</asp:ListItem>
                                            <asp:ListItem Value="17">17 minutes</asp:ListItem>
                                            <asp:ListItem Value="18">18 minutes</asp:ListItem>
                                            <asp:ListItem Value="19">19 minutes</asp:ListItem>
                                            <asp:ListItem Value="20">20 minutes</asp:ListItem>
                                            <asp:ListItem Value="21">21 minutes</asp:ListItem>
                                            <asp:ListItem Value="22">22 minutes</asp:ListItem>
                                            <asp:ListItem Value="23">23 minutes</asp:ListItem>
                                            <asp:ListItem Value="24">24 minutes</asp:ListItem>
                                            <asp:ListItem Value="25">25 minutes</asp:ListItem>
                                            <asp:ListItem Value="26">26 minutes</asp:ListItem>
                                            <asp:ListItem Value="27">27 minutes</asp:ListItem>
                                            <asp:ListItem Value="28">28 minutes</asp:ListItem>
                                            <asp:ListItem Value="29">29 minutes</asp:ListItem>
                                            <asp:ListItem Value="30">30 minutes</asp:ListItem>
                                            <asp:ListItem Value="31">31 minutes</asp:ListItem>
                                            <asp:ListItem Value="32">32 minutes</asp:ListItem>
                                            <asp:ListItem Value="33">33 minutes</asp:ListItem>
                                            <asp:ListItem Value="34">34 minutes</asp:ListItem>
                                            <asp:ListItem Value="35">35 minutes</asp:ListItem>
                                            <asp:ListItem Value="36">36 minutes</asp:ListItem>
                                            <asp:ListItem Value="37">37 minutes</asp:ListItem>
                                            <asp:ListItem Value="38">38 minutes</asp:ListItem>
                                            <asp:ListItem Value="39">39 minutes</asp:ListItem>
                                            <asp:ListItem Value="40">40 minutes</asp:ListItem>
                                            <asp:ListItem Value="41">41 minutes</asp:ListItem>
                                            <asp:ListItem Value="42">42 minutes</asp:ListItem>
                                            <asp:ListItem Value="43">43 minutes</asp:ListItem>
                                            <asp:ListItem Value="44">44 minutes</asp:ListItem>
                                            <asp:ListItem Value="45">45 minutes</asp:ListItem>
                                            <asp:ListItem Value="46">46 minutes</asp:ListItem>
                                            <asp:ListItem Value="47">47 minutes</asp:ListItem>
                                            <asp:ListItem Value="48">48 minutes</asp:ListItem>
                                            <asp:ListItem Value="49">49 minutes</asp:ListItem>
                                            <asp:ListItem Value="50">50 minutes</asp:ListItem>
                                            <asp:ListItem Value="51">51 minutes</asp:ListItem>
                                            <asp:ListItem Value="52">52 minutes</asp:ListItem>
                                            <asp:ListItem Value="53">53 minutes</asp:ListItem>
                                            <asp:ListItem Value="54">54 minutes</asp:ListItem>
                                            <asp:ListItem Value="55">55 minutes</asp:ListItem>
                                            <asp:ListItem Value="56">56 minutes</asp:ListItem>
                                            <asp:ListItem Value="57">57 minutes</asp:ListItem>
                                            <asp:ListItem Value="58">58 minutes</asp:ListItem>
                                            <asp:ListItem Value="59">59 minutes</asp:ListItem>
                                            <asp:ListItem Value="60">60 minutes</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="col1">Average Cooking Time&nbsp;&nbsp;</td>
                                    <td class="col2">
                                        <asp:DropDownList id="DropDownList2" runat="server">
                                            <asp:ListItem Value="0">0 minutes</asp:ListItem>
                                            <asp:ListItem Value="1">1 minute</asp:ListItem>
                                            <asp:ListItem Value="2">2 minutes</asp:ListItem>
                                            <asp:ListItem Value="3">3 minutes</asp:ListItem>
                                            <asp:ListItem Value="4">4 minutes</asp:ListItem>
                                            <asp:ListItem Value="5">5 minutes</asp:ListItem>
                                            <asp:ListItem Value="6">6 minutes</asp:ListItem>
                                            <asp:ListItem Value="7">7 minutes</asp:ListItem>
                                            <asp:ListItem Value="8">8 minutes</asp:ListItem>
                                            <asp:ListItem Value="9">9 minutes</asp:ListItem>
                                            <asp:ListItem Value="10" Selected="True">10 minutes</asp:ListItem>
                                            <asp:ListItem Value="11">11 minutes</asp:ListItem>
                                            <asp:ListItem Value="12">12 minutes</asp:ListItem>
                                            <asp:ListItem Value="13">13 minutes</asp:ListItem>
                                            <asp:ListItem Value="14">14 minutes</asp:ListItem>
                                            <asp:ListItem Value="15">15 minutes</asp:ListItem>
                                            <asp:ListItem Value="16">16 minutes</asp:ListItem>
                                            <asp:ListItem Value="17">17 minutes</asp:ListItem>
                                            <asp:ListItem Value="18">18 minutes</asp:ListItem>
                                            <asp:ListItem Value="19">19 minutes</asp:ListItem>
                                            <asp:ListItem Value="20">20 minutes</asp:ListItem>
                                            <asp:ListItem Value="21">21 minutes</asp:ListItem>
                                            <asp:ListItem Value="22">22 minutes</asp:ListItem>
                                            <asp:ListItem Value="23">23 minutes</asp:ListItem>
                                            <asp:ListItem Value="24">24 minutes</asp:ListItem>
                                            <asp:ListItem Value="25">25 minutes</asp:ListItem>
                                            <asp:ListItem Value="26">26 minutes</asp:ListItem>
                                            <asp:ListItem Value="27">27 minutes</asp:ListItem>
                                            <asp:ListItem Value="28">28 minutes</asp:ListItem>
                                            <asp:ListItem Value="29">29 minutes</asp:ListItem>
                                            <asp:ListItem Value="30">30 minutes</asp:ListItem>
                                            <asp:ListItem Value="31">31 minutes</asp:ListItem>
                                            <asp:ListItem Value="32">32 minutes</asp:ListItem>
                                            <asp:ListItem Value="33">33 minutes</asp:ListItem>
                                            <asp:ListItem Value="34">34 minutes</asp:ListItem>
                                            <asp:ListItem Value="35">35 minutes</asp:ListItem>
                                            <asp:ListItem Value="36">36 minutes</asp:ListItem>
                                            <asp:ListItem Value="37">37 minutes</asp:ListItem>
                                            <asp:ListItem Value="38">38 minutes</asp:ListItem>
                                            <asp:ListItem Value="39">39 minutes</asp:ListItem>
                                            <asp:ListItem Value="40">40 minutes</asp:ListItem>
                                            <asp:ListItem Value="41">41 minutes</asp:ListItem>
                                            <asp:ListItem Value="42">42 minutes</asp:ListItem>
                                            <asp:ListItem Value="43">43 minutes</asp:ListItem>
                                            <asp:ListItem Value="44">44 minutes</asp:ListItem>
                                            <asp:ListItem Value="45">45 minutes</asp:ListItem>
                                            <asp:ListItem Value="46">46 minutes</asp:ListItem>
                                            <asp:ListItem Value="47">47 minutes</asp:ListItem>
                                            <asp:ListItem Value="48">48 minutes</asp:ListItem>
                                            <asp:ListItem Value="49">49 minutes</asp:ListItem>
                                            <asp:ListItem Value="50">50 minutes</asp:ListItem>
                                            <asp:ListItem Value="51">51 minutes</asp:ListItem>
                                            <asp:ListItem Value="52">52 minutes</asp:ListItem>
                                            <asp:ListItem Value="53">53 minutes</asp:ListItem>
                                            <asp:ListItem Value="54">54 minutes</asp:ListItem>
                                            <asp:ListItem Value="55">55 minutes</asp:ListItem>
                                            <asp:ListItem Value="56">56 minutes</asp:ListItem>
                                            <asp:ListItem Value="57">57 minutes</asp:ListItem>
                                            <asp:ListItem Value="58">58 minutes</asp:ListItem>
                                            <asp:ListItem Value="59">59 minutes</asp:ListItem>
                                            <asp:ListItem Value="60">60 minutes</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="col1">Hold Time&nbsp;&nbsp;</td>
                                    <td class="col2">
                                        <asp:DropDownList id="ddlDecayTime" runat="server">
                                            <asp:ListItem Value="0">0 minutes</asp:ListItem>
                                            <asp:ListItem Value="1">1 minute</asp:ListItem>
                                            <asp:ListItem Value="2">2 minutes</asp:ListItem>
                                            <asp:ListItem Value="3">3 minutes</asp:ListItem>
                                            <asp:ListItem Value="4">4 minutes</asp:ListItem>
                                            <asp:ListItem Value="5">5 minutes</asp:ListItem>
                                            <asp:ListItem Value="6">6 minutes</asp:ListItem>
                                            <asp:ListItem Value="7">7 minutes</asp:ListItem>
                                            <asp:ListItem Value="8">8 minutes</asp:ListItem>
                                            <asp:ListItem Value="9">9 minutes</asp:ListItem>
                                            <asp:ListItem Value="10">10 minutes</asp:ListItem>
                                            <asp:ListItem Value="11">11 minutes</asp:ListItem>
                                            <asp:ListItem Value="12">12 minutes</asp:ListItem>
                                            <asp:ListItem Value="13">13 minutes</asp:ListItem>
                                            <asp:ListItem Value="14">14 minutes</asp:ListItem>
                                            <asp:ListItem Value="15" Selected="True">15 minutes</asp:ListItem>
                                            <asp:ListItem Value="16">16 minutes</asp:ListItem>
                                            <asp:ListItem Value="17">17 minutes</asp:ListItem>
                                            <asp:ListItem Value="18">18 minutes</asp:ListItem>
                                            <asp:ListItem Value="19">19 minutes</asp:ListItem>
                                            <asp:ListItem Value="20">20 minutes</asp:ListItem>
                                            <asp:ListItem Value="21">21 minutes</asp:ListItem>
                                            <asp:ListItem Value="22">22 minutes</asp:ListItem>
                                            <asp:ListItem Value="23">23 minutes</asp:ListItem>
                                            <asp:ListItem Value="24">24 minutes</asp:ListItem>
                                            <asp:ListItem Value="25">25 minutes</asp:ListItem>
                                            <asp:ListItem Value="26">26 minutes</asp:ListItem>
                                            <asp:ListItem Value="27">27 minutes</asp:ListItem>
                                            <asp:ListItem Value="28">28 minutes</asp:ListItem>
                                            <asp:ListItem Value="29">29 minutes</asp:ListItem>
                                            <asp:ListItem Value="30">30 minutes</asp:ListItem>
                                            <asp:ListItem Value="31">31 minutes</asp:ListItem>
                                            <asp:ListItem Value="32">32 minutes</asp:ListItem>
                                            <asp:ListItem Value="33">33 minutes</asp:ListItem>
                                            <asp:ListItem Value="34">34 minutes</asp:ListItem>
                                            <asp:ListItem Value="35">35 minutes</asp:ListItem>
                                            <asp:ListItem Value="36">36 minutes</asp:ListItem>
                                            <asp:ListItem Value="37">37 minutes</asp:ListItem>
                                            <asp:ListItem Value="38">38 minutes</asp:ListItem>
                                            <asp:ListItem Value="39">39 minutes</asp:ListItem>
                                            <asp:ListItem Value="40">40 minutes</asp:ListItem>
                                            <asp:ListItem Value="41">41 minutes</asp:ListItem>
                                            <asp:ListItem Value="42">42 minutes</asp:ListItem>
                                            <asp:ListItem Value="43">43 minutes</asp:ListItem>
                                            <asp:ListItem Value="44">44 minutes</asp:ListItem>
                                            <asp:ListItem Value="45">45 minutes</asp:ListItem>
                                            <asp:ListItem Value="46">46 minutes</asp:ListItem>
                                            <asp:ListItem Value="47">47 minutes</asp:ListItem>
                                            <asp:ListItem Value="48">48 minutes</asp:ListItem>
                                            <asp:ListItem Value="49">49 minutes</asp:ListItem>
                                            <asp:ListItem Value="50">50 minutes</asp:ListItem>
                                            <asp:ListItem Value="51">51 minutes</asp:ListItem>
                                            <asp:ListItem Value="52">52 minutes</asp:ListItem>
                                            <asp:ListItem Value="53">53 minutes</asp:ListItem>
                                            <asp:ListItem Value="54">54 minutes</asp:ListItem>
                                            <asp:ListItem Value="55">55 minutes</asp:ListItem>
                                            <asp:ListItem Value="56">56 minutes</asp:ListItem>
                                            <asp:ListItem Value="57">57 minutes</asp:ListItem>
                                            <asp:ListItem Value="58">58 minutes</asp:ListItem>
                                            <asp:ListItem Value="59">59 minutes</asp:ListItem>
                                            <asp:ListItem Value="60">60 minutes</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                            <asp:button id="btnAdd" runat="server" Text="Add" Width="72px"></asp:button>
                            <asp:button id="btnUpdate" runat="server" Text="Update" Width="72px"></asp:button>
                            <asp:button id="btnDelete" runat="server" Text="Delete" Width="72px"></asp:button>
                            <br /><asp:label id="lblErrors" runat="server" ForeColor="Red"></asp:label>
                        </td>
                    </tr>
                </table>
                <p>&nbsp;</p>                    
                <asp:datagrid id="dgProducts" runat="server" PageSize="20" AutoGenerateColumns="False" AllowSorting="True" OnPageIndexChanged="dgProducts_NewPage" AllowPaging="True" OnSortCommand="dgProducts_Sort" CellPadding="3">
                    <Columns>
                        <asp:BoundColumn DataField="ProductID" SortExpression="ProductID" HeaderText="Product ID">
                            <HeaderStyle CssClass="gridHeader" />
                            <ItemStyle CssClass="prodID" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="PLU" Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn DataField="PLUDesc" SortExpression="PLU" HeaderText="Product">
                            <HeaderStyle CssClass="gridHeader" />
                            <ItemStyle CssClass="prodPLU" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Type" Visible="true" HeaderText="Type">
                            <HeaderStyle CssClass="gridHeader" />
                            <ItemStyle CssClass="prodType" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="UpgradePLU" Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn DataField="UpgradePLUDesc" HeaderText="Upgrade Product">
                            <HeaderStyle CssClass="gridHeader" />
                            <ItemStyle CssClass="prodUP" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="PrepTime" HeaderText="Prep Time">
                            <HeaderStyle CssClass="gridHeader" />
                            <ItemStyle CssClass="prodPT" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="CookTime" HeaderText="Cook Time">
                            <HeaderStyle CssClass="gridHeader" />
                            <ItemStyle CssClass="prodPT" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="DecayTime" HeaderText="Hold Time">
                            <HeaderStyle CssClass="gridHeader" />
                            <ItemStyle CssClass="prodDT" />
                        </asp:BoundColumn>
                    </Columns>
                    <AlternatingItemStyle CssClass="odd" />
                    <PagerStyle CssClass="bottomNav" />
                </asp:datagrid>
            </div>
        </form>
    </body>
</html>
