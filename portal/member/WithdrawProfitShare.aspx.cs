using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_member_WithdrawProfitShare : System.Web.UI.Page
{
    ODBC clsOdbc = new ODBC();
    clsWallet cswallet = new clsWallet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Response.Redirect("../../login.aspx");
        }
        if (Session["transID"] == null)
        {
            string v7 = HttpContext.Current.Request.Url.AbsoluteUri;

            string v8 = v7.Substring(v7.LastIndexOf("/") + 1);
            Response.Redirect("checkpoint.aspx?PID=" + v8);
        }

        if (!IsPostBack)
        {
            string strdt = cswallet.getCurDateString();
            DateTime dt = Convert.ToDateTime(strdt);
            string intdate = (dt.Day).ToString();
            Session["dblReqAmountWallet2"] = "";
            if (dt.DayOfWeek != DayOfWeek.Sunday || dt.DayOfWeek != DayOfWeek.Saturday)
            //   if (intdate == "1" || intdate == "15")
            {

                int intBlncCount = clsOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_my_balance_current WHERE userid=" + Session["UserID"]);

                if (intBlncCount == 1)
                    txtCurBalance.Text = clsOdbc.executeScalar_str("SELECT wallet2 FROM mlm_my_balance_current WHERE userid=" + Session["UserID"]);
                else
                    txtCurBalance.Text = "0";
            }
            else
            {
                CommonMessages.ShowAlertMessage_Reload("You can withdraw amount only within monday to friday!", "overview.aspx");
            }

            int intBankStatus = clsOdbc.executeScalar_int("SELECT status FROM `mlm_bank_account_details` WHERE userid=" + Session["UserID"]);

            if (intBankStatus == 0)
            {
                CommonMessages.ShowAlertMessage_Reload("Please update bank details! If you had already uploaded then contact to administrator!", "AccountDetails.aspx");
            }

            int intKycStatus = clsOdbc.executeScalar_int("SELECT kyc_status FROM `mlm_kyc_documents` WHERE userid=" + Session["UserID"]);
            //if (intKycStatus == 0)
            //{
            //    CommonMessages.ShowAlertMessage_Reload("Please upload kyc documents! If you had already uploaded then contact to administrator!", "KYCuploads.aspx");
            //}

        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtCurBalance.Text != "")
            {
                double dblMinWith = clsOdbc.executeScalar_dbl("SELECT min_with FROM mlm_admin_setting WHERE id=1");
                double dblMaxWith = clsOdbc.executeScalar_dbl("SELECT max_with FROM mlm_admin_setting WHERE id=1");
                if (Convert.ToDouble(txtRequestAmount.Text) >= dblMinWith)
                {
                    if (Convert.ToDouble(txtRequestAmount.Text) <= dblMaxWith)
                    {
                        if (Convert.ToDouble(txtRequestAmount.Text) > Convert.ToDouble(txtCurBalance.Text))
                        {
                            CommonMessages.ShowAlertMessage("You can not cash Out more than available balance!");
                            txtRequestAmount.Text = "";
                        }
                        else
                        {
                            Session["dblReqAmountWallet2"] = txtRequestAmount.Text.Trim();
                            Response.Redirect("PaymentConfirmation.aspx");                        
                        }
                    }
                    else
                    {
                        lblError.Text = "Kindly Enter Withdrawal amount less than " + dblMaxWith + " !";
                        CommonMessages.ShowAlertMessage("Kindly Enter Withdrawal amount less than '" + dblMaxWith + "' !");
                        txtRequestAmount.Text = "0";
                        txtRequestAmount.Focus();
                    }
                }
                else
                {
                    lblError.Text = "Kindly Enter Withdrawal amount greater than Rs " + dblMinWith + " !";
                    CommonMessages.ShowAlertMessage("Kindly Enter Withdrawal amount greater than Rs " + dblMinWith + " !");
                    txtRequestAmount.Text = "0";
                    txtRequestAmount.Focus();
                }
                System.Threading.Thread.Sleep(3000);
            }
            else
            {
                CommonMessages.ShowAlertMessage("You Dont Have a Sufficient balance for withdrawal!");
            }
        }
        catch (Exception ex) { }
    }
}