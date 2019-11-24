using Microsoft.Azure.Management.Sql.Fluent.Models;
using System;
using System.Threading.Tasks;

namespace AzureSQLDBCreator
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Initializing Connection...");
            var sqlManager = new AzureSqlManager("CloudDev", "surpasscloud");
            Console.WriteLine("Connection setup successfully.");
            Console.WriteLine("Enter Database name...");
            var databaseName = Console.ReadLine();
            Console.WriteLine($"Create Database {databaseName} in East US region...");
            try
            {
                var startTime = DateTime.UtcNow;
                var status = await sqlManager.CreateDatabaseAsync(databaseName, "[Elastic_Pool]", "East US", CreateMode.Default, DatabaseEdition.GeneralPurpose);
                var totalSecondElapsed = DateTime.UtcNow.Subtract(startTime).TotalSeconds;

                if (status)
                    Console.WriteLine("Database created successfully.");
                else
                    Console.WriteLine($"Failed to create database.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to create database.");
                Console.WriteLine($"Message: {e.Message}");
            }

            Console.ReadKey();
        }
    }
}
