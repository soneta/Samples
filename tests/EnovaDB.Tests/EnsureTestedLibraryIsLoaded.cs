using EnovaDB.Punktacja;
using NUnit.Framework;

namespace EnovaDB.Integration.Tests
{
    /// <summary>
    /// Klasa ta służy do tego, aby testowana biblioteka została załadowana odpowiednio wcześnie.
    /// Dzięki temu możliwa jest rejestracja handlerów, obsługa dbinitów czy dodanie naszych tabel.
    /// </summary>
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
