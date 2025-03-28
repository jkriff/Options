<%@ Register TagPrefix="jlc" Namespace="JLovell.Webcontrols" Assembly="StaticPostBackPosition" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="Register.SuggestSell" CodeFile="SuggestSell.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
		<title>SuggestSell</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<jlc:staticpostbackposition id="StaticPostBackPosition1" runat="server"></jlc:staticpostbackposition>
		<script language="javascript">
		<!--
			function lettersOnly(e)
			{
				var keynum;
				if (window.event)//IE
					keynum = e.keyCode;
				else
					if (e.which)//mozilla/firefox/opera
						keynum = e.which
				
				var letter = String.fromCharCode(keynum);
				//alert(letter);
				var regx = new RegExp(/^\w{1,}$/);
				if (!regx.test(letter))
				{
					if (!((keynum == 13) && (document.getElementById("txtGroup").value.length > 0)))
					{
						e.returnValue = false;
						return;
					}
				}
			}
			
			function setFocus() 
			{
				document.getElementById("ddlStores").focus();
			}
		//-->
		</script>
		<style type="text/css">.ItemRow { PADDING-RIGHT: 3px; PADDING-LEFT: 3px; PADDING-BOTTOM: 0px; PADDING-TOP: 0px }
	</style>
</HEAD>
	<body style="FONT-FAMILY: Arial" onload="setFocus();">
		<form id="Form1" method="post" runat="server">
			<asp:label id="lblStore" style="Z-INDEX: 101; LEFT: 32px; POSITION: absolute; TOP: 80px" runat="server">Store</asp:label>
			<asp:button id="btnAddGroup" style="Z-INDEX: 115; LEFT: 24px; POSITION: absolute; TOP: 184px"
				runat="server" Width="176px" Text="Add Group Name"></asp:button>
			<asp:button id="btnAddPlu" style="Z-INDEX: 114; LEFT: 24px; POSITION: absolute; TOP: 576px"
				runat="server" Width="80px" Text="Add Sell PLU to Group" Height="48px"></asp:button>
			<asp:button id="btnAddBuyPlu" style="Z-INDEX: 113; LEFT: 24px; POSITION: absolute; TOP: 1008px"
				runat="server" Width="80px" Text="Add Buy PLU to Group" Height="48px"></asp:button>
			<asp:button id="btnSaveBuyPlu" style="Z-INDEX: 112; LEFT: 24px; POSITION: absolute; TOP: 1072px"
				runat="server" Width="80px" Text="Save Buy Plu changes" Height="48px"></asp:button>
			<asp:button id="btnSaveSellPlus" style="Z-INDEX: 111; LEFT: 24px; POSITION: absolute; TOP: 640px"
				runat="server" Width="80px" Text="Save Sell Plu changes" Height="48px"></asp:button>
			<asp:button id="btnSaveGroups" style="Z-INDEX: 110; LEFT: 24px; POSITION: absolute; TOP: 240px"
				runat="server" Width="176px" Text="Save Group changes"></asp:button>
			<asp:datagrid id="dgBuyPlus" style="Z-INDEX: 109; LEFT: 128px; POSITION: absolute; TOP: 968px"
				runat="server" Font-Size="12px" CellPadding="0" ShowHeader="False" BorderStyle="Solid" Caption="Purchase PLU Groups for Sell Suggestions"
				GridLines="Vertical" BorderWidth="1px" BorderColor="Silver" AutoGenerateColumns="False" BackColor="White"
				AllowPaging="True" PageSize="15" Width="648px">
				<SelectedItemStyle BackColor="SkyBlue"></SelectedItemStyle>
				<EditItemStyle BackColor="#FFFFCC"></EditItemStyle>
				<AlternatingItemStyle BackColor="PaleGreen"></AlternatingItemStyle>
				<ItemStyle HorizontalAlign="Center" Height="24px" CssClass="itemrow"></ItemStyle>
				<Columns>
					<asp:ButtonColumn Text="Select" CommandName="Select"></asp:ButtonColumn>
					<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="Update" CancelText="Cancel" EditText="Edit"></asp:EditCommandColumn>
					<asp:ButtonColumn Text="Delete" CommandName="Delete"></asp:ButtonColumn>
					<asp:BoundColumn DataField="Store" ReadOnly="True">
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="GroupName" ReadOnly="True">
						<ItemStyle HorizontalAlign="Left"></ItemStyle>
					</asp:BoundColumn>
					<asp:TemplateColumn>
						<ItemStyle HorizontalAlign="Left"></ItemStyle>
						<ItemTemplate>
							<asp:Label id="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PluName") %>'>
							</asp:Label>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:DropDownList id="DropDownList1" runat="server"></asp:DropDownList>
						</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn Visible="False" DataField="Group_ID" ReadOnly="True"></asp:BoundColumn>
					<asp:BoundColumn Visible="False" DataField="BuyPlu_ID" ReadOnly="True"></asp:BoundColumn>
					<asp:BoundColumn Visible="False" DataField="BuyPlu" ReadOnly="True"></asp:BoundColumn>
				</Columns>
				<PagerStyle Height="24px" Font-Size="14px" HorizontalAlign="Justify" Position="Top" Mode="NumericPages"></PagerStyle>
			</asp:datagrid>
			<asp:datagrid id="dgSellPlus" style="Z-INDEX: 108; LEFT: 128px; POSITION: absolute; TOP: 536px"
				runat="server" Font-Size="12px" CellPadding="0" ShowHeader="False" BorderStyle="Solid" Caption="Suggested PLUs to sell by store and group"
				GridLines="Vertical" BorderWidth="1px" BorderColor="Silver" AutoGenerateColumns="False" BackColor="White"
				AllowPaging="True" PageSize="15" Width="648px">
				<SelectedItemStyle BackColor="SkyBlue"></SelectedItemStyle>
				<EditItemStyle BackColor="#FFFFCC"></EditItemStyle>
				<AlternatingItemStyle BackColor="PaleGreen"></AlternatingItemStyle>
				<ItemStyle HorizontalAlign="Center" Height="24px" CssClass="itemrow"></ItemStyle>
				<Columns>
					<asp:ButtonColumn Text="Select" CommandName="Select"></asp:ButtonColumn>
					<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="Update" CancelText="Cancel" EditText="Edit"></asp:EditCommandColumn>
					<asp:ButtonColumn Text="Delete" CommandName="Delete"></asp:ButtonColumn>
					<asp:BoundColumn DataField="Store" ReadOnly="True">
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="GroupName" ReadOnly="True">
						<ItemStyle HorizontalAlign="Left"></ItemStyle>
					</asp:BoundColumn>
					<asp:TemplateColumn>
						<ItemStyle HorizontalAlign="Left"></ItemStyle>
						<ItemTemplate>
							<asp:Label id="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PluName") %>'>
							</asp:Label>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:DropDownList id="DropDownList1" runat="server"></asp:DropDownList>
						</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn Visible="False" DataField="Group_ID" ReadOnly="True"></asp:BoundColumn>
					<asp:BoundColumn Visible="False" DataField="SellPlu_ID" ReadOnly="True"></asp:BoundColumn>
					<asp:BoundColumn Visible="False" DataField="SellPlu" ReadOnly="True"></asp:BoundColumn>
				</Columns>
				<PagerStyle Height="24px" Font-Size="14px" HorizontalAlign="Justify" Position="Top" Mode="NumericPages"></PagerStyle>
			</asp:datagrid>
			<asp:linkbutton id="lbnCopyAllGroups" style="Z-INDEX: 107; LEFT: 24px; POSITION: absolute; TOP: 320px"
				runat="server" Width="176px" Visible="False">Copy All Groups for store</asp:linkbutton>
			<asp:linkbutton id="lbnCopyGroup" style="Z-INDEX: 106; LEFT: 24px; POSITION: absolute; TOP: 288px"
				runat="server" Width="120px" Visible="False">Copy Group</asp:linkbutton>
			<asp:label id="lblError" style="Z-INDEX: 105; LEFT: 675px; POSITION: absolute; TOP: 136px"
				runat="server" Width="100px" Height="374px" Font-Size="14px" ForeColor="Red" Font-Bold="True"></asp:label>
			<asp:textbox onkeypress="lettersOnly(event);" id="txtGroup" style="Z-INDEX: 104; LEFT: 24px; POSITION: absolute; TOP: 208px"
				runat="server" Width="176px" Visible="False" AutoPostBack="True" MaxLength="25"></asp:textbox>
			<asp:datagrid id="dgGroup" style="Z-INDEX: 103; LEFT: 224px; POSITION: absolute; TOP: 112px" runat="server"
				Width="445px" Font-Size="12px" CellPadding="0" ShowHeader="False" BorderStyle="Solid" Caption="Suggested Sell Groups"
				GridLines="Vertical" BorderWidth="1px" BorderColor="Silver" AutoGenerateColumns="False" BackColor="White"
				AllowPaging="True" PageSize="15">
				<SelectedItemStyle BackColor="LightSkyBlue"></SelectedItemStyle>
				<EditItemStyle BackColor="#FFFFCC"></EditItemStyle>
				<AlternatingItemStyle BackColor="PaleGreen"></AlternatingItemStyle>
				<ItemStyle Font-Names="Arial" HorizontalAlign="Center" Height="24px" CssClass="ItemRow"></ItemStyle>
				<Columns>
					<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="Update" CancelText="Cancel" EditText="Edit"></asp:EditCommandColumn>
					<asp:ButtonColumn Text="Delete" CommandName="Delete"></asp:ButtonColumn>
					<asp:BoundColumn DataField="store" ReadOnly="True" HeaderText="Store">
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="GroupName" HeaderText="Group Name">
						<ItemStyle HorizontalAlign="Left"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn Visible="False" DataField="group_id" ReadOnly="True"></asp:BoundColumn>
				</Columns>
				<PagerStyle Height="24px" Font-Size="14px" HorizontalAlign="Justify" Position="Top"></PagerStyle>
			</asp:datagrid>
			<div style="DISPLAY:none">
				<asp:dropdownlist id="ddlStores" style="Z-INDEX: 102; LEFT: 24px; POSITION: absolute; TOP: 152px"
					runat="server" Width="176px" AutoPostBack="True" Visible="False" ></asp:dropdownlist>
			</div>
			<script language="javascript" type="text/javascript">
				if (document.getElementById("txtGroup"))
					document.getElementById("txtGroup").focus();
				
				var dgGroupHeight = document.getElementById("dgGroup").offsetHeight + 10;
				var dgSellPlusHeight = document.getElementById("dgSellPlus").offsetHeight + 10;
				
				var dgGroupTop = 112;
				//var lblErrorTop = 536;
				
				if (document.body.clientWidth > 790)
					document.getElementById("lblError").style.width = document.body.clientWidth - 695;
				
				var btnOffset = 40;
				var btnHeight = 60;
				
				var newTop = dgGroupHeight + dgGroupTop;
				// 350px from top is highest due to copy commands
				if ( newTop < 350 )
					newTop = 350;
				
				var newTop2 = 0;
				
				if ( dgSellPlusHeight < ( btnOffset + (2*btnHeight)))
					newTop2 = newTop + btnOffset + (2*btnHeight);
				else
					newTop2 = newTop + dgSellPlusHeight;
				
				document.getElementById("dgSellPlus").style.top = newTop;
				document.getElementById("btnAddPlu").style.top = newTop + btnOffset;
				document.getElementById("btnSaveSellPlus").style.top = newTop + btnOffset + btnHeight;
				
				document.getElementById("dgBuyPlus").style.top = newTop2;
				document.getElementById("btnAddBuyPlu").style.top = newTop2 + btnOffset;
				document.getElementById("btnSaveBuyPlu").style.top = newTop2 + btnOffset + btnHeight;
				
			</script>
		</form>
	</body>
</HTML>
