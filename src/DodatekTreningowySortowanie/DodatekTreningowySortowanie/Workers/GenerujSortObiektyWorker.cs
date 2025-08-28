using DodatekTreningowySortowanie;
using DodatekTreningowySortowanie.Workers;
using Soneta.Business;
using Soneta.Langs;
using Soneta.Types;
using System;

[assembly: Worker(typeof(GenerujSortObiektyWorker), typeof(SortObiekty))]
namespace DodatekTreningowySortowanie.Workers
{
    public  class GenerujSortObiektyWorker
    {
        public class GenerujSortObiektyWorkerParams : ContextBase
        {
            [Translate]
            public int  IleWygenerowac { get; set; }

            public GenerujSortObiektyWorkerParams(Context context) : base(context)
            {}
        }
        [Action("Generuj SortObiekty",
            Mode = ActionMode.SingleSession | ActionMode.Progress,
            Target = ActionTarget.Menu)]

        public object GenerujSortObiekty(Context context)
        {
            context.Remove(typeof(GenerujSortObiektyWorkerParams));
            var qci = QueryContextInformation.CreateForTypes(typeof(GenerujSortObiektyWorkerParams));
            qci.Context = context;
            qci.AcceptHandler = () => Generuj(context);

            return qci;

        }


        string[] miasta =
        [
            "Warszawa", "Kraków", "Łódź", "Wrocław", "Poznań",
            "Gdańsk", "Szczecin", "Bydgoszcz", "Lublin", "Katowice"
        ];
        string[] ulice =
        [
            "ul. Marszałkowska", "ul. Piotrkowska", "ul. Świętokrzyska", "ul. Długa", "ul. Grunwaldzka",
            "ul. Jana Pawła II", "ul. Krakowska", "ul. Główna", "ul. Kościuszki", "ul. Mickiewicza"
        ];
        int[] kody =
        [
            30222,11222,44222,99111,42222,44111
        ];

        private string Generuj(Context context)
        {
            var scanParams = (GenerujSortObiektyWorkerParams)context[typeof(GenerujSortObiektyWorkerParams)];

            int ile = scanParams.IleWygenerowac;

            using (Session session = context.Session.Login.CreateSession(false, false))
            {
                using (ITransaction trans = session.Logout(true))
                {

                    var ostatniKod = session.GetDodatekTreningowySortowanie().SortObiekty.WgKod.GetLast()?.KodObiektu;
                    int index = 0;
                    if (ostatniKod != null) index = Int32.Parse(ostatniKod);
                    for (int i = 1; i <= ile; i++)
                    {
  
                        Random r = new Random();
                        string kod = (index+i).ToString("D6");

                        var sortObiekt = new SortObiekt
                        {
                            KodObiektu = kod,
                            NazwaVirtual = $"n_{kod}",
                            Opis = $"Opis dla obiektu {typeof(SortObiekt)} mającego kod {kod}",
                            Cena = new Currency(r.NextDouble()),
                            DataObiektu = new Date(DateTime.Now.AddDays(r.Next(10))),
                            OkresObiektu = new FromTo(new Date(DateTime.Now.AddDays(-r.Next(10))), new Date(DateTime.Now.AddDays(r.Next(10))))
                        };
                        session.AddRow(sortObiekt);

                        var sortPodObiekt = new SortPodObiekt();
                        sortObiekt.SortPodObiekt = sortPodObiekt;
                        sortPodObiekt.KodPodObiektu = kod;
                        sortPodObiekt.NazwaPodObiektuVirtual = $"np_{kod}";
                        sortPodObiekt.OpisPodObiektu = $"Opis dla obiektu {typeof(SortPodObiekt)} mającego kod {kod}";
                        sortPodObiekt.OkresPodObiektu = new FromTo(new Date(DateTime.Now.AddDays(-r.Next(10))),
                            new Date(DateTime.Now.AddDays(r.Next(10))));
                        session.AddRow(sortPodObiekt);

                        var sortAdresExt = new SortAdresExt(sortObiekt)
                        {
                            SortAdres =
                            {
                                KodPocztowy = kody[r.Next(kody.Length)],
                                Ulica = ulice[r.Next(ulice.Length)],
                                Miejscowosc = miasta[r.Next(miasta.Length)],
                                NrLokalu = "" + r.Next(40),
                                NrDomu = ""+r.Next(100)
                            }
                        };
                        session.AddRow(sortAdresExt);

                        var sortRelObiekt = new SortRelObiekt(sortObiekt)
                        {
                            KodSortRelObiekt = kod,
                            NazwaSortRelObiektuVirtual = $"rb_{kod}",
                            OpisSortRelObiektu = $"Opis dla SortRelObiektu {typeof(SortRelObiekt)} mającego kod {kod}",
                            OkresSortRelObiektu = new FromTo(new Date(DateTime.Now.AddDays(-r.Next(10))), new Date(DateTime.Now.AddDays(r.Next(10))))
                        };
                        session.AddRow(sortRelObiekt);

                    }
                    trans.Commit();
                }
                session.Save();
            }


            return $"Dodano {ile} wierszy";
        }
    }
}

