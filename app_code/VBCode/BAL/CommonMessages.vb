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
Imports System.IO

''' <summary>
''' Summary description for CommonMessages
''' </summary>
Public Class CommonMessages
    '
    ' TODO: Add constructor logic here
    '
    Public Sub New()
    End Sub
    Public Shared Sub ShowAlertMessage(ByVal message As String)


        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)

        If page IsNot Nothing Then

            'error = error.Replace("'", "\'");
            'ScriptManager.RegisterStartupScript(page, page.GetType(), "err_msg", "alert('" + error + "');", true);
            message = message.Replace("'", "''")


            page.RegisterStartupScript("Alert", [String].Format("<script DEFER : TRUE LANGUAGE = JavaScript> alert('{0}');</script>", message))
        End If

    End Sub

    'function for show alert and then reload the page
    Public Shared Sub ShowAlertMessage_Reload(ByVal message As String, ByVal URL As String)

        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)

        If page IsNot Nothing Then

            'error = error.Replace("'", "\'");
            message = message.Replace("'", "''")


            'ScriptManager.RegisterStartupScript(page, page.GetType(), "err_msg", "alert('" + error + "');window.location='" + URL + "';", true);

            page.RegisterStartupScript("Alert", [String].Format("<script DEFER : TRUE LANGUAGE = JavaScript> alert('{0}');window.location='" & URL & "';</script>", message))
        End If

    End Sub
End Class