using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_franchise_RepurchaseInvoice : System.Web.UI.Page
{
    ODBC objOdbc = new ODBC();
    private const string ASCENDING = " ASC";

    private const string DESCENDING = " DESC";
    int count;

    string intUserID;
    string intOrderID;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            intOrderID = Request.QueryString["ID"];

            FillDetails();
        }
    }

    private void FillDetails()
    {

        gvUsers.DataSource = GetData(1);
        gvUsers.DataBind();

    }

    private DataView GetData(int intpageindex)
    {
        string strQuery = null;
        DataSet ds = new DataSet();
        DataView dv = new DataView();
        int strpageSize = gvUsers.PageSize;
        int intStart = (intpageindex - 1) * strpageSize + 1;

        intStart = intStart - 1;
        gvUsers.PageIndex = intpageindex;

        count = objOdbc.executeScalar_int("SELECT COUNT(1) FROM `mlm_repurchase` a INNER JOIN mlm_order_product b ON a.id=b.order_id INNER JOIN mlm_login c ON a.userid=c.userid INNER JOIN mlm_personal_details d ON a.userid=d.userid WHERE a.id=" + intOrderID + " ");

        strQuery = "SELECT a.userid, c.my_sponsar_id, d.username, b.total_price, b.total_bv, b.product_name,b.product_qty,b.price, b.product_bv, a.repurchase_on, a.transID, a.franchise_id, e.my_sponsar_id AS FID, a.gst  FROM `mlm_repurchase` a INNER JOIN mlm_order_product b ON a.id=b.order_id INNER JOIN mlm_login c ON a.userid=c.userid INNER JOIN mlm_personal_details d ON a.userid=d.userid INNER JOIN mlm_login e ON a.franchise_id=e.userid WHERE a.id=" + intOrderID + "";


        double dblPageCount = Convert.ToDouble(Convert.ToDecimal(count) / Convert.ToDecimal(strpageSize));
        int pageCount = Convert.ToInt32(Math.Ceiling(dblPageCount));
        ViewState["pageCount"] = pageCount + 1;
        this.PopulatePager(intpageindex);
        try
        {
            ds = objOdbc.getDataSet(strQuery);

            lblUserID.Text = ds.Tables[0].Rows[0][1].ToString();
            lblUserName.Text = ds.Tables[0].Rows[0][2].ToString();
            lblDate.Text = ds.Tables[0].Rows[0][9].ToString();
            lblOrderID.Text = ds.Tables[0].Rows[0][10].ToString() + "U" + ds.Tables[0].Rows[0][0] + "O" + intOrderID;
            lblTotalAmt.Text = ds.Tables[0].Rows[0][4].ToString();
            lblTotalAmt1.Text = ds.Tables[0].Rows[0][3].ToString();
     //       lblGst.Text = ds.Tables[0].Rows[0][13].ToString();

            if (ds.Tables[0].Rows.Count > 0)
            {
                if ((ViewState["sortExp"] != null))
                {
                    dv = new DataView(ds.Tables[0]);

                    if ((GridViewSortDirection == SortDirection.Ascending))
                    {
                        GridViewSortDirection = SortDirection.Descending;
                        dv.Sort = Convert.ToString(ViewState["sortExp"] + DESCENDING);
                    }
                    else
                    {
                        GridViewSortDirection = SortDirection.Ascending;
                        dv.Sort = Convert.ToString(ViewState["sortExp"] + ASCENDING);
                    }
                }
                else
                {
                    dv = ds.Tables[0].DefaultView;
                }

                return dv;
            }
            else
            {
                CommonMessages.ShowAlertMessage("Sorry, No Records Found!");
            }
            return dv;

        }
        catch (Exception ex)
        {
        }

        return dv;
    }

    //**** Property for Getting Employee Gridview Display Data Order. *****
    public SortDirection GridViewSortDirection
    {
        get
        {
            if (ViewState["sortDir"] == null)
            {
                ViewState["sortDir"] = SortDirection.Ascending;
            }

            return (SortDirection)ViewState["sortDir"];
        }

        set { ViewState["sortDir"] = value; }
    }


    protected void gvCampaign_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {

    }

    protected void gvUsers_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = "" + (((((GridView)sender).PageIndex - 1) * ((GridView)sender).PageSize) + (e.Row.RowIndex + 1));
        }

        if (e.Row.RowType == DataControlRowType.DataRow | e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[7].Visible = false;
        }

        //*** Set Visible = False for Asset ID  ****
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //if (e.Row.Cells[6].Text == "1")
            //{
            //    e.Row.Cells[6].Text = "Joining";
            //}
            //else if (e.Row.Cells[6].Text == "2")
            //{
            //    e.Row.Cells[6].Text = "Repurchase";
            //}
        }


        //*** Set Edit Form in Edit Link  ****

        //foreach (GridViewRow rw in gvUsers.Rows)
        //{
        //    HyperLink lnkActivate = default(HyperLink);

        //    string strOrderID = null;
        //    strOrderID = rw.Cells[0].Text;

        //    lnkActivate = (HyperLink)rw.FindControl("lnkUpdate");

        //    lnkActivate.NavigateUrl = "ProductDetails.aspx?ID=" + strOrderID;
        //}

    }



    protected void gvUsers_Sorting(object sender, System.Web.UI.WebControls.GridViewSortEventArgs e)
    {
        ViewState["sortExp"] = e.SortExpression;
        gvUsers.DataSource = GetData(gvUsers.PageIndex);
        gvUsers.DataBind();
    }

    protected void Page_Changed(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        gvUsers.DataSource = GetData(pageIndex);
        gvUsers.DataBind();
    }
    protected void PopulatePager(int pageIndex)
    {
        int ButtonCount = 10;
        System.Collections.Generic.List<ListItem> pages = new System.Collections.Generic.List<ListItem>();
        int pageCount = Int32.Parse(ViewState["pageCount"].ToString());
        if (pageCount < 1)
        {
            return;
        }

        int start = pageIndex - (pageIndex % ButtonCount);
        int end = pageIndex + (ButtonCount - (pageIndex % ButtonCount));
        if (start <= 0)
        {
            start = start + 1;
        }
        if (end > pageCount)
        {
            end = pageCount + 1;
        }

        if (start > (ButtonCount - 1))
        {
            pages.Add(new ListItem("---", (start - 1).ToString()));
        }
        int i = 0;
        for (i = start; i <= end - 1; i++)
        {
            pages.Add(new ListItem(i.ToString(), i.ToString(), i != pageIndex));
        }
        if (pageCount > end)
        {
            pages.Add(new ListItem("---", (i).ToString()));
        }
        rptPager.DataSource = pages;
        rptPager.DataBind();
    }

    public string process(object value1)
    {
        if (Convert.ToBoolean(value1) == true)
        {
            return "btn_box";
        }
        else
        {
            return "current_page";
        }
    }

    public string process1(object value1)
    {
        if (!Convert.ToBoolean(value1))
        {
            return "false";
        }
        else
        {
            return "";
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // Verifies that the control is rendered 
    }
}