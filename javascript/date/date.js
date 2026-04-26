const date1 = new Date("December 17, 1995 03:24:00");
console.log(date1);
// Output: 1995-12-17T11:24:00.000Z

const date2 = new Date("1995-12-17T03:24:00.000");
console.log(date2);
// Output: 1995-12-17T11:24:00.000Z

const dateString = date2.toString();
console.log(dateString);
// Output: Sun Dec 17 1995 03:24:00 GMT-0800 (Pacific Standard Time)

const date3 = new Date("Sun Dec 17 1995 03:24:00 GMT-0800 (Pacific Standard Time)");
console.log(date3);
// Output: 1995-12-17T11:24:00.000Z

