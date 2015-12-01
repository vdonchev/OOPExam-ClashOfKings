namespace ClashOfKings.Models.Commands
{
    using System;
    using System.Collections.Generic;

    using ClashOfKings.Attributes;
    using ClashOfKings.Contracts;

    [Command]
    public class CreateCityCommand : Command
    {
        public CreateCityCommand(IGameEngine engine)
            : base(engine)
        {
        }

        public override void Execute(params string[] commandParams)
        {
            string name = commandParams[0];
            string houseName = commandParams[1];
            int defense = int.Parse(commandParams[2]);
            decimal upgradeCost = decimal.Parse(commandParams[3]);
            double initialFoodStorage = double.Parse(commandParams[4]);
            double foodProduction = double.Parse(commandParams[5]);
            decimal taxBase = decimal.Parse(commandParams[6]);
            string cityType = commandParams[7];

            var controllingHouse = this.Engine.Continent.GetHouseByName(houseName);

            if (controllingHouse == null)
            {
                throw new ArgumentNullException("controllingHouse");
            }

            ICity city;
            if (cityType == "default")
            {
                city = new City(name, controllingHouse, defense, upgradeCost, initialFoodStorage, foodProduction, taxBase);
            }
            else
            {
                var type = (CityType)Enum.Parse(typeof(CityType), cityType);
                city = new City(name, controllingHouse, defense, upgradeCost, initialFoodStorage, foodProduction, taxBase, type);
            }

            this.Engine.Continent.AddCity(city);
            this.Engine.Continent.CityNeighborsAndDistances.Add(city, new Dictionary<ICity, double>());
            controllingHouse.AddCityToHouse(city);

            this.Engine.Render("Successfully created city {0}", city.Name);
        }
    }
}
