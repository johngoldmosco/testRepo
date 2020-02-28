using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.IO;

public partial class portal_admin_InactiveUsers : System.Web.UI.Page
{
    ODBC clsOdbc = new ODBC();
    private const string ASCENDING = " ASC";

    private const string DESCENDING = " DESC";
    int count;
    public string search_click()
    {
        count = 0;
        string strsel = "";
        if (txtName.Text != "")
        {
            strsel = strsel + " AND a.username LIKE '%" + Convert.ToString(txtName.Text) + "%' ";
        }

        if (txtUserID.Text != "")
        {
            strsel = " AND c.my_sponsar_id='" + Convert.ToString(txtUserID.Text) + "' ";
        }

        return strsel;
    }


    protected void gvCampaign_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        if (e.CommandName == "APPROVE")
        {
            LinkButton lnkView = (LinkButton)e.CommandSource;
            string strUserid = lnkView.CommandArgument;

            Session["TopupID"] = strUserid;
            Response.Redirect("ActivateMember.aspx");

        }
    }

    //**** Return Employee Data using DataView *****
    private DataView GetData(int intpageindex)
    {

        string strQuery = null;
        DataSet ds = new DataSet();
        DataView dv = new DataView();
        int strpageSize = gvUsers.PageSize;
        int intStart = (intpageindex - 1) * strpageSize + 1;

        intStart = intStart - 1;
        gvUsers.PageIndex = intpageindex;

        string strSel = search_click();
        count = clsOdbc.executeScalar_int("SELECT Count(1) from mlm_login c INNER JOIN mlm_personal_details a ON a.userid=c.userid AND c.LoginTypeId > 1 and c.Active=1 AND c.product_status=0 " + strSel + " ");

        strQuery = "SELECT a.userid,a.Email,a.username,a.mobile_number, c.status, DATE_FORMAT(c.created_on,'%d %b %Y') As DOJ,c.my_sponsar_id,c.referral_id,c.password,c.trans_pwd From mlm_login c INNER JOIN mlm_personal_details a ON a.userid=c.userid  AND c.LoginTypeId>1  and c.Active=1 AND c.product_status=0 " + strSel + " Order By c.userid DESC" + " LIMIT " + intStart + "," + strpageSize + "";


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
                CommonMessages.ShowAlertMessage("Sorry, No Records Found!");
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
            gvUsers.DataSource = GetData(1);
            gvUsers.DataBind();
        }
    }

    protected void gvUsers_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow | e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
        }

        //*** Set Visible = False for Asset ID  ****
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[11].Text == "0")
            {
                e.Row.Cells[11].Text = "Pending";
            }
            else if (e.Row.Cells[11].Text == "1")
            {
                e.Row.Cells[11].Text = "Active";
            }
            else
            {
                e.Row.Cells[11].Text = "Suspended";
            }
        }


        //*** Set Edit Form in Edit Link  ****

        foreach (GridViewRow rw in gvUsers.Rows)
        {
            HyperLink lnkActivate = default(HyperLink);

            string strUserID = null;
            strUserID = rw.Cells[0].Text;

            lnkActivate = (HyperLink)rw.FindControl("lnkUpdate");

            lnkActivate.NavigateUrl = "edit_user_details.aspx?UID=" + strUserID;

        }

    }


    protected void gvUsers_Sorting(object sender, System.Web.UI.WebControls.GridViewSortEventArgs e)
    {
        ViewState["sortExp"] = e.SortExpression;
        gvUsers.DataSource = GetData(gvUsers.PageIndex);
        gvUsers.DataBind();

    }

    protected void lnkActive_Click(object sender, System.EventArgs e)
    {
        clsWallet objWallet = new clsWallet();
        DateTime dtcurDateTime = objWallet.getTime("India Standard Time");
        string active_date = dtcurDateTime.ToString("yyyy-MM-dd HH:mm:ss");

        // To User Name from Gridview

        int i = 0;

        if (gvUsers.Rows.Count == 0)
        {
            lblError.Text = "* No records to Actve.";
            return;
        }


        foreach (GridViewRow rw in gvUsers.Rows)
        {
            string vid = null;
            vid = rw.Cells[0].Text;

            CheckBox chkSel = default(CheckBox);
            chkSel = (CheckBox)rw.Cells[1].FindControl("chkSel");

            if (chkSel.Checked == true)
            {
                int intCurStatus = clsOdbc.executeScalar_int("SELECT status FROM mlm_login WHERE userid=" + vid);

                if (intCurStatus != 1)
                {
                    clsOdbc.executeNonQuery("UPDATE mlm_login SET status=1,status_modified_on = '" + active_date + "' where userid =" + vid + "");

                    clsOdbc.executeNonQuery("INSERT INTO mlm_status_log(userid,old_status,status,modified_by,change_type) VALUES(" + vid + "," + intCurStatus + "," + 1 + "," + 1 + "," + 4 + ")");

                    clsOdbc.executeNonQuery("call UpdateStatus(" + vid + ",1,1,1)");

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
            gvUsers.DataSource = GetData(gvUsers.PageIndex);
            gvUsers.DataBind();
        }
    }


    protected void lnkDisable_Click(object sender, System.EventArgs e)
    {
        clsWallet objWallet = new clsWallet();
        DateTime dtcurDateTime = objWallet.getTime("India Standard Time");
        string suspend_date = dtcurDateTime.ToString("yyyy-MM-dd HH:mm:ss");


        // To User Name from Gridview

        int i = 0;

        if (gvUsers.Rows.Count == 0)
        {
            lblError.Text = "* No records to Suspend.";
            return;
        }


        foreach (GridViewRow rw in gvUsers.Rows)
        {
            string vid = null;
            vid = rw.Cells[0].Text;

            CheckBox chkSel = default(CheckBox);
            chkSel = (CheckBox)rw.Cells[1].FindControl("chkSel");

            if (chkSel.Checked == true)
            {
                int intCurStatus = clsOdbc.executeScalar_int("SELECT status FROM mlm_login WHERE userid=" + vid);


                if (intCurStatus != 2)
                {
                    clsOdbc.executeNonQuery("UPDATE mlm_login SET status=2,status_modified_on = '" + suspend_date + "' where status <> 0 and userid =" + vid + "");

                    clsOdbc.executeNonQuery("INSERT INTO mlm_status_log(userid,old_status,status,modified_by,change_type) VALUES(" + vid + "," + intCurStatus + "," + 2 + "," + 1 + "," + 2 + ")");

                    clsOdbc.executeNonQuery("call UpdateStatus(" + vid + ",2,1,1)");
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
            gvUsers.DataSource = GetData(gvUsers.PageIndex);
            gvUsers.DataBind();
        }

    }

    protected void lnkbtnGenerateReport_Click(object sender, System.EventArgs e)
    {
        gvUsers.DataSource = GetData(1);
        gvUsers.DataBind();
    }
    protected void Page_Changed(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        gvUsers.DataSource = GetData(pageIndex);
        gvUsers.DataBind();
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
    public string process(object value1)
    {
        if (Convert.ToBoolean(value1) == true)
        {
            return "btn_box";
        }
        else
        {
            return "current_page";
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

    protected void lnkPrint_Click(object sender, EventArgs e)
    {
        //gvEpin.AllowPaging = false;

        //DataSet ds = new DataSet();

        //try
        //{

        //    string strQuery = "SELECT d.my_sponsar_id as USERID,e.UserNmae as USERNAME,b.my_sponsar_id,c.UserName,a.amt ,DATE_FORMAT(a.recieved_date,'%d %M %Y') As recieved_date,a.status,a.level FROM mlm_weekly_pay_income a,mlm_personal_details c,mlm_login b,mlm_login d,mlm_personal_details e where  a.child_id = b.userid and a.child_id  = c.userid and a.userid=d.userid and d.userid=e.userid  Order By a.userid DESC ";
        //    ds = clsOdbc.getDataSet(strQuery);
        //    gvEpin.DataSource = ds;
        //    gvEpin.DataBind();
        //}
        //catch (Exception ex)
        //{
        //}
        //finally
        //{
        //    ds.Dispose();
        //}


        StringWriter sw = new StringWriter();

        HtmlTextWriter hw = new HtmlTextWriter(sw);

        gvUsers.RenderControl(hw);

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

        gvUsers.DataSource = GetData(gvUsers.PageIndex);
        gvUsers.DataBind();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        // Verifies that the control is rendered 
    }
    protected void lnkbtnExportExcel_Click(object sender, EventArgs e)
    {
        CommonFunctions.exportFile("InActiveUsers.xls", ".xls", gvUsers, pnllead);
    }
}