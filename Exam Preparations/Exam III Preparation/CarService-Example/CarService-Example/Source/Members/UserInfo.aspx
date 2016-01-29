<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="UserInfo.aspx.cs" Inherits="Members_UserInfo" %>

<asp:Content ID="ContentTitle" ContentPlaceHolderID="pageTitle" Runat="Server">User Info</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="pageBody" Runat="Server">
    <h1>User Information</h1>
    <ul>
        <li>User name: <asp:Literal Mode="Encode" runat="server" ID="LiteralUserName" /></li>
        <li>EMail: <asp:Literal Mode="Encode" runat="server" ID="LiteralEmail" /></li>
        <li>
            Repair cards:
            <asp:GridView runat="server" ID="GridViewRepairCards" AllowPaging="True" 
                AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="CardId" 
                DataSourceID="EntityDataSourceRepairCards" PageSize="5">
                <Columns>
                    <asp:BoundField DataField="CardId" HeaderText="CardId" ReadOnly="True" 
                        SortExpression="CardId" />
                    <asp:TemplateField HeaderText="Car Vin" SortExpression="Automobile.Vin">
                        <ItemTemplate>
                            <asp:Literal Mode="Encode" ID="LabelCarVin" runat="server" 
                                Text='<%# Bind("Automobile.Vin") %>'></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="StartRepair" HeaderText="StartRepair" 
                        SortExpression="StartRepair" />
                    <asp:BoundField DataField="FinishRepair" HeaderText="FinishRepair" 
                        SortExpression="FinishRepair" />
                    <asp:BoundField DataField="Description" HeaderText="Description" 
                        SortExpression="Description" />
                    <asp:BoundField DataField="CardPrice" HeaderText="CardPrice" 
                        SortExpression="CardPrice" />
                </Columns>
            </asp:GridView>
            <asp:EntityDataSource ID="EntityDataSourceRepairCards" runat="server" 
                ConnectionString="name=Entities" DefaultContainerName="Entities" 
                EnableFlattening="False" EntitySetName="RepairCards" Include="Automobile"
                Where = "it.UserId = @userId">
                <WhereParameters>
                    <asp:Parameter Name="userId" DbType="Guid" />
                </WhereParameters>
            </asp:EntityDataSource>
        </li>
    </ul>
</asp:Content>

