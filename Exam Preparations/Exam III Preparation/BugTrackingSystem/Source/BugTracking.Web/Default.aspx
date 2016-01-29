<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BugTrackingSystem.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <link rel="stylesheet" type="text/css" href="~/style/style.css" />
  <link rel="stylesheet" type="text/css" href="~/style/colour.css" />
  <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="main">
    <div id="links">
      <a href="#"></a>
    </div>
    <div id="logo"><h1>Система за следене на грешки</h1></div>
    <div id="content"> 
      <div id="column2">
      <br/>

         <h2>
	        Welcome to our Bug Tracking System!
        </h2>
        <h2>Users</h2>
        <p>
            test / test
        </p>
        <h2>Administrators</h2>
        <p>
	        testAdmin / testAdmin
        </p>
           <asp:Login runat="server" ID="Login" OnLoggedIn="Login_LoggedIn" >
           <LayoutTemplate>
               <table cellpadding="1" cellspacing="0" style="border-collapse:collapse;">
                   <tr>
                       <td>
                           <table cellpadding="0">
                               <tr>
                                   <td align="center" colspan="2">Вход</td>
                               </tr>
                               <tr>
                                   <td align="right">
                                       <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Потребителско име:</asp:Label>
                                   </td>
                                   <td>
                                       <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                                       <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" 
                                           ControlToValidate="UserName" ErrorMessage="Потребителското име е задължително." 
                                           ToolTip="Потребителското име е задължително." ValidationGroup="Login">*</asp:RequiredFieldValidator>
                                   </td>
                               </tr>
                               <tr>
                                   <td align="right">
                                       <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Парола:</asp:Label>
                                   </td>
                                   <td>
                                       <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                                       <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" 
                                           ControlToValidate="Password" ErrorMessage="Паролата е задължителна." 
                                           ToolTip="Паролата е задължителна." ValidationGroup="Login">*</asp:RequiredFieldValidator>
                                   </td>
                               </tr>
                               <tr>
                                   <td colspan="2">
                                       <div class="check">
                                       <asp:CheckBox ID="RememberMe" runat="server" Text="Запомни ме."/>
                                       </div>
                                   </td>
                               </tr>
                               <tr>
                                   <td align="center" colspan="2" style="color:Red;">
                                       <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                   </td>
                               </tr>
                               <tr>
                                   <td align="right" colspan="2">
                                       <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Вход" 
                                           ValidationGroup="Login" style="width:50px" />
                                   </td>
                               </tr>
                           </table>
                       </td>
                   </tr>
               </table>
           </LayoutTemplate>
          </asp:Login>
          <br/><br/><br/><br/><br/><br/><br/><br/><br/><br/>
      </div>
    </div>
    <div id="footer">
        Система за следене на грешки  <%=DateTime.Now.Year.ToString() %>
    </div>
  </div>
    </form>
</body>
</html>
