Imports System.Data
Imports System.Net
Imports System.Web.UI.WebControls
Imports System.Text
Imports System.Data.SqlClient
Imports System.Web.Configuration

Namespace Register


    Partial Class RegisterOptions
        Inherits System.Web.UI.Page

#Region " Page Controls "





        Protected tblAutoTime As Table



#End Region

#Region " Page Variables "

        Protected _dsIn As DataSet
        Protected _dsConfig As DataSet
        Protected _sPath As String = ""
        Protected _sEvent As String = ""
        Protected _aryFloatRows As New ArrayList
        Protected _aryDisplay As New ArrayList
        Protected _aryPrint As New ArrayList
        Protected _bInitialLoad As Boolean = False
        Protected _bShowDisplayHeader As Boolean = False
        Protected _bShowPrintHeader As Boolean = False
        Protected _bShowPayType As Boolean = False
        Protected _bShowLogoHeader As Boolean = False
        Protected _bShowLogoFooter As Boolean = False
        Protected _bShowCSHeader As Boolean = False
        Protected _bShowCSFooter As Boolean = False
        Protected _bPrinterRowAdded As Boolean = False
        Protected _bShowTicketType As Boolean = False
        Protected _sRegister As String = ""
        Protected _sCurrentTab As String = ""
        Protected _aryPassword As New ArrayList

#End Region

#Region " Page Contants "

        Protected Const DEFAULT_TAB As String = "System"

        Protected Const DEFAULT_ROW_HEIGHT As Int16 = 26

        Protected Const MENU_BUTTON_PREFIX As String = "cmd_menu"
        Protected Const FLOATING_BUTTON_PREFIX As String = "cmd_float"

        Protected Const COMBO_TEXT_VALUE_DELIM As String = "^^^"

        Protected Const FLOATING_TABLE_PREFIX As String = "tbl_float"
        Protected Const FLOATING_ROW_PREFIX As String = "tr_float"
        Protected Const FLOATING_TEXTBOX_PREFIX As String = "tbl_float_txt"
        Protected Const FLOATING_DELETE_PREFIX As String = "tbl_float_delete"
        Protected Const FLOATING_ADDNEW_PREFIX As String = "tbl_float_addnew"
        Protected Const FLOATING_EXIT_PREFIX As String = "tbl_float_exit"
        Protected Const FLOATING_ROW_WHACK As String = "^^whack^^"
        Protected Const FLOATING_DISPLAY_DELIM As String = "}{"
        Protected Const FLOATING_NUM_PLACEHOLDER As String = ":!:"
        Protected Const FLOATING_ROW_OUTPUT As String = "output"

        Protected Const HEADER_FLOAT_DISPLAY_POSTFIX As String = "HeaderDisplay"
        Protected Const HEADER_FLOAT_PRINT_POSTFIX As String = "HeaderPrint"
        Protected Const HEADER_FLOAT_BUTTON_PREFIX As String = "cmd_header_float"
        Protected Const HEADER_FLOAT_COMBO_PREFIX_LEFT As String = "cbo_header_float_left"
        Protected Const HEADER_FLOAT_COMBO_PREFIX_RIGHT As String = "cbo_header_float_right"
        Protected Const HEADER_FLOAT_TTEXCLUDE As String = "header_float_ttexclude"
        Protected Const HEADER_FLOAT_TTEXCLUDE_COL As Integer = 2
        Protected Const HEADER_FLOAT_JUSTIFICATION As String = "header_float_justification"
        Protected Const HEADER_FLOAT_JUSTIFICATION_COL As Integer = 3
        Protected Const HEADER_FLOAT_LARGE As String = "header_float_large"
        Protected Const HEADER_FLOAT_LARGE_COL As Integer = 4
        Protected Const HEADER_FLOAT_BOLD As String = "header_float_bold"
        Protected Const HEADER_FLOAT_BOLD_COL As Integer = 5
        Protected Const HEADER_FLOAT_WIDE As String = "header_float_wide"
        Protected Const HEADER_FLOAT_WIDE_COL As Integer = 6
        Protected Const HEADER_FLOAT_HIGH As String = "header_float_high"
        Protected Const HEADER_FLOAT_HIGH_COL As Integer = 7
        Protected Const HEADER_FLOAT_UNDERLINE As String = "header_float_underline"
        Protected Const HEADER_FLOAT_UNDERLINE_COL As Integer = 8
        Protected Const HEADER_FLOAT_COLOR As String = "header_float_color"
        Protected Const HEADER_FLOAT_COLOR_COL As Integer = 9

        Protected Const HEADER_FLOAT_DELETE_PREFIX As String = "cmd_header_float_delete"
        Protected Const HEADER_FLOAT_ADDNEW_PREFIX As String = "cmd_header_float_addnew"
        Protected Const HEADER_FLOAT_EXIT_PREFIX As String = "cmd_header_float_exit"
        Protected Const HEADER_FLOAT_MOVEUP_PREFIX As String = "cmd_header_float_moveup"
        Protected Const HEADER_FLOAT_MOVEDOWN_PREFIX As String = "cmd_header_float_movedown"

        Protected Const PAYTYPE_FLOAT_BUTTON_PREFIX As String = "cmd_paytype_float"
        Protected Const PAYTYPE_FLOAT_DELETE_PREFIX As String = "cmd_paytype_float_delete"
        Protected Const PAYTYPE_FLOAT_ADDNEW_PREFIX As String = "cmd_paytype_float_addnew"
        Protected Const PAYTYPE_FLOAT_EXIT_PREFIX As String = "cmd_paytype_float_exit"
        Protected Const PAYTYPE_FLOAT_MOVEUP_PREFIX As String = "cmd_paytype_float_moveup"
        Protected Const PAYTYPE_FLOAT_MOVEDOWN_PREFIX As String = "cmd_paytype_float_movedown"
        Protected Const PAYTYPE_FLOAT_PAYTYPE As String = "paytype_float_paytype"
        Protected Const PAYTYPE_FLOAT_PAYTYPE_COL As Integer = 0
        Protected Const PAYTYPE_FLOAT_PAYTYPE_TICKET As String = "paytype_float_paytype_ticket"
        Protected Const PAYTYPE_FLOAT_PAYTYPE_TICKET_COL As Integer = 1
        Protected Const PAYTYPE_FLOAT_TYPE As String = "paytype_float_type"
        Protected Const PAYTYPE_FLOAT_TYPE_COL As Integer = 2
        Protected Const PAYTYPE_FLOAT_CHANGE As String = "paytype_float_change"
        Protected Const PAYTYPE_FLOAT_CHANGE_COL As Integer = 3
        Protected Const PAYTYPE_FLOAT_REFUND As String = "paytype_float_refund"
        Protected Const PAYTYPE_FLOAT_REFUND_COL As Integer = 4
        Protected Const PAYTYPE_FLOAT_REQ_NUMBER As String = "paytype_float_req_number"
        Protected Const PAYTYPE_FLOAT_REQ_NUMBER_COL As Integer = 5
        Protected Const PAYTYPE_FLOAT_REQ_MGR_AUTH As String = "paytype_float_req_mgr_auth"
        Protected Const PAYTYPE_FLOAT_REQ_MGR_AUTH_COL As Integer = 6
        Protected Const PAYTYPE_FLOAT_DRAWER As String = "paytype_float_drawer"
        Protected Const PAYTYPE_FLOAT_DRAWER_COL As Integer = 7
        Protected Const PAYTYPE_FLOAT_RECEIPT As String = "paytype_float_receipt"
        Protected Const PAYTYPE_FLOAT_RECEIPT_COL As Integer = 8
        Protected Const PAYTYPE_FLOAT_REQ_CUST As String = "paytype_float_req_cust"
        Protected Const PAYTYPE_FLOAT_REQ_CUST_COL As Integer = 9
        Protected Const PAYTYPE_FLOAT_NO_SURCHARGE As String = "paytype_float_no_surcharge"
        Protected Const PAYTYPE_FLOAT_NO_SURCHARGE_COL As Integer = 10

        Protected Const TICKETTYPE_FLOAT_BUTTON_PREFIX As String = "cmd_tickettype_float"
        Protected Const TICKETTYPE_FLOAT_DELETE_PREFIX As String = "cmd_tickettype_float_delete"
        Protected Const TICKETTYPE_FLOAT_ADDNEW_PREFIX As String = "cmd_tickettype_float_addnew"
        Protected Const TICKETTYPE_FLOAT_EXIT_PREFIX As String = "cmd_tickettype_float_exit"
        Protected Const TICKETTYPE_FLOAT_MOVEUP_PREFIX As String = "cmd_tickettype_float_moveup"
        Protected Const TICKETTYPE_FLOAT_MOVEDOWN_PREFIX As String = "cmd_tickettype_float_movedown"
        Protected Const TICKETTYPE_FLOAT_TICKETTYPE As String = "tickettype_float_tickettype"
        Protected Const TICKETTYPE_FLOAT_TICKETTYPE_COL As Integer = 0
        Protected Const TICKETTYPE_FLOAT_POPUP As String = "tickettype_float_popup"
        Protected Const TICKETTYPE_FLOAT_POPUP_COL As Integer = 1
        Protected Const TICKETTYPE_FLOAT_DRIVE As String = "tickettype_float_drive"
        Protected Const TICKETTYPE_FLOAT_DRIVE_COL As Integer = 2
        Protected Const TICKETTYPE_FLOAT_SCHED_PRICES As String = "tickettype_float_sched_prices"
        Protected Const TICKETTYPE_FLOAT_SCHED_PRICES_COL As Integer = 3
        Protected Const TICKETTYPE_FLOAT_REASSIGN As String = "tickettype_float_reassign"
        Protected Const TICKETTYPE_FLOAT_REASSIGN_COL As Integer = 4
        Protected Const TICKETTYPE_FLOAT_REQUIRE As String = "tickettype_float_require"
        Protected Const TICKETTYPE_FLOAT_REQUIRE_COL As Integer = 5
        Protected Const TICKETTYPE_FLOAT_UPCHARGE As String = "tickettype_float_upcharge"
        Protected Const TICKETTYPE_FLOAT_UPCHARGE_COL As Integer = 6
        Protected Const TICKETTYPE_FLOAT_SCHEDULE As String = "tickettype_float_schedule"
        Protected Const TICKETTYPE_FLOAT_SCHEDULE_COL As Integer = 7
        Protected Const TICKETTYPE_FLOAT_PHRASE As String = "tickettype_float_phrase"
        Protected Const TICKETTYPE_FLOAT_PHRASE_COL As Integer = 8
        Protected Const TICKETTYPE_FLOAT_AUTOCOMBO As String = "tickettype_float_autocombo"
        Protected Const TICKETTYPE_FLOAT_AUTOCOMBO_COL As Integer = 9
        Protected Const TICKETTYPE_FLOAT_CUSTOMERSURVEY As String = "tickettype_float_customersurvey"
        Protected Const TICKETTYPE_FLOAT_CUSTOMERSURVEY_COL As Integer = 10
        Protected Const TICKETTYPE_FLOAT_SURVEYINTERVAL As String = "tickettype_float_surveyinterval"
        Protected Const TICKETTYPE_FLOAT_SURVEYINTERVAL_COL As Integer = 11
        Protected Const TICKETTYPE_FLOAT_EXCLUDE_1 As String = "tickettype_float_exclude_1"
        Protected Const TICKETTYPE_FLOAT_EXCLUDE_1_COL As Integer = 12
        Protected Const TICKETTYPE_FLOAT_EXCLUDE_2 As String = "tickettype_float_exclude_2"
        Protected Const TICKETTYPE_FLOAT_EXCLUDE_2_COL As Integer = 13
        Protected Const TICKETTYPE_FLOAT_EXCLUDE_3 As String = "tickettype_float_exclude_3"
        Protected Const TICKETTYPE_FLOAT_EXCLUDE_3_COL As Integer = 14
        Protected Const TICKETTYPE_FLOAT_EXCLUDE_4 As String = "tickettype_float_exclude_4"
        Protected Const TICKETTYPE_FLOAT_EXCLUDE_4_COL As Integer = 15
        Protected Const TICKETTYPE_FLOAT_KDSTYPE As String = "tickettype_float_kdstype"
        Protected Const TICKETTYPE_FLOAT_KDSTYPE_COL As Integer = 16
        Protected Const TICKETTYPE_FLOAT_COLOR As String = "tickettype_float_color"
        Protected Const TICKETTYPE_FLOAT_COLOR_COL As Integer = 17
        Protected Const TICKETTYPE_FLOAT_HIDDEN As String = "tickettype_float_hidden"
        Protected Const TICKETTYPE_FLOAT_HIDDEN_COL As Integer = 18
        Protected Const TICKETTYPE_FLOAT_SUPPRESS_KDS As String = "tickettype_float_suppress_kds"
        Protected Const TICKETTYPE_FLOAT_SUPPRESS_KDS_COL As Integer = 19
        Protected Const TICKETTYPE_FLOAT_SUPPRESS_KP As String = "tickettype_float_suppress_kp"
        Protected Const TICKETTYPE_FLOAT_SUPPRESS_KP_COL As Integer = 20
        Protected Const TICKETTYPE_FLOAT_DSP As String = "tickettype_float_dsp"
        Protected Const TICKETTYPE_FLOAT_DSP_COL As Integer = 21

        Protected Const LOGO_HEADER_BUTTON_PREFIX As String = "cmd_logo_header"
        Protected Const LOGO_HEADER_DELETE_PREFIX As String = "cmd_logo_header_delete"
        Protected Const LOGO_HEADER_ADDNEW_PREFIX As String = "cmd_logo_header_addnew"
        Protected Const LOGO_HEADER_EXIT_PREFIX As String = "cmd_logo_header_exit"
        Protected Const LOGO_HEADER_MOVEUP_PREFIX As String = "cmd_logo_header_moveup"
        Protected Const LOGO_HEADER_MOVEDOWN_PREFIX As String = "cmd_logo_header_movedown"
        Protected Const LOGO_HEADER_TEXT As String = "logo_header_text"
        Protected Const LOGO_HEADER_TEXT_COL As Integer = 0
        Protected Const LOGO_HEADER_JUSTIFICATION As String = "logo_header_justification"
        Protected Const LOGO_HEADER_JUSTIFICATION_COL As Integer = 1
        Protected Const LOGO_HEADER_LARGE As String = "logo_header_large"
        Protected Const LOGO_HEADER_LARGE_COL As Integer = 2
        Protected Const LOGO_HEADER_BOLD As String = "logo_header_bold"
        Protected Const LOGO_HEADER_BOLD_COL As Integer = 3
        Protected Const LOGO_HEADER_WIDE As String = "logo_header_wide"
        Protected Const LOGO_HEADER_WIDE_COL As Integer = 4
        Protected Const LOGO_HEADER_HIGH As String = "logo_header_high"
        Protected Const LOGO_HEADER_HIGH_COL As Integer = 5
        Protected Const LOGO_HEADER_UNDERLINE As String = "logo_header_underline"
        Protected Const LOGO_HEADER_UNDERLINE_COL As Integer = 6
        Protected Const LOGO_HEADER_QR_CODE As String = "logo_header_qr_code"
        Protected Const LOGO_HEADER_QR_CODE_COL As Integer = 7
        Protected Const LOGO_HEADER_QR_PIXELS As String = "logo_header_qr_pixels"
        Protected Const LOGO_HEADER_QR_PIXELS_COL As Integer = 8
        Protected Const LOGO_HEADER_QR_QUALITY As String = "logo_header_qr_quality"
        Protected Const LOGO_HEADER_QR_QUALITY_COL As Integer = 9

        Protected Const LOGO_FOOTER_BUTTON_PREFIX As String = "cmd_logo_footer"
        Protected Const LOGO_FOOTER_DELETE_PREFIX As String = "cmd_logo_footer_delete"
        Protected Const LOGO_FOOTER_ADDNEW_PREFIX As String = "cmd_logo_footer_addnew"
        Protected Const LOGO_FOOTER_EXIT_PREFIX As String = "cmd_logo_footer_exit"
        Protected Const LOGO_FOOTER_MOVEUP_PREFIX As String = "cmd_logo_footer_moveup"
        Protected Const LOGO_FOOTER_MOVEDOWN_PREFIX As String = "cmd_logo_footer_movedown"
        Protected Const LOGO_FOOTER_TEXT As String = "logo_footer_text"
        Protected Const LOGO_FOOTER_TEXT_COL As Integer = 0
        Protected Const LOGO_FOOTER_JUSTIFICATION As String = "logo_footer_justification"
        Protected Const LOGO_FOOTER_JUSTIFICATION_COL As Integer = 1
        Protected Const LOGO_FOOTER_LARGE As String = "logo_footer_large"
        Protected Const LOGO_FOOTER_LARGE_COL As Integer = 2
        Protected Const LOGO_FOOTER_BOLD As String = "logo_footer_bold"
        Protected Const LOGO_FOOTER_BOLD_COL As Integer = 3
        Protected Const LOGO_FOOTER_WIDE As String = "logo_footer_wide"
        Protected Const LOGO_FOOTER_WIDE_COL As Integer = 4
        Protected Const LOGO_FOOTER_HIGH As String = "logo_footer_high"
        Protected Const LOGO_FOOTER_HIGH_COL As Integer = 5
        Protected Const LOGO_FOOTER_UNDERLINE As String = "logo_footer_underline"
        Protected Const LOGO_FOOTER_UNDERLINE_COL As Integer = 6
        Protected Const LOGO_FOOTER_QR_CODE As String = "logo_footer_qr_code"
        Protected Const LOGO_FOOTER_QR_CODE_COL As Integer = 7
        Protected Const LOGO_FOOTER_QR_PIXELS As String = "logo_footer_qr_pixels"
        Protected Const LOGO_FOOTER_QR_PIXELS_COL As Integer = 8
        Protected Const LOGO_FOOTER_QR_QUALITY As String = "logo_footer_qr_quality"
        Protected Const LOGO_FOOTER_QR_QUALITY_COL As Integer = 9

        Protected Const CS_HEADER_BUTTON_PREFIX As String = "cmd_cs_header"
        Protected Const CS_HEADER_DELETE_PREFIX As String = "cmd_cs_header_delete"
        Protected Const CS_HEADER_ADDNEW_PREFIX As String = "cmd_cs_header_addnew"
        Protected Const CS_HEADER_EXIT_PREFIX As String = "cmd_cs_header_exit"
        Protected Const CS_HEADER_MOVEUP_PREFIX As String = "cmd_cs_header_moveup"
        Protected Const CS_HEADER_MOVEDOWN_PREFIX As String = "cmd_cs_header_movedown"
        Protected Const CS_HEADER_TEXT As String = "cs_header_text"
        Protected Const CS_HEADER_TEXT_COL As Integer = 0
        Protected Const CS_HEADER_JUSTIFICATION As String = "cs_header_justification"
        Protected Const CS_HEADER_JUSTIFICATION_COL As Integer = 1
        Protected Const CS_HEADER_LARGE As String = "cs_header_large"
        Protected Const CS_HEADER_LARGE_COL As Integer = 2
        Protected Const CS_HEADER_BOLD As String = "cs_header_bold"
        Protected Const CS_HEADER_BOLD_COL As Integer = 3
        Protected Const CS_HEADER_WIDE As String = "cs_header_wide"
        Protected Const CS_HEADER_WIDE_COL As Integer = 4
        Protected Const CS_HEADER_HIGH As String = "cs_header_high"
        Protected Const CS_HEADER_HIGH_COL As Integer = 5
        Protected Const CS_HEADER_UNDERLINE As String = "cs_header_underline"
        Protected Const CS_HEADER_UNDERLINE_COL As Integer = 6

        Protected Const CS_FOOTER_BUTTON_PREFIX As String = "cmd_cs_footer"
        Protected Const CS_FOOTER_DELETE_PREFIX As String = "cmd_cs_footer_delete"
        Protected Const CS_FOOTER_ADDNEW_PREFIX As String = "cmd_cs_footer_addnew"
        Protected Const CS_FOOTER_EXIT_PREFIX As String = "cmd_cs_footer_exit"
        Protected Const CS_FOOTER_MOVEUP_PREFIX As String = "cmd_cs_footer_moveup"
        Protected Const CS_FOOTER_MOVEDOWN_PREFIX As String = "cmd_cs_footer_movedown"
        Protected Const CS_FOOTER_TEXT As String = "cs_footer_text"
        Protected Const CS_FOOTER_TEXT_COL As Integer = 0
        Protected Const CS_FOOTER_JUSTIFICATION As String = "cs_footer_justification"
        Protected Const CS_FOOTER_JUSTIFICATION_COL As Integer = 1
        Protected Const CS_FOOTER_LARGE As String = "cs_footer_large"
        Protected Const CS_FOOTER_LARGE_COL As Integer = 2
        Protected Const CS_FOOTER_BOLD As String = "cs_footer_bold"
        Protected Const CS_FOOTER_BOLD_COL As Integer = 3
        Protected Const CS_FOOTER_WIDE As String = "cs_footer_wide"
        Protected Const CS_FOOTER_WIDE_COL As Integer = 4
        Protected Const CS_FOOTER_HIGH As String = "cs_footer_high"
        Protected Const CS_FOOTER_HIGH_COL As Integer = 5
        Protected Const CS_FOOTER_UNDERLINE As String = "cs_footer_underline"
        Protected Const CS_FOOTER_UNDERLINE_COL As Integer = 6

        Protected Const EXCLUSIVE_FLAG As String = "^ex"

        Protected Const NULL_CONTROL As String = "null control"

        Protected Const IN_COL_TYPE As String = "type"
        Protected Const IN_COL_OPT As String = "opt"
        Protected Const IN_COL_VALUE As String = "value"

        Protected Const CONFIG_COL_DEFAULT_VALUE As String = "DefaultValue"
        Protected Const CONFIG_COL_VIEW As String = "View"
        Protected Const CONFIG_COL_DISPLAY As String = "Display"
        Protected Const CONFIG_COL_GROUP_DISPLAY As String = "GroupDisplay"
        Protected Const CONFIG_COL_CONTROL_TYPE As String = "ControlType"
        Protected Const CONFIG_COL_FLOAT_TYPE As String = "FloatType"
        Protected Const CONFIG_COL_COMBO_VALUES As String = "ComboValues"
        Protected Const CONFIG_COL_DATA_VALIDATION As String = "DataValidation"
        Protected Const CONFIG_COL_CONTROL_POSTFIX As String = "ControlPostfix"
        Protected Const CONFIG_COL_EXTRA_OUTPUT As String = "ExtraOutput"
        Protected Const CONFIG_COL_EXTRA_OUTPUT_VALUE As String = "ExtraOutputValue"
        Protected Const CONFIG_COL_TYPE As String = "type"
        Protected Const CONFIG_COL_OPT As String = "opt"
        Protected Const CONFIG_COL_VALUE As String = "value"

        Protected Const SESSION_FLOAT_ROWS As String = "Options_FloatRows"
        Protected Const SESSION_CURRENT_TAB As String = "Options_CurrentTab"
        Protected Const SESSION_OPTIONS_TEST As String = "Options_Test"
        Protected Const SESSION_ARY_PAY_TYPES As String = "Options_aryPayTypes"
        Protected Const SESSION_OPTIONS_STORE As String = "Options_PosOptionsStore"
        Protected Const SESSION_STORE_DATA As String = "Options_StoreData"
        Protected Const SESSION_OPTIONS_REGISTER As String = "Options_Register"
        Protected Const SESSION_OPTIONS_SOURCE_REGISTER As String = "Options_SourceRegister"
        Protected Const SESSION_ARY_LOGO_HEADER As String = "Options_LogoHeader"
        Protected Const SESSION_ARY_LOGO_FOOTER As String = "Options_LogoFooter"
        Protected Const SESSION_ARY_CS_HEADER As String = "Options_CSHeader"
        Protected Const SESSION_ARY_CS_FOOTER As String = "Options_CSFooter"
        Protected Const SESSION_ARY_TICKET_TYPES As String = "Options_aryTicketTypes"

#End Region

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub



        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init

            ' Check for expired session.
            If IsPostBack Then
                If Session(SESSION_OPTIONS_TEST) Is Nothing Then
                    Exit Sub
                End If
            Else
                Session(SESSION_OPTIONS_TEST) = "test"
            End If

            ' Load dynamic page controls prior to loading the viewstate. That way, changes made to the control
            ' values persist over postbacks.
            LoadControls()

            InitializeComponent()
        End Sub

#End Region

#Region " Page Methods "

        Protected Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            Try

                ' Load password box "value" to create masked look on screen.

                For Each ixc_sID As String In _aryPassword
                    Dim txtBox As New TextBox
                    txtBox = FindControl(ixc_sID)
                    If Not txtBox Is Nothing Then
                        txtBox.Attributes.Add("value", txtBox.Text)
                    End If
                Next

                If Not IsPostBack Then
                    ' Client-side event definitions
                    cmdCancelClose.Attributes.Add("onclick", "return confirmClick('" + "Are you sure you want to cancel and lose all changes?" + "');")
                    'cboRegister.Attributes.Add("onchange", "return confirmClick('" + "If changes have been made and not saved, they will be lost. Do you wish to continue?" + "');")

                    If _sRegister <> "" Then
                        cboRegister.SelectedValue = _sRegister
                    End If
                Else
                    ' Check for expired session.
                    If Session(SESSION_OPTIONS_TEST) Is Nothing Then
                        Response.Write("You're session has expired.")
                        'Exit Sub
                    End If

                    divCopyRegister.Style.Add("display", "none")
                    If _sEvent = "cmdCopyRegister" Then
                        divCopyRegister.Style.Remove("display")
                        Exit Sub
                    End If

                    ' Show the display header div or show a button based on the value in _bShowDisplayHeader.
                    ' _bShowDisplayHeader is set in LoadControls() (called from Page_Init). These values need to
                    ' be set here instead in LoadControls() or they will be overwritten by the viewstate load.
                    Dim cmdFloat As LinkButton = FindControl(HEADER_FLOAT_BUTTON_PREFIX + HEADER_FLOAT_DISPLAY_POSTFIX)
                    If _bShowDisplayHeader Then
                        cmdFloat.Visible = False
                        divDisplayHeader.Style.Remove("display")
                    Else
                        cmdFloat.Visible = True
                        divDisplayHeader.Style.Add("display", "none")
                    End If

                    ' Show the print header div or show a button based on the value in _bShowPrintHeader.
                    cmdFloat = FindControl(HEADER_FLOAT_BUTTON_PREFIX + HEADER_FLOAT_PRINT_POSTFIX)
                    If _bShowPrintHeader Then
                        cmdFloat.Visible = False
                        divPrintHeader.Style.Remove("display")
                    Else
                        cmdFloat.Visible = True
                        divPrintHeader.Style.Add("display", "none")
                    End If

                    ' Show the pay type div or show a button based on the value in _bShowPayType.
                    cmdFloat = FindControl(PAYTYPE_FLOAT_BUTTON_PREFIX)
                    If _bShowPayType Then
                        cmdFloat.Visible = False
                        divPayType.Style.Remove("display")
                    Else
                        cmdFloat.Visible = True
                        divPayType.Style.Add("display", "none")
                    End If

                    ' Show the ticket type div or show a button based on the value in _bShowTicketType.
                    cmdFloat = FindControl(TICKETTYPE_FLOAT_BUTTON_PREFIX)
                    If _bShowTicketType Then
                        cmdFloat.Visible = False
                        divTicketType.Style.Remove("display")
                    Else
                        cmdFloat.Visible = True
                        divTicketType.Style.Add("display", "none")
                    End If

                    cmdFloat = FindControl(LOGO_HEADER_BUTTON_PREFIX)
                    If _bShowLogoHeader Then
                        cmdFloat.Visible = False
                        divLogoHeader.Style.Remove("display")
                    Else
                        cmdFloat.Visible = True
                        divLogoHeader.Style.Add("display", "none")
                    End If

                    cmdFloat = FindControl(LOGO_FOOTER_BUTTON_PREFIX)
                    If _bShowLogoFooter Then
                        cmdFloat.Visible = False
                        divLogoFooter.Style.Remove("display")
                    Else
                        cmdFloat.Visible = True
                        divLogoFooter.Style.Add("display", "none")
                    End If

                    cmdFloat = FindControl(CS_HEADER_BUTTON_PREFIX)
                    If _bShowCSHeader Then
                        cmdFloat.Visible = False
                        divCustomerSurveyHeader.Style.Remove("display")
                    Else
                        cmdFloat.Visible = True
                        divCustomerSurveyHeader.Style.Add("display", "none")
                    End If

                    cmdFloat = FindControl(CS_FOOTER_BUTTON_PREFIX)
                    If _bShowCSFooter Then
                        cmdFloat.Visible = False
                        divCustomerSurveyFooter.Style.Remove("display")
                    Else
                        cmdFloat.Visible = True
                        divCustomerSurveyFooter.Style.Add("display", "none")
                    End If

                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    ' The next blocks of code deal with "move up" and "move down" button clicks. This is done in
                    ' Page_Load (after the viewstate load) because values stored in page controls are swapped.
                    ' The basic logic is to check the Request("__EVENTTARGET") (stored in _sEvent) and see if that
                    ' values corresponds to a "move up" or "move down" link button click. If it does, determine the 
                    ' "move from" and "move to" rows of the appropriate table. Then swap the values in the controls
                    ' in those rows.
                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


                    ' iMoveFrom and iMoveTo will remain -1 if a "move" button wasn't clicked.
                    Dim iMoveFrom As Integer = -1 : Dim iMoveTo As Integer = -1
                    Dim sDisplayOrPrint As String = ""

                    ''''''''''''''''' Floating Display/Print Header "move down" button click '''''''''''''''''
                    Dim sEventText As String = _sEvent
                    ' The first part of sEventText will be HEADER_FLOAT_MOVEDOWN_PREFIX is this is a header div "move" button click.
                    If SafeSubString(sEventText, 0, HEADER_FLOAT_MOVEDOWN_PREFIX.Length) = HEADER_FLOAT_MOVEDOWN_PREFIX Then

                        ' Whack the HEADER_FLOAT_MOVEDOWN_PREFIX out of sEventText and see if it's display or print.
                        sEventText = sEventText.Substring(HEADER_FLOAT_MOVEDOWN_PREFIX.Length)

                        If SafeSubString(sEventText, 0, HEADER_FLOAT_DISPLAY_POSTFIX.Length) = HEADER_FLOAT_DISPLAY_POSTFIX Then
                            ' sEventText now looks like "HEADER_FLOAT_DISPLAY_POSTFIXn". Get the n value and load it in iMoveFrom.
                            iMoveFrom = Convert.ToInt32(sEventText.Substring(HEADER_FLOAT_DISPLAY_POSTFIX.Length))
                            sDisplayOrPrint = HEADER_FLOAT_DISPLAY_POSTFIX
                        Else
                            ' sEventText now looks like "HEADER_FLOAT_PRINT_POSTFIXn". Get the n value and load it in iMoveFrom.
                            iMoveFrom = Convert.ToInt32(sEventText.Substring(HEADER_FLOAT_PRINT_POSTFIX.Length))
                            sDisplayOrPrint = HEADER_FLOAT_PRINT_POSTFIX
                        End If

                        ' When a div is first created, table rows are inserted in numeric order. (i.e. Row 0, Row 1, 
                        ' Row 2 ... (imagine that)) If Row 1 is deleted, the rows aren't renumbered, Row 0 and Row 2 would
                        ' remain. Information about those rows are stored in aryVals, and the enties corresponding to deleted
                        ' rows have the flag FLOATING_ROW_WHACK concatenated to them. When a row is being "moved down," the  
                        ' next non-deleted row must be found. This loop accomlishes that. 
                        Dim aryVals As ArrayList = CType(Session("aryVals" + sDisplayOrPrint), ArrayList)
                        For ix As Integer = iMoveFrom + 1 To aryVals.Count - 1
                            If aryVals(ix).ToString().IndexOf(FLOATING_ROW_WHACK) = -1 Then
                                iMoveTo = ix
                                Exit For
                            End If
                        Next
                    End If


                    ''''''''''''''''' Floating Display/Print Header "move up" button click '''''''''''''''''
                    sEventText = _sEvent
                    ' The first part of sEventText will be HEADER_FLOAT_MOVEUP_PREFIX is this is a header div "move" button click.
                    If SafeSubString(sEventText, 0, HEADER_FLOAT_MOVEUP_PREFIX.Length) = HEADER_FLOAT_MOVEUP_PREFIX Then

                        ' Whack the HEADER_FLOAT_MOVEUP_PREFIX out of sEventText and see if it's display or print.
                        sEventText = sEventText.Substring(HEADER_FLOAT_MOVEUP_PREFIX.Length)

                        If SafeSubString(sEventText, 0, HEADER_FLOAT_DISPLAY_POSTFIX.Length) = HEADER_FLOAT_DISPLAY_POSTFIX Then
                            ' sEventText now looks like "HEADER_FLOAT_DISPLAY_POSTFIXn". Get the n value and load it in iMoveFrom.
                            iMoveFrom = Convert.ToInt32(sEventText.Substring(HEADER_FLOAT_DISPLAY_POSTFIX.Length))
                            sDisplayOrPrint = HEADER_FLOAT_DISPLAY_POSTFIX
                        Else
                            ' sEventText now looks like "HEADER_FLOAT_PRINT_POSTFIXn". Get the n value and load it in iMoveFrom.
                            iMoveFrom = Convert.ToInt32(sEventText.Substring(HEADER_FLOAT_PRINT_POSTFIX.Length))
                            sDisplayOrPrint = HEADER_FLOAT_PRINT_POSTFIX
                        End If

                        ' Find the appropriate "non-deleted" row. (See the HEADER_FLOAT_MOVEDOWN_PREFIX code for more details.
                        Dim aryVals As ArrayList = CType(Session("aryVals" + sDisplayOrPrint), ArrayList)
                        For ix As Integer = iMoveFrom - 1 To 0 Step -1
                            If aryVals(ix).ToString().IndexOf(FLOATING_ROW_WHACK) = -1 Then
                                iMoveTo = ix
                                Exit For
                            End If
                        Next

                    End If


                    ''''''''''''''''' MoveFrom and iMoveTo will not equal -1 if a header "move" button has been clicked '''''''''''''''''
                    If iMoveFrom <> -1 And iMoveTo <> -1 Then

                        ' Hard-code:
                        ' Note: This is hard-coded for two controls on each row of a header div. If more controls are added,
                        ' this code needs to be changed.

                        If (Not FindControl(HEADER_FLOAT_COMBO_PREFIX_LEFT + sDisplayOrPrint + iMoveFrom.ToString()) Is Nothing) And _
                                (Not FindControl(HEADER_FLOAT_COMBO_PREFIX_LEFT + sDisplayOrPrint + iMoveTo.ToString()) Is Nothing) Then

                            SwapControlValue(FindControl(HEADER_FLOAT_COMBO_PREFIX_LEFT + sDisplayOrPrint + iMoveFrom.ToString()), _
                                FindControl(HEADER_FLOAT_COMBO_PREFIX_LEFT + sDisplayOrPrint + iMoveTo.ToString()))
                        End If

                        If (Not FindControl(HEADER_FLOAT_COMBO_PREFIX_RIGHT + sDisplayOrPrint + iMoveFrom.ToString()) Is Nothing) And _
                                (Not FindControl(HEADER_FLOAT_COMBO_PREFIX_RIGHT + sDisplayOrPrint + iMoveTo.ToString()) Is Nothing) Then

                            SwapControlValue(FindControl(HEADER_FLOAT_COMBO_PREFIX_RIGHT + sDisplayOrPrint + iMoveFrom.ToString()), _
                                FindControl(HEADER_FLOAT_COMBO_PREFIX_RIGHT + sDisplayOrPrint + iMoveTo.ToString()))
                        End If

                        If (Not FindControl(HEADER_FLOAT_TTEXCLUDE + sDisplayOrPrint + iMoveFrom.ToString()) Is Nothing) And _
                        (Not FindControl(HEADER_FLOAT_TTEXCLUDE + sDisplayOrPrint + iMoveTo.ToString()) Is Nothing) Then

                            SwapControlValue(FindControl(HEADER_FLOAT_TTEXCLUDE + sDisplayOrPrint + iMoveFrom.ToString()), _
                                FindControl(HEADER_FLOAT_TTEXCLUDE + sDisplayOrPrint + iMoveTo.ToString()))
                        End If

                        If (Not FindControl(HEADER_FLOAT_JUSTIFICATION + sDisplayOrPrint + iMoveFrom.ToString()) Is Nothing) And _
                                (Not FindControl(HEADER_FLOAT_JUSTIFICATION + sDisplayOrPrint + iMoveTo.ToString()) Is Nothing) Then

                            SwapControlValue(FindControl(HEADER_FLOAT_JUSTIFICATION + sDisplayOrPrint + iMoveFrom.ToString()), _
                                FindControl(HEADER_FLOAT_JUSTIFICATION + sDisplayOrPrint + iMoveTo.ToString()))
                        End If

                        If (Not FindControl(HEADER_FLOAT_LARGE + sDisplayOrPrint + iMoveFrom.ToString()) Is Nothing) And _
                                (Not FindControl(HEADER_FLOAT_LARGE + sDisplayOrPrint + iMoveTo.ToString()) Is Nothing) Then

                            SwapControlValue(FindControl(HEADER_FLOAT_LARGE + sDisplayOrPrint + iMoveFrom.ToString()), _
                                FindControl(HEADER_FLOAT_LARGE + sDisplayOrPrint + iMoveTo.ToString()))
                        End If

                        If (Not FindControl(HEADER_FLOAT_BOLD + sDisplayOrPrint + iMoveFrom.ToString()) Is Nothing) And _
                                (Not FindControl(HEADER_FLOAT_BOLD + sDisplayOrPrint + iMoveTo.ToString()) Is Nothing) Then

                            SwapControlValue(FindControl(HEADER_FLOAT_BOLD + sDisplayOrPrint + iMoveFrom.ToString()), _
                                FindControl(HEADER_FLOAT_BOLD + sDisplayOrPrint + iMoveTo.ToString()))
                        End If

                        If (Not FindControl(HEADER_FLOAT_WIDE + sDisplayOrPrint + iMoveFrom.ToString()) Is Nothing) And _
                                (Not FindControl(HEADER_FLOAT_WIDE + sDisplayOrPrint + iMoveTo.ToString()) Is Nothing) Then

                            SwapControlValue(FindControl(HEADER_FLOAT_WIDE + sDisplayOrPrint + iMoveFrom.ToString()), _
                                FindControl(HEADER_FLOAT_WIDE + sDisplayOrPrint + iMoveTo.ToString()))
                        End If

                        If (Not FindControl(HEADER_FLOAT_HIGH + sDisplayOrPrint + iMoveFrom.ToString()) Is Nothing) And _
                                (Not FindControl(HEADER_FLOAT_HIGH + sDisplayOrPrint + iMoveTo.ToString()) Is Nothing) Then

                            SwapControlValue(FindControl(HEADER_FLOAT_HIGH + sDisplayOrPrint + iMoveFrom.ToString()), _
                                FindControl(HEADER_FLOAT_HIGH + sDisplayOrPrint + iMoveTo.ToString()))
                        End If

                        If (Not FindControl(HEADER_FLOAT_UNDERLINE + sDisplayOrPrint + iMoveFrom.ToString()) Is Nothing) And _
                                (Not FindControl(HEADER_FLOAT_UNDERLINE + sDisplayOrPrint + iMoveTo.ToString()) Is Nothing) Then

                            SwapControlValue(FindControl(HEADER_FLOAT_UNDERLINE + sDisplayOrPrint + iMoveFrom.ToString()), _
                                FindControl(HEADER_FLOAT_UNDERLINE + sDisplayOrPrint + iMoveTo.ToString()))
                        End If

                        If (Not FindControl(HEADER_FLOAT_COLOR + sDisplayOrPrint + iMoveFrom.ToString()) Is Nothing) And _
                        (Not FindControl(HEADER_FLOAT_COLOR + sDisplayOrPrint + iMoveTo.ToString()) Is Nothing) Then

                            SwapControlValue(FindControl(HEADER_FLOAT_COLOR + sDisplayOrPrint + iMoveFrom.ToString()), _
                                FindControl(HEADER_FLOAT_COLOR + sDisplayOrPrint + iMoveTo.ToString()))
                        End If
                    End If


                    ' Reset iMoveFrom and iMoveTo 
                    iMoveFrom = -1 : iMoveTo = -1

                    ''''''''''''''''' Floating pay type "move down" button click '''''''''''''''''
                    sEventText = _sEvent
                    ' The first part of sEventText will be PAYTYPE_FLOAT_MOVEDOWN_PREFIX is this is a pay type "move" button click.
                    If SafeSubString(sEventText, 0, PAYTYPE_FLOAT_MOVEDOWN_PREFIX.Length) = PAYTYPE_FLOAT_MOVEDOWN_PREFIX Then
                        ' sEventText looks like "PAYTYPE_FLOAT_MOVEDOWN_PREFIXn". Get the n value and load it in iMoveFrom.
                        If IsNumeric(sEventText.Substring(PAYTYPE_FLOAT_MOVEDOWN_PREFIX.Length)) Then
                            iMoveFrom = Convert.ToInt32(sEventText.Substring(PAYTYPE_FLOAT_MOVEDOWN_PREFIX.Length))
                        End If

                        ' Find the appropriate "non-deleted" row. (See the HEADER_FLOAT_MOVEDOWN_PREFIX code for more details.
                        Dim aryPayTypes As ArrayList = CType(Session(SESSION_ARY_PAY_TYPES), ArrayList)
                        For ix As Integer = iMoveFrom + 1 To aryPayTypes.Count - 1
                            If aryPayTypes(ix).ToString().IndexOf(FLOATING_ROW_WHACK) = -1 Then
                                iMoveTo = ix
                                Exit For
                            End If
                        Next
                    End If

                    ''''''''''''''''' Floating pay type "move up" button click '''''''''''''''''
                    sEventText = _sEvent
                    ' The first part of sEventText will be PAYTYPE_FLOAT_MOVEUP_PREFIX is this is a pay type "move" button click.
                    If SafeSubString(sEventText, 0, PAYTYPE_FLOAT_MOVEUP_PREFIX.Length) = PAYTYPE_FLOAT_MOVEUP_PREFIX Then
                        If IsNumeric(sEventText.Substring(PAYTYPE_FLOAT_MOVEUP_PREFIX.Length)) Then
                            iMoveFrom = Convert.ToInt32(sEventText.Substring(PAYTYPE_FLOAT_MOVEUP_PREFIX.Length))
                        End If

                        ' Find the appropriate "non-deleted" row. (See the HEADER_FLOAT_MOVEDOWN_PREFIX code for more details.
                        Dim aryPayTypes As ArrayList = CType(Session(SESSION_ARY_PAY_TYPES), ArrayList)
                        For ix As Integer = iMoveFrom - 1 To 0 Step -1
                            If aryPayTypes(ix).ToString().IndexOf(FLOATING_ROW_WHACK) = -1 Then
                                iMoveTo = ix
                                Exit For
                            End If
                        Next
                    End If


                    ''''''''''''''''' MoveFrom and iMoveTo will not equal -1 if a pay type "move" button has been clicked '''''''''''''''''
                    If iMoveFrom <> -1 And iMoveTo <> -1 Then

                        ' Hard-code:
                        ' Note: This is hard-coded for six controls on each row of a header div. If more controls are added,
                        ' this code needs to be changed.
                        If (Not FindControl(PAYTYPE_FLOAT_PAYTYPE + iMoveFrom.ToString()) Is Nothing) And _
                                (Not FindControl(PAYTYPE_FLOAT_PAYTYPE + iMoveTo.ToString()) Is Nothing) Then

                            SwapControlValue(FindControl(PAYTYPE_FLOAT_PAYTYPE + iMoveFrom.ToString()), _
                                    FindControl(PAYTYPE_FLOAT_PAYTYPE + iMoveTo.ToString()))
                        End If

                        If (Not FindControl(PAYTYPE_FLOAT_PAYTYPE_TICKET + iMoveFrom.ToString()) Is Nothing) And _
                                (Not FindControl(PAYTYPE_FLOAT_PAYTYPE_TICKET + iMoveTo.ToString()) Is Nothing) Then

                            SwapControlValue(FindControl(PAYTYPE_FLOAT_PAYTYPE_TICKET + iMoveFrom.ToString()), _
                                    FindControl(PAYTYPE_FLOAT_PAYTYPE_TICKET + iMoveTo.ToString()))
                        End If

                        If (Not FindControl(PAYTYPE_FLOAT_TYPE + iMoveFrom.ToString()) Is Nothing) And _
                                (Not FindControl(PAYTYPE_FLOAT_TYPE + iMoveTo.ToString()) Is Nothing) Then

                            SwapControlValue(FindControl(PAYTYPE_FLOAT_TYPE + iMoveFrom.ToString()), _
                                    FindControl(PAYTYPE_FLOAT_TYPE + iMoveTo.ToString()))
                        End If

                        If (Not FindControl(PAYTYPE_FLOAT_CHANGE + iMoveFrom.ToString()) Is Nothing) And _
                                (Not FindControl(PAYTYPE_FLOAT_CHANGE + iMoveTo.ToString()) Is Nothing) Then

                            SwapControlValue(FindControl(PAYTYPE_FLOAT_CHANGE + iMoveFrom.ToString()), _
                                    FindControl(PAYTYPE_FLOAT_CHANGE + iMoveTo.ToString()))
                        End If

                        If (Not FindControl(PAYTYPE_FLOAT_REFUND + iMoveFrom.ToString()) Is Nothing) And _
                                (Not FindControl(PAYTYPE_FLOAT_REFUND + iMoveTo.ToString()) Is Nothing) Then

                            SwapControlValue(FindControl(PAYTYPE_FLOAT_REFUND + iMoveFrom.ToString()), _
                                    FindControl(PAYTYPE_FLOAT_REFUND + iMoveTo.ToString()))
                        End If

                        If (Not FindControl(PAYTYPE_FLOAT_REQ_NUMBER + iMoveFrom.ToString()) Is Nothing) And _
                                (Not FindControl(PAYTYPE_FLOAT_REQ_NUMBER + iMoveTo.ToString()) Is Nothing) Then

                            SwapControlValue(FindControl(PAYTYPE_FLOAT_REQ_NUMBER + iMoveFrom.ToString()), _
                                    FindControl(PAYTYPE_FLOAT_REQ_NUMBER + iMoveTo.ToString()))
                        End If

                        If (Not FindControl(PAYTYPE_FLOAT_REQ_MGR_AUTH + iMoveFrom.ToString()) Is Nothing) And _
                                (Not FindControl(PAYTYPE_FLOAT_REQ_MGR_AUTH + iMoveTo.ToString()) Is Nothing) Then

                            SwapControlValue(FindControl(PAYTYPE_FLOAT_REQ_MGR_AUTH + iMoveFrom.ToString()), _
                                    FindControl(PAYTYPE_FLOAT_REQ_MGR_AUTH + iMoveTo.ToString()))
                        End If

                        If (Not FindControl(PAYTYPE_FLOAT_DRAWER + iMoveFrom.ToString()) Is Nothing) And _
                                (Not FindControl(PAYTYPE_FLOAT_DRAWER + iMoveTo.ToString()) Is Nothing) Then

                            SwapControlValue(FindControl(PAYTYPE_FLOAT_DRAWER + iMoveFrom.ToString()), _
                                    FindControl(PAYTYPE_FLOAT_DRAWER + iMoveTo.ToString()))

                        End If

                        If (Not FindControl(PAYTYPE_FLOAT_RECEIPT + iMoveFrom.ToString()) Is Nothing) And (Not FindControl(PAYTYPE_FLOAT_RECEIPT + iMoveTo.ToString()) Is Nothing) Then

                            SwapControlValue(FindControl(PAYTYPE_FLOAT_RECEIPT + iMoveFrom.ToString()), FindControl(PAYTYPE_FLOAT_RECEIPT + iMoveTo.ToString()))

                        End If

                        If (Not FindControl(PAYTYPE_FLOAT_REQ_CUST + iMoveFrom.ToString()) Is Nothing) And (Not FindControl(PAYTYPE_FLOAT_REQ_CUST + iMoveTo.ToString()) Is Nothing) Then

                            SwapControlValue(FindControl(PAYTYPE_FLOAT_REQ_CUST + iMoveFrom.ToString()), FindControl(PAYTYPE_FLOAT_REQ_CUST + iMoveTo.ToString()))

                        End If

                        If (Not FindControl(PAYTYPE_FLOAT_NO_SURCHARGE + iMoveFrom.ToString()) Is Nothing) And (Not FindControl(PAYTYPE_FLOAT_NO_SURCHARGE + iMoveTo.ToString()) Is Nothing) Then

                            SwapControlValue(FindControl(PAYTYPE_FLOAT_NO_SURCHARGE + iMoveFrom.ToString()), FindControl(PAYTYPE_FLOAT_NO_SURCHARGE + iMoveTo.ToString()))

                        End If

                    End If


                    ' Reset iMoveFrom and iMoveTo 
                    iMoveFrom = -1 : iMoveTo = -1

                    ''''''''''''''''' Floating ticket type "move down" button click '''''''''''''''''
                    sEventText = _sEvent
                    ' The first part of sEventText will be TICKETTYPE_FLOAT_MOVEDOWN_PREFIX is this is a ticket type "move" button click.
                    If SafeSubString(sEventText, 0, TICKETTYPE_FLOAT_MOVEDOWN_PREFIX.Length) = TICKETTYPE_FLOAT_MOVEDOWN_PREFIX Then
                        ' sEventText looks like "TICKETTYPE_FLOAT_MOVEDOWN_PREFIXn". Get the n value and load it in iMoveFrom.
                        If IsNumeric(sEventText.Substring(TICKETTYPE_FLOAT_MOVEDOWN_PREFIX.Length)) Then
                            iMoveFrom = Convert.ToInt32(sEventText.Substring(TICKETTYPE_FLOAT_MOVEDOWN_PREFIX.Length))
                        End If

                        ' Find the appropriate "non-deleted" row. (See the HEADER_FLOAT_MOVEDOWN_PREFIX code for more details.
                        Dim aryTicketTypes As ArrayList = CType(Session(SESSION_ARY_TICKET_TYPES), ArrayList)
                        For ix As Integer = iMoveFrom + 1 To aryTicketTypes.Count - 1
                            If aryTicketTypes(ix).ToString().IndexOf(FLOATING_ROW_WHACK) = -1 Then
                                iMoveTo = ix
                                Exit For
                            End If
                        Next
                    End If

                    ''''''''''''''''' Floating ticket type "move up" button click '''''''''''''''''
                    sEventText = _sEvent
                    ' The first part of sEventText will be TICKETTYPE_FLOAT_MOVEUP_PREFIX is this is a ticket type "move" button click.
                    If SafeSubString(sEventText, 0, TICKETTYPE_FLOAT_MOVEUP_PREFIX.Length) = TICKETTYPE_FLOAT_MOVEUP_PREFIX Then
                        If IsNumeric(sEventText.Substring(TICKETTYPE_FLOAT_MOVEUP_PREFIX.Length)) Then
                            iMoveFrom = Convert.ToInt32(sEventText.Substring(TICKETTYPE_FLOAT_MOVEUP_PREFIX.Length))
                        End If

                        ' Find the appropriate "non-deleted" row. (See the HEADER_FLOAT_MOVEDOWN_PREFIX code for more details.
                        Dim aryTicketTypes As ArrayList = CType(Session(SESSION_ARY_TICKET_TYPES), ArrayList)
                        For ix As Integer = iMoveFrom - 1 To 0 Step -1
                            If aryTicketTypes(ix).ToString().IndexOf(FLOATING_ROW_WHACK) = -1 Then
                                iMoveTo = ix
                                Exit For
                            End If
                        Next
                    End If


                    ''''''''''''''''' MoveFrom and iMoveTo will not equal -1 if a ticket type "move" button has been clicked '''''''''''''''''
                    If iMoveFrom <> -1 And iMoveTo <> -1 Then

                        ' Hard-code:
                        ' Note: This is hard-coded for six controls on each row of a header div. If more controls are added,
                        ' this code needs to be changed.
                        If (Not FindControl(TICKETTYPE_FLOAT_TICKETTYPE + iMoveFrom.ToString()) Is Nothing) And _
                                (Not FindControl(TICKETTYPE_FLOAT_TICKETTYPE + iMoveTo.ToString()) Is Nothing) Then

                            SwapControlValue(FindControl(TICKETTYPE_FLOAT_TICKETTYPE + iMoveFrom.ToString()), _
                                    FindControl(TICKETTYPE_FLOAT_TICKETTYPE + iMoveTo.ToString()))
                        End If

                        If (Not FindControl(TICKETTYPE_FLOAT_POPUP + iMoveFrom.ToString()) Is Nothing) And _
                                (Not FindControl(TICKETTYPE_FLOAT_POPUP + iMoveTo.ToString()) Is Nothing) Then

                            SwapControlValue(FindControl(TICKETTYPE_FLOAT_POPUP + iMoveFrom.ToString()), _
                                    FindControl(TICKETTYPE_FLOAT_POPUP + iMoveTo.ToString()))
                        End If

                        If (Not FindControl(TICKETTYPE_FLOAT_REASSIGN + iMoveFrom.ToString()) Is Nothing) And _
                                (Not FindControl(TICKETTYPE_FLOAT_REASSIGN + iMoveTo.ToString()) Is Nothing) Then

                            SwapControlValue(FindControl(TICKETTYPE_FLOAT_REASSIGN + iMoveFrom.ToString()), _
                                    FindControl(TICKETTYPE_FLOAT_REASSIGN + iMoveTo.ToString()))
                        End If

                        If (Not FindControl(TICKETTYPE_FLOAT_REQUIRE + iMoveFrom.ToString()) Is Nothing) And _
                                (Not FindControl(TICKETTYPE_FLOAT_REQUIRE + iMoveTo.ToString()) Is Nothing) Then

                            SwapControlValue(FindControl(TICKETTYPE_FLOAT_REQUIRE + iMoveFrom.ToString()), _
                                    FindControl(TICKETTYPE_FLOAT_REQUIRE + iMoveTo.ToString()))
                        End If

                        If (Not FindControl(TICKETTYPE_FLOAT_SCHEDULE + iMoveFrom.ToString()) Is Nothing) And _
                            (Not FindControl(TICKETTYPE_FLOAT_SCHEDULE + iMoveTo.ToString()) Is Nothing) Then

                            SwapControlValue(FindControl(TICKETTYPE_FLOAT_SCHEDULE + iMoveFrom.ToString()), _
                                    FindControl(TICKETTYPE_FLOAT_SCHEDULE + iMoveTo.ToString()))
                        End If

                        If (Not FindControl(TICKETTYPE_FLOAT_PHRASE + iMoveFrom.ToString()) Is Nothing) And _
                            (Not FindControl(TICKETTYPE_FLOAT_PHRASE + iMoveTo.ToString()) Is Nothing) Then

                            SwapControlValue(FindControl(TICKETTYPE_FLOAT_PHRASE + iMoveFrom.ToString()), _
                                    FindControl(TICKETTYPE_FLOAT_PHRASE + iMoveTo.ToString()))
                        End If

                        If (Not FindControl(TICKETTYPE_FLOAT_EXCLUDE_1 + iMoveFrom.ToString()) Is Nothing) And _
                                (Not FindControl(TICKETTYPE_FLOAT_EXCLUDE_1 + iMoveTo.ToString()) Is Nothing) Then

                            SwapControlValue(FindControl(TICKETTYPE_FLOAT_EXCLUDE_1 + iMoveFrom.ToString()), _
                                    FindControl(TICKETTYPE_FLOAT_EXCLUDE_1 + iMoveTo.ToString()))
                        End If

                        If (Not FindControl(TICKETTYPE_FLOAT_EXCLUDE_2 + iMoveFrom.ToString()) Is Nothing) And _
                                (Not FindControl(TICKETTYPE_FLOAT_EXCLUDE_2 + iMoveTo.ToString()) Is Nothing) Then

                            SwapControlValue(FindControl(TICKETTYPE_FLOAT_EXCLUDE_2 + iMoveFrom.ToString()), _
                                    FindControl(TICKETTYPE_FLOAT_EXCLUDE_2 + iMoveTo.ToString()))
                        End If

                        If (Not FindControl(TICKETTYPE_FLOAT_EXCLUDE_3 + iMoveFrom.ToString()) Is Nothing) And _
                                (Not FindControl(TICKETTYPE_FLOAT_EXCLUDE_3 + iMoveTo.ToString()) Is Nothing) Then

                            SwapControlValue(FindControl(TICKETTYPE_FLOAT_EXCLUDE_3 + iMoveFrom.ToString()), _
                                    FindControl(TICKETTYPE_FLOAT_EXCLUDE_3 + iMoveTo.ToString()))
                        End If

                        If (Not FindControl(TICKETTYPE_FLOAT_EXCLUDE_4 + iMoveFrom.ToString()) Is Nothing) And _
                                (Not FindControl(TICKETTYPE_FLOAT_EXCLUDE_4 + iMoveTo.ToString()) Is Nothing) Then

                            SwapControlValue(FindControl(TICKETTYPE_FLOAT_EXCLUDE_4 + iMoveFrom.ToString()), _
                                    FindControl(TICKETTYPE_FLOAT_EXCLUDE_4 + iMoveTo.ToString()))
                        End If

                        If (Not FindControl(TICKETTYPE_FLOAT_KDSTYPE + iMoveFrom.ToString()) Is Nothing) And _
                                (Not FindControl(TICKETTYPE_FLOAT_KDSTYPE + iMoveTo.ToString()) Is Nothing) Then

                            SwapControlValue(FindControl(TICKETTYPE_FLOAT_KDSTYPE + iMoveFrom.ToString()), _
                                    FindControl(TICKETTYPE_FLOAT_KDSTYPE + iMoveTo.ToString()))
                        End If

                        If (Not FindControl(TICKETTYPE_FLOAT_COLOR + iMoveFrom.ToString()) Is Nothing) And _
                                (Not FindControl(TICKETTYPE_FLOAT_COLOR + iMoveTo.ToString()) Is Nothing) Then

                            SwapControlValue(FindControl(TICKETTYPE_FLOAT_COLOR + iMoveFrom.ToString()), _
                                    FindControl(TICKETTYPE_FLOAT_COLOR + iMoveTo.ToString()))
                        End If

                        If (Not FindControl(TICKETTYPE_FLOAT_HIDDEN + iMoveFrom.ToString()) Is Nothing) And _
                                (Not FindControl(TICKETTYPE_FLOAT_HIDDEN + iMoveTo.ToString()) Is Nothing) Then

                            SwapControlValue(FindControl(TICKETTYPE_FLOAT_HIDDEN + iMoveFrom.ToString()), _
                                    FindControl(TICKETTYPE_FLOAT_HIDDEN + iMoveTo.ToString()))
                        End If

                        If (Not FindControl(TICKETTYPE_FLOAT_SUPPRESS_KDS + iMoveFrom.ToString()) Is Nothing) And _
                                (Not FindControl(TICKETTYPE_FLOAT_SUPPRESS_KDS + iMoveTo.ToString()) Is Nothing) Then

                            SwapControlValue(FindControl(TICKETTYPE_FLOAT_SUPPRESS_KDS + iMoveFrom.ToString()), _
                                    FindControl(TICKETTYPE_FLOAT_SUPPRESS_KDS + iMoveTo.ToString()))
                        End If

                        If (Not FindControl(TICKETTYPE_FLOAT_SUPPRESS_KP + iMoveFrom.ToString()) Is Nothing) And _
                                (Not FindControl(TICKETTYPE_FLOAT_SUPPRESS_KP + iMoveTo.ToString()) Is Nothing) Then

                            SwapControlValue(FindControl(TICKETTYPE_FLOAT_SUPPRESS_KP + iMoveFrom.ToString()), _
                                    FindControl(TICKETTYPE_FLOAT_SUPPRESS_KP + iMoveTo.ToString()))
                        End If

                        If (Not FindControl(TICKETTYPE_FLOAT_DSP + iMoveFrom.ToString()) Is Nothing) And _
                                (Not FindControl(TICKETTYPE_FLOAT_DSP + iMoveTo.ToString()) Is Nothing) Then

                            SwapControlValue(FindControl(TICKETTYPE_FLOAT_DSP + iMoveFrom.ToString()), _
                                    FindControl(TICKETTYPE_FLOAT_DSP + iMoveTo.ToString()))
                        End If

                    End If


                    ''''''''''''''''' Logo Header/Footer "move down" button click '''''''''''''''''
                    sEventText = _sEvent
                    iMoveFrom = -1 : iMoveTo = -1
                    If SafeSubString(sEventText, 0, LOGO_HEADER_MOVEDOWN_PREFIX.Length) = LOGO_HEADER_MOVEDOWN_PREFIX Then

                        iMoveFrom = Convert.ToInt32(sEventText.Substring(LOGO_HEADER_MOVEDOWN_PREFIX.Length))

                        Dim aryLogo As ArrayList = CType(Session(SESSION_ARY_LOGO_HEADER), ArrayList)
                        For ix As Integer = iMoveFrom + 1 To aryLogo.Count - 1
                            If aryLogo(ix).ToString().IndexOf(FLOATING_ROW_WHACK) = -1 Then
                                iMoveTo = ix
                                Exit For
                            End If
                        Next
                    ElseIf SafeSubString(sEventText, 0, LOGO_FOOTER_MOVEDOWN_PREFIX.Length) = LOGO_FOOTER_MOVEDOWN_PREFIX Then

                        iMoveFrom = Convert.ToInt32(sEventText.Substring(LOGO_FOOTER_MOVEDOWN_PREFIX.Length))

                        Dim aryLogo As ArrayList = CType(Session(SESSION_ARY_LOGO_FOOTER), ArrayList)
                        For ix As Integer = iMoveFrom + 1 To aryLogo.Count - 1
                            If aryLogo(ix).ToString().IndexOf(FLOATING_ROW_WHACK) = -1 Then
                                iMoveTo = ix
                                Exit For
                            End If
                        Next
                    End If


                    ''''''''''''''''' Logo Header/Footer "move up" button click '''''''''''''''''
                    sEventText = _sEvent
                    ' The first part of sEventText will be LOGO_HEADER_MOVEUP_PREFIX is this is a pay type "move" button click.
                    If SafeSubString(sEventText, 0, LOGO_HEADER_MOVEUP_PREFIX.Length) = LOGO_HEADER_MOVEUP_PREFIX Then
                        If IsNumeric(sEventText.Substring(LOGO_HEADER_MOVEUP_PREFIX.Length)) Then
                            iMoveFrom = Convert.ToInt32(sEventText.Substring(LOGO_HEADER_MOVEUP_PREFIX.Length))
                        End If

                        ' Find the appropriate "non-deleted" row. (See the HEADER_FLOAT_MOVEDOWN_PREFIX code for more details.
                        Dim aryLogo As ArrayList = CType(Session(SESSION_ARY_LOGO_HEADER), ArrayList)
                        For ix As Integer = iMoveFrom - 1 To 0 Step -1
                            If aryLogo(ix).ToString().IndexOf(FLOATING_ROW_WHACK) = -1 Then
                                iMoveTo = ix
                                Exit For
                            End If
                        Next
                    ElseIf SafeSubString(sEventText, 0, LOGO_FOOTER_MOVEUP_PREFIX.Length) = LOGO_FOOTER_MOVEUP_PREFIX Then
                        If IsNumeric(sEventText.Substring(LOGO_FOOTER_MOVEUP_PREFIX.Length)) Then
                            iMoveFrom = Convert.ToInt32(sEventText.Substring(LOGO_FOOTER_MOVEUP_PREFIX.Length))
                        End If

                        ' Find the appropriate "non-deleted" row. (See the FOOTER_FLOAT_MOVEDOWN_PREFIX code for more details.
                        Dim aryLogo As ArrayList = CType(Session(SESSION_ARY_LOGO_FOOTER), ArrayList)
                        For ix As Integer = iMoveFrom - 1 To 0 Step -1
                            If aryLogo(ix).ToString().IndexOf(FLOATING_ROW_WHACK) = -1 Then
                                iMoveTo = ix
                                Exit For
                            End If
                        Next
                    End If


                    ''''''''''''''''' MoveFrom and iMoveTo will not equal -1 if a pay type "move" button has been clicked '''''''''''''''''
                    If iMoveFrom <> -1 And iMoveTo <> -1 Then

                        Dim bHeader As Boolean = False : Dim bFooter As Boolean = False

                        If SafeSubString(sEventText, 0, LOGO_HEADER_MOVEUP_PREFIX.Length) = LOGO_HEADER_MOVEUP_PREFIX Then
                            bHeader = True
                        End If

                        If SafeSubString(sEventText, 0, LOGO_HEADER_MOVEDOWN_PREFIX.Length) = LOGO_HEADER_MOVEDOWN_PREFIX Then
                            bHeader = True
                        End If

                        If SafeSubString(sEventText, 0, LOGO_FOOTER_MOVEUP_PREFIX.Length) = LOGO_FOOTER_MOVEUP_PREFIX Then
                            bFooter = True
                        End If

                        If SafeSubString(sEventText, 0, LOGO_FOOTER_MOVEDOWN_PREFIX.Length) = LOGO_FOOTER_MOVEDOWN_PREFIX Then
                            bFooter = True
                        End If

                        ' Hard-code:
                        ' Note: This is hard-coded for six controls on each row of a header div. If more controls are added,
                        ' this code needs to be changed.

                        If bHeader Then
                            If (Not FindControl(LOGO_HEADER_TEXT + iMoveFrom.ToString()) Is Nothing) And _
                                    (Not FindControl(LOGO_HEADER_TEXT + iMoveTo.ToString()) Is Nothing) Then

                                SwapControlValue(FindControl(LOGO_HEADER_TEXT + iMoveFrom.ToString()), _
                                        FindControl(LOGO_HEADER_TEXT + iMoveTo.ToString()))
                            End If
                        ElseIf bFooter Then
                            If (Not FindControl(LOGO_FOOTER_TEXT + iMoveFrom.ToString()) Is Nothing) And _
                                   (Not FindControl(LOGO_FOOTER_TEXT + iMoveTo.ToString()) Is Nothing) Then

                                SwapControlValue(FindControl(LOGO_FOOTER_TEXT + iMoveFrom.ToString()), _
                                        FindControl(LOGO_FOOTER_TEXT + iMoveTo.ToString()))
                            End If
                        End If

                        If bHeader Then
                            If (Not FindControl(LOGO_HEADER_JUSTIFICATION + iMoveFrom.ToString()) Is Nothing) And _
                                    (Not FindControl(LOGO_HEADER_JUSTIFICATION + iMoveTo.ToString()) Is Nothing) Then

                                SwapControlValue(FindControl(LOGO_HEADER_JUSTIFICATION + iMoveFrom.ToString()), _
                                        FindControl(LOGO_HEADER_JUSTIFICATION + iMoveTo.ToString()))
                            End If
                        ElseIf bFooter Then
                            If (Not FindControl(LOGO_FOOTER_JUSTIFICATION + iMoveFrom.ToString()) Is Nothing) And _
                                   (Not FindControl(LOGO_FOOTER_JUSTIFICATION + iMoveTo.ToString()) Is Nothing) Then

                                SwapControlValue(FindControl(LOGO_FOOTER_JUSTIFICATION + iMoveFrom.ToString()), _
                                        FindControl(LOGO_FOOTER_JUSTIFICATION + iMoveTo.ToString()))
                            End If
                        End If

                        If bHeader Then
                            If (Not FindControl(LOGO_HEADER_LARGE + iMoveFrom.ToString()) Is Nothing) And _
                                    (Not FindControl(LOGO_HEADER_LARGE + iMoveTo.ToString()) Is Nothing) Then

                                SwapControlValue(FindControl(LOGO_HEADER_LARGE + iMoveFrom.ToString()), _
                                        FindControl(LOGO_HEADER_LARGE + iMoveTo.ToString()))
                            End If
                        ElseIf bFooter Then
                            If (Not FindControl(LOGO_FOOTER_LARGE + iMoveFrom.ToString()) Is Nothing) And _
                                   (Not FindControl(LOGO_FOOTER_LARGE + iMoveTo.ToString()) Is Nothing) Then

                                SwapControlValue(FindControl(LOGO_FOOTER_LARGE + iMoveFrom.ToString()), _
                                        FindControl(LOGO_FOOTER_LARGE + iMoveTo.ToString()))
                            End If
                        End If

                        If bHeader Then
                            If (Not FindControl(LOGO_HEADER_BOLD + iMoveFrom.ToString()) Is Nothing) And _
                                    (Not FindControl(LOGO_HEADER_BOLD + iMoveTo.ToString()) Is Nothing) Then

                                SwapControlValue(FindControl(LOGO_HEADER_BOLD + iMoveFrom.ToString()), _
                                        FindControl(LOGO_HEADER_BOLD + iMoveTo.ToString()))
                            End If
                        ElseIf bFooter Then
                            If (Not FindControl(LOGO_FOOTER_BOLD + iMoveFrom.ToString()) Is Nothing) And _
                                   (Not FindControl(LOGO_FOOTER_BOLD + iMoveTo.ToString()) Is Nothing) Then

                                SwapControlValue(FindControl(LOGO_FOOTER_BOLD + iMoveFrom.ToString()), _
                                        FindControl(LOGO_FOOTER_BOLD + iMoveTo.ToString()))
                            End If
                        End If

                        If bHeader Then
                            If (Not FindControl(LOGO_HEADER_WIDE + iMoveFrom.ToString()) Is Nothing) And _
                                    (Not FindControl(LOGO_HEADER_WIDE + iMoveTo.ToString()) Is Nothing) Then

                                SwapControlValue(FindControl(LOGO_HEADER_WIDE + iMoveFrom.ToString()), _
                                        FindControl(LOGO_HEADER_WIDE + iMoveTo.ToString()))
                            End If
                        ElseIf bFooter Then
                            If (Not FindControl(LOGO_FOOTER_WIDE + iMoveFrom.ToString()) Is Nothing) And _
                                   (Not FindControl(LOGO_FOOTER_WIDE + iMoveTo.ToString()) Is Nothing) Then

                                SwapControlValue(FindControl(LOGO_FOOTER_WIDE + iMoveFrom.ToString()), _
                                        FindControl(LOGO_FOOTER_WIDE + iMoveTo.ToString()))
                            End If
                        End If

                        If bHeader Then
                            If (Not FindControl(LOGO_HEADER_HIGH + iMoveFrom.ToString()) Is Nothing) And _
                                    (Not FindControl(LOGO_HEADER_HIGH + iMoveTo.ToString()) Is Nothing) Then

                                SwapControlValue(FindControl(LOGO_HEADER_HIGH + iMoveFrom.ToString()), _
                                        FindControl(LOGO_HEADER_HIGH + iMoveTo.ToString()))
                            End If
                        ElseIf bFooter Then
                            If (Not FindControl(LOGO_FOOTER_HIGH + iMoveFrom.ToString()) Is Nothing) And _
                                   (Not FindControl(LOGO_FOOTER_HIGH + iMoveTo.ToString()) Is Nothing) Then

                                SwapControlValue(FindControl(LOGO_FOOTER_HIGH + iMoveFrom.ToString()), _
                                        FindControl(LOGO_FOOTER_HIGH + iMoveTo.ToString()))
                            End If
                        End If

                        If bHeader Then
                            If (Not FindControl(LOGO_HEADER_UNDERLINE + iMoveFrom.ToString()) Is Nothing) And _
                                    (Not FindControl(LOGO_HEADER_UNDERLINE + iMoveTo.ToString()) Is Nothing) Then

                                SwapControlValue(FindControl(LOGO_HEADER_UNDERLINE + iMoveFrom.ToString()), _
                                        FindControl(LOGO_HEADER_UNDERLINE + iMoveTo.ToString()))
                            End If
                        ElseIf bFooter Then
                            If (Not FindControl(LOGO_FOOTER_UNDERLINE + iMoveFrom.ToString()) Is Nothing) And _
                                    (Not FindControl(LOGO_FOOTER_UNDERLINE + iMoveTo.ToString()) Is Nothing) Then

                                SwapControlValue(FindControl(LOGO_FOOTER_UNDERLINE + iMoveFrom.ToString()), _
                                        FindControl(LOGO_FOOTER_UNDERLINE + iMoveTo.ToString()))
                            End If
                        End If

                        If bHeader Then
                            If (Not FindControl(LOGO_HEADER_QR_CODE + iMoveFrom.ToString()) Is Nothing) And _
                                    (Not FindControl(LOGO_HEADER_QR_CODE + iMoveTo.ToString()) Is Nothing) Then

                                SwapControlValue(FindControl(LOGO_HEADER_QR_CODE + iMoveFrom.ToString()), _
                                        FindControl(LOGO_HEADER_QR_CODE + iMoveTo.ToString()))
                            End If
                        ElseIf bFooter Then
                            If (Not FindControl(LOGO_FOOTER_QR_CODE + iMoveFrom.ToString()) Is Nothing) And _
                                    (Not FindControl(LOGO_FOOTER_QR_CODE + iMoveTo.ToString()) Is Nothing) Then

                                SwapControlValue(FindControl(LOGO_FOOTER_QR_CODE + iMoveFrom.ToString()), _
                                        FindControl(LOGO_FOOTER_QR_CODE + iMoveTo.ToString()))
                            End If
                        End If

                        If bHeader Then
                            If (Not FindControl(LOGO_HEADER_QR_PIXELS + iMoveFrom.ToString()) Is Nothing) And _
                                    (Not FindControl(LOGO_HEADER_QR_PIXELS + iMoveTo.ToString()) Is Nothing) Then

                                SwapControlValue(FindControl(LOGO_HEADER_QR_PIXELS + iMoveFrom.ToString()), _
                                        FindControl(LOGO_HEADER_QR_PIXELS + iMoveTo.ToString()))
                            End If
                        ElseIf bFooter Then
                            If (Not FindControl(LOGO_FOOTER_QR_PIXELS + iMoveFrom.ToString()) Is Nothing) And _
                                    (Not FindControl(LOGO_FOOTER_QR_PIXELS + iMoveTo.ToString()) Is Nothing) Then

                                SwapControlValue(FindControl(LOGO_FOOTER_QR_PIXELS + iMoveFrom.ToString()), _
                                        FindControl(LOGO_FOOTER_QR_PIXELS + iMoveTo.ToString()))
                            End If
                        End If

                        If bHeader Then
                            If (Not FindControl(LOGO_HEADER_QR_QUALITY + iMoveFrom.ToString()) Is Nothing) And _
                                    (Not FindControl(LOGO_HEADER_QR_QUALITY + iMoveTo.ToString()) Is Nothing) Then

                                SwapControlValue(FindControl(LOGO_HEADER_QR_QUALITY + iMoveFrom.ToString()), _
                                        FindControl(LOGO_HEADER_QR_QUALITY + iMoveTo.ToString()))
                            End If
                        ElseIf bFooter Then
                            If (Not FindControl(LOGO_FOOTER_QR_QUALITY + iMoveFrom.ToString()) Is Nothing) And _
                                    (Not FindControl(LOGO_FOOTER_QR_QUALITY + iMoveTo.ToString()) Is Nothing) Then

                                SwapControlValue(FindControl(LOGO_FOOTER_QR_QUALITY + iMoveFrom.ToString()), _
                                        FindControl(LOGO_FOOTER_QR_QUALITY + iMoveTo.ToString()))
                            End If
                        End If

                    End If


                    ''''''''''''''''' Customer Survey Header/Footer "move down" button click '''''''''''''''''
                    sEventText = _sEvent
                    iMoveFrom = -1 : iMoveTo = -1
                    If SafeSubString(sEventText, 0, CS_HEADER_MOVEDOWN_PREFIX.Length) = CS_HEADER_MOVEDOWN_PREFIX Then

                        iMoveFrom = Convert.ToInt32(sEventText.Substring(CS_HEADER_MOVEDOWN_PREFIX.Length))

                        Dim aryCS As ArrayList = CType(Session(SESSION_ARY_CS_HEADER), ArrayList)
                        For ix As Integer = iMoveFrom + 1 To aryCS.Count - 1
                            If aryCS(ix).ToString().IndexOf(FLOATING_ROW_WHACK) = -1 Then
                                iMoveTo = ix
                                Exit For
                            End If
                        Next
                    ElseIf SafeSubString(sEventText, 0, CS_FOOTER_MOVEDOWN_PREFIX.Length) = CS_FOOTER_MOVEDOWN_PREFIX Then

                        iMoveFrom = Convert.ToInt32(sEventText.Substring(CS_FOOTER_MOVEDOWN_PREFIX.Length))

                        Dim aryCS As ArrayList = CType(Session(SESSION_ARY_CS_FOOTER), ArrayList)
                        For ix As Integer = iMoveFrom + 1 To aryCS.Count - 1
                            If aryCS(ix).ToString().IndexOf(FLOATING_ROW_WHACK) = -1 Then
                                iMoveTo = ix
                                Exit For
                            End If
                        Next
                    End If


                    ''''''''''''''''' Customer Survey Header/Footer "move up" button click '''''''''''''''''
                    sEventText = _sEvent
                    ' The first part of sEventText will be CS_HEADER_MOVEUP_PREFIX is this is a pay type "move" button click.
                    If SafeSubString(sEventText, 0, CS_HEADER_MOVEUP_PREFIX.Length) = CS_HEADER_MOVEUP_PREFIX Then
                        If IsNumeric(sEventText.Substring(CS_HEADER_MOVEUP_PREFIX.Length)) Then
                            iMoveFrom = Convert.ToInt32(sEventText.Substring(CS_HEADER_MOVEUP_PREFIX.Length))
                        End If

                        ' Find the appropriate "non-deleted" row. (See the HEADER_FLOAT_MOVEDOWN_PREFIX code for more details.
                        Dim aryCS As ArrayList = CType(Session(SESSION_ARY_CS_HEADER), ArrayList)
                        For ix As Integer = iMoveFrom - 1 To 0 Step -1
                            If aryCS(ix).ToString().IndexOf(FLOATING_ROW_WHACK) = -1 Then
                                iMoveTo = ix
                                Exit For
                            End If
                        Next
                    ElseIf SafeSubString(sEventText, 0, CS_FOOTER_MOVEUP_PREFIX.Length) = CS_FOOTER_MOVEUP_PREFIX Then
                        If IsNumeric(sEventText.Substring(CS_FOOTER_MOVEUP_PREFIX.Length)) Then
                            iMoveFrom = Convert.ToInt32(sEventText.Substring(CS_FOOTER_MOVEUP_PREFIX.Length))
                        End If

                        ' Find the appropriate "non-deleted" row. (See the FOOTER_FLOAT_MOVEDOWN_PREFIX code for more details.
                        Dim aryLogo As ArrayList = CType(Session(SESSION_ARY_CS_FOOTER), ArrayList)
                        For ix As Integer = iMoveFrom - 1 To 0 Step -1
                            If aryLogo(ix).ToString().IndexOf(FLOATING_ROW_WHACK) = -1 Then
                                iMoveTo = ix
                                Exit For
                            End If
                        Next
                    End If


                    ''''''''''''''''' MoveFrom and iMoveTo will not equal -1 if a pay type "move" button has been clicked '''''''''''''''''
                    If iMoveFrom <> -1 And iMoveTo <> -1 Then

                        Dim bHeader As Boolean = False : Dim bFooter As Boolean = False

                        If SafeSubString(sEventText, 0, CS_HEADER_MOVEUP_PREFIX.Length) = CS_HEADER_MOVEUP_PREFIX Then
                            bHeader = True
                        End If

                        If SafeSubString(sEventText, 0, CS_HEADER_MOVEDOWN_PREFIX.Length) = CS_HEADER_MOVEDOWN_PREFIX Then
                            bHeader = True
                        End If

                        If SafeSubString(sEventText, 0, CS_FOOTER_MOVEUP_PREFIX.Length) = CS_FOOTER_MOVEUP_PREFIX Then
                            bFooter = True
                        End If

                        If SafeSubString(sEventText, 0, CS_FOOTER_MOVEDOWN_PREFIX.Length) = CS_FOOTER_MOVEDOWN_PREFIX Then
                            bFooter = True
                        End If

                        ' Hard-code:
                        ' Note: This is hard-coded for six controls on each row of a header div. If more controls are added,
                        ' this code needs to be changed.

                        If bHeader Then
                            If (Not FindControl(CS_HEADER_TEXT + iMoveFrom.ToString()) Is Nothing) And _
                                    (Not FindControl(CS_HEADER_TEXT + iMoveTo.ToString()) Is Nothing) Then

                                SwapControlValue(FindControl(CS_HEADER_TEXT + iMoveFrom.ToString()), _
                                        FindControl(CS_HEADER_TEXT + iMoveTo.ToString()))
                            End If
                        ElseIf bFooter Then
                            If (Not FindControl(CS_FOOTER_TEXT + iMoveFrom.ToString()) Is Nothing) And _
                                   (Not FindControl(CS_FOOTER_TEXT + iMoveTo.ToString()) Is Nothing) Then

                                SwapControlValue(FindControl(CS_FOOTER_TEXT + iMoveFrom.ToString()), _
                                        FindControl(CS_FOOTER_TEXT + iMoveTo.ToString()))
                            End If
                        End If

                        If bHeader Then
                            If (Not FindControl(CS_HEADER_JUSTIFICATION + iMoveFrom.ToString()) Is Nothing) And _
                                    (Not FindControl(CS_HEADER_JUSTIFICATION + iMoveTo.ToString()) Is Nothing) Then

                                SwapControlValue(FindControl(CS_HEADER_JUSTIFICATION + iMoveFrom.ToString()), _
                                        FindControl(CS_HEADER_JUSTIFICATION + iMoveTo.ToString()))
                            End If
                        ElseIf bFooter Then
                            If (Not FindControl(CS_FOOTER_JUSTIFICATION + iMoveFrom.ToString()) Is Nothing) And _
                                   (Not FindControl(CS_FOOTER_JUSTIFICATION + iMoveTo.ToString()) Is Nothing) Then

                                SwapControlValue(FindControl(CS_FOOTER_JUSTIFICATION + iMoveFrom.ToString()), _
                                        FindControl(CS_FOOTER_JUSTIFICATION + iMoveTo.ToString()))
                            End If
                        End If

                        If bHeader Then
                            If (Not FindControl(CS_HEADER_LARGE + iMoveFrom.ToString()) Is Nothing) And _
                                    (Not FindControl(CS_HEADER_LARGE + iMoveTo.ToString()) Is Nothing) Then

                                SwapControlValue(FindControl(CS_HEADER_LARGE + iMoveFrom.ToString()), _
                                        FindControl(CS_HEADER_LARGE + iMoveTo.ToString()))
                            End If
                        ElseIf bFooter Then
                            If (Not FindControl(CS_FOOTER_LARGE + iMoveFrom.ToString()) Is Nothing) And _
                                   (Not FindControl(CS_FOOTER_LARGE + iMoveTo.ToString()) Is Nothing) Then

                                SwapControlValue(FindControl(CS_FOOTER_LARGE + iMoveFrom.ToString()), _
                                        FindControl(CS_FOOTER_LARGE + iMoveTo.ToString()))
                            End If
                        End If

                        If bHeader Then
                            If (Not FindControl(CS_HEADER_BOLD + iMoveFrom.ToString()) Is Nothing) And _
                                    (Not FindControl(CS_HEADER_BOLD + iMoveTo.ToString()) Is Nothing) Then

                                SwapControlValue(FindControl(CS_HEADER_BOLD + iMoveFrom.ToString()), _
                                        FindControl(CS_HEADER_BOLD + iMoveTo.ToString()))
                            End If
                        ElseIf bFooter Then
                            If (Not FindControl(CS_FOOTER_BOLD + iMoveFrom.ToString()) Is Nothing) And _
                                   (Not FindControl(CS_FOOTER_BOLD + iMoveTo.ToString()) Is Nothing) Then

                                SwapControlValue(FindControl(CS_FOOTER_BOLD + iMoveFrom.ToString()), _
                                        FindControl(CS_FOOTER_BOLD + iMoveTo.ToString()))
                            End If
                        End If

                        If bHeader Then
                            If (Not FindControl(CS_HEADER_WIDE + iMoveFrom.ToString()) Is Nothing) And _
                                    (Not FindControl(CS_HEADER_WIDE + iMoveTo.ToString()) Is Nothing) Then

                                SwapControlValue(FindControl(CS_HEADER_WIDE + iMoveFrom.ToString()), _
                                        FindControl(CS_HEADER_WIDE + iMoveTo.ToString()))
                            End If
                        ElseIf bFooter Then
                            If (Not FindControl(CS_FOOTER_WIDE + iMoveFrom.ToString()) Is Nothing) And _
                                   (Not FindControl(CS_FOOTER_WIDE + iMoveTo.ToString()) Is Nothing) Then

                                SwapControlValue(FindControl(CS_FOOTER_WIDE + iMoveFrom.ToString()), _
                                        FindControl(CS_FOOTER_WIDE + iMoveTo.ToString()))
                            End If
                        End If

                        If bHeader Then
                            If (Not FindControl(CS_HEADER_HIGH + iMoveFrom.ToString()) Is Nothing) And _
                                    (Not FindControl(CS_HEADER_HIGH + iMoveTo.ToString()) Is Nothing) Then

                                SwapControlValue(FindControl(CS_HEADER_HIGH + iMoveFrom.ToString()), _
                                        FindControl(CS_HEADER_HIGH + iMoveTo.ToString()))
                            End If
                        ElseIf bFooter Then
                            If (Not FindControl(CS_FOOTER_HIGH + iMoveFrom.ToString()) Is Nothing) And _
                                   (Not FindControl(CS_FOOTER_HIGH + iMoveTo.ToString()) Is Nothing) Then

                                SwapControlValue(FindControl(CS_FOOTER_HIGH + iMoveFrom.ToString()), _
                                        FindControl(CS_FOOTER_HIGH + iMoveTo.ToString()))
                            End If
                        End If

                        If bHeader Then
                            If (Not FindControl(CS_HEADER_UNDERLINE + iMoveFrom.ToString()) Is Nothing) And _
                                    (Not FindControl(CS_HEADER_UNDERLINE + iMoveTo.ToString()) Is Nothing) Then

                                SwapControlValue(FindControl(CS_HEADER_UNDERLINE + iMoveFrom.ToString()), _
                                        FindControl(CS_HEADER_UNDERLINE + iMoveTo.ToString()))
                            End If
                        ElseIf bFooter Then
                            If (Not FindControl(CS_FOOTER_UNDERLINE + iMoveFrom.ToString()) Is Nothing) And _
                                    (Not FindControl(CS_FOOTER_UNDERLINE + iMoveTo.ToString()) Is Nothing) Then

                                SwapControlValue(FindControl(CS_FOOTER_UNDERLINE + iMoveFrom.ToString()), _
                                        FindControl(CS_FOOTER_UNDERLINE + iMoveTo.ToString()))
                            End If
                        End If

                    End If

                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    ' The next blocks of code deal with "cmdDisplayToPrint" and "cmdPrintToDisplay" button clicks. 
                    ' This is done in Page_Load (after the viewstate load) because values stored in page controls are 
                    ' copied. 
                    ' The logic is based on the ArrayLists _aryFloatRows, aryHeaderDisplay and aryHeaderPrint. 
                    ' These contain information about the controls on the print and display screens. Based on that 
                    ' information, the appropriate control values are copied.
                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                    ' Hard-Code
                    If _sEvent = "cmdDisplayToPrint" Then

                        ' Get count of whacked "HeaderPrint" rows.
                        Dim iWhacked As Integer = 0
                        For Each ixc_sItem As String In _aryFloatRows
                            If ixc_sItem.IndexOf("HeaderPrint") > -1 And ixc_sItem.IndexOf(FLOATING_ROW_WHACK) > -1 Then
                                iWhacked += 1
                            End If
                        Next

                        Dim iDisplayCnt As Integer = 0
                        For Each ixc_sItem As String In _aryDisplay
                            Dim sTmp As String = ixc_sItem
                            sTmp = sTmp.Substring(sTmp.IndexOf("|") + 1)
                            If (Not FindControl(sTmp) Is Nothing) And _
                                    (Not FindControl(sTmp.Replace("TicketDisplay", "TicketPrint")) Is Nothing) Then
                                CopyControlValue(FindControl(sTmp), FindControl(sTmp.Replace("TicketDisplay", "TicketPrint")))
                            End If
                        Next

                        Dim aryHeaderDisplay As ArrayList = CType(Session("aryVals" + HEADER_FLOAT_DISPLAY_POSTFIX), ArrayList)
                        Dim aryHeaderPrint As ArrayList = CType(Session("aryVals" + HEADER_FLOAT_PRINT_POSTFIX), ArrayList)

                        ' Find first "non-whacked aryPrint entry.
                        Dim iPrintStart As Integer = -1
                        For Each ixc_sItem As String In aryHeaderPrint
                            If ixc_sItem.IndexOf(FLOATING_ROW_WHACK) = -1 Then
                                Exit For
                            End If
                            iPrintStart += 1
                        Next

                        For ix As Integer = 0 To aryHeaderDisplay.Count - 1
                            If aryHeaderDisplay(ix).ToString().IndexOf(FLOATING_ROW_WHACK) = -1 Then
                                Dim sTmpDisp As String = HEADER_FLOAT_COMBO_PREFIX_LEFT + HEADER_FLOAT_DISPLAY_POSTFIX + ix.ToString()
                                iPrintStart += 1
                                Dim sTmpPrint As String = HEADER_FLOAT_COMBO_PREFIX_LEFT + HEADER_FLOAT_PRINT_POSTFIX + iPrintStart.ToString()
                                If (Not FindControl(sTmpDisp) Is Nothing) And _
                                          (Not FindControl(sTmpPrint) Is Nothing) Then
                                    CopyControlValue(FindControl(sTmpDisp), FindControl(sTmpPrint))
                                End If

                                sTmpDisp = HEADER_FLOAT_COMBO_PREFIX_RIGHT + HEADER_FLOAT_DISPLAY_POSTFIX + ix.ToString()
                                sTmpPrint = HEADER_FLOAT_COMBO_PREFIX_RIGHT + HEADER_FLOAT_PRINT_POSTFIX + iPrintStart.ToString()
                                If (Not FindControl(sTmpDisp) Is Nothing) And _
                                         (Not FindControl(sTmpPrint) Is Nothing) Then
                                    CopyControlValue(FindControl(sTmpDisp), FindControl(sTmpPrint))
                                End If
                            End If
                        Next

                    End If  ' End: If _sEvent = "cmdDisplayToPrint" Then


                    If _sEvent = "cmdPrintToDisplay" Then

                        ' Get count of whacked "HeaderDisplay" rows.
                        Dim iWhacked As Integer = 0
                        For Each ixc_sItem As String In _aryFloatRows
                            If ixc_sItem.IndexOf("HeaderDisplay") > -1 And ixc_sItem.IndexOf(FLOATING_ROW_WHACK) > -1 Then
                                iWhacked += 1
                            End If
                        Next

                        Dim iPrintCnt As Integer = 0
                        For Each ixc_sItem As String In _aryPrint
                            Dim sTmp As String = ixc_sItem
                            sTmp = sTmp.Substring(sTmp.IndexOf("|") + 1)

                            If (Not FindControl(sTmp) Is Nothing) And _
                                    (Not FindControl(sTmp.Replace("TicketPrint", "TicketDisplay")) Is Nothing) Then
                                CopyControlValue(FindControl(sTmp), FindControl(sTmp.Replace("TicketPrint", "TicketDisplay")))
                            End If
                        Next

                        Dim aryHeaderDisplay As ArrayList = CType(Session("aryVals" + HEADER_FLOAT_DISPLAY_POSTFIX), ArrayList)
                        Dim aryHeaderPrint As ArrayList = CType(Session("aryVals" + HEADER_FLOAT_PRINT_POSTFIX), ArrayList)

                        ' Find first "non-whacked aryPrint entry.
                        Dim iDisplayStart As Integer = -1
                        For Each ixc_sItem As String In aryHeaderDisplay
                            If ixc_sItem.IndexOf(FLOATING_ROW_WHACK) = -1 Then
                                Exit For
                            End If
                            iDisplayStart += 1
                        Next

                        For ix As Integer = 0 To aryHeaderPrint.Count - 1
                            If aryHeaderPrint(ix).ToString().IndexOf(FLOATING_ROW_WHACK) = -1 Then
                                Dim sTmpPrint As String = HEADER_FLOAT_COMBO_PREFIX_LEFT + HEADER_FLOAT_PRINT_POSTFIX + ix.ToString()
                                iDisplayStart += 1
                                Dim sTmpDisp As String = HEADER_FLOAT_COMBO_PREFIX_LEFT + HEADER_FLOAT_DISPLAY_POSTFIX + iDisplayStart.ToString()
                                If (Not FindControl(sTmpPrint) Is Nothing) And _
                                          (Not FindControl(sTmpDisp) Is Nothing) Then
                                    CopyControlValue(FindControl(sTmpPrint), FindControl(sTmpDisp))
                                End If

                                sTmpPrint = HEADER_FLOAT_COMBO_PREFIX_RIGHT + HEADER_FLOAT_PRINT_POSTFIX + ix.ToString()
                                sTmpDisp = HEADER_FLOAT_COMBO_PREFIX_RIGHT + HEADER_FLOAT_DISPLAY_POSTFIX + iDisplayStart.ToString()
                                If (Not FindControl(sTmpPrint) Is Nothing) And _
                                         (Not FindControl(sTmpDisp) Is Nothing) Then
                                    CopyControlValue(FindControl(sTmpPrint), FindControl(sTmpDisp))
                                End If
                            End If
                        Next

                    End If  ' End: If _sEvent = "cmdPrintToDisplay" Then

                End If  ' End: If Not IsPostBack Then

            Catch ex As Exception
                HandleError(ex)
            End Try

        End Sub

        Protected Sub LoadControls()

            Try
                Dim bShowControls As Boolean = True

                ' Get the event that triggered this post.
                If Not Request("__EVENTTARGET") Is Nothing Then
                    _sEvent = Request("__EVENTTARGET").ToString()
                End If

                If Not LoadDataSets() Then
                    Exit Sub
                End If

                Dim aryFloatTables As New ArrayList

                divCopyRegister.Style.Add("display", "none")  ' This may be reset in Page_Load.
                If _sEvent = "cmdCopyRegister" Then
                    bShowControls = False
                End If

                If Not IsPostBack Then
                    _bInitialLoad = True  ' This is the initial page load, set _bInitialLoad to true (it's declared as false).
                    Session(SESSION_FLOAT_ROWS) = ""  ' Wipe out varible in case page is being reloaded.
                End If

                ' Get the list of added or deleted floating rows.
                If Not Session(SESSION_FLOAT_ROWS) Is Nothing Then
                    If Not Session(SESSION_FLOAT_ROWS).ToString() = "" Then
                        _aryFloatRows = CType(Session(SESSION_FLOAT_ROWS), ArrayList)
                    End If
                End If

                ' If the event that triggered this post is a "Delete Float," add the delete flag to that proper entry.
                If SafeSubString(_sEvent, 0, FLOATING_DELETE_PREFIX.Length) = FLOATING_DELETE_PREFIX Then
                    Dim sTmp As String = SafeSubString(_sEvent, FLOATING_DELETE_PREFIX.Length) + "|"
                    For ix As Integer = 0 To _aryFloatRows.Count - 1
                        If SafeSubString(_aryFloatRows(ix), 0, sTmp.Length) = sTmp Then
                            _aryFloatRows(ix) += FLOATING_ROW_WHACK
                            Exit For
                        End If
                    Next
                End If

                ' If the event that triggered this post is an AddNew, add a row to _aryFloatRows. 
                If SafeSubString(_sEvent, 0, FLOATING_ADDNEW_PREFIX.Length) = FLOATING_ADDNEW_PREFIX Then
                    Dim sTmp As String = SafeSubString(_sEvent, FLOATING_ADDNEW_PREFIX.Length) + "|"

                    ' Get the count of entries with this postfix to determine the row number to be added.
                    Dim iCnt As Integer = 1
                    For Each ixc_sItem As String In _aryFloatRows
                        If SafeSubString(ixc_sItem, 0, sTmp.Length) = sTmp Then
                            iCnt += 1
                        End If
                    Next

                    sTmp += iCnt.ToString() + "|" ' A value would go here, but since this is an add, there isn't one.
                    _aryFloatRows.Add(sTmp)
                End If

                ' If the event that triggered this post is pageload (_sEvent = ""), set _sCurrentTab to the default.
                ' If a tab button triggered this post, set _sCurrentTab to its proper value.
                ' Otherwise, get it from Session.
                If _sEvent = "" Then
                    _sCurrentTab = DEFAULT_TAB
                ElseIf SafeSubString(_sEvent, 0, MENU_BUTTON_PREFIX.Length) = MENU_BUTTON_PREFIX Then
                    _sCurrentTab = SafeSubString(_sEvent, MENU_BUTTON_PREFIX.Length)
                Else
                    _sCurrentTab = Session(SESSION_CURRENT_TAB)
                End If
                Session(SESSION_CURRENT_TAB) = _sCurrentTab

                ' Hard-code: set some output sizes.
                Dim trHeader As New TableRow
                trHeader.Height = New Unit(DEFAULT_ROW_HEIGHT * 1.2)

                Dim tdHeader As New TableCell
                tdHeader.Font.Size = FontUnit.Point(12)
                tdHeader.Text = _sCurrentTab
                tdHeader.ColumnSpan = 2
                tdHeader.BackColor = Color.LightBlue
                tdHeader.HorizontalAlign = HorizontalAlign.Center
                trHeader.Cells.Add(tdHeader)
                If Not bShowControls Then
                    trHeader.Visible = False
                    tdHeader.Visible = False
                End If
                tblMain.Rows.Add(trHeader)


                Dim lTagIdx As Long = 0
                For Each ixc_drowConfig As DataRow In _dsConfig.Tables(0).Rows

                    ' Only use rows that have a value in control type
                    If ixc_drowConfig(CONFIG_COL_CONTROL_TYPE).ToString() <> "" Then

                        Dim tr As New TableRow

                        ' If curent tab (most recent menu button clicked) matches the config file's
                        ' view node, make the row display.
                        Dim lRowHeight As Long = 0
                        Dim bRowVisible As Boolean = False
                        If ixc_drowConfig(CONFIG_COL_VIEW).ToString() = _sCurrentTab And bShowControls Then
                            lRowHeight = DEFAULT_ROW_HEIGHT
                            bRowVisible = True
                        End If
                        tr.Visible = bRowVisible
                        tr.Height = New Unit(lRowHeight)

                        ' If a button current view type hasn't been added, do that now.
                        Dim cmdMenu As LinkButton
                        cmdMenu = FindControl(MENU_BUTTON_PREFIX + ixc_drowConfig(CONFIG_COL_VIEW).ToString())
                        If cmdMenu Is Nothing Then
                            cmdMenu = New LinkButton
                            cmdMenu.ID = MENU_BUTTON_PREFIX + ixc_drowConfig(CONFIG_COL_VIEW).ToString()
                            'cmd.CssClass = "Button"
                            cmdMenu.Text = PadTextNBSP(ixc_drowConfig(CONFIG_COL_VIEW).ToString(), 3)
                            tdMenu.Controls.Add(cmdMenu)

                            ' Hard-code
                            ' Add copy display to print and copy print to display buttons.
                            If ixc_drowConfig(CONFIG_COL_VIEW).ToString() = "Display" Or ixc_drowConfig(CONFIG_COL_VIEW).ToString() = "Print" Then
                                Dim cmdCopy As New LinkButton

                                Dim sMsg As String = ""
                                If ixc_drowConfig(CONFIG_COL_VIEW).ToString() = "Display" Then
                                    cmdCopy.ID = "cmdDisplayToPrint"
                                    cmdCopy.Text = PadTextNBSP("Copy Display To Print", 4)
                                    sMsg = "Are you sure you want to copy all display settings to print settings?"
                                Else
                                    cmdCopy.ID = "cmdPrintToDisplay"
                                    cmdCopy.Text = PadTextNBSP("Copy Print To Display", 2)
                                    sMsg = "Are you sure you want to copy all print settings to display settings?"
                                End If
                                cmdCopy.Attributes.Add("onclick", "return confirmClick('" + sMsg + "');")

                                Dim trButton As New TableRow
                                Dim tdButton As New TableCell

                                tdButton.Controls.Add(cmdCopy)
                                tdButton.ColumnSpan = 2
                                tdButton.HorizontalAlign = HorizontalAlign.Center
                                trButton.Cells.Add(tdButton)

                                trButton.Height = New Unit(DEFAULT_ROW_HEIGHT * 2)
                                trButton.VerticalAlign = VerticalAlign.Middle

                                ' Hide if this isn't in the current tab.
                                If _sCurrentTab <> ixc_drowConfig(CONFIG_COL_VIEW).ToString() Then
                                    trButton.Visible = False
                                End If

                                tblMain.Rows.Add(trButton)
                            End If
                            ' End Hard-code
                        End If

                        Dim sDataValue As String = ""

                        Dim objKeys(1) As Object
                        objKeys(0) = ixc_drowConfig(CONFIG_COL_OPT).ToString()
                        objKeys(1) = ixc_drowConfig(CONFIG_COL_TYPE).ToString()
                        Dim drowIn As DataRow = _dsIn.Tables(0).Rows.Find(objKeys)
                        If Not drowIn Is Nothing Then
                            sDataValue = drowIn(IN_COL_VALUE).ToString()
                        End If

                        Dim td_1 As New TableCell
                        td_1.VerticalAlign = VerticalAlign.Middle

                        Select Case ixc_drowConfig(CONFIG_COL_CONTROL_TYPE).ToString()

                            Case "label"
                                tr.Cells.Add(td_1)
                                td_1.Text = ixc_drowConfig(CONFIG_COL_DISPLAY).ToString() + "&nbsp;&nbsp;"
                                td_1.Wrap() = True
                                td_1.ColumnSpan() = 2

                                'Dim td_2 As New TableCell
                                ''Dim txtBox As New TextBox

                                'tr.Cells.Add(td_2)
                                tblMain.Rows.Add(tr)


                            Case "textbox", "password"
                                tr.Cells.Add(td_1)
                                td_1.Text = ixc_drowConfig(CONFIG_COL_DISPLAY).ToString() + "&nbsp;&nbsp;"

                                Dim td_2 As New TableCell
                                Dim txtBox As New TextBox

                                txtBox.ID = ixc_drowConfig(CONFIG_COL_OPT).ToString() + ixc_drowConfig(CONFIG_COL_TYPE).ToString()
                                lTagIdx += 1
                                txtBox.TabIndex = lTagIdx

                                If ixc_drowConfig(CONFIG_COL_CONTROL_TYPE).ToString() = "textbox" Then
                                    txtBox.TextMode = TextBoxMode.SingleLine
                                    txtBox.Text = sDataValue
                                    If txtBox.ID.StartsWith("TipMargin") Then
                                        If txtBox.Text.Length > 0 Then
                                            txtBox.Text = (txtBox.Text * 100).ToString
                                        End If
                                    End If
                                Else
                                    txtBox.TextMode = TextBoxMode.Password
                                    txtBox.Text = sDataValue
                                    'txtBox.Attributes.Add("value", sDataValue)

                                    _aryPassword.Add(txtBox.ID)

                                    'Dim sDecoded = Decode(sDataValue)
                                    'txtBox.Attributes.Add("value", sDecoded)
                                End If

                                txtBox.BorderColor = Color.Black
                                txtBox.BorderStyle = BorderStyle.Ridge
                                txtBox.BorderWidth = New Unit(1)

                                td_2.Controls.Add(txtBox)
                                tr.Cells.Add(td_2)
                                tblMain.Rows.Add(tr)

                                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                                ' Hard-Code
                                ' If this is a "TicketPrint" or "TicketDisplay" type add it to 
                                ' an ArrayList. This is necessary for a copy display or print
                                ' buttong click.
                                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                                If ixc_drowConfig(CONFIG_COL_TYPE).ToString() = "TicketPrint" Then
                                    _aryPrint.Add("textbox|" + txtBox.ID)
                                ElseIf ixc_drowConfig(CONFIG_COL_TYPE).ToString() = "TicketDisplay" Then
                                    _aryDisplay.Add("textbox|" + txtBox.ID)
                                End If

                            Case "checkbox"
                                tr.Cells.Add(td_1)
                                td_1.Text = ixc_drowConfig(CONFIG_COL_DISPLAY).ToString() + "&nbsp;&nbsp;"

                                Dim td_2 As New TableCell
                                Dim chkBox As New CheckBox

                                chkBox.ID = ixc_drowConfig(CONFIG_COL_OPT).ToString() + ixc_drowConfig(CONFIG_COL_TYPE).ToString()
                                If sDataValue <> "" Then
                                    chkBox.Checked = Convert.ToBoolean(sDataValue)
                                    lTagIdx += 1
                                    chkBox.TabIndex = lTagIdx
                                End If

                                td_2.Controls.Add(chkBox)
                                tr.Cells.Add(td_2)
                                tblMain.Rows.Add(tr)

                                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                                ' Hard-Code
                                ' If this is a "TicketPrint" or "TicketDisplay" type add it to 
                                ' an ArrayList. This is necessary for a copy display or print
                                ' buttong click.
                                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                                If ixc_drowConfig(CONFIG_COL_TYPE).ToString() = "TicketPrint" Then
                                    _aryPrint.Add("checkbox|" + chkBox.ID)
                                ElseIf ixc_drowConfig(CONFIG_COL_TYPE).ToString() = "TicketDisplay" Then
                                    _aryDisplay.Add("checkbox|" + chkBox.ID)
                                End If

                            Case "combo"
                                tr.Cells.Add(td_1)
                                td_1.Text = ixc_drowConfig(CONFIG_COL_DISPLAY).ToString() + "&nbsp;&nbsp;"

                                Dim td_2 As New TableCell
                                Dim cboBox As New DropDownList

                                cboBox.ID = ixc_drowConfig(CONFIG_COL_OPT).ToString() + ixc_drowConfig(CONFIG_COL_TYPE).ToString()
                                lTagIdx += 1
                                cboBox.TabIndex = lTagIdx

                                ' Get current value.
                                Dim sCurrent As String = sDataValue

                                Dim sTime As String = ixc_drowConfig(CONFIG_COL_OPT).ToString() + "|" + ixc_drowConfig(CONFIG_COL_TYPE).ToString()
                                ' Hard-code
                                If sTime = "AutoprintTime|Printing" Then
                                    If sDataValue.Length = 4 Then
                                        sDataValue = sDataValue.Substring(0, 2) + ":" + sDataValue.Substring(2, 2)
                                        Dim x = 0
                                    End If
                                End If

                                Dim comboValues As String = ixc_drowConfig(CONFIG_COL_COMBO_VALUES).ToString.Trim, sSelected As String = ""
                                If comboValues.StartsWith("%") And comboValues.EndsWith("%") Then
                                    ' Get combo values from database
                                    Using con As New SqlConnection(New DataAccess().UnMaskString(WebConfigurationManager.ConnectionStrings("Production").ConnectionString, 1, 2, 3, 4, 5))
                                        con.Open()
                                        Using cmd As New SqlCommand("SELECT [Value] FROM Options_ComboValues WHERE [Key] = @KEY ORDER BY [SortKey]", con)
                                            cmd.Parameters.AddWithValue("@KEY", comboValues.Replace("%", ""))
                                            Using rdr As SqlDataReader = cmd.ExecuteReader
                                                While rdr.Read
                                                    If rdr("Value").ToString.Trim.ToUpper = sDataValue.Trim.ToUpper Then
                                                        sSelected = sDataValue
                                                    End If
                                                    cboBox.Items.Add(ComboItem(rdr("Value").ToString))
                                                End While
                                            End Using
                                        End Using
                                    End Using
                                Else
                                    ' Get combo values from config file.
                                    Dim sComboVals() As String = Split(ixc_drowConfig(CONFIG_COL_COMBO_VALUES).ToString(), "|")
                                    For ix As Integer = 0 To sComboVals.GetUpperBound(0)
                                        If ComboVal(sComboVals(ix)).Trim.ToUpper = sDataValue.Trim.ToUpper Then
                                            sSelected = sDataValue
                                        End If
                                        cboBox.Items.Add(ComboItem(sComboVals(ix)))
                                    Next
                                End If

                                If sSelected <> "" Then
                                    cboBox.SelectedValue = sSelected
                                End If

                                td_2.Controls.Add(cboBox)
                                tr.Cells.Add(td_2)
                                tblMain.Rows.Add(tr)

                                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                                ' Hard-Code
                                ' If this is a "TicketPrint" or "TicketDisplay" type add it to 
                                ' an ArrayList. This is necessary for a copy display or print
                                ' buttong click.
                                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                                If ixc_drowConfig(CONFIG_COL_TYPE).ToString() = "TicketPrint" Then
                                    _aryPrint.Add("combo|" + cboBox.ID)
                                ElseIf ixc_drowConfig(CONFIG_COL_TYPE).ToString() = "TicketDisplay" Then
                                    _aryDisplay.Add("combo|" + cboBox.ID)
                                End If

                            Case "float"
                                tr.Cells.Add(td_1)
                                td_1.Text = ixc_drowConfig(CONFIG_COL_GROUP_DISPLAY).ToString() + "&nbsp;&nbsp;"

                                Dim sPostFix As String = ixc_drowConfig(CONFIG_COL_CONTROL_POSTFIX).ToString()

                                ' If a floating table hasn't been created for this "control prefix," create one.
                                ' Also, create a link button to activate it.
                                If FindControl(FLOATING_TABLE_PREFIX + sPostFix) Is Nothing Then
                                    Dim td_2 As New TableCell

                                    Dim tblFloat As Table = CreateFloatingTable(sPostFix)
                                    aryFloatTables.Add(sPostFix + "|" + ixc_drowConfig(CONFIG_COL_DISPLAY).ToString() + _
                                      "|" + ixc_drowConfig(CONFIG_COL_FLOAT_TYPE).ToString() + "|" + _
                                      ixc_drowConfig(CONFIG_COL_COMBO_VALUES).ToString())

                                    Dim cmdFloat As New LinkButton
                                    cmdFloat.ID = FLOATING_BUTTON_PREFIX + sPostFix
                                    'cmdFloat.CssClass = "Button"
                                    cmdFloat.Text = PadTextNBSP(ixc_drowConfig(CONFIG_COL_GROUP_DISPLAY).ToString(), 3)

                                    ' If an "Add New" button click, a "Delete" button click, or a "Floating" button click
                                    ' triggered this post, show the table and hide the floating button.
                                    ' Otherwise, hide the table and show the button.
                                    If _sEvent = FLOATING_ADDNEW_PREFIX + sPostFix Or _
                                      SafeSubString(_sEvent, 0, Len(FLOATING_DELETE_PREFIX + sPostFix)) = FLOATING_DELETE_PREFIX + sPostFix Or _
                                      _sEvent = FLOATING_BUTTON_PREFIX + sPostFix Then
                                        tblFloat.Visible = True
                                        cmdFloat.Visible = False
                                    Else
                                        tblFloat.Visible = False
                                        cmdFloat.Visible = True
                                    End If

                                    td_2.Controls.Add(cmdFloat)
                                    td_2.Controls.Add(tblFloat)
                                    tr.Cells.Add(td_2)
                                    tblMain.Rows.Add(tr)
                                End If


                            Case "header_float"                     ' Print/Display header floating div

                                tr.Cells.Add(td_1)
                                td_1.Text = ixc_drowConfig(CONFIG_COL_GROUP_DISPLAY).ToString() + "&nbsp;&nbsp;"

                                Dim sPostFix = ixc_drowConfig(CONFIG_COL_CONTROL_POSTFIX).ToString()

                                Dim div As HtmlGenericControl
                                Dim tbl As Table


                                If ixc_drowConfig(CONFIG_COL_VIEW).ToString() = "Display" Then
                                    div = divDisplayHeader
                                    tbl = tblDisplayHeader

                                    If _sEvent = HEADER_FLOAT_BUTTON_PREFIX + HEADER_FLOAT_DISPLAY_POSTFIX Then
                                        _bShowDisplayHeader = True
                                    ElseIf SafeSubString(_sEvent, 0, HEADER_FLOAT_ADDNEW_PREFIX.Length) = HEADER_FLOAT_ADDNEW_PREFIX Then
                                        _bShowDisplayHeader = True
                                    ElseIf SafeSubString(_sEvent, 0, HEADER_FLOAT_DELETE_PREFIX.Length) = HEADER_FLOAT_DELETE_PREFIX Then
                                        _bShowDisplayHeader = True
                                    ElseIf SafeSubString(_sEvent, 0, HEADER_FLOAT_DELETE_PREFIX.Length) = HEADER_FLOAT_DELETE_PREFIX Then
                                        _bShowDisplayHeader = True
                                    ElseIf SafeSubString(_sEvent, 0, HEADER_FLOAT_MOVEUP_PREFIX.Length) = HEADER_FLOAT_MOVEUP_PREFIX Then
                                        _bShowDisplayHeader = True
                                    ElseIf SafeSubString(_sEvent, 0, HEADER_FLOAT_MOVEDOWN_PREFIX.Length) = HEADER_FLOAT_MOVEDOWN_PREFIX Then
                                        _bShowDisplayHeader = True
                                    End If
                                ElseIf ixc_drowConfig(CONFIG_COL_VIEW).ToString() = "Print" Then
                                    div = divPrintHeader
                                    tbl = tblPrintHeader

                                    If _sEvent = HEADER_FLOAT_BUTTON_PREFIX + HEADER_FLOAT_PRINT_POSTFIX Then
                                        _bShowPrintHeader = True
                                    ElseIf SafeSubString(_sEvent, 0, HEADER_FLOAT_ADDNEW_PREFIX.Length) = HEADER_FLOAT_ADDNEW_PREFIX Then
                                        _bShowPrintHeader = True
                                    ElseIf SafeSubString(_sEvent, 0, HEADER_FLOAT_DELETE_PREFIX.Length) = HEADER_FLOAT_DELETE_PREFIX Then
                                        _bShowPrintHeader = True
                                    ElseIf SafeSubString(_sEvent, 0, HEADER_FLOAT_MOVEUP_PREFIX.Length) = HEADER_FLOAT_MOVEUP_PREFIX Then
                                        _bShowPrintHeader = True
                                    ElseIf SafeSubString(_sEvent, 0, HEADER_FLOAT_MOVEDOWN_PREFIX.Length) = HEADER_FLOAT_MOVEDOWN_PREFIX Then
                                        _bShowPrintHeader = True
                                    End If
                                End If

                                Dim td_2 As New TableCell

                                Dim cmdFloat As New LinkButton
                                cmdFloat.ID = HEADER_FLOAT_BUTTON_PREFIX + ixc_drowConfig(CONFIG_COL_CONTROL_POSTFIX).ToString()
                                cmdFloat.Text = PadTextNBSP(ixc_drowConfig(CONFIG_COL_GROUP_DISPLAY).ToString(), 3)

                                div.Style.Add("display", "none")
                                cmdFloat.Visible = True

                                td_2.Controls.Add(cmdFloat)
                                td_2.Controls.Add(div)

                                tr.Cells.Add(td_2)
                                tblMain.Rows.Add(tr)



                                Dim drows() As DataRow = _dsIn.Tables(0).Select("type = '" + ixc_drowConfig(CONFIG_COL_TYPE).ToString() + "' and opt like 'TicketHeader%'")

                                If drows.Length < 1 Then
                                    drows = _dsIn.Tables(0).Select("type = '" + ixc_drowConfig(CONFIG_COL_TYPE).ToString() + "' and opt like 'Header%'")
                                End If

                                Dim aryVals As New ArrayList

                                Dim sFloatTypes() As String = Split(ixc_drowConfig(CONFIG_COL_FLOAT_TYPE).ToString(), FLOATING_DISPLAY_DELIM)
                                Dim sComboValues() As String = Split(ixc_drowConfig(CONFIG_COL_COMBO_VALUES).ToString(), FLOATING_DISPLAY_DELIM)
                                Dim sOpts() As String = Split(ixc_drowConfig(CONFIG_COL_OPT).ToString(), FLOATING_DISPLAY_DELIM)

                                If _bInitialLoad Then
                                    'Dim sLeft(9999) As String
                                    'Dim sRight(9999) As String
                                    Dim bFound As Boolean = True
                                    Dim iCnt As Integer = 0


                                    Do While bFound
                                        bFound = False
                                        iCnt += 1

                                        Dim sTmp As String = ""
                                        For innerx As Integer = 0 To sOpts.GetUpperBound(0)
                                            Dim objKeys3(1) As Object
                                            objKeys3(0) = sOpts(innerx).Replace(FLOATING_NUM_PLACEHOLDER, iCnt.ToString())
                                            objKeys3(1) = ixc_drowConfig(CONFIG_COL_TYPE).ToString()
                                            Dim drowIn3 As DataRow = _dsIn.Tables(0).Rows.Find(objKeys3)
                                            If Not drowIn3 Is Nothing Then
                                                bFound = True
                                                sTmp += drowIn3(IN_COL_VALUE.ToString()) + FLOATING_DISPLAY_DELIM
                                            Else
                                                objKeys3(0) = objKeys3(0).Replace("Ticket", "")
                                                drowIn3 = _dsIn.Tables(0).Rows.Find(objKeys3)
                                                If Not drowIn3 Is Nothing Then
                                                    bFound = True
                                                    sTmp += drowIn3(IN_COL_VALUE.ToString()) + FLOATING_DISPLAY_DELIM
                                                Else
                                                    sTmp += FLOATING_DISPLAY_DELIM
                                                End If
                                            End If
                                        Next
                                        If bFound Then
                                            sTmp = sTmp.Substring(0, sTmp.Length - FLOATING_DISPLAY_DELIM.Length)
                                            aryVals.Add(sTmp)
                                        End If


                                        'Next
                                    Loop
                                    Session("aryVals" + sPostFix) = aryVals
                                Else
                                    aryVals = CType(Session("aryVals" + sPostFix), ArrayList)

                                    ' If the addnew button was clicked, add a row.
                                    If SafeSubString(_sEvent, 0, HEADER_FLOAT_ADDNEW_PREFIX.Length) = HEADER_FLOAT_ADDNEW_PREFIX Then
                                        If _sEvent.IndexOf(sPostFix) <> -1 Then
                                            aryVals.Add(FLOATING_DISPLAY_DELIM)
                                        End If
                                    End If

                                    ' If the delete button was clicked, add a delete flag to the corresponding row.
                                    If SafeSubString(_sEvent, 0, HEADER_FLOAT_DELETE_PREFIX.Length) = HEADER_FLOAT_DELETE_PREFIX Then
                                        ' Get the row number.
                                        Dim sTmp As String = ""
                                        If _sEvent.IndexOf(sPostFix) <> -1 Then
                                            sTmp = _sEvent.Substring(HEADER_FLOAT_DELETE_PREFIX.Length + sPostFix.Length)

                                            If IsNumeric(sTmp) Then
                                                aryVals(Convert.ToInt32(sTmp)) += FLOATING_ROW_WHACK
                                            End If
                                        End If
                                    End If

                                    ' If the copy display to print button was clicked, add a "delete" the existing rows and 
                                    ' add the print rows.
                                    ' Hard-code - cmdDisplayToPrint.
                                    If _sEvent = "cmdDisplayToPrint" Then
                                        If sPostFix = HEADER_FLOAT_PRINT_POSTFIX Then
                                            For ix As Integer = 0 To aryVals.Count - 1
                                                If aryVals(ix).ToString().IndexOf(FLOATING_ROW_WHACK) = -1 Then
                                                    aryVals(ix) += FLOATING_ROW_WHACK
                                                End If
                                            Next

                                            Dim aryDisplayVals As ArrayList = CType(Session("aryVals" + HEADER_FLOAT_DISPLAY_POSTFIX), ArrayList)
                                            For Each ixc_sItem As String In aryDisplayVals
                                                If ixc_sItem.IndexOf(FLOATING_ROW_WHACK) = -1 Then
                                                    aryVals.Add(ixc_sItem)
                                                End If
                                            Next
                                        End If
                                    End If

                                    ' If the copy display to print button was clicked, add a "delete" the existing rows and 
                                    ' add the print rows.
                                    ' Hard-code - cmdPrintToDisplay.
                                    If _sEvent = "cmdPrintToDisplay" Then
                                        If sPostFix = HEADER_FLOAT_DISPLAY_POSTFIX Then
                                            For ix As Integer = 0 To aryVals.Count - 1
                                                If aryVals(ix).ToString().IndexOf(FLOATING_ROW_WHACK) = -1 Then
                                                    aryVals(ix) += FLOATING_ROW_WHACK
                                                End If
                                            Next

                                            Dim aryPrintVals As ArrayList = CType(Session("aryVals" + HEADER_FLOAT_PRINT_POSTFIX), ArrayList)
                                            For Each ixc_sItem As String In aryPrintVals
                                                If ixc_sItem.IndexOf(FLOATING_ROW_WHACK) = -1 Then
                                                    aryVals.Add(ixc_sItem)
                                                End If
                                            Next
                                        End If
                                    End If

                                    Session("aryVals" + sPostFix) = aryVals
                                End If

                                ' Get first and last "non-whacked" rows
                                Dim iFirstGood As Integer = -1
                                Dim iLastGood As Integer = -1
                                For ix As Integer = 0 To aryVals.Count - 1
                                    If aryVals(ix).ToString().IndexOf(FLOATING_ROW_WHACK) = -1 Then
                                        If iFirstGood = -1 Then
                                            iFirstGood = ix
                                        End If
                                        iLastGood = ix
                                    End If
                                Next

                                'Dim sComboVals() As String = Split(ixc_drowConfig(CONFIG_COL_COMBO_VALUES).ToString(), "|")
                                Dim sComboVals() As String = Split(sComboValues(0).ToString(), "|")
                                Dim iLableCnt As Integer = 0
                                For ix As Integer = 0 To aryVals.Count - 1
                                    If aryVals(ix).ToString().IndexOf(FLOATING_ROW_WHACK) = -1 Then
                                        Dim ctrlValues() As String = Split(aryVals(ix), FLOATING_DISPLAY_DELIM)
                                        Dim trDiv As New TableRow

                                        iLableCnt += 1

                                        Dim tdDiv_0 As New TableCell
                                        tdDiv_0.Text = FLOATING_ROW_OUTPUT
                                        tdDiv_0.Width = New Unit(0)
                                        tdDiv_0.Visible = False

                                        Dim tdDiv_1 As New TableCell
                                        tdDiv_1.Text = " " + iLableCnt.ToString() + "&nbsp;&nbsp;" 'Header Left

                                        Dim tdDiv_2 As New TableCell
                                        Dim cboLeft As New DropDownList

                                        Dim sSelected As String = ""
                                        cboLeft.Items.Add("")
                                        For ix2 As Integer = 0 To sComboVals.GetUpperBound(0)
                                            Dim sTmp As String = sComboVals(ix2)
                                            If sTmp.IndexOf(EXCLUSIVE_FLAG) > -1 Then
                                                sTmp = sTmp.Substring(0, sTmp.IndexOf(EXCLUSIVE_FLAG))
                                            End If
                                            If sTmp = ctrlValues(0).ToString() Then
                                                sSelected = sTmp
                                            End If
                                            cboLeft.ID = HEADER_FLOAT_COMBO_PREFIX_LEFT + sPostFix + ix.ToString()
                                            cboLeft.Items.Add(ComboItem(sTmp))
                                        Next
                                        If sSelected <> "" Then
                                            cboLeft.SelectedValue = sSelected
                                        End If
                                        tdDiv_2.Controls.Add(cboLeft)

                                        Dim tdDiv_3 As New TableCell
                                        tdDiv_3.Text = "" ' Header Right " + iLableCnt.ToString() + "&nbsp;&nbsp;

                                        Dim tdDiv_4 As New TableCell
                                        Dim cboRight As New DropDownList

                                        sSelected = ""
                                        cboRight.Items.Add("")
                                        For ix2 As Integer = 0 To sComboVals.GetUpperBound(0)
                                            Dim sTmp As String = sComboVals(ix2)
                                            If sTmp.IndexOf(EXCLUSIVE_FLAG) > -1 Then
                                                sTmp = sTmp.Substring(0, sTmp.IndexOf(EXCLUSIVE_FLAG))
                                            End If
                                            If sTmp = ctrlValues(1).ToString() Then
                                                sSelected = sTmp
                                            End If
                                            cboRight.ID = HEADER_FLOAT_COMBO_PREFIX_RIGHT + sPostFix + ix.ToString()
                                            cboRight.Items.Add(ComboItem(sTmp))
                                        Next
                                        If sSelected <> "" Then
                                            cboRight.SelectedValue = sSelected
                                        End If
                                        tdDiv_4.Controls.Add(cboRight)

                                        trDiv.Cells.Add(tdDiv_0)
                                        trDiv.Cells.Add(tdDiv_1)
                                        trDiv.Cells.Add(tdDiv_2)
                                        trDiv.Cells.Add(tdDiv_3)
                                        trDiv.Cells.Add(tdDiv_4)

                                        For ixInner As Integer = 2 To sFloatTypes.GetUpperBound(0)
                                            Dim td As New TableCell
                                            Dim sID As String = ""
                                            If ixc_drowConfig(CONFIG_COL_VIEW).ToString() = "Display" And ixInner > 2 Then
                                                td.Visible = False
                                            End If

                                            If ixInner = HEADER_FLOAT_TTEXCLUDE_COL Then sID = HEADER_FLOAT_TTEXCLUDE + sPostFix + ix.ToString()
                                            If ixInner = HEADER_FLOAT_JUSTIFICATION_COL Then sID = HEADER_FLOAT_JUSTIFICATION + sPostFix + ix.ToString()
                                            If ixInner = HEADER_FLOAT_LARGE_COL Then sID = HEADER_FLOAT_LARGE + sPostFix + ix.ToString()
                                            If ixInner = HEADER_FLOAT_BOLD_COL Then sID = HEADER_FLOAT_BOLD + sPostFix + ix.ToString()
                                            If ixInner = HEADER_FLOAT_WIDE_COL Then sID = HEADER_FLOAT_WIDE + sPostFix + ix.ToString()
                                            If ixInner = HEADER_FLOAT_HIGH_COL Then sID = HEADER_FLOAT_HIGH + sPostFix + ix.ToString()
                                            If ixInner = HEADER_FLOAT_UNDERLINE_COL Then sID = HEADER_FLOAT_UNDERLINE + sPostFix + ix.ToString()
                                            If ixInner = HEADER_FLOAT_COLOR_COL Then sID = HEADER_FLOAT_COLOR + sPostFix + ix.ToString()

                                            Select Case sFloatTypes(ixInner)

                                                Case "textbox"
                                                    Dim txtBox As New TextBox
                                                    txtBox.ID = sID

                                                    txtBox.Text = ""
                                                    If ixInner <= ctrlValues.GetUpperBound(0) Then
                                                        txtBox.Text = ctrlValues(ixInner)
                                                    End If

                                                    txtBox.Width = New Unit(120)
                                                    td.Controls.Add(txtBox)

                                                Case "checkbox"
                                                    Dim chkBox As New CheckBox
                                                    chkBox.ID = sID

                                                    chkBox.Checked = False
                                                    If ixInner <= ctrlValues.GetUpperBound(0) Then
                                                        If ctrlValues(ixInner).ToLower() = "true" Then
                                                            chkBox.Checked = True
                                                        End If
                                                    End If

                                                    td.HorizontalAlign = HorizontalAlign.Center
                                                    td.Controls.Add(chkBox)

                                                Case "combo"
                                                    Dim cboBox As New DropDownList
                                                    Dim correctedIxInner As Integer = ixInner - 1

                                                    cboBox.ID = sID

                                                    Dim sUseVal As String = ""
                                                    If ixInner <= ctrlValues.GetUpperBound(0) Then
                                                        sUseVal = ctrlValues(ixInner)
                                                    End If

                                                    cboBox.Items.Add("")
                                                    Dim sSelected2 As String = ""
                                                    If correctedIxInner <= sComboValues.GetUpperBound(0) Then
                                                        Dim sComboLoad() As String = Split(sComboValues(correctedIxInner), "|")
                                                        For ixCombo As Integer = 0 To sComboLoad.GetUpperBound(0)
                                                            cboBox.Items.Add(ComboItem(sComboLoad(ixCombo)))

                                                            If sSelected2 = "" And sUseVal = ComboVal(sComboLoad(ixCombo)) Then
                                                                sSelected2 = sUseVal
                                                            End If
                                                        Next
                                                    End If
                                                    cboBox.SelectedValue = sSelected2
                                                    td.HorizontalAlign = HorizontalAlign.Center
                                                    td.Controls.Add(cboBox)


                                            End Select                                      ' End: Select Case sFloatTypes(ixInner)

                                            trDiv.Cells.Add(td)
                                        Next


                                        Dim tdDiv_5 As New TableCell
                                        Dim cmdDelete As New LinkButton
                                        cmdDelete.ID = HEADER_FLOAT_DELETE_PREFIX + sPostFix + ix.ToString
                                        cmdDelete.Text = PadTextNBSP("Delete", 3)
                                        tdDiv_5.Controls.Add(cmdDelete)

                                        Dim tdDiv_6 As New TableCell
                                        If ix <> iFirstGood Then
                                            Dim cmdMoveUp As New LinkButton
                                            cmdMoveUp.ID = HEADER_FLOAT_MOVEUP_PREFIX + sPostFix + ix.ToString
                                            cmdMoveUp.Text = PadTextNBSP("Mov &uarr;", 3)
                                            tdDiv_6.Controls.Add(cmdMoveUp)
                                        End If

                                        Dim tdDiv_7 As New TableCell
                                        If ix <> iLastGood Then
                                            Dim cmdMoveDown As New LinkButton
                                            cmdMoveDown.ID = HEADER_FLOAT_MOVEDOWN_PREFIX + sPostFix + ix.ToString
                                            cmdMoveDown.Text = PadTextNBSP("Mov &darr;", 3)
                                            tdDiv_7.Controls.Add(cmdMoveDown)
                                        End If


                                        trDiv.Cells.Add(tdDiv_5)
                                        trDiv.Cells.Add(tdDiv_6)
                                        trDiv.Cells.Add(tdDiv_7)
                                        tbl.Rows.Add(trDiv)
                                    End If
                                Next

                                Dim trAddExit As New TableRow
                                Dim tdAddExit As New TableCell

                                If tbl.Rows.Count > 0 Then
                                    tdAddExit.ColumnSpan = tbl.Rows(tbl.Rows.Count - 1).Cells.Count
                                End If
                                tdAddExit.HorizontalAlign = HorizontalAlign.Center

                                Dim cmdAddNew As New LinkButton
                                cmdAddNew.ID = HEADER_FLOAT_ADDNEW_PREFIX + sPostFix
                                'cmdAddNew.CssClass = "Button"
                                cmdAddNew.Text = PadTextNBSP("Add New", 3)

                                tdAddExit.Controls.Add(cmdAddNew)

                                Dim lit As New Literal
                                lit.Text = "&nbsp;&nbsp;&nbsp;"
                                tdAddExit.Controls.Add(lit)

                                Dim cmdExit As New LinkButton
                                cmdExit.ID = HEADER_FLOAT_EXIT_PREFIX + sPostFix
                                'cmdExit.CssClass = "Button"
                                cmdExit.Text = PadTextNBSP("Exit", 3)
                                tdAddExit.Controls.Add(cmdExit)

                                trAddExit.Cells.Add(tdAddExit)

                                tbl.Rows.Add(trAddExit)


                            Case "paytype_float"                        ' Paytype floating div 
                                tr.Cells.Add(td_1)
                                td_1.Text = ixc_drowConfig(CONFIG_COL_GROUP_DISPLAY).ToString() + "&nbsp;&nbsp;"

                                If _sEvent = PAYTYPE_FLOAT_BUTTON_PREFIX Then
                                    _bShowPayType = True
                                ElseIf SafeSubString(_sEvent, 0, PAYTYPE_FLOAT_ADDNEW_PREFIX.Length) = PAYTYPE_FLOAT_ADDNEW_PREFIX Then
                                    _bShowPayType = True
                                ElseIf SafeSubString(_sEvent, 0, PAYTYPE_FLOAT_DELETE_PREFIX.Length) = PAYTYPE_FLOAT_DELETE_PREFIX Then
                                    _bShowPayType = True
                                ElseIf SafeSubString(_sEvent, 0, PAYTYPE_FLOAT_MOVEUP_PREFIX.Length) = PAYTYPE_FLOAT_MOVEUP_PREFIX Then
                                    _bShowPayType = True
                                ElseIf SafeSubString(_sEvent, 0, PAYTYPE_FLOAT_MOVEDOWN_PREFIX.Length) = PAYTYPE_FLOAT_MOVEDOWN_PREFIX Then
                                    _bShowPayType = True
                                End If

                                Dim td_2 As New TableCell

                                Dim cmdFloat As New LinkButton
                                cmdFloat.ID = PAYTYPE_FLOAT_BUTTON_PREFIX
                                cmdFloat.Text = PadTextNBSP(ixc_drowConfig(CONFIG_COL_GROUP_DISPLAY).ToString(), 3)

                                divPayType.Style.Add("display", "none")
                                cmdFloat.Visible = True

                                td_2.Controls.Add(cmdFloat)
                                td_2.Controls.Add(divPayType)

                                tr.Cells.Add(td_2)
                                tblMain.Rows.Add(tr)

                                Dim aryPayTypes As New ArrayList

                                Dim sFloatTypes() As String = Split(ixc_drowConfig(CONFIG_COL_FLOAT_TYPE).ToString(), FLOATING_DISPLAY_DELIM)
                                Dim sComboValues() As String = Split(ixc_drowConfig(CONFIG_COL_COMBO_VALUES).ToString(), FLOATING_DISPLAY_DELIM)
                                Dim sOpts() As String = Split(ixc_drowConfig(CONFIG_COL_OPT).ToString(), FLOATING_DISPLAY_DELIM)

                                If _bInitialLoad Then
                                    Dim bFound As Boolean = True
                                    Dim iCnt As Integer = 0
                                    Do While bFound
                                        bFound = False
                                        iCnt += 1

                                        Dim sValList As String = ""
                                        For ix As Integer = 0 To sOpts.GetUpperBound(0)
                                            Dim objKeys2(1) As Object
                                            objKeys2(0) = sOpts(ix).Replace(FLOATING_NUM_PLACEHOLDER, iCnt.ToString())
                                            objKeys2(1) = ixc_drowConfig(CONFIG_COL_TYPE).ToString()
                                            Dim drowIn2 As DataRow = _dsIn.Tables(0).Rows.Find(objKeys2)
                                            If Not drowIn2 Is Nothing Then
                                                bFound = True
                                                sValList += drowIn2(IN_COL_VALUE).ToString() + FLOATING_DISPLAY_DELIM
                                            Else
                                                sValList += FLOATING_DISPLAY_DELIM
                                            End If
                                        Next
                                        If bFound Then
                                            ' Whack trailing FLOATING_DISPLAY_DELIM
                                            sValList = sValList.Substring(0, sValList.Length - FLOATING_DISPLAY_DELIM.Length)
                                            aryPayTypes.Add(sValList)
                                        End If
                                    Loop
                                    Session(SESSION_ARY_PAY_TYPES) = aryPayTypes
                                Else
                                    aryPayTypes = CType(Session(SESSION_ARY_PAY_TYPES), ArrayList)

                                    If _sEvent = PAYTYPE_FLOAT_ADDNEW_PREFIX Then
                                        ' Hard-code
                                        ' A string with a bunch of FLOATING_DISPLAY_DELIM characters needs to be inserted
                                        ' into aryPayTypes. The number of characters it the cell count of the first row
                                        ' of tblPayType minus 4 (special columns). Hence the hard-coded 4 below.

                                        Dim iPad As Int16 = tblPayType.Rows(0).Cells.Count - 4
                                        Dim sAdd As String = Space(iPad)
                                        aryPayTypes.Add(sAdd.Replace(" ", FLOATING_DISPLAY_DELIM))
                                    End If

                                    If SafeSubString(_sEvent, 0, PAYTYPE_FLOAT_DELETE_PREFIX.Length) = PAYTYPE_FLOAT_DELETE_PREFIX Then
                                        Dim ix As Integer = Convert.ToInt32(_sEvent.Substring(PAYTYPE_FLOAT_DELETE_PREFIX.Length))
                                        If aryPayTypes(ix).ToString().IndexOf(FLOATING_ROW_WHACK) = -1 Then
                                            aryPayTypes(ix) += FLOATING_ROW_WHACK
                                        End If
                                    End If

                                    Session(SESSION_ARY_PAY_TYPES) = aryPayTypes
                                End If


                                ' Get first and last "non-whacked" rows
                                Dim iFirstGood As Integer = -1
                                Dim iLastGood As Integer = -1
                                For ix As Integer = 0 To aryPayTypes.Count - 1
                                    If aryPayTypes(ix).ToString().IndexOf(FLOATING_ROW_WHACK) = -1 Then
                                        If iFirstGood = -1 Then
                                            iFirstGood = ix
                                        End If
                                        iLastGood = ix
                                    End If
                                Next

                                Dim iLabelCnt As Integer = 0
                                For ix As Integer = 0 To aryPayTypes.Count - 1

                                    If aryPayTypes(ix).IndexOf(FLOATING_ROW_WHACK) = -1 Then

                                        iLabelCnt += 1

                                        Dim sValues() As String = Split(aryPayTypes(ix), FLOATING_DISPLAY_DELIM)

                                        Dim trDiv As New TableRow

                                        Dim tdFlag As New TableCell
                                        tdFlag.Text = FLOATING_ROW_OUTPUT
                                        tdFlag.Width = New Unit(0)
                                        tdFlag.Visible = False
                                        trDiv.Cells.Add(tdFlag)

                                        Dim tdNum As New TableCell
                                        tdNum.Text = iLabelCnt.ToString()
                                        tdNum.HorizontalAlign = HorizontalAlign.Center
                                        trDiv.Cells.Add(tdNum)


                                        For ixInner As Integer = 0 To sFloatTypes.GetUpperBound(0)
                                            Dim td As New TableCell

                                            ' Set the conrol ID name.
                                            Dim sID As String = ""
                                            ' Note: The "control" cells start at 2, ixInner starts at 0.
                                            If ixInner = PAYTYPE_FLOAT_PAYTYPE_COL Then sID = PAYTYPE_FLOAT_PAYTYPE + ix.ToString()
                                            If ixInner = PAYTYPE_FLOAT_PAYTYPE_TICKET_COL Then sID = PAYTYPE_FLOAT_PAYTYPE_TICKET + ix.ToString()
                                            If ixInner = PAYTYPE_FLOAT_TYPE_COL Then sID = PAYTYPE_FLOAT_TYPE + ix.ToString()
                                            If ixInner = PAYTYPE_FLOAT_CHANGE_COL Then sID = PAYTYPE_FLOAT_CHANGE + ix.ToString()
                                            If ixInner = PAYTYPE_FLOAT_REFUND_COL Then sID = PAYTYPE_FLOAT_REFUND + ix.ToString()
                                            If ixInner = PAYTYPE_FLOAT_REQ_NUMBER_COL Then sID = PAYTYPE_FLOAT_REQ_NUMBER + ix.ToString()
                                            If ixInner = PAYTYPE_FLOAT_REQ_MGR_AUTH_COL Then sID = PAYTYPE_FLOAT_REQ_MGR_AUTH + ix.ToString()
                                            If ixInner = PAYTYPE_FLOAT_DRAWER_COL Then sID = PAYTYPE_FLOAT_DRAWER + ix.ToString()
                                            If ixInner = PAYTYPE_FLOAT_RECEIPT_COL Then sID = PAYTYPE_FLOAT_RECEIPT + ix.ToString()
                                            If ixInner = PAYTYPE_FLOAT_REQ_CUST_COL Then sID = PAYTYPE_FLOAT_REQ_CUST + ix.ToString()
                                            If ixInner = PAYTYPE_FLOAT_NO_SURCHARGE_COL Then sID = PAYTYPE_FLOAT_NO_SURCHARGE + ix.ToString()

                                            Select Case sFloatTypes(ixInner)

                                                Case "textbox"
                                                    Dim txtBox As New TextBox
                                                    txtBox.ID = sID

                                                    txtBox.Text = ""
                                                    If ixInner <= sValues.GetUpperBound(0) Then
                                                        txtBox.Text = sValues(ixInner)
                                                    End If

                                                    txtBox.Width = New Unit(120)
                                                    td.Controls.Add(txtBox)

                                                Case "checkbox"
                                                    Dim chkBox As New CheckBox
                                                    chkBox.ID = sID

                                                    chkBox.Checked = False
                                                    If ixInner <= sValues.GetUpperBound(0) Then
                                                        If sValues(ixInner).ToLower() = "true" Then
                                                            chkBox.Checked = True
                                                        End If
                                                    End If

                                                    td.HorizontalAlign = HorizontalAlign.Center
                                                    td.Controls.Add(chkBox)

                                                Case "combo"
                                                    Dim cboBox As New DropDownList
                                                    cboBox.ID = sID

                                                    Dim sUseVal As String = ""
                                                    If ixInner <= sValues.GetUpperBound(0) Then
                                                        sUseVal = sValues(ixInner)
                                                    End If

                                                    cboBox.Items.Add("")

                                                    Dim sSelected As String = ""
                                                    If ixInner <= sComboValues.GetUpperBound(0) Then
                                                        If sComboValues(ixInner).StartsWith("%") And sComboValues(ixInner).EndsWith("%") Then
                                                            Using con As New SqlConnection(New DataAccess().UnMaskString(WebConfigurationManager.ConnectionStrings("Production").ConnectionString, 1, 2, 3, 4, 5))
                                                                con.Open()
                                                                Using cmd As New SqlCommand("SELECT [Value] FROM Options_ComboValues WHERE [Key] = @KEY ORDER BY [SortKey]", con)
                                                                    cmd.Parameters.AddWithValue("@KEY", sComboValues(ixInner).Replace("%", ""))
                                                                    Using rdr As SqlDataReader = cmd.ExecuteReader
                                                                        While rdr.Read
                                                                            If sSelected = "" And sUseVal = ComboVal(rdr("Value").ToString.Trim.ToUpper) Then
                                                                                sSelected = sUseVal
                                                                            End If
                                                                            cboBox.Items.Add(ComboItem(rdr("Value").ToString))
                                                                        End While
                                                                    End Using
                                                                End Using
                                                            End Using
                                                        Else
                                                            Dim sComboLoad() As String = Split(sComboValues(ixInner), "|")
                                                            For ixCombo As Integer = 0 To sComboLoad.GetUpperBound(0)
                                                                cboBox.Items.Add(ComboItem(sComboLoad(ixCombo)))

                                                                If sSelected = "" And sUseVal = ComboVal(sComboLoad(ixCombo)) Then
                                                                    sSelected = sUseVal
                                                                End If
                                                            Next
                                                        End If
                                                    End If
                                                    cboBox.SelectedValue = sSelected

                                                    td.Controls.Add(cboBox)


                                            End Select                                      ' End: Select Case sFloatTypes(ixInner)

                                            trDiv.Cells.Add(td)

                                        Next                                     ' End: For ixInner As Integer = 0 To sFloatTypes.GetUpperBound(0)

                                        Dim tdDelete = New TableCell
                                        Dim cmdDelete As New LinkButton
                                        cmdDelete.ID = PAYTYPE_FLOAT_DELETE_PREFIX + ix.ToString
                                        cmdDelete.Text = PadTextNBSP("Delete", 3)
                                        tdDelete.Controls.Add(cmdDelete)

                                        Dim tdMoveUp As New TableCell
                                        If ix <> iFirstGood Then
                                            Dim cmdMoveUp As New LinkButton
                                            cmdMoveUp.ID = PAYTYPE_FLOAT_MOVEUP_PREFIX + ix.ToString
                                            cmdMoveUp.Text = PadTextNBSP("Mov &uarr;", 3)
                                            tdMoveUp.Controls.Add(cmdMoveUp)
                                        End If

                                        Dim tdMoveDown As New TableCell
                                        If ix <> iLastGood Then
                                            Dim cmdMoveDown As New LinkButton
                                            cmdMoveDown.ID = PAYTYPE_FLOAT_MOVEDOWN_PREFIX + ix.ToString
                                            cmdMoveDown.Text = PadTextNBSP("Mov &darr;", 3)
                                            tdMoveDown.Controls.Add(cmdMoveDown)
                                        End If

                                        trDiv.Cells.Add(tdDelete)
                                        trDiv.Cells.Add(tdMoveUp)
                                        trDiv.Cells.Add(tdMoveDown)
                                        tblPayType.Rows.Add(trDiv)

                                    End If                              ' End: If aryPayTypes(ix).IndexOf(FLOATING_ROW_WHACK) = -1 Then

                                Next                             ' End: For ix As Integer = 0 To aryPayTypes.Count - 1

                                Dim trAddExit As New TableRow
                                Dim tdAddExit As New TableCell

                                tdAddExit.ColumnSpan = tblPayType.Rows(0).Cells.Count
                                tdAddExit.HorizontalAlign = HorizontalAlign.Center

                                Dim cmdAddNew As New LinkButton
                                cmdAddNew.ID = PAYTYPE_FLOAT_ADDNEW_PREFIX
                                'cmdAddNew.CssClass = "Button"
                                cmdAddNew.Text = PadTextNBSP("Add New", 3)

                                tdAddExit.Controls.Add(cmdAddNew)

                                Dim lit As New Literal
                                lit.Text = "&nbsp;&nbsp;&nbsp;"
                                tdAddExit.Controls.Add(lit)

                                Dim cmdExit As New LinkButton
                                cmdExit.ID = PAYTYPE_FLOAT_EXIT_PREFIX
                                'cmdExit.CssClass = "Button"
                                cmdExit.Text = PadTextNBSP("Exit", 3)
                                tdAddExit.Controls.Add(cmdExit)

                                trAddExit.Cells.Add(tdAddExit)

                                tblPayType.Rows.Add(trAddExit)

                            Case "printing"

                                If Not _bPrinterRowAdded Then
                                    td_1.Controls.Add(tblPrinting)
                                    tr.Cells.Add(td_1)
                                    tblMain.Rows.Add(tr)
                                    _bPrinterRowAdded = True
                                End If

                                Dim trPrinter As New TableRow
                                trPrinter.Height = New Unit(DEFAULT_ROW_HEIGHT)

                                Dim tdLabel As New TableCell
                                tdLabel.Text = ixc_drowConfig(CONFIG_COL_DISPLAY).ToString() + "&nbsp;&nbsp;"
                                trPrinter.Cells.Add(tdLabel)

                                Dim tdNum As New TableCell
                                tdNum.HorizontalAlign = HorizontalAlign.Right
                                tdNum.Text = "&nbsp;"
                                trPrinter.Cells.Add(tdNum)

                                Dim sFloatType() As String = Split(ixc_drowConfig(CONFIG_COL_FLOAT_TYPE).ToString(), FLOATING_DISPLAY_DELIM)
                                Dim sOpt() As String = Split(ixc_drowConfig(CONFIG_COL_OPT).ToString(), FLOATING_DISPLAY_DELIM)
                                Dim sComboVals() As String = Split(ixc_drowConfig(CONFIG_COL_COMBO_VALUES).ToString(), FLOATING_DISPLAY_DELIM)

                                For ix As Integer = 0 To sFloatType.Length - 1
                                    Dim td As New TableCell
                                    td.HorizontalAlign = HorizontalAlign.Center

                                    Dim objFind(1) As Object
                                    objFind(0) = sOpt(ix)
                                    objFind(1) = ixc_drowConfig(CONFIG_COL_TYPE).ToString()
                                    Dim drowFind As DataRow = _dsIn.Tables(0).Rows.Find(objFind)
                                    Dim sOldVal As String = ""
                                    If Not drowFind Is Nothing Then
                                        sOldVal = drowFind(IN_COL_VALUE).ToString()
                                    End If

                                    Select Case sFloatType(ix).ToLower()

                                        Case "textbox"
                                            Dim txtBox As New TextBox
                                            txtBox.ID = "printing_" + sOpt(ix)
                                            txtBox.Text = sOldVal
                                            td.Controls.Add(txtBox)

                                        Case "checkbox"
                                            Dim chkBox As New CheckBox
                                            chkBox.ID = "printing_" + sOpt(ix)

                                            Dim bVal As Boolean = False
                                            Try
                                                bVal = Convert.ToBoolean(sOldVal)
                                            Catch ex As Exception
                                                ' If the value won't convert (probably because one wasn't there, it will be set to false.
                                                bVal = False
                                            End Try

                                            chkBox.Checked = bVal
                                            td.Controls.Add(chkBox)

                                        Case "combo"
                                            Dim cboBox As New DropDownList
                                            cboBox.ID = "printing_" + sOpt(ix)

                                            Dim sSelected As String = ""
                                            Dim sVals() As String = Split(sComboVals(ix), "|")
                                            For ix2 As Integer = 0 To sVals.Length - 1

                                                cboBox.Items.Add(ComboItem(sVals(ix2)))

                                                If ComboVal(sVals(ix2)) = sOldVal Then
                                                    sSelected = sOldVal
                                                End If

                                            Next

                                            If sSelected <> "" Then
                                                cboBox.SelectedValue = sSelected
                                            End If

                                            td.Controls.Add(cboBox)

                                    End Select

                                    trPrinter.Cells.Add(td)
                                Next

                                tblPrinting.Rows.Add(trPrinter)


                                ' Hard-code - lots of it
                            Case "scheduled_print"
                                tr.Cells.Add(td_1)
                                td_1.Text = ixc_drowConfig(CONFIG_COL_DISPLAY).ToString() + "&nbsp;&nbsp;"

                                Dim td_2 As New TableCell

                                Dim tblAutoTime As New Table
                                tblAutoTime.Font.Name = "Tahoma"
                                tblAutoTime.Font.Size = FontUnit.Point(9)

                                Dim trAutoTime0 As New TableRow
                                Dim trAutoTime1 As New TableRow
                                Dim td0_0 As New TableCell
                                Dim td0_1 As New TableCell
                                Dim td1_0 As New TableCell
                                Dim td1_1 As New TableCell

                                Dim sValHH As String = ixc_drowConfig(CONFIG_COL_COMBO_VALUES).ToString().Substring(0, ixc_drowConfig(CONFIG_COL_COMBO_VALUES).ToString().IndexOf(FLOATING_DISPLAY_DELIM))
                                Dim sValHHs() As String = sValHH.Split("|")
                                Dim sValMM As String = ixc_drowConfig(CONFIG_COL_COMBO_VALUES).ToString().Substring(ixc_drowConfig(CONFIG_COL_COMBO_VALUES).ToString().IndexOf(FLOATING_DISPLAY_DELIM) + FLOATING_DISPLAY_DELIM.Length)
                                Dim sValMMs() As String = sValMM.Split("|")

                                td0_0.Text = "Hour"
                                td0_0.HorizontalAlign = HorizontalAlign.Center
                                trAutoTime0.Cells.Add(td0_0)

                                td0_1.Text = "Minute"
                                td0_1.HorizontalAlign = HorizontalAlign.Center
                                trAutoTime0.Cells.Add(td0_1)

                                tblAutoTime.Rows.Add(trAutoTime0)

                                Dim cboAutoTimeHH As New DropDownList
                                cboAutoTimeHH.ID = "scheduled_printHH"
                                lTagIdx += 1
                                cboAutoTimeHH.TabIndex = lTagIdx

                                Dim sSelected As String = ""
                                For ix As Integer = 0 To sValHHs.Length - 1
                                    cboAutoTimeHH.Items.Add(sValHHs(ix))
                                    If sDataValue.Length >= 2 Then
                                        If sValHHs(ix) = sDataValue.Substring(0, 2) Then
                                            sSelected = sDataValue.Substring(0, 2)
                                        End If
                                    End If
                                Next

                                If sSelected <> "" Then
                                    cboAutoTimeHH.SelectedValue = sSelected
                                End If

                                td1_0.Controls.Add(cboAutoTimeHH)
                                trAutoTime1.Cells.Add(td1_0)

                                Dim cboAutoTimeMM As New DropDownList
                                cboAutoTimeMM.ID = "scheduled_printMM"
                                lTagIdx += 1
                                cboAutoTimeMM.TabIndex = lTagIdx

                                sSelected = ""
                                For ix As Integer = 0 To sValMMs.Length - 1
                                    cboAutoTimeMM.Items.Add(sValMMs(ix))
                                    If sDataValue.Length >= 4 Then
                                        If sValMMs(ix) = sDataValue.Substring(2, 2) Then
                                            sSelected = sDataValue.Substring(2, 2)
                                        End If
                                    End If
                                Next

                                If sSelected <> "" Then
                                    cboAutoTimeMM.SelectedValue = sSelected
                                End If

                                td1_1.Controls.Add(cboAutoTimeMM)
                                trAutoTime1.Cells.Add(td1_1)

                                tblAutoTime.Rows.Add(trAutoTime1)

                                td_2.Controls.Add(tblAutoTime)
                                tr.Cells.Add(td_2)
                                tblMain.Rows.Add(tr)


                            Case "logo_header", "logo_footer"
                                'td_1.Text = ixc_drowConfig(CONFIG_COL_GROUP_DISPLAY).ToString() + "&nbsp;&nbsp;"

                                Dim bHeader As Boolean = False
                                If ixc_drowConfig(CONFIG_COL_CONTROL_TYPE).ToString() = "logo_header" Then
                                    bHeader = True
                                End If

                                If _sEvent = LOGO_HEADER_BUTTON_PREFIX Then
                                    _bShowLogoHeader = True
                                ElseIf SafeSubString(_sEvent, 0, LOGO_HEADER_ADDNEW_PREFIX.Length) = LOGO_HEADER_ADDNEW_PREFIX Then
                                    _bShowLogoHeader = True
                                ElseIf SafeSubString(_sEvent, 0, LOGO_HEADER_DELETE_PREFIX.Length) = LOGO_HEADER_DELETE_PREFIX Then
                                    _bShowLogoHeader = True
                                ElseIf SafeSubString(_sEvent, 0, LOGO_HEADER_MOVEUP_PREFIX.Length) = LOGO_HEADER_MOVEUP_PREFIX Then
                                    _bShowLogoHeader = True
                                ElseIf SafeSubString(_sEvent, 0, LOGO_HEADER_MOVEDOWN_PREFIX.Length) = LOGO_HEADER_MOVEDOWN_PREFIX Then
                                    _bShowLogoHeader = True
                                ElseIf _sEvent = LOGO_FOOTER_BUTTON_PREFIX Then
                                    _bShowLogoFooter = True
                                ElseIf SafeSubString(_sEvent, 0, LOGO_FOOTER_ADDNEW_PREFIX.Length) = LOGO_FOOTER_ADDNEW_PREFIX Then
                                    _bShowLogoFooter = True
                                ElseIf SafeSubString(_sEvent, 0, LOGO_FOOTER_DELETE_PREFIX.Length) = LOGO_FOOTER_DELETE_PREFIX Then
                                    _bShowLogoFooter = True
                                ElseIf SafeSubString(_sEvent, 0, LOGO_FOOTER_MOVEUP_PREFIX.Length) = LOGO_FOOTER_MOVEUP_PREFIX Then
                                    _bShowLogoFooter = True
                                ElseIf SafeSubString(_sEvent, 0, LOGO_FOOTER_MOVEDOWN_PREFIX.Length) = LOGO_FOOTER_MOVEDOWN_PREFIX Then
                                    _bShowLogoFooter = True
                                End If

                                Dim cmdLogo As New LinkButton
                                If bHeader Then
                                    cmdLogo.ID = LOGO_HEADER_BUTTON_PREFIX
                                Else
                                    cmdLogo.ID = LOGO_FOOTER_BUTTON_PREFIX
                                End If
                                cmdLogo.Text = PadTextNBSP(ixc_drowConfig(CONFIG_COL_GROUP_DISPLAY).ToString(), 3)
                                cmdLogo.Visible = True

                                td_1.Controls.Add(cmdLogo)
                                If bHeader Then
                                    divLogoHeader.Style.Add("display", "none")
                                    td_1.Controls.Add(divLogoHeader)
                                Else
                                    divLogoFooter.Style.Add("display", "none")
                                    td_1.Controls.Add(divLogoFooter)
                                End If

                                tr.Cells.Add(td_1)
                                tblMain.Rows.Add(tr)

                                Dim aryLogo As New ArrayList

                                Dim sFloatTypes() As String = Split(ixc_drowConfig(CONFIG_COL_FLOAT_TYPE).ToString(), FLOATING_DISPLAY_DELIM)
                                Dim sComboValues() As String = Split(ixc_drowConfig(CONFIG_COL_COMBO_VALUES).ToString(), FLOATING_DISPLAY_DELIM)
                                Dim sOpts() As String = Split(ixc_drowConfig(CONFIG_COL_OPT).ToString(), FLOATING_DISPLAY_DELIM)

                                If _bInitialLoad Then
                                    Dim bFound As Boolean = True
                                    Dim iCnt As Integer = 0
                                    Do While bFound
                                        bFound = False
                                        iCnt += 1

                                        Dim sValList As String = ""
                                        For ix As Integer = 0 To sOpts.GetUpperBound(0)
                                            Dim objKeys2(1) As Object
                                            objKeys2(0) = sOpts(ix).Replace(FLOATING_NUM_PLACEHOLDER, iCnt.ToString())
                                            objKeys2(1) = ixc_drowConfig(CONFIG_COL_TYPE).ToString()
                                            Dim drowIn2 As DataRow = _dsIn.Tables(0).Rows.Find(objKeys2)
                                            If Not drowIn2 Is Nothing Then
                                                bFound = True
                                                sValList += drowIn2(IN_COL_VALUE).ToString() + FLOATING_DISPLAY_DELIM
                                            Else
                                                sValList += FLOATING_DISPLAY_DELIM
                                            End If
                                        Next
                                        If bFound Then
                                            ' Whack trailing FLOATING_DISPLAY_DELIM
                                            sValList = sValList.Substring(0, sValList.Length - FLOATING_DISPLAY_DELIM.Length)
                                            aryLogo.Add(sValList)
                                        End If
                                    Loop
                                    If bHeader Then
                                        Session(SESSION_ARY_LOGO_HEADER) = aryLogo
                                    Else
                                        Session(SESSION_ARY_LOGO_FOOTER) = aryLogo
                                    End If
                                Else
                                    If bHeader Then
                                        aryLogo = Session(SESSION_ARY_LOGO_HEADER)
                                    Else
                                        aryLogo = Session(SESSION_ARY_LOGO_FOOTER)
                                    End If

                                    If _sEvent = LOGO_HEADER_ADDNEW_PREFIX And bHeader Then
                                        ' Hard-code
                                        ' A string with a bunch of FLOATING_DISPLAY_DELIM characters needs to be inserted
                                        ' into aryLogo. The number of characters it the cell count of the first row
                                        ' of tblLogoHeader minus 4 (special columns). Hence the hard-coded 4 below.

                                        Dim iPad As Int16 = tblLogoHeader.Rows(0).Cells.Count - 4
                                        Dim sAdd As String = Space(iPad)
                                        aryLogo.Add(sAdd.Replace(" ", FLOATING_DISPLAY_DELIM))
                                    End If

                                    If _sEvent = LOGO_FOOTER_ADDNEW_PREFIX And Not bHeader Then
                                        ' Hard-code
                                        ' A string with a bunch of FLOATING_DISPLAY_DELIM characters needs to be inserted
                                        ' into aryLogo. The number of characters it the cell count of the first row
                                        ' of tblLogoFooter minus 4 (special columns). Hence the hard-coded 4 below.

                                        Dim iPad As Int16 = tblLogoFooter.Rows(0).Cells.Count - 4
                                        Dim sAdd As String = Space(iPad)
                                        aryLogo.Add(sAdd.Replace(" ", FLOATING_DISPLAY_DELIM))
                                    End If

                                    If SafeSubString(_sEvent, 0, LOGO_HEADER_DELETE_PREFIX.Length) = LOGO_HEADER_DELETE_PREFIX And bHeader Then
                                        Dim ix As Integer = Convert.ToInt32(_sEvent.Substring(LOGO_HEADER_DELETE_PREFIX.Length))
                                        If aryLogo(ix).ToString().IndexOf(FLOATING_ROW_WHACK) = -1 Then
                                            aryLogo(ix) += FLOATING_ROW_WHACK
                                        End If
                                    End If

                                    If SafeSubString(_sEvent, 0, LOGO_FOOTER_DELETE_PREFIX.Length) = LOGO_FOOTER_DELETE_PREFIX And Not bHeader Then
                                        Dim ix As Integer = Convert.ToInt32(_sEvent.Substring(LOGO_FOOTER_DELETE_PREFIX.Length))
                                        If aryLogo(ix).ToString().IndexOf(FLOATING_ROW_WHACK) = -1 Then
                                            aryLogo(ix) += FLOATING_ROW_WHACK
                                        End If
                                    End If

                                    If bHeader Then
                                        Session(SESSION_ARY_LOGO_HEADER) = aryLogo
                                    Else
                                        Session(SESSION_ARY_LOGO_FOOTER) = aryLogo
                                    End If
                                End If


                                ' Get first and last "non-whacked" rows
                                Dim iFirstGood As Integer = -1
                                Dim iLastGood As Integer = -1
                                For ix As Integer = 0 To aryLogo.Count - 1
                                    If aryLogo(ix).ToString().IndexOf(FLOATING_ROW_WHACK) = -1 Then
                                        If iFirstGood = -1 Then
                                            iFirstGood = ix
                                        End If
                                        iLastGood = ix
                                    End If
                                Next

                                Dim iLabelCnt As Integer = 0
                                For ix As Integer = 0 To aryLogo.Count - 1

                                    If aryLogo(ix).IndexOf(FLOATING_ROW_WHACK) = -1 Then

                                        iLabelCnt += 1

                                        Dim sValues() As String = Split(aryLogo(ix), FLOATING_DISPLAY_DELIM)

                                        Dim trDiv As New TableRow

                                        Dim tdFlag As New TableCell
                                        tdFlag.Text = FLOATING_ROW_OUTPUT
                                        tdFlag.Width = New Unit(0)
                                        tdFlag.Visible = False
                                        trDiv.Cells.Add(tdFlag)

                                        Dim tdNum As New TableCell
                                        tdNum.Text = iLabelCnt.ToString()
                                        tdNum.HorizontalAlign = HorizontalAlign.Center
                                        trDiv.Cells.Add(tdNum)


                                        For ixInner As Integer = 0 To sFloatTypes.GetUpperBound(0)
                                            Dim td As New TableCell

                                            ' Set the conrol ID name.
                                            Dim sID As String = ""
                                            ' Note: The "control" cells start at 2, ixInner starts at 0.
                                            If bHeader Then
                                                If ixInner = LOGO_HEADER_TEXT_COL Then sID = LOGO_HEADER_TEXT + ix.ToString()
                                                If ixInner = LOGO_HEADER_JUSTIFICATION_COL Then sID = LOGO_HEADER_JUSTIFICATION + ix.ToString()
                                                If ixInner = LOGO_HEADER_LARGE_COL Then sID = LOGO_HEADER_LARGE + ix.ToString()
                                                If ixInner = LOGO_HEADER_BOLD_COL Then sID = LOGO_HEADER_BOLD + ix.ToString()
                                                If ixInner = LOGO_HEADER_WIDE_COL Then sID = LOGO_HEADER_WIDE + ix.ToString()
                                                If ixInner = LOGO_HEADER_HIGH_COL Then sID = LOGO_HEADER_HIGH + ix.ToString()
                                                If ixInner = LOGO_HEADER_UNDERLINE_COL Then sID = LOGO_HEADER_UNDERLINE + ix.ToString()
                                                If ixInner = LOGO_HEADER_QR_CODE_COL Then sID = LOGO_HEADER_QR_CODE + ix.ToString()
                                                If ixInner = LOGO_HEADER_QR_PIXELS_COL Then sID = LOGO_HEADER_QR_PIXELS + ix.ToString()
                                                If ixInner = LOGO_HEADER_QR_QUALITY_COL Then sID = LOGO_HEADER_QR_QUALITY + ix.ToString()
                                            Else
                                                If ixInner = LOGO_FOOTER_TEXT_COL Then sID = LOGO_FOOTER_TEXT + ix.ToString()
                                                If ixInner = LOGO_FOOTER_JUSTIFICATION_COL Then sID = LOGO_FOOTER_JUSTIFICATION + ix.ToString()
                                                If ixInner = LOGO_FOOTER_LARGE_COL Then sID = LOGO_FOOTER_LARGE + ix.ToString()
                                                If ixInner = LOGO_FOOTER_BOLD_COL Then sID = LOGO_FOOTER_BOLD + ix.ToString()
                                                If ixInner = LOGO_FOOTER_WIDE_COL Then sID = LOGO_FOOTER_WIDE + ix.ToString()
                                                If ixInner = LOGO_FOOTER_HIGH_COL Then sID = LOGO_FOOTER_HIGH + ix.ToString()
                                                If ixInner = LOGO_FOOTER_UNDERLINE_COL Then sID = LOGO_FOOTER_UNDERLINE + ix.ToString()
                                                If ixInner = LOGO_FOOTER_QR_CODE_COL Then sID = LOGO_FOOTER_QR_CODE + ix.ToString()
                                                If ixInner = LOGO_FOOTER_QR_PIXELS_COL Then sID = LOGO_FOOTER_QR_PIXELS + ix.ToString()
                                                If ixInner = LOGO_FOOTER_QR_QUALITY_COL Then sID = LOGO_FOOTER_QR_QUALITY + ix.ToString()
                                            End If

                                            Select Case sFloatTypes(ixInner)

                                                Case "textbox"
                                                    Dim txtBox As New TextBox
                                                    txtBox.ID = sID

                                                    txtBox.Text = ""
                                                    If ixInner <= sValues.GetUpperBound(0) Then
                                                        txtBox.Text = sValues(ixInner)
                                                    End If

                                                    txtBox.Width = New Unit(120)
                                                    td.Controls.Add(txtBox)

                                                Case "checkbox"
                                                    Dim chkBox As New CheckBox
                                                    chkBox.ID = sID

                                                    chkBox.Checked = False
                                                    If ixInner <= sValues.GetUpperBound(0) Then
                                                        If sValues(ixInner).ToLower() = "true" Then
                                                            chkBox.Checked = True
                                                        End If
                                                    End If

                                                    td.HorizontalAlign = HorizontalAlign.Center
                                                    td.Controls.Add(chkBox)

                                                Case "combo"
                                                    Dim cboBox As New DropDownList
                                                    cboBox.ID = sID

                                                    Dim sUseVal As String = ""
                                                    If ixInner <= sValues.GetUpperBound(0) Then
                                                        sUseVal = sValues(ixInner)
                                                    End If

                                                    cboBox.Items.Add("")

                                                    Dim sSelected As String = ""
                                                    If ixInner <= sComboValues.GetUpperBound(0) Then
                                                        Dim sComboLoad() As String = Split(sComboValues(ixInner), "|")
                                                        For ixCombo As Integer = 0 To sComboLoad.GetUpperBound(0)
                                                            cboBox.Items.Add(ComboItem(sComboLoad(ixCombo)))

                                                            If sUseVal = ComboVal(sComboLoad(ixCombo)) Then
                                                                sSelected = ComboVal(sComboLoad(ixCombo))
                                                            End If
                                                        Next
                                                    End If
                                                    cboBox.SelectedValue = sSelected

                                                    td.Controls.Add(cboBox)


                                            End Select                                      ' End: Select Case sFloatTypes(ixInner)

                                            trDiv.Cells.Add(td)

                                        Next                                     ' End: For ixInner As Integer = 0 To sFloatTypes.GetUpperBound(0)

                                        Dim tdDelete = New TableCell
                                        Dim cmdDelete As New LinkButton
                                        If bHeader Then
                                            cmdDelete.ID = LOGO_HEADER_DELETE_PREFIX + ix.ToString
                                        Else
                                            cmdDelete.ID = LOGO_FOOTER_DELETE_PREFIX + ix.ToString
                                        End If
                                        cmdDelete.Text = PadTextNBSP("Delete", 3)
                                        tdDelete.Controls.Add(cmdDelete)

                                        Dim tdMoveUp As New TableCell
                                        If ix <> iFirstGood Then
                                            Dim cmdMoveUp As New LinkButton
                                            If bHeader Then
                                                cmdMoveUp.ID = LOGO_HEADER_MOVEUP_PREFIX + ix.ToString
                                            Else
                                                cmdMoveUp.ID = LOGO_FOOTER_MOVEUP_PREFIX + ix.ToString
                                            End If
                                            cmdMoveUp.Text = PadTextNBSP("Mov &uarr;", 3)
                                            tdMoveUp.Controls.Add(cmdMoveUp)
                                        End If

                                        Dim tdMoveDown As New TableCell
                                        If ix <> iLastGood Then
                                            Dim cmdMoveDown As New LinkButton
                                            If bHeader Then
                                                cmdMoveDown.ID = LOGO_HEADER_MOVEDOWN_PREFIX + ix.ToString
                                            Else
                                                cmdMoveDown.ID = LOGO_FOOTER_MOVEDOWN_PREFIX + ix.ToString
                                            End If
                                            cmdMoveDown.Text = PadTextNBSP("Mov &darr;", 3)
                                            tdMoveDown.Controls.Add(cmdMoveDown)
                                        End If

                                        trDiv.Cells.Add(tdDelete)
                                        trDiv.Cells.Add(tdMoveUp)
                                        trDiv.Cells.Add(tdMoveDown)
                                        If bHeader Then
                                            tblLogoHeader.Rows.Add(trDiv)
                                        Else
                                            tblLogoFooter.Rows.Add(trDiv)
                                        End If

                                    End If                              ' End: If aryLogo(ix).IndexOf(FLOATING_ROW_WHACK) = -1 Then

                                Next                             ' End: For ix As Integer = 0 To aryPayTypes.Count - 1

                                Dim trAddExit As New TableRow
                                Dim tdAddExit As New TableCell

                                If bHeader Then
                                    tdAddExit.ColumnSpan = tblLogoHeader.Rows(0).Cells.Count
                                Else
                                    tdAddExit.ColumnSpan = tblLogoFooter.Rows(0).Cells.Count
                                End If
                                tdAddExit.HorizontalAlign = HorizontalAlign.Center

                                Dim cmdAddNew As New LinkButton
                                If bHeader Then
                                    cmdAddNew.ID = LOGO_HEADER_ADDNEW_PREFIX
                                Else
                                    cmdAddNew.ID = LOGO_FOOTER_ADDNEW_PREFIX
                                End If
                                'cmdAddNew.CssClass = "Button"
                                cmdAddNew.Text = PadTextNBSP("Add New", 3)

                                tdAddExit.Controls.Add(cmdAddNew)

                                Dim lit As New Literal
                                lit.Text = "&nbsp;&nbsp;&nbsp;"
                                tdAddExit.Controls.Add(lit)

                                Dim cmdExit As New LinkButton
                                If bHeader Then
                                    cmdExit.ID = LOGO_HEADER_EXIT_PREFIX
                                Else
                                    cmdExit.ID = LOGO_FOOTER_EXIT_PREFIX
                                End If

                                'cmdExit.CssClass = "Button"
                                cmdExit.Text = PadTextNBSP("Exit", 3)
                                tdAddExit.Controls.Add(cmdExit)

                                trAddExit.Cells.Add(tdAddExit)

                                If bHeader Then
                                    tblLogoHeader.Rows.Add(trAddExit)
                                Else
                                    tblLogoFooter.Rows.Add(trAddExit)
                                End If

                            Case "cs_header", "cs_footer"
                                'td_1.Text = ixc_drowConfig(CONFIG_COL_GROUP_DISPLAY).ToString() + "&nbsp;&nbsp;"

                                Dim bHeader As Boolean = False
                                If ixc_drowConfig(CONFIG_COL_CONTROL_TYPE).ToString() = "cs_header" Then
                                    bHeader = True
                                End If

                                If _sEvent = CS_HEADER_BUTTON_PREFIX Then
                                    _bShowCSHeader = True
                                ElseIf SafeSubString(_sEvent, 0, CS_HEADER_ADDNEW_PREFIX.Length) = CS_HEADER_ADDNEW_PREFIX Then
                                    _bShowCSHeader = True
                                ElseIf SafeSubString(_sEvent, 0, CS_HEADER_DELETE_PREFIX.Length) = CS_HEADER_DELETE_PREFIX Then
                                    _bShowCSHeader = True
                                ElseIf SafeSubString(_sEvent, 0, CS_HEADER_MOVEUP_PREFIX.Length) = CS_HEADER_MOVEUP_PREFIX Then
                                    _bShowCSHeader = True
                                ElseIf SafeSubString(_sEvent, 0, CS_HEADER_MOVEDOWN_PREFIX.Length) = CS_HEADER_MOVEDOWN_PREFIX Then
                                    _bShowCSHeader = True
                                ElseIf _sEvent = CS_FOOTER_BUTTON_PREFIX Then
                                    _bShowCSFooter = True
                                ElseIf SafeSubString(_sEvent, 0, CS_FOOTER_ADDNEW_PREFIX.Length) = CS_FOOTER_ADDNEW_PREFIX Then
                                    _bShowCSFooter = True
                                ElseIf SafeSubString(_sEvent, 0, CS_FOOTER_DELETE_PREFIX.Length) = CS_FOOTER_DELETE_PREFIX Then
                                    _bShowCSFooter = True
                                ElseIf SafeSubString(_sEvent, 0, CS_FOOTER_MOVEUP_PREFIX.Length) = CS_FOOTER_MOVEUP_PREFIX Then
                                    _bShowCSFooter = True
                                ElseIf SafeSubString(_sEvent, 0, CS_FOOTER_MOVEDOWN_PREFIX.Length) = CS_FOOTER_MOVEDOWN_PREFIX Then
                                    _bShowCSFooter = True
                                End If

                                Dim cmdCS As New LinkButton
                                If bHeader Then
                                    cmdCS.ID = CS_HEADER_BUTTON_PREFIX
                                Else
                                    cmdCS.ID = CS_FOOTER_BUTTON_PREFIX
                                End If
                                cmdCS.Text = PadTextNBSP(ixc_drowConfig(CONFIG_COL_GROUP_DISPLAY).ToString(), 3)
                                cmdCS.Visible = True

                                td_1.Controls.Add(cmdCS)
                                If bHeader Then
                                    divCustomerSurveyHeader.Style.Add("display", "none")
                                    td_1.Controls.Add(divCustomerSurveyHeader)
                                Else
                                    divCustomerSurveyFooter.Style.Add("display", "none")
                                    td_1.Controls.Add(divCustomerSurveyFooter)
                                End If

                                tr.Cells.Add(td_1)
                                tblMain.Rows.Add(tr)

                                Dim aryCS As New ArrayList

                                Dim sFloatTypes() As String = Split(ixc_drowConfig(CONFIG_COL_FLOAT_TYPE).ToString(), FLOATING_DISPLAY_DELIM)
                                Dim sComboValues() As String = Split(ixc_drowConfig(CONFIG_COL_COMBO_VALUES).ToString(), FLOATING_DISPLAY_DELIM)
                                Dim sOpts() As String = Split(ixc_drowConfig(CONFIG_COL_OPT).ToString(), FLOATING_DISPLAY_DELIM)

                                If _bInitialLoad Then
                                    Dim bFound As Boolean = True
                                    Dim iCnt As Integer = 0
                                    Do While bFound
                                        bFound = False
                                        iCnt += 1

                                        Dim sValList As String = ""
                                        For ix As Integer = 0 To sOpts.GetUpperBound(0)
                                            Dim objKeys2(1) As Object
                                            objKeys2(0) = sOpts(ix).Replace(FLOATING_NUM_PLACEHOLDER, iCnt.ToString())
                                            objKeys2(1) = ixc_drowConfig(CONFIG_COL_TYPE).ToString()
                                            Dim drowIn2 As DataRow = _dsIn.Tables(0).Rows.Find(objKeys2)
                                            If Not drowIn2 Is Nothing Then
                                                bFound = True
                                                sValList += drowIn2(IN_COL_VALUE).ToString() + FLOATING_DISPLAY_DELIM
                                            Else
                                                sValList += FLOATING_DISPLAY_DELIM
                                            End If
                                        Next
                                        If bFound Then
                                            ' Whack trailing FLOATING_DISPLAY_DELIM
                                            sValList = sValList.Substring(0, sValList.Length - FLOATING_DISPLAY_DELIM.Length)
                                            aryCS.Add(sValList)
                                        End If
                                    Loop
                                    If bHeader Then
                                        Session(SESSION_ARY_CS_HEADER) = aryCS
                                    Else
                                        Session(SESSION_ARY_CS_FOOTER) = aryCS
                                    End If
                                Else
                                    If bHeader Then
                                        aryCS = Session(SESSION_ARY_CS_HEADER)
                                    Else
                                        aryCS = Session(SESSION_ARY_CS_FOOTER)
                                    End If

                                    If _sEvent = CS_HEADER_ADDNEW_PREFIX And bHeader Then
                                        ' Hard-code
                                        ' A string with a bunch of FLOATING_DISPLAY_DELIM characters needs to be inserted
                                        ' into aryLogo. The number of characters it the cell count of the first row
                                        ' of tblLogoHeader minus 4 (special columns). Hence the hard-coded 4 below.

                                        Dim iPad As Int16 = tblCustomerSurveyHeader.Rows(0).Cells.Count - 4
                                        Dim sAdd As String = Space(iPad)
                                        aryCS.Add(sAdd.Replace(" ", FLOATING_DISPLAY_DELIM))
                                    End If

                                    If _sEvent = CS_FOOTER_ADDNEW_PREFIX And Not bHeader Then
                                        ' Hard-code
                                        ' A string with a bunch of FLOATING_DISPLAY_DELIM characters needs to be inserted
                                        ' into aryLogo. The number of characters it the cell count of the first row
                                        ' of tblLogoFooter minus 4 (special columns). Hence the hard-coded 4 below.

                                        Dim iPad As Int16 = tblCustomerSurveyFooter.Rows(0).Cells.Count - 4
                                        Dim sAdd As String = Space(iPad)
                                        aryCS.Add(sAdd.Replace(" ", FLOATING_DISPLAY_DELIM))
                                    End If

                                    If SafeSubString(_sEvent, 0, CS_HEADER_DELETE_PREFIX.Length) = CS_HEADER_DELETE_PREFIX And bHeader Then
                                        Dim ix As Integer = Convert.ToInt32(_sEvent.Substring(CS_HEADER_DELETE_PREFIX.Length))
                                        If aryCS(ix).ToString().IndexOf(FLOATING_ROW_WHACK) = -1 Then
                                            aryCS(ix) += FLOATING_ROW_WHACK
                                        End If
                                    End If

                                    If SafeSubString(_sEvent, 0, CS_FOOTER_DELETE_PREFIX.Length) = CS_FOOTER_DELETE_PREFIX And Not bHeader Then
                                        Dim ix As Integer = Convert.ToInt32(_sEvent.Substring(CS_FOOTER_DELETE_PREFIX.Length))
                                        If aryCS(ix).ToString().IndexOf(FLOATING_ROW_WHACK) = -1 Then
                                            aryCS(ix) += FLOATING_ROW_WHACK
                                        End If
                                    End If

                                    If bHeader Then
                                        Session(SESSION_ARY_CS_HEADER) = aryCS
                                    Else
                                        Session(SESSION_ARY_CS_FOOTER) = aryCS
                                    End If
                                End If


                                ' Get first and last "non-whacked" rows
                                Dim iFirstGood As Integer = -1
                                Dim iLastGood As Integer = -1
                                For ix As Integer = 0 To aryCS.Count - 1
                                    If aryCS(ix).ToString().IndexOf(FLOATING_ROW_WHACK) = -1 Then
                                        If iFirstGood = -1 Then
                                            iFirstGood = ix
                                        End If
                                        iLastGood = ix
                                    End If
                                Next

                                Dim iLabelCnt As Integer = 0
                                For ix As Integer = 0 To aryCS.Count - 1

                                    If aryCS(ix).IndexOf(FLOATING_ROW_WHACK) = -1 Then

                                        iLabelCnt += 1

                                        Dim sValues() As String = Split(aryCS(ix), FLOATING_DISPLAY_DELIM)

                                        Dim trDiv As New TableRow

                                        Dim tdFlag As New TableCell
                                        tdFlag.Text = FLOATING_ROW_OUTPUT
                                        tdFlag.Width = New Unit(0)
                                        tdFlag.Visible = False
                                        trDiv.Cells.Add(tdFlag)

                                        Dim tdNum As New TableCell
                                        tdNum.Text = iLabelCnt.ToString()
                                        tdNum.HorizontalAlign = HorizontalAlign.Center
                                        trDiv.Cells.Add(tdNum)


                                        For ixInner As Integer = 0 To sFloatTypes.GetUpperBound(0)
                                            Dim td As New TableCell

                                            ' Set the conrol ID name.
                                            Dim sID As String = ""
                                            ' Note: The "control" cells start at 2, ixInner starts at 0.
                                            If bHeader Then
                                                If ixInner = CS_HEADER_TEXT_COL Then sID = CS_HEADER_TEXT + ix.ToString()
                                                If ixInner = CS_HEADER_JUSTIFICATION_COL Then sID = CS_HEADER_JUSTIFICATION + ix.ToString()
                                                If ixInner = CS_HEADER_LARGE_COL Then sID = CS_HEADER_LARGE + ix.ToString()
                                                If ixInner = CS_HEADER_BOLD_COL Then sID = CS_HEADER_BOLD + ix.ToString()
                                                If ixInner = CS_HEADER_WIDE_COL Then sID = CS_HEADER_WIDE + ix.ToString()
                                                If ixInner = CS_HEADER_HIGH_COL Then sID = CS_HEADER_HIGH + ix.ToString()
                                                If ixInner = CS_HEADER_UNDERLINE_COL Then sID = CS_HEADER_UNDERLINE + ix.ToString()
                                            Else
                                                If ixInner = CS_FOOTER_TEXT_COL Then sID = CS_FOOTER_TEXT + ix.ToString()
                                                If ixInner = CS_FOOTER_JUSTIFICATION_COL Then sID = CS_FOOTER_JUSTIFICATION + ix.ToString()
                                                If ixInner = CS_FOOTER_LARGE_COL Then sID = CS_FOOTER_LARGE + ix.ToString()
                                                If ixInner = CS_FOOTER_BOLD_COL Then sID = CS_FOOTER_BOLD + ix.ToString()
                                                If ixInner = CS_FOOTER_WIDE_COL Then sID = CS_FOOTER_WIDE + ix.ToString()
                                                If ixInner = CS_FOOTER_HIGH_COL Then sID = CS_FOOTER_HIGH + ix.ToString()
                                                If ixInner = CS_FOOTER_UNDERLINE_COL Then sID = CS_FOOTER_UNDERLINE + ix.ToString()
                                            End If

                                            Select Case sFloatTypes(ixInner)

                                                Case "textbox"
                                                    Dim txtBox As New TextBox
                                                    txtBox.ID = sID

                                                    txtBox.Text = ""
                                                    If ixInner <= sValues.GetUpperBound(0) Then
                                                        txtBox.Text = sValues(ixInner)
                                                    End If

                                                    txtBox.Width = New Unit(120)
                                                    td.Controls.Add(txtBox)

                                                Case "checkbox"
                                                    Dim chkBox As New CheckBox
                                                    chkBox.ID = sID

                                                    chkBox.Checked = False
                                                    If ixInner <= sValues.GetUpperBound(0) Then
                                                        If sValues(ixInner).ToLower() = "true" Then
                                                            chkBox.Checked = True
                                                        End If
                                                    End If

                                                    td.HorizontalAlign = HorizontalAlign.Center
                                                    td.Controls.Add(chkBox)

                                                Case "combo"
                                                    Dim cboBox As New DropDownList
                                                    cboBox.ID = sID

                                                    Dim sUseVal As String = ""
                                                    If ixInner <= sValues.GetUpperBound(0) Then
                                                        sUseVal = sValues(ixInner)
                                                    End If

                                                    cboBox.Items.Add("")

                                                    Dim sSelected As String = ""
                                                    If ixInner <= sComboValues.GetUpperBound(0) Then
                                                        Dim sComboLoad() As String = Split(sComboValues(ixInner), "|")
                                                        For ixCombo As Integer = 0 To sComboLoad.GetUpperBound(0)
                                                            cboBox.Items.Add(ComboItem(sComboLoad(ixCombo)))

                                                            If sUseVal = ComboVal(sComboLoad(ixCombo)) Then
                                                                sSelected = ComboVal(sComboLoad(ixCombo))
                                                            End If
                                                        Next
                                                    End If
                                                    cboBox.SelectedValue = sSelected

                                                    td.Controls.Add(cboBox)


                                            End Select                                      ' End: Select Case sFloatTypes(ixInner)

                                            trDiv.Cells.Add(td)

                                        Next                                     ' End: For ixInner As Integer = 0 To sFloatTypes.GetUpperBound(0)

                                        Dim tdDelete = New TableCell
                                        Dim cmdDelete As New LinkButton
                                        If bHeader Then
                                            cmdDelete.ID = CS_HEADER_DELETE_PREFIX + ix.ToString
                                        Else
                                            cmdDelete.ID = CS_FOOTER_DELETE_PREFIX + ix.ToString
                                        End If
                                        cmdDelete.Text = PadTextNBSP("Delete", 3)
                                        tdDelete.Controls.Add(cmdDelete)

                                        Dim tdMoveUp As New TableCell
                                        If ix <> iFirstGood Then
                                            Dim cmdMoveUp As New LinkButton
                                            If bHeader Then
                                                cmdMoveUp.ID = CS_HEADER_MOVEUP_PREFIX + ix.ToString
                                            Else
                                                cmdMoveUp.ID = CS_FOOTER_MOVEUP_PREFIX + ix.ToString
                                            End If
                                            cmdMoveUp.Text = PadTextNBSP("Mov &uarr;", 3)
                                            tdMoveUp.Controls.Add(cmdMoveUp)
                                        End If

                                        Dim tdMoveDown As New TableCell
                                        If ix <> iLastGood Then
                                            Dim cmdMoveDown As New LinkButton
                                            If bHeader Then
                                                cmdMoveDown.ID = CS_HEADER_MOVEDOWN_PREFIX + ix.ToString
                                            Else
                                                cmdMoveDown.ID = CS_FOOTER_MOVEDOWN_PREFIX + ix.ToString
                                            End If
                                            cmdMoveDown.Text = PadTextNBSP("Mov &darr;", 3)
                                            tdMoveDown.Controls.Add(cmdMoveDown)
                                        End If

                                        trDiv.Cells.Add(tdDelete)
                                        trDiv.Cells.Add(tdMoveUp)
                                        trDiv.Cells.Add(tdMoveDown)
                                        If bHeader Then
                                            tblCustomerSurveyHeader.Rows.Add(trDiv)
                                        Else
                                            tblCustomerSurveyFooter.Rows.Add(trDiv)
                                        End If

                                    End If                              ' End: If aryLogo(ix).IndexOf(FLOATING_ROW_WHACK) = -1 Then

                                Next                             ' End: For ix As Integer = 0 To aryPayTypes.Count - 1

                                Dim trAddExit As New TableRow
                                Dim tdAddExit As New TableCell

                                If bHeader Then
                                    tdAddExit.ColumnSpan = tblCustomerSurveyHeader.Rows(0).Cells.Count
                                Else
                                    tdAddExit.ColumnSpan = tblCustomerSurveyFooter.Rows(0).Cells.Count
                                End If
                                tdAddExit.HorizontalAlign = HorizontalAlign.Center

                                Dim cmdAddNew As New LinkButton
                                If bHeader Then
                                    cmdAddNew.ID = CS_HEADER_ADDNEW_PREFIX
                                Else
                                    cmdAddNew.ID = CS_FOOTER_ADDNEW_PREFIX
                                End If
                                'cmdAddNew.CssClass = "Button"
                                cmdAddNew.Text = PadTextNBSP("Add New", 3)

                                tdAddExit.Controls.Add(cmdAddNew)

                                Dim lit As New Literal
                                lit.Text = "&nbsp;&nbsp;&nbsp;"
                                tdAddExit.Controls.Add(lit)

                                Dim cmdExit As New LinkButton
                                If bHeader Then
                                    cmdExit.ID = CS_HEADER_EXIT_PREFIX
                                Else
                                    cmdExit.ID = CS_FOOTER_EXIT_PREFIX
                                End If

                                'cmdExit.CssClass = "Button"
                                cmdExit.Text = PadTextNBSP("Exit", 3)
                                tdAddExit.Controls.Add(cmdExit)

                                trAddExit.Cells.Add(tdAddExit)

                                If bHeader Then
                                    tblCustomerSurveyHeader.Rows.Add(trAddExit)
                                Else
                                    tblCustomerSurveyFooter.Rows.Add(trAddExit)
                                End If

                            Case "tickettype_float"

                                tr.Cells.Add(td_1)
                                td_1.Text = ixc_drowConfig(CONFIG_COL_GROUP_DISPLAY).ToString() + "&nbsp;&nbsp;"

                                If _sEvent = TICKETTYPE_FLOAT_BUTTON_PREFIX Then
                                    _bShowTicketType = True
                                ElseIf SafeSubString(_sEvent, 0, TICKETTYPE_FLOAT_ADDNEW_PREFIX.Length) = TICKETTYPE_FLOAT_ADDNEW_PREFIX Then
                                    _bShowTicketType = True
                                ElseIf SafeSubString(_sEvent, 0, TICKETTYPE_FLOAT_DELETE_PREFIX.Length) = TICKETTYPE_FLOAT_DELETE_PREFIX Then
                                    _bShowTicketType = True
                                ElseIf SafeSubString(_sEvent, 0, TICKETTYPE_FLOAT_MOVEUP_PREFIX.Length) = TICKETTYPE_FLOAT_MOVEUP_PREFIX Then
                                    _bShowTicketType = True
                                ElseIf SafeSubString(_sEvent, 0, TICKETTYPE_FLOAT_MOVEDOWN_PREFIX.Length) = TICKETTYPE_FLOAT_MOVEDOWN_PREFIX Then
                                    _bShowTicketType = True
                                End If

                                Dim td_2 As New TableCell

                                Dim cmdFloat As New LinkButton
                                cmdFloat.ID = TICKETTYPE_FLOAT_BUTTON_PREFIX
                                cmdFloat.Text = PadTextNBSP(ixc_drowConfig(CONFIG_COL_GROUP_DISPLAY).ToString(), 3)

                                divTicketType.Style.Add("display", "none")
                                cmdFloat.Visible = True

                                td_2.Controls.Add(cmdFloat)
                                td_2.Controls.Add(divTicketType)

                                tr.Cells.Add(td_2)
                                tblMain.Rows.Add(tr)

                                Dim aryTicketTypes As New ArrayList

                                Dim sFloatTypes() As String = Split(ixc_drowConfig(CONFIG_COL_FLOAT_TYPE).ToString(), FLOATING_DISPLAY_DELIM)
                                Dim sComboValues() As String = Split(ixc_drowConfig(CONFIG_COL_COMBO_VALUES).ToString(), FLOATING_DISPLAY_DELIM)
                                Dim sOpts() As String = Split(ixc_drowConfig(CONFIG_COL_OPT).ToString(), FLOATING_DISPLAY_DELIM)

                                If _bInitialLoad Then
                                    Dim bFound As Boolean = True
                                    Dim iCnt As Integer = 0
                                    Do While bFound
                                        bFound = False
                                        iCnt += 1

                                        Dim sValList As String = ""
                                        For ix As Integer = 0 To sOpts.GetUpperBound(0)
                                            Dim objKeys2(1) As Object
                                            objKeys2(0) = sOpts(ix).Replace(FLOATING_NUM_PLACEHOLDER, iCnt.ToString())
                                            objKeys2(1) = ixc_drowConfig(CONFIG_COL_TYPE).ToString()
                                            Dim drowIn2 As DataRow = _dsIn.Tables(0).Rows.Find(objKeys2)
                                            If Not drowIn2 Is Nothing Then
                                                bFound = True
                                                sValList += drowIn2(IN_COL_VALUE).ToString() + FLOATING_DISPLAY_DELIM
                                            Else
                                                sValList += FLOATING_DISPLAY_DELIM
                                            End If
                                        Next
                                        If bFound Then
                                            ' Whack trailing FLOATING_DISPLAY_DELIM
                                            sValList = sValList.Substring(0, sValList.Length - FLOATING_DISPLAY_DELIM.Length)
                                            aryTicketTypes.Add(sValList)
                                        End If
                                    Loop
                                    Session(SESSION_ARY_TICKET_TYPES) = aryTicketTypes
                                Else
                                    aryTicketTypes = CType(Session(SESSION_ARY_TICKET_TYPES), ArrayList)

                                    If _sEvent = TICKETTYPE_FLOAT_ADDNEW_PREFIX Then
                                        ' Hard-code
                                        ' A string with a bunch of FLOATING_DISPLAY_DELIM characters needs to be inserted
                                        ' into aryTicketTypes. The number of characters it the cell count of the first row
                                        ' of tblTicketType minus 4 (special columns). Hence the hard-coded 4 below.

                                        Dim iPad As Int16 = tblTicketType.Rows(0).Cells.Count - 4
                                        Dim sAdd As String = Space(iPad)
                                        aryTicketTypes.Add(sAdd.Replace(" ", FLOATING_DISPLAY_DELIM))
                                    End If

                                    If SafeSubString(_sEvent, 0, TICKETTYPE_FLOAT_DELETE_PREFIX.Length) = TICKETTYPE_FLOAT_DELETE_PREFIX Then
                                        Dim ix As Integer = Convert.ToInt32(_sEvent.Substring(TICKETTYPE_FLOAT_DELETE_PREFIX.Length))
                                        If aryTicketTypes(ix).ToString().IndexOf(FLOATING_ROW_WHACK) = -1 Then
                                            aryTicketTypes(ix) += FLOATING_ROW_WHACK
                                        End If
                                    End If

                                    Session(SESSION_ARY_TICKET_TYPES) = aryTicketTypes
                                End If


                                ' Get first and last "non-whacked" rows
                                Dim iFirstGood As Integer = -1
                                Dim iLastGood As Integer = -1
                                For ix As Integer = 0 To aryTicketTypes.Count - 1
                                    If aryTicketTypes(ix).ToString().IndexOf(FLOATING_ROW_WHACK) = -1 Then
                                        If iFirstGood = -1 Then
                                            iFirstGood = ix
                                        End If
                                        iLastGood = ix
                                    End If
                                Next

                                Dim iLabelCnt As Integer = 0
                                For ix As Integer = 0 To aryTicketTypes.Count - 1

                                    If aryTicketTypes(ix).IndexOf(FLOATING_ROW_WHACK) = -1 Then

                                        iLabelCnt += 1

                                        Dim sValues() As String = Split(aryTicketTypes(ix), FLOATING_DISPLAY_DELIM)

                                        Dim trDiv As New TableRow

                                        Dim tdFlag As New TableCell
                                        tdFlag.Text = FLOATING_ROW_OUTPUT
                                        tdFlag.Width = New Unit(0)
                                        tdFlag.Visible = False
                                        trDiv.Cells.Add(tdFlag)

                                        Dim tdNum As New TableCell
                                        tdNum.Text = iLabelCnt.ToString()
                                        tdNum.HorizontalAlign = HorizontalAlign.Center
                                        trDiv.Cells.Add(tdNum)


                                        For ixInner As Integer = 0 To sFloatTypes.GetUpperBound(0)
                                            Dim td As New TableCell

                                            ' Set the conrol ID name.
                                            Dim sID As String = ""
                                            ' Note: The "control" cells start at 2, ixInner starts at 0.
                                            If ixInner = TICKETTYPE_FLOAT_TICKETTYPE_COL Then sID = TICKETTYPE_FLOAT_TICKETTYPE + ix.ToString()
                                            If ixInner = TICKETTYPE_FLOAT_POPUP_COL Then sID = TICKETTYPE_FLOAT_POPUP + ix.ToString()
                                            If ixInner = TICKETTYPE_FLOAT_DRIVE_COL Then sID = TICKETTYPE_FLOAT_DRIVE + ix.ToString()
                                            If ixInner = TICKETTYPE_FLOAT_SCHED_PRICES_COL Then sID = TICKETTYPE_FLOAT_SCHED_PRICES + ix.ToString()
                                            If ixInner = TICKETTYPE_FLOAT_REASSIGN_COL Then sID = TICKETTYPE_FLOAT_REASSIGN + ix.ToString()
                                            If ixInner = TICKETTYPE_FLOAT_REQUIRE_COL Then sID = TICKETTYPE_FLOAT_REQUIRE + ix.ToString()
                                            If ixInner = TICKETTYPE_FLOAT_UPCHARGE_COL Then sID = TICKETTYPE_FLOAT_UPCHARGE + ix.ToString()
                                            If ixInner = TICKETTYPE_FLOAT_SCHEDULE_COL Then sID = TICKETTYPE_FLOAT_SCHEDULE + ix.ToString()
                                            If ixInner = TICKETTYPE_FLOAT_PHRASE_COL Then sID = TICKETTYPE_FLOAT_PHRASE + ix.ToString()
                                            If ixInner = TICKETTYPE_FLOAT_AUTOCOMBO_COL Then sID = TICKETTYPE_FLOAT_AUTOCOMBO + ix.ToString()
                                            If ixInner = TICKETTYPE_FLOAT_CUSTOMERSURVEY_COL Then sID = TICKETTYPE_FLOAT_CUSTOMERSURVEY + ix.ToString()
                                            If ixInner = TICKETTYPE_FLOAT_SURVEYINTERVAL_COL Then sID = TICKETTYPE_FLOAT_SURVEYINTERVAL + ix.ToString()
                                            If ixInner = TICKETTYPE_FLOAT_EXCLUDE_1_COL Then sID = TICKETTYPE_FLOAT_EXCLUDE_1 + ix.ToString()
                                            If ixInner = TICKETTYPE_FLOAT_EXCLUDE_2_COL Then sID = TICKETTYPE_FLOAT_EXCLUDE_2 + ix.ToString()
                                            If ixInner = TICKETTYPE_FLOAT_EXCLUDE_3_COL Then sID = TICKETTYPE_FLOAT_EXCLUDE_3 + ix.ToString()
                                            If ixInner = TICKETTYPE_FLOAT_EXCLUDE_4_COL Then sID = TICKETTYPE_FLOAT_EXCLUDE_4 + ix.ToString()
                                            If ixInner = TICKETTYPE_FLOAT_KDSTYPE_COL Then sID = TICKETTYPE_FLOAT_KDSTYPE + ix.ToString()
                                            If ixInner = TICKETTYPE_FLOAT_COLOR_COL Then sID = TICKETTYPE_FLOAT_COLOR + ix.ToString()
                                            If ixInner = TICKETTYPE_FLOAT_HIDDEN_COL Then sID = TICKETTYPE_FLOAT_HIDDEN + ix.ToString()
                                            If ixInner = TICKETTYPE_FLOAT_SUPPRESS_KDS_COL Then sID = TICKETTYPE_FLOAT_SUPPRESS_KDS + ix.ToString()
                                            If ixInner = TICKETTYPE_FLOAT_SUPPRESS_KP_COL Then sID = TICKETTYPE_FLOAT_SUPPRESS_KP + ix.ToString()
                                            If ixInner = TICKETTYPE_FLOAT_DSP_COL Then sID = TICKETTYPE_FLOAT_DSP + ix.ToString()

                                            Select Case sFloatTypes(ixInner)

                                                Case "textbox"
                                                    Dim txtBox As New TextBox
                                                    txtBox.ID = sID

                                                    txtBox.Text = ""
                                                    If ixInner <= sValues.GetUpperBound(0) Then
                                                        txtBox.Text = sValues(ixInner)
                                                    End If

                                                    txtBox.Width = New Unit(120)
                                                    td.Controls.Add(txtBox)

                                                Case "checkbox"
                                                    Dim chkBox As New CheckBox
                                                    chkBox.ID = sID

                                                    chkBox.Checked = True
                                                    If ixInner <= sValues.GetUpperBound(0) Then
                                                        If sValues(ixInner).ToLower() = "false" Then
                                                            chkBox.Checked = False
                                                        End If
                                                        If sValues(ixInner).ToLower() = "" Then                                                  ' Nothing written to the database yet.
                                                            chkBox.Checked = False
                                                        End If
                                                    End If

                                                    td.HorizontalAlign = HorizontalAlign.Center
                                                    td.Controls.Add(chkBox)

                                                Case "combo"
                                                    Dim cboBox As New DropDownList
                                                    cboBox.ID = sID

                                                    Dim sUseVal As String = ""
                                                    If ixInner <= sValues.GetUpperBound(0) Then
                                                        sUseVal = sValues(ixInner)
                                                    End If

                                                    'cboBox.Items.Add("")

                                                    Dim sSelected As String = ""
                                                    If ixInner <= sComboValues.GetUpperBound(0) Then
                                                        If sComboValues(ixInner).StartsWith("%") And sComboValues(ixInner).EndsWith("%") Then
                                                            Using con As New SqlConnection(New DataAccess().UnMaskString(WebConfigurationManager.ConnectionStrings("Production").ConnectionString, 1, 2, 3, 4, 5))
                                                                con.Open()
                                                                Using cmd As New SqlCommand("SELECT [Value] FROM Options_ComboValues WHERE [Key] = @KEY ORDER BY [SortKey]", con)
                                                                    cmd.Parameters.AddWithValue("@KEY", sComboValues(ixInner).Replace("%", ""))
                                                                    Using rdr As SqlDataReader = cmd.ExecuteReader
                                                                        While rdr.Read
                                                                            If sSelected = "" And sUseVal = ComboVal(rdr("Value").ToString.Trim.ToUpper) Then
                                                                                sSelected = sUseVal
                                                                            End If
                                                                            cboBox.Items.Add(ComboItem(rdr("Value").ToString))
                                                                        End While
                                                                    End Using
                                                                End Using
                                                            End Using
                                                        Else
                                                            Dim sComboLoad() As String = Split(sComboValues(ixInner), "|")
                                                            For ixCombo As Integer = 0 To sComboLoad.GetUpperBound(0)
                                                                cboBox.Items.Add(ComboItem(sComboLoad(ixCombo)))

                                                                If sUseVal = ComboVal(sComboLoad(ixCombo)) Then
                                                                    sSelected = ComboVal(sComboLoad(ixCombo))
                                                                End If
                                                            Next
                                                        End If
                                                    End If

                                                    If sSelected <> "" Then
                                                        cboBox.SelectedValue = sSelected
                                                    End If

                                                    td.Controls.Add(cboBox)


                                            End Select                                      ' End: Select Case sFloatTypes(ixInner)

                                            trDiv.Cells.Add(td)

                                        Next                                     ' End: For ixInner As Integer = 0 To sFloatTypes.GetUpperBound(0)

                                        Dim tdDelete = New TableCell
                                        Dim cmdDelete As New LinkButton
                                        cmdDelete.ID = TICKETTYPE_FLOAT_DELETE_PREFIX + ix.ToString
                                        cmdDelete.Text = PadTextNBSP("Delete", 3)
                                        tdDelete.Controls.Add(cmdDelete)

                                        Dim tdMoveUp As New TableCell
                                        If ix <> iFirstGood Then
                                            Dim cmdMoveUp As New LinkButton
                                            cmdMoveUp.ID = TICKETTYPE_FLOAT_MOVEUP_PREFIX + ix.ToString
                                            cmdMoveUp.Text = PadTextNBSP("Mov &uarr;", 3)
                                            tdMoveUp.Controls.Add(cmdMoveUp)
                                        End If

                                        Dim tdMoveDown As New TableCell
                                        If ix <> iLastGood Then
                                            Dim cmdMoveDown As New LinkButton
                                            cmdMoveDown.ID = TICKETTYPE_FLOAT_MOVEDOWN_PREFIX + ix.ToString
                                            cmdMoveDown.Text = PadTextNBSP("Mov &darr;", 3)
                                            tdMoveDown.Controls.Add(cmdMoveDown)
                                        End If

                                        trDiv.Cells.Add(tdDelete)
                                        trDiv.Cells.Add(tdMoveUp)
                                        trDiv.Cells.Add(tdMoveDown)
                                        tblTicketType.Rows.Add(trDiv)

                                    End If                              ' End: If aryTicketTypes(ix).IndexOf(FLOATING_ROW_WHACK) = -1 Then

                                Next                             ' End: For ix As Integer = 0 To aryTicketTypes.Count - 1

                                Dim trAddExit As New TableRow
                                Dim tdAddExit As New TableCell

                                tdAddExit.ColumnSpan = tblTicketType.Rows(0).Cells.Count
                                tdAddExit.HorizontalAlign = HorizontalAlign.Center

                                Dim cmdAddNew As New LinkButton
                                cmdAddNew.ID = TICKETTYPE_FLOAT_ADDNEW_PREFIX
                                'cmdAddNew.CssClass = "Button"
                                cmdAddNew.Text = PadTextNBSP("Add New", 3)

                                tdAddExit.Controls.Add(cmdAddNew)

                                Dim lit As New Literal
                                lit.Text = "&nbsp;&nbsp;&nbsp;"
                                tdAddExit.Controls.Add(lit)

                                Dim cmdExit As New LinkButton
                                cmdExit.ID = TICKETTYPE_FLOAT_EXIT_PREFIX
                                'cmdExit.CssClass = "Button"
                                cmdExit.Text = PadTextNBSP("Exit", 3)
                                tdAddExit.Controls.Add(cmdExit)

                                trAddExit.Cells.Add(tdAddExit)

                                tblTicketType.Rows.Add(trAddExit)


                        End Select                   ' End: Select Case ixc_drowConfig(CONFIG_COL_CONTROL_TYPE).ToString()

                    End If  '  End: If ixc_drowConfig(CONFIG_COL_CONTROL_TYPE).ToString() = "" Then

                Next  ' End: For Each ixc_drowConfig As DataRow In _dsConfig.Tables(0).Rows

                ' Load rows into the floating tables.

                ' aryFloatTables holds a list of table name post fixes concatenated with text to display.
                ' The table name corresponds to a floating table created above. 
                For Each ixc_sItem As String In aryFloatTables

                    Dim sTmp As String = ixc_sItem
                    Dim sTablePostFix As String = sTmp.Substring(0, sTmp.IndexOf("|"))
                    sTmp = sTmp.Substring(sTmp.IndexOf("|") + 1)
                    Dim sDisplay As String = sTmp.Substring(0, sTmp.IndexOf("|"))
                    sTmp = sTmp.Substring(sTmp.IndexOf("|") + 1)
                    Dim sControlType As String = sTmp.Substring(0, sTmp.IndexOf("|"))
                    Dim sComboVals As String = sTmp.Substring(sTmp.IndexOf("|") + 1)

                    ' Find the table relating to this postfix.
                    Dim tbl As Table = FindControl(FLOATING_TABLE_PREFIX + sTablePostFix)

                    If Not tbl Is Nothing Then

                        ' Get the "opt" value and "type" value that correspond to this postfix from the config file.
                        Dim sOpt As String = ""
                        Dim sType As String = ""
                        _dsConfig.Tables(0).DefaultView.RowFilter = CONFIG_COL_CONTROL_POSTFIX + " = '" + sTablePostFix + "'"
                        For Each ixc_dvRow As DataRowView In _dsConfig.Tables(0).DefaultView
                            sOpt = ixc_dvRow(CONFIG_COL_OPT).ToString
                            sType = ixc_dvRow(CONFIG_COL_TYPE).ToString
                            Exit For ' Only the first one is needed.
                        Next

                        Dim iRowCnt As Integer = 0

                        ' If this is the initial page load, a row is added to the floating table for each
                        ' value in the Options file (_dsIn). It is assumed that the opt field in the options file
                        ' will be named xxx1, xxx2, xxx3, ... xxxn.
                        If _bInitialLoad Then

                            Dim bInRecFound = True
                            Dim iOptNumber As Integer = 0
                            Do While bInRecFound
                                iOptNumber += 1

                                Dim sValue As String = GetOptionValue(sOpt.Replace(FLOATING_NUM_PLACEHOLDER, iOptNumber.ToString), sType)

                                If sValue <> "" Then
                                    iRowCnt += 1
                                    Dim tr As TableRow = CreateFloatingTableRow(sTablePostFix, _
                                                                                    sDisplay, _
                                                                                    sControlType, _
                                                                                    sComboVals, _
                                                                                    iRowCnt.ToString, _
                                                                                    iRowCnt.ToString, _
                                                                                    sValue)
                                    tbl.Rows.AddAt(tbl.Rows.Count - 1, tr)

                                    ' Set the only cell in the last table row (the button row) to span the table.
                                    tbl.Rows(tbl.Rows.Count - 1).Cells(0).ColumnSpan = tr.Cells.Count

                                    ' _aryFloatRows keeps track of the rows that have been added to floating tables.
                                    _aryFloatRows.Add(sTablePostFix + "|" + _
                                                        iRowCnt.ToString + "|" + sValue)
                                Else
                                    bInRecFound = False ' No more records have been found. End loop iteration.
                                End If
                            Loop
                        Else
                            ' If this is the initial page load, a row is added to the floating table for each
                            ' value in the Options file (_dsIn). It is assumed that the opt field in the options file
                            ' will be named xxx1, xxx2, xxx3, ... xxxn.
                            For ix As Integer = 0 To _aryFloatRows.Count - 1
                                Dim sRowPostFix As String = _aryFloatRows(ix)
                                If SafeSubString(sRowPostFix, 0, sTablePostFix.Length) = sTablePostFix Then

                                    ' If a row has been "deleted," FLOATING_ROW_WHACK was added to the text corresponding
                                    ' to that row in _aryFloatRows.
                                    If sRowPostFix.IndexOf(FLOATING_ROW_WHACK) = -1 Then

                                        ' _aryFloatRows entries are in the form: "post fix text|row number|value"

                                        Dim sNewPostFix = sRowPostFix.Substring(0, sRowPostFix.IndexOf("|"))
                                        sRowPostFix = sRowPostFix.Substring(sRowPostFix.IndexOf("|") + 1)

                                        Dim sRowCnt = sRowPostFix.Substring(0, sRowPostFix.IndexOf("|"))
                                        sRowPostFix = sRowPostFix.Substring(sRowPostFix.IndexOf("|") + 1)

                                        Dim sVal = sRowPostFix

                                        iRowCnt += 1
                                        Dim tr As TableRow = CreateFloatingTableRow(sNewPostFix, sDisplay, _
                                                                                        sControlType, sComboVals, _
                                                                                        iRowCnt.ToString, sRowCnt, sVal)

                                        tbl.Rows.AddAt(tbl.Rows.Count - 1, tr)

                                        ' Set the only cell in the last table row (the button row) to span the table.
                                        tbl.Rows(tbl.Rows.Count - 1).Cells(0).ColumnSpan = tr.Cells.Count

                                    End If
                                End If
                            Next

                        End If  ' End: If _bInitialLoad Then

                        ' Hard-Code: Add a link to an exchange rate site.
                        If sTablePostFix = "AltCashType" Then
                            Dim tdLink As New TableCell
                            Dim trLink As New TableRow

                            Dim cmdExchange As New LinkButton
                            cmdExchange.Text = PadTextNBSP("Click to see current exchange rates", 3)
                            cmdExchange.Attributes.Add("onclick", "javascript:window.open('" + hidExchangeRate.Value.ToString() + "');window.event.returnValue = false;")

                            tdLink.Controls.Add(cmdExchange)
                            'tdLink.Text = "Click to see current exchange rates"
                            'tdLink.Attributes.Add("onclick", "javascript:window.open('" + hidExchangeRate.Value.ToString() + "');")

                            tdLink.ColumnSpan = tbl.Rows(0).Cells.Count
                            tdLink.VerticalAlign = VerticalAlign.Middle
                            tdLink.HorizontalAlign = HorizontalAlign.Center
                            tdLink.Height = New Unit(DEFAULT_ROW_HEIGHT * 2)

                            'tdLink.Font.Underline = True
                            'tdLink.ForeColor = Color.Blue

                            trLink.Cells.Add(tdLink)
                            tbl.Rows.Add(trLink)
                        End If

                    End If  ' End:  If Not tbl Is Nothing Then
                Next

                Session(SESSION_FLOAT_ROWS) = _aryFloatRows

            Catch ex As Exception
                HandleError(ex)
            End Try

        End Sub

#End Region

#Region " Event Methods "

        Private Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click
            SaveData()
        End Sub

        Protected Sub cmdSaveClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSaveClose.Click

            Try
                If SaveData() Then
                    CloseWindow()
                End If

            Catch ex As Exception
                HandleError(ex)
            End Try

        End Sub

        Private Sub cmdCancelClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelClose.Click
            CloseWindow()
        End Sub

        Private Sub cboRegister_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboRegister.SelectedIndexChanged
            Try
                'If Not SaveData() Then
                '    Exit Sub
                'End If

                Session(SESSION_FLOAT_ROWS) = ""
                Session(SESSION_CURRENT_TAB) = ""
                Session(SESSION_OPTIONS_REGISTER) = cboRegister.SelectedValue

                Dim sb As New StringBuilder
                sb.Append("<script>" + vbCrLf)
                sb.Append("window.opener = self;" + vbCrLf)
                sb.Append("window.close();" + vbCrLf)
                sb.Append("window.open('RegisterOptions.aspx', '', " + GetWindowOptionString() + ");" + vbCrLf)
                sb.Append("</script>")

                RegisterClientScriptBlock("closeOpenWindow", sb.ToString())

            Catch ex As Exception
                HandleError(ex)
            End Try
        End Sub

        Private Sub cmdCopyRegisterOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCopyRegisterOK.Click
            Try
                Dim iSource As Integer = 0, iTarget As Integer = 0

                Try
                    iSource = Convert.ToInt32(txtSourceRegister.Text)
                Catch ex As Exception
                    txtTargetRegister.Width = New Unit(120)
                    txtSourceRegister.Width = New Unit(120)
                    txtSourceRegister.Text = "Enter valid number"
                    Exit Sub
                End Try

                Try
                    iTarget = Convert.ToInt32(txtTargetRegister.Text)
                Catch ex As Exception
                    txtSourceRegister.Width = New Unit(120)
                    txtTargetRegister.Width = New Unit(120)
                    txtTargetRegister.Text = "Enter valid number"
                    Exit Sub
                End Try

                'If Not SaveData() Then
                '    Exit Sub
                'End If

                Session(SESSION_FLOAT_ROWS) = ""
                Session(SESSION_CURRENT_TAB) = ""
                Session(SESSION_OPTIONS_SOURCE_REGISTER) = iSource
                Session(SESSION_OPTIONS_REGISTER) = iTarget

                Dim sb As New StringBuilder
                sb.Append("<script>" + vbCrLf)
                sb.Append("window.opener = self;" + vbCrLf)
                sb.Append("window.close();" + vbCrLf)
                sb.Append("window.open('RegisterOptions.aspx', '', " + GetWindowOptionString() + ");" + vbCrLf)
                sb.Append("</script>")

                RegisterClientScriptBlock("closeOpenWindow", sb.ToString())

            Catch ex As Exception
                HandleError(ex)
            End Try
        End Sub

#End Region

#Region " Utility Methods "

        Protected Function CreateFloatingTable(ByVal pv_sCtlPostfix As String) As Table

            Dim tblNew As New Table
            tblNew.ID = FLOATING_TABLE_PREFIX + pv_sCtlPostfix
            tblNew.BorderStyle = BorderStyle.Ridge
            tblNew.BackColor = Color.LightBlue

            Try

                ' Create a row to contain add new and exit buttons and add the row to the table.

                Dim trAddExit As New TableRow
                trAddExit.Height = New Unit(DEFAULT_ROW_HEIGHT)
                Dim tdAddExit As New TableCell
                tdAddExit.HorizontalAlign = HorizontalAlign.Center

                Dim cmdAddNew As New LinkButton
                cmdAddNew.ID = FLOATING_ADDNEW_PREFIX + pv_sCtlPostfix
                'cmdAddNew.CssClass = "Button"
                cmdAddNew.Text = PadTextNBSP("Add New", 3)

                tdAddExit.Controls.Add(cmdAddNew)

                Dim lit As New Literal
                lit.Text = "&nbsp;&nbsp;&nbsp;"
                tdAddExit.Controls.Add(lit)

                Dim cmdExit As New LinkButton
                cmdExit.ID = FLOATING_EXIT_PREFIX + pv_sCtlPostfix
                'cmdExit.CssClass = "Button"
                cmdExit.Text = PadTextNBSP("Exit", 3)
                tdAddExit.Controls.Add(cmdExit)

                trAddExit.Cells.Add(tdAddExit)

                tblNew.Rows.Add(trAddExit)

                Return tblNew

            Catch ex As Exception
                HandleError(ex)
            End Try

        End Function

        Protected Function CreateFloatingTableRow(ByVal pv_sCtlPostfix As String, _
                                                    ByVal pv_sDisplay As String, _
                                                    ByVal pv_sControlType As String, _
                                                    ByVal pv_sComboVals As String, _
                                                    ByVal pv_sRowNumDisplay As String, _
                                                    ByVal pv_sRowCnt As String, _
                                                    ByVal pv_sValue As String) As TableRow

            ' Create a row and insert display cells, textbox cells, and a delete button cell.
            Dim trDisplay As New TableRow
            trDisplay.ID = pv_sCtlPostfix + "|" + pv_sRowCnt

            Try
                Dim tdInfo As New TableCell
                tdInfo.Text = FLOATING_ROW_OUTPUT
                tdInfo.Width = New Unit(0)
                tdInfo.Visible = False
                trDisplay.Cells.Add(tdInfo)

                Dim sDisp() As String = Split(pv_sDisplay, FLOATING_DISPLAY_DELIM)
                Dim sValue() As String = Split(pv_sValue, FLOATING_DISPLAY_DELIM)
                Dim sControlType() As String = Split(pv_sControlType, FLOATING_DISPLAY_DELIM)
                Dim sComboOptions() As String = Split(pv_sComboVals, FLOATING_DISPLAY_DELIM)

                For ix As Integer = 0 To sDisp.GetUpperBound(0)
                    Dim tdDisplay As New TableCell
                    Dim tdText As New TableCell

                    tdDisplay.Text = sDisp(ix) + " " + pv_sRowNumDisplay + "&nbsp;&nbsp;"
                    tdDisplay.Font.Size = FontUnit.Point(9)
                    trDisplay.Cells.Add(tdDisplay)

                    If sControlType(ix) = "textbox" Then
                        Dim txtBox As New TextBox
                        txtBox.ID = FLOATING_TEXTBOX_PREFIX + pv_sCtlPostfix + "|" + pv_sRowCnt + "col" + ix.ToString
                        If ix <= sValue.GetUpperBound(0) Then
                            txtBox.Text = sValue(ix)
                        Else
                            txtBox.Text = ""
                        End If
                        tdText.Controls.Add(txtBox)
                    ElseIf sControlType(ix) = "combo" Then
                        Dim cboBox As New DropDownList
                        cboBox.ID = FLOATING_TEXTBOX_PREFIX + pv_sCtlPostfix + "|" + pv_sRowCnt + "col" + ix.ToString

                        Dim sUseVal As String = ""
                        If ix <= sValue.GetUpperBound(0) Then
                            sUseVal = sValue(ix)
                        End If

                        cboBox.Items.Add("")
                        Dim sComboSet() As String = Split(sComboOptions(ix), "|")
                        Dim sSelected As String = ""
                        For ix2 As Integer = 0 To sComboSet.GetUpperBound(0)
                            If sUseVal = sComboSet(ix2) Then
                                sSelected = sUseVal
                            End If
                            cboBox.Items.Add(sComboSet(ix2))
                        Next
                        If sSelected <> "" Then
                            cboBox.SelectedValue = sSelected
                        End If

                        tdText.Controls.Add(cboBox)
                    End If

                    trDisplay.Cells.Add(tdText)
                Next

                Dim tdCmd As New TableCell
                Dim cmdDelete As New LinkButton
                cmdDelete.ID = FLOATING_DELETE_PREFIX + pv_sCtlPostfix + "|" + pv_sRowCnt
                'cmdDelete.CssClass = "Button"
                cmdDelete.Text = PadTextNBSP("Delete", 2)
                tdCmd.Controls.Add(cmdDelete)

                trDisplay.Cells.Add(tdCmd)

                Return trDisplay

            Catch ex As Exception
                HandleError(ex)
            End Try

        End Function

        Protected Function LoadDataSets() As Boolean
            Try
                Dim sStore As String = ""
                If Not Request.Form("STORE") Is Nothing Then
                    sStore = Request.Form("STORE").ToString()
                    Session(SESSION_OPTIONS_STORE) = Request.Form("STORE").ToString()
                ElseIf Not Session(SESSION_OPTIONS_STORE) Is Nothing Then
                    sStore = Session(SESSION_OPTIONS_STORE).ToString()
                End If

                If sStore = "" Then
                    If Request("HTTP_REFERER") Is Nothing And Request("SERVER_NAME") = "localhost" Then  ' Local machine test stuff.
                        sStore = "8888"
                        'sStore = "7777"
                        'sStore = -1
                        Session(SESSION_OPTIONS_STORE) = sStore
                    Else
                        Dim td As New TableCell
                        td.Font.Size = FontUnit.Point(14)
                        td.Text = "No store selected."
                        Dim tr As New TableRow
                        tr.Cells.Add(td)
                        tblMain.Rows.Add(tr)
                        Return False
                    End If
                End If

                lblStoreNum.Text = sStore

                _dsIn = Nothing
                _dsIn = New DataSet

                Dim sSourceRegister As String = ""
                If Not Session(SESSION_OPTIONS_SOURCE_REGISTER) Is Nothing Then
                    If Session(SESSION_OPTIONS_SOURCE_REGISTER).ToString() <> "" Then
                        sSourceRegister = Session(SESSION_OPTIONS_SOURCE_REGISTER).ToString()
                    End If
                End If

                If Not Session(SESSION_OPTIONS_REGISTER) Is Nothing Then
                    If Session(SESSION_OPTIONS_REGISTER).ToString() <> "" Then
                        _sRegister = Session(SESSION_OPTIONS_REGISTER).ToString()
                    Else
                        _sRegister = "1"
                        Session(SESSION_OPTIONS_REGISTER) = "1"
                    End If
                Else
                    _sRegister = "1"
                    Session(SESSION_OPTIONS_REGISTER) = "1"
                End If

                ' Reset these values. They won't be needed again.
                Session(SESSION_OPTIONS_SOURCE_REGISTER) = ""
                Session(SESSION_OPTIONS_REGISTER) = ""

                If Not IsPostBack Then

                    Dim sHost As String = Request("HTTP_HOST").ToString()
                    'sHost = "www.subshop.com"
                    ' For testing

                    Dim webService As New WebService.Exchange
                    webService.Url = "http://" + sHost + "/WebService/Exchange.asmx"
                    If sHost <> "localhost" Then
                        Dim proxyObject As New WebProxy("http://" + sHost + "/WebService:80", True)
                        webService.Proxy = proxyObject
                    End If

                    If sSourceRegister = "" Then
                        _dsIn = webService.downloadDataSet("INFOPOS_OPTIONS:" + sStore + "," + _sRegister)
                    Else
                        _dsIn = webService.downloadDataSet("INFOPOS_OPTIONS:" + sStore + "," + sSourceRegister)

                        ' Hard-code garbage
                        ' Because the web service doesn't return store and register, c
                        ' reate new dataset, with store to send to the update.

                        Dim dtbl As New DataTable

                        Dim dcol0 As New DataColumn
                        dcol0.ColumnName = "store"
                        dcol0.DataType = Type.GetType("System.String")
                        dtbl.Columns.Add(dcol0)

                        Dim dcol1 As New DataColumn
                        dcol1.ColumnName = "register"
                        dcol1.DataType = Type.GetType("System.String")
                        dtbl.Columns.Add(dcol1)

                        For Each ixc_dcol As DataColumn In _dsIn.Tables(0).Columns
                            Dim dcol As New DataColumn
                            dcol.ColumnName = ixc_dcol.ColumnName
                            dcol.DataType = ixc_dcol.DataType
                            dtbl.Columns.Add(dcol)
                        Next

                        ' Load new DataSet.
                        For Each ixc_drow As DataRow In _dsIn.Tables(0).Rows
                            Dim drowNew = dtbl.NewRow()
                            drowNew("store") = sStore
                            drowNew("register") = _sRegister
                            For Each ixc_dcol As DataColumn In _dsIn.Tables(0).Columns
                                drowNew(ixc_dcol.ColumnName) = ixc_drow(ixc_dcol.ColumnName)
                            Next
                            dtbl.Rows.Add(drowNew)
                        Next

                        Dim dsUpdate As New DataSet
                        dsUpdate.Tables.Add(dtbl)

                        webService.uploadDataSet(dsUpdate, "INFOPOS_OPTIONS")
                    End If
                    Session(SESSION_STORE_DATA) = _dsIn
                Else
                    _dsIn = CType(Session(SESSION_STORE_DATA), DataSet)
                End If

                _dsConfig = Nothing
                _dsConfig = New DataSet

                _dsConfig.ReadXml(Server.MapPath(hidConfig.Value.ToString()))

                Dim dcolInKey(1) As DataColumn
                dcolInKey(0) = _dsIn.Tables(0).Columns(IN_COL_OPT)
                dcolInKey(1) = _dsIn.Tables(0).Columns(IN_COL_TYPE)
                _dsIn.Tables(0).PrimaryKey = dcolInKey

                Dim dcolConfigKey(1) As DataColumn
                dcolConfigKey(0) = _dsConfig.Tables(0).Columns(CONFIG_COL_OPT)
                dcolConfigKey(1) = _dsConfig.Tables(0).Columns(CONFIG_COL_TYPE)
                _dsConfig.Tables(0).PrimaryKey = dcolConfigKey

                Return True

            Catch ex As Exception
                HandleError(ex)
            End Try

        End Function

        Protected Function SaveData() As Boolean
            Try
                LoadDataSets()

                Dim dsOutPut As New DataSet
                Dim dtbl As New DataTable

                ' Hard-code

                Dim dcolNew0 As New DataColumn
                dcolNew0.ColumnName = "store"
                dcolNew0.DataType = Type.GetType("System.String")
                dtbl.Columns.Add(dcolNew0)

                Dim dcolNew1 As New DataColumn
                dcolNew1.ColumnName = "register"
                dcolNew1.DataType = Type.GetType("System.String")
                dtbl.Columns.Add(dcolNew1)

                For Each ixc_dcol As DataColumn In _dsIn.Tables(0).Columns
                    Dim dcolNew As New DataColumn
                    dcolNew.ColumnName = ixc_dcol.ColumnName
                    dcolNew.DataType = ixc_dcol.DataType
                    dtbl.Columns.Add(dcolNew)
                Next
                dsOutPut.Tables.Add(dtbl)

                For Each ixc_drowConfig As DataRow In _dsConfig.Tables(0).Rows

                    Dim bFound As Boolean = False

                    ' Try to find a "regular" control

                    If FindControl(ixc_drowConfig(CONFIG_COL_OPT).ToString() & ixc_drowConfig(CONFIG_COL_TYPE).ToString()) IsNot Nothing Then
                        Dim sVal As String = ""

                        Dim drowInsert As DataRow = dsOutPut.Tables(0).NewRow
                        drowInsert(IN_COL_TYPE) = ixc_drowConfig(CONFIG_COL_TYPE).ToString()
                        drowInsert(IN_COL_OPT) = ixc_drowConfig(CONFIG_COL_OPT).ToString()

                        bFound = True

                        Select Case ixc_drowConfig(CONFIG_COL_CONTROL_TYPE).ToString()

                            Case "textbox", "password"
                                Dim txtBox As TextBox = FindControl(ixc_drowConfig(CONFIG_COL_OPT).ToString() +
                                            ixc_drowConfig(CONFIG_COL_TYPE).ToString())

                                If ixc_drowConfig(CONFIG_COL_CONTROL_TYPE).ToString() = "password" Then
                                    'sVal = Encode(txtBox.Text)
                                    sVal = txtBox.Text
                                Else
                                    sVal = txtBox.Text
                                End If
                            Case "checkbox"
                                Dim chkBox As CheckBox = FindControl(ixc_drowConfig(CONFIG_COL_OPT).ToString() +
                                            ixc_drowConfig(CONFIG_COL_TYPE).ToString())
                                sVal = chkBox.Checked.ToString()

                            Case "combo"
                                Dim cboBox As DropDownList = FindControl(ixc_drowConfig(CONFIG_COL_OPT).ToString() +
                                            ixc_drowConfig(CONFIG_COL_TYPE).ToString())

                                sVal = cboBox.SelectedValue

                                Dim sTime As String = ixc_drowConfig(CONFIG_COL_OPT).ToString() + "|" + ixc_drowConfig(CONFIG_COL_TYPE).ToString()

                                ' Hard-code
                                If sTime = "AutoprintTime|Printing" Then
                                    Dim iPos As Integer = sVal.IndexOf(":")
                                    If iPos > 0 Then
                                        sVal = sVal.Substring(0, iPos) + sVal.Substring(iPos + 1)
                                    End If
                                End If

                        End Select

                        ' Validata data.
                        If ixc_drowConfig(CONFIG_COL_DATA_VALIDATION).ToString() <> "" Then
                            Dim sErr As String = ValidateData(sVal, ixc_drowConfig(CONFIG_COL_DATA_VALIDATION).ToString(),
                                    ixc_drowConfig(CONFIG_COL_DISPLAY).ToString())
                            If sErr <> "" Then
                                Dim sCSB As String = "<script>"
                                sCSB += "alert('" & sErr & "');"
                                sCSB += "</script>"
                                RegisterClientScriptBlock("dataValidatationError", sCSB)
                                Return False
                            End If
                        End If

                        'manipulate tipmargin value from integer to decimal
                        If ixc_drowConfig(CONFIG_COL_OPT).ToString().ToLower = "tipmargin" Then
                            Dim txtBoxTM As TextBox = FindControl(ixc_drowConfig(CONFIG_COL_OPT).ToString() +
                                        ixc_drowConfig(CONFIG_COL_TYPE).ToString())

                            If txtBoxTM.Text.Length > 0 Then
                                sVal = (txtBoxTM.Text / 100).ToString
                            End If
                        End If

                        drowInsert(IN_COL_VALUE) = sVal

                        PadDataRow(drowInsert)
                        dsOutPut.Tables(0).Rows.Add(drowInsert)

                    End If

                    ' If the "regular" control wasn't found, try to find a "float" control.

                    If Not bFound Then

                        If FindControl(FLOATING_TABLE_PREFIX & ixc_drowConfig(CONFIG_COL_CONTROL_POSTFIX).ToString()) IsNot Nothing Then

                            bFound = True

                            Dim tbl As Table = FindControl(FLOATING_TABLE_PREFIX + ixc_drowConfig(CONFIG_COL_CONTROL_POSTFIX).ToString())

                            Dim iCnt As Integer = 0
                            For Each ixc_tr As TableRow In tbl.Rows
                                If ixc_tr.Cells(0).Text = FLOATING_ROW_OUTPUT Then

                                    iCnt += 1

                                    If ixc_drowConfig(CONFIG_COL_EXTRA_OUTPUT).ToString() <> "" Then
                                        Dim drowExtra As DataRow = dsOutPut.Tables(0).NewRow
                                        drowExtra(IN_COL_TYPE) = ixc_drowConfig(CONFIG_COL_TYPE).ToString()
                                        drowExtra(IN_COL_OPT) = ixc_drowConfig(CONFIG_COL_EXTRA_OUTPUT).ToString().Replace(FLOATING_NUM_PLACEHOLDER, iCnt.ToString())
                                        drowExtra(IN_COL_VALUE) = ixc_drowConfig(CONFIG_COL_EXTRA_OUTPUT_VALUE).ToString()
                                        PadDataRow(drowExtra)
                                        dsOutPut.Tables(0).Rows.Add(drowExtra)
                                    End If

                                    Dim sOptString As String = ixc_drowConfig(CONFIG_COL_OPT).ToString().Replace(FLOATING_NUM_PLACEHOLDER, iCnt.ToString())
                                    Dim sValidateString As String = ixc_drowConfig(CONFIG_COL_DATA_VALIDATION).ToString()

                                    For Each td As TableCell In ixc_tr.Cells
                                        For Each objCtl As Object In td.Controls
                                            Dim sValue As String = GetControlValue(objCtl)

                                            If sValue <> NULL_CONTROL Then

                                                Dim sCtlID As String = GetControlID(objCtl)
                                                Dim sOpt As String = ""
                                                If sOptString.IndexOf(FLOATING_DISPLAY_DELIM) > -1 Then
                                                    sOpt = sOptString.Substring(0, sOptString.IndexOf(FLOATING_DISPLAY_DELIM))
                                                    sOptString = sOptString.Substring(sOptString.IndexOf(FLOATING_DISPLAY_DELIM) + FLOATING_DISPLAY_DELIM.Length)
                                                Else
                                                    sOpt = sOptString
                                                End If

                                                Dim sValidate As String
                                                If sValidateString.IndexOf(FLOATING_DISPLAY_DELIM) > -1 Then
                                                    sValidate = sValidateString.Substring(0, sValidateString.IndexOf(FLOATING_DISPLAY_DELIM))
                                                    sValidateString = sValidateString.Substring(sValidateString.IndexOf(FLOATING_DISPLAY_DELIM) + FLOATING_DISPLAY_DELIM.Length)
                                                Else
                                                    sValidate = sValidateString
                                                End If


                                                ' Validata data.
                                                If sValidate <> "" Then
                                                    Dim sErr As String = ValidateData(sValue, sValidate, sOpt)
                                                    If sErr <> "" Then
                                                        Dim sCSB As String = "<script>"
                                                        sCSB += "alert('" & sErr & "');"
                                                        sCSB += "</script>"
                                                        RegisterClientScriptBlock("dataValidatationError", sCSB)
                                                        Return False
                                                    End If
                                                End If

                                                If sCtlID.IndexOf(FLOATING_TEXTBOX_PREFIX) > -1 Then
                                                    If sValue <> "" Then
                                                        Dim drowInsert As DataRow = dsOutPut.Tables(0).NewRow
                                                        drowInsert(IN_COL_TYPE) = ixc_drowConfig(CONFIG_COL_TYPE).ToString()
                                                        drowInsert(IN_COL_OPT) = sOpt
                                                        drowInsert(IN_COL_VALUE) = sValue
                                                        PadDataRow(drowInsert)
                                                        dsOutPut.Tables(0).Rows.Add(drowInsert)
                                                    End If
                                                End If

                                            End If

                                        Next
                                    Next
                                End If

                            Next

                        End If  ' End: If Not FindControl(ixc_drowConfig(CONFIG_COL_OPT).ToString() + _ ...

                    End If  ' End: If Not bFound Then  

                    ' A bunch of hard-coding for Display and Print header.

                    If Not bFound Then

                        If ixc_drowConfig(CONFIG_COL_CONTROL_TYPE).ToString() = "header_float" Then
                            bFound = True
                            Dim sOpt() As String = Split(ixc_drowConfig(CONFIG_COL_OPT), FLOATING_DISPLAY_DELIM)

                            Dim sDisplayOrPrint As String = ""
                            Dim tbl As Table
                            If ixc_drowConfig(CONFIG_COL_CONTROL_POSTFIX) = HEADER_FLOAT_DISPLAY_POSTFIX Then
                                sDisplayOrPrint = HEADER_FLOAT_DISPLAY_POSTFIX
                                tbl = tblDisplayHeader
                            ElseIf ixc_drowConfig(CONFIG_COL_CONTROL_POSTFIX) = HEADER_FLOAT_PRINT_POSTFIX Then
                                sDisplayOrPrint = HEADER_FLOAT_PRINT_POSTFIX
                                tbl = tblPrintHeader
                            End If

                            ' Get the "exclusive" dropdown items. The items can't appear on the same line on a ticket
                            ' and are flagged with the constant EXCLUSIVE_FLAG.
                            Dim sComboValues() As String = Split(ixc_drowConfig(CONFIG_COL_COMBO_VALUES).ToString(), FLOATING_DISPLAY_DELIM)
                            Dim sExcl() As String = Split(sComboValues(0).ToString(), "|")
                            Dim aryExclusive As New ArrayList

                            For ix As Integer = 0 To sExcl.GetUpperBound(0)
                                If sExcl(ix).IndexOf(EXCLUSIVE_FLAG) <> -1 Then
                                    aryExclusive.Add(sExcl(ix).Substring(0, sExcl(ix).IndexOf(EXCLUSIVE_FLAG)))
                                End If
                            Next

                            Dim iOutputNum As Integer = 0
                            For ix As Integer = 0 To tbl.Rows.Count - 1

                                If tbl.Rows(ix).Cells(0).Text = FLOATING_ROW_OUTPUT Then

                                    Dim tdLeft As TableCell = tbl.Rows(ix).Cells(2) ' Hard-code cell-num
                                    Dim tdRight As TableCell = tbl.Rows(ix).Cells(4) ' Hard-code cell-num

                                    Dim sValueLeft As String = ""
                                    Dim sValueRight As String = ""

                                    If Not tdLeft.Controls(0) Is Nothing Then
                                        sValueLeft = GetControlValue(tdLeft.Controls(0)) ' There should only be one control (0).
                                    End If

                                    If Not tdRight.Controls(0) Is Nothing Then
                                        sValueRight = GetControlValue(tdRight.Controls(0)) ' There should only be one control (0).
                                    End If

                                    If aryExclusive.Contains(sValueLeft) And aryExclusive.Contains(sValueRight) Then
                                        Dim sCSB As String = "<script>"
                                        If sDisplayOrPrint = HEADER_FLOAT_DISPLAY_POSTFIX Then
                                            sCSB += "alert('Maximum width exceeded on Display Header:\r\n"
                                        ElseIf sDisplayOrPrint = HEADER_FLOAT_PRINT_POSTFIX Then
                                            sCSB += "alert('Maximum width exceeded on Print Header:\r\n"
                                        End If
                                        sCSB += "You cannot have " + sValueLeft + " and " + sValueRight + " on the same header line." + "'); "
                                        sCSB += "</script>"
                                        RegisterClientScriptBlock("dataValidatationError", sCSB)
                                        Return False
                                    End If

                                    If sValueLeft <> "" Or sValueRight <> "" Then
                                        iOutputNum += 1

                                        Dim sVal As String = ""
                                        Dim drowInsert As DataRow = dsOutPut.Tables(0).NewRow
                                        drowInsert(IN_COL_TYPE) = ixc_drowConfig(CONFIG_COL_TYPE).ToString()
                                        drowInsert(IN_COL_OPT) =
                                                ixc_drowConfig(CONFIG_COL_EXTRA_OUTPUT).ToString().Replace(FLOATING_NUM_PLACEHOLDER, iOutputNum.ToString())
                                        drowInsert(IN_COL_VALUE) = "True"
                                        PadDataRow(drowInsert)
                                        dsOutPut.Tables(0).Rows.Add(drowInsert)

                                        'for backwards compatibility to pre InfoPOS v 1.1.9
                                        'save setting again in old format
                                        If Not ixc_drowConfig(CONFIG_COL_EXTRA_OUTPUT).ToString() = "Header:!:" Then
                                            Dim drowInsert2 As DataRow = dsOutPut.Tables(0).NewRow
                                            drowInsert2(IN_COL_TYPE) = ixc_drowConfig(CONFIG_COL_TYPE).ToString()
                                            drowInsert2(IN_COL_OPT) = "Header" + iOutputNum.ToString()
                                            drowInsert2(IN_COL_VALUE) = "True"
                                            PadDataRow(drowInsert2)
                                            dsOutPut.Tables(0).Rows.Add(drowInsert2)
                                        End If


                                        If sValueLeft <> "" Then
                                            Dim drowInsertL As DataRow = dsOutPut.Tables(0).NewRow
                                            drowInsertL(IN_COL_TYPE) = ixc_drowConfig(CONFIG_COL_TYPE).ToString()
                                            drowInsertL(IN_COL_OPT) =
                                                         ixc_drowConfig(CONFIG_COL_EXTRA_OUTPUT).ToString().Replace(FLOATING_NUM_PLACEHOLDER, iOutputNum.ToString()) _
                                                         + "Left"
                                            drowInsertL(IN_COL_VALUE) = sValueLeft
                                            PadDataRow(drowInsertL)
                                            dsOutPut.Tables(0).Rows.Add(drowInsertL)

                                            'for backwards compatibility to pre InfoPOS v 1.1.9
                                            'save setting again in old format
                                            If Not ixc_drowConfig(CONFIG_COL_EXTRA_OUTPUT).ToString() = "Header:!:" Then
                                                Dim drowInsertL2 As DataRow = dsOutPut.Tables(0).NewRow
                                                drowInsertL2(IN_COL_TYPE) = ixc_drowConfig(CONFIG_COL_TYPE).ToString()
                                                drowInsertL2(IN_COL_OPT) = "Header" + iOutputNum.ToString() + "Left"
                                                drowInsertL2(IN_COL_VALUE) = sValueLeft
                                                PadDataRow(drowInsertL2)
                                                dsOutPut.Tables(0).Rows.Add(drowInsertL2)
                                            End If
                                        End If

                                        If sValueRight <> "" Then
                                            Dim drowInsertR As DataRow = dsOutPut.Tables(0).NewRow
                                            drowInsertR(IN_COL_TYPE) = ixc_drowConfig(CONFIG_COL_TYPE).ToString()
                                            drowInsertR(IN_COL_OPT) =
                                                        ixc_drowConfig(CONFIG_COL_EXTRA_OUTPUT).ToString().Replace(FLOATING_NUM_PLACEHOLDER, iOutputNum.ToString()) _
                                                         + "Right"
                                            drowInsertR(IN_COL_VALUE) = sValueRight
                                            PadDataRow(drowInsertR)
                                            dsOutPut.Tables(0).Rows.Add(drowInsertR)

                                            'for backwards compatibility to pre InfoPOS v 1.1.9
                                            'save setting again in old format
                                            If Not ixc_drowConfig(CONFIG_COL_EXTRA_OUTPUT).ToString() = "Header:!:" Then
                                                Dim drowInsertR2 As DataRow = dsOutPut.Tables(0).NewRow
                                                drowInsertR2(IN_COL_TYPE) = ixc_drowConfig(CONFIG_COL_TYPE).ToString()
                                                drowInsertR2(IN_COL_OPT) = "Header" + iOutputNum.ToString() + "Right"
                                                drowInsertR2(IN_COL_VALUE) = sValueRight
                                                PadDataRow(drowInsertR2)
                                                dsOutPut.Tables(0).Rows.Add(drowInsertR2)
                                            End If
                                        End If
                                        'add one to upperBound because we idex 0 of comboValues for two columns 
                                        '"TicketHeader:!:Left" and "TicketHeader:!:Right"
                                        For ixInner As Integer = 2 To sComboValues.GetUpperBound(0) + 1
                                            Dim sTmp As Integer = iOutputNum - 1
                                            Select Case ixInner
                                                Case HEADER_FLOAT_TTEXCLUDE_COL
                                                    sVal = GetControlValue(FindControl(HEADER_FLOAT_TTEXCLUDE + sDisplayOrPrint + sTmp.ToString()))

                                                Case HEADER_FLOAT_JUSTIFICATION_COL
                                                    sVal = GetControlValue(FindControl(HEADER_FLOAT_JUSTIFICATION + sDisplayOrPrint + sTmp.ToString()))

                                                Case HEADER_FLOAT_LARGE_COL
                                                    sVal = GetControlValue(FindControl(HEADER_FLOAT_LARGE + sDisplayOrPrint + sTmp.ToString()))

                                                Case HEADER_FLOAT_BOLD_COL
                                                    sVal = GetControlValue(FindControl(HEADER_FLOAT_BOLD + sDisplayOrPrint + sTmp.ToString()))

                                                Case HEADER_FLOAT_WIDE_COL
                                                    sVal = GetControlValue(FindControl(HEADER_FLOAT_WIDE + sDisplayOrPrint + sTmp.ToString()))

                                                Case HEADER_FLOAT_HIGH_COL
                                                    sVal = GetControlValue(FindControl(HEADER_FLOAT_HIGH + sDisplayOrPrint + sTmp.ToString()))

                                                Case HEADER_FLOAT_UNDERLINE_COL
                                                    sVal = GetControlValue(FindControl(HEADER_FLOAT_UNDERLINE + sDisplayOrPrint + sTmp.ToString()))

                                                Case HEADER_FLOAT_COLOR_COL
                                                    sVal = GetControlValue(FindControl(HEADER_FLOAT_COLOR + sDisplayOrPrint + sTmp.ToString()))

                                            End Select

                                            If sVal <> "" Then
                                                Dim drowInsert2 As DataRow = dsOutPut.Tables(0).NewRow
                                                drowInsert2(IN_COL_TYPE) = ixc_drowConfig(CONFIG_COL_TYPE).ToString()
                                                drowInsert2(IN_COL_OPT) = sOpt(ixInner).Replace(FLOATING_NUM_PLACEHOLDER, iOutputNum.ToString())
                                                drowInsert2(IN_COL_VALUE) = sVal
                                                PadDataRow(drowInsert2)
                                                dsOutPut.Tables(0).Rows.Add(drowInsert2)
                                            End If

                                        Next
                                    End If
                                End If
                            Next

                        End If  ' End: If ixc_drowConfig(CONFIG_COL_CONTROL_TYPE).ToString() = "header_float" Then

                    End If  ' End: If Not bFound Then  


                    If Not bFound Then

                        If ixc_drowConfig(CONFIG_COL_CONTROL_TYPE).ToString() = "paytype_float" Then

                            Dim sOpt() As String = Split(ixc_drowConfig(CONFIG_COL_OPT), FLOATING_DISPLAY_DELIM)
                            Dim sValidate() As String = Split(ixc_drowConfig(CONFIG_COL_DATA_VALIDATION), FLOATING_DISPLAY_DELIM)

                            ' aryPayTypes contains entries for all rows, active and deleted, that have been in the
                            ' pay types div tag. The entry indices in aryPayTypes, correspond to numbers associated
                            ' with controls in the div tag. So the indices of non-deleted entries in aryPayTypes can
                            ' be used to find control names.
                            Dim aryPayTypes As ArrayList = CType(Session(SESSION_ARY_PAY_TYPES), ArrayList)

                            Dim iCnt As Integer = 0
                            For ix As Integer = 0 To aryPayTypes.Count - 1
                                If aryPayTypes(ix).ToString().IndexOf(FLOATING_ROW_WHACK) = -1 Then
                                    iCnt += 1
                                    For ixInner As Integer = 0 To sOpt.GetUpperBound(0)
                                        Dim sVal As String = ""

                                        Select Case ixInner
                                            Case PAYTYPE_FLOAT_PAYTYPE_COL
                                                sVal = GetControlValue(FindControl(PAYTYPE_FLOAT_PAYTYPE + ix.ToString()))

                                            Case PAYTYPE_FLOAT_PAYTYPE_TICKET_COL
                                                sVal = GetControlValue(FindControl(PAYTYPE_FLOAT_PAYTYPE_TICKET + ix.ToString()))

                                            Case PAYTYPE_FLOAT_TYPE_COL
                                                sVal = GetControlValue(FindControl(PAYTYPE_FLOAT_TYPE + ix.ToString()))

                                            Case PAYTYPE_FLOAT_CHANGE_COL
                                                sVal = GetControlValue(FindControl(PAYTYPE_FLOAT_CHANGE + ix.ToString()))

                                            Case PAYTYPE_FLOAT_REFUND_COL
                                                sVal = GetControlValue(FindControl(PAYTYPE_FLOAT_REFUND + ix.ToString()))

                                            Case PAYTYPE_FLOAT_REQ_NUMBER_COL
                                                sVal = GetControlValue(FindControl(PAYTYPE_FLOAT_REQ_NUMBER + ix.ToString()))

                                            Case PAYTYPE_FLOAT_REQ_MGR_AUTH_COL
                                                sVal = GetControlValue(FindControl(PAYTYPE_FLOAT_REQ_MGR_AUTH + ix.ToString()))

                                            Case PAYTYPE_FLOAT_DRAWER_COL
                                                sVal = GetControlValue(FindControl(PAYTYPE_FLOAT_DRAWER + ix.ToString()))

                                            Case PAYTYPE_FLOAT_RECEIPT_COL
                                                sVal = GetControlValue(FindControl(PAYTYPE_FLOAT_RECEIPT + ix.ToString()))

                                            Case PAYTYPE_FLOAT_REQ_CUST_COL
                                                sVal = GetControlValue(FindControl(PAYTYPE_FLOAT_REQ_CUST + ix.ToString()))

                                            Case PAYTYPE_FLOAT_NO_SURCHARGE_COL
                                                sVal = GetControlValue(FindControl(PAYTYPE_FLOAT_NO_SURCHARGE + ix.ToString()))

                                        End Select

                                        '' xxxyyy
                                        '' Hard-code
                                        '' Drawer options are stored in the option file as 0, 1, 0r 2. They are
                                        '' displayed in the combo box as "No Drawer' etc. This function converts the values.
                                        'If sOpt(ixInner).IndexOf("Drawer") <> -1 Then
                                        '    sVal = DrawerTextHardCode(sVal)
                                        'End If

                                        '' Hard-code
                                        '' Drawer options are stored in the option file as 0, 1, 0r 2. They are
                                        '' displayed in the combo box as "No Receipt' etc. This function converts the values.
                                        'If sOpt(ixInner).IndexOf("Receipts") <> -1 Then
                                        '    sVal = ReceiptTextHardCode(sVal)
                                        'End If

                                        If sValidate(ixInner) <> "" Then
                                            Dim sErr As String = ValidateData(sVal, sValidate(ixInner),
                                                    sOpt(ixInner).Replace(FLOATING_NUM_PLACEHOLDER, iCnt.ToString()))
                                            If sErr <> "" Then
                                                Dim sCSB As String = "<script>"
                                                sCSB += "alert('" + "Error in Payment Types. " + sErr + "');"
                                                sCSB += "</script>"
                                                RegisterClientScriptBlock("dataValidatationError", sCSB)
                                                Return False
                                            End If
                                        End If

                                        Dim drowInsert As DataRow = dsOutPut.Tables(0).NewRow
                                        drowInsert(IN_COL_TYPE) = ixc_drowConfig(CONFIG_COL_TYPE).ToString()
                                        drowInsert(IN_COL_OPT) = sOpt(ixInner).Replace(FLOATING_NUM_PLACEHOLDER, iCnt.ToString())
                                        drowInsert(IN_COL_VALUE) = sVal
                                        PadDataRow(drowInsert)
                                        dsOutPut.Tables(0).Rows.Add(drowInsert)

                                    Next
                                End If
                            Next

                            bFound = True
                        End If
                    End If

                    If Not bFound Then
                        If ixc_drowConfig(CONFIG_COL_CONTROL_TYPE).ToString() = "printing" Then
                            bFound = True

                            Dim sOpt() As String = Split(ixc_drowConfig(CONFIG_COL_OPT).ToString(), FLOATING_DISPLAY_DELIM)
                            Dim sFloatType() As String = Split(ixc_drowConfig(CONFIG_COL_FLOAT_TYPE).ToString(), FLOATING_DISPLAY_DELIM)
                            Dim sValidate() As String = Split(ixc_drowConfig(CONFIG_COL_DATA_VALIDATION).ToString(), FLOATING_DISPLAY_DELIM)

                            For ix As Integer = 0 To sOpt.Length - 1
                                If Not FindControl("printing_" + sOpt(ix)) Is Nothing Then

                                    Dim sVal As String = ""

                                    Select Case sFloatType(ix)

                                        Case "textbox"
                                            Dim txtBox As TextBox = FindControl("printing_" + sOpt(ix))
                                            sVal = txtBox.Text

                                        Case "checkbox"
                                            Dim chkBox As CheckBox = FindControl("printing_" + sOpt(ix))
                                            sVal = chkBox.Checked.ToString()

                                        Case "combo"
                                            Dim cboBox As DropDownList = FindControl("printing_" + sOpt(ix))
                                            sVal = cboBox.SelectedValue

                                    End Select

                                    ' Validata data.
                                    If sValidate(ix) <> "" Then
                                        Dim sErr As String = ValidateData(sVal, sValidate(ix),
                                                ixc_drowConfig(CONFIG_COL_DISPLAY).ToString())
                                        If sErr <> "" Then
                                            Dim sCSB As String = "<script>"
                                            sCSB += "alert('" & sErr & "');"
                                            sCSB += "</script>"
                                            RegisterClientScriptBlock("dataValidatationError", sCSB)
                                            Return False
                                        End If
                                    End If

                                    Dim drowInsert As DataRow = dsOutPut.Tables(0).NewRow
                                    drowInsert(IN_COL_TYPE) = ixc_drowConfig(CONFIG_COL_TYPE).ToString()
                                    drowInsert(IN_COL_OPT) = sOpt(ix)
                                    drowInsert(IN_COL_VALUE) = sVal

                                    PadDataRow(drowInsert)
                                    dsOutPut.Tables(0).Rows.Add(drowInsert)
                                End If
                            Next


                        End If
                    End If


                    ' Lots of hard-code
                    If Not bFound Then
                        If ixc_drowConfig(CONFIG_COL_CONTROL_TYPE).ToString() = "scheduled_print" Then
                            bFound = True

                            Dim sVal As String = ""
                            Dim cboBoxHH As DropDownList = FindControl("scheduled_printHH")
                            sVal = cboBoxHH.SelectedValue

                            Dim cboBoxMM As DropDownList = FindControl("scheduled_printMM")
                            sVal += cboBoxMM.SelectedValue

                            Dim drowInsert As DataRow = dsOutPut.Tables(0).NewRow
                            drowInsert(IN_COL_TYPE) = ixc_drowConfig(CONFIG_COL_TYPE).ToString()
                            drowInsert(IN_COL_OPT) = ixc_drowConfig(CONFIG_COL_OPT).ToString()
                            drowInsert(IN_COL_VALUE) = sVal

                            PadDataRow(drowInsert)
                            dsOutPut.Tables(0).Rows.Add(drowInsert)

                        End If
                    End If

                    If Not bFound Then
                        If ixc_drowConfig(CONFIG_COL_CONTROL_TYPE).ToString() = "logo_header" Or
                                ixc_drowConfig(CONFIG_COL_CONTROL_TYPE).ToString() = "logo_footer" Then
                            bFound = True

                            Dim sOpt() As String = Split(ixc_drowConfig(CONFIG_COL_OPT), FLOATING_DISPLAY_DELIM)
                            Dim sValidate() As String = Split(ixc_drowConfig(CONFIG_COL_DATA_VALIDATION), FLOATING_DISPLAY_DELIM)

                            Dim aryLogo As ArrayList
                            If ixc_drowConfig(CONFIG_COL_CONTROL_TYPE).ToString() = "logo_header" Then
                                aryLogo = CType(Session(SESSION_ARY_LOGO_HEADER), ArrayList)
                            Else
                                aryLogo = CType(Session(SESSION_ARY_LOGO_FOOTER), ArrayList)
                            End If

                            Dim iCnt As Integer = 0
                            For ix As Integer = 0 To aryLogo.Count - 1
                                If aryLogo(ix).ToString().IndexOf(FLOATING_ROW_WHACK) = -1 Then
                                    iCnt += 1
                                    For ixInner As Integer = 0 To sOpt.GetUpperBound(0)
                                        Dim sVal As String = ""

                                        If ixc_drowConfig(CONFIG_COL_CONTROL_TYPE).ToString() = "logo_header" Then
                                            Select Case ixInner
                                                Case LOGO_HEADER_TEXT_COL
                                                    sVal = GetControlValue(FindControl(LOGO_HEADER_TEXT + ix.ToString()))

                                                Case LOGO_HEADER_JUSTIFICATION_COL
                                                    sVal = GetControlValue(FindControl(LOGO_HEADER_JUSTIFICATION + ix.ToString()))

                                                Case LOGO_HEADER_LARGE_COL
                                                    sVal = GetControlValue(FindControl(LOGO_HEADER_LARGE + ix.ToString()))

                                                Case LOGO_HEADER_BOLD_COL
                                                    sVal = GetControlValue(FindControl(LOGO_HEADER_BOLD + ix.ToString()))

                                                Case LOGO_HEADER_WIDE_COL
                                                    sVal = GetControlValue(FindControl(LOGO_HEADER_WIDE + ix.ToString()))

                                                Case LOGO_HEADER_HIGH_COL
                                                    sVal = GetControlValue(FindControl(LOGO_HEADER_HIGH + ix.ToString()))

                                                Case LOGO_HEADER_UNDERLINE_COL
                                                    sVal = GetControlValue(FindControl(LOGO_HEADER_UNDERLINE + ix.ToString()))

                                                Case LOGO_HEADER_QR_CODE_COL
                                                    sVal = GetControlValue(FindControl(LOGO_HEADER_QR_CODE + ix.ToString()))

                                                Case LOGO_HEADER_QR_PIXELS_COL
                                                    sVal = GetControlValue(FindControl(LOGO_HEADER_QR_PIXELS + ix.ToString()))

                                                Case LOGO_HEADER_QR_QUALITY_COL
                                                    sVal = GetControlValue(FindControl(LOGO_HEADER_QR_QUALITY + ix.ToString()))

                                            End Select
                                        Else
                                            Select Case ixInner
                                                Case LOGO_FOOTER_TEXT_COL
                                                    sVal = GetControlValue(FindControl(LOGO_FOOTER_TEXT + ix.ToString()))

                                                Case LOGO_FOOTER_JUSTIFICATION_COL
                                                    sVal = GetControlValue(FindControl(LOGO_FOOTER_JUSTIFICATION + ix.ToString()))

                                                Case LOGO_FOOTER_LARGE_COL
                                                    sVal = GetControlValue(FindControl(LOGO_FOOTER_LARGE + ix.ToString()))

                                                Case LOGO_FOOTER_BOLD_COL
                                                    sVal = GetControlValue(FindControl(LOGO_FOOTER_BOLD + ix.ToString()))

                                                Case LOGO_FOOTER_WIDE_COL
                                                    sVal = GetControlValue(FindControl(LOGO_FOOTER_WIDE + ix.ToString()))

                                                Case LOGO_FOOTER_HIGH_COL
                                                    sVal = GetControlValue(FindControl(LOGO_FOOTER_HIGH + ix.ToString()))

                                                Case LOGO_FOOTER_UNDERLINE_COL
                                                    sVal = GetControlValue(FindControl(LOGO_FOOTER_UNDERLINE + ix.ToString()))

                                                Case LOGO_FOOTER_QR_CODE_COL
                                                    sVal = GetControlValue(FindControl(LOGO_FOOTER_QR_CODE + ix.ToString()))

                                                Case LOGO_FOOTER_QR_PIXELS_COL
                                                    sVal = GetControlValue(FindControl(LOGO_FOOTER_QR_PIXELS + ix.ToString()))

                                                Case LOGO_FOOTER_QR_QUALITY_COL
                                                    sVal = GetControlValue(FindControl(LOGO_FOOTER_QR_QUALITY + ix.ToString()))

                                            End Select

                                        End If

                                        If sValidate(ixInner) <> "" Then
                                            Dim sErr As String = ValidateData(sVal, sValidate(ixInner),
                                                    sOpt(ixInner).Replace(FLOATING_NUM_PLACEHOLDER, iCnt.ToString()))
                                            If sErr <> "" Then
                                                Dim sCSB As String = "<script>"
                                                sCSB += "alert('" + "Error in Logo Header/Footer. " + sErr + "');"
                                                sCSB += "</script>"
                                                RegisterClientScriptBlock("dataValidatationError", sCSB)
                                                Return False
                                            End If
                                        End If

                                        Dim drowInsert As DataRow = dsOutPut.Tables(0).NewRow
                                        drowInsert(IN_COL_TYPE) = ixc_drowConfig(CONFIG_COL_TYPE).ToString()
                                        drowInsert(IN_COL_OPT) = sOpt(ixInner).Replace(FLOATING_NUM_PLACEHOLDER, iCnt.ToString())
                                        drowInsert(IN_COL_VALUE) = sVal
                                        PadDataRow(drowInsert)
                                        dsOutPut.Tables(0).Rows.Add(drowInsert)

                                    Next
                                End If
                            Next
                        End If
                    End If


                    If Not bFound Then
                        If ixc_drowConfig(CONFIG_COL_CONTROL_TYPE).ToString() = "cs_header" Or
                                ixc_drowConfig(CONFIG_COL_CONTROL_TYPE).ToString() = "cs_footer" Then
                            bFound = True

                            Dim sOpt() As String = Split(ixc_drowConfig(CONFIG_COL_OPT), FLOATING_DISPLAY_DELIM)
                            Dim sValidate() As String = Split(ixc_drowConfig(CONFIG_COL_DATA_VALIDATION), FLOATING_DISPLAY_DELIM)

                            Dim aryLogo As ArrayList
                            If ixc_drowConfig(CONFIG_COL_CONTROL_TYPE).ToString() = "cs_header" Then
                                aryLogo = CType(Session(SESSION_ARY_CS_HEADER), ArrayList)
                            Else
                                aryLogo = CType(Session(SESSION_ARY_CS_FOOTER), ArrayList)
                            End If

                            Dim iCnt As Integer = 0
                            For ix As Integer = 0 To aryLogo.Count - 1
                                If aryLogo(ix).ToString().IndexOf(FLOATING_ROW_WHACK) = -1 Then
                                    iCnt += 1
                                    For ixInner As Integer = 0 To sOpt.GetUpperBound(0)
                                        Dim sVal As String = ""

                                        If ixc_drowConfig(CONFIG_COL_CONTROL_TYPE).ToString() = "cs_header" Then
                                            Select Case ixInner
                                                Case CS_HEADER_TEXT_COL
                                                    sVal = GetControlValue(FindControl(CS_HEADER_TEXT + ix.ToString()))

                                                Case CS_HEADER_JUSTIFICATION_COL
                                                    sVal = GetControlValue(FindControl(CS_HEADER_JUSTIFICATION + ix.ToString()))

                                                Case CS_HEADER_LARGE_COL
                                                    sVal = GetControlValue(FindControl(CS_HEADER_LARGE + ix.ToString()))

                                                Case CS_HEADER_BOLD_COL
                                                    sVal = GetControlValue(FindControl(CS_HEADER_BOLD + ix.ToString()))

                                                Case CS_HEADER_WIDE_COL
                                                    sVal = GetControlValue(FindControl(CS_HEADER_WIDE + ix.ToString()))

                                                Case CS_HEADER_HIGH_COL
                                                    sVal = GetControlValue(FindControl(CS_HEADER_HIGH + ix.ToString()))

                                                Case CS_HEADER_UNDERLINE_COL
                                                    sVal = GetControlValue(FindControl(CS_HEADER_UNDERLINE + ix.ToString()))

                                            End Select
                                        Else
                                            Select Case ixInner
                                                Case CS_FOOTER_TEXT_COL
                                                    sVal = GetControlValue(FindControl(CS_FOOTER_TEXT + ix.ToString()))

                                                Case CS_FOOTER_JUSTIFICATION_COL
                                                    sVal = GetControlValue(FindControl(CS_FOOTER_JUSTIFICATION + ix.ToString()))

                                                Case CS_FOOTER_LARGE_COL
                                                    sVal = GetControlValue(FindControl(CS_FOOTER_LARGE + ix.ToString()))

                                                Case CS_FOOTER_BOLD_COL
                                                    sVal = GetControlValue(FindControl(CS_FOOTER_BOLD + ix.ToString()))

                                                Case CS_FOOTER_WIDE_COL
                                                    sVal = GetControlValue(FindControl(CS_FOOTER_WIDE + ix.ToString()))

                                                Case CS_FOOTER_HIGH_COL
                                                    sVal = GetControlValue(FindControl(CS_FOOTER_HIGH + ix.ToString()))

                                                Case CS_FOOTER_UNDERLINE_COL
                                                    sVal = GetControlValue(FindControl(CS_FOOTER_UNDERLINE + ix.ToString()))

                                            End Select

                                        End If

                                        If sValidate(ixInner) <> "" Then
                                            Dim sErr As String = ValidateData(sVal, sValidate(ixInner),
                                                    sOpt(ixInner).Replace(FLOATING_NUM_PLACEHOLDER, iCnt.ToString()))
                                            If sErr <> "" Then
                                                Dim sCSB As String = "<script>"
                                                sCSB += "alert('" + "Error in Customer Survey Header/Footer. " + sErr + "');"
                                                sCSB += "</script>"
                                                RegisterClientScriptBlock("dataValidatationError", sCSB)
                                                Return False
                                            End If
                                        End If

                                        Dim drowInsert As DataRow = dsOutPut.Tables(0).NewRow
                                        drowInsert(IN_COL_TYPE) = ixc_drowConfig(CONFIG_COL_TYPE).ToString()
                                        drowInsert(IN_COL_OPT) = sOpt(ixInner).Replace(FLOATING_NUM_PLACEHOLDER, iCnt.ToString())
                                        drowInsert(IN_COL_VALUE) = sVal
                                        PadDataRow(drowInsert)
                                        dsOutPut.Tables(0).Rows.Add(drowInsert)

                                    Next
                                End If
                            Next
                        End If
                    End If


                    If Not bFound Then
                        If ixc_drowConfig(CONFIG_COL_CONTROL_TYPE).ToString() = "tickettype_float" Then
                            bFound = True


                            Dim sOpt() As String = Split(ixc_drowConfig(CONFIG_COL_OPT), FLOATING_DISPLAY_DELIM)
                            Dim sValidate() As String = Split(ixc_drowConfig(CONFIG_COL_DATA_VALIDATION), FLOATING_DISPLAY_DELIM)

                            ' aryTicketTypes contains entries for all rows, active and deleted, that have been in the
                            ' ticket types div tag. The entry indices in aryTicketTypes, correspond to numbers associated
                            ' with controls in the div tag. So the indices of non-deleted entries in aryTicketTypes can
                            ' be used to find control names.
                            Dim aryTicketTypes As ArrayList = CType(Session(SESSION_ARY_TICKET_TYPES), ArrayList)

                            Dim iCnt As Integer = 0
                            For ix As Integer = 0 To aryTicketTypes.Count - 1
                                If aryTicketTypes(ix).ToString().IndexOf(FLOATING_ROW_WHACK) = -1 Then
                                    iCnt += 1
                                    For ixInner As Integer = 0 To sOpt.GetUpperBound(0)
                                        Dim sVal As String = ""

                                        Select Case ixInner
                                            Case TICKETTYPE_FLOAT_TICKETTYPE_COL
                                                sVal = GetControlValue(FindControl(TICKETTYPE_FLOAT_TICKETTYPE + ix.ToString()))

                                            Case TICKETTYPE_FLOAT_POPUP_COL
                                                sVal = GetControlValue(FindControl(TICKETTYPE_FLOAT_POPUP + ix.ToString()))

                                            Case TICKETTYPE_FLOAT_DRIVE_COL
                                                sVal = GetControlValue(FindControl(TICKETTYPE_FLOAT_DRIVE + ix.ToString()))

                                            Case TICKETTYPE_FLOAT_SCHED_PRICES_COL
                                                sVal = GetControlValue(FindControl(TICKETTYPE_FLOAT_SCHED_PRICES + ix.ToString()))

                                            Case TICKETTYPE_FLOAT_REASSIGN_COL
                                                sVal = GetControlValue(FindControl(TICKETTYPE_FLOAT_REASSIGN + ix.ToString()))

                                            Case TICKETTYPE_FLOAT_REQUIRE_COL
                                                sVal = GetControlValue(FindControl(TICKETTYPE_FLOAT_REQUIRE + ix.ToString()))

                                            Case TICKETTYPE_FLOAT_UPCHARGE_COL
                                                sVal = GetControlValue(FindControl(TICKETTYPE_FLOAT_UPCHARGE + ix.ToString()))

                                            Case TICKETTYPE_FLOAT_SCHEDULE_COL
                                                sVal = GetControlValue(FindControl(TICKETTYPE_FLOAT_SCHEDULE + ix.ToString()))

                                            Case TICKETTYPE_FLOAT_PHRASE_COL
                                                sVal = GetControlValue(FindControl(TICKETTYPE_FLOAT_PHRASE + ix.ToString()))

                                            Case TICKETTYPE_FLOAT_AUTOCOMBO_COL
                                                sVal = GetControlValue(FindControl(TICKETTYPE_FLOAT_AUTOCOMBO + ix.ToString()))

                                            Case TICKETTYPE_FLOAT_CUSTOMERSURVEY_COL
                                                sVal = GetControlValue(FindControl(TICKETTYPE_FLOAT_CUSTOMERSURVEY + ix.ToString()))

                                            Case TICKETTYPE_FLOAT_SURVEYINTERVAL_COL
                                                sVal = GetControlValue(FindControl(TICKETTYPE_FLOAT_SURVEYINTERVAL + ix.ToString()))

                                            Case TICKETTYPE_FLOAT_EXCLUDE_1_COL
                                                sVal = GetControlValue(FindControl(TICKETTYPE_FLOAT_EXCLUDE_1 + ix.ToString()))

                                            Case TICKETTYPE_FLOAT_EXCLUDE_2_COL
                                                sVal = GetControlValue(FindControl(TICKETTYPE_FLOAT_EXCLUDE_2 + ix.ToString()))

                                            Case TICKETTYPE_FLOAT_EXCLUDE_3_COL
                                                sVal = GetControlValue(FindControl(TICKETTYPE_FLOAT_EXCLUDE_3 + ix.ToString()))

                                            Case TICKETTYPE_FLOAT_EXCLUDE_4_COL
                                                sVal = GetControlValue(FindControl(TICKETTYPE_FLOAT_EXCLUDE_4 + ix.ToString()))

                                            Case TICKETTYPE_FLOAT_KDSTYPE_COL
                                                sVal = GetControlValue(FindControl(TICKETTYPE_FLOAT_KDSTYPE + ix.ToString()))

                                            Case TICKETTYPE_FLOAT_COLOR_COL
                                                sVal = GetControlValue(FindControl(TICKETTYPE_FLOAT_COLOR + ix.ToString()))

                                            Case TICKETTYPE_FLOAT_HIDDEN_COL
                                                sVal = GetControlValue(FindControl(TICKETTYPE_FLOAT_HIDDEN + ix.ToString()))

                                            Case TICKETTYPE_FLOAT_SUPPRESS_KDS_COL
                                                sVal = GetControlValue(FindControl(TICKETTYPE_FLOAT_SUPPRESS_KDS + ix.ToString()))

                                            Case TICKETTYPE_FLOAT_SUPPRESS_KP_COL
                                                sVal = GetControlValue(FindControl(TICKETTYPE_FLOAT_SUPPRESS_KP + ix.ToString()))

                                            Case TICKETTYPE_FLOAT_DSP_COL
                                                sVal = GetControlValue(FindControl(TICKETTYPE_FLOAT_DSP + ix.ToString()))

                                        End Select

                                        If sValidate(ixInner) <> "" Then
                                            Dim sErr As String = ValidateData(sVal, sValidate(ixInner),
                                                    sOpt(ixInner).Replace(FLOATING_NUM_PLACEHOLDER, iCnt.ToString()))
                                            If sErr <> "" Then
                                                Dim sCSB As String = "<script>"
                                                sCSB += "alert('" + "Error in Ticket Types. " + sErr + "');"
                                                sCSB += "</script>"
                                                RegisterClientScriptBlock("dataValidatationError", sCSB)
                                                Return False
                                            End If
                                        End If

                                        Dim drowInsert As DataRow = dsOutPut.Tables(0).NewRow
                                        drowInsert(IN_COL_TYPE) = ixc_drowConfig(CONFIG_COL_TYPE).ToString()
                                        drowInsert(IN_COL_OPT) = sOpt(ixInner).Replace(FLOATING_NUM_PLACEHOLDER, iCnt.ToString())
                                        drowInsert(IN_COL_VALUE) = sVal
                                        PadDataRow(drowInsert)
                                        dsOutPut.Tables(0).Rows.Add(drowInsert)

                                    Next
                                End If
                            Next

                        End If
                    End If

                    ' If this row wasn't found in any of the controls, look for it in the original file.
                    ' If it's there, write that value back. If it's not, write an empty string back.
                    If Not bFound Then
                        Dim drowInsert As DataRow = dsOutPut.Tables(0).NewRow

                        drowInsert(IN_COL_TYPE) = ixc_drowConfig(CONFIG_COL_TYPE).ToString()
                        drowInsert(IN_COL_OPT) = ixc_drowConfig(CONFIG_COL_OPT).ToString()

                        Dim objKeys(1) As Object
                        objKeys(0) = ixc_drowConfig(IN_COL_OPT).ToString()
                        objKeys(1) = ixc_drowConfig(IN_COL_TYPE).ToString()
                        Dim drowOrig As DataRow = _dsIn.Tables(0).Rows.Find(objKeys)

                        If Not drowOrig Is Nothing Then
                            drowInsert(IN_COL_VALUE) = drowOrig(IN_COL_VALUE).ToString()
                        Else
                            drowInsert(IN_COL_VALUE) = ""
                        End If

                        PadDataRow(drowInsert)
                        dsOutPut.Tables(0).Rows.Add(drowInsert)

                    End If

                Next  '  For Each ixc_drowConfig As DataRow In _dsConfig.Tables(0).Rows

                Dim sHost As String = Request("HTTP_HOST").ToString()
                'sHost = "127.0.0.1"
                ' For testing

                Dim webService As New WebService.Exchange
                webService.Url = "http://" + sHost + "/WebService/Exchange.asmx"
                If sHost <> "localhost" Then
                    Dim proxyObject As New WebProxy("http://" + sHost + "/WebService:80", True)
                    webService.Proxy = proxyObject
                End If

                Dim sMsg As String = webService.uploadDataSet(dsOutPut, "INFOPOS_OPTIONS")
                If sMsg = "SUCCESS" Then
                    Return True
                Else
                    Return False
                End If


            Catch ex As Exception
                HandleError(ex)
            End Try

        End Function

        Protected Sub PadDataRow(ByRef pv_drow As DataRow)
            Try
                ' todo: Hard code?
                pv_drow("store") = lblStoreNum.Text
                pv_drow("register") = cboRegister.SelectedValue
                Dim x = 0
            Catch ex As Exception
                HandleError(ex)
            End Try

        End Sub

        Protected Function ValidateData(ByVal pv_sData As String, ByVal pv_sRule As String, ByVal pv_sFieldName As String) As String

            Try
                Dim sReturn As String = ""

                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                ' Hard-Code: The cases in this select statement are hard-code matches of the 
                ' DataValidation field in the configuration file.
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                Select Case pv_sRule
                    Case "posInt"
                        If Not IsNumeric(pv_sData) Then
                            sReturn = pv_sFieldName + " must be numeric."
                        Else
                            If pv_sData.IndexOf(".") <> -1 Then
                                sReturn = pv_sFieldName + " must be a whole number."
                            Else
                                Dim iTmp As Integer = Convert.ToInt32(pv_sData)
                                If iTmp <= 0 Then
                                    sReturn = pv_sFieldName + " must be greater than zero."
                                End If
                            End If
                        End If

                    Case "posIntOrZero"
                        If Not IsNumeric(pv_sData) Then
                            sReturn = pv_sFieldName + " must be numeric."
                        Else
                            If pv_sData.IndexOf(".") <> -1 Then
                                sReturn = pv_sFieldName + " must be a whole number."
                            Else
                                Dim iTmp As Integer = Convert.ToInt32(pv_sData)
                                If iTmp < 0 Then
                                    sReturn = pv_sFieldName + " must be greater than or equal to zero."
                                End If
                            End If
                        End If

                    Case "posIntOrZeroOrBlank"
                        If pv_sData.Trim.Length > 0 Then ' blank is okay
                            If Not IsNumeric(pv_sData) Then
                                sReturn = pv_sFieldName + " must be numeric."
                            Else
                                If pv_sData.IndexOf(".") <> -1 Then
                                    sReturn = pv_sFieldName + " must be a whole number."
                                Else
                                    Dim iTmp As Integer = Convert.ToInt32(pv_sData)
                                    If iTmp < 0 Then
                                        sReturn = pv_sFieldName + " must be greater than or equal to zero."
                                    End If
                                End If
                            End If
                        End If

                    Case "taxRate"
                        If Not IsNumeric(pv_sData) Then
                            sReturn = pv_sFieldName + " must be numeric."
                        Else
                            Dim dblTmp As Double = Convert.ToDouble(pv_sData)
                            If dblTmp < 0 Then
                                sReturn = pv_sFieldName + " must be greater than or equal to zero."
                            End If
                            If dblTmp >= 100 Then
                                sReturn = pv_sFieldName + " must be less than 100."
                            End If
                        End If

                    Case "posReal"
                        If Not IsNumeric(pv_sData) Then
                            sReturn = pv_sFieldName + " must be numeric."
                        Else
                            Dim dblTmp As Double = Convert.ToDouble(pv_sData)
                            If dblTmp <= 0 Then
                                sReturn = pv_sFieldName + " must be greater than zero."
                            End If
                        End If

                    Case "posRealOrBlank"
                        If pv_sData.Trim() <> "" Then  ' "" is OK.
                            If Not IsNumeric(pv_sData) Then
                                sReturn = pv_sFieldName + " must be numeric."
                            Else
                                Dim dblTmp As Double = Convert.ToDouble(pv_sData)
                                If dblTmp <= 0 Then
                                    sReturn = pv_sFieldName + " must be greater than zero."
                                End If
                            End If
                        End If

                    Case "posFractionOrBlank"
                        If pv_sData.Trim() <> "" Then  ' "" is OK.

                            If Not IsNumeric(pv_sData) Then
                                sReturn = pv_sFieldName + " must be numeric."
                            Else
                                Dim dblTmp As Double = Convert.ToDouble(pv_sData)
                                If dblTmp <= 0 Then
                                    sReturn = pv_sFieldName + " must be greater than zero."
                                End If
                                If dblTmp >= 1 Then
                                    sReturn = pv_sFieldName + " must be less than one."
                                End If
                            End If
                        End If

                    Case "posIntPercOrBlank"
                        If pv_sData.Trim() <> "" Then  ' "" is OK.

                            If Not IsNumeric(pv_sData) Then
                                sReturn = pv_sFieldName + " must be numeric."
                            Else
                                If pv_sData.IndexOf(".") <> -1 Then
                                    sReturn = pv_sFieldName + " must be a whole number."
                                Else
                                    Dim iTmp As Int32 = Convert.ToInt32(pv_sData)
                                    If iTmp <= 0 Then
                                        sReturn = pv_sFieldName + " must be greater than zero."
                                    End If
                                    If iTmp > 100 Then
                                        sReturn = pv_sFieldName + " must be less than one hundred percent."
                                    End If
                                End If
                            End If
                        End If

                    Case "areaCodeOrBlank"
                        If pv_sData.Trim() <> "" Then                    ' "" is OK.

                            If Not IsNumeric(pv_sData) Then
                                sReturn = pv_sFieldName + " must be numeric."
                            Else
                                If pv_sData.IndexOf(".") <> -1 Then
                                    sReturn = pv_sFieldName + " must be a valid area code."
                                Else
                                    Dim iTmp As Int32 = Convert.ToInt32(pv_sData)
                                    If iTmp <= 200 Then
                                        sReturn = pv_sFieldName + "  must be a valid area code."
                                    End If
                                    If iTmp > 999 Then
                                        sReturn = pv_sFieldName + "  must be a valid area code."
                                    End If
                                End If
                            End If
                        End If


                    Case "text"
                        If pv_sData Is Nothing Then
                            sReturn = pv_sFieldName + " cannot be blank."
                        Else
                            If pv_sData.Trim() = "" Then
                                sReturn = pv_sFieldName + " cannot be blank."
                            End If
                        End If

                    Case "textOrBlank"

                End Select

                Return sReturn

            Catch ex As Exception
                HandleError(ex)
            End Try

        End Function

        Protected Function GetOptionValue(ByVal pv_sOpt As String, ByVal pv_sType As String) As String
            Try
                Dim sRet As String = ""
                Dim sOpts() As String

                sOpts = Split(pv_sOpt, FLOATING_DISPLAY_DELIM)

                For ix As Integer = 0 To sOpts.GetUpperBound(0)
                    Dim objKeys(1) As Object
                    objKeys(0) = sOpts(ix)
                    objKeys(1) = pv_sType

                    Dim drow As DataRow = _dsIn.Tables(0).Rows.Find(objKeys)
                    If Not drow Is Nothing Then
                        If sRet = "" Then
                            sRet = drow(IN_COL_VALUE).ToString()
                        Else
                            sRet += FLOATING_DISPLAY_DELIM + drow(IN_COL_VALUE).ToString()
                        End If
                    End If
                Next

                Return sRet

            Catch ex As Exception
                HandleError(ex)
            End Try

        End Function

        Protected Function PadTextNBSP(ByVal pv_sStr As String, ByVal pv_iCnt As Integer) As String
            Try
                Dim sRet As String = pv_sStr
                For ix As Integer = 1 To pv_iCnt
                    sRet = "&nbsp;" + sRet + "&nbsp;"
                Next
                Return sRet

            Catch ex As Exception
                HandleError(ex)
            End Try
        End Function

        Protected Function CloseWindow()
            Try
                Session(SESSION_FLOAT_ROWS) = ""
                Session(SESSION_CURRENT_TAB) = ""
                Session(SESSION_OPTIONS_STORE) = ""
                Session(SESSION_OPTIONS_TEST) = ""
                Session(SESSION_ARY_PAY_TYPES) = ""
                Session(SESSION_STORE_DATA) = ""
                Session(SESSION_OPTIONS_REGISTER) = ""
                Session(SESSION_OPTIONS_SOURCE_REGISTER) = ""
                Session(SESSION_ARY_TICKET_TYPES) = ""

                RegisterClientScriptBlock("closeWindow", "<script>window.opener = self; window.close();</script>")
            Catch ex As Exception
                HandleError(ex)
            End Try
        End Function

        'Protected Function DrawerTextHardCode(ByVal pv_sStr As String) As String
        '    Try
        '        Select Case pv_sStr
        '            Case "0"
        '                Return "No Drawer"
        '            Case "1"
        '                Return "Drawer 1"
        '            Case "2"
        '                Return "Drawer 2"
        '            Case "No Drawer"
        '                Return "0"
        '            Case "Drawer 1"
        '                Return "1"
        '            Case "Drawer 2"
        '                Return "2"
        '            Case Else
        '                Return ""
        '        End Select

        '    Catch ex As Exception
        '        HandleError(ex)
        '    End Try

        'End Function

        'Protected Function ReceiptTextHardCode(ByVal pv_sStr As String) As String
        '    Try
        '        Select Case pv_sStr
        '            Case "0"
        '                Return "None"
        '            Case "1"
        '                Return "Single"
        '            Case "2"
        '                Return "Duplicate"
        '            Case "None"
        '                Return "0"
        '            Case "Single"
        '                Return "1"
        '            Case "Duplicate"
        '                Return "2"
        '            Case Else
        '                Return ""
        '        End Select

        '    Catch ex As Exception
        '        HandleError(ex)
        '    End Try

        'End Function

        'Protected Function ColorHardCode(ByVal pv_sStr As String) As String
        '    Try
        '        Select Case pv_sStr
        '            Case "0"
        '                Return "Color 1"
        '            Case "1"
        '                Return "Color 2"
        '            Case "Color 1"
        '                Return "0"
        '            Case "Color 2"
        '                Return "1"
        '            Case Else
        '                Return ""
        '        End Select

        '    Catch ex As Exception
        '        HandleError(ex)
        '    End Try
        '
        'End Function

        Protected Function ComboItem(ByVal pv_sVal As String) As ListItem
            Dim li As New ListItem

            Try
                If pv_sVal.IndexOf(COMBO_TEXT_VALUE_DELIM) > -1 Then
                    li.Text = pv_sVal.Substring(0, pv_sVal.IndexOf(COMBO_TEXT_VALUE_DELIM))
                    li.Value = pv_sVal.Substring(pv_sVal.IndexOf(COMBO_TEXT_VALUE_DELIM) + COMBO_TEXT_VALUE_DELIM.Length)
                Else
                    li.Text = pv_sVal
                    li.Value = pv_sVal
                End If
            Catch ex As Exception
                ' Ingore excpetions here.
            End Try

            Return li

        End Function

        Protected Function ComboVal(ByVal pv_sVal As String) As String
            Dim sRet As String = ""

            Try
                If pv_sVal.IndexOf(COMBO_TEXT_VALUE_DELIM) > -1 Then
                    sRet = pv_sVal.Substring(pv_sVal.IndexOf(COMBO_TEXT_VALUE_DELIM) + COMBO_TEXT_VALUE_DELIM.Length)
                Else
                    sRet = pv_sVal
                End If
            Catch ex As Exception
                ' Ingore excpetions here.
            End Try

            Return sRet

        End Function

        Protected Sub SwapControlValue(ByRef p_ctl1 As Control, ByRef p_ctl2 As Control)
            Try
                If TypeOf (p_ctl1) Is TextBox And TypeOf (p_ctl2) Is TextBox Then
                    Dim txtBox1 As TextBox = p_ctl1
                    Dim txtBox2 As TextBox = p_ctl2
                    Dim sTmp As String = txtBox1.Text
                    txtBox1.Text = txtBox2.Text
                    txtBox2.Text = sTmp
                End If

                If TypeOf (p_ctl1) Is DropDownList And TypeOf (p_ctl2) Is DropDownList Then
                    Dim cboBox1 As DropDownList = p_ctl1
                    Dim cboBox2 As DropDownList = p_ctl2
                    Dim sTmp As String = cboBox1.SelectedValue
                    cboBox1.SelectedValue = cboBox2.SelectedValue
                    cboBox2.SelectedValue = sTmp
                End If

                If TypeOf (p_ctl1) Is CheckBox And TypeOf (p_ctl2) Is CheckBox Then
                    Dim chkBox1 As CheckBox = p_ctl1
                    Dim chkBox2 As CheckBox = p_ctl2
                    Dim bTmp As Boolean = chkBox1.Checked
                    chkBox1.Checked = chkBox2.Checked
                    chkBox2.Checked = bTmp
                End If

            Catch ex As Exception
                HandleError(ex)
            End Try

        End Sub

        Protected Sub CopyControlValue(ByRef p_ctlSource As Control, ByRef p_ctlTarget As Control)
            Try
                If TypeOf (p_ctlSource) Is TextBox And TypeOf (p_ctlTarget) Is TextBox Then
                    Dim txtBox1 As TextBox = p_ctlTarget
                    Dim txtBox2 As TextBox = p_ctlSource
                    txtBox1.Text = txtBox2.Text
                End If

                If TypeOf (p_ctlSource) Is DropDownList And TypeOf (p_ctlTarget) Is DropDownList Then
                    Dim cboBox1 As DropDownList = p_ctlTarget
                    Dim cboBox2 As DropDownList = p_ctlSource
                    cboBox1.SelectedValue = cboBox2.SelectedValue
                End If

                If TypeOf (p_ctlSource) Is CheckBox And TypeOf (p_ctlTarget) Is CheckBox Then
                    Dim chkBox1 As CheckBox = p_ctlTarget
                    Dim chkBox2 As CheckBox = p_ctlSource
                    chkBox1.Checked = chkBox2.Checked
                End If

            Catch ex As Exception
                HandleError(ex)
            End Try

        End Sub

        Protected Function GetControlValue(ByVal pv_ctl As Control) As String
            Try
                If TypeOf (pv_ctl) Is TextBox Then
                    Dim txtBox As TextBox = pv_ctl
                    Return txtBox.Text
                End If

                If TypeOf (pv_ctl) Is DropDownList Then
                    Dim cboBox As DropDownList = pv_ctl
                    Return cboBox.SelectedValue
                End If

                If TypeOf (pv_ctl) Is CheckBox Then
                    Dim chkBox As CheckBox = pv_ctl
                    Return chkBox.Checked.ToString()
                End If

                Return NULL_CONTROL

            Catch ex As Exception
                HandleError(ex)
            End Try

        End Function

        Protected Function GetControlID(ByVal pv_ctl As Control) As String
            Try
                If TypeOf (pv_ctl) Is TextBox Then
                    Dim txtBox As TextBox = pv_ctl
                    Return txtBox.ID
                End If

                If TypeOf (pv_ctl) Is DropDownList Then
                    Dim cboBox As DropDownList = pv_ctl
                    Return cboBox.ID
                End If

                If TypeOf (pv_ctl) Is CheckBox Then
                    Dim chkBox As CheckBox = pv_ctl
                    Return chkBox.ID
                End If

                Return ""

            Catch ex As Exception
                HandleError(ex)
            End Try

        End Function

        Protected Function GetWindowOptionString() As String
            Dim sbOpts As New StringBuilder
            sbOpts.Append("'resizable=yes,menubar=yes,status=no,toolbar=no,scrollbars=yes")
            sbOpts.Append(",left=" + hidLeft.Value.ToString())
            sbOpts.Append(",width=" + hidWidth.Value.ToString())
            sbOpts.Append(",top=" + hidTop.Value.ToString())
            sbOpts.Append(",height=" + hidHeight.Value.ToString() + "'")

            Return sbOpts.ToString()
        End Function

        Protected Function SafeSubString(ByVal inString As Object, ByVal startIndex As Integer) As String
            Try
                Dim sStr As String = ""

                If Not inString Is Nothing Then
                    sStr = inString.ToString()
                End If

                Return SafeSubString(sStr, startIndex, sStr.Length)

            Catch ex As Exception
                HandleError(ex)
            End Try

        End Function

        Protected Function SafeSubString(ByVal inString As Object, ByVal startIndex As Integer, ByVal length As Integer) As String
            Try
                Dim sReturn As String = ""

                If Not inString Is Nothing Then sReturn = inString.ToString()

                If length + startIndex > sReturn.Length Then
                    length = sReturn.Length - startIndex
                End If

                If length < 0 Then
                    Return ""
                Else
                    Return sReturn.Substring(startIndex, length)
                End If

            Catch ex As Exception
                HandleError(ex)
            End Try

        End Function

        Protected Sub HandleError(ByVal ex As Exception, Optional trace As String = Nothing)
            If trace IsNot Nothing Then
                Throw New Exception(ex.Message & " (" & trace & ")" & vbCrLf & ex.StackTrace)
            Else
                Throw New Exception(ex.Message & vbCrLf & ex.StackTrace)
            End If
        End Sub

        Public Function Encode(ByVal str As String) As String
            Try
                If str.Length = 0 Then Return ""

                Dim ret As String = "", pc As String
                Dim iKey As Integer, iLoopMain As Integer, iLoop As Integer
                Randomize()

                iKey = Math.Floor(Rnd() * (str.Length - 2)) + 2
                If iKey > 9 Then iKey = 9
                If Rnd() > 0.5 Then
                    pc = StrReverse(ToBase26(Asc(iKey.ToString())).PadLeft(2, "a"))
                    pc = pc.Substring(0, 1).ToUpper & IIf((Rnd() > 0.5), pc.Substring(1, 1).ToUpper, pc.Substring(1, 1).ToLower)
                Else
                    pc = ToBase26(Asc(iKey.ToString())).PadLeft(2, "a")
                    pc = pc.Substring(0, 1).ToLower & IIf((Rnd() > 0.5), pc.Substring(1, 1).ToUpper, pc.Substring(1, 1).ToLower)
                End If
                ret = ret & pc

                For iLoopMain = 1 To iKey
                    For iLoop = iLoopMain To Len(str) Step iKey
                        If Rnd() > 0.5 Then
                            pc = StrReverse(ToBase26(Asc(Mid(str, iLoop, 1))).PadLeft(2, "a"))
                            pc = pc.Substring(0, 1).ToUpper & IIf((Rnd() > 0.5), pc.Substring(1, 1).ToUpper, pc.Substring(1, 1).ToLower)
                        Else
                            pc = ToBase26(Asc(Mid(str, iLoop, 1))).PadLeft(2, "a")
                            pc = pc.Substring(0, 1).ToLower & IIf((Rnd() > 0.5), pc.Substring(1, 1).ToUpper, pc.Substring(1, 1).ToLower)
                        End If
                        ret = ret & pc
                    Next
                Next

                Return ret

            Catch ex As Exception
                HandleError(ex)
            End Try

        End Function

        Public Function Decode(ByVal str As String) As String

            Try
                If str.Length = 0 Then Return ""

                Dim ret() As Char, iLoop As Integer
                Dim sKey As String, iKey As Integer
                Dim strLen As Integer = CInt(Len(str) / 2) - 1
                ReDim ret(strLen)

                If Mid(str, 1, 1) = Mid(str, 1, 1).ToUpper Then
                    sKey = StrReverse(Mid(str, 1, 2).ToLower)
                    sKey = Chr(FromBase26(sKey))
                    '    sKey = Chr(FromBase26(StrReverse(Mid(str, 1, 2).ToLower)))
                Else
                    sKey = Chr(FromBase26(Mid(str, 1, 2).ToLower))
                End If
                iKey = CInt(sKey)

                Dim startIndex As Integer = 1, index As Integer = startIndex
                For iLoop = 3 To Len(str) Step 2
                    If Mid(str, iLoop, 1) = Mid(str, iLoop, 1).ToUpper Then
                        ret(index) = Chr(FromBase26(StrReverse(Mid(str, iLoop, 2).ToLower)))
                    Else
                        ret(index) = Chr(FromBase26(Mid(str, iLoop, 2).ToLower))
                    End If
                    index = index + iKey
                    If index > strLen Then
                        startIndex = startIndex + 1
                        index = startIndex
                    End If
                Next

                Dim retstr As String = ""
                For iLoop = 1 To strLen
                    retstr = retstr & ret(iLoop)
                Next

                Return retstr

            Catch ex As Exception
                HandleError(ex)
            End Try

        End Function

        Protected Function ToBase26(ByVal i As Integer) As String

            Try
                Dim b26 As String = ""

                Do
                    b26 = Chr(97 + (i Mod 26)) & b26
                    i = Int(i / 26)
                Loop Until i = 0

                Return b26

            Catch ex As Exception
                HandleError(ex)
            End Try

        End Function

        Protected Function FromBase26(ByVal s As String) As Integer
            Try
                Return ((Asc(Mid(s, 1, 1)) - 97) * 26) + (Asc(Mid(s, 2, 1)) - 97)

            Catch ex As Exception
                HandleError(ex)
            End Try

        End Function

#End Region

    End Class

End Namespace
