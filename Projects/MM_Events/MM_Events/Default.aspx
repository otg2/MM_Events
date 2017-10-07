<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MM_Events._Default" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register assembly="Telerik.Web.UI.Skins" namespace="Telerik.Web.UI.Skins" tagprefix="telerikh" %>



<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <telerik:RadCodeBlock runat="server" ID="RadCodaBlock1">
        <script type="text/javascript">

            function OpenEventFromTask(sender, args)
            {
                //todo - set the selected event in radgrid
                alert("open Event");
            }

            function OpenEditForm(sender, args)
            {
                $find('<%= EventsAfstemmingar_AjaxManager.ClientID %>').ajaxRequest("init_Event");
                openModalWindow($find('<%= Window_EventForm.ClientID %>'), 0.5, 0.5);
            } 

            function openNewEventRequestForm(sender, args)
            {
                $find('<%= EventsAfstemmingar_AjaxManager.ClientID %>').ajaxRequest("init_NewRequest");
                openModalWindow($find('<%= Window_NewEventRequestForm.ClientID %>'), 0.5, 0.5);
            }

            function openTaskForm(sender, args) {
                //alert("open Task Form");
                //return;
                $find('<%= EventsAfstemmingar_AjaxManager.ClientID %>').ajaxRequest("init_Task");
                openModalWindow($find('<%= Window_ViewTask.ClientID %>'), 0.5, 0.5);
            }
            
            function openRequestForm(sender, args) {
                var _requestId = sender.get_value();
                //alert(_requestId);
                //return;
                $find('<%= EventsAfstemmingar_AjaxManager.ClientID %>').ajaxRequest("init_Request;" + _requestId);
                openModalWindow($find('<%= Window_RequestEvent.ClientID %>'), 0.5, 0.5);
            }
            

            function openModalWindow(aReference, aWidth, aHeight) {
                var _workboardWidth = $(window).width() * aWidth;
                var _workboardHeight = $(window).height() * aHeight;

                var oWnd = aReference;
                oWnd.setSize(_workboardWidth, _workboardHeight);
                oWnd.set_modal(true);
                oWnd.set_centerIfModal(true);
                oWnd.set_animation(Telerik.Web.UI.WindowAnimation.Slide);
                oWnd.set_animationDuration(500);
                oWnd.show();
                setWindowBehavior(oWnd);
                setTimeout(function () {
                    overlayCloseHandler(oWnd);}, 510);
            }

            // Adds a function so user can click out of focus box to close it. Not generally supported by Telerik.
            function overlayCloseHandler(sender) {
                var overlay = $telerik.getElementByClassName(document, "TelerikModalOverlay");
                overlay.onclick = function () {
                    sender.close();
                    var scrollAmount = window.pageYOffset;
                    setTimeout(function () { window.scrollTo(0, scrollAmount); }, 10);
                }
            }

            function setWindowBehavior(aWindow) {
                aWindow.set_behaviors(Telerik.Web.UI.WindowBehaviors.Move + Telerik.Web.UI.WindowBehaviors.Close
                    + Telerik.Web.UI.WindowBehaviors.Resize + Telerik.Web.UI.WindowBehaviors.Maximize);
            }

        </script>
    </telerik:RadCodeBlock>

    <telerik:RadAjaxManager runat="server" ID="EventsAfstemmingar_AjaxManager" OnAjaxRequest="Events_AjaxManager_AjaxRequest">
        <AjaxSettings>
             <telerik:AjaxSetting AjaxControlID="EventsAfstemmingar_AjaxManager">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="EventForm_Name" />
                    <telerik:AjaxUpdatedControl ControlID="EventForm_Description" />
                    <telerik:AjaxUpdatedControl ControlID="EventForm_From" />
                    <telerik:AjaxUpdatedControl ControlID="EventForm_To" />
                    <telerik:AjaxUpdatedControl ControlID="EventForm_Comment" />
                    <telerik:AjaxUpdatedControl ControlID="EventForm_Budget" />
                    <telerik:AjaxUpdatedControl ControlID="EventForm_Guests" />
                    <telerik:AjaxUpdatedControl ControlID="EventForm_Type" />

                    <telerik:AjaxUpdatedControl ControlID="RequestEvent_Name" />
                    <telerik:AjaxUpdatedControl ControlID="RequestEvent_Descr" />
                    <telerik:AjaxUpdatedControl ControlID="RequestEvent_From" />
                    <telerik:AjaxUpdatedControl ControlID="RequestEvent_To" />
                    <telerik:AjaxUpdatedControl ControlID="RequestEvent_Comment" />
                    <telerik:AjaxUpdatedControl ControlID="RequestEvent_Budget" />
                    <telerik:AjaxUpdatedControl ControlID="RequestEvent_Guests" />
                    <telerik:AjaxUpdatedControl ControlID="RequestEvent_Type" />
                    <telerik:AjaxUpdatedControl ControlID="RequestEvent_EventId" />
                    <telerik:AjaxUpdatedControl ControlID="RequestEvent_FM_Budget" />
                    <telerik:AjaxUpdatedControl ControlID="RequestEvent_FM_Comment" />
                    <telerik:AjaxUpdatedControl ControlID="RequestEvent_Reject" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="Radgrid_Events">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Radgrid_Events" LoadingPanelID="LoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadButton_AddAction">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="debug" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="Task_AddTaskToEvent">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Task_Name" />
                    <telerik:AjaxUpdatedControl ControlID="Task_Descr" />
                    <telerik:AjaxUpdatedControl ControlID="Task_Subteams" />
                    <telerik:AjaxUpdatedControl ControlID="Task_Budget" />
                    <telerik:AjaxUpdatedControl ControlID="Task_DueDate" />
                    <telerik:AjaxUpdatedControl ControlID="Task_Feedback" />
                </UpdatedControls>
            </telerik:AjaxSetting>
             <telerik:AjaxSetting AjaxControlID="NewEventRequestForm_SendNewRequest">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="NewEventRequestForm_Name" />
                    <telerik:AjaxUpdatedControl ControlID="NewEventRequestForm_Email" />
                    <telerik:AjaxUpdatedControl ControlID="NewEventRequestForm_Number" />
                    <telerik:AjaxUpdatedControl ControlID="NewEventRequestForm_Budget" />
                    <telerik:AjaxUpdatedControl ControlID="NewEventRequestForm_Type" />
                    <telerik:AjaxUpdatedControl ControlID="NewEventRequestForm_DateFrom" />
                    <telerik:AjaxUpdatedControl ControlID="NewEventRequestForm_DateTo" />
                    <telerik:AjaxUpdatedControl ControlID="NewEventRequestForm_Feedback" />
                    <telerik:AjaxUpdatedControl ControlID="NewEventRequestForm_Descr" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel runat="server" ID="LoadingPanel1" Skin="Metro"></telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager runat="server" ID="WindowManager">
         <Shortcuts>
            <telerik:WindowShortcut CommandName="CloseAll" Shortcut="Esc" />
        </Shortcuts>
        <Windows>
            <telerik:RadWindow runat="server" ID="Window_ViewTask" Title="Event View">
                <ContentTemplate>
                    <div>
                        <h2>Task</h2>
                        <div>
                            <div>
                                <telerik:RadTextBox runat="server" ID="ViewTask_Name"></telerik:RadTextBox>
                            </div>
                            <div>
                                <telerik:RadTextBox runat="server" ID="ViewTask_Descr"></telerik:RadTextBox>
                            </div>
                        </div>
                        <div>
                            <div>
                                <telerik:RadTextBox runat="server" ID="ViewTask_CreatedInfo"></telerik:RadTextBox>
                            </div>
                            <div>
                                <telerik:RadButton runat="server" ID="ViewTask_OpenEventInformation" 
                                    Text="View event" AutoPostBack="false" OnClientClicked="OpenEventFromTask"></telerik:RadButton>
                            </div>
                        </div>
                        <div>
                            <div>
                                <telerik:RadTextBox runat="server" ID="ViewTask_Budget"></telerik:RadTextBox>
                            </div>
                            <div>
                                <telerik:RadNumericTextBox runat="server" ID="ViewTask_ExtraBudget" MinValue="0"></telerik:RadNumericTextBox>
                            </div>
                        </div>
                        <div>
                            <telerik:RadTextBox runat="server" ID="ViewTask_ExtraComment" TextMode="MultiLine" Columns="80" Rows="5" Width="100%"
                                EmptyMessage="Comment" RenderMode="Lightweight"  AutoPostBack="false" ></telerik:RadTextBox>
                        </div>
                        <div>
                            <telerik:RadButton runat="server" ID="ViewTask_Accept"></telerik:RadButton>
                        </div>
                    </div>
                </ContentTemplate>
            </telerik:RadWindow>
            <telerik:RadWindow runat="server" ID="Window_EventForm" Title="Event View">
                <ContentTemplate>
                    <div>
                        <div id="EventForm_Header">
                            <telerik:RadTextBox runat="server" ID="EventForm_Name" RenderMode="Lightweight"></telerik:RadTextBox>
                            <telerik:RadTextBox runat="server" ID="EventForm_Description" RenderMode="Lightweight"></telerik:RadTextBox>
                            <telerik:RadTextBox runat="server" ID="EventForm_From" RenderMode="Lightweight"></telerik:RadTextBox>
                            <telerik:RadTextBox runat="server" ID="EventForm_To" RenderMode="Lightweight"></telerik:RadTextBox>

                        </div>
                        <div id="EventForm_Info">
                            <telerik:RadTextBox runat="server" ID="EventForm_Comment" RenderMode="Lightweight"></telerik:RadTextBox>
                            <telerik:RadTextBox runat="server" ID="EventForm_Budget" RenderMode="Lightweight"></telerik:RadTextBox>
                            <telerik:RadTextBox runat="server" ID="EventForm_Guests" RenderMode="Lightweight"></telerik:RadTextBox>
                            <telerik:RadTextBox runat="server" ID="EventForm_Type" RenderMode="Lightweight"></telerik:RadTextBox>
                        </div>
                        <div id="EventForm_Task" runat="server">
                            <telerik:RadTextBox runat="server" ID="Task_Name" EmptyMessage="Name..." RenderMode="Lightweight"  AutoPostBack="false" ></telerik:RadTextBox>
                            <telerik:RadTextBox runat="server" ID="Task_Descr" EmptyMessage="Description..." RenderMode="Lightweight"  AutoPostBack="false" ></telerik:RadTextBox>
                            <telerik:RadComboBox runat="server" ID="Task_Subteams" Filter="Contains" DropDownAutoWidth="Enabled" MarkFirstMatch="true"
                                 EmptyMessage="Select subteam..."  RenderMode="Lightweight"  AutoPostBack="false" >
                                <Items>
                                    <telerik:RadComboBoxItem Text="Audio Technician" Value="AUDIO" />
                                    <telerik:RadComboBoxItem Text="Chef" Value="CHEF" />
                                    <telerik:RadComboBoxItem Text="Catering" Value="CATERING" />
                                    <telerik:RadComboBoxItem Text="Decoration" Value="DECORATION" />
                                    <telerik:RadComboBoxItem Text="Graphic Designer" Value="GRAPHIC_DESIGNER" />
                                    <telerik:RadComboBoxItem Text="IT department" Value="IT" />
                                    <telerik:RadComboBoxItem Text="Photographer" Value="PHOTOGRAPHER" />
                                </Items>
                            </telerik:RadComboBox>
                            <telerik:RadNumericTextBox runat="server" ID="Task_Budget" EmptyMessage="Budget..." RenderMode="Lightweight"  AutoPostBack="false" >
                            </telerik:RadNumericTextBox>
                            <telerik:RadDatePicker runat="server" ID="Task_DueDate" DateInput-EmptyMessage="DueDate..." RenderMode="Lightweight"  AutoPostBack="false" ></telerik:RadDatePicker>
                            <telerik:RadButton runat="server" ID="Task_AddTaskToEvent" Text="Create Task" AutoPostBack="true" RenderMode="Lightweight"
                                OnClick="Task_AddTaskToEvent_Click" ></telerik:RadButton>
                            <telerik:RadTextBox runat="server" ID="Task_Feedback" RenderMode="Lightweight" ></telerik:RadTextBox>
                        </div>
                        <div id="EventForm_Actions">
                            <p>test</p>
                        </div>
                    </div>
                </ContentTemplate>
               
            </telerik:RadWindow>
            <telerik:RadWindow runat="server" ID="Window_NewEventRequestForm" Title="New Event Request">
                <ContentTemplate>
                    <div>
                        <div id="Div_NewEventRequestForm" style="background-color:aliceblue; padding:10px">
                            <div style="margin:5%">
                                <div style="float:left; margin:5px 10px">
                                    <telerik:RadTextBox runat="server" ID="NewEventRequestForm_Name" EmptyMessage="Name..." RenderMode="Lightweight"  AutoPostBack="false" ></telerik:RadTextBox>
                                </div>
                                <div style="float:left; margin:5px 10px ">
                                    <telerik:RadTextBox runat="server" ID="NewEventRequestForm_Number" EmptyMessage="Number..." RenderMode="Lightweight"  AutoPostBack="false" ></telerik:RadTextBox>
                                </div>
                                <div style="float:left; margin:5px 10px">
                                    <telerik:RadTextBox runat="server" ID="NewEventRequestForm_Email" EmptyMessage="Email..." RenderMode="Lightweight"  AutoPostBack="false" ></telerik:RadTextBox>
                                </div>
                            </div>
                            <div style="margin:5%;clear:both">
                                <div style="float:left; margin:5px 10px">
                                    <telerik:RadTextBox runat="server" ID="NewEventRequestForm_Descr" TextMode="MultiLine" Columns="80" Rows="5" Width="100%"
                                         EmptyMessage="Description..." RenderMode="Lightweight"  AutoPostBack="false" ></telerik:RadTextBox>
                                </div>
                            </div>
                            <div style="margin:5%;clear:both">
                                <div style="float:left; margin:5px 10px">
                                    <telerik:RadDropDownList runat="server" ID="NewEventRequestForm_Type" RenderMode="Lightweight" >
                                        <Items>
                                            <telerik:DropDownListItem Text="Wedding" Value="WEDDING" />
                                            <telerik:DropDownListItem Text="Conference" Value="CONFERENCE" />
                                            <telerik:DropDownListItem Text="Birthday" Value="BIRTHDAY" />
                                            <telerik:DropDownListItem Text="Concert" Value="CONCERT" />
                                        </Items>
                                    </telerik:RadDropDownList>
                                </div>
                                <div style="float:left; margin:5px 10px">
                                    <telerik:RadNumericTextBox runat="server" ID="NewEventRequestForm_Budget" EmptyMessage="Budget..." RenderMode="Lightweight"  AutoPostBack="false" >
                                    </telerik:RadNumericTextBox>
                                </div>
                                <div style="float:left; margin:5px 10px">
                                    <telerik:RadNumericTextBox runat="server" ID="NewEventRequestForm_Guests" EmptyMessage="Guests..." RenderMode="Lightweight"  AutoPostBack="false" >
                                    </telerik:RadNumericTextBox>
                                </div>
                            </div>
                            <div style="margin:5%;clear:both">
                                <div style="float:left; margin:5px 10px">
                                    <telerik:RadDatePicker runat="server" ID="NewEventRequestForm_DateFrom" Width="180px" DateInput-EmptyMessage="Starting Date" RenderMode="Lightweight"  AutoPostBack="false" >
                                    </telerik:RadDatePicker>
                                </div>
                                <div style="float:left; margin:5px 10px">
                                    <telerik:RadDatePicker runat="server" ID="NewEventRequestForm_DateTo" Width="180px" DateInput-EmptyMessage="End Date" RenderMode="Lightweight"  AutoPostBack="false" >
                                    </telerik:RadDatePicker>
                                </div>
                           </div>
                            <div style="clear:both;float:right">
                            <telerik:RadButton runat="server" ID="NewEventRequestForm_SendNewRequest" Text="Create Task" AutoPostBack="true" RenderMode="Lightweight"
                                OnClick="NewEventRequestForm_SendNewRequest_Click" >
                                <Icon SecondaryIconRight="5px" SecondaryIconCssClass="rbOk" />
                            </telerik:RadButton>
                            <telerik:RadTextBox runat="server" ID="NewEventRequestForm_Feedback" RenderMode="Lightweight" ></telerik:RadTextBox>
                            </div>
                            <div style="clear:both"></div>
                        </div>
                    </div>
                </ContentTemplate>
               
            </telerik:RadWindow>
            <telerik:RadWindow runat="server" ID="Window_RequestEvent" Title="Event Request">
                <ContentTemplate>
                    <div style="background-color:aliceblue; padding:10px">
                        <div id="RequestEvent_Event" >
                            <div style="margin:5%">
                                <telerik:RadTextBox runat="server" ID="RequestEvent_EventId" RenderMode="Lightweight" Display="false" ></telerik:RadTextBox>
                                <div style="float:left; margin:5px 10px">
                                    <telerik:RadTextBox runat="server" ID="RequestEvent_Name" RenderMode="Lightweight"></telerik:RadTextBox>
                                </div>
                                <div style="float:left; margin:5px 10px">
                                    <telerik:RadTextBox runat="server" ID="RequestEvent_From" RenderMode="Lightweight"></telerik:RadTextBox>
                                </div>
                                <div style="float:left; margin:5px 10px">
                                    <telerik:RadTextBox runat="server" ID="RequestEvent_To" RenderMode="Lightweight"></telerik:RadTextBox>
                                </div>
                                <div style="float:left; margin:5px 10px">
                                    <telerik:RadTextBox runat="server" ID="RequestEvent_Budget" RenderMode="Lightweight"></telerik:RadTextBox>
                                </div>
                                 <div style="float:left; margin:5px 10px">
                                    <telerik:RadTextBox runat="server" ID="RequestEvent_Guests" RenderMode="Lightweight"></telerik:RadTextBox>
                                </div>
                                <div style="float:left; margin:5px 10px">
                                    <telerik:RadTextBox runat="server" ID="RequestEvent_Type" RenderMode="Lightweight"></telerik:RadTextBox>
                                </div>
                                <div style="float:left; margin:5px 10px">
                                    <telerik:RadTextBox runat="server" ID="RequestEvent_Comment" RenderMode="Lightweight"></telerik:RadTextBox>
                                </div>
                            </div>
                            <div style="margin:5%;clear:both">
                                <div style="float:left; margin:5px 10px">
                                    <telerik:RadTextBox runat="server" ID="RequestEvent_Descr" RenderMode="Lightweight" TextMode="MultiLine" Columns="80" Rows="5" Width="100%"></telerik:RadTextBox>
                                </div>
                            </div>
                            <div style="margin:5%;clear:both">
                                <div style="float:left; margin:5px 10px">
                                    <telerik:RadNumericTextBox runat="server" EmptyMessage="Financial Budget" 
                                        ID="RequestEvent_FM_Budget" RenderMode="Lightweight"></telerik:RadNumericTextBox>
                                </div>
                                <div style="float:left; margin:5px 10px">
                                    <telerik:RadTextBox runat="server" EmptyMessage="Financial Comment"  ID="RequestEvent_FM_Comment" RenderMode="Lightweight" TextMode="MultiLine" Columns="80" Rows="5" Width="100%"></telerik:RadTextBox>
                                </div>
                            </div>
                            
                        </div>
                        <div id="RequestEvent_Buttons" style="margin-top:10px;clear:both">
                            <div style="float:left">
                                <telerik:RadButton runat="server" id="RequestEvent_Reject" Text="Reject Request" RenderMode="Lightweight"
                                    OnClick="RequestEvent_Reject_Click">
                                    <Icon SecondaryIconCssClass="rbCancel" />
                                </telerik:RadButton>
                            </div>
                            <div style="float:right">
                                <telerik:RadButton runat="server" id="RequestEvent_Forward" Text="Accept and Forward" RenderMode="Lightweight"
                                     OnClick="RequestEvent_Forward_Click">
                                    <Icon SecondaryIconCssClass="rbNext" />
                                </telerik:RadButton>
                            </div>
                            <div style="clear:both"></div>
                        </div>
                    </div>
                </ContentTemplate>
               
            </telerik:RadWindow>

        </Windows>
    </telerik:RadWindowManager>

    <div runat="server" id="AuthDiv">
        <div>
            <div>
                <asp:Label runat="server" ID="debug"></asp:Label>
            </div>
            <telerik:RadGrid runat="server" ID="Radgrid_Events" RenderMode="Lightweight" Skin="Office2007"
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
                    <telerik:GridBoundColumn DataField="EventFrom" ReadOnly="true" Display="true" HeaderText="Event From"
                            UniqueName="EventFrom">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="EventTo" ReadOnly="true" Display="true" HeaderText="Event To"
                            UniqueName="EventTo">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="EventComment" ReadOnly="true" Display="true" HeaderText="Event Comment"
                            UniqueName="EventComment">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="EventBudget" ReadOnly="true" Display="true" HeaderText="Event Budget"
                            UniqueName="EventBudget">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="EventGuests" ReadOnly="true" Display="true" HeaderText="Event Guests"
                            UniqueName="EventGuests">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="EventType" ReadOnly="true" Display="true" HeaderText="Event Type"
                            UniqueName="EventType">
                    </telerik:GridBoundColumn>

                </Columns>
            </MasterTableView>
            
            </telerik:RadGrid>
        </div>

        <div class="row">
            <div class="col-md-4">
                <h2>Tasks</h2>
                <div runat="server" id="Div_GeneratedTasks">

                </div>
            </div>
            <div class="col-md-4">
                <h2>Requests</h2>
                <div runat="server" id="Div_GeneratedRequests">

                </div>
            </div>
            <div class="col-md-4" runat="server" id="Div_CreateNewEventRequest">
                <h2>A client called?</h2>
                    <telerik:radbutton runat="server" id="RadButton_AddAction" Text="New Event Request" RenderMode="Lightweight" 
                     AutoPostBack="false" OnClientClicked="openNewEventRequestForm" >
                    <Icon PrimaryIconRight="5px" PrimaryIconCssClass="rbAdd" />
                </telerik:radbutton>
            </div>
        </div>

        <asp:SqlDataSource runat="server" ID="Main" ConnectionString="<%$ ConnectionStrings:SqlDataBase %>" SelectCommand="select * from [table]"></asp:SqlDataSource>
    </div>
    <div runat="server" id="NoAuthDiv">
        <p>Nothing to see - please login</p>
    </div>
</asp:Content>
