var player = API.getLocalPlayer();
var cefLoginWindow = null;

API.onServerEventTrigger.connect(function (eventName, args) {
    switch (eventName) {
        case 'showWindow': {
            if (args[0] == "login") {
                API.setCanOpenChat(false);
                var res = API.getScreenResolution();
                cefLoginWindow = API.createCefBrowser(res.Width, res.Height);
                API.setCefBrowserPosition(cefLoginWindow, 1, 1);
                API.waitUntilCefBrowserInit(cefLoginWindow);
                API.loadPageCefBrowser(cefLoginWindow, "ClientFiles/login.html");
                if (API.isCefBrowserInitialized(cefLoginWindow)) {
                    API.sleep(100);
                    API.showCursor(true);
                    cefLoginWindow.call("setName", API.getPlayerName(player));
                }
            }
            break;
        }
        case 'hideWindow': {
            if (args[0] == "login") {
                API.showCursor(false);
                API.setCanOpenChat(true);
                if (cefLoginWindow != null)
                    API.destroyCefBrowser(cefLoginWindow);
            }
            break;
        }
        case 'setLoginmessage': {
            if (cefLoginWindow != null)
                cefLoginWindow.call("setLoginmessage", args[0]);
        }
    }
});

function login(username, password) {
    API.triggerServerEvent("closeLogin", username, pass);
}