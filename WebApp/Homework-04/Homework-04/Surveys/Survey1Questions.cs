using System.Collections;
using System.Collections.Generic;

namespace Homework_04.Surveys
{
    public static class Survey1Questions
    {
      /*  static ArrayList Questions = new ArrayList();
        static ArrayList Options = new ArrayList();
        
        public static  void GenerateOptions()
        {
            Option o1 = new Option
            {
                OptionText = "Strongly Disagree",
                OptionValue = 1
            };
            Option o2 = new Option
            {
                OptionText = "Disagree",
                OptionValue = 2
            };
            Option o3 = new Option
            {
                OptionText = "Not sure",
                OptionValue = 3
            };
            Option o4 = new Option
            {
                OptionText = "Agree",
                OptionValue = 4
            };
            Option o5 = new Option
            {
                OptionText = "Strongly Agree",
                OptionValue = 5
            };

            Options.Add(o1);
            Options.Add(o2);
            Options.Add(o3);
            Options.Add(o4);
            Options.Add(o5);
        }
      
        public static void GenerateQuestions()
        {
            Question q1 = new Question
            {
                QuestionNumber = 1,
                QuestionText = "Take your blood pressure pills?",
                MaxRating = 7,
                MinRating = 0,
                RatingText = "I have not been prescribed blood pressure pills.",
                QuestionType = QuestionType.Rate
                
            };
            Question q2 = new Question
            {
                QuestionNumber = 2,
                QuestionText = "Take your blood pressure pillsat the same time everyday?",
                MaxRating = 7,
                MinRating = 0,
                RatingText = "I have notbeen prescribed blood pressure pills.",
                QuestionType = QuestionType.Rate
            };
            Question q3 = new Question
            {
                QuestionNumber = 3,
                QuestionText = "Take the recommended number of blood pressure pills?",
                MaxRating = 7,
                MinRating = 0,
                RatingText = "I have not been prescribed blood pressure pills. ",
                QuestionType = QuestionType.Rate
            };
            Question q4 = new Question
            {
                QuestionNumber = 4,
                QuestionText = "Eat nuts or peanut butter?",
                MaxRating = 7,
                MinRating = 0,
                RatingText = "I am allergic to nuts.",
                QuestionType = QuestionType.Rate
            };
            Question q5 = new Question
            {
                QuestionNumber = 5,
                QuestionText = "Eat beans, peas, or lentils?",
                MaxRating = 7,
                MinRating = 0,
                QuestionType = QuestionType.Rate
            };
            Question q6 = new Question
            {
                QuestionNumber = 6,
                QuestionText = "Eat eggs?",
                MaxRating = 7,
                MinRating = 0,
                QuestionType = QuestionType.Rate

            };
            Question q7 = new Question
            {
                QuestionNumber = 7,
                QuestionText = "Eat pickles, olives, or other vegetables in brine?",
                MaxRating = 7,
                MinRating = 0,
                QuestionType = QuestionType.Rate

            };
            Question q8 = new Question
            {
                QuestionNumber = 8,
                QuestionText = "Eat five or more servings of fruits and vegetables?",
                MaxRating = 7,
                MinRating = 0,
                QuestionType = QuestionType.Rate

            };
            Question q9 = new Question
            {
                QuestionNumber = 9,
                QuestionText = "Eat more than one serving of fruit (fresh, frozen, canned or fruit juice)?",
                MaxRating = 7,
                MinRating = 0,
                QuestionType = QuestionType.Rate

            };
            Question q10 = new Question
            {
                QuestionNumber = 10,
                QuestionText = "Eat more than one serving of vegetables?",
                MaxRating = 7,
                MinRating = 0,
                RatingText = "I have not been prescribed blood pressure pills. ",
                QuestionType = QuestionType.Rate
            };
            Question q11 = new Question
            {
                QuestionNumber = 11,
                QuestionText = "Drink milk (in a glass, with cereal, or in coffee, tea or cocoa)?",
                MaxRating = 7,
                MinRating = 0,
                QuestionType = QuestionType.Rate

            };
            Question q12 = new Question
            {
                QuestionNumber = 12,
                QuestionText = "Eat broccoli, collard greens, spinach, potatoes, squash or sweet potatoes?",
                MaxRating = 7,
                MinRating = 0,
                QuestionType = QuestionType.Rate

            };
            Question q13 = new Question
            {
                QuestionNumber = 13,
                QuestionText = "Eat apples, bananas, oranges, melon or raisins?",
                MaxRating = 7,
                MinRating = 0,
                QuestionType = QuestionType.Rate

            };
            Question q14 = new Question
            {
                QuestionNumber = 14,
                QuestionText = "Eat whole grain breads, cereals, grits, oatmeal or brown rice?",
                MaxRating = 7,
                MinRating = 0,
                QuestionType = QuestionType.Rate

            };
            Question q15 = new Question
            {
                QuestionNumber = 15,
                QuestionText = "Do at least 30 minutes total of physical activity?",
                MaxRating = 7,
                MinRating = 0,
                QuestionType = QuestionType.Rate

            };
            Question q16 = new Question
            {
                QuestionNumber = 16,
                QuestionText = "Do a specific exercise activity (such as swimming, walking, or biking) other than what you do around the house or as part of your work?",
                MaxRating = 7,
                MinRating = 0,
                QuestionType = QuestionType.Rate

            };
            Question q17 = new Question
            {
                QuestionNumber = 17,
                QuestionText = "Engage in weight lifting or strength training (other than what you do around the house or as part of your work)?",
                MaxRating = 7,
                MinRating = 0,
                QuestionType = QuestionType.Rate

            };
            Question q18 = new Question
            {
                QuestionNumber = 18,
                QuestionText = "Do any repeated heavy lifting or pushing/pulling of heavy items either for your job oraround the house or garden?",
                MaxRating = 7,
                MinRating = 0,
                QuestionType = QuestionType.Rate

            };
            Question q19 = new Question
            {
                QuestionNumber = 19,
                QuestionText = "Smoke a cigarette or cigar, even just one puff?",
                MaxRating = 7,
                MinRating = 0,
                QuestionType = QuestionType.Rate

            };
            Question q20 = new Question
            {
                QuestionNumber = 20,
                QuestionText = "Stay in a room or ride in an enclosed vehicle while someone was smoking?",
                MaxRating = 7,
                MinRating = 0,
                QuestionType = QuestionType.Rate

            };
            Question q21 = new Question
            {
                QuestionNumber = 21,
                QuestionText = "I am careful about what I eat.",
                Options = Options,
                QuestionType = QuestionType.Choice
            };
            Question q22 = new Question
            {
                QuestionNumber = 22,
                QuestionText = "I read food labels when I grocery shop.",
                Options = Options,
                QuestionType = QuestionType.Choice
            };
            Question q23 = new Question
            {
                QuestionNumber = 23,
                QuestionText = " I exercise in order to lose or maintain weight.",
                Options = Options,
                QuestionType = QuestionType.Choice
            };
            Question q24 = new Question
            {
                QuestionNumber = 24,
                QuestionText = " I have cut out drinking sugary sodas and sweet tea.",
                Options = Options,
                QuestionType = QuestionType.Choice
            };
            Question q25 = new Question
            {
                QuestionNumber = 25,
                QuestionText = " I eat smaller portions or eat fewer portions.",
                Options = Options,
                QuestionType = QuestionType.Choice
            };
            Question q26 = new Question
            {
                QuestionNumber = 26,
                QuestionText = " I have stopped buying or bringing unhealthy foods into my home.",                
                Options = Options,
                QuestionType = QuestionType.Choice
            };
            Question q27 = new Question
            {
                QuestionNumber = 27,
                QuestionText = "I have cut out or limit some foods that I like but that are not good for me.",               
                Options = Options,
                QuestionType = QuestionType.Choice
            };
            Question q28 = new Question
            {
                QuestionNumber = 28,
                QuestionText = " I eat at restaurants or fast food places less often.",
                Options = Options,
                QuestionType = QuestionType.Choice
            };
            Question q29 = new Question
            {
                QuestionNumber = 29,
                QuestionText = " I substitute healthier foods for things that I used to eat.",
                Options = Options,
                QuestionType = QuestionType.Choice
            };
            Question q30 = new Question
            {
                QuestionNumber = 30,
                QuestionText = " I have modified my recipes when I cook.",
                Options = Options,
                QuestionType = QuestionType.Choice
            };
            Question q31 = new Question
            {
                QuestionNumber = 31,
                QuestionText = "On average, how many days per week do you drink alcohol?",
                MaxRating = 7,
                MinRating = 0,
                QuestionType = QuestionType.Rate

            };
            Question q32 = new Question
            {
                QuestionNumber = 32,
                QuestionText = " On a typical day that you drink alcohol, how many drinks do you have?",
                QuestionType = QuestionType.Text
            };
            Question q33 = new Question
            {
                QuestionNumber = 2,
                QuestionText = "What is the largest number of drinks that you’ve had on any given day within the last month?",
                QuestionType = QuestionType.Text
            };

            Questions.Add(q1);Questions.Add(q2);Questions.Add(q3);Questions.Add(q4); Questions.Add(q5);
            Questions.Add(q6); Questions.Add(q7); Questions.Add(q8); Questions.Add(q9); Questions.Add(q10);
            Questions.Add(q11); Questions.Add(q12); Questions.Add(q13); Questions.Add(q14); Questions.Add(q15);
            Questions.Add(q16); Questions.Add(q17); Questions.Add(q18); Questions.Add(q19); Questions.Add(q20);
            Questions.Add(q21); Questions.Add(q22); Questions.Add(q23); Questions.Add(q24); Questions.Add(q25);
            Questions.Add(q26); Questions.Add(q27); Questions.Add(q28); Questions.Add(q29); Questions.Add(q30);
            Questions.Add(q31); Questions.Add(q32); Questions.Add(q33); 

        }

        public static ArrayList GetQuestions()
        {
            GenerateOptions();
            GenerateQuestions();
            return Questions;
        }*/
    }
}