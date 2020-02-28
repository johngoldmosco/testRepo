using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_admin_AddProduct : System.Web.UI.Page
{
    ODBC clsOdbc = new ODBC();
    form_func objFormFun = new form_func();
    ClassOther objOther = new ClassOther();
    clsPhoto objphoto = new clsPhoto();
    clsWallet objWallet = new clsWallet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminID"] == null)
        {
            Response.Redirect("../../login.aspx");
        }
    }
    protected void btnAddProduct_Click(object sender, EventArgs e)
    {
        try
        {
            clsOdbc.executeNonQuery("INSERT INTO `mlm_epin_type`(prod_category,`capping`, `main_epin_id`, `pin_type`, `epin_cost`, `product_MRP`, `franchise_comm`, `bv`, `reward_point`, `description`, `direct_status`, `direct_income_type`, `direct_income`, `binary_status`, `binary_income`, `product_name`, `cashback_amt`) VALUES ('"+ ddlCategory.SelectedValue +"','"+ txtCapping.Text +"',2,'"+ txtProdName.Text +"','"+ txtProductCost.Text +"',0,0,'"+ txtBV.Text +"',0,'"+ txtProdDesc.Text +"',1,2,4,1,10,'"+ txtProdName.Text +"','"+ txtCashback.Text +"' )");

            CommonMessages.ShowAlertMessage_Reload("Package Added Successfully!","ProductManager.aspx");
        }
        catch(Exception ex){
            CommonMessages.ShowAlertMessage_Reload(ex.Message,"ProductManager.aspx");
        }
    }
}