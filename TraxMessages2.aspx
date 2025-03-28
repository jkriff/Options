<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TraxMessages2.aspx.vb" Inherits="TraxMessages" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
    <head>
        <title>ManagerTrax Message Definition</title>
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
            .scheduleID, .schedulerID, .startTime, .endTime, .generateEvery { text-align: center; }
            .scheduled, .message { text-align: left; }
            .bottomNav { background-color: #92ADD8; font-weight: bold; text-align: center; }
	        .box H4 { PADDING-RIGHT: 5px; PADDING-LEFT: 5px; MARGIN-BOTTOM: 10px; PADDING-BOTTOM: 5px; PADDING-TOP: 5px; BACKGROUND-COLOR: #ebf3f6 }
	        #labelDesc { BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid; WIDTH: 153px; BORDER-BOTTOM: black 1px solid }
	        .clickedRow { BACKGROUND-COLOR: #3fa84b }
            .odd { background: #f0f2f4; }
            h2 { font-family: Verdana, Geneva, sans-serif; }
            a { font-family: Verdana, Geneva, sans-serif; }
            body { font-family: Verdana, Geneva, sans-serif; color: #000; }
            #dgSchedules tr .gridheader {background: #3A4856; padding: 15px 10px; color: #fff; text-align: left; font-weight: normal; text-transform: uppercase;}
            #dgSchedules tr .gridheader a { color: #FFF; }
            #dgSchedules { font-size: 12px; border-top-style: none; border-right-style: none; border-bottom-style: none; border-left-style: none; border-top-width: 0px; border-right-width: 0px; border-bottom-width: 0px; border-left-width: 0px; }
            #dgSchedules .odd td, #dgSchedules td { padding: 5px 2px; border:none; }
            .topEntry tr td .topEntry { font-size: 14px; margin: 0px; padding: 0px; border: 1px double #3A4856; }
            .editProduct { border: 1px double 92ADD8; }
            .topEntry { border: 1px solid #3A4856; margin-top: 10px; margin-right: 20px; margin-bottom: 20px; margin-left: 20px; padding-top: 10px; padding-right: 20px; padding-bottom: 20px; padding-left: 20px; text-align: center; }
            .topEntry tr td .editProduct .editEntry { font-size: 14px; text-align: right; padding: 0px; margin: 5px; }
            .topEntry tr td .editProduct .editEntry tr .col2 { text-align: left; }
            .topEntry tr td .editProduct .editEntry tr .col1 { font-weight: normal; color: #3A4856; }
            select { background-color: #92ADD8; }
            .acc-header { width: 500px; background-color: #3A4856; margin-bottom:2px; padding:15px 10px; color:white; font-weight:normal; font-size:12px; cursor:pointer; text-transform:uppercase; }
            .acc-selected { width: 500px; background-color: #3fa84b; margin-bottom:2px; padding:15px 10px; color:black; font-weight:normal; font-size:12px; cursor:pointer; text-transform:uppercase; }
            .acc-selected { border:solid 1px #666666; }
            .acc-content { width:500px; margin-bottom:2px; padding:2px; }
            .acc-content { border:solid 1px #666666; }
        </style>

        <script type="text/javascript" language="javascript">
		    <!--
		    var clickedRow;
		    function loadItems(row, scheduleID, schedulerID, typeID, dateParameter, startTime, endTime, message, generateEvery)
		        {
			    try
			        {
				    clickedRow.className = clickedRow.oldClass;
    			    }
			    catch(ex){}
			    
			    row.className = "clickedRow";
			    clickedRow = row;
			    
			    var hScheduleID = document.getElementById("hideScheduleID");
			    var oSchedulerID = document.getElementById("ddlSchedulerID");
			    var oStartTimeHour = document.getElementById("ddlStartTimeHour");
			    var oStartTimeMinute = document.getElementById("ddlStartTimeMinute");
			    var oEndTimeHour = document.getElementById("ddlEndTimeHour");
			    var oEndTimeMinute = document.getElementById("ddlEndTimeMinute");
			    var oMessage = document.getElementById("txtMessage");
			    var oGenerateEvery = document.getElementById("ddlGenerateEvery");
    			
			    hScheduleID.value = scheduleID;
			    oSchedulerID.value = schedulerID;
			    $find('accEdit_AccordionExtender').set_SelectedIndex(typeID-1);
			    switch (typeID)
			        {
			        case "1":
			            var oMonday = document.getElementById("<%= chkWeeklyMonday.ClientID %>");
			            var oTuesday = document.getElementById("<%= chkWeeklyTuesday.ClientID %>");
			            var oWednesday = document.getElementById("<%= chkWeeklyWednesday.ClientID %>");
			            var oThursday = document.getElementById("<%= chkWeeklyThursday.ClientID %>");
			            var oFriday = document.getElementById("<%= chkWeeklyFriday.ClientID %>");
			            var oSaturday = document.getElementById("<%= chkWeeklySaturday.ClientID %>");
			            var oSunday = document.getElementById("<%= chkWeeklySunday.ClientID %>");
			            
			            if (dateParameter.indexOf("0",0) > -1)
			                {
			                oSunday.checked = true;
			                }
			            if (dateParameter.indexOf("1",0) > -1)
			                {
			                oMonday.checked = true;
			                }
			            if (dateParameter.indexOf("2",0) > -1)
			                {
			                oTuesday.checked = true;
			                }
			            if (dateParameter.indexOf("3",0) > -1)
			                {
			                oWednesday.checked = true;
			                }
			            if (dateParameter.indexOf("4",0) > -1)
			                {
			                oThursday.checked = true;
			                }
			            if (dateParameter.indexOf("5",0) > -1)
			                {
			                oFriday.checked = true;
			                }
			            if (dateParameter.indexOf("6",0) > -1)
			                {
			                oSaturday.checked = true;
			                }
    			        break;
    			        
			        case "2":
			            var oDate = document.getElementById("<%= ddlMonthlyDate.ClientID %>");
			            oDate.value = dateParameter;
    			        break;
    			        
			        case "3":
			            var oWhichDay = document.getElementById("<%= ddlMonthlyDayWhich.ClientID %>");
			            var oDayOfWeek = document.getElementById("<%= ddlMonthlyDayOfWeek.ClientID %>");
			            
			            oWhichDay.value = dateParameter.substr(0,1);
			            oDayOfWeek.value = dateParameter.substr(1,1);
    			        break;
    			        
    			    case "4":
    			        var oMonth = document.getElementById("<%= ddlYearlyMonth.ClientID %>");
    			        var oDay = document.getElementById("<%= ddlYearlyDay.ClientID %>");
    			        
    			        oMonth.value = dateParameter.substr(0,dateParameter.indexOf("/",0));
    			        oDay.value = dateParameter.substr(dateParameter.indexOf("/",0)+1);
    			        break;
			        }
		    
			    oStartTimeHour.value = startTime.toString().substr(0,2);
			    oStartTimeMinute.value = startTime.toString().substr(3,2);
			    oEndTimeHour.value = endTime.toString().substr(0,2);
			    oEndTimeMinute.value = endTime.toString().substr(3,2);
			    oMessage.value = message;
			    oGenerateEvery.value = generateEvery;
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
				    row.className = row.oldClass;
		        }
		    //-->
        </script>
    </head>
    <body>
        <form id="frmMain" method="post" runat="server">
            <asp:ScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ScriptManager>
            <input id="hideStore" type="hidden" name="hideStore" runat="server" /> 
            <input id="hideScheduleID" type="hidden" name="hideScheduleID" runat="server" /> 
            <div align="center">
                <table width="100%">
                    <tr>
                        <td><h2>ManagerTrax Message Definition</h2></td>
                        <td><h4 style="text-align:right"><asp:label id="lblStoreDisplay" Runat="server" /></h4></td>
                    </tr>
                </table>
                <p>&nbsp;</p>
                <table class="topEntry">
                    <tr>
                        <td valign="top">
                            <h4>Edit Message:</h4>
                            <table class="editEntry">
                                <tr><td colspan="2"></td></tr>
                                <tr>
                                    <td class="col1" width="30%">Scheduler ID:</td>
                                    <td class="col2" width="70%">
                                        <asp:DropDownList ID="ddlSchedulerID" runat="server" Width="80px">
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
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="col1">
                                        <asp:Accordion ID="accEdit" runat="server" SelectedIndex="0" TransitionDuration="250" FramesPerSecond="40" FadeTransitions="true" HeaderCssClass="acc-header" ContentCssClass="acc-content" HeaderSelectedCssClass="acc-selected">
                                            <Panes>
                                                <asp:AccordionPane ID="apWeekly" runat="server" HeaderCssClass="acc-header" ContentCssClass="acc-content" HeaderSelectedCssClass="acc-selected">
                                                    <Header>Weekly</Header>
                                                    <Content>
                                                        <asp:CheckBox ID="chkWeeklyMonday" runat="server" Text="Monday" /><br />
                                                        <asp:CheckBox ID="chkWeeklyTuesday" runat="server" Text="Tuesday" /><br />
                                                        <asp:CheckBox ID="chkWeeklyWednesday" runat="server" Text="Wednesday" /><br />
                                                        <asp:CheckBox ID="chkWeeklyThursday" runat="server" Text="Thursday" /><br />
                                                        <asp:CheckBox ID="chkWeeklyFriday" runat="server" Text="Friday" /><br />
                                                        <asp:CheckBox ID="chkWeeklySaturday" runat="server" Text="Saturday" /><br />
                                                        <asp:CheckBox ID="chkWeeklySunday" runat="server" Text="Sunday" /><br />
                                                    </Content>
                                                </asp:AccordionPane>
                                                <asp:AccordionPane ID="apMonthlyOrdinal" runat="server" HeaderCssClass="acc-header" ContentCssClass="acc-content" HeaderSelectedCssClass="acc-selected">
                                                    <Header>Monthly by Date</Header>
                                                    <Content>
                                                        <asp:DropDownList ID="ddlMonthlyDate" runat="server" Width="80px">
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
                                                            <asp:ListItem Value="0">Last Day</asp:ListItem>
                                                        </asp:DropDownList>
                                                        of every month.
                                                    </Content>
                                                </asp:AccordionPane>
                                                <asp:AccordionPane ID="apMonthlyDayOfWeek" runat="server" HeaderCssClass="acc-header" ContentCssClass="acc-content" HeaderSelectedCssClass="acc-selected">
                                                    <Header>Monthly by Day of Week</Header>
                                                    <Content>
                                                        <asp:DropDownList ID="ddlMonthlyDayWhich" runat="server" Width="60px">
                                                            <asp:ListItem Value="1">1st</asp:ListItem>
                                                            <asp:ListItem Value="2">2nd</asp:ListItem>
                                                            <asp:ListItem Value="3">3rd</asp:ListItem>
                                                            <asp:ListItem Value="4">4th</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="ddlMonthlyDayOfWeek" runat="server" Width="100px">
                                                            <asp:ListItem Value="1">Monday</asp:ListItem>
                                                            <asp:ListItem Value="2">Tuesday</asp:ListItem>
                                                            <asp:ListItem Value="3">Wednesday</asp:ListItem>
                                                            <asp:ListItem Value="4">Thursday</asp:ListItem>
                                                            <asp:ListItem Value="5">Friday</asp:ListItem>
                                                            <asp:ListItem Value="6">Saturday</asp:ListItem>
                                                            <asp:ListItem Value="0">Sunday</asp:ListItem>
                                                        </asp:DropDownList>
                                                        of every month.
                                                    </Content>
                                                </asp:AccordionPane>
                                                <asp:AccordionPane ID="apYearly" runat="server" HeaderCssClass="acc-header" ContentCssClass="acc-content" HeaderSelectedCssClass="acc-selected">
                                                    <Header>Yearly</Header>
                                                    <Content>
                                                        <asp:DropDownList ID="ddlYearlyMonth" runat="server" Width="100px">
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
                                                        <asp:DropDownList ID="ddlYearlyDay" runat="server" Width="60px">
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
                                                        of every year.
                                                    </Content>
                                                </asp:AccordionPane>
                                            </Panes>
                                        </asp:Accordion>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="col1">Start Time:</td>
                                    <td class="col2">
                                        <asp:DropDownList ID="ddlStartTimeHour" runat="server" Width="40px">
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
                                        </asp:DropDownList>:
                                        <asp:DropDownList ID="ddlStartTimeMinute" runat="server" Width="40px">
                                            <asp:ListItem Value="00">00</asp:ListItem>
                                            <asp:ListItem Value="05">05</asp:ListItem>
                                            <asp:ListItem Value="10">10</asp:ListItem>
                                            <asp:ListItem Value="15">15</asp:ListItem>
                                            <asp:ListItem Value="20">20</asp:ListItem>
                                            <asp:ListItem Value="25">25</asp:ListItem>
                                            <asp:ListItem Value="30">30</asp:ListItem>
                                            <asp:ListItem Value="35">35</asp:ListItem>
                                            <asp:ListItem Value="40">40</asp:ListItem>
                                            <asp:ListItem Value="45">45</asp:ListItem>
                                            <asp:ListItem Value="50">50</asp:ListItem>
                                            <asp:ListItem Value="55">55</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="col1">End Time:</td>
                                    <td class="col2">
                                        <asp:DropDownList ID="ddlEndTimeHour" runat="server" Width="40px">
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
                                        </asp:DropDownList>:
                                        <asp:DropDownList ID="ddlEndTimeMinute" runat="server" Width="40px">
                                            <asp:ListItem Value="00">00</asp:ListItem>
                                            <asp:ListItem Value="05">05</asp:ListItem>
                                            <asp:ListItem Value="10">10</asp:ListItem>
                                            <asp:ListItem Value="15">15</asp:ListItem>
                                            <asp:ListItem Value="20">20</asp:ListItem>
                                            <asp:ListItem Value="25">25</asp:ListItem>
                                            <asp:ListItem Value="30">30</asp:ListItem>
                                            <asp:ListItem Value="35">35</asp:ListItem>
                                            <asp:ListItem Value="40">40</asp:ListItem>
                                            <asp:ListItem Value="45">45</asp:ListItem>
                                            <asp:ListItem Value="50">50</asp:ListItem>
                                            <asp:ListItem Value="55">55</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="col1">Message:</td>
                                    <td class="col2">
                                        <asp:TextBox ID="txtMessage" width="100%" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="col1">Generate Message Every&nbsp;
                                        <asp:DropDownList ID="ddlGenerateEvery" runat="server" Width="80px">
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
                                            <asp:ListItem Value="60">60</asp:ListItem>
                                            <asp:ListItem Value="61">61</asp:ListItem>
                                            <asp:ListItem Value="62">62</asp:ListItem>
                                            <asp:ListItem Value="63">63</asp:ListItem>
                                            <asp:ListItem Value="64">64</asp:ListItem>
                                            <asp:ListItem Value="65">65</asp:ListItem>
                                            <asp:ListItem Value="66">66</asp:ListItem>
                                            <asp:ListItem Value="67">67</asp:ListItem>
                                            <asp:ListItem Value="68">68</asp:ListItem>
                                            <asp:ListItem Value="69">69</asp:ListItem>
                                            <asp:ListItem Value="70">70</asp:ListItem>
                                            <asp:ListItem Value="71">71</asp:ListItem>
                                            <asp:ListItem Value="72">72</asp:ListItem>
                                            <asp:ListItem Value="73">73</asp:ListItem>
                                            <asp:ListItem Value="74">74</asp:ListItem>
                                            <asp:ListItem Value="75">75</asp:ListItem>
                                            <asp:ListItem Value="76">76</asp:ListItem>
                                            <asp:ListItem Value="77">77</asp:ListItem>
                                            <asp:ListItem Value="78">78</asp:ListItem>
                                            <asp:ListItem Value="79">79</asp:ListItem>
                                            <asp:ListItem Value="80">80</asp:ListItem>
                                            <asp:ListItem Value="81">81</asp:ListItem>
                                            <asp:ListItem Value="82">82</asp:ListItem>
                                            <asp:ListItem Value="83">83</asp:ListItem>
                                            <asp:ListItem Value="84">84</asp:ListItem>
                                            <asp:ListItem Value="85">85</asp:ListItem>
                                            <asp:ListItem Value="86">86</asp:ListItem>
                                            <asp:ListItem Value="87">87</asp:ListItem>
                                            <asp:ListItem Value="88">88</asp:ListItem>
                                            <asp:ListItem Value="89">89</asp:ListItem>
                                            <asp:ListItem Value="90">90</asp:ListItem>
                                            <asp:ListItem Value="91">91</asp:ListItem>
                                            <asp:ListItem Value="92">92</asp:ListItem>
                                            <asp:ListItem Value="93">93</asp:ListItem>
                                            <asp:ListItem Value="94">94</asp:ListItem>
                                            <asp:ListItem Value="95">95</asp:ListItem>
                                            <asp:ListItem Value="96">96</asp:ListItem>
                                            <asp:ListItem Value="97">97</asp:ListItem>
                                            <asp:ListItem Value="98">98</asp:ListItem>
                                            <asp:ListItem Value="99">99</asp:ListItem>
                                            <asp:ListItem Value="100">100</asp:ListItem>
                                            <asp:ListItem Value="101">101</asp:ListItem>
                                            <asp:ListItem Value="102">102</asp:ListItem>
                                            <asp:ListItem Value="103">103</asp:ListItem>
                                            <asp:ListItem Value="104">104</asp:ListItem>
                                            <asp:ListItem Value="105">105</asp:ListItem>
                                            <asp:ListItem Value="106">106</asp:ListItem>
                                            <asp:ListItem Value="107">107</asp:ListItem>
                                            <asp:ListItem Value="108">108</asp:ListItem>
                                            <asp:ListItem Value="109">109</asp:ListItem>
                                            <asp:ListItem Value="110">110</asp:ListItem>
                                            <asp:ListItem Value="111">111</asp:ListItem>
                                            <asp:ListItem Value="112">112</asp:ListItem>
                                            <asp:ListItem Value="113">113</asp:ListItem>
                                            <asp:ListItem Value="114">114</asp:ListItem>
                                            <asp:ListItem Value="115">115</asp:ListItem>
                                            <asp:ListItem Value="116">116</asp:ListItem>
                                            <asp:ListItem Value="117">117</asp:ListItem>
                                            <asp:ListItem Value="118">118</asp:ListItem>
                                            <asp:ListItem Value="119">119</asp:ListItem>
                                            <asp:ListItem Value="120">120</asp:ListItem>
                                            <asp:ListItem Value="121">121</asp:ListItem>
                                            <asp:ListItem Value="122">122</asp:ListItem>
                                            <asp:ListItem Value="123">123</asp:ListItem>
                                            <asp:ListItem Value="124">124</asp:ListItem>
                                            <asp:ListItem Value="125">125</asp:ListItem>
                                            <asp:ListItem Value="126">126</asp:ListItem>
                                            <asp:ListItem Value="127">127</asp:ListItem>
                                            <asp:ListItem Value="128">128</asp:ListItem>
                                            <asp:ListItem Value="129">129</asp:ListItem>
                                            <asp:ListItem Value="130">130</asp:ListItem>
                                            <asp:ListItem Value="131">131</asp:ListItem>
                                            <asp:ListItem Value="132">132</asp:ListItem>
                                            <asp:ListItem Value="133">133</asp:ListItem>
                                            <asp:ListItem Value="134">134</asp:ListItem>
                                            <asp:ListItem Value="135">135</asp:ListItem>
                                            <asp:ListItem Value="136">136</asp:ListItem>
                                            <asp:ListItem Value="137">137</asp:ListItem>
                                            <asp:ListItem Value="138">138</asp:ListItem>
                                            <asp:ListItem Value="139">139</asp:ListItem>
                                            <asp:ListItem Value="140">140</asp:ListItem>
                                            <asp:ListItem Value="141">141</asp:ListItem>
                                            <asp:ListItem Value="142">142</asp:ListItem>
                                            <asp:ListItem Value="143">143</asp:ListItem>
                                            <asp:ListItem Value="144">144</asp:ListItem>
                                            <asp:ListItem Value="145">145</asp:ListItem>
                                            <asp:ListItem Value="146">146</asp:ListItem>
                                            <asp:ListItem Value="147">147</asp:ListItem>
                                            <asp:ListItem Value="148">148</asp:ListItem>
                                            <asp:ListItem Value="149">149</asp:ListItem>
                                            <asp:ListItem Value="150">150</asp:ListItem>
                                            <asp:ListItem Value="151">151</asp:ListItem>
                                            <asp:ListItem Value="152">152</asp:ListItem>
                                            <asp:ListItem Value="153">153</asp:ListItem>
                                            <asp:ListItem Value="154">154</asp:ListItem>
                                            <asp:ListItem Value="155">155</asp:ListItem>
                                            <asp:ListItem Value="156">156</asp:ListItem>
                                            <asp:ListItem Value="157">157</asp:ListItem>
                                            <asp:ListItem Value="158">158</asp:ListItem>
                                            <asp:ListItem Value="159">159</asp:ListItem>
                                            <asp:ListItem Value="160">160</asp:ListItem>
                                            <asp:ListItem Value="161">161</asp:ListItem>
                                            <asp:ListItem Value="162">162</asp:ListItem>
                                            <asp:ListItem Value="163">163</asp:ListItem>
                                            <asp:ListItem Value="164">164</asp:ListItem>
                                            <asp:ListItem Value="165">165</asp:ListItem>
                                            <asp:ListItem Value="166">166</asp:ListItem>
                                            <asp:ListItem Value="167">167</asp:ListItem>
                                            <asp:ListItem Value="168">168</asp:ListItem>
                                            <asp:ListItem Value="169">169</asp:ListItem>
                                            <asp:ListItem Value="170">170</asp:ListItem>
                                            <asp:ListItem Value="171">171</asp:ListItem>
                                            <asp:ListItem Value="172">172</asp:ListItem>
                                            <asp:ListItem Value="173">173</asp:ListItem>
                                            <asp:ListItem Value="174">174</asp:ListItem>
                                            <asp:ListItem Value="175">175</asp:ListItem>
                                            <asp:ListItem Value="176">176</asp:ListItem>
                                            <asp:ListItem Value="177">177</asp:ListItem>
                                            <asp:ListItem Value="178">178</asp:ListItem>
                                            <asp:ListItem Value="179">179</asp:ListItem>
                                            <asp:ListItem Value="180">180</asp:ListItem>
                                            <asp:ListItem Value="181">181</asp:ListItem>
                                            <asp:ListItem Value="182">182</asp:ListItem>
                                            <asp:ListItem Value="183">183</asp:ListItem>
                                            <asp:ListItem Value="184">184</asp:ListItem>
                                            <asp:ListItem Value="185">185</asp:ListItem>
                                            <asp:ListItem Value="186">186</asp:ListItem>
                                            <asp:ListItem Value="187">187</asp:ListItem>
                                            <asp:ListItem Value="188">188</asp:ListItem>
                                            <asp:ListItem Value="189">189</asp:ListItem>
                                            <asp:ListItem Value="190">190</asp:ListItem>
                                            <asp:ListItem Value="191">191</asp:ListItem>
                                            <asp:ListItem Value="192">192</asp:ListItem>
                                            <asp:ListItem Value="193">193</asp:ListItem>
                                            <asp:ListItem Value="194">194</asp:ListItem>
                                            <asp:ListItem Value="195">195</asp:ListItem>
                                            <asp:ListItem Value="196">196</asp:ListItem>
                                            <asp:ListItem Value="197">197</asp:ListItem>
                                            <asp:ListItem Value="198">198</asp:ListItem>
                                            <asp:ListItem Value="199">199</asp:ListItem>
                                            <asp:ListItem Value="200">200</asp:ListItem>
                                            <asp:ListItem Value="201">201</asp:ListItem>
                                            <asp:ListItem Value="202">202</asp:ListItem>
                                            <asp:ListItem Value="203">203</asp:ListItem>
                                            <asp:ListItem Value="204">204</asp:ListItem>
                                            <asp:ListItem Value="205">205</asp:ListItem>
                                            <asp:ListItem Value="206">206</asp:ListItem>
                                            <asp:ListItem Value="207">207</asp:ListItem>
                                            <asp:ListItem Value="208">208</asp:ListItem>
                                            <asp:ListItem Value="209">209</asp:ListItem>
                                            <asp:ListItem Value="210">210</asp:ListItem>
                                            <asp:ListItem Value="211">211</asp:ListItem>
                                            <asp:ListItem Value="212">212</asp:ListItem>
                                            <asp:ListItem Value="213">213</asp:ListItem>
                                            <asp:ListItem Value="214">214</asp:ListItem>
                                            <asp:ListItem Value="215">215</asp:ListItem>
                                            <asp:ListItem Value="216">216</asp:ListItem>
                                            <asp:ListItem Value="217">217</asp:ListItem>
                                            <asp:ListItem Value="218">218</asp:ListItem>
                                            <asp:ListItem Value="219">219</asp:ListItem>
                                            <asp:ListItem Value="220">220</asp:ListItem>
                                            <asp:ListItem Value="221">221</asp:ListItem>
                                            <asp:ListItem Value="222">222</asp:ListItem>
                                            <asp:ListItem Value="223">223</asp:ListItem>
                                            <asp:ListItem Value="224">224</asp:ListItem>
                                            <asp:ListItem Value="225">225</asp:ListItem>
                                            <asp:ListItem Value="226">226</asp:ListItem>
                                            <asp:ListItem Value="227">227</asp:ListItem>
                                            <asp:ListItem Value="228">228</asp:ListItem>
                                            <asp:ListItem Value="229">229</asp:ListItem>
                                            <asp:ListItem Value="230">230</asp:ListItem>
                                            <asp:ListItem Value="231">231</asp:ListItem>
                                            <asp:ListItem Value="232">232</asp:ListItem>
                                            <asp:ListItem Value="233">233</asp:ListItem>
                                            <asp:ListItem Value="234">234</asp:ListItem>
                                            <asp:ListItem Value="235">235</asp:ListItem>
                                            <asp:ListItem Value="236">236</asp:ListItem>
                                            <asp:ListItem Value="237">237</asp:ListItem>
                                            <asp:ListItem Value="238">238</asp:ListItem>
                                            <asp:ListItem Value="239">239</asp:ListItem>
                                            <asp:ListItem Value="240">240</asp:ListItem>
                                            <asp:ListItem Value="241">241</asp:ListItem>
                                            <asp:ListItem Value="242">242</asp:ListItem>
                                            <asp:ListItem Value="243">243</asp:ListItem>
                                            <asp:ListItem Value="244">244</asp:ListItem>
                                            <asp:ListItem Value="245">245</asp:ListItem>
                                            <asp:ListItem Value="246">246</asp:ListItem>
                                            <asp:ListItem Value="247">247</asp:ListItem>
                                            <asp:ListItem Value="248">248</asp:ListItem>
                                            <asp:ListItem Value="249">249</asp:ListItem>
                                            <asp:ListItem Value="250">250</asp:ListItem>
                                            <asp:ListItem Value="251">251</asp:ListItem>
                                            <asp:ListItem Value="252">252</asp:ListItem>
                                            <asp:ListItem Value="253">253</asp:ListItem>
                                            <asp:ListItem Value="254">254</asp:ListItem>
                                            <asp:ListItem Value="255">255</asp:ListItem>
                                            <asp:ListItem Value="256">256</asp:ListItem>
                                            <asp:ListItem Value="257">257</asp:ListItem>
                                            <asp:ListItem Value="258">258</asp:ListItem>
                                            <asp:ListItem Value="259">259</asp:ListItem>
                                            <asp:ListItem Value="260">260</asp:ListItem>
                                            <asp:ListItem Value="261">261</asp:ListItem>
                                            <asp:ListItem Value="262">262</asp:ListItem>
                                            <asp:ListItem Value="263">263</asp:ListItem>
                                            <asp:ListItem Value="264">264</asp:ListItem>
                                            <asp:ListItem Value="265">265</asp:ListItem>
                                            <asp:ListItem Value="266">266</asp:ListItem>
                                            <asp:ListItem Value="267">267</asp:ListItem>
                                            <asp:ListItem Value="268">268</asp:ListItem>
                                            <asp:ListItem Value="269">269</asp:ListItem>
                                            <asp:ListItem Value="270">270</asp:ListItem>
                                            <asp:ListItem Value="271">271</asp:ListItem>
                                            <asp:ListItem Value="272">272</asp:ListItem>
                                            <asp:ListItem Value="273">273</asp:ListItem>
                                            <asp:ListItem Value="274">274</asp:ListItem>
                                            <asp:ListItem Value="275">275</asp:ListItem>
                                            <asp:ListItem Value="276">276</asp:ListItem>
                                            <asp:ListItem Value="277">277</asp:ListItem>
                                            <asp:ListItem Value="278">278</asp:ListItem>
                                            <asp:ListItem Value="279">279</asp:ListItem>
                                            <asp:ListItem Value="280">280</asp:ListItem>
                                            <asp:ListItem Value="281">281</asp:ListItem>
                                            <asp:ListItem Value="282">282</asp:ListItem>
                                            <asp:ListItem Value="283">283</asp:ListItem>
                                            <asp:ListItem Value="284">284</asp:ListItem>
                                            <asp:ListItem Value="285">285</asp:ListItem>
                                            <asp:ListItem Value="286">286</asp:ListItem>
                                            <asp:ListItem Value="287">287</asp:ListItem>
                                            <asp:ListItem Value="288">288</asp:ListItem>
                                            <asp:ListItem Value="289">289</asp:ListItem>
                                            <asp:ListItem Value="290">290</asp:ListItem>
                                            <asp:ListItem Value="291">291</asp:ListItem>
                                            <asp:ListItem Value="292">292</asp:ListItem>
                                            <asp:ListItem Value="293">293</asp:ListItem>
                                            <asp:ListItem Value="294">294</asp:ListItem>
                                            <asp:ListItem Value="295">295</asp:ListItem>
                                            <asp:ListItem Value="296">296</asp:ListItem>
                                            <asp:ListItem Value="297">297</asp:ListItem>
                                            <asp:ListItem Value="298">298</asp:ListItem>
                                            <asp:ListItem Value="299">299</asp:ListItem>
                                            <asp:ListItem Value="300">300</asp:ListItem>
                                            <asp:ListItem Value="301">301</asp:ListItem>
                                            <asp:ListItem Value="302">302</asp:ListItem>
                                            <asp:ListItem Value="303">303</asp:ListItem>
                                            <asp:ListItem Value="304">304</asp:ListItem>
                                            <asp:ListItem Value="305">305</asp:ListItem>
                                            <asp:ListItem Value="306">306</asp:ListItem>
                                            <asp:ListItem Value="307">307</asp:ListItem>
                                            <asp:ListItem Value="308">308</asp:ListItem>
                                            <asp:ListItem Value="309">309</asp:ListItem>
                                            <asp:ListItem Value="310">310</asp:ListItem>
                                            <asp:ListItem Value="311">311</asp:ListItem>
                                            <asp:ListItem Value="312">312</asp:ListItem>
                                            <asp:ListItem Value="313">313</asp:ListItem>
                                            <asp:ListItem Value="314">314</asp:ListItem>
                                            <asp:ListItem Value="315">315</asp:ListItem>
                                            <asp:ListItem Value="316">316</asp:ListItem>
                                            <asp:ListItem Value="317">317</asp:ListItem>
                                            <asp:ListItem Value="318">318</asp:ListItem>
                                            <asp:ListItem Value="319">319</asp:ListItem>
                                            <asp:ListItem Value="320">320</asp:ListItem>
                                            <asp:ListItem Value="321">321</asp:ListItem>
                                            <asp:ListItem Value="322">322</asp:ListItem>
                                            <asp:ListItem Value="323">323</asp:ListItem>
                                            <asp:ListItem Value="324">324</asp:ListItem>
                                            <asp:ListItem Value="325">325</asp:ListItem>
                                            <asp:ListItem Value="326">326</asp:ListItem>
                                            <asp:ListItem Value="327">327</asp:ListItem>
                                            <asp:ListItem Value="328">328</asp:ListItem>
                                            <asp:ListItem Value="329">329</asp:ListItem>
                                            <asp:ListItem Value="330">330</asp:ListItem>
                                            <asp:ListItem Value="331">331</asp:ListItem>
                                            <asp:ListItem Value="332">332</asp:ListItem>
                                            <asp:ListItem Value="333">333</asp:ListItem>
                                            <asp:ListItem Value="334">334</asp:ListItem>
                                            <asp:ListItem Value="335">335</asp:ListItem>
                                            <asp:ListItem Value="336">336</asp:ListItem>
                                            <asp:ListItem Value="337">337</asp:ListItem>
                                            <asp:ListItem Value="338">338</asp:ListItem>
                                            <asp:ListItem Value="339">339</asp:ListItem>
                                            <asp:ListItem Value="340">340</asp:ListItem>
                                            <asp:ListItem Value="341">341</asp:ListItem>
                                            <asp:ListItem Value="342">342</asp:ListItem>
                                            <asp:ListItem Value="343">343</asp:ListItem>
                                            <asp:ListItem Value="344">344</asp:ListItem>
                                            <asp:ListItem Value="345">345</asp:ListItem>
                                            <asp:ListItem Value="346">346</asp:ListItem>
                                            <asp:ListItem Value="347">347</asp:ListItem>
                                            <asp:ListItem Value="348">348</asp:ListItem>
                                            <asp:ListItem Value="349">349</asp:ListItem>
                                            <asp:ListItem Value="350">350</asp:ListItem>
                                            <asp:ListItem Value="351">351</asp:ListItem>
                                            <asp:ListItem Value="352">352</asp:ListItem>
                                            <asp:ListItem Value="353">353</asp:ListItem>
                                            <asp:ListItem Value="354">354</asp:ListItem>
                                            <asp:ListItem Value="355">355</asp:ListItem>
                                            <asp:ListItem Value="356">356</asp:ListItem>
                                            <asp:ListItem Value="357">357</asp:ListItem>
                                            <asp:ListItem Value="358">358</asp:ListItem>
                                            <asp:ListItem Value="359">359</asp:ListItem>
                                            <asp:ListItem Value="360">360</asp:ListItem>
                                        </asp:DropDownList>&nbsp;minutes.
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
                <asp:datagrid id="dgSchedules" runat="server" PageSize="20" AutoGenerateColumns="False" AllowSorting="True" OnPageIndexChanged="dgProducts_NewPage" AllowPaging="True" OnSortCommand="dgProducts_Sort" CellPadding="3">
                    <Columns>
                        <asp:BoundColumn DataField="ScheduleID" SortExpression="ScheduleID" HeaderText="Schedule ID">
                            <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                            <ItemStyle CssClass="scheduleID" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="SchedulerID" SortExpression="SchedulerID" HeaderText="Scheduler ID">
                            <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                            <ItemStyle CssClass="schedulerID" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="TypeID" Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn DataField="DateParameter" Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Scheduled" SortExpression="TypeID,DateParameter" HeaderText="Scheduled">
                            <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                            <ItemStyle CssClass="scheduled" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="StartTime" SortExpression="StartTime" HeaderText="Start Time">
                            <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                            <ItemStyle CssClass="startTime" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="EndTime" SortExpression="EndTime" HeaderText="End Time">
                            <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                            <ItemStyle CssClass="endTime" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Message" HeaderText="Message">
                            <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                            <ItemStyle CssClass="message" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="NumberOfMessages" Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn DataField="GenerateEvery" Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn DataField="GenerateEveryText" HeaderText="Generate Every">
                            <HeaderStyle CssClass="gridHeader" />
                            <ItemStyle CssClass="generateEvery" />
                        </asp:BoundColumn>
                    </Columns>
                    <AlternatingItemStyle CssClass="odd" />
                    <PagerStyle CssClass="bottomNav" />
                </asp:datagrid>
            </div>
        </form>
    </body>
</html>
