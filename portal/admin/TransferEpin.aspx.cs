using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_admin_TransferEpin : System.Web.UI.Page
{
    ODBC clsOdbc = new ODBC();
    ClassOther objOther = new ClassOther();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminID"] == "")
        {
            Response.Redirect("../../login.aspx");
        }
        if (!IsPostBack)
        {
            objOther.filldropdownlist("SELECT id, CONCAT(pin_type,' - ', epin_cost) as pin_type FROM mlm_epin_type WHERE Active=1", ddlEpinType, "pin_type", "id");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlEpinType.SelectedValue != "Select")
            {
                double dblEpinCost = clsOdbc.executeScalar_dbl("SELECT epin_cost FROm mlm_epin_type WHERE id= " + ddlEpinType.SelectedValue);

                int intCountEpins = clsOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_epin WHERE userid='" + Session["AdminID"] + "' AND epin_type='" + ddlEpinType.SelectedValue + "' AND status = 1");

                if (int.Parse(txtEpinNo.Text) <= intCountEpins)
                {
                    int intUserID = clsOdbc.executeScalar_int("SELECT userid FROM mlm_login WHERE my_sponsar_id='" + txtUserID.Text + "' ");

                    clsOdbc.executeNonQuery("CALL TransferEpin('" + intUserID + "','" + int.Parse(txtEpinNo.Text) + "', '" + ddlEpinType.SelectedValue + "','" + Session["AdminID"] + "', " + dblEpinCost + ")");

                    CommonMessages.ShowAlertMessage_Reload("E-Pin Transfered Successfully!", "TransferEpin.aspx");
                }
                else
                {
                    CommonMessages.ShowAlertMessage("Insufficient Epins");
                }
            }
            else
                CommonMessages.ShowAlertMessage("Select Epin Tpye!");
        }
        catch (Exception ex)
        {
            CommonMessages.ShowAlertMessage(ex.Message);
        }
    }

    protected void txtUserID_TextChanged(object sender, EventArgs e)
    {
        int intCount = clsOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_login WHERE my_sponsar_id='" + txtUserID.Text + "' AND status=1");

        if (intCount == 1)
        {
            int intUserID = clsOdbc.executeScalar_int("SELECT userid FROM mlm_login WHERE my_sponsar_id='" + txtUserID.Text + "' AND status=1");
            lblUserName.Text = "User Name: " + clsOdbc.executeScalar_str("SELECT username FROM mlm_personal_details WHERE userid=" + intUserID + "");
        }
        else
        {
            lblUserName.Text = "";
            txtUserID.Text = "";
            txtUserID.Attributes.Add("placeholder", "UserID does not exist!");
            txtUserID.Focus();
        }
    }
    protected void ddlEpinType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlEpinType.SelectedValue != "0")
            txtAvail.Text = clsOdbc.executeScalar_str("SELECT COUNT(1) FROM mlm_epin WHERE userid=" + Session["AdminID"] + " AND epin_type='" + ddlEpinType.SelectedValue + "' AND status=1");
        else
            txtAvail.Text = "0";
    }
}