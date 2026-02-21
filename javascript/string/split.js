const myString = "Hello 1 word. Sentence number 2.";

const splits = myString.split(/(\d)/);
console.log(splits);
// Output: [ "Hello ", "1", " word. Sentence number ", "2", "." ]

const splits2 = myString.split(/\d/);
console.log(splits2);
// Output: [ 'Hello ', ' word. Sentence number ', '.' ]
