using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_member_UpgradeMembership : System.Web.UI.Page
{
    ODBC clsOdbc = new ODBC();
    form_func objFormFun = new form_func();
    ClassOther objOther = new ClassOther();
    clsPhoto objphoto = new clsPhoto();
    clsWallet objWallet = new clsWallet();
    BlockChain objBC = new BlockChain();
    clsCommunication objcomm = new clsCommunication();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Response.Redirect("../../login.aspx");
        }

        if (!IsPostBack)
        {
            int PrevPackageID = clsOdbc.executeScalar_int("SELECT package_id FROM mlm_login WHERE userid='" + Session["UserID"] + "'");

            if (PrevPackageID > 0)
            {
                txtUserID.Text = clsOdbc.executeScalar_str("SELECT my_sponsar_id FROM mlm_login WHERE userid=" + Session["UserID"]);
                txtUserName.Text = clsOdbc.executeScalar_str("SELECT username FROM mlm_personal_details WHERE userid='" + Session["UserID"] + "' ");

                int intEpinCost = clsOdbc.executeScalar_int("SELECT b.epin_cost FROM mlm_login a INNER JOIN mlm_epin_type b ON b.id=a.package_id WHERE a.userid=" + Session["UserID"] + "");

                objOther.filldropdownlist("SELECT id, CONCAT( pin_type, '-', ' Rs', epin_cost) AS epinType FROM mlm_epin_type WHERE id != 3 AND epin_cost > " + intEpinCost + " order by epin_cost ASC", ddlPackage, "epinType", "id");
            }
            else
            {
                Response.Redirect("overview.aspx");
            }
        }
    }

    protected void btnUpgrade_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlPackage.SelectedValue != "0")
            {
                double PackageCost = clsOdbc.executeScalar_dbl("SELECT epin_cost FROM mlm_epin_type WHERE id= '" + ddlPackage.SelectedValue + "'");

                double EpinCost = clsOdbc.executeScalar_dbl("SELECT epin_cost FROM mlm_epin WHERE epin='" + txtEpin.Text + "'");

                if (PackageCost == EpinCost)
                {
                    int PrevPackageID = clsOdbc.executeScalar_int("SELECT package_id FROM mlm_login WHERE userid='" + Session["UserID"] + "'");

                    lblMessage.Text = "";
                    clsOdbc.executeNonQuery("INSERT INTO `mlm_topup`(`userid`, `package_id`, `topup_amount`, `prev_package_id`, `topup_by`, `created_on`,epin) VALUES ('" + Session["UserID"] + "','" + ddlPackage.SelectedValue + "'," + EpinCost + "," + PrevPackageID + ",'" + Session["UserID"] + "','" + objWallet.getCurDateTimeString() + "','" + txtEpin.Text + "')");

                    clsOdbc.executeNonQuery("UPDATE mlm_login SET package_id=" + ddlPackage.SelectedValue + ", epin='" + txtEpin.Text + "' WHERE userid=" + Session["UserID"] + "");

                    clsOdbc.executeNonQuery("UPDATE mlm_epin SET status=0   WHERE userid=" + Session["UserID"] + " AND epin='" + txtEpin.Text + "'");
                    int intNode = clsOdbc.executeScalar_int("SELECT node_flag FROM mlm_login WHERE userid='" + Session["UserID"] + "'");

                    clsOdbc.executeNonQuery("CALL reg_updateDownlineCountPosTopup_new('" + Session["UserID"] + "', '" + intNode + "','" + ddlPackage.SelectedValue + "')");

                    //		clsOdbc.executeNonQuery("CALL Update_Awards()");					
                    string struserName, stramount, strmobileNo;
                    struserName = clsOdbc.executeScalar_str("SELECT username FROM mlm_personal_details WHERE userid = '" + Session["UserID"] + "'");
                    stramount = clsOdbc.executeScalar_str("SELECT epin_cost FROM mlm_epin WHERE epin = '" + txtEpin.Text + "'");
                    strmobileNo = clsOdbc.executeScalar_str("SELECT mobile_number FROM mlm_personal_details WHERE userid = '" + Session["UserID"] + "'");

                    string strmessage = "Congratulation Mr/Mrs " + struserName + ", Your account Has Been Upgraded With amount " + stramount + ". Thank You. www.lifegoldecom.com";

                    objcomm.SMS_API_for_Single_SMS(strmessage, strmobileNo);

                    CommonMessages.ShowAlertMessage_Reload("Member upgraded successfully!", "UpgradeMembership.aspx");
                }
                else
                {
                    lblMessage.Text = "Epin Cost not match with selected package type!";
                }
            }
            else
            {
                CommonMessages.ShowAlertMessage("Select Package type first!");
            }
        }
        catch (Exception ex)
        {
            CommonMessages.ShowAlertMessage(ex.Message);
        }
    }

    protected void ddlPackage_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPackage.SelectedValue != "0")
        {
            txtAmount.Text = clsOdbc.executeScalar_str("SELECT epin_cost FROM mlm_epin_type WHERE id= '" + ddlPackage.SelectedValue + "'");
        }
    }
    protected void txtEpin_TextChanged(object sender, EventArgs e)
    {
        try
        {
            int intEpinValid = clsOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_epin WHERE epin='" + txtEpin.Text + "' AND status=1 AND userid='" + Session["UserID"] + "'");

            if (intEpinValid == 1)
            {
                txtEpinAmt.Text = clsOdbc.executeScalar_str("SELECT epin_cost FROM mlm_epin WHERE epin='" + txtEpin.Text + "'");
                lblMessage.Text = "";
            }
            else { lblMessage.Text = "Entered Epin is not valid!"; }
        }
        catch (Exception ex)
        {
            CommonMessages.ShowAlertMessage(ex.Message);
        }
    }
}