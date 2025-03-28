Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration
Imports System.Configuration
Imports DataAccess




Partial Class KitchenPrinter
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

    Private Property _pluSearch() As String
        Get
            Return Convert.ToString(ViewState("PluSearch"))
        End Get
        Set(ByVal Value As String)
            ViewState("PluSearch") = Value
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
        labelSaved.Text = ""
        labelErrors.Text = ""
        If Not Page.IsPostBack() Then
            'Get the store number from one of the prompt.asp files
            'TODO: There needs to be a check to determine if user has access to this store
            _store = Request.Form("store")
            If Len(_store) = 0 Then
                _store = "999999"
            End If

            _sortField = "PLU"           'Default sort field
            _pluSearch = ""
            If _store = "" Then
                labelStoreDisplay.Text = "No store was selected."
            Else
                labelStoreDisplay.Text = "Store Number: " & _store
                BindMasterData()
            End If
        End If
    End Sub

    Private Sub BindMenuData()
        Try
            Dim sql As String = "select "

        Catch ex As Exception

        End Try
    End Sub

    Private Sub BindMasterData()
        Dim db As New SqlConnection(GetConnectString())
        db.Open()

        Try
            Dim pluSearch As String
            If _pluSearch <> "" Then
                pluSearch = " and (p.plu like '%" & _pluSearch.Replace("'", "''") & "%' or p.[Desc] like '%" & _pluSearch.Replace("'", "''") & "%')"
            End If

            Dim sql As String = "select p.Store as 'Store', p.PLU as 'PLU', p.[Desc] as 'Desc', k.KitchenPrinter as 'KitchenPrinter', kp.Priority as 'Priority' " & _
             " from PLUs p" & _
             " left join KitchenPrinter k on p.Client = k.Client and p.Store = k.Store and p.PLU = k.PLU" & _
             " left join KPPriorities kp on kp.Client = k.Client and kp.Store = k.Store and kp.PLU = k.PLU " & _
             " where p.Store = '" & _store & "'" & pluSearch & _
             " group by p.Client, p.Store, p.PLU, p.[Desc], k.KitchenPrinter, kp.priority  " & _
             " order by " & _sortField


            Dim cmd As New SqlCommand(sql, db)
            Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            Dim dt As DataTable = New DataTable()

            dt.Load(dr)
            datagridPLUs.DataSource = dt
            datagridPLUs.DataBind()
        Catch ex As SqlException
            labelErrors.Text = "There was an error trying to load data from the database.<br/><br/>" & ex.ToString
        Finally
            db.Close()
        End Try
    End Sub

    Sub datagridPLUs_NewPage(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        datagridPLUs.CurrentPageIndex = e.NewPageIndex
        BindMasterData()
    End Sub

    Sub datagridPLUs_Sort(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        _sortField = e.SortExpression
        BindMasterData()
    End Sub

    Private Sub datagridPLUs_ItemDataBound(ByVal sender As Object, ByVal e As DataGridItemEventArgs) Handles datagridPLUs.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onclick", "loadItems(this, '" & e.Item.DataItem("Store") & "','" & e.Item.DataItem("PLU") & "','" & e.Item.DataItem("KitchenPrinter") & "','" & e.Item.DataItem("Desc") & "','" & e.Item.DataItem("Priority") & "');")
            e.Item.Attributes.Add("onmouseover", "mouseover(this)")
            e.Item.Attributes.Add("onmouseout", "mouseout(this)")
        End If
    End Sub

    Private Sub buttonSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonSave.Click
        Dim client As String = "1"        'Request.Form("hideClient")
        Dim plu As String = Request.Form("hidePLU")
        Dim store As String = Request.Form("hideStore")
        Dim kp As String = Request.Form("textboxKP")
        Dim priority As String = Request.Form("textboxPriority")
        Dim register As String = Request.Form("tbRegister")

        Dim db As New SqlConnection(GetConnectString())
        db.Open()
        Try
            If Len(register) Then
                register = "0"
            End If
            'Validate KP Number
            'Could have done > 0 and < 100 but where's the fun in that?
            Dim reg As System.Text.RegularExpressions.Regex

            If Not reg.IsMatch(kp, "^[1-9]{1}$|^[1-9]{1}[0-9]{1}$") Then
                labelErrors.Text = "KP Number must be 1-99 or me"
                Return
            End If

            If Not reg.IsMatch(priority, "^-1$|^[0-9]{1}$") Then
                labelErrors.Text = "Printing priority must be between -1 and 9. -1 disables printing."
                Return
            End If

            If kp = 0 Then
                Dim sqlDeletePrinter As String
                sqlDeletePrinter = "Delete From KitchenPrinter where Client = " & client & " and Store = " & store & " and plu = " & plu
                Dim cmdDel As New SqlCommand(sqlDeletePrinter, db)
                cmdDel.ExecuteNonQuery()
            End If

            If priority = -1 Then
                Dim sqlDeletePriority As String
                sqlDeletePriority = "Delete From KPPriorities where Client = " & client & " and Store = " & store & " and plu = " & plu
                Dim cmdDel2 As New SqlCommand(sqlDeletePriority, db)
                cmdDel2.ExecuteNonQuery()

            End If

            If (client <> String.Empty And plu <> String.Empty And store <> String.Empty) And kp <> String.Empty Then
                'Check to see if the record already exists
                Dim sqlCheck As String = "select isnull(KitchenPrinter,200) from KitchenPrinter" & _
                 " where Client = " & client & " and Store = " & store & " and PLU = '" & plu & "'"
                Dim cmd As New SqlCommand(sqlCheck, db)
                Dim retVal As String
                Try
                    retVal = cmd.ExecuteScalar.ToString()
                Catch
                End Try

                Dim sqlUpdate As String
                If retVal = String.Empty Then
                    'The record doesn't exist - create it.
                    sqlUpdate = "insert into KitchenPrinter (Client, Store, PLU, Register, KitchenPrinter) values (" & client & "," & store & ",'" & plu & "','" & register & "','" & kp & "')"
                ElseIf retVal <> kp Then
                    'The record exists and is different from current record then update it.
                    sqlUpdate = "update KitchenPrinter set KitchenPrinter = '" & kp & "' where Client = " & client & " and Store = " & store & " and PLU = '" & plu & "'"
                End If
                If Len(sqlUpdate) > 0 Then
                    cmd = New SqlCommand(sqlUpdate, db)
                    cmd.ExecuteNonQuery()
                End If

                sqlCheck = "select isnull(Priority,20) from KPPriorities" & _
                    " where Client = " & client & " and Store = " & store & " and PLU = '" & plu & "'"
                cmd = New SqlCommand(sqlCheck, db)

                retVal = Nothing
                Try
                    retVal = cmd.ExecuteScalar.ToString()
                Catch ex As Exception                   
                End Try

                If retVal = String.Empty Then
                    'The record doesn't exist - create it.
                    sqlUpdate = "insert into KPPriorities  (Client, Store, PLU, Priority ) values (" & client & "," & store & ",'" & plu & "','" & priority & "')"
                ElseIf retVal <> priority Then
                    'The record exists and is different from current record then update it.
                    sqlUpdate = "update KPPriorities  set Priority  = '" & priority & "' where Client = " & client & " and Store = " & store & " and PLU = '" & plu & "'"
                End If
                If Len(sqlUpdate) > 0 Then
                    cmd = New SqlCommand(sqlUpdate, db)
                    cmd.ExecuteNonQuery()
                End If

                labelSaved.Text = "  Saved Data"
            Else
                labelErrors.Text = "Please select a record to update"
            End If

        Catch ex As SqlException
            labelErrors.Text = "There was an error trying to save the Kitchen Printer data.<br/><br/>" & ex.ToString

        Finally
            db.Close()
        End Try

        BindMasterData()
    End Sub

    Private Sub buttonPluSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonPluSearch.Click
        _pluSearch = textboxSearchPLU.Text.Trim
        BindMasterData()
    End Sub

    Private Function GetConnectString() As String
        Dim data1 As New DataAccess()
        Return data1.UnMaskString(System.Web.Configuration.WebConfigurationManager.ConnectionStrings("Production").ConnectionString, 1, 2, 3, 4, 5)
    End Function



End Class
