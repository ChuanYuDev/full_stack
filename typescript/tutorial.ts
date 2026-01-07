class Test {
    a: number = 5;
    b: number = this.a++;
    private c = 6;
    
    constructor() {
        this.a++;
    }
    
}

const test = new Test();
console.log(test.a);
console.log(test.b);

const user: {
    id: number; name: string; age: number;
} = {
    id: 2,
    name: "Pedro",
    age: 22,
};
console.log(user.id + user.name + user.age);