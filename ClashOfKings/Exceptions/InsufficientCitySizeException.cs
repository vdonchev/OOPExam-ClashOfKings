namespace ClashOfKings.Exceptions
{
    public class InsufficientCitySizeException : GameException
    {
        public InsufficientCitySizeException(string message)
            : base(message)
        {
        }
    }
}
