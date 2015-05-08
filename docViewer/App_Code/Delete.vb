Imports System.Data.SqlClient

Public Class Delete

    Public Shared Sub DeletedDocument(ByVal Identify As String, ByVal UserName As String)
        Dim sql As String = "SELECT Documents.*,Doctype_lib.doctype_desc FROM Documents LEFT OUTER JOIN Doctype_Lib ON Documents.doctype_cd = Doctype_Lib.doctype_cd WHERE doc_code='" & Identify & "'"
        Dim conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
        Dim cmd As SqlCommand = New SqlCommand(sql, conn)
        Dim reader As SqlDataReader
        conn.Open()
        Dim Identity, Agency, Division, Subject, Signatory, Signatory_Ex, FileDate, Type, Availability, Status, Origin, Postedby, Processor As String

        reader = cmd.ExecuteReader()
        If (reader.Read()) Then
            Identity = reader("doc_code")
            Agency = reader("doc_agency")
            Division = reader("doc_div")
            Subject = reader("doc_subject")
            Signatory = reader("doc_signatory")

            If reader("doc_signatory") = "11-1111" Or reader("doc_signatory") = "99-9999" Then
                Signatory_Ex = reader("doc_signatory_external")
            Else
                Signatory_Ex = "N/A"
            End If

            'Publisher = reader("doc_publisher")
            FileDate = reader("doc_date")
            Type = reader("doctype_cd")
            Availability = reader("availability")
            Status = reader("status")
            Origin = reader("origin")
            Try
                Postedby = reader("postedby")
            Catch ex As Exception
                Postedby = ""
            End Try
            Try
                Processor = reader("user_id")
            Catch ex As Exception
                Processor = "Old Document"
            End Try

            Dim sqlDocumentDeleted As String = "INSERT INTO Temporary_Deleted_Document (doc_code, doc_agency, doc_div, doc_subject, doc_signatory, doc_signatory_external,  doc_date, doc_type, availability, status, origin, postedBy, encodedBy, deletedBy) VALUES (@Identifier, @Agency, @Division, @Subject, @Signatory, @SignatoryEx, @FileDate, @Type, @Availability, @Status, @Origin, @PostedBy, @EncodedBy, @DeletedBy)"
            Dim connDocumentDeleted As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
            Dim cmdDocument As SqlCommand = New SqlCommand(sqlDocumentDeleted, connDocumentDeleted)
            With cmdDocument.Parameters
                .AddWithValue("@Identifier", Identify)
                .AddWithValue("@Agency", Agency)
                .AddWithValue("@Division", Division)
                .AddWithValue("@Subject", Subject)
                .AddWithValue("@Signatory", Signatory)
                .AddWithValue("@SignatoryEx", Signatory_Ex)
                '.AddWithValue("@Publisher", Publisher)
                .AddWithValue("@FileDate", FileDate)
                .AddWithValue("@Type", Type)
                .AddWithValue("@Availability", Availability)
                .AddWithValue("@Status", Status)
                .AddWithValue("@Origin", Origin)
                .AddWithValue("@PostedBy", Postedby)
                .AddWithValue("@EncodedBy", Processor)
                .AddWithValue("@DeletedBy", UserName)
            End With
            connDocumentDeleted.Open()
            cmdDocument.ExecuteNonQuery()


            Dim sqlUserActivity As String = "Insert INTO User_Activity_Viewer (UserName,ModuleAccessed,DateAccessed,SearchedWord,FileViewed,DocumentAdded,OriginalDocument,UpdatedDocument,DeletedDocument) Values (@UserName, @ModuleAccessed, @DateAccessed, @SearchedWord, @FileViewed, @DocumentAdded, @OriginalDocument, @UpdatedDocument, @DeletedDocument)"
            Dim connUserActivity As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
            Dim cmdUserActivity As SqlCommand = New SqlCommand(sqlUserActivity, connUserActivity)
            With cmdUserActivity.Parameters
                .AddWithValue("@UserName", UserName)
                .AddWithValue("@ModuleAccessed", "Delete a Document")
                .AddWithValue("@DateAccessed", DateTime.Now())
                .AddWithValue("@SearchedWord", "N/A")
                .AddWithValue("@FileViewed", Identify)
                .AddWithValue("@DocumentAdded", "N/A")
                .AddWithValue("@OriginalDocument", "N/A")
                .AddWithValue("@UpdatedDocument", "N/A")
                .AddWithValue("@DeletedDocument", Identify)
            End With
            connUserActivity.Open()
            cmdUserActivity.ExecuteNonQuery()
        End If
    End Sub

    Public Shared Sub DeleteUser(ByVal UserID As String)
        Dim sqlUserDelete As String = "DELETE FROM User_Library WHERE User_Id = " & "'" & UserID & "'"
        Dim connUserDelete As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
        Dim cmdUserLib As SqlCommand = New SqlCommand(sqlUserDelete, connUserDelete)
        connUserDelete.Open()
        cmdUserLib.ExecuteNonQuery()
    End Sub

End Class
