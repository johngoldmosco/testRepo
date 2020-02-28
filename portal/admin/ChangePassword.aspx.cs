using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_admin_ChangePassword : System.Web.UI.Page
{
    ODBC clsodbc = new ODBC();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminID"] == null)
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
            var checkpwd = clsodbc.executeScalar_str("SELECT password FROM mlm_login WHERE userid='" + Session["AdminID"] + "'");
            if (checkpwd == txtOldPassword.Text)
            {
                if (txtNewPassword.Text == txtcPassword.Text)
                {
                    clsodbc.executeNonQuery("update mlm_login set password='" + txtNewPassword.Text + "' where userid='" + Session["AdminID"] + "' ");
                    clsodbc.executeNonQuery("Insert Into mlm_password_history (userid,old_password,new_password,modified_by,pwd_type) values (" + Session["AdminID"] + ",'" + txtOldPassword.Text + "','" + txtNewPassword.Text + "'," + Session["AdminID"] + ",1)");
                   
                    CommonMessages.ShowAlertMessage_Reload("Password Change Successfully!", "dashboard.aspx");
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