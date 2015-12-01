namespace ClashOfKings.Models.Commands.Extended
{
    using Attributes;
    using Contracts;
    using Exceptions;

    [Command]
    public class BuildStructureCommand : Command
    {
        public BuildStructureCommand(IGameEngine engine)
            : base(engine)
        {
        }

        public override void Execute(params string[] commandParams)
        {
            string structureName = commandParams[0];
            string cityName = commandParams[1];

            var city = this.Engine.Continent.GetCityByName(cityName);
            var structure = this.Engine.ArmyStructureFactory.CreateStructure(structureName);

            if (city == null)
            {
                throw new CityNotFoundException("This city is not presented on the map");
            }

            if (city.CityType < structure.RequiredCityType)
            {
                throw new CityNotAdvancedEnough("Structure requires a more advanced city");
            }

            if (city.ControllingHouse.TreasuryAmount < structure.BuildCost)
            {
                throw new InsufficientFundsException(
                    string.Format($"House {city.ControllingHouse.Name} doesn't have sufficient funds to build {structure.GetType().Name}"));
            }

            city.ControllingHouse.TreasuryAmount -= structure.BuildCost;
            city.AddArmyStructure(structure);
            this.Engine.Render($"Successfully built {structureName} in {cityName}");
        }
    }
}