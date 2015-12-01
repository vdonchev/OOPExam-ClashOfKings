namespace ClashOfKings.Models.Commands
{
    using System;

    using ClashOfKings.Attributes;
    using ClashOfKings.Contracts;

    [Command]
    public class HouseStatusCommand : Command
    {
        public HouseStatusCommand(IGameEngine engine)
            : base(engine)
        {
        }

        public override void Execute(params string[] commandParams)
        {
            string houseName = commandParams[0];

            var house = this.Engine.Continent.GetHouseByName(houseName);

            if (house == null)
            {
                throw new ArgumentNullException("house");
            }

            this.Engine.Render(house.Print());
        }
    }
}
