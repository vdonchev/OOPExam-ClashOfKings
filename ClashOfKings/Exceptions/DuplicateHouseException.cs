namespace ClashOfKings.Exceptions
{
    public class DuplicateHouseException : GameException
    {
        public DuplicateHouseException(string message)
            : base(message)
        {
        }
    }
}
