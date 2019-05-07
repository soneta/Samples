using System.ComponentModel;
using System.Globalization;
using Soneta.Business;
using Soneta.Examples.EnovaDB.Punktacja;
using Soneta.Handel;

//
// Plik zawiera logikę biznesową obiektów punktów.
// Taki plik należy przygotować dla każdego obiektu biznesowego zdefiniowanego w pliku *.business.xml.
// Praktycznie cała logika biznesowa zostaje przeniesiona z bazowego obiektu, który jest automatycznie 
// generowany przez Soneta.Generator. 
//

//
// Atrybut określający, które obiekty biznesowe moją być dodawane do tablicy. Wykorzystywany jest 
// podczas dodawania nowego zapisu. Jeżeli dla jednej tabeli jest więcej atrybutów NewRow, to 
// po naciśnięciu klawisza INS zostanie wyświetlone menu. Brak atrybutu uniemożliwi dodawanie nowych
// zapisów przez operatora.
//
[assembly: NewRow(typeof(Punkt))]

//
// Wszystkie obiekty biznesowe zostały umieszczone w jednym namespace, którego nazwa została również 
// umieszczona w pliku *.business.xml (module/@namespace).
//
namespace Soneta.Examples.EnovaDB.Punktacja
{

    //
    // Nazwa klasy obiektu biznesowego brana jest z nazwy elementu table znajdującego się w 
    // *.business.xml (atrybut table/@name).
    // Klasa ta musi dziedziczyć z klasy bazowej znajdującej się w wygenerowanym przez Soneta.Generator module
    // PunktacjaModule i klasy PunktRow (dodano sufiks Row).
    // W klasie tej znajduje się między innymi implementacja properties biznesowych pochodzących z bazy danych.
    //
    public class Punkt : PunktacjaModule.PunktRow
    {

        //
        // Ponieważ pole dokument określono jako readonly i jest ono inicjowane tylko podczas tworzenia nowego
        // obiektu biznesowego, wartość inicjującą należy przekazać jako parametry konstruktora. Dlatego 
        // należy zdefiniować potrzebne konstruktory.
        // Ten konstruktor wykorzystywany jest przez bibliotekę do tworzenia obiektów biznesowych odczytanych 
        // z bazy danych. Nie będzie wykorzystywany przez programistę.
        // Należy wywołać konstruktor bazowy.
        //
        public Punkt(RowCreator creator)
            : base(creator)
        {
        }

        //
        // Konstruktor inicjujący nowo utworzony obiekt biznesowy, które będzie dodany do bazy danych.
        // Parametrami konstruktora są pola zaznaczone jako readonly. Ustawienie wartości tych pól 
        // później nie będzie już możliwe.
        // Należy wywołać konstruktor bazowy.
        //
        public Punkt(DokumentHandlowy dokument)
            : base(dokument)
        {
        }

        // Tutaj została określona metoda wyliczania napisu reprezentującego dany obiekt biznesowy.
        // Dla obiektu punktu jest to nazwa definicji i liczba przypisanych punktów.
        // Metoda ta będzie wykorzystywana wszędzie tam, gdzie trzeba będzie podać obólną informację 
        // o danym obiekcie biznesowym, np w komunikatach o błędach.
        //
        public override string ToString()
        {
            //
            // Pomimo, że wartość pola Definicja jest required, to należy sprawdzić czy nie jest null. 
            // Ponieważ pole nie jest readonly oznacza to, że może być jeszcze nie zainicjowane.
            // Gdyby odwołanie było zrobione do pola Dokument, weryfikacja nie była by potrzebna, gdyż
            // o pole jest readonly i required, czyli jest inicjowane zawsze w konstruktorze obiektu.
            //
            var liczba = Liczba.ToString(CultureInfo.InvariantCulture);

            return Definicja != null 
                ? Definicja + " " + liczba 
                : liczba;
        }

        //
        // Metoda wywoływana po dodanio obiektu biznesowego do tabeli w sesji. Jest to miejsce, gdzie można
        // robić wszelkiego rodzaju inicjację obiektu, gdy potrzeba jest nam obiekt session. Czyli ogólnie
        // dostęp do innych obiektów biznesowych.
        //
        // Metoda inicjuje obiekt punktu standardową definicją punktu.
        //
        protected override void OnAdded()
        {
            //
            // Na początku konieczne jest wywołanie metody bazowej OnAdded()
            //
            base.OnAdded();
            //
            // Zainicjowanie pola Definicja wartością standardową.
            // Z modułu odczytywany jest obiekt tabeli definicji punktów, który zawiera property
            // Standardowy zwracające standardową definicję punktu.
            // Operację tę można wykonać dopiero w metodzie OnAdded(), ponieważ wcześniej
            // obiekt nie jest przypisany do sesji i odwołanie do property Module zakończyło by się błędem.
            //
            Definicja = Module.DefPunkty.Standardowa;
        }

        //
        // Property wyliczeniowe dodane do obiektu biznesowego. Jego zadaniem jest policzenie faktycznej ilość
        // punktów wynikających z danego zapisu. Chodzi o to, że ilość wprowadzonych punktów musi być 
        // pomnożona przez mnożnik wynikający z definicji.
        //
        [Description("Należna liczba punktów po uwzględnieniu mnożnika.")]
        public int LiczbaNależna
        {
            get
            {
                //
                // Pomimo, że wartość pola Definicja jest required, to należy sprawdzić czy nie jest null. 
                // Ponieważ pole nie jest readonly oznacza to, że może być jeszcze nie zainicjowane.
                //
                if (Definicja == null) return 0;
                //
                // Wyliczenie wartość przez pomnożenie odpowiednich liczb.
                //
                return Liczba * Definicja.Mnoznik;
            }
        }

        public override DefinicjaPunktu Definicja {
            get { return base.Definicja; }
            set
            {
                base.Definicja = value;
                // Ustawienie definicji powoduje przliczenie cache'owanej liczby należnej
                PrzeliczLiczbaNalezna1();
                PrzeliczLiczbaNalezna2();
            }
        }

        public override int Liczba
        {
            get { return base.Liczba; }
            set
            {
                base.Liczba = value;
                // Ustawienie liczby powoduje przliczenie cache'owanej liczby należnej
                PrzeliczLiczbaNalezna1();
                PrzeliczLiczbaNalezna2();
            }
        }

        internal void PrzeliczLiczbaNalezna1()
        {
            baseLiczbaNalezna1 = LiczbaNależna;
        }

        internal void PrzeliczLiczbaNalezna2()
        {
            baseLiczbaNalezna2 = LiczbaNależna;
        }
    }
}
