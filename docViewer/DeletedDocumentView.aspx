<%@ Page Language="VB" MasterPageFile="~/MasterPage3.master" AutoEventWireup="false" CodeFile="DeletedDocumentView.aspx.vb" Inherits="DeletedDocumentView" title="Document Viewer" MaintainScrollPositionOnPostback="True"%>

<asp:Content ID="Content1" ContentPlaceHolderID="main" Runat="Server">

<center><h2><asp:Label ID="lblTitle" runat="server" Text="View Deleted Document(s)"> </asp:Label></h2></center>
<br />
    <table style="width: 712px">
    <tr>
        <td>
            <asp:GridView ID="GrdVwDeleted" runat="server" AllowPaging="True" 
                AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" 
                DataSourceID="SqlDataSourceDeleted" ForeColor="#333333" 
                GridLines="None" Width="720px" 
                DataKeyNames="doc_code">
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
                        <asp:BoundField DataField="doc_code" HeaderText="Identifier" ReadOnly="True" 
                            SortExpression="doc_code" />
                        <asp:BoundField DataField="doc_subject" HeaderText="Subject" 
                            SortExpression="doc_subject" />
                        <asp:BoundField DataField="doc_date" HeaderText="Date" SortExpression="doc_date" />
                        <asp:BoundField DataField="doctype_desc" HeaderText="Type" SortExpression="doctype_desc" />
                        <asp:BoundField DataField="status" HeaderText="Status" 
                            SortExpression="status" />
                        <asp:BoundField DataField="deletedby" HeaderText="Deleted by" 
                            SortExpression="deletedby" />
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
        <td>
        </td>
    </tr>
    
    <%--<tr>
        <td >
            <asp:Button ID="btnDelete1" runat="server" CausesValidation="False" style="font-family: Arial; font-size: x-small; font-weight: 700" Text="Permanently Delete Document"/>
            <asp:Button ID="btnRepost2" runat="server" CausesValidation="False" style="font-family: Arial; font-size: x-small; font-weight: 700" Text="Repost" />
            <asp:Button ID="btnClose2" runat="server" CausesValidation="False" style="font-family: Arial; font-size: x-small; font-weight: 700" Text="Close" /> &nbsp;&nbsp;
            <asp:Label ID="lblMessage1" runat="server" Text="Label" Font-Bold="True" Font-Size="Small" ForeColor="#0099FF" Visible="False"></asp:Label>&nbsp;
            <asp:TextBox ID="txtPassword1" runat="server" Visible="False" TextMode = "Password" ></asp:TextBox>&nbsp;
            <asp:Button ID="btnYes1" runat="server" Height="17px" Text="Yes" Font-Bold="True" Font-Size="XX-Small" Visible="False" Width="50px" />
            <asp:Button ID="btnNo1" runat="server" Height="17px" Text="No" Font-Bold="True" Font-Size="XX-Small" Visible="False" Width="50px" /> 
            
        </td>
    </tr>--%>
    </table> 
    <br />
    <br />

    <table bgcolor="#f3f3f3" style="width:600px; border:solid #ffffff 10px;" border="5" cellpadding="10px" cellspacing="10px">
        <tr><td style="width:150px; background-color:#a9e3f7; text-align:right; padding-right:10px; font-weight:bold;">
                <asp:Label ID="lblIdentifier" runat="server" Text="Document Code : " Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style="background-color:#f3f3f3; text-align:left;">
        	    <asp:Textbox ID="txtIdentifier" runat="server" Width="335px" style="font-weight: 700" Font-Bold="True" Font-Size="Small" ForeColor="#FF3300" Enabled="False"></asp:Textbox>
            </td>
        </tr>
        <tr><td style="width:150px; background-color:#a9e3f7; text-align:right; padding-right:10px; font-weight:bold;">
                <asp:Label ID="lblSubject" runat="server" Text="Subject  :" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style="background-color:#f3f3f3; text-align:left;">
                <asp:TextBox ID="txtSubject" runat="server" Width="335px" Enabled="False" TextMode ="MultiLine"></asp:TextBox>
            </td> 
        </tr>
        <tr><td style="width:150px; background-color:#a9e3f7; text-align:right; padding-right:10px; font-weight:bold;">
                <asp:Label ID="lblSignatory" runat="server" Text="Signatory  :" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style="background-color:#f3f3f3; text-align:left;">
                <asp:TextBox ID="txtSignatory" runat="server" Width="335px" Enabled="False"></asp:TextBox>
            </td> 
        </tr> 
        <tr><td style="width:150px; background-color:#a9e3f7; text-align:right; padding-right:10px; font-weight:bold;">
                <asp:Label ID="lblType" runat="server" Text="Type :" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style="background-color:#f3f3f3; text-align:left;">
                <asp:TextBox ID="txtType" runat="server" Width="335px" Enabled="False"></asp:TextBox>
            </td> 
        </tr> 
    </table>  
    <table style="">
	<tr><td><asp:Button CssClass="button" ID="btnDelete" runat="server" Text="Permanently Delete Document" Width="250px"/>&nbsp;
            <asp:Button CssClass="button" ID="btnRepost" runat="server" Text="Repost"/>&nbsp;
            <asp:Button CssClass="button" ID="btnClose" runat="server" Text="Close"/>
            <asp:Label ID="lblMessage" runat="server" Text="Label" Font-Bold="True" Font-Size="Small" ForeColor="#0099FF" Visible="False"></asp:Label>&nbsp;
            <asp:TextBox ID="txtPassword" runat="server" Visible="False" TextMode = "Password" ></asp:TextBox>&nbsp;
            <asp:Button ID="btnYes" runat="server" Height="17px" Text="Yes" Font-Bold="True" Font-Size="XX-Small" Visible="False" Width="50px" />
            <asp:Button ID="btnNo" runat="server" Height="17px" Text="No" Font-Bold="True" Font-Size="XX-Small" Visible="False" Width="50px" /> 
        </td>
    </tr>
    </table>                    
    
    <asp:SqlDataSource ID="SqlDataSourceDeleted" runat="server" 
        ConnectionString="<%$ ConnectionStrings:DOCTRACKConnectionString %>"
        SelectCommand="SELECT Temporary_Deleted_Document.doc_code, Temporary_Deleted_Document.doc_subject, Temporary_Deleted_Document.doc_date, Doctype_Lib.doctype_desc, Temporary_Deleted_Document.status, Temporary_Deleted_Document.deletedby FROM Temporary_Deleted_Document LEFT OUTER JOIN Doctype_Lib ON Temporary_Deleted_Document.doc_type = Doctype_Lib.doctype_cd">
    </asp:SqlDataSource>
    
</asp:Content>

