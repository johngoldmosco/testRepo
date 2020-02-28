Imports Microsoft.VisualBasic

Public Class clsEpin
    Dim clsOdbc As New ODBC
    Dim objWallet As New clsWallet


    '***** UNIQUE EPIN for Each Member *****
    Public Function RandomEPIN(ByVal intPINType As Integer) As String

        Dim dtcurDateTime As DateTime = getTime("India Standard Time")
        Dim strcurDateTime As String = dtcurDateTime.ToString("yyyy-MM-dd HH:mm:ss")
        Dim strcurDate As String = dtcurDateTime.ToString("yyyy-MM-dd")

        Dim strEPIN As String = GenrateRandomEPIN()

        Dim intCount As Integer = clsOdbc.executeScalar_int("SELECT Count(1) From mlm_epin Where epin='" & strEPIN & "'  and epin_type=" & intPINType & " ")

        If intCount = 0 Then
            clsOdbc.executeNonQuery("INSERT INTO mlm_epin (epin,epin_type,transfer_date,created_date) VALUES ('" & strEPIN & "'," & intPINType & ",'" & strcurDateTime & "','" & strcurDateTime & "')")
        Else
            RandomEPIN(intPINType)
        End If

        Return strEPIN

    End Function

   

   '**** Fill State Details in DropDownList ****
    Public Sub FillEPINType(ByVal ddlDropDownList As DropDownList)

        Dim strQuery As String
        Dim ds As New Data.DataSet

        strQuery = " SELECT id,cast(concat(pin_type,' ','(',epin_cost ,')') as CHAR)as pin_type From mlm_epin_type Order By id ASC"
        ddlDropDownList.Items.Clear()

        Try
            ds = clsOdbc.getDataSet(strQuery)

            If ds.Tables(0).Rows.Count > 0 Then

                ddlDropDownList.DataSource = ds
                ddlDropDownList.DataTextField = "pin_type"
                ddlDropDownList.DataValueField = "id"
                ddlDropDownList.DataBind()
            End If
            ddlDropDownList.Items.Insert(0, "Select")
            ddlDropDownList.SelectedIndex = 0
        Catch ex As Exception

        Finally
            ds.Dispose()
        End Try

    End Sub


    Public Function GenrateRandomEPIN() As String

        Dim allowedChars As String = ""
        allowedChars = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,"
        allowedChars += "1,2,3,4,5,6,7,8,9,0"
        Dim sep As Char() = {","c}
        Dim arr As String() = allowedChars.Split(sep)
        Dim passwordString As String = ""
        Dim temp As String = ""
        Dim rand As New Random()
        For i As Integer = 0 To Convert.ToInt32(7) - 1
            temp = arr(rand.[Next](0, arr.Length))
            passwordString += temp
        Next

        Return passwordString

    End Function

    '**** Fill State Details in DropDownList ****
    Public Sub FillState(ByVal ddlDropDownList As DropDownList)

        Dim strQuery As String
        Dim ds As New Data.DataSet

        strQuery = "SELECT state_id,state_name From mlm_state WHERE Active=1 and country_id=1 Order By state_name ASC"
        ddlDropDownList.Items.Clear()

        Try
            ds = clsOdbc.getDataSet(strQuery)

            If ds.Tables(0).Rows.Count > 0 Then

                ddlDropDownList.DataSource = ds
                ddlDropDownList.DataTextField = "state_name"
                ddlDropDownList.DataValueField = "state_id"
                ddlDropDownList.DataBind()
            End If
            ddlDropDownList.Items.Insert(0, "Select")
            ddlDropDownList.SelectedIndex = 0
        Catch ex As Exception

        Finally
            ds.Dispose()
        End Try

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

End Class
