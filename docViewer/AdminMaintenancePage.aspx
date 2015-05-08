<%@ Page Language="VB" MasterPageFile="~/MasterPage3.master" AutoEventWireup="false" CodeFile="AdminMaintenancePage.aspx.vb" Inherits="Pages_AdminMainPage" Title="Document Viewer" MaintainScrollPositionOnPostback="True"%>

<asp:Content ID="Content1" ContentPlaceHolderID="main" Runat="Server">

<br /><br /><br /><br />

<div id="bgHeader"><h1>USER ACTIVITIES</h1></div> 

<table style="width:800px">
	<tr><td style="text-align: left">
    
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CellPadding="10" DataSourceID="SqlDataSourceAdmin" ForeColor="#333333" GridLines="None" Width="800px">
        
        <PagerSettings FirstPageImageUrl="~/App_Themes/Images/previewStart.png" LastPageImageUrl="~/App_Themes/Images/previewEnd.png" Mode="NextPreviousFirstLast" NextPageImageUrl="~/App_Themes/Images/next.png" PreviousPageImageUrl="~/App_Themes/Images/preview.png" />
        
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <RowStyle BackColor="#EFF3FB" />
        
        <Columns>
        	<asp:BoundField ItemStyle-Width = "200px" DataField="UserName" HeaderText="USER" SortExpression="UserName" />
            <asp:BoundField ItemStyle-Width = "500px" DataField="DateAccessed" HeaderText="DATE" SortExpression="DateAccessed" />
            <asp:BoundField ItemStyle-Width = "500px" DataField="ModuleAccessed" HeaderText="ACTIVITY" SortExpression="ModuleAccessed" />
            <asp:BoundField ItemStyle-Width = "500px" DataField="SearchedWord" HeaderText="Searched Word" SortExpression="SearchedWord" Visible = "false"/>
            <asp:BoundField ItemStyle-Width = "500px" DataField="FileViewed" HeaderText="DOCUMENT" SortExpression="FileViewed" />
            <asp:BoundField ItemStyle-Width = "500px" DataField="DocumentAdded" HeaderText="Document Added" SortExpression="DocumentAdded" Visible = "false" />
            <asp:BoundField ItemStyle-Width = "500px" DataField="OriginalDocument" HeaderText="Original Document" SortExpression="OriginalDocument" Visible = "false"  />
            <asp:BoundField ItemStyle-Width = "500px" DataField="UpdatedDocument" HeaderText="Updated Document" SortExpression="UpdatedDocument"  Visible = "false" />
            <asp:BoundField DataField="DeletedDocument" HeaderText="Deleted Document" SortExpression="DeletedDocument" Visible = "false" />
		</Columns>
    
    	<PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" />
        
        </asp:GridView></td></tr>
        
        <td><asp:Button ID="btnPrintActivity" runat="server" Text="Print Activity Details" /></td></tr>
        <tr><td><asp:SqlDataSource ID="SqlDataSourceAdmin" runat="server" ConnectionString="<%$ ConnectionStrings:DOCTRACKConnectionString %>" 
                	SelectCommand="SELECT * FROM [User_Activity_Viewer]">
                </asp:SqlDataSource></td></tr>
</table>

<table style="">
                    <tr>
                        <td style="width: 122px"><center>
                <asp:Label ID="lblCount" runat="server" Text="Label" style="font-weight: 700"></asp:Label></center>
                       </td> 
                       
                                
                                
                               
   </tr> 
   </table> 
   <center >
   <table>
   <tr>
       <td>
           <asp:Button ID="cmdViewAll" runat="server" 
                style="font-size: x-small; font-family: Arial; font-weight: 700;" Text="View all User Activities" Width="170px" />
       </td>
       <td>
            <asp:Button ID="cmdViewTen" runat="server" 
                  style="font-family: Arial; font-size: x-small; font-weight: 700;"  Width="170px" Text="View 10 Activities per page" />
       </td>
                    </tr>
                    </table>
                    </center>
 </asp:Content>