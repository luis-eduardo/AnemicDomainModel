using System;
using Newtonsoft.Json;

namespace Logic.Entities
{
    public class PurchasedMovie : Entity
    {
        public virtual long MovieId { get; set; }

        public virtual Movie Movie { get; set; }

        public virtual long CustomerId { get; set; }

        private decimal _price;
        public virtual Dollars Price
        {
            get => Dollars.Of(_price);
            set => _price = value;
        }

        public virtual DateTime PurchaseDate { get; set; }

        public virtual DateTime? ExpirationDate { get; set; }
    }
}
