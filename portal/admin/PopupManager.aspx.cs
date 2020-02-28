using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_admin_PopupManager : System.Web.UI.Page
{
    ODBC objOdbc = new ODBC();
    clsPhoto objphoto = new clsPhoto();
    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            gvReport.DataSource = GetData(1);
            gvReport.DataBind();
        }
    }
    protected string Search()
    {
        string strsel = string.Empty;

        if (ddlSelectType.SelectedValue != "0")
        {
            strsel = strsel + " AND id="+ ddlSelectType.SelectedValue +"";
        }
        return strsel;
    }
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

        int count = objOdbc.executeScalar_int("SELECT COUNT(1) FROM tbl_popup WHERE 1 " + StrSearch + "");

        strQuery = "SELECT id, CASE	popup_type WHEN 1 THEN 'Text' WHEN 2 THEN 'Image' END AS popup_type, popup_header, content, imgUrl, CASE display_status WHEN 1  THEN 'Show' WHEN 2 THEN 'Hide' END AS status FROM tbl_popup  WHERE 1 " + StrSearch + "  Order By id ASC Limit " + intStart + "," + strpageSize + "";

        double dblPageCount = Convert.ToDouble(Convert.ToDecimal(count) / Convert.ToDecimal(strpageSize));
        int pageCount = Convert.ToInt32(Math.Ceiling(dblPageCount));
        ViewState["pageCount"] = pageCount;
        this.PopulatePager(intpageindex);
        try
        {
            ds = objOdbc.getDataSet(strQuery);

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
    public override void VerifyRenderingInServerForm(Control control)
    {
        // Verifies that the control is rendered 
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
            e.Row.Cells[3].Text = "" + (((((GridView)sender).PageIndex - 1) * ((GridView)sender).PageSize) + (e.Row.RowIndex + 1));
        }
		
		if (e.Row.RowType == DataControlRowType.DataRow | e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
        }
		
        foreach (GridViewRow rw in gvReport.Rows)
        {
            HyperLink lnkActivate = default(HyperLink);

            string strPID = null;
            strPID = rw.Cells[0].Text;

            lnkActivate = (HyperLink)rw.FindControl("lnkUpdate");

            lnkActivate.NavigateUrl = "EditPopup.aspx?PID=" + strPID;

        }
    }
    //protected void ddlPopupType_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlPopupType.SelectedValue != "0")
    //    {
    //        if (ddlPopupType.SelectedValue == "1")
    //        {
    //            pnlContent.Visible = true;
    //            pnlImage.Visible = false;
    //        }
    //        else
    //        {
    //            pnlContent.Visible = false;
    //            pnlImage.Visible = true;
    //        }
    //    }
    //}
    //protected void btnAddPopUp_Click(object sender, EventArgs e)
    //{
    //    string imgPopup = "";
    //    try
    //    {
    //        if (ddlPopupType.SelectedValue == "2")
    //        {
    //            if (flupImage.HasFile)
    //            {
    //                string strImg = objphoto.UploadPhoto(flupImage);
    //                flupImage.SaveAs(Server.MapPath("../image/PopUp/") + strImg);
    //                imgPopup = "../image/PopUp/" + strImg;
    //            }
    //            else
    //            {
    //                CommonMessages.ShowAlertMessage_Reload("Please select image first!", "PopupManager.aspx");
    //                imgPopup = "";
    //            }

    //            objOdbc.executeNonQuery("INSERT INTO tbl_popup(imgUrl) VALUES( '" + imgPopup + "')");
    //            CommonMessages.ShowAlertMessage_Reload("Popup added successfully!", "PopupManager.aspx");
    //        }
    //        else if (ddlPopupType.SelectedValue == "1")
    //        {
    //            objOdbc.executeNonQuery("INSERT INTO tbl_popup(	content) VALUES( '" + txtContent.Text + "')");
    //            CommonMessages.ShowAlertMessage_Reload("Popup added successfully!", "PopupManager.aspx");
    //        }
    //    }
    //    catch (Exception ex)
    //    { }
    //}
    protected void lnkShow_Click(object sender, EventArgs e)
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
            vid = rw.Cells[0].Text;

            CheckBox chkSel = default(CheckBox);
            chkSel = (CheckBox)rw.Cells[1].FindControl("chkSel");

            if (chkSel.Checked == true)
            {
                int intCurStatus = objOdbc.executeScalar_int("SELECT display_status FROM tbl_popup WHERE id=" + vid);

                if (intCurStatus != 1)
                {
                    objOdbc.executeNonQuery("UPDATE tbl_popup SET display_status=1 where id =" + vid + "");
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
    protected void lnkHide_Click(object sender, EventArgs e)
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
            vid = rw.Cells[0].Text;

            CheckBox chkSel = default(CheckBox);
            chkSel = (CheckBox)rw.Cells[1].FindControl("chkSel");

            if (chkSel.Checked == true)
            {
                int intCurStatus = objOdbc.executeScalar_int("SELECT display_status FROM tbl_popup WHERE id=" + vid);

                if (intCurStatus != 2)
                {
                    objOdbc.executeNonQuery("UPDATE tbl_popup SET display_status=2 where id =" + vid + "");
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
   
    protected void ddlSelectType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSelectType.SelectedValue != "0")
        {
            gvReport.DataSource = GetData(1);
            gvReport.DataBind();
            pnlShowData.Visible = true;
        }
        else
        {
            pnlShowData.Visible = false;
        }
    }
}