Imports System.Data.SqlClient
Imports System.IO
Imports Delete

Partial Class Pages_Documents
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect("~/AccessDenied.aspx")
            End If
            lblDateFormat.Text = Format(Date.Now.Month, "00") & "-" & Format(Date.Now.Day, "00") & "-" & Date.Now.Year & "-"
            lblSign.Text = "99-9999"
            If Session("UserLevel") = "3" Then
                GrdVwPending.Visible = True
                btnPost.Visible = True
            ElseIf Session("UserLevel") = "1" Or Session("UserLevel") = "2" Then
                btnPost.Visible = False
            End If

            txtValue.Text = "Internal"

            FileUpload1.Enabled = True
            FileUpload1.Visible = True
            btnUpload.Enabled = True
            btnUpload.Visible = True

            'lblSelect.Visible = False
            txtAvailability.Enabled = False
            txtAvailability.Text = "N/A"
            txtAvailability.Visible = False
            cmdSave.Enabled = False

            drpdwnlstDivision.Visible = True
            drpdwnlstSignatory.Visible = True
            lblNA1.Visible = False
            txtSignatory.Visible = False
            FileUpload1.Enabled = False
            btnUpload.Enabled = False
            txtAgency.Text = "NAMRIA"
            drpdwnlstAgency.SelectedValue = "NAMRIA"
        End If
    End Sub

    Protected Sub SqlDataSourceDocuments_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles SqlDataSourceDocuments.Selected
        Dim recount As Integer
        recount = e.AffectedRows
        lblCount.Text = "Total Record(s) Waiting for Posting : " & Str(recount)
    End Sub

    Protected Sub SqlDataSourceDocuments_Updating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceCommandEventArgs) Handles SqlDataSourceDocuments.Updating
        Dim s As String = e.Command.CommandText
    End Sub

    Function getlastdoc() As Integer
        Try
            Dim ssql As String = "select isnull(max(substring(doc_code,18,4)),'0000') as seq from Documents where substring(doc_code,7,2)=substring('" & lblDateFormat.Text & "' ,1,2) and substring(doc_code,13,4)=substring('" & lblDateFormat.Text & "' ,7,4)"
            Dim conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
            Dim cmd As SqlCommand = New SqlCommand(ssql, conn)
            conn.Open()
            Dim retVal As Integer = CType(cmd.ExecuteScalar, Integer)
            Return retVal
        Finally
            Dim conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
            conn.Close()
        End Try

    End Function

    Protected Sub EnableAll()
        txtIdentifier.Enabled = True
        txtSubject.Enabled = True
        txtSignatory.Enabled = True
        'txtPublisher.Enabled = True
        'txtKeyword.Enabled = True
        txtType.Enabled = True
        txtAvailability.Enabled = True
        txtDate.Enabled = True
        'drpdwnlstOrigin.Enabled = True
        'drpdwnlstOrigin.SelectedValue = "Select"
        lblPress.Visible = True
        txtIdentifier.Focus()
        drpdwnlstType.Enabled = True
        drpdwnlstSignatory.Enabled = True
        drpdwnlstAgency.Enabled = True

    End Sub

    Protected Sub ClearAll()
        txtIdentifier.Text = ""
        txtSubject.Text = ""
        txtSignatory.Text = ""
        'txtPublisher.Text = ""
        'txtKeyword.Text = ""
        txtType.Text = ""
        txtDate.Text = ""
        txtAvailability.Text = "http://"
        'drpdwnlstOrigin.SelectedValue = "Select"
        drpdwnlstType.DataBind()
        drpdwnlstSignatory.DataBind()
        drpdwnlstAgency.DataBind()
        drpdwnlstDivision.DataBind()
        lblUploaded.Visible = False
        lblPress.Visible = False
    End Sub

    Protected Sub SaveDocument()
        Try
            If Session("UserLevel") = "1" Or Session("UserLevel") = "2" Then
                Dim sqlDocumentSave As String = "INSERT INTO Documents (doc_code, doc_agency, doc_div, doc_subject, doc_signatory, doc_signatory_external, doc_date, doctype_cd, availability, status, origin, user_id, [system_owner]) VALUES (@Identifier, @Agency, @Division, @Subject, @Signatory, @SignatoryEx, @Date, @Type, @Availability, @Status , @Origin, @Encoder, @Own)"
                Dim connDocumentSave As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
                Dim cmdDocument As SqlCommand = New SqlCommand(sqlDocumentSave, connDocumentSave)
                With cmdDocument.Parameters
                    .AddWithValue("@Identifier", txtIdentifier.Text)
                    .AddWithValue("@Agency", txtAgency.Text)
                    .AddWithValue("@Division", txtDivision.Text)
                    .AddWithValue("@Subject", txtSubject.Text)
                    .AddWithValue("@Signatory", lblSign.Text)
                    If lblSign.Text = "11-1111" Or lblSign.Text = "99-9999" Or lblSign.Text = "00-0000" Then
                        .AddWithValue("@SignatoryEx", txtSignatory.Text)
                    Else
                        .AddWithValue("@SignatoryEx", "N/A")
                    End If
                    '.AddWithValue("@Publisher", txtPublisher.Text)
                    '.AddWithValue("@Keyword", txtKeyword.Text)
                    .AddWithValue("@Date", lblIdentifierCode.Text)
                    .AddWithValue("@Type", txtType.Text)
                    .AddWithValue("@Availability", txtAvailability.Text)
                    .AddWithValue("@Status", "For Approval")
                    .AddWithValue("@Origin", txtValue.Text)
                    .AddWithValue("@Encoder", Session("UserName"))
                    .AddWithValue("@Own", "D")
                End With
                connDocumentSave.Open()
                cmdDocument.ExecuteNonQuery()
            ElseIf Session("UserLevel") = "3" Then
                Dim sqlDocumentSave As String = "INSERT INTO Documents (doc_code, doc_agency, doc_div, doc_subject, doc_signatory, doc_signatory_external, doc_date, doctype_cd, availability, status, origin, user_id, postedby, [system_owner]) VALUES (@Identifier, @Agency, @Division, @Subject, @Signatory, @SignatoryEx, @Date, @Type, @Availability, @Status , @Origin, @Encoder, @PostedBy, @Own)"
                Dim connDocumentSave As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
                Dim cmdDocument As SqlCommand = New SqlCommand(sqlDocumentSave, connDocumentSave)
                With cmdDocument.Parameters
                    .AddWithValue("@Identifier", txtIdentifier.Text)
                    .AddWithValue("@Agency", txtAgency.Text)
                    .AddWithValue("@Division", txtDivision.Text)
                    .AddWithValue("@Subject", txtSubject.Text)
                    .AddWithValue("@Signatory", lblSign.Text)
                    If lblSign.Text = "11-1111" Or lblSign.Text = "99-9999" Then
                        .AddWithValue("@SignatoryEx", txtSignatory.Text)
                    Else
                        .AddWithValue("@SignatoryEx", "N/A")
                    End If
                    '.AddWithValue("@Publisher", txtPublisher.Text)
                    '.AddWithValue("@Keyword", txtKeyword.Text)
                    .AddWithValue("@Date", txtDate.Text)
                    .AddWithValue("@Type", txtType.Text)
                    .AddWithValue("@Availability", txtAvailability.Text)
                    .AddWithValue("@Status", "Approved")
                    .AddWithValue("@Origin", txtValue.Text)
                    .AddWithValue("@Encoder", Session("UserName"))
                    .AddWithValue("@PostedBy", Session("UserName"))
                    .AddWithValue("@Own", "D")
                End With
                connDocumentSave.Open()
                cmdDocument.ExecuteNonQuery()
            End If

            'Catch e As SqlException
            '    MsgBox("Some fields contains invalid character.", MsgBoxStyle.Critical, "Error")
            'End Try

            Dim sqlUserActivity As String = "Insert INTO User_Activity_Viewer (UserName, ModuleAccessed, DateAccessed, SearchedWord, FileViewed, DocumentAdded, OriginalDocument, UpdatedDocument, DeletedDocument) Values (@UserName, @ModuleAccessed, @DateAccessed, @SearchedWord, @FileViewed, @DocumentAdded, @OriginalDocument, @UpdatedDocument, @DeletedDocument)"
            Dim connUserActivity As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
            Dim cmdUserActivity As SqlCommand = New SqlCommand(sqlUserActivity, connUserActivity)
            With cmdUserActivity.Parameters
                .AddWithValue("@UserName", Session("UserName"))
                .AddWithValue("@ModuleAccessed", "Save a Document")
                .AddWithValue("@DateAccessed", DateTime.Now())
                .AddWithValue("@SearchedWord", "N/A")
                .AddWithValue("@FileViewed", "N/A")
                .AddWithValue("@DocumentAdded", txtIdentifier.Text)
                .AddWithValue("@OriginalDocument", "N/A")
                .AddWithValue("@UpdatedDocument", "N/A")
                .AddWithValue("@DeletedDocument", "N/A")
            End With
            connUserActivity.Open()
            cmdUserActivity.ExecuteNonQuery()
        Finally
            Dim conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
            conn.Close()
        End Try
        Response.Redirect("~/Documents.aspx")
    End Sub

    Protected Sub GridView_Selected(ByVal sender As Object, ByVal e As System.EventArgs) Handles GrdVwDocuments.SelectedIndexChanged

        Dim row As GridViewRow = GrdVwDocuments.SelectedRow
        Dim key As String = GrdVwDocuments.SelectedDataKey.Value.ToString
        txtIdentifier.Text = GrdVwDocuments.SelectedValue
        Try
            Dim sql As String = "SELECT Documents.doc_code, Documents.doc_agency, Documents.doc_div, Documents.doc_subject, Documents.doc_date, Documents.doc_signatory, Documents.doc_signatory_external, Documents.doc_date, Documents.availability, Documents.origin, Documents.status, Documents.postedby, Documents.doctype_cd, Doctype_lib.doctype_desc, Agency_Lib.agency_nm, Division_Library.Division_Name, Signatories.LAST_M + ', ' +Signatories.LAST_M + ', ' + Signatories.FIRST_M + ' ' + substring(Signatories.MIDDLE_M, 1, 1) + '.' AS FullName FROM Documents LEFT OUTER JOIN Division_Library ON Documents.doc_div = Division_Library.Division_Code LEFT OUTER JOIN Agency_Lib ON Documents.doc_agency = Agency_Lib.agency_cd LEFT OUTER JOIN Signatories ON Documents.doc_signatory = Signatories.EMP_ID LEFT OUTER JOIN Doctype_Lib ON Documents.doctype_cd = Doctype_Lib.doctype_cd WHERE doc_code=@Identifier"
            Dim conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
            Dim cmd As SqlCommand = New SqlCommand(sql, conn)
            cmd.Parameters.AddWithValue("@Identifier", txtIdentifier.Text)
            Dim reader As SqlDataReader
            conn.Open()
            reader = cmd.ExecuteReader()
            ' check if there are results

            If (reader.Read()) Then
                ' populate the values of the controls
                txtAgency.Text = reader("doc_agency")
                drpdwnlstAgency.SelectedValue = reader("doc_agency")

                txtDivision.Text = reader("doc_div")
                drpdwnlstDivision.SelectedValue = reader("doc_div")


                If reader("doc_signatory") = "11-1111" Or reader("doc_signatory") = "99-9999" Or reader("doc_signatory") = "00-0000" Then
                    txtSignatory.Visible = True
                    txtSignatory.Text = reader("doc_signatory_external")
                    drpdwnlstSignatory.SelectedValue = reader("doc_signatory")
                Else
                    drpdwnlstSignatory.SelectedValue = reader("doc_signatory")
                    txtSignatory.Text = "Not Applicable"
                    txtSignatory.Visible = False
                End If

                drpdwnlstType.SelectedValue = reader("doctype_cd")
                txtType.Text = reader("doctype_cd")
                txtSubject.Text = reader("doc_subject")
                txtDate.Text = reader("doc_date")

                'txtPublisher.Text = reader("doc_publisher")
                'txtKeyword.Text = reader("doc_keyword")



                txtAvailability.Text = reader("availability")
                txtValue.Text = reader("origin")
                Session("Status") = reader("status")
                lblIdentifierCode.Text = txtIdentifier.Text
            End If
            If txtValue.Text = "External" Then
                txtAvailability.Visible = True
                lblAvailability.Enabled = True
                lblAvailability.Visible = True
                rdbtnExternal.Checked = True

            End If
        Finally
            Dim conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
            conn.Close()
        End Try
        cmdEdit.Enabled = True
        cmdDelete.Enabled = True
        txtIdentifier.ForeColor = Drawing.Color.Orange

    End Sub

    
    Protected Sub cmdAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        EnableAll()
        ClearAll()
        'txtDivision.Text = "9000"
        FileUpload1.Enabled = True
        btnUpload.Enabled = True
        txtIdentifier.Enabled = False
        GrdVwDocuments.Enabled = False
        cmdAdd.Visible = False
        cmdDelete.Enabled = False
        cmdUpdate.Enabled = False
        cmdEdit.Enabled = False
        cmdSave.Visible = True
        cmdSave.Enabled = False
        cmdCancel.Visible = True
        GrdVwDocuments.Enabled = False
        lblIdentifierCode.Text = DateTime.Now()
        txtAgency.Text = "NAMRIA"
        drpdwnlstAgency.SelectedValue = "NAMRIA"
        drpdwnlstDivision.Enabled = True
        drpdwnlstDivision.SelectedValue = "1000"
        drpdwnlstSignatory.SelectedValue = "01-0019"
        lblNA1.Visible = False
        drpdwnlstDivision.Visible = True
        drpdwnlstSignatory.Visible = True
        txtSignatory.Visible = False
        rdbtnExternal.Enabled = True
        rdbtnInternal.Enabled = True
        txtIdentifier.Text = "I" + "-" + txtType.Text + "-" + lblDateFormat.Text + Format(getlastdoc() + 1, "0000").ToString

    End Sub

    Protected Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        lblMessage.Visible = True
        lblMessage.Text = ""
        If txtIdentifier.Text = "" Then
            lblMessage.Text = "Please input Identifier."
            txtIdentifier.Focus()
        ElseIf txtSubject.Text = "" Then
            lblMessage.Text = "Please input Subject."
            txtSubject.Focus()
        ElseIf txtDate.Text = "" Then
            lblMessage.Text = "Please input Date."
            txtDate.Focus()
        ElseIf drpdwnlstSignatory.Text = "" Then
            lblMessage.Text = "Please choose Signatory."
            drpdwnlstSignatory.Focus()
            'ElseIf txtPublisher.Text = "" Then
            '    lblMessage.Text = "Please input Publisher."
            '    txtPublisher.Focus()
            'ElseIf txtKeyword.Text = "" Then
            '    lblMessage.Text = "Please input Keyword."
            '    txtKeyword.Focus()
        ElseIf drpdwnlstType.Text = "" Then
            lblMessage.Text = "Please choose type."
            drpdwnlstType.Focus()

            'ElseIf txtAvailability.Text = "" Then
            '    lblMessage.Text = "Please input Availability."
            '    txtAvailability.Focus()
        Else
            If txtIdentifier.Text <> "" And txtSubject.Text <> "" And txtDate.Text <> "" And txtType.Text <> "" Then 'And txtPublisher.Text <> "" And txtAvailability.Text <> ""
                SaveDocument()
                'Response.Redirect("~/Pages/Documents.aspx")
            End If
        End If
    End Sub

    Protected Sub cmdEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdEdit.Click
        cmdDelete.Enabled = False
        cmdEdit.Visible = False
        cmdUpdate.Visible = True
        cmdCancel.Visible = True
        GrdVwDocuments.Enabled = False
        txtIdentifier.Enabled = True
        txtSubject.Enabled = True
        txtSignatory.Enabled = False
        'txtPublisher.Enabled = True
        'txtKeyword.Enabled = True
        txtDate.Enabled = True
        txtType.Enabled = True
        txtAvailability.Enabled = True
        drpdwnlstType.Enabled = False
        drpdwnlstAgency.Enabled = False
        drpdwnlstDivision.Enabled = False
        drpdwnlstSignatory.Enabled = False

        'drpdwnlstOrigin.Enabled = False
        txtIdentifier.Enabled = False

        txtSubject.Focus()
        lblPress.Visible = False

        Dim str As String
        str = txtIdentifier.Text
        str = str.Substring(0, 1)
        If str = "I" Then
            lblAvailability.Visible = True
            FileUpload1.Visible = True
            btnUpload.Visible = True
            FileUpload1.Enabled = True
            btnUpload.Enabled = True
        ElseIf str = "E" Then
            lblAvailability.Visible = True
            txtAvailability.Enabled = True
        End If

    End Sub

    Protected Sub cmdDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
        lblMessage.Text = "Are you sure you want to delete this entry?"
        cmdEdit.Enabled = False
        cmdCancel.Visible = True
        cmdDelete.Enabled = False
        GrdVwDocuments.Enabled = False
        lblMessage.Visible = True
        cmdYes.Visible = True
        cmdNo.Visible = True
    End Sub

    Protected Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Response.Redirect("~/Documents.aspx")
    End Sub

    Protected Sub cmdUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUpdate.Click
        txtSignatory.Visible = False
        Try
            Dim sqlDocumentUpdate As String = "UPDATE Documents SET doc_agency = @Agency, doc_div = @Division, doc_subject = @Subject, availability = @Availability, doc_date=@docdate WHERE doc_code = @Identifier"
            Dim connDocumentUpdate As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
            Dim cmdDocument As SqlCommand = New SqlCommand(sqlDocumentUpdate, connDocumentUpdate)
            With cmdDocument.Parameters
                .AddWithValue("@Identifier", txtIdentifier.Text)
                .AddWithValue("@Agency", txtAgency.Text)
                .AddWithValue("@Division", txtDivision.Text)
                .AddWithValue("@Subject", txtSubject.Text)
                '.AddWithValue("@Signatory", lblSign.Text)
                'If lblSign.Text = "11-1111" Or lblSign.Text = "99-9999" Then
                '    .AddWithValue("@SignatoryEx", txtSignatory.Text)
                'Else
                '    .AddWithValue("@SignatoryEx", "N/A")
                'End If
                '.AddWithValue("@Publisher", txtPublisher.Text)
                '.AddWithValue("@Keyword", txtKeyword.Text)
                '.AddWithValue("@Type", txtType.Text)
                .AddWithValue("@Availability", txtAvailability.Text)
                .AddWithValue("@docdate", txtDate.Text)
            End With
            connDocumentUpdate.Open()
            cmdDocument.ExecuteNonQuery()
            cmdUpdate.Enabled = False

            Dim sqlUserActivity As String = "Insert INTO User_Activity_Viewer (UserName,ModuleAccessed,DateAccessed,SearchedWord,FileViewed, DocumentAdded, OriginalDocument, UpdatedDocument, DeletedDocument) Values (@UserName, @ModuleAccessed, @DateAccessed, @SearchedWord, @FileViewed, @DocumentAdded, @OriginalDocument, @UpdatedDocument, @DeletedDocument)"
            Dim connUserActivity As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
            Dim cmdUserActivity As SqlCommand = New SqlCommand(sqlUserActivity, connUserActivity)
            With cmdUserActivity.Parameters
                .AddWithValue("@UserName", Session("UserName"))
                .AddWithValue("@ModuleAccessed", "Update Document")
                .AddWithValue("@DateAccessed", DateTime.Now())
                .AddWithValue("@SearchedWord", "N/A")
                .AddWithValue("@FileViewed", lblIdentifierCode.Text)
                .AddWithValue("@DocumentAdded", "N/A")
                .AddWithValue("@OriginalDocument", lblIdentifierCode.Text)
                .AddWithValue("@UpdatedDocument", txtIdentifier.Text)
                .AddWithValue("@DeletedDocument", "N/A")
            End With
            connUserActivity.Open()
            cmdUserActivity.ExecuteNonQuery()
        Finally
            Dim conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
            conn.Close()
        End Try
        'MsgBox("Document details successfully updated.", MsgBoxStyle.Information, "Update Details")
        Response.Redirect("~/Documents.aspx")
    End Sub

    Protected Sub cmdNo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNo.Click
        If cmdNo.Text = "OK" Then
            If txtPassword.Text = Session("Password") Then
                DeletedDocument(Session("Identifier"), Session("UserName"))
                cmdEdit.Enabled = True
                cmdDelete.Enabled = True
                GrdVwDocuments.Enabled = True
                cmdYes.Visible = False
                cmdNo.Visible = False
                Try
                    'MsgBox("Document successfully deleted.", MsgBoxStyle.Information, "Success")
                    Dim sqlDocumentDelete As String = "DELETE FROM Documents WHERE doc_code = @Identifier"
                    Dim connDocumentDelete As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
                    Dim cmdDocumentDelete As SqlCommand = New SqlCommand(sqlDocumentDelete, connDocumentDelete)
                    cmdDocumentDelete.Parameters.AddWithValue("@Identifier", txtIdentifier.Text)
                    connDocumentDelete.Open()
                    cmdDocumentDelete.ExecuteNonQuery()
                Finally
                    Dim conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
                    conn.Close()
                End Try
                Response.Redirect("~/Documents.aspx")
            Else

                lblMessage.Visible = True
                lblMessage.Text = "Invalid password."
                txtPassword.Text = ""
                txtPassword.Focus()
            End If
        Else
            lblMessage.Visible = False
            cmdYes.Visible = False
            cmdNo.Visible = False
            cmdEdit.Enabled = True
            cmdDelete.Enabled = True
            GrdVwDocuments.Enabled = True
        End If
    End Sub

    Protected Sub cmdYes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdYes.Click
        lblMessage.Text = "Enter password :"
        txtPassword.Visible = True
        txtPassword.Focus()
        cmdYes.Visible = False
        cmdNo.Text = "OK"
    End Sub

    'Protected Sub cmdClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdClose.Click
    '    Response.Redirect("~/Document Browse.aspx")
    'End Sub

    'Protected Sub drpdwnlstOrigin_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpdwnlstOrigin.SelectedIndexChanged
    '    txtValue.Text = drpdwnlstOrigin.SelectedValue
    '    If txtValue.Text = "External" Then
    '        Dim x As String = "E"
    '        lblAvailability.Enabled = True
    '        lblAvailability.Visible = True
    '        txtAvailability.Text = "http://"
    '        txtAvailability.Enabled = True
    '        txtAvailability.Visible = True
    '        cmdSave.Enabled = True

    '        lblSelect.Visible = False
    '        lblFile.Enabled = False
    '        lblFile.Visible = False
    '        FileUpload1.Enabled = False
    '        FileUpload1.Visible = False
    '        btnUpload.Enabled = False
    '        btnUpload.Visible = False
    '        txtIdentifier.Text = x + "-" + txtType.Text + "-" + lblDateFormat.Text + Format(getlastdoc() + 1, "0000").ToString

    '    ElseIf txtValue.Text = "Internal" Then
    '        Dim x As String = "I"
    '        lblFile.Enabled = True
    '        lblFile.Visible = True
    '        FileUpload1.Enabled = True
    '        FileUpload1.Visible = True
    '        btnUpload.Enabled = True
    '        btnUpload.Visible = True

    '        lblSelect.Visible = False
    '        lblAvailability.Enabled = False
    '        lblAvailability.Visible = False
    '        txtAvailability.Enabled = False
    '        txtAvailability.Text = "N/A"
    '        txtAvailability.Visible = False
    '        cmdSave.Enabled = False
    '        txtIdentifier.Text = x + "-" + txtType.Text + "-" + lblDateFormat.Text + Format(getlastdoc() + 1, "0000").ToString

    '    ElseIf txtValue.Text = "Select" Then
    '        lblSelect.Visible = True

    '        lblFile.Enabled = False
    '        lblFile.Visible = False
    '        FileUpload1.Enabled = False
    '        FileUpload1.Visible = False
    '        btnUpload.Enabled = False
    '        btnUpload.Visible = False

    '        lblAvailability.Enabled = False
    '        lblAvailability.Visible = False
    '        txtAvailability.Enabled = False
    '        txtAvailability.Visible = False
    '        cmdSave.Enabled = False
    '    End If


    'End Sub

    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        FileUpload1.Enabled = False
        Dim path As String
        Dim fileID As String
        path = Server.MapPath(".") & "\Attachments\"
        'path = Directory.GetParent(Server.MapPath("")).FullName & "\DRIMS\Attachments\" 'for outside folder

        fileID = txtIdentifier.Text
        Dim fileNames As String() = System.IO.Directory.GetFiles(path, fileID + ".*")

        
        If FileUpload1.HasFile Then
            Dim origFilename As String = FileUpload1.FileName
            Dim extension As String = origFilename.Substring(origFilename.LastIndexOf("."), (origFilename.Length - origFilename.LastIndexOf(".")))

            If extension = ".pdf" Then
                If fileID = "" Then
                    lblUploaded.Visible = True
                    lblUploaded.Text = "No identifier found. Please input identifier first."
                    txtIdentifier.Focus()
                    FileUpload1.Enabled = True
                Else
                    'Dim origFilename As String = FileUpload1.FileName
                    'Dim extension As String = origFilename.Substring(origFilename.LastIndexOf("."), (origFilename.Length - origFilename.LastIndexOf(".")))

                    FileUpload1.SaveAs(path + fileID + ".pdf")
                    lblUploaded.Visible = True
                    lblUploaded.Text = "File Successfully Uploaded."
                    txtAvailability.Text = txtIdentifier.Text + ".pdf"
                    cmdSave.Enabled = True
                    btnUpload.Enabled = False
                End If
            Else
                lblUploaded.Visible = True
                lblUploaded.Text = "Invalid File Type. Please upload PDF format only."
                FileUpload1.Enabled = True
                'MsgBox("Invalid File Type. Please upload PDF format only.", MsgBoxStyle.Exclamation, "Error")
            End If


            
        Else
            lblUploaded.Visible = True
            lblUploaded.Text = "No file selected. Please try again."
            FileUpload1.Enabled = True
        End If






    End Sub

    Protected Sub drpdwnlstType_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles drpdwnlstType.SelectedIndexChanged
        txtType.Text = drpdwnlstType.SelectedValue
        txtIdentifier.Text = "I" + "-" + txtType.Text + "-" + lblDateFormat.Text + Format(getlastdoc() + 1, "0000").ToString
    End Sub

    Protected Sub drpdwnlstSignatory_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles drpdwnlstSignatory.SelectedIndexChanged
        lblSign.Text = drpdwnlstSignatory.SelectedValue
        txtSignatory.Visible = False
        If drpdwnlstSignatory.SelectedValue = "99-9999" Or drpdwnlstSignatory.SelectedValue = "11-1111" Then
            txtSignatory.Enabled = True
            txtSignatory.Visible = True
            txtSignatory.Focus()

        End If
    End Sub

    Protected Sub drpdwnlstAgency_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles drpdwnlstAgency.SelectedIndexChanged
        txtAgency.Text = drpdwnlstAgency.SelectedValue
        drpdwnlstDivision.Enabled = False
        If drpdwnlstAgency.SelectedValue <> "NAMRIA" Then
            drpdwnlstDivision.SelectedValue = "9000"
            txtDivision.Text = "9000"
            drpdwnlstDivision.Enabled = False
            drpdwnlstSignatory.SelectedValue = "99-9999"
            lblSign.Text = drpdwnlstSignatory.SelectedValue
            drpdwnlstSignatory.Enabled = False
            txtSignatory.Visible = True
            txtSubject.Focus()
            lblNA1.Visible = True
            drpdwnlstDivision.Visible = False
            drpdwnlstSignatory.Visible = False
        ElseIf drpdwnlstAgency.SelectedValue = "NAMRIA" Then
            drpdwnlstDivision.Enabled = True
            drpdwnlstDivision.SelectedValue = "1000"
            drpdwnlstSignatory.SelectedValue = "01-0019"
            lblSign.Text = drpdwnlstSignatory.SelectedValue
            drpdwnlstSignatory.Enabled = True
            txtSignatory.Visible = False
            lblNA1.Visible = False
            drpdwnlstDivision.Visible = True
            drpdwnlstSignatory.Visible = True
        End If
    End Sub

    Protected Sub drpdwnlstDivision_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles drpdwnlstDivision.SelectedIndexChanged
        txtDivision.Text = drpdwnlstDivision.SelectedValue
    End Sub

    'Protected Sub btnApproval_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnApproval.Click
    '    Response.Redirect("~/ForApprovalDocuments.aspx")
    'End Sub

    'Protected Sub btnPending_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPending.Click
    '    Response.Redirect("~/PendingDocuments.aspx")
    'End Sub

    Protected Sub btnPost_Click(sender As Object, e As System.EventArgs) Handles btnPost.Click
        Response.Redirect("~/Denied Documents.aspx")
    End Sub

    Protected Sub rdbtnInternal_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdbtnInternal.CheckedChanged
        rdbtnExternal.Checked = False
        txtValue.Text = "Internal"
        Dim x As String = "I"
        lblFile.Visible = True
        FileUpload1.Enabled = True
        FileUpload1.Visible = True
        btnUpload.Enabled = True
        btnUpload.Visible = True
        lblNA2.Visible = True
        lblNA3.Visible = False
        txtAvailability.Visible = False

        'lblSelect.Visible = False
        cmdSave.Enabled = False
        txtIdentifier.Text = x + "-" + txtType.Text + "-" + lblDateFormat.Text + Format(getlastdoc() + 1, "0000").ToString
        txtAvailability.Text = txtIdentifier.Text + ".pdf"
    End Sub

    Protected Sub rdbtnExternal_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdbtnExternal.CheckedChanged
        rdbtnInternal.Checked = False
        txtValue.Text = "External"
        Dim x As String = "E"
        txtAvailability.Visible = True
        txtAvailability.Text = "http://"
        cmdSave.Enabled = True
        lblNA2.Visible = False

        'lblSelect.Visible = False
        lblNA3.Visible = True
        FileUpload1.Enabled = False
        FileUpload1.Visible = False
        btnUpload.Enabled = False
        btnUpload.Visible = False
        txtIdentifier.Text = x + "-" + txtType.Text + "-" + lblDateFormat.Text + Format(getlastdoc() + 1, "0000").ToString
    End Sub

    Protected Sub GrdVwPending_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GrdVwPending.SelectedIndexChanged
        Session("PendingStatus") = "3"
        Session("Identifier") = GrdVwPending.SelectedValue
        Try
            Dim sql As String = "SELECT [doc_code], [user_id], [doc_subject], [doc_date], [status], [origin], [availability] FROM [Documents] WHERE ([doc_code]=@Selected and [status] = @Status1 and [system_owner]='D') or ([doc_code]=@Selected and [status] = @Status2 and [system_owner]='D')"
            Dim conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
            Dim cmd As SqlCommand = New SqlCommand(sql, conn)
            With cmd.Parameters
                .AddWithValue("@Status1", "For Approval")
                .AddWithValue("@Status2", "Pending")
                .AddWithValue("@Selected", Session("Identifier"))
            End With
            Dim reader As SqlDataReader
            conn.Open()
            reader = cmd.ExecuteReader()
            If (reader.Read()) Then
                Session("Status") = reader("status")
                Session("Origin") = reader("origin")
                Session("Doc_Subject") = reader("doc_subject")
                If reader("origin") = "External" Then
                    Session("Availability") = reader("availability")
                ElseIf reader("origin") = "Internal" Then
                    Session("Identifier") = GrdVwPending.SelectedValue
                End If
                Session("ForApprove") = "1"
                Response.Redirect("~/DocumentDetailsDisplay.aspx")
            End If
        Finally
            Dim conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
            conn.Close()
        End Try
    End Sub
End Class


'If FileUpload1.HasFile Then
'    Dim origFilename As String = FileUpload1.FileName
'    Dim extension As String = origFilename.Substring(origFilename.LastIndexOf("."), (origFilename.Length - origFilename.LastIndexOf(".")))

'    FileUpload1.SaveAs(path + ID + extension)
'    lblUploaded.Visible = True
'    lblUploaded.Text = "File Successfully Uploaded."
'    cmdSave.Enabled = True
'    btnUpload.Enabled = False
'Else
'    lblUploaded.Visible = True
'    lblUploaded.Text = "Please try again."
'End If

'Protected Sub cmdSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
'    ClearAll()
'    If txtSearch.Text = "" Then
'        lblError.Visible = "true"
'        lblError.Text = "Please type a keyword."
'    Else

'        GrdVwDocuments.Visible = False
'        GrdVwSearch.Visible = True
'        SqlDataSourceSearch.DataBind()
'        lblError.Text = ""
'        Dim sqlUserActivity As String = "Insert INTO UserActivityLog (UserName,ModuleAccessed,DateAccessed,SearchedWord,FileViewed,RecordAdded) Values ( " & "'" & Session("UserName") & "','Search from Manage Document'," & "'" & DateTime.Now() & "','" & txtSearch.Text & "','N/A', 'N/A')"
'        Dim connUserActivity As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("NLAPSConnectionString").ConnectionString)
'        Dim cmdUserActivity As SqlCommand = New SqlCommand(sqlUserActivity, connUserActivity)
'        connUserActivity.Open()
'        cmdUserActivity.ExecuteNonQuery()
'    End If
'End Sub

'Protected Sub cmdClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdClear.Click
'    txtSearch.Text = ""
'    lblError.Text = ""
'    SqlDataSourceSearch.DataBind()
'    GrdVwDocuments.Visible = True
'    GrdVwSearch.Visible = False
'End Sub

'Protected Sub GridView2_Selected() Handles GrdVwSearch.SelectedIndexChanged
'    Dim row As GridViewRow = GrdVwSearch.SelectedRow
'    Dim key As String = GrdVwSearch.SelectedDataKey.Value.ToString
'    txtIdentifier.Text = row.Cells(1).Text
'    Dim sql As String = "SELECT Subject, Signatory, Publisher, Keyword, Date, Type, Availability FROM RecordsNew WHERE Identifier='" & txtIdentifier.Text & "'"
'    Dim conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("NLAPSConnectionString").ConnectionString)
'    Dim cmd As SqlCommand = New SqlCommand(sql, conn)
'    Dim reader As SqlDataReader
'    conn.Open()
'    reader = cmd.ExecuteReader()
'    ' check if there are results
'    If (reader.Read()) Then
'        ' populate the values of the controls
'        txtSubject.Text = reader(0)
'        txtSignatory.Text = reader(1)
'        txtPublisher.Text = reader(2)
'        txtKeyword.Text = reader(3)
'        txtDate.Text = reader(4)
'        txtType.Text = reader(5)
'        txtAvailability.Text = reader(6)
'        lblIdentifierCode.Text = txtIdentifier.Text
'    End If
'    cmdEdit.Enabled = True
'    cmdDelete.Enabled = True
'    txtIdentifier.ForeColor = Drawing.Color.Orange

'End Sub                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       