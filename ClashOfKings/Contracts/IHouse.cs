namespace ClashOfKings.Contracts
{
    public interface IHouse : ITaxCollector, ICityController, IUpdateable, IRenderable
    {
        string Name { get; }
    }
}
