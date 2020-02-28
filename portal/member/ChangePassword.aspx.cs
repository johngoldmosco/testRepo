using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_member_ChangePassword : System.Web.UI.Page
{
    ODBC clsodbc = new ODBC();
    clsCommunication objcommunication = new clsCommunication();
    clsMailTemplate cstemp = new clsMailTemplate();
    sendMail objMail = new sendMail();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Response.Redirect("../../login.aspx");
        }

        if (!IsPostBack)
        {

        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            var checkpwd = clsodbc.executeScalar_str("SELECT password FROM mlm_login WHERE userid='" + Session["UserID"] + "'");
            if (checkpwd == txtOldPassword.Text)
            {
                if (txtNewPassword.Text == txtcPassword.Text)
                {
                    clsodbc.executeNonQuery("update mlm_login set password='" + txtNewPassword.Text + "' where userid='" + Session["UserID"] + "' ");
                    clsodbc.executeNonQuery("Insert Into mlm_password_history (userid,old_password,new_password,modified_by,pwd_type) values (" + Session["UserID"] + ",'" + txtOldPassword.Text + "','" + txtNewPassword.Text + "'," + Session["UserID"] + ",1)");

                    //          sendmail();

                    DataSet ds = new DataSet();
                    ds = clsodbc.getDataSet("Select b.username, a.my_sponsar_id, a.password, a.trans_pwd, b.email, b.mobile_number From mlm_login a, mlm_personal_details b WHERE a.userid=b.userid AND a.userid =" + Session["UserID"]);
                    string strMobileNumber = ds.Tables[0].Rows[0][5].ToString();

                    string strmessage = "Dear '" + ds.Tables[0].Rows[0][0].ToString() + "', ! Your New Password is " + ds.Tables[0].Rows[0][2].ToString() + ". www.LifeGold.com";

                    objcommunication.SMS_API_for_Single_SMS(strmessage, strMobileNumber);
                    CommonMessages.ShowAlertMessage_Reload("Password Change Successfully!", "overview.aspx");
                }
                else
                {
                    txtcPassword.Text = "";
                    txtcPassword.Attributes.Add("placeholder","password not matched");
                    txtcPassword.Focus();
                }

            }
            else
            {
                CommonMessages.ShowAlertMessage("Old Password not match!");
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void sendmail()
    {
        DataSet ds2 = new DataSet();
        try
        {
            ds2 = clsodbc.getDataSet("SELECT a.username, b.password, a.email FROM mlm_personal_details a INNER JOIN mlm_login b ON a.userid=b.userid AND a.userid=" + Session["UserID"]);
            string strUserName = ds2.Tables[0].Rows[0][0].ToString();
            string strEmailID = ds2.Tables[0].Rows[0][2].ToString();
            string strPassword = ds2.Tables[0].Rows[0][1].ToString();

            string strMailBody = getVerificationOTPMail(strUserName, strPassword);
            objMail.sendMailSendGrid(strEmailID, strUserName, "admin@Zurik.org", "Zurik", "Password Change", strMailBody);

        }
        catch (Exception ex)
        {
        }
    }

    protected string getVerificationOTPMail(string strUsername, string strPassword)
    {
        string strMailString = System.IO.File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("../../Template/for_pass.html")); //change_pass.html

        strMailString = strMailString.Replace("&&strUserName&&", strUsername);
        strMailString = strMailString.Replace("&&strpassword&&", strPassword);
        return strMailString;
    }
}