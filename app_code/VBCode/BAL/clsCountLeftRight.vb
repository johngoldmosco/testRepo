Imports Microsoft.VisualBasic

Public Class clsCountLeftRight

    Dim clsOdbc As New ODBC
    Dim clsOther As New ClassOther
    Public Function Count_nodes(ByVal userid As Integer) As Integer

        Dim temp_count As Integer = clsOdbc.executeScalar_int("SELECT COUNT(*) FROM mlm_binary_tree WHERE parent_node_id =" & userid)

        If temp_count = 0 Then
            Return 1
        Else
            If temp_count = 4 Then
                Dim tempL1_id As Integer = clsOdbc.executeScalar_int("SELECT userid  FROM mlm_binary_tree WHERE node_flag = 'L1' AND parent_node_id = " & userid)
                Dim tempL2_id As Integer = clsOdbc.executeScalar_int("SELECT userid  FROM mlm_binary_tree WHERE node_flag = 'L2' AND parent_node_id =" & userid)
                Dim tempR1_id As Integer = clsOdbc.executeScalar_int("SELECT userid  FROM mlm_binary_tree WHERE node_flag = 'R1' AND parent_node_id = " & userid)
                Dim tempR2_id As Integer = clsOdbc.executeScalar_int("SELECT userid  FROM mlm_binary_tree WHERE node_flag = 'R2' AND parent_node_id =" & userid)
                Return (Count_nodes(tempL1_id) + Count_nodes(tempL2_id) + Count_nodes(tempR1_id) + Count_nodes(tempR2_id) + 1)
            Else
                Dim temp_countL As Integer = clsOdbc.executeScalar_int("SELECT COUNT(*) FROM mlm_binary_tree WHERE node_flag = 'L' AND parent_node_id =" & userid)
                If temp_countL = 1 Then
                    Dim tempL1_id As Integer = clsOdbc.executeScalar_int("SELECT userid FROM mlm_binary_tree WHERE node_flag = 'L' AND parent_node_id = " & userid)
                    Return (Count_nodes(tempL1_id) + 1)
                Else
                    Dim tempR1_id As Integer = clsOdbc.executeScalar_int("SELECT userid FROM mlm_binary_tree WHERE node_flag = 'R' AND parent_node_id =" & userid)
                    Return (Count_nodes(tempR1_id) + 1)
                End If
            End If
        End If

    End Function

    Public Function Count_left(ByVal id As Integer) As Integer

        Dim intDirect_nodeL As Integer = clsOdbc.executeScalar_int("SELECT COUNT(*) FROM mlm_binary_tree WHERE node_flag = 'L' AND parent_node_id =" & id)

        Dim Lcount As Integer = 0


        If intDirect_nodeL = 1 Then
            Dim tempL_id As Integer = clsOdbc.executeScalar_int("SELECT userid FROM mlm_binary_tree WHERE node_flag = 'L' AND parent_node_id =" & id)

            Lcount = Lcount + Count_nodes(tempL_id)
        End If

        Return Lcount

    End Function

    Public Function Count_L1(ByVal id As Integer) As Integer

        Dim intDirect_nodeL As Integer = clsOdbc.executeScalar_int("SELECT COUNT(*) FROM mlm_binary_tree WHERE node_flag = 'L1' AND parent_node_id =" & id)

        Dim Lcount As Integer = 0


        If intDirect_nodeL = 1 Then
            Dim tempL_id As Integer = clsOdbc.executeScalar_int("SELECT userid FROM mlm_binary_tree WHERE node_flag = 'L1' AND parent_node_id =" & id)

            Lcount = Lcount + Count_nodes(tempL_id)
        End If

        Return Lcount

    End Function
    Public Function Count_L2(ByVal id As Integer) As Integer

        Dim intDirect_nodeL As Integer = clsOdbc.executeScalar_int("SELECT COUNT(*) FROM mlm_binary_tree WHERE node_flag = 'L2' AND parent_node_id =" & id)

        Dim Lcount As Integer = 0


        If intDirect_nodeL = 1 Then
            Dim tempL_id As Integer = clsOdbc.executeScalar_int("SELECT userid FROM mlm_binary_tree WHERE node_flag = 'L2' AND parent_node_id =" & id)

            Lcount = Lcount + Count_nodes(tempL_id)
        End If

        Return Lcount

    End Function


    Public Function Count_right(ByVal id As Integer) As Integer
        Dim intDirect_nodeR As Integer = clsOdbc.executeScalar_int("SELECT COUNT(*) FROM mlm_binary_tree WHERE node_flag = 'R' AND parent_node_id =" & id)

        Dim Rcount As Integer = 0

        If intDirect_nodeR = 1 Then
            Dim tempR_id As Integer = clsOdbc.executeScalar_int("SELECT userid FROM mlm_binary_tree WHERE node_flag = 'R' AND parent_node_id = " & id)

            Rcount = Rcount + Count_nodes(tempR_id)
        End If
        Return Rcount
    End Function
    Public Function Count_R1(ByVal id As Integer) As Integer

        Dim intDirect_nodeL As Integer = clsOdbc.executeScalar_int("SELECT COUNT(*) FROM mlm_binary_tree WHERE node_flag = 'R1' AND parent_node_id =" & id)

        Dim Lcount As Integer = 0


        If intDirect_nodeL = 1 Then
            Dim tempL_id As Integer = clsOdbc.executeScalar_int("SELECT userid FROM mlm_binary_tree WHERE node_flag = 'R1' AND parent_node_id =" & id)

            Lcount = Lcount + Count_nodes(tempL_id)
        End If

        Return Lcount

    End Function

    Public Function Count_R2(ByVal id As Integer) As Integer

        Dim intDirect_nodeL As Integer = clsOdbc.executeScalar_int("SELECT COUNT(*) FROM mlm_binary_tree WHERE node_flag = 'R2' AND parent_node_id =" & id)

        Dim Lcount As Integer = 0


        If intDirect_nodeL = 1 Then
            Dim tempL_id As Integer = clsOdbc.executeScalar_int("SELECT userid FROM mlm_binary_tree WHERE node_flag = 'R2' AND parent_node_id =" & id)

            Lcount = Lcount + Count_nodes(tempL_id)
        End If

        Return Lcount

    End Function

End Class
