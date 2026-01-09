"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
class Test {
    a = 5;
    b = this.a++;
    c = 6;
    constructor() {
        this.a++;
    }
}
const test = new Test();
console.log(test.a);
console.log(test.b);
const user = {
    id: 2,
    name: "Pedro",
    age: 22,
};
console.log(user.id + user.name + user.age);
const user1 = {
    greet(message) {
        console.log(message);
    }
};
user1.greet("Hello user");
const func = function (message) {
    console.log(message);
};
func("Hello func");
//# sourceMappingURL=tutorial.js.map