const paragraph = "I think Ruth's dog is cuter than your dog!";

const searchTerm = "dog";
const indexOfFirst = paragraph.indexOf(searchTerm);

console.log(`The index of the first "${searchTerm}" is ${indexOfFirst}`);

const searchTerm2 = "";
console.log(`The index of the first "${searchTerm2}" is ${paragraph.indexOf(searchTerm2)}`);
