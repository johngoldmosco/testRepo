using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_member_PayoutHistory : System.Web.UI.Page
{
    ODBC clsOdbc = new ODBC();
    private const string ASCENDING = " ASC";

    private const string DESCENDING = " DESC";
    int count;
    public string search_click()
    {
        count = 0;
        string strsel = "";

        if (txtUserID.Text != "")
        {
            strsel = " AND b.my_sponsar_id='" + Convert.ToString(txtUserID.Text) + "' ";
        }

        return strsel;
    }

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
        count = clsOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_request a INNER JOIN mlm_login b ON a.userid=b.userid WHERE  a.userid='" + Session["UserID"] + "'" + strSel + "");

        strQuery = "SELECT a.id, a.userid, b.my_sponsar_id, a.wallet1, a.request_amount, a.amount_given, a.tds, a.service_charge, a.request_date, a.confirmed_on, a.reject_on, a.status FROM mlm_request a INNER JOIN mlm_login b ON a.userid=b.userid WHERE a.userid='"+ Session["UserID"] +"' " + strSel + " Order By a.request_date DESC  LIMIT " + intStart + "," + strpageSize + "";

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
   //             lblError.Text = "Sorry, No Records Found!";
            }
            return dv;

        }
        catch (Exception ex)
        {
            CommonMessages.ShowAlertMessage(ex.Message);
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
        if (Session["UserID"] == null)
        {
            Response.Redirect("../../Login.aspx");
        }
        if (!IsPostBack)
        {
            gvUsers.DataSource = GetData(1);
            gvUsers.DataBind();
        }
    }

    protected void gvUsers_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        if (e.Row.RowType.Equals(DataControlRowType.DataRow))
        {
            e.Row.Cells[0].Text = "" + (((((GridView)sender).PageIndex - 1) * ((GridView)sender).PageSize) + (e.Row.RowIndex + 1));
        }

        if(e.Row.Cells[9].Text=="0"){
            e.Row.Cells[9].Text = "Pending";
        }
        if (e.Row.Cells[9].Text == "1")
        {
            e.Row.Cells[9].Text = "Success";
        }
        if (e.Row.Cells[9].Text == "2")
        {
            e.Row.Cells[9].Text = "Reject";
        }
    }

    protected void gvUsers_Sorting(object sender, System.Web.UI.WebControls.GridViewSortEventArgs e)
    {
        ViewState["sortExp"] = e.SortExpression;
        gvUsers.DataSource = GetData(gvUsers.PageIndex);
        gvUsers.DataBind();

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
        CommonFunctions.exportFile("Users.xls", ".xls", gvUsers, pnllead);
    }
}