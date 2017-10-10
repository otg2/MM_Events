﻿<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MM_Events._Default" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register assembly="Telerik.Web.UI.Skins" namespace="Telerik.Web.UI.Skins" tagprefix="telerikh" %>



<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <telerik:RadCodeBlock runat="server" ID="RadCodaBlock1">
        <script type="text/javascript">

            function OpenEventFromTask(sender, args)
            {
                $find('<%= EventsAfstemmingar_AjaxManager.ClientID %>').ajaxRequest("init_Event");
                openModalWindow($find('<%= Window_EventForm.ClientID %>'), 0.5, 0.5);
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
                var _taskId = sender.get_value();
                console.log(_taskId);
                $find('<%= EventsAfstemmingar_AjaxManager.ClientID %>').ajaxRequest("init_Task;" + _taskId);
                openModalWindow($find('<%= Window_ViewTask.ClientID %>'), 0.5, 0.5);
            }
            
            function openRequestForm(sender, args) {
                var _requestId = sender.get_value();
                $find('<%= EventsAfstemmingar_AjaxManager.ClientID %>').ajaxRequest("init_Request;" + _requestId);
                openModalWindow($find('<%= Window_RequestEvent.ClientID %>'), 0.5, 0.5);
            }
            
            function openRequestForm_Finance(sender, args) {
                var _requestId = sender.get_value();
                $find('<%= EventsAfstemmingar_AjaxManager.ClientID %>').ajaxRequest("init_RequestFinance;" + _requestId);
                openModalWindow($find('<%= Window_FinanceRequest.ClientID %>'), 0.5, 0.5);
            }
            function openRequestForm_Outsource(sender, args) {
                var _requestId = sender.get_value();
                $find('<%= EventsAfstemmingar_AjaxManager.ClientID %>').ajaxRequest("init_RequestOutsource;" + _requestId);
                openModalWindow($find('<%= Window_OutsourceRequest.ClientID %>'), 0.5, 0.5);
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
                    <telerik:AjaxUpdatedControl ControlID="EventForm_Name" LoadingPanelID="LoadingPanel1"/>
                    <telerik:AjaxUpdatedControl ControlID="EventForm_Description" LoadingPanelID="LoadingPanel1"/>
                    <telerik:AjaxUpdatedControl ControlID="EventForm_From" LoadingPanelID="LoadingPanel1"/>
                    <telerik:AjaxUpdatedControl ControlID="EventForm_To" LoadingPanelID="LoadingPanel1"/>
                    <telerik:AjaxUpdatedControl ControlID="EventForm_Comment" LoadingPanelID="LoadingPanel1"/>
                    <telerik:AjaxUpdatedControl ControlID="EventForm_Budget" LoadingPanelID="LoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="EventForm_Guests" LoadingPanelID="LoadingPanel1"/>
                    <telerik:AjaxUpdatedControl ControlID="EventForm_Type" LoadingPanelID="LoadingPanel1"/>

                    <telerik:AjaxUpdatedControl ControlID="RequestEvent_Name" LoadingPanelID="LoadingPanel1"/>
                    <telerik:AjaxUpdatedControl ControlID="RequestEvent_Descr" LoadingPanelID="LoadingPanel1"/>
                    <telerik:AjaxUpdatedControl ControlID="RequestEvent_From" LoadingPanelID="LoadingPanel1"/>
                    <telerik:AjaxUpdatedControl ControlID="RequestEvent_To" LoadingPanelID="LoadingPanel1"/>
                    <telerik:AjaxUpdatedControl ControlID="RequestEvent_Comment" LoadingPanelID="LoadingPanel1"/>
                    <telerik:AjaxUpdatedControl ControlID="RequestEvent_Budget" LoadingPanelID="LoadingPanel1"/>
                    <telerik:AjaxUpdatedControl ControlID="RequestEvent_Guests" LoadingPanelID="LoadingPanel1"/>
                    <telerik:AjaxUpdatedControl ControlID="RequestEvent_Type" LoadingPanelID="LoadingPanel1"/>
                    <telerik:AjaxUpdatedControl ControlID="RequestEvent_EventId" LoadingPanelID="LoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="RequestEvent_FM_Budget" LoadingPanelID="LoadingPanel1"/>
                    <telerik:AjaxUpdatedControl ControlID="RequestEvent_FM_Comment" LoadingPanelID="LoadingPanel1"/>
                    <telerik:AjaxUpdatedControl ControlID="RequestEvent_Reject" LoadingPanelID="LoadingPanel1"/>
                    <telerik:AjaxUpdatedControl ControlID="RequestEvent_Forward" LoadingPanelID="LoadingPanel1"/>
                    

                     <telerik:AjaxUpdatedControl ControlID="ViewTask_Name" LoadingPanelID="LoadingPanel1"/>
                    <telerik:AjaxUpdatedControl ControlID="ViewTask_Descr" LoadingPanelID="LoadingPanel1"/>
                    <telerik:AjaxUpdatedControl ControlID="ViewTask_CreatedInfo" LoadingPanelID="LoadingPanel1"/>
                    <telerik:AjaxUpdatedControl ControlID="ViewTask_OpenEventInformation" LoadingPanelID="LoadingPanel1"/>
                    <telerik:AjaxUpdatedControl ControlID="ViewTask_DueDate" LoadingPanelID="LoadingPanel1"/>
                    <telerik:AjaxUpdatedControl ControlID="ViewTask_Budget" LoadingPanelID="LoadingPanel1"/>
                    <telerik:AjaxUpdatedControl ControlID="ViewTask_ExtraBudget" LoadingPanelID="LoadingPanel1"/>
                    <telerik:AjaxUpdatedControl ControlID="ViewTask_ExtraComment" LoadingPanelID="LoadingPanel1"/>
                    <telerik:AjaxUpdatedControl ControlID="ViewTask_Accept" LoadingPanelID="LoadingPanel1"/>

                    <telerik:AjaxUpdatedControl ControlID="RequestEvent_Meeting" LoadingPanelID="LoadingPanel1" />

                    <telerik:AjaxUpdatedControl ControlID="Radgrid_Events" LoadingPanelID="LoadingPanel1" />
                    
                    <telerik:AjaxUpdatedControl ControlID="FinanceRequest_Event" LoadingPanelID="LoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="FinanceRequest_ViewEvent" LoadingPanelID="LoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="FinanceRequest_Name" LoadingPanelID="LoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="FinanceRequest_Department" LoadingPanelID="LoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="FinanceRequest_Descr" LoadingPanelID="LoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="FinanceRequest_Original" LoadingPanelID="LoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="FinanceRequest_Extra" LoadingPanelID="LoadingPanel1" />

                    <telerik:AjaxUpdatedControl ControlID="OutsourceRequest_Name" LoadingPanelID="LoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="OutsourceRequest_Subteam" LoadingPanelID="LoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="OutsourceRequest_Descr" LoadingPanelID="LoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="OutsourceRequest_Regarding" LoadingPanelID="LoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="OutsourceRequest_ViewEvent" LoadingPanelID="LoadingPanel1" />
                    

                </UpdatedControls>
            </telerik:AjaxSetting>
            
            <telerik:AjaxSetting AjaxControlID="ViewTask_Accept">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Div_GeneratedTasks" LoadingPanelID="LoadingPanel1" />
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
                    <telerik:AjaxUpdatedControl ControlID="Task_Name" LoadingPanelID="LoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="Task_Descr" LoadingPanelID="LoadingPanel1"/>
                    <telerik:AjaxUpdatedControl ControlID="Task_Subteams" LoadingPanelID="LoadingPanel1"/>
                    <telerik:AjaxUpdatedControl ControlID="Task_Budget" LoadingPanelID="LoadingPanel1"/>
                    <telerik:AjaxUpdatedControl ControlID="Task_DueDate" LoadingPanelID="LoadingPanel1"/>
                    <telerik:AjaxUpdatedControl ControlID="Task_Feedback" LoadingPanelID="LoadingPanel1"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
             <telerik:AjaxSetting AjaxControlID="NewEventRequestForm_SendNewRequest">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="NewEventRequestForm_Name" LoadingPanelID="LoadingPanel1"/>
                    <telerik:AjaxUpdatedControl ControlID="NewEventRequestForm_Email" LoadingPanelID="LoadingPanel1"/>
                    <telerik:AjaxUpdatedControl ControlID="NewEventRequestForm_Number" LoadingPanelID="LoadingPanel1"/>
                    <telerik:AjaxUpdatedControl ControlID="NewEventRequestForm_Budget" LoadingPanelID="LoadingPanel1"/>
                    <telerik:AjaxUpdatedControl ControlID="NewEventRequestForm_Type" LoadingPanelID="LoadingPanel1"/>
                    <telerik:AjaxUpdatedControl ControlID="NewEventRequestForm_DateFrom" LoadingPanelID="LoadingPanel1"/>
                    <telerik:AjaxUpdatedControl ControlID="NewEventRequestForm_DateTo" LoadingPanelID="LoadingPanel1"/>
                    <telerik:AjaxUpdatedControl ControlID="NewEventRequestForm_Feedback" LoadingPanelID="LoadingPanel1"/>
                    <telerik:AjaxUpdatedControl ControlID="NewEventRequestForm_Descr" LoadingPanelID="LoadingPanel1"/>

                    
                   

                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RequestEvent_BookMeeting">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Radgrid_Events" LoadingPanelID="LoadingPanel1"/>
                    <telerik:AjaxUpdatedControl ControlID="Div_GeneratedRequests" LoadingPanelID="LoadingPanel1"/>
                    
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
            <telerik:RadWindow runat="server" ID="Window_FinanceRequest" Title="Event View">
                <ContentTemplate>
                    <div style="background-color:aliceblue; padding:10px">
                        <h1>Financial request</h1>
                        <div style="margin:5%; clear:both">
                            <div style="float:left; margin:5px 10px">
                                <div style="float:left">
                                    <telerik:RadLabel runat="server" ID="FinanceRequest_Event_Label" Text="Regarding" Font-Bold="true"></telerik:RadLabel>
                                    <telerik:RadTextBox runat="server" ID="FinanceRequest_Event" RenderMode="Lightweight"></telerik:RadTextBox>
                                </div>
                                <div style="float:left; margin:5px 10px">
                                    <telerik:RadButton runat="server" ID="FinanceRequest_ViewEvent" RenderMode="Lightweight" Skin="Simple"
                                        Text="View event" AutoPostBack="false" OnClientClicked="OpenEventFromTask">
                                        <Icon SecondaryIconCssClass="rbOpen" SecondaryIconRight="5px" />
                                    </telerik:RadButton>
                                </div>
                            </div>
                        </div>
                        <div style="margin:5%; clear:both">
                            <div style="float:left; margin:5px 10px">
                                <telerik:RadLabel runat="server" ID="FinanceRequest_Name_Label" Text="Name" Font-Bold="true"></telerik:RadLabel>
                                <telerik:RadTextBox runat="server" ID="FinanceRequest_Name" RenderMode="Lightweight"></telerik:RadTextBox>
                            </div>
                            <div style="float:left; margin:5px 10px">
                                <telerik:RadLabel runat="server" ID="FinanceRequest_Department_Label" Text="Department" Font-Bold="true"></telerik:RadLabel>
                                <telerik:RadTextBox runat="server" ID="FinanceRequest_Department" RenderMode="Lightweight"></telerik:RadTextBox>
                            </div>
                            <div style="float:left; margin:5px 10px">
                                <telerik:RadLabel runat="server" ID="FinanceRequest_Descr_Label" Text="Description" Font-Bold="true"></telerik:RadLabel>
                                <telerik:RadTextBox runat="server" ID="FinanceRequest_Descr" RenderMode="Lightweight"></telerik:RadTextBox>
                            </div>
                        </div>
                        
                        <div style="margin:5%; clear:both">
                            <div style="float:left; margin:5px 10px">
                                <telerik:RadLabel runat="server" ID="FinanceRequest_Original_Label" Text="Planned budget" Font-Bold="true"></telerik:RadLabel>
                                <telerik:RadNumericTextBox runat="server" ID="FinanceRequest_Original" MinValue="0" RenderMode="Lightweight" ReadOnly="true"></telerik:RadNumericTextBox>
                            </div>
                            <div style="float:left; margin:5px 10px">
                                <telerik:RadLabel runat="server" ID="FinanceRequest_Extra_Label" Text="Needed budget" Font-Bold="true"></telerik:RadLabel>
                                <telerik:RadNumericTextBox runat="server" ID="FinanceRequest_Extra" MinValue="0" RenderMode="Lightweight" ReadOnly="true"></telerik:RadNumericTextBox>
                            </div>
                        </div>
                        <div style="margin:5%; clear:both">
                            <telerik:RadButton runat="server" ID="FinanceRequest_Reject" RenderMode="Lightweight" Text="Reject" Skin="Sunset" 
                                OnClick="FinanceRequest_Click" Value="false">
                                <Icon SecondaryIconCssClass="rbCancel" SecondaryIconRight="5px" />
                            </telerik:RadButton>
                            <telerik:RadButton runat="server" ID="FinanceRequest_Accept" RenderMode="Lightweight" Text="Accept" Skin="Telerik" 
                                OnClick="FinanceRequest_Click" Value="true">
                                <Icon SecondaryIconCssClass="rbOk" SecondaryIconRight="5px" />
                            </telerik:RadButton>
                        </div>
                        <div style="clear:both"></div>
                    </div>
                </ContentTemplate>
            </telerik:RadWindow>
            <telerik:RadWindow runat="server" ID="Window_OutsourceRequest" Title="Event View">
                <ContentTemplate>
                    <div style="background-color:aliceblue; padding:10px">
                        <h1>Outsource request</h1>
                        <div style="margin:5%; clear:both">
                            <div style="float:left; margin:5px 10px">
                                <div style="float:left">
                                    <telerik:RadLabel runat="server" ID="RadLabel4" Text="Regarding" Font-Bold="true"></telerik:RadLabel>
                                    <telerik:RadTextBox runat="server" ID="OutsourceRequest_Regarding" RenderMode="Lightweight"></telerik:RadTextBox>
                                </div>
                                <div style="float:left; margin:5px 10px">
                                    <telerik:RadButton runat="server" ID="OutsourceRequest_ViewEvent" RenderMode="Lightweight" Skin="Simple"
                                        Text="View event" AutoPostBack="false" OnClientClicked="OpenEventFromTask">
                                        <Icon SecondaryIconCssClass="rbOpen" SecondaryIconRight="5px" />
                                    </telerik:RadButton>
                                </div>
                            </div>
                        </div>
                        <div style="margin:5%; clear:both">
                            <div style="float:left; margin:5px 10px">
                                <telerik:RadLabel runat="server" ID="RadLabel1" Text="Title" Font-Bold="true"></telerik:RadLabel>
                                <telerik:RadTextBox runat="server" ID="OutsourceRequest_Name" RenderMode="Lightweight"></telerik:RadTextBox>
                            </div>
                        </div>
                        <div style="margin:5%; clear:both">
                            <div style="float:left; margin:5px 10px">
                                <telerik:RadLabel runat="server" ID="RadLabel2" Text="Outsource for" Font-Bold="true"></telerik:RadLabel>
                                <telerik:RadComboBox runat="server" ID="OutsourceRequest_Subteam" Filter="Contains" DropDownAutoWidth="Enabled" MarkFirstMatch="true"
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
                            </div>
                        </div>
                        <div style="margin:5%; clear:both">

                            <div style="float:left; margin:5px 10px">
                                <telerik:RadLabel runat="server" ID="RadLabel3" Text="Description" Font-Bold="true"></telerik:RadLabel>
                                 <telerik:RadTextBox runat="server" ID="OutsourceRequest_Descr" TextMode="MultiLine" Columns="80" Rows="5" Width="100%"
                                    EmptyMessage="Comment" RenderMode="Lightweight"  AutoPostBack="false" ></telerik:RadTextBox>
                            </div>
                        </div>
                        
                        <div style="margin:5%; clear:both">
                            <telerik:RadButton runat="server" ID="OutsourceRequest_Reject" RenderMode="Lightweight" Text="Reject request" Skin="Sunset"
                                OnClick="OutsourceRequest_Send_Click">
                                <Icon SecondaryIconCssClass="rbCancel" SecondaryIconRight="5px" />
                            </telerik:RadButton>
                            <telerik:RadButton runat="server" ID="OutsourceRequest_Accept" RenderMode="Lightweight" Text="Approve outsource request" Skin="Telerik"
                                OnClick="OutsourceRequest_Send_Click">
                                <Icon SecondaryIconCssClass="rbOk" SecondaryIconRight="5px" />
                            </telerik:RadButton>
                        </div>
                        <div style="clear:both"></div>
                    </div>
                </ContentTemplate>
            </telerik:RadWindow>
            <telerik:RadWindow runat="server" ID="Window_ViewTask" Title="Event View">
                <ContentTemplate>
                    <div style="background-color:aliceblue; padding:10px">
                        <h1>Task</h1>
                        <div style="margin:5%; clear:both">
                            <div style="float:left; margin:5px 10px">
                                <telerik:RadLabel runat="server" ID="ViewTask_DueDate_Label" Text="Duedate" Font-Bold="true"></telerik:RadLabel>
                                <telerik:RadTextBox runat="server" ID="ViewTask_DueDate" RenderMode="Lightweight"></telerik:RadTextBox>
                            </div>
                        </div>
                        <div style="margin:5%; clear:both">
                            <div style="float:left; margin:5px 10px">
                                <telerik:RadLabel runat="server" ID="ViewTask_Name_Label" Text="Name" Font-Bold="true"></telerik:RadLabel>
                                <telerik:RadTextBox runat="server" ID="ViewTask_Name" RenderMode="Lightweight"></telerik:RadTextBox>
                            </div>
                            <div style="float:left; margin:5px 10px">
                                <telerik:RadLabel runat="server" ID="ViewTask_Descr_Label" Text="Description" Font-Bold="true"></telerik:RadLabel>
                                <telerik:RadTextBox runat="server" ID="ViewTask_Descr" RenderMode="Lightweight"></telerik:RadTextBox>
                            </div>
                            <div style="float:left; margin:5px 10px">
                                <telerik:RadLabel runat="server" ID="ViewTask_CreatedInfo_Label" Text="Created by" Font-Bold="true"></telerik:RadLabel>
                                <telerik:RadTextBox runat="server" ID="ViewTask_CreatedInfo" RenderMode="Lightweight" Width="200px"></telerik:RadTextBox>
                            </div>
                            <div style="float:left; margin:5px 10px">
                                <telerik:RadButton runat="server" ID="ViewTask_OpenEventInformation" RenderMode="Lightweight" Skin="Simple"
                                    Text="View event" AutoPostBack="false" OnClientClicked="OpenEventFromTask">
                                    <Icon SecondaryIconCssClass="rbOpen" SecondaryIconRight="5px" />
                                </telerik:RadButton>
                            </div>
                        </div>
                        
                        <div style="margin:5%; clear:both">
                            <div style="float:left; margin:5px 10px">
                                <telerik:RadLabel runat="server" ID="ViewTask_Budget_Label" Text="Budget" Font-Bold="true"></telerik:RadLabel>
                                <telerik:RadTextBox runat="server" ID="ViewTask_Budget" RenderMode="Lightweight"></telerik:RadTextBox>
                            </div>
                            <div style="float:left; margin:5px 10px">
                                <telerik:RadLabel runat="server" ID="ViewTask_ExtraBudget_Label" Text="Needed budget (if needed)" Font-Bold="true"></telerik:RadLabel>
                                <telerik:RadNumericTextBox runat="server" ID="ViewTask_ExtraBudget" MinValue="0" RenderMode="Lightweight"></telerik:RadNumericTextBox>
                            </div>
                        </div>
                        <div style="margin:5%;clear:both">
                            <div style="float:left; margin:5px 10px">
                                <telerik:RadLabel runat="server" ID="ViewTask_ExtraComment_Label" Text="Reason for needed budget" Font-Bold="true"></telerik:RadLabel>
                                <telerik:RadTextBox runat="server" ID="ViewTask_ExtraComment" TextMode="MultiLine" Columns="80" Rows="5" Width="100%"
                                    EmptyMessage="Comment" RenderMode="Lightweight"  AutoPostBack="false" ></telerik:RadTextBox>
                            </div>
                        </div>
                        <div style="margin:5%; clear:both">
                            <telerik:RadButton runat="server" ID="ViewTask_Accept" RenderMode="Lightweight" Skin="Telerik"
                                OnClick="ViewTask_Accept_Click">
                                <Icon SecondaryIconCssClass="rbOk" SecondaryIconRight="5px" />
                            </telerik:RadButton>
                        </div>
                        <div style="clear:both"></div>
                    </div>
                </ContentTemplate>
            </telerik:RadWindow>
            <telerik:RadWindow runat="server" ID="Window_EventForm" Title="Event View">
                <ContentTemplate>
                    <h1>Event</h1>
                    <div style="background-color:aliceblue; padding:10px">
                        <div id="EventForm_Header">
                            <div style="float:left; margin:5px 10px">
                                <telerik:RadLabel runat="server" ID="EventForm_Name_Label" Text="Name" Font-Bold="true"></telerik:RadLabel>
                                <telerik:RadTextBox runat="server" ID="EventForm_Name" RenderMode="Lightweight"></telerik:RadTextBox>
                            </div>

                            <div style="float:left; margin:5px 10px">
                                <telerik:RadLabel runat="server" ID="EventForm_Description_Label" Text="Description" Font-Bold="true" ></telerik:RadLabel>
                                <telerik:RadTextBox runat="server" ID="EventForm_Description" RenderMode="Lightweight" Width="500px"></telerik:RadTextBox>
                            </div>
                        </div>
                        <div id="EventForm_Date" style="clear:both">

                            <div style="float:left; margin:5px 10px">
                                <telerik:RadLabel runat="server" ID="EventForm_From_Label" Text="Date Start" Font-Bold="true"></telerik:RadLabel>
                                <telerik:RadTextBox runat="server" ID="EventForm_From" RenderMode="Lightweight"></telerik:RadTextBox>
                            </div>

                            <div style="float:left; margin:5px 10px">
                                <telerik:RadLabel runat="server" ID="EventForm_To_Label" Text="Date End" Font-Bold="true"></telerik:RadLabel>
                                <telerik:RadTextBox runat="server" ID="EventForm_To" RenderMode="Lightweight"></telerik:RadTextBox>
                            </div>

                        </div>
                        <div id="EventForm_Info" style="clear:both">
                            <div style="float:left; margin:5px 10px">
                                <telerik:RadLabel runat="server" ID="EventForm_Comment_Label" Text="Financial Comment" Font-Bold="true"></telerik:RadLabel>
                                <telerik:RadTextBox runat="server" ID="EventForm_Comment" RenderMode="Lightweight"></telerik:RadTextBox>
                            </div>

                            <div style="float:left; margin:5px 10px">
                                <telerik:RadLabel runat="server" ID="EventForm_Budget_Label" Text="Budget" Font-Bold="true"></telerik:RadLabel>
                                <telerik:RadTextBox runat="server" ID="EventForm_Budget" RenderMode="Lightweight"></telerik:RadTextBox>
                            </div>

                            <div style="float:left; margin:5px 10px">
                                <telerik:RadLabel runat="server" ID="EventForm_Guests_Label" Text="Guests" Font-Bold="true"></telerik:RadLabel>
                                <telerik:RadTextBox runat="server" ID="EventForm_Guests" RenderMode="Lightweight"></telerik:RadTextBox>
                            </div>

                            <div style="float:left; margin:5px 10px">
                                <telerik:RadLabel runat="server" ID="EventForm_Type_Label" Text="Type of event" Font-Bold="true"></telerik:RadLabel>
                                <telerik:RadTextBox runat="server" ID="EventForm_Type" RenderMode="Lightweight"></telerik:RadTextBox>
                            </div>
                        </div>
                        <div id="EventForm_Task" runat="server"  style="clear:both">
                            <h3>Create a task</h3>
                            <div style="float:left; margin:5px 10px">
                                <telerik:RadLabel runat="server" ID="Task_Name_Label" Text="Name" Font-Bold="true"></telerik:RadLabel>
                                <telerik:RadTextBox runat="server" ID="Task_Name" EmptyMessage="Name..." RenderMode="Lightweight"  AutoPostBack="false" ></telerik:RadTextBox>
                            </div>
                           
                            <div style="float:left; margin:5px 10px">
                                <telerik:RadLabel runat="server" ID="Task_Descr_Label" Text="Description" Font-Bold="true"></telerik:RadLabel>
                                <telerik:RadTextBox runat="server" ID="Task_Descr" EmptyMessage="Description..." RenderMode="Lightweight"  AutoPostBack="false" ></telerik:RadTextBox>
                            </div>
                            
                            <div style="float:left; margin:5px 10px">
                                <telerik:RadLabel runat="server" ID="Task_Subteams_Label" Text="Assign a task to sub team" Font-Bold="true"></telerik:RadLabel>
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
                            </div>
                            
                            <div style="float:left; margin:5px 10px">
                                <telerik:RadLabel runat="server" ID="Task_Budget_Label" Text="Budget for task" Font-Bold="true"></telerik:RadLabel>
                                <telerik:RadNumericTextBox runat="server" ID="Task_Budget" EmptyMessage="Budget..." RenderMode="Lightweight"  AutoPostBack="false" >
                                </telerik:RadNumericTextBox>
                            </div>
                            
                            <div style="float:left; margin:5px 10px">
                                <telerik:RadLabel runat="server" ID="Task_DueDate_Label" Text="Do before" Font-Bold="true"></telerik:RadLabel>
                                <telerik:RadDatePicker runat="server" ID="Task_DueDate" DateInput-EmptyMessage="DueDate..." RenderMode="Lightweight"  AutoPostBack="false" ></telerik:RadDatePicker>
                            </div>
                            
                            <telerik:RadButton runat="server" ID="Task_AddTaskToEvent" Text="Create Task" AutoPostBack="true" RenderMode="Lightweight"
                                Skin="Simple" OnClick="Task_AddTaskToEvent_Click" ></telerik:RadButton>
                            <telerik:RadTextBox runat="server" ID="Task_Feedback" RenderMode="Lightweight" ></telerik:RadTextBox>
                        </div>
                        <div id="EventForm_Actions" style="clear:both">
                            <p>----</p>
                        </div>
                    </div>
                </ContentTemplate>
               
            </telerik:RadWindow>
            <telerik:RadWindow runat="server" ID="Window_NewEventRequestForm" Title="New Event Request">
                <ContentTemplate>
                    <div>
                        <h1>New event request</h1>
                        <div id="Div_NewEventRequestForm" style="background-color:aliceblue; padding:10px">
                            <div style="margin:5%">
                                <div style="float:left; margin:5px 10px">
                                    <telerik:RadLabel runat="server" ID="NewEventRequestForm_Name_Label" Text="Name" Font-Bold="true"></telerik:RadLabel>
                                    <telerik:RadTextBox runat="server" ID="NewEventRequestForm_Name" EmptyMessage="Name..." RenderMode="Lightweight"  AutoPostBack="false" ></telerik:RadTextBox>
                                </div>
                                <div style="float:left; margin:5px 10px ">
                                    <telerik:RadLabel runat="server" ID="NewEventRequestForm_Number_Label" Text="Contact number" Font-Bold="true"></telerik:RadLabel>
                                    <telerik:RadTextBox runat="server" ID="NewEventRequestForm_Number" EmptyMessage="Number..." RenderMode="Lightweight"  AutoPostBack="false" ></telerik:RadTextBox>
                                </div>
                                <div style="float:left; margin:5px 10px">
                                    <telerik:RadLabel runat="server" ID="NewEventRequestForm_Email_Label" Text="Contact email" Font-Bold="true"></telerik:RadLabel>
                                    <telerik:RadTextBox runat="server" ID="NewEventRequestForm_Email" EmptyMessage="Email..." RenderMode="Lightweight"  AutoPostBack="false" ></telerik:RadTextBox>
                                </div>
                            </div>
                            <div style="margin:5%;clear:both">
                                <div style="float:left; margin:5px 10px">
                                    <telerik:RadLabel runat="server" ID="NewEventRequestForm_Descr_Label" Text="Description" Font-Bold="true"></telerik:RadLabel>
                                    <telerik:RadTextBox runat="server" ID="NewEventRequestForm_Descr" TextMode="MultiLine" Columns="80" Rows="5" Width="100%"
                                         EmptyMessage="Description..." RenderMode="Lightweight"  AutoPostBack="false" ></telerik:RadTextBox>
                                </div>
                            </div>
                            <div style="margin:5%;clear:both">
                                <div style="float:left; margin:5px 10px">
                                    <telerik:RadLabel runat="server" ID="NewEventRequestForm_Type_Label" Text="Type of event" Font-Bold="true"></telerik:RadLabel>
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
                                    <telerik:RadLabel runat="server" ID="NewEventRequestForm_Budget_Label" Text="Budget idea" Font-Bold="true"></telerik:RadLabel>
                                    <telerik:RadNumericTextBox runat="server" ID="NewEventRequestForm_Budget" EmptyMessage="Budget..." RenderMode="Lightweight"  AutoPostBack="false" >
                                    </telerik:RadNumericTextBox>
                                </div>
                                <div style="float:left; margin:5px 10px">
                                    <telerik:RadLabel runat="server" ID="NewEventRequestForm_Guests_Label" Text="Number of guests" Font-Bold="true"></telerik:RadLabel>
                                    <div style="display:block">
                                        <telerik:RadNumericTextBox runat="server" ID="NewEventRequestForm_Guests" EmptyMessage="Guests..." RenderMode="Lightweight"  AutoPostBack="false" >
                                        </telerik:RadNumericTextBox>
                                    </div>
                                </div>
                            </div>
                            <div style="margin:5%;clear:both">
                                <div style="float:left; margin:5px 10px">
                                    <telerik:RadLabel runat="server" ID="NewEventRequestForm_DateFrom_Label" Text="Date from" Font-Bold="true"></telerik:RadLabel>
                                    <telerik:RadDatePicker runat="server" ID="NewEventRequestForm_DateFrom" Width="180px" DateInput-EmptyMessage="Starting Date" RenderMode="Lightweight"  AutoPostBack="false" >
                                    </telerik:RadDatePicker>
                                </div>
                                <div style="float:left; margin:5px 10px">
                                    <telerik:RadLabel runat="server" ID="NewEventRequestForm_DateTo_Label" Text="Date to" Font-Bold="true"></telerik:RadLabel>
                                    <telerik:RadDatePicker runat="server" ID="NewEventRequestForm_DateTo" Width="180px" DateInput-EmptyMessage="End Date" RenderMode="Lightweight"  AutoPostBack="false" >
                                    </telerik:RadDatePicker>
                                </div>
                           </div>
                            <div style="clear:both;float:right">
                            <telerik:RadButton runat="server" ID="NewEventRequestForm_SendNewRequest" Text="Create Task" AutoPostBack="true" RenderMode="Lightweight"
                                OnClick="NewEventRequestForm_SendNewRequest_Click" Skin="Simple" >
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
                    <h1>Request for event</h1>
                    <div style="background-color:aliceblue; padding:10px">
                        <div id="RequestEvent_Event" >
                            <div style="margin:5%">
                                <telerik:RadTextBox runat="server" ID="RequestEvent_EventId" RenderMode="Lightweight" Display="false" ></telerik:RadTextBox>
                                <div style="float:left; margin:5px 10px">
                                    <telerik:RadLabel runat="server" ID="RequestEvent_Name_Label" Text="Name" Font-Bold="true"></telerik:RadLabel>
                                    <telerik:RadTextBox runat="server" ID="RequestEvent_Name" RenderMode="Lightweight"></telerik:RadTextBox>
                                </div>
                                <div style="float:left; margin:5px 10px">
                                    <telerik:RadLabel runat="server" ID="RequestEvent_From_Label" Text="Event from" Font-Bold="true"></telerik:RadLabel>
                                    <telerik:RadTextBox runat="server" ID="RequestEvent_From" RenderMode="Lightweight"></telerik:RadTextBox>
                                </div>
                                <div style="float:left; margin:5px 10px">
                                    <telerik:RadLabel runat="server" ID="RequestEvent_To_Label" Text="Event to" Font-Bold="true"></telerik:RadLabel>
                                    <telerik:RadTextBox runat="server" ID="RequestEvent_To" RenderMode="Lightweight"></telerik:RadTextBox>
                                </div>
                                <div style="float:left; margin:5px 10px">
                                    <telerik:RadLabel runat="server" ID="RequestEvent_Budget_Label" Text="Budget" Font-Bold="true"></telerik:RadLabel>
                                    <telerik:RadTextBox runat="server" ID="RequestEvent_Budget" RenderMode="Lightweight"></telerik:RadTextBox>
                                </div>
                                 <div style="float:left; margin:5px 10px">
                                    <telerik:RadLabel runat="server" ID="RequestEvent_Guests_Label" Text="Guests" Font-Bold="true"></telerik:RadLabel>
                                    <telerik:RadTextBox runat="server" ID="RequestEvent_Guests" RenderMode="Lightweight"></telerik:RadTextBox>
                                </div>
                                <div style="float:left; margin:5px 10px">
                                    <telerik:RadLabel runat="server" ID="RequestEvent_Type_Label" Text="Type" Font-Bold="true"></telerik:RadLabel>
                                    <telerik:RadTextBox runat="server" ID="RequestEvent_Type" RenderMode="Lightweight"></telerik:RadTextBox>
                                </div>
                                <div style="float:left; margin:5px 10px">
                                    <telerik:RadLabel runat="server" ID="RequestEvent_Comment_Label" Text="Comment" Font-Bold="true"></telerik:RadLabel>
                                    <telerik:RadTextBox runat="server" ID="RequestEvent_Comment" RenderMode="Lightweight"></telerik:RadTextBox>
                                </div>
                            </div>
                            <div style="margin:5%;clear:both">
                                <div style="float:left; margin:5px 10px">
                                    <telerik:RadLabel runat="server" ID="RequestEvent_Descr_Label" Text="Description" Font-Bold="true"></telerik:RadLabel>
                                    <telerik:RadTextBox runat="server" ID="RequestEvent_Descr" RenderMode="Lightweight" TextMode="MultiLine" Columns="80" Rows="5" Width="100%"></telerik:RadTextBox>
                                </div>
                            </div>
                            <div style="margin:5%;clear:both">
                                <div style="float:left; margin:5px 10px">
                                    <telerik:RadLabel runat="server" ID="RequestEvent_FM_Budget_Label" Text="Financial Managers budget" Font-Bold="true"></telerik:RadLabel>
                                    <telerik:RadNumericTextBox runat="server" EmptyMessage="Financial Budget" 
                                        ID="RequestEvent_FM_Budget" RenderMode="Lightweight"></telerik:RadNumericTextBox>
                                </div>
                                <div style="float:left; margin:5px 10px">
                                    <telerik:RadLabel runat="server" ID="RequestEvent_FM_Comment_Label" Text="Financial Managers feedback" Font-Bold="true"></telerik:RadLabel>
                                    <telerik:RadTextBox runat="server" EmptyMessage="Financial feedback..."  ID="RequestEvent_FM_Comment" RenderMode="Lightweight" TextMode="MultiLine" Columns="80" Rows="5" Width="100%"></telerik:RadTextBox>
                                </div>
                            </div>
                            
                        </div>
                        <div id="RequestEvent_Buttons" style="margin-top:10px;clear:both">
                            <div style="float:left">
                                <telerik:RadButton runat="server" id="RequestEvent_Reject" Text="Reject Request" RenderMode="Lightweight"
                                    OnClick="RequestEvent_Reject_Click" Skin="Sunset">
                                    <Icon SecondaryIconCssClass="rbCancel" />
                                </telerik:RadButton>
                            </div>
                            <div style="float:right">
                                <telerik:RadButton runat="server" id="RequestEvent_Forward" Text="Accept and Forward" RenderMode="Lightweight"
                                     OnClick="RequestEvent_Forward_Click" Skin="Telerik">
                                    <Icon SecondaryIconCssClass="rbNext" />
                                </telerik:RadButton>
                            </div>
                            
                            <div style="clear:both"></div>
                        </div>
                        <div id="RequestEvent_Meeting" runat="server">
                            <div style="float:left">
                            <h2>Book a meeting!</h2>

                                <telerik:RadLabel runat="server" ID="RadLabel45" Text="Regarding" Font-Bold="true"></telerik:RadLabel>
                                <telerik:RadTimePicker ID="NewCourse_TimeFrom" runat="server"  RenderMode="Lightweight"  DateInput-EmptyMessage="Time start">
                                    <TimeView runat="server" StartTime="08:00" EndTime="17:00" Interval="0:30" TimeFormat="HH:mm"  ></TimeView>
                                </telerik:RadTimePicker>
                                <telerik:RadTimePicker ID="NewCourse_TimeTo" runat="server"   RenderMode="Lightweight" DateInput-EmptyMessage="Time end" >
                                    <TimeView runat="server" StartTime="08:00" EndTime="17:00" Interval="0:30" TimeFormat="HH:mm"  ></TimeView>
                                </telerik:RadTimePicker>
                            <telerik:RadCalendar RenderMode="Lightweight" runat="server" ID="RequestEvent_Calendar" AutoPostBack="false" 
                                MultiViewColumns="3" ShowNavigationButtons="false"
                                MultiViewRows="1" RangeSelectionMode="None" EnableViewSelector="true" 
                                Skin="Web20" EnableMultiSelect="false" 
                                ShowOtherMonthsDays="false" ShowFastNavigationButtons="false"
                                 >
            
                                <WeekendDayStyle BackColor="Tomato" />
                                 <SpecialDays>
                                    <telerik:RadCalendarDay Repeatable="Today">  
                                        <ItemStyle CssClass="rcToday" /> 
                                    </telerik:RadCalendarDay> 
                                </SpecialDays>
                            </telerik:RadCalendar>
                            </div>
                            <div style="float:right">
                                <telerik:RadButton runat="server" ID="RequestEvent_BookMeeting" Text="Book Meeting" RenderMode="Lightweight"
                                    OnClick="RequestEvent_BookMeeting_Click" Skin="Telerik">
                                    <Icon SecondaryIconCssClass="rbSave" SecondaryIconRight="5px" />
                                </telerik:RadButton>
                            </div>
                        </div>
                        <div style="clear:both"></div>

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
            <telerik:RadGrid runat="server" ID="Radgrid_Events" RenderMode="Lightweight" Skin="Office2007" Height="600px"
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
            <MasterTableView  AutoGenerateColumns="false" AllowMultiColumnSorting="true" DataKeyNames="EventId" >
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
                    <telerik:GridBoundColumn DataField="FinancialComment" ReadOnly="true" Display="true" HeaderText="Financial Comment"
                            UniqueName="FinancialComment">
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
        <h1>Hello </h1>
        <h2>Welcome to SED official event booking system</h2>
        <p>
            Please login by clicking the "Log in" button in the top right corner. If you cannot log in, please register by clicking "Register".
        </p>
    </div>
</asp:Content>
