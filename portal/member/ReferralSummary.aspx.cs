using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Data;

public partial class portal_member_ReferralSummary : System.Web.UI.Page
{
    form_func obj = new form_func();
    ODBC clsOdbc = new ODBC();
    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Response.Redirect("../../login.aspx");
        }
        if (!IsPostBack)
        {
            gvEpin.DataSource = GetData(1);
            gvEpin.DataBind();
        }

    }

    //**** Return Employee Data using DataView *****
    private System.Data.DataView GetData(int intpageindex)
    {
        string search = string.Empty;
        string StrSearch = string.Empty;
        string strQuery = null;
        System.Data.DataSet ds = new System.Data.DataSet();
        System.Data.DataView dv = new System.Data.DataView();

        int strpageSize = gvEpin.PageSize;
        int intStart = (intpageindex - 1) * strpageSize + 1;

        intStart = intStart - 1;
        gvEpin.PageIndex = intpageindex;

        int count = clsOdbc.executeScalar_int("SELECT Count(1) FROM mlm_direct_income a, mlm_personal_details c, mlm_login b where  a.child_id = b.userid and a.child_id  = c.userid and a.userid = '" + Session["UserID"] + "'  Order By a.userid DESC");

        strQuery = "SELECT b.my_sponsar_id, c.UserName,a.amt ,DATE_FORMAT(a.created_on,'%d %M %Y') As recieved_date, a.status FROM mlm_direct_income a, mlm_personal_details c,mlm_login b where a.child_id = b.userid and a.child_id = c.userid and a.userid = '" + Session["UserID"] + "'  Order By a.created_on DESC LIMIT " + intStart + "," + strpageSize + "";

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
                lblError.Text= "Sorry, No Records Found!" ;
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
        finally
        {
            ds.Dispose();
        }
        return dv;
    }


    protected void lnkbtnGenerate_Click(object sender, EventArgs e)
    {
        gvEpin.DataSource = GetData(1);
        gvEpin.DataBind();
    }
    protected void lnkbtnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("reports_all.aspx");
    }
    protected void lnkbtnExportExcel_Click(object sender, EventArgs e)
    {
        CommonFunctions.exportFile("report_direct_income.xls", ".xls", gvEpin, pnllead);
    }
    protected void lnkPrint_Click(object sender, EventArgs e)
    {
        gvEpin.AllowPaging = false;

        DataSet ds = new DataSet();

        try
        {

            string strQuery = "SELECT b.my_sponsar_id,c.UserName,a.amt ,DATE_FORMAT(a.created_on,'%d %M %Y') As recieved_date,a.status FROM mlm_direct_income a,mlm_personal_details c,mlm_login b where  a.child_id = b.userid and a.child_id  = c.userid and a.userid = '" + Session["UserID"] + "'  Order By a.created_on DESC";
            ds = clsOdbc.getDataSet(strQuery);
            gvEpin.DataSource = ds;
            gvEpin.DataBind();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            ds.Dispose();
        }


        StringWriter sw = new StringWriter();

        HtmlTextWriter hw = new HtmlTextWriter(sw);

        gvEpin.RenderControl(hw);

        string gridHTML = sw.ToString().Replace("\"", "'").Replace(System.Environment.NewLine, "");

        StringBuilder sb = new StringBuilder();

        sb.Append("<script type = 'text/javascript'>");

        sb.Append("window.onload = new function(){");

        sb.Append("var printWin = window.open('', '', 'left=0");

        sb.Append(",top=0,width=1000,height=600,status=0');");

        sb.Append("printWin.document.write(\"");

        sb.Append(gridHTML);

        sb.Append("\");");

        sb.Append("printWin.document.close();");

        sb.Append("printWin.focus();");

        sb.Append("printWin.print();");

        sb.Append("printWin.close();};");

        sb.Append("</script>");

        ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());

        gvEpin.DataSource = GetData(gvEpin.PageIndex);
        gvEpin.DataBind();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        // Verifies that the control is rendered 
    }

    protected void lnkbtnRefresh_Click(object sender, EventArgs e)
    {
        Response.Redirect("report_direct_income.aspx");
    }
    protected void gvEpin_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState["sortExp"] = e.SortExpression;
        gvEpin.DataSource = GetData(gvEpin.PageIndex);
        gvEpin.DataBind();

    }
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

    protected void PopulatePager(int pageIndex)
    {
        int ButtonCount = 10;
        System.Collections.Generic.List<ListItem> pages = new System.Collections.Generic.List<ListItem>();
        int pageCount = Int32.Parse(ViewState["pageCount"].ToString());


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
        gvEpin.DataSource = GetData(pageIndex);
        gvEpin.DataBind();
    }
    protected void gvEpin_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            if (e.Row.Cells[4].Text == "0")
            {
                e.Row.Cells[4].Text = "Unconfirmed";


            }
            else
            {
                e.Row.Cells[4].Text = "Confirmed";
            }
            e.Row.Cells[0].Text = "" + (((((GridView)sender).PageIndex - 1) * ((GridView)sender).PageSize) + (e.Row.RowIndex + 1));
        }
    }
}