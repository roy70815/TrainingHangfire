using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using TrainingHangfire.Common.Helpers.DapperHelper;
using TrainingHangfireRepositoryTests.Helper.Database;

namespace TrainingHangfireRepositoryTests
{
    [TestClass]
    public class TestHook
    {
        [AssemblyInitialize]
        public static async Task AssemblyInitialize(TestContext context)//TestContext ¤£¥[·|¿ù
        {
            var testDbHelper = new TestDatabase();
            await testDbHelper.CreateDatabase();
        }

        [AssemblyCleanup]
        public static async Task AssemblyCleanup()
        {
            var testDbHelper = new TestDatabase();
            await testDbHelper.DropDatabase();
        }

        public static ITestDatabase GetTestDatabase()
        {
            return new TestDatabase();

        }
    }
}
