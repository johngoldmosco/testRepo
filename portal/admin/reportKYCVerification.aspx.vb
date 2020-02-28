
Partial Class portal_admin_reportKYCVerification
    Inherits System.Web.UI.Page

    Dim clsOdbc As New ODBC
    Dim objCommon As New clsCommon
    Dim objcomm As New clsCommunication
    Private Const ASCENDING As String = " ASC"
    Private Const DESCENDING As String = " DESC"

    Private Function GetData(ByVal intpageindex As Integer) As Data.DataView

        Dim strQuery As String
        Dim ds As Data.DataSet = New Data.DataSet
        Dim dv As Data.DataView = New Data.DataView
        Dim strpageSize As Integer = gvPINRequest.PageSize
        Dim intStart As Integer = (intpageindex - 1) * strpageSize + 1
        Dim strearch As String = ""

        If txtUserID.Text <> "" Then
            strearch = "AND c.my_sponsar_id = '" & txtUserID.Text & "'"
        End If

        intStart = intStart - 1
        gvPINRequest.PageIndex = intpageindex
        Dim count As Integer = clsOdbc.executeScalar_int("SELECT Count(1) From mlm_kyc_documents a,mlm_personal_details b,mlm_login c Where a.kyc_status=0 and a.userid=b.userid and a.userid=c.userid " & strearch & " Order By a.id ASC")
        strQuery = "SELECT a.userid,b.username,c.my_sponsar_id, a.pan_card, a.aadhar_card, a.photo, a.kyc_status, DATE_FORMAT(a.kyc_on,'%d %M %Y') As upload_date, a.cheque From mlm_kyc_documents a,mlm_personal_details b,mlm_login c Where a.kyc_status=0 and a.userid=b.userid and a.userid=c.userid " & strearch & " Order By a.id ASC"
        Dim dblPageCount As Double = CDbl(CDec(count) / Convert.ToDecimal(strpageSize))
        Dim pageCount As Integer = CInt(Math.Ceiling(dblPageCount))
        ViewState("pageCount") = pageCount
        Me.PopulatePager(intpageindex)
        Try
            ds = clsOdbc.getDataSet(strQuery)

            If ds.Tables(0).Rows.Count > 0 Then
                If (ViewState("sortExp") <> Nothing) Then
                    dv = New Data.DataView(ds.Tables(0))

                    If (GridViewSortDirection = SortDirection.Ascending) Then
                        GridViewSortDirection = SortDirection.Descending
                        dv.Sort = CType(ViewState("sortExp") & DESCENDING, String)
                    Else
                        GridViewSortDirection = SortDirection.Ascending
                        dv.Sort = CType(ViewState("sortExp") & ASCENDING, String)
                    End If
                Else
                    dv = ds.Tables(0).DefaultView
                End If

                Return dv
            Else
                CommonMessages.ShowAlertMessage("Sorry, No Records Found!")
            End If

        Catch ex As Exception

        End Try

    End Function

    '**** Property for Getting Employee Gridview Display Data Order. *****
    Public Property GridViewSortDirection() As SortDirection
        Get
            If ViewState("sortDir") = Nothing Then
                ViewState("sortDir") = SortDirection.Ascending
            End If

            Return CType(ViewState("sortDir"), SortDirection)
        End Get

        Set(ByVal value As SortDirection)
            ViewState("sortDir") = value
        End Set

    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Session("AdminID") = Nothing Or Session("AdminID") = "" Then
        '    Response.Redirect("../../login.aspx")
        'End If

        If Not IsPostBack Then
            gvPINRequest.DataSource = GetData(1)
            gvPINRequest.DataBind()
        End If
    End Sub

    Protected Sub gvPINRequest_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvPINRequest.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Or e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(0).Visible = False
            e.Row.Cells(5).Visible = False
            e.Row.Cells(6).Visible = False
            e.Row.Cells(7).Visible = False
            e.Row.Cells(14).Visible = False

        End If
        Dim rw As GridViewRow
        For Each rw In gvPINRequest.Rows

            Dim lnkviewpan As HyperLink
            lnkviewpan = CType(rw.FindControl("lnkviewpancard"), HyperLink)
            If rw.Cells(5).Text = "" Then
                lnkviewpan.Enabled = False
            End If
            lnkviewpan.NavigateUrl = rw.Cells(5).Text

            Dim lnkviewaadhar As HyperLink
            lnkviewaadhar = CType(rw.FindControl("lnkViewAadharCard"), HyperLink)
            If rw.Cells(6).Text = "" Then
                lnkviewaadhar.Enabled = False
            End If
            lnkviewaadhar.NavigateUrl = rw.Cells(6).Text

            Dim lnkviewphoto As HyperLink
            lnkviewphoto = CType(rw.FindControl("lnkviewpho"), HyperLink)
            If rw.Cells(7).Text = "" Then
                lnkviewphoto.Enabled = False
            End If
            lnkviewphoto.NavigateUrl = rw.Cells(7).Text

            Dim lnkviewchq As HyperLink
            lnkviewchq = CType(rw.FindControl("lnkviewchque"), HyperLink)
            If rw.Cells(14).Text = "" Then
                lnkviewchq.Enabled = False
            End If
            lnkviewchq.NavigateUrl = rw.Cells(14).Text

            If rw.Cells(12).Text = "0" Then
                rw.Cells(12).Text = "Pending"
            End If
        Next
    End Sub

    Protected Sub gvPINRequest_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvPINRequest.RowCommand
        If e.CommandName = "APPROVE" Then
            Dim lnkView As LinkButton = DirectCast(e.CommandSource, LinkButton)
            Dim intuserid As Integer = Integer.Parse(lnkView.CommandArgument)

            clsOdbc.executeNonQuery("update mlm_login set kyc_status=1 where userid='" & intuserid & "'")
            clsOdbc.executeNonQuery("update mlm_kyc_documents set kyc_status=1 where userid='" & intuserid & "'")
            CommonMessages.ShowAlertMessage_Reload("KYC Approved Successfully", "reportKYCVerification.aspx")
        End If

        If e.CommandName = "PENDING" Then
            Dim lnkView As LinkButton = DirectCast(e.CommandSource, LinkButton)
            Dim intuserid As Integer = Integer.Parse(lnkView.CommandArgument)
            clsOdbc.executeNonQuery("update mlm_login set kyc_status=0 where userid='" & intuserid & "'")
            clsOdbc.executeNonQuery("update mlm_kyc_documents set kyc_status=0 where userid='" & intuserid & "'")
            CommonMessages.ShowAlertMessage_Reload("KYC Pending Successfully", "reportKYCVerification.aspx")
        End If

        If e.CommandName = "REJECT" Then
            Dim lnkView As LinkButton = DirectCast(e.CommandSource, LinkButton)
            Dim intuserid As Integer = Integer.Parse(lnkView.CommandArgument)
            clsOdbc.executeNonQuery("update mlm_login set kyc_status=2 where userid='" & intuserid & "'")
            clsOdbc.executeNonQuery("update mlm_kyc_documents set kyc_status=2 where userid='" & intuserid & "'")

            Dim strMobileNumber As String = clsOdbc.executeScalar_str("SELECT mobile_number FROM mlm_personal_details WHERE userid=" & intuserid & "")

            Dim strmessage As String = "Hello Member, Your KYC Documents are Rejected Please upload again all Documents. iprogress.co.in"
            '      objcomm.SMS_API_for_Single_Message("T1kxhVrSnUO0VLjxQsS6sg", "API", "TESTIN", 1, strmessage, strMobileNumber)
            CommonMessages.ShowAlertMessage_Reload("KYC Reject Successfully", "reportKYCVerification.aspx")
        End If
    End Sub

    Protected Sub Page_Changed(ByVal sender As Object, ByVal e As EventArgs)
        Dim pageIndex As Integer = Integer.Parse(TryCast(sender, LinkButton).CommandArgument)
        gvPINRequest.DataSource = GetData(pageIndex)
        gvPINRequest.DataBind()
    End Sub
    Protected Sub PopulatePager(ByVal pageIndex As Integer)
        Dim ButtonCount As Integer = 10
        Dim pages As New System.Collections.Generic.List(Of ListItem)()
        Dim pageCount As Integer = Int32.Parse(ViewState("pageCount").ToString())
        If pageCount < 1 Then
            Return
        End If

        Dim start As Integer = pageIndex - (pageIndex Mod ButtonCount)
        Dim [end] As Integer = pageIndex + (ButtonCount - (pageIndex Mod ButtonCount))
        If start <= 0 Then
            start = start + 1
        End If
        If [end] > pageCount Then
            [end] = pageCount + 1
        End If

        If start > (ButtonCount - 1) Then
            pages.Add(New ListItem("---", (start - 1).ToString()))
        End If
        Dim i As Integer = 0
        For i = start To [end] - 1
            pages.Add(New ListItem(i.ToString(), i.ToString(), i <> pageIndex))
        Next
        If pageCount > [end] Then
            pages.Add(New ListItem("---", (i).ToString()))
        End If
        rptPager.DataSource = pages
        rptPager.DataBind()
    End Sub
    Public Function process(ByVal value1 As Object) As String
        If Convert.ToBoolean(value1) = True Then
            Return "btn_box"
        Else
            Return "current_page"
        End If

    End Function
    Public Function process1(ByVal value1 As Object) As String
        If Not Convert.ToBoolean(value1) Then
            Return "false"
        Else
            Return ""
        End If

    End Function

    Protected Sub btnadd_Click(sender As Object, e As EventArgs)
        gvPINRequest.DataSource = GetData(1)
        gvPINRequest.DataBind()
    End Sub
End Class
