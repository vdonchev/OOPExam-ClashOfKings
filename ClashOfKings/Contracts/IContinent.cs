namespace ClashOfKings.Contracts
{
    using System.Collections.Generic;

    public interface IContinent : IRenderable, IUpdateable
    {
        Dictionary<ICity, Dictionary<ICity, double>> CityNeighborsAndDistances { get; }

        ICity GetCityByName(string cityName);

        IHouse GetHouseByName(string houseName);

        void AddCity(ICity city);

        void AddHouse(IHouse house);

        void VerifyTroopMovement(ICity startingCity, ICity destinationCity);
    }
}
