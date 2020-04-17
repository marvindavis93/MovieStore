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
using System.Web;



namespace MovieStore.Tests
{
    [TestClass]
    public class MovieStoreTest2
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





        [TestMethod]
        public void MovieStore_Create_ModelStateIsInvalid()
        {
            // Arrange
            Movie movie = null;
            MoviesController controller = new MoviesController();
            controller.ModelState.AddModelError("Test", "Model state is forced to be invalid");

            // Act
            ActionResult result = controller.Create(movie);

            // Assert
            // TODO: Finish Assertion

            Assert.IsNotNull(result);



        }

        [TestMethod]
        public void MovieStore_Create_ModelStateIsValid()
        {
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

            ViewResult result = controller.Create() as ViewResult;


            //Assert

            Assert.IsNotNull(result);

        }
        [TestMethod]
        public void MovieStore_Delete_IsNull()
        {
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

