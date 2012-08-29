<%@ Page Title="Stackato MSSQL Example" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="HelloStackato.HelloPage" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2> The following string was written to, and read from, an MSSQL database provisioned within stackato. </h2>
    <p>
       <asp:Label ID="databaseInfo" runat="server" />
    </p>
</asp:Content>
