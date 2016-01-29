<%@ Page Title="" Language="C#" MasterPageFile="~/Administrator/Administrator.Master" AutoEventWireup="true" CodeBehind="Testers.aspx.cs" Inherits="BugTrackingSystem.Administrator.Testers" %>
<asp:Content ID="headContent" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="mainContent" ContentPlaceHolderID="mainContent" runat="server">
<h1>Тестери</h1>
<div class="form">
<asp:ListView runat="server" ID="lvTesters" ItemPlaceholderID="itemPlaceHolder"
 OnItemCommand="lvTesters_ItemCommand">
    <LayoutTemplate>
        <table>
            <thead>
              <th><asp:LinkButton runat="server" ID="btnName" CommandName="SortName" 
                CommandArgument="ASC" Text="Име" /></th>
              <th><asp:LinkButton runat="server" ID="btnSurname" CommandName="SortSurname" 
                CommandArgument="ASC" Text="Фамилия" /></th>
              <th colspan="2"></th>
            </thead>
            <tbody>
                <asp:PlaceHolder runat="server" ID="itemPlaceHolder" />
            </tbody>
        </table>
    </LayoutTemplate>
    <ItemTemplate>
        <tr <%# Container.DataItemIndex % 2 == 0?"class=\"odd\"":"" %>>
            <td><%# Server.HtmlEncode(Eval("Name").ToString())%> </td>
            <td><%# Server.HtmlEncode(Eval("Surname").ToString())%> </td>
            <td><asp:LinkButton runat="server" ID="btnEdit" CommandName="EditTester" 
                CommandArgument='<%#Eval("TesterId") %>' Text="Промени"/></td>
            <td><asp:LinkButton runat="server" ID="btnDelete" OnClientClick="return Confirm();"
                CommandName="DeleteTester" CommandArgument='<%#Eval("TesterId") %>' Text="Изтрий"/></td>
        </tr>
    </ItemTemplate>
    <EmptyDataTemplate>
        <p>Няма регистрирани тестери в системата</p>
    </EmptyDataTemplate>
</asp:ListView>
</div>
<div class="pager">
    <asp:LinkButton runat="server" ID="btnPrev" CommandName="Prev" OnCommand="btnPrev_Command" Text="Предишна" />
    <asp:Label runat="server" ID="lblCurrentPage" />
    <asp:LinkButton runat="server" ID="btnNext" CommandName="Next" OnCommand="btnNext_Command" Text="Следваща" />
</div>
<ul class="buttons">
<li><asp:LinkButton runat="server" ID="btnAdd" OnClick="btnAdd_Click" Text="Добави нов тестер"/></li>
</ul>
<script type="text/javascript">
    function Confirm() {
        return confirm("Сигурни ли сте че искате да изтриете тестерa ?");
    }
</script>
</asp:Content>
