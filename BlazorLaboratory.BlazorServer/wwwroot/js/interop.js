function logToConsole(value) {
    console.log(value);
}

function showAlert() {
    alert("JsInterop test success!");
}

function updateResult() {
    DotNet.invokeMethodAsync("BlazorLaboratory.BlazorServer", "CallCSharpFunc");
}