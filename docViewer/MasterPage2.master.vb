
Partial Class MasterPage2
    Inherits System.Web.UI.MasterPage

    Protected Sub form_init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Session("UserLevel") = "1" Then
            Label1.Text = ""
        ElseIf Session("UserLevel") = "2" Then
            Label1.Text = ""
        ElseIf Session("UserLevel") = "3" Then
            Label1.Text = ""
        End If
        If Session("UserName") = "" Then
            Label1.Text = ""
        End If

    End Sub
   
End Class

