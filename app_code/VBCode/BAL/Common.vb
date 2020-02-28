Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.Odbc
Imports System.IO
Imports System.Web.UI

Public Class Common
    Dim myCommand As OdbcCommand
    Dim myConnection As OdbcConnection

    Public Sub showalert(ByRef Page As Page, ByVal param As String)
        Dim str As String
        str = "<script language='javascript'>alert('" + param + "');</script>"
        ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "sankar", str, False)
    End Sub
    Public Sub closePopupWindow(ByRef Page As Page, ByVal param As String)
        Dim str As String
        str = "<script language='javascript'>opener.window.location.reload(true); window.close();</script>"
        ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "sankar", str, False)
    End Sub
    '*** Append DateTime in File Name and Return ***
    Public Function AppendFileName(ByVal fpFileControl As FileUpload) As String

        Try
            Dim strFileName As String = fpFileControl.FileName.ToString()
            Dim strExtension As String = IO.Path.GetExtension(strFileName)
            Dim strTimeStamp = DateTime.Now.Date.ToString()
            strTimeStamp = strTimeStamp.Replace("/", "-")
            strTimeStamp = strTimeStamp.Replace(" ", "-")
            strTimeStamp = strTimeStamp.Replace(":", "-")
            Dim strName = IO.Path.GetFileNameWithoutExtension(strFileName)
            strFileName = strName + "-" + strTimeStamp + strExtension

            Return strFileName

        Catch ex As Exception
            Return ex.Message.ToString()
        End Try

    End Function
End Class
