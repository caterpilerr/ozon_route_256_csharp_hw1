using System;

using MainProject.Contracts.Entities;
using MainProject.Contracts.Entities.ValueObjects;

namespace MainProject.Services
{
    internal static class MainPageManager
    {
        private static readonly SaleInfo BestSaleInfo = new()
        {
            Rating = 10,
            IsActive = true,
            IsBestSeller = true
        };

        private static readonly SaleInfo TrashSaleInfo = new()
        {
            Rating = 0,
            IsActive = false,
            IsBestSeller = false
        };

        public static bool IsForMainPage(Item item)
        {
            if (item.SaleInfo.Equals(TrashSaleInfo))
            {
                return false;
            }

            if (item.Sellers.Count == 0)
            {
                return false;
            }

            return item.SaleInfo.Equals(BestSaleInfo) && CheckMinThreshold(item.Price);
        }

        private static bool CheckMinThreshold(Price price)
        {
            return price.Currency switch
            {
                "RUB" => price.Value > 1000,
                "EUR" => price.Value > 10,
                "USD" => price.Value > 10,
                _=> throw new Exception("Unknown type")
            };
        }
    }
}