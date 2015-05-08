

Public Class AddWatermark
    Public Shared Sub AddWatermarkDoubleText(ByVal sourceFile As String, ByVal outputFile As String, ByVal watermarkText() As String, _
                                         Optional ByVal watermarkFont As iTextSharp.text.pdf.BaseFont = Nothing, _
                                         Optional ByVal watermarkFontSize As Single = 48, _
                                         Optional ByVal watermarkFontColor As iTextSharp.text.BaseColor = Nothing, _
                                         Optional ByVal watermarkFontOpacity As Single = 0.3F, _
                                         Optional ByVal watermarkRotation As Single = 45.0F)

        Dim reader As iTextSharp.text.pdf.PdfReader = Nothing
        Dim stamper As iTextSharp.text.pdf.PdfStamper = Nothing
        Dim gstate As iTextSharp.text.pdf.PdfGState = Nothing
        Dim underContent As iTextSharp.text.pdf.PdfContentByte = Nothing
        Dim rect As iTextSharp.text.Rectangle = Nothing
        Dim currentY As Single = 0.0F
        Dim offset As Single = 0.0F
        Dim pageCount As Integer = 0
        Try
            reader = New iTextSharp.text.pdf.PdfReader(sourceFile)
            rect = reader.GetPageSizeWithRotation(1)
            stamper = New iTextSharp.text.pdf.PdfStamper(reader, New System.IO.FileStream(outputFile, IO.FileMode.Create))
            If watermarkFont Is Nothing Then
                watermarkFont = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.COURIER_BOLD, _
                                                              iTextSharp.text.pdf.BaseFont.CP1252, _
                                                              iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
            End If
            If watermarkFontColor Is Nothing Then
                watermarkFontColor = iTextSharp.text.BaseColor.RED
            End If
            gstate = New iTextSharp.text.pdf.PdfGState()
            gstate.FillOpacity = watermarkFontOpacity
            gstate.StrokeOpacity = watermarkFontOpacity
            pageCount = reader.NumberOfPages()
            For i As Integer = 1 To pageCount
                underContent = stamper.GetOverContent(i)
                With underContent
                    .SaveState()
                    .SetGState(gstate)
                    .SetColorFill(watermarkFontColor)
                    .BeginText()
                    .SetFontAndSize(watermarkFont, watermarkFontSize)
                    .SetTextMatrix(30, 30)
                    If watermarkText.Length > 1 Then
                        currentY = (rect.Height / 2) + ((watermarkFontSize * watermarkText.Length) / 2)
                    Else
                        currentY = (rect.Height / 2)
                    End If
                    For j As Integer = 0 To watermarkText.Length - 1
                        If j > 0 Then
                            offset = (j * watermarkFontSize) + (watermarkFontSize / 4) * j
                        Else
                            offset = 0.0F
                        End If
                        .ShowTextAligned(iTextSharp.text.Element.ALIGN_CENTER, watermarkText(j), rect.Width / 2, currentY - offset, watermarkRotation)
                    Next
                    .EndText()
                    .RestoreState()
                End With
            Next
            stamper.Close()
            reader.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class
