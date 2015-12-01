namespace ClashOfKings.Models.Commands.Extended
{
    using System;
    using Contracts;
    using Attributes;
    using Exceptions;

    [Command]
    public class UpgradeCityCommand : Command 
    {
        public UpgradeCityCommand(IGameEngine engine)
            : base(engine)
        {
        }

        public override void Execute(params string[] commandParams)
        {
            var cityName = commandParams[0];

            var city = this.Engine.Continent.GetCityByName(cityName);
            if (city == null)
            {
                throw new CityNotFoundException("This city is not presented on the map");
            }

            if (city.CityType == CityType.Capital)
            {
                throw new InvalidOperationException($"City {cityName} is at the maximum level and cannot be upgraded further");
            }

            city.ControllingHouse.UpgradeCity(city);
            this.Engine.Render($"City {cityName} successfully upgraded to {city.CityType}");
        }
    }
}