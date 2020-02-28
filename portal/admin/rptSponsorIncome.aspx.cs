﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_admin_rptSponsorIncome : System.Web.UI.Page
{
    form_func obj = new form_func();
    ODBC clsOdbc = new ODBC();
    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminID"] == null)
        {
            Response.Redirect("../../login.aspx");
        }
        if (!IsPostBack)
        {
            gvBinaryIncome.DataSource = GetData(1);
            gvBinaryIncome.DataBind();
        }
    }

    protected string Search()
    {
        string strsel = string.Empty;
        //if (txtUserID.Text != "")
        //{
        //    strsel = strsel + " AND b.my_sponsar_id='" + txtUserID.Text + "'";
        //}
        //if (txtUserName.Text != "")
        //{
        //    strsel = strsel + " AND d.UserName like'%" + txtUserName.Text + "%'";
        //}

        if (txtStartDate.Text != "" && txtEndDate.Text != "")
        {
            strsel = strsel + " AND DATE(a.created_on) BETWEEN '" + txtStartDate.Text + "' AND '" + txtEndDate.Text + "'";
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
        int strpageSize = gvBinaryIncome.PageSize;
        int intStart = (intpageindex - 1) * strpageSize + 1;

        intStart = intStart - 1;
        gvBinaryIncome.PageIndex = intpageindex;

        StrSearch = Search();

        int count = clsOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_sponsor_income a INNER JOIN mlm_login b ON a.userid=b.userid INNER JOIN mlm_personal_details c ON a.userid=c.userid INNER JOIN mlm_login d ON a.child_id=d.userid INNER JOIN mlm_personal_details e ON a.child_id=e.userid  WHERE 1 " + StrSearch + " ");

        strQuery = "SELECT a.child_id, d.my_sponsar_id AS CID,e.username AS CNAME, a.userid, b.my_sponsar_id AS UID,c.username AS UNAME, a.total_amount, a.percent, a.sponsor_income, a.tds, a.service_charge, a.net_amount, a._level, a.created_on FROM mlm_sponsor_income a INNER JOIN mlm_login b ON a.userid=b.userid INNER JOIN mlm_personal_details c ON a.userid=c.userid INNER JOIN mlm_login d ON a.child_id=d.userid INNER JOIN mlm_personal_details e ON a.child_id=e.userid WHERE 1 " + StrSearch + " Order By a.created_on DESC";

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
                //          lblError.Text = "Sorry, No Records Found!";
            }
            return dv;

        }
        catch (Exception ex)
        {
        }

        return dv;
    }


    protected void lnkbtnGenerate_Click(object sender, EventArgs e)
    {
        gvBinaryIncome.DataSource = GetData(1);
        gvBinaryIncome.DataBind();
    }
    protected void lnkbtnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("reports.aspx");
    }
    protected void lnkbtnExportExcel_Click(object sender, EventArgs e)
    {
        CommonFunctions.exportFile("rptSponsorIncome.xls", ".xls", gvBinaryIncome, pnllead);
    }
    protected void lnkPrint_Click(object sender, EventArgs e)
    {
        gvBinaryIncome.AllowPaging = false;

        DataSet ds = new DataSet();

        try
        {
            string strQuery = "SELECT a.child_id, d.my_sponsar_id AS CID,e.username AS CNAME, a.userid, b.my_sponsar_id AS UID,c.username AS UNAME, a.total_amount, a.percent, a.sponsor_income, a.tds, a.service_charge, a.net_amount, a._level, a.created_on FROM mlm_sponsor_income a INNER JOIN mlm_login b ON a.userid=b.userid INNER JOIN mlm_personal_details c ON a.userid=c.userid INNER JOIN mlm_login d ON a.child_id=d.userid INNER JOIN mlm_personal_details e ON a.child_id=e.userid WHERE 1 Order By a.created_on DESC";
            ds = clsOdbc.getDataSet(strQuery);
            gvBinaryIncome.DataSource = ds;
            gvBinaryIncome.DataBind();
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

        gvBinaryIncome.RenderControl(hw);

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

        gvBinaryIncome.DataSource = GetData(gvBinaryIncome.PageIndex);
        gvBinaryIncome.DataBind();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        // Verifies that the control is rendered 
    }

    protected void lnkbtnRefresh_Click(object sender, EventArgs e)
    {
        Response.Redirect("rptSponsorIncome.aspx");
    }
    protected void gvBinaryIncome_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState["sortExp"] = e.SortExpression;
        gvBinaryIncome.DataSource = GetData(gvBinaryIncome.PageIndex);
        gvBinaryIncome.DataBind();

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
        gvBinaryIncome.DataSource = GetData(pageIndex);
        gvBinaryIncome.DataBind();
    }
    protected void gvBinaryIncome_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = "" + (((((GridView)sender).PageIndex - 1) * ((GridView)sender).PageSize) + (e.Row.RowIndex + 1));
        }
    }
    protected void lnkbtnGenerateReport_Click(object sender, EventArgs e)
    {
        gvBinaryIncome.DataSource = GetData(1);
        gvBinaryIncome.DataBind();
    }
}