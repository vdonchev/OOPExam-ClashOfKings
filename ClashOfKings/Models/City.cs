namespace ClashOfKings.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    using ClashOfKings.Contracts;
    using ClashOfKings.Exceptions;
    using ClashOfKings.Models.Armies;

    public class City : ICity
    {
        private const int MinCityNameLength = 2;
        private const int MaxCityNameLength = 25;

        private const string EmptyCityNameErrorMessage = "City name cannot be null";
        private const string InvalidCityNameErrorMessage =
            "City name should be between {0} and {1} symbols long and contain only Latin letters and hyphens";
        private const string InsufficientCitySizeErrorMessage =
            "City {0} is not advanced enough to build {1}";
        private const string MaxCityLevelReachedErrorMessage =
            "City {0} is at the maximum level and cannot be upgraded further";

        private const CityType DefaultCityType = CityType.Keep;

        private static readonly string CityNamePattern = string.Format(@"[a-zA-Z\-]{{{0},{1}}}", MinCityNameLength, MaxCityNameLength);

        private string name;
        private int defense;
        private decimal upgradeCost;
        private double foodProduction;
        private decimal taxBase;
        private IList<IMilitaryUnit> availableMilitaryUnits;
        private IList<IArmyStructure> armyStructures;

        public City(
            string name,
            IHouse controllingHouse,
            int defense,
            decimal upgradeCost,
            double initialFoodStorage,
            double foodProduction,
            decimal taxBase,
            CityType cityType = DefaultCityType)
        {
            this.Name = name;
            this.ControllingHouse = controllingHouse;
            this.Defense = defense;
            this.UpgradeCost = upgradeCost;
            this.FoodStorage = initialFoodStorage;
            this.FoodProduction = foodProduction;
            this.TaxBase = taxBase;
            this.availableMilitaryUnits = new List<IMilitaryUnit>();
            this.armyStructures = new List<IArmyStructure>();
            this.CityType = cityType;
        }

        public string Name
        {
            get
            {
                return this.name;
            }

            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("name", EmptyCityNameErrorMessage);
                }

                if (!Regex.IsMatch(value, CityNamePattern))
                {
                    throw new ArgumentException(string.Format(
                        InvalidCityNameErrorMessage,
                        MinCityNameLength,
                        MaxCityNameLength));
                }

                this.name = value;
            }
        }

        public int Defense
        {
            get
            {
                return this.defense;
            }

            private set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("defense");
                }

                this.defense = value;
            }
        }

        public decimal UpgradeCost
        {
            get
            {
                return this.upgradeCost;
            }

            private set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("upgradeCost");
                }

                this.upgradeCost = value;
            }
        }

        public CityType CityType { get; private set; }

        public double FoodProduction
        {
            get
            {
                return this.foodProduction;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("foodProduction");
                }

                this.foodProduction = value;
            }
        }

        public double FoodStorage { get; set; }

        public decimal TaxBase
        {
            get
            {
                return this.taxBase;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("taxBase");
                }

                this.taxBase = value;
            }
        }

        public IEnumerable<IMilitaryUnit> AvailableMilitaryUnits
        {
            get
            {
                return this.availableMilitaryUnits;
            }
        }

        public Dictionary<UnitType, int> AvailableUnitsByType
        {
            get
            {
                var unitsByType = this.availableMilitaryUnits
                    .GroupBy(u => u.Type)
                    .ToDictionary(u => u.Key, u => u.Count());

                return unitsByType;
            }
        }

        public IEnumerable<IArmyStructure> ArmyStructures
        {
            get
            {
                return this.armyStructures;
            }
        }

        public IHouse ControllingHouse { get; set; }

        public int AvailableUnitCapacity(UnitType type)
        {
            switch (type)
            {
                case UnitType.Infantry:
                    return this.AvailableInfantryCapacity;
                case UnitType.Cavalry:
                    return this.AvailableCavalryCapacity;
                case UnitType.AirForce:
                    return this.AvailableFlyingUnitCapacity;
                default:
                    throw new ArgumentException("Unknown unit type");
            }
        }

        private int AvailableInfantryCapacity
        {
            get
            {
                int totalInfantryCapacity = this.ArmyStructures
                    .Where(structure => structure.UnitType == UnitType.Infantry)
                    .Sum(structure => structure.Capacity);

                int usedInfantryCapacity = this.availableMilitaryUnits
                    .Where(unit => unit.Type == UnitType.Infantry)
                    .Sum(unit => unit.HousingSpacesRequired);

                return totalInfantryCapacity - usedInfantryCapacity;
            }
        }

        private int AvailableCavalryCapacity
        {
            get
            {
                int totalCavalryCapacity = this.ArmyStructures
                  .Where(structure => structure.UnitType == UnitType.Cavalry)
                  .Sum(structure => structure.Capacity);

                int usedCavalryCapacity = this.availableMilitaryUnits
                    .Where(unit => unit.Type == UnitType.Cavalry)
                    .Sum(unit => unit.HousingSpacesRequired);

                return totalCavalryCapacity - usedCavalryCapacity;
            }
        }

        private int AvailableFlyingUnitCapacity
        {
            get
            {
                int totalFlyingUnitCapacity = this.ArmyStructures
                  .Where(structure => structure.UnitType == UnitType.AirForce)
                  .Sum(structure => structure.Capacity);

                int usedFlyingUnitCapacity = this.availableMilitaryUnits
                    .Where(unit => unit.Type == UnitType.AirForce)
                    .Sum(unit => unit.HousingSpacesRequired);

                return totalFlyingUnitCapacity - usedFlyingUnitCapacity;
            }
        }

        public virtual void AddUnits(ICollection<IMilitaryUnit> units)
        {
            this.VerifyHousingSpaces(units);

            foreach (var militaryUnit in units)
            {
                this.availableMilitaryUnits.Add(militaryUnit);
            }
        }

        public ICollection<IMilitaryUnit> RemoveUnits()
        {
            var units = this.availableMilitaryUnits.ToArray();
            this.availableMilitaryUnits = new List<IMilitaryUnit>();

            return units;
        }

        public virtual void AddArmyStructure(IArmyStructure structure)
        {
            if (structure.RequiredCityType > this.CityType)
            {
                throw new InsufficientCitySizeException(string.Format(
                    InsufficientCitySizeErrorMessage,
                    this.Name,
                    structure.GetType().Name));
            }

            this.armyStructures.Add(structure);
        }

        public virtual void Upgrade()
        {
            if (this.CityType == CityType.Capital)
            {
                throw new InvalidOperationException(string.Format(MaxCityLevelReachedErrorMessage, this.Name));
            }

            this.CityType++;
        }

        public virtual void Downgrade()
        {
            if (this.CityType == CityType.Keep)
            {
                return;
            }

            this.CityType--;
            this.RemoveUnsupportedStructures();
        }

        public virtual void Update()
        {
            this.FoodStorage += this.FoodProduction;
            this.ProvisionArmy();
        }

        public virtual string Print()
        {
            StringBuilder result = new StringBuilder();
            result.AppendFormat("{0} ({1}):{2}", this.Name, this.CityType, Environment.NewLine);
            result.AppendFormat("-Food storage: {0:F1}{1}", this.FoodStorage, Environment.NewLine);
            result.AppendFormat("-Food production: {0:F1}{1}", this.foodProduction, Environment.NewLine);
            result.AppendFormat("-Tax base: {0:F1}{1}", this.TaxBase, Environment.NewLine);

            result.AppendFormat(
                "-Army: {0}{1}",
                string.Join(", ", this.AvailableUnitsByType.Select(u => string.Format("{0} {1}", u.Value, u.Key))),
                Environment.NewLine);

            result.AppendFormat("-Ruling House: House {0}", this.ControllingHouse.Name);

            return result.ToString();
        }

        private void VerifyHousingSpaces(ICollection<IMilitaryUnit> units)
        {
            var infantryToAdd = units
                .Where(u => u.Type == UnitType.Infantry)
                .Sum(u => u.HousingSpacesRequired);

            var cavalryToAdd = units
                .Where(u => u.Type == UnitType.Cavalry)
                .Sum(u => u.HousingSpacesRequired);

            var airforceToAdd = units
                .Where(u => u.Type == UnitType.AirForce)
                .Sum(u => u.HousingSpacesRequired);

            if (infantryToAdd > this.AvailableInfantryCapacity
                || cavalryToAdd > this.AvailableCavalryCapacity
                || airforceToAdd > this.AvailableFlyingUnitCapacity)
            {
                throw new InsufficientHousingSpacesException(string.Format("{0} doesn't have enough housing spaces", this.Name));
            }
        }

        private void ProvisionArmy()
        {
            bool starved = false;

            while (this.FoodStorage < this.AvailableMilitaryUnits.Sum(unit => unit.UpkeepCost))
            {
                this.Starve();
                starved = true;
            }

            this.FoodStorage -= this.AvailableMilitaryUnits.Sum(unit => unit.UpkeepCost);

            if (starved)
            {
                throw new NotEnoughProvisionsException(string.Format("City {0} suffered starvation", this.Name));
            }
        }

        private void Starve()
        {
            var numberOfUnitsToRemove = Math.Max(5, this.AvailableMilitaryUnits.Count() / 5);

            this.availableMilitaryUnits = this.availableMilitaryUnits
                .Skip(numberOfUnitsToRemove)
                .ToList();
        }

        private void RemoveUnsupportedStructures()
        {
            this.armyStructures = this.armyStructures
                .Where(structure => structure.RequiredCityType <= this.CityType)
                .ToList();
        }
    }
}
