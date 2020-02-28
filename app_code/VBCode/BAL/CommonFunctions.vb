Imports System.Data
Imports System.Configuration
Imports System.Linq
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Xml.Linq
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.html.simpleparser
Imports iTextSharp.text.pdf
Imports System.Net.Mail

''' <summary>
''' Summary description for CommonFunctions
''' </summary>
Public Class CommonFunctions

    '
    ' TODO: Add constructor logic here
    '
    Public Sub New()
    End Sub
    Public Shared Sub FillComboByDataTable(ByVal cmbbox As DropDownList, ByVal DisplayMember As String, ByVal ValueMember As String, ByVal strQuery As String)
        Dim dt As DataTable = Nothing
        Dim objODBC As New ODBC()
        dt = objODBC.getDataTable(strQuery)
        Try

            cmbbox.DataSource = dt
            cmbbox.DataTextField = DisplayMember
            cmbbox.DataValueField = ValueMember
            cmbbox.DataBind()
        Catch ex As Exception
            CommonMessages.ShowAlertMessage(ex.Message)
        Finally
            dt.Dispose()
        End Try

    End Sub
    Public Shared Function FillComboById(ByVal cmbbox As DropDownList, ByVal DisplayMember As String, ByVal ValueMember As String, ByVal strQuery As String) As Boolean
        Dim dt As DataTable = Nothing
        Try
            Dim objODBC As New ODBC()
            dt = objODBC.getDataTable(strQuery)
            cmbbox.DataSource = dt
            If dt.Rows.Count <> 0 Then
                cmbbox.DataTextField = DisplayMember
                cmbbox.DataValueField = ValueMember
                cmbbox.DataBind()
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            CommonMessages.ShowAlertMessage(ex.Message)
        Finally
            dt.Dispose()
        End Try
        Return False
    End Function

    Public Shared Sub exportFile(ByVal strfileName As String, ByVal strfileExtention As String, ByVal gv As GridView)

        Dim style As String = "<style>.text{mso-number-format:\@;text-align:right;}</style>"
        HttpContext.Current.Response.Clear()
        HttpContext.Current.Response.AddHeader("content-disposition", String.Format("attachment; filename={0}", strfileName))
        Using sw As New System.IO.StringWriter()
            Using htw As New HtmlTextWriter(sw)
                HttpContext.Current.Response.Write(style)

                '  Create a table to contain the grid
                Dim table As New System.Web.UI.WebControls.Table()
                'Panel objPanel = new Panel();
                'System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();

                'string strpath = @"~/images/crmlogo.png";
                'img.ImageUrl = HttpContext.Current.Server.MapPath(strpath);
                'PrepareControlForExport(img);
                'if (strfileExtention.Equals(".pdf"))
                '{
                '    objPanel.Controls.Add(img);
                '}
                'if (strfileExtention.Equals(".doc"))
                '{
                '    objPanel.Controls.Add(img);
                '}
                'include the gridline settings
                table.GridLines = gv.GridLines


                '  add the header row to the table
                If gv.HeaderRow IsNot Nothing Then
                    PrepareControlForExport(gv.HeaderRow)
                    table.Style.Add("width", "25%")
                    table.Style.Add("font-size", "12px")
                    table.Rows.Add(gv.HeaderRow)
                End If

                '  add each of the data rows to the table
                For Each row As GridViewRow In gv.Rows
                    ' add numeric style for each cell
                    For Each cell As TableCell In row.Cells
                        cell.Attributes.Add("class", "text")
                    Next
                    PrepareControlForExport(row)
                    table.Style.Add("text-decoration", "none")
                    table.Style.Add("font-family", "Arial, Helvetica, sans-serif;")
                    table.Style.Add("font-size", "8px")
                    table.Rows.Add(row)
                Next

                '  add the footer row to the table
                If gv.FooterRow IsNot Nothing Then
                    PrepareControlForExport(gv.FooterRow)
                    table.Rows.Add(gv.FooterRow)
                End If

                '  render the table into the htmlwriter
                'objPanel.RenderControl(htw);
                table.RenderControl(htw)


                'If strfileExtention.Equals(".pdf") Then
                '    HttpContext.Current.Response.ContentType = "application/pdf"
                '    '  render the htmlwriter into the response
                '    Dim sr As New StringReader(sw.ToString())
                '    Dim pdfDoc As New Document(PageSize.A4, 7.0F, 7.0F, 40.0F, 7.0F)
                '    Dim htmlparser As New HTMLWorker(pdfDoc)

                '    Dim respone As HttpResponse = HttpContext.Current.Response
                '    PdfWriter.GetInstance(pdfDoc, respone.OutputStream)
                '    pdfDoc.Open()
                '    htmlparser.Parse(sr)
                '    pdfDoc.Close()
                'End If
                If strfileExtention.Equals(".xls") Then
                    HttpContext.Current.Response.ContentType = "application/ms-excel"
                    '  render the htmlwriter into the response
                    HttpContext.Current.Response.Write(sw.ToString())
                    HttpContext.Current.Response.[End]()
                End If
                If strfileExtention.Equals(".doc") Then
                    HttpContext.Current.Response.ContentType = "application/ms-word"
                    '  render the htmlwriter into the response
                    HttpContext.Current.Response.Write(sw.ToString())
                    HttpContext.Current.Response.[End]()

                End If
            End Using
        End Using
    End Sub


    Public Shared Sub exportFile(ByVal strfileName As String, ByVal strfileExtention As String, ByVal gv As GridView, ByVal pnl As Panel)

        Dim style As String = "<style>.text{mso-number-format:\@;text-align:right;}</style>"
        HttpContext.Current.Response.Clear()
        HttpContext.Current.Response.AddHeader("content-disposition", String.Format("attachment; filename={0}", strfileName))
        Using sw As New System.IO.StringWriter()
            Using htw As New HtmlTextWriter(sw)
                HttpContext.Current.Response.Write(style)

                '  Create a table to contain the grid
                Dim table As New System.Web.UI.WebControls.Table()
                Dim objPanel As New Panel()
                Dim img As New System.Web.UI.WebControls.Image()

                'Dim strpath As String = "images/sms_report.png"
                'img.ImageUrl = HttpContext.Current.Server.MapPath(strpath)
                'PrepareControlForExport(img)

                PrepareControlForExport(pnl)
                If strfileExtention.Equals(".pdf") Then
                    objPanel.Style.Add("width", "15%")
                    objPanel.Style.Add("font-size", "9px")
                    objPanel.Controls.Add(img)
                    objPanel.Controls.Add(pnl)
                End If
                If strfileExtention.Equals(".doc") Then
                    objPanel.Style.Add("width", "25%")
                    objPanel.Style.Add("font-size", "12px")
                    objPanel.Controls.Add(img)
                    objPanel.Controls.Add(pnl)
                End If

                If strfileExtention.Equals(".xls") Then
                    objPanel.Style.Add("width", "25%")
                    objPanel.Style.Add("font-size", "12px")
                    objPanel.Controls.Add(pnl)
                End If

                'include the gridline settings
                table.GridLines = gv.GridLines


                '  add the header row to the table
                If gv.HeaderRow IsNot Nothing Then
                    PrepareControlForExport(gv.HeaderRow)
                    table.Style.Add("width", "25%")
                    table.Style.Add("font-size", "12px")
                    table.Rows.Add(gv.HeaderRow)
                End If

                '  add each of the data rows to the table
                For Each row As GridViewRow In gv.Rows
                    ' add numeric style for each cell
                    For Each cell As TableCell In row.Cells
                        cell.Attributes.Add("class", "text")
                    Next
                    PrepareControlForExport(row)
                    table.Style.Add("text-decoration", "none")
                    table.Style.Add("font-family", "Arial, Helvetica, sans-serif;")
                    table.Style.Add("font-size", "8px")
                    table.Rows.Add(row)
                Next

                '  add the footer row to the table
                If gv.FooterRow IsNot Nothing Then
                    PrepareControlForExport(gv.FooterRow)
                    table.Rows.Add(gv.FooterRow)
                End If

                '  render the table into the htmlwriter

                objPanel.RenderControl(htw)
                table.RenderControl(htw)


                'If strfileExtention.Equals(".pdf") Then
                '    HttpContext.Current.Response.ContentType = "application/pdf"
                '    '  render the htmlwriter into the response
                '    Dim sr As New StringReader(sw.ToString())
                '    Dim pdfDoc As New Document(PageSize.A4, 7.0F, 7.0F, 40.0F, 7.0F)
                '    Dim htmlparser As New HTMLWorker(pdfDoc)

                '    Dim respone As HttpResponse = HttpContext.Current.Response
                '    PdfWriter.GetInstance(pdfDoc, respone.OutputStream)
                '    pdfDoc.Open()
                '    htmlparser.Parse(sr)
                '    pdfDoc.Close()
                'End If
                If strfileExtention.Equals(".xls") Then
                    HttpContext.Current.Response.ContentType = "application/ms-excel"
                    '  render the htmlwriter into the response
                    HttpContext.Current.Response.Write(sw.ToString())
                    HttpContext.Current.Response.[End]()
                End If
                If strfileExtention.Equals(".doc") Then
                    HttpContext.Current.Response.ContentType = "application/ms-word"
                    '  render the htmlwriter into the response
                    HttpContext.Current.Response.Write(sw.ToString())
                    HttpContext.Current.Response.[End]()

                End If
            End Using
        End Using
    End Sub

    Public Shared Sub exportFileList(ByVal strfileName As String, ByVal strfileExtention As String, ByVal gv As ListView, ByVal pnl As Panel)

        Dim style As String = "<style>.text{mso-number-format:\@;text-align:right;}</style>"
        HttpContext.Current.Response.Clear()
        HttpContext.Current.Response.AddHeader("content-disposition", String.Format("attachment; filename={0}", strfileName))
        Using sw As New System.IO.StringWriter()
            Using htw As New HtmlTextWriter(sw)
                HttpContext.Current.Response.Write(style)

                '  Create a table to contain the grid
                Dim table As New System.Web.UI.WebControls.Table()
                Dim objPanel As New Panel()
                Dim img As New System.Web.UI.WebControls.Image()

                'Dim strpath As String = "images/sms_report.png"
                'img.ImageUrl = HttpContext.Current.Server.MapPath(strpath)
                'PrepareControlForExport(img)

                PrepareControlForExport(pnl)
                If strfileExtention.Equals(".pdf") Then
                    objPanel.Style.Add("width", "15%")
                    objPanel.Style.Add("font-size", "9px")
                    objPanel.Controls.Add(img)
                    objPanel.Controls.Add(pnl)
                End If
                If strfileExtention.Equals(".doc") Then
                    objPanel.Style.Add("width", "25%")
                    objPanel.Style.Add("font-size", "12px")
                    objPanel.Controls.Add(img)
                    objPanel.Controls.Add(pnl)
                End If

                If strfileExtention.Equals(".xls") Then
                    objPanel.Style.Add("width", "25%")
                    objPanel.Style.Add("font-size", "12px")
                    objPanel.Controls.Add(pnl)
                End If

                'include the gridline settings
              

                '  add the header row to the table
              
                For Each row As ListViewItem In gv.Items

                    ' add numeric style for each cell

                   
                    PrepareControlForExport(row)
                    table.Style.Add("text-decoration", "none")
                    table.Style.Add("font-family", "Arial, Helvetica, sans-serif;")
                    table.Style.Add("font-size", "8px")

                Next

               

               

                '  render the table into the htmlwriter

                objPanel.RenderControl(htw)
                table.RenderControl(htw)


                'If strfileExtention.Equals(".pdf") Then
                '    HttpContext.Current.Response.ContentType = "application/pdf"
                '    '  render the htmlwriter into the response
                '    Dim sr As New StringReader(sw.ToString())
                '    Dim pdfDoc As New Document(PageSize.A4, 7.0F, 7.0F, 40.0F, 7.0F)
                '    Dim htmlparser As New HTMLWorker(pdfDoc)

                '    Dim respone As HttpResponse = HttpContext.Current.Response
                '    PdfWriter.GetInstance(pdfDoc, respone.OutputStream)
                '    pdfDoc.Open()
                '    htmlparser.Parse(sr)
                '    pdfDoc.Close()
                'End If
                If strfileExtention.Equals(".xls") Then
                    HttpContext.Current.Response.ContentType = "application/ms-excel"
                    '  render the htmlwriter into the response
                    HttpContext.Current.Response.Write(sw.ToString())
                    HttpContext.Current.Response.[End]()
                End If
                If strfileExtention.Equals(".doc") Then
                    HttpContext.Current.Response.ContentType = "application/ms-word"
                    '  render the htmlwriter into the response
                    HttpContext.Current.Response.Write(sw.ToString())
                    HttpContext.Current.Response.[End]()

                End If
            End Using
        End Using
    End Sub
    ''' <summary>
    ''' Replace any of the contained controls with literals
    ''' </summary>
    ''' <param name="control"></param>
    Private Shared Sub PrepareControlForExport(ByVal control As Control)
        For i As Integer = 0 To control.Controls.Count - 1
            Dim current As Control = control.Controls(i)
            If TypeOf current Is System.Web.UI.WebControls.Image Then
                control.Controls.Remove(current)
            End If
            If TypeOf current Is LinkButton Then
                control.Controls.Remove(current)
                control.Controls.AddAt(i, New LiteralControl(TryCast(current, LinkButton).Text))
            ElseIf TypeOf current Is ImageButton Then
                control.Controls.Remove(current)
                control.Controls.AddAt(i, New LiteralControl(TryCast(current, ImageButton).AlternateText))
            ElseIf TypeOf current Is HyperLink Then
                control.Controls.Remove(current)
                control.Controls.AddAt(i, New LiteralControl(TryCast(current, HyperLink).Text))
            ElseIf TypeOf current Is DropDownList Then
                control.Controls.Remove(current)
                control.Controls.AddAt(i, New LiteralControl(TryCast(current, DropDownList).SelectedItem.Text))
                'ElseIf TypeOf current Is CheckBox Then
                'control.Controls.Remove(current)
                'control.Controls.AddAt(i, New LiteralControl(If(TryCast(current, CheckBox).Checked, "True", "False")))
            End If
            If current.HasControls() Then
                PrepareControlForExport(current)
            End If
        Next
    End Sub

    'sending email 
    Public Shared Sub sendEmailMessage(ByVal strHost As String, ByVal strFrom As String, ByVal strTO As String, ByVal strSubject As String, ByVal strMessage As String)
        Dim Mailmsg As New MailMessage()
        Dim SmtpMail As New SmtpClient(strHost)
        Try
            Mailmsg.[To].Clear()
            Mailmsg.From = New MailAddress(strFrom)
            Mailmsg.[To].Add(New MailAddress(strTO))
            Mailmsg.Subject = strSubject
            Mailmsg.IsBodyHtml = True
            Mailmsg.Body = strMessage
            SmtpMail.Send(Mailmsg)
        Catch ex As Exception
            CommonMessages.ShowAlertMessage("Error on Sendimg Mail." + ex.Message)
            Return
        End Try
    End Sub

    'send multiple emails
    Public Shared Sub sendEmailMessage(ByVal strHost As String, ByVal strFrom As String, ByVal arrlEmailId As ArrayList, ByVal strSubject As String, ByVal strMessage As String)
        Dim Mailmsg As New MailMessage()
        Dim SmtpMail As New SmtpClient(strHost)
        Try
            Mailmsg.[To].Clear()
            Mailmsg.From = New MailAddress(strFrom)
            For i As Integer = 0 To arrlEmailId.Count - 1
                If i = 0 Then
                    Mailmsg.[To].Add(New MailAddress(arrlEmailId(i).ToString()))
                Else
                    Mailmsg.Bcc.Add(New MailAddress(arrlEmailId(i).ToString()))
                End If
            Next
            Mailmsg.Subject = strSubject
            Mailmsg.IsBodyHtml = True
            Mailmsg.Body = strMessage
            SmtpMail.Send(Mailmsg)
        Catch ex As Exception
            CommonMessages.ShowAlertMessage("Error on Sendimg Mail." + ex.Message)
        End Try
    End Sub

    'Generating Random Passwords
    Public Shared Function GetPassword() As String
        ' Get the GUID
        Dim guidResult As String = System.Guid.NewGuid().ToString()

        ' Remove the hyphens
        guidResult = guidResult.Replace("-", String.Empty)
        Return guidResult.Substring(0, 8)
    End Function
End Class