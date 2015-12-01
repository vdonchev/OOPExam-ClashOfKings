namespace ClashOfKings.Models.ArmyStructures
{
    using Armies;
    using Attributes;

    [ArmyStructure]
    public class Stable : ArmyStructureBase
    {
        private const CityType StableCityTypeRequired = CityType.FortifiedCity;
        private const decimal StableBuildCost = 75000;
        private const int StableCapacity = 2500;
        private const UnitType StableUnitType = UnitType.Cavalry;

        public Stable() 
            : base(StableCityTypeRequired,
                  StableBuildCost,
                  StableCapacity,
                  StableUnitType)
        {
        }
    }
}