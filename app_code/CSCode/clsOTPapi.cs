using System;
using System.Collections.Generic;
using System.Web;
using System.Net;
using System.Net;
using System.Web;
using System.IO;
using System.Text;

/// <summary>
/// Summary description for clsOTPapi
/// </summary>
public class clsOTPapi
{
       ODBC clsOdbc = new ODBC();
	public clsOTPapi()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public  void  sendOTPDomestic(string strMobile,string strMessage,string strOTP) 
    { 
        string strApi="http://sms.onefxzone.com/api/sms.php";
        string  intUid= "6f6e6566787a6f6e6531";
        string strpin = "a4e9876dc8e939d22003bb4e3a7fff30";
        string strSender = "ONEFXZ";
        int intRoute=0;
         string sURL;
         try
         {
             sURL = (("" + strApi + "?uid=" + intUid + "&pin=" + strpin + "&sender=" + strSender + "&route=" + intRoute + "&mobile=" + strMobile + "&message=" + strMessage + "&pushid=1")); 


             WebRequest request = default(WebRequest);

             request = WebRequest.Create(sURL);

             WebResponse resp = request.GetResponse();

             StreamReader reader = new StreamReader(resp.GetResponseStream());
             string responseString = reader.ReadToEnd();

             reader.Close();
             resp.Close();
             string[] strSplit = responseString.Split(',');
             if (strSplit[0] != "0")
             {
                 clsOdbc.executeNonQuery("insert into  mlm_register_otp(mobile_number,otp_password,message,created_on) values('" + strMobile + "','" + strOTP + "','" + strMessage + "','" + DateTime.Now.ToString() + "')");
                 CommonMessages.ShowAlertMessage("OTP send successfully");
            }
             else
             {
                 CommonMessages.ShowAlertMessage("Something is wrong");
             }
             
         }
         catch (Exception ex)
         {
         }
    }

    public  void sendOTPInternational(string strMobile, string strMessage,string strOTP) 
    {
        string strApi = "http://sms.onefxzone.com/api/sms.php";
        string intUid = "6f6e6566787a6f6e6531";
        string strpin = "a4e9876dc8e939d22003bb4e3a7fff30";
        string strSender = "ONEFXZ";
        int intRoute = 13;
        string sURL;
        try
        {
            sURL = (("" + strApi + "?uid=" + intUid + "&pin=" + strpin + "&sender=" + strSender + "&route=" + intRoute + "&mobile=" + strMobile + "&message=" + strMessage + "&pushid=1"));


            WebRequest request = default(WebRequest);

            request = WebRequest.Create(sURL);

            WebResponse resp = request.GetResponse();

            StreamReader reader = new StreamReader(resp.GetResponseStream());
            string responseString = reader.ReadToEnd();

            reader.Close();
            resp.Close();
            string[] strSplit = responseString.Split(',');
            if (strSplit[0] != "0")
            {
                clsOdbc.executeNonQuery("insert into  mlm_register_otp(mobile_number,otp_password,message,created_on) values('" + strMobile + "','" + strOTP + "','" + strMessage + "','" + DateTime.Now.ToString() + "')");
                CommonMessages.ShowAlertMessage("OTP send successfully");
           }
		   else
		   {
		      CommonMessages.ShowAlertMessage("Something is wrong");
		   }
           
        }
        catch (Exception ex)
        {
        }
    }
}