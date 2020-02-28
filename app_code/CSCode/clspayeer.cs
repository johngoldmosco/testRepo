using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Net;
using System.IO;
using Microsoft.VisualBasic;
using System.Text;
using System.Configuration;

/// <summary>
/// Summary description for clspayeer
/// </summary>
public class clspayeer
{
    ODBC objOdbc = new ODBC();
    clsCommon csComm = new clsCommon();
    clsWallet csWallet = new clsWallet();
    string m_shop, m_orderid, m_curr, m_desc, m_key, m_sign, m_amount;
    double m_amount1=0.00;
     
    
	public clspayeer()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public string cardApply_ViaPayeer(int intUserID, double dblAmt, double dblTotalAmt, int intTaskId)
    {
        string URL = "";

        string strdt = csWallet.getAusCurDateTimeString();
        string acc_email, sci_name, currency, order_id, sign, password;
        acc_email = ConfigurationManager.AppSettings["AdvcashEmail"];
        sci_name = ConfigurationManager.AppSettings["AdvcashShopName"];
        password = ConfigurationManager.AppSettings["AdvcashPassword"];
        currency = "USD";
        order_id = csComm.GenrateRandomNumberString();
        string[] arHash = { 
                              acc_email,
	                          sci_name,
	                          dblTotalAmt.ToString() ,
	                          currency,
	                          password,
	                          order_id
                          };

        string implodestr = String.Join(":", arHash);
        sign = sha256_hash(implodestr);


        URL = "https://wallet.advcash.com/sci/?ac_account_email=" + acc_email + "&ac_sci_name=" + sci_name + "&ac_amount=" + dblTotalAmt + "&ac_currency=" + currency + "&ac_order_id=" + order_id + "&ac_sign=" + sign + "";

        WebRequest request = null;

        request = WebRequest.Create(URL);

        WebResponse resp = request.GetResponse();

        StreamReader reader = new StreamReader(resp.GetResponseStream());
        string responseString = reader.ReadToEnd();

        string[] strDateSplit = responseString.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

        reader.Close();
        resp.Close();

        HttpContext.Current.Session["AdvcashID"] = order_id;
        string strdesc = "Payment for Apply Card";
        string strquery = "INSERT INTO mlm_advcash_payment(userid,amt,total_amt,created_on,order_id, description,taskid) VALUES(" + intUserID + "," + dblAmt + "," + dblTotalAmt + ",'" + strdt + "','" + order_id + "','" + strdesc + "',2)";
        objOdbc.executeNonQuery(strquery);

        return URL;
    }

    //int IntuserId, int intEpinType, double dblEpinCost, int intEpinNumber
    public string PurchaseIkeyViaPayeer(int intUserID, double dblAmt,double dblTotalAmt, int intEpinType,int intEpinNo )
    {
        string strdt = csWallet.getAusCurDateTimeString();  
        m_shop = "126937182";
        m_orderid=csComm.GenrateRandomNumberString ()  ;
         m_amount = string.Format("{0:0.00}", dblTotalAmt);
         //m_amount = "1.00";
         m_curr="USD";   
        Byte[] bDesc=  Encoding.ASCII.GetBytes("Purchase Epin");
         m_desc =Convert.ToBase64String(bDesc);
         //       byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes("Test payment №12345");
         //m_desc = System.Convert.ToBase64String(toEncodeAsBytes);
        m_key = "onefx77771234";       

       // string[] arHash = new string[10];

        string[] arHash = { 
                              m_shop,
	                          m_orderid,
	                          m_amount.ToString() ,
	                          m_curr,
	                          m_desc,
	                          m_key
                          };

        //var kv = new Dictionary<string, string>() {
        //     { "m_shop", m_shop },
        //     { "m_orderid",  m_orderid},
        //     { "m_amount", m_amount },
        //      { "m_curr", m_curr },
        //     { "m_desc", m_desc },
        //     { "m_key", m_key }
        // };


        string implodestr = String.Join(":", arHash);
        string sign = sha256_hash(implodestr);
        sign = sign.ToUpper();
        string signp = "0F7FF9395DB1F68FA60EBA880C3F7965569D2F28067CE31BA844A2F883CDFC8C";

        if (signp == sign)
        { 
        
        }

        string URL = "https://payeer.com/merchant/?m_shop=" + m_shop + "&m_orderid=" + m_orderid + "&m_amount=" + m_amount + "&m_curr=" + m_curr + "&m_desc=" + m_desc + "&m_sign=" + sign + "&lang=en";
        
        
        WebRequest request = null;

        request = WebRequest.Create(URL);

        WebResponse resp = request.GetResponse();

        StreamReader reader = new StreamReader(resp.GetResponseStream());
        string responseString = reader.ReadToEnd();
        
        string[] strDateSplit = responseString.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

        reader.Close();
        resp.Close();

        HttpContext.Current.Session["PayeerID"] = m_orderid;    

        objOdbc.executeNonQuery("INSERT INTO mlm_payeer_temporary(userid,epin_type,epin_no,amt,total_amt,payment_type,created_on,order_id) VALUES(" + intUserID + "," + intEpinType + "," + intEpinNo + "," + dblAmt + "," + dblTotalAmt + ",4,'"+ strdt  +"'," + m_orderid + ")");  

        return URL;
        //try
        //{
        //    string strquery = "call purchase_epin(" + IntuserId + "," + intEpinType + "," + dblEpinCost + "," + intEpinNumber + ",2, 0,0 ,0,1)";
        //    objOdbc.executeNonQuery(strquery);
        //   //objOdbc.executeNonQuery("Update mlm_epin_purchase SET payment_status=1, pf_payee_acct_name='" + strPayeeAccName + "', pf_payer_acct='" + strPayerAccount + "',pf_payee_acct='" + strPayeeAccount + "', pf_amt='" + strAmount + "', pf_batch_no='" + strPaymentBatchNo + "', pf_payment_id='" + strPaymentId + "' where userid='" + intUserID + "'and epin_type='" + intEpinType + "' and epin_cost='" + dblEpinCost + "'and epin_count='" + intEpinNumber + "' and  payment_type=1");
        //    //CommonMessages.ShowAlertMessage_Reload("I-Key Purchased Sucessfully.", "purchase_ikey.aspx");
        //}
        //catch (Exception ex)
        //{ }

        
    }

    public string InvestViaPayeer(int intUserID, double dblAmt, double dblTotalAmt, int intEpinTypeID, int intEpinCount, double dblEpinPrice, double dblTotPrice)
    {
        string strdt = csWallet.getAusCurDateTimeString();
        m_shop = "380537073";
        m_orderid = csComm.GenrateRandomNumberString();
        m_amount = string.Format("{0:0.00}", dblTotalAmt);      
        m_curr = "USD";
        Byte[] bDesc = Encoding.ASCII.GetBytes("A-Code Purchase");
        m_desc = Convert.ToBase64String(bDesc);
        m_key = "Eg9jb5AF8pslAYwC";

        string[] arHash = { 
                              m_shop,
	                          m_orderid,
	                          m_amount.ToString() ,
	                          m_curr,
	                          m_desc,
	                          m_key
                          };

        string implodestr = String.Join(":", arHash);
        string sign = sha256_hash(implodestr);
        sign = sign.ToUpper();
        string signp = "7198A6C0F2939E39DC1B50027FD824B39C9D33AAD302A08573844B20DAF7D120";

        if (signp == sign)
        {

        }
        string URL = "https://payeer.com/merchant/?m_shop=" + m_shop + "&m_orderid=" + m_orderid + "&m_amount=" + m_amount + "&m_curr=" + m_curr + "&m_desc=" + m_desc + "&m_sign=" + sign + "&lang=en";

        WebRequest request = null;

        request = WebRequest.Create(URL);

        WebResponse resp = request.GetResponse();

        StreamReader reader = new StreamReader(resp.GetResponseStream());
        string responseString = reader.ReadToEnd();

        string[] strDateSplit = responseString.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

        reader.Close();
        resp.Close();

        HttpContext.Current.Session["PayeerID"] = m_orderid;
        string strdesc = "Payment for A-Code Purchase";
        string strquery = "INSERT INTO mlm_payeer_temporary(userid,amt,total_amt,payment_type,created_on,order_id, description, task, epin_type_id, epin_count, epin_cost, total_cost ) VALUES(" + intUserID + "," + dblAmt + "," + dblTotalAmt + ",4,'" + strdt + "'," + m_orderid + ",'" + strdesc + "', 1 , " + intEpinTypeID + ", " + intEpinCount + ", " + dblEpinPrice + "," + dblTotPrice + ")";
        objOdbc.executeNonQuery(strquery);

        return URL;
    }

    protected string getSHA256(string str)
    {
        //byte[] bytes = Encoding.Unicode.GetBytes(str);
        //SHA256Managed hashstring = new SHA256Managed();
        //byte[] hash = hashstring.ComputeHash(bytes);
        //string hashString = string.Empty;
        //foreach (byte x in hash)
        //{
        //    hashString += String.Format("{0:x2}", x);
        //}
        //return hashString;
//-----------------------------------------------------------------
        //string password = “ThisIsMyPassword”;
        HashAlgorithm hash = new SHA256Managed();
        byte[] plainTextBytes = System.Text.Encoding.UTF8.GetBytes(str);
        byte[] hashBytes = hash.ComputeHash(plainTextBytes);

        //in this string you got the encrypted password
        string hashValue = Convert.ToBase64String(hashBytes);
        return hashValue;
    }

    public String sha256_hash(String value)
    {
        StringBuilder Sb = new StringBuilder();

        using (SHA256 hash = SHA256Managed.Create())
        {
            Encoding enc = Encoding.UTF8;
            Byte[] result = hash.ComputeHash(enc.GetBytes(value));

            foreach (Byte b in result)
                Sb.Append(b.ToString("x2"));
        }

        return Sb.ToString();
    }

    public string PurchaseIKeyByAdvcash(int intUserID, double dblAmt, double dblTotalAmt, int intEpinTypeID, int intEpinCount, double dblEpinPrice, double dblTotPrice)
    {
        string strdt = csWallet.getAusCurDateTimeString();
        string acc_email, sci_name, currency, order_id, sign,password;
        acc_email = "mysuccesswork9090@gmail.com";
        sci_name = "My Success Machine Internation";
        password = "AdtuutfudWET@54646";
        currency = "USD";
        order_id = csComm.GenrateRandomNumberString();
        string[] arHash = { 
                              acc_email,
	                          sci_name,
	                          dblTotalAmt.ToString(),
	                          currency,
	                          password,
	                          order_id
                          };

        string implodestr = String.Join(":", arHash);
        sign = sha256_hash(implodestr);


        string URL = "https://wallet.advcash.com/sci/?ac_account_email=" + acc_email + "&ac_sci_name=" + sci_name + "&ac_amount=" + dblTotalAmt + "&ac_currency=" + currency + "&ac_order_id=" + order_id + "&ac_sign=" + sign + "";

        WebRequest request = null;

        request = WebRequest.Create(URL);

        WebResponse resp = request.GetResponse();

        StreamReader reader = new StreamReader(resp.GetResponseStream());
        string responseString = reader.ReadToEnd();

        string[] strDateSplit = responseString.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

        reader.Close();
        resp.Close();

        HttpContext.Current.Session["AdvcashID"] = order_id;
        string strdesc = "Payment for A-Code Purchase";
        string strquery = "INSERT INTO mlm_advcash_payment(userid,amt,total_amt,created_on,order_id, description,epin_type_id, epin_count, epin_cost, total_cost) VALUES(" + intUserID + "," + dblAmt + "," + dblTotalAmt + ",'" + strdt + "','" + order_id + "','" + strdesc + "', " + intEpinTypeID + ", " + intEpinCount + ", " + dblEpinPrice + "," + dblTotPrice + ")";
        objOdbc.executeNonQuery(strquery);

        return URL;
    }

}