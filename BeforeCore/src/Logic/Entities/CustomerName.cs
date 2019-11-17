using System;
using CSharpFunctionalExtensions;

namespace Logic.Entities
{
    public class CustomerName : ValueObject<CustomerName>
    {
        public string Value { get; }

        private CustomerName(string value)
        {
            this.Value = value;
        }

        public static Result<CustomerName> Create(string customerName)
        {
            customerName = (customerName ?? string.Empty).Trim();

            if (customerName.Length == 0)
                return Result.Failure<CustomerName>("Customer name should not be empty");

            if (customerName.Length > 100)
                return Result.Failure<CustomerName>("Name is too long");

            return Result.Ok(new CustomerName(customerName));
        }

        protected override bool EqualsCore(CustomerName other)
        {
            return Value.Equals(other.Value, System.StringComparison.InvariantCultureIgnoreCase);
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }

        public static implicit operator string(CustomerName customerName)
        {
            return customerName.Value;
        }

        public static explicit operator CustomerName(string customerName)
        {
            var customerNameOrError = Create(customerName);

            if (customerNameOrError.IsFailure)
                throw new InvalidCastException(customerNameOrError.Error);

            return customerNameOrError.Value;
        }
    }
}