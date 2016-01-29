<%@ Page Title="" Language="C#" MasterPageFile="~/Administrator/Administrator.Master"
    AutoEventWireup="true" CodeBehind="Tester.aspx.cs" Inherits="BugTrackingSystem.Administrator.Tester" %>

<asp:Content ID="headContent" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="mainContent" ContentPlaceHolderID="mainContent" runat="server">
    <h1 runat="server" id="header">
    </h1>
    <div class="row">
        <span class="formlabel">
            <asp:Label runat="server" ID="lblUsername" AssociatedControlID="txtUsername" Text="Потребителско име" /></span>
        <span class="forminput">
            <asp:TextBox runat="server" ID="txtUsername" />
            <asp:Label runat="server" ID="lblUsernameEditMode" Visible="false" />
            <asp:RequiredFieldValidator runat="server" ID="reqUsername" ControlToValidate="txtUsername"
                ErrorMessage="Полето е задължително" />
            <asp:RegularExpressionValidator runat="server" ID="regUsername" ControlToValidate="txtUsername"
                ValidationExpression=".{3,32}" ErrorMessage="Дължината на потребителското име трябва да е между 3 и 32 символа" />
            <asp:CustomValidator runat="server" ID="cuvUsername" ControlToValidate="txtUsername"
                OnServerValidate="cuvUsername_ServerValidate" />
        </span>
    </div>
    <div class="row" id="rowPassword" runat="server">
        <span class="formlabel">
            <asp:Label runat="server" ID="lblPassword" AssociatedControlID="txtPassword" Text="Парола" /></span>
        <span class="forminput">
            <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" />
            <asp:RequiredFieldValidator runat="server" ID="reqPassword" ControlToValidate="txtPassword"
                ErrorMessage="Полето е задължително" />
            <asp:RegularExpressionValidator runat="server" ID="regPassword" ControlToValidate="txtPassword"
                ValidationExpression=".{3,32}" ErrorMessage="Дължината на паролата трябва да е между 3 и 32 символа" />
        </span>
    </div>
    <div class="row" id="rowRepeatPassword" runat="server">
        <span class="formlabel">
            <asp:Label runat="server" ID="lblRepeatPassword" AssociatedControlID="txtRepeatPassword"
                Text="Повтори парола" /></span> <span class="forminput">
                    <asp:TextBox runat="server" ID="txtRepeatPassword" TextMode="Password" />
                    <asp:RequiredFieldValidator runat="server" ID="reqRepeatPassword" ControlToValidate="txtRepeatPassword"
                        ErrorMessage="Полето е задължително" />
                    <asp:RegularExpressionValidator runat="server" ID="regRepeatPassword" ControlToValidate="txtRepeatPassword"
                        ValidationExpression=".{3,32}" ErrorMessage="Дължината на паролата трябва да е между 3 и 32 символа" />
                    <asp:CompareValidator runat="server" ID="compPasswords" ControlToValidate="txtRepeatPassword"
                        ControlToCompare="txtPassword" Operator="Equal" Type="String" ErrorMessage="Полетата за парола и повторна парола не са еднакви." />
                </span>
    </div>
    <div class="row">
        <span class="formlabel">
            <asp:Label runat="server" ID="lblName" AssociatedControlID="txtName" Text="Име" /></span>
        <span class="forminput">
            <asp:TextBox runat="server" ID="txtName" />
            <asp:RequiredFieldValidator runat="server" ID="reqName" ControlToValidate="txtName"
                ErrorMessage="Полето е задължително" />
        </span>
    </div>
    <div class="row">
        <span class="formlabel">
            <asp:Label runat="server" ID="lblSurname" AssociatedControlID="txtSurname" Text="Фамилия" /></span>
        <span class="forminput">
            <asp:TextBox runat="server" ID="txtSurname" />
            <asp:RequiredFieldValidator runat="server" ID="reqSurname" ControlToValidate="txtSurname"
                ErrorMessage="Полето е задължително" />
        </span>
    </div>
    <div class="row">
        <span class="formlabel">
            <asp:Label runat="server" ID="lblEmail" AssociatedControlID="txtEmail" Text="Имейл" /></span>
        <span class="forminput">
            <asp:TextBox runat="server" ID="txtEmail" />
            <asp:RequiredFieldValidator runat="server" ID="reqEmail" ControlToValidate="txtEmail"
                ErrorMessage="Полето е задължително" />
            <asp:RegularExpressionValidator runat="server" ID="regEmail" ControlToValidate="txtEmail"
                ErrorMessage="Грешен формат на имейла" ValidationExpression="^[\w\.=-]+@[\w\.-]+\.[\w]{2,4}$" />
        </span>
    </div>
    <div class="row">
        <span class="formlabel">
            <asp:Label runat="server" ID="lblPhone" AssociatedControlID="txtPhone" Text="Телефон" /></span>
        <span class="forminput">
            <asp:TextBox runat="server" ID="txtPhone" />
            <asp:RequiredFieldValidator runat="server" ID="reqPhone" ControlToValidate="txtPhone"
                ErrorMessage="Полето е задължително" />
        </span>
    </div>
    <div class="row">
        <span class="formlabel">
            <asp:LinkButton runat="server" ID="btnChangePassword" 
            OnClick="btnChangePassword_Click" Visible="false">Промени парола</asp:LinkButton></span>
    </div>
    <ul class="buttons">
        <li>
            <asp:LinkButton runat="server" ID="btnCancel" OnClick="btnCancel_Click" OnClientClick="DisableValidators();"
                Text="Отказ" /></li>
        <li>
            <asp:LinkButton runat="server" ID="btnSave" OnClick="btnSave_Click" Text="Запази"/></li>
    </ul>
    <script type="text/javascript" language="javascript">
        function DisableValidators() {
            ValidatorEnable(document.getElementById('<%=reqUsername.ClientID %>'), false);
            ValidatorEnable(document.getElementById('<%=regUsername.ClientID %>'), false);
            if (document.getElementById('<%=reqPassword.ClientID %>') != null) {
                ValidatorEnable(document.getElementById('<%=reqPassword.ClientID %>'), false);
                ValidatorEnable(document.getElementById('<%=regPassword.ClientID %>'), false);
                ValidatorEnable(document.getElementById('<%=reqRepeatPassword.ClientID %>'), false);
                ValidatorEnable(document.getElementById('<%=regRepeatPassword.ClientID %>'), false);
                ValidatorEnable(document.getElementById('<%=compPasswords.ClientID %>'), false);
            }
            ValidatorEnable(document.getElementById('<%=reqName.ClientID %>'), false);
            ValidatorEnable(document.getElementById('<%=reqSurname.ClientID %>'), false);
            ValidatorEnable(document.getElementById('<%=reqEmail.ClientID %>'), false);
            ValidatorEnable(document.getElementById('<%=regEmail.ClientID %>'), false);
            ValidatorEnable(document.getElementById('<%=reqPhone.ClientID %>'), false);
        }
    </script>
</asp:Content>
