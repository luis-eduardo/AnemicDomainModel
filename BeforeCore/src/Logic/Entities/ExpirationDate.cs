using System;
using CSharpFunctionalExtensions;

namespace Logic.Entities
{
    public class ExpirationDate : ValueObject<ExpirationDate>
    {
        public static readonly ExpirationDate Infinite = new ExpirationDate(null);
        public DateTime? Date { get; }

        public bool IsExpired => this != Infinite && Date < DateTime.UtcNow;

        private ExpirationDate(DateTime? date)
        {
            this.Date = date;
        }

        public static Result<ExpirationDate> Create(DateTime date)
        {
            return Result.Ok(new ExpirationDate(date));
        }

        public static implicit operator DateTime?(ExpirationDate date)
        {
            return date.Date;
        }

        public static explicit operator ExpirationDate(DateTime? date)
        {
            if (!date.HasValue)
            {
                return Infinite;
            }

            var dateOrError = Create(date.Value);

            if (dateOrError.IsFailure)
                throw new InvalidCastException(dateOrError.Error);

            return dateOrError.Value;
        }

        protected override bool EqualsCore(ExpirationDate other)
        {
            return Date == other.Date;
        }

        protected override int GetHashCodeCore()
        {
            return Date.GetHashCode();
        }
    }
}