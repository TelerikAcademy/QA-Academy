<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="AddSparePart.aspx.cs" Inherits="presentation.AdminAddSparePart" %>

<asp:Content ID="sparePartsTitle" runat="server" ContentPlaceHolderID="pageTitle">Car Service - Spare Part Management</asp:Content>

<asp:Content ID="sparePartsBody" runat="server" ContentPlaceHolderID="pageBody">
    <h2>
	    Spare Part Management
    </h2>
    <p>
		Use the form below to create or edit a spare part.		
    </p>
    <asp:ValidationSummary ID="AddSparePartValidationSummary" runat="server" CssClass="failureNotification" 
            ValidationGroup="AddSparePartValidationGroup"/>
    <asp:BulletedList ID="notificationMsgList" runat="server" CssClass="failureNotification" Visible="false" />
	<div class="accountInfo">
		<fieldset class="register">
			<legend>Spare part information</legend>
			<p>
				<span>Id:</span>
                <asp:TextBox ID="PartId" runat="server" CssClass="textEntry" Enabled="false"></asp:TextBox>
                <asp:RequiredFieldValidator ID="PartIdRequired" runat="server" ControlToValidate="PartId" 
                        CssClass="failureNotification" ErrorMessage="ID is required." ToolTip="Spare part ID is required." 
                        ValidationGroup="AddSparePartValidationGroup">*</asp:RequiredFieldValidator>
			</p>
			<p>
				<span>Name:</span>
                <asp:TextBox ID="PartName" runat="server" CssClass="textEntry"></asp:TextBox>
                <asp:RequiredFieldValidator ID="PartNameRequired" runat="server" ControlToValidate="PartName" 
                        CssClass="failureNotification" ErrorMessage="Name is required." ToolTip="Spare part name is required." 
                        ValidationGroup="AddSparePartValidationGroup">*</asp:RequiredFieldValidator>
			</p>					
			<p>
				<span>Price in &#36;:</span>
                <asp:TextBox ID="PartPrice" runat="server" CssClass="textEntry"></asp:TextBox>
                <asp:RequiredFieldValidator ID="PartPriceRequired" runat="server" ControlToValidate="PartPrice" 
                        CssClass="failureNotification" ErrorMessage="Price is required." ToolTip="Spare part price is required." 
                        ValidationGroup="AddSparePartValidationGroup">*</asp:RequiredFieldValidator>
			</p>										
			<p>
                <asp:Label ID="PartActiveLabel" runat="server">Active</asp:Label>
                <asp:DropDownList ID="PartActive" runat="server">
                    <asp:ListItem Value="1" Text="Yes" Selected="True" />
                    <asp:ListItem Value="0" Text="No" />
                </asp:DropDownList>
			</p>												
		</fieldset>
        <p class="submitButton">
            <asp:Button ID="CancelUserButton" runat="server" Text="Cancel" OnClick="CancelPart_OnClick" />
            <asp:Button ID="CreateUserButton" runat="server" Text="Save Part" OnClick="AddPart_OnClick"
                    ValidationGroup="AddSparePartValidationGroup"/>
        </p>
	</div>	
</asp:Content>
