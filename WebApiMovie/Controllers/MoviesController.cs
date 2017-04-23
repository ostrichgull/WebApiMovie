using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiMovie.Models;
using System.Data.Entity;
using System.Web.Mvc;

namespace WebApiMovie.Controllers
{
    public class MoviesController : ApiController
    {
        private MovieDBContext db = new MovieDBContext();

        public MoviesController(MovieDBContext context)
        {
            db = context;
        }

        public IEnumerable<Movie> Get()
        {

            var movies = from m in db.Movies
                         select m;
        
            return movies;
        }

        public Movie Get(int id)
        {
            Movie movie = db.Movies.Find(id);

            return movie;
        }

        public void Post([FromBody]Movie movie)
        {
            db.Movies.Add(movie);
            db.SaveChanges();
        }

        public void Put([FromBody]Movie movie)
        {
            db.Entry(movie).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            Movie movie = db.Movies.Find(id);
            db.Movies.Remove(movie);
            db.SaveChanges();
        }

        public HttpResponseMessage Options()
        {
            var resp = new HttpResponseMessage(HttpStatusCode.OK);
            return resp;
        }
    }
}
