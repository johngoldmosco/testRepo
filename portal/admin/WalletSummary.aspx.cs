using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_admin_WalletSummary : System.Web.UI.Page
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

        int count = clsOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_my_balance_current a, mlm_login b, mlm_personal_details c WHERE a.userid=b.userid AND a.userid=c.userid");

        strQuery = "SELECT a.id, a.userid, b.my_sponsar_id, c.username, a.wallet1, a.wallet2, a.wallet3, a.balance_amount FROM mlm_my_balance_current a, mlm_login b, mlm_personal_details c WHERE a.userid=b.userid AND a.userid=c.userid" + StrSearch + " Order By a.id DESC LIMIT " + intStart + "," + strpageSize + "";

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
                lblError.Text = "No Records Found!";
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
            strsel = strsel + " AND b.my_sponsar_id='" + txtUserId.Text + "'";
        }
        if (txtUserName.Text != "")
        {
            strsel = strsel + " AND c.username like'%" + txtUserName.Text + "%'";
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
        if (Session["AdminID"] == null)
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
        if (e.Row.RowType == DataControlRowType.DataRow | e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Visible = true;
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
        CommonFunctions.exportFile("WalletSummary.xls", ".xls", gvMembers, pnllead);
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
        Response.Redirect("WalletSummary.aspx");
    }
    protected void lnkPrint_Click(object sender, EventArgs e)
    {
        gvMembers.AllowPaging = false;

        DataSet ds = new DataSet();

        try
        {
            string strQuery = "SELECT a.userid, b.my_sponsar_id, c.username,c.pancard, a.pan_card, c.aadharcard, a.aadhar_card, a.photo, a.cheque, a.kyc_status, DATE_FORMAT(a.kyc_on,'%d-%b-%Y') AS KYCon FROM mlm_kyc_documents a INNER JOIN mlm_login b ON a.userid=b.userid INNER JOIN mlm_personal_details c ON a.userid=c.userid WHERE a.kyc_status=0  Order By a.kyc_on DESC";
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
    protected void lnkbtnRefresh_Click(object sender, EventArgs e)
    {
        Response.Redirect("WalletSummary.aspx");
    }
    protected void lnkApproveKYC_Click(object sender, EventArgs e)
    {
        clsWallet objWallet = new clsWallet();
        DateTime dtcurDateTime = objWallet.getTime("India Standard Time");
        string active_date = dtcurDateTime.ToString("yyyy-MM-dd HH:mm:ss");

        // To User Name from Gridview
        int i = 0;

        if (gvMembers.Rows.Count == 0)
        {
            lblError.Text = "* No records to Approve.";
            return;
        }

        foreach (GridViewRow rw in gvMembers.Rows)
        {
            string vid = null;
            vid = rw.Cells[1].Text;

            CheckBox chkSel = default(CheckBox);
            chkSel = (CheckBox)rw.Cells[2].FindControl("chkSel");

            if (chkSel.Checked == true)
            {
                int intCurStatus = clsOdbc.executeScalar_int("SELECT kyc_status FROM mlm_kyc_documents WHERE userid=" + vid);

                if (intCurStatus != 1)
                {
                    clsOdbc.executeNonQuery("UPDATE mlm_kyc_documents SET kyc_status=1 where userid =" + vid + "");
                }

                i = i + 1;
            }
        }

        if (i == 0)
        {
            lblError.Text = " * Kindly select a record.";
        }
        else
        {
            gvMembers.DataSource = GetData(gvMembers.PageIndex);
            gvMembers.DataBind();
        }
    }
    protected void lnkRejectKYC_Click(object sender, EventArgs e)
    {
        clsWallet objWallet = new clsWallet();
        DateTime dtcurDateTime = objWallet.getTime("India Standard Time");
        string active_date = dtcurDateTime.ToString("yyyy-MM-dd HH:mm:ss");

        // To User Name from Gridview
        int i = 0;

        if (gvMembers.Rows.Count == 0)
        {
            lblError.Text = "* No records to Approve.";
            return;
        }

        foreach (GridViewRow rw in gvMembers.Rows)
        {
            string vid = null;
            vid = rw.Cells[1].Text;

            CheckBox chkSel = default(CheckBox);
            chkSel = (CheckBox)rw.Cells[2].FindControl("chkSel");

            if (chkSel.Checked == true)
            {
                int intCurStatus = clsOdbc.executeScalar_int("SELECT kyc_status FROM mlm_kyc_documents WHERE userid=" + vid);

                if (intCurStatus != 2)
                {
                    clsOdbc.executeNonQuery("UPDATE mlm_kyc_documents SET kyc_status=2 where userid =" + vid + "");
                }

                i = i + 1;
            }
        }

        if (i == 0)
        {
            lblError.Text = " * Kindly select a record.";
        }
        else
        {
            gvMembers.DataSource = GetData(gvMembers.PageIndex);
            gvMembers.DataBind();
        }
    }
}