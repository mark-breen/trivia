using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using UglyTrivia;

namespace Trivia.Tests
{
    public class TestableGame : Game
    {
        private StringBuilder _messages = new StringBuilder();

        protected override void OutputMessage(string message)
        {
            _messages.AppendLine(message);
        }

        public string GetMessages()
        {
            return _messages.ToString();
        }
    }
}