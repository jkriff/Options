using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class MeatChzRecipeEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ListItem li = new ListItem("Select or Enter a PLU", "0");
            cbPLU.Items.Insert(0, li);
        }
        dsPLU.ConnectionString = GetConnectString();
        dsPLU.SelectCommand = "Select plu from plu_MeatCheeseInventory_tbl group by plu";

        dsRecipe.ConnectionString = GetConnectString();
        dsRecipe.SelectCommand = "Select pm.MeatChzID, MeatChzDescrip, Round(amount, 2) as Amount from plu_MeatCheeseInventory_tbl pm join MeatCheezeType mc on pm.MeatChzID = mc.MeatChzID"
            + " where plu = @PLU";

        dsMeatChz.ConnectionString = GetConnectString();
        dsMeatChz.SelectCommand = "Select MeatChzID, MeatChzDescrip from MeatCheezeType";
        dsMeatChz.InsertCommand = "Insert into MeatCheezetype (MeatChzID, MeatChzDescrip) values (@MeatChzId, @MeatChzDescrip)";

    }

    private string GetConnectString()
    {
        DataAccess data1 = new DataAccess();
        return data1.UnMaskString(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Production"].ConnectionString, 1, 2, 3, 4, 5);
    }
    protected void cbPLU_DataBound(object sender, EventArgs e)
    {
        ListItem li = new ListItem("Select or Enter a PLU", "0");
        cbPLU.Items.Insert(0, li);
    }

    protected void lvRecipe_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ((DropDownList)e.Item.FindControl("ddlDescription")).SelectedIndex = int.Parse(((TextBox)e.Item.FindControl("tbDescriptionId")).Text);
    }
    protected void lvRecipe_ItemInserting(object sender, ListViewInsertEventArgs e)
    {

        //e.Values("MeatId") = ((AjaxControlToolkit.ComboBox)e.Item.FindControl("cbDescription")).Text;
    }

    protected void cbPLU_ItemInserting(object sender, AjaxControlToolkit.ComboBoxItemInsertEventArgs e)
    {
        lblPopupMessage.Text = "Do you intend to add a new PLU '" + e.Item + "'";
        mdlConfirm.Show();
    }
}
