Imports System.Data.SqlClient
Imports System.IO
Imports iTextSharp
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports AddWatermark

Partial Class Pages_AdminMainPage
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("UserName") = "" Then
            Response.Redirect("~/AccessDenied.aspx")
        End If

    End Sub

    Protected Sub SqlDataSourceAdmin_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles SqlDataSourceAdmin.Selected
        Dim recount As Integer
        recount = e.AffectedRows

        lblCount.Text = "Total Record(s) : " & Str(recount)

        If recount > 10 Then
            cmdViewAll.Enabled = True
            cmdViewTen.Enabled = True
        End If
    End Sub

    Protected Sub SqlDataSourceAdmin_Updating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceCommandEventArgs) Handles SqlDataSourceAdmin.Updating
        Dim s As String = e.Command.CommandText
    End Sub

    Protected Sub cmdViewAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdViewAll.Click
        GridView1.AllowPaging = False
        cmdViewAll.Visible = False
        cmdViewTen.Visible = True
    End Sub

    Protected Sub cmdViewTen_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdViewTen.Click
        GridView1.AllowPaging = True
        GridView1.AllowPaging = 10
        cmdViewAll.Visible = True
        cmdViewTen.Visible = False
    End Sub

    Protected Sub btnPrintActivity_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrintActivity.Click
        Dim sql As String = "SELECT * FROM User_Activity_Viewer ORDER BY DateAccessed"
        Dim conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
        Dim cmd As SqlCommand = New SqlCommand(sql, conn)
        Dim reader As SqlDataReader
        conn.Open()
        reader = cmd.ExecuteReader()

        ' check if there are results

        'If (reader.Read()) Then
        Try
            'Created the filestream document PDF
            Dim doc As New Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35)
            Dim writer As PdfWriter = PdfWriter.GetInstance(doc, New FileStream(Server.MapPath(".") & "\Attachments\Temp\printactivity.pdf", FileMode.Create))

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
            title = "User Activity Details"


            doc.Open()
            'Write the image in the filestream document PDF
            doc.Add(image)
            doc.Add(New Phrase(Environment.NewLine))
            doc.Add(New Phrase(Environment.NewLine))
            doc.Add(New Phrase(Environment.NewLine))
            doc.Add(New Paragraph(title, FontFactory.GetFont(FontFactory.TIMES, 22, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.RED)))
            doc.Add(New Phrase(Environment.NewLine))


            ' populate the values of the controls
            Do While (reader.Read())

                'Write the text in the filestream document PDF
                doc.Add(New Paragraph("UserName               : " + reader("UserName"), FontFactory.GetFont(FontFactory.TIMES, 12, iTextSharp.text.Font.NORMAL)))
                'doc.Add(New Phrase(Environment.NewLine))
                doc.Add(New Paragraph("Module Accessed    : " + reader("ModuleAccessed"), FontFactory.GetFont(FontFactory.TIMES, 12, iTextSharp.text.Font.NORMAL)))
                'doc.Add(New Phrase(Environment.NewLine))
                doc.Add(New Paragraph("Date Accessed         : " + reader("DateAccessed"), FontFactory.GetFont(FontFactory.TIMES, 12, iTextSharp.text.Font.NORMAL)))
                'doc.Add(New Phrase(Environment.NewLine))
                doc.Add(New Paragraph("Searched Word        : " + reader("SearchedWord"), FontFactory.GetFont(FontFactory.TIMES, 12, iTextSharp.text.Font.NORMAL)))
                'doc.Add(New Phrase(Environment.NewLine))
                doc.Add(New Paragraph("File Viewed             : " + reader("FileViewed"), FontFactory.GetFont(FontFactory.TIMES, 12, iTextSharp.text.Font.NORMAL)))
                'doc.Add(New Phrase(Environment.NewLine))
                doc.Add(New Paragraph("Document Added    : " + reader("DocumentAdded"), FontFactory.GetFont(FontFactory.TIMES, 12, iTextSharp.text.Font.NORMAL)))
                'doc.Add(New Phrase(Environment.NewLine))
                doc.Add(New Paragraph("Original Document : " + reader("OriginalDocument"), FontFactory.GetFont(FontFactory.TIMES, 12, iTextSharp.text.Font.NORMAL)))
                'doc.Add(New Phrase(Environment.NewLine))
                doc.Add(New Paragraph("Updated Document : " + reader("UpdatedDocument"), FontFactory.GetFont(FontFactory.TIMES, 12, iTextSharp.text.Font.NORMAL)))
                'doc.Add(New Phrase(Environment.NewLine))
                doc.Add(New Paragraph("Deleted Document  : " + reader("DeletedDocument"), FontFactory.GetFont(FontFactory.TIMES, 12, iTextSharp.text.Font.NORMAL)))
                'doc.Add(New Phrase(Environment.NewLine))

                doc.Add(New Phrase(Environment.NewLine))
                doc.Add(New Phrase(Environment.NewLine))
            Loop
            doc.Close()


        Catch ex As Exception
            Throw ex
        End Try

        'End If

        Dim path As String = Server.MapPath(".") & "\Attachments\"
        Dim array() As String = {"NAMRIA - " + "" + Session("UserName") + "", "" + DateTime.Now() + ""}
        AddWatermarkDoubleText(Server.MapPath(".") & "\Attachments\Temp\printactivity.pdf", Server.MapPath(".") & "\Attachments\Temp\printactivitywithmark.pdf", array)
        Response.Redirect("~/Attachments/Temp/printactivitywithmark.pdf")
    End Sub

    
End Class
