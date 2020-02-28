using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_admin_UpdateShopee : System.Web.UI.Page
{
    ODBC obj = new ODBC();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminID"] == null)
        {
            Response.Redirect("../../login.aspx");
        }
        if (!IsPostBack)
        {
            if (Request.QueryString[0] != "")
            {
                txtName.Text = obj.executeScalar_str("SELECT shopee_name FROM  mlm_shopee WHERE id=" + Request.QueryString[0]);
               txtAddress .Text = obj.executeScalar_str("SELECT address FROM mlm_shopee WHERE id=" + Request.QueryString[0]);
            }
            else
            {
                Response.Redirect("shopee_manager.aspx");
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            obj.executeNonQuery("UPDATE mlm_shopee SET shopee_name='" + txtName.Text + "', address='" + txtAddress.Text + "'");
            CommonMessages.ShowAlertMessage_Reload("Shopee updated Successfully.","shopee_manager.aspx");

        }
        catch(Exception ex)
        {

        }
    }
}