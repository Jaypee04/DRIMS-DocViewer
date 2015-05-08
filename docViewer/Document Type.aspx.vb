Imports System.Data.SqlClient
Imports System.IO
Imports iTextSharp
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports AddWatermark

Partial Class Document_Type
    Inherits System.Web.UI.Page

    Protected Sub SqlDataSourceDocType_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles SqlDataSourceDocType.Selected
        Dim recount As Integer
        recount = e.AffectedRows
        lblCount.Text = "Total: " & Str(recount)
    End Sub

    Protected Sub SqlDataSourceDocType_Updating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceCommandEventArgs) Handles SqlDataSourceDocType.Updating
        Dim s As String = e.Command.CommandText
    End Sub

    Protected Sub GridView_Selected(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged
        Dim row As GridViewRow = GridView1.SelectedRow
        Dim key As String = GridView1.SelectedDataKey.Value.ToString
        txtDoc_code.Text = row.Cells(1).Text
        Dim sql As String = "SELECT doctype_cd, doctype_desc, [code] FROM Doctype_Lib WHERE doctype_cd=@DocCode"
        Dim conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
        Dim cmd As SqlCommand = New SqlCommand(sql, conn)
        cmd.Parameters.AddWithValue("@DocCode", txtDoc_code.Text)
        Dim reader As SqlDataReader
        conn.Open()
        reader = cmd.ExecuteReader()
        
        If (reader.Read()) Then
            txtDescription.Text = reader("doctype_desc")
            Try
                txtCode.Text = reader("code")
            Catch ex As Exception
                txtCode.Text = ""
            End Try
            cmdEdit.Enabled = True
            cmdDelete.Enabled = True
            txtDoc_code.ForeColor = Drawing.Color.Orange
        End If
    End Sub

    Protected Sub cmdAdd_Click(sender As Object, e As System.EventArgs) Handles cmdAdd.Click
        GridView1.Enabled = False
        cmdAdd.Visible = False
        cmdDelete.Enabled = False
        cmdUpdate.Enabled = False
        cmdEdit.Enabled = False
        cmdSave.Visible = True
        cmdCancel.Visible = True
        GridView1.Enabled = False
        txtCode.Text = ""
        txtDescription.Text = ""
        txtDoc_code.Text = ""
        txtDoc_code.Enabled = True
        txtDescription.Enabled = True
        txtCode.Enabled = True
        txtDoc_code.Focus()
    End Sub

    Protected Sub cmdSave_Click(sender As Object, e As System.EventArgs) Handles cmdSave.Click
        If txtDoc_code.Text = "" Then
            txtDoc_code.Focus()
        ElseIf txtDescription.Text = "" Then
            txtDescription.Focus()
        ElseIf txtCode.Text = "" Then
            txtCode.Focus()
        Else
            Dim sqlSaveUpdate As String = "INSERT INTO Doctype_Lib(doctype_cd, doctype_desc, code) VALUES(@DocCode, @DocDesc, @code)"
            Dim connSaveUpdate As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
            Dim cmdSaveLib As SqlCommand = New SqlCommand(sqlSaveUpdate, connSaveUpdate)
            With cmdSaveLib.Parameters
                .AddWithValue("@DocCode", txtDoc_code.Text)
                .AddWithValue("@DocDesc", txtDescription.Text)
                .AddWithValue("@code", txtCode.Text)
            End With
            connSaveUpdate.Open()
            cmdSaveLib.ExecuteNonQuery()
            cmdUpdate.Enabled = False
            Response.Redirect("~/Document Type.aspx")
        End If
        
    End Sub

    Protected Sub cmdEdit_Click(sender As Object, e As System.EventArgs) Handles cmdEdit.Click
        cmdDelete.Enabled = False
        cmdEdit.Visible = False
        cmdUpdate.Visible = True
        cmdCancel.Visible = True
        GridView1.Enabled = False
        txtDescription.Enabled = True
        txtDescription.Focus()
        txtCode.Enabled = True
    End Sub

    Protected Sub cmdUpdate_Click(sender As Object, e As System.EventArgs) Handles cmdUpdate.Click
        Dim sqlTypeUpdate As String = "UPDATE Doctype_Lib SET doctype_desc = @DocDesc, code = @code WHERE doctype_cd = @DocCode"
        Dim connTypeUpdate As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
        Dim cmdTypeLib As SqlCommand = New SqlCommand(sqlTypeUpdate, connTypeUpdate)
        With cmdTypeLib.Parameters
            .AddWithValue("@DocCode", txtDoc_code.Text)
            .AddWithValue("@DocDesc", txtDescription.Text)
            .AddWithValue("@code", txtCode.Text)
        End With
        connTypeUpdate.Open()
        cmdTypeLib.ExecuteNonQuery()
        cmdUpdate.Enabled = False
        Response.Redirect("~/Document Type.aspx")

    End Sub

    Protected Sub cmdDelete_Click(sender As Object, e As System.EventArgs) Handles cmdDelete.Click
        Dim sqlTypeDelete As String = "DELETE FROM Doctype_Lib WHERE doctype_cd =@DocCode"
        Dim connTypeDelete As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
        Dim cmdTypeLib As SqlCommand = New SqlCommand(sqlTypeDelete, connTypeDelete)
        cmdTypeLib.Parameters.AddWithValue("@DocCode", txtDoc_code.Text)
        connTypeDelete.Open()
        cmdTypeLib.ExecuteNonQuery()
        Response.Redirect("~/Document Type.aspx")
    End Sub

    Protected Sub cmdCancel_Click(sender As Object, e As System.EventArgs) Handles cmdCancel.Click
        Response.Redirect("~/Document Type.aspx")
    End Sub

    Protected Sub cmdClose_Click(sender As Object, e As System.EventArgs) Handles cmdClose.Click
        Response.Redirect("~/AdminMaintenancePage.aspx")
    End Sub

    Protected Sub btnPrint_Click(sender As Object, e As System.EventArgs) Handles btnPrint.Click
        Dim sql As String = "SELECT * FROM Doctype_Lib"
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
            Dim writer As PdfWriter = PdfWriter.GetInstance(doc, New FileStream(Server.MapPath(".") & "\Attachments\Temp\printtype.pdf", FileMode.Create))

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
            title = "Document Type Details"


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
                'doc.Add(New Paragraph("Employee ID  : " + reader("EmployeeID"), FontFactory.GetFont(FontFactory.TIMES, 12, iTextSharp.text.Font.NORMAL)))
                'doc.Add(New Phrase(Environment.NewLine))
                doc.Add(New Paragraph("Document Code: " + reader("doctype_cd"), FontFactory.GetFont(FontFactory.TIMES, 12, iTextSharp.text.Font.NORMAL)))
                'doc.Add(New Phrase(Environment.NewLine))
                doc.Add(New Paragraph("Description       : " + reader("doctype_desc"), FontFactory.GetFont(FontFactory.TIMES, 12, iTextSharp.text.Font.NORMAL)))
                'doc.Add(New Phrase(Environment.NewLine))
                Try
                    doc.Add(New Paragraph("Code                 : " + reader("code"), FontFactory.GetFont(FontFactory.TIMES, 12, iTextSharp.text.Font.NORMAL)))
                    'doc.Add(New Phrase(Environment.NewLine))
                Catch ex As Exception
                    doc.Add(New Paragraph("Code                 : ", FontFactory.GetFont(FontFactory.TIMES, 12, iTextSharp.text.Font.NORMAL)))
                End Try
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
        AddWatermarkDoubleText(Server.MapPath(".") & "\Attachments\Temp\printtype.pdf", Server.MapPath(".") & "\Attachments\Temp\printtypewithmark.pdf", array)
        Response.Redirect("~/Attachments/Temp/printtypewithmark.pdf")
    End Sub
End Class
