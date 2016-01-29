<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Cars.aspx.cs" Inherits="presentation.MembersCars" %>

<asp:Content ID="carsTitle" runat="server" ContentPlaceHolderID="pageTitle">Car Service - Cars</asp:Content>

<asp:Content ID="carsBody" runat="server" ContentPlaceHolderID="pageBody">
    <h2>
	    Cars Management
    </h2>
    <p>
        <asp:HyperLink ID="addCar" NavigateUrl="~/Members/Cars/AddCar.aspx" runat="server">Add car</asp:HyperLink>	 	
    </p>
    <p>
        <asp:GridView ID="automobilesGrid" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false"
            CssClass="nicetable" runat="server" OnRowCreated="CarsGridView_RowCreated" OnRowEditing="EditAutomobileEventHandler_RowEditing"
            OnPageIndexChanging="AutomobilesGridView_PageIndexChanging" OnSorting="CarsGridView_Sorting">
            <Columns>
                <asp:BoundField HeaderText="Id" DataField="AutomobileId" SortExpression="AutomobileId" />
                <asp:BoundField HeaderText="Vin" DataField="Vin" SortExpression="Vin" />
                <asp:BoundField HeaderText="Chassis" DataField="ChassisNumber" SortExpression="ChassisNumber" />
                <asp:BoundField HeaderText="Make" DataField="Make" SortExpression="Make" />
                <asp:BoundField HeaderText="Model" DataField="Model" SortExpression="Model" />
                <asp:BoundField HeaderText="Owner" DataField="Owner" SortExpression="Owner" />
                <asp:CommandField ShowEditButton="true" />
            </Columns>
        </asp:GridView>				        
    </p>
</asp:Content>
