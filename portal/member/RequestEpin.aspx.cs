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
    clsPhoto objphoto = new clsPhoto();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Response.Redirect("../../Login.aspx");
        }

        if (!IsPostBack)
        {
            lblError.Text = "";
            try
            {
                txtBalance.Text = objOdbc.executeScalar_str("SELECT wallet1 FROM mlm_my_balance_current WHERE userid= " + Session["UserID"] + "");
            }
            catch (Exception ex)
            {
                txtBalance.Text = "0";
            }
            objOther.filldropdownlist("SELECT id, pin_type FROM mlm_epin_type WHERE Active=1",ddlEpinType,"pin_type","id");
        }
    }

    protected void txtEpinCount_TextChanged(object sender, EventArgs e)
    {
        if (txtEpinCount.Text != "")
        {
            int intEpinCount = Int32.Parse(txtEpinCount.Text);

            double dblEpinCost = objOdbc.executeScalar_dbl("SELECT epin_cost FROM mlm_epin_type WHERE id='"+ ddlEpinType.Text +"'");
            //     int intEpinCost = Int32.Parse(dblEpinCost);
            txtTotCost.Text = (intEpinCount * dblEpinCost).ToString();
        }
        else
        {
            lblError.Text = "Please Enter Proper Epin Quantity!";
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        double dblTotalEpinCost = 0.00, dblAvailBalance = 0.00;

        try
        {
            string Rcpt = "";

            if (flupReceipt.HasFile)
            {
                string strImg = objphoto.UploadPhoto(flupReceipt);
                flupReceipt.SaveAs(Server.MapPath("../image/Receipts/") + strImg);
                Rcpt = "../image/Receipts/" + strImg;
            }

            dblTotalEpinCost = double.Parse(txtTotCost.Text);

            if (dblTotalEpinCost != 0)
            {
                int intID = objOdbc.executeScalar_int("CALL epin_request(" + Session["UserID"] + ", " + txtEpinCount.Text + ", '"+ ddlEpinType.SelectedValue +"')");

                objOdbc.executeNonQuery("UPDATE  mlm_epin_request SET reciept_path='" + Rcpt + "' , description='" + txtRefNo.Text + "' WHERE userid=" + Session["UserID"] + " AND id=" + intID + "");

                CommonMessages.ShowAlertMessage_Reload("Epin Request sent Successfully!", "overview.aspx");
            }
            else
            {
                lblError.Text = "Your Balance is not Sufficient for the request!";
                lblError.ForeColor = System.Drawing.Color.Red;
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void ddlEpinType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txtEpinCount.Text != "")
        {
            int intEpinCount = Int32.Parse(txtEpinCount.Text);

            double dblEpinCost = objOdbc.executeScalar_dbl("SELECT epin_cost FROM mlm_epin_type WHERE id='" + ddlEpinType.Text + "'");
            //     int intEpinCost = Int32.Parse(dblEpinCost);
            txtTotCost.Text = (intEpinCount * dblEpinCost).ToString();
        }
        else
        {
            lblError.Text = "Please Enter Proper Epin Quantity!";
        }
    }
}