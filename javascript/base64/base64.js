const encodedData = window.btoa("Hello, world"); // encode a string
console.log(encodedData);
// Output: SGVsbG8sIHdvcmxk
const decodedData = window.atob(encodedData); // decode the string
console.log(decodedData);
// Output: Hello, world
