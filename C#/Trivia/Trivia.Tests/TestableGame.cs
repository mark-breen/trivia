using System.Text;

namespace Trivia.Tests
{
    public class TestableGame : Game
    {
        private readonly StringBuilder _messages = new StringBuilder();

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