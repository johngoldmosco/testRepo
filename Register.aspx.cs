using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Register : System.Web.UI.Page
{
    ODBC objOdbc = new ODBC();
    ClassOther objother = new ClassOther();
    clsWallet objWallet = new clsWallet();
    sendMail objMail = new sendMail();

    protected void Page_Load(object sender, EventArgs e)
    {
	//	Response.Redirect("index.aspx");
        if (!IsPostBack)
        {
            try
            {
                if (Session["UserID"] == null)
                {
                    txtRefID.Text = "";
                    txtRefID.Enabled = true;
                }
                else
                {
                    txtRefID.Text = objOdbc.executeScalar_str("SELECT my_sponsar_id FROM mlm_login WHERE userid=" + Session["UserID"] + "");
                    txtRefID_TextChanged(sender, e);

                    txtRefID.Enabled = true;
                    txtRefName.Enabled = false;
                }

                int intUserID = 0;
                string strposition = Request.QueryString["pos"];
                string strQueryString = Request.QueryString["pid"];

                if (strQueryString != null)
                {
                    intUserID = Int32.Parse(strQueryString);
                    txtRefID.Text = objOdbc.executeScalar_str("SELECT my_sponsar_id FROM mlm_login where userid=" + strQueryString + "");
                    txtRefID.Enabled = false;

                    txtRefName.Text = objOdbc.executeScalar_str("SELECT a.username FROM mlm_personal_details a inner join mlm_login b On a.userid = b.userid WHERE b.my_sponsar_id='" + txtRefID.Text + "'");
				}
				if (strposition != null)
                {
                    if (strposition == "R")
                        ddlPosition.SelectedValue = "2";
                    else if (strposition == "L")
                        ddlPosition.SelectedValue = "1";

                    ddlPosition.Enabled = false;

                }
            }
            catch (Exception ex)
            {
                CommonMessages.ShowAlertMessage(ex.Message);
                // CommonMessages.ShowAlertMessage_Reload("Invalid Referral Link", "index.aspx");
            }
        }
    }

    protected void btnRegister_Click(object sender, EventArgs e)
    {
        //txtEpin_TextChanged(sender, e);
		btnRegister.Enabled = false;
        try
        {
            int intPANCount = objOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_personal_details WHERE pancard = '" + txtPAN.Text + "'");
            if (intPANCount < 3)
            {
                int intUserNameCount = 0;  // objOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_personal_details WHERE username = '" + txtUserName.Text + "'");   //username condition removed on 25 apr on call
                if (intUserNameCount == 0)
                {                   
                    if (txtEpin.Text != "")
                    { 
                        int intValidEpin = objOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_epin WHERE epin='" + txtEpin.Text + "' AND status=1");
                        if (intValidEpin == 1)
                        {
                            string strQuery = "CALL addRegister_new('" + txtRefID.Text + "','" + txtUserName.Text + "','" + txtMobileNo.Text + "', '" + txtEmail.Text + "',2,'" + ddlPosition.SelectedValue + "', '1', '1','1','','" + txtPAN.Text + "')";

                            int intUserID = objOdbc.executeScalar_int(strQuery); 
                            
                            Session["EPIN"] = txtEpin.Text;
                            Session["WelcomeID"] = intUserID;
                         
                            btnRegister.Enabled = false;
                            Response.Redirect("Welcome.aspx");
                        }
                        else
                        {
                            txtEpin.Text = "";
                            txtEpin.Attributes.Add("placeholder", "Enter valid Epin!");
                            txtEpin.BorderColor = System.Drawing.Color.Red;
                            btnRegister.Enabled = false;
                        }
                       
                    }
					else
					{
						lblError.Text = "Please Enter E-Pin!";
						txtEpin.Text = "";
						txtEpin.Focus();
					}
                }
                else
                {
                    lblError.Text = "This User Name already registered!";
                    txtUserName.Text = "";
                    txtUserName.Focus();
                }
            }
            else
            {
                lblError.Text = "This PAN number already registered!";
                txtPAN.Text = "";
                txtPAN.Focus();
            }
        }
        catch (Exception ex)
        {
            CommonMessages.ShowAlertMessage(ex.Message);
        }
    }
        
    protected void txtRefID_TextChanged(object sender, EventArgs e)
    {
        if (txtRefID.Text != "")
        {
            int intCount = objOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_login WHERE my_sponsar_id='" + txtRefID.Text + "'");
							
			if (intCount != 1)
			{
				txtRefID.Text = "";
				txtRefID.Attributes.Add("placeholder", "Referral ID does not exist!");
				txtRefID.Focus();
			}
			else
			{
				int intValidUser = objOdbc.executeScalar_int("SELECT userid FROM mlm_login WHERE my_sponsar_id='" + txtRefID.Text + "'");
				if(intValidUser > 1)
				{
					txtRefName.Text = objOdbc.executeScalar_str("SELECT a.username FROM mlm_personal_details a INNER JOIN mlm_login b On a.userid=b.userid WHERE b.my_sponsar_id='" + txtRefID.Text + "'");
				}
				else{
					txtRefID.Text = "";
					txtRefName.Text = "";
					txtRefID.Attributes.Add("placeholder", "This Referral ID is not allowed!");
					txtRefID.Focus();
				}
			}			
        }
    }
    protected void txtEpin_TextChanged(object sender, EventArgs e)
    {
		if(txtEpin.Text != null)
		{			
			int intValidEpin = objOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_epin WHERE epin='" + txtEpin.Text + "' AND status=1");
			if(intValidEpin==1)
			{
				txtEpin.BorderColor = System.Drawing.Color.Green;
				txtEpin.Enabled = false;
				btnRegister.Enabled = true;
			}
			else
			{
				txtEpin.Text = "";
				txtEpin.Attributes.Add("placeholder","Enter valid Epin!");
				txtEpin.BorderColor = System.Drawing.Color.Red;
				btnRegister.Enabled = false;
			}
		}
		else
		{
			CommonMessages.ShowAlertMessage("Enter Valid Epin!");
		}
    }
}