using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Homework_04.Models;
using Homework_04.DTOs;
using PushBots.NET;
using PushBots.NET.Models;
using PushBots.NET.Enums;

namespace Homework_04.Controllers
{
    [RoutePrefix("api/Surveys")]
    public class SurveysController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private readonly PushBotsClient _pushBotsClient;
        private const string AppId = "5a0c68d49b823ae75c8b4568";
        private const string Secret = "2ea297eee9933ab16e23bbc28d1cf2c1";

        public SurveysController()
        {
            _pushBotsClient = new PushBotsClient(AppId, Secret);
        }

        protected PushBotsClient PushBots_Client
        {
            get
            {
                return _pushBotsClient;
            }
        }

        // GET: api/Surveys
        public IList<SurveyDTO> GetSurveys()
        {
            return db.Surveys.Include(s => s.StudyGroup).Select(s => new SurveyDTO {
                        SurveyId = s.SurveyId,
                        SurveyCreatedTime = s.SurveyCreatedTime,
                        QuestionText = s.QuestionText,
                        StudyGroupId = s.StudyGroupId,
                        StudyGroupName = s.StudyGroup.StudyName

            }).ToList();
        }

        public IList<SurveyDTO> GetSurveysForStudyGroup(string studyGroupId)
        {
            var surveys = db.Surveys.Where(s => s.StudyGroupId == studyGroupId).Include(s => s.StudyGroup).Select(s => new SurveyDTO
            {
                SurveyId = s.SurveyId,
                SurveyCreatedTime = s.SurveyCreatedTime,
                QuestionText = s.QuestionText,
                StudyGroupId = s.StudyGroupId,
                StudyGroupName = s.StudyGroup.StudyName

            });

            foreach(var item in surveys)
            {

            }

            return surveys.ToList();
        }

        // GET: api/Surveys/5
        [ResponseType(typeof(Survey))]
        public IHttpActionResult GetSurvey(string id)
        {
            Survey survey = db.Surveys.Find(id);
            if (survey == null)
            {
                return NotFound();
            }

            return Ok(survey);
        }

        // PUT: api/Surveys/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSurvey(string id, Survey survey)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != survey.SurveyId)
            {
                return BadRequest();
            }

            db.Entry(survey).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SurveyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Surveys
        [ResponseType(typeof(Survey))]
        public IHttpActionResult PostSurvey(Survey survey)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Surveys.Add(survey);

            try
            {
                db.SaveChanges();
                PushNotificationsAsync();
                //push notification to users who opted in
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

        
        private void PushNotificationsAsync()
        {
            var pushMessage = new BatchPush()
            {
                Message = "Survey Received!!",
                Badge = "+1",
                Platforms = new[] { Platform.Android, Platform.iOS }
            };

            var result =  PushBots_Client.Push(pushMessage);

        }
        // DELETE: api/Surveys/5
        [ResponseType(typeof(Survey))]
        public IHttpActionResult DeleteSurvey(string id)
        {
            Survey survey = db.Surveys.Find(id);
            if (survey == null)
            {
                return NotFound();
            }

            db.Surveys.Remove(survey);
            db.SaveChanges();

            return Ok(survey);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SurveyExists(string id)
        {
            return db.Surveys.Count(e => e.SurveyId == id) > 0;
        }
    }
}