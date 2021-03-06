using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using UnitTestApi.Controllers;
using UnitTestApi.Models;

namespace UnitTest.UnitTesting
{
    [TestClass]
    public class BookControllerTest: BuildTest
    {
        [TestMethod]
        public async Task getBooks() 
        {
            // Preparete
            var nameDb = Guid.NewGuid().ToString();
            var context = BuildContext(nameDb);
            context.Book.Add(new Book() { Author = "Charles R. Severance", Pages = 262, Title = "Pyhon Para Todos" });
            context.Book.Add(new Book() { Author = "Eugenia Bahit", Pages = 136, Title = "Pyhon Para Principiantes" });
            await context.SaveChangesAsync();

            //Test
            var context2 = BuildContext(nameDb);
            var controller = new BooksController(context2);
            var response = await controller.GetBook();

            //asserts
            var books = response.Value;
            Assert.AreEqual(2, books.ToList().Count);
        }

    }
}
