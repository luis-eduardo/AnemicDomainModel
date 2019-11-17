using System;
using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;

namespace Logic.Entities
{
    public class Email : ValueObject<Email>
    {
        public string Value { get; }

        private Email(string value)
        {
            this.Value = value;
        }

        public static Result<Email> Create(string email)
        {
            email = (email ?? string.Empty).Trim();

            if (email.Length == 0)
                return Result.Failure<Email>("Email should not be empty");

            if (!Regex.IsMatch(email, @"^(.+)@(.+)$"))
                return Result.Failure<Email>("Email is invalid");

            return Result.Ok(new Email(email));
        }

        protected override bool EqualsCore(Email other)
        {
            return Value.Equals(other.Value, System.StringComparison.InvariantCultureIgnoreCase);
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }

        public static implicit operator string(Email email)
        {
            return email.Value;
        }

        public static explicit operator Email(string email)
        {
            var emailOrError = Create(email);

            if (emailOrError.IsFailure)
                throw new InvalidCastException(emailOrError.Error);

            return emailOrError.Value;
        }
    }
}