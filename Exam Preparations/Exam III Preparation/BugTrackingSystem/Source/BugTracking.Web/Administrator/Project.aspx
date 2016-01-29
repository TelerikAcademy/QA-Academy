<%@ Page Title="" Language="C#" MasterPageFile="~/Administrator/Administrator.Master" AutoEventWireup="true" CodeBehind="Project.aspx.cs" Inherits="BugTrackingSystem.Administrator.Project" %>
<asp:Content ID="headContent" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="mainContent" ContentPlaceHolderID="mainContent" runat="server">
   <h1 runat="server" id="header"></h1>
    <div class="row">
        <span class="formlabel"><asp:Label runat="server" ID="lblName" Text="Име" AssociatedControlID="txtName" /></span>
        <span class="forminput">
            <asp:TextBox runat="server" ID="txtName"/>
            <asp:RequiredFieldValidator runat="server" ID="rfvName" ControlToValidate="txtName" ErrorMessage="Полето е задължително"/>
            <asp:CustomValidator runat="server" ID="cuvName" ControlToValidate="txtName" OnServerValidate="cuvName_ServerValidate" ErrorMessage="Съществува проект с въведеното име" />
        </span>
    </div>
    <div class="row">
        <span class="formlabel"><asp:Label runat="server" ID="lblDescription" AssociatedControlID="txtDescription" Text="Описание" /></span>
        <span class="forminput">
            <asp:TextBox runat="server" ID="txtDescription" Rows="10" Columns="40" TextMode="MultiLine" CssClass="textarea" />
            <asp:RequiredFieldValidator runat="server" ID="rfvDescription" ControlToValidate="txtDescription" ErrorMessage="Полето е задължително"/>
        </span>
    </div>
<ul class="buttons">
<li><asp:LinkButton runat="server" ID="btnCancel" OnClick="btnCancel_Click" OnClientClick="DisableValidators();" Text="Отказ"/></li>
<li><asp:LinkButton runat="server" ID="btnSave" OnClick="btnSave_Click" Text="Запази"/></li>
</ul>

<script type="text/javascript" language="javascript">
    function DisableValidators() {
        ValidatorEnable(document.getElementById('<%=rfvName.ClientID %>'), false);
        ValidatorEnable(document.getElementById('<%=rfvDescription.ClientID %>'), false);
    }
    
</script>
</asp:Content>
