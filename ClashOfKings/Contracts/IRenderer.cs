namespace ClashOfKings.Contracts
{
    public interface IRenderer
    {
        void Print(string message, params object[] parameters);
    }
}
