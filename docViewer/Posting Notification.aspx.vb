Imports System.Data.SqlClient

Partial Class Posting_Notification
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim sql As String = "SELECT [doc_subject], [doc_date] FROM [Documents] WHERE ([status] = @Status and [user_id] = @Username and [system_owner]='D' or [status] = @Status2 and [user_id] = @Username and [system_owner]='D')"
            Dim conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
            Dim cmd As SqlCommand = New SqlCommand(sql, conn)
            cmd.Parameters.AddWithValue("@Status", "Denied-Unread")
            cmd.Parameters.AddWithValue("@Status2", "Denied-Read")
            cmd.Parameters.AddWithValue("@Username", Session("UserName"))
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

    Protected Sub SqlDataSourceDenied_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles SqlDataSourceDenied.Selected
        Dim recount As Integer
        recount = e.AffectedRows
        lblCount.Text = Str(recount)

    End Sub

    Protected Sub SqlDataSourceDenied_Updating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceCommandEventArgs) Handles SqlDataSourceDenied.Updating
        Dim s As String = e.Command.CommandText
    End Sub

    Protected Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        'Dim sqlDenied As String = "UPDATE Documents set [status]=@Status WHERE ([status]=@Status1 and [user_id]=@UserName)"
        'Dim connDenied As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
        'Dim cmdDenied As SqlCommand = New SqlCommand(sqlDenied, connDenied)
        'cmdDenied.Parameters.AddWithValue("@Status", "Denied-Read")
        'cmdDenied.Parameters.AddWithValue("@Status1", "Denied-Unread")
        'cmdDenied.Parameters.AddWithValue("@UserName", Session("UserName"))
        'connDenied.Open()
        'cmdDenied.ExecuteNonQuery()
        Response.Redirect("~/Document Browse.aspx")
    End Sub
End Class
