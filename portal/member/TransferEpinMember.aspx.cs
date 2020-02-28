using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_member_TransferEpinMember :  System.Web.UI.Page
{
    ODBC clsOdbc = new ODBC();
    ClassOther objOther = new ClassOther();
    protected void Page_Load(object sender, EventArgs e)
    {       
        if (!IsPostBack)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("../../login.aspx");
            }
            else
            {
                objOther.filldropdownlist("SELECT id, CONCAT(pin_type,' - ', epin_cost) as pin_type FROM mlm_epin_type WHERE Active=1", ddlEpinType, "pin_type", "id");
            }            
        }
    }
    protected void txtUserID_TextChanged(object sender, EventArgs e)
    {
        if (txtUserID.Text != "")
        {
            int intCount = clsOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_login WHERE my_sponsar_id='" + txtUserID.Text + "' AND status=1");
            if (intCount == 1)
            {
                int intUserID = clsOdbc.executeScalar_int("SELECT userid FROM mlm_login WHERE my_sponsar_id='" + txtUserID.Text + "' AND status=1");

                int intCheckDownline = clsOdbc.executeScalar_int("CALL check_downline(" + intUserID + ", " + Session["UserID"] + ");");
                if (intCheckDownline == 0)
                {
                    txtUserID.Attributes.Add("placeholder","Enter Valid Downline Member ID!");
                    lblError.Text = "Entered Member not your Downline Member";
                    txtUserID.Text = "";
                    txtUserID.Focus();
                }
                else
                {
                    lblError.Text = "";
                    lblUserName.Text = "User Name: " + clsOdbc.executeScalar_str("SELECT username FROM mlm_personal_details WHERE userid=" + intUserID + "");
                }                
            }
            else
            {
                lblUserName.Text = "";
                txtUserID.Text = "";
                txtUserID.Attributes.Add("placeholder", "UserID does not exist!");
                txtUserID.Focus();
            }
        }           
    }
    protected void ddlEpinType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlEpinType.SelectedValue != "0")
            txtAvail.Text = clsOdbc.executeScalar_str("SELECT COUNT(1) FROM mlm_epin WHERE userid=" + Session["UserID"] + " AND epin_type='" + ddlEpinType.SelectedValue + "' AND status=1");
        else
            txtAvail.Text = "0";
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlEpinType.SelectedValue != "Select")
            {
                double dblEpinCost = clsOdbc.executeScalar_dbl("SELECT epin_cost FROm mlm_epin_type WHERE id= " + ddlEpinType.SelectedValue);

                int intCountEpins = clsOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_epin WHERE userid='" + Session["UserID"] + "' AND epin_type='" + ddlEpinType.SelectedValue + "' AND status = 1");

                if (int.Parse(txtEpinNo.Text) <= intCountEpins)
                {
                    int intUserID = clsOdbc.executeScalar_int("SELECT userid FROM mlm_login WHERE my_sponsar_id='" + txtUserID.Text + "' ");

                    clsOdbc.executeNonQuery("CALL TransferEpin('" + intUserID + "','" + int.Parse(txtEpinNo.Text) + "', '" + ddlEpinType.SelectedValue + "','" + Session["UserID"] + "', " + dblEpinCost + ")");

                    CommonMessages.ShowAlertMessage_Reload("E-Pin Transfered Successfully!", "TransferEpinMember.aspx");
                }
                else
                {
                    CommonMessages.ShowAlertMessage("Insufficient Epins");
                    txtEpinNo.Focus();
                    txtEpinNo.BorderColor = System.Drawing.Color.Red;
                }
            }
            else
                CommonMessages.ShowAlertMessage("Select Epin Type!");
        }
        catch (Exception ex)
        {
            CommonMessages.ShowAlertMessage(ex.Message);
        }
    }
}