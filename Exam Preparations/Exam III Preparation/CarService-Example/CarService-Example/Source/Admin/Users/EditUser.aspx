<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="EditUser.aspx.cs" Inherits="presentation.AdminEditUser" %>

<asp:Content ID="editUseritle" runat="server" ContentPlaceHolderID="pageTitle">Car Service - Edit User</asp:Content>

<asp:Content ID="editUserBody" runat="server" ContentPlaceHolderID="pageBody">      
    <h2>
        Edit User Account
    </h2>
    <p>
        Use the form below to edit account.
    </p>
    <p>
        Passwords are required to be a minimum of <%= Membership.MinRequiredPasswordLength %> characters in length.
    </p>
    <span class="failureNotification">
        <asp:Literal ID="ErrorMessage" runat="server"></asp:Literal>
    </span>
    <asp:ValidationSummary ID="EditUserValidationSummary" runat="server" CssClass="failureNotification" 
            ValidationGroup="EditUserValidationGroup"/>
    <div class="accountInfo">
        <fieldset class="register">
            <legend>Account Information</legend>
            <p>
                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User Name:</asp:Label>
                <asp:TextBox ID="UserName" runat="server" CssClass="textEntry" Enabled="false"></asp:TextBox>
                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" 
                        CssClass="failureNotification" ErrorMessage="User Name is required." ToolTip="User Name is required." 
                        ValidationGroup="EditUserValidationGroup">*</asp:RequiredFieldValidator>
            </p>
            <p>
                <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email">E-mail:</asp:Label>
                <asp:TextBox ID="Email" runat="server" CssClass="textEntry"></asp:TextBox>
                <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email" 
                        CssClass="failureNotification" ErrorMessage="E-mail is required." ToolTip="E-mail is required." 
                        ValidationGroup="EditUserValidationGroup">*</asp:RequiredFieldValidator>
            </p>
            <p>
                <asp:Label ID="FirstNameLabel" runat="server">First Name:</asp:Label>
                <asp:TextBox ID="FirstName" runat="server" CssClass="textEntry"></asp:TextBox>
                <asp:RequiredFieldValidator ControlToValidate="FirstName" CssClass="failureNotification" Display="Dynamic" 
                        ErrorMessage="First name is required." ID="FirstNameRequired" runat="server" 
                        ToolTip="First name is required." ValidationGroup="EditUserValidationGroup">*</asp:RequiredFieldValidator>
            </p>
            <p>
                <asp:Label ID="LastNameLabel" runat="server">Last Name:</asp:Label>
                <asp:TextBox ID="LastName" runat="server" CssClass="textEntry"></asp:TextBox>
                <asp:RequiredFieldValidator ControlToValidate="LastName" CssClass="failureNotification" Display="Dynamic" 
                        ErrorMessage="Last name is required." ID="LastNameRequired" runat="server" 
                        ToolTip="Last name is required." ValidationGroup="EditUserValidationGroup">*</asp:RequiredFieldValidator>
            </p>
            <p>
                <span>
                    <asp:Label ID="UserActiveLabel" runat="server">Active</asp:Label>
                    <asp:DropDownList ID="UserActive" runat="server">
                        <asp:ListItem Value="1" Text="Yes" Selected="True" />
                        <asp:ListItem Value="0" Text="No" />
                    </asp:DropDownList>
                </span>
            </p>
        </fieldset>
        <p class="submitButton">
            <asp:Button ID="CancelUserButton" runat="server" OnClick="CancelEventHandler_OnClick" Text="Cancel" />
            <asp:Button ID="CreateUserButton" runat="server" OnClick="SaveEventHandler_OnClick" Text="Save" 
                    ValidationGroup="EditUserValidationGroup"/>
        </p>
    </div>
    <asp:ValidationSummary ID="EditUserPasswordValidationSummary" runat="server" CssClass="failureNotification" 
        ValidationGroup="EditUserPasswordValidationGroup"/>
    <div class="accountInfo">
        <fieldset class="register">
            <legend>Reset Password</legend>
            <p>
                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">New Password:</asp:Label>
                <asp:TextBox ID="Password" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" 
                        CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required." 
                        ValidationGroup="EditUserPasswordValidationGroup">*</asp:RequiredFieldValidator>                                     
            </p>
            <p>
                <asp:Label ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPassword">Confirm New Password:</asp:Label>
                <asp:TextBox ID="ConfirmPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ControlToValidate="ConfirmPassword" CssClass="failureNotification" Display="Dynamic" 
                        ErrorMessage="Confirm Password is required." ID="ConfirmPasswordRequired" runat="server" 
                        ToolTip="Confirm Password is required." ValidationGroup="EditUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword" 
                        CssClass="failureNotification" Display="Dynamic" ErrorMessage="The Password and Confirmation Password must match."
                        ValidationGroup="EditUserPasswordValidationGroup">*</asp:CompareValidator>
            </p>                    
            <p class="submitButton">
                <asp:Button ID="CancelRestPasswordButton" runat="server" OnClick="CancelEventHandler_OnClick" Text="Cancel" />
                <asp:Button ID="RestPasswordButton" runat="server" OnClick="ResetPasswordEventHandler_OnClick" Text="Reset password" 
                        ValidationGroup="EditUserPasswordValidationGroup"/>
            </p>
        </fieldset>
    </div>
    <p class="submitButton">Go back to <a href="Users.aspx">Users</a></p>
</asp:Content>