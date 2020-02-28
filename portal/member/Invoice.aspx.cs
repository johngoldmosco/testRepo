using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_member_Invoice : System.Web.UI.Page
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
            intUserID = Request.QueryString["UID"];
            intOrderID = Request.QueryString["OID"];

            FillDetails(intUserID);
        }
    }

    private void FillDetails(string intUserID1)
    {
        lblUserID.Text = objOdbc.executeScalar_str("SELECT my_sponsar_id FROM mlm_login WHERE userid='" + intUserID1 + "'");
        lblUserName.Text = objOdbc.executeScalar_str("SELECT username FROM mlm_personal_details WHERE userid = '" + intUserID1 + "'");
        lblDate.Text = DateTime.Now.ToShortDateString();

       
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

        count = objOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_topup a INNER JOIN mlm_login b ON a.userid=b.userid INNER JOIN mlm_epin_type c ON a.package_id= c.id INNER JOIN mlm_login d ON a.topup_by= d.userid WHERE a.id=" + intOrderID + " ");

        strQuery = "SELECT a.id, b.my_sponsar_id AS UserID, a.epin,a.topup_amount,a.package_id, c.pin_type, a.topup_by, d.my_sponsar_id AS Topupby, DATE_FORMAT(a.created_on,'%d-%b-%Y') AS TopupOn, a.trans_id, c.gst, a.topup_amount  AS topupamount, a.topup_amount * (c.gst/100) AS GSTamt   FROM mlm_topup a INNER JOIN mlm_login b ON a.userid=b.userid INNER JOIN mlm_epin_type c ON a.package_id= c.id INNER JOIN mlm_login d ON a.topup_by= d.userid WHERE a.id=" + intOrderID + "";    

        double dblPageCount = Convert.ToDouble(Convert.ToDecimal(count) / Convert.ToDecimal(strpageSize));
        int pageCount = Convert.ToInt32(Math.Ceiling(dblPageCount));
        ViewState["pageCount"] = pageCount + 1;
        this.PopulatePager(intpageindex);
        try
        {
            ds = objOdbc.getDataSet(strQuery);

            //  DateTime today = DateTime.Now;

          //  lbldateOf.Text = Convert.ToDateTime(ds.Tables[0].Rows[0][9]).AddDays(365).ToString(); 

            lblOrderID.Text = ds.Tables[0].Rows[0][1].ToString() + "U" + Request.QueryString["UID"].ToString() + "O" + Request.QueryString["OID"].ToString();
            lblTotalAmt.Text = ds.Tables[0].Rows[0][11].ToString();
            lblTotalAmt1.Text = ds.Tables[0].Rows[0][3].ToString();
            lblGst.Text = ds.Tables[0].Rows[0][12].ToString();

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