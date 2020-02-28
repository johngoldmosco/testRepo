Imports Microsoft.VisualBasic
Imports System.Net.Mail

Public Class clsCommon

    Dim clsOdbc As New ODBC
	
	Public Function CheckExpiryofInvestment(ByVal intUserID As Integer) As Integer
        Dim intReturn As Integer = 0
        Try

            Dim intInvestStatus As Integer = clsOdbc.executeScalar_int("SELECT invest_status from mlm_login WHERE userid =" & intUserID & "")
            Dim intReInvestStatus As Integer = clsOdbc.executeScalar_int("SELECT reinvest_status from mlm_login WHERE userid =" & intUserID & "")

            If intInvestStatus = 1 Then
                Dim intExpInvest As Integer = clsOdbc.executeScalar_int("SELECT count(1) from (select userid, max(date(received_on))as received from mlm_daily_growth where userid=" & intUserID & " and invest_type = 1) t1 where t1.received <= curdate() AND t1.userid=" & intUserID & "")
                If intExpInvest = 1 Then
                    If intReInvestStatus = 1 Then
                        Dim intExpReInvest As Integer = clsOdbc.executeScalar_int("SELECT count(1) from (select userid, max(date(received_on))as received from mlm_daily_growth where userid=" & intUserID & " and invest_type = 2) t1 where t1.received <= curdate() AND t1.userid=" & intUserID & "")
                        If intExpReInvest = 1 Then
                            intReturn = 0
                        Else
                            intReturn = 1
                        End If
                    Else
                        intReturn = 0
                    End If
                Else
                    intReturn = 1
                End If
            Else
                intReturn = 1
            End If

        Catch ex As Exception

        End Try
        Return intReturn
    End Function

    Public Sub FillGridViewData(ByVal strQuery As String, ByVal gvGridView As GridView)

        Dim ds As New Data.DataSet

        Try

            ds = clsOdbc.getDataSet(strQuery)
            If (ds.Tables(0).Rows.Count > 0) Then
                gvGridView.DataSource = ds
                gvGridView.DataBind()
            Else
                CommonMessages.ShowAlertMessage("Sorry, No Records Found!")
                gvGridView.DataSource = Nothing
                gvGridView.DataBind()
            End If
        Catch ex As Exception

        Finally
            ds.Dispose()
        End Try

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

    'Mail Sending using SMTP 
    Public Shared Sub sendEmailMessage(ByVal strFrom As String, ByVal strTO As String, ByVal strSubject As String, ByVal strMessage As String)

        Dim Mailmsg As New MailMessage()

        Dim client As New System.Net.Mail.SmtpClient("s152929.gridserver.com", 25)
        Dim netwrkCrd As New System.Net.NetworkCredential()
        netwrkCrd.UserName = "mailer@exioms.info"
        netwrkCrd.Password = "Sl8l8T1kO6578572"
        client.Credentials = netwrkCrd

        Try
            Mailmsg.[To].Clear()
            Mailmsg.From = New MailAddress(strFrom)
            Mailmsg.[To].Add(New MailAddress(strTO))
            Mailmsg.Subject = strSubject
            Mailmsg.IsBodyHtml = True
            Mailmsg.Body = strMessage
            client.Send(Mailmsg)
            CommonMessages.ShowAlertMessage("You Mail has been successfully Sent!")
        Catch ex As Exception
            CommonMessages.ShowAlertMessage("Error on Sendimg Mail." + ex.Message)
            Return
        End Try
    End Sub

    '**** Change Password *****
    Public Sub ChangePassword(ByVal strPassword As String, ByVal strEmail As String)

        Dim strQuery As String = "UPDATE ihro_login SET password='" & strPassword & "' WHERE userid=" & strEmail
        clsOdbc.executeNonQuery(strQuery)
        CommonMessages.ShowAlertMessage("Password Successfully Changed!")

    End Sub


    '**** Generate Random String *****
    Public Function GenrateRandomString() As String

        Dim allowedChars As String = ""
        allowedChars = "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,"
        allowedChars += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,"
        allowedChars += "1,2,3,4,5,6,7,8,9,0,!,@,#,$,%,&,?"
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

    Public Function GenrateRandomPrefixString() As String

        Dim allowedChars As String = ""
        allowedChars = "1,2,3,4,5,6,7,8,9,0"
        Dim sep As Char() = {","c}
        Dim arr As String() = allowedChars.Split(sep)
        Dim passwordString As String = ""
        Dim temp As String = ""
        Dim rand As New Random()
        For i As Integer = 0 To Convert.ToInt32(3) - 1
            temp = arr(rand.[Next](0, arr.Length))
            passwordString += temp
        Next

        Return passwordString

    End Function
	
	 Public Function GenrateRandomNumberInvoiceString() As String

        Dim allowedChars As String = ""
        allowedChars = "1,2,3,4,5,6,7,8,9,0"
        Dim sep As Char() = {","c}
        Dim arr As String() = allowedChars.Split(sep)
        Dim invoiceString As String = ""
        Dim temp As String = ""
        Dim rand As New Random()
        For i As Integer = 0 To Convert.ToInt32(4) - 1
            temp = arr(rand.[Next](0, arr.Length))
            invoiceString += temp
        Next

        Return invoiceString

    End Function
	
	  Public Function Get_ArrayLlist(ByVal strquery As String) As ArrayList
        Dim objODBC As New ODBC
        Dim ds As New System.Data.DataSet
        Dim arrUserParams As New ArrayList

        Try
            ds = objODBC.getDataSet(strquery)
            If ds.Tables(0).Rows.Count > 0 Then

                arrUserParams.Add("TRUE")

                arrUserParams.Add((ds.Tables(0).Rows.Count).ToString)
                Dim col_count As Integer = ds.Tables(0).Columns.Count

                arrUserParams.Add(col_count)

                Dim firstTable As Data.DataTable = ds.Tables(0)

                For i As Integer = 0 To firstTable.Rows.Count - 1

                    For j As Integer = 0 To firstTable.Columns.Count - 1

                        arrUserParams.Add(firstTable.Rows(i)(j).ToString())

                    Next

                Next

            Else
                arrUserParams.Add("FALSE")

            End If

            Return arrUserParams

        Catch ex As Exception

            arrUserParams.Add(ex.Message.ToString)

            Return arrUserParams
        End Try
    End Function
	
	 Public Function GenrateRandomNumberString() As String

        Dim allowedChars As String = ""
        allowedChars = "1,2,3,4,5,6,7,8,9,0"
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
	
	
 Public Function GenrateRandomOtpString() As String

        Dim allowedChars As String = ""
        allowedChars = "1,2,3,4,5,6,7,8,9,0"
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
