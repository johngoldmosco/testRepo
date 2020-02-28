using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CallProfitShareDaily : System.Web.UI.Page
{
    ODBC obj = new ODBC();
    clsWallet objwallet = new clsWallet();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string strdt = objwallet.getCurDateString();
			DateTime dt = DateTime.Parse(strdt);
			int intDate = dt.Day;
			
			if (dt.DayOfWeek != DayOfWeek.Saturday)
            {
				if (dt.DayOfWeek != DayOfWeek.Sunday )
				{				
					int intCount =  obj.executeScalar_int("SELECT COUNT(1) FROM mlm_holidays WHERE holiday_date = '"+ strdt +"' AND status=1");
					if(intCount==0)
					{
					//	CommonMessages.ShowAlertMessage(strdt);
						obj.executeNonQuery("CALL update_profit_share()");
						obj.executeNonQuery("INSERT INTO `mlm_schedulers`(`schedule_task`, `created_on`) VALUES ('Profit Share','"+ objwallet.getCurDateTimeString() +"')"); 
					}						
				}
			}
			else
            {
		//		CommonMessages.ShowAlertMessage_Reload("Message", "overview.aspx");
            } 
        }
        catch (Exception ex)
        {
			
        }
    }
}