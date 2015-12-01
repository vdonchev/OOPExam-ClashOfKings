namespace ClashOfKings.Exceptions
{
   public class LocationOutOfRangeException : GameException
    {
        public LocationOutOfRangeException(string message)
            : base(message)
        {
        }
    }
}
