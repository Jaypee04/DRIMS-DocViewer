Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports iTextSharp
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports AddWatermark

Partial Class Pages_Search
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("UserName") = "" Then
            Response.Redirect("~/AccessDenied.aspx")
        End If
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged
        Session("Identifier") = GridView1.SelectedValue
        Dim sql As String = "SELECT status, origin, availability FROM Documents WHERE doc_code='" & GridView1.SelectedValue & "'"
        Dim conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
        Dim cmd As SqlCommand = New SqlCommand(sql, conn)
        Dim reader As SqlDataReader
        conn.Open()
        reader = cmd.ExecuteReader()
        If (reader.Read()) Then
            Session("Status") = reader("status")
            Session("Origin") = reader("origin")
            Session("Availability") = reader("availability")
        End If

        Response.Redirect("~/DocumentDetailsDisplay.aspx")
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Me.GridView1.Visible = True
        SqlDataSource2.DataBind()
        GridView1.AllowPaging = True
        GridView1.AllowPaging = 10


        If txtSearch.Text = "" Then
            lblMessage.Visible = True
            lblMessage.Text = "Please type a keyword."
        Else
            btnPrint.Enabled = True
            lblMessage.Text = ""
            Dim sqlUserActivity As String = "Insert INTO User_Activity_Viewer (UserName,ModuleAccessed,DateAccessed,SearchedWord,FileViewed,DocumentAdded,OriginalDocument,UpdatedDocument,DeletedDocument) Values (@UserName,@ModuleAccessed,@DateAccessed,@SearchedWord,@FileViewed,@DocumentAdded,@OriginalDocument,@UpdatedDocument,@DeletedDocument)"
            Dim connUserActivity As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
            Dim cmdUserActivity As SqlCommand = New SqlCommand(sqlUserActivity, connUserActivity)
            With cmdUserActivity.Parameters
                .AddWithValue("@UserName", Session("UserName"))
                .AddWithValue("@ModuleAccessed", "Search Document")
                .AddWithValue("@DateAccessed", DateTime.Now())
                .AddWithValue("@SearchedWord", txtSearch.Text)
                .AddWithValue("@FileViewed", "N/A")
                .AddWithValue("@DocumentAdded", "N/A")
                .AddWithValue("@OriginalDocument", "N/A")
                .AddWithValue("@UpdatedDocument", "N/A")
                .AddWithValue("@DeletedDocument", "N/A")
            End With
            connUserActivity.Open()
            cmdUserActivity.ExecuteNonQuery()
        End If
    End Sub

    Protected Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        txtSearch.Text = ""
        lblMessage.Text = ""
        SqlDataSource2.DataBind()
    End Sub


    Protected Sub cmdClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Response.Redirect("~/Document Browse.aspx")
    End Sub

    Protected Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Dim sql As String = "SELECT Documents.doc_code, Documents.doc_subject, Documents.doc_signatory, Documents.doc_signatory_external, Documents.doc_publisher,Documents.doc_date, Documents.availability, Documents.origin, Documents.status, Documents.postedby, [system_owner], Doctype_lib.doctype_desc,  Signatories.LAST_M + ', ' + Signatories.FIRST_M + ' ' + substring(Signatories.MIDDLE_M, 1, 1) + '.' AS FullName FROM Documents LEFT OUTER JOIN Signatories ON Documents.doc_signatory = Signatories.EMP_ID LEFT OUTER JOIN Doctype_Lib ON Documents.doctype_cd = Doctype_Lib.doctype_cd WHERE [status] = 'Approved' AND [doc_subject] LIKE @Keyword AND [system_owner] = 'D' order by doc_code"
        Dim conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
        Dim cmd As SqlCommand = New SqlCommand(sql, conn)
        cmd.Parameters.AddWithValue("@Keyword", "%" + txtSearch.Text + "%")
        Dim reader As SqlDataReader
        conn.Open()
        reader = cmd.ExecuteReader()

        Dim Identity, Subject, Signatory, Publisher, Keyword, FileDate, Type, Availability, Origin, Status, PostedBy As String
        ' check if there are results

        If (txtSearch.Text = "") Then
            Response.Redirect("~/Attachments/Error.pdf")
        Else
            Try
                'Created the filestream document PDF
                Dim doc As New Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35)
                Dim writer As PdfWriter = PdfWriter.GetInstance(doc, New FileStream(Server.MapPath(".") & "\Attachments\Temp\printresult.pdf", FileMode.Create))

                Dim imageFilePath As String = Server.MapPath(".") + "\drimsBannerDocViewer.png"
                Dim image As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(imageFilePath)
                Dim title As String
                Dim para As New Paragraph
                para.Alignment = Element.ALIGN_CENTER
                'Resize image depend upon your need  
                image.ScaleToFit(300.0F, 280.0F)
                'Give space before image  
                image.SpacingBefore = 30.0F
                ' Give some space after the image  
                image.SpacingAfter = 1.0F
                image.Alignment = Element.ALIGN_CENTER
                title = "Search Result Details"


                doc.Open()
                'Write the image in the filestream document PDF
                doc.Add(image)
                doc.Add(New Phrase(Environment.NewLine))
                doc.Add(New Phrase(Environment.NewLine))
                doc.Add(New Phrase(Environment.NewLine))



                ' populate the values of the controls
                Do While (reader.Read())

                    Identity = reader("doc_code")
                    Subject = reader("doc_subject")
                    If reader("doc_signatory") = "11-1111" Or reader("doc_signatory") = "99-9999" Then
                        Signatory = reader("doc_signatory_external")
                    Else
                        Signatory = reader("FullName")
                    End If
                    Publisher = reader("doc_publisher")
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





                    doc.Add(New Paragraph(title, FontFactory.GetFont(FontFactory.TIMES, 16, iTextSharp.text.Font.BOLD)))
                    doc.Add(New Phrase(Environment.NewLine))

                    'Write the text in the filestream document PDF
                    doc.Add(New Paragraph("Identifier        : " + Identity, FontFactory.GetFont(FontFactory.TIMES, 12, iTextSharp.text.Font.NORMAL)))
                    'doc.Add(New Phrase(Environment.NewLine))
                    doc.Add(New Paragraph("Subject           : " + Subject, FontFactory.GetFont(FontFactory.TIMES, 12, iTextSharp.text.Font.NORMAL)))
                    'doc.Add(New Phrase(Environment.NewLine))
                    doc.Add(New Paragraph("Signatory       : " + Signatory, FontFactory.GetFont(FontFactory.TIMES, 12, iTextSharp.text.Font.NORMAL)))
                    'doc.Add(New Phrase(Environment.NewLine))
                    doc.Add(New Paragraph("Publisher        : " + Publisher, FontFactory.GetFont(FontFactory.TIMES, 12, iTextSharp.text.Font.NORMAL)))
                    'doc.Add(New Phrase(Environment.NewLine))
                    doc.Add(New Paragraph("Date Created  : " + FileDate, FontFactory.GetFont(FontFactory.TIMES, 12, iTextSharp.text.Font.NORMAL)))
                    'doc.Add(New Phrase(Environment.NewLine))
                    doc.Add(New Paragraph("Type               : " + Type, FontFactory.GetFont(FontFactory.TIMES, 12, iTextSharp.text.Font.NORMAL)))
                    'doc.Add(New Phrase(Environment.NewLine))
                    If reader(7) = "" Then
                        If Session("Origin") = "External" Then
                            doc.Add(New Paragraph("Availability    : No Link or PDF Attachment Found ", FontFactory.GetFont(FontFactory.TIMES, 12, iTextSharp.text.Font.NORMAL)))
                            'doc.Add(New Phrase(Environment.NewLine))
                        ElseIf Session("Origin") = "Internal" Then
                            doc.Add(New Paragraph("Availability    : No Link or PDF Attachment Found", FontFactory.GetFont(FontFactory.TIMES, 12, iTextSharp.text.Font.NORMAL)))
                            'doc.Add(New Phrase(Environment.NewLine))
                        End If
                    Else
                        If Session("Origin") = "External" Then
                            doc.Add(New Paragraph("Availability    : " + Availability, FontFactory.GetFont(FontFactory.TIMES, 12, iTextSharp.text.Font.NORMAL)))
                            'doc.Add(New Phrase(Environment.NewLine))
                        ElseIf Session("Origin") = "Internal" Then
                            doc.Add(New Paragraph("Availability    : " + Identity + ".pdf", FontFactory.GetFont(FontFactory.TIMES, 12, iTextSharp.text.Font.NORMAL)))
                            'doc.Add(New Phrase(Environment.NewLine))
                        End If
                    End If

                    doc.Add(New Paragraph("Origin             : " + Origin, FontFactory.GetFont(FontFactory.TIMES, 12, iTextSharp.text.Font.NORMAL)))
                    'doc.Add(New Phrase(Environment.NewLine))
                    doc.Add(New Paragraph("Approved By  : " + PostedBy, FontFactory.GetFont(FontFactory.TIMES, 12, iTextSharp.text.Font.NORMAL)))
                    doc.Add(New Phrase(Environment.NewLine))
                    doc.Add(New Phrase(Environment.NewLine))
                Loop
                doc.Close()


            Catch ex As Exception
                Throw ex
            End Try

            Dim path As String = Server.MapPath(".") & "\Attachments\"
            Dim array() As String = {"NAMRIA - " + "" + Session("UserName") + "", "" + DateTime.Now() + ""}
            AddWatermarkDoubleText(Server.MapPath(".") & "\Attachments\Temp\printresult.pdf", Server.MapPath(".") & "\Attachments\Temp\printresultwithmark.pdf", array)
            Response.Redirect("~/Attachments/Temp/printresultwithmark.pdf")
        End If
    End Sub

End Class
