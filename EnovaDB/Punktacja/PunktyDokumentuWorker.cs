using System.ComponentModel;
using System.Linq;
using EnovaDB.Punktacja;
using Soneta.Business;
using Soneta.Handel;

//
// Zadaniem workera jest dodanie do dokumentu handlowego workera, który będzie dla tegoż dokumentu
// zwracał listę dołączonych do niego punktów. Dodatkowo worker będzie informował o sumarycznej ilości 
// naliczonych dokumentowi.
// Ogólnie mówiąc, worker ten dodaje do obiektu dokumnet handlowy logikę biznesową, która służy do 
// obsługi punktów tego dokumentu. Ponieważ dokument handlowy jest obiektem zaimplementowanym
// i dostarczonym przez program enova, nie można go rozbudowywać o nowe właściwości. Stąd istnieje
// potrzeba budowy takigo workera.
// Wywołanie właściwości tego workera na liście będzie odbywać się za pomocą:
//		Workers.PunktyDokumentu.Punkty
//		Workers.PunktyDokumentu.SumaPunktów
//

//
// Klasa definiująca dany worker musi być wcześniej zarejestrowana.
// Dokonujemy tego za pomocą następujacego wpisu.
//
[assembly: Worker(
    //
    // Pierwszym parametrem jest podanie nazwy klasy zawierającej implementacje
    // danego workera. Implementacja tej klasy znajduje się w tym samym pliku, poniżej.
    //
    typeof(PunktyDokumentuWorker),
    //
    // Drugim parametrem jest typ obiektu, do którego przypisany jest dany worker.
    // W tym przypadku jest to obiekt dokumentu handlowego.
    //
    typeof(DokumentHandlowy))]

namespace EnovaDB.Punktacja
{
    // 
    // Klasa implementująca metodę wywoływaną przez dany worker.
    //
    public class PunktyDokumentuWorker
    {
        //
        // Przygotować property, które będzie inicjowane przez system enova
        // z aktualnego kontekstu. Property będzie zainicjowane przed wywołaniem 
        // metod i properties workera.
        //

        //
        // Property posiada atrybut [Context] określający, że należy go zainicjować 
        // z kontekstu (Soneta.Business.Context).
        //
        [Context]
        public DokumentHandlowy Dokument { get; set; }

        //
        // Property wyliczające listę punktów powiązanych z danym dokumentem handlowym.
        // Property to pozwoli na zbudowanie nowej zakładki dokumentu handlowego, zawierającej 
        // listę punktów. Lista (Grid) znajdująca się na tej zakładce będzie podpięta (binding)
        // do tego właśnie property.
        //
        [Description("Lista zapisów punktów przypisanych do dokumentu handlowego.")]
        public SubTable Punkty
        {
            get
            {
                //
                // Poniewaź property Dokument jest zainicjowane, możemy go wykorzystać do 
                // wyciągnięcia modułu Punktacja (po przez obiekt Session), z którego będziemy 
                // mieli dostęp do odpowiedniego klucza.
                //
                var punktacjaModule = PunktacjaModule.GetInstance(Dokument);
                //
                // Po wyciągnięciu modułu, w tabeli Punkty znajduje się klucz WgDokumentu,
                // który zawiera obiekty Punkt według klucza dokument. Można wykorzystać 
                // odpowiedni indekserm żeby odczytać listę punktów dokumentu.
                //
                return punktacjaModule.Punkty.WgDokument[Dokument];
            }
        }

        //
        // Podstawową informacją dostarczaną przez moduł punktacji jest liczba punktów
        // przypisanych do danego dokumentu. To property służy do wyliczenia tych punktów
        // na podstawie zapisów. Można go wykorzystać na przykład na liście dokumentów,
        // wyświetlając dla nich punkację.
        //
        [Description("Suma punktów przypisanych do dokumentu handlowego.")]
        public int SumaPunktów
        {
            get
            {
                //
                // Oczywiście, gdzieś te punkty musimy sumować.
                //
                //
                // Przeglądamy wszystkie obiekty Punkt przypisane do danego dokumentu
                // handlowego. W tym celu możemy posłużyć się wyżej zdefiniowanym property
                // Punkty.
                //
                //
                // ... i wyliczoną sumę zwracamy jako rezultat property.
                //
                return Punkty.Cast<Punkt>().Sum(punkt => punkt.LiczbaNależna);
            }
        }

    }

}