<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPage3.master" CodeFile="Error.aspx.vb" Inherits="ErrorPage" title="Document Viewer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="main" Runat="Server">
    
    <body>
        <center >
        
        
           
            
                <br />
                <br />
                <center><asp:Label ID="lblWarning" runat="server" Text="Error displaying pdf. Please check if the link still exist." 
                Font-Bold="True" Font-Size ="Large"  ForeColor="#FF3300"></asp:Label>
                <br />
                <br />
                    <asp:LinkButton ID="lnkbtnBack" runat="server" PostBackUrl = "~/DocumentDetailsDisplay.aspx">BACK</asp:LinkButton>
                </center>

           
       
    </body>

    </asp:Content>
