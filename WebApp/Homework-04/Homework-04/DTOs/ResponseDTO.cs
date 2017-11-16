using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Homework_04.DTOs
{
    public class ResponseDTO
    {
        public string UserName { get; set; }
        public string StudyGroupName { get; set; }

        public string SurveyId { get; set; }
        public string SurveyComments { get; set; }
        public string SurveyResponseReceivedTime { get; set; }

    }
}