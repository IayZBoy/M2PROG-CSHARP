using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;

namespace ConsoleApp
{
    class Currency
    {
        public string Type { get; set; } = "EUR";
        public double Value { get; set; } = 1.0;

        internal Currency(string Type, double Value)
        {
            this.Type = Type;
            this.Value = Value;
        }
    }

    class Program
    {
        private List<Currency> Currencies = new List<Currency>();
        private Dictionary<string, Currency> CurrencyDict = new Dictionary<string, Currency>();

        internal void AddCurrency(Currency c)
        {
            Currencies.Add(c);
            CurrencyDict.Add(c.Type, c);
            Console.WriteLine(c.Type + " added");
        }

        internal void RemoveCurrency(Currency c)
        {
            Currencies.Remove(c);
            CurrencyDict.Remove(c.Type);
            Console.WriteLine(c.Type + " removed");
        }

        static void Main(string[] args)
        {
            Program program = new Program();

            program.AddCurrency(new Currency("USD", 1.2));
            program.AddCurrency(new Currency("EUR", 1.0));
            program.AddCurrency(new Currency("RUB", 0.011));

            foreach (Currency currency in program.Currencies)
            {
                Console.WriteLine(currency.Type + ": " + currency.Value);
            }

            if (program.CurrencyDict.ContainsKey("EUR"))
            {
                program.CurrencyDict["EUR"].Value = 1.1;
            }

            for (int i = 0; i < program.Currencies.Count; i++)
            {
                if (program.Currencies[i].Type == "RUB")
                {
                    program.RemoveCurrency(program.Currencies[i]);
                    break;
                }
            }

            foreach (Currency currency in program.Currencies)
            {
                Console.WriteLine(currency.Type + ": " + currency.Value);
            }

            string json = JsonSerializer.Serialize(program.Currencies);
            File.WriteAllText("Currencies.json", json);
        }
    }
}