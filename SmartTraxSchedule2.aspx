<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SmartTraxSchedule2.aspx.vb" Inherits="SmartTraxSchedule2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title>SmartTrax Schedule by Product</title>
    </head>
    <body>
        <form id="form1" runat="server">
            <div>
                <h2>SmartTrax Schedule by Day</h2>
                <h4><asp:Label ID="lblStoreDisplay" runat="server">Store 8888</asp:Label></h4>
                <h4><asp:Label ID="Label1" runat="server">Day: Friday</asp:Label></h4>
                <asp:Label ID="lblErrors" runat="server" ForeColor="Red"></asp:Label><strong>
                <table border="1px black solid" cellpadding="3px" cellspacing="3px">
                    <tr>
                        <td align="center" style="border:none"></td>
                        <td align="center" style="border-top:none;border-left:none;border-right:none">&nbsp;9&nbsp;</td>
                        <td align="center" style="border-top:none;border-left:none;border-right:none">10</td>
                        <td align="center" style="border-top:none;border-left:none;border-right:none">11</td>
                        <td align="center" style="border-top:none;border-left:none;border-right:none">12</td>
                        <td align="center" style="border-top:none;border-left:none;border-right:none">13</td>
                        <td align="center" style="border-top:none;border-left:none;border-right:none">14</td>
                        <td align="center" style="border-top:none;border-left:none;border-right:none">15</td>
                        <td align="center" style="border-top:none;border-left:none;border-right:none">16</td>
                        <td align="center" style="border-top:none;border-left:none;border-right:none">17</td>
                        <td align="center" style="border-top:none;border-left:none;border-right:none">18</td>
                        <td align="center" style="border-top:none;border-left:none;border-right:none">19</td>
                        <td align="center" style="border-top:none;border-left:none;border-right:none">20</td>
                        <td align="center" style="border-top:none;border-left:none;border-right:none">21</td>
                        <td style="border:none"></td>
                        <td style="border:none"></td>
                        <td style="border:none"></td>
                    </tr>
                    <tr>
                        <td style="border-top:none;border-left:none;border-bottom:none">PEPPERONI BOMB</td>
                        <td align="center"></td>
                        <td align="center"></td>
                        <td align="center">1</td>
                        <td align="center">5</td>
                        <td align="center">3</td>
                        <td align="center"></td>
                        <td align="center"></td>
                        <td align="center">2</td>
                        <td align="center">7</td>
                        <td align="center">4</td>
                        <td align="center">2</td>
                        <td align="center"></td>
                        <td align="center"></td>
                        <td style="border:none"><asp:button ID="Button1" runat="server" text="Dial ▲" /></td>
                        <td style="border:none"><asp:button ID="Button15" runat="server" text="Dial ▼" /></td>
                        <td style="border:none"><asp:button ID="Button16" runat="server" text="Suggest" /></td>
                    </tr>
                    <tr>
                        <td style="border-top:none;border-left:none;border-bottom:none">BACON PIZZA</td>
                        <td align="center"></td>
                        <td align="center"></td>
                        <td align="center">1</td>
                        <td align="center">3</td>
                        <td align="center">1</td>
                        <td align="center"></td>
                        <td align="center"></td>
                        <td align="center"></td>
                        <td align="center">2</td>
                        <td align="center">5</td>
                        <td align="center">1</td>
                        <td align="center"></td>
                        <td align="center"></td>
                        <td style="border:none"><asp:button ID="Button2" runat="server" text="Dial ▲" /></td>
                        <td style="border:none"><asp:button ID="Button13" runat="server" text="Dial ▼" /></td>
                        <td style="border:none"><asp:button ID="Button14" runat="server" text="Suggest" /></td>
                    </tr>
                    <tr>
                        <td style="border-top:none;border-left:none;border-bottom:none">WINGS</td>
                        <td align="center"></td>
                        <td align="center"></td>
                        <td align="center"></td>
                        <td align="center">2</td>
                        <td align="center">1</td>
                        <td align="center"></td>
                        <td align="center"></td>
                        <td align="center">2</td>
                        <td align="center">4</td>
                        <td align="center">3</td>
                        <td align="center"></td>
                        <td align="center"></td>
                        <td align="center"></td>
                        <td style="border:none"><asp:button ID="Button3" runat="server" text="Dial ▲" /></td>
                        <td style="border:none"><asp:button ID="Button11" runat="server" text="Dial ▼" /></td>
                        <td style="border:none"><asp:button ID="Button12" runat="server" text="Suggest" /></td>
                    </tr>
                    <tr>
                        <td style="border-top:none;border-left:none;border-bottom:none">CHICKEN PIZZA</td>
                        <td align="center"></td>
                        <td align="center"></td>
                        <td align="center">1</td>
                        <td align="center">2</td>
                        <td align="center"></td>
                        <td align="center"></td>
                        <td align="center"></td>
                        <td align="center">1</td>
                        <td align="center">4</td>
                        <td align="center">5</td>
                        <td align="center">2</td>
                        <td align="center"></td>
                        <td align="center"></td>
                        <td style="border:none"><asp:button ID="Button9" runat="server" text="Dial ▲" /></td>
                        <td style="border:none"><asp:button ID="Button10" runat="server" text="Dial ▼" /></td>
                        <td style="border:none"><asp:button ID="Button4" runat="server" text="Suggest" /></td>
                    </tr>
                    <tr>
                        <td style="border-top:none;border-left:none;border-bottom:none">CHEESE BOMB</td>
                        <td align="center"></td>
                        <td align="center"></td>
                        <td align="center">1</td>
                        <td align="center">5</td>
                        <td align="center">3</td>
                        <td align="center"></td>
                        <td align="center"></td>
                        <td align="center">2</td>
                        <td align="center">7</td>
                        <td align="center">4</td>
                        <td align="center">2</td>
                        <td align="center"></td>
                        <td align="center"></td>
                        <td style="border:none"><asp:button ID="Button5" runat="server" text="Dial ▲" /></td>
                        <td style="border:none"><asp:button ID="Button6" runat="server" text="Dial ▼" /></td>
                        <td style="border:none"><asp:button ID="Button7" runat="server" text="Suggest" /></td>
                    </tr>
                    <tr>
                        <td colspan="15" style="border:none"><center><asp:Button ID="Button8" runat="server" Text="Save and Close" /></center></td>
                    </tr>
                </table></strong>
            </div>
        </form>
    </body>
</html>
