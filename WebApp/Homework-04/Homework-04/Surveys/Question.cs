using System.Collections;

namespace Homework_04.Surveys
{
    public class Question
    {
        public int QuestionNumber { get; set; }
        public string QuestionText { get; set; }
       // public int MinRating { get; set; }
       // public int MaxRating { get; set; }
       // public string RatingText { get; set; }
        public ArrayList Options { get; set; }
       // public QuestionType QuestionType { get; set; }

    }

    public class Option
    {
        public int OptionValue { get; set; }
        public string OptionText { get; set; }
    }

 
    public enum QuestionType
    {
        Text,
        Choice,
        Rate
    }

    public class QuestionDecoded
    {
        public object Data { get; set; }
    }
}