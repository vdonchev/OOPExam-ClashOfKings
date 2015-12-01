namespace ClashOfKings.Engine
{
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Models;

    public class WesterosExtended : Westeros
    {
        private const int MinCitiesRequiredForGreat = 10;
        private const int MinCitiesRequiredToReamainGreat = 5;

        public override void Update()
        {
            base.Update();

            this.AddGreatHouse();

            this.RemoveGreatHouse();
        }

        private void RemoveGreatHouse()
        {
            var houseToDowngradeToHouse = this.Houses
                .Where(h => h.ControlledCities.Count() < MinCitiesRequiredToReamainGreat && h is GreatHouse).ToList();

            foreach (var house in houseToDowngradeToHouse)
            {
                var houseToAdd = new House(house.Name, house.TreasuryAmount);
                foreach (var city in house.ControlledCities)
                {
                    houseToAdd.AddCityToHouse(city);
                }

                this.Houses.Remove(house);
                this.Houses.Add(houseToAdd);
            }
        }

        private void AddGreatHouse()
        {
            var houseToUpdateToGreatHouse = this.Houses
                .Where(h => h.ControlledCities.Count() >= MinCitiesRequiredForGreat && !(h is GreatHouse)).ToList();

            foreach (var house in houseToUpdateToGreatHouse)
            {
                this.Houses.Remove(house);
                this.Houses.Add(new GreatHouse(house.Name, house.TreasuryAmount, house.ControlledCities));
            }
        }
    }
}