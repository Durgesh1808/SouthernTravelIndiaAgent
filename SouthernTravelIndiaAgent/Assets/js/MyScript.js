function Trim(TRIM_VALUE)
{
	if(TRIM_VALUE.length < 1)
	{
		return"";
	}
	TRIM_VALUE = RTrim(TRIM_VALUE);
	TRIM_VALUE = LTrim(TRIM_VALUE);
	if(TRIM_VALUE=="")
	{
		return "";
	}
	else
	{
		return TRIM_VALUE;
	}
} //End Function

function RTrim(VALUE)
{
	var w_space = String.fromCharCode(32);
	var v_length = VALUE.length;
	
	var strTemp = "";
	if(v_length < 0)
	{
		return"";
	}
	var iTemp = v_length -1;

	while(iTemp > -1)
	{
		if(VALUE.charAt(iTemp) == w_space)
		{
		}
		else
		{
			strTemp = VALUE.substring(0,iTemp +1);
			break;
		}
		iTemp = iTemp-1;

	} //End While
	return strTemp;

} //End Function

function LTrim(VALUE)
{
	var w_space = String.fromCharCode(32);
	if(v_length < 1)
	{
		return"";
	}
		var v_length = VALUE.length;
		var strTemp = "";

		var iTemp = 0;

	while(iTemp < v_length)
	{
		if(VALUE.charAt(iTemp) == w_space)
		{
		}
		else
		{
			strTemp = VALUE.substring(iTemp,v_length);
			break;
		}
		iTemp = iTemp + 1;
	} //End While
	return strTemp;
} //End Function


function validateOnlyNumber()
{
	if (event.keyCode < 48 || event.keyCode > 57) 
	event.returnValue = false;
}

function validateOnlyNumber1(num)
{
	if (isNaN(num)==true )
	return false;
	else
	return true;
}
function ValidatePGText(evt)
{
	var charCode = (evt.which) ? evt.which : event.keyCode;
	if (((charCode >= 48 && charCode <= 57) || (charCode >= 65 && charCode <= 90) || (charCode >= 97 && charCode <= 122) || (charCode == 44) || (charCode == 32) || (charCode == 8)))
		return true;
	return false;
}
function IsANumeric(evt)
{
    var charCode = (evt.which) ? evt.which : event.keyCode;
	if ((charCode >= 48 && charCode <= 57) || (charCode == 8))
	    return true;
	return false;
}


function WebCheck(str)
{
		var urlf="http://"
		var dot="."
		var lurlf=str.indexOf(urlf)
		var lstr=str.length 
		if (str.indexOf(urlf)==-1)
		{
			return false
		}
		if (str.indexOf(dot)==-1 || str.indexOf(dot)==0 || str.indexOf(dot)==lstr)
		{
		return false
		}
		if (str.indexOf(" ")!=-1)
		{
		return false
		}
		return true

}

function echeck(str) 
{
        if (str.search(/^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/) != -1)
            {
                return true;
            }
            else
            {
                //alert("Invalid E-mail ID");
                return false;
            }	

//	var at="@"
//	var dot="."
//	var lat=str.indexOf(at)
//	var lstr=str.length
//	var ldot=str.indexOf(dot)
//	if (str.indexOf(at)==-1)
//	{
//		return false
//	}
//	if (str.indexOf(at)==-1 || str.indexOf(at)==0 || str.indexOf(at)==lstr)
//	{
//		return false
//	}

//	if (str.indexOf(dot)==-1 || str.indexOf(dot)==0 || str.indexOf(dot)==lstr)
//	{
//		return false
//	}

//	if (str.indexOf(at,(lat+1))!=-1)
//	{
//		return false
//	}

//	if (str.substring(lat-1,lat)==dot || str.substring(lat+1,lat+2)==dot)
//	{
//		return false
//	}

//	if (str.indexOf(dot,(lat+2))==-1)
//	{
//		return false
//	}
//						
//	if (str.indexOf(" ")!=-1)
//	{
//		return false
//	}

	return true					
} 

function isEmail (s)
			{  
				// there must be >= 1 character before @, so we start looking at character position 1 (i.e. second character)
				var i = 1;
				var sLength = s.length;
				// look for @
				while ((i < sLength) && (s.charAt(i) != "@"))
				{
					i++;
				}
				if ((i >= sLength) || (s.charAt(i) != "@")) return false;
				else i += 2;
				// look for .
				while ((i < sLength) && (s.charAt(i) != "."))
				{ i++;
				}
				// there must be at least one character after the .
				if ((i >= sLength - 1) || (s.charAt(i) != ".")) return false;
				else return true;
			}