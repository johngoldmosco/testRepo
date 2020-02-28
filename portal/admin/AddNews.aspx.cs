using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_admin_AddNews : System.Web.UI.Page
{
    ODBC objOdbc = new ODBC();
    clsWallet objWallet = new clsWallet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminID"] == null)
        {
            Response.Redirect("../../Login.aspx");
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            objOdbc.executeNonQuery("INSERT INTO `tbl_news`(`news_title`, `news_desc`, `created_on`) VALUES ('" + txtNewsTitle.Text + "','" + txtDescription.Text + "','" + objWallet.getCurDateTimeString() + "')");

            CommonMessages.ShowAlertMessage_Reload("News Added Successfully!", "NewsManager.aspx");
        }
        catch (Exception ex)
        {

        }
    }
}