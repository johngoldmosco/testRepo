using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CallBinaryIncome : System.Web.UI.Page
{
    ODBC obj = new ODBC();
    clsWallet objwallet = new clsWallet();

    protected void Page_Load(object sender, EventArgs e)
    {
        string strdt = objwallet.getCurDateString();

        int intCount = obj.executeScalar_int("SELECT COUNT(1) FROM mlm_binary_daily_payout WHERE DATE(binary_date)=DATE('" + strdt + "')");

        if (intCount == 0)
        {
            obj.executeNonQuery("CALL UpdateBinary()");
			obj.executeNonQuery("INSERT INTO `mlm_schedulers`(`schedule_task`, `created_on`) VALUES ('Binary Income','"+ objwallet.getCurDateString() +"')");
        }
    }
}