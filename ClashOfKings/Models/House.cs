namespace ClashOfKings.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    using ClashOfKings.Contracts;
    using ClashOfKings.Exceptions;

    public class House : IHouse
    {
        private const int MinHouseNameLength = 2;
        private const int MaxHouseNameLength = 15;

        private const string EmptyHouseNameErrorMessage = "House name cannot be null";
        private const string InvalidHouseNameErrorMessage = 
            "House name should be between {0} and {1} symbols long and contain only Latin letters";
        private const string CityNotFoundErrorMessage = "Specified city does not exist or is not controlled by house {0}";
        private const string InsufficientUpgradeFundsErrorMessage = "House {0} has insufficient funds to upgrade {1}";

        private static readonly string HouseNamePattern = string.Format("[a-zA-Z]{{{0},{1}}}", MinHouseNameLength, MaxHouseNameLength);

        private string name;
        private decimal treasuryAmount;
        private readonly IList<ICity> controlledCities;

        public House(string name, decimal initialTreasuryAmount)
        {
            this.controlledCities = new List<ICity>();
            this.Name = name;
            this.TreasuryAmount = initialTreasuryAmount;
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
                    throw new ArgumentNullException("name", EmptyHouseNameErrorMessage);
                }

                if (!Regex.IsMatch(value, HouseNamePattern))
                {
                    throw new ArgumentException(string.Format(
                        InvalidHouseNameErrorMessage,
                        MinHouseNameLength,
                        MaxHouseNameLength));
                }

                this.name = value;
            }
        }

        public virtual decimal TreasuryAmount
        {
            get
            {
                return this.treasuryAmount;
            }

            set
            {
                if (value < 0)
                {
                    this.DeclareBankruptcy();
                    value = 0;
                }

                this.treasuryAmount = value;
            }
        }

        public IEnumerable<ICity> ControlledCities
        {
            get
            {
                return this.controlledCities;
            }
        }

        public virtual void UpgradeCity(ICity city)
        {
            if (city == null)
            {
                throw new ArgumentNullException("city", CityNotFoundErrorMessage);
            }

            if (this.TreasuryAmount < city.UpgradeCost)
            {
                throw new InsufficientFundsException(string.Format(InsufficientUpgradeFundsErrorMessage, this.Name, city.Name));
            }

            city.Upgrade();
			this.TreasuryAmount -= city.UpgradeCost;
        }

        public void AddCityToHouse(ICity city)
        {
            if (city == null)
            {
                throw new ArgumentNullException("city");
            }

            this.controlledCities.Add(city);
            city.ControllingHouse = this;
        }

        public void RemoveCityFromHouse(string cityName)
        {
            ICity city = this.ControlledCities.FirstOrDefault(c => c.Name == cityName);

            if (city == null)
            {
                throw new ArgumentNullException("city", string.Format(CityNotFoundErrorMessage, this.Name));
            }

            this.controlledCities.Remove(city);
        }

        public virtual void Update()
        {
            this.TreasuryAmount += this.ControlledCities.Sum(city => city.TaxBase);
        }

        public virtual string Print()
        {
            StringBuilder result = new StringBuilder();

            result.AppendFormat("House {0}:{1}", this.Name, Environment.NewLine);
            result.AppendFormat("-Treasury Amount: {0:F1}{1}", this.TreasuryAmount, Environment.NewLine);

            if (this.ControlledCities.Any())
            {
                result.AppendFormat(
                "-Controlled Cities ({0}): {1}",
                this.ControlledCities.Count(),
                string.Join(", ", this.ControlledCities.Select(c => c.Name)));
            }
            else
            {
                result.AppendFormat("-No Cities Controlled");
            }

            return result.ToString();
        }

        private void DeclareBankruptcy()
        {
            foreach (var city in this.controlledCities)
            {
                city.Downgrade();
            }
        }
    }
}
