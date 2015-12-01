namespace ClashOfKings.Models.Armies.Infantry
{
    using ClashOfKings.Attributes;

    [MilitaryUnit]
    public class FootSoldier : MilitaryUnit
    {
        private const int FootSoldierArmor = 2;
        private const int FootSoldierDamage = 5;
        private const decimal FootSoldierTrainingCost = 12.25m;
        private const double FootSoldierUpkeepCost = 1;
        private const int FootSoldierHousingSpacesRequired = 1;
        private const UnitType FootSoldierArmyType = UnitType.Infantry;

        public FootSoldier()
            : base(
                  FootSoldierArmor,
                  FootSoldierDamage,
                  FootSoldierTrainingCost,
                  FootSoldierUpkeepCost,
                  FootSoldierHousingSpacesRequired,
                  FootSoldierArmyType)
        {
        }
    }
}
