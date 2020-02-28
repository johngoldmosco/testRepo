using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Welcome : System.Web.UI.Page
{
    ODBC objOdbc = new ODBC();
    clsCommunication objcomm = new clsCommunication();

    clsMailTemplate objMailTep = new clsMailTemplate();
    sendMail objMail = new sendMail();
    clsCommon objwall = new clsCommon();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
          //  Session["WelcomeID"] = 2;
            if (Session["WelcomeID"] != null)
            {
                DataSet ds = new DataSet();
                try
                {
                    ds = objOdbc.getDataSet("Select b.username, a.my_sponsar_id, a.password, a.trans_pwd, b.email, b.mobile_number From mlm_login a, mlm_personal_details b WHERE a.userid=b.userid AND a.userid =" + Session["WelcomeID"]);

                    if ((ds.Tables[0].Rows.Count > 0))
                    {
                        txtUserName.Text = "User Name : " + ds.Tables[0].Rows[0][0].ToString();
                        txtUserID.Text = "User ID : " + ds.Tables[0].Rows[0][1].ToString();
                        txtPassword.Text = "Password : " + ds.Tables[0].Rows[0][2].ToString();
                        txtTransPwd.Text = "Transaction Password : " + ds.Tables[0].Rows[0][3].ToString();
                        string strMobileNumber = ds.Tables[0].Rows[0][5].ToString();

                        string strmessage = "Welcome to LifeGold ! Your " + txtUserID.Text + ", " + txtPassword.Text + ", " + txtTransPwd.Text + ". www.LifeGold.com";

                        objcomm.SMS_API_for_Single_SMS(strmessage, strMobileNumber);
                        send_mail();

                    }
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    ds.Dispose();
                }
            }
        }
    }

    protected void btnSignIn_Click(object sender, EventArgs e)
    {
        Response.Redirect("Login.aspx");

    }

    protected void send_mail()
    {
        try
        {
            DataSet ds = new DataSet();

            ds = objOdbc.getDataSet("Select a.my_sponsar_id, a.password, a.trans_pwd, b.username, b.email from mlm_login a,mlm_personal_details b where a.userid=b.userid AND a.userid =" + Session["WelcomeID"] + "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                string strUserID = ds.Tables[0].Rows[0][0].ToString();
                string strPassword = ds.Tables[0].Rows[0][1].ToString();
                string strTPassword = ds.Tables[0].Rows[0][2].ToString();
                string strUserName = ds.Tables[0].Rows[0][3].ToString();
                string strEmailID = ds.Tables[0].Rows[0][4].ToString();

                string strMailBody = getVerificationOTPMail(strUserID, strPassword, strTPassword, strUserName, strEmailID);
                objMail.sendMailSendGrid(strEmailID, strUserName, "hr@LifeGold.com", "LifeGold", "LifeGold Registration", strMailBody);
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected string getVerificationOTPMail(string strUserID, string strPass, string strTPass, string strUsername, string strEmail)
    {
        string strMailString = System.IO.File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("/Template/register.html"));
        strMailString = strMailString.Replace("&&strUserID&&", strUserID);
        strMailString = strMailString.Replace("&&pass&&", strPass);
        strMailString = strMailString.Replace("&&transPass&&", strTPass);
        strMailString = strMailString.Replace("&&username&&", strUsername);
        strMailString = strMailString.Replace("&&email&&", strEmail);
        return strMailString;
    }


}