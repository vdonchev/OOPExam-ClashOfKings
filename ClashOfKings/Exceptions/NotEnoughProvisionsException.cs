namespace ClashOfKings.Exceptions
{
    public class NotEnoughProvisionsException : GameException
    {
        public NotEnoughProvisionsException(string message)
            : base(message)
        {
        }
    }
}
