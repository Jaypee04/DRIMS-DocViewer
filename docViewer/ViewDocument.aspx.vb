Imports System.IO
Imports System.Data.SqlClient
Imports iTextSharp
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports AddWatermark
Imports Delete


Partial Class ViewDocument
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblType.Text = "" & Session("Type")
        lblSubject.Text = "" & Session("Doc_Subject")
        If Session("UserLevel") = "1" Or Session("UserLevel") = "2" Then
            btnPrint.Visible = False
            btnDelete.Visible = False
        ElseIf Session("UserLevel") = "3" Then
            btnDelete.Visible = True
        ElseIf Session("UserLevel") = "5" Then
            btnDelete.Visible = True
        End If

        If Session("Origin") = "External" Then
            'Dim id As String
            'id = Session("Identifier")
            Dim pdfSource As String
            pdfSource = Session("Availability")
            'Dim sample As String = String.Concat(Session("FirstName")) + ". " + Session("LastName")
            Dim path As String = Server.MapPath(".") & "\Attachments\"
            Dim nameko As String
            nameko = Session("FirstName") & ". " & Session("LastName")
            Dim array() As String = {"" + nameko + "", "" + Session("Department") + "", "" + DateTime.Now() + ""}
            Try
                AddWatermarkDoubleText(pdfSource, Server.MapPath(".") & "\Attachments\Temp\temp.pdf", array)
                pdfViewer.Attributes("src") = "Attachments\Temp\temp.pdf"
            Catch ex As Exception
                Response.Redirect("~/Error.aspx")
            End Try
            
        ElseIf Session("Origin") = "Internal" Then
            Dim id As String
            id = Session("Identifier")
            'Dim sample As String = String.Concat(Session("FirstName"), 1) + ". " + Session("LastName")
            Dim path As String = Server.MapPath(".") & "\Attachments\"
            'Dim path As String = Directory.GetParent(Server.MapPath("")).FullName & "\DRIMS\Attachments\"
            'Dim array() As String = {"NAMRIA - " + "" + Session("UserName") + "", "" + DateTime.Now() + ""}
            Dim nameko As String
            nameko = Session("FirstName") & ". " & Session("LastName")
            Dim array() As String = {"" + nameko + "", "" + Session("Department") + "", "" + DateTime.Now() + ""}
            AddWatermarkDoubleText(path + id + ".pdf", Server.MapPath(".") & "\Attachments\Temp\" + id + "Temp.pdf", Array)
            pdfViewer.Attributes("src") = "Attachments\Temp\" + id + "Temp.pdf"
        End If

        If Session("SearchStatus") = "1" Then
            btnClose.Visible = False
            Session("SearchStatus") = ""
        ElseIf Session("SearchStatus") = "2" Then
            btnClose.Visible = True
            Session("SearchStatus") = ""
        End If
        If Session("ForApprove") = "1" Then
            btnClose.Visible = False
            btnDelete.Visible = False
            btnPrint.Visible = False
            Session("ForApprove") = ""
        End If
    End Sub

    Protected Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Response.Redirect("~/DocumentDetailsDisplay.aspx")

        If Session("Origin") = "Internal" Then
            Dim FileToDelete1 As New FileInfo(Server.MapPath(".") & "\Attachments\Temp\" + Session("Identifier") + "Temp.pdf")
            Dim FileToDelete2 As New FileInfo(Server.MapPath(".") & "\Attachments\Temp\" + Session("Identifier") + "forprint.pdf")
            FileToDelete1.Delete()
            FileToDelete2.Delete()
        ElseIf Session("Origin") = "External" Then
            Dim FileToDelete3 As New FileInfo(Server.MapPath(".") & "\Attachments\Temp\temp.pdf")
            Dim FileToDelete4 As New FileInfo(Server.MapPath(".") & "\Attachments\Temp\tempforprint.pdf")
            FileToDelete3.Delete()
            FileToDelete4.Delete()

        End If

    End Sub


    Protected Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            Dim sql As String = "SELECT Documents.doc_code, Documents.doc_subject, Documents.doc_signatory, Documents.doc_signatory_external, Documents.doc_date, Documents.availability, Documents.origin, Documents.status, Documents.postedby, Doctype_lib.doctype_desc,  Signatories.LAST_M + ', ' + Signatories.FIRST_M + ' ' + substring(Signatories.MIDDLE_M, 1, 1) + '.' AS FullName FROM Documents LEFT OUTER JOIN Signatories ON Documents.doc_signatory = Signatories.EMP_ID LEFT OUTER JOIN Doctype_Lib ON Documents.doctype_cd = Doctype_Lib.doctype_cd WHERE doc_code = '" & Session("Identifier") & "'"
            Dim conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
            Dim cmd As SqlCommand = New SqlCommand(sql, conn)
            Dim reader As SqlDataReader
            conn.Open()
            reader = cmd.ExecuteReader()

            Dim Identity, Subject, Signatory, FileDate, Type, Availability, Origin, Status, PostedBy As String
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
                Type = reader("doctype_desc")
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
                        .AddCell(Type)
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

                        pdfViewer.Attributes("src") = "Attachments\Temp\tempforprint.pdf"
                        'pdfViewer.Attributes("src") = "Attachments\tempprint.pdf"
                    ElseIf Session("Origin") = "Internal" Then
                        'pdf watermarking
                        'Dim lblWait As Label = CType(FormView2.FindControl("lblWait"), Label)
                        Dim path As String = Server.MapPath(".") & "\Attachments\"
                        Dim array() As String = {"NAMRIA - " + "" + Session("UserName") + "", "" + DateTime.Now() + ""}
                        AddWatermarkDoubleText(Server.MapPath(".") & "\Attachments\Temp\tempprint.pdf", Server.MapPath(".") & "\Attachments\Temp\" + Identity + "forprint.pdf", array)

                        pdfViewer.Attributes("src") = "Attachments\Temp\" + Identity + "forprint.pdf"
                    End If
                Catch ex As Exception
                    Throw ex
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
        lblMessage.Text = "Are you sure you want to delete?"
        lblMessage.Visible = True
        btnDelete.Enabled = False
        cmdYes.Focus()
        btnPrint.Enabled = False

    End Sub

    Protected Sub cmdNo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNo.Click
        If cmdNo.Text = "OK" Then
            If txtPassword.Text = Session("Password") Then
                DeletedDocument(Session("Identifier"), Session("UserName"))
                'MsgBox("Document successfully deleted.", MsgBoxStyle.Information, "Success")
                Try
                    Dim sqlDocumentDelete As String = "DELETE FROM Documents WHERE doc_code = '" & Session("Identifier") & "'"
                    Dim connDocumentDelete As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
                    Dim cmdDocumentDelete As SqlCommand = New SqlCommand(sqlDocumentDelete, connDocumentDelete)
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
        End If
    End Sub

    Protected Sub cmdYes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdYes.Click
        lblMessage.Text = "Enter password :"
        txtPassword.Visible = True
        txtPassword.Focus()
        cmdYes.Visible = False
        cmdNo.Text = "OK"

    End Sub


End Class
