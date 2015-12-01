namespace ClashOfKings.UI
{
    using System;

    using ClashOfKings.Contracts;

    public class ConsoleRenderer : IRenderer
    {
        public void Print(string message, params object[] parameters)
        {
            Console.WriteLine(message, parameters);
        }
    }
}
