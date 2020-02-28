using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_admin_WithdrawRequest : System.Web.UI.Page
{
    ODBC clsOdbc = new ODBC();
    clsWallet cswallet = new clsWallet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminID"] == null)
        {
            Response.Redirect("../../login.aspx");
        }
        /*if (Session["transID"] == null)
        {
            string v7 = HttpContext.Current.Request.Url.AbsoluteUri;

            string v8 = v7.Substring(v7.LastIndexOf("/") + 1);
            Response.Redirect("checkpoint.aspx?PID=" + v8);
        }*/

        if (!IsPostBack)
        {
            string strdt = cswallet.getCurDateString();
            DateTime dt = Convert.ToDateTime(strdt);
            string intdate = (dt.Day).ToString();
            if (intdate == "1" || intdate == "15")
            {
            }
            else
            {
                CommonMessages.ShowAlertMessage_Reload("you can withdraw amount only on 1st And 15th Day of month!", "dashboard.aspx");
            }

        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtCurBalance.Text != "")
            {
                if (Convert.ToDouble(txtRequestAmount.Text) >= 500)
                {
                    if (Convert.ToDouble(txtRequestAmount.Text) > Convert.ToDouble(txtCurBalance.Text))
                    {
                        CommonMessages.ShowAlertMessage("You can not cash Out more than available balance!");
                        txtRequestAmount.Text = "";
                    }
                    else
                    {
                        int intUserID = clsOdbc.executeScalar_int("SELECT userid FROM mlm_login WHERE my_sponsar_id = '" + txtUserID.Text + "'");
                        if (intUserID > 1)
                        {
                            if (ddlWallet.SelectedValue != "Select")
                            {
                                if (ddlWallet.SelectedValue == "1")
                                {
                                    clsOdbc.executeNonQuery("call withdrawal_request_income(" + intUserID + "," + Convert.ToDouble(txtRequestAmount.Text) + "," + Session["AdminID"] + ")");
                                }

                                if (ddlWallet.SelectedValue == "2")
                                {
                                    clsOdbc.executeNonQuery("call withdrawal_request_return(" + intUserID + "," + Convert.ToDouble(txtRequestAmount.Text) + "," + Session["AdminID"] + ")");
                                }

                                CommonMessages.ShowAlertMessage_Reload("Cash Out Successfully Submitted!", "overview.aspx");
                            }
                            else
                            {
                                CommonMessages.ShowAlertMessage("Entered User ID does not Exist!");
                                txtUserName.Text = "";
                                txtUserID.Text = "";
                                txtUserID.Focus();
                            }
                        }
                    }
                }
                else
                {
                    CommonMessages.ShowAlertMessage("Kindly Enter Withdrawal amount greater than Rs 500 !");
                    txtRequestAmount.Text = "0";
                    txtRequestAmount.Focus();
                }
            }
            else
            {
                CommonMessages.ShowAlertMessage("You Dont Have a Sufficient balance for withdrawal!");
            }
        }
        catch (Exception ex) { }
    }
    protected void txtUserID_TextChanged(object sender, EventArgs e)
    {
        if (txtUserID.Text != "")
        {
            int intCount = clsOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_login WHERE my_sponsar_id ='"+ txtUserID.Text +"' AND status=1 AND Active =1 ");
            if (intCount == 1)
            {
                txtUserName.Text = clsOdbc.executeScalar_str("SELECT a.username FROM mlm_personal_details a INNER JOIN mlm_login b ON a.userid=b.userid WHERE b.my_sponsar_id ='" + txtUserID.Text + "'");

                int intUserID = clsOdbc.executeScalar_int("SELECT userid FROM mlm_login WHERE my_sponsar_id ='" + txtUserID.Text + "' ");

                int intKycStatus = clsOdbc.executeScalar_int("SELECT kyc_status FROM `mlm_kyc_documents` WHERE userid=" + intUserID);
                if (intKycStatus == 0)
                {
                    CommonMessages.ShowAlertMessage_Reload("Please ask user to upload kyc documents!", "WithdrawRequest.aspx");
                }
            }
            else
            {
                CommonMessages.ShowAlertMessage("Entered User ID does not Exist!");
                txtUserName.Text = "";
                txtUserID.Text = "";
                txtUserID.Focus();
            }
        }
    }
    protected void ddlWallet_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlWallet.SelectedValue !="Select")
        {
            if (ddlWallet.SelectedValue=="1")
            {
                int intUserID = clsOdbc.executeScalar_int("SELECT userid FROM mlm_login WHERE my_sponsar_id = '" + txtUserID.Text + "'");

                int intBlncCount = clsOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_my_balance_current Where userid=" + intUserID);

                if (intBlncCount == 1)
                    txtCurBalance.Text = clsOdbc.executeScalar_str("SELECT wallet1 From mlm_my_balance_current Where userid=" + intUserID);
                else
                    txtCurBalance.Text = "0";
            }

            if (ddlWallet.SelectedValue =="2")
            {
                int intUserID = clsOdbc.executeScalar_int("SELECT userid FROM mlm_login WHERE my_sponsar_id = '" + txtUserID.Text + "'");

                int intBlncCount = clsOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_my_balance_current Where userid=" + intUserID);

                if (intBlncCount == 1)
                    txtCurBalance.Text = clsOdbc.executeScalar_str("SELECT wallet2 From mlm_my_balance_current Where userid=" + intUserID);
                else
                    txtCurBalance.Text = "0";
            }
        }
    }
}