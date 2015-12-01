namespace ClashOfKings.Models.Commands
{
    using System;

    using ClashOfKings.Attributes;
    using ClashOfKings.Contracts;

    [Command]
    public class ExitCommand : Command
    {
        public ExitCommand(IGameEngine engine)
            : base(engine)
        {
        }

        public override void Execute(params string[] commandParams)
        {
            this.Engine.Render("For the Watch!");

            Environment.Exit(0);
        }
    }
}
