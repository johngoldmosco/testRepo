using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_admin_GenerateEpin : System.Web.UI.Page
{
    ODBC clsOdbc = new ODBC();
    ClassOther objOther = new ClassOther();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminID"] == "")
        {
            Response.Redirect("../../login.aspx");
        }

        if (!IsPostBack)
        {
            objOther.filldropdownlist("SELECT id, CONCAT(pin_type,' - ', epin_cost) as pin_type FROM mlm_epin_type WHERE Active=1", ddlEpinType, "pin_type", "id");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlEpinType.SelectedValue != "Select")
            {
                double dblEpinCost = clsOdbc.executeScalar_dbl("SELECT epin_cost FROM mlm_epin_type WHERE id= "+ ddlEpinType.SelectedValue);

                clsOdbc.executeNonQuery("CALL GenerateEpin('" + Session["AdminID"] + "', '" + ddlEpinType.SelectedValue + "', '" + int.Parse(txtEpinNo.Text) + "','" + Session["AdminID"] + "', " + dblEpinCost + ")");

                CommonMessages.ShowAlertMessage_Reload("EPin generated successfully!", "GenerateEpin.aspx");
            }
            else
                CommonMessages.ShowAlertMessage("Select Epin Type!");
        }
        catch (Exception ex)
        {
            CommonMessages.ShowAlertMessage(ex.Message);
        }
    }
   
}