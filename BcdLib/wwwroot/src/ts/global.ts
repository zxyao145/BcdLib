
var autoDebug = (invoke: Function) => {
    var isDebug = sessionStorage.getItem("isDebug") === "true";

    if (isDebug) {
        invoke();
    }
}

declare global {
    interface Window {
        bcd: any;
        autoDebug: Function;
    }
}

export { autoDebug };
