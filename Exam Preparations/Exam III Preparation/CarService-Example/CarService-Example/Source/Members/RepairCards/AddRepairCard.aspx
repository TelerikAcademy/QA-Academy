<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="AddRepairCard.aspx.cs" Inherits="presentation.MembersAddRepairCard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="aspe" %>
<%@ Register Src="~/CustomControls/CalendarUserControl.ascx" TagName="CalendarUserControl" TagPrefix="ucCal" %>

<asp:Content ID="addRepairCardTitle" runat="server" ContentPlaceHolderID="pageTitle">Car Service - Add Repair Card</asp:Content>

<asp:Content ID="repairCardsBody" runat="server" ContentPlaceHolderID="pageBody">
    <h2>
        Repair Card Management  
    </h2>
    <p>
	    Use the form below to create repair card.			
    </p>
    <aspe:ToolkitScriptManager ID="toolScriptMgr" runat="server" />
    <asp:Label ID="notificationMsg" runat="server" Visible="false" />
    <asp:ValidationSummary ID="AddRepairCardValidationSummary" runat="server" CssClass="failureNotification" 
            ValidationGroup="AddRepairCardValidationGroup"/>
    <asp:BulletedList ID="notificationMsgList" runat="server" CssClass="failureNotification" Visible="false" />
	<div class="repairCardInfo">			
	<fieldset class="register">
		<legend>Repair card information</legend>
		<p>					
		<div class="table-row">
			<div class="left-container2"><p class="text"> 
				<label>Id:</label>
                <asp:Label ID="repairCardIdLbl" runat="server" />
                <label></label>

			<span>Operator:</span>
            <asp:Label ID="operatorLbl" runat="server" />
			</p></div> 
			<div class="right-container2"><p class="text"> 
                <%-- Search car control --%>
                <asp:ValidationSummary ID="SearchCarValidationSummary" runat="server" CssClass="failureNotification" 
                        ValidationGroup="SearchCarValidationGroup"/>             
                <label>Car:</label>
                <asp:DropDownList ID="automobileDropDown" runat="server" DataValueField="AutomobileId"  DataTextField="AutomobileRepresentation" >
                    <asp:ListItem Value="-1">Search car by vin/chassis</asp:ListItem>
                </asp:DropDownList>
                <asp:CustomValidator ID="automobileValidator" runat="server" ControlToValidate="automobileDropDown" CssClass="failureNotification"
                    ErrorMessage="Please select valid car." ToolTip="Please select valid car." ValidationGroup="AddRepairCardValidationGroup"
                    OnServerValidate="Automobile_ServerValidate">X</asp:CustomValidator>
                <label></label>
                <span>Vin / Chassis</span>
                <asp:TextBox ID="VinChassisTxt" runat="server" />
                <asp:RequiredFieldValidator ID="vinChassisRequired" runat="server" ControlToValidate="VinChassisTxt" 
                        CssClass="failureNotification" ErrorMessage="Vin/Chassis number is required." ToolTip="Vin/Chassis number is required." 
                        ValidationGroup="SearchCarValidationGroup">*</asp:RequiredFieldValidator>
                <asp:Button ID="searchVinChassisBtn" runat="server" Text="Search" OnClick="SearchAutomobile_OnClick" 
                    ValidationGroup="SearchCarValidationGroup" />
                <%-- Search car control --%>
			</p></div>						
		</div>

		<div class="table-row">
			<div class="left-container2"><p class="text"> 							
				<label>Start date:</label>
                <ucCal:CalendarUserControl ID="startRepairDate" runat="server" />
			</p></div> 
			<div class="right-container2"><p class="text">
				<label>Finish date:</label>
                <ucCal:CalendarUserControl ID="finishRepairDate" runat="server" /><label></label>	
			</p></div>						
		</div>	

		</p>					

		<p>										
		<p>			
		<h4 class="table-caption"> 
			Please select which spare parts will be used
		</h4> 					
		<div class="table-row">
			<div class="left-container2"><p class="text"> 
				<label>Spare parts:</label>						
                <asp:ListBox ID="unselectedSpareParts" Rows="5" SelectionMode="Multiple" runat="server" DataValueField="PartId" DataTextField="PartName">
                </asp:ListBox>
                <asp:Button ID="selectSparePartsBtn" Text="Select" runat="server" OnClick="SelectSpareParts_OnClick" />
			</p></div> 
			<div class="right-container2"><p class="text"> 
				<label>Selected:</label>
                <asp:ListBox ID="selectedSpareParts" Rows="5" SelectionMode="Multiple" runat="server" DataValueField="PartId" DataTextField="PartName">                    
                </asp:ListBox>
                <asp:Button ID="removeSparePartsBtn" Text="Remove" runat="server" OnClick="UnselectSpareParts_OnClick" />
			</p></div> 					
		</div>
		<div class="space-line"></div> 						
		<div class="space-line"></div> 						
		<div class="table-row">
			<div class="left-container2"><p class="text"> 
				<label>Spare parts price &#36;:</label>
                <asp:Label ID="sparePartsPrice" runat="server" />
			</p></div> 
			<div class="right-container2"><p class="text"> 
				<label>Repair price &#36;:</label>
                <asp:TextBox ID="repairPrice" runat="server" CssClass="textEntry" />
                <asp:RequiredFieldValidator ID="RepairPriceRequiredValidator" runat="server" ControlToValidate="repairPrice" 
                        CssClass="failureNotification" ErrorMessage="Repair price is required." ToolTip="Repair price is required." 
                        ValidationGroup="AddRepairCardValidationGroup">*</asp:RequiredFieldValidator>
                <asp:CustomValidator ID="RepairPriceValidator" runat="server" ControlToValidate="repairPrice" CssClass="failureNotification"
                    ErrorMessage="Repair price is not valid." ToolTip="Repair price is not valid." ValidationGroup="AddRepairCardValidationGroup"
                    OnServerValidate="Price_ServerValidate">X</asp:CustomValidator>
			</p></div> 					
		</div>
		<div class="space-line"></div>														
		</p>					
        <p>
		<div class="table-row">
			<div class="left-container2"><p class="text"> 							
				<label>Description:</label>
                <asp:TextBox ID="repairCardDescription" runat="server" TextMode="MultiLine" />
			</p></div> 						
		</div>						
		<div class="space-line"></div>																			        
        </p>		
	</fieldset>	
	<p class="submitButton">
            <asp:Button ID="CancelAutoButton" runat="server" Text="Cancel" OnClick="CancelRepairCard_OnClick" />
            <asp:Button ID="CreateAutoButton" runat="server" Text="Save" OnClick="SaveRepairCard_OnClick"
                    ValidationGroup="AddRepairCardValidationGroup"/>
	</p>				
	</div>
</asp:Content>
