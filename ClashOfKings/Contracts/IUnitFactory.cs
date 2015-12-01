namespace ClashOfKings.Contracts
{
    using System.Collections.Generic;

    public interface IUnitFactory
    {
        ICollection<IMilitaryUnit> CreateUnits(string unitType, int count);
    }
}
