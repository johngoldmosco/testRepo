using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_member_Topup : System.Web.UI.Page
{
    ODBC objOdbc = new ODBC();
    ClassOther objOther = new ClassOther();
   

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Response.Redirect("../../login.aspx");
        }

        if (!IsPostBack)
        {
            objOther.filldropdownlist("SELECT id, pin_type FROM mlm_epin_type WHERE Active=1 AND prod_category=1", ddlEpinType, "pin_type", "id");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (txtEpin.Text!="")
        {
            try
            {
                int intCount = objOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_epin WHERE userid="+ Session["UserID"] +" AND status = 1 AND Epin='"+ txtEpin.Text +"' AND epin_type='"+ ddlEpinType.SelectedValue +"'");

                if (intCount == 1 )
                {
                    int intUserID = objOdbc.executeScalar_int("SELECT userid FROM mlm_login WHERE my_sponsar_id = '" + txtUserID.Text + "'");

                    int intTopUpID = objOdbc.executeScalar_int("CALL Activate_member(" + intUserID + ", '" + txtEpin.Text + "'," + Session["UserID"] + ")");
					
					Random rnd = new Random();
                    int transno = rnd.Next(1, 999999); // creates a number between 1 and 12

                    int intTransCount = objOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_topup WHERE trans_id='" + transno + "'");
                    while(intTransCount != 0)
                    {
                        transno = rnd.Next(1, 999999);
                        intTransCount = objOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_topup WHERE trans_id='" + transno + "'");
                    }
                    objOdbc.executeNonQuery("UPDATE mlm_topup SET trans_id=" + transno + " WHERE id=" + intTopUpID + " ");
					
					Response.Redirect("Invoice.aspx?UID=" + intUserID + "&OID=" + intTopUpID + "");
       //           CommonMessages.ShowAlertMessage_Reload("Member Top Up Successfully!", "Topup.aspx");
                }
                else
                {
                    txtEpin.Attributes.Add("placeholder", "Epin Not Valid!");
                    txtEpin.Text = "";
                    txtEpin.Focus();
                }
               
            }
            catch (Exception ex)
            {

            }
        }
        else
        {
            txtEpin.Attributes.Add("placeholder", "Enter Valid Epin!");
            txtEpin.Text = "";
            txtEpin.Focus();
        }
    }
    protected void txtUserID_TextChanged(object sender, EventArgs e)
    {
        lblError.Text = "";
        try
        {
            if (txtUserID.Text != "")
            {
                int intCount = objOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_login WHERE my_sponsar_id = '" + txtUserID.Text.Trim() + "' AND status=1");
                if (intCount == 1)
                {
                    int intactivateStatus = objOdbc.executeScalar_int("SELECT product_status FROM mlm_login WHERE my_sponsar_id = '" + txtUserID.Text.Trim() + "' AND status=1");

                    if (intactivateStatus == 0)
                    {
                        txtUserName.Text = objOdbc.executeScalar_str("SELECT a.username FROM mlm_personal_details a INNER JOIN mlm_login b ON a.userid=b.userid WHERE b.my_sponsar_id = '" + txtUserID.Text + "'");
                    }
                    else
                    {
                        lblError.Text = "Entered Member is Already Activated!";
                        txtUserID.Text = "";
                        txtUserID.Focus();
                    }
                }
                else
                {
                    lblError.Text = "Please Enter Correct User ID to Activate!";
                    txtUserID.Text = "";
                    txtUserID.Focus();
                }
            }
            else
            {
                lblError.Text = "Please Enter User ID to Activate!";
                txtUserID.Focus();
            }
        }
        catch (Exception ex)
        { }
    }
    protected void txtEpin_TextChanged(object sender, EventArgs e)
    {
        if (ddlEpinType.SelectedValue != "0")
        {
            int intCount = objOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_epin WHERE epin='" + txtEpin.Text + "' AND epin_type='"+ ddlEpinType.SelectedValue +"' AND userid='" + Session["UserID"] + "' AND status=1");

            if(intCount!=1)
            {
                txtEpin.Text = "";
                txtEpin.Attributes.Add("Placeholder", "Invalid Epin");
                txtEpin.BorderColor = System.Drawing.Color.Red;
                txtEpin.Focus();
            }
            else
            {
                txtEpin.BorderColor = System.Drawing.Color.Green;
            }
        }
        else
            CommonMessages.ShowAlertMessage("Please select epin type!");
    }
}