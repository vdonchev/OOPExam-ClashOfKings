namespace ClashOfKings.Models.Commands
{
    using ClashOfKings.Attributes;
    using ClashOfKings.Contracts;

    [Command]
    public class ContinentStatusCommand : Command
    {
        public ContinentStatusCommand(IGameEngine engine)
            : base(engine)
        {
        }

        public override void Execute(params string[] commandParams)
        {
           this.Engine.Render(this.Engine.Continent.Print());
        }
    }
}
