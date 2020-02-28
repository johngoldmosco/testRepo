
Imports System.Data
Imports System.Configuration
Imports System.Linq
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Xml.Linq
Imports QueryStringEncryption

''' <summary>
''' Summary description for Encryption
''' </summary>
Namespace EncryptTest
    Public Class Encryption
        '
        ' TODO: Add constructor logic here
        '
        Public Sub New()
        End Sub
        Public Function EncryptQueryString(strQueryString As String, strKey As String) As String
            Dim objEDQueryString As New EncryptDecryptQueryString()
            Return objEDQueryString.Encrypt(strQueryString, strKey)
            'the key must be only 8 digit in length
        End Function
        Public Function DecryptQueryString(strQueryString As String, strKey As String) As String
            Dim objEDQueryString As New EncryptDecryptQueryString()
            Return objEDQueryString.Decrypt(strQueryString, strKey)
        End Function
        Public Function Decrypt(strReq As String, strKey As String) As String
            Dim check As [Boolean] = strReq.Contains("?")
            If check = True Then
                strReq = strReq.Substring(strReq.IndexOf("?") + 1)

                If Not strReq.Equals("") Then
                    strReq = DecryptQueryString(strReq, strKey)
                    ' decrypting whole query string
                    Dim arrMsgs As String() = strReq.Split("&"c)
                    ' separating all query string parameters in case of multiple parameter passed
                    Dim arrIndMsg As String()
                    Dim roll As String = ""
                    'you can add loops or so to get the values out of the query string...

                    arrIndMsg = arrMsgs(0).Split("="c)
                    'getting roll
                    roll = arrIndMsg(1).ToString().Trim()

                    ' lblRoll.Text = roll;

                    Return roll
                End If
            End If
            Return Nothing
        End Function


        Public Function DecryptSearch(strReq As String, strKey As String) As String
            Dim check As [Boolean] = strReq.Contains("?")
            If check = True Then
                strReq = strReq.Substring(strReq.IndexOf("?") + 1)

                If Not strReq.Equals("") Then
                    strReq = DecryptQueryString(strReq, strKey)
                    ' decrypting whole query string
                    Dim arrMsgs As String() = strReq.Split("&"c)
                    ' separating all query string parameters in case of multiple parameter passed
                    Dim arrIndMsg As String()
                    Dim roll As String = ""
                    'you can add loops or so to get the values out of the query string...

                    arrIndMsg = arrMsgs(0).Split("="c)
                    'getting roll
                    roll = arrIndMsg(1).ToString().Trim()

                    ' lblRoll.Text = roll;

                    Return roll
                End If
            End If
            Return Nothing
        End Function
    End Class
End Namespace

