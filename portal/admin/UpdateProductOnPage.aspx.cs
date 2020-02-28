using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_admin_UpdateProductOnPage : System.Web.UI.Page
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
            Response.Redirect("../../Login.aspx");
        }
        if (!IsPostBack)
        {
            FillDetails();
        }
    }
    protected void btnUpdateProduct_Click(object sender, EventArgs e)
    {
        string photo = "";
        try
        {
            if (flupProdImg.HasFile)
            {
                string strImg = objphoto.UploadPhoto(flupProdImg);
                flupProdImg.SaveAs(Server.MapPath("../image/Products/") + strImg);
                photo = "../image/Products/" + strImg;
            }
            else
            {
                photo = imgProd.ImageUrl;
            }

            if (Request.QueryString[0] != "")
            {
                clsOdbc.executeNonQuery("UPDATE mlm_product SET `prod_name`='" + txtProductName.Text + "', `prod_desc`='" + txtProdDesc.Text + "',photo='" + photo + "',`prod_price`='" + txtProductCost.Text + "', status='" + ddlStatus.SelectedValue + "',modify_on='" + objWallet.getCurDateTimeString() + "' WHERE id='" + Request.QueryString[0] + "' ");

                clsOdbc.executeNonQuery("UPDATE `mlm_epin_type` SET `pin_type`='" + txtProductName.Text + "',`epin_cost`='" + txtProductCost.Text + "',gst='" + txtGst.Text + "',`product_MRP`='" + txtProductCost.Text + "',`description`='" + txtProdDesc.Text + "', `product_name`='" + txtProductName.Text + "', Active='" + ddlStatus.SelectedValue + "' WHERE 	id='" + lblEpinID.Text + "' AND prod_category=2");

                CommonMessages.ShowAlertMessage_Reload("Product Updated Successfully!", "OnPageProductManager.aspx");
            }
        }
        catch (Exception ex)
        {
            CommonMessages.ShowAlertMessage_Reload(ex.Message, "ProductManager.aspx");
        }
    }

    private void FillDetails()
    {
        string strQuery = "";
        System.Data.DataSet ds = new System.Data.DataSet();
        try
        {
            strQuery = "SELECT prod_name, prod_desc, prod_price, photo, status, active, epin_id FROM `mlm_product` WHERE   id=" + Request.QueryString[0];
            ds = clsOdbc.getDataSet(strQuery);

            if (ds.Tables[0].Rows.Count > 0)
            {
                txtProductName.Text = ds.Tables[0].Rows[0][0].ToString();
                txtProdDesc.Text = ds.Tables[0].Rows[0][1].ToString();
                txtProductCost.Text = ds.Tables[0].Rows[0][2].ToString();
                ddlStatus.SelectedValue = ds.Tables[0].Rows[0][4].ToString();
                imgProd.ImageUrl = ds.Tables[0].Rows[0][3].ToString();
                lblEpinID.Text = ds.Tables[0].Rows[0][6].ToString();
                txtGst.Text = clsOdbc.executeScalar_str("SELECT gst FROM mlm_epin_type WHERE id='" + lblEpinID.Text + "'");
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