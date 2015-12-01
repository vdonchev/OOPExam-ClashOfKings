namespace ClashOfKings.Exceptions
{
    public class DuplicateCityException : GameException
    {
        public DuplicateCityException(string message)
            : base(message)
        {
        }
    }
}
