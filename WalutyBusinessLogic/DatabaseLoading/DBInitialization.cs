﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalutyBusinessLogic.LoadingFromFile;
using WalutyBusinessLogic.LoadingFromFile.DatabaseLoading;

namespace WalutyBusinessLogic.DatabaseLoading
{
    public static class DBInitialization
    {
        public static void InitialiseDB(WalutyDBContext context, ILoader loader)
        {
            if (!context.Currencies.Any() && !context.CurrencyInfos.Any())
            {
            context.AddRange(loader.GetListOfAllCurrencies());
            context.AddRange(loader.LoadCurrencyInformation());
            context.SaveChanges();
            }
        }
    }
}
