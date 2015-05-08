<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage3.master" AutoEventWireup="false" CodeFile="Document Type.aspx.vb" Inherits="Document_Type" MaintainScrollPositionOnPostback="True"%>

<asp:Content ID="Content1" ContentPlaceHolderID="main" Runat="Server">
<center><h2><asp:Label ID="lblTitle" runat="server" Text="Document Type Library"> </asp:Label></h2></center>
<br />
    <table style="width: 712px">
        <tr>
            <td style="text-align: right ">
                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                                AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" 
                                DataSourceID="SqlDataSourceDocType" ForeColor="#333333" 
                                GridLines="None" Width="720px" 
                                 DataKeyNames="doctype_cd" EnableModelValidation="True">
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
                                <asp:BoundField DataField="doctype_cd" HeaderText="Document Code" 
                                        SortExpression="doctype_cd" ReadOnly="True" />
                                <asp:BoundField DataField="doctype_desc" HeaderText="Description" 
                                        SortExpression="doctype_desc"  />
                                <asp:BoundField DataField="code" HeaderText="Code" 
                                        SortExpression="code" />
                </Columns>
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                
                <asp:SqlDataSource ID="SqlDataSourceDocType" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:DOCTRACKConnectionString %>"
                                SelectCommand="SELECT * FROM [Doctype_Lib]">
                </asp:SqlDataSource>
                <center><asp:Label ID="lblCount" runat="server" Text="Label" style="font-weight: 700" 
                                Font-Bold="True"></asp:Label>
            </td>
        </tr>
    </table>
    <table style = "width:710px;">
        <tr>
            <td align ="right" >
                <asp:Button ID="btnPrint" runat="server" Text="Print Type Details" Width= "150px"/>
            </td>
        </tr>
        </table>
       
   <%--</table>                            
    <table style = "width:710px;">
        <tr>
            <td style="height: 30px; width: 130px">
                <asp:Label ID="lblID1" runat="server" Text="Document Code: " style="font-weight: 700" 
                    Font-Bold="True"></asp:Label>
            </td>
            <td style="height: 30px">
                <asp:Textbox ID="txtDoc_code1" runat="server" style="font-weight: 700" 
                    Font-Bold="True" Font-Size="Small" ForeColor="#FF3300" Enabled="False" 
                    MaxLength="3" Width="128px"></asp:Textbox>
             </td>
        </tr>
        <tr> 
             <td class="style5" style="width: 130px">
                <asp:Label ID="lblDescription1" runat="server" Text="Description            :" 
                    style="font-weight: 700" Font-Bold="True"></asp:Label>
             </td>
             <td>
                <asp:TextBox ID="txtDescription1" runat="server" Width="451px" Enabled="False" 
                     MaxLength="50"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style5" style="width: 130px">
                <asp:Label ID="lblcode1" runat="server" Text="Code: " style="font-weight: 700" 
                    Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtCode1" runat="server" Width="128px" Enabled="False" 
                     MaxLength="3"></asp:TextBox>
             </td>
        </tr>
    </table> --%>
    <br />
    <br />
    <table bgcolor="#f3f3f3" style="width:600px; border:solid #ffffff 10px;" border="5" cellpadding="10px" cellspacing="10px">
        <tr><td style="width:150px; background-color:#a9e3f7; text-align:right; padding-right:10px; font-weight:bold;">
                <asp:Label ID="lblID" runat="server" Text="Document Code: " Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style="background-color:#f3f3f3; text-align:left;">
        	    <asp:Textbox ID="txtDoc_code" runat="server" Width="335px" style="font-weight: 700" Font-Bold="True" Font-Size="Small" ForeColor="#FF3300" Enabled="False"></asp:Textbox>
            </td>
        </tr>
        <tr><td style="width:150px; background-color:#a9e3f7; text-align:right; padding-right:10px; font-weight:bold;">
                <asp:Label ID="lblDescription" runat="server" Text="Description            :" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style="background-color:#f3f3f3; text-align:left;">
                <asp:TextBox ID="txtDescription" runat="server" Width="335px" Enabled="False"></asp:TextBox>
            </td> 
        </tr>
        <tr><td style="width:150px; background-color:#a9e3f7; text-align:right; padding-right:10px; font-weight:bold;">
                <asp:Label ID="lblcode" runat="server" Text="Code: " Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style="background-color:#f3f3f3; text-align:left;">
                <asp:TextBox ID="txtCode" runat="server" Width="335px" Enabled="False"></asp:TextBox>
            </td> 
        </tr> 
    </table>
    <br />
    <table style="">
	<tr><td><asp:Button CssClass="button" ID="cmdAdd" OnClick = "cmdAdd_Click" runat="server" Text="ADD"/>
            <asp:Button CssClass="button" ID="cmdSave" OnClick = "cmdSave_Click" runat="server" Text="SAVE" Visible ="false"/>&nbsp;
            <asp:Button CssClass="button" ID="cmdEdit" OnClick = "cmdEdit_Click" runat="server" Text="EDIT" Enabled="False"/>
            <asp:Button CssClass="button" ID="cmdUpdate" OnClick = "cmdUpdate_Click" runat="server" Text="UPDATE" Visible="False"/>&nbsp;
            <asp:Button CssClass="button" ID="cmdDelete" OnClick = "cmdDelete_Click" runat="server" Text="DELETE" Enabled="False"/>
            <asp:Button CssClass="button" ID="cmdCancel" OnClick = "cmdCancel_Click" runat="server" Text="CANCEL" Visible="False"/>&nbsp;
		    <asp:Button CssClass="button" ID="cmdClose" OnClick = "cmdClose_Click" runat="server" Text="CLOSE" Enabled="True" /></td></tr>
    </table>
    <%--&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="cmdAdd1" runat="server" Text="Add Type" 
        OnClick = "cmdAdd_Click" Width= "100px"/>
            <asp:Button ID="cmdSave1" runat="server" Text="Save Type" 
        OnClick = "cmdSave_Click" Width= "100px" Visible ="false" />&nbsp;
            <asp:Button ID="cmdEdit1" runat="server" Text="Edit Type" OnClick = "cmdEdit_Click" 
                    Width= "100px" Enabled="False"/>
            <asp:Button ID="cmdUpdate1" runat="server" Text="Update" OnClick = "cmdUpdate_Click" 
                    Width= "100px" Visible="False"/>&nbsp;
            <asp:Button ID="cmdDelete1" runat="server" Text="Delete Type" 
                    OnClick = "cmdDelete_Click" Width= "100px" Enabled="False"/>&nbsp;
            <asp:Button ID="cmdCancel1" runat="server" Text="Cancel" OnClick = "cmdCancel_Click" Width= "100px" Visible="False"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="cmdClose1" runat="server" Text="Close" OnClick = "cmdClose_Click" Width= "100px"/>--%>
            </asp:Content>

