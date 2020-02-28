Imports Microsoft.VisualBasic
Imports System.Data

Public Class clsMLM
    Dim clsOdbc As New ODBC

    '*** Return Direct Referral User ID ***
    Public Function get_Direct_Referral_UID(ByVal intUserID As Integer) As Integer

        Dim result_get_Direct_Referral_UID As Integer = clsOdbc.executeScalar_int("SELECT a.userid From mlm_referral a,mlm_referral b Where b.userid=" & intUserID & " and b.referral_id=a.my_sponsar_id")

        Return result_get_Direct_Referral_UID

    End Function

    Public Function GetUserName(ByVal strSponsorID As String) As String
        Dim intCount As Integer = clsOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_referral WHERE my_sponsar_id='" & strSponsorID & "'")
        Dim strUserName As String
        If intCount > 0 Then
            strUserName = clsOdbc.executeScalar_str("SELECT l.UserName FROM mlm_login l, mlm_referral r WHERE l.userid = r.userid and r.my_sponsar_id='" & strSponsorID & "'")
        Else
            strUserName = "Member does not exist !"
        End If

        Return strUserName
    End Function

    'Add Node in Binary(DFS)
    Private Function AddBinaryTreeNode(ByVal intParentID As String, ByVal strPos As String, ByVal intUserId As Integer) As Boolean

        Try
            Dim intNodeCount As Integer = clsOdbc.executeScalar_int("SELECT Count(1) From mlm_binary_tree Where parent_node_id=" & intParentID & " and node_flag='" & strPos & "'")
            If intNodeCount = 0 Then

                clsOdbc.executeNonQuery("INSERT INTO mlm_binary_tree (parent_node_id,userid,node_flag) VALUES (" & intParentID & "," & intUserId & ",'" & strPos & "')")
                Dim mySponsarID As String = clsOdbc.executeScalar_str("SELECT my_sponsar_id From mlm_referral Where userid =" & intParentID)

                clsOdbc.executeNonQuery("UPDATE mlm_referral SET my_sponsar_sys_id='" & mySponsarID & "' Where userid=" & intUserId)
                Return True
                Exit Function

            Else

                Dim intChildNode As Integer = clsOdbc.executeScalar_int("SELECT userid From mlm_binary_tree Where parent_node_id=" & intParentID & " and node_flag='" & strPos & "'")
                AddBinaryTreeNode(intChildNode, strPos, intUserId)

            End If

        Catch ex As Exception
            CommonMessages.ShowAlertMessage(ex.Message.ToString)
        End Try

    End Function

    'ilevel starts with zero and intUserid= id to be added and plist starts with grandparent
    Public Sub AddNodeBFS(ByVal intUserID As Integer, ByVal plist As ArrayList, ByVal ilevel As Integer)

        Dim list As New ArrayList()
        list = list_level_users(plist, ilevel + 1, 1)
        If list.Count = (Math.Pow(2, ilevel + 1)) Then
            AddNodeBFS(intUserID, list, ilevel + 1)
        Else
            If plist.Count <> 0 Then
                For i As Integer = 0 To plist.Count - 1
                    Dim intNodeCount As Integer = clsOdbc.executeScalar_int("SELECT Count(1) From mlm_binary_tree Where parent_node_id=" & plist(i).ToString())
                    If intNodeCount < 2 Then
                        Dim strPos As String
                        If intNodeCount = 0 Then
                            strPos = "L"
                        Else
                            strPos = "R"
                        End If

                        clsOdbc.executeNonQuery("INSERT INTO mlm_binary_tree (parent_node_id,userid,node_flag) VALUES (" & plist(i).ToString() & "," & intUserID & ",'" & strPos & "')")

                        Dim mySponsarID As String = clsOdbc.executeScalar_str("SELECT my_sponsar_id From mlm_referral Where userid =" & plist(i).ToString())

                        clsOdbc.executeNonQuery("UPDATE mlm_referral SET my_sponsar_sys_id='" & mySponsarID & "' Where userid=" & intUserID)

                        Exit For
                    End If
                Next

            End If

        End If


    End Sub


    ' Get list of child userid in level plan
    Public Function list_child_level(ByVal tlist As ArrayList, ByVal plist As ArrayList) As ArrayList
        If tlist.Count <> 0 Then
            Dim clist As New ArrayList()
            Dim tcount As Integer = tlist.Count

            For i As Integer = 0 To tcount - 1
                Dim ds As New Data.DataSet
                ds = clsOdbc.getDataSet("SELECT userid FROM `mlm_referral` WHERE `referral_id`=(SELECT `my_sponsar_id` FROM mlm_referral WHERE userid=" & tlist(i).ToString() & ")")
                If ds.Tables(0).Rows.Count > 0 Then
                    Dim rowsCount As Integer = ds.Tables(0).Rows.Count

                    For j As Integer = 0 To rowsCount - 1
                        clist.Add(ds.Tables(0).Rows(j)(0).ToString())
                        plist.Add(ds.Tables(0).Rows(j)(0).ToString())
                    Next
                End If
                ds.Dispose()
            Next
            Return list_child_level(clist, plist)
        Else
            Return plist
        End If
    End Function

    Public Function list_child_left(ByVal id As Integer) As ArrayList
        Dim Llist As New ArrayList()

        Dim intDirect_nodeL As Integer = clsOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_login WHERE node_flag = '1' AND parent_node_id =" & id)

        Llist.Add(0)

        If intDirect_nodeL = 1 Then
            Dim tempL_id As Integer = clsOdbc.executeScalar_int("SELECT userid FROM mlm_login WHERE node_flag = '1' AND parent_node_id =" & id)
            Llist.Add(tempL_id)

            Llist = list_child_matrix(Llist, Llist)
        End If
        Return Llist
    End Function
    Public Function list_child_center(ByVal id As Integer) As ArrayList
        Dim Llist As New ArrayList()

        Dim intDirect_nodeL As Integer = clsOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_login WHERE node_flag = '2' AND parent_node_id =" & id)

        Llist.Add(0)

        If intDirect_nodeL = 1 Then
            Dim tempL_id As Integer = clsOdbc.executeScalar_int("SELECT userid FROM mlm_login WHERE node_flag = '2' AND parent_node_id =" & id)
            Llist.Add(tempL_id)

            Llist = list_child_matrix(Llist, Llist)
        End If
        Return Llist
    End Function

    Public Function list_child_right(ByVal id As Integer) As ArrayList
        Dim Rlist As New ArrayList()
        Dim intDirect_nodeR As Integer = clsOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_login WHERE node_flag = '2' AND parent_node_id =" & id)

        Rlist.Add(0)

        If intDirect_nodeR = 1 Then
            Dim tempR_id As Integer = clsOdbc.executeScalar_int("SELECT userid FROM mlm_login WHERE node_flag = '2' AND parent_node_id = " & id)
            Rlist.Add(tempR_id)

            Rlist = list_child_matrix(Rlist, Rlist)
        End If
        Return Rlist
    End Function


    ' Get list of child userid in Matrix plan plan
    Public Function list_child_matrix(ByVal tlist As ArrayList, ByVal plist As ArrayList) As ArrayList
        If tlist.Count <> 0 Then
            Dim clist As New ArrayList()
            Dim tcount As Integer = tlist.Count

            For i As Integer = 0 To tcount - 1
                Dim ds As New Data.DataSet
                ds = clsOdbc.getDataSet("SELECT userid FROM `mlm_login` WHERE `my_sponsar_sys_id`=(SELECT `my_sponsar_id` FROM mlm_login WHERE userid=" & tlist(i).ToString() & ")")
                If ds.Tables(0).Rows.Count > 0 Then
                    Dim rowsCount As Integer = ds.Tables(0).Rows.Count

                    For j As Integer = 0 To rowsCount - 1
                        clist.Add(ds.Tables(0).Rows(j)(0).ToString())
                        plist.Add(ds.Tables(0).Rows(j)(0).ToString())
                    Next
                End If
                ds.Dispose()
            Next
            Return list_child_matrix(clist, plist)
        Else
            Return plist
        End If
    End Function

    'fpcount is the final level of parent and ipcount initialize with 1

    Public Function ParentList(ByVal userid As Integer, ByVal plist As ArrayList, ByVal fpcount As Integer, ByVal ipcount As Integer) As ArrayList
        If fpcount <> ipcount Then
            Dim Countpid As Integer = clsOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_binary_tree WHERE userid =" & userid)
            If Countpid = 1 Then
                Dim pid As Integer = clsOdbc.executeScalar_int("SELECT parent_node_id FROM mlm_binary_tree WHERE userid =" & userid)
                plist.Add(pid)
                Return ParentList(pid, plist, fpcount, ipcount + 1)
            Else
                Return plist
            End If
        Else
            Return plist
        End If

    End Function



    ' to get all the parent of the user
    Public Function ParentList(ByVal userid As Integer, ByVal plist As ArrayList) As ArrayList
        Dim Countpid As Integer = clsOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_binary_tree WHERE userid =" & userid)
        If Countpid = 1 Then
            Dim pid As Integer = clsOdbc.executeScalar_int("SELECT parent_node_id FROM mlm_binary_tree WHERE userid =" & userid)
            plist.Add(pid)
            Return ParentList(pid, plist)
        Else
            Return plist
        End If

    End Function

    'flevel is the particular of which u need retreive members and ilevel initializes with 1
    Public Function list_level_users(ByVal plist As ArrayList, ByVal flevel As Integer, ByVal ilevel As Integer) As ArrayList
        If plist IsNot Nothing Then
            Dim clist As New ArrayList()
            For i As Integer = 0 To plist.Count - 1
                Dim ds As New DataSet
                ds = clsOdbc.getDataSet("SELECT userid FROM `mlm_referral` WHERE `referral_id`=(SELECT `my_sponsar_id` FROM mlm_referral WHERE userid=" & plist(i).ToString() & ")")
                If ds.Tables(0).Rows.Count > 0 Then
                    Dim rowsCount As Integer = ds.Tables(0).Rows.Count

                    For j As Integer = 0 To rowsCount - 1
                        clist.Add(ds.Tables(0).Rows(j)(0).ToString())
                    Next
                End If
                ds.Dispose()
            Next
            If flevel = ilevel Then
                Return clist
            End If
            If flevel < ilevel Then
                clist = Nothing
                Return clist
            Else
                Return list_level_users(clist, flevel, ilevel + 1)

            End If
        Else
            Return plist
        End If
    End Function

    '**** Count Total Nodes Binary ******
    Public Function Count_nodes(ByVal userid As Integer) As Integer

        Try

            Dim temp_count As Integer = clsOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_binary_tree WHERE parent_node_id =" & userid)

            If temp_count = 0 Then
                Return 1
            Else
                If temp_count = 2 Then
                    Dim tempL1_id As Integer = clsOdbc.executeScalar_int("SELECT userid  FROM mlm_binary_tree WHERE node_flag = 'L' AND parent_node_id = " & userid)
                    Dim tempR1_id As Integer = clsOdbc.executeScalar_int("SELECT userid  FROM mlm_binary_tree WHERE node_flag = 'R' AND parent_node_id =" & userid)
                    Return (Count_nodes(tempL1_id) + Count_nodes(tempR1_id) + 1)
                Else
                    Dim temp_countL As Integer = clsOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_binary_tree WHERE node_flag = 'L' AND parent_node_id =" & userid)
                    If temp_countL = 1 Then
                        Dim tempL1_id As Integer = clsOdbc.executeScalar_int("SELECT userid FROM mlm_binary_tree WHERE node_flag = 'L' AND parent_node_id = " & userid)
                        Return (Count_nodes(tempL1_id) + 1)
                    Else
                        Dim tempR1_id As Integer = clsOdbc.executeScalar_int("SELECT userid FROM mlm_binary_tree WHERE node_flag = 'R' AND parent_node_id =" & userid)
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
            Dim intDirect_nodeL As Integer = clsOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_binary_tree WHERE node_flag = 'L' AND parent_node_id =" & id)

            Dim Lcount As Integer = 0


            If intDirect_nodeL = 1 Then
                Dim tempL_id As Integer = clsOdbc.executeScalar_int("SELECT userid FROM mlm_binary_tree WHERE node_flag = 'L' AND parent_node_id =" & id)

                Lcount = Lcount + Count_nodes(tempL_id)
            End If

            Return Lcount
        Catch ex As Exception

        End Try


    End Function

    '**** Count Right Nodes ******
    Public Function Count_right(ByVal id As Integer) As Integer

        Try

            Dim intDirect_nodeR As Integer = clsOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_binary_tree WHERE node_flag = 'R' AND parent_node_id =" & id)

            Dim Rcount As Integer = 0

            If intDirect_nodeR = 1 Then
                Dim tempR_id As Integer = clsOdbc.executeScalar_int("SELECT userid FROM mlm_binary_tree WHERE node_flag = 'R' AND parent_node_id = " & id)

                Rcount = Rcount + Count_nodes(tempR_id)
            End If
            Return Rcount

        Catch ex As Exception

        End Try


    End Function

    'To determine the level completion of user

    Public Sub updateLevel(ByVal pid As Integer, ByVal flevel As Integer, ByVal doj As String)

        Dim Clevel As Integer = clsOdbc.executeScalar_int("SELECT MAX(level) FROM `mlm_binary_level` WHERE userid=" & pid)
        If Clevel + 1 = flevel Then
            Dim fnodes As Integer = Count_nodes_level(pid, 0, flevel)
            If fnodes = (Math.Pow(2, flevel)) Then
                clsOdbc.executeNonQuery("INSERT INTO mlm_binary_level (userid, level, date_completion) VALUES (" & pid & "," & flevel & ",'" & doj & "')")
                Dim Countgid As Integer = clsOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_binary_tree WHERE userid =" & pid)
                If Countgid = 1 Then
                    Dim gid As Integer = clsOdbc.executeScalar_int("SELECT parent_node_id FROM mlm_binary_tree WHERE userid =" & pid)
                    updateLevel(gid, flevel + 1, doj)
                End If

            End If
        End If
    End Sub

    ' Count till each level is complete
    Public Function Count_nodes_level(ByVal userid As Integer, ByVal ilevel As Integer, ByVal flevel As Integer) As Integer


        If ilevel + 1 = flevel Then
            Dim fcount As Integer = clsOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_binary_tree WHERE parent_node_id =" & userid)
            Return fcount
        Else

            Dim temp_count As Integer = clsOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_binary_tree WHERE parent_node_id =" & userid)
            If temp_count = 2 Then
                Dim tempL1_id As Integer = clsOdbc.executeScalar_int("SELECT userid FROM mlm_binary_tree WHERE node_flag = 'L' AND parent_node_id = " & userid)
                Dim tempR1_id As Integer = clsOdbc.executeScalar_int("SELECT userid FROM mlm_binary_tree WHERE node_flag = 'R' AND parent_node_id =" & userid)
                Return (Count_nodes_level(tempL1_id, ilevel + 1, flevel) + Count_nodes_level(tempR1_id, ilevel + 1, flevel))
            Else
                Return 0
            End If
        End If


    End Function

    'flevel is the final level of parent and ilevel initialize with 1

    Public Function Count_nodes_level_ternary(ByVal userid As Integer, ByVal ilevel As Integer, ByVal flevel As Integer) As Integer


        If ilevel = flevel Then
            Dim fcount As Integer = clsOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_binary_tree WHERE parent_node_id =" & userid)
            Return fcount
        Else

            Dim temp_count As Integer = clsOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_binary_tree WHERE parent_node_id =" & userid)
            If temp_count = 3 Then
                Dim tempL1_id As Integer = clsOdbc.executeScalar_int("SELECT userid FROM mlm_binary_tree WHERE node_flag = 'L' AND parent_node_id = " & userid)
                Dim tempC1_id As Integer = clsOdbc.executeScalar_int("SELECT userid FROM mlm_binary_tree WHERE node_flag = 'C' AND parent_node_id =" & userid)

                Dim tempR1_id As Integer = clsOdbc.executeScalar_int("SELECT userid FROM mlm_binary_tree WHERE node_flag = 'R' AND parent_node_id =" & userid)
                Return (Count_nodes_level_ternary(tempL1_id, ilevel + 1, flevel) + Count_nodes_level_ternary(tempC1_id, ilevel + 1, flevel) + Count_nodes_level_ternary(tempR1_id, ilevel + 1, flevel))
            Else
                Return 0
            End If
        End If


    End Function

    Public Sub updateLevel_ternary(ByVal pid As Integer, ByVal flevel As Integer)

        Dim Clevel As Integer = clsOdbc.executeScalar_int("SELECT MAX(level) FROM `mlm_binary_level` WHERE userid=" & pid)
        If Clevel + 1 = flevel Then
            Dim fnodes As Integer = Count_nodes_level_ternary(pid, 0, flevel)
            If fnodes = (Math.Pow(3, flevel)) Then
                clsOdbc.executeNonQuery("INSERT INTO mlm_binary_level (userid, level) VALUES (" & pid & "," & flevel & ")")
                Dim Countgid As Integer = clsOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_binary_tree WHERE userid =" & pid)
                If Countgid = 1 Then
                    Dim gid As Integer = clsOdbc.executeScalar_int("SELECT parent_node_id FROM mlm_binary_tree WHERE userid =" & pid)
                    updateLevel_ternary(gid, flevel + 1)
                End If
            End If
        End If
    End Sub

    Public Function Count_nodes_ternary(ByVal userid As Integer) As Integer
        Dim temp_count As Integer = clsOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_binary_tree WHERE parent_node_id =" & userid)

        If temp_count = 0 Then
            Return 1
        Else
            If temp_count = 3 Then
                Dim tempL1_id As Integer = clsOdbc.executeScalar_int("SELECT userid FROM mlm_binary_tree WHERE node_flag = 'L' AND parent_node_id = " & userid)
                Dim tempC1_id As Integer = clsOdbc.executeScalar_int("SELECT userid FROM mlm_binary_tree WHERE node_flag = 'C' AND parent_node_id = " & userid)
                Dim tempR1_id As Integer = clsOdbc.executeScalar_int("SELECT userid FROM mlm_binary_tree WHERE node_flag = 'R' AND parent_node_id =" & userid)
                Return (Count_nodes_ternary(tempL1_id) + Count_nodes_ternary(tempR1_id) + Count_nodes_ternary(tempC1_id) + 1)
            Else
                If temp_count = 2 Then
                    Dim temp_countL As Integer = clsOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_binary_tree WHERE node_flag = 'L' AND parent_node_id =" & userid)
                    If temp_countL = 1 Then
                        Dim temp_countC As Integer = clsOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_binary_tree WHERE node_flag = 'C' AND parent_node_id =" & userid)
                        If temp_countC = 1 Then
                            Dim tempL1_id As Integer = clsOdbc.executeScalar_int("SELECT userid FROM mlm_binary_tree WHERE node_flag = 'L' AND parent_node_id = " & userid)
                            Dim tempC1_id As Integer = clsOdbc.executeScalar_int("SELECT userid FROM mlm_binary_tree WHERE node_flag = 'C' AND parent_node_id = " & userid)
                            Return (Count_nodes_ternary(tempL1_id) + Count_nodes_ternary(tempC1_id) + 1)
                        Else
                            Dim tempL1_id As Integer = clsOdbc.executeScalar_int("SELECT userid FROM mlm_binary_tree WHERE node_flag = 'L' AND parent_node_id = " & userid)
                            Dim tempR1_id As Integer = clsOdbc.executeScalar_int("SELECT userid FROM mlm_binary_tree WHERE node_flag = 'R' AND parent_node_id =" & userid)
                            Return (Count_nodes_ternary(tempL1_id) + Count_nodes_ternary(tempR1_id) + 1)
                        End If
                    Else
                        Dim tempC1_id As Integer = clsOdbc.executeScalar_int("SELECT userid FROM mlm_binary_tree WHERE node_flag = 'C' AND parent_node_id = " & userid)
                        Dim tempR1_id As Integer = clsOdbc.executeScalar_int("SELECT userid FROM mlm_binary_tree WHERE node_flag = 'R' AND parent_node_id =" & userid)
                        Return (Count_nodes_ternary(tempR1_id) + Count_nodes_ternary(tempC1_id) + 1)
                    End If
                Else
                    Dim temp_countL As Integer = clsOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_binary_tree WHERE node_flag = 'L' AND parent_node_id =" & userid)
                    If temp_countL = 1 Then
                        Dim tempL1_id As Integer = clsOdbc.executeScalar_int("SELECT userid FROM mlm_binary_tree WHERE node_flag = 'L' AND parent_node_id = " & userid)
                        Return (Count_nodes_ternary(tempL1_id) + 1)
                    End If
                    Dim temp_countC As Integer = clsOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_binary_tree WHERE node_flag = 'C' AND parent_node_id =" & userid)
                    If temp_countC = 1 Then
                        Dim tempC1_id As Integer = clsOdbc.executeScalar_int("SELECT userid FROM mlm_binary_tree WHERE node_flag = 'C' AND parent_node_id = " & userid)
                        Return (Count_nodes_ternary(tempC1_id) + 1)
                    End If
                    Dim temp_countR As Integer = clsOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_binary_tree WHERE node_flag = 'R' AND parent_node_id =" & userid)
                    If temp_countR = 1 Then
                        Dim tempR1_id As Integer = clsOdbc.executeScalar_int("SELECT userid FROM mlm_binary_tree WHERE node_flag = 'R' AND parent_node_id =" & userid)
                        Return (Count_nodes_ternary(tempR1_id) + 1)
                    End If
                End If
            End If
        End If

    End Function

End Class
