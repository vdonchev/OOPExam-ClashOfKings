namespace ClashOfKings.Engine
{
    using System;
    using System.Linq;

    using ClashOfKings.Contracts;
    using ClashOfKings.Exceptions;

    public class WarEngine : IGameEngine
    {
        private readonly IRenderer renderer;
        private readonly IInputController inputController;

        public WarEngine(
            IRenderer renderer, 
            IInputController inputController, 
            IUnitFactory unitFactory,
            IArmyStructureFactory armyStructureFactory,
            ICommandFactory commandFactory,
            IContinent continent)
        {
            this.renderer = renderer;
            this.inputController = inputController;
            this.UnitFactory = unitFactory;
            this.ArmyStructureFactory = armyStructureFactory;
            this.CommandFactory = commandFactory;
            this.Continent = continent;
        }

        public IUnitFactory UnitFactory { get; private set; }

        public IArmyStructureFactory ArmyStructureFactory { get; private set; }

        public ICommandFactory CommandFactory { get; private set; }

        public IContinent Continent { get; private set; }

        public virtual void Run()
        {
            while (true)
            {
                string commandInput = this.inputController.ReadInput();

                try
                {
                    this.ExecuteCommand(commandInput);
                    this.Continent.Update();
                }
                catch (GameException ex)
                {
                    this.Render(ex.Message);
                }
                catch (Exception ex)
                {
                    // Keep game running until exit command is entered
                    this.Render(ex.Message);
                }
            }
        }

        public virtual void ExecuteCommand(string commandInput)
        {
            const string CommandSuffix = "Command";

            var commandInfo = commandInput.Split();

            var commandName = commandInfo[0].Replace("-", string.Empty) + CommandSuffix;
            var commandParams = commandInfo.Skip(1).ToArray();

            var command = this.CommandFactory.CreateCommand(commandName, this);

            command.Execute(commandParams);
        }

        public void Render(string message, params object[] parameters)
        {
            this.renderer.Print(message, parameters);
        }
    }
}
