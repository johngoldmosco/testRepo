using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_member_AccountDetails : System.Web.UI.Page
{
    ODBC clsOdbc = new ODBC();
    clsCommunication objComm = new clsCommunication();
    clsCommon objCommon = new clsCommon();
    clsWallet objWallet = new clsWallet();
    sendMail objMail = new sendMail();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == "")
        {
            Response.Redirect("../../login.aspx");
        }
        if (!IsPostBack)
        {
            FillDetails();
            txtUserID.Text = clsOdbc.executeScalar_str("SELECT my_sponsar_id FROM mlm_login WHERE userid='"+ Session["UserID"] +"'");
            txtUserName.Text = clsOdbc.executeScalar_str("SELECT username FROM mlm_personal_details WHERE userid='" + Session["UserID"] + "'");
        }
    }    
  
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            clsOdbc.executeNonQuery("UPDATE `mlm_bank_account_details` SET `account_number`='" + txtAccount.Text + "',`payee_name`='" + txtPayeeName.Text + "',`bank_name`='" + txtBankName.Text + "',`branch_name`='" + txtBranchName.Text + "',`ifsc_code`='" + txtIFSC.Text + "',`micr_code`='" + txtMICR.Text + "',`branch_code`='" + txtBranchCode.Text + "',`address`='" + txtAddress.Text + "',`status`=1  WHERE userid='" + Session["UserID"] + "'");
            CommonMessages.ShowAlertMessage_Reload("Account Updated Successfully!","AccountDetails.aspx");
        }
        catch(Exception ex)
        {

        }
    }

    private void FillDetails()
    {
        DataTable dt = new DataTable();
        dt = clsOdbc.getDataTable("SELECT a.userid, b.my_sponsar_id, c.username, a.account_number, a.payee_name, c.mobile_number, c.pancard, a.bank_name, a.branch_name, a.city, a.ifsc_code, a.micr_code, a.branch_code, a.address, a.status FROM mlm_bank_account_details a INNER JOIN mlm_login b ON a.userid=b.userid INNER JOIN  mlm_personal_details c ON a.userid=c.userid WHERE a.userid='" + Session["UserID"] + "'");

        if(dt.Rows.Count > 0 )
        {
            txtUserID.Text = dt.Rows[0][1].ToString();
            txtUserName.Text = dt.Rows[0][2].ToString();
            txtPayeeName.Text = dt.Rows[0][4].ToString();
            txtAccount.Text = dt.Rows[0][3].ToString();
            txtPan.Text = dt.Rows[0][6].ToString();
            txtMobile.Text = dt.Rows[0][5].ToString();
            txtBankName.Text = dt.Rows[0][7].ToString();
            txtBranchName.Text = dt.Rows[0][8].ToString();
            txtIFSC.Text = dt.Rows[0][10].ToString();
            txtBranchCode.Text = dt.Rows[0][12].ToString();
            txtMICR.Text = dt.Rows[0][11].ToString();
            txtAddress.Text =  dt.Rows[0][13].ToString();

            int intFillStatus = int.Parse(dt.Rows[0][14].ToString());
            if(intFillStatus==1)
            {
                btnSubmit.Enabled = false;
            }
        }
    }    
}