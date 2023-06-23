# Dodatek demonstracyjny GeekOut 2023

Dodatek pokazuje uruchamianie wydruków z workera w kontekście zmian
w repx między wersjami 2212 i 2304. Dodatek przygotowany jest dla
wersji 2306-net, przy czym opisywane zmiany dotyczą zarówno wersji
wydawanych dla .NET Framework jak i .NET6.
	
## Worker Printout

**Wariant 0:** wykonanie wydruku wskazanego z menu. Nie wymaga zmian.

**Wariant 1:** wykonanie wydruku poprzez zwrócenie rezultatu operacji
_ReportResult_. Zmiany wymaga sposób wskazania wzorca wydruku. Wskazanie
poprzez _ReportResult.TemplateType_ jest obecnie _obsolete_. Standardowe
wydruki nie mają reprezentacji w formie klasy, nie mają swojego typu,
nie mogą być w ten sposób identyfikowane. Jedyną słuszną metodą wskazania
wzorca wydruku jest użycie _ReportResult.TemplateFileName_.

**Wariant 2:** jw., ale bez wyświetlania okna z parametrami. Aby włożyć
instancję klasy parametrów w _Context_, musimy tę instancję utworzyć.
Nie możemy zawołać konstruktora wprost, bo nie mamy klasy parametrów
w logice biznesowej - mamy tę klasę w snippecie. Można to zrobić, ale
przez refleksję. To pokazuje wadę przechowywania klasy parametrów
w snippecie. Dawniej, jak klasa parametrów była osadzona w skrypcie,
to nie było możliwe odwołanie się do niej z workera. Obecnie jest to możliwe,
ale przez refleksję.

**Wariant 3:** jw., dodatkowo pokazuje sposób jak się dostać do typu
zdefiniowanego w snippecie, który znajduje się w bazie danych. Dotyczy
dowolnych typów kompilowanych dynamicznie, nie tylko snippetów.

## Osadzanie wydruku

Aby osadzić wydruk repx w dodatku:

1. Bierzemy plik  wydruku .repx (xml), wkładamy go w katalog Repx w dodatku.
   Na dzień dzisiejszy (dla wserji 2304) istotne jest, by ten katalog
   nazywał się Repx.

2. Każdy plik .repx powinien być wkompilowany w assembly jako _embedded resource_.
   O to docelowo powinno zadbać Soneta.SDK, żeby pliki o rozszerzeniu .repx były
   automatycznie traktowane jako zasób.

3. Wydruk rejestrujemy za pomocą atrybutu _DxReport_. Atrybut pochodzi
   z _Soneta.Business_, istnieje od dawna. Dawniej jako identyfikator
   wzorca wydruku podawany był _Type_, np. _typeof(XtraReportSerialization.Sprzedaz)_.
   Obecnie należy wskazać wzorrzec po nazwie, w przykładzie _"Sprzedaz.repx"_.
   Zwyczajowo rejestrujemy raport w _AssemblyInfo.<suffix>.cs_.

Dawniej osadzenie samego raportu .repx.cs w dodatku wymagało przekompilowania
tego wzorca razem z dodatkiem. A to oznaczało konieczność posiadania referencji
do assembly DevExpress, czyli tym samym licencji na DevExpress. Obecnie wydruk
jest w postaci XML-a, zatem nie wymaga kompilacji. Tym samym do osadzenia samego
wydruku w dodatku nie są już potrzebne referencje do bibliotek DevExpress.

enova365 wyszukuje pliki zasobów (np. form.xml, snippet-y i in.) w bibliotekach,
które rozpoznaje jako ,,assembly biznesowe''. Aby dodatek był widziany przez
enova365 jako moduł biznesowy, musi posiadać referencję do _Soneta.Types.dll_.
Jeżeli ta zależność nie wynika z kodu dodatku, trzeba taką zależność wymusić.
W tym przykładzie zależność jest wymuszona sztucznie przez klasę
_TreatMeAsBusinessAssembly_.

## Osadzanie snippet-ów

Snippet-y są obiektem biznesowym, mieszkającym w bazie. Zatem dodatek może zawierać
plik .dbinit.xml, gwarantujący umieszczanie kodu potrzebnych snippet-ów w bazie po
założeniu / konwersji bazy.

Wkompilowanie snippet-ów w dodatek byłoby dobrym rozwiązaniem, ale jest utrudnione
ze względu na:

1. Referencję do bibliotek UI, które na ten moment nie są publikowane w NuGet-ach.
   Snippety dziedziczą z klasy _ReportSnippet_, która pochodzi z biblioteki
   _Soneta.Business.UI.DxReport_

2. Jeśli snippet dotyka typów pochodzących od _DevExpress_, a tak jest najczęściej,
   kompilacja wymaga referencji do DevExpress. Zatem pojawia się znów potrzeba
   posiadania licencji.