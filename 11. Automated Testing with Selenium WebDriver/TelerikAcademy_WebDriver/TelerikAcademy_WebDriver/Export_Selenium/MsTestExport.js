var doc = '';
var newLine = "\n";
var tab = "\t";

this.configForm = '<description>Variable for Selenium instance</description>' +
                  '<textbox id="options_receiver" value="this.Browser"/>' +
                  '<description>MethodName</description>' +
                  '<textbox id="options_methodName" value="TESTMETHODNAME" />' +
                  '<description>Base URL</description>' +
                  '<textbox id="options_baseUrl" value="http://home.telerik.com"/>';

this.name = "C# Anton (Driver)";
this.testcaseExtension = ".cs";
this.suiteExtension = ".cs";
this.driver = true;

this.options = {
    receiver: "this.Browser",
    methodName: "TestMethodName",
    baseUrl: "http://www.telerik.com/",
    header: 'public void TestMethod()\n{\n',
    footer:
    '}\n' + newLine + newLine,
    defaultExtension: "cs"
};

function parse(testCase, source) {
    doc = source;

    var commands = [];
    var sep = {
        comma: ",",
        tab: "\t"
    };
    var count = 0;
    while (doc.length > 0) {
        var line = /(.*)(\r\n|[\r\n])?/.exec(doc);
        count++;
        var array = line[1].split(sep);
        if (array.length >= 3) {
            var command = new Command();
            command.command = array[0];
            command.target = array[1];
            command.value = array[2];
            commands.push(command);
        }
        doc = doc.substr(line[0].length);

    }
    testCase.setCommands(commands);
}

function format(testCase, name) {
    return formatCommands(testCase.commands);
}

function formatCommands(commands) {
    var result = '';
    result += this.options['header'];

    for (var i = 0; i < commands.length; i++) {
        var command = commands[i];
        if (command.type == 'command') {
            var currentResult = '';
            currentResult = setCommand(command);
            result += currentResult;
        }
    }
    result += this.options['footer'];

    return result;
}

setCommand = function (command) {
    var waitForStart = tab + "this.CurrentElement = this.GetElement((";
    var waitForEnd = "));" + newLine + tab;
    var result = tab;

    switch (command.command.toString()) {
        case 'open':
            if (command.target.toString().substring(0, 4) == "http") {
                result += options['receiver'] + 'Navigate().GoToUrl("' + command.target.toString() + '");';
            }
            else if (command.target.toString() == "/") {
                result += options['receiver'] + 'Navigate().GoToUrl(baseUrl);';
            }
            else if (isStoredVariable(command.target)) {
                result += options['receiver'] + 'Navigate().GoToUrl(' + getStoredVariable(command.target) + ');';
            }
            else {
                result += options['receiver'] + 'Navigate().GoToUrl(baseUrl + "' + command.target.toString() + '");';
            }
            break;
        case 'clickAndWait':
            result = waitForStart + searchContext(command.target.toString()) + waitForEnd;
            result += "this.CurrentElement.Click();";
            break;
        case 'click':
            result = waitForStart + searchContext(command.target.toString()) + waitForEnd;
            result += "this.CurrentElement.Click();";
            break;
        case 'type':
            result = waitForStart + searchContext(command.target.toString()) + waitForEnd;
            result += "this.CurrentElement.Clear();";
            result += newLine + tab;
            if (isStoredVariable(command.target)) {
                result += "this.CurrentElement.SendKeys(\"" + getStoredVariable(command.value) + "\");";
            }
            else {
                result += "this.CurrentElement.SendKeys(\"" + command.value + "\");";
            }
            break;
        case 'assertTextPresent':        
            result = getWaitForText(command);
            break;
        case 'verifyTextPresent':
            result = getWaitForText(command);
            break;
        case 'verifyText':
            result = getWaitForText(command);
            break;
        case 'assertText':
            result = getWaitForText(command);
            break;
        case 'waitForElementPresent':

            result = tab + 'this.WaitForElementPresent(' + searchContext(command.target.toString()) + ');';

            break;
        case 'verifyElementPresent':

            result = tab + 'this.WaitForElementPresent(' + searchContext(command.target.toString()) + ');';

            break;
        case 'verifyElementNotPresent':

            result = tab + 'this.WaitForElementNotPresent(' + searchContext(command.target.toString()) + ');';

            break;
        case 'waitForElementNotPresent':

            result = tab + 'this.WaitForElementNotPresent(' + searchContext(command.target.toString()) + ');';

            break;
        case 'waitForTextPresent':

            result = tab + 'this.WaitForTextPresent(\"' + command.target.toString() + '\");';

            break;
        case 'waitForTextNotPresent':

            result = tab + 'this.WaitForTextNotPresent(\"' + command.target.toString() + '\");';

            break;
        case 'verifyTextPresent':

            result = tab + 'this.WaitForTextPresent(\"' + command.target.toString() + '\");';

            break;
        case 'verifyTextNotPresent':

            result = tab + 'this.WaitForTextNotPresent(\"' + command.target.toString() + '\");';

            break;
        case 'waitForText':

            result = tab + 'this.WaitForText(' + searchContext(command.target.toString()) + ',\"' + command.value + '\");';

            break;
        case 'waitForChecked':
            result = tab + 'this.WaitForChecked(' + searchContext(command.target.toString()) + ');';
            break;
        case 'waitForNotChecked':
            result = tab + 'this.WaitForNotChecked(' + searchContext(command.target.toString()) + ');';
            break;
        case 'store':
            result = tab + "IJavaScriptExecutor js = (IJavaScriptExecutor) this.Driver;" + newLine;
            result += tab + 'string ' + command.value + '= ' + 'js.ExecuteScript(\"' + command.target + '\");';
            break;
        case 'storeEval':
            result = "JavascriptExecutor js = (JavascriptExecutor) this.Driver;" + newLine;
            result = tab + 'string ' + command.value + '= ' + 'js.executeScript(\"' + command.target + '\");';
            break;
        case 'storeValue':
            result = waitForStart + searchContext(command.target.toString()) + waitForEnd;
            result += 'string ' + command.value + '= ' + 'this.CurrentElement.GetAttribute("value");';
            break;
        case 'storeAttribute':
            result = waitForStart + searchContext(command.target.toString()) + waitForEnd;
            result += 'string ' + command.value + '= ' + 'this.CurrentElement.GetAttribute(\"' + getTargetAttribute(command.target) + '\");';
            break;
        case 'gotoIf':
            var strTarget = '';
            var byTarget = '';
            if (command.target.startsWith('selenium.isElementPresent')) {
                strTarget = getIfTargetElementIsPresent(command.target);
                byTarget = searchContext(strTarget);
                result = tab + 'if(' + '!IsElementPresent(' + byTarget + '))' + tab;
                result += '{' + newLine + tab;
            }
            else if (command.target.startsWith('selenium.isElementNotPresent')) {
                strTarget = getIfTargetElementIsNotPresent(command.target);
                byTarget = searchContext(strTarget);
                result = tab + 'if(' + 'IsElementPresent(' + byTarget + ')' + tab;
                result += tab + '{' + newLine + tab;
            }
            break;
        case 'selectFrame':
            result = options['receiver'] + 'SwitchTo().Frame(\"' + command.target + '\");';
            break;
        case 'label':
            result = tab + '}';
            break;
        case 'pause':
            result = tab + 'Thread.Sleep(' + command.target + ');';
            break;
        case 'echo':
            if (isStoredVariable(command.target)) {
                result = tab + 'Console.WriteLine(\"' + getStoredVariable(command.target) + '\");';
            }
            else {
                result = tab + 'Console.WriteLine(\"' + command.target + '\");';
            }
            break;
        case 'setTimeout':
            result = tab + 'timeOut= ' + command.target + ';';
            break;
        case 'select':
            result = waitForStart + searchContext(command.target.toString()) + waitForEnd;
            result += ' SelectElement selectElement = new SelectElement(this.WaitFor);'
             + newLine + tab + 'selectElement.SelectByValue(quantity);' + newLine + tab;
            break;
        default:
            result = '// The command: #####' + command.command + '##### is not supported in the current version of the exporter';
    }

    result += newLine;
    return result;
}

searchContext = function (locator) {
    if (locator.startsWith('xpath')) {
        return 'By.XPath("' + locator.substring('xpath='.length) + '")';
    }
    else if (locator.startsWith('//')) {
        return 'By.XPath("' + locator + '")';
    }
    else if (locator.startsWith('css')) {
        return 'By.CssSelector("' + locator.substring('css='.length) + '")';
    }
    else if (locator.startsWith('link')) {

        return 'By.LinkText("' + locator.substring('link='.length) + '")';
    }
    else if (locator.startsWith('name')) {

        return 'By.Name("' + locator.substring('name='.length) + '")';
    }
    else if (locator.startsWith('tag_name')) {

        return 'By.TagName("' + locator.substring('tag_name='.length) + '")';
    }
    else if (locator.startsWith('partialID')) {

        return 'By.XPath("' + "//*[contains(@id,'" + locator.substring('partialID='.length) + "')]" + '")';
    }
    else if (locator.startsWith('id')) {

        return 'By.Id("' + locator.substring('id='.length) + '")';
    }
    else {
        return 'By.Id("' + locator + '")';
    }
};

function isStoredVariable(commandValue) {

    return commandValue.substring(0, 1) == "$";
}

function getStoredVariable(commandValue) {

    return commandValue.substring(2, (commandValue.length - 1));
}

function getIfTargetElementIsPresent(commandValue) {

    return commandValue.substring(27, (commandValue.length - 2));
}

function getIfTargetElementIsNotPresent(commandValue) {

    return commandValue.substring(30, (commandValue.length - 2));
}

function getTargetAttribute(commandValue) {

    return commandValue.substring(commandValue.indexOf('@') + 1);
}

function getWaitForText(command) {
    var result = '';
    if (command.value.toString() == "") {
        result = tab + 'this.WaitForText(\"' + command.target + '\");';
    }
    else {
        result = tab + 'this.WaitForText(' + searchContext(command.target.toString()) + ',\"' + command.value + '\");';
    }

    return result;
}
