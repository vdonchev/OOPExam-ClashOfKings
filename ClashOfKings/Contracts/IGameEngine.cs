namespace ClashOfKings.Contracts
{
    public interface IGameEngine
    {
        IUnitFactory UnitFactory { get; }

        IArmyStructureFactory ArmyStructureFactory { get; }

        ICommandFactory CommandFactory { get; }

        IContinent Continent { get; }

        void Run();

        void ExecuteCommand(string commandInput);

        void Render(string message, params object[] parameters);
    }
}
