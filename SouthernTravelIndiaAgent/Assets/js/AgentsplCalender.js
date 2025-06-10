// JScript File

/* Internal HTML code start here */
var stObj;
this.today = new Date();			
var thisMonth = this.today.getMonth();
var thisDay = this.today.getDate();
var thisYear = this.today.getFullYear();
var mnth;
var dday;
if(thisMonth+1 < 10)
mnth = "0" + parseInt(thisMonth+1);
else
mnth = thisMonth+1
if(thisDay < 10)
dday = "0" + parseInt(thisDay);
else
dday = thisDay;
var objDate='';

// get x and y postion for calender x 

function findPosX(obj)
  {
    var curleft = 0;
    if(obj.offsetParent)
        while(1) 
        {
          curleft += obj.offsetLeft;
          if(!obj.offsetParent)
            break;
          obj = obj.offsetParent;
        }
    else if(obj.x)
        curleft += obj.x;
    return curleft;
  }

  function findPosY(obj)
  {
    var curtop = 0;
    if(obj.offsetParent)
        while(1)
        {
          curtop += obj.offsetTop;
          if(!obj.offsetParent)
            break;
          obj = obj.offsetParent;
        }
    else if(obj.y)
        curtop += obj.y;
    return curtop;
  }

  //end


function objCal(calIcon,FID) 
		{
				
				var objX=findPosX(document.getElementById(calIcon));
				var objY=findPosY(document.getElementById(calIcon));
				stObj.outerContainer.style.top = objY + "px";
			    stObj.outerContainer.style.left = objX+20+ "px";
			    var _targetDiv = document.getElementById('STContainer');
	            var _targetFrame = document.getElementById('framediv');
	            _targetDiv.style.width = 'auto';
	            _targetDiv.style.display = 'inline';           
   	            _targetFrame.style.display = 'inline';
   	            _targetFrame.style.width = stObj.outerContainer.offsetWidth;
   	            _targetFrame.style.height = stObj.outerContainer.offsetHeight;
			    _targetFrame.style.top = stObj.outerContainer.offsetTop+280;
    	        _targetFrame.style.left = stObj.outerContainer.offsetLeft-10;
	            stObj.outerContainer.style.zIndex = _targetFrame.style.zIndex +1;
			    stObj.outerContainer.style.display='block';
				objDate=FID;
		    }


		function setDate() 
		{
				
				var date = stObj.getSelectedDates()[0];
			    var mon = date.getMonth()+1;
			    var day = date.getDate();
			    if(mon < 10)
			        mon = "0" + mon;			
			    if(day < 10)
			        day = "0" + day;
			    document.getElementById(objDate).value=(day)+ "/" + mon +"/" + date.getFullYear();
			    stObj.hide();		
			    document.getElementById('framediv').style.display = "none";
				checkDate(document.getElementById(objDate).value);
				
		}


		  		
var SOUTHERN = function() {
    return {
        util: {},
        caldoy: {},
        example: {},
        namespace: function( sNameSpace ) {
            if (!sNameSpace || !sNameSpace.length) {
                return null;
            }
            var levels = sNameSpace.split(".");
            var currentNS = SOUTHERN;
            for (var i=(levels[0] == "SOUTHERN") ? 1 : 0; i<levels.length; ++i) {
                currentNS[levels[i]] = currentNS[levels[i]] || {};
                currentNS = currentNS[levels[i]];
            }

            return currentNS;

        }
    };

} ();
SOUTHERN.util.CustomEvent=function(type,oScope){this.type=type;this.scope=oScope||window;this.subscribers=[];if(SOUTHERN.util["Event"]){SOUTHERN.util.Event.regCE(this);}};SOUTHERN.util.CustomEvent.prototype={subscribe:function(fn,obj,bOverride){this.subscribers.push(new SOUTHERN.util.Subscriber(fn,obj,bOverride));},unsubscribe:function(fn,obj){var found=false;for(var i=0;i<this.subscribers.length;++i){var s=this.subscribers[i];if(s&&s.contains(fn,obj)){this._delete(i);found=true;}}
return found;},fire:function(){for(var i=0;i<this.subscribers.length;++i){var s=this.subscribers[i];if(s){var scope=(s.override)?s.obj:this.scope;s.fn.call(scope,this.type,arguments,s.obj);}}},unsubscribeAll:function(){for(var i=0;i<this.subscribers.length;++i){this._delete(i);}},_delete:function(index){var s=this.subscribers[index];if(s){delete s.fn;delete s.obj;}
delete this.subscribers[index];}};SOUTHERN.util.Subscriber=function(fn,obj,bOverride){this.fn=fn;this.obj=obj||null;this.override=(bOverride);};SOUTHERN.util.Subscriber.prototype.contains=function(fn,obj){return(this.fn==fn&&this.obj==obj);};if(!SOUTHERN.util.Event){SOUTHERN.util.Event=function(){var loadComplete=false;var listeners=[];var delayedListeners=[];var unloadListeners=[];var customEvents=[];var legacyEvents=[];var legacyHandlers=[];return{EL:0,TYPE:1,FN:2,WFN:3,SCOPE:3,ADJ_SCOPE:4,isSafari:(navigator.userAgent.match(/safari/gi)),isIE:(!this.isSafari&&navigator.userAgent.match(/msie/gi)),addListener:function(el,sType,fn,oScope,bOverride){if(this._isValidCollection(el)){var ok=true;for(var i=0;i<el.length;++i){ok=(this.on(el[i],sType,fn,oScope,bOverride)&&ok);}
return ok;}else if(typeof el=="string"){if(loadComplete){el=this.getEl(el);}else{delayedListeners[delayedListeners.length]=[el,sType,fn,oScope,bOverride];return true;}}
if(!el){return false;}
if("unload"==sType&&oScope!==this){unloadListeners[unloadListeners.length]=[el,sType,fn,oScope,bOverride];return true;}
var scope=(bOverride)?oScope:el;var wrappedFn=function(e){return fn.call(scope,SOUTHERN.util.Event.getEvent(e),oScope);};var li=[el,sType,fn,wrappedFn,scope];var index=listeners.length;listeners[index]=li;if(this.useLegacyEvent(el,sType)){var legacyIndex=this.getLegacyIndex(el,sType);if(legacyIndex==-1){legacyIndex=legacyEvents.length;legacyEvents[legacyIndex]=[el,sType,el["on"+sType]];legacyHandlers[legacyIndex]=[];el["on"+sType]=function(e){SOUTHERN.util.Event.fireLegacyEvent(SOUTHERN.util.Event.getEvent(e),legacyIndex);};}
legacyHandlers[legacyIndex].push(index);}else if(el.addEventListener){el.addEventListener(sType,wrappedFn,false);}else if(el.attachEvent){el.attachEvent("on"+sType,wrappedFn);}
return true;},fireLegacyEvent:function(e,legacyIndex){var ok=true;var le=legacyHandlers[legacyIndex];for(i=0;i<le.length;++i){var index=le[i];if(index){var li=listeners[index];var scope=li[this.ADJ_SCOPE];var ret=li[this.WFN].call(scope,e);ok=(ok&&ret);}}
return ok;},getLegacyIndex:function(el,sType){for(var i=0;i<legacyEvents.length;++i){var le=legacyEvents[i];if(le&&le[0]==el&&le[1]==sType){return i;}}
return-1;},useLegacyEvent:function(el,sType){return((!el.addEventListener&&!el.attachEvent)||(sType=="click"&&this.isSafari));},removeListener:function(el,sType,fn){if(typeof el=="string"){el=this.getEl(el);}else if(this._isValidCollection(el)){var ok=true;for(var i=0;i<el.length;++i){ok=(this.removeListener(el[i],sType,fn)&&ok);}
return ok;}
var cacheItem=null;var index=this._getCacheIndex(el,sType,fn);if(index>=0){cacheItem=listeners[index];}
if(!el||!cacheItem){return false;}
if(el.removeEventListener){el.removeEventListener(sType,cacheItem[this.WFN],false);}else if(el.detachEvent){el.detachEvent("on"+sType,cacheItem[this.WFN]);}
delete listeners[index][this.WFN];delete listeners[index][this.FN];delete listeners[index];return true;},getTarget:function(ev,resolveTextNode){var t=ev.target||ev.srcElement;if(resolveTextNode&&t&&"#text"==t.nodeName){return t.parentNode;}else{return t;}},getPageX:function(ev){var x=ev.pageX;if(!x&&0!==x){x=ev.clientX||0;if(this.isIE){x+=this._getScrollLeft();}}
return x;},getPageY:function(ev){var y=ev.pageY;if(!y&&0!==y){y=ev.clientY||0;if(this.isIE){y+=this._getScrollTop();}}
return y;},getRelatedTarget:function(ev){var t=ev.relatedTarget;if(!t){if(ev.type=="mouseout"){t=ev.toElement;}else if(ev.type=="mouseover"){t=ev.fromElement;}}
return t;},getTime:function(ev){if(!ev.time){var t=new Date().getTime();try{ev.time=t;}catch(e){return t;}}
return ev.time;},stopEvent:function(ev){this.stopPropagation(ev);this.preventDefault(ev);},stopPropagation:function(ev){if(ev.stopPropagation){ev.stopPropagation();}else{ev.cancelBubble=true;}},preventDefault:function(ev){if(ev.preventDefault){ev.preventDefault();}else{ev.returnValue=false;}},getEvent:function(e){var ev=e||window.event;if(!ev){var c=this.getEvent.caller;while(c){ev=c.arguments[0];if(ev&&Event==ev.constructor){break;}
c=c.caller;}}
return ev;},getCharCode:function(ev){return ev.charCode||(ev.type=="keypress")?ev.keyCode:0;},_getCacheIndex:function(el,sType,fn){for(var i=0;i<listeners.length;++i){var li=listeners[i];if(li&&li[this.FN]==fn&&li[this.EL]==el&&li[this.TYPE]==sType){return i;}}
return-1;},_isValidCollection:function(o){return(o&&o.length&&typeof o!="string"&&!o.tagName&&!o.alert&&typeof o[0]!="undefined");},elCache:{},getEl:function(id){return document.getElementById(id);},clearCache:function(){for(i in this.elCache){delete this.elCache[i];}},regCE:function(ce){customEvents.push(ce);},_load:function(e){loadComplete=true;},_tryPreloadAttach:function(){var tryAgain=!loadComplete;for(var i=0;i<delayedListeners.length;++i){var d=delayedListeners[i];if(d){var el=this.getEl(d[this.EL]);if(el){this.on(el,d[this.TYPE],d[this.FN],d[this.SCOPE],d[this.ADJ_SCOPE]);delete delayedListeners[i];}}}
if(tryAgain){setTimeout("SOUTHERN.util.Event._tryPreloadAttach()",50);}},_unload:function(e,me){for(var i=0;i<unloadListeners.length;++i){var l=unloadListeners[i];if(l){var scope=(l[this.ADJ_SCOPE])?l[this.SCOPE]:window;l[this.FN].call(scope,this.getEvent(e),l[this.SCOPE]);}}
if(listeners&&listeners.length>0){for(i=0;i<listeners.length;++i){l=listeners[i];if(l){this.removeListener(l[this.EL],l[this.TYPE],l[this.FN]);}}
this.clearCache();}
for(i=0;i<customEvents.length;++i){customEvents[i].unsubscribeAll();delete customEvents[i];}
for(i=0;i<legacyEvents.length;++i){delete legacyEvents[i][0];delete legacyEvents[i];}},_getScrollLeft:function(){return this._getScroll()[1];},_getScrollTop:function(){return this._getScroll()[0];},_getScroll:function(){var dd=document.documentElement;db=document.body;if(dd&&dd.scrollTop){return[dd.scrollTop,dd.scrollLeft];}else if(db){return[db.scrollTop,db.scrollLeft];}else{return[0,0];}}};}();SOUTHERN.util.Event.on=SOUTHERN.util.Event.addListener;if(document&&document.body){SOUTHERN.util.Event._load();}else{SOUTHERN.util.Event.on(window,"load",SOUTHERN.util.Event._load,SOUTHERN.util.Event,true);}
SOUTHERN.util.Event.on(window,"unload",SOUTHERN.util.Event._unload,SOUTHERN.util.Event,true);SOUTHERN.util.Event._tryPreloadAttach();}

/* event js start here */

SOUTHERN.namespace("SOUTHERN.caldoy");
SOUTHERN.caldoy.DateMath=new function()
{
this.DAY="D";
this.WEEK="W";
this.YEAR="Y";
this.MONTH="M";
this.ONE_DAY_MS=1000*60*60*24;
this.add=function(date,field,amount)
{
	var d=new Date(date.getTime());
	switch(field)
	{
		case this.MONTH:
			var newMonth=date.getMonth()+amount;
			var years=0;
			if(newMonth<0)
			{
				while(newMonth<0)
				{
					newMonth+=12;
					years-=1;
				}
			}
			else if(newMonth>11)
			{
				while(newMonth>11)
				{
					newMonth-=12;
					years+=1;
				}
			}
			d.setMonth(newMonth);
			d.setFullYear(date.getFullYear()+years);
			break;
		case this.DAY:
			d.setDate(date.getDate()+amount);
			break;
		case this.YEAR:
			d.setFullYear(date.getFullYear()+amount);
			break;
		case this.WEEK:d.setDate(date.getDate()+7);
			break;
	}
	return d;
};

this.subtract=function(date,field,amount)
{
	return this.add(date,field,(amount*-1));
};

this.before=function(date,compareTo)
{
	var ms=compareTo.getTime();
	if(date.getTime()<ms)
	{return true;}
	else{return false;}
};

this.after=function(date,compareTo)
{
	var ms=compareTo.getTime();
	if(date.getTime()>ms)
	{return true;}
	else{return false;}
};

this.getJan1=function(calendarYear)
{
	return new Date(calendarYear,0,1);
};

this.getDayOffset=function(date,calendarYear)
{	
	var beginYear=this.getJan1(calendarYear);
	var dayOffset=Math.ceil((date.getTime()-beginYear.getTime())/this.ONE_DAY_MS);
	return dayOffset;
};

this.getWeekNumber=function(date,calendarYear,weekStartsOn)
{
	if(!weekStartsOn)
	{weekStartsOn=0;}
	if(!calendarYear)
	{calendarYear=date.getFullYear();}
	
	var weekNum=-1;
	var jan1=this.getJan1(calendarYear);
	var jan1DayOfWeek=jan1.getDay();
	var month=date.getMonth();
	var day=date.getDate();
	var year=date.getFullYear();
	var dayOffset=this.getDayOffset(date,calendarYear);
	if(dayOffset<0&&dayOffset>=(-1*jan1DayOfWeek))
	{weekNum=1;}
	else
	{
		weekNum=1;var testDate=this.getJan1(calendarYear);
		while(testDate.getTime()<date.getTime()&&testDate.getFullYear()==calendarYear)
		{
			weekNum+=1;
			testDate=this.add(testDate,this.WEEK,1);
		}
	}
	return weekNum;
};

this.isYearOverlapWeek=function(weekBeginDate)
{
	var overlaps=false;
	var nextWeek=this.add(weekBeginDate,this.DAY,6);
	if(nextWeek.getFullYear()!=weekBeginDate.getFullYear())
	{
		overlaps=true;
	}
	return overlaps;
};

this.isMonthOverlapWeek=function(weekBeginDate)
{
	var overlaps=false;
	var nextWeek=this.add(weekBeginDate,this.DAY,6);
	if(nextWeek.getMonth()!=weekBeginDate.getMonth())
	{
		overlaps=true;
	}
	return overlaps;
};

this.findMonthStart=function(date)
{
	var start=new Date(date.getFullYear(),date.getMonth(),1);
	return start;
};

this.findMonthEnd=function(date)
{
	var start=this.findMonthStart(date);
	var nextMonth=this.add(start,this.MONTH,1);
	var end=this.subtract(nextMonth,this.DAY,1);
	return end;
};

this.clearTime=function(date){date.setHours(0,0,0,0);return date;};}



SOUTHERN.namespace("SOUTHERN.caldoy");
SOUTHERN.caldoy.Calendar_Core=function(id,containerId,monthyear,selected)
{
	if(arguments.length>0)
	{
		this.init(id,containerId,monthyear,selected);
	}
}


SOUTHERN.caldoy.Calendar_Core.DATE="D";
SOUTHERN.caldoy.Calendar_Core.MONTH_DAY="MD";
SOUTHERN.caldoy.Calendar_Core.WEEKDAY="WD";
SOUTHERN.caldoy.Calendar_Core.RANGE="R";
SOUTHERN.caldoy.Calendar_Core.MONTH="M";
SOUTHERN.caldoy.Calendar_Core.DISPLAY_DAYS=42;
SOUTHERN.caldoy.Calendar_Core.STOP_RENDER="S";
SOUTHERN.caldoy.Calendar_Core.prototype=
{
	Config:null,
	parent:null,
	index:-1,
	cells:null,
	weekHeaderCells:null,
	weekFooterCells:null,
	cellDates:null,
	id:null,
	oDomContainer:null,
	today:null,
	renderStack:null,
	_renderStack:null,
	pageDate:null,
	_pageDate:null,
	minDate:null,
	maxDate:null,
	selectedDates:null,
	_selectedDates:null,
	shellRendered:false,
	table:null,
	headerCell:null
};


SOUTHERN.caldoy.Calendar_Core.prototype.init=function(id,containerId,monthyear,selected)
{
	this.setupConfig();
	this.id=id;
	this.cellDates=new Array();
	this.cells=new Array();
	this.renderStack=new Array();
	this._renderStack=new Array();
	this.oDomContainer=document.getElementById(containerId);
	this.today=new Date();
	SOUTHERN.caldoy.DateMath.clearTime(this.today);
	var month;
	var year;
	if(monthyear)
	{
		var aMonthYear=monthyear.split(this.Locale.DATE_FIELD_DELIMITER);
		month=parseInt(aMonthYear[this.Locale.MY_MONTH_POSITION-1]);
		year=parseInt(aMonthYear[this.Locale.MY_YEAR_POSITION-1]);
	}
	else
	{
		month=this.today.getMonth()+1;
		year=this.today.getFullYear();
	}
	this.pageDate=new Date(year,month-1,1);
	this._pageDate=new Date(this.pageDate.getTime());
	if(selected)
	{
		this.selectedDates=this._parseDates(selected);
		this._selectedDates=this.selectedDates.concat();
	}
	else
	{
		this.selectedDates=new Array();
		this._selectedDates=new Array();
	}
	this.wireDefaultEvents();
	this.wireCustomEvents();
};

SOUTHERN.caldoy.Calendar_Core.prototype.wireDefaultEvents=function()
{
	this.doSelectCell=function(e,cal)
	{
		var cell=this;
		var index=cell.index;
		if(cal.Options.MULTI_SELECT)
		{
			var link=cell.getElementsByTagName("A")[0];
			link.blur();
			var cellDate=cal.cellDates[index];
			var cellDateIndex=cal._indexOfSelectedFieldArray(cellDate);
			if(cellDateIndex>-1)
			{
				cal.deselectCell(index);
			}
			else
			{
				cal.selectCell(index);
			}
		}
		else
		{
			var link=cell.getElementsByTagName("A")[0];
			link.blur()
			cal.selectCell(index);
		}
	}
	
	this.doCellMouseOver=function(e,cal)
	{
		SOUTHERN.caldoy.Calendar_Core.prependCssClass(this,cal.Style.CSS_CELL_HOVER);
	}
	
	this.doCellMouseOut=function(e,cal)
	{
		SOUTHERN.caldoy.Calendar_Core.removeCssClass(this,cal.Style.CSS_CELL_HOVER);
	}
	
	this.doNextMonth=function(e,cal)
	{
		cal.nextMonth();
	}
	
	this.doPreviousMonth=function(e,cal)
	{
		cal.previousMonth();
	}
}

SOUTHERN.caldoy.Calendar_Core.prototype.wireCustomEvents=function(){}

SOUTHERN.caldoy.Calendar_Core.prototype.setupConfig=function()
{
	this.Config=new Object();
	this.Config.Style=
	{
		CSS_ROW_HEADER:"calrowhead",
		CSS_ROW_FOOTER:"calrowfoot",
		CSS_CELL:"calcell",
		CSS_CELL_SELECTED:"selected",
		CSS_CELL_RESTRICTED:"restricted",
		CSS_CELL_TODAY:"today",
		CSS_CELL_OOM:"oom",
		CSS_HEADER:"calheader",
		CSS_HEADER_TEXT:"calhead",
		CSS_WEEKDAY_CELL:"calweekdaycell",
		CSS_WEEKDAY_ROW:"calweekdayrow",
		CSS_FOOTER:"calfoot",
		CSS_CALENDAR:"calendar",
		CSS_BORDER:"calbordered",
		CSS_CONTAINER:"calcontainer",
		CSS_NAV_LEFT:"calnavleft",
		CSS_NAV_RIGHT:"calnavright",
		CSS_CELL_TOP:"calcelltop",
		CSS_CELL_LEFT:"calcellleft",
		CSS_CELL_RIGHT:"calcellright",
		CSS_CELL_BOTTOM:"calcellbottom",
		CSS_CELL_HOVER:"calcellhover",
		CSS_CELL_HIGHLIGHT1:"highlight1",
		CSS_CELL_HIGHLIGHT2:"highlight2",
		CSS_CELL_HIGHLIGHT3:"highlight3",
		CSS_CELL_HIGHLIGHT4:"highlight4"
	};
	
	this.Style=this.Config.Style;
	this.Config.Locale={MONTHS_SHORT:["Jan","Feb","Mar","Apr","May","Jun","Jul","Aug","Sep","Oct","Nov","Dec"],MONTHS_LONG:["January","February","March","April","May","June","July","August","September","October","November","December"],WEEKDAYS_1CHAR:["S","M","T","W","T","F","S"],WEEKDAYS_SHORT:["Su","Mo","Tu","We","Th","Fr","Sa"],WEEKDAYS_MEDIUM:["Sun","Mon","Tue","Wed","Thu","Fri","Sat"],WEEKDAYS_LONG:["Sunday","Monday","Tuesday","Wednesday","Thursday","Friday","Saturday"],DATE_DELIMITER:",",DATE_FIELD_DELIMITER:"/",DATE_RANGE_DELIMITER:"-",MY_MONTH_POSITION:1,MY_YEAR_POSITION:2,MD_MONTH_POSITION:1,MD_DAY_POSITION:2,MDY_MONTH_POSITION:1,MDY_DAY_POSITION:2,MDY_YEAR_POSITION:3};this.Locale=this.Config.Locale;this.Config.Options={MULTI_SELECT:false,SHOW_WEEKDAYS:true,START_WEEKDAY:0,SHOW_WEEK_HEADER:false,SHOW_WEEK_FOOTER:false,HIDE_BLANK_WEEKS:false,NAV_ARROW_LEFT:"Images/STlt.gif",NAV_ARROW_RIGHT:"Images/STrt.gif"};
	this.Options=this.Config.Options;
	this.customConfig();
	if(!this.Options.LOCALE_MONTHS)
	{
		this.Options.LOCALE_MONTHS=this.Locale.MONTHS_LONG;
	}
	if(!this.Options.LOCALE_WEEKDAYS)
	{
		this.Options.LOCALE_WEEKDAYS=this.Locale.WEEKDAYS_SHORT;
	}
	if(this.Options.START_WEEKDAY>0)
	{
		for(var w=0;w<this.Options.START_WEEKDAY;++w)
		{
			this.Locale.WEEKDAYS_SHORT.push(this.Locale.WEEKDAYS_SHORT.shift());
			this.Locale.WEEKDAYS_MEDIUM.push(this.Locale.WEEKDAYS_MEDIUM.shift());
			this.Locale.WEEKDAYS_LONG.push(this.Locale.WEEKDAYS_LONG.shift());
		}
	}
};

SOUTHERN.caldoy.Calendar_Core.prototype.customConfig=function(){};

SOUTHERN.caldoy.Calendar_Core.prototype.buildMonthLabel=function()
{
	var text=this.Options.LOCALE_MONTHS[this.pageDate.getMonth()]+" "+this.pageDate.getFullYear();
	return text;
};


SOUTHERN.caldoy.Calendar_Core.prototype.buildDayLabel=function(workingDate)
{
	var day=workingDate.getDate();
	return day;
};

SOUTHERN.caldoy.Calendar_Core.prototype.buildShell=function()
{
	this.table=document.createElement("TABLE");
	this.table.cellSpacing=0;
	SOUTHERN.caldoy.Calendar_Core.setCssClasses(this.table,[this.Style.CSS_CALENDAR]);
	this.table.id=this.id;
	this.buildShellHeader();
	this.buildShellBody();
	this.buildShellFooter();
	SOUTHERN.util.Event.addListener(window,"unload",this._unload,this);
};

SOUTHERN.caldoy.Calendar_Core.prototype.buildShellHeader=function()
{
	var head=document.createElement("THEAD");
	var headRow=document.createElement("TR");
	var headerCell=document.createElement("TH");
	var colSpan=7;
	if(this.Config.Options.SHOW_WEEK_HEADER)
	{
		this.weekHeaderCells=new Array();colSpan+=1;
	}
	if(this.Config.Options.SHOW_WEEK_FOOTER)
	{
		this.weekFooterCells=new Array();colSpan+=1;
	}
	headerCell.colSpan=colSpan;
	SOUTHERN.caldoy.Calendar_Core.setCssClasses(headerCell,[this.Style.CSS_HEADER_TEXT]);
	this.headerCell=headerCell;
	headRow.appendChild(headerCell);
	head.appendChild(headRow);
	if(this.Options.SHOW_WEEKDAYS)
	{
		var row=document.createElement("TR");
		var fillerCell;
		SOUTHERN.caldoy.Calendar_Core.setCssClasses(row,[this.Style.CSS_WEEKDAY_ROW]);
		if(this.Config.Options.SHOW_WEEK_HEADER)
		{
			fillerCell=document.createElement("TH");
			SOUTHERN.caldoy.Calendar_Core.setCssClasses(fillerCell,[this.Style.CSS_WEEKDAY_CELL]);
			row.appendChild(fillerCell);
		}
		for(var i=0;i<this.Options.LOCALE_WEEKDAYS.length;++i)
		{
			var cell=document.createElement("TH");
			SOUTHERN.caldoy.Calendar_Core.setCssClasses(cell,[this.Style.CSS_WEEKDAY_CELL]);
			cell.innerHTML=this.Options.LOCALE_WEEKDAYS[i];
			row.appendChild(cell);
		}
		if(this.Config.Options.SHOW_WEEK_FOOTER)
		{
			fillerCell=document.createElement("TH");
			SOUTHERN.caldoy.Calendar_Core.setCssClasses(fillerCell,[this.Style.CSS_WEEKDAY_CELL]);
			row.appendChild(fillerCell);
		}
		head.appendChild(row);
	}
	this.table.appendChild(head);
};



SOUTHERN.caldoy.Calendar_Core.prototype.buildShellBody=function()
{
	this.tbody=document.createElement("TBODY");
	for(var r=0;r<6;++r)
	{
		var row=document.createElement("TR");
		for(var c=0;c<this.headerCell.colSpan;++c)
		{
			var cell;
			if(this.Config.Options.SHOW_WEEK_HEADER&&c===0)
			{
				cell=document.createElement("TH");
				this.weekHeaderCells[this.weekHeaderCells.length]=cell;
			}
			else if(this.Config.Options.SHOW_WEEK_FOOTER&&c==(this.headerCell.colSpan-1))
			{
				cell=document.createElement("TH");
				this.weekFooterCells[this.weekFooterCells.length]=cell;
			}
			else
			{
				cell=document.createElement("TD");
				this.cells[this.cells.length]=cell;
				SOUTHERN.caldoy.Calendar_Core.setCssClasses(cell,[this.Style.CSS_CELL]);
			}
			row.appendChild(cell);}
			this.tbody.appendChild(row);}
			this.table.appendChild(this.tbody);
		};
		SOUTHERN.caldoy.Calendar_Core.prototype.buildShellFooter=function(){};
		SOUTHERN.caldoy.Calendar_Core.prototype.renderShell=function()
		{
			this.oDomContainer.appendChild(this.table);
			this.shellRendered=true;};
			SOUTHERN.caldoy.Calendar_Core.prototype.render=function()
			{
				if(!this.shellRendered)
				{
					this.buildShell();
					this.renderShell();
				}
				this.resetRenderers();
				this.cellDates.length=0;
				var workingDate=SOUTHERN.caldoy.DateMath.findMonthStart(this.pageDate);
				this.renderHeader();
				this.renderBody(workingDate);
				this.renderFooter();
				this.onRender();
			};
			SOUTHERN.caldoy.Calendar_Core.prototype.renderHeader=function()
			{
				this.headerCell.innerHTML="";
				var headerContainer=document.createElement("DIV");
				headerContainer.className=this.Style.CSS_HEADER;
				headerContainer.appendChild(document.createTextNode(this.buildMonthLabel()));
				this.headerCell.appendChild(headerContainer);
			};
			SOUTHERN.caldoy.Calendar_Core.prototype.renderBody=function(workingDate)
			{
				this.preMonthDays=workingDate.getDay();
				if(this.Options.START_WEEKDAY>0)
				{
					this.preMonthDays-=this.Options.START_WEEKDAY;
				}
			if(this.preMonthDays<0)
			{
				this.preMonthDays+=7;
			}
			this.monthDays=SOUTHERN.caldoy.DateMath.findMonthEnd(workingDate).getDate();
			this.postMonthDays=SOUTHERN.caldoy.Calendar_Core.DISPLAY_DAYS-this.preMonthDays-this.monthDays;
			workingDate=SOUTHERN.caldoy.DateMath.subtract(workingDate,SOUTHERN.caldoy.DateMath.DAY,this.preMonthDays);
			this.table.style.visibility="hidden";
			var weekRowIndex=0;
			for(var c=0;c<this.cells.length;++c)
			{
				var cellRenderers=new Array();
				var cell=this.cells[c];
				this.clearElement(cell);
				SOUTHERN.util.Event.removeListener(cell,"click",this.doSelectCell);
				if(SOUTHERN.caldoy.Calendar_Core._getBrowser()=="ie")
				{
					SOUTHERN.util.Event.removeListener(cell,"mouseover",this.doCellMouseOver);
					SOUTHERN.util.Event.removeListener(cell,"mouseout",this.doCellMouseOut);
				}
				cell.index=c;
				cell.id=this.id+"_cell"+c;
				this.cellDates[this.cellDates.length]=[workingDate.getFullYear(),workingDate.getMonth()+1,workingDate.getDate()];
				if(workingDate.getDay()==this.Options.START_WEEKDAY)
				{
					var rowHeaderCell=null;
					var rowFooterCell=null;
					if(this.Options.SHOW_WEEK_HEADER)
					{
						rowHeaderCell=this.weekHeaderCells[weekRowIndex];
						this.clearElement(rowHeaderCell);
					}
					if(this.Options.SHOW_WEEK_FOOTER)
					{
						rowFooterCell=this.weekFooterCells[weekRowIndex];
						this.clearElement(rowFooterCell);
					}
					if(this.Options.HIDE_BLANK_WEEKS&&this.isDateOOM(workingDate)&&!SOUTHERN.caldoy.DateMath.isMonthOverlapWeek(workingDate))
					{continue;}
					else
					{
						if(rowHeaderCell)
						{
							this.renderRowHeader(workingDate,rowHeaderCell);
						}
						if(rowFooterCell)
						{
							this.renderRowFooter(workingDate,rowFooterCell);
						}
					}
				}
				var renderer=null;
				if(workingDate.getFullYear()==this.today.getFullYear()&&workingDate.getMonth()==this.today.getMonth()&&workingDate.getDate()==this.today.getDate())
				{
					cellRenderers[cellRenderers.length]=this.renderCellStyleToday;
				}
				if(this.isDateOOM(workingDate))
				{
					cellRenderers[cellRenderers.length]=this.renderCellNotThisMonth;
				}
				else
				{
					for(var r=0;r<this.renderStack.length;++r)
					{
						var rArray=this.renderStack[r];
						var type=rArray[0];
						var month;
						var day;
						var year;
						switch(type)
						{
							case SOUTHERN.caldoy.Calendar_Core.DATE:month=rArray[1][1];
							day=rArray[1][2];
							year=rArray[1][0];
							if(workingDate.getMonth()+1==month&&workingDate.getDate()==day&&workingDate.getFullYear()==year)
							{
								renderer=rArray[2];
								this.renderStack.splice(r,1);
							}
							break;
							
							case SOUTHERN.caldoy.Calendar_Core.MONTH_DAY:month=rArray[1][0];
							day=rArray[1][1];
							if(workingDate.getMonth()+1==month&&workingDate.getDate()==day)
							{
								renderer=rArray[2];
								this.renderStack.splice(r,1);
							}
							break;
							
							case SOUTHERN.caldoy.Calendar_Core.RANGE:var date1=rArray[1][0];
							var date2=rArray[1][1];
							var d1month=date1[1];
							var d1day=date1[2];
							var d1year=date1[0];
							var d1=new Date(d1year,d1month-1,d1day);
							var d2month=date2[1];
							var d2day=date2[2];
							var d2year=date2[0];
							var d2=new Date(d2year,d2month-1,d2day);
							if(workingDate.getTime()>=d1.getTime()&&workingDate.getTime()<=d2.getTime())
							{
								renderer=rArray[2];
								if(workingDate.getTime()==d2.getTime())
								{
									this.renderStack.splice(r,1);
								}
							}
							break;
							
							case SOUTHERN.caldoy.Calendar_Core.WEEKDAY:var weekday=rArray[1][0];
							if(workingDate.getDay()+1==weekday)
							{renderer=rArray[2];}
							break;
							
							case SOUTHERN.caldoy.Calendar_Core.MONTH:month=rArray[1][0];if(workingDate.getMonth()+1==month)
							{renderer=rArray[2];}
							break;
						}
						if(renderer)
						{
							cellRenderers[cellRenderers.length]=renderer;
						}
					}
				}
				if(this._indexOfSelectedFieldArray([workingDate.getFullYear(),workingDate.getMonth()+1,workingDate.getDate()])>-1)
				{cellRenderers[cellRenderers.length]=this.renderCellStyleSelected;}
				
				if(this.minDate)
				{this.minDate=SOUTHERN.caldoy.DateMath.clearTime(this.minDate);}
				
				if(this.maxDate)
				{this.maxDate=SOUTHERN.caldoy.DateMath.clearTime(this.maxDate);
			}
			if((this.minDate&&(workingDate.getTime()<this.minDate.getTime()))||(this.maxDate&&(workingDate.getTime()>this.maxDate.getTime())))
			{
				cellRenderers[cellRenderers.length]=this.renderOutOfBoundsDate;
			}
			else
			{
				cellRenderers[cellRenderers.length]=this.renderCellDefault;
			}
			for(var x=0;x<cellRenderers.length;++x)
			{
				var ren=cellRenderers[x];
				if(ren.call(this,workingDate,cell)==SOUTHERN.caldoy.Calendar_Core.STOP_RENDER){break;}
			}
			workingDate=SOUTHERN.caldoy.DateMath.add(workingDate,SOUTHERN.caldoy.DateMath.DAY,1);
			if(workingDate.getDay()==this.Options.START_WEEKDAY){weekRowIndex+=1;}
			SOUTHERN.caldoy.Calendar_Core.addCssClass(cell,this.Style.CSS_CELL);if(c>=0&&c<=6)
			{SOUTHERN.caldoy.Calendar_Core.addCssClass(cell,this.Style.CSS_CELL_TOP);}
			if((c%7)==0)
			{SOUTHERN.caldoy.Calendar_Core.addCssClass(cell,this.Style.CSS_CELL_LEFT);}
			if(((c+1)%7)==0)
			{SOUTHERN.caldoy.Calendar_Core.addCssClass(cell,this.Style.CSS_CELL_RIGHT);}
			var postDays=this.postMonthDays;if(postDays>=7&&this.Options.HIDE_BLANK_WEEKS)
			{var blankWeeks=Math.floor(postDays/7);
			for(var p=0;p<blankWeeks;++p)
			{
				postDays-=7;
			}
		}
		if(c>=((this.preMonthDays+postDays+this.monthDays)-7))
		{
			SOUTHERN.caldoy.Calendar_Core.addCssClass(cell,this.Style.CSS_CELL_BOTTOM);
		}
	}

this.table.style.visibility="visible";
};

SOUTHERN.caldoy.Calendar_Core.prototype.renderFooter=function(){};
SOUTHERN.caldoy.Calendar_Core.prototype._unload=function(e,cal)
{
	for(var c in cal.cells){c=null;}
	cal.cells=null;
	cal.tbody=null;
	cal.oDomContainer=null;
	cal.table=null;
	cal.headerCell=null;
	cal=null;
};

SOUTHERN.caldoy.Calendar_Core.prototype.renderOutOfBoundsDate=function(workingDate,cell)
{
	SOUTHERN.caldoy.Calendar_Core.addCssClass(cell,"previous");
	cell.innerHTML=workingDate.getDate();
	return SOUTHERN.caldoy.Calendar_Core.STOP_RENDER;
}

SOUTHERN.caldoy.Calendar_Core.prototype.renderRowHeader=function(workingDate,cell)
{
	SOUTHERN.caldoy.Calendar_Core.addCssClass(cell,this.Style.CSS_ROW_HEADER);
	var useYear=this.pageDate.getFullYear();
	if(!SOUTHERN.caldoy.DateMath.isYearOverlapWeek(workingDate))
	{
		useYear=workingDate.getFullYear();
	}
	var weekNum=SOUTHERN.caldoy.DateMath.getWeekNumber(workingDate,useYear,this.Options.START_WEEKDAY);
	cell.innerHTML=weekNum;
	if(this.isDateOOM(workingDate)&&!SOUTHERN.caldoy.DateMath.isMonthOverlapWeek(workingDate))
	{
		SOUTHERN.caldoy.Calendar_Core.addCssClass(cell,this.Style.CSS_CELL_OOM);
	}
};

SOUTHERN.caldoy.Calendar_Core.prototype.renderRowFooter=function(workingDate,cell)
{
	SOUTHERN.caldoy.Calendar_Core.addCssClass(cell,this.Style.CSS_ROW_FOOTER);
	if(this.isDateOOM(workingDate)&&!SOUTHERN.caldoy.DateMath.isMonthOverlapWeek(workingDate))
	{
		SOUTHERN.caldoy.Calendar_Core.addCssClass(cell,this.Style.CSS_CELL_OOM);
	}
};


SOUTHERN.caldoy.Calendar_Core.prototype.renderCellDefault=function(workingDate,cell)
{
	cell.innerHTML="";
	var link=document.createElement("a");
	link.href="javascript:void(null);";
	link.name=this.id+"__"+workingDate.getFullYear()+"_"+(workingDate.getMonth()+1)+"_"+workingDate.getDate();
	SOUTHERN.util.Event.addListener(cell,"click",this.doSelectCell,this);
	if(SOUTHERN.caldoy.Calendar_Core._getBrowser()=="ie")
	{
		SOUTHERN.util.Event.addListener(cell,"mouseover",this.doCellMouseOver,this);
		SOUTHERN.util.Event.addListener(cell,"mouseout",this.doCellMouseOut,this);
	}
	link.appendChild(document.createTextNode(this.buildDayLabel(workingDate)));
	cell.appendChild(link);
};

SOUTHERN.caldoy.Calendar_Core.prototype.renderCellStyleHighlight1=function(workingDate,cell)
{
	SOUTHERN.caldoy.Calendar_Core.addCssClass(cell,this.Style.CSS_CELL_HIGHLIGHT1);
};

SOUTHERN.caldoy.Calendar_Core.prototype.renderCellStyleHighlight2=function(workingDate,cell)
{
	SOUTHERN.caldoy.Calendar_Core.addCssClass(cell,this.Style.CSS_CELL_HIGHLIGHT2);
};

SOUTHERN.caldoy.Calendar_Core.prototype.renderCellStyleHighlight3=function(workingDate,cell)
{
	SOUTHERN.caldoy.Calendar_Core.addCssClass(cell,this.Style.CSS_CELL_HIGHLIGHT3);
};

SOUTHERN.caldoy.Calendar_Core.prototype.renderCellStyleHighlight4=function(workingDate,cell)
{SOUTHERN.caldoy.Calendar_Core.addCssClass(cell,this.Style.CSS_CELL_HIGHLIGHT4);};

SOUTHERN.caldoy.Calendar_Core.prototype.renderCellStyleToday=function(workingDate,cell)
{SOUTHERN.caldoy.Calendar_Core.addCssClass(cell,this.Style.CSS_CELL_TODAY);};

SOUTHERN.caldoy.Calendar_Core.prototype.renderCellStyleSelected=function(workingDate,cell)
{SOUTHERN.caldoy.Calendar_Core.addCssClass(cell,this.Style.CSS_CELL_SELECTED);};

SOUTHERN.caldoy.Calendar_Core.prototype.renderCellNotThisMonth=function(workingDate,cell)
{
	SOUTHERN.caldoy.Calendar_Core.addCssClass(cell,this.Style.CSS_CELL_OOM);
	cell.innerHTML=workingDate.getDate();
	return SOUTHERN.caldoy.Calendar_Core.STOP_RENDER;
};

SOUTHERN.caldoy.Calendar_Core.prototype.renderBodyCellRestricted=function(workingDate,cell)
{
	SOUTHERN.caldoy.Calendar_Core.setCssClasses(cell,[this.Style.CSS_CELL,this.Style.CSS_CELL_RESTRICTED]);
	cell.innerHTML=workingDate.getDate();
	return SOUTHERN.caldoy.Calendar_Core.STOP_RENDER;
};

SOUTHERN.caldoy.Calendar_Core.prototype.addMonths=function(count)
{
	this.pageDate=SOUTHERN.caldoy.DateMath.add(this.pageDate,SOUTHERN.caldoy.DateMath.MONTH,count);
	this.resetRenderers();
	this.onChangePage();
};


SOUTHERN.caldoy.Calendar_Core.prototype.subtractMonths=function(count)
{
	this.pageDate=SOUTHERN.caldoy.DateMath.subtract(this.pageDate,SOUTHERN.caldoy.DateMath.MONTH,count);
	this.resetRenderers();
	this.onChangePage();
};

SOUTHERN.caldoy.Calendar_Core.prototype.addYears=function(count)
{
	this.pageDate=SOUTHERN.caldoy.DateMath.add(this.pageDate,SOUTHERN.caldoy.DateMath.YEAR,count);
	this.resetRenderers();
	this.onChangePage();
};

SOUTHERN.caldoy.Calendar_Core.prototype.subtractYears=function(count)
{
	this.pageDate=SOUTHERN.caldoy.DateMath.subtract(this.pageDate,SOUTHERN.caldoy.DateMath.YEAR,count);
	this.resetRenderers();
	this.onChangePage();
};

SOUTHERN.caldoy.Calendar_Core.prototype.nextMonth=function()
{
	this.addMonths(1);
};

SOUTHERN.caldoy.Calendar_Core.prototype.previousMonth=function()
{
	this.subtractMonths(1);
};

SOUTHERN.caldoy.Calendar_Core.prototype.nextYear=function()
{
	this.addYears(1);
};

SOUTHERN.caldoy.Calendar_Core.prototype.previousYear=function()
{
	this.subtractYears(1);
};

SOUTHERN.caldoy.Calendar_Core.prototype.reset=function()
{
	this.selectedDates.length=0;
	this.selectedDates=this._selectedDates.concat();
	this.pageDate=new Date(this._pageDate.getTime());
	this.onReset();
};

SOUTHERN.caldoy.Calendar_Core.prototype.clear=function()
{
	this.selectedDates.length=0;
	this.pageDate=new Date(this.today.getTime());
	this.onClear();
};

SOUTHERN.caldoy.Calendar_Core.prototype.select=function(date)
{
	this.onBeforeSelect();
	var aToBeSelected=this._toFieldArray(date);
	for(var a=0;a<aToBeSelected.length;++a)
	{
		var toSelect=aToBeSelected[a];
		if(this._indexOfSelectedFieldArray(toSelect)==-1)
		{
			this.selectedDates[this.selectedDates.length]=toSelect;
		}
	}
	if(this.parent)
	{
		this.parent.sync(this);
	}
	this.onSelect();return this.getSelectedDates();
};

SOUTHERN.caldoy.Calendar_Core.prototype.selectCell=function(cellIndex)
{
	this.onBeforeSelect();
	this.cells=this.tbody.getElementsByTagName("TD");
	var cell=this.cells[cellIndex];
	var cellDate=this.cellDates[cellIndex];
	var dCellDate=this._toDate(cellDate);
	var selectDate=cellDate.concat();
	this.selectedDates.push(selectDate);
	if(this.parent){this.parent.sync(this);}
	this.renderCellStyleSelected(dCellDate,cell);
	this.onSelect();
	this.doCellMouseOut.call(cell,null,this);
	return this.getSelectedDates();
};

SOUTHERN.caldoy.Calendar_Core.prototype.deselect=function(date)
{
	this.onBeforeDeselect();
	var aToBeSelected=this._toFieldArray(date);
	for(var a=0;a<aToBeSelected.length;++a)
	{
		var toSelect=aToBeSelected[a];
		var index=this._indexOfSelectedFieldArray(toSelect);
		if(index!=-1)
		{
			this.selectedDates.splice(index,1);
		}
	}
	if(this.parent)
	{
		this.parent.sync(this);
	}
	this.onDeselect();return this.getSelectedDates();
};

SOUTHERN.caldoy.Calendar_Core.prototype.deselectCell=function(i)
{
	this.onBeforeDeselect();
	this.cells=this.tbody.getElementsByTagName("TD");
	var cell=this.cells[i];
	var cellDate=this.cellDates[i];
	var cellDateIndex=this._indexOfSelectedFieldArray(cellDate);
	var dCellDate=this._toDate(cellDate);
	var selectDate=cellDate.concat();
	if(cellDateIndex>-1)
	{
		if(this.pageDate.getMonth()==dCellDate.getMonth()&&this.pageDate.getFullYear()==dCellDate.getFullYear())
		{
			SOUTHERN.caldoy.Calendar_Core.removeCssClass(cell,this.Style.CSS_CELL_SELECTED);
		}
		this.selectedDates.splice(cellDateIndex,1);
	}
	if(this.parent){this.parent.sync(this);}
	this.onDeselect();return this.getSelectedDates();
};

SOUTHERN.caldoy.Calendar_Core.prototype.deselectAll=function()
{
	this.onBeforeDeselect();
	var count=this.selectedDates.length;
	this.selectedDates.length=0;
	if(this.parent){this.parent.sync(this);}
	if(count>0)
	{
		this.onDeselect();
	}
	return this.getSelectedDates();
};

SOUTHERN.caldoy.Calendar_Core.prototype._toFieldArray=function(date)
{
	var returnDate=new Array();
	if(date instanceof Date)
	{
		returnDate=[[date.getFullYear(),date.getMonth()+1,date.getDate()]];
	}
	else if(typeof date=='string')
	{
		returnDate=this._parseDates(date);
	}
	else if(date instanceof Array)
	{
		for(var i=0;i<date.length;++i)
		{
			var d=date[i];
			returnDate[returnDate.length]=[d.getFullYear(),d.getMonth()+1,d.getDate()];
		}
	}
	return returnDate;
};

SOUTHERN.caldoy.Calendar_Core.prototype._toDate=function(dateFieldArray)
{if(dateFieldArray instanceof Date)
{return dateFieldArray;}else
{return new Date(dateFieldArray[0],dateFieldArray[1]-1,dateFieldArray[2]);}};SOUTHERN.caldoy.Calendar_Core.prototype._fieldArraysAreEqual=function(array1,array2){var match=false;if(array1[0]==array2[0]&&array1[1]==array2[1]&&array1[2]==array2[2])
{match=true;}
return match;};SOUTHERN.caldoy.Calendar_Core.prototype._indexOfSelectedFieldArray=function(find){var selected=-1;for(var s=0;s<this.selectedDates.length;++s)
{var sArray=this.selectedDates[s];if(find[0]==sArray[0]&&find[1]==sArray[1]&&find[2]==sArray[2])
{selected=s;break;}}
return selected;};

SOUTHERN.caldoy.Calendar_Core.prototype.isDateOOM=function(date)
{var isOOM=false;
this.today = new Date();

if(date.getMonth()!=this.pageDate.getMonth() || (date.getDate() < today.getDate() && date.getMonth() == today.getMonth() && date.getFullYear() == today.getFullYear()))
{isOOM=true;}
if(date.getFullYear() < today.getFullYear())
{isOOM=true;}
if(date.getFullYear() == today.getFullYear() && date.getMonth() < today.getMonth())
{isOOM=true;}
if(date.getFullYear() > today.getFullYear() && date.getMonth() > today.getMonth())
{isOOM=true;}
if(date.getFullYear() > today.getFullYear() && date.getMonth() > today.getMonth())
{
  if(date.getMonth() == today.getMonth()) 
  {
     if(date.getDate() == today.getDate())
     {isOOM=true;}
  }     
}
if(date.getFullYear() > today.getFullYear() && date.getMonth() == today.getMonth())
{
  if(date.getMonth() == today.getMonth()) 
  {
     if(date.getDate() > today.getDate())
     {isOOM=true;}
  }     
}
return isOOM;};

SOUTHERN.caldoy.Calendar_Core.prototype.onBeforeSelect=function(){if(!this.Options.MULTI_SELECT){this.clearAllBodyCellStyles(this.Style.CSS_CELL_SELECTED);this.deselectAll();}};SOUTHERN.caldoy.Calendar_Core.prototype.onSelect=function(){};SOUTHERN.caldoy.Calendar_Core.prototype.onBeforeDeselect=function(){};SOUTHERN.caldoy.Calendar_Core.prototype.onDeselect=function(){};SOUTHERN.caldoy.Calendar_Core.prototype.onChangePage=function(){this.render();};SOUTHERN.caldoy.Calendar_Core.prototype.onRender=function(){};SOUTHERN.caldoy.Calendar_Core.prototype.onReset=function(){this.render();};SOUTHERN.caldoy.Calendar_Core.prototype.onClear=function(){this.render();};SOUTHERN.caldoy.Calendar_Core.prototype.validate=function(){return true;};SOUTHERN.caldoy.Calendar_Core.prototype._parseDate=function(sDate){var aDate=sDate.split(this.Locale.DATE_FIELD_DELIMITER);var rArray;if(aDate.length==2)
{rArray=[aDate[this.Locale.MD_MONTH_POSITION-1],aDate[this.Locale.MD_DAY_POSITION-1]];rArray.type=SOUTHERN.caldoy.Calendar_Core.MONTH_DAY;}else{rArray=[aDate[this.Locale.MDY_YEAR_POSITION-1],aDate[this.Locale.MDY_MONTH_POSITION-1],aDate[this.Locale.MDY_DAY_POSITION-1]];rArray.type=SOUTHERN.caldoy.Calendar_Core.DATE;}
return rArray;};SOUTHERN.caldoy.Calendar_Core.prototype._parseDates=function(sDates){var aReturn=new Array();var aDates=sDates.split(this.Locale.DATE_DELIMITER);for(var d=0;d<aDates.length;++d)
{var sDate=aDates[d];if(sDate.indexOf(this.Locale.DATE_RANGE_DELIMITER)!=-1){var aRange=sDate.split(this.Locale.DATE_RANGE_DELIMITER);var dateStart=this._parseDate(aRange[0]);var dateEnd=this._parseDate(aRange[1]);var fullRange=this._parseRange(dateStart,dateEnd);aReturn=aReturn.concat(fullRange);}else{var aDate=this._parseDate(sDate);aReturn.push(aDate);}}
return aReturn;};SOUTHERN.caldoy.Calendar_Core.prototype._parseRange=function(startDate,endDate){var dStart=new Date(startDate[0],startDate[1]-1,startDate[2]);var dCurrent=SOUTHERN.caldoy.DateMath.add(new Date(startDate[0],startDate[1]-1,startDate[2]),SOUTHERN.caldoy.DateMath.DAY,1);var dEnd=new Date(endDate[0],endDate[1]-1,endDate[2]);var results=new Array();results.push(startDate);while(dCurrent.getTime()<=dEnd.getTime())
{results.push([dCurrent.getFullYear(),dCurrent.getMonth()+1,dCurrent.getDate()]);dCurrent=SOUTHERN.caldoy.DateMath.add(dCurrent,SOUTHERN.caldoy.DateMath.DAY,1);}
return results;};SOUTHERN.caldoy.Calendar_Core.prototype.resetRenderers=function(){this.renderStack=this._renderStack.concat();};SOUTHERN.caldoy.Calendar_Core.prototype.clearElement=function(cell){cell.innerHTML="&nbsp;";cell.className="";};SOUTHERN.caldoy.Calendar_Core.prototype.addRenderer=function(sDates,fnRender){var aDates=this._parseDates(sDates);for(var i=0;i<aDates.length;++i)
{var aDate=aDates[i];if(aDate.length==2)
{if(aDate[0]instanceof Array)
{this._addRenderer(SOUTHERN.caldoy.Calendar_Core.RANGE,aDate,fnRender);}else{this._addRenderer(SOUTHERN.caldoy.Calendar_Core.MONTH_DAY,aDate,fnRender);}}else if(aDate.length==3)
{this._addRenderer(SOUTHERN.caldoy.Calendar_Core.DATE,aDate,fnRender);}}};SOUTHERN.caldoy.Calendar_Core.prototype._addRenderer=function(type,aDates,fnRender){var add=[type,aDates,fnRender];this.renderStack.unshift(add);this._renderStack=this.renderStack.concat();};SOUTHERN.caldoy.Calendar_Core.prototype.addMonthRenderer=function(month,fnRender){this._addRenderer(SOUTHERN.caldoy.Calendar_Core.MONTH,[month],fnRender);};SOUTHERN.caldoy.Calendar_Core.prototype.addWeekdayRenderer=function(weekday,fnRender){this._addRenderer(SOUTHERN.caldoy.Calendar_Core.WEEKDAY,[weekday],fnRender);};SOUTHERN.caldoy.Calendar_Core.addCssClass=function(element,style){if(element.className.length===0)
{element.className+=style;}else{element.className+=" "+style;}};SOUTHERN.caldoy.Calendar_Core.prependCssClass=function(element,style){element.className=style+" "+element.className;}
SOUTHERN.caldoy.Calendar_Core.removeCssClass=function(element,style){var aStyles=element.className.split(" ");for(var s=0;s<aStyles.length;++s)
{if(aStyles[s]==style)
{aStyles.splice(s,1);break;}}
SOUTHERN.caldoy.Calendar_Core.setCssClasses(element,aStyles);};SOUTHERN.caldoy.Calendar_Core.setCssClasses=function(element,aStyles){element.className="";var className=aStyles.join(" ");element.className=className;};SOUTHERN.caldoy.Calendar_Core.prototype.clearAllBodyCellStyles=function(style){for(var c=0;c<this.cells.length;++c)
{SOUTHERN.caldoy.Calendar_Core.removeCssClass(this.cells[c],style);}};SOUTHERN.caldoy.Calendar_Core.prototype.setMonth=function(month){this.pageDate.setMonth(month);};SOUTHERN.caldoy.Calendar_Core.prototype.setYear=function(year){this.pageDate.setFullYear(year);};SOUTHERN.caldoy.Calendar_Core.prototype.getSelectedDates=function(){var returnDates=new Array();for(var d=0;d<this.selectedDates.length;++d)
{var dateArray=this.selectedDates[d];var date=new Date(dateArray[0],dateArray[1]-1,dateArray[2]);returnDates.push(date);}
returnDates.sort();return returnDates;};SOUTHERN.caldoy.Calendar_Core._getBrowser=function()
{var ua=navigator.userAgent.toLowerCase();if(ua.indexOf('opera')!=-1)
return'opera';else if(ua.indexOf('msie')!=-1)
return'ie';else if(ua.indexOf('safari')!=-1)
return'safari';else if(ua.indexOf('gecko')!=-1)
return'gecko';else
return false;}
SOUTHERN.caldoy.Cal_Core=SOUTHERN.caldoy.Calendar_Core;SOUTHERN.namespace("SOUTHERN.caldoy");SOUTHERN.caldoy.CalendarGroup=function(pageCount,id,containerId,monthyear,selected){if(arguments.length>0)
{this.init(pageCount,id,containerId,monthyear,selected);}}
SOUTHERN.caldoy.CalendarGroup.prototype.init=function(pageCount,id,containerId,monthyear,selected){this.id=id;this.selectedDates=new Array();this.containerId=containerId;this.pageCount=pageCount;this.pages=new Array();for(var p=0;p<pageCount;++p)
{var cal=this.constructChild(id+"_"+p,this.containerId+"_"+p,monthyear,selected);cal.parent=this;cal.index=p;cal.pageDate.setMonth(cal.pageDate.getMonth()+p);cal._pageDateOrig=new Date(cal.pageDate.getFullYear(),cal.pageDate.getMonth(),cal.pageDate.getDate());this.pages.push(cal);}
this.doNextMonth=function(e,calGroup){calGroup.nextMonth();}
this.doPreviousMonth=function(e,calGroup){calGroup.previousMonth();}};SOUTHERN.caldoy.CalendarGroup.prototype.setChildFunction=function(fnName,fn){for(var p=0;p<this.pageCount;++p){this.pages[p][fnName]=fn;}}
SOUTHERN.caldoy.CalendarGroup.prototype.callChildFunction=function(fnName,args){for(var p=0;p<this.pageCount;++p){var page=this.pages[p];if(page[fnName]){var fn=page[fnName];fn.call(page,args);}}}
SOUTHERN.caldoy.CalendarGroup.prototype.constructChild=function(id,containerId,monthyear,selected){return new SOUTHERN.caldoy.Calendar_Core(id,containerId,monthyear,selected);};SOUTHERN.caldoy.CalendarGroup.prototype.setMonth=function(month){for(var p=0;p<this.pages.length;++p)
{var cal=this.pages[p];cal.setMonth(month+p);}};SOUTHERN.caldoy.CalendarGroup.prototype.setYear=function(year){for(var p=0;p<this.pages.length;++p)
{var cal=this.pages[p];if((cal.pageDate.getMonth()+1)==1&&p>0)
{year+=1;}
cal.setYear(year);}};

SOUTHERN.caldoy.CalendarGroup.prototype.render=function()
{

	for(var p=0;p<this.pages.length;++p)
	{
		var cal=this.pages[p];cal.render();
	}
};

SOUTHERN.caldoy.CalendarGroup.prototype.select=function(date)
{
	var ret;
	for(var p=0;p<this.pages.length;++p)
	{
		var cal=this.pages[p];
		ret=cal.select(date);
	}
	return ret;
};SOUTHERN.caldoy.CalendarGroup.prototype.selectCell=function(cellIndex){var ret;for(var p=0;p<this.pages.length;++p)
{var cal=this.pages[p];ret=cal.selectCell(cellIndex);}
return ret;};SOUTHERN.caldoy.CalendarGroup.prototype.deselect=function(date){var ret;for(var p=0;p<this.pages.length;++p)
{var cal=this.pages[p];ret=cal.deselect(date);}
return ret;};SOUTHERN.caldoy.CalendarGroup.prototype.deselectAll=function(){var ret;for(var p=0;p<this.pages.length;++p)
{var cal=this.pages[p];ret=cal.deselectAll();}
return ret;};SOUTHERN.caldoy.CalendarGroup.prototype.deselectCell=function(cellIndex){for(var p=0;p<this.pages.length;++p)
{var cal=this.pages[p];cal.deselectCell(cellIndex);}
return this.getSelectedDates();};

SOUTHERN.caldoy.CalendarGroup.prototype.reset=function(){for(var p=0;p<this.pages.length;++p)
{var cal=this.pages[p];cal.reset();}};

SOUTHERN.caldoy.CalendarGroup.prototype.clear=function(){for(var p=0;p<this.pages.length;++p)
{var cal=this.pages[p];cal.clear();}};

SOUTHERN.caldoy.CalendarGroup.prototype.nextMonth=function(){for(var p=0;p<this.pages.length;++p)
{var cal=this.pages[p];cal.nextMonth();}};

SOUTHERN.caldoy.CalendarGroup.prototype.previousMonth=function(){for(var p=this.pages.length-1;p>=0;--p)
{var cal=this.pages[p];cal.previousMonth();}};

SOUTHERN.caldoy.CalendarGroup.prototype.nextYear=function(){for(var p=0;p<this.pages.length;++p)
{var cal=this.pages[p];cal.nextYear();}};

SOUTHERN.caldoy.CalendarGroup.prototype.previousYear=function(){for(var p=0;p<this.pages.length;++p)
{var cal=this.pages[p];cal.previousYear();}};


SOUTHERN.caldoy.CalendarGroup.prototype.sync=function(caller)
{
	var calendar;
	if(caller)
	{
		this.selectedDates=caller.selectedDates.concat();
	}
	else
	{
		var hash=new Object();
		var combinedDates=new Array();
		for(var p=0;p<this.pages.length;++p)
		{
			calendar=this.pages[p];
			var values=calendar.selectedDates;
			for(var v=0;v<values.length;++v)
			{
				var valueArray=values[v];
				hash[valueArray.toString()]=valueArray;
			}
		}
		for(var val in hash)
		{
			combinedDates[combinedDates.length]=hash[val];
		}
		this.selectedDates=combinedDates.concat();
	}
	for(p=0;p<this.pages.length;++p)
	{
		calendar=this.pages[p];
		if(!calendar.Options.MULTI_SELECT)
		{
			calendar.clearAllBodyCellStyles(calendar.Config.Style.CSS_CELL_SELECTED);
		}
		calendar.selectedDates=this.selectedDates.concat();
	}
	return this.getSelectedDates();
};

SOUTHERN.caldoy.CalendarGroup.prototype.getSelectedDates=function(){var returnDates=new Array();for(var d=0;d<this.selectedDates.length;++d)
{var dateArray=this.selectedDates[d];var date=new Date(dateArray[0],dateArray[1]-1,dateArray[2]);returnDates.push(date);}
returnDates.sort();return returnDates;};

SOUTHERN.caldoy.CalendarGroup.prototype.addRenderer=function(sDates,fnRender){for(var p=0;p<this.pages.length;++p)
{var cal=this.pages[p];cal.addRenderer(sDates,fnRender);}};

SOUTHERN.caldoy.CalendarGroup.prototype.addMonthRenderer=function(month,fnRender){for(var p=0;p<this.pages.length;++p)
{var cal=this.pages[p];cal.addMonthRenderer(month,fnRender);}};

SOUTHERN.caldoy.CalendarGroup.prototype.addWeekdayRenderer=function(weekday,fnRender){for(var p=0;p<this.pages.length;++p)
{var cal=this.pages[p];cal.addWeekdayRenderer(weekday,fnRender);}};


SOUTHERN.caldoy.CalendarGroup.prototype.wireEvent=function(eventName,fn){for(var p=0;p<this.pages.length;++p)
{var cal=this.pages[p];cal[eventName]=fn;}};


SOUTHERN.caldoy.CalGrp=SOUTHERN.caldoy.CalendarGroup;

SOUTHERN.namespace("SOUTHERN.caldoy");

SOUTHERN.caldoy.Calendar=function(id,containerId,monthyear,selected){if(arguments.length>0)
{this.init(id,containerId,monthyear,selected);}}


SOUTHERN.caldoy.Calendar.prototype=new SOUTHERN.caldoy.Calendar_Core();

SOUTHERN.caldoy.Calendar.prototype.buildShell=function()
{
	this.border=document.createElement("DIV");
	this.border.className=this.Style.CSS_BORDER;
	this.table=document.createElement("TABLE");
	this.table.cellSpacing=0;
	SOUTHERN.caldoy.Calendar_Core.setCssClasses(this.table,[this.Style.CSS_CALENDAR]);
	this.border.id=this.id;
	this.buildShellHeader();
	this.buildShellBody();
	this.buildShellFooter();
};

SOUTHERN.caldoy.Calendar.prototype.renderShell=function()
{
	this.border.appendChild(this.table);
	this.oDomContainer.appendChild(this.border);
	this.shellRendered=true;
};


SOUTHERN.caldoy.Calendar.prototype.renderHeader=function()
{
	this.headerCell.innerHTML="";
	var headerContainer=document.createElement("DIV");
	headerContainer.className=this.Style.CSS_HEADER;
	var linkLeft=document.createElement("A");
	linkLeft.href="javascript:"+this.id+".previousMonth()";
	var imgLeft=document.createElement("IMG");
	imgLeft.src=this.Options.NAV_ARROW_LEFT;
	imgLeft.className=this.Style.CSS_NAV_LEFT;
	linkLeft.appendChild(imgLeft);
	var linkRight=document.createElement("A");
	linkRight.href="javascript:"+this.id+".nextMonth()";
	var imgRight=document.createElement("IMG");
	imgRight.src=this.Options.NAV_ARROW_RIGHT;
	imgRight.className=this.Style.CSS_NAV_RIGHT;
	linkRight.appendChild(imgRight);
	headerContainer.appendChild(linkLeft);
	headerContainer.appendChild(document.createTextNode(this.buildMonthLabel()));
	headerContainer.appendChild(linkRight);
	this.headerCell.appendChild(headerContainer);
};

SOUTHERN.caldoy.Cal=SOUTHERN.caldoy.Calendar;
SOUTHERN.namespace("SOUTHERN.caldoy");

SOUTHERN.caldoy.Calendar2up_Cal=function(id,containerId,monthyear,selected){if(arguments.length>0)
{this.init(id,containerId,monthyear,selected);}}

SOUTHERN.caldoy.Calendar2up_Cal.prototype=new SOUTHERN.caldoy.Calendar_Core();

SOUTHERN.caldoy.Calendar2up_Cal.prototype.renderHeader=function()
{
	this.headerCell.innerHTML="";
	var headerContainer=document.createElement("DIV");
	headerContainer.className=this.Style.CSS_HEADER;
	if(this.index==0)
	{
		var linkLeft=document.createElement("A");
		linkLeft.href="javascript:void(null)";
		SOUTHERN.util.Event.addListener(linkLeft,"click",this.parent.doPreviousMonth,this.parent);
		var imgLeft=document.createElement("IMG");
		imgLeft.src=this.Options.NAV_ARROW_LEFT;
		imgLeft.className=this.Style.CSS_NAV_LEFT;linkLeft.appendChild(imgLeft);
		headerContainer.appendChild(linkLeft);
	}
	headerContainer.appendChild(document.createTextNode(this.buildMonthLabel()));
	if(this.index==1)
	{
		var linkRight=document.createElement("A");
		linkRight.href="javascript:void(null)";
		SOUTHERN.util.Event.addListener(linkRight,"click",this.parent.doNextMonth,this.parent);
		var imgRight=document.createElement("IMG");
		imgRight.src=this.Options.NAV_ARROW_RIGHT;
		imgRight.className=this.Style.CSS_NAV_RIGHT;
		linkRight.appendChild(imgRight);
		headerContainer.appendChild(linkRight);
	}
	this.headerCell.appendChild(headerContainer);
};

SOUTHERN.caldoy.Calendar2up=function(id,containerId,monthyear,selected,f){if(arguments.length>0)
{this.buildWrapper(containerId);this.init(2,id,containerId,monthyear,selected);}}

SOUTHERN.caldoy.Calendar2up.prototype=new SOUTHERN.caldoy.CalendarGroup();

SOUTHERN.caldoy.Calendar2up.prototype.constructChild=function(id,containerId,monthyear,selected)
{
	var cal=new SOUTHERN.caldoy.Calendar2up_Cal(id,containerId,monthyear,selected);return cal;
};

SOUTHERN.caldoy.Calendar2up.prototype.buildWrapper=function(containerId)
{
	var outerContainer=document.getElementById(containerId);
	outerContainer.className="calcontainer";
	var innerContainer=document.createElement("DIV");
	innerContainer.className="calbordered";
	innerContainer.id=containerId+"_inner";
	var cal1Container=document.createElement("DIV");
	cal1Container.id=containerId+"_0";cal1Container.className="cal2up";
	cal1Container.style.marginRight="10px";
	var cal2Container=document.createElement("DIV");
	cal2Container.id=containerId+"_1";
	cal2Container.className="cal2up";
	outerContainer.appendChild(innerContainer);
	innerContainer.appendChild(cal1Container);
	innerContainer.appendChild(cal2Container);
	this.innerContainer=innerContainer;
	this.outerContainer=outerContainer;
}

SOUTHERN.caldoy.Calendar2up.prototype.render=function()
{
	this.renderHeader();
	SOUTHERN.caldoy.CalendarGroup.prototype.render.call(this);
	this.renderFooter();
};

SOUTHERN.caldoy.Calendar2up.prototype.renderHeader=function(){if(!this.title){this.title="";}
if(!this.titleDiv)
{this.titleDiv=document.createElement("DIV");if(this.title=="")
{this.titleDiv.style.display="none";}}
this.titleDiv.className="title";this.titleDiv.innerHTML=this.title;if(this.outerContainer.style.position=="absolute")
{var linkClose=document.createElement("A");linkClose.href="javascript:void(null)";SOUTHERN.util.Event.addListener(linkClose,"click",this.hide,this);var imgClose=document.createElement("IMG");imgClose.src="Images/close.gif";imgClose.className="close-icon";linkClose.appendChild(imgClose);this.linkClose=linkClose;this.titleDiv.appendChild(linkClose);}
this.innerContainer.insertBefore(this.titleDiv,this.innerContainer.firstChild);}
SOUTHERN.caldoy.Calendar2up.prototype.hide=function(e,cal){if(!cal)
{cal=this;}
cal.outerContainer.style.display="none";
document.getElementById('framediv').style.display = "none";


}
SOUTHERN.caldoy.Calendar2up.prototype.renderFooter=function(){}
SOUTHERN.caldoy.Cal2up=SOUTHERN.caldoy.Calendar2up;

function HideObjects()
		{
		
			if(stObj)
			{
				stObj.hide();
			}
			
		
		}	