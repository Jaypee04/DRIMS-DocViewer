Imports System.Data.SqlClient
Imports System.IO

Partial Class DeletedDocumentView
    Inherits System.Web.UI.Page

    Protected Sub GrdVwDeleted_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GrdVwDeleted.SelectedIndexChanged
        Dim row As GridViewRow = GrdVwDeleted.SelectedRow
        txtIdentifier.Text = row.Cells(1).Text
        lblMessage.Visible = False
        btnRepost.Enabled = True
        btnDelete.Enabled = True
        Dim sql As String = "SELECT Temporary_Deleted_Document.* ,  Signatories.LAST_M + N', ' + Signatories.FIRST_M + N' ' + SUBSTRING(Signatories.MIDDLE_M, 1, 1) + '.' AS FullName, Doctype_lib.doctype_desc FROM Temporary_Deleted_Document LEFT OUTER JOIN Signatories ON Temporary_Deleted_Document.doc_signatory = Signatories.EMP_ID LEFT OUTER JOIN Doctype_Lib ON Temporary_Deleted_Document.doc_type = Doctype_Lib.doctype_cd WHERE doc_code=@Identifier"
        Dim conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
        Dim cmd As SqlCommand = New SqlCommand(sql, conn)
        cmd.Parameters.AddWithValue("@Identifier", txtIdentifier.Text)
        Dim reader As SqlDataReader
        conn.Open()
        Dim Agency, Division, FileDate, Availability, Status, Origin, Postedby, Encodedby As String

        reader = cmd.ExecuteReader()
        ' check if there are results
        If (reader.Read()) Then
            ' populate the values of the controls
            txtSubject.Text = reader("doc_subject")
            txtSignatory.Text = reader("FullName")
            txtType.Text = reader("doctype_desc")

            Agency = reader("doc_agency")
            Division = reader("doc_div")
            'Publisher = reader("doc_publisher")
            FileDate = reader("doc_date")
            Availability = reader("availability")
            Status = reader("status")
            Origin = reader("origin")
            Try
                Postedby = reader("postedby")
                Encodedby = reader("encodedby")
            Catch ex As Exception
                Postedby = ""
                Encodedby = "Old Document"
            End Try

        End If
    End Sub

    Protected Sub btnYes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnYes.Click
        lblMessage.Text = "Enter password :"
        txtPassword.Visible = True
        txtPassword.Focus()
        btnYes.Visible = False
        btnNo.Text = "OK"
    End Sub

    Protected Sub btnNo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNo.Click
        If btnNo.Text = "OK" Then
            If txtPassword.Text = Session("Password") Then
                Dim sqlDocumentDelete As String = "DELETE FROM Temporary_Deleted_Document WHERE doc_code =@Identifier"
                Dim connDocumentDelete As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
                Dim cmdDocumentDelete As SqlCommand = New SqlCommand(sqlDocumentDelete, connDocumentDelete)
                cmdDocumentDelete.Parameters.AddWithValue("@Identifier", txtIdentifier.Text)
                connDocumentDelete.Open()
                cmdDocumentDelete.ExecuteNonQuery()
                GrdVwDeleted.DataBind()
                btnDelete.Enabled = False
                btnRepost.Enabled = False
                btnNo.Visible = False
                btnYes.Visible = False
                lblMessage.Visible = False
                txtPassword.Visible = False
                txtPassword.Text = ""
                txtIdentifier.Text = ""
                txtSignatory.Text = ""
                txtSubject.Text = ""
                txtType.Text = ""

                'Response.Redirect("~/DeletedDocumentView.aspx")
            Else
                lblMessage.Visible = True
                lblMessage.Text = "Invalid password."
                txtPassword.Text = ""
                txtPassword.Focus()
            End If
        Else
            btnDelete.Enabled = True
            lblMessage.Visible = False
            btnYes.Visible = False
            btnNo.Visible = False
            btnRepost.Enabled = True
        End If

    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        btnDelete.Enabled = False
        lblMessage.Text = "Permanently delete this document?"
        lblMessage.Visible = True
        btnYes.Visible = True
        btnNo.Visible = True
    End Sub

    Protected Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Response.Redirect("~/AdminMaintenancePage.aspx")
    End Sub


    Protected Sub btnRepost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRepost.Click
        Dim sql As String = "SELECT Temporary_Deleted_Document.* ,  Signatories.LAST_M + N', ' + Signatories.FIRST_M + N' ' + SUBSTRING(Signatories.MIDDLE_M, 1, 1) + '.' AS FullName, Doctype_lib.doctype_desc FROM Temporary_Deleted_Document LEFT OUTER JOIN Signatories ON Temporary_Deleted_Document.doc_signatory = Signatories.EMP_ID LEFT OUTER JOIN Doctype_Lib ON Temporary_Deleted_Document.doc_type = Doctype_Lib.doctype_cd WHERE doc_code=@Identifier"
        Dim conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
        Dim cmd As SqlCommand = New SqlCommand(sql, conn)
        cmd.Parameters.AddWithValue("@Identifier", txtIdentifier.Text)
        Dim reader As SqlDataReader
        conn.Open()
        Dim Agency, Division, Signatory, FileDate, Availability, Status, Origin, Postedby, Encodedby, TypeCode As String

        reader = cmd.ExecuteReader()
        ' check if there are results
        If (reader.Read()) Then
            ' populate the values of the controls
            txtSubject.Text = reader("doc_subject")
            txtSignatory.Text = reader("FullName")
            txtType.Text = reader("doctype_desc")

            Agency = reader("doc_agency")
            Division = reader("doc_div")
            Signatory = reader("doc_signatory")
            'Publisher = reader("doc_publisher")
            TypeCode = reader("doc_type")
            'Keyword = reader("doc_keyword")
            FileDate = reader("doc_date")
            Availability = reader("availability")
            Status = reader("status")
            Origin = reader("origin")
            Try
                Postedby = reader("postedby")
                Encodedby = reader("encodedby")
            Catch ex As Exception
                Postedby = ""
                Encodedby = "Old Document"
            End Try
            Try
                Dim sqlDocumentRepost As String = "INSERT INTO Documents (doc_code, doc_agency, doc_div, doc_subject, doc_signatory, doc_date, doctype_cd, availability, status, origin, postedby, user_id, [system_owner]) VALUES (@Identifier, @Agency, @Division, @Subject, @Signatory, @Date, @Type, @Availability, @Status, @Origin, @Postedby, @Encodedby, @Own)"
                Dim connDocumentRepost As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
                Dim cmdDocumentRepost As SqlCommand = New SqlCommand(sqlDocumentRepost, connDocumentRepost)
                With cmdDocumentRepost.Parameters
                    .AddWithValue("@Identifier", txtIdentifier.Text)
                    .AddWithValue("@Agency", txtIdentifier.Text)
                    .AddWithValue("@Division", txtIdentifier.Text)
                    .AddWithValue("@Subject", txtSubject.Text)
                    .AddWithValue("@Signatory", Signatory)
                    '.AddWithValue("@Publisher", Publisher)
                    '.AddWithValue("@Keyword", Keyword)
                    .AddWithValue("@Date", FileDate)
                    .AddWithValue("@Type", TypeCode)
                    .AddWithValue("@Availability", Availability)
                    .AddWithValue("@Status", Status)
                    .AddWithValue("@Origin", Origin)
                    .AddWithValue("@Postedby", Postedby)
                    .AddWithValue("@Encodedby", Encodedby)
                    .AddWithValue("@Own", "D")
                End With
                connDocumentRepost.Open()
                cmdDocumentRepost.ExecuteNonQuery()
            Catch ex As Exception
                Throw ex
            End Try

            Dim sqlDocumentDelete As String = "DELETE FROM Temporary_Deleted_Document WHERE doc_code=@Identifier"
            Dim connDocumentDelete As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
            Dim cmdDocument As SqlCommand = New SqlCommand(sqlDocumentDelete, connDocumentDelete)
            cmdDocument.Parameters.AddWithValue("@Identifier", txtIdentifier.Text)
            connDocumentDelete.Open()
            cmdDocument.ExecuteNonQuery()
        End If
        lblMessage.Text = "Document Successfully Reposted."
        lblMessage.Visible = True
        GrdVwDeleted.DataBind()
        txtIdentifier.Text = ""
        txtSubject.Text = ""
        txtSignatory.Text = ""
        txtType.Text = ""
        btnRepost.Enabled = False
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        btnRepost.Enabled = False
        btnDelete.Enabled = False
    End Sub
End Class
