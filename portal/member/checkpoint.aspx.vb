
Partial Class portal_member_checkpoint
    Inherits System.Web.UI.Page

    Dim clsOdbc As New ODBC

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        btnSave.Attributes.Add("onclick", "return validate()")

        If Session("UserID") = Nothing Or Session("UserID") = "" Then
            Response.Redirect("../../login.aspx")
        End If

        If Not IsPostBack Then
            ViewState("PrevPage") = Request.UrlReferrer

        End If

    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect("checkpoint.aspx")
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click


        If Session("UserID") = Nothing Or Session("UserID") = "" Then
            Response.Redirect("../../login.aspx")
        Else
            Dim strUserID As String = Session("UserID")
            Dim intCount As Integer = clsOdbc.executeScalar_int("SELECT COUNT(1) FROM mlm_login WHERE userid='" & Session("UserID") & "' AND trans_pwd='" & txtPwd.Text & "'")
            If intCount = 1 Then
                Session("transID") = strUserID

                If Request.QueryString("PID") = "" Then
                    Response.Redirect("Overview.aspx")

                Else

                    Dim strUrl As String = Request.QueryString("PID")

                    Response.Redirect(strUrl)
                End If
            Else
                 lblError.Text = "Transactional Password is Wrong!"
				  txtPwd.Text = ""
                txtPwd.Focus()
            End If

        End If


    End Sub

End Class

