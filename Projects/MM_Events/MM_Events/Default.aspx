<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MM_Events._Default" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register assembly="Telerik.Web.UI.Skins" namespace="Telerik.Web.UI.Skins" tagprefix="telerikh" %>



<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <telerik:RadScriptBlock runat="server" ID="RadScriptblock1">
        <script type="text/javascript">

            function OpenEditForm(sender, args)
            {
                alert("test");
            }

            function testFunction(sender, args)
            {
                alert("bla");
            }

        </script>
    </telerik:RadScriptBlock>


    <telerik:RadAjaxManager runat="server" ID="SupplierAfstemmingar_AjaxManager">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="Radgrid_Events">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="Radgrid_Events" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="RadButton_AddAction">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="debug" />
                <telerik:AjaxUpdatedControl ControlID="LookForRoles" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
    </telerik:RadAjaxManager>

    <div>
        <div>
            <asp:Label runat="server" ID="debug"></asp:Label>

        <telerik:RadDropDownList runat="server" ID="LookForRoles" >
            <Items>
                <telerik:DropDownListItem Text="Select an action to create" Value=""/>
                <telerik:DropDownListItem Text="Create task" Value=""/>
                <telerik:DropDownListItem Text="Create client request" Value=""/>
                <telerik:DropDownListItem Text="Create outsource request" Value=""/>
                <telerik:DropDownListItem Text="Create financial request" Value=""/>
            </Items>
        </telerik:RadDropDownList>

            <telerik:radbutton runat="server" id="RadButton_AddAction" Text="Gurkfdfd" RenderMode="Lightweight" 
                OnClick="RadButton_AddAction_Click" AutoPostBack="false" OnClientClicked="testFunction" >
                <Icon PrimaryIconRight="5px" PrimaryIconCssClass="rbAdd" />
            </telerik:radbutton>
        </div>
        <telerik:RadGrid runat="server" ID="Radgrid_Events" RenderMode="Lightweight" 
            AllowMultiRowSelection="true" 
            OnNeedDataSource="Radgrid_Events_NeedDataSource" >
            <PagerStyle Mode="NextPrevNumericAndAdvanced" PageSizeControlType="RadComboBox" AlwaysVisible="True"></PagerStyle>

        <ExportSettings ExportOnlyData="true" IgnorePaging="true">
                <Pdf>
                    <PageFooter>
                        <MiddleCell Text="<?page-number?>" />
                    </PageFooter>
                </Pdf>
            </ExportSettings>
        <ClientSettings Selecting-EnableDragToSelectRows="false">
            <ClientEvents OnRowDblClick="OpenEditForm" />
        </ClientSettings>
        <MasterTableView  AutoGenerateColumns="false" AllowMultiColumnSorting="true" >
            <Columns>
                <telerik:GridBoundColumn DataField="EventId" ReadOnly="true" Display="true" HeaderText="Event Id"
                        UniqueName="EventId">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="EventName" ReadOnly="true" Display="true" HeaderText="Event Name"
                        UniqueName="EventName">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="EventDescr" ReadOnly="true" Display="true" HeaderText="Event Description"
                        UniqueName="EventDescr">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="EventFrom" ReadOnly="true" Display="true" HeaderText="Event Description"
                        UniqueName="EventFrom">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="EventTo" ReadOnly="true" Display="true" HeaderText="Event Description"
                        UniqueName="EventTo">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="EventComment" ReadOnly="true" Display="true" HeaderText="Event Description"
                        UniqueName="EventComment">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="EventBudget" ReadOnly="true" Display="true" HeaderText="Event Description"
                        UniqueName="EventBudget">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="EventGuests" ReadOnly="true" Display="true" HeaderText="Event Description"
                        UniqueName="EventGuests">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="EventType" ReadOnly="true" Display="true" HeaderText="Event Description"
                        UniqueName="EventType">
                </telerik:GridBoundColumn>

            </Columns>
        </MasterTableView>
            
        </telerik:RadGrid>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Tasks</h2>
            <p> 
                See tasks
               </p>
            <p>
                <a class="btn btn-default" href="http://go.microsoft.com/fwlink/?LinkId=301948">Learn more &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Requests</h2>
            <p>
                See requests
            </p>
            <p>
                <a class="btn btn-default" href="http://go.microsoft.com/fwlink/?LinkId=301949">Learn more &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Someting else</h2>
            <p>
                gurk æpeðþóþvílíkt maus
            </p>
            <p>
                <a class="btn btn-default" href="http://go.microsoft.com/fwlink/?LinkId=301950">Learn more &raquo;</a>
            </p>
        </div>
    </div>

    <asp:SqlDataSource runat="server" ID="Main" ConnectionString="<%$ ConnectionStrings:SqlDataBase %>" SelectCommand="select * from [table]"></asp:SqlDataSource>

</asp:Content>
