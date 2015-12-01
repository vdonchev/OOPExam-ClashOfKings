namespace ClashOfKings.Engine.Factories
{
    using System;
    using System.Linq;
    using System.Reflection;

    using ClashOfKings.Attributes;
    using ClashOfKings.Contracts;

    public class ArmyStructureFactory : IArmyStructureFactory
    {
        public IArmyStructure CreateStructure(string structureName)
        {
            var structureType = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .FirstOrDefault(type => type.CustomAttributes.Any(a => a.AttributeType == typeof(ArmyStructureAttribute)) && 
                                        type.Name == structureName);

            if (structureType == null)
            {
                throw new ArgumentNullException("structureType", "Unknown structure");
            }

            var structure = Activator.CreateInstance(structureType) as IArmyStructure;

            return structure;
        }
    }
}
