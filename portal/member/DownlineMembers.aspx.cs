using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_member_DownlineMembers : System.Web.UI.Page
{
    ODBC clsOdbc = new ODBC();
    private const string ASCENDING = " ASC";

    private const string DESCENDING = " DESC";
    //**** Return Employee Data using DataView *****
    private System.Data.DataView GetData(int intpageindex)
    {

        string strUsers = string.Empty;
        string search = string.Empty;
        string StrSearch = string.Empty;
        string strQuery = null;

        System.Data.DataSet ds = new System.Data.DataSet();
        System.Data.DataView dv = new System.Data.DataView();

        int strpageSize = gvMembers.PageSize;
        int intStart = (intpageindex - 1) * strpageSize + 1;

        intStart = intStart - 1;
        gvMembers.PageIndex = intpageindex;

        StrSearch = Search();

        int intCountUser = clsOdbc.executeScalar_int("Select Count(1) From mlm_level_log where userid = " + Session["UserID"]);
        if (intCountUser > 0)
        {
            strUsers = clsOdbc.executeScalar_str("CALL get_downline_member(" + Session["UserID"] +", 1)");
			strUsers = strUsers.TrimEnd(',');
        }

        int count = clsOdbc.executeScalar_int("SELECT Count(1) FROM mlm_personal_details a inner join mlm_login b On  a.userid = b.userid INNER JOIN mlm_progress_count d On a.userid = d.userid  AND b.Active = 1 and b.status = 1 and b.status = 1 and find_in_set(b.userid,'" + strUsers + "') " + StrSearch + "");

        strQuery = "Select b.my_sponsar_id,a.UserName, b.created_on as created_on, b.active_on as active_on, d.total_down_members, d.total_direct_members, concat('+',a.mobile_code,' ',a.mobile_number) as mobile_number, a.email, d.l_members, d.r_members,d.direct_left, d.direct_right FROM mlm_personal_details a inner join mlm_login b On  a.userid = b.userid INNER JOIN mlm_progress_count d On a.userid = d.userid  AND b.Active = 1 and b.status = 1 and b.status = 1 and find_in_set(b.userid,'" + strUsers + "') " + StrSearch + " Order By b.created_on DESC Limit " + intStart + "," + strpageSize + "";

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
               lblError.Text="No Records Found!";
            }
        }
        catch (Exception ex)
        {
        }
        return dv;

    }

    protected string Search()
    {
        string strsel = string.Empty;
        if (txtUserId.Text != "")
        {
            strsel = strsel + " AND b.my_sponsar_id='" + txtUserId.Text + "'";
        }
        if (txtUserName.Text != "")
        {
            strsel = strsel + " AND a.UserName like'%" + txtUserName.Text + "%'";
        }

        return strsel;
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

    protected void lnkbtnGenerateReport_Click(object sender, EventArgs e)
    {
        gvMembers.DataSource = GetData(1);
        gvMembers.DataBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        gvMembers.DataSource = GetData(1);
        gvMembers.DataBind();
    }
}