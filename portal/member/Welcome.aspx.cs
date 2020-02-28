using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_member_Welcome : System.Web.UI.Page
{
    ODBC clsOdbc = new ODBC();
    sendMail objMail = new sendMail();
    clsCommunication objComm = new clsCommunication();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["WelcomeID"] != null)
            {
                DataSet ds = new DataSet();
                try
                {
                    ds = clsOdbc.getDataSet("Select b.username, a.my_sponsar_id, a.password, a.trans_pwd, b.email From mlm_login a, mlm_personal_details b WHERE a.userid=b.userid AND a.userid =" + Session["WelcomeID"]);

                    if ((ds.Tables[0].Rows.Count > 0))
                    {
                        txtUserName.Text = "User Name : " + ds.Tables[0].Rows[0][0].ToString();
                        txtUserID.Text = "User ID : " + ds.Tables[0].Rows[0][1].ToString();
                        txtPassword.Text = "Password : " + ds.Tables[0].Rows[0][2].ToString();
                        txtTransactionPwd.Text = "Transaction Password : " + ds.Tables[0].Rows[0][3].ToString();

                        string strUserID = ds.Tables[0].Rows[0][1].ToString();
                        string strTransPassword = ds.Tables[0].Rows[0][3].ToString();
                        string strPassword = ds.Tables[0].Rows[0][2].ToString();
                        string strUserName = ds.Tables[0].Rows[0][0].ToString();
                        string strEmailID = ds.Tables[0].Rows[0][4].ToString();

                        string strMailBody = getVerificationOTPMail(strUserID, strUserName, strPassword, strTransPassword, strEmailID);
                        objMail.sendMailSendGrid(strEmailID, strUserName, "admin@Zuricplus.com", "Zuricplus", "Welcome to Zuricplus", strMailBody);

                        string strMessage = "Welcome to Zuricplus.com UserID= " + strUserID + " UserName= " + strUserName + " Password= " + strPassword + " T Password= " + strTransPassword + ". ";
                        string strNumber = clsOdbc.executeScalar_str("SELECT mobile_number FROM mlm_personal_details WHERE userid='" + Session["WelcomeID"] + "'"); ;
                        objComm.SMS_API_for_Single_SMS(strMessage, strNumber);
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
    protected string getVerificationOTPMail(string strUserID, string strUsername, string strPassword, string strTransPassword, string strEmailID)
    {
        string strMailString = System.IO.File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("/Template/register.html"));
        strMailString = strMailString.Replace("&&strUserID&&", strUserID);
        strMailString = strMailString.Replace("&&pass&&", strPassword);
        strMailString = strMailString.Replace("&&transPass&&", strTransPassword);
        strMailString = strMailString.Replace("&&username&&", strUsername);
        strMailString = strMailString.Replace("&&email&&", strEmailID);

        return strMailString;
    }
    protected void lnkbtnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("NewMember.aspx");
    }
}