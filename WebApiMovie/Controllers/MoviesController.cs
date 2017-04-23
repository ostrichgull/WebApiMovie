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
        private MovieDBContext _db;

        public MoviesController()
        {
            this._db = new MovieDBContext();
        }

        public MoviesController(MovieDBContext db)
        {
            this._db = db;
        }

        public IEnumerable<Movie> Get()
        {

            var movies = from m in this._db.Movies
                         select m;
        
            return movies;
        }

        public Movie Get(int id)
        {
            Movie movie = this._db.Movies.Find(id);

            return movie;
        }

        public void Post([FromBody]Movie movie)
        {
            this._db.Movies.Add(movie);
            this._db.SaveChanges();
        }

        public void Put([FromBody]Movie movie)
        {
            this._db.Entry(movie).State = EntityState.Modified;
            this._db.SaveChanges();
        }

        public void Delete(int id)
        {
            Movie movie = this._db.Movies.Find(id);
            this._db.Movies.Remove(movie);
            this._db.SaveChanges();
        }

        public HttpResponseMessage Options()
        {
            var resp = new HttpResponseMessage(HttpStatusCode.OK);
            return resp;
        }
    }
}
