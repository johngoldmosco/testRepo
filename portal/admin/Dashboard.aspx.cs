using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_admin_Dashboard : System.Web.UI.Page
{
    ODBC clsOdbc = new ODBC();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminID"] == null)
        {
            Response.Redirect("../../login.aspx");
        }

        if (!IsPostBack)
        {
            lblBinary.Text = clsOdbc.executeScalar_str("SELECT COALESCE(sum(amount_given),0) FROM `mlm_binary_daily_payout` WHERE 1");
            lblReferral.Text = clsOdbc.executeScalar_str("SELECT COALESCE(sum(amt),0) FROM `mlm_direct_income` WHERE 1");
            lblSponsorInc.Text = clsOdbc.executeScalar_str("SELECT COALESCE(sum(net_amount),0) FROM `mlm_sponsor_income` WHERE 1");
            lblProfitShare.Text = clsOdbc.executeScalar_str("SELECT COALESCE(sum(amt_comm),0) FROM `mlm_profit_share` WHERE 1");

            lblReceivedAmount.Text = clsOdbc.executeScalar_str("SELECT COALESCE(sum(recieved_amount),0) FROM `mlm_my_balance` WHERE 1");

            lblRequestAmount.Text = clsOdbc.executeScalar_str("SELECT COALESCE(sum(request_amount),0) FROM `mlm_my_balance` WHERE 1");

            lblTodaysBinary.Text = clsOdbc.executeScalar_str("SELECT COALESCE(sum(amount_given),0) FROM `mlm_binary_daily_payout` WHERE  DATE(binary_date)=DATE(NOW())");

            lblTotalBinary.Text = clsOdbc.executeScalar_str("SELECT COALESCE(sum(amount_given),0) FROM `mlm_binary_daily_payout` WHERE 1");

            lblFullName.Text = clsOdbc.executeScalar_str("SELECT username FROM mlm_personal_details WHERE userid='" + Session["AdminID"] + "'");

            lblTotalMember.Text = clsOdbc.executeScalar_str("SELECT COUNT(1) FROM mlm_login WHERE Active = 1 AND UserTypeId = 2");

            lblActiveMembres.Text = clsOdbc.executeScalar_str("SELECT COUNT(1) FROM mlm_login WHERE product_status = 1 AND status = 1 AND UserTypeId = 2");

            lblPendingMember.Text = clsOdbc.executeScalar_str("SELECT COUNT(1) FROM mlm_login WHERE product_status = 0 AND status = 1 AND UserTypeId = 2");

            lblTodaysJoining.Text = clsOdbc.executeScalar_str("SELECT COUNT(1) FROM mlm_login WHERE DATE(created_on) = DATE(now())");
        }
    }
}