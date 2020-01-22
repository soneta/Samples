# Dodatek do enova365 na podstawie szablonu z SDK (rozszerzenie VS: Soneta Platform Developer)

## Stworzenie solution dodatku na podstawie szablonu 
  1. Szablony projektów Soneta - do czego służą?
  1. Generowanie solution z szablonu "Soneta Addon (Soneta.SDK)".
  1. Warstwa biznesowa i warstwa UI - czemu oddzielnie?
  1. Warstwa "Tests" - porzucamy bo nie będziemy się nim zajmowac na tym szkoleniu.
  1. Ustawiamy wersję enova365 i atrybuty AppendTargetFrameworkToOutputPath, AggregateOutput, AggregatePath w Directory.build.props.
  1. Dodajemy odniesienie do business w UI - ułatwia to pracę nad dodatkiem.
  1. Uruchomienie dodatku: executable SonetaExplorer.exe z parametrem **/extpath="_katalog_"**.
 
## Zad.1. Dodawanie własnego widoku (Klienci) z danych enova365 - szablon ViewInfo
  1. Wygenerowanie klasy ViewInfo i pliku viewinfo.xml z szablonu "Soneta - ViewInfo".
  1. Rejestracja kafelka widoku i podwidoku.
  1. Uzupełnienie kodu wewnątrz ViewInfo: CreateView.
  1. Utworzenie viewform.xml.

## Zad.2. Definiowanie workera (UstawRabat) na wybranej tabeli - szablon Worker.
  1. Wygenerowanie klasy workera z szablonu "Soneta - Worker".
  1. Układ projektu i podkatalagi UI, ViewInfo, ViewForms - zalecane i dlaczego.
  1. Rejestracja workera - dla Kontrahent vs. dla Kontrahenci.
  1. ContextAttribute - działanie i zastosowanie.
  1. Parametry workera - wielkość rabatu i warunki (klasa ContextBase: Session i Context).
  1. Własne okno parametrów workera: plik pageform.xml i jego nazewnictwo - DataForm/DataType.
  1. Metoda przypisania rabatu do kontrahentów - MessageBoxInformation i rezultaty operacji.

## Zad.3. Rozszerzanie bazy danych - szablon Business Xml
  1. Utworzenie pliku business.xml, w celu rozszerzenia bazy danych, przy pomocy szablonu "Soneta - Business Xml".
  1. Utworzenie wpisów dotyczących nowych tabel - GUIDed: root i child - relacje.
  1. Wygenerowanie business.cs (rebuild solution).
  1. Poprawa błędów kompilacji - dodanie własnych klas-przelotek dla obiektów biznesowych: wierszy i tabel.
  1. Utworzenie weryfikatora i enuma.
  1. Utworzenie pliku dbinit.xml - inicjowanie tabeli Loty.
  1. Uruchomienie enova365 i konwersja bazy. Tabele można zobaczyć w Podglądzie tabel.
  1. UWAGA! Założone tabele będą wymagały już stale naszego dodatku. Lepiej nie uruchamiać programu bez niego.

## Zad.4. Widoki nowych tabel dodatku
  1. Utworzenie własnego widoku (ViewForm) dla nowych obiektów dodatku: KatalogLotów.
  1. Dodanie pozostałych ViewForms: KatalogSamolotów i Rezerwacje.
  1. Filtry listy - klasy parametrów filtrów widoków.
  1. Definicje wyglądu list: viewform.xml.
  1. Atrybuty RemoveButton, EditButton, NewButton.
  1. Przyciski "Dodaj", "Edytuj" - assembly NewRowAttribute.

## Zad.5. Okna edycji wierszy na podstawie szablonów PageForm
  1. pageform.xml dla własnych obiektów - korzystamy w szablonu "Soneta - PageForm".
  1. Elementy forms.xml: Row, Stack, Group, Label...
  1. Edycja prostych danych: Maszyna i Lot
  1. Edycja danych z relacjami: Rezerwacja
  1. Nadpisanie OnAdded dla Rezerwacji - inicjowanie właściwości nowego obiektu.
  1. Definiowanie lookupform.xml - przyjazny widok lookupów.
  1. Definiowanie GetList - dowolność w tworzeniu lookupów i dropdownów.
  
## Zad.6 Zakładka lotów Kontrahenta - Extender
  1. Utworzenie extendera KontrahentExtender z szablonu "Soneta - PageForm" - rozszerzamy obiekt Kontrahent.
  1. Przeniesienie KontrahentExtender.cs do warswy business
  1. Zdefiniowanie pól extendera: RezerwacjeKontrahenta jako View
