using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_admin_pending_payout : System.Web.UI.Page
{
    ODBC clsOdbc = new ODBC();
    ClassOther objOther = new ClassOther();
    private const string ASCENDING = " ASC";

    private const string DESCENDING = " DESC";

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

        int count = clsOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_login a JOIN mlm_personal_details c on a.userid=c.userid JOIN mlm_request b on a.userid=b.userid LEFT JOIN mlm_bank_account_details d on d.userid=b.userid WHERE b.status=0  Order By b.request_date DESC");

        strQuery = "SELECT b.id, a.my_sponsar_id, c.username AS Beneficiary_Name, d.account_number AS Beneficiary_Bank_Account_Number, b.amount_given as Amount, d.bank_name AS Bank_Name, d.branch_name AS Branch_Name, d.city AS Bank_City, d.ifsc_code AS IFSC_Code From mlm_login a JOIN mlm_personal_details c on a.userid=c.userid JOIN mlm_request b on a.userid=b.userid LEFT JOIN mlm_bank_account_details d on d.userid=b.userid WHERE b.status=0  Order By b.request_date DESC" + " LIMIT " + intStart + "," + strpageSize + "";

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
        }

      //  if (e.Row.Cells[11].Text == "0")
     //   {
     //       e.Row.Cells[11].Text = " Pending";
     //   }
      
        //*** Set Edit Form in Edit Link  ****
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
    //     protected void lnkbtnExportExcel_Click(object sender, EventArgs e)
    //     {
    //         CommonFunctions.exportFile("pending_withdrwal_request.xls", ".xls", gvMembers, pnllead);
    //     }


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
    protected void lnkbtnPayPayout_Click(object sender, EventArgs e)
    {
        clsWallet objWallet = new clsWallet();
        DateTime dtcurDateTime = objWallet.getTime("India Standard Time");
        string active_date = dtcurDateTime.ToString("yyyy-MM-dd HH:mm:ss");

        // To User Name from Gridview

        int i = 0;

        if (gvMembers.Rows.Count == 0)
        {
            lblError.Text = "* No records to Payout.";
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
                //double dblReqAmt = clsOdbc.executeScalar_dbl("SELECT request_amount FROM mlm_request WHERE userid=" + vid);

                //clsOdbc.executeNonQuery("call payPayout(" + vid + ",'"+ dblReqAmt +"')");

                clsOdbc.executeNonQuery("UPDATE mlm_request SET status=3 WHERE id=" + vid);
                i = i + 1;
            }
        }

        if (i == 0)
        {
            lblError.Text = " * Kindly select a record.";
        }
        else
        {            
			try
            {
                DataTable dtsms = clsOdbc.getDataTable("SELECT b.username, a.amount_given, b.mobile_number FROM mlm_request a INNER JOIN mlm_personal_details b On a.userid=b.userid AND a.status=3 ");
                string strmobileNo = "", struserName = "", stramount = "";
                if (dtsms.Rows.Count > 0)
                {
                    clsCommunication objcomm = new clsCommunication();

                    foreach (DataRow dr in dtsms.Rows)
                    {
                        strmobileNo = dr["mobile_number"].ToString();
                        struserName = dr["username"].ToString();
                        stramount = dr["amount_given"].ToString();

                        string strmessage = "Congratulation Mr/Mrs " + struserName + ", Your payout has been transferred to your account, " + stramount + " Will be credited in your A/c within 24 hrs. Thank you. www.mgh4all.in";

                        objcomm.SMS_API_for_Single_SMS(strmessage, strmobileNo);
                    }
                }
            }
            catch (Exception ex)
            {
                CommonMessages.ShowAlertMessage(ex.Message);
            }
			
			clsOdbc.executeNonQuery("call pay_Payout(1)");
			
            gvMembers.DataSource = GetData(gvMembers.PageIndex);
            gvMembers.DataBind();
        }

    }
    protected void lnkRejectPayout_Click(object sender, EventArgs e)
    {
        clsWallet objWallet = new clsWallet();
        DateTime dtcurDateTime = objWallet.getTime("India Standard Time");
        string active_date = dtcurDateTime.ToString("yyyy-MM-dd HH:mm:ss");

        // To User Name from Gridview

        int i = 0;

        if (gvMembers.Rows.Count == 0)
        {
            lblError.Text = "* No records to Payout.";
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

                clsOdbc.executeNonQuery("UPDATE mlm_request SET status=3 WHERE id=" + vid);
                i = i + 1;
            }
        }

        if (i == 0)
        {
            lblError.Text = " * Kindly select a record.";
        }
        else
        {
            clsOdbc.executeNonQuery("call pay_Payout(2)");

            gvMembers.DataSource = GetData(gvMembers.PageIndex);
            gvMembers.DataBind();
        }
    }
}