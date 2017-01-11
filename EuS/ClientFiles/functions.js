function loginPlayer() {
    $("#u344-3").prop("disabled", true);
    resourceCall("login", $("#widgetu355_input").val());
    setTimeout(function () { $("#u344-3").prop("disabled", false); }, 1500);
}

function setName(name) {
    $("#username").val(name);
}

function setLoginmessage(text) {
    $("#u403-2").html(text);
}