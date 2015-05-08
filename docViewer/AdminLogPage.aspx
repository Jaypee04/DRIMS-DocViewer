<%@ Page Language="VB" MasterPageFile="~/MasterPage3.master" AutoEventWireup="false" CodeFile="AdminLogPage.aspx.vb" Inherits="Pages_AdminLogPage" Title="Document Viewer" MaintainScrollPositionOnPostback="True"%>

<asp:Content ID="Content1" ContentPlaceHolderID="main" Runat="Server">
        <center><h2><asp:Label ID="lblTitle" runat="server" Text="View Log Page"> </asp:Label></h2></center>
                    <br />
                    <br />
                    <center>
                    <asp:LinkButton ID="lnkbtnViewAll" runat="server" CssClass = "link " Font-Size="Small"  Font-Bold="True">View All User Log</asp:LinkButton>
                    <br />
                    <br />
                    <asp:LinkButton ID="lnkbtnDate" runat="server" CssClass = "link " Font-Size="Small"  Font-Bold="True">Search by Date</asp:LinkButton>
                    </center>
        <br />
        <br />

        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
       <center> 
       <table>
        <tr>
            <td style="width: 193px">
                <asp:Label ID="lblFrom" runat="server" Text="From (mm/dd/yyyy):" Width="47px" 
                    ForeColor ="Black" Visible="False" ></asp:Label>
                <asp:Calendar ID="clndrfrom" runat="server" BackColor="#3399FF" 
                    BorderColor="#0033CC" ForeColor="#993333" VisibleDate="2010-05-01" 
                    Visible="False" Width="125px">
                    <SelectedDayStyle BackColor="#FF0066" />
                </asp:Calendar>
                
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td style="width: 193px">
                <asp:Label ID="lblTo" runat="server" Text="To (mm/dd/yyyy):" Width="47px" 
                    ForeColor ="Black" Visible="False" ></asp:Label>
                <asp:Calendar ID="clndrto" runat="server" BackColor="#3399FF" 
                    BorderColor="#0033CC" ForeColor="#993333" Visible="False" Width="37px">
                    <SelectedDayStyle BackColor="#FF0066" />
                </asp:Calendar>
                
            </td>
           
        </tr>
        <tr>
            <td style="width: 193px">
                <asp:TextBox ID="txtFrom" runat="server" style="font-family: Verdana; font-size: x-small; margin-left: 0px" 
                                Width="144px" Visible="False"></asp:TextBox>
            </td>
            <td style="width: 12px">
                &nbsp;&nbsp;
            </td>
            <td style="width: 12px">
                &nbsp;&nbsp;
            </td>
            <td>
                <asp:TextBox ID="txtTo" runat="server" style="font-family: Verdana; font-size: x-small; margin-left: 0px" 
                                Width="144px" Visible="False"></asp:TextBox>
            </td>
        </tr>
    </table>
    </center>
    <center >
    <asp:Button ID="btnSearchDate" runat="server" 
            style="font-size: x-small; font-family: Verdana" Text="Search" 
            Width="55px" Visible="False" />
            <asp:Button ID="btnClear" runat="server" 
            style="font-size: x-small; font-family: Verdana" Text="Clear" 
            Width="55px" Visible="False" />
            <br />
            <asp:Label ID="lblMessage" runat="server" Text="Error" 
        Width="302px" ForeColor ="Red"  Visible="False" Font-Bold="True" 
                Font-Size="Small" ></asp:Label>   
    </center>           
        
    
    
    
                <asp:SqlDataSource ID="SqlDataSourceSearchDate" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:DOCTRACKConnectionString %>" 
                    SelectCommand="SELECT [UserName], [LogInTime], [LogOffTime] FROM [User_Log_Viewer] WHERE ([LogInTime]>= @StartDate AND [LogInTime]<=@EndDate)">
                    <SelectParameters>
                        <asp:SessionParameter Name="StartDate" SessionField="StartDate" />
                        <asp:SessionParameter Name="EndDate" SessionField="EndDate" />        
                    </SelectParameters>
                </asp:SqlDataSource>
                               
                        
    <table style="width: 712px">
<tr>
                        <td style="text-align: left">
                        
                            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                                AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" 
                                GridLines="None" Width="720px"  
                                DataKeyNames="UserName">
                                <PagerSettings FirstPageImageUrl="~/App_Themes/Images/previewStart.png" 
                                    LastPageImageUrl="~/App_Themes/Images/previewEnd.png" 
                                    Mode="NextPreviousFirstLast" NextPageImageUrl="~/App_Themes/Images/next.png" 
                                    PreviousPageImageUrl="~/App_Themes/Images/preview.png" />
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <RowStyle BackColor="#EFF3FB" />
                                <Columns>
                        <asp:BoundField DataField="UserName" HeaderText="UserName"  ReadOnly="True"
                            SortExpression="UserName" />
                        <asp:BoundField DataField="LogInTime" HeaderText="Log-In Time" 
                            SortExpression="LogInTime" />
                        <asp:BoundField DataField="LogOffTime" HeaderText="Log-Off Time" 
                            SortExpression="LogOffTime"/>
                        </Columns>
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
                            </td>
                            </tr>
                            <tr>
                            <td align = "right" >
                            <asp:Button ID="btnPrintLog" runat="server" Text="Print Log Details" 
                                        Width= "150px" Visible="False"/>
                            <asp:SqlDataSource ID="SqlDataSourceAdmin" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:DOCTRACKConnectionString %>"
                                SelectCommand="SELECT * FROM [User_Log_Viewer] ORDER BY [LogInTime]">
                            </asp:SqlDataSource>
                         
                        </td>
                    </tr>
                     </table>
                     <table style="width: 18%">
                    <tr>
                        <td style="width: 122px"><center>
                <asp:Label ID="lblCount" runat="server" Text="Total Log(s) :  " style="font-weight: 700" 
                                ForeColor="Black" Visible="False"></asp:Label></center>
                        </td>
   </tr> 
   </table> 
   <center>
   <table>
   <tr>
                        <td>
                            <asp:Button ID="cmdViewAll" runat="server" 
                                style="font-size: x-small; font-family: Arial; font-weight: 700;" Text="View all User Logs" 
                                Width="170px" Visible="False" />
                        </td>
                        <td>
                            <asp:Button ID="cmdViewTen" runat="server" 
                                style="font-family: Arial; font-size: x-small; font-weight: 700;" 
                                Width="170px" Text="View 10 User Logs per page" Visible="False" />
                        </td>
                    </tr>
                    </center>
                    </table>
                    
 </asp:Content>