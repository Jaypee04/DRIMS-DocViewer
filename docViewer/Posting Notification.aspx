<%@ Page Language="VB" MasterPageFile="~/MasterPage3.master" AutoEventWireup="false" CodeFile="Posting Notification.aspx.vb" Inherits="Posting_Notification" title="Document Viewer" MaintainScrollPositionOnPostback="True" %>


<asp:Content ID="Content1" ContentPlaceHolderID="main" Runat="Server">

<br /><br /><br /><br />

<div id="bgHeader"><h1>ATTENTION!</h1></div>
    <br />
    <asp:Label ID="Label1" runat="server" Font-Bold="False" 
        Font-Names="Century Gothic" ForeColor="Black" Font-Size="12pt" 
        Text="You have" ></asp:Label>&nbsp;
    <asp:Label ID="lblCount" runat="server" Font-Bold="True" Font-Names="Century Gothic" ForeColor="Red" Text="Label" Font-Size="12pt"></asp:Label>&nbsp;
    <asp:Label ID="Label2" runat="server" Font-Bold="False" width ="650px"
        Font-Names="Century Gothic" ForeColor="Black" Font-Size="12pt" 
        Text="Document(s) that was/were denied by the Overall Document Controller to be posted. For more information about the said matter, please contact Ms. Z Leano of Records Section." ></asp:Label>
    <br /> 
    <br /> 
    <br />                   

<table><tr><td style="text-align:left;">
   
</table>

<table>
    <tr><td style="text-align:left;">
                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" 
                CellPadding="10" DataSourceID="SqlDataSourceDenied" GridLines="None" DataKeyNames="doc_code" EnableModelValidation="True" 
                Font-Names="Verdana" Font-Size="11px" ForeColor="#333333" Width="690px" PageSize="10" HeaderStyle-BackColor="#a9e3f7" 
                HeaderStyle-CssClass="gridHeader" SelectedRowStyle-BackColor="#a9e3f7" BorderColor="#eeeeee" BorderStyle="Solid" BorderWidth="1px">
	                <PagerSettings FirstPageImageUrl="~/App_Themes/Images/previewStart.png" LastPageImageUrl="~/App_Themes/Images/previewEnd.png" 
                    Mode="NextPreviousFirstLast" NextPageImageUrl="~/App_Themes/Images/next.png" PreviousPageImageUrl="~/App_Themes/Images/preview.png" />
                    <FooterStyle backcolor="#a9e3f7" font-bold="True" forecolor="White" />
                    <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                	    <%--<asp:CommandField ButtonType="Button" ShowSelectButton="True">
                		    <ControlStyle Width="80px" Font-Names="Verdana" Font-Size="11px" Font-Bold="True" Height="25px" />
                        </asp:CommandField>--%>
                        <asp:BoundField DataField="doc_code" HeaderText="Identifier" SortExpression="doc_code" Visible="false" ReadOnly="True" />
                        <asp:BoundField DataField="doc_subject" HeaderText="SUBJECT" SortExpression="doc_subject"  />
                        <asp:BoundField DataField="doc_date" HeaderText="DATE" SortExpression="doc_date" />
               	    </Columns>
                    <PagerStyle backcolor="#007fd0" forecolor="#ffffff" horizontalalign="Center" />
                    <SelectedRowStyle backcolor="#ffffcc" font-bold="False" forecolor="#333333" />
                    <HeaderStyle backcolor="#007fd0" font-bold="True" forecolor="#ffffff" />
                    <EditRowStyle BackColor="#ffffcc" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
         </td></tr>
</table>
                            
                <asp:SqlDataSource ID="SqlDataSourceDenied" runat="server" 
                     ConnectionString="<%$ ConnectionStrings:DOCTRACKConnectionString %>"
                     SelectCommand="SELECT [doc_code], [doc_subject], [doc_date] FROM [Documents] WHERE ([status] = @Status and [user_id]=@UserName and [system_owner]='D' or [status] = @Status2 and [user_id]=@UserName and [system_owner]='D')">
                     <SelectParameters>
                        <asp:QueryStringParameter DefaultValue="Denied-Unread" Name="Status" QueryStringField="Status" Type="String" />
                        <asp:QueryStringParameter DefaultValue="Denied-Read" Name="Status2" QueryStringField="Status" Type="String" />
                        <asp:SessionParameter Name="UserName" SessionField="UserName"/>
                     </SelectParameters>
                </asp:SqlDataSource>                           
          
    <br />
    <center><asp:Button CssClass="button" ID="btnClose" runat="server" 
            CausesValidation="False" Text="Close" Visible="True" /></center>
   
           
</asp:Content>

