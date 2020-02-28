using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ContactUs : System.Web.UI.Page
{
    ODBC objOdbc = new ODBC();
    clsWallet objWallet = new clsWallet();

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objOdbc.executeNonQuery("INSERT INTO `mlm_contact_us`(`name`, `subject`, `email`, mobile_number, `message`, `created_on`) VALUES ('" + txtName.Text + "','','" + txtEmail.Text + "','" + txtMobile.Text + "','" + txtMessage.Text + "','" + objWallet.getCurDateTimeString() + "')");
            CommonMessages.ShowAlertMessage_Reload("We will be contact you very soon!", "index.aspx");
        }
        catch (Exception ex)
        {

        }
    }
}