using System.IO;
using System.Linq;
using System.Text;
using SaveDataFromList;
using Soneta.Business;
using Soneta.Magazyny;
using Soneta.Types;

[assembly: Worker(typeof(SaveSalesData), typeof(Obrot))]

namespace SaveDataFromList
{
    public class SaveSalesData
    {

        [Context]
        public Session Session { get; set; }

        [Context]
        public FromTo FromTo { get; set; }

        [Action("Zapisz dane do pliku",
            Target = ActionTarget.ToolbarWithText | ActionTarget.Menu | ActionTarget.LocalMenu | ActionTarget.Divider,
            Mode = ActionMode.SingleSession)]
        public NamedStream SaveDataWorker()
        {
            return new NamedStream("Dane.csv",
                () =>
                {
                    var writter = new StreamWriter(new MemoryStream(), Encoding.Unicode);

                    WriteHeader(writter);

                    var salesGroupByCode = GetSalesInPeriod().GroupBy(o => o.Towar.Kod);
                    GetDataToSave(writter, salesGroupByCode);

                    writter.Flush();

                    return ((MemoryStream)writter.BaseStream).ToArray();
                });
        }

        private static void GetDataToSave(StreamWriter writter,
            System.Collections.Generic.IEnumerable<IGrouping<string, Obrot>> salesGroupByCode)
        {
            var id = 1;
            decimal sumRozchod = 0, sumPrzychod = 0, sumMarza = 0;

            foreach (var o in salesGroupByCode)
            {
                if (!o.Any()) continue;

                writter.Write(id + "\t");

                var nazwaTowaru = o.ToList()[0].Towar.Nazwa;
                writter.Write(nazwaTowaru + "\t");

                var ilosc = o.Sum(t => t.Ilosc.Value);
                var symbol = o.Select(t => t.Ilosc.Symbol).First();
                writter.Write(ilosc + " " + symbol[0] + "\t");

                var rozchodWartosc = o.Sum(t => t.Rozchod.Wartosc);
                writter.Write(rozchodWartosc + "\t");
                sumRozchod += rozchodWartosc;

                var marza = o.Sum(t => t.Marża);
                writter.Write(marza + "\t");
                sumMarza += marza;

                var przychodWartosc = o.Sum(t => t.Przychod.Wartosc);
                writter.Write(przychodWartosc + "\t");
                sumPrzychod += przychodWartosc;

                id++;
                writter.WriteLine();
            }

            writter.Write("\t\tSUMA\t" + sumRozchod + "\t" + sumMarza + "\t" + sumPrzychod + "\t");
        }

        private System.Collections.Generic.IEnumerable<Obrot> GetSalesInPeriod()
            => MagazynyModule.GetInstance(Session)
                .Obroty.WgTowar
                .Where(o => FromTo.Contains(o.Data));

        private void WriteHeader(StreamWriter writter)
        {
            writter.Write("\t\tRAPORT SPRZEDAŻY TOWARÓW\t\n\n");
            writter.Write("Data raportu: " + Date.Today + "\n");
            writter.Write("Okres sprzedaży: " + FromTo + "\n\n");

            writter.Write("POZYCJA\t");
            writter.Write("TOWAR\t");
            writter.Write("ILOŚĆ OBROTU\t");
            writter.Write("ROZCHÓD WARTOŚĆ\t");
            writter.Write("MARŻA\t");
            writter.Write("PRZYCHÓD WARTOŚĆ\t");
            writter.WriteLine();
        }

    }

}
