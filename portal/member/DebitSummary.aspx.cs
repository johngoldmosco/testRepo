using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_member_DebitSummary : System.Web.UI.Page
{
    ODBC clsOdbc = new ODBC();
    private const string ASCENDING = " ASC";

    private const string DESCENDING = " DESC";
    //**** Return Employee Data using DataView *****
    private System.Data.DataView GetData(int intpageindex)
    {

        string strQuery = null;
        System.Data.DataSet ds = new System.Data.DataSet();
        System.Data.DataView dv = new System.Data.DataView();

        int strpageSize = gvMembers.PageSize;
        int intStart = (intpageindex - 1) * strpageSize + 1;

        intStart = intStart - 1;
        gvMembers.PageIndex = intpageindex;
        string strsearch = "";
        int count = clsOdbc.executeScalar_int("SELECT Count(1) From mlm_transaction a WHERE a.userid='" + Session["UserID"] + "' " + strsearch + " AND a.credit_amount > 0");

        strQuery = "SELECT DATE_FORMAT(a.trans_date,'%d-%m-%Y') as trans_date ,a.description,a.debit_amount,a.credit_amount,a.closing_balance FROM mlm_transaction a WHERE a.userid =" + Session["UserID"] + " AND a.credit_amount > 0 Order By a.id  LIMIT " + intStart + "," + strpageSize + "";

        double dblPageCount = (double)((decimal)count / Convert.ToDecimal(strpageSize));
        int pageCount = (int)Math.Ceiling(dblPageCount);
        ViewState["pageCount"] = pageCount;
        this.PopulatePager(intpageindex);
        try
        {
            ds = clsOdbc.getDataSet(strQuery);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if ((ViewState["sortExp"] != null))
                {
                    dv = new System.Data.DataView(ds.Tables[0]);

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
                lblError.Text = "Sorry, No Records Found!";
            }
        }
        catch (Exception ex)
        {
        }
        return dv;

    }

    protected void gvMembers_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState["sortExp"] = e.SortExpression;
        gvMembers.DataSource = GetData(gvMembers.PageIndex);
        gvMembers.DataBind();

    }

    protected void PopulatePager(int pageIndex)
    {
        int ButtonCount = 10;
        System.Collections.Generic.List<ListItem> pages = new System.Collections.Generic.List<ListItem>();
        int pageCount = Int32.Parse(ViewState["pageCount"].ToString());
        if (pageCount < 1)
            return;

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
        for (i = start; i < end; i++)
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

    protected void Page_Changed(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        gvMembers.DataSource = GetData(pageIndex);
        gvMembers.DataBind();
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




    protected void Page_Load(object sender, System.EventArgs e)
    {

        if (Session["UserID"] == null)
        {
            Response.Redirect("../../login.aspx");
        }

        if (!IsPostBack)
        {
            gvMembers.DataSource = GetData(1);
            gvMembers.DataBind();
        }

    }

    protected void gvUsers_Sorting(object sender, System.Web.UI.WebControls.GridViewSortEventArgs e)
    {
        ViewState["sortExp"] = e.SortExpression;
        gvMembers.DataSource = GetData(gvMembers.PageIndex);
        gvMembers.DataBind();

    }

    protected void gvMembers_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        if (e.Row.RowType.Equals(DataControlRowType.DataRow))
        {
            e.Row.Cells[0].Text = "" + (((((GridView)sender).PageIndex - 1) * ((GridView)sender).PageSize) + (e.Row.RowIndex + 1));
        }
    }
}
