namespace ClashOfKings
{
    using ClashOfKings.Contracts;
    using ClashOfKings.Engine;
    using ClashOfKings.Engine.Factories;
    using ClashOfKings.UI;

    public class ClashOfKingsMain
    {
        public static void Main()
        {
            IInputController inputController = new ConsoleInputController();
            IRenderer renderer = new ConsoleRenderer();

            IUnitFactory unitFactory = new UnitFactory();
            IArmyStructureFactory armyStructureFactory = new ArmyStructureFactory();
            ICommandFactory commandFactory = new CommandFactory();

            IContinent westeros = new WesterosExtended();

            IGameEngine engine = new WarEngine(
                renderer, 
                inputController, 
                unitFactory, 
                armyStructureFactory, 
                commandFactory, 
                westeros);

            engine.Run();
        }
    }
}