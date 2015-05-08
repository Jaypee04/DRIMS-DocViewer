<%@ Page Language="VB" MasterPageFile="~/MasterPage3.master" AutoEventWireup="false" CodeFile="PendingDocuments.aspx.vb" Inherits="Pages_PendingDocuments" title="Document Viewer" MaintainScrollPositionOnPostback="True"%>

<asp:Content ID="Content1" ContentPlaceHolderID="main" Runat="Server">

<center>
    <h2><asp:Label ID="Label5" runat="server" Text="Pending Document(s) for Posting"></asp:Label></h2>
    </center>
    <br />
    <br />
           
                            <asp:GridView ID="GrdVwPending" runat="server" AllowPaging="True" 
                                AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" 
                                DataSourceID="SqlDataSourcePending" ForeColor="#333333" 
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
                                        SortExpression="doc_code" ReadOnly="True" Visible="false" />
                                    <asp:BoundField DataField="user_id" HeaderText="Encoder" 
                                        SortExpression="user_id"  />
                                    <asp:BoundField DataField="doc_subject" HeaderText="Subject" 
                                        SortExpression="doc_subject"  />
                                    <asp:BoundField DataField="doc_date" HeaderText="Date" 
                                        SortExpression="doc_date" />
                                    <asp:BoundField DataField="status" HeaderText="Date" 
                                        SortExpression="status" Visible="false" />
                                </Columns>
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
                            <center><asp:Label ID="lblCount" runat="server" Text="Label" 
        ForeColor="Red"></asp:Label></center>
                            <br />
                            <asp:SqlDataSource ID="SqlDataSourcePending" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:DOCTRACKConnectionString %>"
                                SelectCommand="SELECT [doc_code], [user_id], [doc_subject], [doc_date], [status] FROM [Documents] WHERE ([status] = @Status1 and [system_owner]='D') or ([status] = @Status2 and [system_owner]='D')">
                                <SelectParameters>
                                    <asp:QueryStringParameter DefaultValue="Pending" Name="Status1" 
                                        QueryStringField="Status1" Type="String" />
                                    <asp:QueryStringParameter DefaultValue="For Approval" Name="Status2" 
                                        QueryStringField="Status2" Type="String" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                                                              
    <center >
        </center>

    <br />
    <br />

</asp:Content>

