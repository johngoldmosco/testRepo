using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_admin_AddStockToFranchise : System.Web.UI.Page
{
    ODBC objOdbc = new ODBC();
    ClassOther clsother = new ClassOther();
    clsWallet objWallet = new clsWallet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminID"] == null)
        {
            Response.Redirect("../../Login.aspx");
        }
        if (!IsPostBack)
        {
            clsother.filldropdownlist("SELECT a.userid AS UID, b.username AS Uname FROM mlm_login a INNER JOIN mlm_personal_details b ON a.userid=b.userid WHERE a.UserTypeId=3 AND a.status=1", ddlFranchiese, "Uname", "UID");
            ddlFranchiese.Items.RemoveAt(0);
            ddlFranchiese.Items.Insert(0, new ListItem("Select Franchise", "0"));
            clsother.filldropdownlist("SELECT id, pin_type FROM mlm_epin_type WHERE Active=1 AND prod_category=2", ddlProducts, "pin_type", "id");
            ddlProducts.Items.RemoveAt(0);
            ddlProducts.Items.Insert(0, new ListItem("Select Products", "0"));
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (ddlFranchiese.SelectedValue != "0")
        {
            if (ddlProducts.SelectedValue != "0")
            {
                if (int.Parse(txtStock.Text) > 0)
                {
                    int intCount = objOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_franchise_stock WHERE franchise_id='" + ddlFranchiese.SelectedValue + "' AND product_id= " + ddlProducts.SelectedValue + " ");
                    if (intCount == 0)
                    {
                        objOdbc.executeNonQuery("INSERT INTO `mlm_franchise_stock`(`franchise_id`, `product_id`, `stock`, `last_update`) VALUES ('" + ddlFranchiese.SelectedValue + "', '" + ddlProducts.SelectedValue + "', '" + txtStock.Text + "','" + objWallet.getCurDateTimeString() + "') ");
						
						objOdbc.executeNonQuery("INSERT INTO `mlm_franchise_stock_transfer`(`fransid`, `prod_id`,prev_stock, `stock_qty`, `stock_on`) VALUES ('" + ddlFranchiese.SelectedValue + "', '" + ddlProducts.SelectedValue + "',0, '" + txtStock.Text + "','" + objWallet.getCurDateTimeString() + "') ");
                    }
                    else
                    {
						int intPrevStock= objOdbc.executeScalar_int("SELECT stock FROM mlm_franchise_stock WHERE franchise_id='" + ddlFranchiese.SelectedValue + "' AND product_id= " + ddlProducts.SelectedValue + " ");
						
                        objOdbc.executeNonQuery("UPDATE mlm_franchise_stock SET stock=stock + " + txtStock.Text + " WHERE franchise_id= " + ddlFranchiese.SelectedValue + " AND product_id= " + ddlProducts.SelectedValue + " ");
						
						objOdbc.executeNonQuery("INSERT INTO `mlm_franchise_stock_transfer`(`fransid`, `prod_id`,prev_stock, `stock_qty`, `stock_on`) VALUES ('" + ddlFranchiese.SelectedValue + "', '" + ddlProducts.SelectedValue + "','"+ intPrevStock +"', '" + txtStock.Text + "','" + objWallet.getCurDateTimeString() + "') ");
                    }
					
                    CommonMessages.ShowAlertMessage_Reload("Stock Updated Successfully!", "rptFranchiseStock.aspx");
                }
            }
            else { txtCurStock.Text = ""; }
        }
        else { txtCurStock.Text = ""; }

    }
    protected void ddlProducts_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFranchiese.SelectedValue != "0")
        {
            if (ddlProducts.SelectedValue != "0")
            {
                int intStock = objOdbc.executeScalar_int("SELECT stock FROM mlm_franchise_stock WHERE franchise_id= " + ddlFranchiese.SelectedValue + " AND product_id= " + ddlProducts.SelectedValue + "  ");
                txtCurStock.Text = intStock.ToString();
            }
            else { txtCurStock.Text = ""; }
        }
        else { txtCurStock.Text = ""; }
    }
}