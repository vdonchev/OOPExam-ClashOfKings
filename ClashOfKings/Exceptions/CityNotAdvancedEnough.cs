namespace ClashOfKings.Exceptions
{
    public class CityNotAdvancedEnough : GameException
    {
        public CityNotAdvancedEnough(string message) 
            : base(message)
        {
        }
    }
}