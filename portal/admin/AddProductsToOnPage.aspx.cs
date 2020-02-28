using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_admin_AddProductsToOnPage : System.Web.UI.Page
{
    ODBC objOdbc = new ODBC();
    form_func objFormFun = new form_func();
    ClassOther objOther = new ClassOther();
    clsPhoto objphoto = new clsPhoto();
    clsWallet objWallet = new clsWallet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if(Session["AdminID"]==null)
        {
            Response.Redirect("../../Login.aspx");
        }
    }
    protected void btnAddProduct_Click(object sender, EventArgs e)
    {
        string photo = "";
        try {

            if (flupProdImg.HasFile)
            {
                string strImg = objphoto.UploadPhoto(flupProdImg);
                flupProdImg.SaveAs(Server.MapPath("../image/Products/") + strImg);
                photo = "../image/Products/" + strImg;
            }
            else
            {
        //        photo = imgProfile.ImageUrl;
            }

            objOdbc.executeNonQuery("INSERT INTO `mlm_epin_type`(`capping`, `main_epin_id`, `prod_category`, `pin_type`, `gst`, `epin_cost`, `product_MRP`, `franchise_comm`, `bv`, `reward_point`, `description`, `direct_status`, `direct_income_type`, `direct_income`, `binary_status`, `binary_income`, `product_name`, `cashback_amt`, `qty`, `Active`) VALUES (0,2,2,'" + txtProductName.Text + "', '" + txtGst.Text + "','" + txtProductCost.Text + "',0,0,0,0,'" + txtProdDesc.Text + "',0, 1, 0, 0, 0,'" + txtProductName.Text + "',0,1,1 )");
            int intID = objOdbc.executeScalar_int("SELECT max(id) FROM mlm_epin_type");

            objOdbc.executeNonQuery("INSERT INTO `mlm_product`(epin_id,`prod_name`, `prod_desc`,`prod_price`, `photo`, `created_on`) VALUES ('" + intID + "','" + txtProductName.Text + "','" + txtProdDesc.Text + "','" + txtProductCost.Text + "','" + photo + "','" + objWallet.getCurDateTimeString() + "')");

            CommonMessages.ShowAlertMessage_Reload("Product Added Successfully!", "AddProductsToOnPage.aspx");
        }
        catch (Exception ex){}
    }
}