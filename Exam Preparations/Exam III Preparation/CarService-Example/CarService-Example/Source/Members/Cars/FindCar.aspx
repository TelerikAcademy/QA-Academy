<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="FindCar.aspx.cs" Inherits="Members_Cars_FindCar" %>

<asp:Content ID="findCarTitle" ContentPlaceHolderID="pageTitle" Runat="Server">Car Service - Find a Car</asp:Content>

<asp:Content ID="findCarBody" ContentPlaceHolderID="pageBody" Runat="Server">
    <h2>
	    Find a Car</h2>
    <p>
	    <asp:TextBox ID="TextBoxCarVin" runat="server" Width="847px"></asp:TextBox>
        <asp:Button ID="ButtonSearch" runat="server" onclick="ButtonSearch_Click" 
            Text="Search" />
    </p>
    <asp:Panel ID="PanelResults" runat="server">
        <asp:GridView ID="GridViewCars" runat="server" AllowPaging="True" 
            AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="AutomobileId" 
            DataSourceID="SqlDataSourceCars">
            <Columns>
                <asp:BoundField DataField="AutomobileId" HeaderText="AutomobileId" 
                    InsertVisible="False" ReadOnly="True" SortExpression="AutomobileId" />
                <asp:BoundField DataField="Vin" HeaderText="Vin" SortExpression="Vin" />
                <asp:BoundField DataField="Make" HeaderText="Make" SortExpression="Make" />
                <asp:BoundField DataField="Model" HeaderText="Model" SortExpression="Model" />
                <asp:BoundField DataField="MakeYear" HeaderText="MakeYear" 
                    SortExpression="MakeYear" />
                <asp:BoundField DataField="ChassisNumber" HeaderText="ChassisNumber" 
                    SortExpression="ChassisNumber" />
                <asp:BoundField DataField="EngineNumber" HeaderText="EngineNumber" 
                    SortExpression="EngineNumber" />
                <asp:BoundField DataField="Owner" HeaderText="Owner" SortExpression="Owner" />
            </Columns>
            <EmptyDataTemplate>
                No cars found.
            </EmptyDataTemplate>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSourceCars" runat="server" 
            ConnectionString="<%$ ConnectionStrings:ApplicationServices %>">
        </asp:SqlDataSource>
    </asp:Panel>

</asp:Content>
