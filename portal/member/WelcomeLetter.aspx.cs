using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class portal_member_WelcomeLetter : System.Web.UI.Page
{
    ODBC obj = new ODBC();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Response.Redirect("../../login.aspx");
        }
        else
        {
            if (!IsPostBack)
            {
                DataTable dt = new DataTable();

                try
                {
                    dt = obj.getDataTable("SELECT a.username, a.photo, a.email, b.my_sponsar_id FROM mlm_personal_details a INNER JOIN mlm_login b ON a.userid=b.userid AND a.userid=" + Session["UserID"]);

                    if (dt.Rows.Count == 1)
                    {
                        lblUserName.Text = dt.Rows[0][0].ToString();
                        string strImage = dt.Rows[0][1].ToString();
                        if (strImage == "" || strImage == null)
                            imgProfile.Src = "../../images/avatar-01.jpg";
                        else
                            imgProfile.Src = strImage;
                        lblEmailID.Text = dt.Rows[0][2].ToString();
                        lblUserID.Text = dt.Rows[0][3].ToString();
                    }
                }
                catch (Exception ex)
                {

                }
                finally { dt.Dispose(); }
            }
        }
    }
}