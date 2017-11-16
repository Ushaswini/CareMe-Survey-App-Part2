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

namespace Homework_04.Controllers
{
    [Authorize(Roles ="Admin")]
    [RoutePrefix("api/StudyGroups")]
    public class StudyGroupsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/StudyGroups
        public IQueryable<StudyGroup> GetStudyGroups()
        {
            return db.StudyGroups;
        }

        // GET: api/StudyGroups/5
        [ResponseType(typeof(StudyGroup))]
        public IHttpActionResult GetStudyGroup(string id)
        {
            StudyGroup studyGroup = db.StudyGroups.Find(id);
            if (studyGroup == null)
            {
                return NotFound();
            }

            return Ok(studyGroup);
        }

        // PUT: api/StudyGroups/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStudyGroup(string id, StudyGroup studyGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != studyGroup.StudyGroupId)
            {
                return BadRequest();
            }

            db.Entry(studyGroup).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudyGroupExists(id))
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

        // POST: api/StudyGroups
        [ResponseType(typeof(StudyGroup))]
        public IHttpActionResult PostStudyGroup(StudyGroup studyGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.StudyGroups.Add(studyGroup);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (StudyGroupExists(studyGroup.StudyGroupId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = studyGroup.StudyGroupId }, studyGroup);
        }

        // DELETE: api/StudyGroups/5
        [ResponseType(typeof(StudyGroup))]
        public IHttpActionResult DeleteStudyGroup(string id)
        {
            StudyGroup studyGroup = db.StudyGroups.Find(id);
            if (studyGroup == null)
            {
                return NotFound();
            }

            db.StudyGroups.Remove(studyGroup);
            db.SaveChanges();

            return Ok(studyGroup);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StudyGroupExists(string id)
        {
            return db.StudyGroups.Count(e => e.StudyGroupId == id) > 0;
        }
    }
}