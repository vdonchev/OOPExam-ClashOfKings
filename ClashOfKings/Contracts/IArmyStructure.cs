namespace ClashOfKings.Contracts
{
    using ClashOfKings.Models;
    using ClashOfKings.Models.Armies;

    public interface IArmyStructure
    {
        CityType RequiredCityType { get; }

        decimal BuildCost { get; }

        int Capacity { get; }

        UnitType UnitType { get; }
    }
}
