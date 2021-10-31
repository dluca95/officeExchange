using System;
using ExchangeOffice.Application.Models;

namespace ExchangeOffice.Application
{
    public static class CurrencyHelper
    {
        public static bool IsExchangeRateValid(ExchangeRateModel exchangeRateModel, CurrencyApiModel currencyApiModel)
        {
            return exchangeRateModel.BuyPrice < (currencyApiModel.Value + (currencyApiModel.Value / 100))
                   || exchangeRateModel.BuyPrice > (currencyApiModel.Value - (currencyApiModel.Value / 100))
                   || exchangeRateModel.SellPrice < (currencyApiModel.Value + (currencyApiModel.Value / 100))
                   || exchangeRateModel.SellPrice > (currencyApiModel.Value) - (currencyApiModel.Value / 100);
        }
    }
}