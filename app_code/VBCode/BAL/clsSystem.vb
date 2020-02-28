Imports Microsoft.VisualBasic
Imports System
Imports System.Net.Mail
Imports System.ComponentModel
Imports System.Threading
Imports System.Web.Services

Public Class clsSystem

    Dim clsOdbc As New ODBC

    '**** Fill Date in DropDownList ****
    Public Sub FillDate(ByVal ddlDropDownList As DropDownList)

        ddlDropDownList.Items.Clear()
        ddlDropDownList.Items.Insert(0, "Date")

        For iDate As Integer = 1 To 31
            ddlDropDownList.Items.Insert(iDate, iDate.ToString())
        Next

    End Sub

    '**** Fill Month in DropDownList ****
    Public Sub FillMonth(ByVal ddlDropDownList As DropDownList)

        ddlDropDownList.Items.Clear()
        ddlDropDownList.Items.Insert(0, "Month")

        For iMonth As Integer = 1 To 12
            ddlDropDownList.Items.Insert(iMonth, iMonth.ToString())
        Next

    End Sub

    '**** Fill Year in DropDownList ****
    Public Sub FillYear(ByVal ddlDropDownList As DropDownList)

        ddlDropDownList.Items.Clear()
        ddlDropDownList.Items.Insert(0, "Year")

        For iYear As Integer = 1970 To 2025
            ddlDropDownList.Items.Add(iYear.ToString())
        Next

    End Sub

    '**** Fill Date in DropDownList with Today's Date Seleted ****
    Public Sub FillDateToday(ByVal ddlDropDownList As DropDownList)

        ddlDropDownList.Items.Clear()
        ddlDropDownList.Items.Insert(0, "Date")

        For iDate As Integer = 1 To 31
            ddlDropDownList.Items.Insert(iDate, iDate.ToString())
        Next

        ddlDropDownList.SelectedValue = Today.Day

    End Sub

    '**** Fill Month in DropDownList with Today's Month Seleted ****
    Public Sub FillMonthToday(ByVal ddlDropDownList As DropDownList)

        ddlDropDownList.Items.Clear()
        ddlDropDownList.Items.Insert(0, "Month")

        For iMonth As Integer = 1 To 12
            ddlDropDownList.Items.Insert(iMonth, iMonth.ToString())
        Next

        ddlDropDownList.SelectedValue = Today.Month

    End Sub

    '**** Fill Year in DropDownList with Today's Year Seleted ****
    Public Sub FillYearToday(ByVal ddlDropDownList As DropDownList)

        ddlDropDownList.Items.Clear()
        ddlDropDownList.Items.Insert(0, "Year")

        For iYear As Integer = 1970 To 2025
            ddlDropDownList.Items.Add(iYear.ToString())
        Next

        ddlDropDownList.SelectedValue = Today.Year

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

    '**** Fill City Details Details in DropDownList ****
    Public Sub FillListDetails(ByVal ddlDropDownList As DropDownList)

        Dim strQuery As String
        Dim ds As New Data.DataSet

        strQuery = "SELECT id,list_name From em_list Order By list_name ASC"
        ddlDropDownList.Items.Clear()

        Try
            ds = clsOdbc.getDataSet(strQuery)

            If ds.Tables(0).Rows.Count > 0 Then

                ddlDropDownList.DataSource = ds
                ddlDropDownList.DataTextField = "list_name"
                ddlDropDownList.DataValueField = "id"
                ddlDropDownList.DataBind()
            End If
            ddlDropDownList.Items.Insert(0, "Select List Name")
            ddlDropDownList.SelectedIndex = 0
        Catch ex As Exception

        Finally
            ds.Dispose()
        End Try

    End Sub

    '**** Fill Template  Details Details in DropDownList ****
    Public Sub FillTemplateDetails(ByVal ddlDropDownList As DropDownList)

        Dim strQuery As String
        Dim ds As New Data.DataSet

        strQuery = "SELECT id,temp_name From em_templates Order By temp_name ASC"
        ddlDropDownList.Items.Clear()

        Try
            ds = clsOdbc.getDataSet(strQuery)

            If ds.Tables(0).Rows.Count > 0 Then

                ddlDropDownList.DataSource = ds
                ddlDropDownList.DataTextField = "temp_name"
                ddlDropDownList.DataValueField = "id"
                ddlDropDownList.DataBind()
            End If
            ddlDropDownList.Items.Insert(0, "Select Template Name")
            ddlDropDownList.SelectedIndex = 0
        Catch ex As Exception

        Finally
            ds.Dispose()
        End Try

    End Sub

    '**** Sending Test Mail ****
    Public Sub SendTestMail(ByVal strTo As String, ByVal strFrom As String, ByVal strSubject As String, ByVal strMessage As String)

        Dim Mailmsg As New MailMessage()

        Dim client As New System.Net.Mail.SmtpClient("mail.zestdine.com", 25)
        Dim netwrkCrd As New System.Net.NetworkCredential()
        netwrkCrd.UserName = "mailsend@zestdine.com"
        netwrkCrd.Password = "P@$$wrD{}12"
        client.Credentials = netwrkCrd

        Try
            Mailmsg.[To].Clear()
            Mailmsg.From = New MailAddress(strFrom)
            Mailmsg.[To].Add(New MailAddress(strTo))
            Mailmsg.Subject = strSubject
            Mailmsg.IsBodyHtml = True
            Mailmsg.Body = strMessage.ToString()
            client.Send(Mailmsg)

            clsOdbc.executeNonQuery("INSERT INTO em_testmail (test_email) VALUES ('" & strTo & "')")

            CommonMessages.ShowAlertMessage("You Mail has been successfully Sent!")

        Catch ex As Exception

            clsOdbc.executeNonQuery("INSERT INTO em_testmail (test_email) VALUES ('" & strTo & "')")

            CommonMessages.ShowAlertMessage("Error on Sendimg Mail." + ex.Message)

        End Try

    End Sub

    'Mail Sending using SMTP 
    Public Sub sendEmailMessage(ByVal intCampID As Integer, ByVal intListID As Integer, ByVal strFrom As String, ByVal strSubject As String, ByVal strMessage As String)

        Dim Mailmsg As New MailMessage()

        Dim client As New System.Net.Mail.SmtpClient("mail.zestdine.com", 25)
        Dim netwrkCrd As New System.Net.NetworkCredential()
        netwrkCrd.UserName = "mailsend@zestdine.com"
        netwrkCrd.Password = "P@$$wrD{}12"
        client.Credentials = netwrkCrd

        Dim strQuery As String = "SELECT email_address From em_emails WHERE list_id=" & intListID

        Dim ds As New Data.DataSet

        Try
            ds = clsOdbc.getDataSet(strQuery)

            If ds.Tables(0).Rows.Count > 0 Then

                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

                    Dim strTo As String = ds.Tables(0).Rows(i).Item(0).ToString()

                    Try
                        Mailmsg.[To].Clear()
                        Mailmsg.From = New MailAddress(strFrom)
                        Mailmsg.[To].Add(New MailAddress(strTo))
                        Mailmsg.Subject = strSubject
                        Mailmsg.IsBodyHtml = True
                        Mailmsg.Body = strMessage
                        client.Send(Mailmsg)

                        clsOdbc.executeNonQuery("INSERT INTO em_mail_status(camp_id,email,status) VALUES ('" & intCampID & "','" & strTo & "',1)")

                    Catch ex As Exception
                        clsOdbc.executeNonQuery("INSERT INTO em_mail_status(camp_id,email,status) VALUES ('" & intCampID & "','" & strTo & "',2)")
                    End Try

                Next

                clsOdbc.executeNonQuery("UPDATE em_campaign SET status=2 WHERE id=" & intCampID)

            End If

        Catch ex As Exception

        Finally
            ds.Dispose()
        End Try

    End Sub

    '***** Add New Login Details ****
    Public Sub RegisterUser(ByVal strEmail As String, ByVal strUserName As String, ByVal strPwd As String, ByVal strSName As String, ByVal strSID As String, ByVal strFName As String, ByVal strLName As String, ByVal strDOB As String, ByVal strCountryCode As String, ByVal strPhone As String, ByVal strPAN As String, ByVal strAddress1 As String, ByVal intGender As Integer, ByVal lblStatus As Label)

        Dim intCount As Integer = clsOdbc.executeScalar_str("SELECT Count(*) From bk_login WHERE (Email='" & strEmail & "' OR UserName='" & strUserName & "') and Active=1")

        If intCount > 0 Then
            lblStatus.Text = "* UserName/E-mail is Already Registered with us!"
            'CommonMessages.ShowAlertMessage("UserName/E-mail is Already Registered with us!")
            Exit Sub
        Else

            Dim intSponsarCount As Integer = clsOdbc.executeScalar_int("SELECT Count(*) From bk_sponsar WHERE my_sponsar_id='" & strSID & "'")
            If intSponsarCount > 0 Then

                Try

                    Dim strLoginQuery As String = "INSERT INTO bk_login(Email,UserName,Password,LoginTypeId,plan_type) VALUES ('" & strEmail & "','" & strUserName & "','" & strPwd & "',2,2)"
                    clsOdbc.executeNonQuery(strLoginQuery)

                    Dim strUserID As String = clsOdbc.executeScalar_str("SELECT MAX(UserId) From bk_login WHERE Email='" & strEmail & "'")

                    Dim intCountUser As Integer = clsOdbc.executeScalar_int("SELECT Count(1) FROM bk_login WHERE plan_type=2")

                    Dim strMySponsarID As String = "EBK" & intCountUser + 1

                    Dim strSponsarQuery As String = "INSERT INTO bk_sponsar(userid,my_sponsar_id,sponsar_name,sponsar_id) vALUES ('" & strUserID & "','" & strMySponsarID & "','" & strSName & "','" & strSID & "')"
                    clsOdbc.executeNonQuery(strSponsarQuery)

                    Dim strPersonalQuery As String = "INSERT INTO bk_personal_details(userid,name,last_name,date_birth,country_code,phone,pan_number,address1,gender) VALUES ('" & strUserID & "','" & strFName & "','" & strLName & "','" & strDOB & "','" & strCountryCode & "','" & strPhone & "','" & strPAN & "','" & strAddress1 & "'," & intGender & ")"
                    clsOdbc.executeNonQuery(strPersonalQuery)

                    Dim strPaymentQuery As String = "INSERT INTO bk_payment_details(userid) vALUES ('" & strUserID & "')"
                    clsOdbc.executeNonQuery(strPaymentQuery)

                    'SponsarPayment(strSID)

                    lblStatus.Text = "* Thanks for Registration, Your Account will be Activated within 24 Hours!"
                    'CommonMessages.ShowAlertMessage("Thanks for Registration, Your Account will be Activated within 24 Hours!")
                Catch ex As Exception

                End Try

            Else
                lblStatus.Text = "* This Sponsars ID Doesn't Exist!"
                'CommonMessages.ShowAlertMessage("This Sponsars ID Doesn't Exist!")
                Exit Sub
            End If

        End If

    End Sub

    '**** Sponsar Payment Details ****
    Public Sub SponsarPayment(ByVal strSopnsarID As String)

        'Dim intSponsarCount As String = "SELECT Count(*)As sponsar_count From bk_sponsar WHERE sponsar_id='" & strSopnsarID & "'"

        'If intSponsarCount Mod 2 = 0 Then

        Dim intUseID As Integer = clsOdbc.executeScalar_int("SELECT userid From bk_sponsar WHERE my_sponsar_id='" & strSopnsarID & "'")

        'Dim intUseIDCount As String = clsOdbc.executeScalar_int("SELECT Count(1) From bk_make_payment WHERE userid ='" & intUseID & "'")

        'If intUseIDCount > 0 Then
            'clsOdbc.executeNonQuery("UPDATE bk_make_payment SET total_amount=total_amount+4 WHERE userid=" & intUseID)
       ' Else
            'Dim strPaymentQuery As String = "INSERT INTO bk_make_payment(userid,total_amount) VALUES ('" & intUseID & "',4)"
            'clsOdbc.executeNonQuery(strPaymentQuery)

        'End If


        For i As Integer = 1 To 13

            Dim intUserID As Integer = intUseID

            If intUserID > 0 Then
			
				if i = 1 Then
					MakeTrinaryPayment(intUserID, 4)
					
				ElseIf i = 2 Then
					MakeTrinaryPayment(intUserID, 3)
				ElseIf i = 3 Then
					MakeTrinaryPayment(intUserID, 2)
				
				Else
				
					MakeTrinaryPayment(intUserID, 1)
				
				End If

                

                Dim strSpID As String = clsOdbc.executeScalar_str("SELECT sponsar_id From bk_sponsar Where userid=" & intUserID)

                intUseID = clsOdbc.executeScalar_int("SELECT userid From bk_sponsar WHERE my_sponsar_id='" & strSpID & "'")


                'Dim intTotalMemberCount As Integer = TrinayCount(strSpID, i, 0)

                'If intTotalMemberCount = 0 Then

                '    Exit For

                'End If

                'If i = 1 And intTotalMemberCount = Math.Pow(3, i) Then

                '    Dim myAmount As Integer = intTotalMemberCount * 5

                '    MakeTrinaryPayment(intUserID, myAmount)

                '    intUseID = clsOdbc.executeScalar_int("SELECT userid From bk_sponsar WHERE sponsar_id='" & strSpID & "'")

                'End If

            Else
                Exit For
            End If
        Next

    End Sub

    Private Sub MakeTrinaryPayment(ByVal intUserID As Integer, ByVal intAmount As Integer)

        Dim intUseIDCount As String = clsOdbc.executeScalar_int("SELECT Count(1) From bk_make_payment WHERE userid ='" & intUserID & "'")

        If intUseIDCount > 0 Then
            clsOdbc.executeNonQuery("UPDATE bk_make_payment SET total_amount=total_amount+" & intAmount & " WHERE userid=" & intUserID)
        Else
            Dim strPaymentQuery As String = "INSERT INTO bk_make_payment(userid,total_amount) vALUES ('" & intUserID & "'," & intAmount & ")"
            clsOdbc.executeNonQuery(strPaymentQuery)

        End If

    End Sub


    '**** Sponsar Payment Details ****
    Public Sub PayCommission(ByVal strSopnsarID As String, ByVal dblAmt As Integer)

        'Dim intSponsarCount As String = "SELECT Count(*)As sponsar_count From bk_sponsar WHERE sponsar_id='" & strSopnsarID & "'"

        'If intSponsarCount Mod 2 = 0 Then

        Dim intUseID As Integer = clsOdbc.executeScalar_int("SELECT userid From bk_sponsar WHERE my_sponsar_id='" & strSopnsarID & "'")



        'Dim intUseIDCount As String = clsOdbc.executeScalar_int("SELECT Count(1) From bk_make_payment WHERE userid ='" & intUseID & "'")

        'If intUseIDCount > 0 Then
        'clsOdbc.executeNonQuery("UPDATE bk_make_payment SET total_amount=total_amount+4 WHERE userid=" & intUseID)
        ' Else
        'Dim strPaymentQuery As String = "INSERT INTO bk_make_payment(userid,total_amount) VALUES ('" & intUseID & "',4)"
        'clsOdbc.executeNonQuery(strPaymentQuery)

        'End If

        Dim ChildId As Integer = intUseID
        For i As Integer = 1 To 13

            Dim intUserID As Integer = intUseID

            If intUserID > 0 Then

                Dim dblCommission As Double = dblAmt * 0.1 / 100

                makePaymentCommission(intUserID, ChildId, dblAmt, dblCommission)


                Dim strSpID As String = clsOdbc.executeScalar_str("SELECT sponsar_id From bk_sponsar Where userid=" & intUserID)

                intUseID = clsOdbc.executeScalar_int("SELECT userid From bk_sponsar WHERE my_sponsar_id='" & strSpID & "'")


                'Dim intTotalMemberCount As Integer = TrinayCount(strSpID, i, 0)

                'If intTotalMemberCount = 0 Then

                '    Exit For

                'End If

                'If i = 1 And intTotalMemberCount = Math.Pow(3, i) Then

                '    Dim myAmount As Integer = intTotalMemberCount * 5

                '    MakeTrinaryPayment(intUserID, myAmount)

                '    intUseID = clsOdbc.executeScalar_int("SELECT userid From bk_sponsar WHERE sponsar_id='" & strSpID & "'")

                'End If

            Else
                Exit For
            End If
        Next

    End Sub
    Private Sub makePaymentCommission(ByVal intUserID As Integer, ByVal intChildID As Integer, ByVal dblRechargeAmt As Double, ByVal dblCommission As Double)

        Dim intUseIDCount As String = clsOdbc.executeScalar_int("SELECT Count(1) From bk_make_payment WHERE userid ='" & intUserID & "'")

        If intUseIDCount > 0 Then
            clsOdbc.executeNonQuery("UPDATE bk_make_payment SET total_amount=total_amount+" & dblCommission & " WHERE userid=" & intUserID)
        Else
            Dim strPaymentQuery As String = "INSERT INTO bk_make_payment(userid,total_amount) vALUES ('" & intUserID & "'," & dblCommission & ")"
            clsOdbc.executeNonQuery(strPaymentQuery)

        End If
        clsOdbc.executeNonQuery("INSERT INTO bk_commission(userid,child_id,recharge_amount,commission,created_on) VALUES(" & intUserID & ", " & intChildID & "," & dblRechargeAmt & ", " & dblCommission & ",NOW() )")

    End Sub

    Private Function TrinayCount(ByVal strSponsarID As String, ByVal intLevel As Integer, ByVal intStart As Integer) As Integer

        If intLevel = 1 Then

            Dim intMemberCount As Integer = clsOdbc.executeScalar_int("SELECT Count(1) From bk_sponsar Where sponsar_id='" & strSponsarID & "'")

            intStart = intStart + intMemberCount
        Else

            Dim strQuery As String = "SELECT my_sponsar_id From bk_sponsar Where sponsar_id='" & strSponsarID & "' Order By id ASC LIMIT 3 "
            Dim ds As New Data.DataSet

            Try

                ds = clsOdbc.getDataSet(strQuery)

                If ds.Tables(0).Rows.Count > 0 Then

                    For jCount As Integer = 0 To ds.Tables(0).Rows.Count - 1

                        intStart = intStart + TrinayCount(ds.Tables(0).Rows(0).Item(0).ToString, intLevel - 1, intStart)

                    Next


                End If

            Catch ex As Exception

            End Try

        End If

        Return intStart

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

    '**** Change Password *****
    Public Sub ChangePassword(ByVal strPassword As String, ByVal strEmail As String)

        Dim strQuery As String = "UPDATE bk_login SET Password='" & strPassword & "' WHERE Email='" & strEmail & "'"
        clsOdbc.executeNonQuery(strQuery)
        CommonMessages.ShowAlertMessage("Password Successfully Changed!")

    End Sub

    Public Function MyTotalEarning(ByVal strUserID As String) As String
        Try
            Return clsOdbc.executeScalar_dbl("SELECT SUM(amount_paid) From bk_amount_details WHERE userid=" & strUserID).ToString()

        Catch ex As Exception
            Return "0.00"
        End Try


    End Function

    Public Function MyAccountBalance(ByVal strUserID As String) As String

        Try
            Return clsOdbc.executeScalar_dbl("SELECT total_amount From bk_make_payment WHERE userid=" & strUserID).ToString()

        Catch ex As Exception
            Return "0.00"
        End Try

    End Function

    Public Function MySponsarID(ByVal strUserID As String) As String

        Return clsOdbc.executeScalar_str("SELECT my_sponsar_id From bk_sponsar WHERE userid=" & strUserID)

    End Function

    Public Function MyJoinDate(ByVal strUserID As String) As String

        Return clsOdbc.executeScalar_str("SELECT DATE_FORMAT(created_on,'%d %M %Y') As Created_On From bk_login WHERE userid=" & strUserID)

    End Function

    Public Function MyMembers(ByVal strUserID As String) As String

        Dim strSponsarID As String = clsOdbc.executeScalar_str("SELECT my_sponsar_id From bk_sponsar WHERE userid=" & strUserID)

        Dim strMemberCOunt As Integer = clsOdbc.executeScalar_int("SELECT Count(1) From bk_sponsar WHERE sponsar_id='" & strSponsarID & "'")

        Return strMemberCOunt

    End Function

    Public Function MyUserName(ByVal strUserID As String) As String

        Return clsOdbc.executeScalar_str("SELECT name From bk_personal_details WHERE UserId=" & strUserID)

    End Function

    Public Sub TodayPaid(ByVal lblTPaid As Label)

        Try
            Dim strTPaid = clsOdbc.executeScalar_str("SELECT SUM(amount_paid) FROM bk_amount_details WHERE DATE(amount_paid_date) = CURDATE()")
            If strTPaid <> "" Then
                lblTPaid.Text = strTPaid
            Else
                lblTPaid.Text = "0.00"
            End If
        Catch ex As Exception
            lblTPaid.Text = "0.00"
        End Try

    End Sub

    Public Sub YesterdayPaid(ByVal lblYPaid As Label)

        Try
            Dim strYPaid As String = clsOdbc.executeScalar_str("SELECT SUM(amount_paid) FROM bk_amount_details WHERE DATE(amount_paid_date) = CURDATE() - 1")

            If strYPaid <> "" Then
                lblYPaid.Text = strYPaid
            Else
                lblYPaid.Text = "0.00"
            End If

        Catch ex As Exception
            lblYPaid.Text = "0.00"
        End Try

    End Sub

    Public Function TotalRegisterUser() As String

        Try
            Dim intCount As Integer = clsOdbc.executeScalar_int("SELECT Count(1) From bk_login Where Active=1")
            Dim intTotalCount As Integer = intCount - 1
            Return intTotalCount
        Catch ex As Exception
            Return "0"
        End Try

    End Function

    Public Function TotalMailSent() As String

        Try
            Dim intCount As Integer = clsOdbc.executeScalar_int("SELECT Count(1) From bk_mail_details")
            Return intCount
        Catch ex As Exception
            Return "0"
        End Try

    End Function
    Public Sub filldropdownlist(ByVal selQuery As String, ByVal ddId As DropDownList, ByVal dataTxtField As String, ByVal dataValField As String)
        Dim ds As New Data.DataSet()
        ddId.Items.Clear()
        Try
            ds = clsOdbc.getDataSet(selQuery)
            If ds.Tables(0).Rows.Count > 0 Then
                ddId.DataSource = ds
                ddId.DataTextField = dataTxtField
                ddId.DataValueField = dataValField
                ddId.DataBind()
            Else
                ddId.DataSource = Nothing
                ddId.DataBind()
            End If
        Catch ex As Exception
        Finally
            ds.Dispose()
            ddId.Items.Insert(0, "Select")
            ddId.SelectedIndex = 0
        End Try


    End Sub

End Class
