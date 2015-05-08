<%@ Page Language="VB" MasterPageFile="~/MasterPage3.master" AutoEventWireup="false" CodeFile="AdvancedSearch.aspx.vb" Inherits="SampleSearch" title="Document Viewer" MaintainScrollPositionOnPostback="True"%>

<asp:Content ID="Content1" ContentPlaceHolderID="main" Runat="Server">

<br /><br /><br /><br />

<div id="bgHeader"><h1>SEARCH</h1></div>

<table style="">
                        <asp:SqlDataSource ID="SqlDataSourceSearch" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:DOCTRACKConnectionString %>" 
                    
                            SelectCommand="SELECT doc_code, doc_subject, CONVERT (VARCHAR, doc_date, 101) AS doc_date, doc_keyword FROM Documents WHERE (status = 'Approved') AND (system_owner = 'D') AND (doc_subject LIKE '%' + @Keyword + '%')" 
                            ProviderName="System.Data.SqlClient">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="txtSearch" Name="Keyword" 
                            PropertyName="Text" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
    
    
                <asp:SqlDataSource ID="SqlDataSourceSearchDate" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:DOCTRACKConnectionString %>" 
                    SelectCommand="SELECT [doc_code], [doc_subject], CONVERT(VARCHAR, [doc_date], 101) AS [doc_date] FROM [Documents] WHERE ([status]= 'Approved' AND [doc_date]>= @StartDate AND [doc_date]<=@EndDate) AND [system_owner]='D'">
                    <SelectParameters>
                        <asp:SessionParameter Name="StartDate" SessionField="StartDate" />
                        <asp:SessionParameter Name="EndDate" SessionField="EndDate" />        
                    </SelectParameters>
                </asp:SqlDataSource>
    
    
                <asp:SqlDataSource ID="SqlDataSourceSearchAll" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:DOCTRACKConnectionString %>" 
                    SelectCommand="SELECT [doc_code], [doc_subject], [doc_date], [doc_keyword] FROM [Documents] WHERE ([status]= 'Approved' AND ([doc_subject] LIKE '%' + @Keyword + '%') AND ([doc_date]>=@StartDate AND [doc_date]<=@EndDate)) AND [system_owner]='D'">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="txtSearch" Name="Keyword" 
                            PropertyName="Text" Type="String" />
                        <asp:SessionParameter Name="StartDate" SessionField="StartDate" />
                        <asp:SessionParameter Name="EndDate" SessionField="EndDate" /> 
                    </SelectParameters>
                </asp:SqlDataSource>
	<tr><td style="text-align: center "></td></tr>
</table>

<table style="width:600px; background-color:#FFFFFF; border:solid #ffffff 10px;">
	<tr bgcolor="#a9e3f7" height="50px" border="5" cellpadding="10px" cellspacing="10px"><td style="text-align: center ">
          	<asp:Button CssClass="button" ID="btnSearchbyKey" runat="server"  Text="by SUBJECT" Width="120px" />&nbsp;
          	<asp:Button CssClass="button" ID="btnSearchbyDate" runat="server" Text="by DATE" Width="80px" /></td></tr>
	<tr bgcolor="#f3f3f3"><td>
            <asp:Label ID="lblKey" runat="server" Text="Search" Visible="true" ForeColor ="Black" ></asp:Label>
            <asp:TextBox ID="txtSearch" runat="server" Width="150px" Visible="true" 
                TabIndex="1"></asp:TextBox>
            <asp:Button CssClass="button" ID="btnSearch" runat="server" Text="GO" 
                Visible="true" TabIndex="2" />
            <asp:Button CssClass="button" ID="btnDate" runat="server" Text="Search by Date" Visible="False"/></td></tr>
	<tr><td>
            <asp:Label ID="lblMessage" runat="server" Font-Bold="True" ForeColor="#FF3300" Text="Error" Visible="False"></asp:Label></td></tr>
</table>

<table style="width:600px; background-color:#FFFFFF; border:solid #ffffff 10px;">
	<tr bgcolor="#f3f3f3"><td style="">
            <asp:Label ID="lblFrom" runat="server" Text="" ForeColor ="Black"  Visible="False" >Start Date<br />(mm/dd/yyyy)</asp:Label>
            <asp:Calendar ID="clndrfrom" runat="server" Width="220px" BackColor="#cccccc" CellPadding="5" BorderStyle="Dotted" BorderColor="#0033CC" Font-Name="Century Gothic" Font-Size="8pt" ShowGridLines="true" DayNameFormat="FirstLetter" VisibleDate="2010-05-01" Visible="False">
            	<SelectedDayStyle BackColor="#ffffcc" Font-Size="10pt" ForeColor="#333333" />
                <TodayDayStyle BackColor="#f3f3f3"  />
                <OtherMonthDayStyle ForeColor="#e1e1e1" />
                <NextPrevStyle Font-Bold="True" ForeColor="#333333" Font-Size="8pt" />
                <DayHeaderStyle BackColor="#a9e3f7" Font-Bold="True" />
                <TitleStyle BackColor="#999999" Font-Bold="True" Font-Size="10pt" ForeColor="#ffffff" />
            </asp:Calendar></td>
		<td style="">
            <asp:Label ID="lblTo" runat="server" Text="" ForeColor ="Black" Visible="False" >End Date<br />(mm/dd/yyyy)</asp:Label>
            <asp:Calendar ID="clndrto" runat="server" Width="220px" BackColor="#cccccc" CellPadding="5" BorderStyle="Dotted" BorderColor="#0033CC" Font-Name="Century Gothic" Font-Size="8pt" ShowGridLines="true" DayNameFormat="FirstLetter" Visible="False">
            	<SelectedDayStyle BackColor="#ffffcc" Font-Size="10pt" ForeColor="#333333" />
                <TodayDayStyle BackColor="#f3f3f3"  />
                <OtherMonthDayStyle ForeColor="#e1e1e1" />
                <NextPrevStyle Font-Bold="True" ForeColor="#333333" Font-Size="8pt" />
                <DayHeaderStyle BackColor="#a9e3f7" Font-Bold="True" />
                <TitleStyle BackColor="#999999" Font-Bold="True" Font-Size="10pt" ForeColor="#ffffff" />
            </asp:Calendar></td></tr>
	<tr bgcolor="#f3f3f3"><td style="text-align:center;">
            <asp:TextBox ID="txtFrom" runat="server" Font-Name="Century Gothic" Font-Size="11px" Width="100px" Visible="False"></asp:TextBox></td>
        <td style="text-align:center;">
            <asp:TextBox ID="txtTo" runat="server" Font-Name="Century Gothic" Font-Size="11px" Width="100px" Visible="False"></asp:TextBox></td></tr>
	<tr bgcolor="#f3f3f3"><td colspan="2" style="text-align:center; height:50px;">
        <asp:Button CssClass="button" ID="btnSearchDate" runat="server" Text="SEARCH" Visible="False" />
        <asp:Button CssClass="button" ID="btnSearchAllEntry" runat="server" Visible="False" />
        <asp:Label ID="lblNo" runat="server" Text="No Record(s) found." Visible="False"></asp:Label></td></tr>
</table>
<br /><br />
<table>
	<tr><td style="text-align: left">        
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            AllowPaging="True" AllowSorting="True"  CellPadding="10" 
            DataKeyNames="doc_code" GridLines="None" Font-Names="Verdana" Font-Size="11px" 
            ForeColor="#333333" Width="600px" HeaderStyle-BackColor="#a9e3f7" 
            HeaderStyle-CssClass="gridHeader" SelectedRowStyle-BackColor="#a9e3f7" 
            BorderColor="#EEEEEE" BorderStyle="Solid" BorderWidth="1px" 
            DataSourceID="SqlDataSourceSearch">
        	<PagerSettings FirstPageImageUrl="~/App_Themes/Images/previewStart.png" LastPageImageUrl="~/App_Themes/Images/previewEnd.png" Mode="NextPreviousFirstLast" NextPageImageUrl="~/App_Themes/Images/next.png" PreviousPageImageUrl="~/App_Themes/Images/preview.png" />
            <FooterStyle backcolor="#a9e3f7" font-bold="True" forecolor="White" />
            <rowstyle backcolor="#EFF3FB" />
            	
            <Columns>
            	<asp:CommandField ButtonType="Button" ShowSelectButton="True">
                	<ControlStyle Width="80px" Font-Names="Verdana" Font-Size="11px" Font-Bold="True" Height="25px" />
                </asp:CommandField>
                <asp:BoundField DataField="doc_code" HeaderText="Identifier" SortExpression="doc_code" Visible="false"/>
                <asp:BoundField DataField="doc_subject" HeaderText="SUBJECT" SortExpression="doc_subject" />
                <asp:BoundField DataField="doc_date" HeaderText="DATE" SortExpression="doc_date"/>
                <asp:BoundField DataField="doc_keyword" HeaderText="Keyword" SortExpression="doc_keyword" Visible = "false"/>
            </Columns>            
			
			<editrowstyle backcolor="#ffffcc" />
			<selectedrowstyle backcolor="#D1DDF1" font-bold="True" forecolor="#333333" />
			<pagerstyle backcolor="#007fd0" forecolor="#ffffff" horizontalalign="Center" />
			<headerstyle backcolor="#007fd0" font-bold="True" forecolor="#ffffff" />
			<alternatingrowstyle backcolor="White" />
	</asp:GridView>        
        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
            AllowPaging="True" AllowSorting="True"  CellPadding="10" 
            DataKeyNames="doc_code" GridLines="None" Font-Names="Verdana" Font-Size="11px" 
            ForeColor="#333333" Width="600px" HeaderStyle-BackColor="#a9e3f7" 
            HeaderStyle-CssClass="gridHeader" SelectedRowStyle-BackColor="#a9e3f7" 
            BorderColor="#EEEEEE" BorderStyle="Solid" BorderWidth="1px" 
            DataSourceID="SqlDataSourceSearchDate" EnableModelValidation="True" 
            Visible="False">
        	<PagerSettings FirstPageImageUrl="~/App_Themes/Images/previewStart.png" LastPageImageUrl="~/App_Themes/Images/previewEnd.png" Mode="NextPreviousFirstLast" NextPageImageUrl="~/App_Themes/Images/next.png" PreviousPageImageUrl="~/App_Themes/Images/preview.png" />
            <FooterStyle backcolor="#a9e3f7" font-bold="True" forecolor="White" />
            <rowstyle backcolor="#EFF3FB" />
            	
            <Columns>
            	<asp:CommandField ButtonType="Button" ShowSelectButton="True">
                	<ControlStyle Width="80px" Font-Names="Verdana" Font-Size="11px" Font-Bold="True" Height="25px" />
                </asp:CommandField>
                <asp:BoundField DataField="doc_code" HeaderText="Identifier" SortExpression="doc_code" Visible="false"/>
                <asp:BoundField DataField="doc_subject" HeaderText="SUBJECT" SortExpression="doc_subject" />
                <asp:BoundField DataField="doc_date" HeaderText="DATE" SortExpression="doc_date"/>
                <asp:BoundField DataField="doc_keyword" HeaderText="Keyword" SortExpression="doc_keyword" Visible = "false"/>
            </Columns>            
			
			<editrowstyle backcolor="#ffffcc" />
			<selectedrowstyle backcolor="#D1DDF1" font-bold="True" forecolor="#333333" />
			<pagerstyle backcolor="#007fd0" forecolor="#ffffff" horizontalalign="Center" />
			<headerstyle backcolor="#007fd0" font-bold="True" forecolor="#ffffff" />
			<alternatingrowstyle backcolor="White" />
	</asp:GridView></td></tr>
</table>

<table><tr><td height="50px">
	<asp:Label ID="lblCount" runat="server" Text="Total Record(s) :  " ForeColor="Black" Visible="False"></asp:Label>
    <asp:Button CssClass="button" ID="btnPrint" runat="server" Text="PRINT" Width= "210px" Enabled="False" Visible ="false" />
    <asp:Button CssClass="button" ID="cmdClose" runat="server" Text="CLOSE" Width= "210px" Visible ="false"/></td></tr>
</table>
    
</asp:Content>

