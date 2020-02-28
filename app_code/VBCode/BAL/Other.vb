Imports Microsoft.VisualBasic
Imports System.Net.Mail
Imports System.ComponentModel
Imports System.Threading
Imports System.Web.Services

Public Class ClassOther

    Dim clsOdbc As New ODBC

    Public Sub FillGridViewData(ByVal strQuery As String, ByVal gvGridView As GridView)

        Dim ds As New Data.DataSet

        Try

            ds = clsOdbc.getDataSet(strQuery)
            If (ds.Tables(0).Rows.Count > 0) Then
                gvGridView.DataSource = ds
                gvGridView.DataBind()
            Else
                'CommonMessages.ShowAlertMessage("Sorry, No Records Found!")
                gvGridView.DataSource = Nothing
                gvGridView.DataBind()
            End If
        Catch ex As Exception

        Finally
            ds.Dispose()
        End Try

    End Sub

    '**** Sending Test Mail ****
    Public Function SendTestMail(ByVal strTo As String, ByVal strFrom As String, ByVal strSubject As String, ByVal strMessage As String) As Boolean


        Dim strQuery, strHost, strPort, strUserName, strPasswrod As String
        Dim ds As New Data.DataSet

        Dim intMailCount As Integer = clsOdbc.executeScalar_int("SELECT Count(*) From mlm_send_mail_log Where DATE(mail_time) = DATE(Now())")

        ' ***** Per Day Maximum 50 E-mail can be sent. *****
        If intMailCount < 50 Then

            Try
                strQuery = "SELECT smtp_host,smtp_port,smtp_username,smtp_password From mgk_smtp_setting"
                ds = clsOdbc.getDataSet(strQuery)
                If (ds.Tables(0).Rows.Count > 0) Then

                    strHost = ds.Tables(0).Rows(0).Item(0).ToString
                    strPort = ds.Tables(0).Rows(0).Item(1).ToString
                    strUserName = ds.Tables(0).Rows(0).Item(2).ToString
                    strPasswrod = ds.Tables(0).Rows(0).Item(3).ToString

                End If
            Catch ex As Exception

            Finally
                ds.Dispose()
            End Try



            Dim Mailmsg As New MailMessage()

            Dim client As New System.Net.Mail.SmtpClient(strHost, strPort)
            Dim netwrkCrd As New System.Net.NetworkCredential()
            netwrkCrd.UserName = strUserName
            netwrkCrd.Password = strPasswrod
            client.Credentials = netwrkCrd

            Try
                Mailmsg.[To].Clear()
                Mailmsg.From = New MailAddress(strFrom)
                Mailmsg.[To].Add(New MailAddress(strTo))
                Mailmsg.Subject = strSubject
                Mailmsg.IsBodyHtml = True
                Mailmsg.Body = strMessage.ToString()
                client.Send(Mailmsg)

                clsOdbc.executeNonQuery("INSERT INTO mlm_send_mail_log (mail_to,mail_subject,mail_message,mail_status) VALUES ('" & strTo & "','" & strSubject & "','" & strMessage & "',1)")

                Return True

            Catch ex As Exception

                clsOdbc.executeNonQuery("INSERT INTO mlm_send_mail_log (mail_to,mail_subject,mail_message,mail_status) VALUES ('" & strTo & "','" & strSubject & "','" & strMessage & "',0)")

                Return False

            End Try

        End If

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


    '**** Fill Date in DropDownList ****
    Public Sub FillDate(ByVal ddlDropDownList As DropDownList)

        ddlDropDownList.Items.Clear()
        ddlDropDownList.Items.Insert(0, "Date")

        For iDate As Integer = 1 To 31
            ddlDropDownList.Items.Insert(iDate, iDate.ToString())
        Next

    End Sub

	 '**** Fill Hours in DropDownList ****
    Public Sub FillHours(ByVal ddlDropDownList As DropDownList)

        ddlDropDownList.Items.Clear()
        ddlDropDownList.Items.Insert(0, "Hours")

        For iDate As Integer = 1 To 24
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

    '**** Fill Item Catgeory Details in DropDownList ****
    Public Sub FillItemCategory(ByVal ddlDropDownList As DropDownList)

        Dim strQuery As String
        Dim ds As New Data.DataSet

        strQuery = "SELECT inv_cat_id,inv_cat_name From inv_category WHERE Active=1 Order By inv_cat_name ASC"
        ddlDropDownList.Items.Clear()

        Try
            ds = clsOdbc.getDataSet(strQuery)

            If ds.Tables(0).Rows.Count > 0 Then

                ddlDropDownList.DataSource = ds
                ddlDropDownList.DataTextField = "inv_cat_name"
                ddlDropDownList.DataValueField = "inv_cat_id"
                ddlDropDownList.DataBind()
            End If
            ddlDropDownList.Items.Insert(0, "Select")
            ddlDropDownList.SelectedIndex = 0
        Catch ex As Exception

        Finally
            ds.Dispose()
        End Try

    End Sub

    '**** Fill Vendor Details in DropDownList ****
    Public Sub FillVendorName(ByVal ddlDropDownList As DropDownList)

        Dim strQuery As String
        Dim ds As New Data.DataSet

        strQuery = "SELECT vendor_id,vendor_name From inv_vendors WHERE Active=1 Order By vendor_name ASC"
        ddlDropDownList.Items.Clear()

        Try
            ds = clsOdbc.getDataSet(strQuery)

            If ds.Tables(0).Rows.Count > 0 Then

                ddlDropDownList.DataSource = ds
                ddlDropDownList.DataTextField = "vendor_name"
                ddlDropDownList.DataValueField = "vendor_id"
                ddlDropDownList.DataBind()
            End If
            ddlDropDownList.Items.Insert(0, "Select")
            ddlDropDownList.SelectedIndex = 0
        Catch ex As Exception

        Finally
            ds.Dispose()
        End Try

    End Sub

    '**** Fill Item Stock Location Details in DropDownList ****
    Public Sub FillStockLocation(ByVal ddlDropDownList As DropDownList)

        Dim strQuery As String
        Dim ds As New Data.DataSet

        strQuery = "SELECT inv_location_id,inv_location_name From inv_item_location WHERE Active=1 Order By inv_location_name ASC"
        ddlDropDownList.Items.Clear()

        Try
            ds = clsOdbc.getDataSet(strQuery)

            If ds.Tables(0).Rows.Count > 0 Then

                ddlDropDownList.DataSource = ds
                ddlDropDownList.DataTextField = "inv_location_name"
                ddlDropDownList.DataValueField = "inv_location_id"
                ddlDropDownList.DataBind()
            End If
            ddlDropDownList.Items.Insert(0, "Select")
            ddlDropDownList.SelectedIndex = 0
        Catch ex As Exception

        Finally
            ds.Dispose()
        End Try

    End Sub

    '**** Fill Customer Details in DropDownList ****
    Public Sub FillCustomer(ByVal ddlDropDownList As DropDownList)

        Dim strQuery As String
        Dim ds As New Data.DataSet

        strQuery = "SELECT cust_id,cust_company_name From inv_customers WHERE Active=1 Order By cust_company_name ASC"
        ddlDropDownList.Items.Clear()

        Try
            ds = clsOdbc.getDataSet(strQuery)

            If ds.Tables(0).Rows.Count > 0 Then

                ddlDropDownList.DataSource = ds
                ddlDropDownList.DataTextField = "cust_company_name"
                ddlDropDownList.DataValueField = "cust_id"
                ddlDropDownList.DataBind()
            End If
            ddlDropDownList.Items.Insert(0, "Select")
            ddlDropDownList.SelectedIndex = 0
        Catch ex As Exception

        Finally
            ds.Dispose()
        End Try

    End Sub


    Public Sub FillGridViewDropDown(ByVal ctrlDropDown As Control, ByVal strQuery As String, ByVal strTextField As String, ByVal strValueField As String, ByVal strSelectedValue As String)

        If (ctrlDropDown Is Nothing) Then

        Else

            Dim dd As DropDownList = ctrlDropDown

            dd.Items.Clear()

            Dim ds As Data.DataSet = New Data.DataSet

            Try
                ds = clsOdbc.getDataSet(strQuery)
                If ds.Tables(0).Rows.Count > 0 Then
                    dd.DataSource = ds
                    dd.DataTextField = strTextField
                    dd.DataValueField = strValueField
                    dd.DataBind()
                End If
            Catch ex As Exception

            Finally
                ds.Dispose()
            End Try

        End If

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

    '**** Get Total Joined Members ****
    Public Function GetTotalJoinedMembers() As Integer

        Return clsOdbc.executeScalar_str("SELECT Count(userid) From mlm_login Where LoginTypeId=2") - 1

    End Function

    '**** Get Total Joined Members ****
    Public Function GetTotalJoiningFees() As Integer

        Dim dblJoiningFees As Double = clsOdbc.executeScalar_dbl("SELECT joining_fees From mlm_joining_fees")

        Dim dblTotalJoiningFees As Double = (clsOdbc.executeScalar_str("SELECT Count(userid) From mlm_login Where LoginTypeId=2") - 1) * dblJoiningFees

        Return dblTotalJoiningFees

    End Function

    '**** Get Total Joined Members ****
    Public Function GetTotalAmountPaid() As Double

        Return clsOdbc.executeScalar_str("SELECT SUM(recieved_amount) From mlm_my_balance")

    End Function

    '**** Get Total Joined Members ****
    Public Function GetMyTotalBalance(ByVal intUserId As Integer) As Double

        Return clsOdbc.executeScalar_str("SELECT balance_amount From mlm_my_balance Where userid=" & intUserId)

    End Function

    '**** Get Total Joined Members ****
    Public Function GetMyTotalAmountRecieved(ByVal intUserId As Integer) As Double

        Return clsOdbc.executeScalar_str("SELECT recieved_amount From mlm_my_balance Where userid=" & intUserId)

    End Function

    '**** Get Total Joined Members ****
    Public Function MyDownlineMembers(ByVal intUserId As Integer) As Integer

        Dim strSystemID As String = clsOdbc.executeScalar_str("SELECT my_sponsar_id From mlm_referral Where userid=" & intUserId)

        Return clsOdbc.executeScalar_str("SELECT Count(*) From mlm_referral Where referral_id='" & strSystemID & "'")

    End Function

    '**** Get Total Cart Price ****
    Public Function GetTotalCartPrice() As Decimal
        Return clsOdbc.executeScalar_str("SELECT SUM(total_price) AS Total_Price FROM shopping_cart")
    End Function

    '**** Get Total Cart Item Quantity ****
    Public Function GetTotalCartQuant() As Integer
        Return clsOdbc.executeScalar_str("SELECT SUM(item_quant) AS Item_Count FROM shopping_cart")
    End Function

    '**** Get TAX Amount ****
    Public Function GetTAXAmount() As Decimal
        Return clsOdbc.executeScalar_str("SELECT tax_amount From tax_status WHERE Active=1 and tax_status_id=1")
    End Function

    '**** Get VAT Amount ****
    Public Function GetVATAmount() As Decimal
        Return clsOdbc.executeScalar_str("SELECT vat_amount From vat_status WHERE Active=1 and vat_status_id=1")
    End Function

    '**** Get Company Logo Path ****
    Public Function GetCompanyLogo() As String
        Return clsOdbc.executeScalar_str("SELECT company_logo From personal_setting")
    End Function

    '**** Get Company Name ****
    Public Function GetCompanyName() As String
        Return clsOdbc.executeScalar_str("SELECT company_name From personal_setting")
    End Function

    '**** Get Company Address ****
    Public Function GetCompanyAddress() As String
        Return clsOdbc.executeScalar_str("SELECT company_address From personal_setting")
    End Function

    '**** Get Company Contact Number ****
    Public Function GetCompanyContact() As String
        Return clsOdbc.executeScalar_str("SELECT company_contact From personal_setting")
    End Function

    '**** Get Company Email Address ****
    Public Function GetCompanyEmail() As String
        Return clsOdbc.executeScalar_str("SELECT company_email From personal_setting")
    End Function

    '**** Get Total Collected Amount ****
    Public Function GetTotalCollectedAmount() As Decimal
        Return clsOdbc.executeScalar_str("SELECT SUM(amount_paid) AS Amount_Paid FROM invoice_main Where Active=1")
    End Function

    '**** Get Total Pending Amount ****
    Public Function GetTotalPendingAmount() As Decimal
        Return clsOdbc.executeScalar_str("SELECT SUM(amount_pending) AS Amount_Pending FROM invoice_main Where Active=1")
    End Function

    '**** Get Total Invoices ****
    Public Function GetTotalInvoice() As Decimal
        Return clsOdbc.executeScalar_str("SELECT Count(invoice_id) From invoice_main Where Active=1")
    End Function

    '**** Get Total DISTINCT Active Clients ****
    Public Function GetTotalActiveClients() As Decimal
        Return clsOdbc.executeScalar_str("SELECT Count(DISTINCT cust_id) From invoice_main Where Active=1")
    End Function

    '**** Get Total Number of Pending Invoices ****
    Public Function GetTotalPendingInvoices() As Decimal
        Return clsOdbc.executeScalar_str("SELECT Count(cust_id) From invoice_main Where status='Pending' and Active=1")
    End Function

    '**** Get Total Sales ****
    Public Function GetTotalSales() As Decimal

        Dim decValue = 0
        Try
            If IsDBNull(clsOdbc.executeScalar_str("SELECT SUM(total_price) From invoice_main Where Active=1")) Then
                Return decValue
            Else
                Return clsOdbc.executeScalar_str("SELECT SUM(total_price) From invoice_main Where Active=1")
            End If
        Catch ex As Exception
            Return decValue
        End Try

    End Function

    '**** Get Total Sale Items ****
    Public Function GetTotalSaleItems() As Decimal

        Dim decValue = 0.0
        Try
            If IsDBNull(clsOdbc.executeScalar_int("SELECT SUM(item_quantity) From invoice_item_details")) Then
                Return decValue
            Else
                Return clsOdbc.executeScalar_int("SELECT SUM(item_quantity) From invoice_item_details")
            End If
        Catch ex As Exception
            Return decValue
        End Try
        

    End Function

    '**** Get Total Purchase Items ****
    Public Function GetTotalPurchaseItems() As Decimal

        Dim decValue = 0
        Try
            If IsDBNull(clsOdbc.executeScalar_str("SELECT SUM(item_purchased) From inv_items Where Active=1")) Then
                Return String.Empty
            Else
                Return clsOdbc.executeScalar_str("SELECT SUM(item_purchased) From inv_items Where Active=1")
            End If
        Catch ex As Exception
            Return decValue
        End Try

    End Function

    '**** Get Total Vendors ****
    Public Function GetTotalVendors() As Decimal
        Return clsOdbc.executeScalar_str("SELECT Count(vendor_id) From inv_vendors Where Active=1")
    End Function

    '**** Get Total Sales ****
    Public Function GetTotalExpense() As Decimal

        Try
            Return clsOdbc.executeScalar_str("SELECT SUM(item_total_price) From inv_items Where Active=1")
        Catch ex As Exception
            Return "0"
        End Try

    End Function

    Public Function GetTotalInvoicePrice(ByVal strInvId As String) As String
        Try
            Return clsOdbc.executeScalar_str("SELECT total_price From invoice_main WHERE Active=1 and invoice_id=" & strInvId)
        Catch ex As Exception
            Return "0"
        End Try

    End Function

    '**** Get Total Manual Cart Price ****
    Public Function GetTotalManualCartPrice() As Decimal
        Try
            Return clsOdbc.executeScalar_str("SELECT SUM(item_total_price) AS Total_Price FROM manual_shopping_cart")
        Catch ex As Exception
            Return "0"
        End Try

    End Function

    '**** Get Total Manual Cart Item Quantity ****
    Public Function GetTotalManualCartQuant() As Integer
        Try
            Return clsOdbc.executeScalar_str("SELECT SUM(item_quantity) AS Item_Count FROM manual_shopping_cart")
        Catch ex As Exception
            Return "0"
        End Try

    End Function

    '**** Get MAX Inventory Count for Current Year ****
    Public Function GetMaxInevntoryCount() As Integer
        Try
            Return clsOdbc.executeScalar_str("SELECT SUM(item_purchased) As item_purchased From inv_items WHERE DATE_FORMAT(item_purchase_date,'%Y') = DATE_FORMAT(now(),'%Y')Group By DATE_FORMAT(item_purchase_date,'%b %Y') Order By item_purchased DESC LIMIT 1")
        Catch ex As Exception
            Return "0"
        End Try

    End Function

    Public Function GetTotalAdvancePrice(ByVal strInvId As String) As String
        Try
            Dim strPrice As Integer = clsOdbc.executeScalar_str("SELECT total_price From invoice_main WHERE Active=1 and invoice_id=" & strInvId)
            Return strPrice / 2
        Catch ex As Exception
            Return "0"
        End Try

    End Function

    '**** Get Ads Space Purchase Date ****
    Public Function GetAdSpacePINUsedDate(ByVal intUserid As Integer) As String

        Dim intCount As Integer = clsOdbc.executeScalar_int("SELECT Count(*) From mlm_buy_adspace Where userid=" & intUserid & " and epintype=1")
        Dim strBuyDate As String

        If intCount > 0 Then

            Dim strDate As String = clsOdbc.executeScalar_str("SELECT DATE_FORMAT(buy_date,'%d %M %Y') As buy_date From mlm_buy_adspace Where userid=" & intUserid & " and epintype=1")
            strBuyDate = strDate
        Else
            strBuyDate = "N/A"
        End If

        Return strBuyDate

    End Function

    '**** Get Ads Space Purchase Date ****
    Public Function GetAdditionalAdsSpaceCount(ByVal intUserid As Integer) As Integer

        Try
            Return clsOdbc.executeScalar_int("SELECT Count(*) From mlm_buy_adspace Where userid=" & intUserid & " and epintype=2")
        Catch ex As Exception
            Return "0"
        End Try

    End Function

    Public Function list_child_left(ByVal id As Integer) As ArrayList
        Dim Llist As New ArrayList()

        Dim intDirect_nodeL As Integer = clsOdbc.executeScalar_int("SELECT COUNT(*) FROM mlm_binary_tree WHERE node_flag = 'L' AND parent_node_id =" & id)

        Llist.Add(0)

        If intDirect_nodeL = 1 Then
            Dim tempL_id As Integer = clsOdbc.executeScalar_int("SELECT userid FROM mlm_binary_tree WHERE node_flag = 'L' AND parent_node_id =" & id)
            Llist.Add(tempL_id)

            Llist = list_child(Llist, Llist)
        End If
        Return Llist
    End Function
   
    Public Function list_child_right(ByVal id As Integer) As ArrayList
        Dim Rlist As New ArrayList()
        Dim intDirect_nodeR As Integer = clsOdbc.executeScalar_int("SELECT COUNT(*) FROM mlm_binary_tree WHERE node_flag = 'R' AND parent_node_id =" & id)

        Rlist.Add(0)

        If intDirect_nodeR = 1 Then
            Dim tempR_id As Integer = clsOdbc.executeScalar_int("SELECT userid FROM mlm_binary_tree WHERE node_flag = 'R' AND parent_node_id = " & id)
            Rlist.Add(tempR_id)

            Rlist = list_child(Rlist, Rlist)
        End If
        Return Rlist
    End Function

   
    Public Function list_child(ByVal tlist As ArrayList, ByVal plist As ArrayList) As ArrayList
        If tlist.Count <> 0 Then
            Dim clist As New ArrayList()
            Dim tcount As Integer = tlist.Count

            For i As Integer = 0 To tcount - 1
                Dim ds As New Data.DataSet()
                ds = clsOdbc.getDataSet("SELECT userid FROM `mlm_referral` WHERE `my_sponsar_sys_id`=(SELECT `my_sponsar_id` FROM mlm_referral WHERE userid=" & tlist(i).ToString() & ")")
                If ds.Tables(0).Rows.Count > 0 Then
                    Dim rowsCount As Integer = ds.Tables(0).Rows.Count

                    For j As Integer = 0 To rowsCount - 1
                        clist.Add(ds.Tables(0).Rows(j)(0).ToString())
                        plist.Add(ds.Tables(0).Rows(j)(0).ToString())
                    Next
                End If
                ds.Dispose()
            Next
            Return list_child(clist, plist)
        Else
            Return plist
        End If
    End Function



    Public Function search_click(ByVal txt1 As TextBox, ByVal col1 As String, ByVal txt2 As TextBox, ByVal col2 As String, ByVal txt3 As TextBox, ByVal col3 As String) As String
        Dim strsel As String = ""
        If txt1.Text <> "" Then
            strsel = strsel & " AND " & col1 & " ='" & Convert.ToString(txt1.Text) & "'"
        End If
        If txt2.Text <> "" Then
            strsel = strsel & " AND " & col2 & " LIKE '%" & Convert.ToString(txt2.Text) & "%'"
        End If
        If txt3.Text <> "" Then
            strsel = " AND " & col3 & " LIKE '%" & Convert.ToString(txt3.Text) & "%'"
        End If
        Return strsel
    End Function

    Public Function search_click(ByVal txt1 As TextBox, ByVal col1 As String, ByVal txt2 As TextBox, ByVal col2 As String, ByVal txt3 As TextBox, ByVal col3 As String, ByVal txt4 As TextBox, ByVal col4 As String) As String
        Dim strsel As String = ""
        If txt1.Text <> "" Then
            strsel = strsel & " AND " & col1 & " ='" & Convert.ToString(txt1.Text) & "'"
        End If
        If txt2.Text <> "" Then
            strsel = strsel & " AND " & col2 & " LIKE '%" & Convert.ToString(txt2.Text) & "%'"
        End If
        If txt3.Text <> "" Then
            strsel = " AND " & col3 & " LIKE '%" & Convert.ToString(txt3.Text) & "%'"
        End If
        If txt4.Text <> "" Then
            strsel = " AND " & col4 & " LIKE '%" & Convert.ToString(txt4.Text) & "%'"
        End If
        Return strsel
    End Function

    Public Function search_click(ByVal txt1 As TextBox, ByVal col1 As String, ByVal txt2 As TextBox, ByVal col2 As String, ByVal txt3 As TextBox, ByVal col3 As String, ByVal ddl1 As DropDownList, ByVal col4 As String, ByVal ddl2 As DropDownList, ByVal col5 As String) As String
        Dim strsel As String = ""
        If txt1.Text <> "" Then
            strsel = strsel & " AND " & col1 & " ='" & Convert.ToString(txt1.Text) & "'"
        End If
        If txt2.Text <> "" Then
            strsel = strsel & " AND " & col2 & " LIKE '%" & Convert.ToString(txt2.Text) & "%'"
        End If
        If txt3.Text <> "" Then
            strsel = " AND " & col3 & " LIKE '%" & Convert.ToString(txt3.Text) & "%'"
        End If
        If ddl1.SelectedIndex <> 0 Then
            strsel = strsel & " AND " & col4 & " =" & Convert.ToString(ddl1.SelectedValue)
        End If
        If ddl2.SelectedIndex <> 0 Then
            strsel = strsel & " AND " & col5 & " =" & Convert.ToString(ddl2.SelectedValue)
        End If
        Return strsel
    End Function
    Public Sub sendSMS(ByVal intUserID As Integer, ByVal strMobile As String, ByVal strMessage As String)

        Dim senderusername As String = "SBRETL"
        Dim senderpassword As String = "ZOLTACITY10"
        Dim senderid As String = "SBRETL"

        Try

            'Dim strMobile As String = "8097610479"

            'Dim strMessage As String = "Thanks for visit & login on www.moneybazaar.org Your One Time Password <VAR> have A nice Day."

            'Dim sURL As String

            'sURL = (("http://zsms.co.in/new/api/api_http.php?username=" & senderusername & "&password=" & senderpassword & "&senderid=" & senderid & "&to=91" & strMobile & "&text=" & strMessage & "&route=Transactional" & "&type=text" & "&datetime=2013-05-31%2012%3A20%3A57"))


            'Dim request As Net.WebRequest

            'request = Net.WebRequest.Create(sURL)

            'Dim resp As Net.WebResponse = request.GetResponse()

            clsOdbc.executeNonQuery("INSERT INTO mlm_send_sms_log (userid,sms_to,sms_message,sms_status) VALUES (" & intUserID & ",'" & strMobile & "','" & strMessage & "',1)")

        Catch ex As Exception

            clsOdbc.executeNonQuery("INSERT INTO mlm_send_sms_log (userid,sms_to,sms_message,sms_status) VALUES (" & intUserID & ",'" & strMobile & "','" & strMessage & "',0)")

        End Try


    End Sub
    Public Sub AddLoginHistory(ByVal intUserID As Integer, ByVal strIPAddress As String)

        Try
            clsOdbc.executeNonQuery("INSERT INTO mlm_login_history (userid,ip_address,login_date_time) VALUES (" & intUserID & ",'" & strIPAddress & "',Now())")
        Catch ex As Exception

        End Try

    End Sub

    Public Function FillGridViewDataMessage(ByVal strQuery As String, ByVal gvGridView As GridView) As Integer

        Dim ds As New Data.DataSet

        Try

            ds = clsOdbc.getDataSet(strQuery)
            If (ds.Tables(0).Rows.Count > 0) Then
                gvGridView.DataSource = ds
                gvGridView.DataBind()
                Return 1
            Else
                'CommonMessages.ShowAlertMessage("Sorry, No Records Found!")
                gvGridView.DataSource = Nothing
                gvGridView.DataBind()
                Return 0
            End If
        Catch ex As Exception

        Finally
            ds.Dispose()
        End Try

    End Function

    Public Sub fillState(ByVal ddlState As DropDownList)
        Try
            filldropdownlist("Select state_id,state_name From crm_state", ddlState, "state_name", "state_id")

        Catch ex As Exception

        End Try
    End Sub
    Public Sub fillDistrict(ByVal ddlDistrict As DropDownList, ByVal strState As String)
        Try

            filldropdownlist("Select district_id,district_name From mlm_district Where state_id = " & strState & "", ddlDistrict, "district_name", "district_id")

        Catch ex As Exception

        End Try
    End Sub
End Class
