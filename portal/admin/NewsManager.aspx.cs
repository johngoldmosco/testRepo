﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_admin_NewsManager : System.Web.UI.Page
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

        if (txtStartDate.Text != "" && txtEndDate.Text != "")
        {
            strsel = strsel + " AND DATE(created_on) BETWEEN '" + Convert.ToString(txtStartDate.Text) + "' AND '" + Convert.ToString(txtEndDate.Text) + "' ";
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

        int count = clsOdbc.executeScalar_int("SELECT COUNT(1) FROM tbl_news WHERE 1 " + StrSearch + "");

        strQuery = "SELECT id,news_title, news_desc, CASE status WHEN 0 THEN 'Hide' WHEN 1 THEN 'SHOW' END AS Status, DATE_FORMAT(created_on, '%Y-%b-%d') AS CreatedOn FROM tbl_news WHERE 1 " + StrSearch + " Limit " + intStart + "," + strpageSize + "";

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
        Response.Redirect("NewsManager.aspx");
    }
    protected void lnkbtnExportExcel_Click(object sender, EventArgs e)
    {
        CommonFunctions.exportFile("NewsManager.xls", ".xls", gvReport, pnllead);
    }
    protected void lnkPrint_Click(object sender, EventArgs e)
    {
        gvReport.AllowPaging = false;
        DataSet ds = new DataSet();
        try
        {
            string strQuery = "SELECT id,news_title, news_desc, status, DATE_FORMAT(created_on, '%Y-%b-%d') AS CreatedOn FROM tbl_news Order By created_on DESC";
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
        Response.Redirect("NewsManager.aspx");
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
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = "" + (((((GridView)sender).PageIndex - 1) * ((GridView)sender).PageSize) + (e.Row.RowIndex + 1));
        }
        if (e.Row.RowType == DataControlRowType.DataRow | e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[3].Visible = false;
        }

        foreach (GridViewRow rw in gvReport.Rows)
        {
            HyperLink lnkActivate = default(HyperLink);

            string strUserID = null;
            strUserID = rw.Cells[3].Text;

            lnkActivate = (HyperLink)rw.FindControl("lnkUpdate");

            string lbltest = "NID=" + strUserID;
            //    lnkActivate.NavigateUrl = "EditProfile.aspx?UID=" + strUserID;

            string encryptedID = Encryption.ConvertStringToHex(lbltest, System.Text.Encoding.Unicode);

            lnkActivate.NavigateUrl = "EditNews.aspx?" + encryptedID;
        }
    }
    protected void lnkbtnGenerateReport_Click(object sender, EventArgs e)
    {
        gvReport.DataSource = GetData(1);
        gvReport.DataBind();
    }
    protected void lnkActive_Click(object sender, System.EventArgs e)
    {
        clsWallet objWallet = new clsWallet();
        DateTime dtcurDateTime = objWallet.getTime("India Standard Time");
        string active_date = dtcurDateTime.ToString("yyyy-MM-dd HH:mm:ss");

        // To User Name from Gridview

        int i = 0;

        if (gvReport.Rows.Count == 0)
        {
            lblError.Text = "* No records to Actve.";
            return;
        }

        foreach (GridViewRow rw in gvReport.Rows)
        {
            string vid = null;
            vid = rw.Cells[3].Text;

            CheckBox chkSel = default(CheckBox);
            chkSel = (CheckBox)rw.Cells[1].FindControl("chkSel");

            if (chkSel.Checked == true)
            {
                int intCurStatus = clsOdbc.executeScalar_int("SELECT status FROM tbl_news WHERE id=" + vid);

                if (intCurStatus != 1)
                {
                    clsOdbc.executeNonQuery("UPDATE tbl_news SET status=1 WHERE id=" + vid + "");
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
            gvReport.DataSource = GetData(gvReport.PageIndex);
            gvReport.DataBind();
        }
    }

    protected void lnkDisable_Click(object sender, System.EventArgs e)
    {
        clsWallet objWallet = new clsWallet();
        DateTime dtcurDateTime = objWallet.getTime("India Standard Time");
        string suspend_date = dtcurDateTime.ToString("yyyy-MM-dd HH:mm:ss");

        // To User Name from Gridview

        int i = 0;

        if (gvReport.Rows.Count == 0)
        {
            lblError.Text = "* No records to Suspend.";
            return;
        }

        foreach (GridViewRow rw in gvReport.Rows)
        {
            string vid = null;
            vid = rw.Cells[3].Text;

            CheckBox chkSel = default(CheckBox);
            chkSel = (CheckBox)rw.Cells[1].FindControl("chkSel");

            if (chkSel.Checked == true)
            {
                int intCurStatus = clsOdbc.executeScalar_int("SELECT status FROM tbl_news WHERE id=" + vid);
                if (intCurStatus != 0)
                {
                    clsOdbc.executeNonQuery("UPDATE tbl_news SET status=0 WHERE id=" + vid + "");
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
            gvReport.DataSource = GetData(gvReport.PageIndex);
            gvReport.DataBind();
        }
    }
}