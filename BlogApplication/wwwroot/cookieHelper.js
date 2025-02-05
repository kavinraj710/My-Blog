window.cookieHelper = {
    setCookie: function (name, value, days) {
        document.cookie = `${name}=${encodeURIComponent(value)}; path=/; SameSite=Strict; Secure`;
    },
    deleteCookie: function (name) {
        document.cookie = `${name}=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/; SameSite=Strict; Secure`;
    }
};
//window.CookieReader = {
//    Read: function (cookie_name) {
//        var name = cookie_name + "=";
//        var decodedCookie = decodeURIComponent(document.cookie);
//        var ca = decodedCookie.split(';');
//        for (var i = 0; i < ca.length; i++) {
//            var c = ca[i];
//            while (c.charAt(0) == ' ') {
//                c = c.substring(1);
//            }
//            if (c.indexOf(name) == 0) {
//                return c.substring(name.length, c.length);
//            }
//        }
//        return "";
//    }
//};