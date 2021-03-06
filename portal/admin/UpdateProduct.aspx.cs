﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_admin_UpdateProduct : System.Web.UI.Page
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
                clsOdbc.executeNonQuery("UPDATE mlm_epin_type SET `pin_type`='" + txtProductName.Text + "',`description`='" + txtProdDesc.Text + "',`prod_category`='" + ddlCategory.SelectedValue + "',`epin_cost`='" + txtPrice.Text + "',`bv`='" + txtPV.Text + "', gst='" + txtGst.Text + "',Active='"+ ddlStatus.SelectedValue +"',cashback_amt='"+ txtCashback.Text +"'  WHERE id='" + Request.QueryString[0] + "' ");

                if (ddlCategory.SelectedValue=="2")
                {
                    clsOdbc.executeNonQuery("UPDATE mlm_product SET `prod_name`='" + txtProductName.Text + "', `prod_desc`='" + txtProdDesc.Text + "',`prod_price`='" + txtPrice.Text + "', status='" + ddlStatus.SelectedValue + "',modify_on='" + objWallet.getCurDateTimeString() + "' WHERE epin_id='" + Request.QueryString[0] + "' ");
                }
                CommonMessages.ShowAlertMessage_Reload("Product Updated Successfully!", "ProductManager.aspx");
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
            strQuery = "SELECT  `capping`, `prod_category`, `pin_type`, `gst`, `epin_cost`, `product_MRP`, `franchise_comm`, `bv`, `reward_point`, `description`, `direct_status`, `direct_income_type`, `direct_income`, `binary_status`, `binary_income`, `product_name`, `cashback_amt`, gst, Active FROM `mlm_epin_type` WHERE   id=" + Request.QueryString[0];
            ds = clsOdbc.getDataSet(strQuery);

            if (ds.Tables[0].Rows.Count > 0)
            {
                txtProductName.Text = ds.Tables[0].Rows[0][2].ToString();
                txtProdDesc.Text = ds.Tables[0].Rows[0][9].ToString();
                ddlCategory.SelectedValue = ds.Tables[0].Rows[0][1].ToString();
                txtPrice.Text = ds.Tables[0].Rows[0][4].ToString();
                txtPV.Text = ds.Tables[0].Rows[0][7].ToString();
                txtGst.Text = ds.Tables[0].Rows[0][17].ToString();
				ddlStatus.Text = ds.Tables[0].Rows[0][18].ToString();
				txtCashback.Text = ds.Tables[0].Rows[0][16].ToString();
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