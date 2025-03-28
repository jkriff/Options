<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PerformanceAnalytics.aspx.vb"
    Inherits="PerformanceAnalytics" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Performance Analytics Settings</title>
        <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
        <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
        <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE" />
        <meta content="JavaScript" name="vs_defaultClientScript" />
        <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
        <style type="text/css">
            .mouseover
            {
                cursor: pointer;
                background-color: #ebf3f6;
            }
            .mouseout
            {
                cursor: pointer;
                background-color: #ffffff;
            }
            H1
            {
                margin-top: 35px;
            }
            .gridHeader
            {
                font-weight: bolder;
                color: white;
                background-color: #86adce;
            }
            .gridHeader A
            {
                color: white;
            }
            .scheduleID, .schedulerID, .startTime, .endTime, .generateEvery
            {
                text-align: center;
            }
            .scheduled, .message
            {
                text-align: left;
            }
            .bottomNav
            {
                background-color: #92ADD8;
                font-weight: bold;
                text-align: center;
            }
            .box H4
            {
                padding-right: 5px;
                padding-left: 5px;
                margin-bottom: 10px;
                padding-bottom: 5px;
                padding-top: 5px;
                background-color: #ebf3f6;
            }
            #labelDesc
            {
                border-right: black 1px solid;
                border-top: black 1px solid;
                border-left: black 1px solid;
                width: 153px;
                border-bottom: black 1px solid;
            }
            .clickedRow
            {
                background-color: #3fa84b;
            }
            .odd
            {
                background: #f0f2f4;
            }
            h2
            {
                font-family: Verdana, Geneva, sans-serif;
            }
            a
            {
                font-family: Verdana, Geneva, sans-serif;
            }
            body
            {
                font-family: Verdana, Geneva, sans-serif;
                color: #000;
            }
            #dgSchedules tr .gridheader
            {
                background: #3A4856;
                padding: 15px 10px;
                color: #fff;
                text-align: left;
                font-weight: normal;
                text-transform: uppercase;
            }
            #dgSchedules tr .gridheader a
            {
                color: #FFF;
            }
            #dgSchedules
            {
                font-size: 12px;
                border-top-style: none;
                border-right-style: none;
                border-bottom-style: none;
                border-left-style: none;
                border-top-width: 0px;
                border-right-width: 0px;
                border-bottom-width: 0px;
                border-left-width: 0px;
            }
            #dgSchedules .odd td, #dgSchedules td
            {
                padding: 5px 2px;
                border: none;
            }
            .topEntry tr td .topEntry
            {
                font-size: 14px;
                margin: 0px;
                padding: 0px;
                border: 1px double #3A4856;
            }
            .editProduct
            {
                border: 1px double 92ADD8;
            }
            .topEntry
            {
                border: 1px solid #3A4856;
                margin-top: 10px;
                margin-right: 20px;
                margin-bottom: 20px;
                margin-left: 20px;
                padding-top: 10px;
                padding-right: 20px;
                padding-bottom: 20px;
                padding-left: 20px;
                text-align: center;
            }
            .topEntry tr td .editProduct .editEntry
            {
                font-size: 14px;
                text-align: right;
                padding: 0px;
                margin: 5px;
            }
            .topEntry tr td .editProduct .editEntry tr .col2
            {
                text-align: left;
            }
            .topEntry tr td .editProduct .editEntry tr .col1
            {
                font-weight: normal;
                color: #3A4856;
            }
            .editEntry
            {
            	text-align: left;
            }
            select
            {
                background-color: #92ADD8;
            }
            .acc-header
            {
                width: 500px;
                background-color: #3A4856;
                margin-bottom: 2px;
                padding: 15px 10px;
                color: white;
                font-weight: normal;
                font-size: 12px;
                cursor: pointer;
                text-transform: uppercase;
            }
            .acc-selected
            {
                width: 500px;
                background-color: #3fa84b;
                margin-bottom: 2px;
                padding: 15px 10px;
                color: black;
                font-weight: normal;
                font-size: 12px;
                cursor: pointer;
                text-transform: uppercase;
            }
            .acc-selected
            {
                border: solid 1px #666666;
            }
            .acc-content
            {
                width: 500px;
                margin-bottom: 2px;
                padding: 2px;
            }
            .acc-content
            {
                border: solid 1px #666666;
            }
            .invisible { display:none; }
            .visible { display:inline; }
        </style>

        <script type="text/javascript" language="javascript">
		        <!--
                function changeTracked()
                    {
                    var chkTrackDepts = document.getElementById("chkTrackDepts");
                    var ddlPLUs = document.getElementById("ddlPLUs");
                    var ddlDepts = document.getElementById("ddlDepts");
                    
                    if (chkTrackDepts.checked)
                        {
                        ddlPLUs.className = "invisible";
                        ddlDepts.className = "visible";
                        }
                    else
                        {
                        ddlPLUs.className = "visible";
                        ddlDepts.className = "invisible";
                        }
                    }		    
		        -->
        </script>
    </head>
    <body>
        <form id="formPerformanceAnalytics" runat="server">
            <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
            </asp:ScriptManager>
            <div>
                <input id="hideStore" type="hidden" name="hideStore" runat="server" />
                <input id="hideRegister" type="hidden" name="hideRegister" runat="server" />
                <table>
                    <tr>
                        <td>
                            <h2>Performance Analytics Settings</h2>
                            <h4><asp:Label ID="lblStoreDisplay" runat="server" /></h4>
                        </td>
                    </tr>
                </table>
                <table class="topEntry">
                    <tr>
                        <td>
                            <table class="editEntry">
                                <tr>
                                    <td class="col1">
                                        Register:
                                    </td>
                                    <td class="col2">
                                        <asp:DropDownList ID="ddlRegister" runat="server" Width="40px" AutoPostBack="true">
                                            <asp:ListItem Value="1">1</asp:ListItem>
                                            <asp:ListItem Value="2">2</asp:ListItem>
                                            <asp:ListItem Value="3">3</asp:ListItem>
                                            <asp:ListItem Value="4">4</asp:ListItem>
                                            <asp:ListItem Value="5">5</asp:ListItem>
                                            <asp:ListItem Value="6">6</asp:ListItem>
                                            <asp:ListItem Value="7">7</asp:ListItem>
                                            <asp:ListItem Value="8">8</asp:ListItem>
                                            <asp:ListItem Value="9">9</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr><td>&nbsp;</td><td>&nbsp;</td></tr>
                                <tr>
                                    <td class="col1" colspan="2">
                                        <asp:CheckBox ID="chkEnable" runat="server" Text="Enable Performance Analytics" />
                                    </td>
                                </tr>
                                <tr><td>&nbsp;</td><td>&nbsp;</td></tr>
                                <tr>
                                    <td class="col1">
                                        Item to Track:
                                    </td>
                                    <td class="col2">
                                        <asp:DropDownList ID="ddlPLUs" runat="server" Width="200px" class="visible"></asp:DropDownList>
                                        <asp:DropDownList ID="ddlDepts" runat="server" Width="200px" class="invisible"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td class="col2">
                                        <asp:CheckBox ID="chkTrackDepts" runat="server" Text="Track by Department" onclick="changeTracked()" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="col1">
                                        Item Sell-Through Target:
                                    </td>
                                    <td class="col2" style="text-align:center">
                                        <asp:SliderExtender ID="sldSellThru" runat="server" Minimum="0" Maximum="100" length="200" BoundControlID="txtSellThru" TargetControlID="txtSellThruSlider" Steps="100"></asp:SliderExtender>
                                        <asp:TextBox ID="txtSellThruSlider" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="txtSellThru" width="40px" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr><td>&nbsp;</td><td>&nbsp;</td></tr>
                                <tr>
                                    <td class="col1">
                                        Average Ticket Target:
                                    </td>
                                    <td class="col2">
                                        <asp:TextBox ID="txtAvgTicket" runat="server" Width="200px"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="ftbAvgTicket" runat="server" TargetControlID="txtAvgTicket" FilterType="Numbers,Custom" ValidChars="."></asp:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="col1">
                                        &nbsp;
                                    </td>
                                    <td class="col2">
                                        <small><asp:Label ID="lblAvgTicket" runat="server"></asp:Label></small>
                                    </td>
                                </tr>
                                <tr><td>&nbsp;</td><td>&nbsp;</td></tr>
                                <tr>
                                    <td class="col1" colspan="2" style="text-align:center">
                                        <asp:Button ID="btnSave" Text="Save" Width="80px" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
                <asp:Label ID="lblErrors" runat="server" ForeColor="Red"></asp:Label>
            </div>
        </form>
    </body>
</html>
