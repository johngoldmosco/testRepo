using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_admin_RunScriptManually : System.Web.UI.Page
{
    ODBC clsodbc = new ODBC();
    clsWallet objwallet = new clsWallet();

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string confirmValue = Request.Form["confirm_value"];
        if (confirmValue == "Yes")
        {
            clsodbc.executeNonQuery("call update_profit_share()");
            clsodbc.executeNonQuery("INSERT INTO `mlm_schedulers`(`schedule_task`, `created_on`) VALUES ('Profit Share','" + objwallet.getCurDateTimeString() + "')");
            CommonMessages.ShowAlertMessage("Profit share generated sucessfully");
        }
        else
        {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You click cancel!')", true);
        }
    }
}