var gotoLabels= {};
var whileLabels = {};

// overload the original Selenium reset function
Selenium.prototype.reset = function() {
    // reset the labels
    this.initialiseLabels();
    // proceed with original reset code
    this.defaultTimeout = Selenium.DEFAULT_TIMEOUT;
    this.browserbot.selectWindow("null");
    this.browserbot.resetPopups();
}


/*
This toolkit is the work of Andrey Yegorov & Darren DeRidder of the Ottawa office of Kindsight

Dave Hunt packaged it into a FireFox plugin

The ForEach stuff at the bottom below the separator is a contribution by Martin H. Bramwell, 2011/11
*/

var forEachLabels = {}; // mhb:20111107

/*
* --- Initialize Conditional Elements --- *
* Run through the script collecting line numbers of all conditional elements
* There are three a results arrays: goto labels, while pairs and forEach pairs
*
*/
Selenium.prototype.initialiseLabels = function()
{
    gotoLabels = {};
    whileLabels = { ends: {}, whiles: {} };
    forEachLabels = { forends: {}, fors: {} }; // mhb:20111107
    var command_rows = [];
    var numCommands = testCase.commands.length;
    for (var i = 0; i < numCommands; ++i) {
        var x = testCase.commands[i];
        command_rows.push(x);
    }
    var cycles = [];
    var forEachCmds = [];
    for( var i = 0; i < command_rows.length; i++ ) {
        if (command_rows[i].type == 'command')
        switch( command_rows[i].command.toLowerCase() ) {
            case "label":
                gotoLabels[ command_rows[i].target ] = i;
                break;
            case "while":
            case "endwhile":
                cycles.push( [command_rows[i].command.toLowerCase(), i] )
                break;
            case "storefor":
            case "endfor":
                forEachCmds.push( [command_rows[i].command.toLowerCase(), i] )
                break;
        }
    }
    var i = 0;
    while( cycles.length ) {
        if( i >= cycles.length ) {
            throw new Error( "non-matching while/endWhile found" );
        }
        switch( cycles[i][0] ) {
            case "while":
                if( ( i+1 < cycles.length ) && ( "endwhile" == cycles[i+1][0] ) ) {
                    // pair found
                    whileLabels.ends[ cycles[i+1][1] ] = cycles[i][1];
                    whileLabels.whiles[ cycles[i][1] ] = cycles[i+1][1];
                    cycles.splice( i, 2 );
                    i = 0;
                } else ++i;
                break;
            case "endwhile":
                ++i;
                break;
        }
    }

/* ---- mhb: 20111107 --- begin --- */
    var idxFE = 0;
    while( forEachCmds.length ) {
        if( idxFE >= forEachCmds.length ) {
            throw new Error( "non-matching storeFor/endFor found" );
        }
        switch( forEachCmds[idxFE][0] ) {
            case "storefor":
                if( ( idxFE+1 < forEachCmds.length ) && ("endfor" == forEachCmds[idxFE+1][0]) ) {
                    // pair found
                    forEachLabels.forends[ forEachCmds[idxFE+1][1] ] = forEachCmds[idxFE][1];
                    forEachLabels.fors[ forEachCmds[idxFE][1] ] = forEachCmds[idxFE+1][1];
                    forEachCmds.splice( idxFE, 2 );
                    idxFE = 0;
                } else ++idxFE;
                break;
            case "endfor":
                ++idxFE;
                break;
        }
    }
/* ---- mhb: 20111107 --- end --- */

}

Selenium.prototype.continueFromRow = function( row_num )
{
    if(row_num == undefined || row_num == null || row_num < 0) {
        throw new Error( "Invalid row_num specified." );
    }
    testCase.debugContext.debugIndex = row_num;
}

// do nothing. simple label
Selenium.prototype.doLabel = function(){};

Selenium.prototype.doGotolabel = function( label )
{
    if( undefined == gotoLabels[label] ) {
        throw new Error( "Specified label '" + label + "' is not found." );
    }
    this.continueFromRow( gotoLabels[ label ] );
};

Selenium.prototype.doGoto = Selenium.prototype.doGotolabel;

Selenium.prototype.doGotoIf = function( condition, label )
{
    if( eval(condition) ) this.doGotolabel( label );
}

Selenium.prototype.doWhile = function( condition )
{
    if( !eval(condition) ) {
        var last_row = testCase.debugContext.debugIndex;
        var end_while_row = whileLabels.whiles[ last_row ];
        if( undefined == end_while_row ) throw new Error( "Corresponding 'endWhile' is not found." );
        this.continueFromRow( end_while_row );
    }
}

Selenium.prototype.doEndWhile = function()
{
    var last_row = testCase.debugContext.debugIndex;
    var while_row = whileLabels.ends[ last_row ] - 1;
    if( undefined == while_row ) throw new Error( "Corresponding 'While' is not found." );
    this.continueFromRow( while_row );
}

/* ------- ForEach looping Contributed by Martin "Hasan" Bramwell 2011/11/07 ------- */

/*
* Note. I'm neither a JavaScript nor a Selenium pro.
* There are surely some nice fix-ups that a real expert would spot in a minute.
* If you have an idea of a better way please let me know.
* Some 'nice-to-have' ideas :
* -- a 'For' command instead of a 'storeFor' command
* -- nested ForEach
* --
*/

var iteratorsMap = {};
var collectionsMap = {};

Selenium.prototype.getFor = function( _nameCollection )
{

if( eval(iteratorsMap[_nameCollection].hasNext()) ) {
return iteratorsMap[_nameCollection].next();
} else {
var last_foreach_row = testCase.debugContext.debugIndex;
var end_for_row = forEachLabels.fors[ last_foreach_row ];
if( undefined == end_for_row ) throw new Error( "Corresponding 'endFor' could not be found." );
this.continueFromRow( end_for_row );
}
}

Selenium.prototype.doEndFor = function()
{
var last_foreach_row = testCase.debugContext.debugIndex;
var for_row = forEachLabels.forends[ last_foreach_row ] - 1;
if( undefined == for_row ) throw new Error( "Corresponding 'storeFor' could not be found." );
this.continueFromRow( for_row );
}




Selenium.prototype.doAddToCollection = function(_nameCollection, _valueToStore) {
var initialized = true;

if ((iteratorsMap == undefined) || (collectionsMap == undefined)) initialized = false;

if (collectionsMap[_nameCollection] == undefined) initialized = false;

if (!initialized) throw new Error( "You must first call '|addCollection | " + _nameCollection + "||' in order to initialize it." );

collectionsMap[_nameCollection].add(_valueToStore);
iteratorsMap[_nameCollection] = collectionsMap[_nameCollection].iterator();

};


Selenium.prototype.doAddCollection = function(_nameCollection) {

if (iteratorsMap == undefined) {
iteratorsMap = new Array();
}

if (collectionsMap == undefined) {
collectionsMap = new Array();
}

collectionsMap[_nameCollection] = new Collection(new Array());

};

function Iterator(_collection)
{
var privateCollection = _collection;
var total = privateCollection.length;
var cursor = 0;

function doNext() {
return privateCollection[cursor++];
}

function doHasNext() { return cursor < total; }
function doRemainderCount() { return total - cursor; }

this.next = doNext;
this.hasNext = doHasNext;
this.remainderCount = doRemainderCount;

}


function Collection(_collection)
{
var privateCollection = _collection;

function doSize() { return privateCollection.length; }
this.size = doSize;

function doIterator() {
return new Iterator(privateCollection);
}

function doAdd(_value) {
privateCollection[privateCollection.length] = _value;
}

this.iterator = doIterator;
this.add = doAdd;

}

//Timer extension

var globalTime = new Object();
Selenium.prototype.doTimerStart = function(target) {
 var dt1 = new Date();
 if (target == null || target == "")
 {
  LOG.info ("Target not present so timer was not started");
 } else {
  globalTime[target] = dt1;
 }
 delete dt1;
};
Selenium.prototype.doTimerStop = function(target) {
 var dt = new Date();
 if (target == null || target == "")
 {
  LOG.info ("Please specify a target");
 } else if (globalTime [target] == null) {
  LOG.info ("Start time was not called for " + target);
 } else {
   LOG.info ("Time Passed for " + target + " : " + Math.floor (dt - globalTime[target]));
  delete globalTime[target]; }
 delete dt;
};

Selenium.prototype.doDisplayCurrentPageLoadTime = function(target) {
 var dt = new Date();
 if (target == null || target == "")
 {
  LOG.info ("Please specify a target");
 } else if (globalTime [target] == null) {
  LOG.info ("Start time was not called for " + target);
 } else {
	  var location = this.page().getCurrentWindow().location;  
   LOG.info ("URL: " + location + " " + Math.floor (dt - globalTime[target]));
  delete globalTime[target]; }
 delete dt;
};

Selenium.prototype.doTypeTelerikControls = function (locator, value) { 

var element = this.page().findElement(locator); 
var script = "$find('" + element.getAttribute('id') + "').set_value('" + value + "')"; 

try { 
this.browserbot.getCurrentWindow().eval(script); 
} 
catch (e) { 
throw new SeleniumError("Threw an exception: " + e.message); 
} 

};

//
// Locates an element by a partial match on id
//
PageBot.prototype.locateElementByPartialId = function(text, inDocument) {
    return this.locateElementByXPath("//*[contains(./@id, 'Z')][1]".replace(/Z/,text), inDocument);
};


//
// Custom type for Telerik Rad Text Box Controls 
//
Selenium.prototype.doTypeTelerikRadTextBox = function (locator, value) {

    var element = this.page().findElement(locator);
    var script = "$find('" + element.getAttribute('id') + "').set_value('" + value + "')";
    
    try {
        this.browserbot.getCurrentWindow().eval(script);
    }
    catch (e) {
        throw new SeleniumError("Threw an exception: " + e.message);
    }

};


//
// Custom type for Telerik Rad Date Picker Controls 
//Or use these commands:
// $find('ctl05_adminControlStatus_adminControlClosedDateField_dfClosedDate_rdpFieldDate_dateInput').set_selectedDate(new Date('2012-03-27'));
//<tr>
//	<td>runScript</td>
//	<td>$find('rwOpportunityWizard').get_contentFrame().contentWindow.$find('ctl05_adminControlStatus_adminControlClosedDateField_dfClosedDate_rdpFieldDate_dateInput').set_selectedDate(new Date('2011-10-07'));</td>
//	<td>2011-10-07</td>
//</tr>

Selenium.prototype.doTypeTelerikRadDatePicker = function (locator, value) {

    var element = this.page().findElement(locator);
    var script = "$find('" + element.getAttribute('id') + "').set_selectedDate(new Date('"+ value + "'));";
    
    try 
	{
        this.browserbot.getCurrentWindow().eval(script);
    }
    catch (e) 
	{
        throw new SeleniumError("Threw an exception: " + e.message);
    }

};

//
// Custom type for Telerik Rad Numeric text Box Controls 
//
Selenium.prototype.doTypeTelerikRadNumericTextBox = function (locator, value) {
	var iFrame = "rwOpportunityWizard";
    var element = this.page().findElement(locator);
    var script ="$find('" + element.getAttribute('id') + "').set_value('" + value + "')";
    
    try 
	{
        this.browserbot.getCurrentWindow().eval(script);
    }
    catch (e) 
	{
        throw new SeleniumError("Threw an exception: " + e.message);
    }

};