<%@ Page Language="VB" MasterPageFile="~/MasterPage3.master" AutoEventWireup="false" CodeFile="Document Browse.aspx.vb" Inherits="Document_Browse" title="Document Viewer" MaintainScrollPositionOnPostback="True"%>
<asp:Content ID="Content1" ContentPlaceHolderID="main" Runat="Server">
    
<br /><br /><br />    
<table style="width:1000px;">
	<tr><td style="">
        <table style="">
        	<tr><td style="text-align: center;"><h1><asp:Label ID="Label11" runat="server" Text="BROWSE"> </asp:Label></h1></td></tr>
            <tr><td></td></tr>
            <tr><td style="text-align: left">
                
                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CellPadding="10" DataKeyNames="doc_code" DataSourceID="SqlDataSource2" GridLines="None" Width="100%" Font-Names="Verdana" Font-Size="11px" BorderColor="#666666" BorderStyle="dotted" BorderWidth="0px">
                <PagerSettings FirstPageImageUrl="~/App_Themes/Images/previewStart.png" LastPageImageUrl="~/App_Themes/Images/previewEnd.png" Mode="NextPreviousFirstLast" NextPageImageUrl="~/App_Themes/Images/next.png" PreviousPageImageUrl="~/App_Themes/Images/preview.png" />
                
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <Columns>
                    	<asp:CommandField ButtonType="Button" ShowSelectButton="True"><ControlStyle Width="80px" Font-Names="Verdana" Font-Size="11px" Font-Bold="True" Height="25px" /></asp:CommandField>
                        <asp:BoundField DataField="doc_code" HeaderText="Identifier" ReadOnly="True" SortExpression="doc_code" Visible="False" />
                        <asp:BoundField DataField="doc_subject" HeaderText="SUBJECT" SortExpression="doc_subject" />
                        <asp:BoundField DataField="doctype_desc" HeaderText="TYPE" SortExpression="doctype_desc" />
                        <asp:BoundField DataField="doc_keyword" HeaderText="Keyword" SortExpression="doc_keyword" Visible="False" />
                        <asp:BoundField DataField="doc_date" HeaderText="DATE" SortExpression="doc_date" DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="False" />
			<asp:BoundField DataField="user_id" HeaderText="ENCODER" SortExpression="user_id"/>
                        <asp:BoundField DataField="availability" HeaderText="Availability" SortExpression="availability" Visible="False" />
					</Columns>
                    
                	<RowStyle BackColor="#EFF3FB" />
                    <PagerStyle BackColor="#007fd0" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#007fd0" Font-Bold="True" ForeColor="White" Height="40px" Font-Size="12px" />
                    <EditRowStyle BackColor="#ffffcc" />
                    <AlternatingRowStyle BackColor="White" />
				</asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center; padding-top:20px; ">

                            <asp:Label ID="Label9" runat="server" Text="Label"></asp:Label>
                            
                            <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:DOCTRACKConnectionString %>" 
                                SelectCommand="SELECT * FROM [Documents]">
                            </asp:SqlDataSource>
                            
                                                        <!--
                            
                            <asp:FormView ID="FormView1" runat="server" DataKeyNames="doc_code" 
                                DataSourceID="SqlDataSource3" Height="16px" Width="337px">
                                
                                <ItemTemplate>
                                    <asp:Label ID="IdentifierLabel" runat="server" Text='<%# Eval("doc_code") %>' 
                                        Visible="False" />
                                    &nbsp;<asp:Label ID="SubjectLabel" runat="server" Text='<%# Bind("doc_subject") %>' 
                                        Visible="False" />
                                    &nbsp;<asp:Label ID="SignatoryLabel" runat="server" Text='<%# Bind("doc_signatory") %>' 
                                        Visible="False" />
                                    &nbsp;<asp:Label ID="PublisherLabel" runat="server" Text='<%# Bind("doc_publisher") %>' 
                                        Visible="False" />
                                    <asp:Label ID="KeywordLabel" runat="server" Text='<%# Bind("doc_keyword") %>' 
                                        Visible="False" />
                                    &nbsp;<asp:Label ID="DateLabel" runat="server" Text='<%# Bind("doc_date") %>' 
                                        Visible="False" />
                                    <br />
                                    <asp:Label ID="TypeLabel" runat="server" Text='<%# Bind("doctype_cd") %>' 
                                        Visible="False" />
                                    &nbsp;<asp:Label ID="AvailabilityLabel" runat="server" 
                                        Text='<%# Bind("availability") %>' Visible="False" />
                                    <br />
                                    <asp:Button ID="NewButton" runat="server" CausesValidation="False" 
                                        CommandName="New" 
                                        style="font-family: Arial; font-size: x-small; font-weight: 700" 
                                        Text="Add Record" onclick="NewButton_Click" />
                                </ItemTemplate>
                            </asp:FormView>
                            
                            -->
                            
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 93px">
                <table style="width: 18%">
                    <tr>
                        <td style="width: 122px">
                
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td style="margin-left: 40px" colspan="2">
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:DOCTRACKConnectionString %>" 
                               SelectCommand="SELECT Documents.doc_code, Documents.doc_subject, Documents.doc_date, Documents.user_id, Doctype_Lib.doctype_desc
                                                FROM Documents LEFT OUTER JOIN Doctype_Lib ON Documents.doctype_cd = Doctype_Lib.doctype_cd
                                                WHERE (Documents.status = 'Approved') AND (Documents.system_owner = 'D')
                                                ORDER BY Documents.doc_date DESC"> 
                            </asp:SqlDataSource>
                            <asp:Button CssClass="button" ID="btnPrint" runat="server" Text="Print All" Visible="False" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 122px">&nbsp;
                            </td>
                        <td style="margin-left: 40px">&nbsp;
                            </td>
                        <td>&nbsp;
                            </td>
                    </tr>
                    <tr>
                        <td colspan="2"><asp:Button CssClass="button" ID="Button3" runat="server" Text="VIEW ALL" Width="100px" /></td>
                        <td><asp:Button CssClass="button" ID="Button4" runat="server" Text="VIEW 10 PER PAGE" Width="185px" />
                        </td>
                    </tr>
                    </table>
            </td>
        </tr>
    </table>
</asp:Content>

