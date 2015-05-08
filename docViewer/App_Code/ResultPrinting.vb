Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports iTextSharp
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.Configuration
Imports System


Public Class ResultPrinting

    'Public Shared Sub PrintOnly(ByVal SearchedWord As String, ByVal NewFilePath As String, ByVal ImagePath As String, ByVal Orig As String)

    '    Dim sql As String = "SELECT Documents.doc_code, Documents.doc_subject, Documents.doc_signatory, Documents.doc_signatory_external, Documents.doc_publisher, Documents.doc_date, Documents.availability, Documents.origin, Documents.status, Documents.postedby, [system_owner], Doctype_lib.doctype_desc,  Signatories.LAST_M + ', ' + Signatories.FIRST_M + ' ' + substring(Signatories.MIDDLE_M, 1, 1) + '.' AS FullName FROM Documents LEFT OUTER JOIN Signatories ON Documents.doc_signatory = Signatories.EMP_ID LEFT OUTER JOIN Doctype_Lib ON Documents.doctype_cd = Doctype_Lib.doctype_cd WHERE [status] = 'Approved' AND [doc_keyword] LIKE @Keyword AND [system_owner] = 'D' order by doc_code"
    '    Dim conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
    '    Dim cmd As SqlCommand = New SqlCommand(sql, conn)
    '    cmd.Parameters.AddWithValue("@Keyword", "%" + SearchedWord + "%")
    '    Dim reader As SqlDataReader
    '    conn.Open()
    '    reader = cmd.ExecuteReader()

    '    Dim Identity, Subject, Signatory, Publisher, FileDate, Type, Availability, Origin, Status, PostedBy As String
    '    ' check if there are results


    '    Try
    '        'Created the filestream document PDF
    '        Dim doc As New Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35)
    '        Dim writer As PdfWriter = PdfWriter.GetInstance(doc, New FileStream(NewFilePath, FileMode.Create))

    '        Dim imageFilePath As String = ImagePath
    '        Dim image As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(imageFilePath)
    '        Dim title As String
    '        Dim para As New Paragraph
    '        para.Alignment = Element.ALIGN_CENTER
    '        'Resize image depend upon your need  
    '        image.ScaleToFit(300.0F, 280.0F)
    '        'Give space before image  
    '        image.SpacingBefore = 30.0F
    '        ' Give some space after the image  
    '        image.SpacingAfter = 1.0F
    '        image.Alignment = Element.ALIGN_CENTER
    '        title = "Search Result Details"
    '        'TABLE CREATION CODE
    '        Dim table As PdfPTable = New PdfPTable(2)
    '        'actual width of table in points
    '        table.TotalWidth = 570.0F
    '        'fix the absolute width of the table
    '        table.LockedWidth = True
    '        'column widths
    '        Dim widths() As Integer = {135.0F, 435.0F}
    '        table.SetWidths(widths)
    '        table.HorizontalAlignment = 0
    '        table.DefaultCell.Border = Rectangle.NO_BORDER
    '        'leave a gap before and after the table
    '        table.SpacingBefore = 20.0F
    '        table.SpacingAfter = 30.0F

    '        doc.Open()
    '        'Write the image in the filestream document PDF
    '        doc.Add(image)
    '        doc.Add(New Phrase(Environment.NewLine))
    '        doc.Add(New Phrase(Environment.NewLine))
    '        doc.Add(New Phrase(Environment.NewLine))
    '        doc.Add(New Paragraph(title, FontFactory.GetFont(FontFactory.TIMES, 22, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.RED)))



    '        ' populate the values of the controls
    '        Do While (reader.Read())

    '            Identity = reader("doc_code")
    '            Subject = reader("doc_subject")
    '            If reader("doc_signatory") = "11-1111" Or reader("doc_signatory") = "99-9999" Then
    '                Signatory = reader("doc_signatory_external")
    '            Else
    '                Signatory = reader("FullName")
    '            End If
    '            Publisher = reader("doc_publisher")
    '            FileDate = reader("doc_date")
    '            Type = reader("doctype_desc")
    '            Availability = reader("availability")
    '            Origin = reader("origin")
    '            Status = reader("status")
    '            Try
    '                PostedBy = reader("postedby")
    '            Catch ex As Exception
    '                PostedBy = "Old Document"
    '            End Try

    '            With table
    '                .AddCell("Identifier          :")
    '                .AddCell(Identity)
    '                .AddCell("Subject            :")
    '                .AddCell(Subject)
    '                .AddCell("Signatory         :")
    '                .AddCell(Signatory)
    '                .AddCell("Publisher         :")
    '                .AddCell(Publisher)
    '                .AddCell("Date Created   :")
    '                .AddCell(FileDate)
    '                .AddCell("Type                :")
    '                .AddCell(Type)
    '                .AddCell("Availability       :")
    '                .AddCell(Availability)
    '                .AddCell("Origin               :")
    '                .AddCell(Origin)
    '                .AddCell("Approved for posting by:")
    '                .AddCell(PostedBy)
    '                .AddCell("   ")
    '                .AddCell("   ")
    '                .AddCell("   ")
    '                .AddCell("   ")
    '            End With


    '        Loop

    '        doc.Add(table)
    '        doc.Close()

    '    Catch ex As Exception
    '        Throw ex
    '    End Try

    'End Sub

    Public Shared Sub PrintDate(ByVal startDate As String, ByVal endDate As String, ByVal NewFilePath As String, ByVal ImagePath As String, ByVal Orig As String)
        Dim sql As String = "SELECT Documents.doc_code, Documents.doc_subject, Documents.doc_signatory, Documents.doc_signatory_external, Documents.doc_date, Documents.availability, Documents.origin, Documents.status, Documents.postedby, [system_owner], Doctype_lib.doctype_desc,  Signatories.LAST_M + ', ' + Signatories.FIRST_M + ' ' + substring(Signatories.MIDDLE_M, 1, 1) + '.' AS FullName FROM Documents LEFT OUTER JOIN Signatories ON Documents.doc_signatory = Signatories.EMP_ID LEFT OUTER JOIN Doctype_Lib ON Documents.doctype_cd = Doctype_Lib.doctype_cd WHERE [status] = 'Approved' AND ([doc_date]>=@StartDate AND [doc_date]<=@EndDate) AND [system_owner] = 'D' order by doc_code"
        Dim conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
        Dim cmd As SqlCommand = New SqlCommand(sql, conn)
        cmd.Parameters.AddWithValue("@StartDate", startDate)
        cmd.Parameters.AddWithValue("@EndDate", endDate)
        Dim reader As SqlDataReader
        conn.Open()
        reader = cmd.ExecuteReader()

        Dim Identity, Subject, FileDate, Type As String
        ' check if there are results


        Try
            'Created the filestream document PDF
            Dim doc As New Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35)
            Dim writer As PdfWriter = PdfWriter.GetInstance(doc, New FileStream(NewFilePath, FileMode.Create))

            Dim imageFilePath As String = ImagePath
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



            ' populate the values of the controls
            Do While (reader.Read())
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
                'With table
                '    .AddCell("Identifier          :")
                '    .AddCell(Identity)
                '    .AddCell("Subject            :")
                '    .AddCell(Subject)
                '    .AddCell("Signatory         :")
                '    .AddCell(Signatory)
                '    .AddCell("Publisher         :")
                '    .AddCell(Publisher)
                '    .AddCell("Date Created   :")
                '    .AddCell(FileDate)
                '    .AddCell("Type                :")
                '    .AddCell(Type)
                '    .AddCell("Availability       :")
                '    .AddCell(Availability)
                '    .AddCell("Origin               :")
                '    .AddCell(Origin)
                '    .AddCell("Approved for posting by:")
                '    .AddCell(PostedBy)
                '    .AddCell("   ")
                '    .AddCell("   ")
                '    .AddCell("   ")
                '    .AddCell("   ")
                'End With


            Loop

            doc.Add(table)
            doc.Close()

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Sub PrintSearch(ByVal SearchedWord As String, ByVal NewFilePath As String, ByVal ImagePath As String, ByVal Orig As String)

        Dim sql As String = "SELECT Documents.doc_code, Documents.doc_subject, Documents.doc_signatory, Documents.doc_signatory_external, Documents.doc_date, Documents.availability, Documents.origin, Documents.status, Documents.postedby, [system_owner], Doctype_lib.doctype_desc,  Signatories.LAST_M + ', ' + Signatories.FIRST_M + ' ' + substring(Signatories.MIDDLE_M, 1, 1) + '.' AS FullName FROM Documents LEFT OUTER JOIN Signatories ON Documents.doc_signatory = Signatories.EMP_ID LEFT OUTER JOIN Doctype_Lib ON Documents.doctype_cd = Doctype_Lib.doctype_cd WHERE [status] = 'Approved' AND [doc_subject] LIKE @Keyword AND [system_owner] = 'D' order by doc_code"
        Dim conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
        Dim cmd As SqlCommand = New SqlCommand(sql, conn)
        cmd.Parameters.AddWithValue("@Keyword", "%" + SearchedWord + "%")
        Dim reader As SqlDataReader
        conn.Open()
        reader = cmd.ExecuteReader()

        Dim Identity, Subject, FileDate, Type As String
        ' check if there are results


        Try
            'Created the filestream document PDF
            Dim doc As New Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35)
            Dim writer As PdfWriter = PdfWriter.GetInstance(doc, New FileStream(NewFilePath, FileMode.Create))

            Dim imageFilePath As String = ImagePath
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



            ' populate the values of the controls
            Do While (reader.Read())
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

    End Sub

    Public Shared Sub PrintAllWithDate(ByVal SearchedWord As String, ByVal startDate As String, ByVal endDate As String, ByVal NewFilePath As String, ByVal ImagePath As String, ByVal Orig As String)

        Dim sql As String = "SELECT Documents.doc_code, Documents.doc_subject, Documents.doc_signatory, Documents.doc_signatory_external, Documents.doc_date, Documents.availability, Documents.origin, Documents.status, Documents.postedby, [system_owner], Doctype_lib.doctype_desc,  Signatories.LAST_M + ', ' + Signatories.FIRST_M + ' ' + substring(Signatories.MIDDLE_M, 1, 1) + '.' AS FullName FROM Documents LEFT OUTER JOIN Signatories ON Documents.doc_signatory = Signatories.EMP_ID LEFT OUTER JOIN Doctype_Lib ON Documents.doctype_cd = Doctype_Lib.doctype_cd WHERE [status] = 'Approved' AND [doc_date]>=@StartDate AND [doc_date]<=@EndDate AND [doc_keyword] LIKE @Keyword AND [system_owner] = 'D' order by doc_code"
        Dim conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
        Dim cmd As SqlCommand = New SqlCommand(sql, conn)
        cmd.Parameters.AddWithValue("@Keyword", "%" + SearchedWord + "%")
        cmd.Parameters.AddWithValue("@StartDate", startDate)
        cmd.Parameters.AddWithValue("@EndDate", endDate)
        Dim reader As SqlDataReader
        conn.Open()
        reader = cmd.ExecuteReader()

        Dim Identity, Subject, Signatory, FileDate, Type, Availability, Origin, Status, PostedBy As String
        ' check if there are results


        Try
            'Created the filestream document PDF
            Dim doc As New Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35)
            Dim writer As PdfWriter = PdfWriter.GetInstance(doc, New FileStream(NewFilePath, FileMode.Create))

            Dim imageFilePath As String = ImagePath
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


            ' populate the values of the controls
            Do While (reader.Read())

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
                    .AddCell("Availability       :")
                    .AddCell(Availability)
                    .AddCell("Origin               :")
                    .AddCell(Origin)
                    .AddCell("Approved for posting by:")
                    .AddCell(PostedBy)
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

    End Sub




End Class
