using EnovaDB.Punktacja;
using Soneta.Business.App;

[assembly: DatabaseInit(
    PunktDatabaseInitializer3.InitializerName,
    typeof(PunktDatabaseInitializer3), "Punktacja")]

namespace EnovaDB.Punktacja
{
    class PunktDatabaseInitializer3 : IDatabaseInitializer
    {
        internal const string InitializerName = "Konwersja punktów przy pomocy PunktDatabaseInitializer3";

        void IDatabaseInitializer.Initialize(Login login, int version)
        {
            // Nowa baza
            if (version == 0) return;
            // Nie jest przedmiotem tej konwersji
            if (version >= 3) return;

            Convert(login);
        }

        private static void Convert(Login login)
        {
            using (var session = login.CreateSession(false, false, InitializerName))
            {
                using (var transaction = session.Logout(true))
                {
                    foreach (Punkt punkt in session.Get<PunktacjaModule>().Punkty)
                    {
                        punkt.PrzeliczLiczbaNalezna1();
                    }

                    transaction.Commit();
                }

                session.Save();
            }
        }
    }
}