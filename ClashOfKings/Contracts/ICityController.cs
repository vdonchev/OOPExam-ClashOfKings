namespace ClashOfKings.Contracts
{
    using System.Collections.Generic;

    public interface ICityController
    {
        IEnumerable<ICity> ControlledCities { get; }

        void AddCityToHouse(ICity city);

        void RemoveCityFromHouse(string city);

        void UpgradeCity(ICity city);
    }
}
