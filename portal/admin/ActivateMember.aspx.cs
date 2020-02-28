using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_admin_ActivateMember : System.Web.UI.Page
{
    ODBC objOdbc = new ODBC();
    ClassOther objOther = new ClassOther();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminID"] == null)
        {
            Response.Redirect("../../login.aspx");
        }
        if (!IsPostBack)
        {
            if (Session["TopupID"] != null)
            {
                txtUserID.Text = Session["TopupID"].ToString();
                txtUserID.Enabled = false;
                txtUserID_TextChanged(sender, e);
                Session["TopupID"] = null;

            }
            objOther.filldropdownlist("SELECT id, CONCAT(pin_type,' - ', epin_cost) as pin_type FROM mlm_epin_type WHERE 1", ddlEpinType, "pin_type", "id");
        }
    }
    protected void txtUserID_TextChanged(object sender, EventArgs e)
    {
        lblError.Text = "";
        try
        {
            if (txtUserID.Text != "")
            {
                int intCount = objOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_login WHERE my_sponsar_id = '" + txtUserID.Text.Trim() + "' AND status=1");
                if (intCount == 1)
                {
                    int intactivateStatus = objOdbc.executeScalar_int("SELECT product_status FROM mlm_login WHERE my_sponsar_id = '" + txtUserID.Text.Trim() + "' AND status=1");

                    if (intactivateStatus == 0)
                    {
                        txtUserName.Text = objOdbc.executeScalar_str("SELECT a.username FROM mlm_personal_details a INNER JOIN mlm_login b ON a.userid=b.userid WHERE b.my_sponsar_id = '" + txtUserID.Text + "'");
                    }
                    else
                    {
                        lblError.Text = "Entered Member is Already Activated!";
                        txtUserID.Text = "";
                        txtUserID.Focus();
                    }
                }
                else
                {
                    lblError.Text = "Please Enter Correct User ID to Activate!";
                    txtUserID.Text = "";
                    txtUserID.Focus();
                }
            }
            else
            {
                lblError.Text = "Please Enter User ID to Activate!";
                txtUserID.Focus();
            }
        }
        catch (Exception ex)
        { }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        lblError.Text = "";
        try
        {
            int intCount = objOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_epin WHERE userid=" + Session["AdminID"] + " AND status = 1 ");

            if (intCount > 0)
            {
                if (txtEpin.Text != "" && ddlEpinType.SelectedValue != "Select")
                {
                    string strEpin = txtEpin.Text;

                    int intValidEpinCount = objOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_epin WHERE epin = '" + txtEpin.Text + "' AND status= 1 AND userid= " + Session["AdminID"] + " AND epin_type = " + ddlEpinType.SelectedValue);

                    int intUserID = objOdbc.executeScalar_int("SELECT userid FROM mlm_login WHERE my_sponsar_id = '" + txtUserID.Text + "'");

                    objOdbc.executeNonQuery("CALL Activate_member(" + intUserID + ", '" + strEpin + "'," + Session["AdminID"] + ");");

                    CommonMessages.ShowAlertMessage_Reload("Member TopUP Successfully!", "dashboard.aspx");
                }
                else
                {
                    lblError.Text = "Please Select Epin Type First & then Enter Valid Epin!";
                    txtEpin.Text = "";
                    ddlEpinType.SelectedIndex = 0;
                    ddlEpinType.Focus();
                }
            }
            else
                lblError.Text = "You dont have sufficient balance of epin! Please Generate epin First.";
        }
        catch (Exception ex)
        {
            CommonMessages.ShowAlertMessage(ex.Message);
        }
    }

    protected void txtEpin_TextChanged(object sender, EventArgs e)
    {
        if (txtEpin.Text != null)
        {
            if (ddlEpinType.SelectedValue != "select")
            {
                int intValidEpin = objOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_epin WHERE epin='" + txtEpin.Text + "' AND epin_type='" + ddlEpinType.SelectedValue + "' AND status=1");
                if (intValidEpin == 1)
                {
                    txtEpin.BorderColor = System.Drawing.Color.Green;
                    txtEpin.Enabled = false;
                    ddlEpinType.Enabled = false;
                    txtUserID.Enabled = false;
                    btnSubmit.Enabled = true;
                }
                else
                {
                    txtEpin.Text = "";
                    txtEpin.Attributes.Add("placeholder", "Enter valid Epin!");
                    txtEpin.BorderColor = System.Drawing.Color.Red;
                    btnSubmit.Enabled = false;
                }
            }
        }
        else
        {
            CommonMessages.ShowAlertMessage("Enter Valid Epin!");
        }
    }
}