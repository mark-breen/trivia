using System;

namespace Trivia
{
    public class ConsoleOutput : IGameOutput
    {
        public void OutputMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}