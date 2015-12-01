namespace ClashOfKings.Contracts
{
    using System.Collections.Generic;

    using ClashOfKings.Models.Armies;

    public interface IArmyBase : IFoodProducible
    {
        IEnumerable<IMilitaryUnit> AvailableMilitaryUnits { get; }

        Dictionary<UnitType, int> AvailableUnitsByType { get; }

        IEnumerable<IArmyStructure> ArmyStructures { get; }

        int AvailableUnitCapacity(UnitType type);

        void AddUnits(ICollection<IMilitaryUnit> units);

        ICollection<IMilitaryUnit> RemoveUnits();

        void AddArmyStructure(IArmyStructure structure);
    }
}
