function loginPlayer() {
    $("#button").prop("disabled", true);
    resourceCall("login", $("#pass").val());
    setTimeout(function () { $("#button").prop("disabled", false); }, 1500);
}

function setName(name) {
    $("#username").val(name);
}

function setLoginmessage(text) {
    $("#message").html(text);
}