using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_member_RequestEpin : System.Web.UI.Page
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
            lblError.Text = "";
            try
            {
                txtBalance.Text = objOdbc.executeScalar_str("SELECT wallet1 FROM mlm_my_balance_current WHERE userid= " + Session["UserID"] + "");
				objOther.filldropdownlist("SELECT id, CONCAT( pin_type, '-', epin_cost) AS epinType FROM mlm_epin_type WHERE 1 order by epin_cost ASC", ddlEpinType, "epinType", "id");
            }
            catch (Exception ex)
            {
                txtBalance.Text = "0";
            }
        }
    }

    protected void txtEpinCount_TextChanged(object sender, EventArgs e)
    {
        if (txtEpinCount.Text != "" && ddlEpinType.SelectedValue!="0")
        {
            int intEpinCount = Int32.Parse(txtEpinCount.Text);

            double dblEpinCost = objOdbc.executeScalar_dbl("SELECT epin_cost FROM mlm_epin_type WHERE id='"+ ddlEpinType.SelectedValue +"'");
            //     int intEpinCost = Int32.Parse(dblEpinCost);
            txtTotCost.Text = (intEpinCount * dblEpinCost).ToString();
        }
        else
        {
            lblError.ForeColor = System.Drawing.Color.Red;
            lblError.Text = "Please Select Epin Type! And Enter Proper Epin Quantity!";
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        double dblTotalEpinCost = 0.00, dblAvailBalance = 0.00;

        try
        {
            dblTotalEpinCost = double.Parse(txtTotCost.Text);
            dblAvailBalance = objOdbc.executeScalar_dbl("SELECT wallet1 FROM mlm_my_balance_current WHERE userid = '" + Session["UserID"] + "' ");
            if (dblTotalEpinCost <= dblAvailBalance)
            {
                //   objOdbc.executeNonQuery("CALL purchase_epin("+ Session["UserID"] +", 1, "+ txtEpinCount.Text +", 2, 0, "+ txtTotCost.Text +", 0);");

              
                objOdbc.executeNonQuery("CALL epin_request(" + Session["UserID"] + ", " + txtEpinCount.Text + ","+ ddlEpinType.SelectedValue +" )");
                CommonMessages.ShowAlertMessage_Reload("Epin Requested to admin Successfully!", "ActiveEpin.aspx");
            }
            else
            {
                lblError.Text = "Your Payout Balance is not Sufficient for the request!";
                lblError.ForeColor = System.Drawing.Color.Red;
            }
        }
        catch (Exception ex)
        {
            CommonMessages.ShowAlertMessage(ex.Message);
        }
    }

}