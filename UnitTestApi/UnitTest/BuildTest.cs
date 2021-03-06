using Microsoft.EntityFrameworkCore;
using UnitTestApi.Data;

namespace UnitTest
{
    public class BuildTest
    {
        protected UnitTestApiContext BuildContext(string nameDb)
        {
            var opciones = new DbContextOptionsBuilder<UnitTestApiContext>()
                                .UseInMemoryDatabase(nameDb).Options;

            return new UnitTestApiContext(opciones);
        }
    }
}
