using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for total_type_member
/// </summary>
public class total_type_member
{
    ODBC clsOdbc = new ODBC();
	public total_type_member()
	{
		
	}
    public void Total_Members_count(out int active, out int pending,out int disabled,out int total,int uid)
    {        
        string strReferralID = clsOdbc.executeScalar_str("SELECT my_sponsar_id From mlm_referral Where userid=" + uid);
        active = clsOdbc.executeScalar_int("SELECT Count(*) From  mlm_login a,mlm_referral b,mlm_state c Where a.Active=1 and a.status=1 and a.state=c.state_id  and a.userid=b.userid and b.referral_id='" + strReferralID + "'");


        strReferralID = clsOdbc.executeScalar_str("SELECT my_sponsar_id From mlm_referral Where userid=" + uid);
        pending = clsOdbc.executeScalar_int("SELECT Count(*) From  mlm_login a,mlm_referral b,mlm_state c Where a.Active=1 and a.status=0 and a.state=c.state_id  and a.userid=b.userid and b.referral_id='" + strReferralID + "'");

        strReferralID = clsOdbc.executeScalar_str("SELECT my_sponsar_id From mlm_referral Where userid=" + uid);
        disabled = clsOdbc.executeScalar_int("SELECT Count(*) From  mlm_login a,mlm_referral b,mlm_state c Where a.Active=1 and a.status=2 and a.state=c.state_id  and a.userid=b.userid and b.referral_id='" + strReferralID + "'");

        strReferralID = clsOdbc.executeScalar_str("SELECT my_sponsar_id From mlm_referral Where userid=" + uid);
        total = clsOdbc.executeScalar_int("SELECT Count(*) From  mlm_login a,mlm_referral b,mlm_state c Where a.Active=1 and a.state=c.state_id  and a.userid=b.userid and b.referral_id='" + strReferralID + "'");


    }
    
}
