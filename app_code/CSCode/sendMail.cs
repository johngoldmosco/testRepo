using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;

/// <summary>
/// Summary description for sendMail
/// </summary>
public class sendMail
{
    ODBC obj = new ODBC();

    public sendMail()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public void sendMailSendGrid(string strMailTO, string strToName, string strMailFrom, string strFromName, string strSubject, string strMessage)
    {
        try
        {
            MailMessage mailMsg = new MailMessage();

            mailMsg.To.Add(new MailAddress(strMailTO, strToName));

            mailMsg.From = new MailAddress(strMailFrom, strFromName);

            mailMsg.Subject = strSubject;
            string text = @strMessage;
            string html = @strMessage;
            mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
            mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));

            SmtpClient smtpClient = new SmtpClient("69.30.198.45", Convert.ToInt32(25));
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("hr@lifegoldecom.com", "123@123ABC");
            smtpClient.Credentials = credentials;

            smtpClient.Send(mailMsg);

        }
        catch (Exception ex) { CommonMessages.ShowAlertMessage(ex.Message); }
    }
}