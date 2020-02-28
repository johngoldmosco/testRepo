using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_franchise_Logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["FransID"] = null;
            Session["transID"] = null;
		    Session["showPopup"] = "";
            Response.Redirect("../../FransLogin.aspx");           
        }
    }
}