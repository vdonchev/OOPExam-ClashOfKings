namespace ClashOfKings.UI
{
    using System;

    using ClashOfKings.Contracts;

    public class ConsoleInputController : IInputController
    {
        public string ReadInput()
        {
            return Console.ReadLine();
        }
    }
}
