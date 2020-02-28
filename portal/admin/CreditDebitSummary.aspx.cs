using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class portal_admin_CreditDebitSummary : System.Web.UI.Page
{
    ODBC clsOdbc = new ODBC();
    ClassOther objOther = new ClassOther();
    private const string ASCENDING = " ASC";

    private const string DESCENDING = " DESC";
    int count;

    public string search_click()
    {
        count = 0;
        string strsel = "";
        if (txtName.Text != "")
        {
            strsel = strsel + " AND c.username LIKE '%" + Convert.ToString(txtName.Text) + "%' ";
        }

        if (txtUserID.Text != "")
        {
            strsel = strsel + " AND b.my_sponsar_id='" + Convert.ToString(txtUserID.Text) + "' ";
        }      

        return strsel;
    }

    //**** Return Employee Data using DataView *****
    private DataView GetData(int intpageindex)
    {

        string strQuery = null;
        DataSet ds = new DataSet();
        DataView dv = new DataView();
        int strpageSize = gvMembers.PageSize;
        int intStart = (intpageindex - 1) * strpageSize + 1;

        intStart = intStart - 1;
        gvMembers.PageIndex = intpageindex;

        string strSel = search_click();
        int count = clsOdbc.executeScalar_int("SELECT Count(1) From mlm_add_deduct_wallet a INNER JOIN mlm_login b ON a.userid=b.userid INNER JOIN mlm_personal_details c ON a.userid=c.userid " + strSel + "");

        strQuery = "SELECT a.id, a.userid, b.my_sponsar_id,c.username, a.transaction_type, a.wallet_type, a.amount, DATE_FORMAT(a.created_on,'%d %b %y %H:%i') as created_on From mlm_add_deduct_wallet a INNER JOIN mlm_login b ON a.userid=b.userid INNER JOIN mlm_personal_details c ON a.userid=c.userid " + strSel + " Order By a.created_on DESC" + " LIMIT " + intStart + "," + strpageSize + "";

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
               lblError.Text ="Sorry, No Records Found!";
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


    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (Session["AdminID"] == null)
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

        if (e.Row.RowType == DataControlRowType.DataRow | e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = true;
        }

        if (e.Row.Cells[4].Text == "1")
        {
            e.Row.Cells[4].Text = "Add";
        }
        else if (e.Row.Cells[4].Text == "2")
        {
            e.Row.Cells[4].Text = "Deduct";
        }
        if (e.Row.Cells[5].Text == "1")
        {
            e.Row.Cells[5].Text = "E Wallet";
        }
        else if (e.Row.Cells[5].Text == "2")
        {
            e.Row.Cells[5].Text = "Cashback Wallet";
        }
        if (e.Row.Cells[5].Text == "3")
        {
            e.Row.Cells[5].Text = "Repurchase Wallet";
        }
    }
    protected void lnkbtnGenerateReport_Click(object sender, System.EventArgs e)
    {
        gvMembers.DataSource = GetData(1);
        gvMembers.DataBind();
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

    public string process(object value1)
    {
        if (Convert.ToBoolean(value1) == true)
        {
            return "paginate_button";
        }
        else
        {
            return "paginate_button active";
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

}