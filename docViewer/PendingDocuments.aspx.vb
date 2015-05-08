Imports System.Data.SqlClient

Partial Class Pages_PendingDocuments
    Inherits System.Web.UI.Page

    Protected Sub GrdVwPending_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GrdVwPending.SelectedIndexChanged
        Session("PendingStatus") = "2"
        Session("Identifier") = GrdVwPending.SelectedValue
        Dim sql As String = "SELECT [doc_code], [user_id], [doc_subject], [doc_date], [status] FROM [Documents] WHERE ([status] = @Status1 and [system_owner]='D') or ([status] = @Status2 and [system_owner]='D')"
        Dim conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
        Dim cmd As SqlCommand = New SqlCommand(sql, conn)
        With cmd.Parameters
            .AddWithValue("@Status1", "For Approval")
            .AddWithValue("@Status2", "Pending")
        End With
        Dim reader As SqlDataReader
        conn.Open()
        reader = cmd.ExecuteReader()
        If (reader.Read()) Then
            Session("Status") = reader("status")
            Session("Availability") = GrdVwPending.SelectedValue

            Response.Redirect("~/DocumentDetailsDisplay.aspx")
        End If
    End Sub

    Protected Sub SqlDataSourcePending_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles SqlDataSourcePending.Selected
        Dim reccount As Integer
        reccount = e.AffectedRows

        lblCount.Text = "Total Record(s) : " & Str(reccount)
    End Sub

    Protected Sub SqlDataSourcePending_Updating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceCommandEventArgs) Handles SqlDataSourcePending.Updating
        Dim s As String = e.Command.CommandText
    End Sub

End Class
