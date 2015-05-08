Imports System.Data.SqlClient

Partial Class Denied_Documents
    Inherits System.Web.UI.Page

    
    Protected Sub GrdVwDenied_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GrdVwDenied.SelectedIndexChanged
        Session("PendingStatus") = "4"
        Session("Identifier") = GrdVwDenied.SelectedValue
        Try
            Dim sql As String = "SELECT [doc_code], [user_id], [doc_subject], [doc_date], [status], [availability] FROM [Documents] WHERE ([status] = @Status1 and [system_owner]='D') or ([status] = @Status2 and [system_owner]='D')"
            Dim conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
            Dim cmd As SqlCommand = New SqlCommand(sql, conn)
            With cmd.Parameters
                .AddWithValue("@Status1", "Denied-Unread")
                .AddWithValue("@Status2", "Denied-Read")
            End With
            Dim reader As SqlDataReader
            conn.Open()
            reader = cmd.ExecuteReader()
            If (reader.Read()) Then
                Session("Status") = reader("status")
                Session("Availability") = reader("availability")

                Response.Redirect("~/DocumentDetailsDisplay.aspx")
            End If
        Finally
            Dim conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
            conn.Close()
        End Try

    End Sub

    Protected Sub SqlDataSourceDenied_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles SqlDataSourceDenied.Selected
        Dim recount As Integer
        recount = e.AffectedRows
        lblCount.Text = Str(recount)

    End Sub

    Protected Sub SqlDataSourceDenied_Updating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceCommandEventArgs) Handles SqlDataSourceDenied.Updating
        Dim s As String = e.Command.CommandText
    End Sub

End Class
