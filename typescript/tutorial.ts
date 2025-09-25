interface Employee {
    readonly employeeId: number;
    readonly startDate: Date;
    
    name: string;
    department: string;
}

const employee: Employee = {
    employeeId: 123,   
    startDate: new Date(),
    name: "Pedro",
    department: "Finance",
};

employee.name = "Jessica";
console.log(employee);

// const movies: any[] = [];

function logic (movies?: any[]){
    movies = [];
    if (true && !movies)
        console.log("All true");
}

logic();