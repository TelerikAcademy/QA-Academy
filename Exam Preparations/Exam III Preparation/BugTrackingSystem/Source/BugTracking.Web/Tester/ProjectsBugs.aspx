<%@ Page Title="" Language="C#" MasterPageFile="~/Tester/Tester.Master" AutoEventWireup="true" CodeBehind="ProjectsBugs.aspx.cs" Inherits="BugTrackingSystem.Tester.ProjectsBugs" %>
<asp:Content ID="headContent" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="mainContent" ContentPlaceHolderID="mainContent" runat="server">
<h1>Грешки по проекти</h1>
<asp:ScriptManager runat="server" ID="scriptManager"></asp:ScriptManager>
<asp:DropDownList runat="server" ID="ddlProjects" OnSelectedIndexChanged="ddlProjects_SelectedIndexChanged" AutoPostBack="true"/>
<div class="spacer"></div>
<asp:UpdatePanel runat="server" ID="updBugs" ChildrenAsTriggers="true">
    <ContentTemplate>
        <div class="form">
        <asp:ListView runat="server" ID="lvBugs" ItemPlaceholderID="itemPlaceHolder" OnItemCommand="lvBugs_ItemCommand">
            <LayoutTemplate>
                <table>
                    <thead>
                        <th><asp:LinkButton runat="server" ID="btnId" CommandName="SortId" CommandArgument="DESC" Text="Номер на грешка" /></th>
                        <th><asp:LinkButton runat="server" ID="btnOwner" CommandName="SortOwner" CommandArgument="ASC" Text="Собственик" /></th>
                        <th><asp:LinkButton runat="server" ID="btnPriority" CommandName="SortPriority" CommandArgument="ASC" Text="Приоритет" /></th>
                        <th><asp:LinkButton runat="server" ID="btnDate" CommandName="SortDate" CommandArgument="ASC" Text="Дата" /></th>
                        <th colspan="2"></th>
                    </thead>
                    <tbody>
                        <asp:PlaceHolder runat="server" ID="itemPlaceHolder" />
                    </tbody>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr <%# Container.DataItemIndex % 2 == 0?"class=\"odd\"":"" %>>
                    <td><%#Eval("BugId") %></td>
                    <td><%# Server.HtmlEncode(Eval("Tester.Name").ToString()) %>&nbsp;
                        <%# Server.HtmlEncode(Eval("Tester.Surname").ToString()) %></td>
                    <td ><%# Data.EnumStringValue.StringValue(
                             (Data.Priority)Enum.Parse(typeof(Data.Priority),
                             Eval("Priority").ToString())) %></td>
                    <td><%#Eval("CreationDate") %></td>
                    <td><asp:LinkButton runat="server" ID="btnEdit" CommandName="EditBug"
                        CommandArgument='<%#Eval("BugId") %>' Text="Промени"/></td>
                    <td><asp:LinkButton runat="server" ID="btnDelete" OnClientClick="return Confirm();"
                        CommandName="DeleteBug" CommandArgument='<%#Eval("BugId") %>' Text="Изтрий"/></td>
               </tr>
            </ItemTemplate>
            <EmptyDataTemplate>
                <p>Няма открити грешки за избраният проект</p>
            </EmptyDataTemplate>
        </asp:ListView>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<div class="pager">
    <asp:LinkButton runat="server" ID="btnPrev" CommandName="Prev" OnCommand="btnPrev_Command" Text="Предишна" />
    <asp:Label runat="server" ID="lblCurrentPage" />
    <asp:LinkButton runat="server" ID="btnNext" CommandName="Next" OnCommand="btnNext_Command" Text="Следваща" />
</div>
<ul class="buttons">
<li><asp:LinkButton runat="server" ID="btnAdd" OnClick="btnAdd_Click" Text="Добави грешка"/></li>
</ul>
<script type="text/javascript">
    function Confirm() {
        return confirm("Сигурни ли сте че искате да изтриете грешката ?");
    }
</script>
</asp:Content>
