<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CalendarUserControl.ascx.cs" Inherits="presentation.controls.CalendarUserControl" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="aspe" %>

<asp:Panel ID="calendarControl" runat="server">
    <asp:TextBox ID="selectedDateTxt" runat="server" />
    <asp:ImageButton ID="calendarBtn" ImageUrl="~/Resources/Images/calendar_scheduleHS.png" runat="server" CssClass="calendar" />    
    <aspe:CalendarExtender ID="CalendarExtender" TargetControlID="selectedDateTxt" PopupButtonID="calendarBtn" PopupPosition="BottomRight" Format="yyyy-MM-dd" runat="server" />
</asp:Panel>
