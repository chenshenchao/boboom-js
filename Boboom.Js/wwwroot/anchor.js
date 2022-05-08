export function getPosition(element) {
    // 位移偏移
    let ox = element.offsetLeft;
    let oy = element.offsetTop;
    let op = element.offsetParent;
    while (op) {
        ox += op.offsetLeft;
        oy += op.offsetTop;
        op = op.offsetParent;
    }

    // 滚动条偏移
    let sx = element.scrollLeft;
    let sy = element.scrollTop;
    let sp = element.parentElement;
    while (sp) {
        sx += sp.scrollLeft;
        sy += sp.scrollTop;
        sp = sp.parentElement;
    }

    return {
        x: ox - sx,
        y: oy - sy,
    };
}

export function setPosition(element, x, y) {
    if (element) {
        element.style.left = `${x}px`;
        element.style.top = `${y}px`;
    }
}

export function getSize(element) {
    if (element) {
        return {
            width: element.offsetWidth,
            height: element.offsetHeight,
        };
    }
    return null;
}

export function setSize(element, w, h) {
    if (element) {
        element.style.width = `${w}px`;
        element.style.height = `${h}px`;
    }
}

export function getContentSize(element) {
    return element ? {
        width: element.clientWidth,
        height: element.clientHeight,
    } : null;
}

export function getContentPosition(element) {
    return element ? {
        x: element.clientLeft,
        y: element.clientTop,
    } : null;
}