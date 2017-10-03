<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="MM_Events.About" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register assembly="Telerik.Web.UI.Skins" namespace="Telerik.Web.UI.Skins" tagprefix="telerikh" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <h3>Óttar Guðmundsson</h3>
    <p>Þetta er heavy næs stuff</p>
    
    <telerik:radbutton runat="server" id="test" Text="Gurkfdfd" RenderMode="Lightweight" >
        <Icon PrimaryIconRight="5px" PrimaryIconCssClass="rbAdd" />
    </telerik:radbutton>
</asp:Content>
