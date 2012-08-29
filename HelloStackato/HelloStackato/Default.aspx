<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="HelloStackato.HelloPage" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2> If you're distributed and you know it, clap your hands. </h2>
    <p>
       <asp:Label ID="databaseInfo" runat="server" />
    </p>
</asp:Content>
