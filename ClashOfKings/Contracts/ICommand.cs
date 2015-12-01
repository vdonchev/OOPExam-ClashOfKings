namespace ClashOfKings.Contracts
{
    public interface ICommand
    {
        IGameEngine Engine { get; }

        void Execute(params string[] commandParams);
    }
}
