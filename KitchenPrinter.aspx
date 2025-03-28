<%@ Page Language="VB" AutoEventWireup="false" CodeFile="KitchenPrinter.aspx.vb" Inherits="KitchenPrinter" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
<title>Kitchen Printer Setup</title>
<META http-equiv=Content-Type content="text/html; charset=windows-1252">
<meta content="Microsoft Visual Studio .NET 7.1" name=GENERATOR>
<meta content="Visual Basic .NET 7.1" name=CODE_LANGUAGE>
<meta content=JavaScript name=vs_defaultClientScript>
<meta content=http://schemas.microsoft.com/intellisense/ie5 name=vs_targetSchema>
<style>.mouseover { CURSOR: pointer; BACKGROUND-COLOR: #ebf3f6 }
	.mouseout { CURSOR: pointer; BACKGROUND-COLOR: #ffffff }
	H1 { MARGIN-TOP: 35px }
	.gridHeader { FONT-WEIGHT: bolder; COLOR: white; BACKGROUND-COLOR: #86adce }
	.gridHeader A { COLOR: white }
	.box H4 { PADDING-RIGHT: 5px; PADDING-LEFT: 5px; MARGIN-BOTTOM: 10px; PADDING-BOTTOM: 5px; PADDING-TOP: 5px; BACKGROUND-COLOR: #ebf3f6 }
	#labelDesc { BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid; WIDTH: 153px; BORDER-BOTTOM: black 1px solid }
	.clickedRow { BACKGROUND-COLOR: yellow }
	</style>

<script language=javascript>
		<!--
		var clickedRow;
		function loadItems(row, store, plu, dest, desc, priority)
		{
			try{
				clickedRow.className = mouseout;
			}catch(ex){}
			row.className = "clickedRow";
			clickedRow = row;
			//var oClient = document.getElementById("labelClient");
			var hClient = document.getElementById("hideClient");
			var oStore = document.getElementById("labelStore");
			var hStore = document.getElementById("hideStore");
			var oPlu = document.getElementById("labelPLU");
			var hPlu = document.getElementById("hidePLU");
			var oPri = document.getElementById("textboxPriority");
			//var hPriority = document.getElementById("hidePriority");
			var oKP = document.getElementById("textboxKP");
			//var oDesc = document.getElementById("labelDesc");
			
			//oClient.value = client;
			//oDesc.innerHTML = desc;
			oStore.value = store;
			oPlu.value = plu;
			//hClient.value = client;
			hStore.value = store;
			hPlu.value = plu;
			oPri.value = priority;
			oKP.value = dest;
		}
		function mouseover(row)
		{
			if (row.className != "clickedRow")
				row.className = "mouseover";
		}
		function mouseout(row)
		{
			if (row.className != "clickedRow")
				row.className = "mouseout";
		}
		//-->
		</script>
</HEAD>
<body>
<form id=formMain method=post runat="server">
<table>
  <tr>
    <td vAlign=top><IMG alt=InfoOrder src="/images/logo.jpg" align=left > 
      <h1>Kitchen Printer Setup</h1></td></tr>
  <tr>
    <td vAlign=top>
			<input id=hideClient type=hidden name=hideClient runat="server"> 
      <input id=hideStore type=hidden name=hideStore runat="server"> 
      <input id=hidePLU type=hidden name=hidePLU runat="server"> 
      <asp:label id=labelStoreDisplay Runat="server"></asp:label>
      <table>
        <tr>
          <td vAlign=top>
            <div class=box id=searchBox>
            <h4>Search&nbsp;for PLU or Description:</h4><asp:textbox id=textboxSearchPLU runat="server"></asp:textbox><asp:button id=buttonPluSearch runat="server" Text="GO"></asp:button></div>
            <div class=box id=editBox>
            <h4>Edit Data:</h4>
            <table>
              <TR>
                <TD class=TEXT>Store</TD>
                <TD class=TEXT><asp:textbox id=labelStore Runat="server" BorderStyle="None" ReadOnly="True"></asp:textbox></TD></TR>
              <TR>
                <TD class=TEXT>PLU #&nbsp;&nbsp;</TD>
                <TD class=TEXT><asp:textbox id=labelPLU Runat="server" BorderStyle="None" ReadOnly="True"></asp:textbox></TD></TR>
              <!--<TR>
                <TD class=TEXT>Description&nbsp;&nbsp;</TD>
                <TD class=TEXT><asp:label id="labelDesc" EnableViewState="True" Runat="server"></asp:label></TD></TR>-->
              <TR>
                <TD class=TEXT>KP Number:&nbsp;&nbsp;</TD>
                <TD class=TEXT><asp:textbox id=textboxKP runat="server" Width="30"></asp:textbox></TD></TR>
              <TR>
                <TD class=TEXT>Printing Priority:&nbsp;&nbsp;</TD>
                <TD class=TEXT><asp:textbox id=textboxPriority runat="server" Width="30"></asp:textbox></TD>
              </TR>
              </table><asp:button id=buttonSave runat="server" Text="Save" Width="72px"></asp:button><asp:label id=labelSaved runat="server"></asp:label><br 
            ><asp:label id=labelErrors runat="server" ForeColor="Red"></asp:label></div></td>
          <td vAlign=top><asp:datagrid id=datagridPLUs runat="server" PageSize="20" AutoGenerateColumns="False" AllowSorting="True" OnPageIndexChanged="datagridPLUs_NewPage" AllowPaging="True" OnSortCommand="datagridPLUs_Sort" CellPadding="3">
<Columns>
<asp:BoundColumn DataField="Store" SortExpression="Store" HeaderText="Store">
<HeaderStyle CssClass="gridHeader">
</HeaderStyle>
</asp:BoundColumn>
<asp:BoundColumn DataField="PLU" SortExpression="PLU" HeaderText="PLU">
<HeaderStyle CssClass="gridHeader">
</HeaderStyle>
</asp:BoundColumn>
<asp:BoundColumn DataField="KitchenPrinter" SortExpression="KitchenPrinter" HeaderText="KP Number">
<HeaderStyle CssClass="gridHeader">
</HeaderStyle>
</asp:BoundColumn>
<asp:BoundColumn DataField="Desc" HeaderText="Description">
<HeaderStyle CssClass="gridHeader">
</HeaderStyle>
</asp:BoundColumn>
<asp:BoundColumn DataField="Priority" HeaderText="Print Priority">
<HeaderStyle CssClass="gridHeader">
</HeaderStyle>
</asp:BoundColumn>
</Columns>
</asp:datagrid></td></tr></table></td></tr></table> <!--close main table--></form>
</body>
</HTML>
