using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_admin_EditFranchiseDetails : System.Web.UI.Page
{
    ODBC objOdbc = new ODBC();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminID"] == null)
        {
            Response.Redirect("../../Login.aspx");
        }
        if (!IsPostBack)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = objOdbc.getDataTable("SELECT a.my_sponsar_id, b.username, a.password, b.mobile_number, b.email, a.status, a.trans_pwd FROM mlm_login a INNER JOIN mlm_personal_details b ON a.userid=b.userid WHERE a.userid='" + Request.QueryString[0] + "'");
                if (dt.Rows.Count > 0)
                {
                    txtFransID.Text = dt.Rows[0][0].ToString();
                    txtFransName.Text = txtFullName.Text = dt.Rows[0][1].ToString();
                    txtPassword.Text = dt.Rows[0][2].ToString();
                    txtMobileNumber.Text = dt.Rows[0][3].ToString();
                    txtEmail.Text = dt.Rows[0][4].ToString();
                    txtTransPwd.Text = dt.Rows[0][6].ToString();
                }
            }
            catch (Exception ex) { }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objOdbc.executeNonQuery("UPDATE mlm_personal_details SET username='" + txtFullName.Text + "',  email='" + txtEmail.Text + "', mobile_number='" + txtMobileNumber.Text + "' WHERE userid='" + Request.QueryString[0] + "' ");
            objOdbc.executeNonQuery("UPDATE mlm_login SET password='" + txtPassword.Text + "', trans_pwd='" + txtTransPwd.Text + "' WHERE userid='" + Request.QueryString[0] + "' ");
            CommonMessages.ShowAlertMessage_Reload("Franchise profile added successfully!", "FranchiseManager.aspx");
        }
        catch (Exception ex) { }

    }
}