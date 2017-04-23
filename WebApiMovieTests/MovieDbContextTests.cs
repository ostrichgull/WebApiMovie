using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApiMovie.Models;
using WebApiMovie.Controllers;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Moq;

namespace WebApiMovieTests
{
    [TestClass]
    public class MovieDbContextTests
    {
        [TestMethod]
        public void TestGet()
        {
            var data = new List<Movie>
                            {
                            new Movie { Title = "Movie 1" },
                            new Movie { Title = "Movie 2" },
                            new Movie { Title = "Movie 3" },
                            }.AsQueryable();

            var mockSet = new Mock<DbSet<Movie>>();
            mockSet.As<IQueryable<Movie>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Movie>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Movie>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Movie>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<MovieDBContext>();
            mockContext.Setup(c => c.Movies).Returns(mockSet.Object);

            var service = new MoviesController(mockContext.Object);
            var movies = service.Get();

            Assert.AreEqual(3, movies.Count());
            Assert.AreEqual("Movie 1", movies.ElementAt(0).Title);
            Assert.AreEqual("Movie 2", movies.ElementAt(1).Title);
            Assert.AreEqual("Movie 3", movies.ElementAt(2).Title);
        }
    }
}
