namespace ClashOfKings.Models.Commands.Extended
{
    using System;
    using System.Linq;
    using Attributes;
    using Contracts;
    using Exceptions;

    [Command]
    public class CreateUnitCommand : Command
    {
        public CreateUnitCommand(IGameEngine engine)
            : base(engine)
        {
        }

        public override void Execute(params string[] commandParams)
        {
            int numberOfUnitsToAdd = int.Parse(commandParams[0]);
            string unitName = commandParams[1];
            string cityName = commandParams[2];

            if (numberOfUnitsToAdd < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(numberOfUnitsToAdd), "Number of units should be non-negative");
            }

            var city = this.Engine.Continent.GetCityByName(cityName);
            var units = this.Engine.UnitFactory.CreateUnits(unitName, numberOfUnitsToAdd);
            var unitType = units.First().Type;

            if (city == null)
            {
                throw new CityNotFoundException("This city is not presented on the map");
            }

            if (city.AvailableUnitCapacity(unitType) < numberOfUnitsToAdd * units.First().HousingSpacesRequired)
            {
                throw new InsufficientHousingSpacesException($"City {city.Name} does not have enough housing spaces to accommodate {numberOfUnitsToAdd} units of {unitName}");
            }

            var totalUnitTrainingCost = units.Sum(u => u.TrainingCost);

            if (city.ControllingHouse.TreasuryAmount < totalUnitTrainingCost)
            {
                throw new InsufficientFundsException($"House {city.ControllingHouse.Name} does not have enough funds to train {numberOfUnitsToAdd} units of {unitName}");
            }

            city.AddUnits(units);
            city.ControllingHouse.TreasuryAmount -= totalUnitTrainingCost;
            this.Engine.Render($"Successfully added {numberOfUnitsToAdd} units of {unitName} to city {cityName}");
        }
    }
}