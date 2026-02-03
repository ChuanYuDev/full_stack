const buz = {
    a: 1
};

const foo = Object.create(buz, {
    b: {
        value: 2,
        enumerable: true
    }
});

foo.c = 3;

for (const name in foo) {
    if (foo.hasOwnProperty(name)) {
        console.log(`true: ${name}: ${foo[name]}`);
    } else {
        console.log(`false: ${name}: ${foo[name]}`);
    }
}

// Output:
// true: b: 2
// true: c: 3
// false: a: 1