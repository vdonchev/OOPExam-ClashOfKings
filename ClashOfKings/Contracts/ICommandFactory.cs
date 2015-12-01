namespace ClashOfKings.Contracts
{
    public interface ICommandFactory
    {
        ICommand CreateCommand(string commandName, IGameEngine engine);
    }
}
