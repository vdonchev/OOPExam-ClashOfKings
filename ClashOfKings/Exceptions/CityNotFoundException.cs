namespace ClashOfKings.Exceptions
{
    public class CityNotFoundException : GameException
    {
        public CityNotFoundException(string message) 
            : base(message)
        {
        }
    }
}