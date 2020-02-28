using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_member_Review : System.Web.UI.Page
{
    ODBC clsodbc = new ODBC();
    clsWallet objWallet = new clsWallet();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Response.Redirect("../../login.aspx");
        }

        if (!IsPostBack)
        {

        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
             if(txtReview.Text!="")
             {
                 clsodbc.executeNonQuery("INSERT INTO `tbl_reviews`(`userid`, `comment`, `created_on`) VALUES ('" + Session["UserID"] + "', '" + txtReview.Text + "', '" + objWallet.getCurDateTimeString() + "' )");
                 CommonMessages.ShowAlertMessage_Reload("Review submitted successfully!", "overview.aspx");
             }
        }
        catch (Exception ex)
        {
        }
    } 
}