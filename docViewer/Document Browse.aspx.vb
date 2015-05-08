Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports iTextSharp
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports AddWatermark

Partial Class Document_Browse
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles FormView1.Init
        Button4.Visible = False
        If Session("UserName") = "" Then
            Response.Redirect("~/AccessDenied.aspx")
        ElseIf Session("UserLevel") = "1" Or Session("UserLevel") = "2" Then
            btnPrint.Visible = False

            '    If Session("UserLevel") = "4" Then

            '    ElseIf Session("UserLevel") = "1" Then
            '        Label11.Text = "Browse for Documents."
            '    ElseIf Session("UserLevel") = "2" Then
            '        Label11.Text = "Browse for Documents."
            '    ElseIf Session("UserLevel") = "3" Then
            '        Label11.Text = "Browse for Documents."
            '    End If
        End If
    End Sub

    Protected Sub Button4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button4.Click
        GridView1.AllowPaging = True
        GridView1.AllowPaging = 10
        Button3.Visible = True
        Button4.Visible = False
    End Sub
    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
        GridView1.AllowPaging = False
        Button3.Visible = False
        Button4.Visible = True
    End Sub
    
    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged
        Session("Identifier") = GridView1.SelectedValue
        Session("SearchStatus") = "2"
        Try
            Dim sql As String = "SELECT Documents.doc_subject, Documents.status, Documents.origin, Documents.availability, Doctype_Lib.doctype_desc FROM Documents LEFT OUTER JOIN Doctype_Lib ON Documents.doctype_cd = Doctype_Lib.doctype_cd  WHERE doc_code='" & GridView1.SelectedValue & "'"
            Dim conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
            Dim cmd As SqlCommand = New SqlCommand(sql, conn)
            Dim reader As SqlDataReader
            conn.Open()
            reader = cmd.ExecuteReader()
            If (reader.Read()) Then
                Session("Status") = reader("status")
                Session("Origin") = reader("origin")
                Session("Availability") = reader("availability")
                Session("Type") = reader("doctype_desc")
                Session("Doc_Subject") = reader("doc_subject")
            End If
        Finally
            Dim conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
            conn.Close()
        End Try
        Response.Redirect("~/DocumentDetailsDisplay.aspx")
    End Sub

    'Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
    '    Response.Redirect("~/Pages/DocumentDetailsDisplay.aspx")
    '    'Response.Redirect("~/Pages/DocumentDetails.aspx")
    'End Sub
    
    'Protected Sub InsertButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles InsertButton.Click
    
    'End Sub

    Protected Sub SqlDataSource2_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles SqlDataSource2.Selected
        Dim reccount As Integer
        reccount = e.AffectedRows
        
        Label9.Text = "Total Record(s) : " & Str(reccount)

        If reccount > 10 Then
            Button3.Enabled = True
            Button4.Enabled = True
        End If
    End Sub

    Protected Sub SqlDataSource2_Updating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceCommandEventArgs) Handles SqlDataSource2.Updating
        Dim s As String = e.Command.CommandText
    End Sub

    Protected Sub NewButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Session("AddTime") = DateTime.Now()
            Dim sqlUserActivity As String = "Insert INTO User_Activity_Viewer (UserName,ModuleAccessed,DateAccessed,SearchedWord,FileViewed,DeletedDocument) Values ( " & "'" & Session("UserName") & "','Save a Document'," & "'" & Session("Addtime") & "', 'N/A', 'N/A', 'N/A')"
            Dim connUserActivity As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
            Dim cmdUserActivity As SqlCommand = New SqlCommand(sqlUserActivity, connUserActivity)
            connUserActivity.Open()
            cmdUserActivity.ExecuteNonQuery()
        Finally
            Dim conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
            conn.Close()
        End Try
        Response.Redirect("~/Documents.aspx")
    End Sub

    Protected Sub InsertButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim IdentifierTextBox As TextBox = CType(FormView1.FindControl("IdentifierTextBox"), TextBox)
            Dim sqlUserActivity As String = "UPDATE User_Activity_Viewer SET DocumentAdded = " & "'" & IdentifierTextBox.Text & "'" & " WHERE DateAccessed = " & " '" & Session("AddTime") & "'" & ""
            Dim connUserActivity As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
            Dim cmdUserActivity As SqlCommand = New SqlCommand(sqlUserActivity, connUserActivity)
            connUserActivity.Open()
            cmdUserActivity.ExecuteNonQuery()
        Finally
            Dim conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
            conn.Close()
        End Try
    End Sub

    Protected Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            Dim sql As String = "SELECT Documents.doc_code, Documents.doc_subject, Documents.doc_signatory, Documents.doc_signatory_external, Documents.doc_publisher, Documents.doc_date, Documents.availability, Documents.origin, Documents.status, Documents.postedby, Doctype_lib.doctype_desc,  Signatories.LAST_M + N', ' + Signatories.FIRST_M + N' ' + substring(Signatories.MIDDLE_M, 1, 1) + '.' AS FullName FROM Documents LEFT OUTER JOIN Signatories ON Documents.doc_signatory = Signatories.EMP_ID LEFT OUTER JOIN Doctype_Lib ON Documents.doctype_cd = Doctype_Lib.doctype_cd WHERE [system_owner] = 'D' order by doc_code"
            Dim conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
            Dim cmd As SqlCommand = New SqlCommand(sql, conn)
            Dim reader As SqlDataReader
            conn.Open()
            reader = cmd.ExecuteReader()

            Dim Identity, Subject, Signatory, Publisher, FileDate, Type, Availability, Origin, Status, PostedBy As String
            ' check if there are results

            Try
                'Created the filestream document PDF
                Dim doc As New Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35)
                Dim writer As PdfWriter = PdfWriter.GetInstance(doc, New FileStream(Server.MapPath(".") & "\Attachments\Temp\printall.pdf", FileMode.Create))

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
                title = "Print Details"

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

                doc.Open()
                'Write the image in the filestream document PDF
                doc.Add(image)
                doc.Add(New Phrase(Environment.NewLine))
                doc.Add(New Phrase(Environment.NewLine))
                doc.Add(New Phrase(Environment.NewLine))
                doc.Add(New Paragraph(title, FontFactory.GetFont(FontFactory.TIMES, 22, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.RED)))



                Do While (reader.Read)

                    Dim num As Integer
                    Identity = reader("doc_code")
                    Subject = reader("doc_subject")

                    FileDate = reader("doc_date")
                    Type = reader("doctype_desc")

                    num = num + 1
                    With table
                        .AddCell(num & ". Identifier          ")
                        .AddCell(Identity)
                        .AddCell("      Subject            ")
                        .AddCell(Subject)
                        .AddCell("      Date Created   ")
                        .AddCell(FileDate)
                        .AddCell("      Type                ")
                        .AddCell(Type)
                        .AddCell("   ")
                        .AddCell("   ")
                        .AddCell("   ")
                        .AddCell("   ")
                    End With


                Loop

                doc.Add(table)
                doc.Close()

            Catch ex As Exception
                Throw ex
            End Try
        Finally
            Dim conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
            conn.Close()
        End Try


        Dim path As String = Server.MapPath(".") & "\Attachments\"
        Dim array() As String = {"NAMRIA - " + "" + Session("UserName") + "", "" + DateTime.Now() + ""}
        AddWatermarkDoubleText(Server.MapPath(".") & "\Attachments\Temp\printall.pdf", Server.MapPath(".") & "\Attachments\Temp\printallwithcontrol.pdf", array)
        Response.Redirect("~/Attachments/Temp/printallwithcontrol.pdf")


    End Sub

    
End Class