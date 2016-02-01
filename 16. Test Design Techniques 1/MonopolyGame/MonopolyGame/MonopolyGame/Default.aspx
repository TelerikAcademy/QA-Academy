<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Default" %>

<!DOCTYPE html>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Monopoly Game</title>
    <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
    <style type="text/css">
        h1
        {
            color: #363f45;
            margin-left: 24%;
            margin-top: 4%;
            margin-bottom: -0.3%;
        }
        body
        {
            background: url('Data/images/qfp-bgr.png') 0 -38px no-repeat #21242c;
        }
        .demo
        {
            background: url('Data/images/qsf-telerik-logo.png') no-repeat 0 0 transparent;
            display: block;
            width: 400px;
            height: 45px;
            position: absolute;
            top: 30px;
            left: 20px;
        }
        .tab
        {
            color: #fff;
            font-size: 14px;
            font-family: Arial Baltic;
            padding-top: 15px;
        }
        .indented
        {
            padding-left: 20px;
            font-size: 15px;
        }
        #jailCase
        {
            font-size: 12px;
        }
        .conditions
        {
            font-weight: bold;
            font-size: 17px;
        }
        .RadInput
        {
            margin-bottom: 8px !important;
        }
        #wrapper
        {
            position: absolute;
            left: 20%;
            margin-left: 4%;
            padding-left: 1%;
            padding-top: 1%;
            background-color: #313a40;
        }
        tr td:first-child
        {
            width: 200px;
            vertical-align: top;
        }
        h3
        {
            color: #686868;
            margin-top: 8px;
            position: absolute;
            left: 230px;
            z-index: 1;
            text-transform: uppercase;
        }
        #toolbox
        {
            float: left;
            width: 10%;
            position: absolute;
            font-family: 'Segoe UI';
            background-color: transparent;
            padding-bottom: 1em;
        }
        #toolbox .item
        {
            margin-left: 1em;
            width: inherit;
            margin-bottom: 0;
        }
        #toolbox h4
        {
            margin-left: 13%;
            text-transform: uppercase;
            color: #fff;
            border: 0;
            font-weight: bold;
        }
        #diagramContainer
        {
            height: auto;
            
        }
        .item
        {
            font-family: 'Segoe UI';
            cursor: pointer;
        }
        #description
        {
            float: right;
            width: 70%;
        }
        #description h4
        {
            color: #fff;
            text-align: center;
        }
        #content
        {
            height: 160px;
        }
    </style>
    <link href="Data/Style/Styles.css" rel="stylesheet" type="text/css" />
    <script src="Data/Script/DiceScripts.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <script type="text/javascript">
        function pageLoad() {
            var sum = 0;
            $('#wrapper div').each(function () { sum += $(this).width(); });
            $('img').on('dragstart', function (event) { event.preventDefault(); });
            $.fn.rotatable = function () {
                var allowDrag = false;
                var $target = $("img#monopoly");
                var offset = {
                    x: $target.offset().left + ($target.width() / 2),
                    y: $target.offset().top + ($target.height() / 2)
                };

                var MouseDown = function (e) {
                    allowDrag = true;
                };
                var MouseUp = function () {
                    allowDrag = false;
                };
                var MouseMove = function (e) {
                    if (allowDrag) {
                        var mouse_x = e.pageX - offset.x;
                        var mouse_y = e.pageY - offset.y;
                        var radians = Math.atan2(mouse_x, mouse_y);
                        var degree = (radians * (180 / Math.PI) * -1) + 90;
                        $target.rotate(degree);
                    }
                };

                $target.on("mousedown", MouseDown);
                $(document).on("mouseup", MouseUp);
                $(document).on("mousemove", MouseMove);
            };
            $(".draggable").rotatable();

            $telerik.$("#toolbox").kendoDraggable({
                filter: "div.item",
                hint: function (draggable) {
                    var hint = draggable.clone(true);

                    return hint;
                }
            });
        }
        function OnValueChanged(sender, args) {
            if (sender.get_value() == 3) {
                alert("Exception thrown!");
            }
        }
        function OnClientTabSelected(sender, args) {
            if (sender.get_selectedTab().get_text() == "Go To JAIL") {
                document.getElementById("monopoly").src = "Data/images/MonopolyJail.jpg";
            }
            else {
                document.getElementById("monopoly").src = "Data/images/Monopoly3.jpg";
            }
        }
        function diagram_load(diagram, args) {
            var element = diagram.get_element();

            $telerik.$(element).kendoDropTarget({
                drop: function (e) {
                    var draggable = e.draggable,
                              element = e.dropTarget,
                              diagram = element.getKendoDiagram();

                    if (draggable && draggable.hint) {
                        var item = draggable.hint.data("data"),
                                   offset = draggable.hintOffset,
                                   point = new kendo.dataviz.diagram.Point(offset.left, offset.top),
                                   transformed = diagram.documentToModel(point);
                        item.x = transformed.x;
                        item.y = transformed.y;
                        diagram.addShape(item);
                    }
                }
            });

        }
        function getDiagram() { return $find("<%= MonopolyDiagram.ClientID %>").kendoWidget; }
    </script>
    <div id="logo">
        <span title="Telerik UI for ASP.NET AJAX" class="demo"></span>
    </div>
    <h1>
        MONOPOLY GAME</h1>
    <div id="wrapper">
        <div style="float: right; width: 32%; height: 50%; margin-right: 18%;">
            <div id="target" class="draggable">
                <img src="Data/images/Monopoly3.jpg" id="monopoly" alt="monopoly" width="100%" height="100%" />
            </div>
        </div>
        <div style="float: left; width: 46%;">

            <telerik:RadTabStrip ID="RadTabStrip1" runat="server" Width="100%" MultiPageID="RadMultiPage1"
                OnClientTabSelected="OnClientTabSelected">
                <Tabs>
                    <telerik:RadTab runat="server" Text="Basic Rules" Selected="true">
                    </telerik:RadTab>
                    <telerik:RadTab runat="server" Text="Settings">
                    </telerik:RadTab>
                    <telerik:RadTab runat="server" Text="Go To JAIL">
                    </telerik:RadTab>
                    <telerik:RadTab runat="server" Text="Use Cases">
                    </telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
            <telerik:RadMultiPage runat="server" ID="RadMultiPage1">
                <telerik:RadPageView ID="RadPageView1" runat="server" TabIndex="0" CssClass="tab"
                    Selected="true">
                    <div class="conditions">
                        Preconditions:</div>
                    <div class="indented">
                        Monopoly game could be played by minimum 2 people and maximum 6.
                        <br />
                        Each player is given between $1200 and $1500.
                        <br />
                        All remaining MONOPOLY money(total $12000) and other MONOPOLY equipment go to the
                        Bank.</div>
                    <div class="conditions">
                        Starting the game:</div>
                    <div class="indented">
                        Each player throws the dice and only after throwing 6 they start to play.<br />
                        Each time a player's MONOPOLY token lands on or passes over "GO", whether by throw
                        of the dice or by drawing a card, the Banker pays him $200 salary.<br />
                        <br />
                        Whenever a player lands on an unowned MONOPOLY property he may buy that property
                        from the Bank at its printed price. When a player lands on either of the <strong>"Chance
                            and Community Chest cards"</strong> on the MONOPOLY game board, he takes the
                        top card from the deck indicated.
                        <br />
                        <strong>Chance cards</strong>: Advance to GO, Get out of jail free, Go to Jail,
                        Go back 3 spaces, Pay tax of $15, You won $100.
                        <br />
                        <strong>Community cards</strong>: Bank error in your favor: collect $75, Pay hospital
                        fee of $100, Receive $25 consultancy fee, Inherit $50, Pay school fees of $70<br />
                        <br />
                        For rules about Jail, go to the third tab.
                    </div>
                </telerik:RadPageView>
                <telerik:RadPageView ID="RadPageView4" runat="server" TabIndex="1" CssClass="tab">
                            <table runat="server" style="border: none" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <span class="conditions">Player's count (2 to 6):</span>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox runat="server" Type="Number" ID="PlayerCount" MinValue="2"
                                            Value="2" MaxValue="6" OnTextChanged="OnTextChanged" AutoPostBack="true">
                                            <NumberFormat DecimalDigits="0" KeepNotRoundedValue="true" />
                                            <ClientEvents OnValueChanged="OnValueChanged" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span class="conditions">Names of the players:</span>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="PlayerName1" runat="server" AutoPostBack="true" CausesValidation="true"
                                            ToolTip="PlayerName1" ValidationGroup="Group1">
                                        </telerik:RadTextBox>
                                        <telerik:RadTextBox ID="PlayerName2" runat="server" AutoPostBack="true" CausesValidation="true"
                                            ToolTip="PlayerName2" ValidationGroup="Group1">
                                        </telerik:RadTextBox>
                                        <telerik:RadTextBox ID="PlayerName3" runat="server" AutoPostBack="true" CausesValidation="true"
                                            ToolTip="PlayerName2" ValidationGroup="Group1" Visible="false">
                                        </telerik:RadTextBox>
                                        <telerik:RadTextBox ID="PlayerName4" runat="server" AutoPostBack="true" CausesValidation="true"
                                            ToolTip="PlayerName4" ValidationGroup="Group1" Visible="false">
                                        </telerik:RadTextBox>
                                        <telerik:RadTextBox ID="PlayerName5" runat="server" AutoPostBack="true" CausesValidation="true"
                                            ToolTip="PlayerName5" ValidationGroup="Group1" Visible="false">
                                        </telerik:RadTextBox>
                                        <telerik:RadTextBox ID="PlayerName6" runat="server" AutoPostBack="true" CausesValidation="true"
                                            ToolTip="PlayerName6" ValidationGroup="Group1" Visible="false">
                                        </telerik:RadTextBox>
                                        <br />
                                        <asp:Label ID="ValidationResult" runat="server" />
                                        <asp:CustomValidator runat="server" ID="nameValidator" Display="Dynamic" ValidationGroup="Group1"
                                            ErrorMessage="Invalid!" OnServerValidate="ValidateTextBoxes" ControlToValidate="PlayerName1"></asp:CustomValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span class="conditions">Money for a player:</span>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="MoneyPerPlayer" runat="server" AutoPostBack="true"
                                            Type="Currency" OnTextChanged="PlayerMoneyChanged" MinValue="1200" MaxValue="1500">
                                        </telerik:RadNumericTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span class="conditions">Money in the bank:</span>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="BankMoney" runat="server" Type="Currency" ReadOnly="true">
                                        </telerik:RadNumericTextBox>
                                    </td>
                                </tr>
                            </table>
                            <hr />
                    <span class="conditions">Throw six to start the game!</span>
                    <div class="dice">
                        <div class="cube">
                            <div class="side front">
                                <div class="point pxcenter pycenter one red">
                                </div>
                            </div>
                            <div class="side top">
                                <div class="point ptop pleft">
                                </div>
                                <div class="point ptop pright">
                                </div>
                                <div class="point pbottom pleft">
                                </div>
                                <div class="point pbottom pright">
                                </div>
                            </div>
                            <div class="side left">
                                <div class="point ptop pright">
                                </div>
                                <div class="point pbottom pleft">
                                </div>
                            </div>
                            <div class="side right">
                                <div class="point ptop pleft">
                                </div>
                                <div class="point ptop pright">
                                </div>
                                <div class="point pbottom pleft">
                                </div>
                                <div class="point pbottom pright">
                                </div>
                                <div class="point pxcenter pycenter">
                                </div>
                            </div>
                            <div class="side bottom">
                                <div class="red point ptop pright">
                                </div>
                                <div class="red point pbottom pleft">
                                </div>
                                <div class="red point pxcenter pycenter">
                                </div>
                            </div>
                            <div class="side back">
                                <div class="point ptop pleft">
                                </div>
                                <div class="point ptop pright">
                                </div>
                                <div class="point pycenter pleft">
                                </div>
                                <div class="point pycenter pright">
                                </div>
                                <div class="point pbottom pleft">
                                </div>
                                <div class="point pbottom pright">
                                </div>
                            </div>
                        </div>
                    </div>
                    <telerik:RadNotification runat="server" Position="Center" Text="You are in!!!" VisibleOnPageLoad="false"
                        Title="Start the game" Height="100px" Width="200px" ID="StartGame">
                    </telerik:RadNotification>
                    <div class="btn roll">
                        roll</div>
                    <br />
                    <br />
                    <span class="conditions">Your task:</span><br />
                    <ul>
                        <li>Test the form with Boudary Value Analysis, Equivalence Partitioning. </li>
                        <li>If you find bugs, create a Word file with the bug reports.</li><li>Create a test
                            suite with automated tests for this form.It's up to you whether to use Telerik Testing Framework, Test Studio or other tool</li></ul>
                </telerik:RadPageView>
                <telerik:RadPageView ID="RadPageView2" runat="server" TabIndex="2" CssClass="tab">
                    <span class="conditions">You land in Jail when...</span>
                    <ul>
                        <li>Your MONOPOLY token lands on the space marked "Go to Jail";</li>
                        <li>You draw a card marked "Go to Jail"</li>
                        <li>You throw doubles three times in succession<br />
                            <span id="jailCase">*You may just land on the space "Jail" in the ordinary course of
                                play - then you incur no penalty and move ahead in the usual manner.</span>
                        </li>
                    </ul>
                    <span class="conditions">You get out of Jail by...</span>
                    <ul>
                        <li>Throwing doubles on any of your next three turns</li>
                        <li>Using the "Get Out of Jail Free" card if you have it</li>
                        <li>Purchasing the "Get Out of Jail Free" card from another player and playing it</li>
                        <li>Paying a fine of $50 before you roll the dice on either of your next two turns.</li>
                    </ul>
                    <span class="conditions">Being sent in Jail?!</span>
                    <ul>
                        <li>You cannot collect your $200 salary in that move even if your token passes over
                            "GO" on the way to Jail</li>
                        <li>Your turn ends when sent to Jail</li>
                        <li>You must pay $50 fine if you don't throw doubled by the third turn</li>
                        <li>You may buy and sell MONOPOLY properties, buy and sell houses, hotels and collect
                            rents</li>
                    </ul>
                    <span class="conditions">Your task: </span>
                    <br />
                    <ul>
                        <li>Define the Equivalence partition classes for "Go To JAIL". Create a short list in a Word file.</li>
                        <li>Imagine that your MONOPOLY token is at:<br />
                            <ol>
                                <li>Lecester street </li>
                                <li>Bond street</li>
                            </ol>
                            Create test cases based on the dice roll in the Word file. You could combine test cases that are
                            alike.</li>
                    </ul>
                </telerik:RadPageView>
                <telerik:RadPageView ID="RadPageView3" runat="server" TabIndex="3">
                    <div id="content">
                        <div id="toolbox">
                            <h4>
                                Options</h4>
                            <telerik:RadListView ID="ShapesList" runat="server">
                                <ItemTemplate>
                                    <div class="item" data-data='{"id":"<%# DataBinder.Eval(Container.DataItem, "ID") %>","content":{"text": "<%# DataBinder.Eval(Container.DataItem, "Text") %>", "color": "#fff"}, "background": "<%# DataBinder.Eval(Container.DataItem, "Background") %>"}'>
                                        <svg width="120" height="110">
                              <g transform="scale(1,1)">
                                   <path stroke="gray" stroke-dasharray="" stroke-width="0" fill="<%# DataBinder.Eval(Container.DataItem, "Background") %>" d="M20.5,0.5 L54.5,0.5 L54.5,0.5 L55,0.5 C65.5,0.5 74.5,9 74.5,20 C74.5,30 65,39.5 55,39.5 L54.5,39.5 L54.5,39.5 L20.5,39.5 L20.5,39.5 L20,39.5 C9,39.5 0.5,30 0.5,20 C0.5,9 9,0.5 20,0.5 L20.5,0.5 z"/>
                                   <text stroke="none" x="36" y="20" fill="#fff" text-anchor="middle" font-variant="normal" font-size="15" font-weight="normal" dominant-baseline="central"><%# DataBinder.Eval(Container.DataItem, "Text") %></text>
                              </g>
                         </svg>
                                    </div>
                                </ItemTemplate>
                            </telerik:RadListView>
                        </div>
                        <div id="description">
                            <h4>
                                Imagine that you are a player about to take a turn. Create diagram shapes to show
                                all of the options you have. Drag a shape from the Options section to the diagram. Create a screen shot of the diagram and save it as *.jpg</h4>
                        </div>
                    </div>
                    <div id="diagramContainer">
                        <telerik:RadDiagram runat="server" ID="MonopolyDiagram" ZoomRate="1">
                            <ShapeDefaultsSettings Path="M20.5,0.5 L54.5,0.5 L54.5,0.5 L55,0.5 C65.5,0.5 74.5,9 74.5,20 C74.5,30 65,39.5 55,39.5 L54.5,39.5 L54.5,39.5 L20.5,39.5 L20.5,39.5 L20,39.5 C9,39.5 0.5,30 0.5,20 C0.5,9 9,0.5 20,0.5 L20.5,0.5 z"
                                Width="50" Height="50">
                                <ConnectorsCollection>
                                   <telerik:DiagramShapeConnector Name="Bottom" />
                                    <telerik:DiagramShapeConnector Name="Left" />
                                    <telerik:DiagramShapeConnector Name="Right" />
                                    <telerik:DiagramShapeConnector Name="Top" />
                                    <telerik:DiagramShapeConnector Name="Center" />

                                </ConnectorsCollection>
                            </ShapeDefaultsSettings>
                            <ShapesCollection>
                                <telerik:DiagramShape Id="Player" Background="#8db310" Width="70" Height="70" Y="100"
                                    X="20" Path="M0.5,37.5 L37.5,0.5 L74.5,37.5 M0.5,37.5 L74.5,37.5 L37.5,74.5 z">
                                    <ContentSettings Text="Player" Color="#fff" />
                                </telerik:DiagramShape>
                            </ShapesCollection>
                            <ClientEvents OnLoad="diagram_load" />
                        </telerik:RadDiagram>
                    </div>
                </telerik:RadPageView>
            </telerik:RadMultiPage>
        </div>
    </div>
    </form>
</body>
</html>
