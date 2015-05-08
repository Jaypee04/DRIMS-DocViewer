Imports System.Data.SqlClient
Imports System.Data
Imports AddWatermark

Partial Class LogIN
    Inherits System.Web.UI.Page

    Protected Sub form1_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Label2.Init
        Label2.Text = DateTime.Now()
        Label2.Visible = "false"
        Label3.Text = Date.Now.ToLongDateString
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtUsername.Focus()

    End Sub

    Protected Sub btnLogIn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogIn.Click
        Try
            Dim sql As String = "select * from User_Library where User_Id=@UserName AND RTRIM(User_Password)=@Password" '" & Me.txtUsername.Text & "' and Password='" & Me.txtPassword.Text & "'"
            Dim conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
            Dim cmd As SqlCommand = New SqlCommand(sql, conn)
            cmd.Parameters.AddWithValue("@UserName", txtUsername.Text)
            cmd.Parameters.AddWithValue("@Password", RTrim(txtPassword.Text))
            Dim reader As SqlDataReader
            Dim cLevel As Integer
            conn.Open()

            'Try
            reader = cmd.ExecuteReader()
            If (reader.Read()) Then
                Dim divi As String
                cLevel = reader("User_Level")
                Session("UserLevel") = reader("User_Level") 'cLevel
                Session("FirstName") = Strings.Left(RTrim(reader("User_FName")), 1)
                Session("LastName") = RTrim(reader("User_LName"))
                Session("EmployeeID") = reader("EmployeeID")
                Session("Password") = RTrim(txtPassword.Text)
                'divi = reader("Division_Code")


                Dim sqlUserLog As String = "Insert INTO User_Log_Viewer (UserName,LogInTime) Values (@UserName, @LogInTime)"
                Dim connUserLog As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
                Dim cmdUserLog As SqlCommand = New SqlCommand(sqlUserLog, connUserLog)
                With cmdUserLog.Parameters
                    .AddWithValue("@UserName", txtUsername.Text)
                    .AddWithValue("@LogInTime", Label2.Text)
                End With
                connUserLog.Open()
                cmdUserLog.ExecuteNonQuery()

                Session("UserName") = txtUsername.Text
                Session("LogInTime") = Label2.Text
                'sample with division watermark info
                'Dim sqlDepartment As String = "SELECT [Division_Name] FROM [Division_Library] Where [Division_Code]=@DivisionCode"
                'Dim connDepartment As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
                'Dim cmdDepartment As SqlCommand = New SqlCommand(sqlDepartment, connDepartment)
                'With cmdDepartment.Parameters
                '    .AddWithValue("@DivisionCode", divi)
                'End With
                'connDepartment.Open()
                'reader = cmdDepartment.ExecuteReader()
                'If (reader.Read()) Then

                '    Session("Department") = reader("Division_Name")

                'End If
                'ends here


                If Session("UserLevel") = "3" Then
                    Response.Redirect("~/Notification.aspx")
                ElseIf Session("UserLevel") = "5" Then
                    Response.Redirect("~/AdminMaintenancePage.aspx")
                ElseIf Session("UserLevel") = "1" Or Session("UserLevel") = "2" Then
                    Response.Redirect("~/Posting Notification.aspx")
                End If

            Else
                LblErrorMsg.Visible = True
                LblErrorMsg.Text = "Please Check your Username and/or Password."
                txtPassword.Text = ""

            End If

        Finally
            Dim conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DOCTRACKConnectionString").ConnectionString)
            conn.Close()
        End Try


    End Sub

    Protected Sub lnkbtnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnDownload.Click
        Dim array() As String = {"NAMRIA-DOCUMENT VIEWER", "" + DateTime.Now() + ""}
        AddWatermarkDoubleText(Server.MapPath(".") & "\Manual\DOCUMENT VIEWER SYSTEM (Orig).pdf", Server.MapPath(".") & "\Manual\DOCUMENT VIEWER SYSTEM.pdf", array)
        Response.Redirect("~/Manual/DOCUMENT VIEWER SYSTEM.pdf")
    End Sub

    Protected Sub btnDrims_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDrims.Click
        Response.Redirect("http://app.namria.gov.ph/DRIMS")
    End Sub
End Class
