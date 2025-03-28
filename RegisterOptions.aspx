<%@ Page Language="vb" AutoEventWireup="false" Inherits="Register.RegisterOptions" CodeFile="RegisterOptions.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
		<title>Register Options</title>
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<style type="text/css">
		A:link { BORDER-RIGHT: rgb(183,186,189) 2px solid; FONT-WEIGHT: 800; FONT-SIZE: 8pt; CURSOR: hand; COLOR: #ffffff; PADDING-TOP: 6px; FONT-FAMILY: Tahoma; HEIGHT: 24px; BACKGROUND-COLOR: #0000ff; TEXT-ALIGN: center; TEXT-DECORATION: none }
		A:visited { BORDER-RIGHT: rgb(183,186,189) 2px solid; FONT-WEIGHT: 800; FONT-SIZE: 8pt; CURSOR: hand; COLOR: #ffffff; PADDING-TOP: 6px; FONT-FAMILY: Tahoma; HEIGHT: 24px; BACKGROUND-COLOR: #0000ff; TEXT-ALIGN: center; TEXT-DECORATION: none }
		A:active { BORDER-RIGHT: rgb(183,186,189) 2px solid; FONT-WEIGHT: 800; FONT-SIZE: 8pt; CURSOR: hand; COLOR: #ffffff; PADDING-TOP: 6px; FONT-FAMILY: Tahoma; HEIGHT: 24px; BACKGROUND-COLOR: #6495ed; TEXT-ALIGN: center; TEXT-DECORATION: none }
		A:hover { BORDER-RIGHT: rgb(183,186,189) 2px solid; FONT-WEIGHT: 800; FONT-SIZE: 8pt; CURSOR: hand; COLOR: #ffffff; PADDING-TOP: 6px; FONT-FAMILY: Tahoma; HEIGHT: 24px; BACKGROUND-COLOR: #6495ed; TEXT-ALIGN: center; TEXT-DECORATION: none }
		</style>
	<script type="text/javascript" language="javascript">

	window.onload = window_onload;	

	function window_onload()
	{
		Form1.hidHeight.value = screen.availHeight - Form1.hidSizeOfMenu.value - Form1.hidSizeOfFilePrintMenu.value;
		Form1.hidWidth.value = screen.availWidth - 12;
		Form1.hidTop.value = Form1.hidSizeOfMenu.value;
	}	

	function confirmClick(sMsg)
	{
		if (!confirm(sMsg))
		{
			window.event.returnValue = false;
		}
	}
		
	</script>
</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<asp:Table Runat="server" Font-Name="Tahoma" Font-Size="9pt" id=Table1 Width="1500px">
				<asp:TableRow Runat="server">
					<asp:TableCell Runat="server">
						<asp:Table Runat="server" Font-Size="9pt" Font-Names="Tahoma">
							<asp:TableRow Runat="server">
								<asp:TableCell ID="tdImage" Runat="server" Width="200px">
									<img src="/images/Logo.jpg" alt="Brand Logo"></asp:TableCell>
								<asp:TableCell ID="tdButtons" Runat="server" VerticalAlign="Top">
								<asp:LinkButton id="cmdSave" runat="server">&nbsp;&nbsp;&nbsp;Save&nbsp;&nbsp;&nbsp;</asp:LinkButton>
								&nbsp;
								<asp:LinkButton id="cmdSaveClose" runat="server">&nbsp;&nbsp;&nbsp;Save & Close&nbsp;&nbsp;&nbsp;</asp:LinkButton>
								&nbsp;
								<asp:LinkButton id="cmdCancelClose" runat="server">&nbsp;&nbsp;&nbsp;Cancel & Close&nbsp;&nbsp;&nbsp;</asp:LinkButton>
							</asp:TableCell>
							</asp:TableRow>
						</asp:Table>
					</asp:TableCell>
				</asp:TableRow>
				<asp:TableRow Runat="server">
					<asp:TableCell Runat="server" Height="25px" VerticalAlign="Middle">
						<asp:Label ID="lblMsg1" Runat="server">Store Number:&nbsp;</asp:Label>
						<asp:Label ID="lblStoreNum" Runat="server"></asp:Label>
						&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:Label Runat="server">Register:&nbsp;</asp:Label>
						<asp:DropDownList ID="cboRegister" Runat="server" AutoPostBack="True">
							<asp:ListItem>1</asp:ListItem>
							<asp:ListItem>2</asp:ListItem>
							<asp:ListItem>3</asp:ListItem>
							<asp:ListItem>4</asp:ListItem>
							<asp:ListItem>5</asp:ListItem>
							<asp:ListItem>6</asp:ListItem>
							<asp:ListItem>7</asp:ListItem>
							<asp:ListItem>8</asp:ListItem>
							<asp:ListItem>9</asp:ListItem>
							<asp:ListItem>10</asp:ListItem>
							<asp:ListItem>11</asp:ListItem>
							<asp:ListItem>12</asp:ListItem>
							<asp:ListItem>13</asp:ListItem>
							<asp:ListItem>14</asp:ListItem>
							<asp:ListItem>15</asp:ListItem>
							<asp:ListItem>16</asp:ListItem>
							<asp:ListItem>17</asp:ListItem>
							<asp:ListItem>18</asp:ListItem>
							<asp:ListItem>19</asp:ListItem>
						</asp:DropDownList>
						&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:LinkButton id="cmdCopyRegister" runat="server">&nbsp;&nbsp;&nbsp;Copy Register Data&nbsp;&nbsp;&nbsp;</asp:LinkButton>
					</asp:TableCell>
				</asp:TableRow>
				<asp:TableRow Runat="server">
					<asp:TableCell Runat="server">
						<asp:Table Runat="server" Font-Size="9pt" Font-Names="Tahoma">
							<asp:TableRow Runat="server">
								<asp:TableCell ID="tdMenu" Runat="server"></asp:TableCell>
							</asp:TableRow>
						</asp:Table>
					</asp:TableCell>
				</asp:TableRow>
				<asp:TableRow Runat="server">
					<asp:TableCell Runat="server">
						<asp:Table id="tblMain" Runat="server" Font-Size="9pt" Font-Names="Tahoma"></asp:Table>
					</asp:TableCell>
				</asp:TableRow>
			</asp:Table>
			<div id="divDisplayHeader" runat="server">
				<asp:Table ID="tblDisplayHeader" Runat="server" BorderStyle="Ridge" BackColor="#add8e6" Font-Name="Tahoma" Font-Size="9pt">
				<asp:TableRow Height="24px" runat="server">
					<asp:TableCell ID="tblDisplayHeader1" Runat="server" HorizontalAlign="Left">&nbsp;</asp:TableCell>
					<asp:TableCell ID="tblDisplayHeader2" Runat="server" HorizontalAlign="Center">Header Left</asp:TableCell>
					<asp:TableCell ID="tblDisplayHeader3" Runat="server" HorizontalAlign="Left">&nbsp;</asp:TableCell>
					<asp:TableCell ID="tblDisplayHeader4" Runat="server" HorizontalAlign="Center">Header Right</asp:TableCell>
					<asp:TableCell ID="tblDisplayHeader5" Runat="server" HorizontalAlign="Center" Width="80px">Ticket Type<br />Exclusion</asp:TableCell>
				</asp:TableRow>
                </asp:Table>
			</div>
			<div id="divPrintHeader" runat="server">
				<asp:Table ID="tblPrintHeader" Runat="server" BorderStyle="Ridge" BackColor="#add8e6" Font-Name="Tahoma" Font-Size="9pt">
				<asp:TableRow Height="24px">
					<asp:TableCell ID="tblPrintHeader1" Runat="server" HorizontalAlign="Left">&nbsp;</asp:TableCell>
					<asp:TableCell ID="tblPrintHeader2" Runat="server" HorizontalAlign="Center">Header Left</asp:TableCell>
					<asp:TableCell ID="tblPrintHeader3" Runat="server" HorizontalAlign="Left">&nbsp;</asp:TableCell>
					<asp:TableCell ID="tblPrintHeader4" Runat="server" HorizontalAlign="Center">Header Right</asp:TableCell>
					<asp:TableCell ID="tblPrintHeader5" Runat="server" HorizontalAlign="Center" Width="80px">Ticket Type<br />Exclusion</asp:TableCell>
					<asp:TableCell ID="tblPrintHeader6" Runat="server" HorizontalAlign="Center" Width="80px">Justify</asp:TableCell>
					<asp:TableCell ID="tblPrintHeader7" Runat="server" HorizontalAlign="Center" Width="40px">Large</asp:TableCell>
					<asp:TableCell ID="tblPrintHeader8" Runat="server" HorizontalAlign="Center" Width="40px">Bold</asp:TableCell>
					<asp:TableCell ID="tblPrintHeader9" Runat="server" HorizontalAlign="Center" Width="80px">Double<br />Wide</asp:TableCell>
					<asp:TableCell ID="tblPrintHeader10" Runat="server" HorizontalAlign="Center" Width="80px">Double<br />High</asp:TableCell>
					<asp:TableCell ID="tblPrintHeader11" Runat="server" HorizontalAlign="Center" Width="70px">Underline</asp:TableCell>
					<asp:TableCell ID="tblPrintHeader12" Runat="server" HorizontalAlign="Center" Width="70px">Color</asp:TableCell>
				</asp:TableRow>
				</asp:Table>
			</div>
			<div id="divPayType" runat="server">
				<asp:Table ID="tblPayType" Runat="server" BorderStyle="Ridge" BackColor="#add8e6" Font-Name="Tahoma" Font-Size="9pt">
					<asp:TableRow>
						<asp:TableCell Runat="server" Visible="False"></asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center"></asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center">Pay Type</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center">Pay Type On Ticket</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center">Type</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center">Change</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center">Refund</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center">&nbsp;Require<br>&nbsp;Number</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center">&nbsp;Manager<br>&nbsp;Auth</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center">Drawer</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center">Receipt</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center">&nbsp;Require<br>&nbsp;Customer</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center">&nbsp;Remove<br>&nbsp;Surcharge</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center"></asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center"></asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center"></asp:TableCell>
					</asp:TableRow>
				</asp:Table>
			</div>
			<div id="divCopyRegister" runat="server">
				<asp:Table ID="tblCopyRegister" Runat="server" BorderStyle="Ridge" BackColor="#add8e6" Font-Name="Tahoma" Font-Size="9pt">
					<asp:TableRow Runat="server">
						<asp:TableCell Runat="server" HorizontalAlign="Right">
							Copy from register:&nbsp;
							<asp:TextBox ID="txtSourceRegister" Runat="server" Width="40px">1</asp:TextBox>
						</asp:TableCell>
					</asp:TableRow>
					<asp:TableRow Runat="server">
						<asp:TableCell Runat="server" HorizontalAlign="Right" VerticalAlign="Middle">
							To register:&nbsp;
							<asp:TextBox ID="txtTargetRegister" Runat="server" Width="40px"></asp:TextBox>
						</asp:TableCell>
					</asp:TableRow>
					<asp:TableRow Runat="server">
						<asp:TableCell Runat="server" HorizontalAlign="Center">
							<asp:LinkButton ID="cmdCopyRegisterOK" Runat="server" Width="70px">&nbsp;&nbsp;&nbsp;OK&nbsp;&nbsp;&nbsp;</asp:LinkButton>&nbsp;&nbsp;&nbsp;
							<asp:LinkButton ID="cmdCopyRegisterCancel" Runat="server" Width="70px">&nbsp;&nbsp;&nbsp;Cancel&nbsp;&nbsp;&nbsp;</asp:LinkButton>
						</asp:TableCell>
					</asp:TableRow>
				</asp:Table>
			</div>
			<div id="divLogoHeader" runat="server">
				<asp:Table ID="tblLogoHeader" Runat="server" BorderStyle="Ridge" BackColor="#add8e6" Font-Name="Tahoma" Font-Size="9pt">
					<asp:TableRow>
						<asp:TableCell Runat="server" Visible="False"></asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center"></asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center">Text</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center">Justification</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center">Large</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center">Bold</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center">Double<br />Wide</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center">Double<br />High</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center">Underline</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center">QR<br />Code</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center">QR<br />Pixels</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center">QR<br />Quality</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center"></asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center"></asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center"></asp:TableCell>
					</asp:TableRow>
				</asp:Table>
			</div>
			<asp:Table ID="tblPrinting" Runat="server" BorderStyle="None" Font-Name="Tahoma" Font-Size="9pt">
				<asp:TableRow Height="24px">
					<asp:TableCell Runat="server" HorizontalAlign="Left">&nbsp;</asp:TableCell>
					<asp:TableCell Runat="server" HorizontalAlign="Right">&nbsp;</asp:TableCell>
					<asp:TableCell Runat="server" HorizontalAlign="Center" Width="100px">Justification</asp:TableCell>
					<asp:TableCell Runat="server" HorizontalAlign="Center" Width="40px">Large</asp:TableCell>
					<asp:TableCell Runat="server" HorizontalAlign="Center" Width="40px">Bold</asp:TableCell>
					<asp:TableCell Runat="server" HorizontalAlign="Center" Width="80px">Double<br />Wide</asp:TableCell>
					<asp:TableCell Runat="server" HorizontalAlign="Center" Width="80px">Double<br />High</asp:TableCell>
					<asp:TableCell Runat="server" HorizontalAlign="Center" Width="70px">Underline</asp:TableCell>
				</asp:TableRow>
			</asp:Table>
			<div id="divLogoFooter" runat="server">
				<asp:Table ID="tblLogoFooter" Runat="server" BorderStyle="Ridge" BackColor="#add8e6" Font-Name="Tahoma" Font-Size="9pt">
					<asp:TableRow>
						<asp:TableCell Runat="server" Visible="False"></asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center"></asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center">Text</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center">Justification</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center">Large</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center">Bold</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center">Double<br />Wide</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center">Double<br />High</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center">Underline</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center">QR<br />Code</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center">QR<br />Pixels</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center">QR<br />Quality</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center"></asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center"></asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center"></asp:TableCell>
					</asp:TableRow>
				</asp:Table>
			</div>
			<div id="divTicketType" runat="server">
				<asp:Table ID="tblTicketType" Runat="server" BorderStyle="Ridge" BackColor="#add8e6" Font-Name="Tahoma" Font-Size="9pt">
					<asp:TableRow runat="server">
						<asp:TableCell Runat="server" Visible="False">&nbsp;</asp:TableCell>
						<asp:TableCell Runat="server">&nbsp;</asp:TableCell>
						<asp:TableCell Runat="server">&nbsp;</asp:TableCell>
						<asp:TableCell Runat="server">&nbsp;</asp:TableCell>
						<asp:TableCell Runat="server">&nbsp;</asp:TableCell>
						<asp:TableCell Runat="server">&nbsp;</asp:TableCell>
						<asp:TableCell Runat="server">&nbsp;</asp:TableCell>
						<asp:TableCell Runat="server">&nbsp;</asp:TableCell>
						<asp:TableCell Runat="server">&nbsp;</asp:TableCell>
						<asp:TableCell Runat="server">&nbsp;</asp:TableCell>
						<asp:TableCell Runat="server">&nbsp;</asp:TableCell>
						<asp:TableCell Runat="server">&nbsp;</asp:TableCell>
						<asp:TableCell Runat="server">&nbsp;</asp:TableCell>
						<asp:TableCell Runat="server">&nbsp;</asp:TableCell>
						<asp:TableCell Runat="server" ColumnSpan="4" HorizontalAlign="Center">Exclude Tax</asp:TableCell>
						<asp:TableCell Runat="server">&nbsp;</asp:TableCell>
						<asp:TableCell Runat="server">&nbsp;</asp:TableCell>
						<asp:TableCell Runat="server">&nbsp;</asp:TableCell>
						<asp:TableCell Runat="server">&nbsp;</asp:TableCell>
						<asp:TableCell Runat="server">&nbsp;</asp:TableCell>
						<asp:TableCell Runat="server">&nbsp;</asp:TableCell>
						<asp:TableCell Runat="server">&nbsp;</asp:TableCell>
					</asp:TableRow>
					<asp:TableRow runat="server">
						<asp:TableCell Runat="server" Visible="False"></asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center" VerticalAlign="Bottom"></asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center" VerticalAlign="Bottom">Ticket Type</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center" VerticalAlign="Bottom">Ticket<br />Popup</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center" VerticalAlign="Bottom">Drive<br />Through</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center" VerticalAlign="Bottom">Sched<br />Prices</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center" VerticalAlign="Bottom">Reassign</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center" VerticalAlign="Bottom">Require<br />Customer</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center" VerticalAlign="Bottom">PLU<br />Upcharge</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center" VerticalAlign="Bottom">Schedule</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center" VerticalAlign="Bottom">Schedule<br />Phrase</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center" VerticalAlign="Bottom">Allow<br />Combos</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center" VerticalAlign="Bottom">Customer<br />Survey</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center" VerticalAlign="Bottom">Survey<br />Interval</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center" VerticalAlign="Bottom">1</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center" VerticalAlign="Bottom">2</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center" VerticalAlign="Bottom">3</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center" VerticalAlign="Bottom">4</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center" VerticalAlign="Bottom">KDS Type</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center" VerticalAlign="Bottom">Color</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center" VerticalAlign="Bottom">Hidden</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center" VerticalAlign="Bottom">Suppress<br />KDS</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center" VerticalAlign="Bottom">Suppress<br />KP</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center" VerticalAlign="Bottom">DSP</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center" VerticalAlign="Bottom"></asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center" VerticalAlign="Bottom"></asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center" VerticalAlign="Bottom"></asp:TableCell>
					</asp:TableRow>
				</asp:Table>
			</div>
			<div id="divCustomerSurveyHeader" runat="server">
				<asp:Table ID="tblCustomerSurveyHeader" Runat="server" BorderStyle="Ridge" BackColor="#add8e6" Font-Name="Tahoma" Font-Size="9pt">
					<asp:TableRow>
						<asp:TableCell Runat="server" Visible="False"></asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center"></asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center">Text</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center">Justification</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center">Large</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center">Bold</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center">Double Wide</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center">Double High</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center">Underline</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center"></asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center"></asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center"></asp:TableCell>
					</asp:TableRow>
				</asp:Table>
			</div>
			<div id="divCustomerSurveyFooter" runat="server">
				<asp:Table ID="tblCustomerSurveyFooter" Runat="server" BorderStyle="Ridge" BackColor="#add8e6" Font-Name="Tahoma" Font-Size="9pt">
					<asp:TableRow>
						<asp:TableCell Runat="server" Visible="False"></asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center"></asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center">Text</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center">Justification</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center">Large</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center">Bold</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center">Double Wide</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center">Double High</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center">Underline</asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center"></asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center"></asp:TableCell>
						<asp:TableCell Runat="server" HorizontalAlign="Center"></asp:TableCell>
					</asp:TableRow>
				</asp:Table>
			</div>
			<input id="hidConfig" runat="server" type="hidden" value="./Config/RegisterOptions.xml"> 
			<input id="hidExchangeRate" runat="server" type="hidden" value="http://www.fms.treas.gov/intn.html#rates"> 
			<input id="hidLeft" runat="server" type="hidden" value="1">
			<input id="hidWidth" runat="server" type="hidden" value="800"> 
			<input id="hidTop" runat="server" type="hidden" value="1">
			<input id="hidHeight" runat="server" type="hidden" value="600">
			<input id="hidSizeOfMenu" runat="server" type="hidden" value="118">
			<input id="hidSizeOfFilePrintMenu" runat="server" type="hidden" value="80">
		</form>
	</body>
</HTML>
