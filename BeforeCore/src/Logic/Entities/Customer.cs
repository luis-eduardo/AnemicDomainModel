using System;
using System.Collections.Generic;
using System.Linq;

namespace Logic.Entities
{
    public class Customer : Entity
    {
        private string _name;
        public virtual CustomerName Name
        {
            get => (CustomerName) _name;
            set => _name = value;
        }

        private string _email;
        public virtual Email Email
        {
            get => (Email) _email;
            protected set => _email = value;
        }

        public virtual CustomerStatus Status { get; set; }

        private decimal _moneySpent;
        public virtual Dollars MoneySpent
        {
            get => Dollars.Of(_moneySpent);
            protected set => _moneySpent = value;
        }

        private IList<PurchasedMovie> _purchasedMovies;
        public virtual IReadOnlyList<PurchasedMovie> PurchasedMovies => _purchasedMovies.ToList();

        protected Customer()
        {
            _purchasedMovies = new List<PurchasedMovie>();
        }

        public Customer(CustomerName name, Email email) : this()
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Email = email ?? throw new ArgumentNullException(nameof(email));

            MoneySpent = Dollars.Of(0);
            Status = CustomerStatus.Regular;
        }

        public virtual void PurchaseMovie(Movie movie)
        {
            var expirationDate = movie.GetExpirationDate();
            var price = movie.CalculatePrice(Status);

            var purchasedMovie = new PurchasedMovie(movie, this, price, expirationDate);
            _purchasedMovies.Add(purchasedMovie);
            
            MoneySpent += price;
        }

        public virtual bool Promote()
        {
            if (PurchasedMovies.Count(x => x.IsActiveLast30Days) < 2)
                return false;

            // at least 100 dollars spent during the last year
            if (PurchasedMovies.Where(x => x.IsPurchasedThisYear).Sum(x => x.Price) < 100m)
                return false;

            Status = Status.Promote();

            return true;
        }
    }
}
