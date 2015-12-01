namespace ClashOfKings.Models.Commands
{
    using Attributes;
    using Contracts;
    using Exceptions;

    [Command]
    public class AddNeighborsToCityCommand : Command
    {
        public AddNeighborsToCityCommand(IGameEngine engine)
            : base(engine)
        {
        }

        public override void Execute(params string[] commandParams)
        {
            string cityName = commandParams[0];

            for (int i = 1; i < commandParams.Length; i += 2)
            {
                var neighbourCityName = commandParams[i];
                var neighbourCityDistance = double.Parse(commandParams[i + 1]);

                ICity city = this.Engine.Continent.GetCityByName(cityName);
                ICity neighbour = this.Engine.Continent.GetCityByName(neighbourCityName);

                if (city == null)
                {
                    throw new CityNotFoundException("This city is not presented on the map");
                }

                if (neighbour == null)
                {
                    this.Engine.Render("Specified neighbor does not exist");
                    continue;
                }

                if (neighbourCityDistance < 0)
                {
                    this.Engine.Render("The distance between cities cannot be negative");
                    continue;
                }

                this.Engine.Continent.CityNeighborsAndDistances[city].Add(neighbour, neighbourCityDistance);
                this.Engine.Continent.CityNeighborsAndDistances[neighbour].Add(city, neighbourCityDistance);
            }

            this.Engine.Render($"All valid neighbor records added for city {cityName}");
        }
    }
}