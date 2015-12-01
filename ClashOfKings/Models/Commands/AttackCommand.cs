namespace ClashOfKings.Models.Commands
{
    using System;
    using System.Linq;

    using ClashOfKings.Attributes;
    using ClashOfKings.Contracts;

    [Command]
    public class AttackCommand : Command
    {
        public AttackCommand(IGameEngine engine)
            : base(engine)
        {
        }

        public override void Execute(params string[] commandParams)
        {
            string attackerName = commandParams[0];
            string defenderName = commandParams[1];

            ICity attacker = this.Engine.Continent.GetCityByName(attackerName);
            ICity defender = this.Engine.Continent.GetCityByName(defenderName);

            this.Engine.Continent.VerifyTroopMovement(attacker, defender);

            if (attacker.ControllingHouse == defender.ControllingHouse)
            {
                throw new InvalidOperationException("House cannot attack one of its own cities");
            }

            attacker.FoodStorage -= this.Engine.Continent.CityNeighborsAndDistances[attacker][defender];

            var attackPower = attacker.AvailableMilitaryUnits.Sum(u => u.Damage);
            var defensePower = defender.Defense + defender.AvailableMilitaryUnits.Sum(u => u.Armor);

            if (attackPower > defensePower)
            {
                this.ProcessSuccessfulAttack(defender, attacker);
            }
            else
            {
                this.ProcessFailedAttack(defender, attacker);
            }
        }

        private void ProcessFailedAttack(ICity defender, ICity attacker)
        {
            this.Engine.Render(
                "House {0} defended {1} from House {2}",
                defender.ControllingHouse.Name,
                defender.Name,
                attacker.ControllingHouse.Name);

            var counterAttackStrength = defender.AvailableMilitaryUnits.Sum(u => u.Damage);
            var attackerDefence = attacker.AvailableMilitaryUnits.Sum(u => u.Armor);

            if (counterAttackStrength > attackerDefence)
            {
                attacker.RemoveUnits();

                this.Engine.Render(
                    "House {0} lost all attacking forces dispatched from {1}",
                    attacker.ControllingHouse.Name,
                    attacker.Name);
            }
        }

        private void ProcessSuccessfulAttack(ICity defender, ICity attacker)
        {
            defender.RemoveUnits();
            var previousController = defender.ControllingHouse.Name;

            defender.ControllingHouse.RemoveCityFromHouse(defender.Name);
            attacker.ControllingHouse.AddCityToHouse(defender);

            this.Engine.Render(
                "House {0} conquered {1} from House {2}",
                attacker.ControllingHouse.Name,
                defender.Name,
                previousController);
        }
    }
}
