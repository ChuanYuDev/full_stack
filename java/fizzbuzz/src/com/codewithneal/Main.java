package com.codewithneal;

import java.util.Scanner;

public class Main {
    public static void main(String[] args) {
        System.out.print("Number: ");
        Scanner scanner = new Scanner(System.in);
        int number = scanner.nextInt();

        // If number is divided by 5, print fizz
        // If number is divided by 3, print buzz
        // If number is divided by 5 and 3, print fizzbuzz


        String output = "";

        if (number % 5 == 0)
            output += "fizz";

        if (number % 3 == 0)
            output += "buzz";

        if (output.isEmpty())
            System.out.println(number);
        else
            System.out.println(output);
    }
}