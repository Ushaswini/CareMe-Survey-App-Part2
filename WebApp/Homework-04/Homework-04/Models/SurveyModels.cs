using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Homework_04.Models
{
    public class Survey
    {
        public string SurveyId { get; set; }
        public string QuestionText { get; set; }
        public string StudyGroupId { get; set; }
        public string SurveyCreatedTime { get; set; }
        [EnumDataType(typeof(Frequency))]
        public Frequency FrequencyOfNotifications { get; set; }
        public string Time1 { get; set; }
        public string Time2 { get; set; }

        //Navigation Properties
        public StudyGroup StudyGroup { get; set; }
    }

    public enum Frequency
    {
        Daily,
        Hourly,
        TwiceDaily
    }

    public class SurveyResponse
    {
        public string SurveyId { get; set; }
        public string UserId { get; set; }
        public string SurveyResponseId { get; set; }
        public string StudyGroupId { get; set; }
        public string UserResponseText { get; set; }
        public string SurveyResponseReceivedTime { get; set; }

        public string SurveyComments { get; set; }

        //Navigation Properties
        public Survey Survey { get; set; }
        public ApplicationUser User { get; set; }
        public StudyGroup StudyGroup { get; set; }

    }

    public class StudyGroup
    {
        public string StudyGroupId { get; set; }
        public string StudyName { get; set; }
        public string StudyCoordinatorId { get; set; }
        public string StudyGroupCreadtedTime { get; set; }

        //Navigation Properties
        [Required]
        public ApplicationUser StudyCoordinator { get; set; }
    }

    public class SurveyQuestionAnswer
    {
        public int QuestionNumber { get; set; }
        public int AnswerValue { get; set; }
        public string AnswerText { get; set; }
    }
}