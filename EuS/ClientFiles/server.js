var player = API.getLocalPlayer();
var cefLoginWindow = null;
var cefRegisterWindow = null;

API.onServerEventTrigger.connect(function (eventName, args) {
    switch (eventName) {
        case 'showWindow': {
            if (args[0] == "login") {
                API.setCanOpenChat(false);
                var res = API.getScreenResolution();
                cefLoginWindow = API.createCefBrowser(res.Width, res.Height);
                API.setCefBrowserPosition(cefLoginWindow, 0, 0);
                API.waitUntilCefBrowserInit(cefLoginWindow);
                API.loadPageCefBrowser(cefLoginWindow, "ClientFiles/pages/login/login.html");
                if (API.isCefBrowserInitialized(cefLoginWindow)) {
                    API.sleep(10);
                    if (!API.isCursorShown())
                        API.showCursor(true);
                }
                break;
            }
            else if (args[0] == "register") {
                API.setCanOpenChat(false);
                var res = API.getScreenResolution();
                cefRegisterWindow = API.createCefBrowser(res.Width, res.Height);
                API.setCefBrowserPosition(cefRegisterWindow, 0, 0);
                API.waitUntilCefBrowserInit(cefRegisterWindow);
                API.loadPageCefBrowser(cefRegisterWindow, "ClientFiles/pages/login/register.html");
                if (API.isCefBrowserInitialized(cefRegisterWindow)) {
                    API.sleep(10);
                    if(!API.isCursorShown())
                        API.showCursor(true);
                }
                break;
            }
        }
        case 'hideWindow': {
            if (args[0] == "login") {
                API.showCursor(false);
                API.setCanOpenChat(true);
                if (cefLoginWindow != null)
                    API.destroyCefBrowser(cefLoginWindow);
                break;
            }
            else if (args[0] == "register") {
                API.showCursor(false);
                API.setCanOpenChat(true);
                if (cefRegisterWindow != null)
                    API.destroyCefBrowser(cefRegisterWindow);
                break;
            }
        }
        case 'setLoginmessage': {
            if (cefLoginWindow != null)
                cefLoginWindow.call("setLoginmessage", args[0]);
            break;
        }
        case 'setRegistermessage': {
            if (cefRegisterWindow != null)
                cefRegisterWindow.call("setRegistermessage", args[0]);
            break;
        }
    }
});

function login(username, password) {
    API.triggerServerEvent("closeLogin", username, password);
}

function register(vorname, nachname, pass, passwdh) {
    API.triggerServerEvent("closeRegister", vorname, nachname, pass, passwdh);
}

function showRegister() {
    API.triggerServerEvent("showRegister");
}

function showLogin() {
    API.triggerServerEvent("showLogin");
}