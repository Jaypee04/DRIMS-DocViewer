<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AccessDenied.aspx.vb" Inherits="Pages_Default" title="Document Viewer" %>


                
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">

<title>Document and Record Information Management System</title>
    
<link href="adminStyle.css" rel="stylesheet" type="text/css" />
</head>

<body>

<div id="contentBody">
<form id="form1" runat="server" title="Document Viewer System">
    
    <table style="margin: 0 auto; padding-top:130px" border="0">
    	<tr><td colspan="2" style="text-align:center;"><asp:Image ID="Image2" runat="server" ImageUrl="App_Themes/Images/headerDRIMS.png" /></td></tr>
        <tr><td style="text-align:center;"><asp:Label ID="Label1" runat="server" Text="Access Denied! Please make sure that you are logged in." 
                Font-Bold="True" Font-Size ="Large"  ForeColor="#FF3300"></asp:Label></td></tr>
        <tr><td style="">
            	<table bgcolor="#f1f1f1" style="margin-top:30px; width:600px;" border="0">  
                                        	
            		<tr><td colspan="3" id="bgAccess"><asp:Label ID="Label10" runat="server"><h1>ACCESS CONFIRMATION<br /> <span style="font-size: 18px;">( DOCUMENT VIEWER )</span></h1></asp:Label></td></tr>
                    <tr><td>&nbsp;</td>
                  		<td>&nbsp;</td>
                  		<td rowspan="4" style="width:400px; border: #666666 1px dotted; padding:15px 5px 15px 25px;"><p>The <strong>Document Viewer</strong> provides a facility to add, update and browse internally and externally documents of NAMRIA.</p>
                  	  <p>Login to <a href="http://www.namria.gov.ph/rectrack/">Record Tracker</a>.</p></td></tr>
                    <tr><td style="width:100px; text-align:right; padding-left:20px;"><asp:Label ID="lblUsername" runat="server" Text="USERNAME:" ForeColor="Black"></asp:Label></td>
                        <td style="width: 100px; padding-right:15px;"><asp:TextBox ID="txtUsername" runat="server" height="18px" Font-Name="Verdana" Font-Size="11px" width="100px" TabIndex="1"></asp:TextBox></td></tr>
                    <tr><td style="text-align:right;"><asp:Label ID="lblPassword" runat="server" Text="PASSWORD:" ForeColor="Black"></asp:Label></td>
                        <td style="padding-right:15px;"><asp:TextBox ID="txtPassword" runat="server" height="18px" Font-Name="Verdana" Font-Size="11px" width="100px" TextMode="Password" TabIndex="2"></asp:TextBox></td></tr>
                    <tr><td style="padding-left:60px;" colspan="2">
                        <asp:Button CssClass="button" ID="btnLogIn" runat="server" Text="LOGIN"  Width="60px" Height="25" Font-Bold="True" TabIndex="3" />
                        <asp:Button ID="btnDrims" runat="server" Visible="False" Text="DRIMS" TabIndex="4" /></td></tr>
				</table><br />
                <asp:Label ID="LblErrorMsg" runat="server" style="color: #FF0000" Font-Bold="True" Font-Size="X-Small"></asp:Label>
            	<asp:Label ID="Label3" runat="server" Text="" ForeColor="Black" Visible="False"></asp:Label>
                <asp:LinkButton ID="lnkbtnDownload" runat="server" Visible="False" CSSclass ="link">Download User&#39;s Manual</asp:LinkButton>
                <asp:Label ID="Label2" runat="server" Visible="False" Text="Label"></asp:Label></td></tr>
    </table>
    
    </form>

    <br /><br /><br /><br /><br /><br />
<div class = "FooterText" style="padding: 10px 0px 10px 0px;">
             D O C U M E N T &nbsp;&nbsp; A N D &nbsp;&nbsp; R E C O R D &nbsp;&nbsp;  I N F O R M A T I O N  &nbsp;&nbsp; M A N A G E M E N T &nbsp;&nbsp; S Y S T E M
        <br />
        S y s t e m s &nbsp;&nbsp;  D e v e l o p m e n t  &nbsp;&nbsp; a n d  &nbsp;&nbsp; P r o g r a m m i n g &nbsp;&nbsp;  D i v i s i o n<br />
        I n f o r m a t i o n &nbsp;&nbsp;  M a n a g e m e n t  &nbsp;&nbsp; D e p a r t m e n t <br /><br /><br />                    
        
        <a href="">Download DOCVIEWER Manual</a>
</div>
</div>    
</body>
</html>   