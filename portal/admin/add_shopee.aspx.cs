using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_admin_add_shopee : System.Web.UI.Page
{
    ODBC clsodbc = new ODBC();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminID"] == null)
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
            int intCOunt = clsodbc.executeScalar_int("SELECT COUNT(1) FROM mlm_shopee WHERE shopee_name='" + txtName.Text + "'");
            if (intCOunt == 0)
            {
                clsodbc.executeScalar_int("INSERT INTO mlm_shopee(shopee_name, address) VALUES('" + txtName.Text + "','" + txtAddress.Text + "');");
                CommonMessages.ShowAlertMessage_Reload("Shopee Added Successfully.","shopee_manager.aspx");
            }
            else
            {
                CommonMessages.ShowAlertMessage("This Shopee Already Added!");
            }
        }
        catch (Exception ex)
        {

        }
    }
}