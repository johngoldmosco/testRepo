using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_admin_SendSms : System.Web.UI.Page
{
    ODBC objOdbc = new ODBC();
    clsCommunication objcomm = new clsCommunication();

    protected void Page_Load(object sender, EventArgs e)
    {
        lblSMScnt.Text = objOdbc.executeScalar_str("SELECT sms_count FROM tbl_smscount WHERE 1");
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string strmobileNo = "", strmessage = "";

        if (chkAll.Checked)
        {
            DataTable dt = new DataTable();
            int cnt = objOdbc.executeScalar_int("SELECT COUNT(*) FROM mlm_personal_details WHERE userid>1");
            dt = objOdbc.getDataTable("SELECT mobile_number FROM mlm_personal_details WHERE userid>1");

            for (int i = 0; i < cnt; i++)
            {
                strmessage = txtMessage.Text;
                strmobileNo = dt.Rows[i][0].ToString();
                objcomm.SMS_API_for_Single_SMS(strmessage, strmobileNo);
            }
        }
        else
        {
            try
            {
                strmobileNo = txtMobile.Text;
                strmessage = txtMessage.Text;
                objcomm.SMS_API_for_Single_SMS(strmessage, strmobileNo);
            }
            catch (Exception ex)
            {
                CommonMessages.ShowAlertMessage(ex.Message);
            }
        }
    }
    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        if (chkAll.Checked)
        {
            txtMobile.Enabled = false;
        }
        else
        {
            txtMobile.Enabled = true;
        }
    }
}