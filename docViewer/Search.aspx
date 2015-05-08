<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Search.aspx.vb" Inherits="Pages_Search" title="Document Viewer" MaintainScrollPositionOnPostback="True"%>

<asp:Content ID="Content1" ContentPlaceHolderID="main" Runat="Server">

<table style="width: 669px">
        <tr>
            <td style="width: 371px">
                <table style="width: 100%">
                    <tr>
                        <td style="text-align: left">
                            <asp:Label ID="Label11" runat="server" style="text-align: left; font-weight: 700;" 
                                Text="Search" Width="147px" ForeColor="#3399FF"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                <table style="width: 2%">
                    <tr>
                        <td style="width: 48px">
                <asp:Label ID="Label1" runat="server" Text="Keyword" Width="47px"></asp:Label>
                        </td>
                        <td>
                <asp:TextBox ID="txtSearch" runat="server" 
                                style="font-family: Verdana; font-size: x-small; margin-left: 0px" 
                                Width="144px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btnSearch" runat="server" 
                                style="font-size: x-small; font-family: Verdana" Text="Search" />
                        </td>
                        <td>
                            <asp:Button ID="btnClear" runat="server" 
                                style="font-size: x-small; font-family: Verdana" Text="Clear" 
                                OnClick="btnClear_Click" Width="53px" /></td>
                    </tr>
                    <tr>
                        <td style="width: 48px">
                            &nbsp;</td>
                        <td>
                            <asp:Label ID="lblMessage" runat="server" Font-Bold="True" ForeColor="#FF3300" 
                                Text="Error" Visible="False"></asp:Label>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                        </td>
                    </tr>
                </table>
                        </td>
                    </tr>
                </table>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:DOCTRACKConnectionString %>" 
                    SelectCommand="SELECT [doc_code], [doc_subject] FROM [Documents] WHERE ([status]= 'Approved' AND [doc_subject] LIKE '%' + @Keyword + '%') AND [system_owner]='D'">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="txtSearch" Name="Keyword" 
                            PropertyName="Text" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" DataKeyNames="doc_code" DataSourceID="SqlDataSource2" 
                    ForeColor="#333333" GridLines="None" Width="724px" AllowPaging="True" AllowSorting="True">
                    <PagerSettings FirstPageImageUrl="~/App_Themes/Images/previewStart.png" 
                                    LastPageImageUrl="~/App_Themes/Images/previewEnd.png" 
                                    Mode="NextPreviousFirstLast" NextPageImageUrl="~/App_Themes/Images/next.png" 
                                    PreviousPageImageUrl="~/App_Themes/Images/preview.png" />
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                        <asp:CommandField ButtonType="Button" ShowSelectButton="True">
                            <ControlStyle Font-Size="X-Small" />
                        </asp:CommandField>
                        <asp:BoundField DataField="doc_code" HeaderText="Identifier" ReadOnly="True" 
                            SortExpression="doc_code" />
                        <asp:BoundField DataField="doc_subject" HeaderText="Subject" 
                            SortExpression="doc_subject" />
                        <asp:BoundField DataField="doc_keyword" HeaderText="Keyword" 
                            SortExpression="doc_keyword" Visible="False" />
                    </Columns>
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                <br />
                <br />
                
                <br />
            </td>
        </tr>
        </table>
                <center >
                <asp:Button ID="btnPrint" runat="server" Text="Print Result" 
                 Width= "210px" Enabled="False"/>
                <asp:Button ID="cmdClose" runat="server" Text="Browse Approved Documents" 
                 Width= "210px"/>
                </center>
</asp:Content>

