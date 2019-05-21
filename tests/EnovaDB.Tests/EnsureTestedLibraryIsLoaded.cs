using EnovaDB.Punktacja;
using NUnit.Framework;

namespace EnovaDB.Integration.Tests
{
    [SetUpFixture]
    public class Config
    {
        [OneTimeSetUp]
        public void SetUp()
        {
            _ = new PunktyDokumentuWorker();
        }
    }
}
