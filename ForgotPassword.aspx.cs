using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ForgotPassword : System.Web.UI.Page
{
    ODBC objOdbc = new ODBC();
    clsCommunication objComm = new clsCommunication();

    clsMailTemplate objMailTep = new clsMailTemplate();     
    sendMail objMail = new sendMail();
    clsCommon objwall = new clsCommon();

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnGetPassword_Click(object sender, EventArgs e)
    {
        try
        {
            int intCount = objOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_login WHERE my_sponsar_id ='" + txtUserID.Text + "'");
            if (intCount == 1)
            {
                int intCount1 = objOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_login a INNER JOIN mlm_personal_details b ON a.userid=b.userid WHERE a.my_sponsar_id ='" + txtUserID.Text + "' AND b.mobile_number='" + txtMobile.Text + "' ");
                if (intCount1 == 1)
                {
                    DataSet ds = new DataSet();

                    ds = objOdbc.getDataSet("SELECT  a.password, a.trans_pwd,b.username, b.email,b.mobile_number  FROM mlm_login a, mlm_personal_details b WHERE a.userid=b.userid AND a.my_sponsar_id ='" + txtUserID.Text + "'");

                    if ((ds.Tables[0].Rows.Count > 0))
                    {
                        string strUserID = txtUserID.Text;
                        string strPassword = ds.Tables[0].Rows[0][0].ToString();
                        string strTransPassword = ds.Tables[0].Rows[0][1].ToString();
                        string strUserName = ds.Tables[0].Rows[0][2].ToString();
                        string strEmailID = ds.Tables[0].Rows[0][3].ToString();

                        string strMessage = "Hello " + strUserName + ", Password: " + strPassword + " Trans. Password: " + strTransPassword + ",  LifeGold.com";
                        string strNumber = ds.Tables[0].Rows[0][4].ToString();

                        objComm.SMS_API_for_Single_SMS(strMessage, strNumber);
                        send_mail();

                        CommonMessages.ShowAlertMessage_Reload("Credentials are sent on your registerd mobile no. and email", "Login.aspx");
                    }
                }
                else
                {
                    CommonMessages.ShowAlertMessage("Entered UserID does not match with entered mobile no. Please kindly enter registered mobile no.!");
                }
            }
            else
            {
                CommonMessages.ShowAlertMessage("entered UserID does not exist!");
            }
        }
        catch (Exception ex)
        {
            CommonMessages.ShowAlertMessage(ex.Message);
        }
    }
    protected void send_mail()
    {
        try
        {
            DataSet ds = new DataSet();

            ds = objOdbc.getDataSet("Select a.my_sponsar_id, a.password, a.trans_pwd, b.username, b.email from mlm_login a,mlm_personal_details b where a.userid=b.userid AND a.my_sponsar_id='" + txtUserID.Text + "'");
            if (ds.Tables[0].Rows.Count > 0)
            {
            }

            string strEmailID = ds.Tables[0].Rows[0][4].ToString();
            string strPassword = ds.Tables[0].Rows[0][1].ToString();
            string strUserName = ds.Tables[0].Rows[0][3].ToString();
            string strMailBody = getVerificationOTPMail(strUserName, strPassword);

            objMail.sendMailSendGrid(strEmailID, strUserName, "info@LifeGold.com", "LifeGold", "Your Password is", strMailBody);

        }
        catch (Exception ex)
        {
        }
    }

    protected string getVerificationOTPMail(string strUsername, string strPassword)
    {
        string strMailString = System.IO.File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("/Template/for_pass.html"));

        strMailString = strMailString.Replace("&&strUserName&&", strUsername);
        strMailString = strMailString.Replace("&&strpassword&&", strPassword);
        return strMailString;
    }
}