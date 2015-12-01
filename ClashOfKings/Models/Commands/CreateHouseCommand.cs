namespace ClashOfKings.Models.Commands
{
    using ClashOfKings.Attributes;
    using ClashOfKings.Contracts;

    [Command]
    public class CreateHouseCommand : Command
    {
        public CreateHouseCommand(IGameEngine engine)
            : base(engine)
        {
        }

        public override void Execute(params string[] commandParams)
        {
            string houseName = commandParams[0];
            decimal initialTreasuryAmount = decimal.Parse(commandParams[1]);

            IHouse house = new House(houseName, initialTreasuryAmount);

            this.Engine.Continent.AddHouse(house);

            this.Engine.Render("Successfully created House {0}", house.Name);
        }
    }
}
