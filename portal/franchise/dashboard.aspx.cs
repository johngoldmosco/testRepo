using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_franchise_dashboard : System.Web.UI.Page
{
    ODBC clsOdbc = new ODBC();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["FransID"] == null)
        {
            Response.Redirect("../../FransLogin.aspx");
        }

        if (!IsPostBack)
        {
            lblRepurchase.Text = clsOdbc.executeScalar_str("SELECT  COALESCE(sum(total_amount),0) FROM `mlm_repurchase` WHERE franchise_id='" + Session["FransID"] + "'");
            lblPV.Text = clsOdbc.executeScalar_str("SELECT  COALESCE(sum(total_bv),0) FROM `mlm_repurchase` WHERE franchise_id='" + Session["FransID"] + "'");
        }
    }
}