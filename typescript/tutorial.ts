type UserType = {
    id: number;
    name: string;
    age: number;
};

const User: UserType = {
    id: 2,
    name: "Pedro",
    age: 22
};

console.log(User.id + User.name);