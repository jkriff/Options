Imports System.Net
Imports System.Text


Namespace Register


    Partial Class SuggestSell
        Inherits System.Web.UI.Page

        Dim webService As WebService.Exchange

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
            'Put user code to initialize the page here
            If webService Is Nothing Then
                Dim host As String = Request("HTTP_HOST").ToString()
                host = "127.0.0.1"
                webService = New WebService.Exchange()
                webService.Url = "http://" + host + "/WebService/Exchange.asmx"
                If host <> "localhost" Then
                    Dim proxyObject As New WebProxy("http://" + host + "/WebService:80", True)
                    webService.Proxy = proxyObject
                End If
            End If

            Me.lblError.Text = ""
            'check for user role/permission to view store(s)
            Me.btnSaveSellPlus.Text = "Save Sell" & vbCrLf & "Plu changes"
            Me.btnAddPlu.Text = "Add Sell" & vbCrLf & "Plu to Group"
            Me.btnAddBuyPlu.Text = "Add Buy" & vbCrLf & "Plu to Group"
            Me.btnSaveBuyPlu.Text = "Save Buy" & vbCrLf & "Plu changes"

            If Not IsPostBack Then
                'get list of store numbers to select from

                Dim sStore As String = String.Empty
                If Not Request.Form("STORE") Is Nothing Then
                    sStore = Request.Form("STORE").ToString()
                    'Response.Write("store=" & sStore)
                Else
                    'Response.Write("Nothing")
                End If

                Dim sStoreAccess As String = String.Empty
                If Not Request.Form("STOREACCESS") Is Nothing Then
                    sStoreAccess = Request.Form("STOREACCESS").ToString()
                    'Response.Write("store=" & sStore)
                Else
                    'Response.Write("Nothing")
                End If

                'Dim whereSQL As String = String.Empty
                'If IsNumeric(sStoreAccess) Then
                '    whereSQL = "WHERE [store] = '" & sStore & "'"
                'ElseIf sStoreAccess = "All Stores" Then
                '    whereSQL = "WHERE [store] > 0"
                'Else
                '    whereSQL = "WHERE [store] IN (SELECT [store] from [storegroups] where [groupname] = '" & sStoreAccess & "')"
                'End If

                Dim dsStores As DataSet = webService.downloadSqlDataSet("SELECT [store], CAST([store] AS varchar(20)) + ' - ' + [name] AS 'StoreName' FROM [stores] WHERE [store] = '" & sStore & "' ORDER BY [store]")
                Me.ddlStores.Items.Clear()

                Me.ddlStores.DataSource = dsStores.Tables(0)
                Me.ddlStores.DataTextField = "storename"
                Me.ddlStores.DataValueField = "store"
                Me.ddlStores.DataBind()
                Me.ddlStores.Items.Insert(0, "Select Store")

                If sStore <> String.Empty Then
                    Me.ddlStores.SelectedValue = sStore.Trim
                Else
                    Me.ddlStores.SelectedIndex = 0  'force the selection of a store before retireving menu info
                End If

                lblStore.Text() = "Store# " & ddlStores.SelectedItem.Text

                Dim sbSQL As New StringBuilder("SELECT [store], [groupname], [group_id] FROM [suggest_group] WHERE [store] > '0' ")
                sbSQL.Append("AND [store] IN (SELECT [store] FROM [Stores]) ORDER BY [store], [groupname]")
                Dim ds As DataSet = webService.downloadSqlDataSet(sbSQL.ToString)
                'Cache.Item("dtGroup") = ds.Tables(0)
                'Cache.Insert("dtGroup", ds.Tables(0), New Caching.CacheDependency(""), Cache.NoAbsoluteExpiration, TimeSpan.FromHours(3))
                'decided to keep it in session to avoid others from keeping or erasing changes since cache is like a static variable
                Session.Item("dtGroup") = ds.Tables(0)
                updateGroups()

                Me.dgGroup.Columns(0).ItemStyle.Width = Web.UI.WebControls.Unit.Pixel(100)  'edit, update, cancel
                Me.dgGroup.Columns(1).ItemStyle.Width = Web.UI.WebControls.Unit.Pixel(40)   'delete
                Me.dgGroup.Columns(2).ItemStyle.Width = Web.UI.WebControls.Unit.Pixel(65)   'store
                Me.dgGroup.Columns(3).ItemStyle.Width = Web.UI.WebControls.Unit.Pixel(210)  'group name
                Me.btnSaveGroups.Enabled = False

                Me.dgBuyPlus.Columns(0).ItemStyle.Width = Web.UI.WebControls.Unit.Pixel(40)    'Select
                Me.dgBuyPlus.Columns(1).ItemStyle.Width = Web.UI.WebControls.Unit.Pixel(100)   'edit, update, cancel
                Me.dgBuyPlus.Columns(2).ItemStyle.Width = Web.UI.WebControls.Unit.Pixel(40)    'delete
                Me.dgBuyPlus.Columns(3).ItemStyle.Width = Web.UI.WebControls.Unit.Pixel(65)    'store
                Me.dgBuyPlus.Columns(4).ItemStyle.Width = Web.UI.WebControls.Unit.Pixel(200)   'group name
                Me.dgBuyPlus.Columns(5).ItemStyle.Width = Web.UI.WebControls.Unit.Pixel(160)   'Plu
                Me.btnSaveBuyPlu.Enabled = False
                updateBuyPLUs()

                Me.dgSellPlus.Columns(0).ItemStyle.Width = Web.UI.WebControls.Unit.Pixel(40)    'Select
                Me.dgSellPlus.Columns(1).ItemStyle.Width = Web.UI.WebControls.Unit.Pixel(100)   'edit, update, cancel
                Me.dgSellPlus.Columns(2).ItemStyle.Width = Web.UI.WebControls.Unit.Pixel(40)    'delete
                Me.dgSellPlus.Columns(3).ItemStyle.Width = Web.UI.WebControls.Unit.Pixel(65)    'store
                Me.dgSellPlus.Columns(4).ItemStyle.Width = Web.UI.WebControls.Unit.Pixel(200)   'group name
                Me.dgSellPlus.Columns(5).ItemStyle.Width = Web.UI.WebControls.Unit.Pixel(160)   'Plu
                Me.btnSaveSellPlus.Enabled = False
                updateSuggestPLUs()

                sbSQL.Remove(0, sbSQL.Length)
                sbSQL.Append("SELECT [plu] + ' - ' + [desc] AS 'PluName', [plu], [desc], [dept], [store] FROM [Plus] WHERE [store] > '0' ")
                sbSQL.Append("AND [store] IN (SELECT [store] FROM [Stores]) ORDER BY [store], LEN([plu]), [plu]")
                Dim dsPluList As DataSet = webService.downloadSqlDataSet(sbSQL.ToString)
                Session.Item("dtPluList") = dsPluList.Tables(0)

            End If
        End Sub

        Private Sub ddlStores_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlStores.SelectedIndexChanged
            If ddlStores.SelectedValue = "Select Store" Then
            Else
                updateGroups()
                updateSuggestPLUs()
                updateBuyPLUs()
            End If

        End Sub

        Private Sub dgGroup_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgGroup.EditCommand
            dgGroup.EditItemIndex = e.Item.ItemIndex
            dgGroup.DataSource = Session.Item("dtGroup")
            dgGroup.DataBind()
        End Sub

        Private Sub dgGroup_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgGroup.CancelCommand
            dgGroup.EditItemIndex = -1
            dgGroup.DataSource = Session.Item("dtGroup")
            dgGroup.DataBind()
        End Sub

        Private Sub dgGroup_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgGroup.UpdateCommand
            Dim txtGroupName As TextBox = CType(e.Item.Cells(3).Controls(0), TextBox)
            'Dim eStore As String = e.Item.Cells(2).Text    'Store
            'Dim oldGroup As String = e.Item.Cells(3).Text  'GroupName
            Dim eGroup_ID As String = e.Item.Cells(4).Text  'Group_ID
            Dim dtGroup As DataTable = Session.Item("dtGroup")
            dtGroup.DefaultView.RowFilter = "group_id='" & eGroup_ID & "'"
            If dtGroup.DefaultView.Count > 0 Then
                dtGroup.DefaultView.Item(0).Row.Item("GroupName") = txtGroupName.Text
                Me.btnSaveGroups.Enabled = True
            End If
            If Me.ddlStores.SelectedIndex > 0 Then
                dtGroup.DefaultView.RowFilter = "store=" & Me.ddlStores.SelectedValue
            Else
                dtGroup.DefaultView.RowFilter = ""
            End If
            Session.Item("dtGroup") = dtGroup
            dgGroup.EditItemIndex = -1
            dgGroup.DataSource = dtGroup
            dgGroup.DataBind()
        End Sub

        Private Sub dgGroup_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgGroup.DeleteCommand
            Dim dtGroup As DataTable = Session.Item("dtGroup")
            dtGroup.DefaultView.RowFilter = "group_id=" & e.Item.Cells(4).Text
            dtGroup.DefaultView.Delete(0)
            If Me.ddlStores.SelectedIndex = 0 Then
                dtGroup.DefaultView.RowFilter = ""
            Else
                dtGroup.DefaultView.RowFilter = "store=" & Me.ddlStores.SelectedValue
            End If
            Session.Item("dtGroup") = dtGroup
            Me.dgGroup.DataSource = dtGroup
            Me.dgGroup.DataBind()
            Me.btnSaveGroups.Enabled = True
        End Sub

        Private Sub btnSaveGroups_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveGroups.Click
            Dim sbSQL As New StringBuilder
            Dim dtGroup As DataTable = Session.Item("dtGroup")
            Dim updateError As Boolean = False
            Dim rowEdited As Boolean = False

            dtGroup.DefaultView.RowStateFilter = DataViewRowState.Deleted
            Dim delCount As Int32 = dtGroup.DefaultView.Count   'total number of deleted rows
            Dim delRowNum(delCount) As String
            Dim j As Int32 = 0

            'must use the rowstate field and check the entire table
            dtGroup.DefaultView.RowStateFilter = DataViewRowState.OriginalRows
            For ix As Int32 = 0 To dtGroup.Rows.Count - 1
                sbSQL.Remove(0, sbSQL.Length)
                Select Case dtGroup.Rows.Item(ix).RowState
                    Case DataRowState.Modified
                        sbSQL.Append("UPDATE [Suggest_Group] SET [groupname] = '")
                        sbSQL.Append(dtGroup.Rows.Item(ix).Item("GroupName"))
                        sbSQL.Append("' ")
                        sbSQL.Append("WHERE [group_id] = '")
                        sbSQL.Append(dtGroup.Rows.Item(ix).Item("Group_ID"))
                        sbSQL.Append("' ")
                        rowEdited = True

                    Case DataRowState.Added
                        'must get ID from DB immediately since that is the where clause parameter for updates
                        sbSQL.Append("exec p_suggest_group_i ")
                        sbSQL.Append(dtGroup.Rows.Item(ix).Item("Store"))
                        sbSQL.Append(", ")
                        sbSQL.Append(dtGroup.Rows.Item(ix).Item("GroupName"))
                        rowEdited = True

                    Case DataRowState.Deleted
                        sbSQL.Append("EXEC p_Suggest_Group_d ") 'requires group_id of group to delete
                        sbSQL.Append(dtGroup.Rows.Item(ix).Item("Group_ID", DataRowVersion.Original))
                        rowEdited = True

                    Case Else
                        'do nothing??
                        rowEdited = False

                End Select

                If rowEdited Then
                    Dim response As String = webService.UpdateWithSQL(sbSQL.ToString)
                    Dim dbGroup_ID As Int32
                    If response.ToUpper = "SUCCESS" Then
                        If dtGroup.Rows.Item(ix).RowState = DataRowState.Added Then
                            Dim sbID As New StringBuilder("SELECT [group_id] FROM [Suggest_Group] WHERE [store]=")
                            sbID.Append(dtGroup.Rows.Item(ix).Item("store"))
                            sbID.Append(" AND [groupname]='")
                            sbID.Append(dtGroup.Rows.Item(ix).Item("GroupName"))
                            sbID.Append("' ")
                            Dim dsID As DataSet = webService.downloadSqlDataSet(sbID.ToString)
                            '
                            'should be first column of first row of first table returned in dataset and be the only value ?
                            'prevent duplicates from being added prior to database add 
                            '
                            If Not (dsID.Tables(0).Rows(0).Item("group_id") Is DBNull.Value) Then
                                dbGroup_ID = dsID.Tables(0).Rows(0).Item("group_id")
                                dtGroup.Rows.Item(ix).Item("Group_ID") = dbGroup_ID
                            Else
                                '
                                '?? the row was just inserted  ??
                                '
                            End If
                        End If
                        'set changed field to false since server was updated
                        'cannot accept changes for a deleted row, it changed the overall row count and causes errors
                        If dtGroup.Rows.Item(ix).RowState <> DataRowState.Deleted Then
                            dtGroup.Rows.Item(ix).AcceptChanges()
                        Else
                            'keep track of deleted rows?
                            delRowNum(j) = ix
                            j = j + 1
                        End If
                    Else
                        'an error occured, display message to user
                        Me.lblError.Text = "An error occured while updating the server, some or all of the changes were not saved!<br>Returned: " & response
                        updateError = True
                        Exit For
                    End If
                End If
            Next
            If Not updateError Then
                'accept changes for all deleted rows/all changes
                dtGroup.AcceptChanges()
                Me.btnSaveGroups.Enabled = False
                Me.lblError.Text = ""
            Else
                'accept changes that did not cause error, deleted rows only
                For ix As Int32 = 0 To delRowNum.Length - 1
                    If delRowNum(ix) <> String.Empty Then
                        dtGroup.Rows.Item(delRowNum(ix)).AcceptChanges()
                    End If
                Next
            End If

            dtGroup.DefaultView.RowStateFilter = DataViewRowState.CurrentRows
            If Me.ddlStores.SelectedIndex = 0 Then
                dtGroup.DefaultView.RowFilter = ""
            Else
                dtGroup.DefaultView.RowFilter = "store=" & Me.ddlStores.SelectedValue
            End If
            Session.Item("dtGroup") = dtGroup
            Me.dgGroup.DataSource = dtGroup
            Me.dgGroup.DataBind()

            updateSuggestPLUs()
            updateBuyPLUs()

        End Sub

        Private Sub btnAddGroup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddGroup.Click
            If Me.ddlStores.SelectedIndex > 0 Then
                Me.txtGroup.Text = String.Empty
                Me.txtGroup.Visible = True
            Else
                'Please select a store first
                Me.lblError.Text = "Please Select a store before adding a Suggestive Sales Group"
            End If
        End Sub

        Private Sub txtGroup_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtGroup.TextChanged
            If txtGroup.Text.Length > 0 Then
                Dim newID As Int32
                Dim dtGroup As DataTable = Session("dtGroup")
                Dim dr As DataRow = dtGroup.NewRow

                'get max group_ID
                dtGroup.DefaultView.RowFilter = ""
                dtGroup.DefaultView.Sort = "Group_ID desc"
                If dtGroup.DefaultView.Count > 0 Then
                    newID = dtGroup.DefaultView.Item(0).Row.Item("Group_ID") + 1   'once saved to server this number may change
                Else
                    newID = 1
                End If

                dr.Item("Group_id") = newID
                dr.Item("store") = Me.ddlStores.SelectedValue
                dr.Item("GroupName") = Me.txtGroup.Text
                dtGroup.Rows.Add(dr)

                dtGroup.DefaultView.Sort = "store, groupname"
                dtGroup.DefaultView.RowFilter = "store=" & Me.ddlStores.SelectedValue

                Me.dgGroup.DataSource = dtGroup
                Me.dgGroup.DataBind()

                'force saving new group to server before updating plu grid

                Me.txtGroup.Visible = False
                Me.btnSaveGroups.Enabled = True
            End If
        End Sub

        Private Sub updateGroups()
            Dim dtGroup As DataTable = Session.Item("dtGroup")
            If IsNumeric(Me.ddlStores.SelectedValue) Then
                dtGroup.DefaultView.RowFilter = "store=" & Me.ddlStores.SelectedValue
            Else
                dtGroup.DefaultView.RowFilter = ""
            End If
            dgGroup.DataSource = dtGroup
            dgGroup.DataBind()

            'Dim dgHeight As Int32 = 0
            'Dim tblRows As Int16 = dtGroup.Rows.Count
            'If tblRows < dgGroup.PageSize Then
            '    dgHeight = dgGroup.ItemStyle.Height.Value * tblRows + dgGroup.PagerStyle.Height.Value
            'Else
            '    dgHeight = dgGroup.ItemStyle.Height.Value * dgGroup.PageSize + dgGroup.PagerStyle.Height.Value
            'End If

        End Sub

        Private Sub dgSellPlus_EditCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgSellPlus.EditCommand
            Dim dtPLU As DataTable = Session.Item("dtPLU")
            dgSellPlus.EditItemIndex = e.Item.ItemIndex
            If Me.ddlStores.SelectedIndex > 0 Then
                dtPLU.DefaultView.RowFilter = "store=" & Me.ddlStores.SelectedValue
            Else
                dtPLU.DefaultView.RowFilter = ""
            End If
            dgSellPlus.DataSource = Session.Item("dtPLU")
            dgSellPlus.DataBind()
        End Sub

        Private Sub dgSellPlus_CancelCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgSellPlus.CancelCommand
            dgSellPlus.EditItemIndex = -1
            dgSellPlus.DataSource = Session.Item("dtPLU")
            dgSellPlus.DataBind()
        End Sub

        Private Sub dgSellPlus_DeleteCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgSellPlus.DeleteCommand
            Dim dtPlu As DataTable = Session.Item("dtPlu")
            dtPlu.DefaultView.RowFilter = "SellPlu_id=" & e.Item.Cells(7).Text
            dtPlu.DefaultView.Delete(0)
            If Me.ddlStores.SelectedIndex = 0 Then
                dtPlu.DefaultView.RowFilter = ""
            Else
                dtPlu.DefaultView.RowFilter = "store=" & Me.ddlStores.SelectedValue
            End If
            Session.Item("dtPlu") = dtPlu
            Me.dgSellPlus.DataSource = dtPlu
            Me.dgSellPlus.DataBind()
            Me.btnSaveSellPlus.Enabled = True
        End Sub

        Private Sub dgSellPlus_UpdateCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgSellPlus.UpdateCommand
            Dim ddlEdit As DropDownList = CType(e.Item.Cells(5).Controls(1), DropDownList)
            'Dim newValue = ddlEdit.SelectedValue
            Dim dtPlu As DataTable = Session.Item("dtPlu")
            Dim sbfilter As New StringBuilder("(group_id=")
            sbfilter.Append(e.Item.Cells(6).Text)
            sbfilter.Append(") and (sellplu_id=")
            sbfilter.Append(e.Item.Cells(7).Text)
            sbfilter.Append(")")
            dtPlu.DefaultView.RowFilter = sbfilter.ToString
            If dtPlu.DefaultView.Count > 0 Then
                dtPlu.DefaultView.Item(0).Row.Item("Sellplu") = ddlEdit.SelectedValue
                dtPlu.DefaultView.Item(0).Row.Item("PluName") = ddlEdit.SelectedItem
                'save plu edits!!!
            End If
            If Me.ddlStores.SelectedIndex > 0 Then
                dtPlu.DefaultView.RowFilter = "store=" & Me.ddlStores.SelectedValue
            Else
                dtPlu.DefaultView.RowFilter = ""
            End If
            Session.Item("dtPlu") = dtPlu
            dgSellPlus.EditItemIndex = -1
            dgSellPlus.DataSource = dtPlu
            dgSellPlus.DataBind()
            Me.btnSaveSellPlus.Enabled = True
        End Sub

        Private Sub dgSellPlus_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgSellPlus.ItemDataBound
            If e.Item.ItemType = ListItemType.EditItem Then
                Dim ddlEdit As DropDownList = CType(e.Item.Cells(5).Controls(1), DropDownList)
                Dim dtPluList As DataTable = Session.Item("dtPluList")
                dtPluList.DefaultView.RowFilter = "store=" & e.Item.Cells(3).Text
                ddlEdit.DataSource = dtPluList
                ddlEdit.DataTextField = "pluname"
                ddlEdit.DataValueField = "plu"
                ddlEdit.DataBind()
                If e.Item.Cells(8).Text > 0 Then
                    ddlEdit.SelectedValue = e.Item.Cells(8).Text
                End If

            End If
        End Sub

        Private Sub btnSaveSellPlus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveSellPlus.Click
            Dim dtPlu As DataTable = Session.Item("dtPlu")
            Dim sbSQL As New StringBuilder
            Dim rowEdited As Boolean = False
            Dim updateError As Boolean = False

            dtPlu.DefaultView.RowStateFilter = DataViewRowState.OriginalRows
            For ix As Int32 = 0 To dtPlu.Rows.Count - 1
                sbSQL.Remove(0, sbSQL.Length)
                Select Case dtPlu.Rows.Item(ix).RowState
                    Case DataRowState.Modified
                        sbSQL.Append("UPDATE [Suggest_SellPlu] SET [sellplu] = '")
                        sbSQL.Append(dtPlu.Rows.Item(ix).Item("SellPlu"))
                        sbSQL.Append("' ")
                        sbSQL.Append("WHERE [sellplu_id] = '")
                        sbSQL.Append(dtPlu.Rows.Item(ix).Item("SellPlu_ID"))
                        sbSQL.Append("' ")
                        rowEdited = True

                    Case DataRowState.Added
                        sbSQL.Append("exec p_Suggest_SellPlu_i ")
                        sbSQL.Append(dtPlu.Rows.Item(ix).Item("Store"))
                        sbSQL.Append(", '")
                        sbSQL.Append(dtPlu.Rows.Item(ix).Item("SellPlu"))
                        sbSQL.Append("', ")
                        sbSQL.Append(dtPlu.Rows.Item(ix).Item("Group_ID"))
                        rowEdited = True

                    Case DataRowState.Deleted
                        sbSQL.Append("EXEC p_Suggest_SellPlu_d ")
                        sbSQL.Append(dtPlu.Rows.Item(ix).Item("SellPlu_ID", DataRowVersion.Original))
                        sbSQL.Append(", ")
                        sbSQL.Append(dtPlu.Rows.Item(ix).Item("Group_ID", DataRowVersion.Original))
                        rowEdited = True

                    Case Else
                        'do nothing??
                        rowEdited = False

                End Select

                If rowEdited Then
                    Dim response As String = webService.UpdateWithSQL(sbSQL.ToString)
                    Dim dbItem_ID As Int32
                    If response.ToUpper = "SUCCESS" Then
                        'if added then may need to update item_ID

                    Else
                        Me.lblError.Text = "An error occured while updating the server, some or all of the changes were not saved!<br>Returned: " & response
                        updateError = True
                        Exit For
                    End If
                End If
            Next

            If Not updateError Then
                Me.btnSaveSellPlus.Enabled = False
                Me.lblError.Text = ""
            End If

            updateSuggestPLUs()

        End Sub

        Private Sub btnAddPlu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddPlu.Click
            If Me.dgSellPlus.SelectedIndex > -1 Then
                Dim dtPlu As DataTable = Session.Item("dtPlu")
                Dim dr As DataRow = dtPlu.NewRow
                dr.Item("Store") = Me.dgSellPlus.SelectedItem.Cells(3).Text
                dr.Item("GroupName") = Me.dgSellPlus.SelectedItem.Cells(4).Text
                dr.Item("PluName") = "" 'if this is new then this should be blank
                dr.Item("Group_ID") = Me.dgSellPlus.SelectedItem.Cells(6).Text
                dr.Item("SellPlu_ID") = "-1" 'the item_id is autogenerated upon insert to DB, this is just a place holder until saved to db
                dr.Item("SellPlu") = "0"
                dtPlu.Rows.Add(dr)
                dtPlu.DefaultView.Sort = "Store, GroupName, SellPlu"
                Session.Item("dtPlu") = dtPlu
                Me.dgSellPlus.DataSource = dtPlu
                Me.dgSellPlus.DataBind()
            Else
                Me.lblError.Text = "Please Select a store and group from the grid before adding a PLU to a group"
            End If
            Me.dgSellPlus.SelectedIndex = -1
        End Sub

        Private Sub updateSuggestPLUs()
            Dim sbSQL As New StringBuilder
            sbSQL.Remove(0, sbSQL.Length)
            sbSQL.Append("SELECT [store], [groupname], [sellplu], [group_id], [sellplu_id], [sellplu] + ' - ' + [desc] AS 'PluName' FROM [vw_Suggest_SellPlu] WHERE [store] > '0' ")
            Dim dsPLUs As DataSet = webService.downloadSqlDataSet(sbSQL.ToString)
            Session.Item("dtPlu") = dsPLUs.Tables(0)
            If Me.ddlStores.SelectedIndex > 0 Then
                dsPLUs.Tables(0).DefaultView.RowFilter = "store=" & Me.ddlStores.SelectedValue
            Else
                dsPLUs.Tables(0).DefaultView.RowFilter = ""
            End If
            dgSellPlus.DataSource = dsPLUs.Tables(0)
            dgSellPlus.DataBind()

        End Sub

        Private Sub dgBuyPlus_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgBuyPlus.EditCommand
            dgBuyPlus.EditItemIndex = e.Item.ItemIndex
            Dim dtBuyPlu As DataTable = Session.Item("dtBuyPlu")
            If Me.ddlStores.SelectedIndex > 0 Then
                dtBuyPlu.DefaultView.RowFilter = "store=" & Me.ddlStores.SelectedValue
            Else
                dtBuyPlu.DefaultView.RowFilter = ""
            End If
            dgBuyPlus.DataSource = dtBuyPlu
            dgBuyPlus.DataBind()
        End Sub

        Private Sub dgBuyPlus_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgBuyPlus.CancelCommand
            dgBuyPlus.EditItemIndex = -1
            dgBuyPlus.DataSource = Session.Item("dtBuyPlu")
            dgBuyPlus.DataBind()
        End Sub

        Private Sub dgBuyPlus_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgBuyPlus.DeleteCommand
            Dim dtBuyPlu As DataTable = Session.Item("dtBuyPlu")
            dtBuyPlu.DefaultView.RowFilter = "BuyPlu_id=" & e.Item.Cells(7).Text
            dtBuyPlu.DefaultView.Delete(0)
            If Me.ddlStores.SelectedIndex = 0 Then
                dtBuyPlu.DefaultView.RowFilter = ""
            Else
                dtBuyPlu.DefaultView.RowFilter = "store=" & Me.ddlStores.SelectedValue
            End If
            Session.Item("dtBuyPlu") = dtBuyPlu
            Me.dgBuyPlus.DataSource = dtBuyPlu
            Me.dgBuyPlus.DataBind()
            Me.btnSaveBuyPlu.Enabled = True

        End Sub

        Private Sub dgBuyPlus_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgBuyPlus.UpdateCommand
            Dim ddlEdit2 As DropDownList = CType(e.Item.Cells(5).Controls(1), DropDownList)
            Dim dtBuyPlu As DataTable = Session.Item("dtBuyPlu")
            Dim sbfilter As New StringBuilder("(group_id=")
            sbfilter.Append(e.Item.Cells(6).Text)
            sbfilter.Append(") and (buyplu_id=")
            sbfilter.Append(e.Item.Cells(7).Text)
            sbfilter.Append(")")
            dtBuyPlu.DefaultView.RowFilter = sbfilter.ToString
            If dtBuyPlu.DefaultView.Count > 0 Then
                dtBuyPlu.DefaultView.Item(0).Row.Item("BuyPlu") = ddlEdit2.SelectedValue
                dtBuyPlu.DefaultView.Item(0).Row.Item("PluName") = ddlEdit2.SelectedItem
                'save plu edits!!!
            End If
            If Me.ddlStores.SelectedIndex > 0 Then
                dtBuyPlu.DefaultView.RowFilter = "store=" & Me.ddlStores.SelectedValue
            Else
                dtBuyPlu.DefaultView.RowFilter = ""
            End If
            Session.Item("dtBuyPlu") = dtBuyPlu
            dgBuyPlus.EditItemIndex = -1
            dgBuyPlus.DataSource = dtBuyPlu
            dgBuyPlus.DataBind()
            Me.btnSaveBuyPlu.Enabled = True
        End Sub

        Private Sub dgBuyPlus_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgBuyPlus.ItemDataBound
            If e.Item.ItemType = ListItemType.EditItem Then
                Dim ddlEdit2 As DropDownList = CType(e.Item.Cells(5).Controls(1), DropDownList)
                Dim dtPluList As DataTable = Session.Item("dtPluList")
                dtPluList.DefaultView.RowFilter = "store=" & e.Item.Cells(3).Text
                ddlEdit2.DataSource = dtPluList
                ddlEdit2.DataTextField = "pluname"
                ddlEdit2.DataValueField = "plu"
                ddlEdit2.DataBind()
                If e.Item.Cells(8).Text > 0 Then
                    ddlEdit2.SelectedValue = e.Item.Cells(8).Text
                End If

            End If
        End Sub

        Private Sub btnAddBuyPlu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddBuyPlu.Click
            If Me.dgBuyPlus.SelectedIndex > -1 Then
                Dim dtBuyPlu As DataTable = Session.Item("dtBuyPlu")
                Dim dr As DataRow = dtBuyPlu.NewRow
                dr.Item("Store") = Me.dgBuyPlus.SelectedItem.Cells(3).Text
                dr.Item("GroupName") = Me.dgBuyPlus.SelectedItem.Cells(4).Text
                dr.Item("PluName") = "" 'if this is new then this should be blank
                dr.Item("Group_ID") = Me.dgBuyPlus.SelectedItem.Cells(6).Text
                dr.Item("BuyPlu_ID") = "-1" 'the item_id is autogenerated upon insert to DB, this is just a place holder until saved to db
                dr.Item("BuyPlu") = "0"
                dtBuyPlu.Rows.Add(dr)
                dtBuyPlu.DefaultView.Sort = "Store, GroupName, BuyPlu"
                Session.Item("dtPlu") = dtBuyPlu
                Me.dgBuyPlus.DataSource = dtBuyPlu
                Me.dgBuyPlus.DataBind()
            Else
                Me.lblError.Text = "Please Select a store and group from the grid before adding a PLU to a group"
            End If
            Me.dgBuyPlus.SelectedIndex = -1
        End Sub

        Private Sub btnSaveBuyPlu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveBuyPlu.Click
            Dim dtBuyPlu As DataTable = Session.Item("dtBuyPlu")
            Dim sbSQL As New StringBuilder
            Dim rowEdited As Boolean = False
            Dim updateError As Boolean = False

            dtBuyPlu.DefaultView.RowStateFilter = DataViewRowState.OriginalRows
            For ix As Int32 = 0 To dtBuyPlu.Rows.Count - 1
                sbSQL.Remove(0, sbSQL.Length)
                Select Case dtBuyPlu.Rows.Item(ix).RowState
                    Case DataRowState.Modified
                        sbSQL.Append("UPDATE [Suggest_BuyPlu] SET [buyplu] = '")
                        sbSQL.Append(dtBuyPlu.Rows.Item(ix).Item("BuyPlu"))
                        sbSQL.Append("' ")
                        sbSQL.Append("WHERE [buyplu_id] = '")
                        sbSQL.Append(dtBuyPlu.Rows.Item(ix).Item("BuyPlu_ID"))
                        sbSQL.Append("' ")
                        rowEdited = True

                    Case DataRowState.Added
                        sbSQL.Append("exec p_Suggest_BuyPlu_i ")
                        sbSQL.Append(dtBuyPlu.Rows.Item(ix).Item("Store"))
                        sbSQL.Append(", '")
                        sbSQL.Append(dtBuyPlu.Rows.Item(ix).Item("BuyPlu"))
                        sbSQL.Append("', ")
                        sbSQL.Append(dtBuyPlu.Rows.Item(ix).Item("Group_ID"))
                        rowEdited = True

                    Case DataRowState.Deleted
                        sbSQL.Append("EXEC p_Suggest_BuyPlu_d ")
                        sbSQL.Append(dtBuyPlu.Rows.Item(ix).Item("BuyPlu_ID", DataRowVersion.Original))
                        sbSQL.Append(", ")
                        sbSQL.Append(dtBuyPlu.Rows.Item(ix).Item("Group_ID", DataRowVersion.Original))
                        rowEdited = True

                    Case Else
                        'do nothing??
                        rowEdited = False

                End Select

                If rowEdited Then
                    Dim response As String = webService.UpdateWithSQL(sbSQL.ToString)
                    Dim dbItem_ID As Int32
                    If response.ToUpper = "SUCCESS" Then
                        'if added then may need to update item_ID

                    Else
                        Me.lblError.Text = "An error occured while updating the server, some or all of the changes were not saved!<br>Returned: " & response
                        updateError = True
                        Exit For
                    End If
                End If
            Next

            If Not updateError Then
                Me.btnSaveBuyPlu.Enabled = False
                Me.lblError.Text = ""
            End If

            updateBuyPLUs()

        End Sub

        Private Sub updateBuyPLUs()
            Dim sbSQL As New StringBuilder
            sbSQL.Remove(0, sbSQL.Length)
            sbSQL.Append("SELECT [store], [groupname], [buyplu], [group_id], [buyplu_id], [buyplu] + ' - ' + [desc] AS 'PluName' FROM [vw_Suggest_BuyPlu] WHERE [store] > '0' ")
            Dim dsBuyPlus As DataSet = webService.downloadSqlDataSet(sbSQL.ToString)
            Session.Item("dtBuyPlu") = dsBuyPlus.Tables(0)
            If Me.ddlStores.SelectedIndex > 0 Then
                dsBuyPlus.Tables(0).DefaultView.RowFilter = "store=" & Me.ddlStores.SelectedValue
            Else
                dsBuyPlus.Tables(0).DefaultView.RowFilter = ""
            End If
            dgBuyPlus.DataSource = dsBuyPlus.Tables(0)
            dgBuyPlus.DataBind()

        End Sub

    End Class

End Namespace
