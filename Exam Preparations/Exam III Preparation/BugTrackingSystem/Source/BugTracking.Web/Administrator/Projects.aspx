<%@ Page Title="" Language="C#" MasterPageFile="~/Administrator/Administrator.Master" AutoEventWireup="true" CodeBehind="Projects.aspx.cs" Inherits="BugTrackingSystem.Administrator.Projects" %>
<asp:Content ID="headContent" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="mainContent" ContentPlaceHolderID="mainContent" runat="server">
<h1>Проекти</h1>
<div class="form">
<asp:ListView runat="server" ID="lvProjects" ItemPlaceholderID="itemPlaceHolder" OnItemCommand="lvProjects_ItemCommand">
    <LayoutTemplate>
        <table align="center">
            <thead>
                <th><asp:LinkButton runat="server" ID="btnNumber" CommandName="SortNumber" CommandArgument="DESC" Text="Номер" /></th>
                <th><asp:LinkButton runat="server" ID="btnName" CommandName="SortName" CommandArgument="ASC" Text="Име" /></th>
                <th><asp:LinkButton runat="server" ID="btnDescription" CommandName="SortDescription" CommandArgument="ASC" Text="Описание" /></th>
                <th colspan="2"></th>
            </thead>
            <tbody>
                <asp:PlaceHolder runat="server" ID="itemPlaceHolder" />
            </tbody>
        </table>
    </LayoutTemplate>
    <ItemTemplate>
        <tr <%# Container.DataItemIndex % 2 == 0?"class=\"odd\"":"" %>>
            <td><%# Eval("ProjectId") %></td>
            <td><%# Server.HtmlEncode(Eval("Name").ToString()) %></td>
            <td><%# Data.Utilities.ShortDescription(Server.HtmlEncode(Eval("Description").ToString()))%></td>
            <td><asp:LinkButton runat="server" ID="btnEdit" CommandName="EditProject" 
                CommandArgument='<%#Eval("ProjectId") %>' Text="Промени"/></td>
            <td><asp:LinkButton runat="server" ID="btnDelete" OnClientClick="return Confirm();" 
                CommandName="DeleteProject" CommandArgument='<%#Eval("ProjectId") %>' Text="Изтрий" /></td>
        </tr>
    </ItemTemplate>
    <EmptyDataTemplate>
        <p>Няма регистрирани проекти в системата</p>
    </EmptyDataTemplate>
</asp:ListView>
</div>
<div class="pager">
    <asp:LinkButton runat="server" ID="btnPrev" CommandName="Prev" OnCommand="btnPrev_Command" Text="Предишна" />
    <asp:Label runat="server" ID="lblCurrentPage" />
    <asp:LinkButton runat="server" ID="btnNext" CommandName="Next" OnCommand="btnNext_Command" Text="Следваща" />
</div>
<ul class="buttons">
<li><asp:LinkButton runat="server" ID="btnAdd" OnClick="btnAdd_Click" Text="Добави нов проект" CssClass="btn"/></li>
</ul>
<script type="text/javascript">
    function Confirm() {
        return confirm("Сигурни ли сте че искате да изтриете проекта ?");
    }
</script>
</asp:Content>
