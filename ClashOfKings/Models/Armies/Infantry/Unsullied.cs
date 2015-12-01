namespace ClashOfKings.Models.Armies.Infantry
{
    using Attributes;

    [MilitaryUnit]
    public class Unsullied : MilitaryUnit
    {
        private const int UnsulliedArmor = 5;
        private const int UnsulliedDamage = 25;
        private const decimal UnsulliedTrainingCost = 42.5M;
        private const double UnsulliedUpkeepCost = 0.75;
        private const int UnsulliedHousingSpacesRequired = 1;
        private const UnitType UnsulliedArmyType = UnitType.Infantry;

        public Unsullied() 
            : base(
                  UnsulliedArmor,
                  UnsulliedDamage,
                  UnsulliedTrainingCost,
                  UnsulliedUpkeepCost,
                  UnsulliedHousingSpacesRequired,
                  UnsulliedArmyType)
        {
        }
    }
}