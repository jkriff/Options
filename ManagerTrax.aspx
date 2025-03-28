<%@ Page EnableEventValidation="false" Language="VB" AutoEventWireup="false" CodeFile="ManagerTrax.aspx.vb" Inherits="ManagerTrax" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>ManagerTrax Message Scheduler</title>
        <style type="text/css"">
        </style>
        <script type="text/javascript" language="javascript">
        <!--
            function enableDisableItems() {
                if (document.getElementById("trEditSchedule") != null) {
                    if (document.getElementById("chkSMSDestination").checked)
                        document.getElementById("txtSMSDestination").disabled = false;
                    else
                        document.getElementById("txtSMSDestination").disabled = true;

                    if (document.getElementById("chkEmailDestination").checked)
                        document.getElementById("txtEmailDestination").disabled = false;
                    else
                        document.getElementById("txtEmailDestination").disabled = true;

                    if (document.getElementById("chkStoreDestination").checked) {
                        document.getElementById("ddlManagers").disabled = false;
                        document.getElementById("txtTimeout").disabled = false;
                        document.getElementById("chkAlertOnTimeout").disabled = false;
                        if (document.getElementById("chkAlertOnTimeout").checked) {
                            document.getElementById("chkSMSTimeoutAlert").disabled = false;
                            if (document.getElementById("chkSMSTimeoutAlert").checked)
                                document.getElementById("txtSMSTimeoutAlert").disabled = false;
                            else
                                document.getElementById("txtSMSTimeoutAlert").disabled = true;
                            document.getElementById("chkEmailTimeoutAlert").disabled = false;
                            if (document.getElementById("chkEmailTimeoutAlert").checked)
                                document.getElementById("txtEmailTimeoutAlert").disabled = false;
                            else
                                document.getElementById("txtEmailTimeoutAlert").disabled = true;
                        }
                        else {
                            document.getElementById("chkSMSTimeoutAlert").disabled = true;
                            document.getElementById("chkEmailTimeoutAlert").disabled = true;
                            document.getElementById("txtSMSTimeoutAlert").disabled = true;
                            document.getElementById("txtEmailTimeoutAlert").disabled = true;
                        }
                    }
                    else {
                        document.getElementById("ddlManagers").disabled = true;
                        document.getElementById("txtTimeout").disabled = true;
                        document.getElementById("chkAlertOnTimeout").disabled = true;
                        document.getElementById("chkSMSTimeoutAlert").disabled = true;
                        document.getElementById("chkEmailTimeoutAlert").disabled = true;
                        document.getElementById("txtSMSTimeoutAlert").disabled = true;
                        document.getElementById("txtEmailTimeoutAlert").disabled = true;
                    }

                    if (document.getElementById("rdoScheduleWeekly").checked) {
                        document.getElementById("chkScheduleWeeklyMonday").disabled = false;
                        document.getElementById("chkScheduleWeeklyTuesday").disabled = false;
                        document.getElementById("chkScheduleWeeklyWednesday").disabled = false;
                        document.getElementById("chkScheduleWeeklyThursday").disabled = false;
                        document.getElementById("chkScheduleWeeklyFriday").disabled = false;
                        document.getElementById("chkScheduleWeeklySaturday").disabled = false;
                        document.getElementById("chkScheduleWeeklySunday").disabled = false;
                    }
                    else {
                        document.getElementById("chkScheduleWeeklyMonday").disabled = true;
                        document.getElementById("chkScheduleWeeklyTuesday").disabled = true;
                        document.getElementById("chkScheduleWeeklyWednesday").disabled = true;
                        document.getElementById("chkScheduleWeeklyThursday").disabled = true;
                        document.getElementById("chkScheduleWeeklyFriday").disabled = true;
                        document.getElementById("chkScheduleWeeklySaturday").disabled = true;
                        document.getElementById("chkScheduleWeeklySunday").disabled = true;
                    }

                    if (document.getElementById("rdoScheduleMonthlyDate").checked) {
                        document.getElementById("ddlScheduleMonthlyDate").disabled = false;
                    }
                    else {
                        document.getElementById("ddlScheduleMonthlyDate").disabled = true;
                    }

                    if (document.getElementById("rdoScheduleMonthlyWeekday").checked) {
                        document.getElementById("ddlScheduleMonthlyWeekdayWhich").disabled = false;
                        document.getElementById("ddlScheduleMonthlyWeekday").disabled = false;
                    }
                    else {
                        document.getElementById("ddlScheduleMonthlyWeekdayWhich").disabled = true;
                        document.getElementById("ddlScheduleMonthlyWeekday").disabled = true;
                    }

                    if (document.getElementById("rdoScheduleYearly").checked) {
                        document.getElementById("ddlScheduleYearlyMonth").disabled = false;
                        document.getElementById("ddlScheduleYearlyDate").disabled = false;
                    }
                    else {
                        document.getElementById("ddlScheduleYearlyMonth").disabled = true;
                        document.getElementById("ddlScheduleYearlyDate").disabled = true;
                    }

                    if (document.getElementById("rdoScheduleOneTime").checked) {
                        document.getElementById("ddlScheduleOneTimeMonth").disabled = false;
                        document.getElementById("ddlScheduleOneTimeDate").disabled = false;
                        document.getElementById("ddlScheduleOneTimeYear").disabled = false;
                    }
                    else {
                        document.getElementById("ddlScheduleOneTimeMonth").disabled = true;
                        document.getElementById("ddlScheduleOneTimeDate").disabled = true;
                        document.getElementById("ddlScheduleOneTimeYear").disabled = true;
                    }

                    if (document.getElementById("chkLimitedTime").checked) {
                        document.getElementById("ddlStartDateMonth").disabled = false;
                        document.getElementById("ddlStartDateDate").disabled = false;
                        document.getElementById("ddlStartDateYear").disabled = false;
                        document.getElementById("ddlEndDateMonth").disabled = false;
                        document.getElementById("ddlEndDateDate").disabled = false;
                        document.getElementById("ddlEndDateYear").disabled = false;
                    }
                    else {
                        document.getElementById("ddlStartDateMonth").disabled = true;
                        document.getElementById("ddlStartDateDate").disabled = true;
                        document.getElementById("ddlStartDateYear").disabled = true;
                        document.getElementById("ddlEndDateMonth").disabled = true;
                        document.getElementById("ddlEndDateDate").disabled = true;
                        document.getElementById("ddlEndDateYear").disabled = true;
                    }
                }
            }

            function validateData() {
                if (document.getElementById("txtCustomMessage") != null) {
                    if (document.getElementById("txtCustomMessage").value.replace(/^\s+|\s+$/g, "") == "") {
                        alert("Missing Required Value: Custom Message Text");
                        return false;
                    }
                }

                if (!(document.getElementById("chkSMSDestination").checked || document.getElementById("chkEmailDestination").checked || document.getElementById("chkStoreDestination").checked)) {
                    alert("You must select at least one delivery option for this message");
                    return false;
                }

                if (document.getElementById("chkSMSDestination").checked) {
                    if (document.getElementById("hideSMSLicense").value == "False") {
                        alert("You are not currently licensed to use SMS Message Delivery\nPlease contact Xoikos Sales Support to enable this option");
                        return false;
                    }

                    if (document.getElementById("txtSMSDestination").value.replace(/^\s+|\s+$/g, "") == "") {
                        alert("Missing Required Information: SMS Message Delivery Destination");
                        return false;
                    }
                    else {
                        var smsDests = document.getElementById("txtSMSDestination").value.replace(/^\s+|\s+$/g, "").split(";");
                        for (i = 0; i < smsDests.length; i++) {
                            var smsDest = smsDests[i].replace("-", "").replace(".", "").replace(" ", "");
                            if (smsDest.length != 10 || isNaN(smsDest)) {
                                alert("SMS Delivery Destinations must be 10 digit numeric entries");
                                return false;
                            }
                        }
                    }
                }

                if (document.getElementById("chkEmailDestination").checked) {
                    if (document.getElementById("txtEmailDestination").value.replace(/^\s+|\s+$/g, "") == "") {
                        alert("Missing Required Information: Email Message Delivery Destination");
                        return false;
                    }
                    else {
                        var emlDests = document.getElementById("txtEmailDestination").value.replace(/^\s+|\s+$/g, "").split(";");
                        for (i = 0; i < emlDests.length; i++) {
                            var atLoc = emlDests[i].indexOf("@");
                            var dotLoc = emlDests[i].lastIndexOf(".");
                            if (atLoc < 1 || dotLoc < 1 || dotLoc < atLoc) {
                                alert("Email Delivery Destinations must conform to the pattern 'xxx@yyy.zzz'");
                                return false;
                            }
                        }
                    }
                }

                if (document.getElementById("chkStoreDestination").checked) {
                    if (document.getElementById("ddlManagers").selectedIndex < 0) {
                        alert("Missing Required Information: Store Message Delivery Destination");
                        return false;
                    }

                    if (document.getElementById("txtTimeout").value.replace(/^\s+|\s+$/g, "") == "") {
                        alert("Missing Required Information: Store Message Timeout Value");
                        return false;
                    }
                    else {
                        if (isNaN(document.getElementById("txtTimeout").value)) {
                            alert("Store Message Timeout Value must be numeric");
                            return false;
                        }

                        if (document.getElementById("txtTimeout").value < 5 || document.getElementById("txtTimeout").value > 120) {
                            alert("Store Message Timeout Value must be between 5 and 120 minutes");
                            return false;
                        }
                    }

                    if (document.getElementById("chkAlertOnTimeout").checked) {
                        if (!(document.getElementById("chkSMSTimeoutAlert").checked || document.getElementById("chkEmailTimeoutAlert").checked)) {
                            alert("You must select at least one delivery option for your timeout alert");
                            return false;
                        }

                        if (document.getElementById("chkSMSTimeoutAlert").checked) {
                            if (document.getElementById("hideSMSLicense").value == "False") {
                                alert("You are not currently licensed to use SMS Message Delivery\nPlease contact Xoikos Sales Support to enable this option");
                                return false;
                            }

                            if (document.getElementById("txtSMSTimeoutAlert").value.replace(/^\s+|\s+$/g, "") == "") {
                                alert("Missing Required Information: SMS Alert Delivery Destination");
                                return false;
                            }
                            else {
                                var smsDest = document.getElementById("txtSMSTimeoutAlert").value.replace(/^\s+|\s+$/g, "").replace("-", "").replace(".", "").replace(" ", "");
                                if (smsDest.length != 10 || isNaN(smsDest)) {
                                    alert("SMS Alert Destination must be a 10 digit numeric entry");
                                    return false;
                                }
                            }
                        }

                        if (document.getElementById("chkEmailTimeoutAlert").checked) {
                            if (document.getElementById("txtEmailTimeoutAlert").value.replace(/^\s+|\s+$/g, "") == "") {
                                alert("Missing Required Information: Email Alert Delivery Destination");
                                return false;
                            }
                            else {
                                var emlDest = document.getElementById("txtEmailTimeoutAlert").value.replace(/^\s+|\s+$/g, "");
                                var atLoc = emlDest.indexOf("@");
                                var dotLoc = emlDest.lastIndexOf(".");
                                if (atLoc < 1 || dotLoc < 1 || dotLoc < atLoc) {
                                    alert("Email Alert Destination must conform to the pattern 'xxx@yyy.zzz'");
                                    return false;
                                }
                            }
                        }
                    }
                }

                if (document.getElementById("ddlScheduleTimeHour").selectedIndex < 0) {
                    alert("Missing Required Information: Schedule Time Hour");
                    return false;
                }

                if (document.getElementById("ddlScheduleTimeMinute").selectedIndex < 0) {
                    alert("Missing Required Information: Schedule Time Hour");
                    return false;
                }

                if (document.getElementById("ddlScheduleTimeAMPM").selectedIndex < 0) {
                    alert("Missing Required Information: Schedule Time Hour");
                    return false;
                }

                if (document.getElementById("rdoScheduleWeekly").checked) {
                    if (!(document.getElementById("chkScheduleWeeklyMonday").checked || document.getElementById("chkScheduleWeeklyTuesday").checked || document.getElementById("chkScheduleWeeklyWednesday").checked || document.getElementById("chkScheduleWeeklyThursday").checked || document.getElementById("chkScheduleWeeklyFriday").checked || document.getElementById("chkScheduleWeeklySaturday").checked || document.getElementById("chkScheduleWeeklySunday").checked)) {
                        alert("At least one weekday must be selected for Weekly message delivery");
                        return false;
                    }
                }
                else if (document.getElementById("rdoScheduleMonthlyDate").checked) {
                    if (document.getElementById("ddlScheduleMonthlyDate").selectedIndex < 0) {
                        alert("Missing Required Information: Monthly Delivery Date");
                        return false;
                    }
                }
                else if (document.getElementById("rdoScheduleMonthlyWeekday").checked) {
                    if (document.getElementById("ddlScheduleMonthlyWeekdayWhich").selectedIndex < 0) {
                        alert("Missing Required Information: Monthly Delivery Weekday Instance");
                        return false;
                    }

                    if (document.getElementById("ddlScheduleMonthlyWeekday").selectedIndex < 0) {
                        alert("Missing Required Information: Monthly Delivery Weekday");
                        return false;
                    }
                }
                else if (document.getElementById("rdoScheduleYearly").checked) {
                    if (document.getElementById("ddlScheduleYearlyMonth").selectedIndex < 0) {
                        alert("Missing Required Information: Yearly Delivery Month");
                        return false;
                    }

                    if (document.getElementById("ddlScheduleYearlyDate").selectedIndex < 0) {
                        alert("Missing Required Information: Yearly Delivery Date");
                        return false;
                    }
                }
                else if (document.getElementById("rdoScheduleOneTime").checked) {
                    if (document.getElementById("ddlScheduleOneTimeMonth").selectedIndex < 0) {
                        alert("Missing Required Information: One-Time Delivery Month");
                        return false;
                    }

                    if (document.getElementById("ddlScheduleOneTimeDate").selectedIndex < 0) {
                        alert("Missing Required Information: One-Time Delivery Date");
                        return false;
                    }

                    if (document.getElementById("ddlScheduleOneTimeYear").selectedIndex < 0) {
                        alert("Missing Required Information: One-Time Delivery Year");
                        return false;
                    }
                }
                else {
                    alert("Missing Required Information: Schedule Date Selection");
                    return false;
                }

                if (document.getElementById("chkLimitedTime").checked) {
                    if (document.getElementById("ddlStartDateMonth").selectedIndex < 0) {
                        alert("Missing Required Information: Limited Time Start Date Month");
                        return false;
                    }

                    if (document.getElementById("ddlStartDateDate").selectedIndex < 0) {
                        alert("Missing Required Information: Limited Time Start Date Day");
                        return false;
                    }

                    if (document.getElementById("ddlStartDateYear").selectedIndex < 0) {
                        alert("Missing Required Information: Limited Time Start Date Year");
                        return false;
                    }

                    if (document.getElementById("ddlEndDateMonth").selectedIndex < 0) {
                        alert("Missing Required Information: Limited Time End Date Month");
                        return false;
                    }

                    if (document.getElementById("ddlEndDateDate").selectedIndex < 0) {
                        alert("Missing Required Information: Limited Time End Date Day");
                        return false;
                    }

                    if (document.getElementById("ddlEndDateYear").selectedIndex < 0) {
                        alert("Missing Required Information: Limited Time End Date Year");
                        return false;
                    }
                }

                return true;
            }
        -->
        </script>
    </head>
    <body onload="enableDisableItems();">
        <form id="form1" runat="server">
            <input id="hideStore" type="hidden" name="hideStore" runat="server" /> 
            <input id="hideSchedule" type="hidden" name="hideSchedule" runat="server" />
            <input id="hidePreviousSelection" type="hidden" name="hidePreviousSelection" runat="server" />
            <input id="hideSMSLicense" type="hidden" name="hideSMSLicense" runat="server" />
            <div>
                <h2>ManagerTrax</h2>
                <h4><asp:Label ID="lblStoreDisplay" runat="server"></asp:Label></h4>
                <asp:Label ID="lblErrors" runat="server" ForeColor="Red"></asp:Label>
                <table ID="tblMain" runat="server" width="100%">
                    <tr id="trInstructions1">
                        <td id="tdInstructions1">
                            <center>
                                <table>
                                    <tr>
                                        <td rowspan="2"><b>(1)</b></td>
                                        <td align="left">First, select a predefined message below.</td>
                                    </tr>
                                    <tr>
                                        <td align="left">Or, select <b>Custom Message</b> to create a new one.</td>
                                    </tr>
                                </table>
                            </center>
                        </td>
                        <td id="tdInstructions2">
                            <center>
                                <table id="tblInstructions21" runat="server" visible="false">
                                    <tr>
                                        <td rowspan="2"><b>(2)</b></td>
                                        <td align="left">Now click on <b>Edit</b> to edit an existing schedule for this message</td>
                                    </tr>
                                    <tr><td align="left">Or click <b>Add New</b> to create a new one</td></tr>
                                </table>
                                <table id="tblInstructions22" runat="server" visible="false">
                                    <tr>
                                        <td><b>(2)</b></td>
                                        <td align="left">Now click on <b>Add New</b> to create your message</td>
                                    </tr>
                                </table>
                            </center>
                        </td>
                    </tr>
                    <tr id="trInstructions2" visible="false">
                        <td colspan="2">
                            <asp:Button ID="btnGoBack" runat="server" Text="Go Back" /><br />
                            <center>
                                <table runat="server">
                                    <tr>
                                        <td id="td2or3"><b>(3)</b></td>
                                        <td>Now schedule your message</td>
                                    </tr>
                                </table>
                            </center>
                        </td>
                    </tr>
                    <tr id="trSelectSchedule">
                        <td width="50%">
                            <asp:ListBox ID="lstPredefinedMessages" runat="server" Height="360px" Width="100%" AutoPostBack="true"></asp:ListBox>
                        </td>
                        <td id="tdSchedules" valign="top">
                            <div id="divSchedules" runat="server" style="width:100%;height:336px;overflow:auto;">
                                <asp:datagrid Width="100%" id="dgSchedules" runat="server" PageSize="500" AutoGenerateColumns="False" CellPadding="3" OnItemCommand="dgSchedules_ButtonClick">
                                    <Columns>
                                        <asp:BoundColumn DataField="ScheduleID" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Schedule" HeaderText="Schedule"></asp:BoundColumn>
                                        <asp:ButtonColumn ButtonType="PushButton" Text="Edit" CommandName="Edit" ItemStyle-Width="5%"></asp:ButtonColumn>
                                        <asp:ButtonColumn ButtonType="PushButton" Text="Delete" CommandName="Delete" ItemStyle-Width="5%"></asp:ButtonColumn>
                                    </Columns>
                                </asp:datagrid>
                            </div>
                            <center><asp:Button ID="btnAddNew" Text="Add New" runat="server" /></center>
                        </td>
                    </tr>
                    <tr id="trEditSchedule">
                        <td colspan="2">
                            <table id="tblEdit" runat="server" width="100%">
                                <tr id="trCustomMessage">
                                    <td>
                                        <asp:Label runat="server" ID="lblCustomMessage"><strong>Custom Message Text:</strong></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtCustomMessage" runat="server" Width="100%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr id="trMessageText">
                                    <td>
                                        <asp:Label runat="server" ID="Label1"><strong>Message Text:</strong></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:Label runat="server" ID="lblMessageText"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>Destination:</strong>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkSMSDestination" runat="server" Text="SMS" onClick="enableDisableItems();" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSMSDestination" Width="100%" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr><td colspan="2"></td><td><small>Separate multiple SMS destinations with semicolons (;)</small></td></tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkEmailDestination" runat="server" Text="E-mail" onClick="enableDisableItems();" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEmailDestination" Width="100%" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr><td colspan="2"></td><td><small>Separate multiple email destinations with semicolons (;)</small></td></tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:CheckBox ID="chkStoreDestination" runat="server" Text="Store" onClick="enableDisableItems();" />
                                    </td>
                                    <td>Deliver to:&nbsp;
                                        <asp:DropDownList ID="ddlManagers" runat="server"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td colspan="2">
                                        <table width="100%">
                                            <tr>
                                                <td width="5%"></td>
                                                <td colspan="4">
                                                    Store-delivered message times out and is marked unacknowledged after&nbsp;<asp:TextBox ID="txtTimeout" runat="server" Width="100">30</asp:TextBox>&nbsp;minutes.
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td width="5%"></td>
                                                <td colspan="3">
                                                    <asp:CheckBox ID="chkAlertOnTimeout" runat="server" Text="Alert me if the store-delivered message times out via" onClick="enableDisableItems();" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td><td></td>
                                                <td width="5%"></td>
                                                <td><asp:CheckBox ID="chkSMSTimeoutAlert" runat="server" Text="SMS" onClick="enableDisableItems();" /></td>
                                                <td><asp:TextBox ID="txtSMSTimeoutAlert" runat="server" Width="100%"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td></td><td></td><td></td>
                                                <td><asp:CheckBox ID="chkEmailTimeoutAlert" runat="server" Text="E-mail" onClick="enableDisableItems();" /></td>
                                                <td><asp:TextBox ID="txtEmailTimeoutAlert" runat="server" Width="100%"></asp:TextBox></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr><td colspan="3">&nbsp;</td></tr>
                                <tr>
                                    <td><b>Delivery Time:</b></td>
                                    <td colspan="2"><b>Date Schedule:</b></td>
                                </tr>
                                <tr>
                                    <td>
                                        <center>
                                            <asp:DropDownList ID="ddlScheduleTimeHour" runat="server">
                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                <asp:ListItem Value="3">3</asp:ListItem>
                                                <asp:ListItem Value="4">4</asp:ListItem>
                                                <asp:ListItem Value="5">5</asp:ListItem>
                                                <asp:ListItem Value="6">6</asp:ListItem>
                                                <asp:ListItem Value="7">7</asp:ListItem>
                                                <asp:ListItem Value="8">8</asp:ListItem>
                                                <asp:ListItem Value="9">9</asp:ListItem>
                                                <asp:ListItem Value="10">10</asp:ListItem>
                                                <asp:ListItem Value="11">11</asp:ListItem>
                                                <asp:ListItem Value="0">12</asp:ListItem>
                                            </asp:DropDownList>:
                                            <asp:DropDownList ID="ddlScheduleTimeMinute" runat="server">
                                                <asp:ListItem Value="00">00</asp:ListItem>
                                                <asp:ListItem Value="01">01</asp:ListItem>
                                                <asp:ListItem Value="02">02</asp:ListItem>
                                                <asp:ListItem Value="03">03</asp:ListItem>
                                                <asp:ListItem Value="04">04</asp:ListItem>
                                                <asp:ListItem Value="05">05</asp:ListItem>
                                                <asp:ListItem Value="06">06</asp:ListItem>
                                                <asp:ListItem Value="07">07</asp:ListItem>
                                                <asp:ListItem Value="08">08</asp:ListItem>
                                                <asp:ListItem Value="09">09</asp:ListItem>
                                                <asp:ListItem Value="10">10</asp:ListItem>
                                                <asp:ListItem Value="11">11</asp:ListItem>
                                                <asp:ListItem Value="12">12</asp:ListItem>
                                                <asp:ListItem Value="13">13</asp:ListItem>
                                                <asp:ListItem Value="14">14</asp:ListItem>
                                                <asp:ListItem Value="15">15</asp:ListItem>
                                                <asp:ListItem Value="16">16</asp:ListItem>
                                                <asp:ListItem Value="17">17</asp:ListItem>
                                                <asp:ListItem Value="18">18</asp:ListItem>
                                                <asp:ListItem Value="19">19</asp:ListItem>
                                                <asp:ListItem Value="20">20</asp:ListItem>
                                                <asp:ListItem Value="21">21</asp:ListItem>
                                                <asp:ListItem Value="22">22</asp:ListItem>
                                                <asp:ListItem Value="23">23</asp:ListItem>
                                                <asp:ListItem Value="24">24</asp:ListItem>
                                                <asp:ListItem Value="25">25</asp:ListItem>
                                                <asp:ListItem Value="26">26</asp:ListItem>
                                                <asp:ListItem Value="27">27</asp:ListItem>
                                                <asp:ListItem Value="28">28</asp:ListItem>
                                                <asp:ListItem Value="29">29</asp:ListItem>
                                                <asp:ListItem Value="30">30</asp:ListItem>
                                                <asp:ListItem Value="31">31</asp:ListItem>
                                                <asp:ListItem Value="32">32</asp:ListItem>
                                                <asp:ListItem Value="33">33</asp:ListItem>
                                                <asp:ListItem Value="34">34</asp:ListItem>
                                                <asp:ListItem Value="35">35</asp:ListItem>
                                                <asp:ListItem Value="36">36</asp:ListItem>
                                                <asp:ListItem Value="37">37</asp:ListItem>
                                                <asp:ListItem Value="38">38</asp:ListItem>
                                                <asp:ListItem Value="39">39</asp:ListItem>
                                                <asp:ListItem Value="40">40</asp:ListItem>
                                                <asp:ListItem Value="41">41</asp:ListItem>
                                                <asp:ListItem Value="42">42</asp:ListItem>
                                                <asp:ListItem Value="43">43</asp:ListItem>
                                                <asp:ListItem Value="44">44</asp:ListItem>
                                                <asp:ListItem Value="45">45</asp:ListItem>
                                                <asp:ListItem Value="46">46</asp:ListItem>
                                                <asp:ListItem Value="47">47</asp:ListItem>
                                                <asp:ListItem Value="48">48</asp:ListItem>
                                                <asp:ListItem Value="49">49</asp:ListItem>
                                                <asp:ListItem Value="50">50</asp:ListItem>
                                                <asp:ListItem Value="51">51</asp:ListItem>
                                                <asp:ListItem Value="52">52</asp:ListItem>
                                                <asp:ListItem Value="53">53</asp:ListItem>
                                                <asp:ListItem Value="54">54</asp:ListItem>
                                                <asp:ListItem Value="55">55</asp:ListItem>
                                                <asp:ListItem Value="56">56</asp:ListItem>
                                                <asp:ListItem Value="57">57</asp:ListItem>
                                                <asp:ListItem Value="58">58</asp:ListItem>
                                                <asp:ListItem Value="59">59</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlScheduleTimeAMPM" runat="server">
                                                <asp:ListItem Value="1">am</asp:ListItem>
                                                <asp:ListItem Value="2">pm</asp:ListItem>
                                            </asp:DropDownList>
                                        </center>
                                    </td>
                                    <td colspan="2">
                                        <table width="100%">
                                            <tr>
                                                <td width="35%">
                                                    <strong><asp:RadioButton ID="rdoScheduleWeekly" GroupName="rgSchedule" runat="server" Text="Weekly" onClick="enableDisableItems();" /></strong>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkScheduleWeeklyMonday" runat="server" Text="Monday" />
                                                    <asp:CheckBox ID="chkScheduleWeeklyTuesday" runat="server" Text="Tuesday" />
                                                    <asp:CheckBox ID="chkScheduleWeeklyWednesday" runat="server" Text="Wednesday" />
                                                    <asp:CheckBox ID="chkScheduleWeeklyThursday" runat="server" Text="Thursday" /><br />
                                                    <asp:CheckBox ID="chkScheduleWeeklyFriday" runat="server" Text="Friday" />
                                                    <asp:CheckBox ID="chkScheduleWeeklySaturday" runat="server" Text="Saturday" />
                                                    <asp:CheckBox ID="chkScheduleWeeklySunday" runat="server" Text="Sunday" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="25%">
                                                    <strong><asp:RadioButton ID="rdoScheduleMonthlyDate" GroupName="rgSchedule" runat="server" Text="Day of Month" onClick="enableDisableItems();" /></strong>
                                                </td>
                                                <td>
                                                    The&nbsp;
                                                    <asp:DropDownList ID="ddlScheduleMonthlyDate" runat="server">
                                                        <asp:ListItem Value="1">1st</asp:ListItem>
                                                        <asp:ListItem Value="2">2nd</asp:ListItem>
                                                        <asp:ListItem Value="3">3rd</asp:ListItem>
                                                        <asp:ListItem Value="4">4th</asp:ListItem>
                                                        <asp:ListItem Value="5">5th</asp:ListItem>
                                                        <asp:ListItem Value="6">6th</asp:ListItem>
                                                        <asp:ListItem Value="7">7th</asp:ListItem>
                                                        <asp:ListItem Value="8">8th</asp:ListItem>
                                                        <asp:ListItem Value="9">9th</asp:ListItem>
                                                        <asp:ListItem Value="10">10th</asp:ListItem>
                                                        <asp:ListItem Value="11">11th</asp:ListItem>
                                                        <asp:ListItem Value="12">12th</asp:ListItem>
                                                        <asp:ListItem Value="13">13th</asp:ListItem>
                                                        <asp:ListItem Value="14">14th</asp:ListItem>
                                                        <asp:ListItem Value="15">15th</asp:ListItem>
                                                        <asp:ListItem Value="16">16th</asp:ListItem>
                                                        <asp:ListItem Value="17">17th</asp:ListItem>
                                                        <asp:ListItem Value="18">18th</asp:ListItem>
                                                        <asp:ListItem Value="19">19th</asp:ListItem>
                                                        <asp:ListItem Value="20">20th</asp:ListItem>
                                                        <asp:ListItem Value="21">21st</asp:ListItem>
                                                        <asp:ListItem Value="22">22nd</asp:ListItem>
                                                        <asp:ListItem Value="23">23rd</asp:ListItem>
                                                        <asp:ListItem Value="24">24th</asp:ListItem>
                                                        <asp:ListItem Value="25">25th</asp:ListItem>
                                                        <asp:ListItem Value="26">26th</asp:ListItem>
                                                        <asp:ListItem Value="27">27th</asp:ListItem>
                                                        <asp:ListItem Value="28">28th</asp:ListItem>
                                                    </asp:DropDownList>
                                                    &nbsp;of every month.
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="25%">
                                                    <strong><asp:RadioButton ID="rdoScheduleMonthlyWeekday" GroupName="rgSchedule" runat="server" Text="Weekday of Month" onClick="enableDisableItems();" /></strong>
                                                </td>
                                                <td>
                                                    The&nbsp;
                                                    <asp:DropDownList ID="ddlScheduleMonthlyWeekdayWhich" runat="server">
                                                        <asp:ListItem Value="1">1st</asp:ListItem>
                                                        <asp:ListItem Value="2">2nd</asp:ListItem>
                                                        <asp:ListItem Value="3">3rd</asp:ListItem>
                                                        <asp:ListItem Value="4">4th</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="ddlScheduleMonthlyWeekday" runat="server">
                                                        <asp:ListItem Value="1">Monday</asp:ListItem>
                                                        <asp:ListItem Value="2">Tuesday</asp:ListItem>
                                                        <asp:ListItem Value="3">Wednesday</asp:ListItem>
                                                        <asp:ListItem Value="4">Thursday</asp:ListItem>
                                                        <asp:ListItem Value="5">Friday</asp:ListItem>
                                                        <asp:ListItem Value="6">Saturday</asp:ListItem>
                                                        <asp:ListItem Value="0">Sunday</asp:ListItem>
                                                    </asp:DropDownList>
                                                    &nbsp;of every month.
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="25%">
                                                    <strong><asp:RadioButton ID="rdoScheduleYearly" GroupName="rgSchedule" runat="server" Text="Yearly" onClick="enableDisableItems();" /></strong>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlScheduleYearlyMonth" runat="server">
                                                        <asp:ListItem Value="1">January</asp:ListItem>
                                                        <asp:ListItem Value="2">February</asp:ListItem>
                                                        <asp:ListItem Value="3">March</asp:ListItem>
                                                        <asp:ListItem Value="4">April</asp:ListItem>
                                                        <asp:ListItem Value="5">May</asp:ListItem>
                                                        <asp:ListItem Value="6">June</asp:ListItem>
                                                        <asp:ListItem Value="7">July</asp:ListItem>
                                                        <asp:ListItem Value="8">August</asp:ListItem>
                                                        <asp:ListItem Value="9">September</asp:ListItem>
                                                        <asp:ListItem Value="10">October</asp:ListItem>
                                                        <asp:ListItem Value="11">November</asp:ListItem>
                                                        <asp:ListItem Value="12">December</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="ddlScheduleYearlyDate" runat="server">
                                                        <asp:ListItem Value="1">1st</asp:ListItem>
                                                        <asp:ListItem Value="2">2nd</asp:ListItem>
                                                        <asp:ListItem Value="3">3rd</asp:ListItem>
                                                        <asp:ListItem Value="4">4th</asp:ListItem>
                                                        <asp:ListItem Value="5">5th</asp:ListItem>
                                                        <asp:ListItem Value="6">6th</asp:ListItem>
                                                        <asp:ListItem Value="7">7th</asp:ListItem>
                                                        <asp:ListItem Value="8">8th</asp:ListItem>
                                                        <asp:ListItem Value="9">9th</asp:ListItem>
                                                        <asp:ListItem Value="10">10th</asp:ListItem>
                                                        <asp:ListItem Value="11">11th</asp:ListItem>
                                                        <asp:ListItem Value="12">12th</asp:ListItem>
                                                        <asp:ListItem Value="13">13th</asp:ListItem>
                                                        <asp:ListItem Value="14">14th</asp:ListItem>
                                                        <asp:ListItem Value="15">15th</asp:ListItem>
                                                        <asp:ListItem Value="16">16th</asp:ListItem>
                                                        <asp:ListItem Value="17">17th</asp:ListItem>
                                                        <asp:ListItem Value="18">18th</asp:ListItem>
                                                        <asp:ListItem Value="19">19th</asp:ListItem>
                                                        <asp:ListItem Value="20">20th</asp:ListItem>
                                                        <asp:ListItem Value="21">21st</asp:ListItem>
                                                        <asp:ListItem Value="22">22nd</asp:ListItem>
                                                        <asp:ListItem Value="23">23rd</asp:ListItem>
                                                        <asp:ListItem Value="24">24th</asp:ListItem>
                                                        <asp:ListItem Value="25">25th</asp:ListItem>
                                                        <asp:ListItem Value="26">26th</asp:ListItem>
                                                        <asp:ListItem Value="27">27th</asp:ListItem>
                                                        <asp:ListItem Value="28">28th</asp:ListItem>
                                                        <asp:ListItem Value="29">29th</asp:ListItem>
                                                        <asp:ListItem Value="30">30th</asp:ListItem>
                                                        <asp:ListItem Value="31">31st</asp:ListItem>
                                                    </asp:DropDownList>
                                                    &nbsp;of every year.
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="25%">
                                                    <strong><asp:RadioButton ID="rdoScheduleOneTime" GroupName="rgSchedule" runat="server" Text="One-Time" onClick="enableDisableItems();" /></strong>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlScheduleOneTimeMonth" runat="server">
                                                        <asp:ListItem Value="1">January</asp:ListItem>
                                                        <asp:ListItem Value="2">February</asp:ListItem>
                                                        <asp:ListItem Value="3">March</asp:ListItem>
                                                        <asp:ListItem Value="4">April</asp:ListItem>
                                                        <asp:ListItem Value="5">May</asp:ListItem>
                                                        <asp:ListItem Value="6">June</asp:ListItem>
                                                        <asp:ListItem Value="7">July</asp:ListItem>
                                                        <asp:ListItem Value="8">August</asp:ListItem>
                                                        <asp:ListItem Value="9">September</asp:ListItem>
                                                        <asp:ListItem Value="10">October</asp:ListItem>
                                                        <asp:ListItem Value="11">November</asp:ListItem>
                                                        <asp:ListItem Value="12">December</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="ddlScheduleOneTimeDate" runat="server">
                                                        <asp:ListItem Value="1">1st</asp:ListItem>
                                                        <asp:ListItem Value="2">2nd</asp:ListItem>
                                                        <asp:ListItem Value="3">3rd</asp:ListItem>
                                                        <asp:ListItem Value="4">4th</asp:ListItem>
                                                        <asp:ListItem Value="5">5th</asp:ListItem>
                                                        <asp:ListItem Value="6">6th</asp:ListItem>
                                                        <asp:ListItem Value="7">7th</asp:ListItem>
                                                        <asp:ListItem Value="8">8th</asp:ListItem>
                                                        <asp:ListItem Value="9">9th</asp:ListItem>
                                                        <asp:ListItem Value="10">10th</asp:ListItem>
                                                        <asp:ListItem Value="11">11th</asp:ListItem>
                                                        <asp:ListItem Value="12">12th</asp:ListItem>
                                                        <asp:ListItem Value="13">13th</asp:ListItem>
                                                        <asp:ListItem Value="14">14th</asp:ListItem>
                                                        <asp:ListItem Value="15">15th</asp:ListItem>
                                                        <asp:ListItem Value="16">16th</asp:ListItem>
                                                        <asp:ListItem Value="17">17th</asp:ListItem>
                                                        <asp:ListItem Value="18">18th</asp:ListItem>
                                                        <asp:ListItem Value="19">19th</asp:ListItem>
                                                        <asp:ListItem Value="20">20th</asp:ListItem>
                                                        <asp:ListItem Value="21">21st</asp:ListItem>
                                                        <asp:ListItem Value="22">22nd</asp:ListItem>
                                                        <asp:ListItem Value="23">23rd</asp:ListItem>
                                                        <asp:ListItem Value="24">24th</asp:ListItem>
                                                        <asp:ListItem Value="25">25th</asp:ListItem>
                                                        <asp:ListItem Value="26">26th</asp:ListItem>
                                                        <asp:ListItem Value="27">27th</asp:ListItem>
                                                        <asp:ListItem Value="28">28th</asp:ListItem>
                                                        <asp:ListItem Value="29">29th</asp:ListItem>
                                                        <asp:ListItem Value="30">30th</asp:ListItem>
                                                        <asp:ListItem Value="31">31st</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="ddlScheduleOneTimeYear" runat="server"></asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr><td colspan="3">&nbsp;</td></tr>
                                <tr>
                                    <td><strong><asp:CheckBox ID="chkLimitedTime" runat="server" Text="Limit Schedule" onClick="enableDisableItems();" /></strong></td>
                                    <td><strong><asp:label ID="lblStartDate" runat="server">Start Date</asp:label></strong></td>
                                    <td>
                                        <asp:DropDownList ID="ddlStartDateMonth" runat="server">
                                            <asp:ListItem Value="1">January</asp:ListItem>
                                            <asp:ListItem Value="2">February</asp:ListItem>
                                            <asp:ListItem Value="3">March</asp:ListItem>
                                            <asp:ListItem Value="4">April</asp:ListItem>
                                            <asp:ListItem Value="5">May</asp:ListItem>
                                            <asp:ListItem Value="6">June</asp:ListItem>
                                            <asp:ListItem Value="7">July</asp:ListItem>
                                            <asp:ListItem Value="8">August</asp:ListItem>
                                            <asp:ListItem Value="9">September</asp:ListItem>
                                            <asp:ListItem Value="10">October</asp:ListItem>
                                            <asp:ListItem Value="11">November</asp:ListItem>
                                            <asp:ListItem Value="12">December</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlStartDateDate" runat="server">
                                            <asp:ListItem Value="1">1st</asp:ListItem>
                                            <asp:ListItem Value="2">2nd</asp:ListItem>
                                            <asp:ListItem Value="3">3rd</asp:ListItem>
                                            <asp:ListItem Value="4">4th</asp:ListItem>
                                            <asp:ListItem Value="5">5th</asp:ListItem>
                                            <asp:ListItem Value="6">6th</asp:ListItem>
                                            <asp:ListItem Value="7">7th</asp:ListItem>
                                            <asp:ListItem Value="8">8th</asp:ListItem>
                                            <asp:ListItem Value="9">9th</asp:ListItem>
                                            <asp:ListItem Value="10">10th</asp:ListItem>
                                            <asp:ListItem Value="11">11th</asp:ListItem>
                                            <asp:ListItem Value="12">12th</asp:ListItem>
                                            <asp:ListItem Value="13">13th</asp:ListItem>
                                            <asp:ListItem Value="14">14th</asp:ListItem>
                                            <asp:ListItem Value="15">15th</asp:ListItem>
                                            <asp:ListItem Value="16">16th</asp:ListItem>
                                            <asp:ListItem Value="17">17th</asp:ListItem>
                                            <asp:ListItem Value="18">18th</asp:ListItem>
                                            <asp:ListItem Value="19">19th</asp:ListItem>
                                            <asp:ListItem Value="20">20th</asp:ListItem>
                                            <asp:ListItem Value="21">21st</asp:ListItem>
                                            <asp:ListItem Value="22">22nd</asp:ListItem>
                                            <asp:ListItem Value="23">23rd</asp:ListItem>
                                            <asp:ListItem Value="24">24th</asp:ListItem>
                                            <asp:ListItem Value="25">25th</asp:ListItem>
                                            <asp:ListItem Value="26">26th</asp:ListItem>
                                            <asp:ListItem Value="27">27th</asp:ListItem>
                                            <asp:ListItem Value="28">28th</asp:ListItem>
                                            <asp:ListItem Value="29">29th</asp:ListItem>
                                            <asp:ListItem Value="30">30th</asp:ListItem>
                                            <asp:ListItem Value="31">31st</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlStartDateYear" runat="server"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td><strong><asp:label ID="lblEndDate" runat="server">End Date</asp:label></strong></td>
                                    <td>
                                        <asp:DropDownList ID="ddlEndDateMonth" runat="server">
                                            <asp:ListItem Value="1">January</asp:ListItem>
                                            <asp:ListItem Value="2">February</asp:ListItem>
                                            <asp:ListItem Value="3">March</asp:ListItem>
                                            <asp:ListItem Value="4">April</asp:ListItem>
                                            <asp:ListItem Value="5">May</asp:ListItem>
                                            <asp:ListItem Value="6">June</asp:ListItem>
                                            <asp:ListItem Value="7">July</asp:ListItem>
                                            <asp:ListItem Value="8">August</asp:ListItem>
                                            <asp:ListItem Value="9">September</asp:ListItem>
                                            <asp:ListItem Value="10">October</asp:ListItem>
                                            <asp:ListItem Value="11">November</asp:ListItem>
                                            <asp:ListItem Value="12">December</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlEndDateDate" runat="server">
                                            <asp:ListItem Value="1">1st</asp:ListItem>
                                            <asp:ListItem Value="2">2nd</asp:ListItem>
                                            <asp:ListItem Value="3">3rd</asp:ListItem>
                                            <asp:ListItem Value="4">4th</asp:ListItem>
                                            <asp:ListItem Value="5">5th</asp:ListItem>
                                            <asp:ListItem Value="6">6th</asp:ListItem>
                                            <asp:ListItem Value="7">7th</asp:ListItem>
                                            <asp:ListItem Value="8">8th</asp:ListItem>
                                            <asp:ListItem Value="9">9th</asp:ListItem>
                                            <asp:ListItem Value="10">10th</asp:ListItem>
                                            <asp:ListItem Value="11">11th</asp:ListItem>
                                            <asp:ListItem Value="12">12th</asp:ListItem>
                                            <asp:ListItem Value="13">13th</asp:ListItem>
                                            <asp:ListItem Value="14">14th</asp:ListItem>
                                            <asp:ListItem Value="15">15th</asp:ListItem>
                                            <asp:ListItem Value="16">16th</asp:ListItem>
                                            <asp:ListItem Value="17">17th</asp:ListItem>
                                            <asp:ListItem Value="18">18th</asp:ListItem>
                                            <asp:ListItem Value="19">19th</asp:ListItem>
                                            <asp:ListItem Value="20">20th</asp:ListItem>
                                            <asp:ListItem Value="21">21st</asp:ListItem>
                                            <asp:ListItem Value="22">22nd</asp:ListItem>
                                            <asp:ListItem Value="23">23rd</asp:ListItem>
                                            <asp:ListItem Value="24">24th</asp:ListItem>
                                            <asp:ListItem Value="25">25th</asp:ListItem>
                                            <asp:ListItem Value="26">26th</asp:ListItem>
                                            <asp:ListItem Value="27">27th</asp:ListItem>
                                            <asp:ListItem Value="28">28th</asp:ListItem>
                                            <asp:ListItem Value="29">29th</asp:ListItem>
                                            <asp:ListItem Value="30">30th</asp:ListItem>
                                            <asp:ListItem Value="31">31st</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlEndDateYear" runat="server"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr><td colspan="3">&nbsp;</td></tr>
                                <tr><td colspan="3" align="center"><asp:Button ID="btnSave" runat="server" Text="Save" /></td></tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </form>
    </body>
</html>
