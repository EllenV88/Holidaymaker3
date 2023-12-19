﻿using Npgsql;
using System;

namespace Holidaymaker3;
public class Menu
{
    private readonly NpgsqlDataSource _db;
    public Menu(NpgsqlDataSource db)
    {
        _db = db;
    }
    public async void MainMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
            Console.WriteLine("|    Welcome to Holidaymaker3!    |\n");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
            Console.WriteLine("\n");
            Console.WriteLine("1 - Customers\n");
            Console.WriteLine("2 - Bookings\n");
            Console.WriteLine("3 - Hotels and room availability\n");
            Console.WriteLine("0 - Exit\n");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CustomerMenu();
                    break;
                case "2":
                    BookingsMenu();
                    break;
                case "3":
                    HotelsAndRoomsMenu();
                    break;
                case "0":
                    return;
                default:
                    Console.Clear();
                    Console.WriteLine("Pick something valid");
                    Console.ReadKey();
                    break;
            }
        }
    }

    public async void CustomerMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
            Console.WriteLine("|     Customer Registration!      |\n");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
            Console.WriteLine("1 - Register new customer\n");
            Console.WriteLine("2 - View customer bookings\n");
            Console.WriteLine("0 - Go back\n");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    RegisterNewCustomer();
                    break;
                case "2":
                    ViewCustomerBookings();
                    break;
                case "0":
                    return;
                default:
                    Console.Clear();
                    Console.WriteLine("Pick something valid");
                    Console.ReadKey();
                    break;
            }
        }
    }

    public async void RegisterNewCustomer()
    {
        Console.Clear();
        Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
        Console.WriteLine("|      Enter a new customer!      |\n");
        Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");

        Console.Write("Enter first name: ");
        string firstName = Console.ReadLine();

        Console.Write("Enter last name: ");
        string lastName = Console.ReadLine();

        Console.Write("Enter email: ");
        string email = Console.ReadLine();

        Console.Write("Enter phone number: ");
        string phoneNumber = Console.ReadLine();

        Console.Write("Enter date of birth (yyyy-mm-dd): ");
        string dateOfBirth= Console.ReadLine();

        // Saves to SQL database

        const string query1 =
           @" SELECT setval('customers_customer_id_seq', (SELECT MAX(customer_id) FROM customers));";

        await using (var cmd = _db.CreateCommand(query1))
        {
            await cmd.ExecuteNonQueryAsync();
        }

            const string query2 = 
           @" INSERT INTO customers (first_name, last_name, email, phone_number, date_of_birth)
           VALUES ($1, $2, $3, $4, $5)";

        await using (var cmd = _db.CreateCommand(query2))
        {
            Console.WriteLine(firstName);
            cmd.Parameters.AddWithValue(firstName);
            cmd.Parameters.AddWithValue(lastName);
            cmd.Parameters.AddWithValue(email);
            cmd.Parameters.AddWithValue(phoneNumber);
            cmd.Parameters.AddWithValue(DateTime.Parse(dateOfBirth));

            await cmd.ExecuteNonQueryAsync();
        }


        Console.Clear();
        Console.WriteLine($"{firstName} {lastName} has been registered as a new customer.\n");
        Console.ReadKey();
    }

    public async void ViewCustomerBookings()
    {
        // Implement logic to view customer bookings
        Console.Clear();
        Console.WriteLine("Viewing customer bookings\n");
        Console.ReadKey();
    }

    public async void BookingsMenu()
    {
        // Implement logic for the Bookings menu
    }

    public async void HotelsAndRoomsMenu()
    {
        // Implement logic for the Hotels and Rooms menu
    }
}