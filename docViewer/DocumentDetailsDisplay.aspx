<%@ Page Language="VB" MasterPageFile="~/MasterPage3.master" AutoEventWireup="false" CodeFile="DocumentDetailsDisplay.aspx.vb" Inherits="DocumentDetails" title="Document Viewer" MaintainScrollPositionOnPostback="True"%>
<asp:Content ID="Content1" ContentPlaceHolderID="main" Runat="Server">

<br /><br /><br /><br />

<div id="bgHeader"><h1>DOCUMENT DETAILS</h1></div>
   
<table style="width:600px;" border="0">
	<tr><td align ="right">
            <asp:Label ID="lblMessage" runat="server" Text="Label" ForeColor="#ff0000" Visible="False"></asp:Label>&nbsp;
            <asp:TextBox ID="txtPassword" runat="server" Visible="False" TextMode = "Password" MaxLength="15" ></asp:TextBox>&nbsp;
            <asp:Button CssClass="button" ID="cmdYes" runat="server" Text="YES" Visible="False" />&nbsp;
            <asp:Button CssClass="button" ID="cmdNo" runat="server" Text="NO" Visible="False" />&nbsp;&nbsp;&nbsp;
            <asp:Button CssClass="button" ID="btnPrint" runat="server" CausesValidation="False" Text="PRINT" Visible="False" />&nbsp;
            <asp:Button CssClass="button" ID="btnDelete" runat="server" 
                CausesValidation="False" Text="DENY" Visible="False" />&nbsp;
            <asp:Button CssClass="button" ID="btnApprove" runat="server" CausesValidation="False" Text="POST" Visible="False"/></td></tr>
    <tr><td>&nbsp;</td></tr>
	<tr><td><asp:SqlDataSource ID="SqlDataSource2" runat="server" 
    			ConnectionString="<%$ ConnectionStrings:DOCTRACKConnectionString %>" 
                SelectCommand="SELECT Documents.*, Doctype_Lib.doctype_desc FROM Documents LEFT OUTER JOIN Doctype_Lib ON Documents.doctype_cd = Doctype_Lib.doctype_cd WHERE (doc_code = @Identifier)" >
                   <SelectParameters>
                        <asp:ControlParameter ControlID="TextBox7" Name="Identifier" PropertyName="Text" />
                    </SelectParameters>
			</asp:SqlDataSource>
            <asp:FormView ID="FormView2" runat="server" DataKeyNames="doc_code" DataSourceID="SqlDataSource2" Width="725px">
            
            <ItemTemplate>
            	<table bgcolor="#f3f3f3" style="border:solid #ffffff 10px;" border="5" bordercolor="#ffffff" cellpadding="10px" cellspacing="10px">
                    <tr><td style="width:150px; background-color:#a9e3f7; text-align:right; padding-right:10px;">
                    		<asp:Label ID="Label3" runat="server" Text="Document Code :"></asp:Label></td>
                        <td style="background-color:#f3f3f3; text-align:left;">
                        	<asp:Label ID="IdentifierLabel" runat="server" Text='<%# Eval("doc_code") %>' />
                        	<asp:TextBox ID="TextBox6" runat="server"	Text='<%# Bind("availability") %>' Visible="False"></asp:TextBox></td></tr>
                    <tr><td style="width:150px; background-color:#a9e3f7; text-align:right; padding-right:10px;"><asp:Label ID="Label4" runat="server" Text="Subject :"></asp:Label></td>
                        <td style="background-color:#f3f3f3; text-align:left;"><asp:Label ID="SubjectLabel" runat="server" Text='<%# Bind("doc_subject") %>' Width="" /></td></tr>
                    <%--<tr><td style="width:150px; background-color:#a9e3f7; text-align:right; padding-right:10px;"><asp:Label ID="Label6" runat="server" Text="Publisher :"></asp:Label></td>
                        <td style="background-color:#f3f3f3; text-align:left;"><asp:Label ID="PublisherLabel" runat="server" Text='<%# Bind("doc_publisher") %>' Width="" /></td></tr>--%>
                    <tr><td style="width:150px; background-color:#a9e3f7; text-align:right; padding-right:10px;"><asp:Label ID="Label7" runat="server" Text="Date :"></asp:Label></td>
                        <td style="background-color:#f3f3f3; text-align:left;"><asp:Label ID="DateLabel" runat="server" Text='<%# Bind("doc_date") %>' /></td></tr>
                    <tr><td style="width:150px; background-color:#a9e3f7; text-align:right; padding-right:10px;"><asp:Label ID="Label9" runat="server" Text="Type :"></asp:Label></td>
                        <td style="background-color:#f3f3f3; text-align:left;"><asp:Label ID="TypeLabel" runat="server" Text='<%# Bind("doctype_desc") %>' Width="" /></td></tr>
                    <tr><td style="width:150px; background-color:#a9e3f7; text-align:right; padding-right:10px;"><asp:Label ID="Label10" runat="server" Text="Source URL :"></asp:Label></td>
                        <td style="background-color:#f3f3f3; text-align:left;"><asp:Label ID="AvailabilityLabel" runat="server" Text='<%# Bind("availability") %>' Width="" /></td></tr>
                        
				</table><br /><br />
                			<asp:Button CssClass="button" ID="btnClose" runat="server" CausesValidation="False" onclick="btnClose_Click" Text="CLOSE"/>
                        	<asp:Button CssClass="button" ID="btnPDF" runat="server" CausesValidation="False" Visible ="false"  PostBackUrl="~/ViewDocument.aspx" Text="VIEW" onclick="btnPDF_Click"/>
                        	<asp:Button CssClass="button" ID="EditButton" runat="server" CausesValidation="False" CommandName="Edit"  Text="EDIT" Visible="false"  />
                <asp:LinkButton ID="NewButton" runat="server" CausesValidation="False" CommandName="New" Text="New" Visible="False" />
			</ItemTemplate></asp:FormView></td></tr>
        <tr><td align = "left" >
            <asp:ImageButton ID="ImageButton1" runat="server" ToolTip="View PDF File" ImageUrl="~/App_Themes/Images/icon.jpg" onclick="ImageButton1_Click" Visible="False" />&nbsp;
            <asp:Label ID="lblWait" runat="server" Text="Loading file, please wait..." visible = "false"  style="font-weight: 12px;" ForeColor = "OrangeRed" ></asp:Label></td></tr>
        <tr><td><iframe src="" runat="server" id="pdfViewer" visible="False" ></iframe></td></tr>
        <tr><td><asp:TextBox ID="TextBox7" runat="server" Visible="False"></asp:TextBox></td></tr>
    </table>
       
</asp:Content>

