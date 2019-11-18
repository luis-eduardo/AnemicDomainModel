using System;
using Newtonsoft.Json;

namespace Logic.Entities
{
    public class Movie : Entity
    {
        public virtual string Name { get; set; }

        public virtual LicensingModel LicensingModel { get; set; }

        public virtual Dollars CalculatePrice(CustomerStatus status)
        {
            decimal discount = status.GetDiscount;
            switch (LicensingModel)
            {
                case LicensingModel.TwoDays:
                    return Dollars.Of(ApplyDiscount(4, discount));

                case LicensingModel.LifeLong:
                    return Dollars.Of(ApplyDiscount(8, discount));

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public virtual ExpirationDate GetExpirationDate()
        {
            switch (LicensingModel)
            {
                case LicensingModel.TwoDays:
                    return (ExpirationDate) DateTime.UtcNow.AddDays(2);

                case LicensingModel.LifeLong:
                    return ExpirationDate.Infinite;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private decimal ApplyDiscount(decimal value, decimal discount)
        {
            return value * (1m - discount);
        }
    }
}
