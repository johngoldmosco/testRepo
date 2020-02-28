using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_member_PaymentConfirmation : System.Web.UI.Page
{
    ODBC objOdbc = new ODBC();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] != null)
        {
            lblWalletBalance.Text = objOdbc.executeScalar_str("SELECT wallet2 FROM mlm_my_balance_current WHERE userid='" + Session["UserID"] + "'");
            lblReqAmt.Text = Session["dblReqAmountWallet2"].ToString();

            if (lblReqAmt.Text == "")
            {
                Response.Redirect("WithdrawProfitShare.aspx");
            }
        }
        else
        {
            Response.Redirect("../../Login.aspx");
        }

    }

    protected void btnCnf_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["dblReqAmountWallet2"] == null)
            {
                Response.Redirect("WithdrawProfitShare.aspx");
            }
            else
            {
                double dblWallet2 = objOdbc.executeScalar_dbl("SELECT wallet2 FROM mlm_my_balance_current WHERE userid='" + Session["UserID"] + "'");
                double dblReqAmt = Convert.ToDouble(Session["dblReqAmountWallet2"]);
                if (dblWallet2 >= dblReqAmt)
                {
                    objOdbc.executeNonQuery("call withdrawal_request_return(" + Session["UserID"] + "," + dblReqAmt + ") "); 
                }
                else
                {
                    lblError.Text = "Request Amount is greater than available balance!";
                }
                Session["dblReqAmountWallet2"] = "";
                System.Threading.Thread.Sleep(3000);
                Response.Redirect("PayoutHistory.aspx");
            }

        }
        catch (Exception ex) { }

    }

    protected void btnReject_Click(object sender, EventArgs e)
    {
        Session["dblReqAmountWallet2"] = "";
        Response.Redirect("overview.aspx");
    }
}