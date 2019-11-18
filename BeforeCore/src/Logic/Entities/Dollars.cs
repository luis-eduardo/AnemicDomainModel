using System;
using CSharpFunctionalExtensions;

namespace Logic.Entities
{
    public class Dollars : ValueObject<Dollars>
    {
        private const int MaximumValue = 1_000_000;

        public decimal Value { get; }

        public bool IsZero => Value == 0;

        private Dollars(decimal value)
        {
            this.Value = value;
        }

        public static Result<Dollars> Create(decimal dollars)
        {
            if (dollars < 0)
                return Result.Failure<Dollars>("Money cannot be negative.");

            if (dollars > MaximumValue)
                return Result.Failure<Dollars>($"Money cannot be greater than {MaximumValue}.");

            if (dollars % 0.01m != 0)
                return Result.Failure<Dollars>("Money cannot contain part of a penny.");

            return Result.Ok(new Dollars(dollars));
        }

        protected override bool EqualsCore(Dollars other)
        {
            return Value == other.Value;
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }

        public static implicit operator decimal(Dollars dollars)
        {
            return dollars.Value;
        }

        public static Dollars operator *(Dollars dollars, decimal multiplier)
        {
            return Of(dollars.Value * multiplier);
        }

        public static Dollars operator +(Dollars addend1, Dollars addend2)
        {
            return Of(addend1.Value + addend2.Value);
        }

        public static Dollars Of(decimal dollars)
        {
            var dollarsOrError = Create(dollars);

            if (dollarsOrError.IsFailure)
                throw new InvalidCastException(dollarsOrError.Error);

            return dollarsOrError.Value;
        }
    }
}