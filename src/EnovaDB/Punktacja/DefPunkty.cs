
//
// Plik zawiera logikę biznesową kolekcji obiektów definicji punktacji.
// Taki plik należy przygotować dla każdego obiektu biznesowego zdefiniowanego w pliku *.business.xml.
// Kolekcja ta zarządza obiektami biznesowymi pewnego typu, tutaj definicjami punktów. Znajdują się
// w niej metody, które nie są specyficzne dla poszczególnych obiektów biznesowych,
//

//
// Wszystkie obiekty biznesowe zostały umieszczone w jednym namespace, którego nazwa została również 
// umieszczona w pliku *.business.xml (module/@namespace).
//

using System;

namespace Soneta.Examples.EnovaDB.Punktacja
{
    //
    // Nazwa klasy kolekcji obiektów biznesowych brana jest z nazwy elementu table znajdującego się w 
    // *.business.xml (atrybut table/@tablename).
    // Klasa ta musi dziedziczyć z klasy bazowej znajdującej się w wygenerowanym przez Soneta.Generator module
    // PunktacjaModule i klasy DefinicjaPunktuTable (do nazwy atrybutu @name dodano sufiks Table).
    // W klasie tej znajduje się między innymi implementacja properties reprezentujących klucze dostarczające 
    // obiekty w zadanej kolejności oraz metody pozwajające na odczytanie obiektu o zadanym Guid lub ID.
    //
    public class DefPunkty : PunktacjaModule.DefinicjaPunktuTable
    {
        //
        // Identyfikatora GUID, który został użyty w pliku Punktu.dbinit.xml
        //
        public static readonly Guid StandardowaGuid = new Guid("72BF1FB2-B395-49c7-84D5-E0AFA27AE52B");

        //
        // Property zwracające standardową definicję przypisywaną dla każdego punktu.
        // Zapis odczytywany jest na podstawie identyfikatora GUID, który został użyty w pliku Punktu.dbinit.xml
        //
        public DefinicjaPunktu Standardowa
        {
            get
            {
                //
                // Każda tabela guided="Root" posiada indekser pozwalający odczytać 
                // obiekt biznesowy na podstawie podanego guid.
                return this[StandardowaGuid];
            }
        }
    }
}
