namespace ClashOfKings.Exceptions
{
    public class InsufficientHousingSpacesException : GameException
    {
        public InsufficientHousingSpacesException(string message)
            : base(message)
        {
        }
    }
}
