<%@ Page Language="VB" MasterPageFile="~/MasterPage3.master" AutoEventWireup="false" CodeFile="Notification.aspx.vb" Inherits="Pages_Notification" title="Document Viewer" MaintainScrollPositionOnPostback="True"%>

<asp:Content ID="Content1" ContentPlaceHolderID="main" Runat="Server">

<br /><br /><br /><br />

<div id="bgHeader"><h1>ATTENTION!</h1></div>
    <br />
    <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Century Gothic" ForeColor="Black" Font-Size="12pt" Text="You have" ></asp:Label>&nbsp;
    <asp:Label ID="lblCount" runat="server" Font-Bold="True" Font-Names="Century Gothic" ForeColor="Red" Text="Label" Font-Size="12pt"></asp:Label>&nbsp;
    <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Century Gothic" ForeColor="Black" Font-Size="12pt" Text="pending document(s) for posting." ></asp:Label>
    <br /> 
    <br /> 
    <br />                   

<table><tr><td style="text-align:left;">
<asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CellPadding="10" DataSourceID="SqlDataSourcePending" GridLines="None" DataKeyNames="doc_code" EnableModelValidation="True" Font-Names="Verdana" Font-Size="11px" ForeColor="#333333" Width="690px" PageSize="10" HeaderStyle-BackColor="#a9e3f7" HeaderStyle-CssClass="gridHeader" SelectedRowStyle-BackColor="#a9e3f7" BorderColor="#eeeeee" BorderStyle="Solid" BorderWidth="1px">

	<PagerSettings FirstPageImageUrl="~/App_Themes/Images/previewStart.png" LastPageImageUrl="~/App_Themes/Images/previewEnd.png" Mode="NextPreviousFirstLast" NextPageImageUrl="~/App_Themes/Images/next.png" PreviousPageImageUrl="~/App_Themes/Images/preview.png" />
                
                <FooterStyle backcolor="#a9e3f7" font-bold="True" forecolor="White" />
                <RowStyle BackColor="#EFF3FB" />
                
                <Columns>
                	<asp:CommandField ButtonType="Button" ShowSelectButton="True">
                		<ControlStyle Width="80px" Font-Names="Verdana" Font-Size="11px" Font-Bold="True" Height="25px" />
                    </asp:CommandField>
                    <asp:BoundField DataField="user_id" HeaderText="PUBLISHER" SortExpression="user_id" ReadOnly="True" />
                    <asp:BoundField DataField="doc_code" HeaderText="Identifier" SortExpression="doc_code" Visible="false" ReadOnly="True" />
                    <asp:BoundField DataField="doc_subject" HeaderText="SUBJECT" SortExpression="doc_subject"  />
                    <asp:BoundField DataField="doc_date" HeaderText="DATE" SortExpression="doc_date" />
               	</Columns>
                
                <PagerStyle backcolor="#007fd0" forecolor="#ffffff" horizontalalign="Center" />
                <SelectedRowStyle backcolor="#ffffcc" font-bold="False" forecolor="#333333" />
                <HeaderStyle backcolor="#007fd0" font-bold="True" forecolor="#ffffff" />
                <EditRowStyle BackColor="#ffffcc" />
                <AlternatingRowStyle BackColor="White" />
</asp:GridView></td></tr>
</table>
                            
                            <asp:SqlDataSource ID="SqlDataSourcePending" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:DOCTRACKConnectionString %>"
                                SelectCommand="SELECT [doc_code], [user_id], [doc_subject], [doc_date] FROM [Documents] WHERE ([status] = @Status2 and [system_owner]='D' Or [status] = @Status) and [system_owner]='D'">
                                <SelectParameters>
                                    <asp:QueryStringParameter DefaultValue="Pending" Name="Status2" 
                                        QueryStringField="Status2" Type="String" />
                                    <asp:QueryStringParameter DefaultValue="For Approval" Name="Status" 
                                        QueryStringField="Status" Type="String" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            
   <%-- <br />
    <br />
                          
    <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="Black" Font-Size="14pt" Text="You Have" ></asp:Label>&nbsp;
    <asp:Label ID="lblCount2" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="Red" Text="Label" Font-Size="14pt"></asp:Label>&nbsp;
    <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="Verdana" 
        ForeColor="Black" Font-Size="14pt" Text="New Document(s) for Posting" ></asp:Label>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button 
        ID="btnViewApproval" runat="server" Text="View" 
        PostBackUrl = "~/ForApprovalDocuments.aspx"/>
    <br />
    <br />
           
                            <asp:GridView ID="GridView2" runat="server" AllowPaging="True" 
                                AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" 
                                DataSourceID="SqlDataSourceApproval" ForeColor="#333333" 
                                GridLines="None" Width="720px" 
        DataKeyNames="user_id" PageSize="5">
                                <PagerSettings FirstPageImageUrl="~/App_Themes/Images/previewStart.png" 
                                    LastPageImageUrl="~/App_Themes/Images/previewEnd.png" 
                                    Mode="NextPreviousFirstLast" NextPageImageUrl="~/App_Themes/Images/next.png" 
                                    PreviousPageImageUrl="~/App_Themes/Images/preview.png" />
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <RowStyle BackColor="#EFF3FB" />
                                <Columns>
                                    <asp:BoundField DataField="user_id" HeaderText="Encoder" 
                                        SortExpression="user_id" ReadOnly="True" />
                                    <asp:BoundField DataField="doc_subject" HeaderText="Subject" 
                                        SortExpression="doc_subject"  />
                                    <asp:BoundField DataField="doc_date" HeaderText="Date" 
                                        SortExpression="doc_date" />
                                </Columns>
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
            
                            <asp:SqlDataSource ID="SqlDataSourceApproval" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:DOCTRACKConnectionString %>"
                                SelectCommand="SELECT [user_id], [doc_subject], [doc_date] FROM [Documents] WHERE ([status] = @Status) and [system_owner]='D'">
                                <SelectParameters>
                                    <asp:QueryStringParameter DefaultValue="For Approval" Name="Status" 
                                        QueryStringField="Status" Type="String" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                                                              
    <br />--%>
    
    
   
           
</asp:Content>

