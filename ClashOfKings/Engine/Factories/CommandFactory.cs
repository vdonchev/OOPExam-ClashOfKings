namespace ClashOfKings.Engine.Factories
{
    using System;
    using System.Linq;
    using System.Reflection;

    using ClashOfKings.Attributes;
    using ClashOfKings.Contracts;

    public class CommandFactory : ICommandFactory
    {
        public ICommand CreateCommand(string commandName, IGameEngine engine)
        {
            var commandType = Assembly.GetExecutingAssembly()
               .GetTypes()
               .FirstOrDefault(c => c.CustomAttributes.Any(a => a.AttributeType == typeof(CommandAttribute)) &&
                                       c.Name == commandName);

            if (commandType == null)
            {
                throw new ArgumentNullException("commandType", "Unknown command");
            }

            var command = Activator.CreateInstance(commandType, engine) as ICommand;

            return command;
        }
    }
}
