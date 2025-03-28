Imports System.Data.SqlClient

Partial Class SmartTraxScheduleByDay
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
        Dim db As New SqlConnection(GetConnectString())

        Try
            Dim sql As String = ""
            sql &= "SELECT      [OpenHHMM], "
            sql &= "            [CloseHHMM] "
            sql &= "FROM        dbo.Stores "
            sql &= "WHERE       [store] = " & _store

            db.Open()
            Dim cmd As New SqlCommand(sql, db)
            Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            If dr.Read Then
                Dim startHour As String = dr("OpenHHMM").ToString.Substring(0, 2)
                Dim endHour As String = dr("CloseHHMM").ToString.Substring(0, 2)

                For I As Integer = 2 To CInt(startHour)
                    dgSchedules.Columns(I).Visible = False
                Next

                For I As Integer = CInt(endHour) + 3 To 25
                    dgSchedules.Columns(I).Visible = False
                Next
            End If

            Try
                db.Close()
            Catch ex As Exception
            End Try

            sql = ""
            sql &= "SELECT      a.[productID], "
            sql &= "            a.[PLU], "
            sql &= "            b.[Desc] as 'pluDesc', "
            sql &= "            a.[productType], "
            sql &= "            a.[upgradePLU],  "
            sql &= "            c.[Desc] as 'upgradeDesc' "
            sql &= "FROM        dbo.ST_Products a "
            sql &= "JOIN        dbo.PLUs b "
            sql &= "        ON  b.[store] = a.[store] "
            sql &= "        AND b.[PLU] = a.[PLU] "
            sql &= "LEFT JOIN   dbo.PLUs c "
            sql &= "        ON  c.[store] = a.[store] "
            sql &= "        AND c.[PLU] = a.[upgradePLU] "
            sql &= "WHERE       a.[store] = " & _store & " "
            sql &= "        AND a.[dtDeleted] IS NULL "
            sql &= "ORDER BY    a.[productID] "

            db.Open()
            cmd = New SqlCommand(sql, db)
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            Dim dtProducts As New DataTable
            dtProducts.Columns.Add("productID")
            dtProducts.Columns.Add("description")

            Dim row As DataRow = dtProducts.NewRow()
            row("productID") = 0
            row("description") = "[select product]"
            dtProducts.Rows.Add(row)

            While dr.Read()
                Try
                    row = dtProducts.NewRow()
                    row("productID") = dr("productID")
                    Select Case CInt(dr("productType"))
                        Case 1
                            row("description") = dr("pluDesc") & ", upgrading " & dr("upgradeDesc")

                        Case 2
                            row("description") = dr("pluDesc") & ", new item"

                        Case 3
                            row("description") = dr("pluDesc") & ", add-on item"

                        Case Else
                            Throw New Exception("Unknown Product Type " & row("productType"))

                    End Select

                    dtProducts.Rows.Add(row)

                Catch ex As Exception
                    ' should probably log this somehow
                End Try
            End While

            ddlNewProduct.DataSource = dtProducts
            ddlNewProduct.DataValueField = "productID"
            ddlNewProduct.DataTextField = "description"
            ddlNewProduct.DataBind()

            Try
                db.Close()
            Catch ex As Exception
            End Try

            Dim dtDates As New DataTable
            dtDates.Columns.Add("yyyymmdd")
            dtDates.Columns.Add("description")

            row = dtDates.NewRow()
            row("yyyymmdd") = 0
            row("description") = "[select date]"
            dtDates.Rows.Add(row)

            Dim curDate As Date = Now
            For I As Integer = 1 To 84
                row = dtDates.NewRow()
                row("yyyymmdd") = curDate.ToString("yyyyMMdd")
                row("description") = curDate.ToString("MM/dd/yyyy")

                dtDates.Rows.Add(row)
                curDate = curDate.AddDays(1)
            Next

            ddlDays.DataSource = dtDates
            ddlDays.DataValueField = "yyyymmdd"
            ddlDays.DataTextField = "description"
            ddlDays.DataBind()

        Catch ex As Exception
            lblErrors.Text = "There was an error trying to load data from the database.<br/><br/>" & ex.ToString

        Finally
            Try
                db.Close()
            Catch ex As Exception
            End Try
        End Try

    End Sub

    Private Sub BindMasterData()
        If ddlDays.SelectedIndex > 0 Then
            Dim db As New SqlConnection(GetConnectString())

            Try
                Dim dtSchedules As New DataTable
                dtSchedules.Columns.Add("ProductID")
                dtSchedules.Columns.Add("Product")
                dtSchedules.Columns.Add("Hour00")
                dtSchedules.Columns.Add("Hour01")
                dtSchedules.Columns.Add("Hour02")
                dtSchedules.Columns.Add("Hour03")
                dtSchedules.Columns.Add("Hour04")
                dtSchedules.Columns.Add("Hour05")
                dtSchedules.Columns.Add("Hour06")
                dtSchedules.Columns.Add("Hour07")
                dtSchedules.Columns.Add("Hour08")
                dtSchedules.Columns.Add("Hour09")
                dtSchedules.Columns.Add("Hour10")
                dtSchedules.Columns.Add("Hour11")
                dtSchedules.Columns.Add("Hour12")
                dtSchedules.Columns.Add("Hour13")
                dtSchedules.Columns.Add("Hour14")
                dtSchedules.Columns.Add("Hour15")
                dtSchedules.Columns.Add("Hour16")
                dtSchedules.Columns.Add("Hour17")
                dtSchedules.Columns.Add("Hour18")
                dtSchedules.Columns.Add("Hour19")
                dtSchedules.Columns.Add("Hour20")
                dtSchedules.Columns.Add("Hour21")
                dtSchedules.Columns.Add("Hour22")
                dtSchedules.Columns.Add("Hour23")
                dtSchedules.Columns.Add("Percentage")

                Dim sql As String = ""
                sql &= "SELECT      [productID], "
                sql &= "            [hour00], "
                sql &= "            [hour01], "
                sql &= "            [hour02], "
                sql &= "            [hour03], "
                sql &= "            [hour04], "
                sql &= "            [hour05], "
                sql &= "            [hour06], "
                sql &= "            [hour07], "
                sql &= "            [hour08], "
                sql &= "            [hour09], "
                sql &= "            [hour10], "
                sql &= "            [hour11], "
                sql &= "            [hour12], "
                sql &= "            [hour13], "
                sql &= "            [hour14], "
                sql &= "            [hour15], "
                sql &= "            [hour16], "
                sql &= "            [hour17], "
                sql &= "            [hour18], "
                sql &= "            [hour19], "
                sql &= "            [hour20], "
                sql &= "            [hour21], "
                sql &= "            [hour22], "
                sql &= "            [hour23], "
                sql &= "            [percentage] "
                sql &= "FROM        dbo.ST_Schedules "
                sql &= "WHERE       [store] = " & _store & " "
                sql &= "        AND [yyyymmdd] = '" & ddlDays.SelectedValue & "' "

                db.Open()
                Dim cmd As New SqlCommand(sql, db)
                Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)

                While dr.Read
                    Dim row As DataRow = dtSchedules.NewRow

                    row("ProductID") = CInt(dr("productID"))

                    row("Hour00") = 0
                    row("Hour01") = 0
                    row("Hour02") = 0
                    row("Hour03") = 0
                    row("Hour04") = 0
                    row("Hour05") = 0
                    row("Hour06") = 0
                    row("Hour07") = 0
                    row("Hour08") = 0
                    row("Hour09") = 0
                    row("Hour10") = 0
                    row("Hour11") = 0
                    row("Hour12") = 0
                    row("Hour13") = 0
                    row("Hour14") = 0
                    row("Hour15") = 0
                    row("Hour16") = 0
                    row("Hour17") = 0
                    row("Hour18") = 0
                    row("Hour19") = 0
                    row("Hour20") = 0
                    row("Hour21") = 0
                    row("Hour22") = 0
                    row("Hour23") = 0
                    row("Percentage") = 0

                    If Not IsDBNull(dr("hour00")) Then row("Hour00") = CInt(dr("hour00"))
                    If Not IsDBNull(dr("hour01")) Then row("Hour01") = CInt(dr("hour01"))
                    If Not IsDBNull(dr("hour02")) Then row("Hour02") = CInt(dr("hour02"))
                    If Not IsDBNull(dr("hour03")) Then row("Hour03") = CInt(dr("hour03"))
                    If Not IsDBNull(dr("hour04")) Then row("Hour04") = CInt(dr("hour04"))
                    If Not IsDBNull(dr("hour05")) Then row("Hour05") = CInt(dr("hour05"))
                    If Not IsDBNull(dr("hour06")) Then row("Hour06") = CInt(dr("hour06"))
                    If Not IsDBNull(dr("hour07")) Then row("Hour07") = CInt(dr("hour07"))
                    If Not IsDBNull(dr("hour08")) Then row("Hour08") = CInt(dr("hour08"))
                    If Not IsDBNull(dr("hour09")) Then row("Hour09") = CInt(dr("hour09"))
                    If Not IsDBNull(dr("hour10")) Then row("Hour10") = CInt(dr("hour10"))
                    If Not IsDBNull(dr("hour11")) Then row("Hour11") = CInt(dr("hour11"))
                    If Not IsDBNull(dr("hour12")) Then row("Hour12") = CInt(dr("hour12"))
                    If Not IsDBNull(dr("hour13")) Then row("Hour13") = CInt(dr("hour13"))
                    If Not IsDBNull(dr("hour14")) Then row("Hour14") = CInt(dr("hour14"))
                    If Not IsDBNull(dr("hour15")) Then row("Hour15") = CInt(dr("hour15"))
                    If Not IsDBNull(dr("hour16")) Then row("Hour16") = CInt(dr("hour16"))
                    If Not IsDBNull(dr("hour17")) Then row("Hour17") = CInt(dr("hour17"))
                    If Not IsDBNull(dr("hour18")) Then row("Hour18") = CInt(dr("hour18"))
                    If Not IsDBNull(dr("hour19")) Then row("Hour19") = CInt(dr("hour19"))
                    If Not IsDBNull(dr("hour20")) Then row("Hour20") = CInt(dr("hour20"))
                    If Not IsDBNull(dr("hour21")) Then row("Hour21") = CInt(dr("hour21"))
                    If Not IsDBNull(dr("hour22")) Then row("Hour22") = CInt(dr("hour22"))
                    If Not IsDBNull(dr("hour23")) Then row("Hour23") = CInt(dr("hour23"))
                    If Not IsDBNull(dr("percentage")) Then row("Percentage") = CInt(dr("percentage"))

                    dtSchedules.Rows.Add(row)
                End While

                Try
                    db.Close()
                Catch ex As Exception
                End Try

                For Each row As DataRow In dtSchedules.Rows
                    sql = ""
                    sql &= "SELECT      b.[Desc] as 'pluDesc', "
                    sql &= "            a.[productType], "
                    sql &= "            c.[Desc] as 'upgradeDesc' "
                    sql &= "FROM        dbo.ST_Products a "
                    sql &= "JOIN        dbo.PLUs b "
                    sql &= "        ON  b.[store] = a.[store] "
                    sql &= "        AND b.[PLU] = a.[PLU] "
                    sql &= "LEFT JOIN   dbo.PLUs c "
                    sql &= "        ON  c.[store] = a.[store] "
                    sql &= "        AND c.[PLU] = a.[upgradePLU] "
                    sql &= "WHERE       a.[store] = " & _store & " "
                    sql &= "        AND a.[productID] = " & row("ProductID") & " "

                    db.Open()
                    cmd = New SqlCommand(sql, db)
                    dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)

                    If dr.Read() Then
                        Dim desc As String = dr("pluDesc") & "<br />"
                        Select Case CInt(dr("productType"))
                            Case 1 ' upgrade
                                desc &= "<small>upgrading " & dr("upgradeDesc") & "</small>"

                            Case 2 ' new item
                                desc &= "<small>new item</small>"

                            Case 3 ' add-on 
                                desc &= "<small>add-on item</small>"

                            Case Else
                                desc &= "<small>unknown product type</small>"

                        End Select
                        row("Product") = desc
                    Else
                        row("Product") = "Unknown Product"
                    End If

                    row.AcceptChanges()
                    dtSchedules.AcceptChanges()

                    Try
                        db.Close()
                    Catch ex As Exception
                    End Try
                Next

                dgSchedules.DataSource = dtSchedules
                dgSchedules.DataBind()
                dgSchedules.Visible = True

                btnAddNew.Visible = True

            Catch ex As Exception
                lblErrors.Text = "There was an error trying to load data from the database.<br/><br/>" & ex.ToString

            Finally
                Try
                    db.Close()
                Catch ex As Exception
                End Try
            End Try
        Else
            dgSchedules.Visible = False
            btnAddNew.Visible = False
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblErrors.Text = ""
        If Not Page.IsPostBack() Then
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
            BindStaticData()
        Else
            If Request.Form("__EVENTTARGET") = "dgSchedules_ItemClick" Then
                dgSchedules_EditSchedule(Request.Form("__EVENTARGUMENT"))
            End If
        End If
    End Sub

    Private Sub dgSchedules_EditSchedule(ByVal arg As String)
        Dim productID As String = arg.Substring(0, arg.IndexOf("|"))
        Dim hour As String = arg.Substring(arg.IndexOf("|") + 1)

        Dim db As New SqlConnection(GetConnectString())

        Try
            Dim sql As String = ""
            sql &= "SELECT      [Hour" & hour & "] "
            sql &= "FROM        dbo.ST_Schedules "
            sql &= "WHERE       [store] = " & _store & " "
            sql &= "        AND [productID] = " & productID & " "
            sql &= "        AND [yyyymmdd] = '" & ddlDays.SelectedValue & "' "

            db.Open()
            Dim cmd As New SqlCommand(sql, db)
            Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            hideEditProduct.Value = productID
            hideEditHour.Value = hour
            litModalTitle.Text = "Edit Product, hour " & hour
            If dr.Read() Then
                ddlScheduledProducts.SelectedValue = CInt(dr(0))
            Else
                ddlScheduledProducts.SelectedValue = 0
            End If
            upnEditSchedule.Update()
            mpeEditSchedule.Show()

        Catch ex As Exception
            lblErrors.Text = "There was an error trying to load data from the database.<br/><br/>" & ex.ToString

        Finally
            Try
                db.Close()
            Catch ex As Exception
            End Try
        End Try
    End Sub

    Protected Sub btnEditScheduleOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEditScheduleOK.Click
        Dim db As New SqlConnection(GetConnectString())

        Try
            Dim sql As String = ""
            sql &= "SELECT      * "
            sql &= "FROM        dbo.ST_Schedules "
            sql &= "WHERE       [store] = " & _store & " "
            sql &= "        AND [productID] = " & hideEditProduct.Value & " "
            sql &= "        AND [yyyymmdd] = '" & ddlDays.SelectedValue & "' "

            db.Open()
            Dim cmd As New SqlCommand(sql, db)
            Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            If dr.Read() Then
                sql = ""
                sql &= "UPDATE      dbo.ST_Schedules "
                sql &= "SET         [Hour" & hideEditHour.Value & "] = " & ddlScheduledProducts.SelectedValue & " "
                sql &= "WHERE       [store] = " & _store & " "
                sql &= "        AND [productID] = " & hideEditProduct.Value & " "
                sql &= "        AND [yyyymmdd] = '" & ddlDays.SelectedValue & "' "
            Else
                sql = ""
                sql &= "INSERT      dbo.ST_Schedules ("
                sql &= "            [client], "
                sql &= "            [store], "
                sql &= "            [productID], "
                sql &= "            [yyyymmdd], "
                sql &= "            [hour00], "
                sql &= "            [hour01], "
                sql &= "            [hour02], "
                sql &= "            [hour03], "
                sql &= "            [hour04], "
                sql &= "            [hour05], "
                sql &= "            [hour06], "
                sql &= "            [hour07], "
                sql &= "            [hour08], "
                sql &= "            [hour09], "
                sql &= "            [hour10], "
                sql &= "            [hour11], "
                sql &= "            [hour12], "
                sql &= "            [hour13], "
                sql &= "            [hour14], "
                sql &= "            [hour15], "
                sql &= "            [hour16], "
                sql &= "            [hour17], "
                sql &= "            [hour18], "
                sql &= "            [hour19], "
                sql &= "            [hour20], "
                sql &= "            [hour21], "
                sql &= "            [hour22], "
                sql &= "            [hour23], "
                sql &= "            [percentage]) "
                sql &= "VALUES ( "
                sql &= "            1, "
                sql &= "            " & _store & ", "
                sql &= "            " & hideEditProduct.Value & ", "
                sql &= "            '" & ddlDays.SelectedValue & "', "
                For I As Integer = 0 To 23
                    If I = CInt(hideEditHour.Value) Then
                        sql &= "    " & ddlScheduledProducts.SelectedValue & ", "
                    Else
                        sql &= "    0, "
                    End If
                Next
                sql &= "    0) " ' percentage
            End If

            Try
                db.Close()
            Catch ex As Exception
            End Try

            db.Open()
            cmd = New SqlCommand(sql, db)
            cmd.ExecuteNonQuery()

            BindMasterData()

        Catch ex As Exception
            lblErrors.Text = "There was an error trying to load data from the database.<br/><br/>" & ex.ToString

        Finally
            Try
                db.Close()
            Catch ex As Exception
            End Try
        End Try
    End Sub

    Private Sub DoSuggestion(ByVal productID As Integer, ByVal percentage As Integer)
        Dim db As New SqlConnection(GetConnectString())
        Dim cmd As SqlCommand, dr As SqlDataReader
        Dim sql As String = ""

        Try
            sql &= "SELECT      [productType], "
            sql &= "            [upgradePLU] "
            sql &= "FROM        dbo.ST_Products "
            sql &= "WHERE       [store] = " & _store & " "
            sql &= "        AND [productID] = " & productID

            db.Open()
            cmd = New SqlCommand(sql, db)
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            Dim productType As Integer = 0, upgradePLU As Integer = 0
            If dr.Read Then
                productType = dr("productType")
                If Not IsDBNull(dr("upgradePLU")) Then upgradePLU = dr("upgradePLU")
            End If

            If productType = 0 Or (productType = 1 And upgradePLU = 0) Then
                Throw New Exception("Malformed Product")
            End If

            Try
                db.Close()
            Catch ex As Exception
            End Try

            sql = ""
            sql &= "SELECT      * "
            sql &= "FROM        dbo.ST_Schedules "
            sql &= "WHERE       [store] = " & _store & " "
            sql &= "        AND [productID] = " & productID & " "
            sql &= "        AND [yyyymmdd] = '" & ddlDays.SelectedValue & "' "

            db.Open()
            cmd = New SqlCommand(sql, db)
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            Dim inserting As Boolean = True

            If dr.Read Then
                inserting = False
            End If

            Try
                db.Close()
            Catch ex As Exception
            End Try

            Dim weekday As Integer = CDate(ddlDays.SelectedItem.Text).DayOfWeek + 1
            Dim multiplier As Decimal = CDec(percentage) / 100

            sql = ""
            sql &= "SELECT      [hour], "
            sql &= "            AVG([tickets]) "
            sql &= "FROM ( "
            sql &= "    SELECT      [yyyymmdd], "
            sql &= "                SUBSTRING([hhmm],1,2) as 'hour', "
            sql &= "                COUNT(*) as 'tickets'"
            sql &= "    FROM        dbo.TicketHeader "
            sql &= "    JOIN        dbo.TicketDetail "
            sql &= "            ON  [ticketHeaderID] = [ID] "
            sql &= "    WHERE       [Store] = " & _store & " "
            sql &= "            AND [yyyymmdd] > '" & Now.AddMonths(-3).ToString("yyyyMMdd") & "' "
            sql &= "            AND DATEPART(dw, CONVERT(DateTime, [yyyymmdd], 112)) = " & weekday & " "
            sql &= "            AND [Class] = 'SALE' "
            sql &= "            AND SubClass = 'ITEM' "
            sql &= "            AND [Type] = 'SKU' "
            If productType = 1 Then
                sql &= "        AND [SKU] = '" & upgradePLU & "' "
            End If
            sql &= "    GROUP BY    [yyyymmdd], "
            sql &= "                SUBSTRING([hhmm],1,2) "
            sql &= ") A "
            sql &= "GROUP BY    [hour] "
            sql &= "ORDER BY    [hour]"

            db.Open()
            cmd = New SqlCommand(sql, db)
            cmd.CommandTimeout = 90
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            sql = ""
            If inserting Then
                sql &= "INSERT      dbo.ST_Schedules ( "
                sql &= "    [client], "
                sql &= "    [store], "
                sql &= "    [productID], "
                sql &= "    [yyyymmdd], "
                sql &= "    [percentage], "
                sql &= "    [hour00], "
                sql &= "    [hour01], "
                sql &= "    [hour02], "
                sql &= "    [hour03], "
                sql &= "    [hour04], "
                sql &= "    [hour05], "
                sql &= "    [hour06], "
                sql &= "    [hour07], "
                sql &= "    [hour08], "
                sql &= "    [hour09], "
                sql &= "    [hour10], "
                sql &= "    [hour11], "
                sql &= "    [hour12], "
                sql &= "    [hour13], "
                sql &= "    [hour14], "
                sql &= "    [hour15], "
                sql &= "    [hour16], "
                sql &= "    [hour17], "
                sql &= "    [hour18], "
                sql &= "    [hour19], "
                sql &= "    [hour20], "
                sql &= "    [hour21], "
                sql &= "    [hour22], "
                sql &= "    [hour23]) "
                sql &= "VALUES ( "
                sql &= "    1, "
                sql &= "    " & _store & ", "
                sql &= "    " & productID & ", "
                sql &= "    '" & ddlDays.SelectedValue & "', "
                sql &= "    " & percentage & " "
            Else
                sql &= "UPDATE      dbo.ST_Schedules "
                sql &= "SET         [percentage] = " & percentage & ", "
            End If

            Dim last As Integer = -1, bFirst As Boolean = True
            While dr.Read()
                If inserting Then
                    For I As Integer = last + 1 To CInt(dr(0)) - 1
                        sql &= " , 0 "
                    Next
                    sql &= " , " & CDec(dr(1)) * multiplier & " "
                Else
                    For I As Integer = last + 1 To CInt(dr(0)) - 1
                        If Not bFirst Then sql &= " , "
                        sql &= " [hour" & I.ToString.PadLeft(2, "0") & "] = 0 "
                        bFirst = False
                    Next
                    If Not bFirst Then sql &= " , "
                    sql &= " [hour" & dr(0).ToString.PadLeft(2, "0") & "] = " & CDec(dr(1)) * multiplier & " "
                End If
                last = CInt(dr(0))
                bFirst = False
            End While

            If bFirst Then
                Throw New Exception("Not Enough Historical Data For Analysis")
            End If

            If inserting Then
                If last < 23 Then
                    For I As Integer = last + 1 To 23
                        sql &= " , 0 "
                    Next
                End If
                sql &= ")"
            Else
                If last < 23 Then
                    For I As Integer = last + 1 To 23
                        sql &= " , [hour" & I.ToString.PadLeft(2, "0") & "] = 0 "
                    Next
                End If
                sql &= "WHERE       [store] = " & _store & " "
                sql &= "        AND [productID] = " & productID & " "
                sql &= "        AND [yyyymmdd] = '" & ddlDays.SelectedValue & "' "
            End If

            Try
                db.Close()
            Catch ex As Exception
            End Try

            db.Open()
            cmd = New SqlCommand(sql, db)
            cmd.ExecuteNonQuery()

            BindMasterData()

        Catch ex As Exception
            lblErrors.Text = "There was an error trying to load data from the database.<br/><br/>" & ex.ToString

        Finally
            Try
                db.Close()
            Catch ex As Exception
            End Try
        End Try
    End Sub

    Protected Sub dgSchedules_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgSchedules.ItemCommand
        Dim db As New SqlConnection(GetConnectString())
        Dim cmd As SqlCommand, dr As SqlDataReader

        Try
            Dim productID As Integer = CInt(e.Item.Cells(0).Text)
            Dim percentage As Integer = 5

            Dim sql As String = ""
            sql &= "SELECT      [percentage] "
            sql &= "FROM        dbo.ST_Schedules "
            sql &= "WHERE       [store] = " & _store & " "
            sql &= "        AND [productID] = " & productID & " "
            sql &= "        AND [yyyymmdd] = '" & ddlDays.SelectedValue & "' "

            db.Open()
            cmd = New SqlCommand(sql, db)
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            If dr.Read Then
                If Not IsDBNull(dr("percentage")) Then percentage = CInt(dr("percentage"))
            End If

            Try
                db.Close()
            Catch ex As Exception
            End Try

            If e.CommandName.ToUpper = "SUGGEST" Then
                hidePercentageProduct.Value = productID
                ddlPercentage.SelectedValue = percentage

                upnPercentage.Update()
                mpePercentage.Show()

            ElseIf e.CommandName.ToUpper = "DIALUP" Then
                percentage += 10
                If percentage > 100 Then percentage = 100
                DoSuggestion(productID, percentage)

            ElseIf e.CommandName.ToUpper = "DIALDOWN" Then
                percentage -= 10
                If percentage < 0 Then percentage = 0
                DoSuggestion(productID, percentage)

            ElseIf e.CommandName.ToUpper = "CLEAR" Then
                sql &= "UPDATE      dbo.ST_Schedules "
                sql &= "SET         [hour00] = 0, "
                sql &= "            [hour01] = 0, "
                sql &= "            [hour02] = 0, "
                sql &= "            [hour03] = 0, "
                sql &= "            [hour04] = 0, "
                sql &= "            [hour05] = 0, "
                sql &= "            [hour06] = 0, "
                sql &= "            [hour07] = 0, "
                sql &= "            [hour08] = 0, "
                sql &= "            [hour09] = 0, "
                sql &= "            [hour10] = 0, "
                sql &= "            [hour11] = 0, "
                sql &= "            [hour12] = 0, "
                sql &= "            [hour13] = 0, "
                sql &= "            [hour14] = 0, "
                sql &= "            [hour15] = 0, "
                sql &= "            [hour16] = 0, "
                sql &= "            [hour17] = 0, "
                sql &= "            [hour18] = 0, "
                sql &= "            [hour19] = 0, "
                sql &= "            [hour20] = 0, "
                sql &= "            [hour21] = 0, "
                sql &= "            [hour22] = 0, "
                sql &= "            [hour23] = 0 "
                sql &= "WHERE       [store] = " & _store & " "
                sql &= "        AND [productID] = " & productID & " "
                sql &= "        AND [yyyymmdd] = '" & ddlDays.SelectedValue & "' "

                db.Open()
                cmd = New SqlCommand(sql, db)
                cmd.ExecuteNonQuery()

                BindMasterData()
            End If

        Catch ex As Exception
            lblErrors.Text = "There was an error trying to load data from the database.<br/><br/>" & ex.ToString

        Finally
            Try
                db.Close()
            Catch ex As Exception
            End Try
        End Try
    End Sub

    Protected Sub dgSchedules_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgSchedules.ItemCreated
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim btnClear As Button = CType(e.Item.Cells(30).Controls(0), Button)
            btnClear.Attributes.Add("onclick", "return confirm('Are you sure you want to clear all schedules for this day for this product?');")
        End If
    End Sub

    Protected Sub ddlDays_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDays.SelectedIndexChanged
        BindMasterData()
    End Sub

    Protected Sub btnAddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        ddlNewProduct.SelectedIndex = 0
        upnNewProduct.Update()
        mpeNewProduct.Show()
    End Sub

    Protected Sub btnNewProductOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNewProductOK.Click
        Dim db As New SqlConnection(GetConnectString())

        Try
            Dim sql As String = ""
            sql &= "INSERT      dbo.ST_Schedules ( "
            sql &= "    [client], "
            sql &= "    [store], "
            sql &= "    [productID], "
            sql &= "    [yyyymmdd], "
            sql &= "    [hour00], "
            sql &= "    [hour01], "
            sql &= "    [hour02], "
            sql &= "    [hour03], "
            sql &= "    [hour04], "
            sql &= "    [hour05], "
            sql &= "    [hour06], "
            sql &= "    [hour07], "
            sql &= "    [hour08], "
            sql &= "    [hour09], "
            sql &= "    [hour10], "
            sql &= "    [hour11], "
            sql &= "    [hour12], "
            sql &= "    [hour13], "
            sql &= "    [hour14], "
            sql &= "    [hour15], "
            sql &= "    [hour16], "
            sql &= "    [hour17], "
            sql &= "    [hour18], "
            sql &= "    [hour19], "
            sql &= "    [hour20], "
            sql &= "    [hour21], "
            sql &= "    [hour22], "
            sql &= "    [hour23], "
            sql &= "    [percentage]) "
            sql &= "VALUES ( "
            sql &= "    1, "
            sql &= "    " & _store & ", "
            sql &= "    " & ddlNewProduct.SelectedValue & ", "
            sql &= "    '" & ddlDays.SelectedValue & "', "
            sql &= "    0, "
            sql &= "    0, "
            sql &= "    0, "
            sql &= "    0, "
            sql &= "    0, "
            sql &= "    0, "
            sql &= "    0, "
            sql &= "    0, "
            sql &= "    0, "
            sql &= "    0, "
            sql &= "    0, "
            sql &= "    0, "
            sql &= "    0, "
            sql &= "    0, "
            sql &= "    0, "
            sql &= "    0, "
            sql &= "    0, "
            sql &= "    0, "
            sql &= "    0, "
            sql &= "    0, "
            sql &= "    0, "
            sql &= "    0, "
            sql &= "    0, "
            sql &= "    0, "
            sql &= "    0) "

            db.Open()
            Dim cmd As New SqlCommand(sql, db)
            cmd.ExecuteNonQuery()

            BindMasterData()

        Catch ex As Exception
            lblErrors.Text = "There was an error trying to load data from the database.<br/><br/>" & ex.ToString

        Finally
            Try
                db.Close()
            Catch ex As Exception
            End Try
        End Try
    End Sub

    Protected Sub btnPercentageOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPercentageOK.Click
        DoSuggestion(hidePercentageProduct.Value, ddlPercentage.SelectedValue)
    End Sub
End Class
