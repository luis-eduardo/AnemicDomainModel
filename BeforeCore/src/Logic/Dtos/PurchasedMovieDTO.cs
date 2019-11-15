using System;

namespace Logic.Dtos
{
    public class PurchasedMovieDTO
    {
        public long Id { get; set; }

        public MovieDTO Movie { get; set; }

        public decimal Price { get; set; }

        public DateTime PurchaseDate { get; set; }

        public DateTime? ExpirationDate { get; set; }
    }
}