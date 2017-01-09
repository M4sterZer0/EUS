function loginPlayer() {
    resourceCall("login", $("#username").val(), $("#pass").val());
    $("#loginb").prop("disabled", true);
    setTimeout(function () { $("#loginbutton").prop("disabled", false); }, 1500);
}

function setName(name) {
    $("#username").val(name);
}

function setLoginmessage(text) {
    $("#message").html(text);
}