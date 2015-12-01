namespace ClashOfKings.Models.ArmyStructures
{
    using System;
    using Armies;
    using Contracts;

    public abstract class ArmyStructureBase : IArmyStructure
    {
        private decimal buildCost;
        private int capacity;

        protected ArmyStructureBase(
            CityType cityType,
            decimal buildCost,
            int capacity,
            UnitType unitType)
        {
            this.RequiredCityType = cityType;
            this.BuildCost = buildCost;
            this.Capacity = capacity;
            this.UnitType = unitType;
        }

        public CityType RequiredCityType { get; private set; }

        public decimal BuildCost
        {
            get
            {
                return this.buildCost;
            }

            private set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Building cost can't be negative");
                }

                this.buildCost = value;
            }
        }

        public int Capacity
        {
            get
            {
                return this.capacity;
            }

            private set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Building capacity can't be negative");
                }

                this.capacity = value;
            }
        }

        public UnitType UnitType { get; private set; }
    }
}