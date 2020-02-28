Imports Microsoft.VisualBasic

Public Class clsWallet
    Dim clsOdbc As New ODBC

    Dim strdbName As String = System.Configuration.ConfigurationManager.AppSettings("dbName")


    Public Sub UpdateMyBalance(ByVal intUserID As Integer, ByVal dblWallet1 As Double, ByVal dblWallet2 As Double, ByVal dblWallet3 As Double, ByVal dblSerChrg As Double, ByVal dblOD As Double)
        clsOdbc.executeNonQuery("call " & strdbName & ".UpdateMyBalance(" & intUserID & "," & dblWallet1 & "," & dblWallet2 & "," & dblWallet3 & "," & dblSerChrg & "," & dblOD & ")")
    End Sub


    Public Sub UpdateTransactionDetails(ByVal intUserID As Integer, ByVal dblDebitAmount As Double, ByVal dblCreditAmount As Double, ByVal intTransType As Integer, ByVal strDescription As String)
        clsOdbc.executeNonQuery("call " & strdbName & ".UpdateTransactionDetails(" & intUserID & "," & dblDebitAmount & "," & dblCreditAmount & "," & intTransType & ",'" & strDescription & "')")
    End Sub

    Public Sub UpdateTransactionDetailsDatewise(ByVal intUserID As Integer, ByVal StrEndDate As String, ByVal dblWallet1 As Double, ByVal dblWallet2 As Double, ByVal dblWallet3 As Double, ByVal dblSerChrg As Double, ByVal dblOD As Double, ByVal intTransType As Integer, ByVal strDescription As String)
        clsOdbc.executeNonQuery("call " & strdbName & ".UpdateTransactionDetailsDatewise(" & intUserID & ",'" & StrEndDate & "'," & dblWallet1 & "," & dblWallet2 & "," & dblWallet3 & "," & dblSerChrg & "," & dblOD & "," & intTransType & ",'" & strDescription & "')")
    End Sub

    Public Sub getDailyGrowth()
        clsOdbc.executeNonQuery("call " & strdbName & ".dailyGrowth()")
    End Sub
    Public Sub getBinaryIncome()
        clsOdbc.executeNonQuery("call " & strdbName & ".binaryIncome()")
    End Sub
    Public Sub getDirectIncome()
        clsOdbc.executeNonQuery("call " & strdbName & ".directIncome()")
    End Sub
    '**************Get Time of a Specific Time Zone**********************************
    'eg. strTimeZone = "India Standard Time"

    Public Function getTime(ByVal strTimeZone As String) As DateTime
        Dim timeUtc As DateTime = DateTime.UtcNow
        Try
            Dim cstZone As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(strTimeZone)
            Dim cstTime As DateTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone)
            Return cstTime
        Catch generatedExceptionName As TimeZoneNotFoundException
            CommonMessages.ShowAlertMessage("Time Zone Not Found!")
        Catch generatedExceptionName As InvalidTimeZoneException
            CommonMessages.ShowAlertMessage("Invalid Time Zone!")
        End Try
        Return DateTime.Now
    End Function

    Public Function getCurDateTimeString() As String
        Dim dtcurDateTime As DateTime = getTime("India Standard Time")
        Dim strcurDateTime As String = dtcurDateTime.ToString("yyyy-MM-dd HH:mm:ss")
        Return strcurDateTime
    End Function

    Public Function getCurDateString() As String
        Dim dtcurDateTime As DateTime = getTime("India Standard Time")
        Dim strcurDate As String = dtcurDateTime.ToString("yyyy-MM-dd")
        Return strcurDate
    End Function
	
	 Public Function getAusCurDateTimeString() As String
        Dim dtcurDateTime As DateTime = getTime("Australian Central Standard Time")
        Dim strcurDateTime As String = dtcurDateTime.ToString("yyyy-MM-dd HH:mm:ss")
        Return strcurDateTime
    End Function

    Public Function getAusCurDateString() As String
        Dim dtcurDateTime As DateTime = getTime("Australian Central Standard Time")
        Dim strcurDate As String = dtcurDateTime.ToString("yyyy-MM-dd")
        Return strcurDate
    End Function

    '**** Generate Random String *****
    Private Function GenrateRandomPrefixString() As String

        'Dim passwordString As String = clsOdbc.executeScalar_str("SELECT FLOOR(RAND() * 999999)")

        Dim allowedChars As String = ""
        allowedChars += "1,2,3,4,5,6,7,8,9,0"
        Dim sep As Char() = {","c}
        Dim arr As String() = allowedChars.Split(sep)
        Dim passwordString As String = ""
        Dim temp As String = ""
        Dim rand As New Random()
        For i As Integer = 0 To Convert.ToInt32(6) - 1
            temp = arr(rand.[Next](0, arr.Length))
            passwordString += temp
        Next

        Return passwordString

    End Function

End Class
