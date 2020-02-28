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
using System.Collections;

/// <summary>
/// Summary description for show_dashboard
/// </summary>
public class show_dashboard
{
    ODBC clsOdbc = new ODBC();
    ClassOther obj = new ClassOther();
    public show_dashboard()
    {

    }
    public void reoprt1(Literal Literal1, string userid)
    {
        DataSet ds = new DataSet();
        try
        {
            int total_downline = 0;
            string TopDownline = "";
            string strQuery = "SELECT a.UserName,b.my_sponsar_id,b.userid,c.total_down_members, a.email, a.mobile_number, a.created_on FROM mlm_login b,mlm_personal_details a,mlm_progress_count c WHERE a.userid=b.userid and b.userid=c.userid and b.referral_id='" + userid + "' LIMIT 5";
            ds = clsOdbc.getDataSet(strQuery);
            if (ds.Tables[0].Rows.Count != 0)
            {
                TopDownline = "<table class=\"table\"><thead><tr class=\"text-capitalize\"><th>Member Id</th><th>Member Name</th><th>Mobile</th><th>Email</th><th>Join Date</th></tr></thead><tbody>";
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string strUserID = ds.Tables[0].Rows[i][1].ToString();
                    string downline1 = ds.Tables[0].Rows[i][3].ToString();
                    string stremail = ds.Tables[0].Rows[i][4].ToString();
                    string strmobile = ds.Tables[0].Rows[i][5].ToString();
                    string strJoin = ds.Tables[0].Rows[i][6].ToString();
                    //string strMonth = "SELECT Count(*) FROM zs_Offer WHERE MONTHNAME(Created_On)='" + ds.Tables[0].Rows[i][0] + "'";
                    //alMonthCount.Add(obj.executeScalar_int(strMonth));
                    string uname = ds.Tables[0].Rows[i][0].ToString();
                    TopDownline = TopDownline + "<tr><td>" + strUserID + "</td><td>" + uname + "</td><td>" + strmobile + "</td><td>" + stremail + "</td><td>" + strJoin + "</td></tr>";
                }
                TopDownline = TopDownline + "</tbody></table>";
            }
            Literal1.Text = TopDownline;
        }
        catch (Exception ex)
        {
            CommonMessages.ShowAlertMessage(ex.Message.ToString());
        }
        finally
        {
            ds.Dispose();
        }
    }
    protected int count_downline(int userid)
    {
        string downline = str_all_user(userid);
        int id = clsOdbc.executeScalar_int("Select COUNT(id) From mlm_login a  " + downline);
            return id;
    }

    public void reoprt2(Literal Literal2, string userid)
    {
        DataSet ds = new DataSet();
        try
        {
            string TopDownline = "";
            string strQuery = "SELECT b.UserName,a.my_sponsar_id,c.total_balance,a.userid FROM mlm_login a,mlm_personal_details b,mlm_my_balance c WHERE referral_id='" + userid + "' and a.userid = b.userid and a.userid = c.userid ORDER BY c.total_balance DESC LIMIT 5";
            ds = clsOdbc.getDataSet(strQuery);
            if (ds.Tables[0].Rows.Count != 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string strUserID = ds.Tables[0].Rows[i][3].ToString();
                    string uname = ds.Tables[0].Rows[i][0] + "  " + "(" + ds.Tables[0].Rows[i][1] + ")";
                    TopDownline = TopDownline + "<tr><td class=\"description\"><img src=../images/billing.png /><a href=view_user_details.aspx?UID=" + strUserID + " target=_blank>" + uname + "</a></td><td style=text-align:left class=\"value\"><span>" + ds.Tables[0].Rows[i][2] + "</span></td></tr>";
                }
            }
            Literal2.Text = TopDownline;
        }
        catch (Exception ex)
        {
            CommonMessages.ShowAlertMessage(ex.Message.ToString());
        }
        finally
        {
            ds.Dispose();
        }
    }


    public void reoprt3(Literal Literal3, string userid)
    {
        DataSet ds = new DataSet();
        try
        {
            string TopDownline = "";
            string strQuery = "SELECT a.UserName,b.my_sponsar_id,DATE_FORMAT(b.created_on,'%d %b %y') As DOJ,b.userid FROM mlm_personal_details a,mlm_login b WHERE b.referral_id='" + userid + "' and a.userid = b.userid and b.status = 0 LIMIT 5";
            ds = clsOdbc.getDataSet(strQuery);
            if (ds.Tables[0].Rows.Count != 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string strUserID = ds.Tables[0].Rows[i][3].ToString();
                    string uname = ds.Tables[0].Rows[i][0] + "  " + "(" + ds.Tables[0].Rows[i][1] + ")";
                    TopDownline = TopDownline + "<tr><td class=\"description\"><img src=../images/icons/admission.png /><a href=view_user_details.aspx?UID=" + strUserID + " target=_blank>" + uname + "</a></td><td class=\"value\"><span>" + ds.Tables[0].Rows[i][2] + "</span></td></tr>";
                }
            }
            Literal3.Text = TopDownline;
        }
        catch (Exception ex)
        {
            CommonMessages.ShowAlertMessage(ex.Message.ToString());
        }
        finally
        {
            ds.Dispose();
        }
    }
    public string str_all_user(int userid)
    {
        ClassOther objOther = new ClassOther();
        int s = userid;
        ArrayList Fulllist = new ArrayList();
        Fulllist.Add(s);
        Fulllist = objOther.list_child(Fulllist, Fulllist);

        string full_user = " AND a.userid in (0";

        if (Fulllist != null)
        {
            for (int i = 0; i <= Fulllist.Count - 1; i++)
            {
                full_user = full_user + "," + Fulllist[i].ToString();

            }
        }
        full_user = full_user + ")";

        return full_user;
    }
    
}