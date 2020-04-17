using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MovieStore.Controllers;

using MovieStore.Data;
using MovieStore.Models;

namespace MovieStore.Tests.Controllers
{
    [TestClass]
    public class MovieStoreControllerTest
    {
        [TestMethod]
        public void MovieStore_Index_TestView()
        {
            //Arrange 

            MoviesController controller = new MoviesController();

            //Act

            ViewResult result = controller.Index() as ViewResult;

            //Assert

            Assert.IsNotNull(result);


        }
        [TestMethod]
        public void MovieStore_ListOfMovies()
        {
            //Arrange 

            MoviesController controller = new MoviesController();

            //Act
            List<Movie> result = controller.ListOfMovies();

            //Assert

            Assert.IsNotNull(result);
            Assert.AreEqual(expected: "Iron Man 1", actual: result[0].Title);
            Assert.AreEqual(expected: "Iron Man 2", actual: result[1].Title);
            Assert.AreEqual(expected: "Iron Man 3", actual: result[2].Title);

        }





        [TestMethod]
        public void MovieStore_IndexRedirect_Sucess()
        {
            //Arrange 

            MoviesController controller = new MoviesController();

            //Act

            RedirectToRouteResult result = controller.IndexRedirect(id: 1) as RedirectToRouteResult;

            //Assert

            Assert.IsNotNull(result);
            Assert.AreEqual(expected: "Create", actual: result.RouteValues["action"]);
            Assert.AreEqual(expected: "HomeController", actual: result.RouteValues["controller"]);

        }
        [TestMethod]
        public void MovieStore_IndexRedirect_BadRequest()
        {
            //Arrange 

            MoviesController controller = new MoviesController();

            //Act

            HttpStatusCodeResult result = controller.IndexRedirect(id: 0) as HttpStatusCodeResult;

            //Assert

            Assert.AreEqual(expected: HttpStatusCode.BadRequest, actual: (HttpStatusCode)result.StatusCode);

        }


        [TestMethod]
        public void MovieStore_ListFromDb()
        {
            //Goal:Query from our own list instead of database.

            //Step 1
            var list = new List<Movie>
            {
                new Movie() {
                    MovieID = 1,
                    Title = "Superman 1"
                },

                new Movie() {
                    MovieID = 2,
                    Title = "Superman 2"
                }

            }.AsQueryable();

            //Step 2

            Mock<MovieStoreDbContext> mockContext = new Mock<MovieStoreDbContext>();
            Mock<DbSet<Movie>> mockSet = new Mock<DbSet<Movie>>();



            //Step 3 
            mockSet.As<IQueryable<Movie>>().Setup(expression: m => m.GetEnumerator()).Returns(list.GetEnumerator());
            mockSet.As<IQueryable<Movie>>().Setup(expression: m => m.Provider).Returns(list.Provider);
            mockSet.As<IQueryable<Movie>>().Setup(expression: m => m.ElementType).Returns(list.ElementType);


            //Step 4
            mockContext.Setup(expression: db => db.Movies).Returns(mockSet.Object);

            //Arrange 

            MoviesController controller = new MoviesController(mockContext.Object);


            //Act

            ViewResult result = controller.ListFromDb() as ViewResult;
            List<Movie> resultMovies = result.Model as List<Movie>;

            //Assert

            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void MovieStore_Details_Sucess()
        {
            //Goal:Query from our own list instead of database.

            //Step 1
            var list = new List<Movie>
            {
                new Movie() {
                    MovieID = 1,
                    Title = "Superman 1"
                },

                new Movie() {
                    MovieID = 2,
                    Title = "Superman 2"
                }

            }.AsQueryable();

            //Step 2

            Mock<MovieStoreDbContext> mockContext = new Mock<MovieStoreDbContext>();
            Mock<DbSet<Movie>> mockSet = new Mock<DbSet<Movie>>();



            //Step 3 
            mockSet.As<IQueryable<Movie>>().Setup(expression: m => m.GetEnumerator()).Returns(list.GetEnumerator());
            mockSet.As<IQueryable<Movie>>().Setup(expression: m => m.Provider).Returns(list.Provider);
            mockSet.As<IQueryable<Movie>>().Setup(expression: m => m.ElementType).Returns(list.ElementType);
            mockSet.Setup(expression: m => m.Find(It.IsAny<Object>())).Returns(list.First());


            //Step 4
            mockContext.Setup(expression: db => db.Movies).Returns(mockSet.Object);

            //Arrange 

            MoviesController controller = new MoviesController(mockContext.Object);


            //Act

            ViewResult result = controller.Details(id: 1) as ViewResult;


            //Assert

            Assert.IsNotNull(result);

        }
        [TestMethod]
        public void MovieStore_Details_IdIsNull()
        {
            //Goal:Query from our own list instead of database.

            //Step 1
            var list = new List<Movie>
            {
                new Movie() {
                    MovieID = 1,
                    Title = "Superman 1"
                },

                new Movie() {
                    MovieID = 2,
                    Title = "Superman 2"
                }

            }.AsQueryable();

            //Step 2

            Mock<MovieStoreDbContext> mockContext = new Mock<MovieStoreDbContext>();
            Mock<DbSet<Movie>> mockSet = new Mock<DbSet<Movie>>();



            //Step 3 
            mockSet.As<IQueryable<Movie>>().Setup(expression: m => m.GetEnumerator()).Returns(list.GetEnumerator());
            mockSet.As<IQueryable<Movie>>().Setup(expression: m => m.Provider).Returns(list.Provider);
            mockSet.As<IQueryable<Movie>>().Setup(expression: m => m.ElementType).Returns(list.ElementType);
            mockSet.Setup(expression: m => m.Find(It.IsAny<Object>())).Returns(list.First());


            //Step 4
            mockContext.Setup(expression: db => db.Movies).Returns(mockSet.Object);

            //Arrange 

            MoviesController controller = new MoviesController(mockContext.Object);


            //Act

            HttpStatusCodeResult result = controller.Details(id: null) as HttpStatusCodeResult;


            //Assert

            Assert.AreEqual(expected: HttpStatusCode.BadRequest, actual: (HttpStatusCode)result.StatusCode);

        }
        [TestMethod]
        public void MovieStore_Details_MovieIsNull()
        {
            //Goal:Query from our own list instead of database.

            //Step 1
            var list = new List<Movie>
            {
                new Movie() {
                    MovieID = 1,
                    Title = "Superman 1"
                },

                new Movie() {
                    MovieID = 2,
                    Title = "Superman 2"
                }

            }.AsQueryable();

            //Step 2

            Mock<MovieStoreDbContext> mockContext = new Mock<MovieStoreDbContext>();
            Mock<DbSet<Movie>> mockSet = new Mock<DbSet<Movie>>();



            //Step 3 
            mockSet.As<IQueryable<Movie>>().Setup(expression: m => m.GetEnumerator()).Returns(list.GetEnumerator());
            mockSet.As<IQueryable<Movie>>().Setup(expression: m => m.Provider).Returns(list.Provider);
            mockSet.As<IQueryable<Movie>>().Setup(expression: m => m.ElementType).Returns(list.ElementType);
          

            Movie movie = null;

            mockSet.Setup(expression: m => m.Find(It.IsAny<Object>())).Returns(movie);


            //Step 4
            mockContext.Setup(expression: db => db.Movies).Returns(mockSet.Object);

            //Arrange 

            MoviesController controller = new MoviesController(mockContext.Object);


            //Act

            HttpStatusCodeResult result = controller.Details(id: 0) as HttpStatusCodeResult;


            //Assert

            Assert.AreEqual(expected: HttpStatusCode.NotFound, actual: (HttpStatusCode)result.StatusCode);

        }
    }

}
