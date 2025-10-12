"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
class Test {
    a = 5;
    b = this.a++;
    constructor() {
        this.a++;
    }
}
const test = new Test();
console.log(test.a);
console.log(test.b);
//# sourceMappingURL=tutorial.js.map