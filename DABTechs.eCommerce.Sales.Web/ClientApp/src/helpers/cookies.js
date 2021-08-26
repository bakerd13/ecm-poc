// export const createCookie = (name, value, days, domain) => {
//     if (days) {
//         var date = new Date();
//         date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
//         var expires = "; expires=" + date.toGMTString();
//     }
//     else var expires = "";

//     var cookieValue = name + "=" + value + expires + "; path=/";
//     if (domain != undefined && domain != "") {
//         cookieValue += ";domain=" + domain + ";";
// 	}

//     document.cookie = cookieValue;
// }

// export const readCookie = (name) => {

//     var nameEQ = name + "=";
//     var ca = document.cookie.split(';');
//     for (var i = 0; i < ca.length; i++) {
//         var c = ca[i];
//         while (c.charAt(0) == ' ') c = c.substring(1, c.length);
//         if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
//     }
//     return null;
// }

// export const eraseCookie = (name) => {
//     createCookie(name, "", -1, "");
// }

// // Reads a cookie and splits on "&", returning the value for the keyName provided
// export const readSplitCookie = (cookieName, keyName) => {
//     var cookieValue = {};
//     var nextSessionVariables = readCookie(cookieName);
//     if (nextSessionVariables != null) {
//         var splitVars = nextSessionVariables.split("&");
//         for (var i = 0; i < splitVars.length; i++) {
//             var splitKvp = splitVars[i].split("=");
//             if (splitKvp != null) {
//                 cookieValue[splitKvp[0]] = splitKvp[1];
//             }
//         }
//     }
//     if (typeof (keyName) != 'undefined') {
//         return cookieValue[keyName];
//     }
//     return cookieValue;
// }

// // Sets a cookie - updating it with a key value pair
// export const setSplitCookie = (cookieName, keyName, keyValue, days) => {
 
//     var nextSessionVariables = readCookie(cookieName);
//     var newValue = "";
//     var valueAdded = false;

//     if (nextSessionVariables != null) {
//         var splitVars = nextSessionVariables.split("&");
//         for (var i = 0; i < splitVars.length; i++) {
//            // if (i > 0)
//                 newValue += "&";
//             var splitKvp = splitVars[i].split("=");
//             if (splitKvp != null && splitKvp[0] == keyName) {
//                 newValue += keyName + "=" + keyValue;
//                 valueAdded = true;
//             }
//             else
//                 newValue += splitVars[i];
//         }
//     }
//     if (!valueAdded)
//         newValue += "&" + keyName + "=" + keyValue;

//     if (newValue.indexOf("&") == 0) 
//         newValue = newValue.substring(1);

//     createCookie(cookieName, newValue, days, ABPlatformCookieDomain);
// }

export const nextCookie =
{
    set: (name, value, days) => {
        var domain, date, expires, host;

        if (days) {
            date = new Date();
            date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
            expires = "; expires=" + date.toGMTString();
        }
        else {
            expires = "";
        }

        host = window.location.host;

        domain = nextCookie.getCookieDomain(host);

        if (domain.length === 0) {
            document.cookie = name + "=" + value + expires + "; path=/";
        }
        else {
            domain = ".co.uk.local";
            document.cookie = name + "=" + value + expires + "; path=/; domain=" + domain;

            // check if cookie was successfuly set to the given domain
            // (otherwise it was a Top-Level Domain)
            if (nextCookie.get(name) === null || nextCookie.get(name) !== value) {
                // append "." to current domain
                domain = '.' + host;
                domain = ".co.uk.local";
                document.cookie = name + "=" + value + expires + "; path=/; domain=" + domain;
            }
        }
    },

    getCookieDomain: (host) => {
        var domain, domainParts;

        if (host.split('.').length === 1) {
            // no "." in a domain - it's localhost or something similar
            return "";
        }
        else {
            // Remember the cookie on all subdomains.
            //
            // Start with trying to set cookie to the top domain.
            // (example: if user is on foo.com, try to set
            //  cookie to domain ".com")
            //
            // If the cookie will not be set, it means ".com"
            // is a top level domain and we need to
            // set the cookie to ".foo.com"
            var prefixes = ["next", "nextdirect"];
            domainParts = host.split('.');
            if (prefixes.indexOf(domainParts[0].toLowerCase()) === -1) {
                domainParts.shift();
            }
            domain = '.' + domainParts.join('.');
        }
        return domain;
    },

    get: (name) => {
        var nameEQ = name + "=";
        var ca = document.cookie.split(';');
        for (var i = 0; i < ca.length; i++) {
            var c = ca[i];
            while (c.charAt(0) === ' ') {
                c = c.substring(1, c.length);
            }

            if (c.indexOf(nameEQ) === 0) return c.substring(nameEQ.length, c.length);
        }
        return null;
    },

    erase: (name) => {
        nextCookie.set(name, '', -1);
    }
};