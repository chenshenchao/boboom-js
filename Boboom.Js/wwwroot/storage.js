export class BoStorage {
    constructor(sign, storage) {
        this.sign = sign;
        this.storage = storage;
        this.expiredKey = `${sign}__storage_expired_map`;
        let e = this.storage.getItem(this.expiredKey);
        this.expiredMap = e ? JSON.parse(e) : {};
    }

    has(name) {
        let key = `${this.sign}_${name}`;
        let text = this.storage.getItem(key);
        if (!text) {
            return false;
        }
        if (!this.expiredMap.hasOwnProperty(key)) {
            return true;
        }
        let timestamp = this.expiredMap[key];
        let now = new Date().getTime();
        if (now <= timestamp) {
            return true;
        }
        this.storage.removeItem(key);
        delete this.expiredMap[key];
        let e = JSON.stringify(this.expiredMap);
        this.storage.setItem(this.expiredKey, e);
        return false;
    }

    get(name, defaultValue) {
        let key = `${this.sign}_${name}`;
        let text = this.storage.getItem(key);
        if (!text) {
            return defaultValue;
        }

        if (this.expiredMap.hasOwnProperty(key)) {
            let timestamp = this.expiredMap[key];
            let now = new Date().getTime();
            if (now > timestamp) {
                this.storage.removeItem(key);
                delete this.expiredMap[key];
                let e = JSON.stringify(this.expiredMap);
                this.storage.setItem(this.expiredKey, e);
                return defaultValue;
            }
        }
        return text;
    }

    set(name, text, durationMs) {
        let key = `${this.sign}_${name}`;
        if (durationMs) {
            let now = new Date().getTime();
            this.expiredMap[key] = now + durationMs;
            let e = JSON.stringify(this.expiredMap);
            this.storage.setItem(this.expiredKey, e);
        }
        this.storage.setItem(key, text);
    }
}


export const newBoLocalStorage = (sign) => {
    return new BoStorage(sign, localStorage);
}

export const newBoSessionStorage = (sign) => {
    return new BoStorage(sign, sessionStorage);
}