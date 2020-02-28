Imports Microsoft.VisualBasic
Imports System.Web.UI
Imports System.Data

Public Class clsRegister
	Dim Ec As New EncryptTest.Encryption
    Dim clsOdbc As New ODBC
    Dim clsOther As New ClassOther
    Dim objCommunication As New clsCommunication
	Dim cstemp As New clsMailTemplate

    Public Sub AddTempData(ByVal strSponsar As String, ByVal strParent As String, ByVal strEpin As String)
        Try
            clsOdbc.executeNonQuery("Insert Into mlm_sponsor_detail(referral_id,my_sponsar_sys_id,Epin) values('" & strSponsar & "','" & strParent & "','" & strEpin & "')")
            Dim intID As Integer = clsOdbc.executeScalar_int("Select MAX(id) From mlm_sponsor_detail where Epin = '" & strEpin & "'")
            HttpContext.Current.Response.Redirect("register2.aspx?tempId=" & intID & "")
        Catch ex As Exception

        End Try

    End Sub
 Public Function AddNewRegister(ByVal txtReferralId As String, ByVal txtFName As String, ByVal txtMobilecode As String, ByVal txtMobileNo As String, ByVal txtEmail As String, ByVal intUserTypeId As Integer, ByVal intPosition As Integer, ByVal country_id As Integer, ByVal skypename As String) As Integer
        Dim intID As Integer
        Dim ds As New DataSet
        Try
            Dim MysponsarId As String = String.Empty
            Dim txtPwd As String = String.Empty
            Dim txtTransPwd As String = String.Empty
            Dim strlink As String = String.Empty

            clsOdbc.executeNonQuery("call AddRegister('" & txtReferralId & "','" & txtFName & "','" & txtMobilecode & "','" & txtMobileNo & "','" & txtEmail & "'," & intUserTypeId & "," & intPosition & "," & country_id & ", '" & skypename & "')")
            intID = clsOdbc.executeScalar_int("Select MAX(userid) From mlm_personal_details where mobile_number = '" & txtMobileNo & "' and username = '" & txtFName & "'")
            If intID > 0 Then

                Try
                    ds = clsOdbc.getDataSet("Select my_sponsar_id,password,trans_pwd From mlm_login where userid = " & intID & "")
                    If ds.Tables(0).Rows.Count <> 0 Then
                        MysponsarId = ds.Tables(0).Rows(0)(0).ToString()
                        txtPwd = ds.Tables(0).Rows(0)(1).ToString()
                        txtTransPwd = ds.Tables(0).Rows(0)(2).ToString()
                    End If

                Catch ex As Exception

                Finally
                    ds.Dispose()

                End Try


            End If
            Try
                ' Dim mailMessage1 As String = "<table><tr><td colspan=2>Congratulations ! you have successfully register to Global Success 365 with:</td></tr><tr><td>User ID</td><td>" & MysponsarId & "</td></tr><tr><td>password:</td><td>" & txtPwd & "</td></tr><tr><td>Mobile No.:</td><td>" & txtMobileNo & "</td></tr><tr><td colspan=2>Please login at www.globalsuccess365.com </td></tr></table>"
                'Dim mailSubject As String = "Email Verification For Global Success 365"
                ' Dim mailFrom As String = "admin@globalsuccess365.com"
                'Dim mailFromName As String = "admin"
                'strlink = "https://www.globalsuccess365.com/EmailVarification.aspx?" & Ec.EncryptQueryString(String.Format("UID={0}", intID.ToString()), "VbFM45Lt")
                ' Dim mailMessage As String = cstemp.getEmailVerificationMessage(txtFName, strlink)

                 'objCommunication.Email_API_for_Trsansaction_Email("2407150640", "mnFUaikmG54546cgaD4r", 1, mailSubject, mailFrom, mailFromName, txtEmail,
				 'txtFName, mailMessage, "www.globalsuccess365.com", "")
                ' objCommunication.SendTestMail(txtEmail, mailFrom, mailSubject, mailMessage, intID)
            Catch ex As Exception

            End Try
            Try

                'objCommunication.sendSMSRegister(txtMobileNo, MysponsarId, txtPwd)
            Catch ex As Exception

            End Try
            Return intID

        Catch ex As Exception

        End Try
        Return intID
    End Function


    Public Function AddOtherUser(ByVal txtUserID As String, ByVal txtName As String, ByVal txtMobile As String, ByVal txtEmail As String, ByVal txtCity As String, ByVal strPwd As String, ByVal intUserType As String) As Integer
        Try

            clsOdbc.executeNonQuery("call AddOtherUser('" & txtUserID & "','" & txtName & "','" & txtMobile & "','" & txtEmail & "','" & txtCity & "','" & strPwd & "','" & intUserType & "')")
            Dim intID As Integer = clsOdbc.executeScalar_int("Select MAX(userid) From mlm_login where my_sponsar_id = '" & txtUserID & "'")
            If intID > 0 Then
                'CommonMessages.ShowAlertMessage_Reload("New Member successfully Registered! UserId : " & MysponsarId & " And Password : " & strPwd & "", "register1.aspx")
            End If
            Try
                Dim mailMessage As String = "<table><tr><td colspan=2>Congratulations ! you have successfully register to Versatile Soft Educare Co. with:</td></tr><tr><td>User ID</td><td>" & txtUserID & "</td></tr><tr><td>password:</td><td>" & strPwd & "</td></tr><tr><td>Mobile No.:</td><td>" & txtMobile & "</td></tr><tr><td colspan=2>Please login at  http://www.versatileregistration.com</td></tr></table>"
                Dim mailSubject As String = "Registration confirmed For Versatile Soft Educare Co."
                Dim mailFrom As String = "admin@versatileregistration.com"

                'objCommunication.SendTestMail(txtEmail, mailFrom, mailSubject, mailMessage, intID)
            Catch ex As Exception

            End Try
            Try
                Dim smsMessage As String = "Congratulations ! You are successfully registered to Cash Your Mobile With UserId : " & txtUserID & " And Password : " & strPwd & " http://portal.cashurmobile.com.asp1-23.ord1-1.websitetestlink.com/portal/index.aspx"

            Catch ex As Exception

            End Try
            Return intID

        Catch ex As Exception

        End Try
    End Function

    '**** Generate Random String *****
    Private Function GenrateRandomString() As String

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

    '**** Generate Random String *****
    Private Function GenrateRandomPrefixString() As String

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





    Public Sub GetUserName(ByVal txtReferralId As TextBox, ByVal lblReferralName As Label)
        Dim inrCount As Integer = clsOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_login WHERE my_sponsar_id='" + txtReferralId.Text & "'")
        If inrCount <> 0 Then
            lblReferralName.Text = clsOdbc.executeScalar_str("SELECT username FROM mlm_personal_details a inner join mlm_login b On a.userid = b.userid WHERE b.my_sponsar_id='" + txtReferralId.Text & "'")
        Else
            lblReferralName.Text = "Member With this User Id Does Not exist"
            txtReferralId.Text = String.Empty
        End If
    End Sub

     Public Function GetEpin(ByVal strEpin As String, ByVal intUserID As Integer) As Integer
        Dim intCount As Integer = clsOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_epin WHERE epin ='" & strEpin & "' and userid=" & intUserID & "")
        If intCount > 0 Then
            Dim flag As Integer = clsOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_epin WHERE status = 1 and epin ='" & strEpin & "'and userid=" & intUserID & " ")
            If flag > 0 Then
                Return 1
            Else
                Return 0
            End If
        Else
            Return 2
        End If
    End Function
	
	Public Function GetEpinRenew(ByVal strEpin As String, ByVal pinType As String) As Integer
        Dim intCount As Integer = clsOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_epin WHERE epin ='" & strEpin & "'")
        If intCount > 0 Then
            Dim flag As Integer = clsOdbc.executeScalar_int("SELECT COUNT(1)FROM mlm_epin WHERE status = 1 and epin ='" & strEpin & "' and epin_type=2 ")
            If flag > 0 Then
                Return 1
            Else
                Return 0
            End If
        Else
            Return 2
        End If
    End Function

End Class
