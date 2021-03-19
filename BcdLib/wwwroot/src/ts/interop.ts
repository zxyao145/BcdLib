export {
    enableDraggable, disableDraggable, resetDraggableElePosition
} from "./dragHelper";


import { getDom } from "./core/base";

/**
 * for min to reset drag left and top info
 * @param formRoot
 */
export function minResetStyle(formRoot: string|HTMLElement) {
    let rootEle = getDom(formRoot);
    if (rootEle) {
        let form = rootEle.querySelector('.bcd-form') as HTMLElement;
        form!.style.left = "";
        form!.style.top = "";
        form!.style.right = "";
    }
}


/**
 * for min to reset drag left and top info
 * @param formRoot
 */
export function maxResetStyle(formRoot: string | HTMLElement) {
    let rootEle = getDom(formRoot);
    if (rootEle) {
        let form = rootEle.querySelector('.bcd-form') as HTMLElement;
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
        form!.style.position = "";
        form!.style.left = "";
        form!.style.top = "";
    }
}