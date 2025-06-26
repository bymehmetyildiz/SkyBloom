// Assets/Plugins/mobilecheck.jslib
mergeInto(LibraryManager.library, {
    IsMobileBrowser: function() {
        var ua = navigator.userAgent || navigator.vendor || window.opera;
        // Checks for iOS, Android, Windows Phone
        return /android|iphone|ipad|ipod|windows phone/i.test(ua) ? 1 : 0;
    }
});