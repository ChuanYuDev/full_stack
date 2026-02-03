const proto = {};
const obj = Object.create(proto);
console.log(Object.getPrototypeOf(obj));
console.log(Object.getPrototypeOf(obj) === proto); // true

// {}
// true