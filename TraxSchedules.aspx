<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TraxSchedules.aspx.vb" Inherits="TraxSchedules" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
    <head>
        <title>SmartTrax Schedule Definition</title>
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
            .scheduled, .product { text-align: left; }
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
		    function loadItems(row, scheduleID, schedulerID, typeID, dateParameter, startTime, endTime, productID, generateEvery)
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
			    var oProducts = document.getElementById("ddlProducts");
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
			    oProducts.value = productID;
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
                        <td><h2>SmartTrax Schedule Definition</h2></td>
                        <td><h4 style="text-align:right"><asp:label id="lblStoreDisplay" Runat="server" /></h4></td>
                    </tr>
                </table>
                <p>&nbsp;</p>
                <table class="topEntry">
                    <tr>
                        <td valign="top">
                            <h4>Edit Schedule:</h4>
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
                                    <td class="col1">Product:</td>
                                    <td class="col2"><asp:DropDownList ID="ddlProducts" runat="server" Width="100%"></asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="col1">Generate Product Every&nbsp;
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
                                        </asp:DropDownList>&nbsp;minute(s).
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
                            <HeaderStyle CssClass="gridHeader" />
                            <ItemStyle CssClass="scheduleID" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="SchedulerID" SortExpression="SchedulerID" HeaderText="Scheduler ID">
                            <HeaderStyle CssClass="gridHeader" />
                            <ItemStyle CssClass="schedulerID" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="TypeID" Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn DataField="DateParameter" Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Scheduled" SortExpression="TypeID,DateParameter" HeaderText="Scheduled">
                            <HeaderStyle CssClass="gridHeader" />
                            <ItemStyle CssClass="scheduled" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="StartTime" SortExpression="StartTime" HeaderText="Start Time">
                            <HeaderStyle CssClass="gridHeader" />
                            <ItemStyle CssClass="startTime" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="EndTime" SortExpression="EndTime" HeaderText="End Time">
                            <HeaderStyle CssClass="gridHeader" />
                            <ItemStyle CssClass="endTime" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="ProductID" Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Product" SortExpression="ProductID" HeaderText="Product">
                            <HeaderStyle CssClass="gridHeader" />
                            <ItemStyle CssClass="product" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="NumberOfProducts" Visible="false"></asp:BoundColumn>
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
