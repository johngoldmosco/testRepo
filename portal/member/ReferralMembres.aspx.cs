using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_member_ReferralMembres : System.Web.UI.Page
{
    ODBC clsOdbc = new ODBC();
    ClassOther objOther = new ClassOther();
    private const string ASCENDING = " ASC";
        
    private const string DESCENDING = " DESC";

    //**** Return Employee Data using DataView *****
    private DataView GetData(int intpageindex)
    {

        string strQuery = null;
        string StrSearch = "";
        DataSet ds = new DataSet();
        DataView dv = new DataView();
        int strpageSize = gvMembers.PageSize;
        int intStart = (intpageindex - 1) * strpageSize + 1;

        intStart = intStart - 1;
        gvMembers.PageIndex = intpageindex;

        StrSearch = Search();

        int count = clsOdbc.executeScalar_int("SELECT Count(1) FROM mlm_personal_details b inner join mlm_login a On a.userid = b.userid INNER join mlm_progress_count d On a.userid = d.userid AND a.Active = 1 and a.status = 1 and a.direct_id = " + Session["UserID"] + "  " + StrSearch + "");

        strQuery = "Select a.my_sponsar_id, b.UserName,DATE_FORMAT(a.created_on,'%d %b %y') as created_on,DATE_FORMAT(a.active_on,'%d %b %y') as active_on,d.total_direct_members, b.email as emailid, b.mobile_number,  d.l_members, d.r_members,d.direct_left, d.direct_right FROM mlm_personal_details b inner join mlm_login a On  a.userid = b.userid INNER join mlm_progress_count d On a.userid = d.userid AND a.Active = 1 and a.status = 1 and a.direct_id = " + Session["UserID"] + "  " + StrSearch + "  Order By a.created_on DESC Limit " + intStart + "," + strpageSize + "";

        double dblPageCount = Convert.ToDouble(Convert.ToDecimal(count) / Convert.ToDecimal(strpageSize));
        int pageCount = Convert.ToInt32(Math.Ceiling(dblPageCount));
        ViewState["pageCount"] = pageCount;
        this.PopulatePager(intpageindex);
        try
        {
            ds = clsOdbc.getDataSet(strQuery);

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
                lblError.Text="No Records Found!";
            }
            return dv;

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
            strsel = strsel + " AND a.my_sponsar_id='" + txtUserId.Text + "'";
        }
        if (txtUserName.Text != "")
        {
            strsel = strsel + " AND b.UserName like'%" + txtUserName.Text + "%'";
        }

        return strsel;
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

    protected void gvMembers_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = "" + (((((GridView)sender).PageIndex - 1) * ((GridView)sender).PageSize) + (e.Row.RowIndex + 1));
        }
    }
    protected void Page_Changed(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        gvMembers.DataSource = GetData(pageIndex);
        gvMembers.DataBind();
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

    protected void gvMembers_Sorting(object sender, System.Web.UI.WebControls.GridViewSortEventArgs e)
    {
        ViewState["sortExp"] = e.SortExpression;
        gvMembers.DataSource = GetData(gvMembers.PageIndex);
        gvMembers.DataBind();

    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        // Verifies that the control is rendered 
    }

    protected void lnkbtnExportExcel_Click(object sender, System.EventArgs e)
    {
        CommonFunctions.exportFile("report_active_referral_memeber.xls", ".xls", gvMembers, pnllead);
    }
    protected void lnkbtnGenerate_Click(object sender, EventArgs e)
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