Imports Microsoft.VisualBasic
Imports System.Data

Public Class clsBinaryTree
    Dim clsOdbc As New ODBC
	 	Dim Ec As New EncryptTest.Encryption

    Dim strColorclass, strColorclass2, strPackage As String

    Public Function FillBinaryLiteral(ByVal parent_id As Integer) As String
        Dim userdata As String = ""
        Dim userdata1 As String = ""
        Dim strClass1 As String
		
		Dim strQuery As String = ""
        Dim ds As New Data.DataSet

        Try
            strQuery = "SELECT SUBSTRING(b.UserName,1,10) as UserName,a.my_sponsar_id,a.package_id, a.status From mlm_login a,mlm_personal_details  b Where b.userid=a.userid  and a.userid=" & parent_id
            ds = clsOdbc.getDataSet(strQuery)

         

			Dim username As String = ds.Tables(0).Rows(0).Item(0).ToString
			Dim my_sponsar_id As String = ds.Tables(0).Rows(0).Item(1).ToString

			Dim strStatus1 As Integer = ds.Tables(0).Rows(0).Item(2).ToString
            If strStatus1 = 0 Then
                strClass1 = "free.png"
            ElseIf strStatus1 = 1 Then
                strClass1 = "plan1.png"
			 ElseIf strStatus1 = 2 Then
                strClass1 = "plan2.png"
			 ElseIf strStatus1 = 3 Then
                strClass1 = "plan3.png"
			 ElseIf strStatus1 = 4 Then
                strClass1 = "plan4.png"
			 ElseIf strStatus1 = 5 Then
                strClass1 = "plan5.png"
			 ElseIf strStatus1 = 6 Then
                strClass1 = "plan6.png"
			 ElseIf strStatus1 = 7 Then
                strClass1 = "plan7.png"
            Else
                strClass1 = "RegisteredSpot.png"
            End If            

            userdata = "<div id=""tbl-tree""><table class=""table table-primary text-center"" ><tr><th colspan=""8"" class=""text-center""><div class=""profile""> <a  href=""#"" onmouseover=""popup($('#" & parent_id & "').html());"" ><img src=""../tree/Geneology/" & strClass1 & """ alt=""user""  /></a><div class=""name"">" & my_sponsar_id & "<br/>" & username & "</div></div><!--profile end--></th></tr><tr><td colspan=""8"" style=""padding:0;""><div class=""line-vertical""></div></td></tr><tr><td>&nbsp;</td> <td>&nbsp;</td><td><div class=""line-horizontal""></div><div class=""line-vertical"" style=""float:left;""></div></td><td><div class=""line-horizontal""></div></td><td><div class=""line-horizontal""></div></td><td><div class=""line-horizontal""></div><div class=""line-vertical"" style=""float:right;""></div></td><td>&nbsp;</td><td>&nbsp;</td></tr>"

            userdata1 = get_Recursive_Binary(parent_id)

            Return userdata & userdata1 & "</table></div>"

        Catch ex As Exception

        Finally
            ds.Dispose()
        End Try

    End Function


    Public Function get_Recursive_Binary(ByVal parent_id As Integer) As String

        Dim row_Child_Tree_Count As Integer = clsOdbc.executeScalar_int("SELECT Count(1) as count From mlm_login Where node_flag>0 and parent_node_id=" & parent_id & " Order By node_flag ASC")

        Dim strLit1, strLit2, strLit3, strLit4, strLit5, strLit6, strlit7, strlit8 As String
        Dim strClass1, strClass2 As String
        Dim str(10) As String
        Dim str1(10) As String


        strLit1 = ""

        strLit2 = ""

        strLit3 = ""

        strLit4 = ""

        strLit5 = ""

        strLit6 = ""

        strlit7 = ""

        strlit8 = ""


        If row_Child_Tree_Count > 0 Then

            Dim strQuery As String = ""
            Dim ds As New Data.DataSet

            Try
                strQuery = "SELECT a.userid,SUBSTRING(b.UserName,1,10) as UserName,a.node_flag,a.my_sponsar_id,a.package_id, a.status From mlm_login a,mlm_personal_details  b Where b.userid=a.userid  and a.node_flag>0 and  a.parent_node_id=" & parent_id & " Order By a.node_flag ASC"
                ds = clsOdbc.getDataSet(strQuery)

                If (ds.Tables(0).Rows.Count > 0) Then


                    Dim Tree_Child_UserID1 As Integer = ds.Tables(0).Rows(0).Item(0).ToString
                    Dim Tree_Child_UserName1 As String = ds.Tables(0).Rows(0).Item(1).ToString
                    Dim Tree_Node_Flag1 As String = ds.Tables(0).Rows(0).Item(2).ToString
                    Dim Tree_Child_SystemID1 As String = ds.Tables(0).Rows(0).Item(3).ToString

                    Dim strStatus1 As Integer = ds.Tables(0).Rows(0).Item(4).ToString
                    If strStatus1 = 0 Then
						strClass1 = "free.png"
					ElseIf strStatus1 = 1 Then
						strClass1 = "plan1.png"
					 ElseIf strStatus1 = 2 Then
						strClass1 = "plan2.png"
					 ElseIf strStatus1 = 3 Then
						strClass1 = "plan3.png"
					 ElseIf strStatus1 = 4 Then
						strClass1 = "plan4.png"
					 ElseIf strStatus1 = 5 Then
						strClass1 = "plan5.png"
					 ElseIf strStatus1 = 6 Then
						strClass1 = "plan6.png"
					 ElseIf strStatus1 = 7 Then
						strClass1 = "plan7.png"
					Else
						strClass1 = "RegisteredSpot.png"
					End If  


                    If row_Child_Tree_Count = 2 Then


                        Dim Tree_Child_UserID2 As Integer = ds.Tables(0).Rows(1).Item(0).ToString
                        Dim Tree_Child_UserName2 As String = ds.Tables(0).Rows(1).Item(1).ToString
                        Dim Tree_Node_Flag2 As String = ds.Tables(0).Rows(1).Item(2).ToString
                        Dim Tree_Child_SystemID2 As String = ds.Tables(0).Rows(1).Item(3).ToString

                        Dim strStatus2 As Integer = ds.Tables(0).Rows(1).Item(4).ToString
                        If strStatus2 = 0 Then
							strClass2 = "free.png"
						ElseIf strStatus2 = 1 Then
							strClass2 = "plan1.png"
						 ElseIf strStatus2 = 2 Then
							strClass2 = "plan2.png"
						 ElseIf strStatus2 = 3 Then
							strClass2 = "plan3.png"
						 ElseIf strStatus2 = 4 Then
							strClass2 = "plan4.png"
						 ElseIf strStatus2 = 5 Then
							strClass2 = "plan5.png"
						 ElseIf strStatus2 = 6 Then
							strClass2 = "plan6.png"
						 ElseIf strStatus2 = 7 Then
							strClass2 = "plan7.png"
						Else
							strClass2 = "RegisteredSpot.png"
						End If  

                        strLit1 = strLit1 & "<tr><td colspan=""4""><div class=""profile"">"

                        strLit2 = strLit2 & "<a href=""Tree.aspx?" & Ec.EncryptQueryString(String.Format("userid={0}", Tree_Child_UserID1.ToString()), "VbFM45Lt") & """  onmouseover=""popup($('#" & Tree_Child_UserID1 & "').html());"" ><img src=""../tree/Geneology/" & strClass1 & """ alt=""user"" /></a><div class=""name"">" & Tree_Child_SystemID1 & "<br/>" & Tree_Child_UserName1 & "</div></div><!--profile end--></td>"


                        strLit3 = strLit3 & "<td colspan=""4""><div class=""profile""><!-- Hidden on load by CSS --><!-- Show the hidden div on hover --> <a href=""Tree.aspx?" & Ec.EncryptQueryString(String.Format("userid={0}", Tree_Child_UserID2.ToString()), "VbFM45Lt") & """  onmouseover=""popup($('#" & Tree_Child_UserID2 & "').html());""  ><img src=""../tree/Geneology/" & strClass2 & """ alt=""user""/> </a><div class=""name"">" & Tree_Child_SystemID2 & "<br/>" & Tree_Child_UserName2 & "</div></div><!--profile end--></td></tr>"


                        strlit8 = strlit8 & "<tr>"

                        strLit4 = strLit4 & displayLine()

                        strLit5 = strLit5 & "<tr>"

                        str = getLit(Tree_Child_UserID1)

                        str1 = getLit(Tree_Child_UserID2)

                        strLit5 = strLit5 & str(0)

                        strLit6 = strLit6 & str1(0)

                        strLit6 = strLit6 & "</tr>"
                        strLit6 = strLit6 & "</tr>"
                        strlit7 = strlit7 & displayLine2()
                        strlit8 = strlit8 & str(1)
                        strlit8 = strlit8 & str1(1)
                        strlit8 = strlit8 & "</tr>"

                    ElseIf row_Child_Tree_Count = 1 And Tree_Node_Flag1 = "1" Then

                        strLit1 = strLit1 & "<tr><td colspan=""4""><div class=""profile"">"

                        strLit2 = strLit2 & "<a href=""Tree.aspx?" & Ec.EncryptQueryString(String.Format("userid={0}", Tree_Child_UserID1.ToString()), "VbFM45Lt") & " ""onmouseover=""popup($('#" & Tree_Child_UserID1 & "').html());"" ><img src=""../tree/Geneology/" & strClass1 & """ alt=""user"" /></a><div class=""name"">" & Tree_Child_SystemID1 & "<br/>" & Tree_Child_UserName1 & "</div></div><!--profile end--></td>"

                        strLit3 = strLit3 & "<td colspan=""4""><a href=""../../register.aspx?pos=R&pid=" & parent_id & "&sid=sessionID "" target=""_blank""><img src=""../tree/Geneology/RegisteredSpot.png"" alt=""user"" /></a></td></tr>"

                        strlit8 = strlit8 & "<tr>"

                        strLit4 = strLit4 & displayLine()
                        strLit5 = strLit5 & "<tr>"
                        str = getLit(Tree_Child_UserID1)


                        strLit5 = strLit5 & str(0)

                        strLit5 = strLit5 & "<td colspan=""2""><img src=""../tree/Geneology/RegisteredSpot.png"" alt=""user"" /></td><td colspan=""2""><img src=""../tree/Geneology/RegisteredSpot.png"" alt=""user"" /><!--profile end--></td>"
                        strLit5 = strLit5 & "</tr>"
                        strlit7 = strlit7 & displayLine2()
                        strlit8 = strlit8 & str(1)
                        strlit8 = strlit8 & "<td><img src=""../tree/Geneology/RegisteredSpot.png"" alt=""user"" /></td><td><img src=""../tree/Geneology/RegisteredSpot.png"" alt=""user"" /></td>"
                        strlit8 = strlit8 & "<td><img src=""../tree/Geneology/RegisteredSpot.png"" alt=""user"" /></td><td><img src=""../tree/Geneology/RegisteredSpot.png"" alt=""user"" /></td>"
                        strlit8 = strlit8 & "</tr>"





                    ElseIf row_Child_Tree_Count = 1 And Tree_Node_Flag1 = "2" Then

                        strLit1 = strLit1 & "<tr><td colspan=""4"">"

                        strLit2 = strLit2 & "<a href=""../../register.aspx?pos=L&pid=" & parent_id & "&sid=sessionID "" target=""_blank""><img src=""../tree/Geneology/RegisteredSpot.png"" alt=""user"" /></a></td>"

                        strLit3 = strLit3 & "<td colspan=""4""><div class=""profile""><!-- Hidden on load by CSS --><!-- Show the hidden div on hover -->"" <a href=""Tree.aspx?" & Ec.EncryptQueryString(String.Format("userid={0}", Tree_Child_UserID1.ToString()), "VbFM45Lt") & " "" onmouseover=""popup($('#" & Tree_Child_UserID1 & "').html());"" ><img src=""../tree/Geneology/" & strClass1 & """ alt=""user"" /></a><div class=""name"">" & Tree_Child_SystemID1 & "<br/>" & Tree_Child_UserName1 & "</div></div><!--profile end--></td></tr>"


                        strLit4 = strLit4 & displayLine()

                        strlit8 = strlit8 & "<tr>"

                        strLit5 = strLit5 & "<tr><td colspan=""2""><img src=""../tree/Geneology/RegisteredSpot.png"" alt=""user"" /><!--profile end--></td><td colspan=""2""><img src=""../tree/Geneology/RegisteredSpot.png"" alt=""user"" /><!--profile end--></td>"

                        strlit8 = strlit8 & "<td><img src=""../tree/Geneology/RegisteredSpot.png"" alt=""user"" /></td><td><img src=""../tree/Geneology/RegisteredSpot.png"" alt=""user"" /></td>"
                        strlit8 = strlit8 & "<td><img src=""../tree/Geneology/RegisteredSpot.png"" alt=""user"" /></td><td><img src=""../tree/Geneology/RegisteredSpot.png"" alt=""user"" /></td>"
                        str = getLit(Tree_Child_UserID1)
                        strLit6 = strLit6 & str(0)
                        strLit6 = strLit6 & "</tr>"
                        strlit8 = strlit8 & str(1)
                        strlit8 = strlit8 & "</tr>"

                        strlit7 = strlit7 & displayLine2()

                    End If

                End If
            Catch ex As Exception

            Finally
                ds.Dispose()
            End Try

        Else

            strLit1 = strLit1 & "<tr><td colspan=""4"">"

            strLit2 = strLit2 & "<a href=""../../register.aspx?pos=L&pid=" & parent_id & "&sid=sessionID "" target=""_blank""><img src=""../tree/Geneology/RegisteredSpot.png"" alt=""user"" /></a></td>"

            strLit3 = strLit3 & "<td colspan=""4""><a href=""../../register.aspx?pos=R&pid=" & parent_id & "&sid=sessionID "" target=""_blank""><img src=""../tree/Geneology/RegisteredSpot.png"" alt=""user"" /></a></td></tr>"

            strLit4 = strLit4 & displayLine()

            strLit5 = strLit5 & "<tr><td colspan=""2""><img src=""../tree/Geneology/RegisteredSpot.png"" alt=""user"" /><!--profile end--></td><td colspan=""2""><img src=""../tree/Geneology/RegisteredSpot.png"" alt=""user"" /><!--profile end--></td>"

            strLit6 = strLit6 & "<td colspan=""2""><img src=""../tree/Geneology/RegisteredSpot.png"" alt=""user"" /></td><td colspan=""2""><img src=""../tree/Geneology/RegisteredSpot.png"" alt=""user"" /></td></tr>"

            strlit7 = strlit7 & displayLine2()
            strlit8 = strlit8 & "<tr>"
            strlit8 = strlit8 & "<td><img src=""../tree/Geneology/RegisteredSpot.png"" alt=""user"" /></td><td><img src=""../tree/Geneology/RegisteredSpot.png"" alt=""user"" /></td>"
            strlit8 = strlit8 & "<td><img src=""../tree/Geneology/RegisteredSpot.png"" alt=""user"" /></td><td><img src=""../tree/Geneology/RegisteredSpot.png"" alt=""user"" /></td>"
            strlit8 = strlit8 & "<td><img src=""../tree/Geneology/RegisteredSpot.png"" alt=""user"" /></td><td><img src=""../tree/Geneology/RegisteredSpot.png"" alt=""user"" /></td>"
            strlit8 = strlit8 & "<td><img src=""../tree/Geneology/RegisteredSpot.png"" alt=""user"" /></td><td><img src=""../tree/Geneology/RegisteredSpot.png"" alt=""user"" /></td>"
            strlit8 = strlit8 & "</tr>"


        End If

        Return strLit1 & strLit2 & strLit3 & strLit4 & strLit5 & strLit6 & strlit7 & strlit8



    End Function

    Public Function getLit(ByVal parent_id As Integer) As String()

        Dim row_Child_Tree_Count As Integer = clsOdbc.executeScalar_int("SELECT Count(1) From mlm_login Where  node_flag>0 and  parent_node_id=" & parent_id & " Order By node_flag ASC")

        Dim strLit As String = ""
        Dim strLit1 As String = ""

        Dim strClass1, strClass2 As String

        If row_Child_Tree_Count > 0 Then

            Dim strQuery As String = ""
            Dim ds As New Data.DataSet

            Try
                strQuery = "SELECT a.userid,SUBSTRING(b.UserName,1,10) as UserName,a.node_flag,a.my_sponsar_id, a.package_id, a.status From mlm_login a,mlm_personal_details b Where b.userid=a.userid and a.node_flag>0 and  a.parent_node_id=" & parent_id & " Order By node_flag ASC"
                ds = clsOdbc.getDataSet(strQuery)

                If (ds.Tables(0).Rows.Count > 0) Then


                    Dim Tree_Child_UserID1 As Integer = ds.Tables(0).Rows(0).Item(0).ToString
                    Dim Tree_Child_UserName1 As String = ds.Tables(0).Rows(0).Item(1).ToString
                    Dim Tree_Node_Flag1 As String = ds.Tables(0).Rows(0).Item(2).ToString
                    Dim Tree_Child_SystemID1 As String = ds.Tables(0).Rows(0).Item(3).ToString

                    Dim strStatus1 As Integer = ds.Tables(0).Rows(0).Item(4).ToString
					If strStatus1 = 0 Then
						strClass1 = "free.png"
					ElseIf strStatus1 = 1 Then
						strClass1 = "plan1.png"
					 ElseIf strStatus1 = 2 Then
						strClass1 = "plan2.png"
					 ElseIf strStatus1 = 3 Then
						strClass1 = "plan3.png"
					 ElseIf strStatus1 = 4 Then
						strClass1 = "plan4.png"
					 ElseIf strStatus1 = 5 Then
						strClass1 = "plan5.png"
					 ElseIf strStatus1 = 6 Then
						strClass1 = "plan6.png"
					 ElseIf strStatus1 = 7 Then
						strClass1 = "plan7.png"
					Else
						strClass1 = "RegisteredSpot.png"
					End If                     

                    If row_Child_Tree_Count = 2 Then

                        Dim Tree_Child_UserID2 As Integer = ds.Tables(0).Rows(1).Item(0).ToString
                        Dim Tree_Child_UserName2 As String = ds.Tables(0).Rows(1).Item(1).ToString
                        Dim Tree_Node_Flag2 As String = ds.Tables(0).Rows(1).Item(2).ToString
                        Dim Tree_Child_SystemID2 As String = ds.Tables(0).Rows(1).Item(3).ToString

                        Dim strStatus2 As Integer = ds.Tables(0).Rows(1).Item(4).ToString
                        If strStatus2 = 0 Then
							strClass2 = "free.png"
						ElseIf strStatus2 = 1 Then
							strClass2 = "plan1.png"
						 ElseIf strStatus2 = 2 Then
							strClass2 = "plan2.png"
						 ElseIf strStatus2 = 3 Then
							strClass2 = "plan3.png"
						 ElseIf strStatus2 = 4 Then
							strClass2 = "plan4.png"
						 ElseIf strStatus2 = 5 Then
							strClass2 = "plan5.png"
						 ElseIf strStatus2 = 6 Then
							strClass2 = "plan6.png"
						 ElseIf strStatus2 = 7 Then
							strClass2 = "plan7.png"
						Else
							strClass2 = "RegisteredSpot.png"
						End If  

                        strLit = strLit & "<td colspan=""2""><div class=""profile"">" & "<a href=""Tree.aspx?" & Ec.EncryptQueryString(String.Format("userid={0}", Tree_Child_UserID1.ToString()), "VbFM45Lt") & """ onmouseover=""popup($('#" & Tree_Child_UserID1 & "').html());"" ><img src=""../tree/Geneology/" & strClass1 & """ alt=""user"" /></a><div class=""name"">" & Tree_Child_SystemID1 & "<br/>" & Tree_Child_UserName1 & "</div></div></td><td colspan=""2""><div class=""profile"">" & " <a href=""Tree.aspx?" & Ec.EncryptQueryString(String.Format("userid={0}", Tree_Child_UserID2.ToString()), "VbFM45Lt") & """ onmouseover=""popup($('#" & Tree_Child_UserID2 & "').html());"" ><img src=""../tree/Geneology/" & strClass2 & """ alt=""user"" /></a><div class=""name"">" & Tree_Child_SystemID2 & "<br/>" & Tree_Child_UserName2 & "</div></div></td>"




                        strLit1 = strLit1 & getLit2(Tree_Child_UserID1)

                        strLit1 = strLit1 & getLit2(Tree_Child_UserID2)

                    ElseIf row_Child_Tree_Count = 1 And Tree_Node_Flag1 = "1" Then

                        strLit = strLit & "<td colspan=""2""><div class=""profile"">" & "<a href=""Tree.aspx?" & Ec.EncryptQueryString(String.Format("userid={0}", Tree_Child_UserID1.ToString()), "VbFM45Lt") & " "" onmouseover=""popup($('#" & Tree_Child_UserID1 & "').html());"" ><img src=""../tree/Geneology/" & strClass1 & """ alt=""user"" /></a><div class=""name"">" & Tree_Child_SystemID1 & "<br/>" & Tree_Child_UserName1 & "</div></div></td><td colspan=""2""><a href=""../../register.aspx?pos=R&pid=" & parent_id & "&sid=sessionID "" target=""_blank""><img src=""../tree/Geneology/RegisteredSpot.png"" alt=""user"" /></a></td>"

                        strLit1 = strLit1 & getLit2(Tree_Child_UserID1)
                        strLit1 = strLit1 & "<td ><img src=""../tree/Geneology/RegisteredSpot.png"" alt=""user"" /><!--profile end--></td><td ><img src=""../tree/Geneology/RegisteredSpot.png"" alt=""user"" /><!--profile end--></td>"

                    ElseIf row_Child_Tree_Count = 1 And Tree_Node_Flag1 = "2" Then

                        strLit = strLit & "<td colspan=""2""><a href=""../../register.aspx?pos=L&pid=" & parent_id & "&sid=sessionID "" target=""_blank""><img src=""../tree/Geneology/RegisteredSpot.png"" alt=""user"" /></a></td><td colspan=""2""><div class=""profile"">" & " <a href=""Tree.aspx?" & Ec.EncryptQueryString(String.Format("userid={0}", Tree_Child_UserID1.ToString()), "VbFM45Lt") & " "" onmouseover=""popup($('#" & Tree_Child_UserID1 & "').html());"" ><img src=""../tree/Geneology/" & strClass1 & """ alt=""user"" /></a><div class=""name"">" & Tree_Child_SystemID1 & "<br/>" & Tree_Child_UserName1 & "</div></div></td>"

                        strLit1 = strLit1 & "<td ><img src=""../tree/Geneology/RegisteredSpot.png"" alt=""user"" /><!--profile end--></td><td ><img src=""../tree/Geneology/RegisteredSpot.png"" alt=""user"" /><!--profile end--></td>"
                        strLit1 = strLit1 & getLit2(Tree_Child_UserID1)


                    End If


                End If
            Catch ex As Exception

            Finally
                ds.Dispose()
            End Try
        Else

            strLit = strLit & "<td colspan=""2""><a href=""../../register.aspx?pos=L&pid=" & parent_id & "&sid=sessionID "" target=""_blank""><img src=""../tree/Geneology/RegisteredSpot.png"" alt=""user"" /></a></td><td colspan=""2""><a href=""../../register.aspx?pos=R&pid=" & parent_id & "&sid=sessionID "" target=""_blank""><img src=""../tree/Geneology/RegisteredSpot.png"" alt=""user"" /></a></td>"
            strLit1 = strLit1 & "<td ><img src=""../tree/Geneology/RegisteredSpot.png"" alt=""user"" /><!--profile end--></td><td ><img src=""../tree/Geneology/RegisteredSpot.png"" alt=""user"" /><!--profile end--></td>"
            strLit1 = strLit1 & "<td ><img src=""../tree/Geneology/RegisteredSpot.png"" alt=""user"" /></td><td ><img src=""../tree/Geneology/RegisteredSpot.png"" alt=""user"" /></td>"

        End If

        Dim str(10) As String

        str(0) = strLit
        str(1) = strLit1


        Return str

    End Function

    Public Function displayLine() As String

        Dim strLit As String = ""
        strLit = strLit & "<tr><td colspan=""4""><div class=""line-vertical""></div></td><td colspan=""4""><div class=""line-vertical""></div></td></tr>"
        strLit = strLit & "<tr><td>&nbsp;</td><td><div class=""line-horizontal""></div><div class=""line-vertical"" style=""float:left;""></div></td><td><div class=""line-horizontal""></div><div class=""line-vertical"" style=""float:right;""></div></td> <td>&nbsp;</td><td>&nbsp;</td><td><div class=""line-horizontal""></div><div class=""line-vertical"" style=""float:left;""></div></td><td><div class=""line-horizontal""></div><div class=""line-vertical"" style=""float:right;""></div></td></tr>"

        Return strLit

    End Function

    Public Function displayLine2() As String

        Dim strLit As String = ""
        strLit = strLit & "<tr><td colspan=""2""><div class=""line-vertical""></div></td><td colspan=""2""><div class=""line-vertical""></div></td><td colspan=""2""><div class=""line-vertical""></div></td><td colspan=""2""><div class=""line-vertical""></div></td></tr>"
        strLit = strLit & "<tr><td><div class=""line-horizontal-right""></div><div class=""line-vertical"" style=""float:right;""></div></td><td><div class=""line-horizontal-left""></div><div class=""line-vertical"" style=""float:left;""></div></td><td><div class=""line-horizontal-right""></div><div class=""line-vertical"" style=""float:right;""></div></td><td><div class=""line-horizontal-left""></div><div class=""line-vertical"" style=""float:left;""></div></td><td><div class=""line-horizontal-right""></div><div class=""line-vertical"" style=""float:right;""></div></td><td><div class=""line-horizontal-left""></div><div class=""line-vertical"" style=""float:left;""></div></td><td><div class=""line-horizontal-right""></div><div class=""line-vertical"" style=""float:right;""></div></td><td><div class=""line-horizontal-left""></div><div class=""line-vertical"" style=""float:left;""></div></td></tr>"

        Return strLit

    End Function

    Public Function getLit2(ByVal parent_id As Integer) As String

        Dim row_Child_Tree_Count As Integer = clsOdbc.executeScalar_int("SELECT Count(1) as count From mlm_login Where  node_flag>0 and  parent_node_id=" & parent_id & " Order By node_flag ASC")

        Dim strLit As String = ""
        Dim strClass1, strClass2 As String

        If row_Child_Tree_Count > 0 Then

            Dim strQuery As String = ""
            Dim ds As New Data.DataSet

            Try
                strQuery = "SELECT a.userid,SUBSTRING(b.UserName,1,10) as UserName,a.node_flag,a.my_sponsar_id,a.product_status, a.status From mlm_login a,mlm_personal_details  b Where b.userid=a.userid  and a.node_flag>0 and a.parent_node_id=" & parent_id & " Order By node_flag ASC"
                ds = clsOdbc.getDataSet(strQuery)

                If (ds.Tables(0).Rows.Count > 0) Then


                    Dim Tree_Child_UserID1 As Integer = ds.Tables(0).Rows(0).Item(0).ToString
                    Dim Tree_Child_UserName1 As String = ds.Tables(0).Rows(0).Item(1).ToString
                    Dim Tree_Node_Flag1 As String = ds.Tables(0).Rows(0).Item(2).ToString
                    Dim Tree_Child_SystemID1 As String = ds.Tables(0).Rows(0).Item(3).ToString

                    Dim strStatus1 As Integer = ds.Tables(0).Rows(0).Item(4).ToString
                    If strStatus1 = 0 Then
						strClass1 = "free.png"
					ElseIf strStatus1 = 1 Then
						strClass1 = "plan1.png"
					 ElseIf strStatus1 = 2 Then
						strClass1 = "plan2.png"
					 ElseIf strStatus1 = 3 Then
						strClass1 = "plan3.png"
					 ElseIf strStatus1 = 4 Then
						strClass1 = "plan4.png"
					 ElseIf strStatus1 = 5 Then
						strClass1 = "plan5.png"
					 ElseIf strStatus1 = 6 Then
						strClass1 = "plan6.png"
					 ElseIf strStatus1 = 7 Then
						strClass1 = "plan7.png"
					Else
						strClass1 = "RegisteredSpot.png"
					End If 

                    If row_Child_Tree_Count = 2 Then

                        Dim Tree_Child_UserID2 As Integer = ds.Tables(0).Rows(1).Item(0).ToString
                        Dim Tree_Child_UserName2 As String = ds.Tables(0).Rows(1).Item(1).ToString
                        Dim Tree_Node_Flag2 As String = ds.Tables(0).Rows(1).Item(2).ToString
                        Dim Tree_Child_SystemID2 As String = ds.Tables(0).Rows(1).Item(3).ToString

                        Dim strStatus2 As Integer = ds.Tables(0).Rows(1).Item(4).ToString
                        If strStatus2 = 0 Then
							strClass2 = "free.png"
						ElseIf strStatus2 = 1 Then
							strClass2 = "plan1.png"
						 ElseIf strStatus2 = 2 Then
							strClass2 = "plan2.png"
						 ElseIf strStatus2 = 3 Then
							strClass2 = "plan3.png"
						 ElseIf strStatus2 = 4 Then
							strClass2 = "plan4.png"
						 ElseIf strStatus2 = 5 Then
							strClass2 = "plan5.png"
						 ElseIf strStatus2 = 6 Then
							strClass2 = "plan6.png"
						 ElseIf strStatus2 = 7 Then
							strClass2 = "plan7.png"
						Else
							strClass2 = "RegisteredSpot.png"
						End If 

                        strLit = strLit & "<td><div class=""profile"">" & "<a href=""Tree.aspx?" & Ec.EncryptQueryString(String.Format("userid={0}", Tree_Child_UserID1.ToString()), "VbFM45Lt") & """ onmouseover=""popup($('#" & Tree_Child_UserID1 & "').html());"" ><img src=""../tree/Geneology/" & strClass1 & """ alt=""user"" /></a><div class=""name"">" & Tree_Child_SystemID1 & "<br/>" & Tree_Child_UserName1 & "</div></div></td><td><div class=""profile"">" & "<a href=""Tree.aspx?" & Ec.EncryptQueryString(String.Format("userid={0}", Tree_Child_UserID2.ToString()), "VbFM45Lt") & """ onmouseover=""popup($('#" & Tree_Child_UserID2 & "').html());"" ><img src=""../tree/Geneology/" & strClass2 & """ alt=""user"" /></a><div class=""name"">" & Tree_Child_SystemID2 & "<br/>" & Tree_Child_UserName2 & "</div></div></td>"

                    ElseIf row_Child_Tree_Count = 1 And Tree_Node_Flag1 = "1" Then

                        strLit = strLit & "<td><div class=""profile"">" & "<a href=""Tree.aspx?" & Ec.EncryptQueryString(String.Format("userid={0}", Tree_Child_UserID1.ToString()), "VbFM45Lt") & """ onmouseover=""popup($('#" & Tree_Child_UserID1 & "').html());"" ><img src=""../tree/Geneology/" & strClass1 & """ alt=""user"" /></a><div class=""name"">" & Tree_Child_SystemID1 & "<br/>" & Tree_Child_UserName1 & "</div></div></td><td><a href=""../../register.aspx?pos=R&pid=" & parent_id & "&sid=sessionID "" target=""_blank""><img src=""../tree/Geneology/RegisteredSpot.png"" alt=""user"" /></a></td>"

                    ElseIf row_Child_Tree_Count = 1 And Tree_Node_Flag1 = "2" Then

                        strLit = strLit & "<td><a href=""../../register.aspx?pos=L&pid=" & parent_id & "&sid=sessionID "" target=""_blank""><img src=""../tree/Geneology/RegisteredSpot.png"" alt=""user"" /></a></td><td><div class=""profile"">" & "<a href=""Tree.aspx?" & Ec.EncryptQueryString(String.Format("userid={0}", Tree_Child_UserID1.ToString()), "VbFM45Lt") & " "" onmouseover=""popup($('#" & Tree_Child_UserID1 & "').html());"" ><img src=""../tree/Geneology/" & strClass1 & """ alt=""user"" /></a><div class=""name"">" & Tree_Child_SystemID1 & " <br/> " & Tree_Child_UserName1 & "</div></div></td>"


                    End If


                End If
            Catch ex As Exception

            Finally
                ds.Dispose()
            End Try


        Else
            strLit = strLit & "<td><a href=""../../register.aspx?pos=L&pid=" & parent_id & "&sid=sessionID "" target=""_blank""><img src=""../tree/Geneology/RegisteredSpot.png"" alt=""user"" /></a></td><td><a href=""../../register.aspx?pos=R&pid=" & parent_id & "&sid=sessionID "" target=""_blank""><img src=""../tree/Geneology/RegisteredSpot.png"" alt=""user"" /></a></td>"
        End If



        Return strLit

    End Function

    Public Function get_Recursive_Popup10(ByVal parent_id As Integer) As String

        Dim strQuery As String = ""
        Dim strString As String = ""
        Dim ds As New Data.DataSet
        Dim dt As New Data.DataTable

        Try
            strQuery = "SELECT a.UserName,b.left_business,b.right_business,c.left_business,c.right_business,b.l_members,b.r_members,b.direct_left,b.direct_right,d.my_sponsar_id,d.package_id,d.referral_id From mlm_personal_details a LEFT JOIN mlm_progress_count b ON a.userid=b.userid LEFT JOIN mlm_progress_count_current c ON a.userid=c.userid LEFT JOIN mlm_login d ON a.userid=d.userid Where d.node_flag>0 and a.userid=" & parent_id & ""

            ds = clsOdbc.getDataSet(strQuery)

            If (ds.Tables(0).Rows.Count > 0) Then

                Dim UserName As String = ds.Tables(0).Rows(0).Item(0).ToString
                Dim left_business_total As String = ds.Tables(0).Rows(0).Item(1).ToString
                Dim right_business_total As String = ds.Tables(0).Rows(0).Item(2).ToString
                Dim left_business_current As String = ds.Tables(0).Rows(0).Item(3).ToString

                Dim right_business_current As String = ds.Tables(0).Rows(0).Item(4).ToString
                Dim left_members As String = ds.Tables(0).Rows(0).Item(5).ToString

                Dim right_members As String = ds.Tables(0).Rows(0).Item(6).ToString
                Dim direct_left_members As String = ds.Tables(0).Rows(0).Item(7).ToString

                Dim direct_right_members As String = ds.Tables(0).Rows(0).Item(8).ToString
                Dim userID As String = ds.Tables(0).Rows(0).Item(9).ToString
                Dim referralID As String = ds.Tables(0).Rows(0).Item(11).ToString

                Dim intCountPackage As Integer = ds.Tables(0).Rows(0).Item(10).ToString 

                If intCountPackage <> 0 Then
                 strPackage = clsOdbc.executeScalar_str("SELECT epin_cost FROM mlm_epin_type WHERE id=" & intCountPackage & "")
                Else
                strPackage = "None"                
                End If

                'Dim strBusiness As String = fillBusiness(parent_id)

                Dim left_Carry As String = ""
                Dim right_Carry As String = ""

                dt = clsOdbc.getDataTable("SELECT COALESCE(carry_forward_left,0), COALESCE(carry_forward_right,0) FROM mlm_binary_carry_forward WHERE userid=" & parent_id)
                If (dt.Rows.Count > 0) Then
                    left_Carry = dt.Rows(0).Item(0).ToString
                    right_Carry = dt.Rows(0).Item(1).ToString
                End If

                strString = strString & "<div class=""pop-up""><table><tr><td align=""left"" style=""padding-left:5px;"">UserID:</td><td colspan=""2"" align=""center"">" & userID & "</td></tr><tr>" _
                            & "<td align=""left"" style=""padding-left:5px;"">Name:</td>" _
                            & "<td colspan=""2"" align=""center"">" & UserName & "</td></tr>" _
							& "<tr>" _
							& "<td align=""left"" style=""padding-left:5px;"">Package:</td>" _
                            & "<td colspan=""2"" align=""center"">" & strPackage & "</td></tr>" _
                            & "<tr>" _
                            & "<td align=""left"" >Position: </td>" _
                            & "<td align=""center"" style=""padding-left:10px;width:130px;"">Left</td>" _
                            & "<td align=""right"">Right</td></tr>" _
                            & "<tr>" _
                            & "<td align=""left"" >Total Business: </td>" _
                            & "<td align=""center"" >" & left_business_total & "</td>" _
                            & "<td align=""center"">" & right_business_total & " </td></tr>" _
                            & "<tr>" _
                            & "<td align=""left"" >Current Business: </td>" _
                            & "<td align=""center"" >" & left_business_current & "</td>" _
                            & "<td align=""center"" >" & right_business_current & "</td></tr>" _
                            & "<tr>" _
                            & "<td align=""left"" >Carry Forward: </td>" _
                            & "<td align=""center"" >" & left_Carry & "</td>" _
                            & "<td align=""center"" >" & right_Carry & "</td></tr>" _
                            & "<tr>" _
                            & "<td align=""left"" >Total Members: </td>" _
                            & "<td align=""center"" >" & left_members & "</td>" _
                            & "<td align=""center"" > " & right_members & "</td></tr>" _
                            & "<tr>" _
                            & "<td align=""left"" >Direct Members: </td>" _
                            & "<td align=""center"" >" & direct_left_members & "</td>" _
                            & "<td align=""center"" >" & direct_right_members & "</td></tr></table></div>"
            End If


        Catch ex As Exception

        Finally
            ds.Dispose()
        End Try

        Return strString

    End Function

    Public Function get_Recursive_Popup(ByVal parent_id As Integer, ByVal litPopup As Literal) As String

        Dim strQuery As String = ""
        Dim ds As New Data.DataSet
        Dim strPackage, strPackageAmt As String
        Dim dt As New Data.DataTable

        Try
            strQuery = "SELECT a.UserName,b.left_business,b.right_business,c.left_business,c.right_business,b.l_members,b.r_members,b.direct_left,b.direct_right,d.my_sponsar_id,d.package_id,d.referral_id From mlm_personal_details a LEFT JOIN mlm_progress_count b ON a.userid=b.userid LEFT JOIN mlm_progress_count_current c ON a.userid=c.userid LEFT JOIN mlm_login d ON a.userid=d.userid Where d.node_flag>0 and a.userid=" & parent_id & ""

            ds = clsOdbc.getDataSet(strQuery)

            If (ds.Tables(0).Rows.Count > 0) Then

                Dim UserName As String = ds.Tables(0).Rows(0).Item(0).ToString
                Dim left_business_total As String = ds.Tables(0).Rows(0).Item(1).ToString
                Dim right_business_total As String = ds.Tables(0).Rows(0).Item(2).ToString
                Dim left_business_current As String = ds.Tables(0).Rows(0).Item(3).ToString

                Dim right_business_current As String = ds.Tables(0).Rows(0).Item(4).ToString
                Dim left_members As String = ds.Tables(0).Rows(0).Item(5).ToString

                Dim right_members As String = ds.Tables(0).Rows(0).Item(6).ToString
                Dim direct_left_members As String = ds.Tables(0).Rows(0).Item(7).ToString

                Dim direct_right_members As String = ds.Tables(0).Rows(0).Item(8).ToString
                Dim userID As String = ds.Tables(0).Rows(0).Item(9).ToString
                Dim referralID As String = ds.Tables(0).Rows(0).Item(11).ToString

                Dim intCountPackage As Integer = ds.Tables(0).Rows(0).Item(10).ToString 

                If intCountPackage <> 0 Then
                 strPackage = clsOdbc.executeScalar_str("SELECT epin_cost FROM mlm_epin_type WHERE id=" & intCountPackage & "")
                Else
                strPackage = "None"                
                End If

                Dim left_Carry As String = ""
                Dim right_Carry As String = ""

                dt = clsOdbc.getDataTable("SELECT COALESCE(carry_forward_left,0), COALESCE(carry_forward_right,0) FROM mlm_binary_carry_forward WHERE userid=" & parent_id)
                If (dt.Rows.Count > 0) Then
                    left_Carry = dt.Rows(0).Item(0).ToString
                    right_Carry = dt.Rows(0).Item(1).ToString
                End If


                litPopup.Text = litPopup.Text & "<div id=""" & parent_id & """ style=""display:none;"">" _
                            & "<table class=""hidden-table"">" _
                            & "<tr><td align=""left"" style=""padding-left:5px;"">UserID:</td><td colspan=""2"" align=""center"">" & userID & "</td></tr><tr>" _
                            & "<td align=""left"" style=""padding-left:5px;"">Name:</td>" _
                            & "<td colspan=""2"" align=""center"">" & UserName & "</td></tr>" _
                            & "<tr>" _
							& "<td align=""left"" style=""padding-left:5px;"">Package:</td>" _
                            & "<td colspan=""2"" align=""center"">" & strPackage & "</td></tr>" _
							& "<tr>" _
                            & "<td align=""left"" style=""padding-left:5px;"">Sponsor ID:</td>" _
                            & "<td colspan=""2"" align=""center"">" & referralID & "</td></tr>" _
                            & "<tr>" _
                            & "<td align=""left"" >Position: </td>" _
                            & "<td align=""center"" style=""padding-left:10px;width:130px;"">Left</td>" _
                            & "<td align=""right"">Right</td></tr>" _
                            & "<tr>" _
                            & "<td align=""left"" >Total Business: </td>" _
                            & "<td align=""center"" >" & left_business_total & "</td>" _
                            & "<td align=""center"">" & right_business_total & " </td></tr>" _
                            & "<tr>" _
                            & "<td align=""left"" >Current Business: </td>" _
                            & "<td align=""center"" >" & left_business_current & "</td>" _
                            & "<td align=""center"" >" & right_business_current & "</td></tr>" _
                            & "<tr>" _
                            & "<td align=""left"" >Carry Forward: </td>" _
                            & "<td align=""center"" >" & left_Carry & "</td>" _
                            & "<td align=""center"" >" & right_Carry & "</td></tr>" _
                            & "<tr>" _
                            & "<td align=""left"" >Total Members: </td>" _
                            & "<td align=""center"" >" & left_members & "</td>" _
                            & "<td align=""center"" > " & right_members & "</td></tr>" _
                            & "<tr>" _
                            & "<td align=""left"" >Direct Members: </td>" _
                            & "<td align=""center"" >" & direct_left_members & "</td>" _
                            & "<td align=""center"" >" & direct_right_members & "</td></tr></table></div>"


                Dim ds1 As New Data.DataSet

                Try

                    Dim strQuery1 As String = "SELECT userid From mlm_login Where node_flag>0 and parent_node_id=" & parent_id

                    ds1 = clsOdbc.getDataSet(strQuery1)

                    If (ds1.Tables(0).Rows.Count > 0) Then

                        For i As Integer = 0 To ds1.Tables(0).Rows.Count - 1

                            Dim child_user_id As String = ds1.Tables(0).Rows(i).Item(0).ToString

                            get_Recursive_Popup1(child_user_id, litPopup)

                        Next


                    End If

                Catch ex As Exception

                Finally
                    ds1.Dispose()
                End Try


            End If

        Catch ex As Exception

        Finally
            ds.Dispose()
        End Try

        Return litPopup.Text

    End Function

    Public Function get_Recursive_Popup1(ByVal parent_id As Integer, ByVal litPopup As Literal) As String

        Dim strQuery As String = ""
        Dim ds As New Data.DataSet
        Dim strPackage, strPackageAmt As String
        Dim dt As New Data.DataTable
        Try
            strQuery = "SELECT a.UserName,b.left_business,b.right_business,c.left_business,c.right_business,b.l_members,b.r_members,b.direct_left,b.direct_right,d.my_sponsar_id,d.package_id,d.referral_id From mlm_personal_details a LEFT JOIN mlm_progress_count b ON a.userid=b.userid LEFT JOIN mlm_progress_count_current c ON a.userid=c.userid LEFT JOIN mlm_login d ON a.userid=d.userid Where d.node_flag>0 and a.userid=" & parent_id & ""

            ds = clsOdbc.getDataSet(strQuery)

            If (ds.Tables(0).Rows.Count > 0) Then

                Dim UserName As String = ds.Tables(0).Rows(0).Item(0).ToString
                Dim left_business_total As String = ds.Tables(0).Rows(0).Item(1).ToString
                Dim right_business_total As String = ds.Tables(0).Rows(0).Item(2).ToString
                Dim left_business_current As String = ds.Tables(0).Rows(0).Item(3).ToString

                Dim right_business_current As String = ds.Tables(0).Rows(0).Item(4).ToString
                Dim left_members As String = ds.Tables(0).Rows(0).Item(5).ToString

                Dim right_members As String = ds.Tables(0).Rows(0).Item(6).ToString
                Dim direct_left_members As String = ds.Tables(0).Rows(0).Item(7).ToString

                Dim direct_right_members As String = ds.Tables(0).Rows(0).Item(8).ToString
                Dim userID As String = ds.Tables(0).Rows(0).Item(9).ToString

                Dim referralID As String = ds.Tables(0).Rows(0).Item(11).ToString

                Dim intCountPackage As Integer = ds.Tables(0).Rows(0).Item(10).ToString 

                If intCountPackage <> 0 Then
                 strPackage = clsOdbc.executeScalar_str("SELECT epin_cost FROM mlm_epin_type WHERE id=" & intCountPackage & "")
                Else
                strPackage = "None"                
                End If
				
                Dim left_Carry As String = ""
                Dim right_Carry As String = ""

                dt = clsOdbc.getDataTable("SELECT COALESCE(carry_forward_left,0), COALESCE(carry_forward_right,0) FROM mlm_binary_carry_forward WHERE userid=" & parent_id)
                If (dt.Rows.Count > 0) Then
                    left_Carry = dt.Rows(0).Item(0).ToString
                    right_Carry = dt.Rows(0).Item(1).ToString
                End If


                litPopup.Text = litPopup.Text & "<div id=""" & parent_id & """ style=""display:none;"">" _
                            & "<table class=""hidden-table"">" _
                            & "<tr><td align=""left"" style=""padding-left:5px;"">UserID:</td><td colspan=""2"" align=""center"">" & userID & "</td></tr><tr>" _
                            & "<td align=""left"" style=""padding-left:5px;"">Name:</td>" _
                            & "<td colspan=""2"" align=""center"">" & UserName & "</td></tr>" _
                            & "<tr>" _
							& "<td align=""left"" style=""padding-left:5px;"">Package:</td>" _
                            & "<td colspan=""2"" align=""center"">" & strPackage & "</td></tr>" _
							& "<tr>" _
                            & "<td align=""left"" style=""padding-left:5px;"">Sponsor ID:</td>" _
                            & "<td colspan=""2"" align=""center"">" & referralID & "</td></tr>" _
                            & "<tr>" _
                            & "<td align=""left"" >Position: </td>" _
                            & "<td align=""center"" style=""padding-left:10px;width:130px;"">Left</td>" _
                            & "<td align=""right"">Right</td></tr>" _
                            & "<tr>" _
                            & "<td align=""left"" >Total Business: </td>" _
                            & "<td align=""center"" >" & left_business_total & "</td>" _
                            & "<td align=""center"">" & right_business_total & " </td></tr>" _
                            & "<tr>" _
                            & "<td align=""left"" >Current Business: </td>" _
                            & "<td align=""center"" >" & left_business_current & "</td>" _
                            & "<td align=""center"" >" & right_business_current & "</td></tr>" _
                            & "<tr>" _
                            & "<td align=""left"" >Carry Forward: </td>" _
                            & "<td align=""center"" >" & left_Carry & "</td>" _
                            & "<td align=""center"" >" & right_Carry & "</td></tr>" _
                            & "<tr>" _
                            & "<td align=""left"" >Total Members: </td>" _
                            & "<td align=""center"" >" & left_members & "</td>" _
                            & "<td align=""center"" > " & right_members & "</td></tr>" _
                            & "<tr>" _
                            & "<td align=""left"" >Direct Members: </td>" _
                            & "<td align=""center"" >" & direct_left_members & "</td>" _
                            & "<td align=""center"" >" & direct_right_members & "</td></tr></table></div>"
                Dim ds1 As New Data.DataSet

                Try

                    Dim strQuery1 As String = "SELECT userid From mlm_login Where node_flag>0 and  parent_node_id=" & parent_id

                    ds1 = clsOdbc.getDataSet(strQuery1)

                    If (ds1.Tables(0).Rows.Count > 0) Then

                        For i As Integer = 0 To ds1.Tables(0).Rows.Count - 1

                            Dim child_user_id As String = ds1.Tables(0).Rows(i).Item(0).ToString

                            litPopup.Text = get_Recursive_Popup2(child_user_id, litPopup)

                        Next


                    End If

                Catch ex As Exception

                Finally
                    ds1.Dispose()
                End Try


            End If

        Catch ex As Exception

        Finally
            ds.Dispose()
        End Try

        Return litPopup.Text

    End Function

    Public Function get_Recursive_Popup2(ByVal parent_id As Integer, ByVal litPopup As Literal) As String

        Dim strQuery As String = ""
        Dim ds As New Data.DataSet
        Dim dt As New Data.DataTable
        Dim strPackage, strPackageAmt As String

        Try
            strQuery = "SELECT a.UserName,b.left_business,b.right_business,c.left_business,c.right_business,b.l_members,b.r_members,b.direct_left,b.direct_right,d.my_sponsar_id,d.package_id,d.referral_id From mlm_personal_details a LEFT JOIN mlm_progress_count b ON a.userid=b.userid LEFT JOIN mlm_progress_count_current c ON a.userid=c.userid LEFT JOIN mlm_login d ON a.userid=d.userid Where d.node_flag>0 and a.userid=" & parent_id & ""

            ds = clsOdbc.getDataSet(strQuery)

            If (ds.Tables(0).Rows.Count > 0) Then

                Dim UserName As String = ds.Tables(0).Rows(0).Item(0).ToString
                Dim left_business_total As String = ds.Tables(0).Rows(0).Item(1).ToString
                Dim right_business_total As String = ds.Tables(0).Rows(0).Item(2).ToString
                Dim left_business_current As String = ds.Tables(0).Rows(0).Item(3).ToString

                Dim right_business_current As String = ds.Tables(0).Rows(0).Item(4).ToString
                Dim left_members As String = ds.Tables(0).Rows(0).Item(5).ToString

                Dim right_members As String = ds.Tables(0).Rows(0).Item(6).ToString
                Dim direct_left_members As String = ds.Tables(0).Rows(0).Item(7).ToString

                Dim direct_right_members As String = ds.Tables(0).Rows(0).Item(8).ToString
                Dim userID As String = ds.Tables(0).Rows(0).Item(9).ToString

                Dim referralID As String = ds.Tables(0).Rows(0).Item(11).ToString

                Dim intCountPackage As Integer = ds.Tables(0).Rows(0).Item(10).ToString 

                If intCountPackage <> 0 Then
                 strPackage = clsOdbc.executeScalar_str("SELECT epin_cost FROM mlm_epin_type WHERE id=" & intCountPackage & "")
                Else
                strPackage = "None"                
                End If
				
                Dim left_Carry As String = ""
                Dim right_Carry As String = ""

                dt = clsOdbc.getDataTable("SELECT COALESCE(carry_forward_left,0), COALESCE(carry_forward_right,0) FROM mlm_binary_carry_forward WHERE userid=" & parent_id)
                If (dt.Rows.Count > 0) Then
                    left_Carry = dt.Rows(0).Item(0).ToString
                    right_Carry = dt.Rows(0).Item(1).ToString
                End If


                litPopup.Text = litPopup.Text & "<div id=""" & parent_id & """ style=""display:none;"">" _
                            & "<table class=""hidden-table"">" _
                            & "<tr><td align=""left"" style=""padding-left:5px;"">UserID:</td><td colspan=""2"" align=""center"">" & userID & "</td></tr><tr>" _
                            & "<td align=""left"" style=""padding-left:5px;"">Name:</td>" _
                            & "<td colspan=""2"" align=""center"">" & UserName & "</td></tr>" _
							& "<tr>" _
							& "<td align=""left"" style=""padding-left:5px;"">Package:</td>" _
                            & "<td colspan=""2"" align=""center"">" & strPackage & "</td></tr>" _
                            & "<tr>" _
                            & "<td align=""left"" style=""padding-left:5px;"">Sponsor ID:</td>" _
                            & "<td colspan=""2"" align=""center"">" & referralID & "</td></tr>" _
                            & "<tr>" _
                            & "<td align=""left"" >Position: </td>" _
                            & "<td align=""center"" style=""padding-left:10px;width:130px;"">Left</td>" _
                            & "<td align=""right"">Right</td></tr>" _
                            & "<tr>" _
                            & "<td align=""left"" >Total Business: </td>" _
                            & "<td align=""center"" >" & left_business_total & "</td>" _
                            & "<td align=""center"">" & right_business_total & " </td></tr>" _
                            & "<tr>" _
                            & "<td align=""left"" >Current Business: </td>" _
                            & "<td align=""center"" >" & left_business_current & "</td>" _
                            & "<td align=""center"" >" & right_business_current & "</td></tr>" _
                            & "<tr>" _
                            & "<td align=""left"" >Carry Forward: </td>" _
                            & "<td align=""center"" >" & left_Carry & "</td>" _
                            & "<td align=""center"" >" & right_Carry & "</td></tr>" _
                            & "<tr>" _
                            & "<td align=""left"" >Total Members: </td>" _
                            & "<td align=""center"" >" & left_members & "</td>" _
                            & "<td align=""center"" > " & right_members & "</td></tr>" _
                            & "<tr>" _
                            & "<td align=""left"" >Direct Members: </td>" _
                            & "<td align=""center"" >" & direct_left_members & "</td>" _
                            & "<td align=""center"" >" & direct_right_members & "</td></tr></table></div>"

                Dim ds1 As New Data.DataSet

                Try

                    Dim strQuery1 As String = "SELECT userid From mlm_login Where node_flag>0 and parent_node_id=" & parent_id

                    ds1 = clsOdbc.getDataSet(strQuery1)

                    If (ds1.Tables(0).Rows.Count > 0) Then

                        For i As Integer = 0 To ds1.Tables(0).Rows.Count - 1

                            Dim child_user_id As String = ds1.Tables(0).Rows(i).Item(0).ToString

                            litPopup.Text = get_Recursive_Popup3(child_user_id, litPopup)

                        Next


                    End If

                Catch ex As Exception

                Finally
                    ds1.Dispose()
                End Try


            End If

        Catch ex As Exception

        Finally
            ds.Dispose()
        End Try

        Return litPopup.Text

    End Function

    Public Function get_Recursive_Popup3(ByVal parent_id As Integer, ByVal litPopup As Literal) As String

        Dim strQuery As String = ""
        Dim ds As New Data.DataSet
        Dim dt As New Data.DataTable
        Dim strPackage, strPackageAmt As String

        Try
            strQuery = "SELECT a.UserName,b.left_business,b.right_business,c.left_business,c.right_business,b.l_members,b.r_members,b.direct_left,b.direct_right,d.my_sponsar_id,d.package_id,d.referral_id From mlm_personal_details a LEFT JOIN mlm_progress_count b ON a.userid=b.userid LEFT JOIN mlm_progress_count_current c ON a.userid=c.userid LEFT JOIN mlm_login d ON a.userid=d.userid Where d.node_flag>0 and a.userid=" & parent_id & ""

            ds = clsOdbc.getDataSet(strQuery)

            If (ds.Tables(0).Rows.Count > 0) Then

                Dim UserName As String = ds.Tables(0).Rows(0).Item(0).ToString
                Dim left_business_total As String = ds.Tables(0).Rows(0).Item(1).ToString
                Dim right_business_total As String = ds.Tables(0).Rows(0).Item(2).ToString
                Dim left_business_current As String = ds.Tables(0).Rows(0).Item(3).ToString

                Dim right_business_current As String = ds.Tables(0).Rows(0).Item(4).ToString
                Dim left_members As String = ds.Tables(0).Rows(0).Item(5).ToString

                Dim right_members As String = ds.Tables(0).Rows(0).Item(6).ToString
                Dim direct_left_members As String = ds.Tables(0).Rows(0).Item(7).ToString

                Dim direct_right_members As String = ds.Tables(0).Rows(0).Item(8).ToString
                Dim userID As String = ds.Tables(0).Rows(0).Item(9).ToString

                Dim referralID As String = ds.Tables(0).Rows(0).Item(11).ToString

                Dim intCountPackage As Integer = ds.Tables(0).Rows(0).Item(10).ToString 

                If intCountPackage <> 0 Then
                 strPackage = clsOdbc.executeScalar_str("SELECT epin_cost FROM mlm_epin_type WHERE id=" & intCountPackage & "")
                Else
                strPackage = "None"                
                End If
				
                Dim left_Carry As String = ""
                Dim right_Carry As String = ""

                dt = clsOdbc.getDataTable("SELECT COALESCE(carry_forward_left,0), COALESCE(carry_forward_right,0) FROM mlm_binary_carry_forward WHERE userid=" & parent_id)
                If (dt.Rows.Count > 0) Then
                    left_Carry = dt.Rows(0).Item(0).ToString
                    right_Carry = dt.Rows(0).Item(1).ToString
                End If


                litPopup.Text = litPopup.Text & "<div id=""" & parent_id & """ style=""display:none;"">" _
                            & "<table class=""hidden-table"">" _
                            & "<tr><td align=""left"" style=""padding-left:5px;"">UserID:</td><td colspan=""2"" align=""center"">" & userID & "</td></tr><tr>" _
                            & "<td align=""left"" style=""padding-left:5px;"">Name:</td>" _
                            & "<td colspan=""2"" align=""center"">" & UserName & "</td></tr>" _
							& "<tr>" _
							& "<td align=""left"" style=""padding-left:5px;"">Package:</td>" _
                            & "<td colspan=""2"" align=""center"">" & strPackage & "</td></tr>" _
                            & "<tr>" _
                            & "<td align=""left"" style=""padding-left:5px;"">Sponsor ID:</td>" _
                            & "<td colspan=""2"" align=""center"">" & referralID & "</td></tr>" _
                            & "<tr>" _
                            & "<td align=""left"" >Position: </td>" _
                            & "<td align=""center"" style=""padding-left:10px;width:130px;"">Left</td>" _
                            & "<td align=""right"">Right</td></tr>" _
                            & "<tr>" _
                            & "<td align=""left"" >Total Business: </td>" _
                            & "<td align=""center"" >" & left_business_total & "</td>" _
                            & "<td align=""center"">" & right_business_total & " </td></tr>" _
                            & "<tr>" _
                            & "<td align=""left"" >Current Business: </td>" _
                            & "<td align=""center"" >" & left_business_current & "</td>" _
                            & "<td align=""center"" >" & right_business_current & "</td></tr>" _
                            & "<tr>" _
                            & "<td align=""left"" >Carry Forward: </td>" _
                            & "<td align=""center"" >" & left_Carry & "</td>" _
                            & "<td align=""center"" >" & right_Carry & "</td></tr>" _
                            & "<tr>" _
                            & "<td align=""left"" >Total Members: </td>" _
                            & "<td align=""center"" >" & left_members & "</td>" _
                            & "<td align=""center"" > " & right_members & "</td></tr>" _
                            & "<tr>" _
                            & "<td align=""left"" >Direct Members: </td>" _
                            & "<td align=""center"" >" & direct_left_members & "</td>" _
                            & "<td align=""center"" >" & direct_right_members & "</td></tr></table></div>"
            End If

        Catch ex As Exception

        Finally
            ds.Dispose()
        End Try

        Return litPopup.Text

    End Function


    '**** Count Total Nodes ******
    Public Function Count_nodes(ByVal userid As Integer) As Integer

        Try

            Dim temp_count As Integer = clsOdbc.executeScalar_int("SELECT COUNT(*) FROM mlm_login WHERE parent_node_id =" & userid)

            If temp_count = 0 Then
                Return 1
            Else
                If temp_count = 2 Then
                    Dim tempL1_id As Integer = clsOdbc.executeScalar_int("SELECT userid  FROM mlm_login WHERE node_flag = 'L' AND parent_node_id = " & userid)
                    Dim tempR1_id As Integer = clsOdbc.executeScalar_int("SELECT userid  FROM mlm_login WHERE node_flag = 'R' AND parent_node_id =" & userid)
                    Return (Count_nodes(tempL1_id) + Count_nodes(tempR1_id) + 1)
                Else
                    Dim temp_countL As Integer = clsOdbc.executeScalar_int("SELECT COUNT(*) FROM mlm_login WHERE node_flag = 'L' AND parent_node_id =" & userid)
                    If temp_countL = 1 Then
                        Dim tempL1_id As Integer = clsOdbc.executeScalar_int("SELECT userid FROM mlm_login WHERE node_flag = 'L' AND parent_node_id = " & userid)
                        Return (Count_nodes(tempL1_id) + 1)
                    Else
                        Dim tempR1_id As Integer = clsOdbc.executeScalar_int("SELECT userid FROM mlm_login WHERE node_flag = 'R' AND parent_node_id =" & userid)
                        Return (Count_nodes(tempR1_id) + 1)
                    End If
                End If
            End If

        Catch ex As Exception

        End Try


    End Function

    '**** Count Left Nodes ******
    Public Function Count_left(ByVal id As Integer) As Integer

        Try
            Dim intDirect_nodeL As Integer = clsOdbc.executeScalar_int("SELECT COUNT(*) FROM mlm_login WHERE node_flag = 'L' AND parent_node_id =" & id)

            Dim Lcount As Integer = 0


            If intDirect_nodeL = 1 Then
                Dim tempL_id As Integer = clsOdbc.executeScalar_int("SELECT userid FROM mlm_login WHERE node_flag = 'L' AND parent_node_id =" & id)

                Lcount = Lcount + Count_nodes(tempL_id)
            End If

            Return Lcount
        Catch ex As Exception

        End Try


    End Function

    '**** Count Right Nodes ******
    Public Function Count_right(ByVal id As Integer) As Integer

        Try

            Dim intDirect_nodeR As Integer = clsOdbc.executeScalar_int("SELECT COUNT(*) FROM mlm_login WHERE node_flag = 'R' AND parent_node_id =" & id)

            Dim Rcount As Integer = 0

            If intDirect_nodeR = 1 Then
                Dim tempR_id As Integer = clsOdbc.executeScalar_int("SELECT userid FROM mlm_login WHERE node_flag = 'R' AND parent_node_id = " & id)

                Rcount = Rcount + Count_nodes(tempR_id)
            End If
            Return Rcount

        Catch ex As Exception

        End Try


    End Function

    Public Function FillAllDetails(ByVal intUserID As Integer) As ArrayList


        Dim strQuery As String = "SELECT r_members,l_members FROM mlm_progress_count WHERE userid=" & intUserID
        Dim colCount As Integer = 2
        Dim list As New ArrayList()
        Dim ds As New DataSet()
        Try
            ds = clsOdbc.getDataSet(strQuery)
            If ds.Tables(0).Rows.Count > 0 Then

                For i As Integer = 0 To colCount - 1
                    list.Add(ds.Tables(0).Rows(0)(i).ToString())
                Next
            Else
                list.Add(0)
                list.Add(0)


            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally

            ds.Dispose()
        End Try

        Return list


    End Function

    Public Function FillAllDetailsCurrent(ByVal intUserID As Integer) As ArrayList


        Dim strQuery As String = "SELECT r_members,l_members FROM mlm_progress_count_current WHERE userid=" & intUserID
        Dim colCount As Integer = 2
        Dim list As New ArrayList()
        Dim ds As New DataSet()
        Try
            ds = clsOdbc.getDataSet(strQuery)
            If ds.Tables(0).Rows.Count > 0 Then

                For i As Integer = 0 To colCount - 1
                    list.Add(ds.Tables(0).Rows(0)(i).ToString())
                Next
            Else
                list.Add(0)
                list.Add(0)

            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally

            ds.Dispose()
        End Try

        Return list

    End Function

    Protected Function fillBusiness(ByVal intUserID As Integer) As String
        Dim str As String = ""
        Dim strQuery As String = "SELECT  left_invest ,right_invest FROM mlm_progress_count WHERE userid=" & intUserID & ""
        Dim ds As New DataSet
        ds = clsOdbc.getDataSet(strQuery)
        If (ds.Tables(0).Rows.Count > 0) Then
            str = str & "<tr><td align=""left"">Left Business: </td><td>" & ds.Tables(0).Rows(0).Item(0).ToString() & "</td></tr>"
            str = str & "<tr><td align=""left"">Right Business: </td><td>" & ds.Tables(0).Rows(0).Item(1).ToString() & "</td></tr>"
            Return str
        End If
        Return str
    End Function

    Public Function fillColor(ByVal intStatus1 As Integer) As String
        Dim strClass1 As String = ""
        'Dim intStatus1 As Integer = clsOdbc.executeScalar_int("SELECT status3 FROM mlm_login WHERE userid=" & intUser_ID & "")
        Select Case intStatus1
            Case 0
                strClass1 = "green"
                Exit Select
            Case 1
                strClass1 = "blue"
                Exit Select
            Case 2
                strClass1 = "light_green"
                Exit Select
            Case 3
                strClass1 = "yellow"
                Exit Select
            Case 4
                strClass1 = "dark_pink"
                Exit Select
            Case 5
                strClass1 = "dark_yellow"
                Exit Select
            Case 6
                strClass1 = "violet"
                Exit Select
            Case 7
                strClass1 = "brown"
                Exit Select
        End Select
        'If intStatus1 = 1 Then
        '    strClass1 = "green"
        'Else
        '    strClass1 = "Red"

        'End If
        Return strClass1
    End Function

    Public Function fillStarStatus(ByVal intStatus1 As Integer) As String
        Dim strClass1 As String = ""
        'Dim intStatus1 As Integer = clsOdbc.executeScalar_int("SELECT status3 FROM mlm_login WHERE userid=" & intUser_ID & "")
        Select Case intStatus1
            Case 0
                strClass1 = "Basic"
                Exit Select
            Case 1
                strClass1 = "Star1"
                Exit Select
            Case 2
                strClass1 = "Star2"
                Exit Select
            Case 3
                strClass1 = "Star3"
                Exit Select
            Case 4
                strClass1 = "Star4"
                Exit Select
            Case 5
                strClass1 = "Star5"
                Exit Select
            Case 6
                strClass1 = "Star6"
                Exit Select
            Case 7
                strClass1 = "Star7"
                Exit Select
        End Select
        'If intStatus1 = 1 Then
        '    strClass1 = "green"
        'Else
        '    strClass1 = "Red"

        'End If
        Return strClass1
    End Function
End Class
