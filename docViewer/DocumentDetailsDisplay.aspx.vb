Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Collections.Generic
Imports System.IO
Imports iTextSharp
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports AddWatermark
Imports Delete

Partial Class DocumentDetails
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("UserName") = "" Then
            Response.Redirect("~/AccessDenied.aspx")
        End If
        'lblWait.Text = Session("UserLevel")
        TextBox7.Text = Session("Identifier")
        If Session("UserLevel") = "1" Or Session("UserLevel") = "2" Then
            If Session("Status") = "Approved" Then
                Dim btnPDF As Button = CType(FormView2.FindControl("btnPDF"), Button)
                btnPDF.Visible = True
                btnApprove.Visible = False
                pdfViewer.Visible = False
                btnApprove.Visible = False
            End If
        ElseIf Session("UserLevel") = "3" Then

            If Session("Status") = "For Approval" Or Session("Status") = "Pending" Then
                btnDelete.Visible = True
                Dim btnPDF As Button = CType(FormView2.FindControl("btnPDF"), Button)
                btnPDF.Visible = True
                pdfViewer.Visible = False
                btnApprove.Visible = True
                btnPrint.Visible = False
            ElseIf Session("Status") = "Denied-Unread" Or Session("Status") = "Denied-Read" Then
                btnDelete.Visible = False
                Dim btnPDF As Button = CType(FormView2.FindControl("btnPDF"), Button)
                btnPDF.Visible = True
                pdfViewer.Visible = False
                btnApprove.Visible = True
                btnPrint.Visible = False
                'btnApprove.Visible = True
            ElseIf Session("Status") = "Approved" Then
                Dim btnPDF As Button = CType(FormView2.FindControl("btnPDF"), Button)
                btnPDF.Visible = True
                btnApprove.Visible = False
                pdfViewer.Visible = False
                btnApprove.Visible = False
            End If
            btnPrint.Visible = False
        ElseIf Session("UserLevel") = "5" Then
            pdfViewer.Visible = False
            btnPrint.Visible = False
        End If
        Dim btnClose As Button = CType(FormView2.FindControl("btnClose"), Button)
        If Session("SearchStatus") = "1" Then
            btnClose.Visible = False
        ElseIf Session("SearchStatus") = "2" Then
            btnClose.Visible = True
        End If
        If Session("PendingStatus") = "1" Then
            btnClose.PostBackUrl = "~/Notification.aspx"
        ElseIf Session("PendingStatus") = "2" Then
            btnClose.PostBackUrl = "~/PendingDocuments.aspx"
        ElseIf Session("PendingStatus") = "3" Then
            btnClose.PostBackUrl = "~/Documents.aspx"
        ElseIf Session("PendingStatus") = "4" Then
            btnClose.PostBackUrl = "~/Denied Documents.aspx"
        End If
    End Sub

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        'If FormView2.CurrentMode = FormViewMode.ReadOnly Then

        '    Dim TextBox6 As TextBox = CType(FormView2.FindControl("TextBox6"), TextBox)
        '    'Dim lblWait As Label = CType(FormView2.FindControl("lblWait"), Label)
        '    'Process.Start("IExplore.exe", "http://www.microsoft.com")
        '    'Response.Redirect("~/DigitalFileView.aspx?doc_code=" & TextBox6.Text)

        '    If Session("Availability") = "" Then
        '        'MsgBox("No Link or PDF Attachment Found", MsgBoxStyle.Exclamation, "Error")
        '        lblWait.Visible = True
        '        lblWait.Text = "No Link or PDF Attachment Found."

        '    Else
        '        lblWait.Visible = False
        '        If Session("Origin") = "External" Then
        '            Dim id As String
        '            id = Session("Identifier")
        '            Dim path As String = Server.MapPath(".") & "\Attachments\"
        '            Dim array() As String = {"NAMRIA - " + "" + Session("UserName") + "", "" + DateTime.Now() + ""}
        '            AddWatermarkDoubleText(Session("Availability"), Server.MapPath(".") & "\Attachments\Temp\temp.pdf", array)
        '            lblWait.Visible = True
        '            pdfViewer.Attributes("src") = "Attachments\Temp\temp.pdf"
        '        ElseIf Session("Origin") = "Internal" Then
        '            Dim id As String
        '            id = Session("Identifier")
        '            Dim path As String = Server.MapPath(".") & "\Attachments\"
        '            Dim array() As String = {"NAMRIA - " + "" + Session("UserName") + "", "" + DateTime.Now() + ""}
        '            AddWatermarkDoubleText(Server.MapPath(".") & "\Attachments\" + id + ".pdf", Server.MapPath(".") & "\Attachments\Temp\temp.pdf", array)
        '            lblWait.Visible = True
        '            pdfViewer.Attributes("src") = "Attachments\Temp\temp.pdf" 
        '        End If
        '    End If
        Try
            Dim sqlUserActivity As String = "Insert INTO User_Activity_Viewer (UserName,ModuleAccessed,DateAccessed,FileViewed,SearchedWord,DocumentAdded,OriginalDocument,UpdatedDocument,DeletedDocument) Values (@UserName, @ModuleAccessed, @DateAccessed, @SearchedWord, @FileViewed, @DocumentAdded, @OriginalDocument, @UpdatedDocument, @DeletedDocument)"
            Dim connUserActivity As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
            Dim cmdUserActivity As SqlCommand = New SqlCommand(sqlUserActivity, connUserActivity)
            connUserActivity.Open()
            With cmdUserActivity.Parameters
                .AddWithValue("@UserName", Session("UserName"))
                .AddWithValue("@ModuleAccessed", "View Document Details")
                .AddWithValue("@DateAccessed", DateTime.Now())
                .AddWithValue("@SearchedWord", Session("Identifier"))
                .AddWithValue("@FileViewed", "N/A")
                .AddWithValue("@DocumentAdded", "N/A")
                .AddWithValue("@OriginalDocument", "N/A")
                .AddWithValue("@UpdatedDocument", "N/A")
                .AddWithValue("@DeletedDocument", "N/A")
            End With
            cmdUserActivity.ExecuteNonQuery()

        Finally
            Dim conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
            conn.Close()
        End Try
        Response.Redirect("~/ViewDocument.aspx")
    End Sub

    Protected Sub btnApprove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnApprove.Click
        Try
            Dim sqlApproveDocuments As String = "UPDATE Documents SET status = 'Approved', postedby = '" & Session("UserName") & "' WHERE doc_code = '" & TextBox7.Text & "'"
            Dim connApproveDocuments As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
            Dim cmdApproveDocuments As SqlCommand = New SqlCommand(sqlApproveDocuments, connApproveDocuments)
            connApproveDocuments.Open()
            cmdApproveDocuments.ExecuteNonQuery()
        Finally
            Dim conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
            conn.Close()
        End Try
        'MsgBox("Documents successfully approved.", MsgBoxStyle.Information, "Approve Document")
        Response.Redirect("~/Document Browse.aspx")
    End Sub

    Protected Sub FormView2_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewPageEventArgs) Handles FormView2.PageIndexChanging

    End Sub

    Protected Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click

        Dim TextBox6 As TextBox = CType(FormView2.FindControl("TextBox6"), TextBox)
        Try
            Dim sql As String = "SELECT Documents.doc_code, Documents.doc_subject, Documents.doc_signatory, Documents.doc_signatory_external,  Documents.doc_date, Documents.availability, Documents.origin, Documents.status, Documents.postedby, Doctype_lib.doctype_desc,  Signatories.LAST_M + N', ' + Signatories.FIRST_M + N' ' + substring(Signatories.MIDDLE_M, 1, 1) + '.' AS FullName FROM Documents LEFT OUTER JOIN Signatories ON Documents.doc_signatory = Signatories.EMP_ID LEFT OUTER JOIN Doctype_Lib ON Documents.doctype_cd = Doctype_Lib.doctype_cd WHERE doc_code = '" & Session("Identifier") & "'"
            Dim conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
            Dim cmd As SqlCommand = New SqlCommand(sql, conn)
            Dim reader As SqlDataReader
            conn.Open()
            reader = cmd.ExecuteReader()

            Dim Identity, Subject, Signatory, FileDate, DocumentType, Availability, Origin, Status, PostedBy As String
            ' check if there are results

            If (reader.Read()) Then
                ' populate the values of the controls

                Identity = reader("doc_code")
                Subject = reader("doc_subject")
                If reader("doc_signatory") = "11-1111" Or reader("doc_signatory") = "99-9999" Then
                    Signatory = reader("doc_signatory_external")
                Else
                    Signatory = reader("FullName")
                End If
                'Publisher = reader("doc_publisher")
                FileDate = reader("doc_date")
                DocumentType = reader("doctype_desc")
                Availability = reader("availability")
                Origin = reader("origin")
                Status = reader("status")
                Try
                    PostedBy = reader("postedby")
                Catch ex As Exception
                    PostedBy = "Old Document"
                End Try
    

                Try
                    'Created the filestream document PDF
                    Dim doc As New Document(iTextSharp.text.PageSize.A4, 10, 10, 42, 35)
                    Dim writer As PdfWriter = PdfWriter.GetInstance(doc, New FileStream(Server.MapPath(".") & "\Attachments\Temp\tempprint.pdf", FileMode.Create))
                    Dim imageFilePath As String = Server.MapPath(".") + "\drimsBannerDocViewer.png"
                    Dim image As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(imageFilePath)
                    Dim para As New Paragraph
                    para.Alignment = Element.ALIGN_CENTER
                    'Resize image depend upon your need  
                    image.ScaleToFit(300.0F, 280.0F)
                    'Give space before image  
                    image.SpacingBefore = 30.0F
                    ' Give some space after the image  
                    image.SpacingAfter = 1.0F
                    image.Alignment = Element.ALIGN_CENTER

                    Title = "Print Details"

                    'TABLE CREATION CODE
                    Dim table As PdfPTable = New PdfPTable(2)
                    'actual width of table in points
                    table.TotalWidth = 570.0F
                    'fix the absolute width of the table
                    table.LockedWidth = True
                    'column widths
                    Dim widths() As Integer = {135.0F, 435.0F}
                    table.SetWidths(widths)
                    table.HorizontalAlignment = 0
                    table.DefaultCell.Border = Rectangle.NO_BORDER
                    'leave a gap before and after the table
                    table.SpacingBefore = 20.0F
                    table.SpacingAfter = 30.0F

                    'Dim cell As PdfPCell = New PdfPCell()
                    'cell.Colspan = 2
                    'cell.HorizontalAlignment = 1
                    'table.AddCell(cell)

                    With table
                        .AddCell("Identifier          :")
                        .AddCell(Identity)
                        .AddCell("Subject            :")
                        .AddCell(Subject)
                        .AddCell("Signatory         :")
                        .AddCell(Signatory)
                        '.AddCell("Publisher         :")
                        '.AddCell(Publisher)
                        .AddCell("Date Created   :")
                        .AddCell(FileDate)
                        .AddCell("Type                :")
                        .AddCell(DocumentType)
                        .AddCell("Source             :")
                        If Session("Availability") = "" Then
                            If Session("Origin") = "External" Then
                                .AddCell("No Link or PDF Attachment Found ")
                            ElseIf Session("Origin") = "Internal" Then
                                .AddCell("No Link or PDF Attachment Found ")
                            End If
                        Else
                            If Session("Origin") = "External" Then
                                .AddCell(Availability)
                            ElseIf Session("Origin") = "Internal" Then
                                .AddCell(Identity + ".pdf")
                            End If
                        End If
                        .AddCell("Origin               :")
                        .AddCell(Origin)
                        .AddCell("Approved for posting by:")
                        .AddCell(PostedBy)
                    End With


                    doc.Open()
                    doc.Add(image)

                    doc.Add(New Phrase(Environment.NewLine))
                    doc.Add(New Phrase(Environment.NewLine))
                    doc.Add(New Phrase(Environment.NewLine))
                    doc.Add(New Paragraph(Title, FontFactory.GetFont(FontFactory.TIMES, 22, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.RED)))
                    'doc.Add(New Phrase(Environment.NewLine))
                    doc.Add(table)
                    doc.Close()
                
                If Session("Origin") = "External" Then
                    'Dim lblWait As Label = CType(FormView2.FindControl("lblWait"), Label)
                    Dim path As String = Server.MapPath(".") & "\Attachments\"
                    Dim array() As String = {"NAMRIA - " + "" + Session("UserName") + "", "" + DateTime.Now() + ""}
                    AddWatermarkDoubleText(Server.MapPath(".") & "\Attachments\Temp\tempprint.pdf", Server.MapPath(".") & "\Attachments\Temp\tempforprint.pdf", array)
                    lblWait.Visible = True
                    pdfViewer.Attributes("src") = "Attachments\Temp\tempforprint.pdf"
                    'pdfViewer.Attributes("src") = "Attachments\tempprint.pdf"
                ElseIf Session("Origin") = "Internal" Then
                    'pdf watermarking
                    'Dim lblWait As Label = CType(FormView2.FindControl("lblWait"), Label)
                    Dim path As String = Server.MapPath(".") & "\Attachments\"
                    Dim array() As String = {"NAMRIA - " + "" + Session("UserName") + "", "" + DateTime.Now() + ""}
                    AddWatermarkDoubleText(Server.MapPath(".") & "\Attachments\Temp\tempprint.pdf", Server.MapPath(".") & "\Attachments\Temp\tempforprint.pdf", array)
                    lblWait.Visible = True
                    pdfViewer.Attributes("src") = "Attachments\Temp\tempforprint.pdf"
                End If
                Catch ex As Exception
                    lblMessage.Visible = True
                    lblMessage.Text = "Failed to display file."
                End Try
            End If
        Finally
            Dim conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
            conn.Close()
        End Try
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        cmdYes.Visible = True
        cmdNo.Visible = True
        lblMessage.Text = "Are you sure you want to deny posting?"
        lblMessage.Visible = True
        btnDelete.Enabled = False
        cmdYes.Focus()
        btnPrint.Enabled = False
        btnApprove.Enabled = False

    End Sub

    Protected Sub cmdNo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNo.Click
        If cmdNo.Text = "OK" Then
            If txtPassword.Text = Session("Password") Then
                'DeletedDocument(Session("Identifier"), Session("UserName"))
                'MsgBox("Document successfully deleted.", MsgBoxStyle.Information, "Success")
                Try
                    Dim sqlDocumentDelete As String = "UPDATE Documents set [status]=@Status WHERE doc_code = @doc_code"
                    Dim connDocumentDelete As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
                    Dim cmdDocumentDelete As SqlCommand = New SqlCommand(sqlDocumentDelete, connDocumentDelete)
                    cmdDocumentDelete.Parameters.AddWithValue("@Status", "Denied-Unread")
                    cmdDocumentDelete.Parameters.AddWithValue("@doc_code", Session("Identifier"))
                    connDocumentDelete.Open()
                    cmdDocumentDelete.ExecuteNonQuery()
                Finally
                    Dim conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
                    conn.Close()
                End Try
                Response.Redirect("~/Document Browse.aspx")
            Else
                'MsgBox("Invalid password.", MsgBoxStyle.Exclamation, "Error")
                lblMessage.Visible = True
                lblMessage.Text = "Invalid password."
                txtPassword.Text = ""
                txtPassword.Focus()
            End If
        Else
            cmdYes.Visible = False
            cmdNo.Visible = False
            lblMessage.Visible = False
            btnDelete.Enabled = True
            btnPrint.Enabled = True
            btnApprove.Enabled = True
        End If
    End Sub

    Protected Sub cmdYes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdYes.Click
        lblMessage.Text = "Enter password :"
        txtPassword.Visible = True
        txtPassword.Focus()
        cmdYes.Visible = False
        cmdNo.Text = "OK"

    End Sub

    Protected Sub txtPassword_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPassword.TextChanged
        cmdNo.Focus()
    End Sub

    
    Protected Sub btnPDF_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim sqlUserActivity As String = "Insert INTO User_Activity_Viewer (UserName,ModuleAccessed,DateAccessed,FileViewed,SearchedWord,DocumentAdded,OriginalDocument,UpdatedDocument,DeletedDocument) Values (@UserName, @ModuleAccessed, @DateAccessed, @SearchedWord, @FileViewed, @DocumentAdded, @OriginalDocument, @UpdatedDocument, @DeletedDocument)"
            Dim connUserActivity As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
            Dim cmdUserActivity As SqlCommand = New SqlCommand(sqlUserActivity, connUserActivity)
            connUserActivity.Open()
            With cmdUserActivity.Parameters
                .AddWithValue("@UserName", Session("UserName"))
                .AddWithValue("@ModuleAccessed", "View Document Details")
                .AddWithValue("@DateAccessed", DateTime.Now())
                .AddWithValue("@SearchedWord", Session("Identifier"))
                .AddWithValue("@FileViewed", "N/A")
                .AddWithValue("@DocumentAdded", "N/A")
                .AddWithValue("@OriginalDocument", "N/A")
                .AddWithValue("@UpdatedDocument", "N/A")
                .AddWithValue("@DeletedDocument", "N/A")
            End With
            cmdUserActivity.ExecuteNonQuery()
        Finally
            Dim conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
            conn.Close()
        End Try
        Response.Redirect("~/ViewDocument.aspx")
    End Sub

    Protected Sub btnClose_Click(sender As Object, e As System.EventArgs)
      
            Response.Redirect("~/Document Browse.aspx")

    End Sub
End Class