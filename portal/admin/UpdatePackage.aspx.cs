using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_admin_UpdatePackage : System.Web.UI.Page
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
        if (!IsPostBack)
        {
            FillDetails();
        }
    }
    protected void btnUpdateProduct_Click(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString[0] != "")
            {
                clsOdbc.executeNonQuery("UPDATE mlm_epin_type SET `pin_type`='" + txtProductName.Text + "',`description`='" + txtProdDesc.Text + "',`epin_cost`='" + txtPrice.Text + "', Active='" + ddlStatus.SelectedValue + "', capping='"+ txtCapping.Text +"'  WHERE id='" + Request.QueryString[0] + "' ");

                CommonMessages.ShowAlertMessage_Reload("Package Updated Successfully!", "PackageManager.aspx");
            }
        }
        catch (Exception ex)
        {
            CommonMessages.ShowAlertMessage_Reload(ex.Message, "PackageManager.aspx");
        }
    }

    private void FillDetails()
    {
        string strQuery = "";
        System.Data.DataSet ds = new System.Data.DataSet();
        try
        {
            strQuery = "SELECT  `capping`, `prod_category`, `pin_type`, `gst`, `epin_cost`, `product_MRP`, `franchise_comm`, `bv`, `reward_point`, `description`, `direct_status`, `direct_income_type`, `direct_income`, `binary_status`, `binary_income`, `product_name`, `cashback_amt`, gst, Active FROM `mlm_epin_type` WHERE id=" + Request.QueryString[0];
            ds = clsOdbc.getDataSet(strQuery);

            if (ds.Tables[0].Rows.Count > 0)
            {
                txtProductName.Text = ds.Tables[0].Rows[0][2].ToString();
                txtProdDesc.Text = ds.Tables[0].Rows[0][9].ToString();
                txtPrice.Text = ds.Tables[0].Rows[0][4].ToString();
                ddlStatus.Text = ds.Tables[0].Rows[0][18].ToString();
                txtCapping.Text = ds.Tables[0].Rows[0][0].ToString();
            }
        }
        catch (Exception ex)
        {
        }
        finally
        {
            ds.Dispose();
        }
    }
}