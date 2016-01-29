<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="AddUser.aspx.cs" Inherits="presentation.AdminAddUser" %>

<asp:Content ID="addUserTitle" runat="server" ContentPlaceHolderID="pageTitle">Car Service - Add User</asp:Content>

<asp:Content ID="addUserBody" runat="server" ContentPlaceHolderID="pageBody">
    <div class="accountInfo">
    <asp:CreateUserWizard ID="RegisterUser" runat="server" EnableViewState="false" OnCreatedUser="RegisterUser_CreatedUser" DuplicateUserNameErrorMessage="User name already exists." CancelDestinationPageUrl="~/Admin/Users/Users.aspx" LoginCreatedUser="false">
        <LayoutTemplate>
            <asp:PlaceHolder ID="wizardStepPlaceholder" runat="server"></asp:PlaceHolder>
            <asp:PlaceHolder ID="navigationPlaceholder" runat="server"></asp:PlaceHolder>
        </LayoutTemplate>
        <WizardSteps>
            <asp:CreateUserWizardStep ID="RegisterUserWizardStep" runat="server">
                <ContentTemplate>
                    <h2>
                        Create a New Account
                    </h2>
                    <p>
                        Use the form below to create a new account.
                    </p>
                    <p>
                        Passwords are required to be a minimum of <%= Membership.MinRequiredPasswordLength %> characters in length.
                    </p>
                    <span class="failureNotification">
                        <asp:Literal ID="ErrorMessage" runat="server"></asp:Literal>
                    </span>
                    <asp:ValidationSummary ID="RegisterUserValidationSummary" runat="server" CssClass="failureNotification" 
                         ValidationGroup="RegisterUserValidationGroup"/>
                    <div class="accountInfo">
                        <fieldset class="register">
                            <legend>Account Information</legend>
                            <p>
                                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User Name:</asp:Label>
                                <asp:TextBox ID="UserName" runat="server" CssClass="textEntry"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" 
                                     CssClass="failureNotification" ErrorMessage="User Name is required." ToolTip="User Name is required." 
                                     ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="UserNameRegEx" runat="server" ControlToValidate="UserName" ValidationExpression="^[a-zA-Z0-9]{3,}$"
                                    CssClass="failureNotification" ErrorMessage="User Name should be at least 3 characters long and can contain only latin letters and digits" ToolTip="User Name should be at least 3 characters long and can contain only latin letters and digits"
                                    ValidationGroup="RegisterUserValidationGroup" Display="Dynamic" />
                            </p>
                            <p>
                                <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email">E-mail:</asp:Label>
                                <asp:TextBox ID="Email" runat="server" CssClass="textEntry"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email" 
                                     CssClass="failureNotification" ErrorMessage="E-mail is required." ToolTip="E-mail is required." 
                                     ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="UserEmailRegEx" runat="server" ControlToValidate="Email" ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$"
                                    CssClass="failureNotification" ErrorMessage="Email is not in valid format" ToolTip="Email is not in valid format"
                                    ValidationGroup="RegisterUserValidationGroup" Display="Dynamic" />
                            </p>
                            <p>
                                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                                <asp:TextBox ID="Password" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" 
                                     CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required." 
                                     ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                            </p>
                            <p>
                                <asp:Label ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPassword">Confirm Password:</asp:Label>
                                <asp:TextBox ID="ConfirmPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="ConfirmPassword" CssClass="failureNotification" Display="Dynamic" 
                                     ErrorMessage="Confirm Password is required." ID="ConfirmPasswordRequired" runat="server" 
                                     ToolTip="Confirm Password is required." ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword" 
                                     CssClass="failureNotification" Display="Dynamic" ErrorMessage="The Password and Confirmation Password must match."
                                     ValidationGroup="RegisterUserValidationGroup">*</asp:CompareValidator>
                            </p>
                            <p>
                                <asp:Label ID="FirstNameLabel" runat="server">First Name:</asp:Label>
                                <asp:TextBox ID="FirstName" runat="server" CssClass="textEntry"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="FirstName" CssClass="failureNotification" Display="Dynamic" 
                                     ErrorMessage="First name is required." ID="FirstNameRequired" runat="server" 
                                     ToolTip="First name is required." ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                            </p>
                            <p>
                                <asp:Label ID="LastNameLabel" runat="server">Last Name:</asp:Label>
                                <asp:TextBox ID="LastName" runat="server" CssClass="textEntry"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="LastName" CssClass="failureNotification" Display="Dynamic" 
                                     ErrorMessage="Last name is required." ID="LastNameRequired" runat="server" 
                                     ToolTip="Last name is required." ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
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
                            <asp:Button ID="CancelUserButton" runat="server" CommandName="Cancel" Text="Cancel" />
                            <asp:Button ID="CreateUserButton" runat="server" CommandName="MoveNext" Text="Create User" 
                                 ValidationGroup="RegisterUserValidationGroup"/>
                        </p>
                    </div>
                </ContentTemplate>
                <CustomNavigationTemplate>
                </CustomNavigationTemplate>
            </asp:CreateUserWizardStep>
        </WizardSteps>
    </asp:CreateUserWizard>
    </div>
</asp:Content>