using System.Text;

namespace Trivia.Tests
{

    public class TestOutput : IGameOutput
    {
        private readonly StringBuilder _messages = new StringBuilder();

        public void OutputMessage(string message)
        {
            _messages.AppendLine(message);
        }

        public string GetMessages()
        {
            return _messages.ToString();
        }
    }
}