using Homework_04.Surveys;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Homework_04.Models
{
    [Authorize(Roles ="Admin")]
    [RoutePrefix("api/GenerateSurvey")]
    public class GenerateSurveyController : ApiController
    {

        private ApplicationUserManager _userManager;

        private ApplicationDbContext db = new ApplicationDbContext();
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            set
            {
                _userManager = value;
            }
        }

        
        [ResponseType(typeof(Survey))]
        public IHttpActionResult PostSurvey(Survey survey1)
        {
            
            Survey survey = new Survey {
                SurveyCreatedTime = DateTime.Now.ToString(),
               // QuestionText = JsonConvert.SerializeObject(Survey1Questions.GetQuestions()),
                StudyGroupId = survey1.StudyGroupId,
                SurveyId = System.Guid.NewGuid().ToString()
            };

            db.Surveys.Add(survey);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (SurveyExists(survey.SurveyId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtRoute("DefaultApi", new { id = survey.SurveyId }, survey);


        }

        private bool SurveyExists(string id)
        {
            return db.Surveys.Count(e => e.SurveyId == id) > 0;
        }
    }
}
