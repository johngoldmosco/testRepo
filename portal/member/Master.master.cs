using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_member_Master : System.Web.UI.MasterPage
{
    ODBC clsOdbc = new ODBC();
    protected void Page_Load(object sender, EventArgs e)
    {       
        if (Session["UserID"] == null)
        {
            Response.Redirect("../../login.aspx");
        }

        if (!IsPostBack)
        {           

            DataTable dt = new DataTable();
            dt = clsOdbc.getDataTable("SELECT username,email,photo FROM mlm_personal_details WHERE userid='" + Session["UserID"] + "'");

            lblUserName1.Text = dt.Rows[0][0].ToString();
            lblUserName2.Text = dt.Rows[0][0].ToString();
            lblUserEmail.Text = dt.Rows[0][1].ToString();

            imgprofile1.Src = dt.Rows[0][2].ToString();
            img1.Src = dt.Rows[0][2].ToString();
        }
    }
}
