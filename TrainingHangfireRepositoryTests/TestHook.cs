using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrainingHangfireRepositoryTests.Helper.Database;

namespace TrainingHangfireRepositoryTests
{
    [TestClass]
    public class TestHook
    {
        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)//TestContext ¤£¥[·|¿ù
        {
            var testDbHelper = new TestDatabase();
            testDbHelper.CreateDatabase();
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            var testDbHelper = new TestDatabase();
            testDbHelper.DropDatabase();
        }
    }
}
