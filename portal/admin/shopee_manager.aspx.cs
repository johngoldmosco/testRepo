using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_admin_shopee_manager : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
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
    protected void btnAddProduct_Click(object sender, EventArgs e)
    {
        Response.Redirect("add_shopee.aspx");
    }

    ODBC clsOdbc = new ODBC();
    private const string ASCENDING = " ASC";

    private const string DESCENDING = " DESC";
    int count;
    public string search_click()
    {
        count = 0;
        string strsel = "";
        if (txtShopeeName.Text != "")
        {
            strsel = strsel + " AND shopee_name LIKE '%" + Convert.ToString(txtShopeeName.Text) + "%' ";
        }
      
        return strsel;
    }


    protected void gvCampaign_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {

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
        count = clsOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_shopee WHERE active = 1 " + strSel + " ");

        strQuery = "SELECT id,shopee_name, address, status FROM mlm_shopee WHERE active=1 " + strSel + " Order By id DESC" + " LIMIT " + intStart + "," + strpageSize + "";


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

    protected void gvUsers_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow | e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
        }

        //*** Set Visible = False for Asset ID  ****
        if (e.Row.RowType == DataControlRowType.DataRow)
        {           

            if (e.Row.Cells[6].Text == "0")
            {
                e.Row.Cells[6].Text = "Pending";
            }
            else if (e.Row.Cells[6].Text == "1")
            {
                e.Row.Cells[6].Text = "Active";
            }
            else
            {
                e.Row.Cells[6].Text = "Suspended";
            }
        }


        //*** Set Edit Form in Edit Link  ****

        foreach (GridViewRow rw in gvUsers.Rows)
        {
            HyperLink lnkActivate = default(HyperLink);

            string strUserID = null;
            strUserID = rw.Cells[0].Text;

            lnkActivate = (HyperLink)rw.FindControl("lnkUpdate");

            lnkActivate.NavigateUrl = "UpdateShopee.aspx?ProdID=" + strUserID;
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
                int intCurStatus = clsOdbc.executeScalar_int("SELECT status FROM mlm_shopee WHERE id=" + vid);

                if (intCurStatus != 1)
                {
                    clsOdbc.executeNonQuery("UPDATE mlm_shopee SET status=1 WHERE id =" + vid + "");
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
                int intCurStatus = clsOdbc.executeScalar_int("SELECT status FROM mlm_shopee WHERE id=" + vid);

                if (intCurStatus != 2)
                {
                    clsOdbc.executeNonQuery("UPDATE mlm_shopee SET status=2 where status <> 0 and id =" + vid + "");
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

  
    public override void VerifyRenderingInServerForm(Control control)
    {
        // Verifies that the control is rendered 
    }   
   
}