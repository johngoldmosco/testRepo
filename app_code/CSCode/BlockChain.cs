using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestSharp;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;

/// <summary>
/// Summary description for BlockChain
/// </summary>
public class BlockChain
{
	public BlockChain()
	{
		
	}

    public string GenerateReceivingAddress(int intUserID,string strRandom)
    {
        string strcallback = HttpContext.Current.Server.UrlEncode(System.Configuration.ConfigurationManager.AppSettings["CallbackUrl"] + "?invoice_id=" + strRandom + "&secret=ayelsgtksda");

        string strUrl = System.Configuration.ConfigurationManager.AppSettings["receive_url"] + "xpub=" + System.Configuration.ConfigurationManager.AppSettings["XPub"] + "&callback=" + strcallback + "&key=" + System.Configuration.ConfigurationManager.AppSettings["BlockChainKey"];


        WebRequest request = default(WebRequest);

        request = WebRequest.Create(strUrl);

        WebResponse resp = request.GetResponse();

        StreamReader reader = new StreamReader(resp.GetResponseStream());
        string responseString = reader.ReadToEnd();

        var objTrackLink = JObject.Parse(responseString);
        var strResult = objTrackLink["address"].ToString(); 
       
        reader.Close();
        resp.Close();

        return strResult;
    }
    
    public string GenerateQRCode(string strAddress)
    {
        string strUrl = System.Configuration.ConfigurationManager.AppSettings["generate_qr_url"] + "&chl=bitcoin:"+ strAddress +"";


        WebRequest request = default(WebRequest);

        request = WebRequest.Create(strUrl);

        WebResponse resp = request.GetResponse();

        StreamReader reader = new StreamReader(resp.GetResponseStream());
        string responseString = reader.ReadToEnd();

        reader.Close();
        resp.Close();
       
        return "";
    }
    public string GenerateQRCodeAmount(string strAddress,double dblAmt)
    {
        string strUrl = "https://chart.googleapis.com/chart?chs=200x200&cht=qr&chl=bitcoin:" + strAddress + "?amount=" + dblAmt + "";

       /* WebRequest request = default(WebRequest);

        request = WebRequest.Create(strUrl);

        WebResponse resp = request.GetResponse();

        StreamReader reader = new StreamReader(resp.GetResponseStream());
        string responseString = reader.ReadToEnd();

        reader.Close();
        resp.Close();*/

        return strUrl;
    }
    public string MakeSinglePaymentWallet(string rcvBitCoinAddress,double dblAmount)
    {

        string strUrl = System.Configuration.ConfigurationManager.AppSettings["wallet_payment_url"] + "password=" + System.Configuration.ConfigurationManager.AppSettings["wallet_main_password"] + "&second_password=" + System.Configuration.ConfigurationManager.AppSettings["wallet_second_password"] + "&to=" + rcvBitCoinAddress +"&amount="+ dblAmount +"";


        WebRequest request = default(WebRequest);

        request = WebRequest.Create(strUrl);

        WebResponse resp = request.GetResponse();

        StreamReader reader = new StreamReader(resp.GetResponseStream());
        string responseString = reader.ReadToEnd();

        reader.Close();
        resp.Close();

        return "";
    }
	
	 public bool getGapStatus()
    {
        bool boolFlag = false;
        try
        {
            var uri = String.Format("https://api.blockchain.info/v2/receive/checkgap?xpub=" + System.Configuration.ConfigurationManager.AppSettings["XPub"] +"&key=" + System.Configuration.ConfigurationManager.AppSettings["BlockChainKey"]);
            WebClient client = new WebClient();
            client.UseDefaultCredentials = true;
            var data = client.DownloadString(uri);

            var objTrackLink = JObject.Parse(data.ToString());
            int intResult =Int32.Parse(objTrackLink["gap"].ToString());

            if (intResult < 21)
                boolFlag = true;
            else
                boolFlag = false;
        }
        catch (Exception ex) { }
        return boolFlag;
    }
	
	 public string GenerateQRCodeAmount1(string strAddress, double dblAmt)
    {
        string strUrl = "https://chart.googleapis.com/chart?chs=200x200&cht=qr&chl=bitcoin:" + strAddress + "?amount=" + dblAmt + "";


       /* WebRequest request = default(WebRequest);

        request = WebRequest.Create(strUrl);

        WebResponse resp = request.GetResponse();

        StreamReader reader = new StreamReader(resp.GetResponseStream());
        string responseString = reader.ReadToEnd();

        reader.Close();
        resp.Close();*/

        return strUrl;
    }	
	
}