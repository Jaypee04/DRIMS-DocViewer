Imports System.Data.SqlClient
Imports System.IO
Imports iTextSharp
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports AddWatermark
Imports Delete

Partial Class Pages_Users
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("UserName") = "" Then
            Response.Redirect("~/AccessDenied.aspx")
        End If
    End Sub

    Protected Sub SqlDataSourceUser_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles SqlDataSourceUser.Selected
        Dim recount As Integer
        recount = e.AffectedRows
        lblCount.Text = "Total User(s) : " & Str(recount)
    End Sub

    Protected Sub SqlDataSourceUser_Updating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceCommandEventArgs) Handles SqlDataSourceUser.Updating
        Dim s As String = e.Command.CommandText
    End Sub

    Protected Sub GridView_Selected(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged
        Dim row As GridViewRow = GridView1.SelectedRow
        Dim key As String = GridView1.SelectedDataKey.Value.ToString
        txtUserName.Text = row.Cells(5).Text
        Dim sql As String = "SELECT EmployeeID, User_FName, User_MName, User_LName, User_Password, User_Level FROM User_Library WHERE User_Id=@UserName"
        Dim conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
        Dim cmd As SqlCommand = New SqlCommand(sql, conn)
        cmd.Parameters.AddWithValue("@UserName", txtUserName.Text)
        Dim reader As SqlDataReader
        conn.Open()
        reader = cmd.ExecuteReader()
        If (reader.Read()) Then
            txtFirstName.Text = reader("User_FName")
            txtPassword.Text = reader("User_Password")
            txtLevel.Text = reader("User_Level")
            Try
                txtEmployee_ID.Text = reader("EmployeeID")
            Catch ex As Exception
                txtEmployee_ID.Text = ""
            End Try
            Try
                txtMiddleName.Text = reader("User_MName")
            Catch ex As Exception
                txtMiddleName.Text = ""
            End Try
            Try
                txtLastName.Text = reader("User_LName")
            Catch ex As Exception
                txtLastName.Text = ""
            End Try
            cmdEdit.Enabled = True
            cmdDelete.Enabled = True
            txtEmployee_ID.ForeColor = Drawing.Color.Orange
        End If
    End Sub

    Protected Sub EnableAll()
        txtEmployee_ID.Enabled = True
        txtFirstName.Enabled = True
        txtMiddleName.Enabled = True
        txtLastName.Enabled = True
        txtUserName.Enabled = True
        txtPassword.Enabled = True
        txtLevel.Enabled = True
        
        lblPress.Visible = True
        txtEmployee_ID.Focus()
    End Sub

    Protected Sub SaveUser()
        Try
            Dim sqlUserSave As String = "INSERT INTO User_Library (EmployeeID, User_FName, User_MName, User_LName, User_Id, User_Password, User_Level) VALUES (@EmployeeID, @FirstName, @MiddleName, @LastName, @UserName, @Password, @UserLevel)"
            Dim connUserSave As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
            Dim cmdUserLib As SqlCommand = New SqlCommand(sqlUserSave, connUserSave)
            With cmdUserLib.Parameters
                .AddWithValue("@EmployeeID", txtEmployee_ID.Text)
                .AddWithValue("@FirstName", txtFirstName.Text)
                .AddWithValue("@MiddleName", txtMiddleName.Text)
                .AddWithValue("@LastName", txtLastName.Text)
                .AddWithValue("@UserName", txtUserName.Text)
                .AddWithValue("@Password", txtPassword.Text)
                .AddWithValue("@UserLevel", txtLevel.Text)
            End With
            connUserSave.Open()
            cmdUserLib.ExecuteNonQuery()
        Catch ex As SqlException
            'Throw ex
            lblMessage.Visible = True
            lblMessage.Text = "UserName already exist."
            txtUserName.Focus()
        End Try
    End Sub

    Protected Sub ClearAll()
        txtEmployee_ID.Text = ""
        txtFirstName.Text = ""
        txtMiddleName.Text = ""
        txtLastName.Text = ""
        txtUserName.Text = ""
        txtPassword.Text = ""
        txtLevel.Text = ""
        lblMessage.Visible = False
        lblPress.Visible = False
    End Sub

    Protected Sub cmdAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        EnableAll()
        ClearAll()
        GridView1.Enabled = False
        cmdAdd.Visible = False
        cmdDelete.Enabled = False
        cmdUpdate.Enabled = False
        cmdEdit.Enabled = False
        cmdSave.Visible = True
        cmdCancel.Visible = True
        GridView1.Enabled = False
    End Sub

    Protected Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        lblMessage.Visible = True
        lblMessage.Text = ""
        If txtEmployee_ID.Text = "" Then
            lblMessage.Text = "Please enter Employee ID."
            txtEmployee_ID.Focus()
        ElseIf txtFirstName.Text = "" Then
            lblMessage.Text = "Please enter First Name."
            txtFirstName.Focus()
        ElseIf txtMiddleName.Text = "" Then
            lblMessage.Text = "Please enter Middle Name."
            txtMiddleName.Focus()
        ElseIf txtLastName.Text = "" Then
            lblMessage.Text = "Please enter Last Name."
            txtLastName.Focus()
        ElseIf txtUserName.Text = "" Then
            lblMessage.Text = "Please enter UserName."
            txtUserName.Focus()
        ElseIf txtPassword.Text = "" Then
            lblMessage.Text = "Please enter Password."
            txtPassword.Focus()
        ElseIf txtLevel.Text = "Select" Then
            lblMessage.Text = "Please choose Access Level."
            txtLevel.Focus()
        Else
            If txtEmployee_ID.Text <> "" And txtFirstName.Text <> "" And txtMiddleName.Text <> "" And txtLastName.Text <> "" And txtUserName.Text <> "" And txtPassword.Text <> "" And txtLevel.Text <> "Select" Then
                'Try
                SaveUser()
                'Catch ex As SqlException
                '    lblMessage.Visible = True
                '    lblMessage.Text = "UserName already exist."
                '    txtUserName.Focus()
                'End Try
                Response.Redirect("~/Users.aspx")
            End If
        End If
    End Sub

    Protected Sub cmdEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdEdit.Click
        cmdDelete.Enabled = False
        cmdEdit.Visible = False
        cmdUpdate.Visible = True
        cmdCancel.Visible = True
        GridView1.Enabled = False
        EnableAll()
        txtUserName.Enabled = False
        txtEmployee_ID.Enabled = True
        txtFirstName.Focus()
        lblPress.Visible = False
    End Sub

    Protected Sub cmdDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
        lblMessage.Text = "Are you sure you want to delete this user?"
        cmdEdit.Enabled = False
        cmdCancel.Visible = True
        cmdDelete.Enabled = False
        GridView1.Enabled = False
        lblMessage.Visible = True
        cmdYes.Visible = True
        cmdNo.Visible = True
    End Sub
    Protected Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Response.Redirect("~/Users.aspx")
    End Sub
    Protected Sub cmdUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUpdate.Click

        Dim sqlUserUpdate As String = "UPDATE User_Library SET User_FName = @FirstName, User_MName = @MiddleName, User_LName = @LastName, User_Password = @Password, User_Level = @UserLevel WHERE User_Id = @UserName"
        Dim connUserUpdate As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
        Dim cmdUserLib As SqlCommand = New SqlCommand(sqlUserUpdate, connUserUpdate)
        With cmdUserLib.Parameters
            .AddWithValue("@EmployeeID", txtEmployee_ID.Text)
            .AddWithValue("@FirstName", txtFirstName.Text)
            .AddWithValue("@MiddleName", txtMiddleName.Text)
            .AddWithValue("@LastName", txtLastName.Text)
            .AddWithValue("@UserName", txtUserName.Text)
            .AddWithValue("@Password", txtPassword.Text)
            .AddWithValue("@UserLevel", txtLevel.Text)
        End With
        connUserUpdate.Open()
        cmdUserLib.ExecuteNonQuery()
        cmdUpdate.Enabled = False
        Response.Redirect("~/Users.aspx")
    End Sub

    Protected Sub cmdClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Response.Redirect("~/AdminMaintenancePage.aspx")
    End Sub

    Protected Sub cmdNo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNo.Click
        lblMessage.Visible = False
        cmdYes.Visible = False
        cmdNo.Visible = False
        cmdEdit.Enabled = True
        cmdDelete.Enabled = True
        GridView1.Enabled = True
    End Sub

    Protected Sub cmdYes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdYes.Click
        DeleteUser(txtUserName.Text)
        cmdEdit.Enabled = True
        cmdDelete.Enabled = True
        GridView1.Enabled = True
        cmdYes.Visible = False
        cmdNo.Visible = False
        Response.Redirect("~/Users.aspx")
    End Sub

    Protected Sub btnPrintUsers_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrintUsers.Click
        Dim sql As String = "SELECT * FROM User_Library"
        Dim conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
        Dim cmd As SqlCommand = New SqlCommand(sql, conn)
        Dim reader As SqlDataReader
        conn.Open()
        reader = cmd.ExecuteReader()

        ' check if there are results

        'If (reader.Read()) Then
        Try
            'Created the filestream document PDF
            Dim doc As New Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35)
            Dim writer As PdfWriter = PdfWriter.GetInstance(doc, New FileStream(Server.MapPath(".") & "\Attachments\Temp\printuser.pdf", FileMode.Create))

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
            title = "User Details"


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
                'doc.Add(New Paragraph("Employee ID  : " + reader("EmployeeID"), FontFactory.GetFont(FontFactory.TIMES, 12, iTextSharp.text.Font.NORMAL)))
                'doc.Add(New Phrase(Environment.NewLine))
                doc.Add(New Paragraph("First Name     : " + reader("User_FName"), FontFactory.GetFont(FontFactory.TIMES, 12, iTextSharp.text.Font.NORMAL)))
                'doc.Add(New Phrase(Environment.NewLine))
                doc.Add(New Paragraph("Middle Name : " + reader("User_MName"), FontFactory.GetFont(FontFactory.TIMES, 12, iTextSharp.text.Font.NORMAL)))
                'doc.Add(New Phrase(Environment.NewLine))
                doc.Add(New Paragraph("Last Name      : " + reader("User_LName"), FontFactory.GetFont(FontFactory.TIMES, 12, iTextSharp.text.Font.NORMAL)))
                'doc.Add(New Phrase(Environment.NewLine))
                doc.Add(New Paragraph("UserName      : " + reader("User_Id"), FontFactory.GetFont(FontFactory.TIMES, 12, iTextSharp.text.Font.NORMAL)))
                'doc.Add(New Phrase(Environment.NewLine))
                doc.Add(New Paragraph("User Level     : " + reader("User_Level"), FontFactory.GetFont(FontFactory.TIMES, 12, iTextSharp.text.Font.NORMAL)))
                'doc.Add(New Phrase(Environment.NewLine))

                doc.Add(New Phrase(Environment.NewLine))
                doc.Add(New Phrase(Environment.NewLine))
            Loop
            doc.Close()


        Catch ex As Exception
            Throw ex
        End Try

        'End If

        Dim path As String = Server.MapPath(".") & "\Attachments\"
        Dim array() As String = {"NAMRIA - " + "" + Session("UserName") + "", "" + DateTime.Now() + ""}
        AddWatermarkDoubleText(Server.MapPath(".") & "\Attachments\Temp\printuser.pdf", Server.MapPath(".") & "\Attachments\Temp\printuserwithmark.pdf", array)
        Response.Redirect("~/Attachments/Temp/printuserwithmark.pdf")

    End Sub

End Class
