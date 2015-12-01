namespace ClashOfKings.Models
{
    using System;
    using System.Collections.Generic;
    using Contracts;

    public class GreatHouse : House
    {
        public GreatHouse(string name, decimal initialTreasuryAmount, IEnumerable<ICity> cities) 
            : base(name, initialTreasuryAmount)
        {
            this.AddAllCitiesToHouse(cities);
        }

        public override decimal TreasuryAmount { get; set; }

        public override void UpgradeCity(ICity city)
        {
            if (city == null)
            {
                throw new ArgumentNullException("city", "Specified city does not exist or is not controlled by house {0}");
            }

            city.Upgrade();
            this.TreasuryAmount -= city.UpgradeCost;
        }

        public override string Print()
        {
            return "Great " + base.Print();
        }

        private void AddAllCitiesToHouse(IEnumerable<ICity> cities)
        {
            foreach (var city in cities)
            {
                this.AddCityToHouse(city);
            }
        }
    }
}