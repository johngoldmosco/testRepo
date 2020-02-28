using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_admin_Holidays : System.Web.UI.Page
{
    ODBC objODBC = new ODBC();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminID"] == null)
        {
            Response.Redirect("../../login.aspx");
        }
        if (!IsPostBack)
        {

        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
           int intCount =  objODBC.executeScalar_int("SELECT COUNT(1) FROM mlm_holidays WHERE holiday_date = '"+ txtDate.Text +"'");

           if(intCount == 0)
           {
               objODBC.executeNonQuery("INSERT INTO mlm_holidays(holiday_date, status) VALUES('" + txtDate.Text + "', 1)");

               CommonMessages.ShowAlertMessage_Reload("Holidays Updated SuccessFully!", "Holidays.aspx");
           }
        }
        catch (Exception ex) { }
    }
}