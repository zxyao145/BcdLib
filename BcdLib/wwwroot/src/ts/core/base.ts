
const throttle = (fn: Function, threshold = 160) => {
    let timeout;
    var start = +new Date;
    return function (...args) {
        //@ts-ignore
        let context = this, curTime = +new Date() - 0;
        //总是干掉事件回调
        window.clearTimeout(timeout);
        if (curTime - start >= threshold) {
            //只执行一部分方法，这些方法是在某个时间段内执行一次
            fn.apply(context, args);
            start = curTime;
        }
        else {
            //让方法在脱离事件后也能执行一次
            timeout = window.setTimeout(() => {
                //@ts-ignore
                fn.apply(this, args);
            }, threshold);
        }
    };
}

const getDom = (selector: string | Element): null | HTMLElement => {
    if (!selector) {
        return null;
    }
    if (selector instanceof Element) {
        return selector as HTMLElement;
    }

    return document.querySelector(selector) as HTMLElement;
}

const attr = (selector: string | Element, key: string, value: string | null = null) => {
    let dom = getDom(selector);
    if (dom) {
        if (value) {
            dom.setAttribute(key, value);
            return value;
        } else {
            return dom.getAttribute(key);
        }
    }
    return null;
}


const $ = getDom;

export default $;
export {
    throttle, getDom, $, attr
};