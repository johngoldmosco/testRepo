
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
Imports System.Text
Imports System.IO
Imports System.Security.Cryptography

Namespace QueryStringEncryption
    Public Class EncryptDecryptQueryString
        Private key As Byte() = {}
        Private IV As Byte() = {&H12, &H34, &H56, &H78, &H90, &HAB, _
         &HCD, &HEF}

        Public Function Decrypt(stringToDecrypt As String, sEncryptionKey As String) As String
            Dim inputByteArray As Byte() = New Byte(stringToDecrypt.Length) {}
            Try
                key = System.Text.Encoding.UTF8.GetBytes(sEncryptionKey)
                Dim des As New DESCryptoServiceProvider()
                inputByteArray = Convert.FromBase64String(stringToDecrypt)
                Dim ms As New MemoryStream()
                Dim cs As New CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write)
                cs.Write(inputByteArray, 0, inputByteArray.Length)
                cs.FlushFinalBlock()
                Dim encoding As System.Text.Encoding = System.Text.Encoding.UTF8
                Return encoding.GetString(ms.ToArray())
            Catch e As Exception
                Return e.Message
            End Try
        End Function

        Public Function Encrypt(stringToEncrypt As String, SEncryptionKey As String) As String
            Try
                key = System.Text.Encoding.UTF8.GetBytes(SEncryptionKey)
                Dim des As New DESCryptoServiceProvider()
                Dim inputByteArray As Byte() = Encoding.UTF8.GetBytes(stringToEncrypt)
                Dim ms As New MemoryStream()
                Dim cs As New CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write)
                cs.Write(inputByteArray, 0, inputByteArray.Length)
                cs.FlushFinalBlock()
                Return Convert.ToBase64String(ms.ToArray())
            Catch e As Exception
                Return e.Message
            End Try
        End Function
    End Class
End Namespace

