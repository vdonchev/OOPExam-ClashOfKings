namespace ClashOfKings.Engine.Factories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using ClashOfKings.Attributes;
    using ClashOfKings.Contracts;

    public class UnitFactory : IUnitFactory
    {
        public ICollection<IMilitaryUnit> CreateUnits(string unitTypeName, int count)
        {
            var unitType = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .FirstOrDefault(type => type.CustomAttributes.Any(a => a.AttributeType == typeof(MilitaryUnitAttribute)) &&
                                        type.Name == unitTypeName);

            if (unitType == null)
            {
                throw new ArgumentNullException("unitType", "Unknown unit type");
            }

            var units = Enumerable.Repeat(Activator.CreateInstance(unitType) as IMilitaryUnit, count);

            return new List<IMilitaryUnit>(units);
        }
    }
}
