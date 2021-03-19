export {
    enableDraggable, disableDraggable, resetDraggableElePosition
} from "./dragHelper";

import { autoDebug } from "./global";
import { getDom, attr } from "./core/base";


const lastNormalStyleMap = new WeakMap();

/**
 * for min to reset drag left and top info
 * @param formRoot
 */
export function minResetStyle(formRoot: string | HTMLElement, lastIsNormal:boolean) {
    let rootEle = getDom(formRoot);
    if (rootEle) {
        let form = rootEle.querySelector('.bcd-form') as HTMLElement;
        if (lastIsNormal) {
            let lastNormalStyle = attr(form, "style");
            lastNormalStyleMap.set(form, lastNormalStyle);

            autoDebug(() => {
                console.log("get lastNormalStyle", lastNormalStyle);
            });
        }

        form!.style.left = "";
        form!.style.top = "";
        form!.style.right = "";
    }
}


/**
 * for min to reset drag left and top info
 * @param formRoot
 */
export function maxResetStyle(formRoot: string | HTMLElement, lastIsNormal: boolean) {
    let rootEle = getDom(formRoot);
    if (rootEle) {
        let form = rootEle.querySelector('.bcd-form') as HTMLElement;
        if (lastIsNormal) {
            let lastNormalStyle = attr(form, "style");
            lastNormalStyleMap.set(form, lastNormalStyle);

            autoDebug(() => {
                console.log("get lastNormalStyle", lastNormalStyle);
            });
        }

        form!.style.left = "";
        form!.style.top = "";
    }
}


/**
 * for normal state form to reset style
 * @param formRoot
 */
export function normalResetStyle(formRoot: string | HTMLElement) {
    let rootEle = getDom(formRoot);
    if (rootEle) {
        let form = rootEle.querySelector('.bcd-form') as HTMLElement;
        if (lastNormalStyleMap.has(form)) {
            let lastNormalStyle = lastNormalStyleMap.get(form);
            autoDebug(() => {
                console.log("set lastNormalStyle", lastNormalStyle);
            });
            if (lastNormalStyle !== null) {
                attr(form, "style", lastNormalStyle);
            }
        }
    }
}