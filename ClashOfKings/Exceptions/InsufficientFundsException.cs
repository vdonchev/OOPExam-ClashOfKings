namespace ClashOfKings.Exceptions
{
    public class InsufficientFundsException : GameException
    {
        public InsufficientFundsException(string message)
            : base(message)
        {
        }
    }
}
