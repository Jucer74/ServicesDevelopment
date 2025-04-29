using System;

namespace Pricat.Application.Exceptions
{
    [Serializable]
    public class BadRequestException : Exception
    {
        public BadRequestException()
        {
        }

        public BadRequestException(string message) : base(message)
        {
        }

        public BadRequestException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        // Factory methods para claridad semántica
        public static BadRequestException InvalidAccountNumber() =>
            new BadRequestException("Account number must be exactly 10 digits.");

        public static BadRequestException InvalidAccountType() =>
            new BadRequestException("Account type must be either 'checking' or 'saving'.");

        public static BadRequestException InvalidOwnerName() =>
            new BadRequestException("Owner name must not exceed 50 characters.");

        public static BadRequestException InvalidInitialBalance() =>
            new BadRequestException("Initial balance must be greater than or equal to 0.");

        public static BadRequestException InvalidTransactionAmount() =>
            new BadRequestException("Transaction amount must be greater than 0.");

        public static BadRequestException InvalidTransactionType() =>
            new BadRequestException("Transaction type must be 'deposit' or 'withdrawal'.");

        public static BadRequestException OverdraftLimitExceeded() =>
            new BadRequestException("Overdraft limit exceeded for 'checking' account.");

        public static BadRequestException InsufficientFunds() =>
            new BadRequestException("Insufficient funds for 'saving' account.");
    }
}