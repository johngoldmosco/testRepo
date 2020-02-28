using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_admin_AddNewFranchise : System.Web.UI.Page
{
    ODBC objOdbc = new ODBC();
    ClassOther objother = new ClassOther();   
    clsWallet objWallet = new clsWallet();
    sendMail objMail = new sendMail();

    protected void Page_Load(object sender, EventArgs e)
    {
        if(Session["AdminID"]==null)
        {
            Response.Redirect("../../Login.aspx");
        }
    }
    protected void btnRegister_Click(object sender, EventArgs e)
    {
        if(txtMobile.Text!="" && txtEmail.Text!="")
        {
            int intMobileCount = objOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_personal_details WHERE mobile_number = '" + txtMobile.Text.Trim() + "'");
             if (intMobileCount == 0)
             {
                 string strQuery = "CALL addNewFranchiseRev1('" + txtName.Text + "', '" + txtMobile.Text + "', '" + txtEmail.Text + "', 3,'" + txtPassword.Text + "','" + txtTransPassword.Text + "')";
                  
                 objOdbc.executeNonQuery(strQuery);
                 CommonMessages.ShowAlertMessage("Franchise added successfully!");
             }
        }
    }
}