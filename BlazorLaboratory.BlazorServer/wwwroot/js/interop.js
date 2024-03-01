
function logToConsole(value) {
    console.log(value);
}

function showAlert() {
    alert("JsInterop test success!");
}

window.jsFunctions = {
    dotNetRef: null,

    initDotNetObjectReference: function (dotNetReference) {
        this.dotNetRef = dotNetReference;
    },

    showSnackbar: function (text) {
        this.dotNetRef.invokeMethodAsync('ShowSnackBar', text);
    }
}

function updateResult() {
    DotNet.invokeMethodAsync("BlazorLaboratory.BlazorServer", "CallCSharpFunc");
}

