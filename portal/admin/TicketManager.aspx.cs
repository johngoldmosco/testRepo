using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_admin_TicketManager : System.Web.UI.Page
{
    form_func obj = new form_func();
    ODBC clsOdbc = new ODBC();
    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["AdminID"] == null)
        {
            Response.Redirect("../../Login.aspx");
        }
        if (!IsPostBack)
        {
            gvReport.DataSource = GetData(1);
            gvReport.DataBind();
        }
    }

    protected string Search()
    {
        string strsel = string.Empty;
        if (txtUserID.Text != "")
        {
            strsel = strsel + " AND b.my_sponsar_id='" + txtUserID.Text + "'";
        }
        //if (txtUserName.Text != "")
        //{
        //    strsel = strsel + " AND c.username like'%" + txtUserName.Text + "%'";
        //}
        //if (txtStartDate.Text != "" && txtEndDate.Text != "")
        //{
        //    strsel = strsel + " AND DATE(a.request_date) BETWEEN '" + Convert.ToString(txtStartDate.Text) + "' AND '" + Convert.ToString(txtEndDate.Text) + "' ";
        //}
        if (txtTicketID.Text != "")
        {
            strsel = strsel + " AND a.ticket_id='" + txtTicketID.Text + "'";
        }
        if (txtUserID.Text != "")
        {
            strsel = strsel + " AND b.my_sponsar_id='" + txtUserID.Text + "'";
        }
        if (ddlPriority.SelectedValue != "0")
        {
            strsel = strsel + " AND a.priority ='" + ddlPriority.SelectedValue + "'";
        }

        if (txtStartDate.Text != "")   //&& txtEndDate.Text != ""
        {
            strsel = strsel + "  AND DATE(a.ticket_on)= '" + txtStartDate.Text + "'";
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
        int strpageSize = gvReport.PageSize;
        int intStart = (intpageindex - 1) * strpageSize + 1;

        intStart = intStart - 1;
        gvReport.PageIndex = intpageindex;

        StrSearch = Search();

        int count = clsOdbc.executeScalar_int("SELECT COUNT(1) FROM tbl_ticket a INNER JOIN mlm_login b ON a.userid=b.userid WHERE 1 " + StrSearch + "");

        strQuery = "SELECT a.id, a.userid, b.my_sponsar_id,a.ticket_id, CONCAT('#',a.ticket_id) AS TicketID, CONCAT(SUBSTR(a.subject,1,20) ,'...') AS SUBJECT  , CASE a.department WHEN 1 THEN 'Support' WHEN 2 THEN 'Admin' END AS Department, CASE a.priority WHEN 1 THEN 'High' WHEN 2 THEN 'Medium' WHEN 3 THEN 'Low' END AS Priority , a.message, a.attachment, DATE_FORMAT(a.ticket_on, '%Y-%b-%d') AS TicketOn, CASE a.status WHEN 0 THEN 'Closed' WHEN 1 THEN 'Open' WHEN 2 THEN 'Answerd' WHEN 3 THEN 'Hold' WHEN 4 THEN 'Customer Reply' END AS Status  FROM tbl_ticket a INNER JOIN mlm_login b ON a.userid=b.userid WHERE 1 " + StrSearch + " ORDER by a.ticket_on DESC LIMIT " + intStart + ", " + strpageSize + "";

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
                lblError.Text = "Sorry, No Records Found!";
            }
            return dv;

        }
        catch (Exception ex)
        {
            CommonMessages.ShowAlertMessage(ex.Message);
        }
        return dv;
    }


    protected void lnkbtnGenerate_Click(object sender, EventArgs e)
    {
        gvReport.DataSource = GetData(1);
        gvReport.DataBind();
    }
    protected void lnkbtnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TicketManager.aspx");
    }
    protected void lnkbtnExportExcel_Click(object sender, EventArgs e)
    {
        CommonFunctions.exportFile("rptTicketManager.xls", ".xls", gvReport, pnllead);
    }
    protected void lnkPrint_Click(object sender, EventArgs e)
    {
        gvReport.AllowPaging = false;
        DataSet ds = new DataSet();
        try
        {
            string strQuery = "SELECT a.id, a.userid, b.my_sponsar_id, a.wallet1, a.request_amount, a.amount_given, c.bank_name, c.account_number, c.ifsc_code,  DATE_FORMAT(a.request_date,'%Y-%m-%d') AS RequestDate, CASE a.status WHEN 0 THEN 'Pending' WHEN 1 THEN 'Success' WHEN 2 THEN 'Reject' END AS WStatus FROM tbl_request a LEFT JOIN mlm_login b ON a.userid=b.userid LEFT JOIN tbl_bank_account_details c ON a.userid=c.userid WHERE a.status!= 0 Order By a.request_date DESC";
            ds = clsOdbc.getDataSet(strQuery);
            gvReport.DataSource = ds;
            gvReport.DataBind();
        }
        catch (Exception ex)
        {
            CommonMessages.ShowAlertMessage(ex.Message);
        }
        finally
        {
            ds.Dispose();
        }

        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);

        gvReport.RenderControl(hw);

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

        gvReport.DataSource = GetData(gvReport.PageIndex);
        gvReport.DataBind();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        // Verifies that the control is rendered 
    }

    protected void lnkbtnRefresh_Click(object sender, EventArgs e)
    {
        Response.Redirect("TicketManager.aspx");
    }
    protected void gvReport_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState["sortExp"] = e.SortExpression;
        gvReport.DataSource = GetData(gvReport.PageIndex);
        gvReport.DataBind();

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
        gvReport.DataSource = GetData(pageIndex);
        gvReport.DataBind();
    }
    protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType.Equals(DataControlRowType.DataRow))
        {
            e.Row.Cells[2].Text = "" + (((((GridView)sender).PageIndex - 1) * ((GridView)sender).PageSize) + (e.Row.RowIndex + 1));

        }
        e.Row.Cells[0].Visible = false;
        foreach (GridViewRow rw in gvReport.Rows)
        {
            HyperLink lnkActivate = default(HyperLink);

            string strtitle = null;
            strtitle = rw.Cells[0].Text;
            lnkActivate = (HyperLink)rw.FindControl("hlnktitle");

            lnkActivate.NavigateUrl = "ViewTicket.aspx?VID=" + strtitle;

        }
    }
    protected void lnkbtnGenerateReport_Click(object sender, EventArgs e)
    {
        gvReport.DataSource = GetData(1);
        gvReport.DataBind();
    }
}