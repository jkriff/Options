Imports DataAccess
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration
Imports System.Configuration

Partial Class ManagerTrax
    Inherits System.Web.UI.Page

    Private Property _store() As String
        Get
            Return Convert.ToString(ViewState("Store"))
        End Get
        Set(ByVal Value As String)
            ViewState("Store") = Value
        End Set
    End Property

    Private Function GetConnectString() As String
        'Return "Data Source=.\SQLEXPRESS;Initial Catalog=WebsiteTesting;UID=IKAdmin;PWD=3kMGb4sds;Max Pool Size=500;"
        Dim data1 As New DataAccess()
        Return data1.UnMaskString(System.Web.Configuration.WebConfigurationManager.ConnectionStrings("Production").ConnectionString, 1, 2, 3, 4, 5)
    End Function

    Private Sub BindStaticData()
        Dim db As SqlConnection

        Try
            db = New SqlConnection(GetConnectString())
            db.Open()

            Dim dtPredefinedMessages As New DataTable
            Dim sql As String = ""
            sql &= "SELECT      0 as 'MessageID', "
            sql &= "            'Custom Message...' as 'MessageText' "
            sql &= "UNION "
            sql &= "SELECT      [MessageID], "
            sql &= "            [MessageText] "
            sql &= "FROM        MT_PredefinedMessages "
            sql &= "ORDER BY    MessageID"
            Dim cmd As New SqlCommand(sql, db)
            Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            dtPredefinedMessages.Load(dr)

            lstPredefinedMessages.DataSource = dtPredefinedMessages
            lstPredefinedMessages.DataTextField = "MessageText"
            lstPredefinedMessages.DataValueField = "MessageID"
            lstPredefinedMessages.DataBind()

            If Request.Form("lstPredefinedMessages") <> "" Then
                lstPredefinedMessages.SelectedValue = Request.Form("lstPredefinedMessages")
            End If

        Catch ex As Exception
            lblErrors.Text = "There was an error trying to load data from the database.<br/><br/>" & ex.ToString

        Finally
            Try
                db.Close()
            Catch ex As Exception
            End Try

        End Try

        Try
            db = New SqlConnection(GetConnectString())
            db.Open()

            Dim dtManagers As New DataTable
            Dim sql As String = ""
            sql &= "SELECT    0 as 'EmployeeID', "
            sql &= "          'First Available Manager' as 'EmployeeName' "
            sql &= "UNION "
            sql &= "SELECT    CONVERT(int,e.[Employee]) as 'EmployeeID', "
            sql &= "          e.[First Name] + ' ' + e.[Last Name] as 'EmployeeName' "
            sql &= "FROM      Employees e "
            sql &= "WHERE     e.[Client] = 1 "
            sql &= "      AND e.[Store] = " & _store & " "
            sql &= "      AND e.[Employment Selection] = 'Mgr' "
            sql &= "      AND (e.[Termination Date] > '" & Format(Now, "yyyyMMdd") & "' "
            sql &= "      OR   e.[Termination Date] IS NULL) "
            sql &= "      AND (e.[deletedOn] > '" & Now.ToString() & "' "
            sql &= "      OR   e.[deletedOn] IS NULL) "
            sql &= "      AND NOT EXISTS (SELECT  p.[EmpPosition] "
            sql &= "                      FROM    PayHistory p "
            sql &= "                      WHERE   p.[Client] = e.[Client] "
            sql &= "                          AND p.[Store] = e.[Store] "
            sql &= "                          AND p.[EmpID] = e.[Employee] "
            sql &= "                          AND p.[PayStartDate] <= '" & Format(Now, "yyyyMMdd") & "' "
            sql &= "                          AND (p.[PayEndDate] >= '" & Format(Now, "yyyyMMdd") & "' "
            sql &= "                          OR   p.[PayEndDate] IS NULL)) "
            sql &= "UNION "
            sql &= "SELECT    CONVERT(int,e.[Employee]) as 'EmployeeID', "
            sql &= "          e.[First Name] + ' ' + e.[Last Name] as 'EmployeeName' "
            sql &= "FROM      Employees e "
            sql &= "WHERE     e.[Client] = 1 "
            sql &= "      AND e.[Store] = " & _store & " "
            sql &= "      AND (e.[Termination Date] > '" & Format(Now, "yyyyMMdd") & "' "
            sql &= "      OR   e.[Termination Date] IS NULL) "
            sql &= "      AND (e.[deletedOn] > '" & Now.ToString() & "' "
            sql &= "      OR   e.[deletedOn] IS NULL) "
            sql &= "      AND EXISTS (SELECT      p.[EmpPosition] "
            sql &= "                  FROM        PayHistory p "
            sql &= "                  INNER JOIN  EmployeePositions ep "
            sql &= "                          ON  ep.[EmployeePositionID] = p.[EmpPosition] "
            sql &= "                          AND ep.[Manager] = 'Y' "
            sql &= "                  WHERE       p.[Client] = e.[Client] "
            sql &= "                          AND p.[Store] = e.[Store] "
            sql &= "                          AND p.[EmpID] = e.[Employee] "
            sql &= "                          AND p.[PayStartDate] <= '" & Format(Now, "yyyyMMdd") & "' "
            sql &= "                          AND (p.[PayEndDate] >= '" & Format(Now, "yyyyMMdd") & "' "
            sql &= "                          OR   p.[PayEndDate] IS NULL)) "
            Dim cmd As New SqlCommand(sql, db)
            Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            dtManagers.Load(dr)

            ddlManagers.DataSource = dtManagers
            ddlManagers.DataTextField = "EmployeeName"
            ddlManagers.DataValueField = "EmployeeID"
            ddlManagers.DataBind()

        Catch ex As Exception
            lblErrors.Text = "There was an error trying to load data from the database.<br/><br/>" & ex.ToString

        Finally
            Try
                db.Close()
            Catch ex As Exception
            End Try

        End Try

        For I As Integer = Now.Year To Now.Year + 10
            ddlScheduleOneTimeYear.Items.Add(New ListItem(I.ToString, I.ToString))
            ddlStartDateYear.Items.Add(New ListItem(I.ToString, I.ToString))
            ddlEndDateYear.Items.Add(New ListItem(I.ToString, I.ToString))
        Next
    End Sub

    Private Sub BindMasterData()
        Dim db As SqlConnection

        Try
            db = New SqlConnection(GetConnectString())
            db.Open()

            Dim dtSchedules As New DataTable
            dtSchedules.Columns.Add("ScheduleID")
            dtSchedules.Columns.Add("Schedule")

            If lstPredefinedMessages.SelectedIndex > -1 Then
                If lstPredefinedMessages.SelectedValue = 0 Then
                    tblInstructions21.Visible = False
                    tblInstructions22.Visible = True
                    divSchedules.Visible = False
                    tdSchedules.VAlign = "center"

                    hidePreviousSelection.Value = lstPredefinedMessages.SelectedValue

                    SetupForAddNew()
                Else
                    tblInstructions21.Visible = True
                    tblInstructions22.Visible = False
                    divSchedules.Visible = True
                    tdSchedules.VAlign = "top"

                    Dim sql As String = ""
                    sql &= "SELECT      [ScheduleID], "
                    sql &= "            [DeliveryTime], "
                    sql &= "            [DeliveryScheduleTypeID], "
                    sql &= "            [DeliveryScheduleData] "
                    sql &= "FROM        MT_MessageSchedules "
                    sql &= "WHERE       [StoreID] = " & _store & " "
                    sql &= "        AND [MessageID] = " & lstPredefinedMessages.SelectedValue

                    Dim cmd As New SqlCommand(sql, db)
                    Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)

                    While (dr.Read())
                        Try
                            Dim sSchedule As String = ""
                            Dim dataStr As String = dr("DeliveryScheduleData").ToString()
                            Select Case Convert.ToInt32(dr("DeliveryScheduleTypeID"))
                                Case 1 ' Weekly
                                    If dataStr.Contains("0") Then
                                        sSchedule &= "Sun"
                                    End If

                                    If dataStr.Contains("1") Then
                                        If sSchedule.Length > 0 Then sSchedule &= "/"
                                        sSchedule &= "Mon"
                                    End If

                                    If dataStr.Contains("2") Then
                                        If sSchedule.Length > 0 Then sSchedule &= "/"
                                        sSchedule &= "Tue"
                                    End If

                                    If dataStr.Contains("3") Then
                                        If sSchedule.Length > 0 Then sSchedule &= "/"
                                        sSchedule &= "Wed"
                                    End If

                                    If dataStr.Contains("4") Then
                                        If sSchedule.Length > 0 Then sSchedule &= "/"
                                        sSchedule &= "Thu"
                                    End If

                                    If dataStr.Contains("5") Then
                                        If sSchedule.Length > 0 Then sSchedule &= "/"
                                        sSchedule &= "Fri"
                                    End If

                                    If dataStr.Contains("6") Then
                                        If sSchedule.Length > 0 Then sSchedule &= "/"
                                        sSchedule &= "Sat"
                                    End If

                                Case 2 ' Monthly by Date
                                    If dataStr.Length = 2 And dataStr.Substring(0, 1) = "1" Then
                                        sSchedule &= dataStr & "th"
                                    Else
                                        Select Case dataStr.Substring(dataStr.Length - 1, 1)
                                            Case "1"
                                                sSchedule &= dataStr & "st"

                                            Case "2"
                                                sSchedule &= dataStr & "nd"

                                            Case "3"
                                                sSchedule &= dataStr & "rd"

                                            Case Else
                                                sSchedule &= dataStr & "th"

                                        End Select
                                    End If
                                    sSchedule &= " each month"

                                Case 3 ' Monthly by Weekday
                                    sSchedule = "Every "

                                    Dim whichStr As String = dataStr.Substring(0, 1)
                                    Select Case whichStr
                                        Case "1"
                                            sSchedule &= "1st "

                                        Case "2"
                                            sSchedule &= "2nd "

                                        Case "3"
                                            sSchedule &= "3rd "

                                        Case Else
                                            sSchedule &= whichStr & "th "

                                    End Select

                                    Dim dayStr As String = dataStr.Substring(1, 1)
                                    Select Case dayStr
                                        Case "0"
                                            sSchedule &= "Sunday"

                                        Case "1"
                                            sSchedule &= "Monday"

                                        Case "2"
                                            sSchedule &= "Tuesday"

                                        Case "3"
                                            sSchedule &= "Wednesday"

                                        Case "4"
                                            sSchedule &= "Thursday"

                                        Case "5"
                                            sSchedule &= "Friday"

                                        Case Else
                                            sSchedule &= "Saturday"

                                    End Select

                                Case 4 ' Yearly
                                    sSchedule = dataStr.Substring(0, 2) & "/" & dataStr.Substring(2, 2) & " each year"

                                Case 5 ' One-Time
                                    sSchedule = dataStr.Substring(4, 2) & "/" & dataStr.Substring(6, 2) & "/" & dataStr.Substring(0, 4)

                            End Select

                            Dim timeStr As String = dr("DeliveryTime").ToString()
                            Dim hourStr As String = timeStr.Substring(0, 2)
                            Dim minStr As String = timeStr.Substring(2, 2)
                            Dim hour As Integer = CInt(hourStr)
                            If hour > 12 Then
                                sSchedule &= ", " & hour - 12 & ":" & minStr & "pm"
                            ElseIf hour = 12 Then
                                sSchedule &= ", " & hour & ":" & minStr & "pm"
                            Else
                                If hour = 0 Then hour = 12
                                sSchedule &= ", " & hour & ":" & minStr & "am"
                            End If

                            Dim drow As DataRow = dtSchedules.NewRow
                            drow("ScheduleID") = dr("ScheduleID")
                            drow("Schedule") = sSchedule
                            dtSchedules.Rows.Add(drow)

                        Catch ex As Exception
                        End Try
                    End While

                    dgSchedules.DataSource = dtSchedules
                    dgSchedules.DataBind()

                    hidePreviousSelection.Value = lstPredefinedMessages.SelectedValue
                    trEditSchedule.Visible = False
                    trSelectSchedule.Visible = True
                    trInstructions1.Visible = True
                    trInstructions2.Visible = False
                End If

                btnAddNew.Visible = True
            Else
                tblInstructions21.Visible = False
                tblInstructions22.Visible = False
                btnAddNew.Visible = False

                hidePreviousSelection.Value = lstPredefinedMessages.SelectedValue
                trEditSchedule.Visible = False
                trSelectSchedule.Visible = True
                trInstructions1.Visible = True
                trInstructions2.Visible = False
            End If

        Catch ex As SqlException
            lblErrors.Text = "There was an error trying to load data from the database.<br/><br/>" & ex.ToString

        Finally
            Try
                db.Close()
            Catch ex As Exception
            End Try

        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblErrors.Text = ""
        If Not Page.IsPostBack() Then
            'Get the store number from one of the prompt.asp files
            'TODO: There needs to be a check to determine if user has access to this store
            _store = Request.Form("store")
            If Len(_store) = 0 Then
                _store = "8888"
            End If

            If _store = "" Then
                lblStoreDisplay.Text = "No store was selected."
                hideStore.Value = ""
            Else
                lblStoreDisplay.Text = "Store " & _store
                hideStore.Value = _store
            End If
            trEditSchedule.Visible = False
            BindStaticData()
            BindMasterData()
        Else
            If Not Request.Form("lstPredefinedMessages") Is Nothing Then
                If Request.Form("lstPredefinedMessages") <> Request.Form("hidePreviousSelection") Then
                    BindMasterData()
                End If
            End If
        End If
        btnSave.Attributes.Add("onclick", "return validateData();")
        hideSMSLicense.Value = SMSLicensed().ToString()
    End Sub

    Protected Sub dgSchedules_ButtonClick(ByVal Sender As System.Object, ByVal E As System.Web.UI.WebControls.DataGridCommandEventArgs)
        Dim scheduleID As Integer = CInt(E.Item.Cells(0).Text)
        If E.CommandName.ToUpper = "EDIT" Then
            hideSchedule.Value = scheduleID

            Dim db As SqlConnection

            Try
                db = New SqlConnection(GetConnectString())
                db.Open()

                Dim sql As String = ""
                sql &= "SELECT      a.[MessageID], "
                sql &= "            b.[MessageText], "
                sql &= "            a.[DeliverSMS], "
                sql &= "            a.[SMSDestination], "
                sql &= "            a.[DeliverEmail], "
                sql &= "            a.[EmailDestination], "
                sql &= "            a.[DeliverStore], "
                sql &= "            a.[StoreDestination], "
                sql &= "            a.[MessageTimeout], "
                sql &= "            a.[AlertOnTimeout], "
                sql &= "            a.[AlertSMS], "
                sql &= "            a.[SMSAlertDestination], "
                sql &= "            a.[AlertEmail], "
                sql &= "            a.[EmailAlertDestination], "
                sql &= "            a.[DeliveryTime], "
                sql &= "            a.[DeliveryScheduleTypeID], "
                sql &= "            a.[DeliveryScheduleData], "
                sql &= "            a.[Limited], "
                sql &= "            a.[dtStartDate], "
                sql &= "            a.[dtEndDate] "
                sql &= "FROM        MT_MessageSchedules a "
                sql &= "JOIN        MT_PredefinedMessages b "
                sql &= "        ON  b.[MessageID] = a.[MessageID] "
                sql &= "WHERE       a.[ScheduleID] = " & scheduleID
                sql &= "ORDER BY    a.[MessageID]"

                Dim cmd As New SqlCommand(sql, db)
                Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)

                If dr.Read() Then
                    trCustomMessage.Visible = False
                    txtCustomMessage.Text = ""

                    trMessageText.Visible = True
                    lblMessageText.Text = dr("MessageText")

                    If CBool(dr("DeliverSMS")) Then
                        chkSMSDestination.Checked = True
                        txtSMSDestination.Text = dr("SMSDestination").ToString
                    Else
                        chkSMSDestination.Checked = False
                        txtSMSDestination.Text = ""
                    End If

                    If CBool(dr("DeliverEmail")) Then
                        chkEmailDestination.Checked = True
                        txtEmailDestination.Text = dr("EmailDestination").ToString
                    Else
                        chkEmailDestination.Checked = False
                        txtEmailDestination.Text = ""
                    End If

                    If CBool(dr("DeliverStore")) Then
                        chkStoreDestination.Checked = True
                        ddlManagers.SelectedValue = dr("StoreDestination")
                        txtTimeout.Text = dr("MessageTimeout")

                        If CBool(dr("AlertOnTimeout")) Then
                            chkAlertOnTimeout.Checked = True

                            If CBool(dr("AlertSMS")) Then
                                chkSMSTimeoutAlert.Checked = True
                                txtSMSTimeoutAlert.Text = dr("SMSAlertDestination").ToString
                            Else
                                chkSMSTimeoutAlert.Checked = False
                                txtSMSTimeoutAlert.Text = ""
                            End If

                            If CBool(dr("AlertEmail")) Then
                                chkEmailTimeoutAlert.Checked = True
                                txtEmailTimeoutAlert.Text = dr("EmailAlertDestination").ToString
                            Else
                                chkEmailTimeoutAlert.Checked = False
                                txtEmailTimeoutAlert.Text = ""
                            End If
                        Else
                            chkAlertOnTimeout.Checked = False
                            chkSMSTimeoutAlert.Checked = False
                            txtSMSTimeoutAlert.Text = ""
                            chkEmailTimeoutAlert.Checked = False
                            txtEmailTimeoutAlert.Text = ""
                        End If
                    Else
                        chkStoreDestination.Checked = False
                        ddlManagers.SelectedIndex = -1
                        txtTimeout.Text = ""
                        chkAlertOnTimeout.Checked = False
                        chkSMSTimeoutAlert.Checked = False
                        txtSMSTimeoutAlert.Text = ""
                        chkEmailTimeoutAlert.Checked = False
                        txtEmailTimeoutAlert.Text = ""
                    End If

                    Dim hourStr As String = dr("DeliveryTime").ToString.Substring(0, 2)
                    Dim minStr As String = dr("DeliveryTime").ToString.Substring(2, 2)
                    Dim hour As Integer = CInt(hourStr)
                    If hour > 12 Then
                        hour -= 12
                        ddlScheduleTimeAMPM.SelectedValue = 2
                    ElseIf hour = 12 Then
                        hour = 0
                        ddlScheduleTimeAMPM.SelectedValue = 2
                    Else
                        'If hour = 0 Then hour = 12
                        ddlScheduleTimeAMPM.SelectedValue = 1
                    End If
                    ddlScheduleTimeHour.SelectedValue = hour
                    ddlScheduleTimeMinute.SelectedValue = minStr

                    Select Case CInt(dr("DeliveryScheduleTypeID"))
                        Case 1 ' Weekly
                            rdoScheduleWeekly.Checked = True

                            Dim weekStr As String = dr("DeliveryScheduleData").ToString

                            If weekStr.Contains("0") Then
                                chkScheduleWeeklySunday.Checked = True
                            Else
                                chkScheduleWeeklySunday.Checked = False
                            End If

                            If weekStr.Contains("1") Then
                                chkScheduleWeeklyMonday.Checked = True
                            Else
                                chkScheduleWeeklyMonday.Checked = False
                            End If

                            If weekStr.Contains("2") Then
                                chkScheduleWeeklyTuesday.Checked = True
                            Else
                                chkScheduleWeeklyTuesday.Checked = False
                            End If

                            If weekStr.Contains("3") Then
                                chkScheduleWeeklyWednesday.Checked = True
                            Else
                                chkScheduleWeeklyWednesday.Checked = False
                            End If

                            If weekStr.Contains("4") Then
                                chkScheduleWeeklyThursday.Checked = True
                            Else
                                chkScheduleWeeklyThursday.Checked = False
                            End If

                            If weekStr.Contains("5") Then
                                chkScheduleWeeklyFriday.Checked = True
                            Else
                                chkScheduleWeeklyFriday.Checked = False
                            End If

                            If weekStr.Contains("6") Then
                                chkScheduleWeeklySaturday.Checked = True
                            Else
                                chkScheduleWeeklySaturday.Checked = False
                            End If

                        Case 2 ' Monthly by Date
                            rdoScheduleMonthlyDate.Checked = True
                            ddlScheduleMonthlyDate.SelectedValue = dr("DeliveryScheduleData").ToString

                        Case 3 ' Monthly by Weekday
                            rdoScheduleMonthlyWeekday.Checked = True
                            ddlScheduleMonthlyWeekdayWhich.SelectedValue = dr("DeliveryScheduleData").ToString.Substring(0, 1)
                            ddlScheduleMonthlyWeekday.SelectedValue = dr("DeliveryScheduleData").ToString.Substring(1, 1)

                        Case 4 ' Yearly
                            rdoScheduleYearly.Checked = True
                            ddlScheduleYearlyMonth.SelectedValue = CInt(dr("DeliveryScheduleData").ToString.Substring(0, 2)).ToString
                            ddlScheduleYearlyDate.SelectedValue = CInt(dr("DeliveryScheduleData").ToString.Substring(2, 2)).ToString

                        Case 5 ' One-time
                            rdoScheduleOneTime.Checked = True
                            ddlScheduleOneTimeMonth.SelectedValue = CInt(dr("DeliveryScheduleData").ToString.Substring(4, 2)).ToString
                            ddlScheduleOneTimeDate.SelectedValue = CInt(dr("DeliveryScheduleData").ToString.Substring(6, 2)).ToString
                            ddlScheduleOneTimeYear.SelectedValue = CInt(dr("DeliveryScheduleData").ToString.Substring(0, 4)).ToString

                    End Select

                    If CBool(dr("Limited")) Then
                        chkLimitedTime.Checked = True
                        Dim startDate As DateTime = CDate(dr("dtStartDate"))
                        ddlStartDateMonth.SelectedValue = startDate.Month
                        ddlStartDateDate.SelectedValue = startDate.Day
                        ddlStartDateYear.SelectedValue = startDate.Year
                        Dim endDate As DateTime = CDate(dr("dtEndDate"))
                        ddlEndDateMonth.SelectedValue = endDate.Month
                        ddlEndDateDate.SelectedValue = endDate.Day
                        ddlEndDateYear.SelectedValue = endDate.Year
                    Else
                        chkLimitedTime.Checked = False
                    End If
                End If

            Catch ex As Exception
                lblErrors.Text = "There was an error trying to load data from the database.<br/><br/>" & ex.ToString

            Finally
                Try
                    db.Close()
                Catch ex As Exception
                End Try

            End Try

            trEditSchedule.Visible = True
            trSelectSchedule.Visible = False
            trInstructions1.Visible = False
            trInstructions2.Visible = True

        ElseIf E.CommandName.ToUpper = "DELETE" Then
            Dim db As SqlConnection

            Try
                db = New SqlConnection(GetConnectString())
                db.Open()

                Dim sql As String = ""
                sql &= "DELETE      MT_MessageSchedules "
                sql &= "WHERE       [ScheduleID] = " & scheduleID

                Dim cmd As New SqlCommand(sql, db)
                cmd.ExecuteNonQuery()

                BindMasterData()

            Catch ex As Exception
                lblErrors.Text = "There was an error trying to delete data from the database.<br/><br/>" & ex.ToString

            Finally
                Try
                    db.Close()
                Catch ex As Exception
                End Try

            End Try
        End If
    End Sub

    Protected Sub SetupForAddNew()
        hideSchedule.Value = "NEW"

        If lstPredefinedMessages.SelectedValue = "0" Then
            trCustomMessage.Visible = True
            trMessageText.Visible = False
        Else
            trCustomMessage.Visible = False
            trMessageText.Visible = True
            lblMessageText.Text = lstPredefinedMessages.SelectedItem.Text
        End If
        txtCustomMessage.Text = ""
        chkSMSDestination.Checked = False
        txtSMSDestination.Text = ""
        chkEmailDestination.Checked = False
        txtEmailDestination.Text = ""
        chkStoreDestination.Checked = False
        ddlManagers.SelectedIndex = -1
        txtTimeout.Text = ""
        chkAlertOnTimeout.Checked = False
        chkSMSTimeoutAlert.Checked = False
        txtSMSTimeoutAlert.Text = ""
        chkEmailTimeoutAlert.Checked = False
        txtEmailTimeoutAlert.Text = ""
        ddlScheduleTimeHour.SelectedIndex = -1
        ddlScheduleTimeMinute.SelectedIndex = -1
        rdoScheduleWeekly.Checked = False
        chkScheduleWeeklySunday.Checked = False
        chkScheduleWeeklyMonday.Checked = False
        chkScheduleWeeklyTuesday.Checked = False
        chkScheduleWeeklyWednesday.Checked = False
        chkScheduleWeeklyThursday.Checked = False
        chkScheduleWeeklyFriday.Checked = False
        chkScheduleWeeklySaturday.Checked = False
        rdoScheduleMonthlyDate.Checked = False
        ddlScheduleMonthlyDate.SelectedIndex = -1
        rdoScheduleMonthlyWeekday.Checked = False
        ddlScheduleMonthlyWeekdayWhich.SelectedIndex = -1
        ddlScheduleMonthlyWeekday.SelectedIndex = -1
        rdoScheduleYearly.Checked = False
        ddlScheduleYearlyMonth.SelectedIndex = -1
        ddlScheduleYearlyDate.SelectedIndex = -1
        rdoScheduleOneTime.Checked = False
        ddlScheduleOneTimeMonth.SelectedIndex = -1
        ddlScheduleOneTimeDate.SelectedIndex = -1
        ddlScheduleOneTimeYear.SelectedIndex = -1
        chkLimitedTime.Checked = False
        ddlStartDateMonth.SelectedIndex = -1
        ddlStartDateDate.SelectedIndex = -1
        ddlStartDateYear.SelectedIndex = -1
        ddlEndDateMonth.SelectedIndex = -1
        ddlEndDateDate.SelectedIndex = -1
        ddlEndDateYear.SelectedIndex = -1

        trEditSchedule.Visible = True
        trSelectSchedule.Visible = False
        trInstructions1.Visible = False
        trInstructions2.Visible = True
    End Sub

    Protected Sub btnAddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        SetupForAddNew()
    End Sub

    Protected Function SMSLicensed() As Boolean
        Dim db As SqlConnection, licensed As Boolean = False

        Try
            db = New SqlConnection(GetConnectString())
            db.Open()

            Dim dtPredefinedMessages As New DataTable
            Dim sql As String = ""
            sql &= "SELECT      [value] "
            sql &= "FROM        InfoPOSOptions "
            sql &= "WHERE       [store] = " & _store & " "
            sql &= "        AND [type] = 'Xoikos' "
            sql &= "        AND [opt] = 'SubscribedTwilio' "
            Dim cmd As New SqlCommand(sql, db)
            Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            If dr.HasRows Then
                If dr.Read() Then
                    licensed = CBool(dr("Value"))
                End If
            End If

        Catch ex As Exception
            lblErrors.Text = "There was an error trying to load data from the database.<br/><br/>" & ex.ToString

        Finally
            Try
                db.Close()
            Catch ex As Exception
            End Try

        End Try

        Return licensed
    End Function

    Protected Function ValidateData() As Boolean
        If lstPredefinedMessages.SelectedValue = 0 Then
            If txtCustomMessage.Text.Trim.Length = 0 Then
                lblErrors.Text = "Missing Required Information: Custom Message Text"
                Return False
            End If
        End If

        If Not (chkSMSDestination.Checked Or chkEmailDestination.Checked Or chkStoreDestination.Checked) Then
            lblErrors.Text = "You must select at least one delivery option for this message."
            Return False
        End If

        If chkSMSDestination.Checked Then
            If Not SMSLicensed() Then
                lblErrors.Text = "You are not currently licensed to use SMS Message Delivery." & vbCrLf & "Please contact Xoikos Sales Support to enable this option."
                Return False
            End If

            If txtSMSDestination.Text.Trim.Length > 0 Then
                Dim smsDestinations As String() = txtSMSDestination.Text.Split(";")
                For Each dest As String In smsDestinations
                    Dim altDest As String = dest.Replace("-", "").Replace(".", "")
                    If Not IsNumeric(altDest) Then
                        lblErrors.Text = "SMS Delivery Destinations must be 10 digit numeric entries"
                        Return False
                    End If

                    If altDest.Length <> 10 Then
                        lblErrors.Text = "SMS Delivery Destinations must be 10 digit numeric entries"
                        Return False
                    End If
                Next
            Else
                lblErrors.Text = "Missing Required Information: SMS Message Delivery Destination"
                Return False
            End If
        End If

        If chkEmailDestination.Checked Then
            If txtEmailDestination.Text.Trim.Length > 0 Then
                Dim emailDestinations As String() = txtEmailDestination.Text.Split(";")
                For Each dest As String In emailDestinations
                    Dim atLoc As Integer = dest.IndexOf("@")
                    Dim dotLoc As Integer = dest.LastIndexOf(".")
                    If atLoc < 1 Or dotLoc < 1 Or dotLoc < atLoc Then
                        lblErrors.Text = "Email Delivery Destinations must conform to the pattern 'xxx@yyy.zzz'"
                        Return False
                    End If
                Next
            Else
                lblErrors.Text = "Missing Required Information: Email Message Delivery Destination"
                Return False
            End If
        End If

        If chkStoreDestination.Checked Then
            If ddlManagers.SelectedIndex = -1 Then
                lblErrors.Text = "Missing Required Information: Store Message Delivery Destination"
                Return False
            End If

            If txtTimeout.Text.Trim.Length > 0 Then
                If Not IsNumeric(txtTimeout.Text) Then
                    lblErrors.Text = "Store Message Timeout Value must be numeric"
                    Return False
                End If

                If CInt(txtTimeout.Text) > 120 Or CInt(txtTimeout.Text) < 1 Then
                    lblErrors.Text = "Store Message Timeout Value must be between 1 and 120 minutes"
                    Return False
                End If
            Else
                lblErrors.Text = "Missing Required Information: Store Message Timeout Value"
                Return False
            End If

            If chkAlertOnTimeout.Checked Then
                If Not (chkSMSTimeoutAlert.Checked Or chkEmailTimeoutAlert.Checked) Then
                    lblErrors.Text = "You must select at least one delivery option for your timeout alert."
                    Return False
                End If

                If chkSMSTimeoutAlert.Checked Then
                    If Not SMSLicensed() Then
                        lblErrors.Text = "You are not currently licensed to use SMS Alert Delivery." & vbCrLf & "Please contact Xoikos Sales Support to enable this option."
                        Return False
                    End If

                    If txtSMSTimeoutAlert.Text.Trim.Length > 0 Then
                        Dim altDest As String = txtSMSTimeoutAlert.Text.Trim.Replace("-", "").Replace(".", "")
                        If Not IsNumeric(altDest) Then
                            lblErrors.Text = "SMS Alert Destination must be a 10 digit numeric entry"
                            Return False
                        End If

                        If altDest.Length <> 10 Then
                            lblErrors.Text = "SMS Alert Destination must be a 10 digit numeric entry"
                            Return False
                        End If
                    Else
                        lblErrors.Text = "Missing Required Information: SMS Alert Delivery Destination"
                        Return False
                    End If
                End If

                If chkEmailTimeoutAlert.Checked Then
                    If txtEmailTimeoutAlert.Text.Trim.Length > 0 Then
                        Dim atLoc As Integer = txtEmailTimeoutAlert.Text.IndexOf("@")
                        Dim dotLoc As Integer = txtEmailTimeoutAlert.Text.LastIndexOf(".")
                        If atLoc < 1 Or dotLoc < 1 Or dotLoc < atLoc Then
                            lblErrors.Text = "Email Alert Destination must conform to the pattern 'xxx@yyy.zzz'"
                            Return False
                        End If
                    Else
                        lblErrors.Text = "Missing Required Information: Email Alert Delivery Destination"
                        Return False
                    End If
                End If
            End If
        End If

        If ddlScheduleTimeHour.SelectedIndex = -1 Then
            lblErrors.Text = "Missing Required Information: Schedule Time Hour"
            Return False
        End If

        If ddlScheduleTimeMinute.SelectedIndex = -1 Then
            lblErrors.Text = "Missing Required Information: Schedule Time Minute"
            Return False
        End If

        If ddlScheduleTimeAMPM.SelectedIndex = -1 Then
            lblErrors.Text = "Missing Required Information: Schedule Time AM/PM Selection"
            Return False
        End If

        If rdoScheduleWeekly.Checked Then
            If Not (chkScheduleWeeklyMonday.Checked Or chkScheduleWeeklyTuesday.Checked Or chkScheduleWeeklyWednesday.Checked Or chkScheduleWeeklyThursday.Checked Or chkScheduleWeeklyFriday.Checked Or chkScheduleWeeklySaturday.Checked Or chkScheduleWeeklySunday.Checked) Then
                lblErrors.Text = "At least one weekday must be selected for Weekly message delivery"
                Return False
            End If
        ElseIf rdoScheduleMonthlyDate.Checked Then
            If ddlScheduleMonthlyDate.SelectedIndex = -1 Then
                lblErrors.Text = "Missing Required Information: Monthly Delivery Date"
                Return False
            End If
        ElseIf rdoScheduleMonthlyWeekday.Checked Then
            If ddlScheduleMonthlyWeekdayWhich.SelectedIndex = -1 Then
                lblErrors.Text = "Missing Required Information: Monthly Delivery Weekday Instance"
                Return False
            End If

            If ddlScheduleMonthlyWeekday.SelectedIndex = -1 Then
                lblErrors.Text = "Missing Required Information: Monthly Delivery Weekday"
                Return False
            End If
        ElseIf rdoScheduleYearly.Checked Then
            If ddlScheduleYearlyMonth.SelectedIndex = -1 Then
                lblErrors.Text = "Missing Required Information: Yearly Delivery Month"
                Return False
            End If

            If ddlScheduleYearlyDate.SelectedIndex = -1 Then
                lblErrors.Text = "Missing Required Information: Yearly Delivery Date"
                Return False
            End If
        Else
            lblErrors.Text = "Missing Required Information: Schedule Date Selection"
            Return False
        End If

        Return True
    End Function

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim scheduleID As String = Request.Form("hideSchedule")

        Dim bFound As Boolean = False, predefinedMessageID As Int64 = 0, bNeedToRebindStatic As Boolean = False
        If lstPredefinedMessages.SelectedIndex <> 0 Then
            bFound = True
            predefinedMessageID = lstPredefinedMessages.SelectedValue
        Else
            Try
                Dim db As New SqlConnection(GetConnectString())
                db.Open()

                Dim sql As String = ""
                sql &= "SELECT      [MessageID] "
                sql &= "FROM        dbo.MT_PredefinedMessages "
                sql &= "WHERE       [MessageText] = '" & txtCustomMessage.Text.Trim & "'"

                Dim cmd As New SqlCommand(sql, db)
                Dim rdr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
                If rdr.Read() Then
                    bFound = True
                    predefinedMessageID = Convert.ToInt64(rdr("MessageID"))
                End If

            Catch ex As Exception
            End Try

            If Not bFound Then
                Try
                    Dim db As New SqlConnection(GetConnectString())
                    db.Open()

                    Dim sql As String = ""
                    sql &= "INSERT      dbo.MT_PredefinedMessages ("
                    sql &= "            [MessageText]) "
                    sql &= "VALUES ("
                    sql &= "            '" & txtCustomMessage.Text.Trim & "')"

                    Dim cmd As New SqlCommand(sql, db)
                    cmd.ExecuteNonQuery()

                    bNeedToRebindStatic = True

                Catch ex As Exception
                    lblErrors.Text = "There was an error trying to update data in the database.<br/><br/>" & ex.ToString
                End Try
            End If

            Try
                Dim db As New SqlConnection(GetConnectString())
                db.Open()

                Dim sql As String = ""
                sql &= "SELECT      [MessageID] "
                sql &= "FROM        dbo.MT_PredefinedMessages "
                sql &= "WHERE       [MessageText] = '" & txtCustomMessage.Text.Trim & "'"

                Dim cmd As New SqlCommand(sql, db)
                Dim rdr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
                If rdr.Read() Then
                    bFound = True ' it better be now, otherwise something's gone wrong
                    predefinedMessageID = Convert.ToInt64(rdr("MessageID"))
                End If

            Catch ex As Exception
            End Try
        End If

        If bFound Then
            If scheduleID = "NEW" Then
                Dim db As SqlConnection

                Try
                    db = New SqlConnection(GetConnectString())
                    db.Open()

                    Dim sql As String = ""
                    sql &= "INSERT      MT_MessageSchedules ( "
                    sql &= "            [StoreID], "
                    sql &= "            [MessageID], "
                    sql &= "            [DeliverSMS], "
                    sql &= "            [SMSDestination], "
                    sql &= "            [DeliverEmail], "
                    sql &= "            [EmailDestination], "
                    sql &= "            [DeliverStore], "
                    sql &= "            [StoreDestination], "
                    sql &= "            [MessageTimeout], "
                    sql &= "            [AlertOnTimeout], "
                    sql &= "            [AlertSMS], "
                    sql &= "            [SMSAlertDestination], "
                    sql &= "            [AlertEmail], "
                    sql &= "            [EmailAlertDestination], "
                    sql &= "            [DeliveryTime], "
                    sql &= "            [DeliveryScheduleTypeID], "
                    sql &= "            [DeliveryScheduleData], "
                    sql &= "            [Limited], "
                    sql &= "            [dtStartDate], "
                    sql &= "            [dtEndDate]) "
                    sql &= "VALUES ( "
                    sql &= "            " & _store & ", "
                    sql &= "            " & predefinedMessageID & ", "

                    If chkSMSDestination.Checked Then
                        sql &= "        1, "
                        sql &= "        '" & txtSMSDestination.Text & "', "
                    Else
                        sql &= "        0, "
                        sql &= "        NULL, "
                    End If

                    If chkEmailDestination.Checked Then
                        sql &= "        1, "
                        sql &= "        '" & txtEmailDestination.Text & "', "
                    Else
                        sql &= "        0, "
                        sql &= "        NULL, "
                    End If

                    If chkStoreDestination.Checked Then
                        sql &= "        1, "
                        sql &= "        '" & ddlManagers.SelectedValue & "', "
                        sql &= "        " & txtTimeout.Text & ", "
                        If chkAlertOnTimeout.Checked Then
                            sql &= "    1, "
                            If chkSMSTimeoutAlert.Checked Then
                                sql &= "1, "
                                sql &= "'" & txtSMSTimeoutAlert.Text & "', "
                            Else
                                sql &= "0, "
                                sql &= "NULL, "
                            End If
                            If chkEmailTimeoutAlert.Checked Then
                                sql &= "1, "
                                sql &= "'" & txtEmailTimeoutAlert.Text & "', "
                            Else
                                sql &= "0, "
                                sql &= "NULL, "
                            End If
                        Else
                            sql &= "    0, "
                            sql &= "    0, "
                            sql &= "    NULL, "
                            sql &= "    0, "
                            sql &= "    NULL, "
                        End If
                    Else
                        sql &= "        0, "
                        sql &= "        NULL, "
                        sql &= "        0, "
                        sql &= "        0, "
                        sql &= "        0, "
                        sql &= "        NULL, "
                        sql &= "        0, "
                        sql &= "        NULL, "
                    End If

                    Dim hour As Integer = CInt(ddlScheduleTimeHour.SelectedValue)
                    Dim minStr As String = ddlScheduleTimeMinute.SelectedValue
                    If ddlScheduleTimeAMPM.SelectedValue = 2 Then
                        hour += 12
                    End If
                    sql &= "            '" & hour.ToString.PadLeft(2, "0") & minStr & "', "

                    If rdoScheduleWeekly.Checked Then
                        sql &= "        1, "
                        Dim weekStr As String = ""
                        If chkScheduleWeeklySunday.Checked Then weekStr &= "0"
                        If chkScheduleWeeklyMonday.Checked Then weekStr &= "1"
                        If chkScheduleWeeklyTuesday.Checked Then weekStr &= "2"
                        If chkScheduleWeeklyWednesday.Checked Then weekStr &= "3"
                        If chkScheduleWeeklyThursday.Checked Then weekStr &= "4"
                        If chkScheduleWeeklyFriday.Checked Then weekStr &= "5"
                        If chkScheduleWeeklySaturday.Checked Then weekStr &= "6"
                        sql &= "        '" & weekStr & "', "
                    End If

                    If rdoScheduleMonthlyDate.Checked Then
                        sql &= "        2, "
                        sql &= "        '" & ddlScheduleMonthlyDate.SelectedValue & "', "
                    End If

                    If rdoScheduleMonthlyWeekday.Checked Then
                        sql &= "        3, "
                        sql &= "        '" & ddlScheduleMonthlyWeekdayWhich.SelectedValue & ddlScheduleMonthlyWeekday.SelectedValue & "', "
                    End If

                    If rdoScheduleYearly.Checked Then
                        sql &= "        4, "
                        sql &= "        '" & ddlScheduleYearlyMonth.SelectedValue.PadLeft(2, "0") & ddlScheduleYearlyDate.SelectedValue.PadLeft(2, "0") & "', "
                    End If

                    If rdoScheduleOneTime.Checked Then
                        sql &= "        5, "
                        sql &= "        '" & ddlScheduleOneTimeYear.SelectedValue & ddlScheduleOneTimeMonth.SelectedValue.PadLeft(2, "0") & ddlScheduleOneTimeDate.SelectedValue.PadLeft(2, "0") & "', "
                    End If

                    If chkLimitedTime.Checked Then
                        sql &= "        1, "
                        sql &= "        '" & ddlStartDateMonth.SelectedValue & "/" & ddlStartDateDate.SelectedValue & "/" & ddlStartDateYear.SelectedValue & "', "
                        sql &= "        '" & ddlEndDateMonth.SelectedValue & "/" & ddlEndDateDate.SelectedValue & "/" & ddlEndDateYear.SelectedValue & "') "
                    Else
                        sql &= "        0, "
                        sql &= "        NULL, "
                        sql &= "        NULL) "
                    End If

                    Dim cmd As New SqlCommand(sql, db)
                    cmd.ExecuteNonQuery()

                Catch ex As Exception
                    lblErrors.Text = "There was an error trying to insert data into the database.<br/><br/>" & ex.ToString

                Finally
                    Try
                        db.Close()
                    Catch ex As Exception
                    End Try

                End Try
            Else
                Dim db As SqlConnection

                Try
                    db = New SqlConnection(GetConnectString())
                    db.Open()

                    Dim sql As String = ""
                    sql &= "UPDATE      MT_MessageSchedules "
                    sql &= "SET         [StoreID] = " & _store & ", "
                    sql &= "            [MessageID] = " & predefinedMessageID & ", "

                    If chkSMSDestination.Checked Then
                        sql &= "        [DeliverSMS] = 1, "
                        sql &= "        [SMSDestination] = '" & txtSMSDestination.Text & "', "
                    Else
                        sql &= "        [DeliverSMS] = 0, "
                        sql &= "        [SMSDestination] = NULL, "
                    End If

                    If chkEmailDestination.Checked Then
                        sql &= "        [DeliverEmail] = 1, "
                        sql &= "        [EmailDestination] = '" & txtEmailDestination.Text & "', "
                    Else
                        sql &= "        [DeliverEmail] = 0, "
                        sql &= "        [EmailDestination] = NULL, "
                    End If

                    If chkStoreDestination.Checked Then
                        sql &= "        [DeliverStore] = 1, "
                        sql &= "        [StoreDestination] = '" & ddlManagers.SelectedValue & "', "
                        sql &= "        [MessageTimeout] = " & txtTimeout.Text & ", "
                        If chkAlertOnTimeout.Checked Then
                            sql &= "    [AlertOnTimeout] = 1, "
                            If chkSMSTimeoutAlert.Checked Then
                                sql &= "[AlertSMS] = 1, "
                                sql &= "[SMSAlertDestination] = '" & txtSMSTimeoutAlert.Text & "', "
                            Else
                                sql &= "[AlertSMS] = 0, "
                                sql &= "[SMSAlertDestination] = NULL, "
                            End If
                            If chkEmailTimeoutAlert.Checked Then
                                sql &= "[AlertEmail] = 1, "
                                sql &= "[EmailAlertDestination] = '" & txtEmailTimeoutAlert.Text & "', "
                            Else
                                sql &= "[AlertEmail] = 0, "
                                sql &= "[EmailAlertDestination] = NULL, "
                            End If
                        Else
                            sql &= "    [AlertOnTimeout] = 0, "
                            sql &= "    [AlertSMS] = 0, "
                            sql &= "    [SMSAlertDestination] = NULL, "
                            sql &= "    [AlertEmail] = 0, "
                            sql &= "    [EmailAlertDestination] = NULL, "
                        End If
                    Else
                        sql &= "        [DeliverStore] = 0, "
                        sql &= "        [StoreDestination] = NULL, "
                        sql &= "        [MessageTimeout] = 0, "
                        sql &= "        [AlertOnTimeout] = 0, "
                        sql &= "        [AlertSMS] = 0, "
                        sql &= "        [SMSAlertDestination] = NULL, "
                        sql &= "        [AlertEmail] = 0, "
                        sql &= "        [EmailAlertDestination] = NULL, "
                    End If

                    Dim hour As Integer = CInt(ddlScheduleTimeHour.SelectedValue)
                    Dim minStr As String = ddlScheduleTimeMinute.SelectedValue

                    If ddlScheduleTimeAMPM.SelectedValue = 2 Then
                        hour += 12
                        If hour = 24 Then hour = 0
                    End If

                    sql &= "            [DeliveryTime] = '" & hour.ToString.PadLeft(2, "0") & minStr & "', "

                    If rdoScheduleWeekly.Checked Then
                        sql &= "        [DeliveryScheduleTypeID] = 1, "
                        Dim weekStr As String = ""
                        If chkScheduleWeeklySunday.Checked Then weekStr &= "0"
                        If chkScheduleWeeklyMonday.Checked Then weekStr &= "1"
                        If chkScheduleWeeklyTuesday.Checked Then weekStr &= "2"
                        If chkScheduleWeeklyWednesday.Checked Then weekStr &= "3"
                        If chkScheduleWeeklyThursday.Checked Then weekStr &= "4"
                        If chkScheduleWeeklyFriday.Checked Then weekStr &= "5"
                        If chkScheduleWeeklySaturday.Checked Then weekStr &= "6"
                        sql &= "        [DeliveryScheduleData] = '" & weekStr & "', "
                    End If

                    If rdoScheduleMonthlyDate.Checked Then
                        sql &= "        [DeliveryScheduleTypeID] = 2, "
                        sql &= "        [DeliveryScheduleData] = '" & ddlScheduleMonthlyDate.SelectedValue & "', "
                    End If

                    If rdoScheduleMonthlyWeekday.Checked Then
                        sql &= "        [DeliveryScheduleTypeID] = 3, "
                        sql &= "        [DeliveryScheduleData] = '" & ddlScheduleMonthlyWeekdayWhich.SelectedValue & ddlScheduleMonthlyWeekday.SelectedValue & "', "
                    End If

                    If rdoScheduleYearly.Checked Then
                        sql &= "        [DeliveryScheduleTypeID] = 4, "
                        sql &= "        [DeliveryScheduleData] = '" & ddlScheduleYearlyMonth.SelectedValue.PadLeft(2, "0") & ddlScheduleYearlyDate.SelectedValue.PadLeft(2, "0") & "', "
                    End If

                    If rdoScheduleOneTime.Checked Then
                        sql &= "        [DeliveryScheduleTypeID] = 5, "
                        sql &= "        [DeliveryScheduleData] = '" & ddlScheduleOneTimeYear.SelectedValue & ddlScheduleOneTimeMonth.SelectedValue.PadLeft(2, "0") & ddlScheduleOneTimeDate.SelectedValue.PadLeft(2, "0") & "', "
                    End If

                    If chkLimitedTime.Checked Then
                        sql &= "        [Limited] = 1, "
                        sql &= "        [dtStartDate] = '" & ddlStartDateMonth.SelectedValue & "/" & ddlStartDateDate.SelectedValue & "/" & ddlStartDateYear.SelectedValue & "', "
                        sql &= "        [dtEndDate] = '" & ddlEndDateMonth.SelectedValue & "/" & ddlEndDateDate.SelectedValue & "/" & ddlEndDateYear.SelectedValue & "' "
                    Else
                        sql &= "        [Limited] = 0, "
                        sql &= "        [dtStartDate] = NULL, "
                        sql &= "        [dtEndDate] = NULL "
                    End If

                    sql &= "WHERE       [ScheduleID] = " & scheduleID

                    Dim cmd As New SqlCommand(sql, db)
                    cmd.ExecuteNonQuery()

                Catch ex As Exception
                    lblErrors.Text = "There was an error trying to update data in the database.<br/><br/>" & ex.ToString

                Finally
                    Try
                        db.Close()
                    Catch ex As Exception
                    End Try

                End Try
            End If

            If bNeedToRebindStatic Then BindStaticData()
            BindMasterData()
        End If
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGoBack.Click
        trEditSchedule.Visible = False
        trSelectSchedule.Visible = True
        trInstructions1.Visible = True
        trInstructions2.Visible = False
    End Sub

    Protected Sub dgSchedules_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgSchedules.ItemCreated
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim btnDelete As Button = CType(e.Item.Cells(3).Controls(0), Button)
            btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this schedule?');")
        End If
    End Sub
End Class
