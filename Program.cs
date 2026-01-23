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
        static void Main(string[] args)
        {
            List<Currency> Currencies = new List<Currency>();
            Dictionary<string, Currency> CurrencyDict = new Dictionary<string, Currency>();

            void AddCurrency(Currency c)
            {
                Currencies.Add(c);
                CurrencyDict[c.Type] = c;
            }

            AddCurrency(new Currency("USD", 1.2));
            AddCurrency(new Currency("EUR", 1.0));
            AddCurrency(new Currency("RUB", 0.011));

            foreach (Currency currency in Currencies)
            {
                Console.WriteLine(currency.Type + ": " + currency.Value);
            }

            if (CurrencyDict.ContainsKey("EUR"))
            {
                CurrencyDict["EUR"].Value = 1.1;
            }

            for (int i = 0; i < Currencies.Count; i++)
            {
                if (Currencies[i].Type == "RUB")
                {
                    CurrencyDict.Remove(Currencies[i].Type);
                    Currencies.RemoveAt(i);
                    Console.WriteLine("RUB removed");
                    break;
                }
            }

            foreach (Currency currency in Currencies)
            {
                Console.WriteLine(currency.Type + ": " + currency.Value);
            }

            string json = JsonSerializer.Serialize(Currencies);
            File.WriteAllText("Currencies.json", json);
        }
    }
}