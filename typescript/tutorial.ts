class Test {
    a: number = 5;
    b: number = this.a++;
    
    constructor() {
        this.a++;
    }
    
}

const test = new Test();
console.log(test.a);
console.log(test.b);
