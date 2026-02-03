const myDate = new Date();
let object = myDate;

while (object) {
    console.log(object);
    object = Object.getPrototypeOf(object);
}

console.log(object);

// 2026-02-03T19:17:50.360Z
// {}
// [Object: null prototype] {}
// null