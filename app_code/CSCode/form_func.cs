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
using System.Net.Mail;
using System.IO;

public class form_func
{
    ODBC objdb = new ODBC();
	public form_func()
	{
		
	}
    //_______________________________________________________________________________________________________________________________

    public void filldropdownlist(DropDownList ddlDateID, DropDownList ddlMonthID, DropDownList ddlYearID)
    {
        ddlDateID.Items.Clear();
        ddlDateID.Items.Insert(0, "Date");

        //fill Date
        string strDate = "";
        for (int iDate = 1; iDate <= 31; iDate++)
        {
            if (iDate <= 9)
                strDate = "0" + iDate.ToString();
            else
                strDate = iDate.ToString();
            ddlDateID.Items.Insert(iDate, strDate);
        }

        ddlMonthID.Items.Clear();
        ddlMonthID.Items.Insert(0, "Month");

        // Fill Month
        string strMonth = "";
        for (int iMonth = 1; iMonth <= 12; iMonth++)
        {
            if (iMonth <= 9)
                strMonth = "0" + iMonth.ToString();
            else
                strMonth = iMonth.ToString();
            ddlMonthID.Items.Insert(iMonth, strMonth);
        }
        ddlYearID.Items.Clear();
        ddlYearID.Items.Insert(0, "Year");

        for (int iYear = 1940; iYear <= 2020; iYear++)
        {
            int ind = iYear - 1939;
            ddlYearID.Items.Insert(ind, iYear.ToString());
        }
    }


    public void filldropdownlist(string selQuery, DropDownList ddId, string dataTxtField, string dataValField)
    {
        DataSet ds = new DataSet();
        ddId.Items.Clear();
        try
        {
            ds = objdb.getDataSet(selQuery);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddId.DataSource = ds;
                ddId.DataTextField = dataTxtField;
                ddId.DataValueField = dataValField;
                ddId.DataBind();
            }
            else
            {
                ddId.DataSource = null;
                ddId.DataBind();
            }
        }
        catch (Exception ex)
        {
        }
        finally
        {
            ds.Dispose();
            ddId.Items.Insert(0, "Select");
            ddId.SelectedIndex = 0;
        }


    }
//___________________________________________________________________________________________________________________________________________________

    public void filldropdownlist_presentDate(DropDownList ddlDateID, DropDownList ddlMonthID, DropDownList ddlYearID)
    {
        ddlDateID.Items.Clear();
        ddlDateID.Items.Insert(0, "Date");

        for (int iDate = 1; iDate <= 31; iDate++)
        {
            ddlDateID.Items.Insert(iDate, iDate.ToString());
        }

        ddlMonthID.Items.Clear();
        ddlMonthID.Items.Insert(0, "Month");

        for (int iMonth = 1; iMonth <= 12; iMonth++)
        {
            ddlMonthID.Items.Insert(iMonth, iMonth.ToString());
        }
        ddlYearID.Items.Clear();
        ddlYearID.Items.Insert(0, "Year");

        for (int iYear = 2013; iYear <= 2025; iYear++)
        {
            int ind = iYear - 2012;
            ddlYearID.Items.Insert(ind, iYear.ToString());
        }

        ddlDateID.SelectedValue = DateTime.Now.Day.ToString();
        ddlMonthID.SelectedValue = DateTime.Now.Month.ToString();
        ddlYearID.SelectedValue = DateTime.Now.Year.ToString();

    }
 //___________________________________________________________________________________________________________________________________________________

    public void filldropdownlist(DropDownList ddID, int intfrom, int intTo, string strSelect)
    {
        ddID.Items.Clear();
        ddID.Items.Insert(0, strSelect);

        for (int iYear = intfrom; iYear <= intTo; iYear++)
        {
            int ind = iYear - intfrom + 1;
            ddID.Items.Insert(ind, iYear.ToString());
        }
    }
 //_________________________________________________________________________________________________________________________________________________

    public static void ChangePassword(string strPassword, string userid, Label lbl)
    {
        ODBC objdb = new ODBC();
        string strQuery = "UPDATE bk_login SET Password='" + strPassword + "' WHERE userid=" + userid;
        objdb.executeNonQuery(strQuery);
        lbl.Text = "Password Changed Successfully !";
    }
//________________________________________________________________________________________________________________________________________________

    // To fetch all details in an array

    public ArrayList FillAllDetails(string strQuery, int colCount)
    {
        ArrayList list = new ArrayList();
        DataSet ds = new DataSet();
        try
        {
            ds = objdb.getDataSet(strQuery);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ds = objdb.getDataSet(strQuery);
                for (int i = 0; i < colCount; i++)
                {
                    list.Add(ds.Tables[0].Rows[0][i].ToString());
                }
            }
            else
            {
                list = null;
            }
        }

        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            ds.Dispose();

        }
        return list;

    }
    //-------------------------------------------------------------------------------------------------------

    public void SendPassword(string strEmail, Label lblStatus)
    {
        
            string strPassword = objdb.executeScalar_str("SELECT password From bk_login WHERE email='" + strEmail + "' and active=1");
            string strFrom = "support@exioms.com";
            string strTo = strEmail;
            string strSubject = "Goal Maker Login Credentials";
            string strMessage = "<table><tr><td>Email id:</td><td>" + strEmail + "</td></tr><tr><td>Password:</td><td>" + strPassword + "</td></tr></table>";
            sendEmailMessage(strFrom, strTo, strSubject, strMessage, lblStatus);
    }
    //____________________________________________________________________________________________________________________________

    public static void sendEmailMessage(string strFrom, string strTO, string strSubject, string strMessage, Label lblStatus)
    {
        MailMessage Mailmsg = new MailMessage();

        System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("mail.zestdine.com", 25);
        System.Net.NetworkCredential netwrkCrd = new System.Net.NetworkCredential();
        netwrkCrd.UserName = "mailsend@zestdine.com";
        netwrkCrd.Password = "P@$$wrD{}12";
        client.Credentials = netwrkCrd;

        try
        {
            Mailmsg.To.Clear();
            Mailmsg.From = new MailAddress(strFrom);
            Mailmsg.To.Add(new MailAddress(strTO));
            Mailmsg.Subject = strSubject;
            Mailmsg.IsBodyHtml = true;
            Mailmsg.Body = strMessage;
            client.Send(Mailmsg);
            lblStatus.Text = "Your Password has been send to your Email. Please check your Email";
            //CommonMessages.ShowAlertMessage("You Mail has been successfully Sent!");
        }
        catch (Exception ex)
        {
            // CommonMessages.ShowAlertMessage("Error on Sending Mail." + ex.Message);
            lblStatus.Text = "Error on Sending Mail." + ex.Message;
            return;
        }
    }
    //___________________________________________________________________________________________________________________

    public void SendPassword(string strEmail, string userid)
    {

        string strPassword = objdb.executeScalar_str("SELECT password From ihro_login WHERE email='" + strEmail + "' and active=1");
        string strFrom = "support@exioms.com";
        string strTo = strEmail;
        string strSubject = "IHRO Login Credentials";
        string strMessage = "<table><tr><td>User Id: </td><td>" + userid + "</td></tr><tr><td>Email id:</td><td>" + strEmail + "</td></tr><tr><td>Password:</td><td>" + strPassword + "</td></tr></table>";
        sendEmailMessage(strFrom, strTo, strSubject, strMessage);
    }
    //____________________________________________________________________________________________________________________________

    public static void sendEmailMessage(string strFrom, string strTO, string strSubject, string strMessage)
    {
        MailMessage Mailmsg = new MailMessage();

        System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("mail.zestdine.com", 25);
        System.Net.NetworkCredential netwrkCrd = new System.Net.NetworkCredential();
        netwrkCrd.UserName = "mailsend@zestdine.com";
        netwrkCrd.Password = "P@$$wrD{}12";
        client.Credentials = netwrkCrd;

        try
        {
            Mailmsg.To.Clear();
            Mailmsg.From = new MailAddress(strFrom);
            Mailmsg.To.Add(new MailAddress(strTO));
            Mailmsg.Subject = strSubject;
            Mailmsg.IsBodyHtml = true;
            Mailmsg.Body = strMessage;
            client.Send(Mailmsg);
            
            //CommonMessages.ShowAlertMessage("You Mail has been successfully Sent!");
        }
        catch (Exception ex)
        {
            // CommonMessages.ShowAlertMessage("Error on Sending Mail." + ex.Message);
            
            return;
        }
    }
    //____________________________________________________________________________________________________________________________
    //************ Fill Epin Type **********************//
    public void FillEPINType(DropDownList ddlDropDownList)
    {
        string strQuery = null;
        System.Data.DataSet ds = new System.Data.DataSet();

        strQuery = "SELECT id,pin_type From mlm_epin_type Order By pin_type ASC";
        ddlDropDownList.Items.Clear();

        try
        {
            ds = objdb.getDataSet(strQuery);


            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDropDownList.DataSource = ds;
                ddlDropDownList.DataTextField = "pin_type";
                ddlDropDownList.DataValueField = "id";
                ddlDropDownList.DataBind();
            }
            ddlDropDownList.Items.Insert(0, "Select");
            ddlDropDownList.SelectedIndex = 0;

        }
        catch (Exception ex)
        {
        }
        finally
        {
            ds.Dispose();
        }

    }
    public void FillDDLPost(DropDownList ddlDropDownList)
    {
        string strQuery = null;
        System.Data.DataSet ds = new System.Data.DataSet();

        strQuery = "SELECT id,post_name From mlm_user_post Order By post_name ASC";
        ddlDropDownList.Items.Clear();

        try
        {
            ds = objdb.getDataSet(strQuery);


            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDropDownList.DataSource = ds;
                ddlDropDownList.DataTextField = "post_name";
                ddlDropDownList.DataValueField = "id";
                ddlDropDownList.DataBind();
            }
            ddlDropDownList.Items.Insert(0, "Select");
            ddlDropDownList.SelectedIndex = 0;

        }
        catch (Exception ex)
        {
        }
        finally
        {
            ds.Dispose();
        }

    }
    public void FillYear(DropDownList ddlYear)
    {
        string strQuery = null;
        System.Data.DataSet ds = new System.Data.DataSet();

        strQuery = "SELECT distinct trans_year From mlm_transaction_monthwise Order By trans_year ASC";
        ddlYear.Items.Clear();

        try
        {
            ds = objdb.getDataSet(strQuery);


            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlYear.DataSource = ds;
                ddlYear.DataTextField = "trans_year";
                ddlYear.DataValueField = "trans_year";
                ddlYear.DataBind();
            }
            ddlYear.Items.Insert(0, "Select");
            ddlYear.SelectedIndex = 0;

        }
        catch (Exception ex)
        {
        }
        finally
        {
            ds.Dispose();
        }
    }
    public void FillMonth(DropDownList ddl)
    {

        for (int month = 1; month <= 12; month++)
        {
            string monthName = System.Globalization.DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            ddl.Items.Add(new ListItem(monthName, month.ToString().PadLeft(2, '0')));
        }
        ddl.Items.Insert(0, "Select");
        ddl.SelectedIndex = 0;
    }
    public void FillWeek(DropDownList ddlWeek)
    {
        string strQuery = null;
        System.Data.DataSet ds = new System.Data.DataSet();

        strQuery = "SELECT distinct trans_week From mlm_transaction_weekwise Order By trans_year ASC";
        ddlWeek.Items.Clear();

        try
        {
            ds = objdb.getDataSet(strQuery);


            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlWeek.DataSource = ds;
                ddlWeek.DataTextField = "trans_week";
                ddlWeek.DataValueField = "trans_week";
                ddlWeek.DataBind();
            }
            ddlWeek.Items.Insert(0, "Select");
            ddlWeek.SelectedIndex = 0;

        }
        catch (Exception ex)
        {
        }
        finally
        {
            ds.Dispose();
        }
    }

    public void fillLevel(DropDownList ddlLevel)
    {
        try
        {
            int intTO = objdb.executeScalar_int("SELECT MAX(level_completed) FROM  mlm_level_log");

            for (int i = 1; i <= intTO; i++)
            {
                ddlLevel.Items.Add(i.ToString());
            }
        }
        catch (Exception ex)
        {
            int intTO = 1;

            for (int i = 1; i <= intTO; i++)
            {
                ddlLevel.Items.Add(i.ToString());
            }
        }
        ddlLevel.Items.Insert(0, "Select");
        ddlLevel.SelectedIndex = 0;

    }
    public string getAllUsers(int intUserID)
    {
        ODBC obj = new ODBC();
        DataSet ds = new DataSet();
        string strChild = "";
        string strQuery = "SELECT a.child_id FROM mlm_level_log a WHERE a.userid=" + intUserID + " ";
        ds = objdb.getDataSet(strQuery);
        if (ds.Tables[0].Rows.Count != 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (i == 0)
                {
                    strChild = strChild + ds.Tables[0].Rows[i][0].ToString();
                }
                else
                {
                    strChild = strChild + "," + ds.Tables[0].Rows[i][0].ToString();
                }

            }
        }
        return strChild;
    }

}

