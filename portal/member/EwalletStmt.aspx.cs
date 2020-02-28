using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_member_EwalletStmt : System.Web.UI.Page
{
    ODBC clsOdbc = new ODBC();
    ClassOther objOther = new ClassOther();
    private const string ASCENDING = " ASC";

    private const string DESCENDING = " DESC";

    protected string Search()
    {
        string strsel = string.Empty;
        if (txtUserId.Text != "")
        {
            strsel = strsel + " AND b.my_sponsar_id='" + txtUserId.Text + "'";
        }
        if (txtUserName.Text != "")
        {
            strsel = strsel + " AND c.username like'%" + txtUserName.Text + "%'";
        }

        return strsel;
    }
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

        int count = clsOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_transaction a INNER JOIN mlm_login b ON a.userid=b.userid INNER JOIN mlm_personal_details c ON a.userid=c.userid WHERE a.userid='"+ Session["UserID"]+ "' " + StrSearch + "");

        strQuery = "SELECT a.id, a.trans_number, a.userid, b.my_sponsar_id, c.username, a.debit_amount, a.credit_amount, a.tds, a.ser_charge, a.total_amt, a.closing_balance, DATE_FORMAT(a.trans_date,'%d-%b-%Y') AS TransDate, a.description FROM mlm_transaction a INNER JOIN mlm_login b ON a.userid=b.userid INNER JOIN mlm_personal_details c ON a.userid=c.userid WHERE a.userid='" + Session["UserID"] + "' " + StrSearch + "  ORDER BY a.id DESC Limit " + intStart + "," + strpageSize + "";

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
                //  CommonMessages.ShowAlertMessage("Sorry, No Records Found!");
            }
            return dv;

        }
        catch (Exception ex)
        {
        }
        return dv;

    }

    protected void lnkPrint_Click(object sender, EventArgs e)
    {
        gvMembers.AllowPaging = false;

        DataSet ds = new DataSet();

        try
        {
            string strQuery = "SELECT a.trans_number, a.userid, b.my_sponsar_id, c.username, a.debit_amount, a.credit_amount, a.tds, a.ser_charge, a.total_amt, a.closing_balance, DATE_FORMAT(a.trans_date,'%d-%b-%Y') AS TransDate, a.description FROM mlm_transaction a INNER JOIN mlm_login b ON a.userid=b.userid INNER JOIN mlm_personal_details c ON a.userid=c.userid WHERE a.userid='" + Session["UserID"] + "' ORDER BY a.trans_date DESC";
            ds = clsOdbc.getDataSet(strQuery);
            gvMembers.DataSource = ds;
            gvMembers.DataBind();
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

        gvMembers.RenderControl(hw);

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

        gvMembers.DataSource = GetData(gvMembers.PageIndex);
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
            Response.Redirect("../../Login.aspx");
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
        CommonFunctions.exportFile("EwalletStmt.xls", ".xls", gvMembers, pnllead);
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
    protected void lnkbtnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("EwalletStmt.aspx");
    }
  
    protected void lnkbtnRefresh_Click(object sender, EventArgs e)
    {
        Response.Redirect("EwalletStmt.aspx");
    }
}