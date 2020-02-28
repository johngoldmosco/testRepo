using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_admin_EpinRevert : System.Web.UI.Page
{
    ODBC clsOdbc = new ODBC();
    ClassOther objOther = new ClassOther();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminID"] == null)
        {
            Response.Redirect("../../Login.aspx");
        }
        if (!IsPostBack)
        {
            objOther.filldropdownlist("SELECT id, CONCAT(pin_type,' - ', epin_cost) AS pin_type  FROM mlm_epin_type ORDER BY id ASC ", ddlEpinType, "pin_type", "id");
            ddlEpinType.Items.RemoveAt(0);
            ddlEpinType.Items.Insert(0, new ListItem("Select", "0"));
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlEpinType.SelectedValue != "0")
            {
                if (double.Parse(txtEpinNos.Text) <= double.Parse(txtAvlEpins.Text))
                {
                    int intCount = clsOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_login WHERE my_sponsar_id='" + txtUserID.Text + "'");
                    if (intCount>0)
                    {
                        int intUserID = clsOdbc.executeScalar_int("SELECT userid FROM mlm_login WHERE my_sponsar_id='" + txtUserID.Text + "' ");

                        string strQuery = "CALL RevertEpin('" + intUserID + "', '" + txtEpinNos.Text + "', '" + ddlEpinType.SelectedValue + "', '" + txtEpinAmt.Text + "', 0)";
                        clsOdbc.executeNonQuery(strQuery);
                        CommonMessages.ShowAlertMessage_Reload("Epin(s) reverted successfully!", "ActiveEpin.aspx");
                    }
                    else
                    {
                        CommonMessages.ShowAlertMessage("Enter Proper userid!");
                    }
                    
                }
                else {
                    CommonMessages.ShowAlertMessage("Entered epin numbers are greater than availble epins!");
                }
            }

        }
        catch (Exception ex)
        {
        }
    }
    protected void ddlEpinType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txtUserID.Text != "")
        {
            int intUserID = clsOdbc.executeScalar_int("SELECT userid FROM mlm_login WHERE my_sponsar_id='" + txtUserID.Text + "' ");
            if (ddlEpinType.SelectedValue != "0")
            {
                txtEpinAmt.Text = clsOdbc.executeScalar_str("SELECT  epin_cost  FROM mlm_epin_type WHERE id='" + ddlEpinType.SelectedValue + "'");

                txtAvlEpins.Text = clsOdbc.executeScalar_str("SELECT COUNT(1) FROM mlm_epin  WHERE epin_type='" + ddlEpinType.SelectedValue + "' AND status=1 AND userid='" + intUserID + "'");
            }
        }
        else
        {
            CommonMessages.ShowAlertMessage("Please enter userid!");
            txtUserID.Focus();
        }
    }
}