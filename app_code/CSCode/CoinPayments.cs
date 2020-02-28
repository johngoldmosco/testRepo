using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Script.Serialization;
using System.IO;
using System.Runtime.Serialization.Json;

public class CoinPayments
{
    private string s_privkey = "";
    private string s_pubkey = "";
    private static readonly Encoding encoding = Encoding.UTF8;

    public CoinPayments()
    {
        s_privkey = "Acb210c2C28cf16f756ce7095cCce97596858381470a3184b4767bF950A09198";
        s_pubkey = "915ff58c14fa01944afa6479aee488fb58d5207278977ebe4459a3236d9b2986";
        if (s_privkey.Length == 0 || s_pubkey.Length == 0)
        {
            throw new ArgumentException("Private or Public Key is empty");
        }
    }

    public Dictionary<string, object> CallAPI(string cmd, SortedList<string, string> parms = null)
    {
        if (parms == null)
        {
            parms = new SortedList<string, string>();
        }
        parms["version"] = "1";
        parms["key"] = s_pubkey;
        parms["cmd"] = cmd;

        string post_data = "";
        foreach (KeyValuePair<string, string> parm in parms)
        {
            if (post_data.Length > 0) { post_data += "&"; }
            post_data += parm.Key + "=" + Uri.EscapeDataString(parm.Value);
        }

        byte[] keyBytes = encoding.GetBytes(s_privkey);
        byte[] postBytes = encoding.GetBytes(post_data);
        var hmacsha512 = new System.Security.Cryptography.HMACSHA512(keyBytes);
        string hmac = BitConverter.ToString(hmacsha512.ComputeHash(postBytes)).Replace("-", string.Empty);

        // do the post:
        System.Net.WebClient cl = new System.Net.WebClient();
        cl.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
        cl.Headers.Add("HMAC", hmac);
        cl.Encoding = encoding;

        var ret = new Dictionary<string, object>();
        try
        {
            string resp = cl.UploadString("https://www.coinpayments.net/api.php", post_data);
            var decoder = new System.Web.Script.Serialization.JavaScriptSerializer();
            ret = decoder.Deserialize<Dictionary<string, object>>(resp);
        }
        catch (System.Net.WebException e)
        {
            ret["error"] = "Exception while contacting CoinPayments.net: " + e.Message;
        }
        catch (Exception e)
        {
            ret["error"] = "Unknown exception: " + e.Message;
        }
        return ret;
    }

    public string PurchaseIKeyByCoinpayment(double dblAmount, int IntUserID, int intEpinTypeID, int intEpinCount, double dblEpinPrice, double dblTotPrice )
    {
        string status_url="";
        SortedList<string, string> listParam = new SortedList<string, string>();
        //add the elements in sortedlist
        listParam.Add("amount", (dblAmount).ToString());
        listParam.Add("currency1", "USD");
        listParam.Add("currency2", "BTC");
        listParam.Add("address", "37KfikBRm8q9b6efZ5pKbggQeA8pKfvyCc");
        listParam.Add("item_name", "A-Code Purchase");
        listParam.Add("ipn_url", "https://www.mysuccesswork.com/portal/user/success_bitcoin.aspx");

        var ret = new Dictionary<string, object>();

        ret = CallAPI("create_transaction", listParam);

        string error = ret["error"].ToString();

        if (error == "ok")
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string objectString = serializer.Serialize(ret["result"]);
            BlogSite bsObj = new BlogSite()
            {
                amount = "",
                txn_id = "",
                address = "",
                confirms_needed = "",
                status_url = "",
                qrcode_url = ""
            };

            string amount = "", txn_id = "", address = "", confirms_needed = "",  qrcode_url = "";
            var json = new JavaScriptSerializer().Serialize(ret["result"]);
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(json)))
            {
                // Deserialization from JSON  
                DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(BlogSite));
                BlogSite bsObj2 = (BlogSite)deserializer.ReadObject(ms);
                amount = bsObj2.amount;
                txn_id = bsObj2.txn_id;
                address = bsObj2.address;
                confirms_needed = bsObj2.confirms_needed;
                status_url = bsObj2.status_url;
                qrcode_url = bsObj2.qrcode_url;
            }

            clsWallet objwallet = new clsWallet();
            ODBC clsOdbc = new ODBC();
           
            clsOdbc.executeNonQuery("INSERT INTO `mlm_temp_invest_bitcoin`(`userid`, `amount`, `invest_by`, `error`, `btc_amount`, `txn_id`, `address`, `confirms_needed`, `status_url`, `qrcode_url`, `created_on`, epin_type_id, epin_cost, epin_count, total_cost) VALUES (" + IntUserID + ", " + dblAmount + ", " + IntUserID + ", '" + error + "','" + amount + "','" + txn_id + "','" + address + "','" + confirms_needed + "','" + status_url + "','" + qrcode_url + "','" + objwallet.getCurDateTimeString() + "', " + intEpinTypeID + ", " + dblEpinPrice + "," + intEpinCount + ", " + dblTotPrice + "  ) ");  
        }

        CommonMessages.ShowAlertMessage(error.ToString());
        return status_url;
    }

    public class response
    {
        public string amount;
        public string txn_id;
        public string address;
        public string confirms_needed;
        public string timeout;
        public string status_url;
        public string qrcode_url;
    }
}
