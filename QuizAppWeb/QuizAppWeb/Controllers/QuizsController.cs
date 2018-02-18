﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using QuizAppWeb.Models;

namespace QuizAppWeb.Controllers
{
    public class QuizsController : ApiController
    {
        private MusseumTestContext db = new MusseumTestContext();

        // GET: api/Quiz
        public IQueryable<Quiz> GetQuizs()
        {
            return db.Quiz;
        }

        // GET: api/Quiz/5
        [ResponseType(typeof(Quiz))]
        public IHttpActionResult GetQuiz(int id)
        {
            Quiz quiz = db.Quiz.Find(id);
            if (quiz == null)
            {
                return NotFound();
            }

            return Ok(quiz);
        }

        // PUT: api/Quiz/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutQuiz(int id, Quiz quiz)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != quiz.QuizId)
            {
                return BadRequest();
            }

            db.Entry(quiz).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuizExists(id))
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

        // POST: api/Quiz
        [ResponseType(typeof(Quiz))]
        public IHttpActionResult PostQuiz(Quiz quiz)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Quiz.Add(quiz);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = quiz.QuizId }, quiz);
        }

        // DELETE: api/Quiz/5
        [ResponseType(typeof(Quiz))]
        public IHttpActionResult DeleteQuiz(int id)
        {
            Quiz quiz = db.Quiz.Find(id);
            if (quiz == null)
            {
                return NotFound();
            }

            db.Quiz.Remove(quiz);
            db.SaveChanges();

            return Ok(quiz);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool QuizExists(int id)
        {
            return db.Quiz.Count(e => e.QuizId == id) > 0;
        }
    }
}