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

interface UserInterface {
    greet(message: string): void;
}

interface UserInterface2 {
    greet: (message: string) => void;
}

const user1: UserInterface2 = {
    greet(message) {
        console.log(message);
    }
}

user1.greet("Hello user");

interface FuncInterface {
    (message: string): void;
}
const func: FuncInterface = function (message: string): void {
    console.log(message);
};

func("Hello func");