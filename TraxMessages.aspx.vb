Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration
Imports System.Configuration
Imports DataAccess

Partial Class TraxMessages
    Inherits System.Web.UI.Page

    Protected WithEvents datalistMenu As System.Web.UI.WebControls.DataList

    Private Property _store() As String
        Get
            Return Convert.ToString(ViewState("Store"))
        End Get
        Set(ByVal Value As String)
            ViewState("Store") = Value
        End Set
    End Property

    Private Property _sortField() As String
        Get
            Return Convert.ToString(ViewState("SortField"))
        End Get
        Set(ByVal Value As String)
            ViewState("SortField") = Value
        End Set
    End Property

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub


    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lblErrors.Text = ""
        If Not Page.IsPostBack() Then
            'Get the store number from one of the prompt.asp files
            'TODO: There needs to be a check to determine if user has access to this store
            _store = Request.Form("store")
            If Len(_store) = 0 Then
                _store = "999999"
            End If

            _sortField = "ScheduleID"           'Default sort field
            If _store = "" Then
                lblStoreDisplay.Text = "No store was selected."
                hideStore.Value = ""
            Else
                lblStoreDisplay.Text = "Store Number: " & _store
                hideStore.Value = _store
                BindMasterData()
            End If
        End If
    End Sub

    Private Sub BindMasterData()
        Dim db As New SqlConnection(GetConnectString())
        db.Open()

        Try
            Dim sql As String = ""
            sql &= "SELECT      ts.scheduleID as 'ScheduleID', "
            sql &= "            ts.schedulerID as 'SchedulerID', "
            sql &= "            ts.typeID as 'TypeID', "
            sql &= "            ts.dateParameter as 'DateParameter', "
            sql &= "            SUBSTRING(ts.startTime,1,2) + ':' + SUBSTRING(ts.startTime,3,2) as 'StartTime', "
            sql &= "            SUBSTRING(ts.endTime,1,2) + ':' + SUBSTRING(ts.endTime,3,2) as 'EndTime', "
            sql &= "            ts.message as 'Message', "
            sql &= "            ts.numberOfProducts as 'NumberOfMessages', "
            sql &= "            ts.generateEvery as 'GenerateEvery', "
            sql &= "            CONVERT(varchar(3),ts.generateEvery) + ' mins' as 'GenerateEveryText' "
            sql &= "FROM        TraxSchedules ts "
            sql &= "WHERE       ts.client = 1 "
            sql &= "        AND ts.store = " & _store & " "
            sql &= "        AND ts.productID = 0 "
            sql &= "        AND ts.dtDeleted IS NULL "
            sql &= "ORDER BY " & _sortField

            Dim cmd As New SqlCommand(sql, db)
            Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            Dim dt As DataTable = New DataTable()

            dt.Load(dr)

            dt.Columns.Add("Scheduled")

            For Each row As DataRow In dt.Rows
                Dim scheduled As String = ""
                Select Case CInt(row("TypeID"))
                    Case 1 ' Weekly
                        scheduled = row("DateParameter").ToString.Trim.Replace("0", "Sun/").Replace("1", "Mon/").Replace("2", "Tue/").Replace("3", "Wed/").Replace("4", "Thu/").Replace("5", "Fri/").Replace("6", "Sat/")
                        scheduled = scheduled.Substring(0, scheduled.Length - 1)
                        scheduled &= " of every week"

                    Case 2 ' Monthly Ordinal
                        scheduled = row("DateParameter").ToString.Trim
                        Select Case scheduled
                            Case "0"
                                scheduled = "Last"

                            Case "1", "21"
                                scheduled &= "st"

                            Case "2", "22"
                                scheduled &= "nd"

                            Case "3", "23"
                                scheduled &= "rd"

                            Case Else
                                scheduled &= "th"

                        End Select
                        scheduled &= " of every month"

                    Case 3 ' Monthly Day of Week
                        Select Case row("DateParameter").ToString.Trim.Substring(0, 1)
                            Case "1"
                                scheduled = "1st"

                            Case "2"
                                scheduled = "2nd"

                            Case "3"
                                scheduled = "3rd"

                            Case "4"
                                scheduled = "4th"

                        End Select

                        scheduled &= " "

                        Select Case row("DateParameter").ToString.Trim.Substring(1, 1)
                            Case "0"
                                scheduled &= "Sunday"

                            Case "1"
                                scheduled &= "Monday"

                            Case "2"
                                scheduled &= "Tuesday"

                            Case "3"
                                scheduled &= "Wednesday"

                            Case "4"
                                scheduled &= "Thursday"

                            Case "5"
                                scheduled &= "Friday"

                            Case "6"
                                scheduled &= "Saturday"

                        End Select

                        scheduled &= " of every month"

                    Case 4 ' Yearly
                        scheduled = row("DateParameter").ToString.Trim & " of every year"

                    Case 5 ' One-time
                        scheduled = row("DateParameter")

                End Select
                row("Scheduled") = scheduled
            Next

            dgSchedules.DataSource = dt
            dgSchedules.DataBind()

        Catch ex As SqlException
            lblErrors.Text = "There was an error trying to load data from the database.<br/><br/>" & ex.ToString

        Finally
            db.Close()
        End Try
    End Sub

    Sub dgProducts_NewPage(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        dgSchedules.CurrentPageIndex = e.NewPageIndex
        BindMasterData()
    End Sub

    Sub dgProducts_Sort(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        _sortField = e.SortExpression
        BindMasterData()
    End Sub

    Private Sub dgSchedules_ItemDataBound(ByVal sender As Object, ByVal e As DataGridItemEventArgs) Handles dgSchedules.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onclick", "loadItems(this,'" & e.Item.DataItem("ScheduleID") & "','" & e.Item.DataItem("SchedulerID") & "','" & e.Item.DataItem("TypeID") & "','" & e.Item.DataItem("DateParameter") & "','" & e.Item.DataItem("StartTime") & "','" & e.Item.DataItem("EndTime") & "','" & e.Item.DataItem("Message") & "','" & e.Item.DataItem("GenerateEvery") & "');")
            e.Item.Attributes.Add("onmouseover", "mouseover(this)")
            e.Item.Attributes.Add("onmouseout", "mouseout(this)")
        End If
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Dim store As String = Request.Form("hideStore")
        Dim schedulerID As String = Request.Form("ddlSchedulerID")
        Dim startTime As String = Request.Form("ddlStartTimeHour") & Request.Form("ddlStartTimeMinute")
        Dim endTime As String = Request.Form("ddlEndTimeHour") & Request.Form("ddlEndTimeMinute")
        Dim message As String = Request.Form("txtMessage")
        Dim generateEvery As String = Request.Form("ddlGenerateEvery")

        Dim selectedPane As String = Request.Form("accEdit_AccordionExtender_ClientState")
        Dim typeID As Integer, dateParameter As String = ""
        Select Case selectedPane
            Case "0"
                typeID = 1
                If Not Request.Form("apWeekly_content:chkWeeklySunday") Is Nothing Then dateParameter &= "0"
                If Not Request.Form("apWeekly_content:chkWeeklyMonday") Is Nothing Then dateParameter &= "1"
                If Not Request.Form("apWeekly_content:chkWeeklyTuesday") Is Nothing Then dateParameter &= "2"
                If Not Request.Form("apWeekly_content:chkWeeklyWednesday") Is Nothing Then dateParameter &= "3"
                If Not Request.Form("apWeekly_content:chkWeeklyThursday") Is Nothing Then dateParameter &= "4"
                If Not Request.Form("apWeekly_content:chkWeeklyFriday") Is Nothing Then dateParameter &= "5"
                If Not Request.Form("apWeekly_content:chkWeeklySaturday") Is Nothing Then dateParameter &= "6"
                If dateParameter.Trim.Length = 0 Then
                    lblErrors.Text = "You Must Select At Least One Day"
                    Return
                End If

            Case "1"
                typeID = 2
                dateParameter = Request.Form("apMonthlyOrdinal_content:ddlMonthlyDate")

            Case "2"
                typeID = 3
                dateParameter = Request.Form("apMonthlyDayOfWeek_content:ddlMonthlyDayWhich") & Request.Form("apMonthlyDayOfWeek_content:ddlMonthlyDayOfWeek")

            Case "3"
                typeID = 4
                Dim month As String = Request.Form("apYearly_content:ddlYearlyMonth")
                Dim day As String = Request.Form("apYearly_content:ddlYearlyDay")
                Select Case month
                    Case "4", "6", "9", "11"
                        If day > "30" Then
                            lblErrors.Text = "Invalid Yearly Date"
                            Return
                        End If

                    Case "2"
                        If day > "28" Then
                            lblErrors.Text = "Invalid Yearly Date"
                            Return
                        End If

                End Select

                dateParameter = Request.Form("apYearly_content:ddlYearlyMonth") & "/" & Request.Form("apYearly_content:ddlYearlyDay")

        End Select

        Dim db As New SqlConnection(GetConnectString())
        db.Open()
        Try
            If endTime < startTime Then
                lblErrors.Text = "Start Time Must Precede End Time"
                Return
            End If

            Dim startTimeDate As DateTime = CDate(Now.ToString("M/d/yyyy") & " " & Request.Form("ddlStartTimeHour") & ":" & Request.Form("ddlStartTimeMinute"))
            Dim endTimeDate As DateTime = CDate(Now.ToString("M/d/yyyy") & " " & Request.Form("ddlEndTimeHour") & ":" & Request.Form("ddlEndTimeMinute"))
            Dim numberOfMessages As Integer = DateDiff(DateInterval.Minute, startTimeDate, endTimeDate) / generateEvery

            Dim sql As String = "SELECT     MAX(scheduleID) " & _
                                "FROM       TraxSchedules " & _
                                "WHERE      client = 1 " & _
                                "       AND store = " & _store

            Dim cmd As New SqlCommand(sql, db)
            Dim retVal As String = ""
            Try
                retVal = cmd.ExecuteScalar.ToString()
            Catch ex As SqlException
            End Try

            Dim newScheduleID As Integer = 1
            If retVal.Length > 0 Then
                newScheduleID = CInt(retVal) + 1
            End If

            sql = "INSERT TraxSchedules (client, store, scheduleID, schedulerID, typeID, dateParameter, startTime, endTime, productID, generateEvery, numberOfProducts, message, dtCreated, dtUpdated) " & _
                  "VALUES (1, " & _store & ", " & newScheduleID & ", " & schedulerID & ", " & typeID & ", '" & dateParameter & "', '" & startTime & "', '" & endTime & "', 0, " & generateEvery & ", " & numberOfMessages & ", '" & message & "', GETDATE(), GETDATE())"
            cmd = New SqlCommand(sql, db)
            cmd.ExecuteNonQuery()

        Catch ex As SqlException
            lblErrors.Text = "Error Adding New Message<br/><br/>" & ex.ToString

        Finally
            db.Close()
        End Try

        BindMasterData()
    End Sub

    Private Function GetConnectString() As String
        'Return "Data Source=.\SQLEXPRESS;Initial Catalog=WebsiteTesting;UID=IKAdmin;PWD=3kMGb4sds;Max Pool Size=500;"
        Dim data1 As New DataAccess()
        Return data1.UnMaskString(System.Web.Configuration.WebConfigurationManager.ConnectionStrings("Production").ConnectionString, 1, 2, 3, 4, 5)
    End Function

    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Dim store As String = Request.Form("hideStore")
        Dim scheduleID As String = Request.Form("hideScheduleID")
        Dim schedulerID As String = Request.Form("ddlSchedulerID")
        Dim startTime As String = Request.Form("ddlStartTimeHour") & Request.Form("ddlStartTimeMinute")
        Dim endTime As String = Request.Form("ddlEndTimeHour") & Request.Form("ddlEndTimeMinute")
        Dim message As String = Request.Form("txtMessage")
        Dim generateEvery As String = Request.Form("ddlGenerateEvery")

        Dim selectedPane As String = Request.Form("accEdit_AccordionExtender_ClientState")
        Dim typeID As Integer, dateParameter As String = ""
        Select Case selectedPane
            Case "0"
                typeID = 1
                If Not Request.Form("apWeekly_content:chkWeeklySunday") Is Nothing Then dateParameter &= "0"
                If Not Request.Form("apWeekly_content:chkWeeklyMonday") Is Nothing Then dateParameter &= "1"
                If Not Request.Form("apWeekly_content:chkWeeklyTuesday") Is Nothing Then dateParameter &= "2"
                If Not Request.Form("apWeekly_content:chkWeeklyWednesday") Is Nothing Then dateParameter &= "3"
                If Not Request.Form("apWeekly_content:chkWeeklyThursday") Is Nothing Then dateParameter &= "4"
                If Not Request.Form("apWeekly_content:chkWeeklyFriday") Is Nothing Then dateParameter &= "5"
                If Not Request.Form("apWeekly_content:chkWeeklySaturday") Is Nothing Then dateParameter &= "6"
                If dateParameter.Trim.Length = 0 Then
                    lblErrors.Text = "You Must Select At Least One Day"
                    Return
                End If

            Case "1"
                typeID = 2
                dateParameter = Request.Form("apMonthlyOrdinal_content:ddlMonthlyDate")

            Case "2"
                typeID = 3
                dateParameter = Request.Form("apMonthlyDayOfWeek_content:ddlMonthlyDayWhich") & Request.Form("apMonthlyDayOfWeek_content:ddlMonthlyDayOfWeek")

            Case "3"
                typeID = 4
                Dim month As String = Request.Form("apYearly_content:ddlYearlyMonth")
                Dim day As String = Request.Form("apYearly_content:ddlYearlyDay")
                Select Case month
                    Case "4", "6", "9", "11"
                        If day > "30" Then
                            lblErrors.Text = "Invalid Yearly Date"
                            Return
                        End If

                    Case "2"
                        If day > "28" Then
                            lblErrors.Text = "Invalid Yearly Date"
                            Return
                        End If

                End Select

                dateParameter = month & "/" & day

        End Select

        Dim db As New SqlConnection(GetConnectString())
        db.Open()
        Try
            If endTime < startTime Then
                lblErrors.Text = "Start Time Must Precede End Time"
                Return
            End If

            Dim startTimeDate As DateTime = CDate(Now.ToString("M/d/yyyy") & " " & Request.Form("ddlStartTimeHour") & ":" & Request.Form("ddlStartTimeMinute"))
            Dim endTimeDate As DateTime = CDate(Now.ToString("M/d/yyyy") & " " & Request.Form("ddlEndTimeHour") & ":" & Request.Form("ddlEndTimeMinute"))
            Dim numberOfMessages As Integer = DateDiff(DateInterval.Minute, startTimeDate, endTimeDate) / generateEvery

            Dim sql As String = ""
            sql &= "UPDATE      TraxSchedules "
            sql &= "SET         schedulerID = " & schedulerID & ", "
            sql &= "            typeID = " & typeID & ", "
            sql &= "            dateParameter = '" & dateParameter & "', "
            sql &= "            startTime = '" & startTime & "', "
            sql &= "            endTime = '" & endTime & "', "
            sql &= "            message = '" & message & "', "
            sql &= "            generateEvery = " & generateEvery & ", "
            sql &= "            numberOfProducts = " & numberOfMessages & ", "
            sql &= "            dtUpdated = GETDATE() "
            sql &= "WHERE       client = 1 "
            sql &= "        AND store = " & _store & " "
            sql &= "        AND scheduleID = " & scheduleID

            Dim cmd As New SqlCommand(sql, db)
            cmd.ExecuteNonQuery()

        Catch ex As SqlException
            lblErrors.Text = "Error Updating Message<br/><br/>" & ex.ToString

        Finally
            db.Close()
        End Try

        BindMasterData()
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim scheduleID As String = Request.Form("hideScheduleID")

        Dim db As New SqlConnection(GetConnectString())
        db.Open()
        Try
            Dim sql As String = ""
            sql &= "UPDATE     TraxSchedules "
            sql &= "SET        dtDeleted = GETDATE(), "
            sql &= "           dtUpdated = GETDATE() "
            sql &= "WHERE      client = 1 "
            sql &= "       AND store = " & _store & " "
            sql &= "       AND scheduleID = " & scheduleID

            Dim cmd As New SqlCommand(sql, db)
            cmd.ExecuteNonQuery()

        Catch ex As Exception
            lblErrors.Text = "Error Deleting Message<br/><br/>" & ex.ToString

        Finally
            db.Close()
        End Try

        BindMasterData()
    End Sub
End Class
