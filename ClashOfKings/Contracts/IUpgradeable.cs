namespace ClashOfKings.Contracts
{
    public interface IUpgradeable
    {
        decimal UpgradeCost { get; }

        void Upgrade();
    }
}
