namespace ClashOfKings.Models.ArmyStructures
{
    using Armies;
    using Attributes;

    [ArmyStructure]
    public class DragonLair : ArmyStructureBase
    {
        private const CityType DragonLairCityTypeRequired = CityType.Capital;
        private const decimal DragonLairBuildCost = 200000;
        private const int DragonLairCapacity = 3;
        private const UnitType DragonLairUnitType = UnitType.AirForce;

        public DragonLair() 
            : base(DragonLairCityTypeRequired,
                  DragonLairBuildCost,
                  DragonLairCapacity,
                  DragonLairUnitType)
        {
        }
    }
}