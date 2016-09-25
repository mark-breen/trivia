using System.Collections.Generic;

namespace Trivia
{
    public class Question
    {
        public string QuestionText { get; }

        public Question(string questionText)
        {
            QuestionText = questionText;
        }
    }

    public class Questions
    {
        private readonly IGameOutput _gameOutput;

        private readonly Dictionary<string, LinkedList<Question>> _questions = new Dictionary<string, LinkedList<Question>>
        {
            { Categories.Pop, new LinkedList<Question>() },
            { Categories.Science, new LinkedList<Question>() },
            { Categories.Sports, new LinkedList<Question>() },
            { Categories.Rock, new LinkedList<Question>() },
        };

        public Questions(IGameOutput gameOutput)
        {
            _gameOutput = gameOutput;
            for (var i = 0; i < 50; i++)
            {
                AddQuestionFor(Categories.Pop, i);
                AddQuestionFor(Categories.Science, i);
                AddQuestionFor(Categories.Sports, i);
                AddQuestionFor(Categories.Rock, i);
            }
        }

        public void AskQuestionFor(Player currentPlayer)
        {
            var questions = _questions[currentPlayer.CurrentPlace.Category];
            _gameOutput.OutputMessage(questions.First.Value.QuestionText);
            questions.RemoveFirst();
        }

        private void AddQuestionFor(string category, int questionNo)
        {
            _questions[category].AddLast(new Question($"{category} Question {questionNo}"));
        }
    }
}