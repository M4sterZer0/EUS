function loginPlayer() {
    $("#button").prop("disabled", true);
    resourceCall("login", $("#username").val(), $("#pass").val());
    setTimeout(function () { $("#button").prop("disabled", false); }, 1500);
}

function registerPlayer() {
    $("#button").prop("disabled", true);
    resourceCall("register", $("#vorname").val(), $("#nachname").val(), $("#pass").val(), $("#passwdh").val());
    setTimeout(function () { $("#button").prop("disabled", false); }, 1500);
}

function setLoginmessage(text) {
    $("#message").html(text);
}

function setRegistermessage(text) {
    $("#message").html(text);
}

function showRegister() {
    resourceCall("showRegister");
}

function showLogin() {
    resourceCall("showLogin");
}