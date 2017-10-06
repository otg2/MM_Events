<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="MM_Events.About" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register assembly="Telerik.Web.UI.Skins" namespace="Telerik.Web.UI.Skins" tagprefix="telerikh" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <h3><%: User.Identity.GetUserName()  %></h3>
    <p>Þetta er heavy næs stuff</p>
    
    <asp:Label runat="server" ID="debug"></asp:Label>

    <telerik:RadDropDownList runat="server" ID="LookForUsers" DataTextField="UserName" DataValueField="UserName"></telerik:RadDropDownList>

    <telerik:RadDropDownList runat="server" ID="LookForRoles" DataTextField="RoleName" DataValueField="RoleName"></telerik:RadDropDownList>

    <telerik:radbutton runat="server" id="test" Text="Gurkfdfd" RenderMode="Lightweight" >
        <Icon PrimaryIconRight="5px" PrimaryIconCssClass="rbAdd" />
    </telerik:radbutton>
</asp:Content>
