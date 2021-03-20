export {
    enableDraggable, disableDraggable, resetDraggableElePosition
} from "./dragHelper";

import { autoDebug } from "./global";
import { getDom, attr } from "./core/base";


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