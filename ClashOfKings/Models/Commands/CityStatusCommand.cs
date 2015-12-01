namespace ClashOfKings.Models.Commands
{
    using System;

    using ClashOfKings.Attributes;
    using ClashOfKings.Contracts;

    [Command]
    public class CityStatusCommand : Command
    {

        public CityStatusCommand(IGameEngine engine)
            : base(engine)
        {
        }

        public override void Execute(params string[] commandParams)
        {
            string cityName = commandParams[0];
            var city = this.Engine.Continent.GetCityByName(cityName);

            if (city == null)
            {
                throw new ArgumentNullException("city");
            }

            this.Engine.Render(city.Print());
        }
    }
}
