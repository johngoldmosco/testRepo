Imports Microsoft.VisualBasic

Public Class Staff

    Dim clsOdbc As New ODBC

    Public Function AddStaffLevel(ByVal strLevelName As String) As String

        Dim strQuery As String = "INSERT INTO staff_level(staff_level_name) VALUES ('" & strLevelName & "')"

        Try
            clsOdbc.executeNonQuery(strQuery)
            Return "Staff Level Successfully Added!"
        Catch ex As Exception
            Return ex.Message.ToString
        End Try


    End Function

    Public Function AddPayrollYear(ByVal strPayRollYear As String) As String

        Dim strQuery As String = "INSERT INTO payroll_year(py_name) VALUES ('" & strPayRollYear & "')"

        Try
            clsOdbc.executeNonQuery(strQuery)
            Return "PayRoll Year Successfully Added!"
        Catch ex As Exception
            Return ex.Message.ToString
        End Try


    End Function

    Public Function AddLevelSalary(ByVal strLevel As String, ByVal strSalary As String) As String

        Dim intCount As Integer = clsOdbc.executeScalar_int("SELECT Count(*) From staff_level_salary Where Active=1 and staff_level_id=" & strLevel)

        If intCount > 0 Then
            Return "Salary for this Level is already Added!"
        Else
            Dim strQuery As String = "INSERT INTO staff_level_salary(staff_level_id,staff_level_salary) VALUES ('" & strLevel & "','" & strSalary & "')"

            Try
                clsOdbc.executeNonQuery(strQuery)
                Return "Level Salary Successfully Added!"
            Catch ex As Exception
                Return ex.Message.ToString
            End Try
        End If

    End Function

    Public Function AddStaff(ByVal strStaffID As String, ByVal strStaffLevel As String, ByVal strStaffPost As String, ByVal strStaffGender As String, ByVal strDate As String, ByVal strStaffReligion As String, ByVal strStaffBloodGroup As String, ByVal strStaffHandiCaped As String, ByVal strStaffFullName As String, ByVal strStaffFatherName As String, ByVal strStaffMobile As String, ByVal strStaffEmail As String, ByVal strStaffCountry As String, ByVal strStaffState As String, ByVal strStaffAddress As String) As Boolean

        Dim intCount As Integer = clsOdbc.executeScalar_int("SELECT Count(*) From staff_information Where staff_reg_id=" & strStaffID)

        If intCount > 0 Then
            Return False
        Else
            Dim strQuery As String = "INSERT INTO staff_information(staff_reg_id,staff_level_id,staff_post,Staff_Gender,Staff_DOB,Staff_Relegion,Staff_Blood_Group,Staff_Handicapped,Staff_Full_Name,Staff_Father_Name,Staff_Mobile,Staff_Email,Staff_Country,Staff_State,Staff_Address) VALUES ('" & strStaffID & "','" & strStaffLevel & "','" & strStaffPost & "','" & strStaffGender & "','" & strDate & "','" & strStaffReligion & "','" & strStaffBloodGroup & "','" & strStaffHandiCaped & "','" & strStaffFullName & "','" & strStaffFatherName & "','" & strStaffMobile & "','" & strStaffEmail & "','" & strStaffCountry & "','" & strStaffState & "','" & strStaffAddress & "')"

            Try
                clsOdbc.executeNonQuery(strQuery)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End If
        

    End Function

    Public Function AddStaffEducation(ByVal strStaffRegID As String, ByVal strStaffStudy1 As String, ByVal strStaffInstitute1 As String, ByVal strStaffUniverCity1 As String, ByVal strStaffParcent1 As String, ByVal strStaffPassYear1 As String, ByVal strStaffStudy2 As String, ByVal strStaffInstitute2 As String, ByVal strStaffUniverCity2 As String, ByVal strStaffParcent2 As String, ByVal strStaffPassYear2 As String, ByVal strStaffStudy3 As String, ByVal strStaffInstitute3 As String, ByVal strStaffUniverCity3 As String, ByVal strStaffParcent3 As String, ByVal strStaffPassYear3 As String) As String

        Dim strStaffID As String = clsOdbc.executeScalar_str("SELECT Staff_ID From staff_information Where staff_reg_id=" & strStaffRegID)

        Dim strQuery1 As String = "INSERT INTO staff_education(Staff_ID,Staff_Study,Staff_Insti,Staff_Univ,Staff_Perc,Staff_Pass) VALUES ('" & strStaffID & "','" & strStaffStudy1 & "','" & strStaffInstitute1 & "','" & strStaffUniverCity1 & "','" & strStaffParcent1 & "','" & strStaffPassYear1 & "')"
        Dim strQuery2 As String = "INSERT INTO staff_education(Staff_ID,Staff_Study,Staff_Insti,Staff_Univ,Staff_Perc,Staff_Pass) VALUES ('" & strStaffID & "','" & strStaffStudy2 & "','" & strStaffInstitute2 & "','" & strStaffUniverCity2 & "','" & strStaffParcent2 & "','" & strStaffPassYear2 & "')"
        Dim strQuery3 As String = "INSERT INTO staff_education(Staff_ID,Staff_Study,Staff_Insti,Staff_Univ,Staff_Perc,Staff_Pass) VALUES ('" & strStaffID & "','" & strStaffStudy3 & "','" & strStaffInstitute3 & "','" & strStaffUniverCity3 & "','" & strStaffParcent3 & "','" & strStaffPassYear3 & "')"

        Try
            clsOdbc.executeNonQuery(strQuery1)
            clsOdbc.executeNonQuery(strQuery2)
            clsOdbc.executeNonQuery(strQuery3)

            Return "Staff Details Successfully Added!"
        Catch ex As Exception
            Return ex.Message.ToString
        End Try

    End Function

End Class
