
Partial Class Pages_ForApprovalDocuments
    Inherits System.Web.UI.Page

    Protected Sub GrdVwApproval_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GrdVwApproval.SelectedIndexChanged
        Session("Identifier") = GrdVwApproval.SelectedValue
        Session("Status") = GrdVwApproval.SelectedValue
        Session("Availability") = GrdVwApproval.SelectedValue
        Response.Redirect("~/DocumentDetailsDisplay.aspx")
    End Sub

    Protected Sub SqlDataSourceApproval_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles SqlDataSourceApproval.Selected
        Dim reccount As Integer
        reccount = e.AffectedRows

        lblCount.Text = "Total Document(s) : " & Str(reccount)
    End Sub

    Protected Sub SqlDataSourceApproval_Updating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceCommandEventArgs) Handles SqlDataSourceApproval.Updating
        Dim s As String = e.Command.CommandText
    End Sub

End Class
