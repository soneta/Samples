// Plik zawiera logikę biznesową kolekcji obiektów punktów.
// Taki plik należy przygotować dla każdego obiektu biznesowego zdefiniowanego w pliku *.business.xml.
// Kolekcja ta zarządza obiektami biznesowymi pewnego typu, tutaj punktami. Znajdują się
// w niej metody, które nie są specyficzne dla poszczególnych obiektów biznesowych,
// 
// Przedstawiony poniżej przykład pokazuje w jaki sposób podłączyć własny kod do properties biznesowych
// innych modułów. W tym przypadku podłączany kod wywoływany jest po zmianie wartości property biznesowego
// pozycji dokumentu handlowego 'Ilosc'
//

//
// Wszystkie obiekty biznesowe zostały umieszczone w jednym namespace, którego nazwa została również 
// umieszczona w pliku *.business.xml (module/@namespace).
//

using Soneta.Handel;

namespace Soneta.Examples.EnovaDB.Punktacja
{

    //
    // Nazwa klasy kolekcji obiektów biznesowych brana jest z nazwy elementu table znajdującego się w 
    // *.business.xml (atrybut table/@tablename).
    // Klasa ta musi dziedziczyć z klasy bazowej znajdującej się w wygenerowanym przez Soneta.Generator module
    // PunktacjaModule i klasy PunktTable (do nazwy atrybutu @name dodano sufiks Table).
    // W klasie tej znajduje się między innymi implementacja properties reprezentujących klucze dostarczające 
    // obiekty w zadanej kolejności oraz metody pozwajające na odczytanie obiektu o zadanym ID.
    //
    public class Punkty : PunktacjaModule.PunktTable
    {
        //
        // Konstruktor statyczny wywoływany jest tylko raz po uruchomieniu programu i pozwala na zarejestrowanie 
        // metod (trigger-ów) wywoływanych na zmianę wartości w bazodanowych obiektach biznesowych.
        // Dostępne są dwa typy metod rejestrujących:
        //		AddXXXXXBeforeEdit - rejestruje delegacje wywoływaną przed ustawieniem wartości obiektów biznesowych.
        //			Pozwala na sprawdzenie wartości przed podstawieniem, a nawet zmianę wartości. Zmiana wartości musi
        //			być dokonywana bardzo ostrorznie, ponieważ wykonywana jest już po wykonaniu kodu biznesowego obiektu.
        //		AddXXXXXAfterEdit - rejestruje delegacje wywoływaną po ustawieniu wartości obiektów biznesowych.
        //			Pozwala na tworzenie kodu reagującego na zmianę wartości property. Można np przeliczyć wartość innego property.
        //
        static Punkty()
        {
            //
            // Rejestracja delegacji wywoływanej po zmianie property Ilosc w obiekcie pozycji dokumentu handlowego.
            // Delegaja przelicza ilość punktów przypisanych do dokumentu.
            // HandelModule - odwołuje się do modułu HANDEL w programie enova.
            // PozycjaDokHandlowegoSchema - zawiera wszystkie deklaracje metod rejestracji delegacji w tabeli
            //		pozycji dokumentów handlowych.
            // AddIloscAfterEdit - metoda dodająca delegację po zmiane ilości pozycji.
            //
            HandelModule.PozycjaDokHandlowegoSchema.AddIloscAfterEdit(PrzeliczaniePunktowHelper.Przelicz);
            HandelModule.PozycjaDokHandlowegoSchema.AddOnDeleted(PrzeliczaniePunktowHelper.Przelicz);
        }
    }
}
