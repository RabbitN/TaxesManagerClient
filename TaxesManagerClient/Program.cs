using Application;
using Domain.Dto;
using RestSharp;
using System;
using System.IO;

namespace TaxesManagerClient
{
    class Program
    {
        private static RestClient client = new RestClient("https://localhost:5001/api/Taxes/");
        static void Main(string[] args)
        {
            TaxCommands taxCommands = new TaxCommands(client);

            // usage of import functionality
            taxCommands.ImportTaxesFromFile(Path.Combine(Directory.GetCurrentDirectory(), "TestImport.json"));

            // usage of insert with a schedule functionality
            taxCommands.PostTaxYearly(new TaxDto { Municipality = "Klaipeda", TaxAmount = 0.51 });
            taxCommands.PostTaxMonthly(new TaxDto { Municipality = "Klaipeda", Month = 78, TaxAmount = 0.2 });
            taxCommands.PostTaxWeekly(new TaxDto { Municipality = "Klaipeda", Week = 7, TaxAmount = 0.4 });
            taxCommands.PostTaxDaily(new TaxDto { Municipality = "Klaipeda", Month = 10, Day = 14, TaxAmount = 0.05 });
            taxCommands.PostTaxMonthly(new TaxDto { Municipality = "Elektrenai", Month = 8, TaxAmount = 0.2 });

            // usage of get specific municipality
            taxCommands.GetTaxAmount("Elektrenai", "2020-01-01");

            // usage of update functionality
            taxCommands.UpdateTax("Vilnius", "2020-07-07", "2020-07-07", 0.16);
            taxCommands.UpdateTax("Vilnius", "2020-06-01", "2020-06-30", 0.5);
            taxCommands.UpdateTax("Vilnius", "2020-01-01", "2020-12-28", 0.5);
            taxCommands.UpdateTax("Vilnius", "2020-01-01", "2020-01-01", 0.5);
            taxCommands.UpdateTax("Vilnius", "2020-07-07", "2020-07-07", 0.16);

            // other
            taxCommands.GetAllTaxes();
            taxCommands.GetTax("Druskininkai", "2020-06-01", "2020-06-30");           
            taxCommands.DeleteTax("Vilnius", "2020-07-07", "2020-07-31");

            Console.ReadKey();
        }
    }
}
