Imports System.Data.SqlClient
Imports System.Data
Imports AddWatermark

Partial Class MasterPage3
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
        Label2.Text = Session("UserName") 'fname & " " & lname

        If Session("UserLevel") = "3" Then
            lnkbtnAddUser.Text = "BROWSE"
            lnkbtnAddUser.ToolTip = "Look for Documents"
            lnkbtnAddUser.PostBackUrl = "~/Document Browse.aspx"
            lnkbtnDocument.PostBackUrl = "~/Documents.aspx"

        ElseIf Session("UserLevel") = "1" Or Session("UserLevel") = "2" Then
            lnkbtnAddUser.Text = "BROWSE"
            lnkbtnAddUser.ToolTip = "Look for Documents"
            lnkbtnAddUser.PostBackUrl = "~/Document Browse.aspx"
            lnkbtnDocument.Text = "MANAGE"
            lnkbtnDocument.ToolTip = "Documents Management"
            lnkbtnDocument.PostBackUrl = "~/Documents.aspx"
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

        ElseIf Session("UserLevel") = "5" Then
            lnkbtnDocument.Text = "USER ACTIVITIES"
            lnkbtnDocument.ToolTip = "View User Activity Logs"
            lnkbtnDocument.PostBackUrl = "~/AdminMaintenancePage.aspx"
            lnkbtnSearch.Text = "USER LOGS"
            lnkbtnSearch.ToolTip = "View User Logs"
            lnkbtnSearch.PostBackUrl = "~/AdminLogPage.aspx"
            lnkbtnAddUser.PostBackUrl = "~/Users.aspx"
            lnkbtnDocType.Visible = True
            lnkbtnDeleted.Visible = True
        End If
    End Sub

    'Protected Sub DropDownList1_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownList1.PreRender
    '    Label2.Text = DropDownList1.SelectedItem.Text
    'End Sub

    'Protected Sub cmdLogout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLogout.Click
    '    lblTime.Text = DateTime.Now()
    '    Dim sqlUserLog As String = "UPDATE User_Log_Viewer SET LogOffTime = " & "'" & Me.lblTime.Text & "'" & " WHERE LogInTime = " & " '" & Session("LogInTime") & "'" & ""
    '    Dim connUserLog As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
    '    Dim cmdUserLog As SqlCommand = New SqlCommand(sqlUserLog, connUserLog)
    '    connUserLog.Open()
    '    cmdUserLog.ExecuteNonQuery()

    '    If Session("UserLevel") = "2" Then
    '        Dim sqlDocumentUpdate As String = "UPDATE Documents SET Status = 'Pending' WHERE Status = 'For Approval'"
    '        Dim connDocumentUpdate As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
    '        Dim cmdDocument As SqlCommand = New SqlCommand(sqlDocumentUpdate, connDocumentUpdate)
    '        connDocumentUpdate.Open()
    '        cmdDocument.ExecuteNonQuery()

    '    End If
    '    Session("UserLevel") = ""
    '    Session("UserName") = ""
    '    Response.Redirect("http:\\namria.gov.ph\drims")

    'End Sub
    
    
    Protected Sub lnkLogout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkLogout.Click
        lblTime.Text = DateTime.Now()
        Try
            Dim sqlUserLog As String = "UPDATE User_Log_Viewer SET LogOffTime = " & "'" & Me.lblTime.Text & "'" & " WHERE LogInTime = " & " '" & Session("LogInTime") & "'" & ""
            Dim connUserLog As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
            Dim cmdUserLog As SqlCommand = New SqlCommand(sqlUserLog, connUserLog)
            connUserLog.Open()
            cmdUserLog.ExecuteNonQuery()
        Finally
            Dim conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
            conn.Close()
        End Try
        If Session("UserLevel") = "3" Then
            Try
                Dim sqlDocumentUpdate As String = "UPDATE Documents SET Status = 'Pending' WHERE Status = 'For Approval'"
                Dim connDocumentUpdate As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
                Dim cmdDocument As SqlCommand = New SqlCommand(sqlDocumentUpdate, connDocumentUpdate)
                connDocumentUpdate.Open()
                cmdDocument.ExecuteNonQuery()
            Finally
                Dim conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
                conn.Close()
            End Try
        End If
        Session("UserLevel") = ""
        Session("UserName") = ""
        Session("Availability") = ""
        Session("Identifier") = ""
        Response.Redirect("http:\\app.namria.gov.ph\drims")
    End Sub

    Protected Sub lnkManual_Click(sender As Object, e As System.EventArgs) Handles lnkManual.Click
        Dim array() As String = {Session("UserName") & "-NAMRIA", "" + DateTime.Now() + ""}
        AddWatermarkDoubleText(Server.MapPath(".") & "\Manual\DOCUMENT VIEWER SYSTEM (Orig).pdf", Server.MapPath(".") & "\Manual\DOCUMENT VIEWER SYSTEM.pdf", array)
        Response.Redirect("~/Manual/DOCUMENT VIEWER SYSTEM.pdf")
    End Sub

    Protected Sub lnkRectrack_Click(sender As Object, e As System.EventArgs) Handles lnkRectrack.Click
        Response.Redirect("http:\\app.namria.gov.ph\rectrack")
    End Sub
End Class

