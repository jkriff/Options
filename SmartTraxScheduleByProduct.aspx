<%@ Page Language="VB" EnableEventValidation="false" AutoEventWireup="false" CodeFile="SmartTraxScheduleByProduct.aspx.vb" Inherits="SmartTraxScheduleByProduct" %>
<%@ Register Assembly="AjaxControlToolkit" TagPrefix="act" Namespace="AjaxControlToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>SmartTrax Schedules</title>
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
            .prodID a { text-decoration: none; COLOR: Black; }
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
            function ok(sender, e) {
                $find('mpeEditSchedule').hide();
                __doPostBack('btnEditScheduleOK', e);
            }

            function cancel(sender, e) {
                $find('mpeEditSchedule').hide();
            }

            function ok_percent(sender, e) {
                $find('mpePercentage').hide();
                __doPostBack('btnPercentageOK', e);
            }

            function cancel_percent(sender, e) {
                $find('mpePercentage').hide();
            }
        </script>
    </head>
    <body>
        <form id="form1" runat="server">
            <asp:ScriptManager ID="tsmMain" runat="server" />
            <input id="hideStore" type="hidden" name="hideStore" runat="server" /> 
            <div>
                <h2>SmartTrax Schedules</h2>
                <h4><asp:Label ID="lblStoreDisplay" runat="server">Store 8888</asp:Label></h4>
                <asp:Label ID="lblErrors" runat="server" ForeColor="Red"></asp:Label>
                <asp:DropDownList ID="ddlProducts" runat="server" Width="400px" AutoPostBack="true" />
                <asp:DropDownList ID="ddlWeeks" runat="server" Width="400px" AutoPostBack="true" />
                <br /><br />
                <asp:datagrid id="dgSchedules" runat="server" AutoGenerateColumns="False" CellPadding="3" Width="100%">
                    <Columns>
                        <asp:BoundColumn DataField="Date" HeaderText="Date">
                            <HeaderStyle CssClass="gridHeader2" />
                            <ItemStyle CssClass="prodID" />
                        </asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="00">
                            <ItemTemplate>
                                <a href="javascript:__doPostBack('dgSchedules_ItemClick', '<%# DataBinder.Eval(Container.DataItem, "Date") %>|00')"><%# DataBinder.Eval(Container.DataItem, "Hour00")%></a>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridHeader2" />
                            <ItemStyle CssClass="prodID" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="01">
                            <ItemTemplate>
                                <a href="javascript:__doPostBack('dgSchedules_ItemClick', '<%# DataBinder.Eval(Container.DataItem, "Date") %>|01')"><%# DataBinder.Eval(Container.DataItem, "Hour01")%></a>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridHeader2" />
                            <ItemStyle CssClass="prodID" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="02">
                            <ItemTemplate>
                                <a href="javascript:__doPostBack('dgSchedules_ItemClick', '<%# DataBinder.Eval(Container.DataItem, "Date") %>|02')"><%# DataBinder.Eval(Container.DataItem, "Hour02")%></a>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridHeader2" />
                            <ItemStyle CssClass="prodID" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="03">
                            <ItemTemplate>
                                <a href="javascript:__doPostBack('dgSchedules_ItemClick', '<%# DataBinder.Eval(Container.DataItem, "Date") %>|03')"><%# DataBinder.Eval(Container.DataItem, "Hour03")%></a>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridHeader2" />
                            <ItemStyle CssClass="prodID" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="04">
                            <ItemTemplate>
                                <a href="javascript:__doPostBack('dgSchedules_ItemClick', '<%# DataBinder.Eval(Container.DataItem, "Date") %>|04')"><%# DataBinder.Eval(Container.DataItem, "Hour04")%></a>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridHeader2" />
                            <ItemStyle CssClass="prodID" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="05">
                            <ItemTemplate>
                                <a href="javascript:__doPostBack('dgSchedules_ItemClick', '<%# DataBinder.Eval(Container.DataItem, "Date") %>|05')"><%# DataBinder.Eval(Container.DataItem, "Hour05")%></a>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridHeader2" />
                            <ItemStyle CssClass="prodID" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="06">
                            <ItemTemplate>
                                <a href="javascript:__doPostBack('dgSchedules_ItemClick', '<%# DataBinder.Eval(Container.DataItem, "Date") %>|06')"><%# DataBinder.Eval(Container.DataItem, "Hour06")%></a>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridHeader2" />
                            <ItemStyle CssClass="prodID" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="07">
                            <ItemTemplate>
                                <a href="javascript:__doPostBack('dgSchedules_ItemClick', '<%# DataBinder.Eval(Container.DataItem, "Date") %>|07')"><%# DataBinder.Eval(Container.DataItem, "Hour07")%></a>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridHeader2" />
                            <ItemStyle CssClass="prodID" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="08">
                            <ItemTemplate>
                                <a href="javascript:__doPostBack('dgSchedules_ItemClick', '<%# DataBinder.Eval(Container.DataItem, "Date") %>|08')"><%# DataBinder.Eval(Container.DataItem, "Hour08")%></a>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridHeader2" />
                            <ItemStyle CssClass="prodID" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="09">
                            <ItemTemplate>
                                <a href="javascript:__doPostBack('dgSchedules_ItemClick', '<%# DataBinder.Eval(Container.DataItem, "Date") %>|09')"><%# DataBinder.Eval(Container.DataItem, "Hour09")%></a>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridHeader2" />
                            <ItemStyle CssClass="prodID" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="10">
                            <ItemTemplate>
                                <a href="javascript:__doPostBack('dgSchedules_ItemClick', '<%# DataBinder.Eval(Container.DataItem, "Date") %>|10')"><%# DataBinder.Eval(Container.DataItem, "Hour10")%></a>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridHeader2" />
                            <ItemStyle CssClass="prodID" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="11">
                            <ItemTemplate>
                                <a href="javascript:__doPostBack('dgSchedules_ItemClick', '<%# DataBinder.Eval(Container.DataItem, "Date") %>|11')"><%# DataBinder.Eval(Container.DataItem, "Hour11")%></a>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridHeader2" />
                            <ItemStyle CssClass="prodID" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="12">
                            <ItemTemplate>
                                <a href="javascript:__doPostBack('dgSchedules_ItemClick', '<%# DataBinder.Eval(Container.DataItem, "Date") %>|12')"><%# DataBinder.Eval(Container.DataItem, "Hour12")%></a>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridHeader2" />
                            <ItemStyle CssClass="prodID" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="13">
                            <ItemTemplate>
                                <a href="javascript:__doPostBack('dgSchedules_ItemClick', '<%# DataBinder.Eval(Container.DataItem, "Date") %>|13')"><%# DataBinder.Eval(Container.DataItem, "Hour13")%></a>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridHeader2" />
                            <ItemStyle CssClass="prodID" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="14">
                            <ItemTemplate>
                                <a href="javascript:__doPostBack('dgSchedules_ItemClick', '<%# DataBinder.Eval(Container.DataItem, "Date") %>|14')"><%# DataBinder.Eval(Container.DataItem, "Hour14")%></a>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridHeader2" />
                            <ItemStyle CssClass="prodID" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="15">
                            <ItemTemplate>
                                <a href="javascript:__doPostBack('dgSchedules_ItemClick', '<%# DataBinder.Eval(Container.DataItem, "Date") %>|15')"><%# DataBinder.Eval(Container.DataItem, "Hour15")%></a>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridHeader2" />
                            <ItemStyle CssClass="prodID" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="16">
                            <ItemTemplate>
                                <a href="javascript:__doPostBack('dgSchedules_ItemClick', '<%# DataBinder.Eval(Container.DataItem, "Date") %>|16')"><%# DataBinder.Eval(Container.DataItem, "Hour16")%></a>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridHeader2" />
                            <ItemStyle CssClass="prodID" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="17">
                            <ItemTemplate>
                                <a href="javascript:__doPostBack('dgSchedules_ItemClick', '<%# DataBinder.Eval(Container.DataItem, "Date") %>|17')"><%# DataBinder.Eval(Container.DataItem, "Hour17")%></a>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridHeader2" />
                            <ItemStyle CssClass="prodID" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="18">
                            <ItemTemplate>
                                <a href="javascript:__doPostBack('dgSchedules_ItemClick', '<%# DataBinder.Eval(Container.DataItem, "Date") %>|18')"><%# DataBinder.Eval(Container.DataItem, "Hour18")%></a>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridHeader2" />
                            <ItemStyle CssClass="prodID" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="19">
                            <ItemTemplate>
                                <a href="javascript:__doPostBack('dgSchedules_ItemClick', '<%# DataBinder.Eval(Container.DataItem, "Date") %>|19')"><%# DataBinder.Eval(Container.DataItem, "Hour19")%></a>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridHeader2" />
                            <ItemStyle CssClass="prodID" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="20">
                            <ItemTemplate>
                                <a href="javascript:__doPostBack('dgSchedules_ItemClick', '<%# DataBinder.Eval(Container.DataItem, "Date") %>|20')"><%# DataBinder.Eval(Container.DataItem, "Hour20")%></a>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridHeader2" />
                            <ItemStyle CssClass="prodID" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="21">
                            <ItemTemplate>
                                <a href="javascript:__doPostBack('dgSchedules_ItemClick', '<%# DataBinder.Eval(Container.DataItem, "Date") %>|21')"><%# DataBinder.Eval(Container.DataItem, "Hour21")%></a>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridHeader2" />
                            <ItemStyle CssClass="prodID" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="22">
                            <ItemTemplate>
                                <a href="javascript:__doPostBack('dgSchedules_ItemClick', '<%# DataBinder.Eval(Container.DataItem, "Date") %>|22')"><%# DataBinder.Eval(Container.DataItem, "Hour22")%></a>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridHeader2" />
                            <ItemStyle CssClass="prodID" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="23">
                            <ItemTemplate>
                                <a href="javascript:__doPostBack('dgSchedules_ItemClick', '<%# DataBinder.Eval(Container.DataItem, "Date") %>|23')"><%# DataBinder.Eval(Container.DataItem, "Hour23")%></a>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridHeader2" />
                            <ItemStyle CssClass="prodID" />
                        </asp:TemplateColumn>
                        <asp:BoundColumn DataField="Percentage" HeaderText="%">
                            <HeaderStyle CssClass="gridHeader2" />
                            <ItemStyle CssClass="prodID" />
                        </asp:BoundColumn>
                        <asp:ButtonColumn ButtonType="PushButton" CommandName="DialUp" Text="+" ItemStyle-Width="1%">
                            <HeaderStyle CssClass="gridHeader" />
                            <ItemStyle CssClass="prodDT" />
                        </asp:ButtonColumn>
                        <asp:ButtonColumn ButtonType="PushButton" CommandName="DialDown" Text="-" ItemStyle-Width="1%">
                            <HeaderStyle CssClass="gridHeader" />
                            <ItemStyle CssClass="prodDT" />
                        </asp:ButtonColumn>
                        <asp:ButtonColumn ButtonType="PushButton" CommandName="Suggest" Text="Suggest" ItemStyle-Width="5%">
                            <HeaderStyle CssClass="gridHeader" />
                            <ItemStyle CssClass="prodDT" />
                        </asp:ButtonColumn>
                        <asp:ButtonColumn ButtonType="PushButton" CommandName="Clear" Text="Clear" ItemStyle-Width="5%">
                            <HeaderStyle CssClass="gridHeader" />
                            <ItemStyle CssClass="prodDT" />
                        </asp:ButtonColumn>
                    </Columns>
                </asp:datagrid>
                <asp:Button runat="server" ID="hiddenTargetControlForModalPopup" style="display:none"/>
            </div>

            <asp:Panel ID="pnlEditSchedule" runat="server" CssClass="modalPopup" style="display:none">
                <asp:Panel ID="pnlTitleBar" runat="server" CssClass="modalPopupTitleBar">
                    <asp:Literal ID="litModalTitle" runat="server" Text="Edit Schedule"></asp:Literal>
                </asp:Panel>
                <div style="margin:10px">
                    <asp:UpdatePanel runat="server" ID="upnEditSchedule" RenderMode="Inline" UpdateMode="Conditional"> 
                        <ContentTemplate>
                            <input type="hidden" id="hideEditDate" runat="server" />
                            <input type="hidden" id="hideEditHour" runat="server" />
                            <center>
                                <table>
                                    <tr>
                                        <td><b>Scheduled Products:</b></td>
                                        <td>
                                            <asp:DropDownList runat="server" id="ddlScheduledProducts" Width="100px">
                                                <asp:ListItem Value="0">0</asp:ListItem>
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
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </center>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <br />
                    <center>
                        <asp:Button ID="btnEditScheduleOK" runat="server" Text="OK" width="100px" />
                        <asp:Button ID="btnEditScheduleCancel" runat="server" Text="Cancel" width="100px"  />
                    </center>
                </div>
            </asp:Panel>

            <act:ModalPopupExtender ID="mpeEditSchedule" runat="server" TargetControlID="hiddenTargetControlForModalPopup" PopupControlID="pnlEditSchedule" BackgroundCssClass="modalBackground" OkControlID="btnEditScheduleOK" OnOkScript="ok()" CancelControlID="btnEditScheduleCancel" OnCancelScript="cancel()" DropShadow="true" PopupDragHandleControlID="pnlTitleBar" /> 

            <asp:Panel ID="pnlPercentage" runat="server" CssClass="modalPopup" style="display:none">
                <asp:Panel ID="pnlPercentageTitleBar" runat="server" CssClass="modalPopupTitleBar">
                    <asp:Literal ID="litPercentage" runat="server" Text="Edit Schedule">Select Percentage</asp:Literal>
                </asp:Panel>
                <div style="margin:10px">
                    <asp:UpdatePanel runat="server" ID="upnPercentage" RenderMode="Inline" UpdateMode="Conditional"> 
                        <ContentTemplate>
                            <input type="hidden" id="hidePercentageDate" runat="server" />
                            <input type="hidden" id="hidePercentageWeekday" runat="server" />
                            <center>
                                <table>
                                    <tr>
                                        <td><b>Percentage:</b></td>
                                        <td>
                                            <asp:DropDownList runat="server" id="ddlPercentage" Width="100px">
                                                <asp:ListItem Value="5">5</asp:ListItem>
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
                                                <asp:ListItem Value="60">60</asp:ListItem>
                                                <asp:ListItem Value="65">65</asp:ListItem>
                                                <asp:ListItem Value="70">70</asp:ListItem>
                                                <asp:ListItem Value="75">75</asp:ListItem>
                                                <asp:ListItem Value="80">80</asp:ListItem>
                                                <asp:ListItem Value="85">85</asp:ListItem>
                                                <asp:ListItem Value="90">90</asp:ListItem>
                                                <asp:ListItem Value="95">95</asp:ListItem>
                                                <asp:ListItem Value="100">100</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </center>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <br />
                    <center>
                        <asp:Button ID="btnPercentageOK" runat="server" Text="OK" width="100px" />
                        <asp:Button ID="btnPercentageCancel" runat="server" Text="Cancel" width="100px"  />
                    </center>
                </div>
            </asp:Panel>

            <act:ModalPopupExtender ID="mpePercentage" runat="server" TargetControlID="hiddenTargetControlForModalPopup" PopupControlID="pnlPercentage" BackgroundCssClass="modalBackground" OkControlID="btnPercentageOK" OnOkScript="ok_percent()" CancelControlID="btnPercentageCancel" OnCancelScript="cancel_percent()" DropShadow="true" PopupDragHandleControlID="pnlPercentageTitleBar" /> 
        </form>
    </body>
</html>
