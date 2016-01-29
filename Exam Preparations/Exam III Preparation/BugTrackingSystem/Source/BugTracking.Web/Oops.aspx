<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Oops.aspx.cs" Inherits="BugTrackingSystem.Oops" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
      <div id="column1">
      </div>
      <div id="column2">
       <p>Възника грешка при изпълнение на вътрешен процес, за което Ви молим за извинение</p>
       <br/><br/><br/><br/><br/><br/><br/><br/><br/>
       <br/><br/><br/><br/><br/><br/><br/><br/><br/>
      </div>
    </div>
    <div id="footer">
        Система за следене на грешки  <%=DateTime.Now.Year.ToString() %>
    </div>
  </div>
    </form>
</body>
</html>
