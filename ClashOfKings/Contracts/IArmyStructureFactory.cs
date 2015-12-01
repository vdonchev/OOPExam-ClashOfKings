namespace ClashOfKings.Contracts
{
    public interface IArmyStructureFactory
    {
        IArmyStructure CreateStructure(string structureName);
    }
}
