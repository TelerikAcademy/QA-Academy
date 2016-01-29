<%@ Page Title="" Language="C#" MasterPageFile="~/Administrator/Administrator.Master" AutoEventWireup="true" CodeBehind="Inquiries.aspx.cs" Inherits="BugTrackingSystem.Administrator.Inquiries" %>
<asp:Content ID="headContent" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="mainContent" ContentPlaceHolderID="mainContent" runat="server">
<h1>Справки</h1>
 <a href="javascript:ShowHideArea('testers')">Всички тестери</a>&nbsp;
 <a href="javascript:ShowHideArea('bugs')">Всички грешки</a>&nbsp;
 <a href="javascript:ShowHideArea('dropdown');Bind('<%=btnProjects.ClientID %>');">Грешки</a>
 
 <br />
 <br />
<asp:ScriptManager runat="server" ID="scriptManager"></asp:ScriptManager>
<div id="testers" class="form" style="display:none">
<asp:UpdatePanel runat="server" ID="updTesters" ChildrenAsTriggers="true">
<Triggers>
    <asp:AsyncPostBackTrigger ControlID="btnTesters" EventName="Click" />
</Triggers>
<ContentTemplate>
<asp:ListView runat="server" ID="lvTesters" ItemPlaceholderID="itemPlaceHolder"
    OnItemCommand="lvTesters_ItemCommand">
    <LayoutTemplate>
        <table>
            <thead>
                <th><asp:LinkButton runat="server" ID="btnName" CommandName="SortName" CommandArgument="ASC" 
                    Text="Име"></asp:LinkButton></th>
                <th><asp:LinkButton runat="server" ID="btnProjects" CommandName="SortProjectsParticipating"
                    CommandArgument="ASC" Text="Проекти"></asp:LinkButton></th>
                <th><asp:LinkButton runat="server" ID="btnFoundBugs" CommandName="SortFoundBugs"
                    CommandArgument="ASC" Text="Открити грешки"></asp:LinkButton></th>
                <th><asp:LinkButton runat="server" ID="btnLastActivity" CommandName="SortLastActivity"
                    CommandArgument="ASC" Text="Последна активност"></asp:LinkButton></th>
                <th><asp:LinkButton runat="server" ID="btnLastAction" CommandName="SortLastAction"
                    CommandArgument="ASC" Text="Последно действие"></asp:LinkButton></th>
            </thead>
            <tbody>
                <asp:PlaceHolder runat="server" ID="itemPlaceHolder" />
            </tbody>
        </table>
    </LayoutTemplate>
    <ItemTemplate>
        <tr <%# Container.DataItemIndex % 2 == 0?"class=\"odd\"":"" %>>
        <td><%# SafeEval("Name") %></td>
        <td><%# SafeEval("ProjectsParticipating")%></td>
        <td><%# SafeEval("FoundBugs")%></td>
        <td><%# SafeEval("LastActivity")%></td>
        <td><%# SafeEval("LastAction")%></td>
        </tr>
    </ItemTemplate>
    <EmptyDataTemplate>
        <p>Няма регистрирани тестери</p>
    </EmptyDataTemplate>
</asp:ListView>
      <div class="pager">
         <asp:LinkButton runat="server" ID="btnTestersPrev" CommandName="Prev" 
            OnCommand="btnTestersPrev_Command" Text="Предишна" />
         <asp:Label runat="server" ID="lblTestersCurrentPage" />
         <asp:LinkButton runat="server" ID="btnTestersNext" CommandName="Next"
            OnCommand="btnTestersNext_Command" Text="Следваща" />
     </div>
</ContentTemplate>
</asp:UpdatePanel>
<br />
</div>

<div id="dropdown" style="display:none">
    <asp:DropDownList runat="server" ID="ddlProjects" OnSelectedIndexChanged="ddlProjects_SelectedIndexChanged"
        AutoPostBack="true"/>
    <asp:Label Text="" runat="server" ID="lblBugsCount" />
<br />
<br />
</div>

<div id="bugs" style="display:none">
<asp:UpdatePanel runat="server" ID="updBugs" ChildrenAsTriggers="true">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnBugs" EventName="Click" />
    </Triggers>
    <ContentTemplate>
    <div class="form">
    <asp:ListView runat="server" ID="lvBugs" ItemPlaceholderID="itemPlaceHolder"
        OnItemCommand="lvBugs_ItemCommand">
    <LayoutTemplate>
        <table>
            <thead>
                <th><asp:LinkButton runat="server" ID="btnDescription"
                     CommandName="SortDescription" CommandArgument="ASC"
                         Text="Кратко описание"></asp:LinkButton></th>
                <th><asp:LinkButton runat="server" ID="btnPriority"
                     CommandName="SortPriority" CommandArgument="ASC" Text="Приоритет"></asp:LinkButton></th>
                <th><asp:LinkButton runat="server" ID="btnOwner"
                     CommandName="SortOwner" CommandArgument="ASC" Text="Собственик"></asp:LinkButton></th>
                <th><asp:LinkButton runat="server" ID="btnProject"
                     CommandName="SortProject" CommandArgument="ASC" Text="Проект"></asp:LinkButton></th>
                <th><asp:LinkButton runat="server" ID="btnStatus"
                     CommandName="SortStatus" CommandArgument="ASC" Text="Статус"></asp:LinkButton></th>
            </thead>
            <tbody>
                <asp:PlaceHolder runat="server" ID="itemPlaceHolder" />
            </tbody>
         </table>
     </LayoutTemplate>
         <ItemTemplate>
            <tr <%# Container.DataItemIndex % 2 == 0?"class=\"odd\"":"" %>>
            <td><%# Data.Utilities.ShortDescription(SafeEval("Description").ToString()) %></td>
            <td ><%# Data.EnumStringValue.StringValue(
                     (Data.Priority)Enum.Parse(typeof(Data.Priority),
                     Eval("Priority").ToString())) %></td>
            <td><%# SafeEval("Tester.Name") %>&nbsp;<%# SafeEval("Tester.Surname") %></td>
            <td><%# SafeEval("Project.Name") %></td>
            <td><%# Data.EnumStringValue.StringValue(
                     (Data.Status)Enum.Parse(typeof(Data.Status),
                     Eval("Status").ToString())) %></td>
            </tr>
          </ItemTemplate>
          <EmptyDataTemplate>
            <p>Няма активни грешки</p>
          </EmptyDataTemplate>
     </asp:ListView>
     </div>
     <div class="pager">
         <asp:LinkButton runat="server" ID="btnPrev" CommandName="Prev" OnCommand="btnPrev_Command" Text="Предишна" />
         <asp:Label runat="server" ID="lblCurrentPage" />
         <asp:LinkButton runat="server" ID="btnNext" CommandName="Next" OnCommand="btnNext_Command" Text="Следваща" />
     </div>
    </ContentTemplate>
</asp:UpdatePanel>
</div>
<asp:LinkButton runat="server" ID="btnTesters" OnClick="btnTesters_Click" style="display:none"></asp:LinkButton>
<asp:LinkButton runat="server" ID="btnBugs" OnClick="btnBugs_Click" style="display:none"></asp:LinkButton>
<asp:LinkButton runat="server" ID="btnProjects" OnClick="btnProjects_Click" style="display:none"></asp:LinkButton>
<asp:HiddenField runat="server" ID="hdnTab" />
<asp:HiddenField runat="server" ID="hdnFirstPage" />
<script type="text/javascript">
    function ShowHideArea(areaId) {
        document.getElementById('testers').style.display = 'none';
        document.getElementById('dropdown').style.display = 'none';
        document.getElementById('bugs').style.display = 'none';
        if (areaId == 'testers') {
            document.getElementById('testers').style.display = '';
            document.getElementById('<%=hdnTab.ClientID %>').value = "testers";
            document.getElementById('<%=hdnFirstPage.ClientID %>').value = "1";
            Bind('<%=btnTesters.ClientID %>');
        }
        else if (areaId == 'dropdown') {
            document.getElementById('dropdown').style.display = '';
            document.getElementById('<%=hdnTab.ClientID %>').value = "projectBugs";
            document.getElementById('<%=hdnFirstPage.ClientID %>').value = "1";
        }
        else {
            document.getElementById('bugs').style.display = '';
            document.getElementById('<%=hdnTab.ClientID %>').value = "bugs";
            document.getElementById('<%=hdnFirstPage.ClientID %>').value = "1";
            Bind('<%=btnBugs.ClientID %>');
        }
    }

    function ShowArea(areaId) {
        document.getElementById(areaId).style.display = '';
    }

    function Bind(buttonId) {
        var button = document.getElementById(buttonId);
        eval(button.href);
    }
</script>
</asp:Content>
