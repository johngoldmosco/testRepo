Imports Microsoft.VisualBasic
Imports System.Net.Mail
Imports System.Xml
Imports System.Data
Imports System.Net.Http
Imports System.Net
Imports System.IO

Public Class clsCommunication

    Dim objOdbc As New ODBC
    Dim intFlag As Integer = 1    

    Public Sub SMS_API_for_Single_SMS(ByVal strmessage As String, ByVal strNumber As String)
        Dim sURL As String
        Dim strauthkey As String = "SHGIdnXW30ervR9bt1ZE8Q"
        Dim strsenderid As String = "LIFGLD"
        Dim ds As New DataSet
		Dim intCount As Integer

        Try
			intCount = objOdbc.executeScalar_int("SELECT sms_count FROM tbl_smscount WHERE status=1 AND id=1")

            If intCount < 5000 Then
                If intFlag = 1 Then
                    sURL = "http://www.smsgatewayhub.com/api/mt/SendSMS?APIKey=" & strauthkey & "&senderid=" & strsenderid & "&channel=2&DCS=0&flashsms=0&number=" & strNumber & "&text=" & strmessage & "&route=13"

                    Dim sResponse As WebRequest
                    sResponse = WebRequest.Create(sURL)
                    Try
                        Dim objStream As Stream
                        objStream = sResponse.GetResponse.GetResponseStream()

                    Catch ex As Exception

                    End Try

                    intFlag = 2
                End If
                objOdbc.executeNonQuery("UPDATE tbl_smscount SET sms_count=(sms_count + 1) WHERE id=1")

            Else
                objOdbc.executeNonQuery("UPDATE tbl_smscount SET status=2 WHERE id=1")
            End If

        Catch ex As Exception

        Finally
            ds.Dispose()

        End Try

    End Sub   

End Class

