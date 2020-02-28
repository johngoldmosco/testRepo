using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_admin_edit_user_details : System.Web.UI.Page
{
    ODBC clsOdbc = new ODBC();
    form_func objFormFun = new form_func();
    ClassOther objOther = new ClassOther();
    clsPhoto objphoto = new clsPhoto();
    clsWallet objWallet = new clsWallet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminID"] == "")
        {
            Response.Redirect("../../login.aspx");
        }
        if (!IsPostBack)
        {
            fill_ddl_country();
            FillDetails();
            fill_Account(sender, e);
        }
    }

    private void FillDetails()
    {
        string strQuery = "";
        DataSet ds = new DataSet();

        try
        {
            strQuery = "SELECT a.my_sponsar_id, b.username, a.my_sponsar_sys_id,a.password, b.email, b.country,b.mobile_code, b.mobile_number,b.city,b.photo, b.pancard, b.aadhar FROM mlm_login a INNER JOIN mlm_personal_details b ON a.userid=b.userid WHERE a.userid=" + Request.QueryString[0];
            ds = clsOdbc.getDataSet(strQuery);

            if (ds.Tables[0].Rows.Count > 0)
            {
                txtUserID.Text = ds.Tables[0].Rows[0][0].ToString();
                txtUserName.Text = ds.Tables[0].Rows[0][1].ToString();
                txtReferralID.Text = ds.Tables[0].Rows[0][2].ToString();
                txtPassword.Text = ds.Tables[0].Rows[0][3].ToString();
                txtFullName.Text = ds.Tables[0].Rows[0][1].ToString();
                txtEmail.Text = ds.Tables[0].Rows[0][4].ToString();
                ddlCountry.SelectedValue = ds.Tables[0].Rows[0][5].ToString();

                txtMobileCode.Text = ds.Tables[0].Rows[0][6].ToString();
                txtMobileNumber.Text = ds.Tables[0].Rows[0][7].ToString();
                txtCity.Text = ds.Tables[0].Rows[0][8].ToString();
                txtPan.Text = ds.Tables[0].Rows[0][10].ToString();
                txtAadhar.Text = ds.Tables[0].Rows[0][11].ToString();
                imgProfile.ImageUrl = ds.Tables[0].Rows[0][9].ToString();
            }

        }
        catch (Exception ex)
        {
            CommonMessages.ShowAlertMessage(ex.Message);
        }
        finally
        {
            ds.Dispose();
        }
    }

    protected void fill_ddl_country()
    {
        objOther.filldropdownlist("SELECT country_id,country_name FROM mlm_country WHERE Active=1", ddlCountry, "country_name", "country_id");
    }
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtMobileCode.Text = clsOdbc.executeScalar_str("SELECT mobile_code FROM mlm_country WHERE Active=1 AND country_id ='" + ddlCountry.SelectedValue + "'");
    }
    protected void btnSubmit_Click(object sender, System.EventArgs e)
    {
        string photo = "";

        try
        {
            clsOdbc.executeNonQuery("Update mlm_login set password = '" + txtPassword.Text + "' where userid  =" + Request.QueryString[0]);

            if (fileImage.HasFile)
            {
                string strImg = objphoto.UploadPhoto(fileImage);
                fileImage.SaveAs(Server.MapPath("../image/ProfilePic/") + strImg);
                photo = "../image/ProfilePic/" + strImg;
            }
            else
            {
                photo = imgProfile.ImageUrl;
            }

            clsOdbc.executeNonQuery("Update mlm_personal_details a SET a.username = '" + txtFullName.Text + "',a.mobile_code='" + txtMobileCode.Text + "',a.mobile_number = '" + txtMobileNumber.Text + "',a.email = '" + txtEmail.Text + "', a.country=" + Convert.ToInt32(ddlCountry.SelectedValue) + " , a.photo ='" + photo + "', a.city = '" + txtCity.Text + "', a.aadhar='" + txtAadhar.Text + "', a.pancard='" + txtPan.Text + "' Where a.userid=" + Request.QueryString[0] + "");

            CommonMessages.ShowAlertMessage_Reload("Profile Successfully Updated!", "users.aspx");
        }
        catch (Exception ex)
        {
            CommonMessages.ShowAlertMessage(ex.Message);
        }
    }

    private void fill_Account(object sender, EventArgs e)
    {
        string strQuery = "";
        DataSet ds = new DataSet();

        try
        {
            strQuery = "SELECT a.userid, a.payee_name, a.bank_name, a.branch_name, a.account_number, a.ifsc_code, a.address FROM mlm_bank_account_details a WHERE a.userid=" + Request.QueryString[0];
            ds = clsOdbc.getDataSet(strQuery);

            if (ds.Tables[0].Rows.Count > 0)
            {
                txtAccountNo.Text = ds.Tables[0].Rows[0][4].ToString();
                txtIFSC.Text = ds.Tables[0].Rows[0][5].ToString();
                txtBank.Text = ds.Tables[0].Rows[0][2].ToString();
                txtBranch.Text = ds.Tables[0].Rows[0][3].ToString();
                txtAddress1.Text = ds.Tables[0].Rows[0][6].ToString();
            }
        }
        catch (Exception ex)
        {
            CommonMessages.ShowAlertMessage(ex.Message);
        }
        finally
        {
            ds.Dispose();
        }
    }
    protected void btnBank_Click(object sender, EventArgs e)
    {
        try
        {
            clsOdbc.executeNonQuery("UPDATE mlm_bank_account_details SET account_number='" + txtAccountNo.Text + "',ifsc_code='" + txtIFSC.Text + "', bank_name='" + txtBank.Text + "', branch_name='" + txtBranch.Text + "', address='" + txtAddress1.Text + "' WHERE userid=" + Request.QueryString[0]); 

            clsOdbc.executeNonQuery("INSERT INTO `mlm_bank_details_update`(`userid`, `created_on`) VALUES ('" + Request.QueryString[0] + "','" + objWallet.getCurDateTimeString() + "') ");

            CommonMessages.ShowAlertMessage("Bank details updated successfully!");
        }
        catch (Exception ex) { }
    }
}