Imports System.Data.SqlClient

Partial Class Pages_Notification
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim sql As String = "SELECT [user_id], [doc_subject], [doc_date] FROM [Documents] WHERE ([status] = @Status and [system_owner]='D' Or [status] = @Status2 and [system_owner]='D')"
            Dim conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
            Dim cmd As SqlCommand = New SqlCommand(sql, conn)
            cmd.Parameters.AddWithValue("@Status", "For Approval")
            cmd.Parameters.AddWithValue("@Status2", "Pending")
            Dim reader As SqlDataReader
            conn.Open()
            reader = cmd.ExecuteReader()
            ' check if there are results
            If Not (reader.Read()) Then
                Response.Redirect("~/Document Browse.aspx")
            End If
        Finally
            Dim conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
            conn.Close()
        End Try

    End Sub

    Protected Sub SqlDataSourcePending_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles SqlDataSourcePending.Selected
        Dim recount As Integer
        recount = e.AffectedRows
        lblCount.Text = Str(recount)

    End Sub

    Protected Sub SqlDataSourcePending_Updating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceCommandEventArgs) Handles SqlDataSourcePending.Updating
        Dim s As String = e.Command.CommandText
    End Sub

    'Protected Sub SqlDataSourceApproval_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles SqlDataSourceApproval.Selected
    '    Dim recount As Integer
    '    recount = e.AffectedRows
    '    lblCount2.Text = Str(recount)
    'End Sub

    'Protected Sub SqlDataSourceApproval_Updating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceCommandEventArgs) Handles SqlDataSourceApproval.Updating
    '    Dim s As String = e.Command.CommandText
    'End Sub

    

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged
        Session("PendingStatus") = "1"
        Session("Identifier") = GridView1.SelectedValue
        Try
            Dim sql As String = "SELECT [doc_code], [user_id], [doc_subject], [doc_date], [status], [availability], [origin] FROM [Documents] WHERE ([doc_code]=@Selected and [status] = @Status1 and [system_owner]='D') or ([doc_code]=@Selected and[status] = @Status2 and [system_owner]='D')"
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

                'Session("Availability") = reader("availability")
                Session("Doc_Subject") = reader("doc_subject")
                Session("Origin") = reader("origin")
                If reader("origin") = "External" Then
                    Session("Availability") = reader("availability")
                ElseIf reader("origin") = "Internal" Then
                    Session("Identifier") = GridView1.SelectedValue
                End If
                'MsgBox(Session("Doc_Subject") + _
                '    Session("Origin") + _
                '    Session("Availability") + Session("Identifier"), vbOK)
                Session("ForApprove") = "1"
                Response.Redirect("~/DocumentDetailsDisplay.aspx")
            End If

        Finally
            Dim conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
            conn.Close()
        End Try

    End Sub
End Class
