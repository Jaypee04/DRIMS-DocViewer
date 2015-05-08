Imports System.Data.SqlClient
Imports System.IO
Imports iTextSharp
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports AddWatermark
Imports System.Data

Partial Class Pages_AdminLogPage
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("UserName") = "" Then
            Response.Redirect("~/AccessDenied.aspx")
        End If
        'Session("PrintCode") = "1"
    End Sub

    Protected Sub SqlDataSourceAdmin_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles SqlDataSourceAdmin.Selected
        Dim recount As Integer
        recount = e.AffectedRows

        lblCount.Text = "Total Log(s) : " & Str(recount)

        If recount > 10 Then
            cmdViewAll.Enabled = True
            cmdViewTen.Enabled = True
        End If
    End Sub

    Protected Sub SqlDataSourceAdmin_Updating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceCommandEventArgs) Handles SqlDataSourceAdmin.Updating
        Dim s As String = e.Command.CommandText
    End Sub

    Protected Sub SqlDataSourceSearchDate_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles SqlDataSourceSearchDate.Selected
        Dim recount As Integer
        recount = e.AffectedRows

        lblCount.Text = "Total Log(s) : " & Str(recount)

        If recount > 10 Then
            cmdViewAll.Enabled = True
            cmdViewTen.Enabled = True
        End If
    End Sub

    Protected Sub SqlDataSourceSearchDate_Updating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceCommandEventArgs) Handles SqlDataSourceSearchDate.Updating
        Dim s As String = e.Command.CommandText
    End Sub

    Protected Sub cmdViewAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdViewAll.Click
        GridView1.AllowPaging = False
        cmdViewAll.Visible = False
        cmdViewTen.Visible = True
    End Sub

    Protected Sub cmdViewTen_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdViewTen.Click
        GridView1.AllowPaging = True
        GridView1.AllowPaging = 10
        cmdViewAll.Visible = True
        cmdViewTen.Visible = False
    End Sub

    Protected Sub btnPrintLog_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrintLog.Click
        If Session("PrintCode") = "1" Then
            Dim sqlvalue As String = "SELECT * FROM User_Log_Viewer ORDER BY LogInTime"
            Session("PrintSQL") = sqlvalue
        ElseIf Session("PrintCode") = "2" Then
            Dim sqlvalue As String = "SELECT [UserName], [LogInTime], [LogOffTime] FROM [User_Log_Viewer] WHERE ([LogInTime]>= @StartDate AND [LogInTime]<=@EndDate)"
            Session("PrintSQL") = sqlvalue
        End If
        Dim sqlprint As String = Session("PrintSQL")
        Dim conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
        Dim cmd As SqlCommand = New SqlCommand(sqlprint, conn)
        If Session("PrintCode") = "2" Then
            With cmd.Parameters
                .AddWithValue("@StartDate", Session("StartDate"))
                .AddWithValue("@EndDate", Session("EndDate"))
            End With
        End If
        Dim reader As SqlDataReader
        conn.Open()
        reader = cmd.ExecuteReader()

        ' check if there are results
        Try
            'Created the filestream document PDF
            Dim doc As New Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35)
            Dim writer As PdfWriter = PdfWriter.GetInstance(doc, New FileStream(Server.MapPath(".") & "\Attachments\Temp\printlog.pdf", FileMode.Create))

            Dim imageFilePath As String = Server.MapPath(".") + "\drimsBannerDocViewer.png"
            Dim image As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(imageFilePath)
            Dim title As String
            Dim para As New Paragraph
            para.Alignment = Element.ALIGN_CENTER
            'Resize image depend upon your need  
            image.ScaleToFit(300.0F, 280.0F)
            'Give space before image  
            image.SpacingBefore = 30.0F
            ' Give some space after the image  
            image.SpacingAfter = 1.0F
            image.Alignment = Element.ALIGN_CENTER
            title = "User Log Details"


            doc.Open()
            'Write the image in the filestream document PDF
            doc.Add(image)
            doc.Add(New Phrase(Environment.NewLine))
            doc.Add(New Phrase(Environment.NewLine))
            doc.Add(New Phrase(Environment.NewLine))
            doc.Add(New Paragraph(title, FontFactory.GetFont(FontFactory.TIMES, 22, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.RED)))
            doc.Add(New Phrase(Environment.NewLine))


            ' populate the values of the controls
            Do While (reader.Read())


                'Write the text in the filestream document PDF
                doc.Add(New Paragraph("UserName  : " + reader("UserName"), FontFactory.GetFont(FontFactory.TIMES, 12, iTextSharp.text.Font.NORMAL)))
                'doc.Add(New Phrase(Environment.NewLine))
                doc.Add(New Paragraph("Log In Time   : " + reader("LogInTime"), FontFactory.GetFont(FontFactory.TIMES, 12, iTextSharp.text.Font.NORMAL)))
                'doc.Add(New Phrase(Environment.NewLine))
                doc.Add(New Paragraph("Log Off Time : " + reader("LogOffTime"), FontFactory.GetFont(FontFactory.TIMES, 12, iTextSharp.text.Font.NORMAL)))


                doc.Add(New Phrase(Environment.NewLine))
                doc.Add(New Phrase(Environment.NewLine))
            Loop
            doc.Close()


        Catch ex As Exception
            Throw ex
        End Try
        Dim path As String = Server.MapPath(".") & "\Attachments\"
        Dim array() As String = {"NAMRIA - " + "" + Session("UserName") + "", "" + DateTime.Now() + ""}
        AddWatermarkDoubleText(Server.MapPath(".") & "\Attachments\Temp\printlog.pdf", Server.MapPath(".") & "\Attachments\Temp\printlogwithmark.pdf", array)
        Response.Redirect("~/Attachments/Temp/printlogwithmark.pdf")
    End Sub

    Protected Sub btnSearchDate_Click(sender As Object, e As System.EventArgs) Handles btnSearchDate.Click
        lblMessage.Visible = False
        If txtFrom.Text = Nothing Then
            lblMessage.Visible = True
            lblMessage.Text = "Please enter start date."
            clndrfrom.Focus()
        ElseIf txtTo.Text = Nothing Then
            lblMessage.Visible = True
            lblMessage.Text = "Please enter end date."
            clndrto.Focus()
        Else
            Session("StartDate") = txtFrom.Text
            Session("EndDate") = txtTo.Text
            
            Try
                SqlDataSourceSearchDate.DataBind()
                Dim dview As DataView = CType(SqlDataSourceSearchDate.Select(DataSourceSelectArguments.Empty), DataView)
                GridView1.DataSource = dview
                GridView1.DataBind()
                GridView1.Visible = True
            Catch ex As SqlException
                lblMessage.Visible = True
                lblMessage.Text = "Please follow the format indicated above. mm/dd/yyyy"
                txtFrom.Focus()
            End Try
            
            GridView1.AllowPaging = True
            GridView1.AllowPaging = 10
            lblMessage.Text = ""
            Session("PrintCode") = "2"
            btnPrintLog.Visible = True
            lblCount.Visible = True
            cmdViewAll.Visible = True
            cmdViewTen.Visible = True

        End If

    End Sub

    Protected Sub btnClear_Click(sender As Object, e As System.EventArgs) Handles btnClear.Click
        Session("PrintCode") = "1"
        SqlDataSourceAdmin.DataBind()
        lnkbtnViewAll.Enabled = True
        btnSearchDate.Enabled = True
        GridView1.Visible = False
        btnPrintLog.Visible = False
        lblCount.Visible = False
        cmdViewAll.Visible = False
        cmdViewTen.Visible = False
        'Dim dview As DataView = CType(SqlDataSourceAdmin.Select(DataSourceSelectArguments.Empty), DataView)
        'GridView1.DataSource = dview
        'GridView1.DataBind()
    End Sub

    'Protected Sub btnGrid_Click(sender As Object, e As System.EventArgs) Handles btnGrid.Click
    '    SqlDataSourceAdmin.DataBind()
    '    Dim dview As DataView = CType(SqlDataSourceAdmin.Select(DataSourceSelectArguments.Empty), DataView)
    '    GridView1.DataSource = dview
    '    GridView1.DataBind()
    '    btnPrintLog.Visible = True
    '    lblCount.Visible = True
    '    cmdViewAll.Visible = True
    '    cmdViewTen.Visible = True
    'End Sub

    Protected Sub clndrfrom_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles clndrfrom.SelectionChanged
        txtFrom.Text = clndrfrom.SelectedDate
    End Sub

    Protected Sub clndrto_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles clndrto.SelectionChanged
        txtTo.Text = clndrto.SelectedDate
    End Sub

    Protected Sub lnkbtnViewAll_Click(sender As Object, e As System.EventArgs) Handles lnkbtnViewAll.Click
        Session("Search") = "All"
        txtFrom.Text = ""
        txtTo.Text = ""
        lblFrom.Visible = False
        lblTo.Visible = False
        clndrfrom.Visible = False
        clndrto.Visible = False
        txtFrom.Visible = False
        txtTo.Visible = False
        btnSearchDate.Visible = False
        btnClear.Visible = False
        GridView1.Visible = False
        btnPrintLog.Visible = False
        lblCount.Visible = False
        cmdViewAll.Visible = False
        cmdViewTen.Visible = False
        GridView1.AllowPaging = True
        GridView1.AllowPaging = 10
        SqlDataSourceAdmin.DataBind()
        Dim dview As DataView = CType(SqlDataSourceAdmin.Select(DataSourceSelectArguments.Empty), DataView)
        GridView1.DataSource = dview
        GridView1.DataBind()
        Session("PrintCode") = "1"
        GridView1.Visible = True
        btnPrintLog.Visible = True
        lblCount.Visible = True
        cmdViewAll.Visible = True
        cmdViewTen.Visible = True

    End Sub

    Protected Sub lnkbtnDate_Click(sender As Object, e As System.EventArgs) Handles lnkbtnDate.Click
        Session("Search") = "Date"
        lblFrom.Visible = True
        lblTo.Visible = True
        clndrfrom.Visible = True
        clndrto.Visible = True
        txtFrom.Visible = True
        txtTo.Visible = True
        btnSearchDate.Visible = True
        btnClear.Visible = True

        GridView1.Visible = False
        btnPrintLog.Visible = False
        lblCount.Visible = False
        cmdViewAll.Visible = False
        cmdViewTen.Visible = False
    End Sub

   
   
    Protected Sub GridView1_PageIndexChanged(sender As Object, e As System.EventArgs)

        'If Session("Search") = "All" Then
        '    SqlDataSourceAdmin.DataBind()
        '    Dim dview As DataView = CType(SqlDataSourceAdmin.Select(DataSourceSelectArguments.Empty), DataView)
        '    GridView1.DataSource = dview
        '    GridView1.DataBind()
        'ElseIf Session("Search") = "Date" Then
        '    Session("StartDate") = txtFrom.Text
        '    Session("EndDate") = txtTo.Text
        '    SqlDataSourceSearchDate.DataBind()
        '    Dim dview As DataView = CType(SqlDataSourceSearchDate.Select(DataSourceSelectArguments.Empty), DataView)
        '    GridView1.DataSource = dview
        '    GridView1.DataBind()

        'End If

    End Sub

    Protected Sub GridView1_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        If Session("Search") = "All" Then
            SqlDataSourceAdmin.DataBind()
            GridView1.PageIndex = e.NewPageIndex
            Dim dview As DataView = CType(SqlDataSourceAdmin.Select(DataSourceSelectArguments.Empty), DataView)
            GridView1.DataSource = dview
            GridView1.DataBind()
        ElseIf Session("Search") = "Date" Then
            Session("StartDate") = txtFrom.Text
            Session("EndDate") = txtTo.Text
            SqlDataSourceSearchDate.DataBind()
            GridView1.PageIndex = e.NewPageIndex
            Dim dview As DataView = CType(SqlDataSourceSearchDate.Select(DataSourceSelectArguments.Empty), DataView)
            GridView1.DataSource = dview
            GridView1.DataBind()

        End If
        'GridView1.PageIndex = e.NewPageIndex
        'GridView1.DataBind()
    End Sub
End Class
