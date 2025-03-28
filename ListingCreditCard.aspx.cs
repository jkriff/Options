using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

public partial class ListingCreditCard : System.Web.UI.Page
{
    string store = "";
    string BegDate = "";
    string EndDate = "";
    string visa = "";
    string mastercard = "";
    string discover = "";
    string amex = "";


    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {

            try
            {
                if (Request.Form["store"] != null)
                {
                    if (Request.Form["Begdate"].Contains('/'))
                    {
                        store = Request.Form["Store"].ToString();
                        BegDate = ToYYYYMMDD(Request.Form["BegDate"].ToString());
                        EndDate = ToYYYYMMDD(Request.Form["EndDate"].ToString());
                    }
                    else
                    {
                        store = Request.Form["Store"].ToString();
                        BegDate = Request.Form["BegDate"].ToString();
                        EndDate = Request.Form["EndDate"].ToString();
                    }
                }
                else
                {
                    store = "93";
                    BegDate = "20091225";
                    EndDate = "20091229";
                }
                tbStoreId.Text = store.ToString();
                tbStartDate.Text = BegDate;
                tbEndDate.Text = EndDate;

                lblAsOf.Text = DateTime.Now.ToString();
                lblDates.Text = fromYYYYMMDD(tbStartDate.Text) + " - " + fromYYYYMMDD(tbEndDate.Text);
                lblStore.Text = tbStoreId.Text;

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();

                SqlCommand cmd = new SqlCommand();


                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_Report_ListingCreditCardmonthly";
                cmd.Parameters.Add(new SqlParameter("@Store", store));
                cmd.Parameters.Add(new SqlParameter("@Beginning_Date", BegDate));
                cmd.Parameters.Add(new SqlParameter("@Ending_Date", EndDate));
                cmd.Parameters.Add(new SqlParameter("@Visa", SqlDbType.Decimal, 20, ParameterDirection.Output, true, 10, 2, "visa", DataRowVersion.Current, 0.00));
                cmd.Parameters.Add(new SqlParameter("@Mastercard", SqlDbType.Decimal, 20, ParameterDirection.Output, true, 10, 2, "Mastercard", DataRowVersion.Current, 0.00));
                cmd.Parameters.Add(new SqlParameter("@Discover", SqlDbType.Decimal, 20, ParameterDirection.Output, true, 10, 2, "Discover", DataRowVersion.Current, 0.00));
                cmd.Parameters.Add(new SqlParameter("@AMEX", SqlDbType.Decimal, 20, ParameterDirection.Output, true, 10, 2, "AMEX", DataRowVersion.Current, 0.00));
                cmd.Parameters.Add(new SqlParameter("@StoreName", SqlDbType.VarChar, 50, ParameterDirection.Output, true, 0, 0, "StoreName", DataRowVersion.Current, 0.00));
                cmd.Connection = new SqlConnection(GetConnectString());

                da.SelectCommand = cmd;
                da.Fill(ds);

                AddMissingDates(ds, DateTime.Parse(fromYYYYMMDD(BegDate)), DateTime.Parse(fromYYYYMMDD(EndDate)));

                gvListing.DataSource = ds;
                gvListing.DataBind();


                gvListing.FooterRow.Style.Value = "Font-weight:bold;text-align:right;";
                gvListing.FooterRow.Cells[0].Text = "Total";
                gvListing.FooterRow.Cells[1].Text = "$" + cmd.Parameters["@Visa"].Value.ToString();
                gvListing.FooterRow.Cells[2].Text = "$" + cmd.Parameters["@Mastercard"].Value.ToString();
                gvListing.FooterRow.Cells[3].Text = "$" + cmd.Parameters["@Discover"].Value.ToString();
                gvListing.FooterRow.Cells[4].Text = "$" + cmd.Parameters["@AMEX"].Value.ToString();
                lblStoreName.Text = cmd.Parameters["@StoreName"].Value.ToString();
            }
            catch (Exception ex)
            {
                Response.Write("Error Loading Page::" + ex.ToString());
            }
        }

    }

    private void AddMissingDates(DataSet ds, DateTime bDate, DateTime eDate)
    {
        DateTime targetDate = bDate;
        DateTime rowDate = DateTime.Now;
        int currentRow = 0;

        if (ds.Tables[0].Rows[0].ItemArray[0] != null)
        {
            rowDate = DateTime.Parse(fromYYYYMMDD(ds.Tables[0].Rows[0].ItemArray[0].ToString()));
        }

        while (targetDate.CompareTo(eDate) < 1)
        {
            DataRow newRow = ds.Tables[0].NewRow();
            newRow["Date"] = targetDate.ToString("MM/dd/yyyy");
            
            
            switch (Math.Sign(targetDate.CompareTo(rowDate)))
            {

                case -1:
                    ds.Tables[0].Rows.InsertAt(newRow, currentRow);
                    currentRow++;
                    targetDate = targetDate.AddDays(1);
                    break;
                case 0:
                    if (currentRow < ds.Tables[0].Rows.Count - 1)
                    {
                    rowDate = DateTime.Parse(fromYYYYMMDD(ds.Tables[0].Rows[currentRow + 1].ItemArray[0].ToString()));
                    } 
                    currentRow++;

                    targetDate = targetDate.AddDays(1);
                    break;
                case 1:
                    ds.Tables[0].Rows.InsertAt(newRow, currentRow);
                    //ds.Tables[0].Rows[currentRow].ItemArray["Date"] = targetDate.ToShortDateString();
                    ds.Tables[0].Rows[currentRow].AcceptChanges();
                    currentRow++;
                    targetDate = targetDate.AddDays(1);
                    break;
                default:
                    break;
            }
        }
        gvListing.DataBind();

    }

    private string ToYYYYMMDD(string MDY)
    {
        string[] d = MDY.Split('/');
        return d[2].PadLeft(4, '0') + d[0].PadLeft(2, '0') + d[1].PadLeft(2, '0');
    }



    protected string fromYYYYMMDD(string YYYYMMDD)
    {
        return YYYYMMDD.Substring(4, 2) + "/" + YYYYMMDD.Substring(6, 2) + "/" + YYYYMMDD.Substring(0, 4);
    }

    private string GetConnectString()
    {
        DataAccess data1 = new DataAccess();
        return data1.UnMaskString(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Production"].ConnectionString, 1, 2, 3, 4, 5);
    }
}


