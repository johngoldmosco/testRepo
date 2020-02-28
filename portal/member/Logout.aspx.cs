using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_member_Logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["UserID"] = null;
            Session["transID"] = null;
		    Session["showPopup"] = "";            
            Response.Redirect("../../login.aspx");
           
        }
    }
}