// Show the package where the class belongs to
package com.codewithneal;

import java.awt.*;
import java.text.NumberFormat;
import java.util.Arrays;
import java.util.Date;
import java.util.Scanner;

public class Main {
    public static void main(String[] args) {
        // out is field of System class
        //      Its type is PrintStream
        System.out.println("Hello World");

        Date now = new Date();
        // Type sout shortcut for System.out.println();
        System.out.println(now);

        // Reference type
        Point point1 = new Point(1, 1);
        Point point2 = point1;
        point1.x = 2;
        System.out.println(point2);

        // String
        String message = "  Hello world" + "!!  ";
        System.out.println(message.endsWith("!!"));
        System.out.println(message.indexOf("o"));
        System.out.println(message.indexOf("sky"));
        System.out.println(message.replace("!", "*"));
        System.out.println(message.trim());
        System.out.println(message);

        // Escape sequence
        String message2 = "Hello \"world";
        System.out.println(message2);

        String message3 = "c:\\Windows\\...";
        System.out.println(message3);

        // Arrays class
        int[] numbers = { 4, 2, 7 };
        Arrays.sort(numbers);

        // Method overloading
        //      Multiple methods have the same name but different parameters
        String result = Arrays.toString(numbers);
        System.out.println(result);

        // Multi-dimensional arrays
        int [][] matrix = new int[2][3];
        matrix[0][1] = 1;
        System.out.println(Arrays.toString(matrix));
        System.out.println(Arrays.deepToString(matrix));

        // Arithmetic expressions
        int x = 1;
        int y = x++;
        System.out.println(x);
        System.out.println(y);

        // Formatting numbers
        NumberFormat currency = NumberFormat.getCurrencyInstance();
        result = currency.format(123456); // $123,456
        System.out.println(result);

        NumberFormat percent = NumberFormat.getPercentInstance();
        result = percent.format(0.04); // 4%
        System.out.println(result);

        result = NumberFormat.getPercentInstance().format(0.1);
        System.out.println(result);

        // Reading input
        Scanner scanner = new Scanner(System.in);
        // System.out.print("Age: ");
        // byte age = scanner.nextByte();
        // System.out.println("You are " + age);
        String name = scanner.nextLine().trim();
        System.out.println("You are " + name);
   }
}
