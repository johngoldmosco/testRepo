using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_admin_CreditDebitWallet : System.Web.UI.Page
{
    ODBC clsOdbc = new ODBC();
    clsWallet objwallet = new clsWallet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminID"] == null)
        {
            Response.Redirect("../../login.aspx");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            double dblAmount = double.Parse(txtAmount.Text);
            string operat = "";
            if (ddlTransactionType.SelectedValue == "1")
                operat = "+";
            if (ddlTransactionType.SelectedValue == "2")
                operat = "-";

            int valid = clsOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_login WHERE my_sponsar_id='" + txtMemberID.Text + "'");
            if (valid == 1)
            {
                int intuserid = clsOdbc.executeScalar_int("SELECT userid FROM mlm_login WHERE my_sponsar_id='" + txtMemberID.Text + "'");

                clsOdbc.executeNonQuery("INSERT INTO mlm_add_deduct_wallet(userid, transaction_type, wallet_type, amount, created_on) VALUES(" + intuserid + "," + ddlTransactionType.SelectedValue + ", '" + ddlWalletType.SelectedValue + "'," + dblAmount + ", '" + objwallet.getCurDateTimeString() + "')");

                int rcount = clsOdbc.executeScalar_int("SELECT Count(1) FROM mlm_my_balance_current WHERE userid=" + intuserid);

                if (rcount == 1)
                {
                    if (ddlWalletType.SelectedValue == "1")
                    {
                        clsOdbc.executeNonQuery("UPDATE mlm_my_balance_current SET wallet1 = wallet1 " + operat + dblAmount + " , balance_amount=balance_amount " + operat + dblAmount + " , total_balance=total_balance" + operat + dblAmount + " WHERE userid=" + intuserid);
                    }
                    if (ddlWalletType.SelectedValue == "2")
                    {
                        clsOdbc.executeNonQuery("UPDATE mlm_my_balance_current SET wallet2 = wallet2 " + operat + dblAmount + " , balance_amount=balance_amount " + operat + dblAmount + " , total_balance=total_balance" + operat + dblAmount + " WHERE userid=" + intuserid);
                    }
                }
                else
                {
                    if (ddlWalletType.SelectedValue == "1")
                    {
                        clsOdbc.executeNonQuery("INSERT INTO mlm_my_balance_current (userid, wallet1, balance_amount, total_balance ) VALUES(" + intuserid + "," + dblAmount + "," + dblAmount + "," + dblAmount + ") ");
                    }
                    if (ddlWalletType.SelectedValue == "2")
                    {
                        clsOdbc.executeNonQuery("INSERT INTO mlm_my_balance_current (userid, wallet2, balance_amount, total_balance ) VALUES(" + intuserid + "," + dblAmount + "," + dblAmount + "," + dblAmount + ") ");
                    }
                }

                int rwcount = clsOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_my_balance WHERE userid=" + intuserid);

                if (rwcount == 1)
                {
                    if (ddlWalletType.SelectedValue == "1")
                    {
                        clsOdbc.executeNonQuery("UPDATE mlm_my_balance SET wallet1 = wallet1 " + operat + dblAmount + " , balance_amount = balance_amount" + operat + dblAmount + " , total_balance = total_balance" + operat + dblAmount + " WHERE userid=" + intuserid);
                    }
                    if (ddlWalletType.SelectedValue == "2")
                    {
                        clsOdbc.executeNonQuery("UPDATE mlm_my_balance SET wallet2 = wallet2 " + operat + dblAmount + " , balance_amount = balance_amount" + operat + dblAmount + " , total_balance = total_balance" + operat + dblAmount + " WHERE userid=" + intuserid);
                    }
                }
                else
                {
                    if (ddlWalletType.SelectedValue == "1")
                    {
                        clsOdbc.executeNonQuery("INSERT INTO mlm_my_balance (userid, wallet1, balance_amount, total_balance ) VALUES(" + intuserid + "," + dblAmount + "," + dblAmount + "," + dblAmount + ")");
                    }
                    if (ddlWalletType.SelectedValue == "2")
                    {
                        clsOdbc.executeNonQuery("INSERT INTO mlm_my_balance (userid, wallet2, balance_amount, total_balance ) VALUES(" + intuserid + "," + dblAmount + "," + dblAmount + "," + dblAmount + ")");
                    }

                }
                CommonMessages.ShowAlertMessage_Reload("Transaction Successed", "CreditDebitWallet.aspx");
            }
            else
                CommonMessages.ShowAlertMessage("Please enter valid userid");
        }
        catch (Exception ex)
        {
        }
    }

    protected void txtMemberID_TextChanged(object sender, EventArgs e)
    {       
        try
        {
            int intCount = clsOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_login WHERE my_sponsar_id='" + txtMemberID.Text + "' AND Active=1 AND status=1");
            if (intCount == 1)
            { 
                txtUserName.Text = clsOdbc.executeScalar_str("SELECT a.username FROM mlm_personal_details a INNER JOIN mlm_login b ON a.userid=b.userid WHERE b.my_sponsar_id='" + txtMemberID.Text + "'");
                if (ddlWalletType.SelectedValue == "1")
                {
                    txtCurrentBalance.Text = clsOdbc.executeScalar_str("SELECT a.wallet1 FROM mlm_my_balance_current a INNER JOIN mlm_login b ON a.userid=b.userid WHERE b.my_sponsar_id='" + txtMemberID.Text + "'");
                }
                else if (ddlWalletType.SelectedValue == "2")
                {
                    txtCurrentBalance.Text = clsOdbc.executeScalar_str("SELECT a.wallet2 FROM mlm_my_balance_current a INNER JOIN mlm_login b ON a.userid=b.userid WHERE b.my_sponsar_id='" + txtMemberID.Text + "'");
                }
                else {
                    txtCurrentBalance.Text = "Select Wallet type First";
                    txtMemberID.Text = "";
                    txtUserName.Text = "";
                    ddlWalletType.Focus();
                }
            }
            else
            {
                txtMemberID.Text = "";
                txtMemberID.Attributes.Add("placeholder", "Invalid Member ID !");
                txtMemberID.Focus();
            }
        }
        catch (Exception ex) { CommonMessages.ShowAlertMessage(ex.Message); }
    }
}