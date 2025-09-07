package com.codewithneal;

import java.text.NumberFormat;
import java.util.Scanner;

public class Main {
    public static void main(String[] args) {
//        Principal: 100000
//        Annual Interest Rate: 3.92
//        Period (Years): 30
//        Mortgage: $472.81
        final int MIN_PRINCIPAL = 1000;
        final int MAX_PRINCIPAL = 1_000_000;
        final float MIN_ANNUAL_INTEREST_RATE = 0;
        final float MAX_ANNUAL_INTEREST_RATE = 30;
        final byte MIN_PERIOD_YEAR = 1;
        final byte MAX_PERIOD_YEAR = 30;

        final byte MONTHS_NUM = 12;
        final int HUNDRED_PERCENT = 100;

        int principal = 0;
        float annualInterestRate = 0;
        byte periodYear = 0;

        Scanner scanner = new Scanner(System.in);
        NumberFormat integerFormatter = NumberFormat.getIntegerInstance();

        while (true) {
            System.out.print("Principal ($1K - $1M): ");
            principal = scanner.nextInt();
            if (principal >= MIN_PRINCIPAL && principal <= MAX_PRINCIPAL)
                break;

            System.out.println("Enter a number between " + integerFormatter.format(MIN_PRINCIPAL) + " and " + integerFormatter.format(MAX_PRINCIPAL) + ".");
        }

        while (true) {
            System.out.print("Annual Interest Rate: ");
            annualInterestRate = scanner.nextFloat();

            if (annualInterestRate > 0 && annualInterestRate <= 30)
                break;

            System.out.println("Enter a value greater than " + MIN_ANNUAL_INTEREST_RATE + " and less than or equal to " + MAX_ANNUAL_INTEREST_RATE + ".");
        }

        while (true) {
            System.out.print("Period (Years): ");
            periodYear = scanner.nextByte();

            if (periodYear >= MIN_PERIOD_YEAR && periodYear <= MAX_PERIOD_YEAR)
                break;

            System.out.println("Enter a value between " + MIN_PERIOD_YEAR + " and " + MAX_PERIOD_YEAR + ".");
        }

        float monthlyInterestRate = annualInterestRate / HUNDRED_PERCENT / MONTHS_NUM;
        int periodMonth = periodYear * MONTHS_NUM;

        double rPlus1PowerN = Math.pow(1 + monthlyInterestRate, periodMonth);
        double mortgage = principal * monthlyInterestRate * rPlus1PowerN / (rPlus1PowerN - 1);

        NumberFormat currency = NumberFormat.getCurrencyInstance();
        String mortgageString = currency.format(mortgage);

        System.out.println("Mortgage: " + mortgageString);
    }
}
