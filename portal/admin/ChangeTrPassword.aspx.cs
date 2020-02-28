using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_admin_ChangeTrPassword : System.Web.UI.Page
{
    ODBC clsodbc = new ODBC();
    clsCommunication objcommunication = new clsCommunication();
    clsMailTemplate cstemp = new clsMailTemplate();
    sendMail objMail = new sendMail();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminID"] == null)
        {
            Response.Redirect("../../login.aspx");
        }
        if (!IsPostBack)
        { }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            var checkpwd = clsodbc.executeScalar_str("SELECT trans_pwd FROM mlm_login WHERE userid='" + Session["AdminID"] + "'");
            if (checkpwd == txtOldPassword.Text)
            {
                if (txtNewPassword.Text == txtcPassword.Text)
                {
                    clsodbc.executeNonQuery("update mlm_login set trans_pwd='" + txtNewPassword.Text + "' where userid='" + Session["AdminID"] + "' ");

                    CommonMessages.ShowAlertMessage_Reload("Transaction Password Change Successfully!", "dashboard.aspx");
                }
                else
                {
                    txtcPassword.Text = "";
                    txtcPassword.Attributes.Add("placeholder", "password not matched");
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
}