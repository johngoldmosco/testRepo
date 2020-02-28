using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_member_NewMember : System.Web.UI.Page
{
    ODBC clsOdbc = new ODBC();
    form_func objFormFun = new form_func();
    ClassOther objOther = new ClassOther();
    clsPhoto objphoto = new clsPhoto();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Response.Redirect("../../login.aspx");
        }

        if (!IsPostBack)
        {
            objOther.filldropdownlist("SELECT country_id,country_name FROM mlm_country WHERE Active=1", ddlCountry, "country_name", "country_id");
            txtReferralID.Text = clsOdbc.executeScalar_str("SELECT my_sponsar_id FROM mlm_login WHERE userid='" + Session["UserID"] + "'");
            txtReferralName.Text = clsOdbc.executeScalar_str("SELECT username FROM mlm_personal_details WHERE userid='" + Session["UserID"] + "'");
        }

        string strposition = Request.QueryString["pos"];
        string strQueryString = Request.QueryString["pid"];
        if (strQueryString != null && strposition != null)
        {
            txtParentID.Text = clsOdbc.executeScalar_str("SELECT my_sponsar_id FROM mlm_login WHERE userid=" + strQueryString);
            txtParentID.Enabled = false;

            txtParentName.Text = clsOdbc.executeScalar_str("SELECT username FROM mlm_personal_details WHERE userid=" + strQueryString);
            if (strposition == "R")
                ddlPosition.SelectedValue = "2";
            else if (strposition == "L")
                ddlPosition.SelectedValue = "1";

            ddlPosition.Enabled = false;
        }
    }

    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCountry.SelectedValue != "0")
        {
            txtMobileCode.Text = clsOdbc.executeScalar_str("SELECT mobile_code  FROM mlm_country WHERE country_id='" + ddlCountry.SelectedValue + "'");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int intParentID = clsOdbc.executeScalar_int("SELECT userid FROM mlm_login WHERE my_sponsar_id='"+ txtParentID.Text +"'");

            if (ddlPosition.SelectedValue != "0")
            {
                int intUserID = clsOdbc.executeScalar_int("CALL addRegister ('" + txtReferralID.Text + "', '" + txtFullName.Text + "', '" + txtMobileCode.Text + "', '" + txtMobileNumber.Text + "', '" + txtEmail.Text + "',2, '" + ddlPosition.SelectedValue + "', '" + ddlCountry.SelectedValue + "', '" + intParentID + "')");

                //      CommonMessages.ShowAlertMessage_Reload("A New Member Added Successfully!", "overview.aspx");

                if (intUserID > 0)
                {
                    Session["WelcomeID"] = intUserID.ToString();
                    string strIPAddress = Request.ServerVariables["REMOTE_ADDR"];
                    //     objOther.AddRegisterIPHistory(intUserId, strIPAddress);       

                }

                CommonMessages.ShowAlertMessage("A New Member Added Successfully!");
                Response.Redirect("Welcome.aspx"); 
            }          
            
        }
        catch (Exception ex)
        { }
    }

    protected void txtReferralID_TextChanged(object sender, EventArgs e)
    {
        int intCount = clsOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_login WHERE my_sponsar_id='" + txtReferralID.Text + "'");
        if (intCount == 1)
        {
            txtReferralName.Text = clsOdbc.executeScalar_str("SELECT a.username FROM mlm_personal_details a INNER JOIN mlm_login b ON a.userid=b.userid WHERE b.my_sponsar_id= '" + txtReferralID.Text + "'");
        }
        else
        {
            txtReferralID.Text = "";
            txtReferralID.Attributes.Add("placeholder", "User Does not Exist");
            txtReferralID.Focus();
        }
    }
    protected void txtParentID_TextChanged(object sender, EventArgs e)
    {
        int intCount = clsOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_login WHERE my_sponsar_id='" + txtParentID.Text + "'");
        if (intCount == 1)
        {
            txtParentName.Text = clsOdbc.executeScalar_str("SELECT a.username FROM mlm_personal_details a INNER JOIN mlm_login b ON a.userid=b.userid WHERE b.my_sponsar_id= '" + txtParentID.Text + "'");
        }
        else
        {
            txtParentID.Text = "";
            txtParentID.Attributes.Add("placeholder", "User Does not Exist");
            txtParentID.Focus();
        }
    }
    protected void ddlPosition_SelectedIndexChanged(object sender, EventArgs e)
    {
        int intParentActiveStatus = 1;// clsOdbc.executeScalar_int("SELECT product_status FROM mlm_login WHERE my_sponsar_id='" + txtParentID.Text + "'");

        if (intParentActiveStatus == 1)
        {
            int intNodeStatus = clsOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_login WHERE my_sponsar_sys_id='" + txtParentID.Text + "' AND 	node_flag= '" + ddlPosition.SelectedValue + "'");

            if (intNodeStatus == 1)
            {
                lblMessage.Text = "Selected Position of Parent ID is already filled";
                btnSubmit.Enabled = false;
            }
            else
            {
                lblMessage.Text = "";
                btnSubmit.Enabled = true;
            }
        }
        else
        {
            lblMessage.Text = "Entered Parent ID is not Activated";
            btnSubmit.Enabled = false;
        }
    }
}