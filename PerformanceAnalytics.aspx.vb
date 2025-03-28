Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration
Imports System.Configuration
Imports DataAccess

Partial Class PerformanceAnalytics
    Inherits System.Web.UI.Page

    Private Property _store() As String
        Get
            Return Convert.ToString(ViewState("Store"))
        End Get
        Set(ByVal Value As String)
            ViewState("Store") = Value
        End Set
    End Property

    Private Property _register() As String
        Get
            Return Convert.ToString(ViewState("Register"))
        End Get
        Set(ByVal Value As String)
            ViewState("Register") = Value
        End Set
    End Property

    Private Function GetConnectString() As String
        'Return "Data Source=.\SQLEXPRESS;Initial Catalog=WebsiteTesting;UID=IKAdmin;PWD=3kMGb4sds;Max Pool Size=500;"
        Dim data1 As New DataAccess()
        Return data1.UnMaskString(System.Web.Configuration.WebConfigurationManager.ConnectionStrings("Production").ConnectionString, 1, 2, 3, 4, 5)
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack() Then
            'Get the store number from one of the prompt.asp files
            'TODO: There needs to be a check to determine if user has access to this store
            _store = Request.Form("store")
            If Len(_store) = 0 Then
                _store = "8888"
            End If
            _register = "1"

            If _store = "" Then
                lblStoreDisplay.Text = "No store was selected."
                hideStore.Value = ""
            Else
                lblStoreDisplay.Text = "Store Number: " & _store
                hideStore.Value = _store
            End If
            BindMasterData()
        Else

            _register = Request.Form("ddlRegister")
        End If
        LoadCurrentData()
    End Sub

    Private Sub BindMasterData()
        Dim db As New SqlConnection(GetConnectString())
        db.Open()

        Try
            Dim sql As String = ""
            sql &= "SELECT      '0' as 'PLU', "
            sql &= "            '[select]' as 'Desc' "
            sql &= "UNION "
            sql &= "SELECT      PLU, "
            sql &= "            CONVERT(varchar(20),PLU) + '-' + [Desc] as 'Desc' "
            sql &= "FROM        PLUs "
            sql &= "WHERE       client = 1 "
            sql &= "        AND store = " & _store & " "
            sql &= "ORDER BY    PLU "

            Dim cmd As New SqlCommand(sql, db)
            Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            ddlPLUs.DataSource = dr
            ddlPLUs.DataValueField = "PLU"
            ddlPLUs.DataTextField = "Desc"
            ddlPLUs.DataBind()

            sql = ""
            sql &= "SELECT      0 as 'Dept', "
            sql &= "            '[select]' as 'Desc' "
            sql &= "UNION "
            sql &= "SELECT      Dept, "
            sql &= "            CONVERT(varchar(20),Dept) + '-' + [Desc] as 'Desc' "
            sql &= "FROM        Departments "
            sql &= "WHERE       client = 1 "
            sql &= "        AND store = " & _store & " "
            sql &= "ORDER BY    Dept "

            db.Open()
            cmd = New SqlCommand(sql, db)
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            ddlDepts.DataSource = dr
            ddlDepts.DataValueField = "Dept"
            ddlDepts.DataTextField = "Desc"
            ddlDepts.DataBind()

            sql = ""
            sql &= "SELECT      CASE SUM(b.amount) WHEN 0 THEN 0 ELSE SUM(a.amount)/SUM(b.amount) END "
            sql &= "FROM        CalculationDetail a "
            sql &= "JOIN        CalculationDetail b "
            sql &= "        ON  a.store = b.store "
            sql &= "WHERE       a.ID = 'NET1' "
            sql &= "        AND b.ID = 'TRANS COUNT' "
            sql &= "        AND a.store = " & _store & " "
            sql &= "        AND a.BizDate >= CONVERT(varchar(8),DATEADD(d,-30,GETDATE()),112) "
            sql &= "        AND b.BizDate >= CONVERT(varchar(8),DATEADD(d,-30,GETDATE()),112) "

            db.Open()
            cmd = New SqlCommand(sql, db)
            Dim avg As Decimal = 0.0
            Try
                avg = CDec(cmd.ExecuteScalar())
            Catch ex As Exception
            End Try

            If avg = 0.0 Then
                lblAvgTicket.Text = "No Suggested Value"
            Else
                lblAvgTicket.Text = "Suggested Value: " & (avg * 1.1).ToString("C")
            End If

        Catch ex As SqlException
            lblErrors.Text = "There was an error trying to load data from the database.<br/><br/>" & ex.ToString

        Finally
            db.Close()
        End Try
    End Sub

    Private Sub LoadCurrentData()
        Dim db As New SqlConnection(GetConnectString())
        db.Open()

        Try
            Dim sql As String = ""
            sql &= "SELECT      value "
            sql &= "FROM        InfoPOSOptions "
            sql &= "WHERE       store = " & _store & " "
            sql &= "        AND register = " & _register & " "
            sql &= "        AND type = 'ProfitTrax' "
            sql &= "        AND opt = 'Enabled'"

            Dim cmd As New SqlCommand(sql, db)
            Dim enabled As String = CStr(cmd.ExecuteScalar())
            If enabled Is Nothing Then enabled = "False"

            chkEnable.Checked = CBool(enabled)

            sql = ""
            sql &= "SELECT      value "
            sql &= "FROM        InfoPOSOptions "
            sql &= "WHERE       store = " & _store & " "
            sql &= "        AND register = " & _register & " "
            sql &= "        AND type = 'ProfitTrax' "
            sql &= "        AND opt = 'TrackByDepartment'"

            cmd = New SqlCommand(sql, db)
            Dim trackByDepartment As String = CStr(cmd.ExecuteScalar())
            If trackByDepartment Is Nothing Then trackByDepartment = "False"

            If CBool(trackByDepartment) Then
                chkTrackDepts.Checked = True
                ddlPLUs.CssClass = "invisible"
                ddlDepts.CssClass = "visible"
            Else
                chkTrackDepts.Checked = False
                ddlPLUs.CssClass = "visible"
                ddlDepts.CssClass = "invisible"
            End If

            sql = ""
            sql &= "SELECT      value "
            sql &= "FROM        InfoPOSOptions "
            sql &= "WHERE       store = " & _store & " "
            sql &= "        AND register = " & _register & " "
            sql &= "        AND type = 'ProfitTrax' "
            sql &= "        AND opt = 'ItemToTrack'"

            cmd = New SqlCommand(sql, db)
            Dim itemToTrack As String = CStr(cmd.ExecuteScalar())
            If itemToTrack Is Nothing Then itemToTrack = "0"

            If CBool(trackByDepartment) Then
                Try
                    ddlDepts.SelectedValue = itemToTrack
                Catch ex As Exception
                    ddlDepts.SelectedIndex = 0
                End Try
            Else
                Try
                    ddlPLUs.SelectedValue = itemToTrack
                Catch ex As Exception
                    ddlPLUs.SelectedIndex = 0
                End Try
            End If

            sql = ""
            sql &= "SELECT      value "
            sql &= "FROM        InfoPOSOptions "
            sql &= "WHERE       store = " & _store & " "
            sql &= "        AND register = " & _register & " "
            sql &= "        AND type = 'ProfitTrax' "
            sql &= "        AND opt = 'ItemTargetPercentage'"

            cmd = New SqlCommand(sql, db)
            Dim itemTargetPercentage As String = CStr(cmd.ExecuteScalar())
            If itemTargetPercentage Is Nothing Then itemTargetPercentage = "0"

            txtSellThruSlider.Text = itemTargetPercentage

            sql = ""
            sql &= "SELECT      value "
            sql &= "FROM        InfoPOSOptions "
            sql &= "WHERE       store = " & _store & " "
            sql &= "        AND register = " & _register & " "
            sql &= "        AND type = 'ProfitTrax' "
            sql &= "        AND opt = 'AverageTicketTarget'"

            cmd = New SqlCommand(sql, db)
            Dim avgTicketTarget As String = CStr(cmd.ExecuteScalar())
            If avgTicketTarget Is Nothing Then avgTicketTarget = "0.00"

            txtAvgTicket.Text = avgTicketTarget

        Catch ex As SqlException
            lblErrors.Text = "There was an error trying to load data from the database.<br/><br/>" & ex.ToString

        Finally
            db.Close()
        End Try
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim db As New SqlConnection(GetConnectString())
        db.Open()

        Dim enabled As String = "True"
        If Request.Form("chkEnable") = "" Then
            enabled = "False"
        End If

        Dim sql As String = ""
        sql &= "DELETE FROM     InfoPOSOptions "
        sql &= "WHERE           store = " & _store & " "
        sql &= "            AND register = " & _register & " "
        sql &= "            AND type = 'ProfitTrax' "
        sql &= "            AND opt = 'Enabled' "

        Dim cmd As New SqlCommand(sql, db)
        cmd.ExecuteNonQuery()

        sql = ""
        sql &= "INSERT          InfoPOSOptions ("
        sql &= "    client, "
        sql &= "    store, "
        sql &= "    register, "
        sql &= "    type, "
        sql &= "    opt, "
        sql &= "    value) "
        sql &= "VALUES ("
        sql &= "    1, "
        sql &= "    " & _store & ", "
        sql &= "    " & _register & ", "
        sql &= "    'ProfitTrax', "
        sql &= "    'Enabled', "
        sql &= "    '" & enabled & "')"

        cmd = New SqlCommand(sql, db)
        cmd.ExecuteNonQuery()

        Dim trackByDepartment As String = "True"
        If Request.Form("chkTrackDepts") = "" Then
            trackByDepartment = "False"
        End If

        sql = ""
        sql &= "DELETE FROM     InfoPOSOptions "
        sql &= "WHERE           store = " & _store & " "
        sql &= "            AND register = " & _register & " "
        sql &= "            AND type = 'ProfitTrax' "
        sql &= "            AND opt = 'TrackByDepartment' "

        cmd = New SqlCommand(sql, db)
        cmd.ExecuteNonQuery()

        sql = ""
        sql &= "INSERT          InfoPOSOptions ("
        sql &= "    client, "
        sql &= "    store, "
        sql &= "    register, "
        sql &= "    type, "
        sql &= "    opt, "
        sql &= "    value) "
        sql &= "VALUES ("
        sql &= "    1, "
        sql &= "    " & _store & ", "
        sql &= "    " & _register & ", "
        sql &= "    'ProfitTrax', "
        sql &= "    'TrackByDepartment', "
        sql &= "    '" & trackByDepartment & "')"

        cmd = New SqlCommand(sql, db)
        cmd.ExecuteNonQuery()

        Dim itemToTrack As String = ""
        If CBool(trackByDepartment) Then
            itemToTrack = Request.Form("ddlDepts")
        Else
            itemToTrack = Request.Form("ddlPLUs")
        End If

        sql = ""
        sql &= "DELETE FROM     InfoPOSOptions "
        sql &= "WHERE           store = " & _store & " "
        sql &= "            AND register = " & _register & " "
        sql &= "            AND type = 'ProfitTrax' "
        sql &= "            AND opt = 'ItemToTrack' "

        cmd = New SqlCommand(sql, db)
        cmd.ExecuteNonQuery()

        sql = ""
        sql &= "INSERT          InfoPOSOptions ("
        sql &= "    client, "
        sql &= "    store, "
        sql &= "    register, "
        sql &= "    type, "
        sql &= "    opt, "
        sql &= "    value) "
        sql &= "VALUES ("
        sql &= "    1, "
        sql &= "    " & _store & ", "
        sql &= "    " & _register & ", "
        sql &= "    'ProfitTrax', "
        sql &= "    'ItemToTrack', "
        sql &= "    '" & itemToTrack & "')"

        cmd = New SqlCommand(sql, db)
        cmd.ExecuteNonQuery()

        Dim sellThruTarget As String = Request.Form("txtSellThru")

        sql = ""
        sql &= "DELETE FROM     InfoPOSOptions "
        sql &= "WHERE           store = " & _store & " "
        sql &= "            AND register = " & _register & " "
        sql &= "            AND type = 'ProfitTrax' "
        sql &= "            AND opt = 'ItemTargetPercentage' "

        cmd = New SqlCommand(sql, db)
        cmd.ExecuteNonQuery()

        sql = ""
        sql &= "INSERT          InfoPOSOptions ("
        sql &= "    client, "
        sql &= "    store, "
        sql &= "    register, "
        sql &= "    type, "
        sql &= "    opt, "
        sql &= "    value) "
        sql &= "VALUES ("
        sql &= "    1, "
        sql &= "    " & _store & ", "
        sql &= "    " & _register & ", "
        sql &= "    'ProfitTrax', "
        sql &= "    'ItemTargetPercentage', "
        sql &= "    '" & sellThruTarget & "')"

        cmd = New SqlCommand(sql, db)
        cmd.ExecuteNonQuery()

        Dim avgTicketTarget As String = Request.Form("txtAvgTicket")

        sql = ""
        sql &= "DELETE FROM     InfoPOSOptions "
        sql &= "WHERE           store = " & _store & " "
        sql &= "            AND register = " & _register & " "
        sql &= "            AND type = 'ProfitTrax' "
        sql &= "            AND opt = 'AverageTicketTarget' "

        cmd = New SqlCommand(sql, db)
        cmd.ExecuteNonQuery()

        sql = ""
        sql &= "INSERT          InfoPOSOptions ("
        sql &= "    client, "
        sql &= "    store, "
        sql &= "    register, "
        sql &= "    type, "
        sql &= "    opt, "
        sql &= "    value) "
        sql &= "VALUES ("
        sql &= "    1, "
        sql &= "    " & _store & ", "
        sql &= "    " & _register & ", "
        sql &= "    'ProfitTrax', "
        sql &= "    'AverageTicketTarget', "
        sql &= "    '" & avgTicketTarget & "')"

        cmd = New SqlCommand(sql, db)
        cmd.ExecuteNonQuery()

        LoadCurrentData()
        lblMessage.Text = "Settings Saved Successfully"
    End Sub
End Class
