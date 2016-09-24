using System.Security.Cryptography.X509Certificates;

namespace Trivia
{
    public interface IQuestion
    {
        string Category { get; }

        string QuestionText { get; }

        void CorrectAnswerFor(Player player);

        void IncorrectAnswerFor(Player player);
    }

    public class PenaltyBoxQuestion : IQuestion
    {
        public string Category { get; }
        public string QuestionText { get; }

        public PenaltyBoxQuestion()
        {
            Category = Categories.Penalty;
            QuestionText = "Penalty Box - no question asked";
        }

        public void CorrectAnswerFor(Player player)
        {
            // Do nothing
        }

        public void IncorrectAnswerFor(Player player)
        {
            // Do nothing
        }
    }

    public class Question : IQuestion
    {
        public string Category { get; }
        public string QuestionText { get; }

        public Question(string category, string questionText)
        {
            Category = category;
            QuestionText = questionText;
        }

        public void CorrectAnswerFor(Player player)
        {
            player.WinsGoldCoin();
        }

        public void IncorrectAnswerFor(Player player)
        {
            // SEND PLAYER TO PENALTY BOX!
        }
    }

    public class Categories
    {
        public const string Penalty = "Penalty";
        public const string Pop = "Pop";
        public const string Sports = "Sports";
        public const string Rock = "Rock";
        public const string Science = "Science";
    }
}