export class AnchorBox {
    constructor(dn, element) {
        console.log('constructor', dn);
        this.dn = dn;
        this.element = element;
        this.isDrapping = false;
        this.x = 0;
        this.y = 0;
        this.element.addEventListener('mousedown', this.onDrapStart);
        this.element.addEventListener('mousemove', this.onDrapMove);
        this.element.addEventListener('mouseup', this.onDrapEnd);
        this.element.addEventListener('mouseleave', this.onDrapMove);
    }

    dispose() {
        this.element.removeEventListener('mousedown', this.onDrapStart);
        this.element.removeEventListener('mousemove', this.onDrapMove);
        this.element.removeEventListener('mouseup', this.onDrapEnd);
        this.element.removeEventListener('mouseleave', this.onDrapMove);
    }

    onDrapStart = (e) => {
        //console.log(e, this.isDrapping, this);
        this.isDrapping = true;
        this.x = e.x;
        this.y = e.y;
    }

    onDrapMove = async (e) => {
        if (this.isDrapping && this.dn) {
            //console.log(e);
            let ex = this.element.offsetLeft;
            let ey = this.element.offsetTop;
            let x = ex + (e.x - this.x);
            let y = ey + (e.y - this.y);
            this.x = e.x;
            this.y = e.y;
            this.element.style.left = `${x}px`;
            this.element.style.top = `${y}px`;
            await this.dn.invokeMethodAsync('OnMouseMove');
        }
    }

    onDrapEnd = (e) => {
        this.isDrapping = false;
    }
}

export const init = (dn, element) => {
    return new AnchorBox(dn, element);
};