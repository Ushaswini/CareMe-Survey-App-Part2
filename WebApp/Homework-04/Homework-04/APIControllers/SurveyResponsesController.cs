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
using Newtonsoft.Json;
using Homework_04.Surveys;
using Homework_04.DTOs;

namespace Homework_04.Controllers
{
    [RoutePrefix("api/SurveyResponses")]
    public class SurveyResponsesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/SurveyResponses
        public IQueryable<SurveyResponse> GetSurveyResponses()
        {
            return db.SurveyResponses;
        }

        // GET: api/SurveyResponses/5
        [ResponseType(typeof(SurveyResponse))]
        public IHttpActionResult GetSurveyResponse(string id)
        {
            SurveyResponse surveyResponse = db.SurveyResponses.Find(id);
            if (surveyResponse == null)
            {
                return NotFound();
            }

            return Ok(surveyResponse);
        }

        [ResponseType(typeof(SurveyResponse))]
        public IList<ResponseDTO> GetSurveyResponseForStudy(string studyGroupId)
        {
            var result = db.SurveyResponses.Include(r => r.StudyGroup)
                                            .Include(r => r.Survey)
                                            .Include(r => r.User).Where(r => r.StudyGroupId == studyGroupId)
                                            .Select(r => new ResponseDTO {
                                                StudyGroupName = r.StudyGroup.StudyName,
                                                SurveyId = r.SurveyId,
                                                UserName = r.User.UserName,
                                                SurveyResponseReceivedTime = r.SurveyResponseReceivedTime,
                                                SurveyComments = r.SurveyComments
                                            });
            return result.ToList();

        }

        // PUT: api/SurveyResponses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSurveyResponse(string id, SurveyResponse surveyResponse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != surveyResponse.SurveyResponseId)
            {
                return BadRequest();
            }

            db.Entry(surveyResponse).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SurveyResponseExists(id))
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

        // POST: api/SurveyResponses
        [ResponseType(typeof(SurveyResponse))]
        public IHttpActionResult PostSurveyResponse(SurveyResponse surveyResponse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           var comments = CalculateScore(surveyResponse);
           surveyResponse.SurveyComments = JsonConvert.SerializeObject(comments);
           surveyResponse.SurveyResponseId = System.Guid.NewGuid().ToString();
            db.SurveyResponses.Add(surveyResponse);

            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                if (SurveyResponseExists(surveyResponse.SurveyResponseId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = surveyResponse.SurveyResponseId }, surveyResponse);
        }

        private List<string> CalculateScore(SurveyResponse surveyResponse)
        {
            int score = 0;
            List<string> scoreComments = new List<string>();
            QuestionDecoded q = JsonConvert.DeserializeObject<QuestionDecoded>(((object)surveyResponse.UserResponseText).ToString());
            List<SurveyQuestionAnswer> answers = JsonConvert.DeserializeObject <List<SurveyQuestionAnswer>>(q.Data.ToString());
            for(int i = 1; i<= answers.Count; i++)
            {
                if(i <= 30)
                {
                    score += answers[i-1].AnswerValue;
                }
                if(i == 3)
                {
                    if(score == 21) { scoreComments.Add("Adherent"); }
                    else { scoreComments.Add("Not-Adherent"); }
                    score = 0;
                }

                if(i == 14)
                {
                    if(score >= 52) { scoreComments.Add("Adherent to Diet"); }
                    else if(score > 33 && score < 51) { scoreComments.Add("Medium Diet Quality"); }
                    else { scoreComments.Add("Low Diet Quality"); }
                    score = 0;
                }

                if(i == 16)
                {
                    if(i >= 8) { scoreComments.Add("Physical Activity Adherent"); }
                    else { scoreComments.Add("Non-Adherent to Physical Activity"); }
                    score = 0;
                }
                if(i == 18)
                {
                    score = 0;
                }
                if(i == 20)
                {
                    if(score == 0) { scoreComments.Add("Adherent"); }
                    else { scoreComments.Add("Non-Adherent"); }
                    score = 0;
                }
                if(i == 30)
                {
                    if(score >= 40) { scoreComments.Add("Adherent to good weight management process"); }
                    else { scoreComments.Add("Non-Adherent to good weight management process"); }
                    score = 1;
                }

                if(i > 30)
                {
                    score *= answers[i-1].AnswerValue;
                }
                if(i == 32)
                {
                    if(score <= 14) { scoreComments.Add("Adherent to good drink management process"); }
                    else { scoreComments.Add("Non-Adherent to good drink management process"); }
                }
            }

            return scoreComments;
        }

        // DELETE: api/SurveyResponses/5
        [ResponseType(typeof(SurveyResponse))]
        public IHttpActionResult DeleteSurveyResponse(string id)
        {
            SurveyResponse surveyResponse = db.SurveyResponses.Find(id);
            if (surveyResponse == null)
            {
                return NotFound();
            }

            db.SurveyResponses.Remove(surveyResponse);
            db.SaveChanges();

            return Ok(surveyResponse);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SurveyResponseExists(string id)
        {
            return db.SurveyResponses.Count(e => e.SurveyResponseId == id) > 0;
        }
    }
}