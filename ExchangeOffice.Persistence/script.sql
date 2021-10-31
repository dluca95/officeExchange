SELECT MIN(SellPrice) as minSellPrice, Max(SellPrice) as maxSellPrice,
       MIN(BuyPrice) as minBuyPrice, Max(BuyPrice) as maxBuyPrice,
       CurrencyId
FROM CurrencyExchangeRates as exchange
         INNER JOIN Currencies as currency
                    on exchange.CurrencyId = currency.Id
Where currency.CharCode = "USD" AND
    exchange.CreatedAt BETWEEN 	'2021/10/20' and  '2021/11/01'
Group By CurrencyId 

