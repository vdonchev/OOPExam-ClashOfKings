namespace ClashOfKings.Models.Commands
{
    using ClashOfKings.Contracts;

    public abstract class Command : ICommand
    {
        protected Command(IGameEngine engine)
        {
            this.Engine = engine;
        }

        public IGameEngine Engine { get; private set; }

        public abstract void Execute(params string[] commandParams);
    }
}
