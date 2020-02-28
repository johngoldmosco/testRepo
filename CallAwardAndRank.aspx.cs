using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CallAwardAndRank : System.Web.UI.Page
{
    ODBC obj = new ODBC();
    clsWallet objwallet = new clsWallet();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            obj.executeNonQuery("CALL SP_award()");
            obj.executeNonQuery("CALL SP_Rank()");
            obj.executeNonQuery("INSERT INTO `mlm_schedulers`(`schedule_task`, `created_on`) VALUES ('Award - Rank','" + objwallet.getCurDateString() + "')");
        }
        catch (Exception ex) { }         
    }
}