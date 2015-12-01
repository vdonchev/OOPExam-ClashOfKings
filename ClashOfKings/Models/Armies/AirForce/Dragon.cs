namespace ClashOfKings.Models.Armies.AirForce
{
    using Attributes;

    [MilitaryUnit]
    public class Dragon : MilitaryUnit
    {
        private const int DragonArmor = 700;
        private const int DragonDamage = 1200;
        private const decimal DragonTrainingCost = 1500;
        private const double DragonUpkeepCost = 100;
        private const int DragonHousingSpacesRequired = 1;
        private const UnitType DragonArmyType = UnitType.AirForce;

        public Dragon() 
            : base(DragonArmor,
                  DragonDamage,
                  DragonTrainingCost,
                  DragonUpkeepCost,
                  DragonHousingSpacesRequired,
                  DragonArmyType)
        {
        }
    }
}