<%@ Page Language="VB" MasterPageFile ="~/MasterPage3.master"  AutoEventWireup="false" CodeFile="Users.aspx.vb" Inherits="Pages_Users" Title="Document Viewer" MaintainScrollPositionOnPostback="True"%>

<asp:Content ID="Content1" ContentPlaceHolderID="main" Runat="Server">
<center>
    <h2><asp:Label ID="Label5" runat="server" Text="User Library"></asp:Label></h2>
    </center>
    <table style="width: 712px">
    <tr>
                        <td style="text-align: left">
                        
                            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                                AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" 
                                DataSourceID="SqlDataSourceUser" ForeColor="#333333" 
                                GridLines="None" Width="720px" DataKeyNames="EmployeeID">
                                <PagerSettings FirstPageImageUrl="~/App_Themes/Images/previewStart.png" 
                                    LastPageImageUrl="~/App_Themes/Images/previewEnd.png" 
                                    Mode="NextPreviousFirstLast" NextPageImageUrl="~/App_Themes/Images/next.png" 
                                    PreviousPageImageUrl="~/App_Themes/Images/preview.png" />
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <RowStyle BackColor="#EFF3FB" />
                                <Columns>
                                <asp:CommandField ButtonType="Button" ShowSelectButton="True">
                                        <ControlStyle Font-Names="Arial" Font-Size="X-Small" />
                                    </asp:CommandField>
                                    <asp:BoundField DataField="EmployeeID" HeaderText="EmployeeID" 
                                        SortExpression="EmployeeID" ReadOnly="True" />
                                    <asp:BoundField DataField="User_FName" HeaderText="FirstName" 
                                        SortExpression="User_FName"  />
                                    <asp:BoundField DataField="User_MName" HeaderText="MiddleName" 
                                        SortExpression="User_MName" />
                                    <asp:BoundField DataField="User_LName" HeaderText="LastName" 
                                        SortExpression="User_LName" />
                                    <asp:BoundField DataField="User_Id" HeaderText="UserName" 
                                        SortExpression="User_Id" />
                                    <asp:BoundField DataField="User_Level" HeaderText="UserLevel" 
                                        SortExpression="User_Level" />
                                </Columns>
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
                          
                            <asp:SqlDataSource ID="SqlDataSourceUser" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:DOCTRACKConnectionString %>"
                                SelectCommand="SELECT [EmployeeID], [User_FName], [User_MName], [User_LName], [User_Id], [User_Level] FROM [User_Library]">
                            </asp:SqlDataSource>
                            <center ><asp:Label ID="lblCount" runat="server" Text="Label" style="font-weight: 700" 
                                Font-Bold="True"></asp:Label></center>
                        </td>
                    </tr>
                    </table>
                     <table style="width: 51%">
                    <tr>
                        
                                <td align ="right" >
                                <asp:Button ID="btnPrintUsers" runat="server" Text="Print User Details" 
                                        Width= "150px"/>
                                </td>
   </tr> 
   </table>
   <br />

   <table bgcolor="#f3f3f3" style="width:600px; border:solid #ffffff 10px;" border="5" cellpadding="10px" cellspacing="10px">
        <tr><td style="width:150px; background-color:#a9e3f7; text-align:right; padding-right:10px; font-weight:bold;">
                <asp:Label ID="lblID" runat="server" Text="Employee ID: " Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style="background-color:#f3f3f3; text-align:left;">
                <asp:TextBox ID="TextBox1" runat="server" Width="9px" Visible = "false" ></asp:TextBox>
        	    <asp:Textbox ID="txtEmployee_ID" runat="server" Width="250px" style="font-weight: 700" MaxLength="7" Font-Bold="True" Font-Size="Small" ForeColor="#FF3300" Enabled="False"></asp:Textbox>
            </td>
        </tr>
        <tr><td style="width:150px; background-color:#a9e3f7; text-align:right; padding-right:10px; font-weight:bold;">
                <asp:Label ID="lblFirstName" runat="server" Text="First Name:" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style="background-color:#f3f3f3; text-align:left;">
                <asp:TextBox ID="txtFirstName" runat="server" Width="250px" Enabled="False"></asp:TextBox>
            </td> 
        </tr>
        <tr><td style="width:150px; background-color:#a9e3f7; text-align:right; padding-right:10px; font-weight:bold;">
                <asp:Label ID="lblMiddleName" runat="server" Text="Middle Name:" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style="background-color:#f3f3f3; text-align:left;">
                <asp:TextBox ID="txtMiddleName" runat="server" Width="250px" Enabled="False"></asp:TextBox>
            </td> 
        </tr> 
        <tr><td style="width:150px; background-color:#a9e3f7; text-align:right; padding-right:10px; font-weight:bold;">
                <asp:Label ID="lblLastName" runat="server" Text="Last Name:" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style="background-color:#f3f3f3; text-align:left;">
                <asp:TextBox ID="txtLastName" runat="server" Width="250px" Enabled="False"></asp:TextBox>
            </td> 
        </tr>
        <tr><td style="width:150px; background-color:#a9e3f7; text-align:right; padding-right:10px; font-weight:bold;">
                <asp:Label ID="lblUsername" runat="server" Text="UserName :" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style="background-color:#f3f3f3; text-align:left;">
                <asp:TextBox ID="txtUserName" runat="server" Width="250px" Enabled="False"></asp:TextBox>
            </td> 
        </tr>
        <tr><td style="width:150px; background-color:#a9e3f7; text-align:right; padding-right:10px; font-weight:bold;">
                <asp:Label ID="lblPassword" runat="server" Text="Password :" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style="background-color:#f3f3f3; text-align:left;">
                <asp:TextBox ID="txtPassword" runat="server" MaxLength="15" Width="250px" Enabled="False"></asp:TextBox>
            </td> 
        </tr> 
        <tr><td style="width:150px; background-color:#a9e3f7; text-align:right; padding-right:10px; font-weight:bold;">
                <asp:Label ID="lblUserLevel" runat="server" Text="User Level :" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style="background-color:#f3f3f3; text-align:left;">
                <asp:TextBox ID="txtLevel" runat="server" MaxLength="1" Width="150px" Enabled="False"></asp:TextBox>&nbsp;&nbsp;
                <asp:Label ID="lblPress" runat="server" Text="Press ENTER after this box." Font-Bold="True" Font-Size="Smaller" ForeColor="#FF9933" Visible="False"></asp:Label>
            </td> 
        </tr> 
    </table>  
    <table style="">
        <tr>
            <td>
               <center><asp:Label ID="lblMessage" runat="server" Text="Label" Font-Bold="True" 
                       Font-Size="Small" ForeColor="#0099FF" Visible="False"></asp:Label>&nbsp;&nbsp;
                    <asp:Button ID="cmdYes" runat="server" Height="17px" Text="Yes" 
                       Font-Bold="True" Font-Size="XX-Small" Visible="False" Width="50px" />
                    <asp:Button ID="cmdNo" runat="server" Height="17px" Text="No" Font-Bold="True" Font-Size="XX-Small" 
                       Visible="False" Width="50px" />
               </center>   
            </td>
        </tr>     
    </table>
    <table style="">
	<tr><td><asp:Button CssClass="button" ID="cmdAdd" OnClick = "cmdAdd_Click" runat="server" Text="ADD"/>
            <asp:Button CssClass="button" ID="cmdSave" OnClick = "cmdSave_Click" runat="server" Text="SAVE" Visible ="false"/>&nbsp;
            <asp:Button CssClass="button" ID="cmdEdit" OnClick = "cmdEdit_Click" runat="server" Text="EDIT" Enabled="False"/>
            <asp:Button CssClass="button" ID="cmdUpdate" OnClick = "cmdUpdate_Click" runat="server" Text="UPDATE" Visible="False"/>&nbsp;
            <asp:Button CssClass="button" ID="cmdDelete" OnClick = "cmdDelete_Click" runat="server" Text="DELETE" Enabled="False"/>
            <asp:Button CssClass="button" ID="cmdCancel" OnClick = "cmdCancel_Click" runat="server" Text="CANCEL" Visible="False"/>&nbsp;
		    <asp:Button CssClass="button" ID="cmdClose" OnClick = "cmdClose_Click" runat="server" Text="CLOSE" Enabled="True" /></td></tr>
    </table>

    <table style="width: 762px">
        <%--<tr>
            <td style="text-align: left">
            <asp:Label ID="lblID" runat="server" Text="Employee ID: " style="font-weight: 700" 
                    Font-Bold="True"></asp:Label>
                <asp:TextBox ID="TextBox1" runat="server" Width="16px" Visible = "false" ></asp:TextBox>
            <asp:Textbox ID="txtEmployee_ID" runat="server" style="font-weight: 700" 
                    Font-Bold="True" Font-Size="Small" ForeColor="#FF3300" Enabled="False" 
                    MaxLength="7"></asp:Textbox>
            </td>  
        </tr>
        
        <tr>    
            <td style="text-align: left">
            <asp:Label ID="lblFirstName" runat="server" Text="First Name:" 
                    style="font-weight: 700" Font-Bold="True"></asp:Label>
            <asp:TextBox ID="txtFirstName" runat="server" Width="150px" Enabled="False"></asp:TextBox>
                &nbsp;&nbsp;
            
            <asp:Label ID="lblMiddleName" runat="server" Text="Middle Name:" 
                    style="font-weight: 700" Font-Bold="True"></asp:Label>
            <asp:TextBox ID="txtMiddleName" runat="server" Width="150px" Enabled="False"></asp:TextBox>
                &nbsp;&nbsp;
            
            <asp:Label ID="lblLastName" runat="server" Text="Last Name:" 
                    style="font-weight: 700" Font-Bold="True"></asp:Label>
            <asp:TextBox ID="txtLastName" runat="server" Width="150px" Font-Bold="False" 
                    Enabled="False"></asp:TextBox>
            </td>                       
        </tr>  
        
        <tr>     
            <td style="text-align: left">
            <asp:Label ID="lblUsername" runat="server" Text="UserName :" 
                    style="font-weight: 700" Font-Bold="True"></asp:Label>
                    
            <asp:TextBox ID="txtUserName" runat="server" Width="150px" Enabled="False"></asp:TextBox>
                &nbsp;&nbsp;
            
            <asp:Label ID="lblPassword" runat="server" Text="Password :" 
                    style="font-weight: 700" Font-Bold="True"></asp:Label>
                    &nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="txtPassword" runat="server" Width="150px" 
                    Enabled="False" MaxLength="15"></asp:TextBox>
                &nbsp;&nbsp;
            
            <asp:Label ID="lblUserLevel" runat="server" Text="User Level :" 
                    style="font-weight: 700" Font-Bold="True"></asp:Label>
            <asp:TextBox ID="txtLevel" runat="server" Width="30px" Font-Bold="False" 
                    Enabled="False" MaxLength="1"></asp:TextBox>
            <asp:Label ID="lblPress" runat="server" Text="Press ENTER after this box." 
                    Font-Bold="True" Font-Size="Smaller" ForeColor="#FF9933" Visible="False"></asp:Label>
            </td>                       
        </tr>--%>  
           
        <%--<tr>
            <td>
            <asp:Button ID="cmdAdd" runat="server" Text="Add User" OnClick = "cmdAdd_Click" Width= "100px"/>&nbsp;
            <asp:Button ID="cmdSave" runat="server" Text="Save User" OnClick = "cmdSave_Click" Width= "100px" Visible ="false" />&nbsp;
            <asp:Button ID="cmdEdit" runat="server" Text="Edit User" OnClick = "cmdEdit_Click" 
                    Width= "100px" Enabled="False"/>
            <asp:Button ID="cmdUpdate" runat="server" Text="Update" OnClick = "cmdUpdate_Click" 
                    Width= "100px" Visible="False"/>&nbsp;
            <asp:Button ID="cmdDelete" runat="server" Text="Delete User" 
                    OnClick = "cmdDelete_Click" Width= "100px" Enabled="False"/>&nbsp;
            <asp:Button ID="cmdCancel" runat="server" Text="Cancel" OnClick = "cmdCancel_Click" Width= "100px" Visible="False"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
            <asp:Button ID="cmdClose" runat="server" Text="Close" OnClick = "cmdClose_Click" Width= "100px"/>
            </td>
        </tr>    --%>
    </table>

</asp:Content>