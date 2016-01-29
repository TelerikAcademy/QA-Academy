<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Users.aspx.cs" Inherits="presentation.AdminUsers" %>

<asp:Content ID="carServiceTitle" runat="server" ContentPlaceHolderID="pageTitle">Car Service - Users</asp:Content>

<asp:Content ID="usersBody" runat="server" ContentPlaceHolderID="pageBody">
    <h2>User management</h2>
    <p>
	    <asp:HyperLink NavigateUrl="~/Admin/Users/AddUser.aspx" runat="server">Add user</asp:HyperLink>			
    </p>
    <p>
        <asp:GridView ID="carServiceUsers" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false" 
            CssClass="nicetable" runat="server" OnRowCreated="CarServiceUsersGridView_RowCreated" 
            OnRowEditing="EditUserEventHandler_RowEditing" OnRowDeleting="DeactivateUserEventHandler_RowDeliting"
            OnPageIndexChanging="UsersGridView_PageIndexChanging" OnSorting="UsersGridView_Sorting">
            <Columns>
                <asp:BoundField HeaderText="User Name" DataField="UserName" SortExpression="UserName" />
                <asp:BoundField HeaderText="Email" DataField="Email" SortExpression="Email" />
                <asp:BoundField HeaderText="First Name" DataField="FirstName" SortExpression="FirstName" />
                <asp:BoundField HeaderText="Last Name" DataField="LastName" SortExpression="LastName" />
                <asp:BoundField HeaderText="Active" DataField="IsActive" SortExpression="IsActive" />
                <asp:CommandField ShowEditButton="true" />
                <asp:CommandField ShowDeleteButton="true" />
            </Columns>
        </asp:GridView>
    </p>
</asp:Content>