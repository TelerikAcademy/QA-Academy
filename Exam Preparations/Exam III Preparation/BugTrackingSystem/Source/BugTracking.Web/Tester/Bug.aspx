<%@ Page Title="" Language="C#" MasterPageFile="~/Tester/Tester.Master" AutoEventWireup="true"
    CodeBehind="Bug.aspx.cs" Inherits="BugTrackingSystem.Tester.Bug" %>

<asp:Content ID="headContent" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="mainContent" ContentPlaceHolderID="mainContent" runat="server">
    <h1 runat="server" id="header">
    </h1>
    <div class="row" runat="server" id="trDate" visible="false">
        <span class="formlable">
            <asp:Label runat="server" ID="lblDate" Text="Дата на създаване" /></span> <span class="forminput">
                <asp:Label runat="server" ID="lblDateValue" /></span>
    </div>
    <div class="row" runat="server" id="trOwner" visible="false">
        <span class="formlable">
            <asp:Label runat="server" ID="lblOwner" Text="Собственик" /></span> <span class="forminput">
                <asp:Label runat="server" ID="lblOwnerValue" /></span>
    </div>
    <div class="row">
        <span class="formlable">
            <asp:Label runat="server" ID="lblDescription" Text="Описание" /></span> <span class="forminput">
                <asp:TextBox runat="server" ID="txtDescription" TextMode="MultiLine" Rows="10" Columns="40"
                    CssClass="textarea" /></span>
        <asp:RequiredFieldValidator runat="server" ID="rfvDescription" ControlToValidate="txtDescription"
            ErrorMessage="Полето е задължително" />
    </div>
    <div class="row">
        <span class="formlable">
            <asp:Label runat="server" ID="lblPriority" Text="Приоритет" /></span> <span class="forminput">
                <asp:DropDownList runat="server" ID="ddlPriority" />
            </span>
    </div>
    <div class="row">
        <span class="formlable">
            <asp:Label runat="server" ID="lblProject" Text="Проект" /></span> <span class="forminput">
                <asp:DropDownList runat="server" ID="ddlProjects" />
                <asp:Label runat="server" ID="lblProjectValue" />
            </span>
    </div>
    <div class="row" runat="server" id="trSattus" visible="false">
        <span class="formlable">
            <asp:Label runat="server" ID="lblStatus" Text="Статус" /></span> <span class="forminput">
                <asp:DropDownList runat="server" ID="ddlStatus" />
            </span>
    </div>
    <ul class="buttons">
        <li>
            <asp:LinkButton runat="server" ID="btnCancel" OnClientClick="DisableValidators();"
                OnClick="btnCancel_Click" Text="Отказ" /></li>
        <li>
            <asp:LinkButton runat="server" ID="btnSave" OnClick="btnSave_Click" Text="Запази" /></li>
    </ul>
    <script type="text/javascript" language="javascript">
        function DisableValidators() {
            ValidatorEnable(document.getElementById('<%=rfvDescription.ClientID %>'), false);
        }
    </script>
</asp:Content>
