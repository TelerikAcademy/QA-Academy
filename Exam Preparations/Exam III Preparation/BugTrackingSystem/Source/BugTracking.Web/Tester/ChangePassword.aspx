<%@ Page Title="" Language="C#" MasterPageFile="~/Administrator/Administrator.Master"
    AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="BugTrackingSystem.Administrator.ChangePassword" %>

<asp:Content ID="headContent" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="mainContent" ContentPlaceHolderID="mainContent" runat="server">
    <h1>Промяна на парола</h1>
    
    <div runat="server" id="content">

        <div class="row">
            <span class="formlabel">
            <asp:Label runat="server" ID="lblNewPassword" AssociatedControlID="txtNewPassword"
                    Text="Нова парола" />
            </span><span class="forminput">
            <asp:TextBox runat="server" ID="txtNewPassword" TextMode="Password" />
            <asp:RequiredFieldValidator runat="server" ID="reqNewPassword" ControlToValidate="txtNewPassword"
                            ErrorMessage="Полето е задължително" />
            <asp:RegularExpressionValidator runat="server" ID="regNewPassword" ControlToValidate="txtNewPassword"
                        ValidationExpression=".{3,32}" ErrorMessage="Дължината на новата паролата трябва да е между 3 и 32 символа" />
            </span>
        </div>

        <div class="row">
            <span class="formlabel">
                <asp:Label runat="server" ID="lblRepeatNewPassword" AssociatedControlID="txtRepeatNewPassword"
                    Text="Повтори нова парола" />
            </span>
            <span class="forminput">
                <asp:TextBox runat="server" ID="txtRepeatNewPassword" TextMode="Password"/>
                <asp:RequiredFieldValidator runat="server" ID="reqRepeatNewPassword" ControlToValidate="txtRepeatNewPassword"
                    ErrorMessage="Полето е задължително" />
                <asp:RegularExpressionValidator runat="server" ID="regRepeatNewPassword" ControlToValidate="txtRepeatNewPassword"
                ValidationExpression=".{3,32}" ErrorMessage="Дължината на новата паролата трябва да е между 3 и 32 символа" />
                <asp:CompareValidator runat="server" ID="compPasswords" ControlToValidate="txtRepeatNewPassword"
                    ControlToCompare="txtNewPassword" Operator="Equal" Type="String" ErrorMessage="Полетата за нова парола и повторна нова парола не са еднакви." />
            </span>
        </div>
    
        <ul class="buttons">
            <li>
                <asp:LinkButton runat="server" ID="btnCancel" OnClick="btnCancel_Click"
                    OnClientClick="DisableValidators();">Отказ</asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton runat="server" ID="btnChangePassword" OnClick="btnChangePassword_Click"
                    Text="Промени парола" />
            </li>
        </ul>
    </div>

    <asp:Label runat="server" ID="lblMessage" />
    <asp:LinkButton runat="server" ID="btnRedirect" OnClick="btnCancel_Click"
        style="display:none"></asp:LinkButton>

    <script type="text/javascript" language="javascript">
        function DisableValidators() {
            ValidatorEnable(document.getElementById('<%=reqNewPassword.ClientID %>'), false);
            ValidatorEnable(document.getElementById('<%=regNewPassword.ClientID %>'), false);
            ValidatorEnable(document.getElementById('<%=reqRepeatNewPassword.ClientID %>'), false);
            ValidatorEnable(document.getElementById('<%=regRepeatNewPassword.ClientID %>'), false);
            ValidatorEnable(document.getElementById('<%=compPasswords.ClientID %>'), false);
        }
    </script>
</asp:Content>
