Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports iTextSharp
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports AddWatermark
Imports ResultPrinting

Partial Class SampleSearch
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session("UserName") = "" Then
            Response.Redirect("~/AccessDenied.aspx")
        ElseIf Session("UserLevel") = "1" Or Session("UserLevel") = "2" Then
            btnPrint.Visible = False
        End If
        txtSearch.Focus()
        
        
    End Sub

    Protected Sub SqlDataSourceSearch_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles SqlDataSourceSearch.Selected
        Dim recount As Integer
        recount = e.AffectedRows

        lblCount.Text = "Total Record(s) : " & Str(recount)
    End Sub
    Protected Sub SqlDataSourceSearch_Updating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceCommandEventArgs) Handles SqlDataSourceSearch.Updating
        Dim s As String = e.Command.CommandText
    End Sub

    Protected Sub SqlDataSourceSearchDate_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles SqlDataSourceSearchDate.Selected
        Dim recount As Integer
        recount = e.AffectedRows

        lblCount.Text = "Total Record(s) : " & Str(recount)

        
    End Sub

    Protected Sub SqlDataSourceSearchDate_Updating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceCommandEventArgs) Handles SqlDataSourceSearchDate.Updating
        Dim s As String = e.Command.CommandText
    End Sub


    'Protected Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
    '    txtSearch.Text = ""
    '    lblMessage.Text = ""
    '    SqlDataSourceSearch.DataBind()
    'End Sub

    Protected Sub clndrfrom_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles clndrfrom.SelectionChanged
        txtFrom.Text = clndrfrom.SelectedDate
    End Sub

    Protected Sub clndrto_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles clndrto.SelectionChanged
        txtTo.Text = clndrto.SelectedDate
    End Sub

    Protected Sub btnSearchbyDate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearchbyDate.Click
        lblCount.Text = "Total Record(s) : "
        lblKey.Visible = False
        txtSearch.Visible = False
        btnSearch.Visible = False
        txtTo.Visible = True
        txtFrom.Visible = True
        GridView1.Visible = False
        lblCount.Visible = False

        btnDate.Visible = False
        'btnClear.Visible = False
        btnSearchAllEntry.Visible = False
        GridView2.Visible = False
        btnPrint.Enabled = False
        clndrfrom.VisibleDate = "2010-05-01"
        clndrto.VisibleDate = Today
        txtSearch.Text = ""
        lblMessage.Text = ""

        lblTo.Visible = True
        lblFrom.Visible = True
        'txtFrom.Visible = False
        'txtTo.Visible = False
        clndrfrom.Visible = True
        clndrto.Visible = True
        btnSearchDate.Visible = True
    End Sub

    Protected Sub btnSearchbyKey_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearchbyKey.Click
        lblCount.Text = "Total Record(s) : "
        lblCount.Visible = False
        lblTo.Visible = False
        lblFrom.Visible = False
        txtFrom.Visible = False
        txtTo.Visible = False
        clndrfrom.Visible = False
        clndrto.Visible = False
        txtSearch.Text = ""
        lblMessage.Text = ""
        clndrfrom.VisibleDate = "2010-05-01"
        clndrto.VisibleDate = Today
        txtFrom.Text = ""
        txtTo.Text = ""
        GridView2.Visible = False


        btnSearchDate.Visible = False
        btnSearchAllEntry.Visible = False
        GridView1.Visible = False
        txtSearch.Focus()
        btnPrint.Enabled = False

        lblKey.Visible = True
        txtSearch.Visible = True
        btnSearch.Visible = True
        'btnClear.Visible = True
        btnDate.Visible = False
    End Sub

    'Protected Sub btnSearchAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearchAll.Click
    '    clndrfrom.VisibleDate = "2010-05-01"
    '    clndrto.VisibleDate = Today
    '    txtSearch.Text = ""
    '    lblMessage.Text = ""

    '    lblTo.Visible = True
    '    lblFrom.Visible = True
    '    txtFrom.Visible = False
    '    txtTo.Visible = False
    '    clndrfrom.Visible = True
    '    clndrto.Visible = True
    '    btnSearchDate.Visible = False
    '    GridView1.Visible = False
    '    txtSearch.Focus()
    '    btnPrint.Enabled = False

    '    lblKey.Visible = True
    '    txtSearch.Visible = True
    '    btnSearch.Visible = False
    '    'btnClear.Visible = True
    '    btnSearchAllEntry.Visible = True
    'End Sub

    

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        If Session("UserLevel") = "1" Or Session("UserLevel") = "2" Then
            btnPrint.Enabled = False
        ElseIf Session("UserLevel") = "3" Then
            btnPrint.Enabled = False
        End If

        lblCount.Visible = True
        Session("Number") = "1"

        If txtSearch.Text = "" Then
            lblMessage.Visible = True
            lblMessage.Text = "Please type a keyword."
        Else
            Session("SearchDocu") = "ByKeyword"
            GridView1.DataBind()
            GridView1.Visible = True
            lblMessage.Text = ""
            Try
                Dim sqlUserActivity As String = "Insert INTO User_Activity_Viewer (UserName,ModuleAccessed,DateAccessed,SearchedWord,FileViewed,DocumentAdded,OriginalDocument,UpdatedDocument,DeletedDocument) Values (@UserName,@ModuleAccessed,@DateAccessed,@SearchedWord,@FileViewed,@DocumentAdded,@OriginalDocument,@UpdatedDocument,@DeletedDocument)"
                Dim connUserActivity As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
                Dim cmdUserActivity As SqlCommand = New SqlCommand(sqlUserActivity, connUserActivity)
                With cmdUserActivity.Parameters
                    .AddWithValue("@UserName", Session("UserName"))
                    .AddWithValue("@ModuleAccessed", "Search Document")
                    .AddWithValue("@DateAccessed", DateTime.Now())
                    .AddWithValue("@SearchedWord", txtSearch.Text)
                    .AddWithValue("@FileViewed", "N/A")
                    .AddWithValue("@DocumentAdded", "N/A")
                    .AddWithValue("@OriginalDocument", "N/A")
                    .AddWithValue("@UpdatedDocument", "N/A")
                    .AddWithValue("@DeletedDocument", "N/A")
                End With
                connUserActivity.Open()
                cmdUserActivity.ExecuteNonQuery()
            Finally
                Dim conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
                conn.Close()
            End Try
        End If
    End Sub

    Protected Sub btnSearchDate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearchDate.Click
        lblNo.Visible = False


        If txtFrom.Text = "" Then
            lblMessage.Visible = True
            lblMessage.Text = "Please select start date."
        ElseIf txtTo.Text = "" Then
            lblMessage.Visible = True
            lblMessage.Text = "Please select end date."
        Else
            Session("StartDate") = txtFrom.Text
            Session("EndDate") = txtTo.Text
            Session("Number") = "2"
            Session("SearchDocu") = "ByDate"
            GridView2.DataBind()
            Try
                GridView2.Visible = True
                GridView1.Visible = False
            Catch ex As SqlException
                lblNo.Visible = True
                lblNo.Text = "Please follow the format indicated above. mm/dd/yyyy"
                txtFrom.Focus()
            End Try
            
            lblCount.Visible = True
            lblMessage.Text = ""
            GridView1.AllowPaging = True
            GridView1.AllowPaging = 10
            If Session("UserLevel") = "1" Or Session("UserLevel") = "2" Then
                btnPrint.Enabled = False
            ElseIf Session("UserLevel") = "3" Then
                btnPrint.Enabled = False
            End If
            Try
                Dim sqlUserActivity As String = "Insert INTO User_Activity_Viewer (UserName,ModuleAccessed,DateAccessed,SearchedWord,FileViewed,DocumentAdded,OriginalDocument,UpdatedDocument,DeletedDocument) Values (@UserName,@ModuleAccessed,@DateAccessed,@SearchedWord,@FileViewed,@DocumentAdded,@OriginalDocument,@UpdatedDocument,@DeletedDocument)"
                Dim connUserActivity As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
                Dim cmdUserActivity As SqlCommand = New SqlCommand(sqlUserActivity, connUserActivity)
                With cmdUserActivity.Parameters
                    .AddWithValue("@UserName", Session("UserName"))
                    .AddWithValue("@ModuleAccessed", "Search Document")
                    .AddWithValue("@DateAccessed", DateTime.Now())
                    .AddWithValue("@SearchedWord", txtFrom.Text + " to " + txtTo.Text)
                    .AddWithValue("@FileViewed", "N/A")
                    .AddWithValue("@DocumentAdded", "N/A")
                    .AddWithValue("@OriginalDocument", "N/A")
                    .AddWithValue("@UpdatedDocument", "N/A")
                    .AddWithValue("@DeletedDocument", "N/A")
                End With
                connUserActivity.Open()
                cmdUserActivity.ExecuteNonQuery()
            Finally
                Dim conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
                conn.Close()
            End Try
        End If
        
        
    End Sub

    Protected Sub btnSearchAllEntry_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearchAllEntry.Click
        Session("StartDate") = txtFrom.Text
        Session("EndDate") = txtTo.Text
        SqlDataSourceSearchDate.DataBind()
        Dim dview As DataView = CType(SqlDataSourceSearchDate.Select(DataSourceSelectArguments.Empty), DataView)
        GridView1.Visible = True
        GridView1.DataSource = dview
        GridView1.DataBind()
        Session("Number") = "3"

        If txtSearch.Text = "" Then
            lblMessage.Visible = True
            lblMessage.Text = "Please type a keyword."
        ElseIf txtFrom.Text = "" Then
            lblMessage.Visible = True
            lblMessage.Text = "Please select start date."
        ElseIf txtTo.Text = "" Then
            lblMessage.Visible = True
            lblMessage.Text = "Please select end date."
        Else

            lblMessage.Text = ""
            Try
                Dim sqlUserActivity As String = "Insert INTO User_Activity_Viewer (UserName,ModuleAccessed,DateAccessed,SearchedWord,FileViewed,DocumentAdded,OriginalDocument,UpdatedDocument,DeletedDocument) Values (@UserName,@ModuleAccessed,@DateAccessed,@SearchedWord,@FileViewed,@DocumentAdded,@OriginalDocument,@UpdatedDocument,@DeletedDocument)"
                Dim connUserActivity As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
                Dim cmdUserActivity As SqlCommand = New SqlCommand(sqlUserActivity, connUserActivity)
                With cmdUserActivity.Parameters
                    .AddWithValue("@UserName", Session("UserName"))
                    .AddWithValue("@ModuleAccessed", "Search Document")
                    .AddWithValue("@DateAccessed", DateTime.Now())
                    .AddWithValue("@SearchedWord", txtSearch.Text + ", " + txtFrom.Text + " to " + txtTo.Text)
                    .AddWithValue("@FileViewed", "N/A")
                    .AddWithValue("@DocumentAdded", "N/A")
                    .AddWithValue("@OriginalDocument", "N/A")
                    .AddWithValue("@UpdatedDocument", "N/A")
                    .AddWithValue("@DeletedDocument", "N/A")
                End With
                connUserActivity.Open()
                cmdUserActivity.ExecuteNonQuery()
            Finally
                Dim conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
                conn.Close()
            End Try
        End If
        GridView1.AllowPaging = True
        GridView1.AllowPaging = 10
        If Session("UserLevel") = "1" Or Session("UserLevel") = "2" Then
            btnPrint.Enabled = False
        ElseIf Session("UserLevel") = "3" Then
            btnPrint.Enabled = True
        End If
    End Sub

    Protected Sub cmdClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Response.Redirect("~/Document Browse.aspx")
    End Sub

    Protected Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Dim NewFilePath As String = Server.MapPath(".") & "\Attachments\Temp\printresult.pdf"
        Dim ImagePath As String = Server.MapPath(".") + "\drimsBannerDocViewer.png"
        If Session("Number") = "1" Then
            If (txtSearch.Text = "") Then
                Response.Redirect("~/Attachments/Error.pdf")
            Else
                PrintSearch(txtSearch.Text, NewFilePath, ImagePath, Session("Origin"))
            End If
        ElseIf Session("Number") = "2" Then
            If (txtFrom.Text = "" And txtTo.Text = "") Then
                Response.Redirect("~/Attachments/Error.pdf")
            Else
                PrintDate(txtFrom.Text, txtTo.Text, NewFilePath, ImagePath, Session("Origin"))
            End If
        ElseIf Session("Number") = "3" Then
            PrintAllWithDate(txtSearch.Text, Session("StartDate"), Session("EndDate"), NewFilePath, ImagePath, Session("Origin"))
        End If

        Dim path As String = Server.MapPath(".") & "\Attachments\"
        Dim array() As String = {"NAMRIA - " + "" + Session("UserName") + "", "" + DateTime.Now() + ""}
        AddWatermarkDoubleText(Server.MapPath(".") & "\Attachments\Temp\printresult.pdf", Server.MapPath(".") & "\Attachments\Temp\printresultwithmark.pdf", array)
        Response.Redirect("~/Attachments/Temp/printresultwithmark.pdf")
    End Sub

    Protected Sub btnDate_Click(sender As Object, e As System.EventArgs) Handles btnDate.Click
        lblCount.Text = "Total Record(s) : "
        lblKey.Visible = False
        txtSearch.Visible = False
        btnSearch.Visible = False
        txtTo.Visible = True
        txtFrom.Visible = True

        btnDate.Visible = False
        'btnClear.Visible = False
        btnSearchAllEntry.Visible = False
        GridView1.Visible = False
        btnPrint.Enabled = False
        clndrfrom.VisibleDate = "2010-05-01"
        clndrto.VisibleDate = Today
        txtSearch.Text = ""
        lblMessage.Text = ""

        lblTo.Visible = True
        lblFrom.Visible = True
        'txtFrom.Visible = False
        'txtTo.Visible = False
        clndrfrom.Visible = True
        clndrto.Visible = True
        btnSearchDate.Visible = True
    End Sub

   
    Protected Sub txtSearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        btnSearch.Focus()
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged
        Session("Identifier") = GridView1.SelectedValue
        Session("SearchStatus") = "1"
        Session("SearchKey") = txtSearch.Text
        Try
            Dim sql As String = "SELECT Documents.doc_subject, Documents.status, Documents.origin, Documents.availability, Doctype_Lib.doctype_desc FROM Documents LEFT OUTER JOIN Doctype_Lib ON Documents.doctype_cd = Doctype_Lib.doctype_cd WHERE doc_code='" & GridView1.SelectedValue & "'"
            Dim conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
            Dim cmd As SqlCommand = New SqlCommand(sql, conn)
            Dim reader As SqlDataReader
            conn.Open()
            reader = cmd.ExecuteReader()
            If (reader.Read()) Then
                Session("Status") = reader("status")
                Session("Origin") = reader("origin")
                Session("Availability") = reader("availability")
                Session("Doc_Subject") = reader("doc_subject")
                Session("Type") = reader("doctype_desc")
            End If
        Finally
            Dim conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
            conn.Close()
        End Try
        Response.Redirect("~/DocumentDetailsDisplay.aspx")
    End Sub

    Protected Sub GridView2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView2.SelectedIndexChanged
        Session("Identifier") = GridView2.SelectedValue
        Session("SearchStatus") = "1"
        Session("SearchKey") = txtSearch.Text
        Try
            Dim sql As String = "SELECT Documents.doc_subject, Documents.status, Documents.origin, Documents.availability, Doctype_Lib.doctype_desc FROM Documents LEFT OUTER JOIN Doctype_Lib ON Documents.doctype_cd = Doctype_Lib.doctype_cd WHERE doc_code='" & GridView1.SelectedValue & "'"
            Dim conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
            Dim cmd As SqlCommand = New SqlCommand(sql, conn)
            Dim reader As SqlDataReader
            conn.Open()
            reader = cmd.ExecuteReader()
            If (reader.Read()) Then
                Session("Status") = reader("status")
                Session("Origin") = reader("origin")
                Session("Availability") = reader("availability")
                Session("Doc_Subject") = reader("doc_subject")
                Session("Type") = reader("doctype_desc")
            End If
        Finally
            Dim conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
            conn.Close()
        End Try
        Response.Redirect("~/DocumentDetailsDisplay.aspx")
    End Sub
End Class
