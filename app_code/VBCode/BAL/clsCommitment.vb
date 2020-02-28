Imports Microsoft.VisualBasic
Imports System.Globalization
Imports System.Data

Public Class clsCommitment

    Dim clsOdbc As New ODBC
    Dim objOther As New ClassOther
    Dim objWallet As New clsWallet

    Dim strdbName As String = System.Configuration.ConfigurationManager.AppSettings("dbName")

    Dim TodaysDate As String = DateTime.Now.ToString("yyyy-MM-dd")

    '***** Add New Help Someone *****
    Public Function Add_New_Commitment(ByVal intCommitAmount As Integer, ByVal intUserID As Integer, ByVal intCommitType As Integer) As Boolean

        Dim row_PensingComitCount = clsOdbc.executeScalar_int("SELECT Count(1) As UserCount From mlm_commitment Where status in (1,2,4,5) and providing_id=" & intUserID)

        If row_PensingComitCount > 0 Then

            'Return "Your Commitment Status is Not Confirmed!"

            CommonMessages.ShowAlertMessage("Your Commitment Status is Not Confirmed!")

            Exit Function

        Else

            Dim row_UserCount As Integer = clsOdbc.executeScalar_int("SELECT Count(1) As UserCount From mlm_provide_help Where amount_remaining > 0 and userid=" & intUserID)

            If row_UserCount > 0 Then

                'Return "You have Pending Commitment Amount!"

                CommonMessages.ShowAlertMessage("You have Pending Commitment Amount!")

                Exit Function
            Else

                Dim intCommitmentAmoint As Integer = intCommitAmount

                'Dim auto_status As Integer = clsOdbc.executeScalar_int("SELECT auto_status FROM vb_commit_mngr WHERE id=1")

                ' clsOdbc.executeNonQuery("UPDATE mlm_login SET upgrade_status=1 WHERE userid=" & intUserID)

                Dim strDt As String = objWallet.getCurDateString()

                Dim strCommitmentID As String = GenrateRandomPrefixString() & intUserID

                clsOdbc.executeNonQuery("INSERT INTO mlm_provide_help(commit_id,userid,commit_amount,actual_amount,amount_remaining,commit_date,commit_type,auto_status) values ('" & strCommitmentID & "'," & intUserID & "," & intCommitmentAmoint & "," & intCommitmentAmoint & "," & intCommitmentAmoint & ",'" & strDt & "'," & intCommitType & ",1)")

                Dim intMAXCommitID As Integer = clsOdbc.executeScalar_int("SELECT MAX(id) From mlm_provide_help Where userid=" & intUserID)

                add_new_commit_link(intCommitmentAmoint, intUserID, intMAXCommitID)


                CommonMessages.ShowAlertMessage("Your Commitment has been successfully Done!")

                Return True

            End If
        End If

    End Function

    '**** Generate Random String *****
    Private Function GenrateRandomPrefixString() As String

        Dim passwordString As String = clsOdbc.executeScalar_str("SELECT FLOOR(RAND() * 999999)")

        Return passwordString

    End Function

 
    Public Sub add_new_commit_link(ByVal intCommitAmount As Integer, ByVal intUserID As Integer, ByVal intCommitID As Integer)

          If intCommitAmount > 0 Then

            Dim strDtTime As String = objWallet.getCurDateTimeString()

            Dim rcvCount As Integer = clsOdbc.executeScalar_int("SELECT COUNT(1) From mlm_request a, mlm_login b Where a.ask_amount > 0 and b.status=1 and a.userid=b.userid and b.UserTypeId=2 Order By a.id ASC LIMIT 1")
            If rcvCount > 0 Then
                Dim strQuery As String = "SELECT a.userid,a.ask_amount,a.id From mlm_request a,mlm_login b Where   a.ask_amount > 0 and b.status=1 and a.userid=b.userid and b.UserTypeId=2 Order By a.id ASC LIMIT 1"
                Dim ds1 As New System.Data.DataSet()
                Try
                    ds1 = clsOdbc.getDataSet(strQuery)
                    If ds1.Tables(0).Rows.Count > 0 Then
                        Dim rcvr_id As Integer = Convert.ToInt32(ds1.Tables(0).Rows(0)(0).ToString())
                        Dim ask_amt_rem As Integer = Convert.ToInt32(ds1.Tables(0).Rows(0)(1).ToString())
                        Dim rcv_help_id As Integer = Convert.ToInt32(ds1.Tables(0).Rows(0)(2).ToString())

                        If ask_amt_rem <= intCommitAmount Then

                            clsOdbc.executeNonQuery("INSERT INTO mlm_commitment(providing_id,asking_id,amount,helpsomeone_id,helpme_id,date_assigned) values (" & intUserID & "," & rcvr_id & "," & ask_amt_rem & "," & intCommitID & "," & rcv_help_id & ",'" & strDtTime & "')")
                            clsOdbc.executeNonQuery("UPDATE mlm_request SET ask_amount=ask_amount-" & ask_amt_rem & " Where id=" & rcv_help_id)

                            clsOdbc.executeNonQuery("UPDATE mlm_user_invest SET amount_remaining=amount_remaining-" & ask_amt_rem & " Where id=" & intCommitID)

                            intCommitAmount = intCommitAmount - ask_amt_rem
                            add_new_commit_link(intCommitAmount, intUserID, intCommitID)


                        Else
                            clsOdbc.executeNonQuery("INSERT INTO mlm_commitment(providing_id,asking_id,amount,helpsomeone_id,helpme_id,date_assigned) values (" & intUserID & "," & rcvr_id & "," & intCommitAmount & "," & intCommitID & "," & rcv_help_id & ",'" & strDtTime & "')")
                            clsOdbc.executeNonQuery("UPDATE mlm_request SET ask_amount=ask_amount-" & intCommitAmount & " Where id=" & rcv_help_id)

                            clsOdbc.executeNonQuery("UPDATE mlm_user_invest SET amount_remaining=amount_remaining-" & intCommitAmount & " Where id=" & intCommitID)
                        End If


                    End If
                Catch ex As Exception
                Finally
                    ds1.Dispose()

                End Try

            End If
        End If

    End Sub

    Public Sub add_new_commit_link_donor(ByVal intHelpMeAmount As Integer, ByVal intUserID As Integer, ByVal intHelpMeID As Integer)

       If intHelpMeAmount > 0 Then

            Dim providerCount As Integer = clsOdbc.executeScalar_int("SELECT COUNT(1) From mlm_provide_help a, mlm_login b Where a.amount_remaining > 0 and b.status=1 and a.userid=b.userid Order By a.id ASC LIMIT 1")
			
			 Dim strDtTime As String = objWallet.getCurDateTimeString()

            If providerCount > 0 Then
                Dim strQuery As String = "SELECT a.userid,a.amount_remaining,a.id From mlm_provide_help a,mlm_login b Where a.amount_remaining > 0 and b.status=1 and a.userid=b.userid Order By a.id ASC LIMIT 1"
                Dim ds1 As New System.Data.DataSet()
                Try
                    ds1 = clsOdbc.getDataSet(strQuery)
                    If ds1.Tables(0).Rows.Count > 0 Then
                        Dim provider_id As Integer = Convert.ToInt32(ds1.Tables(0).Rows(0)(0).ToString())
                        Dim amt_rem As Integer = Convert.ToInt32(ds1.Tables(0).Rows(0)(1).ToString())
                        Dim provide_help_id As Integer = Convert.ToInt32(ds1.Tables(0).Rows(0)(2).ToString())

                        If amt_rem > intHelpMeAmount Then

                            clsOdbc.executeNonQuery("INSERT INTO mlm_commitment(providing_id,asking_id,amount,helpsomeone_id,helpme_id,date_assigned) values (" & provider_id & "," & intUserID & "," & intHelpMeAmount & "," & provide_help_id & "," & intHelpMeID & ",'" & strDtTime & "')")
                            clsOdbc.executeNonQuery("UPDATE mlm_request SET status=1,ask_amount=ask_amount-" & intHelpMeAmount & " Where id=" & intHelpMeID)

                            clsOdbc.executeNonQuery("UPDATE mlm_user_invest SET amount_remaining=amount_remaining-" & intHelpMeAmount & " Where id=" & provide_help_id)



                        Else
                            clsOdbc.executeNonQuery("INSERT INTO mlm_commitment(providing_id,asking_id,amount,helpsomeone_id,helpme_id,date_assigned) values (" & provider_id & "," & intUserID & "," & amt_rem & "," & provide_help_id & "," & intHelpMeID & ",'" & strDtTime & "')")
                            clsOdbc.executeNonQuery("UPDATE mlm_request SET ask_amount=ask_amount-" & amt_rem & " Where id=" & intHelpMeID)

                            clsOdbc.executeNonQuery("UPDATE mlm_user_invest SET amount_remaining=amount_remaining-" & amt_rem & " Where id=" & provide_help_id)

                            intHelpMeAmount = intHelpMeAmount - amt_rem
                            add_new_commit_link_donor(intHelpMeAmount, intUserID, intHelpMeID)


                        End If


                    End If
                Catch ex As Exception
                Finally
                    ds1.Dispose()

                End Try

            End If

        End If



    End Sub
    '***** Confirm Payment Status by Help Me Member *****
    Public Sub add_Update_Payment_Status(ByVal intCommitmentID As Integer)

        Try

            Dim strDtTime As String = objWallet.getCurDateTimeString()
			
			Dim strDt As String = objWallet.getCurDateString()

            clsOdbc.executeNonQuery("UPDATE mlm_commitment SET status=3,date_confirmed='" & strDtTime & "' Where id=" & intCommitmentID)

            Dim strQuery As String = "SELECT providing_id,asking_id,amount,helpsomeone_id,helpme_id From mlm_commitment WHERE id=" & intCommitmentID

            Dim ds As New Data.DataSet

            Try
                ds = clsOdbc.getDataSet(strQuery)

                If ds.Tables(0).Rows.Count > 0 Then

                    Dim providing_id As Integer = ds.Tables(0).Rows(0).Item(0).ToString
                    Dim asking_id As Integer = ds.Tables(0).Rows(0).Item(1).ToString
                    Dim amount As Integer = ds.Tables(0).Rows(0).Item(2).ToString

                    Dim helpsomeone_id As Integer = ds.Tables(0).Rows(0).Item(3).ToString
                    Dim helpme_id As Integer = ds.Tables(0).Rows(0).Item(4).ToString

                    clsOdbc.executeNonQuery("UPDATE mlm_my_balance SET recieved_amount=recieved_amount+" & amount & "  Where userid=" & asking_id)
                    clsOdbc.executeNonQuery("UPDATE mlm_request SET ask_recieve_amount = ask_recieve_amount+" & amount & " Where id=" & helpme_id)

                    Dim intStatusAskAmount As Integer = clsOdbc.executeScalar_int("SELECT ask_actual_amount-ask_recieve_amount From mlm_request Where id=" & helpme_id)

                    If intStatusAskAmount = 0 Then

                        clsOdbc.executeNonQuery("UPDATE mlm_request SET status=1 Where id=" & helpme_id)


                    End If
                    Dim intStatusProvider As Integer = clsOdbc.executeScalar_int("SELECT Count(1) From mlm_commitment Where status in (1,2,4,5) and helpsomeone_id=" & helpsomeone_id)

                    If intStatusProvider = 0 Then

                        Dim dblAmtRem As Double = clsOdbc.executeScalar_dbl("SELECT amount_remaining From mlm_user_invest Where id=" & helpsomeone_id)

                        If dblAmtRem = 0 Then

                            clsOdbc.executeNonQuery("UPDATE mlm_login SET pdt_status=1 WHERE userid=" & providing_id)
                            clsOdbc.executeNonQuery("UPDATE mlm_user_invest SET investment_status=1,confirmed_on='" & strDtTime & "' WHERE id=" & helpsomeone_id)
                            Dim dblCommitAmt As Double = clsOdbc.executeScalar_dbl("SELECT actual_amount FROM mlm_user_invest Where id=" & helpsomeone_id)
                            clsOdbc.executeNonQuery("INSERT INTO mlm_commitment_completed(helpsomeone_id,userid,commitment_amount,commitment_completed_date) VALUES(" & helpsomeone_id & "," & providing_id & "," & dblCommitAmt & ",'" & strDtTime & "')")
							
							objWallet.UpdateMyBalance(providing_id,0,dblCommitAmt,0,0,0)
							
							objWallet.UpdateTransactionDetailsDatewise(providing_id,strDt,0,dblCommitAmt,0,0,0,11,"Investement successfully completed !")

                            update_Direct_Income(helpsomeone_id)
                        End If

                    End If
                End If

            Catch ex As Exception

            Finally
                ds.Dispose()

            End Try

        Catch ex As Exception

        End Try

    End Sub

    '*** Update Direct Income to Parent ID ***
    Private Sub update_Direct_Income(ByVal helpsomeone_id As Integer)

        Dim strQueryHelpSomeone As String = "SELECT userid,actual_amount From mlm_user_invest Where id=" & helpsomeone_id

        Dim ds As New Data.DataSet

        Dim intUserID, intCommitAmount As Integer

        Try
            ds = clsOdbc.getDataSet(strQueryHelpSomeone)

            If ds.Tables(0).Rows.Count > 0 Then

                intUserID = CType(ds.Tables(0).Rows(0).Item(0).ToString, Integer)
                intCommitAmount = CType(ds.Tables(0).Rows(0).Item(1).ToString, Integer)

                Dim objWallet As New clsWallet

                Dim strDate As String = objWallet.getCurDateString()
                Dim strDtTime As String = objWallet.getCurDateTimeString()

                Dim objMlm As New clsMLM
                Dim intPid As Integer = objMlm.get_Direct_Referral_UID(intUserID)
				
				Dim intInvestCount As Integer = clsOdbc.executeScalar_int("SELECT COUNT(1) From mlm_user_invest Where investment_status=1 and userid=" & intPid)
				
				If intInvestCount > 0 Then
				
					clsOdbc.executeNonQuery("INSERT INTO mlm_direct_income(userid,child_id,invest_amt,amt,created_on) VALUES(" & intPid & "," & intUserID & "," & intCommitAmount & "," & intCommitAmount / 10 & ",'" & strDtTime & "')")
				
				End If

                

                clsOdbc.executeNonQuery("Call " & strdbName & ".UpdateBinary(" & intUserID & "," & intCommitAmount & ")")

            End If

        Catch ex As Exception

        Finally
            ds.Dispose()
        End Try

    End Sub

    '**** Check Commitment CountDown ******
    Public Sub Check_CommitmentCountDown()

        Dim strDtTime As String = objWallet.getCurDateTimeString()

        Dim strQuery As String = "SELECT id,(24 - TIMESTAMPDIFF(HOUR, date_assigned,'" & strDtTime & "')) as time_remain,extend_status From mlm_commitment Where status=1"
        Dim ds As New Data.DataSet

        Try
            ds = clsOdbc.getDataSet(strQuery)

            If ds.Tables(0).Rows.Count > 0 Then

                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

                    Dim intID As Integer = ds.Tables(0).Rows(i).Item(0).ToString
                    Dim time_remain As Integer = ds.Tables(0).Rows(i).Item(1).ToString
                    Dim extend_status As Integer = ds.Tables(0).Rows(i).Item(2).ToString

                    If extend_status = 2 Then
                        clsOdbc.executeNonQuery("UPDATE mlm_commitment SET time_remain=" & time_remain & " + extend_time Where extend_status=2 and id=" & intID)
                    Else
                        clsOdbc.executeNonQuery("UPDATE mlm_commitment SET time_remain=" & time_remain & " Where id=" & intID)
                    End If

                Next

            End If

        Catch ex As Exception

        Finally
            ds.Dispose()
        End Try

    End Sub

    '**** Check Commitment CountDown ******
    Public Sub Update_CommitmentStatus_Expired()

        'Dim strQuery As String = "SELECT id,providing_id From mlm_commitment Where status=1 and time_remain <= 0"
        ' Dim strQuery As String = "SELECT id,providing_id,amount,helpme_id,helpsomeone_id From mlm_commitment Where status=1 and time_remain <= 0"
        Dim strQuery As String = "SELECT DISTINCT providing_id From mlm_commitment Where status=1 and time_remain <= 0"
        Dim ds As New Data.DataSet

        Try
            ds = clsOdbc.getDataSet(strQuery)

            If ds.Tables(0).Rows.Count > 0 Then

                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

                    Dim intID As Integer = ds.Tables(0).Rows(i).Item(0).ToString
                    'objOther.UpdateStatus(intID, 2, 1, 5)
                    BlockUser(intID)
                  
                Next



            End If

        Catch ex As Exception

        Finally
            ds.Dispose()
        End Try

    End Sub


    Public Sub RejectPayment(ByVal intCommitID As Integer)

        'Dim strQuery As String = "SELECT id,providing_id From mlm_commitment Where status=1 and time_remain <= 0"
        Dim strQuery As String = "SELECT id, providing_id,amount,helpme_id,helpsomeone_id,asking_id From mlm_commitment Where id=" & intCommitID
        Dim ds As New Data.DataSet


        Dim strDtTime As String = objWallet.getCurDateTimeString()


        Try
            ds = clsOdbc.getDataSet(strQuery)

            If ds.Tables(0).Rows.Count > 0 Then

                Dim intID As Integer = ds.Tables(0).Rows(0).Item(0).ToString
                Dim providing_id As Integer = ds.Tables(0).Rows(0).Item(1).ToString
                BlockUser(providing_id)
                Dim amount As Integer = Convert.ToInt32(ds.Tables(0).Rows(0)(2).ToString())
                Dim helpme_id As Integer = Convert.ToInt32(ds.Tables(0).Rows(0)(3).ToString())
                Dim helpsomeone_id As Integer = Convert.ToInt32(ds.Tables(0).Rows(0)(4).ToString())
                Dim asking_id As Integer = Convert.ToInt32(ds.Tables(0).Rows(0)(5).ToString())

                Try

                    clsOdbc.executeNonQuery("UPDATE mlm_request SET status=0, ask_amount=ask_amount+" & amount & " Where id=" & helpme_id)
                    clsOdbc.executeNonQuery("DELETE FROM mlm_commitment Where id=" & intID)
                    clsOdbc.executeNonQuery("UPDATE mlm_login SET status=2,status_modified_on='" & strDtTime & "' Where UserId=" & providing_id)

                    clsOdbc.executeNonQuery("INSERT INTO mlm_status_log(userid,old_status,status,modified_by,change_type,modified_on) VALUES(" & providing_id & "," & 1 & "," & 2 & "," & 1 & "," & 6 & ",'" + strDtTime + "')")

                    clsOdbc.executeNonQuery("call " & strdbName & ".UpdateStatus(" & providing_id & "," & 1 & ",2)")


                    clsOdbc.executeNonQuery("UPDATE mlm_user_invest SET amount_remaining= amount_remaining	+" & amount & " Where id=" & helpsomeone_id)


                    ' Dim ask_rem_amt As Integer = clsOdbc.executeScalar_int("SELECT ask_amount FROM mlm_help_me WHERE id=" & helpme_id)
                    '  add_new_commit_link_donor(ask_rem_amt, asking_id, helpme_id)


                Catch ex As Exception

                End Try
               
            End If

        Catch ex As Exception

        Finally
            ds.Dispose()
        End Try

    End Sub

    Public Sub BlockUser(ByVal intUserID As Integer)


        Dim strDtTime As String = objWallet.getCurDateTimeString()

        'Removing commitment where user involved as provider
        Dim strCommitID As String = "SELECT id, providing_id,amount,helpme_id,helpsomeone_id,asking_id From mlm_commitment Where status=1 and providing_id=" & intUserID
        Dim ds As New Data.DataSet

        Try
            ds = clsOdbc.getDataSet(strCommitID)

            If ds.Tables(0).Rows.Count > 0 Then

                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

                    Dim intID As Integer = ds.Tables(0).Rows(i).Item(0).ToString


                    Dim providing_id As Integer = ds.Tables(0).Rows(i).Item(1).ToString
                    Dim amount As Integer = Convert.ToInt32(ds.Tables(0).Rows(i)(2).ToString())
                    Dim helpme_id As Integer = Convert.ToInt32(ds.Tables(0).Rows(i)(3).ToString())
                    Dim helpsomeone_id As Integer = Convert.ToInt32(ds.Tables(0).Rows(i)(4).ToString())
                    Dim asking_id As Integer = Convert.ToInt32(ds.Tables(0).Rows(i)(5).ToString())
                    Try

                        clsOdbc.executeNonQuery("UPDATE mlm_request SET status=0, ask_amount=ask_amount+" & amount & " Where id=" & helpme_id)
                        clsOdbc.executeNonQuery("DELETE FROM mlm_commitment Where id=" & intID)
                        clsOdbc.executeNonQuery("UPDATE mlm_login SET status=2,status_modified_on='" & strDtTime & "' Where UserId=" & providing_id)

                        clsOdbc.executeNonQuery("INSERT INTO mlm_status_log(userid,old_status,status,modified_by,change_type,modified_on) VALUES(" & providing_id & "," & 1 & "," & 2 & "," & 1 & "," & 5 & ",'" + strDtTime + "')")

                        clsOdbc.executeNonQuery("call " & strdbName & ".UpdateStatus(" & providing_id & "," & 1 & ",2)")


                        clsOdbc.executeNonQuery("UPDATE mlm_user_invest SET amount_remaining= amount_remaining	+" & amount & " Where id=" & helpsomeone_id)


                        'Dim ask_rem_amt As Integer = clsOdbc.executeScalar_int("SELECT ask_amount FROM mlm_request WHERE id=" & helpme_id)
                        'add_new_commit_link_donor(ask_rem_amt, asking_id, helpme_id)


                    Catch ex As Exception

                    End Try

                Next

            End If

        Catch ex As Exception

        Finally
            ds.Dispose()
        End Try


        'Removing commitment where user involved as receiver

        Dim strCommitID1 As String = "SELECT id, providing_id,amount,helpme_id,helpsomeone_id From mlm_commitment Where status=1 and asking_id=" & intUserID
        Dim ds1 As New Data.DataSet

        Try
            ds1 = clsOdbc.getDataSet(strCommitID1)

            If ds1.Tables(0).Rows.Count > 0 Then

                For j As Integer = 0 To ds1.Tables(0).Rows.Count - 1

                    Dim intID As Integer = ds1.Tables(0).Rows(j).Item(0).ToString
                    Dim providing_id As Integer = ds1.Tables(0).Rows(j).Item(1).ToString
                    Dim amount As Integer = Convert.ToInt32(ds1.Tables(0).Rows(j)(2).ToString())
                    Dim helpme_id As Integer = Convert.ToInt32(ds1.Tables(0).Rows(j)(3).ToString())
                    Dim helpsomeone_id As Integer = Convert.ToInt32(ds1.Tables(0).Rows(j)(4).ToString())
                    clsOdbc.executeNonQuery("UPDATE mlm_request SET status=0, ask_amount=ask_amount+" & amount & " Where id=" & helpme_id)
                    clsOdbc.executeNonQuery("DELETE FROM mlm_commitment Where id=" & intID)

                    clsOdbc.executeNonQuery("UPDATE mlm_user_invest SET amount_remaining= amount_remaining	+" & amount & " Where id=" & helpsomeone_id)
                    add_new_commit_link(amount, providing_id, helpsomeone_id)
                Next

            End If

        Catch ex As Exception

        Finally
            ds1.Dispose()
        End Try



    End Sub


   

    Public Function add_New_Extend_Payment(ByVal intCommitID As Integer) As String

        Dim intExtendStatus As Integer = clsOdbc.executeScalar_int("SELECT extend_status From mlm_commitment Where id=" & intCommitID)

        If intExtendStatus = 1 Then

            Return "You have already Asked for Time Extension for this Commitment!"
        Else
            Try
                clsOdbc.executeNonQuery("UPDATE mlm_commitment SET extend_time=24,extend_status=1,extend_number=extend_number+1 Where id=" & intCommitID)
                Return "Your Request for Consent to make Payment has been Successfully Sent!"
            Catch ex As Exception
                Return ex.Message.ToString
            End Try
        End If

    End Function

   

    '**** Refuse Payment By Help Someone ID *****
    Public Sub RefusePayment(ByVal intCommitID As Integer)

        Dim strQuery As String = "SELECT helpme_id,amount From mlm_commitment Where id=" & intCommitID
        Dim ds As New Data.DataSet
        Dim intProvidingID As Integer = clsOdbc.executeScalar_int("SELECT providing_id From mlm_commitment Where id=" & intCommitID)

        Try
            ds = clsOdbc.getDataSet(strQuery)

            If ds.Tables(0).Rows.Count > 0 Then

                Dim intHelpMeID As Integer = ds.Tables(0).Rows(0).Item(0).ToString
                Dim intAmount As Integer = ds.Tables(0).Rows(0).Item(1).ToString

                clsOdbc.executeNonQuery("UPDATE mlm_request SET ask_amount=ask_amount+" & intAmount & " Where id=" & intHelpMeID)

                'Dim strQuery1 As String = "SELECT id,userid From mlm_request Where userid not in (SELECT asking_id From mlm_commitment Where providing_id=" & intProvidingID & ") LIMIT 1"
                'Dim ds1 As New Data.DataSet

                'Try

                '    ds1 = clsOdbc.getDataSet(strQuery1)

                '    If ds1.Tables(0).Rows.Count > 0 Then

                '        Dim id As Integer = ds1.Tables(0).Rows(0).Item(0).ToString
                '        Dim userid As Integer = ds1.Tables(0).Rows(0).Item(1).ToString

                '        clsOdbc.executeNonQuery("UPDATE mlm_request SET ask_amount=ask_amount-" & intAmount & " Where id=" & id)

                '        clsOdbc.executeNonQuery("UPDATE mlm_commitment SET asking_id=" & userid & ",helpme_id=" & id & " Where id=" & intCommitID)

                '        HttpContext.Current.Response.Redirect("dashboard.aspx")

                '    End If

                'Catch ex As Exception

                'Finally
                '    ds1.Dispose()
                'End Try

            End If

        Catch ex As Exception

        Finally
            ds.Dispose()
        End Try

    End Sub


    Public Sub assign_help()
        Dim strProvider As String = "SELECT a.userid,a.amount_remaining,a.id From mlm_user_invest a,mlm_login b Where a.amount_remaining > 0 and b.status=1 and a.userid=b.userid "

        Dim ds As New System.Data.DataSet()
        Try
            ds = clsOdbc.getDataSet(strProvider)
            If ds.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Dim provider_id As Integer = Convert.ToInt32(ds.Tables(0).Rows(i)(0).ToString())
                    Dim amt_rem As Integer = Convert.ToInt32(ds.Tables(0).Rows(i)(1).ToString())
                    Dim provide_help_id As Integer = Convert.ToInt32(ds.Tables(0).Rows(i)(2).ToString())
                    add_new_commit_link(amt_rem, provider_id, provide_help_id)
                Next
            End If
        Catch ex As Exception
        Finally
            ds.Dispose()
        End Try

    End Sub

    Public Function get_Direct_Referral_UID(ByVal intUserID As Integer) As Integer

        Dim result_get_Direct_Referral_UID As Integer = clsOdbc.executeScalar_int("SELECT a.userid From mlm_referral a,mlm_referral b Where b.userid=" & intUserID & " and b.referral_id=a.my_sponsar_id")

        Return result_get_Direct_Referral_UID

    End Function

End Class
