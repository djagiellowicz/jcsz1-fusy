﻿using System;
using System.Globalization;
using System.Linq;
using WalutyBusinessLogic.LoadingFromFile;

namespace WalutyBusinessLogic.CurrenciesComparision
{
    public class CurrenciesComparator
    {
        private readonly ILoader _loader;
        public string FileExtension { get; set; }

        public CurrenciesComparator(ILoader loader)
        {
            _loader = loader;
            FileExtension = ".txt";
        }

        public string CompareCurrencies(string firstCurrencyCode, string secondCurrencyCode, int date)
        {
            DateTime dateFromInt = DateTime.ParseExact(date.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);

            Currency firstCurrency = _loader.LoadCurrencyFromFile(firstCurrencyCode + FileExtension);
            Currency secondCurrency = _loader.LoadCurrencyFromFile(secondCurrencyCode + FileExtension);

            CurrencyRecord firstCurrencyRecord =
                firstCurrency.ListOfRecords.Single(currency => currency.Date == date);
            CurrencyRecord secondCurrencyRecord =
                secondCurrency.ListOfRecords.Single(currency => currency.Date == date);

            float firstCloseValue = firstCurrencyRecord.Close;
            float secondCloseValue = secondCurrencyRecord.Close;

            float comparision = firstCloseValue / secondCloseValue;

            return $"In day {dateFromInt.ToShortDateString()} {firstCurrency.Name} is worth {comparision} {secondCurrency.Name}";
        }
        // ...
        // CompareCurrencies(string firstCurrencyCode, string secondCurrencyCode, int date) usage example:

        //CurrenciesComparator currencies = new CurrenciesComparator();
        //Console.WriteLine(currencies.CompareCurrencies("GBP", "EUR", 20141010));
        //Console.ReadKey();
    }
}
