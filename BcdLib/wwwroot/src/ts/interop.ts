export {
    enableDraggable, disableDraggable, resetDraggableElePosition
} from "./dragHelper";

import { autoDebug } from "./global";
import { getDom, attr, css, addCls, removeCls } from "./core/base";


/**
 * for min to reset drag left and top info
 * @param formRoot
 */
export function minResetStyle(formRoot: string | HTMLElement, lastIsNormal:boolean) {
    let rootEle = getDom(formRoot);
    let lastNormalStyle: string | null = "";
    if (rootEle) {
        let form = rootEle.querySelector('.bcd-form') as HTMLElement;
        
        if (lastIsNormal) {
            lastNormalStyle = attr(form, "style");

            autoDebug(() => {
                console.log("get lastNormalStyle", lastNormalStyle);
            });
        }

        autoDebug(() => {
            console.log("minResetStyle");
        });

        form!.style.left = "";
        form!.style.top = "";
        form!.style.right = "";
    }
    return lastNormalStyle;
}


/**
 * for min to reset drag left and top info
 * @param formRoot
 */
export function maxResetStyle(formRoot: string | HTMLElement, lastIsNormal: boolean) {
    let rootEle = getDom(formRoot);
    let lastNormalStyle: string | null = "";
    if (rootEle) {
        let form = rootEle.querySelector('.bcd-form') as HTMLElement;
        if (lastIsNormal) {
            lastNormalStyle = attr(form, "style");

            autoDebug(() => {
                console.log("get lastNormalStyle", lastNormalStyle);
            });
        }

        autoDebug(() => {
            console.log("maxResetStyle");
        });

        form!.style.left = "";
        form!.style.top = "";
    }
    return lastNormalStyle;
}

let oldBodyCacheStack: Array<any> = [];


const hasScrollbar = () => {
    let overflow = document.body.style.overflow;
    if (overflow && overflow === "hidden") return false;
    return document.body.scrollHeight > (window.innerHeight || document.documentElement.clientHeight);
}

export function  disableBodyScroll() {
    let body = document.body;
    const oldBodyCache = {};
    ["position", "width", "overflow"].forEach((key) => {
        oldBodyCache[key] = body.style[key];
    });
    oldBodyCacheStack.push(oldBodyCache);
    css(body,
        {
            "position": "relative",
            "width": hasScrollbar() ? "calc(100% - 17px)" : null,
            "overflow": "hidden"
        });
    addCls(document.body, "ant-scrolling-effect");
}

export function  enableBodyScroll() {
    let oldBodyCache = oldBodyCacheStack.length > 0 ? oldBodyCacheStack.pop() : {};

    css(document.body,
        {
            "position": oldBodyCache["position"] ?? null,
            "width": oldBodyCache["width"] ?? null,
            "overflow": oldBodyCache["overflow"] ?? null
        });
    removeCls(document.body, "ant-scrolling-effect");
}