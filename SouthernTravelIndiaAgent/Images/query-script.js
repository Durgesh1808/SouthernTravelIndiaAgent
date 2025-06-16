
<!-- HIDING FROM OTHER BROWSERS
expires = new Date();
expires.setTime (expires.getTime() + 24 * 60 * 60 * 150 * 1000);
function set(){

if ( (document.dataform.S_name.value == "") || (document.dataform.S_email.value == "") || (document.dataform.S_phone.value == "")
        || (document.dataform.S_country.value == "")  )
{
    alert ("Please fill in all the compulsory fields");
    return (false);
}

if(document.dataform.S_email.value.indexOf(" ") >= 0)
{
	alert("Please enter your email-id without any white space character.");
	document.dataform.S_email.focus();
	return (false);
}
if ( (document.dataform.S_email.value.indexOf("@") == -1) || (document.dataform.S_email.value.indexOf(".") == -1) )
{
	alert("Please enter a valid email-id"); 
	document.dataform.S_email.focus();
	return (false);
}


BeforeAtRate = document.dataform.S_email.value.substring(0,document.dataform.S_email.value.indexOf("@"));
AfterAtRate = document.dataform.S_email.value.substring(document.dataform.S_email.value.indexOf("@")+1,document.dataform.S_email.value.length);

if (AfterAtRate.indexOf(".") == -1)
{
	alert("Please enter a valid email-id"); 
	document.dataform.S_email.focus();
	return (false);
}

middle = AfterAtRate.substring(0, AfterAtRate.indexOf("."))
last = AfterAtRate.substring(AfterAtRate.indexOf(".") + 1,AfterAtRate.length)

if (BeforeAtRate.length == 0 || middle.length == 0 || last.length == 0)
{
	alert("Please enter a valid email-id"); 
	document.dataform.S_email.focus();
	return (false);
}

newCookie = document.dataform.S_name.value;
newCookie +="|"+document.dataform.S_email.value;
newCookie +="|"+document.dataform.S_phone.value;
newCookie +="|"+document.dataform.S_country.value;
newCookie +="|";
setCookie("newImeshID",newCookie);
return true;
}
function get(){
if( (cookie = getCookie("newImeshID")) > ""){
	Values = cookie.split("|");
	if (Values.length >= 1){
		if (document.dataform.S_name.value.length == 0) document.dataform.S_name.value = Values[0];
		if (document.dataform.S_email.value.length == 0)    document.dataform.S_email.value = Values[1];
		if (document.dataform.S_phone.value.length == 0)    document.dataform.S_phone.value=Values[2];
		if (document.dataform.S_country.value.length == 0) document.dataform.S_country.value=Values[3];
	}
}
return true;
}
function setCookie(name, value){
if (value.length > 0)
 document.cookie = name + "=" + escape(value)+ ";"+"expires=" + expires.toGMTString()+";"
}
function getCookie(Name) {
          var search = Name + "="
          if (document.cookie.length > 0) { // if there are any cookies
                    offset = document.cookie.indexOf(search) 
                    if (offset != -1) { // if cookie exists 
                              offset += search.length 
                              // set index of beginning of value
                              end = document.cookie.indexOf(";", offset) 
                              // set index of end of cookie value
                              if (end == -1) end = document.cookie.length
                              return unescape(document.cookie.substring(offset, end))
                    } 
          }
return "";
}
// STOP HIDING FROM OTHER BROWSERS -->
