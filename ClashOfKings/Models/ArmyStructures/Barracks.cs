namespace ClashOfKings.Models.ArmyStructures
{
    using Armies;
    using Attributes;

    [ArmyStructure]
    public class Barracks : ArmyStructureBase
    {
        private const CityType BarracksCityTypeRequired = CityType.Keep;
        private const decimal BarracksBuildCost = 15000;
        private const int BarracksCapacity = 5000;
        private const UnitType BarracksUnitType = UnitType.Infantry;

        public Barracks() 
            : base(BarracksCityTypeRequired,
                  BarracksBuildCost,
                  BarracksCapacity,
                  BarracksUnitType)
        {
        }
    }
}