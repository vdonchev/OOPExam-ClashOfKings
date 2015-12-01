namespace ClashOfKings.Engine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using ClashOfKings.Contracts;
    using ClashOfKings.Exceptions;

    public class Westeros : IContinent
    {
        private const string DuplicateCityErrorMessage = "City {0} already exists";
        private const string DuplicateHouseErrorMessage = "House {0} already exists";
        private const string DestinationAndStartAreSameErrorMessage = "Cannot move units: starting city and destination are the same";
        private const string CitiesAreNotNeighborsErrorMessage = "Cannot move units: starting city and destination are not neighbors";
        private const string NotEnoughProvisionsErrorMessage = "{0} doesn't have enough provisions to send troops to {1}";

        public Westeros()
        {
            this.Cities = new List<ICity>();
            this.Houses = new List<IHouse>();
            this.CityNeighborsAndDistances = new Dictionary<ICity, Dictionary<ICity, double>>();
        }

        public Dictionary<ICity, Dictionary<ICity, double>> CityNeighborsAndDistances { get; private set; }

        protected IList<ICity> Cities { get; private set; }

        protected IList<IHouse> Houses { get; private set; }

        public ICity GetCityByName(string cityName)
        {
            if (string.IsNullOrWhiteSpace(cityName))
            {
                throw new ArgumentNullException("cityName");
            }

            return this.Cities
                .FirstOrDefault(city => city.Name == cityName);
        }

        public IHouse GetHouseByName(string houseName)
        {
            if (string.IsNullOrWhiteSpace(houseName))
            {
                throw new ArgumentNullException("houseName");
            }

            return this.Houses
                .FirstOrDefault(house => house.Name == houseName);
        }

        public void AddCity(ICity city)
        {
            if (city == null)
            {
                throw new ArgumentNullException("city");
            }

            if (this.Cities.Any(c => c.Name == city.Name))
            {
                throw new DuplicateCityException(string.Format(DuplicateCityErrorMessage, city.Name));
            }

            this.Cities.Add(city);
        }

        public void AddHouse(IHouse house)
        {
            if (house == null)
            {
                throw new ArgumentNullException("house");
            }

            if (this.Houses.Any(h => h.Name == house.Name))
            {
                throw new DuplicateHouseException(string.Format(DuplicateHouseErrorMessage, house.Name));
            }

            this.Houses.Add(house);
        }

        public virtual string Print()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("Westeros:");

            if (this.Houses.Any())
            {
                result.AppendFormat(
                "-Houses ({0}): {1}{2}",
                this.Houses.Count,
                string.Join(", ", this.Houses.Select(h => h.Name).OrderBy(h => h)),
                Environment.NewLine);
            }
            else
            {
                result.AppendFormat("-No Houses{0}", Environment.NewLine);
            }

            if (this.Cities.Any())
            {
                result.AppendFormat(
                "-Cities ({0}): {1}",
                this.Cities.Count,
                string.Join(", ", this.Cities.Select(c => c.Name)));
            }
            else
            {
                result.Append("-No Cities");
            }

            return result.ToString();
        }

        public void VerifyTroopMovement(ICity startingCity, ICity destinationCity)
        {
            if (startingCity == null)
            {
                throw new ArgumentNullException("startingCity");
            }

            if (destinationCity == null)
            {
                throw new ArgumentNullException("destinationCity");
            }

            if (startingCity == destinationCity)
            {
                throw new InvalidOperationException(DestinationAndStartAreSameErrorMessage);
            }

            if (!this.CityNeighborsAndDistances[startingCity].ContainsKey(destinationCity))
            {
                throw new LocationOutOfRangeException(CitiesAreNotNeighborsErrorMessage);
            }

            if (!startingCity.AvailableMilitaryUnits.Any())
            {
                throw new ArgumentException("No troops to move");
            }

            if (startingCity.FoodStorage < this.CityNeighborsAndDistances[startingCity][destinationCity])
            {
                throw new NotEnoughProvisionsException(string.Format(
                    NotEnoughProvisionsErrorMessage, 
                    startingCity.Name, 
                    destinationCity.Name));
            }
        }

        public virtual void Update()
        {
            foreach (var city in this.Cities)
            {
                city.Update();
            }

            foreach (var house in this.Houses)
            {
                house.Update();
            }
        }
    }
}
