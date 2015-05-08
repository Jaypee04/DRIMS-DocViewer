Imports System.Data.SqlClient
Imports System.Data

Partial Class MasterPage
    Inherits System.Web.UI.MasterPage

    Protected Sub form_load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Dim Employee_ID As String
        Dim fname As String
        Dim lname As String
        'Employee_ID = Session("EmployeeID")
        Label2.Enabled = True
        Label2.Visible = True
        fname = Session("FirstName")
        lname = Session("LastName")
        Label2.Text = fname & " " & lname
        btnDeepSearch.Visible = True
        If Session("UserLevel") = "3" Then
            cmdAddUser.Text = "BROWSE"
            cmdAddUser.ToolTip = "Look for Documents"
            cmdAddUser.PostBackUrl = "~/Document Browse.aspx"
            cmdDocument.PostBackUrl = "~/Documents.aspx"

        ElseIf Session("UserLevel") = "1" Or Session("UserLevel") = "2" Then
            cmdAddUser.Text = "BROWSE"
            cmdAddUser.ToolTip = "Look for Documents"
            cmdAddUser.PostBackUrl = "~/Document Browse.aspx"
            cmdDocument.Text = "MANAGE"
            cmdDocument.ToolTip = "Manage Documents"
            cmdDocument.PostBackUrl = "~/Documents.aspx"
            btnDeepSearch.PostBackUrl = "~/AdvancedSearch.aspx"

            'ElseIf Session("UserLevel") = "2" Then
            '    cmdAddUser.Text = "MANAGE DOCUMENTS"
            '    cmdAddUser.ToolTip = "Add/Edit Documents"
            '    cmdAddUser.PostBackUrl = "~/Documents.aspx"
            '    cmdDocument.Text = "FOR APPROVAL"
            '    cmdDocument.ToolTip = "View for Approval Documents"
            '    cmdDocument.PostBackUrl = "~/ForApprovalDocuments.aspx"
            '    cmdSearch.Text = "PENDING"
            '    cmdSearch.ToolTip = "View Pending for Approval Documents"
            '    cmdSearch.PostBackUrl = "~/PendingDocuments.aspx"
            '    btnDeepSearch.PostBackUrl = "~/AdvancedSearch.aspx"

        ElseIf Session("UserLevel") = "5" Then
            cmdDocument.Text = "USER ACTIVITIES"
            cmdDocument.ToolTip = "View User Activity Logs"
            cmdDocument.PostBackUrl = "~/AdminMaintenancePage.aspx"
            cmdSearch.Text = "USER LOGS"
            cmdSearch.ToolTip = "View User Logs"
            cmdSearch.PostBackUrl = "~/AdminLogPage.aspx"
            cmdAddUser.PostBackUrl = "~/Users.aspx"
            btnDeleted.Visible = True
            btnDeepSearch.Text = "DOCUMENT TYPE LIBRARY"
            btnDeepSearch.ToolTip = "View Document Type Library"
            btnDeepSearch.PostBackUrl = "~/Document Type.aspx"

        End If
    End Sub
    
    'Protected Sub DropDownList1_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownList1.PreRender
    '    Label2.Text = DropDownList1.SelectedItem.Text
    'End Sub

    Protected Sub cmdLogout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLogout.Click
        lblTime.Text = DateTime.Now()
        Dim sqlUserLog As String = "UPDATE User_Log_Viewer SET LogOffTime = " & "'" & Me.lblTime.Text & "'" & " WHERE LogInTime = " & " '" & Session("LogInTime") & "'" & ""
        Dim connUserLog As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
        Dim cmdUserLog As SqlCommand = New SqlCommand(sqlUserLog, connUserLog)
        connUserLog.Open()
        cmdUserLog.ExecuteNonQuery()

        If Session("UserLevel") = "2" Then
            Dim sqlDocumentUpdate As String = "UPDATE Documents SET Status = 'Pending' WHERE Status = 'For Approval'"
            Dim connDocumentUpdate As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
            Dim cmdDocument As SqlCommand = New SqlCommand(sqlDocumentUpdate, connDocumentUpdate)
            connDocumentUpdate.Open()
            cmdDocument.ExecuteNonQuery()

        End If
        Session("UserLevel") = ""
        Session("UserName") = ""
        Response.Redirect("http:\\namria.gov.ph\drims")
        
    End Sub

    'Protected Sub btnDeepSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeepSearch.Click
    'Response.Redirect("~/SampleSearch.aspx")
    'End Sub
End Class

