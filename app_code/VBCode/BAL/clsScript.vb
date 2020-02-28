Imports Microsoft.VisualBasic

Public Class clsScript

    Dim clsOdbc As New ODBC

    '***** Daily Growth Rate Script ******
    Public Sub DailyGrowthRate()

        Dim strQueryCapping As String = "SELECT per_growth,till_days From mlm_growth_rate Where id=1"
        Dim dsCapping As New Data.DataSet
        Dim per_growth, till_days As Integer

        Dim GrowthDate As String = DateTime.Now.ToString("yyyy-MM-dd")

        'Dim GrowthDate As String = "2013-05-28"


        Try

            dsCapping = clsOdbc.getDataSet(strQueryCapping)

            If dsCapping.Tables(0).Rows.Count > 0 Then

                per_growth = dsCapping.Tables(0).Rows(0).Item(0).ToString
                till_days = dsCapping.Tables(0).Rows(0).Item(1).ToString

            End If

        Catch ex As Exception

        Finally
            dsCapping.Dispose()
        End Try

        Dim strQuery As String = "SELECT userid From mlm_login Where LoginTypeId=2"

        Dim ds As New Data.DataSet

        Try

            ds = clsOdbc.getDataSet(strQuery)

            If ds.Tables(0).Rows.Count > 0 Then

                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

                    Dim myUserID = ds.Tables(0).Rows(i).Item(0).ToString

                    Dim totoalDaysGrowthCount As Integer = clsOdbc.executeScalar_int("SELECT Count(*) As Count From mlm_my_balance_details Where userid=" & myUserID & " and balance_type=2")

                    Dim TodaysDaysGrowthCount As Integer = clsOdbc.executeScalar_int("SELECT Count(*) As Count From mlm_my_balance_details Where userid=" & myUserID & " and balance_type=2 and DATE(reciv_date)=DATE('" & GrowthDate & "')")

                    Dim intUserCount As Integer = clsOdbc.executeScalar_int("SELECT Count(*) From mlm_login Where userid=" & myUserID & " and DATE('" & GrowthDate & "')  >= DATE(created_on)")

                    If totoalDaysGrowthCount < till_days And TodaysDaysGrowthCount = 0 And intUserCount > 0 Then

                        Dim intMyGrowthRate As Integer = per_growth

                        MakeGrowthPayment(myUserID, intMyGrowthRate, 2, GrowthDate)

                    End If


                Next

            End If

        Catch ex As Exception

        Finally
            ds.Dispose()
        End Try

    End Sub


    '***** Make Payment on User Account *****
    Public Sub MakeGrowthPayment(ByVal intUserID As Integer, ByVal intAmount As Integer, ByVal intBalanceType As Integer, ByVal dtDate As String)

        Try
            Dim intUserCount As Integer = clsOdbc.executeScalar_int("SELECT Count(*) As UserCount From mlm_my_balance Where userid=" & intUserID)

            If intUserCount > 0 Then

                clsOdbc.executeNonQuery("UPDATE mlm_my_balance SET balance_amount=balance_amount+" & intAmount & " Where userid=" & intUserID)
            Else
                clsOdbc.executeNonQuery("INSERT INTO mlm_my_balance(userid,balance_amount) values (" & intUserID & "," & intAmount & ")")
            End If

            Dim intMyBlanaceDetailsCount As Integer = clsOdbc.executeScalar_int("SELECT Count(*) As UserCount From mlm_my_balance_details Where userid=" & intUserID & " and DATE(reciv_date)=DATE('" & dtDate & "')")

            If intMyBlanaceDetailsCount > 0 Then

                Dim strQuery As String = "UPDATE mlm_my_balance_details SET reciv_amount=" & intAmount & " Where userid=" & intUserID & " and DATE(reciv_date)=DATE('" & dtDate & "') and balance_type=" & intBalanceType & ""
                clsOdbc.executeNonQuery(strQuery)
            Else

                Dim strQuery As String = "INSERT INTO mlm_my_balance_details(userid,reciv_amount,balance_type,reciv_date) values (" & intUserID & "," & intAmount & "," & intBalanceType & ",'" & dtDate & "')"
                clsOdbc.executeNonQuery(strQuery)

            End If



        Catch ex As Exception

        End Try

    End Sub

End Class
