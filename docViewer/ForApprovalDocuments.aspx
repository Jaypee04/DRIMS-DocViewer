<%@ Page Language="VB" MasterPageFile="~/MasterPage3.master" AutoEventWireup="false" CodeFile="ForApprovalDocuments.aspx.vb" Inherits="Pages_ForApprovalDocuments" title="Document Viewer" MaintainScrollPositionOnPostback="True"%>

<asp:Content ID="Content1" ContentPlaceHolderID="main" Runat="Server">


    <center>
    <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Names="Verdana" 
            ForeColor= "#FF6600" Text="Document(s) for Posting" Font-Size="18pt"></asp:Label>
    </center>
    <br />
    <br />
           
                            <asp:GridView ID="GrdVwApproval" runat="server" AllowPaging="True" 
                                AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" 
                                DataSourceID="SqlDataSourceApproval" ForeColor="#333333" 
                                GridLines="None" Width="720px" 
        DataKeyNames="doc_code" PageSize="5">
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
                                    <asp:BoundField DataField="doc_code" HeaderText="Identifier" 
                                        SortExpression="doc_code" ReadOnly="True" />
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
                            <asp:Label ID="lblCount" runat="server" Text="Label" 
        ForeColor="Red"></asp:Label>
                            <br />
                            <asp:SqlDataSource ID="SqlDataSourceApproval" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:DOCTRACKConnectionString %>"
                                SelectCommand="SELECT [doc_code], [doc_subject], [doc_date] FROM [Documents] WHERE ([status] = @Status) and [system_owner] = 'D'">
                                <SelectParameters>
                                    <asp:QueryStringParameter DefaultValue="For Approval" Name="Status" 
                                        QueryStringField="Status" Type="String" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                                               
         <center>
             </center>
         <br />
          
                                                              
    

           
                                                                          
    

</asp:Content>

