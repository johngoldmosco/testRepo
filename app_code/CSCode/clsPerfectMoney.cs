using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Configuration;

/// <summary>
/// Summary description for clsPerfectMoney
/// </summary>
public class clsPerfectMoney
{
    ODBC objOdbc = new ODBC();
    clsPerfectMoney1 obj = new clsPerfectMoney1();
    clsCommon csComm = new clsCommon();
	public clsPerfectMoney()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string InvestViaPerfectMoney(int intUserID, string PerfectMoneyPayeerID, string strAmt, int intEpinTypeID, int intEpinCount, double dblEpinPrice, double dblTotPrice)
    {

        ArrayList arrResult = new ArrayList();
        clsWallet csWallet = new clsWallet();
        string strResult = "";
        string PerfectMoneyAc, PerfectMoneyPwd, ReciverPayeerID, strPayId;
        byte b = 0;
        string strdt = csWallet.getAusCurDateTimeString();
        int strPayin;
        Dictionary<string, string> d = new Dictionary<string, string>();

        PerfectMoneyAc = "9573063";// ConfigurationManager.AppSettings["PMLoginID"];
        PerfectMoneyPwd = "35T5RE49bzVcPqoThyzkNWMZD";// ConfigurationManager.AppSettings["PMPassPhrase"];
        ReciverPayeerID = "U14156621";// ConfigurationManager.AppSettings["PMPayee"];
       
        strPayin=1;
        strPayId = csComm.GenrateRandomNumberString();

        d = obj.Transfer(PerfectMoneyAc, PerfectMoneyPwd, PerfectMoneyPayeerID, ReciverPayeerID, Convert.ToDouble(strAmt), Convert.ToInt32(strPayin), Convert.ToInt32(strPayId));
        foreach (var item in d)
        {
            if (item.Key == "ERROR")
            {
                strResult = item.Value;
                objOdbc.executeNonQuery("insert into mlm_perfectmoney_payment(userid,message,status) values('" + intUserID + "','" + strResult + "',2)");
               // CommonMessages.ShowAlertMessage(item.Value);
               // strResult = "Payment Fail!";
            }
            else
            {
                arrResult.Add(item.Value);
                b = 1;
            }
        }

        if (b == 1)
        {
            string strPayeeAccName = arrResult[0].ToString();
            string strPayeeAccount = arrResult[1].ToString();
            string strPayerAccount = arrResult[2].ToString();
            string strAmount = arrResult[3].ToString();
            string strPaymentBatchNo = arrResult[4].ToString();
            string strPaymentId = arrResult[5].ToString();            

            try
            {                
                objOdbc.executeNonQuery("INSERT INTO mlm_perfectmoney_payment(userid,amt,pf_payee_acct_name,pf_payer_acct,pf_payee_acct,pf_amt,pf_batch_no,pf_payment_id,status,created_on, epin_type_id, epin_cost, epin_count, total_cost) VALUES(" + intUserID + "," + strAmount + ",'" + strPayeeAccName + "','" + strPayerAccount + "','" + strPayeeAccount + "'," + strAmount + ",'" + strPaymentBatchNo + "','" + strPaymentId + "', 1,'" + strdt + "', " + intEpinTypeID + ", " + dblEpinPrice + "," + intEpinCount + ", " + dblTotPrice + "  )");

                objOdbc.executeNonQuery("call purchase_epinBYPaymentgateway(" + intUserID + "," + intEpinTypeID + ", " + intEpinCount + ", 2, 2 , " + dblEpinPrice + " )");
                strResult = "A-Code Purchased Successfully !";
            }
            catch (Exception ex)
            { }
        }

        return strResult;
    }

	public string getBalance(string strLoginId, string strPwd)
    {
        ArrayList arrResult = new ArrayList();
        string strResult = "";
        byte b = 0;
        Dictionary<string, string> d = new Dictionary<string, string>();
			
		d = obj.QueryBalance(strLoginId, strPwd);
        foreach (var item in d)
        {
            if (item.Key == "ERROR")
            {
                strResult = item.Value;
                // CommonMessages.ShowAlertMessage(item.Value);
            }
            else
            {
                arrResult.Add(item.Value);
                b = 1;
            }
        }

        if (b == 1)
        {
            strResult = arrResult[0].ToString();            
        }
        return strResult;
    }  
    
}