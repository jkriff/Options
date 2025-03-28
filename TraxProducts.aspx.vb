Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration
Imports System.Configuration
Imports DataAccess

Partial Class TraxProducts
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

            _sortField = "ProductID"           'Default sort field
            If _store = "" Then
                lblStoreDisplay.Text = "No store was selected."
                hideStore.Value = ""
            Else
                lblStoreDisplay.Text = "Store Number: " & _store
                hideStore.Value = _store
                BindPLUData()
                BindMasterData()
            End If
        End If
    End Sub

    Private Sub BindPLUData()
        Dim db As New SqlConnection(GetConnectString())

        Try
            Dim sql As String = "SELECT     CONVERT(int,PLU) as 'PLU', " & _
                                "           LTRIM(RTRIM(PLU)) + ' - ' + LTRIM(RTRIM([Desc])) as 'Desc' " & _
                                "FROM       PLUs " & _
                                "WHERE      Client = 1 " & _
                                "       AND Store = " & _store & " " & _
                                "       AND deactivatedOn IS NULL " & _
                                "ORDER BY   CONVERT(int,PLU)"

            db.Open()
            Dim cmd As New SqlCommand(sql, db)
            Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            'Dim dt As DataTable = New DataTable()
            'dt.Load(dr)

            ddlPLU.DataSource = dr
            ddlPLU.DataValueField = "PLU"
            ddlPLU.DataTextField = "Desc"
            ddlPLU.DataBind()

            sql = "SELECT     CONVERT(int,PLU) as 'PLU', " & _
                  "           LTRIM(RTRIM(PLU)) + ' - ' + LTRIM(RTRIM([Desc])) as 'Desc' " & _
                  "FROM       PLUs " & _
                  "WHERE      Client = 1 " & _
                  "       AND Store = " & _store & " " & _
                  "       AND deactivatedOn IS NULL " & _
                  "UNION " & _
                  "SELECT     0, '0 - None' " & _
                  "ORDER BY   CONVERT(int,PLU)"
            db.Open()
            cmd = New SqlCommand(sql, db)
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            ddlUpgradePLU.DataSource = dr
            ddlUpgradePLU.DataValueField = "PLU"
            ddlUpgradePLU.DataTextField = "Desc"
            ddlUpgradePLU.DataBind()

        Catch ex As Exception
            lblErrors.Text = "There was an error trying to load data from the database.<br/><br/>" & ex.ToString

        Finally
            db.Close()
        End Try
    End Sub

    Private Sub BindMasterData()
        Dim db As New SqlConnection(GetConnectString())
        db.Open()

        Try
            Dim sql As String = "SELECT     tp.productID as 'ProductID', " & _
                                "           tp.PLU as 'PLU', " & _
                                "           pa.PLU + ' - ' + pa.[Desc] as 'PLUDesc', " & _
                                "           'Upgrade' as 'Type', " & _
                                "           tp.upgradePLU as 'UpgradePLU', " & _
                                "           pb.PLU + ' - ' + pb.[Desc] as 'UpgradePLUDesc', " & _
                                "           tp.preparationTime as 'PrepTime', " & _
                                "           tp.cookingTime as 'CookTime', " & _
                                "           tp.decayTime as 'DecayTime' " & _
                                "FROM       TraxProducts tp " & _
                                "JOIN       PLUs pa " & _
                                "       ON  pa.client = tp.client " & _
                                "       AND pa.store = tp.store " & _
                                "       AND pa.PLU = tp.PLU " & _
                                "LEFT JOIN  PLUs pb " & _
                                "       ON  pb.client = tp.client " & _
                                "       AND pb.store = tp.store " & _
                                "       AND pb.PLU = tp.UpgradePLU " & _
                                "WHERE      tp.client = 1 " & _
                                "       AND tp.store = " & _store & " " & _
                                "       AND tp.dtDeleted IS NULL " & _
                                "ORDER BY " & _sortField

            Dim cmd As New SqlCommand(sql, db)
            Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            Dim dt As DataTable = New DataTable()

            dt.Load(dr)
            dgProducts.DataSource = dt
            dgProducts.DataBind()

        Catch ex As SqlException
            lblErrors.Text = "There was an error trying to load data from the database.<br/><br/>" & ex.ToString

        Finally
            db.Close()
        End Try
    End Sub

    Sub dgProducts_NewPage(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        dgProducts.CurrentPageIndex = e.NewPageIndex
        BindMasterData()
    End Sub

    Sub dgProducts_Sort(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        _sortField = e.SortExpression
        BindMasterData()
    End Sub

    Private Sub dgProducts_ItemDataBound(ByVal sender As Object, ByVal e As DataGridItemEventArgs) Handles dgProducts.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onclick", "loadItems(this, '" & e.Item.DataItem("ProductID") & "','" & e.Item.DataItem("PLU") & "','" & e.Item.DataItem("UpgradePLU") & "','" & e.Item.DataItem("PrepTime") & "','" & e.Item.DataItem("DecayTime") & "');")
            e.Item.Attributes.Add("onmouseover", "mouseover(this)")
            e.Item.Attributes.Add("onmouseout", "mouseout(this)")
        End If
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Dim store As String = Request.Form("hideStore")
        Dim plu As String = Request.Form("ddlPLU")
        Dim upgradePLU As String = Request.Form("ddlUpgradePLU")
        Dim prepTime As String = Request.Form("ddlPrepTime")
        Dim decayTime As String = Request.Form("ddlDecayTime")

        Dim db As New SqlConnection(GetConnectString())
        db.Open()
        Try
            If prepTime.Trim.Length > 0 Then
                If Not IsNumeric(prepTime) Then
                    lblErrors.Text = "Preparation Time Must Be Numeric"
                    Return
                End If
            Else
                lblErrors.Text = "Please Enter a Value for Preparation Time"
                Return
            End If

            If decayTime.Trim.Length > 0 Then
                If Not IsNumeric(decayTime) Then
                    lblErrors.Text = "Decay Time must be numeric"
                    Return
                End If
            Else
                lblErrors.Text = "Please Enter a Value for Decay Time"
                Return
            End If

            Dim sql As String = "SELECT     MAX(productID) " & _
                                "FROM       TraxProducts " & _
                                "WHERE      client = 1 " & _
                                "       AND store = " & _store

            Dim cmd As New SqlCommand(sql, db)
            Dim retVal As String = ""
            Try
                retVal = cmd.ExecuteScalar.ToString()
            Catch
            End Try

            Dim newProductID As Integer = 1
            If retVal.Length > 0 Then
                newProductID = CInt(retVal) + 1
            End If

            sql = "INSERT TraxProducts (client, store, productID, PLU, upgradePLU, preparationTime, decayTime, dtCreated, dtUpdated) " & _
                  "VALUES (1, " & _store & ", " & newProductID & ", " & plu & ", " & upgradePLU & ", " & prepTime & ", " & decayTime & ", GETDATE(), GETDATE())"
            cmd = New SqlCommand(sql, db)
            cmd.ExecuteNonQuery()

        Catch ex As SqlException
            lblErrors.Text = "Error Adding New Product<br/><br/>" & ex.ToString

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
        Dim productID As String = Request.Form("hideProductID")
        Dim plu As String = Request.Form("ddlPLU")
        Dim upgradePLU As String = Request.Form("ddlUpgradePLU")
        Dim prepTime As String = Request.Form("ddlPrepTime")
        Dim decayTime As String = Request.Form("ddlDecayTime")

        Dim db As New SqlConnection(GetConnectString())
        db.Open()
        Try
            If prepTime.Trim.Length > 0 Then
                If Not IsNumeric(prepTime) Then
                    lblErrors.Text = "Preparation Time Must Be Numeric"
                    Return
                End If
            Else
                lblErrors.Text = "Please Enter a Value for Preparation Time"
                Return
            End If

            If decayTime.Trim.Length > 0 Then
                If Not IsNumeric(decayTime) Then
                    lblErrors.Text = "Decay Time must be numeric"
                    Return
                End If
            Else
                lblErrors.Text = "Please Enter a Value for Decay Time"
                Return
            End If

            Dim sql As String = "UPDATE     TraxProducts " & _
                                "SET        PLU = " & plu & ", " & _
                                "           upgradePLU = " & upgradePLU & ", " & _
                                "           preparationTime = " & prepTime & ", " & _
                                "           decayTime = " & decayTime & ", " & _
                                "           dtUpdated = GETDATE() " & _
                                "WHERE      client = 1 " & _
                                "       AND store = " & _store & " " & _
                                "       AND productID = " & productID

            Dim cmd As New SqlCommand(Sql, db)
            cmd.ExecuteNonQuery()

        Catch ex As SqlException
            lblErrors.Text = "Error Updating Product<br/><br/>" & ex.ToString

        Finally
            db.Close()
        End Try

        BindMasterData()
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim productID As String = Request.Form("hideProductID")

        Dim db As New SqlConnection(GetConnectString())
        db.Open()
        Try
            Dim sql As String = "UPDATE     TraxProducts " & _
                                "SET        dtDeleted = GETDATE(), " & _
                                "           dtUpdated = GETDATE() " & _
                                "WHERE      client = 1 " & _
                                "       AND store = " & _store & " " & _
                                "       AND productID = " & productID

            Dim cmd As New SqlCommand(sql, db)
            cmd.ExecuteNonQuery()

        Catch ex As Exception
            lblErrors.Text = "Error Deleting Product<br/><br/>" & ex.ToString

        Finally
            db.Close()
        End Try

        BindMasterData()
    End Sub
End Class
