using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_member_overview : System.Web.UI.Page
{
    ODBC clsOdbc = new ODBC();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                string strUserID = Request.QueryString["UID"];
                if (strUserID != null)
                {
                    Session["UserID"] = strUserID;
                }
                lblReceivedAmount.Text = clsOdbc.executeScalar_str("SELECT recieved_amount FROM `mlm_my_balance` where userid='" + Session["UserID"] + "'");
                lblRequestAmount.Text = clsOdbc.executeScalar_str("SELECT request_amount FROM `mlm_my_balance` where userid='" + Session["UserID"] + "'");

                lblTodaysBinary.Text = clsOdbc.executeScalar_str("SELECT COALESCE(sum(amount_given),0) FROM `mlm_binary_daily_payout` WHERE userid='" + Session["UserID"] + "' AND DATE(binary_date)=DATE(NOW()) AND id>9");

                lblTotalBinary.Text = clsOdbc.executeScalar_str("SELECT COALESCE(sum(amount_given),0) FROM `mlm_binary_daily_payout` WHERE userid='" + Session["UserID"] + "' AND id>9");

                lblFullName.Text = clsOdbc.executeScalar_str("SELECT username FROM mlm_personal_details WHERE userid='" + Session["UserID"] + "'");

                lblDirectMember.Text = clsOdbc.executeScalar_str("SELECT total_direct_members FROM mlm_progress_count_current WHERE userid='" + Session["UserID"] + "'");

                lblDownlineMember.Text = clsOdbc.executeScalar_str("SELECT total_down_members FROM mlm_progress_count_current WHERE userid='" + Session["UserID"] + "'");

                lblActiveDirect.Text = clsOdbc.executeScalar_str("SELECT active_direct_members FROM mlm_progress_count_current WHERE userid='" + Session["UserID"] + "'");

                lblActiveDownline.Text = clsOdbc.executeScalar_str("SELECT active_down_members FROM mlm_progress_count_current WHERE userid='" + Session["UserID"] + "'");

                lblBinary.Text = clsOdbc.executeScalar_str("SELECT  COALESCE(sum(amount_given),0) FROM `mlm_binary_daily_payout` WHERE userid='" + Session["UserID"] + "' AND id>9 ");

                lblReferral.Text = clsOdbc.executeScalar_str("SELECT  COALESCE(sum(amt),0) FROM `mlm_direct_income` WHERE userid='" + Session["UserID"] + "' ");

                lblSponsorInc.Text = clsOdbc.executeScalar_str("SELECT  COALESCE(sum(net_amount),0) FROM `mlm_sponsor_income` WHERE userid='" + Session["UserID"] + "' ");

                lblProfitShare.Text = clsOdbc.executeScalar_str("SELECT  COALESCE(sum(amt_comm),0) FROM `mlm_profit_share` WHERE userid='" + Session["UserID"] + "' ");

                lblAward.Text = clsOdbc.executeScalar_str("SELECT b.award_name FROM mlm_login a INNER JOIN mlm_award_structure b ON a.award_id=b.id WHERE a.userid='" + Session["UserID"] + "' ");

                lblWallet1.Text = clsOdbc.executeScalar_str("SELECT wallet1 FROM mlm_my_balance_current WHERE userid='" + Session["UserID"] + "'");
                lblWallet2.Text = clsOdbc.executeScalar_str("SELECT wallet2 FROM mlm_my_balance_current WHERE userid='" + Session["UserID"] + "'");

                int intRank = clsOdbc.executeScalar_int("SELECT a.rank_id FROM mlm_login a WHERE a.userid='" + Session["UserID"] + "' ");
                if (intRank == 0)
                {
                    lblRank.Text = "None";
                }
                else
                    lblRank.Text = clsOdbc.executeScalar_str("SELECT b.rank_name FROM mlm_login a INNER JOIN mlm_rank_structure b ON a.rank_id=b.id WHERE a.userid='" + Session["UserID"] + "' ");

                lblTotalLeftDirect.Text = clsOdbc.executeScalar_str("SELECT direct_left FROM mlm_progress_count_current  WHERE  userid='" + Session["UserID"] + "' ");
                lblTotalRightDirect.Text = clsOdbc.executeScalar_str("SELECT direct_right FROM mlm_progress_count_current  WHERE  userid='" + Session["UserID"] + "' ");
                lblTotalLeftDownline.Text = clsOdbc.executeScalar_str("SELECT l_members FROM mlm_progress_count_current  WHERE  userid='" + Session["UserID"] + "' ");
                lblTotalRightDownline.Text = clsOdbc.executeScalar_str("SELECT r_members FROM mlm_progress_count_current  WHERE  userid='" + Session["UserID"] + "' ");

                lblLeftRefLink.Text = "http://lifegoldecom.com/register.aspx?pos=L&pid=" + Session["UserID"] + "";
                lblRightRefLink.Text = "http://lifegoldecom.com/register.aspx?pos=R&pid=" + Session["UserID"] + "";

                int intCount = clsOdbc.executeScalar_int("SELECT product_status FROM mlm_login WHERE userid='" + Session["UserID"] + "'");

                if (intCount == 0)
                {
                    lblActivestatus.ForeColor = System.Drawing.Color.Red;
                    lblActivestatus.Text = "Your membership is not Activated";
                    divActive.Attributes.Add("class", "breadcrumb bg-danger");
                }
                else if (intCount == 1)
                {
                    string strPackage = clsOdbc.executeScalar_str("SELECT CONCAT(CONCAT('Package ', a.package_id), ' - ' , b.pin_type) AS Package FROM mlm_login a INNER JOIN mlm_epin_type b ON a.package_id=b.id WHERE userid='" + Session["UserID"] + "'");

                    lblActivestatus.ForeColor = System.Drawing.Color.White;
                    lblActivestatus.Text = "Your are Activated Member. " + strPackage + "";
                    divActive.Attributes.Add("class", "breadcrumb bg-success");
                }

                int intPopupStatus = clsOdbc.executeScalar_int("SELECT display_status FROM tbl_popup WHERE id=2");
                if (intPopupStatus == 1)
                {
                    int intPopupType = clsOdbc.executeScalar_int("SELECT popup_type FROM tbl_popup WHERE id=2");
                    if (intPopupType == 1)
                    {
                        lblPopupHeader.Text = clsOdbc.executeScalar_str("SELECT popup_header FROM tbl_popup WHERE id=2");
                        lblPopupBody.Text = clsOdbc.executeScalar_str("SELECT content FROM tbl_popup WHERE id=2");
                        imgPopup.Visible = false;
                    }
                    else if (intPopupType == 2)
                    {
                        lblPopupHeader.Text = clsOdbc.executeScalar_str("SELECT popup_header FROM tbl_popup WHERE id=2");
                        imgPopup.ImageUrl = clsOdbc.executeScalar_str("SELECT imgUrl FROM tbl_popup WHERE id=2");
                        lblPopupBody.Visible = false;
                    }
                    ModalPopupExtender1.Show();

                }
                if (intPopupStatus == 2)
                {
                    ModalPopupExtender1.Hide();
                }

                lblBinaryTime.Text = clsOdbc.executeScalar_str("SELECT created_on FROM `mlm_schedulers` WHERE schedule_task='Binary Income' ORDER BY id DESC LIMIT 1");
                lblProfitShareTime.Text = clsOdbc.executeScalar_str("SELECT created_on FROM `mlm_schedulers` WHERE schedule_task='Profit Share' ORDER BY id DESC LIMIT 1");

            }
            catch (Exception ex)
            {
            }
        }
        if (Session["UserID"] == null)
        {
            Response.Redirect("../../login.aspx");
        }
    }
}