Imports Microsoft.VisualBasic

Public Class clsDashboard
    Dim clsOdbc As New ODBC

    Public Sub UserDashboardStatus(ByVal litDashboard As Literal, ByVal intUserID As Integer)

        litDashboard.Text = ""

        Providing_Pending_Dashboard(litDashboard, intUserID)

        Providing_Payment_Done_Dashboard(litDashboard, intUserID)

        Asking_Pending_Dashboard(litDashboard, intUserID)

        Asking_Payment_Done_Dashboard(litDashboard, intUserID)

        Asking_Payment_Confirmation_Dashboard(litDashboard, intUserID)

        Provider_Payment_Confirmation_Dashboard(litDashboard, intUserID)



    End Sub

    Public Sub UserDashboardRightStatus(ByVal litDashboard As Literal, ByVal intUserID As Integer)

        litDashboard.Text = ""

        Pending_Commitment_Status(litDashboard, intUserID)

        Asking_Commitment_Status(litDashboard, intUserID)

    End Sub

    Private Sub Providing_Pending_Dashboard(ByVal litDashboard As Literal, ByVal intUserID As Integer)

        Dim row_providing_count As Integer = clsOdbc.executeScalar_int("SELECT Count(1) As UserCount From mlm_commitment Where status=1 and providing_id=" & intUserID)

        If row_providing_count > 0 Then

            Dim strQuery As String = "SELECT a.UserName As PName,c.UserName As AName,time_remain,b.amount,DATE(b.date_assigned) As commit_date,d.my_sponsar_id As PID,e.my_sponsar_id As AID,b.asking_id,b.id  From mlm_login a,mlm_commitment b,mlm_login c,mlm_referral d,mlm_referral e Where b.providing_id=a.userid and b.asking_id=c.userid and b.providing_id=d.userid and b.asking_id=e.userid and b.status=1 and b.providing_id=" & intUserID
            Dim ds As New Data.DataSet
            Dim PName, AName, time_remain, commit_date, PID, AID, amount, asking_id, commitment_id As String
            Dim strLiteral As String = ""

            Try
                ds = clsOdbc.getDataSet(strQuery)

                If ds.Tables(0).Rows.Count > 0 Then

                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

                        PName = ds.Tables(0).Rows(i).Item(0).ToString
                        AName = ds.Tables(0).Rows(i).Item(1).ToString
                        time_remain = ds.Tables(0).Rows(i).Item(2).ToString
                        commit_date = ds.Tables(0).Rows(i).Item(4).ToString
                        PID = ds.Tables(0).Rows(i).Item(5).ToString
                        AID = ds.Tables(0).Rows(i).Item(6).ToString
                        amount = ds.Tables(0).Rows(i).Item(3).ToString
                        asking_id = ds.Tables(0).Rows(i).Item(7).ToString
                        commitment_id = ds.Tables(0).Rows(i).Item(8).ToString


                        'Dim dt As DateTime = DateTime.ParseExact(commit_date, "dd/MM/YYYY hh:mm:ss", CultureInfo.CreateSpecificCulture("en-US"))
                        'commit_date = dt.ToString("yyyyMMdd")

                        commit_date = CType(commit_date, Date).ToString("dd/MM/yyyy")

                        Dim msgCount As Integer = clsOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_commitment_chat WHERE cmmitment_id=" & commitment_id)

                        strLiteral = "<tr>" _
                                    & "<td >" & PID & "</td>" _
                                    & "<td >" & PName & "</td>" _
                                    & "<td >" & AID & "</td>" _
                                    & "<td >" & AName & "</td>" _
                                    & "<td >" & amount & "</td>" _
                                    & "<td >" & commit_date & "</td>" _
                                    & "<td >" & time_remain & "</td>" _
                                    & "<td ><a href=""javascript:my_window1=window.open('commitment_chat.aspx?CID=" & commitment_id & "', 'my_window1','status=no,location=no,menubar=no,titlebar=no,resizable=no,scrollbars=no,width=650,height=558,Top=250,right=50,left=200');my_window1.focus()"">" & msgCount & "</a></td>" _
                                    & "<td ><a href=""javascript:my_window1=window.open('pending_payment1.aspx?CID=" & commitment_id & "', 'my_window1','status=no,location=no,menubar=no,titlebar=no,resizable=no,scrollbars=no,width=650,height=558,Top=250,right=50,left=200');my_window1.focus()"">Pending</a></td>" _
                                    & "</tr>"


                        'strLiteral = "<div class=""arrg""><table width=""100%"" border=""0"" cellspacing=""2"" cellpadding=""2"" class=""arrg_tbarrg""><tbody><tr>" _
                        '            & "<td width=""36"" rowspan=""2"" class=""arrg_num"">" _
                        '            & "<img src=""../templates/images/play.png"" width=""36"" height=""36"" class=""arrg_status_img""><br>" _
                        '            & "<span class=""arrg_sm10""><span class=""translate"">Number</span>:<br></span><span class=""arrg_id"">" _
                        '            & "" & AID & "</span>" _
                        '            & "</td><td class=""arrg_status_name"">Expect payment confirmation (Request to Help " & PID & ")</td>" _
                        '            & "<td class=""arrg_wait""><span class=""translate"">Accomplish in</span>: <span class=""arrg_red"">" & time_remain & " Hour.</span></td>" _
                        '            & "<td class=""arrg_msg"" style=""width:120px;""><span>" _
                        '            & "<a href=""javascript:my_window1=window.open('commitment_chat.aspx?CID=" & commitment_id & "', 'my_window1','status=no,location=no,menubar=no,titlebar=no,resizable=no,scrollbars=no,width=650,height=558,Top=250,right=50,left=200');my_window1.focus()"">Messages:" & msgCount _
                        '            & "</a></span></td></tr>" _
                        '            & "<tr><td colspan=""3""><table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0""><tbody><tr>" _
                        '            & "<td class=""arrg_num""><span class=""arrg_sm10""><span class=""translate"">Date of creating</span>:<br></span>" _
                        '            & "<span class=""arrg_date"">" & commit_date & "</span></td><td class=""arrg_name1"">" _
                        '            & "<span class=""arrg_user_in"">" & PName & "</span>" _
                        '            & "</td><td align=""center"" class=""arrg_summ"">" _
                        '            & "<span class=""arrg_summ_in"">&gt;</span> &nbsp; <span class=""arrg_amt"">" & amount & " INR</span> &nbsp;<span class=""arrg_summ_out"">&gt; " _
                        '            & "</span>	<div class=""arrg_out_files"" style=""display:none;""><span class=""files""><span class=""translate"">Confirmation</span>:</span>/div>" _
                        '            & "</td><td class=""arrg_name2""><span class=""arrg_user_out"">" _
                        '            & "<a href=""javascript:my_window1=window.open('pending_payment1.aspx?CID=" & commitment_id & "', 'my_window1','status=no,location=no,menubar=no,titlebar=no,resizable=no,scrollbars=no,width=650,height=558,Top=250,right=50,left=200');my_window1.focus()"">" _
                        '            & "" & AName & "" _
                        '            & "</a></span></td><td width=""30"">" _
                        '            & "<img src=""images/printout.png"" width=""30"" height=""30"" style=""cursor: pointer; display: none;"" class=""arrg_print"">" _
                        '            & "</td></tr></tbody></table></td></tr></tbody></table></div>"

                        litDashboard.Text = litDashboard.Text & strLiteral

                    Next

                End If

            Catch ex As Exception

            End Try

        End If

    End Sub

    Private Sub Providing_Payment_Done_Dashboard(ByVal litDashboard As Literal, ByVal intUserID As Integer)

        Dim row_providing_count As Integer = clsOdbc.executeScalar_int("SELECT Count(1) As UserCount From mlm_commitment Where status=2 and providing_id=" & intUserID)

        If row_providing_count > 0 Then

            Dim strQuery As String = "SELECT a.UserName As PName,c.UserName As AName,b.amount,DATE(b.date_assigned) As commit_date,d.my_sponsar_id As PID,e.my_sponsar_id As AID,b.asking_id,b.id  From mlm_login a,mlm_commitment b,mlm_login c,mlm_referral d,mlm_referral e Where b.providing_id=a.userid and b.asking_id=c.userid and b.providing_id=d.userid and b.asking_id=e.userid and b.status=2 and b.providing_id=" & intUserID
            Dim ds As New Data.DataSet
            Dim PName, AName, commit_date, PID, AID, amount, asking_id, commitment_id As String
            Dim strLiteral As String = ""

            Try
                ds = clsOdbc.getDataSet(strQuery)

                If ds.Tables(0).Rows.Count > 0 Then

                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

                        PName = ds.Tables(0).Rows(i).Item(0).ToString
                        AName = ds.Tables(0).Rows(i).Item(1).ToString
                        amount = ds.Tables(0).Rows(i).Item(2).ToString
                        commit_date = ds.Tables(0).Rows(i).Item(3).ToString
                        PID = ds.Tables(0).Rows(i).Item(4).ToString
                        AID = ds.Tables(0).Rows(i).Item(5).ToString
                        asking_id = ds.Tables(0).Rows(i).Item(6).ToString
                        commitment_id = ds.Tables(0).Rows(i).Item(7).ToString

                        Dim msgCount As Integer = clsOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_commitment_chat WHERE cmmitment_id=" & commitment_id)

                        strLiteral = "<tr>" _
                                    & "<td >" & PID & "</td>" _
                                    & "<td >" & PName & "</td>" _
                                    & "<td >" & AID & "</td>" _
                                    & "<td >" & AName & "</td>" _
                                    & "<td >" & amount & "</td>" _
                                    & "<td >" & commit_date & "</td>" _
                                    & "<td >0</td>" _
                                    & "<td ><a href=""javascript:my_window1=window.open('commitment_chat.aspx?CID=" & commitment_id & "', 'my_window1','status=no,location=no,menubar=no,titlebar=no,resizable=no,scrollbars=no,width=650,height=558,Top=250,right=50,left=200');my_window1.focus()"">" & msgCount & "</a></td>" _
                                    & "<td ><a href=""javascript:my_window1=window.open('pending_reciept1.aspx?CID=" & commitment_id & "', 'my_window1','status=no,location=no,menubar=no,titlebar=no,resizable=no,scrollbars=no,width=650,height=558,Top=250,right=50,left=200');my_window1.focus()"">Processing</a></td>" _
                                    & "</tr>"

                        'strLiteral = "<div class=""arrg"">" _
                        '            & "<table width=""100%"" border=""0"" cellspacing=""2"" cellpadding=""2"" class=""arrg_tbarrg"">" _
                        '            & "<tbody><tr><td width=""36"" rowspan=""2"" class=""arrg_num"">" _
                        '            & "<img src=""../templates/images/half_confirm.png"" width=""36"" height=""36"" class=""arrg_status_img""><br>" _
                        '            & "<span class=""arrg_sm10""><span class=""translate"">Number</span>:<br></span><span class=""arrg_id"">" _
                        '            & "" & AID & "</span>" _
                        '            & "</td><td class=""arrg_status_name"">Pending for confirmation (Request to Help " & PID & ")</td>" _
                        '            & "<td class=""arrg_msg"" style=""width:120px;""><span>" _
                        '            & "<a href=""javascript:my_window1=window.open('commitment_chat.aspx?CID=" & commitment_id & "', 'my_window1','status=no,location=no,menubar=no,titlebar=no,resizable=no,scrollbars=no,width=650,height=558,Top=250,right=50,left=200');my_window1.focus()"">Messages:" & msgCount _
                        '            & "</a></span></td></tr><tr><td colspan=""3""><table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0""><tbody><tr><td class=""arrg_num"">" _
                        '            & "<span class=""arrg_sm10""><span class=""translate"">Date of creating</span>:<br></span>" _
                        '            & "<span class=""arrg_date"">" & commit_date & "</span></td><td class=""arrg_name1"">" _
                        '            & "<span class=""arrg_user_in"">" & PName & "</span></td>" _
                        '            & "<td align=""center"" class=""arrg_summ""><span class=""arrg_summ_in"">&gt;</span> &nbsp;<span class=""arrg_amt"">" & amount & " INR</span> &nbsp;" _
                        '            & "<span class=""arrg_summ_out"">&gt; </span><div class=""arrg_out_files"">" _
                        '            & "<span class=""files""><span class=""translate"">Confirmation</span>:" _
                        '            & "</span></div></td><td class=""arrg_name2""><span class=""arrg_user_out"">" _
                        '            & "<a href=""javascript:my_window1=window.open('pending_reciept1.aspx?CID=" & commitment_id & "', 'my_window1','status=no,location=no,menubar=no,titlebar=no,resizable=no,scrollbars=no,width=650,height=558,Top=250,right=50,left=200');my_window1.focus()"">" & AName & "</a>" _
                        '            & "</span></td><td width=""30""><img src=""../templates/images/printout.png"" width=""30"" height=""30"" style=""cursor: pointer; display: none;"" class=""arrg_print""></td>" _
                        '            & "</tr></tbody></table></td></tr></tbody></table></div>"


                        litDashboard.Text = litDashboard.Text & strLiteral

                    Next

                End If

            Catch ex As Exception

            End Try

        End If

    End Sub


    Private Sub Asking_Pending_Dashboard(ByVal litDashboard As Literal, ByVal intUserID As Integer)

        Dim row_providing_count As Integer = clsOdbc.executeScalar_int("SELECT Count(1) As UserCount From mlm_commitment Where status=1 and asking_id=" & intUserID)

        If row_providing_count > 0 Then

            Dim strQuery As String = "SELECT a.UserName As PName,c.UserName As AName,time_remain,b.amount,DATE(b.date_assigned) As commit_date,d.my_sponsar_id As PID,e.my_sponsar_id As AID,b.id  From mlm_login a,mlm_commitment b,mlm_login c,mlm_referral d,mlm_referral e Where b.providing_id=a.userid and b.asking_id=c.userid and b.providing_id=d.userid and b.status=1 and b.asking_id=e.userid and b.asking_id=" & intUserID
            Dim ds As New Data.DataSet
            Dim PName, AName, time_remain, commit_date, PID, AID, amount, asking_id, commitment_id As String
            Dim strLiteral As String = ""

            Try
                ds = clsOdbc.getDataSet(strQuery)

                If ds.Tables(0).Rows.Count > 0 Then

                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

                        PName = ds.Tables(0).Rows(i).Item(0).ToString
                        AName = ds.Tables(0).Rows(i).Item(1).ToString
                        time_remain = ds.Tables(0).Rows(i).Item(2).ToString

                        commit_date = ds.Tables(0).Rows(i).Item(4).ToString
                        PID = ds.Tables(0).Rows(i).Item(5).ToString
                        AID = ds.Tables(0).Rows(i).Item(6).ToString
                        amount = ds.Tables(0).Rows(i).Item(3).ToString
                        commitment_id = ds.Tables(0).Rows(i).Item(7).ToString

                        Dim msgCount As Integer = clsOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_commitment_chat WHERE cmmitment_id=" & commitment_id)

                        strLiteral = "<tr>" _
                                   & "<td >" & PID & "</td>" _
                                   & "<td >" & PName & "</td>" _
                                   & "<td >" & AID & "</td>" _
                                   & "<td >" & AName & "</td>" _
                                   & "<td >" & amount & "</td>" _
                                   & "<td >" & commit_date & "</td>" _
                                   & "<td >" & time_remain & "</td>" _
                                   & "<td ><a href=""javascript:my_window1=window.open('commitment_chat.aspx?CID=" & commitment_id & "', 'my_window1','status=no,location=no,menubar=no,titlebar=no,resizable=no,scrollbars=no,width=650,height=558,Top=250,right=50,left=200');my_window1.focus()"">" & msgCount & "</a></td>" _
                                   & "<td ><a href=""javascript:my_window1=window.open('approval_commitment1.aspx?CID=" & commitment_id & "', 'my_window1','status=no,location=no,menubar=no,titlebar=no,resizable=no,scrollbars=no,width=650,height=558,Top=250,right=50,left=200');my_window1.focus()"">Pending</a></td>" _
                                   & "</tr>"
                        
                        'strLiteral = "<div class=""arrg""><table width=""100%"" border=""0"" cellspacing=""2"" cellpadding=""2"" class=""arrg_tbarrg""><tbody><tr>" _
                        '            & "<td width=""36"" rowspan=""2"" class=""arrg_num"">" _
                        '            & "<img src=""../templates/images/play.png"" width=""36"" height=""36"" class=""arrg_status_img""><br>" _
                        '            & "<span class=""arrg_sm10""><span class=""translate"">Number</span>:<br></span><span class=""arrg_id"">" _
                        '            & "" & AID & "</span>" _
                        '            & "</td><td class=""arrg_status_name"">Expect payment confirmation (Request to Help " & PID & ")</td>" _
                        '            & "<td class=""arrg_wait""><span class=""translate"">Accomplish in</span>: <span class=""arrg_red"">" & time_remain & " Hour.</span></td>" _
                        '            & "<td class=""arrg_msg"" style=""width:120px;""><span>" _
                        '            & "<a href=""javascript:my_window1=window.open('commitment_chat.aspx?CID=" & commitment_id & "', 'my_window1','status=no,location=no,menubar=no,titlebar=no,resizable=no,scrollbars=no,width=650,height=558,Top=250,right=50,left=200');my_window1.focus()"">Messages:" & msgCount _
                        '            & "</a></span></td></tr>" _
                        '            & "<tr><td colspan=""3""><table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0""><tbody><tr>" _
                        '            & "<td class=""arrg_num""><span class=""arrg_sm10""><span class=""translate"">Date of creating</span>:<br></span>" _
                        '            & "<span class=""arrg_date"">" & commit_date & "</span></td><td class=""arrg_name1"">" _
                        '            & "<span class=""arrg_user_in"">" & PName & "</span>" _
                        '            & "</td><td align=""center"" class=""arrg_summ"">" _
                        '            & "<span class=""arrg_summ_in"">&gt;</span> &nbsp; <span class=""arrg_amt"">" & amount & " INR</span> &nbsp;<span class=""arrg_summ_out"">&gt; " _
                        '            & "</span>	<div class=""arrg_out_files"" style=""display:none;""><span class=""files""><span class=""translate"">Confirmation</span>:</span>/div>" _
                        '            & "</td><td class=""arrg_name2""><span class=""arrg_user_out"">" _
                        '            & "<a href=""javascript:my_window1=window.open('approval_commitment1.aspx?CID=" & commitment_id & "', 'my_window1','status=no,location=no,menubar=no,titlebar=no,resizable=no,scrollbars=no,width=650,height=558,Top=250,right=50,left=200');my_window1.focus()"">" _
                        '            & "" & AName & "" _
                        '            & "</a></span></td><td width=""30"">" _
                        '            & "<img src=""images/printout.png"" width=""30"" height=""30"" style=""cursor: pointer; display: none;"" class=""arrg_print"">" _
                        '            & "</td></tr></tbody></table></td></tr></tbody></table></div>"

                        litDashboard.Text = litDashboard.Text & strLiteral

                    Next

                End If

            Catch ex As Exception

            End Try

        End If

    End Sub


    Private Sub Asking_Payment_Done_Dashboard(ByVal litDashboard As Literal, ByVal intUserID As Integer)

        Dim row_providing_count As Integer = clsOdbc.executeScalar_int("SELECT Count(1) As UserCount From mlm_commitment Where status=2 and asking_id=" & intUserID)

        If row_providing_count > 0 Then

            Dim strQuery As String = "SELECT a.UserName As PName,c.UserName As AName,b.amount,DATE(b.date_assigned) As commit_date,d.my_sponsar_id As PID,e.my_sponsar_id As AID,b.asking_id,b.id  From mlm_login a,mlm_commitment b,mlm_login c,mlm_referral d,mlm_referral e Where b.providing_id=a.userid and b.asking_id=c.userid and b.providing_id=d.userid and b.asking_id=e.userid and b.status=2 and b.asking_id=" & intUserID
            Dim ds As New Data.DataSet
            Dim PName, AName, commit_date, PID, AID, amount, asking_id, commitment_id As String
            Dim strLiteral As String = ""

            Try
                ds = clsOdbc.getDataSet(strQuery)

                If ds.Tables(0).Rows.Count > 0 Then

                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

                        PName = ds.Tables(0).Rows(i).Item(0).ToString
                        AName = ds.Tables(0).Rows(i).Item(1).ToString
                        amount = ds.Tables(0).Rows(i).Item(2).ToString
                        commit_date = ds.Tables(0).Rows(i).Item(3).ToString
                        PID = ds.Tables(0).Rows(i).Item(4).ToString
                        AID = ds.Tables(0).Rows(i).Item(5).ToString
                        asking_id = ds.Tables(0).Rows(i).Item(6).ToString
                        commitment_id = ds.Tables(0).Rows(i).Item(7).ToString

                        Dim msgCount As Integer = clsOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_commitment_chat WHERE cmmitment_id=" & commitment_id)

                        strLiteral = "<tr>" _
                                   & "<td >" & PID & "</td>" _
                                   & "<td >" & PName & "</td>" _
                                   & "<td >" & AID & "</td>" _
                                   & "<td >" & AName & "</td>" _
                                   & "<td >" & amount & "</td>" _
                                   & "<td >" & commit_date & "</td>" _
                                   & "<td >0</td>" _
                                   & "<td ><a href=""javascript:my_window1=window.open('commitment_chat.aspx?CID=" & commitment_id & "', 'my_window1','status=no,location=no,menubar=no,titlebar=no,resizable=no,scrollbars=no,width=650,height=558,Top=250,right=50,left=200');my_window1.focus()"">" & msgCount & "</a></td>" _
                                   & "<td ><a href=""javascript:my_window1=window.open('approval_reciept1.aspx?CID=" & commitment_id & "', 'my_window1','status=no,location=no,menubar=no,titlebar=no,resizable=no,scrollbars=no,width=650,height=558,Top=250,right=50,left=200');my_window1.focus()"">Processing</a></td>" _
                                   & "</tr>"

                        'strLiteral = "<div class=""arrg"">" _
                        '            & "<table width=""100%"" border=""0"" cellspacing=""2"" cellpadding=""2"" class=""arrg_tbarrg"">" _
                        '            & "<tbody><tr><td width=""36"" rowspan=""2"" class=""arrg_num"">" _
                        '            & "<img src=""../templates/images/half_confirm.png"" width=""36"" height=""36"" class=""arrg_status_img""><br>" _
                        '            & "<span class=""arrg_sm10""><span class=""translate"">Number</span>:<br></span><span class=""arrg_id"">" _
                        '            & "" & AID & "</span>" _
                        '            & "</td><td class=""arrg_status_name"">Pending for confirmation (Request to Help " & PID & ")</td>" _
                        '            & "<td class=""arrg_msg"" style=""width:120px;""><span>" _
                        '            & "<a href=""javascript:my_window1=window.open('commitment_chat.aspx?CID=" & commitment_id & "', 'my_window1','status=no,location=no,menubar=no,titlebar=no,resizable=no,scrollbars=no,width=650,height=558,Top=250,right=50,left=200');my_window1.focus()"">Messages:" & msgCount _
                        '            & "</a></span></td></tr><tr><td colspan=""3""><table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0""><tbody><tr><td class=""arrg_num"">" _
                        '            & "<span class=""arrg_sm10""><span class=""translate"">Date of creating</span>:<br></span>" _
                        '            & "<span class=""arrg_date"">" & commit_date & "</span></td><td class=""arrg_name1"">" _
                        '            & "<span class=""arrg_user_in"">" & PName & "</span></td>" _
                        '            & "<td align=""center"" class=""arrg_summ""><span class=""arrg_summ_in"">&gt;</span> &nbsp;<span class=""arrg_amt"">" & amount & " INR</span> &nbsp;" _
                        '            & "<span class=""arrg_summ_out"">&gt; </span><div class=""arrg_out_files"">" _
                        '            & "<span class=""files""><span class=""translate"">Confirmation</span>:" _
                        '            & "</span></div></td><td class=""arrg_name2""><span class=""arrg_user_out"">" _
                        '            & "<a href=""javascript:my_window1=window.open('approval_reciept1.aspx?CID=" & commitment_id & "', 'my_window1','status=no,location=no,menubar=no,titlebar=no,resizable=no,scrollbars=no,width=650,height=558,Top=250,right=50,left=200');my_window1.focus()"">" & AName & "</a>" _
                        '            & "</span></td><td width=""30""><img src=""../templates/images/printout.png"" width=""30"" height=""30"" style=""cursor: pointer; display: none;"" class=""arrg_print""></td>" _
                        '            & "</tr></tbody></table></td></tr></tbody></table></div>"


                        litDashboard.Text = litDashboard.Text & strLiteral

                    Next

                End If

            Catch ex As Exception

            End Try

        End If

    End Sub

    Private Sub Provider_Payment_Confirmation_Dashboard(ByVal litDashboard As Literal, ByVal intUserID As Integer)

        Dim row_providing_count As Integer = clsOdbc.executeScalar_int("SELECT Count(1) As UserCount From mlm_commitment Where status=3 and providing_id=" & intUserID)

        If row_providing_count > 0 Then

            Dim strQuery As String = "SELECT a.UserName As PName,c.UserName As AName,b.amount,DATE(b.date_assigned) As commit_date,d.my_sponsar_id As PID,e.my_sponsar_id As AID,b.asking_id,b.id  From mlm_login a,mlm_commitment b,mlm_login c,mlm_referral d,mlm_referral e Where b.providing_id=a.userid and b.asking_id=c.userid and b.providing_id=d.userid and b.asking_id=e.userid and b.status=3 and b.providing_id=" & intUserID
            Dim ds As New Data.DataSet
            Dim PName, AName, commit_date, PID, AID, amount, asking_id, commitment_id As String
            Dim strLiteral As String = ""

            Try
                ds = clsOdbc.getDataSet(strQuery)

                If ds.Tables(0).Rows.Count > 0 Then

                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

                        PName = ds.Tables(0).Rows(i).Item(0).ToString
                        AName = ds.Tables(0).Rows(i).Item(1).ToString
                        amount = ds.Tables(0).Rows(i).Item(2).ToString
                        commit_date = ds.Tables(0).Rows(i).Item(3).ToString
                        PID = ds.Tables(0).Rows(i).Item(4).ToString
                        AID = ds.Tables(0).Rows(i).Item(5).ToString
                        asking_id = ds.Tables(0).Rows(i).Item(6).ToString
                        commitment_id = ds.Tables(0).Rows(i).Item(7).ToString

                        Dim msgCount As Integer = clsOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_commitment_chat WHERE cmmitment_id=" & commitment_id)

                        strLiteral = "<tr>" _
                                  & "<td >" & PID & "</td>" _
                                  & "<td >" & PName & "</td>" _
                                  & "<td >" & AID & "</td>" _
                                  & "<td >" & AName & "</td>" _
                                  & "<td >" & amount & "</td>" _
                                  & "<td >" & commit_date & "</td>" _
                                  & "<td >0</td>" _
                                  & "<td ><a href=""javascript:my_window1=window.open('commitment_chat.aspx?CID=" & commitment_id & "', 'my_window1','status=no,location=no,menubar=no,titlebar=no,resizable=no,scrollbars=no,width=650,height=558,Top=250,right=50,left=200');my_window1.focus()"">" & msgCount & "</a></td>" _
                                  & "<td ><a href=""javascript:my_window1=window.open('payment_done.aspx?CID=" & commitment_id & "', 'my_window1','status=no,location=no,menubar=no,titlebar=no,resizable=no,scrollbars=no,width=650,height=558,Top=250,right=50,left=200');my_window1.focus()"">Completed</a></td>" _
                                  & "</tr>"

                        'strLiteral = "<div class=""arrg"">" _
                        '            & "<table width=""100%"" border=""0"" cellspacing=""2"" cellpadding=""2"" class=""arrg_tbarrg"">" _
                        '            & "<tbody><tr><td width=""36"" rowspan=""2"" class=""arrg_num"">" _
                        '            & "<img src=""../templates/images/ok.png"" width=""36"" height=""36"" class=""arrg_status_img""><br>" _
                        '            & "<span class=""arrg_sm10""><span class=""translate"">Number</span>:<br></span><span class=""arrg_id"">" _
                        '            & "" & AID & "</span>" _
                        '            & "</td><td class=""arrg_status_name"">You confirmed funds reception (Request to Help " & PID & ")</td>" _
                        '            & "<td class=""arrg_msg"" style=""width:120px;""><span>" _
                        '            & "<a href=""javascript:my_window1=window.open('commitment_chat.aspx?CID=" & commitment_id & "', 'my_window1','status=no,location=no,menubar=no,titlebar=no,resizable=no,scrollbars=no,width=650,height=558,Top=250,right=50,left=200');my_window1.focus()"">Messages:" & msgCount _
                        '            & "</a></span></td></tr><tr><td colspan=""3""><table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0""><tbody><tr><td class=""arrg_num"">" _
                        '            & "<span class=""arrg_sm10""><span class=""translate"">Date of creating</span>:<br></span>" _
                        '            & "<span class=""arrg_date"">" & commit_date & "</span></td><td class=""arrg_name1"">" _
                        '            & "<span class=""arrg_user_in"">" & PName & "</span></td>" _
                        '            & "<td align=""center"" class=""arrg_summ""><span class=""arrg_summ_in"">&gt;</span> &nbsp;<span class=""arrg_amt"">" & amount & " INR</span> &nbsp;" _
                        '            & "<span class=""arrg_summ_out"">&gt; </span><div class=""arrg_out_files"">" _
                        '            & "<span class=""files""><span class=""translate"">Confirmation</span>:" _
                        '            & "</span></div></td><td class=""arrg_name2""><span class=""arrg_user_out"">" _
                        '            & "<a href=""javascript:my_window1=window.open('payment_done.aspx?CID=" & commitment_id & "', 'my_window1','status=no,location=no,menubar=no,titlebar=no,resizable=no,scrollbars=no,width=650,height=558,Top=250,right=50,left=200');my_window1.focus()"">" & AName & "</a>" _
                        '            & "</span></td><td width=""30""><img src=""../templates/images/printout.png"" width=""30"" height=""30"" style=""cursor: pointer; display: none;"" class=""arrg_print""></td>" _
                        '            & "</tr></tbody></table></td></tr></tbody></table></div>"


                        litDashboard.Text = litDashboard.Text & strLiteral

                    Next

                End If

            Catch ex As Exception

            End Try

        End If

    End Sub


    Private Sub Asking_Payment_Confirmation_Dashboard(ByVal litDashboard As Literal, ByVal intUserID As Integer)

        Dim row_providing_count As Integer = clsOdbc.executeScalar_int("SELECT Count(1) As UserCount From mlm_commitment Where status=3 and asking_id=" & intUserID)

        If row_providing_count > 0 Then

            Dim strQuery As String = "SELECT a.UserName As PName,c.UserName As AName,b.amount,DATE(b.date_assigned) As commit_date,d.my_sponsar_id As PID,e.my_sponsar_id As AID,b.asking_id,b.id  From mlm_login a,mlm_commitment b,mlm_login c,mlm_referral d,mlm_referral e Where b.providing_id=a.userid and b.asking_id=c.userid and b.providing_id=d.userid and b.asking_id=e.userid and b.status=3 and b.asking_id=" & intUserID
            Dim ds As New Data.DataSet
            Dim PName, AName, commit_date, PID, AID, amount, asking_id, commitment_id As String
            Dim strLiteral As String = ""

            Try
                ds = clsOdbc.getDataSet(strQuery)

                If ds.Tables(0).Rows.Count > 0 Then

                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

                        PName = ds.Tables(0).Rows(i).Item(0).ToString
                        AName = ds.Tables(0).Rows(i).Item(1).ToString
                        amount = ds.Tables(0).Rows(i).Item(2).ToString
                        commit_date = ds.Tables(0).Rows(i).Item(3).ToString
                        PID = ds.Tables(0).Rows(i).Item(4).ToString
                        AID = ds.Tables(0).Rows(i).Item(5).ToString
                        asking_id = ds.Tables(0).Rows(i).Item(6).ToString
                        commitment_id = ds.Tables(0).Rows(i).Item(7).ToString

                        Dim msgCount As Integer = clsOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_commitment_chat WHERE cmmitment_id=" & commitment_id)

                        strLiteral = "<tr>" _
                                & "<td >" & PID & "</td>" _
                                & "<td >" & PName & "</td>" _
                                & "<td >" & AID & "</td>" _
                                & "<td >" & AName & "</td>" _
                                & "<td >" & amount & "</td>" _
                                & "<td >" & commit_date & "</td>" _
                                & "<td >0</td>" _
                                & "<td ><a href=""javascript:my_window1=window.open('commitment_chat.aspx?CID=" & commitment_id & "', 'my_window1','status=no,location=no,menubar=no,titlebar=no,resizable=no,scrollbars=no,width=650,height=558,Top=250,right=50,left=200');my_window1.focus()"">" & msgCount & "</a></td>" _
                                & "<td ><a href=""javascript:my_window1=window.open('payment_done.aspx?CID=" & commitment_id & "', 'my_window1','status=no,location=no,menubar=no,titlebar=no,resizable=no,scrollbars=no,width=650,height=558,Top=250,right=50,left=200');my_window1.focus()"">Completed</a></td>" _
                                & "</tr>"

                        'strLiteral = "<div class=""arrg"">" _
                        '            & "<table width=""100%"" border=""0"" cellspacing=""2"" cellpadding=""2"" class=""arrg_tbarrg"">" _
                        '            & "<tbody><tr><td width=""36"" rowspan=""2"" class=""arrg_num"">" _
                        '            & "<img src=""../templates/images/ok.png"" width=""36"" height=""36"" class=""arrg_status_img""><br>" _
                        '            & "<span class=""arrg_sm10""><span class=""translate"">Number</span>:<br></span><span class=""arrg_id"">" _
                        '            & "" & AID & "</span>" _
                        '            & "</td><td class=""arrg_status_name"">You confirmed funds reception (Request to Help " & PID & ")</td>" _
                        '            & "<td class=""arrg_msg"" style=""width:120px;""><span>" _
                        '            & "<a href=""javascript:my_window1=window.open('commitment_chat.aspx?CID=" & commitment_id & "', 'my_window1','status=no,location=no,menubar=no,titlebar=no,resizable=no,scrollbars=no,width=650,height=558,Top=250,right=50,left=200');my_window1.focus()"">Messages:" & msgCount _
                        '            & "</a></span></td></tr><tr><td colspan=""3""><table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0""><tbody><tr><td class=""arrg_num"">" _
                        '            & "<span class=""arrg_sm10""><span class=""translate"">Date of creating</span>:<br></span>" _
                        '            & "<span class=""arrg_date"">" & commit_date & "</span></td><td class=""arrg_name1"">" _
                        '            & "<span class=""arrg_user_in"">" & PName & "</span></td>" _
                        '            & "<td align=""center"" class=""arrg_summ""><span class=""arrg_summ_in"">&gt;</span> &nbsp;<span class=""arrg_amt"">" & amount & " INR</span> &nbsp;" _
                        '            & "<span class=""arrg_summ_out"">&gt; </span><div class=""arrg_out_files"">" _
                        '            & "<span class=""files""><span class=""translate"">Confirmation</span>:" _
                        '            & "</span></div></td><td class=""arrg_name2""><span class=""arrg_user_out"">" _
                        '            & "<a href=""javascript:my_window1=window.open('payment_done.aspx?CID=" & commitment_id & "', 'my_window1','status=no,location=no,menubar=no,titlebar=no,resizable=no,scrollbars=no,width=650,height=558,Top=250,right=50,left=200');my_window1.focus()"">" & AName & "</a>" _
                        '            & "</span></td><td width=""30""><img src=""../templates/images/printout.png"" width=""30"" height=""30"" style=""cursor: pointer; display: none;"" class=""arrg_print""></td>" _
                        '            & "</tr></tbody></table></td></tr></tbody></table></div>"


                        litDashboard.Text = litDashboard.Text & strLiteral

                    Next

                End If

            Catch ex As Exception

            End Try

        End If

    End Sub

    Private Sub Pending_Commitment_Status(ByVal litDashboard As Literal, ByVal intUserID As Integer)

        Dim row_providing_count As Integer = clsOdbc.executeScalar_int("SELECT Count(1) As UserCount From mlm_provide_help Where userid=" & intUserID)

        If row_providing_count > 0 Then

            Dim strQuery As String = "SELECT DISTINCT a.username,b.actual_amount,DATE_FORMAT(DATE(b.commit_date), '%D %M %Y') As commit_date From mlm_login a,mlm_provide_help b Where a.userid=b.userid and a.userid=" & intUserID
            Dim ds As New Data.DataSet
            Dim my_UserName, my_commit_amount, my_commit_date As String
            Dim strLiteral As String = ""

            Try
                ds = clsOdbc.getDataSet(strQuery)

                If ds.Tables(0).Rows.Count > 0 Then

                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

                        my_UserName = ds.Tables(0).Rows(i).Item(0).ToString
                        my_commit_amount = ds.Tables(0).Rows(i).Item(1).ToString
                        my_commit_date = ds.Tables(0).Rows(i).Item(2).ToString

                        strLiteral = "<div class=""ordout green"">" _
                                    & "<table width=""100%"" border=""0"" cellspacing=""6"" cellpadding=""0"">" _
                                    & "<tbody><tr>" _
                                    & "<td class=""ord_title""><span class=""translate"">Request to Help Someone</span></td>" _
                                    & "<td width=""32""><img src=""../templates/images/strelka_32.png"" width=""32"" height=""32""></td></tr><tr>" _
                                    & "<td colspan=""2"" class=""ord_body"">" _
                                    & "<span class=""translate"">Name</span>: <span class=""order_out_fio"">" & my_UserName & "</span><br>" _
                                    & "<span class=""translate"">Commitment Amount</span>:" _
                                    & "<span class=""order_out_amount"">" & my_commit_amount & "</span>" _
                                    & "<span class=""order_out_currency""> INR</span><br>" _
                                    & "<span class=""translate"">Commitment Date</span>:" _
                                    & "<span class=""order_out_date"">" & my_commit_date & "</span><br>" _
                                    & "</td></tr></tbody></table></div>"


                        litDashboard.Text = litDashboard.Text & strLiteral

                    Next

                End If

            Catch ex As Exception

            End Try

        End If


    End Sub

    Private Sub Asking_Commitment_Status(ByVal litDashboard As Literal, ByVal intUserID As Integer)

        Dim row_providing_count As Integer = clsOdbc.executeScalar_int("SELECT Count(1) As UserCount From mlm_help_me Where userid=" & intUserID)

        If row_providing_count > 0 Then

            Dim strQuery As String = "SELECT a.UserName,b.ask_actual_amount,DATE(b.ask_date) As ask_date,b.ask_status,c.balance_amount From mlm_login a,mlm_help_me b,mlm_my_balance c Where a.userid=b.userid and b.userid=c.userid and b.userid=" & intUserID
            Dim ds As New Data.DataSet
            Dim my_UserName, ask_amount, ask_date, ask_status, balance_amount As String
            Dim strLiteral As String = ""

            Try
                ds = clsOdbc.getDataSet(strQuery)

                If ds.Tables(0).Rows.Count > 0 Then

                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

                        my_UserName = ds.Tables(0).Rows(i).Item(0).ToString
                        ask_amount = ds.Tables(0).Rows(i).Item(1).ToString
                        ask_date = ds.Tables(0).Rows(i).Item(2).ToString
                        ask_status = ds.Tables(0).Rows(i).Item(3).ToString
                        balance_amount = ds.Tables(0).Rows(i).Item(4).ToString

                        If ask_status = "0" Then
                            ask_status = "Pending"
                        ElseIf ask_status = "1" Then
                            ask_status = "Completed"
                        End If

                        strLiteral = "<div class=""ordout"">" _
                                    & "<table width=""100%"" border=""0"" cellspacing=""6"" cellpadding=""0"">" _
                                    & "<tbody><tr>" _
                                    & "<td class=""ord_title""><span class=""translate"">Request for Help Me</span></td>" _
                                    & "<td width=""32""><img src=""../templates/images/strelka_32.png"" width=""32"" height=""32""></td></tr><tr>" _
                                    & "<td colspan=""2"" class=""ord_body"">" _
                                    & "<span class=""translate"">Name</span>: <span class=""order_out_fio"">" & my_UserName & "</span><br>" _
                                    & "<span class=""translate"">Balance Amount</span>:" _
                                    & "<span class=""order_out_amount"">" & balance_amount & "</span>" _
                                    & "<span class=""order_out_currency""> INR</span><br>" _
                                     & "<span class=""translate"">Ask for Help</span>:" _
                                    & "<span class=""order_out_amount"">" & ask_amount & "</span>" _
                                    & "<span class=""order_out_currency""> INR</span><br>" _
                                    & "<span class=""translate"">Commitment Date</span>:" _
                                    & "<span class=""order_out_date"">" & ask_date & "</span><br>" _
                                    & "<span class=""translate"">Status</span>:" _
                                    & "<span class=""order_out_date"">" & ask_status & "</span><br>" _
                                    & "</td></tr></tbody></table></div>"


                        litDashboard.Text = litDashboard.Text & strLiteral

                    Next

                End If

            Catch ex As Exception

            End Try

        End If


    End Sub

    Public Function get_Direct_Members_DropDown(ByVal intUserID As Integer, ByVal ddlDropDownList As DropDownList) As Integer

        Dim strQuery As String = "SELECT a.userid,c.UserName from mlm_referral a,mlm_referral b,mlm_login c Where c.Active=1 and c.status=1 and a.referral_id=b.my_sponsar_id and a.userid=c.userid and b.userid=" & intUserID & " Order By c.UserName ASC"
        Dim ds As New Data.DataSet

        ddlDropDownList.Items.Clear()

        Try
            ds = clsOdbc.getDataSet(strQuery)

            If ds.Tables(0).Rows.Count > 0 Then

                ddlDropDownList.DataSource = ds
                ddlDropDownList.DataTextField = "UserName"
                ddlDropDownList.DataValueField = "userid"
                ddlDropDownList.DataBind()
            End If
            ddlDropDownList.Items.Insert(0, "Select Downline Member")
            ddlDropDownList.SelectedIndex = 0
        Catch ex As Exception

        Finally
            ds.Dispose()
        End Try



    End Function

    Public Function get_Direct_Referral(ByVal intUserID As Integer) As String

        Dim strReferralID As String = clsOdbc.executeScalar_str("SELECT referral_id From mlm_referral Where userid=" & intUserID)

        Dim strQuery As String = "SELECT b.UserName,b.Email,b.mobile_number From mlm_referral a,mlm_login b Where b.Active=1 and b.status=1 and a.userid=b.userid and a.my_sponsar_id='" & strReferralID & "'"
        Dim ds As New Data.DataSet
        Dim my_UserName, my_Email, my_mobile_number As String

        Dim strDetails As String = ""

        Try
            ds = clsOdbc.getDataSet(strQuery)

            If ds.Tables(0).Rows.Count > 0 Then

                Try

                    my_UserName = ds.Tables(0).Rows(0).Item(0).ToString
                    my_Email = ds.Tables(0).Rows(0).Item(1).ToString
                    my_mobile_number = ds.Tables(0).Rows(0).Item(2).ToString

                    strDetails = "<b>Name: </b>" & my_UserName & " <b>E-mail: </b>" & my_Email & " <b>Mobile: </b>" & my_mobile_number

                    Return strDetails

                Catch ex As Exception

                End Try

            End If

        Catch ex As Exception

        Finally
            ds.Dispose()
        End Try

    End Function


    '*** Get Direct Referral Details ***
    Public Function Get_Direct_Referral_Details(ByVal intUserID As Integer) As String

        Dim row_Direct_Referral_ID As String = clsOdbc.executeScalar_str("SELECT referral_id From mlm_referral Where userid=" & intUserID)
        Dim Direct_Details As String = ""

        Dim strQuery As String = "SELECT b.UserName,b.Email,b.mobile_number From mlm_referral a,mlm_login b Where b.Active=1 and b.status=1 and a.userid=b.userid and a.my_sponsar_id='" & row_Direct_Referral_ID & "'"
        Dim ds As New Data.DataSet

        Try

            ds = clsOdbc.getDataSet(strQuery)

            If ds.Tables(0).Rows.Count > 0 Then

                Dim UserName As String = ds.Tables(0).Rows(0).Item(0).ToString
                Dim Email As String = ds.Tables(0).Rows(0).Item(1).ToString
                Dim mobile_number As String = ds.Tables(0).Rows(0).Item(2).ToString

                Direct_Details = "<b>Name: </b>" & UserName & " <b>E-mail: </b>" & UserName & " <b>Mobile: </b>" & mobile_number

            End If

        Catch ex As Exception

        Finally
            ds.Dispose()
        End Try

        Return Direct_Details

    End Function


End Class
