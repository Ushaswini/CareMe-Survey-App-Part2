using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Homework_04.DTOs
{
    public class SurveyDTO
    {
        public string SurveyId { get; set; }
        public string QuestionText { get; set; }
        public string StudyGroupId { get; set; }
        public string SurveyCreatedTime { get; set; }
        public string StudyGroupName { get; set; }
    }
}