<%@ Page Language="VB" MasterPageFile="~/MasterPage3.master" AutoEventWireup="false" CodeFile="Documents.aspx.vb" Inherits="Pages_Documents" title="Document Viewer" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="main" Runat="Server">

<br /><br /><br /><br />

<div id="bgHeader"><h1>MANAGE</h1></div>
<table style="" border="0">
    <tr><td style="text-align:center;">
    
		<asp:Label ID="lblCount" runat="server" Text="Label" style="" Font-Bold="False" ForeColor="Black"></asp:Label><br /><br /></td></tr>
    
    <tr><td style="text-align:left;">
    
    	<asp:GridView ID="GrdVwDocuments" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CellPadding="10" DataSourceID="SqlDataSourceDocuments" GridLines="None" DataKeyNames="doc_code" Font-Names="Verdana" Font-Size="11px" ForeColor="#333333" Width="690px" PageSize="10" HeaderStyle-BackColor="#a9e3f7" HeaderStyle-CssClass="gridHeader" SelectedRowStyle-BackColor="#a9e3f7" BorderColor="#eeeeee" BorderStyle="Solid" BorderWidth="1px">
            	<PagerSettings FirstPageImageUrl="~/App_Themes/Images/previewStart.png" LastPageImageUrl="~/App_Themes/Images/previewEnd.png" Mode="NextPreviousFirstLast" NextPageImageUrl="~/App_Themes/Images/next.png" PreviousPageImageUrl="~/App_Themes/Images/preview.png" />
                
                <FooterStyle backcolor="#a9e3f7" font-bold="True" forecolor="White" />
                <RowStyle BackColor="#EFF3FB" />                
                	<Columns>
                    	<asp:CommandField ButtonType="Button" ShowSelectButton="True">
                			<ControlStyle Width="80px" Font-Names="Verdana" Font-Size="11px" Font-Bold="True" Height="25px" />
                        </asp:CommandField>
                        <asp:BoundField DataField="doc_code" HeaderText="IDENTIFIER" SortExpression="doc_code" Visible="false" />
                        <asp:BoundField DataField="doc_subject" HeaderText="SUBJECT" SortExpression="doc_subject" />
                        <asp:BoundField DataField="doc_date" HeaderText="DATE" SortExpression="doc_date" />
					</Columns>
                
				<PagerStyle backcolor="#007fd0" forecolor="#ffffff" horizontalalign="Center" />
                <SelectedRowStyle backcolor="#ffffcc" font-bold="False" forecolor="#333333" />
                <HeaderStyle backcolor="#007fd0" font-bold="True" forecolor="#ffffff" />
                <EditRowStyle BackColor="#ffffcc" />
                <AlternatingRowStyle BackColor="White" />
			</asp:GridView>
    
    	<asp:GridView ID="GrdVwPending" runat="server" AllowPaging="True" 
            AllowSorting="True" AutoGenerateColumns="False" CellPadding="10" 
            DataSourceID="SqlDataSourcePending" GridLines="None" DataKeyNames="doc_code" 
            Font-Names="Verdana" Font-Size="11px" ForeColor="#333333" Width="690px" 
            HeaderStyle-BackColor="#a9e3f7" HeaderStyle-CssClass="gridHeader" 
            SelectedRowStyle-BackColor="#a9e3f7" BorderColor="#EEEEEE" BorderStyle="Solid" 
            BorderWidth="1px" Visible="False" EnableModelValidation="True">
            	<PagerSettings FirstPageImageUrl="~/App_Themes/Images/previewStart.png" LastPageImageUrl="~/App_Themes/Images/previewEnd.png" Mode="NextPreviousFirstLast" NextPageImageUrl="~/App_Themes/Images/next.png" PreviousPageImageUrl="~/App_Themes/Images/preview.png" />
                
                <FooterStyle backcolor="#a9e3f7" font-bold="True" forecolor="White" />
                <RowStyle BackColor="#EFF3FB" />                
                	<Columns>
                    	<asp:CommandField ButtonType="Button" ShowSelectButton="True">
                			<ControlStyle Width="80px" Font-Names="Verdana" Font-Size="11px" Font-Bold="True" Height="25px" />
                        </asp:CommandField>
                        <asp:BoundField DataField="doc_code" HeaderText="IDENTIFIER" SortExpression="doc_code" Visible="false" />
                        <asp:BoundField DataField="user_id" HeaderText="SUBJECT" SortExpression="user_id" />
                        <asp:BoundField DataField="doc_subject" HeaderText="SUBJECT" SortExpression="doc_subject" />
                        <asp:BoundField DataField="doc_date" HeaderText="DATE" SortExpression="doc_date" />
					</Columns>
                
				<PagerStyle backcolor="#007fd0" forecolor="#ffffff" horizontalalign="Center" />
                <SelectedRowStyle backcolor="#ffffcc" font-bold="False" forecolor="#333333" />
                <HeaderStyle backcolor="#007fd0" font-bold="True" forecolor="#ffffff" />
                <EditRowStyle BackColor="#ffffcc" />
                <AlternatingRowStyle BackColor="White" />
			</asp:GridView></td></tr>
    <tr><td><asp:SqlDataSource ID="SqlDataSourceDocuments" runat="server" ConnectionString="<%$ ConnectionStrings:DOCTRACKConnectionString %>" 
            	SelectCommand="SELECT doc_code, doc_subject, doc_date FROM Documents WHERE ([user_id]=@UserName AND [status] ='For Approval' OR [user_id]=@UserName AND [status] ='Pending')">
            	<SelectParameters>
            		<asp:SessionParameter Name="UserName" SessionField="UserName" />
            	</SelectParameters>
            </asp:SqlDataSource>
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
            </td></tr>
</table>
    <br />
    <br />
<table style="">
	<tr><td><asp:Button CssClass="button" ID="cmdAdd" runat="server" Text="ADD"/>
            <asp:Button CssClass="button" ID="cmdSave" runat="server" Text="SAVE" Visible ="false" TabIndex="12" />&nbsp;
            <asp:Button CssClass="button" ID="cmdEdit" runat="server" Text="EDIT" Enabled="False"/>
            <asp:Button CssClass="button" ID="cmdUpdate" runat="server" Text="UPDATE" Visible="False"/>&nbsp;
            <asp:Button CssClass="button" ID="cmdDelete" runat="server" Text="DELETE" Enabled="False" Visible="False"/>
            <asp:Button CssClass="button" ID="cmdCancel" runat="server" Text="CANCEL" Visible="False"/>&nbsp;
		    <asp:Button CssClass="button" ID="btnPost" runat="server" Text="DENIED" Enabled="True" /></td></tr>
</table>
<br />
<table bgcolor="#f3f3f3" style="width:600px; border:solid #ffffff 10px;" border="5" cellpadding="10px" cellspacing="10px">
        	<asp:Label ID="lblIdentifier" runat="server" Text="Document Code :" style="" Font-Bold="True" ForeColor="Black" Visible ="false"  ></asp:Label>
        	<asp:Textbox ID="txtIdentifier" runat="server" Width="250px" Visible="false" style="" Font-Bold="True" Font-Size="Small" ForeColor="#FF3300" Enabled="False"></asp:Textbox>
            <asp:Label ID="lblIdentifierCode" runat="server" Text="Label" Visible="False"></asp:Label>
            <asp:Label ID="lblDateFormat" runat="server" Text="Label" Font-Bold="True" Font-Size="X-Small" ForeColor="#FF3300" Visible="False"></asp:Label>
	<tr><td style="width:150px; background-color:#a9e3f7; text-align:right; padding-right:10px; padding-left:100px; font-weight:bold;">    
            <asp:Label ID="Label1" runat="server" Text="Agency :"></asp:Label></td>
        <td style="background-color:#f3f3f3; text-align:left;">
			<asp:DropDownList Width="450px" ID="drpdwnlstAgency" runat="server" 
                Enabled="False" AutoPostBack="True" DataSourceID="SqlDataSourceAgency" 
                DataTextField="agency_nm" DataValueField="agency_cd" TabIndex="1" Font-Name="Verdana"></asp:DropDownList>
            <asp:TextBox ID="txtAgency" runat="server" Width="101px" Enabled="False" Visible="false"></asp:TextBox></td></tr>
	<tr><td style="width:150px; background-color:#a9e3f7; text-align:right; padding-right:10px; padding-left:100px; font-weight:bold;">
        	<asp:Label ID="Label2" runat="server" Text="Office :"></asp:Label></td>
        <td style="background-color:#f3f3f3; text-align:left;">
        	<asp:DropDownList Width="450px" Font-Name="Verdana" ID="drpdwnlstDivision" runat="server" 
                Enabled="False" AutoPostBack="True" DataSourceID="SqlDataSourceDivision" 
                DataTextField="Division_Name" DataValueField="Division_Code" TabIndex="2"></asp:DropDownList>
            <asp:TextBox ID="txtDivision" runat="server" Width="101px" Font-Name="Verdana" Enabled="False" Visible="false"></asp:TextBox>&nbsp;
        	<asp:Label ID="lblNA1" runat="server" Text="Not Applicable" Font-Size="12px" Visible="False"></asp:Label></td></tr>
    <tr><td style="width:150px; background-color:#a9e3f7; text-align:right; padding-right:10px; font-weight:bold;">
            <asp:Label ID="lblSubject" runat="server" Text="Subject  :" Font-Bold="True" ForeColor="Black"></asp:Label></td>
        <td style="background-color:#f3f3f3; text-align:left;">
        	<asp:TextBox ID="txtSubject" runat="server" Width="445px" Font-Name="Verdana" Font-Size="12px" Enabled="False" TextMode ="MultiLine" TabIndex="3"></asp:TextBox></td></tr>
            <tr><td style="width:150px; background-color:#a9e3f7; text-align:right; padding-right:10px; font-weight:bold;">
            <asp:Label ID="Label3" runat="server" Text="Date  :" Font-Bold="True" 
                    ForeColor="Black"></asp:Label></td>
        <td style="background-color:#f3f3f3; text-align:left;">
            <asp:TextBox ID="txtDate" runat="server" Width="136px" Font-Name="Verdana" 
                Font-Bold="False" Enabled="False" TabIndex="6"></asp:TextBox>&nbsp;&nbsp;
            <asp:Label ID="lblDateForm" runat="server" Text="(mm/dd/yyyy)"></asp:Label>
                </td></tr>
    <tr><td style="width:150px; background-color:#a9e3f7; text-align:right; padding-right:10px; font-weight:bold;">    
            <asp:Label ID="lblSignatory" runat="server" Text="Signatory  :" Font-Bold="True" ForeColor="Black"></asp:Label></td>
        <td style="background-color:#f3f3f3; text-align:left;">
            <asp:DropDownList Width="450px" Font-Name="Verdana" ID="drpdwnlstSignatory" runat="server" Enabled="False" 
                AutoPostBack="True" DataSourceID="SqlDataSourceSignatory" DataTextField="Full" 
                DataValueField="EMP_ID" TabIndex="4"></asp:DropDownList>
            <asp:Label ID="lblSign" runat="server" Text="signatory" Visible="False"></asp:Label>
            <asp:TextBox ID="txtSignatory" runat="server" Width="445px" Font-Name="Verdana" Visible = "False" 
                Enabled="False" TabIndex="5"></asp:TextBox></td></tr>
	<%--<tr><td style="width:150px; background-color:#a9e3f7; text-align:right; padding-right:10px; font-weight:bold;"> 
            <asp:Label ID="lblPublisher" runat="server" Text="Publisher  :" style="" Font-Bold="True" ForeColor="Black"></asp:Label></td>
        <td style="background-color:#f3f3f3; text-align:left;">
            <asp:TextBox ID="txtPublisher" runat="server" Width="445px" Font-Name="Verdana" Font-Bold="False" Enabled="False" TabIndex="6"></asp:TextBox></td></tr>--%>
    <tr><td style="width:150px; background-color:#a9e3f7; text-align:right; padding-right:10px; font-weight:bold;">
            <asp:Label ID="lblType" runat="server" Text="Type :" style="" Font-Bold="True" ForeColor="Black"></asp:Label></td>
        <td style="background-color:#f3f3f3; text-align:left;">
            <asp:DropDownList Width="450px" ID="drpdwnlstType" runat="server" Font-Name="Verdana" Enabled="False" 
                AutoPostBack="True" DataSourceID="SqlDataSourceType" 
                DataTextField="doctype_desc" DataValueField="doctype_cd" TabIndex="7"></asp:DropDownList>
            <asp:TextBox ID="txtType" runat="server" Width="97px" Enabled="False" Visible="False"></asp:TextBox></td></tr>
    <tr><td style="width:150px; background-color:#a9e3f7; text-align:right; padding-right:10px; font-weight:bold;">
            <asp:Label ID="lblOrigin" runat="server" Text="Origin :" Font-Bold="True" ForeColor="Black"></asp:Label></td>
        <td style="background-color:#f3f3f3; text-align:left;">                
                <asp:RadioButton ID="rdbtnInternal" Checked="true"  runat="server" 
                    Text="Internal" ForeColor="Black" GroupName="OriginGroup" TabIndex="8" 
                    Enabled="False" AutoPostBack="True"/>
                <asp:RadioButton ID="rdbtnExternal" runat="server" 
                    Text="External" ForeColor="Black" GroupName="OriginGroup" Enabled="False" 
                    AutoPostBack="True"/>
                <asp:TextBox ID="txtValue" runat="server" Enabled="False" Visible = "false" ></asp:TextBox></td><%--<asp:Button ID="cmdClose" runat="server" Text="Close" Width= "116px"/>--%></tr>  
	<tr><td style="width:150px; background-color:#a9e3f7; text-align:right; padding-right:10px; font-weight:bold;">
            <asp:Label ID="lblAvailability" runat="server" Text="Source URL :" 
                Font-Bold="True" ForeColor="Black"></asp:Label></td>
        <td style="background-color:#f3f3f3; text-align:left;">
            <asp:TextBox ID="txtAvailability" runat="server" Width="445px" Font-Name="Verdana" Font-Bold="False" Enabled="False" Visible="False" TabIndex="9">http://</asp:TextBox>&nbsp;&nbsp;
            <asp:Label ID="lblPress" runat="server" Text="Press ENTER after this box." Font-Bold="True" Font-Size="X-Small" ForeColor="#FF3300" Visible="False"></asp:Label>
        	<asp:Label ID="lblNA2" runat="server" Text="Not Applicable" Font-Size="12px"></asp:Label></td></tr>  
	<tr><td style="width:150px; background-color:#a9e3f7; text-align:right; padding-right:10px; font-weight:bold;">
            <asp:Label ID="lblFile" runat="server" Text="PDF File :" style="" 
                Font-Bold="True"></asp:Label></td>
        <td style="background-color:#f3f3f3; text-align:left;">
            <asp:FileUpload BackColor="#ffffff" ID="FileUpload1" runat="server" width="360px" height="20px" Font-Name="Verdana" Font-Size="12px"  Enabled="False" Visible="False" TabIndex="10" />
            <asp:Button CssClass="button" ID="btnUpload" runat="server" Text="UPLOAD" Enabled="False" Visible="False" TabIndex="11" />
			<asp:Label ID="lblUploaded" runat="server" Text="PDF File Uploaded" Font-Bold="True" Font-Size="X-Small" ForeColor="#FF3300" Visible="False"></asp:Label>&nbsp;
        	<asp:Label ID="lblNA3" runat="server" Text="Not Applicable" Font-Size="12px" 
                Visible="False"></asp:Label></td></tr> 
	<tr><td colspan="2" style="background-color:#f3f3f3; text-align:center;">
            <asp:Label ID="lblMessage" runat="server" Text="Label" ForeColor="#ff0000" Visible="False"></asp:Label>&nbsp;&nbsp;
            <asp:TextBox ID="txtPassword" runat="server" Visible="False" TextMode = "Password" ></asp:TextBox>&nbsp;
            <asp:Button CssClass="button" ID="cmdYes" runat="server" Height="17px" Text="YES" Font-Bold="True" Font-Size="XX-Small" Visible="False" Width="50px" />
            <asp:Button CssClass="button" ID="cmdNo" runat="server" Height="17px" Text="NO" Font-Bold="True" Font-Size="XX-Small" Visible="False" Width="50px" /></td></tr> 
</table> 

<table style="width: 712px">       
	<tr><td><!--OnClick = "cmdSave_Click"--></td>
            <!--OnClick = "cmdEdit_Click"-->
            <!--OnClick = "cmdUpdate_Click"-->
            <!--OnClick = "cmdDelete_Click"-->
            <!--&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;-->
            <!--OnClick = "cmdCancel_Click"-->
            <!--OnClick = "cmdClose_Click"--></tr>    
    </table>

    <asp:SqlDataSource ID="SqlDataSourceAgency" runat="server" ConnectionString="<%$ ConnectionStrings:DOCTRACKConnectionString %>" 
    	SelectCommand="SELECT * FROM [Agency_Lib]">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSourceDivision" runat="server" ConnectionString="<%$ ConnectionStrings:DOCTRACKConnectionString %>" 
        SelectCommand="SELECT [Division_Code], [Division_Name] FROM [Division_Library]">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSourceType" runat="server" ConnectionString="<%$ ConnectionStrings:DOCTRACKConnectionString %>" 
         SelectCommand="SELECT [doctype_cd], [doctype_desc] FROM [Doctype_Lib]">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSourceSignatory" runat="server" ConnectionString="<%$ ConnectionStrings:DOCTRACKConnectionString %>" 
         SelectCommand="SELECT DIV_C, FIRST_M, MIDDLE_M, LAST_M, EMP_ID, LAST_M + N', ' + FIRST_M + N' ' + MIDDLE_M  AS [Full] FROM Signatories">
    </asp:SqlDataSource>

</asp:Content>