namespace ClashOfKings.Models.Commands
{
    using System;
    using System.Linq;

    using ClashOfKings.Attributes;
    using ClashOfKings.Contracts;
    using ClashOfKings.Exceptions;
    using ClashOfKings.Models.Armies;

    [Command]
    public class MoveCommand : Command
    {
        public MoveCommand(IGameEngine engine)
            : base(engine)
        {
        }

        public override void Execute(params string[] commandParams)
        {
            string startingCityName = commandParams[0];
            string destinationName = commandParams[1];

            ICity startingCity = this.Engine.Continent.GetCityByName(startingCityName);
            ICity destinationCity = this.Engine.Continent.GetCityByName(destinationName);

            this.Engine.Continent.VerifyTroopMovement(startingCity, destinationCity);

            if (startingCity.ControllingHouse != destinationCity.ControllingHouse)
            {
                throw new InvalidOperationException("Cannot move troops between cities controlled by different Houses");
            }

            var canArmyMove = VerifyDestinationHousingSpaces(destinationCity, startingCity);

            if (!canArmyMove)
            {
                throw new InsufficientHousingSpacesException("Destination city has insufficient housing spaces");
            }

            startingCity.FoodStorage -= this.Engine.Continent.CityNeighborsAndDistances[startingCity][destinationCity];
            var unitsToMove = startingCity.RemoveUnits();
            destinationCity.AddUnits(unitsToMove);

            this.Engine.Render("Successfully moved all units from {0} to {1}", startingCity.Name, destinationCity.Name);
        }

        private static bool VerifyDestinationHousingSpaces(ICity destinationCity, ICity startingCity)
        {
            bool infantryCanMove = destinationCity.AvailableUnitCapacity(UnitType.Infantry)
                                   >= startingCity.AvailableMilitaryUnits.Where(u => u.Type == UnitType.Infantry).Sum(u => u.HousingSpacesRequired);

            bool cavalryCanMove = destinationCity.AvailableUnitCapacity(UnitType.Cavalry)
                                  >= startingCity.AvailableMilitaryUnits.Where(u => u.Type == UnitType.Cavalry).Sum(u => u.HousingSpacesRequired);

            bool airforceCanMove = destinationCity.AvailableUnitCapacity(UnitType.AirForce)
                                   >= startingCity.AvailableMilitaryUnits.Where(u => u.Type == UnitType.AirForce).Sum(u => u.HousingSpacesRequired);

            bool canArmyMove = infantryCanMove && cavalryCanMove && airforceCanMove;

            return canArmyMove;
        }
    }
}