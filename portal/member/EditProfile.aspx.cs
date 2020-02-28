using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_member_EditProfile : System.Web.UI.Page
{
    ODBC clsOdbc = new ODBC();
    form_func objFormFun = new form_func();
    ClassOther objOther = new ClassOther();
    clsPhoto objphoto = new clsPhoto();
    static int captchaFlag = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Response.Redirect("../../login.aspx");
        }

        if (!IsPostBack)
        {
            objOther.filldropdownlist("SELECT country_id,country_name FROM mlm_country WHERE Active=1", ddlCountry, "country_name", "country_id");
            FillDetails( sender,  e);
        }
    }

    private void FillDetails(object sender, EventArgs e)
    {

        string strQuery = "";
        System.Data.DataSet ds = new System.Data.DataSet();
        System.Data.DataTable dtNominee = new System.Data.DataTable();

        try
        {
            strQuery = "SELECT a.username,b.my_sponsar_id, b.my_sponsar_sys_id, a.country, a.mobile_number, a.city, b.password,a.email,a.photo, a.pancard, a.aadhar FROM mlm_personal_details a INNER JOIN mlm_login b ON a.userid=b.userid WHERE a.userid=" + Session["UserID"];

            ds = clsOdbc.getDataSet(strQuery);

            if (ds.Tables[0].Rows.Count > 0)
            {
                txtUserID.Text = ds.Tables[0].Rows[0][1].ToString();
                txtUserName.Text = ds.Tables[0].Rows[0][0].ToString();
                txtReferralID.Text = ds.Tables[0].Rows[0][2].ToString();
                //   txtReferralName.Text = ds.Tables[0].Rows[0][6].ToString();
                txtPassword.Text = ds.Tables[0].Rows[0][6].ToString();

                txtFullName.Text = ds.Tables[0].Rows[0][0].ToString();
                txtEmail.Text = ds.Tables[0].Rows[0][7].ToString();
                ddlCountry.SelectedValue = ds.Tables[0].Rows[0][3].ToString();
                ddlCountry_SelectedIndexChanged(sender,e);
                txtMobileNumber.Text = ds.Tables[0].Rows[0][4].ToString();
                txtCity.Text = ds.Tables[0].Rows[0][5].ToString();
                txtPan.Text = ds.Tables[0].Rows[0][9].ToString();
                txtAadhar.Text = ds.Tables[0].Rows[0][10].ToString();
                imgProfile.ImageUrl = ds.Tables[0].Rows[0][8].ToString();
            }

            dtNominee = clsOdbc.getDataTable("SELECT nominee_name, nominee_age, nominee_relation FROM mlm_nominee_details WHERE userid =" + Session["UserID"]);

            if (dtNominee.Rows.Count == 1)
            {
                txtNomName.Text = dtNominee.Rows[0][0].ToString();
                txtNomAge.Text = dtNominee.Rows[0][1].ToString();
                txtNomRel.Text = dtNominee.Rows[0][2].ToString();
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
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCountry.SelectedValue != "0")
        {
            txtMobileCode.Text = clsOdbc.executeScalar_str("SELECT mobile_code  FROM mlm_country WHERE country_id='" + ddlCountry.SelectedValue + "'");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string photo = "";
        try
        {
            if (fileImage.HasFile)
            {
                string strImg = objphoto.UploadPhoto(fileImage);
                fileImage.SaveAs(Server.MapPath("../image/ProfilePic/") + strImg);
                photo = "../image/ProfilePic/" + strImg;
            }
            else
                photo = imgProfile.ImageUrl;

            if (ddlCountry.SelectedValue == "Select")
            {
                clsOdbc.executeNonQuery("Update mlm_personal_details a SET a.mobile_code='" + txtMobileCode.Text + "',a.mobile_number = '" + txtMobileNumber.Text + "', a.email='" + txtEmail.Text + "', a.photo ='" + photo + "', a.aadhar='" + txtAadhar.Text + "', a.city='"+ txtCity.Text +"' Where a.userid=" + Session["UserID"] + "");
            }
            else
            {
                clsOdbc.executeNonQuery("Update mlm_personal_details a SET  a.mobile_code='" + txtMobileCode.Text + "', a.mobile_number = '" + txtMobileNumber.Text + "',a.email='" + txtEmail.Text + "', a.photo ='" + photo + "' , a.country=" + Convert.ToInt32(ddlCountry.SelectedValue) + ", a.aadhar='" + txtAadhar.Text + "', a.city='" + txtCity.Text + "'  WHERE a.userid=" + Session["UserID"] + "");
            }

            CommonMessages.ShowAlertMessage_Reload("Profile Successfully Updated!", "overview.aspx");
        }
        catch (Exception ex)
        {
            CommonMessages.ShowAlertMessage(ex.Message);
        }
    }
    protected void btnNominee_Click(object sender, EventArgs e)
    {
        try
        {
            int intCount = clsOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_nominee_details WHERE userid ="+ Session["UserID"]);
            if (intCount == 0)
            {
                clsOdbc.executeNonQuery("INSERT INTO mlm_nominee_details(userid, nominee_name, 	nominee_age, nominee_relation) VALUES (" + Session["UserID"] + ", '"+ txtNomName.Text +"','"+ txtNomAge.Text +"','"+ txtNomRel.Text +"')");

                CommonMessages.ShowAlertMessage_Reload("Nominee Details Updated!","EditProfile.aspx");
            }
            else
            {
                clsOdbc.executeNonQuery("UPDATE mlm_nominee_details SET nominee_name = '" + txtNomName.Text + "', nominee_age = '" + txtNomAge.Text + "', nominee_relation= '" + txtNomRel.Text + "' WHERE userid = " + Session["UserID"] + "");

                CommonMessages.ShowAlertMessage_Reload("Nominee Details Updated!", "EditProfile.aspx");
            }
        }
        catch(Exception ex)
        {
            CommonMessages.ShowAlertMessage(ex.Message);
        }
    }
}