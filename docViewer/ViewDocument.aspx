<%@ Page Language="VB" MasterPageFile="~/MasterPage3.master" AutoEventWireup="false" CodeFile="ViewDocument.aspx.vb" Inherits="ViewDocument" title="Document Viewer" MaintainScrollPositionOnPostback="True"%>

<asp:Content ID="Content1" ContentPlaceHolderID="main" Runat="Server">

<br /><br /><br /><br />

<div id="bgHeader"><h1>PDF VIEWER</h1></div> 

<table bgcolor="#f3f3f3" style="width:800px; border:solid #ffffff 10px;" border="5" bordercolor="#ffffff" cellpadding="10px" cellspacing="10px">
	<tr><td style="width:150px; background-color:#a9e3f7; text-align:right; padding-right:10px; font-weight:bold;">TYPE :</td>
        <td style="background-color:#f3f3f3; text-align:left;"><asp:Label ID="lblType" runat="server" Text="TYPE"></asp:Label></td></tr>
    <tr><td style="width:150px; background-color:#a9e3f7; text-align:right; padding-right:10px; font-weight:bold;">SUBJECT :</td>
        <td style="background-color:#f3f3f3; text-align:left;"><asp:Label ID="lblSubject" runat="server" Text="SUBJECT"></asp:Label></td></tr>
</table>

<br /><br />    
<table style="width:800px;">
	<tr><td style="text-align:right; padding-right:20px; height:45px;">
    	<asp:Label ID="lblMessage" runat="server" Text="Label" ForeColor="#ff0000" Visible="False"></asp:Label>&nbsp;
        <asp:TextBox ID="txtPassword" runat="server" Visible="False" TextMode = "Password" MaxLength="15" ></asp:TextBox>&nbsp;
        <asp:Button CssClass="button" ID="cmdYes" runat="server" Text="YES" Visible="False" />&nbsp;
        <asp:Button CssClass="button" ID="cmdNo" runat="server" Text="NO" Visible="False" />&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button CssClass="button" ID="btnDelete" runat="server" CausesValidation="False" Text="DELETE" Visible="True" />&nbsp;&nbsp;
        <asp:Button CssClass="button" ID="btnPrint" runat="server" CausesValidation="False" Text="PRINT" />&nbsp;&nbsp;
        <asp:Button CssClass="button" ID="btnClose" runat="server" CausesValidation="False" Text="CLOSE"/></td></tr>
	<tr><td><iframe src="" style="width:800px; height: 1200px;" runat="server" id="pdfViewer" visible="true" ></iframe></td></tr>
</table> 
</asp:Content>

