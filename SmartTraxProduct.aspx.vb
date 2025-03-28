Imports System.Data.SqlClient

Partial Class SmartTraxProduct
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
            Dim sql As String = "SELECT     CONVERT(int,PLU) as 'PLU', " & _
                                "           LTRIM(RTRIM(PLU)) + ' - ' + LTRIM(RTRIM([Desc])) as 'Desc' " & _
                                "FROM       dbo.PLUs " & _
                                "WHERE      Client = 1 " & _
                                "       AND Store = " & _store & " " & _
                                "       AND deactivatedOn IS NULL " & _
                                "UNION " & _
                                "SELECT     0, " & _
                                "           '[select]' " & _
                                "ORDER BY   CONVERT(int,PLU)"

            db.Open()
            Dim cmd As New SqlCommand(sql, db)
            Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            ddlProduct.DataSource = dr
            ddlProduct.DataValueField = "PLU"
            ddlProduct.DataTextField = "Desc"
            ddlProduct.DataBind()

            db.Open()
            cmd = New SqlCommand(sql, db)
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            ddlUpgradeProduct.DataSource = dr
            ddlUpgradeProduct.DataValueField = "PLU"
            ddlUpgradeProduct.DataTextField = "Desc"
            ddlUpgradeProduct.DataBind()

            sql = "SELECT     [ProductTypeID], " & _
                  "           [Description] " & _
                  "FROM       dbo.ST_ProductTypes " & _
                  "UNION " & _
                  "SELECT     0, " & _
                  "           '[select]' " & _
                  "ORDER BY   [ProductTypeID]"

            db.Open()
            cmd = New SqlCommand(sql, db)
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            ddlProductType.DataSource = dr
            ddlProductType.DataValueField = "ProductTypeID"
            ddlProductType.DataTextField = "Description"
            ddlProductType.DataBind()

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
                                "           tp.productType as 'TypeID', " & _
                                "           pt.Description as 'Type', " & _
                                "           tp.upgradePLU as 'UpgradePLU', " & _
                                "           pb.PLU + ' - ' + pb.[Desc] as 'UpgradePLUDesc', " & _
                                "           tp.preparationTime as 'PrepTime', " & _
                                "           tp.cookingTime as 'CookTime', " & _
                                "           tp.decayTime as 'DecayTime' " & _
                                "FROM       dbo.ST_Products tp " & _
                                "JOIN       dbo.ST_ProductTypes pt " & _
                                "       ON  pt.ProductTypeID = tp.productType " & _
                                "JOIN       dbo.PLUs pa " & _
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
                                "ORDER BY tp.productID " '& _sortField

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
            BindStaticData()
            BindMasterData()
        End If
    End Sub

    Protected Sub btnAddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        hideEditProduct.Value = "NEW"
        litModalTitle.Text = "Add New Product"
        trUpgradeProduct.Visible = False
        ddlProduct.SelectedIndex = 0
        ddlProductType.SelectedIndex = 0
        ddlUpgradeProduct.SelectedIndex = 0
        ddlPrepTime.SelectedIndex = 0
        ddlCookTime.SelectedIndex = 0
        ddlHoldTime.SelectedIndex = 0
        upnEditProduct.Update()
        mpeEditProduct.Show()
    End Sub

    Protected Sub ddlProductType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlProductType.SelectedIndexChanged
        If ddlProductType.SelectedValue = "1" Then
            trUpgradeProduct.Visible = True
        Else
            trUpgradeProduct.Visible = False
        End If
        upnEditProduct.Update()
        mpeEditProduct.Show()
    End Sub

    Protected Sub dgProducts_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgProducts.DeleteCommand
        Dim db As New SqlConnection(GetConnectString())
        Try
            Dim productID As Integer = e.Item.Cells(0).Text
            Dim sql As String = ""
            sql &= "DELETE      dbo.ST_Products "
            sql &= "WHERE       client = 1 "
            sql &= "        AND store = " & _store & " "
            sql &= "        AND productID = " & productID

            db.Open()
            Dim cmd As New SqlCommand(sql, db)
            cmd.ExecuteNonQuery()

            BindMasterData()

        Catch ex As Exception
            lblErrors.Text = "Error Deleting Product: " & ex.Message

        Finally
            Try
                db.Close()
            Catch ex As Exception
            End Try
        End Try
    End Sub

    Protected Sub dgProducts_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgProducts.EditCommand
        hideEditProduct.Value = e.Item.Cells(0).Text
        litModalTitle.Text = "Edit Product " & e.Item.Cells(0).Text
        ddlProduct.SelectedValue = e.Item.Cells(1).Text
        ddlProductType.SelectedValue = e.Item.Cells(3).Text
        If ddlProductType.SelectedValue = "1" Then
            trUpgradeProduct.Visible = True
            ddlUpgradeProduct.SelectedValue = e.Item.Cells(5).Text
        Else
            trUpgradeProduct.Visible = False
        End If
        ddlPrepTime.SelectedValue = e.Item.Cells(7).Text
        ddlCookTime.SelectedValue = e.Item.Cells(8).Text
        ddlHoldTime.SelectedValue = e.Item.Cells(9).Text
        upnEditProduct.Update()
        mpeEditProduct.Show()
    End Sub

    Protected Sub btnEditProductOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEditProductOK.Click
        Dim db As New SqlConnection(GetConnectString())
        Try
            Dim sql As String

            If hideEditProduct.Value = "NEW" Then
                Dim productID As Integer = 0

                sql = ""
                sql &= "SELECT      MAX([productID]) "
                sql &= "FROM        dbo.ST_Products "
                sql &= "WHERE       client = 1 "
                sql &= "        AND store = " & _store

                db.Open()
                Dim cmd As New SqlCommand(sql, db)
                Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)

                If dr.Read() Then
                    If Not IsDBNull(dr(0)) Then
                        productID = dr(0)
                    End If
                End If
                productID += 1

                Try
                    db.Close()
                Catch ex As Exception
                End Try

                sql = ""
                sql &= "INSERT dbo.ST_Products ("
                sql &= "    [client], "
                sql &= "    [store], "
                sql &= "    [productID], "
                sql &= "    [PLU], "
                sql &= "    [productType], "
                sql &= "    [upgradePLU], "
                sql &= "    [preparationTime], "
                sql &= "    [cookingTime], "
                sql &= "    [decayTime], "
                sql &= "    [dtCreated], "
                sql &= "    [dtUpdated]) "
                sql &= "VALUES ("
                sql &= "    1, "
                sql &= "    " & _store & ", "
                sql &= "    " & productID & ", "
                sql &= "    " & ddlProduct.SelectedValue & ", "
                sql &= "    " & ddlProductType.SelectedValue & ", "
                If ddlProductType.SelectedValue = "1" Then
                    sql &= "    " & ddlUpgradeProduct.SelectedValue & ", "
                Else
                    sql &= "    NULL, "
                End If
                sql &= "    " & ddlPrepTime.SelectedValue & ", "
                sql &= "    " & ddlCookTime.SelectedValue & ", "
                sql &= "    " & ddlHoldTime.SelectedValue & ", "
                sql &= "    GETDATE(), "
                sql &= "    GETDATE())"

                db.Open()
                cmd = New SqlCommand(sql, db)
                cmd.ExecuteNonQuery()
            Else
                Dim productID As Integer = hideEditProduct.Value

                sql = ""
                sql &= "UPDATE      dbo.ST_Products "
                sql &= "SET         [PLU] = " & ddlProduct.SelectedValue & ", "
                sql &= "            [productType] = " & ddlProductType.SelectedValue & ", "
                If ddlProductType.SelectedValue = "1" Then
                    sql &= "        [upgradePLU] = " & ddlUpgradeProduct.SelectedValue & ", "
                Else
                    sql &= "        [upgradePLU] = NULL, "
                End If
                sql &= "            [preparationTime] = " & ddlPrepTime.SelectedValue & ", "
                sql &= "            [cookingTime] = " & ddlCookTime.SelectedValue & ", "
                sql &= "            [decayTime] = " & ddlHoldTime.SelectedValue & ", "
                sql &= "            [dtUpdated] = GETDATE() "
                sql &= "WHERE       [client] = 1 "
                sql &= "        AND [store] = " & _store & " "
                sql &= "        AND [productID] = " & productID

                db.Open()
                Dim cmd As New SqlCommand(sql, db)
                cmd.ExecuteNonQuery()
            End If

            BindMasterData()

        Catch ex As Exception
            lblErrors.Text = "Error Updating Product: " & ex.Message

        Finally
            Try
                db.Close()
            Catch ex As Exception
            End Try
        End Try
    End Sub

    Protected Sub dgProducts_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgProducts.ItemCreated
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim btnDelete As Button = CType(e.Item.Cells(11).Controls(0), Button)
            btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this product?');")
        End If
    End Sub
End Class
