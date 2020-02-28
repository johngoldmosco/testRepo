Imports Microsoft.VisualBasic

Public Class clsMailTemplate

    Public Function getEmailVerificationMessage(strUsername As [String], strLink As String, strEmailID As String) As String

        Dim strMailString As String = System.IO.File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("Template/activation.html"))

        strMailString = strMailString.Replace("&&strUser&&", strUsername)
        strMailString = strMailString.Replace("&&strEmailID&&", strEmailID)
        strMailString = strMailString.Replace("&&strActvateLink&&", strLink)
        Return strMailString

    End Function

    Public Function getRegisterMessage(strUsername As [String], strUserID As String, strPassword As String, strEmail As String, strTransPass As String) As String

        Dim strMailString As String = System.IO.File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("Template/register.html"))


        strMailString = strMailString.Replace("&&username&&", strUsername)
        strMailString = strMailString.Replace("&&strUserID&&", strUserID)
        strMailString = strMailString.Replace("&&pass&&", strPassword)
        strMailString = strMailString.Replace("&&email&&", strEmail)
        strMailString = strMailString.Replace("&&transPass&&", strTransPass)
        Return strMailString
    End Function
	
	 Public Function getforPasswordMessage(strUsername As [String], strPass As String) As String

        Dim strMailString As String = System.IO.File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("Template/for_pass.html"))

        strMailString = strMailString.Replace("&&strUserName&&", strUsername)
        strMailString = strMailString.Replace("&&strpassword&&", strPass)

        Return strMailString
    End Function
	
	
     Public Function getTransPasswordMessage(strUsername As [String], strPass As String) As String

        Dim strMailString As String = System.IO.File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("../../Template/master-key.html"))

        strMailString = strMailString.Replace("&&strUser&&", strUsername)
		strMailString = strMailString.Replace("&&strPassword&&", strPass)

        Return strMailString
    End Function
   
    Public Function getFundtransferMessage(strUsername As [String], strLink As String) As String

        Dim strMailString As String = System.IO.File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("Template/emailVerification.html"))


        strMailString = strMailString.Replace("&&username&&", strUsername)
        strMailString = strMailString.Replace("&&link&&", strLink)

        Return strMailString
    End Function
End Class
