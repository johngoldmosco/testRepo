Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.Odbc
Imports System.IO
Imports System.Web.UI
Imports System

Public Class LoginBAL

    Dim clsOdbc As New ODBC

    Public Function Check_Login(ByVal strLoginID As String, ByVal strLoginPwd As String) As Boolean

        Dim strQuery As String

        strQuery = "SELECT Count(*) From login Where login_id='" & strLoginID & "' and login_pwd='" & strLoginPwd & "' and Active=1"

        Dim intCount As Integer = clsOdbc.executeScalar_int(strQuery)

        If intCount > 0 Then
            Return True

        Else
            Return False
        End If

    End Function

End Class
